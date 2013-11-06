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
using System.Windows.Threading;
using System.Diagnostics;

namespace WpfCrossThreadDemo
{

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GUI running on ThreadId=" + Thread.CurrentThread.ManagedThreadId);
        }

        private void Run(object o)
        {
            Debug.WriteLine("Run(" + o.ToString() + ") called from Thread=" + Thread.CurrentThread.ManagedThreadId);
            textBox1.Text += o.ToString() + "-" + Thread.CurrentThread.ManagedThreadId + " ";

            Thread.Sleep(2000);
        }

        // call from GUI-thread
        private void btnFromGUI_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "GUI-" + Thread.CurrentThread.ManagedThreadId + " ";
        }

        // illegal call from non-GUI-thread
        private void btnWrong_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Run));
            t.Start("Illegal from Thread");
        }

        // correct call from non-GUI-thread
        private void btnCorrect_Click(object sender, RoutedEventArgs e)
        {
            //long calculation is done here in a thread in a method calc()
            this.Dispatcher.BeginInvoke(new TextBox1Delegate(Run), DispatcherPriority.Normal, "Legal from thread");
        }

        //--------------------------------------------------------------------------------------------------------

        // responsability left to addToTextBox method

        delegate void TextBox1Delegate(object o);

        private void addToTextBox(object o)
        {
            int ThreadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine(" ThreadId=" + ThreadId + ", addTotextBox.CheckAccess=" + textBox1.Dispatcher.CheckAccess());

            if (textBox1.Dispatcher.CheckAccess())
                textBox1.Text += o.ToString() + Thread.CurrentThread.ManagedThreadId + " ";
            else
            {
                Delegate del = new TextBox1Delegate(addToTextBox);
                this.Dispatcher.BeginInvoke(del, o);
            }
        }

        private void Run2()
        {
            addToTextBox("From Thread(2)");
            Thread.Sleep(10);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            addToTextBox("From GUI(2)");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(Run2));
            t1.Start();
        }

    }
}
