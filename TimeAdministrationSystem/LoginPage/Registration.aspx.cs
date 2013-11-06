using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["User"];
            Debug.WriteLine(cookie + "this is the cookie");
            if (cookie != null)
            {
                String employeeNr = cookie["EmployeeNumber"];
                String password = cookie["Password"];
                String project = cookie["Project"];
                Debug.WriteLine(employeeNr + password + project + "values from login page");
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["User"];
        String login = DateTime.Now.ToLongTimeString();
        String loginDate = DateTime.Now.ToShortDateString();
        String type = cookie["Type"];
        String logout = "Currently Working";
        String timespan = "not calculated yet";
        String employeeNr = cookie["EmployeeNumber"];
        String password = cookie["Password"];
        String project = cookie["Project"];
        string id = new Random().Next().ToString();
        cookie["ID"] = id;
        if (cookie != null)
        {
            cookie["Login"] = login;
            cookie["LoginDate"] = loginDate;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
        }
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "INSERT INTO dbo.EmployeeRegistrations ";
        sql += "(Date,login,logout,EmployeeNr,TypeOfProject,ProjectID,Timespan,id)";
        sql += "VALUES (" + "'" + loginDate + "'," + "'" + login.Substring(0, 8) + "'," + "'" + logout + "'," + "'" + employeeNr + "',";
        sql += "'" + type + "'," + "'" + project + "'," + "'" + timespan + "'," + "'" + id + "'" + ")";

        Debug.WriteLine(sql);
        cmd = new SqlCommand(sql, con);
        // open the connection
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        btnStart.Enabled = false;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["User"];
        String employeeNr = cookie["EmployeeNumber"];
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "SELECT * from dbo.Registrations WHERE EmployeeNr = " + employeeNr;
        try
        {
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "dbo.Registrations");
            DataTable dt = ds.Tables["dbo.Registrations"];
            dataGrid1.DataSource = dt.DefaultView;
            dataGrid1.DataBind();
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
    protected void btnEnd_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["User"];
        String loginTime = cookie["Login"];
        String logout = DateTime.Now.ToLongTimeString();
        String loginDate = DateTime.Now.ToShortDateString();
        String employeeNr = cookie["EmployeeNumber"];
        String id = cookie["ID"];
        if (cookie != null)
        {
            cookie["Logout"] = logout;
            cookie["LoginDate"] = loginDate;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
        }
        DateTime loginDateTime = DateTime.Now.Date + TimeSpan.Parse(loginTime.Substring(0, 8));
        DateTime logoutDateTime = Convert.ToDateTime(logout);
        TimeSpan Timespan = logoutDateTime - loginDateTime;
        String timespan = Timespan.Hours + "," + Timespan.Minutes;
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimePlan;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;

        sql = "UPDATE dbo.EmployeeRegistrations SET logout = ";
        sql += "'" + logout + "'" + ",Timespan=" + "'" + timespan + "' ";
        sql += "WHERE EmployeeNr = " + "'" + employeeNr + "' AND ";
        sql += "login=" + "'" + loginTime.Substring(0, 8) + "' AND ";
        sql += "id=" + "'" + id + "'";


        //string q = "INSERT INTO employees VALUES(@1, @2, @3)";
        //SqlCommand comm = new SqlCommand(q, null);
        //comm.Parameters.Add("@1", 123);
        cmd = new SqlCommand(sql, con);
        //  open the connection
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        btnStart.Enabled = false;
    }

}