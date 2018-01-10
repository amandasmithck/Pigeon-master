Imports System.Data.SqlClient
Imports System.Web.SessionState
Imports System.Reflection
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Xml
Imports Pigeon.DifferentialWebService
Imports Pigeon.TransferCaseWebService
Imports Pigeon.EngineWebService
Imports Pigeon.TransmissionWebService
Imports Pigeon.ManualTransmissionWebService
Imports Pigeon.OrderWebService
Imports System.Runtime.CompilerServices
Imports Pigeon.Enums
Imports wrangler
Imports Pigeon.CKExtensions

Public Class Pigeon
    'Public Shared clientConnection As New SqlConnection
    Public Shared strclientConnection As String
    'Public Shared clientConnection2 As New SqlConnection
    'Public Shared clientConnection3 As New SqlConnection
    'Public Shared clientConnection4 As New SqlConnection
    'Public Shared conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)
    Public Shared wisConnection = ConfigurationManager.ConnectionStrings("InspectionJournalConnectionString").ConnectionString
    'Public Shared conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)
    'Public Shared ckConnection3 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)
    'Public Shared ckConnection4 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)
    'Public Shared ckConnectionCalc As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)
    'Public Shared ckConnectionDefaults As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString)

    Public Shared clientwebsitename As String
    Public Shared clientnoreplyemail As String
    Public Shared clientemails As String
    Public Shared clientckorderemail As String
    Public Shared clientckcustomerno As String
    Public Shared clientckcompanyid As String
    Public Shared clientcertifiedkey As String
    Public Shared clientnewcustomerfromemail As String
    Public Shared clientnewcustomertoemail As String
    Public Shared clientnewcustomerccemail As String
    Public Shared clientdeliveryrollovertime As String
    Public Shared clienttransmissionstock As String
    Public Shared clientenginestock As String
    Public Shared clientdifferentialstock As String
    Public Shared clienttransfercasestock As String
    Public Shared clientmanualtransmissionstock As String
    Public Shared clienturl As String
    Public Shared clienttype As String

    Public Shared Sub GetDefaults(ByVal client As String)
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlCommDefault As New SqlCommand("select * from tblpigeonclients where client = '" & client & "'", conn)
            conn.Open()
            Using rDefaults As SqlDataReader = sqlCommDefault.ExecuteReader
                While rDefaults.Read()
                    Try
                        clientemails = rDefaults("salesmandefaultemail").ToString
                        clientwebsitename = rDefaults("websitename").ToString
                        clientnoreplyemail = rDefaults("noreplyemail").ToString
                        clientckorderemail = rDefaults("ckorderemail").ToString
                        clientckcustomerno = rDefaults("ckcustomerno").ToString
                        clientckcompanyid = rDefaults("ckcompanyid").ToString
                        clientcertifiedkey = rDefaults("certifiedkey").ToString
                        clientdeliveryrollovertime = rDefaults("deliveryrollovertime").ToString
                        clientnewcustomerfromemail = rDefaults("newcustomerfromemail").ToString
                        clientnewcustomertoemail = rDefaults("newcustomertoemail").ToString
                        clientnewcustomerccemail = rDefaults("newcustomerccemail").ToString
                        clienttransmissionstock = rDefaults("TransmissionStock").ToString
                        clientmanualtransmissionstock = rDefaults("ManualTransmissionStock").ToString
                        clientenginestock = rDefaults("EngineStock").ToString
                        clientdifferentialstock = rDefaults("DifferentialStock").ToString
                        clienttransfercasestock = rDefaults("TransferCaseStock").ToString
                        clienturl = rDefaults("Url").ToString
                        clienttype = rDefaults("ClientType").ToString
                    Catch ex As Exception
                    End Try
                End While
            End Using
        End Using
    End Sub
    Public Shared Function GetClientConnectionString(ByVal client As String) As String
        Select Case UCase(client)

            Case "AUTOWAY"
                Return ConfigurationManager.ConnectionStrings("AutowayConnectionString").ConnectionString

            Case "GO"
                Return ConfigurationManager.ConnectionStrings("GOConnectionString").ConnectionString

            Case "LARRYMILLER"
                Return ConfigurationManager.ConnectionStrings("LarryMillerConnectionString").ConnectionString

            Case "CK"
                Return ConfigurationManager.ConnectionStrings("PartsManagerConnectionString").ConnectionString

            Case "BIGVALLEY"
                Return ConfigurationManager.ConnectionStrings("BigValleyConnectionString").ConnectionString

            Case "FITZ"
                Return ConfigurationManager.ConnectionStrings("FitzConnectionString").ConnectionString

            Case "DICKMYERS"
                Return ConfigurationManager.ConnectionStrings("DickMyersConnectionString").ConnectionString

            Case "DUPRATT"
                Return ConfigurationManager.ConnectionStrings("DuPrattConnectionString").ConnectionString

            Case "QUIRK"
                Return ConfigurationManager.ConnectionStrings("QuirkConnectionString").ConnectionString

            Case "GAUDIN"
                Return ConfigurationManager.ConnectionStrings("GaudinConnectionString").ConnectionString

            Case "AUTONATION"
                Return ConfigurationManager.ConnectionStrings("AutoNationConnectionString").ConnectionString

            Case "MESA"
                Return ConfigurationManager.ConnectionStrings("MesaConnectionString").ConnectionString

            Case "GOPOWER"
                Return ConfigurationManager.ConnectionStrings("GoPowerConnectionString").ConnectionString
            Case "FMP"
                Return ConfigurationManager.ConnectionStrings("FMPConnectionString").ConnectionString

        End Select

    End Function
    Public Class AdminInfo
        Public Property UserName As String
        Public Property Email As String
        Public Property Password As String
    End Class
    Public Class Customer
        Public Property Contact As String
        Public Property CompanyMakes() As ArrayList
            Get
                Return m_CompanyMakes
            End Get
            Set(ByVal value As ArrayList)
                m_CompanyMakes = value
            End Set
        End Property
        Private m_CompanyMakes As ArrayList

        Public Property Warranties() As List(Of Warranty)
            Get
                Return m_Warranties
            End Get
            Set(ByVal value As List(Of Warranty))
                m_Warranties = value
            End Set
        End Property
        Private m_Warranties As List(Of Warranty)

        Public Property Role() As String
            Get
                Return m_Role
            End Get
            Set(ByVal value As String)
                m_Role = value
            End Set
        End Property
        Private m_Role As String

        Public Property CustomerType() As String
            Get
                Return m_CustomerType
            End Get
            Set(ByVal value As String)
                m_CustomerType = value
            End Set
        End Property
        Private m_CustomerType As String
        Public Property CompanyID() As String
            Get
                Return m_CompanyID
            End Get
            Set(ByVal value As String)
                m_CompanyID = value
            End Set
        End Property
        Private m_CompanyID As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property LoggedInCompany() As String
            Get
                Return m_LoggedInCompany
            End Get
            Set(ByVal value As String)
                m_LoggedInCompany = value
            End Set
        End Property
        Private m_LoggedInCompany As String
        Public Property LoggedInEmail() As String
            Get
                Return m_LoggedInEmail
            End Get
            Set(ByVal value As String)
                m_LoggedInEmail = value
            End Set
        End Property
        Private m_LoggedInEmail As String
        Public Property CustNo() As String
            Get
                Return m_CustNo
            End Get
            Set(ByVal value As String)
                m_CustNo = value
            End Set
        End Property
        Private m_CustNo As String
        Public Property Address() As String
            Get
                Return m_Address
            End Get
            Set(ByVal value As String)
                m_Address = value
            End Set
        End Property
        Private m_Address As String
        Public Property City() As String
            Get
                Return m_City
            End Get
            Set(ByVal value As String)
                m_City = value
            End Set
        End Property
        Private m_City As String
        Public Property State() As String
            Get
                Return m_State
            End Get
            Set(ByVal value As String)
                m_State = value
            End Set
        End Property
        Private m_State As String
        Public Property Zip() As String
            Get
                Return m_Zip
            End Get
            Set(ByVal value As String)
                m_Zip = value
            End Set
        End Property
        Private m_Zip As String
        Public Property Phone() As String
            Get
                Return m_Phone
            End Get
            Set(ByVal value As String)
                m_Phone = value
            End Set
        End Property
        Private m_Phone As String
        Public Property Autonation() As String
            Get
                Return m_Autonation
            End Get
            Set(ByVal value As String)
                m_Autonation = value
            End Set
        End Property
        Private m_Autonation As String
        Public Property SalesmanEmail() As String
            Get
                Return m_SalesmanEmail
            End Get
            Set(ByVal value As String)
                m_SalesmanEmail = value
            End Set
        End Property
        Private m_SalesmanEmail As String

        Public Property SalesPhone() As String
            Get
                Return m_SalesPhone
            End Get
            Set(ByVal value As String)
                m_SalesPhone = value
            End Set
        End Property
        Private m_SalesPhone As String

        Public Property UserName() As String
            Get
                Return m_UserName
            End Get
            Set(ByVal value As String)
                m_UserName = value
            End Set
        End Property
        Private m_UserName As String
        Public Property Tier() As String
            Get
                Return m_Tier
            End Get
            Set(ByVal value As String)
                m_Tier = value
            End Set
        End Property
        Private m_Tier As String
        Public Property Client() As String
            Get
                Return m_Client
            End Get
            Set(ByVal value As String)
                m_Client = value
            End Set
        End Property
        Private m_Client As String

        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String

        Public Property IP() As String
            Get
                Return m_IP
            End Get
            Set(ByVal value As String)
                m_IP = value
            End Set
        End Property
        Private m_IP As String
        Public Property Calc() As Boolean
            Get
                Return m_Calc
            End Get
            Set(ByVal value As Boolean)
                m_Calc = value
            End Set
        End Property
        Private m_Calc As Boolean
        Public Property ChargeOEMEOC() As Boolean
            Get
                Return m_ChargeOEMEOC
            End Get
            Set(ByVal value As Boolean)
                m_ChargeOEMEOC = value
            End Set
        End Property
        Private m_ChargeOEMEOC As Boolean
        Public Property WarrantyPaperwork() As Boolean
            Get
                Return m_WarrantyPaperwork
            End Get
            Set(ByVal value As Boolean)
                m_WarrantyPaperwork = value
            End Set
        End Property
        Private m_WarrantyPaperwork As Boolean
        Public Property GoogleAnalytics() As String
            Get
                Return m_GoogleAnalytics
            End Get
            Set(ByVal value As String)
                m_GoogleAnalytics = value
            End Set
        End Property
        Private m_GoogleAnalytics As String

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

    Public Class Markup
        Public Property MakeGroup As String
            Get
                Return m_MakeGroup
            End Get
            Set(ByVal value As String)
                m_MakeGroup = value
            End Set
        End Property
        Private m_MakeGroup As String
        Public Property Source As String
            Get
                Return m_Source
            End Get
            Set(ByVal value As String)
                m_Source = value
            End Set
        End Property
        Private m_Source As String
        Public Property Markup() As String
            Get
                Return m_Markup
            End Get
            Set(ByVal value As String)
                m_Markup = value
            End Set
        End Property
        Private m_Markup As String
        Public Property MarkupVal() As Decimal
            Get
                Return m_MarkupVal
            End Get
            Set(ByVal value As Decimal)
                m_MarkupVal = value
            End Set
        End Property
        Private m_MarkupVal As Decimal

    End Class

    Public Class Users
        Public Property UserID() As Guid
            Get
                Return m_UserID
            End Get
            Set(ByVal value As Guid)
                m_UserID = value
            End Set
        End Property
        Private m_UserID As Guid
        Public Property Username() As String
            Get
                Return m_Username
            End Get
            Set(ByVal value As String)
                m_Username = value
            End Set
        End Property
        Private m_Username As String
    End Class

    Public Class UserInfo
        Public Property CustomerNo() As String
            Get
                Return m_CustomerNo
            End Get
            Set(ByVal value As String)
                m_CustomerNo = value
            End Set
        End Property
        Private m_CustomerNo As String
        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(ByVal value As String)
                m_Password = value
            End Set
        End Property
        Private m_Password As String
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(ByVal value As String)
                m_Email = value
            End Set
        End Property
        Private m_Email As String
        Public Property Active() As String
            Get
                Return m_Active
            End Get
            Set(ByVal value As String)
                m_Active = value
            End Set
        End Property
        Private m_Active As String
        Public Property TierID() As String
            Get
                Return m_TierID
            End Get
            Set(ByVal value As String)
                m_TierID = value
            End Set
        End Property
        Private m_TierID As String
        Public Property CanOrder() As String
            Get
                Return m_CanOrder
            End Get
            Set(ByVal value As String)
                m_CanOrder = value
            End Set
        End Property
        Private m_CanOrder As String
        Public Property Err() As String
            Get
                Return m_Error
            End Get
            Set(ByVal value As String)
                m_Error = value
            End Set
        End Property
        Private m_Error As String
    End Class

    Public Class Warranty
        Public Property Tier() As String
            Get
                Return m_Tier
            End Get
            Set(ByVal value As String)
                m_Tier = value
            End Set
        End Property
        Private m_Tier As String
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property Warranty() As String
            Get
                Return m_Warranty
            End Get
            Set(ByVal value As String)
                m_Warranty = value
            End Set
        End Property
        Private m_Warranty As String
        Public Property Base() As Boolean
            Get
                Return m_Base
            End Get
            Set(ByVal value As Boolean)
                m_Base = value
            End Set
        End Property
        Private m_Base As Boolean
        Public Property Percentage() As Decimal
            Get
                Return m_Percentage
            End Get
            Set(ByVal value As Decimal)
                m_Percentage = value
            End Set
        End Property
        Private m_Percentage As String
        Public Property Flat() As Decimal
            Get
                Return m_Flat
            End Get
            Set(ByVal value As Decimal)
                m_Flat = value
            End Set
        End Property
        Private m_Flat As Decimal
        Public Property Href() As String
            Get
                Return m_Href
            End Get
            Set(ByVal value As String)
                m_Href = value
            End Set
        End Property
        Private m_Href As String
    End Class

    Public Class VisibleTier
        Public Property Price() As Decimal
            Get
                Return m_Price
            End Get
            Set(ByVal value As Decimal)
                m_Price = value
            End Set
        End Property
        Private m_Price As Decimal
        Public Property Label() As String
            Get
                Return m_Label
            End Get
            Set(ByVal value As String)
                m_Label = value
            End Set
        End Property
        Private m_Label As String
        Public Property TierID() As String
            Get
                Return m_TierID
            End Get
            Set(ByVal value As String)
                m_TierID = value
            End Set
        End Property
        Private m_TierID As String
        Public Property WarrantyPrice() As Decimal
            Get
                Return m_WarrantyPrice
            End Get
            Set(ByVal value As Decimal)
                m_WarrantyPrice = value
            End Set
        End Property
        Private m_WarrantyPrice As Decimal
        Public Property Local() As Boolean
            Get
                Return m_Local
            End Get
            Set(ByVal value As Boolean)
                m_Local = value
            End Set
        End Property
        Private m_Local As Boolean
    End Class

    Public Class Tier
        Public Property TierID() As String
            Get
                Return m_TierID
            End Get
            Set(ByVal value As String)
                m_TierID = value
            End Set
        End Property
        Private m_TierID As String
        Public Property BasePrice() As String
            Get
                Return m_BasePrice
            End Get
            Set(ByVal value As String)
                m_BasePrice = value
            End Set
        End Property
        Private m_BasePrice As String
        Public Property CostPercentage() As Decimal
            Get
                Return m_CostPercentage
            End Get
            Set(ByVal value As Decimal)
                m_CostPercentage = value
            End Set
        End Property
        Private m_CostPercentage As Decimal
        Public Property CostFlat() As Decimal
            Get
                Return m_CostFlat
            End Get
            Set(ByVal value As Decimal)
                m_CostFlat = value
            End Set
        End Property
        Private m_CostFlat As Decimal
        Public Property Percentage() As Decimal
            Get
                Return m_Percentage
            End Get
            Set(ByVal value As Decimal)
                m_Percentage = value
            End Set
        End Property
        Private m_Percentage As Decimal
        Public Property Flat() As Decimal
            Get
                Return m_Flat
            End Get
            Set(ByVal value As Decimal)
                m_Flat = value
            End Set
        End Property
        Private m_Flat As Decimal
        Public Property Price() As Decimal
            Get
                Return m_Price
            End Get
            Set(ByVal value As Decimal)
                m_Price = value
            End Set
        End Property
        Private m_Price As Decimal
        Public Property Label() As String
            Get
                Return m_Label
            End Get
            Set(ByVal value As String)
                m_Label = value
            End Set
        End Property
        Private m_Label As String
        Public Property Local() As Boolean
            Get
                Return m_Local
            End Get
            Set(ByVal value As Boolean)
                m_Local = value
            End Set
        End Property
        Private m_Local As Boolean
    End Class

    Public Class TheMapData
        Public Property State As String
            Get
                Return m_State
            End Get
            Set(ByVal value As String)
                m_State = value
            End Set
        End Property
        Private m_State As String
        Public Property Schedule As String
            Get
                Return m_Schedule
            End Get
            Set(ByVal value As String)
                m_Schedule = value
            End Set
        End Property
        Private m_Schedule As String
        Public Property Value As String
            Get
                Return m_Value
            End Get
            Set(ByVal value As String)
                m_Value = value
            End Set
        End Property
        Private m_Value As String
        Public Property Abbreviation As String
            Get
                Return m_Abbreviation
            End Get
            Set(ByVal value As String)
                m_Abbreviation = value
            End Set
        End Property
        Private m_Abbreviation As String
        Public Property Warehouse As Long
            Get
                Return m_Warehouse
            End Get
            Set(ByVal value As Long)
                m_Warehouse = value
            End Set
        End Property
        Private m_Warehouse As Long
    End Class

    Public Class InstallationKit
        Public Property Quantity As String
            Get
                Return m_Quantity
            End Get
            Set(ByVal value As String)
                m_Quantity = value
            End Set
        End Property
        Private m_Quantity As String
        Public Property Part As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property Description As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property TotalPrice As String
            Get
                Return m_TotalPrice
            End Get
            Set(ByVal value As String)
                m_TotalPrice = value
            End Set
        End Property
        Private m_TotalPrice As String
    End Class

    Public Class Application
        Public Property notes As String
            Get
                Return m_notes
            End Get
            Set(ByVal value As String)
                m_notes = value
            End Set
        End Property
        Private m_notes As String
        Public Property tagid As String
            Get
                Return m_tagid
            End Get
            Set(ByVal value As String)
                m_tagid = value
            End Set
        End Property
        Private m_tagid As String
        Public Property appnumber As String
            Get
                Return m_appnumber
            End Get
            Set(ByVal value As String)
                m_appnumber = value
            End Set
        End Property
        Private m_appnumber As String
        Public Property labortime As String
            Get
                Return m_labortime
            End Get
            Set(ByVal value As String)
                m_labortime = value
            End Set
        End Property
        Private m_labortime As String
    End Class

    Public Class Applications
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
        Public Property Desc() As String
            Get
                Return m_Desc
            End Get
            Set(ByVal value As String)
                m_Desc = value
            End Set
        End Property
        Private m_Desc As String
        Public Property TagID() As String
            Get
                Return m_TagID
            End Get
            Set(ByVal value As String)
                m_TagID = value
            End Set
        End Property
        Private m_TagID As String
    End Class
    Public Class Pricing
        Public Property cutOffMins As String
        Public Property warranties As List(Of Warranty)
            Get
                Return m_warranties
            End Get
            Set(ByVal value As List(Of Warranty))
                m_warranties = value
            End Set
        End Property
        Private m_warranties As List(Of Warranty)
        Public Property tiers As List(Of VisibleTier)
            Get
                Return m_tiers
            End Get
            Set(ByVal value As List(Of VisibleTier))
                m_tiers = value
            End Set
        End Property
        Private m_tiers As List(Of VisibleTier)
        Public Property maps As List(Of TheMapData)
            Get
                Return m_maps
            End Get
            Set(ByVal value As List(Of TheMapData))
                m_maps = value
            End Set
        End Property
        Private m_maps As List(Of TheMapData)
        Public Property installations As List(Of InstallationKit)
            Get
                Return m_installations
            End Get
            Set(ByVal value As List(Of InstallationKit))
                m_installations = value
            End Set
        End Property
        Private m_installations As List(Of InstallationKit)
        Public Property applications As List(Of Application)
            Get
                Return m_applications
            End Get
            Set(ByVal value As List(Of Application))
                m_applications = value
            End Set
        End Property
        Private m_applications As List(Of Application)
        Public Property core As String
            Get
                Return m_core
            End Get
            Set(ByVal value As String)
                m_core = value
            End Set
        End Property
        Private m_core As String
        Public Property stock As Long
            Get
                Return m_stock
            End Get
            Set(ByVal value As Long)
                m_stock = value
            End Set
        End Property
        Private m_stock As Long

        Public Property localstock As Long
            Get
                Return m_localstock
            End Get
            Set(ByVal value As Long)
                m_localstock = value
            End Set
        End Property
        Private m_localstock As Long

        Public Property partno As String
            Get
                Return m_partno
            End Get
            Set(ByVal value As String)
                m_partno = value
            End Set
        End Property
        Private m_partno As String

        Private m_appnumber As String
        Public Property totalinstall As Decimal
            Get
                Return m_totalinstall
            End Get
            Set(ByVal value As Decimal)
                m_totalinstall = value
            End Set
        End Property
        Private m_totalinstall As Decimal

        Property installtotal As Integer

        Public Property vendor As Long
            Get
                Return m_vendor
            End Get
            Set(ByVal value As Long)
                m_vendor = value
            End Set
        End Property
        Private m_vendor As Long
        Public Property subtype As String
            Get
                Return m_subtype
            End Get
            Set(ByVal value As String)
                m_subtype = value
            End Set
        End Property
        Private m_subtype As String
        Public Property showpartno As Boolean
            Get
                Return m_showpartno
            End Get
            Set(ByVal value As Boolean)
                m_showpartno = value
            End Set
        End Property
        Private m_showpartno As Boolean
        Public Property installationitems As Long
            Get
                Return m_installationitems
            End Get
            Set(ByVal value As Long)
                m_installationitems = value
            End Set
        End Property
        Private m_installationitems As Long
        Public Property WarningHeader As String
            Get
                Return m_WarningHeader
            End Get
            Set(ByVal value As String)
                m_WarningHeader = value
            End Set
        End Property
        Private m_WarningHeader As String
        Public Property WarningContent As String
            Get
                Return m_WarningContent
            End Get
            Set(ByVal value As String)
                m_WarningContent = value
            End Set
        End Property
        Private m_WarningContent As String
        Public Property AutoNation As Boolean
            Get
                Return m_AutoNation
            End Get
            Set(ByVal value As Boolean)
                m_AutoNation = value
            End Set
        End Property
        Private m_AutoNation As Boolean
        Public Property AutoNationDeliveryState As String
            Get
                Return m_AutoNationDeliveryState
            End Get
            Set(ByVal value As String)
                m_AutoNationDeliveryState = value
            End Set
        End Property
        Private m_AutoNationDeliveryState As String
        Public Property AutoNationDelivery As Long
            Get
                Return m_AutoNationDelivery
            End Get
            Set(ByVal value As Long)
                m_AutoNationDelivery = value
            End Set
        End Property
        Private m_AutoNationDelivery As Long

        Public Property AutoNationDeliveryPrice As Decimal
            Get
                Return m_AutoNationDeliveryPrice
            End Get
            Set(ByVal value As Decimal)
                m_AutoNationDeliveryPrice = value
            End Set
        End Property
        Private m_AutoNationDeliveryPrice As Decimal
        Public Property AutoNationDeliveryFrom As String
            Get
                Return m_AutoNationDeliveryFrom
            End Get
            Set(ByVal value As String)
                m_AutoNationDeliveryFrom = value
            End Set
        End Property
        Private m_AutoNationDeliveryFrom As String
        Public Property AutoNationAltDelivery As Long
            Get
                Return m_AutoNationAltDelivery
            End Get
            Set(ByVal value As Long)
                m_AutoNationAltDelivery = value
            End Set
        End Property
        Private m_AutoNationAltDelivery As Long
        Public Property AutoNationAltDeliveryPrice As Decimal
            Get
                Return m_AutoNationAltDeliveryPrice
            End Get
            Set(ByVal value As Decimal)
                m_AutoNationAltDeliveryPrice = value
            End Set
        End Property
        Private m_AutoNationAltDeliveryPrice As Decimal
        Public Property AutoNationAltDeliveryFrom As String
            Get
                Return m_AutoNationAltDeliveryFrom
            End Get
            Set(ByVal value As String)
                m_AutoNationAltDeliveryFrom = value
            End Set
        End Property
        Private m_AutoNationAltDeliveryFrom As String
    End Class

    Public Class Parts
        Public Property PartNumber() As String
            Get
                Return m_PartNumber
            End Get
            Set(ByVal value As String)
                m_PartNumber = value
            End Set
        End Property
        Private m_PartNumber As String
        Public Property SalePrice() As String
            Get
                Return m_SalePrice
            End Get
            Set(ByVal value As String)
                m_SalePrice = value
            End Set
        End Property
        Private m_SalePrice As String
        Public Property CostPrice() As String
            Get
                Return m_CostPrice
            End Get
            Set(ByVal value As String)
                m_CostPrice = value
            End Set
        End Property
        Private m_CostPrice As String
        Public Property WarrantyPrice() As String
            Get
                Return m_WarrantyPrice
            End Get
            Set(ByVal value As String)
                m_WarrantyPrice = value
            End Set
        End Property
        Private m_WarrantyPrice As String
        Public Property CorePrice() As String
            Get
                Return m_CorePrice
            End Get
            Set(ByVal value As String)
                m_CorePrice = value
            End Set
        End Property
        Private m_CorePrice As String
        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property LocalStock() As Integer
            Get
                Return m_LocalStock
            End Get
            Set(ByVal value As Integer)
                m_LocalStock = value
            End Set
        End Property
        Private m_LocalStock As Integer
        Public Property CustomerTransit() As String
            Get
                Return m_CustomerTransit
            End Get
            Set(ByVal value As String)
                m_CustomerTransit = value
            End Set
        End Property
        Private m_CustomerTransit As String
        Public Property ClientTransit() As String
            Get
                Return m_ClientTransit
            End Get
            Set(ByVal value As String)
                m_ClientTransit = value
            End Set
        End Property
        Private m_ClientTransit As String
        Public Property CKTransit() As String
            Get
                Return m_CKTransit
            End Get
            Set(ByVal value As String)
                m_CKTransit = value
            End Set
        End Property
        Private m_CKTransit As String
        Public Property AvaliableSN() As List(Of PartAndSerialNumber)
            Get
                Return m_AvaliableSN
            End Get
            Set(ByVal value As List(Of PartAndSerialNumber))
                m_AvaliableSN = value
            End Set
        End Property
        Private m_AvaliableSN As List(Of PartAndSerialNumber)
        Public Property Subtype() As String
            Get
                Return m_Subtype
            End Get
            Set(ByVal value As String)
                m_Subtype = value
            End Set
        End Property
        Private m_Subtype As String
        Public Property Position() As String
            Get
                Return m_Position
            End Get
            Set(ByVal value As String)
                m_Position = value
            End Set
        End Property
        Private m_Position As String

        Public Property Vendor() As Long
            Get
                Return m_Vendor
            End Get
            Set(ByVal value As Long)
                m_Vendor = value
            End Set
        End Property
        Private m_Vendor As Long
        Public Property PartID() As Long
            Get
                Return m_PartID
            End Get
            Set(ByVal value As Long)
                m_PartID = value
            End Set
        End Property
        Private m_PartID As Long
    End Class

    Public Class PartAndSerialNumber
        Public Property PartNumber() As String
            Get
                Return m_PartNumber
            End Get
            Set(ByVal value As String)
                m_PartNumber = value
            End Set
        End Property
        Private m_PartNumber As String
        Public Property SerialNumber() As String
            Get
                Return m_SerialNumber
            End Get
            Set(ByVal value As String)
                m_SerialNumber = value
            End Set
        End Property
        Private m_SerialNumber As String

    End Class

    Public Class OrderDetails
        Public Property CustNo() As String
            Get
                Return m_CustNo
            End Get
            Set(ByVal value As String)
                m_CustNo = value
            End Set
        End Property
        Private m_CustNo As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property CompanyAddress() As String
            Get
                Return m_CompanyAddress
            End Get
            Set(ByVal value As String)
                m_CompanyAddress = value
            End Set
        End Property
        Private m_CompanyAddress As String
        Public Property Username() As String
            Get
                Return m_Username
            End Get
            Set(ByVal value As String)
                m_Username = value
            End Set
        End Property
        Private m_Username As String
        Public Property CompanyPhone() As String
            Get
                Return m_CompanyPhone
            End Get
            Set(ByVal value As String)
                m_CompanyPhone = value
            End Set
        End Property
        Private m_CompanyPhone As String
        Public Property PO() As String
            Get
                Return m_PO
            End Get
            Set(ByVal value As String)
                m_PO = value
            End Set
        End Property
        Private m_PO As String
        Public Property OwnerName() As String
            Get
                Return m_OwnerName
            End Get
            Set(ByVal value As String)
                m_OwnerName = value
            End Set
        End Property
        Private m_OwnerName As String
        Public Property Mileage() As String
            Get
                Return m_Mileage
            End Get
            Set(ByVal value As String)
                m_Mileage = value
            End Set
        End Property
        Private m_Mileage As String
        Public Property RepairFacility() As String
            Get
                Return m_RepairFacility
            End Get
            Set(ByVal value As String)
                m_RepairFacility = value
            End Set
        End Property
        Private m_RepairFacility As String
        Public Property Address() As String
            Get
                Return m_Address
            End Get
            Set(ByVal value As String)
                m_Address = value
            End Set
        End Property
        Private m_Address As String
        Public Property City() As String
            Get
                Return m_City
            End Get
            Set(ByVal value As String)
                m_City = value
            End Set
        End Property
        Private m_City As String
        Public Property State() As String
            Get
                Return m_State
            End Get
            Set(ByVal value As String)
                m_State = value
            End Set
        End Property
        Private m_State As String
        Public Property Zip() As String
            Get
                Return m_Zip
            End Get
            Set(ByVal value As String)
                m_Zip = value
            End Set
        End Property
        Private m_Zip As String
        Public Property Phone() As String
            Get
                Return m_Phone
            End Get
            Set(ByVal value As String)
                m_Phone = value
            End Set
        End Property
        Private m_Phone As String
        Public Property Contact() As String
            Get
                Return m_Contact
            End Get
            Set(ByVal value As String)
                m_Contact = value
            End Set
        End Property
        Private m_Contact As String
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
        Public Property VIN() As String
            Get
                Return m_VIN
            End Get
            Set(ByVal value As String)
                m_VIN = value
            End Set
        End Property
        Private m_VIN As String
        Public Property Parts() As List(Of Parts)
            Get
                Return m_Parts
            End Get
            Set(ByVal value As List(Of Parts))
                m_Parts = value
            End Set
        End Property
        Private m_Parts As List(Of Parts)
        Public Property Warranty() As String
            Get
                Return m_Warranty
            End Get
            Set(ByVal value As String)
                m_Warranty = value
            End Set
        End Property
        Private m_Warranty As String
        Public Property AutoOwner() As String
            Get
                Return m_AutoOwner
            End Get
            Set(ByVal value As String)
                m_AutoOwner = value
            End Set
        End Property
        Private m_AutoOwner As String
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

    Public Class ExcludeMakes
        Public Property Make() As String
            Get
                Return m_Make
            End Get
            Set(ByVal value As String)
                m_Make = value
            End Set
        End Property
        Private m_Make As String
    End Class
    Public Class VINResult
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
        Public Property Trim() As String
            Get
                Return m_Trim
            End Get
            Set(ByVal value As String)
                m_Trim = value
            End Set
        End Property
        Private m_Trim As String
        Public Property Style() As String
            Get
                Return m_Style
            End Get
            Set(ByVal value As String)
                m_Style = value
            End Set
        End Property
        Private m_Style As String
        Public Property Drive() As String
            Get
                Return m_Drive
            End Get
            Set(ByVal value As String)
                m_Drive = value
            End Set
        End Property
        Private m_Drive As String
        Public Property Cylinders() As String
            Get
                Return m_Cylinders
            End Get
            Set(ByVal value As String)
                m_Cylinders = value
            End Set
        End Property
        Private m_Cylinders As String
        Public Property Liters() As String
            Get
                Return m_Liters
            End Get
            Set(ByVal value As String)
                m_Liters = value
            End Set
        End Property
        Private m_Liters As String
        Public Property TransmissionType() As String
            Get
                Return m_TransmissionType
            End Get
            Set(ByVal value As String)
                m_TransmissionType = value
            End Set
        End Property
        Private m_TransmissionType As String
        Public Property TransmissionSpeed() As String
            Get
                Return m_TransmissionSpeed
            End Get
            Set(ByVal value As String)
                m_TransmissionSpeed = value
            End Set
        End Property
        Private m_TransmissionSpeed As String
        Public Property FuelType() As String
            Get
                Return m_FuelType
            End Get
            Set(ByVal value As String)
                m_FuelType = value
            End Set
        End Property
        Private m_FuelType As String
    End Class
    Public Class AdditionalInfo
        Public Property quoteID As String
        Public Property NoCheckout() As String
            Get
                Return m_NoCheckout
            End Get
            Set(ByVal value As String)
                m_NoCheckout = value
            End Set
        End Property
        Private m_NoCheckout As String
        Public Property partNo As String

    End Class
    Public Class CKPartType
        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String

        Public Property PartDescGroup() As String
            Get
                Return m_PartDescGroup
            End Get
            Set(ByVal value As String)
                m_PartDescGroup = value
            End Set
        End Property
        Private m_PartDescGroup As String

    End Class
    Public Class OverridePart
        Public Property Override() As Boolean
            Get
                Return m_Override
            End Get
            Set(ByVal value As Boolean)
                m_Override = value
            End Set
        End Property
        Private m_Override As Boolean

        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String

        Public Property OriginalPartNo() As String
            Get
                Return m_OriginalPartNo
            End Get
            Set(ByVal value As String)
                m_OriginalPartNo = value
            End Set
        End Property
        Private m_OriginalPartNo As String

        Public Property OriginalPartVendor() As Long
            Get
                Return m_OriginalPartVendor
            End Get
            Set(ByVal value As Long)
                m_OriginalPartVendor = value
            End Set
        End Property
        Private m_OriginalPartVendor As Long

        Public Property MatchKey() As String
            Get
                Return m_MatchKey
            End Get
            Set(ByVal value As String)
                m_MatchKey = value
            End Set
        End Property
        Private m_MatchKey As String
        Public Property OverridePartNo() As String
            Get
                Return m_OverridePartNo
            End Get
            Set(ByVal value As String)
                m_OverridePartNo = value
            End Set
        End Property
        Private m_OverridePartNo As String

        Public Property OverridePartVendor() As Long
            Get
                Return m_OverridePartVendor
            End Get
            Set(ByVal value As Long)
                m_OverridePartVendor = value
            End Set
        End Property
        Private m_OverridePartVendor As Long



    End Class
    Public Class Warehouses
        Public Property WarehouseID As Long
            Get
                Return m_WarehouseID
            End Get
            Set(ByVal value As Long)
                m_WarehouseID = value
            End Set
        End Property
        Private m_WarehouseID As Long

    End Class
    Public Class ClosestWarehouse
        Public Property WarehouseID As Long
            Get
                Return m_WarehouseID
            End Get
            Set(ByVal value As Long)
                m_WarehouseID = value
            End Set
        End Property
        Private m_WarehouseID As Long
        Public Property Schedule As Long
            Get
                Return m_Schedule
            End Get
            Set(ByVal value As Long)
                m_Schedule = value
            End Set
        End Property
        Private m_Schedule As Long
        Public Property State As String
            Get
                Return m_State
            End Get
            Set(ByVal value As String)
                m_State = value
            End Set
        End Property
        Private m_State As String

    End Class
    Public Shared Function GetCustChargeOEMEOC(ByVal CustNo As String) As Boolean
        Dim boolCharge As Boolean
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT ChargeOEMEOC from tblcompany WHERE customerno = '" & CustNo & "'", conn)
            conn.Open()
            boolCharge = CBool(sqlComm.ExecuteScalar())
        End Using
        Return boolCharge
    End Function


    Public Shared Function GetWarranty(ByVal parttype As PartTypes, ByVal name As String, ByVal client As String)
        Dim warranties As New List(Of Warranty)

        Dim strtier = GetUserTier(name, client)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Select Warranty, Base, Percentage, Flat from tblWarrantyOptions where tblWarrantyOptions.PartType = '" & parttype & "' and tblWarrantyOptions.Tier = '" & strtier & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim warranty As New Warranty
                    warranty.Warranty = r("Warranty")
                    warranty.Base = r("Base")
                    warranty.Percentage = r("Percentage")
                    warranty.Flat = r("Flat")

                    warranties.Add(warranty)
                End While
            End Using
        End Using

        Return warranties
    End Function

    Public Shared Function GetWarrantyMarkup(ByVal sellprice As Decimal, ByVal warranty As String, ByVal parttype As PartTypes, ByVal name As String, ByVal client As String)
        Dim decPercentage, decFlat, decWarranty As Decimal
        Dim boolBase As Boolean
        Dim arrWarranty() = warranty.Split("--")
        Dim strtier = GetUserTier(name, client)

        Dim strFinalWarranty As String
        If (warranty.Contains("$-")) Then
            strFinalWarranty = Trim(arrWarranty(3))
        Else
            strFinalWarranty = Trim(arrWarranty(2))
        End If
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Select base, Percentage, Flat from tblWarrantyOptions Where parttype = '" & parttype & "' and Tier = '" & strtier & "' and warranty='" & strFinalWarranty & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    boolBase = r("Base")
                    decPercentage = r("Percentage")
                    decFlat = r("Flat")
                End While
            End Using
        End Using
        If boolBase = True Then
            decWarranty = 0
        Else
            decWarranty = (sellprice * decPercentage) + decFlat
        End If
        Return decWarranty
    End Function

    Public Shared Function GetClientMarkup(ByVal amount As Decimal, ByVal parttype As String, ByVal client As String) As Decimal
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm3 As New SqlCommand("Select tblTierBasePrice.BasePrice, tblTierBasePrice.Percentage, tblTierBasePrice.Flat from tblTierBasePrice Where PartType = '" & parttype & "' and tblTierBasePrice.TierID = '2'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm3.ExecuteReader
                While r.Read()


                    amount = FormatNumber((amount + amount * Math.Abs(r("Percentage"))) + r("Flat"), 2)


                End While
            End Using
        End Using
        Return amount
    End Function

    Public Shared Function GetTiers(ByVal parttype As String, ByVal name As String, ByVal client As String, ByVal amount As Decimal, ByVal calculating As String, Optional ByVal clientonly As Boolean = False)


        Dim tiers As New List(Of Tier)
        Dim visibletiers As New List(Of VisibleTier)
        Dim tier As New Tier
        'Dim userrole As String = GetUserRole(name, client)
        Dim strtier = GetUserTier(name, client)
        'determine client price
        'If client = "CK" Then 'we already have the final ck base cost
        '    tier.TierID = 2
        '    tier.BasePrice = 0
        '    tier.Percentage = 0
        '    tier.Flat = 0
        '    tier.Price = FormatNumber(amount, 2)
        '    tier.Local = True

        '    If (userrole = "Admin") Then
        '        tier.Label = "Your True Cost*"
        '    ElseIf (userrole = "Customer") Then
        '        tier.Label = ""
        '    End If

        '    tiers.Add(tier)
        'Else 'all other clients start with client costflat and flat
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm3 As New SqlCommand("Select tblTierBasePrice.BasePrice, tblTierBasePrice.Percentage,tblTierBasePrice.CostPercentage, tblTierBasePrice.CostFlat, tblTierBasePrice.Flat, tblTierBasePrice.TierID from tblTierBasePrice Where PartType = '" & parttype & "' and tblTierBasePrice.TierID = '2'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm3.ExecuteReader
                While r.Read()

                    tier.TierID = r("TierID").ToString
                    tier.BasePrice = r("BasePrice").ToString
                    tier.CostPercentage = r("CostPercentage").ToString
                    tier.CostFlat = CDec(r("CostFlat").ToString)
                    tier.Percentage = r("Percentage").ToString
                    tier.Flat = CDec(r("Flat").ToString)
                    Select Case calculating
                        Case "cost"
                            tier.Price = IIf(client = "CK", amount, amount + (amount * tier.CostPercentage) + tier.CostFlat)
                        Case "sell"
                            tier.Price = IIf(client = "CK", amount, amount + (amount * tier.Percentage) + tier.Flat)
                        Case "pretty"
                            tier.Price = amount
                    End Select

                    tier.Local = True

                    If GetUserRole(name, client) = "Admin" Then
                        tier.Label = "Your True Cost*"
                    ElseIf GetUserRole(name, client) = "Customer" Then
                        tier.Label = ""
                    End If

                    tiers.Add(tier)

                End While
            End Using
        End Using
        'End If
        If clientonly = True Then
            Dim vtier As New VisibleTier
            vtier.Label = tier.Label
            vtier.Local = tier.Local
            vtier.Price = tier.Price
            vtier.TierID = tier.TierID
            visibletiers.Add(vtier)
            visibletiers.Sort(Function(v1 As VisibleTier, v2 As VisibleTier) v2.Price.CompareTo(v1.Price))
            Return visibletiers
            Exit Function
        End If


        GetNextTier(tier.Price, tier.TierID, parttype, name, client, tiers, calculating)



        For Each tier In tiers
            Dim vtier As New VisibleTier
            Dim count
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm1 As New SqlCommand("SELECT count(AdditionalTier) From aspnet_Membership INNER JOIN tblTierVisibility on tblTierVisibility.TierID = aspnet_Membership.TierID INNER JOIN aspnet_Users on aspnet_Users.UserId = aspnet_Membership.UserID WHERE aspnet_Users.LoweredUserName = '" & name.ToLower & "' and AdditionalTier = '" & tier.TierID & "'", conn)
                conn.Open()
                count = sqlComm1.ExecuteScalar()
            End Using

            If (count > 0) Or tier.TierID = strtier Then
                vtier.TierID = tier.TierID
                vtier.Price = tier.Price
                vtier.Label = tier.Label
                vtier.Local = tier.Local

                visibletiers.Add(vtier)
            End If
        Next
        visibletiers.Sort(Function(v1 As VisibleTier, v2 As VisibleTier) v2.Price.CompareTo(v1.Price))
        Return visibletiers
    End Function

    Public Shared Function GetNextTier(ByVal amount As Decimal, ByVal basetier As String, ByVal parttype As String, ByVal name As String, ByVal client As String, ByRef tiers As List(Of Tier), ByVal calculating As String)
        Dim currenttiers As New List(Of Tier)
        'Dim userrole As String = GetUserRole(name, client)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            'Using conn As New SqlConnection(ConnectionStrings.GetClientConnectionString())
            Dim sqlComm4 As New SqlCommand("Select tblTiers.Local, tblTierBasePrice.BasePrice, tblTierBasePrice.CostPercentage, tblTierBasePrice.CostFlat, tblTierBasePrice.Percentage, tblTierBasePrice.Flat, tblTiers.TierID, tblTiers.CustomerLabel, tblTiers.AdminLabel from tblTierBasePrice LEFT OUTER JOIN tblTiers on tblTiers.TierID = tblTierBasePrice.TierID Where PartType = '" & parttype & "' and tblTiers.BaseTier = '" & basetier & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm4.ExecuteReader
                    While r.Read()
                        Dim tier As New Tier
                        tier.TierID = r("TierID").ToString
                        tier.BasePrice = r("BasePrice").ToString
                        tier.CostPercentage = r("CostPercentage").ToString
                        tier.CostFlat = r("CostFlat").ToString
                        tier.Percentage = r("Percentage").ToString
                        tier.Flat = r("Flat").ToString
                        Select Case calculating
                            Case "cost" 'TODO: Can be removed-no longer use nor the costflat due to no longer using tier2 for our cost
                                tier.Price = amount + (amount * tier.CostPercentage) + tier.CostFlat
                            Case "sell", "pretty"
                                tier.Price = amount + (amount * tier.Percentage) + tier.Flat
                        End Select
                        If client = "CK" And (tier.TierID = 3 Or tier.TierID = 42) And calculating <> "pretty" Then
                            tier.Price = MyOwnRound(50, tier.Price)
                        End If
                        tier.Local = CType(r("Local"), Boolean)

                        If GetUserRole(name, client) = "Admin" Then
                            tier.Label = r("AdminLabel").ToString
                        ElseIf GetUserRole(name, client) = "Customer" Then
                            tier.Label = r("CustomerLabel").ToString
                        End If

                        tiers.Add(tier)
                        currenttiers.Add(tier)
                    End While
                End Using
            End Using

            For Each tier In currenttiers
            Dim count
            'if count of rows with this tier as basetier is > 0 run this same function
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm2 As New SqlCommand("SELECT COUNT(TierID) FROM tblTiers WHERE BaseTier = '" & tier.TierID & "'", conn)
                conn.Open()
                count = sqlComm2.ExecuteScalar()
            End Using

            If (count > 0) Then
                GetNextTier(tier.Price, tier.TierID, parttype, name, client, tiers, calculating)
            End If
        Next

        Return True
    End Function

    Public Shared Function MyOwnRound(ByVal NumberToRound As Integer,
                            ByVal ValueToRound As Integer) As Integer
        Dim HalfRound As Integer = NumberToRound \ 2
        If NumberToRound < 0 Then
            NumberToRound = Math.Abs(NumberToRound)
        End If
        If ValueToRound Mod NumberToRound > HalfRound Then
            MyOwnRound = ValueToRound + (NumberToRound - (ValueToRound Mod NumberToRound))
        Else
            MyOwnRound = ValueToRound - (ValueToRound Mod NumberToRound)
        End If

        '.99
        If Right(MyOwnRound, 2) = "00" Then MyOwnRound = MyOwnRound - 1
    End Function

    Public Shared Function getClientConnectionStringByCKCustomerNo(ByVal CustomerNo As String) As String
        Dim Client As String
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim isClientQuery = "select Client from tblPigeonClients where CKCustomerNo='" & CustomerNo & "'"
            Dim sqlIsPigeon As New SqlCommand(isClientQuery, conn)
            conn.Open()
            Using reader As SqlDataReader = sqlIsPigeon.ExecuteReader()
                If reader.Read Then
                    Client = reader("Client").ToString()
                Else
                    Client = "CK"
                End If
            End Using
        End Using
        Return Client
    End Function

    Public Shared Function GetCustomerNo(ByVal name As String, ByVal client As String) As String
        Dim strcustno As String = String.Empty
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("select customerno from aspnet_membership inner join aspnet_users on aspnet_users.userid = aspnet_membership.userid where username = '" & name & "'", conn)
            conn.Open()
            Using CustNoR As SqlDataReader = sqlComm.ExecuteReader
                While CustNoR.Read()
                    strcustno = CustNoR("customerno").ToString
                End While
            End Using
        End Using
        Return strcustno
    End Function

    Public Shared Function GetOrderCustomerNo(ByVal orderid As Long, ByVal client As String) As String
        Dim strcustno As String = String.Empty

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String
            If client = "CK" Then
                strsql = "select customerno from tblorder where orderid = " & orderid
            Else
                strsql = "select customerno from tblorders where orderid = " & orderid
            End If
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using CustNoR As SqlDataReader = sqlComm.ExecuteReader
                While CustNoR.Read()
                    strcustno = CustNoR("customerno").ToString
                End While
            End Using
        End Using

        Return strcustno

    End Function

    Public Shared Function GetUserRole(ByVal name As String, ByVal client As String)
        Dim strrole = ""
        If name = Nothing Then
            Return "Customer"
        End If
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Select RoleName FROM aspnet_Roles LEFT OUTER JOIN aspnet_UsersInRoles on aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId INNER JOIN aspnet_Users ON aspnet_Users.UserID = aspnet_UsersInRoles.UserId Where aspnet_Users.LoweredUserName = '" & name.ToLower & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    strrole = r("RoleName").ToString
                End While
            End Using
        End Using
        Return IIf(strrole = Nothing, "Customer", strrole)
    End Function

    Public Shared Function GetUserNameByEmail(ByVal email As String, ByVal strCustomerClient As String) As String
        Dim userName As String
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(strCustomerClient))
            Dim sqlComm As New SqlCommand("select top 1 loweredUserName from aspnet_Users where UserId=(select top 1 UserId from aspnet_Membership where loweredEmail='" & email & "')", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    userName = r("loweredUserName").ToString
                End While
            End Using
        End Using
        Return IIf(userName = Nothing, "", userName)
    End Function
    Public Shared Function GetUserTier(ByVal name As String, ByVal client As String) As Long
        Dim intTier As Long = 0
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT TierID FROM aspnet_Membership INNER JOIN aspnet_Users on aspnet_Users.UserId = aspnet_Membership.UserId WHERE aspnet_Users.LoweredUserName = '" & name.ToLower & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    intTier = CInt(r("TierID").ToString)
                End While
            End Using
        End Using
        Return intTier
    End Function


    Public Shared Function getVisibleTiers(Email As String, client As String, ListOfTiers As List(Of VisibleTier), tierID As String) As List(Of VisibleTier)
        ListOfTiers.RemoveAll(Function(a) Not (Convert.ToInt32(a.TierID) = Convert.ToInt32(TierTypes.List) Or Convert.ToInt32(a.TierID) = Convert.ToInt32(TierTypes.Client) Or Convert.ToInt32(a.TierID) = tierID))
        Return ListOfTiers
    End Function

    Public Shared Function GetHotBuildFee(ByVal name As String, ByVal parttype As String, ByVal client As String) As Decimal

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT hotbuild from tbltierbaseprice where parttype ='" & parttype & "' and tierid= '" & GetUserTier(name, client) & "'", conn)
            conn.Open()
            GetHotBuildFee = sqlComm.ExecuteScalar
        End Using

    End Function


    Public Shared Function GetCustomerState(ByVal custno As String, ByVal customer As String)
        Dim strState = ""
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(customer))
            Dim sqlComm As New SqlCommand("SELECT state from tblcompany where customerno =" & custno.fqq, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    strState = r("State").ToString
                End While
            End Using
        End Using
        Return strState
    End Function

    Public Shared Function GetCustomerTier(ByVal custno As String, ByVal client As String)
        Dim strTierID = ""
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT top 1 tierid from aspnet_membership where isapproved=1 and islockedout=0 and customerno =" & custno.fqq, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    strTierID = r("TierID").ToString
                End While
            End Using
        End Using
        Return strTierID
    End Function

    Public Shared Function GetUserEmail(ByVal name As String, ByVal strClient As String)
        Dim stremail = ""
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(strClient))
            Dim sqlComm As New SqlCommand("SELECT email FROM aspnet_Membership INNER JOIN aspnet_Users on aspnet_Users.UserId = aspnet_Membership.UserId WHERE aspnet_Users.LoweredUserName = '" & name.ToLower & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    stremail = r("email").ToString
                End While
            End Using
        End Using
        Return stremail
    End Function

    Public Shared Function GetWarehouseAutonation(ByVal warehouseid As String)
        Dim boolAutonation As Boolean = False
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT autonation from tblremanwarehouses where active=1 and id = " & warehouseid.fqq, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    boolAutonation = CBool(r("Autonation"))
                End While
            End Using
        End Using
        Return boolAutonation
    End Function
    Public Shared Function GetWarehouseClient(ByVal warehouseid As String)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
            Dim sqlComm As New SqlCommand("SELECT client from tblremanwarehouses where active=1 and id = " & warehouseid.fqq, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Return r("client")
                End While
            End Using
        End Using

    End Function

    Public Shared Function GetWarehouseDeliveryToState(ByVal warehouseid As Long, ByVal state As String)
        Dim intTransit As Long
        'now get schedule for that warehouse
        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT Schedule from tblRemanSchedule where Warehouse = " & warehouseid & " and (state = " & state.fqq & " or abbreviation = " & state.fqq & ")", conn2)
            conn2.Open()
            Using r = sqlComm.ExecuteReader()
                While r.Read()
                    intTransit = (CDec(Left(r("Schedule"), 1)) + CDec(Right(r("Schedule"), 1))) / 2
                End While
            End Using
        End Using
        Return intTransit
    End Function
    Public Shared Function GetWarehouseState(ByVal warehouseid As Long)


        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT State from tblRemanwarehouses where active=1 and id = " & warehouseid, conn2)
            conn2.Open()
            Using r = sqlComm.ExecuteReader()
                While r.Read()
                    Return UCase(r("State").ToString())
                End While
            End Using
        End Using

    End Function
    Public Shared Function GetMasterClient(ByVal masterclientid As String)
        Dim strMasterClient
        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT client from tblpigeonclients where clientid = '" & masterclientid & "'", conn2)
            conn2.Open()
            strMasterClient = sqlComm.ExecuteScalar
        End Using
        Return strMasterClient
    End Function

    Public Shared Function GetOrderUsername(ByVal orderid As String, ByVal client As String)
        Dim strUsername
        Using conn2 As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT username from tblorders where orderid = '" & orderid & "'", conn2)
            conn2.Open()
            strUsername = sqlComm.ExecuteScalar
        End Using
        Return strUsername
    End Function
    Public Shared Function GetMapData(ByVal partnumber As String, ByVal parttype As String, ByVal client As String, ByVal checklocal As Boolean, ByVal checkothers As Boolean, ByVal checksupplier As Boolean, ByVal vendor As Long, Optional ByVal ExcludeWarehouse As List(Of Warehouses) = Nothing, Optional ByVal ExcludeTracyWarehouses As Boolean = False, Optional ByVal NonAutoNationOnly As Boolean = False)
        Dim list As New List(Of TheMapData)

        Dim intLocalStock As Integer

        Select Case parttype
            Case 6, 7, 8, 9
                parttype = 3

        End Select

        '***************step 1 get local warehouse
        If checklocal = True Then

            'get all warehouses for that client pull
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommWarehouses As New SqlCommand("Select id, client from tblremanwarehouses where client = '" & client & "' and active=1", conn)
                conn.Open()
                Using rWarehouses = sqlCommWarehouses.ExecuteReader()
                    While rWarehouses.Read()

                        'see if that warehouse has the part in stock
                        Using conn2 As New SqlConnection(GetClientConnectionString(client))
                            Dim sqlCommInStock As New SqlCommand("SELECT count(Part) from tblStock WHERE part = '" & partnumber & "' AND type = " & parttype & " AND Arrive IS NOT NULL AND ReturnType IS NULL  and ckorderid is null and warehouseid = '" & rWarehouses("id") & "'", conn2)
                            conn2.Open()
                            intLocalStock = sqlCommInStock.ExecuteScalar
                        End Using

                        If intLocalStock > 0 Then 'part is at that location now get delivery schedule

                            'now get schedule for that warehouse
                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = " & rWarehouses("id"), conn2)
                                conn2.Open()
                                Using r = sqlComm.ExecuteReader()
                                    While r.Read()
                                        Dim M1 As New TheMapData
                                        M1.State = r("State")
                                        M1.Schedule = r("Schedule")
                                        M1.Abbreviation = r("Abbreviation")
                                        M1.Value = r("Schedule")
                                        M1.Warehouse = rWarehouses("id")
                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                        End If

                                    End While
                                End Using
                            End Using
                        End If
                    End While
                End Using
            End Using

        End If
        '***************step 2 check all other warehouse
        If checkothers = True Then

            'get all warehouses for that client pull
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim strSqlWarehouses As String

                strSqlWarehouses = "Select id, client from tblremanwarehouses where client <> '" & client & "' and client is not null and active=1"
                If ExcludeTracyWarehouses = True Then strSqlWarehouses = strSqlWarehouses & " and client <> 'Tracy'"
                If NonAutoNationOnly = True Then strSqlWarehouses = strSqlWarehouses & " and autonation <> 1"

                Dim sqlCommWarehouses As New SqlCommand(strSqlWarehouses, conn)
                conn.Open()
                Using rWarehouses As SqlDataReader = sqlCommWarehouses.ExecuteReader()
                    While rWarehouses.Read()

                        'see if that warehouse has the part in stock
                        Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(rWarehouses("client")))
                            Dim sqlCommInStock As New SqlCommand("SELECT count(Part) from tblStock WHERE part = '" & partnumber & "' AND type = " & parttype & " AND Arrive IS NOT NULL AND ReturnType IS NULL  and ckorderid is null and warehouseid = '" & rWarehouses("id") & "'", conn2)
                            conn2.Open()
                            intLocalStock = sqlCommInStock.ExecuteScalar
                        End Using

                        If intLocalStock > 0 Then 'part is at that location now get delivery schedule

                            'now get schedule for that warehouse
                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = " & rWarehouses("id"), conn2)
                                conn2.Open()
                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                    While r.Read()
                                        Dim M1 As New TheMapData
                                        M1.State = r("State")
                                        M1.Schedule = r("Schedule")
                                        M1.Abbreviation = r("Abbreviation")
                                        M1.Value = r("Schedule")
                                        M1.Warehouse = rWarehouses("id")

                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                        End If

                                    End While
                                End Using
                            End Using
                        End If
                    End While
                End Using
            End Using

        End If


        '***************step 3, see if supplier has it
        Dim certifiedResult As New DataSet
        If checksupplier = True Then
            Select Case parttype

                Case 1
                    If vendor = 78 Then 'certified


                        Try
                            Dim inventorySearch As CertifiedLookup.Lookup = New CertifiedLookup.Lookup()
                            certifiedResult = inventorySearch.GetInventoryCTR("B4RY=O=DXHVNEU=8ZDKMJD5YK1TU65ZEA=UP8DW5KM2O0==C4L", partnumber)

                        Catch Ex As WebException

                        End Try

                        Dim intSupplierInvetory As Long = 0

                        If certifiedResult.Tables.Count > 0 Then

                            Try
                                Dim ows As OrderWebService = New OrderWebService()
                                ows.SetCertifiedWarnings(partnumber)
                            Catch ex As Exception
                                ex.wrangle()
                            End Try



                            'certified has it, get the warnings if any exist for the part
                            'Dim certifiedWarningsClient As CertifiedLookup.Lookup = New CertifiedLookup.Lookup()
                            'Dim CertifiedWarningsResult = certifiedWarningsClient.GetWarnings(BaseApplicationVariables.CertifiedUKey, partnumber)


                            For Each row As DataRow In certifiedResult.Tables(0).Rows
                                Debug.WriteLine(row("uLocation"))
                                Debug.WriteLine(row("uInventory"))


                                If row("uLocation") = "OMA" And row("uInventory") > 0 Then 'omaha has it
                                    intSupplierInvetory = intSupplierInvetory + CInt(row("uInventory"))
                                    'now get schedule for that warehouse
                                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                        Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 10", conn)
                                        conn.Open()
                                        Using r As SqlDataReader = sqlComm.ExecuteReader()
                                            While r.Read()
                                                Dim M1 As New TheMapData
                                                M1.State = r("State")
                                                M1.Schedule = r("Schedule")
                                                M1.Abbreviation = r("Abbreviation")
                                                M1.Value = r("Schedule")
                                                M1.Warehouse = 10

                                                If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                    list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                End If

                                            End While
                                        End Using
                                    End Using
                                End If

                                If row("uLocation") = "FTD" And row("uInventory") > 0 Then 'fort dodge has it
                                    intSupplierInvetory = intSupplierInvetory + CInt(row("uInventory"))
                                    'now get schedule for that warehouse
                                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                        Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 11", conn)
                                        conn.Open()
                                        Using r As SqlDataReader = sqlComm.ExecuteReader()
                                            While r.Read()
                                                Dim M1 As New TheMapData
                                                M1.State = r("State")
                                                M1.Schedule = r("Schedule")
                                                M1.Abbreviation = r("Abbreviation")
                                                M1.Value = r("Schedule")
                                                M1.Warehouse = 11

                                                If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                    list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                End If

                                            End While
                                        End Using
                                    End Using
                                End If

                                If row("uLocation") = "PHX-CTR" And row("uInventory") > 0 Then 'phoenix has it
                                    intSupplierInvetory = intSupplierInvetory + CInt(row("uInventory"))
                                    'now get schedule for that warehouse
                                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                        Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 12", conn)
                                        conn.Open()
                                        Using r As SqlDataReader = sqlComm.ExecuteReader()
                                            While r.Read()
                                                Dim M1 As New TheMapData
                                                M1.State = r("State")
                                                M1.Schedule = r("Schedule")
                                                M1.Abbreviation = r("Abbreviation")
                                                M1.Value = r("Schedule")
                                                M1.Warehouse = 12

                                                If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                    list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                End If

                                            End While
                                        End Using
                                    End Using
                                End If

                                If row("uLocation") = "PUB-RDU" And row("uInventory") > 0 Then 'Raleigh has it
                                    intSupplierInvetory = intSupplierInvetory + CInt(row("uInventory"))
                                    'now get schedule for that warehouse
                                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                        Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 29", conn)
                                        conn.Open()
                                        Using r As SqlDataReader = sqlComm.ExecuteReader()
                                            While r.Read()
                                                Dim M1 As New TheMapData
                                                M1.State = r("State")
                                                M1.Schedule = r("Schedule")
                                                M1.Abbreviation = r("Abbreviation")
                                                M1.Value = r("Schedule")
                                                M1.Warehouse = 29

                                                If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                    list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                End If

                                            End While
                                        End Using
                                    End Using
                                End If
                            Next row
                        End If
                        'supplier didnt have it all so will be a true hot build...add 2 days to normal delivery
                        If intSupplierInvetory = 0 Then
                            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 10", conn)
                                conn.Open()
                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                    While r.Read()
                                        Dim M1 As New TheMapData
                                        M1.State = r("State")
                                        M1.Schedule = r("Schedule") + 2
                                        M1.Abbreviation = r("Abbreviation")
                                        M1.Value = r("Schedule") + 2
                                        M1.Warehouse = 10

                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                        End If

                                    End While
                                End Using
                            End Using
                        End If

                    ElseIf vendor = 2209 Then 'ete
                        Dim intSupplierInvetory As Long = 0
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlComm6 As New SqlCommand("select isnull(mke,0) as mke, isnull(ont,0) as ont, isnull(abe,0) as abe, isnull(iah,0) as iah, isnull(mco,0) as mco, isnull(sea,0) as sea from tblETEStock where partno = '" & partnumber & "'", conn)
                            conn.Open()
                            Using rETE As SqlDataReader = sqlComm6.ExecuteReader
                                While rETE.Read()
                                    If rETE("MKE") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 22) = False Then
                                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 22", conn2)
                                            conn2.Open()
                                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                While r.Read()
                                                    Dim M1 As New TheMapData
                                                    M1.State = r("State")
                                                    M1.Schedule = r("Schedule")
                                                    M1.Abbreviation = r("Abbreviation")
                                                    M1.Value = r("Schedule")
                                                    M1.Warehouse = 22

                                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                    End If

                                                End While
                                            End Using
                                        End Using
                                    End If

                                    If rETE("abe") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 3) = False Then
                                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 3", conn2)
                                            conn2.Open()
                                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                While r.Read()
                                                    Dim M1 As New TheMapData
                                                    M1.State = r("State")
                                                    M1.Schedule = r("Schedule")
                                                    M1.Abbreviation = r("Abbreviation")
                                                    M1.Value = r("Schedule")
                                                    M1.Warehouse = 26

                                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                    End If

                                                End While
                                            End Using
                                        End Using
                                    End If

                                    If rETE("ONT") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 18) = False Then
                                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 18", conn2)
                                            conn2.Open()
                                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                While r.Read()
                                                    Dim M1 As New TheMapData
                                                    M1.State = r("State")
                                                    M1.Schedule = r("Schedule")
                                                    M1.Abbreviation = r("Abbreviation")
                                                    M1.Value = r("Schedule")
                                                    M1.Warehouse = 18

                                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                    End If

                                                End While
                                            End Using
                                        End Using
                                    End If

                                    If rETE("IAH") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 5) = False Then
                                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 5", conn2)
                                            conn2.Open()
                                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                While r.Read()
                                                    Dim M1 As New TheMapData
                                                    M1.State = r("State")
                                                    M1.Schedule = r("Schedule")
                                                    M1.Abbreviation = r("Abbreviation")
                                                    M1.Value = r("Schedule")
                                                    M1.Warehouse = 1

                                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                    End If

                                                End While
                                            End Using
                                        End Using
                                    End If

                                    If rETE("MCO") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 35) = False Then
                                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 35", conn2)
                                            conn2.Open()
                                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                While r.Read()
                                                    Dim M1 As New TheMapData
                                                    M1.State = r("State")
                                                    M1.Schedule = r("Schedule")
                                                    M1.Abbreviation = r("Abbreviation")
                                                    M1.Value = r("Schedule")
                                                    M1.Warehouse = 23

                                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                    End If

                                                End While
                                            End Using
                                        End Using
                                    End If

                                    If rETE("SEA") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 8) = False Then
                                        Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 8", conn2)
                                            conn2.Open()
                                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                While r.Read()
                                                    Dim M1 As New TheMapData
                                                    M1.State = r("State")
                                                    M1.Schedule = r("Schedule")
                                                    M1.Abbreviation = r("Abbreviation")
                                                    M1.Value = r("Schedule")
                                                    M1.Warehouse = 25

                                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                    End If

                                                End While
                                            End Using
                                        End Using
                                    End If
                                    intSupplierInvetory = rETE("MKE") + rETE("ABE") + rETE("ONT") + rETE("IAH") + rETE("MCO") + rETE("SEA")
                                End While
                            End Using
                        End Using
                        'supplier didnt have it all so will be a true hot build...add 2 days to normal delivery
                        If intSupplierInvetory = 0 Then
                            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 22", conn)
                                conn.Open()
                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                    While r.Read()
                                        Dim M1 As New TheMapData
                                        M1.State = r("State")
                                        M1.Schedule = r("Schedule") + 2
                                        M1.Abbreviation = r("Abbreviation")
                                        M1.Value = r("Schedule") + 2
                                        M1.Warehouse = 10

                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                        End If

                                    End While
                                End Using
                            End Using
                        End If
                    ElseIf vendor = 13095 Then 'ZF
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlComm6 As New SqlCommand("select isnull(stock,0) as stock from tblZFStock where replace(partno,' ', '-') = '" & partnumber & "'", conn)
                            conn.Open()
                            Using rZF As SqlDataReader = sqlComm6.ExecuteReader
                                If rZF.HasRows Then
                                    While rZF.Read()
                                        'now get schedule for all atk warehouses
                                        If FindWarehouseExclusion(ExcludeWarehouse, 24) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 24", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 24

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If
                                    End While
                                Else
                                    'supplier didnt have it all so will be a true hot build...add 6 days to normal delivery
                                    Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                        Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 24", conn2)
                                        conn2.Open()
                                        Using r As SqlDataReader = sqlComm.ExecuteReader()
                                            While r.Read()
                                                Dim M1 As New TheMapData
                                                M1.State = r("State")
                                                M1.Schedule = r("Schedule") + 6
                                                M1.Abbreviation = r("Abbreviation")
                                                M1.Value = r("Schedule") + 6
                                                M1.Warehouse = 24

                                                If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                    list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                End If

                                            End While
                                        End Using
                                    End Using
                                End If
                            End Using
                        End Using
                    End If
                Case 2
                    If vendor = 75 Then 'ATK
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sb As New System.Text.StringBuilder()

                            sb.Append("select  *, convert(int,[CrystalRiver]) +convert(int,[Topeka]) +convert(int,[Bellingham]) +convert(int,[LasVegas]) ")
                            sb.Append(" + convert(int,[Stockton]) +convert(int,[Atlanta]) +convert(int,[Indiana]) +Convert(Int, [Massachusetts]) +convert(int,[SantaFeSprings]) + ")
                            sb.Append(" Convert(Int, missouri) +convert(int,dallas)+convert(int,jacksonville)+convert(int,pennsylvania) + Convert(Int, washington)+convert(int,ohio)+convert(int,phoenix) As quantity2 ")
                            sb.Append(" from tblATKStock Where Part = '" & partnumber & "'")

                            'Dim sqlComm6 As New SqlCommand("select  *, convert(int,missouri) +convert(int,dallas)+convert(int,jacksonville)+convert(int,pennsylvania)+convert(int,washington)+convert(int,ohio)+convert(int,phoenix) as quantity2 from tblATKStock where part = '" & partnumber & "'", conn)
                            Dim sqlComm6 As New SqlCommand(sb.ToString(), conn)

                            conn.Open()
                            Using rATK As SqlDataReader = sqlComm6.ExecuteReader
                                While rATK.Read()
                                    If rATK("quantity2") > 0 Then
                                        'now get schedule for all atk warehouses

                                        If rATK("Missouri") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 7) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 7", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 7

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Dallas") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 1) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 1", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 1

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Jacksonville") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 4) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 4", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 4

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Pennsylvania") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 3) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 3", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 3

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Washington") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 8) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 8", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 8

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Ohio") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 6) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 6", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 6

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Phoenix") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 2) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 2", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 2

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("CrystalRiver") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 36) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 36", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 36

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Topeka") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 37) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 37", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 37

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If


                                        If rATK("Bellingham") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 8) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 8", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 8

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("LasVegas") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 38) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 38", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 38

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Stockton") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 39) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 39", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 39

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Atlanta") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 40) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 40", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 40

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Indiana") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 41) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 41", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 41

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rATK("Massachusetts") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 42) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 42", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 42

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If



                                        If rATK("SantaFeSprings") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 43) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 43", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 43

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If
                                    Else
                                        'no part at all
                                    End If
                                End While
                            End Using
                        End Using

                    End If

                    If vendor = 13404 Then 'Pilot
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlComm6 As New SqlCommand("select  pilot1, pilot2 , pilot1 +pilot2 as quantity from tblPilotStock where part = '" & partnumber & "'", conn)
                            conn.Open()
                            Using rPilot As SqlDataReader = sqlComm6.ExecuteReader
                                While rPilot.Read()
                                    If rPilot("quantity") > 0 Then
                                        'now get schedule for all pilot warehouses

                                        If rPilot("Pilot1") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 27) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 27", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 27

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If

                                        If rPilot("Pilot2") > 0 And FindWarehouseExclusion(ExcludeWarehouse, 28) = False Then
                                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                                Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 28", conn2)
                                                conn2.Open()
                                                Using r As SqlDataReader = sqlComm.ExecuteReader()
                                                    While r.Read()
                                                        Dim M1 As New TheMapData
                                                        M1.State = r("State")
                                                        M1.Schedule = r("Schedule")
                                                        M1.Abbreviation = r("Abbreviation")
                                                        M1.Value = r("Schedule")
                                                        M1.Warehouse = 28

                                                        If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                                            list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                                        End If

                                                    End While
                                                End Using
                                            End Using
                                        End If
                                    End If
                                End While
                            End Using
                        End Using
                    End If
                Case 4

                    If vendor = 91 Then 'zumbrota
                        'eventually we will be able to check zumbrota stock live, for now pad 2 days to their normal delivery schedule since we dont know if its a hot build or not
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 14", conn)
                            conn.Open()
                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                While r.Read()
                                    Dim M1 As New TheMapData
                                    M1.State = r("State")
                                    M1.Schedule = r("Schedule")
                                    M1.Abbreviation = r("Abbreviation")
                                    M1.Value = r("Schedule")
                                    M1.Warehouse = 14

                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                    End If

                                End While
                            End Using
                        End Using
                    End If
                Case 3, 6, 7, 8, 9
                    If vendor = 91 Then 'zumbrota
                        'eventually we will be able to check zumbrota stock live, for now pad 2 days to their normal delivery schedule since we dont know if its a hot build or not
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 14", conn)
                            conn.Open()
                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                While r.Read()
                                    Dim M1 As New TheMapData
                                    M1.State = r("State")
                                    M1.Schedule = r("Schedule")
                                    M1.Abbreviation = r("Abbreviation")
                                    M1.Value = r("Schedule")
                                    M1.Warehouse = 14

                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                    End If

                                End While
                            End Using
                        End Using
                    End If
                Case 10
                    If vendor = 91 Then 'zumbrota
                        'eventually we will be able to check zumbrota stock live, for now pad 2 days to their normal delivery schedule since we dont know if its a hot build or not
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlComm As New SqlCommand("SELECT State, Schedule, Abbreviation from tblRemanSchedule where Warehouse = 14", conn)
                            conn.Open()
                            Using r As SqlDataReader = sqlComm.ExecuteReader()
                                While r.Read()
                                    Dim M1 As New TheMapData
                                    M1.State = r("State")
                                    M1.Schedule = r("Schedule")
                                    M1.Abbreviation = r("Abbreviation")
                                    M1.Value = r("Schedule")
                                    M1.Warehouse = 14

                                    If FindState(list, M1.State, M1.Value, M1.Warehouse) = False Then
                                        list.Add(M1) 'we didnt find a faster delivery for this state so add this one
                                    End If

                                End While
                            End Using
                        End Using
                    End If
            End Select


        End If

        Return list

    End Function

    Public Shared Function FindState(ByRef mp As List(Of TheMapData), ByVal state As String, ByVal value As Long, ByVal warehouse As Long) As Boolean

        'see if state is already there
        For Each listingresult In mp
            If listingresult.State = state Then 'found

                If listingresult.Value < value Then 'we previously found faster delivery
                    Return True
                    Exit Function
                Else
                    'update previous one
                    listingresult.Value = value
                    listingresult.Schedule = value
                    listingresult.Warehouse = warehouse
                    Return True
                    Exit Function
                End If
            End If
        Next
        Return False
    End Function
    Public Shared Function FindStateTransit(ByVal mp As List(Of TheMapData), ByVal state As String) As String
        state = Trim(state)
        'see if state is already there
        For Each listingresult In mp
            If LCase(listingresult.State) = LCase(state) Or LCase(listingresult.Abbreviation) = LCase(state) Then 'found

                Return listingresult.Value
            End If
        Next
        Return "N/A"
    End Function
    Public Shared Function GetExpArrivalDate(ByVal part As String, ByVal parttype As String, ByVal state As String, ByVal client As String, ByVal vendor As Long) As DateTime
        Dim intTransit
        Dim dtExpArrival As DateTime
        Dim override As New OverridePart

        override = VendorOverride(part, parttype, vendor)
        If client = "CK" And override.Override = True Then
            intTransit = FindStateTransit(GetMapData(part, parttype, client, False, False, False, vendor), state)
        Else
            intTransit = FindStateTransit(GetMapData(part, parttype, client, True, True, True, vendor), state)

        End If
        If intTransit = "N/A" Then 'pushing out to 14 days if not in stock and needs to be built
            dtExpArrival = DateAdd(DateInterval.Day, 14, Now())
            Return dtExpArrival
        End If

        If DateTime.Now.TimeOfDay.ToString > "16:00" Then
            dtExpArrival = DateAdd(DateInterval.Day, intTransit + 1, Now())
        Else
            dtExpArrival = DateAdd(DateInterval.Day, intTransit, Now())
        End If

        'check for weekend
        Dim FromDate = Now()
        Dim ToDate = dtExpArrival


        Dim TotalDaysInt As Integer = ToDate.Date.Subtract(FromDate.Date).Days
        Dim WeeksInt As Integer = Math.Floor(TotalDaysInt / 7)
        Dim WeekendDaysInt As Integer = WeeksInt * 2

        If TotalDaysInt Mod 7 <> 0 Then
            If FromDate.DayOfWeek = DayOfWeek.Sunday Then
                WeekendDaysInt += 1
            Else
                Dim SaturdayOffsetInt = Math.Max((TotalDaysInt Mod 7) - (DayOfWeek.Saturday - FromDate.DayOfWeek), 0)
                WeekendDaysInt += Math.Min(SaturdayOffsetInt, 2)
            End If
        End If

        If WeekendDaysInt Mod 2 = 0 Then
            dtExpArrival = DateAdd(DateInterval.Day, WeekendDaysInt, dtExpArrival)
        Else
            dtExpArrival = DateAdd(DateInterval.Day, WeekendDaysInt + 1, dtExpArrival)

        End If

        If dtExpArrival.DayOfWeek = DayOfWeek.Saturday Then
            dtExpArrival = DateAdd(DateInterval.Day, 2, dtExpArrival)
        ElseIf dtExpArrival.DayOfWeek = DayOfWeek.Sunday Then
            dtExpArrival = DateAdd(DateInterval.Day, 1, dtExpArrival)
        End If

        Return dtExpArrival
    End Function

    Public Shared Function FindClosestWarehouse(ByVal mp As List(Of TheMapData), ByVal state As String) As ClosestWarehouse
        Dim c As New ClosestWarehouse
        state = Trim(state)
        'see if state is already there
        For Each listingresult In mp
            If LCase(listingresult.State) = LCase(state) Or LCase(listingresult.Abbreviation) = LCase(state) Then 'found
                c.WarehouseID = listingresult.Warehouse
                c.Schedule = listingresult.Schedule
                c.State = GetWarehouseState(listingresult.Warehouse)
                Return c
            End If
        Next
        Return c
    End Function


    Public Shared Function GetMakeExclusions(ByVal client As String, ByVal customerno As String)
        Dim strsql As String
        Dim lstMake As New List(Of ExcludeMakes)
        Using conn As New SqlConnection(GetClientConnectionString(client))
            strsql = "select make from tblcompanymake where customerno = '" & customerno & "'"
            Dim sqlcomm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlcomm.ExecuteReader
                While r.Read

                    Select Case r("make").ToString
                        Case "GM"
                            Dim e1 As New ExcludeMakes
                            e1.Make = "Chevrolet"
                            lstMake.Add(e1)
                            Dim e2 As New ExcludeMakes
                            e2.Make = "Buick"
                            lstMake.Add(e2)
                            Dim e3 As New ExcludeMakes
                            e3.Make = "Cadillac"
                            lstMake.Add(e3)
                            Dim e4 As New ExcludeMakes
                            e4.Make = "Oldsmobile"
                            lstMake.Add(e4)
                            Dim e5 As New ExcludeMakes
                            e5.Make = "Pontiac"
                            lstMake.Add(e5)
                            Dim e6 As New ExcludeMakes
                            e6.Make = "GMC"
                            lstMake.Add(e6)
                            Dim e7 As New ExcludeMakes
                            e7.Make = "Saturn"
                            lstMake.Add(e7)
                            Dim e8 As New ExcludeMakes
                            e8.Make = "Hummer"
                            lstMake.Add(e8)

                        Case "Chrysler"
                            Dim e9 As New ExcludeMakes
                            e9.Make = "Chrysler"
                            lstMake.Add(e9)
                            Dim e10 As New ExcludeMakes
                            e10.Make = "Dodge"
                            lstMake.Add(e10)
                            Dim e11 As New ExcludeMakes
                            e11.Make = "Jeep"
                            lstMake.Add(e11)
                            Dim e12 As New ExcludeMakes
                            e12.Make = "Plymouth"
                            lstMake.Add(e12)
                        Case "Ford"
                            Dim e13 As New ExcludeMakes
                            e13.Make = "Ford"
                            lstMake.Add(e13)
                            Dim e14 As New ExcludeMakes
                            e14.Make = "Lincoln"
                            lstMake.Add(e14)
                            Dim e15 As New ExcludeMakes
                            e15.Make = "Mercury"
                            lstMake.Add(e15)
                        Case Else
                            Dim e16 As New ExcludeMakes
                            e16.Make = r("make").ToString
                            lstMake.Add(e16)

                    End Select
                End While
            End Using
        End Using

        Return lstMake
    End Function


    Public Shared Function FindWarehouseExclusion(ByVal w As List(Of Warehouses), ByVal warehouseid As Long) As Boolean

        If w Is Nothing Then
            Return False
            Exit Function
        End If

        'see if state is already there
        For Each listingresult In w
            If listingresult.WarehouseID = warehouseid Then 'found
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    Public Shared Function ProcessOrder(ByVal source As String, ByVal parttype As String, ByVal Parts As List(Of Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal VIN As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal client As String, ByVal IP As String, ByVal customernumberbehalf As String, ByVal vendor As String, ByVal subtype As String, ByVal notes As String, ByVal email As String, ByVal quoteID As String, ByVal emailItems As String)
        Dim strPartType As String = String.Empty
        Dim strcompany As String = String.Empty
        Dim stremail As String = String.Empty
        Dim strphone As String = String.Empty
        Dim straddress As String = String.Empty
        Dim strcustno As String = String.Empty
        Dim strPartDescGroup As String = String.Empty
        Dim intCKPartID As Long
        GetDefaults(client)

        'get year, make, model if vin only supplied
        If String.IsNullOrEmpty(year) Or String.IsNullOrEmpty(make) Or String.IsNullOrEmpty(model) Then
            Dim vehicle As VINResult = GetVehicleFromVIN(Trim(VIN))
            year = vehicle.Year
            make = vehicle.Make
            model = vehicle.Model
        End If


        Select Case parttype
            Case 1
                strPartType = "Transmission"
                strPartDescGroup = If(subtype = "Reman", "Reman Transmission", "Builder Transmission")

            Case 2
                strPartType = "Engine"
                strPartDescGroup = "Reman Engine"

            Case 3, 6, 7, 8, 9
                strPartType = "Differential"
                strPartDescGroup = "Reman Differential"
            Case 4
                strPartType = "Transfer Case"
                strPartDescGroup = "Reman Transfer Case"
            Case 10
                strPartType = "Manual Transmission"
                strPartDescGroup = "Manual Transmission"
            Case 5
        End Select

        If source = "customer" Then
            clientemails = "none@nowhere.com,"
            'get salesman email, customername
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo, dbo.tblCompany.SalesmanEmail, dbo.tblCompany.Company, dbo.tblCompany.Phone,  dbo.tblCompany.Address + ',' + dbo.tblCompany.City + N',' + dbo.tblCompany.State + N' ' + dbo.tblCompany.Zip AS Address, dbo.aspnet_Membership.Email, dbo.tblCompany.Autonation FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN dbo.tblCompany ON dbo.aspnet_Membership.CustomerNo = dbo.tblCompany.CustomerNo where username = '" & name & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        If IsDBNull(r("salesmanemail").ToString) = False And r("salesmanemail").ToString <> "" Then clientemails = r("salesmanemail").ToString
                        strcompany = r("company").ToString
                        strphone = r("phone").ToString
                        straddress = r("address").ToString
                        stremail = IIf(client = "FMP", email, r("email").ToString)
                        strcustno = r("CustomerNo").ToString

                    End While
                End Using
            End Using



            'split emails if any

            Dim emailtocc() As String
            emailtocc = clientemails.Split(",")
#If DEBUG Then
            Dim mm As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "andrew_rand@ckautoparts.com")
            mm.CC.Add("james_obrien@ckautoparts.com")
