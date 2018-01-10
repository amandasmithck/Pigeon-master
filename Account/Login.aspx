<%@ Page Title="Log In" Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Pigeon.Login" %>
<!doctype html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Login/Register</title>

	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	
	<link href='http://fonts.googleapis.com/css?family=Muli&v2' rel='stylesheet' type='text/css'>
	<!-- 1140px Grid styles for IE -->
	<!--[if lte IE 9]><link rel="stylesheet" href="Styles/ie.css" type="text/css" media="screen" /><![endif]-->

	<!-- The 1140px Grid - http://cssgrid.net/ -->
	<link rel="stylesheet" href="../Styles/1140.css" type="text/css" media="screen" />
	
	<!-- Your styles -->
	<link rel="stylesheet" href="../Styles/styles.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="../Styles/ui-black/jquery.ui.custom.css" type="text/css" media="screen" />

	<!--css3-mediaqueries-js - http://code.google.com/p/css3-mediaqueries-js/ - Enables media queries in some unsupported browsers-->
	<!--<script type="text/javascript" src="../Scripts/css3-mediaqueries.js"></script>-->
    <script type="text/javascript" src="../Scripts/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.16.all.min.js"></script>
    <script src="../Scripts/DD_roundies_0.0.2a-min.js"></script>
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate-1.0.js"></script>
    <script type="text/javascript">
        $('document').ready(function () {
            $("#dialog-message").dialog({
                autoOpen: false,
                modal: true,
                buttons: {
                    Ok: function () {
                        $("#dialog-message").dialog("close");
                        $('.small-form input').val('');
                    }
                }
            });
            $('#new-user-form #submit-newcustomer').click(function () {
                if ($('#new-user-form input').valid()) { NewCustEmail(); }
            });
            $('#txtUsername').blur(function () {
                GoodUserName($(this).val());
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

        function NewCustEmail() {
            var urlMethod = "../OEMWebService.asmx/NewCustEmail";

            var data = { 'company': $('#txtCompanyName').val()
                , 'address': $('#txtAddress').val()
                , 'city': $('#txtCity').val()
                , 'state': $('#txtState option:selected').val()
                , 'zip': $('#txtZip').val()
                , 'phone': $('#txtPhone').val()
                , 'contact': $('#txtContact').val()
                , 'email': $('#txtEmail').val()
                , 'salesman': $('#txtPrefSalesman').val()
                , 'username': $('#txtUsername').val()
                , 'password': $('#txtPassword').val()
                , 'client': $('.current_client').text()
            };
            var jsonData = JSON.stringify(data);
            SendAjax(urlMethod, jsonData, ReturnNewCustEmail);
        }

        function ReturnNewCustEmail(msg) {
            if (msg.d == true) {
                $("#dialog-message").dialog("open");
                $("#email-error").html("");
            }
            else {
                $("#email-error").html("Unable to send email.");
            }
        }
        function GoodUserName(username) {
            if ((username.length < 5) || (username.length > 20)) {

                $('#good-username').show();
                $('#good-username').html('Username must be between 5 and 20 characters');
                $('#submit').hide()
            } else {
                var urlMethod = "../OEMWebService.asmx/GoodUserName";

                var json = { 'username': username, 'client': $('.current_client').text() };
                var jsonData = JSON.stringify(data);
                SendAjax(urlMethod, jsonData, ReturnGoodUserName);

            }
        }
        function ReturnGoodUserName(msg) {
            if (msg.d == true) {
                $('#good-username').hide();
                $('#submit').show()
            } else {
                $('#good-username').show();
                $('#good-username').html('Username already in use');
                $('#submit').hide()
            }
        }
    </script>
</head>
<body>
    <div class="container header">
	    <div class="row">
		    <div class="threecol">
			    <%--<a href="../Default.aspx"><img src="../Pages/Assets/GO/images/GO_LOGO_3CSH1_60.png" alt="GO" /></a>--%>
		    </div>
            <div class="fourcol">
            
            </div>
		    <div class="fivecol last">
		    </div>
	    </div>
    </div>
    <div class="container header">
	    <div class="row">
		    <div class="sixcol text-block">
                <form class="small-form rounded-ten" id="Form1" runat="server"><asp:Label runat="server" ID="current_client" class="current_client" ForeColor="White" ></asp:Label>
                    <h1>Log In</h1>
                    <p class="description">Please enter your username and password.</p>
                    <div class="form-twocol">
                        <div class="form-twocol-left"> 
                            <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" >
                                <LayoutTemplate>
                                    <span class="failureNotification"><asp:Literal ID="FailureText" runat="server"></asp:Literal></span>
                                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup"/>
                            
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server"  CssClass="text-box"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    
                                    <asp:TextBox style="width: 80%;margin-right: 20%;padding: 5px;font-size: 14px;margin-bottom: 4%;" ID="Password" runat="server" CssClass="text-box" TextMode="Password"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                    
                                    <asp:CheckBox ID="RememberMe" runat="server"/>

                                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                                    
                                    <div class="clear"></div>

                                    <asp:Button style="border-radius: 6px;-moz-border-radius: 6px;background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0, #4a8ee5),color-stop(1, #3a75cc));background-image: -moz-linear-gradient(top, #4a8ee5, #3a75cc);cursor:pointer; color: #fff;background-color: #4a8ee5;float:left;padding:10px;width:100px;" ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                   
                                </LayoutTemplate>
                            </asp:Login>
                            <asp:LoginStatus CssClass="button" Style="float:right;" ID="LoginStatus1" runat="server" LogoutPageUrl="../Default.aspx" LogoutAction="Redirect" />
                        </div>
                    </div>
                </form>
            </div>
          <%--  <div class="sixcol text-block last">
                <form class="small-form rounded-ten" id="new-user-form">
                <h1>Signup for Premium Account</h1>
                <p class="description">Fill out the form below to create an account for discount pricing.</p>
                <p class="description">*New customers only. If your company already has an account but you need a login, please call 1-866-463-4619.</p>
                
                <div class="form-twocol">
                    <div class="form-twocol-left"> 
                        <label>Company Name</label>
                        <input type="text" class="required" id="txtCompanyName" name="txtCompanyName" />
                        <label>Address</label>
                        <input type="text" class="required" id="txtAddress" />
                        <label>City</label>
                        <input type="text" class="required" id="txtCity" />
                        
                        <div style="width: 33%" class="form-twocol-left">
                            <label>State</label>
                            <select class="required" style="padding: 4px;width:100%;margin-right: 50%;" id="txtState" name="txtState" size="1">
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
                        </div>

                        <div style="width: 66%" class="form-twocol-right">
                            <label>Zip</label>
                            <input style="width: 70%;margin-right: 30%;" type="text" class="required" id="txtZip" />
                        </div>
                        <label>Phone</label>
                        <input type="text" class="required" id="txtPhone" />
                    </div>
                    <div class="form-twocol-right"> 
                        <label>Contact</label>
                        <input type="text" class="required" id="txtContact" />
                        <label>Email</label>
                        <input type="text" class="required email" id="txtEmail" />
                        <label>Desired Username</label>
                        <input type="text" class="required" id="txtUsername" />
                        <label class="error" style="display:none;" id="good-username"></label>
                        <label>Desired Password</label>
                        <input type="password" class="required" id="txtPassword" />
                        <label>Preferred Sales Person</label>
                        <input type="text" class="required" id="txtPrefSalesman" />
                        <div class="button" id="submit-newcustomer">Submit</div><label id="email-error" style="color: Red"></label>
                    </div>
                </div>
            </form>
            </div>--%>
        </div>
    </div>

<div id="dialog-message" title="Confirmation">
	<p class="message">Account request submitted. One of our sales associates will contact you with your new account information.</p>
</div>

</body>
</html>
