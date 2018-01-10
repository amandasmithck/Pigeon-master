 <%@ Page Title="Login" Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Pigeon._Default" %>
<!DOCTYPE html>
<head id="Head1" runat="server">
    <title></title>

    <!--custom style-->
    <link href="~/Styles/common.css" rel="stylesheet" type="text/css" />

    <!-- jquery-->
    <script type="text/javascript" src="../Scripts/jquery.min.js" ></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.10.custom.min.js" ></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.min.js"></script>
    <link href="~/Styles/ui-theme/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />

    <!--good ol bootstrap [2.1] -->
    <link href="~/Styles/bootstrap/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap/bootstrap.js"></script>

    <!--my modules-->
    <link type="text/css" href="~/Styles/sidebarsearch.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/modules/search-filters-1.4.js"></script>
    <script type="text/javascript" src="../Scripts/modules/checkout-form-2.2.js"></script>
    <script type="text/javascript" src="../Scripts/modules/tools.js"></script>

    <!--vector map-->
<%--    <link href="~/Scripts/jquery.jvectormap/jquery.vector-map.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.jvectormap/jquery.vector-map.js"></script>
    <script src="../Scripts/jquery.jvectormap/usa-en.js"></script>--%>

    <!--tabs-->
    <script src="../Scripts/jquery.idTabs.min.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="~/Styles/idTabs.css">

    <!--cursor message-->
    <link type="text/css" rel="stylesheet" href="~/Styles/jquery.cursorMessage.css" />
    <script type="text/javascript" src="../Scripts/jquery.cursorMessage.js"></script>

    <!--fancy box-->
    <script type="text/javascript" src="../Scripts/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../Scripts/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link type="text/css" rel="stylesheet" href="~/Scripts/fancybox/jquery.fancybox-1.3.4.css" media="screen" />

    <!--other scripts-->
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/accounting.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.jqDock.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.counter-1.0.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.maskedinput-1.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate-1.0.js"></script>

    <style type="text/css">
        input {
            width: 150px;
        }
        .well {background-color:White;}
        form {margin:0;}

        .promo-contain {margin: 10px 0 4px; font-size:11px;}
        .promo-contain ul {line-height:16px;}

        .footer-categories 
        {
            position: relative;
            bottom: 0px;
            margin: 0;
            list-style-type: none;
            min-height: 20px !important;
         }
         
         #part-lines {}
         
        .footer-categories li {float:left;text-align:left;padding: 0 35px 0 10px;font-weight: bold;font-size:10px;line-height:25px;}
        .footer-categories li.list-title {width:72px;}
    </style>

    <script type="text/javascript">
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

        var client = '<%= Session("Client")%>', clientInfo;

        $('document').ready(function () {

            if (client == "FMP")
            {
                $('.well.span6').hide();
                $('#LoginContainer').attr("class", "well span10 offset1");
                $('#LoginContainer').attr("style", "padding:0px;margin-left:80px");
                $('.well.span4.offset1').attr("style", "margin-left: 0px;border:none;float:left;margin-left:23px;margin-top:0px");
                $('.well.span4').show();
                $('#FMPHeader').show();
                $('#Header').hide();
                //$('#Login1').hide();
                //$('#FMPLogin').show();
                //$('#Login1').attr("class", "form-inline");
                //$('#Login1').attr("style", "border-collapse:collapse;width:2000px;");
                //$('#Login1').attr("style", "border-collapse:collapse;margin-left:68px");
            }

            var urlMethod = "../PigeonWebService.asmx/GetClientInfo";
            var jsonData = JSON.stringify({ 'client': client });
            SendAjax(urlMethod, jsonData, function (msg) {
                clientInfo = jQuery.parseJSON(msg.d);

                $('#phone').html($('#phone').html() + clientInfo.Phone + '.');
                $('body').css({ 'background': "url('" + clientInfo.BackgroundImgURL + "')" });
                $('.promo-contain').append(clientInfo.PromoHTML);
                $('.logo-contain').append('<a href="../Default.aspx"><img style="padding: 14px;" src="' + clientInfo.LogoURL + '" alt="Logo" /></a>');

                $(clientInfo.PartLines).each(function (e, v) {
                    $('#part-lines').append("<li>" + v + "</li>");
                    $('#part-lines li:not(:first)').css({ 'width': 'auto' });//(100 / clientInfo.PartLines.length) + "%" });
                });

                /*$(clientInfo.Warranties).each(function (e, v) {
                    $('#warranties').append("<li>" + v + "</li>");
                    $('#warranties li:not(:first)').css({ 'width': 'auto' }); //(100 / clientInfo.Warranties.length) + "%" });
                });*/
            });

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


            $('.small-form .btn').click(function () {

                if ($('.small-form input').valid()) {
                    NewCustEmail();
                }

            });

            $('#txtUsername').blur(function () {

                GoodUserName($(this).val());
            });
            //if (client == 'CK') {
                //$('#alert').text("Due to severe weather and power outages in the area we are experiencing issues with our phones, please utilize email. ////Thanks for your patience.");
            //}
            // $('#alert').text("This website will be down from maintenance starting Saturday January 21st at 6pm EST and may continue until no later than Monday January 23rd at 6am EST but may be back online earlier.  If you are able to reach this page and no longer see this message, the website is fully functional and back online.");
        });

        function NewCustEmail() {

            var urlMethod = "CustomerManageWebService.asmx/NewCustEmail";

            var data = {
                'company': $('#txtCompanyName').val()
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
            , 'client': client
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
            }
            else {
                var urlMethod = "../CustomerManageWebService.asmx/GoodUserName";

                var json = { 'username': username, 'client': client };
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, ReturnGoodUserName);

            }
        }
        function ReturnGoodUserName(msg) {
            if (msg.d == true) {
                $('#good-username').hide();
                $('#submit').show()
            }
            else {
                $('#good-username').show();
                $('#good-username').html('Username already in use');
                $('#submit').hide()
            }
        }
    </script>
    
