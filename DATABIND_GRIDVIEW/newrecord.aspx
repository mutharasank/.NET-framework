<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newrecord.aspx.cs" Inherits="newrecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="New category"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="CategoryName"></asp:Label>
        <br />
        <asp:TextBox ID="TBoxName" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
        <br />
        <asp:TextBox ID="TBoxDescr" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Insert" 
            Width="65px" />
&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" PostBackUrl="~/Default.aspx" 
            Text="Cancel" Width="65px" />
        <br />
        <br />
        <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red" 
            Text="Error creating new category"></asp:Label>
    
    </div>
    </form>
</body>
</html>
