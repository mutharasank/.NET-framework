<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Here you can see all exams for which you are allowed to participate." Font-Italic="True" Font-Size="15pt"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    Select exam :
    <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" style="margin-left: 77px" Width="146px">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-left: 61px" Text="Register Me" Width="166px" />
        <br />
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt"></asp:Label>
</asp:Content>


