<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="OrderTracking.aspx.vb" Inherits="Pigeon.OrderTracking" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

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
            var  OrderStatusViewModel=function() {
                orderInfo = ko.observable();
                parts = ko.observable();
                orderID = ko.observable();


                var pageLoad = function () {
            
                    var spinTarget = document.getElementById('pageBody')
                    spinner = new Spinner(spinnerOpts).spin(spinTarget);

                    var urldata = $.parseQuery();
                    var orderID = urldata.orderid

                    //get order info
                    var urlMethod = "../OrderTrackingWebService.asmx/GetOrderInfo";
                    var json = { 'orderid': orderID, 'client': user.Client };
                    var jsonData = JSON.stringify(json);
                    SendAjax(urlMethod, jsonData, function (msg) {
                        response = jQuery.parseJSON(msg.d);
                        orderInfo(response);

                        //get parts
                        var urlMethod = "../OrderTrackingWebService.asmx/GetParts";
                        var json = { 'orderid': orderID, 'client': user.Client };
                        var jsonData = JSON.stringify(json);
                        SendAjax(urlMethod, jsonData, function (msg) {
                            response = jQuery.parseJSON(msg.d);
                            parts(response);
                            $(".spinner").remove();
                        });
                    });

                    
                    
                    
                }

                pageLoad();

                return {
                    orderInfo: orderInfo
                };
            }
            ko.applyBindings(new OrderStatusViewModel());

        });
        
    </script>
    
    <div id="pageBody"
                <div id="OrderInfo" class="span12 main" style="margin-bottom:10px;width: 100% !important" data-bind="with: orderInfo">                       
                    <h3 id="OrderHeading">Order No. <span data-bind="text:OrderID"></span></h3>
                    <h4 data-bind="text: 'Date Ordered: ' + DateOrdered"></h4>
                    <h5 data-bind="text: 'Placed by: ' + AdjusterEmail"></h5>
                    <h5 data-bind="text: 'Purchase Order: ' + PurchaseOrder, visible: PigeonVisible"></h5>
                </div>
                
                <div class="span12 main" style="width: 100% !important" data-bind="with: orderInfo, visible: orderInfo.CKVisible">
                    <p data-bind="text: 'Company: ' + Company"></p> 
                    <p data-bind="text: 'Adjuster: ' + AdjusterName"></p>
                </div>
                <div class="span12 main" style="width: 100% !important" data-bind="with: orderInfo"> 
                    <p data-bind="text: 'Owner: ' + AutoOwner"></p>
                    <p data-bind="text: 'Vehicle: ' + Vehicle"></p>
                    <p data-bind="text: 'VIN: ' + VinNo + '&nbsp;&nbsp;Mileage: ' + Mileage"></p>
                </div>
                <div class="span12 main" style="width: 100% !important" data-bind="with: orderInfo, visible: orderInfo.CKVisible"> 
                    <p data-bind="text: 'Contract No.: ' + ContractNo + '&nbsp;&nbsp;Authorization No: ' + AuthorizationNo"></p>
                </div>
                <br/>
                       

                <div style="width:95%;margin-left:20px">
                     <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <td colspan = "7"><h3>Part(s) Ordered</h3></td>
                            </tr>
                            <tr>
                                <td style="font-size:12px">Part No.</td>
                                <td style="font-size:12px">Description</td>
                                <td style="font-size:12px;text-align:center">Price</td>  
                                <td style="font-size:12px;text-align:center">Quantity</td>
                                <td style="font-size:12px;text-align:center">Core Value</td> 
                                <td style="font-size:12px">Status</td>
                                <td style="font-size:12px;text-align:center">Expected<br />Ship Date</td>
                                <td style="font-size:12px;text-align:center">Expected<br />Delivery Date</td>
                                <td style="font-size:12px;text-align:center">Delivered</td>
                                <td style="font-size:12px">Tracking</td>
                            </tr>
                        </thead>
                        <tbody  data-bind="foreach: parts">
                             <tr>
                                 <td data-bind="text: PartNo"></td>
                                 <td>
                                     <p data-bind="text: PartDescription"></p>
                                     <span style="font-size:12px" data-bind="html: 'Ship To: ' + Shop"></span><br />
                                     <span style="font-size:12px" data-bind="text: CityStateZip"></span>
                                 </td>
                                 <td style="text-align:right" data-bind="text: '$' + Price"></td>
                                 <td style="text-align:center" data-bind="text: Quantity"></td>
                                 <td style="text-align:right" data-bind="text: '$' + Core"></td>
                                 <td data-bind="text: Status"></td>
                                 <td><span data-bind="text: ExpShipDate"></span></td>
                                 <td><span data-bind="text: ArrivalDate"></span></td>
                                 <td><span data-bind="text: ArriveDate"></span></td>
                                 
                                 <td>
                                     <p data-bind="text: ShipperName"></p>
                                     <span data-bind="html: Tracking"></span>
                                 </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tfoot>
                     </table>
                </div>
              <%-- <div class="span12 main">
                    <a  id ="btnFinalize" class="btn btn-primary btn-large">Finalize</a>
                </div>--%>
     </div>          

</asp:Content>

