<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="GraduateAppProcessing._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Content header
    </h2>
    <p>
        Content body
    </p>
    <asp:TextBox ID="TextBox1" runat="server" Text="Placeholder"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <br />
    <br />
    <label style="font-size:larger">Temporary backdoor links: </label>
    <br />
    <br />
    <a href="Secretary.aspx">Secretary page</a>
    <br />
    <br />
    <a href="ApplicationForm.aspx">Create application</a>
    <br />
    <br />
    <a href="ViewApplication.aspx">View application</a>
    <br />
    <br />
    <asp:Button ID="btnRedirect" runat="server" Text="Button" 
        onclick="btnRedirect_Click" />
</asp:Content>
