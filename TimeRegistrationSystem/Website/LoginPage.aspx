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
        <asp:Label ID="Label1" runat="server" Text="Time Registration System" Font-Size="25pt" BorderStyle="Double" Font-Bold="True" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Please Log In to your account." Font-Size="15pt" BackColor="#CCFFFF" Font-Italic="True"></asp:Label>
        <br />
         <br />
        <br />
         <asp:Button ID="btnAdministrator" runat="server" Text="Administrator" Width="127px" OnClick="btnAdministrator_Click"/>
        <asp:Button ID="btnEmployee" runat="server" Text="Employee" style="margin-left: 63px" Width="147px" OnClick="btnEmployee_Click" />
        <br />
        <div style="text-align: center;">
 
   <div style="width: 333px; margin-left: auto; margin-right:auto; margin-top: 36px;" aria-atomic="False" aria-dropeffect="copy">
 
      <asp:login ID="AdminLogin" runat="server"
                 DestinationPageUrl="~/LoginPage.aspx"
                 TitleText="Please enter your ADMINISTRATOR credentials" OnAuthenticate="AdminLogin_Authenticate" UserNameLabelText="Name:" UserNameRequiredErrorMessage="Name is required." ValidateRequestMode="Enabled" Height="128px" style="margin-top: 22px">
      </asp:login>
               <asp:login ID="EmployeeLogin" runat="server"
                 DestinationPageUrl="~/LoginPage.aspx"
                 TitleText="Please enter your EMPLOYEE credentials" OnAuthenticate="EmployeeLogin_Authenticate" UserNameLabelText="Name:" UserNameRequiredErrorMessage="Name is required." ValidateRequestMode="Enabled" Width="308px">
      </asp:login>
 
   </div>
 
</div>
        
       </div>
    </form>
</body>
</html>