#Else
            Dim mm As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", emailtocc(0))

#End If
            Select Case parttype
                Case 1
                    mm.Subject = clientwebsitename & " Transmission Order Received"

                Case 2
                    mm.Subject = clientwebsitename & " Engine Order Received"

                Case 3, 6, 7, 8, 9
                    mm.Subject = clientwebsitename & " Differential Order Received"

                Case 4
                    mm.Subject = clientwebsitename & " Transfer Case Order Received"
                Case 10
                    mm.Subject = clientwebsitename & " Manual Transmission Order Received"
                Case 5
            End Select
            Dim strbody As String = String.Empty

            strbody &= "Company: " & strcompany & "<br/>"
            strbody &= "Customer No: " & strcustno & "<br/>"
            strbody &= "Address: " & straddress & "<br/>"
            strbody &= "User: " & name & "<br/>"
            strbody &= "Email: " & stremail & "<br/>"


            strbody &= "Phone: " & strphone & "<br/><br/>"


            strbody &= "Vehicle: " & year & " " & make & " " & model & "<br/>"
            strbody &= "VIN: " & VIN & "<br/>"
            For Each thepart As Parts In Parts
                If parttype = 3 Or parttype = 6 Or parttype = 7 Or parttype = 8 Or parttype = 9 Then parttype = IIf(GetDiffPosition(thepart.PartNumber) = 22, Convert.ToInt32(PartTypes.FrontDiff), Convert.ToInt32(PartTypes.RearDiff))
                thepart.PartNumber = thepart.PartNumber.Replace("Part #: ", Nothing)
                strbody &= "Part No:" & thepart.PartNumber & "<br/>"
                strbody &= "Cost Price: " & FormatCurrency(GetClientCost(thepart.PartNumber, parttype, IIf(thepart.Vendor = 0, vendor, thepart.Vendor), client), 2) & "<br/>"
                strbody &= "List Price: " & FormatCurrency(GetClientList(thepart.PartNumber, parttype, IIf(thepart.Vendor = 0, vendor, thepart.Vendor), client), 2) & "<br/>"
                strbody &= "Sell Price: " & FormatCurrency(thepart.SalePrice, 2) & "<br/>"
                strbody &= "Core Price: " & FormatCurrency(thepart.CorePrice.Replace("Core:", Nothing), 2) & "<br/><br/>"

                strbody &= "Warranty: " & warranty & "<br/>"
            Next
            strbody &= "PO: " & PO & "<br/>"
            strbody &= "Customer Last Name: " & Customer & "<br/>"
            strbody &= "Mileage: " & Mileage & "<br/>"
            strbody &= "Repair Facility: " & RepairFacility & "<br/>"
            strbody &= "Contact Info: <br/>"
            strbody &= Contact & "<br/>"
            strbody &= Address & "<br/>"
            strbody &= City & "," & State & " " & Zip & "<br/>"
            strbody &= Phone & "<br/><br/>"
            strbody &= "Notes: <br/>"
            strbody &= notes & "<br/><br/>"
            If client = "FMP" Then
                If Convert.ToInt32(parttype) = Convert.ToInt32(PartTypes.Engine) Then
                    strbody &= "Additional Parts To Quote:<br/>" & emailItems & "<br/><br/>"
                End If
            End If
            'insert into client order table
            Dim strorderid As String = String.Empty
            Dim strpartid As String = String.Empty

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm4 As New SqlCommand("insert into tblorders(orderdate, username, customerno, automake, ordertype, shop, address, city, state, zip, phone, contact, autoyear, automodel, mileage, po, autoowner, salesmanemail, IP, vinno, Notes, email) select '" & Now() & "', '" & name & "','" & strcustno & "','" & make & "','" & strPartType & "','" & Replace(RepairFacility, "'", "") & "','" & Replace(Address, "'", "") & "','" & Replace(City, "'", "") & "','" & State & "','" & Zip & "','" & Phone & "','" & Contact & "','" & year & "','" & model & "','" & Mileage & "','" & PO & "','" & Replace(Customer, "'", "") & "','" & emailtocc(0) & "','" & IP & "','" & VIN & "','" & notes & "','" & stremail & "';SELECT orderid FROM tblorders WHERE (orderid = SCOPE_IDENTITY())", conn)
                conn.Open()
                strorderid = sqlComm4.ExecuteScalar()
            End Using

            For Each thepart As Parts In Parts
                If parttype = 3 Then
                    'get proper id
                    parttype = IIf(GetDiffPosition(thepart.PartNumber) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)
                End If
                'insert into client order details table
                Dim decWarrantyCost, decOurCost As Decimal
                decOurCost = FormatNumber(GetClientCost(thepart.PartNumber, parttype, vendor, client), 2)
                decWarrantyCost = GetWarrantyMarkup(decOurCost, warranty, parttype, name, client)

                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm5 As New SqlCommand("insert into tblorderdetails(orderid,part,description,quantity,coreprice,ourcost,warrantycost,theirprice, warranty, subtype,vendor) values ('" & strorderid & "','" & thepart.PartNumber.Replace("Part #: ", Nothing) & "','" & strPartType & "','" & "1" & "','" & Trim(thepart.CorePrice.Replace("Core: $", Nothing)) & "','" & decOurCost & "','" & decWarrantyCost & "','" & FormatCurrency(thepart.SalePrice, 2) & "','" & warranty & "','" & subtype & "','" & vendor & "');SELECT partid FROM tblorderdetails WHERE (partid = SCOPE_IDENTITY())", conn)
                    conn.Open()
                    strpartid = sqlComm5.ExecuteScalar
                End Using
                If client = "AutoNation" Then GoTo AutoNationInsertToCK
            Next



            'special link
            strbody &= "****New Procedures**** You must click the below link to finalize this order even if you wish to pull the part from your warehouse<br/><br/>"
            Dim strurl
            strurl = clienturl & "Pages/FinalizeOrder.aspx?orderid=" & strorderid



            strbody &= "<a href='" & strurl & "'>Finalize This Order</a>"

            mm.Body = strbody
            mm.IsBodyHtml = True
