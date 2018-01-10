Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon
Imports System.Data.SqlClient
Imports Pigeon.Enums

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class DifferentialWebService
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
    Public Class Catalog
        Public Property PartNo As String
            Get
                Return m_PartNo
            End Get
            Set(ByVal value As String)
                m_PartNo = value
            End Set
        End Property
        Private m_PartNo As String
        Public Property price As Decimal
            Get
                Return m_price
            End Get
            Set(ByVal value As Decimal)
                m_price = value
            End Set
        End Property
        Private m_price As Decimal
        Public Property wholesale As Decimal
            Get
                Return m_wholesale
            End Get
            Set(ByVal value As Decimal)
                m_wholesale = value
            End Set
        End Property
        Private m_wholesale As Decimal
        Public Property thirtysixone As Decimal
            Get
                Return m_thirtysixone
            End Get
            Set(ByVal value As Decimal)
                m_thirtysixone = value
            End Set
        End Property
        Private m_thirtysixone As Decimal
        Public Property thirtysixunlimited As Decimal
            Get
                Return m_thirtysixunlimited
            End Get
            Set(ByVal value As Decimal)
                m_thirtysixunlimited = value
            End Set
        End Property
        Private m_thirtysixunlimited As Decimal
        Public Property Core As String
            Get
                Return m_Core
            End Get
            Set(ByVal value As String)
                m_Core = value
            End Set
        End Property
        Private m_Core As String
        Public Property Description As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property LocalStock As String
            Get
                Return m_LocalStock
            End Get
            Set(ByVal value As String)
                m_LocalStock = value
            End Set
        End Property
        Private m_LocalStock As String
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
        Public Property Type() As String
            Get
                Return m_Type
            End Get
            Set(ByVal value As String)
                m_Type = value
            End Set
        End Property
        Private m_Type As String

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
    Public Function GetData(ByVal source As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal position As String, ByVal name As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of TheData)
        Dim list2 As New List(Of Parts)
        Dim response = New ArrayList
        Dim lstMakes As New List(Of ExcludeMakes)
        Dim positionID As Long

        GetDefaults(client)

        positionID = IIf(position = "Rear", 30, 22)

        If year <> "null" And make <> "null" And model <> "null" And position <> "null" Then
            'get results


            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand(String.Format("SELECT DISTINCT
                  TOP (100) PERCENT
                  pricing.part            AS partno,
                  pricing.cost,
                  pricing.core,
                  note               AS description,
                CASE WHEN diff.PositionID = 30
                    THEN 'Rear'
                  WHEN diff.PositionID = 22
                    THEN 'Front'
                  ELSE 'Unknown' END AS Position

                FROM ACES.dbo.Vehicle AS aces_vehicle LEFT OUTER JOIN
                  PartsManager.dbo.tblZumbrotaDifferentialCatalog AS diff ON aces_vehicle.BaseVehicleID = diff.BaseVehicleID
                  RIGHT OUTER JOIN
                  VehicleData.dbo.LKP_VEH_AAIA AS dataone_aaia_cross ON aces_vehicle.VehicleID = dataone_aaia_cross.AAIA_VehicleID
                  RIGHT OUTER JOIN
                  VehicleData.dbo.VIN_Pattern AS dataone_vin ON dataone_aaia_cross.vehicle_id = dataone_vin.VEHICLE_ID AND
                                                                dataone_aaia_cross.engine_id = dataone_vin.DEF_ENGINE_ID
                  INNER JOIN PartsManager.dbo.tblZumbrotaDifferentialPricing AS pricing ON pricing.part = diff.Part

                WHERE (dataone_vin.year='{0}' and dataone_vin.make='{1}' and dataone_vin.model='{2}' and diff.positionid={3})
                ORDER BY partno", year, make, model, positionID), conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim p1 As New Parts
                        p1.CorePrice = r("Core")

                        p1.Description = r("Description")
                        p1.PartNumber = r("PartNo")
                        p1.Position = r("Position")

                        'local stock
                        If client = "CK" Or String.IsNullOrEmpty(clientdifferentialstock) Then
                            p1.LocalStock = 0
                        Else
                            Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(IIf(client = "AutoNation", GetDistributor(name), clientdifferentialstock)))
                                conn2.Open()
                                Dim sqlComm8 As New SqlCommand("SELECT count(Part) from tblStock WHERE part = '" & p1.PartNumber & "' AND type = 3 AND Arrive IS NOT NULL AND ReturnType IS NULL and ckorderid is null", conn2)

                                p1.LocalStock = sqlComm8.ExecuteScalar
                            End Using
                        End If


                        list2.Add(p1)

                        'quote
                        'Dim strsqlins As String
                        'Using conn2 As New SqlConnection(GetClientConnectionString(client))
                        '    strsqlins = "insert into tbldifferentialquotes(quotedate, username, customerno, year, make, model, type, partno, localstock) values ('" & Now() & "','" & name & "','" & GetCustomerNo(name, client) & "','" & year & "','" & make & "','" & model & "','" & position & "','" & p1.PartNumber & "','" & p1.LocalStock & "')"
                        '    Dim sqlCommins As New SqlCommand(strsqlins, conn2)
                        '    conn2.Open()
                        '    sqlCommins.ExecuteNonQuery()
                        'End Using
                    End While
                End Using
            End Using

            Return js.Serialize(list2)
        End If

        If year <> "null" And make <> "null" And model <> "null" Then
            Dim p1 As New TheData
            p1.thevalue = "Front"
            list.Add(p1)
            Dim p2 As New TheData
            p2.thevalue = "Rear"
            list.Add(p2)
            Return js.Serialize(list)
        End If
        If year <> "null" And make <> "null" Then
            'get model
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT model from VehicleData.dbo.VIN_Pattern where year = '" & year & "' and make = '" & make & "' group by model order by model", conn)
                conn.Open()
                sqlComm.CommandTimeout = 120
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim e1 As New TheData
                        e1.thevalue = r("model")
                        list.Add(e1)
                    End While
                End Using
            End Using

            Return js.Serialize(list)
        End If

        If year <> "null" Then
            'get make
            If client <> "CK" Then
                'get make exclusions
                lstMakes = GetMakeExclusions(client, GetCustomerNo(name, client))
            End If
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("SELECT make from VehicleData.dbo.VIN_Pattern where year = '" & year & "' group by make order by make", conn)
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
        End If

        'get years
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select year from VehicleData.dbo.VIN_Pattern group by year order by year desc", conn)
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


    End Function
    <WebMethod()>
    Public Function GetDataByVin(ByVal vin As String, ByVal name As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list2 As New List(Of Parts)
        Dim pattern As String = ""

        pattern = GetVinPattern(Trim(vin))

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand(String.Format("SELECT DISTINCT
                  TOP (100) PERCENT
                  pricing.part            AS partno,
                  cost,
                  pricing.core,
                  note               AS description,
                CASE WHEN diff.PositionID = 30
                    THEN 'Rear'
                  WHEN diff.PositionID = 22
                    THEN 'Front'
                  ELSE 'Unknown' END AS Position,
                  dataone_vin.year,
                  dataone_vin.make,
                  dataone_vin.model

                FROM ACES.dbo.Vehicle AS aces_vehicle LEFT OUTER JOIN
                  PartsManager.dbo.tblZumbrotaDifferentialCatalog AS diff ON aces_vehicle.BaseVehicleID = diff.BaseVehicleID
                  RIGHT OUTER JOIN
                  VehicleData.dbo.LKP_VEH_AAIA AS dataone_aaia_cross ON aces_vehicle.VehicleID = dataone_aaia_cross.AAIA_VehicleID
                  RIGHT OUTER JOIN
                  VehicleData.dbo.VIN_Pattern AS dataone_vin ON dataone_aaia_cross.vehicle_id = dataone_vin.VEHICLE_ID AND
                                                                dataone_aaia_cross.engine_id = dataone_vin.DEF_ENGINE_ID
                  INNER JOIN PartsManager.dbo.tblZumbrotaDifferentialPricing AS pricing ON pricing.part = diff.Part

                WHERE (dataone_vin.vin_pattern='{0}')
                ORDER BY partno", pattern), conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New Parts
                    p1.CorePrice = r("Core")

                    p1.Description = r("Description")
                    p1.PartNumber = r("PartNo")
                    p1.Position = r("Position")

                    'local stock
                    If client = "CK" Or String.IsNullOrEmpty(clientdifferentialstock) Then
                        p1.LocalStock = 0
                    Else
                        Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(IIf(client = "AutoNation", GetDistributor(name), clientdifferentialstock)))
                            conn2.Open()
                            Dim sqlComm8 As New SqlCommand("SELECT count(Part) from tblStock WHERE part = '" & p1.PartNumber & "' AND type = 3 AND Arrive IS NOT NULL AND ReturnType IS NULL and ckorderid is null", conn2)

                            p1.LocalStock = sqlComm8.ExecuteScalar
                        End Using
                    End If


                    list2.Add(p1)

                    'quote
                    Dim strsqlins As String
                    Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        strsqlins = "insert into tbldifferentialquotes(quotedate, username, customerno, year, make, model, type, partno, localstock) values ('" & Now() & "','" & name & "','" & GetCustomerNo(name, client) & "','" & r("year") & "','" & r("make") & "','" & r("model") & "','" & r("position") & "','" & p1.PartNumber & "','" & p1.LocalStock & "')"
                        Dim sqlCommins As New SqlCommand(strsqlins, conn2)
                        conn2.Open()
                        sqlCommins.ExecuteNonQuery()
                    End Using
                End While
            End Using
        End Using

        Return js.Serialize(list2)
    End Function
    Public Shared Function GetDifferentialPrice(ByVal partno As String, ByVal name As String, ByVal client As String)

        Dim decCKCost, decCKSell As Decimal



        Dim diffType As Enums.PartTypes


        GetDefaults(client)


        diffType = IIf(GetDiffPosition(partno) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)


        'get initial full ck cost
        decCKCost = GetCKCatalogCost(partno, diffType, 91)


        Dim p1 As New Pricing
        If decCKCost = 0 Then
            Return p1
        End If


        p1.showpartno = True
        p1.partno = partno
        p1.vendor = 91
        p1.subtype = "Reman"

        'check see if part has an override
        Dim override As New OverridePart
        override = VendorOverride(partno, diffType, 91)
        If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" Then
            If override.Override = True Then
                p1.vendor = override.OverridePartVendor
                p1.partno = override.OverridePartNo
                decCKCost = GetCKCatalogCost(p1.partno, diffType, p1.vendor)
            End If
        End If


        p1.warranties = GetWarranty(diffType, name, client)
        'get our all important base Tier(2) to mark on top of this
        decCKSell = GetCKSell(decCKCost, diffType)
        'see if pretty, if so set sell to this new price
        Dim decPrettyPrice As Integer
        decPrettyPrice = GetPrettyPrice(partno, diffType, p1.vendor)
        If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" And decPrettyPrice > 0 Then
            p1.tiers = GetTiers(diffType, name, client, decPrettyPrice, "pretty")
        Else
            p1.tiers = GetTiers(diffType, name, client, decCKSell, "sell")
        End If

        'see if price override, if so set sell to this new price
        Dim decOverridePrice As Integer
        decOverridePrice = GetOverridePrice(partno, diffType, p1.vendor, name, client)
        If decOverridePrice > 0 And client <> "CK" Then
            p1.tiers = GetTiers(diffType, name, client, decOverridePrice, "pretty")
        ElseIf decOverridePrice > 0 And client = "CK" Then
            'override tier price for that user
            Dim overrideTier = p1.tiers.Where(Function(t) t.TierID = GetUserTier(name, client)).FirstOrDefault()
            overrideTier.Price = decOverridePrice
        End If
        'check for any pigeon pretty prices(pretty prices for the pigeon's tier not the pigeon's true cost)
        If client <> "CK" Then
            For Each tier As VisibleTier In p1.tiers
                If tier.TierID <> 1 And tier.TierID <> 2 Then
                    decPrettyPrice = GetPigeonPrettyPrice(partno, PartTypes.Differential, p1.vendor, tier.TierID, client)
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

        clientdifferentialstock = IIf(client = "AutoNation", GetDistributor(name), clientdifferentialstock).ToString
        If String.IsNullOrEmpty(clientdifferentialstock) Then
            p1.localstock = 0
        Else
            p1.localstock = GetLocalStock(partno, diffType, clientdifferentialstock)
        End If

        'get maps
        Dim objClosestWarehouse As New ClosestWarehouse
        Dim objAltClosestWarehouse As New ClosestWarehouse
        Dim strWarehouseClient As String
        Dim strAltWarehouseClient As String

        If p1.localstock > 0 Then 'locally available, get delivery map data
            p1.maps = GetMapData(partno, diffType, clientdifferentialstock, True, IIf(client = "AutoNation", False, True), IIf(client = "AutoNation", False, True), p1.vendor)
            If client = "AutoNation" Then
                'set local distributor as closet warehouse
                objClosestWarehouse = GetDistributorWarehouse(clientdifferentialstock, name)
            End If
        Else 'check others
            If client = "AutoNation" And (GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "CO" Or GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "WA") Then
                'exclude tracy warehouses

                p1.maps = GetMapData(partno, diffType, clientdifferentialstock, False, True, True, p1.vendor, , True)
            Else
                p1.maps = GetMapData(partno, diffType, clientdifferentialstock, False, True, True, p1.vendor)
            End If

            If client = "AutoNation" Then
                'back out any markup for the dealership tier
                Dim decNewPrice As Decimal = GetClientCost(partno, diffType, 91, "AutoNation")

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
                    Dim objNonAutoNationMaps = GetMapData(partno, diffType, clientdifferentialstock, False, True, True, p1.vendor, , IIf(GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "CO" Or GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation") = "WA", True, False), True)
                    objAltClosestWarehouse = FindClosestWarehouse(objNonAutoNationMaps, GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"))
                    p1.AutoNationAltDeliveryFrom = UCase(objAltClosestWarehouse.State)
                    p1.AutoNationAltDelivery = objAltClosestWarehouse.Schedule
                    strAltWarehouseClient = GetWarehouseClient(objAltClosestWarehouse.WarehouseID).ToString
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
            End If
        End If
        p1.AutoNationDeliveryState = UCase(IIf(client = "AutoNation", GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"), "N/A"))
        p1.AutoNationDeliveryFrom = UCase(IIf(client = "AutoNation", objClosestWarehouse.State, "N/A"))
        p1.AutoNationDelivery = IIf(client = "AutoNation", objClosestWarehouse.Schedule, 0)
        p1.AutoNation = IIf(client = "AutoNation", True, False)
        p1.tiers.Sort(Function(v1 As VisibleTier, v2 As VisibleTier) v2.Price.CompareTo(v1.Price))
        Return p1
    End Function


    <WebMethod()>
    Public Function InsertNewQuote(ByVal customerNo As String, ByVal customerEmail As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal name As String, ByVal client As String, ByVal partNo As String, ByVal vin As String)
        Dim js As New JavaScriptSerializer
        Try

            If GetCustomerNo(name, client) = "1040" Or GetCustomerNo(name, client) = "1041" Then Exit Function
            Dim priceInfo As Pricing = GetDifferentialPrice(partNo, name, client)
            Dim decSellPrice As Decimal

            For Each searchedtier As VisibleTier In priceInfo.tiers
                If searchedtier.TierID = If(GetUserRole(name, client) = "Admin", GetUserTierByEmail(customerEmail, client), GetUserTier(name, client)) Then
                    decSellPrice = searchedtier.Price
                    Exit For
                End If
            Next

            Dim add1 As New AdditionalInfo
            If vin <> "" Or vin <> String.Empty Then
                add1.quoteID = ProcessQuoteWithVIN(customerNo, customerEmail, name, year, make, model, Nothing, Nothing, Nothing, client, partNo, priceInfo.localstock, decSellPrice, priceInfo, PartTypes.Differential, Nothing, IIf(GetDiffPosition(partNo) = 22, PartTypes.FrontDiff, PartTypes.RearDiff), vin)
            Else
                add1.quoteID = ProcessQuote(customerNo, customerEmail, name, year, make, model, Nothing, Nothing, Nothing, client, partNo, priceInfo.localstock, decSellPrice, priceInfo, PartTypes.Differential, Nothing, IIf(GetDiffPosition(partNo) = 22, PartTypes.FrontDiff, PartTypes.RearDiff))
            End If

            add1.partNo = partNo
            Return js.Serialize(add1)

        Catch ex As Exception
            Return js.Serialize(False)

        End Try

    End Function

    <WebMethod()>
    Public Function GetPrice(ByVal year As String, ByVal make As String, ByVal model As String, ByVal customerNo As String, ByVal customerEmail As String, ByVal partno As String, ByVal name As String, ByVal client As String, ByVal customerClient As String, ByVal vin As String, ByVal tierID As String)

        If GetCustomerNo(name, client) = "1040" Or GetCustomerNo(name, client) = "1041" Then Exit Function

        Dim js As New JavaScriptSerializer()
        Dim response = New ArrayList
        Dim adminTiers As New List(Of VisibleTier)
        Dim pigeonTiers As New List(Of VisibleTier)
        Dim priceInfo As Pricing = IIf(customerClient = client, GetDifferentialPrice(partno, name, client), GetDifferentialPrice(partno, GetUserNameByEmail(customerEmail, customerClient), customerClient))
        Try
            If GetUserRole(name, client) = "Admin" Then
                If customerClient <> client Then
                    adminTiers = getVisibleTiers(GetUserEmail(name, client), client, GetDifferentialPrice(partno, name, client).tiers, tierID)
                    adminTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "C&K's Cost"
                    Dim pigeonAdmin = GetAdminUserByClient(customerClient)
                    Try
                        pigeonTiers = SearchDiffByPartNumber(partno, customerClient, pigeonAdmin)(0).tiers
                        pigeonTiers.Where(Function(s) s.TierID = 2).FirstOrDefault().Label = "Pigeon's Cost"
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
                            pigeonTiers = SearchDiffByPartNumber(partno, client, pigeonAdmin)(0).tiers
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

            End If
        Catch ex As Exception

        End Try
        If client = "FMP" Or customerClient = "FMP" Then
            Dim cutoffdatetime As Date = FormatDateTime(Now(), vbShortDate) & " 15:30"
            priceInfo.cutOffMins = DateDiff(DateInterval.Minute, Now(), cutoffdatetime)
        End If
        response.Add(priceInfo)

        Dim decSellPrice As Decimal
        For Each searchedtier As VisibleTier In priceInfo.tiers
            If searchedtier.TierID = tierID Then
                decSellPrice = searchedtier.Price
                Exit For
            End If
        Next
        'additional info
        Dim add1 As New AdditionalInfo
        add1.NoCheckout = NoCheckout(name, client)
        If GetUserRole(name, client) = "Admin" Then

        Else
            If String.IsNullOrEmpty(vin) = False Then
                add1.quoteID = ProcessQuoteWithVIN(customerNo, customerEmail, name, year, make, model, Nothing, Nothing, Nothing, client, partno, priceInfo.localstock, decSellPrice, priceInfo, PartTypes.Differential, Nothing, IIf(GetDiffPosition(partno) = 22, PartTypes.FrontDiff, PartTypes.RearDiff), vin)
            Else
                add1.quoteID = ProcessQuote(customerNo, customerEmail, name, year, make, model, Nothing, Nothing, Nothing, client, partno, priceInfo.localstock, decSellPrice, priceInfo, PartTypes.Differential, Nothing, IIf(GetDiffPosition(partno) = 22, PartTypes.FrontDiff, PartTypes.RearDiff))
            End If
        End If

        response.Add(add1)
        Return js.Serialize(response)
    End Function



    <WebMethod()>
    Public Function GetQuotes(ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Quote)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT top 350 dbo.tblRemanQuotes.QuoteID as ID, dbo.tblRemanQuotes.customerEmail, dbo.tblRemanQuotes.DiffType, dbo.tblRemanQuotes.QuoteDate, 
            dbo.tblRemanQuotes.Username, case when  dbo.tblCompany.Company is null 
            then 'Internal' else dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine,tblRemanQuotes.Transmission, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice FROM dbo.tblRemanQuotes 
            left outer JOIN dbo.tblCompany ON 
            dbo.tblRemanQuotes.CustomerNo = dbo.tblCompany.CustomerNo
            where tblRemanQuotes.PartTypeID=" & Convert.ToInt32(PartTypes.Differential) & " order by QuoteID desc", conn)

            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim q1 As New Quote()
                    q1.QuoteID = r("ID")
                    q1.QuoteDate = r("QuoteDate").ToString
                    q1.User = r("UserName").ToString
                    q1.CustomerContactEmail = r("customerEmail").ToString
                    q1.Customer = r("Company").ToString
                    q1.Year = r("Year").ToString
                    q1.Make = r("Make").ToString
                    q1.Model = r("Model").ToString
                    q1.Type = r("DiffType").ToString
                    q1.Part = r("PartNo").ToString
                    q1.LocalStock = r("LocalStock").ToString
                    list.Add(q1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

End Class