<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="Label1" runat="server" Text="Employees"></asp:Label>
    &nbsp;from country:
    <asp:DropDownList ID="ddListCountry" runat="server">
        <asp:ListItem>UK</asp:ListItem>
        <asp:ListItem>USA</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Find" />
    <br />
    <br />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default2.aspx">Show ListBox with DataBinding</asp:HyperLink>
    </form>
</body>
</html>
