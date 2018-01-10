Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports Pigeon.Enums

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class PricingWebService
    Inherits System.Web.Services.WebService
    Public Class Price_updates
        Public Property Date_Val() As String
            Get
                Return d_date
            End Get
            Set(ByVal value As String)
                d_date = value
            End Set
        End Property
        Private d_date As String
        Public Property Part() As String
            Get
                Return p_part
            End Get
            Set(ByVal value As String)
                p_part = value
            End Set
        End Property
        Private p_part As String
        Public Property List() As Decimal
            Get
                Return c_List
            End Get
            Set(ByVal value As Decimal)
                c_List = value
            End Set
        End Property
        Private c_List As Decimal
        Public Property Cost() As Decimal
            Get
                Return c_Cost
            End Get
            Set(ByVal value As Decimal)
                c_Cost = value
            End Set
        End Property
        Private c_Cost As Decimal
        Public Property Core() As Decimal
            Get
                Return C_core
            End Get
            Set(ByVal value As Decimal)
                C_core = value
            End Set
        End Property
        Private C_core As Decimal
        Public Property Part_Type() As Decimal
            Get
                Return p_type
            End Get
            Set(ByVal value As Decimal)
                p_type = value
            End Set
        End Property
        Private p_type As Decimal
        Public Property Installationkit() As String
            Get
                Return k_kit
            End Get
            Set(ByVal value As String)
                k_kit = value
            End Set
        End Property
        Private k_kit As String
        Public Property Action() As String
            Get
                Return a_action
            End Get
            Set(ByVal value As String)
                a_action = value
            End Set
        End Property
        Private a_action As String
        Public Property Makes() As String
            Get
                Return m_makes
            End Get
            Set(ByVal value As String)
                m_makes = value
            End Set
        End Property
        Private m_makes As String
    End Class
    Public Class PricingPart
        Public Property Part() As String
            Get
                Return p_part
            End Get
            Set(ByVal value As String)
                p_part = value
            End Set
        End Property
        Private p_part As String
        Public Property List() As Decimal
            Get
                Return c_List
            End Get
            Set(ByVal value As Decimal)
                c_List = value
            End Set
        End Property
        Private c_List As Decimal
        Public Property Cost() As Decimal
            Get
                Return c_Cost
            End Get
            Set(ByVal value As Decimal)
                c_Cost = value
            End Set
        End Property
        Private c_Cost As Decimal
        Public Property CustomerPrice() As Decimal
            Get
                Return c_CustomerPrice
            End Get
            Set(ByVal value As Decimal)
                c_CustomerPrice = value
            End Set
        End Property
        Private c_CustomerPrice As Decimal
        Public Property Core() As Decimal
            Get
                Return C_core
            End Get
            Set(ByVal value As Decimal)
                C_core = value
            End Set
        End Property
        Private C_core As Decimal
        Public Property Part_Type() As Decimal
            Get
                Return p_type
            End Get
            Set(ByVal value As Decimal)
                p_type = value
            End Set
        End Property
        Private p_type As Decimal
        Public Property Year() As String
            Get
                Return p_year
            End Get
            Set(ByVal value As String)
                p_year = value
            End Set
        End Property
        Private p_year As String
        Public Property Make() As String
            Get
                Return p_make
            End Get
            Set(ByVal value As String)
                p_make = value
            End Set
        End Property
        Private p_make As String
        Public Property Model() As String
            Get
                Return p_model
            End Get
            Set(ByVal value As String)
                p_model = value
            End Set
        End Property
        Private p_model As String
        Public Property Cylinders() As String
            Get
                Return p_cylinders
            End Get
            Set(ByVal value As String)
                p_cylinders = value
            End Set
        End Property
        Private p_cylinders As String
        Public Property Size() As String
            Get
                Return p_size
            End Get
            Set(ByVal value As String)
                p_size = value
            End Set
        End Property
        Private p_size As String
        Public Property Engine() As String
            Get
                Return p_engine
            End Get
            Set(ByVal value As String)
                p_engine = value
            End Set
        End Property
        Private p_engine As String
        Public Property Transmission() As String
            Get
                Return p_transmission
            End Get
            Set(ByVal value As String)
                p_transmission = value
            End Set
        End Property
        Private p_transmission As String
        Public Property Options() As String
            Get
                Return p_options
            End Get
            Set(ByVal value As String)
                p_options = value
            End Set
        End Property
        Private p_options As String
        Public Property NewPart() As Boolean
            Get
                Return p_NewPart
            End Get
            Set(ByVal value As Boolean)
                p_NewPart = value
            End Set
        End Property
        Private p_NewPart As Boolean
        Public Property AsOf() As String
            Get
                Return p_AsOf
            End Get
            Set(ByVal value As String)
                p_AsOf = value
            End Set
        End Property
        Private p_AsOf As String


    End Class

    Public Class PriceDates
        Public Property Date_val As String
            Get
                Return d_Date
            End Get
            Set(ByVal value As String)
                d_Date = value
            End Set
        End Property
        Public Property Company_val As String
            Get
                Return c_company
            End Get
            Set(ByVal value As String)
                c_company = value
            End Set
        End Property
        Private c_company As String
        Private d_Date As String
    End Class
    '<WebMethod()>
    'Public Function GetPriceUpdateDates()
    '    Dim lstArrLanguage As ArrayList = New ArrayList
    '    Dim list As New List(Of PriceDates)
    '    Dim js As New JavaScriptSerializer()

    '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '        Dim sqlComm As New SqlCommand("SELECT distinct updatedate from tblremanpriceupdate where action in ('PartUpdate','CoreUpdate','Newpart') and cost is not null order by updatedate desc", conn)
    '        conn.Open()
    '        Using r As SqlDataReader = sqlComm.ExecuteReader()

    '            While r.Read()
    '                Dim m1 As New PriceDates()
    '                m1.Date_val = r("updatedate").ToString

    '                'lstArrLanguage.Add(New ListItem(r("updatedate").ToString, r("updatedate").ToString))
    '                list.Add(m1)
    '            End While
    '        End Using
    '    End Using
    '    Return js.Serialize(list)
    'End Function
    ' <WebMethod()>
    'Public Function GetPriceUpdateCompany()
    '    Dim lstArrLanguage As ArrayList = New ArrayList
    '    Dim list As New List(Of PriceDates)
    '    Dim js As New JavaScriptSerializer()

    '    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '        Dim sqlComm As New SqlCommand("SELECT client FROM  tblPigeonClients", conn)
    '        conn.Open()
    '        Using r As SqlDataReader = sqlComm.ExecuteReader()

    '            While r.Read()
    '                Dim m1 As New PriceDates()
    '                m1.Company_val = r("client").ToString

    '                'lstArrLanguage.Add(New ListItem(r("updatedate").ToString, r("updatedate").ToString))
    '                list.Add(m1)
    '            End While
    '        End Using
    '    End Using
    '    Return js.Serialize(list)
    'End Function

    Private Function PigeonPrices(ByVal custno As String, ByVal parttype As Long, Optional ByVal updatesince As String = "")

        Dim lstParts As New List(Of PricingPart)
        Dim strSql As String = ""
        Dim client As String = ""
        Dim name As String = ""


        'get client name to be used in pricing
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
            conn.Open()
            Dim sqlComm As New SqlCommand("select client,adminuser from tblPigeonClients where ckcustomerno='" & custno & "'", conn)
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    client = r("client")
                    name = r("adminuser")
                End While
            End Using
        End Using

        'get list of parts
        If String.IsNullOrEmpty(updatesince) Then
            'get all parts
            Select Case parttype
                Case PartTypes.Engine
                    strSql = "select distinct tblATKPricing.atkno as part, isnull(core,0) as core, 0 as new from tblATKPricing inner join tblATKCatalog on tblATKCatalog.ATKNo= tblATKPricing.ATKNo "
                Case PartTypes.Transmission
                    strSql = "select distinct part, core, 0 as new from PartsManager.dbo.tblcertifiedpricing inner join PartsManager.dbo.tblcertifiedcatalog on PartsManager.dbo.tblcertifiedpricing.part=PartsManager.dbo.tblcertifiedcatalog.partnumber"
                Case PartTypes.ManualTransmission
                    strSql = "Select distinct dbo.tblZumbrotaTransmissionPricing.part, core, 0 as new from dbo.tblZumbrotaTransmissionPricing inner join dbo.tblZumbrotaTransmissionCatalog on dbo.tblZumbrotaTransmissionPricing.Part=dbo.tblZumbrotaTransmissionCatalog.part"
                Case PartTypes.TransferCase
                    strSql = "Select distinct dbo.tblZumbrotaTransferCasePricing.part, core, 0 as new from dbo.tblZumbrotaTransferCasePricing inner join dbo.tblZumbrotaTransferCaseCatalog on dbo.tblZumbrotaTransferCasePricing.Part=dbo.tblZumbrotaTransferCaseCatalog.part"
                Case PartTypes.Differential
                    strSql = "Select distinct dbo.tblZumbrotaDifferentialPricing.part, core, 0 as new from dbo.tblZumbrotaDifferentialPricing inner join dbo.tblZumbrotaDifferentialCatalog on dbo.tblZumbrotaDifferentialPricing.Part=dbo.tblZumbrotaDifferentialCatalog.part"
            End Select
        Else
            'get updated parts
            Select Case parttype
                Case PartTypes.Engine

                Case PartTypes.Transmission
                    strSql = "select distinct PartsManager.dbo.tblRemanPricingUpdates.part, core, new from PartsManager.dbo.tblcertifiedpricing inner join  PartsManager.dbo.tblRemanPricingUpdates on PartsManager.dbo.tblRemanPricingUpdates.Part=PartsManager.dbo.tblcertifiedpricing.part where PartsManager.dbo.tblRemanPricingUpdates.UpdateDate >='" & updatesince & "' and ( PartsManager.dbo.tblRemanPricingUpdates.New=1 or PartsManager.dbo.tblRemanPricingUpdates.PriceUpdate=1 or PartsManager.dbo.tblRemanPricingUpdates.CoreUpdate=1 or PartsManager.dbo.tblRemanPricingUpdates.installationkitupdate=1)
