
Public Class FedExPackageDetail
    Public Property TrackingNumber As String
    Public Property RateType As String
    Public Property BillingWeight As String
    Public Property BaseCharge As String
    Public Property NetCharge As String
    Public Property FuelSurcharge As String
    Public Property TotalSurcharge As String
    Public Property Barcode As String
    Public Property BarcodeType As String
    Public Property BinaryBarcodeType As String
    Public Property TransitTime As String
    Public Property ServiceTypeID As String
    Public Property ServiceType As String
    Public Property PartsList As String

    Public Property DeliveryDate As String
    Public Property DeliveryDay As String
    Public Property FileName As String

    Public Property PickupConfirmationNumber As String
    Public Property IsScheduledPickup As Boolean

    Public Property PickupLocation As String
    Public Property ShipmentMode As ShipmentModes

    Public Sub New()
        TrackingNumber = String.Empty
        RateType = String.Empty
        BillingWeight = String.Empty
        BaseCharge = String.Empty
        NetCharge = String.Empty
        FuelSurcharge = String.Empty
        TotalSurcharge = "String.Empty"
        Barcode = String.Empty
        BinaryBarcodeType = String.Empty
        BarcodeType = String.Empty
        TransitTime = String.Empty
        ServiceType = String.Empty
        ServiceTypeID = String.Empty
        PartsList = String.Empty
        Me.DeliveryDate = String.Empty
        Me.DeliveryDay = String.Empty
        FileName = String.Empty
        PickupConfirmationNumber = String.Empty
        IsScheduledPickup = False
        PickupLocation = String.Empty
        ShipmentMode = ShipmentModes.None
    End Sub


End Class
