<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="WarrantyLookup.aspx.vb" Inherits="Pigeon.WarrantyLookup" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

     <script id="foundTemplate" type="text/html">
        <div class="hero-unit results">
				<h2>
					VIN Found!
				</h2>
				<form class="form-inline">
				<fieldset>
					<label>Sale Date: </label><span class="help">${OrderDate}</span><br />
                    <label>Vehicle: </label><span class="help">${AutoYear} ${AutoMake} ${AutoModel}</span><br />
                    <label>Mileage: </label><span class="help">${Mileage}</span><br />
				</fieldset>
			</form>
		</div>
        <div id="Div1" class="row-fluid results">
		<div class="span4">
			<form class="form-inline">
				<fieldset>
					 <legend>Seller Info </legend> 
                     <label>Dealership: </label><span class="help">${Company}</span><br />
                     <label>Address: </label><span class="help">${Address}</span><br />
                     <label>City, State: </label><span class="help">${City}, ${State} ${Zip}</span><br />
                     <label>Salesman: </label><span class="help">${Username}</span><br />
                     <label>Email: </label><span class="help">${Email}</span><br /> 
				</fieldset>
			</form>
		</div>
		<div class="span4">
			<form class="form-inline">
				<fieldset>
					 <legend>Order Info </legend> 
                     <label>Shop: </label><span class="help">${Shop}</span><br />
                     <label>Address: </label><span class="help">${ShopAddress}</span><br />
                     <label>City, State: </label><span class="help">${ShopCity}, ${ShopState} ${ShopZip}</span><br />
                     <label>Contact: </label><span class="help">${Contact}</span><br />
                     <label>PO: </label><span class="help">${PO}</span><br /> 
                     <label>Auto Owner: </label><span class="help">${AutoOwner}</span><br />  
				</fieldset>
			</form>
		</div>
		<div class="span4">
			<form class="form-inline">
				<fieldset>
					 <legend>Part Info </legend> 
                     <label>Part Type: </label><span class="help">${Description}</span><br />
                     <label>Part Number: </label><span class="help">${Part}</span><br />
                     <label>Warranty: </label><span class="help">${Warranty}</span><br />
                     <label>Sell Price: </label><span class="help">${accounting.formatMoney(TheirPrice)}</span><br /> 
                     <label>Core Price: </label><span class="help">${accounting.formatMoney(CorePrice)}</span><br />  
                     <label>List Price: </label><span class="help">${accounting.formatMoney(ListPrice)}</span><br />  
                     
				</fieldset>
			</form>
		</div>
        <div class="span4"></div>
        <div class="span4">
            <button  type="submit" class="btn btn-primary btnWarranty" data-toggle="warranty-modal">Initiate Warranty Claim</button>
        </div>
        <div class="span4"></div>
	</div>
     </script>
    <script id="notFoundTemplate" type="text/html">
        <div class="hero-unit results">
				<h1>
					Sorry No Match For VIN
				</h1>
			</div>
     </script>
    <script id="detailsTemplate" type="text/html">
        <div id="results-forms" class="row-fluid">
		<div class="span4">
			<form class="form-inline">
				<fieldset>
					 <legend>Legend</legend> <label>Label name</label><input type="text" /> <span class="help-block">Example block-level help text here.</span> <label class="checkbox"><input type="checkbox" /> Check me out</label> <button type="submit" class="btn">Submit</button>
				</fieldset>
			</form>
		</div>
		<div class="span4">
			<form class="form-inline">
				<fieldset>
					 <legend>Legend</legend> <label>Label name</label><input type="text" /> <span class="help-block">Example block-level help text here.</span> <label class="checkbox"><input type="checkbox" /> Check me out</label> <button type="submit" class="btn">Submit</button>
				</fieldset>
			</form>
		</div>
		<div class="span4">
			<form class="form-inline">
				<fieldset>
					 <legend>Legend</legend> <label>Label name</label><input type="text" /> <span class="help-block">Example block-level help text here.</span> <label class="checkbox"><input type="checkbox" /> Check me out</label> <button type="submit" class="btn">Submit</button>
				</fieldset>
			</form>
		</div>
	</div>
    </script>


    <script language="javascript" type="text/javascript">
        $('document').ready(function () {
            $('.tab-warranties').hide();

            $('#btn-search').click(function () {
                GetData();
                return false;
                
            });



        });

        function GetData() {

            $('#search-loader').show();
            $('.results').remove();
            
            var data = { 'vin': $('#vin').val(), 'client': user.Client };
            var jsonData = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "../WarrantyWebService.asmx/WarrantyLookup",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var resp = jQuery.parseJSON(msg.d);

                    if (resp.length == 0) {
                        $("#header").append($('#notFoundTemplate').tmpl())
                    } else {
                        $("#header").append($('#foundTemplate').tmpl(resp))
                        $(".btnWarranty").click(function () {
                            $('#warranty-modal').modal('show')
                            $("#txtPhone").mask("(999) 999-9999? x99999");
                        });
                        $('#warranty-send').click(function () {
                            if ($('#warranty-modal input:visible, #warranty-modal select').valid()) {
                                var data = { 'vin': $('#vin').val(), 'name': $('#txtName').val(), 'email': $('#txtEmail').val(), 'phone': $('#txtPhone').val(), 'issue': $('#txtIssue').val(), 'client': user.Client };
                                var jsonData = JSON.stringify(data);
                                 $.ajax({
                                    type: "POST",
                                    url: "../WarrantyWebService.asmx/SendWarranty",
                                    data: jsonData,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        var resp = jQuery.parseJSON(msg.d);
                                        if (resp == true) {
                                            alert("Claim sent. You will be contacted shortly by someone from the warranty department.");
                                            $('#warranty-modal').modal('hide');
                                        } else {
                                            alert('Error sending claim. Check your values and try again.');
                                         }
                                    }
                                });
                            }
                        });
                    }
                    

                    $('#search-loader').hide();
                },
                error: function () {
                    $('#search-loader').hide();
                    alert("An error has occurred during processing your request.");
                }
            });
        }
    </script>

   

    </form>
