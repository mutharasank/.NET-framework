<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: aliceblue; height: 358px; width:823px; margin:100px auto 0 auto; text-align:center;">
    
        <br />
        <asp:Label ID="Label1" runat="server" Text="Welcome to the Students Exam System!" Font-Size="25pt" BorderStyle="Double" Font-Bold="True" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Please Log In to your account." Font-Size="15pt" BackColor="#CCFFFF" Font-Italic="True"></asp:Label>
        <br />
        <br />
        (Use Cpr: 2710912605 and pass : 12345 for test purposes)<br />
        <br />
        <div style="text-align: center;">
 
   <div style="width: 400px; margin-left: auto; margin-right:auto;" aria-atomic="False" aria-dropeffect="copy">
 
      <asp:login ID="AppLogin" runat="server"
                 DestinationPageUrl="~/LoginPage.aspx"
                 TitleText="Please enter your credentials to access the System." OnAuthenticate="AppLogin_Authenticate" UserNameLabelText="CPR" UserNameRequiredErrorMessage="CPR is required." ValidateRequestMode="Enabled">
      </asp:login>
 
   </div>
 
</div>
        </div>
    </form>
</body>
</html>
