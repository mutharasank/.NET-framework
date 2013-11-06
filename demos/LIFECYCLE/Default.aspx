<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListBox ID="ListBox1" runat="server" OnInit="ListBox1_Init" OnLoad="ListBox1_Load"
                OnPreRender="ListBox1_PreRender" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged">
                <asp:ListItem>Aarhus</asp:ListItem>
                <asp:ListItem>Horsens</asp:ListItem>
                <asp:ListItem>Randers</asp:ListItem>
            </asp:ListBox>
            &nbsp;
        <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" ToolTip="Indtast alder"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox1"
                ErrorMessage="Fejl i alder " MaximumValue="100" MinimumValue="0" Type="Integer">Tal mellem 0 og 100</asp:RangeValidator><br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Tal forskellige"
                ControlToCompare="TextBox1" ControlToValidate="TextBox2">Ikke samme tal som ovenfor</asp:CompareValidator>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnInit="Button1_Init"
                OnLoad="Button1_Load" OnPreRender="Button1_PreRender" Text="Button" />
            <br />
            <br />
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        </div>
    </form>
</body>
</html>