<div id="details" class="container-fluid">
	<div class="row-fluid">
		<div id="header" class="span12">
			<form class="form-search">
				<input id="vin" type="text" class="input-medium search-query" /> <button id="btn-search" type="submit" class="btn">VIN Search</button>
                <div id="search-loader" class="loader" style="display:none">
                        <img src="/images/ajax-loader-blue.gif" />
                </div>
			</form>
			
		</div>
	</div>
	
</div>
        <div id="warranty-modal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="warranty-Label" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="warranty-Label">Initiate Warranty Claim</h3>
        </div>
        <div class="modal-body">
            <form class="form-horizontal">
                <div class="control-group">
					 <label class="control-label" for="txtName">Name</label>
					<div class="controls">
						<input type="text" id="txtName" class="required" />
					</div>
				</div>
				<div class="control-group">
					 <label class="control-label" for="txtEmail">Email</label>
					<div class="controls">
						<input type="text" id="txtEmail" class="required"" />
					</div>
				</div>
                 <div class="control-group">
                            <label class="control-label" for="txtPhone">
                                Phone</label>
                            <div class="controls">
                                <input type="text" class="required input-large" id="txtPhone">
                            </div>
                        </div>
				<div class="control-group">
					 <label class="control-label" for="txtIssue">Describe Issue</label>
					<div class="controls">
                        <textarea rows ="5" class="input-large required" id="txtIssue"></textarea>
						
					</div>
				</div>
				
			</form>
        </div>
        <div class="modal-footer">
            
            <button id="warranty-close" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button id="warranty-send" class="btn btn-primary">Send</button>
        </div>
    </div>
    </asp:Content>
