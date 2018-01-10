<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="CustomerRelat.aspx.vb" Inherits="Pigeon.CustomerRelat" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery.flot.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery.flot.stack.js"></script>
    <script id="afterTemplate" type="text/html">
        <tr>
            <td style="width:210px"><span style="margin-left:25px;">${PartType}</span></td>
            <td style="text-align:center; width:125px !important;">${Units}</td>
            <td style="text-align:right; width:100px !important;">${accounting.formatMoney(Amount)}</td>
            <td style="text-align:right; width:100px !important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
    </script>
     <script id="monthTemplate" type="text/html">
         
         <table style="width:550px" class="table table-striped">
            <tr>
                <th style="width:210px">${MonthName}</th>
                <th style="text-align:center; text-decoration:underline; width:125px !important">Units</th>
                <th style="text-align:right; text-decoration:underline; width:100px !important">$</th>
                <th style="text-align:right; text-decoration:underline; width:100px  !important">Inspections</th>
            </tr>
            <tr style="font-weight:bold">
                <td style="width:210px">Aftermarket</td>
                <td style="text-align:center; width:125px !important">${AftermarketUnits}</td>
                <td style="text-align:right; width:100px !important">${accounting.formatMoney(AftermarketAmount)}</td>
                <td style="text-align:right; text-decoration:underline; width:100px  !important"></td>
            </tr>
         </table>
         <table id="${OrderMonth}-${OrderYear}"  style="width:550px" class="table table-striped">

         </table>
         <table style="margin-bottom:15px;width:550px" class="table table-striped ">
            <tr>
                <th style="width:210px">OEM</th>
                <th style="text-align:center; width:125px !important">${OEMUnits}</th>
                <th style="text-align:right; width:100px !important">${accounting.formatMoney(OEMAmount)}</th>
                <th style="text-align:right; text-decoration:underline; width:100px  !important"></th>
            </tr>
            <tr style="font-weight:bold">
                <td style="width:210px">Small Parts</td>
                <td style="text-align:center; width:125px !important">${SmallUnits}</td>
                <td style="text-align:right; width:100px !important">${accounting.formatMoney(SmallAmount)}</td>
                <td style="text-align:right; text-decoration:underline; width:100px  !important"></td>
            </tr>
        </table>
         <table style="margin-bottom:100px;width:550px" class="table table-striped ">
            <tr style="font-weight:bold">
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
        $('document').ready(function () {
            $('.tab-warranties').hide();

            $('#btnPrint').hide();

            $('#txtfromdate').datepicker()
              .on('changeDate', function (ev) {
                  $('#txtfromdate').datepicker('hide');
              });

            $('#txttodate').datepicker()
              .on('changeDate', function (ev) {
                  $('#txttodate').datepicker('hide');
              });

            $('#btnGo').click(function () {

                GetData();
            });

            $('#btnPrint').click(function () {
                window.open('CustomerRelatPrint.aspx?customerno=' + $('#selCustomer').val() + '&fromdate=' + $('#txtfromdate').val() + '&todate=' + $('#txttodate').val());
            });

            //get customers
            var urlMethod = "../CustomerManageWebService.asmx/GetCustomers";
            var json = { 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selCustomer').append($("#customerTemplate").tmpl(response));
               
            });

        });

        function GetData() {
            
            $('#months').children().remove();
            $('#months').loadmask("Sit back and relax while I do some magic...");
            $('#btnExcel').hide();
            $('#viewing').html('Currently Viewing ' + $('#txtfromdate').val() + ' - ' + $('#txttodate').val())

            var data = { 'customerno': $('#selCustomer').val(), 'fromdate': $('#txtfromdate').val(), 'todate': $('#txttodate').val(), 'client': user.Client };
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
                    $('#btnPrint').show();
                    $('#title').show();
                },
                error: function () {
                    $('#months').unloadmask();
                    alert("An error has occurred during processing your request.");
                }
            });
        }
    </script>

   

    </form>
    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em; overflow: visible; height: 80px;">
            <div class="span12">

                <h3 id="viewing"></h3>

                <div class="control-group">
                    <label class="control-label" for="modal-note">Date Range:</label>
                    <div class="controls">
                        <input type="text" id="txtfromdate" size="40" value="">
                        <input type="text" id="txttodate" size="40" value="">
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="modal-note">Company:</label>
                        <select id="selCustomer">
                            <option value=""></option>
                        </select>
                        <input  id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small" />

                    </div>

                    <input id="btnPrint" type="submit" value="Print" class="btn btn-success btn-large" />
                    

                </div>
            </div>
        </div>

        <div class="row-fluid" style="margin-bottom: 2em; overflow: visible; height: 80px;">
            <div class="span6">
                <h5 id="title" style="display:none">Customer Relationship Summary</h5>
                <h5 id="company"></h5><br />
                <div id="months">
                    
                </div>
            </div>
            <div class="span6">
                    <div id="graphs">
                        <div style="margin-bottom:50px;height:350px">
                            <h3 id="purchase-header"></h3>
                            <div id="purchases-graph" class="graph" style="width: 500px; height: 250px;overflow:hidden;float:left"></div>
                            <div id="purchases-legend" style="float:left"></div>
                        </div>
                        <div style="margin-bottom:50px;height:350px">
                            <h3 id="units-header"></h3>
                            <div id="parts-graph" class="graph" style="width: 500px; height: 250px;overflow:hidden;float:left"></div>
                            <div id="parts-legend" style="float:left"></div>
                        </div>
                        
                        <div style="margin-bottom:50px;height:350px">
                            <h3 id="inspections-header"></h3>
                            <div id="inspections-graph" class="graph" style="width: 500px; height: 250px;overflow:hidden;float:left"></div>
                        </div>
                    </div>
                
            </div>
            
        </div>

       
    </div>
    </asp:Content>