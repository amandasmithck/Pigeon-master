<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PartsPortal.ascx.vb" Inherits="Pigeon.PartsPortal2" %>

<script type="text/javascript">
     $('.tab-warranties').hide();
 </script>
<div class="container-fluid">
	<div class="row-fluid">
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                SelectCommand="SELECT MakeID, Make FROM [tblMake] WHERE oemid is not NULL union select 0 as makeid, ' Please Select' as make ORDER BY [Make]">
        </asp:SqlDataSource>
                <asp:SqlDataSource ID="dsCustomers" runat="server" 
            SelectCommand=" SELECT CustomerNo, Company FROM [tblCompany] where company is not null union select '0', ' Please Select' oRDER BY [Company]">
        </asp:SqlDataSource>

        <div id="order_grid_overlay"><img src="../images/ajax-loader.gif" alt="ajax loader" /></div>
        <div id="recent_quotes_contain">
            <div id="list">
                <span class="make">Honda</span>
            </div>
            <div style="display:none;" id="results">
                <span id="back">Back</span>
                <span id="use">Load</span>
            </div>
        </div>

        <table id="order_grid">    
            <thead>
                <tr>
                    <td class="title" colspan="3"><!--<h1>OEM Parts Search</h1>--></td>
                    <td colspan="5"></td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td class="title" colspan="6">
                        <div class="pull-left" style="margin-right:15px">
                            <label>Customer:</label>
                            <asp:DropDownList stlye="width: 300px;" ID="cboCustomer" runat="server" DataMember="DefaultView" 
                             DataSourceID="dsCustomers"    DataTextField="Company"  DataValueField="CustomerNo" >
                            </asp:DropDownList>

                        </div>
                        <div class="pull-left">
                            <label>Make:</label>
                            <asp:DropDownList ID="cbomake2" runat="server" DataMember="DefaultView" 
                                DataSourceID="SqlDataSource2"   DataTextField="Make"  DataValueField="MakeID"  cssclass="make2">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td colspan="4" style="font-size: 10px;line-height:1.2em;text-align:right;">
                        <p>Disclaimer: Stock quantity as of 12:00AM.
                        <br />No returns on electrical/electronic parts.</p>
                    </td>
                </tr>
                <tr>
                    <th style="width: 5px; text-align:center;">Part</th>
                    <th style="width:120px">Part #</th>
                    <th style="width:45px">Qty</th>
                    <th class="description">
                        Description
                    </th>
                    <th style="width:55px">
                        List Price (each)
                    </th>   
                    <th class="yourprice_col" style="width:55px">
                        Your Price (each)
                    </th>
                    <th style="width:45px">
                        Core
                    </th>
                    <th style="width:45px">
                        In Stock
                    </th>
                    <th class="large" style="width:75px">
                        <b>Total</b>
                    </th>
                    <th class="savings2" style="width:50px">
                        Savings
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td style="width: 5px; text-align:center;">1</td>
                    <td style="width:140px; text-align:right;">
                        <div><img title="Remove" alt="remove" class="remove_row" src="../images/Remove_Icon.png" />
                        <input id="PartNumber1" name="partnumber1" class="PartNumber" type="text" style="width: 100px;" /></div>
                    </td>
                    <td style="width:45px">
                        <input name="quantity" class="Quantity" type="text" style="width: 20px;" />
                    </td>
                    <td class="description"></td>
                    <td style="width:55px"></td>
                    <td class="yourprice_col" style="width:55px"></td>
                    <td style="width:45px"></td>
                    <td style="width:45px"></td>
                    <td class="total" style="width:75px"></td>
                    <td class="savings2" style="width:50px"></td>
                </tr>
                <tr>
                    <td style="width: 5px; text-align:center;">2</td>
                    <td style="width:120px; text-align:right;">
                        <div><img title="Remove" alt="remove" class="remove_row" src="../images/Remove_Icon.png" />
                        <input class="PartNumber" type="text" style="width: 100px;" /></div>
                    </td>
                    <td style="width:45px">
                        <input name="quantity" class="Quantity" type="text" style="width: 20px;" />
                    </td>
                    <td class="description">
                    </td>
                    <td style="width:55px">
                    </td>
                    <td class="yourprice_col" style="width:55px"> 
                    </td>
                    <td style="width:45px"> 
                    </td>
                    <td style="width:45px">
                    </td>
                    <td class="total" style="width:75px"> 
                    </td>
                    <td class="savings2" style="width:50px"> 
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td class="noborder"></td>
                    <td class="noborder" colspan="2">
                        <span id="GetParts" class="btn button">Get Parts</span>
                        <span id="ClearAll" class="btn button">Clear All</span>
                    </td>
                    <td class="noborder" colspan="5"></td>
                    <td class="total2"></td>
                    <td class="savings2 total2"></td>
                </tr>
            </tfoot>
        </table>
        <div id="shipping">
        </div>
        <div id="stock-level"></div>
        
        <div style="display:none;" id="guest-message">
            <p>Please call a sales representative at 800-555-1234 to set up an account in order to continue checkout.</p>
        </div>

        <div id="part-switch" title="Use superseded part?">
	        <ul id="superseded_info">
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
            </ul>
        </div>
        <div id="confirm-dialog" title="Order received.">
	        <p>Your order has been submitted</p>
        </div>
    </div>
</div>

<script type="text/javascript" >
    $(document).ready(function () {
        $("#ctl00_MainContent_AdminPartsPortal_cboCustomer").change(function () {
            $('#emulate').html($(this).val());
        });
        if (user.Client == 'AutoNation') {
            $('#ctl00_MainContent_AdminPartsPortal_cboCustomer').val(user.CustNo)
            $('#ctl00_MainContent_AdminPartsPortal_cboCustomer').prop("disabled", true)
            $('#emulate').html(user.CustNo);
        }
    });
</script>
