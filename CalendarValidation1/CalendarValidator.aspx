<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarValidator.aspx.cs" Inherits="CalendarValidator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Arrival Date (from today until 90 days from now)"></asp:Label>
    
    </div>
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Date must be in the interval of 90 days" Display="Dynamic"></asp:CustomValidator>
        </p>
            Number of persons<asp:TextBox ID="TextBox1" runat="server" CausesValidation="True"></asp:TextBox>
        <p>

           
        <asp:Label ID="Label2" runat="server" Text="Adults among these"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" CausesValidation="True"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Reserve" />
   
            
            <p>

           

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBox1" runat="server" ErrorMessage="Number of persons is required field." Display="Dynamic"></asp:RequiredFieldValidator>

           

        </p>
        <p>
   
            
            <asp:RangeValidator ID="RangeValidator1" runat="server"   ControlToValidate="TextBox1"
                ErrorMessage=" Only integer in 1..12 is valid persons number" MaximumValue="12" MinimumValue="1"
                Type="Integer" Display="Dynamic"></asp:RangeValidator>

           
         

        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextBox2" runat="server" ErrorMessage="Number of adults is required field." Display="Dynamic"></asp:RequiredFieldValidator>

           

        </p>
        <p>
   
            
            <asp:RangeValidator ID="RangeValidator3" runat="server"   ControlToValidate="TextBox2"
                ErrorMessage=" Only integer in 1..12 is valid persons number" MaximumValue="12" MinimumValue="1"
                Type="Integer" Display="Dynamic"></asp:RangeValidator>

           

        </p>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Number of adults must be less than or equal to the number of persons."
                Operator="LessThanEqual" ControlToCompare="TextBox1"  ControlToValidate="TextBox2" Display="Dynamic"></asp:CompareValidator>
    </form>
</body>
</html>
