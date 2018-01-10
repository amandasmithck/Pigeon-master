//Global Variables Start
var orderid;
var currentPartID;

//Global Variables End

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

function SendAjaxAsyncFalse(urlMethod, jsonData, returnFunction) {
    $.ajax({
        type: "POST",
        contentType: 'application/json',
        url: urlMethod,
        async:false,
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

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function GetCurrentPartID()
{
    if (currentPartID == null)
    {
        currentPartID = "";
    }
    return currentPartID;
}

function SetCurrentPartID(pID)
{
    //window.alert(pID);

    currentPartID = pID;
    return false;
}



//*******************document.ready*******************************
$(document).ready(function () {

    if (GetQueryStringParams('orderid') == null) {

    } else {

   
        //Header
        GetHeader();
        $('#docs-modal').on('shown', function () {
            GetDocs();
        })

        $('#history-modal').on('shown', function () {
            GetHistory();
        })
       
        $('#quick-switch').keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                document.location.href='order.aspx?orderid=' + $(this).val();
            }
        });
        $('#cancel-order').click(function () {
            $('#cancel-modal').modal('hide')
            var urlMethod = "../OrderWebService.asmx/CancelOrder";
            var json = { 'orderid': orderid, 'enteredby': user.UserName, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                if (msg.d == true) {
                    noty({
                        text: '<strong>Order Cancelled</strong>',
                        type: 'success',
                        dismissQueue: true,
                        layout: 'topRightOrder',
                        theme: 'defaultTheme',
                        timeout: 2000,
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });
                    $('.brand').css('color', 'red');
                } else {
                    noty({
                        text: '<strong>Error cancelling order.<strong><br/><br/> Please check your values and try again.<br/><br/>If problem persists please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                        type: 'error',
                        dismissQueue: true,
                        layout: 'topRightOrder',
                        theme: 'defaultTheme',
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });

                }

            });

            
            
        });
        
        //Add Part
        $('#addpart').click(function () {
            $('#cancel-modal').modal('hide')
            var urlMethod = "../OrderWebService.asmx/AddPart";
            var json = { 'orderid': orderid, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                if (msg.d == true) {
                    noty({
                        text: '<strong>Part Added</strong>',
                        type: 'success',
                        dismissQueue: true,
                        layout: 'topRightOrder',
                        theme: 'defaultTheme',
                        timeout: 2000,
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });
                    $('.brand').css('color', 'red');
                } else {
                    noty({
                        text: '<strong>Error Adding Part.<strong><br/><br/> Please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                        type: 'error',
                        dismissQueue: true,
                        layout: 'topRightOrder',
                        theme: 'defaultTheme',
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });

                }

            });



        });
       
        //PARTS AVAILABILITY
        $('#divPartsAvailabilityModal').on('shown', function () { GetPartsAvailabilityModal(); });

        //SHIPMENT DATA CONTROLS START
        $('#lnkCreateShipment').on('click', function () {
            GetListOfPartsForShipping();                
            GetListOfVendor(orderid); //get collection of vendors
        });
            
        //SHIPMENT DATA CONTROLS START
        $('#lnkReturnCreateShipment').on('click', function () { GetReturnOrderPartsForShipping(); });

        $("#btnGetRates").click(function () {GetRates(); });

        $("#btnCreateShipment").click(function () { CreateShipment(); });

        $("#btnBackShipmentModal").click(function () {
            var divShipmentLoaderID = "divShipmentLoader";
            var divShipmentCreationScreenID = "divShipmentCreationScreen";
            var divShipmentResultScreenID = "divShipmentResultScreen";
            var divShipmentRatesSectionID = "divShipmentRatesSection";

            $("#" + divShipmentLoaderID).hide();
            $("#" + divShipmentCreationScreenID).hide();
            $("#" + divShipmentResultScreenID).hide();
            $("#" + divShipmentRatesSectionID).show();
        });

        $("#btnCancelShipmentModal").click(function () {
            var divShipmentLoaderID = "divShipmentLoader";
            var divShipmentCreationScreenID = "divShipmentCreationScreen";
            var divShipmentResultScreenID = "divShipmentResultScreen";
            var divShipmentRatesSectionID = "divShipmentRatesSection";

            $("#" + divShipmentLoaderID).hide();
            $("#" + divShipmentCreationScreenID).hide();
            $("#" + divShipmentResultScreenID).hide();
            $("#" + divShipmentRatesSectionID).show();
        });
 
        $("#btnCancelShipment").click(function () { DeleteShipmentByTrackingNumber(); });
        //SHIPMENT DATA CONTROLS END

        $("#ddlPackageType").change(function () {
           
            if (parseInt(this.value) == 10) {
                $("#divDimensions").show();
            }
            else {
                $("#divDimensions").hide();
            }

        });

        $("#ddlDropOffType").change(function () {

            if (parseInt(this.value) == 3) {
                              
                var currentVendorDropDownLen = $('#ddlVendors').children('option').length;
                if (currentVendorDropDownLen != null && currentVendorDropDownLen > 0)
                {
                    $("#divScheduledPickup").show();
                    return;
                }
             
                //get collection of vendors
                GetListOfVendor(orderid);
                $("#divScheduledPickup").show();
            }
            else {
                $("#divScheduledPickup").hide();
            }

        });


        $("#divDeleteShipment").on('hide', function () {   
            var divShipmentDeletionMsgID = "divShipmentDeletionMsg";
            var txtTrackingNumberID = "txtTrackingNumber";
            $("#" + txtTrackingNumberID).val("");
            $("#" + divShipmentDeletionMsgID).empty();

        });

        //$("#ddlKOParts").on('change', function () {
        //    GetPartOrderByID(this.value);
        //});

        //GetKOListOfParts(orderid);

        //Notes
        GetNotes();
        
        //Order Info
        GetOrderInfo();
        

        //Part Info
        GetParts();

        //Parts Summary Section
        GetTotalPartsSummary(orderid);

        //Warranty

        //Invoices
        GetInvoices();
        $('#delete-invoice-modal').on('shown', function () {
            $(".invoice-type").popover('hide');
            if (accounting.formatNumber($('#invoices-table tr.info td.invoice-amt').text()) - accounting.formatNumber($('#invoices-table tr.info td.invoice-amt-paid').text()) != 0) {
                $('#delete-invoice-body').html('Unable to delete a non-zero invoice.');
                $('#delete-invoice-close').text('OK');
                $('#delete-invoice').hide();
            } else {
                $('#delete-invoice-body').html('Are you sure you wish to delete this invoice?');
                $('#delete-invoice-close').text('Nope');
                $('#delete-invoice').show();
            }
        })

    }
    var $content = $('#content');
    $content.find('div.widgets').wl_Widget();

    var $part_content = $('#part_content');
    $part_content.find('div.widgets').wl_Widget();

    var $parts_total_summary_content = $('#parts_total_summary_content');
    $parts_total_summary_content.find('div.widgets').wl_Widget();


    //var $ko_tester = $('#ko_tester');
    //$ko_tester.find('div.widgets').wl_Widget();

});





//********************Header info*******************************

