using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // get country name from DropDownList
        string country = ddListCountry.SelectedValue;

        // create a new Service class
        Service service = new Service();

        // getDataTbale with Employees
        DataTable dt = service.getEmployees(country);

        // Bind ListBox to DataTbale, dt
        LBoxEmployees.DataSource = dt;

        // set DataTextField (shown in ListBox)
        if (RadioButtonList1.SelectedValue == "last")
            LBoxEmployees.DataTextField = "Lastname";
        else
            if (RadioButtonList1.SelectedValue == "first")
                LBoxEmployees.DataTextField = "Firstname";
            else
                LBoxEmployees.DataTextField = "name";

        // set DataValueField (is used to initialise the SelectedValue after selection)
        LBoxEmployees.DataValueField = "EmployeeID";

        DataBind();

    }
}
