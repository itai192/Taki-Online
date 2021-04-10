<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Taki_AssosiationUI.Account" %>

<%@ Register Src="~/UserDetailsTable.ascx" TagPrefix="uc1" TagName="UserDetailsTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UserDetailsTable runat="server" id="UserDetailsTable" />
</asp:Content>
