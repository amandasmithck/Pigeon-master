$('document').ready(function () {
    GetTiers();
    //$('.button').button();
    $("#csv-example").dialog({
        autoOpen: false,
        resizable: false,
        width: 1020,
        modal: true,
        buttons: {
            'OK': function () {
                $(this).dialog("close");
            }
        }
    });

    $('#AdminDDL').change(function () {
        getAdminUsersInfo();
    });

    $("#btnCSVExample").click(function () {
        $("#csv-example").dialog("open");
    });

    $(".chzn-select").chosen().hide();
    $('.chzn-container').hide().prev().prev().hide();
    $('#edtCustomerType, #txtCustomerType, #emlCustomerType').change(function () {
       // if (this.value == "Dealer") {
            $(this).parent().find('.chzn-container').fadeIn().prev().prev().fadeIn();
       // } else {
        //    $(this).parent().find('.chzn-container').hide().prev().prev().hide();
       // }
    });

    $('#btnGenerateEmail').click(function () {
        GenerateEmail($('#emlCustomerType').val(), $('#emlCompanyMakes').val());
    });

    //global array used in many places (creation of html blocks, request and saving of markup)
    var makegroups = new Array("GM", "Ford", "Chrysler", "Honda", "Toyota", "Nissan", "Lexus", "Mercedes");

    $(makegroups).each(function () {
        var html = '<h3>' + this + '</h3> \
                    <div style="width:48%;float:left;"> \
                        <label style="font-weight:bold; width: 100%;float: left;">Discount Type</label> \
                        <select class="MGDiscountType' + this + '" style="padding:4px;width: 100%;font-size: 16px;"> \
                            <option></option> \
                            <option value="Dealer">Dealer</option> \
                            <option value="List">List</option> \
                        </select> \
                    </div> \
                    <div style="width:48%; margin-left: 2%; float:left;"> \
                        <label style="font-weight:bold; width: 100%;float: left;">Amount</label> \
                        <input class="txtMGDiscount' + this + '" style="padding:4px;font-size:16px;width:60px;" type="text" /> \
                    </div><div class="clear"></div>';

        $('.discounts-group .btnSaveMG:nth(1)').before(html);
    });
    $('.btnSaveMG').click(function () {
        $(makegroups).each(function () {
            var source = $('.MGDiscountType' + this + ' option:selected').val();
            var percent = $('.txtMGDiscount' + this + '').val();
            var markup = (percent.slice(0, -1) * 1) / 100;

            SaveMarkupGroup($('#ctl00_MainContent_cboCustomer').val(), this, source, markup);
        });
    });

    //$("select.uniform").uniform();
    $("#slider").slider({
        value: 0,
        min: -100,
        max: 100,
        slide: function (event, ui) {
            $("#amount").val(ui.value + "%");
        }
    });

    //  $("#amount").val($("#slider").slider("value") + "%");

    $('#amount').change(function () {
        var newamt = $("#amount").val().split("%");

        $('#slider').slider("value", newamt[0])
    });

    $('#ctl00_MainContent_cboCustomer').change(function () {
        // $('.main').mask("Loading...");
        $('.success').hide();
        $('#customer-name').html($(this).find('option:selected').html());
        $('#loading-overlay').css({ 'height': $('.main').height(), 'width': $('.main').width() }).show().find('img').css({ 'position': 'absolute', 'top': ($('.main').height() / 2 - 16), 'left': ($('.main').width() / 2 - 16) });
        setTimeout(function () {
            $('#loading-overlay').hide();

        }, 1000);
        $('#markup_saved').html('');
        GetUsers($(this).val());
        $('#btnNewUser').show();
        $('#btnDeleteUser').show();
        GetMarkupAll($('#ctl00_MainContent_cboCustomer').val());
        GetMarkupIndividual($('#ctl00_MainContent_cboCustomer').val(), makegroups);
        GetCustomerInfo($('#ctl00_MainContent_cboCustomer').val());
        // $('.main').unmask();
    });

    $('#cboMake').change(function () {
        GetMarkup($('#ctl00_MainContent_cboCustomer').val(), $('#cboMake').val())
        $('#markup_saved').html('');
    });

    $('#btnSaveAll').click(function () {
        //SaveMarkup($('#ctl00_MainContent_cboCustomer').val(), $('#cboMake').val(), $('#discount_type').val(), $("#slider").slider("value"))
        SaveMarkupAll($('#ctl00_MainContent_cboCustomer').val(), $('#discount_type').val(), $("#slider").slider("value"))
    });

    $('#cboUser').change(function () {

        $('#activesuccess').html('')
        $('#emailsuccess').html('')
        $('#passwordsuccess').html('')
        $('#newpassword').val('');
        $('#newemail').val('');

        if ($(this).val() != '') {
            GetUser($(this).val());
        }
        else {
            $('#oldpassword').val('');
            $('#oldemail').val('');
            $('#active').prop("checked", false);
        }
    });
    $('#btnPassword').click(function () { ChangePassword($('#cboUser').val(), $('#newpassword').val()); });
    $('#btnEmail').click(function () { ChangeEmail($('#cboUser').val(), $('#newemail').val()); });
    $('#btnUserComp').click(function () { ChangeUserComp($('#cboUser').val(), $('#ctl00_MainContent_selUserComp').val()); });
    $('#btnActive').click(function () { ChangeActive($('#cboUser').val(), $('#active').prop('checked')); });
    $('#btnCanOrder').click(function () { ChangeCanOrder($('#cboUser').val(), $('#canorder').prop('checked')); });
    $('#btnTier').click(function () { ChangeTier($('#cboUser').val(), $('#selTier').val()); });
    $('#txtCustomerNo').blur(function () { GoodCustomerNo($(this).val(), 'txt'); });
    $('#edtCustomerNo').blur(function () { GoodCustomerNo($(this).val(), 'edt'); });
    $('#txtUserName').blur(function () { GoodUserName($(this).val()); });
    $('#txtAdminUserName').blur(function () { GoodAdminUserName($(this).val()); });

    $('#btnNewCustomer').click(function () {
        $("#new-customer-success").val('');
        $("#new-customer-error").val('');
        $("#new-customer").dialog({
            autoOpen: false,
            resizable: false,
            height: 620,
            width: 380,
            modal: true,
            buttons: {
                "Create": function () {
                    if ($('#new-customer form input').valid()) {
                        SaveNewCustomer();

                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#new-customer").dialog("open");
    });

    $('#btnDeleteCustomer').click(function () {
        $("#delete-company").dialog({
            autoOpen: false,
            resizable: false,
            modal: true,
            buttons: {
                "Delete": function () {
                    DeleteCustomer();
                    $(this).dialog("close");
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        });
        $("#delete-company").dialog("open");
    });

    $('#btnNewUser').click(function () {
        $("#new-user-success").val('');
        $("#new-user-error").val('');
        $("#new-user").dialog({
            autoOpen: false,
            resizable: false,
            height: 425,
            width: 380,
            modal: true,
            buttons: {
                "Create": function () {
                    if ($('#new-user form input').valid()) {
                        SaveNewUser();

                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#new-user").dialog("open");
    });

    $('#btnEditAdminUser').click(function () {
        $("#updateAdminUser").dialog({
            autoOpen: false,
            resizable: false,
            height: 425,
            width: 380,
            modal: true,
            buttons: {
                "Update": function () {
                    UpdateAdmin();
                    $('#SuccessDialog').dialog({
                        autoOpen: false,
                        resizable: false,
                        height: 200,
                        width: 200,
                        modal: true,
                        buttons: {
                            "Done":function()
                            {
                                $('#Success').hide();
                                $(this).dialog("close");
                                $('#AdminDDL').empty();
                                getAllAdminUsers();
                                $('#AdminDDL').val(0);
                                $('#AdminUsersName').val("");
                                $('#AdminEmail').val("");
                                $('#AdminPassword').val("");
                            }
                        }});
                    $('#Success').text("Admin Info Updated");
                    $('#Success').show();
                    $(this).dialog("close");
                    $('#SuccessDialog').dialog("open");
                },
                "Delete": function () {
                    DeleteAdmin();
                    $('#SuccessDialog').dialog({
                        autoOpen: false,
                        resizable: false,
                        height: 200,
                        width: 200,
                        modal: true,
                        buttons: {
                            "Done": function () {
                                $('#Success').hide();
                                $(this).dialog("close");
                                $('#AdminDDL').empty();
                                getAllAdminUsers();
                                $('#AdminDDL').val(0);
                                $('#AdminUsersName').val("");
                                $('#AdminEmail').val("");
                                $('#AdminPassword').val("");
                            }
                        }
                    });
                    $('#Success').text("Admin Deleted");
                    $('#Success').show();
                    $(this).dialog("close");
                    $('#SuccessDialog').dialog("open");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        getAllAdminUsers();
        $("#updateAdminUser").dialog("open");
    });

    $('#btnNewAdminUser').click(function () {
        $("#new-admin-user-success").val('');
        $("#new-admin-user-error").val('');
        $("#new-admin-user").dialog({
            autoOpen: false,
            resizable: false,
            height: 425,
            width: 380,
            modal: true,
            buttons: {
                "Create": function () {
                    if ($('#new-admin-user form input').valid()) {
                        SaveNewAdminUser();

                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#new-admin-user").dialog("open");
    });

    $('#btnDeleteUser').click(function () {
        DeleteUser();
    });

    $('#discount-radios input[name=discount-type]').change(function () {
        switch (this.value) {
            case "all":
                $('.discounts-group').hide();
                $('.discounts-all').fadeIn(200);
                break;
            case "groups":
                $('.discounts-all').hide();
                $('.discounts-group').fadeIn(200);
                break;
        }
    });

    $('#btnUpdateInfo').click(function () {
        UpdateCustomerInfo();
    });
    $('#btnSaveMG').click(function () { });

    $("#btnCancelShipment").click(function () {

    });

});

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

function SaveNewUser() {
    var urlMethod = "../CustomerManageWebService.asmx/SaveNewUser";

    var json = { 'username': $('#txtUserName').val(), 'custno': $('#ctl00_MainContent_cboCustomer').val(), 'email': $('#txtEmail').val(), 'password': $('#txtPassword').val(), 'tierid': $('#selTierNew').val(), 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnSaveNewUser);
}


function ReturnSaveNewUser(msg) {
    //console.log(msg.d);
    if (msg.d == true) {
        $("#new-user-success").html('New User Saved');
        $("#new-user").dialog("close");
        GetUsers($('#ctl00_MainContent_cboCustomer').val())
        $('#new-user input[type=text]').val('')
    }
    else {
        $("#new-user-error").html('Error Saving User');
    }
}

function SaveNewAdminUser() {
    var urlMethod = "../CustomerManageWebService.asmx/SaveNewAdminUser";

    var json = { 'username': $('#txtAdminUserName').val(), 'email': $('#txtAdminEmail').val(), 'password': $('#txtAdminPassword').val(), 'tierid': '2', 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnSaveNewAdminUser);
}

function ReturnSaveNewAdminUser(msg) {
    if (msg.d == true) {
        $("#new-admin-user-success").html('New User Saved');
        $("#new-admin-user").dialog("close");
        GetUsers($('#ctl00_MainContent_cboCustomer').val())
        $('#new-admin-user input[type=text]').val('')
    }
    else {
        $("#new-admin-user-error").html('Error Saving User');
    }
}

function getAdminUsersInfo()
{
    var urlMethod = "../CustomerManageWebService.asmx/getAdminUsersInfo";

    //$('#AdminDDL :selected').tect(); //this gets the Text
    var json = { 'Username': $('#AdminDDL').val(), 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, PopuplateUsersInfo);
}
function PopuplateUsersInfo(msg)
{
    var info = jQuery.parseJSON(msg.d);
    //Continue here
    $('#AdminUsersName').val($('#AdminDDL').val());
    $('#AdminEmail').val(info.Email);
    $('#AdminPassword').val("**********");
}

function DeleteAdmin() {
    var urlMethod = "../CustomerManageWebService.asmx/DeleteUser";

    var json = { 'Username': $('#AdminUsersName').val(), 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnDeleteAdmin);
}
function ReturnDeleteAdmin(msg)
{
    //What TO Do here?
}
function UpdateAdmin()
{
    var urlMethod = "../CustomerManageWebService.asmx/updateAdmin";

    var json = { 'username': $('#AdminUsersName').val(), 'email': $('#AdminEmail').val(), 'password': $('#AdminPassword').val(), 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnUpdatedAdmin);

}
function ReturnUpdatedAdmin(msg)
{
    //Updated now what?
    $('#new-admin-user').dialog("close");
    $('#AdminUsersName').val("");
    $('#AdminEmail').val("");
    $('#AdminPassword').val("");
}
function getAllAdminUsers()
{
    var urlMethod = "../CustomerManageWebService.asmx/getAllAdmins";

    var json = { 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, PopulateDropDown);


}
function PopulateDropDown(msg)
{

    var info = jQuery.parseJSON(msg.d);
    $(info).each(function (i) {

        $('#AdminDDL').append($('<option></option>').val($(this)[0].UserName).html($(this)[0].UserName));
    });
}

function DeleteUser() {
    var urlMethod = "../CustomerManageWebService.asmx/DeleteUser";

    var json = { 'Username': $('#cboUser option:selected').text(), 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnDeleteUser);
}

function ReturnDeleteUser(msg) {
    if (msg.d == true) {
        $("#new-user-success").html('User Deleted');
        $("#new-user-success").show();
        $('#new-user-success').fadeOut(2000);
        GetUsers($('#ctl00_MainContent_cboCustomer').val())
        $('#oldpassword').val('');
        $('#oldemail').val('');
        $('#ctl00_ctl00_Content_CustomerManage1_selUserComp option:selected').html('Select Customer')
    }
    else {
        $("#new-user-error").html('Error Deleting User');
    }
}

function GoodUserName(username) {
    var urlMethod = "../CustomerManageWebService.asmx/GoodUserName";

    var json = { 'username': username, 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnGoodUserName);
}

function ReturnGoodUserName(msg) {
    if (msg.d == true) {
        $('#good-username').hide();
        $("#new-user").next().find('button:nth(0)').show()
    }
    else {
        $('#good-username').show();
        $("#new-user").next().find('button:nth(0)').hide()
    }
}

function GoodAdminUserName(username) {
    var urlMethod = "../CustomerManageWebService.asmx/GoodUserName";

    var json = { 'username': username, 'client': user.Client };
    var jsonData = JSON.stringify(json);

    SendAjax(urlMethod, jsonData, ReturnGoodAdminUserName);
}

function ReturnGoodAdminUserName(msg) {
    if (msg.d == true) {
        $('#good-admin-username').hide();
        $("#new-admin-user").next().find('button:nth(0)').show()
    }
    else {
        $('#good-admin-username').show();
        $("#new-admin-user").next().find('button:nth(0)').hide()
    }
}

function SaveNewCustomer() {
    var urlMethod = "../CustomerManageWebService.asmx/SaveNewCustomer";
    var json = { 'company': $('#txtCompanyName').val()
        , 'custno': $('#txtCustomerNo').val()
        , 'address': $('#txtAddress').val()
        , 'city': $('#txtCity').val()
        , 'state': $('#txtState option:selected').val()
        , 'zip': $('#txtZip').val()
        , 'phone': $('#txtPhone').val()
        , 'salesman': $('#txtSalesman').val()
        , 'customertype': $('#txtCustomerType option:selected').val()
        , 'companymakes': $('#txtCompanyMakes').val()
        , 'client': user.Client
    };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnSaveNewCustomer);
}

function ReturnSaveNewCustomer(msg) {
    if (msg.d == true) {
        $("#new-customer-success").html('New Customer Saved');
        $("#new-customer").dialog("close");
        $('#ctl00_MainContent_cboCustomer').append("<option value='" + $('#txtCustomerNo').val() + "'>" + $('#txtCompanyName').val() + "</option>")
        $('#new-customer input[type=text]').val('')
        location.reload(); 
    }
    else {
        $("#new-customer-error").html('Error Saving Customer');
    }
}

function DeleteCustomer() {
    var urlMethod = "../CustomerManageWebService.asmx/DeleteCustomer";
    var json = { 'companyID': $('#ctl00_MainContent_cboCustomer').val(), 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnDeleteCustomer);
}

function ReturnDeleteCustomer(msg) {
    if (msg.d == true) {
        $("#new-customer-success").html('Customer Deleted');
        $("#new-customer-success").fadeOut(2000);
        location.reload();     
    }
    else {
        $("#new-customer-error").html('Error Saving Customer');
        $("#new-customer-success").fadeOut(2000);     
    }
}


function GoodCustomerNo(custno, type) {
    var typ = type;
    var urlMethod = "../CustomerManageWebService.asmx/GoodCustomerNo";

    var json = { 'custno': custno, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    
    SendAjax(urlMethod, jsonData, function (msg) {
        var error;
        var submit;
        switch (typ) {
            case "txt":
                error = $('#txtCustomerNo').parents('.small-form').find('.cust-no-error');
                submit = $("#new-customer").next().find('button:nth(0)');
                break;
            case "edt":
                error = $('#edtCustomerNo').parents('.small-form').find('.cust-no-error');
                submit = $('#edtCustomerNo').parents('.small-form').find('input[type=submit],input[type=button]');
                break;
        }
        if (msg.d == true) {
            error.hide();
            submit.show()
        }
        else {
            error.show();
            submit.hide();
        }
    });
}

//      function GetMarkup(custno, oemid) {
//          var urlMethod = "../CustomerManageWebService.asmx/GetMarkup";
//          var jsonData = "{'custno': '" + custno + "', 'oemid': '" + oemid + "'}";
//          SendAjax(urlMethod, jsonData, ReturnGetMarkup);
//      }
//      var info
//      function ReturnGetMarkup(msg) {
//       info = jQuery.parseJSON(msg.d);
//       $('#amount').val(info[0].Markup)
//       $('#discount_type').val(info[0].Source)
//       $('#slider').slider( "value" , info[0].MarkupVal)
//   }

var emails;

function GenerateEmail(CustomerType, CompanyMakes) {
    var urlMethod = "../CustomerManageWebService.asmx/GenerateEmail";

    var json = { 'CustomerType': CustomerType, 'CompanyMakes': CompanyMakes, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, function (msg) {
        emails = jQuery.parseJSON(msg.d);
        var email_string = "";
        $(emails).each(function () {
            email_string = email_string + this + ", ";
        });

        var subject = "";
        var body = "";
        var mailto_link = 'mailto:' + email_string + '?subject=' + subject + '&body=' + body;

        //win = window.open(mailto_link, 'emailWindow');
        //if (win && win.open &&!win.closed) win.close();
        $("#email-list").dialog({
            autoOpen: false,
            resizable: false,
            height: 420,
            width: 680,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#email-list span").html("");
        $("#email-list span").html(email_string);
        $("#email-list").dialog("open");
    });
}

function UpdateCustomerInfo() {
    var urlMethod = "../CustomerManageWebService.asmx/UpdateCustomerInfo";

    var json = {"CompanyID" : $('#CompanyID').val()
        , "CustomerNo" : $('#edtCustomerNo').val()
        , "Company" : $('#edtCompanyName').val()
        , "Address" : $('#edtAddress').val()
        , "City" : $('#edtCity').val()
        , "State" : $('#edtState option:selected').val()
        , "Zip" : $('#edtZip').val()
        , "Phone": $('#edtPhone').val()
        //, "AutoNation": ($('#edtAutoNation').attr('checked')=="checked")? 1 : 0
        , "SalesmanEmail": $('#edtSalesmanEmail').val()
        , "CustomerType": $('#edtCustomerType option:selected').val()
        , "CompanyMakes": $('#edtCompanyMakes').val()
        , "client": user.Client
    };

    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnUpdateCustomerInfo);
}

function ReturnUpdateCustomerInfo(msg) {
    $('#update_saved').hide();
    if (msg.d) $('#update_saved').fadeIn();
}

function GetCustomerInfo(custno) {
    var urlMethod = "../CustomerManageWebService.asmx/GetCustomerInfo";

    var json = { 'custno': custno, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnGetCustomerInfo);
}

function ReturnGetCustomerInfo(msg) {
    var info = jQuery.parseJSON(msg.d);

    $('#CompanyID').val(info.CompanyID);
    $('#edtCompanyName').val(info.Company);
    $('#edtCustomerNo').val(info.CustNo);
    $('#edtAddress').val(info.Address);
    $('#edtCity').val(info.City);
    $('#edtState option[value='+info.State+']').attr('selected','selected');
    $('#edtZip').val(info.Zip);
    $('#edtPhone').val(info.Phone); 
    //$('#edtAutoNation').attr('checked', (info.Autonation == "True") ? true : false);
    $('#edtSalesmanEmail').val(info.SalesmanEmail);
    $('#edtCustomerType option[value=' + JSON.stringify(info.CustomerType) + ']').attr('selected', 'selected');

    // (info.CustomerType == "Dealer") ? $('#edtCompanyMakes').parent().find('.chzn-container').fadeIn().prev().prev().fadeIn() : $('#edtCompanyMakes').parent().find('.chzn-container').hide().prev().prev().hide();
    $('#edtCompanyMakes').parent().find('.chzn-container').fadeIn().prev().prev().fadeIn();
    $('#edtCompanyMakes option:selected').attr('selected', false);

    $('.search-choice').remove();
    $(info.CompanyMakes).each(function () {
        $('#edtCompanyMakes option[value='+ this +']').attr('selected', 'selected').trigger("liszt:updated");
    });
}

function GetMarkupIndividual(custno, makegroups) { 
    var urlMethod = "../CustomerManageWebService.asmx/GetMarkupIndividual";
    var json = {'custno': custno,'makegroups' : makegroups, 'client': user.Client}
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnGetMarkupIndividual);
}
function ReturnGetMarkupIndividual(msg) {
    var result = jQuery.parseJSON(msg.d);
    $(result).each(function () {
        $(".txtMGDiscount" + this.MakeGroup + "").val(this.Markup);
        $(".MGDiscountType" + this.MakeGroup + " option[value=" + this.Source + "]").attr('selected', true);
    });
}

function GetMarkupAll(custno) {
    var urlMethod = "../CustomerManageWebService.asmx/GetMarkupAll";

    var json = { 'custno': custno, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnGetMarkupAll);
}

function ReturnGetMarkupAll(msg) {
    var info = jQuery.parseJSON(msg.d);

    $('#amount').val(info[0].Markup);
    $('#discount_type').val(info[0].Source);
    $('#slider').slider("value", info[0].MarkupVal);
}

function SaveMarkupGroup(custno, makegroup, source, markup) {
    var urlMethod = "../CustomerManageWebService.asmx/SaveMarkupGroup";

    var json = {'custno': custno, 'makegroup': makegroup, 'source': source, 'markup': markup, 'client': user.Client};
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnSaveMarkupGroup);
}
function ReturnSaveMarkupGroup(msg) {
    if (msg.d == true) {
        $('.group-markup-saved').show();
    }
}

function SaveMarkupAll(custno, source, markup) {
    var urlMethod = "../CustomerManageWebService.asmx/SaveMarkupAll";

    var json = { 'custno': custno, 'source': source, 'markup': markup, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnSaveMarkupAll);
}
var info
function ReturnSaveMarkupAll(msg) {
    if (msg.d == true) {
        $('.all-markup-saved').show();
    }
}

function GetUsers(custno) {
    var urlMethod = "../CustomerManageWebService.asmx/GetUsers";
    var json = {'custno': custno, 'client':user.Client};
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnGetUsers);
}

var users;
function ReturnGetUsers(msg) {
    users = jQuery.parseJSON(msg.d);

    $('#cboUser').empty();
    $("#cboUser").append($("#usersTemplate").tmpl(users[0]));
}

function GetUser(UserID) {
    var urlMethod = "../CustomerManageWebService.asmx/GetUser";
    var json = { 'userid': UserID, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnGetUser);
}

var theuser
function ReturnGetUser(msg) {
    theuser = jQuery.parseJSON(msg.d);

    $('#oldpassword').val(theuser[0].Password);
    $('#oldemail').val(theuser[0].Email);

    if (theuser[0].Active == 'Yes') {
        $('#active').prop("checked", true);
    }
    else {
        $('#active').prop("checked", false);
    }
    if (theuser[0].CanOrder == 'Yes') {
        $('#canorder').prop("checked", true);
    }
    else {
        $('#canorder').prop("checked", false);
    }
    $('#selTier').attr('value', theuser[0].TierID)
    $('#ctl00_ctl00_Content_CustomerManage1_selUserComp').attr('value', theuser[0].CustomerNo)
}

function GetTiers() {

    var urlMethod = "../CustomerManageWebService.asmx/GetTiers";
    var json = { 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnGetTiers);

}
var tiers;
function ReturnGetTiers(msg) {

    tiers = jQuery.parseJSON(msg.d);
    var tieri = 0
    $(tiers).each(function () {
        $('#selTier').append("<option value=" + tiers[tieri].TierID + ">" + tiers[tieri].Tier + "</option>");
        $('#selTierNew').append("<option value=" + tiers[tieri].TierID + ">" + tiers[tieri].Tier + "</option>");
        $('#csv-tiers').append("<tr><td>" + tiers[tieri].TierID + "</td><td>" + tiers[tieri].Tier + "</td></tr>");
        tieri = tieri + 1;
    });
}

function ChangePassword(UserID, Password) {
    var urlMethod = "../CustomerManageWebService.asmx/ChangePassword";
    var json = { 'userid': UserID, 'password': Password, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnChangePassword);
}

function ReturnChangePassword(msg) {
    $('#passwordsuccess').hide();
    if (msg.d == true) { $('#passwordsuccess').html('Saved'); $('#passwordsuccess').show(); }
    $('#passwordsuccess').fadeOut(2000);
}

function ChangeEmail(UserID, Email) {
    var urlMethod = "../CustomerManageWebService.asmx/ChangeEmail";
    var json = { 'userid': UserID, 'email': Email, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnChangeEmail);
}

function ReturnChangeEmail(msg) {
    $('#emailsuccess').hide();
    if (msg.d == true) { $('#emailsuccess').html('Saved'); $('#emailsuccess').show(); }
    $('#emailsuccess').fadeOut(2000);
}
function ChangeUserComp(UserID, companyID) {
    var urlMethod = "../CustomerManageWebService.asmx/ChangeUserComp";
    var json = { 'userid': UserID, 'companyID': companyID, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnChangeUserComp);
}
function ReturnChangeUserComp(msg) {
    $('#userCompSuccess').hide();
    if (msg.d == true) { $('#userCompSuccess').html('User moved from ' + $('#ctl00_MainContent_cboCustomer option:selected').text() + ' to ' + $('#ctl00_MainContent_selUserComp option:selected').html()); $('#userCompSuccess').show(); }
    GetUsers($('#ctl00_MainContent_cboCustomer').val())
    $('#oldpassword').val('');
    $('#oldemail').val('');
    $('#ctl00_ctl00_Content_CustomerManage1_selUserComp option:selected').html('Select Customer')
    $('#userCompSuccess').fadeOut(6000);
}
function ChangeActive(UserID, Active) {
    var urlMethod = "../CustomerManageWebService.asmx/ChangeActive";
    var json = {'userid':  UserID, 'active':  Active, 'client': user.Client};
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnChangeActive);
}
function ReturnChangeActive(msg) {
    $('#activesuccess').hide();
    if (msg.d == true) { $('#activesuccess').html('Saved'); $('#activesuccess').show(); }
    $('#activesuccess').fadeOut(2000);
}
function ChangeCanOrder(UserID, CanOrder) {
    var urlMethod = "../CustomerManageWebService.asmx/ChangeCanOrder";
    var json = { 'userid': UserID, 'canorder': CanOrder, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnChangeCanOrder);
}
function ReturnChangeCanOrder(msg) {
    $('#canordersuccess').hide();
    if (msg.d == true) { $('#canordersuccess').html('Saved'); $('#canordersuccess').show(); }
    $('#canordersuccess').fadeOut(2000);
}
function ChangeTier(UserID, TierID) {
    var urlMethod = "../CustomerManageWebService.asmx/ChangeTier";
    var json = { 'userid': UserID, 'TierID': TierID, 'client': user.Client };
    var jsonData = JSON.stringify(json);
    SendAjax(urlMethod, jsonData, ReturnChangeTier);
}
function ReturnChangeTier(msg) {

    $('#tiersuccess').hide();
    if (msg.d == true) { $('#tiersuccess').html('Saved'); $('#tiersuccess').show(); }
    $('#tiersuccess').fadeOut(2000);
}