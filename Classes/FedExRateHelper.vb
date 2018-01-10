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
Public Class FedExRateHelper

#Region "Properties"
    Public Property DropOffType As DropoffType
    Public Property ServiceType As ServiceType
    Public Property PackagingType As PackagingType

    Public Property ShipperAddress As Address
    Public Property Origin As ContactAndAddress
    Public Property DestinationAddress As Address

    Private _requestedPackageLineItems As List(Of RequestedPackageLineItem)
    Public Property RequestedPackageLineItems As List(Of RequestedPackageLineItem)
        Get
            If _requestedPackageLineItems Is Nothing Then
                _requestedPackageLineItems = New List(Of RequestedPackageLineItem)()
            End If
            Return _requestedPackageLineItems
        End Get
        Set(value As List(Of RequestedPackageLineItem))
            _requestedPackageLineItems = value
        End Set
    End Property

    Public Property OrderID As String


    Public Property ErrorMsg As String

#End Region

    Public Sub New()
        Me.DropOffType = RateWebServiceClient.RateWebReference.DropoffType.REGULAR_PICKUP
        Me.ServiceType = RateWebServiceClient.RateWebReference.ServiceType.FEDEX_2_DAY
        Me.PackagingType = RateWebServiceClient.RateWebReference.PackagingType.YOUR_PACKAGING
        Me.ShipperAddress = New Address()
        Me.DestinationAddress = New Address()
        Me.OrderID = String.Empty
    End Sub



