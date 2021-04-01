<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="UI.Room" %>

<%@ Register Src="~/Custom Controls/UserCard.ascx" TagPrefix="uc1" TagName="UserCard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="Players" runat="server">
        <ItemTemplate>
            <uc1:UserCard ID="UserCard1" runat="server" user='<%#Container.DataItem%>' />
        </ItemTemplate>
    </asp:DataList>
    <asp:DataList ID="FriendsToInvite" runat="server" OnItemDataBound="FriendsToInvite_ItemDataBound">
        <ItemTemplate>
            <uc1:UserCard ID="UserCard1" runat="server" user='<%#Container.DataItem%>' />
            <asp:Button ID="InviteBtn" runat="server" Text="Invite To Game" CommandName="Invite" CommandArgument='<%#Container.DataItem%>' />
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
