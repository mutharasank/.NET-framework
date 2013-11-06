<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
            Text="Categories"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" 
            DataKeyNames="CategoryID,CategoryName,Description" 
            onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing">
            <Columns>
                <asp:CommandField EditText="Edit " ShowEditButton="True" />
                <asp:CommandField DeleteText="Delete " ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="BtnAdd" runat="server" PostBackUrl="~/newrecord.aspx" 
            Text="Add new" />
    
    </div>
    </form>
</body>
</html>
