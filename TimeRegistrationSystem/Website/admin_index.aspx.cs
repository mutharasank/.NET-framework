using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_index : System.Web.UI.Page
{
    string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimeRegistrationSystem;Integrated Security=True";
    SqlConnection con;
    string sql;
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Name"] == null)
        {
            Server.Transfer("LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            showUserInfo();
            GetAllEmployees();

        }
    }
    /// <summary>
    /// Displays User information
    /// </summary>
    private void showUserInfo()
    {
        string name = (string)Session["Name"];
        userInfo.Text = "You are logged in as Administrator : " + name;
    }

    /// <summary>
    /// Gets All Assigned Registrations of employee
    /// </summary>
    private void GetAllAssignedRegistrations()
    {

        sql = "SELECT * FROM dbo.AssignedRegistrations WHERE employee_id = " + "'" + GetIdOfEmployee() + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            // SqlDataReader reader = cmd.ExecuteReader();

            DataSet ds = new DataSet();

            dataAdapter.Fill(ds, "dbo.AssignedRegistrations");
            DataTable dt = ds.Tables["dbo.AssignedRegistrations"];

            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
            //   reader.Close();

        }

        catch (Exception ex)
        {
            Debug.WriteLine("failed to get all assigned registrations: " + ex.Message);
        }
        finally
        {
            con.Close();
        }

    }
    /// <summary>
    /// Gets All Registered Time Registrations of employee
    /// </summary>
    private void GetAllTimeRegistrations()
    {

        sql = "SELECT * FROM dbo.TimeRegistrations WHERE employee_id = " + "'" + GetIdOfEmployee() + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            // SqlDataReader reader = cmd.ExecuteReader();

            DataSet ds = new DataSet();

            dataAdapter.Fill(ds, "dbo.TimeRegistrations");
            DataTable dt = ds.Tables["dbo.TimeRegistrations"];

            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
            //   reader.Close();

        }

        catch (Exception ex)
        {
            Debug.WriteLine("failed to get all assigned registrations: " + ex.Message);
        }
        finally
        {
            con.Close();
        }

    }
    /// <summary>
    /// Returns ID of employee
    /// </summary>
    /// <returns></returns>
    private int GetIdOfEmployee()
    {
        string name = DropDownList1.SelectedItem.ToString();

        int id = 0;
        sql = "SELECT employee_id FROM dbo.Employees WHERE name  LIKE " + "'" + name + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader[0];
            }

            reader.Close();
        }

        catch (Exception ex)
        {
            Debug.WriteLine("fejl " + ex.Message);
        }
        finally
        {

            con.Close();
        }
        return id;
    }
    /// <summary>
    /// Gets all employees from DataBase
    /// </summary>
    private void GetAllEmployees()
    {
        List<String> names = new List<String>();

        sql = "SELECT name FROM dbo.Employees";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = (string)reader[0];
                names.Add(name);
            }
            reader.Close();
            DropDownList1.DataSource = names;
            DropDownList1.DataBind();



        }

        catch (Exception ex)
        {
            Debug.WriteLine("fail to get all employees: " + ex.Message);
        }
        finally
        {
            con.Close();
        }

    }
    /// <summary>
    /// Event handler for DropDown list with employees
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (CheckBox1.Checked == true)
        {

            GetIdOfEmployee();
            GetAllAssignedRegistrations();
        }
        if (CheckBox2.Checked == true)
        {


            GetIdOfEmployee();
            GetAllTimeRegistrations();
        }


    }
}