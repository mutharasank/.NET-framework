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
using System.Data;

namespace ADO.NET_ex2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
          public MainWindow()
        {
            InitializeComponent();

            if (!dao.CheckConnection())
            {
                MessageBox.Show("Forbindelse til database eksisterer ikke.\nProgrammet kan ikke fortsætte.");
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable supplTable = Service.getSuppliers();
            cBoxCountries.ItemsSource = supplTable.DefaultView;
            cBoxCountries.DisplayMemberPath = "CompanyName";
            cBoxCountries.SelectedValuePath = "SupplierID";
        }

        private void cBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int supplId = (int)cBoxCountries.SelectedValue;

            DataTable products=Service.getProducts(supplId);

            dataGrid1.ItemsSource = products.DefaultView;
            dataGrid1.AutoGenerateColumns = true;
        }
    }
    
}
