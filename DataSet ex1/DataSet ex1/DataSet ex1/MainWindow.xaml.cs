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
using System.Data.SqlClient;
using System.Data;


namespace DataSet_ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String conStr, sql = null;
        SqlConnection con = null;
        DataSet ds = null;
        DataTable dt  = null;
        SqlDataAdapter daCustomer = null;
        public MainWindow()
        {

            InitializeComponent();
            conStr = @"data source=YORDAN\SQLEXPRESS; database=northwind; integrated security=true";
            con = new SqlConnection(conStr);
            // select all customers
            sql = "select * from products";
            ds = new DataSet();
           daCustomer = new SqlDataAdapter(sql, con);

           //DataColumn ProductName = new DataColumn("Product Name", typeof(String));
           //   dt.Columns.AddRange(new DataColumn[] { ProductName});


        
         
           daCustomer.Fill(ds, "Products");
         dt = ds.Tables["Products"];
            dataGrid1.DataContext = dt.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                          SqlCommandBuilder catCB = new
     SqlCommandBuilder(daCustomer);

                daCustomer.UpdateCommand = catCB.GetUpdateCommand();
                daCustomer.DeleteCommand = catCB.GetDeleteCommand();
                daCustomer.InsertCommand = catCB.GetInsertCommand();

                daCustomer.Update(dt);
            }
            catch (Exception s)
            {
                Console.WriteLine(s + "errrrrorrrr");
            }

        }
    }
}
