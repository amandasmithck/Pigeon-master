<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RemanInventory.aspx.vb" Inherits="Pigeon.RemanInventory" MasterPageFile="~/Pages/Pigeon.Master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $('document').ready(function () {
            var remanInventoryViewModel = function () {
                //extended view models
                ko.utils.extend(this, new ajaxHelperViewModel());
                ko.utils.extend(this, new stringHelperViewModel());
                ko.utils.extend(this, new notificationHelperViewModel());

                //properties
                errorMsg = ko.observable("");

                availableStock = ko.observable();
                orderID = ko.observable();
                partID = ko.observable();
                partSelectionModalSpinner = ko.observable(false);

                availableStocks = ko.observableArray();
                orderParts = ko.observableArray();
             
                //functions
                setSection = function (sectionID) {
                    $("#divSection1").hide();
                    $("#divSection2").hide();

                    $("#divSection" + sectionID).slideDown();
                };

                loadPartSelectionModal = function (stock) {
                    setSection(1);
                    orderID('');
                    errorMsg('');
                    availableStock(stock);
                    $("#divPartSelectionModal").modal("toggle");
                };

                getOrderParts = function () {
                    try
                    {
                        if (!amIAnInteger(orderID())) { errorMsg("Order ID requires an integer."); return; }
                        
                        partSelectionModalSpinner(true);
                        var url = "../OrderWebService.asmx/GetOrderPartsCollection";
                        var data = {'orderid' : orderID(), 'client': user.Client };

                        ajaxHelper(url, "POST", data).done(function (msg) {
                            try
                            {
                                partSelectionModalSpinner(false);

                                if (msg == null || msg.d == null) { errorMsg("Missing returned response."); return; }

                                setSection(2);
                                var unfilteredOrderParts = msg.d;
                               // console.log(orderParts());
                                if (unfilteredOrderParts == null || unfilteredOrderParts.length == 0) {
                                    errorMsg("Missing Ordered Parts");
                                    return;
                                }
                                //all good
                                orderParts(unfilteredOrderParts.filter(function (a) { return a.Incorrect != true }));
                                //console.log(orderParts());
                            }
                            catch (err) {
                                partSelectionModalSpinner(false);
                                errorMsg(err);
                            }
                        });
                    }
                    catch (err)
                    {
                        partSelectionModalSpinner(false);
                        errorMsg(err);
                    }
                };
                   
                getAvailableStocks = function () {
                    var urlMethod = "../InventoryWebService.asmx/GetAvailableRemanStock";
                    var json = { 'client': user.Client };
                    var jsonData = JSON.stringify(json);

                    ajaxHelper(urlMethod, "POST", json).done(function (msg) {
                        availableStocks(msg.d);
                        $('#loader').hide();

                        $("#parts-table").dataTable({
                            "sDom": "<'row'<'col-md-6 hidden-xs'l><'col-md-6'f>r>t<'row'<'col-md-6'i><'col-md-6'p>>",
                            "oLanguage": {
                                "sLengthMenu": "_MENU_",
                                "sInfo": "Showing <strong>_START_ to _END_</strong> of _TOTAL_ entries"
                            },
                            "oClasses": {
                                "sFilter": "pull-right",
                                "sFilterInput": "form-control input-transparent ml-sm"
                            },
                            "bPaginate": false
                            //"aoColumns": unsortableColumns
                        });
                    });
                };


                remanPart = function (p) {
                    //set part in case we need to reference it later
                    partID(p.PartID);

                    //server access
                    var url = "../InventoryWebService.asmx/AssignRemanPart";
                    var data = {'orderid':orderID(), 'availableStock':availableStock(), 'partID':partID() };
                  
                    ajaxHelper(url, "POST", data).done(function (msg) {
                        try
                        {
                            if (msg == null || msg.d == null) { notify("Response returned no results.", false, null); return; }
                            if (msg.d == false) {
                                notify("Whoa, Something went wrong, probably user error!", false, null);
                                return;
                            }

                            //all good     
                            $("#trAvailStocks" + availableStock().ID).hide();
                            notify("Good job!, Part pulled and assigned", true, null);
                            $("#divPartSelectionModal").modal("toggle");
                        }
                        catch (err) {
                            errorMsg(err);
                        }
                     
                    });                   
                };

                //LOAD
                pageLoad = function () {
                    getAvailableStocks();
                };

                pageLoad();

                //public methods and properties
                return {
                    errorMsg : errorMsg, //Properties
                    orderID : orderID,
                    partID : partID,
                    availableStocks : availableStocks,
                    partSelectionModalSpinner: partSelectionModalSpinner,
                    orderParts: orderParts,//Methods
                    loadPartSelectionModal : loadPartSelectionModal,
                    getOrderParts: getOrderParts,
                    remanPart: remanPart
                };
            }

            ko.applyBindings(remanInventoryViewModel);

        });
    </script>

    
    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em; overflow: visible; height: 80px;">
            <div class="span12">
                <h3>Available Reman Inventory</h3>
                <img id="loader" src="/images/ajax-loader-blue.gif" />
                <table id="parts-table" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>Part Type</th>
                            <th>Part No</th>
                            <th>SN No</th>
                            <th>Warehouse</th>
                            <th>Cost</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody data-bind="foreach: availableStocks">
                        <tr data-bind="attr:{id : 'trAvailStocks' + ID}">
                            <td data-bind="text: PartType"></td>
                            <td data-bind="text: Part"></td>
                            <td data-bind="text: SN"></td>
                            <td data-bind="text: Warehouse"></td>
                            <td data-bind="text: CKCost"></td>
                            <td style="text-align:center"><input type="button" class="btn btn-primary btn-small" value="Pull" data-bind="attr: { id: ID, client: Client }, click: $parent.loadPartSelectionModal"/></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>


        <div id="divPartSelectionModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="part-selection-header" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="part-selection-header">Part Selection Modal</h3>
        </div>
        <div class="modal-body">
            <div style="color:maroon;font-size:1em;text-align:center;width:100%;" data-bind="text : $root.errorMsg"></div>
            <div data-bind="visible:$root.partSelectionModalSpinner" style="width:100%;text-align:center;height:auto;"><img src="../images/ajax-loader-blue.gif" /></div>

            <div  class="form-group" id="divSection1" style="text-align:center;">
            <h4>Input the Order Id to Retrive Part List</h4>
                <div style="clear:both;height:10px;"></div>
                <input type="number" style="width:250px;font-size:1.5em;" class="form-control" data-bind="value : $root.orderID" />
                <div style="clear:both;height:10px;"></div>
                <div>
                    <button class="btn btn-success" id="btnSelectPart" data-bind="click : $root.getOrderParts"  >Okay</button> &nbsp;&nbsp;&nbsp;
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>          
                </div>
            </div>
       
             <div class="form-group" id="divSection2"   style="text-align:center;">
                <h4>Select the Part Being Remanded</h4>
                <div style="clear:both;height:10px;"></div>

                <table class="table table-bordered table-striped" style="width:100%;height:auto;">
                 <thead>
                     <tr>
                         <th style="width:300px;height:auto;">Part Desc</th>
                         <th style="width:75px;height:auto;">Action</th>
                     </tr>
                 </thead>
                 <tbody data-bind="foreach: $root.orderParts">
                     <tr>
                         <td style="width:300px;height:auto;" data-bind="text: Row + '.' + ' '+ PartNo + ' ' + PartDescription ">
                             <span data-bind="text : PartDescription"></span> &nbsp;
                                <span style="font-style:italic;" data-bind="if: Defect">(Defect)</span>
                         </td>
                         <td style="width:75px;height:auto;"><a href="#" data-bind="click: $root.remanPart">Reman</a></td>
                     </tr>
                 </tbody>
             </table>
                <div style="clear:both;height:10px;"></div>

            </div>

        </div>

    </div>
</asp:Content>