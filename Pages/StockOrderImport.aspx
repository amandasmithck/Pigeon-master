<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Pigeon.Master" CodeBehind="StockOrderImport.aspx.vb" Inherits="Pigeon.StockOrderImport" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

    

    <script language="javascript" type="text/javascript">
        $('document').ready(function () {
            $('.tab-warranties').hide();

           



        });

        
    </script>

   

    </form>
<div class="container-fluid">
        <div class="row-fluid" style="margin-bottom: 2em;overflow: visible;height:80px;">
            <div class="span12">
                <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:CKConnectionString %>' SelectCommand="SELECT * FROM [tblPigeonClients] ORDER BY [Client]"></asp:SqlDataSource>
        1. Enter dates(mm/dd/yy)<br /><br />
        <asp:TextBox ID="txtOrderDate" runat="server"></asp:TextBox> Stock Order Date<br />
        <asp:TextBox ID="txtETADate" runat="server"></asp:TextBox> ETA Date<br />
        <asp:TextBox ID="txtArriveDate" runat="server"></asp:TextBox> Arrive Date(if left blank stock will be placed in Awaiting Arrival in IMS)<br />
        <asp:TextBox ID="txtInventoryPODate" runat="server"></asp:TextBox> Inventory/Vendor PO Paid Date
        <br /><br />
        2. Select file <asp:FileUpload ID="FileUpload1" runat="server" /> (must be a csv file with field names exactly as above. also note no $ in prices)<br /><br />
        <img src="../images/Screen%20Shot%202012-02-23%20at%2012.24.08%20PM.png" /><br /><br />

        3. Select Client<br /><br />
        <asp:DropDownList ID="cboClient" runat="server" DataSourceID="SqlDataSource1" DataTextField="Client" DataValueField="CKCustomerNo"></asp:DropDownList>
        <br />
        
        <br />
        4. Select type of import<br /><br />
        <%--<asp:Button ID="AutowayTrans" runat="server" Text="Autoway Trans" /><br /><br />--%>
        <%--<asp:Button ID="TracyTrans" runat="server" Text="Tracy Trans" />--%>
         <%--<asp:Button ID="TracyEng" runat="server" Text="Tracy Eng" />--%>
        <%--   <asp:Button ID="TracyZumb" runat="server" Text="Tracy Zumbrota Diffs and T Cases" /><br /><br />--%>
          <%--<asp:Button ID="GoTrans" runat="server" Text="Go Trans" />--%>
           <%--<asp:Button ID="GOEngine" runat="server" Text="Go Engine" />--%>
           <%--<asp:Button ID="CKTrans" runat="server" Text="C&K Trans" />--%>
        <asp:Button ID="btnTransConsign" runat="server" Text="Trans Consignment" /><br /><br />
        <asp:Button ID="btnEngineConsign" runat="server" Text="Engine Consignment" /><br /><br />
        <asp:Button ID="btnTransPurchase" runat="server" Text="Trans Purchase" /><br /><br />
        <asp:Button ID="btnEnginePurchase" runat="server" Text="Engine Purchase" /><br /><br />
        
        
        Results<br />
        <asp:Label ID="lblstock" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblpartorder" runat="server"></asp:Label>
        <br /><br />
    </div>
    </form>
       
            </div>
        </div>
</div>

    </asp:Content>