order by PartsManager.dbo.tblRemanPricingUpdates.part"
                Case PartTypes.ManualTransmission

                Case PartTypes.TransferCase

                Case PartTypes.Differential
                    strSql = "select distinct PartsManager.dbo.tblRemanPricingUpdates.part, core, new from PartsManager.dbo.tblZumbrotaDifferentialPricing inner join  PartsManager.dbo.tblRemanPricingUpdates on PartsManager.dbo.tblRemanPricingUpdates.Part=PartsManager.dbo.tblZumbrotaDifferentialPricing.part where PartsManager.dbo.tblRemanPricingUpdates.UpdateDate >='" & updatesince & "' and ( PartsManager.dbo.tblRemanPricingUpdates.New=1 or PartsManager.dbo.tblRemanPricingUpdates.PriceUpdate=1 or PartsManager.dbo.tblRemanPricingUpdates.CoreUpdate=1)
order by PartsManager.dbo.tblRemanPricingUpdates.part"

            End Select

        End If

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
            conn.Open()
            Dim sqlComm As New SqlCommand(strSql, conn)
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p As New PricingPart
                    p.Part = r("part")
                    p.Core = r("core")
                    p.NewPart = r("new")
                    lstParts.Add(p)
                End While
            End Using
        End Using


        If String.IsNullOrEmpty(updatesince) Then
            'loop through each part and get latest update date
            For Each part As PricingPart In lstParts
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
                    conn.Open()
                    Select Case parttype
                        Case PartTypes.Engine
                            strSql = String.Format("select isnull(max(updatedate),'1/1/16') as asof from PartsManager.dbo.tblRemanPricingUpdates where part='{0}' and parttype=2", part.Part)
                        Case PartTypes.Transmission
                            strSql = String.Format("select isnull(max(updatedate),'1/1/16') as asof from PartsManager.dbo.tblRemanPricingUpdates where part='{0}' and parttype=1", part.Part)
                        Case PartTypes.ManualTransmission
                            strSql = String.Format("select isnull(max(updatedate),'1/1/16') as asof from PartsManager.dbo.tblRemanPricingUpdates where part='{0}' and parttype=10", part.Part)
                        Case PartTypes.TransferCase
                            strSql = String.Format("select isnull(max(updatedate),'1/1/16') as asof from PartsManager.dbo.tblRemanPricingUpdates where part='{0}' and parttype=4", part.Part)
                        Case PartTypes.Differential
                            strSql = String.Format("select isnull(max(updatedate),'1/1/16') as asof from PartsManager.dbo.tblRemanPricingUpdates where part='{0}' and parttype=3", part.Part)
                    End Select
                    Dim sqlComm As New SqlCommand(strSql, conn)
                    part.AsOf = sqlComm.ExecuteScalar()
                End Using
            Next
        End If


        Dim row As Integer = 0
        'loop through each part and get customer price
        For Each part As PricingPart In lstParts

            Dim tiers As List(Of VisibleTier) = GetCustomerPrice(part.Part, parttype, custno, name, client)
            part.List = tiers.Find(Function(x) x.TierID = 1).Price
            part.Cost = tiers.Find(Function(x) x.TierID = 2).Price
            part.CustomerPrice = tiers.Find(Function(x) x.TierID = 5).Price

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
                conn.Open()
                Select Case parttype
                    Case PartTypes.Engine
                        strSql = "select top 1 year, make, model, cylinders, size, options, '' as engine, '' as transmission from tblATKCatalog  where atkno='" & part.Part & "'"
                    Case PartTypes.Transmission
                        strSql = "select top 1 year, make, model, engine, transmission, '' as cylinders, '' as size, '' as options from PartsManager.dbo.tblCertifiedCatalog  where partnumber='" & part.Part & "'"
                    Case PartTypes.ManualTransmission
                        strSql = "SELECT        TOP (1) ACES.dbo.BaseVehicle.YearID AS Year, ACES.dbo.Make.MakeName AS Make, ACES.dbo.Model.ModelName AS Model, 
                         ACES.dbo.DriveType.DriveTypeName AS Drive, ACES.dbo.EngineBase.Liter AS Size, ACES.dbo.EngineBase.Cylinders, '' as options, '' as engine, '' as transmission
