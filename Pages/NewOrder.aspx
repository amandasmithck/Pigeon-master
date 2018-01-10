<%@ Page Language="vb" MasterPageFile="~/Pages/Pigeon.Master" AutoEventWireup="false"  CodeBehind="NewOrder.aspx.vb" Inherits="Pigeon.NewOrder" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script id="customerTemplate" type="x-jquery-tmpl">
       <option value="${CustNo}">${Company}</option>
    </script>
    <script id="thirdpartyTemplate" type="x-jquery-tmpl">
       <option value="${CompanyID}">${Company}</option>
    </script>
     <script id="makeTemplate" type="x-jquery-tmpl">
       <option value="${Make}">${Make}</option>
    </script>
    <script id="modelTemplate" type="x-jquery-tmpl">
       <option value="${Model}">${Model}</option>
    </script>
    <script id="partTemplate" type="x-jquery-tmpl">
       <option value="${PartDescGroup}">${Part}</option>
    </script>
    <script id="vendorTemplate" type="x-jquery-tmpl">
       <option value="${CompanyID}">${Company}</option>
    </script>
 <%--   <link href="/Styles/bootstrap.css" rel="stylesheet" />--%>
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">
    <script src="/Scripts/Quotes-v1.6-.js"></script>
    <div id="AddQuoteItemsHere"></div>
    <div id="MainForm" class="container-fluid">
        <div class="row-fluid">
            <div class="screen">
                <h3>Manual Order Entry</h3>
          <label class="radio">
            <input type="radio" name="optionsRadios" id="optQuote" value="Quote"> Quote</label>
          <label class="radio">
            <input type="radio" name="optionsRadios" id="optOrder" value="Order">Place Order</label>
                <h5>Only use this form when ordering parts that we do not have a lookup page or when special 3rd party billing is needed</h5>
                <form id="order-form" name="order-form">
                    <table class="normal bgGray">
                        <thead>
                            <tr>
                                <td class="spacer" colspan="2"></td>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <td class="spacer" colspan="2"></td>
                            </tr>
                            <tr>
                                <td class="footer" colspan="2"></td>
                            </tr>
                        </tfoot>
                        <tbody class="colorGrayDark">
                            <tr>
                                <td width="25%">
                                        Customer
                                    </td>
                                    <td width="75%" style="cursor:pointer;width:20%;">
                                     <input  type="text" class="input-medium" id="txtCustomer" disabled/><input type="text" style="display:none" id="txtCustNo" />
                                </td>
                            </tr>
                       <%--     <tr class="Order">
                                <td width="25%">
                                        3rd Party Bill(if any)
                                    </td>
                                    <td width="75%">
                                        <select  id="selThirdParty">
                                        <option value=""></option>
                                        </select>
                                </td>
                            </tr>--%>
                            <tr class="Order">
                                <td width="25%">
                                    Adjuster
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtAdjuster" size="40" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Both">
                                <td width="25%">
                                    Adjuster Email
                                </td>
                                <td width="75%">
                                      <input type="text" class="required input-medium" id="txtAdjusterEmail" disabled />
                                </td>
                            </tr>
                            <tr id="quoteOnly" style="display:none;">
                                 <td width="25%">
                                    Part Type
                                </td>
                                <td width="75%">
                                    <select id="partTypesDDL">
                                        <option>Please Select Part Type</option>
                                        <option value="<%=Pigeon.Enums.PartTypes.Engine %>">Engine</option>
                                        <option value="<%=Pigeon.Enums.PartTypes.Transmission %>">Transmission</option>
                                        <option value="<%=Pigeon.Enums.PartTypes.ManualTransmission %>">Manual Transmission</option>
                                        <option value="<%=Pigeon.Enums.PartTypes.TransferCase %>">Transfer Case</option>
                                        <option value="<%=Pigeon.Enums.PartTypes.Differential %>">Differential</option>
                                    </select>
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Contract No.
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtContractNo" size="40" value="">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Authorization No.
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtAuthNo" size="40" value="">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Vehicle Owner
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtOwner" size="40" value="">
                                </td>
                            </tr>
                            <tr class="Both">
                                <td width="25%">
                                    Year
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtYear" size="10" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Both">
                                <td width="25%">
                                        Make
                                    </td>
                                    <td width="75%">
                                        <select  id="selMake" class="required">
                                        <option value=""></option>
                                        </select>
                                </td>
                            </tr>
                            <tr class="Both">
                                <td width="25%">
                                        Model
                                    </td>
                                    <td width="75%">
                                        <%--<select  id="selModel" class="required">
                                        <option value=""></option>
                                        </select>--%>
                                        <input type="text" id="txtModel" size="40" value="" class="required" >
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    VIN #
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtVin" size="40" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();" class="required">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Mileage
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtMileage" size="40" value="" class="required" >
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                        Drive
                                    </td>
                                    <td width="75%">
                                        <select  id="selDrive">
                                            <option value=""></option>
                                            <option value="FWD">FWD</option>
                                            <option value="RWD">RWD</option>
                                            <option value="AWD">AWD</option>
                                            <option value="2WD">2WD</option>
                                            <option value="4WD">4WD</option>
                                        </select>
                                    </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                        Transmission
                                    </td>
                                    <td width="75%">
                                        <select  id="selTransmission">
                                            <option value=""></option>
                                            <option value="Automatic">Automatic</option>
                                            <option value="Manual">Manual</option>
                                        </select>
                                    </td>
                            </tr>
