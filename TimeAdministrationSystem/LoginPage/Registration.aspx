<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 538px; margin-left:auto; margin-right:auto; height: 384px;">
    
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">View your work schedule</asp:LinkButton>
        <br />
        <br />
        <br />
        <asp:Button ID="btnStart" runat="server" Text="Register Start Time" OnClick="Button2_Click" Width="241px" style="margin-left: 127px" />
    
        <br />
        <br />
        <asp:Button ID="btnEnd" runat="server" style="margin-left: 126px" Text="Register End Time" Width="244px" OnClick="btnEnd_Click" />
    
        <br />
    
        <br />
        <asp:GridView ID="dataGrid1" runat="server" Height="188px" Width="537px">
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
