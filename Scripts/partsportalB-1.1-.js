$(document).ready(function () {
    //if ( $('#emulate').html() == "false" ) GetAdjusterSavings();

    if (user.UserName == 'Guest') {
        /*$('.yourprice_col').hide();
        $('.savings2').hide();
        $('#order_grid tfoot tr td:nth(2)').attr('colspan', '4');
        $('#order_grid thead tr:nth(1) td:nth(1)').attr('colspan', '2');*/
    }

    $('#checkout').hide();

    $('#form1').validate({
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
            p0: "required",
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
            phone: "Pleaseprovide PO#",
            warrantydate: "Please enter a warranty date"
        },
        errorLabelContainer: "#error_contain",
        wrapper: "li"
    });
    $("#warrantydate").mask("99/99/9999");
    $("#phone").mask("(999) 999-9999? x99999");
    $("#year").mask("99");
    $("#vin").mask("*****************", { placeholder: "" });

    //IE CHECK...
    function getInternetExplorerVersion()
    // Returns the version of Internet Explorer or a -1
    // (indicating the use of another browser).
    {
        var rv = -1; // Return value assumes failure.
        if (navigator.appName == 'Microsoft Internet Explorer') {
            var ua = navigator.userAgent;
            var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
            if (re.exec(ua) != null)
                rv = parseFloat(RegExp.$1);
        }
        return rv;
    }
    function checkVersion() {
        var msg = "You're not using Internet Explorer.";
        var ver = getInternetExplorerVersion();

        if (ver > -1) {
            if (ver >= 8.0)
                $('#browser_info').html("You are running a current version of IE.");
            else {
                $('.remove_row').remove();
                $('#browser_info').html("You are running an older version of IE.  Some functionality of the system has been degraded.");
            }
        }
        $('#browser_info').show();
    }

    //hide eoc fields
    $('#warranty_inputs').hide();

    $('#ClearAll').button().click(function () {
        window.location = "PartsPortal.aspx";
    });

    $('#submit-order').click(function (e) {
        e.preventDefault();
        PlaceOrder();
    });

    $($('.make2')).change(function () {
        if ($(this).val() == '24') {
            $('#order_grid thead tr:nth(1)').find('td:nth(1)').prepend('<p id="merc_alert" style="font-size:12px;text-align:left;margin:0;color:#CFA600;">AutoNation cannot supply TRP(Theft Relevant Parts) per Mercedes Benz USA</p>');
        } else {
            $('#merc_alert').remove();
        }
    });

    styleGrid();
    $('#GetParts').button();

    var ROW_HTML = ' <tr> \
                    <td style="width: 5px; text-align:center;"></td> \
                    <td style="width:120px; text-align:right;"> \
                        <div><img title="Remove" alt="remove" class="remove_row" src="../images/Remove_Icon.png" /> \
                        <input tabindex="3" class="PartNumber" type="text" style="width: 100px;" /></div> \
                    </td> \
                    <td style="width:45px"> \
                        <input name="quantity" class="Quantity" type="text" style="width: 20px;" /> \
                    </td> \
                    <td class="description"> \
                    </td> \
                    <td style="width:55px"> \
                    </td> \
                    <td class="yourprice_col" style="width:55px">  \
                    </td> \
                    <td style="width:45px">  \
                    </td> \
                    <td style="width:45px"> \
                    </td> \
                    <td class="total" style="width:75px">  \
                    </td> \
                    <td class="savings2" style="width:50px">  \
                    </td> \
                </tr>';

    $('#order_grid td input').live("focus", function () {
        var index = $('#order_grid td:nth-child(2) input').index($(this)) + 1;
        var count = $('#order_grid td:nth-child(2) input').size();
        if (index == count) {
            $('#order_grid tbody').append(ROW_HTML);
        }
        $('.PartNumber:last').attr('tabindex', $('.PartNumber, .Quantity').length + 1);
        $('.Quantity:last').attr('tabindex', $('.PartNumber, .Quantity').length + 2);

        styleGrid();
    });

    $(".remove_row").live('click', function () {
        $(this).parents("tr").remove();
        styleGrid();
        GetPartInfo('true');
    });

    $(".superseded_part").live('click', function () {
        var currentrow = $(this).parents('tr');

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
                    currentrow.find('.PartNumber').val(part)
                    GetPartInfo('true');
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });


    });

    GetDeliveryInfo();

    function GetDeliveryInfo() {
        var urlMethod = "../OEMWebService.asmx/GetDeliveryInfo";
        var json = { 'name': user.UserName, 'client': user.Client }
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnGetDeliveryInfo);
    };

    function ReturnGetDeliveryInfo(msg) {
        var info = jQuery.parseJSON(msg.d);

        // $('#CompanyID').val(info.CompanyID);
        $('#shop').val(info.Company);
        //$('#edtCustomerNo').val(info.CustNo);
        $('#address').val(info.Address);
        $('#city').val(info.City);
        $('#state').val(info.State);
        $('#zip').val(info.Zip);
        $('#phone').val(info.Phone);
        //$('#edtAutoNation').attr('checked', (info.Autonation == "True") ? true : false);
        // $('#edtSalesmanEmail').val(info.SalesmanEmail);
    }



    function styleGrid() {
        $('#order_grid tbody tr:even').css('background-color', '#E8E8E8');
        $('#order_grid tbody tr:odd').css('background-color', '#FFFFFF');
        $('#order_grid tbody tr:last').find("td:nth-child(9)").css('border-bottom', '#181818');
        $('#order_grid tbody tr:last').find("td:nth-child(10)").css('border-bottom', '#181818');
        $('#order_grid tbody tr:not(:last)').find("img").attr('src', '../images/Remove_Icon.png').addClass('remove_row').show();
        $('#order_grid tbody tr:last').find("img.remove_row").attr('src', '../images/spacer-1.gif').removeClass('remove_row');
        var rowcount = 1;
        $('#order_grid tbody tr').each(function () {
            $(this).find("td:nth-child(1)").text(rowcount++);
        });
        checkVersion();
    }

    $("#shipping ul li.checkout span").button().click(function () {
        $('#make').val($(".make option:selected").text()); //fill in make

        var shippingtype = $(this).attr('id');

        //text fields
        $('#warranty').val($(this).parents('ul').find('.warranty > select option:selected').text());
        $('#warrantycost').val($(this).parents('ul').find('.warranty_cost .amount').html());
        $('#shippingcost').val($(this).parents('ul').find('.shipping_cost .amount').html());
        $('#returnshippingcost').val($(this).parents('ul').find('.coreshipping .amount').html());
        $('#shippingtype').val(shippingtype);

        //set shipping summary on checkout dialoge
        for (i = 0; i < 6; i++) {
            $('#checkout-form .shipping li:nth(' + i + ')').html($("#" + shippingtype + "_summary li:nth(" + (i + 4) + ")").html());
        }
        var checkout_height;
        if ($(this).parents('ul').find('.warranty > select option:selected').val() == "man") {
            checkout_height = 555;
        } else if ($(this).parents('ul').find('.warranty > select option:selected').val() == "eoc") {
            checkout_height = 635;
        }

        $("#checkout-form").dialog({
            //height: checkout_height,
            width: 850,
            modal: true,
            buttons: {
                "Submit Order": function () {
                    if ($('#form1').valid()) {
                        PlaceOrder();
                        $(this).dialog("close");
                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        }).parent().appendTo("form").end().parent().siblings('.ui-widget-overlay').appendTo("form");
        $('#year').focus();
    });

    $("#vin").counter({
        count: 'up',
        goal: 18
    });

    var vin_counter = $("#vin_counter");
    vin_counter.detach();
    $(".form_contain label:nth(3)").find('span').append(vin_counter);

    /*$('#order_grid tbody tr td:nth-child(2) input[type=text]').live("focus", function () {
    var index = $('#order_grid tbody tr td:nth-child(2) input[type=text]').index($(this)) + 1;
    var count = $('#order_grid tbody tr td:nth-child(2) input[type=text]').size() + 1;
    if ($(this).parents("tr").index() != 0 && (index == count - 1)) {
    $(this).parents("tr").after('<tr><td style="width: 5px; text-align:center;">' + count + '</td><td style="width:140px; text-align:right;"><div><img title="Remove" alt="remove" class="remove_row" src="../images/Remove_Icon.png" /><input name="partnumber" class="PartNumber" type="text" style="width: 100px;" /></div></td><td><input name="quantity" class="Quantity" type="text" style="width: 20px;" /></td><td class="description"></td><td></td><td class="yourprice_col"></td><td></td><td></td><td class="total" style="width:75px"> </td><td class="savings2" style="width:50px"></td></tr>');
    }

    styleGrid();
    $('#order_grid_overlay').css({ 'height': $('#order_grid').height() + 20 });
    });*/

    $('#GetParts').click(function () {
        if ($('.make2').val() == 0) {
            alert('Please select a make.');
        }
        else {
            GetPartInfo('true');
        }
    });
    $('#order_grid tbody tr input[type=text]').live('keydown', function (event) {
        if (event.keyCode == '13') {
            GetPartInfo('true');
            return false;
        }
        if (event.keyCode == '9') {
            var thisquant = $(this).parents('tr').find('.Quantity');
            if (thisquant.val() == "") {
                thisquant.val('1').focus().select();
            } else {
                if ($(this).hasClass("Quantity")) {
                    $(this).parents('tr').next().find('.PartNumber').focus();
                } else if ($(this).hasClass("PartNumber")) {
                    $(this).parents('tr').find('.Quantity').focus();
                }
            }
            return false;
        }
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

    function makeDollar(amount) {
        var i = parseFloat(amount);
        if (isNaN(i)) { i = 0.00; }
        var minus = '';
        if (i < 0) { minus = '-'; }
        i = Math.abs(i);
        i = parseInt((i + .005) * 100);
        i = i / 100;
        s = new String(i);
        if (s.indexOf('.') < 0) { s += '.00'; }
        if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
        s = minus + s;
        return s;
    }

    //get part info for a single part passed in as a parameter
    function GetSupersededInfo(partnumber, quantity, quote) {
        var Parts = new Array();
        var urlMethod = "../OEMWebService.asmx/GetOEMPrice";
        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();
        var Part = { 'PartNumber': partnumber, 'Quantity': quantity };
        Parts.push(Part);
        var json = { 'Emulate': emulate, 'Quote': quote, 'MakeID': $('.make2').val(), 'Name': user.UserName, 'Email': $('#txtAdjusterEmail').css('display') == "block" ? $('#txtAdjusterEmail').val() : $('#DDLEmail :selected').text(), 'Part': Parts, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnGetSupersededInfo);
    }
    function ReturnGetSupersededInfo(msg) {
        var info = jQuery.parseJSON(msg.d);
        $('#superseded_info li:nth(0)').text("Description: " + info[0][0].Description);

        if (user.UserName == 'Guest') {
            $('#superseded_info li:nth(1)').text("").hide();
        } else {
            $('#superseded_info li:nth(1)').text("List Price: " + makeDollar(info[0][0].List)).show();
        }

        $('#superseded_info li:nth(2)').text("Your Price: " + makeDollar(info[0][0].Your)).css('font-weight', 'bold');
        $('#superseded_info li:nth(3)').text("Core: " + makeDollar(info[0][0].Core));
        $('#superseded_info li:nth(4)').text("In Stock: " + info[0][0].Stock);
    }

    function GetPartInfo(quote) {
        //dynamically set position of loading overlay
        $('#order_grid_overlay').css({ 'height': $('#order_grid').height(), 'top': $('#order_grid').offset().top, 'left': $('#order_grid').offset().left });

        $('#order_grid_overlay').show();

        if ($('.make2').val() == '40') {
            alert('Please select a vehicle Make');
            $('#order_grid_overlay').hide();
            return false;
        }

        var Parts = new Array();

        $('#order_grid tbody tr').each(function () {
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

                Part = { 'PartNumber': partnumber, 'Quantity': quantity }
                Parts.push(Part);
            }
        });

        //Parts.pop();

        var urlMethod = "../OEMWebService.asmx/GetOEMPrice";
        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();
        var json = { 'Emulate': emulate, 'Quote': quote, 'MakeID': $('.make2').val(), 'Name': user.UserName, 'Email': $('#txtAdjusterEmail').css('display') == "block" ? $('#txtAdjusterEmail').val() : $('#DDLEmail :selected').text() , 'Part': Parts, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnGetPartInfo);
    }

    var hasCore = false;

    function ReturnGetPartInfo(msg) {
        partData = jQuery.parseJSON(msg.d);

        
        $('#pickup-cutoff-time').html("The cutoff time to order and pickup today is 5:00pm EST</b>.");
        var d1 = new Date(), d2 = new Date(d1);
        d2.setMinutes(d1.getMinutes() + (partData[1][0].CutoffMinutes * 1));
        var newYear = new Date();
        newYear = new Date(newYear.getFullYear(), newYear.getMonth(), newYear.getDate(), 17);
        ($('#pickup-countdown').hasClass('hasCountdown')) ? $('#pickup-countdown').countdown('change', { until: newYear }) : $('#pickup-countdown').countdown({ until: newYear });

        if (partData[1][0].Cutoff == '6:00:00 PM') {
            $('#delivery-cutoff-time').html("Order by 6:00pm EST for next day delivery</b>.");
        }
        else {
            $('#delivery-cutoff-time').html("Time before next truck leaves. Please order 15 minutes prior to this time.</b>.");
        }

        var d3 = new Date(), d4 = new Date(d1);
        d4.setMinutes(d3.getMinutes() + (partData[1][0].CutoffMinutes * 1));
        ($('#delivery-countdown').hasClass('hasCountdown')) ? $('#delivery-countdown').countdown('change', { until: d4 }) : $('#delivery-countdown').countdown({ until: d4 });

        if (user.Client == 'Fitz') {
            $('#pickup-delivery').hide();
            $('#pickup').hide();
            $('#delivery').hide();
        }


        var count = 1;
        var subTotal = 0;
        var subTotalList = 0;

        hasCore = false; //reset hasCore

        $('#part-summary tbody').empty();

        $(partData[0]).each(function () {
            var currentRow = $("#order_grid tbody tr:nth-child(" + count + ")");
            currentRow.find("td:nth-child(4)").css({ 'color': 'black', 'font-weight': 'normal' }); //reset style if previously made red

            if (partData[0][count - 1].Core) hasCore = true; //set hasCore to true if a non-zero value

            if (partData[0][count - 1].Description == "No Parts Found") {
                currentRow.find("td:nth-child(4)").text(partData[0][count - 1].Description);
                currentRow.find("td:nth-child(4)").css({ 'color': 'red', 'font-weight': 'bold' });

                //reset rest of fields
                currentRow.find("td:nth-child(5)").text("");
                currentRow.find("td:nth-child(6)").text("");
                currentRow.find("td:nth-child(7)").text("");
                currentRow.find("td:nth-child(8)").text("");
                currentRow.find("td:nth-child(9)").text("");
                currentRow.find("td:nth-child(10)").text("");
            } else {
                if (user.UserName == 'Guest') {
                    $('#guest-message').fadeIn();
                } else {
                    $('#checkout').fadeIn();
                }

                var total = currentRow.find('.Quantity').val() * partData[0][count - 1].Your;
                var totalList = currentRow.find('.Quantity').val() * partData[0][count - 1].List;
                var savings = totalList - total;

                subTotal = subTotal + total;
                subTotalList = subTotalList + totalList;

                currentRow.find("td:nth-child(4)").text(partData[0][count - 1].Description);
                if (partData[0][count - 1].SupersededPart) currentRow.find("td:nth-child(4)").append("<br /><span class='yellowalert'>Superseded Part: <span class='superseded_part'>" + partData[0][count - 1].SupersededPart + "</span></span>");
                currentRow.find("td:nth-child(5)").text(makeDollar(partData[0][count - 1].List));

                currentRow.find("td:nth-child(7)").text(makeDollar(partData[0][count - 1].Core));
                currentRow.find("td:nth-child(8)").text(partData[0][count - 1].Stock);
                currentRow.find("td:nth-child(9)").text(makeDollar(total));

                if (user.UserName == 'Guest') {
                    currentRow.find("td:nth-child(6)").text("Account Only");
                    currentRow.find("td:nth-child(10)").text("Account Only");
                    currentRow.find("td:nth-child(9)").text(makeDollar(partData[0][count - 1].List));
                } else {
                    currentRow.find("td:nth-child(10)").text(makeDollar(savings));
                    currentRow.find("td:nth-child(6)").text(makeDollar(partData[0][count - 1].Your));
                }

                //fill in order summary...
                //console.log(partData[0][count - 1].Description.length);
                var short_description;
                (partData[0][count - 1].Description.length < 14) ? short_description = partData[0][count - 1].Description : short_description = partData[0][count - 1].Description.substring(0, 11) + '..';

                $('#part-summary tbody').append('<tr><td class="count">' + count + '</td><td>' + partData[0][count - 1].PartNumber + '</td><td>' + currentRow.find('.Quantity').val() + '</td><td>' + short_description + '</td><td>' + total + '</td></tr>');
            }
            count++;
        });

        var taxrate = 0.045;
        var taxamount = subTotal * taxrate;

        var shipping = 20.00;
        var shipping = 0.00;

        if (user.UserName == 'Guest') {
            $("#order_grid tfoot tr td:nth-child(4), #part-summary tfoot tr:nth(1) td:nth(2)").text(makeDollar(subTotalList));
        } else {
            $("#order_grid tfoot tr td:nth-child(4), #part-summary tfoot tr:nth(1) td:nth(2)").text(makeDollar(subTotal));
            $("#order_grid tfoot tr td:nth-child(4), #part-summary tfoot tr:nth(2) td:nth(2)").text(makeDollar(subTotal));
        }

        //        $("#part-summary tfoot tr:nth(2) td:nth(2)").text(makeDollar(taxamount));
        $("#part-summary tfoot tr:nth(3) td:nth(2)").text(makeDollar(shipping));
        var grandTotal = calcTotal();
        //        $("#part-summary tfoot tr:nth(4) td:nth(2)").text('$' + makeDollar(grandTotal));
        $("#part-summary tfoot tr:nth(2) td:nth(2)").text('$' + makeDollar(grandTotal));
        var grandSavings = subTotalList - subTotal;

        if (user.UserName == 'Guest') {
            $("#order_grid tfoot tr td:nth-child(5)").text("");
        } else {
            $("#order_grid tfoot tr td:nth-child(5)").text(makeDollar(grandSavings));
        }

        $('#order_grid_overlay').hide();

        //run additional logic for admin page
        if ($('#emulate').html() != 'false') RenderAdmin();
        if ($('#ctl00_ctl00_Content_CustomerPartsPortal_autonation').html() == 'yes') RenderAutonation();

        //show/hide checkout button
        if (partData[1][0].NoCheckout == "yes") {
            $("#checkout").hide();
        } else {
            $("#checkout").show();
        }

    }

    //admin function real quick...
    function RenderAdmin() {
        //hide some stuff that admins dont need

        $('#cutoff').hide();
        $('.shipping li.description').hide();

        //remove old stock tables
        $('#stockoverall').remove();
        $('h2.warehouse_info').remove();

        var i = 0;
        $('#order_grid tbody tr:not(:last)').find('td:nth(7)').each(function () {
            var text = $(this).html();
            var position = $(this).position();

            var stockhtml = "<table><tr><td>Hyperion</td><td>Name</td><td>Phone</td><td>Contact</td></tr>";

            $(partData[0][i].StockLevels).each(function () {

                stockhtml += "<tr><td>" + this.Hyperion + "</td><td>" + this.Name + "</td><td>" + this.Phone + "</td><td>" + this.Contact + "</td></tr>";
            });

            stockhtml += "</table>";

            $(this).css({ 'text-decoration': 'underline', 'color': 'blue', 'cursor': 'pointer' }).hover(
                function () {
                    $('#stock-level').html(stockhtml);

                    $('#stock-level').css({ 'top': (position.top + 52) + "px", 'left': (position.left - 455) + "px" }).show();
                }, function () {
                    $('#stock-level').hide();
                }
            );
            i++;
        });

        var stockoverall = "<h2 class='warehouse_info'>Warehouse Info</h2><table id='stockoverall'><tr><td>Note</td><td>Hyperion</td><td>Name</td><td>Phone</td><td>Contact</td></tr>";
        $(partData[1][0].StockLevels).each(function () {
            stockoverall += "<tr><td>" + this.Note + "</td><td>" + this.Hyperion + "</td><td>" + this.Name + "</td><td>" + this.Phone + "</td><td>" + this.Contact + "</td></tr>";
        });
        stockoverall += "</table>";

        $('#shipping').prepend(stockoverall);
    }

    function RenderAutonation() {

        var i = 0;
        $('#order_grid tbody tr:not(:last)').find('td:nth(7)').each(function () {
            var text = $(this).html();
            var position = $(this).position();

            var stockhtml = "<table><tr><td>Hyperion</td><td>Name</td><td>Phone</td><td>Contact</td></tr>";

            $(partData[0][i].StockLevels).each(function () {

                stockhtml += "<tr><td>" + this.Hyperion + "</td><td>" + this.Name + "</td><td>" + this.Phone + "</td><td>" + this.Contact + "</td></tr>";
            });

            stockhtml += "</table>";

            $(this).css({ 'text-decoration': 'underline', 'color': 'blue', 'cursor': 'pointer' }).hover(
                function () {
                    $('#stock-level').html(stockhtml);

                    $('#stock-level').css({ 'top': (position.top + 52) + "px", 'left': (position.left - 455) + "px" }).show();
                }, function () {
                    $('#stock-level').hide();
                }
            );
            i++;
        });
    }

    $('#checkout-radios input[name=checkout-type]').change(function () {
        switch (this.value) {
            case "pickup":
                $('#delivery').hide();
                $('#part-summary tfoot tr:nth(3)').hide();
                $('#pickup').fadeIn(200);
                break;
            case "delivery":
                $('#pickup').hide();
                $('.checkout-info .form_contain').fadeIn(200);
                $('#delivery').fadeIn(200);
                break;
        }

        setTimeout(function () {
            var total = calcTotal();
            //console.log(total);
            //            $("#part-summary tfoot tr:nth(1) td:nth(2)").html('$' + makeDollar(total));
            //            $("#part-summary tfoot tr:nth(2) td:nth(2)").html('$' + makeDollar(total));
        }, 500);
    });

    function calcTotal() {
        var total = 0;
        for (i = 1; i <= 1; i++) {
            total = total + $("#part-summary tfoot tr:nth(" + i + "):visible td:nth(2)").html() * 1;
        }
        return total;
    }

    function GetQuotes() {
        var urlMethod = "../OEMWebService.asmx/GetQuotes";
        var json = { 'Name': user.UserName, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, ReturnGetQuotes);
    }
    function ReturnGetQuotes() {
        return true;
    }
    function PlaceOrder() {
        var a = "test";
        var urlMethod = "../OEMWebService.asmx/PlaceOrder";

        var emulate = ($('#emulate').html() == '') ? "false" : $('#emulate').html();

        var data = { 'Emulate': emulate
            , 'Name': user.UserName
            , 'MakeID': $('.make2').val()
            , 'Model': +''
            , 'VIN': $("#vin2").val()
            , 'Mileage': +''
            , 'Year': +''
            , 'Drive': +''
            , 'Trans': +''
            , 'ContractNo': +''
            , 'AuthNo': +''
            , 'Owner': +''
            , 'PO': $("#po").val()
            , 'Shop': $("#shop").val()
            , 'Address': $("#address").val()
            , 'City': $("#city").val()
            , 'State': $("#state").val()
            , 'Zip': $("#zip").val()
            , 'Contact': $("#contact").val()
            , 'Phone': $("#phone").val()
            , 'Warranty': +''
            , 'WarrantyCost': +''
            , 'WarrantyDate': +''
            , 'WarrantyMileage': +''
            , 'ShippingCost': +'0.00'
            , 'ReturnShippingCost': +'0.00'
            , 'ShippingType': +'delivery'
            , 'Parts': partData[0]
            , 'client': user.Client
            , 'IP': $('.ip').html()
        };
        var jsonData = JSON.stringify(data);
        SendAjax(urlMethod, jsonData, ReturnPlaceOrder);
    }
    function ReturnPlaceOrder(msg) {
        //  var response = msg.split('/');

        // $("#confirm-dialog").html("<p>Your order has been submitted.</p><p><b>Order number: " + response[0] + "</b></p><p>You saved enough money to purchase " + response[1] + "</p>");
        var response = msg;
        $("#confirm-dialog").html("<p>Your order has been submitted.</p><p><b>Order number: " + response.d + "</b></p>");

        $("#confirm-dialog").dialog("open");
    }
    $("#confirm-dialog").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function () {
                window.location = "PartsPortal.aspx";
            }
        }
    });

});
