<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="PricingUpdates.aspx.vb" Inherits="Pigeon.pricing_1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script id="dateTemplate" type="text/html">
            <option>${Date_val}</option>
    </script>
    <script id="Company_Template" type="text/html">
           <option>${Company_val}</option>
    </script>

     <script id="printTemplate" type="text/html">
        <tr>
            <td>${Date_Val}</td> 
           <td>${Part}</td> 
           <td>${Action}</td>   
           <td>${Part_Type}</td> 
           <td>${List}</td> 
           <td>${Cost}</td> 
           <td>${Core}</td> 
           <td>${Makes}</td> 
        </tr>
     </script>


    <script language="javascript" type="text/javascript">
        $('document').ready(function () {
            $('.tab-warranties').hide();
            $('#btnGo').click(function (e) {
                GetData();
                return false;
            });

            $('#btnExcel').hide();

            $.ajax({
                type: "POST",
                url: "../PricingWebService.asmx/GetPriceUpdateCompany",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (msg) {
                    var resp = jQuery.parseJSON(msg.d);

                    $("#Company").empty().append($("<option></option>").val("[-]").html("Please select"));



                    $.each(resp, function () {
                        $("#Company").append($('#Company_Template').tmpl(this));
                    });
                },
                error: function () {
                    alert("An error has occurred during processing your request.");
                }
            });

            $.ajax({
                type: "POST",
                url: "../PricingWebService.asmx/GetPriceUpdateDates",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var resp = jQuery.parseJSON(msg.d);

                    $("#update-dates").empty().append($("<option></option>").val("[-]").html("Please select"));
                    $("#update-dates").append($("<option></option>").val("0").html("Entire Catalog"));
                    $.each(resp, function () {
                        $("#update-dates").append($('#dateTemplate').tmpl(this));
                    });
                },
                error: function () {
                    alert("An error has occurred during processing your request.");
                }
            });

            function GetData() {
                $('#print-table').loadmask("Getting Price Updates...");
                $('#btnExcel').hide();
                var data = { date_val: $("#update-dates").val(), Company: $("#Company").val(), part_type: $("#part_types").val(), control: $("#selControlStock").val() };

                var jsonData = JSON.stringify(data);

                $.ajax({
                    type: "POST",
                    url: "../PricingWebService.asmx/GetPriceUpdates",
                    data: jsonData,
                    // data: "{'date_val': " + $("#update-dates").val() + ", 'Company': " + $("#Company").val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var resp = jQuery.parseJSON(msg.d);

                        $('#print-table tr:not(:first)').remove();

                        $.each(resp, function (i, n) {
                            if (this.Part_Type == 1) this.Part_Type = "Transmission";
                            if (this.Part_Type == 2) this.Part_Type = "Engine";
                            if (this.Part_Type == 3) this.Part_Type = "Differential";
                            if (this.Part_Type == 4) this.Part_Type = "Transfer Case";
                            if (this.Part_Type == 6) this.Part_Type = "Front Diff";
                            if (this.Part_Type == 7) this.Part_Type = "IFS";
                            if (this.Part_Type == 8) this.Part_Type = "IRS";
                            if (this.Part_Type == 9) this.Part_Type = "REAR DIFF";




                            $("#print-table").append($('#printTemplate').tmpl(this));
                            
                            

                        });
                        $('#print-table').unloadmask();
                        $('#btnExcel').show();
                    },
                    error: function () {
                        $('#print-table').unloadmask();
                        alert("An error has occurred during processing your request.");
                    }
                });
            }
        });
    </script>

   


    <form id="form1" >
    <div style="float:left">
        Select Company :
        <select id="Company">
        </select>
   
    </div>
    <div style="float:left">
        Select date : 
        <select id="update-dates">
        </select>
        </div>
 <div style="float:left">
         Select Part Type:
         <select id="part_types">
          <option>Please Select</option>
          <option value = "0">All</option>
          <option value = "1">Transmissions</option>
          <option value = "2">Engines</option>
        <option value = "3">Diffs</option>
        <option value = "4">Transfer Case</option>
         <option value = "6">FRONT DIFF</option>
         <option value = "7">IFS</option>
         <option value = "8">IRS</option>
          <option value = "9">REAR DIFF</option>
        </select>
 
        </div>

         <div style="float:left">
         Control Stock:
         <select id="selControlStock">
              <option value="na">N/A</option>
              <option value = "all">Current & Past</option>
              <option value = "current">Current Only</option>
              <option value = "past">Past Only</option>
          </select>
        </div>

        <div>
        <input id="btnGo" type="button" value="Get Data" class="btn btn-primary btn-small"</ >
        </div>
        </form>

          <div id="CalcPanel" class="DivStyle">
        
       
       
        <br />
        </div>
<form id="export-form" action="ExcelExport.aspx" method= "post"
onsubmit='$("#datatodisplay").val( $("<div>").append( $("#print-table").eq(0).clone() ).html() )'>
        <table name="print-table" id="print-table" class="table table-striped table-bordered">
        <tr>
            <th>Date</th>
            <th>Part</th>
            <th>Action</th>
            <th>Part Type</th>
            <th>List</th>
            <th>Cost</th>
            <th>Core</th>
            <th>Make</th>
        </tr>
        
        </table>
        <input type="hidden" id="datatodisplay" name="datatodisplay"  />
        <input id="btnExcel" type="submit" value="Download Excel" class="btn btn-success btn-large" />
</form>


    </asp:Content>