<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UI.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--grid
    <asp:GridView ID="FriendRequests" runat="server" AutoGenerateColumns="False" 
        Caption="Friend requests" AllowPaging="True" OnSelectedIndexChanged="FriendRequests_SelectedIndexChanged">
        <Columns>
            <asp:ButtonField Text="Button" ButtonType="Button" />
            <asp:ButtonField Text="Button" ButtonType="Button" />
            <asp:TemplateField HeaderText="Username"></asp:TemplateField>
        </Columns>
    </asp:GridView>--%>
    <asp:DataList ID="FriendRequests" runat="server" OnItemCommand="FriendRequests_ItemCommand">
        <HeaderTemplate>
            Friend Requests
        </HeaderTemplate>
        <ItemTemplate>
            <asp:Label ID="FrUsername" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
            <asp:Button ID="Accept" runat="server" Text="Button" />
            <asp:Button ID="Decline" runat="server" Text="Button" />
        </ItemTemplate>
    </asp:DataList>
    Friends
    <asp:BulletedList ID="Friends" runat="server"></asp:BulletedList>
    Username:<asp:Label ID="Usernamelbl" runat="server" Text=""></asp:Label><br />
    Level:<asp:Label ID="Levellbl" runat="server" Text=""></asp:Label>
    XP:

    <br />
    multyview

    <asp:MultiView ID="Stats" runat="server"></asp:MultiView>
    
</asp:Content>
