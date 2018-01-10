<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="Arrivals.aspx.vb" Inherits="Pigeon.Arrivals" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css"> 
        /*hack to slide in from right*/
          .modal.fade {
          left: 200%;
          -webkit-transition: opacity 0.3s linear, left 0.3s ease-out;
             -moz-transition: opacity 0.3s linear, left 0.3s ease-out;
               -o-transition: opacity 0.3s linear, left 0.3s ease-out;
                  transition: opacity 0.3s linear, left 0.3s ease-out;
        }

        .modal.fade.in {
          left: 50%;
        }
        #modal-form input
        {
            margin-left:10px;
        }
    </style>
        <script id="resultsTemplate" type="x-jquery-tmpl">
        <tr  {{if InstallPacket != null  && InstallPacket != ""}} style="background-color:orange;color:white" {{/if}}>
            <td style="text-decoration: underline;cursor: pointer" class='orderid'>${OrderID}</td> 
           <td>${PartType}</td>
           <td style="display:none">${Vendor}</td> 
           <td>{{if InstallPacket != null && InstallPacket != ""}}<a href='${InstallPacket}' target='_blank'>${Vendor}</a>{{else}}${Vendor}{{/if}}</td> 
           <td>${PartStatus}</td>   
           <td>${ComingBack}</td> 
           <td>${ExpShipDate}</td> 
           <td>${ArrivalDate}</td> 
           <td class='freight'>${FreightETA}</td> 
           <td>${Company}</td>
           <td>${CustomerType}</td>
           <td>${Servicer}</td>
           <td>${State}</td> 
           <td>${Shipper}</td> 
           <td><a href='${TrackUrl}' target='_blank' class='tracking'>${Track}</a></td> 
           <td>${ShipperStatus}</td> 
           <td class='notes'>${Reminder}</td> 
           <td style="display:none" class='partid'>${PartID}</td>
            <td style="display:none" class='vendorinvoiceno'>${VendorInvoiceNo}</td>
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
            $("#modal-freight").mask("99/99/9999");

            $('.input-append').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
            var urlMethod = "../OrderWebService.asmx/GetArrivals";
            var json = { 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);

                $('.loader').remove();

                $('#details').dataTable().fnDestroy();
                $('#details tbody').find('tr').remove();

                $('#details thead tr').show();
                $.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");

                var dTable = $('#details').dataTable({ "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false,
                    sDom: 'Wlfriptip',
                    oColumnFilterWidgets: {
                        aiExclude: [0, 3, 6, 7, 8, 11, 14, 16,17,18],
                        sSeparator: ',  ',
                        bGroupTerms: true,
                        aoColumnDefs: [
            //{ bSort: false, sSeparator: ' / ', aiTargets: [2] },
            //{ fnSort: function (a, b) { return a - b; }, aiTargets: [3] }
                    ]

                    },
                    
                    

                });

                $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable
                $('#details tr').dblclick(function (e) {
                    var editorderrow = $(this)[0];
                    var editnote = $(editorderrow).find('.notes')[0].innerHTML;
                    var editorderid = $(editorderrow).find('.orderid')[0].innerHTML
                    var editpartid = $(editorderrow).find('.partid')[0].innerHTML
                    var edittracking = $(editorderrow).find('.tracking')[0].innerHTML
                    var editfreight = $(editorderrow).find('.freight')[0].innerHTML
                    var editvendorinvoiceno = $(editorderrow).find('.vendorinvoiceno')[0].innerHTML
                    $('#modal-form').modal();
                    $('#modal-header').html('Quick Edit for ' + editorderid);
                    $('#modal-note').val(editnote);
                    $('#modal-tracking').val(edittracking);
                    $('#modal-freight').val(editfreight);
                    $('#modal-vendorinvoiceno').val(editvendorinvoiceno);

                    $('#modal-save').click(function (e) {
                        $(editorderrow).find('.notes')[0].innerHTML = $('#modal-note').val();
                        $(editorderrow).find('.tracking')[0].innerHTML = $('#modal-tracking').val();
                        $(editorderrow).find('.freight')[0].innerHTML = $('#modal-freight').val();
                        $(editorderrow).find('.vendorinvoiceno')[0].innerHTML = $('#modal-vendorinvoiceno').val();
                        var urlMethod = "../OrderWebService.asmx/SaveArrivals";
                        var json = { 'user': user.UserName, 'orderid': editorderid, 'partid': editpartid, 'note': $('#modal-note').val(), 'shippertrack': $('#modal-tracking').val(), 'freighteta': $('#modal-freight').val(), 'vendorinvoiceno': $('#modal-vendorinvoiceno').val(), 'client': user.Client };
                        var jsonData = JSON.stringify(json);
                        SendAjax(urlMethod, jsonData, function (msg) { });
                    });

                    return false;
                });

                //$('.notes').click(function (e) {
                //    var thenote = $(this);
                //    var editorderrow = $(this).parents('tr')[0];
                //    var editorderid = $(editorderrow).find('td.orderid')[0].innerHTML
                //    var editpartid = $(editorderrow).find('td.partid')[0].innerHTML
                //    $('#modal-form').modal();
                //    $('#modal-header').html('Quick Note for ' + editorderid);
                //    $('#modal-note').val($(this)[0].innerHTML);

                //    $('#modal-save').click(function (e) {
                //        $(thenote)[0].innerHTML = $('#modal-note').val();
                //        var urlMethod = "../OrderWebService.asmx/SaveArrivals";
                //        var json = { 'partid': editpartid, 'note': $('#modal-note').val(), 'client': user.Client };
                //        var jsonData = JSON.stringify(json);
                //        SendAjax(urlMethod, jsonData, function (msg) { });
                //    });

                //    return false;

                //});

                $('#modal-form').on('hidden', function () {
                    $('#modal-save').unbind('click');
                })

                $('.orderid').click(function (e) {
                    var url = "http://admin.ckautoparts.com/Admin/order2.aspx?orderid=" + $(this).html();
                    var win = window.open(url, '_blank');
                    win = null;
                    return false;
                });

                /* Add/remove class to a row when clicked on */
                $('#details tr').click(function () {
                    $(this).toggleClass('row_selected');
                });

            });
        });
    </script>

    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <table class="table table-condensed" id="details">
                    <thead><tr style="display:none">
                        <th>Order ID</th>
                        <th>Part Type</th>
                        <th style="display:none">Vendor</th>
                        <th>Vendor</th>
                        <th>Part Status</th>
                        <th>Coming Back</th>
                        <th>Exp. Ship</th>
                        <th>Exp. Arrival</th>
                        <th>Freight ETA</th>
                        <th>Customer</th>
                        <th>Customer Type</th>
                        <th>Servicer</th>
                        <th>State</th>
                        <th>Shipper</th>
                        <th>Tracking No.</th>
                        <th>Tracking Status</th>
                        <th>Note</th>
                        <th style="display:none">PartID</th>
                        <th style="display:none">VendorInvoiceNo</th>
                    </tr></thead>
                </table>
                <!-- Modal -->
                <div id="modal-form" class="modal hide fade form-horizontal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 id="modal-header"></h3>
                  </div>
                  <div class="modal-body">
                      <div class="control-group">
                        <label class="control-label" for="modal-note">Note:</label>
                        <div class="controls">
                          <input type="text" class="controls" id='modal-note' />
                        </div>
                      </div>
                      <div class="control-group">
                        <label class="control-label" for="modal-note">Tracking No:</label>
                        <div class="controls">
                          <input type="text" class="controls" id='modal-tracking' />
                        </div>
                      </div>
                      <div class="control-group">
                        <label class="control-label" for="modal-note">Freight ETA:</label>
                        <div class="controls">
                          <input type="text" class="controls" id='modal-freight' />
                        </div>
                      </div>
                      <div class="control-group">
                        <label class="control-label" for="modal-note">Vendor Invoice No:</label>
                        <div class="controls">
                          <input type="text" class="controls" id='modal-vendorinvoiceno' />
                        </div>
                      </div>
                    
                  </div>
                  <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
                    <button class="btn btn-primary" id='modal-save'  data-dismiss="modal">Save changes</button>
                  </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>