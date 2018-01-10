<%@ Page Title="Order History" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="CKOrders.aspx.vb" Inherits="Pigeon.CKOrders" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         var user = <%= Session("UserModel") %>;
         $('.tab-warranties').hide();
 </script>


<script id="NewTemp" type="text/html">
 

        <tr rowid = '${OrderID}' >
            <td>${OrderID}</td>
            <td>${DateOrdered}</td>
            <td>${Adjuster}</td>
            <td>${CustomerNo}</td>
            <td>${ContractNo}</td>
            <td>${AuthNo}</td>
            <td>${Owner}</td>
            <td>${VinNo}</td>
            <td>${Location}</td>
        </tr>

  
</script>


<script id="ArrivedTemp" type="text/html">
  

        <tr rowid = '${OrderID}'>
            <td>${OrderID}</td>
            <td>${DateOrdered}</td>
            <td>${Adjuster}</td>
            <td>${CustomerNo}</td>
            <td>${ContractNo}</td>
            <td>${AuthNo}</td>
            <td>${Owner}</td>
            <td>${VinNo}</td>
            <td>${Location}</td>
            <td>${Tracking}</td>
            <td>${Shipper}</td>
        </tr>

    
</script>

<script id="InRouteTemp" type="text/html">
  

        <tr rowid = '${OrderID}'>
            <td>${OrderID}</td>
            <td>${DateOrdered}</td>
            <td>${Adjuster}</td>
            <td>${CustomerNo}</td>
            <td>${ContractNo}</td>
            <td>${AuthNo}</td>
            <td>${Owner}</td>
            <td>${VinNo}</td>
            <td>${Location}</td>
            <td>${Tracking}</td>
            <td>${Shipper}</td>
            <td>${Status}</td>
        </tr>

   
</script>

<script id="OrderTemp" type="text/html">
  

        <tr>
            <td>${PartNo}</td>
            <td>${Description}</td>
            <td>${Quantity}</td>
            <td>${accounting.formatMoney(SellPrice)}</td>
            <td>${accounting.formatMoney(CorePrice)}</td>
            <td>${Warranty}</td>
            <td>${TrackingNo}</td>
            <td class ="orderdetailid" style="display:none">${OrderID}</td>   
        </tr>

   
</script>




