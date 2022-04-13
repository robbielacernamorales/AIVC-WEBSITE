<%@ Page Title="SWRelease" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Release.aspx.vb" Inherits="AIVC_WEBSITE.SWRelease" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="AIVCStyleSheet.css" rel="stylesheet" type="text/css" />
    <h1>AIVC Official Releases</h1>
    <p>SOP1 SW Releases.</p>
    <h1><asp:GridView ID="GridViewSOP1" runat="server" CellPadding="10" CellSpacing="10"></asp:GridView></></h1>
</asp:Content>

<%--  --%>