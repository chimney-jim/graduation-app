<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Secretary.aspx.cs" Inherits="GraduateAppProcessing.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="secretaryLabel" runat="server" Font-Size="X-Large" 
        Text="Hello, SECRETARYNAME"></asp:Label>
    <asp:Button ID="addAppButton" runat="server" onclick="addAppButton_Click" 
        style="margin-left: 525px" Text="Add Application" />
    <span class="style1">
    <br />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" AutoGenerateSelectButton="True" CellPadding="4" 
        DataKeyNames="Id" DataSourceID="Application" Font-Size="Small" 
        ForeColor="#333333" GridLines="None" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" AllowPaging="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="StudentFirstName" HeaderText="StudentFirstName" 
                SortExpression="StudentFirstName" />
            <asp:BoundField DataField="StudentLastName" HeaderText="StudentLastName" 
                SortExpression="StudentLastName" />
            <asp:BoundField DataField="ProgramId" HeaderText="ProgramId" 
                SortExpression="ProgramId" />
            <asp:BoundField DataField="AppTerm" HeaderText="AppTerm" 
                SortExpression="AppTerm" />
            <asp:BoundField DataField="AppYear" HeaderText="AppYear" 
                SortExpression="AppYear" />
            <asp:BoundField DataField="DateInputed" HeaderText="DateInputed" 
                SortExpression="DateInputed" />
            <asp:BoundField DataField="AppDate" HeaderText="AppDate" 
                SortExpression="AppDate" />
            <asp:BoundField DataField="TimesReviewed" HeaderText="TimesReviewed" 
                SortExpression="TimesReviewed" />
            <asp:BoundField DataField="LastReviewed" HeaderText="LastReviewed" 
                SortExpression="LastReviewed" />
            <asp:BoundField DataField="IsAccepted" HeaderText="IsAccepted" 
                SortExpression="IsAccepted" />
            <asp:BoundField DataField="IsFinalized" HeaderText="IsFinalized" 
                SortExpression="IsFinalized" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
    <br />
    <asp:Button ID="editAppButton" runat="server" onclick="editAppButton_Click" 
        style="margin-left: 781px" Text="Edit Application" />
    <br />
    <asp:SqlDataSource ID="Application" runat="server" 
    ConnectionString="<%$ ConnectionStrings:applicationConnectionString2 %>" 
    ProviderName="<%$ ConnectionStrings:applicationConnectionString2.ProviderName %>" 
    SelectCommand="select * from application"></asp:SqlDataSource>
    </span>
</asp:Content>
