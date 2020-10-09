<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Sign in.aspx.cs" Inherits="UI.Sign_in" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label1" runat="server" Text="Label" AssociatedControlID="email">Email:</asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="email" runat="server"></asp:TextBox> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label2" runat="server" Text="Label" AssociatedControlID="password">Password:</asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="password" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="Button1" runat="server" Text="SIGN IN" /><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
        <hr />
        <asp:Label ID="Label3" runat="server" Text="Label" AssociatedControlID="signUp">new user?</asp:Label><asp:Button ID="signUp" runat="server" Text="SIGN UP" PostBackUrl="~/Sign up.aspx" />
    </asp:Panel>
</asp:Content>
