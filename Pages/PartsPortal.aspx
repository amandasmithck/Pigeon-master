<%@ Page Title="Parts Portal" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="PartsPortal.aspx.vb" Inherits="Pigeon.PartsPortal3" %>

<%@ Register src="../Controls/Admin/PartsPortal.ascx" tagname="AdminPartsPortal" tagprefix="uc1" %>
<%@ Register src="../Controls/Customer/PartsPortal.ascx" tagname="CustomerPartsPortal" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Styles/partsportal.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.countdown.css" />

    <script type="text/javascript" src="../Scripts/jquery.countdown.min.js"></script>
    <script type="text/javascript" src="../Scripts/partsportalB-1.0.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.counter-1.0.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:CustomerPartsPortal ID="CustomerPartsPortal" runat="server" />
    <uc1:AdminPartsPortal ID="AdminPartsPortal" runat="server" />
</asp:Content>