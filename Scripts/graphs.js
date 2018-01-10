


//*********************start common functions**************************************//


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



//*********************end common functions**************************************//

//*********************start dashboard functions**************************************//
function GetGraphs(customerno, fromdate, todate) {
    GetTotalGPGraph(customerno, fromdate,todate)
    GetTotalPartsGraph(customerno, fromdate, todate)
    GetLaborPOCredGraph(customerno,fromdate, todate)

}
function dollarFormatter(v, axis) {
    return "$" + v.toFixed(axis.tickDecimals);
}

function showTooltip(x, y, contents) {
    $('<div id="tooltip" style="z-index:9999999999">' + contents + '</div>').css({
        position: 'absolute',
        display: 'none',
        top: y + 5,
        left: x + 5,
        border: '1px solid #fdd',
        padding: '2px',
        'background-color': '#fee',
        opacity: 0.80
    }).appendTo("body").fadeIn(200);
}

function TimestampToDate(timestamp) {
    
    var pubDate = new Date(timestamp);
    var newDate = new Date();
    newDate.setDate(pubDate.getDate() + 1);
    
    var weekday = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat");
    var monthname = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var formattedDate = weekday[newDate.getDay()] + ' '
                        + monthname[newDate.getMonth()] + ' '
                        + newDate.getDate() + ', ' + newDate.getFullYear()
    return formattedDate;
}

function GetTotalGPGraph(customerno, fromdate, todate) {
    var urlMethod = "../ReportsWebService.asmx/GetTotalGPGraph";
    var json = { 'customerno': customerno, 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function(msg) {
        data = jQuery.parseJSON(msg.d);
        var without = [];
        $(data).each(function () {
            var dataobj = []
            dataobj.push($(this)[0].DateOrdered, $(this)[0].Gross);
            without.push(dataobj);
        });

        var urlMethod = "../ReportsWebService.asmx/GetTotalPurchasesGraph";
        var json = { 'customerno': customerno, 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            data = jQuery.parseJSON(msg.d);
            var purchases = [];
            $(data).each(function () {
                var dataobj = []
                dataobj.push($(this)[0].DateOrdered, $(this)[0].Gross);
                purchases.push(dataobj);
            });


            $.plot($("#gp-graph"),
                       [{ data: without, label: "Gross Profit", yaxis: 2 }, { data: purchases, label: "Purchases" }],
                       {
                           xaxes: [{ mode: 'time'}],
                           yaxes: [{ min: 0, tickFormatter: dollarFormatter}, { min: 0, tickFormatter: dollarFormatter, position:"right" }],
                           legend: { position: 'sw' },
                             grid: { hoverable: true } 
                       });
            
            var previousPoint = null;
            $("#gp-graph").bind("plothover", function (event, pos, item) {
               
                    if (item) {
                        if (previousPoint != item.dataIndex) {
                            previousPoint = item.dataIndex;

                            $("#tooltip").remove();
                            var x = item.datapoint[0].toFixed(2),
                                y = item.datapoint[1].toFixed(2);

                            showTooltip(item.pageX, item.pageY,
                                       item.series.label + ": $" + accounting.formatMoney(item.datapoint[1], "", 2, "", ".") + " on " + TimestampToDate(item.datapoint[0]));
                        }
                    }
                    else {
                        $("#tooltip").remove();
                        previousPoint = null;
                    }
         
            });
            $('#gp-loader').hide();
        });

      });
}

