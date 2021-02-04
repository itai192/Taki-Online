<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CSS Control test.aspx.cs" Inherits="UI.CSS_Control_test" %>

<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>
<%@ Register Src="~/Custom Controls/Card.ascx" TagPrefix="uc1" TagName="Card" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <uc1:LoadingBar runat="server" outOf="3" progress="1" inColor="Red" outColor="Green" />
    <asp:Panel ID="Panel1" runat="server"  CssClass="Panel">
    <uc1:Card runat="server" ID="Pile" />
    <uc1:Card runat="server" ID="Deck" />
    <uc1:Card runat="server" ID="Card"  />
    <script type="text/javascript" src="CardAnimations.js"></script></asp:Panel>
</asp:Content>


