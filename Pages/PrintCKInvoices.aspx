<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="PrintCKInvoices.aspx.vb" Inherits="Pigeon.PrintCKInvoices" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

     <script id="printTemplate" type="text/html">
        <tr>
            <td>${DateOrdered}</td>
            <td>${ContractNo}</td>
            <td>${PartDescription}</td>
            <td>${accounting.formatMoney(SellPrice)}</td></tr>
     </script>
    
    <script id="customerTemplate" type="x-jquery-tmpl">
       <option value="${CompanyID}">${Company}</option>
    </script>


    <script language="javascript" type="text/javascript">
        $('document').ready(function () {
            $('.tab-warranties').hide();

            $('#btnExcel').hide();

            $('#txtfromdate').datepicker()
              .on('changeDate', function (ev) {
                  $('#txtfromdate').datepicker('hide');
                  if ($('#txttodate').val() != "") {
                      GetPrintCKCustomers();
                  }
              });

            $('#txttodate').datepicker()
              .on('changeDate', function (ev) {
                  $('#txttodate').datepicker('hide');
                  if ($('#txtfromdate').val() != "") {
                      GetPrintCKCustomers();
                  }
              });

            $('#btnGo').click(function () {

                GetData();
            });

            $('#loader').hide();

        });

        function GetPrintCKCustomers() {
            $('#loader').show();
            var urlMethod = "../AccountingWebService.asmx/GetPrintCKCustomers";
            var json = { 'fromdate': $('#txtfromdate').val(), 'todate': $('#txttodate').val(), 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selCustomer option').remove();
                $('#selCustomer').append($("#customerTemplate").tmpl(response));
                $('#loader').hide();
            });
        }

        function GetData() {
            ViewInvoice($('#txtfromdate').val(),$('#txttodate').val(),$('#selCustomer').val())
        }

        function ViewInvoice(from, to, companyid) {

            var url = "ViewInvoice.aspx?from=" + from + "&to=" + to + "&companyid=" + companyid;
            var win = window.open(url, '_blank', 'width=800,height=800,toolbar=no,status=no,scrollbars=yes,resizable=yes,location=no,menu=no,directories=no,top=auto');
            win = null;
            return false;

        }
    </script>

   

    </form>
<div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
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
                        </select><img id="loader" src="/images/ajax-loader-blue.gif" />
                        <input id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small">
                    </div>

                    <input id="btnExcel" type="submit" value="Download Excel" class="btn btn-success btn-large">
                    <table name="print-table" id="print-table" class="table table-striped table-bordered">
                     </table>
                </div>
            </div>
    </div>

</asp:Content>