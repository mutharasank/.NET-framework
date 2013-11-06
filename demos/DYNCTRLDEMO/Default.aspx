<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
      <br />
      <br />
      <asp:Table ID="Table1" runat="server" GridLines="Both">
        <asp:TableRow runat="server">
          <asp:TableCell runat="server">Firstname</asp:TableCell>
          <asp:TableCell runat="server">Lastname</asp:TableCell>
        </asp:TableRow>
      </asp:Table>
      <br />
    </div>
    </form>
</body>
</html>
