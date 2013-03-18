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
using System.Windows.Shapes;

namespace LayoutPanels
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
            WindowGrid win = new WindowGrid();
            win.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            WindowDockPanel win = new WindowDockPanel();
            win.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            WindowStackPanel win = new WindowStackPanel();
            win.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            WindowWrapPanel win = new WindowWrapPanel();
            win.Show();
        }
    }
}
