<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Calculator.ascx.vb" Inherits="Pigeon.Calculator" %>

<style type="text/css">
    #calculator {margin-top: 20px;}
    #calculator table input {width:90px;}
    #calculator table td {padding-right: 8px;}
</style>

<div id="calculator">
    <table style="width: 300px;font-size: 12px;">
        <tr>
            <td>Cost Price</td>
            <td id="margin-percent">Margin</td>
            <td id="sale-price">Sale Price</td>
        </tr>
        <tr>
            <td>
                <div class="input-prepend input-append">
                    <span class="add-on">$</span><input type="text" name="CostPrice" />
                 </div>
            </td>
            <td>
                <div class="input-prepend input-append">
                    <input type="text" name="Margin" /><span class="add-on">%</span>
                 </div>
            </td>
            <td>
                <div class="input-prepend input-append">
                    <span class="add-on">$</span><input type="text" name="SalePrice" />
                 </div>
            <td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    $('document').ready(function () {
        $('#calculator input[name=Margin]').attr('disabled', 'disabled');
        $('#calculator input[name=SalePrice]').attr('disabled', 'disabled');
        $('#calculator input[name=CostPrice]').keyup(function () {

            $('#calculator input[name=Margin]').val('');
            $('#calculator input[name=SalePrice]').val('');

            if (this.value.length == 0) {
                $('#calculator input[name=Margin]').attr('disabled', 'disabled');
                $('#calculator input[name=SalePrice]').attr('disabled', 'disabled');
            } else {
                $('#calculator input[name=Margin]').removeAttr('disabled');
                $('#calculator input[name=SalePrice]').removeAttr('disabled');
            }
        });

        $('#calculator input[name=SalePrice]').keyup(function () {
            if ($('#calculator input[name=CostPrice]').val() == '') $('#calculator input[name=CostPrice]').val('0');
            var cost = $('#calculator input[name=CostPrice]').val();
            $('#calculator input[name=Margin]').val(((parseInt(this.value) / cost - 1) * 100).toFixed(2));
        });

        $('#calculator input[name=Margin]').keyup(function () {
            if ($('#calculator input[name=CostPrice]').val() == '') $('#calculator input[name=CostPrice]').val('0');
            var cost = $('#calculator input[name=CostPrice]').val();
            $('#calculator input[name=SalePrice]').val((cost * (1 + (this.value / 100))).toFixed(2));
        });
    });
</script>