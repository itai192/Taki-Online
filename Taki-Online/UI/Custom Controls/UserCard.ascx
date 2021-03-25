<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserCard.ascx.cs" Inherits="UI.Custom_Controls.UserCard" %>
<%@ Register src="ProfilePicture.ascx" tagname="ProfilePicture" tagprefix="uc1" %>
<asp:Panel ID="Panel1" runat="server" Width="">
    <uc1:ProfilePicture ID="ProfilePicture" runat="server" height="3lh" pictureName="3lh" width="3lh" ViewStateMode="Enabled" />
    <asp:Table ID="Details" runat="server">
        
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">Username:</asp:TableCell>
            <asp:TableCell runat="server"><asp:Label ID="UsernameLbl" runat="server" ></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">Level:</asp:TableCell>
            <asp:TableCell runat="server"><asp:Label ID="LevelLbl" runat="server" ></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">Rank:</asp:TableCell>
            <asp:TableCell runat="server"><asp:Label ID="RankLbl" runat="server" ></asp:Label></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
        
        
</asp:Panel>