function GetHeader() {
    orderid = GetQueryStringParams('orderid')
    $('#order-id').html(orderid);
    document.title = orderid;
    var urlMethod = "../OrderWebService.asmx/GetHeaderInfo";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        $('#customer').html(data.Customer);
        $('#vehicle').html(data.Vehicle);
        $('#vinno').html(data.VinNo);
        $('#mileage').html('Mileage: ' + data.Mileage);
        $('#dateordered').html(data.DateOrdered);
        if (data.Cancelled == true) { $('.brand').css('color', 'red') };
    });
}



//********************Order Info**************************
function GetOrderInfo() {
    var urlMethod = "../OrderWebService.asmx/GetOrderInfo";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        $("#order-info").append($('#orderInfoTemplate').tmpl(data))
        $('#order-loader').hide();
        $('.select-drive-type').click(function () {
            $('#order-drive').text($(this).text());
        });
        $('.select-trans-type').click(function () {
            $('#order-trans').text($(this).text());
        });
        $('#btnSaveOrder').click(function () {
            SaveOrderInfo();
            $('#txtNewNote').text('Enter new notes here...');
        });
    });
}

function SaveOrderInfo() {
    var urlMethod = "../OrderWebService.asmx/SaveOrderInfo";
    var json = { 'orderid': orderid, 'adjustername': $('#order-adjuster').text(), 'contractno': $('#order-contract').text(), 'authorizationno': $('#order-auth').text(), 'autoowner': $('#order-owner').text(), 'contractenddate': $('#order-contract-date').text(), 'contractendmiles': $('#order-contract-miles').text(), 'mileage': $('#order-mileage').text(), 'autoyear': $('#order-year').text(), 'automodel': $('#order-model').text(), 'drive': $('#order-drive').text(), 'transmission': $('#order-trans').text(), 'transmission': $('#order-trans').text(), 'vinno': $('#order-vin').text(), 'enteredby': user.UserName, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        if (msg.d == true) {
            noty({
                text: '<strong>Order Saved</strong>',
                type: 'success',
                dismissQueue: true,
                layout: 'topRightOrder',
                theme: 'defaultTheme',
                timeout: 2000,
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                }
            });
        } else {
            noty({
                text: '<strong>Error saving order info.<strong><br/><br/> Please check your values and try again.<br/><br/>If problem persists please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                type: 'error',
                dismissQueue: true,
                layout: 'topRightOrder',
                theme: 'defaultTheme',
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                }
            });

        }

    });
}

//********************Parts**************************
function GetParts() {
    var urlMethod = "../OrderWebService.asmx/GetOrderParts";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        $('#parts-loader').hide();

        //set current part id
        if(data != null){SetCurrentPartID(data[0].PartID);}
        
        $("#parts_list").append($('#partsTabTemplate').tmpl(data))
    //    var oemparts = _.where(data, { PartDescGroup: "OEM", Incorrect: false, Defect: false });
     //   var smallparts = _.where(data, { PartDescGroup: "Small Parts", Incorrect: false, Defect: false });
        $('#parts_list li').first().addClass('active')
        // if (oemparts.length > 0) {
        //     $('#parts_list').append('<legend>OEM Availability</legend><div id="oem-avail"></div><div id="oemavail-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
        //     var urlMethod = "../OrderWebService.asmx/GetOEMAvailability";
        //     var json = { 'orderid': orderid, 'client': user.Client };
        //     var jsonData = JSON.stringify(json);
        //     SendAjax(urlMethod, jsonData, function (msg) {
        //         data = jQuery.parseJSON(msg.d);
        //         $("#oem-avail").append($('#oemAvailTemplate').tmpl(data))
        //         $(data).each(function () {
        //             var hyp = $(this)[0].Hyperion;
                     
        //             $.each($(this)[0].Parts, function () {
        //                 $("#" + hyp).append("<span style='font-size:11px'><li>" + this.Description + "<br/>" + this.PartNumber + "</li></span>");
        //             });
                     
        //         });
        //         $('#oemavail-loader').hide();
        //     });
        //}
        //if (smallparts.length > 0) {
        //    $('#parts_list').append('<legend>Small Part Options&nbsp;&nbsp;</legend><div id="small-options"></div><div id="smalloptions-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div>');
        //    var urlMethod = "../OrderWebService.asmx/GetSmallPartOptions";
        //    var json = { 'orderid': orderid, 'client': user.Client };
        //    var jsonData = JSON.stringify(json);
        //    SendAjax(urlMethod, jsonData, function (msg) {
        //        data = jQuery.parseJSON(msg.d);
        //        $("#small-options").append($('#smallOptionsTemplate').tmpl(data))
        //        $(data).each(function () {
        //            var partid = "small-" + $(this)[0].PartID;
        //            $("#" + partid).append('<table id="table-' + partid + '" class="table table-striped table-bordered table-hover table-condensed"><th>Vendor</th><th>Part No</th><th>Brand</th><th>Cost</th><th>Core</th><th>Sell Price</th><th>Stock</th></table>');

        //            $.each($(this)[0].Options, function () {
        //                if (this.Matched == true) {
        //                    $("#table-" + partid).append("<tr><td>" + this.Vendor + "</td><td>" + this.PartNo + "</td><td>" + this.Brand + "</td><td>" + this.OurCost + "</td><td>" + this.CorePrice + "</td><td>" + this.TheirPrice + "</td><td>" + this.Stock + "</td></tr>");
        //                } else {
        //                    $("#table-" + partid).append("<tr style='font-size:10px'><td>" + this.Vendor + "</td><td>" + this.PartNo + "</td><td>" + this.Brand + "</td><td>" + this.OurCost + "</td><td>" + this.CorePrice + "</td><td>" + this.TheirPrice + "</td><td>" + this.Stock + "</td></tr>");
        //                }
        //            });
                   
        //        });
        //        $('#smalloptions-loader').hide();
        //    });
        //}
        //TODO: get real sell and costs price
        //$('#parts_list').append('<li class="profit"><table class="table table-striped table-bordered table-hover table-condensed"><tr><td>Total Sell</td><td style="text-align:right">$1,699.00</td></tr><tr><td>Total Cost</td><td style="text-align:right">$1,221.05</td></tr><tr><th>Gross Profit</th><th style="text-align:right">$477.46</th></tr></table></li>').tmpl(data);

        $("#parts_content").append($('#partsContentTemplate').tmpl(data))
        $('#parts_content div').first().addClass('active')

        //$('#parts_list li a').each(function () {
        //    if ($(this).attr('prev') > 0) {
        //        var row = $('#parts_list li a[href="#' + $(this).attr('prev') + '"]').attr('row');
        //        var desc = $(this).html();
        //        $(this).html(desc + ' {replaces #' + row + '}');
        //    }
        //})
      
        $('.dt-picker').datepicker()
             .on('changeDate', function (ev) {
                 $('.dt-picker').datepicker('hide');
             });

    });
}


