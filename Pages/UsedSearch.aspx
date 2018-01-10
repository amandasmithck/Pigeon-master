<%@ Page Title="Used Search" Language="vb" MasterPageFile="~/Pages/Pigeon.Master" AutoEventWireup="false" CodeBehind="UsedSearch.aspx.vb" Inherits="Pigeon.UsedSearch" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script id="partTemplate" type="x-jquery-tmpl">
       <option value="${PartDescGroup}">${Part}</option>
    </script>
    <div id="used-request" class="container-fluid">
        <div class="row-fluid">
            <div class="screen">
                <h3>Used Powertrain Assemblies</h3>
                <h5>Please call 800-981-7358 or fill out the form below</h5>
                <form id="used-form" name="used-form">
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
                                    Type
                                </td>
                                <td width="75%">
                                    <label class="radio">
                                      <input type="radio" name="optionsRadios" id="optQuote" value="Quote" checked>
                                      Quote
                                    </label>
                                    <label class="radio">
                                      <input type="radio" name="optionsRadios" id="optOrder" value="Order">
                                      Place Order
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    Year
                                </td>
                                <td width="75%">
                                    <input type="text" id="year" size="5" class="required" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    Make
                                </td>
                                <td width="75%">
                                    <input type="text" id="make" class="required" size="20" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    Model
                                </td>
                                <td width="75%">
                                    <input type="text" id="model" class="required" size="20" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    VIN #
                                </td>
                                <td width="75%">
                                    <input type="text" id="vin" class="required" size="40" value="" onkeyup="this.value=this.value.substring(0,25).toUpperCase();">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    Part
                                </td>
                                <td width="75%">
                                    <select  id="part" class="required">
                                    <option value=""></option>
                                    </select>
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Quoted Price
                                </td>
                                <td width="75%">
                                    <input type="number" id="txtprice" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Authorization No./PO
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtauth" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Contract No.
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtcontract" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Customer Name
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtowner" class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Mileage
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtmileage" class="required" size="40" value="">
                                </td>
                            </tr>
                            

                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Warranty
                                </td>
                                <td width="75%">
                                    <select class="quote" id="selWarranty">
                                    <option value=""></option>
                                    <option value="12/12">12 month/12,000</option>
                                    <option value="EOC">EOC</option>
                                    </select>
                                </td>
                            </tr>
                            <tr class="eoc" style="display:none">
                                <td width="25%">
                                    EOC Date
                                </td>
                                <td width="75%">
                                    <input type="text" id="txteocdate" size="40" value="" >
                                </td>
                            </tr>
                            <tr class="eoc" style="display:none">
                                <td width="25%">
                                    EOC Mileage
                                </td>
                                <td width="75%">
                                    <input type="text" id="txteocmileage" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Repair Facility
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtfacility" class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr  class="quote" style="display:none">
                                <td width="25%">
                                    Address
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtaddress" class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    City
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtcity"  class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    State
                                </td>
                                <td width="75%">
                                    <select class="quote" id="selState" class="required">
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
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Zip
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtzip" class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Phone
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtphone" class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr class="quote" style="display:none">
                                <td width="25%">
                                    Contact
                                </td>
                                <td width="75%">
                                    <input type="text" id="txtcontact" class="required" size="40" value="">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    Comments
                                </td>
                                <td width="75%">
                                    <textarea rows="5" id="comments" cols="50"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    Your email
                                </td>
                                <td width="75%">
                                    <input type="email" class="required" size="40" id="txtemail" />
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
        function UsedRequest() {
            var urlMethod = "../UsedWebService.asmx/UsedRequest";

            var json = {
                'type': $('input[name=optionsRadios]:checked').val()
                , 'price': $('#txtprice').val()
                , 'auth': $('#txtauth').val()
                , 'contract': $('#txtcontract').val()
                , 'owner': $('#txtowner').val()
                , 'mileage': $('#txtmileage').val()
                , 'year': $('#year').val()
                , 'make': $('#make').val()
                , 'model': $('#model').val()
                , 'facility': $('#txtfacility').val()
                , 'address': $('#txtaddress').val()
                , 'city': $('#txtcity').val()
                , 'state': $('#selState').val()
                , 'zip': $('#txtzip').val()
                , 'phone': $('#txtphone').val()
                , 'contact': StripString($('#txtcontact').val())
                , 'vin': $('#vin').val()
                , 'part': $('#part option:selected').text()
                , 'partdescgroup': $('#part option:selected').val()
                , 'warranty': $('#selWarranty').val()
                , 'eocdate': $('#txteocdate').val()
                , 'eocmileage': $('#txteocmileage').val()
                , 'comments': $('#comments').val()
                , 'name': user.UserName
                , 'email': $('#txtemail').val()
                , 'client': user.Client
            }
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, ReturnUsedRequest);
        }

        function ReturnUsedRequest(msg) {
            response = jQuery.parseJSON(msg.d);
            
            if (response == true) {
                $(".dialog-message .message").html("Your used powertrain assembly part request has been received and someone will contact you shortly.");
                $(".dialog-message").dialog("open");
            } else {
                if (response == false) {
                    $(".dialog-message .message").html("ERROR processing info. Please try again");
                    $(".dialog-message").dialog("open");
                } else {
                    $(".dialog-message .message").html("Order number " + msg.d + " has been received and will be processed shortly");
                    $(".dialog-message").dialog("open");
                }
            }
            
        }

        $('document').ready(function () {

            $('#txteocdate').datepicker()
              .on('changeDate', function(ev){
                  $('#txteocdate').datepicker('hide');
              });

            $(".dialog-message").dialog({
                autoOpen: false,
                modal: true,
                buttons: {
                    Ok: function () {
                        //location.reload(true);
                    }
                }
            });

            $('#submit').click(function () {
                if ($('#used-request input:visible, #used-request select:visible').valid()) {
                    UsedRequest();
                    return false;
                }
                
               
            });
           
            $('.tab-warranties').hide();



            $('input[name=optionsRadios]').change(function () {
                $('input[name=optionsRadios]:checked').val()=="Quote" ? $('.quote').hide() : $('.quote').show()
            });

            $('#selWarranty').change(function() {
                $(this).val()=="EOC" ? $('.eoc').show() : $('.eoc').hide()
            });

            var urlMethod = "../UsedWebService.asmx/GetPartTypes";
            var json = {};
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                response = jQuery.parseJSON(msg.d);
                $('#part').append($("#partTemplate").tmpl(response));
            });


        });
        function StripString(data)
        {
            
            return data.replace(/'/g,'');
        }
    </script>
</asp:Content>