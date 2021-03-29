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
            
        </asp:Table>
    
   <div>
    Add Friends<br />
            <asp:TextBox ID="FindFriendTxtBox" runat="server" ControlToValidate="FindFriendTxtBox" ValidationGroup="SearchUsers"></asp:TextBox><br />
            <asp:Button ID="SearchFriendsBtn" runat="server" Text="Search!" OnClick="SearchFriendsBtn_Click" ValidationGroup="SearchUsers" />
    </div>
    <asp:GridView ID="UserSearch" runat="server" GridLines="None" PageSize="4" ItemType="BLL.User" AutoGenerateColumns="False" ShowHeader="False" OnRowDataBound="GridSearchGridView_RowDataBound" EmptyDataText="No users were found" AllowPaging="True" OnPageIndexChanging="UserSearch_PageIndexChanging" ViewStateMode="Enabled" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <uc1:UserCard runat="server" ID="UserCard" user='<%#Item%>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="AddFriendBtn" runat="server" Text="Add Friend" CommandName="AddFriend" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:RequiredFieldValidator ID="RequiredSearchString" runat="server" ErrorMessage="You can't search for nothing" ValidationGroup="SearchUsers" ControlToValidate="FindFriendTxtBox"></asp:RequiredFieldValidator>
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