//******************PARTS AVAILABILITY******************
function GetPartsAvailabilityModal()
{
    var hfPartsAvailabilitySectionOpenedID = "hfPartsAvailabilitySectionOpened";
    var isOpened = $("#" + hfPartsAvailabilitySectionOpenedID).val();

    //window.alert(parseInt(isOpened));
    if (parseInt(isOpened) == 1)
    { //this has already been opened once so the data should still be loaded
        return false;
    }
   
    //first time opening ... set the isopened value to 1 and load information
    $("#" + hfPartsAvailabilitySectionOpenedID).val("1");

        var urlMethod = "../OrderWebService.asmx/GetOrderParts";
        var json = { 'orderid': orderid, 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            data = jQuery.parseJSON(msg.d);
            $('#parts_availability_loader').hide();

            //$("#parts_availability_list").append($('#partsTabTemplate').tmpl(data))
            var oemparts = _.where(data, { PartDescGroup: "OEM", Incorrect: false, Defect: false });
            var smallparts = _.where(data, { PartDescGroup: "Small Parts", Incorrect: false, Defect: false });
            $('#parts_availability_list li').first().addClass('active');

            if (oemparts.length > 0) {
                $('#parts_availability_list').append('<div class="divGeneralAvailSection"> <div class="divAvailSectHdr" >OEM Availability</div><div class="divAvailSectBody"   id="oem-avail"></div><div class="divAvailLoader" id="oemavail-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div></div>');
                var urlMethod = "../OrderWebService.asmx/GetOEMAvailability";
                var json = { 'orderid': orderid, 'client': user.Client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    data = jQuery.parseJSON(msg.d);
                    $("#oem-avail").append($('#oemAvailTemplate').tmpl(data))
                    $(data).each(function () {
                        var hyp = $(this)[0].Hyperion;

                        $.each($(this)[0].Parts, function () {
                            $("#" + hyp).append("<span style='font-size:11px'><li>" + this.Description + "<br/>" + this.PartNumber + "</li></span>");
                        });

                    });
                    $('#oemavail-loader').hide();
                });
            }
            if (smallparts.length > 0) {
                $('#parts_availability_list').append('<div class="divGeneralAvailSection"><div class="divAvailSectHdr">Small Part Options&nbsp;&nbsp;</div><div class="divAvailSectBody" id="small-options"></div><div class="divAvailLoader" id="smalloptions-loader" class="loader"><img src="/images/ajax-loader-blue.gif" /></div></div>');
                var urlMethod = "../OrderWebService.asmx/GetSmallPartOptions";
                var json = { 'orderid': orderid, 'client': user.Client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    data = jQuery.parseJSON(msg.d);
                    $("#small-options").append($('#smallOptionsTemplate').tmpl(data))
                    $(data).each(function () {
                        var partid = "small-" + $(this)[0].PartID;
                        $("#" + partid).append('<table id="table-' + partid + '" class="table table-striped table-bordered table-hover table-condensed"><th>Vendor</th><th>Part No</th><th>Brand</th><th>Cost</th><th>Core</th><th>Sell Price</th><th>Stock</th></table>');

                        $.each($(this)[0].Options, function () {
                            if (this.Matched == true) {
                                $("#table-" + partid).append("<tr><td>" + this.Vendor + "</td><td>" + this.PartNo + "</td><td>" + this.Brand + "</td><td>" + this.OurCost + "</td><td>" + this.CorePrice + "</td><td>" + this.TheirPrice + "</td><td>" + this.Stock + "</td></tr>");
                            } else {
                                $("#table-" + partid).append("<tr style='font-size:10px'><td>" + this.Vendor + "</td><td>" + this.PartNo + "</td><td>" + this.Brand + "</td><td>" + this.OurCost + "</td><td>" + this.CorePrice + "</td><td>" + this.TheirPrice + "</td><td>" + this.Stock + "</td></tr>");
                            }
                        });

                    });
                    $('#smalloptions-loader').hide();
                });
            }
            //TODO: get real sell and costs price
            //$('#parts_list').append('<li class="profit"><table class="table table-striped table-bordered table-hover table-condensed"><tr><td>Total Sell</td><td style="text-align:right">$1,699.00</td></tr><tr><td>Total Cost</td><td style="text-align:right">$1,221.05</td></tr><tr><th>Gross Profit</th><th style="text-align:right">$477.46</th></tr></table></li>').tmpl(data);

            $('#parts_availability_list li a').each(function () {
                if ($(this).attr('prev') > 0) {
                    var row = $('#parts_availability_list li a[href="#' + $(this).attr('prev') + '"]').attr('row');
                    var desc = $(this).html();
                    $(this).html(desc + ' {replaces #' + row + '}');
                }
            })
          

        });
    }

//********************Notes*******************************

function GetNotes() {
    var urlMethod = "../OrderWebService.asmx/GetNotes";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function(msg) {
        data = jQuery.parseJSON(msg.d);
        $('#notes-table tr:not(:first)').remove();
        $("#notes-table").append($('#notesTemplate').tmpl(data))
        $('#notes-loader').hide();
        $('#txtNewNote').keydown(function (e) {
            if (event.keyCode == 13) {
                if ($('#txtNewNote').text() != 'Enter new notes here...') {
                    InsertNote($('#txtNewNote').text(), $('#chkVendor').prop('checked'));
                    $('#txtNewNote').text('Enter new notes here...');
                    e.preventDefault();
                }
            }

        });
        //$('#btnSaveNote').click(function () {
        //    InsertNote($('#txtNewNote').text(), $('#chkVendor').prop('checked'));
        //    $('#txtNewNote').text('Enter new notes here...');
        //});
        $('#txtNewNote').click(function () {
            if ($('#txtNewNote').text() == 'Enter new notes here...') {
                $(this).text('');
            }
        })
        $('#notes-less').click(function () {
            $('#notes').css({ 'height': '250px' });
            $('#notes-all').show()
            $('#notes-more').show()
            $('#notes-more').text('More');
            $(this).hide();
        });
        $('#notes-more').click(function () {
            $('#notes').css({ 'height': '500px' });

            $('#notes-all').show()
            $('#notes-less').show()
            $(this).hide();
        });
        $('#notes-all').click(function () {
            $('#notes').css({ 'height': '' });
            $('#notes-less').show()
            $('#notes-less').text('Fewer');
            $('#notes-more').show()
            $('#notes-more').text('Few');
            $(this).hide();
        });

    });
}

function InsertNote(note,vendor) {
    var urlMethod = "../OrderWebService.asmx/AddNote";
    var json = { 'orderid': orderid, 'note': note, 'vendor': vendor, 'enteredby': user.UserName, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        if (msg.d==true) {
            noty({
                text: '<strong>Note Saved</strong>',
                type: 'success',
                dismissQueue: true,
                layout: 'topRightOrder',
                theme: 'defaultTheme',
                timeout: 2000,
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                }
            });
            $('#txtNewNote').val('');
            $('#chkVendor').prop('checked', false);
            $('#notes-loader').show();
            GetNotes();
        } else{
            noty({
                text: '<strong>Error saving note.<strong><br/><br/> Please check your values and try again.<br/><br/>If problem persists please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                type: 'error',
                dismissQueue: true,
                layout: 'topRightOrder',
                theme: 'defaultTheme',
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                }
            });

        }

    });
}

