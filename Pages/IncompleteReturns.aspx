<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="IncompleteReturns.aspx.vb" Inherits="Pigeon.IncompleteReturns" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <script id="resultsTemplate" type="x-jquery-tmpl">
        <tr>
            <td style="text-decoration: underline;cursor: pointer" class='orderid'>${OrderID}</td> 
           <%--<td>${DateOrdered}</td>--%>
           <td>${PartType}</td>
           <td>${Part}</td>
           <td>${Vendor}</td> 
           <td>${ComingBack}</td> 
           <td>${accounting.formatMoney(PartValue)}</td> 
           <td>${Company}</td>
           <td>${CustomerType}</td>
           <td>${Servicer}</td>
           <td>${State}</td> 
           <td>${FollowUpDate}</td> 
           <td>${FollowUpStatus}</td> 
           <td><a href='#' class='notes'>${Reminder}</a></td>
           <td style="display:none" class='partid'>${PartID}</td>
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



            var urlMethod = "../OrderWebService.asmx/GetIncompleteReturns";
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
                    "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false,
                    sDom: 'Wlfriptip',
                    oColumnFilterWidgets: {
                        aiExclude: [0, 2, 5, 8, 10, 12,13],
                        sSeparator: ',  ',
                        bGroupTerms: true,
                        aoColumnDefs: [
            //{ bSort: false, sSeparator: ' / ', aiTargets: [2] },
            //{ fnSort: function (a, b) { return a - b; }, aiTargets: [3] }
                        ]

                    }

                });

                $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable


                $('.notes').click(function (e) {
                    var thenote = $(this);
                    var editorderrow = $(this).parents('tr')[0];
                    var editorderid = $(editorderrow).find('td.orderid')[0].innerHTML
                    var editpartid = $(editorderrow).find('td.partid')[0].innerHTML
                    $('#modal-form').modal();
                    $('#modal-header').html('Quick Note for ' + editorderid);
                    $('#modal-note').val($(this)[0].innerHTML);

                    $('#modal-save').click(function (e) {
                        $(thenote)[0].innerHTML = $('#modal-note').val();
                        var urlMethod = "../OrderWebService.asmx/SaveIncompleteReturns";
                        var json = { 'partid': editpartid, 'note': $('#modal-note').val(), 'client': user.Client };
                        var jsonData = JSON.stringify(json);
                        SendAjax(urlMethod, jsonData, function (msg) { });
                    });

                    return false;

                });

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
                       <%-- <th>DateOrdered</th>--%>
                        <th>Part Type</th>
                        <th>Part</th>
                        <th>Vendor</th>
                        <th>Coming Back</th>
                        <th>Value</th>
                        <th>Customer</th>
                        <th>Customer Type</th>
                        <th>Servicer</th>
                        <th>State</th>
                        <th>Follow Up Date</th>
                        <th>Follow Up Status</th>
                        <th>Note</th>
                        <th style="display:none">PartID</th>
                    </tr></thead>
                </table>
                <!-- Modal -->
                <div id="modal-form" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 id="modal-header"></h3>
                  </div>
                  <div class="modal-body">
                    <p><input type="text" id='modal-note' /></p>
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