<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="Search.aspx.vb" Inherits="Pigeon.Search" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script id="resultsTemplate" type="x-jquery-tmpl">
        {{if Cancelled=='True'}}
         <tr style="color:red">
             {{else}}
         <tr>
       {{/if}}
        
           <td style="text-decoration: underline;cursor: pointer" class='orderid'>${OrderID}</td> 
           <td>${Company}</td> 
           <td>${DateOrdered}</td>   
           <td>${Servicer}</td> 
           <td>${AutoYear}</td> 
           <td>${AutoMake}</td> 
           <td>${AutoModel}</td> 
           <td>${VinNo}</td>
           <td>${PartDescription}</td>
           <td>${Warehouse}</td>
           <td>${accounting.formatMoney(SellPrice)}</td> 
           <td>${accounting.formatMoney(CostPrice)}</td> 
           <td>${ActiveProblemStatus}</td> 
           <td>${Note}</td> 
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

            $("input").bind("keydown", function (event) {
                // track enter key
                var keycode = (event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode));
                if (keycode == 13) { // keycode for enter key
                    // force the 'Enter Key' to implicitly click the Update button
                    document.getElementById('OrderID').click();
                    return false;
                } else {
                    return true;
                }
            }); // end of function

            $('.tab-warranties').hide();

            $('.search-btn').click(function (e) {
                if ($('#search').val()) {
                    GetOrders2($(this).attr('id'))
                }
            });

            $('#specific').change(function (e) {

                if ($('#search').val()) {
                    GetOrders2($(this).val())
                    $('#Specific-btn').show();
                }
            });

            $('#Specific-btn').click(function (e) {
                if ($('#search').val()) {
                    GetOrders2($('#specific').val())
                }
            });



        });

        function GetOrders(searchwhere) {
            $('#results-table').show();
            $('#results-table').loadmask("Searching orders...");
            $('#results-table tr:not(:first)').remove();
            var urlMethod = "../OrderWebService.asmx/SearchOrders";
            var json = { 'search': $('#search').val(), 'searchwhere': searchwhere, client: user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                //$('#results-table tbody').append($('#resultsTemplate')).tmpl(response);
                $.tmpl($('#resultsTemplate'), response).appendTo("#results-table tbody");

                $('.orderid').click(function (e) {
                    var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).html();
                    var win = window.open(url, '_blank');
                    win = null;
                    return false;
                });
                $('#results-table').unloadmask();
            });
        }

        function GetOrders2(searchwhere) {
            $('.input-append').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
            

            var urlMethod = "../OrderWebService.asmx/SearchOrders";
            var json = { 'search': $('#search').val(), 'searchwhere': searchwhere, client: user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);

                $('.loader').remove();

                $('#details').dataTable().fnDestroy();
                $('#details tbody').find('tr').remove();
                
                $('#details thead tr').show();
                $.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");

                var dTable = $('#details').dataTable({ "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false });
                $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable
                

                $('.orderid').click(function (e) {
                    var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).html();
                    var win = window.open(url, '_blank');
                    win = null;
                    return false;
                });
                
            });


          

        }
</script>
       
    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <p>Enter search criteria</p>
                <div class="input-append">
                  <input class="span2" id="search" type="text">
                  <button class="btn search-btn" type="button" id="OrderID">Order ID</button>
                  <button class="btn search-btn" type="button" id="Everywhere">Everywhere</button>
                  <select id="specific">
                      <option>Select Specific</option>
                      <option value="Acreminder">Active Problem</option>
                      <option value="Adjustername">Adjuster</option>
                      <option value="Authorizationno">Authorization No.</option>
                      <option value="City">City</option>
                      <option value="Company">Company</option>
                      <option value="Contractno">Contract No.</option>
                      <option value="Invoiceno">Invoice No.</option>
                      <option value="Autoowner">Owner</option>
                      <option value="PartDescription">Part Description</option>
                      <option value="Partno">Part No.</option>
                      <option value="SerialNumber">Serial Number</option>
                      <option value="Servicer">Servicer</option>
                      <option value="State">State</option>
                      <option value="Tracking">Tracking</option>
                      <option value="Vendor">Vendor</option>
                      <option value="Vendorinvoiceno">Vendor Invoice No.</option>
                      <option value="VinNo">VIN</option>
                      <option value="ActiveProblem">Warranty Claim No.</option>
                      <option value="Vehicle">Yr Make Model</option>
                      <option value="Zip">Zip</option>
                  </select>
                    <button class="btn specific-btn" type="button" id="Specific-btn" style="display:none">Search</button>
                </div>
            </div>
            
         </div>


         <div class="row-fluid">
            <div class="span12 body">
                <table class="table table-condensed" id="details">
                    <thead><tr style="display:none">
                        <th>Order ID</th>
                        <th>Company</th>
                        <th>Date Ordered</th>
                        <th>Servicer</th>
                        <th>Year</th>
                        <th>Make</th>
                        <th>Model</th>
                        <th>VIN</th>
                        <th>Part</th>
                        <th>Warehouse</th>
                        <th>Sell Price</th>
                        <th>Cost Price</th>
                        <th>Problem Status</th>
                        <th>Notes</th>
                    </tr></thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>

        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <table id="results-table" class="table table-striped table-bordered" style="display:none">
                    <tr>
                        <th>Order ID</th>
                        <th>Company</th>
                        <th>Date Ordered</th>
                        <th>Servicer</th>
                        <th>Year</th>
                        <th>Make</th>
                        <th>Model</th>
                        <th>VIN</th>
                        <th>Part</th>
                        <th>Warehouse</th>
                        <th>Sell Price</th>
                        <th>Cost Price</th>
                        <th>Problem Status</th>
                        <th>Notes</th>
                    </tr>
                </table>
            </div>
         </div>
   </div>



</asp:Content>