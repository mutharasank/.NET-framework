<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="register_time.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Please select project and register hours :" Font-Italic="True" Font-Size="15pt"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    Projects you are assigned to :<asp:DropDownList ID="DropDownList1" runat="server" Height="23px" style="margin-left: 22px" Width="150px">
    </asp:DropDownList>
     <asp:Label ID="Label4" runat="server" Text="Hours" ></asp:Label>
    :<asp:TextBox ID="txbHours" runat="server" style="margin-left: 5px" Width="52px"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-left: 61px" Text="Register Me" Width="166px" />
           <br />
     <br />
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt"></asp:Label>
   
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
       <br /> 
    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
     <br />
       <br />
</asp:Content>


