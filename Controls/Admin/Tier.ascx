<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Tier.ascx.vb" Inherits="Pigeon.Tier" %>

    <link rel="stylesheet" type="text/css" href="../Styles/land.css">
    <link href="../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/datatable/table.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ig/jquery.ui.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tier.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../../Scripts/accounting.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.jqDock.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.16.all.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.jeditable.min.js" type="text/javascript"></script>

   <script id="TiersTemplate" type="text/x-jquery-tmpl">
        <tr>
            <td><a href="#" class="BtnTierDelete" DeleteID="${TierID}"><img src="../../images/Remove_Icon.png" /></a></td>
            <td class="editable" rowid="${TierID}" column="Tier">${Tier}</td>
            <td class="TierSelEditable" rowid="${TierID}" column="BaseTiers">${BaseTiers}</td>
            <td class="editable" rowid="${TierID}" column="CustomerLabel">${CustomerLabel}</td>
            <td class="editable" rowid="${TierID}" column="AdminLabel">${AdminLabel}</td>
        </tr> 
   </script>

    <script id="TiersVisibilityTemplate" type="text/x-jquery-tmpl">
        <tr >
            <td rowid="${ID}" column="TierID">${TierID}</td>
            <td class="TierSelEditable" rowid="${ID}" column="AdditionalTier">${AdditionalTier}</td>
        </tr> 
    </script>

    <script id="WarrantyTemplate" type="text/x-jquery-tmpl">
        <tr>
            <td rowid="${ID}" column="PartType">${PartType}</td>
            <td rowid="${ID}" column="Tier">${Tier}</td>
            <td class="editable" rowid="${ID}" column="Warranty">${Warranty}</td>
            <td class="editable" rowid="${ID}" column="Base">${Base}</td>
            <td class="editable" rowid="${ID}" column="Percentage">${Percentage}</td>
            <td class="editable" rowid="${ID}" column="Flat">${Flat}</td>
            <td class="editable" rowid="${ID}" column="Href">${Href}</td>
        </tr> 
    </script>


    <script id="TierPriceTemplate" type="text/x-jquery-tmpl">
        <tr>
            <td rowid="${ID}" column="TierID">${TierID}</td>
            <td rowid="${ID}" column="PartType">${PartType}</td>
            <td class="editable" rowid="${ID}" column="Percentage">${Percentage}</td>
            <td class="editable" rowid="${ID}" column="Flat">${Flat}</td>
        </tr> 
    </script>


    <script id="NewTierVisibilityTempalate" type="text/x-jquery-tmpl">
        <tr >
            <td><a href="#" class="BtnNewTierVisibilityDelete" DeleteID="${ID}"><img src="../../images/Remove_Icon.png" /></a></td>
            <td rowid="${ID}" column="TierID">${TierID}</td>
            <td class="TierSelEditable" rowid="${ID}" column="AdditionalTier">${AdditionalTier}</td>
        </tr> 
    </script>

    <script id="NewTierWarrantyTempalate" type="text/x-jquery-tmpl">
        <tr>
            <td rowid="${ID}" column="PartType">${PartType}</td>
            <td rowid="${ID}" column="Tier">${Tier}</td>
            <td class="editable" rowid="${ID}" column="Warranty">${Warranty}</td>
            <td class="editable" rowid="${ID}" column="Base">${Base}</td>
            <td class="editable" rowid="${ID}" column="Percentage">${Percentage}</td>
            <td class="editable" rowid="${ID}" column="Flat">${Flat}</td>
            <td class="editable" rowid="${ID}" column="Href">${Href}</td>
        </tr> 
    </script>


    <script id="NewTierBasePriceTempalate" type="text/x-jquery-tmpl">
        <tr>
            <td rowid="${ID}" column="TierID">${TierID}</td>
            <td rowid="${ID}" column="PartType">${PartType}</td>
            <td class="editable" rowid="${ID}" column="Percentage">${Percentage}</td>
            <td class="editable" rowid="${ID}" column="Flat">${Flat}</td>
        </tr> 
    </script>

    <script id="NewTierSelect" type="text/x-jquery-tmpl">
    <option value="${TierID}">${Tier}</option> 
    </script>

    <script type="text/javascript">

            var TierData = {}
            var TiersVisibilityData = {}
            var WarrantyOptionsData = {}
            var TierSelects
            var NewTierID
            var DeleteTierID
            var DeleteTierVisibilityID

            $(document).ready(function () {


                GetTierSelects()

                $("#tabs").tabs({
                    "show": function (event, ui) {
                        var oTable = $('div.dataTables_scrollBody>table.display', ui.panel).dataTable();
                        if (oTable.length > 0) {
                            oTable.fnAdjustColumnSizing();
                        }
                    }
                });

                $("tr").live("click", function () {
                    if ($(this).hasClass('row_selected')) {
                        $(this).removeClass('row_selected');
                    }
                    else {
                        $('tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                    }
                });

                GetTiers()
                GetTiersVisibilty()
                GetWarrantyOptions()
                GetTierPricing()

                $("#NewTier").dialog({
                    title: "New Tier",
                    autoOpen: false,
                    buttons: {
                        "Continue To Visibility": function () {
                            AddTier()
                        }
                    }
                });

                $("#NewTier-2").dialog({
                    title: "New Tier",
                    autoOpen: false,
                    width: 700,
                    height: 475,
                    buttons: {
                        "Continue to Warranty": function () {
                            GetNewTiersWarranty()
                        }
                    }
                });

                $("#NewTier-3").dialog({
                    title: "New Tier",
                    autoOpen: false,
                    width: 700,
                    height: 475,
                    buttons: {
                        "Continue To Tier Pricing": function () {
                            GetNewTiersPricing()
                        }
                    }
                });

                $("#NewTier-4").dialog({
                    title: "New Tier",
                    autoOpen: false,
                    width: 700,
                    height: 475,
                    buttons: {
                        "Done": function () {
                            window.location.reload()

                        }
                    }
                });

                $("#DeleteTierPop").dialog({
                    title: "Delete Tier",
                    autoOpen: false,
                    buttons:
                    {
                        "Yes": function () {
                            DeleteTier(DeleteTierID)

                        },


                        "No": function () {
                            $("#DeleteTierPop").dialog("close");
                        }
                    }
                });


                $("#DeleteTierVisibilityPop").dialog({
                    title: "Delete Tier Visibility",
                    autoOpen: false,
                    buttons:
                    {
                        "Yes": function () {
                            DeleteTierVisibility(DeleteTierID)

                        },


                        "No": function () {
                            $("#DeleteTierVisibilityPop").dialog("close");
                        }
                    }
                });

                $("#btnTier").click(function () {
                    $("#NewTier").dialog("open");
                    if ($("#txtBaseTier option").length < 2) {
                        GetNewTierSelects()
                    }
                });

               


            });

        function GetTiers() {

            var urlMethod = "../TierWebService.asmx/GetTiers";

            var json = {'client': $('.current_client').text() };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetTiers);

        }

        function ReturnGetTiers(msg) {
            TierData = jQuery.parseJSON(msg.d)
            console.log(TierData);
            $("#TiersTemplate").tmpl(TierData).appendTo("#TiersTable tbody");

                    var TiersTable = $('#TiersTable').dataTable({
                        "bJQueryUI": true,
                        "bProcessing": true,
                        "bDestroy": true,
                        "sScrollY": "250",
                        "sScrollX": "100%",
                        "sScrollXInner": "110%",
                        "aoColumnDefs": [ 
                                    { "sWidth": "20px", "aTargets": [ 0 ] }
                                ],
                        "fnDrawCallback": function () {
                            $('#TiersTable tbody td.editable').editable(function (value, settings) {
                                UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'Tier')
                                 return(value);
                              }, {
                                "callback": function (sValue, y) {
                                    /* Redraw the table from the new data on the server */
                                    TiersTable.fnDraw();
                                }
                            });

                            $('#TiersTable tbody td.TierSelEditable').editable(function (value, settings) {
                                UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'Tier')
                                return ($("#value option:selected").text());
                            }, {
                                "callback": function (sValue, y) {
                                    /* Redraw the table from the new data on the server */
                                    TiersTable.fnDraw();
                                },
                                data: TierSelects,
                                type: 'select',
                                submit: 'OK'
                            });

                           
                        }
                    });
                    if (TiersTable.length > 0) {
                        TiersTable.fnAdjustColumnSizing();

                       

                    $(".BtnTierDelete").each(function () {
                        $(this).click(function () {
                            DeleteTierID = $(this).attr("deleteid")
                            $("#DeleteTierPop").dialog("open");
                            console.log(DeleteTierID)
                        });
                    });
                   
                  
                }
            }


         function GetTiersVisibilty() {

             var urlMethod = "../TierWebService.asmx/GetTiersVisibilty";

            var json = {'client': $('.current_client').text() };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetTiersVisibilty);

        }

        function ReturnGetTiersVisibilty(msg) {
            TiersVisibilityData = jQuery.parseJSON(msg.d)
            console.log(TiersVisibilityData);
            $("#TiersVisibilityTemplate").tmpl(TiersVisibilityData).appendTo("#TiersVisibilityTable tbody");


            var TiersVisibiltyTable = $('#TiersVisibilityTable').dataTable({
                "bJQueryUI": true,
                "bProcessing": true,
                "bDestroy": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%",
                "fnDrawCallback": function () {
                    $('#TiersVisibilityTable tbody td.editable').editable(function (value, settings) {
                         UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'),'TierVisibilty' )
                         console.log(value);
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            TiersVisibiltyTable.fnDraw();
                        }
                    });

                    $('#TiersVisibilityTable tbody td.TierSelEditable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'TierVisibilty')
                        return ($("#value option:selected").text());
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            TiersVisibiltyTable.fnDraw();
                        },
                        data: TierSelects,
                        type: 'select',
                        submit: 'OK'
                    });
                }
            });
            if (TiersVisibiltyTable.length > 0) {
                TiersVisibiltyTable.fnAdjustColumnSizing();
            }
            $(".BtnTierDelete").each(function () {
                $(this).click(function () {
                    DeleteTierID = $(this).attr("deleteid")
                    $("#DeleteTierPop").dialog("open");
                    console.log(DeleteTierID)
                });
            });
        }


        function GetWarrantyOptions() {

            var urlMethod = "../TierWebService.asmx/GetWarrantyOptions";

            var json = { 'client': $('.current_client').text() };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetWarrantyOptions);

        }

        function ReturnGetWarrantyOptions(msg) {
            WarrantyOptionsData = jQuery.parseJSON(msg.d)
            console.log(WarrantyOptionsData);
            $("#WarrantyTemplate").tmpl(WarrantyOptionsData).appendTo("#WarrantyTable tbody");


            WarrantyTable = $('#WarrantyTable').dataTable({
                "bJQueryUI": true,
                "bProcessing": true,
                "bDestroy": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "fnDrawCallback": function () {
                    $('#WarrantyTable tbody td.editable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'WarrantyOptions')
                        return (value);
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            WarrantyTable.fnDraw();
                        }
                    });
                }
            });

            if (WarrantyTable.length > 0) {
                WarrantyTable.fnAdjustColumnSizing();
            }

        }

        function GetTierPricing() {

            var urlMethod = "../TierWebService.asmx/GetTierPrice";

            var json = { 'client': $('.current_client').text() };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetTierPrice);

        }

        function ReturnGetTierPrice(msg) {
            TierPriceData = jQuery.parseJSON(msg.d)
            console.log(TierPriceData);
            $("#TierPriceTemplate").tmpl(TierPriceData).appendTo("#TierPriceTable tbody");


            TierPriceTable = $('#TierPriceTable').dataTable({
                "bJQueryUI": true,
                "bProcessing": true,
                "bDestroy": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "fnDrawCallback": function () {
                    $('#TierPriceTable tbody td.editable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'TierPricing')
                        
                        return (value);
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            TierPriceTable.fnDraw();
                        }
                    });
                }
            });

            if (TierPriceTable.length > 0) {
                TierPriceTable.fnAdjustColumnSizing();
            }

        }


        function UpdateInfo(info, column, rowid, table) {

            var urlMethod = "../TierWebService.asmx/EditTierInfo";

            var json = { 'client': $('.current_client').text(), 'info' : info, 'col': column, 'rowid': rowid, 'table' : table };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnUpdateInfo);

        }

        function ReturnUpdateInfo(msg) {
            console.log(msg.d);
        }

        function GetTierSelects() {

            var urlMethod = "../TierWebService.asmx/GetTierSelects";

            var json = { 'client': $('.current_client').text() 
                        
                        };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, function (msg) {
                TierSelects = jQuery.parseJSON(msg.d)  
                 }
           );

        }

        function GetNewTierSelects() {
            var urlMethod = "../TierWebService.asmx/GetNewTierSelects";

            var json = { 'client': $('.current_client').text()
            };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturGetNewTierSelects);
        }


        function ReturGetNewTierSelects(msg) {
            console.log(jQuery.parseJSON(msg.d))

            $("#NewTierSelect").tmpl(jQuery.parseJSON(msg.d)).appendTo("#txtBaseTier");
            
        }

        function AddTier() {

            var urlMethod = "../TierWebService.asmx/NewTier";

            var json = { 'client': $('.current_client').text(),
                'Tier': $('#txtTier').val(),
                'BaseTier': $('#txtBaseTier').val(),
                'CustomerLabel': $('#txtCustomerLabel').val(),
                'AdminLabel': $('#txtAdminLAbel').val()
                        
                        };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturAddTier);

        }

        function ReturAddTier(msg) {
            GetTierSelects()
            NewTierID = (msg.d)
            GetNewTiersVisibilty()

        }

        function GetNewTiersVisibilty() {

            var urlMethod = "../TierWebService.asmx/GetNewTiersVisibilty";

            var json = { 
                'client': $('.current_client').text(),
                'NewTierID' : NewTierID
             };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetNewTiersVisibility);

        }

        function ReturnGetNewTiersVisibility(msg) {
            NewTiersVisibilityData = jQuery.parseJSON(msg.d)
            console.log(NewTiersVisibilityData);
            $("#NewTier-2").dialog("open");
            $("#NewTier").dialog("close");
            $("#NewTierVisibilityTempalate").tmpl(NewTiersVisibilityData).appendTo("#EditNewVisibility tbody");


            var EditNewVisibility = $('#EditNewVisibility').dataTable({
                "bJQueryUI": true,
                "bProcessing": true,
                "bDestroy": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%",
                "fnDrawCallback": function () {
                    $('#EditNewVisibility tbody td.editable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'TierVisibilty')
                        console.log(value);
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            TiersVisibiltyTable.fnDraw();
                        }
                    });

                    $('#EditNewVisibility tbody td.TierSelEditable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'TierVisibilty')
                        return ($("#value option:selected").text());
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            EditNewVisibility.fnDraw();
                        },
                        data: TierSelects,
                        type: 'select',
                        submit: 'OK'
                    });
                }
            });
            if (EditNewVisibility.length > 0) {
                EditNewVisibility.fnAdjustColumnSizing();
            }

             $(".BtnNewTierVisibilityDelete").each(function () {
                            $(this).click(function () {
                                DeleteTierVisibilityID = $(this).attr("deleteid")
                                
                                console.log(DeleteTierVisibilityID)
                                DeleteTierVisibilty(DeleteTierVisibilityID)
                                var row = $(this).closest("tr").get(0);
                                EditNewVisibility.fnDeleteRow(EditNewVisibility.fnGetPosition(row));
                                
                            }); 
                         });   


        }


        function GetNewTiersWarranty() {

            var urlMethod = "../TierWebService.asmx/GetNewWarrantyOptions";

            var json = {
                'client': $('.current_client').text(),
                'NewTierID': NewTierID
            };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetNewTiersWarranty);

        }

        function ReturnGetNewTiersWarranty(msg) {
            NewTiersWarrantyData = jQuery.parseJSON(msg.d)
            console.log(NewTiersWarrantyData);
            $("#NewTier-3").dialog("open");
            $("#NewTier-2").dialog("close");
            $("#NewTierWarrantyTempalate").tmpl(NewTiersWarrantyData).appendTo("#EditNewWaranty tbody");


            var EditNewWarranty = $('#EditNewWaranty').dataTable({
                "bJQueryUI": true,
                "bProcessing": true,
                "bDestroy": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%",
                "fnDrawCallback": function () {
                    $('#EditNewWaranty tbody td.editable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'WarrantyOptions')
                        console.log(value);
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            EditNewWarranty.fnDraw();
                        }
                    });

                    $('#EditNewWaranty tbody td.TierSelEditable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'WarrantyOptions')
                        return ($("#value option:selected").text());
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            EditNewWarranty.fnDraw();
                        },
                        data: TierSelects,
                        type: 'select',
                        submit: 'OK'
                    });
                }
            });
            if (EditNewWarranty.length > 0) {
                EditNewWarranty.fnAdjustColumnSizing();
            }


        }

        function GetNewTiersPricing() {

            var urlMethod = "../TierWebService.asmx/GetNewTierPrice";

            var json = {
                'client': $('.current_client').text(),
                'NewTierID': NewTierID
            };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturnGetNewTiersPricing);

        }

        function ReturnGetNewTiersPricing(msg) {
            NewTiersPricingData = jQuery.parseJSON(msg.d)
            console.log(NewTiersPricingData);
            $("#NewTier-4").dialog("open");
            $("#NewTier-3").dialog("close");
            $("#NewTierBasePriceTempalate").tmpl(NewTiersPricingData).appendTo("#EditNewPrice tbody");


            var EditNewPrice = $('#EditNewPrice').dataTable({
                "bJQueryUI": true,
                "bProcessing": true,
                "bDestroy": true,
                "sScrollY": "250",
                "sScrollX": "100%",
                "sScrollXInner": "110%",
                "fnDrawCallback": function () {
                    $('#EditNewPrice tbody td.editable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'TierPricing')
                        console.log(value);
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            EditNewPrice.fnDraw();
                        }
                    });

                    $('#EditNewPrice tbody td.TierSelEditable').editable(function (value, settings) {
                        UpdateInfo(value, $(this).attr('column'), $(this).attr('rowid'), 'TierPricing')
                        return ($("#value option:selected").text());
                    }, {
                        "callback": function (sValue, y) {
                            /* Redraw the table from the new data on the server */
                            EditNewPrice.fnDraw();
                        },
                        data: TierSelects,
                        type: 'select',
                        submit: 'OK'
                    });
                }
            });
            if (EditNewPrice.length > 0) {
                EditNewPrice.fnAdjustColumnSizing();
            }


        }


        function DeleteTier(DeleteTierID) {

            var urlMethod = "../TierWebService.asmx/DeleteTier";

            var json = { 'Client': $('.current_client').text(),
                        'TierID': DeleteTierID
            };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturDeleteTier);

        }

        function ReturDeleteTier(msg) {
            window.location.reload()
        }

        function DeleteTierVisibilty(DeleteTierVisibiltyID) {

            var urlMethod = "../TierWebService.asmx/DeleteTierVisibility";

            var json = { 'Client': $('.current_client').text(),
                'TierVisibilityID': DeleteTierVisibiltyID
            };
            var jsonData = JSON.stringify(json);

            SendAjax(urlMethod, jsonData, ReturDeleteNewTier);

        }

        function ReturDeleteNewTier(msg) {
            
            
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
                    if (window.console) console.log(err.Message);
                }
            });
        }


    </script>

 <div id="container">
    <div class="row">
	    <div class="twocol">

            <div id="land-links">
                
                <table id="links-table">
                    <tbody>

                        <tr>
                            <td id="btnTier"><a>New Tier</a></td>
                        </tr>

                      
      
                    </tbody>
            
                </table>

            </div>
       </div>
       <div class="eightcol main">
            <div id="tabs">
	                <ul>
		                <li><a href="#tabs-1">Tiers</a></li>
		                <li><a href="#tabs-2">Tiers Visibility</a></li>
		                <li><a href="#tabs-3">Warranty Options</a></li>
                        <li><a href="#tabs-4">Tier Pricing</a></li>
	                </ul>
	                <div id="tabs-1">
		                
                        <table id="TiersTable" class="display">
                            <thead>
                                <tr>
                                    <td>Delete</td>
                                    <td>Tiers</td>
                                    <td>Base Tier</td>
                                    <td>Customer Label</td>
                                    <td>Admin Label</td>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>

                            <tfoot>
                            </tfoot>

                        </table>


	                </div>
	                <div id="tabs-2">
		               
                       <table id="TiersVisibilityTable" class="display">
                            <thead>
                                <tr>
                                    <td>Tier</td>
                                    <td>Visible Tier</td>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>

                            <tfoot>
                            </tfoot>

                        </table>

	                </div>
	                <div id="tabs-3">
		                <table id="WarrantyTable" class="display"> 
                            <thead>
                                <tr>
                                    <td>Part</td>
                                    <td>Tier</td>
                                    <td>Warranty</td>
                                    <td>Base</td>
                                    <td>Percentages</td>
                                    <td>Flat</td>
                                    <td>Link</td>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>

                            <tfoot>
                            </tfoot>

                        </table>
	                </div>
                    <div id="tabs-4">
		                <table id="TierPriceTable" class="display">
                            <thead>
                                <tr>
                                    <td>Tier</td>
                                    <td>Part</td>
                                    <td>Percentage</td>
                                    <td>Flat</td>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>

                            <tfoot>
                            </tfoot>

                        </table>
	                </div>
            </div>

       </div>
    </div>
