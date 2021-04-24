<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Ladders.aspx.cs" Inherits="UI.Ladders" %>
<%@ Register src="Custom Controls/UserCard.ascx" tagname="UserCard" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="CenterBox">
    <asp:DropDownList ID="RanksDropDown" runat="server" AutoPostBack="True" DataTextField="name" DataValueField="ID" ItemType="Rank"></asp:DropDownList>
    <asp:GridView ID="Leaderboard" runat="server" AutoGenerateColumns="False" CssClass="Grid" OnSelectedIndexChanged="Leaderboard_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField HeaderText="User">
                <ItemTemplate>
                    <uc1:UserCard ID="UserCard1" runat="server"  user='<%#new BLL.User(((System.Data.DataRowView)Container.DataItem)["User"].ToString()) %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ELO" HeaderText="Elo" />
        </Columns>
        <EmptyDataTemplate>
            There are no players at that rank
        </EmptyDataTemplate>
    </asp:GridView>
        </div>
</asp:Content>
