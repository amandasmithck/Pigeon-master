<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="ChecksBalances.aspx.vb" Inherits="Pigeon.ChecksBalances" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

        <script id="resultsTemplate" type="text/x-jquery-tmpl">
           <tr>
                   
                   <td style="text-decoration: underline;cursor: pointer" class='orderid' ref='${theOrderID}'>${theOrderID}<img class='check' style="display:none"  src="../images/Check-icon.png" width="12px" height="12px" /></td>
                   <td>${theInvoiceNo}</td>
                   <td>${theDateEntered}</td>
                   <td>${thePayer} </td>
                   <td>${theInvoiceType} </td>
                   <td>${theContractNo} </td>
                   <td>${theAuthNo} </td> 
                   <td>${accounting.formatMoney(theAmount,"", 2, "", ".")}</td>
                   <td>${accounting.formatMoney(theAmountPaid,"", 2, "", ".")}</td> 
                   <td>${theDatePaid}</td>
                   <td>${thePaymentType}</td>
                   <td>${theCheckNo}</td>
                   <td style='display:none;' class="invoice-id">${theInvoiceID} </td>
                   
                   
            </tr> 
        </script>

    <script type="text/javascript">


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
                    //alert(err.Message);
                }
            });
        };

        $('document').ready(function () {
            $('.tab-warranties').hide();

            //get type 1
            $('#header1').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
            var urlMethod = "../AccountingWebService.asmx/GetChecksBalances";
            var json = { 'type': 1, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                

                $('#details1').dataTable().fnDestroy();
                $('#details1 tbody').find('tr').remove();

                $('#details1 thead tr').show();
                //$.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");
                $("#resultsTemplate").tmpl(response).appendTo("#details1 tbody");

                var dTable = $('#details1').dataTable({
                    "aaSorting": [[0, "asc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false,
                });

                $('#header1 .loader').remove();


                $('#details1 .orderid').click(function (e) {
                    var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).attr('ref');
                    var win = window.open(url, '_blank', 'width=975,height=730,toolbar=no,status=no,scrollbars=yes,resizable=yes,location=no,menu=no,directories=no,top=auto');
                    win = null;
                    return false;
                });

            });

            //get type 2
            $('#header2').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
            var urlMethod = "../AccountingWebService.asmx/GetChecksBalances";
            var json = { 'type': 2, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);


                $('#details2').dataTable().fnDestroy();
                $('#details2 tbody').find('tr').remove();

                $('#details2 thead tr').show();
                //$.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");
                $("#resultsTemplate").tmpl(response).appendTo("#details2 tbody");

                var dTable = $('#details2').dataTable({
                    "aaSorting": [[0, "asc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false,
                });

                $('#header2 .loader').remove();


                $('#details2 .orderid').click(function (e) {
                    var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).attr('ref');
                    var win = window.open(url, '_blank');
                    win = null;
                    return false;
                });

            });

         
        });

 
    </script>

    <div class="container-fluid">
        <div class="row-fluid" style="height:80px">
            <ul>
                <li><a href="#1">All receivable invoice types closed at any date with Amount Paid = 0 and Amount > 0</a></li>
                <li><a href="#2">All receivable invoice types Date Paid before Date Entered</a></li>
            </ul>
        </div>
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px">
            <div class="span12">
                <div class="control-group">
                   <a name="1"><h4 id="header1">All receivable invoice types closed at any date with Amount Paid = 0 and Amount > 0</h4></a>
                </div>
                <table class="table table-condensed" id="details1">
                    <thead><tr style='display:none;'> 
                    
                    <th>OrderID</th>
                    <th>Invoice No</th>
                    <th>Date Entered</th>
                    <th>Payer</th>             
                    <th>Invoice Type</th>
                    <th>Contract No</th>
                    <th>Auth No</th>
                    <th>Amount</th>
                    <th>Amound Paid</th> 
                    <th>Date Paid</th>
                    <th>Type</th> 
                    <th>Check No</th> 
                    <th style='display:none;'>InvoiceID</th>
 
                </tr></thead><tbody></tbody>
                </table>
            </div>
        </div>
        
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <div class="control-group">
                   <a name="2"><h4 id="header2">All receivable invoice types Date Paid before Date Entered</h4></a>
                </div>
                <table class="table table-condensed" id="details2">
                    <thead><tr style='display:none;'> 
                    
                    <th>OrderID</th>
                    <th>Invoice No</th>
                    <th>Date Entered</th>
                    <th>Payer</th>             
                    <th>Invoice Type</th>
                    <th>Contract No</th>
                    <th>Auth No</th>
                    <th>Amount</th>
                    <th>Amound Paid</th> 
                    <th>Date Paid</th>
                    <th>Type</th> 
                    <th>Check No</th> 
                    <th style='display:none;'>InvoiceID</th>
 
                </tr></thead><tbody></tbody>
                </table>
           </div>
        </div>
    </div>

</asp:Content>