</div>


<div id="NewTier">
    <label for="txtTier">Tier Name</label>
        <input type="text" id="txtTier"/>
    <label for="txtBaseTier">Base Tier</label>
        <select id="txtBaseTier">
        <option value="">None</option>
        </select>
    <label for="txtCustomerLabel">Customer Label</label>
        <input type="text" id="txtCustomerLabel"/>
    <label for="txtAdminLAbel">Admin Label</label>
        <input type="text" id="txtAdminLAbel"/>
        <br />
    
</div>

<div id="NewTier-2">
    <table id="EditNewVisibility" class="display">
    <thead>
        <tr>
            <td>Delete</td>
            <td>Tier</td>
            <td>Visible Tier</td>
        </tr>
    </thead>
    <tbody>
                                
    </tbody>

    <tfoot>
    </tfoot>

    </table>
    
</div>

<div id="NewTier-3">
   <table id="EditNewWaranty" class="display"> 
        <thead>
            <tr>
                <td>Part</td>
                <td>Tier</td>
                <td>Warranty</td>
                <td>Base</td>
                <td>Percentages</td>
                <td>Flat</td>
                <td>Link</td>
            </tr>
        </thead>
        <tbody>
                                
        </tbody>

        <tfoot>
        </tfoot>
    </table>
    
</div>

<div id="NewTier-4">
   <table id="EditNewPrice"" class="display">
        <thead>
            <tr>
                <td>Tier</td>
                <td>Part</td>
                <td>Percentage</td>
                <td>Flat</td>                     
            </tr>
        </thead>
        <tbody>
                                
        </tbody>

        <tfoot>
        </tfoot>

    </table>
    
</div>

<div id="DeleteTierPop">
<p>Are you sure you want to Delete this Tier?</p>
</div>

<div id="DeleteTierVisibilityPop">
<p>Are you sure you want to Delete this Tier Visibility?</p>
</div>

           
      


           
       


        
