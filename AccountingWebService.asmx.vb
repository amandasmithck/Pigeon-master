Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class AccountingWebService
    Inherits System.Web.Services.WebService
    Public Class InvoiceData

        Public Property theOrderID As String
            Get
                Return OrderID
            End Get
            Set(ByVal value As String)
                OrderID = value
            End Set
        End Property
        Private OrderID As String
        Public Property theInvoiceNo As String
            Get
                Return InvoiceNo
            End Get
            Set(ByVal value As String)
                InvoiceNo = value
            End Set
        End Property
        Private InvoiceNo As String
        Public Property theCompany As String
            Get
                Return Company
            End Get
            Set(ByVal value As String)
                Company = value
            End Set
        End Property
        Private Company As String
        Public Property theInvoiceType As String
            Get
                Return InvoiceType
            End Get
            Set(ByVal value As String)
                InvoiceType = value
            End Set
        End Property
        Private InvoiceType As String
        Public Property theAmount As String
            Get
                Return Amount
            End Get
            Set(ByVal value As String)
                Amount = value
            End Set
        End Property
        Private Amount As String
        Public Property theAmountPaid As String
            Get
                Return AmountPaid
            End Get
            Set(ByVal value As String)
                AmountPaid = value
            End Set
        End Property
        Private AmountPaid As String
        Public Property theDatePaid As String
            Get
                Return DatePaid
            End Get
            Set(ByVal value As String)
                DatePaid = value
            End Set
        End Property
        Private DatePaid As String
        Public Property thePaymentType As String
            Get
                Return PaymentType
            End Get
            Set(ByVal value As String)
                PaymentType = value
            End Set
        End Property
        Private PaymentType As String
        Public Property theCheckNo As String
            Get
                Return CheckNo
            End Get
            Set(ByVal value As String)
                CheckNo = value
            End Set
        End Property
        Private CheckNo As String
        Public Property theDateEntered As String
            Get
                Return DateEntered
            End Get
            Set(ByVal value As String)
                DateEntered = value
            End Set
        End Property
        Private DateEntered As String
        Public Property thePayer As String
            Get
                Return Payer
            End Get
            Set(ByVal value As String)
                Payer = value
            End Set
        End Property
        Private Payer As String
        Public Property theVinNo As String
            Get
                Return VinNo
            End Get
            Set(ByVal value As String)
                VinNo = value
            End Set
        End Property
        Private VinNo As String
        Public Property theAutoOwner As String
            Get
                Return AutoOwner
            End Get
            Set(ByVal value As String)
                AutoOwner = value
            End Set
        End Property
        Private AutoOwner As String
        Public Property theServicer As String
            Get
                Return Servicer
            End Get
            Set(ByVal value As String)
                Servicer = value
            End Set
        End Property
        Private Servicer As String

        Public Property theInvoiceID As String
            Get
                Return InvoiceID
            End Get
            Set(ByVal value As String)
                InvoiceID = value
            End Set
        End Property
        Private InvoiceID As String
        Public Property theContractNo As String
            Get
                Return ContractNo
            End Get
            Set(ByVal value As String)
                ContractNo = value
            End Set
        End Property
        Private ContractNo As String
        Public Property theAuthNo As String
            Get
                Return AuthNo
            End Get
            Set(ByVal value As String)
                AuthNo = value
            End Set
        End Property
        Private AuthNo As String


    End Class


    <WebMethod()>
    Public Function GetPayable(ByVal filter As String, ByVal companyid As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of InvoiceData)
        Dim sqlString As String = "SELECT tblInvoices.OrderID, tblInvoices.InvoiceNo, tblCompany.Company, " &
                                   "tblInvoiceType.InvoiceType, tblInvoices.Amount, tblInvoices.AmountPaid, " &
                                   "tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.DateEntered, " &
                                   "tblInvoices.InvoiceID, tblOrder.VinNo, tblOrder.AutoOwner, tblPartOrder.Servicer, tblCompany_1.Company AS Payer " &
                                    "FROM tblInvoices INNER JOIN " &
                                    "tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN " &
                                    "tblCompany tblCompany_1 ON tblInvoices.Payer = tblCompany_1.CompanyID LEFT OUTER JOIN " &
                                    "tblCompany ON tblInvoices.Payee = tblCompany.CompanyID LEFT OUTER JOIN " &
                                    "tblPartOrder ON tblInvoices.PartID = tblPartOrder.PartID LEFT OUTER JOIN " &
                                    "tblOrder ON tblInvoices.OrderID = tblOrder.OrderID " &
                                   "WHERE (tblInvoices.BizExpense = N'0') AND (tblInvoices.DatePaid IS NULL) AND " &
                                   "(tblInvoiceType.Type = N'Payable') AND (tblInvoices.Deleted = 0) "
        If filter = "payee" Then
            sqlString = sqlString & " and tblCompany.ParentCompanyid=" & companyid
        Else
            sqlString = sqlString & " and tblCompany_1.ParentCompanyid=" & companyid
        End If
        sqlString = sqlString & " GROUP BY tblCompany.Company, tblInvoiceType.InvoiceType, tblInvoices.Amount, tblInvoices.DateEntered, " &
                                    "tblInvoices.OrderID, tblInvoices.InvoiceNo, tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, " &
                                    "tblInvoices.CheckNo,  tblInvoices.InvoiceID, tblOrder.VinNo, tblOrder.AutoOwner, tblPartOrder.Servicer, tblCompany_1.Company " &
                                   "ORDER BY tblInvoices.DateEntered"
        Using conn As New SqlConnection(GetClientConnectionString(client))

            Dim sqlComm As New SqlCommand(sqlString, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim Pay1 As New InvoiceData
                    Pay1.theOrderID = r("OrderID").ToString
                    Pay1.theInvoiceNo = r("InvoiceNo").ToString
                    Pay1.theCompany = r("Company").ToString
                    ''Converts C&K to C and k to its sortable 
                    'If (Pay1.theCompany = "C&K") Then
                    '    Pay1.theCompany = "C and K"
                    'End If
                    Pay1.theInvoiceType = r("InvoiceType").ToString
                    Pay1.theAmount = r("Amount").ToString
                    Pay1.theAmountPaid = r("AmountPaid").ToString
                    Pay1.theDatePaid = r("DatePaid").ToString
                    Pay1.thePaymentType = r("PaymentType").ToString
                    Pay1.theCheckNo = r("CheckNo").ToString
                    Pay1.theDateEntered = r("DateEntered").ToString
                    Pay1.theInvoiceID = r("InvoiceID").ToString
                    Pay1.theVinNo = r("VinNo").ToString
                    Pay1.theAutoOwner = r("AutoOwner").ToString
                    Pay1.theServicer = r("Servicer").ToString
                    Pay1.thePayer = r("Payer").ToString
                    ''Converts C&K to C and k to its sortable 
                    'If (Pay1.thePayer = "C&K") Then
                    '    Pay1.thePayer = "C and K"
                    'End If
                    list.Add(Pay1)
                End While
            End Using
        End Using
        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function GetPayableCompanies(ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String

        strSql = "SELECT tblCompany.ParentCompanyID as CompanyID, tblCompany.Company + '-' + tblCompany.Type as Company FROM tblInvoices INNER JOIN " &
                                "tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN " &
                                "tblCompany tblCompany_1 ON tblInvoices.Payer = tblCompany_1.CompanyID LEFT OUTER JOIN " &
                                "tblCompany ON tblInvoices.Payee = tblCompany.CompanyID LEFT OUTER JOIN " &
                                "tblPartOrder ON tblInvoices.PartID = tblPartOrder.PartID LEFT OUTER JOIN " &
                                "tblOrder ON tblInvoices.OrderID = tblOrder.OrderID " &
                               "WHERE (tblInvoices.BizExpense = N'0') AND (tblInvoices.DatePaid IS NULL) AND " &
                               "(tblInvoiceType.Type = N'Payable') AND (tblInvoices.Deleted = 0) and tblcompany.companyid is not null and tblcompany.active=1 " &
                                "UNION " &
                                "SELECT tblCompany_1.ParentCompanyID as CompanyID, tblCompany_1.Company + '-' + tblCompany_1.Type as Company FROM tblInvoices INNER JOIN " &
                                "tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN " &
                                "tblCompany tblCompany_1 ON tblInvoices.Payer = tblCompany_1.CompanyID LEFT OUTER JOIN " &
                                "tblCompany ON tblInvoices.Payee = tblCompany.CompanyID LEFT OUTER JOIN " &
                                "tblPartOrder ON tblInvoices.PartID = tblPartOrder.PartID LEFT OUTER JOIN " &
                                "tblOrder ON tblInvoices.OrderID = tblOrder.OrderID " &
                               "WHERE (tblInvoices.BizExpense = N'0') AND (tblInvoices.DatePaid IS NULL) AND " &
                               "(tblInvoiceType.Type = N'Payable') AND (tblInvoices.Deleted = 0) and tblcompany_1.companyid is not null and tblcompany_1.active=1 " &
                                "GROUP BY tblCompany.Company, tblCompany.ParentCompanyID,tblCompany_1.ParentCompanyID,tblCompany_1.Company + '-' + tblCompany_1.Type order by Company"
        Using conn As New SqlConnection(GetClientConnectionString(client))

            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CompanyID = r("Companyid")
                    Dim tmpList = list.Where(Function(x) x.CompanyID = CInt(r("CompanyID")))
                    If tmpList.Count = 0 Then
                        list.Add(c1)
                    End If
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function GetPrintCKCustomers(ByVal fromdate As String, ByVal todate As String, ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String

        strSql = "SELECT TOP (100) PERCENT dbo.tblCompany.CompanyID, dbo.tblCompany.Company, dbo.tblCompany.Type FROM dbo.tblCompany INNER JOIN dbo.tblInvoices ON dbo.tblCompany.CompanyID = dbo.tblInvoices.Payer Inner JOIN dbo.tblPartOrder ON dbo.tblInvoices.InvoiceNo = dbo.tblPartOrder.InvoiceNo WHERE  (dbo.tblInvoices.InvoiceTypeID = 1 or dbo.tblInvoices.InvoiceTypeID = 5 or dbo.tblInvoices.InvoiceTypeID = 6  ) AND (right(dbo.tblInvoices.InvoiceNo,1) <> 'B') and  (dbo.tblInvoices.DateEntered > '" & fromdate & "') AND (dbo.tblInvoices.DateEntered < DATEADD(d, 1, '" & todate & "')) AND  (dbo.tblInvoices.Deleted = 0) AND (dbo.tblInvoices.DatePaid IS NULL) GROUP BY dbo.tblCompany.CompanyID, dbo.tblCompany.Company, dbo.tblCompany.Type HAVING  (dbo.tblCompany.Type = N'customer') ORDER BY dbo.tblCompany.Company"
        Using conn As New SqlConnection(GetClientConnectionString(client))

            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CompanyID = r("Companyid")
                    list.Add(c1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function UpdatePayable(ByVal Amount As String, ByVal InvoiceNo As String, ByVal AmountPaid As String, ByVal DatePaid As String,
                                  ByVal PaymentType As String, ByVal CheckNo As String, ByVal InvoiceID As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of InvoiceData)
        Dim sqlString1 As String = "UPDATE tblInvoices SET "


        If (Amount <> "") Then
            sqlString1 = (sqlString1 & ("InvoiceNo = '" & InvoiceNo & "',"))
        Else
            sqlString1 = (sqlString1 & ("InvoiceNo = NULL,"))
        End If

        If (Amount <> "") Then
            sqlString1 = (sqlString1 & ("Amount = '" & Amount & "',"))
        Else
            sqlString1 = (sqlString1 & ("Amount = 0,"))
        End If

        If (AmountPaid <> "") Then
            sqlString1 = (sqlString1 & ("AmountPaid = '" & AmountPaid & "', "))
        Else
            sqlString1 = (sqlString1 & ("AmountPaid = 0, "))
        End If

        If (DatePaid <> "") Then
            sqlString1 = (sqlString1 & (" DatePaid = '" & DatePaid & "', "))
        Else
            sqlString1 = (sqlString1 & (" DatePaid = NULL, "))

        End If

        If (PaymentType <> "") Then
            sqlString1 = (sqlString1 & (" PaymentType = '" & PaymentType & "'"))
        End If

        If (CheckNo <> "") Then
            sqlString1 = (sqlString1 & (", CheckNo = '" & CheckNo & "'"))
        End If
        sqlString1 = (sqlString1 & (" WHERE InvoiceID = '" & InvoiceID & "'"))

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm1 As New SqlCommand(sqlString1, conn)
            conn.Open()
            sqlComm1.ExecuteNonQuery()
        End Using
        Return js.Serialize(list)


    End Function
    <WebMethod()>
    Public Function UpdateInvoice(ByVal field As String, ByVal invoiceid As String, ByVal value As String, ByVal paymenttype As String, client As String)
        Dim strSql As String
        Dim boolAmountPaidUpdated As Boolean = False
        Try
            Select Case field
                Case "invoiceno"
                    strSql = "update tblinvoices set invoiceno='" & value & "', paymenttype = '" & paymenttype & "' where invoiceid = '" & invoiceid & "'"
                Case "amountpaid"
                    strSql = "update tblinvoices set amountpaid='" & value & "', paymenttype = '" & paymenttype & "' where invoiceid = '" & invoiceid & "'"
                    If CDec(value) > 0 Then
                        boolAmountPaidUpdated = True
                    End If
                Case "datepaid"
                    If value <> "" Then
                        strSql = "update tblinvoices set datepaid='" & value & "', paymenttype = '" & paymenttype & "' where invoiceid = '" & invoiceid & "'"
                    Else
                        strSql = "update tblinvoices set datepaid=null, paymenttype = '" & paymenttype & "' where invoiceid = '" & invoiceid & "'"
                    End If

                Case "checkno"
                    strSql = "update tblinvoices set checkno='" & value & "', paymenttype = '" & paymenttype & "' where invoiceid = '" & invoiceid & "'"
                Case "paymenttype"
                    strSql = "update tblinvoices set paymenttype='" & value & "' where invoiceid = '" & invoiceid & "'"
            End Select

            Using conn As New SqlConnection(GetClientConnectionString(client))
                conn.Open()
                Dim sqlComm As New SqlCommand(strSql, conn)
                sqlComm.ExecuteNonQuery()
            End Using

            'send core credited email
            If boolAmountPaidUpdated = True Then
                Dim partid
                Dim adjusteremail As String
                Dim orderid As Long
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)
                    Dim sqlComm As New SqlCommand("select partid, adjusteremail, tblorder.orderid from tblinvoices inner join tblorder on tblinvoices.orderid=tblorder.orderid inner join tblcustinvguide on tblcustinvguide.customerno=tblorder.customerno where invoicetypeid=16 and deleted=0 and ckincludecore=1 and invoiceid=" & invoiceid, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            partid = r("partid").ToString()
                            adjusteremail = r("adjusteremail").ToString()
                            orderid = CInt(r("orderid").ToString)
                        End While
                    End Using
                End Using
                If String.IsNullOrEmpty(partid) = False Then
                    AddToAutoEmail("CK", Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString(), 4, adjusteremail, "sales@ckautoparts.com", orderid, IIf(String.IsNullOrEmpty(partid), Nothing, partid),,)
                End If
            End If
        Catch ex As Exception
            Return False
            Exit Function
        End Try
        Return True
    End Function

    <WebMethod()>
    Public Function GetReceivable(ByVal companyid As String, ByVal client As String) As String

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of InvoiceData)
        Dim sqlString As String = "SELECT dbo.tblInvoices.OrderID, tblinvoices.invoicetypeid,dbo.tblInvoices.InvoiceNo, '' AS [View], dbo.tblInvoiceType.InvoiceType AS Type, dbo.tblCompany.Company AS Payer, " &
        "CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END AS Amount, " &
        "dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.DateEntered AS [Date Entered], dbo.tblInvoices.InvoiceID, dbo.tblInvoices.DiscrepancyID, dbo.tblOrder.EnteredBy FROM dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN " &
        "dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID INNER JOIN dbo.tblOrder ON dbo.tblInvoices.OrderID = dbo.tblOrder.OrderID INNER JOIN (SELECT OrderID, SUM(Quantity * SellPrice + CustShippingPrice + CustCoreShippingPrice + WarrantyCost) AS Sellprice FROM dbo.tblPartOrder AS tblPartOrder_1 " &
        "WHERE (Cancelled = 0) AND (PreviousPartID = 0) GROUP BY OrderID) AS tblPartOrder ON dbo.tblOrder.OrderID = tblPartOrder.OrderID WHERE (dbo.tblInvoices.Payee = 192 OR dbo.tblInvoices.Payee = 364) AND (dbo.tblInvoices.BizExpense <> 1) AND (dbo.tblInvoiceType.Type = N'receivable') AND (dbo.tblInvoices.deleted = 0) and tblinvoices.payer = " & companyid & " GROUP BY tblinvoices.invoicetypeid, dbo.tblInvoices.InvoiceNo, dbo.tblInvoiceType.InvoiceType, dbo.tblCompany.Company, dbo.tblInvoices.DateEntered, dbo.tblInvoices.OrderID, dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, " &
        "dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.InvoiceID, dbo.tblInvoices.DiscrepancyID, CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END, dbo.tblOrder.EnteredBy HAVING (dbo.tblInvoices.DatePaid IS NULL) UNION SELECT TOP 100 PERCENT tblinvoices.invoicetypeid,tblInvoices.OrderID, tblInvoices.InvoiceNo AS [Invoice No], '' AS [View], " &
        "tblInvoiceType.InvoiceType AS Type, tblCompany.Company AS Payer, dbo.tblInvoices.Amount AS Amount, '' AS ContractNo, '' , tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.DateEntered AS [Date Entered], tblInvoices.InvoiceID, tblInvoices.DiscrepancyID, '' AS enteredby FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID WHERE (tblInvoices.Payee = 192 OR " &
        "tblInvoices.Payee = 364) AND (tblInvoices.BizExpense <> 1) AND (tblInvoiceType.Type = N'receivable') AND (tblInvoices.NoPartSale = 1) AND (dbo.tblInvoices.deleted = 0) and tblinvoices.payer = " & companyid & " GROUP BY  tblinvoices.invoicetypeid, tblInvoices.InvoiceNo, tblInvoiceType.InvoiceType, tblCompany.Company, tblInvoices.DateEntered, tblInvoices.OrderID, tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.InvoiceID, tblInvoices.DiscrepancyID, tblInvoices.Amount HAVING (tblInvoices.DatePaid IS NULL) ORDER BY [date entered]"

        Using conn As New SqlConnection(GetClientConnectionString(client))

            Dim sqlComm As New SqlCommand(sqlString, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim Rec1 As New InvoiceData
                    Rec1.theOrderID = r("OrderID").ToString
                    Rec1.theInvoiceNo = r("InvoiceNo").ToString
                    'Rec1.theView = "viewinvoice.aspx?invoiceid=" & r("InvoiceID").ToString & "&invoiceno=" & r("InvoiceNo").ToString & "&orderid=" & r("OrderID").ToString & "&invoicetypeid=" & r("invoicetypeid").ToString
                    Rec1.theInvoiceType = r("Type").ToString
                    Rec1.thePayer = r("Payer").ToString
                    Rec1.theAmount = r("Amount").ToString
                    Rec1.theContractNo = r("ContractNo").ToString
                    Rec1.theDatePaid = r("DatePaid").ToString
                    Rec1.thePaymentType = r("PaymentType").ToString
                    Rec1.theCheckNo = r("CheckNo").ToString
                    Rec1.theAuthNo = r("AuthorizationNo").ToString
                    Rec1.theAmountPaid = r("AmountPaid").ToString
                    Rec1.theInvoiceID = r("InvoiceID").ToString
                    list.Add(Rec1)
                End While
            End Using
        End Using
        Return js.Serialize(list)

    End Function

    <WebMethod()>
    Public Function GetReceivableCompanies(ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String

        strSql = "SELECT  dbo.tblCompany.Company, dbo.tblCompany.CompanyID " &
         "FROM dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN " &
        "dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID INNER JOIN dbo.tblOrder ON dbo.tblInvoices.OrderID = dbo.tblOrder.OrderID INNER JOIN (SELECT OrderID, SUM(Quantity * SellPrice + CustShippingPrice + CustCoreShippingPrice) AS Sellprice FROM dbo.tblPartOrder AS tblPartOrder_1 " &
        "WHERE (Cancelled = 0) AND (PreviousPartID = 0) GROUP BY OrderID) AS tblPartOrder ON dbo.tblOrder.OrderID = tblPartOrder.OrderID WHERE (dbo.tblInvoices.Payee = 192 OR dbo.tblInvoices.Payee = 364) AND (dbo.tblInvoices.BizExpense <> 1) AND (dbo.tblInvoiceType.Type = N'receivable') AND (dbo.tblInvoices.deleted = 0)  and (dbo.tblInvoices.DatePaid IS NULL) GROUP BY dbo.tblCompany.Company, dbo.tblCompany.CompanyID UNION SELECT  tblCompany.Company, tblCompany.CompanyID FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID WHERE  (tblInvoices.Payee = 192 OR " &
        "tblInvoices.Payee = 364) AND (tblInvoices.BizExpense <> 1) AND (tblInvoiceType.Type = N'receivable') AND (tblInvoices.NoPartSale = 1) AND (dbo.tblInvoices.deleted = 0)  and (tblInvoices.DatePaid IS NULL) GROUP BY  Company,CompanyID  ORDER BY company"
        Using conn As New SqlConnection(GetClientConnectionString(client))

            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CompanyID = r("Companyid")
                    list.Add(c1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

    <WebMethod()>
    Public Function GetChecksBalances(ByVal type As String, ByVal client As String) As String

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of InvoiceData)
        Dim sqlString As String
        Select Case type
            Case "1" 'All receivable invoice types Closed at any date with AmountPaid = 0 and Amount > 0
                sqlString = "SELECT dbo.tblInvoices.OrderID, tblinvoices.invoicetypeid,dbo.tblInvoices.InvoiceNo, '' AS [View], dbo.tblInvoiceType.InvoiceType AS Type, dbo.tblCompany.Company AS Payer, " &
"CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END AS Amount, " &
"dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.DateEntered, dbo.tblInvoices.InvoiceID, dbo.tblInvoices.DiscrepancyID, dbo.tblOrder.EnteredBy FROM dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN " &
"dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID INNER JOIN dbo.tblOrder ON dbo.tblInvoices.OrderID = dbo.tblOrder.OrderID INNER JOIN (SELECT OrderID, SUM(Quantity * SellPrice + CustShippingPrice + CustCoreShippingPrice) AS Sellprice FROM dbo.tblPartOrder AS tblPartOrder_1 " &
"WHERE (Cancelled = 0) AND (PreviousPartID = 0) GROUP BY OrderID) AS tblPartOrder ON dbo.tblOrder.OrderID = tblPartOrder.OrderID WHERE (dbo.tblInvoices.Payee = 192 OR dbo.tblInvoices.Payee = 364) AND (dbo.tblInvoices.BizExpense <> 1) AND (dbo.tblInvoiceType.Type = N'receivable') AND (dbo.tblInvoices.deleted = 0) and amountpaid=0 and CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END > 0 and year(dateentered) >=2013 GROUP BY tblinvoices.invoicetypeid, dbo.tblInvoices.InvoiceNo, dbo.tblInvoiceType.InvoiceType, dbo.tblCompany.Company, dbo.tblInvoices.DateEntered, dbo.tblInvoices.OrderID, dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, " &
"dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.InvoiceID, dbo.tblInvoices.DiscrepancyID, CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END, dbo.tblOrder.EnteredBy HAVING (dbo.tblInvoices.DatePaid IS not NULL) UNION SELECT TOP 100 PERCENT tblinvoices.invoicetypeid,tblInvoices.OrderID, tblInvoices.InvoiceNo AS [Invoice No], '' AS [View], " &
"tblInvoiceType.InvoiceType AS Type, tblCompany.Company AS Payer, dbo.tblInvoices.Amount AS Amount, '' AS ContractNo, '' , tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.DateEntered, tblInvoices.InvoiceID, tblInvoices.DiscrepancyID, '' AS enteredby FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID WHERE (tblInvoices.Payee = 192 OR " &
"tblInvoices.Payee = 364) AND (tblInvoices.BizExpense <> 1) AND (tblInvoiceType.Type = N'receivable') AND (tblInvoices.NoPartSale = 1) AND (dbo.tblInvoices.deleted = 0) and amountpaid=0 and amount > 0 and year(dateentered) >=2013 GROUP BY  tblinvoices.invoicetypeid, tblInvoices.InvoiceNo, tblInvoiceType.InvoiceType, tblCompany.Company, tblInvoices.DateEntered, tblInvoices.OrderID, tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.InvoiceID, tblInvoices.DiscrepancyID, tblInvoices.Amount HAVING (tblInvoices.DatePaid IS not NULL) ORDER BY dateentered"

            Case "2" 'All receivable invoice types  DatePaid before DateEntered
                sqlString = "SELECT dbo.tblInvoices.OrderID, tblinvoices.invoicetypeid,dbo.tblInvoices.InvoiceNo, '' AS [View], dbo.tblInvoiceType.InvoiceType AS Type, dbo.tblCompany.Company AS Payer, " &
"CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END AS Amount, " &
"dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.DateEntered, dbo.tblInvoices.InvoiceID, dbo.tblInvoices.DiscrepancyID, dbo.tblOrder.EnteredBy FROM dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN " &
"dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID INNER JOIN dbo.tblOrder ON dbo.tblInvoices.OrderID = dbo.tblOrder.OrderID INNER JOIN (SELECT OrderID, SUM(Quantity * SellPrice + CustShippingPrice + CustCoreShippingPrice) AS Sellprice FROM dbo.tblPartOrder AS tblPartOrder_1 " &
"WHERE (Cancelled = 0) AND (PreviousPartID = 0) GROUP BY OrderID) AS tblPartOrder ON dbo.tblOrder.OrderID = tblPartOrder.OrderID WHERE (dbo.tblInvoices.Payee = 192 OR dbo.tblInvoices.Payee = 364) AND (dbo.tblInvoices.BizExpense <> 1) AND (dbo.tblInvoiceType.Type = N'receivable') AND (dbo.tblInvoices.deleted = 0) and dateadd(d,1,datepaid) < dateentered and year(dateentered) >= 2013 GROUP BY tblinvoices.invoicetypeid, dbo.tblInvoices.InvoiceNo, dbo.tblInvoiceType.InvoiceType, dbo.tblCompany.Company, dbo.tblInvoices.DateEntered, dbo.tblInvoices.OrderID, dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, " &
"dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.InvoiceID, dbo.tblInvoices.DiscrepancyID, CASE WHEN RIGHT(tblinvoices.invoiceno, 1) = 'B' THEN tblinvoices.amount ELSE CASE WHEN tblinvoices.invoicetypeid <> 1 THEN tblinvoices.amount ELSE CASE WHEN payer = 144 THEN .05 * sellprice + sellprice ELSE SellPrice END END END, dbo.tblOrder.EnteredBy HAVING (dbo.tblInvoices.DatePaid IS not NULL) UNION SELECT TOP 100 PERCENT tblinvoices.invoicetypeid,tblInvoices.OrderID, tblInvoices.InvoiceNo AS [Invoice No], '' AS [View], " &
"tblInvoiceType.InvoiceType AS Type, tblCompany.Company AS Payer, dbo.tblInvoices.Amount AS Amount, '' AS ContractNo, '' , tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.DateEntered, tblInvoices.InvoiceID, tblInvoices.DiscrepancyID, '' AS enteredby FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID WHERE (tblInvoices.Payee = 192 OR " &
"tblInvoices.Payee = 364) AND (tblInvoices.BizExpense <> 1) AND (tblInvoiceType.Type = N'receivable') AND (tblInvoices.NoPartSale = 1) AND (dbo.tblInvoices.deleted = 0) and dateadd(d,1,datepaid) < dateentered  and year(dateentered) >= 2013 GROUP BY  tblinvoices.invoicetypeid, tblInvoices.InvoiceNo, tblInvoiceType.InvoiceType, tblCompany.Company, tblInvoices.DateEntered, tblInvoices.OrderID, tblInvoices.AmountPaid, tblInvoices.DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblInvoices.InvoiceID, tblInvoices.DiscrepancyID, tblInvoices.Amount HAVING (tblInvoices.DatePaid IS not NULL) ORDER BY dateentered"
        End Select

        Using conn As New SqlConnection(GetClientConnectionString(client))

            Dim sqlComm As New SqlCommand(sqlString, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim Rec1 As New InvoiceData
                    Rec1.theOrderID = r("OrderID").ToString
                    Rec1.theInvoiceNo = r("InvoiceNo").ToString
                    'Rec1.theView = "viewinvoice.aspx?invoiceid=" & r("InvoiceID").ToString & "&invoiceno=" & r("InvoiceNo").ToString & "&orderid=" & r("OrderID").ToString & "&invoicetypeid=" & r("invoicetypeid").ToString
                    Rec1.theInvoiceType = r("Type").ToString
                    Rec1.thePayer = r("Payer").ToString
                    Rec1.theAmount = r("Amount").ToString
                    Rec1.theContractNo = r("ContractNo").ToString
                    Rec1.theDatePaid = FormatDateTime(r("DatePaid").ToString, DateFormat.ShortDate)
                    Rec1.thePaymentType = r("PaymentType").ToString
                    Rec1.theCheckNo = r("CheckNo").ToString
                    Rec1.theAuthNo = r("AuthorizationNo").ToString
                    Rec1.theAmountPaid = r("AmountPaid").ToString
                    Rec1.theInvoiceID = r("InvoiceID").ToString
                    Rec1.theDateEntered = FormatDateTime(r("DateEntered").ToString, DateFormat.ShortDate)
                    list.Add(Rec1)
                End While
            End Using
        End Using
        Return js.Serialize(list)

    End Function
End Class