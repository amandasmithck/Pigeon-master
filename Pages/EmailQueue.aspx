<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="EmailQueue.aspx.vb" Inherits="Pigeon.EmailQueue" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <script id="resultsTemplate" type="x-jquery-tmpl">
        <tr>
        {{if Processed ==null}}
            <td></td>
        {{else}}
            <td><input type='button' value='Resend' onclick='ResendEmail(${ID});' class='paginate_button' /></td>
        {{/if}}
        
        <td>${OrderID}</td>
        <td>${MainDate}</td>
        <td>${WhoEntered}</td>
        <td>${CustomerName}</td>
        <td>${WhoTo}</td>
        <td>${EmailType}</td>
        <td>${Processed}</td>
        <td>${isError}</td>
        </tr>

    </script>
    
    <script type="text/javascript">

        $(document).ready(function () {
            $('#Start').datepicker();
            $('#End').datepicker();
            var today = new Date();
            var Previous = new Date();
            Previous.setDate(today.getDate() - 30);
            $('#Start').val(Previous.getMonth() + "/" + Previous.getDate() + "/" + Previous.getFullYear());
            $('#End').val((today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear());
            getInfo();
            getAllCustomers();

        });
        
        function ResendEmail(ID)
        {
            swal({type: 'warning',title: 'Resend Email?',text: 'Are you sure you want to resend Email?',showCancelButton: true,confirmButtonText: 'Yes',cancelButtonText:'No'},function()
            {
                var url = "/EmailWebService.asmx/AddEmail";
                var Data = { 'ID':ID};
                ajaxHelper(url, 'POST', Data).done(function (r) {
                    response = jQuery.parseJSON(r.d);
                    
                    if(response.Success==true)
                    {
                        location.reload();
                        //swal({ type: 'success', title: 'Email Successfully Added to Queue', showCancelButton: false, confirmButtonText: 'OK' },
                        //function () {
                        //    location.reload();
                        //});
                    }
                    else
                    {
                        swal({ type: 'error', title: 'There was an Error adding Email to the Queue', text: 'Would you Like to try again?', showCancelButton: true, confirmButtonText: 'Yes', cancelButtonText: 'No' },
                        function () {
                            ResendEmail(ID);
                        });
                    }
                });
            });

        }
        function getInfo() {
            $('#details').dataTable().fnClearTable();
            var url = "/EmailWebService.asmx/getEmails";
            var Data = { 'StartDate': $('#Start').val(), 'EndDate': $('#End').val(), 'OrderID': $('#OrderIDTB').val()!=null?$('#OrderIDTB').val():"0",CustomerNo: $('#CustomerDDL').val() };
            ajaxHelper(url, 'POST', Data).done(function (r) {
                response = jQuery.parseJSON(r.d);
              
                $.tmpl($('#resultsTemplate'), response).appendTo("#details tbody");

                $('#details').dataTable().fnDestroy();
                $('#details').dataTable({
                    "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false,
                    sDom: 'Wlfriptip',
                    oColumnFilterWidgets: {
                        aiExclude: [0,1,2,3,4,5,6,7,8,9],//How to Choose Filters
                        sSeparator: ',  ',
                        bGroupTerms: true,
                        aoColumnDefs: [
            //{ bSort: false, sSeparator: ' / ', aiTargets: [2] },
            //{ fnSort: function (a, b) { return a - b; }, aiTargets: [3] }
                        ]
                    },
                });
            });
        }
        function getAllCustomers()
        {
            var url = "/EmailWebService.asmx/getCustomers";
            ajaxHelper(url, 'POST', null).done(function (r) {
                response = jQuery.parseJSON(r.d);
                var AddDDLHtml = "<option value=0>Select Customer</option>";
                $(response).each(function (i) {
                    AddDDLHtml += "<option value=" + this.CustomerNo + ">" + this.CustomerName + "</option>";
                });
                $('#CustomerDDL').append(AddDDLHtml);
            });
        }
        function ajaxHelper(uri, method, data) {
            return $.ajax({
                type: method,
                url: uri,
                dataType: 'json',
                contentType: 'application/json',
                data: data ? JSON.stringify(data) : null
            }).fail(function (jqXHR, textStatus, errorThrown) {
            });
        }
    </script>
        <input type="button" class="paginate_button" value="Search Date Range" onclick="getInfo()" style="float:right;margin-right:35px;" />
                    <input id="End" style="margin-right:20px;float:right;" placeholder="End Date" required/>
                <input id="Start" style="margin-right:20px;float:right;" placeholder="Start Date" required />
               <input type="button" class="paginate_button" value="Search Customer" onclick="getInfo()" style="float:right;margin-right:20px;" />
               <select id="CustomerDDL" style="margin-right:20px;float:right;"></select>
               <input type="button" class="paginate_button" value="Search Order ID" onclick="getInfo()" style="float:right;margin-right:20px;" />  
    <input type="text" id="OrderIDTB" placeholder="Order ID" style="margin-right:20px;margin-left:20px;width:100px;float:right;" />

      <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <table class="table table-condensed" id="details">
                    <thead><tr>
                        <th></th>
                        <th>Order ID</th>
                        <th>Date</th>
                        <th>Initiated By</th>
                        <th>Customer</th>
                        <th>Email Address</th>
                        <th>Email Type</th>
                        <%--<th>Shipper</th>
                        <th>Tracking No.</th>--%>
                        <th>Processed</th>
                        <th>Error</th>
                    </tr></thead>
                    <tbody id="TablesBody">
                    </tbody>
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