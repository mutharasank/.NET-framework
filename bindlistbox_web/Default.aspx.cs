using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            // get country name from DropDownList
            string country = ddListCountry.SelectedValue;

            // create a new Service class
            Service service = new Service();

            // getDataTbale with Employees
            DataTable dt = service.getEmployees(country);

            
            Literal1.Text = "";
            // iterate through the rows of the DataTable
            // get the fields and write them to a Literal1
            foreach (DataRow row in dt.Rows)
            {
                // ret fields from row
                int id = (int)row["EmployeeID"];
                string fName=row["FirstName"] as string;
                string lName = row["LastName"] as string;
                DateTime birthDate = (DateTime)row["BirthDate"];
                DateTime HireDate = (DateTime)row["HireDate"];

                // put field data into Literal1
                Literal1.Text += id + ", " + fName + " " + lName + " " +
                    birthDate.ToShortDateString() + " " +
                    HireDate.ToShortDateString() + "<br>";
            }
        }
    }
}
