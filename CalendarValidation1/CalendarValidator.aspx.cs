using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CalendarValidator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
   new ScriptResourceDefinition
   {
       Path = "~/scripts/jquery-1.7.2.min.js",
       DebugPath = "~/scripts/jquery-1.7.2.min.js",
       CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
       CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
   });

    }
    //RequiredFieldValidator1.ControlToValidate = TextBox1.Text;
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime date1 = DateTime.Now;
        DateTime date2 = date1.AddDays(90);
        DateTime dateSelected = Calendar1.SelectedDate;
        if (dateSelected > date2)
        {
            Label3.Text = "Date must be in the range of 90 days from today";

        }
    }
}