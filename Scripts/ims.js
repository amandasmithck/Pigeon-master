if(!Array.prototype.filter) {
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

function formObjectify( selector )
{
		var form = {};
		$(selector).find(':input[name]:not([type=submit])').each( function() {
		    var self = $(this);
		    var name = self.attr('name');
		    if (form[name]) {
		    form[name] = form[name] + ',' + self.val();
		    }
		    else {
		    form[name] = self.val();
		    }
		});

		return form;
}


function AddInService() {

    var urlMethod = "../IMSWebService.asmx/AddInService";

    var data = { 'sn': $('#service-sn').val()
            , 'po': $('#service-po').val()
            , 'vin': $('#service-vin').val()
            , 'mileage': $('#service-mileage').val()
            , 'solddate': $('#service-solddate').val()
            , 'client': user.Client
    };
    var jsonData = JSON.stringify(data);
    SendAjax(urlMethod, jsonData, ReturnAddInService);


}

function ReturnAddInService(msg) {
    

    if (msg.d == true) {
        $(".dialog-message .message").html("In-service information received and will be processed.");
        $(".dialog-message").dialog("open");
    }
    else {

        $(".dialog-message .message").html("ERROR processing info. Please try again");
        $(".dialog-message").dialog("open");
    }
}

var formdata;
function InitiateWarranty() {

    formdata = formObjectify($('#initiatewarranty-form'))
    var urlMethod = "../IMSWebService.asmx/InitiateWarranty";

    var data = formdata;
    data.client = user.Client;
    var jsonData = JSON.stringify(data);
    SendAjax(urlMethod, jsonData, ReturnInitiateWarranty);


}


function ReturnInitiateWarranty(msg) {

    if (msg.d == true) {
        $(".dialog-message .message").html("Warranty information received and will be processed.");
        $(".dialog-message").dialog("open");
    }
    else {

        $(".dialog-message .message").html("ERROR processing info. Please try again");
        $(".dialog-message").dialog("open");
    }
}


$('document').ready(function () {
    $(".dialog-message").dialog({
        autoOpen: false,
        modal: true,
        btns: {
            Ok: function () {
                location.reload(true);
            }
        }
    });

    $(".unsalable-message").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            Ok: function () {
                MarkUnsalable();
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $('#inservice-submit').click(function () {
        AddInService();
        return false;
    });

    $('#initiatewarranty-form').submit(function () {
        InitiateWarranty();
        return false;
    });

    setTimeout(function () { renderReturnStatus(); }, 1000);

    $.collapsible(".subscreen .subscreen-header");

    if (window.location.hash) {
        var hash = window.location.hash;
        var id = hash.replace('#', '');
        $('.screens > .screen').hide();
        switch (id) {
            case "IMS":
                showScreen(0);
                break;
            case "inservice":
                showScreen(1);
                break;
            case "printwarranty":
                showScreen(2);
                break;
            case "initiatewarranty":
                showScreen(3);
                break;
            default:
                showScreen(0);
                break;
        }
    } else {
        $('.screens > .screen:not(:first)').hide();
    }

    $('.toolbar .btn').click(function () {
        var index = $('.toolbar .btn').index(this);
        showScreen(index);
    });

    function showScreen(index) {
        $('.toolbar .btn').removeClass('btn-primary');
        $('.toolbar .btn:nth(' + index + ')').addClass('btn-primary');

        $('.screens > .screen').hide();
        $('.screens > .screen:nth(' + (index) + ')').show();

        setTimeout(function () { window.location.hash = $('.toolbar .btn:nth(' + index + ')').attr('title') }, 50);
    }

    function showSubScreen(group, index) {
        showScreen(group);

        $('.screens > .screen:nth(' + (group) + ') .subscreen').hide();
        $('.screens > .screen:nth(' + (group) + ') .subscreen:nth(' + (index) + ')').show();
    }

    $('.arrivalsbtn').click(function () {
        switch ($(this).attr("id")) {
            case "arrivalselect":
                $('#arrivals-grid input[type=checkbox]').prop('checked', true)
                break;
            case "arrivaldeselect":
                $('#arrivals-grid input[type=checkbox]').prop('checked', false)
                break;
        }

    });

    GetStock();

    /** EXPECTED ARRIVALS GRID LOGIC **/
    $('#arrivals-grid select').live('change', function () {
        $(this).parents('tr').find('input[type=checkbox]').attr('checked', 'checked');
    });

    $('#place-inventory-btn').click(function () {
        $('#arrivals-grid input[type=checkbox]:checked').each(function () {
            var row = $(this).parents('tr');
            var sn = row.find('.sn').html();
            var val = row.find('select').val();
            var index = $('#arrivals-grid input[type=checkbox]').index($(this));

            $("#arrivals-grid").igGrid('deleteRow', index);

            var urlMethod = "../IMSWebService.asmx/UpdatePart";
            var data = { 'SN': sn
            , 'Source': 'Arrivals'
            , 'Dest': 'Inventory'
            , 'Val': val
            , 'client': user.Client
            };
            var jsonData = JSON.stringify(data);
            SendAjax(urlMethod, jsonData,
                function (msg) {
                    var response = jQuery.parseJSON(msg.d);
                    //console.log(response);
                    if (response[0].Success) {

                        $("#inventory-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN, 'Location': response[0].Location });
                        //$("#inventory-grid").find('.btn').btn();
                    } else {
                        $("#arrivals-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN });
                    }
                }
            );
        });
    });

    $('#mark-damaged-btn').click(function () {
        $('#arrivals-grid input[type=checkbox]:checked').each(function () {
            var row = $(this).parents('tr');
            var sn = row.find('.sn').html();
            var val = 'Damage';
            var index = $('#arrivals-grid input[type=checkbox]').index($(this));

            $("#arrivals-grid").igGrid('deleteRow', index);

            var urlMethod = "../IMSWebService.asmx/UpdatePart";
            var data = { 'SN': sn
            , 'Source': 'Arrivals'
            , 'Dest': 'Vendor'
            , 'Val': val
            , 'client': user.Client
            };
            var jsonData = JSON.stringify(data);
            SendAjax(urlMethod, jsonData,
                function (msg) {
                    var response = jQuery.parseJSON(msg.d);
                    //console.log(response);
                    if (response[0].Success) {
                        $("#vendor-return-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN, 'Location': response[0].Location, 'ReturnType': response[0].ReturnType, 'DateReceived': response[0].DateReceived });
                        // $("#vendor-return-grid").find('.btn').btn();
                        renderReturnStatus();
                    } else {
                        $("#arrivals-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN });
                    }
                }
            );
        });
    });

    $("#inventory-grid .inv-remove-btn").live('click', function () {
        var row = $(this).parents('tr');
        var sn = row.find('.sn').html();
        var val = row.find('select option:selected').val();

        var index = $('#inventory-grid .inv-remove-btn').index($(this));

        var urlMethod = "../IMSWebService.asmx/UpdatePart";
        var data = { 'SN': sn
            , 'Source': 'Inventory'
            , 'Dest': 'Field'
            , 'Val': val
            , 'client': user.Client
        };
        var jsonData = JSON.stringify(data);
        SendAjax(urlMethod, jsonData,
                function (msg) {
                    var response = jQuery.parseJSON(msg.d);
                    //console.log(response);
                    if (response[0].Success) {
                        $("#inventory-grid").igGrid('deleteRow', index);
                        $("#shop-return-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN, 'ReturnType': response[0].ReturnType });
                        // $("#shop-return-grid").find('.btn').btn();
                    }
                }
            );
    });

    $("#inventory-grid .inv-unsalable-btn").live('click', function () {
       var row = $(this).parents('tr');
       var sn = row.find('.sn').html();
       $('#unsalable-sn').html(sn);
       $('#unsalable-index').html($('#inventory-grid .inv-unsalable-btn').index($(this)));
       $(".unsalable-message").dialog("open");
        
    });

    function MarkUnsalable() {
        var val = 'Unsalable';
        var urlMethod = "../IMSWebService.asmx/UpdatePart";
        var data = {
            'SN': $('#unsalable-sn').html()
            , 'Source': 'Inventory'
            , 'Dest': 'Vendor'
            , 'Val': val
            , 'client': user.Client
        };
        var jsonData = JSON.stringify(data);
        SendAjax(urlMethod, jsonData,
        function (msg) {
            var response = jQuery.parseJSON(msg.d);
            //console.log(response);
            if (response[0].Success) {
                $("#inventory-grid").igGrid('deleteRow', $('#unsalable-index').html());
                $("#vendor-return-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN, 'ReturnType': response[0].ReturnType, 'DateReceived': response[0].DateReceived });
                renderReturnStatus();
            }
        }
        );
        $(".unsalable-message").dialog("close");
    }
    $('#shop-return-grid .cancel-btn').live('click', function () {
        var row = $(this).parents('tr');
        var sn = row.find('.sn').html();
        var val = "Cancel";

        var index = $('#shop-return-grid .cancel-btn').index($(this));

        var urlMethod = "../IMSWebService.asmx/UpdatePart";
        var data = { 'SN': sn
            , 'Source': 'Field'
            , 'Dest': 'Inventory'
            , 'Val': val
            , 'client': user.Client
        };
        var jsonData = JSON.stringify(data);
        SendAjax(urlMethod, jsonData,
                function (msg) {
                    var response = jQuery.parseJSON(msg.d);
                    //console.log(response);
                    if (response[0].Success) {
                        $("#shop-return-grid").igGrid('deleteRow', index);
                        $("#inventory-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN, 'Location': response[0].Location });
                        // $("#inventory-grid").find('.btn').btn();

                    }
                }
            );
    });

    $("#shop-return-grid input[type=checkbox]").live('change', function () {
        var row = $(this).parents('tr');
        var sn = row.find('.sn').html();
        var val = "1";

        var index = $('#shop-return-grid input[type=checkbox]').index($(this));

        var urlMethod = "../IMSWebService.asmx/UpdatePart";
        var data = { 'SN': sn
            , 'Source': 'Field'
            , 'Dest': 'Vendor'
            , 'Val': val
            , 'client': user.Client
        };
        var jsonData = JSON.stringify(data);
        SendAjax(urlMethod, jsonData,
                function (msg) {
                    var response = jQuery.parseJSON(msg.d);
                    //console.log(response);
                    if (response[0].Success) {
                        $("#shop-return-grid").igGrid('deleteRow', index);
                        $("#vendor-return-grid").igGrid('addRow', { 'Part': response[0].Part, 'SN': response[0].SN, 'ReturnType': response[0].ReturnType, 'DateReceived': response[0].DateReceived });
                        //  $("#vendor-return-grid").find('.btn').btn();
                    }
                    renderReturnStatus();
                }
            );
    });
}); 
//end document.ready


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

var stockdata;
var arrivaldata;
var inventorydata;
var fieldreturndata;
var vendorreturndata;
var data = new Array();
var bins;
var binsoption;
var binsoption2="<option value=''></option>";
function ReturnGetStock(msg) {

    stockdata = jQuery.parseJSON(msg.d);
    
    arrivaldata = stockdata[0].filter(function (el) {
        return el.Arrive == null;
    });

    bins = stockdata[1];
    binsoption = "<td><select ><option value=''></option>";
    $(bins).each(function () {

        binsoption += "<option value='" + $(this).attr('Bin') + "'>" + $(this).attr('Bin') + "</option>"
        binsoption2 += "<option value='" + $(this).attr('Bin') + "'>" + $(this).attr('Bin') + "</option>"

    });
    binsoption += "</select></td>"
    //console.log(binsoption2);
    $('#new-bin').append(binsoption2);

    $("#arrivals-grid").igGrid({
        virtualization: false,
        rowTemplate: "<tr>" +
                     "<td><input type='checkbox'/></td>" +
                     "<td>${Part}</td>" +
                     "<td class='sn'>${SN}</td>" +
                     "<td>${ETA}</td>" +
                     binsoption +
                     "</tr>",
        autoGenerateColumns: false,
        columns: [
            { headerText: "", key: "VIN", dataType: "string", width: 75  },
            { headerText: "Part #", key: "Part", dataType: "string" },
            { headerText: "Serial #", key: "SN", dataType: "string" },
            { headerText: "ETA", key: "ETA", dataType: "string" },
            { headerText: "Bin", key: "Location", dataType: "string" }
        ],
        features: [
         {
             name: "Sorting",
             type: "local",
             caseSensitive: true
         },
         
                     {
                         name: 'Paging',
                         type: "local",
                         pageSize: 10
                     },
                {
                    name: 'Filtering',
                    type: 'local',
                    columnSettings: [
                        {columnKey: "VIN", allowFiltering: false },
                        {columnKey: "Part", allowFiltering: true, condition: "contains" },
                        {columnKey: "SN", allowFiltering: true, condition: "contains"},
                        {columnKey: "ETA", allowFiltering: false},
                        {columnKey: "Location", allowFiltering: false}
                    ]
                }
            ],
        width: "100%",
        dataSource: arrivaldata,
        headerCaption: "Expected Arrivals",
        jQueryTemplating: true
    });

    inventorydata = stockdata[0].filter(function (el) {
        return (el.Arrive != null && el.ReturnType == null);
    });
    $("#inventory-grid").igGrid({
        autoGenerateColumns: false,
        rowTemplate: "<tr>" +
                     "<td>${Part}</td>" +
                     "<td class='sn'>${SN}</td>" +
                     "<td><div class='grid-btn btn change-loc-btn'>${Location}</div></td>" +
                     "<td><select class='${PO}'><option value=''></option><option value='core'>New Sale</option><option value='defect'>Warranty</option></select></td>" +
                     "<td><div class='${PO} grid-btn btn inv-remove-btn'>Remove From Inventory</div></td>" +
                     "<td><div class='${PO} grid-btn btn inv-unsalable-btn'>Unsalable</div></td>" +
                     "</tr>",
        columns: [
            { headerText: "Part #", key: "Part", dataType: "string" },
            { headerText: "Serial #", key: "SN", dataType: "string" },
            { headerText: "Bin", key: "Location", dataType: "string" },
            { headerText: "Type", key: "ReturnType", dataType: "string" },
            { headerText: "", key: "VIN", dataType: "string" },
            { headerText: "", key: "ETA", dataType: "string" }
        ],
        features: [
         {
             name: "Sorting",
             type: "local",
             caseSensitive: true
         },
                     {
                         name: 'Paging',
                         type: "local",
                         pageSize: 10
                     },

                {
                    name: 'Filtering',
                    type: 'local',
                    columnSettings: [
                        {columnKey: "Part", allowFiltering: true, condition: "contains" },
                        {columnKey: "SN", allowFiltering: false},
                        {columnKey: "Location", allowFiltering: false},
                        {columnKey: "ReturnType", allowFiltering: false},
                        { columnKey: "VIN", allowFiltering: false },
                        { columnKey: "ETA", allowFiltering: false }
                    ]
                }
            ],
        width: "100%",
        dataSource: inventorydata,
        jQueryTemplating: true
        
        //headerCaption: "Inventory"
    });

    $("#change-bin-diag").dialog({
        autoOpen: false,
        resizable: false,
        height: 250,
        width: 200,
        modal: true,
        buttons: {
            "Update": function () {
                var urlMethod = "../IMSWebService.asmx/UpdatePartBin";
                var data = { 'SN': $('#bin-change-sn').html(),
                    'NewBin': $('#new-bin option:selected').val(),
                    'client': user.Client
                };
                var jsonData = JSON.stringify(data);
                SendAjax(urlMethod, jsonData, function (msg) {
                    $("#change-bin-diag").dialog("close");
                    location.reload(true);
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $("#edit-info-diag").dialog({
        autoOpen: false,
        resizable: false,
        height: 250,
        width: 400,
        modal: true,
        buttons: {
            "Update": function () {
                var urlMethod = "../IMSWebService.asmx/EditInfo";
                var data = { 'sn': $('#edit-sn').html(),
                    'vin': $('#edit-vin').val(),
                    'mileage': $('#edit-mileage').val(),
                    'solddate': $('#edit-sold').val(),
                    'client': user.Client

                };
                var jsonData = JSON.stringify(data);
                SendAjax(urlMethod, jsonData, function (msg) {
                    $("#edit-info-diag").dialog("close");
                    location.reload(true);
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $('.change-loc-btn').live('click', function () {
        var current = $(this).html();
        var row = $(this).parents('tr');
        var sn = row.find('.sn').html();
        $('#bin-change-sn').html(sn);
        $('#new-bin option:nth(0)').attr('selected', true);
        $("#change-bin-diag #current-bin").html("Current Bin: " + current);
        $("#change-bin-diag").dialog("open");
    });

    $('.edit-btn').live('click', function () {
        var current = $(this).html();
        var row = $(this).parents('tr');
        var sn = row.find('.sn').html();
        var vin = row.find('.vin').html();
        var mileage = row.find('.mileage').html();
        var sold = row.find('.sold').html();
        $("#edit-sn").html(sn);
        $("#edit-vin").val(vin);
        $("#edit-mileage").val(mileage);
        $("#edit-sold").val(sold);
        $("#edit-info-diag").dialog("open");
    });

    //$("#inventory-grid").live('iggridfilteringdatafiltered', function (event, args) {$('.btn').btn();});

    fieldreturndata = stockdata[0].filter(function (el) {
        return ((el.Arrive != null && el.ReturnType != null && !el.Received) && el.ReturnType != "unsalable");
    });
    $("#shop-return-grid").igGrid({
        autoGenerateColumns: false,
        rowTemplate: "<tr>" +
                     "<td><div class='${PO} btn grid-btn cancel-btn'>Cancel Sale</div></td>" +
                     "<td>${Part}</td>" +
                     "<td class='sn'>${SN}</td>" +
                     "<td>${Location}</td>" +
                     "<td>${ReturnType}</td>" +
                     "<td><input id='${Received}' type='checkbox' /></td>" +
                     "</tr>",
        columns: [
            { headerText: "", key: "PO", dataType: "string", width: "100px" },
            { headerText: "Part #", key: "Part", dataType: "string" },
            { headerText: "Serial #", key: "SN", dataType: "string" },
            { headerText: "Original Location", key: "Location", dataType: "string" },
            { headerText: "Return Type", key: "ReturnType", dataType: "string" },
            { headerText: "Received", key: "Received", dataType: "string", width: "100px" }
        ],
        features: [
         {
             name: "Sorting",
             type: "local",
             caseSensitive: true
         },
                     {
                         name: 'Paging',
                         type: "local",
                         pageSize: 10
                     },
            {
                name: 'Filtering',
                type: 'local',
                columnSettings: [
                    {columnKey: "PO", allowFiltering: false },
                    {columnKey: "Part", allowFiltering: false},
                    { columnKey: "SN", allowFiltering: true, condition: "contains" },
                    {columnKey: "Location", allowFiltering: false},
                    { columnKey: "ReturnType", allowFiltering: false },
                    { columnKey: "Rceived", allowFiltering: false }
                    
                ]
            }
        ],
        width: "100%",
        dataSource: fieldreturndata,
        headerCaption: "Shop Returns",
        jQueryTemplating: true
    })

    vendorreturndata = stockdata[0].filter(function (el) {
        return ((el.Arrive != null && el.ReturnType != null && el.Received == true && el.Process == false) || el.ReturnType == "unsalable" && el.Process == false);
    });
    $("#vendor-return-grid").igGrid({
        autoGenerateColumns: false,
        rowTemplate: "<tr>" +
                     "<td><div class='${PO} grid-btn btn edit-btn'>Edit Info</div></td>" +
                     "<td>${Part}</td>" +
                     "<td class='sn'>${SN}</td>" +
                     "<td>${Location}</td>" +
                     "<td class='vin'>${VIN}</td>" +
                     "<td class='mileage'>${Mileage}</td>" +
                     "<td class='sold'>${SoldDate}</td>" +
                     "<td>${DateReceived}</td>" +
                     "<td>${ReturnType}</td>" +
                     "</tr>",
        columns: [
            { headerText: "", key: "VIN", dataType: "string" },
            { headerText: "Part #", key: "Part", dataType: "string" },
            { headerText: "Serial #", key: "SN", dataType: "string" },
            { headerText: "Original Location", key: "Location", dataType: "string" },
            { headerText: "VIN", key: "VIN", dataType: "string" },
            { headerText: "Mileage", key: "Mileage", dataType: "string" },
            { headerText: "Sold Date", key: "SoldDate", dataType: "string" },
            { headerText: "Date Recv", key: "DateReceived", dataType: "string" },
            { headerText: "Return Type", key: "ReturnType", dataType: "string", width: "75px" }
        ],

        width: "100%",
        dataSource: vendorreturndata,
        headerCaption: "Vendor Retuns",
        jQueryTemplating: true,
        features: [
//            {
//                name: 'Filtering',
//                type: 'local'
//            }
        ]
    });



    //adding and delete code samples...
    //$("#inventory-grid").igGrid('addRow', {'Part':'9999', 'Serial':'9999', 'PO': 'PO123', 'Location': 'Omaha'}, true);
    //$("#inventory-grid").igGrid('deleteRow', 1);
}

function renderReturnStatus() {
    var count= $("#vendor-return-grid tr").length - 1;
    var remainder = 20 - count;

    $('#return-status #count').html(count);
    (remainder > 0) ? $('#return-status #remainder').html(remainder) : $('#return-status #remainder').html("0");
}

//fake row moving...
/*$("#inventory-grid .btn").live('click', function () {
    var index = $("#inventory-grid .btn").index(this);
    $("#inventory-grid").igGrid('deleteRow', index);

    $("#shop-return-grid").igGrid('addRow', { 'Part': '8888', 'Serial': '8888', 'PO': 'PO123', 'Location': 'Omaha' });
    //$("#inventory-grid tr:nth(" + index + ")")
});*/