#If DEBUG Then

#Else
            For x As Integer = 1 To emailtocc.Count - 1
                If emailtocc(x) <> "" Then mm.CC.Add(emailtocc(x))
            Next
#End If


            Try
                Dim smtp As New System.Net.Mail.SmtpClient
                smtp.Host = "smtp.emailsrvr.com"
                smtp.Timeout = 500000
#If DEBUG Then
                smtp.Send(mm)
#Else
                smtp.Send(mm)
                Try
                    AddToAutoEmail(client, Membership.GetUser(name).ProviderUserKey.ToString(), 1, stremail, clientnoreplyemail, strorderid,,,)
                Catch ex As Exception
                    Dim mm1 As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "james_obrien@ckautoparts.com")
                    Dim UserID As String = String.Empty

                    If String.IsNullOrEmpty(name) = False Then
                        If String.IsNullOrEmpty(Membership.GetUser(name).ProviderUserKey.ToString()) = False Then
                            UserID = Membership.GetUser(name).ProviderUserKey.ToString()
                        Else
                            UserID = ""
                        End If
                    Else
                        name = ""
                    End If
                    If String.IsNullOrEmpty(stremail) = False Then
                    Else
                        stremail = ""
                    End If

                    mm1.Subject = "Error Adding to tblAutoEmail"
                    mm1.Body = String.Format("Message: <br />{0}<br /><br />StackTrace: <br />{1}<br /><br />Client: <br />{2}<br /><br />Name: <br />{11}<br /><br />UserID: <br />{3}<br /><br />emailType: <br />{4}<br /><br />SendTo: <br />{5}<br /><br />SendFrom: <br />{6}<br /><br />OrderID: <br />{7}<br /><br />PartID: <br />{8}<br /><br />Subject Override: <br />{9}<br /><br />CC: <br />{10}", ex.Message, ex.StackTrace, client, UserID, 1, stremail, IIf(clientnoreplyemail = Nothing, "", clientnoreplyemail), IIf(strorderid = Nothing, "", strorderid), Nothing, Nothing, Nothing, name)
                    mm1.IsBodyHtml = True


                    Dim smtp1 As New System.Net.Mail.SmtpClient
                    smtp1.Host = "smtp.emailsrvr.com"
                    smtp1.Timeout = 500000
                    smtp1.Send(mm1)

                End Try

