Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Xml
Imports Pigeon.Pigeon
Imports Pigeon.OrderWebService
Imports System.Net
Imports System.IO
Imports System.Globalization
Imports OEMSmallPartPricing.AutoNation
Imports OEMSmallPartPricing.Pricing

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class OEMWebService
    Inherits System.Web.Services.WebService
    Public ClientHyps As New ArrayList

    Public Class Quote
        Public Property QuoteID() As Long
            Get
                Return m_QuoteID
            End Get
            Set(ByVal value As Long)
                m_QuoteID = value
            End Set
        End Property
        Private m_QuoteID As Long
        Public Property QuoteDate() As String
            Get
                Return m_QuoteDate
            End Get
            Set(ByVal value As String)
                m_QuoteDate = value
            End Set
        End Property
        Private m_QuoteDate As String
        Public Property User() As String
            Get
                Return m_User
            End Get
            Set(ByVal value As String)
                m_User = value
            End Set
        End Property
        Private m_User As String
        Public Property Customer() As String
            Get
                Return m_Customer
            End Get
            Set(ByVal value As String)
                m_Customer = value
            End Set
        End Property
        Private m_Customer As String
        Public Property Make() As String
            Get
                Return m_Make
            End Get
            Set(ByVal value As String)
                m_Make = value
            End Set
        End Property
        Private m_Make As String
        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property Desc() As String
            Get
                Return m_Desc
            End Get
            Set(ByVal value As String)
                m_Desc = value
            End Set
        End Property
        Private m_Desc As String
        Public Property Quantity() As String
            Get
                Return m_Quantity
            End Get
            Set(ByVal value As String)
                m_Quantity = value
            End Set
        End Property
        Private m_Quantity As String
        Public Property List() As Decimal
            Get
                Return m_List
            End Get
            Set(ByVal value As Decimal)
                m_List = value
            End Set
        End Property
        Private m_List As Decimal
        Public Property Cost() As Decimal
            Get
                Return m_Cost
            End Get
            Set(ByVal value As Decimal)
                m_Cost = value
            End Set
        End Property
        Private m_Cost As Decimal
        Public Property SellPrice() As Decimal
            Get
                Return m_SellPrice
            End Get
            Set(ByVal value As Decimal)
                m_SellPrice = value
            End Set
        End Property
        Private m_SellPrice As Decimal
        Public Property Core() As Decimal
            Get
                Return m_Core
            End Get
            Set(ByVal value As Decimal)
                m_Core = value
            End Set
        End Property
        Private m_Core As Decimal
        Public Property InStock() As String
            Get
                Return m_InStock
            End Get
            Set(ByVal value As String)
                m_InStock = value
            End Set
        End Property
        Private m_InStock As String
    End Class
    Public Class Order
        Public Property OrderID() As Long
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As Long)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As Long
        Public Property OrderDate() As String
            Get
                Return m_OrderDate
            End Get
            Set(ByVal value As String)
                m_OrderDate = value
            End Set
        End Property
        Private m_OrderDate As String
        Public Property User() As String
            Get
                Return m_User
            End Get
            Set(ByVal value As String)
                m_User = value
            End Set
        End Property
        Private m_User As String
        Public Property Customer() As String
            Get
                Return m_Customer
            End Get
            Set(ByVal value As String)
                m_Customer = value
            End Set
        End Property
        Private m_Customer As String
        Public Property Make() As String
            Get
                Return m_Make
            End Get
            Set(ByVal value As String)
                m_Make = value
            End Set
        End Property
        Private m_Make As String
        Public Property TotalParts() As Long
            Get
                Return m_TotalParts
            End Get
            Set(ByVal value As Long)
                m_TotalParts = value
            End Set
        End Property
        Private m_TotalParts As Long

        Public Property TotalSale() As Decimal
            Get
                Return m_TotalSale
            End Get
            Set(ByVal value As Decimal)
                m_TotalSale = value
            End Set
        End Property
        Private m_TotalSale As Decimal
        Public Property TotalCost() As Decimal
            Get
                Return m_TotalCost
            End Get
            Set(ByVal value As Decimal)
                m_TotalCost = value
            End Set
        End Property
        Private m_TotalCost As Decimal
        Public Property Profit() As Decimal
            Get
                Return m_Profit
            End Get
            Set(ByVal value As Decimal)
                m_Profit = value
            End Set
        End Property
        Private m_Profit As Decimal
        Public Property GPProfit() As Decimal
            Get
                Return m_GPProfit
            End Get
            Set(ByVal value As Decimal)
                m_GPProfit = value
            End Set
        End Property
        Private m_GPProfit As Decimal
        Public Property OrderType() As String
            Get
                Return m_OrderType
            End Get
            Set(ByVal value As String)
                m_OrderType = value
            End Set
        End Property
        Private m_OrderType As String
        Public Property Notes() As String
            Get
                Return m_Notes
            End Get
            Set(ByVal value As String)
                m_Notes = value
            End Set
        End Property
        Private m_Notes As String

    End Class
    Public Class OrderDetail

        Public Property PartID() As Long
            Get
                Return m_PartID
            End Get
            Set(ByVal value As Long)
                m_PartID = value
            End Set
        End Property
        Private m_PartID As Long
        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property Quantity() As Long
            Get
                Return m_Quantity
            End Get
            Set(ByVal value As Long)
                m_Quantity = value
            End Set
        End Property
        Private m_Quantity As Long
        Public Property ListPrice() As Decimal
            Get
                Return m_ListPrice
            End Get
            Set(ByVal value As Decimal)
                m_ListPrice = value
            End Set
        End Property
        Private m_ListPrice As Decimal
        Public Property CorePrice() As Decimal
            Get
                Return m_CorePrice
            End Get
            Set(ByVal value As Decimal)
                m_CorePrice = value
            End Set
        End Property
        Private m_CorePrice As Decimal
        Public Property OurCost() As Decimal
            Get
                Return m_OurCost
            End Get
            Set(ByVal value As Decimal)
                m_OurCost = value
            End Set
        End Property
        Private m_OurCost As Decimal
        Public Property TheirPrice() As Decimal
            Get
                Return m_TheirPrice
            End Get
            Set(ByVal value As Decimal)
                m_TheirPrice = value
            End Set
        End Property
        Private m_TheirPrice As Decimal
        Public Property InStock() As String
            Get
                Return m_InStock
            End Get
            Set(ByVal value As String)
                m_InStock = value
            End Set
        End Property
        Private m_InStock As String
    End Class

    Public Class CustShippingPrices
        Public Property OEMShipping() As Decimal
            Get
                Return m_OEMShipping
            End Get
            Set(ByVal value As Decimal)
                m_OEMShipping = value
            End Set
        End Property
        Private m_OEMShipping As Decimal

        Public Property SmallPartsShipping() As Decimal
            Get
                Return m_SmallPartsShipping
            End Get
            Set(ByVal value As Decimal)
                m_SmallPartsShipping = value
            End Set
        End Property
        Private m_SmallPartsShipping As Decimal
        Public Property GroundOEMShipping() As Decimal
            Get
                Return m_GroundOEMShipping
            End Get
            Set(ByVal value As Decimal)
                m_GroundOEMShipping = value
            End Set
        End Property
        Private m_GroundOEMShipping As Decimal

        Public Property GroundSmallPartsShipping() As Decimal
            Get
                Return m_GroundSmallPartsShipping
            End Get
            Set(ByVal value As Decimal)
                m_GroundSmallPartsShipping = value
            End Set
        End Property
        Private m_GroundSmallPartsShipping As Decimal
    End Class

    Public Class MagnetiDealer
        Public Property CompanyID() As Long
            Get
                Return m_CompanyID
            End Get
            Set(ByVal value As Long)
                m_CompanyID = value
            End Set
        End Property
        Private m_CompanyID As Long

        Public Property Latitude() As String
            Get
                Return m_Latitude
            End Get
            Set(ByVal value As String)
                m_Latitude = value
            End Set
        End Property
        Private m_Latitude As String
        Public Property Longitude() As String
            Get
                Return m_Longitude
            End Get
            Set(ByVal value As String)
                m_Longitude = value
            End Set
        End Property
        Private m_Longitude As String
    End Class
    Public Class CartParts
        Public Property PartNumber() As String
            Get
                Return m_PartNumber
            End Get
            Set(ByVal value As String)
                m_PartNumber = value
            End Set
        End Property
        Private m_PartNumber As String
        Public Property Quantity() As Long
            Get
                Return m_Quantity
            End Get
            Set(ByVal value As Long)
                m_Quantity = value
            End Set
        End Property
        Private m_Quantity As Long
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String

    End Class
    Public Class PigeonPartsResponse
        Public Property Row() As Long
            Get
                Return m_Row
            End Get
            Set(ByVal value As Long)
                m_Row = value
            End Set
        End Property
        Private m_Row As Long
        Public Property List() As Decimal
            Get
                Return m_List
            End Get
            Set(ByVal value As Decimal)
                m_List = value
            End Set
        End Property
        Private m_List As Decimal
        Public Property Your() As Decimal
            Get
                Return m_Your
            End Get
            Set(ByVal value As Decimal)
                m_Your = value
            End Set
        End Property
        Private m_Your As Decimal
        Public Property Core() As Decimal
            Get
                Return m_Core
            End Get
            Set(ByVal value As Decimal)
                m_Core = value
            End Set
        End Property
        Private m_Core As Decimal
        Public Property Stock() As String
            Get
                Return m_Stock
            End Get
            Set(ByVal value As String)
                m_Stock = value
            End Set
        End Property
        Private m_Stock As String
        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property Brand() As String
            Get
                Return m_Brand
            End Get
            Set(ByVal value As String)
                m_Brand = value
            End Set
        End Property
        Private m_Brand As String
        Public Property Provider() As String
            Get
                Return m_Provider
            End Get
            Set(ByVal value As String)
                m_Provider = value
            End Set
        End Property
        Private m_Provider As String
        Public Property SupersededPart() As String
            Get
                Return m_SupersededPart
            End Get
            Set(ByVal value As String)
                m_SupersededPart = value
            End Set
        End Property
        Private m_SupersededPart As String
        Public Property OEMPartNumber() As String
            Get
                Return m_OEMPartNumber
            End Get
            Set(ByVal value As String)
                m_OEMPartNumber = value
            End Set
        End Property
        Private m_OEMPartNumber As String
        Public Property PartNumber() As String
            Get
                Return m_PartNumber
            End Get
            Set(ByVal value As String)
                m_PartNumber = value
            End Set
        End Property
        Private m_PartNumber As String
        Public Property Quantity() As String
            Get
                Return m_Quantity
            End Get
            Set(ByVal value As String)
                m_Quantity = value
            End Set
        End Property
        Private m_Quantity As String
        Public Property Our() As Decimal
            Get
                Return m_Our
            End Get
            Set(ByVal value As Decimal)
                m_Our = value
            End Set
        End Property
        Private m_Our As Decimal
        Public Property StockLevels() As List(Of StockResponse)
            Get
                Return m_StockLevels
            End Get
            Set(ByVal value As List(Of StockResponse))
                m_StockLevels = value
            End Set
        End Property
        Private m_StockLevels As List(Of StockResponse)
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property QuoteID() As Long
            Get
                Return m_QuoteID
            End Get
            Set(ByVal value As Long)
                m_QuoteID = value
            End Set
        End Property
        Private m_QuoteID As Long

    End Class

    Protected Function GetCustCost(ByVal CustNo As String, ByVal MakeID As Long, ByVal dealer As String, ByVal list As String, ByVal client As String) As String

        Dim strOEM As Long
        Dim strmarkup As Decimal
        Dim strflat As Decimal
        Dim strsource As String = String.Empty
        Dim intExceptionCount

        'get oemid first
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            conn.Open()

            Dim command As New SqlCommand("SELECT oemid from tblmake WHERE makeid = " & MakeID, conn)
            strOEM = command.ExecuteScalar()

            'here we check for a price exemption rule starting with the most specific first
            'right now (2/8/2012) the most specific we get is make/oemid
            command = New SqlCommand("SELECT count(oemid) from tblPriceException WHERE CustomerNo = '" & CustNo & "' AND OEMID = '" & strOEM & "'", conn)
            intExceptionCount = command.ExecuteScalar()


        End Using

        'if a (vehicle make) rule exists use it
        'in the future there will need to be an "Exception check" starting with the most specific (part#) and moving on up to leat specific (make)
        If (intExceptionCount > 0) Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                conn.Open()

                Dim command As New SqlCommand("SELECT BasePrice from tblPriceException WHERE oemid = " & strOEM & " and customerno = '" & CustNo & "'", conn)
                strsource = command.ExecuteScalar()

                command = New SqlCommand("SELECT case when Percentage is null then 0 else percentage end as Percentage from tblPriceException WHERE oemid = " & strOEM & " and customerno = '" & CustNo & "'", conn)
                strmarkup = command.ExecuteScalar()

                command = New SqlCommand("SELECT case when flat is null then 0 else Flat end as Flat from tblPriceException WHERE oemid = " & strOEM & " and customerno = '" & CustNo & "'", conn)
                strflat = command.ExecuteScalar()
            End Using

        Else 'if no rules exists use the base info

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                conn.Open()

                Dim command As New SqlCommand("SELECT BasePrice from tblTierBasePrice INNER JOIN tblCompany on tblCompany.TierID = tblTierBasePrice.TierID WHERE customerno = '" & CustNo & "' AND tblTierBasePrice.PartType = '5'", conn)
                strsource = command.ExecuteScalar()

                command = New SqlCommand("SELECT case when Percentage is null then 0 else percentage end as Percentage from tblTierBasePrice INNER JOIN tblCompany on tblCompany.TierID = tblTierBasePrice.TierID WHERE customerno = '" & CustNo & "' AND tblTierBasePrice.PartType = '5'", conn)
                strmarkup = command.ExecuteScalar()

                command = New SqlCommand("SELECT case when flat is null then 0 else Flat end as Flat from tblTierBasePrice INNER JOIN tblCompany on tblCompany.TierID = tblTierBasePrice.TierID WHERE customerno = '" & CustNo & "' AND tblTierBasePrice.PartType = '5'", conn)
                strflat = command.ExecuteScalar()

            End Using

        End If

        If strsource = "List" Then
            Return (list - list * Math.Abs(strmarkup)) + strflat
        ElseIf strsource = "Dealer" Then
            Return (dealer + dealer * Math.Abs(strmarkup)) + strflat
        End If

        Return False
    End Function
    Protected Function GetOEMID(ByVal makeid As Long, ByVal client As String) As String
        Dim strOEM As Long
        'get oemid first
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm1 As New SqlCommand("SELECT oemid from tblmake WHERE makeid = " & makeid, conn)
            conn.Open()
            strOEM = sqlComm1.ExecuteScalar()
        End Using
        Return strOEM
    End Function

    Private Function FindSource(ByVal lr As List(Of ListingResult), ByVal value As Decimal) As String
        For Each listingresult In lr
            If listingresult.Value = value And listingresult.Source = "Autonation" Then Return "Autonation"
        Next

        For Each listingresult2 In lr
            If listingresult2.Value = value And listingresult2.Source = "Trademotion" Then Return "Trademotion"
        Next

        For Each listingresult3 In lr
            If listingresult3.Value = value And listingresult3.Source = "Browns" Then Return "Browns"
        Next

        Return String.Empty

    End Function

    Private Function FindNoPartsfound(ByVal pt As PartsResponse) As Boolean

        Return pt.Description = "No Parts Found"

    End Function

    Private Function FindLocation(ByVal pa As List(Of PartsAvailability), ByVal client As String) As Boolean

        If client = "AutoNation" Then 'search all autonation stores
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT hyperion from tblAutonation", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        ClientHyps.Add(r("Hyperion"))
                    End While
                End Using
            End Using
        Else 'get list of hyperions for this client
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT hyperion from tblPigeonClientsHypSearch where client = '" & client & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        ClientHyps.Add(r("Hyperion"))
                    End While
                End Using
            End Using
        End If


        'see if any results are in any of these hyperions
        For Each listingresult In pa
            If ClientHyps.Contains(listingresult.Hyperion) Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    Private Function CheckMortorcraftFordCross(ByVal part As String) As String
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select * from tblMotorcraftFordCross where motorcraft = '" & part & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If r("Ford").ToString() <> "" Then
                        Return r("Ford")
                        Exit Function

                    Else
                        Return part
                        Exit Function
                    End If
                End While
                Return part
                Exit Function
            End Using
        End Using
    End Function

    <WebMethod()>
    Public Function GetCustShippingPrices(ByVal custno As String)
        Dim p As New CustShippingPrices
        Dim js As New JavaScriptSerializer
        p = GetCustomerShippingPrices(custno)

        Return js.Serialize(p)
    End Function
    Public Shared Function GetCustomerShippingPrices(ByVal custno As String) As CustShippingPrices
        Dim p As New CustShippingPrices
        Dim js As New JavaScriptSerializer
        p.OEMShipping = GetCustShipping(custno, "OEM")
        p.SmallPartsShipping = GetCustShipping(custno, "SmallParts")
        p.GroundOEMShipping = GetCustShipping(custno, "GroundOEM")
        p.GroundSmallPartsShipping = GetCustShipping(custno, "GroundSmallParts")

        Return p
    End Function

    <WebMethod()>
    Public Function GetOEMPrice(ByVal Emulate As String, ByVal Quote As String, ByVal MakeID As String, ByVal Name As String, ByVal Email As String, ByVal Part As OEMSmallPartPricing.Pricing.Parts(), ByVal client As String)
        Dim strcustno As String = String.Empty
        Dim strmake As String = String.Empty
        Dim strdesc As String = String.Empty
        Dim strsuper As String = String.Empty
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of PartsResponse)
        Dim listOverall As New List(Of OverallSearch)
        Dim strcore, strdisplaycore, strlist, stryour, strour, strdealer As Decimal
        Dim strpartcount As Long
        Dim stronhand As Decimal
        Dim Hyperions, HyperionsMultiple As New NameValueCollection
        Dim result, resulteverywhere As Array
        Dim notfound As Boolean = False
        Dim strstock As String = String.Empty
        Dim strcomplicated As String = String.Empty
        Dim strcutoff, strCutoffMinutes As String
        Dim o1 As New OverallSearch
        Dim strsearchmakeid As String = String.Empty
        Dim stroverallstock As String = String.Empty
        Dim strforceship As String = String.Empty
        Dim strnocheckout As String = "no"
        Dim strlistchecktotal As Decimal
        Dim loopedtime As Integer = 0
        Dim strlistsource, strdealersource As String
        Dim strcoresource As String = "No Core"

        Dim pricingLog As New OEMSmallPartPricing.Pricing
        For Each item In Part
            pricingLog.InsertErrorLog(1.11, item.QuoteID)
        Next

        If GetCustomerNo(Name, client) = "1040" Or GetCustomerNo(Name, client) = "1041" Then Exit Function
        For Each item In Part
            pricingLog.InsertErrorLog(1.12, item.QuoteID)
        Next
        'only continue if this is an autonation location which has own inventory
        Dim strautonationlocation As Boolean
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlCommAutonation As New SqlCommand("SELECT autonationlocation from tblPigeonClients where client = '" & client & "'", conn)
            conn.Open()
            strautonationlocation = CBool(sqlCommAutonation.ExecuteScalar())
        End Using
        For Each item In Part
            pricingLog.InsertErrorLog(1.13, item.QuoteID)
        Next
        If client = "CK" Then
            Dim Parts As New List(Of OEMSmallPartPricing.Pricing.Parts)
            For Each item In Part
                pricingLog.InsertErrorLog(1.14, item.QuoteID)
                Dim p1 As New OEMSmallPartPricing.Pricing.Parts()
                p1.PartNumber = item.PartNumber
                p1.Quantity = item.Quantity
                p1.QuoteID = item.QuoteID
                Parts.Add(p1)
            Next

            Dim oemPricing As New OEMSmallPartPricing.Pricing
            Dim portalresponse As New OEMSearchResult
            For Each item In Part
                pricingLog.InsertErrorLog(1.15, item.QuoteID)
            Next
            portalresponse = oemPricing.GetOEMPrice(Emulate, Quote, MakeID, Name, Email, Parts.ToArray)
            Dim finalResponse As New ArrayList
            finalResponse.Add(portalresponse.list)
            finalResponse.Add(portalresponse.listOverall)
            Return js.Serialize(finalResponse)
            Exit Function
        End If



        'first get customer number
        strcustno = Emulate
        If Emulate = "false" Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo FROM dbo.aspnet_Membership INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId WHERE dbo.aspnet_Users.UserName = '" & Name & "'", conn)
                conn.Open()
                strcustno = sqlComm.ExecuteScalar()
            End Using
        End If


        'crossmake
        Select Case MakeID
            Case 12 'gm
                strsearchmakeid = "6"
            Case 5 'cadillac
                strsearchmakeid = "6"
            Case 4 'buick
                strsearchmakeid = "6"
            Case 28 'olds
                strsearchmakeid = "6"
            Case 43 'hummer
                strsearchmakeid = "6"
            Case 34 'saturn
                strsearchmakeid = "6"
            Case 30 'pontiac
                strsearchmakeid = "6"
            Case 22 'lincoln
                strsearchmakeid = "11"
            Case 25 'mercury
                strsearchmakeid = "11"
            Case 15 'infiniti
                strsearchmakeid = "27"
            Case Else
                strsearchmakeid = MakeID
        End Select



        'get make name
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm3 As New SqlCommand("SELECT make from tblmake WHERE makeid = '" & strsearchmakeid & "'", conn)
            conn.Open()
            strmake = sqlComm3.ExecuteScalar()
        End Using



        'get part data

        Dim x As Integer = 1
        stroverallstock = "yes"

        For Each thepart As OEMSmallPartPricing.Pricing.Parts In Part
            Dim p1 As New PartsResponse()
            Dim HyperionsAdded As New NameValueCollection
            Dim HyperionsInd As New NameValueCollection
            Dim boolCheckMotorcraftCross As Boolean = False
            Dim strPartUserSearched As String = thepart.PartNumber
            HyperionsAdded.Clear()
            strlistchecktotal = 0


            If client = "Fitz" Then

                'get stock level
                Dim intListingOnHand As Integer = 0
                Dim decList, decCost, decCore As Decimal

                strdesc = "No Parts Found"

                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm6 As New SqlCommand("select qoh,list,cost,core,description from tblFitzStock where replace(right(part#,len(part#)-2),'-','') = '" & thepart.PartNumber.Replace("-", "") & "'", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm6.ExecuteReader
                        While r.Read()
                            intListingOnHand = r("qoh")
                            decList = CDec(r("list").ToString)
                            decCore = CDec(r("core").ToString)
                            decCost = CDec(r("cost").ToString)
                            strdesc = r("description").ToString
                        End While
                    End Using
                End Using

                If intListingOnHand > 0 And intListingOnHand >= Val(thepart.Quantity) Then
                    strstock = "Yes"
                    stroverallstock = "yes"
                    strnocheckout = "no"

                Else
                    strstock = "No"
                    stroverallstock = "no"
                    strnocheckout = "yes"
                End If




                p1.Core = FormatNumber(decCore, 2)

                p1.Description = If(IsNothing(strdesc), "", strdesc)
                p1.List = FormatNumber(decList, 2)
                p1.Row = x
                If p1.Description = "No Parts Found" Or p1.Description = "Not Stocked at " & client Then
                    p1.Stock = ""
                Else
                    p1.Stock = strstock
                End If

                p1.PartNumber = thepart.PartNumber
                p1.Our = FormatNumber(decCost, 2)
                p1.Quantity = thepart.Quantity
                p1.Your = FormatNumber(GetCustCost(strcustno, MakeID, decCost, decList, client), 2)

                strlistsource = "Fitz"
                strdealersource = "Fitz"
                strcoresource = "Fitz"

                'ins(quote)
                If Quote = "true" Then
                    Dim strsqlins As String
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        strsqlins = "insert into tbloemquotes(quotedate, username, customerno, make, part, description, quantity, listprice, coreprice, ourcost, theirprice, discontinued, InStock, listsource, dealersource, coresource) values ('" & Now() & "','" & Name & "','" & strcustno & "','" & GetOEMID(MakeID, client) & "','" & thepart.PartNumber & "','" & p1.Description & "','" & thepart.Quantity & "'," & p1.List & "," & p1.Core & "," & p1.Our & "," & p1.Your & ",0,'" & strstock & "','" & strlistsource & "','" & strdealersource & "','" & strcoresource & "')"
                        Dim sqlComm7 As New SqlCommand(strsqlins, conn)
                        conn.Open()
                        sqlComm7.ExecuteNonQuery()
                    End Using
                End If

                js.Serialize(p1)
                list.Add(p1)
                x = x + 1


            Else

MotorcraftCrossReRun:
                Try
                    Dim clientSearch As PartsLookUpServiceClient = New PartsLookUpServiceClient()
                    'result = client.GetPartsAvailabilityByPartNumber("CandK", "dM7l@Sn0", Nothing, thepart.PartNumber)
                    result = clientSearch.GetPartsAvailabilityByAlphanumericPartNumber("CandK", "dM7l@Sn0", Nothing, thepart.PartNumber)
                    clientSearch.Close()
                Catch Ex As WebException


                End Try
                GoTo skipover
rerunwithmake:
                loopedtime = loopedtime + 1
                Try
                    Dim clientSearch As PartsLookUpServiceClient = New PartsLookUpServiceClient()
                    'result = client.GetPartsAvailabilityByPartNumber("CandK", "dM7l@Sn0", strmake, thepart.PartNumber)
                    result = clientSearch.GetPartsAvailabilityByAlphanumericPartNumber("CandK", "dM7l@Sn0", strmake, thepart.PartNumber)
                    clientSearch.Close()
                Catch Ex As WebException


                End Try


skipover:
                stronhand = 0

                Dim listingcheck As New List(Of PartsAvailability)
                For Each hypcheck As PartsAvailability In result
                    Dim h1 As New PartsAvailability
                    h1.Hyperion = hypcheck.Hyperion
                    listingcheck.Add(h1)
                Next

                If result.Length > 0 And FindLocation(listingcheck, client) = True Then

                    If loopedtime > 0 Then GoTo continueoutofloop


continueoutofloop:


                    'get appropriate list and dealer costs exluding zero dollar dealer cost
                    Dim listinglist As New List(Of ListingResult)
                    For Each hyplisting As PartsAvailability In result
                        Dim h1 As New ListingResult
                        If hyplisting.ListPrice > 0 And ClientHyps.Contains(hyplisting.Hyperion) Then
                            h1.Value = hyplisting.ListPrice
                            h1.Source = "Autonation"
                            listinglist.Add(h1)
                        End If
                    Next
                    Dim listinglist2 As New List(Of ListingResult)
                    For Each hyplisting As PartsAvailability In result
                        Dim h1 As New ListingResult
                        If hyplisting.DealerCost > 0 And ClientHyps.Contains(hyplisting.Hyperion) Then
                            h1.Value = hyplisting.DealerCost
                            h1.Source = "Autonation"
                            listinglist2.Add(h1)
                        End If
                    Next
                    Dim listinglist3 As New List(Of ListingResult)
                    For Each hyplisting As PartsAvailability In result
                        Dim h1 As New ListingResult
                        If hyplisting.CorePrice > 0 And ClientHyps.Contains(hyplisting.Hyperion) Then
                            h1.Value = hyplisting.CorePrice
                            h1.Source = "Autonation"
                            listinglist3.Add(h1)
                        Else
                            strcore = 0
                        End If
                    Next


                    'get rest of part info and hyperions with in stock
                    For Each listing As PartsAvailability In result
                        If ClientHyps.Contains(listing.Hyperion) Then
                            strdesc = listing.Description
                            strsuper = listing.SupersededPart
                            stronhand = stronhand + listing.OnHand
                            If Val(listing.OnHand) >= Val(thepart.Quantity) Then
                                'hyperion array
                                HyperionsInd.Add(listing.Hyperion, 1)

                                If Hyperions.GetValues(listing.Hyperion) Is Nothing Then 'first see if hyperion is array if not add to array if so, increase counter
                                    If HyperionsAdded.GetValues(listing.Hyperion) Is Nothing Then
                                        Hyperions.Add(listing.Hyperion, 1)
                                        HyperionsAdded.Add(listing.Hyperion, 1)
                                    End If
                                Else
                                    If HyperionsAdded.GetValues(listing.Hyperion) Is Nothing Then
                                        Dim item As String
                                        For Each item In New NameValueCollection(Hyperions)
                                            If item = listing.Hyperion Then
                                                Dim hyperioncount = Hyperions.GetValues(listing.Hyperion)
                                                Hyperions.Remove(item)
                                                Hyperions.Add(listing.Hyperion, CInt(hyperioncount.GetValue(0)) + 1)
                                                HyperionsAdded.Add(listing.Hyperion, 1)
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        End If
                    Next

                    'check MotorcraftFord cross
                    If MakeID = 11 Then
                        If boolCheckMotorcraftCross = False Then
                            thepart.PartNumber = CheckMortorcraftFordCross(thepart.PartNumber)
                            If thepart.PartNumber <> "" Then
                                boolCheckMotorcraftCross = True
                                GoTo MotorcraftCrossReRun
                            Else
                                thepart.PartNumber = strPartUserSearched
                            End If

                        Else
                            thepart.PartNumber = strPartUserSearched
                        End If
                    End If


                    'get final list
                    Dim listinglistb As New List(Of Decimal)

                    For Each ListingResult In listinglist
                        listinglistb.Add(ListingResult.Value)
                    Next
                    Dim counts = From n In listinglistb Group n By n Into Group
                                 Select Number = n, Count = Group.Count()
                                 Order By Count Descending, Number

                    For Each c In counts
                        strlist = FormatNumber(c.Number, 2)
                        strlistsource = FindSource(listinglist, c.Number)
                        Exit For
                    Next

                    'get final dealer
                    Dim listinglist2b As New List(Of Decimal)

                    For Each ListingResult In listinglist2
                        listinglist2b.Add(ListingResult.Value)
                    Next
                    Dim counts2 = From n2 In listinglist2b Group n2 By n2 Into Group
                                  Select Number2 = n2, Count2 = Group.Count()
                                  Order By Count2 Descending, Number2 Descending

                    For Each c2 In counts2
                        strdealer = FormatNumber(c2.Number2, 2)
                        strdealersource = FindSource(listinglist2, c2.Number2)
                        Exit For
                    Next
                    'get final core
                    Dim listinglist3b As New List(Of Decimal)

                    For Each ListingResult In listinglist3
                        listinglist3b.Add(ListingResult.Value)
                    Next
                    Dim counts3 = From n3 In listinglist3b Group n3 By n3 Into Group
                                  Select Number3 = n3, Count3 = Group.Count()
                                  Order By Count3 Descending, Number3 Descending

                    For Each c3 In counts3
                        strcore = FormatNumber(c3.Number3, 2)
                        strcoresource = FindSource(listinglist3, c3.Number3)
                        Exit For
                    Next

                    stryour = FormatNumber(GetCustCost(strcustno, MakeID, strdealer, strlist, client), 2)
                    strour = strdealer

                    'add hyperions to StockResponse for admin only
                    If Emulate <> "false" Or ClientHyps.Contains(strcustno) Then

                        Dim stocks As New List(Of StockResponse)
                        For Each itemhyp In New NameValueCollection(HyperionsInd)

                            Dim s1 As New StockResponse

                            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlComm5 As New SqlCommand("SELECT cutofftime, entity_name, phone_number, contact, companyid from tblAutonation WHERE hyperion = ('" & itemhyp & "')", conn)
                                conn.Open()
                                Using r5 As SqlDataReader = sqlComm5.ExecuteReader()
                                    While r5.Read()
                                        s1.Hyperion = itemhyp
                                        's1.OnHand = CInt(Hyperions.GetValues(itemhyp).GetValue(0))
                                        If IsDBNull(r5("Contact")) = False Then
                                            s1.Contact = r5("Contact")
                                        Else
                                            s1.Contact = ""
                                        End If
                                        If IsDBNull(r5("Phone_Number")) = False Then
                                            s1.Phone = r5("Phone_Number")
                                        Else
                                            s1.Phone = ""
                                        End If
                                        If IsDBNull(r5("Entity_Name")) = False Then
                                            s1.Name = r5("Entity_Name")
                                        Else
                                            s1.Name = ""
                                        End If
                                        If IsDBNull(r5("Cutofftime")) = False Then
                                            s1.Cutoff = CDate(r5("Cutofftime"))
                                        Else
                                            s1.Cutoff = ""
                                        End If
                                        s1.Note = ""
                                        'get our cost per this vendor/warehouse
                                        If IsDBNull(r5("CompanyID")) = False Then
                                            s1.OurCost = strdealer + strdealer * GetCost(MakeID, False, r5("CompanyID"), client)
                                        Else
                                            s1.OurCost = strdealer + strdealer * GetCost(MakeID, False, 10827, client)
                                        End If
                                    End While
                                End Using
                            End Using
                            stocks.Add(s1)
                        Next

                        p1.StockLevels = stocks

                    End If

                    If stronhand > 0 And stronhand >= Val(thepart.Quantity) Then
                        strstock = "Yes"
                    Else
                        strstock = "Call"
                        stroverallstock = "no"
                    End If

                    notfound = False
                ElseIf ClientHyps.Contains(strcustno) = True Then 'autonation store so search everywhere
                    Try
                        Dim clientSearch As PartsLookUpServiceClient = New PartsLookUpServiceClient()
                        'result = client.GetPartsAvailabilityByPartNumber("CandK", "dM7l@Sn0", Nothing, thepart.PartNumber)
                        resulteverywhere = clientSearch.GetPartsAvailabilityByAlphanumericPartNumber("CandK", "dM7l@Sn0", Nothing, thepart.PartNumber)
                        clientSearch.Close()
                    Catch Ex As WebException


                    End Try

                    If resulteverywhere.Length > 0 Then

                        'get appropriate list and dealer costs exluding zero dollar dealer cost
                        Dim listinglisteverywhere As New List(Of ListingResult)
                        For Each hyplistingeverywhere As PartsAvailability In resulteverywhere
                            Dim h1 As New ListingResult
                            If hyplistingeverywhere.ListPrice > 0 Then
                                h1.Value = hyplistingeverywhere.ListPrice
                                h1.Source = "Autonation"
                                listinglisteverywhere.Add(h1)
                            End If
                        Next
                        Dim listinglist2everywhere As New List(Of ListingResult)
                        For Each hyplistingeverywhere As PartsAvailability In resulteverywhere
                            Dim h1 As New ListingResult
                            If hyplistingeverywhere.DealerCost > 0 Then
                                h1.Value = hyplistingeverywhere.DealerCost
                                h1.Source = "Autonation"
                                listinglist2everywhere.Add(h1)
                            End If
                        Next
                        Dim listinglist3everywhere As New List(Of ListingResult)
                        For Each hyplistingeverywhere As PartsAvailability In resulteverywhere
                            Dim h1 As New ListingResult
                            If hyplistingeverywhere.CorePrice > 0 Then
                                h1.Value = hyplistingeverywhere.CorePrice
                                h1.Source = "Autonation"
                                listinglist3everywhere.Add(h1)
                            Else
                                strcore = 0
                            End If
                        Next

                        'get rest of part info and hyperions with in stock
                        For Each listing As PartsAvailability In resulteverywhere
                            strdesc = listing.Description
                            strsuper = listing.SupersededPart
                            stronhand = stronhand + listing.OnHand
                            If Val(listing.OnHand) >= Val(thepart.Quantity) Then
                                'hyperion array
                                HyperionsInd.Add(listing.Hyperion, 1)

                                If Hyperions.GetValues(listing.Hyperion) Is Nothing Then 'first see if hyperion is array if not add to array if so, increase counter
                                    If HyperionsAdded.GetValues(listing.Hyperion) Is Nothing Then
                                        Hyperions.Add(listing.Hyperion, 1)
                                        HyperionsAdded.Add(listing.Hyperion, 1)
                                    End If
                                Else
                                    If HyperionsAdded.GetValues(listing.Hyperion) Is Nothing Then
                                        Dim item As String
                                        For Each item In New NameValueCollection(Hyperions)
                                            If item = listing.Hyperion Then
                                                Dim hyperioncount = Hyperions.GetValues(listing.Hyperion)
                                                Hyperions.Remove(item)
                                                Hyperions.Add(listing.Hyperion, CInt(hyperioncount.GetValue(0)) + 1)
                                                HyperionsAdded.Add(listing.Hyperion, 1)
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                        Next

                        'get final list
                        Dim listinglistbeverywhere As New List(Of Decimal)

                        For Each ListingResult In listinglisteverywhere
                            listinglistbeverywhere.Add(ListingResult.Value)
                        Next
                        Dim counts = From n In listinglistbeverywhere Group n By n Into Group
                                     Select Number = n, Count = Group.Count()
                                     Order By Count Descending, Number

                        For Each c In counts
                            strlist = FormatNumber(c.Number, 2)
                            strlistsource = FindSource(listinglisteverywhere, c.Number)
                            Exit For
                        Next

                        'get final dealer
                        Dim listinglist2beverywhere As New List(Of Decimal)

                        For Each ListingResult In listinglist2everywhere
                            listinglist2beverywhere.Add(ListingResult.Value)
                        Next
                        Dim counts2 = From n2 In listinglist2beverywhere Group n2 By n2 Into Group
                                      Select Number2 = n2, Count2 = Group.Count()
                                      Order By Count2 Descending, Number2 Descending

                        For Each c2 In counts2
                            strdealer = FormatNumber(c2.Number2, 2)
                            strdealersource = FindSource(listinglist2everywhere, c2.Number2)
                            Exit For
                        Next
                        'get final core
                        Dim listinglist3beverywhere As New List(Of Decimal)

                        For Each ListingResult In listinglist3everywhere
                            listinglist3beverywhere.Add(ListingResult.Value)
                        Next
                        Dim counts3 = From n3 In listinglist3beverywhere Group n3 By n3 Into Group
                                      Select Number3 = n3, Count3 = Group.Count()
                                      Order By Count3 Descending, Number3 Descending

                        For Each c3 In counts3
                            strcore = FormatNumber(c3.Number3, 2)
                            strcoresource = FindSource(listinglist3everywhere, c3.Number3)
                            Exit For
                        Next

                        stryour = FormatNumber(GetCustCost(strcustno, MakeID, strdealer, strlist, client), 2)
                        strour = strdealer

                        strstock = "No"
                        stroverallstock = "no"
                        strnocheckout = "yes"
                        notfound = False

                    Else
                        strdesc = "No Parts Found"
                        strnocheckout = "yes"

                    End If

                Else
                    strdesc = "Not Stocked at " & client
                    strnocheckout = "yes"
                End If

                strdisplaycore = strcore

                p1.Core = strdisplaycore

                p1.Description = If(IsNothing(strdesc), "", strdesc)
                p1.List = strlist
                p1.Row = x
                p1.SupersededPart = strsuper
                If p1.Description = "No Parts Found" Or p1.Description = "Not Stocked at " & client Then
                    p1.Stock = ""
                Else
                    p1.Stock = strstock
                End If

                p1.PartNumber = thepart.PartNumber
                p1.Our = strour
                p1.Quantity = thepart.Quantity
                p1.Your = stryour

                'ins(quote)
                If Quote = "true" Then
                    Dim strsqlins As String
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        strsqlins = "insert into tbloemquotes(quotedate, username, customerno, make, part, description, quantity, listprice, coreprice, ourcost, theirprice, discontinued, InStock, listsource, dealersource, coresource) values ('" & Now() & "','" & Name & "','" & strcustno & "','" & GetOEMID(MakeID, client) & "','" & thepart.PartNumber & "','" & p1.Description & "','" & thepart.Quantity & "'," & p1.List & "," & strcore & "," & strour & "," & p1.Your & ",0,'" & strstock & "','" & strlistsource & "','" & strdealersource & "','" & strcoresource & "')"
                        Dim sqlComm7 As New SqlCommand(strsqlins, conn)
                        conn.Open()
                        sqlComm7.ExecuteNonQuery()
                    End Using
                End If

                js.Serialize(p1)
                list.Add(p1)
                x = x + 1
            End If
        Next

        'remove any hyperions that dont have all parts
        If Hyperions.Count > 0 Then
            'first clone it to be used when multiple warehouses come into play
            HyperionsMultiple = New NameValueCollection(Hyperions)
            strpartcount = x - 1
            Dim itemh As String
            For Each itemh In New NameValueCollection(Hyperions)

                If CInt(Hyperions.GetValues(itemh).GetValue(0)) < strpartcount Then
                    Hyperions.Remove(itemh)

                End If
            Next
        End If

        o1.Complicated = strcomplicated

        Dim strCutoffMinutestemp As String = String.Empty
        strCutoffMinutestemp = 1440
        'get cutoff
        GetDefaults(client)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlCommCutt As New SqlCommand("SELECT dbo.tblCutoff.CutOff fROM  dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN dbo.tblCompany ON dbo.aspnet_Membership.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.tblTerritory ON dbo.tblCompany.Zip = dbo.tblTerritory.Zip INNER JOIN dbo.tblCutoff ON dbo.tblTerritory.Territory = dbo.tblCutoff.Route WHERE (dbo.aspnet_Users.UserName = '" & Name & "')", conn)
            conn.Open()
            Using rCutt As SqlDataReader = sqlCommCutt.ExecuteReader()
                While rCutt.Read()
                    If TimeOfDay < clientdeliveryrollovertime Then
                        'strcutoff = CDate(rCutt("CutOff"))
                        Dim cutoffdatetime As Date
                        If rCutt("Cutoff") < TimeOfDay Then
                            cutoffdatetime = FormatDateTime(DateAdd(DateInterval.Day, 1, Now()), vbShortDate) & " " & rCutt("Cutoff")
                        Else
                            cutoffdatetime = FormatDateTime(Now(), vbShortDate) & " " & rCutt("Cutoff")
                        End If


                        If DateDiff(DateInterval.Minute, Now(), cutoffdatetime) < strCutoffMinutestemp Then
                            strCutoffMinutes = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
                            strcutoff = CDate(rCutt("CutOff"))
                            strCutoffMinutestemp = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
                        End If
                    Else
                        'strcutoff = CDate(rCutt("CutOff"))

                        Dim cutoffdatetime As Date
                        cutoffdatetime = FormatDateTime(DateAdd(DateInterval.Day, 1, Now()), vbShortDate) & " " & rCutt("Cutoff")
                        If DateDiff(DateInterval.Minute, Now(), cutoffdatetime) < strCutoffMinutestemp Then
                            strCutoffMinutes = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
                            strcutoff = CDate(rCutt("CutOff"))
                            strCutoffMinutestemp = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
                        End If
                    End If
                End While
            End Using
        End Using


        'check to see if no hyperions have all parts(only applies to a Autonation search)

        If Hyperions.Count = 0 And HyperionsMultiple.Count > 0 Then 'parts in multiple stores
            strcomplicated = "yes"



            'add hyperions to StockResponse for admin only
            If Emulate <> "false" Or ClientHyps.Contains(strcustno) Then

                Dim stocks As New List(Of StockResponse)
                For Each keyz In HyperionsMultiple.Keys

                    Dim s1 As New StockResponse

                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlComm5 As New SqlCommand("SELECT cutofftime, entity_name, phone_number, contact, companyid from tblAutonation WHERE hyperion = ('" & keyz.ToString & "')", conn)
                        conn.Open()
                        Using r5 As SqlDataReader = sqlComm5.ExecuteReader()
                            While r5.Read()
                                s1.Hyperion = keyz.ToString
                                's1.OnHand = CInt(Hyperions.GetValues(itemhyp).GetValue(0))
                                If IsDBNull(r5("Contact")) = False Then
                                    s1.Contact = r5("Contact")
                                Else
                                    s1.Contact = ""
                                End If
                                If IsDBNull(r5("Phone_Number")) = False Then
                                    s1.Phone = r5("Phone_Number")
                                Else
                                    s1.Phone = ""
                                End If
                                If IsDBNull(r5("Entity_Name")) = False Then
                                    s1.Name = r5("Entity_Name")
                                Else
                                    s1.Name = ""
                                End If
                                'If IsDBNull(r5("Cutofftime")) = False Then
                                '    s1.Cutoff = CDate(r5("Cutofftime"))
                                'Else
                                '    s1.Cutoff = ""
                                'End If
                                'get our cost per this vendor/warehouse
                                'If IsDBNull(r5("CompanyID")) = False Then
                                's1.OurCost = strdealer + strdealer * GetCost(MakeID, False, r5("CompanyID"), client)
                                'Else
                                's1.OurCost = strdealer + strdealer * GetCost(MakeID, False, 10827, client)
                                'End If
                                s1.Note = "Some Parts"
                            End While
                        End Using
                    End Using
                    stocks.Add(s1)



                Next

                o1.StockLevels = stocks
            End If



        ElseIf Hyperions.Count > 0 And HyperionsMultiple.Count > 0 Then 'parts in single store
            strcomplicated = "no"


            'add hyperions to StockResponse for admin only
            If Emulate <> "false" Or ClientHyps.Contains(strcustno) Then

                Dim ostocks As New List(Of StockResponse)
                For Each keyz In Hyperions.Keys


                    Dim s1 As New StockResponse
                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlComm5 As New SqlCommand("SELECT cutofftime, entity_name, phone_number, contact, companyid from tblAutonation WHERE hyperion = ('" & keyz.ToString & "')", conn)
                        conn.Open()
                        Using r5 As SqlDataReader = sqlComm5.ExecuteReader()
                            While r5.Read()
                                s1.Hyperion = keyz.ToString
                                's1.OnHand = CInt(Hyperions.GetValues(itemhyp).GetValue(0))
                                If IsDBNull(r5("Contact")) = False Then
                                    s1.Contact = r5("Contact")
                                Else
                                    s1.Contact = ""
                                End If
                                If IsDBNull(r5("Phone_Number")) = False Then
                                    s1.Phone = r5("Phone_Number")
                                Else
                                    s1.Phone = ""
                                End If
                                If IsDBNull(r5("Entity_Name")) = False Then
                                    s1.Name = r5("Entity_Name")
                                Else
                                    s1.Name = ""
                                End If
                                'If IsDBNull(r5("Cutofftime")) = False Then
                                '    s1.Cutoff = CDate(r5("Cutofftime"))
                                'Else
                                '    s1.Cutoff = ""
                                'End If
                                'get our cost per this vendor/warehouse
                                'If IsDBNull(r5("CompanyID")) = False Then
                                '    s1.OurCost = strdealer + strdealer * GetCost(MakeID, False, r5("CompanyID"), client)
                                'Else
                                '    s1.OurCost = strdealer + strdealer * GetCost(MakeID, False, 10827, client)
                                'End If
                                s1.Note = "All Parts"
                            End While
                        End Using
                    End Using
                    ostocks.Add(s1)

                Next
                o1.StockLevels = ostocks
            End If
        End If


        o1.Cutoff = strcutoff
        o1.CutoffMinutes = strCutoffMinutes
        o1.OverallInStock = stroverallstock

        o1.ForceShip = strforceship
        If strnocheckout = "no" Then
            o1.NoCheckout = NoCheckout(Name, client)
        Else
            o1.NoCheckout = strnocheckout
        End If


        'double check to make sure all parts are valid
        Dim results As List(Of PartsResponse) = list.FindAll(AddressOf FindNoPartsfound)
        If results.Count <> 0 Then
            If results.Count = list.Count Then 'no valid parts
                o1.NoShipping = "yes"
            Else
                o1.NoShipping = "no"
            End If
        Else
            o1.NoShipping = "no"
        End If

        'tax
        o1.Tax = "yes"
        js.Serialize(o1)
        listOverall.Add(o1)

        'send response
        Dim response = New ArrayList
        response.Add(list)
        response.Add(listOverall)

        Return js.Serialize(response)
    End Function


    Private Function ExcludeHyperion(ByVal hyperion As String) As Boolean

        '' Go stores check
        'Dim sqlComm As New SqlCommand("SELECT count(*) from tblAutonation where left(entity_name,3)= 'Go ' and hyperion = '" & hyperion & "'", myConnection)
        'myConnection.Open()
        'Dim thecount = sqlComm.ExecuteScalar()
        'myConnection.Close()

        'If thecount = 1 Then
        '    Return True
        'End If

        'Exit Function
        Return False


    End Function

    Protected Function GetCost(ByVal MakeID As Long, ByVal customer As Boolean, ByVal vendor As Long, ByVal client As String) As String
        Dim strOEM As Long
        Dim strmarkup As Decimal
        'get oemid first
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm1 As New SqlCommand("SELECT oemid from tblmake WHERE makeid = " & MakeID, conn)
            conn.Open()
            strOEM = sqlComm1.ExecuteScalar()
        End Using

        'get markup
        If customer = False Then
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm2 As New SqlCommand("SELECT markup from tbloemdealer WHERE companyid = " & vendor & " and oemid = " & strOEM, conn)
                conn.Open()
                strmarkup = sqlComm2.ExecuteScalar()
            End Using
        Else
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm2 As New SqlCommand("SELECT markupcustomer from tbloemdealer WHERE companyid = " & vendor & " and oemid = " & strOEM, conn)
                conn.Open()
                strmarkup = sqlComm2.ExecuteScalar()
            End Using

        End If
        Return strmarkup
    End Function

    Protected Function GetCustCost(ByVal CustNo As String, ByVal MakeID As Long, ByVal client As String) As String

        Dim strOEM As Long
        Dim strmarkup As Decimal

        'get oemid first
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm1 As New SqlCommand("SELECT oemid from tblmake WHERE makeid = " & MakeID, conn)
            conn.Open()
            strOEM = sqlComm1.ExecuteScalar()
        End Using

        'get markup

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT markup from tbloemcompany WHERE oemid = " & strOEM & " and customerno = '" & CustNo & "'", conn)
            conn.Open()
            strmarkup = sqlComm2.ExecuteScalar()
        End Using

        Return strmarkup
    End Function
    <WebMethod()>
    Public Function PlaceOrder(ByVal Emulate As String, ByVal Name As String, ByVal MakeID As String, ByVal Model As String, ByVal VIN As String, ByVal Mileage As String, ByVal Year As String, ByVal Drive As String, ByVal Trans As String, ByVal ContractNo As String, ByVal AuthNo As String, ByVal Owner As String, ByVal PO As String, ByVal Shop As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Contact As String, ByVal Phone As String, ByVal Parts As PartsResponse(), ByVal Warranty As String, ByVal WarrantyCost As String, ByVal WarrantyDate As String, ByVal WarrantyMileage As String, ByVal ShippingCost As String, ByVal ReturnShippingCost As String, ByVal ShippingType As String, ByVal client As String, ByVal IP As String)
        Dim strorderid As Long
        Dim strcustno As String = String.Empty
        Dim stroemid As String = String.Empty

        Dim stradjemail As String = String.Empty
        Dim strcore As Boolean
        Dim strsavings As Decimal

        GetDefaults(client)
        strsavings = 0

        strcustno = Emulate

        'first get customer number and email
        If Emulate = "false" Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo, dbo.aspnet_Membership.Email FROM dbo.aspnet_Membership INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId WHERE dbo.aspnet_Users.UserName = '" & Name & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        strcustno = r("CustomerNo")
                        stradjemail = r("Email")
                    End While
                End Using
            End Using
        Else

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo, dbo.aspnet_Membership.Email FROM dbo.aspnet_Membership INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId WHERE dbo.aspnet_Users.UserName = '" & Name & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        stradjemail = r("Email")
                    End While
                End Using
            End Using
        End If
        'get make name
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm3 As New SqlCommand("SELECT oemid from tblmake WHERE makeid = '" & MakeID & "'", conn)
            conn.Open()
            stroemid = sqlComm3.ExecuteScalar()
        End Using

        'insert in order table
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm4 As New SqlCommand("insert into tblorders(orderdate, username, customerno, oemid, ordertype, shop, address, city, state, zip, phone, contact, IP) select '" & Now() & "', '" & Name & "','" & strcustno & "','" & stroemid & "', 'OEM','" & Shop & "','" & Address & "','" & City & "','" & State & "','" & Zip & "','" & Phone & "','" & Contact & "','" & IP & "';SELECT orderid FROM tblorders WHERE (orderid = SCOPE_IDENTITY())", conn)
            conn.Open()
            strorderid = sqlComm4.ExecuteScalar()
        End Using

        'insert in parts table
        For Each thepart As PartsResponse In Parts
            strsavings = strsavings + (thepart.List - thepart.Your)

            If IsDBNull(thepart.Core) = False And Val(thepart.Core) > 0 Then
                strcore = True
            Else
                strcore = False
            End If

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm5 As New SqlCommand("insert into tblorderdetails(orderid,part,description,quantity,listprice,coreprice,ourcost,theirprice,instock) values ('" & strorderid & "','" & thepart.PartNumber & "','" & thepart.Description & "','" & thepart.Quantity & "','" & thepart.List & "','" & thepart.Core & "','" & thepart.Our & "','" & thepart.Your & "','" & thepart.Stock & "')", conn)
                conn.Open()
                sqlComm5.ExecuteNonQuery()
            End Using

        Next

        EmailCustomer(strcustno, stradjemail, Parts, client)
        EmailSalesman(strcustno, stradjemail, Parts, Contact, Phone, PO, Shop, Address, City, State, Zip, MakeID, VIN, client)

        Return strorderid '& "/" & GetSavings(strsavings)
    End Function

    '<WebMethod()> _
    'Public Function GetMarkup(ByVal custno As String, ByVal oemid As String)

    '    Dim strmarkup As Decimal
    '    Dim strsource As String
    '    Dim js As New JavaScriptSerializer()

    '    Dim sqlComm2 As New SqlCommand("SELECT source, markup from tbloemcompany WHERE oemid = " & oemid & " and customerno = '" & custno & "'", SetClientConnectionString2(client))
    '    SetClientConnectionString2(client).Open()
    '    Using r as sqldatareader
    '    r = sqlComm2.ExecuteReader()
    '    While r.Read()
    '        strsource = r("Source")
    '        strmarkup = r("Markup")
    '    End While
    '    end using
    '    SetClientConnectionString2(client).Close()

    '    Dim m1 As New Markup
    '    Dim list As New List(Of Markup)
    '    m1.Markup = FormatPercent(strmarkup, 1)
    '    m1.Source = strsource
    '    m1.MarkupVal = FormatNumber(strmarkup * 100, 2)
    '    list.Add(m1)
    '    Return js.Serialize(list)
    'End Function

    <WebMethod()>
    Public Function GetQuotes(ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Quote)


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT top 350 dbo.tblOemQuotes.QuoteID, dbo.tblOemQuotes.QuoteDate, dbo.tblOemQuotes.Username, dbo.tblCompany.Company, dbo.tblMake.Make, dbo.tblOemQuotes.Part, dbo.tblOemQuotes.Description, dbo.tblOemQuotes.Quantity, dbo.tblOemQuotes.ListPrice, dbo.tblOemQuotes.CorePrice, dbo.tblOemQuotes.OurCost, dbo.tblOemQuotes.TheirPrice, dbo.tblOemQuotes.InStock FROM dbo.tblOemQuotes INNER JOIN dbo.tblMake ON dbo.tblOemQuotes.Make = dbo.tblMake.OEMID INNER JOIN dbo.tblCompany ON dbo.tblOemQuotes.CustomerNo = dbo.tblCompany.CustomerNo  order by quoteid desc", conn)

            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim q1 As New Quote()
                    q1.Core = r("CorePrice")
                    q1.Cost = r("OurCost")
                    q1.Customer = r("Company")
                    q1.Desc = r("Description")
                    q1.InStock = r("InStock")
                    q1.List = r("ListPrice")
                    q1.Make = r("Make")
                    q1.Part = r("Part")
                    q1.Quantity = r("Quantity")
                    q1.QuoteDate = r("QuoteDate")
                    q1.QuoteID = r("QuoteID")
                    q1.SellPrice = r("TheirPrice")
                    q1.User = r("UserName")
                    list.Add(q1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

    <WebMethod()>
    Public Function GetOrdersAdmin(ByVal client As String, ByVal type As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Order)


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT  dbo.tblOrders.Notes,dbo.tblOrders.OrderID, dbo.tblOrders.OrderType, dbo.tblOrders.OrderDate, dbo.tblOrders.Username, dbo.tblCompany.Company AS Customer, case when ordertype <> 'OEM' then AutoMake else dbo.tblMake.Make end as Make, details.TotalParts, details.TotalSale, details.TotalCost, details.Profit, case when details.totalsale <> 0 then details.Profit / details.TotalSale * 100 else 0 end AS GPProfit FROM  dbo.tblOrders LEFT OUTER JOIN dbo.tblCompany ON dbo.tblOrders.CustomerNo = dbo.tblCompany.CustomerNo Left Outer Join dbo.tblMake ON dbo.tblOrders.OEMID = dbo.tblMake.OEMID INNER JOIN (SELECT     OrderID, COUNT(PartID) AS TotalParts, SUM(TheirPrice) AS TotalSale, SUM(OurCost) AS TotalCost, SUM(TheirPrice - OurCost) AS Profit FROM dbo.tblOrderDetails GROUP BY OrderID) AS details ON details.OrderID = dbo.tblOrders.OrderID where dbo.tblOrders.OrderType = '" & type & "' order by orderid desc", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim o1 As New Order()
                    o1.Customer = r("Customer").ToString()
                    o1.GPProfit = r("GPProfit")
                    o1.Make = r("Make").ToString()
                    o1.OrderDate = r("OrderDate")
                    o1.OrderID = r("OrderID")
                    o1.OrderType = r("OrderType")
                    o1.Profit = r("Profit")
                    o1.TotalCost = r("TotalCost")
                    o1.TotalParts = r("TotalParts")
                    o1.TotalSale = r("TotalSale")
                    o1.User = r("Username")
                    o1.Notes = r("Notes").ToString

                    list.Add(o1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function GetOrdersCust(ByVal username As String, ByVal client As String, ByVal type As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Order)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT  dbo.tblOrders.Notes, dbo.tblOrders.OrderID, dbo.tblOrders.OrderType, dbo.tblOrders.OrderDate, dbo.tblOrders.Username, dbo.tblCompany.Company AS Customer, case when ordertype <> 'OEM' then AutoMake else dbo.tblMake.Make end as Make, details.TotalParts, details.TotalSale FROM  dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN dbo.tblOrders INNER JOIN dbo.tblCompany ON dbo.tblOrders.CustomerNo = dbo.tblCompany.CustomerNo Left Outer JOIN dbo.tblMake ON dbo.tblOrders.OEMID = dbo.tblMake.OEMID INNER JOIN (SELECT     OrderID, COUNT(PartID) AS TotalParts, SUM(TheirPrice) AS TotalSale FROM dbo.tblOrderDetails GROUP BY OrderID) AS details ON details.OrderID = dbo.tblOrders.OrderID ON dbo.aspnet_Membership.CustomerNo = dbo.tblCompany.CustomerNo WHERE (dbo.aspnet_Users.UserName = '" & username & "' AND dbo.tblOrders.OrderType = '" & type & "')  order by orderid desc", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim o1 As New Order()
                    o1.Customer = r("Customer")
                    'o1.GPProfit = r("GPProfit")
                    o1.Make = r("Make")
                    o1.OrderDate = r("OrderDate")
                    o1.OrderID = r("OrderID")
                    o1.OrderType = r("OrderType")
                    ' o1.Profit = r("Profit")
                    ' o1.TotalCost = r("TotalCost")
                    o1.TotalParts = r("TotalParts")
                    o1.TotalSale = r("TotalSale")
                    o1.User = r("Username")
                    o1.Notes = r("Notes").ToString


                    list.Add(o1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function GetEliteOrders(ByVal username As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Order)

        If client <> "GO" Then
            Return False
            Exit Function
        End If



        Dim SearchUrl As String = "http://chromium.eliteextra.com/extra-api/GetInvoiceStatus.action?dealerId=20&locationId=99398&custNo={0}&hash=testing&json=true"
        Dim formattedUri As String = [String].Format(CultureInfo.InvariantCulture, SearchUrl, GetCustomerNo(username, client))

        Dim webRequest As HttpWebRequest = GetWebRequest(formattedUri)
        Dim response As HttpWebResponse = DirectCast(webRequest.GetResponse(), HttpWebResponse)
        Dim jsonResponse As String = String.Empty
        Using sr As New StreamReader(response.GetResponseStream())
            jsonResponse = sr.ReadToEnd()
        End Using
        Return js.Serialize(jsonResponse)

    End Function
    <WebMethod()>
    Public Function GetOrderDetails(ByVal orderid As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of OrderDetail)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT * from tblOrderDetails where orderid = '" & orderid & "' order by partid", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim o1 As New OrderDetail()
                    o1.CorePrice = r("CorePrice").ToString
                    o1.Description = r("Description").ToString
                    o1.InStock = r("Instock").ToString
                    'o1.ListPrice = r("ListPrice").ToString
                    o1.OurCost = r("Ourcost").ToString
                    o1.Part = r("Part").ToString
                    o1.PartID = r("Partid").ToString
                    o1.Quantity = r("Quantity").ToString
                    o1.TheirPrice = r("TheirPrice").ToString

                    list.Add(o1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function


    Function EmailCustomer(ByVal custno As String, ByVal adjemail As String, ByVal Parts As PartsResponse(), ByVal client As String)

        GetDefaults(client)

        Dim mm As New System.Net.Mail.MailMessage(clientnoreplyemail, adjemail)
        mm.Subject = clientwebsitename & " OEM Parts Order Received"
        Dim strbody As String = String.Empty

        For Each thepart As PartsResponse In Parts
            strbody &= "Part:" & thepart.PartNumber & "-" & thepart.Description & "-" & thepart.Your & "<br/>"
        Next

        strbody &= "<br/><br/> Your order will be processed as soon as possible and you will be contacted with any issues."
        mm.Body = strbody
        mm.IsBodyHtml = True

        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
            Return True
        Catch Ex As WebException
            Console.WriteLine(Ex)
            Return False
        End Try

    End Function

    Function EmailSalesman(ByVal custno As String, ByVal adjemail As String, ByVal Parts As PartsResponse(), ByVal contact As String, ByVal phone As String, ByVal PO As String, ByVal Shop As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal MakeID As String, ByVal VIN As String, ByVal client As String)

        GetDefaults(client)
        Dim strmake
        'get make name
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm3 As New SqlCommand("SELECT make from tblmake WHERE makeid = '" & MakeID & "'", conn)
            conn.Open()
            strmake = sqlComm3.ExecuteScalar()
        End Using

        'get salesman email

        Dim stremails As String
        stremails = clientemails
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT salesmanemail from tblcompany where customerno = '" & custno & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If IsDBNull(r("salesmanemail")) = False Then stremails = r("salesmanemail")

                End While
            End Using
        End Using


        'split emails if any

        Dim emailtocc() As String
        emailtocc = stremails.Split(",")



        Dim mm As New System.Net.Mail.MailMessage(adjemail, emailtocc(0))
        mm.Subject = clientwebsitename & " OEM Order"
        Dim strbody As String

        strbody = "PO#:" & PO & "<br/>"
        strbody = strbody & "Shop:" & Shop & "<br/>"
        strbody = strbody & "Address:" & Address & "<br/>"
        strbody = strbody & "City:" & City & "<br/>"
        strbody = strbody & "State:" & State & "<br/>"
        strbody = strbody & "Zip:" & Zip & "<br/>"
        strbody = strbody & "Contact:" & contact & "<br/>"
        strbody = strbody & "Phone:" & phone & "<br/>"
        strbody = strbody & "Make:" & strmake & "<br/><br/>"
        strbody = strbody & "VIN:" & VIN & "<br/><br/>"


        For Each thepart As PartsResponse In Parts
            strbody = strbody & "Part No:" & thepart.PartNumber & "-" & thepart.Description & "-Quantity:" & thepart.Quantity & "(" & thepart.Your & ")<br/>"
        Next
        mm.Body = strbody
        mm.IsBodyHtml = True

        For x As Integer = 1 To emailtocc.Count - 1
            mm.CC.Add(emailtocc(x))
        Next

        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
            Return True
        Catch Ex As WebException
            Console.WriteLine(Ex)
            Return False
        End Try

    End Function



    <WebMethod()>
    Public Function QuoteCount(ByVal client As String)
        Dim strcount As Long
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT count(quoteid) from tbloemquotes", conn)
            conn.Open()
            strcount = sqlComm.ExecuteScalar()
            strcount = strcount + 100000
        End Using
        Return strcount
    End Function


    <WebMethod()>
    Public Function GetDeliveryInfo(ByVal name As String, ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim c As New Customer
        'Dim list As New List(Of Customer)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT tblCompany.*, aspnet_membership.tierid FROM aspnet_Membership INNER JOIN aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId INNER JOIN tblCompany ON aspnet_Membership.CustomerNo = tblCompany.CustomerNo WHERE (aspnet_Users.UserName = '" & name & "')", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()
                While r.Read()
                    c.CompanyID = r("CompanyID")
                    c.Company = r("Company")
                    c.CustNo = r("CustomerNo")
                    If client <> "CK" Then
                        c.Address = r("Address")
                    Else
                        c.Address = r("Address1")
                    End If

                    c.City = r("City")
                    c.State = r("State")
                    c.Zip = r("Zip")
                    c.Phone = r("Phone")
                    'If (IsDBNull(r("SalesmanEmail"))) Then
                    '    c.SalesmanEmail = ""
                    'Else
                    '    c.SalesmanEmail = r("SalesmanEmail")
                    'End If
                    'c.Autonation = r("Autonation")
                    'c&k warranty company fixes
                    If client = "CK" And r("TierID").ToString = "3" Then
                        c.Company = " "
                        c.Address = " "
                        c.City = " "
                        c.State = " "
                        c.Zip = " "
                        c.Phone = " "
                    End If

                End While
            End Using
        End Using


        'list.Add(c)
        Return js.Serialize(c)
    End Function





    <WebMethod()>
    Public Function InitiateQuote(ByVal rows As String)
        Dim js As New JavaScriptSerializer
        'Dim quoteids As New ArrayList

        'For x As Integer = 0 To rows - 1
        '    Dim strsql As String
        '    strsql = "Insert into tblOEMQuotes (username) values ('awaiting quote'); Select Scope_Identity()"
        '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
        '        conn.Open()
        '        Dim sqlComm2 As New SqlCommand(strsql, conn)
        '        Dim quoteid = sqlComm2.ExecuteScalar
        '        quoteids.Add(quoteid)
        '    End Using
        'Next
        Dim pricing As New OEMSmallPartPricing.Pricing
        Return js.Serialize(pricing.InitiateQuote(rows))
    End Function
    <WebMethod()>
    Public Function CheckCartContents(ByVal Emulate As String, ByVal Zip As String, ByVal MakeID As String, ByVal Cart As CartParts(), ByVal Name As String)
        Dim hasOEM, hasAftermarket As Boolean
        Dim Parts As New List(Of OEMSmallPartPricing.Pricing.Parts)
        Dim AfterParts As New List(Of AfterParts)
        Dim o1 As New OverallSearch
        Dim js As New JavaScriptSerializer
        Dim strcustno As String
        Dim HasCore As Long = 0

        strcustno = Emulate
        If Emulate = False Then
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo FROM dbo.aspnet_Membership INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId WHERE dbo.aspnet_Users.UserName = '" & Name & "'", conn)
                conn.Open()
                strcustno = sqlComm.ExecuteScalar()
            End Using
        End If

        'first lets see what is in the cart
        hasAftermarket = If(FindAftermarketInCart(Cart) = True, True, False)
        hasOEM = If(FindOEMInCart(Cart) = True, True, False)


        'OEM PArts
        If hasOEM = True Then
            'add cart items to Parts class 
            For Each item In Cart
                If UCase(item.PartType) = UCase("OEM") Then
                    Dim p1 As New OEMSmallPartPricing.Pricing.Parts
                    p1.PartNumber = item.PartNumber
                    p1.Quantity = item.Quantity
                    Parts.Add(p1)
                End If
            Next
            'check to see if complicated and overall in stock
            Dim response2 = New ArrayList
            response2 = js.Deserialize(Of ArrayList)(GetOEMPrice(strcustno, False, MakeID, "shopping cart", "", Parts.ToArray, "CK"))
            o1.Complicated = response2(1).Item(0).Item("Complicated")
            o1.Cutoff = response2(1).Item(0).Item("Cutoff")
            o1.CutoffMinutes = response2(1).Item(0).Item("CutoffMinutes")
            o1.ForceShip = response2(1).Item(0).Item("ForceShip")
            o1.NoCheckout = response2(1).Item(0).Item("NoCheckout")
            o1.NoShipping = response2(1).Item(0).Item("NoShipping")
            o1.OverallInStock = response2(1).Item(0).Item("OverallInStock")
            'o1.StockLevels = response2(1).Item(0).Item("StockLevels")
            o1.Tax = response2(1).Item(0).Item("Tax")
            For Each item In response2(0)
                If item("Core") > 0 Then HasCore = 1

            Next
            o1.HasCore = HasCore

        End If

        ''''''''''''''''''''''''''''''''''
        'Aftermarket Parts
        If hasAftermarket = True Then
            'add cart items to AfterParts class 
            For Each item In Cart
                If UCase(item.PartType) = UCase("Aftermarket") Then
                    Dim a1 As New AfterParts
                    a1.PartNumber = item.PartNumber
                    a1.Quantity = item.Quantity
                    AfterParts.Add(a1)
                End If
            Next

            'Dim AfterResponse As New List(Of PartsResponse)
            'AfterResponse = GetAfterPrice(MakeID, Name, Zip, AfterParts.ToArray)

            ''get shipping
            'For Each PartsResponse In AfterResponse
            '    ' o1.AftermarketShipping = PartsResponse.Shipping
            '    ' o1.OverallInStock = PartsResponse.SupersededPart 'remove this janky shit later
            '    Exit For
            'Next
            'no checkout
            If o1.NoCheckout = "" Or o1.NoCheckout = " " Or o1.NoCheckout = Nothing Or IsDBNull(o1.NoCheckout) Then
                o1.NoCheckout = NoCheckout(Name, "CK")
            End If
            'tax
            If o1.Tax = "" Or o1.Tax = " " Or o1.Tax = Nothing Or IsDBNull(o1.Tax) Then
                o1.Tax = If(strcustno = "456", "yes", "no")
            End If
            'cutoff
            If o1.Cutoff = "" Or o1.Cutoff = " " Or o1.Cutoff = Nothing Or IsDBNull(o1.Cutoff) Then
                o1.Cutoff = CDate("16:00") 'get cardone cutoff if no oem parts
                Dim cutoffdatetime As Date
                cutoffdatetime = FormatDateTime(Now(), vbShortDate) & " 16:00"
                o1.CutoffMinutes = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
            Else
                If o1.Cutoff > CDate("16:00") Then
                    'if has oem but its after cardone cutoff use cardone
                    o1.Cutoff = CDate("16:00")
                    Dim cutoffdatetime As Date
                    cutoffdatetime = FormatDateTime(Now(), vbShortDate) & " 16:00"
                    o1.CutoffMinutes = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
                End If

            End If


        End If

        'get cutoff time

        'return overall search
        Return (js.Serialize(o1))
    End Function
    Private Function FindOEMInCart(ByVal Cart As CartParts()) As Boolean
        For Each item In Cart
            If UCase(item.PartType) = UCase("OEM") Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function
    Private Function FindAftermarketInCart(ByVal Cart As CartParts()) As Boolean
        For Each item In Cart
            If UCase(item.PartType) = UCase("Aftermarket") Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function
    'Public Function FindAftermarketProvider(ByVal oem As String, ByVal aftermarket As String, ByVal name As String, ByVal custno As String)
    '    FindAftermarketProvider = "none"

    '    'check fmp as aftermarket first
    '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '        Dim sqlComm As New SqlCommand("select count(id) from tblfmpcatalog where replace(part_number,'-','') ='" & aftermarket & "' or replace(long_number,'-','') ='" & aftermarket & "'", conn)
    '        conn.Open()
    '        Dim thecount As Long = sqlComm.ExecuteScalar()
    '        If thecount > 0 Then
    '            FindAftermarketProvider = "FMP"
    '            Exit Function
    '        End If
    '    End Using

    '    'check fmp as oem 
    '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '        Dim sqlComm As New SqlCommand("select count(id) from tblfmpcatalog where replace(part_number,'-','') ='" & oem & "' or replace(long_number,'-','') ='" & oem & "'", conn)
    '        conn.Open()
    '        Dim thecount As Long = sqlComm.ExecuteScalar()
    '        If thecount > 0 Then
    '            FindAftermarketProvider = "FMP"
    '            Exit Function
    '        End If
    '    End Using

    '    'check zumbrota next
    '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '        Dim sqlComm As New SqlCommand("select count(id) from tblzumbrotacatalog where replace(oem,'-','') ='" & oem & "'", conn)
    '        conn.Open()
    '        Dim thecount As Long = sqlComm.ExecuteScalar()
    '        If thecount > 0 Then
    '            FindAftermarketProvider = "Zumbrota"
    '            Exit Function
    '        End If
    '    End Using
    '    'check cardone next
    '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '        Dim sqlComm As New SqlCommand("select count(*) from tblCarDoneCatalog WHERE replace([OE Part Number],'-','') = '" & oem & "'", conn)
    '        conn.Open()
    '        Dim thecount As Long = sqlComm.ExecuteScalar()
    '        If thecount > 0 Then
    '            FindAftermarketProvider = "Cardone"
    '            Exit Function
    '        End If
    '    End Using

    '    Return FindAftermarketProvider

    'End Function
    Private Sub CarDoneSuper(ByVal PartNumber As String, ByVal Supercession As String)
        Dim thecount
        'first make sure supercession is not already in catalog
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT count(id) from tblCarDoneCatalog WHERE [OE Part Number] = '" & RTrim(LTrim(Supercession)) & "'", conn)
            conn.Open()
            thecount = sqlComm.ExecuteScalar()
        End Using

        If thecount = 0 Then 'insert
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm2 As New SqlCommand("insert into tblCarDoneCatalog( [Product Line], [OE Part Number], [A-1 Part Number], [C&K Part Number], Make, Application, Description, AddedByUs) SELECT [Product Line], '" & RTrim(LTrim(Supercession)) & "', [A-1 Part Number], [C&K Part Number], Make, Application, Description, '1' from tblCarDoneCatalog WHERE [OE Part Number] = '" & PartNumber & "'", conn)
                conn.Open()
                sqlComm2.ExecuteNonQuery()
            End Using
        End If
    End Sub
    <WebMethod()>
    Public Function GetAfterPrice(ByVal Emulate As String, ByVal MakeID As String, ByVal Name As String, ByVal Zip As String, ByVal AfterList As AfterParts())

        Dim oemPricing As New OEMSmallPartPricing.Pricing
        Dim js As New JavaScriptSerializer()

        If GetCustomerNo(Name, "CK") = "1040" Or GetCustomerNo(Name, "CK") = "1041" Then Exit Function

        Return js.Serialize(oemPricing.GetAfterPrice(Emulate, MakeID, Name, Nothing, AfterList.ToArray))

    End Function

    Public Shared Function GetOEMFromMake(ByVal makeid As Long) As Long
        Dim intOEM As Long
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT oemid from tblmake WHERE makeid = " & makeid, conn)
            conn.Open()
            intOEM = sqlComm.ExecuteScalar()
        End Using
        Return intOEM
    End Function

    <WebMethod()>
    Public Function SendJSErrorA(ByVal message As String, ByVal url As String, ByVal line As String, ByVal from As String, ByVal website As String)
        Dim mm As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "faiz_khalid@ckautoparts.com")

        mm.Subject = "Part Portal Error Report #1"
        mm.Body = "message: " & message & "<br />url: " & url & "<br />line: " & line & "<br />"
        mm.IsBodyHtml = True

        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
        Catch exc As Exception
            Console.WriteLine(exc)
        End Try

        Return True
    End Function

    <WebMethod()>
    Public Function SendJSErrorB(ByVal ex As String)
        Dim mm As New System.Net.Mail.MailMessage("jason_smith@ckautoparts.com", "jason_smith@ckautoparts.com")

        mm.Subject = "Part Portal Error Report #2"
        mm.Body = ex
        mm.IsBodyHtml = True

        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
        Catch exc As Exception
            Console.WriteLine(exc)
        End Try

        Return True
    End Function

    <WebMethod()>
    Public Function PlaceOrderPlusAfter(ByVal Emulate As String, ByVal Name As String, ByVal MakeID As String, ByVal Model As String, ByVal VIN As String, ByVal Mileage As String, ByVal Year As String, ByVal Drive As String, ByVal Trans As String, ByVal ContractNo As String, ByVal AuthNo As String, ByVal Owner As String, ByVal Shop As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Contact As String, ByVal Phone As String, ByVal Notes As String, ByVal Parts As PigeonPartsResponse(), ByVal Warranty As String, ByVal WarrantyCost As String, ByVal WarrantyDate As String, ByVal WarrantyMileage As String, ByVal ShippingCost As String, ByVal ReturnShippingCost As String, ByVal ShippingType As String)
        Dim strorderid As Long
        Dim strcustno, strmake As String
        Dim r As SqlDataReader
        Dim stradjemail As String = String.Empty
        Dim strcore As Boolean
        Dim strcoreprice As Decimal
        Dim strsavings As Decimal
        Dim strwarrantydate As String
        Dim result As Array
        Dim strshippingcost As Decimal

        Try
            strsavings = 0
            If IsDBNull(WarrantyDate) = True Or WarrantyDate Is Nothing Or WarrantyDate = "" Then
                strwarrantydate = "1/1/1900"
            Else
                strwarrantydate = WarrantyDate
            End If
            strcustno = Emulate

            'first get customer number and email
            If Emulate = "false" Then
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo, dbo.aspnet_Membership.Email FROM dbo.aspnet_Membership INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId WHERE dbo.aspnet_Users.UserName = '" & Name & "'", conn)
                    conn.Open()
                    r = sqlComm.ExecuteReader()
                    While r.Read()
                        strcustno = r("CustomerNo")
                        stradjemail = r("Email")
                    End While
                    r.Close()
                End Using

            Else
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo, dbo.aspnet_Membership.Email FROM dbo.aspnet_Membership INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId WHERE dbo.aspnet_Users.UserName = '" & Name & "'", conn)
                    conn.Open()
                    r = sqlComm.ExecuteReader()
                    While r.Read()
                        stradjemail = r("Email")
                    End While
                    r.Close()
                End Using

            End If
            'get make name
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm3 As New SqlCommand("SELECT make from tblmake WHERE makeid = '" & MakeID & "'", conn)
                conn.Open()
                strmake = sqlComm3.ExecuteScalar()
            End Using


            'insert in order table
            'INSERT INTO tblOrder (DateOrdered, CustomerNo, AdjusterName, AdjusterEmail, ContractNo, AuthorizationNo, AutoOwner, AutoMake, Mileage, VinNo, Notes, AutoModel, EnteredBy, AutoYear, Drive, Transmission) VALUES (@DateOrdered, @CustomerNo, @AdjusterName, @AdjusterEmail, @ContractNo, @AuthorizationNo, @AutoOwner, @AutoMake, @Mileage, @VinNo, @Notes, @AutoModel, @EnteredBy, @AutoYear, @Drive, @Transmission);
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm4 As New SqlCommand("INSERT INTO tblOrder (DateOrdered, CustomerNo, AdjusterName, AdjusterEmail, ContractNo, AuthorizationNo, AutoOwner, AutoMake, Mileage, VinNo, Notes, AutoModel, EnteredBy, AutoYear, Drive, Transmission) VALUES ('" & Now() & "', '" & strcustno & "', '" & Name & "', '" & stradjemail & "', '" & ContractNo & "', '" & AuthNo & "', '" & Owner & "', '" & strmake & "', '" & Mileage & "', '" & VIN & "', '" & "" & "' , '" & Model & "', '" & "Parts Portal" & "', '" & Year & "', '" & Nothing & "', '" & Trans & "');SELECT orderid FROM tblorder WHERE (orderid = SCOPE_IDENTITY())", conn)
                conn.Open()
                strorderid = sqlComm4.ExecuteScalar()
            End Using
            'strorderid = db.usp_InsOrder(Now(), strcustno, Name, stradjemail, ContractNo, AuthNo, Owner, strmake, Mileage, VIN, "", Model, "Parts Portal", Year, Nothing, Trans).ReturnValue

            'insert in parts table
            Dim oem As New List(Of PigeonPartsResponse)
            Dim aftermarket As New List(Of PigeonPartsResponse)
            For Each thepart As PigeonPartsResponse In Parts
                If UCase(thepart.PartType) = UCase("oem") Then
                    Dim o1 As New PigeonPartsResponse
                    o1 = thepart
                    oem.Add(o1)
                Else
                    Dim a1 As New PigeonPartsResponse
                    a1 = thepart
                    aftermarket.Add(a1)
                End If
            Next

            Dim strvendor As String = String.Empty
            Dim strPartNumber As String = String.Empty

            'oem
            Dim oemcount As Integer = 0
            For Each thepart As PigeonPartsResponse In oem

                strsavings = strsavings + (thepart.List - thepart.Your)

                'if customer is interstate,re-ping to get correct core
                'If strcustno = "605" Then
                '    Try
                '        Dim client As PartsLookUpServiceClient = New PartsLookUpServiceClient()
                '        'result = client.GetPartsAvailabilityByPartNumber("CandK", "dM7l@Sn0", Nothing, thepart.PartNumber)
                '        result = client.GetPartsAvailabilityByAlphanumericPartNumber("CandK", "dM7l@Sn0", Nothing, thepart.PartNumber)
                '        client.Close()
                '    Catch Ex As WebException
                '        strcoreprice = 0
                '        strcore = False
                '        GoTo continueoem
                '    End Try

                '    If result.Length > 0 Then
                '        For Each listing As PartsAvailability In result
                '            strcoreprice = listing.CorePrice
                '            If IsDBNull(listing.CorePrice) = False And Val(listing.CorePrice) > 0 Then
                '                strcore = True
                '            Else
                '                strcore = False
                '            End If

                '        Next
                '    End If


                'Else
                '    'not interstate
                '    If IsDBNull(thepart.Core) = False And Val(thepart.Core) > 0 Then
                '        strcore = True
                '        strcoreprice = thepart.Core
                '    Else
                '        strcore = False
                '        strcoreprice = 0
                '    End If


                'End If
continueoem:
                strvendor = "10827"
                strPartNumber = thepart.PartNumber

                'determing shipping
                If aftermarket.Count = 0 Then
                    strshippingcost = ShippingCost
                Else
                    strshippingcost = ShippingCost / 2
                End If
                Try
                    'partordertable
                    If oemcount = 0 Then
                        'Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        '    Dim sqlComm4 As New SqlCommand("insert into tblpartorder(orderid, dateentered, servicer, address1, city, [state], zip, phone, fax, contact,enteredby, partdescription, partno, quantity, sellprice, listprice, shippertrack, custshippingprice, custcoreshippingprice, coreprice, core,warrantycost, warranty, warrantydate, warrantymileage, costprice, vendor, DealerCost) Values ('" & strorderid & "', '" & Now() & "'," & Shop & "', '" & "'," & Address & "', '" & City & "', '" & State & "', '" & Zip & "', '" & Phone & "', '" & "" & "', '" & Contact & "', '" & Name & "', '" & thepart.Description & "', '" & strPartNumber & "', '" & thepart.Quantity & "', '" & FormatNumber(thepart.Your, 2) & "', '" & FormatNumber(thepart.List, 2) & "', '" & Shop & "', '" & strvendor & "', '" & "" & "', '" & "" & "', '" & FormatNumber(strshippingcost, 2) & "', '" & FormatNumber(ReturnShippingCost, 2) & "', '" & FormatNumber(strcoreprice, 2) & "', '" & FormatNumber(WarrantyCost, 2) & "', '" & Warranty & "', '" & strwarrantydate & "', '" & WarrantyMileage & "', '" & FormatNumber(thepart.Our, 2) & "', '" & FormatNumber(strcore, 2) & "', '" & FormatNumber(DealerCost(thepart.Our, MakeID), 2) & ")", conn)
                        '    conn.Open()
                        '    sqlComm4.ExecuteNonQuery()
                        'End Using
                        Using myConnection As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim myCommand As New SqlCommand("usp_InsPart", myConnection)
                            myCommand.CommandType = CommandType.StoredProcedure

                            myCommand.Parameters.AddWithValue("orderid", strorderid)
                            'myCommand.Parameters.AddWithValue("dateentered", Now())
                            myCommand.Parameters.AddWithValue("servicer", Shop)
                            myCommand.Parameters.AddWithValue("address", Address)
                            myCommand.Parameters.AddWithValue("city", City)
                            myCommand.Parameters.AddWithValue("state", State)
                            myCommand.Parameters.AddWithValue("zip", Zip)
                            myCommand.Parameters.AddWithValue("phone", Phone)
                            myCommand.Parameters.AddWithValue("fax", "")
                            myCommand.Parameters.AddWithValue("contact", Contact)
                            myCommand.Parameters.AddWithValue("enteredby", "PartsPortal")
                            myCommand.Parameters.AddWithValue("description", IIf(thepart.Description <> "" Or thepart.Description <> String.Empty, thepart.Description, DBNull.Value))
                            myCommand.Parameters.AddWithValue("partno", strPartNumber)
                            myCommand.Parameters.AddWithValue("quantity", thepart.Quantity)
                            myCommand.Parameters.AddWithValue("sellprice", CDec(thepart.Your))
                            myCommand.Parameters.AddWithValue("listprice", CDec(thepart.List))
                            myCommand.Parameters.AddWithValue("shippertrack", "")
                            myCommand.Parameters.AddWithValue("custship", CDec(strshippingcost))
                            myCommand.Parameters.AddWithValue("custcoreship", CDec(ReturnShippingCost))
                            myCommand.Parameters.AddWithValue("core", strcore)
                            myCommand.Parameters.AddWithValue("coreprice", CDec(strcoreprice))
                            myCommand.Parameters.AddWithValue("warranty", Warranty)
                            myCommand.Parameters.AddWithValue("warrantycost", CDec(WarrantyCost))
                            myCommand.Parameters.AddWithValue("warrantydate", strwarrantydate)
                            myCommand.Parameters.AddWithValue("warrantymileage", WarrantyMileage)
                            myCommand.Parameters.AddWithValue("costprice", CDec(thepart.Our))
                            myCommand.Parameters.AddWithValue("thevendor", strvendor)
                            myCommand.Parameters.AddWithValue("DealerCost", CDec(DealerCost(thepart.Our, MakeID)))
                            myCommand.Parameters.AddWithValue("PartDescGroup", "OEM")
                            myCommand.Parameters.AddWithValue("PartType", "OEM")
                            myCommand.Parameters.AddWithValue("Brand", DBNull.Value)
                            myCommand.Parameters.AddWithValue("ShippingType", IIf(ShippingType = "regular", "overnight", ShippingType))
                            If ShippingType = "regular" Or ShippingType = "freight" Then
                                myCommand.Parameters.AddWithValue("Shipper", 49)
                            ElseIf ShippingType = "ground" Then
                                myCommand.Parameters.AddWithValue("Shipper", 38)
                            End If


                            'Execute the sproc
                            myConnection.Open()
                            myCommand.ExecuteNonQuery()
                            myConnection.Close()
                        End Using

                        'insert into tblpartorder(orderid, dateentered, servicer, address1, city, [state], zip, phone, fax, contact,enteredby, partdescription, partno, quantity, sellprice, listprice, shippertrack, custshippingprice, custcoreshippingprice, coreprice, core,warrantycost, warranty, warrantydate, warrantymileage, costprice, vendor, DealerCost) Values (@orderid, { fn NOW() }, @Servicer, @Address, @City, @State, @Zip, @Phone, @Fax, @Contact, @enteredby, @description, @partno, @quantity, @sellprice, @listprice, @shippertrack, @custship, @custcoreship, @coreprice, @core, @warrantycost, @warranty, @warrantydate, @warrantymileage, @costprice, @TheVendor, @DealerCost)
                        'db.usp_InsPart(strorderid, Address, City, Contact, thepart.Description, strPartNumber, Phone, thepart.Quantity, "Parts Portal", FormatNumber(thepart.Your, 2), FormatNumber(thepart.List, 2), Shop, State, strvendor, Zip, "", "", FormatNumber(strshippingcost, 2), FormatNumber(ReturnShippingCost, 2), FormatNumber(strcoreprice, 2), FormatNumber(WarrantyCost, 2), Warranty, strwarrantydate, WarrantyMileage, FormatNumber(thepart.Our, 2), FormatNumber(strcore, 2), FormatNumber(DealerCost(thepart.Our, MakeID), 2))
                    Else
                        'Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        '    Dim sqlComm4 As New SqlCommand("insert into tblpartorder(orderid, dateentered, servicer, address1, city, [state], zip, phone, fax, contact,enteredby, partdescription, partno, quantity, sellprice, listprice, shippertrack, custshippingprice, custcoreshippingprice, coreprice, core,warrantycost, warranty, warrantydate, warrantymileage, costprice, vendor, DealerCost) Values ('" & strorderid & "', '" & Now() & "'," & Shop & "', '" & Address & "', '" & City & "', '" & State & "', '" & Zip & "', '" & Phone & "', '" & "" & "', '" & Contact & "', '" & Name & "', '" & thepart.Description & "', '" & strPartNumber & "', '" & thepart.Quantity & "', '" & FormatNumber(thepart.Your, 2) & "', '" & FormatNumber(thepart.List, 2) & "', '" & Shop & "', '" & strvendor & "', '" & "" & "', '" & "" & "', '" & 0 & "', '" & 0 & "', '" & FormatNumber(strcoreprice, 2) & "', '" & FormatNumber(0.075 * thepart.Your, 2) & "', '" & Warranty & "', '" & 1 / 1 / 1900 & "', '" & "" & "', '" & FormatNumber(thepart.Our, 2) & "', '" & FormatNumber(strcore, 2) & "', '" & FormatNumber(DealerCost(thepart.Our, MakeID)) & ",'2')", conn)
                        '    conn.Open()
                        '    sqlComm4.ExecuteNonQuery()
                        'End Using
                        Using myConnection As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim myCommand As New SqlCommand("usp_InsPart", myConnection)
                            myCommand.CommandType = CommandType.StoredProcedure


                            myCommand.Parameters.AddWithValue("orderid", strorderid)
                            'myCommand.Parameters.AddWithValue("dateentered", Now())
                            myCommand.Parameters.AddWithValue("servicer", Shop)
                            myCommand.Parameters.AddWithValue("address", Address)
                            myCommand.Parameters.AddWithValue("city", City)
                            myCommand.Parameters.AddWithValue("state", State)
                            myCommand.Parameters.AddWithValue("zip", Zip)
                            myCommand.Parameters.AddWithValue("phone", Phone)
                            myCommand.Parameters.AddWithValue("fax", DBNull.Value)
                            myCommand.Parameters.AddWithValue("contact", Contact)
                            myCommand.Parameters.AddWithValue("enteredby", "PartsPortal")
                            myCommand.Parameters.AddWithValue("description", IIf(thepart.Description <> "" Or thepart.Description <> String.Empty, thepart.Description, DBNull.Value))
                            myCommand.Parameters.AddWithValue("partno", strPartNumber)
                            myCommand.Parameters.AddWithValue("quantity", thepart.Quantity)
                            myCommand.Parameters.AddWithValue("sellprice", CDec(thepart.Your))
                            myCommand.Parameters.AddWithValue("listprice", CDec(thepart.List))
                            myCommand.Parameters.AddWithValue("shippertrack", DBNull.Value)
                            myCommand.Parameters.AddWithValue("custship", CDec("0"))
                            myCommand.Parameters.AddWithValue("custcoreship", CDec("0"))
                            myCommand.Parameters.AddWithValue("core", strcore)
                            myCommand.Parameters.AddWithValue("coreprice", CDec("0"))
                            myCommand.Parameters.AddWithValue("warranty", Warranty)
                            myCommand.Parameters.AddWithValue("warrantycost", CDec("0"))
                            myCommand.Parameters.AddWithValue("warrantydate", "1/1/1900")
                            myCommand.Parameters.AddWithValue("warrantymileage", DBNull.Value)
                            myCommand.Parameters.AddWithValue("costprice", CDec(thepart.Our))
                            myCommand.Parameters.AddWithValue("thevendor", strvendor)
                            myCommand.Parameters.AddWithValue("DealerCost", CDec(DealerCost(thepart.Our, MakeID)))
                            myCommand.Parameters.AddWithValue("PartDescGroup", "OEM")
                            myCommand.Parameters.AddWithValue("PartType", "OEM")
                            myCommand.Parameters.AddWithValue("Brand", DBNull.Value)
                            myCommand.Parameters.AddWithValue("ShippingType", IIf(ShippingType = "regular", "overnight", ShippingType))
                            If ShippingType = "regular" Or ShippingType = "freight" Then
                                myCommand.Parameters.AddWithValue("Shipper", 49)
                            ElseIf ShippingType = "ground" Then
                                myCommand.Parameters.AddWithValue("Shipper", 38)
                            End If

                            'Execute the sproc
                            myConnection.Open()
                            myCommand.ExecuteNonQuery()
                            myConnection.Close()
                        End Using
                        'db.usp_InsPart(strorderid, Address, City, Contact, thepart.Description, strPartNumber, Phone, thepart.Quantity, "Parts Portal", FormatNumber(thepart.Your, 2), FormatNumber(thepart.List, 2), Shop, State, strvendor, Zip, "", "", 0, 0, FormatNumber(strcoreprice, 2), FormatNumber(0.075 * thepart.Your, 2), Warranty, "1/1/1900", "", FormatNumber(thepart.Our, 2), FormatNumber(strcore, 2), FormatNumber(DealerCost(thepart.Our, MakeID), 2))
                    End If
                Catch ex As Exception
                    Console.Write(ex.Message)
                End Try
                oemcount = oemcount + 1
            Next

            'update part type
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm6 As New SqlCommand("update tblpartorder set parttype ='OEM' where orderid = '" & strorderid & "'", conn)
                conn.Open()
                sqlComm6.ExecuteNonQuery()
            End Using




            'aftermarket

            Dim aftermarketcount As Integer = 0
            For Each thepart As PigeonPartsResponse In aftermarket
                thepart.PartNumber = thepart.PartNumber.Replace("-", Nothing)
                If UCase(thepart.PartType) = UCase("ecm") Then
                    strvendor = "13084"
                    strPartNumber = thepart.PartNumber
                    strcoreprice = 0
                    thepart.Our = If(thepart.Your = 40, 19.32, 38.7)
                    thepart.Description = "ECM Programming"

                ElseIf UCase(thepart.PartType) = UCase("aftermarket") Then

                    strvendor = thepart.Provider 'TODO: fix wis connection here IIf(thepart.Provider = 13558, FindMagnetiProvider(thepart.PartNumber, thepart.Quantity, Zip), thepart.Provider)
                    strPartNumber = thepart.PartNumber & "(" & thepart.OEMPartNumber & ")"
                    strcoreprice = thepart.Core
                    strcore = IIf(IsDBNull(thepart.Core) = False And Val(thepart.Core) > 0, True, False)


                End If
                'determing shipping
                If oem.Count = 0 Then
                    strshippingcost = ShippingCost
                Else
                    strshippingcost = ShippingCost / 2
                End If

                'switch warranty if manufacturer selected
                If Warranty = "Manufacturer" Then
                    Warranty = "12/12"
                End If
                Dim intPartID As Long
                If aftermarketcount = 0 Then
                    'Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    '    Dim sqlComm4 As New SqlCommand("insert into tblpartorder(orderid, dateentered, servicer, address1, city, [state], zip, phone, fax, contact,enteredby, partdescription, partno, quantity, sellprice, listprice, shippertrack, custshippingprice, custcoreshippingprice, coreprice, core,warrantycost, warranty, warrantydate, warrantymileage, costprice, vendor, DealerCost) Values ('" & strorderid & "', '" & Address & "', '" & City & "', '" & Contact & "', '" & thepart.Description & "', '" & strPartNumber & "', '" & Phone & "', '" & thepart.Quantity & "', '" & "Parts Portal" & "', '" & FormatNumber(thepart.Your, 2) & "', '" & FormatNumber(thepart.List, 2) & "', '" & Shop & "', '" & State & "', '" & strvendor & "', '" & Zip & "', '" & "" & "', '" & "" & "', '" & FormatNumber(strshippingcost, 2) & "', '" & FormatNumber(ReturnShippingCost, 2) & "', '" & FormatNumber(strcoreprice, 2) & "', '" & FormatNumber(WarrantyCost, 2) & "', '" & Warranty & "', '" & strwarrantydate & "', '" & WarrantyMileage & "', '" & FormatNumber(thepart.Our, 2) & "', '" & FormatNumber(strcore, 2) & "', '" & FormatNumber(DealerCost(thepart.Our, MakeID), 2) & "')", conn)
                    '    conn.Open()
                    '    sqlComm4.ExecuteNonQuery()
                    'End Using


                    Using myConnection As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim myCommand As New SqlCommand("usp_InsPart", myConnection)
                        myCommand.CommandType = CommandType.StoredProcedure

                        myCommand.Parameters.AddWithValue("orderid", strorderid)
                        'myCommand.Parameters.AddWithValue("dateentered", Now())
                        myCommand.Parameters.AddWithValue("servicer", Shop)
                        myCommand.Parameters.AddWithValue("address", Address)
                        myCommand.Parameters.AddWithValue("city", City)
                        myCommand.Parameters.AddWithValue("state", State)
                        myCommand.Parameters.AddWithValue("zip", Zip)
                        myCommand.Parameters.AddWithValue("phone", Phone)
                        myCommand.Parameters.AddWithValue("fax", "")
                        myCommand.Parameters.AddWithValue("contact", Contact)
                        myCommand.Parameters.AddWithValue("enteredby", "PartsPortal")
                        myCommand.Parameters.AddWithValue("description", IIf(thepart.Description <> "" Or thepart.Description <> String.Empty, thepart.Description, DBNull.Value))
                        myCommand.Parameters.AddWithValue("partno", strPartNumber)
                        myCommand.Parameters.AddWithValue("quantity", thepart.Quantity)
                        myCommand.Parameters.AddWithValue("sellprice", CDec(thepart.Your))
                        myCommand.Parameters.AddWithValue("listprice", CDec(thepart.List))
                        myCommand.Parameters.AddWithValue("shippertrack", "")
                        myCommand.Parameters.AddWithValue("custship", CDec(strshippingcost))
                        myCommand.Parameters.AddWithValue("custcoreship", CDec(ReturnShippingCost))
                        myCommand.Parameters.AddWithValue("core", strcore)
                        myCommand.Parameters.AddWithValue("coreprice", CDec(strcoreprice))
                        myCommand.Parameters.AddWithValue("warranty", Warranty)
                        myCommand.Parameters.AddWithValue("warrantycost", CDec("0"))
                        myCommand.Parameters.AddWithValue("warrantydate", strwarrantydate)
                        myCommand.Parameters.AddWithValue("warrantymileage", WarrantyMileage)
                        myCommand.Parameters.AddWithValue("costprice", CDec(thepart.Our))
                        myCommand.Parameters.AddWithValue("thevendor", strvendor)
                        myCommand.Parameters.AddWithValue("DealerCost", CDec(DealerCost(thepart.Our, MakeID)))
                        myCommand.Parameters.AddWithValue("PartDescGroup", "Small Parts")
                        myCommand.Parameters.AddWithValue("PartType", "Small Parts")
                        myCommand.Parameters.AddWithValue("Brand", thepart.Brand)
                        myCommand.Parameters.AddWithValue("ShippingType", IIf(ShippingType = "regular", "overnight", ShippingType))
                        If ShippingType = "regular" Or ShippingType = "freight" Then
                            myCommand.Parameters.AddWithValue("Shipper", 49)
                        ElseIf ShippingType = "ground" Then
                            myCommand.Parameters.AddWithValue("Shipper", 38)
                        End If

                        'Execute the sproc
                        myConnection.Open()
                        intPartID = myCommand.ExecuteScalar()
                        myConnection.Close()
                    End Using

                    'db.usp_InsPart(strorderid, Address, City, Contact, thepart.Description, strPartNumber, Phone, thepart.Quantity, "Parts Portal", FormatNumber(thepart.Your, 2), FormatNumber(thepart.List, 2), Shop, State, strvendor, Zip, "", "", FormatNumber(strshippingcost, 2), 0, FormatNumber(strcoreprice, 2), 0, Warranty, strwarrantydate, WarrantyMileage, FormatNumber(thepart.Our, 2), FormatNumber(strcore, 2), FormatNumber(DealerCost(thepart.Our, MakeID), 2))
                Else
                    'Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    '    Dim sqlComm4 As New SqlCommand("insert into tblpartorder(orderid, dateentered, servicer, address1, city, [state], zip, phone, fax, contact,enteredby, partdescription, partno, quantity, sellprice, listprice, shippertrack, custshippingprice, custcoreshippingprice, coreprice, core,warrantycost, warranty, warrantydate, warrantymileage, costprice, vendor, DealerCost) Values ('" & strorderid & "', '" & Address & "', '" & City & "', '" & Contact & "', '" & thepart.Description & "', '" & strPartNumber & "', '" & Phone & "', '" & thepart.Quantity & "', '" & "Parts Portal" & "', '" & FormatNumber(thepart.Your, 2) & "', '" & FormatNumber(thepart.List, 2) & "', '" & Shop & "', '" & State & "', '" & strvendor & "', '" & Zip & "', '" & "" & "', '" & "" & "', '" & 0 & "', '" & 0 & "', '" & FormatNumber(strcoreprice, 2) & "', '" & FormatNumber(0.075 * thepart.Your, 2) & "', '" & Warranty & "', '" & 1 / 1 / 1900 & "', '" & "" & "', '" & FormatNumber(thepart.Our, 2) & "', '" & FormatNumber(strcore, 2) & "', '" & FormatNumber(DealerCost(thepart.Our, MakeID)) & ",'2')", conn)
                    '    conn.Open()
                    '    sqlComm4.ExecuteNonQuery()
                    'End Using

                    Using myConnection As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim myCommand As New SqlCommand("usp_InsPart", myConnection)
                        myCommand.CommandType = CommandType.StoredProcedure


                        myCommand.Parameters.AddWithValue("orderid", strorderid)
                        'myCommand.Parameters.AddWithValue("dateentered", Now())
                        myCommand.Parameters.AddWithValue("servicer", Shop)
                        myCommand.Parameters.AddWithValue("address", Address)
                        myCommand.Parameters.AddWithValue("city", City)
                        myCommand.Parameters.AddWithValue("state", State)
                        myCommand.Parameters.AddWithValue("zip", Zip)
                        myCommand.Parameters.AddWithValue("phone", Phone)
                        myCommand.Parameters.AddWithValue("fax", "")
                        myCommand.Parameters.AddWithValue("contact", Contact)
                        myCommand.Parameters.AddWithValue("enteredby", "PartsPortal")
                        myCommand.Parameters.AddWithValue("description", IIf(thepart.Description <> "" Or thepart.Description <> String.Empty, thepart.Description, DBNull.Value))
                        myCommand.Parameters.AddWithValue("partno", strPartNumber)
                        myCommand.Parameters.AddWithValue("quantity", thepart.Quantity)
                        myCommand.Parameters.AddWithValue("sellprice", CDec(thepart.Your))
                        myCommand.Parameters.AddWithValue("listprice", CDec(thepart.List))
                        myCommand.Parameters.AddWithValue("shippertrack", "")
                        myCommand.Parameters.AddWithValue("custship", CDec("0"))
                        myCommand.Parameters.AddWithValue("custcoreship", CDec("0"))
                        myCommand.Parameters.AddWithValue("core", strcore)
                        myCommand.Parameters.AddWithValue("coreprice", CDec("0"))
                        myCommand.Parameters.AddWithValue("warranty", Warranty)
                        myCommand.Parameters.AddWithValue("warrantycost", CDec("0"))
                        myCommand.Parameters.AddWithValue("warrantydate", "1/1/1900")
                        myCommand.Parameters.AddWithValue("warrantymileage", "")
                        myCommand.Parameters.AddWithValue("costprice", CDec(thepart.Our))
                        myCommand.Parameters.AddWithValue("thevendor", strvendor)
                        myCommand.Parameters.AddWithValue("DealerCost", CDec(DealerCost(thepart.Our, MakeID)))
                        myCommand.Parameters.AddWithValue("PartDescGroup", "Small Parts")
                        myCommand.Parameters.AddWithValue("PartType", "Small Parts")
                        myCommand.Parameters.AddWithValue("Brand", thepart.Brand)
                        myCommand.Parameters.AddWithValue("ShippingType", IIf(ShippingType = "regular", "overnight", ShippingType))
                        If ShippingType = "regular" Or ShippingType = "freight" Then
                            myCommand.Parameters.AddWithValue("Shipper", 49)
                        ElseIf ShippingType = "ground" Then
                            myCommand.Parameters.AddWithValue("Shipper", 38)
                        End If


                        'Execute the sproc
                        myConnection.Open()
                        intPartID = myCommand.ExecuteScalar()
                        myConnection.Close()
                    End Using

                    'db.usp_InsPart(strorderid, Address, City, Contact, thepart.Description, strPartNumber, Phone, thepart.Quantity, "Parts Portal", FormatNumber(thepart.Your, 2), FormatNumber(thepart.List, 2), Shop, State, strvendor, Zip, "", "", 0, 0, FormatNumber(strcoreprice, 2), 0, Warranty, "1/1/1900", "", FormatNumber(thepart.Our, 2), FormatNumber(strcore, 2), FormatNumber(DealerCost(thepart.Our, MakeID), 2))
                End If

                'update tblSmallPartOptions

                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("update tblsmallpartoptions set partid = '" & intPartID & "' where quoteid=" & thepart.QuoteID, conn)
                    conn.Open()
                    sqlComm.ExecuteNonQuery()
                End Using



                aftermarketcount = aftermarketcount + 1
            Next


            'update part type
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm6 As New SqlCommand("update tblpartorder set parttype ='Small Parts' where orderid = '" & strorderid & "' and parttype is null", conn)
                conn.Open()
                sqlComm6.ExecuteNonQuery()
            End Using

            'insert note if any
            InsertNote(strorderid, Notes.Replace("'", ""), False, Name)

            If strorderid > 0 Then
                EmailAdjuster(strcustno, stradjemail, AuthNo, ContractNo, strorderid)
                Return strorderid '& "/" & GetSavings(strsavings)
            Else
                Return False
            End If
            Exit Function
        Catch Ex As WebException

            Return False
            Exit Function
        End Try
    End Function
    Private Function FindMagnetiProvider(ByVal part As String, ByVal quantity As String, ByVal zip As String)

        Dim lst As New List(Of MagnetiDealer)
        Dim strLat As String = String.Empty
        Dim strLong As String = String.Empty

        'get lat, long of delivery
        Using connZip As New SqlConnection(wisConnection)
            Dim sqlComm2 As New SqlCommand("Select Lat, Long From tblZipCodes2 Where ZipCode = '" & zip & "'", connZip)
            connZip.Open()
            Using rZip As SqlDataReader = sqlComm2.ExecuteReader
                While rZip.Read()
                    strLat = rZip("lat").ToString
                    strLong = rZip("long").ToString
                End While
            End Using
        End Using


        'get list of magneti dealers thta have part
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm3 As New SqlCommand("select stock, tblmagnetistock.companyid, lat, long from tblMagnetiCatalog left outer join tblMagnetiStock on tblMagnetiCatalog.part=tblMagnetiStock.Part inner join tblCompany on tblCompany.companyID=tblMagnetiStock.Companyid inner join InspectionJournal.dbo.tblZipCodes2 z on z.zipcode=tblCompany.zip where tblmagneticatalog.part='" & part & "' and stock >='" & quantity & "' group by tblmagnetistock.companyid, stock, lat, long", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm3.ExecuteReader
                While r.Read()
                    Dim m1 As New MagnetiDealer
                    m1.CompanyID = r("companyid").ToString
                    m1.Latitude = r("lat").ToString
                    m1.Longitude = r("long").ToString
                    lst.Add(m1)

                End While
            End Using
        End Using

        Dim intClosest As Integer = 0
        Dim intDistance As Integer = 99999

        'loop through each dealer and get distance
        For Each dealer As MagnetiDealer In lst
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlcomm As New SqlCommand("SELECT InspectionJournal.dbo.LatLonRadiusDistance(" & dealer.Latitude & "," & dealer.Longitude & "," & strLat & "," & strLong & ") as miles", conn)
                conn.Open()
                Dim tmpDistance As Integer = sqlcomm.ExecuteScalar
                If tmpDistance < intDistance Then
                    intClosest = dealer.CompanyID
                    intDistance = tmpDistance
                End If

            End Using
        Next

        Return intClosest
    End Function

    Private Sub EmailAdjuster(ByVal CustNo As String, ByVal Email As String, ByVal AuthNo As String, ByVal ContractNo As String, orderid As Long)
        Select Case CustNo
            Case "50"
                AddToAutoEmail("CK", Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString(), 1, Email, "sales@ckautoparts.com", orderid,,, "pfincher@mercuryinsurance.com")
            Case "110"

                AddToAutoEmail("CK", Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString(), 1, Email, "sales@ckautoparts.com", orderid,,, "newagwsorder@ckautoparts.com")
            Case Else
                AddToAutoEmail("CK", Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString(), 1, Email, "sales@ckautoparts.com", orderid,,,)
        End Select

    End Sub

    Public Function DealerCost(ByVal cost As String, ByVal makeid As String)

        Dim stroemid As Long



        'get oemid
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm3 As New SqlCommand("SELECT dbo.tblMake.OEMID FROM dbo.tblMake WHERE  makeid = '" & makeid & "'", conn)
            conn.Open()
            stroemid = sqlComm3.ExecuteScalar()
        End Using




        'get current markup
        Dim strmarkup As Decimal
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm5 As New SqlCommand("SELECT markup from tbloemdealer WHERE oemid = " & stroemid & " and companyid = 10827", conn)
            conn.Open()
            strmarkup = sqlComm5.ExecuteScalar()
        End Using





        'get new cost
        Dim strdealer = cost / (1 + strmarkup)



        Return Double.Parse(strdealer)
        'Return GetOEMPrice("456", "false", strmakeid, "cust", TheParts.ToArray)


    End Function

End Class