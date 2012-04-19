<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Secretary.aspx.cs" Inherits="GraduateAppProcessing.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!--<style type="text/css">
        .style1
        {
            font-size: x-large;
        }
    </style>-->
    <link href="Styles/SiteApplicationForm.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span class="style1">Hello, SECRETARYNAME&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="viewAppButton" runat="server" Text="View Application" 
        onclick="viewAppButton_Click" />
    <asp:Button ID="addAppButton" runat="server" Text="Add Application" 
        onclick="addAppButton_Click" />
    <br />
    </span>
    <br />
    <br />
    <br />
    <br />
    <br />
    <hr />
    <asp:Label ID="lblSecretaryError" runat="server" Text=""></asp:Label>
    <br />
    <fieldset><legend>Application List</legend>
    <asp:GridView ID="gvApplications" CssClass="gvApplications" runat="server" AutoGenerateColumns="false"  
        AllowPaging="true" PageSize="5" 
        AllowSorting="true" 
        onpageindexchanging="gvApplications_PageIndexChanging" 
        onsorting="gvApplications_Sorting">
        <HeaderStyle CssClass="gvApplicationsHeader" />
        <RowStyle CssClass="gvApplicationsRowStyle" />
        <AlternatingRowStyle CssClass="gvApplicationsAlternatingRowStyle" />
        <Columns>
        <asp:HyperLinkField DataTextField="Id" HeaderText="App. ID" DataNavigateUrlFields="Id" ItemStyle-Width="60px" 
                            DataNavigateUrlFormatString="~/ViewApplication.aspx?AppId={0}" Target="_blank" SortExpression="Id" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" ItemStyle-Width="90px" SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" ItemStyle-Width="90px" SortExpression="LastName" />
        <asp:BoundField DataField="ProgName" HeaderText="Program" ItemStyle-Width="70px" SortExpression="ProgName" />
        <asp:BoundField DataField="Term" HeaderText="Term" ItemStyle-Width="60px" SortExpression="Term" />
        <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-Width="50px" SortExpression="Year" />
        <asp:BoundField DataField="DateSubmitted" HeaderText="Submission Date" ItemStyle-Width="100px" SortExpression="DateSubmitted" />
        <asp:BoundField DataField="AppDate" HeaderText="App. Date" ItemStyle-Width="100px" SortExpression="AppDate" />
        <asp:BoundField DataField="Reviewed" HeaderText="Reviewed" ItemStyle-Width="80px" SortExpression="Reviewed" />
        <asp:BoundField DataField="LastReviewed" HeaderText="Last Reviewed" ItemStyle-Width="100px" SortExpression="LastReviewed" />
        </Columns>
    </asp:GridView>
    </fieldset>
    
</asp:Content>
