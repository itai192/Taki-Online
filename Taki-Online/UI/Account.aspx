<%@ Page Title="" Language="C#" EnableEventValidation="True" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/Custom Controls/Loading Bar.ascx" TagPrefix="uc1" TagName="LoadingBar" %>
<%@ Register Src="~/Custom Controls/ProfilePicture.ascx" TagPrefix="uc1" TagName="ProfilePicture" %>
<%@ Register Src="~/Custom Controls/ProfilePictureUpload.ascx" TagPrefix="uc1" TagName="ProfilePictureUpload" %>
<%@ Register Src="~/Custom Controls/UserCard.ascx" TagPrefix="uc1" TagName="UserCard" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="FriendRequests" runat="server" OnItemCommand="FriendRequests_ItemCommand">
        <HeaderTemplate>
            Friend Requests
        </HeaderTemplate>
        <ItemTemplate>
            <uc1:UserCard runat="server" id="UserCard1" user=<%# Container.DataItem %> />
            <asp:Button ID="Accept" runat="server" Text="Accept" CommandName="Accept" CommandArgument="<%# Container.DataItem %>" />
            <asp:Button ID="Decline" runat="server" Text="Decline" CommandName="Decline" CommandArgument="<%# Container.DataItem %>" />
        </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="FriendRequestErrorLbl" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:DataList ID="Friends" runat="server">
        <HeaderTemplate>
            Friends
        </HeaderTemplate>
        <ItemTemplate>
            <uc1:UserCard runat="server" id="UserCard1" user=<%# Container.DataItem %> />
        </ItemTemplate>
    </asp:DataList>
    <br />
    <uc1:profilepicture runat="server" id="ProfilePicture" height="15 em" width="15 em" />
    <br/>
    
    <asp:ListView ID="ListView1" runat="server">
        <LayoutTemplate>
            <asp:Table ID="Table1" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableCell>Users</asp:TableCell>
                </asp:TableHeaderRow>
                <asp:TableRow ID="itemPlaceholder" runat="server" />
            </asp:Table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
        </ItemTemplate>
    </asp:ListView>
    <uc1:ProfilePictureUpload runat="server" ID="ProfilePictureUpload" ViewStateMode="Disabled" />
     
        <asp:Button ID="UploadPhotoBtn" runat="server" Text="Upload Photo" OnClick="UploadPicBtn_Click"  />
        <asp:Table ID="Details" runat="server">
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
                <asp:TableCell runat="server"><uc1:LoadingBar runat="server" ID="XpBar" inColor="Aqua" outColor="Red" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">ELO this season:</asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="EloLbl" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Add Friends:</asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="AddFriendTxt" runat="server"></asp:TextBox>
                <asp:Button ID="AddFriendBtn" runat="server" Text="Send Friend Request" OnClick="AddFriend" ValidationGroup="AddFriend" /><asp:Label ID="FriendAddMsg" runat="server" ViewStateMode="Disabled"></asp:Label><asp:CustomValidator ID="UserNotExist" runat="server" ValidationGroup="AddFriend" ErrorMessage="A user with this name doesn't exist" ControlToValidate="AddFriendTxt" OnServerValidate="FriendValidator_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                <asp:CustomValidator ID="FriendValidator" runat="server" ErrorMessage=""></asp:CustomValidator></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    <asp:DataList ID="DataList1" runat="server">
        <HeaderTemplate>
            Add Friends<br />
            <asp:TextBox ID="FindFriendTxtBox" runat="server"></asp:TextBox><br />
            <asp:Button ID="SearchFriendsBtn" runat="server" Text="Search!" />
        </HeaderTemplate>
        <ItemTemplate>
            <uc1:UserCard runat="server" id="UserCard1" user=<%# Container.DataItem %> />
        </ItemTemplate>
    </asp:DataList>
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
