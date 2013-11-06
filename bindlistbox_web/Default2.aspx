<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="Label1" runat="server" Text="Employees"></asp:Label>
    &nbsp;from country:
    <asp:DropDownList ID="ddListCountry" runat="server" AutoPostBack="True">
        <asp:ListItem>UK</asp:ListItem>
        <asp:ListItem>USA</asp:ListItem>
    </asp:DropDownList>
    &nbsp;(has AutoPostBack=true)<br />
    <br />
    
        <br />
        <asp:ListBox ID="LBoxEmployees" runat="server" Height="90px"></asp:ListBox>
        &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
            AutoPostBack="True">
            <asp:ListItem Selected="True" Value="full">Show full name</asp:ListItem>
            <asp:ListItem Value="last">Show last name</asp:ListItem>
            <asp:ListItem Value="first">Show first name</asp:ListItem>
        </asp:RadioButtonList>
        (has AutoPostBack=true)<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Back</asp:HyperLink>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
