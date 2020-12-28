<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>
<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:DataList ID="FriendRequests" runat="server" OnItemCommand="FriendRequests_ItemCommand">
        <HeaderTemplate>
            Friend Requests
        </HeaderTemplate>
        <ItemTemplate>
            <asp:Label ID="FrUsername" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
            <asp:Button ID="Accept" runat="server" Text="Accept" CommandName="Accept" />
            <asp:Button ID="Decline" runat="server" Text="Decline" CommandName="Decline" />
        </ItemTemplate>
    </asp:DataList>
    Friends
    <asp:BulletedList ID="Friends" runat="server"></asp:BulletedList>
    <asp:Image runat="server" ID="ProfilePic" Height="20em" Width="20em"></asp:Image>
        <asp:Table ID="Details" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Username:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="UsernameLbl" runat="server" Text=""></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Level:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="Levellbl" runat="server" Text=""></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">XP:</asp:TableCell>
                <asp:TableCell runat="server"><uc1:LoadingBar runat="server" ID="XpBar" /></asp:TableCell>
            </asp:TableRow>
            
        </asp:Table>

    
    multyview

    <asp:MultiView ID="Stats" runat="server"></asp:MultiView>
    
</asp:Content>
