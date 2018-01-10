<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormerTracyCustomerRedirect.aspx.vb" Inherits="Pigeon.FormerTracyRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:LoginStatus ID="LoginStatus1" runat="server"   LogoutPageUrl="../default.aspx" LogoutAction="Redirect" />
    <div >
    <p align="center" style="font-weight:bold; font-size:xx-large">TECPowertrain.com customers will now be handled directly by C&K Auto Parts, the supplier for TECPowertrain.com.</p>
<p align="center">There will be no change in part availability, and the 36/100 and 36/Unlimited Nationwide Parts and Labor warranties are still in effect and available.
Your TECPowertrain.com User Name and Password will remain the same.
However - Your pricing on most parts will GO DOWN!</p>

<p align="center">In addition, you will now have full cataloging access to ALL MAKES in all of our different catalogs.</p>

<p align="center">Also, as former TECPowertrain.com customers, you are eligible to receive a free, customized, private label powertrain website with which to offer wholesale powertrain products.  Sign up your own customers and price things online at your own discretion.  Simply call 800-981-7358 x403 to find out more details. The process is fast and easy.</p>

<p align="center">Thanks for using TECPowertrain.com, and we look forward to serving you as C&K Auto Parts.</p>
    </div>
    </form>

    <form name="redirect">
<center>
<font face="Arial"><b>You will be redirected to C&K Auto Parts in<br><br>
<form>
<input type="text" size="3" name="redirect2">
</form>
seconds</b></font>
<br /><br />
<a href="http://parts.ckautoparts.com">If not, please click here to be redirected</a>
</center>

<script>
<!--

    /*
    Count down then redirect script
    By JavaScript Kit (http://javascriptkit.com)
    Over 400+ free scripts here!
    */

    //change below target URL to your own
    var targetURL = "http://parts.ckautoparts.com"
    //change the second to start counting down from
    var countdownfrom = 30


    var currentsecond = document.redirect.redirect2.value = countdownfrom + 1
    function countredirect() {
        if (currentsecond != 1) {
            currentsecond -= 1
            document.redirect.redirect2.value = currentsecond
        }
        else {
            window.location = targetURL
            return
        }
        setTimeout("countredirect()", 1000)
    }

    countredirect()
//-->
</script>
</body>
</html>
