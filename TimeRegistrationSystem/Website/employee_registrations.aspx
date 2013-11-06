<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="employee_registrations.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="ContentPlaceHolder2">
    
    Show assigned work for project :&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" Height="16px" style="margin-left: 0px" Width="135px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
     </asp:DropDownList>
&nbsp;&nbsp;&nbsp; Show registered work on project :&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="DropDownList2" runat="server"  Height="16px" Width="135px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
     </asp:DropDownList>
&nbsp;<asp:GridView ID="GridView1" runat="server" style="margin-top: 20px" Width="821px">
     </asp:GridView>
     <asp:GridView ID="GridView2" runat="server" style="margin-top: 20px" Width="821px">
     </asp:GridView>
     </asp:Content>


