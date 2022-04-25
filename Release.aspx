<%@ Page Title="SWRelease" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Release.aspx.vb" Inherits="AIVC_WEBSITE.SWRelease" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="AIVCStyleSheet.css" rel="stylesheet" type="text/css" />
    <h1>AIVC Official Releases</h1>

<div class="td.row">
    <div class="row">
        <div class="column">
            <h2>SOP1 Official Releases</h2>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True"></asp:GridView>
        </div>
        <div class="column">
            <h2>SOP2 Official Releases</h2>
            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
        </div>
        <div class="column">
            <h2>SOP3 Official Releases</h2>
            <asp:GridView ID="GridView3" runat="server"></asp:GridView>
        </div>
    </div>
</div>

</asp:Content>