<%--               <!-- begin olark code -->
            <script data-cfasync="false" type='text/javascript'>if (client == 'CK') {/*<![CDATA[*/window.olark || (function (c) {
var f = window, d = document, l = f.location.protocol == "https:" ? "https:" : "http:", z = c.name, r = "load"; var nt = function () {
    f[z] = function () {
        (a.s = a.s || []).push(arguments)
    }; var a = f[z]._ = {
    }, q = c.methods.length; while (q--) {
        (function (n) {
            f[z][n] = function () {
                f[z]("call", n, arguments)
            }
        })(c.methods[q])
    } a.l = c.loader; a.i = nt; a.p = {
        0: +new Date
    }; a.P = function (u) {
        a.p[u] = new Date - a.p[0]
    }; function s() {
        a.P(r); f[z](r)
    } f.addEventListener ? f.addEventListener(r, s, false) : f.attachEvent("on" + r, s); var ld = function () {
        function p(hd) {
            hd = "head"; return ["<", hd, "></", hd, "><", i, ' onl' + 'oad="var d=', g, ";d.getElementsByTagName('head')[0].", j, "(d.", h, "('script')).", k, "='", l, "//", a.l, "'", '"', "></", i, ">"].join("")
        } var i = "body", m = d[i]; if (!m) {
            return setTimeout(ld, 100)
        } a.P(1); var j = "appendChild", h = "createElement", k = "src", n = d[h]("div"), v = n[j](d[h](z)), b = d[h]("iframe"), g = "document", e = "domain", o; n.style.display = "none"; m.insertBefore(n, m.firstChild).id = z; b.frameBorder = "0"; b.id = z + "-loader"; if (/MSIE[ ]+6/.test(navigator.userAgent)) {
            b.src = "javascript:false"
        } b.allowTransparency = "true"; v[j](b); try {
            b.contentWindow[g].open()
        } catch (w) {
            c[e] = d[e]; o = "javascript:var d=" + g + ".open();d.domain='" + d.domain + "';"; b[k] = o + "void(0);"
        } try {
            var t = b.contentWindow[g]; t.write(p()); t.close()
        } catch (x) {
            b[k] = o + 'd.write("' + p().replace(/"/g, String.fromCharCode(92) + '"') + '");d.close();'
        } a.P(2)
    }; ld()
}; nt()
})({
    loader: "static.olark.com/jsclient/loader0.js", name: "olark", methods: ["configure", "extend", "declare", "identify"]
});
    /* custom configuration goes here (www.olark.com/documentation) */
    olark.identify('8837-654-10-7636');/*]]>*/
}</script><noscript><a href="https://www.olark.com/site/8837-654-10-7636/contact" title="Contact us" target="_blank">Questions? Feedback?</a> powered by <a href="http://www.olark.com?welcome" title="Olark live chat software">Olark live chat software</a></noscript>
     --%>   

</head>
<body style="background-size: cover !important">
   <div style="margin-top: 20px;" class="container">
	    <div class="row">
		    <div style="padding:0px;margin-bottom: 5px;" class="well span10 offset1">
			    <div class="logo-contain span4"></div>
                <div class="promo-contain span6"></div>
                <div class="clear-fix"></div>
            </div>
        </div>
    </div>

    <div style="border-radius: 0;padding: 0;margin: 0 0 1%;width: 100%;background:#F2F2F2;" class="well container-fluid">
        <div class="container">
            <div class="row-fluid">
                <ul id="part-lines" class="footer-categories span10 offset1"></ul>
		    </div>
        </div>
    </div>
                
    <div style="" class="container">
        <div class="row-fluid" id="LoginContainer">
            <div class="well span4 offset1">
                <h3 id="Header">Login</h3>
                <h4 style="display:none;" id="FMPHeader">Start your powertrain search</h4>
                <p id="alert" style="color:red"></p>
                <form id="Form1" runat="server">
                    <asp:Login VisibleWhenLoggedIn="true" ID="Login1" runat="server" LoginButtonImageUrl="images/login.jpg"
                        LoginButtonType="Image">
                        <LayoutTemplate>
                               <div class="form-group">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name</asp:Label>
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                   </div>
                               <div class="form-group">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                         </div>
                               <div class="clear-fix"></div>
                            <asp:Button ID="LoginImageButton" runat="server" Text="Login" CommandName="Login"
                                ValidationGroup="Login1" class="btn btn-primary" />
                            <!--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Account/Login.aspx">Forgot your password?</asp:HyperLink>-->
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        </LayoutTemplate>
                    </asp:Login>
                    <!--<asp:LoginStatus CssClass="button" Style="float: right;" ID="LoginStatus1" runat="server"
                        LogoutPageUrl="Default.aspx" LogoutAction="Redirect" />
                    <asp:HyperLink ID="HyperLink3" CssClass="button" runat="server" NavigateUrl="Account/login.aspx"
                        Style="float: right; text-decoration: underline; color: #C1004A" Visible="False">Return To Member Area</asp:HyperLink>-->
                </form>
                <br />
                <br />
            </div>
            <div class="well span6">
                <form class="small-form rounded-ten">
                    <h3>Signup for Premium Account</h3>
                    <p id="phone"><small>* New customers only. If your company already has an account but you need a login, please call </small></p>
                    
                    <hr />

                    <div style="padding:0;" class="container-fluid">
                        <div class="span6">
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
                        <div class="span6">
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
                            <div class="clear-fix"></div>
                            <div class="btn btn-primary" id="submit">Submit</div><label id="email-error" style="color: Red"></label>
                        </div>
                    </div>
                </form>
            </div>
            <div class="well span4" style="border:none;display:none;float:right">
                <img src="/images/FVPLogin.jpg" />
            </div>
	    </div>
    </div>
    <div id="dialog-message" title="Confirmation">
	<p class="message">Account request submitted. One of our sales associates will contact you with your new account information.</p>
</div>
</body>
</html>