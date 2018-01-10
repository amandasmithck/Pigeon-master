<%@ Page Title="Profile" Language="vb" MasterPageFile="~/Pages/Pigeon.Master" AutoEventWireup="false" CodeBehind="Profile.aspx.vb" Inherits="Pigeon.Profile1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $('.tab-warranties').hide();

            var UserID;
            GetUserID();

            if (user.UserName == 'twg') {
                $("#btnPassword").hide();
            }

            $("#btnPassword").click(function () {
                if ($("#newpassword").val() == $("#confirmpassword").val()) {
                    ChangePassword();
                } else {
                    alert("Please make sure Passwords matches in both text fields")
                }
            });

            $("#btnEmail").click(function () {
                if ($("#newemail").val() == $("#confirmemail").val()) {
                    ChangeEmail();
                } else {
                    alert("Please make sure Email matches in both text fields")
                }
            });
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
                    if (window.console) console.log(err.Message);
                }
            });
        }

        function GetUserID() {
            var urlMethod = "../CustomerManageWebService.asmx/GetUserID";
            var json = { 'username': user.UserName, 'client': user.Client }
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                UserID = jQuery.parseJSON(msg.d);
                console.log(UserID);
            });
        }

        function ChangePassword() {
            var urlMethod = "../CustomerManageWebService.asmx/ChangePassword";
            var json = { 'userid': UserID[0].UserID, 'client': user.Client, 'password': $("#newpassword").val() }
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                if (msg.d == true) {
                    $("#passwordsuccess").show()
                }
            });
        }

        function ChangeEmail() {
            var urlMethod = "../CustomerManageWebService.asmx/ChangeEmail";
            var json = { 'userid': UserID[0].UserID, 'client': user.Client, 'email': $("#newemail").val() }
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                if (msg.d == true) {
                    $("#emailsuccess").show()
                }
            });
        }
    </script>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12 main">
                <h3>
                    Change Password</h3>
                <br />
                <div class="small-form section">
                    <label>
                        New Password</label>
                    <input type="password" id="newpassword" />
                    <label>
                        Confirm Password</label>
                    <input type="password" id="confirmpassword" /><br />
                    <input class="button" type="button" value="Update" id="btnPassword" />
                    <p class="success" id="passwordsuccess" style="display: none;">
                        Information Updated.</p>
                    <div class="clear">
                    </div>
                </div>
                <hr />
                <h3>
                    Change Email</h3>
                <br />
                <div class="small-form section">
                    <label>
                        New Email</label>
                    <input type="text" id="newemail" />
                    <label>
                        Confirm Email</label>
                    <input type="text" id="confirmemail" /><br />
                    <input class="button" type="button" value="Update" id="btnEmail" />
                    <p class="success" id="emailsuccess" style="display: none;">
                        Information Updated.</p>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>