FROM            dbo.tblZumbrotaTransmissionCatalog INNER JOIN
                         ACES.dbo.BaseVehicle ON dbo.tblZumbrotaTransmissionCatalog.BaseVehicleID = ACES.dbo.BaseVehicle.BaseVehicleID INNER JOIN
                         ACES.dbo.Make ON ACES.dbo.BaseVehicle.MakeID = ACES.dbo.Make.MakeID INNER JOIN
                         ACES.dbo.Model ON ACES.dbo.BaseVehicle.ModelID = ACES.dbo.Model.ModelID LEFT OUTER JOIN
                         ACES.dbo.DriveType ON dbo.tblZumbrotaTransmissionCatalog.DriveTypeID = ACES.dbo.DriveType.DriveTypeID LEFT OUTER JOIN
                         ACES.dbo.EngineBase ON dbo.tblZumbrotaTransmissionCatalog.EngineBaseID = ACES.dbo.EngineBase.EngineBaseID
WHERE        (dbo.tblZumbrotaTransmissionCatalog.Part = '" & part.Part & "')"
                    Case PartTypes.TransferCase
                        strSql = "SELECT        TOP (1) ACES.dbo.BaseVehicle.YearID AS Year, ACES.dbo.Make.MakeName AS Make, ACES.dbo.Model.ModelName AS Model, 
                         ACES.dbo.DriveType.DriveTypeName AS Drive, ACES.dbo.EngineBase.Liter AS Size, ACES.dbo.EngineBase.Cylinders, '' as options, '' as engine, '' as transmission
