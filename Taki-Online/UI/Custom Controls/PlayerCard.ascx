<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlayerCard.ascx.cs" Inherits="UI.Custom_Controls.PlayerCard" %>
<%@ Register src="ProfilePicture.ascx" tagname="ProfilePicture" tagprefix="uc1" %>
<asp:Panel ID="UserPanel" runat="server" Width="200px" Direction="LeftToRight">
    <uc1:ProfilePicture ID="ProfilePicture" runat="server" height="3em" pictureName="3lh" width="3em" ViewStateMode="Enabled" CssClass="FloatLeft"/>
    <asp:Table ID="Details" runat="server">
        
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">Username:</asp:TableCell>
            <asp:TableCell runat="server"><asp:Label ID="UsernameLbl" runat="server" ></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">Amount of Cards:</asp:TableCell>
            <asp:TableCell runat="server"><asp:Label ID="CardsLbl" runat="server" ></asp:Label></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Panel>