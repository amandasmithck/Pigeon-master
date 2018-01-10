<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerCentral.aspx.vb" Inherits="Pigeon.DealerCentral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
   
    <title></title>
    <link href="Styles/common.css" rel="stylesheet" type="text/css" />
    <link href="Styles/lionbars.css" rel="stylesheet" type="text/css" />
    <link href="Styles/dealerCentral.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-latest.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"></script>
    <script src="Scripts/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.lionbars.0.3.js" type="text/javascript"></script>
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript'>
      google.load('visualization', '1', { 'packages': ['geochart'] });
      
//       maparr = new Array(
//              
//                );

      $('document').ready(function () {
          //          $(table).dataTable();
          $('.chart').hide();
          //          $('#info').fadeIn("slow");


          var data = new google.visualization.DataTable();
          data.addColumn('string', 'State');
          data.addColumn('number', 'Click to see each location in the State');
          // data.addColumn('string', 'Schedule');
          data.addRows([
                ['AL', 1],
                ['FL', 1],
                ['GA', 1],
                ['IL', 1],
                ['MD', 1],
                ['MN', 1],
                ['OH', 1],
                ['TN', 1],
                ['VA', 1],
                ['WA', 1],
                ['AZ', 1],
                ['CA', 1],
                ['TX', 1],
                ['CO', 1],
                ['NV', 1]
                ]);
          var options = { width: 500,
              //displayMode: 'markers',
              region: "US",
              resolution: "provinces",
              colorAxis: { colors: ['green', 'blue'] },
              datalessRegionColor: 'white',
              backgroundColor: { stroke: '#666', fill: '#C0C0C0' },
              'tooltip': { trigger: 'none' },
              legend: 'none',
              tooltip: { showColorCode: false }
          };

          var chart = new google.visualization.GeoChart(document.getElementById('map'));
          chart.draw(data, options);
          google.visualization.events.addListener(chart, 'select', function () {
              var selectedrow = chart.getSelection();
              var state = data.getValue(selectedrow[0].row, 0);
              $(".chartlabel").html('Please click on your store name below to enter the cataloging and ordering section.');
              $('.chart').fadeOut("fast");
              $('#' + state).fadeIn("slow");
              $('#maplabel').hide();
              $("#mapcontainer").animate({
                  height: "0%"
              }, 1000);
              $("#container").animate({
                  height: "468px"
              }, 1000);
              $("#map").fadeOut('slow');


          });
          $('.displaymap').hover(function () {
              $(this).css('cursor', 'pointer');
          });

          $('.displaymap').click(function () {
              $("#mapcontainer").animate({
                  height: "312px"
              }, 1000);
              $("#container").animate({
                  height: "780px"
              }, 1000);
              $("#map").fadeIn('slow');
              $('#maplabel').fadeIn('slow');
          });

      });

  </script>


</head>
<body>
   
    <form id="form1" runat="server">
    <div id="container">
    <div id="heading">
    <p><h1>AutoNation Remanufactured Powertrain and Differential Assemblies</h1></p>
    <h4>All remanufactured assemblies come with 2 warranty options:</h4><br />

          <h2> 1) 36 Month / 100,000 Mile Parts and Labor (payable at $60/hr)<br />
            2) 36 Month / Unlimited Mile Parts and Labor (payable at posted shop labor rate)<br />

    </h2>
    </div>
    <div id="mapcontainer"><div id="map"></div></div>
    <div id="maplabel">Please click on your state and find your store to log in.</div>
    
        <asp:SqlDataSource ID="dsAL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'AL' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsFL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'FL' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsGA" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'GA' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsIL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'IL' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsMD" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'MD' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsMN" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'MN' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsOH" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'OH' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsTN" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'TN' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsVA" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AutowayConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'VA' group by company, hyperion, address order by company"></asp:SqlDataSource>


         <asp:SqlDataSource ID="dsAZ" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GOConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'AZ' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsCA" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GOConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'CA' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsCO" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GOConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'CO' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsNV" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GOConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'NV' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsTX" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GOConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'TX' group by company, hyperion, address order by company"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsWA" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GOConnectionString %>" 
            SelectCommand="select company,hyperion,tblcompany.address from tbldealercentral inner join tblcompany on tbldealercentral.hyperion = tblcompany.customerno  where tblcompany.state = 'WA' group by company, hyperion, address order by company"></asp:SqlDataSource>

<div id="statecharts" class="pane">
        <div id="AL" class="chart">
        <h3>Alabama <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdAL" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsAL" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
        </div>
        
        <div id="FL" class="chart">
        <h3>Florida <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdFL" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsFL" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="GA" class="chart">
        <h3>Georgia <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdGA" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsGA" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        
        <div id="IL" class="chart">
        <h3>Illinois <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdIL" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsIL" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="MD" class="chart">
        <h3>Maryland <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdMD" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsMD" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="MN" class="chart">
        <h3>Minnesota <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdMN" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsMN" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="OH" class="chart">
        <h3>Ohio <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdOH" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsOH" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="TN" class="chart">
        <h3>Tennessee <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdTN" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsTN" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="VA" class="chart">
        <h3>Virginia <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from AutoWay Parts Center.</div>
        <asp:GridView ID="grdVA" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsVA" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="AZ" class="chart">
        <h3>Arizona <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from GO Parts Center.</div>
         <asp:GridView ID="grdAZ" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsAZ" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="CA" class="chart">
        <h3>California <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from GO Parts Center.</div>
         <asp:GridView ID="grdCA" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsCA" EnableModelValidation="True">
           <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="CO" class="chart">
        <h3>Colorado <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from GO Parts Center.</div>
         <asp:GridView ID="grdCO" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsCO" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="NV" class="chart">
        <h3>Nevada <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from GO Parts Center.</div>
         <asp:GridView ID="grdNV" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsNV" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="TX" class="chart">
        <h3>Texas <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from GO Parts Center.</div>
         <asp:GridView ID="grdTX" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsTX" EnableModelValidation="True">
           <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
         </div>
        <div id="WA" class="chart">
        <h3>Washington <span class="displaymap">Display Map</span></h3>
        <div class="chartlabel"></div>
        <div class="note" style="text-align: center;">All Invoicing will come from GO Parts Center.</div>
         <asp:GridView ID="grdWA" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsWA" EnableModelValidation="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField >
                <HeaderTemplate>
                Company
                </HeaderTemplate>
                <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text='<%# Bind("Company") %>'  ></asp:Label>
                <asp:Label ID="lblHyperion"  runat="server" Text='<%# Bind("Hyperion") %>' cssclass="hyperion"  ></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="address" HeaderText="Address" 
                    SortExpression="address" />
           </Columns>
        </asp:GridView>
        </div>
    </div>
    <hr />
        <img id="logo" src="images/pigeon_logo.png" width="100px" height="28px" />
    </div>
    </form>
</body>
</html>
