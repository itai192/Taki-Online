<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Taki_AssosiationUI.Sign_In" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="SignInPanel" runat="server" DefaultButton="Login">
        <asp:Table ID="SignInTbl" runat="server">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="UsernameLbl" runat="server"  AssociatedControlID="username">Username:</asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="username" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="PasswordLbl" runat="server"  AssociatedControlID="password">Password:</asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Insert A Password" ControlToValidate="password" SetFocusOnError="True" ValidationGroup="signin"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="Login" runat="server" Text="SIGN IN" ValidationGroup="signin" OnClick="SignIn" /><asp:Label ID="Error" runat="server" ></asp:Label>
        <hr />
        <asp:Label ID="newuserlbl" runat="server" Text="Label" AssociatedControlID="signUp">new user?</asp:Label>
        <br/>
        <asp:Button ID="signUp" runat="server" Text="SIGN UP" PostBackUrl="~/Sign up.aspx" />
    </asp:Panel>
</asp:Content>
