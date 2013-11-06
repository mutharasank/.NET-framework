using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

namespace BackgroundWorker_Threading_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_loaded(object sender, RoutedEventArgs e)
        {

            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ScanDisk);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ReportFileName);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
        }


        private void btnStartScan_Click(object sender, RoutedEventArgs e)
        {
            lbxFiles.Items.Clear();

            worker.RunWorkerAsync();

        }

        private void btnStopScan_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        private void btnShowFile_Click(object sender)
        {
        }
        private void ScanDisk(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < 3; i++)
            {

                //    string[] files= Directory.GetFiles(@"c:\myfiles","*.txt");
                worker.ReportProgress(1, "filename");
                if (worker.CancellationPending)
                    break;
            }
        }
        private void ReportFileName(object sender, ProgressChangedEventArgs e)
        {
            string filename = (string)e.UserState;
            lbxFiles.Items.Add(filename);
        }
    }
}
