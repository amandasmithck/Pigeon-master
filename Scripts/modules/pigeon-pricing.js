var PigeonPricing = (function (partno,parttype) {
    var pub = {},
    rendered = false;

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

    pub.render = function () {
        var CustomerNumber;
        //get list of pigeon clients
        var urlMethod = "../PigeonWebService.asmx/GetPigeonClients";
        var json = {};
        var jsonData = JSON.stringify(json);
        __sendAjax(urlMethod, jsonData, function (msg) {
            responseb = jQuery.parseJSON(msg.d);
            $(responseb).each(function () {
                $(this)[0].partno = partno;

            });
            $('.pigeon-pricing[rel="' + partno + '"]').append($("#pigeon-pricingTemplate").tmpl(responseb));
            $('.client-collapse').on('show', function () {
                $(this).loadmask("  Wait a sec, doing big things here....");
                    //get tiered data
                    var accordiondiv = $(this);
                    var urlMethod = "";
                    switch(parttype)
                    {
                        case 1:
                            urlMethod="../HomeWebService.asmx/SearchTransmissionByPartNo";
                            break;
                        case 2:
                            urlMethod="../HomeWebService.asmx/SearchEngineByPartNo";
                            break;
                        case 4:
                            urlMethod="../HomeWebService.asmx/SearchTransferByPartNo";
                            break;
                        case 10:
                            urlMethod = "../HomeWebService.asmx/SearchManualTransmissionByPartNo";
                            break;
                        case 3:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            urlMethod="../HomeWebService.asmx/SearchDiffByPartNo";
                            break;
                    }
                    CustomerNumber = $(this).attr('relCKCustNo');
                    var json = { 'partno': $(this).attr('relPart'), 'client': $(this).attr('relClient'), 'name': $(this).attr('relUser') };
                    var jsonData = JSON.stringify(json);
                    __sendAjax(urlMethod, jsonData, function (msg) {
                        responsec = jQuery.parseJSON(msg.d);
                        accordiondiv.find('.accordion-inner').html('');
                        accordiondiv.find('.accordion-inner').append($("#pigeon-pricing-resultTemplate").tmpl(responsec))
                        accordiondiv.find('.table:nth-child(2)').remove();
                        accordiondiv.unloadmask();
                        $('.table-pricing tbody tr:last').addClass('info'); //.css('background-color', '#FFF858');    
                        var SalePriceForItem=$('.table-pricing tbody tr td:last').text().replace('$', '');
                        if (SalePriceForItem.indexOf(',') > 0) {
                            SalePriceForItem = parseInt(SalePriceForItem.replace(',', ''));
                        }
                        $('.table-pricing tbody tr:last').attr('onclick', 'CheckOut(' + $(".pigeon-pricing").attr("rel") + ',' + SalePriceForItem + ',"' + CustomerNumber + '");'); //.css('background-color', '#FFF858');   
                        $('.table-pricing tbody tr:last').attr('style', 'cursor: pointer;');
                        //$('.table-pricing tbody tr td:last').attr('id', 'PassingSalePrice');

                    });
                    
                });
 
         });
    }

    return pub;
});