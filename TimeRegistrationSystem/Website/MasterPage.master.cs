using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Contains LinkButton handlers
public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Server.Transfer("employee_registrations.aspx");
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Server.Transfer("register_time.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Server.Transfer("LoginPage.aspx");
    }
}