//********************Documents*******************************
function GetDocs() {
    $('#docs-loader').show();
    var urlMethod = "../OrderWebService.asmx/GetDocs";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        $('#docs-table tr:not(:first)').remove();
        $("#docs-table").append($('#docsTemplate').tmpl(data))
        $('.doc-delete').click(function () {
            DeleteDoc($(this).attr('rel'));
        });
        $('#docs-loader').hide();
    });
}

function DeleteDoc(docid) {
    $('#docs-loader').show();
    var urlMethod = "../OrderWebService.asmx/DeleteDoc";
    var json = { 'docid': docid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        if (msg.d == true) {
            GetDocs();
        } else {
            $('#docs-loader').hide();
            noty({
                text: '<strong>Error deleting document.<strong><br/><br/> Please try again.<br/><br/>If problem persists please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                type: 'error',
                dismissQueue: true,
                layout: 'topRightOrder',
                theme: 'defaultTheme',
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                }
            });

        }
       
    });
}

$(function () {
    $('#fileupload').fileupload({
        replaceFileInput: false,
        formData: function (form) {
            return [{ name: "orderid", value: orderid }, { name: "uploadedby", value: user.UserName }]
        },
        dataType: 'json',
        url: '../DocumentHandler.ashx',
        add: function (e, data) {
            $('#docs-loader').show();
            data.submit();
        },
        done: function (e, data) {
            $.each(data.result, function (index, file) {
                // $('<p/>').text(file.name).appendTo(document.body);
                GetDocs();
            });


        }
    });
});


//********************History*******************************
function GetHistory() {
    var urlMethod = "../OrderWebService.asmx/GetHistory";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        $('#history-table tr:not(:first)').remove();
        $("#history-table").append($('#historyTemplate').tmpl(data))
        $('#history-loader').hide();

    });
}

//********************Invoices*******************************
function ViewInvoice(invoiceid) {

    var url = "ViewInvoice.aspx?invoiceid=" + invoiceid;
    var win = window.open(url, '_blank', 'width=800,height=800,toolbar=no,status=no,scrollbars=yes,resizable=yes,location=no,menu=no,directories=no,top=auto');
    win = null;
    return false;

}

function InvoicePopoverContent(ElementClass) {
    if (ElementClass == 'invoice-type printable') {
        var content = '<div ><a href="#" class="save-invoice"><i class="icon-hdd icon-black" title="Save Changes" style="padding-left:10px"></i></a><a href="#" class="print-invoice"><i class="icon-print icon-black" title="Print Invoice" style="margin-left:20px"></i></a><a href="#delete-invoice-modal" data-toggle="modal"><i class="icon-remove icon-black" title="Delete Invoice" style="margin-left:30px"></i></a></div>';
    } else {
        var content = '<div ><a href="#" class="save-invoice"><i class="icon-hdd icon-black" title="Save Changes" style="padding-left:10px"></i></a><a href="#delete-invoice-modal" data-toggle="modal"><i class="icon-remove icon-black" title="Delete Invoice" style="margin-left:60px"></i></a></div>';

    }
    return content;
}

//function InvoicePayTypeContent(InvoiceID) {
//    var content = '<div style="text-align:center; font-size:14px;"><a href="#" class="pay-select" rel="' + InvoiceID + '">Check</a><br/><a href="#" class="pay-select" rel="' + InvoiceID + '">Visa</a></div>'
    
//    return content;
//}
function GetInvoices() {
    var urlMethod = "../OrderWebService.asmx/GetInvoices";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        data = jQuery.parseJSON(msg.d);
        $('#invoices-table').dataTable().fnDestroy();
        $('#invoices-table tr:not(:first)').remove();
        $("#invoices-table").append($('#invoicesTemplate').tmpl(data))
        $('#invoices-table').dataTable({ "bJQueryUI": true, "sPaginationType": "full_numbers", "bPaginate": false, "bFilter": false, "sDom": '<"top"i>rt<"bottom"flp><"clear">' });
        $('#invoices-table tr td').removeClass('sorting_1')
        $('#invoices-loader').hide();
        $('#invoices-table tbody tr').live('click', function (event) {
            $(this).addClass('info').siblings().removeClass('info');
        });
        
        $('.select-pay-type').click(function () {
            $(this).closest('div').find('.invoice-pay-type').text($(this).text());
        });
        $('.invoice-amt-paid').keydown(function (e) {
            if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 16 || event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 46 || (event.keyCode >= 96 && event.keyCode <= 105)) {
            } else {
                e.preventDefault()
            }
     
        });

        
         $('.invoice-type').popover({
            placement: 'right', //placement of the popover. also can use top, bottom, left or right
            title: '<div style="text-align:center; font-size:14px;">Invoice Options</div>', //this is the top title bar of the popover. add some basic css
            html: 'true', //needed to show html of course
            content: function () {

                return InvoicePopoverContent($(this).attr('class'));
            }
        }).click(function (e) {
            //$('.popover').hide(); //hides any previously open popovers
            //$(this).popover('show');
            $('.popover').css("width", "145px");
            $('.print-invoice').click(function () {
                $(".invoice-type").popover('hide');
                ViewInvoice($('#invoices-table tr.info').attr("rel"))
            });
            $('.save-invoice').click(function () {
                $(".invoice-type").popover('hide');
                var urlMethod = "../OrderWebService.asmx/SaveInvoice";
                var json = { 'orderid': orderid, 'invoiceid': $('#invoices-table tr.info').attr("rel"), 'amountpaid': $('#invoices-table tr.info td.invoice-amt-paid').text(), 'datepaid': $('#invoices-table tr.info td.invoice-date-paid').text(), 'paytype': $('#invoices-table tr.info a.invoice-pay-type').text(), 'checkno': $('#invoices-table tr.info td.invoice-checkno').text(), 'enteredby': user.UserName, 'client': user.Client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    if (msg.d == true) {
                        noty({
                            text: '<strong>Invoice Saved</strong>',
                            type: 'success',
                            dismissQueue: true,
                            layout: 'topRightOrder',
                            theme: 'defaultTheme',
                            timeout: 2000,
                            animation: {
                                open: { height: 'toggle' },
                                close: { height: 'toggle' },
                                easing: 'swing',
                                speed: 500 // opening & closing animation speed
                            }
                        });

                    } else {
                        noty({
                            text: '<strong>Error saving invoice.<strong><br/><br/> Please check your values and try again.<br/><br/>If problem persists please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                            type: 'error',
                            dismissQueue: true,
                            layout: 'topRightOrder',
                            theme: 'defaultTheme',
                            animation: {
                                open: { height: 'toggle' },
                                close: { height: 'toggle' },
                                easing: 'swing',
                                speed: 500 // opening & closing animation speed
                            }
                        });

                    }

                });
            });
            e.preventDefault()
            
        });

        $('#delete-invoice').click(function () {
            $('#delete-invoice-modal').modal('hide')
            var urlMethod = "../OrderWebService.asmx/DeleteInvoice";
            var json = { 'orderid': orderid, 'invoiceid': $('#invoices-table tr.info').attr("rel"), 'enteredby': user.UserName, 'client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                if (msg.d == true) {
                    noty({
                        text: '<strong>Invoice Deleted</strong>',
                        type: 'success',
                        dismissQueue: true,
                        layout: 'topRightOrder',
                        theme: 'defaultTheme',
                        timeout: 2000,
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });
                    GetInvoices();//refresh invoices
                } else {
                    noty({
                        text: '<strong>Error deleting invoice.<strong><br/><br/> Please check your values and try again.<br/><br/>If problem persists please provide a screenshot of this error to I.T.<br/><br/>' + msg.d,
                        type: 'error',
                        dismissQueue: true,
                        layout: 'topRightOrder',
                        theme: 'defaultTheme',
                        animation: {
                            open: { height: 'toggle' },
                            close: { height: 'toggle' },
                            easing: 'swing',
                            speed: 500 // opening & closing animation speed
                        }
                    });

                }

            });
        });
        
    });
}

