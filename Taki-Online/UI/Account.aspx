<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    list<asp:ListView ID="F1" runat="server" OnSelectedIndexChanged="F1_SelectedIndexChanged"><ItemTemplate></ItemTemplate></asp:ListView>
    <br />
    grid
    <asp:GridView ID="F2" runat="server"></asp:GridView>
</asp:Content>
