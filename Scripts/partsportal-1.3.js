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

//globals
var partData = [];
partData[0] = []; //placeholder OEM
partData[2] = []; //placeholder Aftermarket

var cartInfo = [];
var cartParts = [];

$('document').ready(function () {

    $('.main-container').find('*').css('padding', '.1em');

    $('#img-warranty-info').hover(function () {
        $('#warranty-info').css({ 'left': $('#img-warranty-info').offset().left, 'top': $('#img-warranty-info').offset().top + 20 }).show();
    }, function () {
        $('#warranty-info').hide();
    });

    $('#li-oemtype').hover(function () {
        $('#shipping-info').css({ 'left': $('#li-oemtype').offset().left + 320, 'top': $('#li-oemtype').offset().top + 20 }).show();
    }, function () {
        $('#shipping-info').hide();
    });

    $('#li-aftertype').hover(function () {
        $('#shipping-info').css({ 'left': $('#li-aftertype').offset().left + 320, 'top': $('#li-aftertype').offset().top + 20 }).show();
    }, function () {
        $('#shipping-info').hide();
    });


    $(".cboCompany").change(function () {
        $('#emulate').html($('.cboCompany').val());
        //get shipping prices
        var urlMethod = "../OEMWebService.asmx/GetCustShippingPrices";
        var json = { 'custno': $('.cboCompany option:selected').val() };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            var info = jQuery.parseJSON(msg.d);
            user.OEMShipping = info.OEMShipping;
            user.SmallPartsShipping = info.SmallPartsShipping;
            user.GroundOEMShipping = info.GroundOEMShipping;
            user.GroundSmallPartsShipping = info.GroundSmallPartsShipping;
            var current_reg_oem = $('.regular-oem-shipping').text().split(' - $');
            $('.regular-oem-shipping').html($('.regular-oem-shipping').html().replace(current_reg_oem[1], user.OEMShipping))
            var current_ground_oem = $('.ground-oem-shipping').text().split(' - $');
            $('.ground-oem-shipping').html($('.ground-oem-shipping').html().replace(current_ground_oem[1], user.GroundOEMShipping))

            var current_reg_smallparts = $('.regular-smallparts-shipping').text().split(' - $');
            $('.regular-smallparts-shipping').html($('.regular-smallparts-shipping').html().replace(current_reg_smallparts[1], user.SmallPartsShipping))
            var current_ground_smallparts = $('.ground-smallparts-shipping').text().split(' - $');
            $('.ground-smallparts-shipping').html($('.ground-smallparts-shipping').html().replace(current_ground_smallparts[1], user.GroundSmallPartsShipping))

            $('#shipping-container input[type=radio]').change(function () {
                $('#checkout').fadeIn();
                $('input[name=select-oemtype]').filter('[value=' + $(this).val() + ']').prop('checked', true);
                $('input[name=select-aftertype]').filter('[value=' + $(this).val() + ']').prop('checked', true);
                updateCart();
            });
        });
    });

    $(".superseded_part").live('click', function () {
        var currentrow = $(this).parents('.search-row');

        var part = $(this).text();
        part = part.replace(/-/g, "");
        part = part.replace(/ /g, "");

        var quantity = currentrow.find('.Quantity').val();

        GetSupersededInfo(part, quantity, 'false');

        $("#part-switch").dialog({
            resizable: false,
            height: 290,
            width: 380,
            modal: true,
            buttons: {
                "Use this part instead": function () {
                    currentrow.find('.PartNumber').val(part);
                    InitiateQuote('true');
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });

    function GetSupersededInfo(part, quantity, quote) {
        //Pass Info here
        var urlMethod = "../OEMWebService.asmx/GetOEMPrice";
        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();
        var jsonData = "{'Emulate': '" + emulate + "', 'Quote': '" + quote + "', 'MakeID': '" + $('.cbomake').val() + "', 'Name': '" + user.UserName + "', 'Email': '" + $('#txtAdjusterEmail').css('display') == "block" ? $('#txtAdjusterEmail').val() : $('#DDLEmail :selected').text() + "', 'Part': [{'PartNumber':'" + part + "','Quantity': '" + quantity + "'}]}"
        SendAjax(urlMethod, jsonData, ReturnGetSupersededInfo);
    }
    function ReturnGetSupersededInfo(msg) {
        var info = jQuery.parseJSON(msg.d);
        $('#superseded_info li:nth(0)').text("Description: " + info[0][0].Description);
        $('#superseded_info li:nth(1)').text("List Price: " + accounting.formatMoney(info[0][0].List));
        $('#superseded_info li:nth(2)').text("Your Price: " + accounting.formatMoney(info[0][0].Your)).css('font-weight', 'bold');
        $('#superseded_info li:nth(3)').text("Core: " + accounting.formatMoney(info[0][0].Core));
        $('#superseded_info li:nth(4)').text("In Stock: " + info[0][0].Stock);
    }
    
    if (user.OEMShipping != null) {
        $('.regular-oem-shipping').html($('.regular-oem-shipping').html().replace('xx.xx', user.OEMShipping))
    }
    $('.ground-oem-shipping').html($('.ground-oem-shipping').html().replace('xx.xx', user.GroundOEMShipping))
    $('.regular-smallparts-shipping').html($('.regular-smallparts-shipping').html().replace('xx.xx', user.SmallPartsShipping))
    $('.ground-smallparts-shipping').html($('.ground-smallparts-shipping').html().replace('xx.xx', user.GroundSmallPartsShipping))

    $("#warrantydate").mask("99/99/9999");
    $("#phone").mask("(999) 999-9999? x99999");
    $("#year").mask("99");
    $("#vin").mask("*****************", { placeholder: "" });

    $("#vin").counter({
        count: 'up',
        goal: 18
    });

    var vin_counter = $("#vin_counter");
    vin_counter.detach();
    $(".form_contain label:nth(3)").find('span').append(vin_counter);

    styleGrid();

    function styleGrid() {
        $(".search-row:even").css({ 'background': '#F0F1E9' });
        $(".search-row:odd").css({ 'background': '#FFF' });
        //$('.results').hide();
    }

    $("#diag-select-make").dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "OK": function () {
                $('.cbomake option[value=' + $('#cbomake3 option:selected').val() + ']').prop('selected', true);
                InitiateQuote();
                $(this).dialog("close");
            }
        }
    });

    $("#diag-order-confirm").dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "OK": function () {
                window.location.reload();
                $(this).dialog("close");
            }
        }
    });

    $("#diag-ecm").dialog({
        autoOpen: false,
        resizable: false,
        width: 380,
        modal: true,
        buttons: {
            "Add Programming to Cart": function () {
                var row = $("#diag-ecm .row-fluid").html() * 1;

                //add row to cart for programming...
                var p = partData[2][row];
                $('.part-summary tbody').append('<tr><td style="width: 28px;"><img title="Remove" alt="remove" class="remove_cart_row" src="../images/Remove_Icon.png" /></td> \
                                            <td class="type" style="width: 90px;">ECM Prog.</td> \
                                            <td class="PartNumber">' + p.FlashPartNumber + '</td> \
                                            <td class="qty">' + p.Quantity + '</td> \
                                            <td class="price">' + accounting.formatMoney(p.FlashCost) + '</td></tr>');

                var ecm = {};
                ecm.PartType = "ECM";
                ecm.PartNumber = p.FlashPartNumber;
                ecm.Quantity = p.Quantity;
                ecm.Your = accounting.unformat(p.FlashCost);
                cartParts.push(ecm);

                updateCart();

                $(this).dialog("close");
            },
            "No Pre-Programming ": function () {
                var row = $("#diag-ecm .row").html() * 1;

                $(this).dialog("close");
            }
        }
    });
    $("#diag-ecm2").dialog({
        autoOpen: false,
        resizable: false,
        width: 380,
        modal: true,
        buttons: {
            "I Understand": function () {
                var row = $("#diag-ecm2 .row").html() * 1;

                AddToCart(row, 'Aftermarket');
                $(this).dialog("close");
            }
        }
    });

    $('.btn-addtocart').live('click', function () {
        var $this = $(this);
        var row = $('.search-row').index($(this).parents('.search-row'));
        var type = ($this.parent().hasClass('oem')) ? 'OEM' : 'Aftermarket';

        if ($this.hasClass('ecm')) {
            $("#diag-ecm .row").html(row);
            $("#diag-ecm").dialog("open");
        }

        if ($this.hasClass('ecm2')) {
            $("#diag-ecm2 .row").html(row);
            $("#diag-ecm2").dialog("open");
        }

        AddToCart(row, type);
    });

    function AddToCart(row, type, type_index) {
        var type_index = (type == "OEM") ? 0 : 2;

        $('#empty-message').hide();

        //$('.total-summary, .part-summary, #checkout').fadeIn();
        $('.total-summary, .part-summary').fadeIn();

        $('#make').val($('.cbomake option:selected').html());

        $row = $('.search-row:nth(' + row + ')');

        var partnumber = $row.find('.PartNumber').val();
        var qty = $row.find('.Quantity').val();

        var each = partData[type_index][row].Your;

        var thispart = partData[type_index][row];
        thispart.Core = accounting.unformat(thispart.Core);
        thispart.Your = accounting.unformat(thispart.Your);
        thispart.Savings = $row.find("." + type.toLowerCase() + "  .result .savings").html()

        cartParts.push(thispart);

        $('.part-summary tbody').append('<tr><td style="width: 28px;"><img title="Remove" alt="remove" class="remove_cart_row" src="../images/Remove_Icon.png" /></td> \
                                            <td class="type" style="width: 90px;">' + type + '</td> \
                                            <td class="PartNumber">' + partnumber + '</td> \
                                            <td class="qty">' + qty + '</td> \
                                            <td class="price">' + accounting.formatMoney(each) + '</td></tr>');

        if (type == "OEM") {
            $('.shopping-cart').addClass('hasOEM');
        } else if (type == "Aftermarket") {
            $('.shopping-cart').addClass('hasAftermarket');
        }

        //force $75 shipping for Jorgen Aftermarket
        if (type == "Aftermarket" && thispart.Provider == 80) {
            $('input:radio[name="select-aftertype"]').filter('[value="heavy"]').attr('checked', true);
            $('input:radio[name="select-aftertype"]').filter('[value="regular"]').attr('disabled', true);
            $('input:radio[name="select-aftertype"]').filter('[value="ground"]').attr('disabled', true);
            $('input:radio[name="select-aftertype"]').filter('[value="freight"]').attr('disabled', true);
            $('input[name=select-oemtype]').filter('[value="heavy"]').prop('checked', true);
            $('#checkout').fadeIn();
        } else {
            $('input:radio[name="select-aftertype"]').attr('disabled', false);
        }
        updateCart();
    }

    $('.part-summary .remove_cart_row').live('click', function () {
        $thisrow = $(this).parents('tr');

        var index = $('.part-summary tr').index($thisrow) - 1;

        if ($thisrow.next('tr').find('.type').html() == "ECM Prog.") {
            cartParts.splice(index, 2);
            $thisrow.next('tr').remove();
            $thisrow.remove();
        } else {
            cartParts.splice(index, 1);
            $thisrow.remove();
        }

        updateCart();
    });

    $('input[name=warrantytype]').change(function () { updateCart(); });
    $('#shipping-container input[type=radio]').change(function () {
        $('#checkout').fadeIn();
        $('input[name=select-oemtype]').filter('[value=' + $(this).val() + ']').prop('checked', true);
        $('input[name=select-aftertype]').filter('[value=' + $(this).val() + ']').prop('checked', true);
        updateCart();
    });
    function updateCart() {
        try {
            $('.shopping-cart').loadmask("Updating...");

            

            $('#btn-placeorder').fadeOut();

            $('.shopping-cart').removeClass('hasOEM').removeClass('hasAftermarket'); //reset cart content flags
            $('#shipping-container li').hide();
            $('.part-summary tbody tr').each(function () {
                var type = $(this).find('.type').html();

                if (type == "OEM") {
                    $('#li-oemtype').show();
                    $('.shopping-cart').addClass('hasOEM')
                } else if (type == "Aftermarket") {
                    $('#li-aftertype').show();
                    $('.shopping-cart').addClass('hasAftermarket');
                }
            });

            var urlMethod = "../OEMWebService.asmx/CheckCartContents";
            var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();

            var json = {
                'Emulate': emulate,
                'MakeID': $('.cbomake').val()
                    , 'Zip': '' //($('#text-prezip').val() == '') ? $('#text-actualzip').val() : $('#text-prezip').val()
                    , 'Cart': cartParts
                    , 'Name': user.UserName
            };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, returnUpdateCart);
        }
        catch (err) {
            //console.log('cart updated');
            returnUpdateCart();
        }
    }

    function returnUpdateCart(msg) {
        cartInfo = jQuery.parseJSON(msg.d);

        if (!$('.part-summary tbody tr').length) {
            $('#empty-message, .total-summary, .part-summary, #cutoff, #checkout').hide();
            $('input[name=select-oemtype]:nth(0), input[name=select-aftertype]:nth(0), input[name=warrantytype]:nth(0)').prop('selected', true);
        }
        //see if last of certain part type is removed...
        if (!$('.shopping-cart').hasClass('hasOEM')) $('input[name=select-oemtype]:nth(0)').prop('selected', true);
        if (!$('.shopping-cart').hasClass('hasAftermarket')) $('input[name=select-aftertype]:nth(0)').prop('selected', true);
        //console.log(cartInfo);

        //$('#label-shiplocation').html("Ship from: " + cartInfo.OverallInStock);

        totals = {};

        //crazy oem logic
        if (cartInfo.Complicated == "yes") {
            $('#shipping-message').html('Parts availability includes multiple warehouses.  We will notify you promptly if additional shipping cost or transit time applies.').show();
        } else {
            $('#shipping-message').hide()
        }

        //calculate totals
        var partstotal = 0;
        var oemtotal = 0;
        $('.part-summary tbody tr').each(function () {
            $row = $(this);
            price = $row.find('td.price').html();
            qty = $row.find('td.qty').html();
            type = $row.find('td.type').html();

            partstotal = partstotal + (accounting.unformat(price) * qty);

            if (type == "OEM") oemtotal += (accounting.unformat(price) * qty);
        })
        totals['parts'] = partstotal;
        totals['oemtotal'] = oemtotal;
        $('.total-summary #partstotal').html(accounting.formatMoney(totals['parts']));

        //core...
        if (cartInfo.HasCore) {
            switch ($('input[name=select-oemtype]:checked').val()) {
                case 'ground':
                    totals['coreshipping'] = 25;
                    break;
                case 'regular':
                    totals['coreshipping'] = 25;
                    break;
                case 'heavy':
                    totals['coreshipping'] = 40;
                    break;
                case 'freight':
                    totals['coreshipping'] = 100;
                    break;
                default:
                    totals['coreshipping'] = 0;
            }

            $('#li-coreshipping').show().find('p').html(accounting.formatMoney(totals['coreshipping']));

        } else {
            $('#li-coreshipping').hide();
            totals['coreshipping'] = 0;
        }

        //shipping...
        totals['shipping'] = calculateShipping();
        if ($('input[name=select-oemtype]').is(':visible'))
        {
            switch ($('input[name=select-oemtype]:checked').val())
            {
                case 'ground':
                    totals['oemshipping'] = user.GroundOEMShipping;
                    break;
                case 'regular':
                    totals['oemshipping'] = user.OEMShipping;
                    break;
                case 'heavy':
                    totals['oemshipping'] = 75;
                    break;
                case 'freight':
                    totals['oemshipping'] = 125;
                    break;
                default:
                    totals['oemshipping'] = 0;
            }
        }
        //warranty...
        if ($('.shopping-cart').hasClass('hasOEM'))
        {  
            if (user.Role == "admin")
            {
                $('input[name=warrantytype]:nth(1)').next('span').html(($('#ctl00_MainContent_AdminPartsPortal_cboCompany :selected').attr("chargeoemeoc") == "True") ? "EOC - " + accounting.formatMoney((totals['oemtotal']) * 0.081) : "EOC - " + accounting.formatMoney(0));
            }
            else
            {
                $('input[name=warrantytype]:nth(1)').next('span').html((user.ChargeOEMEOC == true) ? "EOC - " + accounting.formatMoney((totals['oemtotal']) * 0.081) : "EOC - " + accounting.formatMoney(0));
            }
        }
        else
        {
            $('input[name=warrantytype]:nth(1)').next('span').html("EOC - " + accounting.formatMoney(0));
        }
        if ($('input[name=warrantytype]:checked').val() == 'EOC')
        {
            if ($('.shopping-cart').hasClass('hasOEM')) {
                if (user.Role == "admin") {
                    totals['warranty'] = $('#ctl00_MainContent_AdminPartsPortal_cboCompany :selected').attr("chargeoemeoc") == "True" ? totals['oemtotal'] * 0.081 : 0;
                }
                else {
                    totals['warranty'] = (user.ChargeOEMEOC == true) ? totals['oemtotal'] * 0.081 : 0;
                }
            } else {
                if (user.Role == "admin") {
                    totals['warranty'] = $('#ctl00_MainContent_AdminPartsPortal_cboCompany :selected').attr("chargeoemeoc") == "True" ? totals['oemtotal'] * 0.081 : 0;
                }
                else {
                    totals['warranty'] = 0;
                }
            }
            $('#warranty_inputs').fadeIn();
        }
        else if ($('input[name=warrantytype]:checked').val() == 'Manufacturer') {
            totals['warranty'] = 0;
            $('#warranty_inputs').fadeOut();
        }
        //tax...
        if (cartInfo.Tax == "no") {
            totals['tax'] = 0;
            $('#tax').parents('li').hide();
        } else {
            totals['tax'] = ((totals['parts'] * 1) + (totals['warranty'] * 1)) * 0.05;
            $('#tax').html(accounting.formatMoney(totals['tax']));
        }

        //build cuttoff area...
        if (cartInfo['OverallInStock'] == "no") {
            $('#cutoff').hide()
        } else {
            if (cartInfo.CutoffMinutes) {
                $('#cutoff').show().find('#cuttoff_time').html("The cutoff time to ship today is <b>" + cartInfo.Cutoff + " EST</b>.");

                var d1 = new Date(), d2 = new Date(d1);
                d2.setMinutes(d1.getMinutes() + (cartInfo.CutoffMinutes * 1));
                ($('#countdown').hasClass('hasCountdown')) ? $('#countdown').countdown('change', { until: d2 }) : $('#countdown').countdown({ until: d2 });
            }
        }

        $('#grandtotal').html(accounting.formatMoney(totals['parts'] + totals['warranty'] + totals['tax'] + totals['shipping'] + totals['coreshipping']));
        var intGrandSaving = 0
        $(cartParts).each(function () {
            if (this.Savings != undefined) {
                intGrandSaving = intGrandSaving + parseFloat(this.Savings.replace('$', ''))
            }
        });
        //        console.log(intGrandSaving);
        $('#grandsaving').html(accounting.formatMoney(intGrandSaving));
        $('.shopping-cart').unloadmask();
        $('#btn-placeorder').fadeIn();
    }

    function calculateShipping() {
        var aftermarket = 0;
        var oem = 0;
        if ($('input[name=select-oemtype]').is(':visible')) {
            switch ($('input[name=select-oemtype]:checked').val()) {
                case 'ground':
                    oem = user.GroundOEMShipping;
                    break;
                case 'regular':
                    oem = user.OEMShipping;
                    break;
                case 'heavy':
                    oem = 75;
                    break;
                case 'freight':
                    oem = 125;
                    break;
                default:
                    oem = 0;
            }
        }
        if ($('input[name=select-aftertype]').is(':visible')) {
            switch ($('input[name=select-aftertype]:checked').val()) {
                case 'ground':
                    aftermarket = user.GroundSmallPartsShipping;
                    break;
                case 'regular':
                    aftermarket = user.SmallPartsShipping;
                    break;
                case 'heavy':
                    aftermarket = 75;
                    break;
                case 'freight':
                    aftermarket = 125;
                    break;
                default:
                    aftermarket = 0;
            }
        }
        return aftermarket + oem;
    }

    $('.search-table input[type=text]').live('keydown', function (event) {
        if (event.keyCode == '13') {
            $('.search-table input.Quantity:not(:last)').each(function () {
                if (!this.value) this.value = 1;
            });
            InitiateQuote();
        }
    });

    $('.search-table input.Quantity').live('focus', function (event) {
        $this = $(this);
        if (!$this.val()) $this.val('1');
    });

    $('#btn-search').click(function () {
        InitiateQuote();
    });

    function InitiateQuote() {
        //$(".btn-addtocart").attr('disabled', 'true');
        if ($('#emulate').html() == "0") {
            alert('Please select a Company');
            $('.cboCompany').focus()
            return false
        }
        if ($('.cbomake').val() == "40") {
            alert('Please select a Make');
            $('.cbomake').focus()
            return false
        }
        $('#bottom-info').appendTo('.bottom-info-area:last')
        $('.bottom-info-area').each(function () {
            if ($(this).children().length <= 0) {
                $(this).remove()
            }
        });
        $('#bottom-info').show();
        partData = [];

        //clear order form
        $('#checkout')
            .find('input:not(:disabled)').val('').end()
            .find('select').find('option:nth(0)').prop('selected', true);

        //reset cart
        cartParts = [];
        $('.part-summary tbody').empty();
        $('#empty-message').show();
        $('.total-summary, .part-summary, #cutoff, #checkout').hide();
        //$('input[name=select-oemtype]:nth(0), input[name=select-aftertype]:nth(0), input[name=warrantytype]:nth(0)').prop('checked', true);
        //default warranty
        if (user.CustNo == '230' || user.CustNo =='92') {
            $('input[name=warrantytype]:nth(1)').prop('checked', true);
        } else {
            $('input[name=warrantytype]:nth(0)').prop('checked', true);
        }
        

        $(".result-contain").loadmask("Searching...", 400);

        var count = 0;
        $(".PartNumber").each(function () {
            if ($(this).val() != "")
                count++;
        });

        var urlMethod = "../OEMWebService.asmx/InitiateQuote";

        var json = { 'rows': count };

        var jsonData = JSON.stringify(json);

        SendAjax(urlMethod, jsonData, function (msg) {
            GetParts(jQuery.parseJSON(msg.d));
        });
    }

    function GetParts(quoteids) {
        if ($('.cbomake option:selected').val() == "40") {
            /*$('#cbomake2 option[value=11]').prop('selected', true);*/
            $("#diag-select-make").dialog("open");
            return true;
        }

        $('.search-row .results').each(function () {
            $this = $(this);
            //console.log($this.parent().find("input.PartNumber"));
            if ($this.parent().find("input.PartNumber").val() != "") $this.fadeIn().prev('.list-contain').fadeIn();
        });

        var Parts = new Array();
        var i = 0;
        $('.search-table').each(function () {
            var Part = new Object;

            var partnumber = $(this).find('.PartNumber').val();
            if (partnumber !== "") {

                if ($(this).find('.Quantity').val() == "") {
                    var quantity = 1;
                    $(this).find('.Quantity').val("1");
                } else {
                    var quantity = $(this).find('.Quantity').val();
                }

                partnumber = partnumber.replace(/-/g, "");
                partnumber = partnumber.replace(/ /g, "");

                var currentquote = i;
                Part = { 'PartNumber': partnumber, 'Quantity': quantity, 'QuoteID': quoteids[currentquote] };
                Parts.push(Part);

                $(".search-row:nth(" + i + ")").attr("quoteid", quoteids[currentquote]);
                ++i;
            }
        });

        //Parts.pop();
        if (!Parts.length) {
            alert('Please enter a Part #');
        }



        var urlMethod = "../OEMWebService.asmx/GetOEMPrice";
        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();
        var jsonData = "{'Emulate': '" + emulate + "', 'Quote': 'true', 'MakeID': '" + $('.cbomake').val() + "', 'Name': '" + user.UserName + "', 'Email':'";
        jsonData += $('#txtAdjusterEmail').css('display') == "block" ? $('#txtAdjusterEmail').val() : $('#DDLEmail :selected').text();
        jsonData += "', 'client': '" + user.Client + "', 'Part': " + JSON.stringify(Parts) + "}";
        //$(Parts).each(function () {
        //    //console.log($(this));
        //    //console.log($(this)[0].PartNumber)
        //    alert($(this)[0].PartNumber);
        //    alert($(this)[0].Quantity);
        //    alert($(this)[0].QuoteID);
        //});
        SendAjax(urlMethod, jsonData, ReturnGetOEMPrice);
       
       

        var urlMethod = "../OEMWebService.asmx/GetAfterPrice";
        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();
        var jsonData = "{'Emulate': '" + emulate + "', 'MakeID': '" + $('.cbomake').val() + "', 'Name': '" + user.UserName + "', 'Zip': '', 'AfterList': " + JSON.stringify(Parts) + "}";
        SendAjax(urlMethod, jsonData, ReturnGetAfterPrice);

        $('.Quantity').keyup(function () { $('.btn-addtocart').hide(); });
        //$('.results').fadeIn();
    }

    function ReturnGetOEMPrice(msg) {
        
        partData[0] = jQuery.parseJSON(msg.d)[0];
        if (jQuery.parseJSON(msg.d)[1] != undefined) {
            additionalData = jQuery.parseJSON(msg.d)[1][0];
            //removing cart div for users that do not have permission to order. removing from DOM so future fadeIn's don't work
            if (additionalData.NoCheckout == "yes") $('#checkout').remove();
        }
        var count1 = 0;
        var count2 = 0;
        var subTotal = 0;
        var subTotalList = 0;

        hasCore = false; //reset hasCore

        //oem results...
        $(partData[0]).each(function () {
            
            var currentRow = $(".search-row:nth(" + count1 + ")");

            currentRow.find(".result-contain.oem").empty();
            //if (partData[0][count - 1].Core) hasCore = true; //set hasCore to true if a non-zero value

            //fill in list...
            currentRow.find(".list").html(accounting.formatMoney(partData[0][count1].List))

            //fill in numbers if part exists...
            if (partData[0][count1].Description == null) {
                currentRow.find(".result-contain.oem").empty().append("<p style='padding: 25px;'>Please call 800-981-7358 option 1 for assistance</p>");
            } else {
                var qty = currentRow.find('.Quantity').val();
                var total = qty * partData[0][count1].Your;
                var savings = (partData[0][count1].List * qty) - total;

                partData[0][count1].Total = accounting.formatMoney(total);
                partData[0][count1].Savings = accounting.formatMoney(savings);
                partData[0][count1].Your = accounting.formatMoney(partData[0][count1].Your);
                partData[0][count1].Core = accounting.formatMoney(partData[0][count1].Core);
                partData[0][count1].PartType = "OEM";

                currentRow.find(".result-contain.oem").empty().append($("#oemResultsTemplate").tmpl(partData[0][count1])).css({ opacity: 0 }).animate({ opacity: 1 }, "slow");
                //$('.btn-addtocart').btn({ icons: { primary: 'ui-icon-cart'} });
            }
            count1++;


        });

        calculateSavings();
        $(".result-contain.oem").unloadmask();

        //run additional logic for admin page
        if ($('#emulate').html() != 'false') RenderAdmin();

        //alert for customer side
        if ($('#emulate').html() == 'false') {
            var n = noty({
                text: '<strong>Need a REMAN quote? Click the chat below</strong>',
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
    }

    function RenderAdmin() {
      
        //remove old stock tables
        $('#stockoverall').remove();
        $('h2.warehouse_info').remove();

        var i = 0;
        $('.part-stock').each(function () {
            if (i < $(partData[0]).length)
            {
                var text = $(this).html();
                var position = $(this).position();

                //var stockhtml = "<div class='stock-level fit-content'><table class='table table-bordered table-striped '><tr><td>Hyperion</td><td>Cutoff</td><td>Name</td><td>Phone</td><td>Contact</td></tr>";

                var stockhtml = "<div><table class='table table-bordered table-striped table-condensed'><tr><td>Name</td><td>Cutoff</td><td>Phone</td><td>Contact</td></tr>";

                $(partData[0][i].StockLevels).each(function () {
                    //stockhtml += "<tr><td>" + this.Hyperion + "</td><td>" + this.Cutoff + "</td><td>" + this.Name + "</td><td>" + this.Phone + "</td><td>" + this.Contact + "</td></tr>";
                    stockhtml += "<tr><td>" + this.Name + "</td><td>" + this.Cutoff + "</td><td>" + this.Phone + "</td><td>" + this.Contact + "</td></tr>";
                });

                stockhtml += "</table></div>";

                //$(this).css({ 'text-decoration': 'underline', 'color': 'blue', 'cursor': 'pointer' }).hover(
                //    function () {
                    
                //        $(this).parent().parent().parent().append(stockhtml);
                //        $('.stock-level').css({ 'top': (position.top + 52) + "px", 'left': (position.left - 455) + "px" }).show();
                //    }, function () {
                //        $('.stock-level').remove();
                //    }
                //);
                $(this).css({ 'text-decoration': 'underline', 'color': 'blue', 'cursor': 'pointer' }).popover(
                   { 'placement': 'bottom', 'trigger': 'hover', 'html': true, 'content': stockhtml });
                i++;
            }
        });

        var stockoverall = "<h2 class='warehouse_info'>Warehouse Info</h2><table id='stockoverall' class='table table-bordered table-striped'><tr><td>Note</td><td>Name</td><td>Cutoff</td><td>Phone</td><td>Contact</td></tr>";
        $(additionalData.StockLevels).each(function () {
            stockoverall += "<tr><td>" + this.Note + "</td><td>" + this.Name + "</td><td>" + this.Cutoff + "</td><td>" + this.Phone + "</td><td>" + this.Contact + "</td></tr>";
        });
        stockoverall += "</table>";

        $('#stock-info').prepend(stockoverall);

    }

    var afterPartData;
    function ReturnGetAfterPrice(msg) {
        //remove previous popovers
        $('.aftermarket').popover('destroy');
        afterPartData = jQuery.parseJSON(msg.d); //initilize, gonna modify it a little below before it makes it to partData
        var count1 = 0;
        var count2 = 0;
        var subTotal = 0;
        var subTotalList = 0;

        hasCore = false; //reset hasCore

        //aftermarket results...
        var afterPartsModified = [];
        $(afterPartData).each(function () {
            var currentRow = $(".search-row:nth(" + count2 + ")");

            currentRow.find(".result-contain.aftermarket").empty();

            //fill in numbers if part exists...
            if (afterPartData[count2].Description == "No Parts Found" || !afterPartData[count2].Description) {
                currentRow.find(".result-contain.aftermarket").empty().append("<p style='padding: 25px;'>No Aftermarket Alternative Available</p>");
                afterPartsModified.push(afterPartData[count2]);
            } else {
                
        
                var qty = currentRow.find('.Quantity').val();
                var total = qty * afterPartData[count2].Your;
                var savings = (afterPartData[count2].List * qty) - total;
                //(savings < 0) ? savings = 0 : savings = savings;
                //console.log(afterPartData[count2])
                //console.log(savings)
                
               // afterPartData[count2].List = partData[0][count2].List; //disabling-problematic when aftermarket is returned before oem
                afterPartData[count2].Total = accounting.formatMoney(total);
                //console.log("after total: " + afterPartData[count2].Total);
                afterPartData[count2].Savings = accounting.formatMoney(savings);
                afterPartData[count2].Your = accounting.formatMoney(afterPartData[count2].Your);
                afterPartData[count2].Core = accounting.formatMoney(afterPartData[count2].Core);
                afterPartData[count2].PartType = "Aftermarket";

                currentRow.find(".result-contain.aftermarket").empty().append($("#aftermarketResultsTemplate").tmpl(afterPartData[count2])).css({ opacity: 0 }).animate({ opacity: 1 }, "slow");
                //$('.btn-addtocart').btn({ icons: { primary: 'ui-icon-cart'} });
                if (afterPartData[count2].URL) currentRow.find('.aftermarket').popover({ 'placement': 'bottom', 'trigger': 'hover', 'html': true, 'content': '<img src=' + afterPartData[count2].URL + ' alt="image" />' });
                
                afterPartsModified.push(afterPartData[count2]);
                //console.log(afterPartsModified);
            }

            //ecm flash...
            if (afterPartData[count2].FlashPartNumber && afterPartData[count2].FlashDescription) {
                currentRow.find('.aftermarket .btn-addtocart').removeClass('ecm2').addClass('ecm');
                $('#diag-ecm .ecm-description').html(afterPartData[count2].FlashDescription);
                $('#diag-ecm .cost').html(accounting.formatMoney(afterPartData[count2].FlashCost));
            } else if (!afterPartData[count2].FlashPartNumber && afterPartData[count2].FlashDescription) {
                currentRow.find('.aftermarket .btn-addtocart').removeClass('ecm').addClass('ecm2');
                $('#diag-ecm2 p').html(afterPartData[count2].FlashDescription);
            }
            else {
                currentRow.find('.aftermarket .btn-addtocart').removeClass('ecm').removeClass('ecm2');
            }

            count2++;
        });

        partData[2] = afterPartsModified; //add to master partData collection

        calculateSavings();

        $(".result-contain.aftermarket").unloadmask();
    }

    function calculateSavings() {

        $('.search-row').each(function () {
            $this = $(this);
            if ($this.find('.aftermarket .result .savings').length) {
                var list = accounting.unformat($this.find('.list').html());
                var after_savings = $this.find('.aftermarket .result .savings');

                if (list != "" && $(after_savings).is(':visible')) {
                    var qty = $this.find('input.Quantity').val();
                    var each = accounting.unformat($this.find('.aftermarket .price').html());
                    var amt = accounting.formatMoney((list - each) * qty);
                    $(after_savings).html(amt);
                }
            }
        });

        $("#tsoem").html("");
        if ($(partData[0]).length) {
            var intOemSaving = 0
            $(partData[0]).each(function () {
                if (this.Savings != undefined) {
                    intOemSaving = intOemSaving + parseFloat(this.Savings.replace('$', ''))
                }
            });
            //            console.log(intOemSaving);
            $('#tsoem').html(accounting.formatMoney(intOemSaving))
        }

        if ($('.aftermarket .result .savings').length) {
            $("#tsafter").html("");
            var intAfterSaving = 0
            $('.aftermarket .result .savings').each(function () {

                intAfterSaving = intAfterSaving + parseFloat($(this).html().replace('$', ''))
                //                console.log(intAfterSaving);
            });

            $('#tsafter').html(accounting.formatMoney(intAfterSaving))
        }
    }

    $('.btn-clear').click(function () {
        window.location = "PartsPortal.aspx";
    });

    var ROW_HTML = ' <div class="bottom-info-area row-fluid"> \
                </div> \
            <div class="row-fluid search-row" quoteid=""> \
                <table class="search-table"> \
                    <thead> \
                        <tr> \
                            <th width="25%"></th> \
                            <th width="45%">Part #</th> \
                            <th width="30%">Quantity</th> \
                        </tr> \
                    </thead> \
                    <tbody> \
                        <tr> \
                            <td width="25%" class="td-remove"><img title="Remove" alt="remove" class="remove_row" src="../images/Remove_Icon.png" /></td> \
                            <td width="45%"><input class="PartNumber" style="width: 100px; margin: 0 auto; display: block;" type="text" /></td> \
                            <td width="30%"><input class="Quantity" style="width: 40px; margin: 0 auto; display: block;" type="text" /></td> \
                        </tr> \
                    </tbody> \
                </table> \
                <div class="list-contain"> \
                    <h6>List Price</h6> \
                    <span class="list">-</span> \
                </div> \
                <div class="results"> \
                    <div class="result-contain oem"> \
                    </div> \
                    <div class="result-contain aftermarket"> \
                    </div> \
                </div> \
            </div>';

    $('.search-table td input').live("focus", function () {
        var index = $('.search-table td:nth-child(2) input').index($(this)) + 1;
        var count = $('.search-table td:nth-child(2) input').size();
        if (index == count) {
            $(this).parents(".search-row").after(ROW_HTML);
        }

        $('.PartNumber:last').attr('tabindex', $('.PartNumber, .Quantity').length + 1);
        $('.Quantity:last').attr('tabindex', $('.PartNumber, .Quantity').length + 2);

        styleGrid();
    });

    $(".search-table .remove_row").live('click', function () {
        $(this).parents(".search-row").remove();
        InitiateQuote();
        styleGrid();
    });

    $('#btn-placeorder').click(function () {
        $('#aspnetForm').validate({
            rules: {
                year: "required",
                model: "required",
                vin: "required",
                mileage: "required",
                shop: "required",
                address: "required",
                city: "required",
                state: "required",
                zip: "required",
                contact: "required",
                phone: "required",
                warrantydate: {
                    required: "#warrantydate:visible"
                }
            },
            messages: {
                year: "Please fill in vehicle year",
                model: "Please fill in vehicle model",
                vin: "Please fill in VIN",
                mileage: "Please fill in mileage",
                shop: "Please fill in shop",
                address: "Please fill in shop address",
                city: "Please fill in shop city",
                state: "Please fill in shop state",
                zip: "Please fill in shop zip",
                contact: "Please fill in shop contact",
                phone: "Please fill in shop phone",
                warrantydate: "Please enter a warranty date"
            },
            errorLabelContainer: "#error_contain",
            wrapper: "li"
        });

        if ($('#aspnetForm').valid()) {
            PlaceOrder();
            /*return false;*/
        }
    });
    function PlaceOrder() {
        $('#checkout').loadmask("Sending order to C&K...");

        var urlMethod = "../OEMWebService.asmx/PlaceOrderPlusAfter";

        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();

        var data = { 'Emulate': emulate
            , 'Name': user.UserName
            , 'MakeID': $(".cbomake").val()
            , 'Model': $("#model").val()
            , 'VIN': $("#vin").val()
            , 'Mileage': $("#mileage").val()
            , 'Year': $("#year").val()
            , 'Drive': ''
            , 'Trans': $("#trans").val()
            , 'ContractNo': $("#contractno").val()
            , 'AuthNo': $("#authno").val()
            , 'Owner': $("#owner").val()
            , 'Shop': $("#shop").val()
            , 'Address': $("#address").val()
            , 'City': $("#city").val()
            , 'State': $("#state").val()
            , 'Zip': $("#zip").val()
            , 'Contact': $("#contact").val()
            , 'Phone': $("#phone").val()
            , 'Notes': $("#notes").val()
            , 'Warranty': $('input[name=warrantytype]:checked').val()
            , 'WarrantyCost': totals["warranty"] /*($('#warrantytype option:selected').val() == "EOC") ? 15 : 0*/
            , 'WarrantyDate': $("#warrantydate").val()
            , 'WarrantyMileage': $("#warrantymileage").val()
            , 'ShippingCost': totals["shipping"] //accounting.unformat($('#shipping').html())
            , 'ReturnShippingCost': totals["coreshipping"] //need to fill in when we get this
            , 'ShippingType': $('input[name=select-oemtype]:checked').val()
            , 'Parts': cartParts
        };

        var jsonData = JSON.stringify(data);

        if (window.console) { console.log(data); }

        SendAjax(urlMethod, jsonData, ReturnPlaceOrder);
    }
    function ReturnPlaceOrder(msg) {
        if (!msg.d) {
            alert('Could not submit order. Please try again.');
            return false;
        }

        $('#checkout').unloadmask();
        $("#diag-order-confirm").dialog("open");
        $("#order-confirm").html("Your order #" + msg.d + " has been placed.");
    }
});