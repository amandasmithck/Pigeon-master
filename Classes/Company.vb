Public Class Company
    Public Property CompanyID As Integer
    Public Property Company As String
    Public Property CustomerNo As String
    Public Property Type As String
    Public Property Address1 As String
    Public Property Address2 As String
    Public Property City As String
    Public Property State As String
    Public Property Zip As String
    Public Property Phone As String
    Public Property Fax As String
    Public Property Contact As String
    Public Property Email As String
    Public Property WarrantyEmail As String
    Public Property Notes As String
    Public Property Active As Boolean
    Public Property Tax As Boolean
    Public Property DealerGroup As String
    Public Property Region As String
    Public Property Hyperion As String
    Public Property VerifiedEmail As Boolean
    Public Property VAdjuster As Boolean
    Public Property VIP As Boolean
    Public Property OEMShipping As Decimal
    Public Property SmallPartsShipping As Decimal
    Public Property GroundOEMShipping As Decimal
    Public Property GroundSmallPartsShipping As Decimal
    Public Property RebateAmount As Decimal
    Public Property NetShipping As Integer
    Public Property VanityShippingName As String

    Public Sub New()
        Me.Company = String.Empty
        Me.CompanyID = 0
        Me.CustomerNo = String.Empty
        Me.Address1 = String.Empty
        Me.Address2 = String.Empty
        Me.City = String.Empty
        Me.State = String.Empty
        Me.Zip = String.Empty
        Me.Phone = String.Empty
        Me.Fax = String.Empty
        Me.Email = String.Empty
        Me.WarrantyEmail = String.Empty
        Me.Notes = String.Empty
        Me.Active = False
        Me.Tax = False
        Me.DealerGroup = String.Empty
        Me.Region = String.Empty
        Me.Hyperion = String.Empty
        Me.VerifiedEmail = False
        Me.VAdjuster = False
        Me.VIP = False
        OEMShipping = 0
        Me.SmallPartsShipping = 0
        Me.GroundOEMShipping = 0
        Me.GroundSmallPartsShipping = 0
        Me.RebateAmount = 0
        Me.NetShipping = 0
        Me.VanityShippingName = String.Empty

    End Sub
End Class
