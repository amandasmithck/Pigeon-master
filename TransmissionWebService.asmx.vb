Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon
Imports Pigeon.CertifiedLookup
Imports System.Math
Imports Pigeon.Enums
Imports System.Linq

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class TransmissionWebService
    Inherits System.Web.Services.WebService

    Public Class TheData
        Public Property thevalue As String
            Get
                Return m_thevalue
            End Get
            Set(ByVal value As String)
                m_thevalue = value
            End Set
        End Property
        Private m_thevalue As String
    End Class



    Public Class Quote
        Public Property CustomerContactEmail As String

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
        Public Property Year() As String
            Get
                Return m_Year
            End Get
            Set(ByVal value As String)
                m_Year = value
            End Set
        End Property
        Private m_Year As String
        Public Property Make() As String
            Get
                Return m_Make
            End Get
            Set(ByVal value As String)
                m_Make = value
            End Set
        End Property
        Private m_Make As String
        Public Property Model() As String
            Get
                Return m_Model
            End Get
            Set(ByVal value As String)
                m_Model = value
            End Set
        End Property
        Private m_Model As String
        Public Property Engine() As String
            Get
                Return m_Engine
            End Get
            Set(ByVal value As String)
                m_Engine = value
            End Set
        End Property
        Private m_Engine As String
        Public Property Transmission() As String
            Get
                Return m_Transmission
            End Get
            Set(ByVal value As String)
                m_Transmission = value
            End Set
        End Property
        Private m_Transmission As String

        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property Cost() As Decimal
            Get
                Return m_Cost
            End Get
            Set(ByVal value As Decimal)
                m_Cost = value
            End Set
        End Property
        Private m_Cost As Decimal
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
        Public Property SellPrice() As Decimal
            Get
                Return m_Sellprice
            End Get
            Set(ByVal value As Decimal)
                m_Sellprice = value
            End Set
        End Property
        Private m_Sellprice As Decimal
        Public Property LocalStock() As String
            Get
                Return m_LocalStock
            End Get
            Set(ByVal value As String)
                m_LocalStock = value
            End Set
        End Property
        Private m_LocalStock As String
    End Class

    <WebMethod()>
    Public Function GetData(ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal transmission As String, ByVal name As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of TheData)
        Dim list2 As New List(Of TheData)
        Dim lstMakes As List(Of ExcludeMakes)

        Dim response = New ArrayList
        Dim searchon As String

        engine = engine.Replace(" / /", " /  / ")

        'If Tagid <> "null" Then
        '    searchon = "TagID"
        '    GoTo search
        'End If

        If transmission <> "null" Then
            searchon = "Transmission"
            GoTo search
        End If

        If engine <> "null" Then
            searchon = "engine"
            GoTo search
        End If

        If model <> "null" Then
            searchon = "model"
            GoTo search
        End If

        If make <> "null" Then
            searchon = "make"
            GoTo search
        End If

        If year <> "null" Then
            searchon = "year"
            GoTo search
        End If

        searchon = "initial"
