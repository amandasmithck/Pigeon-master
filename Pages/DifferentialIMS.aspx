<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="DifferentialIMS.aspx.vb" Inherits="Pigeon.DifferentialIMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Styles/ims.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../Styles/forms.css" type="text/css" media="screen, print">
    
    <script type="text/javascript" src="../Scripts/ig/ig.ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/ims.js"></script>
    
    <link type="text/css" href="../Styles/collapse.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery.collapsible.js"></script>

    <script type="text/javascript" src="../Scripts/ig/ig.ui.min.js"></script>
    <link type="text/css" href="../Styles/ig/jquery.ui.all.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/ig/ig.ui.editors.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/ig/jquery.ui.custom.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/ig/ig.ui.grid_VD.css" rel="stylesheet" />
    <link type="text/css" href="../Styles/ig/ig.ui.shared.css" rel="stylesheet" />

    <script type="text/javascript">
        function GetStock() {
            var urlMethod = "../IMSWebService.asmx/GetStock";

            var json = { 'client': user.Client, 'type': 3 };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, ReturnGetStock);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         $('.tab-warranties').hide();
 </script>
    <div class="main-content  container-fluid">
        <div class="row-fluid">
            <div class="span12"><h3>Transfer Case & Differential IMS</h3></div>
        </div>
        <div class="row-fluid">
            <div class="span2 toolbar">
                <div class="btn btn-primary btn-block" title="IMS">
                    IMS</div>
                <div class="btn btn-block" title="inservice">
                    Add In-Service</div>
                <div class="btn btn-block" title="printwarranty">
                    Print Warranty Paperwork</div>
                <div class="btn btn-block" title="initiatewarranty">
                    Initiate Warranty Claim</div>
            </div>
            <div class="span10 screens last">
                <div class="screen" id="ims">
                    <div id="expected-arrivals-container" class="subscreen ui-expandable ui-widget">
                        <div class="subscreen-header collapsed ui-state-default">
                            <!-- this will be collapsed by default (unless the cookie says otherwise) -->
                            Expected Arrivals
                        </div>
                        <div class="content">
                            <p class="description">
                                This grid shows parts that you have ordered but yet to receive. Select the serial
                                numbers you have verified as received to place them in inventory.</p>
                            <%-- <div class="btn arrivalsbtn" style="float:left;margin: 5px" id="arrivalselect">Select All</div>
                        <div class="btn arrivalsbtn" style="float:left;margin: 5px" id="arrivaldeselect">Deselect All</div>--%>
                            <a href="PrintArrivals.aspx?type=1" target="_blank" class="btn arrivalsbtn"
                                style="float: left; margin: 5px" id="print">Print List</a>
                            <div class="btn" style="margin: 5px; float: right;" id="place-inventory-btn">
                                Place in Inventory</div>
                            <div class="btn" style="margin: 5px; float: right" id="mark-damaged-btn">
                                Mark as Damaged</div>
                            <div class="clear">
                            </div>
                            <table id="arrivals-grid">
                            </table>
                        </div>
                    </div>
                    <div id="inventory-container" class="subscreen ui-expandable ui-widget">
                        <div class="subscreen-header collapsed ui-state-default">
                            <!-- this will be collapsed by default (unless the cookie says otherwise) -->
                            Inventory
                        </div>
                        <div class="content">
                            <p class="description">
                                This grid shows parts that you currently have in inventory. Remember to select the
                                appropriate return type when marking a part sold.</p>
                            <table id="inventory-grid">
                            </table>
                        </div>
                    </div>
                    <div id="field-returns-container" class="subscreen ui-expandable ui-widget">
                        <div class="subscreen-header collapsed ui-state-default">
                            <!-- this will be collapsed by default (unless the cookie says otherwise) -->
                            Field Returns
                        </div>
                        <div class="content">
                            <p class="description">
                                This grid shows parts that have been sent out and the type of return expected.</p>
                            <table id="shop-return-grid">
                            </table>
                        </div>
                    </div>
                    <div id="vendor-returns-container" class="subscreen ui-expandable ui-widget">
                        <div class="subscreen-header collapsed ui-state-default">
                            <!-- this will be collapsed by default (unless the cookie says otherwise) -->
                            Awaiting Vendor Return
                        </div>
                        <div class="content">
                            <div class="left" id="return-status">
                                <p>
                                    <span id="count">0</span> returns ready</p>
                                <p class="sub-description">
                                    <span id="remainder">20</span> more before shipping scheduled</p>
                            </div>
                            <div class="left" id="return-btn">
                                Process Return</div>
                            <div class="clear">
                            </div>
                            <table id="vendor-return-grid">
                            </table>
                        </div>
                    </div>
                </div>
                <div class="screen">
                    <h1 class="form-title">
                        Add In-Service</h1>
                    <form id="inservice-form">
                    <table class="normal bgGray">
                        <thead>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="footer" colspan="2">
                                </td>
                            </tr>
                        </tfoot>
                        <tbody class="colorGrayDark">
                            <tr>
                                <td width="25%">
                                    SN #
                                </td>
                                <td width="75%">
                                    <input type="text" id="service-sn" size="40" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    PO #
                                </td>
                                <td width="75%">
                                    <input type="text" id="service-po" size="40" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    VIN #
                                </td>
                                <td width="75%">
                                    <input type="text" id="service-vin" size="40" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Mileage
                                </td>
                                <td>
                                    <input type="text" id="service-mileage" size="10" value="" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sale Date
                                </td>
                                <td>
                                    <input type="text" id="service-solddate" value="" size="11">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table class="bare">
                        <tbody>
                            <tr>
                                <td>
                                    <input id="inservice-submit"  name="Submit" type="submit" class="btnNormal" value="Submit">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </form>
                </div>
                <div class="screen">
                    <h1 class="form-title">
                        Print Warranty Paperwork</h1>
                    <form id="printwarranty-form">
                    <table class="normal bgGray">
                        <tr>
                            <%--     <td> <a href="12 Month Transmission Warranty.pdf" target="_blank"><img src="../images/1year.png" /></a></td>
                <td><a href="24 Month Transmission Warranty.pdf" target="_blank"><img src="../images/2year.png" /></a></td>--%>
                            <td>
                                <a href="Assets/GO/Docs/36%20Month%20Warranty.pdf" target="_blank">
                                    <img src="../images/3year.png" /></a>
                            </td>
                        </tr>
                        <tr>
                            <%--  <td>12 months/12,000 miles</td>
                <td>24 months/24,000 miles</td>--%>
                            <td>
                                36 months/100,000 miles
                            </td>
                        </tr>
                    </table>
                    <br />
                    </form>
                </div>
                <div class="screen">
                    <h1 class="form-title">
                        Initiate Warranty Claim</h1>
                    <form id="initiatewarranty-form">
                    <table class="normal bgGray">
                        <thead>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="footer" colspan="2">
                                </td>
                            </tr>
                        </tfoot>
                        <tbody class="colorGrayDark">
                            <tr>
                                <td width="25%">
                                    Your claim #
                                </td>
                                <td width="75%" class="colorGrayDark">
                                    <input type="text" name="ClaimNo" size="25" value="" onkeyup="this.value=this.value.substring(0,50);">&nbsp;&nbsp;<i>optional</i>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Repairing shop
                                </td>
                                <td>
                                    <input type="text" name="Shop" size="45" value="" class="marginRight" onkeyup="this.value=this.value.substring(0,60);">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address
                                </td>
                                <td>
                                    <input type="text" name="Address" size="45" value="" onkeyup="this.value=this.value.substring(0,50);">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City, state zip
                                </td>
                                <td>
                                    <input type="text" name="City" size="25" value="" onkeyup="this.value=this.value.substring(0,28);">,&nbsp;&nbsp;<input
                                        type="text" name="State" size="3" value="" onkeyup="this.value=this.value.substring(0,2).toUpperCase();">&nbsp;&nbsp;&nbsp;&nbsp;<input
                                            type="text" name="Zip" size="9" value="" onkeyup="this.value=this.value.substring(0,20);">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Phone
                                </td>
                                <td>
                                    <input type="text" name="Phone" size="25" value="" onkeyup="this.value=this.value.substring(0,20);">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact person
                                </td>
                                <td>
                                    <input type="text" name="Contact" size="25" value="" onkeyup="this.value=this.value.substring(0,30);">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br>
                    <table class="normal bgGray">
                        <thead>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="footer" colspan="2">
                                </td>
                            </tr>
                        </tfoot>
                        <tbody class="colorGrayDark">
                            <tr>
                                <td width="25%">
                                    VIN #
                                </td>
                                <td width="75%">
                                    <input type="text" name="VIN" size="40" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CTR serial #
                                </td>
                                <td>
                                    <input type="text" name="SerialNo" value="" onkeyup="this.value=this.value.substring(0,10).toUpperCase();">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br>
                    <table class="normal bgGray">
                        <thead>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <td class="spacer" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="footer" colspan="2">
                                </td>
                            </tr>
                        </tfoot>
                        <tbody class="colorGrayDark">
                            <tr>
                                <td width="25%">
                                    Miles at installation
                                </td>
                                <td width="75%">
                                    <input type="text" name="MilesInstall" size="10" value="" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Miles now
                                </td>
                                <td>
                                    <input type="text" name="MilesNow" size="10" value="" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date of installation
                                </td>
                                <td>
                                    <input type="text" id="DateInstall" name="DateInstall" value="" size="11" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Engine condition
                                </td>
                                <td>
                                    <select name="EngineHealth">
                                        <option value="N/A">-- Select --</option>
                                        <option value="Well">Runs well</option>
                                        <option value="Fair">Runs okay</option>
                                        <option value="Poor">Runs poorly</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fluid level
                                </td>
                                <td>
                                    <select name="FluidLevel">
                                        <option value="N/A">-- Select --</option>
                                        <option value="Low">Low</option>
                                        <option value="Full">Full</option>
                                        <option value="Overfull">Overfull</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fluid condition
                                </td>
                                <td>
                                    <select name="FluidCondition">
                                        <option value="N/A">-- Select --</option>
                                        <option value="Clean">Clean</option>
                                        <option value="Slightly discolored / slight odor">Slightly discolored / slight odor</option>
                                        <option value="Burnt">Burnt</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Leaks?
                                </td>
                                <td>
                                    <input name="Leaks" type="text" value="" size="50">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Noises?
                                </td>
                                <td>
                                    <input name="Noises" type="text" value="" size="50" onkeyup="this.value=this.value.substring(0,200);">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Codes?
                                </td>
                                <td>
                                    <input name="Codes" type="text" value="" size="50" onkeyup="this.value=this.value.substring(0,200);">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Warning lights?
                                </td>
                                <td>
                                    <input name="WarningLights" type="text" value="" size="50" onkeyup="this.value=this.value.substring(0,200);">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Complaint
                                </td>
                                <td>
                                    <textarea name="Complaint" cols="50" rows="5"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Is the complaint hot or cold?
                                </td>
                                <td>
                                    <select name="ComplaintTemp">
                                        <option value="Both">-- Select --</option>
                                        <option value="Hot">Hot</option>
                                        <option value="Cold">Cold</option>
                                        <option value="Both">Both</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td height="50">
                                    If complaint is hot, how many miles does the vehicle drive before the problem occurs?
                                </td>
                                <td>
                                    <input type="text" name="ComplaintMiles" value="" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Comments
                                </td>
                                <td>
                                    <textarea name="Comments" cols="50" rows="5"></textarea>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br>
                    <table class="bare">
                        <tbody>
                            <tr>
                                <td>
                                    <input name="Submit" type="submit" class="btnNormal" value="Submit">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="dialog-message" title="Confirmation">
        <p class="message">
        </p>
    </div>
    <div id="change-bin-diag" title="Change Bin Location">
        <div class="small-form">
            <span style="display: none;" id="bin-change-sn"></span>
            <label id="current-bin">
                Current Bin:</label>
            <br />
            <br />
            <label>
                New Bin</label>
            <select style="margin-left: 10px; width: 100px;" id="new-bin">
                <%--        <option value=''></option>
            <option value='601'>601</option>
            <option value='602'>602</option>
            <option value='603'>603</option>
            <option value='604'>604</option>--%>
            </select>
        </div>
    </div>
    <div id="edit-info-diag" title="Update sale info">
        <span style="display: none;" id="edit-sn"></span>
        <p>
            <label for="edit-vin" style="width: 60px; display: block; float: left">
                VIN:</label><input id="edit-vin" type="text" /></p>
        <p>
            <label for="edit-mileage" style="width: 60px; display: block; float: left">
                Mileage:</label><input id="edit-mileage" type="text" /></p>
        <p>
            <label for="edit-sold" style="width: 60px; display: block; float: left">
                Sold Date:</label><input id="edit-sold" type="text" /></p>
    </div>
</asp:Content>
