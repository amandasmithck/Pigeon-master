Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports Pigeon.TransmissionWebService
Imports System.Web.Script.Serialization
Imports Pigeon.OEMWebService
Imports System.Text.RegularExpressions
Imports RateWebServiceClient.RateWebReference
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports Pigeon.CKExtensions
Imports OEMSmallPartPricing.Pricing


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class OrderWebService
    Inherits System.Web.Services.WebService


    Public Class Quote
        Public Property vin As String
        Public Property warranty As String
        Public Property notes As String
        Public Property pigeonCompany As String
        Public Property CustomerContactEmail As String
        Public Property QuoteID() As String
            Get
                Return m_QuoteID
            End Get
            Set(ByVal value As String)
                m_QuoteID = value
            End Set
        End Property
        Private m_QuoteID As String
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
        Public Property Size() As String
            Get
                Return m_Size
            End Get
            Set(ByVal value As String)
                m_Size = value
            End Set
        End Property
        Private m_Size As String
        Public Property Options() As String
            Get
                Return m_Options
            End Get
            Set(ByVal value As String)
                m_Options = value
            End Set
        End Property
        Private m_Options As String
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
        Public Property NationalStock() As String
            Get
                Return m_NationalStock
            End Get
            Set(ByVal value As String)
                m_NationalStock = value
            End Set
        End Property
        Private m_NationalStock As String
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


    Public Class Make
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
    Public Class Model
        Public Property Model() As String
            Get
                Return m_Model
            End Get
            Set(ByVal value As String)
                m_Model = value
            End Set
        End Property
        Private m_Model As String
    End Class
    Public Class SearchResults
        Public Property OrderID() As String
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As String)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As String
        Public Property DateOrdered() As String
            Get
                Return m_DateOrdered
            End Get
            Set(ByVal value As String)
                m_DateOrdered = value
            End Set
        End Property
        Private m_DateOrdered As String
        Public Property Servicer() As String
            Get
                Return m_Servicer
            End Get
            Set(ByVal value As String)
                m_Servicer = value
            End Set
        End Property
        Private m_Servicer As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property AutoYear() As String
            Get
                Return m_AutoYear
            End Get
            Set(ByVal value As String)
                m_AutoYear = value
            End Set
        End Property
        Private m_AutoYear As String
        Public Property AutoMake() As String
            Get
                Return m_AutoMake
            End Get
            Set(ByVal value As String)
                m_AutoMake = value
            End Set
        End Property
        Private m_AutoMake As String
        Public Property AutoModel() As String
            Get
                Return m_AutoModel
            End Get
            Set(ByVal value As String)
                m_AutoModel = value
            End Set
        End Property
        Private m_AutoModel As String
        Public Property Mileage() As String
            Get
                Return m_Mileage
            End Get
            Set(ByVal value As String)
                m_Mileage = value
            End Set
        End Property
        Private m_Mileage As String
        Public Property VinNo() As String
            Get
                Return m_VinNo
            End Get
            Set(ByVal value As String)
                m_VinNo = value
            End Set
        End Property
        Private m_VinNo As String
        Public Property PartDescription() As String
            Get
                Return m_PartDescription
            End Get
            Set(ByVal value As String)
                m_PartDescription = value
            End Set
        End Property
        Private m_PartDescription As String
        Public Property ActiveProblemStatus() As String
            Get
                Return m_ActiveProblemStatus
            End Get
            Set(ByVal value As String)
                m_ActiveProblemStatus = value
            End Set
        End Property
        Private m_ActiveProblemStatus As String
        Public Property Cancelled() As String
            Get
                Return m_Cancelled
            End Get
            Set(ByVal value As String)
                m_Cancelled = value
            End Set
        End Property
        Private m_Cancelled As String
        Public Property Note() As String
            Get
                Return m_Note
            End Get
            Set(ByVal value As String)
                m_Note = value
            End Set
        End Property
        Private m_Note As String
        Public Property Warehouse() As String
            Get
                Return m_Warehouse
            End Get
            Set(ByVal value As String)
                m_Warehouse = value
            End Set
        End Property
        Private m_Warehouse As String
        Public Property SellPrice() As String
            Get
                Return m_SellPrice
            End Get
            Set(ByVal value As String)
                m_SellPrice = value
            End Set
        End Property
        Private m_SellPrice As String
        Public Property CostPrice() As String
            Get
                Return m_CostPrice
            End Get
            Set(ByVal value As String)
                m_CostPrice = value
            End Set
        End Property
        Private m_CostPrice As String
    End Class
    Public Class Arrivals
        Public Property OrderID() As String
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As String)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As String
        Public Property PartID() As String
            Get
                Return m_PartID
            End Get
            Set(ByVal value As String)
                m_PartID = value
            End Set
        End Property
        Private m_PartID As String
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property PartStatus() As String
            Get
                Return m_PartStatus
            End Get
            Set(ByVal value As String)
                m_PartStatus = value
            End Set
        End Property
        Private m_PartStatus As String
        Public Property ComingBack() As String
            Get
                Return m_ComingBack
            End Get
            Set(ByVal value As String)
                m_ComingBack = value
            End Set
        End Property
        Private m_ComingBack As String
        Public Property ExpShipDate() As String
            Get
                Return m_ExpShipDate
            End Get
            Set(ByVal value As String)
                m_ExpShipDate = value
            End Set
        End Property
        Private m_ExpShipDate As String
        Public Property ArrivalDate() As String
            Get
                Return m_ArrivalDate
            End Get
            Set(ByVal value As String)
                m_ArrivalDate = value
            End Set
        End Property
        Private m_ArrivalDate As String
        Public Property FreightETA() As String
            Get
                Return m_FreightETA
            End Get
            Set(ByVal value As String)
                m_FreightETA = value
            End Set
        End Property
        Private m_FreightETA As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property CustomerType() As String
            Get
                Return m_CustomerType
            End Get
            Set(ByVal value As String)
                m_CustomerType = value
            End Set
        End Property
        Private m_CustomerType As String
        Public Property Servicer() As String
            Get
                Return m_Servicer
            End Get
            Set(ByVal value As String)
                m_Servicer = value
            End Set
        End Property
        Private m_Servicer As String
        Public Property State() As String
            Get
                Return m_State
            End Get
            Set(ByVal value As String)
                m_State = value
            End Set
        End Property
        Private m_State As String
        Public Property Vendor() As String
            Get
                Return m_Vendor
            End Get
            Set(ByVal value As String)
                m_Vendor = value
            End Set
        End Property
        Private m_Vendor As String
        Public Property Shipper() As String
            Get
                Return m_Shipper
            End Get
            Set(ByVal value As String)
                m_Shipper = value
            End Set
        End Property
        Private m_Shipper As String
        Public Property Track() As String
            Get
                Return m_Track
            End Get
            Set(ByVal value As String)
                m_Track = value
            End Set
        End Property
        Private m_Track As String
        Public Property TrackUrl() As String
            Get
                Return m_TrackUrl
            End Get
            Set(ByVal value As String)
                m_TrackUrl = value
            End Set
        End Property
        Private m_TrackUrl As String
        Public Property ShipperStatus() As String
            Get
                Return m_ShipperStatus
            End Get
            Set(ByVal value As String)
                m_ShipperStatus = value
            End Set
        End Property
        Private m_ShipperStatus As String
        Public Property Reminder() As String
            Get
                Return m_Reminder
            End Get
            Set(ByVal value As String)
                m_Reminder = value
            End Set
        End Property
        Private m_Reminder As String
        Public Property VendorInvoiceNo() As String
            Get
                Return m_VendorInvoiceNo
            End Get
            Set(ByVal value As String)
                m_VendorInvoiceNo = value
            End Set
        End Property
        Private m_VendorInvoiceNo As String

        Public Property InstallPacket() As String
            Get
                Return m_InstallPacket
            End Get
            Set(ByVal value As String)
                m_InstallPacket = value
            End Set
        End Property
        Private m_InstallPacket As String
    End Class
    Public Class IncompleteReturns
        Public Property OrderID() As String
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As String)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As String
        Public Property PartID() As String
            Get
                Return m_PartID
            End Get
            Set(ByVal value As String)
                m_PartID = value
            End Set
        End Property
        Private m_PartID As String
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property DateOrdered() As String
            Get
                Return m_DateOrdered
            End Get
            Set(ByVal value As String)
                m_DateOrdered = value
            End Set
        End Property
        Private m_DateOrdered As String
        Public Property ComingBack() As String
            Get
                Return m_ComingBack
            End Get
            Set(ByVal value As String)
                m_ComingBack = value
            End Set
        End Property
        Private m_ComingBack As String
        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property PartValue() As String
            Get
                Return m_PartValue
            End Get
            Set(ByVal value As String)
                m_PartValue = value
            End Set
        End Property
        Private m_PartValue As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property CustomerType() As String
            Get
                Return m_CustomerType
            End Get
            Set(ByVal value As String)
                m_CustomerType = value
            End Set
        End Property
        Private m_CustomerType As String
        Public Property Servicer() As String
            Get
                Return m_Servicer
            End Get
            Set(ByVal value As String)
                m_Servicer = value
            End Set
        End Property
        Private m_Servicer As String
        Public Property State() As String
            Get
                Return m_State
            End Get
            Set(ByVal value As String)
                m_State = value
            End Set
        End Property
        Private m_State As String
        Public Property Vendor() As String
            Get
                Return m_Vendor
            End Get
            Set(ByVal value As String)
                m_Vendor = value
            End Set
        End Property
        Private m_Vendor As String
        Public Property FollowUpDate() As String
            Get
                Return m_FollowUpDate
            End Get
            Set(ByVal value As String)
                m_FollowUpDate = value
            End Set
        End Property
        Private m_FollowUpDate As String
        Public Property FollowUpStatus() As String
            Get
                Return m_FollowUpStatus
            End Get
            Set(ByVal value As String)
                m_FollowUpStatus = value
            End Set
        End Property
        Private m_FollowUpStatus As String

        Public Property Reminder() As String
            Get
                Return m_Reminder
            End Get
            Set(ByVal value As String)
                m_Reminder = value
            End Set
        End Property
        Private m_Reminder As String

    End Class
    Public Class Order
        Public Property OrderID() As String
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As String)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As String
        Public Property DateOrdered() As String
            Get
                Return m_DateOrdered
            End Get
            Set(ByVal value As String)
                m_DateOrdered = value
            End Set
        End Property
        Private m_DateOrdered As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property AutoYear() As String
            Get
                Return m_AutoYear
            End Get
            Set(ByVal value As String)
                m_AutoYear = value
            End Set
        End Property
        Private m_AutoYear As String
        Public Property AutoMake() As String
            Get
                Return m_AutoMake
            End Get
            Set(ByVal value As String)
                m_AutoMake = value
            End Set
        End Property
        Private m_AutoMake As String
        Public Property AutoModel() As String
            Get
                Return m_AutoModel
            End Get
            Set(ByVal value As String)
                m_AutoModel = value
            End Set
        End Property
        Private m_AutoModel As String
        Public Property Mileage() As String
            Get
                Return m_Mileage
            End Get
            Set(ByVal value As String)
                m_Mileage = value
            End Set
        End Property
        Private m_Mileage As String
        Public Property VinNo() As String
            Get
                Return m_VinNo
            End Get
            Set(ByVal value As String)
                m_VinNo = value
            End Set
        End Property
        Private m_VinNo As String

    End Class
    Public Class PartOrder

        Private _Row As System.Int64
        Private _Customer As System.String
        Private _PartID As System.Int32
        Private _PreviousPartID As System.Int32
        Private _OrderID As System.Int32
        Private _DateEntered As System.DateTime
        Private _DateOrdered As System.String
        Private _PartNo As System.String
        Private _PartDescription As System.String
        Private _Serial As System.String
        Private _Quantity As System.Int32
        Private _Weight As System.Int32
        Private _Length As System.Decimal
        Private _Width As System.Decimal
        Private _Height As System.Decimal
        Private _VendorID As System.Int32
        Private _Vendor As System.String
        Private _Servicer As System.String
        Private _Address1 As System.String
        Private _City As System.String
        Private _State As System.String
        Private _Zip As System.String
        Private _Phone As System.String
        Private _Fax As System.String
        Private _Contact As System.String
        Private _ContactPhone As System.String
        Private _LaborRate As System.Decimal
        Private _Core As System.Boolean
        Private _SellPrice As System.Decimal
        Private _ShippingPrice As System.Decimal
        Private _ShippingCost As System.Decimal
        Private _CustShippingPrice As System.Decimal
        Private _CustCoreShippingPrice As System.Decimal
        Private _WarrantyCost As System.Decimal
        Private _CorePrice As System.Decimal
        Private _CoreCost As System.Decimal
        Private _Corecredit As System.Decimal
        Private _CostPrice As System.Decimal
        Private _IncorrectDefectCredit As System.Decimal
        Private _CoreCharge As System.Decimal
        Private _FreightInvoice As System.Decimal
        Private _CoreInvoice As System.Decimal
        Private _PartRefund As System.Decimal
        Private _CoreRefund As System.Decimal
        Private _Warranty As System.String
        Private _EOCInfo As System.String
        Private _WarrantyMileage As System.String
        Private _WarrantyDate As System.DateTime
        Private _Arrival As System.String
        Private _UpdatedArrival As System.String
        Private _ArriveDate As System.String
        Private _FreightETA As System.String
        Private _ExpShipDate As System.String
        Private _Reminder As System.String
        Private _ReminderNotes As System.String
        Private _CoreReceived As System.Boolean
        Private _Defect As System.Boolean
        Private _DefectReturned As System.Boolean
        Private _Incorrect As System.Boolean
        Private _IncorrectReturned As System.Boolean
        Private _Cancelled As System.Boolean
        Private _SupplementalPart As System.Boolean
        Private _VendorInvoiceNo As System.String
        Private _ListPrice As System.Decimal
        Private _Warehouse As System.String
        Private _PartType As System.String
        Private _PartDescGroup As System.String
        Private _Brand As System.String

        Public Property Row() As System.Int64
            Get
                Return _Row
            End Get
            Set(ByVal value As System.Int64)
                _Row = value
            End Set
        End Property

        Public Property CustomerID() As Integer
        Public Property Customer() As System.String
            Get
                Return _Customer
            End Get
            Set(ByVal value As System.String)
                _Customer = value
            End Set
        End Property

        Public Property PartID() As System.Int32
            Get
                Return _PartID
            End Get
            Set(ByVal value As System.Int32)
                _PartID = value
            End Set
        End Property

        Public Property PreviousPartID() As System.Int32
            Get
                Return _PreviousPartID
            End Get
            Set(ByVal value As System.Int32)
                _PreviousPartID = value
            End Set
        End Property

        Public Property OrderID() As System.Int32
            Get
                Return _OrderID
            End Get
            Set(ByVal value As System.Int32)
                _OrderID = value
            End Set
        End Property

        Public Property DateEntered() As System.DateTime
            Get
                Return _DateEntered
            End Get
            Set(ByVal value As System.DateTime)
                _DateEntered = value
            End Set
        End Property

        Public Property DateOrdered() As System.String
            Get
                Return _DateOrdered
            End Get
            Set(ByVal value As System.String)
                _DateOrdered = value
            End Set
        End Property

        Public Property PartNo() As System.String
            Get
                Return _PartNo
            End Get
            Set(ByVal value As System.String)
                _PartNo = value
            End Set
        End Property

        Public Property PartDescription() As System.String
            Get
                Return _PartDescription
            End Get
            Set(ByVal value As System.String)
                _PartDescription = value
            End Set
        End Property

        Public Property Serial() As System.String
            Get
                Return _Serial
            End Get
            Set(ByVal value As System.String)
                _Serial = value
            End Set
        End Property

        Public Property Quantity() As System.Int32
            Get
                Return _Quantity
            End Get
            Set(ByVal value As System.Int32)
                _Quantity = value
            End Set
        End Property

        Public Property Weight() As System.Int32
            Get
                Return _Weight
            End Get
            Set(ByVal value As System.Int32)
                _Weight = value
            End Set
        End Property

        Public Property Length() As System.Decimal
            Get
                Return _Length
            End Get
            Set(ByVal value As System.Decimal)
                _Length = value
            End Set
        End Property

        Public Property Width() As System.Decimal
            Get
                Return _Width
            End Get
            Set(ByVal value As System.Decimal)
                _Width = value
            End Set
        End Property

        Public Property Height() As System.Decimal
            Get
                Return _Height
            End Get
            Set(ByVal value As System.Decimal)
                _Height = value
            End Set
        End Property

        Public Property VendorID() As System.Int32
            Get
                Return _VendorID
            End Get
            Set(ByVal value As System.Int32)
                _VendorID = value
            End Set
        End Property

        Public Property Vendor() As System.String
            Get
                Return _Vendor
            End Get
            Set(ByVal value As System.String)
                _Vendor = value
            End Set
        End Property

        Public Property Servicer() As System.String
            Get
                Return _Servicer
            End Get
            Set(ByVal value As System.String)
                _Servicer = value
            End Set
        End Property

        Public Property Address1() As System.String
            Get
                Return _Address1
            End Get
            Set(ByVal value As System.String)
                _Address1 = value
            End Set
        End Property

        Public Property City() As System.String
            Get
                Return _City
            End Get
            Set(ByVal value As System.String)
                _City = value
            End Set
        End Property

        Public Property State() As System.String
            Get
                Return _State
            End Get
            Set(ByVal value As System.String)
                _State = value
            End Set
        End Property

        Public Property Zip() As System.String
            Get
                Return _Zip
            End Get
            Set(ByVal value As System.String)
                _Zip = value
            End Set
        End Property

        Public Property Phone() As System.String
            Get
                Return _Phone
            End Get
            Set(ByVal value As System.String)
                _Phone = value
            End Set
        End Property

        Public Property Fax() As System.String
            Get
                Return _Fax
            End Get
            Set(ByVal value As System.String)
                _Fax = value
            End Set
        End Property

        Public Property Contact() As System.String
            Get
                Return _Contact
            End Get
            Set(ByVal value As System.String)
                _Contact = value
            End Set
        End Property

        Public Property ContactPhone() As System.String
            Get
                Return _ContactPhone
            End Get
            Set(ByVal value As System.String)
                _ContactPhone = value
            End Set
        End Property

        Public Property LaborRate() As System.Decimal
            Get
                Return _LaborRate
            End Get
            Set(ByVal value As System.Decimal)
                _LaborRate = value
            End Set
        End Property

        Public Property Core() As System.Boolean
            Get
                Return _Core
            End Get
            Set(ByVal value As System.Boolean)
                _Core = value
            End Set
        End Property

        Public Property SellPrice() As System.Decimal
            Get
                Return _SellPrice
            End Get
            Set(ByVal value As System.Decimal)
                _SellPrice = value
            End Set
        End Property

        Public Property ShippingPrice() As System.Decimal
            Get
                Return _ShippingPrice
            End Get
            Set(ByVal value As System.Decimal)
                _ShippingPrice = value
            End Set
        End Property

        Public Property ShippingCost() As System.Decimal
            Get
                Return _ShippingCost
            End Get
            Set(ByVal value As System.Decimal)
                _ShippingCost = value
            End Set
        End Property

        Public Property ShipperTrack() As String
        Public Property ShipmentCreatedDisplayValue() As String

        Public Property CustShippingPrice() As System.Decimal
            Get
                Return _CustShippingPrice
            End Get
            Set(ByVal value As System.Decimal)
                _CustShippingPrice = value
            End Set
        End Property

        Public Property CustCoreShippingPrice() As System.Decimal
            Get
                Return _CustCoreShippingPrice
            End Get
            Set(ByVal value As System.Decimal)
                _CustCoreShippingPrice = value
            End Set
        End Property

        Public Property WarrantyCost() As System.Decimal
            Get
                Return _WarrantyCost
            End Get
            Set(ByVal value As System.Decimal)
                _WarrantyCost = value
            End Set
        End Property

        Public Property CorePrice() As System.Decimal
            Get
                Return _CorePrice
            End Get
            Set(ByVal value As System.Decimal)
                _CorePrice = value
            End Set
        End Property

        Public Property CoreCost() As System.Decimal
            Get
                Return _CoreCost
            End Get
            Set(ByVal value As System.Decimal)
                _CoreCost = value
            End Set
        End Property

        Public Property CoreCredit() As System.Decimal
            Get
                Return _Corecredit
            End Get
            Set(ByVal value As System.Decimal)
                _Corecredit = value
            End Set
        End Property

        Public Property IncorrectDefectCredit() As System.Decimal
            Get
                Return _IncorrectDefectCredit
            End Get
            Set(ByVal value As System.Decimal)
                _IncorrectDefectCredit = value
            End Set
        End Property

        Public Property CoreCharge() As System.Decimal
            Get
                Return _CoreCharge
            End Get
            Set(ByVal value As System.Decimal)
                _CoreCharge = value
            End Set
        End Property

        Public Property FreightInvoice() As System.Decimal
            Get
                Return _FreightInvoice
            End Get
            Set(ByVal value As System.Decimal)
                _FreightInvoice = value
            End Set
        End Property

        Public Property CoreInvoice() As System.Decimal
            Get
                Return _CoreInvoice
            End Get
            Set(ByVal value As System.Decimal)
                _CoreInvoice = value
            End Set
        End Property

        Public Property PartRefund() As System.Decimal
            Get
                Return _PartRefund
            End Get
            Set(ByVal value As System.Decimal)
                _PartRefund = value
            End Set
        End Property

        Public Property CoreRefund() As System.Decimal
            Get
                Return _CoreRefund
            End Get
            Set(ByVal value As System.Decimal)
                _CoreRefund = value
            End Set
        End Property

        Public Property CostPrice() As System.Decimal
            Get
                Return _CostPrice
            End Get
            Set(ByVal value As System.Decimal)
                _CostPrice = value
            End Set
        End Property

        Public Property Warranty() As System.String
            Get
                Return _Warranty
            End Get
            Set(ByVal value As System.String)
                _Warranty = value
            End Set
        End Property

        Public Property EOCInfo() As System.String
            Get
                Return _EOCInfo
            End Get
            Set(ByVal value As System.String)
                _EOCInfo = value
            End Set
        End Property

        Public Property WarrantyMileage() As System.String
            Get
                Return _WarrantyMileage
            End Get
            Set(ByVal value As System.String)
                _WarrantyMileage = value
            End Set
        End Property

        Public Property WarrantyDate() As System.DateTime
            Get
                Return _WarrantyDate
            End Get
            Set(ByVal value As System.DateTime)
                _WarrantyDate = value
            End Set
        End Property

        Public Property Arrival() As System.String
            Get
                Return _Arrival
            End Get
            Set(ByVal value As System.String)
                _Arrival = value
            End Set
        End Property

        Public Property UpdatedArrival() As System.String
            Get
                Return _UpdatedArrival
            End Get
            Set(ByVal value As System.String)
                _UpdatedArrival = value
            End Set
        End Property

        Public Property ArriveDate() As System.String
            Get
                Return _ArriveDate
            End Get
            Set(ByVal value As System.String)
                _ArriveDate = value
            End Set
        End Property

        Public Property FreightETA() As System.String
            Get
                Return _FreightETA
            End Get
            Set(ByVal value As System.String)
                _FreightETA = value
            End Set
        End Property

        Public Property ExpShipDate() As System.String
            Get
                Return _ExpShipDate
            End Get
            Set(ByVal value As System.String)
                _ExpShipDate = value
            End Set
        End Property

        Public Property Reminder() As System.String
            Get
                Return _Reminder
            End Get
            Set(ByVal value As System.String)
                _Reminder = value
            End Set
        End Property

        Public Property ReminderNotes() As System.String
            Get
                Return _ReminderNotes
            End Get
            Set(ByVal value As System.String)
                _ReminderNotes = value
            End Set
        End Property

        Public Property CoreReceived() As System.Boolean
            Get
                Return _CoreReceived
            End Get
            Set(ByVal value As System.Boolean)
                _CoreReceived = value
            End Set
        End Property

        Public Property Defect() As System.Boolean
            Get
                Return _Defect
            End Get
            Set(ByVal value As System.Boolean)
                _Defect = value
            End Set
        End Property

        Public Property DefectReturned() As System.Boolean
            Get
                Return _DefectReturned
            End Get
            Set(ByVal value As System.Boolean)
                _DefectReturned = value
            End Set
        End Property

        Public Property Incorrect() As System.Boolean
            Get
                Return _Incorrect
            End Get
            Set(ByVal value As System.Boolean)
                _Incorrect = value
            End Set
        End Property

        Public Property IncorrectReturned() As System.Boolean
            Get
                Return _IncorrectReturned
            End Get
            Set(ByVal value As System.Boolean)
                _IncorrectReturned = value
            End Set
        End Property

        Public Property Cancelled() As System.Boolean
            Get
                Return _Cancelled
            End Get
            Set(ByVal value As System.Boolean)
                _Cancelled = value
            End Set
        End Property

        Public Property SupplementalPart() As System.Boolean
            Get
                Return _SupplementalPart
            End Get
            Set(ByVal value As System.Boolean)
                _SupplementalPart = value
            End Set
        End Property

        Public Property VendorInvoiceNo() As System.String
            Get
                Return _VendorInvoiceNo
            End Get
            Set(ByVal value As System.String)
                _VendorInvoiceNo = value
            End Set
        End Property

        Public Property ListPrice() As System.Decimal
            Get
                Return _ListPrice
            End Get
            Set(ByVal value As System.Decimal)
                _ListPrice = value
            End Set
        End Property

        Public Property Warehouse() As System.String
            Get
                Return _Warehouse
            End Get
            Set(ByVal value As System.String)
                _Warehouse = value
            End Set
        End Property

        Public Property PartType() As System.String
            Get
                Return _PartType
            End Get
            Set(ByVal value As System.String)
                _PartType = value
            End Set
        End Property

        Public Property PartDescGroup() As System.String
            Get
                Return _PartDescGroup
            End Get
            Set(ByVal value As System.String)
                _PartDescGroup = value
            End Set
        End Property

        Public Property Brand() As System.String
            Get
                Return _Brand
            End Get
            Set(ByVal value As System.String)
                _Brand = value
            End Set
        End Property

    End Class
    Public Class InvoiceType

        Private _InvoiceTypeID As System.Int32
        Private _InvoiceType As System.String

        Public Property InvoiceTypeID() As System.Int32
            Get
                Return _InvoiceTypeID
            End Get
            Set(ByVal value As System.Int32)
                _InvoiceTypeID = value
            End Set
        End Property

        Public Property InvoiceType() As System.String
            Get
                Return _InvoiceType
            End Get
            Set(ByVal value As System.String)
                _InvoiceType = value
            End Set
        End Property

    End Class
    Public Class Notes

        Private _NoteID As System.Int32
        Private _OrderID As System.Int32
        Private _NoteDate As System.String
        Private _NoteUser As System.String
        Private _Notes As System.String
        Private _type As System.String
        Private _Vendor As System.Boolean

        Public Property NoteID() As System.Int32
            Get
                Return _NoteID
            End Get
            Set(ByVal value As System.Int32)
                _NoteID = value
            End Set
        End Property

        Public Property OrderID() As System.Int32
            Get
                Return _OrderID
            End Get
            Set(ByVal value As System.Int32)
                _OrderID = value
            End Set
        End Property

        Public Property NoteDate() As System.String
            Get
                Return _NoteDate
            End Get
            Set(ByVal value As System.String)
                _NoteDate = value
            End Set
        End Property

        Public Property NoteUser() As System.String
            Get
                Return _NoteUser
            End Get
            Set(ByVal value As System.String)
                _NoteUser = value
            End Set
        End Property

        Public Property Notes() As System.String
            Get
                Return _Notes
            End Get
            Set(ByVal value As System.String)
                _Notes = value
            End Set
        End Property

        Public Property type() As System.String
            Get
                Return _type
            End Get
            Set(ByVal value As System.String)
                _type = value
            End Set
        End Property

        Public Property Vendor() As System.Boolean
            Get
                Return _Vendor
            End Get
            Set(ByVal value As System.Boolean)
                _Vendor = value
            End Set
        End Property

    End Class
    Public Class History

        Private _TrackDate As System.String
        Private _TrackUser As System.String
        Private _TrackAction As System.String


        Public Property TrackDate() As System.String
            Get
                Return _TrackDate
            End Get
            Set(ByVal value As System.String)
                _TrackDate = value
            End Set
        End Property

        Public Property TrackUser() As System.String
            Get
                Return _TrackUser
            End Get
            Set(ByVal value As System.String)
                _TrackUser = value
            End Set
        End Property

        Public Property TrackAction() As System.String
            Get
                Return _TrackAction
            End Get
            Set(ByVal value As System.String)
                _TrackAction = value
            End Set
        End Property

    End Class
    Public Class Header

        Private _Customer As System.String
        Private _Vendor As System.String
        Private _Vehicle As System.String
        Private _VinNo As System.String
        Private _Mileage As System.String
        Private _DateOrdered As System.String
        Private _Cancelled As System.Boolean

        Public Property Customer() As System.String
            Get
                Return _Customer
            End Get
            Set(ByVal value As System.String)
                _Customer = value
            End Set
        End Property

        Public Property Vendor() As System.String
            Get
                Return _Vendor
            End Get
            Set(ByVal value As System.String)
                _Vendor = value
            End Set
        End Property
        Public Property Vehicle() As System.String
            Get
                Return _Vehicle
            End Get
            Set(ByVal value As System.String)
                _Vehicle = value
            End Set
        End Property
        Public Property VinNo() As System.String
            Get
                Return _VinNo
            End Get
            Set(ByVal value As System.String)
                _VinNo = value
            End Set
        End Property
        Public Property Mileage() As System.String
            Get
                Return _Mileage
            End Get
            Set(ByVal value As System.String)
                _Mileage = value
            End Set
        End Property

        Public Property DateOrdered() As System.String
            Get
                Return _DateOrdered
            End Get
            Set(ByVal value As System.String)
                _DateOrdered = value
            End Set
        End Property

        Public Property Cancelled() As System.Boolean
            Get
                Return _Cancelled
            End Get
            Set(ByVal value As System.Boolean)
                _Cancelled = value
            End Set
        End Property

    End Class
    Public Class Document

        Private _uploaddate As System.String
        Private _filename As System.String
        Private _uploadedby As System.String
        Private _url As System.String
        Private _thumbnailurl As System.String
        Private _docid As System.Int64

        Public Property uploaddate() As System.String
            Get
                Return _uploaddate
            End Get
            Set(ByVal value As System.String)
                _uploaddate = value
            End Set
        End Property

        Public Property filename() As System.String
            Get
                Return _filename
            End Get
            Set(ByVal value As System.String)
                _filename = value
            End Set
        End Property

        Public Property uploadedby() As System.String
            Get
                Return _uploadedby
            End Get
            Set(ByVal value As System.String)
                _uploadedby = value
            End Set
        End Property
        Public Property url() As System.String
            Get
                Return _url
            End Get
            Set(ByVal value As System.String)
                _url = value
            End Set
        End Property
        Public Property thumbnailurl() As System.String
            Get
                Return _thumbnailurl
            End Get
            Set(ByVal value As System.String)
                _thumbnailurl = value
            End Set
        End Property
        Public Property docid() As System.Int64
            Get
                Return _docid
            End Get
            Set(ByVal value As System.Int64)
                _docid = value
            End Set
        End Property

    End Class

    <WebMethod()>
    Public Function GetInvoiceTypes(ByVal client As String)
        Dim list As New List(Of InvoiceType)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = "select InvoiceTypeID, InvoiceType from tblInvoiceType where bizexpense =0 order by invoicetype"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim i1 As New InvoiceType
                    Dim objType As Type = i1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(i1, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(i1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetMakes()
        Dim list As New List(Of Make)
        Dim js As New JavaScriptSerializer
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT [Make] FROM [tblMake] where make is not null ORDER BY [Make]", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    'see if this make is excluded
                    Dim m1 As New Make
                    m1.Make = r("make")
                    list.Add(m1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetModels()
        Dim list As New List(Of Model)
        Dim js As New JavaScriptSerializer
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT Model FROM dbo.tblModel where model is not null GROUP BY Model ORDER BY Model", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    'see if this make is excluded
                    Dim m1 As New Model
                    m1.Model = r("model")
                    list.Add(m1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetVendors()

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            strSql = "SELECT Company,Companyid from tblCompany where company is not null and active =1 and type ='Vendor' order by Company"

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
    Public Function GetNonCustomers()

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            strSql = "SELECT Company + '-' + Type as Company,Companyid from tblCompany where company is not null and active =1 and type <>'Customer' order by Company"

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
    Public Class Invoice

        Private _InvoiceID As System.Int32
        Private _OrderID As System.Int32
        Private _PartID As System.Int32
        Private _DateEntered As System.String
        Private _InvoiceNo As System.String
        Private _InvoiceType As System.String
        Private _Company As System.String
        Private _Amount As System.Decimal
        Private _AmountPaid As System.Decimal
        Private _DatePaid As System.String
        Private _PaymentType As System.String
        Private _CheckNo As System.String
        Private _InvoiceTypeID As System.Int32
        Private _CCDatePaid As System.DateTime
        Private _PayerID As System.Int32
        Private _PayeeID As System.Int32
        Private _Deleted As System.Boolean
        Private _Flow As System.String

        Public Property InvoiceID() As System.Int32
            Get
                Return _InvoiceID
            End Get
            Set(ByVal value As System.Int32)
                _InvoiceID = value
            End Set
        End Property

        Public Property OrderID() As System.Int32
            Get
                Return _OrderID
            End Get
            Set(ByVal value As System.Int32)
                _OrderID = value
            End Set
        End Property

        Public Property PartID() As System.Int32
            Get
                Return _PartID
            End Get
            Set(ByVal value As System.Int32)
                _PartID = value
            End Set
        End Property

        Public Property DateEntered() As System.String
            Get
                Return _DateEntered
            End Get
            Set(ByVal value As System.String)
                _DateEntered = value
            End Set
        End Property

        Public Property InvoiceNo() As System.String
            Get
                Return _InvoiceNo
            End Get
            Set(ByVal value As System.String)
                _InvoiceNo = value
            End Set
        End Property

        Public Property InvoiceType() As System.String
            Get
                Return _InvoiceType
            End Get
            Set(ByVal value As System.String)
                _InvoiceType = value
            End Set
        End Property

        Public Property Flow() As System.String
            Get
                Return _Flow
            End Get
            Set(ByVal value As System.String)
                _Flow = value
            End Set
        End Property

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
            End Set
        End Property

        Public Property Amount() As System.Decimal
            Get
                Return _Amount
            End Get
            Set(ByVal value As System.Decimal)
                _Amount = value
            End Set
        End Property

        Public Property AmountPaid() As System.Decimal
            Get
                Return _AmountPaid
            End Get
            Set(ByVal value As System.Decimal)
                _AmountPaid = value
            End Set
        End Property

        Public Property DatePaid() As System.String
            Get
                Return _DatePaid
            End Get
            Set(ByVal value As System.String)
                _DatePaid = value
            End Set
        End Property

        Public Property PaymentType() As System.String
            Get
                Return _PaymentType
            End Get
            Set(ByVal value As System.String)
                _PaymentType = value
            End Set
        End Property

        Public Property CheckNo() As System.String
            Get
                Return _CheckNo
            End Get
            Set(ByVal value As System.String)
                _CheckNo = value
            End Set
        End Property

        Public Property InvoiceTypeID() As System.Int32
            Get
                Return _InvoiceTypeID
            End Get
            Set(ByVal value As System.Int32)
                _InvoiceTypeID = value
            End Set
        End Property

        Public Property CCDatePaid() As System.DateTime
            Get
                Return _CCDatePaid
            End Get
            Set(ByVal value As System.DateTime)
                _CCDatePaid = value
            End Set
        End Property

        Public Property PayerID() As System.Int32
            Get
                Return _PayerID
            End Get
            Set(ByVal value As System.Int32)
                _PayerID = value
            End Set
        End Property

        Public Property PayeeID() As System.Int32
            Get
                Return _PayeeID
            End Get
            Set(ByVal value As System.Int32)
                _PayeeID = value
            End Set
        End Property

        Public Property Deleted() As System.Boolean
            Get
                Return _Deleted
            End Get
            Set(ByVal value As System.Boolean)
                _Deleted = value
            End Set
        End Property

    End Class
    Public Class OrderInfo

        Private _OrderID As System.Int32
        Private _CustomerNo As System.String
        Private _CompanyID As System.Int32
        Private _DateOrdered As System.String
        Private _Company As System.String
        Private _AdjusterName As System.String
        Private _ContractNo As System.String
        Private _AuthorizationNo As System.String
        Private _AutoOwner As System.String
        Private _ContractEndDate As System.String
        Private _ContractEndMiles As System.String
        Private _Mileage As System.String
        Private _AutoYear As System.String
        Private _AutoMake As System.String
        Private _AutoModel As System.String
        Private _Drive As System.String
        Private _Transmission As System.String
        Private _VinNo As System.String

        Public Property OrderID() As System.Int32
            Get
                Return _OrderID
            End Get
            Set(ByVal value As System.Int32)
                _OrderID = value
            End Set
        End Property

        Public Property CustomerNo() As System.String
            Get
                Return _CustomerNo
            End Get
            Set(ByVal value As System.String)
                _CustomerNo = value
            End Set
        End Property

        Public Property CompanyID() As System.Int32
            Get
                Return _CompanyID
            End Get
            Set(ByVal value As System.Int32)
                _CompanyID = value
            End Set
        End Property

        Public Property DateOrdered() As System.String
            Get
                Return _DateOrdered
            End Get
            Set(ByVal value As System.String)
                _DateOrdered = value
            End Set
        End Property

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
            End Set
        End Property

        Public Property AdjusterName() As System.String
            Get
                Return _AdjusterName
            End Get
            Set(ByVal value As System.String)
                _AdjusterName = value
            End Set
        End Property

        Public Property ContractNo() As System.String
            Get
                Return _ContractNo
            End Get
            Set(ByVal value As System.String)
                _ContractNo = value
            End Set
        End Property

        Public Property AuthorizationNo() As System.String
            Get
                Return _AuthorizationNo
            End Get
            Set(ByVal value As System.String)
                _AuthorizationNo = value
            End Set
        End Property

        Public Property AutoOwner() As System.String
            Get
                Return _AutoOwner
            End Get
            Set(ByVal value As System.String)
                _AutoOwner = value
            End Set
        End Property

        Public Property ContractEndDate() As System.String
            Get
                Return _ContractEndDate
            End Get
            Set(ByVal value As System.String)
                _ContractEndDate = value
            End Set
        End Property

        Public Property ContractEndMiles() As System.String
            Get
                Return _ContractEndMiles
            End Get
            Set(ByVal value As System.String)
                _ContractEndMiles = value
            End Set
        End Property

        Public Property Mileage() As System.String
            Get
                Return _Mileage
            End Get
            Set(ByVal value As System.String)
                _Mileage = value
            End Set
        End Property

        Public Property AutoYear() As System.String
            Get
                Return _AutoYear
            End Get
            Set(ByVal value As System.String)
                _AutoYear = value
            End Set
        End Property

        Public Property AutoMake() As System.String
            Get
                Return _AutoMake
            End Get
            Set(ByVal value As System.String)
                _AutoMake = value
            End Set
        End Property

        Public Property AutoModel() As System.String
            Get
                Return _AutoModel
            End Get
            Set(ByVal value As System.String)
                _AutoModel = value
            End Set
        End Property

        Public Property Drive() As System.String
            Get
                Return _Drive
            End Get
            Set(ByVal value As System.String)
                _Drive = value
            End Set
        End Property

        Public Property Transmission() As System.String
            Get
                Return _Transmission
            End Get
            Set(ByVal value As System.String)
                _Transmission = value
            End Set
        End Property

        Public Property VinNo() As System.String
            Get
                Return _VinNo
            End Get
            Set(ByVal value As System.String)
                _VinNo = value
            End Set
        End Property

    End Class

    Public Class OEMAvailability

        Public Property Hyperion() As String
            Get
                Return m_Hyperion
            End Get
            Set(ByVal value As String)
                m_Hyperion = value
            End Set
        End Property
        Private m_Hyperion As String
        Public Property Cutoff() As String
            Get
                Return m_Cutoff
            End Get
            Set(ByVal value As String)
                m_Cutoff = value
            End Set
        End Property
        Private m_Cutoff As String
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String
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
        Public Property Note() As String
            Get
                Return m_Note
            End Get
            Set(ByVal value As String)
                m_Note = value
            End Set
        End Property
        Private m_Note As String
        Public Property Parts() As List(Of PartsResponse)
            Get
                Return m_Parts
            End Get
            Set(ByVal value As List(Of PartsResponse))
                m_Parts = value
            End Set
        End Property
        Private m_Parts As List(Of PartsResponse)
        Public Property Status() As String
            Get
                Return m_Status
            End Get
            Set(ByVal value As String)
                m_Status = value
            End Set
        End Property
        Private m_Status As String

    End Class

    Public Class SmallPartQuoteOption

        Public Property ID() As Long
            Get
                Return m_ID
            End Get
            Set(ByVal value As Long)
                m_ID = value
            End Set
        End Property
        Private m_ID As Long
        Public Property Vendor() As String
            Get
                Return m_Vendor
            End Get
            Set(ByVal value As String)
                m_Vendor = value
            End Set
        End Property
        Private m_Vendor As String
        Public Property PartNo() As String
            Get
                Return m_PartNo
            End Get
            Set(ByVal value As String)
                m_PartNo = value
            End Set
        End Property
        Private m_PartNo As String
        Public Property PartDescription() As String
            Get
                Return m_PartDescription
            End Get
            Set(ByVal value As String)
                m_PartDescription = value
            End Set
        End Property
        Private m_PartDescription As String

        Public Property Brand() As String
            Get
                Return m_Brand
            End Get
            Set(ByVal value As String)
                m_Brand = value
            End Set
        End Property
        Private m_Brand As String

        Public Property OurCost() As Decimal
            Get
                Return m_OurCost
            End Get
            Set(ByVal value As Decimal)
                m_OurCost = value
            End Set
        End Property
        Private m_OurCost As String

        Public Property CorePrice() As Decimal
            Get
                Return m_CorePrice
            End Get
            Set(ByVal value As Decimal)
                m_CorePrice = value
            End Set
        End Property
        Private m_CorePrice As String

        Public Property TheirPrice() As Decimal
            Get
                Return m_TheirPrice
            End Get
            Set(ByVal value As Decimal)
                m_TheirPrice = value
            End Set
        End Property
        Private m_TheirPrice As String

        Public Property Stock() As String
            Get
                Return m_Stock
            End Get
            Set(ByVal value As String)
                m_Stock = value
            End Set
        End Property
        Private m_Stock As String

        Public Property StockException() As Boolean
            Get
                Return m_StockException
            End Get
            Set(ByVal value As Boolean)
                m_StockException = value
            End Set
        End Property
        Private m_StockException As Boolean

        Public Property Matched() As Boolean
            Get
                Return m_Matched
            End Get
            Set(ByVal value As Boolean)
                m_Matched = value
            End Set
        End Property
        Private m_Matched As Boolean


    End Class
    Public Class SmallPartOption

        Public Property PartID() As Long
            Get
                Return m_PartID
            End Get
            Set(ByVal value As Long)
                m_PartID = value
            End Set
        End Property
        Private m_PartID As Long
        Public Property PartNo() As String
            Get
                Return m_PartNo
            End Get
            Set(ByVal value As String)
                m_PartNo = value
            End Set
        End Property
        Private m_PartNo As String
        Public Property PartDescription() As String
            Get
                Return m_PartDescription
            End Get
            Set(ByVal value As String)
                m_PartDescription = value
            End Set
        End Property
        Private m_PartDescription As String

        Public Property Vendor() As String
            Get
                Return m_Vendor
            End Get
            Set(ByVal value As String)
                m_Vendor = value
            End Set
        End Property
        Private m_Vendor As String

        Public Property Options() As List(Of SmallPartQuoteOption)
            Get
                Return m_Options
            End Get
            Set(ByVal value As List(Of SmallPartQuoteOption))
                m_Options = value
            End Set
        End Property
        Private m_Options As List(Of SmallPartQuoteOption)


    End Class

    <WebMethod()>
    Public Function NewManualQuote(ByVal customerNo As String, ByVal customerEmail As String, ByVal partTypeID As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal part As String, ByVal sellPrice As String, ByVal enteredBy As String, ByVal client As String)
        Dim js As New JavaScriptSerializer
        Try

            Dim priceInfo As New Pricing()
            priceInfo.core = 0
            ProcessQuote(customerNo, customerEmail, enteredBy, year, make, model, Nothing, Nothing, Nothing, client, part, Nothing, sellPrice, priceInfo, DirectCast(Convert.ToInt32(partTypeID), Enums.PartTypes), Nothing, Nothing)
            Return js.Serialize(True)
        Catch ex As Exception
            ex.wrangle()
            Return js.Serialize(False)
        End Try

    End Function
    <WebMethod()>
    Public Function NewManualOrder(ByVal customer As String, ByVal thirdparty As String, ByVal adjuster As String, ByVal adjusteremail As String, ByVal contractno As String, ByVal authno As String, ByVal owner As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal vin As String, ByVal mileage As String, ByVal drive As String, ByVal transmission As String, ByVal failure As String, ByVal servicer As String, ByVal address As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal phone As String, ByVal contact As String, ByVal part As String, ByVal quantity As String, ByVal sellprice As String, ByVal additional As String, ByVal vendor As String, ByVal etadate As String, ByVal warranty As String, ByVal warrantydate As String, ByVal warrantymileage As String, ByVal otherpart As String, ByVal enteredby As String, ByVal partdescgroup As String)
        Dim intOrderID As Long
        Try
            'order table
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim strsqlOrder = "Insert into tblOrder (AuthorizationNo, ContractNo, VInNo, AutoYear, AutoMake, AutoModel, AutoOwner, CustomerNo, DateOrdered, EnteredBy, Mileage, JustaField, AdjusterName, AdjusterEmail, Notes, drive, transmission, contractenddate, contractendmiles) values ('" & authno & "', '" & contractno & "', '" & vin & "', '" & year & "', '" & make & "', '" & model & "', '" & owner & "', '" & customer & "','" & Now() & "', '" & enteredby & "', '" & mileage & "',1,'" & adjuster & "', '" & adjusteremail & "', '" & failure.Replace("'", "") & "','" & drive & "','" & transmission & "','" & warrantydate & "','" & warrantymileage & "');SELECT SCOPE_IDENTITY()"
                Dim sqlCommOrder As New SqlCommand(strsqlOrder, conn)
                conn.Open()
                intOrderID = sqlCommOrder.ExecuteScalar()
            End Using

            'part order table
            Dim decCustShippingPrice As Decimal
            Dim strCore As String
            Dim intCoreShipper As Integer
            Dim decWarrantyCost As Decimal
            If customer = "33764" And vendor = 13081 Then 'autoway parts as customer and certified-autoway as vendor
                decCustShippingPrice = 80
            Else
                decCustShippingPrice = 0
            End If

            If vendor <> 10827 Then
                strCore = GetCore(vendor)
            Else
                strCore = 0
            End If

            If customer = "33764" And vendor = 13081 Then 'autoway parts as customer and certified-autoway as vendor
                intCoreShipper = 309
            End If

            If warranty = "EOC Plus 100" Then
                decWarrantyCost = 100
            Else
                decWarrantyCost = 0
            End If

            'todo: orderscreen launch-remove warrantydate and warranty mileage from these insert statements and do a mass update from tblpartorder
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim strsqlPartOrder = "Insert into tblPartOrder(Orderid, Address1, City, Contact, DateEntered, EnteredBy, PartDescription, PartNO, Phone, Quantity, SellPrice, Servicer, State, Vendor, Zip, Warranty, WarrantyDate, WarrantyMileage,arrivaldate, CustShippingPrice, Core,CoreShipper,WarrantyCost, PartDescGroup, PartType) values (" & intOrderID & ",'" & address.Replace("'", "") & "','" & city & "','" & contact & "','" & Now() & "','" & enteredby & "', '" & part & "', '" & Nothing & "','" & phone & "','" & quantity & "','" & sellprice & "','" & servicer.Replace("'", "") & "','" & state & "','" & vendor & "', '" & zip & "','" & warranty & "','" & warrantydate & "','" & warrantymileage & "','" & etadate & "','" & decCustShippingPrice & "','" & strCore & "','" & intCoreShipper & "','" & decWarrantyCost & "','" & partdescgroup & "','Aftermarket')"
                Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                conn.Open()
                sqlCommPartOrder.ExecuteNonQuery()

                'additional parts
                If additional > 0 Then
                    For x As Integer = 0 To additional - 1
                        Dim strsqlPartOrderAdd = "Insert into tblPartOrder(Orderid, Address1, City, Contact, DateEntered, EnteredBy, PartDescription, PartNO, Phone, Quantity, SellPrice, Servicer, State, Vendor, Zip, Warranty, WarrantyDate, WarrantyMileage,arrivaldate, CustShippingPrice, Core,CoreShipper,WarrantyCost) values (" & intOrderID & ",'" & address.Replace("'", "") & "','" & city & "','" & contact & "','" & Now() & "','" & enteredby & "', 'No Part Yet', '" & Nothing & "','" & phone & "','" & quantity & "','0','" & servicer.Replace("'", "") & "','" & state & "','" & vendor & "', '" & zip & "','" & warranty & "','" & warrantydate & "','" & warrantymileage & "','" & etadate & "','" & decCustShippingPrice & "','" & strCore & "','" & intCoreShipper & "','" & decWarrantyCost & "')"
                        Dim sqlCommPartOrderAdd As New SqlCommand(strsqlPartOrderAdd, conn)
                        sqlCommPartOrderAdd.ExecuteNonQuery()
                    Next
                End If
            End Using

            '3rd party autonation
            If thirdparty <> "10341" And thirdparty <> "" And IsDBNull(thirdparty) = False And IsNothing(thirdparty) = False Then
                'add ck invoice
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim sqlCommCopyOrder As New SqlCommand("usp_InvCKThird", conn)
                    sqlCommCopyOrder.CommandType = CommandType.StoredProcedure
                    sqlCommCopyOrder.Parameters.Add("@orderid", SqlDbType.Int)
                    sqlCommCopyOrder.Parameters("@orderid").Value = intOrderID
                    sqlCommCopyOrder.Parameters.Add("@excepuser", SqlDbType.NVarChar)
                    sqlCommCopyOrder.Parameters("@excepuser").Value = enteredby
                    sqlCommCopyOrder.Parameters.Add("@thirdid", SqlDbType.Int)
                    sqlCommCopyOrder.Parameters("@thirdid").Value = thirdparty
                    conn.Open()
                    sqlCommCopyOrder.ExecuteNonQuery()
                End Using

            End If

            'orginal faliure to notes
            If IsDBNull(failure) = False And failure <> "" Then
                InsertNote(intOrderID, "Original Failure-" & failure.Replace("'", ""), False, enteredby)
            End If
            'add note if applicable
            If part = "Other" Or part = "Used Other" Then
                InsertNote(intOrderID, "Other Description-" & otherpart, False, enteredby)
            End If
            Return intOrderID


        Catch Ex As Exception
            Console.WriteLine(Ex)
            Return False
        End Try
    End Function
    Public Shared Function InsertNote(ByVal orderid As Long, ByVal note As String, ByVal vendor As Boolean, ByVal enteredby As String)
        Try
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommInsertNote As New SqlCommand("Insert into tblNotes(orderid, notedate,notes,vendor,noteuser) values (" & orderid & ", { fn now() },'" & note.Replace("'", "") & "'," & IIf(vendor = False, 0, 1) & ",'" & enteredby & "')", conn)

                conn.Open()
                sqlCommInsertNote.ExecuteNonQuery()
                Return True
            End Using
        Catch Ex As Exception
            Return Ex.ToString()
        End Try

    End Function
    Private Function GetCore(ByVal CompanyID As Integer)

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim strCore = "select autochkcore from tblinvguide where companyid=" & CompanyID
            Dim sqlCore As New SqlCommand(strCore, conn)
            conn.Open()
            Dim AutoChkCore As Boolean
            AutoChkCore = CBool(sqlCore.ExecuteScalar())
            Return AutoChkCore
        End Using
    End Function


    <WebMethod()>
    Public Function getCustomerInfoByCustomerNo(ByVal CustomerNo As String, ByVal Client As String)
        Dim js As New JavaScriptSerializer()
        Dim PigeonsCustomers As New Customer
        Using Conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(Client))

            Conn.Open()
            Dim SqlgetPigeonClients As New SqlCommand("select CustomerNo,Company,Address,City,State,Zip,Phone from tblCompany where customerNo='" & CustomerNo & "'", Conn)
            Using r As SqlDataReader = SqlgetPigeonClients.ExecuteReader()
                While r.Read()

                    PigeonsCustomers.Company = r("Company")
                    PigeonsCustomers.CustNo = r("CustomerNo")
                    PigeonsCustomers.Address = r("Address")
                    PigeonsCustomers.City = r("City")
                    PigeonsCustomers.State = r("State")
                    PigeonsCustomers.Zip = r("Zip")
                    PigeonsCustomers.Phone = r("Phone")
                    PigeonsCustomers.Client = Client
                End While
            End Using
            Conn.Close()
        End Using
        Return js.Serialize(PigeonsCustomers)
    End Function

    <WebMethod()>
    Public Function getEmailsByCustomerNo(ByVal CustomerNo As String, ByVal Client As String)
        Dim js As New JavaScriptSerializer()
        Try
            Dim Conn
            Dim listOfEmails As New List(Of Customer)
            Dim customerClient
            Dim Query = "Select Distinct tierID, Email from [aspnet_Membership] where CustomerNo='" & CustomerNo & "' order by Email asc"
            'And canOrder=1"
            If Client = "CK" Then
                customerClient = getClientConnectionStringByCKCustomerNo(CustomerNo)
                If customerClient <> "CK" Then
                    Conn = New SqlConnection(ConnectionStrings.GetSpecificConnectionString(customerClient))
                    Dim ListofPigeonsCustomers As New List(Of Customer)
                    Conn.Open()
                    Dim SqlgetPigeonClients As New SqlCommand("select CustomerNo,Company,Address,City,State,Zip,Phone from tblCompany order by Company asc", Conn)
                    Using r As SqlDataReader = SqlgetPigeonClients.ExecuteReader()
                        While r.Read()
                            Dim customer As New Customer
                            customer.Company = r("Company")
                            customer.CustNo = r("CustomerNo")
                            customer.Address = r("Address")
                            customer.City = r("City")
                            customer.State = r("State")
                            customer.Zip = r("Zip")
                            customer.Phone = r("Phone")
                            customer.Client = customerClient
                            ListofPigeonsCustomers.Add(customer)
                        End While
                    End Using
                    Conn.Close()
                    Return js.Serialize(ListofPigeonsCustomers)
                End If
                Conn = New SqlConnection(ConnectionStrings.GetSpecificConnectionString(Client))
            Else
                Conn = New SqlConnection(ConnectionStrings.GetSpecificConnectionString(Client))
            End If
            Dim sqlComm As New SqlCommand(Query, Conn)
            Conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim customer As New Customer
                    customer.SalesmanEmail = r("Email").ToString
                    customer.Tier = r("tierID").ToString
                    listOfEmails.Add(customer)
                End While
            End Using
            Return js.Serialize(listOfEmails)
        Catch ex As Exception
            ex.wrangle(wrangler.enums.ProjectTypes.CK, User.Identity.Name, False)
            Return js.Serialize("False")
        End Try

    End Function

    <WebMethod()>
    Public Function SearchOrders(ByVal search As String, ByVal searchwhere As String, ByVal client As String)
        Dim strSql As String
        Dim list As New List(Of SearchResults)
        Dim js As New JavaScriptSerializer
        If searchwhere = "btnVin" Then searchwhere = "OrderID"

        strSql = "Select dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblPartOrder.Servicer, dbo.tblCompany.Company, dbo.tblOrder.AutoYear, dbo.tblOrder.AutoMake, dbo.tblOrder.AutoModel, dbo.tblOrder.Mileage, dbo.tblOrder.VinNo, tblPartOrder.PartDescription, tblPartOrder.SellPrice, tblPartOrder.CostPrice, dbo.tblPartOrder.ActiveProblemStatus, dbo.tblPartOrder.ACReminder, dbo.tblPartOrder.PartID, dbo.tblPartOrder.Cancelled, Left(dbo.vwLastNotesB.Note, 50) As Note, dbo.tblCompany.VIP, dbo.tblPartOrder.Warehouse FROM dbo.tblOrder INNER JOIN dbo.tblCompany On dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.tblPartOrder On dbo.tblOrder.OrderID = dbo.tblPartOrder.OrderID LEFT OUTER JOIN dbo.vwLastNotesB On dbo.tblOrder.OrderID = dbo.vwLastNotesB.OrderID left outer join tblInvoices On tblinvoices.orderid=tblorder.orderid left outer join dbo.tblCompany As tblCompany_1 On dbo.tblPartOrder.Vendor = tblCompany_1.CompanyID WHERE "

        Select Case searchwhere
            Case "Acreminder"
                strSql = strSql & "(dbo.tblpartorder.acreminder Like '%" & search & "%')"
            Case "Adjustername"
                strSql = strSql & "(dbo.tblOrder.adjustername = '" & search & "')"
            Case "Authorizationno"
                strSql = strSql & "(dbo.tblOrder.authorizationno like '%" & search & "%')"
            Case "Company"
                strSql = strSql & "(dbo.tblCompany.company like '%" & search & "%')"
            Case "Contractno"
                strSql = strSql & "(dbo.tblOrder.contractno like '%" & search & "%')"
            Case "Invoiceno"
                strSql = strSql & "(dbo.tblinvoices.invoiceno like '%" & search & "%')"
            Case "Autoowner"
                strSql = strSql & "(dbo.tblOrder.autoowner like '%" & search & "%')"
            Case "PartDescription"
                strSql = strSql & "(dbo.tblpartorder.partdescription like '%" & search & "%')"
            Case "Partno"
                strSql = strSql & "(dbo.tblpartorder.partno like '%" & search & "%')"
            Case "SerialNumber"
                strSql = strSql & "(dbo.tblpartorder.partdescription2 like '%" & search & "%')"
            Case "Servicer"
                strSql = strSql & "(dbo.tblpartorder.servicer like '%" & search & "%')"
            Case "Tracking"
                strSql = strSql & "(dbo.tblpartorder.coreshippertrack like '%" & search & "%')" &
                    " or (dbo.tblpartorder.shippertrack like '%" & search & "%')"
            Case "Vendorinvoiceno"
                strSql = strSql & "(dbo.tblpartorder.vendorinvoiceno like '%" & search & "%')"
            Case "Vendor"
                strSql = strSql & "(tblCompany_1.company like '%" & search & "%')"
            Case "VinNo"
                strSql = strSql & "(dbo.tblOrder.vinno like '%" & search & "%')"
            Case "ActiveProblem"
                strSql = strSql & "(dbo.tblpartorder.activeproblem = '" & search & "')"
            Case "Vehicle"
                strSql = strSql & "(tblorder.autoyear + ' ' + tblorder.automake + ' ' + tblorder.automodel like '%" & search & "%')"
            Case "City"
                strSql = strSql & "(dbo.tblpartorder.city = '" & search & "')"
            Case "State"
                strSql = strSql & "(dbo.tblpartorder.state = '" & search & "')"
            Case "Zip"
                strSql = strSql & "(dbo.tblpartorder.zip = '" & search & "')"
            Case Else
                Dim number As Integer
                Dim result As Boolean = Int32.TryParse(search, number)
                If result Then
                    'all good
                Else
                    GoTo nointeger
                End If
                If (searchwhere = "OrderID" And IsNumeric(search) = True) Then
                    strSql = strSql & "(dbo.tblOrder.OrderID ='" & search & "')"
                ElseIf searchwhere = "Everywhere" And IsNumeric(search) = True Then
                    strSql = strSql & "(dbo.tblOrder.OrderID ='" & search & "')"
                    strSql = strSql & " or (dbo.tblOrder.vinno like '%" & search & "%')" &
                    " or (dbo.tblpartorder.partdescription like '%" & search & "%' )" &
                    " or (dbo.tblpartorder.servicer like '%" & search & "%')" &
                    " or (tblorder.autoyear + ' ' + tblorder.automake + ' ' + tblorder.automodel like '%" & search & "%')" &
                    " or (dbo.tblpartorder.shippertrack = '" & search & "') " &
                    " or (dbo.tblpartorder.coreshippertrack = '" & search & "')" &
                    " or (dbo.tblpartorder.partdescription = '" & search & "')" &
                    " or (dbo.tblpartorder.partdescription2 like '%" & search & "%')" &
                    " or (dbo.tblpartorder.activeproblem = '" & search & "')" &
                    " or (dbo.tblpartorder.acreminder = '" & search & "')" &
                    " or (dbo.tblinvoices.invoiceno like '%" & search & "%')" &
                    " or (dbo.tblpartorder.vendorinvoiceno like '%" & search & "%')" &
                    " or (dbo.tblpartorder.partno like '%" & search & "%')" &
                    " or (dbo.tblOrder.contractno like '%" & search & "%')" &
                    " or (dbo.tblOrder.authorizationno like '%" & search & "%')" &
                    " or (dbo.tblOrder.autoowner like '%" & search & "%')" &
                    " or (dbo.tblOrder.adjustername = '" & search & "')"
                ElseIf searchwhere = "Everywhere" Or (searchwhere = "OrderID" And IsNumeric(search) = False) Then
nointeger:
                    strSql = strSql & "(dbo.tblOrder.vinno like '%" & search & "%')" &
                    " or (dbo.tblpartorder.partdescription like '%" & search & "%' )" &
                    " or (dbo.tblpartorder.servicer like '%" & search & "%')" &
                    " or (tblorder.autoyear + ' ' + tblorder.automake + ' ' + tblorder.automodel like '%" & search & "%')" &
                    " or (dbo.tblpartorder.shippertrack = '" & search & "') " &
                    " or (dbo.tblpartorder.coreshippertrack = '" & search & "')" &
                    " or (dbo.tblpartorder.partdescription = '" & search & "')" &
                    " or (dbo.tblpartorder.partdescription2 like '%" & search & "%')" &
                    " or (dbo.tblpartorder.activeproblem = '" & search & "')" &
                    " or (dbo.tblpartorder.acreminder = '" & search & "')" &
                    " or (dbo.tblinvoices.invoiceno like '%" & search & "%')" &
                    " or (dbo.tblpartorder.vendorinvoiceno like '%" & search & "%')" &
                    " or (dbo.tblpartorder.partno like '%" & search & "%')" &
                    " or (dbo.tblOrder.contractno like '%" & search & "%')" &
                    " or (dbo.tblOrder.authorizationno like '%" & search & "%')" &
                    " or (dbo.tblOrder.autoowner like '%" & search & "%')" &
                    " or (dbo.tblOrder.adjustername = '" & search & "')"
                End If
        End Select

        strSql = strSql & " GROUP BY left(dbo.vwLastNotesB.Note,50), dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblPartOrder.Servicer, dbo.tblCompany.Company, dbo.tblOrder.AutoMake, dbo.tblOrder.Mileage, dbo.tblOrder.VinNo, dbo.tblOrder.AutoYear, dbo.tblOrder.AutoModel, dbo.tblPartOrder.ActiveProblemStatus, dbo.tblPartOrder.ACReminder, tblPartOrder.PartDescription, tblPartOrder.SellPrice, tblPartOrder.CostPrice, dbo.tblPartOrder.PartID, left(PartDescription, 50), dbo.tblPartOrder.Cancelled, dbo.tblCompany.VIP, dbo.tblPartOrder.Warehouse order by dbo.tblorder.orderid desc OPTION (RECOMPILE)"

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim s1 As New SearchResults
                    s1.ActiveProblemStatus = r("ActiveProblemStatus").ToString
                    s1.AutoMake = r("AutoMake").ToString
                    s1.AutoModel = r("AutoModel").ToString
                    s1.AutoYear = r("AutoYear").ToString
                    s1.Cancelled = (CBool(r("Cancelled")))
                    s1.Company = r("Company").ToString
                    s1.DateOrdered = r("DateOrdered").ToString
                    s1.Mileage = r("Mileage").ToString
                    s1.Note = r("Note").ToString
                    s1.OrderID = r("OrderID").ToString
                    s1.PartDescription = r("PartDescription").ToString
                    s1.Servicer = r("Servicer").ToString
                    s1.VinNo = r("VinNo").ToString
                    s1.Warehouse = r("Warehouse").ToString
                    s1.SellPrice = r("SellPrice").ToString
                    s1.CostPrice = r("costprice").ToString

                    list.Add(s1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetArrivals(ByVal client As String)
        Dim strSql As String
        Dim list As New List(Of Arrivals)
        Dim js As New JavaScriptSerializer


        strSql = "SELECT TOP 100 PERCENT dbo.tblPartOrder.VendorInvoiceNo, dbo.tblOrder.OrderID,dbo.tblPartOrder.PartType, dbo.tblPartOrder.PartNo, 'Original' AS PartStatus, CASE WHEN core = 1 THEN 'Core' ELSE 'Nothing' END AS ComingBack, convert(varchar,dbo.tblPartOrder.ExpShipDate,101) as ExpShipDate, convert(varchar,dbo.tblPartOrder.ArrivalDate,101) as ArrivalDate, convert(varchar,dbo.tblPartOrder.FreightETA,101) as FreightETA, dbo.tblCompany.Company, case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, dbo.tblPartOrder.Servicer, dbo.tblPartOrder.State, tblCompany_1.Company AS Vendor, tblCompany_1.CompanyID AS VendorID, tblCompany_3.Company AS Shipper, dbo.tblPartOrder.ShipperTrack AS Track, dbo.tblPartOrder.ShipperStatus, dbo.tblPartOrder.Reminder, dbo.tblPartOrder.PartID, tblCompany_3.CompanyID AS ShipperID, dbo.tblOrder.CustomerNo, dbo.tblCompany.VIP FROM dbo.tblOrder INNER JOIN dbo.tblPartOrder ON dbo.tblOrder.OrderID = dbo.tblPartOrder.OrderID INNER JOIN dbo.tblCompany AS tblCompany_2 ON dbo.tblOrder.CustomerNo = tblCompany_2.CustomerNo INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo LEFT OUTER JOIN dbo.tblCompany AS tblCompany_1 ON dbo.tblPartOrder.Vendor = tblCompany_1.CompanyID LEFT OUTER JOIN dbo.tblCompany AS tblCompany_3 ON dbo.tblPartOrder.Shipper = tblCompany_3.CompanyID left outer join tblPigeonClients on dbo.tblOrder.CustomerNo = tblPigeonClients.CKCustomerNo WHERE dbo.tblOrder.CustomerNo <> '9997C' and  (dbo.tblPartOrder.Cancelled <> 1) AND (dbo.tblPartOrder.DateOrdered IS NOT NULL) AND (dbo.tblPartOrder.ArriveDate IS NULL) AND (dbo.tblPartOrder.Wholesale = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) GROUP BY tblCompany_1.CompanyID,dbo.tblPartOrder.PartNo, dbo.tblOrder.OrderID, dbo.tblPartOrder.ExpShipDate, dbo.tblPartOrder.ArrivalDate, dbo.tblPartOrder.Servicer, dbo.tblPartOrder.State, tblCompany_1.Company, dbo.tblPartOrder.ShipperTrack, dbo.tblPartOrder.Reminder, dbo.tblPartOrder.PartID, tblCompany_3.CompanyID, tblCompany_3.Company, dbo.tblPartOrder.FreightETA, dbo.tblPartOrder.ShipperStatus, CASE WHEN core = 1 THEN 'Core' ELSE 'Nothing' END, dbo.tblOrder.CustomerNo, dbo.tblCompany.Company, dbo.tblCompany.VIP,dbo.tblPartOrder.PartType, tblPigeonClients.Client, dbo.tblPartOrder.VendorInvoiceNo UNION " &
"SELECT TOP 100 PERCENT tblPartOrder_2.VendorInvoiceNo, tblOrder_1.OrderID, tblPartOrder_2.PartType, tblPartOrder_2.PartNo, 'Replacement' AS PartStatus, CASE WHEN tblpartorder_1.defect = 1 THEN 'Defect' ELSE 'Incorrect' END AS ComingBack, convert(varchar,tblPartOrder_2.ExpShipDate,101) as ExpShipDate, convert(varchar,tblPartOrder_2.ArrivalDate,101) as ArrivalDate, convert(varchar,tblPartOrder_2.FreightETA,101) as FreightETA, tblCompany_4.Company, case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, tblPartOrder_2.Servicer, tblPartOrder_2.State, tblCompany_1.Company AS Vendor, tblCompany_1.CompanyID AS VendorID, tblCompany_3.Company AS Shipper, tblPartOrder_2.ShipperTrack AS Track, tblPartOrder_2.ShipperStatus, tblPartOrder_2.Reminder, tblPartOrder_2.PartID, tblCompany_3.CompanyID AS ShipperID, tblOrder_1.CustomerNo, tblCompany_4.VIP FROM dbo.tblOrder AS tblOrder_1 INNER JOIN dbo.tblPartOrder AS tblPartOrder_2 ON tblOrder_1.OrderID = tblPartOrder_2.OrderID INNER JOIN dbo.tblCompany AS tblCompany_2 ON tblOrder_1.CustomerNo = tblCompany_2.CustomerNo INNER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON tblPartOrder_2.PreviousPartID = tblPartOrder_1.PartID INNER JOIN dbo.tblCompany AS tblCompany_4 ON tblOrder_1.CustomerNo = tblCompany_4.CustomerNo LEFT OUTER JOIN dbo.tblCompany AS tblCompany_1 ON tblPartOrder_2.Vendor = tblCompany_1.CompanyID LEFT OUTER JOIN dbo.tblCompany AS tblCompany_3 ON tblPartOrder_2.Shipper = tblCompany_3.CompanyID left outer join tblPigeonClients on tblOrder_1.CustomerNo = tblPigeonClients.CKCustomerNo WHERE tblOrder_1.CustomerNo <> '9997C' and  (tblPartOrder_2.Cancelled <> 1) AND (tblPartOrder_2.DateOrdered IS NOT NULL) AND (tblPartOrder_2.ArriveDate IS NULL) AND (tblPartOrder_2.Wholesale = 0) AND (tblPartOrder_2.PreviousPartID <> 0) GROUP BY tblCompany_1.CompanyID,tblPartOrder_2.PartNO, tblOrder_1.OrderID, tblPartOrder_2.ExpShipDate, tblPartOrder_2.ArrivalDate, tblPartOrder_2.Servicer, tblPartOrder_2.State, tblCompany_1.Company, tblPartOrder_2.ShipperTrack, tblPartOrder_2.Reminder, tblPartOrder_2.PartID, tblCompany_3.CompanyID, tblCompany_3.Company, tblPartOrder_2.FreightETA, tblPartOrder_2.ShipperStatus, CASE WHEN tblpartorder_1.defect = 1 THEN 'Defect' ELSE 'Incorrect' END, tblOrder_1.CustomerNo, tblCompany_4.Company, tblCompany_4.VIP,tblPartOrder_2.PartType, tblPigeonClients.Client, tblPartOrder_2.VendorInvoiceNo ORDER BY ArrivalDate, dbo.tblOrder.OrderID "
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim a1 As New Arrivals
                    a1.ArrivalDate = r("ArrivalDate").ToString
                    a1.ComingBack = r("ComingBack").ToString
                    a1.Company = r("Company").ToString
                    a1.CustomerType = r("CustomerType").ToString
                    a1.ExpShipDate = r("ExpShipDate").ToString
                    a1.FreightETA = r("FreightETA").ToString
                    a1.OrderID = r("OrderID").ToString
                    a1.PartID = r("PartID").ToString
                    a1.PartStatus = r("PartStatus").ToString
                    a1.PartType = r("PartType").ToString
                    a1.Reminder = r("Reminder").ToString
                    a1.Servicer = r("Servicer").ToString
                    a1.Shipper = r("Shipper").ToString
                    a1.ShipperStatus = r("ShipperStatus").ToString
                    a1.State = r("State").ToString
                    a1.Track = r("Track").ToString
                    a1.VendorInvoiceNo = r("VendorInvoiceNo").ToString
                    Select Case r("ShipperID").ToString
                        Case "65"
                            a1.TrackUrl = "http://wwwapps.ups.com/WebTracking/processInputRequest?HTMLVersion=5.0&loc=en_US&Requester=UPSHome&tracknum=" + a1.Track + "&AgreeToTermsAndConditions=yes&track.x=7&track.y=5"

                        Case "59"
                            a1.TrackUrl = "http://admin.ckautoparts.com/Admin/tracking.aspx?partid=" + r("PartID").ToString

                        Case "39"
                            a1.TrackUrl = "http://www.dbschenkerusa.com/dynamic_frameset.html?data.node.id=21097&data.language.id=28&request.page_template.frame=dynamic_frameset&request.frame.url=/apps/Tracking/TrackResults.aspx&trackby=H&trackbyno=" + a1.Track

                        Case "65"
                            a1.TrackUrl = "http://wwwapps.ups.com/WebTracking/processInputRequest?HTMLVersion=5.0&loc=en_US&Requester=UPSHome&tracknum=" + a1.Track + "&AgreeToTermsAndConditions=yes&track.x=7&track.y=5"

                        Case "907"
                            a1.TrackUrl = "http://www.cevalogistics.com/Solutions/CustomerTools/CEVATrak.aspx?sv=" + a1.Track + "~"


                        Case "56"
                            a1.TrackUrl = "http://www.mmeinc.com/logon/quicktrax.asp?pro=" + a1.Track


                        Case "49"
                            a1.TrackUrl = "https://www.fedex.com/fedextrack/index.html?tracknumbers=" & a1.Track & "&cntry_code=us"


                        Case "2505"
                            a1.TrackUrl = "https://www.fedex.com/fedextrack/index.html?tracknumbers=" & a1.Track & "&cntry_code=us"

                        Case "38"
                            a1.TrackUrl = "https://www.fedex.com/fedextrack/index.html?tracknumbers=" & a1.Track & "&cntry_code=us"


                        Case "5549"
                            a1.TrackUrl = "https://www.fedex.com/fedextrack/index.html?tracknumbers=" & a1.Track & "&cntry_code=us"


                    End Select
                    a1.Vendor = r("Vendor").ToString
                    If r("VendorID") = 78 Then 'get install packet
                        a1.InstallPacket = GetInstallPacket(r("PartNo").ToString(), True)
                    End If
                    list.Add(a1)
                End While
            End Using
        End Using
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetIncompleteReturns(ByVal client As String)

        Dim list As New List(Of IncompleteReturns)
        Dim js As New JavaScriptSerializer
        Dim sb As New System.Text.StringBuilder()

        sb.Append("SELECT dbo.tblOrder.OrderID, 
dbo.tblPartOrder.DateOrdered, 
dbo.tblPartOrder.PartType,
 tblCompany_1.Company AS Vendor, 
 dbo.tblPartOrder.PartDescription, 
 dbo.tblCompany.Company, 
 case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, 
 'No Replacement' AS ComingBack, 
 dbo.tblPartOrder.CostPrice AS Value, 
 dbo.tblPartOrder.Servicer, 
 dbo.tblPartOrder.State,
  dbo.tblPartOrder.FUDate, 
  dbo.tblPartOrder.FUStatus, 
  dbo.tblPartOrder.Reminder, 
  dbo.tblPartOrder.PartID, 
  dbo.tblOrder.CustomerNo, 
  dbo.tblCompany.VIP 
  FROM dbo.tblOrder 
  INNER JOIN dbo.tblPartOrder ON dbo.tblOrder.OrderID = dbo.tblPartOrder.OrderID 
  INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo 
  INNER JOIN dbo.tblCompany AS tblCompany_1 ON dbo.tblPartOrder.Vendor = tblCompany_1.CompanyID 
  LEFT OUTER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON dbo.tblPartOrder.PartID = tblPartOrder_1.PreviousPartID 
  left outer join tblPigeonClients on dbo.tblOrder.CustomerNo = tblPigeonClients.CKCustomerNo 
  WHERE (dbo.tblPartOrder.Cancelled <> 1)
   AND (dbo.tblPartOrder.DateOrdered IS NOT NULL) 
   AND (dbo.tblPartOrder.ArriveDate IS NOT NULL) 
   AND (dbo.tblPartOrder.ReturnLost = 0) 
   AND (dbo.tblPartOrder.Incorrect = 1)
    AND (dbo.tblPartOrder.IncorrectReturned = 0) 
	AND (dbo.tblPartOrder.ArriveDate IS NOT NULL)
	 GROUP BY dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblCompany.Company, dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.State, dbo.tblPartOrder.FUDate, dbo.tblPartOrder.FUStatus, dbo.tblPartOrder.PartID, dbo.tblPartOrder.Reminder, dbo.tblOrder.CustomerNo, dbo.tblPartOrder.CostPrice, dbo.tblPartOrder.Servicer, tblCompany_1.CompanyID, tblCompany_1.Company, dbo.tblCompany.VIP, dbo.tblPartOrder.PartType, tblPigeonClients.Client HAVING (COUNT(tblPartOrder_1.PreviousPartID) = 0) 
	 ")

        sb.Append(" UNION ")

        sb.Append("
        Select tblOrder_1.OrderID,
	  tblPartOrder_2.DateOrdered, 
	  tblPartOrder_2.PartType, 
	  tblCompany_3.Company AS Vendor, 
	  tblPartOrder_2.PartDescription,
	   tblCompany_1.Company, 
	   case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, 
	    'No Replacement'  AS ComingBack, 
	   tblPartOrder_2.CostPrice AS Value,
	    tblPartOrder_2.Servicer, 
		tblPartOrder_2.State,
		 tblPartOrder_2.FUDate, 
		 tblPartOrder_2.FUStatus, 
		 tblPartOrder_2.Reminder, 
		 tblPartOrder_2.PartID, 
		 tblOrder_1.CustomerNo, 
		 tblCompany_1.VIP
		  FROM dbo.tblOrder AS tblOrder_1 
		  INNER JOIN dbo.tblPartOrder AS tblPartOrder_2 ON tblOrder_1.OrderID = tblPartOrder_2.OrderID 
		  INNER JOIN dbo.tblCompany AS tblCompany_1 ON tblOrder_1.CustomerNo = tblCompany_1.CustomerNo 
		  INNER JOIN dbo.tblCompany AS tblCompany_3 ON tblPartOrder_2.Vendor = tblCompany_3.CompanyID 
		  LEFT OUTER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON tblPartOrder_2.PartID = tblPartOrder_1.PreviousPartID 
		  left outer join tblPigeonClients on tblOrder_1.CustomerNo = tblPigeonClients.CKCustomerNo 
		  WHERE (tblPartOrder_2.Cancelled <> 1) 
		  AND (tblPartOrder_2.DateOrdered IS NOT NULL) 
		  AND (tblPartOrder_2.ArriveDate IS NOT NULL) 
		  AND (tblPartOrder_2.ReturnLost = 0) 
		  AND (tblPartOrder_2.ArriveDate IS NOT NULL) 
		  AND (tblPartOrder_2.Defect = 1) 
		  AND (tblPartOrder_2.DefectReturned = 0) 
		  AND (tblPartOrder_2.DefectRepaired = 0) 
		  GROUP BY tblOrder_1.OrderID, tblPartOrder_2.DateOrdered, tblCompany_1.Company, tblPartOrder_2.PartDescription, tblPartOrder_2.State, tblPartOrder_2.FUDate, tblPartOrder_2.FUStatus, tblPartOrder_2.PartID, tblPartOrder_2.Reminder, tblOrder_1.CustomerNo, tblPartOrder_2.CostPrice, tblPartOrder_2.Servicer, tblCompany_3.Company, tblCompany_1.VIP,tblPartOrder_2.PartType, tblPigeonClients.Client HAVING (COUNT(tblPartOrder_1.PreviousPartID) = 0)
")
        sb.Append(" Union ")

        sb.Append("  Select tblOrder_1.OrderID, 
tblPartOrder_2.DateOrdered, 
tblPartOrder_2.PartType, 
tblCompany_5.Company AS Vendor, 
tblPartOrder_2.PartDescription, 
tblCompany_1.Company, 
case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, 
'Incorrect' AS ComingBack, 
tblPartOrder_2.CostPrice AS Value, 
CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.Servicer ELSE tblpartorder_2.servicer END AS Servicer,
CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.State ELSE tblpartorder_2.state END AS State, 
CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.fudate ELSE tblpartorder_2.fudate END AS FUDate, 
CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.fustatus ELSE tblpartorder_2.fustatus END AS FUStatus, 
CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.reminder ELSE tblpartorder_2.reminder END AS Reminder, 
CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.partid ELSE tblpartorder_2.partid END AS PartID, 
tblOrder_1.CustomerNo, 
tblCompany_1.VIP 
FROM dbo.tblOrder AS tblOrder_1 
INNER JOIN dbo.tblPartOrder AS tblPartOrder_2 ON tblOrder_1.OrderID = tblPartOrder_2.OrderID 
INNER JOIN dbo.tblCompany AS tblCompany_1 ON tblOrder_1.CustomerNo = tblCompany_1.CustomerNo 
INNER JOIN dbo.tblCompany AS tblCompany_5 ON tblPartOrder_2.Vendor = tblCompany_5.CompanyID 
LEFT OUTER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON tblPartOrder_2.PartID = tblPartOrder_1.PreviousPartID 
left outer join tblPigeonClients on tblOrder_1.CustomerNo = tblPigeonClients.CKCustomerNo 
WHERE tblOrder_1.CustomerNo <> '9997C' and (tblPartOrder_2.Cancelled <> 1) AND (tblPartOrder_2.DateOrdered IS NOT NULL) AND (tblPartOrder_2.ArriveDate IS NOT NULL) AND (tblPartOrder_2.ReturnLost = 0) AND (tblPartOrder_1.ArriveDate IS NOT NULL) AND (tblPartOrder_2.Incorrect = 1) AND (tblPartOrder_2.IncorrectReturned = 0) OR (tblPartOrder_2.Cancelled <> 1) AND (tblPartOrder_2.DateOrdered IS NOT NULL) AND (tblPartOrder_2.ArriveDate IS NOT NULL) AND (tblPartOrder_2.ReturnLost = 0) AND (tblPartOrder_1.ArriveDate IS NOT NULL) AND (tblPartOrder_2.Incorrect = 1) AND (tblPartOrder_2.IncorrectReturned = 0) GROUP BY tblOrder_1.OrderID, tblPartOrder_2.DateOrdered, tblCompany_1.Company, tblPartOrder_2.PartDescription, CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.State ELSE tblpartorder_2.state END, CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.fudate ELSE tblpartorder_2.fudate END, CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.fustatus ELSE tblpartorder_2.fustatus END, CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.partid ELSE tblpartorder_2.partid END, CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.reminder ELSE tblpartorder_2.reminder END, tblOrder_1.CustomerNo, tblPartOrder_2.CostPrice, CASE WHEN tblpartorder_1.previouspartid IS NOT NULL THEN tblpartorder_1.Servicer ELSE tblpartorder_2.servicer END, tblCompany_5.Company, tblCompany_1.VIP,tblPartOrder_2.PartType, tblPigeonClients.Client HAVING (COUNT(tblPartOrder_1.PreviousPartID) > 0) OR (COUNT(tblPartOrder_1.PreviousPartID) = 0)
    ")

        sb.Append(" Union ")

        sb.Append(" Select dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered,dbo.tblPartOrder.PartType,tblCompany_3.Company AS Vendor, dbo.tblPartOrder.PartDescription, dbo.tblCompany.Company,
  case when tblPigeonClients.Client Is null then 'Warranty' else 
  'Private Label' end as CustomerType,
    'Core' AS ComingBack, 
   CASE WHEN dbo.tblpartorder.coreprice Is NULL THEN 500 
   Else dbo.tblpartorder.coreprice End As Value, dbo.tblPartOrder.Servicer, 
   dbo.tblPartOrder.State, dbo.tblPartOrder.FUDate, dbo.tblPartOrder.FUStatus,
   dbo.tblPartOrder.Reminder, dbo.tblPartOrder.PartID, dbo.tblOrder.CustomerNo,
   dbo.tblCompany.VIP FROM dbo.tblOrder INNER JOIN dbo.tblPartOrder ON 
   dbo.tblOrder.OrderID = dbo.tblPartOrder.OrderID INNER JOIN dbo.tblCompany On 
   dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.tblCompany 
   AS tblCompany_3 ON dbo.tblPartOrder.Vendor = tblCompany_3.CompanyID left outer join 
   tblPigeonClients on dbo.tblOrder.CustomerNo = tblPigeonClients.CKCustomerNo 
   WHERE dbo.tblOrder.CustomerNo <> '9997C' and (dbo.tblPartOrder.Cancelled <> 1) 
   And (dbo.tblPartOrder.DateOrdered Is Not NULL) And (dbo.tblPartOrder.ArriveDate Is Not NULL) 
   And (dbo.tblPartOrder.Core = 1) And (dbo.tblPartOrder.CoreReceived = 0) 
   And (dbo.tblPartOrder.ReturnLost = 0) GROUP BY dbo.tblOrder.OrderID, 
   dbo.tblPartOrder.DateOrdered, tblCompany_3.Company, dbo.tblCompany.Company,
   dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.Servicer, dbo.tblPartOrder.State,
   dbo.tblPartOrder.FUDate, dbo.tblPartOrder.FUStatus, dbo.tblPartOrder.PartID,
   dbo.tblPartOrder.Reminder, dbo.tblOrder.CustomerNo, CASE WHEN dbo.tblpartorder.coreprice Is NULL 
   THEN 500 ELSE dbo.tblpartorder.coreprice END, dbo.tblCompany.Company, dbo.tblCompany.VIP,
   dbo.tblPartOrder.PartType, tblPigeonClients.Client 
")

        sb.Append("        UNION ")

        sb.Append(" Select TOP (100) PERCENT tblOrder_2.OrderID, 
   tblPartOrder_3.DateOrdered, tblPartOrder_3.PartType, tblCompany_4.Company AS Vendor,tblPartOrder_3.PartDescription, tblCompany_2.Company, case when tblPigeonClients.Client 
   Is null then 'Warranty' else 'Private Label' end as CustomerType,  
   'Defect' AS ComingBack, tblPartOrder_3.CostPrice AS Value, 
   CASE WHEN tblpartorder_1.previouspartid Is Not NULL THEN tblpartorder_1.Servicer ELSE tblpartorder_3.servicer 
   End As Expr1, Case When tblpartorder_1.previouspartid Is Not NULL Then tblpartorder_1.State Else 
   tblpartorder_3.state END AS Expr2, CASE WHEN tblpartorder_1.previouspartid Is Not NULL THEN 
   tblpartorder_1.fudate Else tblpartorder_3.fudate End As Expr3, Case When tblpartorder_1.previouspartid Is 
   Not NULL THEN tblpartorder_1.fustatus ELSE tblpartorder_3.fustatus END AS Expr4, CASE WHEN 
   tblpartorder_1.previouspartid Is Not NULL THEN tblpartorder_1.reminder ELSE tblpartorder_3.reminder 
   End As Expr5, Case When tblpartorder_1.previouspartid Is Not NULL Then tblpartorder_1.partid Else 
   tblpartorder_3.partid END AS Expr6, tblOrder_2.CustomerNo, tblCompany_2.VIP FROM dbo.tblOrder AS tblOrder_2 
   INNER Join dbo.tblPartOrder AS tblPartOrder_3 ON tblOrder_2.OrderID = tblPartOrder_3.OrderID INNER JOIN
    dbo.tblCompany AS tblCompany_2 ON tblOrder_2.CustomerNo = tblCompany_2.CustomerNo INNER JOIN dbo.tblCompany 
	AS tblCompany_4 ON tblPartOrder_3.Vendor = tblCompany_4.CompanyID LEFT OUTER JOIN dbo.tblPartOrder AS 
	tblPartOrder_1 ON tblPartOrder_3.PartID = tblPartOrder_1.PreviousPartID left outer join tblPigeonClients on 
	tblOrder_2.CustomerNo = tblPigeonClients.CKCustomerNo WHERE tblOrder_2.CustomerNo <> '9997C' and 
	(tblPartOrder_3.Cancelled <> 1) And (tblPartOrder_3.DateOrdered Is Not NULL) And (tblPartOrder_3.ArriveDate Is
	 Not NULL) And (tblPartOrder_3.ReturnLost = 0) And (tblPartOrder_1.ArriveDate Is Not NULL) And 
	 (tblPartOrder_3.Defect = 1) And (tblPartOrder_3.DefectReturned = 0) And (tblPartOrder_3.DefectRepaired = 0) 
	 Or (tblPartOrder_3.Cancelled <> 1) And (tblPartOrder_3.DateOrdered Is Not NULL) And 
	 (tblPartOrder_3.ArriveDate Is Not NULL) And (tblPartOrder_3.ReturnLost = 0) And (tblPartOrder_1.ArriveDate 
	 Is Not NULL) And (tblPartOrder_3.Defect = 1) And (tblPartOrder_3.DefectReturned = 0) 
	 And (tblPartOrder_3.DefectRepaired = 0) GROUP BY tblOrder_2.OrderID, tblPartOrder_3.DateOrdered, 
	 tblCompany_2.Company, tblPartOrder_3.PartDescription, CASE WHEN tblpartorder_1.previouspartid Is Not NULL 
	 THEN tblpartorder_1.Servicer ELSE tblpartorder_3.servicer END, CASE WHEN tblpartorder_1.previouspartid Is 
	 Not NULL THEN tblpartorder_1.State ELSE tblpartorder_3.state END, CASE WHEN tblpartorder_1.previouspartid Is 
	 Not NULL THEN tblpartorder_1.fudate ELSE tblpartorder_3.fudate END, CASE WHEN tblpartorder_1.previouspartid 
	 Is Not NULL THEN tblpartorder_1.fustatus ELSE tblpartorder_3.fustatus END, CASE
	  WHEN tblpartorder_1.previouspartid Is Not NULL THEN tblpartorder_1.partid ELSE tblpartorder_3.partid 
	  End, Case When tblpartorder_1.previouspartid Is Not NULL Then tblpartorder_1.reminder Else 
	  tblpartorder_3.reminder END, tblOrder_2.CustomerNo, tblPartOrder_3.CostPrice, tblCompany_4.Company,
      tblCompany_2.VIP, tblPartOrder_3.PartType, tblPigeonClients.Client HAVING (COUNT(tblPartOrder_1.PreviousPartID)
	   > 0) Or (COUNT(tblPartOrder_1.PreviousPartID) = 0) 
				 order by vendor")

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(sb.ToString(), conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim i1 As New IncompleteReturns

                    i1.PartID = r("PartID").ToString
                    i1.ComingBack = r("ComingBack").ToString
                    i1.Company = r("Company").ToString
                    i1.CustomerType = r("CustomerType").ToString
                    i1.DateOrdered = r("DateOrdered").ToString
                    If r("FUDate").ToString = "" Then
                        i1.FollowUpDate = ""
                    Else
                        i1.FollowUpDate = FormatDateTime(r("FUDate").ToString, DateFormat.ShortDate)
                    End If

                    i1.FollowUpStatus = r("FUStatus").ToString
                    i1.OrderID = r("OrderID").ToString
                    i1.Part = r("PartDescription").ToString
                    i1.PartType = r("PartType").ToString
                    i1.PartValue = r("Value").ToString
                    i1.Reminder = IIf(r("Reminder").ToString = "", "-", r("Reminder").ToString)
                    i1.Servicer = r("Servicer").ToString
                    i1.State = r("State").ToString
                    i1.Vendor = r("Vendor").ToString
                    list.Add(i1)
                End While
            End Using
        End Using
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetIncompleteReturnsNoReplacement(ByVal client As String)
        Dim strSql As String
        Dim list As New List(Of IncompleteReturns)
        Dim js As New JavaScriptSerializer


        strSql = "SELECT dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblPartOrder.PartType, tblCompany_1.Company AS Vendor, dbo.tblPartOrder.PartDescription, dbo.tblCompany.Company, case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, 'Incorrect' AS ComingBack, dbo.tblPartOrder.CostPrice AS Value, dbo.tblPartOrder.Servicer, dbo.tblPartOrder.State, dbo.tblPartOrder.FUDate, dbo.tblPartOrder.FUStatus, dbo.tblPartOrder.Reminder, dbo.tblPartOrder.PartID, dbo.tblOrder.CustomerNo, dbo.tblCompany.VIP FROM dbo.tblOrder INNER JOIN dbo.tblPartOrder ON dbo.tblOrder.OrderID = dbo.tblPartOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.tblCompany AS tblCompany_1 ON dbo.tblPartOrder.Vendor = tblCompany_1.CompanyID LEFT OUTER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON dbo.tblPartOrder.PartID = tblPartOrder_1.PreviousPartID left outer join tblPigeonClients on dbo.tblOrder.CustomerNo = tblPigeonClients.CKCustomerNo WHERE (dbo.tblPartOrder.Cancelled <> 1) AND (dbo.tblPartOrder.DateOrdered IS NOT NULL) AND (dbo.tblPartOrder.ArriveDate IS NOT NULL) AND (dbo.tblPartOrder.ReturnLost = 0) AND (dbo.tblPartOrder.Incorrect = 1) AND (dbo.tblPartOrder.IncorrectReturned = 0) AND (dbo.tblPartOrder.ArriveDate IS NOT NULL) GROUP BY dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblCompany.Company, dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.State, dbo.tblPartOrder.FUDate, dbo.tblPartOrder.FUStatus, dbo.tblPartOrder.PartID, dbo.tblPartOrder.Reminder, dbo.tblOrder.CustomerNo, dbo.tblPartOrder.CostPrice, dbo.tblPartOrder.Servicer, tblCompany_1.CompanyID, tblCompany_1.Company, dbo.tblCompany.VIP, dbo.tblPartOrder.PartType, tblPigeonClients.Client HAVING (COUNT(tblPartOrder_1.PreviousPartID) = 0) UNION SELECT tblOrder_1.OrderID, tblPartOrder_2.DateOrdered, tblPartOrder_2.PartType, tblCompany_3.Company AS Vendor, tblPartOrder_2.PartDescription, tblCompany_1.Company, case when tblPigeonClients.Client is null then 'Warranty' else 'Private Label' end as CustomerType, 'Defect' AS ComingBack, tblPartOrder_2.CostPrice AS Value, tblPartOrder_2.Servicer, tblPartOrder_2.State, tblPartOrder_2.FUDate, tblPartOrder_2.FUStatus, tblPartOrder_2.Reminder, tblPartOrder_2.PartID, tblOrder_1.CustomerNo, tblCompany_1.VIP FROM dbo.tblOrder AS tblOrder_1 INNER JOIN dbo.tblPartOrder AS tblPartOrder_2 ON tblOrder_1.OrderID = tblPartOrder_2.OrderID INNER JOIN dbo.tblCompany AS tblCompany_1 ON tblOrder_1.CustomerNo = tblCompany_1.CustomerNo INNER JOIN dbo.tblCompany AS tblCompany_3 ON tblPartOrder_2.Vendor = tblCompany_3.CompanyID LEFT OUTER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON tblPartOrder_2.PartID = tblPartOrder_1.PreviousPartID left outer join tblPigeonClients on tblOrder_1.CustomerNo = tblPigeonClients.CKCustomerNo WHERE (tblPartOrder_2.Cancelled <> 1) AND (tblPartOrder_2.DateOrdered IS NOT NULL) AND (tblPartOrder_2.ArriveDate IS NOT NULL) AND (tblPartOrder_2.ReturnLost = 0) AND (tblPartOrder_2.ArriveDate IS NOT NULL) AND (tblPartOrder_2.Defect = 1) AND (tblPartOrder_2.DefectReturned = 0) AND (tblPartOrder_2.DefectRepaired = 0) GROUP BY tblOrder_1.OrderID, tblPartOrder_2.DateOrdered, tblCompany_1.Company, tblPartOrder_2.PartDescription, tblPartOrder_2.State, tblPartOrder_2.FUDate, tblPartOrder_2.FUStatus, tblPartOrder_2.PartID, tblPartOrder_2.Reminder, tblOrder_1.CustomerNo, tblPartOrder_2.CostPrice, tblPartOrder_2.Servicer, tblCompany_3.Company, tblCompany_1.VIP,tblPartOrder_2.PartType, tblPigeonClients.Client HAVING (COUNT(tblPartOrder_1.PreviousPartID) = 0)"
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim i1 As New IncompleteReturns

                    i1.PartID = r("PartID").ToString
                    i1.ComingBack = r("ComingBack").ToString
                    i1.Company = r("Company").ToString
                    i1.CustomerType = r("CustomerType").ToString
                    i1.DateOrdered = r("DateOrdered").ToString
                    If r("FUDate").ToString = "" Then
                        i1.FollowUpDate = ""
                    Else
                        i1.FollowUpDate = FormatDateTime(r("FUDate").ToString, DateFormat.ShortDate)
                    End If
                    i1.FollowUpStatus = r("FUStatus").ToString
                    i1.OrderID = r("OrderID").ToString
                    i1.Part = r("PartDescription").ToString
                    i1.PartType = r("PartType").ToString
                    i1.PartValue = r("Value").ToString
                    i1.Reminder = IIf(r("Reminder").ToString = "", "-", r("Reminder").ToString)
                    i1.Servicer = r("Servicer").ToString
                    i1.State = r("State").ToString
                    i1.Vendor = r("Vendor").ToString
                    list.Add(i1)
                End While
            End Using
        End Using
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function SaveArrivals(ByVal user As String, ByVal orderid As String, ByVal partid As String, ByVal note As String, ByVal shippertrack As String, ByVal freighteta As String, ByVal vendorinvoiceno As String, ByVal client As String)
        'On Error GoTo errorz
        Dim strSql As String
        Dim js As New JavaScriptSerializer

        ''get old values for tracking purpose
        Dim strOldTrack, strOldFreightETA, strOldVendorInvoiceNo
        strSql = "select shippertrack, freighteta, vendorinvoiceno from tblpartorder where partid = " & partid
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    strOldTrack = r("shippertrack").ToString
                    strOldFreightETA = r("freighteta").ToString
                    strOldVendorInvoiceNo = r("vendorinvoiceno").ToString
                End While
            End Using
        End Using


        'update values


        strSql = "update tblpartorder set reminder = '" & note.Replace("'", "") & "',"
        strSql = strSql & " shippertrack = '" & shippertrack & "',"
        strSql = strSql & " vendorinvoiceno = '" & vendorinvoiceno & "',"
        If freighteta = "" Then
            strSql = strSql & " freighteta = null"
        Else
            strSql = strSql & " freighteta = '" & freighteta & "'"
        End If
        strSql = strSql & " where partid = " & partid

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        'tracking
        If strOldTrack <> shippertrack Then
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("usp_AddTrack", conn)
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.Add("@orderid", SqlDbType.Int)
                sqlComm.Parameters("@orderid").Value = orderid
                sqlComm.Parameters.Add("@trackuser", SqlDbType.NVarChar)
                sqlComm.Parameters("@trackuser").Value = user
                sqlComm.Parameters.Add("@trackaction", SqlDbType.NVarChar)
                sqlComm.Parameters("@trackaction").Value = "Tracking number changed from '" & strOldTrack & "' to '" & shippertrack & "'"
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
        End If
        If strOldFreightETA <> freighteta Then
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("usp_AddTrack", conn)
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.Add("@orderid", SqlDbType.Int)
                sqlComm.Parameters("@orderid").Value = orderid
                sqlComm.Parameters.Add("@trackuser", SqlDbType.NVarChar)
                sqlComm.Parameters("@trackuser").Value = user
                sqlComm.Parameters.Add("@trackaction", SqlDbType.NVarChar)
                sqlComm.Parameters("@trackaction").Value = "Freight ETA changed from '" & strOldFreightETA & "' to '" & freighteta & "'"
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
        End If
        If strOldVendorInvoiceNo <> vendorinvoiceno Then
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("usp_AddTrack", conn)
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.Add("@orderid", SqlDbType.Int)
                sqlComm.Parameters("@orderid").Value = orderid
                sqlComm.Parameters.Add("@trackuser", SqlDbType.NVarChar)
                sqlComm.Parameters("@trackuser").Value = user
                sqlComm.Parameters.Add("@trackaction", SqlDbType.NVarChar)
                sqlComm.Parameters("@trackaction").Value = "Vendor Invoice No. changed from '" & strOldVendorInvoiceNo & "' to '" & vendorinvoiceno & "'"
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
        End If
        Return js.Serialize(True)
        '        Exit Function
        'errorz:
        '        Return js.Serialize(False)

    End Function
    <WebMethod()>
    Public Function SaveIncompletereturns(ByVal partid As String, ByVal note As String, ByVal client As String)
        On Error GoTo errorz
        Dim strSql As String
        Dim js As New JavaScriptSerializer

        strSql = "update tblpartorder set reminder = '" & note.Replace("'", "") & "' where partid = " & partid
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        Return js.Serialize(True)
        Exit Function
errorz:
        Return js.Serialize(False)

    End Function

    <WebMethod()>
    Public Function AddPart(ByVal orderid As String, ByVal client As String)
        On Error GoTo errorz
        Dim strSql As String
        Dim js As New JavaScriptSerializer

        strSql = "exec usp_AddPart " & orderid
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        Return True
        Exit Function
errorz:
        Return False

    End Function

    <WebMethod()>
    Public Function GetToOrder(ByVal client As String)
        Dim strSql As String
        Dim list As New List(Of Order)

        Dim js As New JavaScriptSerializer


        strSql = "SELECT tblOrder.OrderID, tblOrder.DateOrdered, tblCompany.Company, tblOrder.AutoYear, tblOrder.AutoMake, tblOrder.AutoModel, tblOrder.Mileage, tblOrder.VinNo FROM tblOrder INNER JOIN tblPartOrder ON tblOrder.OrderID = tblPartOrder.OrderID INNER JOIN tblCompany ON tblOrder.CustomerNo = tblCompany.CustomerNo WHERE (tblPartOrder.Cancelled <> 1) AND (tblPartOrder.DateOrdered IS NULL) GROUP BY tblOrder.OrderID, tblOrder.DateOrdered, tblCompany.Company, tblOrder.AutoMake, tblOrder.Mileage, tblOrder.VinNo, tblOrder.AutoYear, tblOrder.AutoModel"
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim o1 As New Order

                    o1.Company = r("Company").ToString
                    o1.DateOrdered = TimeAgo(r("DateOrdered").ToString)
                    o1.Mileage = r("Mileage").ToString
                    o1.OrderID = r("orderid").ToString
                    o1.AutoMake = r("automake").ToString
                    o1.AutoModel = r("automodel").ToString
                    o1.AutoYear = r("autoyear").ToString
                    o1.VinNo = r("vinno").ToString
                    list.Add(o1)
                End While
            End Using
        End Using
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetToOrderPart(ByVal orderid As String, ByVal client As String)
        Dim strSql As String
        Dim list As New List(Of PartOrder)

        Dim js As New JavaScriptSerializer


        strSql = "SELECT orderid,partid,parttype,partno,partdescription,quantity, sellprice, servicer,address1,city,state,zip,phone FROM [tblPartOrder] where dateordered is null and cancelled <> 1 and orderid=" & orderid
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim p1 As New PartOrder

                    p1.Address1 = r("Address1").ToString
                    p1.City = r("city").ToString
                    p1.OrderID = orderid
                    p1.PartDescription = r("partdescription").ToString
                    p1.PartID = r("partid").ToString
                    p1.PartNo = r("partno").ToString
                    p1.PartType = r("parttype").ToString
                    p1.Phone = r("phone").ToString
                    p1.Quantity = r("quantity").ToString
                    p1.SellPrice = r("sellprice").ToString
                    p1.Servicer = r("servicer").ToString
                    p1.State = r("state").ToString
                    p1.Zip = r("zip").ToString
                    list.Add(p1)
                End While
            End Using
        End Using
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetPartTypes()
        Dim c As New List(Of CKPartType)
        Dim js As New JavaScriptSerializer
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select description, [group] as partdescgroup from tblpartdesc inner join tblpartdescgroup on tblpartdesc.groupid=tblpartdescgroup.groupid order by description", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim c1 As New CKPartType
                    c1.Part = r("description").ToString
                    c1.PartDescGroup = r("partdescgroup").ToString
                    c.Add(c1)
                End While
            End Using
        End Using
        Return js.Serialize(c)
    End Function
    <WebMethod()>
    Public Function GetNotes(ByVal orderid As String, ByVal client As String)
        Dim list As New List(Of Notes)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = "SELECT NoteID, OrderID, CONVERT(VARCHAR, NoteDate, 100) as NoteDate, NoteUser, CONVERT (varchar(500), Notes) AS Notes, type, Vendor FROM dbo.tblNotes WHERE OrderID = '" & orderid & "' ORDER BY NoteID"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim n1 As New Notes
                    Dim objType As Type = n1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try

                            prop.SetValue(n1, r(prop.Name), Nothing)


                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(n1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function AddNote(ByVal orderid As Long, ByVal note As String, ByVal vendor As Boolean, ByVal enteredby As String, ByVal client As String)
        If client = "CK" Then
            Return InsertNote(orderid, note, vendor, enteredby)
        Else
            Return False
        End If

    End Function
    <WebMethod()>
    Public Function GetHeaderInfo(ByVal orderid As String, ByVal client As String)

        Dim js As New JavaScriptSerializer
        Dim h1 As New Header

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = "select top 1 tblCompany.Company as Customer, tblCompany_1.Company as Vendor, Cancelled, AutoYear + ' ' + AutoMake + ' ' + AutoModel as Vehicle, Mileage, VinNo, convert(varchar,tblOrder.DateOrdered,101) as DateOrdered from tblorder inner join tblCompany on tblorder.customerno=tblcompany.customerno inner join tblpartorder on tblpartorder.orderid=tblorder.orderid inner join tblcompany as tblcompany_1 on tblcompany_1.companyid=tblpartorder.vendor where tblorder.orderid=" & orderid
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()

                    Dim objType As Type = h1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(h1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next

                End While
            End Using
        End Using

        Return js.Serialize(h1)
    End Function
    <WebMethod()>
    Public Function GetOrderParts(ByVal orderid As String, ByVal client As String)
        Dim list As New List(Of PartOrder)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = " SELECT ROW_NUMBER() OVER(ORDER BY dbo.tblPartOrder.PartID) AS Row, CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.PreviousPartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.DateEntered, convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, dbo.tblPartOrder.PartNo, dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.PartDescription2 AS Serial, dbo.tblPartOrder.Quantity, dbo.tblPartOrder.Weight,  Length, Width, Height, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.Servicer, dbo.tblPartOrder.Address1, dbo.tblPartOrder.City, dbo.tblPartOrder.State, dbo.tblPartOrder.Zip, dbo.tblPartOrder.Phone, dbo.tblPartOrder.Fax, dbo.tblPartOrder.Contact, dbo.tblPartOrder.LaborRate,  dbo.tblPartOrder.Core, dbo.tblPartOrder.SellPrice, dbo.tblPartOrder.ShippingPrice, ShippingCost, CustShippingPrice, CustCoreShippingPrice, WarrantyCost, dbo.tblPartOrder.CorePrice, CoreCost, CoreCredit, dbo.tblPartOrder.CostPrice, IncorrectDefectCredit, CoreCharge, FreightInvoice, CoreInvoice, PartRefund, CoreRefund, dbo.tblPartOrder.Warranty,  dbo.tblPartOrder.EOCInfo, dbo.tblPartOrder.WarrantyMileage, dbo.tblPartOrder.WarrantyDate, convert(varchar,dbo.tblPartOrder.ArrivalDate,101) AS Arrival,  convert(varchar,dbo.tblPartOrder.ArrivalDate2,101) AS UpdatedArrival, convert(varchar,dbo.tblPartOrder.ArriveDate,101) as ArriveDate, convert(varchar,dbo.tblPartOrder.FreightETA,101) as FreightETA, convert(varchar,dbo.tblPartOrder.ExpShipDate,101) as ExpShipDate,  dbo.tblPartOrder.AcReminder AS Reminder, dbo.tblPartOrder.ActiveProblemStatus AS ReminderNotes, dbo.tblPartOrder.CoreReceived, dbo.tblPartOrder.Defect,  dbo.tblPartOrder.DefectReturned, dbo.tblPartOrder.Incorrect, dbo.tblPartOrder.IncorrectReturned, dbo.tblPartOrder.Cancelled, dbo.tblPartOrder.SupplementalPart, dbo.tblPartOrder.VendorInvoiceNo, dbo.tblPartOrder.ListPrice, dbo.tblPartOrder.Warehouse, dbo.tblPartOrder.PartType, dbo.tblPartOrder.PartDescGroup, dbo.tblPartOrder.Brand FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE dbo.tblPartOrder.OrderID = " & orderid

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New PartOrder
                    Dim objType As Type = p1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(p1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(p1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetOrderPartsCollection(ByVal orderid As String, ByVal client As String) As List(Of PartOrder)
        Dim list As New List(Of PartOrder)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = " SELECT ROW_NUMBER() OVER(ORDER BY dbo.tblPartOrder.PartID) AS Row, CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.PreviousPartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.DateEntered, convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, dbo.tblPartOrder.PartNo, dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.PartDescription2 AS Serial, dbo.tblPartOrder.Quantity, dbo.tblPartOrder.Weight,  Length, Width, Height, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.Servicer, dbo.tblPartOrder.Address1, dbo.tblPartOrder.City, dbo.tblPartOrder.State, dbo.tblPartOrder.Zip, dbo.tblPartOrder.Phone, dbo.tblPartOrder.Fax, dbo.tblPartOrder.Contact, dbo.tblPartOrder.LaborRate,  dbo.tblPartOrder.Core, dbo.tblPartOrder.SellPrice, dbo.tblPartOrder.ShippingPrice, ShippingCost, CustShippingPrice, CustCoreShippingPrice, WarrantyCost, dbo.tblPartOrder.CorePrice, CoreCost, CoreCredit, dbo.tblPartOrder.CostPrice, IncorrectDefectCredit, CoreCharge, FreightInvoice, CoreInvoice, PartRefund, CoreRefund, dbo.tblPartOrder.Warranty,  dbo.tblPartOrder.EOCInfo, dbo.tblPartOrder.WarrantyMileage, dbo.tblPartOrder.WarrantyDate, convert(varchar,dbo.tblPartOrder.ArrivalDate,101) AS Arrival,  convert(varchar,dbo.tblPartOrder.ArrivalDate2,101) AS UpdatedArrival, convert(varchar,dbo.tblPartOrder.ArriveDate,101) as ArriveDate, convert(varchar,dbo.tblPartOrder.FreightETA,101) as FreightETA, convert(varchar,dbo.tblPartOrder.ExpShipDate,101) as ExpShipDate,  dbo.tblPartOrder.AcReminder AS Reminder, dbo.tblPartOrder.ActiveProblemStatus AS ReminderNotes, dbo.tblPartOrder.CoreReceived, dbo.tblPartOrder.Defect,  dbo.tblPartOrder.DefectReturned, dbo.tblPartOrder.Incorrect, dbo.tblPartOrder.IncorrectReturned, dbo.tblPartOrder.Cancelled, dbo.tblPartOrder.SupplementalPart, dbo.tblPartOrder.VendorInvoiceNo, dbo.tblPartOrder.ListPrice, dbo.tblPartOrder.Warehouse, dbo.tblPartOrder.PartType, dbo.tblPartOrder.PartDescGroup, dbo.tblPartOrder.Brand FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE dbo.tblPartOrder.OrderID = " & orderid

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New PartOrder
                    Dim objType As Type = p1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(p1, r(prop.Name), Nothing)
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next
                    list.Add(p1)
                End While
            End Using
        End Using

        Return list
    End Function

    <WebMethod()>
    Public Function CancelOrder(ByVal orderid As Long, ByVal enteredby As String, ByVal client As String)
        Try
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommCancel As New SqlCommand("Update tblpartorder set cancelled=1 where orderid=" & orderid, conn)
                conn.Open()
                sqlCommCancel.ExecuteNonQuery()
            End Using
            'tracking
            Return InsertTracking(orderid, "Cancelled order.", enteredby)
        Catch Ex As Exception
            Return Ex.ToString()
        End Try

    End Function

    <WebMethod()>
    Public Function GetQuoteByClient(ByVal partTypeID As Integer, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Try
            Return js.Serialize(GetQuotes(partTypeID, client))
        Catch ex As Exception

        End Try

    End Function

    <WebMethod()>
    Public Function GetQuoteByQuoteID(ByVal quoteID As String)
        Dim js As New JavaScriptSerializer()
        Try
            Return js.Serialize(GetQuotes(Convert.ToInt32(Enums.PartTypes.All), [Enum].GetName(GetType(Enums.PigeonClientIDs), Convert.ToInt32(quoteID.Substring(0, quoteID.IndexOf("-"))))).Where(Function(a) a.QuoteID = quoteID).FirstOrDefault())
        Catch ex As Exception

        End Try
    End Function

    <WebMethod()>
    Public Function GetQuoteByCustomerEmail(ByVal partTypeID As Integer, ByVal customerEmail As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Try
            Return js.Serialize(GetQuotes(partTypeID, client).Where(Function(a) a.CustomerContactEmail = customerEmail).ToList())
        Catch ex As Exception

        End Try
    End Function

    <WebMethod()>
    Public Function GetQuoteByCustomer(ByVal partTypeID As Integer, ByVal client As String, ByVal customer As String)
        Dim js As New JavaScriptSerializer()
        Try
            Return js.Serialize(GetQuotes(partTypeID, client).Where(Function(a) a.Customer = customer).ToList())
        Catch ex As Exception

        End Try
    End Function

    <WebMethod()>
    Public Function GetQuotesByPartTypeID(ByVal partTypeID As Integer, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Try
            Return js.Serialize(GetQuotes(partTypeID, client))
        Catch ex As Exception

        End Try
    End Function

    Private Function GetQuotes(ByVal partTypeID As Integer, ByVal client As String) As List(Of Quote)
        Try

            Dim queryBuilder As New StringBuilder
            Select Case CType(partTypeID, Enums.PartTypes)
                Case Enums.PartTypes.All

                    If client = Enums.PigeonClientIDs.CK.ToString Then
                        Dim quotelist As New List(Of Quote)
                        For Each customerClient As Integer In CType([Enum].GetValues(GetType(Enums.PigeonClientIDs)), Integer())

#If DEBUG Then
                            If CType(customerClient, Enums.PigeonClientIDs) = Enums.PigeonClientIDs.GoPower Or CType(customerClient, Enums.PigeonClientIDs) = Enums.PigeonClientIDs.CK Then
                                Dim ConnectionString As String = ConnectionStrings.GetSpecificConnectionString([Enum].GetName(GetType(Enums.PigeonClientIDs), customerClient))
                                If ConnectionString <> Nothing Then
                                    Dim Catalog As String = ConnectionString.Substring(ConnectionString.IndexOf("Initial Catalog=") + 16)
                                    Catalog = "[" & Catalog.Substring(0, Catalog.IndexOf(";")) & "]"
                                    If customerClient <> Convert.ToInt32(Enums.PigeonClientIDs.GoPower) Then
                                        queryBuilder.Append("SELECT top 500 CONCAT('" & [Enum].Parse(GetType(Enums.PigeonClientIDs), customerClient) & "','-'," & Catalog & ".dbo.tblRemanQuotes.QuoteID) as ID, " & Catalog & ".dbo.tblRemanQuotes.customerEmail, " & Catalog & ".dbo.tblRemanQuotes.QuoteDate, 
            " & Catalog & ".dbo.tblRemanQuotes.Username, case when  " & Catalog & ".dbo.tblCompany.Company is null 
            then 'Internal' else " & Catalog & ".dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            " & Catalog & ".dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, " & Catalog & ".dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice," & Catalog & ".dbo.tblRemanQuotes.VIN, " & Catalog & ".dbo. tblRemanQuotes.warranty, " & Catalog & ".dbo. tblRemanQuotes.notes FROM " & Catalog & ".dbo.tblRemanQuotes 
            left outer JOIN " & Catalog & ".dbo.tblCompany ON 
            " & Catalog & ".dbo.tblRemanQuotes.CustomerNo = " & Catalog & ".dbo.tblCompany.CustomerNo
             Union All ")
                                    Else
                                        queryBuilder.Append("SELECT top 500 CONCAT('" & [Enum].Parse(GetType(Enums.PigeonClientIDs), customerClient) & "','-'," & Catalog & ".dbo.tblRemanQuotes.QuoteID) as ID, " & Catalog & ".dbo.tblRemanQuotes.customerEmail, " & Catalog & ".dbo.tblRemanQuotes.QuoteDate, 
            " & Catalog & ".dbo.tblRemanQuotes.Username, case when  " & Catalog & ".dbo.tblCompany.Company is null 
            then 'Internal' else " & Catalog & ".dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            " & Catalog & ".dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, " & Catalog & ".dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice, " & Catalog & ".dbo.tblRemanQuotes.VIN, " & Catalog & ".dbo. tblRemanQuotes.warranty, " & Catalog & ".dbo. tblRemanQuotes.notes FROM " & Catalog & ".dbo.tblRemanQuotes 
            left outer JOIN " & Catalog & ".dbo.tblCompany ON 
            " & Catalog & ".dbo.tblRemanQuotes.CustomerNo = " & Catalog & ".dbo.tblCompany.CustomerNo
            order by QuoteDate desc")
                                    End If
                                End If
                            End If
                        Next
#Else

                            Dim ConnectionString As String = ConnectionStrings.GetSpecificConnectionString([Enum].GetName(GetType(Enums.PigeonClientIDs), customerClient))
                            If ConnectionString <> Nothing Then
                                Dim Catalog As String = ConnectionString.Substring(ConnectionString.IndexOf("Initial Catalog=") + 16)
                                Catalog = "[" & Catalog.Substring(0, Catalog.IndexOf(";")) & "]"
                                '                        If customerClient < [Enum].GetNames(GetType(Enums.PigeonClientIDs)).Length Then
                                '                            queryBuilder.Append("SELECT top 500 CONCAT('" & [Enum].Parse(GetType(Enums.PigeonClientIDs), customerClient) & "','-'," & Catalog & ".dbo.tblRemanQuotes.QuoteID) as ID, " & Catalog & ".dbo.tblRemanQuotes.customerEmail, " & Catalog & ".dbo.tblRemanQuotes.QuoteDate, 
                                '" & Catalog & ".dbo.tblRemanQuotes.Username, case when  " & Catalog & ".dbo.tblCompany.Company is null 
                                'then 'Internal' else " & Catalog & ".dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
                                '" & Catalog & ".dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine, tblRemanQuotes.Size, 
                                'tblRemanQuotes.Options, " & Catalog & ".dbo.tblRemanQuotes.PartNo, 
                                'tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
                                'tblRemanQuotes.coreprice," & Catalog & ".dbo.tblRemanQuotes.VIN, " & Catalog & ".dbo. tblRemanQuotes.warranty, " & Catalog & ".dbo. tblRemanQuotes.notes FROM " & Catalog & ".dbo.tblRemanQuotes 
                                'left outer JOIN " & Catalog & ".dbo.tblCompany ON 
                                '" & Catalog & ".dbo.tblRemanQuotes.CustomerNo = " & Catalog & ".dbo.tblCompany.CustomerNo
                                ' Union All ")
                                '                        Else
                                queryBuilder.Clear()
                                queryBuilder.Append("SELECT top 750 CONCAT('" & [Enum].Parse(GetType(Enums.PigeonClientIDs), customerClient) & "','-'," & Catalog & ".dbo.tblRemanQuotes.QuoteID) as ID, " & Catalog & ".dbo.tblRemanQuotes.customerEmail, " & Catalog & ".dbo.tblRemanQuotes.QuoteDate, 
            " & Catalog & ".dbo.tblRemanQuotes.Username, case when  " & Catalog & ".dbo.tblCompany.Company is null 
            then 'Internal' else " & Catalog & ".dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            " & Catalog & ".dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, " & Catalog & ".dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice, " & Catalog & ".dbo.tblRemanQuotes.VIN, " & Catalog & ".dbo. tblRemanQuotes.warranty, " & Catalog & ".dbo. tblRemanQuotes.notes FROM " & Catalog & ".dbo.tblRemanQuotes 
            left outer JOIN " & Catalog & ".dbo.tblCompany ON 
            " & Catalog & ".dbo.tblRemanQuotes.CustomerNo = " & Catalog & ".dbo.tblCompany.CustomerNo
            order by ID desc")
                                'End If
                                Using conn As New SqlConnection(ConnectionString)
                                    Dim sqlComm As New SqlCommand(queryBuilder.ToString, conn)
                                    conn.Open()
                                    Using r As SqlDataReader = sqlComm.ExecuteReader()

                                        While r.Read()
                                            Dim q1 As New Quote()
                                            q1.QuoteID = r("ID").ToString
                                            q1.QuoteDate = r("QuoteDate").ToString
                                            q1.User = r("UserName").ToString
                                            q1.CustomerContactEmail = r("customerEmail").ToString
                                            q1.Customer = r("Company").ToString
                                            If (CType(partTypeID, Enums.PartTypes) <> Enums.PartTypes.OEM) Then
                                                q1.pigeonCompany = [Enum].Parse(GetType(Enums.PigeonClientIDs), Convert.ToInt32(r("ID").ToString.Substring(0, r("ID").ToString.IndexOf("-")))).ToString
                                                q1.Year = r("Year").ToString
                                                q1.Model = r("Model").ToString
                                                q1.vin = r("VIN").ToString
                                                q1.warranty = r("Warranty").ToString
                                                q1.notes = r("notes").ToString
                                            End If
                                            q1.Make = r("Make").ToString
                                            q1.Part = r("PartNo").ToString
                                            q1.LocalStock = r("LocalStock").ToString
                                            q1.CorePrice = r("coreprice")
                                            q1.Cost = r("costprice")
                                            q1.SellPrice = r("sellprice")
                                            quotelist.Add(q1)
                                        End While
                                    End Using
                                End Using
                            End If
                             Next
                        Return quotelist
#End If

                    Else
                        queryBuilder.Append("Select top 750 CONCAT('" & [Enum].Parse(GetType(Enums.PigeonClientIDs), client) & "','-', dbo.tblRemanQuotes.QuoteID) As ID, dbo.tblRemanQuotes.customerEmail, dbo.tblRemanQuotes.QuoteDate,
            dbo.tblRemanQuotes.Username, case when  dbo.tblCompany.Company is null 
            then 'Internal' else dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice,tblRemanQuotes.VIN, tblRemanQuotes.warranty, tblRemanQuotes.notes FROM dbo.tblRemanQuotes 
            left outer JOIN dbo.tblCompany ON 
            dbo.tblRemanQuotes.CustomerNo = dbo.tblCompany.CustomerNo
            order by QuoteDate desc")
                    End If

                   'If All Do something Different..Big Ass Union
                Case Enums.PartTypes.OEM
                    queryBuilder.Append("Select top 750 dbo.tblOemQuotes.QuoteID As ID, dbo.tblOemQuotes.QuoteDate, dbo.tblOemQuotes.Username, dbo.tblCompany.Company, dbo.tblMake.Make, dbo.tblOemQuotes.Part As PartNo, dbo.tblOemQuotes.Description, dbo.tblOemQuotes.Quantity, dbo.tblOemQuotes.Email As customerEmail, dbo.tblOemQuotes.ListPrice, dbo.tblOemQuotes.CorePrice, dbo.tblOemQuotes.OurCost As costprice, dbo.tblOemQuotes.TheirPrice As sellprice, dbo.tblOemQuotes.InStock As LocalStock FROM dbo.tblOemQuotes INNER JOIN dbo.tblMake On dbo.tblOemQuotes.Make = dbo.tblMake.OEMID INNER JOIN dbo.tblCompany On dbo.tblOemQuotes.CustomerNo = dbo.tblCompany.CustomerNo  order by QuoteDate desc")
                Case Else
                    queryBuilder.Append("Select top 750 CONCAT('" & [Enum].Parse(GetType(Enums.PigeonClientIDs), client) & "','-', dbo.tblRemanQuotes.QuoteID) As ID, dbo.tblRemanQuotes.customerEmail, dbo.tblRemanQuotes.QuoteDate,
            dbo.tblRemanQuotes.Username, case when  dbo.tblCompany.Company is null 
            then 'Internal' else dbo.tblCompany.Company end as Company, tblRemanQuotes.[Year], 
            dbo.tblRemanQuotes.Make, tblRemanQuotes.Model, tblRemanQuotes.Engine, tblRemanQuotes.Size, 
            tblRemanQuotes.Options, dbo.tblRemanQuotes.PartNo, 
            tblRemanQuotes.LocalStock, tblRemanQuotes.CostPrice, tblRemanQuotes.SellPrice,
            tblRemanQuotes.coreprice, tblRemanQuotes.VIN, tblRemanQuotes.warranty, tblRemanQuotes.notes FROM dbo.tblRemanQuotes 
            left outer JOIN dbo.tblCompany ON 
            dbo.tblRemanQuotes.CustomerNo = dbo.tblCompany.CustomerNo
            where tblRemanQuotes.PartTypeID=" & partTypeID & " order by QuoteDate desc")
            End Select

            Dim list As New List(Of Quote)
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand(queryBuilder.ToString, conn)

                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()

                    While r.Read()
                        Dim q1 As New Quote()
                        'TODO: Do we want the OEM to have the Company Number in fron tof it too?..or just Reman..
                        q1.QuoteID = r("ID").ToString
                        q1.QuoteDate = r("QuoteDate").ToString
                        q1.User = r("UserName").ToString
                        q1.CustomerContactEmail = r("customerEmail").ToString
                        q1.Customer = r("Company").ToString
                        If (CType(partTypeID, Enums.PartTypes) <> Enums.PartTypes.OEM) Then
                            q1.pigeonCompany = [Enum].Parse(GetType(Enums.PigeonClientIDs), Convert.ToInt32(r("ID").ToString.Substring(0, r("ID").ToString.IndexOf("-")))).ToString
                            q1.Year = r("Year").ToString
                            q1.Model = r("Model").ToString
                            q1.vin = r("VIN").ToString
                            q1.warranty = r("Warranty").ToString
                            q1.notes = r("notes").ToString
                        End If
                        q1.Make = r("Make").ToString
                        q1.Part = r("PartNo").ToString
                        q1.LocalStock = r("LocalStock").ToString
                        q1.CorePrice = r("coreprice")
                        q1.Cost = r("costprice")
                        q1.SellPrice = r("sellprice")
                        list.Add(q1)
                    End While
                End Using
            End Using
            'Currently Ordering By QuoteID should I do QuoteDate?..
            Return list
        Catch ex As Exception

        End Try
    End Function

    <WebMethod()>
    Public Function ProcessMainQuote(ByVal partTypeID As Integer, ByVal Parts As List(Of Pigeon.Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal VIN As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal Auth As String, ByVal Contract As String, ByVal client As String, ByVal customernumberbehalf As String, ByVal adjuster As String, ByVal email As String, ByVal customerClient As String, ByVal notes As String, ByVal quoteID As String) As String

        Return Pigeon.ProcessMainQuote(partTypeID, Parts, IIf(GetUserRole(name, client) = "Admin", GetUserNameByEmail(email, customerClient), name), year, make, model, PO, Customer, Mileage, VIN, RepairFacility, Address, City, State, Zip, Phone, Contact, warranty, Auth, Contract, client, customernumberbehalf, adjuster, email, notes, quoteID)
    End Function
    Public Function InsertTracking(ByVal orderid As Long, ByVal action As String, ByVal enteredby As String)
        Try
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlCommTracking As New SqlCommand("Insert into tblOrderTrack(orderid, trackdate,trackaction,trackuser) values (" & orderid & ", { fn now() },'" & action.Replace("'", "") & "','" & enteredby & "')", conn)

                conn.Open()
                sqlCommTracking.ExecuteNonQuery()
                Return True
            End Using
        Catch Ex As Exception
            Return Ex.ToString()
        End Try
    End Function
    <WebMethod()>
    Public Function GetDocs(ByVal orderid As String, ByVal client As String)

        Dim js As New JavaScriptSerializer
        Dim list As New List(Of Document)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = "select docid, convert(varchar,uploaddate,101) as uploaddate,filename,uploadedby,cdn from tbldocuments inner join tblorder on tblorder.orderid=tbldocuments.orderid where tbldocuments.orderid=" & orderid & " and deleted=0 order by docid desc"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim d1 As New Document
                    d1.uploaddate = r("uploaddate").ToString
                    d1.filename = r("filename").ToString
                    d1.uploadedby = r("uploadedby").ToString
                    If UCase(r("filename")).ToString.Contains("JPG") Or UCase(r("filename")).ToString.Contains("JPEG") Or UCase(r("filename")).ToString.Contains("PNG") Or UCase(r("filename")).ToString.Contains("GIF") Then
                        d1.url = r("cdn").ToString & "/" & r("filename").ToString
                        d1.thumbnailurl = r("cdn").ToString & "/" & r("filename").ToString
                    ElseIf UCase(r("filename")).ToString.Contains("DOC") Then
                        d1.url = r("cdn").ToString & "/" & r("filename").ToString
                        d1.thumbnailurl = "../images/word.png"
                    ElseIf UCase(r("filename")).ToString.Contains("XLS") Then
                        d1.url = r("cdn").ToString & "/" & r("filename").ToString
                        d1.thumbnailurl = "../images/excel.png"
                    ElseIf UCase(r("filename")).ToString.Contains("PDF") Then
                        d1.url = r("cdn").ToString & "/" & r("filename").ToString
                        d1.thumbnailurl = "../images/pdf.png"
                    Else
                        d1.url = r("cdn").ToString & "/" & r("filename").ToString
                        d1.thumbnailurl = "../images/unknown.png"
                    End If
                    d1.docid = r("docid").ToString
                    list.Add(d1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function DeleteDoc(ByVal docid As String, ByVal client As String)
        Try
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "update tbldocuments set deleted=1 where docid=" & docid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
            Return True
        Catch Ex As Exception
            Return Ex.ToString()
        End Try
    End Function
    <WebMethod()>
    Public Function GetHistory(ByVal orderid As String, ByVal client As String)
        Dim list As New List(Of History)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = "SELECT CONVERT(VARCHAR, TrackDate, 100) as TrackDate, TrackUser, TrackAction FROM dbo.tblOrderTrack WHERE OrderID = '" & orderid & "' ORDER BY TrackID"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim n1 As New History
                    Dim objType As Type = n1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try

                            prop.SetValue(n1, r(prop.Name), Nothing)


                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(n1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetInvoices(ByVal orderid As String, ByVal client As String)
        Dim list As New List(Of Invoice)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))

            Dim sqlComm As New SqlCommand("usp_Invoices", conn)
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.Add("@orderid", SqlDbType.Int)
            sqlComm.Parameters("@orderid").Value = orderid
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim i1 As New Invoice
                    Dim objType As Type = i1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try

                            prop.SetValue(i1, r(prop.Name), Nothing)


                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(i1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetOrderInfo(ByVal orderid As String, ByVal client As String)
        Dim list As New List(Of OrderInfo)
        Dim js As New JavaScriptSerializer

        'todo: orderscreen launch-this update statement for warranty
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("update tblorder set contractenddate=warrantydate, contractendmiles=warrantymileage from tblorder,tblpartorder where tblorder.orderid = " & orderid & " and tblorder.orderid=tblpartorder.orderid", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()

        End Using

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))

            Dim sqlComm As New SqlCommand("select tblOrder.OrderID, tblOrder.CustomerNo, tblCompany.CompanyID, convert(varchar,DateOrdered,101) as DateOrdered, Company, AdjusterName, ContractNo, AuthorizationNo, AutoOwner, ContractEndDate, ContractEndMiles, Mileage, AutoYear, AutoMake, AutoModel, Drive, Transmission, VinNo  from tblorder inner join tblcompany on tblorder.customerno=tblcompany.customerno where tblorder.orderid = " & orderid, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim i1 As New OrderInfo
                    Dim objType As Type = i1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try

                            prop.SetValue(i1, r(prop.Name), Nothing)


                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(i1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function DeleteInvoice(ByVal orderid As String, ByVal invoiceid As String, ByVal enteredby As String, ByVal client As String)
        Dim strInvoiceType As String

        If enteredby <> "darrellb" And enteredby <> "tj_smith" And enteredby <> "joshw" And enteredby <> "rickm" And enteredby <> "staceyp" And enteredby <> "jamiew" And enteredby <> "carolk" Then Return "Not authorized to delete invoices."

        Try
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "select invoicetype from tblinvoices inner join tblinvoicetype on tblinvoices.invoicetypeid=tblinvoicetype.invoicetypeid where invoiceid=" & invoiceid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                strInvoiceType = sqlComm.ExecuteScalar()
            End Using
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "update tblinvoices set deleted=1 where invoiceid=" & invoiceid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            Return InsertTracking(orderid, "Deleted " & strInvoiceType & " invoice.", enteredby)
        Catch Ex As Exception
            Return Ex.ToString()
        End Try
    End Function
    <WebMethod()>
    Public Function SaveInvoice(ByVal orderid As String, ByVal invoiceid As String, ByVal amountpaid As String, ByVal datepaid As String, ByVal paytype As String, ByVal checkno As String, ByVal enteredby As String, ByVal client As String)
        Dim strInvoiceType, strInvoiceNo, strPayType, strCheckNo, strDatePaid As String
        Dim decAmountpaid As Decimal

        If enteredby <> "darrellb" And enteredby <> "tj_smith" And enteredby <> "joshw" And enteredby <> "rickm" And enteredby <> "staceyp" And enteredby <> "jamiew" And enteredby <> "carolk" Then Return "Not authorized to make changes."

        Try
            'get initial values
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "select invoicetype,paymenttype,checkno,amountpaid,datepaid from tblinvoices inner join tblinvoicetype on tblinvoices.invoicetypeid=tblinvoicetype.invoicetypeid where invoiceid=" & invoiceid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader
                    While r.Read()
                        strInvoiceType = r("invoicetype").ToString
                        strPayType = r("paymenttype").ToString
                        strCheckNo = r("checkno").ToString
                        decAmountpaid = CDec(r("amountpaid"))
                        strDatePaid = r("datepaid").ToString
                    End While
                End Using

            End Using
            'update
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "update tblinvoices set amountpaid=" & CDec(IIf(String.IsNullOrEmpty(amountpaid), 0, amountpaid)) & IIf(String.IsNullOrEmpty(datepaid), ", datepaid=null", ", datepaid='" & datepaid & "'") & IIf(String.IsNullOrEmpty(paytype) Or paytype = "None", ", paymenttype=null", ", paymenttype='" & paytype & "'") & IIf(String.IsNullOrEmpty(checkno), ", checkno=null", ", checkno='" & checkno & "'") & " where invoiceid=" & invoiceid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            Dim strTracking As String
            If String.IsNullOrEmpty(strDatePaid) = False And String.IsNullOrEmpty(datepaid) = False Then
                strDatePaid = FormatDateTime(strDatePaid)
                datepaid = FormatDateTime(datepaid)
            End If
            If decAmountpaid <> CDec(amountpaid) Or strDatePaid <> datepaid <> 0 Or strPayType <> paytype Or strCheckNo <> checkno Then
                strTracking = strInvoiceType & "("
                If decAmountpaid <> CDec(amountpaid) Then strTracking = strTracking & "Amount Paid changed from " & FormatCurrency(decAmountpaid, 2) & " to " & FormatCurrency(IIf(String.IsNullOrEmpty(amountpaid), 0, amountpaid), 2) & ","
                If strDatePaid <> datepaid Then strTracking = strTracking & "Date Paid changed from " & IIf(String.IsNullOrEmpty(strDatePaid), "blank", strDatePaid) & " to " & IIf(String.IsNullOrEmpty(datepaid), "blank", datepaid) & ","
                If strPayType <> paytype Then strTracking = strTracking & "Payment Type changed from " & IIf(String.IsNullOrEmpty(strPayType), "blank", strPayType) & " to " & IIf(String.IsNullOrEmpty(paytype) Or paytype = "None", "blank", paytype) & ","
                If strCheckNo <> checkno Then strTracking = strTracking & "Check No changed from " & IIf(String.IsNullOrEmpty(strCheckNo), "blank", strCheckNo) & " to " & IIf(String.IsNullOrEmpty(checkno), "blank", checkno) & ","
                strTracking = strTracking & ")"
                strTracking = strTracking.Replace(",)", ")")
                strTracking = strTracking.Replace(" 12:00:00 AM", "")
                Return InsertTracking(orderid, strTracking, enteredby)
            Else
                Return True
            End If

        Catch Ex As Exception
            Return Ex.ToString()
        End Try
    End Function
    <WebMethod()>
    Public Function SaveOrderInfo(ByVal orderid As String, ByVal adjustername As String, ByVal contractno As String, ByVal authorizationno As String, ByVal autoowner As String, ByVal contractenddate As String, ByVal contractendmiles As String, ByVal mileage As String, ByVal autoyear As String, ByVal automodel As String, ByVal drive As String, ByVal transmission As String, ByVal vinno As String, ByVal enteredby As String, ByVal client As String)
        Dim stradjustername, strcontractno, strauthorizationno, strautoowner, strcontractenddate, strcontractendmiles, strmileage, strautoyear, strautomodel, strdrive, strtransmission, strvinno



        Try
            'get initial values
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "select adjustername, contractno, authorizationno, autoowner, contractenddate, contractendmiles, mileage, autoyear, automodel, drive, transmission, vinno from tblorder where orderid=" & orderid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader
                    While r.Read()
                        stradjustername = r("adjustername").ToString
                        strcontractno = r("contractno").ToString
                        strauthorizationno = r("authorizationno").ToString
                        strautoowner = r("autoowner").ToString
                        strcontractenddate = r("contractenddate").ToString
                        strcontractendmiles = r("Contractendmiles").ToString
                        strmileage = r("mileage").ToString
                        strautoyear = r("autoyear").ToString
                        strautomodel = r("automodel").ToString
                        strdrive = r("drive").ToString
                        strtransmission = r("transmission").ToString
                        strvinno = r("vinno").ToString
                    End While
                End Using

            End Using
            'update
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = "update tblorder set " & IIf(String.IsNullOrEmpty(adjustername), "adjustername=null", "adjustername=" & adjustername.fqq) & IIf(String.IsNullOrEmpty(contractno), ", contractno=null", ", contractno=" & contractno.fqq) & IIf(String.IsNullOrEmpty(authorizationno), ", authorizationno=null", ", authorizationno=" & authorizationno.fqq) & IIf(String.IsNullOrEmpty(autoowner), ", autoowner=null", ", autoowner=" & autoowner.fqq) & IIf(String.IsNullOrEmpty(contractenddate) Or contractenddate = "Jan 1 1900 12:00AM", ", contractenddate=null", ", contractenddate=" & contractenddate.fqq) & IIf(String.IsNullOrEmpty(contractendmiles), ", contractendmiles=null", ", contractendmiles=" & contractendmiles.fqq) & IIf(String.IsNullOrEmpty(mileage), ", mileage=null", ", mileage=" & mileage.fqq) & IIf(String.IsNullOrEmpty(autoyear), ", autoyear=null", ", autoyear=" & autoyear.fqq) & IIf(String.IsNullOrEmpty(automodel), ", automodel=null", ", automodel=" & automodel.fqq) & IIf(String.IsNullOrEmpty(drive), ", drive=null", ", drive=" & drive.fqq) & IIf(String.IsNullOrEmpty(transmission) Or transmission = "Unknown", ", transmission=null", ", transmission=" & transmission.fqq) & IIf(String.IsNullOrEmpty(vinno), ", vinno=null", ", vinno=" & vinno.fqq) & " where orderid=" & orderid
                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            Dim strTracking As String

            If adjustername <> stradjustername Or contractno <> strcontractno Or authorizationno <> strauthorizationno Or autoowner <> strautoowner Or contractenddate <> strcontractenddate Or contractendmiles <> strcontractendmiles Or mileage <> strmileage Or autoyear <> strautoyear Or automodel <> strautomodel Or drive <> strdrive Or transmission <> strtransmission Or vinno <> strvinno Then

                If stradjustername <> adjustername Then strTracking = strTracking & "Adjuster changed from " & IIf(String.IsNullOrEmpty(stradjustername), "blank", stradjustername) & " to " & IIf(String.IsNullOrEmpty(adjustername), "blank", adjustername) & ","
                If strcontractno <> contractno Then strTracking = strTracking & "Contract No changed from " & IIf(String.IsNullOrEmpty(strcontractno), "blank", strcontractno) & " to " & IIf(String.IsNullOrEmpty(contractno), "blank", contractno) & ","
                If strauthorizationno <> authorizationno Then strTracking = strTracking & "Authorization No changed from " & IIf(String.IsNullOrEmpty(strauthorizationno), "blank", strauthorizationno) & " to " & IIf(String.IsNullOrEmpty(authorizationno), "blank", authorizationno) & ","
                If strautoowner <> autoowner Then strTracking = strTracking & "Owner changed from " & IIf(String.IsNullOrEmpty(strautoowner), "blank", strautoowner) & " to " & IIf(String.IsNullOrEmpty(autoowner), "blank", autoowner) & ","
                If strcontractenddate <> contractenddate Then strTracking = strTracking & "Contract End Date changed from " & IIf(String.IsNullOrEmpty(strcontractenddate), "blank", strcontractenddate) & " to " & IIf(String.IsNullOrEmpty(contractenddate), "blank", contractenddate) & ","
                If strcontractendmiles <> contractendmiles Then strTracking = strTracking & "Contract End Miles changed from " & IIf(String.IsNullOrEmpty(strcontractendmiles), "blank", strcontractendmiles) & " to " & IIf(String.IsNullOrEmpty(contractendmiles), "blank", contractendmiles) & ","
                If strmileage <> mileage Then strTracking = strTracking & "Mileage changed from " & IIf(String.IsNullOrEmpty(strmileage), "blank", strmileage) & " to " & IIf(String.IsNullOrEmpty(mileage), "blank", mileage) & ","
                If strautoyear <> autoyear Then strTracking = strTracking & "Year changed from " & IIf(String.IsNullOrEmpty(strautoyear), "blank", strautoyear) & " to " & IIf(String.IsNullOrEmpty(autoyear), "blank", autoyear) & ","
                If strautomodel <> automodel Then strTracking = strTracking & "Model changed from " & IIf(String.IsNullOrEmpty(strautomodel), "blank", strautomodel) & " to " & IIf(String.IsNullOrEmpty(automodel), "blank", automodel) & ","
                If strdrive <> drive Then strTracking = strTracking & "Drive changed from " & IIf(String.IsNullOrEmpty(strdrive), "blank", strdrive) & " to " & IIf(String.IsNullOrEmpty(drive), "blank", drive) & ","
                If strtransmission <> transmission Then strTracking = strTracking & "Transmission changed from " & IIf(String.IsNullOrEmpty(strtransmission), "blank", strtransmission) & " to " & IIf(String.IsNullOrEmpty(transmission), "blank", transmission) & ","
                If strvinno <> vinno Then strTracking = strTracking & "VIN changed from " & IIf(String.IsNullOrEmpty(strvinno), "blank", strvinno) & " to " & IIf(String.IsNullOrEmpty(vinno), "blank", vinno) & ","

                strTracking = strTracking.Replace(",)", ")")
                strTracking = strTracking.Replace(" 12:00:00 AM", "")
                strTracking = strTracking.Trim(",")
                Return InsertTracking(orderid, strTracking, enteredby)
            Else
                Return True
            End If

        Catch Ex As Exception
            Return Ex.ToString()
        End Try
    End Function

    <WebMethod()>
    Public Function GetOEMAvailability(ByVal orderid As String, ByVal client As String)

        Dim TheParts As New List(Of OEMSmallPartPricing.Pricing.Parts)
        Dim strMake As String
        Dim intOEMCount As Integer



        'get make first

        'get makeid
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblMake.MakeID FROM dbo.tblOrder INNER JOIN dbo.tblMake ON dbo.tblOrder.AutoMake = dbo.tblMake.Make WHERE  orderid = '" & orderid & "'", conn)
            conn.Open()
            strMake = sqlComm.ExecuteScalar()
        End Using

        'get parts
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT partno, quantity from tblpartorder WHERE orderid = '" & orderid & "' and partdescgroup = 'OEM' ORDER BY dbo.tblPartOrder.PartID", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New OEMSmallPartPricing.Pricing.Parts
                    p1.PartNumber = r("partno")
                    p1.Quantity = r("quantity")
                    TheParts.Add(p1)
                End While
            End Using
        End Using

        'get oemcount
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT count(partid) from tblpartorder WHERE orderid = '" & orderid & "' and partdescgroup = 'OEM'", conn)
            conn.Open()
            intOEMCount = sqlComm.ExecuteScalar()
        End Using

        Dim oemPricing As New OEMSmallPartPricing.Pricing
        Dim oemResponse As New OEMSearchResult
        oemResponse = oemPricing.GetOEMPrice("456", "false", strMake, "cust", "", TheParts.ToArray)
        Dim lst As New List(Of PartsResponse)
        For Each Part As PartsResponse In oemResponse.list
            lst.Add(Part)
        Next

        'loop through each part and build list of stores and the part(s) each store has
        Dim lstOEMAvail As New List(Of OEMAvailability)
        For Each Part As PartsResponse In lst
            If Part IsNot Nothing AndAlso Part.StockLevels IsNot Nothing AndAlso Part.StockLevels.Count > 0 Then
                For Each StockResponse As StockResponse In Part.StockLevels
                    'does store exist?
                    If FindStoreInOEMAvail(lstOEMAvail, StockResponse.Hyperion) = False Then
                        'not found. add store and part
                        Dim o1 As New OEMAvailability
                        o1.Contact = StockResponse.Contact
                        o1.Cutoff = StockResponse.Cutoff
                        o1.Hyperion = StockResponse.Hyperion
                        o1.Name = StockResponse.Name
                        o1.Note = StockResponse.Note
                        o1.Phone = StockResponse.Phone
                        Dim p1 As New PartsResponse
                        p1.PartNumber = Part.PartNumber
                        p1.Description = Part.Description
                        o1.Parts = New List(Of PartsResponse)
                        o1.Parts.Add(Part)
                        o1.Status = IIf(intOEMCount = 1, "All Parts", "Some Parts")
                        lstOEMAvail.Add(o1)
                    Else
                        'found just add part to store
                        Dim p1 As New PartsResponse
                        p1.PartNumber = Part.PartNumber
                        p1.Description = Part.Description
                        AddPartToStoreInOEMAvail(lstOEMAvail, StockResponse.Hyperion, p1, intOEMCount)
                    End If
                Next
            End If
        Next

        Dim js As New JavaScriptSerializer
        Return js.Serialize(lstOEMAvail)

    End Function

    Private Function FindStoreInOEMAvail(ByVal oemavail As List(Of OEMAvailability), ByVal hyperion As String) As Boolean
        For Each item In oemavail
            If item.Hyperion = hyperion Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    Private Sub AddPartToStoreInOEMAvail(ByRef oemavail As List(Of OEMAvailability), ByVal hyperion As String, ByVal part As PartsResponse, ByVal oemcount As Integer)
        For Each item In oemavail
            If item.Hyperion = hyperion Then
                item.Parts.Add(part)
                item.Status = IIf(oemcount = item.Parts.Count, "All Parts", "Some Parts")
                Exit Sub
            End If
        Next
    End Sub


    <WebMethod()>
    Public Function GetSmallPartOptions(ByVal orderid As String, ByVal client As String)
        Dim lstSmallPartOptions As New List(Of SmallPartOption)
        'get parts
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT partid, partno, vendor,partdescription from tblpartorder WHERE orderid = '" & orderid & "' and partdescgroup = 'Small Parts' ORDER BY dbo.tblPartOrder.PartID", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New SmallPartOption
                    p1.PartNo = r("partno").ToString
                    p1.PartID = r("partid").ToString
                    p1.PartDescription = r("partdescription").ToString
                    p1.Vendor = r("vendor").ToString
                    'get options
                    Dim lstOptions As New List(Of SmallPartQuoteOption)
                    Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm2 As New SqlCommand("SELECT id,company as vendor, companyid, partno, brand, description, ourcost, coreprice, theirprice, stock, stockexception from tblsmallpartoptions inner join tblcompany on tblcompany.companyid=tblsmallpartoptions.vendor WHERE partid = '" & p1.PartID & "' order by stock desc", conn2)
                        conn2.Open()
                        Using r2 As SqlDataReader = sqlComm2.ExecuteReader()
                            While r2.Read()
                                Dim o1 As New SmallPartQuoteOption
                                o1.Brand = r2("brand").ToString
                                o1.CorePrice = r2("coreprice").ToString
                                o1.ID = r2("id")
                                o1.OurCost = r2("ourcost").ToString
                                o1.PartDescription = r2("description").ToString
                                o1.PartNo = r2("partno").ToString
                                o1.StockException = r2("stockexception").ToString
                                o1.Stock = IIf(CBool(o1.StockException) = True, r2("stock").ToString & "*", r2("stock").ToString)

                                o1.TheirPrice = r2("theirprice").ToString
                                o1.Vendor = r2("vendor").ToString
                                'see if this option matches the part

                                Dim replacement As String = ""
                                Dim expression As New Regex("\(.*\)")
                                Dim result As String = expression.Replace(p1.PartNo, replacement)

                                o1.Matched = IIf(o1.PartNo.Replace("-", "").Replace("*", "") = result.Replace("-", "").Replace("*", "") And r2("companyid").ToString = p1.Vendor, True, False)
                                lstOptions.Add(o1)
                            End While
                            p1.Options = lstOptions
                        End Using
                    End Using
                    lstSmallPartOptions.Add(p1)
                End While
            End Using
        End Using

        Dim js As New JavaScriptSerializer
        Return js.Serialize(lstSmallPartOptions)
    End Function


#Region "FED Ex Shipping Rates Methods"

    Public Class VendorShippingAddress
        Public Property VendorShippingID As Integer
        Public Property CompanyID As Integer
        Public Property VanityShippingName As String
        Public Property ShippingDirectionTypeID As Integer
        Public Property Address1 As String
        Public Property Address2 As String
        Public Property City As String
        Public Property State As String
        Public Property Zip As String
        Public Property ContactPerson As String
        Public Property BuildingLocation As Integer
        Public Property BuildingPartCode As Integer
        Public Property LocationDescription As String 'Suite, unit, floor, etc
        Public Property OfficeClosingTime As String
        Public Property ContactPersonPhone As String
        Public Property ContactPersonPhoneExt As String
        Public Property TimeStamp As DateTime
        Public Property Email As String
        Public Property Fax As String

        Public Property SpecialInstructions As String

        Public Sub New()
            Me.VendorShippingID = 0
            Me.CompanyID = 0
            Me.ShippingDirectionTypeID = 3
            Me.Address1 = String.Empty
            Me.Address2 = String.Empty
            Me.City = String.Empty
            Me.State = String.Empty
            Me.Zip = String.Empty
            Me.ContactPerson = String.Empty
            Me.BuildingLocation = 0
            Me.BuildingPartCode = 0
            Me.LocationDescription = String.Empty
            Me.OfficeClosingTime = New DateTime(1977, 8, 31, 18, 0, 0)
            Me.VanityShippingName = String.Empty
            Me.ContactPersonPhone = String.Empty
            Me.ContactPersonPhoneExt = String.Empty

            Me.Fax = String.Empty
            Me.Email = String.Empty
            Me.SpecialInstructions = String.Empty
        End Sub
    End Class
    Public Function UpdatePartWithDimension(ByVal height As String, ByVal weight As String, ByVal width As String, ByVal length As String, ByVal partID As Integer, ByVal client As String) As Boolean
        Dim heightParam As String = "@height"
        Dim widthParam As String = "@width"
        Dim lengthParam As String = "@length"
        Dim weightParam As String = "@weight"
        Dim partIDParam As String = "@partID"

        Dim sbUpdate As New System.Text.StringBuilder()
        sbUpdate.Append("update dbo.tblPartOrder ")
        sbUpdate.Append(" set Weight = " & weightParam & ", ")
        sbUpdate.Append(" Height=" & heightParam & ",")
        sbUpdate.Append(" Length=" & lengthParam & ", ")
        sbUpdate.Append(" Width=" & widthParam & " ")
        sbUpdate.Append(" where PartID = " & partIDParam)

        Try
            Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Using oCmd As New SqlCommand(sbUpdate.ToString(), oConn)


                    oCmd.CommandType = CommandType.Text

                    oCmd.Parameters.Add(New SqlParameter(weightParam, weight))
                    oCmd.Parameters.Add(New SqlParameter(heightParam, Convert.ToDecimal(height)))
                    oCmd.Parameters.Add(New SqlParameter(lengthParam, Convert.ToDecimal(length)))
                    oCmd.Parameters.Add(New SqlParameter(widthParam, Convert.ToDecimal(width)))
                    oCmd.Parameters.Add(New SqlParameter(partIDParam, partID))

                    oConn.Open()
                    oCmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function GetOrderPartsForShipping(ByVal orderid As String, ByVal client As String)

        Dim list As New List(Of PartOrder)
        Dim js As New JavaScriptSerializer

        Try
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = " SELECT ROW_NUMBER() OVER(ORDER BY dbo.tblPartOrder.PartID) AS Row, CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.PreviousPartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.PartNo, dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.Weight,  Length, Width, Height, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.Address1, dbo.tblPartOrder.City, dbo.tblPartOrder.State, dbo.tblPartOrder.Zip, dbo.tblPartOrder.Phone, dbo.tblPartOrder.ShipperTrack, (case when dbo.tblPartOrder.ShipperTrack is null or dbo.tblPartOrder.ShipperTrack = '' then 'none' else 'block' end) as 'ShipmentCreatedDisplayValue' FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE (dbo.tblPartOrder.Incorrect= 0 and dbo.tblPartOrder.Defect = 0) and dbo.tblPartOrder.OrderID = " & orderid & " order by dbo.tblPartOrder.ShipperTrack asc"

                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()

                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim p1 As New PartOrder
                        Dim objType As Type = p1.GetType()
                        Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                        For Each prop As System.Reflection.PropertyInfo In properties
                            Try
                                prop.SetValue(p1, r(prop.Name), Nothing)
                            Catch ex As Exception
                                Continue For
                            End Try
                        Next
                        list.Add(p1)
                    End While
                End Using
            End Using

            Return js.Serialize(list)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message.ToString())
            Return js.Serialize(errList)
        End Try

    End Function


    <WebMethod()>
    Public Function GetReturnOrderPartsForShipping(ByVal orderid As String, ByVal client As String)

        Dim list As New List(Of PartOrder)
        Dim js As New JavaScriptSerializer

        Try
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim strsql As String = " SELECT ROW_NUMBER() OVER(ORDER BY dbo.tblPartOrder.PartID) AS Row, CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.PreviousPartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.PartNo, dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.Weight,  Length, Width, Height, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.Address1, dbo.tblPartOrder.City, dbo.tblPartOrder.State, dbo.tblPartOrder.Zip, dbo.tblPartOrder.Phone, dbo.tblPartOrder.ShipperTrack, (case when dbo.tblPartOrder.ShipperTrack is null or dbo.tblPartOrder.ShipperTrack = '' then 'none' else 'block' end) as 'ShipmentCreatedDisplayValue' FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno "
                strsql = strsql + " WHERE (dbo.tblPartOrder.Core = 1 or dbo.tblPartOrder.Incorrect= 1 or dbo.tblPartOrder.Defect = 1) and  dbo.tblPartOrder.OrderID = " & orderid & " order by dbo.tblPartOrder.ShipperTrack asc"

                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()

                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim p1 As New PartOrder
                        Dim objType As Type = p1.GetType()
                        Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                        For Each prop As System.Reflection.PropertyInfo In properties
                            Try
                                prop.SetValue(p1, r(prop.Name), Nothing)
                            Catch ex As Exception
                                Continue For
                            End Try
                        Next
                        list.Add(p1)
                    End While
                End Using
            End Using

            Return js.Serialize(list)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message.ToString())
            Return js.Serialize(errList)
        End Try

    End Function


    Public Function GetOrderPartsByPartList(ByVal orderid As String, ByVal listOfParts As String, ByVal client As String) As List(Of PartOrder)
        Dim list As New List(Of PartOrder)


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            ' Dim strsql As String = " SELECT CustomerCompany.CompanyID as 'CustomerID', CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.ShipperTrack FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE dbo.tblPartOrder.OrderID = " & orderid & " and dbo.tblPartOrder.PartID in (" & listOfParts & ")"
            Dim sbSql As New System.Text.StringBuilder()
            sbSql.Append(" SELECT CustomerCompany.CompanyID as 'CustomerID', CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.ShipperTrack,")
            sbSql.Append(" tblPartOrder.Servicer, tblPartOrder.Address1, tblPartOrder.City, tblPartOrder.State, tblPartOrder.Zip, tblPartOrder.Phone, tblPartOrder.Contact ")
            sbSql.Append(" FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE dbo.tblPartOrder.OrderID = " & orderid & " and dbo.tblPartOrder.PartID in (" & listOfParts & ")")

            Dim sqlComm As New SqlCommand(sbSql.ToString(), conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New PartOrder
                    Dim objType As Type = p1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(p1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(p1)
                End While
            End Using
        End Using

        Return list
    End Function

    Public Function GetCompanyShippingDataByListOfIDs(ByVal listOfCompanyIDs As List(Of Integer), ByVal client As String) As List(Of Company)
        Dim list As New List(Of Company)
        Dim strCompanyList As String = listOfCompanyIDs.ToCommaDelimitedString()


        Dim sbSql As New System.Text.StringBuilder()
        Dim sbSelect As New System.Text.StringBuilder()
        Dim sbFrom As New System.Text.StringBuilder()
        Dim sbWhere As New System.Text.StringBuilder()

        sbSelect.Append("select VA.companyID,C.Company,VA.Address1,VA.Address2,VA.City,VA.State,VA.Zip,VA.contactPerson as contact,c.CustomerNo,c.Phone,c.Fax,C.Email,VSA.Vanity as VanityShippingName from tblVanityShippingAddress as VSA inner join tblVendorShippingAddress as VA on VSA.VendorshippingAddressID=VA.vendorshippingID inner join tblCompany as c on C.CompanyID=VA.CompanyID")
        sbWhere.Append(" where c.CompanyID in (" & strCompanyList & ")")

        sbSql.Append(sbSelect.ToString())
        sbSql.Append(sbFrom.ToString())
        sbSql.Append(sbWhere.ToString())


        Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Using oCmd As New SqlCommand(sbSql.ToString(), oConn)
                oCmd.CommandType = CommandType.Text

                oConn.Open()

                Using oReader As SqlDataReader = oCmd.ExecuteReader()
                    While oReader.Read
                        Dim company = New Company With {.CompanyID = oReader.SanitizeInteger("CompanyID"),
                                                            .Company = oReader.SanitizeString("Company"),
                                                            .CustomerNo = oReader.SanitizeString("CustomerNo"),
                                                           .Address1 = oReader.SanitizeString("Address1"),
                                                           .Address2 = oReader.SanitizeString("Address2"),
                                                           .City = oReader.SanitizeString("City"),
                                                           .State = oReader.SanitizeString("State"),
                                                           .Zip = oReader.SanitizeString("Zip"),
                                                           .Phone = oReader.SanitizeString("Phone"),
                                                           .Fax = oReader.SanitizeString("Fax"),
                                                        .Contact = oReader.SanitizeString("Contact"),
                                                        .Email = oReader.SanitizeString("Email"),
                                                        .VanityShippingName = oReader.SanitizeString("VanityShippingName")}
                        list.Add(company)
                    End While
                End Using

            End Using
        End Using

        Return list
    End Function

    <WebMethod()>
    Public Function GetsVendorShippingAddressByVendorShippingID(ByVal client As String, ByVal vendorShippingID As Integer) As VendorShippingAddress
        Dim js As New JavaScriptSerializer

        Try
            Dim sb As New System.Text.StringBuilder()
            Dim vendor As New VendorShippingAddress()

            Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                sb.Append(" Select top 1 (CASE WHEN (tblCompany.VanityShippingName is null or tblCompany.VanityShippingName = '') then tblCompany.[Company] else tblCompany.VanityShippingName END) as 'VanityShippingName',tblVendorShippingAddress.VendorShippingID,tblVendorShippingAddress.CompanyID, tblVendorShippingAddress.ShippingDirectionTypeID,")
                sb.Append(" tblVendorShippingAddress.Address1,tblVendorShippingAddress.Address2,tblVendorShippingAddress.City,tblVendorShippingAddress.State,tblVendorShippingAddress.Zip,")
                sb.Append(" tblVendorShippingAddress.ContactPerson,tblVendorShippingAddress.BuildingLocation,tblVendorShippingAddress.BuildingPartCode,tblVendorShippingAddress.LocationDescription, ")
                sb.Append(" tblVendorShippingAddress.OfficeClosingTime,tblCompany.Phone, tblCompany.Fax, tblCompany.Email ")
                sb.Append(" from tblVendorShippingAddress ")
                sb.Append(" inner join tblCompany on tblVendorShippingAddress.CompanyID = tblCompany.CompanyID ")
                sb.Append(" where tblVendorShippingAddress.VendorShippingID = " & vendorShippingID.ToString())
                sb.Append(" order by VanityShippingName asc ")

                Using oCmd As New SqlCommand(sb.ToString(), oConn)
                    oCmd.CommandType = CommandType.Text
                    oConn.Open()

                    Using r As SqlDataReader = oCmd.ExecuteReader()
                        While r.Read()
                            vendor.VendorShippingID = r.SanitizeInteger("VendorShippingID")
                            vendor.CompanyID = r.SanitizeInteger("CompanyID")
                            vendor.ShippingDirectionTypeID = r.SanitizeInteger("ShippingDirectionTypeID")
                            vendor.Address1 = r.SanitizeString("Address1")
                            vendor.Address2 = r.SanitizeString("Address2")
                            vendor.City = r.SanitizeString("City")
                            vendor.State = r.SanitizeString("State")
                            vendor.Zip = r.SanitizeString("Zip")
                            vendor.ContactPerson = r.SanitizeString("ContactPerson")
                            vendor.BuildingLocation = r.SanitizeInteger("BuildingLocation")
                            vendor.BuildingPartCode = r.SanitizeInteger("BuildingPartCode")
                            vendor.LocationDescription = r.SanitizeString("LocationDescription")
                            vendor.OfficeClosingTime = r.SanitizeShortDateTime("OfficeClosingTime")
                            vendor.VanityShippingName = r.SanitizeString("VanityShippingName")
                            vendor.ContactPersonPhone = r.SanitizeString("Phone")
                            vendor.Email = r.SanitizeString("Email")
                            vendor.Fax = r.SanitizeString("Fax")
                        End While
                    End Using
                End Using
            End Using
            Return vendor
        Catch Ex As Exception
            Return Nothing
        End Try

    End Function

    <WebMethod()>
    Public Function GetsVendorShippingAddressByVendorID(ByVal client As String, ByVal vendorID As Integer) As VendorShippingAddress
        Dim js As New JavaScriptSerializer

        Try
            Dim sb As New System.Text.StringBuilder()

            sb.Append(" Select top 1 (CASE WHEN (tblCompany.VanityShippingName is null or tblCompany.VanityShippingName = '') then tblCompany.[Company] else tblCompany.VanityShippingName END) as 'VanityShippingName',tblVendorShippingAddress.VendorShippingID,tblVendorShippingAddress.CompanyID, tblVendorShippingAddress.ShippingDirectionTypeID,")
            sb.Append(" tblVendorShippingAddress.Address1,tblVendorShippingAddress.Address2,tblVendorShippingAddress.City,tblVendorShippingAddress.State,tblVendorShippingAddress.Zip,")
            sb.Append(" tblVendorShippingAddress.ContactPerson,tblVendorShippingAddress.BuildingLocation,tblVendorShippingAddress.BuildingPartCode,tblVendorShippingAddress.LocationDescription, ")
            sb.Append(" tblVendorShippingAddress.OfficeClosingTime,tblCompany.Phone, tblCompany.Fax, tblCompany.Email ")
            sb.Append(" from tblVendorShippingAddress ")
            sb.Append(" inner join tblCompany on tblVendorShippingAddress.CompanyID = tblCompany.CompanyID ")
            sb.Append(" where tblVendorShippingAddress.CompanyID = " & vendorID.ToString())
            sb.Append(" order by VanityShippingName asc ")

            Dim vendor As New VendorShippingAddress()

            Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Using oCmd As New SqlCommand(sb.ToString(), oConn)
                    oCmd.CommandType = CommandType.Text

                    oConn.Open()

                    Using r As SqlDataReader = oCmd.ExecuteReader()
                        While r.Read()
                            vendor.VendorShippingID = r.SanitizeInteger("VendorShippingID")
                            vendor.CompanyID = r.SanitizeInteger("CompanyID")
                            vendor.ShippingDirectionTypeID = r.SanitizeInteger("ShippingDirectionTypeID")
                            vendor.Address1 = r.SanitizeString("Address1")
                            vendor.Address2 = r.SanitizeString("Address2")
                            vendor.City = r.SanitizeString("City")
                            vendor.State = r.SanitizeString("State")
                            vendor.Zip = r.SanitizeString("Zip")
                            vendor.ContactPerson = r.SanitizeString("ContactPerson")
                            vendor.BuildingLocation = r.SanitizeInteger("BuildingLocation")
                            vendor.BuildingPartCode = r.SanitizeInteger("BuildingPartCode")
                            vendor.LocationDescription = r.SanitizeString("LocationDescription")
                            vendor.OfficeClosingTime = r.SanitizeShortDateTime("OfficeClosingTime")
                            vendor.VanityShippingName = r.SanitizeString("VanityShippingName")
                            vendor.ContactPersonPhone = r.SanitizeString("Phone")
                            vendor.Email = r.SanitizeString("Email")
                            vendor.Fax = r.SanitizeString("Fax")
                        End While
                    End Using
                End Using
            End Using
            Return vendor
        Catch Ex As Exception
            Return Nothing
        End Try

    End Function


    <WebMethod()>
    Public Function GetShippingRates(ByVal orderID As String, ByVal client As String, ByVal listOfParts As String, ByVal height As String, ByVal weight As String, ByVal width As String, ByVal length As String, ByVal dropOffTypeID As String, ByVal packageTypeID As String, ByVal vendorShippingID As Integer, ByVal scheduledDateTime As String)

        Try
            Dim js As New JavaScriptSerializer()

            'delete all files in the directory ...
            DeteleAllFilesInFedExPDFDirectory()

            If listOfParts.IsNullOrEmpty() Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Data missing.")
                Return js.Serialize(errList)
            End If

            Dim selectedParts As New List(Of PartOrder)
            selectedParts = GetOrderPartsByPartList(orderID, listOfParts, client)

            If selectedParts Is Nothing OrElse selectedParts.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts missing.")
                Return js.Serialize(errList)
            End If

            'Got part data .... grab vendor and shop data 
            'these should all be going to the same place, verify 
            Dim vendorID As String = selectedParts(0).VendorID
            Dim shop As New Company()
            Dim vendor As New Company()
            Dim cont As Boolean = True

            For Each po As PartOrder In selectedParts
                If Not po.VendorID = vendorID Then
                    cont = False
                    Exit For
                End If
            Next

            If cont = False Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts Selected Have Different Vendors.")
                Return js.Serialize(errList)
            End If

            shop.Company = selectedParts(0).Servicer
            shop.VanityShippingName = selectedParts(0).Servicer
            shop.Address1 = selectedParts(0).Address1
            shop.City = selectedParts(0).City
            shop.State = selectedParts(0).State
            shop.Zip = selectedParts(0).Zip
            shop.Phone = selectedParts(0).Phone
            shop.Contact = selectedParts(0).Contact

            'got the customer and vendor id ...grab the company data
            Dim companyIDs As New List(Of Integer)
            companyIDs.Add(vendorID)

            Dim companies As New List(Of Company)
            companies = GetCompanyShippingDataByListOfIDs(companyIDs, client)

            If companies Is Nothing OrElse companies.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Company data missing.")
                Return js.Serialize(errList)
            End If

            vendor = companies(0)


            Dim sbErr As New StringBuilder()
            Dim shippingRates As New List(Of FedExShippingRate)
            Dim dropOffType As RateWebServiceClient.RateWebReference.DropoffType
            Dim packageType As RateWebServiceClient.RateWebReference.PackagingType
            Dim fedEx As New FedExRateHelper()

            dropOffType = DirectCast(Convert.ToInt32(dropOffTypeID), RateWebServiceClient.RateWebReference.DropoffType)
            packageType = DirectCast(Convert.ToInt32(packageTypeID), RateWebServiceClient.RateWebReference.PackagingType)

            'Gets parts based on the ids provided
            Dim listOfServiceTypes As New List(Of RateWebServiceClient.RateWebReference.ServiceType)
            listOfServiceTypes.Add(ServiceType.PRIORITY_OVERNIGHT)
            listOfServiceTypes.Add(ServiceType.FEDEX_2_DAY)
            listOfServiceTypes.Add(ServiceType.FEDEX_EXPRESS_SAVER)
            listOfServiceTypes.Add(ServiceType.FIRST_OVERNIGHT)
            listOfServiceTypes.Add(ServiceType.STANDARD_OVERNIGHT)

            If packageType = PackagingType.YOUR_PACKAGING Then
                listOfServiceTypes.Add(ServiceType.FEDEX_GROUND)
            End If

            fedEx.OrderID = orderID

            fedEx.SetDestinationAddress(shop.Address1, shop.City, shop.State, shop.Zip, "US")
            fedEx.SetShipperAddress(vendor.Address1, vendor.City, vendor.State, vendor.Zip, "US")

            If dropOffType = RateWebServiceClient.RateWebReference.DropoffType.REQUEST_COURIER Then
                'update the origin accordinly
                If vendorShippingID = 0 Then
                    'something has gone wrong ... fail it
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Required Scheduled Shipment Data.")
                    Return js.Serialize(errList)
                End If
                Dim vendorShippingAddress As New VendorShippingAddress()
                vendorShippingAddress = GetsVendorShippingAddressByVendorShippingID(client, vendorShippingID)

                ''Make sure date is in proper format
                'Dim newTimeStamp As DateTime
                'If (DateTime.TryParse(scheduledDateTime, newTimeStamp) = False) Then
                '    Dim errList As New List(Of String)
                '    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Improper Date Time in the Scheduled Information.")
                '    Return js.Serialize(errList)
                'End If
                'vendorShippingAddress.TimeStamp = newTimeStamp
                fedEx.SetOrigin(vendorShippingAddress.VanityShippingName, String.Empty, String.Empty, vendorShippingAddress.ContactPerson, vendorShippingAddress.ContactPersonPhone, vendorShippingAddress.ContactPersonPhoneExt, vendorShippingAddress.Address1, vendorShippingAddress.City, vendorShippingAddress.State, vendorShippingAddress.Zip, "US")
            End If

            If height.IsNullOrEmpty() OrElse length.IsNullOrEmpty() Then
                fedEx.AddPackageLineItem(Convert.ToDecimal(weight), RateWebServiceClient.RateWebReference.WeightUnits.LB)
            Else
                fedEx.AddPackageLineItem(Convert.ToDecimal(weight), RateWebServiceClient.RateWebReference.WeightUnits.LB, length, width, height, RateWebServiceClient.RateWebReference.LinearUnits.IN)
            End If

            For Each st As ServiceType In listOfServiceTypes
                Dim rate As New FedExShippingRate
                rate.ServiceTypeID = Convert.ToInt32(st)
                rate.ServiceType = [Enum].GetName(GetType(RateWebServiceClient.RateWebReference.ServiceType), st)
                rate.DropOffType = [Enum].GetName(GetType(RateWebServiceClient.RateWebReference.DropoffType), dropOffType)
                rate.DropOffTypeID = Convert.ToInt32(dropOffType)
                rate.PackageTypeID = Convert.ToInt32(packageTypeID)


                fedEx.SetShipmentDetailData(dropOffType, st, packageType)

                Dim fedExRateReply As RateReply

                Try
                    fedExRateReply = fedEx.GetRateReply()
                    If fedExRateReply.HighestSeverity = NotificationSeverityType.ERROR OrElse fedExRateReply.HighestSeverity = NotificationSeverityType.FAILURE Then
                        'something went wrong
                        Continue For
                    End If
                    'If fedEx.ErrorMsg.IsNullOrEmpty() = False Then
                    '    'found error...fail out process and send error message notification
                    '    'Dim errList As New List(Of String)
                    '    'errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & fedEx.ErrorMsg)
                    '    'Return js.Serialize(errList) 'BaseApplicationVariables.JSErrorMessageKeyword & "=" & fedEx.ErrorMsg
                    '    Continue For
                    'End If
                Catch Ex As Exception
                    'Dim errList As New List(Of String)
                    'errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & ex.Message)
                    'Return js.Serialize(errList)
                    Continue For
                End Try

                If fedExRateReply Is Nothing Then
                    rate.Rate = "0"
                Else
                    Dim shipmentRateDetail As ShipmentRateDetail = fedEx.GetShipmentRateDetail(fedExRateReply)

                    If shipmentRateDetail Is Nothing Then
                        rate.Rate = "0"
                    End If

                    'all good, add the value to the text
                    If dropOffType = RateWebServiceClient.RateWebReference.DropoffType.REQUEST_COURIER And st = ServiceType.FEDEX_GROUND Then
                        rate.Rate = (shipmentRateDetail.TotalNetCharge.Amount + Convert.ToDecimal(4)).ToString("C")  '4 represents
                    Else
                        rate.Rate = shipmentRateDetail.TotalNetCharge.Amount.ToString("C")
                    End If

                    rate.DeliveryTimestamp = IIf(fedExRateReply.RateReplyDetails(0).DeliveryTimestampSpecified = True, fedExRateReply.RateReplyDetails(0).DeliveryTimestamp.ToShortDateString(), "")
                    rate.TransitTime = IIf(fedExRateReply.RateReplyDetails(0).TransitTimeSpecified = True, fedExRateReply.RateReplyDetails(0).TransitTime.ToString(), "")
                    shippingRates.Add(rate)
                End If
            Next

            If shippingRates Is Nothing AndAlso shippingRates.Count = 0 Then
                Return String.Empty
            End If

            'got data ...serialize it and send it on
            Return js.Serialize(shippingRates)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            Dim jsErr As New JavaScriptSerializer()
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message)
            Return jsErr.Serialize(errList)
        End Try
    End Function

    <WebMethod()>
    Public Function GetReturnShippingRates(ByVal orderID As String, ByVal client As String, ByVal listOfParts As String, ByVal height As String, ByVal weight As String, ByVal width As String, ByVal length As String, ByVal dropOffTypeID As String, ByVal packageTypeID As String, ByVal scheduledPickupDataCollection As String)
        'FROM Servicer(shop) to vendor
        Try
            Dim js As New JavaScriptSerializer()

            'delete all files in the directory ...
            DeteleAllFilesInFedExPDFDirectory()

            If listOfParts.IsNullOrEmpty() Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Data missing.")
                Return js.Serialize(errList)
            End If

            Dim selectedParts As New List(Of PartOrder)
            selectedParts = GetOrderPartsByPartList(orderID, listOfParts, client)

            If selectedParts Is Nothing OrElse selectedParts.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts missing.")
                Return js.Serialize(errList)
            End If

            Dim cont As Boolean = True

            For Each po As PartOrder In selectedParts
                If Not po.VendorID = selectedParts(0).VendorID Then
                    cont = False
                    Exit For
                End If
            Next

            If cont = False Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts Selected Have Different Vendors.")
                Return js.Serialize(errList)
            End If

            'Got part data .... grab vendor and customer data 
            'these should all be going to the same place, verify 
            Dim shop As New Company()
            shop.Company = selectedParts(0).Servicer
            shop.VanityShippingName = selectedParts(0).Servicer
            shop.Address1 = selectedParts(0).Address1
            shop.City = selectedParts(0).City
            shop.State = selectedParts(0).State
            shop.Zip = selectedParts(0).Zip
            shop.Phone = selectedParts(0).Phone
            shop.Contact = selectedParts(0).Contact

            Dim dropOffType As RateWebServiceClient.RateWebReference.DropoffType
            Dim packageType As RateWebServiceClient.RateWebReference.PackagingType
            Dim vendor As New Company()

            dropOffType = DirectCast(Convert.ToInt32(dropOffTypeID), RateWebServiceClient.RateWebReference.DropoffType)
            packageType = DirectCast(Convert.ToInt32(packageTypeID), RateWebServiceClient.RateWebReference.PackagingType)


            'Get Vendor
            Dim vendorID As String = String.Empty

            If dropOffType = RateWebServiceClient.RateWebReference.DropoffType.REQUEST_COURIER Then
                If scheduledPickupDataCollection.IsNullOrEmpty() Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Scheduled Pickup Data.")
                    Return js.Serialize(errList)
                End If

                vendorID = scheduledPickupDataCollection.GetValueByName("VSID", Convert.ToChar("_"))
                If vendorID.IsNullOrEmpty() Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Scheduled Pickup Data.")
                    Return js.Serialize(errList)
                End If

                'so far so good
                Dim vendorShippingAddress As New VendorShippingAddress()
                vendorShippingAddress = GetsVendorShippingAddressByVendorShippingID(client, vendorID)
                If vendorShippingAddress Is Nothing Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "An Error Was Encountered While Retrieving the Shipping Address Object.")
                    Return js.Serialize(errList)
                End If

                vendor.Company = vendorShippingAddress.VanityShippingName
                vendor.VanityShippingName = vendorShippingAddress.VanityShippingName
                vendor.Address1 = vendorShippingAddress.Address1
                vendor.City = vendorShippingAddress.City
                vendor.State = vendorShippingAddress.State
                vendor.Zip = vendorShippingAddress.Zip
                vendor.Phone = vendorShippingAddress.ContactPersonPhone
                vendor.Contact = vendorShippingAddress.ContactPerson
            Else
                Dim companyIDs As New List(Of Integer)
                Dim companies As New List(Of Company)

                companyIDs.Add(selectedParts(0).VendorID)
                companies = GetCompanyShippingDataByListOfIDs(companyIDs, client)

                If companies Is Nothing OrElse companies.Count = 0 Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Company data missing.")
                    Return js.Serialize(errList)
                End If

                vendor = companies(0)
            End If

            Dim sbErr As New StringBuilder()
            Dim shippingRates As New List(Of FedExShippingRate)
            Dim fedEx As New FedExRateHelper()

            'Gets parts based on the ids provided
            Dim listOfServiceTypes As New List(Of RateWebServiceClient.RateWebReference.ServiceType)
            listOfServiceTypes.Add(ServiceType.PRIORITY_OVERNIGHT)
            listOfServiceTypes.Add(ServiceType.FEDEX_2_DAY)
            listOfServiceTypes.Add(ServiceType.FEDEX_EXPRESS_SAVER)
            listOfServiceTypes.Add(ServiceType.FIRST_OVERNIGHT)
            listOfServiceTypes.Add(ServiceType.STANDARD_OVERNIGHT)

            If packageType = PackagingType.YOUR_PACKAGING Then
                listOfServiceTypes.Add(ServiceType.FEDEX_GROUND)
            End If

            fedEx.OrderID = orderID

            fedEx.SetShipperAddress(shop.Address1, shop.City, shop.State, shop.Zip, "US")
            fedEx.SetDestinationAddress(vendor.Address1, vendor.City, vendor.State, vendor.Zip, "US")



            If height.IsNullOrEmpty() OrElse length.IsNullOrEmpty() Then
                fedEx.AddPackageLineItem(Convert.ToDecimal(weight), RateWebServiceClient.RateWebReference.WeightUnits.LB)
            Else
                fedEx.AddPackageLineItem(Convert.ToDecimal(weight), RateWebServiceClient.RateWebReference.WeightUnits.LB, length, width, height, RateWebServiceClient.RateWebReference.LinearUnits.IN)
            End If

            For Each st As ServiceType In listOfServiceTypes
                Dim rate As New FedExShippingRate
                rate.ServiceTypeID = Convert.ToInt32(st)
                rate.ServiceType = [Enum].GetName(GetType(RateWebServiceClient.RateWebReference.ServiceType), st)
                rate.DropOffType = [Enum].GetName(GetType(RateWebServiceClient.RateWebReference.DropoffType), dropOffType)
                rate.DropOffTypeID = Convert.ToInt32(dropOffType)
                rate.PackageTypeID = Convert.ToInt32(packageTypeID)

                fedEx.SetShipmentDetailData(dropOffType, st, packageType)

                Dim fedExRateReply As RateReply

                Try
                    fedExRateReply = fedEx.GetRateReply()

                    If fedExRateReply.HighestSeverity = NotificationSeverityType.ERROR OrElse fedExRateReply.HighestSeverity = NotificationSeverityType.FAILURE Then
                        'something went wrong
                        Continue For
                    End If
                Catch Ex As Exception
                    'Dim errList As New List(Of String)
                    'errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & ex.Message)
                    'Return js.Serialize(errList)
                    Continue For
                End Try

                If fedExRateReply Is Nothing Then
                    rate.Rate = "0"
                Else
                    Dim shipmentRateDetail As ShipmentRateDetail = fedEx.GetShipmentRateDetail(fedExRateReply)

                    If shipmentRateDetail Is Nothing Then
                        rate.Rate = "0"
                    End If

                    'all good, add the value to the text
                    rate.Rate = shipmentRateDetail.TotalNetCharge.Amount.ToString("C")
                    rate.DeliveryTimestamp = IIf(fedExRateReply.RateReplyDetails(0).DeliveryTimestampSpecified = True, fedExRateReply.RateReplyDetails(0).DeliveryTimestamp.ToShortDateString(), "")
                    rate.TransitTime = IIf(fedExRateReply.RateReplyDetails(0).TransitTimeSpecified = True, fedExRateReply.RateReplyDetails(0).TransitTime.ToString(), "")
                    shippingRates.Add(rate)
                End If
            Next

            If shippingRates Is Nothing AndAlso shippingRates.Count = 0 Then
                Return String.Empty
            End If

            'got data ...serialize it and send it on
            Return js.Serialize(shippingRates)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            Dim jsErr As New JavaScriptSerializer()
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message)
            Return jsErr.Serialize(errList)
        End Try
    End Function

    <WebMethod()>
    Public Function CreateShipment(ByVal orderID As String, ByVal client As String, ByVal listOfParts As String, ByVal height As String, ByVal weight As String, ByVal width As String, ByVal length As String, ByVal dropOffTypeID As String, ByVal packageTypeID As String, ByVal serviceTypeID As Integer, ByVal vendorShippingID As Integer, ByVal scheduledDateTime As String) As String
        Dim js As New JavaScriptSerializer()
        Dim fedExShipper As New FedExGroundShippingHelper()
        Dim vendorShippingAddress As New VendorShippingAddress()

        Try
            If listOfParts.IsNullOrEmpty() Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts List Missing.")
                Return js.Serialize(errList)
            End If

            Dim selectedParts As New List(Of PartOrder)
            selectedParts = GetOrderPartsByPartList(orderID, listOfParts, client)

            If selectedParts Is Nothing OrElse selectedParts.Count = 0 Then
                Return String.Empty
            End If

            If selectedParts.Count = 1 Then
                'update database with dimensions of the one part
                UpdatePartWithDimension(height, weight, width, length, selectedParts(0).PartID, client)
            End If

            'Got part data .... grab vendor and customer data 
            'these should all be going to the same place, verify 
            Dim vendorID As String = selectedParts(0).VendorID
            Dim shop As New Company()
            Dim vendor As New Company()

            Dim cont As Boolean = True

            For Each po As PartOrder In selectedParts
                If Not po.VendorID = vendorID Then
                    cont = False
                    Exit For
                End If
            Next

            If cont = False Then
                Return String.Empty
            End If

            'got the customer and vendor id ...grab the company data
            shop.Company = selectedParts(0).Servicer
            shop.VanityShippingName = selectedParts(0).Servicer
            shop.Address1 = selectedParts(0).Address1
            shop.City = selectedParts(0).City
            shop.State = selectedParts(0).State
            shop.Zip = selectedParts(0).Zip
            shop.Phone = selectedParts(0).Phone
            shop.Contact = selectedParts(0).Contact

            Dim companyIDs As New List(Of Integer)
            companyIDs.Add(vendorID)

            Dim companies As New List(Of Company)
            companies = GetCompanyShippingDataByListOfIDs(companyIDs, client)

            vendor = companies(0)

            If companies Is Nothing OrElse companies.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Could not find the origin and destination companies.")
                Return js.Serialize(errList)
            End If

            'got data necessary to create shipment
            Dim dropOffType As ShipWebServiceClient.ShipServiceWebReference.DropoffType
            Dim serviceType As ShipWebServiceClient.ShipServiceWebReference.ServiceType
            Dim packageType As ShipWebServiceClient.ShipServiceWebReference.PackagingType

            dropOffType = DirectCast(Convert.ToInt32(dropOffTypeID), ShipWebServiceClient.ShipServiceWebReference.DropoffType)
            serviceType = DirectCast(serviceTypeID, ShipWebServiceClient.ShipServiceWebReference.ServiceType)
            packageType = DirectCast(Convert.ToInt32(packageTypeID), ShipWebServiceClient.ShipServiceWebReference.PackagingType)


            fedExShipper.OrderID = orderID
            fedExShipper.SetShipmentDetailData(dropOffType, serviceType, packageType, False)

            fedExShipper.SetDestinationAddress(shop.Address1, shop.City, shop.State, shop.Zip, "US")
            fedExShipper.SetShipperAddress(vendor.Address1, vendor.City, vendor.State, vendor.Zip, "US")

            fedExShipper.SetDestinationContact(shop.Contact, IIf(shop.VanityShippingName.IsNullOrEmpty(), shop.Company, shop.VanityShippingName), shop.Phone)
            fedExShipper.SetShipperContact(vendor.Contact, IIf(vendor.VanityShippingName.IsNullOrEmpty(), vendor.Company, vendor.VanityShippingName), vendor.Phone)

            If dropOffType = ShipWebServiceClient.ShipServiceWebReference.DropoffType.REQUEST_COURIER Then

                If vendorShippingID = 0 Then
                    'something has gone wrong ... fail it
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Required Scheduled Shipment Data.")
                    Return js.Serialize(errList)
                End If

                vendorShippingAddress = GetsVendorShippingAddressByVendorShippingID(client, vendorShippingID)

                'Make sure date is in proper format
                Dim newTimeStamp As DateTime
                If (DateTime.TryParse(scheduledDateTime, newTimeStamp) = False) Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Improper Date Time in the Scheduled Information.")
                    Return js.Serialize(errList)
                End If
                vendorShippingAddress.TimeStamp = newTimeStamp

                fedExShipper.SetOriginAddress(vendorShippingAddress.Address1, vendorShippingAddress.City, vendorShippingAddress.State, vendorShippingAddress.Zip, "US")
                fedExShipper.SetOriginContact(vendorShippingAddress.VanityShippingName, vendorShippingAddress.Email, vendorShippingAddress.Fax, vendorShippingAddress.ContactPerson, vendorShippingAddress.ContactPersonPhone, vendorShippingAddress.ContactPersonPhoneExt)
            End If

            Dim custRefs As New List(Of ShipWebServiceClient.ShipServiceWebReference.CustomerReference)()
            Dim custRef1 As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()

            custRef1.Value = orderID
            custRef1.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.CUSTOMER_REFERENCE
            custRefs.Add(custRef1)

            fedExShipper.AddPackageLineItem(Convert.ToDecimal(weight), ShipWebServiceClient.ShipServiceWebReference.WeightUnits.LB, length, width, height, ShipWebServiceClient.ShipServiceWebReference.LinearUnits.IN, custRefs)

            Dim reply As New ShipWebServiceClient.ShipServiceWebReference.ProcessShipmentReply()
            Dim listOfPackageDetails As New List(Of FedExPackageDetail)


            reply = fedExShipper.SendShipmentRequest()
            'do we have an error in the shipper
            If fedExShipper.ErrorMsg.IsNullOrEmpty() = False Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & fedExShipper.ErrorMsg)
                Return js.Serialize(errList) 'Return BaseApplicationVariables.JSErrorMessageKeyword & "=" & fedExShipper.ErrorMsg
            End If

            listOfPackageDetails = fedExShipper.GetPackageDetails(reply)

            If listOfPackageDetails Is Nothing OrElse listOfPackageDetails.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Package detail information is missing.")
                Return js.Serialize(errList)
            End If

            If dropOffType = ShipWebServiceClient.ShipServiceWebReference.DropoffType.REQUEST_COURIER Then
                ' pickup needs to be initiated
                Dim fedExPickupRequestReply As New CreatePickupWebServiceClient.CreatePickupWebReference.CreatePickupReply()
                fedExPickupRequestReply = InitiateScheduledFedExPickup(weight, vendorShippingAddress, orderID, client)
                If fedExPickupRequestReply.HighestSeverity = CreatePickupWebServiceClient.CreatePickupWebReference.NotificationSeverityType.ERROR OrElse fedExPickupRequestReply.HighestSeverity = CreatePickupWebServiceClient.CreatePickupWebReference.NotificationSeverityType.FAILURE Then
                    'problem ... destroy shipment and send error notice
                    DeleteShipmentByTrackingNumber(listOfPackageDetails(0).TrackingNumber, client)
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & FedExPickupHelper.GetErrorNotification(fedExPickupRequestReply))
                    Return js.Serialize(errList)
                End If

                If fedExPickupRequestReply.PickupConfirmationNumber.IsNullOrEmpty() Then
                    'problem ... destroy shipment and send error notice
                    DeleteShipmentByTrackingNumber(listOfPackageDetails(0).TrackingNumber, client)
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Pickup Confirmation Number Missing.")
                    Return js.Serialize(errList)
                End If

                'all good ... add pickup confirmation number to returned values
                listOfPackageDetails(0).PickupConfirmationNumber = fedExPickupRequestReply.PickupConfirmationNumber
            End If

            Dim strServiceType As String
            strServiceType = [Enum].GetName(GetType(ShipWebServiceClient.ShipServiceWebReference.ServiceType), serviceType)

            For Each pd In listOfPackageDetails
                pd.ServiceTypeID = serviceTypeID
                pd.ServiceType = strServiceType
                pd.PartsList = listOfParts
            Next pd

            Dim fedExPackageDetail As New FedExPackageDetail()
            fedExPackageDetail = listOfPackageDetails(0)

            Dim trackDetailErrMsg As String = Me.SaveTrackingDetails(fedExPackageDetail, listOfParts, strServiceType, client)
            If trackDetailErrMsg.IsNullOrEmpty() = False Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & trackDetailErrMsg)
                Return js.Serialize(errList)
            End If

            Return js.Serialize(listOfPackageDetails)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message.ToString())
            Return js.Serialize(errList)
        End Try
    End Function

    <WebMethod()>
    Public Function CreateReturnShipment(ByVal orderID As String, ByVal client As String, ByVal listOfParts As String, ByVal height As String, ByVal weight As String, ByVal width As String, ByVal length As String, ByVal dropOffTypeID As String, ByVal packageTypeID As String, ByVal serviceTypeID As Integer, ByVal scheduledPickupDataCollection As String) As String
        Dim js As New JavaScriptSerializer()
        Dim fedExShipper As New FedExGroundShippingHelper()
        Dim vendorShippingAddress As New VendorShippingAddress()

        Try
            If listOfParts.IsNullOrEmpty() Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts List Missing.")
                Return js.Serialize(errList)
            End If

            Dim selectedParts As New List(Of PartOrder)

            selectedParts = GetOrderPartsByPartList(orderID, listOfParts, client)

            If selectedParts Is Nothing OrElse selectedParts.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Parts List Missing.")
                Return js.Serialize(errList)
            End If


            If selectedParts.Count = 1 Then
                'update database with dimensions of the one part
                UpdatePartWithDimension(height, weight, width, length, selectedParts(0).PartID, client)
            End If

            'Got part data .... grab vendor and customer data 
            'these should all be going to the same place, verify 
            Dim cont As Boolean = True

            For Each po As PartOrder In selectedParts
                If Not po.VendorID = selectedParts(0).VendorID Then
                    cont = False
                    Exit For
                End If
            Next

            If cont = False Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Some of the Parts Contain Different Vendors.")
                Return js.Serialize(errList)
            End If

            'got the customer and vendor id ...grab the company data
            Dim shop As New Company()
            shop.Company = selectedParts(0).Servicer
            shop.VanityShippingName = selectedParts(0).Servicer
            shop.Address1 = selectedParts(0).Address1
            shop.City = selectedParts(0).City
            shop.State = selectedParts(0).State
            shop.Zip = selectedParts(0).Zip
            shop.Phone = selectedParts(0).Phone
            shop.Contact = selectedParts(0).Contact

            'got data necessary to create shipment
            Dim dropOffType As ShipWebServiceClient.ShipServiceWebReference.DropoffType
            Dim serviceType As ShipWebServiceClient.ShipServiceWebReference.ServiceType
            Dim packageType As ShipWebServiceClient.ShipServiceWebReference.PackagingType

            dropOffType = DirectCast(Convert.ToInt32(dropOffTypeID), ShipWebServiceClient.ShipServiceWebReference.DropoffType)
            serviceType = DirectCast(serviceTypeID, ShipWebServiceClient.ShipServiceWebReference.ServiceType)
            packageType = DirectCast(Convert.ToInt32(packageTypeID), ShipWebServiceClient.ShipServiceWebReference.PackagingType)


            fedExShipper.OrderID = orderID
            fedExShipper.SetShipmentDetailData(dropOffType, serviceType, packageType, False)

            fedExShipper.SetShipperAddress(shop.Address1, shop.City, shop.State, shop.Zip, "US")
            fedExShipper.SetShipperContact(shop.Contact, IIf(shop.VanityShippingName.IsNullOrEmpty(), shop.Company, shop.VanityShippingName), shop.Phone)

            Dim custRefs As New List(Of ShipWebServiceClient.ShipServiceWebReference.CustomerReference)()
            Dim custRef1 As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()
            Dim custPONo As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()
            Dim custInv As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()
            Dim custDeptNo As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()


            If Not scheduledPickupDataCollection.IsNullOrEmpty() Then
                Dim refNumber = scheduledPickupDataCollection.GetValueByName("REFNO", Convert.ToChar("_"))
                Dim poNumber = scheduledPickupDataCollection.GetValueByName("PONO", Convert.ToChar("_"))
                Dim invNumber = scheduledPickupDataCollection.GetValueByName("INVNO", Convert.ToChar("_"))
                Dim deptNumber = scheduledPickupDataCollection.GetValueByName("DEPTNO", Convert.ToChar("_"))

                If Not refNumber.IsNullOrEmpty() Then
                    custRef1.Value = refNumber
                    custRef1.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.CUSTOMER_REFERENCE
                    custRefs.Add(custRef1)
                Else
                    custRef1.Value = orderID
                    custRef1.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.CUSTOMER_REFERENCE
                    custRefs.Add(custRef1)
                End If

                If Not poNumber.IsNullOrEmpty() Then
                    custPONo.Value = poNumber
                    custPONo.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.P_O_NUMBER
                    custRefs.Add(custPONo)
                End If

                If Not invNumber.IsNullOrEmpty() Then
                    custInv.Value = invNumber
                    custInv.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.INVOICE_NUMBER
                    custRefs.Add(custInv)
                End If

                If Not deptNumber.IsNullOrEmpty() Then
                    custDeptNo.Value = deptNumber
                    custDeptNo.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.DEPARTMENT_NUMBER
                    custRefs.Add(custDeptNo)
                End If
            Else
                'missing elements needed for the label...give the order id as default
                custRef1.Value = orderID
                custRef1.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.CUSTOMER_REFERENCE
                custRefs.Add(custRef1)
            End If

            Dim vendor As New Company()
            Dim vendorID As String = String.Empty
            Dim vendorShippingID As String = scheduledPickupDataCollection.GetValueByName("VSID", Convert.ToChar("_"))

            'missing vendor shipping id, it wasn't chosen ... grab default
            If String.IsNullOrEmpty(vendorShippingID) Then
                Dim companyIDs As New List(Of Integer)
                Dim companies As New List(Of Company)

                companyIDs.Add(selectedParts(0).VendorID)
                companies = GetCompanyShippingDataByListOfIDs(companyIDs, client)

                If companies Is Nothing OrElse companies.Count = 0 Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Company data missing.")
                    Return js.Serialize(errList)
                End If

                vendorShippingAddress = GetsVendorShippingAddressByVendorID(client, companies(0).CompanyID)
            Else
                'so far so good, make sure it's an integer
                Dim vendorShippingIDAsInteger As Integer
                If Integer.TryParse(vendorShippingID, vendorShippingIDAsInteger) Then
                    vendorShippingAddress = GetsVendorShippingAddressByVendorShippingID(client, vendorShippingIDAsInteger)
                Else
                    'couldn't find it, don't guess, fail it
                    vendorShippingAddress = New VendorShippingAddress()
                End If
            End If

            'do we have a valid shipping address
            If vendorShippingAddress Is Nothing OrElse vendorShippingAddress.VendorShippingID = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "An Error Was Encountered While Retrieving the Shipping Address Object.")
                Return js.Serialize(errList)
            End If

            'company should exist
            vendor.Company = vendorShippingAddress.VanityShippingName
            vendor.VanityShippingName = vendorShippingAddress.VanityShippingName
            vendor.Address1 = vendorShippingAddress.Address1
            vendor.City = vendorShippingAddress.City
            vendor.State = vendorShippingAddress.State
            vendor.Zip = vendorShippingAddress.Zip
            vendor.Phone = vendorShippingAddress.ContactPersonPhone
            vendor.Contact = vendorShippingAddress.ContactPerson


            fedExShipper.SetDestinationAddress(vendor.Address1, vendor.City, vendor.State, vendor.Zip, "US")
            fedExShipper.SetDestinationContact(vendor.Contact, IIf(vendor.VanityShippingName.IsNullOrEmpty(), vendor.Company, vendor.VanityShippingName), vendor.Phone)


            fedExShipper.AddPackageLineItem(Convert.ToDecimal(weight), ShipWebServiceClient.ShipServiceWebReference.WeightUnits.LB, length, width, height, ShipWebServiceClient.ShipServiceWebReference.LinearUnits.IN, custRefs)

            Dim reply As New ShipWebServiceClient.ShipServiceWebReference.ProcessShipmentReply()
            Dim listOfPackageDetails As New List(Of FedExPackageDetail)

            reply = fedExShipper.SendShipmentRequest()
            'do we have an error in the shipper
            If fedExShipper.ErrorMsg.IsNullOrEmpty() = False Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & fedExShipper.ErrorMsg)
                Return js.Serialize(errList) 'Return BaseApplicationVariables.JSErrorMessageKeyword & "=" & fedExShipper.ErrorMsg
            End If

            listOfPackageDetails = fedExShipper.GetPackageDetails(reply)

            If listOfPackageDetails Is Nothing OrElse listOfPackageDetails.Count = 0 Then
                Dim errList As New List(Of String)
                errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Package detail information is missing.")
                Return js.Serialize(errList)
            End If

            If dropOffType = ShipWebServiceClient.ShipServiceWebReference.DropoffType.REQUEST_COURIER Then
                ' pickup needs to be initiated
                Dim fedExPickupRequestReply As New CreatePickupWebServiceClient.CreatePickupWebReference.CreatePickupReply()
                'Create Shipping Address
                Dim shopShippingAddress As New VendorShippingAddress()
                shopShippingAddress.VanityShippingName = shop.VanityShippingName
                shopShippingAddress.Address1 = shop.Address1
                shopShippingAddress.City = shop.City
                shopShippingAddress.State = shop.State
                shopShippingAddress.Zip = shop.Zip
                shopShippingAddress.ContactPersonPhone = shop.Phone
                shopShippingAddress.ContactPerson = shop.Contact

                Dim newTimeStamp As DateTime
                Dim schDateTime As String = scheduledPickupDataCollection.GetValueByName("SDT", Convert.ToChar("_"))
                Dim buildingPartCode As String = scheduledPickupDataCollection.GetValueByName("BPC", Convert.ToChar("_"))
                Dim pickupBuildingType As String = scheduledPickupDataCollection.GetValueByName("PBT", Convert.ToChar("_"))
                Dim specInstr As String = scheduledPickupDataCollection.GetValueByName("SPINSTR", Convert.ToChar("_"))
                Dim shopClosingTime As String = scheduledPickupDataCollection.GetValueByName("SCT", Convert.ToChar("_"))

                If schDateTime.IsNullOrEmpty() Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Scheduled Pickup Date Time.")
                    Return js.Serialize(errList)
                End If

                If (DateTime.TryParse(schDateTime, newTimeStamp) = False) Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Scheduled Pickup Date Time or it's Format is Incorrect.")
                    Return js.Serialize(errList)
                End If
                shopShippingAddress.TimeStamp = newTimeStamp

                If buildingPartCode.IsNullOrEmpty() OrElse buildingPartCode.IsInt() = False Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Scheduling Data.")
                    Return js.Serialize(errList)
                End If
                shopShippingAddress.BuildingPartCode = Convert.ToInt32(buildingPartCode)

                If pickupBuildingType.IsNullOrEmpty() OrElse pickupBuildingType.IsInt() = False Then
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Missing Scheduling Data.")
                    Return js.Serialize(errList)
                End If
                shopShippingAddress.BuildingLocation = Convert.ToInt32(pickupBuildingType)

                If specInstr.IsNullOrEmpty() = False Then
                    shopShippingAddress.SpecialInstructions = specInstr
                End If

                shopShippingAddress.BuildingLocation = Convert.ToInt32(pickupBuildingType)

                If shopClosingTime.IsNullOrEmpty() Then
                    shopClosingTime = "5:00 PM"
                End If
                shopShippingAddress.OfficeClosingTime = shopClosingTime


                'all good
                fedExPickupRequestReply = InitiateScheduledFedExPickup(weight, shopShippingAddress, orderID, client)
                If fedExPickupRequestReply.HighestSeverity = CreatePickupWebServiceClient.CreatePickupWebReference.NotificationSeverityType.ERROR OrElse fedExPickupRequestReply.HighestSeverity = CreatePickupWebServiceClient.CreatePickupWebReference.NotificationSeverityType.FAILURE Then
                    'problem ... destroy shipment and send error notice
                    DeleteShipmentByTrackingNumber(listOfPackageDetails(0).TrackingNumber, client)
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & FedExPickupHelper.GetErrorNotification(fedExPickupRequestReply))
                    Return js.Serialize(errList)
                End If

                If fedExPickupRequestReply.PickupConfirmationNumber.IsNullOrEmpty() Then
                    'problem ... destroy shipment and send error notice
                    DeleteShipmentByTrackingNumber(listOfPackageDetails(0).TrackingNumber, client)
                    Dim errList As New List(Of String)
                    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & "Pickup Confirmation Number Missing.")
                    Return js.Serialize(errList)
                End If

                'all good ... add pickup confirmation number to returned values
                listOfPackageDetails(0).PickupConfirmationNumber = fedExPickupRequestReply.PickupConfirmationNumber
            End If

            Dim strServiceType As String
            strServiceType = [Enum].GetName(GetType(ShipWebServiceClient.ShipServiceWebReference.ServiceType), serviceType)

            For Each pd In listOfPackageDetails
                pd.ServiceTypeID = serviceTypeID
                pd.ServiceType = strServiceType
                pd.PartsList = listOfParts
            Next pd

            Dim fedExPackageDetail As New FedExPackageDetail()
            fedExPackageDetail = listOfPackageDetails(0)

            ' Dim trackDetailErrMsg As String = Me.SaveTrackingDetails(fedExPackageDetail, listOfParts, strServiceType, client)
            'If trackDetailErrMsg.IsNullOrEmpty() = False Then
            '    Dim errList As New List(Of String)
            '    errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & trackDetailErrMsg)
            '    Return js.Serialize(errList)
            'End If

            Return js.Serialize(listOfPackageDetails)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message.ToString())
            Return js.Serialize(errList)
        End Try
    End Function


    Private Function SaveTrackingDetails(ByVal fedExPackageDetails As FedExPackageDetail, ByVal listOfParts As String, ByVal serviceType As String, ByVal client As String) As String
        Dim trackNumParam As String = "@trackNumber"
        Dim shipStatusDetailParam As String = "@shipStatusDetail"
        Dim shipTypeParam As String = "@shipType"

        Dim sbUpdate As New System.Text.StringBuilder()
        sbUpdate.Append("update dbo.tblPartOrder ")
        sbUpdate.Append(" set ShipperTrack= " & trackNumParam & ", ")
        sbUpdate.Append(" ShipperStatusDetail=" & shipStatusDetailParam & ",")

        If Not fedExPackageDetails.DeliveryDate = Nothing Then
            sbUpdate.Append(" FreightETA = '" & fedExPackageDetails.DeliveryDate & "',")
            sbUpdate.Append(" ArrivalDate2 = '" & fedExPackageDetails.DeliveryDate & "',")
        End If

        sbUpdate.Append(" ShippingType=" & shipTypeParam & " ")
        sbUpdate.Append(" where PartID in (" & listOfParts & ")")

        Try

            Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Using oCmd As New SqlCommand(sbUpdate.ToString(), oConn)
                    oCmd.CommandType = CommandType.Text

                    oCmd.Parameters.Add(New SqlParameter(trackNumParam, fedExPackageDetails.TrackingNumber))
                    oCmd.Parameters.Add(New SqlParameter(shipStatusDetailParam, "Tracking Number")) '
                    oCmd.Parameters.Add(New SqlParameter(shipTypeParam, serviceType))

                    oConn.Open()

                    oCmd.ExecuteNonQuery()
                End Using
            End Using

            Return String.Empty
        Catch Ex As Exception
            Return Ex.Message.ToString()
        End Try

    End Function

    Public Function InitiateScheduledFedExPickup(ByVal weight As Decimal, ByVal shippingAddress As VendorShippingAddress, ByVal orderID As String, ByVal client As String) As CreatePickupWebServiceClient.CreatePickupWebReference.CreatePickupReply
        Dim returnMsg As String = String.Empty

        Try
            Dim fedExPickupHelper As New FedExPickupHelper()

            fedExPickupHelper.SetAddress(shippingAddress.Address1, shippingAddress.City, shippingAddress.State, shippingAddress.Zip, "US")
            fedExPickupHelper.SetContactPerson(shippingAddress.VanityShippingName, shippingAddress.Email, shippingAddress.Fax, shippingAddress.ContactPerson, shippingAddress.ContactPersonPhone, shippingAddress.ContactPersonPhoneExt)
            fedExPickupHelper.OrderID = orderID
            fedExPickupHelper.ReadyTimestamp = shippingAddress.TimeStamp
            fedExPickupHelper.SpecialInstruction = shippingAddress.SpecialInstructions
            '    If buildingLocationType.IsNullOrEmpty() = False AndAlso buildingLocationType.IsInt() Then
            fedExPickupHelper.PackageLocationType = DirectCast(Convert.ToInt32(shippingAddress.BuildingLocation), CreatePickupWebServiceClient.CreatePickupWebReference.PickupBuildingLocationType)
            '   End If

            '  If buildingPartCode.IsNullOrEmpty() = False AndAlso buildingPartCode.IsInt() Then
            fedExPickupHelper.BuildingPartCode = DirectCast(Convert.ToInt32(shippingAddress.BuildingPartCode), CreatePickupWebServiceClient.CreatePickupWebReference.BuildingPartCode)
            '   End If

            '  If Unit.IsNullOrEmpty() Then
            fedExPickupHelper.BuildingPartDescription = shippingAddress.LocationDescription
            '   End If

            fedExPickupHelper.Weight = weight


            Dim fedExPickupRequestReply As New CreatePickupWebServiceClient.CreatePickupWebReference.CreatePickupReply()
            fedExPickupRequestReply = fedExPickupHelper.CreatePickupRequestReply()

            Return fedExPickupRequestReply
        Catch Ex As Exception
            Return Nothing
        End Try
    End Function



    <WebMethod()>
    Public Function DeleteShipmentByTrackingNumber(ByVal trackingNumber As String, ByVal client As String) As String
        Dim returnMsg As String = String.Empty

        Try
            Dim fedExAdvServices As New FedExAdvancedServicesHelper(trackingNumber)

            If fedExAdvServices.DeleteShipment() = False Then
                returnMsg = fedExAdvServices.ErrorMsg
                Return returnMsg
            End If

            ' ''shipment deleted successfully
            Dim sbSql As New System.Text.StringBuilder()
            sbSql.Append(" update tblPartOrder ")
            sbSql.Append(" set ShipperTrack = null, ShipperStatusDetail = null, ShipperStatus = null ")
            sbSql.Append(" where ShipperTrack = '" & trackingNumber & "' ")

            Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Using oCmd As New SqlCommand(sbSql.ToString(), oConn)
                    oCmd.CommandType = CommandType.Text
                    oConn.Open()
                    oCmd.ExecuteNonQuery()
                End Using
            End Using

            Return returnMsg
        Catch Ex As Exception
            returnMsg = Ex.Message.ToString()
            Return returnMsg
        End Try
    End Function

    Private Function DeteleAllFilesInFedExPDFDirectory() As Boolean
        If Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(BaseApplicationVariables.FedExLabelDirectory)) = False OrElse Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(BaseApplicationVariables.FedExLabelDirectory)).Count = 0 Then
            Return False
        End If

        Dim info As DirectoryInfo = New DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(BaseApplicationVariables.FedExLabelDirectory))

        If (info.Exists AndAlso ((info.Attributes And FileAttributes.[ReadOnly]) = FileAttributes.[ReadOnly])) Then
            info.Attributes = (info.Attributes Xor FileAttributes.[ReadOnly])
        End If

        Dim failCount As Integer = 0
        'directory exist ... delete
        For Each s As String In System.IO.Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(BaseApplicationVariables.FedExLabelDirectory))
            Try
                System.IO.File.Delete(s)
            Catch Ex As Exception
                failCount = failCount + 1

                If failCount >= 3 Then
                    Exit For
                End If

                Continue For
            End Try

        Next s

        If failCount = 3 Then
            Return False
        End If

        Return True
    End Function


    <WebMethod()>
    Public Function GetsVendors(ByVal orderID As Integer, ByVal client As String)

        Dim list As New List(Of VendorShippingAddress)
        Dim js As New JavaScriptSerializer

        Try

            Dim sb As New System.Text.StringBuilder()
            sb.Append("  Select tblVanityShippingAddress.Vanity as 'VanityShippingName',tblVendorShippingAddress.VendorShippingID,tblVendorShippingAddress.CompanyID, tblVendorShippingAddress.ShippingDirectionTypeID,tblVendorShippingAddress.Address1,tblVendorShippingAddress.Address2,tblVendorShippingAddress.City,tblVendorShippingAddress.State,tblVendorShippingAddress.Zip,tblVendorShippingAddress.ContactPerson,tblVendorShippingAddress.BuildingLocation,tblVendorShippingAddress.BuildingPartCode,tblVendorShippingAddress.LocationDescription,tblVendorShippingAddress.OfficeClosingTime,tblCompany.Phone, tblCompany.Fax, tblCompany.Email 
from tblVendorShippingAddress inner join tblCompany on tblVendorShippingAddress.CompanyID = tblCompany.CompanyID inner join tblVanityShippingAddress on tblVanityShippingAddress.VendorShippingAddressID=tblVendorShippingAddress.VendorShippingID where tblVendorShippingAddress.CompanyID in (select distinct tblPartOrder.Vendor from dbo.tblPartOrder where OrderID = " & orderID.ToString() & " )")
            sb.Append(" order by VanityShippingName asc ")

            Using oConn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Using oCmd As New SqlCommand(sb.ToString(), oConn)
                    oCmd.CommandType = CommandType.Text

                    oConn.Open()

                    Using r As SqlDataReader = oCmd.ExecuteReader()
                        While r.Read()
                            Dim vendor As New VendorShippingAddress()
                            vendor.VendorShippingID = r.SanitizeInteger("VendorShippingID")
                            vendor.CompanyID = r.SanitizeInteger("CompanyID")
                            vendor.ShippingDirectionTypeID = r.SanitizeInteger("ShippingDirectionTypeID")
                            vendor.Address1 = r.SanitizeString("Address1")
                            vendor.Address2 = r.SanitizeString("Address2")
                            vendor.City = r.SanitizeString("City")
                            vendor.State = r.SanitizeString("State")
                            vendor.Zip = r.SanitizeString("Zip")
                            vendor.ContactPerson = r.SanitizeString("ContactPerson")
                            vendor.BuildingLocation = r.SanitizeInteger("BuildingLocation")
                            vendor.BuildingPartCode = r.SanitizeInteger("BuildingPartCode")
                            vendor.LocationDescription = r.SanitizeString("LocationDescription")
                            vendor.OfficeClosingTime = r.SanitizeShortDateTime("OfficeClosingTime")
                            vendor.VanityShippingName = r.SanitizeString("VanityShippingName")
                            vendor.ContactPersonPhone = r.SanitizeString("Phone")
                            vendor.Email = r.SanitizeString("Email")
                            vendor.Fax = r.SanitizeString("Fax")
                            list.Add(vendor)
                        End While
                    End Using
                End Using
            End Using

            Return js.Serialize(list)
        Catch Ex As Exception
            Dim errList As New List(Of String)
            errList.Add(BaseApplicationVariables.JSErrorMessageKeyword & "=" & Ex.Message.ToString())
            Return js.Serialize(errList)
        End Try

    End Function


    <WebMethod()>
    Public Function GetPartOrderByID(ByVal partID As String, ByVal client As String) As String
        Dim list As New List(Of PartOrder)
        Dim js As New JavaScriptSerializer

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            ' Dim strsql As String = " SELECT CustomerCompany.CompanyID as 'CustomerID', CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.ShipperTrack FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE dbo.tblPartOrder.OrderID = " & orderid & " and dbo.tblPartOrder.PartID in (" & listOfParts & ")"
            Dim sbSql As New System.Text.StringBuilder()
            sbSql.Append(" SELECT CustomerCompany.CompanyID as 'CustomerID', CustomerCompany.Company as Customer, dbo.tblPartOrder.PartID, dbo.tblPartOrder.OrderID, dbo.tblPartOrder.Vendor AS VendorID, dbo.tblCompany.Company AS Vendor, dbo.tblPartOrder.ShipperTrack,")
            sbSql.Append(" tblPartOrder.Servicer, tblPartOrder.Address1, tblPartOrder.City, tblPartOrder.State, tblPartOrder.Zip, tblPartOrder.Phone, tblPartOrder.Contact ")
            sbSql.Append(" FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID inner join tblOrder on tblOrder.OrderID=tblPartOrder.OrderID  inner join tblCompany as CustomerCompany on tblOrder.CustomerNo = CustomerCompany.Customerno WHERE dbo.tblPartOrder.PartID = " & partID & "")

            Dim sqlComm As New SqlCommand(sbSql.ToString(), conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New PartOrder
                    Dim objType As Type = p1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(p1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(p1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function


#End Region





#Region "Certified Warnings List"
    <WebMethod()>
    Public Function SetCertifiedWarnings(ByVal partNumber As String) As String

        Try
            'certified has it, get the warnings if any exist for the part

            Dim certifiedWarningClient As CertifiedLookup.Lookup = New CertifiedLookup.Lookup()
            Dim certifiedWarningResult = certifiedWarningClient.GetWarnings(BaseApplicationVariables.CertifiedUKey, partNumber)


            If certifiedWarningResult Is Nothing OrElse certifiedWarningResult.Tables.Count = 0 Then
                'nothing to do, bail
                Return Nothing
            End If

            'we have records ... drop into database
            Dim tbl As New DataTable
            tbl = certifiedWarningResult.Tables(0)

            If tbl.Rows.Count = 0 Then
                Return String.Empty
            End If


            Dim pPart As String = "@part"
            Dim pType As String = "@type"
            Dim pHeader As String = "@header"
            Dim pContent As String = "@content"


            Dim sqlParamPart As New SqlParameter()
            sqlParamPart.Value = tbl.Rows(0)("UPart")
            sqlParamPart.ParameterName = pPart
            sqlParamPart.DbType = SqlDbType.VarChar
            sqlParamPart.Direction = ParameterDirection.Input

            Dim sqlParamType As New SqlParameter()
            sqlParamType.Value = tbl.Rows(0)("UType")
            sqlParamType.ParameterName = pType
            sqlParamType.DbType = SqlDbType.VarChar
            sqlParamType.Direction = ParameterDirection.Input

            Dim sqlParamHeader As New SqlParameter()
            sqlParamHeader.Value = tbl.Rows(0)("UHeader")
            sqlParamHeader.ParameterName = pHeader
            sqlParamHeader.DbType = SqlDbType.VarChar
            sqlParamHeader.Direction = ParameterDirection.Input

            Dim sqlParamContent As New SqlParameter()
            sqlParamContent.Value = tbl.Rows(0)("UContent")
            sqlParamContent.ParameterName = pContent
            sqlParamContent.DbType = SqlDbType.VarChar
            sqlParamContent.Direction = ParameterDirection.Input


            'either we have a warning or we don't - if a warning is currently there but one isn't found, destroy what's there ...
            'if nothing is there the delete is superfulous and the insert holds sway.. avoids the if statement
            Dim sb As New System.Text.StringBuilder()

            sb.AppendLine(" delete from tblTransmissionWarnings where part = @part ")
            sb.AppendLine(" INSERT INTO [tblTransmissionWarnings](Part, Type, Header, Content) ")
            sb.AppendLine(" VALUES(" & pPart & ", " & pType & "," & pHeader & ", " & pContent & " ) ")

            Using oConn As New SqlConnection(ConnectionStrings.CKConnectionString)
                oConn.Open()
                Using oCmd As New SqlCommand(sb.ToString(), oConn)
                    oCmd.Parameters.Add(sqlParamPart)
                    oCmd.Parameters.Add(sqlParamType)
                    oCmd.Parameters.Add(sqlParamHeader)
                    oCmd.Parameters.Add(sqlParamContent)
                    oCmd.ExecuteNonQuery()
                End Using
            End Using

            Return String.Empty
        Catch Ex As Exception
            Return Ex.Message.ToString()
        End Try

    End Function

    '<WebMethod()>
    'Public Function GetPartsUpdateTransmissionWarnings() As Boolean

    '    Try
    '        Dim listOfError As New List(Of String)

    '        'certified has it, get the warnings if any exist for the part
    '        Dim sql As New System.Text.StringBuilder()
    '        Dim list As New List(Of String)
    '        sql.Append(" SELECT Distinct partnumber from tblCertifiedCatalog where partnumber IS NOT NULL AND len(partnumber) > 0 ")

    '        Using oConn As New SqlConnection(ConnectionStrings.CKConnectionString)
    '            oConn.Open()
    '            Using oCmd As New SqlCommand(sql.ToString(), oConn)
    '                Using oReader As SqlDataReader = oCmd.ExecuteReader()
    '                    While oReader.Read()
    '                        list.Add(oReader("partnumber").ToString())
    '                    End While
    '                End Using
    '            End Using
    '        End Using

    '        If list Is Nothing OrElse list.Count = 0 Then
    '            'nothing to update
    '            Return True
    '        End If

    '        For Each item In list
    '            Try
    '                SetCertifiedWarnings(item)
    '            Catch ex As Exception
    '                listOfError.Add(item & " - " & ex.Message.ToString())
    '                Continue For
    '            End Try

    '        Next

    '        Return True
    '    Catch Ex As Exception
    '        Return False
    '    End Try

    'End Function

    <WebMethod()>
    Public Function GetPartsUpdateTransmissionWarnings() As Boolean

        Try
            Dim listOfError As New List(Of String)

            'certified has it, get the warnings if any exist for the part
            Dim sql As New System.Text.StringBuilder()
            Dim list As New List(Of String)
            sql.Append(" SELECT Distinct partnumber from tblCertifiedCatalog where partnumber IS NOT NULL AND len(partnumber) > 0 ")

            Using oConn As New SqlConnection(ConnectionStrings.CKConnectionString)
                oConn.Open()
                Using oCmd As New SqlCommand(sql.ToString(), oConn)
                    Using oReader As SqlDataReader = oCmd.ExecuteReader()
                        While oReader.Read()
                            list.Add(oReader("partnumber").ToString())
                        End While
                    End Using
                End Using
            End Using

            If list Is Nothing OrElse list.Count = 0 Then
                'nothing to update
                Return True
            End If

            For Each item In list
                Try
                    SetCertifiedWarnings(item)
                Catch ex As Exception
                    listOfError.Add(item & " - " & ex.Message.ToString())
                    Continue For
                End Try

            Next

            Return True
        Catch Ex As Exception
            Return False
        End Try

    End Function

#End Region




End Class



