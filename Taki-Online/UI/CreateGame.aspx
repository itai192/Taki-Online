<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CreateGame.aspx.cs" Inherits="UI.CreateGame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="GameNameTxt" runat="server"></asp:TextBox><asp:Button ID="CreateGameBtn" runat="server" Text="Create game" Enabled="True" OnClick="CreateGameBtn_Click" /><asp:Label ID="ErorLbl" runat="server" ></asp:Label>
</asp:Content>
