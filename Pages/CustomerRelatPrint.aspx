<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerRelatPrint.aspx.vb" Inherits="Pigeon.CustomerRelatPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script type="text/javascript" src="../Scripts/jquery.min.js" ></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery.flot.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery.flot.stack.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.loadmask.js"></script>
    <script type="text/javascript" src="../Scripts/accounting.js"></script>

<%--    <link href="~/Styles/bootstrap/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap/bootstrap.js"></script>--%>
    <script src="../Scripts/bootstrap-typeahead.js" type="text/javascript"></script>
    <script id="afterTemplate" type="text/html">
        <tr style="font-size:11px">
            <td style="width:210px"><span style="margin-left:25px;">${PartType}</span></td>
            <td style="text-align:center; width:125px !important;">${Units}</td>
            <td style="text-align:right; width:100px !important;">${accounting.formatMoney(Amount)}</td>
            <td style="text-align:right; width:100px !important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
    </script>
     <script id="monthTemplate" type="text/html">
         
         <table style="width:550px" class="table table-striped">
            <tr style="font-size:12px">
                <th style="width:210px">${MonthName}</th>
                <th style="text-align:center; text-decoration:underline; width:125px !important">Units</th>
                <th style="text-align:right; text-decoration:underline; width:100px !important">$</th>
                <th style="text-align:right; text-decoration:underline; width:100px  !important">Inspections</th>
            </tr>
            <tr style="font-weight:bold;font-size:12px">
                <td style="width:210px">Aftermarket</td>
                <td style="text-align:center; width:125px !important">${AftermarketUnits}</td>
                <td style="text-align:right; width:100px !important">${accounting.formatMoney(AftermarketAmount)}</td>
                <td style="text-align:right; text-decoration:underline; width:100px  !important"></td>
            </tr>
         </table>
         <table id="${OrderMonth}-${OrderYear}"  style="width:550px" class="table table-striped">

         </table>
         <table style="margin-bottom:15px;width:550px" class="table table-striped ">
            <tr style="font-size:12px">
                <th style="width:210px">OEM</th>
                <th style="text-align:center; width:125px !important">${OEMUnits}</th>
                <th style="text-align:right; width:100px !important">${accounting.formatMoney(OEMAmount)}</th>
                <th style="text-align:right; text-decoration:underline; width:100px  !important"></th>
            </tr>
            <tr style="font-weight:bold;font-size:12px">
                <td style="width:210px">Small Parts</td>
                <td style="text-align:center; width:125px !important">${SmallUnits}</td>
                <td style="text-align:right; width:100px !important">${accounting.formatMoney(SmallAmount)}</td>
                <td style="text-align:right; text-decoration:underline; width:100px  !important"></td>
            </tr>
        </table>
         <table style="margin-bottom:20px;width:550px" class="table table-striped ">
            <tr style="font-weight:bold;font-size:12px">
                <td style="width:210px !important">Total</td>
                <td style="text-align:center; width:125px !important">${TotalUnits}</td>
                <td style="text-align:right; width:100px !important">${accounting.formatMoney(TotalAmount)}</td>
                <td style="text-align:right; width:100px  !important">${Inspections}</td>
            </tr>
        </table>
     </script>
    
    <script id="customerTemplate" type="x-jquery-tmpl">
       <option value="${CustNo}">${Company}</option>
    </script>


    <script language="javascript" type="text/javascript">
        function GetQueryStringParams(sParam) {
            var sPageURL = window.location.search.substring(1);
            var sURLVariables = sPageURL.split('&');
            for (var i = 0; i < sURLVariables.length; i++) {
                var sParameterName = sURLVariables[i].split('=');
                if (sParameterName[0] == sParam) {
                    return sParameterName[1];
                }
            }
        }
        
        $('document').ready(function () {
            GetData();
           
        });
        var client = 'CK';
        function GetData() {
            $('#months').children().remove();
            $('#months').loadmask("Sit back and relax while I do some magic...");
            $('#viewing').html('Currently Viewing ' + $('#txtfromdate').val() + ' - ' + $('#txttodate').val())

            var data = { 'customerno': GetQueryStringParams('customerno'), 'fromdate': GetQueryStringParams('fromdate'), 'todate': GetQueryStringParams('todate'), 'client': client };
            var jsonData = JSON.stringify(data);
            var company = "";
            $.ajax({
                type: "POST",
                url: "../ReportsWebService.asmx/CustomerRelat",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var resp = jQuery.parseJSON(msg.d);
                    var oemParts = [];
                    var aftermarketParts = [];
                    var smallParts = [];
                    var oemPurchases = [];
                    var aftermarketPurchases = [];
                    var smallPurchases = [];
                    var inspections = [];

                    

                    $("#months").append($('#monthTemplate').tmpl(resp))
                    $(resp).each(function () {
                        company = $(this)[0].Company;
                        var aftermarketTable = $(this)[0].OrderMonth + '-' + $(this)[0].OrderYear;
                        var $el = $('#' + aftermarketTable)
                        $el.append($('#afterTemplate').tmpl($(this)[0].AftermarketList))

                        var oemobj = []
                        oemobj.push($(this)[0].Timestamp, $(this)[0].OEMUnits);
                        oemParts.push(oemobj);

                        var aftermarketobj = []
                        aftermarketobj.push($(this)[0].Timestamp, $(this)[0].AftermarketUnits);
                        aftermarketParts.push(aftermarketobj);

                        var smallobj = []
                        smallobj.push($(this)[0].Timestamp, $(this)[0].SmallUnits);
                        smallParts.push(smallobj);

                        var oemobj = []
                        oemobj.push($(this)[0].Timestamp, $(this)[0].OEMAmount);
                        oemPurchases.push(oemobj);

                        var aftermarketobj = []
                        aftermarketobj.push($(this)[0].Timestamp, $(this)[0].AftermarketAmount);
                        aftermarketPurchases.push(aftermarketobj);

                        var smallobj = []
                        smallobj.push($(this)[0].Timestamp, $(this)[0].SmallAmount);
                        smallPurchases.push(smallobj);

                        var insobj = []
                        insobj.push($(this)[0].Timestamp, $(this)[0].Inspections);
                        inspections.push(insobj);


                    });

                    $.plot($("#purchases-graph"), [{ data: oemPurchases, label: "OEM" }, { data: aftermarketPurchases, label: "Aftermarket" }, { data: smallPurchases, label: "Small Parts" }],

                   {
                       xaxis: {
                           mode: 'time',
                           timeformat: "%b",
                           minTickSize: [1, "month"]
                       },
                       series: {
                           stack: true,
                           lines: { show: false, fill: false, steps: false },
                           bars: { show: true, barWidth: 864000000 }
                       },
                       grid: { hoverable: true },
                       legend: { container: $('#purchases-legend') }
                   });

                    $.plot($("#parts-graph"), [{ data: oemParts, label: "OEM" }, { data: aftermarketParts, label: "Aftermarket" }, { data: smallParts, label: "Small Parts" }],

                    {
                        xaxis: {
                            mode: 'time',
                            timeformat: "%b",
                            minTickSize: [1, "month"]
                        },
                        series: {
                            stack: true,
                            lines: { show: false, fill: false, steps: false },
                            bars: { show: true, barWidth: 864000000 }
                        },
                        grid: { hoverable: true },
                        legend: { container: $('#parts-legend') }
                    });

                    $.plot($("#inspections-graph"), [{ data: inspections, label: "Inspections" }],

                    {
                        xaxis: {
                            mode: 'time',
                            timeformat: "%b",
                            minTickSize: [1, "month"]
                        },
                        series: {
                            stack: true,
                            lines: { show: false, fill: false, steps: false },
                            bars: { show: true, barWidth: 864000000 }
                        },
                        legend: {
                            show: false
                        }
                    });

                    $('#purchase-header').text('Value of Parts Purchased');
                    $('#units-header').text('Units Purchased');
                    $('#inspections-header').text('Inspections');
                    $('#company').text(company);
                    $('#months').unloadmask();
                },
                error: function () {
                    $('#months').unloadmask();
                    alert("An error has occurred during processing your request.");
                }
            });
        }
    </script>
    <title></title>
</head>
<body>

        <div >
            <div >
                <h5>Customer Relationship Summary</h5>
                <h5 id="company"></h5><br />
                <div id="months">
                    
                </div>
            </div>

            
        </div>
        <br />
        <div>
            <div >
                    <div id="graphs">
                        <div style="margin-bottom:50px;height:350px"><h3 id="purchase-header"></h3>
                            <div id="purchases-graph" class="graph" style="width: 400px; height: 175px;overflow:hidden;float:left"></div>
                            <div id="purchases-legend" style="float:left"></div>
                        </div>
                        <div style="margin-bottom:50px;height:350px"><h3 id="units-header"></h3>
                            <div id="parts-graph" class="graph" style="width: 400px; height: 175px;overflow:hidden;float:left"></div>
                            <div id="parts-legend" style="float:left"></div>
                        </div>
                        <div style="margin-bottom:50px;height:350px"><h3 id="inspections-header"></h3>
                            <div id="inspections-graph" class="graph" style="width: 400px; height: 175px;overflow:hidden;float:left"></div>
                        </div>
                    </div>
                </div>
        </div>

</body>
</html>
