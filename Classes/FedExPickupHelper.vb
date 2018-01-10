Option Strict On

Imports System.Web.Services.Protocols
Imports CreatePickupWebServiceClient.CreatePickupWebReference
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Web.Services
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports Pigeon.TransmissionWebService
Imports System.Web.Script.Serialization
Imports Pigeon.OEMWebService
Imports System.Text.RegularExpressions
Imports Pigeon.CKExtensions


Public Class FedExPickupHelper

#Region "Properties"

    Public Property OrderID As String
    Public Property TransactionID As String
    Public Property ErrorMsg As String

    Public Property Contact As Contact
    Public Property Address As Address

    Public Property PackageLocationType As PickupBuildingLocationType

    Public Property BuildingPartCode As BuildingPartCode
    Public Property BuildingPartDescription As String
    Public Property ReadyTimestamp As DateTime?
    Public Property SpecialInstruction As String
    Public Property Weight As Decimal

#End Region

    Public Sub New()    
        ErrorMsg = String.Empty
        OrderID = String.Empty
        PackageLocationType = PickupBuildingLocationType.FRONT
        BuildingPartCode = CreatePickupWebServiceClient.CreatePickupWebReference.BuildingPartCode.BUILDING
        BuildingPartDescription = String.Empty
        SpecialInstruction = String.Empty
        Me.ReadyTimestamp = Nothing
        Me.Weight = 0
    End Sub



