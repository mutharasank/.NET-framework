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
using System.Threading;
using System.ComponentModel;

namespace WpfBackGroundWorker
{
    public partial class Window1 : Window
    {
        private int lastPrime = 2;

        private BackgroundWorker worker = new BackgroundWorker();

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 3 events in BackgroundWorker
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.doWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.progressChanged);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.runWorkerCompleted);

            // 4 properties in BackgroundWorker
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            // worker.IsBusy //read only
            // worker.CancellationPending //read only

            // 3 methods in BackgroundWorker
            // RunWorkerAsync() // raises the DoWork event; this will start the eventhandler in DoWork in another thread 
            // CancelAsync() // sets the CancellationPending property to true; when the other thread stops, the RunWorkerCompleted event is raised
            // ReportProgress() // called from the eventhandler in the DoWork event; raises the ProgressChanged event
        }

        private void btnStartWorker_Click(object sender, RoutedEventArgs e)
        {
            Trace("btnStartWorker_Click");

            lbxPrimes.Items.Clear();
            pgbPrimes.Value = 0;
            lblProgress.Content = "0";
            lblStatus.Content = "Worker Working";
            btnStartWorker.IsEnabled = false;
            btnCancelWorker.IsEnabled = true;

            worker.RunWorkerAsync(new int[] { 4000000, 4005000 });
        }

        private void btnCancelWorker_Click(object sender, RoutedEventArgs e)
        {
            Trace("btnCancelWorker_Click");

            worker.CancelAsync();
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
            Trace("GeneratePrimes");

            int candidate = lower;
            while (candidate <= upper)
            {
                if (isPrime(candidate))
                {
                    lastPrime = candidate;

                    int pct = (int)(100.0 * (candidate - lower) / (upper - lower) + 0.5);
                    worker.ReportProgress(pct);

                    if (worker.CancellationPending)
                        break;
                }
                candidate += 1;
            }
            // ReportProgress to reach 100%
            // worker.ReportProgress(100);
        }

        // method for the DoWork event 
        private void doWork(object sender, DoWorkEventArgs e)
        {
            Trace("DoWork");

            int[] args = (int[])e.Argument;
            GeneratePrimes(args[0], args[1]);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
                e.Result = lastPrime;
        }

        // method for the ProgressChanged event
        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            Trace("ProgressChanged ");

            lbxPrimes.Items.Insert(0, lastPrime);
            lblProgress.Content = e.ProgressPercentage + " %";
            pgbPrimes.Value = e.ProgressPercentage;
        }

        // method for the RunWorkerCompleted event
        private void runWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Trace("RunWorkerCompleted");

            btnStartWorker.IsEnabled = true;
            btnCancelWorker.IsEnabled = false;

            if (e.Cancelled)
                lblStatus.Content = "Worker Cancelled. Last prime: " + lastPrime;
            else
                lblStatus.Content = "Worker Completed. Last prime: " + (int)e.Result;
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Trace("Form1_FormClosing");

            if (worker.WorkerSupportsCancellation)
            {
                worker.CancelAsync();
                if (worker != null && worker.IsBusy)
                {
                    e.Cancel = true;
                    Trace("Can't close, Worker is running");
                }
            }
        }
    }
}
