using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace ADO.NET_ex2
{
   public  class dao
    {
           static string conStr = @"Data Source=YORDAN\SQLExpress; database=Northwind; Integrated Security=true;";
        static SqlConnection con = null;

        public static SqlConnection getConnection()
        {
            if (con == null)
                con = new SqlConnection(conStr);

            return con;
        }

        public static bool CheckConnection()
        {
            SqlConnection con = getConnection();
            try
            {
                con.Open();
                con.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
    }
    }

