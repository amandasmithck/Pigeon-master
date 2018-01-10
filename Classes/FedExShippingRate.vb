
Public Class FedExShippingRate
    Public Property Rate As String
    Public Property ServiceTypeID As Integer
    Public Property ServiceType As String
    Public Property Height As String
    Public Property Width As String
    Public Property Length As String
    Public Property Weight As Decimal
    Public Property DropOffTypeID As Integer
    Public Property DropOffType As String
    Public Property PackageTypeID As Integer
    Public Property PackageType As String

    Public Property WeightUnitID As Integer
    Public Property WeightUnit As String

    Public Property LinearUnitID As Integer
    Public Property LinearUnit As String

    Public Property DeliveryTimestamp As String
    Public Property TransitTime As String


    Public Sub New()
        ServiceTypeID = 0
        ServiceType = String.Empty
        Height = "0"
        Weight = 0
        Length = "0"
        Width = "0"
        DropOffType = String.Empty
        DropOffTypeID = 0
        PackageTypeID = 0
        PackageType = String.Empty
        WeightUnitID = 0
        WeightUnit = String.Empty
        LinearUnit = String.Empty
        LinearUnitID = 0
        Rate = "0"
        DeliveryTimestamp = String.Empty
        TransitTime = String.Empty
    End Sub


End Class
