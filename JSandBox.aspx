<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="JSandBox.aspx.vb" Inherits="Pigeon.JSandBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Styles/bootstrap/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/bootstrap/bootstrap.js"></script>
    <script src="Scripts/bootstrap-typeahead.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="Scripts/jquery.tmpl.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>J's SANDBOX FED EX RATE</h2>
        <h4 id="fedExRate" runat="server"></h4>
        <asp:RadioButtonList ID="rblServiceTypes" runat="server" CausesValidation="false" RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="2">            
        </asp:RadioButtonList>
    </div>


        <p>
            Weight:
            <input type="text" id="txtWeight" />
        </p>
    </form>
</body>
</html>
