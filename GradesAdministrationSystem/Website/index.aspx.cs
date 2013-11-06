using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//This is the home page of the Exam system where the user can sign up for exam or see his exams
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
            showUserInfo();
        }
    }
    //Displays User information
    private void showUserInfo()
    {
        string cpr = (string)Session["Cpr"];
        // connection string
                string connStr = @"Data Source=localhost\SQLEXPRESS; database=GradesSystem;Integrated Security=True";
                SqlConnection con = new SqlConnection(connStr);
                string sql;
                SqlCommand cmd;
                sql = "SELECT Name,Cpr from Students where Cpr like" + " " + cpr;
                try
                {
                    cmd = new SqlCommand(sql, con);
                    // open the connection
                    con.Open();
                    // execute the SELECT statement; a reader is returned
                    SqlDataReader reader = cmd.ExecuteReader();
                    // read the column names 
                    string headline = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string s = reader.GetName(i);
                        headline += s.PadLeft(20) + "  ";
                    }
                    List<String> data = new List<String>();
                    while (reader.Read())
                    {
                        string line = "";
                        string s1 = (string)reader[0];
                        data.Add(line += s1);
                        Console.WriteLine(line + data.Count);
                        Console.WriteLine(line);

                           
                    }                    // close the reader
                    reader.Close();

                    foreach (String s in data)
                    {
                        userInfo.Text = "You are logged in as :" + s;
                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine("fejl: " + ex.Message);
                }
                finally
                {   // close the connection
                    //if (con.State == ConnectionState.Open) con.Close();
                    con.Close();
                }
            
    }
}