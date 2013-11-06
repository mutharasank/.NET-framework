<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="CPR"></asp:Label>
            &nbsp;
            <br />
            <asp:TextBox ID="tBoxCPR" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*1: Error in CPR"
                OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True" ControlToValidate="tBoxCPR"
                ValidationGroup="vg1" EnableClientScript="False" Display="Dynamic">*1</asp:CustomValidator>
            &nbsp;
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*2: CPR not correct"
                ControlToCompare="TextBox4" ValidationGroup="vg1" ControlToValidate="tBoxCPR" Display="Dynamic">*2</asp:CompareValidator>
            <br />
            <br />
            Add<br />
            <br />
            1. number
            <br />
            <asp:TextBox ID="TextBox1" runat="server" CausesValidation="True"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                ErrorMessage="*3: Number required" ValidationGroup="vg1">*3</asp:RequiredFieldValidator>
            <br />
            <br />
            2. number&nbsp; (0..1000)<br />
            <asp:TextBox ID="TextBox2" runat="server" CausesValidation="True"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBox2"
                ErrorMessage="*4: Only integer in 0..1000 is valid" MaximumValue="1000" MinimumValue="0"
                Type="Integer" ValidationGroup="vg1" Display="Dynamic">*4</asp:RangeValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vg1"
                ErrorMessage="*5: Number required" ControlToValidate="TextBox2" Display="Dynamic">*5</asp:RequiredFieldValidator>
            <br />
            <br />
            Sum<br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="CPR again"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="TextBox4"
                ValidationGroup="vg1" ErrorMessage="*6: Error in CPR"
                OnServerValidate="CustomValidator2_ServerValidate" EnableClientScript="False">*6</asp:CustomValidator>
            <br />
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vg1"
                Height="52px" />
            &nbsp;<br />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button"
                CausesValidation="True" /><br />
            <br />
            &nbsp;
        </div>
    </form>
</body>
</html>