search:
        Select Case searchon
            Case "initial"


                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT year from tblCertifiedCatalog inner join tblcertifiedpricing on tblCertifiedCatalog.partnumber=tblcertifiedpricing.part where leveltwo > 0 union SELECT year from tblZFCatalog inner join tblzfpricing on tblzfCatalog.partnumber=tblzfpricing.partno group by year order by year desc", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("year")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function
            Case "year"

                If client <> "CK" Then
                    'get make exclusions
                    lstMakes = GetMakeExclusions(client, GetCustomerNo(name, client))
                End If

                'get years
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT make from tblCertifiedCatalog inner join tblcertifiedpricing on tblCertifiedCatalog.partnumber=tblcertifiedpricing.part where leveltwo > 0 and  year = '" & year & "' union SELECT make from tblZFCatalog inner join tblzfpricing on tblzfCatalog.partnumber=tblzfpricing.partno where year = '" & year & "' group by make order by make", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            'see if this make is excluded
                            Dim e1 As New TheData
                            If client = "CK" Then
                                e1.thevalue = r("make")
                                list.Add(e1)
                            Else
                                If lstMakes.Any(Function(x) x.Make.ToUpper() = UCase(r("make"))) = False Then
                                    e1.thevalue = r("make")
                                    list.Add(e1)
                                End If
                            End If
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function

            Case "make"

                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT model from tblCertifiedCatalog inner join tblcertifiedpricing on tblCertifiedCatalog.partnumber=tblcertifiedpricing.part where leveltwo > 0 and year = '" & year & "' and make = '" & make & "' union SELECT model from tblZFCatalog inner join tblzfpricing on tblzfCatalog.partnumber=tblzfpricing.partno where year = '" & year & "' and make = '" & make & "' group by model order by model", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("model")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function

            Case "model"


                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT Engine from tblCertifiedCatalog inner join tblcertifiedpricing on tblCertifiedCatalog.partnumber=tblcertifiedpricing.part where leveltwo > 0 and year = '" & year & "' and make = '" & make & "' and model='" & model & "' union SELECT engine from tblZFCatalog inner join tblzfpricing on tblzfCatalog.partnumber=tblzfpricing.partno where year = '" & year & "' and make = '" & make & "' and model='" & model & "' group by engine order by engine", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("engine")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function

            Case "engine"

                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT Transmission from tblCertifiedCatalog inner join tblcertifiedpricing on tblCertifiedCatalog.partnumber=tblcertifiedpricing.part where leveltwo > 0 and year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' union SELECT transmission from tblZFCatalog inner join tblzfpricing on tblzfCatalog.partnumber=tblzfpricing.partno where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' group by transmission order by transmission", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("transmission")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function

            Case "Transmission"
                'Dim sqlComm As New SqlCommand("SELECT TagID from tblCertifiedCatalog where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "' group by Tagid order by Tagid", conn)
                ' conn.Open()
                ' Dim r = sqlComm.ExecuteReader()
                ' While r.Read()
                '     Dim e1 As New TheData
                '     e1.thevalue = r("tagid")
                '     list.Add(e1)
                ' End While
                ' end using
                ' end using

                ' Return js.Serialize(list)
                Return True
                Exit Function
                'Case "tagid"
                '    Return True

        End Select
    End Function
    Public Shared Function GetTransmissionPrice(ByVal partno As String, ByVal name As String, ByVal client As String, ByVal tierID As String)
        'Dim strPartNumber As String
        Dim p1 As New Pricing
        Dim decCKCost, decCKSell, decCore As Decimal


        GetDefaults(client)

        'only admin for certain pigeon types will see part number
        If clienttype = "CK" Or clienttype = "PremierWholesaler" Or client = "LarryMiller" Then 'TODO: remove the hackish reference to LarryMiller once we have a better system of Tiers, and ClientType
            If GetUserRole(name, client) = "Admin" Then
                p1.showpartno = True
            Else
                p1.showpartno = False
            End If
        Else
            p1.showpartno = False
        End If

        p1.subtype = "Reman"
        p1.installtotal = 0
        p1.core = 0
        p1.stock = 0
        p1.partno = partno



        'see what vendor has this part
        Dim boolFound As Boolean
        'try certified first
        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT count(*) from tblcertifiedcatalog where partnumber ='" & partno & "'", conn2)
            conn2.Open()
            Dim intCount As Integer = CInt(sqlComm.ExecuteScalar())
            If intCount > 0 Then
                boolFound = True
                p1.vendor = 78
            End If
        End Using
        'try zf next
        If boolFound = False Then
            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT count(*) from tblzfcatalog where partnumber ='" & partno & "'", conn2)
                conn2.Open()
                Dim intCount As Integer = CInt(sqlComm.ExecuteScalar())
                If intCount > 0 Then
                    boolFound = True
                    p1.vendor = 13095
                End If
            End Using
        End If


        'get initial needed info
        If p1.vendor = 78 Then 'certified
            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommPrice As New SqlCommand("SELECT core  from tblcertifiedpricing  where tblCertifiedPricing.part ='" & partno & "'", conn2)
                conn2.Open()
                Using r As SqlDataReader = sqlCommPrice.ExecuteReader()
                    While r.Read()
                        decCore = CDec(r("core"))
                    End While
                End Using
            End Using

        ElseIf p1.vendor = 13095 Then 'zf
            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT core from tblzfpricing where partno ='" & partno & "'", conn2)
                conn2.Open()
                decCore = CDec(sqlComm.ExecuteScalar())
            End Using
        End If

        'get initial full ck cost
        decCKCost = GetCKCatalogCost(p1.partno, PartTypes.Transmission, p1.vendor)

        'check see if part has an override
        Dim override As New OverridePart
        override = VendorOverride(p1.partno, PartTypes.Transmission, p1.vendor)
        If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" Then
            If override.Override = True Then
                p1.vendor = override.OverridePartVendor
                p1.partno = override.OverridePartNo
                decCKCost = GetCKCatalogCost(p1.partno, PartTypes.Transmission, p1.vendor)
            End If
        End If

        'get warranties
        p1.warranties = GetWarranty(PartTypes.Transmission, name, client)
        'get our all important base Tier(2) to mark on top of this
        decCKSell = GetCKSell(decCKCost, PartTypes.Transmission)
        'see if pretty, if so set sell to this new price
        Dim decPrettyPrice As Integer
        decPrettyPrice = GetPrettyPrice(p1.partno, PartTypes.Transmission, p1.vendor)
        If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" And decPrettyPrice > 0 Then
            p1.tiers = GetTiers(1, name, client, decPrettyPrice, "pretty")
        Else
            p1.tiers = GetTiers(1, name, client, decCKSell, "sell")
        End If
        'see if price override, if so set sell to this new price
        Dim decOverridePrice As Integer
        decOverridePrice = GetOverridePrice(partno, PartTypes.Transmission, p1.vendor, name, client)
        If decOverridePrice > 0 And client <> "CK" Then
            p1.tiers = GetTiers(PartTypes.Transmission, name, client, decOverridePrice, "pretty")
        ElseIf decOverridePrice > 0 And client = "CK" Then
            'override tier price for that user
            Try

                If String.IsNullOrEmpty(tierID) = False Then
                    Dim overrideTier = p1.tiers.Where(Function(t) t.TierID = tierID).FirstOrDefault()
                    overrideTier.Price = decOverridePrice
                Else
                    Dim overrideTier = p1.tiers.Where(Function(t) t.TierID = GetUserTier(name, client)).FirstOrDefault()
                    overrideTier.Price = decOverridePrice
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End If

        'check for any pigeon pretty prices(pretty prices for the pigeon's tier not the pigeon's true cost)
        If client <> "CK" Then
            For Each tier As VisibleTier In p1.tiers
                If tier.TierID <> 1 And tier.TierID <> 2 Then
                    decPrettyPrice = GetPigeonPrettyPrice(p1.partno, PartTypes.Transmission, p1.vendor, tier.TierID, client)
                    If decPrettyPrice > 0 Then tier.Price = decPrettyPrice
                End If
            Next
        End If


        'for c&k- sell prices already determined so revert Tier 2 back to true part cost
        If client = "CK" Then
            Dim tierTwo = p1.tiers.Where(Function(x) x.TierID = 2).FirstOrDefault()
            If IsNothing(tierTwo) = False Then tierTwo.Price = decCKCost
        End If

        'final rounding for non C&K 
        If client <> "CK" Then
            For Each tier As VisibleTier In p1.tiers
                If tier.TierID <> 2 Then
                    tier.Price = Math.Round(tier.Price)
                End If
            Next
        End If

        clienttransmissionstock = IIf(client = "AutoNation", GetDistributor(name), clienttransmissionstock).ToString
        If String.IsNullOrEmpty(clienttransmissionstock) Then
            p1.localstock = 0
        Else
            p1.localstock = GetLocalStock(partno, PartTypes.Transmission, clienttransmissionstock)
        End If



        'get Maps
        Dim objClosestWarehouse As New ClosestWarehouse
        Dim objAltClosestWarehouse As New ClosestWarehouse
        Dim strWarehouseClient As String
        Dim strAltWarehouseClient As String

        If p1.localstock > 0 Then 'locally available,check others as well and get delivery map data
            p1.maps = GetMapData(p1.partno, PartTypes.Transmission, clienttransmissionstock, True, IIf(client = "AutoNation", False, True), IIf(client = "AutoNation", False, True), p1.vendor)
            If client = "AutoNation" Then
                'set local distributor as closet warehouse
                objClosestWarehouse = GetDistributorWarehouse(clienttransmissionstock, name)
            End If
        Else 'not available locally check others
            If client = "AutoNation" And (GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "CO" Or GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "WA") Then
                'exclude tracy warehouses

                p1.maps = GetMapData(p1.partno, PartTypes.Transmission, clienttransmissionstock, False, True, True, p1.vendor, , True)
            Else
                p1.maps = GetMapData(p1.partno, PartTypes.Transmission, clienttransmissionstock, False, True, True, p1.vendor)
            End If


            If client = "AutoNation" Then
                'back out any markup for the dealership tier
                Dim decNewPrice As Decimal = GetClientCost(p1.partno, PartTypes.Transmission, p1.vendor, "AutoNation")

                objClosestWarehouse = FindClosestWarehouse(p1.maps, GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"))

                strWarehouseClient = GetWarehouseClient(objClosestWarehouse.WarehouseID).ToString
                If GetWarehouseAutonation(objClosestWarehouse.WarehouseID) = False Then 'Non Autonation is the closes
                    decNewPrice = IIf(strWarehouseClient = "Tracy", decNewPrice + 225, decNewPrice + 90 + 90)
                    For Each searchedtier As VisibleTier In p1.tiers
                        If searchedtier.TierID = 3 Or searchedtier.TierID = 42 Then
                            searchedtier.Price = decNewPrice
                            Exit For
                        End If
                    Next
                Else
                    'AutoNation distributor is closest

                    For Each searchedtier As VisibleTier In p1.tiers
                        If searchedtier.TierID = 3 Or searchedtier.TierID = 42 Then
                            searchedtier.Price = decNewPrice + 50 + 90 + 90
                            p1.AutoNationDeliveryPrice = decNewPrice + 50 + 90 + 90
                            Exit For
                        End If
                    Next
                    'Find alternative closest non-autonation
                    Dim objNonAutoNationMaps = GetMapData(partno, PartTypes.Transmission, clienttransmissionstock, False, True, True, p1.vendor, , IIf(GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "CO" Or GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "WA", True, False), True)
                    objAltClosestWarehouse = FindClosestWarehouse(objNonAutoNationMaps, GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"))
                    strAltWarehouseClient = GetWarehouseClient(objAltClosestWarehouse.WarehouseID).ToString
                    p1.AutoNationAltDeliveryFrom = UCase(objAltClosestWarehouse.State)
                    p1.AutoNationAltDelivery = objAltClosestWarehouse.Schedule
                    p1.AutoNationAltDeliveryPrice = IIf(strAltWarehouseClient = "Tracy", decNewPrice + 225, decNewPrice + 90 + 90)
                    If p1.AutoNationAltDeliveryPrice > p1.AutoNationDeliveryPrice Then 'alternate turned out to not be cheaper so dont display
                        p1.AutoNationAltDeliveryFrom = "N/A"
                        p1.AutoNationAltDelivery = 0
                        p1.AutoNationAltDeliveryPrice = 0
                    End If
                    If p1.AutoNationAltDeliveryPrice < p1.AutoNationDeliveryPrice And p1.AutoNationAltDelivery < p1.AutoNationDelivery Then 'alertnate turned out to be cheaper and faster so only display that
                        p1.AutoNationDelivery = p1.AutoNationAltDelivery
                        p1.AutoNationDeliveryFrom = p1.AutoNationAltDeliveryFrom
                        p1.AutoNationDeliveryPrice = p1.AutoNationAltDeliveryPrice

                        p1.AutoNationAltDeliveryFrom = "N/A"
                        p1.AutoNationAltDelivery = 0
                        p1.AutoNationAltDeliveryPrice = 0
                    End If

                End If
            Else
                ''***************VisibleTier price value plus hot build here 
                For Each tier In p1.tiers
                    Dim hotbuild
                    Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm2 As New SqlCommand("SELECT HotBuild FROM tblTierBasePrice WHERE TierID = '" & tier.TierID & "' AND PartType = 1", conn2)
                        conn2.Open()
                        hotbuild = sqlComm2.ExecuteScalar()
                    End Using

                    tier.Price = FormatNumber(tier.Price + hotbuild, 2)
                Next
            End If

        End If
        p1.AutoNationDeliveryState = UCase(IIf(client = "AutoNation", GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"), "N/A"))
        p1.AutoNationDeliveryFrom = UCase(IIf(client = "AutoNation", objClosestWarehouse.State, "N/A"))
        p1.AutoNationDelivery = IIf(client = "AutoNation", objClosestWarehouse.Schedule, 0)
        p1.AutoNation = IIf(client = "AutoNation", True, False)
        p1.tiers.Sort(Function(v1 As VisibleTier, v2 As VisibleTier) v2.Price.CompareTo(v1.Price))
        'get core
        p1.core = decCore


        Return p1
    End Function
    <WebMethod()>
    Public Function InsertNewQuote(ByVal customerNo As String, ByVal customerEmail As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal transmission As String, ByVal name As String, ByVal client As String, ByVal partNo As String, ByVal tierID As String)
        Dim js As New JavaScriptSerializer
        Try
            'ProcessQuote(customerNo, customerEmail, enteredBy, year, make, model, engine, Nothing, Nothing, client, partNo)
            engine = engine.Replace(" / /", " /  / ")

            If GetCustomerNo(name, client) = "1040" Or GetCustomerNo(name, client) = "1041" Then Exit Function
            Dim intLocalStock As Integer
            GetDefaults(client)
            Dim responseOuter = New ArrayList
            Dim responseBuilder = New ArrayList
            Dim responseReman = New ArrayList
            Dim listOfPrices As New List(Of Pricing)

            'Reman Search
            responseReman = getRemanResponse(year, make, model, engine, transmission, name, client, customerEmail, tierID)
            If responseReman.Count > 0 Then
                responseOuter.AddRange(responseReman)
            End If
            'Builder Search
            responseBuilder = getBuilderResponse(year, make, model, engine, transmission, name, client, customerEmail, tierID)
            Try
                If responseBuilder.Count > 0 Then
                    responseOuter.AddRange(responseBuilder)
                End If

            Catch ex As Exception

            End Try

            For i As Integer = 0 To responseOuter(0).Count
                If i <> responseOuter(0).Count Then
                    listOfPrices.Add(CType(responseOuter(0)(i), Pricing))
                End If
            Next


            Dim CurrentPartInfo As Pricing = listOfPrices.Where(Function(a) a.partno = partNo).FirstOrDefault()
            Dim decSellPrice As Decimal
            For Each searchedtier As VisibleTier In CurrentPartInfo.tiers
                If searchedtier.TierID = If(GetUserRole(name, client) = "Admin", GetUserTierByEmail(customerEmail, client), GetUserTier(name, client)) Then
                    decSellPrice = searchedtier.Price
                    Exit For
                End If
            Next
            'quote
            Dim add1 As New AdditionalInfo

            add1.quoteID = ProcessQuote(customerNo, customerEmail, name, year, make, model, engine, Nothing, Nothing, client, partNo, intLocalStock, decSellPrice, CurrentPartInfo, PartTypes.Transmission, transmission, Nothing)
            add1.partNo = partNo
            Return js.Serialize(add1)
        Catch ex As Exception
            Return js.Serialize(ex.Message & " " & ex.StackTrace)
        End Try

    End Function
    <WebMethod()>
    Public Function GetPrice(ByVal customerNo As String, ByVal customerEmail As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal transmission As String, ByVal name As String, ByVal client As String, ByVal customerClient As String, ByVal tierID As String)
        Dim js As New JavaScriptSerializer()
        Try
            engine = engine.Replace(" / /", " /  / ")

            If GetCustomerNo(name, client) = "1040" Or GetCustomerNo(name, client) = "1041" Then Exit Function
            Dim intLocalStock As Integer
            GetDefaults(client)
            Dim responseOuter = New ArrayList
            Dim responseBuilder = New ArrayList
            Dim responseReman = New ArrayList
            Dim p1 As New Pricing
            'Reman Search
            responseReman = IIf(customerClient <> client, getRemanResponse(year, make, model, engine, transmission, GetUserNameByEmail(customerEmail, customerClient), customerClient, customerEmail, tierID), getRemanResponse(year, make, model, engine, transmission, name, client, customerEmail, tierID))
            If responseReman.Count > 0 Then
                responseOuter.AddRange(responseReman)
            End If
            'Builder Search
            responseBuilder = IIf(customerClient <> client, getBuilderResponse(year, make, model, engine, transmission, GetUserNameByEmail(customerEmail, customerClient), customerClient, customerEmail, tierID), getBuilderResponse(year, make, model, engine, transmission, name, client, customerEmail, tierID))
            Try
                If responseBuilder(0).Count > 0 Then
                    responseOuter.AddRange(responseBuilder)
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try

            Dim decSellPrice As Decimal
            Try

                For counter As Integer = 0 To responseReman.Count
                    Dim priceInfo As New Pricing
                    priceInfo = CType(responseOuter(0)(counter), Pricing)


                    Dim adminTiers As New List(Of VisibleTier)
                    Dim pigeonTiers As New List(Of VisibleTier)


                    If GetUserRole(name, client) = "Admin" Then

                        If customerClient <> client Then
                            adminTiers = getVisibleTiers(GetUserEmail(name, client), client, getRemanResponse(year, make, model, engine, transmission, name, client, GetUserEmail(name, client), tierID)(0)(counter).tiers, tierID)
                            adminTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "C&K's Cost"
                            Dim pigeonAdmin = GetAdminUserByClient(customerClient)
                            Try
                                pigeonTiers = getVisibleTiers(pigeonAdmin, customerClient, getRemanResponse(year, make, model, engine, transmission, pigeonAdmin, customerClient, GetUserEmail(pigeonAdmin, customerClient), tierID)(0)(counter).tiers, tierID)
                                If pigeonTiers.Where(Function(s) s.TierID = 2).Count > 0 Then
                                    pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "Pigeon's Cost"
                                Else
                                    pigeonTiers.Where(Function(a) a.TierID >= 3).FirstOrDefault().TierID = 2
                                    pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "Pigeon's Cost"
                                End If

                            Catch ex As Exception

                            End Try
                        End If

                        If priceInfo.tiers.Count > 0 Then
                            priceInfo.tiers = getVisibleTiers(customerEmail, customerClient, priceInfo.tiers, tierID)
                            If priceInfo.tiers.Where(Function(s) s.TierID >= 3).Count > 0 Then
                                priceInfo.tiers.Where(Function(s) s.TierID >= 3).FirstOrDefault().Label = "Sell Price"
                            End If
                        End If

                        If client <> "CK" Then
                            Dim pigeonAdmin = GetAdminUserByClient(client)
                            If (priceInfo.tiers.Where(Function(a) a.TierID = 2).Count > 0) Then
                            Else
                                Try
                                    pigeonTiers = SearchTransmissionByPartNumber(priceInfo.partno, client, pigeonAdmin, tierID)(0).tiers
                                    pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "Your True Cost"
                                    If pigeonTiers.Count > 0 Then
                                        priceInfo.tiers.Add(pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault())
                                    End If
                                Catch ex As Exception

                                End Try
                            End If


                        End If
                        If customerClient <> client Then

                            If pigeonTiers.Count > 0 Then
                                priceInfo.tiers.Add(pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault())
                            End If
                            If adminTiers.Count > 0 Then
                                priceInfo.tiers.Add(adminTiers.Where(Function(s) s.TierID = 2).FirstOrDefault())
                            End If
                        End If
                        responseOuter(0)(counter) = priceInfo
                    End If
                    counter += 1
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
            p1 = CType(responseOuter(0)(0), Pricing)
            If client = "FMP" Or customerClient = "FMP" Then
                Dim cutoffdatetime As Date = FormatDateTime(Now(), vbShortDate) & " 15:30"
                p1.cutOffMins = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
            End If


            For Each searchedtier As VisibleTier In p1.tiers
                If searchedtier.TierID = If(GetUserRole(name, client) = "Admin", tierID, GetUserTier(name, client)) Then
                    decSellPrice = searchedtier.Price
                    Exit For
                End If
            Next
            'p1.tiers.Item(GetUserTierByEmail(customerEmail, client)).Price
            'p1.tiers.Item(1).Price
            'quote
            Dim add1 As New AdditionalInfo
            add1.partNo = p1.partno
            If GetUserRole(name, client) = "Admin" Then
                If responseOuter(0).Count > 1 Then

                Else
                    add1.quoteID = ProcessQuote(customerNo, customerEmail, name, year, make, model, engine, Nothing, Nothing, client, p1.partno, intLocalStock, decSellPrice, p1, PartTypes.Transmission, transmission, Nothing)
                End If
            Else

                add1.quoteID = ProcessQuote(GetCustomerNo(name, client), GetUserEmail(name, client), name, year, make, model, engine, Nothing, Nothing, client, p1.partno, intLocalStock, decSellPrice, p1, PartTypes.Transmission, transmission, Nothing)
            End If
            'additional info
            add1.NoCheckout = NoCheckout(name, client)
            responseOuter.Add(add1)
            Return js.Serialize(responseOuter)

        Catch Ex As Exception

            'Dim ToEmail As String
            'ToEmail = "darrellb@ckautoparts.com, darrellb@ckautoparts.com"

            'Dim mm As New System.Net.Mail.MailMessage("darrellb@ckautoparts.com", ToEmail)

            'mm.Subject = client & " ERROR"


            'Dim strbody As String

            'strbody = "Message:<br />" & Ex.Message & "<br /><br />Stacktrace:<br />" & Ex.StackTrace & "<br /><br />" 'Source:<br />" & ex.Source & "<br /><br />InnerExceptions:<br />" & ex.InnerException.ToString & "<br /><br />" & "<br /><br />Strings:<br />" & ex.ToString & "<br /><br />"

            'strbody = "The error below occurred with client: " & client & ":<br /><br />" & strbody
            'mm.Body = strbody
            'mm.IsBodyHtml = True


            'Dim smtp As New System.Net.Mail.SmtpClient
            'smtp.Host = "smtp.emailsrvr.com"
            'smtp.Timeout = 500000
            'smtp.Send(mm)
            'Return js.Serialize("Error Check Email")
            Return js.Serialize(Ex.Message.ToString)
        End Try
    End Function
    '<WebMethod()> _
    'Function PlaceOrder(ByVal source As String, ByVal parttype As String, Parts As List(Of Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal VIN As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal client As String)
    '    PlaceOrder(source, parttype, Parts, name, year, make, model, PO, Customer, Mileage, VIN, RepairFacility, Address, City, State, Zip, Phone, Contact, warranty, client)
    'End Function

    Private Function getBuilderResponse(year As String, make As String, model As String, engine As String, transmission As String, name As String, client As String, customerEmail As String, ByVal tierID As String) As ArrayList

        Dim strPartNumber As String
        '*******Builder search
        Dim responseOuter = New ArrayList
        Dim p1 As New Pricing
        Dim decCKCost, decCKSell As Decimal
        Dim decLevelTwo, decInstallationKit, decCore, decBuilderSellPrice As Decimal
        Dim responseBuilder = New ArrayList
        If client = "CK" And GetUserTier(name, client) = 2 Or GetUserTier(name, client) = 3 Then
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommPart As New SqlCommand("SELECT partnumber from tbltransmissioncatalogbuilder where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "' and cost is not null group by year, make, model, engine, transmission, partnumber", conn)
                conn.Open()
                Using rPart As SqlDataReader = sqlCommPart.ExecuteReader()
                    While rPart.Read()
                        Dim list As New List(Of InstallationKit)
                        p1.vendor = 2209
                        p1.subtype = "Builder"
                        p1.installtotal = 0
                        p1.core = 0
                        p1.stock = 0
                        p1.partno = rPart("partnumber").ToString
                        strPartNumber = rPart("partnumber").ToString

                        'get list of possible applications
                        Dim appList As New List(Of Application)
                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlCommApps As New SqlCommand("SELECT notes, tagid, appnumber, labortime from tbltransmissioncatalogbuilder where partnumber = '" & strPartNumber & "' and year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "' and cost is not null", conn2)
                            conn2.Open()
                            Using rApps As SqlDataReader = sqlCommApps.ExecuteReader()
                                While rApps.Read()
                                    Dim a1 As New Application
                                    a1.notes = If(rApps("notes").ToString = "", "N/A", rApps("notes").ToString)
                                    a1.tagid = If(rApps("tagid").ToString = "", "N/A", rApps("tagid").ToString)
                                    a1.appnumber = rApps("appnumber").ToString
                                    a1.labortime = rApps("labortime").ToString
                                    appList.Add(a1)
                                End While
                            End Using
                        End Using

                        p1.applications = appList

                        'get price
                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlCommPrice As New SqlCommand("SELECT top 1 cost, sellprice from tbltransmissioncatalogbuilder where partnumber = '" & strPartNumber & "'", conn2)
                            conn2.Open()
                            Using r As SqlDataReader = sqlCommPrice.ExecuteReader()
                                While r.Read()
                                    decLevelTwo = CDec(r("cost"))
                                    decInstallationKit = 0
                                    decBuilderSellPrice = CDec(r("sellprice"))
                                    decCore = 0
                                End While
                            End Using
                        End Using

                        decCKCost = decLevelTwo + decInstallationKit

                        'get warranties and tiers
                        p1.warranties = GetWarranty(1, name, client)
                        decCKSell = GetCKSell(decCKCost, 1)
                        p1.tiers = GetTiers(1, name, client, decCKSell, "sell")

                        For Each searchedtier As VisibleTier In p1.tiers
                            If searchedtier.TierID = 3 Or searchedtier.TierID = 42 Then
                                searchedtier.Price = decBuilderSellPrice
                            End If
                        Next
                        Try
                            If User.IsInRole("Admin") Then
                                p1.tiers = getVisibleTiers(customerEmail, client, p1.tiers, tierID)
                            End If
                        Catch ex As Exception

                        End Try
                        Dim intLocalStock = 0

                        'get Maps
                        p1.maps = GetMapData(strPartNumber, 1, clienttransmissionstock, False, False, False, p1.vendor)
                        p1.tiers.Sort(Function(v1 As VisibleTier, v2 As VisibleTier) v2.Price.CompareTo(v1.Price))
                        'get core
                        p1.core = decCore
                        p1.installationitems = 0
                        responseBuilder.Add(p1)
                    End While
                End Using

            End Using

            responseOuter.Add(responseBuilder)
        Else
            'add empty array
            responseOuter.Add(responseBuilder)
        End If

    End Function
    Private Function getRemanResponse(year As String, make As String, model As String, engine As String, transmission As String, name As String, client As String, customerEmail As String, ByVal tierID As String) As ArrayList

        Dim decRemanSellPrice, decRemanCorePrice As Decimal
        '******Reman search
        Dim responseReman = New ArrayList
        Dim strPartNumber As String

        Dim responseOuter = New ArrayList

        'get applicable part number(s)
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlCommPart As New SqlCommand("SELECT tblCertifiedCatalog.partnumber from tblCertifiedCatalog where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "' union select partnumber from tblZFCatalog where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "' group by year, make, model, engine, transmission, partnumber", conn)
            conn.Open()
            Using rPart As SqlDataReader = sqlCommPart.ExecuteReader()
                While rPart.Read()
                    If IsDBNull(rPart("PartNumber")) Then
                        Continue While
                    End If

                    Dim p1 As New Pricing

                    strPartNumber = rPart("PartNumber").ToString()
                    p1 = GetTransmissionPrice(rPart("PartNumber"), name, client, tierID)
                    'get list of possible applications
                    p1.applications = GetTransmissionApplications(rPart("PartNumber"), year, make, model, engine, transmission)
                    'get installation kit data
                    Dim list As New List(Of InstallationKit)

                    If p1.vendor = 13095 Then 'zf
                        Dim i1 As New InstallationKit
                        i1.Quantity = 1
                        i1.Part = "N/A"
                        i1.Description = "Pre-filled and pre-programed"
                        i1.TotalPrice = 0
                        list.Add(i1)
                        p1.installtotal = 0
                        p1.installationitems = 1
                        p1.installations = list
                    Else
                        'transmission warnings
                        If client = "CK" Then
                            p1.installationitems = 0
                            'get any warnings
                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlCommWarning As New SqlCommand("select header,cast(content as nvarchar(999)) as content from tbltransmissionwarnings where part = '" & rPart("PartNumber") & "' and right(part,1) <> '%' UNION select header,cast(content as nvarchar(999)) as content from tbltransmissionwarnings where left(part,5) = left('" & rPart("PartNumber") & "',5) and right(part,1) = '%'", conn2)
                                conn2.Open()
                                Using rWarning As SqlDataReader = sqlCommWarning.ExecuteReader()
                                    While rWarning.Read()
                                        p1.WarningHeader = rWarning("header").ToString
                                        p1.WarningContent = rWarning("content").ToString
                                    End While
                                End Using
                            End Using
                        End If

                        'install kits
                        If p1.vendor = 78 Then 'certified

                            Dim i As Integer = 0
                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlComm As New SqlCommand("select * from tblcertifiedinstallkit where upartctr = '" & rPart("partnumber") & "'", conn2)
                                conn2.Open()
                                Using row As SqlDataReader = sqlComm.ExecuteReader()
                                    While row.Read()
                                        If client <> "CK" Then 'include everything
                                            Dim i1 As New InstallationKit
                                            i1.Quantity = row("uQuantity")
                                            i1.Part = row("uPart")
                                            i1.Description = row("uDescription")
                                            i1.TotalPrice = row("uTotalPrice")
                                            p1.installtotal = p1.installtotal + i1.TotalPrice
                                            list.Add(i1)
                                            i = i + 1
                                        Else 'include only pre-filled,cooler or 1 QT for display and pricing
                                            Dim i1 As New InstallationKit
                                            If row("uTotalPrice") = 0 Or UCase(row("uDescription")).Contains("COOLER") = True Then
                                                i1.Quantity = row("uQuantity")
                                                i1.Part = row("uPart")
                                                i1.Description = row("uDescription")
                                                i1.TotalPrice = row("uTotalPrice")
                                                p1.installtotal = p1.installtotal + i1.TotalPrice
                                                list.Add(i1)
                                                i = i + 1
                                            End If
                                            'add additional fluid to pricing only
                                            If (UCase(row("uDescription")).Contains("INSTALL") = True And UCase(row("uDescription")).Contains("FLUID") = True) Or (UCase(row("uDescription")).Contains("FLUID") = True And row("uQuantity") = 1) Then
                                                p1.installtotal = p1.installtotal + row("uTotalPrice")
                                            End If


                                        End If

                                    End While
                                End Using
                            End Using

                            p1.installationitems = i
                            If p1.installationitems > 0 Then
                                p1.installations = list
                            End If
                        End If

                    End If
                    Try
                        If User.IsInRole("Admin") Then
                            p1.tiers = getVisibleTiers(customerEmail, client, p1.tiers, tierID)
                        End If
                    Catch ex As Exception
                        Debug.WriteLine(ex.Message)
                    End Try
                    responseReman.Add(p1)
                    For Each searchedtier As VisibleTier In p1.tiers
                        If searchedtier.TierID = tierID Then
                            decRemanSellPrice = searchedtier.Price
                            Exit For
                        End If
                    Next
                    decRemanCorePrice = p1.core
                End While
            End Using
        End Using

        responseOuter.Add(responseReman)

        Return responseOuter

    End Function

    <WebMethod()>
    Public Function GetYears()
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of TheData)
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT year from tblCertifiedCatalog union select year from tblZFCatalog group by year order by year desc", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim y1 As New TheData
                    y1.thevalue = r("year")
                    list.Add(y1)
                End While
            End Using
        End Using
        Return js.Serialize(list)

    End Function


    <WebMethod()>
    Public Function GetQuotes(ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Quote)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT top 350 dbo.tblRemanQuotes.QuoteID as ID, dbo.tblRemanQuotes.customerEmail, dbo.tblRemanQuotes.QuoteDate, 
            dbo.tblRemanQuotes.Username, case when  dbo.tblCompany.Company is null 
            then 'Internal' else dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine,tblRemanQuotes.Transmission, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice FROM dbo.tblRemanQuotes 
            left outer JOIN dbo.tblCompany ON 
            dbo.tblRemanQuotes.CustomerNo = dbo.tblCompany.CustomerNo
            where tblRemanQuotes.PartTypeID=" & Convert.ToInt32(PartTypes.Transmission) & " order by QuoteID desc", conn)

            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim q1 As New Quote()
                    q1.QuoteID = r("ID")
                    q1.QuoteDate = r("QuoteDate").ToString
                    q1.CustomerContactEmail = r("customerEmail").ToString
                    q1.User = r("UserName").ToString
                    q1.Customer = r("Company").ToString
                    q1.Year = r("Year").ToString
                    q1.Make = r("Make").ToString
                    q1.Model = r("Model").ToString
                    q1.Engine = r("Engine").ToString
                    q1.Transmission = r("Transmission").ToString
                    q1.Part = r("PartNo").ToString
                    q1.LocalStock = r("LocalStock").ToString
                    q1.Cost = r("costprice")
                    q1.SellPrice = r("sellprice")
                    q1.CorePrice = r("coreprice")
                    list.Add(q1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

    Public Function GetTransmissionApplications(ByVal partno As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal transmission As String)
        'get list of possible applications
        Dim appList As New List(Of Application)
        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlCommApps As New SqlCommand("SELECT notes, tagid, appnumber, labortime from tblCertifiedCatalog where partnumber = '" & partno & "' and year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "' union select notes, tagid, appnumber, labortime from tblZFCatalog where partnumber = '" & partno & "' and year = '" & year & "' and make = '" & make & "' and model='" & model & "' and engine = '" & engine & "' and transmission = '" & transmission & "'", conn2)
            conn2.Open()
            Using rApps As SqlDataReader = sqlCommApps.ExecuteReader()
                While rApps.Read()
                    Dim a1 As New Application
                    a1.notes = If(rApps("notes").ToString = "", "N/A", rApps("notes").ToString)
                    a1.tagid = If(rApps("tagid").ToString = "", "N/A", rApps("tagid").ToString)
                    a1.appnumber = rApps("appnumber").ToString
                    a1.labortime = rApps("labortime").ToString
                    appList.Add(a1)
                End While
            End Using
        End Using
        Return appList
    End Function

    Public Shared Function GetInstalledFluidPrice(ByVal partno As String)
        Dim decFluid As Decimal = 0
        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            'Dim sqlComm As New SqlCommand("select case when sum(utotalprice) is null then 0 else sum (utotalprice) end  from tblcertifiedinstallkit where ((udescription like '%FLUID%' and udescription like '%INSTALL%') or udescription like '%COOLER%' ) and upartctr = '" & partno & "'", conn2)
            'conn2.Open()
            Dim sqlComm As New SqlCommand("select case when sum(utotalprice) is null then 0 else sum (utotalprice) end  from tblcertifiedinstallkit where ((udescription like '%FLUID%') or udescription like '%COOLER%' ) and upartctr = '" & partno & "'", conn2)
            conn2.Open()
            decFluid = sqlComm.ExecuteScalar
        End Using

        ''add in any 1 quarts of extra fluid
        'Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
        '    Dim sqlComm As New SqlCommand("select case when sum(utotalprice) is null then 0 else sum (utotalprice) end  from tblcertifiedinstallkit where udescription like '%FLUID%' and udescription not like '%INSTALL%' and uquantity=1 and upartctr = '" & partno & "'", conn2)
        '    conn2.Open()
        '    decFluid = decFluid + CInt(sqlComm.ExecuteScalar)
        'End Using
        Return decFluid
    End Function
    Public Shared Function GetInstallPacket(ByVal partno As String, ByVal warningonly As Boolean) As String
        Dim strPacket As String = ""
        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim strsql As String
            If warningonly = True Then
                strsql = "select upart  from tblcertifiedinstallkit inner join tbltransmissionwarnings on tblcertifiedinstallkit.upartctr=tbltransmissionwarnings.part where udescription = 'INSTALLATION PACKET' and upartctr = '" & partno & "'"
            Else
                strsql = "select upart  from tblcertifiedinstallkit where udescription = 'INSTALLATION PACKET' and upartctr = '" & partno & "'"
            End If
            Dim sqlComm As New SqlCommand(strsql, conn2)
            conn2.Open()
            Try
                strPacket = sqlComm.ExecuteScalar.ToString()
            Catch Ex As Exception
            End Try
        End Using
        If String.IsNullOrEmpty(strPacket) = False Then
            strPacket = "https://smtc.certifiedtransmission.com/files/installation/" & strPacket & ".pdf"
        End If
        Return strPacket
    End Function
    <WebMethod()>
    Public Function GetPartNo(ByVal appno As String)
        Dim strPartNo As String
        Dim js As New JavaScriptSerializer

        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT partnumber from tblCertifiedCatalog where appnumber = '" & appno & "'", conn2)
            conn2.Open()
            strPartNo = sqlComm.ExecuteScalar()
        End Using

        Return js.Serialize(strPartNo)
    End Function

End Class