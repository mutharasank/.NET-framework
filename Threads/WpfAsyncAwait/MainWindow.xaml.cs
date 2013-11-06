using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //A method containing await must be marked with async.
        private async void btnDoWorkAsync_Click(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "DoWork running...";
            //await and Task.Run:
            // 1) Runs the Dowork method on a thread from ThreadPool.
            // 2) Blocks this method (i.e. btnDoWork_Click) until Dowork() is finished.
            // 3) Returns at once to the method calling this method (i.e. the Dispatcher on the UI-thread).
            //string s = await Task.Run(new Func<string>(() => DoWork()));
            string s = await Task.Run(() => DoWork());
            lbl1.Content = s;
        }

        private void btnDoWork_Click(object sender, RoutedEventArgs e)
        {
            lbl2.Content = "DoWork running...";
            string s = DoWork(); ;
            lbl2.Content = s;
        }

        private string DoWork()
        {
            Thread.Sleep(5000);
            return "DoWork done";
        }
    }
}