function GetTotalPartsGraph(customerno, fromdate, todate) {

    var urlMethod = "../ReportsWebService.asmx/GetTotalPartsGraph";
    var json = { 'customerno': customerno, 'parttype': 'OEM', 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        dataoem = jQuery.parseJSON(msg.d);
        var oem = [];
        $(dataoem).each(function () {
            var oemobj = []
            oemobj.push($(this)[0].DateOrdered, $(this)[0].Parts);
            oem.push(oemobj);
        });
        
        var urlMethod = "../ReportsWebService.asmx/GetTotalPartsGraph";
        var json = { 'customerno': customerno, 'parttype': 'Aftermarket', 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            dataaftermarket = jQuery.parseJSON(msg.d);
            var aftermarket = [];
            $(dataaftermarket).each(function () {
                var aftermarketobj = []
                aftermarketobj.push($(this)[0].DateOrdered, $(this)[0].Parts);
                aftermarket.push(aftermarketobj);
            });

            var urlMethod = "../ReportsWebService.asmx/GetTotalPartsGraph";
            var json = { 'customerno': customerno, 'parttype': 'Small Parts', 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                datasmall = jQuery.parseJSON(msg.d);
                var small = [];
                $(datasmall).each(function () {
                    var smallobj = []
                    smallobj.push($(this)[0].DateOrdered, $(this)[0].Parts);
                    small.push(smallobj);
                });

                $.plot($("#parts-graph"), [{ data: oem, label: "OEM" }, { data: aftermarket, label: "Aftermarket" }, { data: small, label: "Small Parts" }],
                    
                    {
                        xaxis: { mode: 'time' },
                        series: {
                        stack: true,
                        lines: { show: false, fill: true, steps: false },
                        bars: { show: true, barWidth: 86400000 }
                        },
                        grid: { hoverable: true }
                    });
                var previousPoint = null;
                $("#parts-graph").bind("plothover", function (event, pos, item) {

                    if (item) {
                        //if (previousPoint != item.dataIndex) { //bar graph only hack
                            previousPoint = item.dataIndex;

                            $("#tooltip").remove();
                            var x = item.datapoint[0].toFixed(2),
                                y = item.datapoint[1].toFixed(2);

                            showTooltip(item.pageX, item.pageY,
                                       item.series.label + ": " + item.datapoint[1] + " on " + TimestampToDate(item.datapoint[0]));
                        //}
                    }
                    else {
                        $("#tooltip").remove();
                        previousPoint = null;
                    }

                });
                $('#parts-loader').hide();
            });

        });


    });


}

function GetLaborPOCredGraph(customerno, fromdate, todate) {
    var urlMethod = "../ReportsWebService.asmx/GetLaborPOGraph";
    var json = { 'customerno': customerno, 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        var po = [];
        $(data).each(function () {
            var dataobj = []
            dataobj.push($(this)[0].DateEntered, $(this)[0].Amount);
            po.push(dataobj);
        });

        var urlMethod = "../ReportsWebService.asmx/GetLaborCredGraph";
        var json = { 'customerno': customerno, 'fromdate': fromdate, 'todate': todate, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            data = jQuery.parseJSON(msg.d);
            var credit = [];
            $(data).each(function () {
                var dataobj = []
                dataobj.push($(this)[0].DateEntered, $(this)[0].Amount);
                credit.push(dataobj);
            });

            $.plot($("#labor-graph"),
                       [{ data: po, label: "Labor PO" }, { data: credit, label: "Labor Credit" }],
                       {
                           xaxes: [{ mode: 'time' }],
                           yaxes: [{ min: 0, tickFormatter: dollarFormatter }],
                           legend: { position: 'sw' },
                           grid: { hoverable: true }
                       });
            var previousPoint = null;
            $("#labor-graph").bind("plothover", function (event, pos, item) {

                if (item) {
                    if (previousPoint != item.dataIndex) {
                        previousPoint = item.dataIndex;

                        $("#tooltip").remove();
                        var x = item.datapoint[0].toFixed(2),
                            y = item.datapoint[1].toFixed(2);

                        showTooltip(item.pageX, item.pageY,
                                   item.series.label + ": $" + accounting.formatMoney(item.datapoint[1], "", 2, "", ".") + " on " + TimestampToDate(item.datapoint[0]));
                    }
                }
                else {
                    $("#tooltip").remove();
                    previousPoint = null;
                }

            });
        });
        $('#labor-loader').hide();

    });
}



//*********************end dashboard functions**************************************//
