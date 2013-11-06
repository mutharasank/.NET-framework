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
using System.Diagnostics;
using System.Windows.Threading;

namespace WpfCrossThreadDemo2
{
    delegate void SetTextDelegate(TextBox tBox, object o);

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void setTextBox(TextBox tBox, object o)
        {
            if (tBox.Dispatcher.CheckAccess())
                // ok, set Text directly
                tBox.Text += o.ToString() + "-t" + Thread.CurrentThread.ManagedThreadId + " ";
            else
            {
                // call BeginInvoke om window
                Delegate del = new SetTextDelegate(setTextBox);
                this.Dispatcher.BeginInvoke(del, tBox, o);
            }
        }

        private void Run()
        {
            setTextBox(textBox1, "Threading");
            setTextBox(textBox2, "is great!!!");
            setTextBox(textBox3, "And challenging.");
            Thread.Sleep(10);
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Run));
            t.Start();
        }
    }
}
