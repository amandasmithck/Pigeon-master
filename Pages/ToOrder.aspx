<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="ToOrder.aspx.vb" Inherits="Pigeon.ToOrder" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <script id="resultsTemplate" type="x-jquery-tmpl">
        <tr>
            <td style="text-decoration: underline;cursor: pointer" class='orderid'>${OrderID}</td> 
           <td>${DateOrdered}</td>
           <td>${Company}</td>
           <td>${AutoYear} ${AutoMake} ${AutoModel}</td>
           <td>${Mileage}</td> 
           <td>${VinNo}</td>
        </tr>
     </script>
            <script id="PartresultsTemplate" type="x-jquery-tmpl">
        <tr>
          <%-- <td>${PartType}</td>--%>
          <%-- <td>${PartNo}</td>--%>
           <td>${PartDescription}</td>
           <td>${Quantity}</td>
           <td>${accounting.formatMoney(SellPrice)}</td> 
           <td>${Servicer}</td> 
           <td>${Address1} ${City},${State} ${Zip}</td>
           <td>${Phone}</td> 
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
            $('.input-append').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');



            var urlMethod = "../OrderWebService.asmx/GetToOrder";
            var json = { 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);

                $('.loader').remove();

                $('#details').dataTable().fnDestroy();
                $('#details tbody').find('tr').remove();

                $('#details thead tr').show();
                $.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");

                var dTable = $('#details').dataTable({
                    "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false, "bFilter": false
                });

                $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable


                

                $('.orderid').click(function (e) {
                    var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).html();
                    var win = window.open(url, '_blank');
                    win = null;
                    return false;
                });

                /* Add/remove class to a row when clicked on */
                $('#details tr').click(function () {
                    if ($(this).hasClass('row_selected')) {
                        $(this).removeClass('row_selected');
                    }
                    else {
                        $('#details tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                    }

                    //get part order
                    var orderid = $(this).find('td.orderid')[0].innerHTML
                    var urlMethod = "../OrderWebService.asmx/GetToOrderPart";
                    var json = { 'orderid': orderid, 'client': user.Client };
                    var jsonData = JSON.stringify(json);
                    SendAjax(urlMethod, jsonData, function (msg) {
                        response = jQuery.parseJSON(msg.d);
                        $('#part-details').dataTable().fnDestroy();
                        $('#part-details tbody').find('tr').remove();

                        $('#part-details thead tr').show();
                        $.tmpl($('#PartresultsTemplate'), response).appendTo("#part-details tbody");

                        var dTable2 = $('#part-details').dataTable({
                            "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false, "bFilter": false
                        });
                        $('#title').html(orderid);

                        $('#part-details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable

                    });
                });

            });
        });
    </script>

    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span5">
                <table class="table table-condensed" id="details">
                    <thead><tr style="display:none">
                        <th>Order ID</th>
                        <th>DateOrdered</th>
                        <th>Customer</th>
                        <th>Vehicle</th>
                        <th>Mileage</th>
                        <th>VIN</th>
                    </tr></thead>
                </table>
         
            </div>
            <div class="span7">
                <h2 id="title" style="text-align: center;"></h2>
                <table class="table table-condensed" id="part-details">
                    <thead><tr style="display:none">
                       <%-- <th>Part Type</th>--%>
                       <%-- <th>Part No.</th>--%>
                        <th>Part</th>
                        <th>Quantity</th>
                        <th>Sell Price</th>
                        <th>Servicer</th>
                        <th>Address</th>
                        <th>Phone</th>
                    </tr></thead>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
