using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Service
/// </summary>
public class Service
{
    // common connectionString
    string connStr = @"data source=localhost\SQLExpress2005; Database=northwind; user=sa; password=golf2000";

    // retreives some columns from Categories table 
    public DataTable getCategories()
    {
        // create a SqlConnection
        SqlConnection conn = new SqlConnection(connStr);

        // create a sql expression
        string sql = "SELECT categoryID, categoryName, Description";
        sql += " FROM categories";

        // cerate a SqlCommand
        SqlCommand cmd = new SqlCommand(sql, conn);

        // create a SqlDataAdapter for the SqlCommand
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        // create a new DataTable
        DataTable dt = new DataTable();

        // fill the DataTable using the DataAdapter
        da.Fill(dt);

        // return the DataTable
        return dt;
    }

    public bool InsertCategory(string name, string descr)
    {
        // create a SqlConnection
        SqlConnection conn = new SqlConnection(connStr);

        // create a sql expression
        string sql = "INSERT INTO categories(categoryName, Description)";
        sql += "  VALUES (@name, @descr)";

        // create a SqlCommand
        SqlCommand cmd = new SqlCommand(sql, conn);

        // add two parameters
        SqlParameter param = new SqlParameter("@name", name);
        cmd.Parameters.Add(param);

        param = new SqlParameter("@descr", descr);
        cmd.Parameters.Add(param);

        // open connection
        conn.Open();

        // execute insert (non-query)
        int rowsAffected = cmd.ExecuteNonQuery();

        // close connection
        conn.Close();

        return rowsAffected == 1;
    }

    public bool deleteCategory(int ID)
    {
        // create a SqlConnection
        SqlConnection conn = new SqlConnection(connStr);

        // create a sql expression
        string sql = "DELETE FROM categories";
        sql += " WHERE categoryID=@id";

        // create a SqlCommand
        SqlCommand cmd = new SqlCommand(sql, conn);

        // add two parameters
        SqlParameter param = new SqlParameter("@id", ID);
        cmd.Parameters.Add(param);

        // open connection
        conn.Open();

        // execute insert (non-query)
        int rowsAffected = cmd.ExecuteNonQuery();

        // close connection
        conn.Close();

        return rowsAffected == 1;
    }

    public bool UpdateCategory(int id, string name, string descr)
    {
        // create a SqlConnection
        SqlConnection conn = new SqlConnection(connStr);

        // create a sql expression
        string sql  = "UPDATE categories SET";
               sql += " categoryName=@name";
               sql += ", Description=@descr";
               sql += " WHERE categoryID=@id";

        // create a SqlCommand
        SqlCommand cmd = new SqlCommand(sql, conn);

        // add two parameters
        SqlParameter param = new SqlParameter("@id", id);
        cmd.Parameters.Add(param);

        param = new SqlParameter("@name", name);
        cmd.Parameters.Add(param);

        param = new SqlParameter("@descr", descr);
        cmd.Parameters.Add(param);

        // open connection
        conn.Open();

        // execute insert (non-query)
        int rowsAffected = cmd.ExecuteNonQuery();

        // close connection
        conn.Close();

        return rowsAffected == 1;
    }

}