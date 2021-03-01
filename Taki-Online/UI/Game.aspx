<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="UI.Game" %>

<%@ Register Src="~/Custom Controls/Card.ascx" TagPrefix="uc1" TagName="Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Panel">
    <uc1:Card runat="server" ID="Pile" />
    <uc1:Card runat="server" ID="Deck" />
    <uc1:Card runat="server" ID="Card" IsButton="true" />
    <uc1:Card runat="server" ID="Card2" IsButton="True" />
    </div>
    <asp:Label ID="Statuslbl" runat="server" Text=""  ></asp:Label>
    <script type="text/javascript" src="CardAnimations.js"></script>
</asp:Content>
