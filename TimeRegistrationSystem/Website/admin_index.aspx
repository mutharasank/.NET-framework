<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  <div style="background-color: aliceblue; height: 858px; width:823px; margin:10px auto 0 auto; text-align:center;">
    
    
        <asp:Label ID="userInfo" runat="server" Font-Italic="True" ForeColor="#0000CC"></asp:Label>
       <br />
       <br />
       <br />

    <br /> <asp:Label ID="Label2" runat="server" Text="Show Assigned Registrations : "></asp:Label>&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" />
&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Show Registered Time Registrations:"></asp:Label>
        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" />
        <br />
      <br />
      <asp:Label ID="Label1" runat="server" Text="Please select an employee :" BorderStyle="Double" Font-Italic="True" Font-Size="Larger" ForeColor="#0000CC"></asp:Label>
      <br/>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" ForeColor="#003366" Height="25px" style="margin-left: 0px" Width="277px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
      <br />
      <br />
      <br />
      <asp:GridView ID="GridView1" runat="server" style="margin-left: 50px" Width="720px"></asp:GridView>
                </div>
    </form>
</body>
    

</html>
