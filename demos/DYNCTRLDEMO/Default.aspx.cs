using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // insert lbl in PlaceHolder1
        Label lbl = new Label();
        lbl.ID = "lbl";
        lbl.Text = "This is a label";
        PlaceHolder1.Controls.Add(lbl);

        // insert newline in PlaceHolder1
        Literal lit = new Literal();
        lit.Text = "<br/>";
        PlaceHolder1.Controls.Add(lit);

        // insert tbx in PlaceHolder1
        TextBox tbox = new TextBox();
        tbox.ID = "tbox";
        tbox.Width = 200;
        PlaceHolder1.Controls.Add(tbox);

        Label lbl2 = new Label();
        lbl2.ID = "lbl2";
        lbl2.Text = "Label 2";

        TextBox tbox2 = new TextBox();
        tbox2.ID = "tbox2";
        tbox2.Width = 200;

        // create a tablerow
        TableRow row = new TableRow();

        // create a tablecell, insert lbl2 and add cell to row
        TableCell cell = new TableCell();
        cell.Controls.Add(lbl2);
        row.Cells.Add(cell);

        // create a tablecell, insert tbox2 and add cell to row
        cell = new TableCell();
        cell.Controls.Add(tbox2);
        row.Cells.Add(cell);

        // insert row in table
        Table1.Rows.Add(row);


        // insert tboxpage at the bottom of the page
        TextBox tboxpage = new TextBox();
        tboxpage.ID = "tboxpage";
        tboxpage.Text = "TextBox added to page; automatically placed at the bottom";
        tboxpage.Width = 500;
        form1.Controls.Add(tboxpage);

        // THE FOLLOWING IS ILLEGAL: outside form
        //Page.Controls.Add(tboxpage);
    }
}
