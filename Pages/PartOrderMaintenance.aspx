<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master"  CodeBehind="PartOrderMaintenance.aspx.vb"  Inherits="Pigeon.PartOrderMaintenance" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<!DOCTYPE html>
  <link href="../Styles/bootstrap2.3.2/bootstrap.css" rel="stylesheet" />   
    <link href="../Styles/sweetalert2.css" rel="stylesheet" />
    <link href="../Styles/PartOrderMaintenance.css" rel="stylesheet" />    

    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/bootstrap/bootstrap.js"></script>
    <script src="../Scripts/knockout_3_2_0.js" type="text/javascript"></script>
    <script src="../Scripts/ajaxHelperViewModel.js"></script>
    <script src="../Scripts/notificationHelperViewModel.js"></script>
    <script src="../Scripts/stringHelperViewModel.js"></script>
    <script src="../Scripts/moment.min.js" type="text/javascript" ></script>
    <script src="../Scripts/sweetalert2.min.js"></script>
 
      <script type="text/javascript">
        $(document).ready(function () {
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
                    //if (window.console) console.log(err.Message);
                }
            });
        }

        function GetOrderDetails() {
            console.log('GetOrderDetails');
            var urlMethod = "../AdminMaintenanceeWebService.asmx/GetPartOrder";
            var json = { 'OrderId': $("#orderID").val() }
            console.log(json);
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                console.log(msg);
            });
        }

          </script>


    <script type="text/javascript">

        $(document).ready(function () {

      
            var orderMaintenanceViewModel = function () {
                //ADDITIONAL VIEW MODELS
                ko.utils.extend(this, new ajaxHelperViewModel);
                ko.utils.extend(this, new stringHelperViewModel);
                ko.utils.extend(this, new notificationHelperViewModel);
                orderID = ko.observable();
                parts = ko.observableArray();
                part = ko.observable();
                updateItem = ko.observable();
            

                //VARIABLES
          
                btnOrderClick = function () {
                 
                    var url = "../AdminMaintenance.asmx/GetOrderPartsCollection";
                    var json = { 'orderid': parseInt(orderID()) , "client": "CK"}
                    var data = JSON.stringify(json);
                    orderTable.style.visibility = 'visible';
                    ajaxHelper(url, "POST", json).done(function (response) {
                        try {
                            if (response == null || response.d == null) {
                                notify("An error occurred while trying to retrieve parts.", false, null);
                                return false;
                            }

                            parts(response.d);
                           
                            //notify("Done",true,null)
                            return;
                        }
                        catch (err) {
                            notify("Display Error: " + err, false);
                            return false;
                        }
                    });
                };


                updateDefect = function (item) {
                    updateItem = 1;
                    part(item);                   
                    warningAlert();
                };
               
                updateIncorrect = function (item) {
                    updateItem = 2;
                    part(item);
                    warningAlert();
                };

                updateArriveDate = function (item) {
                    updateItem = 3;
                    part(item);
                    warningAlert();
                };

                warningAlert = function () {
                    swal({
                        title: "Are you sure?",
                        text: "",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes!",
                        cancelButtonText: "No, cancel!",
                        closeOnConfirm: true,
                        closeOnCancel: false
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            switch (updateItem) {
                                case 1:
                                    part().Defect = "False";
                                    break;
                                case 2:
                                    part().Incorrect = "False";
                                    break;
                                case 3:
                                    part().ArriveDate = null;
                                    break;
                            }
                                    var url = "../AdminMaintenance.asmx/PartOrderMaintenaceUpdate";
                                    var json = { 'part': part(), "client": "CK" }
                                    var data = JSON.stringify(json);
                                    ajaxHelper(url, "POST", json).done(function (response) {
                                        btnOrderClick();
                                        try {
                                            if (response == true || response.d == true) {
                                                swal("Your record has been updated.", "success");
                                            } 
                                            return;
                                        }
                                        catch (err) {
                                            notify("Display Error: " + err, false);
                                            return false;
                                        }
                                    });
                                    btnOrderClick();
                        } else {
                            swal("Cancelled", "Your record was not updated.", "error");
                        }
                    });
                }
                                //PAGE CONFIG SETTING METHODS
        
   
                pageLoad = function () {
                orderTable.style.visibility = 'hidden';
                orderID("");
                return;
            };



            //initiate model view
            pageLoad();

            return {
                btnOrderClick: btnOrderClick,
                orderID: orderID,
                parts: parts,
                part: part,
                updateDefect: updateDefect,
                updateArriveDate: updateArriveDate,
                updateIncorrect: updateIncorrect,
                updateItem: updateItem
            }

        };

            ko.applyBindings(new orderMaintenanceViewModel());

        });


    </script>

<html>
<head>
    <title></title>
</head>
    <body>
    <div style="padding-left:45px">
         <h3>Order Maintenance</h3>
        <br />       
          
        <label>Enter Order Number</label>&nbsp
 
      <p>
          <input type="text" id="orderID" data-bind="value: $root.orderID"/>

      </p>
      <p>
          <input class="button" type="button" value="Enter" id="btnOrderID" data-bind="click: $root.btnOrderClick"/>
      </p>
        </div>
        <div style="padding-left:45px" id="orderTable">
            <table class="orderTable">
                <thead>
                    <tr><th>Part ID</th><th>Part #</th><th>Description</th><th>Arrive Date</th><th>Defect</th><th>Incorrect</th><th>Clear Arrive Date</th><th>Clear Defect</th><th>Clear Incorrect</th></tr>
                </thead>
                <tbody data-bind="foreach: $root.parts">
                    <tr style="width:50px">                        
                        <td data-bind="text: PartID"></td>
                        <td data-bind="text: PartNo"></td>
                        <td data-bind="text: PartDescription"></td>
                        <td data-bind="text: ArriveDate, style: { color: ArriveDate == null || ArriveDate == false ? 'red' : 'black' }"></td>
                        <td><img style="width: 15px ; display: none;" src="../images/Check-icon.png" data-bind="style: { display: Defect == true ? 'block' : 'none' }" /></td>
                        <td><img style="width: 15px ; display: none" src="../images/Check-icon.png" data-bind="style: { display: Incorrect == true ? 'block' : 'none' }" /></td>
                        <td style="text-align:center"><button class="button" type="button" value="Clear Date" data-bind="click: $root.updateArriveDate">Clear</button></td>
                        <td style="text-align:center"><button class="button" type="button" value="Clear Date" data-bind="click: $root.updateDefect">Clear</button></td>
                        <td style="text-align:center"><button class="button" type="button" value="Clear Date" data-bind="click: $root.updateIncorrect">Clear</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
</body>
</html>
    </asp:Content>