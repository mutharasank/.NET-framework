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

    String IDsRegistered = "";
    String IDsAssigned = "";

    string connStr = @"Data Source=localhost\SQLEXPRESS; database=TimeRegistrationSystem;Integrated Security=True";
    SqlConnection con;
    string sql;
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Cpr"] == null)
        {
            Server.Transfer("LoginPage.aspx");
        }
        if (!IsPostBack)
        {
            long cpr = (long)Session["Cpr"];
            LoadTimeRegistrationsProjectsIDs();
            LoadAssignedRegistrationsProjectsIDs();
            LoadRegisteredProjects();
            LoadAssignedProjects(); 

        }

    }
    /// <summary>
    /// Event Handler for DropDownList with projects
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        Debug.WriteLine("Event Fired");
        if (DropDownList2.SelectedItem != null)
        {
            GridView2.Visible = true;
            GridView1.Visible = false;

            ShowRegisteredRegistrations(DropDownList2.SelectedItem.ToString());


        }
    }
    /// <summary>
    /// Event handler for DropDownList with projects
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedItem != null)
        {
            GridView2.Visible = false;
            GridView1.Visible = true;
            ShowAssignedRegistrations(DropDownList1.SelectedItem.ToString());
        }
    }
   

    /// <summary>
    /// Populates GridView control with Assigned registration for project for employee
    /// </summary>
    /// <param name="project_name"></param>
    private void ShowAssignedRegistrations(String project_name)
    {
        sql = "SELECT * from dbo.AssignedRegistrations WHERE project_id = " + "'" + GetIdOfProject(project_name) 
            + "'" + "AND " + "employee_id = " + "'" + GetIdOfEmployee() + "'";
        Debug.WriteLine(sql);
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            dataAdapter.Fill(ds, "dbo.AssignedRegistrations");
            DataTable dt = ds.Tables["dbo.AssignedRegistrations"];

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
    /// <summary>
    /// Populates GridView control with Registered Time Registration for project for employee
    /// </summary>
    /// <param name="project_name"></param>
    /// 
    private void ShowRegisteredRegistrations(String project_name)
    {

        sql = "SELECT * from dbo.TimeRegistrations WHERE project_id = " + "'" + GetIdOfProject(project_name) + "'" + "AND " + "employee_id = " + "'" + GetIdOfEmployee() + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            dataAdapter.Fill(ds, "dbo.TimeRegistrations");
            DataTable dt = ds.Tables["dbo.TimeRegistrations"];

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
    /// <summary>
    /// Populates a DropDownList with Projects in which an employee has registered Time Registrations
    /// </summary>
    private void LoadRegisteredProjects()
    {
        List<String> projectNames = new List<String>();

        sql = "SELECT name FROM dbo.Projects where dbo.Projects.project_id in " + "(" + IDsRegistered.Remove(IDsRegistered.Length - 1) + ");";

        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string temp = (string)reader[0];
                projectNames.Add(temp);
            }

            DropDownList2.DataSource = projectNames;

            DropDownList2.DataBind();

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
    /// <summary>
    /// Populates a DropDownList  with projects for which an employee is assigned to work
    /// </summary>
    private void LoadAssignedProjects()
    {
        List<String> projectNames = new List<String>();

        sql = "SELECT name FROM dbo.Projects where dbo.Projects.project_id in " + "(" + IDsAssigned.Remove(IDsAssigned.Length - 1) + ");";

        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string temp = (string)reader[0];
                Debug.WriteLine(temp + "test");
                projectNames.Add(temp);
            }

            DropDownList1.DataSource = projectNames;

            DropDownList1.DataBind();
            Session["AssignedProjects"] = projectNames;

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

    /// <summary>
    /// Gets Project IDs from Registered Time Registrations for an employee
    /// </summary>
    /// <returns></returns>
    private String LoadTimeRegistrationsProjectsIDs()
    {

        sql = "SELECT distinct project_id FROM dbo.TimeRegistrations where dbo.TimeRegistrations.employee_id = " + "'" + GetIdOfEmployee() + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int temp = (int)reader[0];
                IDsRegistered += temp.ToString() + ",";
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
        return IDsRegistered;
    }
    /// <summary>
    /// Gets Project IDs from Assigned Registrations for employee
    /// </summary>
    /// <returns></returns>
    private String LoadAssignedRegistrationsProjectsIDs()
    {

        sql = "SELECT distinct project_id FROM dbo.AssignedRegistrations where dbo.AssignedRegistrations.employee_id = " + "'" + GetIdOfEmployee() + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int temp = (int)reader[0];
                IDsAssigned += temp.ToString() + ",";
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

        return IDsAssigned;
    }
    /// <summary>
    /// Gets Id of Employee
    /// </summary>
    /// <returns></returns>
    private int GetIdOfEmployee()
    {
        long cpr = (long)Session["Cpr"];

        int id = 0;
        sql = "SELECT employee_id FROM dbo.Employees WHERE cpr  LIKE " + "'" + cpr + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader[0];
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
        return id;
    }

    /// <summary>
    /// Gets Id of a Project
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private int GetIdOfProject(String name)
    {
        long cpr = (long)Session["Cpr"];

        int id = 0;
        sql = "SELECT project_id FROM dbo.Projects WHERE name  LIKE " + "'" + name + "'";
        try
        {
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader[0];
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
        return id;
    }

   
}