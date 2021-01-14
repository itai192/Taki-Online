<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
    <br />
    <asp:Label ID="Friendslbl" runat="server" Text="Friends"></asp:Label>
    <br/>
    <asp:BulletedList ID="Friends" runat="server"></asp:BulletedList>
    <br />
    <uc1:profilepicture runat="server" id="ProfilePicture" height="15 em" width="15 em" />
    <br/>
    <uc1:ProfilePictureUpload runat="server" ID="ProfilePictureUpload" ViewStateMode="Disabled" />
     
        <asp:Button ID="UploadPhotoBtn" runat="server" Text="Upload Photo" OnClick="UploadPicBtn_Click"  />
        <asp:Table ID="Details" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Username:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="UsernameLbl" runat="server" Text=""></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Change Username:</asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="ChngUsernameTxt" runat="server" ></asp:TextBox><asp:Button ID="ChngUsernameBtn" runat="server" Text="Change!" ValidationGroup="ChangeUserName" OnClick="ChngUsername_Click"/><asp:CustomValidator OnServerValidate="UsernameValidator_ServerValidate" ID="UserExists" runat="server" ErrorMessage="A user With that name already exists" ControlToValidate="ChngUsernameTxt" ValidationGroup="ChangeUserName" Display="Dynamic"></asp:CustomValidator><asp:RequiredFieldValidator  ValidationGroup="ChangeUserName" ID="RequiredUsername" runat="server" ErrorMessage="Please insert a username" ControlToValidate="ChngUsernameTxt" Display="Dynamic"></asp:RequiredFieldValidator></asp:TableCell>
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
                <asp:TableCell runat="server"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="AddFriendBtn" runat="server" Text="Button" OnClick="ChngUsername_Click" /><asp:Label ID="FriendErrorLbl" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    
    <asp:Panel ID="ChartsPanel" runat="server">

    
    <%--<asp:Chart runat="server" ID="ctl00">
        <Series>
            <asp:Series Name="Series1" ChartType="Line"></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>--%>

    </asp:Panel>
    
    
</asp:Content>
