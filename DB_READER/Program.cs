using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DB_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            // connection string
            string connStr = @"Data Source=localhost\SQLEXPRESS; database=Northwind;Integrated Security=True";
            //string connStr = @"Data Source=(localdb)\Projects; database=Northwind;Integrated Security=True";

            // create a connection
            SqlConnection con = new SqlConnection(connStr);

            string sql;
            SqlCommand cmd;

            // read the shippers table
            try
            {
                sql = "SELECT * FROM shippers";
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
                    headline += s.PadLeft(20) + " | ";
                }

                Console.WriteLine(headline);
                Console.WriteLine("====================================================================");

                // read all rows and print
                while (reader.Read())
                {
                    // onw way to read:
                    string line = "";
                    int id = (Int32)reader[0];
                    string s1 = (string)reader[1];
                    string s2 = (string)reader[2];

                    line += ("" + id).PadLeft(20) + " | ";
                    line += s1.PadLeft(20) + " | ";
                    line += s2.PadLeft(20) + " | ";

                    // -------------------------------------------------
                    // another way to read:
                    //string line = "";
                    //int id = (Int32)reader["ShipperID"];
                    //string s1 = (string)reader["CompanyName"];
                    //string s2 = (string)reader["Phone"];

                    //line += ("" + id).PadLeft(20) + " | ";
                    //line += s1.PadLeft(20) + " | ";
                    //line += s2.PadLeft(20) + " | ";

                    // -------------------------------------------------
                    // a third way to read:
                    //string line = "";
                    //for (int i = 0; i < reader.FieldCount; i++)
                    //{
                    //  string s = reader[i].ToString();
                    //  line += s.PadLeft(20) + " | ";
                    //}

                    Console.WriteLine(line);
                }

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

            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();

            // -------------------------------------------------------------------------------------
            // read some scalar results (Note: a reader is not used)
            try
            {
                con.Open();

                sql = "SELECT count(*) FROM \"Order Details\"";
                cmd = new SqlCommand(sql, con);
                int count = (int)cmd.ExecuteScalar();
                Console.WriteLine("Row count of 'Order Details': " + count);
                Console.WriteLine();

                sql = "SELECT sum(UnitPrice*Quantity) FROM [Order Details]";
                cmd = new SqlCommand(sql, con);
                object o = cmd.ExecuteScalar();
                Console.WriteLine("Type of returned scalar: " + o.GetType().ToString());
                Decimal d = (Decimal)o;
                Console.WriteLine("Total price of units: " + d);
            }
            finally
            {
                con.Close();
            }

            Console.ReadLine();
        }
    }
}
