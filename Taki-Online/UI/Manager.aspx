<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="UI.Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="CenterBox">
    Online:<asp:Label ID="OnilneLbl" runat="server" ></asp:Label><br />
    Today:<asp:Label ID="VisitorsTodayLbl" runat="server"></asp:Label><br/>
    General:<asp:Label ID="UserAmount" runat="server"></asp:Label><br/>
    <asp:Chart ID="GamesInDates" runat="server">
        <Series>
            <asp:Series Name="Games" XValueType="DateTime" Legend="Legend1" XAxisType="Primary" YValueType="Auto">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" >
                <AxisX Title="Date"> </AxisX>
                <AxisY Title="Games"></AxisY>
            </asp:ChartArea>
        </ChartAreas>
        <Titles>
            <asp:Title Name="Games in month" Alignment="BottomCenter" IsDockedInsideChartArea="False" BackColor="Transparent" Font="Microsoft Sans Serif, 8.25pt, style=Bold" Text="Games In Dates">
            </asp:Title>
        </Titles>
    </asp:Chart>
        </div>
</asp:Content>
