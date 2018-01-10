﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports Pigeon.TransmissionWebService
Imports System.Web.Script.Serialization
Imports Pigeon.OEMWebService
Imports System.Text.RegularExpressions
Imports ShipWebServiceClient.ShipServiceWebReference
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports Pigeon.CKExtensions
Imports System.Resources

Public Class FedExGroundShippingHelper
#Region "Properties"
    Public Property DropOffType As DropoffType
    Public Property ServiceType As ServiceType
    Public Property PackagingType As PackagingType

    Public Property OriginContact As Contact
    Public Property OriginAddress As Address
    Public Property ShipperAddress As Address
    Public Property ShipperContact As Contact
    Public Property DestinationAddress As Address

    Public Property DestinationContact As Contact

    Public Property isCashOnDelivery As Boolean

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
    Public Property FilePath As String
    Public Property ErrorMsg As String
#End Region

    Public Sub New()
        Me.DropOffType = RateWebServiceClient.RateWebReference.DropoffType.REGULAR_PICKUP
        Me.ServiceType = RateWebServiceClient.RateWebReference.ServiceType.FEDEX_2_DAY
        Me.PackagingType = RateWebServiceClient.RateWebReference.PackagingType.FEDEX_TUBE
        Me.ShipperAddress = New Address()
        Me.DestinationAddress = New Address()
        Me.DestinationContact = New Contact()
        Me.ShipperContact = New Contact()
        Me.isCashOnDelivery = False
        Me.OrderID = String.Empty
    End Sub

    Public Function SendShipmentRequest() As ProcessShipmentReply

        If Me.isReadyToRetrieveReply() = False Then
            Me.ErrorMsg = "Necessary data has not been added to object to send request."
            Return Nothing
        End If

        ' Set this to true to process a COD shipment and print a COD return Label
        Dim isCodShipment As Boolean = Me.isCashOnDelivery
        Dim request As ProcessShipmentRequest = CreateShipmentRequest(isCodShipment)

        Dim service As ShipService = New ShipService() ' Initialize the service
        service.Url = BaseApplicationVariables.FedExWebServiceUrl


        Try
            ' Call the ship web service passing in a ProcessShipmentRequest and returning a ProcessShipmentReply
            Dim reply As ProcessShipmentReply = service.processShipment(request)

            If reply.HighestSeverity = NotificationSeverityType.ERROR OrElse reply.HighestSeverity = NotificationSeverityType.FAILURE Then
                Dim sbNotification As New System.Text.StringBuilder()

                For Each notification As Notification In reply.Notifications
                    sbNotification.Append(notification.Message & " ")
                Next
                Me.ErrorMsg = sbNotification.ToString()
                Return Nothing
            End If

            'all good
            '  ShowShipmentReply(reply, isCodShipment)
            'Dim labels As List(Of System.Drawing.Image) = GetCollectionOfLabels(reply)
            'Dim label As System.Drawing.Image = labels(0)
            'Dim ms As New System.IO.MemoryStream()
            'label.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            Return reply

        Catch e As SoapException
            Console.WriteLine(e.Detail.InnerText)
            Me.ErrorMsg = e.Detail.InnerText
            Return Nothing
        Catch e As Exception
            Console.WriteLine(e.Message)
            Me.ErrorMsg = e.Message
            Return Nothing
        End Try

    End Function


    Public Sub SetShipmentDetailData(ByVal dropOffType As DropoffType, ByVal serviceType As ServiceType, ByVal packageType As PackagingType, ByVal isCashOnDelivery As Boolean)
        Me.DropOffType = dropOffType
        Me.ServiceType = serviceType
        Me.PackagingType = packageType
        Me.isCashOnDelivery = isCashOnDelivery
    End Sub

    Public Sub SetDestinationAddress(ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.DestinationAddress = New Address()
        Me.DestinationAddress.StreetLines = New String(0) {street}
        Me.DestinationAddress.City = city
        Me.DestinationAddress.StateOrProvinceCode = stateOrProvinceCode
        Me.DestinationAddress.PostalCode = postalCode
        Me.DestinationAddress.CountryCode = countryCode
    End Sub


    Public Sub SetDestinationContact(ByVal name As String, ByVal companyName As String, ByVal phoneNumber As String)
        Me.DestinationContact = New Contact()
        Me.DestinationContact.PersonName = name
        Me.DestinationContact.CompanyName = companyName
        Me.DestinationContact.PhoneNumber = phoneNumber
    End Sub

    Public Sub SetOriginAddress(ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.OriginAddress = New Address()
        Me.OriginAddress.StreetLines = New String(0) {street}
        Me.OriginAddress.City = city
        Me.OriginAddress.StateOrProvinceCode = stateOrProvinceCode
        Me.OriginAddress.PostalCode = postalCode
        Me.OriginAddress.CountryCode = countryCode
    End Sub


    Public Sub SetOriginContact(ByVal companyName As String, ByVal email As String, ByVal fax As String, ByVal name As String, ByVal phoneNumber As String, ByVal ext As String)
        Me.OriginContact = New Contact()
        Me.OriginContact.CompanyName = companyName
        Me.OriginContact.EMailAddress = email
        Me.OriginContact.FaxNumber = fax
        Me.OriginContact.PagerNumber = String.Empty
        Me.OriginContact.PersonName = name
        Me.OriginContact.PhoneNumber = phoneNumber
        Me.OriginContact.PhoneExtension = ext
    End Sub


    Public Sub SetShipperAddress(ByVal street As String, ByVal city As String, ByVal stateOrProvinceCode As String, ByVal postalCode As String, ByVal countryCode As String)
        Me.ShipperAddress = New Address()
        Me.ShipperAddress.StreetLines = New String(0) {street}
        Me.ShipperAddress.City = city
        Me.ShipperAddress.StateOrProvinceCode = stateOrProvinceCode
        Me.ShipperAddress.PostalCode = postalCode
        Me.ShipperAddress.CountryCode = countryCode
    End Sub


    Public Sub SetShipperContact(ByVal name As String, ByVal companyName As String, ByVal phoneNumber As String)
        Me.ShipperContact = New Contact()
        Me.ShipperContact.PersonName = name
        Me.ShipperContact.CompanyName = companyName
        Me.ShipperContact.PhoneNumber = phoneNumber
    End Sub

    Public Sub AddPackageLineItem(ByVal itemWeight As Decimal, ByVal weightUnit As WeightUnits, ByVal length As String, ByVal width As String, ByVal height As String, ByVal dimensionalUnit As LinearUnits, ByVal listOfCustomerReference As List(Of CustomerReference))
        Dim item As New RequestedPackageLineItem()
        item.SequenceNumber = Me.RequestedPackageLineItems.Count + 1
        item.GroupPackageCount = Me.RequestedPackageLineItems.Count + 1

        Dim weight As New Weight()
        weight.Units = weightUnit
        weight.Value = itemWeight
        item.Weight = weight

        Dim dimensions As New Dimensions()
        dimensions.Length = length
        dimensions.Height = height
        dimensions.Width = width
        dimensions.Units = dimensionalUnit
        item.Dimensions = dimensions

        ' item.CustomerReferences =
        item.CustomerReferences = listOfCustomerReference.ToArray()

        Me.RequestedPackageLineItems.Add(item)
    End Sub

    Public Function GetPackageDetails(ByVal reply As ProcessShipmentReply) As List(Of FedExPackageDetail)
        If reply Is Nothing Then
            Return Nothing
        End If

        If reply.CompletedShipmentDetail.CompletedPackageDetails Is Nothing OrElse reply.CompletedShipmentDetail.CompletedPackageDetails.Count = 0 Then
            Return Nothing
        End If

        Dim listOfFedExPackageDetails As New List(Of FedExPackageDetail)

        Dim packageDetail As New CompletedPackageDetail()
        packageDetail = reply.CompletedShipmentDetail.CompletedPackageDetails(0)

        'If reply.CompletedShipmentDetail.CompletedPackageDetails.Count >= 1 Then
        Dim fedExPackageDetail As New FedExPackageDetail()
        GetTrackingDetails(packageDetail.TrackingIds, fedExPackageDetail)
        GetPackageRateDetails(packageDetail.PackageRating, GetActualRateType(reply.CompletedShipmentDetail), fedExPackageDetail)
        GetBarcodeDetails(packageDetail.OperationalDetail.Barcodes, fedExPackageDetail)
        GetPackageRouteDetails(reply.CompletedShipmentDetail.OperationalDetail, fedExPackageDetail)
        ShowShipmentLabels(reply.CompletedShipmentDetail, packageDetail, fedExPackageDetail)

        listOfFedExPackageDetails.Add(fedExPackageDetail)
        Return listOfFedExPackageDetails
        'End If



        'For Each detail As CompletedPackageDetail In reply.CompletedShipmentDetail.CompletedPackageDetails
        '    Dim fedExDetail As New FedExPackageDetail()
        '    GetTrackingDetails(packageDetail.TrackingIds, fedExDetail)
        '    GetPackageRateDetails(packageDetail.PackageRating, GetActualRateType(reply.CompletedShipmentDetail), fedExDetail)
        '    ShowBarcodeDetails(packageDetail.OperationalDetail.Barcodes, fedExDetail)
        'ShowShipmentLabels(reply.CompletedShipmentDetail, packageDetail, fedExDetail)
        'listOfFedExPackageDetails.Add(fedExDetail)
        'Next
        'Return listOfFedExPackageDetails

    End Function

#Region "Private Methods"

    Private Function CreateShipmentRequest(ByVal isCodShipment As Boolean) As ProcessShipmentRequest
        ' Build a ProcessShipmentRequest
        Dim request As ProcessShipmentRequest = New ProcessShipmentRequest()

        SetShipmentDetails(request)
        '
        SetPackageLineItems(request, isCodShipment)
        '
        Return request
    End Function

    Private Sub SetShipmentDetails(ByRef request As ProcessShipmentRequest)
        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()

        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        request.WebAuthenticationDetail.UserCredential.Key = BaseApplicationVariables.FedExKey
        request.WebAuthenticationDetail.UserCredential.Password = BaseApplicationVariables.FedExPassword

        If (usePropertyFile()) Then 'Set values from a file for testing purposes
            request.WebAuthenticationDetail.UserCredential.Key = getProperty("key")
            request.WebAuthenticationDetail.UserCredential.Password = getProperty("password")
        End If
        '
        request.ClientDetail = New ClientDetail()
        request.ClientDetail.AccountNumber = BaseApplicationVariables.FedExAccountNumber
        request.ClientDetail.MeterNumber = BaseApplicationVariables.FedExMeterNumber

        If (usePropertyFile()) Then 'Set values from a file for testing purposes
            request.ClientDetail.AccountNumber = getProperty("accountnumber")
            request.ClientDetail.MeterNumber = getProperty("meternumber")
        End If

        '
        request.TransactionDetail = New TransactionDetail()
        request.TransactionDetail.CustomerTransactionId = "Ground Shipping " & Me.OrderID

        request.Version = New VersionId() ' WSDL version information, value is automatically set from wsdl
        '
        request.RequestedShipment = New RequestedShipment()
        request.RequestedShipment.ShipTimestamp = DateTime.Now ' Ship date and time
        request.RequestedShipment.DropoffType = Me.DropOffType
        request.RequestedShipment.ServiceType = Me.ServiceType
        request.RequestedShipment.PackagingType = Me.PackagingType


        request.RequestedShipment.TotalWeight = New Weight() ' Total weight information
        request.RequestedShipment.TotalWeight.Value = Me.RequestedPackageLineItems.TotalWeight()
        request.RequestedShipment.TotalWeight.Units = Me.RequestedPackageLineItems(0).Weight.Units
        '
        request.RequestedShipment.PackageCount = Me.RequestedPackageLineItems.Count.ToString()
        '
        SetSender(request)
        '
        SetRecipient(request)
        '
        SetPayment(request)

        If DropOffType = ShipWebServiceClient.ShipServiceWebReference.DropoffType.REQUEST_COURIER Then
            Me.SetOrigin(request)
        End If
        '
        SetLabelDetails(request)
    End Sub

    Private Sub SetSender(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.Shipper = New Party()
        request.RequestedShipment.Shipper.Contact = New Contact()
        request.RequestedShipment.Shipper.Contact = ShipperContact
        request.RequestedShipment.Shipper.Address = New Address()
        request.RequestedShipment.Shipper.Address = Me.ShipperAddress

    End Sub

    Private Sub SetRecipient(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.Recipient = New Party()
        request.RequestedShipment.Recipient.Contact = New Contact()
        request.RequestedShipment.Recipient.Contact = DestinationContact

        request.RequestedShipment.Recipient.Address = New Address()
        request.RequestedShipment.Recipient.Address = DestinationAddress

    End Sub

    Private Sub SetOrigin(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.Origin = New ContactAndAddress()
        request.RequestedShipment.Origin.Contact = New Contact()
        request.RequestedShipment.Origin.Contact = Me.OriginContact
        request.RequestedShipment.Origin.Address = New Address()
        request.RequestedShipment.Origin.Address = Me.OriginAddress
    End Sub
    Private Sub SetPayment(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.ShippingChargesPayment = New Payment()
        request.RequestedShipment.ShippingChargesPayment.PaymentType = PaymentType.SENDER
        request.RequestedShipment.ShippingChargesPayment.Payor = New Payor()
        request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty = New Party()
        request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = BaseApplicationVariables.FedExAccountNumber
        If (usePropertyFile()) Then 'Set values from a file for testing purposes
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = getProperty("payoraccount")
        End If
        request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Contact = New Contact()
        request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Address = New Address()
        request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Address.CountryCode = "US"
    End Sub

    Private Sub SetLabelDetails(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.LabelSpecification = New LabelSpecification()
        request.RequestedShipment.LabelSpecification.LabelFormatType = LabelFormatType.COMMON2D ' COMMON2D, LABEL_DATA_ONLY
        request.RequestedShipment.LabelSpecification.ImageType = ShippingDocumentImageType.PDF ' Image types PDF, PNG, DPL, ...
        request.RequestedShipment.LabelSpecification.ImageTypeSpecified = True
    End Sub

    Private Sub SetPackageLineItems(ByRef request As ProcessShipmentRequest, ByVal isCodShipment As Boolean)
        If Me.RequestedPackageLineItems Is Nothing AndAlso Me.RequestedPackageLineItems.Count = 0 Then
            Return
        End If

        Dim i As Integer = 0
        For Each RequestedPackageLineItem In Me.RequestedPackageLineItems
            request.RequestedShipment.RequestedPackageLineItems = New RequestedPackageLineItem(i) {}
            request.RequestedShipment.RequestedPackageLineItems(i) = Me.RequestedPackageLineItems(i)
            request.RequestedShipment.RequestedPackageLineItems(i).SequenceNumber = (i + 1)
            i = i + 1
        Next

        If (isCodShipment) Then
            SetCOD(request)
        End If
    End Sub

    Private Sub SetCOD(ByRef request As ProcessShipmentRequest)
        request.RequestedShipment.SpecialServicesRequested = New ShipmentSpecialServicesRequested() ' Special service requested
        request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = New ShipmentSpecialServiceType(0) {ShipmentSpecialServiceType.COD} ' Special Services types COD, HOLD_AT_LOCATION, RESIDENTIAL DELIVERY, ...
        '
        request.RequestedShipment.SpecialServicesRequested.CodDetail = New CodDetail()
        request.RequestedShipment.SpecialServicesRequested.CodDetail.CollectionType = CodCollectionType.GUARANTEED_FUNDS ' ANY, CASH, GUARANTEED_FUNDS
        request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount = New Money()
        request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.Amount = 250
        request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.Currency = "USD"
    End Sub

    'Private Sub ShowShipmentReply(ByRef reply As ProcessShipmentReply, ByVal isCodShipment As Boolean)

    '    ' Details for each package
    '    'ShowShipmentRateDetails(reply.CompletedShipmentDetail.ShipmentRating, GetActualRateType(reply.CompletedShipmentDetail))
    '    For Each packageDetail As CompletedPackageDetail In reply.CompletedShipmentDetail.CompletedPackageDetails
    '        'ShowTrackingDetails(packageDetail.TrackingIds)
    '        'ShowPackageRateDetails(packageDetail.PackageRating, GetActualRateType(reply.CompletedShipmentDetail))
    '        'ShowBarcodeDetails(packageDetail.OperationalDetail.Barcodes)
    '        ShowShipmentLabels(reply.CompletedShipmentDetail, packageDetail, isCodShipment, False)
    '    Next
    '    'ShowPackageRouteDetails(reply.CompletedShipmentDetail.OperationalDetail)
    'End Sub

    Private Sub ShowTrackingDetails(ByRef TrackingIds() As TrackingId)
        ' Tracking information for each package
        Console.WriteLine()
        Console.WriteLine("Tracking details")
        If (TrackingIds IsNot Nothing) Then
            For Each trackingId As TrackingId In TrackingIds
                Console.WriteLine("Tracking # {0} Form ID {1}", trackingId.TrackingNumber, trackingId.FormId)
            Next
        End If
    End Sub

    Private Function GetActualRateType(ByRef CompletedShipmentDetail As CompletedShipmentDetail) As ReturnedRateType
        If (CompletedShipmentDetail.ShipmentRating IsNot Nothing) Then
            If (CompletedShipmentDetail.ShipmentRating.ActualRateTypeSpecified) Then

                Return CompletedShipmentDetail.ShipmentRating.ActualRateType
            End If
        End If
        Return Nothing
    End Function

    Private Sub ShowShipmentRateDetails(ByRef ShipmentRating As ShipmentRating, ByRef ActualRateType As ReturnedRateType)
        If (ShipmentRating IsNot Nothing) Then
            Dim ShipmentRateDetails As ShipmentRateDetail() = ShipmentRating.ShipmentRateDetails
            If (ShipmentRateDetails IsNot Nothing) Then
                For Each ratedShipment As ShipmentRateDetail In ShipmentRateDetails
                    If (ratedShipment IsNot Nothing) Then
                        If (ratedShipment.RateTypeSpecified) Then
                            If (ratedShipment.RateType = ActualRateType) Then
                                Console.WriteLine()
                                Console.WriteLine("Shipment Rate details")
                                Console.WriteLine("Rate Type {0}", ratedShipment.RateType)
                                If (ratedShipment.TotalBillingWeight IsNot Nothing) Then
                                    Console.WriteLine("Billing weight {0} {1}", ratedShipment.TotalBillingWeight.Value, ratedShipment.TotalBillingWeight.Units)
                                End If

                                If (ratedShipment.TotalBaseCharge IsNot Nothing) Then
                                    Console.WriteLine("Base charge {0} {1}", ratedShipment.TotalBaseCharge.Amount, ratedShipment.TotalBaseCharge.Currency)
                                End If

                                If (ratedShipment.TotalNetCharge IsNot Nothing) Then
                                    Console.WriteLine("Net charge {0} {1}", ratedShipment.TotalNetCharge.Amount, ratedShipment.TotalNetCharge.Currency)
                                End If

                                If (ratedShipment.Surcharges IsNot Nothing) Then
                                    ' Individual surcharge for each package
                                    For Each surcharge As Surcharge In ratedShipment.Surcharges
                                        Console.WriteLine("{0} surcharge {1} {2}", surcharge.SurchargeType, surcharge.Amount.Amount, surcharge.Amount.Currency)
                                    Next
                                End If

                                If (ratedShipment.TotalSurcharges IsNot Nothing) Then
                                    Console.WriteLine("Total surcharge {0} {1}", ratedShipment.TotalSurcharges.Amount, ratedShipment.TotalSurcharges.Currency)
                                End If
                            End If
                        End If
                    End If
                Next ratedShipment
            End If
        End If
    End Sub
    Private Sub ShowPackageRateDetails(ByRef PackageRating As PackageRating, ByRef ActualRateType As ReturnedRateType)
        If (PackageRating IsNot Nothing) Then
            Dim PackageRateDetails As PackageRateDetail() = PackageRating.PackageRateDetails
            If (PackageRateDetails IsNot Nothing) Then
                For Each ratedPackage As PackageRateDetail In PackageRateDetails
                    If (ratedPackage IsNot Nothing) Then
                        If (ratedPackage.RateTypeSpecified) Then
                            If (ratedPackage.RateType = ActualRateType) Then
                                Console.WriteLine()
                                Console.WriteLine("Package Rate details")
                                Console.WriteLine("Rate Type {0}", ratedPackage.RateType)
                                If (ratedPackage.BillingWeight IsNot Nothing) Then
                                    Console.WriteLine("Billing weight {0} {1}", ratedPackage.BillingWeight.Value, ratedPackage.BillingWeight.Units)
                                End If

                                If (ratedPackage.BaseCharge IsNot Nothing) Then
                                    Console.WriteLine("Base charge {0} {1}", ratedPackage.BaseCharge.Amount, ratedPackage.BaseCharge.Currency)
                                End If

                                If (ratedPackage.NetCharge IsNot Nothing) Then
                                    Console.WriteLine("Net charge {0} {1}", ratedPackage.NetCharge.Amount, ratedPackage.NetCharge.Currency)
                                End If

                                If (ratedPackage.Surcharges IsNot Nothing) Then
                                    ' Individual surcharge for each package
                                    For Each surcharge As Surcharge In ratedPackage.Surcharges
                                        Console.WriteLine("{0} surcharge {1} {2}", surcharge.SurchargeType, surcharge.Amount.Amount, surcharge.Amount.Currency)
                                    Next
                                End If

                                If (ratedPackage.TotalSurcharges IsNot Nothing) Then
                                    Console.WriteLine("Total surcharge {0} {1}", ratedPackage.TotalSurcharges.Amount, ratedPackage.TotalSurcharges.Currency)
                                End If
                            End If
                        End If
                    End If
                Next ratedPackage
            End If
        End If
    End Sub

    Private Sub ShowBarcodeDetails(ByRef barcodes As PackageBarcodes)
        ' Barcode information for each package
        Console.WriteLine()
        Console.WriteLine("Barcode details")
        If (barcodes IsNot Nothing) Then
            If (barcodes.StringBarcodes IsNot Nothing) Then
                For i As Integer = 0 To barcodes.StringBarcodes.Length - 1
                    Console.WriteLine("String barcode {0} Type {1}", barcodes.StringBarcodes(i).Value, barcodes.StringBarcodes(i).Type)
                Next
            End If

            If (barcodes.BinaryBarcodes IsNot Nothing) Then
                For i As Integer = 0 To barcodes.BinaryBarcodes.Length - 1
                    Console.WriteLine("Binary barcode Type {0}", barcodes.BinaryBarcodes(i).Type)
                Next
            End If
        End If
    End Sub

    Private Sub GetPackageRouteDetails(ByVal operationalDetail As ShipmentOperationalDetail, ByRef fedExPackageDetail As FedExPackageDetail)

        If operationalDetail.DeliveryDaySpecified Then
            fedExPackageDetail.DeliveryDay = operationalDetail.DeliveryDay.ToString()
        End If

        If operationalDetail.DeliveryDateSpecified Then
            fedExPackageDetail.DeliveryDate = operationalDetail.DeliveryDate.ToShortDateString()
        End If

        If (operationalDetail.TransitTimeSpecified) Then
            fedExPackageDetail.TransitTime = operationalDetail.TransitTime.ToString()
        End If

    End Sub


    Private Sub ShowShipmentLabels(ByVal CompletedShipmentDetail As CompletedShipmentDetail, ByVal packageDetail As CompletedPackageDetail, ByRef fedExPackageDetail As FedExPackageDetail)
        If (packageDetail.Label.Parts(0).Image IsNot Nothing) Then
            ' Save outbound shipping label 
            Dim fileName As String = Me.OrderID & "-" & packageDetail.TrackingIds(0).TrackingNumber + ".pdf"
            Dim filePath As String = HttpContext.Current.Server.MapPath(BaseApplicationVariables.FedExLabelLocation & "/" & fileName)
            fedExPackageDetail.FileName = fileName
            SaveLabel(filePath, packageDetail.Label.Parts(0).Image)
            '   DisplayLabel(filePath)
        End If
    End Sub

    Private Sub SaveLabel(ByRef labelFileName As String, ByRef labelBuffer() As Byte)
        ' Save label buffer to file
        Me.FilePath = labelFileName

        Dim LabelFile As FileStream = New FileStream(labelFileName, FileMode.Create, FileAccess.ReadWrite)
        LabelFile.Write(labelBuffer, 0, labelBuffer.Length)
        LabelFile.Close()
        '' Display label in Acrobat
        'DisplayLabel(labelFileName)
    End Sub

    Private Function PresentPDFLabel(ByVal labelFileName As String, ByVal labelBuffer() As Byte) As Boolean
        System.Web.HttpContext.Current.Response.Clear()
        System.Web.HttpContext.Current.Response.Buffer = True
        System.Web.HttpContext.Current.Response.AddHeader("Content-Length", labelBuffer.Length.ToString())
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + labelFileName)
        System.Web.HttpContext.Current.Response.AddHeader("Expires", "0")
        System.Web.HttpContext.Current.Response.AddHeader("Pragma", "cache")
        System.Web.HttpContext.Current.Response.AddHeader("Cache-Control", "private")
        'Set the output stream to the correct content type (PDF).
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf"
        System.Web.HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes")
        'Output the file
        System.Web.HttpContext.Current.Response.BinaryWrite(labelBuffer)
        'Flushing the Response to display the serialized data to the client browser.
        System.Web.HttpContext.Current.Response.Flush()
        Try
            System.Web.HttpContext.Current.Response.End()
            Return True
        Catch Ex As Exception
            Return False
        End Try

    End Function

    Private Function DisplayLabel(ByVal filePath As String) As Boolean
        Dim info As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo(filePath)
        info.UseShellExecute = True
        info.Verb = "" ' Leave this verb empty. Using the verb "Open" will provoke an error. Leaving it blank forces the default process for the pdf.
        System.Diagnostics.Process.Start(info)

    End Function
    Private Function usePropertyFile() As Boolean 'Set to true for common properties to be set with getProperty function.
        Return getProperty("usefile").Equals("True")
    End Function
    Private Function getProperty(ByRef propertyname As String) As String 'This function sets common properties for testing purposes.
        Try
            Dim filename As String = "C:\filepath\filename.txt"
            If System.IO.File.Exists(filename) Then
                Dim sr As New System.IO.StreamReader(filename)
                Do While Not sr.EndOfStream
                    Dim parts As String() = sr.ReadLine.Split(New Char() {","c})
                    If (parts(0).Equals(propertyname) And parts.Length = 2) Then
                        Return parts(1)
                    End If
                Loop
            End If
            Console.WriteLine("Property {0} set to default 'XXX'", propertyname)
            Return "XXX"
        Catch ex As System.Exception
            Console.WriteLine("Property {0} set to default 'XXX'", propertyname)
            Return "XXX"
        End Try
    End Function


    Sub GetTrackingDetails(ByVal TrackingIds() As TrackingId, ByRef fedExPackageDetail As FedExPackageDetail)
        ' Tracking information for each package
        If TrackingIds Is Nothing Then
            Return
        End If

        If TrackingIds.Count = 1 Then
            fedExPackageDetail.TrackingNumber = TrackingIds(0).TrackingNumber
        Else
            Dim trackingNumbers As String = String.Empty

            For Each trackingId As TrackingId In TrackingIds
                If trackingNumbers.IsNullOrEmpty() Then
                    trackingNumbers = trackingId.TrackingNumber
                Else
                    trackingNumbers = trackingNumbers & "," & trackingId.TrackingNumber
                End If
            Next
            fedExPackageDetail.TrackingNumber = trackingNumbers
        End If

    End Sub

    Sub GetBarcodeDetails(ByRef barcodes As PackageBarcodes, ByRef fedExPackageDetail As FedExPackageDetail)
        ' Barcode information for each package
        If barcodes Is Nothing Then
            Return
        End If

        If barcodes.StringBarcodes IsNot Nothing Then
            For i As Integer = 0 To barcodes.StringBarcodes.Length - 1
                If fedExPackageDetail.Barcode.IsNullOrEmpty() Then
                    fedExPackageDetail.Barcode = barcodes.StringBarcodes(i).Value
                    fedExPackageDetail.BarcodeType = barcodes.StringBarcodes(i).Type
                Else
                    fedExPackageDetail.Barcode = fedExPackageDetail.Barcode & "," & barcodes.StringBarcodes(i).Value
                    fedExPackageDetail.BarcodeType = fedExPackageDetail.BinaryBarcodeType & "," & barcodes.StringBarcodes(i).Type
                End If
            Next


            If (barcodes.BinaryBarcodes IsNot Nothing) Then
                For i As Integer = 0 To barcodes.BinaryBarcodes.Length - 1
                    If fedExPackageDetail.BinaryBarcodeType.IsNullOrEmpty() Then
                        fedExPackageDetail.BinaryBarcodeType = barcodes.BinaryBarcodes(i).Type
                    Else
                        fedExPackageDetail.BinaryBarcodeType = fedExPackageDetail.BinaryBarcodeType & "," & barcodes.BinaryBarcodes(i).Type
                    End If
                Next
            End If
        End If
    End Sub
    Sub GetPackageRateDetails(ByVal PackageRating As PackageRating, ByVal ActualRateType As ReturnedRateType, ByRef fedExPackageDetail As FedExPackageDetail)

        If PackageRating Is Nothing Then
            Return
        End If

        Dim PackageRateDetails As PackageRateDetail() = PackageRating.PackageRateDetails

        If PackageRateDetails Is Nothing OrElse PackageRateDetails.Count = 0 Then
            Return
        End If

        If PackageRateDetails.Count = 1 Then
            Dim ratedPackage As PackageRateDetail
            ratedPackage = PackageRateDetails(0)

            If ratedPackage.RateTypeSpecified = False Then
                Return
            End If

            If Not ratedPackage.RateType = ActualRateType Then
                Return
            End If

            fedExPackageDetail.RateType = ratedPackage.RateType

            If ratedPackage.BillingWeight IsNot Nothing Then
                fedExPackageDetail.BillingWeight = ratedPackage.BillingWeight.Value
            End If

            If ratedPackage.BaseCharge IsNot Nothing Then
                fedExPackageDetail.BaseCharge = ratedPackage.BaseCharge.Amount
            End If

            If ratedPackage.TotalSurcharges IsNot Nothing Then
                fedExPackageDetail.TotalSurcharge = ratedPackage.TotalSurcharges.Amount
            End If
            Return
        End If

        'more than one record
        For Each ratedPackage As PackageRateDetail In PackageRateDetails
            If (ratedPackage IsNot Nothing) Then
                If (ratedPackage.RateTypeSpecified) Then
                    If (ratedPackage.RateType = ActualRateType) Then
                        If fedExPackageDetail.RateType.IsNullOrEmpty() Then
                            fedExPackageDetail.RateType = ratedPackage.RateType
                        Else
                            fedExPackageDetail.RateType = fedExPackageDetail.RateType & "," & ratedPackage.RateType
                        End If

                        If (ratedPackage.BillingWeight IsNot Nothing) Then
                            If fedExPackageDetail.BillingWeight.IsNullOrEmpty() Then
                                fedExPackageDetail.BillingWeight = ratedPackage.BillingWeight.Value
                            Else
                                fedExPackageDetail.BillingWeight = fedExPackageDetail.BillingWeight & "," & ratedPackage.BillingWeight.Value
                            End If
                        End If

                        If (ratedPackage.BaseCharge IsNot Nothing) Then
                            If fedExPackageDetail.BaseCharge.IsNullOrEmpty() Then
                                fedExPackageDetail.BaseCharge = ratedPackage.BaseCharge.Amount
                            Else
                                fedExPackageDetail.BaseCharge = fedExPackageDetail.BaseCharge & "," & ratedPackage.BaseCharge.Amount
                            End If
                        End If

                        If (ratedPackage.NetCharge IsNot Nothing) Then
                            If fedExPackageDetail.NetCharge.IsNullOrEmpty() Then
                                fedExPackageDetail.NetCharge = ratedPackage.NetCharge.Amount
                            Else
                                fedExPackageDetail.NetCharge = fedExPackageDetail.NetCharge & "," & ratedPackage.NetCharge.Amount
                            End If
                        End If

                            If (ratedPackage.TotalSurcharges IsNot Nothing) Then
                            If fedExPackageDetail.TotalSurcharge.IsNullOrEmpty() Then
                                fedExPackageDetail.TotalSurcharge = ratedPackage.TotalSurcharges.Amount
                            Else
                                fedExPackageDetail.TotalSurcharge = fedExPackageDetail.TotalSurcharge & "," & ratedPackage.TotalSurcharges.Amount
                            End If
                        End If

                        End If
                End If
            End If
        Next ratedPackage


    End Sub



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


#End Region
End Class
