<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="VendorManagementPortal.aspx.vb" Inherits="Pigeon.VendorManagementPortal" %>
<link href="/Styles/bootstrap.css" rel="stylesheet" />
<link href="/Styles/sweetalert2.css" rel="stylesheet" />
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>
<script src="/Scripts/jquery-1.11.3.min.js"></script>
<script src="/Scripts/knockout_3_2_0.js"></script>
<script src="/Scripts/ajaxHelperViewModel.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var VendorMangementViewModel=function()
        {
            ko.utils.extend(self, new ajaxHelperViewModel());
            vendorInfo = ko.observableArray();

            function EmptyVendorInfo()
            {
                var emptyVendorInfo = {};
            }
        }
        ko.applyBindings(new VendorManagementViewModel());
    });

</script>
