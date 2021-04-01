<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="UI.Play" %>

<%@ Register Src="~/Custom Controls/UserCard.ascx" TagPrefix="uc1" TagName="UserCard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="SearchPanel" runat="server">
        <asp:TextBox ID="SearchText" runat="server" ></asp:TextBox><asp:Button ID="SearchBtn" runat="server" Text="Search Game!" OnClick="SearchBtn_Click" /><asp:Button ID="CreateGameBtn" runat="server" Text="Create Game" PostBackUrl="~/CreateGame.aspx" />
    </asp:Panel>
    <asp:GridView ID="Games" runat="server" AutoGenerateColumns="False" ItemType="BLL.GameRoom" EmptyDataText="There does not apear to be games, try searching for another name or try creating one yourselves" AllowPaging="True" OnPageIndexChanging="Games_PageIndexChanging" PageSize="6" OnRowCommand="Games_RowCommand">
        <Columns>
            <asp:BoundField DataField="GameID" HeaderText="ID" />
            <asp:BoundField DataField="GameName" HeaderText="Game Name" />
            <asp:BoundField DataField="users.Count" HeaderText="Players In Game" />
            <asp:TemplateField HeaderText="Host">
                <ItemTemplate>
                    <uc1:UserCard runat="server" ID="UserCard1" user='<%#Item.host%>'/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="JoinBtn" runat="server" CommandName="Join" Text="Join Game" CommandArgument=<%#Item.GameID%> />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
