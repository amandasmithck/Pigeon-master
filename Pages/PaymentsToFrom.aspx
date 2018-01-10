<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="PaymentsToFrom.aspx.vb" Inherits="Pigeon.PaymentsToFrom" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

     <script id="printTemplate" type="text/html">
        <tr>
            <td style="text-decoration: underline;cursor: pointer" class='orderid'>${OrderID}</td>
            <td>'${InvoiceNo}</td>
            <td>${InvoiceType}</td>
            <td>${Payer}</td>
            <td>${Payee}</td>
            <td>${accounting.formatMoney(Amount)}</td> 
            <td>${accounting.formatMoney(AmountPaid)}</td>
            <td>${DatePaid}</td>
            <td>${PaymentType}</td>
<%--            <td>${CheckNo}</td>
            <td>${CCDatePaid}</td>--%>
        </tr>
     </script>
    
    <script id="vendorTemplate" type="x-jquery-tmpl">
       <option value="${CompanyID}">${Company}</option>
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

            //get vendors
            var urlMethod = "../ReportsWebService.asmx/GetNonCustomersByParent";
            var json = {};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selVendor').append($("#vendorTemplate").tmpl(response));
                
            });

            

        });

        function GetData() {

            $('#print-table').loadmask("Getting Stats...");
            $('#btnExcel').hide();
            $('#viewing').html('Currently Viewing ' + $('#txtfromdate').val() + ' - ' + $('#txttodate').val())

            var data = { 'companyid': $('#selVendor').val(), 'fromdate': $('#txtfromdate').val(), 'todate': $('#txttodate').val(), 'client': user.Client };
            var jsonData = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "../ReportsWebService.asmx/GetPaymentsToFrom",
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
                          
  
                 </div>
                <div class="control-group">
                        <label class="control-label" for="modal-note">Vendor:</label>
                        <select  id="selVendor" ><option value=""></option></select>
                        <input id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small"</ >
  
                 </div>
               
                <input id="btnExcel" type="submit" value="Download Excel" class="btn btn-success btn-large" />
                <table name="print-table" id="print-table" class="table table-striped table-bordered">
                <tr>
                    <th>Order ID</th>
                    <th>Invoice No</th>
                    <th>Invoice Type</th>
                    <th>Payer</th>
                    <th>Payee</th>
                    <th>Amount</th>
                    <th>Amount Paid</th>
                    <th>Date Paid</th>
                    <th>Pay Type</th>
              <%--      <th>Check No</th>
                    <th>CC Date Paid</th>--%>
                 </tr>
        
                </table>
                <input type="hidden" id="datatodisplay" name="datatodisplay"  />
                
        </form>
        </div>
            </div>
</div>

    </asp:Content>