//SHIPPING METHODS START

function GetListOfPartsForShipping()
{
    try {
        $("#hfShipmentMode").val("1");
        $("#divReturnPickup").hide();
        SetServicerIntoShipmentWidget();
        $("#divAdditionalReturnShippingData").hide();
        var urlMethod = "../OrderWebService.asmx/GetOrderPartsForShipping";
        var json = { 'orderid': orderid, 'client': user.Client };
        var jsonData = JSON.stringify(json);
    
        SendAjax(urlMethod, jsonData, DisplayListOfPartsForShipping);
        return false;
    }
    catch (err) {
        window.alert(err);
    }
}

function GetReturnOrderPartsForShipping() {
    try {
        $("#hfShipmentMode").val("2");
        $("#divAdditionalReturnShippingData").show();
        $("#divReturnPickup").show();

        SetServicerIntoShipmentWidget();

         var urlMethod = "../OrderWebService.asmx/GetReturnOrderPartsForShipping";
        var json = { 'orderid': orderid, 'client': user.Client };
        var jsonData = JSON.stringify(json);

        SendAjax(urlMethod, jsonData, DisplayListOfPartsForShipping);
        return false;
    }
    catch (err) {
        window.alert(err);
    }
}

function DisplayListOfPartsForShipping(response)
{
    //check for an error response
    var errMsg = getErrorMsg(response);
     
    if (errMsg != "")
    {
        window.alert(errMsg);
        return false;
    }

    errMsg = "There are no parts to be shipped. If this alert is in error, please contact your IT administrator immediately.";
    if (response == null || response == "")
    {
        window.alert(errMsg);
        return false;
    }
    
    //ALL GOOD ...create a js template and display the parts
    var parts = jQuery.parseJSON(response.d);

    if (parts == null || parts == "")
    {
        window.alert(errMsg);
        //e.preventDefault();
        //return false;
    }

    //the above have already been validated
    var txtHeightID = "txtHeight";
    $("#" + txtHeightID).val("");
    var txtWeightID = "txtWeight";
    $("#" + txtWeightID).val("");
    var txtLengthID = "txtLength";
    $("#" + txtLengthID).val("");
    var txtWidthID = "txtWidth";
    $("#" + txtWidthID).val("");

    var jsPartsTmpl = "<tr>";
    jsPartsTmpl = jsPartsTmpl + "<td class='tdShipmentCount' ><input type='checkbox' name='ckbPart' id='ckbPart_${Row}' value='${PartID}' class='' /></td>";
    jsPartsTmpl = jsPartsTmpl + "<td class='tdShipmentVendor' style='padding-top:8px;' >${Row}.&nbsp;&nbsp;${Vendor}</td>";
    jsPartsTmpl = jsPartsTmpl + "<td class='tdShipmentDesc' style='padding-top:8px;' >${PartDescription}</td>";
    jsPartsTmpl = jsPartsTmpl + "<td class='' style='width:20px;' > <img src='../images/purple_checkmark.png' style='height:20px;display:${ShipmentCreatedDisplayValue};' title='Item Already Shipped'  /> </td>";

    jsPartsTmpl = jsPartsTmpl + "</tr>";
               
    

    $.template("partsTemplate", jsPartsTmpl);
    $("#trPartsForShipping").empty();
    $.tmpl("partsTemplate", parts).appendTo("#trPartsForShipping");
    
    return false;
}

function GetSelectedParts()
{//get values of checkboxes selected
    var listOfPartsSelected = "";

    $('input[name="ckbPart"]:checked').each(function () {
        if (listOfPartsSelected == ""){ listOfPartsSelected = this.value; }
        else {listOfPartsSelected = listOfPartsSelected + "," + this.value; }        
    });
    return listOfPartsSelected;
}

function GetRates()
{
    try
    {
        var divShipmentLoaderID = "divShipmentLoader";
        var divShipmentRatesSectionID = "divShipmentRatesSection";

        $("#" + divShipmentRatesSectionID).hide();
        $("#" + divShipmentLoaderID).fadeIn();

        var txtHeightID = "txtHeight";
        var height = $("#" + txtHeightID).val();
        var txtWeightID = "txtWeight";
        var weight = $("#" + txtWeightID).val();
        var txtLengthID = "txtLength";
        var length = $("#" + txtLengthID).val();
        var txtWidthID = "txtWidth";
        var width = $("#" + txtWidthID).val();
        var dropOffType = $("#ddlDropOffType").val();
        var packageType = $("#ddlPackageType").val();
        var vendorShippingID = 0;
        var scheduledDate = "";
        var scheduledTime = "";
        var scheduledDateTime = "";

        if (weight == "")
        {
            $("#" + divShipmentLoaderID).hide();
            $("#" + divShipmentRatesSectionID).fadeIn();
            window.alert("Weight is a Required Field.")
            return false;
        }

        if (parseInt(packageType) == 10)
        {
            if (height == "" || length == "" || width == "")
            {
                $("#" + divShipmentLoaderID).hide();
                $("#" + divShipmentRatesSectionID).fadeIn();
                window.alert("Height, Length, and Width are Required Fields.")
                return false;
            }
        }
        else {
            height = "";          
            length = "";
            width = "";
        }

        //all good...send request
        var listOfParts = GetSelectedParts();

        if (listOfParts == null || listOfParts == "")
        {
            $("#" + divShipmentLoaderID).hide();
            $("#" + divShipmentRatesSectionID).fadeIn();
            window.alert("Select at least one part for shipment.");
            return false;
        }


        if (parseInt(dropOffType) == 3) {
            vendorShippingID = $("#ddlVendors").val();
            scheduledDate = $("#txtScheduledDate").val();
            scheduledTime = $("#ddlScheduledTime").val();
            scheduledDateTime = scheduledDate + " " + scheduledTime;
            //window.alert(scheduledDate + " " + scheduledTime);
        }

        var shipmentModeID = parseInt($("#hfShipmentMode").val());


        var urlMethod = "";
        var json = "";

        if (shipmentModeID == 1)
        {
            vendorShippingID = $("#ddlVendors").val();
            urlMethod = "../OrderWebService.asmx/GetShippingRates";
            json = {'orderID': orderid, 'client': user.Client, 'listOfParts': listOfParts, 'height': height, 'weight': weight, 'width': width, 'length': length, 'dropOffTypeID': dropOffType, 'packageTypeID': packageType, 'vendorShippingID': vendorShippingID, 'scheduledDateTime': scheduledDateTime };      
        }
        else if(shipmentModeID == 2)
        {
            var scheduledDataCollection = GetReturnScheduledPickupData();
          //  window.alert(scheduledDataCollection);
            urlMethod = "../OrderWebService.asmx/GetReturnShippingRates";
            json = { 'orderID': orderid, 'client': user.Client, 'listOfParts': listOfParts, 'height': height, 'weight': weight, 'width': width, 'length': length, 'dropOffTypeID': dropOffType, 'packageTypeID': packageType, 'scheduledPickupDataCollection': scheduledDataCollection };
        }
        
       var jsonData = JSON.stringify(json);

        SendAjax(urlMethod, jsonData, DisplayShippingRates);
        return false;
    }
    catch(err)
    {
        window.alert(err);
        return false;
    }
  
}

