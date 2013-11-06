using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Handles  the signing up for exam proces for specific exam
//Only for exams for which the student is allowed to participate
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
            fillDropDownList();
        }
    }

    //Fills DropDownList only with exams for which the student is allowed to attempt
    private void fillDropDownList()
    {
        string cpr = (string)Session["Cpr"];

        string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd;
        sql = "SELECT Exam FROM Students WHERE Passed LIKE 0 AND Attempts NOT LIKE '3' AND Exemptions NOT LIKE '3'";
        try
        {
            cmd = new SqlCommand(sql, con);
            // open the connection
            con.Open();
            DataSet dsView = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            adp.Fill(dsView, "Exam");
            adp.Dispose();
            DropDownList1.DataSource = dsView;
            DropDownList1.DataTextField = "Exam";
            DropDownList1.DataValueField = "Exam";
            DropDownList1.DataBind();
            SqlDataReader reader = cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("fejl: " + ex.Message);
        }
        finally
        {
            //    con.Close();
        }
    }
    // Handles the signing up for specific exam
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cpr = (string)Session["Cpr"];

        string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
        SqlConnection con = new SqlConnection(connStr);
        string sql;
        SqlCommand cmd, cmd2;
        sql = "SELECT Name,Education,StartDate,Password FROM Students WHERE Cpr = " + cpr;
        String sql5 = "";
        String exam = DropDownList1.SelectedItem.ToString();
        try
        {
            con.Open();
            cmd = new SqlCommand(sql, con);
            // open the connection


            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                //    string s1 = (string)reader[0] + reader[1] + reader[2] + reader[3];
                sql5 = "INSERT INTO Students ";
                sql5 += "(Cpr,Name,Exam,Grade,Attempts,Exemptions,Passed,Education,StartDate,Password)";
                sql5 += "VALUES (" + "'" + cpr + "'," + "'" + reader[0].ToString() + "'," + "'" + exam + "'," + "NULL" + ",";
                sql5 += "'" + "0" + "'," + "'" + "0" + "'," + "'" + "0" + "'," + "'" + reader[1].ToString() + "'," + "'" + reader[2].ToString() + "'," + "'" + reader[3].ToString() + "'" + ")";

                Label2.Text = "You registered : " + reader[0].ToString() + " with CPR : " + cpr + " for : " + exam;

            }

            Debug.WriteLine(sql5.ToString());
            con.Close();

        }

        catch (Exception ex)
        {
            Debug.WriteLine("fejl: " + ex.Message);
        }
        finally
        {

            try
            {
                cmd2 = new SqlCommand(sql5, con);

                SqlDataReader reader = cmd2.ExecuteReader();


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

}