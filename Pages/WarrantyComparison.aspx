<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master"  CodeBehind="WarrantyComparison.aspx.vb" Inherits="Pigeon.WarrantyComparison" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Styles/reset.css" rel="stylesheet" />
    <link href="/Styles/style.css" rel="stylesheet" />
    <%--<link href="/Styles/bootstrap.css" rel="stylesheet" />--%>
    <style type="text/css">
        .nav-list > li > a, .nav-list .nav-header {
  display: block;
 padding: 10px 15px;
     font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
     font-size:14px;
  margin-left: -15px;
  margin-right: -15px;
  text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);
}
        .cd-products-wrapper {
  overflow-x: auto;
  /* this fixes the buggy scrolling on webkit browsers - mobile devices only - when overflow property is applied */
  -webkit-overflow-scrolling: touch;
}
 
.cd-products-table .features {
  /* fixed left column - product properties list */
  position: absolute;
  z-index: 1;
  top: 0;
  left: 0;
  width: 120px;
}
 
.cd-products-columns {
  /* products list wrapper */
  width: 1200px; /* single column width * products number */
  margin-left: 120px; /* .features width */
}
@media only screen and (min-width: 1170px) {
  .cd-products-table.top-fixed .cd-products-columns > li {
    padding-top: 160px;
  }
 
  .cd-products-table.top-fixed .top-info {
    height: 160px;
    position: fixed;
    top: 0;
  }
 
  .cd-products-table.top-fixed .top-info h3 {
    transform: translateY(-116px);
  }
  
  .cd-products-table.top-fixed .top-info img {
    transform: translateY(-62px) scale(0.4);
  }
 
}
    </style>
      <div class="row-fluid">
            <div class="span3" style="margin-top:80px;">
                <div class="well directory">
                    <ul class="nav nav-list"></ul>
                </div>
            </div>
            <div class="span9">
<section class="cd-products-comparison-table">
	<header>
		<h2 style="margin-top:20px;margin-left:-50px">Warranty Comparison</h2>
        <!--Return the Database Numbers Next time-->
		<div class="actions" style="margin-top: 20px;">
            <a href="#0" class="filter" data-bind="click: function () { isShowAll(true) }" style="background-color:#e38a50;color:black;">Show All</a>
			<a href="#0" class="filter" data-bind="click: function () { isBasic(true); isShowAll(false); }" style="background-color:#e38a50;color:black;">Base</a>
			<a href="#0" class="filter" data-bind="click: function () { isBasic(false); isShowAll(false) }" style="background-color:#e38a50;color:black;">Enhanced</a>
		</div>
	</header>
 
	<div class="cd-products-table">
		<div class="features" >
			<div class="top-info"></div>
			<ul class="cd-features-list" data-bind="foreach: listOfItems">
				<li data-bind="text: itemName"></li>
			</ul>
		</div> <!-- .features -->
		
		<div class="cd-products-wrapper">
			<ul class="cd-products-columns" data-bind="foreach: warrantyInfo">
				<li class="product" data-bind=" visible: !isShowAll()? warrantyTypeID == 1 ? isBasic() : !isBasic():isShowAll()">
					<div class="top-info"  style="width:250px;">
                        <center>
						<%--<div class="check"></div>--%>
                        <div style="height:85px;">
						<img data-bind="attr: {src: imageLocation}" alt="LOGO">
                            </div>
						<h3 data-bind="text: vendorName"></h3>
					</div> <!-- .top-info -->
 </center>
					<ul class="cd-features-list" data-bind="foreach: itemInfo">
						<li data-bind="html: itemValue"></li>
					</ul>
				</li> <!-- .product -->
 
		<%--		<li class="product">
					<!-- product content here -->
				</li> <!-- .product -->--%>
 
				<!-- other products here -->
			</ul> <!-- .cd-products-columns -->
		</div> <!-- .cd-products-wrapper -->
		
<%--		<ul class="cd-table-navigation">
			<li><a href="#0" class="prev inactive">Prev</a></li>
			<li><a href="#0" class="next">Next</a></li>
		</ul>--%>
	</div> <!-- .cd-products-table -->
</section> <!-- .cd-products-comparison-table -->
                </div>
          </div>
    <script src="/Scripts/jquery-2.2.0.min.js"></script>
    <script src="/Scripts/knockout_3_2_0.js"></script>
    <script src="/Scripts/ajaxHelperViewModel.js"></script>
    <script src="/Scripts/modernizr.js"></script>
    <script src="/Scripts/main.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var warrantyComparisonViewModel= function()
            {
                warrantyInfo = ko.observableArray();
                listOfItems = ko.observableArray();
                isBasic = ko.observable(false);
                isShowAll = ko.observable(true);

                ko.utils.extend(self, new ajaxHelperViewModel())

                function getWarrantyInfo()
                {
                    var url = "/WarrantyWebService.asmx/GetComparisonInfo";
                    self.ajaxHelper(url, 'POST', null).done(function (response) {
                        warrantyInfo(response.d);
                        listOfItems(warrantyInfo()[2].itemInfo)
                    });
                }
                getWarrantyInfo();
                return
                {
                    warrantyInfo = warrantyInfo,
                    listOfItems = listOfItems,
                    isBasic=isBasic,
                    isShowAll=isShowAll
                };
            }
            ko.applyBindings(new warrantyComparisonViewModel());
            function productsTable(element) {
                this.element = element;
                this.table = this.element.children('.cd-products-table');
                this.productsWrapper = this.table.children('.cd-products-wrapper');
                this.tableColumns = this.productsWrapper.children('.cd-products-columns');
                this.products = this.tableColumns.children('.product');
                //additional properties here
                // bind table events
                this.bindEvents();
            }

            productsTable.prototype.bindEvents = function () {
                var self = this;

                self.productsWrapper.on('scroll', function () {
                    //detect scroll left inside products table
                });

                self.products.on('click', '.top-info', function () {
                    //add/remove .selected class to products 
                });

                self.filterBtn.on('click', function (event) {
                    //This would be Enhanced BUt i can use Kno
                    //filter products
                });
                //reset product selection
                self.resetBtn.on('click', function (event) {
                    //This would be Basic
                    //reset products visibility
                });

                this.navigation.on('click', 'a', function (event) {
                    //scroll inside products table - left/right arrows
                });
            }

            var comparisonTables = [];
            $('.cd-products-comparison-table').each(function () {
                //create a productsTable object for each .cd-products-comparison-table
                comparisonTables.push(new productsTable($(this)));
            });

            productsTable.prototype.updateLeftScrolling = function () {
                var scrollLeft = this.productsWrapper.scrollLeft();

                if (this.table.hasClass('top-fixed') && checkMQ() == 'desktop') setTranformX(this.productsTopInfo, '-' + scrollLeft);
            }
        });

    </script>
</asp:Content>