function DisplayShippingRates(response)
{
    try {
    //check for an error response
        var errMsg = getErrorMsg(response);

            var divShipmentLoaderID = "divShipmentLoader";
            var divShipmentRatesSectionID = "divShipmentRatesSection";
       
    if (errMsg != null && errMsg != "")
    {
        $("#" + divShipmentLoaderID).hide();
        $("#" + divShipmentRatesSectionID).show();
        window.alert(errMsg);
        return;
    }

        //so far so good ... no errors so far
             errMsg = "A problem was encountered while trying to get the rates for this shipment. Check the shipment details and try again or contact your software administrator."
            if (response == null || response == "")
            {
                $("#" + divShipmentLoaderID).hide();
                $("#" + divShipmentRatesSectionID).show();
                window.alert(errMsg);
                return;
            }

            var rates = jQuery.parseJSON(response.d);

            //window.alert(rates);
            if (rates == null || rates == "") {
                $("#" + divShipmentLoaderID).hide();
                $("#" + divShipmentRatesSectionID).show();
                window.alert(errMsg);
                return;
            }

            $("#" + divShipmentLoaderID).hide();
            var divShipmentCreationScreenID = "divShipmentCreationScreen";
            $("#" + divShipmentCreationScreenID).fadeIn();

            var jsRatesTmpl = "<tr>";
            jsRatesTmpl = jsRatesTmpl + "<td class='tdShipmentRateSelection' ><input type='radio' name='rblRate' id='rblRate' value='${ServiceTypeID}' class='' /></td>";
            jsRatesTmpl = jsRatesTmpl + "<td class='tdShipmentType' style='padding-top:8px;' >${ServiceType}</td>";
            jsRatesTmpl = jsRatesTmpl + "<td class='tdShipmentTime' style='padding-top:8px;' >${TransitTime} &nbsp;&nbsp;${DeliveryTimestamp}</td>";
            jsRatesTmpl = jsRatesTmpl + "<td class='tdShipmentRate' style='padding-top:8px;' >${Rate}</td>";
            jsRatesTmpl = jsRatesTmpl + "</tr>";

            $.template("ratesTemplate", jsRatesTmpl);
            $("#trShipmentRates").empty();
            $.tmpl("ratesTemplate", rates).appendTo("#trShipmentRates");


            return false;
    }
    catch(err)
    {
        window.alert("Display Rates Error:" + err);
        return false;
    }  
}

function CreateShipment()
{
    try {
        var divShipmentLoaderID = "divShipmentLoader";
        var divShipmentCreationScreenID = "divShipmentCreationScreen";
      
        $("#" + divShipmentCreationScreenID).hide();
        $("#" + divShipmentLoaderID).fadeIn();

        var serviceTypeID = GetShippingTypeSelected();
   
        //window.alert(serviceTypeID);
        //return;

        if (serviceTypeID == null || serviceTypeID == "") {
            window.alert("A shipment type is required in order to create a shipment.")
            return false;
        }

        //the above have already been validated

        var txtHeightID = "txtHeight";
        var txtWidthID = "txtWidth";
        var txtLengthID = "txtLength";
        var txtWeightID = "txtWeight";

        var dropOffType = $("#ddlDropOffType").val();
        var packageType = $("#ddlPackageType").val();
        var height;
        var width;
        var length;
        var weight;
   
        switch (parseInt(packageType))
        {
            case 10:
                //custom
                break;
            case 9:
                $("#" + txtLengthID).val("38");
                $("#" + txtWidthID).val("6");
                $("#" + txtHeightID).val("6");
                 break;
            case 8:
                $("#" + txtLengthID).val("12");
                $("#" + txtWidthID).val("10");
                $("#" + txtHeightID).val("1");
                break;
            case 7:
                $("#" + txtLengthID).val("12");
                $("#" + txtWidthID).val("15");
                $("#" + txtHeightID).val("5");
                break;
            case 6:
                $("#" + txtLengthID).val("13");
                $("#" + txtWidthID).val("11");
                $("#" + txtHeightID).val("2");
                break;
            case 5:
                $("#" + txtLengthID).val("17");
                $("#" + txtWidthID).val("12");
                $("#" + txtHeightID).val("3");
                break;
            case 4:
                $("#" + txtLengthID).val("17");
                $("#" + txtWidthID).val("20");
                $("#" + txtHeightID).val("5");
                break;
            case 3:
                $("#" + txtLengthID).val("9");
                $("#" + txtWidthID).val("13");
                $("#" + txtHeightID).val("5");
                break;
            case 2:
                $("#" + txtLengthID).val("15");
                $("#" + txtWidthID).val("12");
                $("#" + txtHeightID).val("10");
                break;
            default:
                break;
        }

        height = $("#" + txtHeightID).val();
        width = $("#" + txtWidthID).val();
        length = $("#" + txtLengthID).val();
        weight = $("#" + txtWeightID).val();

        var vendorShippingID = 0;
        var scheduledDate = "";
        var scheduledTime = "";
        var scheduledDateTime = "";

        var listOfParts = GetSelectedParts();     

        if (parseInt(dropOffType) == 3) {
            vendorShippingID = $("#ddlVendors").val();
            scheduledDate = $("#txtScheduledDate").val();
            scheduledTime = $("#ddlScheduledTime").val();
            scheduledDateTime = scheduledDate + " " + scheduledTime;
            //window.alert(scheduledDate + " " + scheduledTime);
        }

        var shipmentModeID = parseInt($("#hfShipmentMode").val());
        var urlMethod = "";
        var json = "";
       
         
        if (shipmentModeID == 1)
        {
            urlMethod = "../OrderWebService.asmx/CreateShipment";
            json = {'orderID': orderid, 'client': user.Client, 'listOfParts': listOfParts, 'height': height, 'weight': weight, 'width': width, 'length': length, 'dropOffTypeID': dropOffType, 'packageTypeID': packageType, 'serviceTypeID': parseInt(serviceTypeID), 'vendorShippingID': parseInt(vendorShippingID), 'scheduledDateTime': scheduledDateTime };
        }
        else
        {
            var scheduledDataCollection = GetReturnScheduledPickupData();
            urlMethod = "../OrderWebService.asmx/CreateReturnShipment";
            json = { 'orderID': orderid, 'client': user.Client, 'listOfParts': listOfParts, 'height': height, 'weight': weight, 'width': width, 'length': length, 'dropOffTypeID': dropOffType, 'packageTypeID': packageType, 'serviceTypeID': parseInt(serviceTypeID), 'scheduledPickupDataCollection': scheduledDataCollection };
        }
         var jsonData = JSON.stringify(json);

        SendAjax(urlMethod, jsonData, ShipmentComplete);
        return false;
    }
    catch (err)
    {
        window.alert(err);
        return false;
    }
    
}

