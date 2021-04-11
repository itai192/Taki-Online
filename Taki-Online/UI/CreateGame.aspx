<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CreateGame.aspx.cs" Inherits="UI.CreateGame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="CreateGameBtn">
    Enter game name:<asp:TextBox ID="GameNameTxt" runat="server"></asp:TextBox><asp:Button ID="CreateGameBtn" runat="server" Text="Create game" Enabled="True" OnClick="CreateGameBtn_Click" />
    <asp:RegularExpressionValidator ID="NameValidator" runat="server" ErrorMessage="Game name must be a word or many words" ControlToValidate="GameNameTxt" Display="Dynamic" ValidationExpression="[\w\s]+"></asp:RegularExpressionValidator>
    <asp:Label ID="ErorLbl" runat="server" ViewStateMode="Disabled"></asp:Label>
        </asp:Panel>
</asp:Content>
