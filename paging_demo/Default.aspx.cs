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

  protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
    GridView1.PageIndex = e.NewPageIndex;
    GridView1.DataSource = new Service().getCategories();
    this.DataBind();
  }

  protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
  {
    int categoryID = (int)GridView1.DataKeys[e.NewSelectedIndex].Values["CategoryID"];

    LblDebugInfo.Text = "categoryID = " + categoryID + ", e.NewSelectedIndex = " + e.NewSelectedIndex;
  }
}
