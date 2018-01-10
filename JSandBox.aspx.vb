Option Strict On
Option Explicit On

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

'Imports ShipWebServiceClient.ShipServiceWebReference
Public Class JSandBox
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DeteleAllFilesInFedExPDFDirectory()

        'Dim listOfServiceTypes As New List(Of ServiceType)
        'listOfServiceTypes.Add(ServiceType.PRIORITY_OVERNIGHT)
        'listOfServiceTypes.Add(ServiceType.FEDEX_2_DAY)
        'listOfServiceTypes.Add(ServiceType.FEDEX_EXPRESS_SAVER)
        'listOfServiceTypes.Add(ServiceType.FIRST_OVERNIGHT)
        'listOfServiceTypes.Add(ServiceType.STANDARD_OVERNIGHT)
        'listOfServiceTypes.Add(ServiceType.FEDEX_GROUND)
        'Dim sbErr As New StringBuilder()

        'For Each st As ServiceType In listOfServiceTypes

        '    Dim sbItemText As New StringBuilder()
        '    Dim fedEx As New FedExRateHelper()
        '    fedEx.OrderID = "23222"
        '    fedEx.SetDestinationAddress("3601 Maryland Avenue", "Richmond", "VA", "23222", "US")
        '    fedEx.SetOriginAddress("1206 Hollywalk", "Poughkeepsie", "NY", "12603", "US")
        '    fedEx.AddPackageLineItem(Convert.ToDecimal(10), RateWebServiceClient.RateWebReference.WeightUnits.LB, "3", "5", "3", RateWebServiceClient.RateWebReference.LinearUnits.IN)
        '    fedEx.SetShipmentDetailData(RateWebServiceClient.RateWebReference.DropoffType.REGULAR_PICKUP, st, RateWebServiceClient.RateWebReference.PackagingType.YOUR_PACKAGING)
        '    Dim fedExRateReply As RateReply

        '    Try
        '        fedExRateReply = fedEx.GetRateReply()
        '    Catch ex As Exception
        '        sbErr.Append(ex.Message.ToString() & "<br />")
        '        fedExRateReply = Nothing
        '    End Try

        '    sbItemText.Append("&nbsp; &nbsp; &nbsp" & [Enum].GetName(GetType(ServiceType), st).ToString() & "&nbsp; &nbsp; &nbsp")
        '    If fedExRateReply Is Nothing Then
        '        sbItemText.Append(" - 0")
        '    Else
        '        Dim shipmentRateDetail As ShipmentRateDetail = fedEx.GetShipmentRateDetail(fedExRateReply)

        '        If shipmentRateDetail Is Nothing Then
        '            sbItemText.Append(" - 0")
        '        End If

        '        'all good, add the value to the text
        '        sbItemText.Append(" - " & shipmentRateDetail.TotalNetCharge.Amount.ToString("C"))
        '    End If

        '    Dim item As New ListItem()
        '    item.Text = sbItemText.ToString()
        '    item.Value = Convert.ToInt32(st).ToString()
        '    Me.rblServiceTypes.Items.Add(item)
        'Next

        'Me.fedExRate.InnerHtml = sbErr.ToString()


        'Dim fedEx As New FedExRateHelper()
        'fedEx.OrderID = "232323"
        'fedEx.SetShipmentDetailData(RateWebServiceClient.RateWebReference.DropoffType.REGULAR_PICKUP, RateWebServiceClient.RateWebReference.ServiceType.STANDARD_OVERNIGHT, RateWebServiceClient.RateWebReference.PackagingType.FEDEX_BOX)
        'fedEx.SetDestinationAddress("3601 Maryland Avenue", "Richmond", "VA", "23222", "US")
        'fedEx.SetOriginAddress("", "COLLIERVILLE", "TN", "38017", "US")
        'fedEx.AddPackageLineItem(Convert.ToDecimal(10), RateWebServiceClient.RateWebReference.WeightUnits.LB, "3", "5", "3", RateWebServiceClient.RateWebReference.LinearUnits.IN)

        'Dim fedExRateReply As RateReply = fedEx.GetRateReply()

        'If fedExRateReply Is Nothing Then
        '    Me.fedExRate.InnerHtml = "Reply Missing"
        '    Return
        'End If
        'Dim shipmentRateDetail As ShipmentRateDetail = fedEx.GetShipmentRateDetail(fedExRateReply)

        'If shipmentRateDetail Is Nothing Then
        '    Me.fedExRate.InnerHtml = "Nothing To See Here"
        '    Return
        'End If

        ''   all good
        'Me.fedExRate.InnerHtml = shipmentRateDetail.TotalNetCharge.Amount.ToString("C")

        ''  Dim fedExShipper As New FedExGroundShippingHelper()
        'Dim fedExShipper As New FedExExpressShippingHelper()
        'fedExShipper.OrderID = "232323"
        ''  fedExShipper.SetShipmentDetailData(ShipWebServiceClient.ShipServiceWebReference.DropoffType.REGULAR_PICKUP, ShipWebServiceClient.ShipServiceWebReference.ServiceType.FEDEX_GROUND, ShipWebServiceClient.ShipServiceWebReference.PackagingType.YOUR_PACKAGING, False)
        'fedExShipper.SetShipmentDetailData(ShipWebServiceClient.ShipServiceWebReference.DropoffType.REGULAR_PICKUP, ShipWebServiceClient.ShipServiceWebReference.ServiceType.STANDARD_OVERNIGHT, ShipWebServiceClient.ShipServiceWebReference.PackagingType.YOUR_PACKAGING, False)

        'fedExShipper.SetDestinationAddress("3601 Maryland Avenue", "Richmond", "VA", "23222", "US")
        'fedExShipper.SetOriginAddress("1002 Holly Walk", "Poughkeepsie", "NY", "12603", "US")
        'fedExShipper.SetOriginContact("Bob Wright", "Chicklets Inc", "8043433333")
        'fedExShipper.SetDestinationContact("Tom Wrong", "Cokes Products LLC", "9099994444")

        'Dim custRefs As New List(Of ShipWebServiceClient.ShipServiceWebReference.CustomerReference)()
        'Dim custRef1 As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()
        'Dim custRef2 As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()
        'Dim custRef3 As New ShipWebServiceClient.ShipServiceWebReference.CustomerReference()

        ''custRef1.Value = "GR4567892"
        ''custRef1.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.CUSTOMER_REFERENCE
        ''custRef2.Value = "INV4567892"
        ''custRef2.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.INVOICE_NUMBER
        ''custRef3.Value = "PO4567892"
        ''custRef3.CustomerReferenceType = ShipWebServiceClient.ShipServiceWebReference.CustomerReferenceType.P_O_NUMBER
        ''custRefs.Add(custRef1)
        ''custRefs.Add(custRef2)
        ''custRefs.Add(custRef3)

        '' fedExShipper.AddPackageLineItem(Convert.ToDecimal(10), ShipWebServiceClient.ShipServiceWebReference.WeightUnits.LB, "3", "5", "3", ShipWebServiceClient.ShipServiceWebReference.LinearUnits.IN, custRefs)
        'fedExShipper.AddPackageLineItem(Convert.ToDecimal(10), ShipWebServiceClient.ShipServiceWebReference.WeightUnits.LB, "3", "5", "3", ShipWebServiceClient.ShipServiceWebReference.LinearUnits.IN)

        'Dim reply As New ProcessShipmentReply()
        'reply = fedExShipper.SendShipmentRequestAndRetrieveProcessShipmentReplyWithLabels()

        'If reply Is Nothing Then
        '    Me.fedExRate.InnerHtml = "NOTHING"
        'Else
        '    Me.fedExRate.InnerHtml = "SUCCESS"
        'End If


    End Sub


    Private Function DeteleAllFilesInFedExPDFDirectory() As Boolean
        If Directory.Exists(MapPath(BaseApplicationVariables.FedExLabelDirectory)) = False Then
            Return False
        End If

        Dim failCount As Integer = 0
        'directory exist ... delete
        For Each s As String In System.IO.Directory.GetFiles(MapPath(BaseApplicationVariables.FedExLabelDirectory))
            Try
                System.IO.File.Delete(s)
            Catch ex As Exception
                failCount = failCount + 1

                If failCount >= 3 Then
                    Exit For
                End If

                Continue For
            End Try

        Next s

    End Function
End Class