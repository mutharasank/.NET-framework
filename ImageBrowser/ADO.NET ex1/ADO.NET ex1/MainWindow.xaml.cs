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
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace ADO.NET_ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            string connStr = @"Data Source=Yordan\SQLEXPRESS; database=Northwind;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";

            // create a connection
            SqlConnection con = new SqlConnection(connStr);

            string sql;
            SqlCommand cmd;
            InitializeComponent();
            // read the shippers table
            try
            {
                sql = "SELECT Country FROM Customers";
                cmd = new SqlCommand(sql, con);
                List<String> countries = new List<String>();
                // open the connection
                con.Open();

                // execute the SELECT statement; a reader is returned
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    String country = (String)reader[0];
                    countries.Add(country);


                }
                ComboBoxCountry.ItemsSource = countries;


                // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("fejl: " + ex.Message);
            }
            finally
            {   // close the connection
                //if (con.State == ConnectionState.Open) con.Close();
                con.Close();
            }




        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string connStr = @"Data Source=Yordan\SQLEXPRESS; database=Northwind;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";

            // create a connection
            SqlConnection con = new SqlConnection(connStr);

            string sql;
            SqlCommand cmd;
            String country = (String)ComboBoxCountry.SelectedValue;
            try
            {
                sql = "SELECT FirstName,LastName,City FROM Employees WHERE Country='" + country + "'";
                Debug.WriteLine(sql);
                cmd = new SqlCommand(sql, con);

                con.Open();

                // execute the SELECT statement; a reader is returned
                SqlDataReader reader = cmd.ExecuteReader();
                List<String> details = new List<String>();
                while (reader.Read())
                {
                    String FirstName = reader[0] as String;
                    String LastName = reader[1] as String;
                    String City = reader[2] as String;
                    details.Add("First name : " + FirstName + "\n" + " Last Name : " + LastName + "\n" + "City : " + City);


                }

                Debug.WriteLine(details.Count);
                ListBoxDetails.ItemsSource = details;
                // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("fejl: " + ex.Message);
            }
            finally
            {   // close the connection
                //if (con.State == ConnectionState.Open) con.Close();
                con.Close();
            }
        }

        private void Average1_Click(object sender, RoutedEventArgs e)
        {
            string connStr = @"Data Source=Yordan\SQLEXPRESS; database=Northwind;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";

            // create a connection
            SqlConnection con = new SqlConnection(connStr);

            string sql;
            SqlCommand cmd;
            String country = (String)ComboBoxCountry.SelectedValue;
            try
            {
                //sql = "SELECT AVG(freight) FROM orders";
                sql = "SELECT freight FROM orders";
                Debug.WriteLine(sql);
                cmd = new SqlCommand(sql, con);

                con.Open();

                // execute the SELECT statement; a reader is returned
                SqlDataReader reader = cmd.ExecuteReader();

                List<Decimal> details = new List<Decimal>();
                while (reader.Read())
                {
                    Decimal id = (Decimal)reader[0];

                    details.Add(id);


                }

                //// close the reader
                reader.Close();
                decimal average = details.Average();

                lbl1.Content = average;


            }
            catch (Exception ex)
            {
                Console.WriteLine("fejl: " + ex.Message);
            }
            finally
            {   // close the connection
                //if (con.State == ConnectionState.Open) con.Close();
                con.Close();
            }

        }


        private void Average2_Click(object sender, RoutedEventArgs e)
        {
            string connStr = @"Data Source=Yordan\SQLEXPRESS; database=Northwind;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";

            // create a connection
            SqlConnection con = new SqlConnection(connStr);

            string sql;
            SqlCommand cmd;
            String country = (String)ComboBoxCountry.SelectedValue;
            try
            {
                sql = "SELECT AVG(freight) FROM orders";

                Debug.WriteLine(sql);
                cmd = new SqlCommand(sql, con);

                con.Open();

                object o = cmd.ExecuteScalar();
                Decimal n = (Decimal)o;

                lbl1.Content = n;
            }
            catch (Exception ex)
            {
                Console.WriteLine("fejl: " + ex.Message);
            }
            finally
            {   // close the connection
                //if (con.State == ConnectionState.Open) con.Close();
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

            string connStr = @"Data Source=Yordan\SQLEXPRESS; database=Northwind;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";

            // create a connection
            SqlConnection con = new SqlConnection(connStr);
            string sql = "CustOrderHist";


            try
            {
                con.Open();

                SqlCommand salesCMD = new SqlCommand(sql, con);
                salesCMD.CommandType = CommandType.StoredProcedure;

                SqlParameter myParam = new SqlParameter("@CustomerID", SqlDbType.VarChar, 12);
                myParam.Value = textBox1.Text;
                salesCMD.Parameters.Add(myParam);

                SqlDataReader reader = salesCMD.ExecuteReader();
                while (reader.Read())
                {
                    Debug.WriteLine(reader[0]);
                    Debug.WriteLine(reader[1]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("fejl: " + ex.Message);
            }
            finally
            {   // close the connection
                //if (con.State == ConnectionState.Open) con.Close();
                con.Close();
            }

        }
    }
}
