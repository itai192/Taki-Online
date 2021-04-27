<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Taki_AssosiationUI.Account" %>

<%@ Register Src="~/UserDetailsTable.ascx" TagPrefix="uc1" TagName="UserDetailsTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td><uc1:UserDetailsTable runat="server" ID="UserDetailsTable" /></td>
            <td>
                >Friends:</>
                <asp:GridView ID="Friends" runat="server" AutoGenerateColumns="False" OnRowCommand="Friends_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="username" HeaderText="Username" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                            <asp:Button runat="server" Text="See User Details" CommandName="Details" ID="Detailsbtn" CommandArgument="<%# ((Taki_AssosiationUI.WebService.WSUser)Container.DataItem).username %>" />
                             </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <EmptyDataTemplate>
                        you have no friends ):<br />
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>

        <asp:Label ID="ErrorLbl" runat="server"></asp:Label>
   
</asp:Content>
