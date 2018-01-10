Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports Pigeon.CKExtensions
Imports System.Resources
Imports DeleteShipmentWebServiceClient.ShipServiceWebReference
Imports ShipWebServiceClient.ShipServiceWebReference

Public Class FedExAdvancedServicesHelper
    Public Shared Function CreateTransactionID(ByVal orderID As String, ByVal listOfParts As List(Of String))
        If orderID.IsNullOrEmpty() = True Then
            Return String.Empty
        End If

        If listOfParts Is Nothing OrElse listOfParts.Count = 0 Then
            Return orderID
        End If

        Dim sb As New System.Text.StringBuilder()
        sb.Append(orderID & "_")

        For Each s As String In listOfParts
            sb.Append(s & "_")
        Next

        Return sb.ToString.Substring(0, (sb.ToString().Length - 1))

    End Function
#Region "Properties"

    Public Property Notification As String
    Public Property ErrorMsg As String
    Public Property TrackingNumber As String

#End Region

    Public Sub New(ByVal trackingNumber As String)
        Me.TrackingNumber = trackingNumber

    End Sub

    Public Function DeleteShipment() As Boolean
        Dim request As DeleteShipmentRequest = CreateDeleteShipmentRequest()
        Dim service As ShipService = New ShipService() ' Initialize the service
        service.Url = BaseApplicationVariables.FedExWebServiceUrl

        Try
            ' Call to the ship web service passing in a DeleteShipmentRequest and returning a ShipmentReply
            Dim reply As ShipmentReply = service.deleteShipment(request)



            If (reply.HighestSeverity = NotificationSeverityType.SUCCESS) Then
                Return True
            Else
                Return False
            End If

        Catch e As SoapException
            Me.ErrorMsg = e.Detail.InnerText
        Catch e As Exception
            Me.ErrorMsg = e.Message
        End Try

    End Function

#Region "Private Methods"

    Private Function CreateDeleteShipmentRequest() As DeleteShipmentRequest
        ' Build a DeleteShipmentRequest
        Dim request As DeleteShipmentRequest = New DeleteShipmentRequest()

        request.WebAuthenticationDetail = New WebAuthenticationDetail()
        request.WebAuthenticationDetail.UserCredential = New WebAuthenticationCredential()
        request.WebAuthenticationDetail.UserCredential.Key = BaseApplicationVariables.FedExKey
        request.WebAuthenticationDetail.UserCredential.Password = BaseApplicationVariables.FedExPassword

        request.ClientDetail = New ClientDetail()
        request.ClientDetail.AccountNumber = BaseApplicationVariables.FedExAccountNumber
        request.ClientDetail.MeterNumber = BaseApplicationVariables.FedExMeterNumber

        request.TransactionDetail = New TransactionDetail()
        request.TransactionDetail.CustomerTransactionId = "***Delete Shipment Request using VB.NET***"

        request.Version = New VersionId() ' WSDL version information, value is automatically set from wsdl
        '
        request.TrackingId = New TrackingId()
        request.TrackingId.TrackingIdType = TrackingIdType.GROUND ' Replace with desired tracking id type
        request.TrackingId.TrackingIdTypeSpecified = True
        request.TrackingId.TrackingNumber = Me.TrackingNumber ' Replace "XXX" with the tracking number to delete

        '
        request.DeletionControl = DeletionControlType.DELETE_ALL_PACKAGES
        Return request
    End Function

    'Private Sub GetNotifications(ByVal reply As ShipmentReply)
    '    Dim sbNotification As New System.Text.StringBuilder()
    '    sbNotification.Append("Notifications: <br />")
    '    For i As Integer = 0 To reply.Notifications.Length - 1
    '        Dim notification As Notification = reply.Notifications(i)
    '        sbNotification.Append("Notification no. {0}", i)
    '        sbNotification.Append(" Severity: {0}", notification.Severity)
    '        sbNotification.Append(" Code: {0}", notification.Code)
    '        sbNotification.Append(" Message: {0}", notification.Message)
    '        'sbNotification.Append(" Source: {0}", notification.Source)
    '        sbNotification.Append("<br />")
    '    Next

    '    Me.Notification = sbNotification.ToString()
    'End Sub


#End Region

End Class
