using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataSetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = @"data source=localhost\SQLEXPRESS2005; database=northwind; integrated security=true";

            SqlConnection con = new SqlConnection(conStr);

            DataSet ds = new DataSet();

            // select all customers
            string sql = "select * from customers";
            SqlDataAdapter daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "Customers");

            // select all orders
            sql = "select * from orders";
            SqlDataAdapter daOrders = new SqlDataAdapter(sql, con);
            daOrders.Fill(ds, "Orders");

            // define a relation between the two tables
            DataRelation custOrderRel =
                    ds.Relations.Add(
                        "CustOrders",
                        ds.Tables["Customers"].Columns["CustomerID"],
                        ds.Tables["Orders"].Columns["CustomerID"]);
            

            // travers customers and use GetChildRows
            foreach (DataRow custRow in
              ds.Tables["Customers"].Rows)
            {
                Console.WriteLine(custRow["CustomerID"]);

                foreach (DataRow orderRow in
                         custRow.GetChildRows(custOrderRel))

                    Console.WriteLine(" - " + orderRow["OrderID"]);
            }

            Console.ReadLine();
        }
    }
}