function ShipmentComplete(response)
{
    var divShipmentRatesSectionID = "divShipmentRatesSection";
    var divShipmentLoaderID = "divShipmentLoader";
    var divShipmentCreationScreenID = "divShipmentCreationScreen";
    var divShipmentResultScreenID = "divShipmentResultScreen";

    try
    {
        var errMsg = "";

        $("#" + divShipmentLoaderID).hide();

        if (response == null || response == "")
        {
            errMsg = "An error was encountered while trying to process this shipment request. Please contact your IT administrator immediately.";
            $("#" + divShipmentCreationScreenID).fadeIn();
            window.alert(errMsg);
            return false;
        }

        //So far so good... see if there is an error message in the response
        var errMsg = getErrorMsg(response);

        if (errMsg != null && errMsg != "")
        {
            $("#" + divShipmentCreationScreenID).fadeIn();
            window.alert(errMsg);
            return;
        }

       //Last test ..
        var fedExPackageDetails = jQuery.parseJSON(response.d);    
        if (fedExPackageDetails == null || fedExPackageDetails == "")
        {
            errMsg = "Missing Package Detail Information. Contact Your IT Administrator Immediately.";
            $("#" + divShipmentCreationScreenID).fadeIn();
            window.alert(errMsg);
            return false;
        }

        //The world is great ... show the results of the shipment  
        $("#" + divShipmentResultScreenID).fadeIn();
      
        var jsShipmentTmpl = "<tr>";
        jsShipmentTmpl = jsShipmentTmpl + "<td class='tdShipTracNum' >${TrackingNumber}</td>";
        jsShipmentTmpl = jsShipmentTmpl + "<td class='tdShippingDetailLong' >${ServiceType}</td>";
        jsShipmentTmpl = jsShipmentTmpl + "<td class='tdShippingDetail' >${BillingWeight}</td>";
        jsShipmentTmpl = jsShipmentTmpl + "<td class='tdShippingDetail'>${PartsList}</td>";
        jsShipmentTmpl = jsShipmentTmpl + "</tr>";
        jsShipmentTmpl = jsShipmentTmpl + "<tr><td><br /></td></tr>";
        jsShipmentTmpl = jsShipmentTmpl + "<tr><th colspan='2' valign='middle' align='center' style='font-weight:bolder;'>Transit Time</th><th  style='font-weight:bolder;' valign='middle' align='center' >Delivery Date</th><th  style='font-weight:bolder;' valign='middle' align='center' >Delivery Day</th></tr>";
        jsShipmentTmpl = jsShipmentTmpl + "<tr><td colspan='2' valign='middle' align='center' >${TransitTime}</td><td colspan='1' valign='middle' align='center' >${DeliveryDate}</td><td  colspan='1' valign='middle' align='center' >${DeliveryDay}</td></tr>"
        if (fedExPackageDetails[0].PickupConfirmationNumber != null && fedExPackageDetails[0].PickupConfirmationNumber != "")
        {
            jsShipmentTmpl = jsShipmentTmpl + "<tr><td colspan='4' valign='middle' align='center' style=''><span style='font-weight:bolder;'>Pickup Confirmation Number:</span> ${PickupConfirmationNumber} </td></tr>";
        }
        jsShipmentTmpl = jsShipmentTmpl + "<tr><td><br /><br /></td></tr>";
        jsShipmentTmpl = jsShipmentTmpl + "<tr><td colspan='4' valign='middle' align='center' style=''><a id='lnkFedExLabelPdf' name='lnkFedExLabelPdf' href='http://parts.ckautoparts.com/FedExLabels/${FileName}'>http://parts.ckautoparts.com/FedExLabels/${FileName}</a></td></tr>"
      //  jsShipmentTmpl = jsShipmentTmpl + "<tr><td colspan='4' valign='middle' align='center' style='height:40px;'><a id='lnkFedExLabelPdf' name='lnkFedExLabelPdf' href='http://localhost:47976/FedExLabels/${FileName}'>http://parts.ckautoparts.com/FedExLabels/${FileName}</a></td></tr>"

        $.template("shipmentDetailTemplate", jsShipmentTmpl);
        $("#trShippingDetail").empty();
        $.tmpl("shipmentDetailTemplate", fedExPackageDetails).appendTo("#trShippingDetail");
    
          var labelPath = "http://parts.ckautoparts.com/FedExLabels/" + fedExPackageDetails[0].FileName;
     // var labelPath = "http://localhost:47976/FedExLabels/" + fedExPackageDetails[0].FileName;
       window.open(labelPath, "Fed Ex Shipping Label", "resizable=yes,scrollbars=yes,toolbar=yes,titlebar=yes,fullscreen=yes,menubar=yes");       
        return false;
    }
    catch (err) {
        window.alert(err);
        return false;
    }
  
}

function GetShippingTypeSelected()
{//get values of checkboxes selected
    try {
    var serviceTypeID = "";

    serviceTypeID = $('input[name=rblRate]:checked').val();
    return serviceTypeID;
    }
    catch (err)
    { return "0";}

}


function isValidDimensionalEntry(value)
{
    var valids = "0123456789.";
    if (valids.indexOf(value) == -1) { return false; }

    return true; 
}

function GetReturnScheduledPickupData()
{
    var scheduledPickupData = "";
    var dropOffType = $("#ddlDropOffType").val();

    if (parseInt(dropOffType) != 3)
    {
        return "";    
    }

    //create pickupcollection
   //var companyName = $("#txtServicerName").val();
   //var person = $("#txtServicer").val();
   //var address = $("#txtServicerAddress").val();
   //var city = $("#txtServierCity").val();
   //var state = $("#txtServicerState").val();
   //var zip = $("#txtServicerZip").val();
   //var phone = $("#txtServicerPhone").val();
   var vendorShippingID = $("#ddlVendors").val();
   var scheduledDate = $("#txtScheduledDate").val();
   var scheduledTime = $("#ddlScheduledTime").val();
   var scheduledDateTime = scheduledDate + " " + scheduledTime;
   var shopClosingTime = $("#ddlShopClosingTime").val();
   var pickupBuildingType = $("#ddlPickupBuildingType").val();
   var buildingPartCode = $("#ddlBuildingPartCode").val();
   //var suite = $("#txtSuite").val();
   var specInstr = $("#txtSpecialInstructions").val();
   var referenceNumber = $("#txtRSReferenceNumber").val();
   var poNO = $("#txtRSPoNo").val();
   var invoiceNumber = $("#txtRSInvoiceNumber").val();
   var deptNo = $("#txtRSDeptNumber").val();

   scheduledPickupData = "VSID=" + vendorShippingID + "_SDT=" + scheduledDateTime + "_PBT=" + pickupBuildingType + "_BPC=" + buildingPartCode;
   scheduledPickupData = scheduledPickupData + "_SPINSTR=" + specInstr + "_SCT=" + shopClosingTime;
   scheduledPickupData = scheduledPickupData + "_REFNO=" + referenceNumber + "_PONO=" + poNO + "_INVNO=" + invoiceNumber + "_DEPTNO=" + deptNo;

   return scheduledPickupData;

}

