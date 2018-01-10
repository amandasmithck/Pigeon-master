
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class ReportsWebService
    Inherits System.Web.Services.WebService
    Public Class VendorStats

        Private _Company As System.String
        Private _Sold As System.Int32
        Private _Cost As System.Decimal
        Private _AvgCost As System.Decimal
        Private _Sale As System.Decimal
        Private _AvgSale As System.Decimal
        Private _AvgProfit As System.Decimal

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
            End Set
        End Property

        Public Property Sold() As System.Int32
            Get
                Return _Sold
            End Get
            Set(ByVal value As System.Int32)
                _Sold = value
            End Set
        End Property

        Public Property Cost() As System.Decimal
            Get
                Return _Cost
            End Get
            Set(ByVal value As System.Decimal)
                _Cost = value
            End Set
        End Property

        Public Property AvgCost() As System.Decimal
            Get
                Return _AvgCost
            End Get
            Set(ByVal value As System.Decimal)
                _AvgCost = value
            End Set
        End Property

        Public Property Sale() As System.Decimal
            Get
                Return _Sale
            End Get
            Set(ByVal value As System.Decimal)
                _Sale = value
            End Set
        End Property

        Public Property AvgSale() As System.Decimal
            Get
                Return _AvgSale
            End Get
            Set(ByVal value As System.Decimal)
                _AvgSale = value
            End Set
        End Property

        Public Property AvgProfit() As System.Decimal
            Get
                Return _AvgProfit
            End Get
            Set(ByVal value As System.Decimal)
                _AvgProfit = value
            End Set
        End Property

    End Class
    Public Class TotalGP

        Private _Company As System.String
        Private _Month As System.Int32
        Private _Year As System.Int32
        Private _PartType As System.String
        Private _PartCount As System.Int32
        Private _Sell As System.Decimal
        Private _Cost As System.Decimal
        Private _Ship As System.Decimal
        Private _CoreShip As System.Decimal
        Private _Livingston As System.Decimal
        Private _Gross As System.Decimal
        Private _GrossAverage As System.Decimal

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
            End Set
        End Property

        Public Property Month() As System.Int32
            Get
                Return _Month
            End Get
            Set(ByVal value As System.Int32)
                _Month = value
            End Set
        End Property

        Public Property Year() As System.Int32
            Get
                Return _Year
            End Get
            Set(ByVal value As System.Int32)
                _Year = value
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

        Public Property PartCount() As System.Int32
            Get
                Return _PartCount
            End Get
            Set(ByVal value As System.Int32)
                _PartCount = value
            End Set
        End Property

        Public Property Sell() As System.Decimal
            Get
                Return _Sell
            End Get
            Set(ByVal value As System.Decimal)
                _Sell = value
            End Set
        End Property

        Public Property Cost() As System.Decimal
            Get
                Return _Cost
            End Get
            Set(ByVal value As System.Decimal)
                _Cost = value
            End Set
        End Property

        Public Property Ship() As System.Decimal
            Get
                Return _Ship
            End Get
            Set(ByVal value As System.Decimal)
                _Ship = value
            End Set
        End Property

        Public Property CoreShip() As System.Decimal
            Get
                Return _CoreShip
            End Get
            Set(ByVal value As System.Decimal)
                _CoreShip = value
            End Set
        End Property

        Public Property Livingston() As System.Decimal
            Get
                Return _Livingston
            End Get
            Set(ByVal value As System.Decimal)
                _Livingston = value
            End Set
        End Property

        Public Property Gross() As System.Decimal
            Get
                Return _Gross
            End Get
            Set(ByVal value As System.Decimal)
                _Gross = value
            End Set
        End Property

        Public Property GrossAverage() As System.Decimal
            Get
                Return _GrossAverage
            End Get
            Set(ByVal value As System.Decimal)
                _GrossAverage = value
            End Set
        End Property

    End Class
    Public Class TotalGPGraph

        Private _DateOrdered As System.String
        Private _Gross As System.Decimal

        Public Property DateOrdered() As System.String
            Get
                Return _DateOrdered
            End Get
            Set(ByVal value As System.String)
                _DateOrdered = value
            End Set
        End Property

        Public Property Gross() As System.Decimal
            Get
                Return _Gross
            End Get
            Set(ByVal value As System.Decimal)
                _Gross = value
            End Set
        End Property

    End Class
    Public Class TotalPartsGraph

        Private _DateOrdered As System.String
        Private _Parts As Long
        Private _Index As Long

        Public Property DateOrdered() As System.String
            Get
                Return _DateOrdered
            End Get
            Set(ByVal value As System.String)
                _DateOrdered = value
            End Set
        End Property

        Public Property Parts() As Long
            Get
                Return _Parts
            End Get
            Set(ByVal value As Long)
                _Parts = value
            End Set
        End Property
        Public Property Index() As Long
            Get
                Return _Index
            End Get
            Set(ByVal value As Long)
                _Index = value
            End Set
        End Property

    End Class
    Public Class TotalGPWarranty

        Private _Company As System.String
        Private _Vendor As System.String
        Private _Month As System.Int32
        Private _Year As System.Int32
        Private _PartType As System.String
        Private _OrderCount As System.Int32
        Private _Sell As System.Decimal
        Private _Cost As System.Decimal
        Private _Ship As System.Decimal
        Private _CoreShip As System.Decimal
        Private _Livingston As System.Decimal
        Private _Gross As System.Decimal
        Private _GrossAverage As System.Decimal
        Private _LaborPayout As System.Decimal
        Private _LaborCredit As System.Decimal

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
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

        Public Property Month() As System.Int32
            Get
                Return _Month
            End Get
            Set(ByVal value As System.Int32)
                _Month = value
            End Set
        End Property

        Public Property Year() As System.Int32
            Get
                Return _Year
            End Get
            Set(ByVal value As System.Int32)
                _Year = value
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

        Public Property OrderCount() As System.Int32
            Get
                Return _OrderCount
            End Get
            Set(ByVal value As System.Int32)
                _OrderCount = value
            End Set
        End Property

        Public Property Sell() As System.Decimal
            Get
                Return _Sell
            End Get
            Set(ByVal value As System.Decimal)
                _Sell = value
            End Set
        End Property

        Public Property Cost() As System.Decimal
            Get
                Return _Cost
            End Get
            Set(ByVal value As System.Decimal)
                _Cost = value
            End Set
        End Property

        Public Property Ship() As System.Decimal
            Get
                Return _Ship
            End Get
            Set(ByVal value As System.Decimal)
                _Ship = value
            End Set
        End Property

        Public Property CoreShip() As System.Decimal
            Get
                Return _CoreShip
            End Get
            Set(ByVal value As System.Decimal)
                _CoreShip = value
            End Set
        End Property

        Public Property Livingston() As System.Decimal
            Get
                Return _Livingston
            End Get
            Set(ByVal value As System.Decimal)
                _Livingston = value
            End Set
        End Property

        Public Property Gross() As System.Decimal
            Get
                Return _Gross
            End Get
            Set(ByVal value As System.Decimal)
                _Gross = value
            End Set
        End Property

        Public Property GrossAverage() As System.Decimal
            Get
                Return _GrossAverage
            End Get
            Set(ByVal value As System.Decimal)
                _GrossAverage = value
            End Set
        End Property

        Public Property LaborPayout() As System.Decimal
            Get
                Return _LaborPayout
            End Get
            Set(ByVal value As System.Decimal)
                _LaborPayout = value
            End Set
        End Property

        Public Property LaborCredit() As System.Decimal
            Get
                Return _LaborCredit
            End Get
            Set(ByVal value As System.Decimal)
                _LaborCredit = value
            End Set
        End Property

    End Class
    Public Class IncomeExpense

        Private _InvoiceType As System.String
        Private _InvoiceNo As System.String
        Private _DateEntered As System.String
        Private _Amount As System.Decimal
        Private _AmountPaid As System.Decimal
        Private _DatePaid As System.String
        Private _Company As System.String
        Private _OrderID As System.Int32

        Public Property InvoiceType() As System.String
            Get
                Return _InvoiceType
            End Get
            Set(ByVal value As System.String)
                _InvoiceType = value
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

        Public Property DateEntered() As System.String
            Get
                Return _DateEntered
            End Get
            Set(ByVal value As System.String)
                _DateEntered = value
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

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
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

    End Class
    Public Class OutstandingRecPay

        Private _InvoiceType As System.String
        Private _InvoiceNo As System.String
        Private _DateEntered As System.String
        Private _Amount As System.Decimal
        Private _AmountPaid As System.Decimal
        Private _DatePaid As System.String
        Private _Company As System.String
        Private _OrderID As System.Int32
        Private _AuthorizationNo As System.String
        Private _ContractNo As System.String
        Private _Tax As System.Boolean

        Public Property InvoiceType() As System.String
            Get
                Return _InvoiceType
            End Get
            Set(ByVal value As System.String)
                _InvoiceType = value
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

        Public Property DateEntered() As System.String
            Get
                Return _DateEntered
            End Get
            Set(ByVal value As System.String)
                _DateEntered = value
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

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
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

        Public Property AuthorizationNo() As System.String
            Get
                Return _AuthorizationNo
            End Get
            Set(ByVal value As System.String)
                _AuthorizationNo = value
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

        Public Property Tax() As System.Boolean
            Get
                Return _Tax
            End Get
            Set(ByVal value As System.Boolean)
                _Tax = value
            End Set
        End Property


    End Class

    Public Class CustomerRec

        Private _InvoiceType As System.String
        Private _InvoiceNo As System.String
        Private _DateEntered As System.String
        Private _Amount As System.Decimal
        Private _AmountPaid As System.Decimal
        Private _DatePaid As System.String
        Private _Company As System.String
        Private _OrderID As System.Int32
        Private _AuthorizationNo As System.String
        Private _ContractNo As System.String
        Private _Tax As System.Boolean
        Private _Servicer As System.String

        Public Property InvoiceType() As System.String
            Get
                Return _InvoiceType
            End Get
            Set(ByVal value As System.String)
                _InvoiceType = value
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

        Public Property DateEntered() As System.String
            Get
                Return _DateEntered
            End Get
            Set(ByVal value As System.String)
                _DateEntered = value
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

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
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

        Public Property AuthorizationNo() As System.String
            Get
                Return _AuthorizationNo
            End Get
            Set(ByVal value As System.String)
                _AuthorizationNo = value
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

        Public Property Tax() As System.Boolean
            Get
                Return _Tax
            End Get
            Set(ByVal value As System.Boolean)
                _Tax = value
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

    End Class
    Public Class LaborPayCred

        Private _LaborPayouts As System.Decimal
        Private _LaborCredits As System.Decimal
        Private _Total As System.Decimal


        Public Property LaborPayouts() As System.Decimal
            Get
                Return _LaborPayouts
            End Get
            Set(ByVal value As System.Decimal)
                _LaborPayouts = value
            End Set
        End Property

        Public Property LaborCredits() As System.Decimal
            Get
                Return _LaborCredits
            End Get
            Set(ByVal value As System.Decimal)
                _LaborCredits = value
            End Set
        End Property

        Public Property Total() As System.Decimal
            Get
                Return _Total
            End Get
            Set(ByVal value As System.Decimal)
                _Total = value
            End Set
        End Property

    End Class
    Public Class LaborPayCredGraph

        Private _DateEntered As System.String
        Private _Amount As System.Decimal

        Public Property DateEntered() As System.String
            Get
                Return _DateEntered
            End Get
            Set(ByVal value As System.String)
                _DateEntered = value
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

    End Class
    Public Class PaymentsTofrom

        Private _InvoiceNo As System.String
        Private _InvoiceType As System.String
        Private _OrderID As System.Int32
        Private _PartDescription As System.String
        Private _Payee As System.String
        Private _Amount As System.Decimal
        Private _AmountPaid As System.Decimal
        Private _DatePaid As System.String
        Private _PaymentType As System.String
        Private _CheckNo As System.String
        Private _Type As System.String
        Private _InvoiceTypeID As System.Int32
        Private _InvoiceID As System.Int32
        Private _PartID As System.Int32
        Private _Payer As System.String
        Private _CCDatePaid As System.String

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

        Public Property OrderID() As System.Int32
            Get
                Return _OrderID
            End Get
            Set(ByVal value As System.Int32)
                _OrderID = value
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

        Public Property Payee() As System.String
            Get
                Return _Payee
            End Get
            Set(ByVal value As System.String)
                _Payee = value
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

        Public Property Type() As System.String
            Get
                Return _Type
            End Get
            Set(ByVal value As System.String)
                _Type = value
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

        Public Property InvoiceID() As System.Int32
            Get
                Return _InvoiceID
            End Get
            Set(ByVal value As System.Int32)
                _InvoiceID = value
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

        Public Property Payer() As System.String
            Get
                Return _Payer
            End Get
            Set(ByVal value As System.String)
                _Payer = value
            End Set
        End Property

        Public Property CCDatePaid() As System.String
            Get
                Return _CCDatePaid
            End Get
            Set(ByVal value As System.String)
                _CCDatePaid = value
            End Set
        End Property

    End Class
    Public Class SalesVolume

        Private _Company As System.String
        Private _DateOrdered As System.String
        Private _ContractNo As System.String
        Private _PartDescription As System.String
        Private _SellPrice As System.Decimal

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
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

        Public Property ContractNo() As System.String
            Get
                Return _ContractNo
            End Get
            Set(ByVal value As System.String)
                _ContractNo = value
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

        Public Property SellPrice() As System.Decimal
            Get
                Return _SellPrice
            End Get
            Set(ByVal value As System.Decimal)
                _SellPrice = value
            End Set
        End Property

    End Class
    Public Class PaymentsMarked

        Private _Company As System.String
        Private _AmountPaid As System.Decimal
        Private _PaymentType As System.String
        Private _CheckNo As System.String
        Private _OrderID As System.Int32

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
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

        Public Property OrderID() As System.Int32
            Get
                Return _OrderID
            End Get
            Set(ByVal value As System.Int32)
                _OrderID = value
            End Set
        End Property

    End Class
    Public Class AftermarketType

        Private _PartType As System.String
        Private _Units As System.Int32
        Private _Amount As System.Decimal

        Public Property PartType() As System.String
            Get
                Return _PartType
            End Get
            Set(ByVal value As System.String)
                _PartType = value
            End Set
        End Property

        Public Property Units() As System.Int32
            Get
                Return _Units
            End Get
            Set(ByVal value As System.Int32)
                _Units = value
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

    End Class
    Public Class CustRelat

        Private _OrderYear As System.Int32
        Private _OrderMonth As System.Int32
        Private _MonthName As System.String
        Private _Timestamp As System.String
        Private _Customerno As System.String
        Private _Company As System.String
        Private _AftermarketUnits As System.Int32
        Private _AftermarketAmount As System.Decimal
        Private _AftermarketList As List(Of AftermarketType)
        Private _OEMUnits As System.Int32
        Private _OEMAmount As System.Decimal
        Private _SmallUnits As System.Int32
        Private _SmallAmount As System.Decimal
        Private _TotalUnits As System.Int32
        Private _TotalAmount As System.Decimal
        Private _Inspections As System.Int32

        Public Property OrderYear() As System.Int32
            Get
                Return _OrderYear
            End Get
            Set(ByVal value As System.Int32)
                _OrderYear = value
            End Set
        End Property

        Public Property OrderMonth() As System.Int32
            Get
                Return _OrderMonth
            End Get
            Set(ByVal value As System.Int32)
                _OrderMonth = value
            End Set
        End Property

        Public Property MonthName() As System.String
            Get
                Return _MonthName
            End Get
            Set(ByVal value As System.String)
                _MonthName = value
            End Set
        End Property

        Public Property Timestamp() As System.String
            Get
                Return _Timestamp
            End Get
            Set(ByVal value As System.String)
                _Timestamp = value
            End Set
        End Property

        Public Property Customerno() As System.String
            Get
                Return _Customerno
            End Get
            Set(ByVal value As System.String)
                _Customerno = value
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

        Public Property AftermarketUnits() As System.Int32
            Get
                Return _AftermarketUnits
            End Get
            Set(ByVal value As System.Int32)
                _AftermarketUnits = value
            End Set
        End Property

        Public Property AftermarketAmount() As System.Decimal
            Get
                Return _AftermarketAmount
            End Get
            Set(ByVal value As System.Decimal)
                _AftermarketAmount = value
            End Set
        End Property

        Public Property AftermarketList() As List(Of AftermarketType)
            Get
                Return _AftermarketList
            End Get
            Set(ByVal value As List(Of AftermarketType))
                _AftermarketList = value
            End Set
        End Property

        Public Property OEMUnits() As System.Int32
            Get
                Return _OEMUnits
            End Get
            Set(ByVal value As System.Int32)
                _OEMUnits = value
            End Set
        End Property

        Public Property OEMAmount() As System.Decimal
            Get
                Return _OEMAmount
            End Get
            Set(ByVal value As System.Decimal)
                _OEMAmount = value
            End Set
        End Property

        Public Property SmallUnits() As System.Int32
            Get
                Return _SmallUnits
            End Get
            Set(ByVal value As System.Int32)
                _SmallUnits = value
            End Set
        End Property

        Public Property SmallAmount() As System.Decimal
            Get
                Return _SmallAmount
            End Get
            Set(ByVal value As System.Decimal)
                _SmallAmount = value
            End Set
        End Property

        Public Property TotalUnits() As System.Int32
            Get
                Return _TotalUnits
            End Get
            Set(ByVal value As System.Int32)
                _TotalUnits = value
            End Set
        End Property

        Public Property TotalAmount() As System.Decimal
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As System.Decimal)
                _TotalAmount = value
            End Set
        End Property

        Public Property Inspections() As System.Int32
            Get
                Return _Inspections
            End Get
            Set(ByVal value As System.Int32)
                _Inspections = value
            End Set
        End Property
    End Class
    <WebMethod()>
    Public Function GetNonCustomersByParent()

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            strSql = "SELECT Company + '-' + Type as Company,ParentCompanyID as Companyid from tblCompany where company is not null and active =1 and type <>'Customer' order by Company"

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
    Public Function GetVendorStatsYTD(ByVal ytd As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of VendorStats)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT TOP 100 PERCENT dbo.tblCompany.Company, COUNT(dbo.tblPartOrder.PartID) AS Sold, SUM(dbo.tblPartOrder.CostPrice) AS Cost, AVG(dbo.tblPartOrder.CostPrice) AS AvgCost, SUM(dbo.tblPartOrder.SellPrice) AS Sale, AVG(dbo.tblPartOrder.SellPrice) AS AvgSale, AVG(dbo.tblPartOrder.SellPrice - dbo.tblPartOrder.CostPrice) AS AvgProfit FROM dbo.tblPartOrder INNER JOIN dbo.tblCompany ON dbo.tblPartOrder.Vendor = dbo.tblCompany.CompanyID WHERE (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND "
            If ytd = "yes" Then
                strsql = strsql & " (YEAR(dbo.tblPartOrder.DateOrdered) = YEAR({ fn NOW() }))"
            Else
                strsql = strsql & "(dbo.tblpartorder.dateordered >= '" & fromdate & "') and (dbo.tblpartorder.dateordered < '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "')"
            End If
            strsql = strsql & " GROUP BY dbo.tblCompany.Company ORDER BY dbo.tblCompany.Company "
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim v1 As New VendorStats
                    Dim objType As Type = v1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(v1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(v1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetTotalGP(ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of TotalGP)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT TOP (100) PERCENT dbo.tblCompany.Company, MONTH(dbo.tblPartOrder.DateOrdered) AS Month, YEAR(dbo.tblPartOrder.DateOrdered) AS Year, dbo.tblPartOrder.PartType, COUNT(dbo.tblPartOrder.OrderID) AS PartCount, SUM(dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + isnull(dbo.tblPartOrder.CustShippingPrice,0) + isnull(dbo.tblPartOrder.CustCoreShippingPrice,0) + dbo.tblPartOrder.WarrantyCost) AS Sell, SUM(CASE WHEN sellprice > 0 THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity ELSE 0 END) AS Cost, SUM(isnull(dbo.tblPartOrder.ShippingPrice,0))   AS Ship, SUM(dbo.tblPartOrder.CoreShippingPrice) AS CoreShip, SUM(dbo.tblPartOrder.Livingston) AS Livingston, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + isnull(dbo.tblPartOrder.CustShippingPrice,0) + isnull(dbo.tblPartOrder.CustCoreShippingPrice,0) + isNull(dbo.tblPartOrder.WarrantyCost,0)) - ((CASE WHEN sellprice > 0 THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity ELSE 0 END) + isnull(dbo.tblPartOrder.ShippingPrice,0) + isnull(dbo.tblPartOrder.CoreShippingPrice,0) + isnull(dbo.tblPartOrder.Livingston,0))) AS Gross, AVG(dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + isnull(dbo.tblPartOrder.CustShippingPrice,0) + isnull(dbo.tblPartOrder.CustCoreShippingPrice,0) + isNull(dbo.tblPartOrder.WarrantyCost,0) - ((CASE WHEN sellprice > 0 THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity ELSE 0 END)) + isnull(dbo.tblPartOrder.ShippingPrice,0) + isnull(dbo.tblPartOrder.CoreShippingPrice,0) + isnull(dbo.tblPartOrder.Livingston,0)) AS GrossAverage  FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID  INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo  WHERE tblOrder.Customerno <> '9997C'  and (dbo.tblPartOrder.Cancelled = 0)  AND (dbo.tblPartOrder.PreviousPartID = 0)  AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "'))  GROUP BY dbo.tblPartOrder.PartType, dbo.tblCompany.Company, MONTH(dbo.tblPartOrder.DateOrdered), YEAR(dbo.tblPartOrder.DateOrdered)  ORDER BY dbo.tblCompany.Company, MONTH(dbo.tblPartOrder.DateOrdered), YEAR(dbo.tblPartOrder.DateOrdered), dbo.tblPartOrder.PartType"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim t1 As New TotalGP
                    Dim objType As Type = t1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(t1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(t1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetTotalGPGraph(ByVal customerno As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of TotalGPGraph)
        Dim js As New JavaScriptSerializer

        'get a list of date range(important for stacked bar graph since all part types might not have sales on the same day

        Dim startDate = CDate(fromdate)
        Dim endDate = CDate(todate)

        Dim dates = Enumerable.Range(0, 1 + CInt((endDate - startDate).TotalDays)).
                               Select(Function(n) startDate.AddDays(n)).
                               ToArray()

        Dim x As Integer = 0
        For Each d As Date In dates
            Dim t1 As New TotalGPGraph
            t1.DateOrdered = d.ToShortDateString()
            't1.Index = x
            x = x + 1
            list.Add(t1)
        Next

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String
            If customerno = "0" Then
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost) - ((CASE WHEN sellprice > 0 THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity ELSE 0 END)   + dbo.tblPartOrder.ShippingPrice + dbo.tblPartOrder.CoreShippingPrice + dbo.tblPartOrder.Livingston)) AS Gross FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE tblOrder.Customerno <> '9997C' and (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "')) GROUP BY convert(varchar,dbo.tblPartOrder.DateOrdered,101) order by convert(varchar,dbo.tblPartOrder.DateOrdered,101)"
            Else
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost) - ((CASE WHEN sellprice > 0 THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity ELSE 0 END)   + dbo.tblPartOrder.ShippingPrice + dbo.tblPartOrder.CoreShippingPrice + dbo.tblPartOrder.Livingston)) AS Gross FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE tblOrder.Customerno <> '9997C' and (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "')) and dbo.tblOrder.CustomerNo = '" & customerno & "' GROUP BY convert(varchar,dbo.tblPartOrder.DateOrdered,101) order by convert(varchar,dbo.tblPartOrder.DateOrdered,101)"
            End If

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    For Each t As TotalGPGraph In list
                        If DateDiff(DateInterval.Day, CDate(t.DateOrdered), CDate(r("dateordered").ToString)) = 0 Then
                            t.Gross = r("gross").ToString
                        End If
                    Next

                End While
            End Using
        End Using

        'now loop through each and change format
        For Each t As TotalGPGraph In list
            Dim thedate As Date = t.DateOrdered
            Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
            Dim time As System.DateTime = thedate.Subtract(span)
            t.DateOrdered = CLng(time.Ticks \ 10000)
        Next
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetTotalGPWarranty(ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of TotalGPWarranty)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT  TOP (100) PERCENT dbo.tblCompany.Company, tblCompany_1.Company AS Vendor, MONTH(dbo.tblPartOrder.DateEntered) AS Month, YEAR(dbo.tblPartOrder.DateEntered) AS Year, dbo.tblPartOrder.PartType AS PartType, COUNT(dbo.tblPartOrder.OrderID) AS OrderCount, SUM(dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost) AS Sell, SUM(CASE WHEN parttype = 'OEM' THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity + 2.00 ELSE costprice * quantity END) AS Cost,  SUM(dbo.tblPartOrder.ShippingPrice) AS Ship, SUM(dbo.tblPartOrder.CoreShippingPrice) AS CoreShip, SUM(dbo.tblPartOrder.Livingston) AS Livingston, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost) - (CASE WHEN parttype = 'OEM' THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity + 2.00 ELSE costprice * quantity END + dbo.tblPartOrder.ShippingPrice + dbo.tblPartOrder.CoreShippingPrice + dbo.tblPartOrder.Livingston)) AS Gross,  AVG((dbo.tblPartOrder.SellPrice + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost)   - (CASE WHEN parttype = 'OEM' THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity + 2.00 ELSE costprice * quantity END + dbo.tblPartOrder.ShippingPrice + dbo.tblPartOrder.CoreShippingPrice  + dbo.tblPartOrder.Livingston)) AS GrossAverage, SUM(laborpay.AmountPaid) AS LaborPayout, SUM(laborcredit.AmountPaid) AS LaborCredit FROM dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.tblCompany AS tblCompany_1 ON dbo.tblPartOrder.Vendor = tblCompany_1.CompanyID LEFT OUTER JOIN (SELECT  OrderID, AmountPaid FROM dbo.tblInvoices WHERE  (Deleted = 0) AND (InvoiceTypeID = 7)) AS laborpay ON laborpay.OrderID = dbo.tblPartOrder.OrderID LEFT OUTER JOIN  (SELECT  OrderID, AmountPaid FROM  dbo.tblInvoices AS tblInvoices_1 WHERE  (Deleted = 0) AND (InvoiceTypeID = 8)) AS laborcredit ON laborcredit.OrderID = dbo.tblPartOrder.OrderID WHERE  (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateEntered >= '" & fromdate & "') AND (dbo.tblPartOrder.DateEntered < DATEADD(d, 1, '" & todate & "')) GROUP BY dbo.tblPartOrder.PartType, dbo.tblCompany.Company, MONTH(dbo.tblPartOrder.DateEntered), YEAR(dbo.tblPartOrder.DateEntered), tblCompany_1.Company ORDER BY dbo.tblCompany.Company, Month, Year, dbo.tblPartOrder.PartType"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim t1 As New TotalGPWarranty
                    Dim objType As Type = t1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(t1, r(prop.Name), Nothing)
                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(t1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetTotalGPWarrantyGraph(ByVal customerno As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of TotalGPGraph)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String
            If customerno = "0" Then
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost) - (CASE WHEN parttype = 'OEM' THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity + 2.00 ELSE costprice * quantity END + dbo.tblPartOrder.ShippingPrice + dbo.tblPartOrder.CoreShippingPrice + dbo.tblPartOrder.Livingston)) AS Gross FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE tblOrder.Customerno <> '9997C' and (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "')) GROUP BY convert(varchar,dbo.tblPartOrder.DateOrdered,101) order by convert(varchar,dbo.tblPartOrder.DateOrdered,101)"
            Else
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost) - (CASE WHEN parttype = 'OEM' THEN dbo.tblPartOrder.CostPrice * dbo.tblPartOrder.Quantity + 2.00 ELSE costprice * quantity END + dbo.tblPartOrder.ShippingPrice + dbo.tblPartOrder.CoreShippingPrice + dbo.tblPartOrder.Livingston)) AS Gross FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE tblOrder.Customerno <> '9997C' and (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "')) and dbo.tblOrder.CustomerNo = '" & customerno & "' GROUP BY convert(varchar,dbo.tblPartOrder.DateOrdered,101) order by convert(varchar,dbo.tblPartOrder.DateOrdered,101)"
            End If

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim t1 As New TotalGPGraph
                    't1.DateOrdered = r("DateOrdered").ToString
                    Dim thedate As Date = CDate(r("DateOrdered").ToString)
                    Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
                    Dim time As System.DateTime = thedate.Subtract(span)
                    t1.DateOrdered = CLng(time.Ticks \ 10000)
                    t1.Gross = r("gross").ToString
                    list.Add(t1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetIncomeExpense(ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of IncomeExpense)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT dbo.tblInvoiceType.InvoiceType, dbo.tblInvoices.InvoiceNo, convert(varchar,dbo.tblInvoices.DateEntered,101) as DateEntered, CASE WHEN tblinvoices.invoicetypeid = 1 THEN SUM(sellprice * Quantity) + SUM(custshippingprice) + SUM(custcoreshippingprice) ELSE Amount END AS Amount, dbo.tblInvoices.AmountPaid, case when dbo.tblInvoices.DatePaid is null then '' else convert(varchar,dbo.tblInvoices.DatePaid,101) end as DatePaid, dbo.tblCompany.Company, dbo.tblInvoices.OrderID FROM dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID LEFT OUTER JOIN dbo.tblPartOrder ON dbo.tblInvoices.InvoiceNo = dbo.tblPartOrder.InvoiceNo WHERE (dbo.tblinvoices.deleted = 0) and (dbo.tblInvoices.DateEntered >= '" & fromdate & "') AND (dbo.tblInvoices.DateEntered < '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "') AND(dbo.tblInvoices.InvoiceTypeID = 1) AND (dbo.tblPartOrder.Cancelled IS NULL OR dbo.tblPartOrder.Cancelled <> 1)  GROUP BY tblinvoices.invoicetypeid, dbo.tblInvoiceType.InvoiceType, dbo.tblInvoices.InvoiceNo, dbo.tblInvoices.DateEntered, dbo.tblInvoices.Amount, dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, dbo.tblCompany.Company, dbo.tblInvoices.OrderID UNION SELECT TOP 100 PERCENT tblInvoiceType_1.InvoiceType, tblInvoices_1.InvoiceNo, convert(varchar,tblInvoices_1.DateEntered,101) as DateEntered, tblInvoices_1.Amount, tblInvoices_1.AmountPaid, case when tblInvoices_1.DatePaid is null then '' else convert(varchar,tblInvoices_1.DatePaid,101) end as DatePaid, CASE WHEN tblcompany_1.Company = 'C&K' THEN tblcompany_2.company ELSE tblcompany_1.company END AS Company, tblInvoices_1.OrderID FROM dbo.tblInvoices AS tblInvoices_1 INNER JOIN dbo.tblInvoiceType AS tblInvoiceType_1 ON tblInvoices_1.InvoiceTypeID = tblInvoiceType_1.InvoiceTypeID INNER JOIN dbo.tblCompany AS tblCompany_1 ON tblInvoices_1.Payer = tblCompany_1.CompanyID INNER JOIN dbo.tblCompany AS tblCompany_2 ON tblInvoices_1.Payee = tblCompany_2.CompanyID LEFT OUTER JOIN dbo.tblPartOrder AS tblPartOrder_1 ON tblInvoices_1.PartID = tblPartOrder_1.PartID WHERE (tblinvoices_1.deleted = 0) and (tblInvoices_1.DateEntered >= '" & fromdate & "') AND (tblInvoices_1.DateEntered < '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "')  AND (tblInvoices_1.InvoiceTypeID <> 1) AND (tblPartOrder_1.Cancelled IS NULL OR tblPartOrder_1.Cancelled <> 1)  ORDER BY tblInvoices.OrderID"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim i1 As New IncomeExpense
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
    Public Function GetOutstandingRecPay(ByVal invoicetype As String, ByVal asofdate As String, ByVal client As String)
        Dim list As New List(Of OutstandingRecPay)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT   TOP (100) PERCENT dbo.tblInvoiceType.InvoiceType, dbo.tblInvoices.InvoiceNo, convert(varchar,dbo.tblInvoices.DateEntered,101) as DateEntered, dbo.tblInvoices.Amount, dbo.tblInvoices.AmountPaid,  convert(varchar,dbo.tblInvoices.DatePaid,101) as DatePaid, CASE WHEN payer = 192 THEN tblcompany_1.company ELSE tblcompany.company END AS Company, dbo.tblInvoices.OrderID, dbo.tblOrder.AuthorizationNo, dbo.tblOrder.ContractNo, dbo.tblCompany.Tax FROM  dbo.tblOrder INNER JOIN dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN  dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID ON dbo.tblOrder.OrderID = dbo.tblInvoices.OrderID INNER JOIN dbo.tblCompany AS tblCompany_1 ON dbo.tblInvoices.Payee = tblCompany_1.CompanyID WHERE (dbo.tblInvoices.Deleted = 0) AND (dbo.tblInvoices.DatePaid > '" & asofdate & "' OR dbo.tblInvoices.DatePaid IS NULL) AND (dbo.tblInvoices.DateEntered < '" & DateAdd(DateInterval.Day, 1, CDate(asofdate)) & "') and dbo.tblInvoiceType.InvoiceType like '%" & invoicetype & "%' " 'GROUP BY dbo.tblInvoiceType.InvoiceType, dbo.tblInvoices.InvoiceNo, dbo.tblInvoices.DateEntered, dbo.tblInvoices.Amount, dbo.tblInvoices.AmountPaid, dbo.tblInvoices.DatePaid, CASE WHEN payer = 192 THEN tblcompany_1.company ELSE tblcompany.company END, dbo.tblInvoices.OrderID, dbo.tblInvoices.InvoiceTypeID, dbo.tblOrder.AuthorizationNo, dbo.tblOrder.ContractNo, dbo.tblCompany.Tax "
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim o1 As New OutstandingRecPay
                    Dim objType As Type = o1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(o1, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(o1)
                End While
            End Using
        End Using
        js.MaxJsonLength = Int32.MaxValue
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetCustomerRec(ByVal asofdate As String, ByVal client As String)
        Dim list As New List(Of CustomerRec)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT distinct dbo.tblInvoiceType.InvoiceType, dbo.tblInvoices.InvoiceNo, convert(varchar,dbo.tblInvoices.DateEntered,101) as DateEntered, dbo.tblInvoices.Amount, dbo.tblInvoices.AmountPaid,  convert(varchar,dbo.tblInvoices.DatePaid,101) as DatePaid, CASE WHEN payer = 192 THEN tblcompany_1.company ELSE tblcompany.company END AS Company, dbo.tblInvoices.OrderID, dbo.tblOrder.AuthorizationNo, dbo.tblOrder.ContractNo, dbo.tblCompany.Tax, Servicer FROM  dbo.tblOrder INNER JOIN dbo.tblInvoices INNER JOIN dbo.tblInvoiceType ON dbo.tblInvoices.InvoiceTypeID = dbo.tblInvoiceType.InvoiceTypeID INNER JOIN  dbo.tblCompany ON dbo.tblInvoices.Payer = dbo.tblCompany.CompanyID ON dbo.tblOrder.OrderID = dbo.tblInvoices.OrderID INNER JOIN dbo.tblCompany AS tblCompany_1 ON dbo.tblInvoices.Payee = tblCompany_1.CompanyID INNER JOIN tblPartOrder on tblPartOrder.OrderID = tblInvoices.OrderID WHERE (dbo.tblInvoices.Deleted = 0) AND (dbo.tblInvoices.DatePaid > '" & asofdate & "' OR dbo.tblInvoices.DatePaid IS NULL) AND (dbo.tblInvoices.DateEntered < '" & DateAdd(DateInterval.Day, 1, CDate(asofdate)) & "') and dbo.tblInvoices.InvoiceTypeID in (1,5,6) "
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim o1 As New CustomerRec
                    Dim objType As Type = o1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(o1, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(o1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetLaborPayCred(ByVal fromdate As String, ByVal todate As String, ByVal client As String)

        Dim list As New List(Of LaborPayCred)
        Dim js As New JavaScriptSerializer
        Dim l1 As New LaborPayCred

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT sum(case when datepaid is not null then tblInvoices.amountpaid else amount end)  as LaborPayout FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany tblCompany_1 ON tblInvoices.Payee = tblCompany_1.CompanyID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID WHERE (tblInvoices.Dateentered >= '" & fromdate & "') AND (tblInvoices.Dateentered < dateadd(d,1,'" & todate & "')) AND (tblInvoices.InvoiceTypeID = 7) AND (tblInvoices.Payer = 192) And (tblInvoices.Deleted = 0)"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Dim decPayouts = sqlComm.ExecuteScalar

            l1.LaborPayouts = If(IsDBNull(decPayouts), 0, decPayouts)

            strsql = "SELECT sum(case when datepaid is not null then tblInvoices.amountpaid else amount end)  as LaborCredit FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany tblCompany_1 ON tblInvoices.Payee = tblCompany_1.CompanyID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID WHERE (tblInvoices.Dateentered >= '" & fromdate & "') AND (tblInvoices.Dateentered < dateadd(d,1,'" & todate & "'))  AND (tblInvoices.InvoiceTypeID = 8) AND (tblInvoices.Payee = 192) And (tblInvoices.Deleted = 0)"
            sqlComm = New SqlCommand(strsql, conn)
            Dim decCredits = sqlComm.ExecuteScalar

            l1.LaborCredits = IIf(IsDBNull(decCredits), 0, decCredits)

        End Using

        l1.Total = l1.LaborPayouts - l1.LaborCredits
        list.Add(l1)

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetLaborPOGraph(ByVal customerno As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of LaborPayCredGraph)
        Dim js As New JavaScriptSerializer

        'get a list of date range(important for stacked bar graph since all part types might not have sales on the same day

        Dim startDate = CDate(fromdate)
        Dim endDate = CDate(todate)

        Dim dates = Enumerable.Range(0, 1 + CInt((endDate - startDate).TotalDays)).
                               Select(Function(n) startDate.AddDays(n)).
                               ToArray()

        Dim x As Integer = 0
        For Each d As Date In dates
            Dim t1 As New LaborPayCredGraph
            t1.DateEntered = d.ToShortDateString()
            't1.Index = x
            x = x + 1
            list.Add(t1)
        Next

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String

            strsql = "SELECT convert(varchar,dateentered,101) as dateentered, sum(tblInvoices.amountPaid) as Amount FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany tblCompany_1 ON tblInvoices.Payee = tblCompany_1.CompanyID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID inner join tblorder on tblinvoices.orderid=tblorder.orderid WHERE (tblInvoices.dateentered >= '" & fromdate & "') AND (tblInvoices.dateentered < dateadd(d,1,'" & todate & "')) AND (tblInvoices.InvoiceTypeID = 7) AND (tblInvoices.Payer = 192)"

            If customerno <> "0" Then
                strsql = strsql & " and tblorder.customerno ='" & customerno & "'"
            End If

            strsql = strsql & " group by convert(varchar,dateentered,101)  order by convert(varchar,dateentered,101)"


            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    For Each t As LaborPayCredGraph In list
                        If DateDiff(DateInterval.Day, CDate(t.DateEntered), CDate(r("dateentered").ToString)) = 0 Then
                            t.Amount = r("amount").ToString
                        End If
                    Next
                End While
            End Using
        End Using

        'now loop through each and change format
        For Each t As LaborPayCredGraph In list
            Dim thedate As Date = t.DateEntered
            Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
            Dim time As System.DateTime = thedate.Subtract(span)
            t.DateEntered = CLng(time.Ticks \ 10000)
        Next

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetLaborCredGraph(ByVal customerno As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of LaborPayCredGraph)
        Dim js As New JavaScriptSerializer

        'get a list of date range(important for stacked bar graph since all part types might not have sales on the same day

        Dim startDate = CDate(fromdate)
        Dim endDate = CDate(todate)

        Dim dates = Enumerable.Range(0, 1 + CInt((endDate - startDate).TotalDays)).
                               Select(Function(n) startDate.AddDays(n)).
                               ToArray()

        Dim x As Integer = 0
        For Each d As Date In dates
            Dim t1 As New LaborPayCredGraph
            t1.DateEntered = d.ToShortDateString()
            't1.Index = x
            x = x + 1
            list.Add(t1)
        Next

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT convert(varchar,dateentered,101) as dateentered, sum(tblInvoices.amount) as Amount FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID INNER JOIN tblCompany tblCompany_1 ON tblInvoices.Payee = tblCompany_1.CompanyID INNER JOIN tblCompany ON tblInvoices.Payer = tblCompany.CompanyID inner join tblorder on tblinvoices.orderid=tblorder.orderid WHERE (tblInvoices.dateentered >= '" & fromdate & "') AND (tblInvoices.dateentered < dateadd(d,1,'" & todate & "'))  AND (tblInvoices.InvoiceTypeID = 8) AND (tblInvoices.Payee = 192)"

            If customerno <> "0" Then
                strsql = strsql & " and tblorder.customerno ='" & customerno & "'"
            End If

            strsql = strsql & " group by convert(varchar,dateentered,101)  order by convert(varchar,dateentered,101)"

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    For Each t As LaborPayCredGraph In list
                        If DateDiff(DateInterval.Day, CDate(t.DateEntered), CDate(r("dateentered").ToString)) = 0 Then
                            t.Amount = r("amount").ToString
                        End If
                    Next
                End While
            End Using
        End Using

        'now loop through each and change format
        For Each t As LaborPayCredGraph In list
            Dim thedate As Date = t.DateEntered
            Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
            Dim time As System.DateTime = thedate.Subtract(span)
            t.DateEntered = CLng(time.Ticks \ 10000)
        Next

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetPaymentsToFrom(ByVal companyid As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of PaymentsTofrom)
        Dim js As New JavaScriptSerializer

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT tblInvoices.InvoiceNo as InvoiceNo, tblInvoiceType.InvoiceType, tblInvoices.OrderID, tblPartOrder.PartDescription, tblCompany.Company AS Payee, CASE WHEN [tblcompany_1].[companyid] <> 192 THEN - ABS(tblInvoices.Amount) ELSE [tblinvoices].[amount] END AS Amount, CASE WHEN [tblcompany_1].[companyid] <> 192 THEN - ABS(tblInvoices.Amountpaid) ELSE [tblinvoices].[amountpaid] END AS AmountPaid, convert(varchar,tblInvoices.DatePaid,101) as DatePaid, tblInvoices.PaymentType, tblInvoices.CheckNo, tblCompany.Type, tblInvoiceType.InvoiceTypeID, tblInvoices.InvoiceID, tblPartOrder.PartID, tblCompany_1.Company AS Payer, convert(varchar,tblInvoices.CCDatePaid,101) as CCDatePaid FROM tblInvoices INNER JOIN tblInvoiceType ON tblInvoices.InvoiceTypeID = tblInvoiceType.InvoiceTypeID LEFT OUTER JOIN tblCompany ON tblInvoices.Payee = tblCompany.CompanyID LEFT OUTER JOIN tblCompany tblCompany_1 ON tblInvoices.Payer = tblCompany_1.CompanyID LEFT OUTER JOIN tblPartOrder ON tblInvoices.PartID = tblPartOrder.PartID WHERE ((tblInvoices.DatePaid >= '" & fromdate & "') AND (tblInvoices.DatePaid < dateadd(d,1,'" & todate & "')) AND (tblCompany.ParentCompanyID = " & companyid & ") AND (tblInvoices.Deleted = 0)) OR ((tblInvoices.DatePaid >= '" & fromdate & "') AND (tblInvoices.DatePaid < dateadd(d,1,'" & todate & "')) AND (tblInvoices.Deleted = 0) AND (tblCompany_1.ParentCompanyID =" & companyid & ") ) ORDER BY tblInvoices.DatePaid, tblInvoices.InvoiceNo"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim p1 As New PaymentsTofrom
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
    Public Function GetSalesVolume(ByVal customerno As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of SalesVolume)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT tblCompany.Company, convert(varchar,tblOrder.DateOrdered,101) as DateOrdered, tblOrder.ContractNo, PartDescription, tblPartOrder.SellPrice FROM tblOrder INNER JOIN tblPartOrder ON tblOrder.OrderID = tblPartOrder.OrderID INNER JOIN tblCompany ON tblOrder.CustomerNo = tblCompany.CustomerNo  WHERE (tblCompany.CustomerNO = '" & customerno & "') AND (tblOrder.DateOrdered >= '" & fromdate & "') AND (tblOrder.DateOrdered < dateadd(d,1,'" & todate & "')) AND (tblPartOrder.PreviousPartID = 0) AND (tblPartOrder.Cancelled = 0) AND (tblPartOrder.SellPrice > 0) AND (tblPartOrder.SupplementalPart = 0) ORDER BY tblCompany.Company, tblOrder.DateOrdered DESC"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim s1 As New SalesVolume
                    Dim objType As Type = s1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(s1, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(s1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetTotalPartsGraph(ByVal customerno As String, ByVal parttype As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of TotalPartsGraph)
        Dim js As New JavaScriptSerializer

        'get a list of date range(important for stacked bar graph since all part types might not have sales on the same day

        Dim startDate = CDate(fromdate)
        Dim endDate = CDate(todate)

        Dim dates = Enumerable.Range(0, 1 + CInt((endDate - startDate).TotalDays)).
                               Select(Function(n) startDate.AddDays(n)).
                               ToArray()

        Dim x As Integer = 0
        For Each d As Date In dates
            Dim t1 As New TotalPartsGraph
            t1.DateOrdered = d.ToShortDateString()
            t1.Index = x
            x = x + 1
            list.Add(t1)
        Next


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String
            If customerno = "0" Then
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateEntered,101) as DateOrdered, Count(dbo.tblPartOrder.PartID) AS Parts FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateEntered >='" & fromdate & "' AND dbo.tblPartOrder.DateEntered < DATEADD(d, 1, '" & todate & "')) and parttype='" & parttype & "' GROUP BY convert(varchar,dbo.tblPartOrder.DateEntered,101) order by convert(varchar,dbo.tblPartOrder.DateEntered,101)"
            Else
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateEntered,101) as DateOrdered, Count(dbo.tblPartOrder.PartID) AS Parts FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateEntered >='" & fromdate & "' AND dbo.tblPartOrder.DateEntered < DATEADD(d, 1, '" & todate & "')) and parttype='" & parttype & "' and dbo.tblOrder.CustomerNo = '" & customerno & "' GROUP BY convert(varchar,dbo.tblPartOrder.DateEntered,101) order by convert(varchar,dbo.tblPartOrder.DateEntered,101)"
            End If

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    For Each t As TotalPartsGraph In list
                        If DateDiff(DateInterval.Day, CDate(t.DateOrdered), CDate(r("dateordered").ToString)) = 0 Then
                            t.Parts = r("parts").ToString
                        End If
                    Next
                End While
            End Using
        End Using


        'now loop through each and change format
        For Each t As TotalPartsGraph In list
            Dim thedate As Date = t.DateOrdered
            Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
            Dim time As System.DateTime = thedate.Subtract(span)
            t.DateOrdered = CLng(time.Ticks \ 10000)
        Next

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetTotalPurchasesGraph(ByVal customerno As String, ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of TotalGPGraph)
        Dim js As New JavaScriptSerializer

        'get a list of date range(important for stacked bar graph since all part types might not have sales on the same day

        Dim startDate = CDate(fromdate)
        Dim endDate = CDate(todate)

        Dim dates = Enumerable.Range(0, 1 + CInt((endDate - startDate).TotalDays)).
                               Select(Function(n) startDate.AddDays(n)).
                               ToArray()

        Dim x As Integer = 0
        For Each d As Date In dates
            Dim t1 As New TotalGPGraph
            t1.DateOrdered = d.ToShortDateString()
            't1.Index = x
            x = x + 1
            list.Add(t1)
        Next

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String
            If customerno = "0" Then
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost)) AS Gross FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE tblOrder.Customerno <> '9997C' and (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "')) GROUP BY convert(varchar,dbo.tblPartOrder.DateOrdered,101) order by convert(varchar,dbo.tblPartOrder.DateOrdered,101)"
            Else
                strsql = "SELECT convert(varchar,dbo.tblPartOrder.DateOrdered,101) as DateOrdered, SUM((dbo.tblPartOrder.SellPrice * dbo.tblPartOrder.Quantity + dbo.tblPartOrder.CustShippingPrice + dbo.tblPartOrder.CustCoreShippingPrice + dbo.tblPartOrder.WarrantyCost))  AS Gross FROM  dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo WHERE tblOrder.Customerno <> '9997C' and (dbo.tblPartOrder.Cancelled = 0) AND (dbo.tblPartOrder.PreviousPartID = 0) AND (dbo.tblPartOrder.DateOrdered >='" & fromdate & "' AND dbo.tblPartOrder.DateOrdered < DATEADD(d, 1, '" & todate & "')) and dbo.tblOrder.CustomerNo = '" & customerno & "' GROUP BY convert(varchar,dbo.tblPartOrder.DateOrdered,101) order by convert(varchar,dbo.tblPartOrder.DateOrdered,101)"
            End If

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    For Each t As TotalGPGraph In list
                        If DateDiff(DateInterval.Day, CDate(t.DateOrdered), CDate(r("dateordered").ToString)) = 0 Then
                            t.Gross = r("gross").ToString
                        End If
                    Next

                End While
            End Using
        End Using

        'now loop through each and change format
        For Each t As TotalGPGraph In list
            Dim thedate As Date = t.DateOrdered
            Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
            Dim time As System.DateTime = thedate.Subtract(span)
            t.DateOrdered = CLng(time.Ticks \ 10000)
        Next
        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function GetPaymentsMarked(ByVal fromdate As String, ByVal todate As String, ByVal client As String)
        Dim list As New List(Of PaymentsMarked)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strsql As String = "SELECT     dbo.tblCompany.Company, dbo.tblInvoices.AmountPaid, dbo.tblInvoices.PaymentType, dbo.tblInvoices.CheckNo, dbo.tblInvoices.OrderID FROM         dbo.tblCompany INNER JOIN dbo.tblInvoices ON dbo.tblCompany.CompanyID = dbo.tblInvoices.Payer WHERE (dbo.tblInvoices.InvoiceTypeID = 1 or dbo.tblInvoices.InvoiceTypeID = 5) AND  (tblInvoices.DatePaid >= '" & fromdate & "') AND (tblInvoices.DatePaid < '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "') "

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim i1 As New PaymentsMarked
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
    Public Function CustomerRelat(ByVal fromdate As String, ByVal todate As String, ByVal customerno As String, ByVal client As String)
        Dim list As New List(Of CustRelat)
        Dim js As New JavaScriptSerializer

        'get list of months in betwwen dates

        Try
            Using conn As New SqlConnection(GetClientConnectionString(client))
                Dim strsql As String = "select distinct Year(tblOrder.DateOrdered) as OrderYear, Month(tblOrder.DateOrdered) as OrderMonth, datename(month,tblOrder.DateOrdered) as MonthName, tblOrder.Customerno, tblCompany.Company from tblPartOrder inner join tblOrder on tblPartOrder.OrderID=tblOrder.OrderID inner join tblCompany on tblOrder.CustomerNo=tblCompany.CustomerNo where cancelled=0 and tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "'"

                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim c1 As New CustRelat
                        Dim objType As Type = c1.GetType()
                        Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                        For Each prop As System.Reflection.PropertyInfo In properties
                            Try
                                prop.SetValue(c1, r(prop.Name), Nothing)

                            Catch ex As Exception

                            End Try
                        Next
                        list.Add(c1)
                    End While
                End Using
            End Using

            'now loop through each in list and get other data items
            Dim intAftermarketUnits As Integer = 0
            Dim intAftermarketAmount As Decimal = 0
            Dim intAftermarketReplacementUnits As Integer = 0
            Dim intAftermarketReplacementAmount As Decimal = 0
            Dim intOEMUnits As Integer = 0
            Dim intOEMAmount As Decimal = 0
            Dim intOEMReplacementUnits As Integer = 0
            Dim intOEMReplacementAmount As Decimal = 0
            Dim intSmallUnits As Integer = 0
            Dim intSmallAmount As Decimal = 0
            Dim intSmallReplacementUnits As Integer = 0
            Dim intSmallReplacementAmount As Decimal = 0
            Dim intInspections As Integer = 0

            For Each c As CustRelat In list
                Dim strAftermarket = "select sum(isnull(quantity,1)) as AftermarketUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as AftermarketAmount from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid inner join tblPartDesc on tblPartOrder.PartDescription = tblPartDesc.Description where tblPartOrder.PartType='Aftermarket' and previouspartid =0 and cancelled=0 and tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm As New SqlCommand(strAftermarket, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intAftermarketUnits = If(String.IsNullOrEmpty(r("AftermarketUnits").ToString), 0, r("AftermarketUnits"))
                            intAftermarketAmount = If(String.IsNullOrEmpty(r("AftermarketAmount").ToString), 0, r("AftermarketAmount"))
                        End While
                    End Using

                End Using

                Dim strAftermarketReplacement = "select sum(isnull(quantity,1)) as AftermarketReplacementUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as AftermarketReplacementAmount  from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid inner join tblPartDesc on tblPartOrder.PartDescription = tblPartDesc.Description  where tblPartOrder.PartType='Aftermarket' and previouspartid <>0 and cancelled=0 and tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm As New SqlCommand(strAftermarketReplacement, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intAftermarketReplacementUnits = If(String.IsNullOrEmpty(r("AftermarketReplacementUnits").ToString), 0, r("AftermarketReplacementUnits"))
                            intAftermarketReplacementAmount = If(String.IsNullOrEmpty(r("AftermarketReplacementAmount").ToString), 0, r("AftermarketReplacementAmount"))
                        End While
                    End Using

                End Using

                Dim strOEM = "select sum(isnull(quantity,1)) as OEMUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as OEMAmount from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid where tblPartOrder.PartType='OEM' and previouspartid =0 and cancelled=0 and tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm As New SqlCommand(strOEM, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intOEMUnits = If(String.IsNullOrEmpty(r("OEMUnits").ToString), 0, r("OEMUnits"))
                            intOEMAmount = If(String.IsNullOrEmpty(r("OEMAmount").ToString), 0, r("OEMAmount"))
                        End While
                    End Using

                End Using

                Dim strOEMReplacement = "select sum(isnull(quantity,1)) as OEMReplacementUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as OEMReplacementAmount  from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid where tblPartOrder.PartType='OEM' and previouspartid <>0 and cancelled=0 and tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm As New SqlCommand(strOEMReplacement, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intOEMReplacementUnits = If(String.IsNullOrEmpty(r("OEMReplacementUnits").ToString), 0, r("OEMReplacementUnits"))
                            intOEMReplacementAmount = If(String.IsNullOrEmpty(r("OEMReplacementAmount").ToString), 0, r("OEMReplacementAmount"))
                        End While
                    End Using

                End Using

                Dim strSmall = "select sum(isnull(quantity,1)) as SmallUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as SmallAmount from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid where tblPartOrder.PartType='Small Parts' and previouspartid =0 and cancelled=0 and tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm As New SqlCommand(strSmall, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intSmallUnits = If(String.IsNullOrEmpty(r("SmallUnits").ToString), 0, r("SmallUnits"))
                            intSmallAmount = If(String.IsNullOrEmpty(r("SmallAmount").ToString), 0, r("SmallAmount"))
                        End While
                    End Using

                End Using

                Dim strSmallReplacement = "select sum(isnull(quantity,1)) as SmallReplacementUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as SmallReplacementAmount  from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid where tblPartOrder.PartType='Small Parts' and previouspartid <>0 and cancelled=0 and  tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm As New SqlCommand(strSmallReplacement, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intSmallReplacementUnits = If(String.IsNullOrEmpty(r("SmallReplacementUnits").ToString), 0, r("SmallReplacementUnits"))
                            intSmallReplacementAmount = If(String.IsNullOrEmpty(r("SmallReplacementAmount").ToString), 0, r("SmallReplacementAmount"))
                        End While
                    End Using

                End Using

                Dim strInspections = "select count(requestid) as Inspections from tblInspectionRequest where cancelled is null and customerno='" & If(customerno = "303052", "30305", customerno) & "' and tblinspectionrequest.requestdate >='" & fromdate & "' and tblinspectionrequest.requestdate <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblinspectionrequest.requestdate) = " & c.OrderMonth & " and year(tblinspectionrequest.requestdate) = " & c.OrderYear
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("InspectionJournalConnectionString").ConnectionString)
                    Dim sqlComm As New SqlCommand(strInspections, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            intInspections = r("Inspections")
                        End While
                    End Using

                End Using

                c.AftermarketAmount = intAftermarketAmount + intAftermarketReplacementAmount
                c.AftermarketUnits = intAftermarketUnits '+ intAftermarketReplacementUnits
                c.OEMAmount = intOEMAmount + intOEMReplacementAmount
                c.OEMUnits = intOEMUnits '+ intOEMReplacementUnits
                c.SmallAmount = intSmallAmount + intSmallReplacementAmount
                c.SmallUnits = intSmallUnits '+ intSmallReplacementUnits
                c.TotalAmount = c.AftermarketAmount + c.OEMAmount + c.SmallAmount
                c.TotalUnits = c.AftermarketUnits + c.OEMUnits + c.SmallUnits
                c.Inspections = intInspections

                Dim list2 As New List(Of AftermarketType)
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    'Dim strsql As String = "Select [Group] as PartType, Sum(AftermarketUnits) as Units, sum(AftermarketAmount) as Amount from (select [Group], sum(isnull(quantity,1)) as AftermarketUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as AftermarketAmount from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid inner join tblPartDesc on tblPartOrder.PartDescription = tblPartDesc.Description inner join tblPartDescGroup on tblPartDescGroup.GroupID=tblPartDesc.GroupID where tblPartOrder.PartType='Aftermarket' and previouspartid =0 and cancelled=0 and  tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear & " group by [group] union select [Group], 0 as AftermarketUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as AftermarketAmount from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid inner join tblPartDesc on tblPartOrder.PartDescription = tblPartDesc.Description inner join tblPartDescGroup on tblPartDescGroup.GroupID=tblPartDesc.GroupID where tblPartOrder.PartType='Aftermarket' and previouspartid <>0 and cancelled=0 and  tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear & " group by [group]) as thedata where aftermarketunits >0 group by [group] order by [group] "
                    Dim strsql As String = "Select PartDescGroup as PartType, Sum(AftermarketUnits) as Units, sum(AftermarketAmount) as Amount from (select PartDescGroup, sum(isnull(quantity,1)) as AftermarketUnits, isnull(sum(sellprice * isnull(quantity,1) + custshippingprice + custcoreshippingprice),0) as AftermarketAmount from tblPartOrder inner join tblOrder on tblOrder.OrderID=tblPartOrder.Orderid where tblPartOrder.PartType='Aftermarket' and previouspartid =0 and cancelled=0 and  tblorder.customerno='" & customerno & "' and tblorder.dateordered >='" & fromdate & "' and tblorder.dateordered <  '" & DateAdd(DateInterval.Day, 1, CDate(todate)) & "' and month(tblorder.dateordered) = " & c.OrderMonth & " and year(tblorder.dateordered) = " & c.OrderYear & " group by PartDescGroup ) as thedata group by PartDescGroup order by PartDescGroup "


                    Dim sqlComm As New SqlCommand(strsql, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim a1 As New AftermarketType
                            Dim objType As Type = a1.GetType()
                            Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                            For Each prop As System.Reflection.PropertyInfo In properties
                                Try
                                    prop.SetValue(a1, r(prop.Name), Nothing)

                                Catch ex As Exception

                                End Try
                            Next
                            list2.Add(a1)
                        End While
                    End Using
                End Using

                c.AftermarketList = list2


                Dim thedate As Date = c.OrderMonth.ToString + "/1/" + c.OrderYear.ToString
                Dim span As New System.TimeSpan(System.DateTime.Parse("1/1/1970").Ticks)
                Dim time As System.DateTime = thedate.Subtract(span)
                c.Timestamp = CLng(time.Ticks \ 10000)


            Next

            Return js.Serialize(list)
        Catch ex As Exception
            Return js.Serialize(ex.Message)
        End Try
    End Function

End Class