#Region "Public Methods"""

    Function CreatePickupRequestReply() As CreatePickupReply
        ' Build the CreatePickupRequest
        Dim request As CreatePickupRequest = New CreatePickupRequest()

        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        request.WebAuthenticationDetail.UserCredential.Key = BaseApplicationVariables.FedExKey.ToString()
        request.WebAuthenticationDetail.UserCredential.Password = BaseApplicationVariables.FedExPassword.ToString()

        request.ClientDetail = New ClientDetail()
        request.ClientDetail.AccountNumber = BaseApplicationVariables.FedExAccountNumber
        request.ClientDetail.MeterNumber = BaseApplicationVariables.FedExMeterNumber

        If isReadyToRetrieveReply() = False Then
            Return Nothing
        End If

        'All good to get request
        request.TransactionDetail = New TransactionDetail()
        request.TransactionDetail.CustomerTransactionId = Me.OrderID

        request.Version = New VersionId()
        ' Origin Information
        request.OriginDetail = New PickupOriginDetail
        request.OriginDetail.PickupLocation = New ContactAndAddress()
        ' Pick up location contact details
        request.OriginDetail.PickupLocation.Contact = Me.Contact

        ' Pick up location Address
        request.OriginDetail.PickupLocation.Address = Me.Address

        If Me.PackageLocationType = PickupBuildingLocationType.NONE Then
            request.OriginDetail.PackageLocationSpecified = False
        Else
            request.OriginDetail.PackageLocationSpecified = True
            request.OriginDetail.PackageLocation = Me.PackageLocationType
        End If

        request.OriginDetail.BuildingPart = Me.BuildingPartCode ' Building PartCode are APARTMENT, BUILDING,DEPARTMENT, SUITE, FLOOR, ROOM

        If Me.BuildingPartDescription.IsNullOrEmpty() = False Then
            request.OriginDetail.BuildingPartDescription = Me.BuildingPartDescription
            request.OriginDetail.BuildingPartSpecified = True
        Else
            request.OriginDetail.BuildingPartSpecified = False
        End If

        request.OriginDetail.ReadyTimestamp = Me.ReadyTimestamp.Value
        request.OriginDetail.ReadyTimestampSpecified = True

        request.OriginDetail.CompanyCloseTimeSpecified = False
        '
        request.PackageCount = "1" ' Number of Packages to pickup
        '
        request.TotalWeight = New Weight() ' All Packages Weight
        request.TotalWeight.Value = Me.Weight
        request.TotalWeight.ValueSpecified = True
        request.TotalWeight.Units = WeightUnits.LB
        request.TotalWeight.UnitsSpecified = True
        '
        request.CarrierCode = CarrierCodeType.FDXG ' CarrierCodeTypes are FDXC(FedEx Cargo), FDXE (Express), FDXG (Ground), FDCC (Custom Critical), FXFR (Freight)
        request.CarrierCodeSpecified = True
        request.Remarks = Me.SpecialInstruction ' Pickup Remarks

        Dim service As PickupService = New PickupService()
        service.Url = BaseApplicationVariables.FedExWebServiceUrl

        Dim reply As CreatePickupReply = service.createPickup(request)

        If (reply.HighestSeverity = NotificationSeverityType.ERROR) OrElse (reply.HighestSeverity = NotificationSeverityType.FAILURE) Then
            GetErrorNotifications(reply)
        End If

        Return reply
    End Function

    Public Function GetPickupConfirmationNumber(ByVal reply As CreatePickupReply) As String
        If reply Is Nothing Then
            Return String.Empty
        End If
        Return reply.PickupConfirmationNumber
    End Function

    Public Function GetPickupLocation(ByVal reply As CreatePickupReply) As String
        If reply Is Nothing Then
            Return String.Empty
        End If
        Return reply.Location
    End Function

    Public Sub SetAddress(ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.Address = New Address()
        Me.Address.StreetLines = New String(0) {street}
        Me.Address.City = city
        Me.Address.StateOrProvinceCode = stateOrProvinceCode
        Me.Address.PostalCode = postalCode
        Me.Address.CountryCode = countryCode
    End Sub

    Public Sub SetContactPerson(ByVal companyName As String, ByVal email As String, ByVal fax As String, ByVal name As String, ByVal phoneNumber As String, ByVal ext As String)
        Me.Contact = New Contact()
        Me.Contact.CompanyName = companyName
        Me.Contact.EMailAddress = email
        Me.Contact.FaxNumber = fax
        Me.Contact.PagerNumber = String.Empty
        Me.Contact.PersonName = name
        Me.Contact.PhoneNumber = phoneNumber
        Me.Contact.PhoneExtension = ext
    End Sub


#End Region

#Region "Private Methods"

    Private Function isReadyToRetrieveReply() As Boolean
        If Me.Contact Is Nothing OrElse Me.Contact.CompanyName.IsNullOrEmpty() OrElse Me.Contact.PersonName.IsNullOrEmpty() OrElse Me.Contact.PhoneNumber.IsNullOrEmpty() Then
            Me.ErrorMsg = "Contact object is missing necessary information."
            Return False

        End If

        If Me.Address Is Nothing OrElse Me.Address.StreetLines Is Nothing OrElse Me.Address.StreetLines.Count = 0 Then
            Me.ErrorMsg = "Address object is missing necessary information."
            Return False
        End If

        If Me.OrderID.IsNullOrEmpty() Then
            Me.ErrorMsg = "Order Id is missing."
            Return False
        End If

        If Me.ReadyTimestamp Is Nothing Then
            Me.ErrorMsg = "Ready Timestamp is not set."
            Return False
        End If

        If Me.Weight = 0 Then
            Me.ErrorMsg = "Weight has not been set"
            Return False
        End If
        Return True

    End Function

    Private Sub GetErrorNotifications(ByRef reply As CreatePickupReply)

        For i As Integer = 0 To reply.Notifications.Length - 1
            Dim notification As Notification = reply.Notifications(i)
            Me.ErrorMsg = Me.ErrorMsg & " " & notification.Message
        Next
    End Sub


    Public Shared Function GetErrorNotification(ByVal reply As CreatePickupReply) As String
        Dim err As String = ""
        For i As Integer = 0 To reply.Notifications.Length - 1
            Dim notification As Notification = reply.Notifications(i)
            err = err & " " & notification.Message
        Next
    End Function
#End Region

    'Function usePropertyFile() As Boolean 'Set to true for common properties to be set with getProperty function.
    '    Return getProperty("usefile").Equals("True")
    'End Function
    'Function getProperty(ByRef propertyname As String) As String 'This function sets common properties for testing purposes.
    '    Try
    '        Dim filename As String = "C:\filepath\filename.txt"
    '        If System.IO.File.Exists(filename) Then
    '            Dim sr As New System.IO.StreamReader(filename)
    '            Do While Not sr.EndOfStream
    '                Dim parts As String() = sr.ReadLine.Split(New Char() {","c})
    '                If (parts(0).Equals(propertyname) And parts.Length = 2) Then
    '                    Return parts(1)
    '                End If
    '            Loop
    '        End If
    '        Console.WriteLine("Property {0} set to default 'XXX'", propertyname)
    '        Return "XXX"
    '    Catch ex As System.Exception
    '        Console.WriteLine("Property {0} set to default 'XXX'", propertyname)
    '        Return "XXX"
    '    End Try
    'End Function

End Class
