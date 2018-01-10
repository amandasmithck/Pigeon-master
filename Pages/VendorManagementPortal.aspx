<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="VendorManagementPortal.aspx.vb" Inherits="Pigeon.VendorManagementPortal1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="/Scripts/jquery-1.10.2.js"></script>
     <link href="/Styles/bootstrap-3.3.7/bootstrap-3.3.7.min.css" rel="stylesheet" />     
    <script type="text/javascript" src="/Scripts/bootstrap-3.3/bootstrap-3.3.7.js" ></script>
    <link href="/Styles/sweetalert2.css" rel="stylesheet"/>
    <link href="/Styles/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="/Styles/bootstrapVendor.css" rel="stylesheet" />
      
    <style type="text/css">
        .validationMessage { color: Red; }
.customMessage { color: Orange; }
        .NavbarMenu
        {
            color:#F88017;
            font-size:20px;
            cursor:pointer
        }
        .Options{
            color:black;
            font-size:15px;
            font-weight:bold;
            cursor:pointer;
            width:10px;
        }
        .InformationTable{
            
              /*float: right;
              margin-right: 100px;*/
              width: 1000px;
              /*height:400px;*//*Check on this later..Bc Atm its just so big..*/
              /*margin-top: 0;*/
              margin-left: 280px;
        }
        .SubmitButton{
              /*float: right;*/
              /*margin-right: 100px;*/
              /*width: 1000px;*/
              /*height:400px;*//*Check on this later..Bc Atm its just so big..*/
              margin-top: 30px;
              margin-left:700px;
        }
    </style>


  
    <div data-bind="with: vendorInfo" id="body">
    <div id="form1">
        <script id="customMessageTemplate" type="text/html">
        <em class="customMessage" data-bind='validationMessage: field'></em>
    </script>
