<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrintArrivals.aspx.vb" Inherits="Pigeon.PrintArrivals2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    
    <title>Print Arrivals</title>
    <script id="printTemplate" type="text/html">
        <tr>
            <td>${Part}</td><td class='sn'>${SN}</td><td>${ETA}</td><td style="width: 180px">&nbsp;</td>
        <tr>
    </script>
    <style type="text/css">
        body {font-family: Arial, Helvetica, Sans-Serif;}
        
        .container {width:600px;}
        table{ font-size:12px; width: 100%; border-collapse: collapse;}
        table td { padding: 10px; border: 1px solid #ccc; }
        h1 {margin: 10px 0;}
        p { margin-bottom: 20px; font-weight:bold; }
    </style>
    <script>
        var stockdata = {};
        var arrivaldata;
        $('document').ready(function () {
            var urlMethod = "../IMSWebService.asmx/GetStock";
            var json = { 'client': $('.current_client').text(), 'type': getParameterByName('type') };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                stockdata = jQuery.parseJSON(msg.d);
                arrivaldata = stockdata[0].filter(function (el) {


                    return el.Arrive == null;
                });

                $(arrivaldata).each(function () {
                    $('#print-table').append($("#printTemplate").tmpl(this));
                });
                $('.count').html("Count: " + arrivaldata.length);
            });
        });
        function SendAjax(urlMethod, jsonData, returnFunction) {
            $.ajax({
                type: "POST",
                contentType: 'application/json',
                url: urlMethod,
                data: jsonData,
                dataType: "json",
                success: function (msg) {
                    if (msg != null) {
                        returnFunction(msg);
                    }
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.search);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    </script>
</head>

<body>
    <form id="form1" runat="server"><asp:Label runat="server" ID="current_client" class="current_client" ></asp:Label>
    <div class="container">
        <h1>Expected Arrivals</h1>
        <p class="count"></p>
        <table id="print-table">
        <tr>
            <th>Part #</th>
            <th>Serial #</th>
            <th>ETA</th>
            <th>Bin</th>
        </tr>
        
        </table>
    </div>
    </form>
</body>
</html>
