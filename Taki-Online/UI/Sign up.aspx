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
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please insert your email address" ValidationGroup="signup" ControlToValidate="email" ValidationExpression="[^@]+@[^\.]+\..+" Display="Dynamic"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="signup" ErrorMessage="Please insert a valid email address" ControlToValidate="email" Display="Dynamic"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label4" runat="server" Text="Username:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="Username" runat="server"></asp:TextBox><asp:RequiredFieldValidator  ValidationGroup="signup" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please insert your username" ControlToValidate="Username"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label5" runat="server" Text="Password:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="signup" runat="server" ErrorMessage="Your passowrd is too short please enter at least 5 charecters" ValidationExpression="[\S]{5,}" ControlToValidate="Password" Display="Dynamic"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please insert your password " ControlToValidate="Password" Display="Dynamic"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label6" runat="server" Text="Confirm Password:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="CPassword" runat="server" TextMode="Password"></asp:TextBox><asp:CompareValidator runat="server" ValidationGroup="signup" ErrorMessage="Your passwords don't match" ControlToCompare="Password" ControlToValidate="CPassword" Display="Dynamic"></asp:CompareValidator><asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please insert your password  again" ControlToValidate="CPassword" Display="Dynamic"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label7" runat="server" Text="First Name:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="FName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please insert your first name" ValidationGroup="signup" ControlToValidate="FName"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"><asp:Label ID="Label1" runat="server" Text="Last Name:"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="LName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please insert your last name" ValidationGroup="signup" ControlToValidate="LName"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Birth Date:" AssociatedControlID="Calendar"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Calendar ID="Calendar" runat="server" SelectionMode="Day"></asp:Calendar><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please insert your birth date"  OnServerValidate="CalanderValidation" ControlToValidate="Username" ValidationGroup="signup"></asp:CustomValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="Label8" runat="server" Text="Photo" AssociatedControlID="Photo"></asp:Label></asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:FileUpload ID="Photo" runat="server" AllowMultiple="False" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="False" />
        <asp:Button ID="SignUp" runat="server" Text="SIGN UP" OnClick="signup" ValidationGroup="SignUp" /><asp:Label ID="error" runat="server" Text=""></asp:Label>
    </asp:Panel>
</asp:Content>