FROM            dbo.tblZumbrotaTransferCaseCatalog INNER JOIN
                         ACES.dbo.BaseVehicle ON dbo.tblZumbrotaTransferCaseCatalog.BaseVehicleID = ACES.dbo.BaseVehicle.BaseVehicleID INNER JOIN
                         ACES.dbo.Make ON ACES.dbo.BaseVehicle.MakeID = ACES.dbo.Make.MakeID INNER JOIN
                         ACES.dbo.Model ON ACES.dbo.BaseVehicle.ModelID = ACES.dbo.Model.ModelID LEFT OUTER JOIN
                         ACES.dbo.DriveType ON dbo.tblZumbrotaTransferCaseCatalog.DriveTypeID = ACES.dbo.DriveType.DriveTypeID LEFT OUTER JOIN
                         ACES.dbo.EngineBase ON dbo.tblZumbrotaTransferCaseCatalog.EngineBaseID = ACES.dbo.EngineBase.EngineBaseID
WHERE        (dbo.tblZumbrotaTransferCaseCatalog.Part = '" & part.Part & "')"
                    Case PartTypes.Differential
                        strSql = "SELECT        TOP (1) ACES.dbo.BaseVehicle.YearID AS Year, ACES.dbo.Make.MakeName AS Make, ACES.dbo.Model.ModelName AS Model, 
                         ACES.dbo.DriveType.DriveTypeName AS Drive, ACES.dbo.EngineBase.Liter AS Size, ACES.dbo.EngineBase.Cylinders, dbo.tblzumbrotadifferentialpricing.description as options, '' as engine, '' as transmission
