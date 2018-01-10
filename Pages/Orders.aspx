<%@ Page Title="Order History"  Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="Orders.aspx.vb" Inherits="Pigeon.Orders1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script id="orderTemplate" type="text/html">
        <tr>
            <td>${OrderID}</td>
            <td>${OrderDate}</td>
            <td>${User}</td>
            <td>${Customer}</td>
            <td>${Make}</td>
            <td>${TotalParts}</td>
            <td>${accounting.formatMoney(TotalSale)}</td>
            <td>${Notes}</td>
   
        </tr> 
    </script>
    <script id="orderDetailsTemplate" type="text/html">
        <tr>
            <td>${Part}</td>
            <td>${Description}</td>
            <td>${Quantity}</td>
            <%--<td>${ListPrice}</td>--%>
            <td>${accounting.formatMoney(TheirPrice)}</td>
            <td>${accounting.formatMoney(CorePrice)}</td>
        
        </tr>
         
    </script>
    <div class="container-fluid main-content">
        <div class="row-fluid">
            <div class="span12"><h3>View Order History</h3></div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <ul class="nav quote-categories nav-pills">
                    <li class="active" data-type="Engine"><a id='btnEngine' href="#">Engine</a></li>
                    <li data-type="Transmission"><a id='btnTrans' href="#">Transmission</a></li>
                    <li data-type="TransferCase"><a id='btnTransfer' href="#">Transfer Case</a></li>
                    <li data-type="Differential"><a id='btnDiff' href="#">Differential</a></li>
                    <li data-type="OEM"><a id='btnOEM' href="#">OEM</a></li>
                </ul>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <table id="details">
                    <thead>
                        <tr>
                            <th>
                                Order ID
                            </th>
                            <th>
                                Date
                            </th>
                            <th>
                                User
                            </th>
                            <th>
                                Customer
                            </th>
                            <th>
                                Make
                            </th>
                            <th>
                                Total Parts
                            </th>
                            <th>
                                Total Sale
                            </th>
                             <th>
                                Notes
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <table id="tblGoInroute" style="display: none;">
                    <thead>
                        <tr>
                            <th>
                                Invoice No.
                            </th>
                            <th>
                                Arrival Time
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                                Date Created
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <br />
                <br />
                <span id="label-details"></span>
                <table id="orderDetails" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>
                                Part
                            </th>
                            <th>
                                Description
                            </th>
                            <th>
                                Quantity
                            </th>
                            <%--<th>
                                List Price
                            </th>--%>
                            <th>
                                Sell Price
                            </th>
                            <th>
                                Core Price
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var clickid;
        var user = <%= Session("UserModel") %>;

        $('document').ready(function () {

            $('.tab-warranties').hide();

            if (user.Client == "Tracy") {
                $("#btnOEM").hide();
            }

            if (user.Client == "GO") {
                $("#links").html($("#links").html() + "<input type='radio' id='radio6' name='radio' /><label  id='btnGoInroute' for='radio6'>En Route</label>")
                $("#links").css('width', '492px');
            }

            $("#orderDetails").dialog({
                "bJQueryUI": true,
                width: '650px',
                autoOpen: false,
                buttons: {
                    Ok: function () {
                        $("#orderDetails").dialog("close");

                    }
                }
            });

            $(".quote-categories a").click(function () {
                $("#details").animate({
                    opacity: '0%'
                }, 500);

                $(".quote-categories li.active").removeClass("active");

                $(this).parents('li').addClass("active");

                GetOrders($(this).html());
            });

            $("#btnGoInroute").click(function () {
                $("#details").animate({
                    opacity: '0%'
                }, 500);

                GetGoInroute()
            });
            $("#loading-overlay").show();

            GetOrders("Engine");

            $("#details tbody tr").live("click", function () {
                clickid = $(event.target.parentNode).find('td:nth(0)').html()

                GetOrderDetails(clickid);
            });
        });

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

        function GetOrders(type) {
            //$("#loading-overlay").show();
            $("#links input").attr('disabled', true);
            var urlMethod = (user.Role === "customer") ? "../OEMWebService.asmx/GetOrdersCust" : "../OEMWebService.asmx/GetOrdersAdmin";

            var json = { username: user.UserName, 'client': user.Client, 'type': type };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, ReturnGetOrders);
        }

        var data = new Array();
        function ReturnGetOrders(msg) {
            data = jQuery.parseJSON(msg.d);

            $('#details').dataTable().fnDestroy();
            $('#details tbody').find('tr').remove();

            $('#tblGoInroute').dataTable().fnDestroy();
            $('#tblGoInroute tbody').find('tr').remove();
            $('#tblGoInroute').hide();

            $('#details tbody').append($("#orderTemplate").tmpl(data));
            $('#details').show();
            var oTable;
            oTable = $('#details').dataTable({
                "aoColumnDefs": [{ "asSorting": ["desc"], "aTargets": [1] }],
                "oLanguage": {
                    "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries. To view more entries please scroll down"
                },
                "bScrollInfinite": true,
                "bScrollCollapse": true,
                "bJQueryUI": true,
                "bProcessing": true
                /*"sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%"*/
            });

            oTable.fnAdjustColumnSizing();

            oTable.animate({
                opacity: '100%'
            }, 500);
        }

        function GetGoInroute() {
            $("#loading-overlay").show();
            $("#links input").attr('disabled', true);
            var urlMethod = "../OEMWebService.asmx/GetEliteOrders";

            var json = { username: user.UserName, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, ReturnGoInroute);
        }

        var GoInroutedata = new Array();
        var jsonconversion = new Array();
        function ReturnGoInroute(msg) {
            jsonconversion = jQuery.parseJSON(msg.d);
            GoInroutedata = jQuery.parseJSON(jsonconversion);

            $('#details').dataTable().fnDestroy();
            $('#details tbody').find('tr').remove();
            $('#details').hide();
            $('#tblGoInroute').dataTable().fnDestroy();
            $('#tblGoInroute tbody').find('tr').remove();
            $('#tblGoInroute').hide();
            $('#tblGoInroute tbody').append($("#GoInrouteTemp").tmpl(GoInroutedata.orderList));
            $('#tblGoInroute').show();

            var oTable;
            oTable = $('#tblGoInroute').dataTable({"aoColumnDefs": [{ "asSorting": ["desc"], "aTargets": [0] }],
                "oLanguage": {
                    "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries. To view more entries please scroll down"
                },
                "bScrollInfinite": true,
                "bScrollCollapse": true,
                "bJQueryUI": true,
                "bProcessing": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "100%"
            });
            //$('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable

            $("#loading-overlay").hide();

        }

        function GetOrderDetails(orderid) {

            var urlMethod = "../OEMWebService.asmx/GetOrderDetails";

            var json = { 'orderid': orderid, 'client': user.Client };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetOrderDetails);
        }

        var dataDetails = [];
        function ReturnGetOrderDetails(msg) {

            dataDetails = jQuery.parseJSON(msg.d);

            $('#orderDetails').dataTable().fnDestroy();
            $('#orderDetails tbody').find('tr').remove();

            $('#orderDetails tbody').append($("#orderDetailsTemplate").tmpl(dataDetails));
            $('#orderDetails').show();
            $("#orderDetails").dialog({ title: "Details for Order ID: " + clickid });
            $("#orderDetails").dialog("open");
        }
    </script>
</asp:Content>