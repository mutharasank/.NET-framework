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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
namespace TimeAdministrationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        String sql = null;
        SqlConnection con = null;
        DataSet ds = null;
        DataTable dt = null;
        SqlDataAdapter daCustomer = null;
        decimal total;
        List<String> names = new List<String>();
        List<String> types = new List<String>();
        List<DateTime> d;
        List<String> dates = new List<String>();
        int regId2 = new Random().Next();


        public MainWindow()
        {
            InitializeComponent();
            fillEmployees();
            fillProjects();
            txtBoxID.Text = regId2.ToString();
        }

        private void fillProjects()
        {
            types.Add("'Work for a client'");
            types.Add("'Work project'");
            types.Add("'Other work'");
            cbxType.ItemsSource = types;
        }
        // Assigning future work registrations button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedEmployee = cbxEmployees.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please choose an Employee" + ex.Message);
            }
            {
                string selectedEmployee = cbxEmployees.SelectedItem.ToString();
                String ProjectType = cbxType.SelectedItem.ToString();
                String Project = lstProjects.SelectedItem.ToString();
                String newDate = null;
                String hours = calculateHours().ToString();
                int regId2 = new Random().Next();
                string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
                SqlConnection con = new SqlConnection(connStr);
                lstOverview.Items.Add("Total Registrations made : " + dates.Count
                    + "\n" + "Total hours : " + total + "\n");
                foreach (String d in dates)
                {
                    newDate = "'" + d + "'";
                    String values = "(" + regId2 + "," + selectedEmployee + "," + ProjectType + "," + "'"
+ Project + "'" + "," + newDate + "," + "'" + hours + "'" + ")";
                    string sql;
                    SqlCommand cmd;
                    try
                    {
                        sql = "INSERT INTO dbo.Registrations VALUES" + " " + values;
                        cmd = new SqlCommand(sql, con);
                        // open the connection
                        con.Open();
                        // execute the SELECT statement; a reader is returned
                        SqlDataReader reader = cmd.ExecuteReader();
                        // read the column names 
                        string headline = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string s = reader.GetName(i);
                            headline += s.PadLeft(20) + "  ";
                        }
                        List<String> names = new List<String>();
                        while (reader.Read())
                        {
                            string line = "";
                            int id = (Int32)reader[0];
                            string s1 = (string)reader[1];
                            names.Add(line += s1);
                            Console.WriteLine(line + names.Count);
                            Console.WriteLine(line);
                        }
                        // close the reader
                        reader.Close();
                        lstOverview.Items.Add(
                              "Registration ID :" + regId2 + "\n" +
                        "Employee : " + " " + selectedEmployee + "\n"
                            + "" + "Project Type : " + " " + ProjectType + "\n"
                            + "Project Name : " + " " + Project + "\n" +
                            "Dates to work : " + "\n" + newDate + "\n" +
                            "Hours " + " " + hours + "\n" +
                            "--------------");

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
                MessageBox.Show("Saved to Database");
            }

        }
        private void fillEmployees()
        {            // connection string
            string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";
            // create a connection
            SqlConnection con = new SqlConnection(connStr);
            string sql;
            SqlCommand cmd;
            try
            {
                sql = "SELECT * FROM dbo.Employees";
                cmd = new SqlCommand(sql, con);
                // open the connection
                con.Open();
                // execute the SELECT statement; a reader is returned
                SqlDataReader reader = cmd.ExecuteReader();
                // read the column names 
                string headline = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string s = reader.GetName(i);
                    headline += s.PadLeft(20) + "  ";
                }
                while (reader.Read())
                {
                    string line = "";
                    // int id = (Int32)reader[0];
                    String s1 = (String)reader[0];
                    names.Add(line += s1);
                    Console.WriteLine(line + names.Count);
                    Console.WriteLine(line);
                }
                // close the reader
                reader.Close();
                cbxEmployees.ItemsSource = names;
                cbxEmployees_Copy.ItemsSource = names;
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
        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cbxType.SelectedItem.ToString();
            if (types[0].ToString().Equals(selected))
            {
                selected = types[0];
            }
            if (types[1].ToString().Equals(selected))
            {
                selected = types[1];
            }
            if (types[2].ToString().Equals(selected))
            {
                selected = types[2];
            }
            {
                // connection string
                string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
                SqlConnection con = new SqlConnection(connStr);
                string sql;
                SqlCommand cmd;
                sql = "SELECT * from dbo.Projects where Type like" + " " + selected;
                try
                {
                    cmd = new SqlCommand(sql, con);
                    // open the connection
                    con.Open();
                    // execute the SELECT statement; a reader is returned
                    SqlDataReader reader = cmd.ExecuteReader();
                    // read the column names 
                    string headline = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string s = reader.GetName(i);
                        headline += s.PadLeft(20) + "  ";
                    }
                    List<String> projects = new List<String>();
                    while (reader.Read())
                    {
                        string line = "";
                        string s1 = (string)reader[0];
                        projects.Add(line += s1);
                        Console.WriteLine(line + projects.Count);
                        Console.WriteLine(line);
                    }                    // close the reader
                    reader.Close();
                    lstProjects.ItemsSource = projects;
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

        private void Calendar_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {
            d = Calendar.SelectedDates.ToList();
            foreach (DateTime date in d)
            {
                lstboxInfo.Items.Add(date.Date.ToShortDateString());
                dates.Add(date.Date.ToShortDateString());
            }
            calculateHours();
        }
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstboxInfo.SelectedIndex == -1) return;
            String time = lstboxInfo.SelectedValue.ToString();
            DateTime toDate = DateTime.Parse(time);
            String toShortDate = toDate.ToShortDateString();
            for (int i = 0; i < dates.Count; i++)
            {
                if (dates[i].Equals(toShortDate))
                {
                    dates.Remove(toShortDate);
                    lstboxInfo.Items.Remove(toShortDate);
                    Console.WriteLine("removed element" + toShortDate);
                }
            }
            calculateHours();
            lstboxInfo.InvalidateVisual();
        }
        public decimal calculateHours()
        {
            decimal size = dates.Count;
            double hours = 7.4;
            decimal workingHours = (decimal)hours;
            total = size * workingHours;
            txbHours.Text = total.ToString();
            return workingHours;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
            // String var = txbFrom.Text;
            con = new SqlConnection(connStr);
            // select all customers
            sql = "SELECT * FROM dbo.Registrations";
            ds = new DataSet();
            daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "dbo.Registrations");
            dt = ds.Tables["dbo.Registrations"];
            dataGrid1.DataContext = dt.DefaultView;
        }

        private void cbxEmployees_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cbxEmployees_Copy.SelectedItem.ToString();
            string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
            // String var = txbFrom.Text;
            con = new SqlConnection(connStr);
            sql = "SELECT * from dbo.Registrations where EmployeeNr LIKE" + " " + selected;
            ds = new DataSet();
            daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "dbo.Registrations");
            dt = ds.Tables["dbo.Registrations"];
            dataGrid1.DataContext = dt.DefaultView;
            sql = "SELECT * from dbo.EmployeeRegistrations where EmployeeNr LIKE" + " " + selected;
            ds = new DataSet();
            daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "dbo.EmployeeRegistrations");
            dt = ds.Tables["dbo.EmployeeRegistrations"];
            dt.Columns.Add(new DataColumn("Approved", typeof(Boolean)));
            dataGrid1_Copy.DataContext = dt.DefaultView;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommandBuilder catCB = new SqlCommandBuilder(daCustomer);
                daCustomer.UpdateCommand = catCB.GetUpdateCommand();
                daCustomer.DeleteCommand = catCB.GetDeleteCommand();
                daCustomer.InsertCommand = catCB.GetInsertCommand();
                daCustomer.Update(dt);
                MessageBox.Show("Saved to Database");
            }
            catch (Exception s)
            {
                Console.WriteLine(s + "error saving to database");
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
            SqlConnection con = new SqlConnection(connStr);
            string sql;
            SqlCommand cmd;
            foreach (DataRow row in dt.Select())
            {
                object columnValue = row["Approved"];
                object columnValue1 = row["Date"];
                object columnValue2 = row["EmployeeNr"];
                if (columnValue.ToString().Equals("True"))
                {
                    Debug.WriteLine(columnValue.ToString() + columnValue1.ToString() + columnValue2.ToString());
                    try
                    {
                        sql = "INSERT INTO dbo.ApprovedEmployees ";
                        sql += "(Date,EmployeeNr,Approved)";
                        sql += "VALUES (" + "'" + columnValue1 + "'," + "' ";
                        sql += columnValue2 + "'," + "'" + columnValue + "'" + ")";
                        cmd = new SqlCommand(sql, con);
                        // open the connection
                        con.Open();
                        // execute the SELECT statement; a reader is returned
                        SqlDataReader reader = cmd.ExecuteReader();
                        MessageBox.Show("Saved to Database");
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine(s + "error saving to database");
                    }
                    finally
                    {   // close the connection
                        //if (con.State == ConnectionState.Open) con.Close();
                        con.Close();
                    }
                }
            }
        }
    }
}
