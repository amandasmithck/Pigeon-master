<%@ Page Language="vb" MasterPageFile="~/Pages/Pigeon.Master" AutoEventWireup="false" CodeBehind="Contacts.aspx.vb" Inherits="Pigeon.Contacts1" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $('document').ready(function () {
            $('.tab-warranties').hide();
            GetContacts();
        });

        function SendAjax(urlMethod, jsonData, returnFunction) {
            $.ajax({
                type: "POST",
                contentType: 'application/json',
                url: urlMethod,
                data: jsonData,
                //dataType: "json",
                success: function (msg) {
                    if (msg != null) {
                        returnFunction(msg);
                    }
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }
        var contacts;
        function GetContacts() {
            var urlMethod = "../CustomerManageWebService.asmx/GetContacts";

            var json = { 'Client': user.Client };
            var jsonData = JSON.stringify(json);
            SendAjax(urlMethod, jsonData, function (msg) {
                contacts = jQuery.parseJSON(msg.d);

                $(contacts).each(function () {
                    $('#contacts').append("<tr><td>" + this.Position + "</td><td>" + this.Name + "</td><td>" + this.Phone + "</td><td><a href='mailto:" + this.Email + "'>" + this.Email + "</a></td><td>");
                });
            });
        }
</script>

    <div class="container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <table id="contacts" class="table table-bordered table-striped">
                <tr>
                    <th>Position</th><th>Name</th><th>Phone</th><th>Email</th>
                </tr>
            </table>
        </div>
    </div>
</div>
</asp:Content>