#End If


                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim strsqlPartOrder = "update tblRemanQuotes set OrderID=" & strorderid & "where QuoteID=" & quoteID
                    Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                    conn.Open()
                    sqlCommPartOrder.ExecuteNonQuery()
                End Using

                Return strorderid
            Catch Ex As WebException
                Console.WriteLine(Ex)
                Return False
            End Try


        Else 'admin placing order

            'insert into client order table
            Dim strorderid As String = String.Empty
            Dim strpartid As String = String.Empty

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm4 As New SqlCommand("insert into tblorders(orderdate, username, customerno, automake, ordertype, shop, address, city, state, zip, phone, contact, autoyear, automodel, mileage, po, autoowner, salesmanemail, IP, vinno,notes, email) select '" & Now() & "', '" & name & "','" & customernumberbehalf & "','" & make & "','" & strPartType & "','" & Replace(RepairFacility, "'", "") & "','" & Replace(Address, "'", "") & "','" & Replace(City, "'", "") & "','" & State & "','" & Zip & "','" & Phone & "','" & Contact & "','" & year & "','" & model & "','" & Mileage & "','" & PO & "','" & Replace(Customer, "'", "") & "','" & "" & "','" & IP & "','" & VIN & "','" & notes & "','" & email & "';SELECT orderid FROM tblorders WHERE (orderid = SCOPE_IDENTITY())", conn)
                conn.Open()
                strorderid = sqlComm4.ExecuteScalar()
            End Using



            For Each thepart As Parts In Parts
                If parttype = 3 Then
                    'get proper id
                    parttype = IIf(GetDiffPosition(thepart.PartNumber) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)
                End If
                'insert into client order details table
                Dim decWarrantyCost, decOurCost As Decimal
                decOurCost = FormatNumber(GetClientCost(thepart.PartNumber, parttype, vendor, client), 2)
                decWarrantyCost = GetWarrantyMarkup(decOurCost, warranty, parttype, name, client)
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm5 As New SqlCommand("insert into tblorderdetails(orderid, Part, description, quantity, coreprice, ourcost, warrantycost, theirprice, warranty, subtype, vendor) values ('" & strorderid & "','" & thepart.PartNumber.Replace("Part #: ", Nothing) & "','" & strPartType & "','" & "1" & "','" & Trim(thepart.CorePrice.Replace("Core: $", Nothing)) & "','" & decOurCost & "','" & decWarrantyCost & "','" & FormatCurrency(thepart.SalePrice, 2) & "','" & warranty & "','" & subtype & "','" & vendor & "');SELECT partid FROM tblorderdetails WHERE (partid = SCOPE_IDENTITY())", conn)
                    conn.Open()
                    strpartid = sqlComm5.ExecuteScalar
                End Using



            Next

