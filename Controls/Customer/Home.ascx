<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Home.ascx.vb" Inherits="Pigeon.Home2" %>
  
  <link rel="stylesheet" type="text/css" href="../Styles/land.css">
  <link href="../Scripts/scroll/jquery.hoverscroll.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/bootstrap.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Scripts/jquery.cursorMessage.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
    
    <script src="../../Scripts/accounting.js" type="text/javascript"></script>

   <script src="../../Scripts/scroll/jquery.hoverscroll.js" type="text/javascript"></script>
   <script id="AnounceTemplate" type="text/html">
        <div>
            {{html Announcement}}
        </div>
   </script>
   
   
   <script id="PricingTemplate" type="text/html">
   
   <table class="tblpricing">
                        <thead>
                            <tr>
                                <td></td>
                                <td class="tblheading">Total<br/><span class='note'>NOT INCLUDING CORE</span></td>
                            </tr>                      
                        </thead>
                <tbody>
                {{if Transmission != null }}
                    <tr class='parttype'>
                        <td colspan = 2>Part type: Transmission</td>
                    </tr>
                    {{each Transmission[0].tiers}}
                        <tr>
                            <td>${Label}</td>
                            <td class="price">${accounting.formatMoney(Price)}</td>
                        </tr>
                    {{/each}}
                {{/if}}
                
                {{if Engine != null }}
                    <tr class='parttype'>
                        <td colspan = 2>Part type: Engine</td>
                    </tr>
                    {{each Engine[0].tiers}}
                        <tr>
                            <td>${Label}</td>
                             <td class="price">${accounting.formatMoney(Price)}</td>
                        </tr>
                    {{/each}}
                {{/if}}    

                {{if TransferCase != null }}
                    <tr class='parttype'>
                        <td colspan = 2>Part type: Transfer Case</td>
                    </tr>
                    {{each TransferCase.tiers}}
                        <tr>
                            <td>${Label}</td>
                             <td class="price">${accounting.formatMoney(Price)}</td>
                        </tr>
                    {{/each}}
                {{/if}}    

                {{if Differential != null }}
                    <tr class='parttype'>
                        <td colspan = 2>Part type: Differential</td>
                    </tr>
                    {{each Differential.tiers}}
                        <tr>
                            <td>${Label}</td>
                            <td class="price">${accounting.formatMoney(Price)}</td>
                        </tr>
                    {{/each}}
                {{/if}}    
                
                </tbody>

                <tfoot>
                                
                </tfoot>

            </table>

   </script>
   
   
    <script type="text/javascript">

        var SearchResult = {};

        SearchResult.TransferCase = {};
        SearchResult.Differential = {};
        SearchResult.Engine = {};
        SearchResult.Transmission = {};

        $(document).ready(function () {

            if ($('.current_client').text() == 'GO') {
                $('#Quick-links').show();
                $('.Quick-links').show();
                $('#menu-auto').hide();
                $('#menu-go').hoverscroll({
                    width: 700,
                    height: 200,
                    fixedArrows: true

                });
            }

            if ($('.current_client').text() == 'Autoway') {
                $('#Quick-links').show();
                $('.Quick-links').show();
                $('#menu-go').hide();
                $('#menu-auto').hoverscroll({
                    width: 700,
                    height: 200,
                    fixedArrows: true

                });
            }


            GetAnnouncement()

            if ($('.current_client').text() == "CK" && $('.current_tier').text() == "3") {
                $('.main').prepend('<div class="tutorial well"><h2><a target="_blank" href="../Pages/Assets/CK/Docs/C&KAutoPartsTutorial.pdf">Click Here for Tutorial & Documentation</a></h2></div>');
            }

            if ($('.current_client').text() != 'Tracy') {
                $('#tools').hide();
            }

            $('#links-table tbody tr').each(function () {
                $(this).hide();
            });


            $('.nav a').each(function () {
                var nav = $(this).text();

                $('#links-table tbody tr').each(function () {
                    var dir = $(this).attr('id');
                    $(this).attr('hoverid', hoverid)
                    //alert("1 DIR =" + dir + " NAV =" + nav);
                    if (nav == dir) {
                        //alert("2 DIR =" + dir + " NAV =" + nav);
                        $(this).show();
                    }
                });

            });

            $('.link-desc').hide();
            var hoverid = 0;
            $('#links-table tbody tr td.link-desc').each(function () {
                $(this).attr('id', "linkhover" + hoverid)
                hoverid = hoverid + 1
            });

            hoverid = 0;
            $('#links-table tbody tr').each(function () {
                $(this).attr('hoverid', hoverid)
                hoverid = hoverid + 1
            });

            var linkdesc;
            $('#links-table tbody tr').mouseover(function () {
                linkdesc = $('#linkhover' + $(this).attr('hoverid')).html()
                showCursorMessage(linkdesc);
            });
            $('#links-table tbody tr').mouseout($.hideCursorMessage);

            $('#ShowTools').click(function () {

                if ($('#tools').height() == 190) {
                    $("#tools").animate({
                        height: "0px"
                    }, 1000);
                    $('#ShowTools').html('Show Quick Pricing Tools')
                }

                if ($('#tools').height() == 0) {
                    $("#tools").animate({
                        height: "190px"
                    }, 1000);
                    $('#ShowTools').html('Hide Quick Pricing Tools')
                }

            });


            $('#txtPartNo').keydown(function (event) {
                if (event.keyCode == 13) {

                    if ($('#txtPartNo').val() != "") {
                        $('.loader img').show();
                        $('#btnPartNo').attr("disabled", "true");
                        $('#txtPartNo').attr("disabled", "true");
                        $('#pricing-heading').html('Searching');
                        PartNoSearch()
                    } else {
                        alert('Please Enter a Part Number');
                    }

                }

            });

            $('#btnPartNo').click(function () {
                if ($('#txtPartNo').val() != "") {
                    $('.loader img').show();
                    $('#btnPartNo').attr("disabled", "true");
                    $('#txtPartNo').attr("disabled", "true");
                    $('#pricing-heading').html('Searching');
                    PartNoSearch()
                } else {
                    alert('Please Enter a Part Number');
                }
            });

            if ($('.current_client').text() == 'CK') {
                $('.resources').hide();
            }
        });

        function showCursorMessage(linkdesc) {
            $.cursorMessage(linkdesc, { offsetX: 20 });


        }





        function PartNoSearch() {

            //Search Transfer Case for part number

            var urlMethod = "../TransferCaseWebService.asmx/GetPrice";
            var json = { 'partno': $('#txtPartNo').val(), 'name': $('.current_user').text(), 'client': $('.current_client').text() }
            var jsonData = JSON.stringify(json);
            $('#pricing-heading').html('Searching Transfer Case Catalog......');
            SendAjax(urlMethod, jsonData, function (msg) {
                SearchResult.TransferCase = jQuery.parseJSON(msg.d);

                //Search Differential for part number

                var urlMethod = "../DifferentialWebService.asmx/GetPrice";
                var json = { 'partno': $('#txtPartNo').val(), 'name': $('.current_user').text(), 'client': $('.current_client').text() }
                var jsonData = JSON.stringify(json);
                $('#pricing-heading').html('Searching Differential Catalog......');
                SendAjax(urlMethod, jsonData, function (msg) {
                    SearchResult.Differential = jQuery.parseJSON(msg.d);

                    //Search Transmission for part number

                    var urlMethod = "../HomeWebService.asmx/SearchTransmissionByPartNo";
                    var json = { 'partno': $('#txtPartNo').val(), 'name': $('.current_user').text(), 'client': $('.current_client').text() }
                    var jsonData = JSON.stringify(json);
                    $('#pricing-heading').html('Searching Transmission Catalog......');
                    SendAjax(urlMethod, jsonData, function (msg) {
                        SearchResult.Transmission = jQuery.parseJSON(msg.d);

                        //Search Engine for part number

                        var urlMethod = "../HomeWebService.asmx/SearchEngineByPartNo";
                        var json = { 'partno': $('#txtPartNo').val(), 'name': $('.current_user').text(), 'client': $('.current_client').text() }
                        var jsonData = JSON.stringify(json);
                        $('#pricing-heading').html('Searching Engine Catalog......');
                        SendAjax(urlMethod, jsonData, function (msg) {
                            SearchResult.Engine = jQuery.parseJSON(msg.d);
                            $('.loader img').hide();
                            $("#pricing-table").html("");
                            if (SearchResult.Differential == null & SearchResult.Engine == null & SearchResult.TransferCase == null & SearchResult.Transmission == null) {
                                $('#pricing-heading').html('No Result Found | ');
                                $("#tools").animate({
                                    height: "0px"
                                }, 1000);
                                $('#ShowTools').html('Show Quick Pricing Tools')
                                $('#btnPartNo').attr('value', 'Search');
                                $('#txtPartNo').removeAttr('disabled');
                                $('#btnPartNo').removeAttr('disabled');
                            } else {
                                $("#PricingTemplate").tmpl(SearchResult).appendTo("#pricing-table");
                                $('#pricing-heading').html('Showing results for Part number: ' + $('#txtPartNo').val() + ' | ');
                                $("#tools").animate({
                                    height: "0px"
                                }, 1000);
                                $('#ShowTools').html('Show Quick Pricing Tools')
                                $('#btnPartNo').attr('value', 'Search');
                                $('#txtPartNo').removeAttr('disabled');
                                $('#btnPartNo').removeAttr('disabled');
                            }
                        });
                    });
                });
            });
        }

        var Announce = {}
        function GetAnnouncement() {
            var urlMethod = "../HomeWebService.asmx/GetAnnouncement";
            var json = { 'Credential': 'Customer', 'client': $('.current_client').text() }
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                Announce = jQuery.parseJSON(msg.d);
                //console.log(Announce);
                $("#AnounceTemplate").tmpl(Announce).appendTo("#anounce-slider");

//                $('#anounce-slider').bxSlider({
//                    mode: 'vertical',
//                    speed: 1000,
//                    tickerSpeed: 5000,
//                    pager: true,
//                    pagerType: 'short',
//                    pagerShortSeparator: 'of',
//                    hideControlOnEnd: true,
//                    pagerLocation: 'bottom'
//                });
            });

           

        }


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
                    if (window.console) console.log(err.Message);
                }
            });
        }


    </script>

 <div id="container">
    <div class="row">
	    <div class="twocol">

        <div id="land-links">
                    <table id="links-table">
                    <tbody>
                    <tr id="Parts Search" class="link-heading">
                        <td>Parts Search</td>
                        <td class="link-desc"></td>
                    </tr>
                    <tr id="OEM">
                        <td><a href='PartsPortal.aspx'>OEM</a></td>
                        <td class="link-desc">Check pricing and availability on over 21 million unique OEM part numbers</td>
                    </tr>
                    <tr id="Engine" >
                        <td><a href='EngineSearch.aspx'>Engine</a></td>
                        <td class="link-desc">Complete line of Remanufactured Engines for all Makes and Models</td>
                    </tr>
                    <tr  id="Automatic Transmissions">
                        <td><a href='TransmissionSearch.aspx'>Automatic Transmissions</a></td>
                        <td class="link-desc">Complete line of Remanufactured Transmissions for all Makes and Models</td>
                    </tr>
                    <tr  id="Differential" style="display: none; ">
                        <td><a href='DifferentialSearch.aspx'>Differential</a></td>
                        <td class="link-desc">Complete line of Remanufactured Differentials for all Makes and Models</td>
                    </tr>
                    <tr id="Transfer Cases">
                        <td><a href='TransferCaseSearch.aspx'>Transfer Cases</a></td>
                        <td class="link-desc">Complete line of Remanufactured Transfer Cases for all Makes and Models</td>
                    </tr>
                    <tr id="Used Powertrain Assemblies">
                        <td><a href='UsedSearch.aspx'>Used Powertrain Assemblies</a></td>
                        <td class="link-desc">Complete line of Used Powertrain Assemblies for all Makes and Models</td>
                    </tr>
                    <tr id="Cooling">
                        <td><a href='CoolingSearch.aspx'>Cooling System</a></td>
                        <td class="link-desc">Complete line of Cooling System parts</td>
                    </tr>
            <tr id="History" class="link-heading">
                <td>History</td>
                <td class="link-desc"></td>
            </tr>
                    <tr id="OEM Orders">
                    <td ><a href='Orders.aspx'>All Orders</a></td>
                    <td class="link-desc">See all past Orders</td>
                    </tr>
            
                    <tr id="OEM Quotes">
                    <td ><a href='Quotes.aspx'>OEM Quotes</a></td>
                    <td class="link-desc">See all past OEM Quotes</td>
                    </tr>

                    <tr id="Engine Quotes">
                    <td><a href='EngineQuotes.aspx'>Engine Quotes</a></td>
                    <td class="link-desc">See all past Engine Quotes</td>
                    </tr>

                    <tr id="Differential Quotes">
                    <td><a href='DifferentialQuotes.aspx'>Differential Quotes</a></td>
                    <td class="link-desc">See all past Differential Quotes</td>
                    </tr>

                    <tr id="Transfer Case Quotes">
                    <td><a href='TransferCaseQuotes.aspx'>Transfer Case Quotes</a></td>
                    <td class="link-desc">See all past Transfer Case Quotes</td>
                    </tr>
                    
            <tr id="Tools" class="link-heading">
                <td >Tools</td>
                <td class="link-desc"></td>
            </tr>              
                    <tr id="Customer Manager">
                    <td><a href='CustomerManage.aspx'>Customer Manager</a></td>
                    <td class="link-desc">Manage customer info including pricing and users</td>
                    </tr>

                    <tr id="Transmission IMS">
                    <td><a href='IMS.aspx'>Transmission IMS</a></td>
                    <td class="link-desc">View expected and current transmissions stock levels</td>
                    </tr>

                    <tr id="Engine IMS">
                    <td><a href='EngineIMS.aspx'>Engine IMS</a></td>
                    <td class="link-desc">View expected and current engine stock levels</td>
                    </tr>

                    <tr id="Diff & T-Case IMS">
                    <td><a href='DifferentialIMS.aspx'>Diff & T-Case IMS</a></td>
                    <td class="link-desc">View expected and current differential and transfer case stock levels</td>
                    </tr>

                    


             <tr id="OEM" class="link-heading resources">
                <td >Don't know your OEM Part number? Try these resources!</td>
                <td class="link-desc"></td>
            </tr>
            <tr id="OEM" class="resources">
            <td ><a href='http://www.moreoemparts.com/' target="_blank">MoreOEMParts.com</a></td>
            <td class="link-desc">Parts cataloging & Pictures</td>
            </tr>
            <tr class="GO-OEM resources">
            <td ><a href='https://www.dealers-mopar.com/moparone/all_makes/lookup.action' target="_blank"><img src="../Pages/Assets/GO/images/mopar.jpg" /></a></td>
            <td class="link-desc">Parts cataloging & Pictures</td>
            </tr>
            <tr class="GO-OEM resources">
            <td ><a href='http://www.nissanpartsrewards.com' target="_blank"><img src="../Pages/Assets/GO/images/nissan.jpg" /></a></td>
            <td class="link-desc">Parts cataloging & Pictures</td>
            </tr>
            <tr class="GO-OEM resources">
            <td ><a href='http://gonissanarapahoeparts.com' target="_blank"><img src="../Pages/Assets/GO/images/estore.jpg" /></a></td>
            <td class="link-desc">Parts cataloging & Pictures</td>
            </tr>
            
            </tbody>
            
            </table>
        </div>
       </div>
            <div class="eightcol main">
              <%--  <div>
                  We have added the ability for you to now view all of your OEM and Reman Orders. Simply click all orders from History menu.
              </div>--%>
       <p id="anounce"> Anouncements:</p>
             <div id="anounce-slider">
          
             </div>

                   
                <div id="tools">
                <hr /> 
                    <p class="tools-heading">Quick Pricing Tools</p><br />
                    <div class="fivecol partNoSearch">
                        <p>Search By Part Number.</p>
                        <hr />
                        <div class="searchForm">
                            
                                <label>Part Number:</label><input id="txtPartNo" type="text"  /><br />
                                <input id="btnPartNo" type="button" value="Search" style="width:100px; height:30px; " />
                           
                        </div>
                    </div>  
                    <div class="fivecol vinNoSearch" style="display:none;" >
                        <p>Search by Vin Number</p>
                        <hr />
                        <div class="searchForm">
                            
                                <label>Part Number:</label><input id="Text1" type="text"  /><br />
                                <input id="Button1" type="button" value="Search" style="width:100px; height:30px; " />
                           
                        </div>
                    </div>
                    
                 
                 </div> 
                 <br />

                 <div id="pricing-info">
                   <div class='loader'>
                        <img style="display:none;" src="/images/ajax-loader-blue.gif" />
                        <span id="pricing-heading"></span><a id="ShowTools"></a>
                        <div id="pricing-table"></div>
                   </div>
                   
                 
                 </div>
                 <div class="row">
                        <hr />
                        <p class="tools-heading Quick-links" style="display:none;">Quick Links, OEM Programs, Parts Specials & Promotion</p>
                        
                     <div id="Quick-links" style="display:none;">
                            
                                <ul id="menu-go">
                                    <li><a href='http://www.moreoemparts.com/' target="_blank"><img src="../Pages/Assets/GO/images/more.png" /></a></li>
                                    <li class="go"><a href='http://oeconnection.com/ ' target="_blank"><img src="../Pages/Assets/GO/images/oeconnection.png" /></a></li>
                                    <li class="go"><a href='http://autopartsbridge.com' target="_blank"><img src="../Pages/Assets/GO/images/bridge.png" /></a></li>
                                    <li class="go"><a href='https://www.dealers-mopar.com/moparone/all_makes/lookup.action' target="_blank"><img src="../Pages/Assets/GO/images/mopar.jpg" /></a></li>
                                    <li class="go"><a href='http://www.nissanpartsrewards.com' target="_blank"><img src="../Pages/Assets/GO/images/nissan.png" /></a></li>
                                    <li class="go"><a href='http://gonissanarapahoeparts.com' target="_blank"><img src="../Pages/Assets/GO/images/estore.png" /></a></li>
                                    <li class="go"><a href='https://ford.partsrebates.com' target="_blank"><img src="../Pages/Assets/GO/images/ford-moto.png" /></a></li>
                                    <li class="go"><a href='http://toyotapartsandservice.com/oem-toyota-parts.do' target="_blank"><img src="../Pages/Assets/GO/images/toyota.png" /></a></li>
                                    <li class="go"><a href="../Pages/Assets/GO/Docs/Go-Reman-Flyer2.pdf"><img src="../Pages/Assets/GO/images/toyo-denso.png" /></a></li>
                                    <li class="go"><a href='http://genuinegmparts.com/ShowPromotions.do' target="_blank"><img src="../Pages/Assets/GO/images/gm.png" /></a></li>
                                    <li class="go"><a href='http://www.youtube.com/watch?v=P0xLbUF4AKI&feature=youtube_gdata_player' target="_blank"><img src="../Pages/Assets/GO/images/oereman.png" /></a></li>
                                    <li class="go"><a href="../Pages/Assets/GO/Docs/Rebate_Form_Preview.pdf"><img src="../Pages/Assets/GO/images/mopar2.png" /></a></li>
                                    <li class="go"><a href="../Pages/Assets/GO/Docs/Tracy Mopar 3Q 2012 PT Wholesale Rebate.pdf"><img src="../Pages/Assets/GO/images/tracy.png" /></a></li>
                                    <li class="go"><a href="../Pages/Assets/GO/Docs/CollisionPartsAdvantage_Flyer.pdf"><img src="../Pages/Assets/GO/images/collision.png" /></a></li>
                                    <li class="go"><a href="../Pages/Assets/GO/Docs/AutoNation_0512_MT_full%20page.pdf"><img src="../Pages/Assets/GO/images/freon.png" /></a></li>
                                    <li class="go"><a href='http://www.parts.com' target="_blank"><img src="../Pages/Assets/Autoway/images/parts.jpg" /></a></li>
                                    
                                </ul>

                                <ul id="menu-auto">

                                    <li><a href='http://www.moreoemparts.com/' target="_blank"><img src="../Pages/Assets/Autoway/images/more.png" /></a></li>
                                    
                                   <li class="go"><a href='http://gonissanarapahoeparts.com' target="_blank"><img src="../Pages/Assets/Autoway/images/estore.png" /></a></li>
                                   
                                    <li class="go"><a href='http://genuinegmparts.com/ShowPromotions.do' target="_blank"><img src="../Pages/Assets/Autoway/images/gm.png" /></a></li>
                                    
                                    <li class="go"><a href='http://www.parts.com' target="_blank"><img src="../Pages/Assets/Autoway/images/parts.jpg" /></a></li>
                                    
                                    <li class="go"><a href='http://www.fordparts.com' target="_blank"><img src="../Pages/Assets/Autoway/images/ford-parts.jpg" /></a></li>
                               <li class="go"><a href='http://www.chevroletperformance.com/' target="_blank"><img src="../Pages/Assets/Autoway/images/gmperformance.jpg" /></a></li>
                                </ul>
                            

                        </div>
                 
                 </div>
            </div>
       </div>
 </div>

           
       


        
