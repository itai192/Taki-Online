<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="CSS Control test.aspx.cs" Inherits="UI.CSS_Control_test" %>

<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LoadingBar runat="server" outOf="3" progress="1" inColor="Red" outColor="Green" />
  <object type="image/svg+xml" data="Cards/Plus2.svg" class="Red" width="500" height="500">
  </object>
    <a href="Cards/Plus2.svg">Cards/Plus2.svg</a>
</asp:Content>