#Region "Public Methods"""

    Public Function GetRateReply() As RateReply
        If Me.isReadyToRetrieveReply = False Then
            Me.ErrorMsg = "Object is not filled."
            Return Nothing
        End If

        'got necessary data
        Dim request As RateRequest = CreateRateRequest()
        Dim service As RateService = New RateService()
        service.Url = BaseApplicationVariables.FedExWebServiceUrl

        Try
            'Attempt to call the web service by passing in a raterequest and returning a rate reply
            Dim rateReply As RateReply = New RateReply()

            rateReply = service.getRates(request)

            If rateReply.HighestSeverity = NotificationSeverityType.ERROR OrElse rateReply.HighestSeverity = NotificationSeverityType.FAILURE Then
                'something went wrong
                Me.ErrorMsg = rateReply.Notifications(0).Message.ToString() & "- Severity Level is " & Convert.ToInt32(rateReply.Notifications(0).Severity)
            End If

            Return rateReply
        Catch e As SoapException
            Me.ErrorMsg = e.Message.ToString()
            Return Nothing
        Catch e As Exception
            Me.ErrorMsg = e.Message.ToString()
            Return Nothing
        End Try
    End Function

    Public Sub SetShipmentDetailData(ByVal dropOffType As DropoffType, ByVal serviceType As ServiceType, ByVal packageType As PackagingType)
        Me.DropOffType = dropOffType
        Me.ServiceType = serviceType
        Me.PackagingType = packageType

    End Sub
    Public Sub SetDestinationAddress(ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.DestinationAddress = New Address()
        Me.DestinationAddress.StreetLines = New String(0) {street}
        Me.DestinationAddress.City = city
        Me.DestinationAddress.StateOrProvinceCode = stateOrProvinceCode
        Me.DestinationAddress.PostalCode = postalCode
        Me.DestinationAddress.CountryCode = countryCode
    End Sub

    Public Sub SetOrigin(ByVal companyName As String, ByVal email As String, ByVal fax As String, ByVal name As String, ByVal phoneNumber As String, ByVal ext As String, ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.Origin = New ContactAndAddress()
        Me.Origin.Contact = New Contact()
        Me.Origin.Contact.CompanyName = companyName
        Me.Origin.Contact.EMailAddress = email
        Me.Origin.Contact.FaxNumber = fax
        Me.Origin.Contact.PersonName = name
        Me.Origin.Contact.PhoneNumber = phoneNumber
        Me.Origin.Contact.PhoneExtension = ext
        Me.Origin.Address = New Address()
        Me.Origin.Address.StreetLines = New String(0) {street}
        Me.Origin.Address.City = city
        Me.Origin.Address.StateOrProvinceCode = stateOrProvinceCode
        Me.Origin.Address.PostalCode = postalCode
        Me.Origin.Address.CountryCode = countryCode
    End Sub

    Public Sub SetShipperAddress(ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.ShipperAddress = New Address()
        Me.ShipperAddress.StreetLines = New String(0) {street}
        Me.ShipperAddress.City = city
        Me.ShipperAddress.StateOrProvinceCode = stateOrProvinceCode
        Me.ShipperAddress.PostalCode = postalCode
        Me.ShipperAddress.CountryCode = countryCode
    End Sub

    Public Sub AddPackageLineItem(ByVal itemWeight As Decimal, ByVal weightUnit As WeightUnits, ByVal length As String, ByVal width As String, ByVal height As String, ByVal dimensionalUnit As LinearUnits)
        Dim item As New RequestedPackageLineItem()
        item.SequenceNumber = Me.RequestedPackageLineItems.Count + 1
        item.GroupPackageCount = Me.RequestedPackageLineItems.Count + 1

        Dim weight As New Weight()
        weight.Units = weightUnit
        weight.UnitsSpecified = True
        weight.Value = itemWeight
        weight.ValueSpecified = True
        item.Weight = weight

        'If length.IsNullOrEmpty() = False AndAlso width.IsNullOrEmpty() = False AndAlso height.IsNullOrEmpty() = False Then
        Dim dimensions As New Dimensions()
        Dimensions.Length = length
        Dimensions.Height = height
        Dimensions.Width = width
        Dimensions.Units = dimensionalUnit
        Dimensions.UnitsSpecified = True
        item.Dimensions = Dimensions
        'End If


        Me.RequestedPackageLineItems.Add(item)
    End Sub
    Public Sub AddPackageLineItem(ByVal itemWeight As Decimal, ByVal weightUnit As WeightUnits)
        Dim item As New RequestedPackageLineItem()
        item.SequenceNumber = Me.RequestedPackageLineItems.Count + 1
        item.GroupPackageCount = Me.RequestedPackageLineItems.Count + 1

        Dim weight As New Weight()
        weight.Units = weightUnit
        weight.UnitsSpecified = True
        weight.Value = itemWeight
        weight.ValueSpecified = True
        item.Weight = weight

        Me.RequestedPackageLineItems.Add(item)
    End Sub


    Public Function GetRateReplyDetail(ByVal rateReply As RateReply) As RateReplyDetail
        'derive the rate reply detail from the rate reply object
        If rateReply Is Nothing OrElse rateReply.RateReplyDetails Is Nothing OrElse rateReply.RateReplyDetails.Count = 0 Then
            Return Nothing
        End If

        'so far so good
        Dim rateReplyDetail As RateReplyDetail = rateReply.RateReplyDetails(0)
        Return rateReplyDetail
    End Function


    Public Function GetRatedShipmentDetail(ByVal rateReply As RateReply) As RatedShipmentDetail
        'derive the rated shipment detail from the rate reply object
        If rateReply Is Nothing OrElse rateReply.RateReplyDetails Is Nothing OrElse rateReply.RateReplyDetails.Count = 0 Then
            Return Nothing
        End If

        'so far so good
        Dim rateReplyDetail As RateReplyDetail = rateReply.RateReplyDetails(0)

        If rateReplyDetail Is Nothing OrElse rateReplyDetail.RatedShipmentDetails Is Nothing OrElse rateReplyDetail.RatedShipmentDetails.Count = 0 Then
            Return Nothing
        End If

        Dim ratedShipmentDetail As RatedShipmentDetail = rateReplyDetail.RatedShipmentDetails(0)
        If ratedShipmentDetail Is Nothing Then
            Return Nothing
        End If

        Return ratedShipmentDetail
    End Function


    Public Function GetShipmentRateDetail(ByVal rateReply As RateReply) As ShipmentRateDetail
        'derive the rated shipment detail from the rate reply object
        If rateReply Is Nothing OrElse rateReply.RateReplyDetails Is Nothing OrElse rateReply.RateReplyDetails.Count = 0 Then
            Return Nothing
        End If

        'so far so good
        Dim rateReplyDetail As RateReplyDetail = rateReply.RateReplyDetails(0)

        If rateReplyDetail Is Nothing OrElse rateReplyDetail.RatedShipmentDetails Is Nothing OrElse rateReplyDetail.RatedShipmentDetails.Count = 0 Then
            Return Nothing
        End If

        Dim ratedShipmentDetail As RatedShipmentDetail = rateReplyDetail.RatedShipmentDetails(0)

        If ratedShipmentDetail Is Nothing OrElse ratedShipmentDetail.ShipmentRateDetail Is Nothing Then
            Return Nothing
        End If


        Return ratedShipmentDetail.ShipmentRateDetail
    End Function



#End Region

#Region "Private Methods"

    Private Function isReadyToRetrieveReply() As Boolean
        If Me.DropOffType = Nothing OrElse Me.ServiceType = Nothing OrElse Me.PackagingType = Nothing Then
            Return False
        End If

        If Me.ShipperAddress Is Nothing OrElse Me.DestinationAddress Is Nothing Then
            Return False
        End If

        If Me.RequestedPackageLineItems Is Nothing OrElse Me.RequestedPackageLineItems.Count = 0 Then
            Return False
        End If

        If Me.OrderID.IsNullOrEmpty() Then
            Return False
        End If

        Return True

    End Function

    Private Function CreateRateRequest() As RateRequest
        ' Build a RateRequest
        Dim request As RateRequest = New RateRequest()
        '
        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        request.WebAuthenticationDetail.UserCredential.Key = BaseApplicationVariables.FedExKey.ToString()
        request.WebAuthenticationDetail.UserCredential.Password = BaseApplicationVariables.FedExPassword.ToString()

        request.ClientDetail = New ClientDetail()
        request.ClientDetail.AccountNumber = BaseApplicationVariables.FedExAccountNumber
        request.ClientDetail.MeterNumber = BaseApplicationVariables.FedExMeterNumber

        request.TransactionDetail = New TransactionDetail()
        request.TransactionDetail.CustomerTransactionId = Me.OrderID

        request.Version = New VersionId() ' WSDL version information, value is automatically set from wsd


        request.ReturnTransitAndCommit = True
        request.ReturnTransitAndCommitSpecified = True

        'If Me.DropOffType = RateWebServiceClient.RateWebReference.DropoffType.REQUEST_COURIER Then
        '    request.RequestedShipment.PickupDetail = New PickupDetail()
        '    request.RequestedShipment.PickupDetail.RequestType = PickupRequestType.FUTURE_DAY
        '    request.RequestedShipment.PickupDetail.RequestTypeSpecified = True
        '    request.RequestedShipment.PickupDetail.RequestSource = PickupRequestSourceType.AUTOMATION
        'End If


        SetShipmentDetails(request)

        '
        Return request
    End Function

    Private Sub SetShipmentDetails(ByRef request As RateRequest)
        request.RequestedShipment = New RequestedShipment()
        request.RequestedShipment.ShipTimestamp = DateTime.Now ' Ship date and time
        request.RequestedShipment.ShipTimestampSpecified = True
        request.RequestedShipment.DropoffType = Me.DropOffType
        request.RequestedShipment.ServiceType = Me.ServiceType
        request.RequestedShipment.ServiceTypeSpecified = True
        request.RequestedShipment.PackagingType = Me.PackagingType
        request.RequestedShipment.PackagingTypeSpecified = True

        SetShipper(request)
        SetDestination(request)
        If DropOffType = RateWebServiceClient.RateWebReference.DropoffType.REQUEST_COURIER Then
            SetOrigin(request)
        End If
        SetPackageLineItems(request)

        request.RequestedShipment.TotalInsuredValue = New Money()
        request.RequestedShipment.TotalInsuredValue.Amount = 100
        request.RequestedShipment.TotalInsuredValue.Currency = "USD"

        request.RequestedShipment.PackageCount = Me.RequestedPackageLineItems.Count

    End Sub

    Private Sub SetOrigin(ByRef request As RateRequest)
        request.RequestedShipment.Origin = New ContactAndAddress()
        request.RequestedShipment.Origin = Me.Origin

    End Sub

    Private Sub SetShipper(ByRef request As RateRequest)
        request.RequestedShipment.Shipper = New Party()
        request.RequestedShipment.Shipper.Address = Me.ShipperAddress

    End Sub

    Private Sub SetDestination(ByRef request As RateRequest)
        request.RequestedShipment.Recipient = New Party()
        request.RequestedShipment.Recipient.Address = Me.DestinationAddress

    End Sub

    Private Function SetPackageLineItems(ByRef request As RateRequest) As Boolean
        If Me.RequestedPackageLineItems Is Nothing AndAlso Me.RequestedPackageLineItems.Count = 0 Then
            Return False
        End If

        Dim i As Integer = 0
        For Each RequestedPackageLineItem In Me.RequestedPackageLineItems
            request.RequestedShipment.RequestedPackageLineItems = New RequestedPackageLineItem(i) {}
            request.RequestedShipment.RequestedPackageLineItems(i) = Me.RequestedPackageLineItems(i)
            request.RequestedShipment.RequestedPackageLineItems(i).SequenceNumber = (i + 1)
            i = i + 1
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
