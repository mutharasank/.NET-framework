using System;
using System.Collections.Generic;
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

namespace databinding_light1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Novel rg = new Novel { Author = "Robert Goddard", Title = "Caugth in the light", Rating = 9 };
        Novel ee = new Novel { Author = "Elsebeth Egholm", Title = "Selvrisiko", Rating = 5 };

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lblAuthor.DataContext = rg;
            lblTitle.DataContext = rg;
            sldRating.DataContext = rg;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lblAuthor.DataContext = ee;
            lblTitle.DataContext = ee;
            sldRating.DataContext = ee;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //lblAuthor.DataContext = rg;
            //lblTitle.DataContext = rg;
            //sldRating.DataContext = rg;

            stPanelMain.DataContext = rg;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //lblAuthor.DataContext = ee;
            //lblTitle.DataContext = ee;
            //sldRating.DataContext = ee;

            stPanelMain.DataContext = ee;
        }
    }
}
