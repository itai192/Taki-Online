<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Loading Bar.ascx.cs" Inherits="UI.Custom_Controls.Loading_Bar" ClientIDMode="Static" %>

<div id="Out" runat="server" style="width:20em;height:1em;border-radius:1em;padding:0.25em">
    <div id="In" runat="server" style="text-align:center;border-radius:1em;height:90%;animation-name:fill;animation-duration:4s;margin-bottom:auto;margin-top:auto">
        <asp:Label ID="ProgressLbl"  runat="server" CssClass="center"></asp:Label></div>
</div>


