<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>
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
    Username:<asp:Label ID="Usernamelbl" runat="server" Text=""></asp:Label><br />
    Level:<asp:Label ID="Levellbl" runat="server" Text=""></asp:Label>
    XP:

    <br />
    image:<asp:Image runat="server"></asp:Image>
    multyview

    <asp:MultiView ID="Stats" runat="server"></asp:MultiView>
    
</asp:Content>