<%--    <header>
          <div class="navbar-fixed-top" style="background-color:black;width:100%;height:50px;">
            <div class="navbar-top" style="background-color:black;width:100%;height:50px;">

            <label></label>
            <div style="float:right;margin-right:20px;margin-top:20px;">
                <a href="Home.aspx"><label class="NavbarMenu">Home</label></a>
            </div>
        </div>
    </header>--%>
           <div style="width:100px;height:20px;margin-left:50px;padding-bottom:15px">
            <span class="Options" data-bind="style: { color: isNew() ? '#F88017' : 'black' }, click: function () { isNew(true); RefreshNew(); }">New</span> <span style="font-size:15px;width:5px;">|</span> <span class="Options" data-bind="style: { color: !isNew() ? '#F88017' : 'black' }, click: function () { isNew(false); GetAllVendors() }">Existing</span>
           <%-- <select style="width:200px;margin-bottom:10px;margin-top:5px;" class="form-control"  data-bind="visible: !isNew(), options: exsitingCompanies, optionsText: 'companyName', optionsValue: 'companyID', value: companyInfo.companyID, optionsCaption: 'Choose Company', event: { change: function () { GetExsitingCompanyData() } }" ></select>--%>
        </div>
         <div style="width:100%;height:85%;margin-top:50px;margin-left:40px;">
            <table style="width:200px;position:absolute;margin-top:45px;" class="table table-bordered">
                <tr>
                    <td data-bind="click: ViewCompanyInfo, style: { background: viewCompany() ? '#98AFC7' : 'none' }" style="cursor:pointer;">Company Info<i data-bind="css: companyClass(), style: { color: isCompanyComplete() ? 'green' : 'red' }" style="font-size:15px;float:right"></i></td>
                </tr>
                 <tr>
                    <td data-bind="click: ViewInvoiceGuide, style: { background: viewInvoice() ? '#98AFC7' : 'none' }" style="cursor:pointer;">Invoice Guide Info<i data-bind="css: invoiceClass(), style: { color: isInvoiceComplete() ? 'green' : 'red' }" style="font-size:15px;float:right"></i></td>
                </tr>
                   <tr>
                    <td data-bind="click: ViewShippingInfo, style: { background: viewShipping() ? '#98AFC7' : 'none' }" style="cursor:pointer;">Shipping Addresses<i data-bind="css: shippingClass(), style: { color: isShippingComplete() ? 'green' : 'red' }" style="font-size:15px;float:right"></i></td>
                </tr>
                   <tr>
                    <td data-bind="click: ViewOEMDealerInfo, style: { background: viewOEMDealer() ? '#98AFC7' : 'none' },visible: isOEMDealer()" style="cursor:pointer;">OEM Mark-Ups</td>
                </tr>
                   <%-- <tr>
                    <td data-bind="click: ViewOEMDealerInfo, style: { background: viewOEMDealer() ? '#98AFC7' : 'none' }, visible: isOEMDealer()" style="cursor:pointer;">OEM Something Else<i style="background-color:red;font-size:20px;margin-left:5px;" data-bind="    visible: !isOEMComplete()"></i></td>
                </tr>--%>
            </table>
           
             <select style="width:200px;margin-bottom:10px;margin-top:5px;" class="form-control"  data-bind="visible: !isNew(), options: exsitingCompanies, optionsText: 'companyName', optionsValue: 'companyID', value: companyInfo.companyID, optionsCaption: 'Choose Company', event: { change: function () { GetExsitingCompanyData() } }" ></select>

             <div class="InformationTable" style="margin-top:-45px" data-bind="visible: viewCompany">
                 <table class="table-condensed">
              <tbody data-bind="with: companyInfo">
               <tr style="text-align:center;">
                   <td style="text-align:right;"><label>Company Name</label></td>
                   <td><input placeholder="Company Name" data-bind="value: company" class="form-control" /></td>
                   <td style="text-align:right;"><label>Phone</label></td>
                   <td><input placeholder="Phone" data-bind="value: phone, masked: phone, mask: '(999) 999-9999'" class="form-control"  /></td>
                   <td style="text-align:right;"><label>Fax</label></td>
                   <td><input placeholder="Fax" data-bind="value: fax, masked: fax, mask: '(999) 999-9999'" class="form-control"  /></td>
               </tr>
               <tr style="text-align:center">
                    <td style="text-align:right;"><label>Email</label></td>
                    <td><input placeholder="Email" data-bind="value: email, event: { blur: function () { if (checkEmail()) ; } } " class="form-control"  /></td>
                   <td style="text-align:right;"> <div style="width:180px;"><input type="checkbox" data-bind="checked: isSameEmail" /><span style="margin-left:5px;">Same Email</span></div><label>Warranty Email</label></td>
                  <td><input placeholder="Warranty Email(if applicable)" data-bind="value: isSameEmail() ? email : warrantyEmail" class="form-control"  /></td>
                     <td style="text-align:right;"><label>Contact Name</label></td>
                     <td><input placeholder="Contact Name" data-bind="value: contact" class="form-control" /></td><!--valueUpdate: 'keyup', event: { keyup: function () { sameAsCompany(); }}  option that works..event: { blur: function () { sameAsCompany(); } }-->
               </tr>
                   <tr style="text-align:center">
                        <td style="text-align:right;"><label>Address</label></td>
                   <td><input placeholder="Address" data-bind="value: address1" class="form-control"  /></td> 
                   <td style="text-align:right;"><label>Suite #</label></td>
                   <td><input placeholder="Suite Number(if applicable)" data-bind="value: address2" class="form-control"  /></td>
                   <td style="text-align:right;"><label>City</label></td>
                   <td><input placeholder="City" data-bind="value: city" class="form-control"  /></td>
               </tr>
                  <tr style="text-align:center">
                  <td style="text-align:right;"><label>State</label></td>
                  <td><select class="form-control" data-bind="value: state, optionsCaption: 'State'" >
                                    <option value="AL">Alabama</option>
                                    <option value="AK">Alaska</option>
                                    <option value="AZ">Arizona</option>
                                    <option value="AR">Arkansas</option>
                                    <option value="CA">California</option>
                                    <option value="CO">Colorado</option>
                                    <option value="CT">Connecticut</option>
                                    <option value="DE">Delaware</option>
                                    <option value="DC">District Of Columbia</option>
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
                                    <option value="MD">Maryland</option>
                                    <option value="MA">Massachusetts</option>
                                    <option value="MI">Michigan</option>
                                    <option value="MN">Minnesota</option>
                                    <option value="MS">Mississippi</option>
                                    <option value="MO">Missouri</option>
                                    <option value="MT">Montana</option>
                                    <option value="NE">Nebraska</option>
                                    <option value="NV">Nevada</option>
                                    <option value="NH">New Hampshire</option>
                                    <option value="NJ">New Jersey</option>
                                    <option value="NM">New Mexico</option>
                                    <option value="NY">New York</option>
                                    <option value="NC">North Carolina</option>
                                    <option value="ND">North Dakota</option>
                                    <option value="OH">Ohio</option>
                                    <option value="OK">Oklahoma</option>
                                    <option value="OR">Oregon</option>
                                    <option value="PA">Pennsylvania</option>
                                    <option value="RI">Rhode Island</option>
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
                                    <option value="AS">American Samoa</option>
                                    <option value="GU">Guam</option>
                                    <option value="MP">Northern Mariana Islands</option>
                                    <option value="PR">Puerto Rico</option>
                                    <option value="VI">Virgin Islands</option>
                                </select></td>
                   <td style="text-align:right;"><label>Zip</label></td>
                  <td><input placeholder="Zip" data-bind="value: zip" class="form-control" maxlength="5" /></td>
                 <%--     <td><label>Company Vanity Shipping Name</label></td><!--Diffferent Name to mak it shorter??-->
                   <td><input placeholder="Vanity Shipping Name" data-bind="value: vanityName" class="form-control"  /></td>--%>
               </tr>
               <tr style="text-align:center">
                   
                   <td style="text-align:right;"><label>Vendor Order Method</label></td>
                   <td><select  class="form-control"  data-bind="options: vendorOrderOptions, optionsText: 'type', optionsValue: 'ID', value: vendorOrderMethodID, optionsCaption: 'Vendor Order Method'" ></select></td>
                   <td style="text-align:right;"><label>Status</label></td>
                    <td><span style="float:left;">Active<input type="checkbox" data-bind="checked: active" class="checkbox" style="margin-top:2px;" /></span>
                      <span style="float:right;">OEM Dealer<input type="checkbox" data-bind="checked: isOEMDealer(), event: { change: function () { isOEMDealer(!isOEMDealer()); setOEMRequiredElements(); } }" class="checkbox" style="margin-top:2px;" /></span>
                  </td>
                   <td style="text-align:right;"><label>Notes</label></td>
                     <td><textarea placeholder="Notes" data-bind="value: notes, valueUpdate: 'keyup'"  class="form-control" style="font-size:14px;" ></textarea></td>
                      <!--event: { keyup: checkForSpecialCharacters }-->
               </tr>
           </tbody>
        </table>
             </div>
                          
             <div class="InformationTable" style="margin-top:-45px" data-bind="with: invoiceGuideInfo,visible: viewInvoice">
               <select style="width:200px;float:right;margin-bottom:10px" class="form-control"  data-bind="options: companiesToMirror, optionsText: 'companyName', optionsValue: 'companyID', value: companyToMirror, optionsCaption: 'Company To Mirror', event: { change: function () { MirrorCompany('invoice');  } }" ></select>
               <table class="table table-condensed table-bordered table-striped">
            <thead>
                <tr style="font-weight:bold">
                    <td>Guide Type</td>
                    <td style="text-align:center">Guide Value</td>
                </tr>
            </thead>
           <tbody data-bind="foreach: allElements">
               <tr><td><span data-bind="text: elementName"></span><br /><span data-bind="text: '* ' + description" style="color:blue;font-size:10px;"></span></td>
               <td data-bind="visible: elementDataTypeID == 1"><input type="checkbox" data-bind="visible: elementDataTypeID == 1, checked: checkBoxValue" class="form-control" style="text-align:right;height:20px;" />
                   </td><td data-bind="visible: elementDataTypeID > 2"><input type="text" class="form-control" data-bind=" visible: elementDataTypeID > 2, value: textValue"/></td><td data-bind="    visible: elementID == 29 "><select class="form-control" data-bind="    visible: elementID == 29, options: $parent.allVendorTypes, optionsText: 'direction', optionsValue: 'ID', value: $parent.chosenVendorTypeID, optionsCaption: 'Please Select Vendor Type', event: { change: function () {  } }"></select></td><td data-bind="visible: elementID == 26"><select class="form-control" data-bind="    visible: elementID == 26, options: $parent.allPaymentTypes, optionsText: 'direction', optionsValue: 'ID', value: $parent.chosenPaymentTypeID, optionsCaption: 'Please Select Payment Type'"></select></td>
                   </tr>
           </tbody>
        </table>
          </div>
       
             <div class="InformationTable" style="margin-top:-45px;" data-bind="visible: viewShipping">
                 <div style="width:180px;margin-bottom:5px"><span>Same as Company Address</span><input type="checkbox" data-bind="checked: isSameAddress, event: { change: function () { sameAsCompany() }}"/></div>
                    <table class="table table-bordered table-striped table-condensed">
                    <thead  data-bind="with: emptyShippingInfo">
                        <tr style="text-align:center">
                           <td><label>Shipping Direction</label></td>
                           <td><label>Vanity Name</label></td>
                           <td><label>Address</label></td>
                           <td><label>Suite #</label></td>
                           <td><label>City</label></td>
                           <td><label>State</label></td>
                           <td><label>Zip</label></td>
                           <td><label>Contact Name</label></td>
                           <td><label>Closing Time</label></td>
                       </tr>
                         <tr>
                          <td><select class="form-control"  data-bind="options: shippingDirections, optionsText: 'direction', optionsValue: 'ID', value: shippingDirectionTypeID, optionsCaption: 'Shipping Direction', event: { change: function () {  } }" ></select></td>
                             <td><input placeholder="Vanity" data-bind="value: vanityName" class="form-control"  /></td>
                                  <td><input placeholder="Address" data-bind="value: address1" class="form-control"  /></td>
                          <td><input placeholder="Suite #(if applicable)" data-bind="value: address2" class="form-control"  /></td>
                          <td><input placeholder="City" data-bind="value: city" class="form-control"  /></td>
                          <td><select class="form-control" data-bind="value: state, optionsCaption: 'State'" >
                                            <option value="AL">Alabama</option>
                                            <option value="AK">Alaska</option>
                                            <option value="AZ">Arizona</option>
                                            <option value="AR">Arkansas</option>
                                            <option value="CA">California</option>
                                            <option value="CO">Colorado</option>
                                            <option value="CT">Connecticut</option>
                                            <option value="DE">Delaware</option>
                                            <option value="DC">District Of Columbia</option>
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
                                            <option value="MD">Maryland</option>
                                            <option value="MA">Massachusetts</option>
                                            <option value="MI">Michigan</option>
                                            <option value="MN">Minnesota</option>
                                            <option value="MS">Mississippi</option>
                                            <option value="MO">Missouri</option>
                                            <option value="MT">Montana</option>
                                            <option value="NE">Nebraska</option>
                                            <option value="NV">Nevada</option>
                                            <option value="NH">New Hampshire</option>
                                            <option value="NJ">New Jersey</option>
                                            <option value="NM">New Mexico</option>
                                            <option value="NY">New York</option>
                                            <option value="NC">North Carolina</option>
                                            <option value="ND">North Dakota</option>
                                            <option value="OH">Ohio</option>
                                            <option value="OK">Oklahoma</option>
                                            <option value="OR">Oregon</option>
                                            <option value="PA">Pennsylvania</option>
                                            <option value="RI">Rhode Island</option>
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
                                            <option value="AS">American Samoa</option>
                                            <option value="GU">Guam</option>
                                            <option value="MP">Northern Mariana Islands</option>
                                            <option value="PR">Puerto Rico</option>
                                            <option value="VI">Virgin Islands</option>
                                        </select></td>
                          <td><input placeholder="Zip" data-bind="value: zip" class="form-control"  /></td>
                          <td><input placeholder="Contact Name" data-bind="value: contact" class="form-control" /></td>
                          <td><input id="ClosingTime" type="text" class="form-control" data-bind="value: closingTime" /></td>
                          <td><button data-bind="click: AddShippingAddress,visible: !isNew()" class="btn btn-success">Add</button></td>
                       </tr>
                    </thead>   
                    <tbody data-bind="foreach: exsitingShippingInfo"><!--Visible if it is not New OR if exsiting ShippingINfo has stuff in it!-->
                       <tr>
                          <td><select  class="form-control"  data-bind="options: shippingDirections, optionsText: 'direction', optionsValue: 'ID', value: shippingDirectionTypeID, optionsCaption: 'Shipping Direction',enable: isEdit()" ></select></td>
                             <td><label data-bind="text: vanityName"></label><input placeholder="Vanity" data-bind="value: vanityName, visible: isEdit()" class="form-control"  /></td>
                              <td><label data-bind="text: address1"></label><input placeholder="Address" data-bind="value: address1, visible: isEdit()" class="form-control"  /></td>
                          <td><label data-bind="text: address2"></label><input placeholder="Suite Number(if applicable)" data-bind="value: address2, visible: isEdit()" class="form-control"  /></td>
                          <td><label data-bind="text: city"></label><input placeholder="City" data-bind="value: city, visible: isEdit()" class="form-control"  /></td>
                            <td><label data-bind="text: state"></label><select class="form-control" data-bind="value: state, optionsCaption: 'State', visible: isEdit()" >
                                            <option value="AL">Alabama</option>
                                            <option value="AK">Alaska</option>
                                            <option value="AZ">Arizona</option>
                                            <option value="AR">Arkansas</option>
                                            <option value="CA">California</option>
                                            <option value="CO">Colorado</option>
                                            <option value="CT">Connecticut</option>
                                            <option value="DE">Delaware</option>
                                            <option value="DC">District Of Columbia</option>
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
                                            <option value="MD">Maryland</option>
                                            <option value="MA">Massachusetts</option>
                                            <option value="MI">Michigan</option>
                                            <option value="MN">Minnesota</option>
                                            <option value="MS">Mississippi</option>
                                            <option value="MO">Missouri</option>
                                            <option value="MT">Montana</option>
                                            <option value="NE">Nebraska</option>
                                            <option value="NV">Nevada</option>
                                            <option value="NH">New Hampshire</option>
                                            <option value="NJ">New Jersey</option>
                                            <option value="NM">New Mexico</option>
                                            <option value="NY">New York</option>
                                            <option value="NC">North Carolina</option>
                                            <option value="ND">North Dakota</option>
                                            <option value="OH">Ohio</option>
                                            <option value="OK">Oklahoma</option>
                                            <option value="OR">Oregon</option>
                                            <option value="PA">Pennsylvania</option>
                                            <option value="RI">Rhode Island</option>
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
                                            <option value="AS">American Samoa</option>
                                            <option value="GU">Guam</option>
                                            <option value="MP">Northern Mariana Islands</option>
                                            <option value="PR">Puerto Rico</option>
                                            <option value="VI">Virgin Islands</option>
                                        </select></td>
                          <td><label data-bind="text: zip"></label><input placeholder="Zip" data-bind="value: zip, visible: isEdit()" class="form-control"  /></td>
                          <td><label data-bind="text: contact"></label><input placeholder="Contact Name" data-bind="value: contact, visible: isEdit()" class="form-control" /></td>
                          <td><label data-bind="text: moment(closingTime).format('hh:mm')"></label><input id="ClosingTime1" type="text" class="form-control" data-bind="value: closingTime, visible: isEdit()" /></td>
                           <td class="btn-group">                          
                               <button type="button" class="btn btn-danger dropdown-toggle" data-bind="click: VanityShippingOptionToggle"><span style="margin:3px;" class="glyphicon glyphicon-menu-down"></span><%--<span class="caret"></span>--%></button>
                               <ul data-bind="attr: { id: 'vanityOptionList_' + vendorShippingID }" style="align-content:center;background-color:#d9534f;color:white" class="dropdown-menu">
                                   <li><span class="glyphicon glyphicon-edit" style="font-size:large;margin-left:5%;margin-right:2%;cursor:pointer" data-bind="click: EditShippingAddress.bind($data, vendorShippingID)">Edit</span></li>
                                   <li><span class="glyphicon glyphicon-trash" style="font-size:large;margin-left:5%;margin-right:2%;cursor:pointer" data-bind="click: DeleteShippingAddress.bind($data,vendorShippingID)">Delete</span></li>
                               </ul>
                           </td>
                   
                               </tr>
                
                   </tbody>
                </table>
            </div>

             <div class="InformationTable" style="margin-top:-45px" data-bind="visible: viewOEMDealer, with: oemDealerInfo">
            <select style="width:200px;float:right;margin-bottom:10px" class="form-control"  data-bind="options: companiesToMirror, optionsText: 'companyName', optionsValue: 'companyID', value: companyToMirror, optionsCaption: 'Company To Mirror', event: { change: function () { MirrorCompany('oem'); } }" ></select>
        <table class="table table-bordered table-striped table-condensed">
            <thead class="fix">
                <tr style="font-size:15px;font-weight:bold">
                    <td>Make</td>
                    <td style="text-align:right;">Mark-Up</td>
                    <td style="text-align:right;">Customer Mark-Up</td>
                </tr>
            </thead>  
             <tbody data-bind="foreach: markUpInfo">
               <tr>
                     <td data-bind="text: make"></td>
                     <td><input data-bind="value: markUp" class="form-control" style="float:left;width:95%;text-align:right" /><span style="margin-top:5px;font-weight:bold;font-size:20px">%</span></td>
                     <td><input data-bind="value: markUpCustomer" class="form-control" style="float:left;width:95%;text-align:right" /><span style="margin-top:5px;font-weight:bold;font-size:20px">%</span></td>
               </tr>
           </tbody>
        </table>
            </div>
         </div>
            <button data-bind="click: SendData, text: isNew() ? 'Create Vendor' : 'Update Vendor', style: { cursor: isComplete() ? 'pointer' : 'not-allowed' }, event: { mouseover: function () { errors.showAllMessages(); } }" class="SubmitButton btn btn-md btn-primary"></button>

    <footer></footer>
    </div>
        </div>
  
    <script src="/Scripts/bootstrapVendor.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/spin.js"></script>
    <script src="/Scripts/spinner.js"></script>
   <script src="/Scripts/bootstrap-datepicker.js"></script>
    <script src="/Scripts/knockout_3_2_0.js"></script>
    <script src="/Scripts/knockout.validation.js"></script>
    <script src="/Scripts/en-US.js"></script>
    <script src="/Scripts/AjaxHandler.js"></script>
      <script src="/Scripts/sweetalert2.min.js"></script>
    <script src="/Scripts/jquery.maskedinput.js"></script>
    <script src="/Scripts/VendorPortalV1.js"></script>
</asp:Content>
