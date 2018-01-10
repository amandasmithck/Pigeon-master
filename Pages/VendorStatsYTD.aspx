<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="VendorStatsYTD.aspx.vb" Inherits="Pigeon.VendorStatsYTD" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

     <script id="printTemplate" type="text/html">
        <tr>
           <td>${Company}</td> 
           <td>${Sold}</td> 
           <td>${accounting.formatMoney(Cost)}</td>   
           <td>${accounting.formatMoney(AvgCost)}</td> 
           <td>${accounting.formatMoney(Sale)}</td>> 
           <td>${accounting.formatMoney(AvgSale)}</td> 
           <td>${accounting.formatMoney(AvgProfit)}</td> 
        </tr>
     </script>


    <script language="javascript" type="text/javascript">
        $('document').ready(function () {
            $('.tab-warranties').hide();
            
            $('#btnExcel').hide();

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

            GetData()
            
        });

        function GetData() {

            $('#print-table').loadmask("Getting Stats...");
            $('#btnExcel').hide();
            if ($('#txtfromdate').val() == '' || $('#txttodate').val() == '') {
                var ytd = 'yes';
            } else {
                var ytd = 'no';
                $('#viewing').html('Currently Viewing ' + $('#txtfromdate').val() + ' - ' + $('#txttodate').val())
            }
            var data = { 'ytd': ytd, 'fromdate':$('#txtfromdate').val(), 'todate':$('#txttodate').val(), 'client': user.Client };
            var jsonData = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "../ReportsWebService.asmx/GetVendorStatsYTD",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var resp = jQuery.parseJSON(msg.d);

                    $('#print-table tr:not(:first)').remove();
                    $("#print-table").append($('#printTemplate').tmpl(resp))

                    $('#print-table').unloadmask();
                    $('#btnExcel').show();
                },
                error: function () {
                    $('#print-table').unloadmask();
                    alert("An error has occurred during processing your request.");
                }
            });
        }
    </script>

   

    </form>
<div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
     
        <form id="export-form" action="ExcelExport.aspx" method= "post"
        onsubmit='$("#datatodisplay").val( $("<div>").append( $("#print-table").eq(0).clone() ).html() )'>
                <h3 id="viewing">Currently Viewing YTD</h3>
            
                <div class="control-group">
                        <label class="control-label" for="modal-note">Date Range:</label>
                        <div class="controls">
                          <input type="text" id="txtfromdate" size="40" value="" >
                          <input type="text" id="txttodate" size="40" value="" >
                          <input id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small"</ >
  
                 </div>
               
                <input id="btnExcel" type="submit" value="Download Excel" class="btn btn-success btn-large" />
                <table name="print-table" id="print-table" class="table table-striped table-bordered">
                <tr>
                    <th>Company</th>
                    <th>Sold</th>
                    <th>Total Cost</th>
                    <th>Avg Cost</th>
                    <th>Total Sales</th>
                    <th>Avg Sales</th>
                    <th>Avg Profit </th>
                </tr>
        
                </table>
                <input type="hidden" id="datatodisplay" name="datatodisplay"  />
                <
        </form>
        </div>
            </div>
</div>

    </asp:Content>