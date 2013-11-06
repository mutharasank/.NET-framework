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
        Cookies2TextBox(TextBox3);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // create a cookie for browser
        // in response-object
        HttpCookie cookie = new HttpCookie(TextBox1.Text, TextBox2.Text);
        DateTime d = DateTime.Now;
        cookie.Expires = d.AddSeconds(30);  // expires after 30 seconds
        Response.Cookies.Add(cookie);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Cookies2TextBox(TextBox3);
    }

    public void Cookies2TextBox(TextBox txtBox)
    {
        // read all cookies for this website
        // in request-object

        txtBox.Text = "";
        for (int i = 0; i < Request.Cookies.Count; i++)
        {
            string name = Request.Cookies[i].Name;
            string val = Request.Cookies[i].Value;
            txtBox.Text += name + ": " + val + Environment.NewLine;
        }
    }

}