function getErrorMsg(response)
{ 
    try
    {
        if (response == null || response == "") { return ""; }

        var rawErrMsg = jQuery.parseJSON(response.d);
        if (rawErrMsg.length == 0)
        { return ""; }

        if (rawErrMsg[0].indexOf("ERRORMESSAGE=") == -1)
        {//Error Message indicator is not present, return empty          
            return "";
        }

        //We have a message, grab it and return it
        var arrErrMsg = rawErrMsg[0].split("=");
        var errMsg = arrErrMsg[1];
        return errMsg;
    }
    catch (err) {return "";}
}



function DeleteShipmentByTrackingNumber() {
    var txtTrackingNumberID = "txtTrackingNumber";
    var trackingNumber = $("#" + txtTrackingNumberID).val();

    var urlMethod = "../OrderWebService.asmx/DeleteShipmentByTrackingNumber";
    var json = { 'trackingNumber': trackingNumber, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, DeleteShipmentReturn);
}

function DeleteShipmentReturn(msg) {
    var result = msg.d;
    var divShipmentDeletionMsgID = "divShipmentDeletionMsg";

    if (result == null || result == "") {
        $("#" + divShipmentDeletionMsgID).html("Shipment Successfully Deleted.");
        $("#" + divShipmentDeletionMsgID).css("color", "Green");
    }
    else {
        $("#" + divShipmentDeletionMsgID).html("An Error Was Encountered While Trying to Delete this Shipment. Please Try Again.");
        $("#" + divShipmentDeletionMsgID).css("color", "Red");
    }
   
    return false;
}

function GetListOfVendor(orderID)
{
    try
    {       
        var urlMethod = "../OrderWebService.asmx/GetsVendors";
        var json = {'orderID':parseInt(orderID), 'client': user.Client };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, LoadVendorsIntoDropdown);
        return false;
    }
    catch (err) {
        window.alert(err);
    }
}

function LoadVendorsIntoDropdown(msg)
{
    var vendors = jQuery.parseJSON(msg.d);
    $("select[name='ddlVendors'] option").remove();
    $("#ddlVendors").append($('#vendorDropDownTemplate').tmpl(vendors))

}

function SetServicerIntoShipmentWidget()
{
    try
    {        
        var partID = GetCurrentPartID();
       // window.alert(partID);
        var servicer = $("#" + partID + "_Servicer").val();
        //window.alert(servicer);
        $("#divServicer").html(servicer);
        return true;
    }
    catch (err) { return false; }

}
//SHIPPING METHODS ENDS



//PARTS TOTAL SUMMARY STARTS
function GetTotalPartsSummary(orderid) {
    var urlMethod = "../OrderWebService.asmx/GetOrderParts";
    var json = { 'orderid': orderid, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, SetTotalPartsSummary);
    return true;
}

function SetTotalPartsSummary(msg) {

    data = jQuery.parseJSON(msg.d);
    $('#tblPartsTotalSummary tr:not(:first)').remove();
    $("#tblPartsTotalSummary").append($('#partsTotalSummaryTemplate').tmpl(data))
    $('#parts_total_summary_loader').hide();

    console.log(data);
}


function MoveScreenToPartsContent()
{
    $(".active").removeClass('active');
    //$("#" + "li_" + partNo).addClass('active');
    //var p = $("#part_content").position()
    $("body").scrollTop($("#part_content").offset().top);
    //window.alert(p.left + " - " + p.top);
    return true;
}




//PARTS TOTAL SUMMARY ENDS

//KO TESTER


//function GetPartOrderByID(partID)
//{
//    try {
//        var urlMethod = "../OrderWebService.asmx/GetPartOrderByID";
//        var json = { 'partID': partID, 'client': user.Client };
//        var jsonData = JSON.stringify(json);
//        var part = "NEW";

//        SendAjaxAsyncFalse(urlMethod, jsonData, function (msg) {
//            var parts = jQuery.parseJSON(msg.d);
//            part = parts[0];
//        });

//        return part;
//    }
//    catch (err) {
//        window.alert(err);
//    }
//}

//function BindKOTester(msg)
//{
//    try {
//        var parts = jQuery.parseJSON(msg.d);
//        //       window.alert(parts[0].Vendor);
//        window.alert(parts[0].VendorID);

//        //var vendor = parts[0].VendorID;
//        //var type = parts[0].PartType;
//        //var group = parts[0].PartDescGroup;
//        //var description = parts[0].PartDescription;
//        //var brand = parts[0].Brand;
//        //window.alert(ViewModel.vendor);
//    }
//    catch (err)
//    { window.alert(err); }
//}

//function GetKOListOfParts(orderID) {
//    try {
//        var urlMethod = "../OrderWebService.asmx/GetOrderParts";
//        var json = { 'orderid': orderid, 'client': user.Client };
//        var jsonData = JSON.stringify(json);
//        SendAjax(urlMethod, jsonData, LoadPartsIntoKODropdown);
//        return false;
//    }
//    catch (err) {
//        window.alert(err);
//    }
//}


//function LoadPartsIntoKODropdown(msg) {
//    var parts = jQuery.parseJSON(msg.d);
//    $("select[name='ddlKOParts'] option").remove();
//    $("#ddlKOParts").append($('#koDropDownPartsTemplate').tmpl(parts))

//    var vendor = parts[0].VendorID;
//    var type = parts[0].PartType;
//    var group = parts[0].PartDescGroup;
//    var description = parts[0].PartDescription;
//    var brand = parts[0].Brand;
//    //window.alert(vendor + "-" + type + "-" + group + "-" + description + "-" + brand);
//    //return;
//    ko.applyBindings(new ViewModel(vendor, type, group, description, brand));
//}
////SHIPPING METHODS ENDS


//var ViewModel = function (v, t, g, d, b)
//{
//    this.vendor = ko.observable(v);
//    this.type = ko.observable(t);
//    this.group = ko.observable(g);
//    this.description = ko.observable(d);
//    this.brand = ko.observable(b);

//    this.registerChange = function ()
//    {       
//        var dropdownValue = $("#ddlKOParts").val();
//        var part = GetPartOrderByID(dropdownValue);
//        this.vendor(part.Vendor);
//        this.type(part.Vendor);
//        this.group(part.Vendor);
//        this.description(part.Vendor);
//        this.brand(part.Vendor);
//    };

//}