#If DEBUG Then
#Else
            Try
            AddToAutoEmail(client, Membership.GetUser(name).ProviderUserKey.ToString(), 1, email, clientnoreplyemail, strorderid,,,)
                 Catch ex As Exception
                Dim mm1 As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "james_obrien@ckautoparts.com")
                Dim UserID As String = String.Empty

                If String.IsNullOrEmpty(name) = False Then

                    Try
                        If String.IsNullOrEmpty(Membership.GetUser(name).ProviderUserKey.ToString()) = False Then
                            UserID = Membership.GetUser(name).ProviderUserKey.ToString()
                        Else
                            UserID = ""
                        End If
                    Catch ex2 As Exception
                        UserID = ex2.Message
                    End Try
                Else
                    name = ""
                End If
                If String.IsNullOrEmpty(stremail) = False Then
                Else
                    stremail = ""
                End If

                mm1.Subject = "Error Adding to tblAutoEmail"
                mm1.Body = String.Format("Message: <br />{0}<br /><br />StackTrace: <br />{1}<br /><br />Client: <br />{2}<br /><br />Name: <br />{11}<br /><br />UserID: <br />{3}<br /><br />emailType: <br />{4}<br /><br />SendTo: <br />{5}<br /><br />SendFrom: <br />{6}<br /><br />OrderID: <br />{7}<br /><br />PartID: <br />{8}<br /><br />Subject Override: <br />{9}<br /><br />CC: <br />{10}", ex.Message, ex.StackTrace, client, UserID, 1, stremail, IIf(clientnoreplyemail = Nothing, "", clientnoreplyemail), IIf(strorderid = Nothing, "", strorderid), Nothing, Nothing, Nothing, name)
                mm1.IsBodyHtml = True



                Dim smtp1 As New System.Net.Mail.SmtpClient
                smtp1.Host = "smtp.emailsrvr.com"
                smtp1.Timeout = 500000
                smtp1.Send(mm1)

            End Try

#End If

            'insert into C&K order table
AutoNationInsertToCK:
            Dim strcore

            Try
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlCommEmail As New SqlCommand("SELECT dbo.aspnet_Membership.Email FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId where username = '" & name & "'", conn)
                    conn.Open()
                    stremail = sqlCommEmail.ExecuteScalar()
                End Using

                Dim strckorderid
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)

                    Dim strsqlOrder = "Insert into tblOrder (AuthorizationNo, AutoYear, AutoMake, AutoModel, vinno, AutoOwner, CustomerNo, DateOrdered, EnteredBy, Mileage, JustaField, AdjusterName, AdjusterEmail, IP, Notes) values ('" & PO & "', '" & year & "', '" & make & "', '" & model & "', '" & VIN & "', '" & Customer & "', '" & IIf(clientckcustomerno = "10000", "MiscRepair", clientckcustomerno) & "','" & Now() & "', 'Web', '" & Mileage & "',1,'" & name & "', '" & stremail & "', '" & IP & "', '" & notes & "');SELECT SCOPE_IDENTITY()"
                    Dim sqlCommOrder As New SqlCommand(strsqlOrder, conn)
                    conn.Open()
                    strckorderid = sqlCommOrder.ExecuteScalar()
                End Using
                For Each thepart As Parts In Parts

                    If Trim(thepart.CorePrice.Replace("Core: $", Nothing)) > 0 Then
                        strcore = 1
                    Else
                        strcore = 0
                    End If

                    If parttype = 1 Then
                        strPartType = subtype & " Transmission"
                    ElseIf parttype = 3 Then
                        'get proper id
                        parttype = IIf(GetDiffPosition(thepart.PartNumber) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)
                        Select Case parttype
                            Case 6
                            Case 7
                                strPartType = "Front Differential"
                            Case 8
                            Case 9
                                strPartType = "Rear Differential"
                        End Select
                    Else
                        strPartType = "Reman " & strPartType
                    End If

                    Dim decWarrantyCost As Decimal
                    Dim decCKCost As Decimal
                    Dim sellprice = IIf(client = "AutoNation", FormatCurrency(thepart.SalePrice, 2), GetClientCost(thepart.PartNumber, parttype, vendor, client))
                    decWarrantyCost = GetWarrantyMarkup(sellprice, warranty, parttype, name, client)
                    decCKCost = GetCKCatalogCost(thepart.PartNumber, parttype, vendor)
                    'warranty cleanup
                    warranty = WarrantyCleanup(warranty)

                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        'If ever ned to add other warranties or change this for the 36/unlimted please refer to the Warranty CleanUp Function
                        Dim strsqlPartOrder = String.Empty
                        If client = "FMP" AndAlso warranty = "36/un" Then
                            'ALtering Cost ONLY in our database...Since we charge them 75 and they charge their customers 100
                            strsqlPartOrder = "Insert into tblPartOrder(Orderid, Address1, City, Contact, DateEntered, EnteredBy, PartDescription, PartNO, Phone, Quantity, SellPrice, WarrantyCost,Servicer, State, Vendor, Zip, Warranty, Core, CorePrice, CoreCost, CoreValue, CostPrice,UnitValue,custshippingprice, arrivaldate, partdescgroup, parttype) values (" & strckorderid & ",'" & Address & "','" & City & "','" & Contact & "','" & Now() & "','Web', '" & strPartType & "', '" & thepart.PartNumber & "','" & Phone & "','1','" & sellprice - GetHotBuildFee(name, parttype, client) & "','" & 75.0 & "','" & RepairFacility & "','" & State & "','" & vendor & "', '" & Zip & "','" & warranty & "','" & strcore & "','" & thepart.CorePrice & "','" & thepart.CorePrice & "','" & thepart.CorePrice & "','" & decCKCost & "','" & decCKCost & "','" & GetHotBuildFee(name, parttype, client) & "','" & GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor) & "','" & strPartDescGroup & "','Aftermarket');SELECT partid FROM tblpartorder WHERE (partid = SCOPE_IDENTITY())"
                        Else
                            strsqlPartOrder = "Insert into tblPartOrder(Orderid, Address1, City, Contact, DateEntered, EnteredBy, PartDescription, PartNO, Phone, Quantity, SellPrice, WarrantyCost,Servicer, State, Vendor, Zip, Warranty, Core, CorePrice, CoreCost, CoreValue, CostPrice,UnitValue,custshippingprice, arrivaldate, partdescgroup, parttype) values (" & strckorderid & ",'" & Address & "','" & City & "','" & Contact & "','" & Now() & "','Web', '" & strPartType & "', '" & thepart.PartNumber & "','" & Phone & "','1','" & sellprice - GetHotBuildFee(name, parttype, client) & "','" & decWarrantyCost & "','" & RepairFacility & "','" & State & "','" & vendor & "', '" & Zip & "','" & warranty & "','" & strcore & "','" & thepart.CorePrice & "','" & thepart.CorePrice & "','" & thepart.CorePrice & "','" & decCKCost & "','" & decCKCost & "','" & GetHotBuildFee(name, parttype, client) & "','" & GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor) & "','" & strPartDescGroup & "','Aftermarket');SELECT partid FROM tblpartorder WHERE (partid = SCOPE_IDENTITY())"
                        End If

                        Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                        conn.Open()
                        intCKPartID = sqlCommPartOrder.ExecuteScalar()
                    End Using
                    If client = "AutoNation" Then InsertNote(strckorderid, notes.Replace("'", ""), False, name)

                    'update pigeon partid with ckpartid
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm As New SqlCommand(String.Format("update tblorderdetails set ckpartid={0} where partid={1}", intCKPartID, strpartid), conn)
                        conn.Open()
                        sqlComm.ExecuteNonQuery()
                    End Using

                Next

#If DEBUG Then
#Else

                AddToAutoEmail("CK", "BE54ACB4-FACB-4A27-8203-8584590F589A", 1, GetUserEmail(name, client), "sales@ckautoparts.com", strckorderid,,,)

