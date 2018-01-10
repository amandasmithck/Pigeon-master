Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Public Class Home2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'redirect east coast tracy customers to C&K
    If Session("Client") ="Tracy" then
            If NeedsRedirect(Page.User.Identity.Name) = True Then Server.Transfer("FormerTracyCustomerRedirect.aspx")
    End If
    End Sub

    Private Function NeedsRedirect(ByVal user as string) as Boolean
    	dim strState as String
        Using conn As New SqlConnection(GetClientConnectionString(Session("Client")))
            Dim sqlComm As New SqlCommand("SELECT state from tblCompany inner join aspnet_membership on tblcompany.customerno=aspnet_membership.customerno inner join aspnet_users on aspnet_users.userid = aspnet_membership.userid WHERE username = '" & user & "'", conn)
            conn.Open()
            strState = sqlComm.ExecuteScalar
        End Using

        Select case strState

        Case "CA","HI","NV","AZ","UT","ID","MT","WY",""," "
        	'do nothing
        	return false
        Case Else
        	return true
        End Select
    End Function

End Class