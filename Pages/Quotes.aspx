<%@ Page Title="Quote History" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="Quotes.aspx.vb" Inherits="Pigeon.Quotes2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script id="detailsTemplate" type="text/html">
        <tr>
            <td style="display:none;">${pigeonCompany}</td>
            <td><a href="#">${QuoteID}</a></td>
            <td class="table-date">${QuoteDate}</td>
            <td>${Customer}</td>
            <td>${CustomerContactEmail}</td>
            <td>${Part}</td>
            <td>${Year}</td>
            <td>${Make}</td>
            <td>${Model}</td>
            {{if user.Client != "AutoNation"}}
            <td>${Cost}</td>
            <td>${SellPrice}</td>
            <td>${CorePrice}</td>
            {{/if}}
            <td>${vin}</td> 
            <td>${warranty}</td> 
            <td>${User}</td>
            <td>${LocalStock}</td>
            <td>${notes}</td>
        </tr> 
    </script>
    <script id="headings" type="text/html">
         <tr style="display:none;">
             <th style="display:none;">Pigeon Company</th>
             <th>Quote ID</th>
             <th class="table-date">Date</th>
             <th>Customer</th>
             <th>Contact Email</th>
             <th>Part</th>
             <th>Year</th>
             <th>Make</th>
             <th>Model</th>
             {{if user.Client != "AutoNation"}}
             <th>Cost Price</th>
             <th>Sell Price</th>
             <th>Core Price</th>
             {{/if}}
             <th>VIN</th>
             <th>Warranty</th>
              <th>User</th>
             <th>Local Stock</th>
             <th>Notes</th>

         </tr>
    </script>
    <script id="headingsOEM" type="text/html">
        <tr style="display:none;">
             <th style="display:none;">Pigeon Company</th>
             <th>Quote ID</th>
             <th class="table-date">Date</th> 
             <th>Customer</th>
             <th>Contact Email</th>
             <th>Part</th>
             <th>Make</th>
             <th>Cost Price</th>
            <th>Sell Price</th>
            <th>Core Price</th>
            <th>User</th>
            <th>In Stock</th>

        </tr>
    </script>
    <script id="detailsTemplateOEM" type="text/html">
        <tr>
             <td style="display:none;">${pigeonCompany}</td>
            <td>${QuoteID}</td>
            <td class="table-date">${QuoteDate}</td>
            <td>${Customer}</td>
            <td>${CustomerContactEmail}</td>
            <td>${Part}</td>
            <td>${Make}</td>
            <td>${Cost}</td>
            <td>${SellPrice}</td>
            <td>${CorePrice}</td>
              <td>${User}</td>
            <td>${LocalStock}</td>
        </tr> 
    </script>
    <div class="container-fluid main-content">
        <div class="row-fluid">
            <div class="span12"><h3>View Quote History</h3></div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <ul class="nav quote-categories nav-pills">
                     <li class="active" data-type="All"><a id='btnAll' href="#">All</a></li>
                    <li data-type="OEM"><a id='btnOEM' href="#">OEM</a></li>
                    <li data-type="Engine"><a id='btnEngine' href="#">Engine</a></li>
                    <li data-type="Transmission"><a id='btnTrans' href="#">Transmission</a></li>
                    <li data-type="TransferCase"><a id='btnTransfer' href="#">Transfer Case</a></li>
                    <li data-type="Differential"><a id='btnDiff' href="#">Differential</a></li>
                </ul>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12 body">
                <table class="table table-condensed" id="details">
                    <thead></thead>
                    <tbody></tbody>
                </table>
            </div>
    </div>

    <script type="text/javascript">
        var user = <%= Session("UserModel") %>;

        $('document').ready(function () {

            $('.tab-warranties').hide();
            if(user.Role.charAt(0).toUpperCase() + user.Role.slice(1) != "Admin")
            {
                $('#btnAll').hide();
                $('#liOEM').addClass('active');
                $('#btnAll').removeClass('active');
            }
            if (user.Client == "Tracy") {
                $("#btnOEM").hide();
            }
            
            GetQuotes();
            $('.quote-categories a').click(function() { 
                $('.quote-categories li').removeClass('active');
                $(this).parents('li').addClass('active');

                $('#details').dataTable().fnDestroy();
                $('#details tbody').find('tr').remove();

                GetQuotes(); 
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

        var data = new Array();
        function GetQuotes() {
            $('.body').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
            
            var part = $('.quote-categories li.active').attr('data-type');
            
            var urlMethod = "/OrderWebService.asmx/GetQuotesByPartTypeID";
            var data;
            switch(part)
            {
                case "Engine":
                    data={'partTypeID': <%=Convert.ToInt32(Pigeon.Enums.PartTypes.Engine)%>,'client': user.Client}; 
                    break;
                case "Transmission":
                    data={'partTypeID':  <%=Convert.ToInt32(Pigeon.Enums.PartTypes.Transmission)%>,'client': user.Client}; 
                    break;
                case "ManualTransmission":
                    data={'partTypeID':  <%=Convert.ToInt32(Pigeon.Enums.PartTypes.ManualTransmission)%>,'client': user.Client}; 
                    break;
                case "Differential":
                    data={'partTypeID':  <%=Convert.ToInt32(Pigeon.Enums.PartTypes.Differential)%>,'client': user.Client}; 
                    break;
                case "TransferCase":
                    data={'partTypeID':  <%=Convert.ToInt32(Pigeon.Enums.PartTypes.TransferCase)%>,'client': user.Client}; 
                    break;
                case "OEM":
                    data={'partTypeID':  <%=Convert.ToInt32(Pigeon.Enums.PartTypes.OEM)%>,'client': user.Client}; 
                    break;
                default:
                   data={'partTypeID':  <%=Convert.ToInt32(Pigeon.Enums.PartTypes.All)%>,'client': user.Client}; 
                    break;
            }
            if(part=="OEM")
            {
                $('#details thead').find('tr').remove().end().append($("#headingsOEM").tmpl());
            }
            else
            {
                $('#details thead').find('tr').remove().end().append($("#headings").tmpl());
            }
          
            SendAjax(urlMethod, JSON.stringify(data), function ReturnGetQuotes(msg) {
              
                data = jQuery.parseJSON(msg.d);

                $('.loader').remove();
                $('#details thead tr').show();
                if(part=="OEM")
                {
                    $('#details tbody').append($("#detailsTemplateOEM").tmpl(data));
                }
                else
                {
                   
                    $('#details tbody').append($("#detailsTemplate").tmpl(data));
                }
                var dTable = $('#details').dataTable({  "aaSorting": [[0, "desc"]], "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": true,
                    sDom: 'Wlfriptip',  
                    oColumnFilterWidgets: {
                    aiExclude: [1,2,5,6,7,8,9,10,11,12,14,15,16],
                    sSeparator: ',  ',
                    bGroupTerms: true,
                    aoColumnDefs: [
        //{ bSort: false, sSeparator: ' / ', aiTargets: [2] },
        //{ fnSort: function (a, b) { return a - b; }, aiTargets: [3] }
                    ]

                } });
                $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable
            });
        }
    </script>
    </div>
</asp:Content>