<%@ Page Title="Cooling Search"  Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="CoolingSearch.aspx.vb" Inherits="Pigeon.CoolingSearch3" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid main-content">
	    <div class="row-fluid">
            <div class="span3">
                <!--<h1 class="datatable-heading">Search</h1>-->
                <div class="well">
                    <!--<p><h6>In order to look up cooling products you will need to input your 17 digit vehicle identification number</h6></p><p></p>-->
                    <label>Vin</label><input class="input-medium" id="vid" type="text" />
                    <label>Zip</label><input class="input-small" id="zip" type="text" />

                    <div class="clear-fix"></div>

                    <input class="btn" id="search-button" type="button" value="Search" />
                </div>
                
                <div style="display: none;" class="empty-cart alert alert-info">
                    <a class="close" data-dismiss="alert" href="#">&times;</a> Cart is empty. Click
                    on a part result and then click <b>Order</b>.
                </div>
                <div style="display: none;" class="cart"></div>
            </div>
            <div style="min-height: 200px;" class="catalog-container span9">
                <div id="catalog">
                    <table id="details">
                        <thead>
                            <tr><th>Order</th><th>Part</th><th>Family / Category / Liters / Cubic In. / Cyl</th><th width="30%">Description</th><th>Delivery</th><th>Deliver From</th><th>List Price</th><th>Sell Price</th></tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    </form>

    <script id="detailsTemplateCust" type="text/html">
        <tr><td><button class="btnOrderPart">Order</button></td><td>${part}</td><td>${fam} / ${cat} / ${engLi} / ${engCi} / ${engCyl}</td><td>${comment}</td><td>${delivery}</td><td>${deliverFrom}</td><td>${listPrice}</td><td>${sellPrice}</td></tr>
    </script>

    <script id="detailsTemplateAdmin" type="text/html">
        <tr><td>${part}</td><td>${fam} / ${cat} / ${engLi} / ${engCi} / ${engCyl}</td><td width="30%">${comment}</td><td>${inStock}</td><td>${delivery}</td><td>${deliverFrom}</td><td>${listPrice}</td><td>${price}</td><td>${sellPrice}</td></tr> 
    </script>

    <script>
        var user = <%= Session("UserModel") %>;
        
        $('document').ready(function () {
             $('.tab-warranties').hide();

            $('#search-button').click(function () { GetCatalog(); });

            $('#details').dataTable({ "bJQueryUI": true, "sPaginationType": "full_numbers" });
            $('#details').css({ 'width': '100%' }); //hack, prolly an option w/ dataTable

            $('.btnOrderPart').live('click', function (event) {
                event.preventDefault();
                
                var part = $(this).parents('tr').find('td:nth(1)').html();
                var price = $(this).parents('tr').find('td:nth(12)').html();
            });
            $('.order-summary .remove').live('click', function (event) {
                $(this).parent('li').remove();
                calcTotal();
                if ($('.order-summary li.part').length == 0) { $('#btnSubmitOrder, .order-summary li.total, .order-summary li.quant, .order-summary li.headings').hide(); $('#msgEmpty').show(); }
            });

            $('#btnSubmitOrder').click(function () {
                $('#txtZip').val($('#zip').val());
                $('#order-diag').dialog("open");
                return false;
            });

            GetDeliveryInfo();

            function GetDeliveryInfo() {
                var urlMethod = "../OEMWebService.asmx/GetDeliveryInfo";
                var json = { 'name': user.UserName, 'client': user.Client }
                var jsonData = JSON.stringify(json);
                SendAjax(urlMethod, jsonData, ReturnGetDeliveryInfo);
            };


            function ReturnGetDeliveryInfo(msg) {
                var info = jQuery.parseJSON(msg.d);

                $('#zip').val(info.Zip);
            }

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

        function GetCatalog() {
            var urlMethod = "../CoolingWebService.asmx/GetCatalog";
            var an = "2013612";
            var aid = "AutowayChev";
            var zip = ($('#zip').val());
            var vid = ($('#vid').val());

            var data = { 'an': an
                    , 'aid': aid
                    , 'zip': zip
                    , 'vid': vid
            };

            var jsonData = JSON.stringify(data);
            SendAjax(urlMethod, jsonData,
                function (msg) {
                    //response = jQuery.parseJSON(msg.d);
                    response = jQuery.parseJSON("{\"LookupReply\":{\"Type\":\"LookupReply\",\"AccountNumber\":\"2013612\",\"AccountID\":\"AutowayChev\",\"Status\":\"ok\",\"Vehicle_ID\":\"1HGCM82633A004352\",\"Make\":\"honda\",\"Model\":\"accord\",\"Year\":\"2003\"},\"Location\":{\"ZipCode\":\"33613\"},\"AppCatalog\":[{\"part\":\"601170\",\"fam\":\"ENC\",\"cat\":\"FAN\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"DUAL FAN ASSEMBLY; DENSO ONLY\",\"inStock\":true,\"sameDay\":2,\"nextDay\":3,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"134.00\",\"listPrice\":\"216.00\",\"freight\":0},{\"part\":\"601180\",\"fam\":\"ENC\",\"cat\":\"FAN\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"DUAL FAN ASSEMBLY; VALEO ONLY\",\"inStock\":true,\"sameDay\":2,\"nextDay\":2,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"119.06\",\"listPrice\":\"200.00\",\"freight\":0},{\"part\":\"601190\",\"fam\":\"ENC\",\"cat\":\"FAN\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"CON FAN ASSEMBLY; DENSO ONLY\",\"inStock\":true,\"sameDay\":1,\"nextDay\":3,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"66.53\",\"listPrice\":\"125.00\",\"freight\":0},{\"part\":\"601060\",\"fam\":\"ENC\",\"cat\":\"FAN\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"RAD FAN ASSEMBLY;DENSO ONLY\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":10,\"deliverFrom\":\"GRN\",\"price\":\"78.93\",\"listPrice\":\"143.00\",\"freight\":0},{\"part\":\"2599\",\"fam\":\"ENC\",\"cat\":\"RAD\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\" \",\"inStock\":true,\"sameDay\":2,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"94.50\",\"listPrice\":\"160.00\",\"freight\":0},{\"part\":\"2854\",\"fam\":\"ENC\",\"cat\":\"RAD\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"VALEO ONLY; CAN USE# 2599\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":9,\"deliverFrom\":\"LEM\",\"price\":\"121.00\",\"listPrice\":\"207.00\",\"freight\":0},{\"part\":\"2855\",\"fam\":\"ENC\",\"cat\":\"RAD\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\" \",\"inStock\":true,\"sameDay\":1,\"nextDay\":2,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"83.50\",\"listPrice\":\"156.00\",\"freight\":0},{\"part\":\"7723262\",\"fam\":\"ENC\",\"cat\":\"RDHS\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"LOWER\",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":\"10.00\",\"listPrice\":\"14.30\",\"freight\":0},{\"part\":\"7723263\",\"fam\":\"ENC\",\"cat\":\"RDHS\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"UPPER\",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":\"10.00\",\"listPrice\":\"14.30\",\"freight\":0},{\"part\":\"7723264\",\"fam\":\"ENC\",\"cat\":\"RDHS\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"LOWER\",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":\"12.00\",\"listPrice\":\"17.16\",\"freight\":0},{\"part\":\"7723265\",\"fam\":\"ENC\",\"cat\":\"RDHS\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"UPPER\",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":\"11.00\",\"listPrice\":\"15.73\",\"freight\":0},{\"part\":\"1411677\",\"fam\":\"AC\",\"cat\":\"ACDR\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"COUPE\",\"inStock\":true,\"sameDay\":1,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"15.95\",\"listPrice\":\"26.00\",\"freight\":0},{\"part\":\"1411667\",\"fam\":\"AC\",\"cat\":\"ACDR\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"SEDAN\",\"inStock\":true,\"sameDay\":3,\"nextDay\":7,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"13.95\",\"listPrice\":\"23.00\",\"freight\":0},{\"part\":\"1411677\",\"fam\":\"AC\",\"cat\":\"ACDR\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"COUPE\",\"inStock\":true,\"sameDay\":1,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"15.95\",\"listPrice\":\"26.00\",\"freight\":0},{\"part\":\"1411667\",\"fam\":\"AC\",\"cat\":\"ACDR\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"SEDAN\",\"inStock\":true,\"sameDay\":3,\"nextDay\":7,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"13.95\",\"listPrice\":\"23.00\",\"freight\":0},{\"part\":\"4811678\",\"fam\":\"AC\",\"cat\":\"ACHS\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"DISCHARGE LINE ONLY\",\"inStock\":true,\"sameDay\":0,\"nextDay\":4,\"days3to5\":2,\"deliverFrom\":\"SRA\",\"price\":\"56.95\",\"listPrice\":\"90.00\",\"freight\":0},{\"part\":\"4811679\",\"fam\":\"AC\",\"cat\":\"ACHS\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"DISCHARGE LINE ONLY\",\"inStock\":true,\"sameDay\":0,\"nextDay\":1,\"days3to5\":10,\"deliverFrom\":\"JAX\",\"price\":\"57.95\",\"listPrice\":\"93.00\",\"freight\":0},{\"part\":\"56248\",\"fam\":\"AC\",\"cat\":\"ACHS\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"SUCTION LINE ONLY\",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":\"64.95\",\"listPrice\":\"110.00\",\"freight\":0},{\"part\":\"8011242\",\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"AC FLUSH; 22OZ BOTTLE\",\"inStock\":true,\"sameDay\":5,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"14.95\",\"listPrice\":\"24.00\",\"freight\":0},{\"part\":\"8011242\",\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"AC FLUSH; 22OZ BOTTLE\",\"inStock\":true,\"sameDay\":5,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"14.95\",\"listPrice\":\"24.00\",\"freight\":0},{\"part\":\"6512109\",\"fam\":\"AC\",\"cat\":\"COMP\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"NEW; 10S17C; 7 GROOVE\",\"inStock\":true,\"sameDay\":3,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"217.95\",\"listPrice\":\"366.00\",\"freight\":0},{\"part\":\"6512102\",\"fam\":\"AC\",\"cat\":\"COMP\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"NEW; 10S17C; 6 GROOVE\",\"inStock\":true,\"sameDay\":3,\"nextDay\":7,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"226.95\",\"listPrice\":\"382.00\",\"freight\":0},{\"part\":\"3757\",\"fam\":\"AC\",\"cat\":\"CON\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"COUPE ONLY; W\\/ DRIER ATTACHED\",\"inStock\":true,\"sameDay\":2,\"nextDay\":5,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"75.00\",\"listPrice\":\"133.00\",\"freight\":0},{\"part\":\"3680\",\"fam\":\"AC\",\"cat\":\"CON\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"SEDAN ONLY\",\"inStock\":true,\"sameDay\":5,\"nextDay\":7,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"63.50\",\"listPrice\":\"124.00\",\"freight\":0},{\"part\":\"3757\",\"fam\":\"AC\",\"cat\":\"CON\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"COUPE ONLY; W\\/ DRIER ATTACHED\",\"inStock\":true,\"sameDay\":2,\"nextDay\":5,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"75.00\",\"listPrice\":\"133.00\",\"freight\":0},{\"part\":\"3683\",\"fam\":\"AC\",\"cat\":\"CON\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"EXCEPT COUPE\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":10,\"deliverFrom\":\"GRN\",\"price\":\"74.00\",\"listPrice\":\"127.00\",\"freight\":0},{\"part\":\"4711679\",\"fam\":\"AC\",\"cat\":\"EVAP\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"SEDAN ONLY\",\"inStock\":true,\"sameDay\":0,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"SRA\",\"price\":\"74.95\",\"listPrice\":\"119.00\",\"freight\":0},{\"part\":\"4711679\",\"fam\":\"AC\",\"cat\":\"EVAP\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"SEDAN ONLY\",\"inStock\":true,\"sameDay\":0,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"SRA\",\"price\":\"74.95\",\"listPrice\":\"119.00\",\"freight\":0},{\"part\":\"3411309\",\"fam\":\"AC\",\"cat\":\"EXPD\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"COUPE\",\"inStock\":true,\"sameDay\":1,\"nextDay\":6,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"13.95\",\"listPrice\":\"23.00\",\"freight\":0},{\"part\":\"3411309\",\"fam\":\"AC\",\"cat\":\"EXPD\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"SEDAN\",\"inStock\":true,\"sameDay\":1,\"nextDay\":6,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"13.95\",\"listPrice\":\"23.00\",\"freight\":0},{\"part\":\"3411309\",\"fam\":\"AC\",\"cat\":\"EXPD\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"COUPE\",\"inStock\":true,\"sameDay\":1,\"nextDay\":6,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"13.95\",\"listPrice\":\"23.00\",\"freight\":0},{\"part\":\"3411309\",\"fam\":\"AC\",\"cat\":\"EXPD\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"SEDAN\",\"inStock\":true,\"sameDay\":1,\"nextDay\":6,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"13.95\",\"listPrice\":\"23.00\",\"freight\":0},{\"part\":\"1321278\",\"fam\":\"AC\",\"cat\":\"SEAL\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"ALL SEALS FOR AC\",\"inStock\":true,\"sameDay\":2,\"nextDay\":9,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"9.95\",\"listPrice\":\"15.66\",\"freight\":0},{\"part\":\"1321278\",\"fam\":\"AC\",\"cat\":\"SEAL\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"ALL SEALS FOR AC\",\"inStock\":true,\"sameDay\":2,\"nextDay\":9,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"9.95\",\"listPrice\":\"15.66\",\"freight\":0},{\"part\":\"HO14A\",\"fam\":\"FUEL\",\"cat\":\"FUEL\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\" \",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":10,\"deliverFrom\":\"LEM\",\"price\":\"121.00\",\"listPrice\":\"194.00\",\"freight\":20},{\"part\":\"HD38EL\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"DRIVER SIDE POWER, NON-HEATED; SEDAN ONLY\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":10,\"deliverFrom\":\"LIT\",\"price\":\"53.00\",\"listPrice\":\"80.00\",\"freight\":0},{\"part\":\"HD38L\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"DRIVER SIDE,  MANUAL, USA BUILT, SEDAN\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":2,\"deliverFrom\":\"AH\",\"price\":\"47.00\",\"listPrice\":\"72.00\",\"freight\":0},{\"part\":\"HD40EL\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"DRIVER SIDE,  POWER,  HEATED, FOLDING, BLACK, SEDAN\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":10,\"deliverFrom\":\"AH\",\"price\":\"81.00\",\"listPrice\":\"120.00\",\"freight\":0},{\"part\":\"HD55EL\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"DRIVER SIDE, POWER, HEATED, FOLDING, BLACK, COUPE\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":4,\"deliverFrom\":\"LEM\",\"price\":\"58.00\",\"listPrice\":\"87.00\",\"freight\":0},{\"part\":\"HD54EL\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"DRIVER SIDE, POWER, NON HTD, FOLDING, BLACK, COUPE\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":7,\"deliverFrom\":\"AH\",\"price\":\"52.00\",\"listPrice\":\"79.00\",\"freight\":0},{\"part\":\"HD38R\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"PASS. SIDE,  MANUAL, USA BUILT, SEDAN\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":2,\"deliverFrom\":\"AH\",\"price\":\"47.00\",\"listPrice\":\"72.00\",\"freight\":0},{\"part\":\"HD55ER\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"PASS. SIDE, POWER, HEATED, FOLDING, BLACK, COUPE\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":2,\"deliverFrom\":\"AH\",\"price\":\"58.00\",\"listPrice\":\"87.00\",\"freight\":0},{\"part\":\"HD40ER\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"PASS. SIDE, POWER, HEATED, FOLDING, BLACK, SEDAN\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":8,\"deliverFrom\":\"LEM\",\"price\":\"81.00\",\"listPrice\":\"120.00\",\"freight\":0},{\"part\":\"HD54ER\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"PASS. SIDE, POWER, NON HTD, FOLDING, BLACK, COUPE\",\"inStock\":true,\"sameDay\":0,\"nextDay\":0,\"days3to5\":2,\"deliverFrom\":\"LEM\",\"price\":\"58.00\",\"listPrice\":\"87.00\",\"freight\":0},{\"part\":\"HD38ER\",\"fam\":\"CRSH\",\"cat\":\"MIRR\",\"engLi\":0,\"engCi\":0,\"engCyl\":\" \",\"comment\":\"PASSENGER SIDE POWER, NON-HEATED; SEDAN ONLY\",\"inStock\":true,\"sameDay\":1,\"nextDay\":0,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"53.00\",\"listPrice\":\"80.00\",\"freight\":0},{\"part\":\"9642417\",\"fam\":\"AC\",\"cat\":\"ACKT\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"10S17C; 7 GROOVE; COUPE\",\"inStock\":0,\"sameDay\":1,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"\",\"price\":280.7,\"listPrice\":467.46,\"freight\":0},{\"part\":\"9642416\",\"fam\":\"AC\",\"cat\":\"ACKT\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\"10S17C; 7 GROOVE; SEDAN\",\"inStock\":0,\"sameDay\":1,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"\",\"price\":278.7,\"listPrice\":464.46,\"freight\":0},{\"part\":\"9642911\",\"fam\":\"AC\",\"cat\":\"ACKT\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"NEW; 10S17C; 6 GROOVE; COUPE\",\"inStock\":0,\"sameDay\":1,\"nextDay\":4,\"days3to5\":10,\"deliverFrom\":\"\",\"price\":289.7,\"listPrice\":483.46,\"freight\":0},{\"part\":\"9642551\",\"fam\":\"AC\",\"cat\":\"ACKT\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\"NEW; 10S17C; 6 GROOVE; SEDAN\",\"inStock\":0,\"sameDay\":1,\"nextDay\":6,\"days3to5\":10,\"deliverFrom\":\"\",\"price\":287.7,\"listPrice\":480.46,\"freight\":0},{\"part\":\"NON-STOCKING\",\"fam\":\"ENC\",\"cat\":\"HTR\",\"engLi\":2.4,\"engCi\":143,\"engCyl\":\"4\",\"comment\":\" \",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":0,\"listPrice\":0,\"freight\":0},{\"part\":\"NON-STOCKING\",\"fam\":\"ENC\",\"cat\":\"HTR\",\"engLi\":3,\"engCi\":181,\"engCyl\":\"6\",\"comment\":\" \",\"inStock\":false,\"sameDay\":0,\"nextDay\":0,\"days3to5\":0,\"deliverFrom\":\"\",\"price\":0,\"listPrice\":0,\"freight\":0},{\"part\":8011242,\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"AC FLUSH\",\"inStock\":true,\"sameDay\":5,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"14.95\",\"listPrice\":\"24.00\",\"freight\":0},{\"part\":\"PAG46-8\",\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"PAG OIL 46 - 8 OUNCE\",\"inStock\":true,\"sameDay\":5,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"7.95\",\"listPrice\":\"12.80\",\"freight\":0},{\"part\":\"PAG100-8\",\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"PAG OIL 100 - 8 OUNCE\",\"inStock\":true,\"sameDay\":3,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"6.95\",\"listPrice\":\"11.37\",\"freight\":0},{\"part\":\"PAG150-8\",\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"PAG OIL 150 - 8 OUNCE\",\"inStock\":true,\"sameDay\":3,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"7.95\",\"listPrice\":\"12.80\",\"freight\":0},{\"part\":8011259,\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"ICE 32 W\\/ UV DYE\",\"inStock\":true,\"sameDay\":3,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"12.95\",\"listPrice\":\"21.00\",\"freight\":0},{\"part\":\"R134A-30\",\"fam\":\"AC\",\"cat\":\"CHEM\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"30LB 134A REFRIGERANT\",\"inStock\":true,\"sameDay\":1,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"139.00\",\"listPrice\":\"204.00\",\"freight\":0},{\"part\":\"ANTIFREEZE\",\"fam\":\"ENC\",\"cat\":\"RAD\",\"engLi\":\"All\",\"engCi\":\"All\",\"engCyl\":\"All\",\"comment\":\"ANTIFREEZE\\/COOLANT 50\\/50 PREMIX (1 GAL)\",\"inStock\":true,\"sameDay\":10,\"nextDay\":10,\"days3to5\":10,\"deliverFrom\":\"TPA\",\"price\":\"7.50\",\"listPrice\":\"11.08\",\"freight\":0}]}");
                    
                    $('#vehicle').html(response.LookupReply.Year + ' ' + response.LookupReply.Make.toUpperCase() + ' ' + response.LookupReply.Model.toUpperCase());
                    $('#catalog').fadeIn();
                    $('.order-summary').show();

                    $('#details').dataTable().fnDestroy();
                    $('#details tbody').find('tr').remove();

                    $(response.AppCatalog).each(function () {
                        if (this.inStock == true) {
                            this.inStock = 'Yes';
                        } else {
                            this.inStock = 'No';
                        }

                        if (this.sameDay != 0) {
                            this.delivery = 'Next Day';
                        }
                        else if (this.nextDay != 0) {
                            this.delivery = '2 Days';
                        }
                        else {
                            this.delivery = '4-6 Days';
                        }

                        this.listPrice = accounting.formatNumber(this.listPrice);
                        this.price = accounting.formatNumber(this.price);
                        this.sellPrice = accounting.formatNumber(accounting.formatNumber(this.price) * .20 + accounting.formatNumber(this.price) * 1);

                        (user.Role === "customer") ? $('#details tbody').append($("#detailsTemplateCust").tmpl(this)) : $('#details tbody').append($("#detailsTemplateAdmin").tmpl(this));
                    });
                    $('#details').dataTable({ "bJQueryUI": true, "bPaginate": false /*"sPaginationType": "full_numbers"*/ });
                    $('#details').css({ 'width': '100%' });
                }
            );
        }
    </script>
</asp:Content>