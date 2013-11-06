<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="exams.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="ContentPlaceHolder2">
    
     <asp:Button ID="Button1" runat="server" Text="All Exams" style="margin-left: 0px" Width="240px" OnClick="Button1_Click" />
     <asp:Button ID="Button2" runat="server" Text="Exams with more attempts" style="margin-left: 91px" Width="240px" OnClick="Button2_Click" />
     <asp:GridView ID="GridView1" runat="server" style="margin-top: 20px" Width="821px">
     </asp:GridView>
     <asp:GridView ID="GridView2" runat="server" style="margin-top: 20px" Width="821px">
     </asp:GridView>
     </asp:Content>


