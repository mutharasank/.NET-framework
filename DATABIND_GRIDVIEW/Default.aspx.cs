using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Service service = new Service();
            DataTable dt = service.getCategories();

            GridView1.DataSource = dt;

            DataBind();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int categoryID=(int)GridView1.DataKeys[e.RowIndex].Values["CategoryID"];
        Session["catID"] = categoryID;
        string categoryName=(string)GridView1.DataKeys[e.RowIndex].Values["CategoryName"];
        Session["catName"]=categoryName;
        string description=(string)GridView1.DataKeys[e.RowIndex].Values["Description"];
        Session["descr"]=description;

        Server.Transfer("deleterecord.aspx");
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int categoryID = (int)GridView1.DataKeys[e.NewEditIndex].Values["CategoryID"];
        Session["catID"] = categoryID;
        string categoryName = (string)GridView1.DataKeys[e.NewEditIndex].Values["CategoryName"];
        Session["catName"] = categoryName;
        string description = (string)GridView1.DataKeys[e.NewEditIndex].Values["Description"];
        Session["descr"] = description;

        Server.Transfer("updaterecord.aspx");
    }
}
