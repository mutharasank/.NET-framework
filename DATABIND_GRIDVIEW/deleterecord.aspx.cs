using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class deleterecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LblRecord.Text = Session["catName"] + " " + Session["descr"];

        LblError.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Service service = new Service();
        int id = (int)Session["catID"];

        bool ok=service.deleteCategory(id);

        if (ok)
            Server.Transfer("Default.aspx");
        else
            LblError.Visible = true;
    }
}
