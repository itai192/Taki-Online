<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Taki_AssosiationUI.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="SignUpBtn" CssClass="CenterBox">
        <asp:Table ID="Table2" runat="server">

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="EmailLbl" runat="server" Text="Email" AssociatedControlID="email"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="UsernameLbl" runat="server" Text="Username:" AssociatedControlID="Username"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="Username" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="PasswordLbl" runat="server" Text="Password:" AssociatedControlID="Password"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="FirstNameLbl" runat="server" Text="First Name:" AssociatedControlID="FName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="FName" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="LastNameLbl" runat="server" Text="Last Name:" AssociatedControlID="LName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="LName" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="CalanderLbl" runat="server" Text="Birth Date:" AssociatedControlID="Calendar"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Calendar ID="Calendar" runat="server" SelectionMode="Day" ></asp:Calendar>
                    <br />
                    <asp:Label ID="YearLbl" runat="server" Text="Select Year:" AssociatedControlID="YearLbl"></asp:Label><asp:DropDownList ID="YearSelect" runat="server" AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="YearSelect_SelectedIndexChanged"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="SignUpBtn" runat="server" Text="SIGN UP" OnClick="signup" ValidationGroup="signup" /><asp:Label ID="error" runat="server" Text=""></asp:Label>
    </asp:Panel>
</asp:Content>
