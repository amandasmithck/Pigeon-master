<%@ Page Title="Parts Portal" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/PigeonCarmax.Master" CodeBehind="CarmaxPartsPortal.aspx.vb" Inherits="Pigeon.CarmaxPartsPortal" %>

<%@ Register src="../Controls/Admin/CKPartsPortal.ascx" tagname="AdminPartsPortal" tagprefix="uc1" %>
<%@ Register src="../Controls/Customer/CKPartsPortal.ascx" tagname="CustomerPartsPortal" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Styles/ckpartsportal.css" />
    <link type="text/css" href="../Styles/jquery.loadmask.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.countdown.css" />

    <script type="text/javascript" src="../Scripts/jquery.countdown.min.js"></script>
    <!--<script type="text/javascript" src="../Scripts/partsportal2.js"></script>-->
    <script type="text/javascript" src="../Scripts/jquery.counter-1.0.js"></script>
    <script type="text/javascript" src="../Scripts/partsportal-1.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.loadmask.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:CustomerPartsPortal ID="CustomerPartsPortal" runat="server" />
    <uc1:AdminPartsPortal ID="AdminPartsPortal" runat="server" />
</asp:Content>