<script type="text/javascript">
    var routetable
    var newtable
    var arrivedtable

    $('document').ready(function () {


        $("#orderDetails").dialog({
            modal: true,
            width: '700px',
            autoOpen: false,
            buttons: {
                Ok: function () {
                    $("#orderDetails").dialog("close");

                }
            }
        });

        $("#links").hide();
        $(".history").hide();

        $("#loading-overlay").show();


        GetNewHistory()

        $(".order-categories a").click(function () {
            
            $(".order-categories li.active").removeClass("active");

            $(this).parents('li').addClass("active");

           
        });


    });

    function GetNewHistory() {
        $('#loadingLabel').html('Loading..')
        var urlMethod = "../CkOrderHistroyWebService.asmx/GetHistory";

        var json = { 'client': user.Client,type: 'New', Username: user.UserName };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnNewHistory);
    }

    function ReturnNewHistory(msg) {
        var NewData = new Array();
        NewData = jQuery.parseJSON(msg.d);
        //console.log(NewData);
        $('#New table tbody').append($("#NewTemp").tmpl(NewData));
        newtable =  $('#tblnew').dataTable(
            {
                "oLanguage": {
                    "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries. To view more entries please scroll down"
                },
                "bScrollInfinite": true,
                "bScrollCollapse": true,
                "bJQueryUI": true,
                "bProcessing": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%"
            }
        );
        $('#loadingLabel').html('Loading....')
        GetInRouteHistory()
    
    }

    function GetInRouteHistory() {
    
        var urlMethod = "../CkOrderHistroyWebService.asmx/GetHistory";

        var json = { 'client': user.Client,type: 'Inroute', Username: user.UserName  };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnInRouteHistory);
    }

    function ReturnInRouteHistory(msg) {
        var InRouteData = new Array();
        InRouteData = jQuery.parseJSON(msg.d);
        // console.log(InRouteData);
        $('#InRoute table tbody').append($("#InRouteTemp").tmpl(InRouteData));
        routetable = $('#tblroute').dataTable(
            {
                "oLanguage": {
                    "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries. To view more entries please scroll down"
                },
                "bScrollInfinite": true,
                "bScrollCollapse": true,
                "bJQueryUI": true,
                "bProcessing": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%"
            }
        );
        GetArrivedHistory()
    }
    function GetArrivedHistory() {
      

        var urlMethod = "../CkOrderHistroyWebService.asmx/GetHistory";

        var json = { 'client': user.Client,type: 'Arrived', Username: user.UserName  };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnArrivedHistory);
    }

    function ReturnArrivedHistory(msg) {
        var ArrivedData
        ArrivedData = jQuery.parseJSON(msg.d);
        // console.log(ArrivedData);
        $('#Arrived table tbody').append($("#ArrivedTemp").tmpl(ArrivedData));

        arrivedtable = $('#tblArrived').dataTable(
           {
               "oLanguage": {
                   "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries. To view more entries please scroll down"
               },
               "aaSorting": [[0, "desc"]],
               "sPaginationType": "full_numbers", 
               "bPaginate": false,
               "bJQueryUI": true,
               "bProcessing": true,
               "sScrollY": "250",
               "sScrollX": "100%",
               "sScrollXInner": "110%"
           }
       );
        $("#links").buttonset();
        $("#loading-overlay").hide();
        $("#links").animate({
            opacity: 'toggle'
        },1000);
        $("#New").animate({
            opacity: 'toggle'
        },1000);
        newtable.fnAdjustColumnSizing();

        $("#btnNew").click(function () {
            $("#New").animate({
                opacity: 'toggle'
            },500);
            newtable.fnAdjustColumnSizing();
            $("#InRoute").hide();
            $("#Arrived").hide();
        });
        $("#btnInRoute").click(function () {
            $("#InRoute").animate({
                opacity: 'toggle'
            },500);
            routetable.fnAdjustColumnSizing();
            $("#New").hide();
            $("#Arrived").hide();
        });
        $("#btnArrived").click(function () {
            $("#Arrived").animate({
                opacity: 'toggle'
            },500);
            arrivedtable.fnAdjustColumnSizing();
            $("#InRoute").hide();
            $("#New").hide();
        });

        $(".histable tbody tr").live("click", function () {
            //         console.log($(this).attr('rowid'));
        

            GetOrder($(this).attr('rowid'))

        });

    }

    function GetOrder(rowID) {

        var urlMethod = "../CkOrderHistroyWebService.asmx/GetOrderDetails";

        var json = { 'client': user.Client, OrderID: rowID };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnOrder);
    }

    function ReturnOrder(msg) {
        var OrderData = new Array();
        OrderData = jQuery.parseJSON(msg.d);
        // console.log(OrderData);
        $('#orderDetails tbody').html("");
        $('#orderDetails tbody').append($("#OrderTemp").tmpl(OrderData));
        $("#orderDetails").dialog({ title: "Details for Order ID: " + $('.orderdetailid').html() });
        $('#orderDetails').dialog("open");
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


</script>

<div class="container-fluid">
    
    <div class="row-fluid">
        <div class="span12">
            <div id="loading-overlay">
                <img src="../../images/ajax-loader-blue.gif"/> <span id="loadingLabel">Loading</span> 
            </div>
       <div class="row-fluid">
            <div class="span12">
                <ul class="nav order-categories nav-pills">
                    <li class="active" data-type="New"><a id='btnNew' href="#">New Orders</a></li>
                    <li data-type="InRoute"><a id='btnInRoute' href="#">In Route Orders</a></li>
                    <li data-type="Arrived"><a id='btnArrived' href="#">Recently Arrived Orders</a></li>
                 </ul>
            </div>
        </div>
            <div id="New" class="history">
                 <table id="tblnew" class="histable">
                    <thead>
                    
                        <tr>
                            <td>Order ID</td>
                            <td>Date Ordered</td>
                            <td>Adjuster</td>
                            <td>Customer No.</td>
                            <td>Contract No.</td>
                            <td>Authorization No.</td>
                            <td>Auto Owner</td>
                            <td>Vin No.</td>
                            <td>Location</td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table> 
            </div>
            
            <div id="InRoute" class="history">
                <table id="tblroute" class="histable">
                <thead>
                   
                    <tr>
                        <td>Order ID</td>
                        <td>Date Ordered</td>
                        <td>Adjuster</td>
                        <td>Customer No.</td>
                        <td>Contract No.</td>
                        <td>Authorization No.</td>
                        <td>Auto Owner</td>
                        <td>Vin No.</td>
                        <td>Location</td>
                        <td>Tracking #</td>
                        <td>Shipping Company</td>
                        <td>Shipping Status</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>
            </table> 
            
            </div>
            <div id="Arrived" class="history">
            
            <table id="tblArrived" class="histable">
                <thead>
                    <tr>
                        <td>Order ID</td>
                        <td>Date Ordered</td>
                        <td>Adjuster</td>
                        <td>Customer No.</td>
                        <td>Contract No.</td>
                        <td>Authorization No.</td>
                        <td>Auto Owner</td>
                        <td>Vin No.</td>
                        <td>Location</td>
                        <td>Tracking #</td>
                        <td>Shipping Company</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>
            </table> 
            </div>
        </div>
    </div>
   
</div>

<div id="orderDetails">
    <table id="tblorder" class="table table-bordered table-striped table-condensed">
        <thead>
            <tr>
                <td>Part No.</td>
                <td>Part Description</td>
                <td>Quantity</td>
                <td>Sell Price</td>
                <td>Core Price</td>
                <td>Warranty</td>
                <td>Tracking No.</td>
            </tr>
        </thead>
        <tbody>
        </tbody>
        <tfoot>
        </tfoot>
    </table>

</div>
</asp:Content>