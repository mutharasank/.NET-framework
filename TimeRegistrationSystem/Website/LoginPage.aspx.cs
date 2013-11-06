using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// This is a login page where the user can log in to the Exam System with his credentials

public partial class LoginPage : System.Web.UI.Page
{

    string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimeRegistrationSystem;Integrated Security=True";
    SqlConnection con = null;
    string sql;
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            EmployeeLogin.Visible = false;
            AdminLogin.Visible = false;

            //script resource definition
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-1.7.2.min.js",
                DebugPath = "~/scripts/jquery-1.7.2.min.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
            });
        }

    }

    /// <summary>
    /// Authenticates Aministratior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AdminLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String name = AdminLogin.UserName;
        String pass = AdminLogin.Password;
        String passToCompare = "";

        sql = "SELECT password FROM dbo.Administrators WHERE name = " + "'" + name + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string line = "";
                string s1 = (string)reader[0];
                passToCompare = line += s1;
                Debug.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("fejl: " + ex.Message);
        }
        finally
        {
            con.Close();
        }


        if (pass != passToCompare.Trim())
        {
            Debug.WriteLine("error");
        }
        if (pass == passToCompare.Trim())
        {
            Session["Name"] = name;

            Server.Transfer("admin_index.aspx");
        }

    }
    /// <summary>
    /// Authenticates Employee
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EmployeeLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String name = EmployeeLogin.UserName;
        string pass = EmployeeLogin.Password;
        String passToCompare = "";

        sql = "SELECT cpr FROM dbo.Employees WHERE name = " + "'" + name + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string line = "";
                long s1 = (long)reader[0];
                passToCompare = line += s1;
                Debug.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("fejl: " + ex.Message);
        }
        finally
        {
            con.Close();
        }

        if (pass != passToCompare.Trim())
        {
            Debug.WriteLine("error");
        }
        if (pass == passToCompare.Trim())
        {

            Session["Cpr"] = pass;

            Server.Transfer("employee_index.aspx");
        }
    }

    protected void btnAdministrator_Click(object sender, EventArgs e)
    {
        AdminLogin.Visible = true;
        EmployeeLogin.Visible = false;

    }
    protected void btnEmployee_Click(object sender, EventArgs e)
    {
        AdminLogin.Visible = false;
        EmployeeLogin.Visible = true;
    }

}