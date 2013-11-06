using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    /// <summary>
    /// Displays User information
    /// </summary>
    private void showUserInfo()
    {
        string name = (string)Session["Cpr"];
        long cpr = (long)Convert.ToInt64(name);
        Session["Cpr"] = cpr;
        userInfo.Text = "You are logged in as  Employee.  CPR : " + name;
      
    }


}