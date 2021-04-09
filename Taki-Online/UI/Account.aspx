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
            <asp:Button ID="Accept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Container.DataItem.ToString() %>' />
            <asp:Button ID="Decline" runat="server" Text="Decline" CommandName="Decline" CommandArgument='<%# Container.DataItem.ToString() %>' />
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
                    <asp:TextBox ID="ChngUsernameTxt" runat="server" ></asp:TextBox><asp:Button ID="ChngUsernameBtn" runat="server" Text="Change!" ValidationGroup="ChangeUserName" OnClick="ChngUsername_Click"/><asp:RegularExpressionValidator ID="NameValidator" runat="server" Display="Dynamic" ValidationGroup="ChangeUserName" ValidationExpression="\w" ErrorMessage="Username must be a word (english alphabet and numbers)" ControlToValidate="ChngUsernameTxt"></asp:RegularExpressionValidator><asp:CustomValidator OnServerValidate="UsernameValidator_ServerValidate" ID="UserExists" runat="server" ErrorMessage="A user With that name already exists" ControlToValidate="ChngUsernameTxt" ValidationGroup="ChangeUserName" Display="Dynamic"></asp:CustomValidator></asp:TableCell>
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
    <asp:GridView ID="UserSearch" runat="server" GridLines="None" PageSize="4" ItemType="BLL.User" AutoGenerateColumns="False" ShowHeader="False" OnRowDataBound="GridSearchGridView_RowDataBound" EmptyDataText="No users were found" AllowPaging="True" OnPageIndexChanging="UserSearch_PageIndexChanging" OnRowCommand="UserSearch_RowCommand" ViewStateMode="Enabled" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <uc1:UserCard runat="server" ID="UserCard" user=<%#Item%>/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="AddFriendBtn" runat="server" Text="Add Friend" CommandName="AddFriend" CommandArgument=<%#Item.username%> />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:RequiredFieldValidator ID="RequiredSearchString" runat="server" ErrorMessage="You can't search for nothing" ValidationGroup="SearchUsers" ControlToValidate="FindFriendTxtBox" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="friendUsernameValidator" runat="server" Display="Dynamic" ValidationGroup="ChangeUserName" ValidationExpression="\w" ErrorMessage="Friend sername must be a word (english alphabet and numbers)" ControlToValidate="FindFriendTxtBox"></asp:RegularExpressionValidator>
    <asp:DataList ID="GameInvites" runat="server" OnItemCommand="GameInvites_ItemCommand">
        <HeaderTemplate>
            <asp:Label ID="Label1" runat="server" Text="Invites to games"></asp:Label>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:Label ID="PlayerIntro" runat="server" Text="The Player"></asp:Label>
            <asp:Label ID="Playerlbl" runat="server" Text="<%# ((BLL.GameInvite)Container.DataItem).sender %>"></asp:Label>
            <asp:Label ID="Invitelbl" runat="server" Text="invites you to join the game"></asp:Label>
            <asp:Label ID="GameNameLbl" runat="server" Text="<%# ((BLL.GameInvite)Container.DataItem).gameRoom.gameName %>"></asp:Label>
            <asp:Button ID="JoinBtn" runat="server" Text="Join Game" CommandName="Join" CommandArgument="<%# ((BLL.GameInvite)Container.DataItem).gameID %>" />
        </ItemTemplate>
    </asp:DataList>
    <asp:Panel ID="ChartsPanel" runat="server">
        
    <asp:Chart ID="Games" runat="server" Palette="Berry">
        <Series>
            <asp:Series Name="Games" ChartType="Pie" YValuesPerPoint="4" Legend="Legend1" IsValueShownAsLabel="True" Label="#PERCENT\n#VALY">
                <Points>
                    <asp:DataPoint LegendText="Games Won" />
                    <asp:DataPoint LegendText="Games Lost" />
                </Points>
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="Games" Area3DStyle-Enable3D="True" Area3DStyle-LightStyle="Simplistic" AlignmentOrientation="Vertical">
<Area3DStyle Enable3D="True"></Area3DStyle>
            </asp:ChartArea>
        </ChartAreas>
        <Legends>
            <asp:Legend Name="Legend1">
            </asp:Legend>
        </Legends>
    </asp:Chart>

    </asp:Panel>
    
    
</asp:Content>
