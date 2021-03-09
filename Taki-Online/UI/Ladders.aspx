<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Ladders.aspx.cs" Inherits="UI.Ladders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="RanksDropDown" runat="server" AutoPostBack="True" DataTextField="name" DataValueField="ID" ItemType="Rank"></asp:DropDownList>
    <asp:GridView ID="Leaderboard" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="User" />
            <asp:BoundField DataField="ELO" />
        </Columns>
    </asp:GridView>
</asp:Content>
