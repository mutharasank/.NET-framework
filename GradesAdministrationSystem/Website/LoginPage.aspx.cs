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
    protected void Page_Load(object sender, EventArgs e)
    {
        //script for resolving error when rendering the page
        if (!IsPostBack)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
  new ScriptResourceDefinition
  {
      Path = "~/scripts/jquery-1.7.2.min.js",
      DebugPath = "~/scripts/jquery-1.7.2.min.js",
      CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
      CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
  });
        }

    }
    // Authetnicates the user and redirects to next page if successful
    // create session variable to hold the Çpr number of the user

    protected void AppLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String cpr = AppLogin.UserName;
        String pass = AppLogin.Password;
        String passToCompare = "";

        string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "SELECT Password FROM Students WHERE Cpr = " + cpr;
        try
        {
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
            //keeps Cpr number
            Session["Cpr"] = cpr;

            Server.Transfer("index.aspx");
        }

    }
}