#End If

                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim strsqlPartOrder = "update tblRemanQuotes set OrderID=" & strorderid & "where QuoteID=" & quoteID.Substring(quoteID.IndexOf("-") + 1)
                    Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                    conn.Open()
                    sqlCommPartOrder.ExecuteNonQuery()
                End Using

                Return strckorderid
            Catch Ex As WebException
                Return False
            End Try



        End If
    End Function

    Public Shared Function GetUserTierByEmail(Email As String, client As String) As Long
        Dim intTier As Long = 0
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT TierID FROM aspnet_Membership WHERE LoweredEmail = '" & Email.ToLower & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    intTier = CInt(r("TierID").ToString)
                End While
            End Using
        End Using
        Return intTier

    End Function
    Public Shared Function ProcessOrderCK(ByVal source As String, ByVal parttype As String, ByVal Parts As List(Of Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal VIN As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal Auth As String, ByVal Contract As String, ByVal EOCDate As String, ByVal EOCMileage As String, ByVal client As String, ByVal IP As String, ByVal customernumberbehalf As String, ByVal vendor As String, ByVal subtype As String, ByVal notes As String, ByVal adjuster As String, ByVal email As String, ByVal quoteID As String)
        name = name.Replace(",", "")
        adjuster = adjuster.Replace(",", "")
        Dim strPartType As String = String.Empty
        Dim strVendor As String = String.Empty
        Dim strPartDescGroup As String = String.Empty
        GetDefaults(client)

        'get year, make, model if vin only supplied
        If String.IsNullOrEmpty(year) Or String.IsNullOrEmpty(make) Or String.IsNullOrEmpty(model) Then
            Dim vehicle As VINResult = GetVehicleFromVIN(Trim(VIN))
            year = vehicle.Year
            make = vehicle.Make
            model = vehicle.Model
        End If

        strVendor = vendor
        Select Case parttype
            Case 1

                strPartType = "Transmission"
                strPartDescGroup = If(subtype = "Reman", "Reman Transmission", "Builder Transmission")
            'strVendor = "78"
            Case 2

                strPartType = "Engine"
                strPartDescGroup = "Reman Engine"
            'strVendor = "75"
            Case 3, 6, 7, 8, 9

                strPartType = "Differential"
                strPartDescGroup = "Reman Differential"
            'strVendor = "91"
            Case 4

                strPartType = "Transfer Case"
                strPartDescGroup = "Reman Transfer Case"
            'strVendor = "91"
            Case 10

                strPartType = "Manual Transmission"
                strPartDescGroup = "Manual Transmission"
            Case 5
        End Select



        'insert into C&K order table


        Dim stremail As String = String.Empty
        Dim strcore As String = String.Empty
        Dim strorderid As String = String.Empty
        Dim strAdjuster As String

        If Auth = "" And PO <> "" Then Auth = PO

        Try
            If customernumberbehalf <> "" Then
                stremail = email
                clientckcustomerno = customernumberbehalf
                strAdjuster = adjuster
            Else
                strAdjuster = name
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlCommEmail As New SqlCommand("SELECT dbo.aspnet_Membership.Email, customerno FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId where username = '" & name & "'", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlCommEmail.ExecuteReader()
                        While r.Read()
                            stremail = r("Email")
                            clientckcustomerno = r("customerno")
                        End While
                    End Using
                End Using
            End If

            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim strsqlOrder = "Insert into tblOrder (AuthorizationNo, ContractNo, VInNo, AutoYear, AutoMake, AutoModel, AutoOwner, CustomerNo, DateOrdered, EnteredBy, Mileage, JustaField, AdjusterName, AdjusterEmail, IP, Notes) values ('" & Auth & "', '" & Contract & "', '" & VIN & "', '" & year & "', '" & make & "', '" & model & "', '" & Customer & "', '" & clientckcustomerno & "','" & Now() & "', 'Web', '" & Mileage & "',1,'" & strAdjuster & "', '" & stremail & "', '" & IP & "', '" & notes & "');SELECT SCOPE_IDENTITY()"
                Dim sqlCommOrder As New SqlCommand(strsqlOrder, conn)
                conn.Open()
                strorderid = sqlCommOrder.ExecuteScalar()
            End Using
            For Each thepart As Parts In Parts


                thepart.CorePrice = Trim(thepart.CorePrice.Replace("Core: $", Nothing))
                If thepart.CorePrice > 0 Then
                    strcore = 1
                Else
                    strcore = 0
                End If


                'get cost
                Dim strCost As String = String.Empty
                Dim dtETA As String = String.Empty
                Dim decCKCost As Decimal


                Select Case strPartType
                    Case "Transmission"
                        strPartType = thepart.Subtype & " Transmission"
                        If subtype = "Reman" Then
                            decCKCost = GetCKCatalogCost(thepart.PartNumber, parttype, vendor)
                            dtETA = GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor)
                        ElseIf thepart.Subtype = "Builder" Then
                            strCost = "SELECT top 1 cost tbltransmissioncatalogbuilder where partnumber = '" & thepart.PartNumber & "'"
                            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlCommCost As New SqlCommand(strCost, conn)
                                conn.Open()
                                decCKCost = sqlCommCost.ExecuteScalar
                            End Using
                            strVendor = "2209"
                            dtETA = GetExpArrivalDate(thepart.PartNumber, parttype, State, client, "2209")
                        End If
                    Case "Engine"
                        decCKCost = GetCKCatalogCost(thepart.PartNumber, parttype, vendor)
                        strPartType = subtype & " Engine"
                        dtETA = GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor)
                    Case "Transfer Case"
                        decCKCost = GetCKCatalogCost(thepart.PartNumber, parttype, vendor)
                        strPartType = subtype & " Transfer Case"
                        dtETA = GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor)
                    Case "Differential"
                        parttype = IIf(GetDiffPosition(thepart.PartNumber) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)
                        decCKCost = GetCKCatalogCost(thepart.PartNumber, parttype, vendor)
                        Select Case parttype
                            Case 6, 7
                                strPartType = subtype & " Front Differential"
                            Case 8, 9
                                strPartType = subtype & " Rear Differential"
                        End Select

                        dtETA = GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor)
                    Case "Manual Transmission"
                        decCKCost = GetCKCatalogCost(thepart.PartNumber, parttype, vendor)
                        strPartType = subtype & " Mannual Transmission"
                        dtETA = GetExpArrivalDate(thepart.PartNumber, parttype, State, client, vendor)
                End Select




