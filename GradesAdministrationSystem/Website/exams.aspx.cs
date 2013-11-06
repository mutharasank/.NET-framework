using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Presents information about student's exams and future attempts
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Cpr"] == null)
        {
            Server.Transfer("LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            string cpr = (string)Session["Cpr"];
            Debug.WriteLine(cpr);
          
         
        }
    }
    // Fills GridView with  all exams in which the student is participating/participated
    public void displayAllExams()
    {
        string cpr = (string)Session["Cpr"];
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "SELECT * from Students WHERE Cpr = " + cpr;
        try
        {
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "Students");
            DataTable dt = ds.Tables["Students"];
            dt.Columns.Remove("Password");
            dt.Columns.Remove("Id");
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
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
    // Fills a GridView with all exams for which the student has more attempts
    private void displayExamsWithAttempts()
    {
        string cpr = (string)Session["Cpr"];
        string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "SELECT * FROM Students WHERE Cpr  LIKE '" + cpr + "' AND Passed LIKE 0 AND Attempts NOT LIKE '3' AND Exemptions NOT LIKE '3'";
        try
        {
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter daCustomer = new SqlDataAdapter(sql, con);
            daCustomer.Fill(ds, "Students");
            DataTable dt = ds.Tables["Students"];
            dt.Columns.Remove("Password");
            dt.Columns.Remove("Id");
            GridView2.DataSource = dt.DefaultView;
            GridView2.DataBind();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView2.Visible = false;
        GridView1.Visible = true;
        displayAllExams();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        GridView2.Visible = true;
        displayExamsWithAttempts();
    }
}