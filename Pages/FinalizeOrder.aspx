<%@ Page Title="Finalize Order"  Language="vb" MasterPageFile="~/Pages/Pigeon.Master" AutoEventWireup="false" CodeBehind="FinalizeOrder.aspx.vb" Inherits="Pigeon.FinalizeOrder" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    
    <script id="OrderTemplate" type="text/html">


                <div id="OrderInfo" class="span12 main">                       
                    <h3 id="OrderHeading">Finalize Order</h3>
                      <h4>Order Details</h4>
                </div>
                <div class="span3 main"> 
                    <p><b>Company: ${Company}</b></p>
                    <p>Address: ${CompanyAddress}</p>
                    <p>User: ${Username}</p>
                    <p>Phone: ${Phone}</p>
                </div>
                <div class="span3 main"> 
                    <p><b>Repair Facility: ${RepairFacility}</b></p>
                    <p>Contact: ${Contact}</p>
                    <p>Address: ${Address} ${City},${State} ${Zip}</p>
                    <p>Phone: ${Phone}</p>
                </div>
                <div class="span3 main"> 
                    <p><b>Customer Last Name: ${OwnerName}</b></p>
                    <p>PO: ${PO}</p>
                    <p>Vehicle: ${Year} ${Make} ${Model}</p>
                    <p>VIN: ${VIN}</p>
                    <p>Mileage: ${Mileage} </p>  
                </div>
                <div class="span12 main">
                    <p><b>Notes: ${Notes}</b></p>
                </div> 
                       

                <div class="span12 main">
                     <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <td colspan = "5"><h3>Part Details</h3></td>
                                <td style="text-align:right;"><h4>Waranty:</h4>${Warranty}</td>
                            </tr>
                            <tr>
                                <td><h4>Part No.</h4></td>
                                <td><h4>Warehouse Stock</h4></td>
                                <td class="attention"><h4>Shipping</h4></td>  
                                <td class="attention"><h4>Core Pick-up</h4></td>
                                <td><h4>Core Price</h4></td> 
                                <td class="total"><h4>Sell Price</h4></td>
                            </tr>
                        </thead>
                        <tbody>
                        {{each Parts}}
                             <tr>
                                <td>${PartNumber}</td>
                                 {{if LocalStock < 1}}
                                <td style="color:red; font-weight:bold;">Out of Stock</td>
                                {{/if}}
                                {{if LocalStock >= 1}}
                                <td style="color:green; font-weight:bold;">${LocalStock}</td>
                                {{/if}}
                                <td class="attention form">
                                    <u>Transit time Estimated to Customer: ${CustomerTransit} Days</u><br />
                                    <form id="ship">
                                        {{if LocalStock < 1}}
                                            <input type="radio" name="ship" value="default" disabled="disabled"/><span style="color:gray;">Ship from Your Warehouse: Not Available</span><br />
                                            <input type="radio" name="ship" value="CK" checked="checked"/>Ship from C&K's Warehouse: Estimated ${parseInt(CKTransit)} Days
                                            <p><span id="shipCKPOLabel">PO # </span>
                                            <input id="shipCKPO" type="text" /></p>
                                        {{/if}}
                                        {{if LocalStock >= 1}}
                                            <input id="shipWarehouse" type="radio" name="ship" value="default"/>Ship from Your Warehouse: Estimated ${parseInt(ClientTransit)} Days<br />
                                            <span id="shipSeriallabel">Serial # </span><select id="shipSerial">
                                                {{each AvaliableSN}}
                                                    <option>${SerialNumber}</option>
                                                {{/each}}
                                            </select><br />
                                            <input id="shipCK" type="radio" name="ship" value="CK" />Ship from C&K's Warehouse: Estimated ${parseInt(CKTransit)} Days
                                            <p><span id="shipCKPOLabel">PO # </span>
                                            <input id="shipCKPO" type="text" /></p>
                                        {{/if}}
                                    </form>
                                </td>
                                <td class="attention form">
                                    <form id="core" style="text-align:left;">
                                        <br />
                                            <input type="radio" name="core" value="default" />Your Company<br />
                                            <input type="radio" name="core" value="CK"/>C&K
                                    </form>
                                </td>
                                <td>${accounting.formatMoney(CorePrice)}</td>
                            
                            
                            
                                <td class="selprice total">${accounting.formatMoney(SalePrice)}</td>
                            </tr>
                            {{/each}}
                        </tbody>
                        <tfoot>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td id = "TotalPrice" class="total"></td>
                            </tr>
                        </tfoot>
                     </table>
                </div>
               <div class="span12 main">
                    <a  id ="btnFinalize" class="btn btn-primary btn-large">Finalize</a>
                </div>
               

    </script>

     
    <script type="text/javascript">
        var user = <%= Session("UserModel") %>;
        var urldata = $.parseQuery();
        var OrderID = urldata.orderid
       
        $(document).ready(function () {

            $('.tab-warranties').hide();

            $('#orderDetails').html('<div class="loader"><img src="/images/ajax-loader-blue.gif" /> <p>Loading Order ID</p></div>');

        
            GetOrder(OrderID, user.Client);
           


            $('#links-table tbody tr').each(function () {
                $(this).hide();
            });

            $('.nav a').each(function () {
                var nav = $(this).text();

                $('#links-table tbody tr').each(function () {
                    var dir = $(this).attr('id');
                    $(this).attr('hoverid', hoverid)
                    //alert("1 DIR =" + dir + " NAV =" + nav);
                    if (nav == dir) {
                        //alert("2 DIR =" + dir + " NAV =" + nav);
                        $(this).show();
                    }
                });

            });

            $('.link-desc').hide();
            var hoverid = 0;
            $('#links-table tbody tr td.link-desc').each(function () {
                $(this).attr('id', "linkhover" + hoverid)
                hoverid = hoverid + 1
            });

            hoverid = 0;
            $('#links-table tbody tr').each(function () {
                $(this).attr('hoverid', hoverid)
                hoverid = hoverid + 1
            });

            var linkdesc;
            $('#links-table tbody tr').mouseover(function () {
                linkdesc = $('#linkhover' + $(this).attr('hoverid')).html()
                showCursorMessage(linkdesc);
            });
            $('#links-table tbody tr').mouseout($.hideCursorMessage);
        });

        function showCursorMessage(linkdesc) {
            $.cursorMessage(linkdesc, { offsetX: 20 });
        }

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

        function GetOrder(OrderID, Client) {

            var urlMethod = "../PigeonWebService.asmx/GetOrderDetails";
            var data = {
                'orderid': OrderID
                       , 'client': user.Client
            };
            var jsonData = JSON.stringify(data);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);


                $("#OrderTemplate").tmpl(response)
                .appendTo("#orderDetails");



                $('#TotalPrice').html("0");

                $(".selprice").each(function () {
                    $('#TotalPrice').html($('#TotalPrice').html() + $(this).html());
                });

                $('#TotalPrice').html("Total: " + accounting.formatMoney($('#TotalPrice').html()));

                $("#shipWarehouse").change(function () {
                    $("#shipSerial").show();
                    $("#shipSeriallabel").show();
                    $("#shipCKPOLabel").hide();
                    $("#shipCKPO").hide();

                });

                $("#shipCK").change(function () {
                    $("#shipSerial").hide();
                    $("#shipSeriallabel").hide();
                    $("#shipCKPOLabel").show();
                    $("#shipCKPO").show();
                });

                $("#shipSerial").hide();
                $("#shipSeriallabel").hide();
                $('.loader').hide();

                $("#btnFinalize").click(function () {

                    var serial
                    if ($("input[name=ship]:checked").val() == "CK") {
                        serial = "null";
                    } else {
                        serial = $('#shipSerial').val();
                    }

                    var ship
                    if ($("input[name=ship]:checked").val() != "CK") {
                        ship = user.Client
                    } else {
                        ship = "CK"
                    }

                    var po
                    if ($("input[name=ship]:checked").val() == "CK" && $("#shipCKPO").val() == "") {
                        alert("Please enter your PO#");
                        return false
                    } else {
                        po=$("#shipCKPO").val();
                    }

                    var core
                    if ($("input[name=core]:checked").val() != "CK") {
                        core = user.Client
                    } else {
                        core = "CK"
                    }

                    if ($("input[name=ship]:checked").val() == undefined) {
                        $("#ship").css("color", "red")
                        alert("Please select vendor for Shipping");
                        return false
                    } else {
                        $("#ship").css("color", "black")
                    }
                    if ($("input[name=core]:checked").val() == undefined) {
                        $("#core").css("color", "red")
                        alert("Please select vendor for Core Pickup");
                        return false
                    } else {
                        $("#core").css("color", "black")
                    }
                    confirmbox(OrderID, serial, ship, core, po, user.Client);

                });

            });
        }

        

        function confirmbox(OrderID, serial, ship, core, po, Client) {
            if (serial == "null") {
                var r = confirm("Based on your Selections Order #" + OrderID + " will be:" + '\n' + "Shipped by: " + ship + '\n' + "Core Pickup by: " + core  + '\n' + "PO: " + po)
            } else {
                var r = confirm("Based on your Selections Order #" + OrderID + " will be:" + '\n' + "Shipped by: " + ship + '\n' + "Serial #: " + serial + '\n' + "Core Pickup by: " + core)
            }


            if (r == true) {
                FinalizeOrder(OrderID, Client, $("input[name=ship]:checked").val(), serial, $("input[name=core]:checked").val(),po)
            }
            else {
                return false
            }
        }

        function FinalizeOrder(OrderID, Client, ShipFrom, SN, CorePickup,po) {
            var urlMethod = "../PigeonWebService.asmx/FinalizeOrder";

            var data = {
                'orderid': OrderID
                       , 'client': Client
                       , 'shipfrom': ShipFrom
                       , 'corepickup': CorePickup
                       , 'sn': SN
                       , 'po': po
                       , 'name': user.UserName

            };

            var jsonData = JSON.stringify(data);
            SendAjax(urlMethod, jsonData, function (msg) {
                response2 = jQuery.parseJSON(msg.d);
                //console.log(response2);
                if (response2 = 'true') {
                    if (ShipFrom != "CK") {
                        $('#dialog-message').html('<p>Order #' + OrderID + ' is now complete. Please pull serial number ' + SN + ' from your warehouse and ship to the customer.</p>')
                        showDialog()
                        $('#btnFinalize').hide();
                        $('#OrderHeading').html('Finalized Order');
                        $('#OrderHeading').css('color', 'green');
                        $('input').attr('disabled', 'disabled');
                        $('select').attr('disabled', 'disabled')
                    } else {
                        $('#dialog-message').html('<p>Order #' + OrderID + ' is now complete. C&K has successfully received your order and will process it immediately. A C&K representative will be in contact if there are any issues.</p>')
                        showDialog()
                        $('#btnFinalize').hide();
                        $('#OrderHeading').html('Finalized Order');
                        $('#OrderHeading').css('color', 'green');
                        $('input').attr('disabled', 'disabled');
                        $('select').attr('disabled', 'disabled')
                    }
                } else {
                    $('#dialog-message').html('<p>There was an error processing Order #' + OrderID + ' please try again. If you are still unable to complete the order, send a screenshot of this page to C&K.</p>');
                    showDialog()
                }

            });
        }


        

        function showDialog() {
            $("#dialog-message").dialog({
                autoOpen: true,
                modal: true,
                buttons: {
                    Ok: function () {
                        $("#dialog-message").dialog("close");
                        return false
                    }
                }
            });
        }

    </script>
    <div class="container-fluid">
        <div class="row-fluid" id="orderDetails">
           
         </div>        
    </div>
    <div id="dialog-message"></div>
</asp:Content>