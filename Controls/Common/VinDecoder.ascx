<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="VinDecoder.ascx.vb" Inherits="Pigeon.VinDecoder" %>

<div id="divVinInfo">
    <div style="margin-bottom: 10px;" class="form-search">
        <div class="input-append">
            <input id="txtVinNumber" name="VinNumber" placeholder="VIN" class="input-large search-query" size="16" type="text" /><button type="submit" id="btnVin" class="btn">Decode</button>
        </div>
    </div>

    <table style="display:none;font-size: 11px;" class="table table-bordered table-condensed vin-result">
        <tr>
            <td>Year</td>
            <td>Make</td>
            <td>Model</td>
            <td>Trim</td>
            <td>Style</td>
            <td>Drive</td>
            <td>Cylinders</td>
            <td>Liters</td>
          <%--  <td>Trans Type</td>
            <td>Trans Speed</td>--%>
            <td>Fuel Type</td>
        </tr>
        <tr>
            <td class="vinResult" id="VinYear"></td>
            <td class="vinResult" id="VinMake"></td>
            <td class="vinResult" id="VinModel"></td>
            <td class="vinResult" id="VinTrim"></td>
            <td class="vinResult" id="VinStyle"></td>
            <td class="vinResult" id="VinDrive"></td>
            <td class="vinResult" id="VinCylinders"></td>
            <td class="vinResult" id="VinLiters"></td>
         <%--   <td class="vinResult" id="VinTType"></td>
            <td class="vinResult" id="VinTSpeed"></td>--%>
            <td class="vinResult" id="VinFuelType"></td>
        </tr>
    </table>
</div>

<script type="text/javascript">

    $('document').ready(function () {

        $('#txtVinNumber').keydown(function (event) {
            if (event.keyCode == 13) {
                GetVin();
            }
        });

        $('#btnVin').click(function (e) {
            e.preventDefault();
            GetVin();
        });
        $('#txtVinNumber').focus(function () {
            $('#btnVin').val('Decode')
        });

    });

    function GetVin() {
        var VinInfo = {}
        var urlMethod = "../PigeonWebService.asmx/DecodeVIN";

        var json = { 'vin': $('#txtVinNumber').val() };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            VinInfo = jQuery.parseJSON(msg.d);
            //            console.log(VinInfo);
            if (VinInfo == null) {
                $('#btnVin').val('VIN Not Found')
                $('.vin-result').hide();
            } else {
                $('.vin-result').fadeIn();

                $('#btnVin').val('Decode')
                $('#VinYear').html(VinInfo.Year)
                $('#VinMake').html(VinInfo.Make)
                $('#VinModel').html(VinInfo.Model)
                $('#VinLiters').html(VinInfo.Liters)
                $('#VinCylinders').html(VinInfo.Cylinders)
                $('#VinDrive').html(VinInfo.Drive)

                if (VinInfo.TransmissionSpeed != "0") {
                    $('#VinTSpeed').html(VinInfo.TransmissionSpeed);
                } else {
                    $('#VinTSpeed').html("-")
                };

                if (VinInfo.TransmissionType != "") {
                    $('#VinTType').html(VinInfo.TransmissionType);
                } else {
                    $('#VinTType').html("-")
                };

                $('#VinStyle').html(VinInfo.Style)
                $('#VinTrim').html(VinInfo.Trim)
                $('#VinFuelType').html(VinInfo.FuelType)
            }

        });
    }

</script>