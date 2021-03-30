<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CreateGame.aspx.cs" Inherits="UI.CreateGame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="SearchPanel" runat="server">
        <asp:TextBox ID="GameText" runat="server"></asp:TextBox><asp:Button ID="SearchBtn" runat="server" Text="Search Game!" />
    </asp:Panel>

</asp:Content>
