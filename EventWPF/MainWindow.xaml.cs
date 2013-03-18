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

namespace EventWPF
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            Thread.Sleep(1000);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button1.Click += new RoutedEventHandler(newSoundHandler);
            // can be written short as
            // button1.Click += newSoundHandler;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            button1.Click += new RoutedEventHandler(newMessageBoxHandler);
            // can be written short as
            // button1.Click += newMessageBoxHandler;
        }

        void newSoundHandler(object sender, RoutedEventArgs e)
        {
            System.Media.SystemSounds.Asterisk.Play();
            Thread.Sleep(1000);
        }

        void newMessageBoxHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You pressed Button1");
        }

    }
}
