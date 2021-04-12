<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Sign up.aspx.cs" Inherits="UI.Sign_up" %>

<%@ Register Src="~/Custom Controls/ProfilePictureUpload.ascx" TagPrefix="uc1" TagName="ProfilePictureUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="SignUp">
        <asp:Table ID="Table2" runat="server">

            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="EmailLbl" runat="server" Text="Email" AssociatedControlID="email"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="ValidEmailValidator" runat="server" ErrorMessage="Please insert a valid email address" ValidationGroup="signup" ControlToValidate="email" ValidationExpression="[^@]+@[^\.]+\..+" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredEmailValidator" runat="server" ValidationGroup="signup" ErrorMessage="Please insert your email address" ControlToValidate="email" Display="Dynamic"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="UsernameLbl" runat="server" Text="Username:" AssociatedControlID="Username"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="Username" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator  ValidationGroup="signup" ID="RequiredUsername" runat="server" ErrorMessage="Please insert your username" ControlToValidate="Username"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="PasswordLbl" runat="server" Text="Password:" AssociatedControlID="Password"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" ViewStateMode="Enabled"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="PasswordValidator" ValidationGroup="signup" runat="server" ErrorMessage="Your passowrd is too short please enter at least 5 charecters" ValidationExpression="[\S]{5,}" ControlToValidate="Password" Display="Dynamic"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please insert your password " ControlToValidate="Password" Display="Dynamic"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="Label6" runat="server" Text="Confirm Password:" AssociatedControlID="CPassword"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="CPassword" runat="server" TextMode="Password" ViewStateMode="Enabled"></asp:TextBox>
                    <asp:CompareValidator runat="server" ValidationGroup="signup" ErrorMessage="Your passwords don't match" ControlToCompare="Password" ControlToValidate="CPassword" Display="Dynamic" ID="ComparePasswords"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredPasswordAgain" runat="server" ErrorMessage="Please insert your password  again" ControlToValidate="CPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="FirstNameLbl" runat="server" Text="First Name:" AssociatedControlID="FName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="FName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FirstNameValidator" runat="server" ErrorMessage="Please insert your first name" ValidationGroup="signup" ControlToValidate="FName"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="LastNameLbl" runat="server" Text="Last Name:" AssociatedControlID="LName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox ID="LName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="LastNameValidator" runat="server" ErrorMessage="Please insert your last name" ValidationGroup="signup" ControlToValidate="LName"></asp:RequiredFieldValidator></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="CalanderLbl" runat="server" Text="Birth Date:" AssociatedControlID="Calendar"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Calendar ID="Calendar" runat="server" SelectionMode="Day" ></asp:Calendar>
                    <asp:CustomValidator ID="BirthdayValidator" runat="server" ErrorMessage="Please insert a valid birth date"  OnServerValidate="CalanderValidation"  ValidationGroup="signup"></asp:CustomValidator>
                    <br/>
                    <asp:Label ID="YearLbl" runat="server" Text="Select Year:" AssociatedControlID="YearLbl"></asp:Label><asp:DropDownList ID="YearSelect" runat="server" AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="YearSelect_SelectedIndexChanged"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="PhotoLbl" runat="server" Text="Profile picture:" AssociatedControlID="ProfilePictureUpload"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <uc1:ProfilePictureUpload runat="server" ID="ProfilePictureUpload" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="SignUp" runat="server" Text="SIGN UP" OnClick="signup" ValidationGroup="signup" /><asp:Label ID="error" runat="server" Text=""></asp:Label>
    </asp:Panel>
    
</asp:Content>
