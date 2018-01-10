$(document).ready(function () {
    if (user.Role.charAt(0).toUpperCase() + user.Role.slice(1) == "Admin") {
        $('.span3').hide();
        $('#MainForm').hide();
        $('#AddQuoteItemsHere').append('<div class="modal-header" style="margin-top:2px;"><div class="control-group admin-only" style="margin-top:10px"><div id="CustomerName" style="display:none;cursor:pointer;width:20%;"><i class="glyphicon glyphicon-edit" style="float:left;font-size: 20px;margin-right:10px;"></i><label id="CustomerNameLabel" relClient="" relCustNo="" reltierID=""></label></div><div id="CustomerSearchInfo"><label class="control-label" for="FrontCustomerSearch">Customer Search</label><div class="controls"><input type="text" class="input-medium" id="FrontCustomerSearch" /></div><span style="font-size: 10px;font-weight: bold;">*Select the customer name to only fill in customer, select address to fill in customer and address info</span><table id="FronttblCustomerSearchResults" style="width:50%;" class="table table-striped table-bordered table-condensed"><thead><tr><th>Customer</th><th>Address</th><th>City</th><th>State</th><th>Zip</th><th>Phone</th></tr></thead><tbody></tbody></table></div><div class="controls"><div id="PigeonCompanies" style="display:none;cursor:pointer;width:20%;"><i class="glyphicon glyphicon-edit" style="float:left;font-size: 20px;margin-right:10px;"></i><label id="PigeonCompanyLabel"></label></div><select class="input-medium" style="display:none;vertical-align: top;float: left;width:15%;" id="PigeonCompaniesDDL"></select></div><div id="CustomerEmail" style="display:none;cursor:pointer;width:20%;"><i class="glyphicon glyphicon-edit" style="float:left;font-size: 20px;margin-right:10px;"></i><label id="CustomerEmailLabel" relquoteID=""></label></div><div id="CustomerEmailInfo" style="display:none;"><input type="text" class="input-medium" style="display:none;vertical-align: top;float: left;" id="FronttxtEmail" placeholder="Customer Email" /><select class="input-medium" style="display:block;vertical-align: top;float: left;width:15%;" id="FrontDDLEmail"></select><i class="glypicon glyphicon-plus" style="width: 10%;vertical-align: top;margin-left:10px;font-size: 20px;" onclick="HideShowEmailDDL()"></i></div></div></div></div><br />');

        $('#FrontCustomerSearch').keyup(function () {
            if (($(this).val().length) > 0) {
                var urlMethod = "../CustomerManageWebService.asmx/searchCustomers";
                var json = { 'searchval': $(this).val(), 'client': user.Client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, function (msg) {
                    $("#FronttblCustomerSearchResults tbody").children().remove();
                    response = jQuery.parseJSON(msg.d);
                    if (response.length != 0 && response.length < 2) {//May need to remove this if statement but keep logic
                        $(response).each(function () {
                            $("#txtCustomerName").val(this.Company);
                            $("#txtCustNo").val(this.CustNo);
                            $("#txtTier").val(this.Tier);
                            $("#txtRepairFacility").val(this.Company);
                            $("#txtAddress").val(this.Address);
                            $("#txtCity").val(this.City);
                            $("#selState").val(this.State);
                            $("#txtZip").val(this.Zip);
                            $('#CustomerNameLabel').text(this.Company);
                            $('#CustomerNameLabel').attr("relClient", this.Client);
                            $('#CustomerNameLabel').attr("relCustNo", this.CustNo);
                            $('#CustomerNameLabel').attr("reltierID", this.Tier);
                        });

                        $('#CustomerName').show();
                        $("#CustomerSearchInfo").hide();
                        var Data = { "CustomerNo": $('#CustomerNameLabel').attr("relCustNo"), "Client": user.Client };
                        var url = "/OrderWebService.asmx/getEmailsByCustomerNo";
                        SendAjax(url, JSON.stringify(Data), function (response) {
                            var info = jQuery.parseJSON(response.d);
                                    //if (info.length>0)
                                    //{
                                        if (info[0].Company != null) {
                                            $('#PigeonCompaniesDDL').empty();
                                            var DDLHtml = "<option>Please Select Company</option>";
                                            $(info).each(function () {
                                                DDLHtml += "<option value='" + this.CustNo + "' data-rel='" + this.Client + "'>" + this.Company + "</option>";
                                            });
                                            $('#PigeonCompaniesDDL').append(DDLHtml);
                                            $('#PigeonCompaniesDDL').show();
                                            $('#PigeonCompaniesDDL').focus();
                                            ExpandSelect("PigeonCompaniesDDL");
                                        }
                                        else {
                                            $('#FrontDDLEmail').empty();
                                            var DDLHtml = "<option>Please Select Email</option>";
                                                $(info).each(function () {
                                                    DDLHtml += "<option reltierID='" + this.Tier + "'>" + this.SalesmanEmail + "</option>";
                                                });
                                            $('#FrontDDLEmail').append(DDLHtml);
                                            $('#CustomerEmailInfo').show();

                                            $('#FrontDDLEmail').focus();
                                            ExpandSelect("FrontDDLEmail");

                                        }
                                    //}
                                    //else
                                    //{
                                    //    $('#CustomerEmailInfo').show();
                                    //    $('#FrontDDLEmail').hide();
                                    //    $('#FronttxtEmail').show();
                                    //    $('#FronttxtEmail').focus();
                                    //    //HideShowEmailDDL();
                                    //}

                                });
                            }
                            else {
                            $.get('/Scripts/modules/templates/customer-search-template.htm', function (template) {
                                $.tmpl(template, response).appendTo('#FronttblCustomerSearchResults tbody');
                                //selection handler
                                $(".customer-search-address").click(function () {
                                    $("#txtCustomerName").val($(this).closest("tr").find(".customer-search-company").text());
                                    $("#txtCustNo").val($(this).closest("tr").find(".customer-search-company").attr("custno"));
                                    $("#txtTier").val($(this).closest("tr").find(".customer-search-company").attr("tier"));
                                    $("#txtRepairFacility").val($(this).closest("tr").find(".customer-search-company").text());
                                    $("#txtAddress").val($(this).closest("tr").find(".customer-search-address").text());
                                    $("#txtCity").val($(this).closest("tr").find(".customer-search-city").text());
                                    $("#selState").val($(this).closest("tr").find(".customer-search-state").text());
                                    $("#txtZip").val($(this).closest("tr").find(".customer-search-zip").text());

                                });
                                $(".customer-search-company").click(function () {
                                    $("#txtCustomerName").val($(this).closest("tr").find(".customer-search-company").text());
                                    $("#txtCustNo").val($(this).closest("tr").find(".customer-search-company").attr("custno"));
                                    $("#txtTier").val($(this).closest("tr").find(".customer-search-company").attr("tier"));
                                    $("#txtRepairFacility").val($(this).closest("tr").find(".customer-search-company").text());
                                    $("#txtAddress").val($(this).closest("tr").find(".customer-search-address").text());
                                    $("#txtCity").val($(this).closest("tr").find(".customer-search-city").text());
                                    $("#selState").val($(this).closest("tr").find(".customer-search-state").text());
                                    $("#txtZip").val($(this).closest("tr").find(".customer-search-zip").text());
                                    $('#CustomerNameLabel').text($(this).closest("tr").find(".customer-search-company").text());
                                    $('#CustomerNameLabel').attr("relClient", $(this).closest("tr").find(".customer-search-company").attr("client"));
                                    $('#CustomerNameLabel').attr("relCustNo", $(this).closest("tr").find(".customer-search-company").attr("custNo"));
                                    $('#CustomerNameLabel').attr("reltierID", $(this).closest("tr").find(".customer-search-company").attr("tier"));
                                    $('#CustomerName').show();
                                    $("#CustomerSearchInfo").hide();
                            var url = "/OrderWebService.asmx/getEmailsByCustomerNo";
                            var Data = { "CustomerNo": $(this).closest("tr").find(".customer-search-company").attr("custno"), "Client": user.Client };
                            SendAjax(url, JSON.stringify(Data), function (response) {
                                var info = jQuery.parseJSON(response.d);
                            //if (info.length > 0) {
                            if (info[0].Company != null) {
                                $('#PigeonCompaniesDDL').empty();
                                var DDLHtml = "<option>Please Select Company</option>";
                                $(info).each(function () {
                                    DDLHtml += "<option value='" + this.CustNo + "' data-rel='" + this.Client + "'>" + this.Company + "</option>";
                                });
                                $('#PigeonCompaniesDDL').append(DDLHtml);
                                $('#PigeonCompaniesDDL').show();
                                $('#PigeonCompaniesDDL').focus();
                                ExpandSelect("PigeonCompaniesDDL");
                            }
                            else {
                                $('#FrontDDLEmail').empty();
                                var DDLHtml = "<option>Please Select Email</option>";
                                $(info).each(function () {
                                    DDLHtml += "<option reltierID='" + this.Tier + "'>" + this.SalesmanEmail + "</option>";
                                });
                                $('#FrontDDLEmail').append(DDLHtml);
                                $('#CustomerEmailInfo').show();
                                $('#FrontDDLEmail').focus();
                                ExpandSelect("FrontDDLEmail");

                            }
                            //}
                            //else {
                            //    $('#CustomerEmailInfo').show();
                            //    $('#FrontDDLEmail').hide();
                            //    $('#FronttxtEmail').show();
                            //    $('#FronttxtEmail').focus();
                            //    HideShowEmailDDL();
                            //}

                            });
                        });
                    
                            });
                            }

                        });
                    }
                });

                $('#PigeonCompaniesDDL').change(function () {

                    $("#PigeonCompanyLabel").text($('#PigeonCompaniesDDL :selected').text());
                    $("#PigeonCompaniesDDL").hide();
                    $("#PigeonCompanies").show();
                    var url = "/OrderWebService.asmx/getEmailsByCustomerNo";
                    var url1="/OrderWebService.asmx/getCustomerInfoByCustomerNo"
                    var Data = { "CustomerNo": $('#PigeonCompaniesDDL :selected').val(), "Client": $('#PigeonCompaniesDDL :selected').attr("data-rel") };
                    SendAjax(url, JSON.stringify(Data), function (response) {
                        var info = jQuery.parseJSON(response.d);
                        $('#FrontDDLEmail').empty();
                        var DDLHtml = "<option>Please Select Email</option>";
                        $(info).each(function () {
                            DDLHtml += "<option reltierID='" + this.Tier + "'>" + this.SalesmanEmail + "</option>";
                        });
                        $('#FrontDDLEmail').append(DDLHtml);
                        $('#CustomerEmailInfo').show();
                        $('#FrontDDLEmail').focus();
                        ExpandSelect("FrontDDLEmail");

                    });
                    SendAjax(url1, JSON.stringify(Data), function (response) {
                        var info = jQuery.parseJSON(response.d);
                        $(info).each(function () {
                            //$("#txtCustomerName").val(this.Company);
                            $("#txtCustNo").val($('#CustomerNameLabel').attr("relCustNo"));
                            $("#txtTier").val(this.Tier);
                            $("#txtRepairFacility").val(this.Company);
                            $('#txtServicer').val(this.Company);
                            $("#txtAddress").val(this.Address);
                            $("#txtCity").val(this.City);
                            $("#selState").val(this.State);
                            $("#txtZip").val(this.Zip);
                            $("#txtPhone").val(this.Phone);
                            //$("txtContact").val(this.Contact);
                            //$('#CustomerNameLabel').text(this.Company);
                            // $('#CustomerNameLabel').attr("relCustNo", this.Client);
                            //$('#CustomerNameLabel').attr("reltierID", this.Tier);
                        });
                    });
                });

                $('#FrontDDLEmail').change(function () {
                    $('#CustomerEmailLabel').text($('#FrontDDLEmail :selected').text());
                    $('#CustomerEmailInfo').hide();
                    $('#CustomerEmail').show();
                    $('#txtCustomer').val($('#CustomerNameLabel').text());
                    $('#txtAdjusterEmail').val($('#CustomerEmailLabel').text());
                    $('#MainForm').show();
                    $('.span3').show();

                });

                $('#FronttxtEmail').blur(function () {
                    var email = $('#FronttxtEmail').val();
                    if (email == null || email == "") {
                        swal({ title: 'Email Required', type: 'warning' });
                        return;
                    }
                    if (email.match(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
                        if (email.substring(email.indexOf('@') + 1).match(/ckautoparts.com/i) || email.substring(email.indexOf('@') + 1).match(/wisinspections.com/i)) {
                            swal({ title: 'Customers Email Address', text: 'Not yours...', type: 'warning' });
                            return;
                        }
                        $('#CustomerEmailLabel').text($('#FronttxtEmail').val());
                        $('#CustomerEmailInfo').hide();
                        $('#CustomerEmail').show();
                        $('#txtCustomer').val($('#CustomerNameLabel').text())
                        $('#MainForm').show();
                        $('.span3').show();
                        return;
                    }

                    swal({ title: 'Invalid Email Address', type: 'warning' });

                });

                $('#CustomerName').click(function () {
                    $('.span3').hide();
                    $('#CustomerName').hide();
                    $("#CustomerSearchInfo").show();
                    $('#CustomerEmail').hide();
                    $('#CustomerEmailInfo').hide();
                });

                $('#CustomerEmail').click(function () {
                    $('.span3').hide();
                    $('select option:first-child').attr("selected", "selected");
                    $('#CustomerEmail').hide();
                    $('#CustomerEmailInfo').show();
                });

                $('#PigeonCompanies').click(function () {
                    $('.span3').hide();
                    $('select option:first-child').attr("selected", "selected");
                    $('#PigeonCompanies').hide();
                    $('#PigeonCompaniesDDL').show();
                    $('#CustomerEmailInfo').hide();
                    $('#CustomerEmail').hide();
                });
        //    }
        //});


        $('#FrontCustomerSearch').focus();
    }
});

function HideShowEmailDDL() {
    if ($('#FrontDDLEmail').css('display') == "block") {
        $('#FrontDDLEmail').hide();
        $('#FronttxtEmail').show();
    }
    else {
        $('#FrontDDLEmail').show();
        $('#FronttxtEmail').hide();
    }

}
