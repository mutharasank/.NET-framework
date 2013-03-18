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

namespace Listbox_products
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Product> products = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();

            products.Add(new Product { Nr = 101, Name = "Boremaskine", Price = 800, inStock = 5 });
            products.Add(new Product { Nr = 120, Name = "Hammer", Price = 85.00, inStock = 13 });
            products.Add(new Product { Nr = 145, Name = "Skruetrækker", Price = 67.00, inStock = 20 });
            products.Add(new Product { Nr = 200, Name = "Svensknøgle", Price = 120.00, inStock = 8 });
            products.Add(new Product { Nr = 238, Name = "Umbraco sæt", Price = 45.00, inStock = 20 });
            products.Add(new Product { Nr = 281, Name = "Sav", Price = 78.95, inStock = 33 });
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lBoxProducts.ItemsSource = products;
            lBoxProducts.DisplayMemberPath = "NrAndName";
            lBoxProducts.SelectedValuePath = "Nr";
        }

        private void RBtnToString_Checked(object sender, RoutedEventArgs e)
        {
            lBoxProducts.DisplayMemberPath = "";
        }

        private void RBtnNrAndName_Checked(object sender, RoutedEventArgs e)
        {
            lBoxProducts.DisplayMemberPath = "NrAndName";
        }

        private void BtnSelctedItem_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)lBoxProducts.SelectedItem;
            if (product != null)
                MessageBox.Show("SelectedItem: type=" + product.GetType() + "\n" + product);
            else
                MessageBox.Show("SelectedItem: null");
        }

        private void BtnSelectedValue_Click(object sender, RoutedEventArgs e)
        {
            object o = lBoxProducts.SelectedValue;
            int? nr = (int?)o;
            if (o != null)
                MessageBox.Show("SelectedValue: type=" + o.GetType() + "\n" + nr);
            else
                MessageBox.Show("SelectedValue: null");
        }
    }
}