continueorder:
                'warranty cleanup

                Dim decWarrantyCost As Decimal
                decWarrantyCost = WarrantyCleanupAmount(warranty)
                warranty = WarrantyCleanup(warranty)


                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim strsqlPartOrder = "Insert into tblPartOrder(Orderid, Address1, City, Contact, DateEntered, EnteredBy, PartDescription, PartNO, Phone, Quantity, SellPrice, Servicer, State, Vendor, Zip, Warranty, Core, CorePrice, CoreCost, CoreValue, CostPrice, UnitValue, warrantycost, WarrantyDate, WarrantyMileage, arrivaldate, partdescgroup,parttype) values (" & strorderid & ",'" & Address & "','" & City & "','" & Contact & "','" & Now() & "','Web', '" & strPartType & "', '" & Trim(thepart.PartNumber.Replace("Part #:", Nothing)) & "','" & Phone & "','1','" & thepart.SalePrice & "','" & RepairFacility & "','" & State & "','" & strVendor & "', '" & Zip & "','" & warranty & "','" & strcore & "','" & thepart.CorePrice & "','" & thepart.CorePrice & "','" & thepart.CorePrice & "','" & decCKCost & "','" & decCKCost & "','" & decWarrantyCost & "','" & EOCDate & "','" & EOCMileage & "','" & dtETA & "','" & strPartDescGroup & "','Aftermarket')"
                    Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                    conn.Open()
                    sqlCommPartOrder.ExecuteNonQuery()
                End Using

            Next
            Try
                AddToAutoEmail(client, Membership.GetUser(name).ProviderUserKey.ToString(), 1, stremail, "sales@ckautoparts.com", strorderid,,,)

            Catch ex As Exception
                Dim mm1 As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "james_obrien@ckautoparts.com")
                Dim UserID As String = String.Empty
                If String.IsNullOrEmpty(name) = False Then

                    Try
                        If String.IsNullOrEmpty(Membership.GetUser(name).ProviderUserKey.ToString()) = False Then
                            UserID = Membership.GetUser(name).ProviderUserKey.ToString()
                        Else
                            UserID = ""
                        End If
                    Catch ex2 As Exception
                        UserID = ex2.Message
                    End Try
                Else
                    name = ""
                End If
                If String.IsNullOrEmpty(stremail) = False Then
                Else
                    stremail = ""
                End If

                mm1.Subject = "Error Adding to tblAutoEmail"
                mm1.Body = String.Format("Message: <br />{0}<br /><br />StackTrace: <br />{1}<br /><br />Client: <br />{2}<br /><br />Name: <br />{11}<br /><br />UserID: <br />{3}<br /><br />emailType: <br />{4}<br /><br />SendTo: <br />{5}<br /><br />SendFrom: <br />{6}<br /><br />OrderID: <br />{7}<br /><br />PartID: <br />{8}<br /><br />Subject Override: <br />{9}<br /><br />CC: <br />{10}", ex.Message, ex.StackTrace, client, UserID, 1, stremail, IIf(clientnoreplyemail = Nothing, "", clientnoreplyemail), IIf(strorderid = Nothing, "", strorderid), Nothing, Nothing, Nothing, name)
                mm1.IsBodyHtml = True

                Dim smtp1 As New System.Net.Mail.SmtpClient
                smtp1.Host = "smtp.emailsrvr.com"
                smtp1.Timeout = 500000
                smtp1.Send(mm1)

            End Try

            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim strsqlPartOrder = "update tblRemanQuotes set OrderID=" & strorderid & "where QuoteID=" & quoteID.Substring(quoteID.IndexOf("-") + 1)
                Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                conn.Open()
                sqlCommPartOrder.ExecuteNonQuery()
            End Using
            Return strorderid
        Catch Ex As WebException

            Return False

        End Try
        Return False

    End Function

    Public Shared Function ProcessMainQuote(ByVal partTypeID As Integer, ByVal Parts As List(Of Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal vin As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal Auth As String, ByVal Contract As String, ByVal client As String, ByVal customernumberbehalf As String, ByVal adjuster As String, ByVal email As String, ByVal notes As String, ByVal quoteID As String) As String
        Try
            If quoteID = "" Then
                For Each part As Parts In Parts
                    Dim strsqlins As String = "insert into tblRemanQuotes(PartTypeID, QuoteDate, Username, CustomerNo, [Year], Make, Model, PartNo,LocalStock, SellPrice, CostPrice, CorePrice,customerEmail,VIN,Warranty,Notes) values (" & Convert.ToInt32(partTypeID) & ",'" & Now().ToString & "'," & name.fqq & "," & customernumberbehalf & "," & year.fqq & "," & make.fqq & "," & model.fqq & "," & part.PartNumber.fqq & ",0," & part.SalePrice & ",0," & part.CorePrice & ",'" & email & "','" & vin & "','" & warranty & "','" & notes & "'); Select Scope_Identity()"
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlCommins As New SqlCommand(strsqlins, conn)
                        conn.Open()
                        quoteID = sqlCommins.ExecuteScalar()
                    End Using
                Next
                Return [Enum].Parse(GetType(PigeonClientIDs), client) & "-" & quoteID.ToString
            Else
                For Each part As Parts In Parts
                    Dim strsqlins As String = "update tblRemanQuotes set SellPrice=" & part.SalePrice & ",VIN='" & vin & "',Warranty='" & warranty & "', Notes ='" & notes & "' where quoteID=" & quoteID.Substring(quoteID.IndexOf("-") + 1)
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlCommins As New SqlCommand(strsqlins, conn)
                        conn.Open()
                        sqlCommins.ExecuteNonQuery()
                    End Using
                Next
                Return quoteID
            End If

        Catch
            'Do nothing
        End Try

        Return String.Empty

    End Function


    Public Shared Function ProcessQuoteWithVIN(ByVal customerNo As String, ByVal customerEmail As String, ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal engine As String, ByVal size As String, ByVal options As String, ByVal client As String, ByVal strPartNumber As String, ByVal intLocalStock As Integer, ByVal decSellPrice As Decimal, ByVal pricing As Pricing, ByVal PartTypeID As PartTypes, ByVal Transmission As String, ByVal DiffType As String, ByVal vin As String) As String
        Dim quoteID As String = ProcessQuote(customerNo, customerEmail, name, year, make, model, engine, size, options, client, strPartNumber, intLocalStock, decSellPrice, pricing, PartTypeID, Transmission, DiffType)
        Dim updateQuery As String = "update tblRemanQuotes set VIN='" & vin & "' where quoteID=" & quoteID.Substring(quoteID.IndexOf("-") + 1)
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlCommUpdate As New SqlCommand(updateQuery.ToString, conn)
            conn.Open()
            sqlCommUpdate.ExecuteNonQuery()
        End Using
        Return quoteID
    End Function

    Public Shared Function ProcessQuote(ByVal customerNo As String,
                                        ByVal customerEmail As String,
                                        ByVal name As String,
                                        ByVal year As String,
                                        ByVal make As String,
                                        ByVal model As String,
                                        ByVal engine As String,
                                        ByVal size As String,
                                        ByVal options As String,
                                        ByVal client As String,
                                        ByVal strPartNumber As String,
                                        ByVal intLocalStock As Integer,
                                        ByVal decSellPrice As Decimal,
                                        ByVal pricing As Pricing,
                                        ByVal PartTypeID As PartTypes,
                                        ByVal Transmission As String,
                                        ByVal DiffType As String) As String
        Try
            Dim stackTrace As New StackTrace()
            'Could have just had one Insert Statement and passed the values that are being passed did it this way incase we wanna do different logic for each Item down the Road..
            Dim strsqlins As New StringBuilder
            Dim decCost As Decimal

            strsqlins.Append("insert into tblRemanQuotes(PartTypeID, QuoteDate, Username, CustomerNo, [Year], Make, Model, PartNo,LocalStock, SellPrice, CostPrice, CorePrice,customerEmail")

            If stackTrace.GetFrame(1).GetMethod().Name = "NewManualQuote" Then
                If pricing.core = Nothing Then
                    pricing.core = 0
                End If
                strsqlins.Append(") values(" & Convert.ToInt32(PartTypeID) & ",'" & Now() & "'," & name.fqq & "," & customerNo & "," & year.fqq & "," & make.fqq & "," & model.fqq & "," & strPartNumber.fqq & "," & intLocalStock & "," & decSellPrice & ",0," & pricing.core & ",'" & customerEmail & "')")
            Else
                If GetUserRole(name, client) = "Admin" Then
                    decCost = pricing.tiers.Where(Function(a) a.TierID = 2).Select(Function(s) s.Price).FirstOrDefault
                Else
                    decCost = GetClientCost(strPartNumber, PartTypeID, pricing.vendor, client)
                End If
                Select Case PartTypeID
                    Case PartTypes.Engine
                        strsqlins.Append(",Engine,Size,Options) values(" & Convert.ToInt32(PartTypeID) & ",'" & Now() & "'," & name.fqq & ",'" & customerNo & "'," & year.fqq & "," & make.fqq & "," & model.fqq & "," & strPartNumber.fqq & "," & intLocalStock & "," & decSellPrice & "," & decCost & "," & pricing.core & ",'" & customerEmail & "'," & engine.fqq & "," & size.fqq & "," & options.fqq & ")")
                    Case PartTypes.TransferCase
                        If pricing.core = Nothing Then
                            pricing.core = 0
                        End If
                        strsqlins.Append(") values(" & Convert.ToInt32(PartTypeID) & ",'" & Now() & "'," & name.fqq & ",'" & customerNo & "'," & year.fqq & "," & make.fqq & "," & model.fqq & "," & strPartNumber.fqq & "," & intLocalStock & "," & decSellPrice & "," & decCost & "," & pricing.core & ",'" & customerEmail & "')")
                    Case PartTypes.Transmission
                        If pricing.core = Nothing Then
                            pricing.core = 0
                        End If
                        strsqlins.Append(",Engine,Transmission) values(" & Convert.ToInt32(PartTypeID) & ",'" & Now() & "'," & name.fqq & ",'" & customerNo & "'," & year.fqq & "," & make.fqq & "," & model.fqq & "," & strPartNumber.fqq & "," & intLocalStock & "," & decSellPrice & "," & decCost & "," & pricing.core & ",'" & customerEmail & "'," & engine.fqq & ",'" & Transmission & "')")
                    Case PartTypes.ManualTransmission
                        If pricing.core = Nothing Then
                            pricing.core = 0
                        End If
                        strsqlins.Append(") values(" & Convert.ToInt32(PartTypeID) & ",'" & Now() & "'," & name.fqq & ",'" & customerNo & "'," & year.fqq & "," & make.fqq & "," & model.fqq & "," & strPartNumber.fqq & "," & intLocalStock & "," & decSellPrice & "," & decCost & "," & pricing.core & ",'" & customerEmail & "')")
                    Case PartTypes.Differential
                        If pricing.core = Nothing Then
                            pricing.core = 0
                        End If
                        strsqlins.Append(",DiffType) values(" & Convert.ToInt32(PartTypeID) & ",'" & Now() & "'," & name.fqq & ",'" & customerNo & "'," & year.fqq & "," & make.fqq & "," & model.fqq & "," & strPartNumber.fqq & "," & intLocalStock & "," & decSellPrice & "," & decCost & "," & pricing.core & ",'" & customerEmail & "','" & DiffType & "')")
                    Case PartTypes.OEM
                End Select
            End If
            strsqlins.Append("; Select Scope_Identity()")
            Dim ID As Integer
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlCommins As New SqlCommand(strsqlins.ToString, conn)
                conn.Open()
                ID = sqlCommins.ExecuteScalar()
            End Using
            Return [Enum].Parse(GetType(PigeonClientIDs), client) & "-" & ID.ToString
        Catch
            'Do nothing

        End Try

        Return String.Empty

    End Function


    Public Shared Function WarrantyCleanupAmount(ByVal warranty As String)
        Dim strWarranty = warranty.Split("--")
        Return CDec(Trim(strWarranty(0)))
    End Function
    Public Shared Function WarrantyCleanup(ByVal warranty As String)
        Dim strWarranty = warranty.Split("--")
        Select Case Trim(strWarranty(2))
            Case "90 Day"
                warranty = "90"
            Case "3 months/3,000 miles"
                warranty = "3/3"
            Case "6 months/6,000 miles"
                warranty = "6/6"
            Case "12 months/12,000 miles"
                warranty = "12/12"
            Case "12 months/unlimited miles"
                warranty = "12/un"
            Case "24 months/24,000 miles"
                warranty = "24/24"
            Case "36 months/36,000 miles"
                warranty = "36/36"
            Case "36 months/100,000 miles", "36/100 $60/hr"
                warranty = "36/100"
                'IF EVER changing FMP Warranty Cost again or need other warranties look here..
            Case "36 months/unlimited miles", "36/Unlimited shop labor rate"
                warranty = "36/un"
            Case "EOC"
                warranty = "EOC"
            Case "EOC(No Charge)"
                warranty = "EOC(No Charge)"
            Case Else
                warranty = "36/100"
        End Select
        Return warranty
    End Function
    Public Shared Function EmailCK(ByVal noreplyemail As String, ByVal orderid As String)

        Dim mm As New System.Net.Mail.MailMessage(noreplyemail, "tracyorders@ckautoparts.com")


        mm.Body = "A Tracy customer just ordered a part and should have automatically been placed into Tracy's system. The purpose of this email is so that we can monitor this new process.  The order number is " & orderid & " and you can see the details in the Tec Powertrain site. Once we are confident this is working correctly let Darrell know to kill this message."
        mm.Subject = "New Tracy Ordered Entered-Let's confrim everything ok"
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

    Public Shared Function GetDiffPosition(ByVal partno As String)
        Dim intPositionID As Long
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            conn.Open()
            Dim sqlcomm As New SqlCommand("select top 1 PositionID from tblZumbrotaDifferentialCatalog where Part='" & partno & "'
group by positionid
order by count(*) desc", conn)
            intPositionID = sqlcomm.ExecuteScalar()
        End Using
        Return intPositionID
    End Function
    Public Shared Function GetCKSell(ByVal CKCost As Decimal, ByVal parttype As String)
        Dim decCKPercentage, decCKFlat As Decimal
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlCommCost As New SqlCommand("Select percentage, flat from tblTierbaseprice where tierid=2 and parttype=" & parttype, conn)
            conn.Open()
            Using r As SqlDataReader = sqlCommCost.ExecuteReader
                While r.Read
                    decCKPercentage = r("percentage")
                    decCKFlat = r("flat")
                End While
            End Using

        End Using
        Return CKCost + (CKCost * decCKPercentage) + decCKFlat
    End Function
    Public Shared Function GetCKCatalogCost(ByVal partno As String, ByVal parttype As String, ByVal vendor As Long)
        Dim decCKCost As Decimal
        Dim strSql As String = String.Empty
        Dim decInstallationKit As Decimal = 0

        Select Case parttype
            Case 1
                If vendor = 78 Then
                    strSql = "SELECT leveltwo core from tblcertifiedpricing where part = '" & partno & "'"
                End If
                If vendor = 2209 Then
                    strSql = "select cost from tbletepricing where partno ='" & partno & "'"
                End If
                If vendor = 13095 Then
                    strSql = "select cost from tblzfpricing where partno ='" & partno & "'"
                End If
            Case 2
                If vendor = 75 Then
                    strSql = "select cost from tblatkpricing where atkno = '" & partno & "'"
                End If
                If vendor = 13404 Then
                    strSql = "select cost from tblpilotpricing where part = '" & partno & "'"
                End If
            Case 3, 6, 7, 8, 9
                If vendor = 91 Then
                    strSql = "select cost from tblZumbrotaDifferentialPricing where part = '" & partno & "'"
                End If
            Case 4
                If vendor = 91 Then
                    strSql = "select cost from tblZumbrotaTransferCasePricing where part = '" & partno & "'"
                End If
            Case 10
                If vendor = 91 Then
                    strSql = "select cost from tblZumbrotaTransmissionPricing where Part = '" & partno & "'"
                End If
        End Select


        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlCommCost As New SqlCommand(strSql, conn)
            conn.Open()
            Try
                decCKCost = sqlCommCost.ExecuteScalar

            Catch ex As Exception

            End Try
        End Using

        'certified install kit
        If parttype = 1 And vendor = 78 Then 'certified
            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommPrice As New SqlCommand("SELECT installationkit from tblcertifiedpricing  where tblCertifiedPricing.part ='" & partno & "'", conn2)
                conn2.Open()
                Using r As SqlDataReader = sqlCommPrice.ExecuteReader()
                    While r.Read()
                        'decLevelTwo = CDec(r("leveltwo"))
                        If r("installationkit") = "" Or r("installationkit") = "0" Then
                            decInstallationKit = 0
                        Else
                            decInstallationKit = CDec(r("installationkit"))
                        End If
                    End While
                End Using
            End Using
        End If
        Return decCKCost + decInstallationKit

    End Function

    Public Shared Function GetClientList(ByVal partno As String, ByVal parttype As String, ByVal vendor As Long, ByVal client As String)
        Dim decCKCost, decCKSell, decOverridePrice As Decimal
        Dim strName As String
        Dim tiers As List(Of VisibleTier)
        Dim decPrettyPrice As Integer


        'then get client cost for part
        strName = LCase(client & "admin")

        If client <> "CK" Then
            'if price override then return that
            decOverridePrice = GetOverridePrice(partno, parttype, vendor, "", client)
            If decOverridePrice > 0 Then
                'get client tier pricing only
                tiers = GetTiers(parttype, strName, client, decOverridePrice, "sell", False)

                Return tiers.Where(Function(t) t.TierID = 1).Single().Price
            End If

            'if pretty price then return that
            decPrettyPrice = GetPrettyPrice(partno, parttype, vendor)
            If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" And decPrettyPrice > 0 Then
                tiers = GetTiers(parttype, strName, client, decPrettyPrice, "sell", False)
                Return tiers.Where(Function(t) t.TierID = 1).Single().Price
            End If
        End If

        'first get ck cost for part
        decCKCost = GetCKCatalogCost(partno, parttype, vendor)
        decCKSell = GetCKSell(decCKCost, parttype)



        tiers = GetTiers(parttype, strName, client, decCKSell, "sell", False)

        Return tiers.Where(Function(t) t.TierID = 1).Single().Price
    End Function

    Public Shared Function GetClientCost(ByVal partno As String, ByVal parttype As PartTypes, ByVal vendor As Long, ByVal client As String)
        Dim decCKCost, decHotBuild, decCKSell, decOverridePrice As Decimal
        Dim decClientCost As Decimal
        Dim strName As String
        Dim tiers As List(Of VisibleTier)
        Dim decPrettyPrice As Integer

        If client <> "CK" Then
            'if price override then return that
            decOverridePrice = GetOverridePrice(partno, parttype, vendor, "", client)
            If decOverridePrice > 0 Then Return decOverridePrice

            'if pretty price then return that
            decPrettyPrice = GetPrettyPrice(partno, parttype, vendor)
            If (client = "CK" Or clienttype <> "PremierWholesaler") And client <> "DickMyers" And decPrettyPrice > 0 Then Return decPrettyPrice
        End If

        'first get ck cost for part
        decCKCost = GetCKCatalogCost(partno, parttype, vendor)
        decCKSell = GetCKSell(decCKCost, parttype)



        'then get client cost for part
        strName = LCase(client & "admin")


        'get client tier pricing only
        tiers = GetTiers(parttype, strName, client, decCKSell, "sell", True)

        For Each Tier As VisibleTier In tiers
            decClientCost = Tier.Price
        Next

        If client = "AutoNation" Then
            Return decClientCost
        Else
            'any hot build fee
            Dim intLocalStock As Integer = 0
            Select Case parttype
                Case 1
                    intLocalStock = GetLocalStock(partno, 1, clienttransmissionstock)
                Case 2
                    intLocalStock = GetLocalStock(partno, 2, clientenginestock)
                Case 4
                    intLocalStock = GetLocalStock(partno, 4, clienttransfercasestock)
                Case 3, 6, 7, 8, 9
                    intLocalStock = GetLocalStock(partno, parttype, clientdifferentialstock)
                Case 10
                    intLocalStock = GetLocalStock(partno, 10, clientmanualtransmissionstock)
            End Select

            If intLocalStock = 0 Then
                Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm2 As New SqlCommand("SELECT HotBuild FROM tblTierBasePrice WHERE TierID = 2 AND PartType = " & parttype, conn2)
                    conn2.Open()
                    decHotBuild = sqlComm2.ExecuteScalar()
                End Using
            End If
            Return decClientCost + decHotBuild
        End If
    End Function

    Public Shared Function RetrieveOrderDetails(ByVal orderid As String, ByVal client As String)

        Dim intLocalStock
        Dim strStockSearch As String = String.Empty

        GetDefaults(client)


        Dim parttype As Long
        'orders table info

        Dim o1 As New OrderDetails
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlCommOrder As New SqlCommand("select top 1 tblorders.*, tblcompany.company, tblcompany.address + ',' + tblcompany.city + ',' + tblcompany.state + ' ' + tblcompany.zip as CompanyAddress, tblcompany.phone as CompanyPhone, warranty from tblOrders inner join tblcompany on tblorders.customerno = tblcompany.customerno inner join tblorderdetails on tblorders.orderid = tblorderdetails.orderid where tblorders.orderid = " & orderid, conn)
            conn.Open()
            Using rOrder As SqlDataReader = sqlCommOrder.ExecuteReader()
                While rOrder.Read()
                    o1.CustNo = rOrder("CustomerNo").ToString
                    o1.Company = rOrder("Company").ToString
                    o1.CompanyAddress = rOrder("CompanyAddress").ToString
                    o1.Username = rOrder("Username").ToString
                    o1.CompanyPhone = rOrder("CompanyPhone").ToString
                    o1.PO = rOrder("PO").ToString
                    o1.OwnerName = rOrder("AutoOwner").ToString
                    o1.Mileage = rOrder("Mileage").ToString
                    o1.RepairFacility = rOrder("Shop").ToString
                    o1.Address = rOrder("City").ToString
                    o1.City = rOrder("City").ToString
                    o1.State = rOrder("State").ToString
                    o1.Zip = rOrder("Zip").ToString
                    o1.Phone = rOrder("Phone").ToString
                    o1.Contact = rOrder("Contact").ToString
                    o1.Year = rOrder("AutoYear").ToString
                    o1.Make = rOrder("AutoMake").ToString
                    o1.Model = rOrder("AutoModel").ToString
                    o1.VIN = rOrder("VinNo").ToString
                    o1.Warranty = rOrder("Warranty").ToString
                    o1.Notes = rOrder("Notes").ToString
                    Select Case rOrder("OrderType").ToString
                        Case "Transmission"
                            parttype = 1
                            strStockSearch = clienttransmissionstock
                        Case "Engine"
                            parttype = 2
                            strStockSearch = clientenginestock
                        Case "Differential"
                            parttype = 3
                            strStockSearch = clientdifferentialstock

                        Case "Transfer Case"
                            parttype = 4
                            strStockSearch = clienttransfercasestock

                        Case "Manual Transmission"
                            parttype = 10
                            strStockSearch = clientmanualtransmissionstock



                    End Select
                End While
            End Using
        End Using

        'order details info
        Dim d As New List(Of Parts)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlCommOrderDetails As New SqlCommand("select * from tblOrderDetails inner join tblorders on tblorders.orderid = tblorderdetails.orderid where tblorderdetails.orderid = " & orderid, conn)
            conn.Open()
            Using rOrderDetails As SqlDataReader = sqlCommOrderDetails.ExecuteReader()
                While rOrderDetails.Read()
                    Dim d1 As New Parts

                    If parttype = 3 Then
                        'get proper id
                        parttype = IIf(GetDiffPosition(rOrderDetails("Part").ToString) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)
                    End If
                    d1.CorePrice = rOrderDetails("CorePrice").ToString
                    d1.PartNumber = rOrderDetails("Part").ToString
                    d1.Description = rOrderDetails("Description").ToString
                    d1.SalePrice = rOrderDetails("TheirPrice").ToString
                    'check local stock
                    Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(strStockSearch))
                        Dim sqlComm8 As New SqlCommand("SELECT count(Part) from tblStock WHERE part = '" & rOrderDetails("Part").ToString & "' AND type = '" & parttype & "' AND Arrive IS NOT NULL AND ReturnType IS NULL and ckorderid is null", conn2)
                        conn2.Open()
                        intLocalStock = sqlComm8.ExecuteScalar
                    End Using


                    d1.LocalStock = intLocalStock
                    d1.CustomerTransit = FindStateTransit(GetMapData(rOrderDetails("Part").ToString, parttype, client, True, True, True, rOrderDetails("vendor").ToString), o1.State)
                    If intLocalStock > 0 Then
                        d1.ClientTransit = FindStateTransit(GetMapData(rOrderDetails("Part").ToString, parttype, client, True, False, False, rOrderDetails("vendor").ToString), o1.State)
                        'get clist of serial numbers
                        Dim s As New List(Of PartAndSerialNumber)
                        Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(strStockSearch))
                            Dim sqlComm9 As New SqlCommand("SELECT part,sn from tblStock WHERE part = '" & rOrderDetails("Part").ToString & "' AND type = '" & parttype & "' AND Arrive IS NOT NULL AND ReturnType IS NULL and ckorderid is null", conn2)
                            conn2.Open()
                            Using rOrderStock As SqlDataReader = sqlComm9.ExecuteReader()
                                While rOrderStock.Read()
                                    Dim s1 As New PartAndSerialNumber
                                    s1.PartNumber = rOrderStock("part").ToString
                                    s1.SerialNumber = rOrderStock("sn").ToString
                                    s.Add(s1)
                                End While
                            End Using
                        End Using
                        d1.AvaliableSN = s
                    Else
                        d1.ClientTransit = "N/A"
                    End If
                    d1.CKTransit = FindStateTransit(GetMapData(rOrderDetails("Part").ToString, parttype, client, False, True, True, rOrderDetails("vendor").ToString), o1.State)
                    d.Add(d1)
                End While
            End Using
        End Using

        o1.Parts = d


        Return o1
    End Function

    Public Shared Function FinalizeCustomerOrder(ByVal orderid As String, ByVal shipfrom As String, ByVal corepickup As String, ByVal sn As String, ByVal po As String, ByVal name As String, ByVal client As String) As Boolean
        Try


            GetDefaults(client)
            Dim o1 As New OrderDetails
            Dim parttype As Long = 0
            Dim strparttype As String = String.Empty
            Dim decCoreShip As Decimal = 0D
            Dim intCKPartID As Long


            'get order info 

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlCommOrder As New SqlCommand("select top 1 tblorders.*, tblcompany.company, tblcompany.address + ',' + tblcompany.city + ',' + tblcompany.state + ' ' + tblcompany.zip as CompanyAddress, tblcompany.phone as CompanyPhone, warranty, AutoOwner from tblOrders inner join tblcompany on tblorders.customerno = tblcompany.customerno inner join tblorderdetails on tblorders.orderid = tblorderdetails.orderid where tblorders.orderid = " & orderid, conn)
                conn.Open()
                Using rOrder As SqlDataReader = sqlCommOrder.ExecuteReader()
                    While rOrder.Read()
                        o1.Mileage = rOrder("Mileage").ToString
                        o1.RepairFacility = rOrder("Shop").ToString
                        o1.Address = rOrder("Address").ToString
                        o1.City = rOrder("City").ToString
                        o1.State = rOrder("State").ToString
                        o1.Zip = rOrder("Zip").ToString
                        o1.Phone = rOrder("Phone").ToString
                        o1.Contact = rOrder("Contact").ToString
                        o1.Year = rOrder("AutoYear").ToString
                        o1.Make = rOrder("AutoMake").ToString
                        o1.Model = rOrder("AutoModel").ToString
                        o1.VIN = rOrder("VinNo").ToString
                        o1.Warranty = rOrder("Warranty").ToString
                        o1.AutoOwner = rOrder("AutoOwner").ToString
                        o1.Notes = rOrder("Notes").ToString
                        strparttype = rOrder("OrderType").ToString
                        Select Case rOrder("OrderType").ToString
                            Case "Transmission"
                                parttype = 1
                            Case "Engine"
                                parttype = 2
                            Case "Differential"
                                parttype = 3
                            Case "Transfer Case"
                                parttype = 4
                            Case "Manual Transmission"
                                parttype = 10
                        End Select
                        o1.Username = rOrder("username").ToString
                        o1.PO = rOrder("PO").ToString
                        o1.OwnerName = rOrder("autoowner").ToString
                    End While
                End Using
            End Using




            'get all of part info
            Dim strsubtype As String
            Dim d As New List(Of Parts)
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlCommOrderDetails As New SqlCommand("select * from tblOrderDetails inner join tblorders on tblorders.orderid = tblorderdetails.orderid where tblorderdetails.orderid = " & orderid, conn)
                conn.Open()
                Using rOrderDetails As SqlDataReader = sqlCommOrderDetails.ExecuteReader()
                    While rOrderDetails.Read()
                        Dim d1 As New Parts
                        d1.PartID = rOrderDetails("PartID")
                        If parttype = 3 Then
                            'get proper id
                            parttype = IIf(GetDiffPosition(rOrderDetails("Part").ToString) = 22, PartTypes.FrontDiff, PartTypes.RearDiff)
                            Select Case parttype
                                Case 6, 7
                                    strparttype = "Front Differential"
                                Case 8, 9
                                    strparttype = "Rear Differential"
                            End Select
                        End If
                        d1.Subtype = rOrderDetails("Subtype").ToString
                        strsubtype = rOrderDetails("Subtype").ToString
                        d1.CorePrice = rOrderDetails("CorePrice").ToString
                        d1.PartNumber = rOrderDetails("Part").ToString
                        d1.Description = rOrderDetails("Description").ToString
                        d1.SalePrice = rOrderDetails("TheirPrice").ToString
                        d1.CostPrice = rOrderDetails("OurCost").ToString
                        d1.WarrantyPrice = rOrderDetails("WarrantyCost").ToString
                        d1.Vendor = rOrderDetails("Vendor").ToString
                        d.Add(d1)
                    End While
                End Using
            End Using



            '********in service info
            Dim intCKOrderID As Long
            Dim ckOrderCustomer As String = ""

            If shipfrom <> "CK" Then
                For Each Part As Parts In d
                    'add inservice info to ims and pull part from availability
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlCommIMS As New SqlCommand("Update tblstock set mileage = '" & o1.Mileage & "', vin = '" & o1.VIN & "', solddate = '" & Now() & "', returntype = 'core' where sn = '" & sn & "'", conn)
                        conn.Open()
                        sqlCommIMS.ExecuteNonQuery()
                    End Using

                Next
                'send inservice to certified **********this needs to be modified in case the actual vendor will not be certified
                If parttype = 1 Then EmailAddInService(sn, o1.PO, o1.VIN, o1.Mileage, Now(), client)
            Else

            End If


            '****ck order

            'order table
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommInsOrder As New SqlCommand("insert into tblorder(dateordered, customerno, adjustername, adjusteremail, authorizationno, autoowner, autoyear, automake, automodel, mileage, vinno,notes,enteredby) select '" & Now() & "', '" & IIf(clientckcustomerno = "10000", "MiscRepair", clientckcustomerno) & "','" & o1.Username & "','" & GetUserEmail(o1.Username, client) & "','" & po & "','" & o1.OwnerName & "','" & o1.Year & "','" & o1.Make & "','" & o1.Model & "','" & o1.Mileage & "','" & o1.VIN & "','" & o1.Notes & "','Web';SELECT orderid FROM tblorder WHERE (orderid = SCOPE_IDENTITY())", conn)
                conn.Open()
                intCKOrderID = sqlCommInsOrder.ExecuteScalar()
            End Using
            'parts order table
            For Each Part As Parts In d
                Dim strCore As String
                If Part.CorePrice.ToString > 0 Then
                    strCore = "1"
                Else
                    strCore = "0"
                End If

                Dim strPartDescGroup As String = String.Empty

                'get cost
                Dim strCost As String = String.Empty

                Select Case parttype
                    Case 1
                        If Part.Subtype = "Reman" Then
                            If Part.Vendor = 78 Then
                                strCost = "select convert(decimal,leveltwo) + convert(decimal,installationkit) as cost from tblcertifiedpricing where part =  '" & Part.PartNumber & "'"
                            End If
                            If Part.Vendor = 2209 Then
                                strCost = "SELECT top 1 cost from tbletepricing where partno = '" & Part.PartNumber & "'"
                            End If
                            If Part.Vendor = 13095 Then
                                strCost = "SELECT top 1 cost from tblzfpricing where partno = '" & Part.PartNumber & "'"
                            End If
                            strparttype = "Reman " & strparttype
                            strPartDescGroup = "Reman Transmission"
                        ElseIf Part.Subtype = "Builder" Then

                            If Part.Vendor = 2209 Then
                                strCost = "SELECT top 1 cost from tbltransmissioncatalogbuilder where partnumber = '" & Part.PartNumber & "'"
                            End If
                            strparttype = "Builder " & strparttype
                            strPartDescGroup = "Builder Transmission"
                        End If
                    Case 2
                        If Part.Vendor = 75 Then
                            strCost = "select cost from tblatkcatalog inner join tblatkpricing on tblatkcatalog.atkno=tblatkpricing.atkno where tblatkcatalog.atkno = '" & Part.PartNumber & "'"
                        End If
                        strparttype = "Reman " & strparttype
                        strPartDescGroup = "Reman Engine"
                    Case 4
                        If Part.Vendor = 91 Then
                            strCost = "select top 1 cost from tblZumbrotaTransferCasePricing where part = '" & Part.PartNumber & "'"
                        End If
                        strparttype = "Reman " & strparttype
                        strPartDescGroup = "Reman Transfer Case"
                    Case 6, 7, 8, 9
                        If Part.Vendor = 91 Then
                            strCost = "select top 1 cost from tblZumbrotaDifferentialPricing where part = '" & Part.PartNumber & "'"
                        End If
                        strparttype = "Reman " & strparttype
                        strPartDescGroup = "Reman Differential"
                    Case 10
                        If Part.Vendor = 91 Then
                            strCost = "select top 1 cost from tblZumbrotaTransmissionPricing where Part = '" & Part.PartNumber & "'"
                        End If
                        strparttype = "Reman " & strparttype
                        strPartDescGroup = "Manual Transmission"
                End Select

                Dim decCKCost As Decimal
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlCommCost As New SqlCommand(strCost, conn)
                    conn.Open()
                    decCKCost = sqlCommCost.ExecuteScalar
                End Using


                'warranty cleanup
                o1.Warranty = WarrantyCleanup(o1.Warranty)


                'insert parts table
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlCommInsPartOrder As New SqlCommand("insert into tblpartorder(orderid, dateentered, partno, partdescription, quantity, vendor, servicer, address1, city, state,zip,phone,contact,core,sellprice,coreprice,corecost, corevalue, costprice,unitvalue, warranty, warrantycost,arrivaldate,custshippingprice, partdescgroup, parttype) select '" & intCKOrderID & "','" & Now() & "','" & Part.PartNumber & "','" & strparttype & "','1','" & Part.Vendor & "','" & o1.RepairFacility & "','" & o1.Address & "','" & o1.City & "','" & o1.State & "','" & o1.Zip & "','" & o1.Phone & "','" & o1.Contact & "','" & strCore & "','" & Part.CostPrice - GetHotBuildFee(name, parttype, client) & "','" & Part.CorePrice & "','" & Part.CorePrice & "','" & Part.CorePrice & "','" & decCKCost & "','" & decCKCost & "','" & o1.Warranty & "','" & Part.WarrantyPrice & "','" & GetExpArrivalDate(Part.PartNumber, parttype, o1.State, client, Part.Vendor) & "','" & GetHotBuildFee(name, parttype, client) & "','" & strPartDescGroup & "','Aftermarket';SELECT partid FROM tblpartorder WHERE (partid = SCOPE_IDENTITY())", conn)
                    conn.Open()
                    intCKPartID = sqlCommInsPartOrder.ExecuteScalar()
                End Using

                'update pigeon partid with ckpartid
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm As New SqlCommand(String.Format("update tblorderdetails set ckpartid={0} where partid={1}", intCKPartID, Part.PartID), conn)
                    conn.Open()
                    sqlComm.ExecuteNonQuery()
                End Using

                If shipfrom <> "CK" Then
                    'pigeon shipped this part so make it "flow" through our system
                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlCommInsPartOrder As New SqlCommand("update tblpartorder set dateordered=getdate(),arrivedate=getdate() where partid=" & intCKPartID, conn)
                        conn.Open()
                        intCKPartID = sqlCommInsPartOrder.ExecuteScalar()
                    End Using

                End If

            Next