<%--                            <tr>
                                <td width="25%">
                                        Transmission Type
                                    </td>
                                    <td width="75%">
                                        <select  id="selTransmissionType">
                                            <option value=""></option>
                                            <option value="Automatic 3 Speed">Automatic 3 Speed</option>
                                            <option value="Automatic 4 Speed">Automatic 4 Speed</option>
                                            <option value="Automatic 5 Speed">Automatic 5 Speed</option>
                                            <option value="Automatic 6 Speed">Automatic 6 Speed</option>
                                            <option value="Automatic 7 Speed">Automatic 7 Speed</option>
                                            <option value="Manual 3 Speed">Manual 3 Speed</option>
                                            <option value="Manual 4 Speed">Manual 4 Speed</option>
                                            <option value="Manual 5 Speed">Manual 5 Speed</option>
                                            <option value="Manual 6 Speed">Manual 6 Speed</option>
                                            <option value="Manual 7 Speed">Manual 7 Speed</option>
                                            <option value="CVT">CVT</option>
                                        </select>
                                    </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                        Engine Type
                                    </td>
                                    <td width="75%">
                                        <select  id="selEngineType">
                                            <option value=""></option>
                                            <option value="Longblock">Longblock</option>
                                            <option value="Complete">Complete</option>
                                        </select>
                                    </td>
                            </tr>--%>
                             <tr class="Order">
                                <td width="25%">
                                    Failure
                                </td>
                                <td width="75%">
                                    <textarea id="txtFailure" rows="5"></textarea>
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Servicer Site
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtServicer" size="40" value="" class="required">
                                </td>
                            </tr>
                            <%--<tr>
                                <td width="25%">
                                    Labor Rate
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtLabor" size="40" value="" >
                                </td>
                            </tr>--%>
                            <tr class="Order">
                                <td width="25%">
                                    Address
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtAddress" size="40" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    City
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtCity" size="40" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    State
                                </td>
                                <td width="75%">
                                    <select id="selState" class="required">
                                    <option value=""></option>
                                    <option value="AL">Alabama</option>
                                    <option value="AK">Alaska</option>
                                    <option value="AZ">Arizona</option>
                                    <option value="AR">Arkansas</option>
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
                                </select>
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Zip
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtZip" size="40" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Phone
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtPhone" size="40" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Contact
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtContact" size="40" value="">
                                </td>
                            </tr>
                            <tr class="Both">
                                <td width="25%">
                                    Part
                                </td>
                                <td width="75%">
                                    <select  id="selPart" >
                                    <option value=""></option>
                                    <%--<input type="text" id="txtPart" size="40" value="" class="required">--%>
                                    </select>
                                </td>
                            </tr>
                            <tr class="other-part" style=" display:none">
                                <td width="25%">
                                    Describe Part
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtOtherPart" size="40" >
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Quantity
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtQuantity" size="40" value="1" maxlength="2">
                                </td>
                            </tr>
                            <tr class="Both">
                                <td width="25%">
                                    Sell Price
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtSellPrice" size="40" value="" class="required">
                                </td>
                            </tr>   
                            <tr class="Order">
                                <td width="25%">
                                    Additional Parts
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtAdditional" size="40" value="0" maxlength="2">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                        Vendor
                                    </td>
                                    <td width="75%">
                                        <select  id="selVendor" class="required">
                                        <option value=""></option>
                                        </select>
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    ETA Date
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtETADate" size="40" value="" class="required">
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    Warranty
                                </td>
                                <td width="75%">
                                    <select id="selWarranty">
                                        <option value=""></option>
                                        <option Value="3/3">3 months/3,000 miles</option>
                                        <option Value="6/6">6 months/6,000 miles</option>
                                        <option Value="12/12">12 months/12,000 miles</option>
                                        <option Value="12/un">12 months/unlimited miles</option>
										<option Value="24/24">24 months/24,000 miles</option>
										<option Value="36/36">36 months/36,000 miles</option>
                                        <option Value="36/100">36 months/100,000 miles</option>
                                        <option Value="36/un">36 months/unlimited miles</option>
										<option>EOC</option>
                                    </select>
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    End of Contract Date
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtWarrantyDate" size="40" value="" >
                                </td>
                            </tr>
                            <tr class="Order">
                                <td width="25%">
                                    End of Contract Miles
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtWarrantyMileage" size="40" value="" >
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table class="bare">
                        <tbody>
                            <tr>
                                <td>
                                    <input name="Submit" type="button" class="btn btn-large btn-primary" value="Submit" id="submit">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </form>
            </div>
        </div>
    </div>

    <div class="dialog-message" title="Confirmation">
	    <p class="message"></p>
    </div>

    <script type="text/javascript" >
      
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
                    alert(err.Message);
                }
            });
        }
        var user = <%= Session("UserModel") %>;


        $('document').ready(function () {

            $('input[name=optionsRadios]').change(function () {
                if($('input[name=optionsRadios]:checked').val()=="Order")
                {
                    $('#quoteOnly').hide();
                    $('#selWarranty').attr("class","required");
                    $('.Both').show()
                    $('.Order').show();
                }
                else
                {
                    $('#quoteOnly').show();
                    $('#selWarranty').attr("class","");
                    $('.Order').hide();
                    $('.Both').show();
                }
            });
      
            var urlMethod = "../CustomerManageWebService.asmx/GetAutonation";
            var json = {};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selThirdParty').append($("#thirdpartyTemplate").tmpl(response));
            });

            var urlMethod = "../OrderWebService.asmx/GetMakes";
            var json = {};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selMake').append($("#makeTemplate").tmpl(response));
            });

            //var urlMethod = "../OrderWebService.asmx/GetModels";
            //var json = {};
            //var jsonData = JSON.stringify(json);
            //SendAjax(urlMethod, jsonData, function (msg) {
            //    response = jQuery.parseJSON(msg.d);
            //    $('#selModel').append($("#modelTemplate").tmpl(response));
            //});

            var urlMethod = "../OrderWebService.asmx/GetPartTypes";
            var json = {};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selPart').append($("#partTemplate").tmpl(response));
                //var selSource=[];
                
                //$(response).each(function () {
                //   var source={};
                //    source.id=$(this)[0].Part;
                //    source.name=$(this)[0].Part;
                //    selSource.push(source);
                //});
                
                //$('#txtPart').typeahead({
                //    source:selSource
                //});

                
            });

            var urlMethod = "../OrderWebService.asmx/GetVendors";
            var json = {};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#selVendor').append($("#vendorTemplate").tmpl(response));
                $('#selVendor').val(10827)
            });

            //$('#txtETADate').datepicker({
            //    format: 'mm-dd-yyyy'

            //});

            $('#txtETADate').datepicker()
              .on('changeDate', function(ev){
                  $('#txtETADate').datepicker('hide');
            });

            $("#txtWarrantyDate").mask("99/99/9999");
            $("#txtPhone").mask("(999) 999-9999? x99999");
            $("#txtVin").mask("*****************", { placeholder: "" });

            //init vin counter
            $("#txtVin").counter({
                count: 'up',
                goal: 18
            });

            $('#selPart').change(function() {
                $(this).val()=="Other" || $(this).val()=="Used Other" ? $('.other-part').show() : $('.other-part').hide()
                $(this).val()=="Other" || $(this).val()=="Used Other" ?  $('#txtOtherPart').addClass('required') : $('#txtOtherPart').removeClass('required')
            });

            //$(".dialog-message").dialog({
            //    autoOpen: false,
            //    modal: true,
            //    buttons: {
            //        Ok: function () {
            //            location.reload(true);
            //        }
            //    }
            //});

            $('#submit').click(function () {
                
                if ($('input:visible, select').valid()) {
                    if($('#txtCustNo').val()==''){
                        $(".dialog-message .message").html("Dude, did you really just try to put an order in without a customer??? #RookieMove #HowLongHaveYouWorkedHere #AreYouReallyStillReadingThis");
                        $(".dialog-message").dialog(
                            {
                                autoOpen: true,
                                buttons:
                                {
                                    "My Bad": function () {
                                        $(this).dialog('close');
                                      
                                    }
                                }
                            });
                        return false;
                    } else {
                        if($('input[name=optionsRadios]:checked').val()=="Quote")
                        {
                            var urlMethod = "../OrderWebService.asmx/NewManualQuote";
                            var json = {
                                'customerNo': $('#txtCustNo').val()
                               , 'customerEmail':$('#txtAdjusterEmail').val()
                               , 'partTypeID':$('#partTypesDDL :selected').val()
                               , 'year': $('#txtYear').val()
                               , 'make': $('#selMake').val()
                               , 'model': $('#txtModel').val()
                               , 'part': $('#selPart option:selected').text()
                               , 'sellPrice': $('#txtSellPrice').val()
                               , 'enteredBy': user.UserName
                               , 'client': user.Client
                            };
                        }
                        else
                        {
                            var urlMethod = "../OrderWebService.asmx/NewManualOrder";
                            var json = {
                                'customer': $('#txtCustNo').val()
                                , 'thirdparty': ""//$('#selThirdParty').val()
                                , 'adjuster': $('#txtAdjuster').val()
                                , 'adjusteremail':$('#txtAdjusterEmail').val()
                                , 'contractno': $('#txtContractNo').val()
                                , 'authno': $('#txtAuthNo').val()
                                , 'owner': $('#txtOwner').val()
                                , 'year': $('#txtYear').val()
                                , 'make': $('#selMake').val()
                                , 'model': $('#txtModel').val()
                                , 'vin': $('#txtVin').val()
                                , 'mileage': $('#txtMileage').val()
                                , 'drive': $('#selDrive').val()
                                , 'transmission': $('#selTransmission').val()
                                , 'failure': $('#txtFailure').val()
                                , 'servicer': $('#txtServicer').val()
                                , 'address': $('#txtAddress').val()
                                , 'city': $('#txtCity').val()
                                , 'state': $('#selState').val()
                                , 'zip': $('#txtZip').val()
                                , 'phone': $('#txtPhone').val()
                                , 'contact': $('#txtContact').val()
                                , 'part': $('#selPart option:selected').text()
                                , 'partdescgroup': $('#selPart option:selected').val()
                                , 'quantity': $('#txtQuantity').val()
                                , 'sellprice': $('#txtSellPrice').val()
                                , 'additional': $('#txtAdditional').val()
                                , 'vendor': $('#selVendor').val()
                                , 'etadate': $('#txtETADate').val()
                                , 'warranty': $('#selWarranty').val()
                                , 'warrantydate': $('#txtWarrantyDate').val()
                                , 'warrantymileage': $('#txtWarrantyMileage').val()
                                , 'otherpart': $('#txtOtherPart').val()
                                , 'enteredby': user.UserName
                            };
                        }
                            var jsonData = JSON.stringify(json);
                            SendAjax(urlMethod, jsonData, function (msg) {
                                response = jQuery.parseJSON(msg.d);
                                if (msg.d == false) {
                                    $(".dialog-message .message").html("ERROR processing info. Check to see if order was entered successfully before clicking submit again.");
                                    $(".dialog-message").dialog("open");
                                }else{
                                    if($('input[name=optionsRadios]:checked').val()=="Quote")
                                    {
                                        $(".dialog-message .message").html("Quote has been entered successfully");
                                    }
                                    else
                                    {
                                        $(".dialog-message .message").html("Order number " + msg.d + " has been entered successfully");
                                    }
                                    
                                    $(".dialog-message").dialog("open"); 
                                }
                            });
                    }
                }
            });
        });
    </script>
</asp:Content>
