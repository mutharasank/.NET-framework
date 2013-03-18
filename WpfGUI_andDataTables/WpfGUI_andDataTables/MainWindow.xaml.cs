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

namespace WpfGUI_andDataTables
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!dal.CheckConnection())
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

            DataTable categories = Service.getCategories();
            //dataGrid1.dataGrid1.DisplayMemberPath = "CategoryID";
            //    Console.WriteLine(categories.Columns);
            // dataGrid1.ItemsSource = categories.DefaultView; 
            dataGrid1.ItemsSource = categories.DefaultView;
            dataGrid1.AutoGenerateColumns = true;


        }

        private void cBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int supplId = (int)cBoxCountries.SelectedValue;

            DataTable products = Service.getProducts(supplId);


            dataGrid1.ItemsSource = products.DefaultView;
            dataGrid1.AutoGenerateColumns = true;
            // dataGrid1.Columns[0].Visibility = Visibility.Hidden;
            //  dataGrid1.Columns[0].Header = "text";


        }


        private void MenuItemInsert_Click(object sender, RoutedEventArgs e)
        {


        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dataGrid1.SelectedItem as DataRowView;
            int id = (int)row["categoryID"];
           
                
                    Service.DeleteRow(id);
                
           
                //if (!(MessageBox.Show("Are you sure you want to delete?", "Please confirm.", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                //{
                //    // Cancel Delete.
                //    e.Handled = true;
                //   // Service.DeleteRow(selected);
                //}
            }
        }
    
}