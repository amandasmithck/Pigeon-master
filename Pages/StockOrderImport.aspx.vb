Public Class StockOrderImport
    Inherits System.Web.UI.Page
    '  Dim myConnection2 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '  Dim myConnection1 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '  Dim myConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)

    '  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '  End Sub
    '  Private Sub UploadFile(ByVal client As String, ByVal parttype As String)
    '      'delete temp table

    '      Dim sqlComm3 As New SqlCommand("delete from tempcertifiedstock", myConnection)
    '      myConnection.Open()
    '      sqlComm3.ExecuteNonQuery()
    '      myConnection.Close()


    '      'upload
    '      FileUpload1.SaveAs(Server.MapPath("stock.csv"))
    '      'load to temp table
    '      Dim dt As DataTable = BuildDataTable2(Server.MapPath("stock.csv"))

    '      Dim dtForDB As New DataTable()
    '      dtForDB.Columns.Add("Inv#")
    '      dtForDB.Columns.Add("PN#")
    '      dtForDB.Columns.Add("SN#")
    '      dtForDB.Columns.Add("Unit")
    '      dtForDB.Columns.Add("Core")
    '      dtForDB.Columns.Add("Total")
    '      dtForDB.Columns.Add("Warehouse")


    '      For Each dr As DataRow In dt.Rows
    '          If UCase(dr("Inv#").ToString()) = "INV#" Then
    '          Else
    '              dtForDB.Rows.Add(dr("Inv#").ToString(), dr("PN#").ToString(), dr("SN#").ToString(), dr("Unit").ToString(), dr("Core").ToString(), dr("Total").ToString(), dr("Warehouse").ToString())
    '          End If


    '      Next


    '      'insert into tblstocks for client
    '      Dim x As Long = 0

    '      For Each dr2 As DataRow In dtForDB.Rows
    '          Dim strsql As String
    '          Dim strlocation As String

    '          If client = "Tracy" Then
    '              Dim sqlCommGetWarehouse As New SqlCommand("select name from tblremanwarehouses where id = '" & dr2("Warehouse").ToString() & "'", myConnection1)
    '              myConnection1.Open()
    '              strlocation = sqlCommGetWarehouse.ExecuteScalar
    '              myConnection1.Close()
    '          Else
    '              strlocation = "Unknown"
    '          End If

    '          If txtArriveDate.Text = "" Or IsDBNull(txtArriveDate.Text) Or txtArriveDate.Text = Nothing Then
    '              strsql = "Insert into tblStock(Part,sn,eta, type, WarehouseID) Values ('" & dr2("PN#").ToString() & "','" & GetGoodSN(dr2("SN#").ToString()) & "','" & Me.txtETADate.Text & "','" & parttype & "','" & dr2("Warehouse").ToString() & "')"
    '          Else
    '              strsql = "Insert into tblStock(Part,sn,location,eta,arrive, type, WarehouseID) Values ('" & dr2("PN#").ToString() & "','" & GetGoodSN(dr2("SN#").ToString()) & "','" & strlocation & "','" & Me.txtETADate.Text & "','" & Me.txtArriveDate.Text & "','" & parttype & "','" & dr2("Warehouse").ToString() & "')"
    '          End If
    '          Dim sqlComm2 As New SqlCommand(strsql, setclientconnectionstring(client))
    '          clientConnection.Open()
    '          sqlComm2.ExecuteNonQuery()
    '          clientConnection.Close()
    '          x = x + 1





    '          'insert into temp table
    '          strsql = "Insert into tempcertifiedstock(inv#,part#,sn#,unit,core,subtotal, warehouse) Values ('" & dr2("INV#").ToString() & "','" & dr2("PN#").ToString() & "','" & GetGoodSN(dr2("SN#").ToString()) & "','" & dr2("Unit").ToString() & "','" & dr2("Core").ToString() & "','" & dr2("Total").ToString() & "','" & dr2("Warehouse").ToString() & "')"

    '          Dim sqlComm4 As New SqlCommand(strsql, myConnection)
    '          myConnection.Open()
    '          sqlComm4.ExecuteNonQuery()
    '          myConnection.Close()

    '      Next

    '      Me.lblstock.Text = x & " items added to " & client & " stock"

    '  End Sub

    '  Private Function BuildDataTable2(ByVal fileFullPath As String) As DataTable
    '      Dim myTable As New DataTable("MyTable")


    '      myTable.Columns.Add("Inv#")
    '      myTable.Columns.Add("PN#")
    '      myTable.Columns.Add("SN#")
    '      myTable.Columns.Add("Unit")
    '      myTable.Columns.Add("Core")
    '      myTable.Columns.Add("Total")
    '      myTable.Columns.Add("Warehouse")

    '      Using Reader As New Microsoft.VisualBasic.FileIO.
    'TextFieldParser(fileFullPath)

    '          Reader.TextFieldType =
    '             Microsoft.VisualBasic.FileIO.FieldType.Delimited
    '          Reader.SetDelimiters(","c)
    '          Dim currentRow As String()
    '          While Not Reader.EndOfData
    '              Try
    '                  currentRow = Reader.ReadFields()
    '                  'Dim currentField As String
    '                  'For Each currentField In currentRow
    '                  '    MsgBox(currentField)
    '                  'Next
    '                  myTable.Rows.Add(currentRow)
    '              Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
    '                  MsgBox("Line " & ex.Message &
    '                  "is not valid and will be skipped.")
    '              End Try
    '          End While
    '      End Using
    '      Return myTable
    '  End Function
    '  Private Function GetGoodSN(ByVal sn As String)
    '      Dim strNewSN As String
    '      Dim x As Integer = 2

    '      strNewSN = sn
    '      Dim sqlComm As New SqlCommand("select PartDescription2 from tblpartorder where PartDescription2 like '" & sn & "'", myConnection2)
    '      myConnection2.Open()
    '      Dim r = sqlComm.ExecuteReader()
    '      While r.Read()
    '          If r("PartDescription2").ToString = strNewSN Then
    '              strNewSN = sn & "-" & x
    '          End If
    '          x = x + 1
    '      End While
    '      myConnection2.Close()

    '      Return strNewSN
    '  End Function

    '  Private Sub btnTransConsign_Click(sender As Object, e As EventArgs) Handles btnTransConsign.Click



    '      UploadFile(Me.cboClient.SelectedItem.Text, 1)



    '      Dim x As Long = 0
    '      Dim sqlComm2b As New SqlCommand("SELECT * from tempcertifiedstock", myConnection2)
    '      myConnection2.Open()
    '      Dim r = sqlComm2b.ExecuteReader()
    '      While r.Read()

    '          'insert tblOrder

    '          Dim order = New tblOrder

    '          'order.AutoMake = "Ford"
    '          'order.AutoModel = "Nothing"
    '          'order.AutoYear = "2011"
    '          'order.CustomerNo = Me.cboCompany2.SelectedItems(0).Value
    '          order.CustomerNo = "9997C"
    '          order.DateOrdered = Me.txtOrderDate.Text
    '          'order.Drive = Me.cboDrive2.CurrentValue
    '          order.Drive = "FWD"
    '          order.EnteredBy = Page.User.Identity.Name
    '          order.Transmission = "Automatic"
    '          order.JustaField = 1

    '          'insert tblPartOrder
    '          Dim detail = New tblPartOrder
    '          With detail
    '              .DateEntered = Me.txtOrderDate.Text
    '              .DateOrdered = Me.txtOrderDate.Text
    '              .EnteredBy = Page.User.Identity.Name
    '              .PartDescription = "Reman Transmission"
    '              .Weight = 0
    '              .PartDescription2 = r("SN#")
    '              .Quantity = 1
    '              .SellPrice = 0
    '              .Vendor = 13086
    '              .PreviousPartID = 0
    '              .CoreCredited = 0
    '              .CoreReceived = 0
    '              .Defect = 0
    '              .DefectCredited = 0
    '              .DefectRepaired = 0
    '              .DefectReturned = 0
    '              .Cancelled = 0
    '              .NeedsRefresh = 0
    '              .ReturnLost = 0
    '              '.Warranty = "36/100"
    '              ' .ArrivalDate = "8/1/11"
    '              .SupplementalPart = 0
    '              .Wholesale = 0
    '              .Incorrect = 0
    '              .IncorrectReturned = 0
    '              .IncorrectCredited = 0
    '              .HotCore = 0
    '              .ProblemCore = 0
    '              .VendorVerified = 1
    '              .LateReviewed = 0
    '              .ShippingPrice = 0
    '              .CoreShippingPrice = 0
    '              .CustShippingPrice = 0
    '              .Core = 1
    '              .CustCoreShippingPrice = 0
    '              .CorePrice = Convert.ToDecimal(r("core"))
    '              .CostPrice = Convert.ToDecimal(r("unit"))
    '              .Livingston = 0
    '              .CoreLivingston = 0
    '              .WarrantyCost = 0
    '              '.ArriveDate = "7/29/11"
    '              '.ExpShipDate = "7/27/11"
    '              .PartNo = r("PART#")
    '              .Shipper = 10827
    '              .VendorInvoiceNo = r("INV#").ToString.Replace("rj", "")


    '              .tblOrder = order


    '          End With





    '          Using context = New OrderDataContext
    '              context.tblOrders. _
    '              InsertOnSubmit(order)
    '              context.SubmitChanges()


    '              'create inventory asset and po

    '              Dim strsql As String = "insert into tblinvoices(orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid) values (" & order.OrderID & ", '" & Me.txtOrderDate.Text & "',136,192,13086," & Convert.ToDecimal(r("unit") + r("core")) & "," & Convert.ToDecimal(r("unit") + r("core")) & ")"
    '              Dim sqlComm As New SqlCommand(strsql, myConnection1)
    '              myConnection1.Open()
    '              sqlComm.ExecuteNonQuery()
    '              myConnection1.Close()

    '              Dim strsql2 As String = "insert into tblinvoices(orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid, datepaid, invoiceno) values (" & order.OrderID & ", '" & Me.txtOrderDate.Text & "',137,192,13086," & Convert.ToDecimal(r("unit") + r("core")) & "," & Convert.ToDecimal(r("unit") + r("core")) & ",'" & Me.txtOrderDate.Text & "','" & r("INV#").ToString.Replace("rj", "") & "')"
    '              Dim sqlComm2 As New SqlCommand(strsql2, myConnection1)
    '              myConnection1.Open()
    '              sqlComm2.ExecuteNonQuery()
    '              myConnection1.Close()

    '              'update warehouse
    '              Dim strWarehouse As String = "Update tblpartorder set warehouse = (select name from tblremanwarehouses where id = '" & r("warehouse") & "') where orderid = " & order.OrderID
    '              Dim sqlCommWarehouse As New SqlCommand(strWarehouse, myConnection1)
    '              myConnection1.Open()
    '              sqlCommWarehouse.ExecuteNonQuery()
    '              myConnection1.Close()


    '          End Using
    '          'Exit While
    '          x = x + 1
    '      End While
    '      r.Close()
    '      myConnection2.Close()

    '      Me.lblpartorder.Text = x & " orders added to C&K database"
    '  End Sub

    '  Protected Sub btnEngineConsign_Click(sender As Object, e As EventArgs) Handles btnEngineConsign.Click

    '      UploadFile(Me.cboClient.SelectedItem.Text, 2)
    '      Dim x As Long = 0
    '      Dim sqlComm2b As New SqlCommand("SELECT * from tempcertifiedstock", myConnection2)
    '      myConnection2.Open()
    '      Dim r = sqlComm2b.ExecuteReader()
    '      While r.Read()

    '          'insert tblOrder

    '          Dim order = New tblOrder

    '          'order.AutoMake = "Ford"
    '          'order.AutoModel = "Nothing"
    '          'order.AutoYear = "2011"
    '          'order.CustomerNo = Me.cboCompany2.SelectedItems(0).Value
    '          order.CustomerNo = "9997C"
    '          order.DateOrdered = Me.txtOrderDate.Text
    '          'order.Drive = Me.cboDrive2.CurrentValue
    '          order.Drive = "FWD"
    '          order.EnteredBy = Page.User.Identity.Name
    '          order.Transmission = "Automatic"
    '          order.JustaField = 1

    '          'insert tblPartOrder
    '          Dim detail = New tblPartOrder
    '          With detail
    '              .DateEntered = Me.txtOrderDate.Text
    '              .DateOrdered = Me.txtOrderDate.Text
    '              .EnteredBy = Page.User.Identity.Name
    '              .PartDescription = "Reman Engine"
    '              .Weight = 0
    '              .PartDescription2 = r("SN#")
    '              .Quantity = 1
    '              .SellPrice = 0
    '              .Vendor = 13103
    '              .PreviousPartID = 0
    '              .CoreCredited = 0
    '              .CoreReceived = 0
    '              .Defect = 0
    '              .DefectCredited = 0
    '              .DefectRepaired = 0
    '              .DefectReturned = 0
    '              .Cancelled = 0
    '              .NeedsRefresh = 0
    '              .ReturnLost = 0
    '              '.Warranty = "36/100"
    '              ' .ArrivalDate = "8/1/11"
    '              .SupplementalPart = 0
    '              .Wholesale = 0
    '              .Incorrect = 0
    '              .IncorrectReturned = 0
    '              .IncorrectCredited = 0
    '              .HotCore = 0
    '              .ProblemCore = 0
    '              .VendorVerified = 1
    '              .LateReviewed = 0
    '              .ShippingPrice = 0
    '              .CoreShippingPrice = 0
    '              .CustShippingPrice = 0
    '              .Core = 1
    '              .CustCoreShippingPrice = 0
    '              .CorePrice = Convert.ToDecimal(r("core"))
    '              .CostPrice = Convert.ToDecimal(r("unit"))
    '              .Livingston = 0
    '              .CoreLivingston = 0
    '              .WarrantyCost = 0
    '              '.ArriveDate = "7/29/11"
    '              '.ExpShipDate = "7/27/11"
    '              .PartNo = r("PART#")
    '              .Shipper = 10827
    '              .VendorInvoiceNo = r("INV#").ToString.Replace("rj", "")


    '              .tblOrder = order


    '          End With





    '          Using context = New OrderDataContext
    '              context.tblOrders. _
    '              InsertOnSubmit(order)
    '              context.SubmitChanges()


    '              'create inventory invoice asset & po

    '              Dim strsql As String = "insert into tblinvoices(invoiceno, orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid) values (" & r("INV#").ToString.Replace("rj", "") & ", " & order.OrderID & ", '" & Me.txtOrderDate.Text & "',136,192,13103," & Convert.ToDecimal(r("unit") + r("core")) & "," & Convert.ToDecimal(r("unit") + r("core")) & ")"
    '              Dim sqlComm As New SqlCommand(strsql, myConnection1)
    '              myConnection1.Open()
    '              sqlComm.ExecuteNonQuery()
    '              myConnection1.Close()

    '              Dim strsql2 As String
    '              If txtInventoryPODate.Text = "" Or IsDBNull(txtInventoryPODate.Text) Or txtInventoryPODate.Text = Nothing Then
    '                  strsql2 = "insert into tblinvoices(invoiceno, orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid) values (" & r("INV#").ToString.Replace("rj", "") & ", " & order.OrderID & ", '" & Me.txtOrderDate.Text & "',137,192,13103," & Convert.ToDecimal(r("unit") + r("core")) & "," & Convert.ToDecimal(r("unit") + r("core")) & ")"
    '              Else
    '                  strsql2 = "insert into tblinvoices(invoiceno, orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid, datepaid) values (" & r("INV#").ToString.Replace("rj", "") & ", " & order.OrderID & ", '" & Me.txtOrderDate.Text & "',137,192,13103," & Convert.ToDecimal(r("unit") + r("core")) & "," & Convert.ToDecimal(r("unit") + r("core")) & ",'" & txtInventoryPODate.Text & ")"

    '              End If

    '              Dim sqlComm2 As New SqlCommand(strsql2, myConnection1)
    '              myConnection1.Open()
    '              sqlComm2.ExecuteNonQuery()
    '              myConnection1.Close()

    '              'update warehouse
    '              Dim strWarehouse As String = "Update tblpartorder set warehouse = (select name from tblremanwarehouses where id = '" & r("warehouse") & "') where orderid = " & order.OrderID
    '              Dim sqlCommWarehouse As New SqlCommand(strWarehouse, myConnection1)
    '              myConnection1.Open()
    '              sqlCommWarehouse.ExecuteNonQuery()
    '              myConnection1.Close()


    '          End Using
    '          'Exit While
    '          x = x + 1
    '      End While
    '      r.Close()
    '      myConnection2.Close()
    '      Me.lblpartorder.Text = x & " orders added to C&K database"
    '  End Sub

    '  Protected Sub btnTransPurchaseTrans_Click(sender As Object, e As EventArgs) Handles btnTransPurchase.Click

    '      UploadFile(Me.cboClient.SelectedItem.Text, 1)
    '      Dim x As Long = 0
    '      Dim sqlComm2b As New SqlCommand("SELECT * from tempcertifiedstock", myConnection2)
    '      myConnection2.Open()
    '      Dim r = sqlComm2b.ExecuteReader()
    '      While r.Read()

    '          'insert tblOrder

    '          Dim order = New tblOrder

    '          'order.AutoMake = "Ford"
    '          'order.AutoModel = "Nothing"
    '          'order.AutoYear = "2011"
    '          'order.CustomerNo = Me.cboCompany2.SelectedItems(0).Value
    '          order.CustomerNo = Me.cboClient.SelectedItem.Value
    '          order.DateOrdered = txtOrderDate.Text
    '          'order.Drive = Me.cboDrive2.CurrentValue
    '          order.Drive = "FWD"
    '          order.EnteredBy = Page.User.Identity.Name
    '          order.Transmission = "Automatic"
    '          order.JustaField = 1

    '          'insert tblPartOrder
    '          Dim detail = New tblPartOrder
    '          With detail
    '              .DateEntered = txtOrderDate.Text
    '              .DateOrdered = txtOrderDate.Text
    '              .EnteredBy = Page.User.Identity.Name
    '              .PartDescription = "Reman Transmission"
    '              .Weight = 0
    '              .PartDescription2 = r("SN#")
    '              .Quantity = 1
    '              .SellPrice = Convert.ToDecimal(((r("unit")) * 1.03) + 170)
    '              .Vendor = 13100
    '              .PreviousPartID = 0
    '              .CoreCredited = 0
    '              .CoreReceived = 1
    '              .Defect = 0
    '              .DefectCredited = 0
    '              .DefectRepaired = 0
    '              .DefectReturned = 0
    '              .Cancelled = 0
    '              .NeedsRefresh = 0
    '              .ReturnLost = 0
    '              '.Warranty = "36/100"
    '              .ArrivalDate = txtOrderDate.Text
    '              .SupplementalPart = 0
    '              .Wholesale = 0
    '              .Incorrect = 0
    '              .IncorrectReturned = 0
    '              .IncorrectCredited = 0
    '              .HotCore = 0
    '              .ProblemCore = 0
    '              .VendorVerified = 1
    '              .LateReviewed = 0
    '              .ShippingPrice = 0
    '              .CoreShippingPrice = 0
    '              .CustShippingPrice = 0
    '              .Core = 1
    '              .CustCoreShippingPrice = 0
    '              .CorePrice = Convert.ToDecimal(r("core"))
    '              .CostPrice = Convert.ToDecimal(r("unit"))
    '              .Livingston = 0
    '              .CoreLivingston = 0
    '              .WarrantyCost = 0
    '              .ArriveDate = txtOrderDate.Text
    '              .ExpShipDate = txtOrderDate.Text
    '              .PartNo = r("part#")
    '              .Shipper = 10827
    '              .VendorInvoiceNo = r("inv#").ToString.Replace("rj", "")


    '              .tblOrder = order


    '          End With





    '          Using context = New OrderDataContext
    '              context.tblOrders. _
    '              InsertOnSubmit(order)
    '              context.SubmitChanges()



    '              'ck invoice and corerefund
    '              db2.usp_InvCK(order.OrderID, Page.User.Identity.Name)



    '              'vendor invoice
    '              Dim strsql2 As String
    '              db2.usp_InvVen(order.OrderID, Page.User.Identity.Name)
    '              If txtInventoryPODate.Text <> "" And IsDBNull(txtInventoryPODate.Text) = False And txtInventoryPODate.Text <> Nothing Then
    '                  strsql2 = "update tblinvoices set invoiceno = '" & r("inv#").ToString.Replace("rj", "") & "', datepaid = '" & txtInventoryPODate.Text & "', amountpaid=amount where invoicetypeid = 2 and orderid = " & order.OrderID
    '              Else
    '                  strsql2 = "update tblinvoices set invoiceno = '" & r("inv#").ToString.Replace("rj", "") & "', amountpaid=0 where invoicetypeid = 2 and orderid = " & order.OrderID
    '              End If
    '              Dim sqlComm2 As New SqlCommand(strsql2, myConnection1)
    '              myConnection1.Open()
    '              sqlComm2.ExecuteNonQuery()
    '              myConnection1.Close()

    '              'core credit
    '              db2.usp_InvCoreCred(order.OrderID, Page.User.Identity.Name)


    '              'change invoice date
    '              If txtInventoryPODate.Text <> "" And IsDBNull(txtInventoryPODate.Text) = False And txtInventoryPODate.Text <> Nothing Then
    '                  Dim strsql As String = "update tblinvoices set dateentered = '" & txtInventoryPODate.Text & "' where orderid = " & order.OrderID
    '                  Dim sqlComm As New SqlCommand(strsql, myConnection1)
    '                  myConnection1.Open()
    '                  sqlComm.ExecuteNonQuery()
    '                  myConnection1.Close()
    '              End If

    '              'update warehouse
    '              Dim strWarehouse As String = "Update tblpartorder set warehouse = (select name from tblremanwarehouses where id = '" & r("warehouse") & "') where orderid = " & order.OrderID
    '              Dim sqlCommWarehouse As New SqlCommand(strWarehouse, myConnection1)
    '              myConnection1.Open()
    '              sqlCommWarehouse.ExecuteNonQuery()
    '              myConnection1.Close()


    '          End Using
    '          'Exit While
    '          x = x + 1
    '      End While
    '      r.Close()
    '      myConnection2.Close()
    '      Me.lblpartorder.Text = x & " orders added to C&K database"
    '  End Sub

    '  Protected Sub btnEnginePurchase_Click(sender As Object, e As EventArgs) Handles btnEnginePurchase.Click

    '      UploadFile(Me.cboClient.SelectedItem.Text, 2)
    '      Dim x As Long = 0
    '      Dim sqlComm2b As New SqlCommand("SELECT * from tempcertifiedstock", myConnection2)
    '      myConnection2.Open()
    '      Dim r = sqlComm2b.ExecuteReader()
    '      While r.Read()

    '          'insert tblOrder

    '          Dim order = New tblOrder

    '          'order.AutoMake = "Ford"
    '          'order.AutoModel = "Nothing"
    '          'order.AutoYear = "2011"
    '          'order.CustomerNo = Me.cboCompany2.SelectedItems(0).Value
    '          order.CustomerNo = Me.cboClient.SelectedItem.Value
    '          order.DateOrdered = txtOrderDate.Text
    '          'order.Drive = Me.cboDrive2.CurrentValue
    '          order.Drive = "FWD"
    '          order.EnteredBy = Page.User.Identity.Name
    '          order.Transmission = "Automatic"
    '          order.JustaField = 1

    '          'insert tblPartOrder
    '          Dim detail = New tblPartOrder
    '          With detail
    '              .DateEntered = txtOrderDate.Text
    '              .DateOrdered = txtOrderDate.Text
    '              .EnteredBy = Page.User.Identity.Name
    '              .PartDescription = "Reman Engine"
    '              .Weight = 0
    '              .PartDescription2 = r("SN#")
    '              .Quantity = 1
    '              .SellPrice = Convert.ToDecimal(((r("unit")) + 245))
    '              .Vendor = 13113
    '              .PreviousPartID = 0
    '              .CoreCredited = 0
    '              .CoreReceived = 1
    '              .Defect = 0
    '              .DefectCredited = 0
    '              .DefectRepaired = 0
    '              .DefectReturned = 0
    '              .Cancelled = 0
    '              .NeedsRefresh = 0
    '              .ReturnLost = 0
    '              '.Warranty = "36/100"
    '              .ArrivalDate = txtOrderDate.Text
    '              .SupplementalPart = 0
    '              .Wholesale = 0
    '              .Incorrect = 0
    '              .IncorrectReturned = 0
    '              .IncorrectCredited = 0
    '              .HotCore = 0
    '              .ProblemCore = 0
    '              .VendorVerified = 1
    '              .LateReviewed = 0
    '              .ShippingPrice = 0
    '              .CoreShippingPrice = 0
    '              .CustShippingPrice = 0
    '              .Core = 1
    '              .CustCoreShippingPrice = 0
    '              .CorePrice = Convert.ToDecimal(r("core"))
    '              .CostPrice = Convert.ToDecimal(r("unit"))
    '              .Livingston = 0
    '              .CoreLivingston = 0
    '              .WarrantyCost = 0
    '              .ArriveDate = txtOrderDate.Text
    '              .ExpShipDate = txtOrderDate.Text
    '              .PartNo = r("part#")
    '              .Shipper = 10827
    '              .VendorInvoiceNo = r("inv#").ToString.Replace("rj", "")


    '              .tblOrder = order


    '          End With





    '          Using context = New OrderDataContext
    '              context.tblOrders. _
    '              InsertOnSubmit(order)
    '              context.SubmitChanges()

    '              'ck invoice and corerefund
    '              db2.usp_InvCK(order.OrderID, Page.User.Identity.Name)




    '              'vendor invoice
    '              Dim strsql2 As String
    '              db2.usp_InvVen(order.OrderID, Page.User.Identity.Name)
    '              If txtInventoryPODate.Text <> "" And IsDBNull(txtInventoryPODate.Text) = False And txtInventoryPODate.Text <> Nothing Then
    '                  strsql2 = "update tblinvoices set invoiceno = '" & r("inv#").ToString.Replace("rj", "") & "', datepaid = '" & txtInventoryPODate.Text & "', amountpaid=amount where invoicetypeid = 2 and orderid = " & order.OrderID
    '              Else
    '                  strsql2 = "update tblinvoices set invoiceno = '" & r("inv#").ToString.Replace("rj", "") & "', amountpaid=0 where invoicetypeid = 2 and orderid = " & order.OrderID
    '              End If
    '              Dim sqlComm2 As New SqlCommand(strsql2, myConnection1)
    '              myConnection1.Open()
    '              sqlComm2.ExecuteNonQuery()
    '              myConnection1.Close()

    '              'core credit
    '              db2.usp_InvCoreCred(order.OrderID, Page.User.Identity.Name)


    '              'change invoice date
    '              If txtInventoryPODate.Text <> "" And IsDBNull(txtInventoryPODate.Text) = False And txtInventoryPODate.Text <> Nothing Then
    '                  Dim strsql As String = "update tblinvoices set dateentered = '" & txtInventoryPODate.Text & "' where orderid = " & order.OrderID
    '                  Dim sqlComm As New SqlCommand(strsql, myConnection1)
    '                  myConnection1.Open()
    '                  sqlComm.ExecuteNonQuery()
    '                  myConnection1.Close()
    '              End If

    '              'update warehouse
    '              Dim strWarehouse As String = "Update tblpartorder set warehouse = (select name from tblremanwarehouses where id = '" & r("warehouse") & "') where orderid = " & order.OrderID
    '              Dim sqlCommWarehouse As New SqlCommand(strWarehouse, myConnection1)
    '              myConnection1.Open()
    '              sqlCommWarehouse.ExecuteNonQuery()
    '              myConnection1.Close()

    '          End Using
    '          'Exit While
    '          x = x + 1
    '      End While
    '      r.Close()
    '      myConnection2.Close()
    '      Me.lblpartorder.Text = x & " orders added to C&K database"
    '  End Sub
End Class