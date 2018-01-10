<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="Payables.aspx.vb" Inherits="Pigeon.Payables" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

        <script id="resultsTemplate" type="text/x-jquery-tmpl">
            
             <tr>
                   
                   <td style="text-decoration: underline;cursor: pointer" class='orderid' ref='${theOrderID}'>${theOrderID}<img class='check' style="display:none"  src="../images/Check-icon.png" width="12px" height="12px" /></td>
                  <td>${theInvoiceNo} </td> 
                  <td> <input type="text" class="invoice-no" style="width:130px" value="${theInvoiceNo}"/> </></td>
                   <td>${thePayer} </td>
                   <td>${theCompany} </td>             
                   <td>${theInvoiceType} </td>
                   <td>${theServicer} </td>
                   <td>${theVinNo} </td> 
                   <td>${accounting.formatMoney(theAmount,"", 2, "", ".")}</td>
                   <td> <input type="text" class="amount-paid" style="width:60px" value="${accounting.formatMoney(theAmountPaid,"", 2, "", ".")}"/> </> </td> 
                   <td> <input class="date-paid"type="text" style="width:60px" value="${theDatePaid}"/> </></td>
                   <td> <select style="width:85px" class="payment-type">
                            <option value="Amex-Rick" {{if thePaymentType == 'Amex-Rick'}} selected {{/if}}>Amex-Rick</option>
                            <option value="Amex-Chris" {{if thePaymentType == 'Amex-Chris'}} selected {{/if}}>Amex-Chris</option>
                            <option value="Amex-In" {{if thePaymentType == 'Amex-In'}} selected {{/if}}>Amex-In</option>
                            <option value="Check" {{if thePaymentType == 'Check' || thePaymentType == ''}} selected {{/if}}>Check</option>
                            <option value="Visa" {{if thePaymentType == 'Visa'}} selected {{/if}}>Visa</option>
                            <option value="Wire/ACH" {{if thePaymentType == 'Wire/ACH'}} selected {{/if}}>Wire/ACH</option>
                        </select>
                      <%-- ${thePaymentType}--%>

                   </td>
                   <td> <input class="check-no"type="text" style="width:60px" value="${theCheckNo}"/> </></td>
                   <td style='display:none;' class="invoice-id">${theInvoiceID} </td>
                   
                   
            </tr> 
        </script>

        <script id="companyTemplate" type="x-jquery-tmpl">
       <option value="${CompanyID}">${Company}</option>
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
            
            //get vendors
            var urlMethod = "../AccountingWebService.asmx/GetpayableCompanies";
            var json = {'client': user.Client};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selCompany').append($("#companyTemplate").tmpl(response));

            });

            $('#btnGo').click(function () {
                $('.control-group').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
                var urlMethod = "../AccountingWebService.asmx/GetPayable";
                var json = { 'filter': $('input[name=optCompany]:checked').attr("id"), 'companyid': $('#selCompany').val(), 'client': user.Client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    response = jQuery.parseJSON(msg.d);



                    $('#details').dataTable().fnDestroy();
                    $('#details tbody').find('tr').remove();

                    $('#details thead tr').show();
                    //$.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");
                    $("#resultsTemplate").tmpl(response).appendTo("#details tbody");

                    var dTable = $('#details').dataTable({
                        "aaSorting": [[0, "asc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false,
                    });
                    $("input[type=text]").click(function () {
                        $(this).select();
                    });
                    $(".date-paid").mask("99/99/99");
                    $('.date-paid').dblclick(function () {
                        var d = new Date();
                        var month = d.getMonth() + 1;
                        var day = d.getDate();
                        var output = (month < 10 ? '0' : '') + month + '/' + (day < 10 ? '0' : '') + day + '/' + d.getFullYear().toString().slice(2);
                        $(this).val(output);
                    });
                    $('.date-paid').keydown(function (e) {
                        if (e.keyCode == 40) {
                            var d = new Date();
                            var month = d.getMonth() + 1;
                            var day = d.getDate();
                            var output = (month < 10 ? '0' : '') + month + '/' + (day < 10 ? '0' : '') + day + '/' + d.getFullYear().toString().slice(2);
                            $(this).val(output);
                            return false;
                        }
                    });
                    $('.invoice-no').change(function () {
                        var editrow = $(this).closest('tr')[0];
                        var editinvoiceid = $(editrow).find('.invoice-id')[0].innerHTML;
                        var e = $(editrow).find('.payment-type')[0]
                        var editpaymenttype = e.options[e.selectedIndex].value;
                        UpdateInvoice('invoiceno', editinvoiceid, $(this).val(), editrow, editpaymenttype)
                    });
                    $('.amount-paid').change(function () {
                        var editrow = $(this).closest('tr')[0];
                        var editinvoiceid = $(editrow).find('.invoice-id')[0].innerHTML;
                        var e = $(editrow).find('.payment-type')[0]
                        var editpaymenttype = e.options[e.selectedIndex].value;
                        UpdateInvoice('amountpaid', editinvoiceid, $(this).val(), editrow, editpaymenttype)
                    });
                    $('.date-paid').change(function () {
                        var editrow = $(this).closest('tr')[0];
                        var editinvoiceid = $(editrow).find('.invoice-id')[0].innerHTML;
                        var e = $(editrow).find('.payment-type')[0]
                        var editpaymenttype=e.options[e.selectedIndex].value;
                        UpdateInvoice('datepaid', editinvoiceid, $(this).val(), editrow, editpaymenttype)
                    });
                    $('.check-no').change(function () {
                        var editrow = $(this).closest('tr')[0];
                        var editinvoiceid = $(editrow).find('.invoice-id')[0].innerHTML;
                        var e = $(editrow).find('.payment-type')[0]
                        var editpaymenttype = e.options[e.selectedIndex].value;
                        UpdateInvoice('checkno', editinvoiceid, $(this).val(), editrow, editpaymenttype)
                    });
                    $('.payment-type').change(function () {
                        var editrow = $(this).closest('tr')[0];
                        var editinvoiceid = $(editrow).find('.invoice-id')[0].innerHTML;
                        var e = $(editrow).find('.payment-type')[0]
                        var editpaymenttype = e.options[e.selectedIndex].value;
                        UpdateInvoice('paymenttype', editinvoiceid, $(this).val(), editrow, editpaymenttype)
                    });


                    $('.loader').remove();


                    $('.orderid').click(function (e) {
                        var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).attr('ref');
                        var win = window.open(url, '_blank');
                        win = null;
                        return false;
                    });



                });

                
            });
        });

        function UpdateInvoice(field, invoiceid, value, editrow, paymenttype) {
            //console.log(field + '-' + invoiceid + '-' + value);
            var urlMethod = "../AccountingWebService.asmx/UpdateInvoice";
            var json = { 'field': field, 'invoiceid': invoiceid, 'value': value, 'paymenttype': paymenttype, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $(editrow).find('.check')[0].style.display = 'inline';
            });
        }
    </script>

    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <div class="control-group">
                        
                        <label class="control-label" for="modal-note">Company:</label>
                        <select  id="selCompany" style="float: left;" ><option value=""></option></select>
                        
                        <div class="btn-group" data-toggle="buttons" style="float: left;margin-left:10px" >
                            <label class="btn btn-default">
                          <input type="radio"  name="optCompany" id="payer" value="Payer">Payer
                                </label>
                            <label class="btn btn-default">
                          <input type="radio" name="optCompany" id="payee" checked="checked" value="Payee">Payee
                                </label>
                        </div>
                        <input id="btnGo" type="button" value="Get Data" class="btn btn-primary " style="float: left;margin-left:30px" /></ >
  
                 </div>
               
                <table class="table table-condensed" id="details">
                    <thead><tr style='display:none;'> 
                    
                    <th>OrderID</th>
                    <th>Invoice No</th>
                    <th>Invoice No</th>
                    <th>Payer</th>             
                    <th>Payee</th>
                    <th>Invoice Type</th>
                    <th>Servicer</th>
                    <th>Vin No</th>
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