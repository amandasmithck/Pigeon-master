<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="IncomeExpense.aspx.vb" Inherits="Pigeon.IncomeExpense" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

     <script id="printTemplate" type="text/html">
        <tr>
           <td>${InvoiceType}</td>
           <td>${InvoiceNo}</td> 
           <td>${DateEntered}</td> 
           <td>${accounting.formatMoney(Amount)}</td> 
           <td>${accounting.formatMoney(AmountPaid)}</td>
           <td>${DatePaid}</td> 
           <td>${Company}</td> 
           <td style="text-decoration: underline;cursor: pointer" class='orderid'>${OrderID}</td>
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
            var data = { 'fromdate': $('#txtfromdate').val(), 'todate': $('#txttodate').val(), 'client': user.Client };
            var jsonData = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "../ReportsWebService.asmx/GetIncomeExpense",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var resp = jQuery.parseJSON(msg.d);

                    $('#print-table tr:not(:first)').remove();
                    $("#print-table").append($('#printTemplate').tmpl(resp))

                    $('#print-table').unloadmask();
                    $('#btnExcel').show();

                    $('.orderid').click(function (e) {
                        var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).html();
                        var win = window.open(url, '_blank');
                        win = null;
                        return false;
                    });
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
                <h3 id="viewing"></h3>
            
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
                    <th>Invoice Type</th>
                    <th>Invoice No</th>
                    <th>Date Entered</th>
                    <th>Amount</th>
                    <th>Amount Paid</th>
                    <th>Date Paid</th>
                    <th>Company</th>
                    <th>Order ID</th>
                </tr>
        
                </table>
                <input type="hidden" id="datatodisplay" name="datatodisplay"  />
                
        </form>
        </div>
            </div>
</div>

    </asp:Content>