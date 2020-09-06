<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Home_Index" %>

<%@ Register Src="../Home/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_menu" runat="Server">
    <uc1:Menu runat="server" ID="Menu" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_head" runat="Server">
    <title>保税仓储系统</title>
    <script type="text/javascript" src="../Declare/js/Warning.js?t_=<%=DateTime.Now.ToString("HHmmssfff") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_body" runat="Server">

     



</asp:Content>

