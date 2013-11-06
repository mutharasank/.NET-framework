<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 737px; margin: 0 10%;">
    
        <br />
        Employee Number :<asp:TextBox ID="txbNr" runat="server" style="margin-left: 23px" Width="152px"></asp:TextBox>
        
        <br />
    
        <br />
        Password :&nbsp; &nbsp;<asp:TextBox ID="txbPass" runat="server" style="margin-left: 65px" Width="151px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <br />
        Select Project :<br />
        <asp:DropDownList ID="cbxProject" runat="server" Height="18px" style="margin-left: 0px; margin-bottom: 0px" Width="254px">
        </asp:DropDownList>
        
        <br />
&nbsp;<p>
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" Width="169px" />
        </p>
        </div>
    </form>
</body>
</html>
