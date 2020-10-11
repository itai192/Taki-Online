<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Sign up.aspx.cs" Inherits="UI.Sign_up" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <asp:Table ID="Table2" runat="server">

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="Label3" runat="server" Text="Email" AssociatedControlID="email"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label4" runat="server" Text="Username:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="Username" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label5" runat="server" Text="Password:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="Password" runat="server"></asp:TextBox><asp:RangeValidator runat="server" ErrorMessage="RangeValidator"></asp:RangeValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label6" runat="server" Text="Confirm Password:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="CPassword" runat="server"></asp:TextBox><asp:CompareValidator runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label7" runat="server" Text="First Name:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="FName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label1" runat="server" Text="Last Name:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="LName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Birth Date:" AssociatedControlID="Calander"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Calendar ID="Calendar" runat="server"></asp:Calendar>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="Button1" runat="server" Text="SIGN UP" />
    </asp:Panel>
</asp:Content>
