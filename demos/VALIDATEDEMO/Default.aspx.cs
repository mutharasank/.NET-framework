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
        if (!IsPostBack)
            tBoxCPR.Text = "1906519999";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Validate();

        if (this.IsValid)
            TextBox3.Text = "" + (Convert.ToInt32(TextBox1.Text) + Convert.ToInt32(TextBox2.Text));

    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string cpr = (string)args.Value;
        args.IsValid = isValidateCPR(cpr);
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string cpr = (string)args.Value;
        args.IsValid = isValidateCPR(cpr);
    }

    private bool isValidateCPR(string cpr)
    {
        bool ok = true;

        if (cpr.Length != 10) ok = false;

        foreach (char ch in cpr)
            if (!Char.IsDigit(ch)) ok = false;

        try
        {
            int y = Convert.ToInt32(cpr.Substring(4, 2));
            int m = Convert.ToInt32(cpr.Substring(2, 2));
            int d = Convert.ToInt32(cpr.Substring(0, 2));

            DateTime dt = new DateTime(y, m, d);
        }
        catch (Exception)
        {
            ok = false;
        }

        if (ok)
        {
            int[] w = { 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 };
            int sum = 0;
            for (int i = 0; i < 10; i++)
                sum += w[i] * (cpr[i] - '0');

            if (sum % 11 != 0) ok = false;
        }

        return ok;
    }
}
