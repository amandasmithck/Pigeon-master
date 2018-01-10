<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="SalesVolume.aspx.vb" Inherits="Pigeon.SalesVolume" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

     <script id="printTemplate" type="text/html">
        <tr>
            <td>${DateOrdered}</td>
            <td>${ContractNo}</td>
            <td>${PartDescription}</td>
            <td>${accounting.formatMoney(SellPrice)}</td></tr>
     </script>
    
    <script id="customerTemplate" type="x-jquery-tmpl">
       <option value="${CustNo}">${Company}</option>
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

            //get customers
            var urlMethod = "../CustomerManageWebService.asmx/GetCustomers";
            var json = {'client': user.Client};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selCustomer').append($("#customerTemplate").tmpl(response));

            });

        });

        function GetData() {

            $('#print-table').loadmask("Getting Stats...");
            $('#btnExcel').hide();
            $('#viewing').html('Currently Viewing ' + $('#txtfromdate').val() + ' - ' + $('#txttodate').val())

            var data = { 'customerno': $('#selCustomer').val(), 'fromdate': $('#txtfromdate').val(), 'todate': $('#txttodate').val(), 'client': user.Client };
            var jsonData = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "../ReportsWebService.asmx/GetSalesVolume",
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
                <h3 id="viewing"></h3>
            
                <div class="control-group">
                        <label class="control-label" for="modal-note">Date Range:</label>
                        <div class="controls">
                          <input type="text" id="txtfromdate" size="40" value="" >
                          <input type="text" id="txttodate" size="40" value="" >
                          
  
                 </div>
                <div class="control-group">
                        <label class="control-label" for="modal-note">Company:</label>
                        <select  id="selCustomer" ><option value=""></option></select>
                        <input id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small"</ >
  
                 </div>
               
                <input id="btnExcel" type="submit" value="Download Excel" class="btn btn-success btn-large" />
                <table name="print-table" id="print-table" class="table table-striped table-bordered">
                <tr>
                    <th>Date Ordered</th>
                    <th>Contract No</th>
                    <th>Part</th>
                    <th>Sell Price</th>
                 </tr>
        
                </table>
                <input type="hidden" id="datatodisplay" name="datatodisplay"  />
                
        </form>
        </div>
            </div>
</div>

    </asp:Content>