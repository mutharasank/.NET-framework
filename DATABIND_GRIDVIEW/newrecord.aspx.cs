using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newrecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LblError.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Service service = new Service();
        bool ok = service.InsertCategory(TBoxName.Text, TBoxDescr.Text);

        if (ok)
            Server.Transfer("Default.aspx");
        else
            LblError.Visible = true;
    }
}
