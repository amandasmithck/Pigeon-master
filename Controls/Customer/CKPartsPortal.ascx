<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CKPartsPortal.ascx.vb" Inherits="Pigeon.CKPartsPortal2" %>
     <script type="text/javascript">
         $('.tab-warranties').hide();
 </script>
    <script id="oemResultsTemplate" type="text/x-jquery-tmpl">
        <div class="total-container">
            <p class="type">Each</p>
            <h1 class="price">${Your}</h1>
            <p style="font-weight: bold;">
                {{if Stock == "Yes"}} 
                In Stock
                {{else Stock == "Extra Day"}} 
                Extra Day
                {{else Stock == "No"}} 
                Out of Stock
                {{else Stock == "Call for ETA"}} 
                Chat for ETA 
                {{else}} 
                ${Stock}
                 {{/if}}
            </p>
            {{if !URL}}
           <%--     <p class="stock-note">Stock as of now.</p>--%>
            {{else}}
                <p class="stock-note">Stock as of midnight.</p>
            {{/if}}
        </div>
        
        <table class="result">
            <thead>
                <tr><th>Total</th><th>Core</th><th>Savings</th></tr>
            </thead>
            <tbody>
                <tr><td class="total">${Total}</td><td class="core">${Core}</td><td class="savings">${Savings}</td></tr>
            </tbody>
        </table>

        {{if Stock != "No" }}<span class="btn-addtocart btn">Add</span>{{/if}}

        <p class="description">
            ${Description}
            {{if SupersededPart}} (Superseded Part: <span class='superseded_part'>${SupersededPart}</span>) {{/if}}
         </p>
        <!--<label class="stock-message">In Stock</label>-->
    </script>
