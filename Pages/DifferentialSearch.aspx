<%@ Page Title="Differential Search" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="DifferentialSearch.aspx.vb" Inherits="Pigeon.DifferentialSearch1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">
        <link href="../Styles/jquery.countdown.css" rel="stylesheet" />
        <script src="/Scripts/jquery.countdown.min.js"></script>
<script src="/Scripts/Quotes-v1.6-.js"></script>
    <style type="text/css">
        #details tbody tr:hover
        {
            background: #B0BED9;
            cursor: pointer;
        }
        #details tbody tr.selected
        {
            background-color: #D8F0C0;
        }
    </style>
    <div class="container-fluid main-content">
        <div id="AddQuoteItemsHere"></div>
        <div class="row-fluid">
            <div class="span3">
                <span id="vendor" style="display: none"></span><span id="subtype" style="display: none">
                </span>
                <div style="margin-bottom: 30px;" class="form-search">
                    <div class="input-append">
                        <input id="txtVinSearch" name="VinNumber" placeholder="VIN" class="input-large search-query" size="16" type="text"  style="height:30px;" /><input type="button"  id="btnVinSearch" class="btn" value="Search" />
                    </div>
                </div>
                <h4>Manual Search</h4>
                <div class="well" id="search-filters">
                </div>
                <div style="display: none;" class="empty-cart alert alert-info">
                    <a class="close" data-dismiss="alert" href="#">&times;</a> Cart is empty. Click
                    on a part result and then click <b>Order</b>.
                </div>
                <div style="display: none;" class="cart">
                </div>
            </div>
            <div class="span9 search-results">
                <div id="catalog" style="display: none">
                    <table id="details">
                        <thead>
                            <tr>
                                <td style="display: none;">
                                    Local Stock
                                </td>
                                <th>
                                    Part #
                                </th>
                                <th>
                                    Position
                                </th>
                                <%--<th>
                                    Core Value
                                </th>--%>
                                <th style="text-align:left">
                                    Description
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>

    <script id="detailsTemplate" type="text/html">
        <tr>
            <td style="display:none;">${LocalStock}</td>
            <td class="partno" style="text-align:center;width:75px">${PartNumber}</td>
            <td style="text-align:center;width:75px">${Position}</td>
            <%--<td style="text-align:center;width:75px">${accounting.formatMoney(CorePrice)}</td>--%>
            <td>${Description}</td>
        </tr> 
    </script>
    <script id="pricingTemplateAdmin" type="text/html">
          <h3 class="popover-title">
            <div class="row-fluid">
                <div class="span10">
                    <button class="btn-success btn-large btnOrderPart">Add To Cart</button>
                    <button class="btn-primary btn-large" style="margin-left:5px;" onclick="RequestQuote('${partno}')">Quote</button>
                    <img id="${partno}Spinner" style="display:none;" src="../images/ajax-loader-blue.gif" />
                  <p id="${partno}Quoted" style="display:none"><i style="color:green;font-size:15px;margin-left:5px;margin-right:5px;" class="glyphicon glyphicon-ok"></i>Your Quote ID is: </p>
                    {{if cutOffMins!=null}}
                        <div style="padding-top:20px">
                            <p>The cutoff time to ship today is <em style="font-weight:bold">3:30:00 PM EST</em></p>
                            <p>Time left to ship today:</p>
                            <p style="width: 200px;overflow: hidden;" id="countdown"></p>
                        </div>

        {{/if}}
                     </div>
                <div class="span2">
                    <button class="pull-right btn-danger btnClosePricing">Close</button>
                    <div class="clear-fix"></div>
                </div>
            </div>
        </h3>
        <div class="popover-content" style="padding:9px 0px">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span6">
                        <div id="map" class="map"></div>
                    </div>
                    <div class="span6">
                        <table class="table table-pricing table-striped table-bordered">
                           {{each tiers}}
                                {{if TierID >=3}}
                              <tr class="info">
                                <td class="field">${Label}</td>
                                <td class="price">${accounting.formatMoney(Price)}</td>                                
                            </tr>
                    {{else}}
                              <tr>
                                <td class="field">${Label}</td>
                                <td class="price">${accounting.formatMoney(Price)}</td>                                
                            </tr>
                    {{/if}}
                            {{/each}}
                        </table>

                        <div class="accordion pigeon-pricing-header-accordion" id="pigeon-pricing-header-accordion${partno}" style="display:none">
                            <div class="accordion-group">
                                <div class="accordion-heading">
                                  <a class="accordion-toggle" data-toggle="collapse" data-parent="#pigeon-pricing-header-accordion${partno}" href="#client-accordion${partno}">
                                    View Pigeon Pricing
                                  </a>
                                </div>
                                <div id="client-accordion${partno}" class="accordion-body collapse">
                                  <div class="accordion-inner">
                                    <div class="pigeon-pricing" rel="${partno}"></div>
                                  </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </script>
    <script id="pricingTemplate" type="text/html">
        <h3 class="popover-title">
            <div class="row-fluid">
                <div class="span10">
                    <button class="btn-success btn-large btnOrderPart">Add To Cart</button>
                </div>
                     {{if cutOffMins!=null}}
                        <div style="padding-top:20px">
                            <p>The cutoff time to ship today is <em style="font-weight:bold">3:30:00 PM EST</em></p>
                            <p>Time left to ship today:</p>
                            <p style="width: 200px;overflow: hidden;" id="countdown"></p>
                        </div>

        {{/if}}
                <div class="span2">
                    <button class="pull-right btn-primary btnClosePricing">Close</button>
                    <div class="clear-fix"></div>
                </div>
            </div>
        </h3>
        <div class="popover-content" style="padding:9px 0px">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span6">
                        <div id="map" class="map"></div>
                    </div>
                    <div class="span6">
                        <table class="table table-pricing table-striped table-bordered">
                            {{each tiers}}
                                <tr>
                                    <td>${Label}</td>
                                    <td>${accounting.formatMoney(Price)}</td>
                                </tr>
                            {{/each}}
                        </table>

                        <div class="accordion pigeon-pricing-header-accordion" id="pigeon-pricing-header-accordion${partno}" style="display:none">
                            <div class="accordion-group">
                                <div class="accordion-heading">
                                  <a class="accordion-toggle" data-toggle="collapse" data-parent="#pigeon-pricing-header-accordion${partno}" href="#client-accordion${partno}">
                                    View Pigeon Pricing
                                  </a>
                                </div>
                                <div id="client-accordion${partno}" class="accordion-body collapse">
                                  <div class="accordion-inner">
                                    <div class="pigeon-pricing" rel="${partno}"></div>
                                  </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
     
    </script>
    <script id="pigeon-pricingTemplate" type="x-jquery-tmpl">
        <div class="accordion" id="pigeon-pricing-accordion${Client}${partno}">
  <div class="accordion-group">
    <div class="accordion-heading">
      <a class="accordion-toggle " data-toggle="collapse" data-parent="#pigeon-pricing-accordion${Client}${partno}" href="#${Client}${partno}">
        ${Client}
        </a>
      </div>
      <div id="${Client}${partno}" relClient="${Client}" relPart="${partno}" relUser="${AdminUser}" relCKCustNo="${CKCustNo}" class="accordion-body collapse client-collapse">
        <div class="accordion-inner" >
        </div>
      </div>
    </div>
  </div>
    </script>
    <script id="pigeon-pricing-resultTemplate" type="x-jquery-tmpl">
    <table class="table table-hover table-bordered table-pricing">
                    <thead>
                        <tr>
                            <th>Pricing</th>
                            <th style="width:30%;">Total<b>*</b>
                                
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {{each tiers}}
                        <tr>
                            <td class="field">${Label}</td>
                            <td class="price">${accounting.formatMoney(Price)}</td>                                
                        </tr>
                        {{/each}}
                    </tbody>
                </table>
    </script>
    <script type="text/javascript">


        if (!Array.prototype.filter) {
            Array.prototype.filter = function (fun /*, thisp*/) {
                var len = this.length >>> 0;
                if (typeof fun != "function")
                    throw new TypeError();

                var res = [];
                var thisp = arguments[1];
                for (var i = 0; i < len; i++) {
                    if (i in this) {
                        var val = this[i]; // in case fun mutates this
                        if (fun.call(thisp, val, i, this))
                            res.push(val);
                    }
                }
                return res;
            };
        }

        function DrawMap(maparr) {
            $('#map').vectorMap({
                map: 'us_en',
                backgroundColor: "#ffffff",
                values: maparr,
                scaleColors: ['#00710F', '#A1E2AA'],
                normalizeFunction: 'polynomial',
                hoverOpacity: 0.90,
                hoverColor: false,
                onLabelShow: function (e, el, code) {
                    if (code == 'US-AK' || code == 'US-HI') {
                        el.html(el.html() + " Delivery Days: <b>" + maparr[code] + "</b><br /><span style='color:#FE6960;'>Additional charges may apply. Please call.</span>");
                    } else {
                        el.html(el.html() + " Delivery Days: <b>" + maparr[code] + "</b>");
                    }
                }
            });
            $('#map').after('<p class="note">Hover over your state to see delivery time <b>(Darker is sooner)</b></p>');
        };
        function CheckOut(partnumber,cost,customersNo)
        {

            if (reman.partno != undefined) {
                $('#orderpartnum').html(partnumber);
            } else {
                //$('#orderpartnum').html(reman[$(this).attr('for')].partno);
                $('#orderpartnum').html(partnumber);
            }

            cart = [];


            if ($('#reman:visible').length) {
                
                var part = reman.filter(function (el) {
                    
                    return el.partno;
                })[0];
            } else if ($('#builder:visible').length) {
                var part = builder.filter(function (el) {
                    return el.partno;
                })[0];
            }

            user.CustNo=customersNo;
            part.SalePrice = cost;
            part.CorePrice = part.core;
            part.PartNumber = part.partno;

            //add to cart global
            cart.push(part);

            /**** begin Checkout Module ****/
        
            var checkoutOptions = {};
            var checkout = new CheckoutForm(checkoutOptions, user);         
            var orderInfo = {Parts: cart};
            checkout.render(orderInfo);

            /**** end Checkout Module ****/

                
            return false;
        }
        var user = <%= Session("UserModel") %>,
            partType = "Differential",
            partTypeID = "3",
            pricingdata = {};

        user.Warranties = user.Warranties.filter(function(e) {
            return (e.PartType == partTypeID && e.Tier == user.Tier);
        });



        $('document').ready(function () {
            /**** begin Search Module ****/
            var searchOptions = {
                css: {}
                , filters: ['Year', 'Make', 'Model', 'Position']
                , asmxLocation: '../DifferentialWebService.asmx/'
                , preResultsFunc: function () {
                    $('.search-results').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
                }
            }

            var s = new SearchFilters('#search-filters', searchOptions, user, DisplaySearchResults);

            function DisplaySearchResults() {
                $('.search-results .alert').hide();
                $('.loader').remove();
                
                $('.empty-cart').fadeIn();

                $('#catalog').show();
                $('.order-summary').show()

                $('#details').dataTable().fnDestroy();
                $('#details tbody').find('tr').remove();

                $(response).each(function () {
                    $('#details tbody').append($("#detailsTemplate").tmpl(this));
                });

                $('#details').dataTable({ "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false });
                $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable
            }

            $('#btnVinSearch').click(function() {
                $('.search-results').append('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
                var urlMethod = "../DifferentialWebService.asmx/GetDataByVIN";
                var json = { 'vin': $('#txtVinSearch').val(), 'name': user.UserName, 'client': user.Client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    response = jQuery.parseJSON(msg.d);
                    DisplaySearchResults();
                });
                
            });

            s.render();
            /**** end Search Module ****/

            /**** begin Checkout Module ****/
            var checkoutOptions = {};
            checkout = new CheckoutForm(checkoutOptions, user);
            /**** end Checkout Module ****/

            if (user.WarrantyPaperwork != true) {
                $('.tab-warranties').hide();
            } else {
                $(user.Warranties).each(function () {
                    $('.warranty-info').append("<li><a href='../Docs/" + this.Href + "' target='_blank'>" + this.Warranty + "</a></li>");
                });
            }

            if (user.Client == "CK" && user.Tier == "3") {
                $('.warranty-info').hide();
            }


            $('#details tbody tr').live('click', function () {
                $this = $(this);

                var height = $this.height() - 6;

                $('tr.selected').popover('destroy');

                $('#details tbody tr').removeClass('selected');
                $this.addClass('selected');

                partno = $(this).find('.partno').text();

                $this.popover({
                    //title: "Start your search here",
                    content: "Loading...",
                    placement: 'bottom',
                    trigger: 'manual'
                });

                $this.popover('show');

                var urlMethod = "../DifferentialWebService.asmx/GetPrice";
                var json = {'year':$('.year :selected').text(),'make':$('.make :selected').text(),'model':$('.model :selected').text(), 'customerNo': $('#CustomerNameLabel').text() != "" ? $('#CustomerNameLabel').attr("relCustNo") : user.UserName
            , 'customerEmail': $('#CustomerEmailLabel').text() != "" ? $('#CustomerEmailLabel').text() : user.UserName,'partno': partno, 'name': user.UserName,'client': user.Client, 'customerClient': $('#PigeonCompanies').css('display') == "block" ? $('#PigeonCompaniesDDL :selected').attr("data-rel") : user.Client,'vin': $('#txtVinSearch').val()!=""?$('#txtVinSearch').val():"",'tierID': user.Role=='admin' ? $('#txtEmail').val() == "" ? $('#CustomerNameLabel').attr('reltierID') : $('#FrontDDLEmail :selected').attr('reltierID') : user.Tier };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    pricingdata = jQuery.parseJSON(msg.d)[0];
                    $(pricingdata).each(function () {
                        $(this)[0].partno = partno;
                    });
                    additionalData = jQuery.parseJSON(msg.d)[1];

                    //if (window.console) console.log(pricingdata);
                    $('#vendor').html(pricingdata.vendor);
                    $('#subtype').html(pricingdata.subtype);
                    $('#pricing').empty();
                    
                    //$('tr.selected').popover('show');
                    $('.popover').css({ 'left': ($('tr.selected').width() / 2 ), 'width': '550px'});
                    if (user.Role.charAt(0).toUpperCase() + user.Role.slice(1) == "Admin")
                    {
                        $('.popover .popover-inner').empty().append($('#pricingTemplateAdmin').tmpl(pricingdata));
                    }
                    else
                    {
                        $('.popover .popover-inner').empty().append($('#pricingTemplate').tmpl(pricingdata));

                    }
                    
                    if (additionalData.NoCheckout == "yes") $(".btnOrderPart").hide();

                    //$('.table-pricing tbody tr:last').addClass('info');

                    //pigeon pricing for C&K admin
            
                    if (user.Client == "CK" && (user.Tier == "2")) {
                        $('.pigeon-pricing-header-accordion').each(function () {
                            $(this).show();
                            var pigeonpricing = new PigeonPricing($(this).find('.pigeon-pricing').attr('rel'),3);
                            pigeonpricing.render();
                        });
                    }

                    $(pricingdata).each(function () {
                        var currentpart=this;
                        if ($(this.AutoNation)[0]) {
                        
                            if (this.AutoNationAltDelivery == 0) {
                                $('#map').append('<i style="margin-right: 5px;" class="icon-road"></i>Delivery Days<br/><div style="padding:20px"><span style="font-size:20px;line-height:45px">' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '</span><input type="hidden" id="tmp-selected-delivery" value="' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '" /></div>');
                            }else{
                                $('#map').append('<i style="margin-right: 5px;" class="icon-road"></i>Delivery Days-Select Delivery Option Below<br/><div class="btn-group" data-toggle-name="tmp-selected-delivery" data-toggle="buttons-radio" ><button type="button" value="' + this.AutoNationDeliveryPrice + '" class="btn btn-info active" data-toggle="button"><span style="font-size:12px;padding-right:15px">fastest</span><span class="delivery-info" style="font-size:20px;line-height:25px">' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '</span></button><br/><br/><button type="button" value="' + this.AutoNationAltDeliveryPrice + '" class="btn btn-info" data-toggle="button"><span style="font-size:12px;padding-right:10px">cheapest</span><span class="delivery-info" style="font-size:15px;line-height:25px">' + this.AutoNationAltDelivery + ' days from ' + this.AutoNationAltDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '</span></button><input type="hidden" id="tmp-selected-delivery" value="' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '" /></div>');
                                $('div.btn-group[data-toggle-name=*]').each(function(){
                                    var group   = $(this);
                                    var form    = group.parents('form').eq(0);
                                    var name    = group.attr('data-toggle-name');
                                    var hidden  = $('input[name="' + name + '"]', form);
                                    $('button', group).each(function(){
                                        var button = $(this);
                                        button.live('click', function(){
                                            $('#tmp-selected-delivery').val($(this).find('.delivery-info').html());
                                            $('.info').find('.price').html(accounting.formatMoney($(this).val()));
                                            currentpart.tiers.slice(-1)[0].Price=$(this).val();
                                        });
                                    
                                    });
                                });
                            }
                           
                            }else{
                            var mapi = 0;
                            window.maparr = {};

                            $(pricingdata.maps).each(function () {
                                maparr["US-" + this.Abbreviation.toUpperCase()] = parseInt(this.Value);
                            });
                            DrawMap(maparr);

                             }
                        
                    });
                    if(pricingdata.cutOffMins!=null)
                    {
                        var d1 = new Date(), d2 = new Date(d1);
                        d2.setMinutes(d1.getMinutes() + (pricingdata.cutOffMins * 1));
                        ($('#countdown').hasClass('hasCountdown')) ? $('#countdown').countdown('change', { until: d2 }) : $('#countdown').countdown({ until: d2 });
                    }
                });
            });

            $('.btnClosePricing').live('click', function () {
                $('tr.selected').popover('destroy');
                //$('#pricing').empty().hide();

                return false;
            });

            $('.btnOrderPart').live('click', function (event) {
                
                var partnumber = $('#details tr.selected').find('.partno').html();
                var part = response.filter(function (el) {
                    return el.PartNumber == partnumber;
                })[0];

                if (window.console) console.log(part);

                //add a price to the part object (should be the logged-in-person's tier)
                part.price = pricingdata.tiers[0].Price;
                if ($('#PigeonCompanyLabel').text() == "") {
                    part.SalePrice = pricingdata.tiers.slice(-1)[0].Price;
                }
                else
                {
                    part.SalePrice = pricingdata.tiers[2].Price;
                }
               

                checkout.addToCart(part);

                return false;
            });
        
            /*$('#btnSubmitOrder').click(function () {
                $('#pricing').empty().hide();

                var orderInfo = {Parts: cart};
                checkout.render(orderInfo);

                $('.ui-dialog-buttonpane').find('button:nth(0)').addClass('btn-success');
                $('.ui-dialog-buttonpane').find('button:nth(1)').addClass('btn-danger');
                return false;
            });*/

            
            function GetDeliveryInfo() {
                var urlMethod = "../OEMWebService.asmx/GetDeliveryInfo";
                var json = { 'name': $('.current_user').text(), 'client': $('.current_client').text() }
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, ReturnGetDeliveryInfo);
            };

            function ReturnGetDeliveryInfo(msg) {
                var info = jQuery.parseJSON(msg.d);

                $('#zip').val(info.Zip);
            }
        });

        function RequestQuote(partNumber) {
            $('#'+partNumber+'Spinner').show();

            var url = "../DifferentialWebService.asmx/InsertNewQuote";
            var json = {
                'customerNo': $('#CustomerNameLabel').text() != "" ? $('#CustomerNameLabel').attr("relCustNo") : user.UserName
            , 'customerEmail': $('#CustomerEmailLabel').text() != "" ? $('#CustomerEmailLabel').text() : user.UserName
                               , 'year': $('.year').val()
                               , 'make': $('.make').val()
                               , 'model': $('.model').val()
                               , 'partNo': partNumber
                               , 'name': user.UserName
                               , 'client': $('#PigeonCompanies').css('display')=="block"?$('#PigeonCompaniesDDL :selected').attr("data-rel") : user.Client
                               , 'vin': $('#txtVinSearch').val()!=""?$('#txtVinSearch').val():"" 
                               ,'tierID': $('#txtEmail').val() == "" ? $('#CustomerNameLabel').attr('reltierID') : $('#FrontDDLEmail :selected').attr('reltierID')
            };
            var jsonData = JSON.stringify(json);
            SendAjax(url, jsonData, function (msg) {
                $('#'+partNumber+'Spinner').hide();

                var info = jQuery.parseJSON(msg.d);
                if (info != false) {
                    $('#'+info.partNo+'Quoted').append(info.quoteID);
                    $('#' + info.partNo + 'Quoted').show();
                    $('#CustomerEmailLabel').attr('relquoteID',info.quoteID);
                }
                //What to Do here??..
            });
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
</asp:Content>