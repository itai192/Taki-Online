<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="UI.Play" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="SearchPanel" runat="server">
        <asp:TextBox ID="GameText" runat="server"></asp:TextBox><asp:Button ID="SearchBtn" runat="server" Text="Search Game!" />
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ItemType="BLL.GameRoom">
        <Columns>
            <asp:BoundField DataField="GameID" />
            <asp:BoundField DataField="GameName" />
            <asp:BoundField />
            <asp:ButtonField Text="Join Game" ButtonType="Button" />
        </Columns>
    </asp:GridView>
</asp:Content>
