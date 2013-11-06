using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
public partial class LoginPage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            
            string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
            SqlConnection con = new SqlConnection(connStr);
            string sql;
            SqlCommand cmd;
            sql = "SELECT * from dbo.Projects";
            try
            {
                cmd = new SqlCommand(sql, con);
                // open the connection
                con.Open();
                DataSet dsView = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                adp.Fill(dsView, "dbo.Projects");
                adp.Dispose();
                cbxProject.DataSource = dsView;
                cbxProject.DataTextField = "ProjectID";
                cbxProject.DataValueField = "ProjectID";
                cbxProject.DataBind();
                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("fejl: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        String empNumber = txbNr.Text.ToString();
        String pass = txbPass.Text.ToString();
        String passToCompare = "";
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "SELECT password FROM dbo.Employees WHERE EmployeeNr = " + empNumber;
        try
        {
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet dsView = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string line = "";
                string s1 = (string)reader[0];
                passToCompare = line += s1;
                Debug.WriteLine(passToCompare);
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
            Label1.Text = "Please make sure your username or password is correct.";
        }
        String project = cbxProject.SelectedItem.ToString();
        if (Request.Cookies["User"] == null)
        {
            HttpCookie userCookie = new HttpCookie("User");
            userCookie["EmployeeNumber"] = empNumber;
            userCookie["Password"] = pass;
            userCookie["Project"] = project;
            userCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userCookie);
        }
        else

        {

            HttpCookie userCookie = Request.Cookies["User"];
            userCookie["EmployeeNumber"] = empNumber;
            userCookie["Password"] = pass;
            userCookie["Project"] = project;
            userCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userCookie);
            
        }
        Server.Transfer("Registration.aspx");

    }

}

