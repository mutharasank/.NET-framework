using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Service
/// </summary>
public class Service
{
    // common connectionString
    string connStr = "data source=EAA-SH-ATM\\SQLExpress; Database=\"NorthWind\"; Integrated Security=true";

    // retreives some columns fromEmployee table for a specified country
    public DataTable getEmployees(string country)
    {
        // create a SqlConnection
        SqlConnection conn=new SqlConnection(connStr);

        // create a sql expression
        string sql = "SELECT employeeID, firstName, lastname";
               sql+=" ,firstname + ' '+ lastname as name, BirthDate, HireDate";
               sql += " FROM employees WHERE";
               sql += " country=@country";

        // cerate a SqlCommand
        SqlCommand cmd = new SqlCommand(sql,conn);

        // add parameter to command
        cmd.Parameters.AddWithValue("@country",country);

        // create a SqlDataAdapter for the SqlCommand
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        // create a new DataTable
        DataTable dt = new DataTable();

        // fill the DataTable using the DataAdapter
        da.Fill(dt);

        // return the DataTable
        return dt;
    }
}