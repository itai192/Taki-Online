<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="Taki_AssosiationUI.Leaderboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="RankDropDown" runat="server" ItemType="Taki_AssosiationUI.WebService.WSRank" AutoPostBack="True" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="RankDropDown_SelectedIndexChanged"></asp:DropDownList>
    <asp:GridView ID="LeaderboardGrid" runat="server" AutoGenerateColumns="False" OnRowCommand="Leaderboard_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="username" HeaderText="Username" />
                        
                        <asp:BoundField DataField="elo" HeaderText="Elo Raiting" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                            <asp:Button runat="server" Text="See User Details" CommandName="Details" ID="Detailsbtn" CommandArgument="<%# ((Taki_AssosiationUI.WebService.WSUser)Container.DataItem).username %>" />
                             </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <EmptyDataTemplate>
                        there are no users at that rank<br />
                    </EmptyDataTemplate>
                </asp:GridView>
</asp:Content>
