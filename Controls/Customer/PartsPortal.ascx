<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PartsPortal.ascx.vb" Inherits="Pigeon.PartsPortal4" %>
 <script type="text/javascript">
     $('.tab-warranties').hide();
 </script>
<div class="container-fluid">
    <asp:Label runat="server" ID="autonation" class="autonation" ForeColor="White" ></asp:Label>
	<div class="row-fluid">
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                 
                    SelectCommand="SELECT MakeID, Make FROM [tblMake] WHERE oemid is not NULL union select 0 as makeid, ' Please Select' as make ORDER BY [Make]">
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
                    <td class="title" colspan="3"></td>
                    <td colspan="7">
                    </td>
                </tr>
                <tr>
                    <td width="250px;" class="title" colspan="3">
                            <asp:DropDownList TabIndex="1" ID="cbomake2" runat="server" DataMember="DefaultView"
                                DataSourceID="SqlDataSource2" DataTextField="Make"  DataValueField="MakeID" cssclass="make2" >
                            </asp:DropDownList>
                    </td>
                    <td colspan="7" style="font-size: 10px;line-height:1.2em;text-align:right;">
                        <div class="promotion">
                            <!--<p style="width:100%;text-align:left;padding-bottom:5px;" class="hook">Part number resources:</p>-->
                            <a target="_blank" class="img-link" href="http://www.moreoemparts.com/">
                                <img style="padding: 5px;" alt="Find Part Numbers" src="../images/more_logo.png" />
                            </a>
                        </div>
                            
                        <p>Disclaimer: Stock quantity as of 12:00AM.
                            <br />No returns on electrical/electronic parts.
                            <br />All information, prices and availability subject to change without notice.
                            <br />*Certain engines, airbags and other large assemblies may have non-standard discounts.
                        </p>
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
                        <input tabindex="2" id="PartNumber1" name="partnumber1" class="PartNumber" type="text" style="width: 100px;" /></div>
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
                        <input tabindex="3" class="PartNumber" type="text" style="width: 100px;" /></div>
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
                    <td class="total2" style="width:75px"></td>
                    <td class="savings2 total2"></td>
                </tr>
            </tfoot>
        </table>
    </div>
    <div class="row-fluid">
        <div style="display:none;" id="guest-message">
            <p>Please call a sales representative at 1-800-888-2292 ext 289 to set up an account in order to continue checkout.</p>
        </div>
        <div id="stock-level">
        </div>
        
        <div id="checkout">
            <h1>Checkout</h1>
            <div class="span6 checkout-info">
                <div id="pickup-delivery" class="row-fluid">
                    <div class="form-inline" id="checkout-radios">
                        <label class="radio">Delivery<input type="radio" name="checkout-type" value="delivery" checked="checked"></label>
                        <label class="radio">Pickup<input type="radio" name="checkout-type" value="pickup"></label>
                    </div>
                </div>
                <div class="row-fluid">
                    <div id="pickup" style="display:none;">
                        <div id="pickup-cutoff">
                            <p id="pickup-cutoff-time"></p>
                            <%--<p style="float: left;margin-right: 5px;">Time left to order and pickup today:</p>--%>
                            <p style="width: 200px;overflow: hidden;" id="pickup-countdown"></p>
                        </div>        
                    </div>
                    <div id="delivery">
                        <div id="delivery-cutoff">
                            <p id="delivery-cutoff-time"></p>
                            <%--<p style="float: left;margin-right: 5px;">Time left until next truck leaves:</p>--%>
                            <p style="width: 200px;overflow: hidden;" id="delivery-countdown"></p>
                        </div> 
                    </div>
                </div>
                <div class="row-fluid">
                    <ul style="display:none;" id="error_contain"></ul>
                    
                    <div class="span5">
                        <label>PO</label>
                        <input type="text" name="po" id="po" />
                        <label>Shop</label>
                        <input type="text" name="shop" id="shop" />
                        <label>Address</label>
                        <input type="text" name="address" id="address" />
                        <label>City<span class="small">Shop City</span></label>
                        <input type="text" name="city" id="city" />
                        <label>State<span class="small">Shop State</span></label>
                        <select id="state" name="state" size="1">
                            <option value="AL">Alabama</option>
                            <option value="AK">Alaska</option>
                            <option value="AB">Alberta</option>
                            <option value="AZ">Arizona</option>
                            <option value="AR">Arkansas</option>
                            <option value="BC">British Columbia</option>
                            <option value="CA">California</option>
                            <option value="CO">Colorado</option>
                            <option value="CT">Connecticut</option>
                            <option value="DE">Delaware</option>
                            <option value="DC">District of Columbia</option>
                            <option value="FL">Florida</option>
                            <option value="GA">Georgia</option>
                            <option value="HI">Hawaii</option>
                            <option value="ID">Idaho</option>
                            <option value="IL">Illinois</option>
                            <option value="IN">Indiana</option>
                            <option value="IA">Iowa</option>
                            <option value="KS">Kansas</option>
                            <option value="KY">Kentucky</option>
                            <option value="LA">Louisiana</option>
                            <option value="ME">Maine</option>
                            <option value="MB">Manitoba</option>
                            <option value="MD">Maryland</option>
                            <option value="MA">Massachusetts</option>
                            <option value="MI">Michigan</option>
                            <option value="MN">Minnesota</option>
                            <option value="MS">Mississippi</option>
                            <option value="MO">Missouri</option>
                            <option value="MT">Montana</option>
                            <option value="NE">Nebraska</option>
                            <option value="NV">Nevada</option>
                            <option value="NB">New Brunswick</option>
                            <option value="NH">New Hampshire</option>
                            <option value="NJ">New Jersey</option>
                            <option value="NM">New Mexico</option>
                            <option value="NY">New York</option>
                            <option value="NL">Newfoundland and Labrador</option>
                            <option value="NC">North Carolina</option>
                            <option value="ND">North Dakota</option>
                            <option value="NT">Northwest Territories</option>
                            <option value="NS">Nova Scotia</option>
                            <option value="NU">Nunavut</option>
                            <option value="OH">Ohio</option>
                            <option value="OK">Oklahoma</option>
                            <option value="ON">Ontario</option>
                            <option value="OR">Oregon</option>
                            <option value="PA">Pennsylvania</option>
                            <option value="PE">Prince Edward Island</option>
                            <option value="PR">Puerto Rico</option>
                            <option value="QC">Quebec</option>
                            <option value="RI">Rhode Island</option>
                            <option value="SK">Saskatchewan</option>
                            <option value="SC">South Carolina</option>
                            <option value="SD">South Dakota</option>
                            <option value="TN">Tennessee</option>
                            <option value="TX">Texas</option>
                            <option value="UT">Utah</option>
                            <option value="VT">Vermont</option>
                            <option value="VA">Virginia</option>
                            <option value="WA">Washington</option>
                            <option value="WV">West Virginia</option>
                            <option value="WI">Wisconsin</option>
                            <option value="WY">Wyoming</option>
                            <option value="YT">Yukon</option>
                            </select>
                    </div>
                    <div class="span5 offset1">
                        <label>Zip</label>
                        <input type="text" name="zip" id="zip" />
            
                        <label>Contact</label>
                        <input type="text" name="contact" id="contact" />
                        <label>Phone</label>
                        <input type="text" name="phone" id="phone" />

                        <label>VIN</label>
                        <input type="text" name="vin2" id="vin2" />
                        

                        <div style="clear:left;" id="warranty_inputs">
                            <p class="validateTips">EOC Warranty Information:</p>
                            <label>Date</label>
                            <input type="text" name="warrantydate" id="warrantydate" />
                            <label>Mileage</label>
                            <input type="text" name="warrantymileage" id="warrantymileage" />
                        </div>

                        <input style="display:none;" type="text" name="warranty" id="warranty" />
                        <input style="display:none;" type="text" name="warrantycost" id="warrantycost" />
                        <input style="display:none;" type="text" name="shippingcost" id="shippingcost" />
                        <input style="display:none;" type="text" name="returnshippingcost" id="returnshippingcost" />
                        <input style="display:none;" type="text" name="shippingtype" id="shippingtype" />
                    </div>
                </div>
            </div>
            <div class="span6" id="order-summary">
                <div id="part-summary">
                    <table>
                        <thead>
                            <tr><th></th><th>Part #</th><th>Qty</th><th>Description</th><th>Total Price</th></tr>
                        </thead>
                        <tbody>
                                       
                        </tbody>
                        <tfoot>
                            <tr><td colspan="5">&nbsp;</td></tr>
                            <tr><td colspan="3"></td><td>Subtotal</td><td></td></tr>
                            <%--<tr><td colspan="3"></td><td>Tax</td><td></td></tr>--%>
                            <%--<tr><td colspan="3"></td><td>Shipping</td><td></td></tr>--%>
                            <tr class="total"><td colspan="3"></td><td>Total</td><td></td></tr>
                        </tfoot>
                    </table>
                </div>
            
                <button style="float: right;" class="btn-primary btn btn-large button" id="submit-order">Submit Order</button>
                <!--<ul style="float:right;" class="shipping">
                        <li>Subtotal: $</li>
                        <li class="shipping_cost">Shipping: $</li>
                        <li class="coreshipping">Core Shipping: $</li>
                        <li class="grandtotal">Grand Total: $</li> 
                </ul>-->
            </div>
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
	    <%--<td colspan="2">
		    <img src="../images/customers_04.png" width="899" height="65" alt=""></td>
	    </tr>--%>
    </div>
</div>