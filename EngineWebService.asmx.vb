Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon
Imports Pigeon.Enums

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class EngineWebService
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

    <WebMethod()>
    Public Function GetData(ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal size As String, ByVal options As String, ByVal name As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of TheData)
        Dim list2 As New List(Of TheData)
        Dim stratk As String
        Dim lstMakes As List(Of ExcludeMakes)

        Dim response = New ArrayList
        Dim searchon As String

        If options <> "null" Then
            searchon = "options"
            GoTo search
        End If

        If size <> "null" Then
            searchon = "size"
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
                    Dim sqlComm As New SqlCommand("SELECT year from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno group by year", conn)
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
                'get make exclusions

                If client <> "CK" Then
                    'get make exclusions
                    lstMakes = GetMakeExclusions(client, GetCustomerNo(name, client))
                End If
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT make from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where year = '" & year & "' group by make order by make", conn)
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
                'Dim m1 As New TheData
                'm1.thevalue = " Select Model"
                'list.Add(m1)
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT model from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where year = '" & year & "' and make = '" & make & "' group by model order by model", conn)
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
                'Dim m1 As New TheData
                'm1.thevalue = " Select Engine"
                'list.Add(m1)
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT cylinders as engtype from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where year = '" & year & "' and make = '" & make & "' and model='" & model & "' group by cylinders order by cylinders", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("engtype")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function

            Case "engine"
                'Dim m1 As New TheData
                'm1.thevalue = " Select Liters"
                'list.Add(m1)
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT size as liter from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and cylinders = '" & engine & "' group by size order by size", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("liter")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function

            Case "size"
                'Dim m1 As New TheData
                'm1.thevalue = " Select Options"
                'list.Add(m1)
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlComm As New SqlCommand("SELECT options from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and cylinders = '" & engine & "' and size = '" & size & "' group by options order by options", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim e1 As New TheData
                            e1.thevalue = r("options")
                            list.Add(e1)
                        End While
                    End Using
                End Using

                Return js.Serialize(list)
                Exit Function
            Case "options"
                Return True

        End Select
    End Function
    Public Shared Function GetEnginePrice(ByVal partno As String, ByVal vendor As Long, ByVal name As String, ByVal client As String, ByVal tierID As String)
        Dim p1 As New Pricing
        Try
            GetDefaults(client)

            Dim decCKCost As Decimal = 0
            Dim decCKSell As Decimal = 0

            'only admin for certain pigeon types will see part number
            If clienttype = "CK" Or clienttype = "PremierWholesaler" Then
                If GetUserRole(name, client) = "Admin" Then
                    p1.showpartno = True
                Else
                    p1.showpartno = False
                End If
            Else
                p1.showpartno = False
            End If
            p1.vendor = vendor
            p1.subtype = "Reman"

            p1.partno = partno

            'get initial full ck cost
            decCKCost = GetCKCatalogCost(p1.partno, 2, vendor)

            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT core from tblatkpricing where atkno = '" & p1.partno & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()

                        If IsDBNull(r("core")) = True Or r("core") Is Nothing Then
                            p1.core = 0
                        Else
                            p1.core = r("core")
                        End If
                        Exit While
                    End While
                End Using
            End Using



            'check see if part has an override
            Dim override As New OverridePart
            override = VendorOverride(p1.partno, 2, p1.vendor)
            If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" Then
                If override.Override = True Then
                    p1.vendor = override.OverridePartVendor
                    p1.partno = override.OverridePartNo
                    decCKCost = GetCKCatalogCost(p1.partno, 2, p1.vendor)
                End If
            End If

            'get warranties and tiers
            p1.warranties = GetWarranty(PartTypes.Engine, name, client)
            'get our all important base Tier(2) to mark on top of this
            decCKSell = GetCKSell(decCKCost, 2)
            'see if pretty, if so set sell to this new price
            Dim decPrettyPrice As Integer
            decPrettyPrice = GetPrettyPrice(p1.partno, 2, p1.vendor)
            If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" And decPrettyPrice > 0 Then
                p1.tiers = GetTiers(2, name, client, decPrettyPrice, "pretty")
            Else
                p1.tiers = GetTiers(2, name, client, decCKSell, "sell")
            End If

            'see if price override, if so set sell to this new price
            Dim decOverridePrice As Integer
            decOverridePrice = GetOverridePrice(partno, 2, p1.vendor, name, client)
            If decOverridePrice > 0 And client <> "CK" Then
                p1.tiers = GetTiers(2, name, client, decOverridePrice, "pretty")
            ElseIf decOverridePrice > 0 And client = "CK" Then
                'override tier price for that user
                'Dim overrideTier = p1.tiers.Where(Function(t) t.TierID = GetUserTier(name, client)).FirstOrDefault()
                Dim overrideTier = p1.tiers.Where(Function(t) t.TierID = tierID).FirstOrDefault()
                overrideTier.Price = decOverridePrice
            End If

            'check for any pigeon pretty prices(pretty prices for the pigeon's tier not the pigeon's true cost)
            If client <> "CK" Then
                For Each tier As VisibleTier In p1.tiers
                    If tier.TierID <> 1 And tier.TierID <> 2 Then
                        decPrettyPrice = GetPigeonPrettyPrice(p1.partno, PartTypes.Engine, p1.vendor, tier.TierID, client)
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

            clientenginestock = IIf(client = "AutoNation", GetDistributor(name), clientenginestock).ToString
            If String.IsNullOrEmpty(clientenginestock) Then
                p1.localstock = 0
            Else
                p1.localstock = GetLocalStock(p1.partno, 2, clientenginestock)
            End If


            'get Maps
            Dim objClosestWarehouse As New ClosestWarehouse
            Dim objAltClosestWarehouse As New ClosestWarehouse
            Dim strWarehouseClient As String
            Dim strAltWarehouseClient As String

            Dim ExcludeWarehouses As New List(Of Warehouses)
            Dim warehouse As New Warehouses
            'If client <> "CK" Or (client = "CK" And GetUserTier(name, client) <> 2 And GetUserTier(name, client) <> 3) Then ''sidenote i hate the way i coded this below
            '    warehouse.WarehouseID = 7
            '    ExcludeWarehouses.Add(warehouse)
            '    Dim warehouse2 As New Warehouses
            '    warehouse2.WarehouseID = 4
            '    ExcludeWarehouses.Add(warehouse2)
            '    Dim warehouse3 As New Warehouses
            '    warehouse3.WarehouseID = 3
            '    ExcludeWarehouses.Add(warehouse3)
            '    Dim warehouse4 As New Warehouses
            '    warehouse4.WarehouseID = 8
            '    ExcludeWarehouses.Add(warehouse4)
            '    Dim warehouse5 As New Warehouses
            '    warehouse5.WarehouseID = 6
            '    ExcludeWarehouses.Add(warehouse5)
            '    Dim warehouse6 As New Warehouses
            '    warehouse6.WarehouseID = 2
            '    ExcludeWarehouses.Add(warehouse6)
            'End If
            If p1.localstock > 0 Then 'locally available, get delivery map data
                p1.maps = GetMapData(p1.partno, 2, clientenginestock, True, IIf(client = "AutoNation", False, True), IIf(client = "AutoNation", False, True), p1.vendor, ExcludeWarehouses)
                If client = "AutoNation" Then
                    'set local distributor as closet warehouse
                    objClosestWarehouse = GetDistributorWarehouse(clientenginestock, name)
                End If
            Else 'check others
                If client = "AutoNation" And (GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "CO" Or GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "WA") Then
                    'exclude tracy warehouses
                    p1.maps = GetMapData(p1.partno, PartTypes.Engine, clientenginestock, False, True, True, p1.vendor, ExcludeWarehouses, True)
                Else
                    p1.maps = GetMapData(p1.partno, PartTypes.Engine, clientenginestock, False, True, True, p1.vendor, ExcludeWarehouses)
                End If

                If client = "AutoNation" Then
                    'back out any markup for the dealership tier
                    Dim decNewPrice As Decimal = GetClientCost(p1.partno, PartTypes.Engine, 75, "AutoNation")

                    objClosestWarehouse = FindClosestWarehouse(p1.maps, GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"))
                    strWarehouseClient = GetWarehouseClient(objClosestWarehouse.WarehouseID).ToString
                    If GetWarehouseAutonation(objClosestWarehouse.WarehouseID) = False Then 'Non Autonation is the closes
                        decNewPrice = IIf(strWarehouseClient = "Tracy", decNewPrice + 225, decNewPrice + 90)

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
                                searchedtier.Price = decNewPrice + 50 + 90
                                p1.AutoNationDeliveryPrice = decNewPrice + 50 + 90
                                Exit For
                            End If
                        Next
                        'Find alternative closest non-autonation
                        Dim objNonAutoNationMaps = GetMapData(p1.partno, 2, clientenginestock, False, True, True, p1.vendor, ExcludeWarehouses, IIf(GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "CO" Or GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "WA", True, False), True)
                        objAltClosestWarehouse = FindClosestWarehouse(objNonAutoNationMaps, GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"))
                        p1.AutoNationAltDeliveryFrom = UCase(objAltClosestWarehouse.State)
                        p1.AutoNationAltDelivery = objAltClosestWarehouse.Schedule
                        strAltWarehouseClient = GetWarehouseClient(objAltClosestWarehouse.WarehouseID).ToString
                        p1.AutoNationAltDeliveryPrice = IIf(strAltWarehouseClient = "Tracy", decNewPrice + 225, decNewPrice + 90)
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

                    'VisibleTier price value plus hot build here 

                    For Each tier In p1.tiers
                        Dim hotbuild
                        Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                            Dim sqlComm2 As New SqlCommand("SELECT HotBuild FROM tblTierBasePrice WHERE TierID = '" & tier.TierID & "' AND PartType = '2'", conn2)
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

        Catch ex As Exception

        End Try

        Return p1
    End Function
    <WebMethod()>
    Public Function GetPrice(ByVal customerNo As String, ByVal customerEmail As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal size As String, ByVal options As String, ByVal name As String, ByVal client As String, ByVal customerClient As String, ByVal tierID As String)
        Dim js As New JavaScriptSerializer()

        Try
            If GetCustomerNo(name, client) = "1040" Or GetCustomerNo(name, client) = "1041" Then Exit Function
            'Pass UsersClient and SelectedCustomerClient..?
            Dim response = New ArrayList
            Dim responseOuter = New ArrayList
            Dim strPartNumber As String
            Dim intLocalStock As Integer
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT top 1 tblatkcatalog.atkno from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where year = '" & year & "' and make = '" & make & "' and model='" & model & "' and cylinders = '" & engine & "' and size = '" & size & "' and options='" & options & "'", conn)
                conn.Open()
                strPartNumber = sqlComm.ExecuteScalar
            End Using

            Dim p As New Pricing
            Dim adminTiers As New List(Of VisibleTier)
            Dim pigeonTiers As New List(Of VisibleTier)
            Dim decSellPrice As Decimal
            p = IIf(customerClient <> client, GetEnginePrice(strPartNumber, 75, GetUserNameByEmail(customerEmail, customerClient), customerClient, tierID), GetEnginePrice(strPartNumber, 75, name, client, tierID))
            Try
                If GetUserRole(name, client) = "Admin" Then
                    p.showpartno = True
                    If customerClient <> client Then
                        adminTiers = getVisibleTiers(GetUserEmail(name, client), client, GetEnginePrice(strPartNumber, 75, name, client, tierID).tiers, tierID)
                        adminTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "C&K's Cost"
                        Dim pigeonAdmin = GetAdminUserByClient(customerClient)
                        Try
                            pigeonTiers = SearchEngineByPartNumber(strPartNumber, customerClient, pigeonAdmin)(0).tiers
                            pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "Pigeon's Cost"
                        Catch ex As Exception

                        End Try

                    End If

                    If p.tiers.Count > 0 Then
                        p.tiers = getVisibleTiers(customerEmail, customerClient, p.tiers, tierID)
                        If p.tiers.Where(Function(s) s.TierID >= 3).Count > 0 Then
                            p.tiers.Where(Function(s) s.TierID >= 3).FirstOrDefault().Label = "Sell Price"
                        End If
                    End If

                    If client <> "CK" Then
                        Dim pigeonAdmin = GetAdminUserByClient(client)
                        If (p.tiers.Where(Function(a) a.TierID = 2).Count > 0) Then
                        Else
                            Try
                                pigeonTiers = SearchEngineByPartNumber(strPartNumber, client, pigeonAdmin)(0).tiers
                                pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "Your True Cost"
                                If pigeonTiers.Count > 0 Then
                                    p.tiers.Add(pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault())
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If

                    If customerClient <> client Then

                        If pigeonTiers.Count > 0 Then
                            p.tiers.Add(pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault())
                        End If
                        If adminTiers.Count > 0 Then
                            p.tiers.Add(adminTiers.Where(Function(s) s.TierID = 2).FirstOrDefault())
                        End If
                    End If
                End If
            Catch ex As Exception
                'If it breaks it will still return Pricing for that customer..
            End Try
            If client = "FMP" Or customerClient = "FMP" Then
                Dim cutoffdatetime As Date = FormatDateTime(Now(), vbShortDate) & " 15:30"
                p.cutOffMins = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
            End If
            response.Add(p)
            responseOuter.Add(response)
            If customerClient <> client Then
                For Each searchedtier As VisibleTier In p.tiers
                    If searchedtier.TierID = If(GetUserRole(name, client) = "Admin", tierID, GetUserTier(name, client)) Then
                        decSellPrice = searchedtier.Price
                        Exit For
                    End If
                Next
            Else
                For Each searchedtier As VisibleTier In p.tiers
                    'If searchedtier.TierID = IIf(GetUserRole(name, client) = "Admin", GetUserTierByEmail(customerEmail, client), GetUserTier(name, client)) Then
                    If searchedtier.TierID = If(GetUserRole(name, client) = "Admin", tierID, GetUserTier(name, client)) Then
                        decSellPrice = searchedtier.Price
                        Exit For
                    End If
                Next
            End If
            'p1.tiers.Item(GetUserTierByEmail(customerEmail, client)).Price
            'p1.tiers.Item(1).Price
            'quote
            If String.IsNullOrEmpty(clientenginestock) Then
                intLocalStock = 0
            Else
                intLocalStock = GetLocalStock(strPartNumber, 2, clientenginestock)
            End If
            'additional info
            Dim add1 As New AdditionalInfo
            If GetUserRole(name, client) = "Admin" Then

                add1.quoteID = ProcessQuote(customerNo, customerEmail, name, year, make, model, engine, size, options, client, strPartNumber, intLocalStock, decSellPrice, p, PartTypes.Engine, Nothing, Nothing)
            Else
                add1.quoteID = ProcessQuote(GetCustomerNo(name, client), GetUserEmail(name, client), name, year, make, model, engine, size, options, client, strPartNumber, intLocalStock, decSellPrice, p, PartTypes.Engine, Nothing, Nothing)
            End If


            add1.NoCheckout = NoCheckout(name, client)
            responseOuter.Add(add1)
            Return js.Serialize(responseOuter)

        Catch ex As Exception
            Return js.Serialize(ex.Message & "   " & ex.StackTrace)
        End Try

    End Function

    <WebMethod()>
    Function PlaceOrder(ByVal source As String, ByVal parttype As String, Parts As List(Of Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal VIN As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal client As String)
        PlaceOrder(source, parttype, Parts, name, year, make, model, PO, Customer, Mileage, VIN, RepairFacility, Address, City, State, Zip, Phone, Contact, warranty, client)
    End Function

    <WebMethod()>
    Public Function GetYears()
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of TheData)
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT year from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno group by year order by year desc", conn)
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class