#If DEBUG Then
#Else
            AddToAutoEmail("CK", "BE54ACB4-FACB-4A27-8203-8584590F589A", 1, GetUserEmail(name, client), "sales@ckautoparts.com", intCKOrderID,,,)

#End If



            '***********ck part order record
            If corepickup <> "CK" Then
            Else
                For Each Part As Parts In d
                    'unckeck core recived and update core ship amount

                    If shipfrom <> "CK" Then
                        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                            Dim sqlCommCKOrder As New SqlCommand("Select orderid from tblpartorder where partno ='" & Part.PartNumber & "' and partdescription2 = '" & sn & "'", conn)
                            conn.Open()
                            intCKOrderID = sqlCommCKOrder.ExecuteScalar
                        End Using
                    End If

                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlCommGetCore As New SqlCommand("SELECT tblTierBasePrice.CoreReturnFee FROM tblOrders INNER JOIN tblTierBasePrice INNER JOIN aspnet_Users INNER JOIN aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId ON tblTierBasePrice.TierID = aspnet_Membership.TierID ON tblOrders.Username = aspnet_Users.UserName WHERE (tblOrders.OrderID = '" & orderid & "') AND (tblTierBasePrice.PartType = '" & parttype & "')", conn)
                        conn.Open()
                        decCoreShip = sqlCommGetCore.ExecuteScalar
                    End Using

                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlCommCK As New SqlCommand("Update tblpartorder set corereceived = '0', custcoreshippingprice = '" & decCoreShip & "' where orderid = '" & intCKOrderID & "'", conn)
                        conn.Open()
                        sqlCommCK.ExecuteNonQuery()
                    End Using

                Next
            End If



            '***ck invoicing
            If corepickup <> "CK" Then
            Else

                If decCoreShip > 0 Then
                    'freight invoice client to ck
                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlCommFreight As New SqlCommand("insert into tblinvoices(orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid) values (" & intCKOrderID & ", '" & Now() & "',6,'" & clientckcompanyid & "',192," & decCoreShip & ",0)", conn)
                        conn.Open()
                        sqlCommFreight.ExecuteNonQuery()
                    End Using
                    'core shipper po ck to r&l
                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlCommCoreShipPO As New SqlCommand("insert into tblinvoices(orderid, dateentered, invoicetypeid, payer, payee, Amount, AmountPaid) values (" & intCKOrderID & ", '" & Now() & "',4,192,59," & decCoreShip & ",0)", conn)
                        conn.Open()
                        sqlCommCoreShipPO.ExecuteNonQuery()
                    End Using
                End If
            End If


        Catch Ex As WebException
            Return False
        End Try
        Return True

    End Function
    Public Shared Sub AddToAutoEmail(ByVal client As String, ByVal userid As String, ByVal emailtype As Long, ByVal sendto As String, ByVal sendfrom As String, ByVal orderid As Long, Optional ByVal partid As Long = Nothing, Optional ByVal subjectoverride As String = Nothing, Optional ByVal cc As String = Nothing)
        'add to tblAutoEmail
        Try
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("usp_AddToAutoEmail", conn)
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userid
                sqlComm.Parameters.Add("@emailtype", SqlDbType.Int).Value = emailtype
                sqlComm.Parameters.Add("@sendto", SqlDbType.NVarChar).Value = sendto

                If cc <> Nothing Then sqlComm.Parameters.Add("@cc", SqlDbType.NVarChar).Value = "cc"

                sqlComm.Parameters.Add("@sendfrom", SqlDbType.NVarChar).Value = sendfrom

                Dim strSubject As String = ""
                Select Case emailtype
                    Case 1
                        strSubject = "Your order has been received and is being processed"
                    Case 2
                        strSubject = "Tracking info has been updated for your order"
                    Case 3
                        strSubject = "The core for one or more parts have been picked up"
                    Case 4
                        strSubject = "We have credited you the core for one or more parts"
                End Select
                sqlComm.Parameters.Add("@subject", SqlDbType.NVarChar).Value = strSubject
                If subjectoverride <> Nothing Then sqlComm.Parameters.Add("@subject", SqlDbType.NVarChar).Value = subjectoverride

                sqlComm.Parameters.Add("@orderid", SqlDbType.Int).Value = orderid
                If partid <> Nothing And partid <> 0 Then sqlComm.Parameters.Add("@partid", SqlDbType.Int).Value = partid
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Dim mm As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "james_obrien@ckautoparts.com")

            mm.Subject = "Error Adding to tblAutoEmail"
            mm.Body = String.Format("Message: <br />{0}<br /><br />StackTrace: <br />{1}<br /><br />Client: <br />{2}<br /><br />UserID: <br />{3}<br /><br />emailType: <br />{4}<br /><br />SendTo: <br />{5}<br /><br />SendFrom: <br />{6}<br /><br />OrderID: <br />{7}<br /><br />PartID: <br />{8}<br /><br />Subject Override: <br />{9}<br /><br />CC: <br />{10}", ex.Message, ex.StackTrace, client, userid, emailtype, sendto, sendfrom, orderid, partid, subjectoverride, cc)
            mm.IsBodyHtml = True


            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)

        End Try
    End Sub
    Public Shared Function EmailAddInService(ByVal sn As String, ByVal po As String, ByVal vin As String, ByVal mileage As String, ByVal solddate As String, ByVal client As String)
        On Error GoTo errorz
        GetDefaults(client)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("update tblStock set po = '" & po & "', vin= '" & vin & "', Mileage = '" & mileage & "' where sn ='" & sn & "'", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        'email ck
        Dim mm As New System.Net.Mail.MailMessage(clientckorderemail, "andrew_rand@ckautoparts.com")
        mm.Subject = clientwebsitename & " In Service Info Added"
        Dim strbody As String

        strbody = "Please enter the following in-service info in SMTC for this transmission<br/><br/>"
        strbody = strbody & "SN:" & sn & "<br/>"
        strbody = strbody & "PO:" & po & "<br/>"
        strbody = strbody & "Vin:" & vin & "<br/>"
        strbody = strbody & "Mileage:" & mileage & "<br/>"
        strbody = strbody & "Sold Date:" & solddate & "<br/><br/>"

        mm.Body = strbody
        mm.IsBodyHtml = True

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Host = "smtp.emailsrvr.com"
        smtp.Timeout = 500000
        smtp.Send(mm)


        Return True

        Exit Function
errorz:
        Return True

    End Function

    Public Shared Function GetStateAbbreviation(ByVal state As String) As String
        Dim strAbbreviation As String
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("Select Abbreviation from tblstates where state ='" & state & "'", conn)
            conn.Open()
            strAbbreviation = sqlComm.ExecuteScalar
        End Using

        Return UCase(strAbbreviation)
    End Function
    Public Shared Function GetWebRequest(ByVal formattedUri As String) As HttpWebRequest
        ' Create the request’s URI.
        Dim serviceUri As New Uri(formattedUri, UriKind.Absolute)

        ' Return the HttpWebRequest.
        Return DirectCast(System.Net.WebRequest.Create(serviceUri), HttpWebRequest)
    End Function
    Public Shared Function ToBase64(ByVal data() As Byte) As String
        If data Is Nothing Then Throw New ArgumentNullException("data")
        Return Convert.ToBase64String(data)
    End Function

    Public Shared Function FromBase64(ByVal base64 As String) As Byte()
        If base64 Is Nothing Then Throw New ArgumentNullException("base64")
        Return Convert.FromBase64String(base64)
    End Function

    Public Shared Function GetVinPattern(ByVal vin As String)
        If vin.Length <> 17 Then Return ""
        If UCase(vin).Contains("I") Or UCase(vin).Contains("O") Or UCase(vin).Contains("Q") Then Return ""

        Return Left(vin, 8) & Mid(vin, 10, 2)
    End Function

    Public Shared Function GetVehicleFromVIN(ByVal vin As String) As VINResult

        Dim strPattern As String
        strPattern = GetVinPattern(vin)

        Dim v1 As New VINResult
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select * from VehicleData.dbo.vin_pattern where vin_pattern = '" & strPattern & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    v1.Make = r("Make")
                    v1.Model = r("Model")
                    v1.Year = r("Year")
                    v1.Cylinders = r("DEF_ENGINE_CYLINDERS")
                    v1.Drive = r("DRIVE_TYPE")
                    v1.Liters = r("DEF_ENGINE_SIZE")
                    v1.Style = r("Style")
                    v1.TransmissionSpeed = r("DEF_TRANS_SPEEDS")
                    v1.TransmissionType = r("DEF_TRANS_TYPE")
                    v1.Trim = r("Trim")
                    v1.FuelType = r("FUEL_TYPE")

                End While
            End Using
        End Using
        Return v1
    End Function

    Public Shared Function VendorOverride(ByVal partno As String, ByVal parttype As String, ByVal vendor As Long) As OverridePart
        Dim intOverrideCount As Long
        Dim o1 As New OverridePart

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select count(*) from tblremanoverride where partno = '" & partno & "' and parttype=" & parttype & " and vendor=" & vendor & " and preferred=0", conn)
            conn.Open()
            intOverrideCount = sqlComm.ExecuteScalar
        End Using

        If intOverrideCount = 0 Then
            o1.Override = False
            o1.OriginalPartNo = partno
            o1.OriginalPartVendor = vendor
            o1.PartType = parttype
            Return o1
        Else
            o1.Override = True
            Dim strMatchKey As String

            'first get the match key
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("select matchkey from tblremanoverride where partno = '" & partno & "' and parttype=" & parttype & " and vendor =" & vendor, conn)
                conn.Open()
                strMatchKey = sqlComm.ExecuteScalar
            End Using

            o1.MatchKey = strMatchKey

            'now get preferred vendor info for this key
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("select top 1 partno, vendor from tblremanoverride where matchkey = '" & o1.MatchKey & "' and parttype=" & parttype & " and vendor <>" & vendor & " and preferred=1", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        o1.OriginalPartNo = partno
                        o1.OriginalPartVendor = vendor
                        o1.PartType = parttype
                        o1.OverridePartNo = r("partno")
                        o1.OverridePartVendor = r("vendor")
                    End While
                End Using
            End Using

            Return o1
        End If
    End Function


    Public Shared Function GetPrettyPrice(ByVal partno As String, ByVal parttype As String, ByVal vendor As Long)
        Dim decOverrideSell As Decimal
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select isnull(sellprice,0) as sellprice from tblRemanPrettyPrices where part = '" & partno & "' and parttype = '" & parttype & "' and vendor=" & vendor, conn)
            conn.Open()
            decOverrideSell = sqlComm.ExecuteScalar
        End Using

        Return decOverrideSell
    End Function

    Public Shared Function GetPigeonPrettyPrice(ByVal partno As String, ByVal parttype As String, ByVal vendor As Long, ByVal tierid As Long, ByVal client As String)
        Dim decOverrideSell As Decimal
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(String.Format("select isnull(sellprice,0) as sellprice from tblRemanPrettyPrices where part = '{0}' and parttype = '{1}' and vendor={2} and tierid={3}", partno, parttype, vendor, tierid), conn)
            conn.Open()
            decOverrideSell = sqlComm.ExecuteScalar
        End Using

        Return decOverrideSell
    End Function
    Public Shared Function GetOverridePrice(ByVal partno As String, ByVal parttype As String, ByVal vendor As Long, ByVal name As String, ByVal client As String)
        Dim decOverrideSell As Decimal
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("Select isnull(sellprice, 0) as sellprice from tblRemanOverridePrices where partno = '" & partno & "' and parttype = '" & parttype & "' and vendor=" & vendor & " and customerno='" & IIf(client = "CK", GetCustomerNo(name, client), clientckcustomerno) & "'", conn)
            conn.Open()
            decOverrideSell = sqlComm.ExecuteScalar
        End Using

        Return decOverrideSell
    End Function


    Public Shared Function NoCheckout(ByVal name As String, ByVal client As String) As String
        Dim boolCanOrder As Boolean

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlcomm As New SqlCommand("SELECT canorder from aspnet_membership inner join aspnet_users on aspnet_membership.userid=aspnet_users.userid WHERE username = '" & name & "'", conn)
            conn.Open()
            boolCanOrder = CBool(sqlcomm.ExecuteScalar())
        End Using

        If boolCanOrder = False Then
            Return "yes"
        Else
            Return "no"
        End If
    End Function
    Public Shared Function SearchTransmissionByPartNumber(ByVal partno As String, ByVal client As String, ByVal name As String, ByVal tierID As String)
        Dim Applist As New List(Of Applications)

        Dim PartNoFromApp As String

        Dim TransPrices As New Pricing

        Dim js As New JavaScriptSerializer()
        Dim TransResult = New ArrayList

        PartNoFromApp = partno 'handle road rippers or other part numbers where we have them in pricing but not catalog

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlPartNoFromApp As New SqlCommand("SELECT PartNumber from tblCertifiedCatalog where AppNumber = '" & partno & "'" & " or PartNumber = '" & partno & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlPartNoFromApp.ExecuteReader()
                While r.Read()
                    PartNoFromApp = r("PartNumber").ToString
                End While
            End Using
        End Using




        TransPrices = GetTransmissionPrice(PartNoFromApp, name, client, tierID)

        TransResult.Add(TransPrices)
        Return TransResult
    End Function

    Public Shared Function GetAdminUserByClient(ByVal client As String) As String

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT AdminUser from tblPigeonClients where client ='" & client & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                If r.Read() Then
                    Return r("AdminUser").ToString()
                End If
            End Using
        End Using

        Return String.Empty
    End Function

    Public Shared Function SearchEngineByPartNumber(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim Applist As New List(Of Applications)

        Dim js As New JavaScriptSerializer()
        Dim EngineResult = New ArrayList


        Dim p1 As New Pricing
        Dim tierID As String = GetUserTier(name, client)
        p1 = GetEnginePrice(partno, 75, name, client, tierID)


        EngineResult.Add(p1)
        Return EngineResult
    End Function

    Public Shared Function SearchTransferByPartNumber(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim Applist As New List(Of Applications)
        Dim js As New JavaScriptSerializer()


        Dim strPartNumber = partno


        Dim TransResult = New ArrayList

        Dim p1 As New Pricing
        p1 = GetTransferCasePrice(partno, name, client)
        TransResult.Add(p1)
        Return TransResult
    End Function

    Public Shared Function SearchManualTransmissionByPartNumber(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim Applist As New List(Of Applications)
        Dim js As New JavaScriptSerializer()


        Dim strPartNumber = partno


        Dim TransResult = New ArrayList

        Dim p1 As New Pricing
        p1 = GetManualTransmissionPrice(partno, name, client)
        TransResult.Add(p1)
        Return TransResult
    End Function

    Public Shared Function SearchDiffByPartNumber(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim Applist As New List(Of Applications)
        Dim js As New JavaScriptSerializer()


        Dim strPartNumber = partno
        Dim strDiffType = ""
        Dim DiffResult = New ArrayList

        GetDefaults(client)

        Dim p1 As New Pricing
        p1 = GetDifferentialPrice(partno, name, client)
        DiffResult.Add(p1)

        Return DiffResult
    End Function

    Public Shared Function GetLocalStock(ByVal partno As String, ByVal parttype As String, ByVal client As String)
        Dim intLocalStock As Integer

        Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm8 As New SqlCommand("SELECT count(Part) from tblStock WHERE part = '" & partno & "' AND type = '" & parttype & "' AND Arrive IS NOT NULL AND ReturnType IS NULL and ckorderid is null", conn2)
            conn2.Open()
            intLocalStock = sqlComm8.ExecuteScalar
        End Using

        Return intLocalStock
    End Function


    Public Shared Function CheckForCalc(ByVal Client As String) As Boolean
        Dim js As New JavaScriptSerializer()

        Dim ShowCalculator
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT ShowCalculator FROM tblPigeonClients WHERE Client = '" & Client & "'", conn)
            conn.Open()
            ShowCalculator = CBool(sqlComm.ExecuteScalar())
        End Using

        Return ShowCalculator
    End Function

    Public Shared Function ShowWarrantyPaperwork(ByVal tierid As String, ByVal client As String) As Boolean
        Dim js As New JavaScriptSerializer()

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT warrantypaperwork FROM tbltiers WHERE tierid = '" & tierid & "'", conn)
            conn.Open()
            ShowWarrantyPaperwork = CBool(sqlComm.ExecuteScalar())
        End Using

        Return ShowWarrantyPaperwork
    End Function

    Public Shared Function GetGoogleAnalytics(ByVal client As String) As String
        Dim js As New JavaScriptSerializer()

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT googleanalyticscode FROM tblpigeonclients WHERE client = '" & client & "'", conn)
            conn.Open()
            GetGoogleAnalytics = sqlComm.ExecuteScalar()
        End Using

        Return GetGoogleAnalytics
    End Function
    Public Shared Function TimeAgo([date] As DateTime) As String
        Dim timeSince As TimeSpan = DateTime.Now.Subtract([date])
        If timeSince.TotalMilliseconds < 1 Then
            Return "not yet"
        End If
        If timeSince.TotalMinutes < 1 Then
            Return "just now"
        End If
        If timeSince.TotalMinutes < 2 Then
            Return "1 mi. ago"
        End If
        If timeSince.TotalMinutes < 60 Then
            Return String.Format("{0} mins ago", timeSince.Minutes)
        End If
        If timeSince.TotalMinutes < 120 Then
            Return "1 hour ago"
        End If
        If timeSince.TotalHours < 24 Then
            Return String.Format("{0} hours ago", timeSince.Hours)
        End If
        If timeSince.TotalDays < 2 Then
            Return "yesterday"
        End If
        If timeSince.TotalDays < 7 Then
            Return String.Format("{0} days ago", timeSince.Days)
        End If
        If timeSince.TotalDays < 14 Then
            Return "last week"
        End If
        If timeSince.TotalDays < 21 Then
            Return "2 weeks ago"
        End If
        If timeSince.TotalDays < 28 Then
            Return "3 weeks ago"
        End If
        If timeSince.TotalDays < 60 Then
            Return "last month"
        End If
        If timeSince.TotalDays < 365 Then
            Return String.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30))
        End If
        If timeSince.TotalDays < 730 Then
            Return "last year"
        End If
        'last but not least...
        Return String.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365))
    End Function

    Public Shared Function GetDistributor(ByVal name As String)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("AutoNation"))
            Dim sqlcomm As New SqlCommand("select distributor from tblDealerCentral where username = " & name.fqq, conn)
            conn.Open()
            Try
                Dim strDistributor = sqlcomm.ExecuteScalar()
                If (String.IsNullOrEmpty(strDistributor) = False) Then
                    Return strDistributor
                Else
                    Return ""
                End If

            Catch Ex As WebException
                Return ""
            End Try
        End Using
    End Function
    Public Shared Function GetDistributorWarehouse(ByVal distributor As String, ByVal name As String) As ClosestWarehouse
        Dim objClosestWarehouse As New ClosestWarehouse
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
            Dim sqlcomm As New SqlCommand("select id,state from tblremanwarehouses where active=1 and client = " & distributor.fqq, conn)
            conn.Open()
            Using r As SqlDataReader = sqlcomm.ExecuteReader
                While r.Read()
                    objClosestWarehouse.WarehouseID = r("id").ToString
                    objClosestWarehouse.State = UCase(r("state").ToString)
                    objClosestWarehouse.Schedule = GetWarehouseDeliveryToState(r("id").ToString, GetCustomerState(GetCustomerNo(name, "AutoNation"), "AutoNation"))
                End While
            End Using
            Return objClosestWarehouse
        End Using

    End Function
End Class
Module StringFunctions

    <Extension()>
    Public Function fq(ByVal val As String) As String
        Dim ret As String = ""
        If val Is Nothing OrElse val.Trim() = "" Then
            ret = "NULL"
        Else
            ret = val.Replace("'", "''")
        End If
        Return ret
    End Function

    <Extension()>
    Public Function fqq(ByVal val As String) As String
        Dim ret As String = ""
        If val Is Nothing OrElse val.Trim() = "" Then
            ret = "NULL"
        Else
            ret = "'" & val.Replace("'", "''") & "'"
        End If
        Return ret
    End Function

    <Extension()>
    Public Function fqqjs(ByVal val As String) As String
        Dim ret As String = ""
        If val Is Nothing OrElse val.Trim() = "" Then
            ret = "''"
        Else
            ret = "'" & val.Replace("'", "\'") & "'"
        End If
        Return ret
    End Function

End Module