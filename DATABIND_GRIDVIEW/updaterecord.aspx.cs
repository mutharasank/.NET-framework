using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class updaterecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LblError.Visible = false;
        
        // important: only set TextBoxes if !isPostBack
        if (!IsPostBack)
        {
            TBoxName.Text = Session["catName"] as string;
            TBoxDescr.Text = Session["descr"] as string;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Service service = new Service();
        int id=(int)Session["catID"];

        bool ok = service.UpdateCategory(id,TBoxName.Text, TBoxDescr.Text);

        if (ok)
            Server.Transfer("Default.aspx");
        else
            LblError.Visible = true;
    }
}
