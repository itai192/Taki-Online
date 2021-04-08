<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Summery.aspx.cs" Inherits="UI.Summery" %>
<%@ Register Src="~/Custom Controls/UserCard.ascx" TagPrefix="uc1" TagName="UserCard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="SummeryTbl" runat="server" AutoGenerateColumns="False" ItemType="BLL.UserStatsInGame" OnRowDataBound="SummeryTbl_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Place">
                <ItemTemplate>
                    #<asp:Label ID="Numberlbl" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User">
                <ItemTemplate>
                    <uc1:UserCard runat="server" ID="UserCard" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Cards in hand" DataField="cardsInHand" />
            
        </Columns>
    </asp:GridView>
    Elo Change:<asp:Label ID="EloChangeLbl" runat="server"></asp:Label><br/>
    XP Change:<asp:Label ID="XpChangeLbl" runat="server" Text="Label"></asp:Label>
</asp:Content>
