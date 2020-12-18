<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CSS Control test.aspx.cs" Inherits="UI.CSS_Control_test" %>
<%@ Register src="Custom%20Controls/Loading%20Bar.ascx" tagname="Loading" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:Loading ID="Loading1" runat="server" OutColor="red"/>
    
</asp:Content>
