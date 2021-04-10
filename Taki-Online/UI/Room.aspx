<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="UI.Room" %>

<%@ Register Src="~/Custom Controls/UserCard.ascx" TagPrefix="uc1" TagName="UserCard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DataList ID="Players" runat="server">
                <ItemTemplate>
                    <uc1:UserCard ID="UserCard1" runat="server" user='<%#Container.DataItem%>' />
                </ItemTemplate>
            </asp:DataList>
            Friends To Invite:
            <asp:DataList ID="FriendsToInvite" runat="server" OnItemDataBound="FriendsToInvite_ItemDataBound" OnItemCommand="FriendsToInvite_ItemCommand">
                <ItemTemplate>
                    <uc1:UserCard ID="UserCard1" runat="server" user='<%# Container.DataItem %>' />
                    <asp:Button ID="InviteBtn" runat="server" Text="Invite To Game" CommandName="Invite" CommandArgument='<%# ((BLL.User)Container.DataItem).username %>' />
                </ItemTemplate>
            </asp:DataList>
            <asp:Panel ID="HostPanel" runat="server">
                <asp:Button ID="StartBtn" runat="server" Text="Start Game!" OnClick="StartBtn_Click" />
            </asp:Panel>
            <asp:Timer ID="timer" runat="server" OnTick="Page_Load" Interval="3000"></asp:Timer>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timer" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:Label ID="errorLbl" runat="server" EnableViewState="False"></asp:Label>
</asp:Content>

