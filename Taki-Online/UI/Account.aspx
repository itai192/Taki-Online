<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>
<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>
<%@ Register Src="~/Custom Controls/ProfilePicture.ascx" TagPrefix="uc1" TagName="ProfilePicture" %>


<%@ Register Src="~/Custom Controls/ProfilePictureUpload.ascx" TagPrefix="uc1" TagName="ProfilePictureUpload" %>


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
    <uc1:profilepicture runat="server" id="ProfilePicture" />
    <br/>
    <uc1:ProfilePictureUpload runat="server" ID="ProfilePictureUpload" />
        <asp:Button ID="UploadPhotoBtn" runat="server" Text="Upload Photo" OnClick="UploadPicBtn_Click" />
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
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Add Friends:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="AddFriendBtn" runat="server" Text="Button" /><asp:Label ID="FriendErrorLbl" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    
    
    
</asp:Content>
