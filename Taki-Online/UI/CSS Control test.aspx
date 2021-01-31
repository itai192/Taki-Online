<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CSS Control test.aspx.cs" Inherits="UI.CSS_Control_test" %>

<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LoadingBar runat="server" outOf="3" progress="1" inColor="Red" outColor="Green" />
    <asp:Image ID="Image1" runat="server" CssClass="red" ImageUrl="~/Cards/+2.svg" Height="50" Width="50" />
</asp:Content>
