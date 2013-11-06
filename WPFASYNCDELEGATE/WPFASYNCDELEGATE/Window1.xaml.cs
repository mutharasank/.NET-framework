using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace WpfAsyncDelegate
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private delegate void ListBoxAddDelegate(int prime);
        private delegate void ProgressbarPerformStepDelegate(ProgressBar pgb, int p);
        private delegate void SetButtonDelegate(Button btn, bool enabled);
        private delegate void SetLabelTextDelegate(Label lbl, string text);

        private bool workCancelled;

        private Thread workerThread;

        public Window1()
        {
            InitializeComponent();
        }

        public void Trace(string msg)
        {
            Console.WriteLine("ThreadID: {0,3}, msg: {1}", Thread.CurrentThread.ManagedThreadId, msg);
        }


        private bool isPrime(int x)
        {
            int i = 2;
            bool divisorFound = false;
            while (!divisorFound && i < x)
            {
                if (x % i == 0)
                    divisorFound = true;
                i++;
            }
            return !divisorFound;
        }

        public void GeneratePrimes(int lower, int upper)
        {
            Trace("GeneratePrimes " + Thread.CurrentThread.ManagedThreadId);

            int lastPrime = 1;
            int lastPct = 0;
            int candidate = lower;
            while (!workCancelled && candidate <= upper)
            {
                if (isPrime(candidate))
                {
                    lastPrime = candidate;
                    Display(candidate); // ***
                }
                int pct = (int)(1.0 * (candidate - lower) / (upper - lower) * 100);
                if (pct - lastPct >= 10)
                {
                    ProgressbarPerformStep(pgbPrimes, pct); // ***
                    SetLabelText(lblProgress, pct + " %"); // ***
                    lastPct = pct;
                }
                candidate += 1;
            }
            if (!workCancelled)
            {
                ProgressbarPerformStep(pgbPrimes, 100);
                SetLabelText(lblProgress, " 100%");
                SetLabelText(lblStatus, "Worker stopped");
            }
            else
                SetLabelText(lblStatus, "Worker cancelled");

            SetButtonEnabled(btnStartWorker, true);
            SetButtonEnabled(btnCancelWorker, false);
        }


        private void btnStartWorker_Click(object sender, RoutedEventArgs e)
        {
            Trace("btnStartWorker_Click");

            lbxPrimes.Items.Clear();
            pgbPrimes.Value = 0;
            lblProgress.Content = "0";
            lblStatus.Content = "Worker running";
            btnStartWorker.IsEnabled = false;
            btnCancelWorker.IsEnabled = true;

            workCancelled = false;
            ThreadStart worker = delegate { this.GeneratePrimes(4000000, 4002500); };
            workerThread = new Thread(worker);
            workerThread.Start();
        }

        private void btnCancelWorker_Click(object sender, RoutedEventArgs e)
        {
            Trace("btnCancelWorker_Click");
            workCancelled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Trace("Window_Closing");

            workCancelled = true;
            while (workerThread != null && workerThread.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(10);
            }
        }


        private void Display(int prime)
        {
            if (lbxPrimes.Dispatcher.CheckAccess())
            {
                Trace("Display, Access OK " + Thread.CurrentThread.ManagedThreadId);
                lbxPrimes.Items.Add(prime);
            }
            else
            {
                Trace("Display, Access NOT OK " + Thread.CurrentThread.ManagedThreadId);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ListBoxAddDelegate(Display), prime);
            }
        }

        private void ProgressbarPerformStep(ProgressBar pgb, int p)
        {
            if (pgb.Dispatcher.CheckAccess())
            {
                Trace("Display, Access OK " + Thread.CurrentThread.ManagedThreadId);
                pgb.Value = p;
            }
            else
            {
                Trace("Display, Access NOT OK " + Thread.CurrentThread.ManagedThreadId);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ProgressbarPerformStepDelegate(ProgressbarPerformStep), pgb, p);
            }
        }

        private void SetButtonEnabled(Button btn, bool enabled)
        {
            if (btn.Dispatcher.CheckAccess())
            {
                Trace("Display, Access OK " + Thread.CurrentThread.ManagedThreadId);
                btn.IsEnabled = enabled;
            }
            else
            {
                Trace("Display, Access NOT OK " + Thread.CurrentThread.ManagedThreadId);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new SetButtonDelegate(SetButtonEnabled), btn, enabled);
            }
        }

        private void SetLabelText(Label lbl, string text)
        {
            if (lbl.Dispatcher.CheckAccess())
            {
                Trace("Display, Access OK " + Thread.CurrentThread.ManagedThreadId);
                lbl.Content = text;
            }
            else
            {
                Trace("Display, Access NOT OK " + Thread.CurrentThread.ManagedThreadId);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new SetLabelTextDelegate(SetLabelText), lbl, text);
            }
        }
    }
}
