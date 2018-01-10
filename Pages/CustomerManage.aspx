<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="CustomerManage.aspx.vb" Inherits="Pigeon.CustomerManage1" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/customermanage.js"></script>
    <script type="text/javascript" src="../Scripts/chosen.jquery.min.js"></script>
    <link rel="stylesheet" href="../Styles/chosen.css" type="text/css" media="screen" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         var user = <%= Session("UserModel") %>;
         $('.tab-warranties').hide();
 </script>
    <asp:SqlDataSource ID="dsCustomers" runat="server" SelectCommand=""></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMake" runat="server" SelectCommand="SELECT  OEMID, Make FROM dbo.tblMake where oemid is not null union select 0, ' Please Select' 
    ORDER BY Make"></asp:SqlDataSource>

    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
		    <div class="span4">
		        <label style="font-weight:bold;">Select a Company</label>
                <asp:DropDownList stlye="width: 300px;" ID="cboCustomer" style="padding: 5px;" CssClass="uniform" runat="server" DataMember="DefaultView" 
                    DataSourceID="dsCustomers" DataTextField="Company"  DataValueField="CustomerNo" >
                </asp:DropDownList><br />

                <input class="btn btn-success" type="button" value="New Customer" id="btnNewCustomer" />
                <input class="btn btn-danger" type="button" value="Delete Customer" id="btnDeleteCustomer" />
                <p class="success" id="new-customer-success"></p>
		    </div>
            <div class="span4">
                <div>
                    <label style="font-weight:bold;">Customer Type</label>
                    <select class="required" style="padding: 4px; width:200px;" CssClass="uniform" id="emlCustomerType" name="emlCustomerType">
                        <option value=""></option>
                        <option value="Aftermarket">Aftermarket</option>
                        <option value="Collision Shop">Collision Shop</option>
                        <option value="Repair">Repair</option>
                        <option value="Fleet">Fleet</option>
                        <option value="Dealer">Dealer</option>
                    </select>
                
                    <label style="display: block;width: 100%;font-weight:bold;">Customer Makes</label>
                    <select id="emlCompanyMakes" data-placeholder="Choose Make/Make Group" style="padding: 4px;width:250px;margin-right: 50%;" multiple class="chzn-select">
                        <option value='Audi'>Audi</option>
                        <option value='Bentley'>Bentley</option>
                        <option value='BMW'>BMW</option>
                        <option value='Chrysler'>Chrysler</option>
                        <option value='Daewoo'>Daewoo</option>
                        <option value='Eagle'>Eagle</option>
                        <option value='Fiat'>Fiat</option>
                        <option value='Ford'>Ford</option>
                        <option value='GM'>GM</option>
                        <option value='Honda'>Honda</option>
                        <option value='Hyundai'>Hyundai</option>
                        <option value='Infiniti'>Infiniti</option>
                        <option value='Isuzu'>Isuzu</option>
                        <option value='Jaguar'>Jaguar</option>
                        <option value='Kia'>Kia</option>
                        <option value='Land Rover'>Land Rover</option>
                        <option value='Lexus'>Lexus</option>
                        <option value='Maybach'>Maybach</option>
                        <option value='Mazda'>Mazda</option>
                        <option value='Mercedes'>Mercedes</option>
                        <option value='MINI'>MINI</option>
                        <option value='Mitsubishi'>Mitsubishi</option>
                        <option value='Nissan'>Nissan</option>
                        <option value='Porsche'>Porsche</option>
                        <option value='Ram'>Ram</option>
                        <option value='Renault'>Renault</option>
                        <option value='Saab'>Saab</option>
                        <option value='Scion'>Scion</option>
                        <option value='Smart'>Smart</option>
                        <option value='Subaru'>Subaru</option>
                        <option value='Suzuki'>Suzuki</option>
                        <option value='Toyota'>Toyota</option>
                        <option value='Volkswagen'>Volkswagen</option>
                        <option value='Volvo'>Volvo</option>
                    </select><br />
                
                    <input class="btn btn-primary" type="button" value="Generate Email" id="btnGenerateEmail" />
                </div>
            </div>
            <div class="span4">
                <label style="font-weight:bold;">Add Customers</label>
                <asp:FileUpload ID="FileUpload1" 
                    runat="server" style="margin:10px;" /><asp:Button
                        cssclass="btn btn-primary btn-small" ID="btnUpload" runat="server" Text="Add" />
                <a style="display: block;" id="btnCSVExample" href="#">See Example</a>
            </div>  
        </div>

    </div>

    </form>	
    <hr />

    <div class="container-fluid main">
    
        <div style="display:none;" id="loading-overlay">
            <img src="../images/ajax-loader-blue.gif" />
        </div>

        <div class="row-fluid">
		    <div class="span6">
		        <h2 id="customer-name"></h2>
		    </div>
        </div>

        <div style="margin-bottom:2em;" class="row-fluid">
            <%--<div class="twocol">
		        <label style="font-weight:bold; width: 100%;float: left;">Make</label>
                <asp:DropDownList stlye="width: 300px;" ID="cboMake" style="padding:5px;" CssClass="uniform" runat="server" DataMember="DefaultView" 
                    DataSourceID="dsMake" DataTextField="Make"  DataValueField="OEMID" >
                </asp:DropDownList>	
                    <option>Dealer</option>
                    <option>List</option>
                </select>
		    </div>--%>
		    <div class="span4">
                <h4 style="padding:10px 0">Edit Company Info</h4>
                <form class="small-form">
                    <input type="text" style="display:none;" id="CompanyID" name="CompanyID" />
                
                    <label>Customer Type</label>
                    <select class="required" style="padding: 4px;width:200px;margin-right: 50%;" id="edtCustomerType" name="edtCustomerType" size="1">
                        <option value=""></option>
                        <option value="Aftermarket">Aftermarket</option>
                        <option value="Collision Shop">Collision Shop</option>
                        <option value="Repair">Repair</option>
                        <option value="Fleet">Fleet</option>
                        <option value="Dealer">Dealer</option>
                    </select>
                
                    <label>Customer Makes</label>
                    <select id="edtCompanyMakes" data-placeholder="Choose Make/Make Group" style="padding: 4px;width:250px;margin-right: 50%;" multiple class="chzn-select">
                        <option value='Audi'>Audi</option>
                        <option value='Bentley'>Bentley</option>
                        <option value='BMW'>BMW</option>
                        <option value='Chrysler'>Chrysler</option>
                        <option value='Daewoo'>Daewoo</option>
                        <option value='Eagle'>Eagle</option>
                        <option value='Fiat'>Fiat</option>
                        <option value='Ford'>Ford</option>
                        <option value='GM'>GM</option>
                        <option value='Honda'>Honda</option>
                        <option value='Hyundai'>Hyundai</option>
                        <option value='Infiniti'>Infiniti</option>
                        <option value='Isuzu'>Isuzu</option>
                        <option value='Jaguar'>Jaguar</option>
                        <option value='Kia'>Kia</option>
                        <option value='Land Rover'>Land Rover</option>
                        <option value='Lexus'>Lexus</option>
                        <option value='Maybach'>Maybach</option>
                        <option value='Mazda'>Mazda</option>
                        <option value='Mercedes'>Mercedes</option>
                        <option value='MINI'>MINI</option>
                        <option value='Mitsubishi'>Mitsubishi</option>
                        <option value='Nissan'>Nissan</option>
                        <option value='Porsche'>Porsche</option>
                        <option value='Ram'>Ram</option>
                        <option value='Renault'>Renault</option>
                        <option value='Saab'>Saab</option>
                        <option value='Scion'>Scion</option>
                        <option value='Smart'>Smart</option>
                        <option value='Subaru'>Subaru</option>
                        <option value='Suzuki'>Suzuki</option>
                        <option value='Toyota'>Toyota</option>
                        <option value='Volkswagen'>Volkswagen</option>
                        <option value='Volvo'>Volvo</option>
                    </select>

                    <label>Company Name</label>
                    <input type="text" class="required" id="edtCompanyName" name="txtCompanyName" />

                    <label>Customer No</label>
                    <input type="text" class="required" id="edtCustomerNo" />
                    <label class="error cust-no-error" style="display:none;">Customer Number already in use</label>
                
                    <label>Address</label>
                    <input type="text" class="required" id="edtAddress" />
                
                    <label>City</label>
                    <input type="text" class="required" id="edtCity" />
                
                    <label>State</label>
                    <select class="required" style="padding: 4px;width:150px;margin-right: 50%;" id="edtState" name="edtState" size="1">
                        <option value="AL">AL</option>
                        <option value="AK">AK</option>
                        <option value="AB">AB</option>
                        <option value="AZ">AZ</option>
                        <option value="AR">AR</option>
                        <option value="BC">BC</option>
                        <option value="CA">CA</option>
                        <option value="CO">CO</option>
                        <option value="CT">CT</option>
                        <option value="DE">DE</option>
                        <option value="DC">DC</option>
                        <option value="FL">FL</option>
                        <option value="GA">GA</option>
                        <option value="HI">HI</option>
                        <option value="ID">ID</option>
                        <option value="IL">IL</option>
                        <option value="IN">IN</option>
                        <option value="IA">IA</option>
                        <option value="KS">KS</option>
                        <option value="KY">KY</option>
                        <option value="LA">LA</option>
                        <option value="ME">ME</option>
                        <option value="MB">MB</option>
                        <option value="MD">MD</option>
                        <option value="MA">MA</option>
                        <option value="MI">MI</option>
                        <option value="MN">MN</option>
                        <option value="MS">MS</option>
                        <option value="MO">MO</option>
                        <option value="MT">MT</option>
                        <option value="NE">NE</option>
                        <option value="NV">NV</option>
                        <option value="NB">NB</option>
                        <option value="NH">NH</option>
                        <option value="NJ">NJ</option>
                        <option value="NM">NM</option>
                        <option value="NY">NY</option>
                        <option value="NL">NL</option>
                        <option value="NC">NC</option>
                        <option value="ND">ND</option>
                        <option value="NT">NT</option>
                        <option value="NS">NS</option>
                        <option value="NU">NU</option>
                        <option value="OH">OH</option>
                        <option value="OK">OK</option>
                        <option value="ON">ON</option>
                        <option value="OR">OR</option>
                        <option value="PA">PA</option>
                        <option value="PE">PE</option>
                        <option value="PR">PR</option>
                        <option value="QC">QC</option>
                        <option value="RI">RI</option>
                        <option value="SK">SK</option>
                        <option value="SC">SC</option>
                        <option value="SD">SD</option>
                        <option value="TN">TN</option>
                        <option value="TX">TX</option>
                        <option value="UT">UT</option>
                        <option value="VT">VT</option>
                        <option value="VA">VA</option>
                        <option value="WA">WA</option>
                        <option value="WV">WV</option>
                        <option value="WI">WI</option>
                        <option value="WY">WY</option>
                        <option value="YT">YT</option>
                    </select>
                
                    <label>Zip</label>
                    <input type="text" class="required" id="edtZip" />
                
                    <label>Phone</label>
                    <input type="text" class="required" id="edtPhone" />

                    <label>Salesperson Email</label>
                    <input type="text" class="required" id="edtSalesmanEmail" />
                
                   <%-- <div style="padding:10px 0;">
                        <label for="edtAutoNation">In-network dealer &nbsp;</label>
                        <input type="checkbox" id="edtAutoNation" name="edtAutoNation" style="margin-top:8px;"/>
                        <div class="clear"></div>
                    </div>--%>

                    <p><input class="btn btn-primary btn-small" type="button" value="Update" id="btnUpdateInfo"/></p>
                    <p style="display:none;" class="success" id="update_saved">Information Updated.</p>
                </form>
            </div>
            <div class="span4 discounts">
		        <h4 style="padding:10px 0">Edit Discounts</h4>
                <div id="discount-radios">
                    <div style="margin-left:auto; margin-right:auto; width:230px;"><label for="all" style="display:inline;">All Makes &nbsp;</label><input id="all" type="radio" name="discount-type" value="all" checked="checked" style="display:inline;">
                    <label for="groups" style="display:inline; margin-left:10px;">Individual Groups &nbsp;</label><input id="groups" type="radio" name="discount-type" value="groups" style="display:inline;"></div>
                    <div class="clear"></div>
                </div>
                <div class="panel discounts-all">
                    <h3>All Makes</h3>
                    <label style="font-weight:bold; width: 100%;float: left;">Discount Type</label>
                    <select style="padding:4px;width: 100%;font-size: 16px;" id="discount_type">
                        <option></option>
                        <option>Dealer</option>
                        <option>List</option>
                    </select>

                    <div style="width:100%"> 
                        <label style="font-weight:bold; width: 100%;float: left;">Amount</label>
                        <input style="padding:4px;font-size:16px;width:60px;" type="text" id="amount" />
                    </div>

                    <div style="margin:10px 0;" id="slider"></div>

                    <input class="btn btn-primary btn-small" type="button" value="Save" id="btnSaveAll" />
                    <p style="display:none;" class="all-markup-saved success">Markup Amount Saved</p> 
                </div>
                <div style="display:none;" class="panel discounts-group">
                    <input class="btn btn-primary btn-small btnSaveMG" type="button" value="Save" />
                    <p style="display:none;" class="group-markup-saved success">Markup Amounts Saved</p>

                    <!--javascript appends groups here-->

                    <input class="btn btn-primary btn-small btnSaveMG" type="button" value="Save" />
                    <p style="display:none;" class="group-markup-saved success">Markup Amounts Saved</p> 
                </div>  
		    </div>
            <div class="span4">
                <div class="section">
                    <h4 style="padding:10px 0">Edit Individual Users</h4>
                    <input class="btn btn-success btn-small" style="display:none" type="button" value="New User" id="btnNewUser" />
                    <input class="btn btn-danger btn-small" style="display:none" type="button" value="Delete User" id="btnDeleteUser" />
                    <select size="2" style="padding:5px;font-size: 16px;width:100%;" id="cboUser"></select>
                    <p class="success" id="new-user-success"></p>
		        </div>
                 <div class="small-form section">
                 <label>User Company &nbsp;</label>
                  <asp:DropDownList stlye="width: 300px;" ID="selUserComp" style="padding: 5px;" CssClass="uniform" runat="server" DataMember="DefaultView" 
                    DataSourceID="dsCustomers" DataTextField="Company"  DataValueField="CustomerNo" >
                </asp:DropDownList>
                    <input class="btn btn-primary btn-small" type="button" value="Update" id="btnUserComp" />
                     <p class="success" id="userCompSuccess" style="display:none;"></p>
		        </div>
                <hr />
                <div class="small-form section">
                    <input type="checkbox" id="active"  style="margin-top:8px;"/>
                    <label>Active &nbsp;</label>
                    <div class="clear"></div>
                    <input class="btn btn-primary btn-small" type="button" value="Update" id="btnActive" />
                    <p class="success" id="activesuccess" style="display:none;"></p>
           
                    <div class="clear"></div>
		        </div>  
                <hr />
                <div class="small-form section">
                    <input type="checkbox" id="canorder"  style="margin-top:8px;"/>
                    <label>Can Place Orders &nbsp;</label>
                    <div class="clear"></div>
                    <input class="btn btn-primary btn-small" type="button" value="Update" id="btnCanOrder" />
                    <p class="success" id="canordersuccess" style="display:none;"></p>
           
                    <div class="clear"></div>
		        </div>
                <hr />
                 <div class="small-form section" id="tier">
                 <label>Tier &nbsp;</label>
                   <select style="padding:4px;width: 100%;font-size: 16px;" id="selTier">
                    </select>
                    <input class="btn btn-primary btn-small" type="button" value="Update" id="btnTier" />
                     <p class="success" id="tiersuccess" style="display:none;"></p>
		        </div>  
                <hr />
		        <div class="small-form section">
                    <label>Current Password</label>
                    <input disabled="disabled" type="text" id="oldpassword" />
                    <label>New Password</label>
                    <input type="text" id="newpassword" />
                    <input class="btn btn-primary btn-small" type="button" value="Update" id="btnPassword" />
                    <p class="success" id="passwordsuccess" style="display:none;">Information Updated.</p>
       
                    <div class="clear"></div>
		        </div>
                <hr />
                <div class="small-form section">
                    <label>Current Email</label>
                    <input disabled="disabled" type="text" id="oldemail" />
                    <label>New Email</label>
                    <input type="text" id="newemail" />
                    <input class="btn btn-primary btn-small" type="button" value="Update" id="btnEmail"/>
                    
                    <p class="success" id="emailsuccess" style="display:none;">Information Updated.</p>
          
                    <div class="clear"></div>
		        </div>
		    </div>
        </div> 
    </div>

    <hr />
    <div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
		    <div class="span4">
                <table>
                <tr><td><label style="font-weight:bold;">Admin Tools</label></td></tr>
                <tr><td><input class="btn btn-success" type="button" value="New Admin User" id="btnNewAdminUser" />&nbsp;&nbsp;&nbsp;<input class ="btn btn-success" type="button" value="Edit Existing Admin" id="btnEditAdminUser" /></td></tr>
                <p class="success" id="new-admin-user-success"></p>
                </table>
             </div>
          </div>
        </div>

    <div id="csv-example" style="display:none;" title="CSV Example">
        <p>File needs to be in *.CSV format with a header row and column names (in this order):</p>
        <ul style="list-style: none;">
            <li>Name</li>
            <li>Address</li>
            <li>City</li>
            <li>State</li>
            <li>Zip</li>
            <li>Phone</li>
            <li>Saleman Email</li>
            <li>CustomerNo</li>
            <li>Tier ID
                <ul style="list-style: none; font-size:10px;margin-left:10px 10px 10px 40px; padding:0px;"> List of tiers
                    <li>
                        <table id="csv-tiers" class="table-bordered">
                        <thead><tr><th>Tier ID</th><th>Tier Name</th></tr></thead>
                        <tbody></tbody>
                        </table>
                   </li>
                </ul>
            </li>
            <li>Desired Usrename</li>
            <li>Desired Password</li>
            <li>User Email</li>
        </ul>

        <a style="margin: 10px 0;display: block;color: Blue;" href="../../files/customers.csv">Download template</a>
        <img alt="CSV Exmaple" src="../../images/csv_example.png" />
    </div>


    <div id="new-customer" style="display:none;" title="Create New Customer">
        <form class="small-form">
            <ul id="error_contain"></ul>
            <label>Customer Type</label>
            <select class="required" style="padding: 4px;width:200px;margin-right: 50%;" id="txtCustomerType" name="txtCustomerType" size="1">
                <option value=""></option>
                 <option value="Aftermarket">Aftermarket</option>
                <option value="Collision Shop">Collision Shop</option>
                <option value="Repair">Repair</option>
                <option value="Fleet">Fleet</option>
                <option value="Dealer">Dealer</option>
            </select>

            <label>Customer Makes</label>
            <select id="txtCompanyMakes" data-placeholder="Choose Make/Make Group" style="padding: 4px;width:250px;margin-right: 50%;" multiple class="chzn-select">
                <option>Audi</option>
                <option>Bentley</option>
                <option>BMW</option>
                <option>Chrysler</option>
                <option>Daewoo</option>
                <option>Eagle</option>
                <option>Fiat</option>
                <option>Ford</option>
                <option>GM</option>
                <option>Honda</option>
                <option>Hyundai</option>
                <option>Infiniti</option>
                <option>Isuzu</option>
                <option>Jaguar</option>
                <option>Kia</option>
                <option>Land Rover</option>
                <option>Lexus</option>
                <option>Maybach</option>
                <option>Mazda</option>
                <option>Mercedes</option>
                <option>MINI</option>
                <option>Mitsubishi</option>
                <option>Nissan</option>
                <option>Porsche</option>
                <option>Ram</option>
                <option>Renault</option>
                <option>Saab</option>
                <option>Scion</option>
                <option>Smart</option>
                <option>Subaru</option>
                <option>Suzuki</option>
                <option>Toyota</option>
                <option>Volkswagen</option>
                <option>Volvo</option>
            </select>

            <label>Company Name</label>
            <input type="text" class="required" id="txtCompanyName" name="txtCompanyName" />
            <label>Customer No</label>
            <input type="text" class="required" id="txtCustomerNo" />
            <label class="error cust-no-error" style="display:none;">Customer Number already in use</label>
            <label>Address</label>
            <input type="text" class="required" id="txtAddress" />
            <label>City</label>
            <input type="text" class="required" id="txtCity" />
            <label>State</label>
            <select class="required" style="padding: 4px;width:150px;margin-right: 50%;" id="txtState" name="txtState" size="1">
                <option value="AL">AL</option>
                <option value="AK">AK</option>
                <option value="AB">AB</option>
                <option value="AZ">AZ</option>
                <option value="AR">AR</option>
                <option value="BC">BC</option>
                <option value="CA">CA</option>
                <option value="CO">CO</option>
                <option value="CT">CT</option>
                <option value="DE">DE</option>
                <option value="DC">DC</option>
                <option value="FL">FL</option>
                <option value="GA">GA</option>
                <option value="HI">HI</option>
                <option value="ID">ID</option>
                <option value="IL">IL</option>
                <option value="IN">IN</option>
                <option value="IA">IA</option>
                <option value="KS">KS</option>
                <option value="KY">KY</option>
                <option value="LA">LA</option>
                <option value="ME">ME</option>
                <option value="MB">MB</option>
                <option value="MD">MD</option>
                <option value="MA">MA</option>
                <option value="MI">MI</option>
                <option value="MN">MN</option>
                <option value="MS">MS</option>
                <option value="MO">MO</option>
                <option value="MT">MT</option>
                <option value="NE">NE</option>
                <option value="NV">NV</option>
                <option value="NB">NB</option>
                <option value="NH">NH</option>
                <option value="NJ">NJ</option>
                <option value="NM">NM</option>
                <option value="NY">NY</option>
                <option value="NL">NL</option>
                <option value="NC">NC</option>
                <option value="ND">ND</option>
                <option value="NT">NT</option>
                <option value="NS">NS</option>
                <option value="NU">NU</option>
                <option value="OH">OH</option>
                <option value="OK">OK</option>
                <option value="ON">ON</option>
                <option value="OR">OR</option>
                <option value="PA">PA</option>
                <option value="PE">PE</option>
                <option value="PR">PR</option>
                <option value="QC">QC</option>
                <option value="RI">RI</option>
                <option value="SK">SK</option>
                <option value="SC">SC</option>
                <option value="SD">SD</option>
                <option value="TN">TN</option>
                <option value="TX">TX</option>
                <option value="UT">UT</option>
                <option value="VT">VT</option>
                <option value="VA">VA</option>
                <option value="WA">WA</option>
                <option value="WV">WV</option>
                <option value="WI">WI</option>
                <option value="WY">WY</option>
                <option value="YT">YT</option>
            </select>
            <label>Zip</label>
            <input type="text" class="required" id="txtZip" />
            <label>Phone</label>
            <input type="text" class="required" id="txtPhone" />
            <label>Preferred Salesman(s) Email<h6>(seperate by commas)</h6></label>
            <input type="text" class="required" id="txtSalesman" />

            <p class="error" id="new-customer-error"></p>
    </div>
    </form>
    <div id="new-user" style="display:none;" title="Create New User">
        <form class="small-form">
            <ul id="Ul1"></ul>
            <label>UserName</label>
            <input type="text" class="required" id="txtUserName" name="txtUserName" />
            <label class="error" style="display:none;" id="good-username">UserName already in use</label>
            <label>Password</label>
            <input type="text" class="required" id="txtPassword" />
            <label>Email</label>
            <input type="text" class="required" id="txtEmail" />
            <label>Tier</label>
            <select style="padding:4px;width: 100%;font-size: 16px;" id="selTierNew">
                    </select>
            <p class="error" id="new-user-error"></p>
        </form>
    </div>

     <div id="new-admin-user" style="display:none;" title="Create New Admin User">
        <form class="small-form">
            <ul id="Ul2"></ul>
            <label>UserName</label>
            <input type="text" class="required" id="txtAdminUserName" name="txtUserName" />
            <label class="error" style="display:none;" id="good-admin-username">UserName already in use</label>
            <label>Password</label>
            <input type="text" class="required" id="txtAdminPassword" />
            <label>Email</label>
            <input type="text" class="required" id="txtAdminEmail" />
            <p class="error" id="new-admin-user-error"></p>
        </form>
    </div>

    <div id="updateAdminUser" style="display:none;" title="Edit Existing User">
        <form class="small-form"> 
        <select id="AdminDDL"></select>
         <table>
             <tr><td>UserName:</td><td><input id="AdminUsersName" type="text" readonly /></td></tr>
             <tr><td>Email:</td><td><input  id="AdminEmail" type="text" /></td></tr>
             <tr><td>Password:</td><td><input id="AdminPassword" type="text" /></td></tr>
         </table>
            </form> 
    </div>

        <div id="SuccessDialog" style="display:none;" title="Success">
             <label id="Success" style="display:none;"></label>
    </div>

    <div id="email-list" style="display:none;" title="Email Recipients">
        <span></span>
    </div>
    <div id="delete-company" style="display:none;" title="Email Recipients">
        <span>Are you Sure You want to Delete this Company?</span>
    </div>

    <script id="usersTemplate" type="text/html">
        <option value="${UserID}">${Username}</option>
    </script>
</asp:Content>