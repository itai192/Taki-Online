<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="UI.Game" %>
<%@ Register Src="~/Custom Controls/Card.ascx" TagPrefix="uc1" TagName="Card" %>
<%@ Register Src="~/Custom Controls/PlayerCard.ascx" TagPrefix="uc1" TagName="PlayerCard" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer" runat="server" Interval="750" EnableViewState="False" OnTick="Timer_Tick"></asp:Timer>
            <div class="Panel">
            <uc1:Card runat="server" ID="Pile" />
            <uc1:Card runat="server" ID="Deck" />
            <asp:Repeater ID="Hand" runat="server" ItemType="Card" OnItemDataBound="Hand_ItemDataBound">
                <ItemTemplate>
                    <uc1:Card runat="server" ID="Card" IsButton="True" />
                </ItemTemplate>
            </asp:Repeater>
            </div>
            <asp:Label ID="Statuslbl" runat="server" Text=""  ></asp:Label>
            <script type="text/javascript" src="CardAnimations.js"></script>
            <asp:DataList ID="Users" runat="server" RepeatDirection="Horizontal" OnItemDataBound="Users_ItemDataBound">
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Players"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <uc1:PlayerCard runat="server" player="<%#Container.DataItem%>" ID="PlayerCard" />
                </ItemTemplate>
            </asp:DataList>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
