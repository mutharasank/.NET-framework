<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deleterecord.aspx.cs" Inherits="deleterecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="Delete category"></asp:Label>
        <br />
        <br />
        Do you want to delete the category<br />
        <br />
        <asp:Label ID="LblRecord" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Yes" 
            Width="65px" />
&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" PostBackUrl="~/Default.aspx" Text="No" 
            Width="65px" />
        <br />
        <br />
        <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red" 
            Text="Error deleteting category"></asp:Label>
    
    </div>
    </form>
</body>
</html>
