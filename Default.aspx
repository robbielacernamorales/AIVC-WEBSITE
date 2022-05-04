<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AIVC_WEBSITE._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Default.css" rel="stylesheet" type="text/css" />
    <div class="DailyBuildSchedules" id="wehavebuild" runat="server">
            <h2>Build Schedule (TODAY)</h2>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>
    <div class="DailyBuildSchedules" id="wedonthavebuild" runat="server">
        <h2>Build Schedule</h2>
        <p style="color: red; font-size: xx-large;">No Build Today</p>
    </div>

    <div class="FutureBuildSchedules" id="wehavefuturebuilds" runat="server">
        <h2>Future Schedule (Week)</h2>
        <asp:GridView ID="GridViewFutureBuilds" runat="server"></asp:GridView>
    </div>
    <div class="FutureBuildSchedules" id="wedonthavefuturebuilds" runat="server">
        <h2>Future Schedule</h2>
        <p style="color: red; font-size: xx-large;">No Future Build Schedule(s)</p>
    </div>

   <%-- <div class="row">
        <div class="col-md-4">
            <h2>1</h2>
            <p>
                1
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>2</h2>
            <p>
                2
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>3</h2>
            <p>
                3
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>--%>

</asp:Content>
