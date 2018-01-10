<%@ Page Title="Transmission Search" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="TransmissionSearch.aspx.vb" Inherits="Pigeon.TransmissionSearch2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">
    <link href="../Styles/jquery.countdown.css" rel="stylesheet" />
<script src="/Scripts/Quotes-v1.6-.js"></script>
    <script src="/Scripts/jquery.countdown.min.js"></script>
    <script id="transTemplate" type="x-jquery-tmpl">
    <div class='part'>
        <span id="vendor" style="display:none">${vendor}</span>
        <span id="subtype" style="display:none">${subtype}</span>
        <div class="row-fluid">           
            <div class="span12">
                {{if showpartno}}
        <div>
        <button style="display:none;float:right;margin-right:66%;" class="btn btn-primary" type="button" onclick="RequestQuote('${partno}')">Quote</button>
                <h2>Part# ${partno}</h2>
        </div>
                <img id="${partno}Spinner" style="display:none;" src="../images/ajax-loader-blue.gif" /><p id=${partno}Quoted style="display:none"><i style="color:green;font-size:15px;margin-left:5px;margin-right:5px;" class="glyphicon glyphicon-ok"></i>Your Quote ID is: </p>
                {{else}}
          <button style="display:none;float:right;margin-right:66%;" class="btn btn-primary" type="button" onclick="RequestQuote('${partno}')">Quote</button>
                <h2>Matched Part:</h2>
         <img id="${partno}Spinner" style="display:none;" src="../images/ajax-loader-blue.gif" /><p id=${partno}Quoted style="display:none"><i style="color:green;font-size:15px;margin-left:5px;margin-right:5px;" class="glyphicon glyphicon-ok"></i>Your Quote ID is: </p>
                {{/if}}
                {{if WarningHeader}}
                <div class="alert alert-block">
                  <button type="button" class="close" data-dismiss="alert">×</button>
                  <h4>${WarningHeader}</h4>
                  {{html WarningContent}}
                </div>
                {{/if}}
            </div>
        </div>
        <div class="row-fluid">           
            <div class="span6">
                <h5 id="delivery-days-title"><i style="margin-right: 5px;" class="icon-road"></i>Delivery Days</h5>
                <div class="map" id="map"></div>

                <hr />

                <h5>Possible Applications:</h5>
                 <table style="font-size: 12px;" class="table table-bordered table-condensed tbltrans">
                    <thead>
                        <tr>
                            <th class="appno">Application#</th>
                            <th class="tagid">Tag ID</th>
                            <th class="notes">Notes</th>
                            <th class="labortime">Labor Time</th>
                        </tr>
                    <thead>
                    <tbody>
                    {{each applications}}
                        <tr>
                            <td>${appnumber}</td>
                            <td>${tagid}</td>
                            <td>${notes}</td>
                            <td>${labortime}</td>
                        </tr>
                    {{/each}}
                    </tbody>
                </table>
                
                <hr />

                {{if subtype == "Reman" && installationitems > 0}}
                    <h5 style="color:#D90023">Installation Kits</h5>
                    <table style="font-size: 12px;" class="table table-bordered table-condensed tblinstall">
                        <thead>
                            <tr>
                                <td>Part#</td>
                                <td>Description</td>
                                <td>Quantity</td>
                                <td>Price</td>
                            </tr>
                        </thead>
                        <tbody>
                            {{each installations}}
                                <tr>
                                    <td>${Part}</td>
                                    <td>${Description.toLowerCase()}</td>
                                    <td>${Quantity}</td>
                                    <td style="color:#D90023">Included</td>
                                </tr> 
                            {{/each}}
                        </tbody>
                    </table>

                    <a href="" target='_blank' style="margin-right:10px; display:none;">Installation Packet<img src="../../images/acrobat.png" style="width:15px;" /></a>
                {{/if}}
            </div>
            <div class="span6">
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
                    </tbody>
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
                <table class="table table-bordered table-hover">
                    <thead>     
                        <tr>
                            <th class="core">Core</th>
                            <th  style="width:30%;" class="coreprice">Core Value</th>
                        </tr>
                    <thead>
                    <tbody>
                        {{if core == null}}
                        <tr>
                            <td>No</td>
                            <td>N/A</td>
                        </tr>
                        {{/if}}
                        {{if core != null}}
                        <tr>
                            <td>Yes</td>
                            <td class="price">${accounting.formatMoney(core)}</td>
                        </tr>
                        {{/if}}
                    </tbody>
                </table>

                <div class="checkout btn btn-large btn-block btn-info checkout-button" id="${partno}">Order</div>
        {{if cutOffMins!=null}}
                        <div style="padding-top:20px">
                            <p>The cutoff time to ship today is <em style="font-weight:bold">3:30:00 PM EST</em></p>
                            <p>Time left to ship today:</p>
                            <p style="width: 200px;overflow: hidden;" id="countdown"></p>
                        </div>

        {{/if}}
               
            </div>
        </div>
        <div class="row-fluid fine-print">
            <div class="span12">
                 <p><small><b>* Delivered price, including core return. Core value not included. No additional freight needs to be added.</b></small></p>
                 <p><small>Price includes base warranty of ${$('.warranty-info a').first().text()} nationwide warranty that covers the assembly and the labor to remove and reinstall.  See checkout screen for possible warranty upgrades.</small></p>
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


    function getValueFromQueryString (name) {
          var value = "";

            if (window.location.toString().indexOf("?") == -1) { return value; }

            var queryString = window.location.search.substring(1);

            if (queryString.indexOf(name) == -1) { return value; }

            var qsArray = new Array();
            var i = parseInt("0");

            qsArray = queryString.split("&");
            if (qsArray == null || qsArray.length == 0) { return value; }

            for (i = 0; i < qsArray.length; i++) {
                var nvp = new Array();
                nvp = qsArray[i].split("=");
                if (nvp[0] == name) {
                    value = nvp[1];
                    break;
                }
            }

            return value;
        }



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

        var user = <%= Session("UserModel") %>,
            partType = "Transmission",
            partTypeID = "1",
            price,
            response,
            response2,
            reman,
            builder,
            cart = [];

        user.Warranties = user.Warranties.filter(function(e) {
            return (e.PartType == partTypeID && e.Tier == user.Tier);
        });

      
        $('document').ready(function () {

            //determine whether to show the builder information     
            var builderMsgDisplayStyle = ((window.location.href.indexOf('parts.ckautoparts.com') > -1 || window.location.href.indexOf('localhost') > -1) ? "block" : "none");
           // window.alert(builderMsgDisplayStyle);

            $("#divBuilderMsg").css("display", builderMsgDisplayStyle);
            

            /**** begin Search Module ****/
            var searchOptions = {
                css: { 'min-width': '148px' }
                , filters: ['Year', 'Make', 'Model', 'Engine', 'Transmission']
                , growable: true
                , asmxLocation: '../TransmissionWebService.asmx/'
                , initMethod: 'GetYears'
                , nextMethod: 'GetData'
                , resultsMethod: 'GetPrice'
                , preResultsFunc: function () {
                    $('#Listing').empty();
                    $('#Listing').html('<div class="loader"><img src="/images/ajax-loader-blue.gif" /><p> Locating matches and estimating delivery days</p></div>');
                }
            }

            var s = new SearchFilters('#search-filters', searchOptions, user, function (response) {
                DisplayResults(response);
                if (user.Client == "CK" && user.Tier != "2") {
                    var n = noty({
                        text: '<strong>Need a USED quote? Click the chat below</strong>',
                        type: 'information',
                        dismissQueue: true,
                        layout: 'topRight',
                        theme: 'defaultTheme',
                        timeout: 10000,
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });
                }
            });
            
            s.render();
            /**** end Search Module ****/

            if (user.Client == 'GO' && user.Role =='admin') { $('#partlookup').show();};

            $(user.Warranties).each(function () {
                $('.warranty-info').append("<li><a href='../Docs/" + this.Href + "' target='_blank'>" + this.Warranty + "</a></li>");
            });
            if (user.WarrantyPaperwork != true) {
                $('.tab-warranties').hide();
            }

            if (user.Client == "CK" && (user.Tier == "3" || user.Tier == "2")) {
                $(".results ul").idTabs();
                $('.warranty-info').hide();
            } else {
                $('.results ul').remove();
            }

            

            $('.loader').hide();

            $('#checkout-button').hide();

            $('.button').button();
       
          

        }); 

        function RequestQuote(partNumber)
        {
            $('#'+partNumber+'Spinner').show();
            var url="../TransmissionWebService.asmx/InsertNewQuote";
            var json = {
                'customerNo': $('#CustomerNameLabel').text() != "" ? $('#CustomerNameLabel').attr("relCustNo") : user.UserName
            , 'customerEmail': $('#CustomerEmailLabel').text() != "" ? $('#CustomerEmailLabel').text() : user.UserName
                               , 'year': $('.year').val()
                               , 'make': $('.make').val()
                               , 'model': $('.model').val()
                               , 'engine':$('.engine').val()
                               , 'transmission':$('.transmission').val()
                               , 'partNo': partNumber
                               , 'name': user.UserName
                               , 'client': $('#PigeonCompanies').css('display')=="block"?$('#PigeonCompaniesDDL :selected').attr("data-rel") : user.Client
                               ,'tierID': $('#txtEmail').val() != "" ? $('#CustomerNameLabel').attr('reltierID') : $('#FrontDDLEmail :selected').attr('reltierID')
            };
            var jsonData = JSON.stringify(json);
            SendAjax(url,jsonData,function(msg)
            {
                $('#'+partNumber+'Spinner').hide();

                var info=jQuery.parseJSON(msg.d);
                if(info!=false)
                {
                    $('#'+info.partNo+'Quoted').append(info.quoteID);
                    $('#'+info.partNo+'Quoted').show();
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
                    if(window.console) console.log(err.Message);
                }
            });
        }

  
        function DisplayResults(response) {
            response2 = response[0];

            reman = response2;
            builder = response2[0];
                additionalData = response[1] || {};
                $('#CustomerEmailLabel').attr('relquoteID',additionalData.quoteID);
            //plug in search results
            if ($(reman).length >= 0) {
                $('#reman #Listing').html('<div class="alert alert-success">Your search returned ' + $(reman).length + ' result(s)</div>');
            }
            if ($(builder).length >= 0) {
                $('#builder').find('#Listing').html('<div class="alert alert-success">Your search returned ' + $(builder).length + ' result(s)</div>');
            }

            //creating each element from tempalate
            $("#transTemplate").tmpl(reman).appendTo("#reman #Listing");
            if($(builder).length>0)
            {
                $('#builder').find('#Listing').append($("#transTemplate").tmpl(builder));
            }
            if (user.Role.charAt(0).toUpperCase() + user.Role.slice(1) == "Admin")
            {
                if($(reman).length>1)
                {
                    $('.btn-primary').show();
                }
            }
            if(reman[0].cutOffMins!=null)
            {
        var d1 = new Date(), d2 = new Date(d1);
        d2.setMinutes(d1.getMinutes() + (reman[0].cutOffMins * 1));
               ($('#countdown').hasClass('hasCountdown')) ? $('#countdown').countdown('change', { until: d2 }) : $('#countdown').countdown({ until: d2 });
            }
                //removing checkout button for users that do not have permission to order. removing from DOM so future fadeIn's don't work
                if (additionalData.NoCheckout == "yes") {
                    $('.checkout-button').remove();
                }

                //$('.table-pricing tbody tr:last').addClass('info'); //.css('background-color', '#FFF858');


            //pigeon pricing for C&K admin
            
            if (user.Client == "CK" && (user.Tier == "2")) {
                $('#reman').find('.pigeon-pricing-header-accordion').each(function () {
                    $(this).show();
                    var pigeonpricing = new PigeonPricing($(this).find('.pigeon-pricing').attr('rel'),1);
                    pigeonpricing.render();
                });
            }

            //dialogbox
            var checkoutid = 0;
            $(".checkout").each(function () {
                $(".checkout:eq(" + checkoutid + ")").attr({ 'for': checkoutid });
                checkoutid = checkoutid + 1;
            });

            $('.checkout').click(function () {
                var partnumber = $(this).attr("id");

                if (reman.partno != undefined) {
                    $('#orderpartnum').html(partnumber);
                } else {
                    //$('#orderpartnum').html(reman[$(this).attr('for')].partno);
                    $('#orderpartnum').html(partnumber);
                }

                cart = [];


                if ($('#reman:visible').length) {
                    var part = reman.filter(function (el) {
                        return el.partno == partnumber;
                    })[0];
                } else if ($('#builder:visible').length) {
                    var part = builder.filter(function (el) {
                        return el.partno == partnumber;
                    })[0];
                }

                part.SalePrice = part.tiers.slice(-1)[0].Price;
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
            });

            //Waranty selection and adjusting total price
            var tblpriceid = 0;
            $(".tblpricing").each(function () {
                $(".tblpricing:eq(" + tblpriceid + ")").attr({ id: "tblpricing" + tblpriceid });
                var priceid = 0;
                $("#tblpricing" + tblpriceid + " .price").each(function () {
                    $("#tblpricing" + tblpriceid + " .price").attr({ id: "price" + tblpriceid });
                    priceid = priceid + 1;
                });
                tblpriceid = tblpriceid + 1;
            });
            var ci = 0
            $('.tblpricing').each(function () {

                $('#tblpricing' + ci + ' tbody tr:last').css('background-color', 'yellow')
                ci = ci + 1
            });

            if ($("#reman #Listing").html() != "") {
                var locid = 0;
                $(".map").each(function () {
                    $(".map:eq(" + locid + ")").attr({ id: "map" + locid });
                    locid = locid + 1;
                });

            
                var ri = 0;
                $(reman).each(function () {
                    var currentpart=this;
                    if (this.maps[ri] != undefined) {
                        if ($(this.AutoNation)[0]) {
                        
                            if (this.AutoNationAltDelivery == 0) {
                                $('#delivery-days-title').html('<i style="margin-right: 5px;" class="icon-road"></i>Delivery Days');
                                $('#map' + ri).append('<div style="padding:40px"><span style="font-size:30px;line-height:45px">' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '</span><input type="hidden" id="selected-delivery" value="' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '" /></div>');
                            }else{
                                $('#delivery-days-title').html('<i style="margin-right: 5px;" class="icon-road"></i>Delivery Days-Select Delivery Option Below');
                                $('#map' + ri).append('<div class="btn-group" data-toggle-name="selected-delivery" data-toggle="buttons-radio" ><button type="button" value="' + this.AutoNationDeliveryPrice + '" class="btn btn-info active" data-toggle="button"><span style="font-size:12px;padding-right:30px">fastest</span><span class="delivery-info" style="font-size:30px;line-height:45px">' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '</span></button><br/><br/><button type="button" value="' + this.AutoNationAltDeliveryPrice + '" class="btn btn-info" data-toggle="button"><span style="font-size:12px;padding-right:20px">cheapest</span><span class="delivery-info" style="font-size:30px;line-height:45px">' + this.AutoNationAltDelivery + ' days from ' + this.AutoNationAltDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '</span></button><input type="hidden" id="selected-delivery" value="' + this.AutoNationDelivery + ' days from ' + this.AutoNationDeliveryFrom + ' to ' + this.AutoNationDeliveryState + '" /></div>');
                                $('div.btn-group[data-toggle-name=*]').each(function(){
                                    var group   = $(this);
                                    var form    = group.parents('form').eq(0);
                                    var name    = group.attr('data-toggle-name');
                                    var hidden  = $('input[name="' + name + '"]', form);
                                    $('button', group).each(function(){
                                        var button = $(this);
                                        button.live('click', function(){
                                            $('#selected-delivery').val($(this).find('.delivery-info').html());
                                            $('.info').find('.price').html(accounting.formatMoney($(this).val()));
                                            currentpart.tiers.slice(-1)[0].Price=$(this).val();
                                        });
                                    
                                    });
                                });
                            }
                            ri = ri + 1;
                        }else{
                            var maparr = {};
                            $(this.maps).each(function () {
                                maparr["US-" + this.Abbreviation.toUpperCase()] = parseInt(this.Value);
                            });

                            DrawMap(maparr, ri);
                            //hidden spans
                            ri = ri + 1;
                        }
                    } else {
                        $('#map0').html('<img src="../../images/usa.jpg" / alt="Part not Available">');
                    }
                });

                $(builder).each(function () {
                    if ($(this.AutoNation)=='true') {

                    }else{
                        var maparr = {};
                        $(this.maps).each(function () {
                            maparr["US-" + this.Abbreviation.toUpperCase()] = parseInt(this.Value);
                        });

                        DrawMap(maparr, ri);
                        //hidden spans
                        ri = ri + 1;
                    }
                });
            }
            
            $('#'+additionalData.partNo+'Quoted').append(additionalData.quoteID);
            $('#'+additionalData.partNo+'Quoted').show();
        }

        function DrawMap(maparr,locid) {
            $('#map' + locid).vectorMap({
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

            //$('#map' + locid).after('<p class="note">Hover over your state to see delivery time <b>(Darker is sooner)</b></p>');
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
    </script>
    
    <div class="main-content container-fluid">
                <div id="AddQuoteItemsHere"></div>
	    <div class="row-fluid">
		    <div class="span3">
                <div class="well" id="search-filters""></div> 
            </div>
		    <div class="span9 results screens">
                <ul> 
                    <li><a href="#reman" class="selected">Reman</a></li> 
                    <li><a href="#builder">Builder</a></li> 
                </ul> 

                <div id="reman" style="display: block; ">
                    <div class="result">
                        <!--<h2>Reman Transmision</h2>-->
                        <div class="details">
                            <div id="Listing">
                                <p id="searchresult"></p>
                                    <div class="loader">
                                        <img src="/images/ajax-loader-blue.gif" />
                                    </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="pics">
                        </div>
                    </div>
                </div> 

                <div id="builder" style="text-align:left; ">
                    <div style="display:none;" id="divBuilderMsg">
                        Please email <a href="mailto:sales@ckautoparts.com">sales@ckautoparts.com</a> or call Sales (1-800-981-7358 x3) to obtain your builder quote.              
                    </div>
                  
                    <div class="result" style="display: none; ">
                        <div class="details">
                            <div id="Listing">
                                <p id="searchresult"></p>
                                    <div class="loader">
                                        <img src="/images/ajax-loader-blue.gif" />
                                    </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="pics">
                        </div>
                    </div>
                </div>        	
		    </div>   
	    </div>        
    </div>

    </form>
</asp:Content>