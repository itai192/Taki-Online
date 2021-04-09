<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CSS Control test.aspx.cs" Inherits="UI.CSS_Control_test" %>

<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>
<%@ Register Src="~/Custom Controls/Card.ascx" TagPrefix="uc1" TagName="Card" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server"  CssClass="Panel"></asp:Panel>
    <uc1:LoadingBar runat="server" ID="LoadingBar" inColor="Red" outColor="Yellow" outOf="50" progress="30"/>
    
</asp:Content>



