var CheckoutForm = (function (options, user) {
    var pub = {},
    rendered = false,
    cart = [];

    var defaults = {
        cartSelector: ".cart"
    };
    options = mergeObject(defaults, options);
    /**
    * merge 2 objects into a new object
    * @param   object  obj1
    * @param   object  obj2
    * @return  object  merged object
    */
    function mergeObject(obj1, obj2) {
        var output = {};

        if (!obj2) {
            return obj1;
        }

        for (var prop in obj1) {
            if (prop in obj2) {
                output[prop] = obj2[prop];
            } else {
                output[prop] = obj1[prop];
            }
        }
        return output;
    }


    /*begin module logic*/

    var __calcTotal = function () {
        
        var total = 0;
        $(cart).each(function () {
            if (user.Client == 'CK' ){//&& (user.Tier == '3' || user.Tier == '41')) {
                total = total + this.SalePrice
            } else if (user.Client == 'CK' && user.Role == 'admin' && ($("#txtTier").val() == '3' || $("#txtTier").val() == '41')) {
                total = total + this.SalePrice
            } else if (user.Client == 'FMP') {
                total = total + this.SalePrice
            } else if(user.Role == 'admin' && user.CustNo != null)
            {
                //Possibly may need to run a Switch Case based on Tier Here??
                total = total + this.SalePrice
            }
            else {
                total = total + this.SalePrice + (this.CorePrice * 1)
            }
        });

        //show or hide EOC fields
        $option = $('.warranty-options:last').find("option:selected").html();
        if ($option != null) {
            $values = $option.split(" -- ");
        }
        if (user.Client == 'CK' && (user.Tier == '3' || user.Tier == '2')) {
            if ($values[1] == 'EOC' || $values[1] == 'EOC(No Charge)') {
                $('.eocinfo').show();
            } else {
                $('.eocinfo').hide();
            }
        }

        //show warranty prices in warranty-options
        $('.warranty-options option').each(function () {
            $this = $(this);
            var values = $this.val().split(",");
            if (values[1] < 0) {
                var warrPrice = (total * (values[0] * 1)) + (values[1] * 1);
            } else {
                var warrPrice = (total * (values[1] * 1)) + (values[0] * 1);
            }
            if ($this.html().split(' -- ')[1]) {
                var html = $this.html().split(' -- ')[1];
            } else {
                var html = $this.html()
            }
            $this.html(accounting.formatMoney(warrPrice) + " -- " + html);
        });

        $option = $('.warranty-options:last').find("option:selected");
        values = $option.val().split(",");
        if (values[1] < 0) {
            total = total * (1 + (values[0] * 1)) + (values[1] * 1);
        } else {
            total = total * (1 + (values[1] * 1)) + (values[0] * 1);
        }
        $('.cart .cart-total').html(accounting.formatMoney(total));

        return total;
    }

    var __emailOrder = function (orderInfo) {
            
        //c&k admin only(trans and engine price overriding)
        if (user.Client == 'CK' && user.Tier == '2' && (partTypeID == '1' || partTypeID == '2')) {
            var warrantymarkup = $('.warranty-options option:selected').val().split(",");
            var warrantyflat = warrantymarkup[0];
            var warrantypercentage = warrantymarkup[1];
            if (warrantyflat != 0) {
                orderInfo.Parts[0].SalePrice = parseInt($('.cart-total').html().replace("$", "").replace(",", ""), 10) - warrantyflat
            } 
        }
        var urlMethod = "../PigeonWebService.asmx/PlaceOrder";
        var customernumberbehalf = (user.Role == 'admin' ? $('#CustomerNameLabel').attr("relCustNo") : '');
        var quoteID = (user.Role == 'admin' ? $('#CustomerEmailLabel').attr('relquoteID') : '0');
        
        //Check QuoteID
        if (user.Client == 'AutoNation') {
            var notes = $('#selected-delivery').val() + '-' + $('#txtNotes').val();
        } else {
            var notes = $('#txtNotes').val();
        }
        if (quoteID == null || quoteID == "")
        {
            InsertNewQuote(orderInfo.Parts[0].PartNumber).done(function (msg) {
                var info = jQuery.parseJSON(msg.d);
                quoteID = info!=false? info.quoteID: "0";
            });
        }
        var emailItems = "";
        if (user.Client == 'FMP' && partTypeID == '2' && user.Role == 'customer')
        {
           $.each($('#ListofItems').find('input.checkbox'), function (index, item) {
                if ($(this).attr('checked')) {
                    if(index=0)
                    {
                        emailItems = $(this).attr('data-rel') + "/";
                    }
                    else
                    {
                        emailItems = emailItems + $(this).attr('data-rel') + "/";
                    }
                }
            });
        }
        var json = {
            source: user.Role
                    , parttype: partTypeID
                    , Parts: orderInfo.Parts
                    , name: user.UserName
                    , year: $('.year option:selected').val()
                    , make: $('.make option:selected').val()
                    , model: $('.model option:selected').val()
                    , PO: $('#txtPO').val()
                    , Customer: $('#txtCustomer').val()
                    , Mileage: $('#txtMileage').val()
                    , VIN: $('#txtVIN').val()
                    , RepairFacility: $('#txtRepairFacility').val()
                    , Address: $('#txtAddress').val()
                    , City: $('#txtCity').val()
                    , State: $('#selState option:selected').val()
                    , Zip: $('#txtZip').val()
                    , Phone: $('#txtPhone').val()
                    , Contact: $('#txtContact').val()
                    , warranty: $('.warranty-options option:selected').html()
                    , Auth: $('#txtAuth').val()
                    , Contract: $('#txtContract').val()
                    , EOCDate: $('#txtEOCDate').val()
                    , EOCMileage: $('#txtEOCMileage').val()
                    , client: user.Client
                    , IP: user.IP
                    , customernumberbehalf: customernumberbehalf
                    , vendor: $('#vendor').html()
                    , subtype: $('#subtype').html()
                    , Notes: notes
                    , adjuster: $('#txtAdjuster').val()
                    , email: $('#txtEmail').css('display') == "block" ? $('#txtEmail').val() : $('#DDLEmail :selected').text()
                    , quoteID: quoteID
                    , tierID: user.Role == 'admin' ? $('#txtEmail').val() != "" ? $('#CustomerNameLabel').attr('reltierID') : $('#FrontDDLEmail :selected').attr('reltierID') : user.Tier
                    , emailItems: emailItems
        };

        var jsonData = JSON.stringify(json);

        __sendAjax(urlMethod, jsonData, function (msg) {
            response = msg.d;
            if (response != "false") {
                $('#order-diag').modal("hide");
                $("#dialog-message").modal("show");
                $("#order-confirm-message").html("Your order #" + msg.d + " has been placed.")
                $("#email-error").html("");
            }
            else {
                //$('#order-diag').modal("hide");
                $("#error-message").modal("show");
                $("#email-error").html("Unable to send email.");
            }
        });
    }

    var __toggleCart = function () {
        if (cart.length) {
            $(options.cartSelector).show();
            $('.empty-cart').hide();
        } else {
            $(options.cartSelector).hide();
            $('.empty-cart').show();
        }
    }
    /*end module logic*/

    var __sendAjax = function (urlMethod, jsonData, returnFunction) {
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

    /* public functions to be accessible from the outside */
    pub.init = function () {
       
    }

    pub.addToCart = function (part) {
        if ($(options.cartSelector).length == 0) return false;

        $(options.cartSelector).empty();

        cart.push(part);

        var orderInfo = { Parts: cart, Warranties: user.Warranties };

        $.get('../Scripts/modules/templates/cart-template.htm', function (template) {
            $.tmpl(template, orderInfo).appendTo(options.cartSelector);

            __calcTotal();
            __toggleCart();

            //register submit order button click
            $(options.cartSelector + ' #btnSubmitOrder').click(function (e) {
                e.preventDefault();
                pub.render(orderInfo);
            });

            //remove from cart click event
            $('.cart .remove').die();
            $('.cart .remove').live('click', function (event) {
                var index = $(this).parents('.cart').find('.remove').index($(this));

                var $part = $('.cart tbody tr:nth(' + index + ')');
                var $core = $('.cart tbody tr:nth(' + index + ')').next('tr');

                $part.remove();
                $core.remove();

                cart.splice(index, 1);

                __toggleCart();
                __calcTotal();
            });

            //register warr change event
            $('.warranty-options').change(function () {
                __calcTotal();
            });

            $('#selected-delivery').val($('#tmp-selected-delivery').val());
            $('tr.selected').popover('destroy');
        });
    }

    //update price based upon selected customer
    var _updatePrice = function (tier) {
        var x = 0;
        $(cart).each(function () {
            
            var seltier = tier

            if (partType == 'Manual Transmission' || partType == "Differential" || partType == "Transfer Case") {
                
                if ($('#subtype').html() == 'Reman') {
                    
                    var custprice = _.findWhere(pricingdata.tiers, { TierID: seltier }).Price;
                }

                if ($('#subtype').html() == 'Builder') {
                    
                    var custprice = _.findWhere(pricingdata.tiers, { TierID: seltier }).Price;
                }
            }
            if (partType == 'Transmission') {
                
                if ($('#subtype').html() == 'Reman') {
                    
                    var custprice = _.findWhere(reman[0].tiers, { TierID: seltier }).Price;
                }

                if ($('#subtype').html() == 'Builder') {
                    
                    var custprice = _.findWhere(builder[0].tiers, { TierID: seltier }).Price;
                }
            }
            if (partType == 'Engine') {
                
                if ($('#subtype').html() == 'Reman') {
                    
                    var custprice = _.findWhere(response2[0].tiers, { TierID: seltier }).Price;
                }

                if ($('#subtype').html() == 'Builder') {
                    var custprice = _.findWhere(response2[0].tiers, { TierID: seltier }).Price;
                }
            }

            

            $(cart)[x].SalePrice = custprice;
            $('.saleprice').html(accounting.formatMoney(custprice))
            x++;
            __calcTotal();
            

        });
    };

    pub.render = function (orderInfo) {
        //set a global module var for the parts, used in emailorder
        cart = orderInfo.Parts;

        //add the warranties to orderInfo obj to template into diag
        orderInfo.Warranties = user.Warranties;

        //remove any old versions of the cart and rerender the template
        $(".diag").remove();
      
        //http get of the template to pass the orderInfo object into
        $.get('../Scripts/modules/templates/checkout-template.htm', function (template) {

            $.tmpl(template, orderInfo).appendTo('body');

            //special hiding and showing certain fields for c&k warranty companies
            if (user.Client == 'CK' && (user.Tier == '3' || user.Tier == '2')) {
                $('.non-warranty-companies').hide();
                $('.warranty-companies').show();
            } else {
                $('.non-warranty-companies').show();
                $('.warranty-companies').hide();
            }

            if (user.Client == 'FMP' || (user.Client == 'CK' && user.Role == 'admin') || user.Role == 'admin') {
                if (user.Client == 'FMP') {
                    $('#txtEmail').prop('disabled', false);
                }
                $('.needemail').show();
            } else {
                $('.needemail').hide();
            }

            if (user.Client == 'CK' && user.Tier == '2' ) {
                $('.ckadmin-only').show();
            } else {
                $('.ckadmin-only').hide();
            }

            //default warranty override
            if (user.CustNo == '230') {
                $('#.warranty-options:last option').attr('selected', 'selected');
                $('.eocinfo').show();
                __calcTotal();
            } 


            //c&k admin only(trans and engine price overriding)
            if (user.Client == 'CK' && user.Tier == '2' && (partTypeID == '1' || partTypeID == '2')) {
                $('.cart-total').attr('contenteditable', 'false');
                $('.saleprice').attr('contenteditable', 'true');
                
            }
  
            //customer searching
            $('#txtCustomerSearch').keyup(function () {
                if (($(this).val().length) > 1) {
                    var urlMethod = "../CustomerManageWebService.asmx/searchCustomers";
                    var json = { 'searchval': $(this).val(), 'client': user.Client };
                    var jsonData = JSON.stringify(json);
                    __sendAjax(urlMethod, jsonData, function (msg) {
                        $("#tblCustomerSearchResults tbody").children().remove();
                        response = jQuery.parseJSON(msg.d);
                        $.get('../Scripts/modules/templates/customer-search-template.htm', function (template) {
                            $.tmpl(template, response).appendTo('#tblCustomerSearchResults tbody');
                            //selection handler
                            $(".customer-search-address").click(function () {
                                $("#txtCustomerName").val($(this).closest("tr").find(".customer-search-company").text());
                                $("#txtCustNo").val($(this).closest("tr").find(".customer-search-company").attr("custno"));
                                $("#txtTier").val($(this).closest("tr").find(".customer-search-company").attr("tier"));
                                $("#txtRepairFacility").val($(this).closest("tr").find(".customer-search-company").text());
                                $("#txtAddress").val($(this).closest("tr").find(".customer-search-address").text());
                                $("#txtCity").val($(this).closest("tr").find(".customer-search-city").text());
                                $("#selState").val($(this).closest("tr").find(".customer-search-state").text());
                                $("#txtZip").val($(this).closest("tr").find(".customer-search-zip").text());
                                _updatePrice($(this).closest("tr").find(".customer-search-company").attr("tier"));
                            });
                            $(".customer-search-company").click(function () {
                                $("#txtCustomerName").val($(this).closest("tr").find(".customer-search-company").text());
                                $("#txtCustNo").val($(this).closest("tr").find(".customer-search-company").attr("custno"));
                                $("#txtTier").val($(this).closest("tr").find(".customer-search-company").attr("tier"));
                                $("#txtServicer").val('');
                                $("#txtAddress").val('');
                                $("#txtCity").val('');
                                $("#selState").val('');
                                $("#txtZip").val('');
                                _updatePrice($(this).closest("tr").find(".customer-search-company").attr("tier"));
                                var url = "/OrderWebService.asmx/getEmailsByCustomerNo";
                                var Data = { "CustomerNo": $('#txtCustNo').val(), "Client": user.Client };
                                SendAjax(url, JSON.stringify(Data), function (response) {
                                    var DDLHtml;
                                    var info = jQuery.parseJSON(response.d);
                                    for (var i = 0; i < info.length; i++) {
                                        DDLHtml += "<option>" + info[i] + "</option>";
                                    }
                                    $('#DDLEmail').append(DDLHtml);
                                });
                            });

                        });
                      
                    });
                }
            });
           

            //non admin hiding
            if (user.Role != 'admin') {
                $('.admin-only').hide();
                $('#txtCustomerName').removeClass('required');
                $('#txtCustomerName').removeClass('error');
            } else {
                $('#AdminQuoteDiv').show();
            }

            //if a cart exists init warr select on order form to be the same one selected in the cart
            $('.warranty-options:last option').eq($('.warranty-options:first option').index($('.warranty-options:first option:selected'))).attr('selected', 'selected');

            $("#order-diag").modal({
                show: true,
                backdrop: 'static'
            });

            $('.btn-send').click(function () {
                if ($('#order-diag form input:visible, #order-diag select').valid()) {
                    __emailOrder(orderInfo);
                }
            });
            $('.btn-success').click(function () {
                $('#QuoteSpinner').show();
                var partTypeID;
                switch ($(location).attr('pathname')) {
                    case "/Pages/EngineSearch.aspx" || "/Pages/EngineSearch.aspx#":
                        partTypeID = 2;
                        break;
                    case "/Pages/DifferentialSearch.aspx" || "/Pages/DifferentialSearch.aspx#":
                        partTypeID = 3;
                        break;
                    case "/Pages/TransmissionSearch.aspx" || "/Pages/TransmissionSearch.aspx#":
                        partTypeID = 1;
                        break;
                    case "/Pages/ManualTransmissionSearch.aspx" || "/Pages/ManualTransmissionSearch.aspx#":
                        partTypeID = 10;
                        break;
                    case "/Pages/TransferCaseSearch.aspx" || "/Pages/TransferCaseSearch.aspx#":
                        partTypeID = 4;
                        break;
                    default:
                        break;

                }
                var url = "/OrderWebService.asmx/ProcessMainQuote";
                var json = {
                    partTypeID: partTypeID
                    , Parts: orderInfo.Parts
                    , name: user.UserName
                    , year: $('.year option:selected').val()
                    , make: $('.make option:selected').val()
                    , model: $('.model option:selected').val()
                    , PO: $('#txtPO').val()
                    , Customer: $('#txtCustomer').val()
                    , Mileage: $('#txtMileage').val()
                    , VIN: $('#txtVIN').val()
                    , RepairFacility: $('#txtRepairFacility').val()
                    , Address: $('#txtAddress').val()
                    , City: $('#txtCity').val()
                    , State: $('#selState option:selected').val()
                    , Zip: $('#txtZip').val()
                    , Phone: $('#txtPhone').val()
                    , Contact: $('#txtContact').val()
                    , warranty: $('.warranty-options option:selected').html()
                    , Auth: $('#txtAuth').val()
                    , Contract: $('#txtContract').val()
                    , client: user.Client
                    , customernumberbehalf: $('#CustomerNameLabel').text() != "" ? $('#CustomerNameLabel').attr("relCustNo") : user.UserName
                    , adjuster: $('#txtAdjuster').val()
                    , email: $('#txtEmail').css('display') == "block" ? $('#txtEmail').val() : $('#DDLEmail :selected').text()
                    , customerClient: $('#PigeonCompanies').css('display') == "block" ? $('#PigeonCompaniesDDL :selected').attr("data-rel") : user.Client
                    , notes: $('#txtNotes').val()
                    , quoteID: $('#CustomerEmailLabel').attr('relquoteID')
                };
                SendAjax(url, JSON.stringify(json), function (response) {
                    $('#QuoteSpinner').hide();
                    var info = response.d;
                    if (info != false) {
                        $('#Quoted').text("Your Quote ID is: " + info);
                        //$('#Quoted').append(info);
                        $('#Quoted').show();
                        $('#CustomerEmailLabel').attr('relquoteID', info);
                    }
                });
            });
            $('.btn-cancel').click(function () {
                $('#order-diag').modal("hide");
                $('#order-diag').remove();
            });
               $('.btn-danger').click(function () {
                $('#order-diag').modal("hide");
                $('#order-diag').remove();
            });

            //initially calc the total for the parts in the cart
            __calcTotal();

            if (user.Role == 'admin' && user.CustNo != null) {
                
                var urlMethod = "../CustomerManageWebService.asmx/SearchCustomersByCustNo";
                var json = { 'CustNo': user.CustNo, 'client': user.Client };
                var jsonData = JSON.stringify(json);
                __sendAjax(urlMethod, jsonData, function (msg) {
                    
                    $("#tblCustomerSearchResults tbody").children().remove();
                    response = jQuery.parseJSON(msg.d);
                    //$.get('../Scripts/modules/templates/customer-search-template.htm', function (template) {
                    //    $.tmpl(template, response).appendTo('#tblCustomerSearchResults tbody');
                        //selection handler
                            $("#txtCustomerName").val(response[0].Company);
                            $("#txtCustNo").val(response[0].CustNo);
                            $("#txtTier").val(response[0].Tier);
                            $("#txtServicer").val('');
                            $("#txtAddress").val(response[0].Address);
                            $("#txtCity").val(response[0].City);
                            $("#selState").val(response[0].State);
                            $("#txtZip").val(response[0].Zip);
                    });
                //});
            }

            $("#dialog-message").modal({
                show: false,
                backdrop: 'static'
            });
            
             $("#dialog-message .btn-close").click(function(){
                $("#dialog-message").modal("hide");
                window.location.reload();
            });

            $("#error-message").modal({
                show: false,
                backdrop: 'static'
            });
            $("#error-message .btn-close").click(function () {
                $("#error-message").modal("hide");
            });

            if (user.Client == 'FMP' && partTypeID == '2' && user.Role == 'customer') {
                $('#rowNextTofmp').attr('style', 'float:left;width:50%;');
                $('#selState').attr('style', 'margin-top:10px;');
                $('#txtZip').attr('style', 'margin-top:10px;');
                $('#fmpEmail').show();
            }

            $('#selectAll').change(function () {
                if($('#selectAll').prop('checked')==true)
                {
                    $.each($('#ListofItems').find('input.checkbox'), function (index, item) {
                        $(this).prop('checked',true)
                    });

                }
                else
                {
                    $.each($('#ListofItems').find('input.checkbox'), function (index, item) {
                        $(this).prop('checked',false)
                    });
                }
            });
            $('#fmpEmailCheckbox').change(function () {
                if($('#fmpEmailCheckbox').prop('checked')==true)
                {
                    $('#selectAll').prop('disabled', false);
                    $.each($('#ListofItems').find('input.checkbox'), function (index, item) {
                        $(this).prop('disabled', false)
                    });
                }
                else
                {
                    $('#selectAll').prop('checked', false);
                    $.each($('#ListofItems').find('input.checkbox'), function (index, item) {
                        $(this).prop('checked', false)
                    });
                    $('#selectAll').prop('disabled', true);
                    $.each($('#ListofItems').find('input.checkbox'), function (index, item) {
                        $(this).prop('disabled', true)
                    });
                  
                }
            });
      
            if ($('#CustomerNameLabel').text() != null) {
                if ($('#PigeonCompanyLabel').text() == "") {
                    var urlMethod = "../CustomerManageWebService.asmx/searchCustomers";
                    var json = { 'searchval': $('#CustomerNameLabel').text(), 'client': user.Client };
                    var jsonData = JSON.stringify(json);
                    __sendAjax(urlMethod, jsonData, function (msg) {
                        response = jQuery.parseJSON(msg.d)[0];
                        $("#txtCustomerName").val(response.Company);
                        $("#txtCustNo").val(response.CustNo);
                        $("#txtTier").val(response.Tier);
                        if (response.Tier == "3" || response.Tier=="") {
                        }
                        else {
                            $("#txtServicer").val(response.Servicer);
                            $("#txtAddress").val(response.Address);
                            $("#txtCity").val(response.City);
                            $("#selState").val(response.State);
                            if (response.Zip.length > 5) {
                                $("#txtZip").val(response.Zip.substring(0, response.Zip.length - 1));

                            }
                            else {
                                $("#txtZip").val(response.Zip);
                            }
                        }
                        $('#txtEmail').val($('#CustomerEmailLabel').text());
                        _updatePrice(response.Tier);
                    });

                }
                else {

                    //$('.modal-header').hide();
                    var url1 = "/OrderWebService.asmx/getCustomerInfoByCustomerNo"
                    var Data = { "CustomerNo": $('#PigeonCompaniesDDL :selected').val(), "Client": $('#PigeonCompaniesDDL :selected').attr("data-rel") };
                    __sendAjax(url1, JSON.stringify(Data), function (response) {
                        var info = jQuery.parseJSON(response.d);
                        $(info).each(function () {
                            $("#txtCustomerName").val(this.Company);
                            $('#txtEmail').val($('#CustomerEmailLabel').text());
                            $("#txtCustNo").val(this.CustNo);
                            $("#txtTier").val(this.Tier);
                            $("#txtRepairFacility").val(this.Company);
                            $("#txtAddress").val(this.Address);
                            $("#txtCity").val(this.City);
                            $("#selState").val(this.State);
                            if (this.Zip.length > 5) {
                                $("#txtZip").val(this.Zip.substring(0, this.Zip.length - 1));

                            }
                            else {
                                $("#txtZip").val(this.Zip);
                            }
                            $("#txtPhone").val(this.Phone);
                            //_updatePrice($('#CustomerNameLabel').attr("reltierid"));
                            _updatePrice("2");
                        });
                    });

                }
            }
            //prefill user data (inc warranties)
            if (user.Client != "FMP") {
                $("#txtRepairFacility").val(user.Company);
                $("#txtAddress").val(user.Address);
                $("#txtCity").val(user.City);
                $("#txtAddress").val(user.Address);
                $('#selState').val(user.State);
                $('#txtZip').val(user.Zip);
                $('#txtPhone').val(user.Phone);
            }
            //register warr change event
            $('.warranty-options').change(function () {
                __calcTotal();
            });
            //$('.saleprice').keyup(function (event) {
                
            //    $('.saleprice').html(accounting.formatMoney($('.saleprice').text()));
            //    $(cart)[0].SalePrice = $('.saleprice').text().replace('$', '');
            //    if ($(cart)[0].SalePrice.indexOf(',') > 0) {
            //        $(cart)[0].SalePrice = parseInt($(cart)[0].SalePrice.replace(',', ''));
            //    }
            //    __calcTotal();
            //});
            $('.saleprice').blur(function (event) {
                $('.saleprice').html(accounting.formatMoney($('.saleprice').text()));
                $(cart)[0].SalePrice = $('.saleprice').text().replace('$', '');
                if ($(cart)[0].SalePrice.indexOf(',') > 0) {
                    $(cart)[0].SalePrice = parseInt($(cart)[0].SalePrice.replace(',', ''));
                }
                __calcTotal();
            });
            //init masks for certain text fields
            $("#txtVIN").mask("*****************", { placeholder: "" });
            $("#txtPhone").mask("(999) 999-9999? x99999");
            $("#txtEOCDate").mask("99/99/9999");
            //$('#txtEOCDate').datepicker({
            //    format: 'mm-dd-yyyy'
            //});

            //init vin counter
            $("#txtVIN").counter({
                count: 'up',
                goal: 18
            });

            //prefill vin if searched by vin
            if ($('#txtVinSearch') != undefined) {
                if ($('#txtVinSearch').val != null) {
                    $('#txtVIN').val($('#txtVinSearch').val())
                }
            };

            //show or hide EOC fields
            $option = $('.warranty-options:last').find("option:selected").html();
            $values = $option.split(" -- ");
            if (user.Client == 'CK' && (user.Tier == '3' || user.Tier == '2')) {
                if ($values[1] == 'EOC' || $values[1] == 'EOC(No Charge)') {
                    $('.eocinfo').show();
                } else {
                    $('.eocinfo').hide();
                }
            }
        });


    }

    return pub;
});

function InsertNewQuote(partNumber)
{
    
    var url = "../ManualTransmissionWebService.asmx/InsertNewQuote";
    var json = {
        'customerNo': $('#CustomerNameLabel').text() != "" ? $('#CustomerNameLabel').attr("relCustNo") : user.UserName
    , 'customerEmail': $('#CustomerEmailLabel').text() != "" ? $('#CustomerEmailLabel').text() : user.UserName
                       , 'year': $('.year').val()
                       , 'make': $('.make').val()
                       , 'model': $('.model').val()
                       , 'partNo': partNumber
                       , 'name': user.UserName
                       , 'client': $('#PigeonCompanies').css('display') == "block" ? $('#PigeonCompaniesDDL :selected').attr("data-rel") : user.Client
                       , 'vin': $('#txtVinSearch').val() != null ? $('#txtVinSearch').val() : ""
    };
        return $.ajax({
            type: 'POST',
            async: false,
            url: url,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(json)
        }).fail(function (jqXHR, textStatus, errorThrown) {
        });
    }