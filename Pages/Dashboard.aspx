<%@ Page Language="vb" AutoEventWireup="false"   MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="Dashboard.aspx.vb" Inherits="Pigeon.Dashboard" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      <%--<link href='http://fonts.googleapis.com/css?family=Lekton' rel='stylesheet' type='text/css'>--%>
  <link href='../Styles/mbContainer.css' rel='stylesheet' type='text/css'>

  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.js"></script>
  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.min.js"></script>
  <script type="text/javascript" src="../Scripts/mbContainer.js"></script>
  <script type="text/javascript" src="../Scripts/jquery.cookie.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.iphoneui.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.metadata.js"></script>

    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="../Scripts/excanvas.min.js"></script><![endif]-->
   <script language="javascript" type="text/javascript" src="../Scripts/jquery.flot.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery.flot.stack.js"></script>

    



    <script src="../Scripts/graphs.js"></script>



    <script id="customerTemplate" type="x-jquery-tmpl">
       <option value="${CustNo}">${Company}</option>
    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            $(".containerPlus").buildContainers({
                containment: "document",
                elementsPath: "../images/elements/",
                onCreate: function (o) { },
                onResize: function (o) { },
                onClose: function (o) { },
                onCollapse: function (o) { },
                onIconize: function (o) { },
                onDrag: function (o) { },
                onRestore: function (o) { },
            });
            
            var start = new Date();
            start.setDate(start.getDate() - 31)
            var now = new Date();

            $('#txtfromdate').val(start.getMonth() + 1 + "/" + start.getDate() + "/" + start.getFullYear());
            $('#txttodate').val(now.getMonth() + 1 + "/" + now.getDate() + "/" + now.getFullYear());

            $('#txtfromdate').datepicker()
              .on('changeDate', function (ev) {
                  $('#txtfromdate').datepicker('hide');
              });

            $('#txttodate').datepicker()
              .on('changeDate', function (ev) {
                  $('#txttodate').datepicker('hide');
              });

            //get customers
            var urlMethod = "../CustomerManageWebService.asmx/GetCustomers";
            var json = { 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selCustomer').append($("#customerTemplate").tmpl(response));

            });

            $('#btnGo').click(function () {
                $('.graph').children().remove();
                $('.loader').show();
                GetGraphs($('#selCustomer').val(), $('#txtfromdate').val(), $('#txttodate').val());
            });

            GetGraphs($('#selCustomer').val(), $('#txtfromdate').val(), $('#txttodate').val());
            $('.containerPlus').removeAttr('title'); //hides default html tooltip since have own hover going on
        });
    </script>
 
    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 200em; overflow: visible; height: 80px">
            <div class="span6">
                <div class="control-group">
                        <label class="control-label" for="modal-note">Date Range:</label>
                        <div class="controls">
                          <input type="text" id="txtfromdate" size="40" value="" >
                          <input type="text" id="txttodate" size="40" value="" >
                        </div>
                </div>
                <div class="control-group">
                        <label class="control-label" for="modal-note">Company:</label>
                        <select  id="selCustomer" ><option value="0">All Customers</option></select>
                        <input id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small" >
                </div>
            </div>
        </div>

        <div class="row-fluid" style="margin-bottom: 200em; overflow: visible; height: 80px">
            <div class="span6">
                <div id="gp-widget" class="containerPlus draggable resizable {buttons:'m', skin:'default', width:'650',title:'Total Purchases/GP',rememberMe:true }"  tooltip="" >
                    <div id="gp-dock"></div><div id="gp-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div>
                    <div style="clear: both;"></div>
                    <div id="gp-graph" class="graph" style="width: 600px; height: 250px;overflow:hidden"></div>
                </div>
            </div>
            <div class="span6">
                <div id="parts-widget" class="containerPlus draggable resizable {buttons:'m', skin:'default', width:'650',title:'Part Orders',rememberMe:true }" >
                    <div id="parts-dock"></div><div id="parts-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div>
                    <div style="clear: both;"></div>
                    <div id="parts-graph" class="graph" style="width: 600px; height: 250px;overflow:hidden"></div>
                </div>
            </div>
        </div>

 

        <div class="row-fluid" style="margin-bottom: 200em; overflow: visible; height: 80px">
            <div class="span6">
                <div id="labor-widget" class="containerPlus draggable resizable {buttons:'m', skin:'default', width:'650',title:'Labor PO/Credits',rememberMe:true}" >
                    <div id="labor-dock"></div><div id="labor-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div>
                    <div style="clear: both;"></div>
                    <div id="labor-graph" class="graph" style="width: 600px; height: 250px;overflow:hidden"></div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>