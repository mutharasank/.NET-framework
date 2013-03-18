using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ADO.NET_ex2
{
    public class Service
    {
         private static DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = dao.getConnection();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error while reading database table:\n\n" + ex.Message);
            }
            return dt;

        }

        public static DataTable getSuppliers()
        {
            return getDataTable("SELECT SupplierID, CompanyName FROM Suppliers");
        }

        internal static DataTable getProducts(int supplId)
        {
            string sql = "";
            sql += "SELECT ProductId, ProductName, CategoryName, UnitPrice";
            sql += " FROM Products p";
            sql += " JOIN Categories c ON p.CategoryId=c.CategoryId";
            sql += " WHERE supplierId=" + supplId;

            return getDataTable(sql);
        }
    }
    }

