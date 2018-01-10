<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Order.aspx.vb"  Inherits="Pigeon.Order" %>

<!doctype html>
<html lang="en-us">
<head id="Head1" runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Montserrat:700,400' rel='stylesheet' type='text/css'>
    <link href='../Styles/widget.css' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="../Styles/theme.css" id="themestyle">
    <link href='../Styles/order.css' rel='stylesheet' type='text/css'>


    <script type="text/javascript" src="../Scripts/jquery.min.js" ></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.10.custom.min.js" ></script>



    <%--needed for widget--%>
    <script type="text/javascript" src="../Scripts/jstorage.js"></script>
    <script type="text/javascript" src="../Scripts/wl_Store.js"></script>
    <script type="text/javascript" src="../Scripts/wl_Widget.js"></script>

     <!--good ol bootstrap [2.1] -->
    <link href="~/Styles/bootstrap/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap/bootstrap.js"></script>
    <script src="../Scripts/bootstrap-typeahead.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.min.js"></script>

    <!--noty-->
    <script type="text/javascript" src="../Scripts/noty/jquery.noty.js"></script>
    <script type="text/javascript" src="../Scripts/noty/layouts/bottomRight.js"></script>
    <script type="text/javascript" src="../Scripts/noty/layouts/topRightOrder.js"></script>
    <script type="text/javascript" src="../Scripts/noty/themes/default.js"></script>

    <!--upload-->
    <script type="text/javascript" src="../Scripts/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.fileupload.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.fileupload-fp.js"></script>
    <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../Styles/jquery.fileupload-ui.css">

    <!-- datatable -->
    <link type="text/css" href="../Styles/datatable/table2.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery.dataTables.min.js"></script>

    <script type="text/javascript" src="../Scripts/accounting.js"></script>
    <script type="text/javascript" src="../Scripts/moment.min.js"></script>
    <script type="text/javascript" src="../Scripts/underscore.min.js"></script>
       <script src="../Scripts/knockout_3_2_0.js"></script>     
   

    <script type="text/javascript" src="../Scripts/order.js"></script>
    <script type="text/javascript">
        var user = <%= Session("UserModel") %>
    </script>
    <script id="notesTemplate" type="text/html">
        <tr {{if Vendor}} class="info" {{/if}}>
           <td style="font-size:10px">${NoteDate}</td> 
           <td  style="font-size:12px">${NoteUser}</td> 
           <td  style="font-size:12px">${Notes}</td>
        </tr>
     </script>
    <script id="historyTemplate" type="text/html">
        <tr>
           <td style="font-size:10px">${TrackDate}</td> 
           <td  style="font-size:12px">${TrackUser}</td> 
           <td  style="font-size:12px">${TrackAction}</td>
        </tr>
     </script>
    <script id="docsTemplate" type="text/html">
        <tr>
           <td><a href="${url}" target="_blank"><img src="${thumbnailurl}" /></a><br />${filename}-Uploaded ${uploaddate} by ${uploadedby}<p class="doc-delete" rel="${docid}" style="text-decoration:underline;cursor: pointer;">Delete</p></td>
        </tr>
     </script>
    <script id="invoicesTemplate" type="text/html">
         <tr {{if InvoiceTypeID == 1}} class="success" {{/if}} {{if Flow == 'Out'}} style="color:red" {{/if}} rel="${InvoiceID}">
            <td style="font-size:11px">${DateEntered}</td> 
            <td style="font-size:11px" class="invoice-no">${InvoiceNo}</td> 
            <td><a href="#" class="invoice-type{{if InvoiceTypeID == 1 || InvoiceTypeID == 5 || InvoiceTypeID ==6 || InvoiceTypeID ==134}} printable{{/if}}" style="font-size:11px;{{if Flow == 'Out'}}color:red{{else}}color:black{{/if}}">${InvoiceType}</a></td>
            <td style="font-size:11px">${Company}</td>
            <td style="font-size:11px" class="invoice-amt">${accounting.formatMoney(Amount)}</td>
            <td style="font-size:11px" class="invoice-amt-paid" contenteditable="true">${accounting.formatMoney(AmountPaid)}</td>
            <td style="font-size:11px" contenteditable="true" class="invoice-date-paid">${DatePaid}</td>
             <td style="font-size: 10px" >
                <div class="btn-group">
                    <a class="btn btn-link invoice-pay-type" style="padding:0px" href="#" >${PaymentType}</a>
                    <a class="btn btn-link dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="#" class="select-pay-type">None</a></li>
                        <li><a href="#" class="select-pay-type">Amex-Chris</a></li>
                        <li><a href="#" class="select-pay-type">Amex-Rick</a></li>
                        <li><a href="#" class="select-pay-type">Amex-In</a></li>
                        <li><a href="#" class="select-pay-type">Check</a></li>
                        <li><a href="#" class="select-pay-type">Visa</a></li>
                        <li><a href="#" class="select-pay-type">Wire/ACH</a></li>
                    </ul>
                </div>
             </td>
            <td style="font-size:11px" contenteditable="true" class="invoice-checkno">${CheckNo}</td>
            
        </tr>
     </script>
     <script id="orderInfoTemplate" type="text/html">
                             <div style="float: left">
                        <form class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label order-label" for="order-date">Date Ordered:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-date">${DateOrdered}</label>
                                </div>
                                <label class="control-label order-label" for="order-customer">Customer:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-customer">${Company}</label>
                                </div>
                                <label class="control-label order-label" for="order-adjuster">Adjuster:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-adjuster" contenteditable="true">${AdjusterName}</label>
                                </div>
                                <label class="control-label order-label" for="order-contract">Contract No:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-contract" contenteditable="true">${ContractNo}</label>
                                </div>
                                <label class="control-label order-label" for="order-auth">Auth No:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-auth" contenteditable="true">${AuthorizationNo}</label>
                                </div>
                                <label class="control-label order-label" for="order-owner">Owner:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-owner" contenteditable="true">${AutoOwner}</label>
                                </div>
                                <label class="control-label order-label" for="order-contract-miles">Contract End Miles:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-contract-miles" contenteditable="true">${ContractEndMiles}</label>
                                </div>
                                <label class="control-label order-label" for="order-contract-date">Contract End Date:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-contract-date" contenteditable="true">${ContractEndDate}</label>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div style="float: left">
                        <form class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label order-label" for="order-mileage">Mileage:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-mileage" contenteditable="true">${Mileage}</label>
                                </div>
                                <label class="control-label order-label" for="order-year">Year:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-year" contenteditable="true">${AutoYear}</label>
                                </div>
                                <label class="control-label order-label" for="order-make">Make:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-make">${AutoMake}</label>
                                </div>
                                <label class="control-label order-label" for="order-model">Model:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-model" contenteditable="true">${AutoModel}</label>
                                </div>
                                <label class="control-label order-label" for="order-drive">Drive:</label>
                                <div class="controls">
                                    <div class="btn-group">
                                        <a class="btn btn-link drive-type" id="order-drive" style="padding:0px" href="#" >${Drive}</a>
                                        <a class="btn btn-link dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="#" class="select-drive-type">FWD</a></li>
                                            <li><a href="#" class="select-drive-type">RWD</a></li>
                                            <li><a href="#" class="select-drive-type">4WD</a></li>
                                            <li><a href="#" class="select-drive-type">AWD</a></li>
                                            <li><a href="#" class="select-drive-type">4X4</a></li>
                                            <li><a href="#" class="select-drive-type">2WD</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <label class="control-label order-label" for="order-trans">Trans:</label>
                                <div class="controls">
                                    <div class="btn-group">
                                        <a class="btn btn-link trans-type" id="order-trans" style="padding:0px" href="#" >${Transmission}</a>
                                        <a class="btn btn-link dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="#" class="select-trans-type">Automatic</a></li>
                                            <li><a href="#" class="select-trans-type">Manual</a></li>
                                            <li><a href="#" class="select-trans-type">Unknown</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <label class="control-label order-label" for="order-vin">VIN:</label>
                                <div class="controls">
                                    <label class="order-text" id="order-vin" contenteditable="true">${VinNo}</label>
                                </div>
                                <label class="control-label order-label" for="btnSaveOrder"></label>
                                <div class="controls">
                                    <span id="btnSaveOrder" class="btn" style="margin-top:10px">Save Changes</span>
                                </div>
                            </div>
                        </form>
                    </div>
     </script>
    <script id="partsTabTemplate" type="text/html">
         <li><a href="#${PartID}" data-toggle="tab" onclick="return SetCurrentPartID(${PartID})" style="font-size:11px" prev="${PreviousPartID}" row="${Row}">#${Row}:  ${PartDescription}<br />{{if Defect}}defect{{/if}}{{if Incorrect}}incorrect{{/if}}</a></li>
    </script>

    <script id="partsContentTemplate" type="text/html">
        <div class="tab-pane" id="${PartID}">
            <div style="width:100%;margin-bottom:10px"><span style="font-weight:bold">Date Ordered:</span> ${DateOrdered}<span style="font-weight:bold; margin-left:20px">Core:</span> Out<span style="font-weight:bold;padding-left:15px">Defect:</span> Back<span style="font-weight:bold;padding-left:15px">Incorrect:</span> N/A</div>
            <div style="float:left">
                <legend>Part Info:</legend>
                <form class="form-horizontal partorder">
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Vendor">Vendor</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Vendor" value="${Vendor}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_PartType">Type</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_PartType" value="${PartType}" readonly="true">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_PartDescGroup">Group</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_PartDescGroup" value="${PartDescGroup}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_PartDescription">Description</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_PartDescription" value="${PartDescription}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Brand">Brand</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Brand" value="${Brand}" readonly="true">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_PartNo">Part No.</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_PartNo" value="${PartNo}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Serial">Serial No.</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Serial" value="${Serial}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Quantity">Quantity</label>
                        <div class="controls">
                            <input type="number" id="${PartID}_Quantity" value="${Quantity}">
                        </div>
                    </div>
                    <br/>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Weight">Weight</label>
                        <div class="controls">
                            <input type="number" id="${PartID}_Weight" value="${Weight}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Length">Length</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Length" value="${Length}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Width">Width</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Width" value="${Width}">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Height">Height</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Height" value="${Height}">
                        </div>
                    </div>
                    <br/>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Warehouse">Warehouse</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Warehouse" value="${Warehouse}" readonly="true">
                        </div>
                    </div>
                    <br/>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CostPrice">Unit Cost</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CostPrice" value="${CostPrice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CorePrice">Core Value</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CorePrice" value="${CorePrice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CoreCost">Core Cost</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CoreCost" value="${CoreCost}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_ShippingCost">Shipping Cost</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_ShippingCost" value="${ShippingCost}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_VendorInvoiceNo">Vendor Inv No.</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_VendorInvoiceNo" value="${VendorInvoiceNo}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CoreCredit">Core Credit</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CoreCredit" value="${CoreCredit}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_IncorrectDefectCredit">Inc/Def Credit</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_IncorrectDefectCredit" value="${IncorrectDefectCredit}" >
                        </div>
                    </div>
                </form>
            </div>
            <div style="float:left">
                <legend>Shipment Info:</legend>
                <form class="form-horizontal partorder">
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_ExpShipDate">Expected Ship</label>
                        <div class="controls">
                            <input type="text"  id="${PartID}_ExpShipDate" placeholder="mm/dd/yy" size="40" value="${ExpShipDate}" {{if ArriveDate != null}} readonly="true" {{else}} class="dt-picker" {{/if}}>
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Arrival">Promised Arrival</label>
                        <div class="controls">
                            <input type="text"  id="${PartID}_Arrival" placeholder="mm/dd/yy" size="40" value="${Arrival}" {{if ArriveDate != null}} readonly="true" {{else}} class="dt-picker" {{/if}}>
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_UpdatedArrival">Updated Arrival</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_UpdatedArrival" placeholder="mm/dd/yy" size="40" value="${UpdatedArrival}" {{if ArriveDate != null}} readonly="true" {{else}} class="dt-picker" {{/if}}>
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_FreightETA">Freight ETA</label>
                        <div class="controls">
                            <input type="text"  id="${PartID}_FreightETA" placeholder="mm/dd/yy" size="40" value="${FreightETA}" {{if ArriveDate != null}} readonly="true" {{else}} class="dt-picker" {{/if}}>
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_ArriveDate">Arrive Date</label>
                        <div class="controls">
                            <input type="text" class="dt-picker" id="${PartID}_ArriveDate" placeholder="mm/dd/yy" size="40" value="${ArriveDate}">
                        </div>
                    </div>
                </form>
            </div>
            <div style="float:left">
                <legend>Customer Info:</legend>
                <form class="form-horizontal partorder">
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Customer">Customer</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Customer" value="${Customer}" readonly="true">
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_SellPrice">Sell Price</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_SellPrice" value="${SellPrice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CoreCharge">Core Charge</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CoreCharge" value="${CoreCharge}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CustShippingPrice">Shipping</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CustShippingPrice" value="${CustShippingPrice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CustCoreShippingPrice">Return Shipping</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CustCoreShippingPrice" value="${CustCoreShippingPrice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_WarrantyCost">Warranty Cost</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_WarrantyCost" value="${WarrantyCost}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Warranty">Warranty Type</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Warranty" value="${Warranty}" >
                        </div>
                    </div>
                    <br/>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_FreightInvoice">Freight Invoice</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_FreightInvoice" value="${FreightInvoice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CoreInvoice">Core Invoice</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CoreInvoice" value="${CoreInvoice}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_PartRefund">Part Refund</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_PartRefund" value="${PartRefund}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_CoreRefund">Core Refund</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_CoreRefund" value="${CoreRefund}" >
                        </div>
                    </div>
                </form>
            </div>
            <div style="float:left">
                <legend>Servicer Info:</legend>
                <form class="form-horizontal partorder">
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Servicer">Servicer</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Servicer" value="${Servicer}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Address1">Address</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Address1" value="${Address1}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_City">City</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_City" value="${City}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_State">State</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_State" value="${State}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Zip">Zip</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Zip" value="${Zip}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Phone">Phone</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Phone" value="${Phone}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Fax">Fax</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Fax" value="${Fax}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_Contact">Contact</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_Contact" value="${Contact}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_ContactPhone">Direct Phone</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_ContactPhone" value="${ContactPhone}" >
                        </div>
                    </div>
                    <div class="control-group no-bottom-margin">
                        <label class="control-label" for="${PartID}_LaborRate">Labor Rate</label>
                        <div class="controls">
                            <input type="text" id="${PartID}_LaborRate" value="${LaborRate}" >
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </script>

    <script id="oemAvailTemplate" type="text/html">
        <div style="margin-bottom:10px;width:500px;text-align:left;">
            <span style="text-decoration: underline">${Name}</span><br />
            <span style="font-size:11px">Contact:${Contact}<br />Phone:${Phone}<br />Cutoff:${Cutoff}<br />Parts:${Status}<br /></span>
            <ul id="${Hyperion}" {{if Status=='All Parts'}} style="display:none"{{/if}}></ul>
        </div>
        
    </script>
    <script id="smallOptionsTemplate" type="text/html">
        <div style="margin-bottom:10px;width:500px;text-align:left;" id="small-${PartID}">
            <span style="text-decoration: underline">${PartDescription}</span><br />
            <span style="font-size:11px">${PartNo}</span>
        </div>
        
    </script>
        <script id="partsTotalSummaryTemplate" type="text/html">
        <tr style="{{if Incorrect== true}}background-color:yellow;{{/if}}{{if Defect == true}}background-color:#6EA3CC;{{/if}}">
           <td class="tdPartsSummaryLarge"  ><a onclick="MoveScreenToPartsContent();return SetCurrentPartID(${PartID})" " href="#${PartID}" data-toggle="tab" style="font-size:11px" prev="${PreviousPartID}" row="${Row}">${DateOrdered}</a></td> 
           <td class="tdPartsSummarySmall"   style="">${PartType}</td> 
           <td class="tdPartsSummarySmall"   style="">${Quantity}</td>
           <td  class="tdPartsSummaryMedium"  style="">${PartNo}</td>
           <td class="tdPartsSummaryLarge"   style="">${PartDescription}</td>
           <td  class="tdPartsSummaryLarge"  style="">${Vendor}</td>
            <td  class="tdPartsSummaryMedium" style="">${ExpShipDate}</td>
            <td class="tdPartsSummaryMedium"   style="">${Arrival}</td>
            <td  class="tdPartsSummaryMedium"  style="">${UpdatedArrival}</td>
            <td  class="tdPartsSummarySmall"  style="">{{if Core == false }}No{{else}}Yes{{/if}}</td>
            <td  class="tdPartsSummarySmall"  style="">${Servicer}</td>
            <td class="tdPartsSummarySmall"   style="">${Phone}</td>
            <td class="tdPartsSummarySmall"   style="">${SellPrice}</td>
            <td class="tdPartsSummaryMedium"   style="">${ShippingPrice}</td>
            <td  class="tdPartsSummaryMedium"  style="">${ShippingCost}</td>
            <td  class="tdPartsSummaryMedium"  style="">${CoreCost}</td>
            <td  class="tdPartsSummaryMedium"  style="">${CustCoreShippingPrice}</td>
            <td  class="tdPartsSummaryMedium"  style="">${Warranty}</td>
            <td class="tdPartsSummaryMedium"   style="">${CostPrice}</td>
            <td  class="tdPartsSummaryMedium"  style="">${CoreCost}</td>
        </tr>
     </script>

    <script id="vendorDropDownTemplate" type="text/html">
        <option value="${VendorShippingID}">${VanityShippingName}</option>
    </script>

        <script id="koDropDownPartsTemplate" type="text/html">
        <option value="${PartID}">${PartDescription}</option>
    </script>

    <style>
        .tdShipmentItemSelection {
            text-align:center;
            width:50px;
        }

        .tdShipmentCount {
            text-align:center;
            width:150px;
        }

        .tdShipmentVendor {
            text-align:center;
            width:300px;
        }

        .tdShipmentDesc {
            text-align:center;
            width:200px;
        }

        .tdShipmentType {
            text-align:center;
            width:250px;
        }

        .tdShipmentTime {
            text-align:center;
            width:200px;
        }

        .tdShipmentRate {
            text-align:center;
            width:200px;
        }

        .tdShipmentRateSelection {
            text-align:center;
            width:50px;
        }

        .txtShipDim{
            text-align:center;
            width:35px;
            margin-top:5px;
        }

        .txtSchedulePickupFieldLarge {
            text-align:center;
            width:250px;
        }

        .txtSchedulePickupFieldMedium {
            text-align:center;
            width:150px;
        }

   .txtSchedulePickupFieldSmall {
            text-align:center;
            width:75px;
        }

      .txtSchedulePickupFieldExtraSmall {
            text-align:center;
            width:50px;
        }
        .divDimSection {
            margin-left:8px;
            margin-right:8px;
            float:left;
        }

        .tdShipTracNum {
            width:200px;
            text-align:center;
        }

        .tdShippingDetail {
       width:200px;
       font-size:1em;
       text-align:center;
             }

        .tdShippingDetailLong{
       width:300px;
       text-align:center;
             }

        .divGeneralAvailSection{
            width:500px;
            text-align:left;
        }

        .divAvailSectHdr {
            width:98%;
            text-align:left;
            margin-left:10px;
            font-size:1.4em;
            margin-bottom:20px;
            clear:both;
        }

        .divAvailSectBody{
         width:98%;
         margin-left:20px;
         text-align:left;
         font-size:1.2em;
         border-collapse:collapse;
         border:0px none transparent;
        }

        .divAvailLoader {
            width:98%;
            text-align:center;
        }

        .tdPartsSummarySmall {
            font-size:12px;
            text-align:center;
            width:50px;
        }

        .tdPartsSummaryMedium {
            font-size:12px;
           text-align:center;
            width:100px;
        }

      .tdPartsSummaryLarge {
            font-size:12px;
            text-align:center;
            width:150px;
        }

        

    </style>
 </head>
<body>
    
    <!--Header -->
    <div class="navbar navbar-fixed-top navbar-inverse">
        <div class="navbar-inner" style="padding:5px">
            <a class="brand" id="order-id" href="#" style="color:white;font-size:14px"></a>
            <input id="quick-switch" type="text" placeholder="Switch" class="input-mini pull-left" style="margin: 5px">
            <span id="dateordered" class="brand" style="padding: 10px;text-align:center;font-family: 'Montserrat', sans-serif;margin-left:8px;font-size:14px;color:white"></span>
            <span id="customer" class="brand" style="padding: 10px;text-align:center;font-family: 'Montserrat', sans-serif;margin-left:8px;font-size:14px;color:white"></span>
            <span id="vehicle"  class="brand" style="padding: 10px;text-align:center;font-family: 'Montserrat', sans-serif;margin-left:8px;font-size:14px;color:white"></span>
            <span id="vinno"  class="brand" style="padding: 10px;text-align:center;font-family: 'Montserrat', sans-serif;margin-left:4px;font-size:14px;color:white"></span>
            <span id="mileage"  class="brand" style="padding: 10px;text-align:center;font-family: 'Montserrat', sans-serif;margin-left:4px;font-size:14px;color:white"></span>
            <ul class="nav pull-right">
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" style="color:white" data-toggle="dropdown">Options
      <b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a href="#cancel-modal" data-toggle="modal">Cancel Order</a></li>
                        <li><a href="#docs-modal" data-toggle="modal">Documents</a></li>
                        <li><a href="#history-modal" data-toggle="modal">History</a></li>
                        <li><a href="#" id="addpart" data-toggle="modal">Add Part</a></li>
                         <li><a href="#divDeleteShipment" data-toggle="modal">Delete Shipment</a></li>
                        <li><a href="#divPartsAvailabilityModal" id="lnkPartsAvailability" data-toggle="modal">Part Availability</a></li>
                        <li><a href="#divShipmentModal" id="lnkCreateShipment" data-toggle="modal">Create Shipment</a></li>
                        <li><a href="#divShipmentModal" id="lnkReturnCreateShipment" data-toggle="modal">Create Return Shipment</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>


    <!--Widgets -->
    <section id="content">
        

        <div class="g6 widgets">
            <div class="widget" id="order_widget">
                <h3 class="handle">Order Info</h3>
                <div id="order-info" style="height: 275px; overflow-y: scroll; display: block;">
                    <div id="order-loader" class="loader">
                        <img src="/images/ajax-loader-blue.gif" />
                    </div>
                </div>
            </div>
            <div class="widget" id="notes_widget">
                <h3 class="handle">Notes</h3>
                <div id="notes" style="height: 500px; overflow-y: scroll; display: block;">
                    <a href="#" onclick="$('#notes').prop({ scrollTop: $('#notes').prop('scrollHeight') });" style="float:left">Jump to bottom</a>
                    <a href="#" id="notes-all" style="float:right; padding-left:10px">All</a>
                    <a href="#" id="notes-more" style="float:right; padding-left:10px;display:none">More</a>
                    <a href="#" id="notes-less" style="float:right; padding-left:10px">Less</a>
                    
                    
                    <div id="notes-loader" class="loader">
                        <img src="/images/ajax-loader-blue.gif" />
                    </div>
                    <table id="notes-table" class="table table-striped table-bordered table-hover table-condensed">
                        <tr>
                            <th style="width:16%">Date</th>
                            <th>User</th>
                            <th>Notes</th>
                        </tr>
                    </table>
                    <p></p>
                    <label class="checkbox">
                        <input id="chkVendor" type="checkbox">
                        Visible to Vendor
                    </label>
                    <%--<textarea id="txtNewNote" rows="3" class="span11" placeholder="Add new note…"></textarea>--%>
                    <div id="txtNewNote" contenteditable="true">Enter new notes here...</div>
                    <%--<span id="btnSaveNote" class="btn" style="margin-top:20px">Save</span>--%>
                </div>
            </div>
        </div>
        <div class="g6 widgets">
            <div class="widget" id="invoices_widget">
                <h3 class="handle">Invoices</h3>
                <div id="invoices" style="display: block;">
                    <div id="invoices-loader" class="loader">
                        <img src="/images/ajax-loader-blue.gif" />
                    </div>
                    <table id="invoices-table" class="table table-striped table-bordered table-hover">
                        <thead><tr>
                            <th style="width:16%">Date</th>
                            <th>Invoice No.</th>
                            <th>Invoice Type</th>
                            <th>Company</th>
                            <th>Amount</th>
                            <th>Amount Paid</th>
                            <th>Date Paid</th>
                            <th>Pay Type</th>
                            <th>Check No.</th>
                        </tr></thead>
                    </table>
                </div>
            </div>
            
        </div>
        
          

    </section>

  <section id="parts_total_summary_content">
                 <div class="g12 widgets">
                <div class="widget" id="ffdfdd">
                <h3 class="handle">Parts Total Summary</h3>
                <div id="parts_total_summary_widget" style="display:block;">
                    <div id="parts_total_summary_loader" class="loader">
                        <img src="/images/ajax-loader-blue.gif" />
                    </div>
                    <table id="tblPartsTotalSummary" class="table table-striped table-bordered">
                      <tr>
                         <td  class="tdPartsSummaryLarge"   style="font-weight:bolder;">Ordered</td>  
                         <td class="tdPartsSummarySmall" style="font-weight:bolder;">Type</td> 
                         <td class="tdPartsSummarySmall" style="font-weight:bolder;">Quantity</td>     
                         <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Part #</td>
                         <td class="tdPartsSummaryLarge" style="font-weight:bolder;">Part</td>           
                         <td class="tdPartsSummaryLarge" style="font-weight:bolder;">Vendor</td>                                     
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Exp Ship</td>    
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Exp Arr</td>    
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Updated Arr</td>    
                          <td class="tdPartsSummarySmall" style="font-weight:bolder;">Core</td>                                          
                          <td class="tdPartsSummarySmall" style="font-weight:bolder;">Servicer</td> 
                          <td class="tdPartsSummarySmall" style="font-weight:bolder;">Phone</td> 
                          <td class="tdPartsSummarySmall" style="font-weight:bolder;">Sell Price</td> 
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Shipping</td> 
                           <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Shipping<br />Charges</td> 
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Core<br />Charge</td> 
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Return<br />Shipping</td> 
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Warranty</td> 
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Unit<br />Cost</td> 
                          <td class="tdPartsSummaryMedium" style="font-weight:bolder;">Core<br />Cost</td> 
                    </tr>

                      <tr id="trPartsTotalSummary"><td></td></tr>
                    </table>                  
                </div>                
            </div>
        </div>
    </section>
  
 
    <section id="part_content" >
        <div class="g12 widgets">
            <div class="widget" id="ffdfd">
                <h3 class="handle">Parts</h3>
                <div id="parts_widget" style="display:block;">
                    <div id="parts-loader" class="loader">
                        <img src="/images/ajax-loader-blue.gif" />
                    </div>
                    <div class="tabbable tabs-left">
                        <ul id="parts_list" class="nav nav-tabs">
                           
                        </ul>
                        <div id="parts_content" class="tab-content">
                            
                            
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </section>
    
   



    <!--Modals-->
    <div id="cancel-modal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="cancel-Label" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="cancel-Label">Cancel Order</h3>
        </div>
        <div class="modal-body">
            <p>Are you sure you wish to cancel this order?</p>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Nope</button>
            <button id="cancel-order" class="btn btn-danger">Cancel</button>
        </div>
    </div>

    <div id="docs-modal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="docs-Label" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="docs-label">Documents</h3>
        </div>
        <div class="modal-body" style=    "overflow-x:hidden">
            <!-- The file upload form used as target for the file upload widget -->
            <form id="fileupload" action="//jquery-file-upload.appspot.com/" method="POST" enctype="multipart/form-data">
                <!-- Redirect browsers with JavaScript disabled to the origin page -->
                <noscript>
                    <input type="hidden" name="redirect" value="http://blueimp.github.com/jQuery-File-Upload/"></noscript>
                <!-- The fileupload-buttonbar contains buttons to add/delete files and start/cancel the upload -->
                <div class="row fileupload-buttonbar">
                    <div class="span7">
                        <!-- The fileinput-button span is used to style the file input field as button -->
                        <span class="btn btn-success fileinput-button">
                            <i class="icon-plus icon-white"></i>
                            <span>Drag file here or click to add...</span>
                            <input type="file" name="files[]" multiple>
                        </span>
                    </div>
             
                </div>
                <!-- The loading indicator is shown during file processing -->
                <div class="fileupload-loading"></div>
          
            </form>
            <div id="docs-loader" class="loader">
                <img src="/images/ajax-loader-blue.gif" />
            </div>
            <table id="docs-table" class="table table-striped table-bordered">
                <tr>
                    <th></th>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" data-dismiss="modal">Close</button>
        </div>
    </div>

    <div id="history-modal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="cancel-Label" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="H1">History</h3>
        </div>
        <div class="modal-body">
            <div id="history-loader" class="loader">
                <img src="/images/ajax-loader-blue.gif" />
            </div>
            <table id="history-table" class="table table-striped table-bordered">
                <tr>
                    <th style="width:16%">Date</th>
                    <th>User</th>
                    <th>Action</th>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>
        </div>
    </div>

    <div id="delete-invoice-modal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="delete-invoice-Label" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="delete-invoice-Label">Delete Invoice</h3>
        </div>
        <div class="modal-body">
            <p id="delete-invoice-body">Are you sure you wish to delete this invoice?</p>
        </div>
        <div class="modal-footer">
            <button id="delete-invoice-close" class="btn" data-dismiss="modal" aria-hidden="true">Nope</button>
            <button id="delete-invoice" class="btn btn-danger">Delete</button>
        </div>
    </div>

    <div id="divShipmentModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="hdrCreateShipment" aria-hidden="true">
        <div class="modal-header"><h3 id="hdrCreateShipment">Create Shipment</h3></div>
        <input type="hidden" value="0" id="hfShipmentMode" />
         <div id="divShipmentLoader" class="loader" style="display:none;text-align:center;width:100%;height:150px;margin-top:75px;">
                <img src="/images/ajax-loader-blue.gif" />
            </div>

            <div id="divShipmentRatesSection">
            <div class="modal-body">
            <div id="divPartSelection" style="width:100%;display:block;">
                <table>
                    <thead style="display:block;background-color:#CEF6F5;">
                        <tr> 
                            <th class="tdShipmentCount">Select Part</th>          
                            <th class="tdShipmentVendor" >Vendor</th>
                            <th class="tdShipmentDesc" >Description</th>
                            <th style="width:15px;"></th>
                        </tr>    
                    </thead>
                    <tr id="trPartsForShipping"></tr>
                </table>

                <div style="clear:both;height:5px;"></div>

                <div>                                  
                    <div>
                       <span style="font-weight:bolder;margin-right:8px;">Pickup/Dropoff</span>
                        <select id="ddlDropOffType" name="ddlDropOffType" style="margin-top:6px;width:300px;height:auto;">
                            <option value="4" >Drop Off At Fed Ex Station</option>
                            <option value="3">Schedule Pickup</option>
                            <option value="2" selected="selected">Regular Pickup</option>
                            <option value="1">Dropbox</option>
                        </select>
                    </div>
                   <div style="clear:both;height:5px;"></div>
                   
                        <span style="font-weight:bolder;margin-right:8px;">Select Vendor</span>
                        <select id="ddlVendors" name="ddlVendors" style="margin-top:6px;width:300px;height:auto;"></select>
                        <br />  
                    <div id="divScheduledPickup" style="width:auto;height:auto;display:none;"> 
                        <div style="float:left;font-weight:bolder;width:118px;">Servicer</div>
                        <div style="float:left;" id="divServicer"></div>
                        <div style="clear:both;height:5px;"></div>
       
                        <span style="font-weight:bolder;margin-right:8px;">Pickup DateTime</span>
                         <input type="text" class="dt-picker" id="txtScheduledDate" placeholder="mm/dd/yy" size="20" value="" style="width:100px;">
                        <select id="ddlScheduledTime" name="ddlScheduledTime" style="margin-top:2px;width:150px;height:auto;">
                          <option selected="selected" value="7:00 AM">7:00 AM</option>
                          <option value="7:30 AM">7:30 AM</option>
                           <option value="8:00 AM">8:00 AM</option>
                          <option value="8:30 AM">8:30 AM</option>
                          <option value="9:00 AM">9:00 AM</option>
                           <option value="9:30 AM">9:30 AM</option>
                          <option value="10:00 AM">10:00 AM</option>
                           <option value="10:30 AM">10:30 AM</option>
                           <option value= "11:00 AM">11:00 AM</option>
                          <option value="11:30 AM">11:30 AM</option>
                           <option value="12:00 AM">12:00 PM</option>
                        </select>                   
                        <br />
                        <div id="divReturnPickup" style="width:100%;height:auto;display:none;"> 
                               
                        <span style="font-weight:bolder;margin-right:8px;">Office Closing Time</span>                                 
                        <select id="ddlShopClosingTime" name="ddlShopClosingTime" style="margin-top:2px;width:150px;height:auto;">
                          <option selected="selected" value="7:00 AM">7:00 AM</option>
                          <option value="7:30 AM">7:30 AM</option>
                           <option value="8:00 AM">8:00 AM</option>
                          <option value="8:30 AM">8:30 AM</option>
                          <option value="9:00 AM">9:00 AM</option>
                           <option value="9:30 AM">9:30 AM</option>
                          <option value="10:00 AM">10:00 AM</option>
                           <option value="10:30 AM">10:30 AM</option>
                           <option value= "11:00 AM">11:00 AM</option>
                          <option value="11:30 AM">11:30 AM</option>
                           <option value="12:00 PM">12:00 PM</option>
                            <option value="12:30 PM">12:30 PM</option>
                           <option value="1:00 PM">1:00 PM</option>
                          <option value="1:30 PM">1:30 PM</option>
                          <option value="2:00 PM">2:00 PM</option>
                           <option value="2:30 PM">2:30 PM</option>
                          <option value="3:00 PM">3:00 PM</option>
                           <option value="3:30 PM">3:30 PM</option>
                           <option value= "4:00 PM">4:00 PM</option>
                          <option value="4:30 PM">4:30 PM</option>
                           <option value="5:00 PM" selected="selected">5:00 PM</option>
                           <option value="5:30 AM">5:30 PM</option>
                           <option value="6:00 PM">6:00 PM</option>
                            <option value="6:30 PM">6:30 PM</option>
                            <option value="7:00 PM">7:00 PM</option>
                            <option value="7:30 PM">7:30 PM</option>
                            <option value="8:00 PM">8:00 PM</option>
                            <option value="8:30 PM">8:30 PM</option>
                            <option value= "9:00 PM">9:00 PM</option>
                            <option value="9:30 PM">9:30 PM</option>
                            <option value="10:00 PM">10:00 PM</option>
                             <option value="10:30 PM">10:30 PM</option>
                            <option value="11:00 PM">11:00 PM</option>
                            <option value="11:30 PM">11:30 PM</option>
                            <option value= "12:00 AM">12:00 AM</option>
                        </select>                     
                               <br />Building Type: <select id="ddlPickupBuildingType" style="width:100px;">
                                                        <option value="0">Front</option>
                                                        <option value="2">Side</option>
                                                        <option value="3">Rear</option>
                                                    </select>
                            &nbsp;&nbsp;&nbsp;Building Part Code: <select id="ddlBuildingPartCode" style="width:100px;">
                                                        <option value="1">Building</option>
                                                        <option value="0">Apartment</option>
                                                        <option value="2">Department</option>
                                                        <option value="3">Floor<option>
                                                        <option value="4">Room</option>
                                                       <option value="5">Suite</option>
                                                    </select>
                          <br />                           
                             Special Instructions: <input type="text" id="txtSpecialInstructions" style="width:250px;" />
                        </div>
                   </div>

                    <div style="clear:both;height:5px;"></div>
                    <div>
                       <span style="font-weight:bolder;margin-right:8px;">Package Type</span>
                        <select id="ddlPackageType" name="ddlPackageType" style="margin-top:6px;width:300px;height:auto;">
                            <option value="10" selected="selected">Your Packaging</option>
                            <option value="9" >FedEx Tube</option>
                            <option value="8" >FedEx Small Box</option>
                            <option value="7" >FedEx Pak</option>
                            <option value="6" >FedEx Medium Box</option>
                            <option value="5" >FedEx Large Box</option>
                            <option value="4" >FedEx Extra Large Box</option>
                            <option value="3" >FedEx Envelope</option>
                            <option value="2" >FedEx Box</option>
                        </select>
                    </div>
                                     
                      <div style="clear:both;height:5px;"></div>                
                        <div style="font-weight:bolder;">Enter the shipment's weight in pounds.&nbsp;&nbsp;<input type="text" class="txtShipDim" id="txtWeight"  /></div>
                        <div style="clear:both;height:5px;"></div>                    
                  
                    <div id="divDimensions">
                        <div style="clear:both;height:5px;"></div>                
                        <div style="font-weight:bolder;">Enter the shipment's dimensions in inches.</div>
                        <div style="clear:both;height:5px;"></div>                    
                        <div>
      
                        <div class="divDimSection" >
                           Height: &nbsp;&nbsp;<input type="text" class="txtShipDim"  id="txtHeight" />
                    </div>
                        <div  class="divDimSection" >
                           Length: &nbsp;&nbsp;<input type="text" class="txtShipDim"  id="txtLength" />
                        </div>
                        <div  class="divDimSection" >
                           Width: &nbsp;&nbsp;<input type="text" class="txtShipDim"  id="txtWidth" />
                        </div>
                        
                    </div>
                    </div>
                     <div style="clear:both;height:10px;"></div>
                    <div id="divAdditionalReturnShippingData" style="display:none;">
                        <div style="font-weight:bolder;font-size:1em;height:30px;">Additional Shipping Information</div>
                        <div>
                            <div id="" style="width:150px;float:left;">Reference Number:</div>
                            <div style="float:left;"><input type="text" id="txtRSReferenceNumber" /></div>
                        </div>
                        <div style="clear:both;"></div>
                        <div>
                            <div id="" style="width:150px;float:left;">P.O. Number:</div>
                            <div style="float:left;"><input type="text" id="txtRSPoNo" /></div>
                        </div>
                         <div style="clear:both;"></div>
                        <div>
                            <div id="" style="width:150px;float:left;">Invoice Number:</div>
                            <div style="float:left;"><input type="text" id="txtRSInvoiceNumber" /></div>
                        </div>
                         <div style="clear:both;"></div>
                        <div>
                            <div id="" style="width:150px;float:left;">Department Number:</div>
                            <div style="float:left;"><input type="text" id="txtRSDeptNumber" /></div>
                        </div>
                         <div style="clear:both;"></div>
                    </div>
                    
                </div>

            </div>
      </div>
        
            <div class="modal-footer">
           <button id="btnCancelRates" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button id="btnGetRates" class="btn" >Get Rates</button>
        </div>
        </div>
        
            <div id="divShipmentCreationScreen" style="display:none;">
            <div class="modal-body">
                <p style="font-size:2em;">Shipping Rates</p>
                 <table>
                    <thead style="display:block;background-color:#CEF6F5;">
                        <tr>           
                            <th class="tdShipmentRateSelection">Select </th>
                            <th class="tdShipmentType" >Shipping Type</th>
                            <th class="tdShipmentTime">Transit Time / Delivery Time Stamp</th>
                            <th class="tdShipmentRate" >Cost</th>
                        </tr>    
                    </thead>
                    <tr id="trShipmentRates"></tr>
                </table>
            </div>
        
            <div class="modal-footer">
                <button id="btnBackShipmentModal" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
                <button id="btnCreateShipment" class="btn"  >Create Shipment</button>
            </div>
        </div>
        
            <div id="divShipmentResultScreen" style="display:none;">
            <div class="modal-body">
                <p style="font-size:2em;">Shipping Detail</p>
                  <table>
                    <thead style="display:block;background-color:#CEF6F5;">
                        <tr>           
                             <th class="tdShipTracNum">Track# </th>
                             <th class="tdShippingDetailLong" >Ship Type</th>
                             <th class="tdShippingDetail">Billing Weight</th>
                             <th class="tdShippingDetail" >Parts List</th>
                        </tr>    
                    </thead>
                    <tr id="trShippingDetail"></tr>
                </table>

             
            </div>
        
            <div class="modal-footer">
                <button id="btnCancelShipmentModal" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            </div>
        </div>
    
    </div>



      <div id="divPartsAvailabilityModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="hdrCreateShipment" aria-hidden="true">
         <div class="modal-header"><h3 id="hdrPartAvailability">Parts Availability</h3>
             <input type="hidden" id="hfPartsAvailabilitySectionOpened" value="0" />
         </div>  
            <div class="modal-body">
                   <div id="parts_availability_widget" style="display:block;">
                    <div id="parts_availability_loader" class="loader">
                        <img src="/images/ajax-loader-blue.gif" />
                    </div>
                    <div class="">
                        <ul id="parts_availability_list" class="nav nav-tabs" style="border-bottom-style:none;border-bottom-color:transparent;border-bottom-width:0px;">                           
                        </ul>
                    </div>
                </div>
            </div>
        
            <div class="modal-footer">
                <button id="ClosePartsAvailability" class="btn" data-dismiss="modal" >Close</button>

            </div>
        </div>


        <div id="divDeleteShipment" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="delete-shipment-header" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="delete-shipment-header">Delete Shipment</h3>
        </div>
        <div class="modal-body">
            <div id="divShipmentDeletionMsg" style="width:98%;margin-bottom:3px;font-size:1.3em;"></div>
            <div style="font-size:1.4em;text-align:center;">Enter Tracking Number Of Shipment You Wish to Delete</div>
            <div style="height:10px;clear:both;"></div>
            <div style="text-align:center;">
                <input type="text" id="txtTrackingNumber" name="txtTrackingNumber" />
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" id="btnCloseShipmentDelete" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button id="btnCancelShipment" class="btn btn-danger">Delete</button>
        </div>
    </div>
      <section id="ko_tester" style="display:none;">
                 <div class="g12 widgets">
                <div class="widget" id="ffdfdd">
                <h3 class="handle">Parts Total Summary</h3>
                <div id="ko_tester_widget" style="display:block;">
                    <select id="ddlKOParts" data-bind="event: { change: registerChange }"></select>
                    <br />
                    Vendor: <input data-bind="value: vendor" />
                    <br />
                    Type: <input data-bind="value: type" />
                    <br />
                    Group: <input data-bind="value: group" />
                    <br />
                    Description: <input data-bind="value: description" />
                    <br />
                    Brand: <input data-bind="value: brand" />
                </div>                
            </div>
        </div>
    </section>
  
</body>
</html>
