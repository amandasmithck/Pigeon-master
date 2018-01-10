Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Xml
Imports Pigeon.Pigeon
Imports Pigeon.OEMWebService
Imports System.Net
Imports System.IO
Imports System.Globalization

Public Class PigeonCarmax
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Me.IsPostBack = False Then
        '    Me.cboPigeon.SelectedValue = Session("Client")
        'End If
        'build user model
        Dim js As New JavaScriptSerializer()
        Dim c As New Customer

        Dim client = Session("Client")
        Dim name = Page.User.Identity.Name

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT tblCompany.*, aspnet_membership.tierid, aspnet_membership.email as LoggedInEmail FROM aspnet_Membership INNER JOIN aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId INNER JOIN tblCompany ON aspnet_Membership.CustomerNo = tblCompany.CustomerNo WHERE (aspnet_Users.UserName = '" & name & "')", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()
                While r.Read()
                    c.CompanyID = r("CompanyID")
                    c.Company = r("Company")
                    c.LoggedInCompany = r("Company")
                    c.LoggedInEmail = r("LoggedInEmail")
                    c.CustNo = r("CustomerNo")
                    If client <> "CK" Then
                        c.Address = r("Address")
                    Else
                        c.Address = r("Address1")
                    End If

                    c.City = r("City")
                    c.State = r("State")
                    c.Zip = r("Zip")
                    c.Phone = r("Phone")
                    If client = "CK" And (r("TierID").ToString = "3" Or r("TierID").ToString = "41") Then
                        c.Company = " "
                        c.Address = " "
                        c.City = " "
                        c.State = ""
                        c.Zip = " "
                        c.Phone = " "
                    End If
                    If client = "CK" And r("TierID").ToString = "2" Then
                        c.LoggedInCompany = "C&K"
                        c.LoggedInEmail = name
                    End If


                End While
            End Using
        End Using

        c.UserName = Page.User.Identity.Name
        'If Session("UserTier") Is Nothing Then
        Session("UserTier") = GetUserTier(Page.User.Identity.Name, Session("Client"))
        'End If
        If String.IsNullOrEmpty(Session("UserTier")) Then
            Response.Redirect("404/404-light.html")

        End If

        c.Tier = Session("UserTier")
        c.Client = Session("Client")
        If client = "CK" Then

            If c.Tier = "2" Then
                c.OEMShipping = 42.5
                c.SmallPartsShipping = 42.5
                c.GroundOEMShipping = 29.95
                c.GroundSmallPartsShipping = 29.95
            Else
                Dim p As New CustShippingPrices
                p = GetCustomerShippingPrices(c.CustNo)
                c.OEMShipping = p.OEMShipping
                c.SmallPartsShipping = p.SmallPartsShipping
                c.GroundOEMShipping = p.GroundOEMShipping
                c.GroundSmallPartsShipping = p.GroundSmallPartsShipping
            End If
        End If
        c.IP = Session("IP")
        c.Role = LCase(GetUserRole(Page.User.Identity.Name, Session("Client")))
        c.Calc = CheckForCalc(Session("Client"))
        c.WarrantyPaperwork = ShowWarrantyPaperwork(Session("UserTier"), Session("Client"))
        c.GoogleAnalytics = GetGoogleAnalytics(Session("Client"))

        Dim warranties As New List(Of Warranty)

        Dim strtier = GetUserTier(name, client)
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("Select Href, Tier, PartType, Warranty, Base, Percentage, Flat from tblWarrantyOptions LEFT OUTER JOIN tblPartType on tblPartType.Type = tblWarrantyOptions.PartType Where tblWarrantyOptions.Tier = '" & strtier & "' order by base desc", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim warranty As New Warranty
                    warranty.Warranty = r("Warranty")
                    warranty.Base = r("Base")
                    warranty.Percentage = r("Percentage")
                    warranty.Flat = r("Flat")
                    warranty.PartType = r("PartType")
                    warranty.Tier = r("Tier")
                    warranty.Href = r("Href")

                    warranties.Add(warranty)
                End While
            End Using
        End Using

        c.Warranties = warranties
        Session("UserModel") = js.Serialize(c)
    End Sub

    'Private Sub cboPigeon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPigeon.SelectedIndexChanged

    '    FormsAuthentication.SignOut()
    '    Select Case Me.cboPigeon.SelectedValue
    '        Case "Tracy"
    '            Response.Redirect("http://tracypigeon.ckautoparts.com")
    '        Case "Autoway"
    '            Response.Redirect("http://autowaypigeon.ckautoparts.com")
    '        Case "LarryMiller"
    '            Response.Redirect("http://larrymillerpigeon.ckautoparts.com")
    '        Case "GO"
    '            Response.Redirect("http://gopigeon.ckautoparts.com")
    '        Case "BigValley"
    '            Response.Redirect("http://bigvalleypigeon.ckautoparts.com")
    '        Case "CK"
    '            Response.Redirect("http://pigeon.ckautoparts.com")
    '        Case "DuPratt"
    '            Response.Redirect("http://duprattpigeon.ckautoparts.com")
    '        Case "DickMyers"
    '            Response.Redirect("http://dickmyerspigeon.ckautoparts.com")
    '        Case "Fitz"
    '            Response.Redirect("http://fitzpigeon.ckautoparts.com")
    '        Case "Quirk"
    '            Response.Redirect("http://quirkpigeon.ckautoparts.com")
    '    End Select

    'End Sub
End Class