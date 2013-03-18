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

namespace Dag7Opg1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Person p = new Person { Name = "NN", Height = 1.8, Weight = 84 };
            TopStackPanel.DataContext = p;


        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(TopStackPanel.DataContext.ToString());
        }

        private void btnSet1_Click(object sender, RoutedEventArgs e)
        {
            Person p = (Person)TopStackPanel.DataContext;
            p.Height = 1.70; p.Weight = 70;
        }

        private void btnSet2_Click(object sender, RoutedEventArgs e)
        {
            Person p = (Person)TopStackPanel.DataContext;
            p.Height = 1.64; p.Weight = 87;

        }
    }
}
