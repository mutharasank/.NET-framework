using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfGUI_andDataTables
{
    public class Service
    {
        private static DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = dal.getConnection();
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
        public static DataTable getCategories()
        {
            return getDataTable("SELECT CategoryID, CategoryName, Description FROM Categories");
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

       internal static void DeleteRow(int categoryId)
        {
            string sql = "";
            sql+= "DELETE * FROM Category";
            sql += " WHERE CategoryID=" + categoryId;

        }
        public void UpdateRow(int categoryId, string categoryName, string categoryDesc)
        {
        }
        public void NewRow(string categoryName, string categoryDesc)
        {
            
        }

    }
}
