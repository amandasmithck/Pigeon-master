<%@ Page Title="Quick Pricing" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="QuickPricing.aspx.vb" Inherits="Pigeon.QuickPricing" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         var user = <%= Session("UserModel") %>;
         $('.tab-warranties').hide();
 </script>


<script id="priceTemplate" type="text/html">
 

        <tr>
            <td>${Company}</td>
            <td>${PartType}</td>
            <td>${Family}</td>
            <td>${accounting.formatMoney(Price)}</td>
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
                alert(err.Message);
            }
        });
    }
    var user = <%= Session("UserModel") %>;
    $('document').ready(function () {

        var urlMethod = "../QuickPricingWebService.asmx/GetPricing";
        var json = {'client': user.Client};
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            response = jQuery.parseJSON(msg.d);
            $('#tblpricing tbody').append($("#priceTemplate").tmpl(response));
            $('#tblpricing').dataTable(
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
        });

    });

</script>
    <div class="container-fluid">
    
    <div class="row-fluid">
        <div class="span12">
        <%--    <div id="loading-overlay">
                <img src="../../images/ajax-loader-blue.gif"/> <span id="loadingLabel">Loading</span> 
            </div>--%>
       
            <div  class="history">
                 <table id="tblpricing" class="histable">
                    <thead>
                    
                        <tr>
                            <td>Company</td>
                            <td>PartType</td>
                            <td>Family</td>
                            <td>Price</td>
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
 </asp:Content>