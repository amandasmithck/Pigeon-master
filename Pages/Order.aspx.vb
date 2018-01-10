Imports Pigeon.Pigeon
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.IO

Public Class Order
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Me.IsPostBack = False Then
        '    Me.cboPigeon.SelectedValue = Session("Client")
        'End If
        'build user model


        Dim js As New JavaScriptSerializer()
        Dim c As New Customer

        Dim client = Session("Client")
        Dim name = Page.User.Identity.Name

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT tblCompany.*, aspnet_membership.tierid FROM aspnet_Membership INNER JOIN aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId INNER JOIN tblCompany ON aspnet_Membership.CustomerNo = tblCompany.CustomerNo WHERE (aspnet_Users.UserName = '" & name & "')", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()
                While r.Read()
                    c.CompanyID = r("CompanyID")
                    c.Company = r("Company")
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
                    If client = "CK" And r("TierID").ToString = "3" Then
                        c.Company = " "
                        c.Address = " "
                        c.City = " "
                        c.State = ""
                        c.Zip = " "
                        c.Phone = " "
                    End If

                End While
            End Using
        End Using

        c.UserName = Page.User.Identity.Name
        'If Session("UserTier") Is Nothing Then
        Session("UserTier") = GetUserTier(Page.User.Identity.Name, Session("Client"))
        'End If
        c.Tier = Session("UserTier")
        c.Client = Session("Client")
        c.IP = Session("IP")
        c.Role = LCase(GetUserRole(Page.User.Identity.Name, Session("Client")))
        c.Calc = CheckForCalc(Session("Client"))
        c.WarrantyPaperwork = ShowWarrantyPaperwork(Session("UserTier"), Session("Client"))
        c.GoogleAnalytics = GetGoogleAnalytics(Session("Client"))

        Dim warranties As New List(Of Warranty)

        Dim strtier = GetUserTier(name, client)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Select Href, Tier, PartType, Warranty, Base, Percentage, Flat from tblWarrantyOptions LEFT OUTER JOIN tblPartType on tblPartType.Type = tblWarrantyOptions.PartType Where tblWarrantyOptions.Tier = '" & strtier & "'", conn)
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



End Class