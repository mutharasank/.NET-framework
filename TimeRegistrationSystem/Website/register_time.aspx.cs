using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
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
            Debug.WriteLine(cpr);
            LoadAssignedRegistrationsProjectsIDs();
            LoadAssignedProjects();
        }
    }
    /// <summary>
    /// Loads projects for which employee is assigned and binds DropDownList control
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

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string temp = (string)reader[0];

                projectNames.Add(temp);
            }

            DropDownList1.DataSource = projectNames;

            DropDownList1.DataBind();


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
    /// Fills a List of IDs of Projects for which an employee has assigned time registrations
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
    /// Gets the Id of Employee
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
    /// Gets the total assigned hours for a specific project of a employee
    /// </summary>
    private void getTotalAssignedHours()
    {
        double hours = 0;
        String projectName = DropDownList1.SelectedItem.ToString();
        sql = "SELECT SUM (estimated_hours) FROM dbo.AssignedRegistrations where project_id = " + "'" + GetIdOfProject(projectName) + "'" + "AND employee_id=" + "'" + GetIdOfEmployee() + "'";
        Debug.WriteLine(sql);
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
                hours = (double)reader[0];
            }
            Label5.Text = "You have " + hours.ToString() + " working hours assigned for this project. ";
        }

        catch (Exception ex)
        {
            Label5.Text = ("fejl: " + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }

    /// <summary>
    /// Gets the total amount of accumulated hours on specific project of employee
    /// </summary>
    private void getAccumulatedHours()
    {
        double hours = 0;
        String projectName = DropDownList1.SelectedItem.ToString();
        sql = "SELECT SUM (registered_hours) FROM dbo.TimeRegistrations where project_id = " + "'" + GetIdOfProject(projectName) + "'" + "AND employee_id=" + "'" + GetIdOfEmployee() + "'";
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
                hours = (double)reader[0];
            }
            Label6.Text = "You have " + hours.ToString() + " registered accumulated hours for this project. ";
        }

        catch (Exception ex)
        {
            Label6.Text = ("fejl: " + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }

    /// <summary>
    /// Registeres new TimeRegistration for the selected project and employee
    /// Displays information about estimated and accumulated hours on project
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Button1_Click(object sender, EventArgs e)
    {

        String projectName = DropDownList1.SelectedItem.ToString();
        DateTime date = DateTime.Today;

        string text = txbHours.Text;
        double registered_hours = 0;

        try
        {
            Double.TryParse(text, out registered_hours);
            String day = date.Day.ToString();
            String month = date.Month.ToString();
            String year = date.Year.ToString();
            String formated_date = day + "-" + month + "-" + year;

            sql = "INSERT INTO dbo.TimeRegistrations(employee_id,project_id,date,registered_hours) VALUES(" + GetIdOfEmployee() + "," + GetIdOfProject(projectName) + "," + "'" + formated_date + "'" + "," + registered_hours + ");";
            Debug.WriteLine(sql);
            con = new SqlConnection(connStr);
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            Label2.Text = "You have registered " + registered_hours.ToString() + " hours for Project : " + projectName + " on :" + formated_date;
            getTotalAssignedHours();
            getAccumulatedHours();
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Label2.Text = ("fejl: " + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }

}