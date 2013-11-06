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
using System.Diagnostics;

namespace WpfActionTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Action<string> messageTarget;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            messageTarget = ShowWindowsMessage;
            //messageTarget = s => MessageBox.Show(s);
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            messageTarget = WriteDebugMessage;
            //messageTarget = s => Debug.WriteLine(s);
        }

        private void WriteDebugMessage(string message)
        {
            Debug.WriteLine(message);
        }

        private void ShowWindowsMessage(string message)
        {
            MessageBox.Show(message);
        }


        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (messageTarget!=null) 
                messageTarget("button1 clicked");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (messageTarget != null) 
                messageTarget("button2 clicked");
        }
    }
}
