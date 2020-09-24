<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Sign in.aspx.cs" Inherits="UI.Sign_in" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Label">Email</asp:Label><asp:TextBox ID="username" runat="server"></asp:TextBox> <br />
        <asp:Label ID="Label2" runat="server" Text="Label">Password</asp:Label><asp:TextBox ID="password" runat="server"></asp:TextBox>
    </asp:Panel>
</asp:Content>
