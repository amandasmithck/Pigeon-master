<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sandbox.aspx.vb" Inherits="Pigeon.sandbox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link type="text/css" href="../Styles/ui-theme/jquery-ui-1.8.10.custom.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/modules/checkoutform.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/bootstrap.css" rel="stylesheet" />
    <link href="../../../Styles/1140.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <button>Order</button>
</body>

    <script type="text/javascript" src="../Scripts/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.10.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate-1.0.js"></script>
    <script type="text/javascript" src="../Scripts/accounting.js"></script>

    <script type="text/javascript" src="../Scripts/jquery.counter-1.0.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.maskedinput-1.3.min.js"></script>

    <!--<script id="checkoutTemplate" type="text/x-jquery-tmpl" src="../Scripts/modules/checkout-form-tmpl.js"></script>-->
    <script type="text/javascript" src="../Scripts/modules/checkout-form.js"></script>
    <script type="text/javascript">
        var checkoutOptions = {};
        var user = <%= Session("UserModel") %>;

        var checkout = new CheckoutForm(checkoutOptions, user);

        var orderInfo = {Parts: [{ "warranties": [], "tiers": [{ "Price": 2099, "Label": "Your Cost*", "TierID": "3", "WarrantyPrice": 0, "Local": true}], "maps": [{ "State": "Arizona", "Schedule": "1", "Value": "1", "Abbreviation": "az", "Warehouse": 2 }, { "State": "California", "Schedule": "1", "Value": "1", "Abbreviation": "ca", "Warehouse": 2 }, { "State": "Colorado", "Schedule": "1", "Value": "1", "Abbreviation": "co", "Warehouse": 13 }, { "State": "Idaho", "Schedule": "1", "Value": "1", "Abbreviation": "id", "Warehouse": 8 }, { "State": "Montana", "Schedule": "1", "Value": "1", "Abbreviation": "mt", "Warehouse": 8 }, { "State": "Nevada", "Schedule": "1", "Value": "1", "Abbreviation": "nv", "Warehouse": 2 }, { "State": "New Mexico", "Schedule": "1", "Value": "1", "Abbreviation": "nm", "Warehouse": 2 }, { "State": "Oregon", "Schedule": "1", "Value": "1", "Abbreviation": "or", "Warehouse": 8 }, { "State": "Utah", "Schedule": "1", "Value": "1", "Abbreviation": "ut", "Warehouse": 13 }, { "State": "Washington", "Schedule": "1", "Value": "1", "Abbreviation": "wa", "Warehouse": 8 }, { "State": "Wyoming", "Schedule": "1", "Value": "1", "Abbreviation": "wy", "Warehouse": 13 }, { "State": "Alabama", "Schedule": "1", "Value": "1", "Abbreviation": "al", "Warehouse": 4 }, { "State": "Alaska", "Schedule": "5", "Value": "5", "Abbreviation": "ak", "Warehouse": 2 }, { "State": "Arkansas", "Schedule": "1", "Value": "1", "Abbreviation": "ar", "Warehouse": 7 }, { "State": "Connecticut", "Schedule": "2", "Value": "2", "Abbreviation": "ct", "Warehouse": 6 }, { "State": "Delaware", "Schedule": "1", "Value": "1", "Abbreviation": "de", "Warehouse": 6 }, { "State": "Florida", "Schedule": "1", "Value": "1", "Abbreviation": "fl", "Warehouse": 4 }, { "State": "Georgia", "Schedule": "1", "Value": "1", "Abbreviation": "ga", "Warehouse": 4 }, { "State": "Hawaii", "Schedule": "5", "Value": "5", "Abbreviation": "hi", "Warehouse": 2 }, { "State": "Illinois", "Schedule": "1", "Value": "1", "Abbreviation": "il", "Warehouse": 6 }, { "State": "Indiana", "Schedule": "1", "Value": "1", "Abbreviation": "in", "Warehouse": 6 }, { "State": "Iowa", "Schedule": "1", "Value": "1", "Abbreviation": "ia", "Warehouse": 7 }, { "State": "Kansas", "Schedule": "1", "Value": "1", "Abbreviation": "ks", "Warehouse": 1 }, { "State": "Kentucky", "Schedule": "1", "Value": "1", "Abbreviation": "ky", "Warehouse": 6 }, { "State": "Lousiana", "Schedule": "1", "Value": "1", "Abbreviation": "la", "Warehouse": 1 }, { "State": "Maine", "Schedule": "2", "Value": "2", "Abbreviation": "me", "Warehouse": 6 }, { "State": "Maryland", "Schedule": "1", "Value": "1", "Abbreviation": "md", "Warehouse": 6 }, { "State": "Massachussetts", "Schedule": "2", "Value": "2", "Abbreviation": "ma", "Warehouse": 6 }, { "State": "Michigan", "Schedule": "1", "Value": "1", "Abbreviation": "mi", "Warehouse": 6 }, { "State": "Minnesota", "Schedule": "1", "Value": "1", "Abbreviation": "mn", "Warehouse": 7 }, { "State": "Mississippi", "Schedule": "1", "Value": "1", "Abbreviation": "ms", "Warehouse": 1 }, { "State": "Missouri", "Schedule": "1", "Value": "1", "Abbreviation": "mo", "Warehouse": 1 }, { "State": "Nebraska", "Schedule": "1", "Value": "1", "Abbreviation": "ne", "Warehouse": 7 }, { "State": "New Hampshire", "Schedule": "2", "Value": "2", "Abbreviation": "nh", "Warehouse": 6 }, { "State": "New Jersey", "Schedule": "1", "Value": "1", "Abbreviation": "nj", "Warehouse": 6 }, { "State": "New York", "Schedule": "1", "Value": "1", "Abbreviation": "ny", "Warehouse": 6 }, { "State": "North Carolina", "Schedule": "1", "Value": "1", "Abbreviation": "nc", "Warehouse": 6 }, { "State": "North Dakota", "Schedule": "2", "Value": "2", "Abbreviation": "nd", "Warehouse": 7 }, { "State": "Ohio", "Schedule": "1", "Value": "1", "Abbreviation": "oh", "Warehouse": 6 }, { "State": "Oklahoma", "Schedule": "1", "Value": "1", "Abbreviation": "ok", "Warehouse": 1 }, { "State": "Pennsylvania", "Schedule": "1", "Value": "1", "Abbreviation": "pa", "Warehouse": 6 }, { "State": "Rhode Island", "Schedule": "1", "Value": "1", "Abbreviation": "ri", "Warehouse": 6 }, { "State": "South Carolina", "Schedule": "1", "Value": "1", "Abbreviation": "sc", "Warehouse": 4 }, { "State": "South Dakota", "Schedule": "1", "Value": "1", "Abbreviation": "sd", "Warehouse": 7 }, { "State": "Tennessee", "Schedule": "1", "Value": "1", "Abbreviation": "tn", "Warehouse": 6 }, { "State": "Texas", "Schedule": "1", "Value": "1", "Abbreviation": "tx", "Warehouse": 1 }, { "State": "Vermont", "Schedule": "2", "Value": "2", "Abbreviation": "vt", "Warehouse": 6 }, { "State": "Virginia", "Schedule": "2", "Value": "2", "Abbreviation": "va", "Warehouse": 6 }, { "State": "West Virginia", "Schedule": "1", "Value": "1", "Abbreviation": "wv", "Warehouse": 6 }, { "State": "Wisconsin", "Schedule": "1", "Value": "1", "Abbreviation": "wi", "Warehouse": 6}], "installations": null, "applications": null, "core": "260", "stock": 0, "localstock": 0, "partno": "DB57", "totalinstall": 0, "installtotal": 0, "vendor": 75, "subtype": "Reman", "showpartno": true, "SalePrice": 2099, "CorePrice": "260", "PartNumber": "DB57"}]
        };

        $('button').click(function () {
            checkout.render(orderInfo);
        });
        
    </script>
</html>