FROM            dbo.tblZumbrotaDifferentialCatalog INNER JOIN dbo.tblZumbrotaDifferentialPricing on dbo.tblZumbrotaDifferentialPricing.Part= dbo.tblZumbrotaDifferentialCatalog.Part INNER JOIN
                         ACES.dbo.BaseVehicle ON dbo.tblZumbrotaDifferentialCatalog.BaseVehicleID = ACES.dbo.BaseVehicle.BaseVehicleID INNER JOIN
                         ACES.dbo.Make ON ACES.dbo.BaseVehicle.MakeID = ACES.dbo.Make.MakeID INNER JOIN
                         ACES.dbo.Model ON ACES.dbo.BaseVehicle.ModelID = ACES.dbo.Model.ModelID LEFT OUTER JOIN
                         ACES.dbo.DriveType ON dbo.tblZumbrotaDifferentialCatalog.DriveTypeID = ACES.dbo.DriveType.DriveTypeID LEFT OUTER JOIN
                         ACES.dbo.EngineBase ON dbo.tblZumbrotaDifferentialCatalog.EngineBaseID = ACES.dbo.EngineBase.EngineBaseID
WHERE        (dbo.tblZumbrotaDifferentialCatalog.Part = '" & part.Part & "')"


                End Select
                Dim sqlComm As New SqlCommand(strSql, conn)
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        part.Year = r("year").ToString
                        part.Make = r("make").ToString
                        part.Model = r("model").ToString
                        part.Cylinders = r("cylinders").ToString
                        part.Size = r("size").ToString
                        part.Options = r("options").ToString
                        part.Engine = r("engine").ToString
                        part.Transmission = r("transmission").ToString
                    End While
                End Using
            End Using
            row = row + 1
        Next


        'loop through each part and see if any venodr matches
        Dim matchParts As New List(Of PricingPart)
        For Each part As PricingPart In lstParts
            Select Case parttype
                Case PartTypes.Transmission
                    strSql = String.Format("select partno from tblremanoverride where matchkey in (select matchkey from tblremanoverride where partno='{0}' and parttype=1) and partno <> '{0}' and parttype={1}", part.Part, 1)
                Case PartTypes.Engine
                    strSql = String.Format("select partno from tblremanoverride where matchkey in (select matchkey from tblremanoverride where partno='{0}' and parttype=1) and partno <> '{0}' and parttype={1}", part.Part, 2)
                Case PartTypes.Differential
                    strSql = String.Format("select partno from tblremanoverride where matchkey in (select matchkey from tblremanoverride where partno='{0}' and parttype=1) and partno <> '{0}' and parttype={1}", part.Part, 3)
                Case PartTypes.TransferCase
                    strSql = String.Format("select partno from tblremanoverride where matchkey in (select matchkey from tblremanoverride where partno='{0}' and parttype=1) and partno <> '{0}' and parttype={1}", part.Part, 4)
                Case PartTypes.ManualTransmission
                    strSql = String.Format("select partno from tblremanoverride where matchkey in (select matchkey from tblremanoverride where partno='{0}' and parttype=1) and partno <> '{0}' and parttype={1}", part.Part, 10)
            End Select
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
                conn.Open()
                Dim sqlComm As New SqlCommand(strSql, conn)
                Dim newPart = sqlComm.ExecuteScalar()
                If IsNothing(newPart) = False Then
                    Dim matchPart = New PricingPart()
                    matchPart.Part = newPart
                    matchPart.AsOf = part.AsOf
                    matchPart.Core = part.Core
                    matchPart.Cost = part.Cost
                    matchPart.CustomerPrice = part.CustomerPrice
                    matchPart.Cylinders = part.Cylinders
                    matchPart.Engine = part.Engine
                    matchPart.List = part.List
                    matchPart.Make = part.Make
                    matchPart.Model = part.Model
                    matchPart.Options = part.Options
                    matchPart.Part_Type = part.Part_Type
                    matchPart.Size = part.Size
                    matchPart.Transmission = part.Transmission
                    matchPart.Year = part.Year
                    matchParts.Add(matchPart)
                End If
            End Using
        Next
        lstParts.AddRange(matchParts)
        Return lstParts
    End Function


    <WebMethod()>
    Public Function GetUpdatedPigeonPartPrices(ByVal custno As String, ByVal parttype As Long, ByVal updatesince As String)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(PigeonPrices(custno, parttype, updatesince))
    End Function

    <WebMethod()>
    Public Function GetAllPigeonPartPrices(ByVal custno As String, ByVal parttype As Long)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(PigeonPrices(custno, parttype))



    End Function
    Public Function GetCustomerPrice(ByVal partno As String, ByVal parttype As String, ByVal custno As String, ByVal name As String, ByVal client As String) As List(Of VisibleTier)


        Dim Result As ArrayList
        Dim PricingResult As New Pricing
        Select Case parttype
            Case 1 'trans
                Result = SearchTransmissionByPartNumber(partno, client, name, Nothing)
            Case 2 'engine
                Result = SearchEngineByPartNumber(partno, client, name)
            Case 4 'transfer
                Result = SearchTransferByPartNumber(partno, client, name)
            Case 3, 6, 7, 8, 9 'diffs
                Result = SearchDiffByPartNumber(partno, client, name)
            Case 10 'manual trans
                Result = SearchManualTransmissionByPartNumber(partno, client, name)
        End Select

        PricingResult = Result(0)
        Return PricingResult.tiers
        'Return js.Serialize(GetTieredPrice(Tiers, intTierID))

    End Function

    Public Shared Function GetTieredPrice(ByVal tiers As List(Of VisibleTier), ByVal tierid As Long)
        On Error GoTo errorz
        For Each searchedtier As VisibleTier In tiers
            If searchedtier.TierID = tierid Then
                Return searchedtier.Price
                Exit For
            End If
        Next
errorz:
        Return 0
    End Function
End Class