using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page
{
    public void debugInfo(string s)
    {
        Application.Lock();
        if (Application["n"] == null) Application["n"] = 0;

        int n = (int)Application["n"];
        n++;
        Application["n"] = n;

        Debug.WriteLine("" + n + ": " + s);
        Application.UnLock();
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        debugInfo("Page_Init");

        this.InitComplete += new EventHandler(_Default_InitComplete);
        this.LoadComplete += new EventHandler(_Default_LoadComplete);
        this.PreRender += new EventHandler(_Default_PreRender);
        this.SaveStateComplete += new EventHandler(_Default_SaveStateComplete);
        this.Unload += new EventHandler(_Default_Unload);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Load += new EventHandler(_Default_Load);
        //this.Init += new EventHandler(_Default_Init);

        debugInfo("Page_Load");
    }

    void _Default_Unload(object sender, EventArgs e)
    {
        debugInfo("Page_Unload ---------------------");
    }

    void _Default_SaveStateComplete(object sender, EventArgs e)
    {
        debugInfo("Page_SaveStateCompleted");
    }

    void _Default_PreRender(object sender, EventArgs e)
    {
        debugInfo("Page_PreRender");
    }

    void _Default_LoadComplete(object sender, EventArgs e)
    {
        debugInfo("Page_LoadCompleted");
    }

    void _Default_InitComplete(object sender, EventArgs e)
    {
        debugInfo("Page_InitCompleted");
    }

    protected void ListBox1_Init(object sender, EventArgs e)
    {
        debugInfo("ListBox1_Ínit");
    }
    protected void ListBox1_Load(object sender, EventArgs e)
    {
        debugInfo("ListBox1_Load");
    }
    protected void ListBox1_PreRender(object sender, EventArgs e)
    {
        debugInfo("ListBox1_PreRender");
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        debugInfo("==> ListBox1_SelectedIndexChanged: selectedIndex=" + ListBox1.SelectedIndex);
    }
    protected void Button1_Init(object sender, EventArgs e)
    {
        debugInfo("Button_Init");
    }
    protected void Button1_Load(object sender, EventArgs e)
    {
        debugInfo("Button_Load");
    }
    protected void Button1_PreRender(object sender, EventArgs e)
    {
        debugInfo("Button_PreRender");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        debugInfo("==> Button_Click");

    }
}