<script id="aftermarketResultsTemplate" type="text/x-jquery-tmpl">
        <div class="total-container">
            <p class="type">Each</p>
            <h1 class="price">${Your}</h1>
            <p style="font-weight: bold;">
                {{if Stock == "Yes"}} 
                In Stock
                {{else Stock == "Extra Day"}} 
                Extra Day
                {{else Stock == "No"}} 
                Out of Stock
                {{else Stock == "Call for ETA"}} 
                Chat for ETA 
                {{else}} 
                ${Stock}
                 {{/if}}
            </p>
            {{if !URL}}
           <%--     <p class="stock-note">Stock as of now.</p>--%>
            {{else}}
                <p class="stock-note">Stock as of midnight.</p>
            {{/if}}
        </div>
        
        <table class="result">
            <thead>
                <tr><th>Total</th><th>Core</th><th>Savings</th></tr>
            </thead>
            <tbody>
                <tr><td class="total">${Total}</td><td class="core">${Core}</td><td class="savings">${Savings}</td></tr>
            </tbody>
        </table>

        {{if Stock != "No" }}<span class="btn-addtocart btn">Add</span>{{/if}}

        <p class="description">
            ${Description}
            {{if SupersededPart}} (Superseded Part: <span class='superseded_part'>${SupersededPart}</span>) {{/if}}
         </p>
        <!--<label class="stock-message">In Stock</label>-->
    </script>
        <div style="margin-bottom: 4em;" class="container-fluid">
	          
            <div class="row-fluid">
                <div class="title-contain" id="select-make-contain">
                    <h3 class="help-inline">Select make:</h3>

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CKConnectionString %>" 
                        SelectCommand="SELECT * FROM [tblMake] where makeid <> 33  ORDER BY [Make]">
                    </asp:SqlDataSource>
                    <asp:DropDownList TabIndex="1" style="margin: 1em 0;" ID="cbomake2" CssClass="cbomake" runat="server" DataMember="DefaultView" 
                        DataSourceID="SqlDataSource2" DataTextField="Make"  DataValueField="MakeID">
                    </asp:DropDownList>

                    <div class="clear"></div>
                </div>
            </div>

            <div class="row-fluid results-heading">
                <p style="margin-left:33%;color:steelBlue;">OEM </p>  
                
                    
                <p style="color:#F80;">Aftermarket </p>
                    
            </div>
            <div class="row-fluid search-row" quoteid="">
                <table class="search-table">
                    <thead>
                        <tr>
                            <th width="25%"></th>
                            <th width="45%">Part #</th>
                            <th width="30%">Quantity</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td width="25%" class="td-remove"></td>
                            <td width="45%"><input tabindex="2" class="PartNumber" style="width: 100px; margin: 0 auto; display: block;" type="text" /></td>
                            <td width="30%"><input tabindex="3" class="Quantity" style="width: 40px; margin: 0 auto; display: block;" type="text" /></td>
                        </tr>
                    </tbody>
                </table>

                <div class="list-contain">
                    <h6>List Price</h6>
                    <span class="list">-</span>
                </div>

                <div class="results">
                    <div class="result-contain oem"></div>
                    <div class="result-contain aftermarket"></div>
                </div>
            </div>
             <div id="bottom-info" class="row-fluid"> 
                    <div id="bottom-info-oem" style="margin-right:30px;"> 
                        Total Aftermarket Savings: <span class="total-savings" id="tsafter"></span> 
                    </div> 
                    <div id="bottom-info-after" > 
                        Total OEM Savings: <span class="total-savings" id="tsoem"></span> 
                    </div> 
                </div> 
            <div class="row-fluid search-row" quoteid="">
                <table class="search-table">
                    <thead>
                        <tr>
                            <th width="25%"></th>
                            <th width="45%">Part #</th>
                            <th width="30%">Quantity</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td width="25%" class="td-remove"><img title="Remove" alt="remove" class="remove_row" src="../images/Remove_Icon.png" /></td>
                            <td width="45%"><input tabindex="4" class="PartNumber" style="width: 100px; margin: 0 auto; display: block;" type="text" /></td>
                            <td width="30%"><input tabindex="5" class="Quantity" style="width: 40px; margin: 0 auto; display: block;" type="text" /></td>
                        </tr>
                    </tbody>
                </table>

                <div class="list-contain">
                    <h6>List Price</h6>
                    <span class="list">-</span>
                </div>

                <div class="results">
                    <div class="result-contain oem"></div>
                    <div class="result-contain aftermarket"></div>
                </div>
            </div>
           
            <div class="row-fluid search-footer">
           
            <hr />
                
                <span class="btn btn-large btn-primary" style="margin:0 10px 0 20px" id="btn-search">Search</span><span class="btn btn-large btn-clear">Clear Results</span>
                
            </div>

            <div id="order-completion" class="container-fluid">
	            <div class="row-fluid">
                    <div class="span4 shopping-cart">
                        <h1>Shopping Cart</h1>

                        <!--<label id="label-shiplocation"></label>-->

                        <p id="empty-message">Cart is empty</p>
                        
                        <table class="table table-bordered part-summary" style="width:100%">
                            <thead><tr>
                                <th style="width: 35px;"></th>
                                <th>Type</th>
                                <th>Part #</th>
                                <th>Qty</th>
                                <th>Each</th>
                            </tr></thead>
                            <tbody>
                                
                            </tbody>
                        </table>

                        <ul class="total-summary">
                            <li>
                                <label>Parts Total:</label><p id="partstotal"></p>
                            </li>
                            <li>
                                <label><img id="img-warranty-info" src="../images/info2.png" alt="Info" />Warranty</label>
                                <p>
                                    <!--<select style="margin: -4px 0;width:125px; " id="warrantytype">
                                        <option value="Manufacturer">Base - $0</option>
                                        <option value="EOC">EOC - $15</option>
                                    </select>-->

                                    <label class="radio"><input type="radio" name="warrantytype" value="Manufacturer" /><span>Base - $0</span></label>
                                    <label class="radio"><input type="radio" name="warrantytype" value="EOC"/><span>EOC - $15</span></label>
                                </p>
                            </li>
                            <li>
                                <label>Tax</label><p id="tax"></p>
                            </li>
                        </ul>
                        <ul id="shipping-container" class="total-summary estimated">
                            <!--<li>
                                <label id="label-shipping">Est Shipping</label><p id="shipping"></p>
                            </li>
                            <li id="li-actualzip">
                                <label>Zip Code</label>
                                <p>
                                    <input style="margin: -14px 0;width: 95px;" type="text" id="text-actualzip" />
                                </p>
                            </li>-->
                            <li id="li-oemtype">
                                <label><img id="img-oem-shipping-info" src="../images/info2.png" alt="Info" />OEM Shipping</label>
                                <p>
                                    <label class="radio regular-oem-shipping"><input type="radio" name="select-oemtype" value="regular" />Regular - $xx.xx</label>
                                    <label class="radio ground-oem-shipping"><input type="radio" name="select-oemtype" value="ground" />Ground - $xx.xx</label>
                                    <label class="radio"><input type="radio" name="select-oemtype" value="heavy" />Heavy - $75</label>
                                    <label class="radio"><input type="radio" name="select-oemtype" value="freight"/>Freight - $125</label>
                                </p>
                            </li>
                            <li id="li-coreshipping">
                                <label>OEM Core Shipping:</label><p></p>
                            </li>
                            <li id="li-aftertype">
                                <label><img id="img-aftermarket-shipping-info" src="../images/info2.png" alt="Info" />Aftermarket Shipping</label>
                                <p>
                                    <label class="radio regular-smallparts-shipping"><input type="radio" name="select-aftertype" value="regular" />Regular - $xx.xx</label>
                                    <label class="radio ground-smallparts-shipping"><input type="radio" name="select-aftertype" value="ground" />Ground - $xx.xx</label>
                                    <label class="radio"><input type="radio" name="select-aftertype" value="heavy" />Heavy - $75</label>
                                    <label class="radio"><input type="radio" name="select-aftertype" value="freight"/>Freight - $125</label>
                                </p>
                            </li>
                            <!--<li style="margin-bottom: 10px;">
                                <span style="float: right;" id="btn-calcship">Calculate Shipping</span>
                            </li>-->
                        </ul>
                        <ul class="total-summary">
                            <li>
                                <label>Grand Total</label><p id="grandtotal"></p>
                            </li>
                            <li class="li-savings">
                                <label class="li-savings">Total Savings</label><p class="li-savings" id="grandsaving"></p>
                            </li>
                        </ul>

                        <div id="shipping-message"></div>
                        
                        <div style="display:none;" id="cutoff">
                            <p id="cuttoff_time"></p>
                            <p>Time left to ship today:</p>
                            <p style="width: 200px;overflow: hidden;" id="countdown"></p>
                        </div>
                    </div>
                    <div style="display:none;" id="checkout" class="span8 last">
                        <h1>Place Order</h1>
                        <div id="checkout-form" class="form_contain">
                        <ul style="display:none;" id="error_contain"></ul>

                        <label>Year<span class="small">2-digit Year</span></label>
                        <input class="required" type="text" name="year" id="year" />
                        <label>Make<span class="small">Vehicle Make</span></label>
                        <input disabled="disabled" type="text" name="make" id="make" />
                        <label>Model<span class="small">Vehicle Model</span></label>
                        <input type="text" name="model" id="model" />
            
                        <label>VIN<span class="small"></span></label>
                        <input type="text" name="vin" id="vin" />
                        <label>Mileage<span class="small">Vehicle Mileage</span></label>
                        <input type="text" name="mileage" id="mileage" />
           
                        <label>Trans<span class="small">Tranmission Type</span></label>
                        <select id="trans">
                            <option value="Automatic">Automatic</option>
                            <option value="Manual">Manual</option>
                        </select>

                        <label>Contract No<span class="small"></span></label>
                        <input type="text" name="contractno" id="contractno" />
                        <label>Auth No<span class="small"></span></label>
                        <input type="text" name="authno" id="authno" />
            
                        <label>Owner<span class="small">Vehicle Owner</span></label>
                        <input type="text" name="owner" id="owner" />
                        <label>Shop<span class="small">Shop Name</span></label>
                        <input type="text" name="shop" id="shop" />
                        <label>Address<span class="small">Shop Address</span></label>
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
                        <label>Zip<span class="small">Shop Zip</span></label>
                        <input type="text" name="zip" id="zip" />
                        
            
                        <label>Contact<span class="small">Shop Contact</span></label>
                        <input type="text" name="contact" id="contact" />
                        <label>Phone<span class="small">Shop Phone</span></label>
                        <input type="text" name="phone" id="phone" />
                        <label>Notes<span class="small" >Enter any notes regarding this order</span></label>
                        <textarea id="notes"></textarea>

                        <div style="clear:left;display:none;" id="warranty_inputs">
                            <p class="validateTips">EOC Warranty Information:</p>
                            <label>Date<span class="small">Contract End Date</span></label>
                            <input type="text" name="warrantydate" id="warrantydate" />
                            <label>Mileage<span class="small">Contract End Mileage</span></label>
                            <input type="text" name="warrantymileage" id="warrantymileage" />
                        </div>

                        <input style="display:none;" type="text" name="warranty" id="warranty" />
                        <input style="display:none;" type="text" name="warrantycost" id="warrantycost" />
                        <input style="display:none;" type="text" name="shippingcost" id="shippingcost" />
                        <input style="display:none;" type="text" name="returnshippingcost" id="returnshippingcost" />
                        <input style="display:none;" type="text" name="shippingtype" id="shippingtype" />

                        <div class="clear"></div>
                        <span class="btn btn-primary btn-large" id="btn-placeorder" style="margin-right: 80px;float:right;">Place Order</span>
                       <%-- <input  id="btn-placeorder" class="submit" type="submit" value="Place Order" style="float: right;margin-right: 80px;"/>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid footer">
                <div class="row-fluid">

                </div>
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
        <div id="diag-ecm" title="ECM Notification">
	        <span style="display:none;" class="row-fluid"></span>
            <p>
                <b>Note:</b>
                <span class="ecm-description"></span> 
            </p>

            <br />

            <p>This programming service can be performed by the remanufacturer for a cost of <b><span class="cost"></span></b>. If you do not purchase the pre-programming option, the part will need to be programmed by the repair facility.</p>
        </div>
        <div id="diag-ecm2" title="ECM Notification">
	        <span style="display:none;" class="row-fluid"></span>
            <p class="ecm-description"></p>
        </div>
        <div id="diag-order-confirm" title="Order Confirmation">
            <p id="order-confirm"></p>
        </div>
        <div id="diag-select-make" title="Whoops...">
            <h2>Please select a make</h2>
            
            <asp:DropDownList style="margin: 1em 0;float:left;" ID="cbomake3" runat="server" DataMember="DefaultView" 
                DataSourceID="SqlDataSource2" DataTextField="Make"  DataValueField="MakeID">
            </asp:DropDownList>
        </div>
        <div id="warranty-info">
            <h4>OEM</h4> 
            <p>Base warranty is the manufacturer's warranty.</p>
            <h4>Aftermarket</h4>
            <p>Base warranty is 12/12 parts and labor. EOC is a no-cost option.</p>
        </div>
        <div id="shipping-info">
            <h4>Normal</h4> 
            <p>Overnight Ssmall parts such as A/C Compressors, Water Pumps, Electronic parts, Steering Pumps, and Starters.</p>
            <h4>Ground </h4> 
            <p>1-3 days Ground small parts such as A/C Compressors, Water Pumps, Electronic parts, Steering Pumps, and Starters.</p>
            <h4>Heavy</h4>
            <p>Bigger parts such as CV Axles, Rack and Pinions, Window Regulators, Driveshafts, Seat Frames, and multiple part orders. Please also use this option for shipping ANY part to Hawaii or Alaska.</p>
            <h4>Freight</h4>
            <p>Transmissions, Engines, and Fuel Tanks.</p>
        </div>
