<%@ Page Title="Home" Language="vb" MasterPageFile="~/Pages/Pigeon.Master" AutoEventWireup="false"
    CodeBehind="Home.aspx.vb" Inherits="Pigeon.Home" %>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link href="../Styles/ninja-slider.css" rel="stylesheet" type="text/css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script src="../Scripts/ninjaVideoPlugin.js"></script>
    <script src="../Scripts/ninja-slider.js" type="text/javascript"></script>
    
     <style type="text/css">
        .carousel-indicators
{
    display:none;
}
.carousel { z-index: 10; } /* keeps this behind all content */
.carousel .item {
    
    width: 100%; height: 100%;
    -webkit-transition: opacity 15s;
    -moz-transition: opacity 15s;
    -ms-transition: opacity 15s;
    -o-transition: opacity 15s;
    transition: opacity 15s;
	 background-size: cover;
    -moz-background-size: cover;
 
}
.carousel-control{
    
margin-top: 100px;
    background: transparent;
    border: none;
    color: black;
    font-size: 100px;
    width: 5%;

}
  .carousel-inner > .item > img,
  .carousel-inner > .item > a > img {
      width: 90%;
      margin: auto;
  }

    </style>


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
         

            $('.tab-warranties').hide();

            GetAnnouncement();

            //$('a[href="' + uri.file + '"]').addClass('active').parents('li').addClass('active');
            
            if (user.Client == 'GO') {
                $('#Quick-links').show();
                $('.Quick-links').show();
                $('#menu-go').show();
            }

            if (user.Client == 'Autoway') {
                $('#Quick-links').show();
                $('.Quick-links').show();
                $('#menu-auto').show();
            }

            if (user.Client == 'FMP') {

                var Html = '<div id="ninja-slider"><div class="slider-inner"><ul><li><div class="video"><video controls data-autoplay="1" width="100%"><source src="/images/FVO PowerTrain Introduction.mp4" type="video/mp4" /></video></div> </li><li><a class="ns-img" href="/images/FMPSlider-01.png"></a></li><li><a class="ns-img" href="/images/FMPSlider-03.png"></a></li><li><a class="ns-img" href="/images/FMPSlider-05.png"></a></li></ul><div class="fs-icon"></div></div></div>';
                $("#anounce-slider").append(Html);
                //$('#FMPCarousel').carousel();
                
                //$('#FVPVideo').on("play",function () {
                //    $('#FMPCarousel').carousel('pause');
                //});
                //$('#FVPVideo').on("pause", function () {
                //    $('#FMPCarousel').carousel('cycle');
                //});
                $(".carousel-caption").click(function () {
                    $(location).attr('href', $(location).attr('origin') + '/Pages/WarrantyComparison.aspx');
                    //$('#FMPCarousel').carousel(1);
                    //$('#FMPCarousel').carousel("pause");
                });
                //$(".carousel-control").click(function () {
                //    $('#FMPCarousel').carousel();
                //});
               
            }

            if (user.Client == 'CK') {
              
                var Html = '<div style="margin-left:0;margin-top:20px;width:800px;" id="CKCarousel" class="carousel container slide" data-ride="carousel" data-interval="10000"><ol class="carousel-indicators"><li data-target="#CKCarousel" data-slide-to="0" class="active"></li><li data-target="#CKCarousel" data-slide-to="1"></li></ol><div class="carousel-inner" style="width:120%;"><div class="item active"><img src="/images/CKAnnouncement.png" alt="..."><div class="carousel-caption" style="background:transparent;cursor:pointer;"></div></div><div class="item"><img src="/images/CKVinAnnouncement.png" alt="..."></div><a class="left carousel-control" href="#CKCarousel" role="button" data-slide="prev"><span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#CKCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>';
                $("#anounce-slider").append(Html);
                $('#CKCarousel').carousel({ interval: 10000 });
                $(".carousel-caption").click(function () {
                    $('#CKCarousel').carousel(1);
                    $('#CKCarousel').carousel("pause");
                });
                $(".carousel-control").click(function () {
                    $('#CKCarousel').carousel();
                });

            }
           
            //if (user.Client == "CK" && user.Tier == "3") {
            //    $('.main').prepend('<div class="tutorial well"><h2><a target="_blank" href="../Docs/C&KAutoPartsTutorial.pdf">Click Here for Tutorial & Documentation</a></h2></div>');
            //}

            //if (user.Client == 'LarryMiller') {
            //    $('#tools').show();
            //}

            $('#links-table tbody tr').each(function () {
                $(this).hide();
            });

            $('#ShowTools').click(function () {
                if ($('#tools').height() == 190) {
                    $("#tools").animate({
                        height: "0px"
                    }, 1000);
                    $("#tools").hide();
                    $('#ShowTools').html('Show Quick Pricing Tools')
                }

                if ($('#tools').height() == 0) {
                    $("#tools").animate({
                        height: "190px"
                    }, 1000);
                    $("#tools").show();
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

            if (user.Client == 'CK') {
                $('.resources').hide();
            }
        });

        function PartNoSearch() {

            //Search Transfer Case for part number

            var urlMethod = "../HomeWebService.asmx/SearchTransferByPartNo";
            var json = { 'partno': $('#txtPartNo').val(), 'name': user.UserName, 'client': user.Client }
            var jsonData = JSON.stringify(json);
            $('#pricing-heading').html('Searching Transfer Case Catalog......');
            SendAjax(urlMethod, jsonData, function (msg) {
                SearchResult.TransferCase = jQuery.parseJSON(msg.d);

                //Search Differential for part number

                var urlMethod = "../HomeWebService.asmx/SearchDiffByPartNo";
                var json = { 'partno': $('#txtPartNo').val(), 'name': user.UserName, 'client': user.Client }
                var jsonData = JSON.stringify(json);
                $('#pricing-heading').html('Searching Differential Catalog......');
                SendAjax(urlMethod, jsonData, function (msg) {
                    SearchResult.Differential = jQuery.parseJSON(msg.d);

                    //Search Transmission for part number
                    //
                    var urlMethod = "../HomeWebService.asmx/SearchTransmissionByPartNo";
                    var json = { 'partno': $('#txtPartNo').val(), 'name': user.UserName, 'client': user.Client }
                    var jsonData = JSON.stringify(json);
                    $('#pricing-heading').html('Searching Transmission Catalog......');
                    SendAjax(urlMethod, jsonData, function (msg) {
                        SearchResult.Transmission = jQuery.parseJSON(msg.d);

                        //Search Engine for part number

                        var urlMethod = "../HomeWebService.asmx/SearchEngineByPartNo";
                        var json = { 'partno': $('#txtPartNo').val(), 'name': user.UserName, 'client': user.Client }
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
                                $("#tools").hide();
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
                                $("#tools").hide();
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
            var json = { 'Credential': 'Customer', 'client': user.Client }
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                //Announce = jQuery.parseJSON(msg.d);
                //$("#AnounceTemplate").tmpl(Announce).appendTo("#anounce-slider");
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
    <div class="main-content container-fluid ">
        <div class="row-fluid">
            <div class="span3">
                <div class="well directory">
                    <ul class="nav nav-list"></ul>
                </div>
            </div>
            <div class="span9">
                <%--<p id="anounce">Anouncements:</p>--%>
                <div id="anounce-slider" style="width:70%;height:100%;">
                </div>
                <div id="tools" style="display: none;padding-bottom:25px">
                    <hr />
                    <p class="tools-heading">
                        Quick Pricing Tools</p>
                    <br />
                    <div class="fivecol partNoSearch">
                        <p>
                            Search By Part Number.</p>
                        <hr />
                        <div class="searchForm">
                            <label>
                                Part Number:</label><input id="txtPartNo" type="text" /><br />
                            <input id="btnPartNo" type="button" class="btn btn-primary btn-small" value="Search" style="width: 100px; height: 30px;" />
                        </div>
                    </div>
                    <div class="fivecol vinNoSearch" style="display: none;">
                        <p>
                            Search by Vin Number</p>
                        <hr />
                        <div class="searchForm">
                            <label>
                                Part Number:</label><input id="Text1" type="text" /><br />
                            <input id="Button1" type="button" value="Search" style="width: 100px; height: 30px;" />
                        </div>
                    </div>
                </div>
                <br />
                <div id="pricing-info">
                    <div class='loader'>
                        <img style="display: none;" src="/images/ajax-loader-blue.gif" />
                        <span id="pricing-heading"></span><a id="ShowTools"></a>
                        <div id="pricing-table"></div>
                    </div>
                </div>
                <div class="row-fluid">
              
                    <hr />
                    <p class="tools-heading Quick-links" style="display: none;">
                        Quick Links, OEM Programs, Parts Specials & Promotions</p>
                    <div id="Quick-links" style="display: none;" class="span8">
                        <div id="menu-go" style="display:none">
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href='../Docs/GO36%20Month%20Warranty.pdf' target="_blank">Read about our 36 months/100,000 miles warranty</a></th><th><a href='../Docs/GO36%20Unlimited%20Warranty.pdf' target="_blank">Read about our 36 months/Unlimited miles warranty</a></th>
                                </tr>
                                
                            </table>
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href='http://parts.autonationnissanarapahoe.com/ ' target="_blank"><img src="../images/oeconnection.png" /></a></th><th><a href='http://autopartsbridge.com' target="_blank"><img src="../images/bridge.png" /></a></th><th><a href='http://parts-catalog.moparrepairconnection.com/catalog-2' target="_blank"><img src="../images/mopar.jpg" /></a></th>
                                </tr>
                                
                            </table>
                             <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href='http://parts.autonationnissanarapahoe.com/' target="_blank"><img src="../images/estore.png" /></a></th><th><a href='https://ford.partsrebates.com' target="_blank"><img src="../images/ford-moto.png" /></a></th><th><a href='http://toyotapartsandservice.com/oem-toyota-parts.do' target="_blank"><img src="../images/toyota.png" /></a></th>
                                </tr>
                                
                            </table>
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href='http://genuinegmparts.com/ShowPromotions.do' target="_blank"><img src="../images/gm.png" /></a></th><th><a href="../Docs/Rebate_Form_Preview.pdf"><img src="../images/mopar2.png" /></a></th>
                                </tr>
                                
                            </table>
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href="../Docs/Mopar 4Q 2012 PT Wholesale Rebate.pdf"><img src="../images/tracy.png" /></a></th>
                                    <!-- <th><a href="../Docs/CollisionPartsAdvantage_Flyer.pdf"><img src="../images/collision.png" /></a></th> -->
                                    <th><a href='http://www.parts.com' target="_blank"><img src="../images/parts_com_logo.jpg" /></a></th>
                                </tr>
                                
                            </table>

                        </div>
                        <div id="menu-auto"style="display:none">
                             <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href='http://www.moreoemparts.com/' target="_blank"><img src="../images/more.png" /></a></th><th><a href='http://gonissanarapahoeparts.com' target="_blank"><img src="../images/estore.png" /></a></th><th><a href='http://genuinegmparts.com/ShowPromotions.do' target="_blank"><img src="../images/gm.png" /></a></th>
                                </tr>
                                
                            </table>
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th><a href='http://www.parts.com' target="_blank"><img src="../images/parts_com_logo.jpg" /></a></th><th><a href='http://www.fordparts.com' target="_blank"><img src="../images/ford_parts_online.gif" /></a></th><th><a href='http://www.chevroletperformance.com/' target="_blank"><img src="../images/img_performance.jpg" /></a></th>
                                </tr>
                            </table>
                         
                        </div>
                    </div>
               
                                     
                </div>
            </div>
        </div>
    </div>
</asp:Content>
