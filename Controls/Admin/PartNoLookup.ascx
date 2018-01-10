<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PartNoLookup.ascx.vb" Inherits="Pigeon.PartNoLookup" %>


<div id="divInfo">
    <div style="margin-bottom: 10px;" class="form-search">
        <div class="input-append">
            <input id="txtAppNumber" name="AppNumber" placeholder="App Number" class="input-large search-query" size="16" type="text" /><button type="submit" id="btnSearch" class="btn">Get Part No.</button>
        </div>
    </div>

    <div>
        <h4 id="partlabel" style="display:none">Part No:</h4>
        <p id="result"></p>
    </div>
</div>


<script type="text/javascript">

    $('document').ready(function () {

        $('#txtAppNumber').keydown(function (event) {
            if (event.keyCode == 13) {
                GetPart();
            }
        });

        $('#btnSearch').click(function (e) {
            e.preventDefault();
            GetPart();
        });


    });

    function GetPart() {
        var urlMethod = "../TransmissionWebService.asmx/GetPartNo";

        var json = { 'appno': $('#txtAppNumber').val() };
        var jsonData = JSON.stringify(json);
        SendAjax(urlMethod, jsonData, function (msg) {
            info = jQuery.parseJSON(msg.d);
            //            console.log(VinInfo);
            $('#partlabel').show();
            $('#result').text(info);
        });
    }

</script>
