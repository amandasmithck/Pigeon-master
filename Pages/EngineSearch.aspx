<%@ Page Title="Engine Search" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="EngineSearch.aspx.vb" Inherits="Pigeon.WebForm1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">
        <link href="../Styles/jquery.countdown.css" rel="stylesheet" />
        <script src="/Scripts/jquery.countdown.min.js"></script>
<script src="/Scripts/Quotes-v1.6-.js"></script>
    <script id="transTemplate" type="x-jquery-tmpl">
        <div class="container-fluid">
            <span id="vendor" style="display:none"></span>
            <span id="subtype" style="display:none"></span>
            <div class="row-fluid">           
                <div class="span12">
                    {{if showpartno}}
                    <h2>Part# ${partno}</h2>
                     <p id="MainQuoted" style="display:none"><i style="color:green;font-size:15px;margin-left:5px;margin-right:5px;" class="glyphicon glyphicon-ok"></i>Your Quote ID is: </p>
                    {{else}}
                    <h2>Matched Part:</h2>
         <p id="MainQuoted" style="display:none">Your Quote ID is: </p>
                    {{/if}}
                </div>
            </div>
            <div class="row-fluid">           
                <div class="span6">
                    <h5 id="delivery-days-title"><i style="margin-right: 5px;" class="icon-road"></i>Delivery Days</h5>
                    <div class="map" id="map"></div>

                    <hr />

                    <h5><i style="margin-right: 5px;" class="icon-camera"></i>Pictures</h5>
                    <div id="pics" class="pics">
                        <a rel='pics_group' href='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/atk${partno.toLowerCase()}-1.jpg' ><img src='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/tnailatk${partno.toLowerCase()}-1.jpg'  /></a>
                        <a rel='pics_group' href='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/atk${partno.toLowerCase()}-2.jpg' ><img src='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/tnailatk${partno.toLowerCase()}-2.jpg'  /></a>
                        <a rel='pics_group' href='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/atk${partno.toLowerCase()}-3.jpg' ><img src='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/tnailatk${partno.toLowerCase()}-3.jpg'  /></a>
                        <a rel='pics_group' href='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/atk${partno.toLowerCase()}-4.jpg' ><img src='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/tnailatk${partno.toLowerCase()}-4.jpg'  /></a>
                        <a rel='pics_group' href='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/atk${partno.toLowerCase()}-5.jpg' ><img src='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/tnailatk${partno.toLowerCase()}-5.jpg'  /></a>
                        <a rel='pics_group' href='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/atk${partno.toLowerCase()}-6.jpg' ><img src='http://www.atksales.com/jpg/engines/${partno.toLowerCase()}/tnailatk${partno.toLowerCase()}-6.jpg'  /></a>
                    </div>
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
                               {{if tiers!=null}}
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
                    <p><small>All engines come with all pictured components. If no pictures are available, engine will come as a longblock with no tinware, all gaskets, and new oil pump.</small></p>
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
      <div id="${Client}${partno}" relClient="${Client}" relPart="${partno}" relUser="${AdminUser}"  relCKCustNo="${CKCustNo}" class="accordion-body collapse client-collapse">
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
    
        var user = <%= Session("UserModel") %>,
            partType = "Engine",
            partTypeID = "2",
            price,
            response,
            response2,
            cart = [];

        user.Warranties = user.Warranties.filter(function(e) {
            return (e.PartType == partTypeID && e.Tier == user.Tier);
        });


        $('document').ready(function () {
            /**** begin Search Module ****/
            var searchOptions = {
                css: { 'min-width': '148px' }
                , filters: ['Year', 'Make', 'Model', 'Engine', 'Size', 'Options']
                , growable: true
                , asmxLocation: '../EngineWebService.asmx/'
                , initMethod: 'GetYears'
                , nextMethod: 'GetData'
                , resultsMethod: 'GetPrice'
                , preResultsFunc: function () {
                    $('#Listing').empty();
                    $('#Listing').html('<div class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
                }
            }

            var s = new SearchFilters('#search-filters', searchOptions, user, function (resp) {
                DisplayResults(resp);
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

            /**** begin Checkout Module ****/
        
            var checkoutOptions = {};
            var checkout = new CheckoutForm(checkoutOptions, user);

            /**** end Checkout Module ****/

            $('.loader').hide();
            $('.button').button();
            $('#checkout-button').hide();

            $(user.Warranties).each(function () {
                $('.warranty-info').append("<li><a href='../Docs/" + this.Href + "' target='_blank'>" + this.Warranty + "</a></li>");
            });
            if (user.WarrantyPaperwork != true) {
                $('.tab-warranties').hide();
            }
        
            if (user.Client == "CK" && user.Tier == "3") {
                $('.warranty-info').hide();
            }

            
            /*$('input[name=warranty]').change(function () {
                $('input[name=warranty]').parents('tr').removeClass('selected');
                $(this).parents('tr').addClass('selected');
            });*/

            $('.checkout-button').live('click', function () {
                if (response2.partno != undefined) {
                    // if (window.console) console.log('if');
                    $('#orderpartnum').html(response2.partno);
                } else {
                    //if (window.console) console.log('response2[' + $(this).attr('for') + '].partno');
                    $('#orderpartnum').html(response2[$(this).attr('for')].partno);
                }

                orderInfo = {};

                var partnumber = $(this).attr("id");
                var part = response2.filter(function (el) {
                    return el.partno == partnumber;
                })[0];

                //if (window.console) console.log(part);

                part.SalePrice = part.tiers.slice(-1)[0].Price;
                part.CorePrice = part.core;
                part.PartNumber = part.partno;

                //apparently filter function was adding tier info to part, so hacking this by pushing just what I want to partFinal
                var partFinal={};
                partFinal.SalePrice=part.SalePrice;
                partFinal.CorePrice=part.CorePrice;
                partFinal.PartNumber=part.PartNumber;
                
                //add to cart global
                cart = [];
                cart.push(partFinal);

                var orderInfo = {Parts: cart};

                checkout.render(orderInfo);

                return false;
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

        function DisplayResults(resp) {
            response2= resp[0];//$('#PigeonCompanies').css('display') == "block" ?  //: resp[0][0];
            additionalData = resp[1] || {};
            $('#Listing').empty();
            $('#CustomerEmailLabel').attr('relquoteID',additionalData.quoteID);
            if ($(response2).length >= 0) {
                //$('#Listing').html('<p class="alert alert-success">Your search returned ' + $(response2).length + ' result(s)</span>');
            }

            //building images
            if (response2.partno != "Not Available") {

                
                            $("#transTemplate").tmpl(response2).appendTo("#Listing");

                            //removing checkout button for users that do not have permission to order. removing from DOM so future fadeIn's don't work
                            if (additionalData.NoCheckout == "yes") {
                                $('.checkout-button').remove();
                            } else {
                                //gives for to checkout button required to make cart work
                                var checkoutid = 0;
                                $(".checkout").each(function () {
                                    $(this).attr('for', checkoutid);
                                    checkoutid = checkoutid + 1;
                                });
                            }

                            //$('.table-pricing tbody tr:last').addClass('info'); //.css('background-color', '#FFF858');

                            //pigeon pricing for C&K admin
            
                //Get Rid of Below if statement to remove Piegon priing maybe get rid of all stuff associated with it
                            if (user.Client == "CK" && (user.Tier == "2")) {
                                $('.pigeon-pricing-header-accordion').each(function () {
                                    $(this).show();
                                    var pigeonpricing = new PigeonPricing($(this).find('.pigeon-pricing').attr('rel'),2);
                                    pigeonpricing.render();
                                });
                            }

                            $("a[rel=pics_group]").fancybox({
                                'transitionIn': 'none',
                                'transitionOut': 'none',
                                'titlePosition': 'over',
                                'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                                    return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
                                }
                            });

            } else {
                $('#Listing').html('<p class="alert alert-danger">Your search returned No results');
            }

            $(".pics a img").error(function () {
                $(this).hide();
            });
            // $('#pics').jqDock({ align: 'middle', size: 80 });
            //data to build the map

            

            if ($("#Listing").html() != "") {
                var locid = 0;
                $(".map").each(function () {
                    $(".map:eq(" + locid + ")").attr({ id: "map" + locid });
                    locid = locid + 1;
                });


                ri = 0;
                $(response2).each(function () {
                    var currentpart=this;
                    var maparr = {};
                    var mapi = 0;
                    if (response2[ri].maps[0] != undefined) {
                        
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
                            $(response2[ri].maps).each(function () {
                                maparr["US-" + this.Abbreviation.toUpperCase()] = parseInt(this.Value);
                            });

                            DrawMap(maparr, ri);
                            ri = ri + 1;
                            $('#' + response2.partno).show();
                        }
                    } else {
                        $('#map0').html('<img src="../../images/usa.jpg" / alt="Part not Available">');
                        $('#' + response2.partno).hide();
                    }
                    //hidden spans
                    $('#vendor').html($(this)[0].vendor);
                    $('#subtype').html($(this)[0].subtype);

                });
            }
            if(response2[0].cutOffMins!=null)
            {
                var d1 = new Date(), d2 = new Date(d1);
                d2.setMinutes(d1.getMinutes() + (response2[0].cutOffMins * 1));
                ($('#countdown').hasClass('hasCountdown')) ? $('#countdown').countdown('change', { until: d2 }) : $('#countdown').countdown({ until: d2 });
            }
            $('loader').hide();
            $('#MainQuoted').append(additionalData.quoteID);
            $('#MainQuoted').show();
        }

        function DrawMap(maparr, locid) {
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
            //$('#map' + locid).append('<p class="note">Hover over your state to see delivery time <b>(Darker is sooner)</b></p>');
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
            <div class="span9">
                <div class="result">
                    <div class="details">
                        <div id="Listing">
                            <p id="searchresult"></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    </form>
</asp:Content>