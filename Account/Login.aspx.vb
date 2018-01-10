Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Public Class Login
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        current_client.Text = Session("Client")

        If HttpContext.Current.User Is Nothing Then Exit Sub

        If Session("Role") = "Admin" Or Session("Role") = "Customer" Then
            Response.Redirect("../Pages/Home.aspx")
        Else
            Response.Redirect("../Default.aspx")
        End If
    End Sub

    Public Sub LoginUser_Authenticate(ByVal sender As Object, ByVal e As AuthenticateEventArgs) Handles LoginUser.Authenticate


        Dim login As Login = DirectCast(sender, Login)
        ' Get the user attempting to login

        Dim p As SqlMembershipProvider = PidgeonMembership.GetMembershipProvider()

        If IsNothing(p) Then Throw New Provider.ProviderException(String.Format("Current URL {0} does has no provider to select", Page.Session("CurrentURL")))

        Dim strUserName As String = CType(login.FindControl("UserName"), TextBox).Text
        Dim strPassword As String = CType(login.FindControl("Password"), TextBox).Text

        Dim user As MembershipUser = p.GetUser(strUserName, True)

        If user IsNot Nothing Then
            ' Get the date the user last logged in before
            ' the user is validated and the login gets updated
            ' to the current login date

            Dim dtLastLogin As DateTime = user.LastLoginDate

            ' Attempt to validate user -- if success then set the authenticated
            ' flag so that the framework knows to allow the user access

            If p.ValidateUser(strUserName, strPassword) Then
                e.Authenticated = True
                Dim rp As SqlRoleProvider = PidgeonMembership.GetRoleProvider()
                Dim roles As String() = rp.GetRolesForUser(strUserName)

                FormsAuthentication.SetAuthCookie(strUserName, True)
                GlobalFunctions.RenewCurrentUser(roles)

            End If
        End If
    End Sub

    Private Sub LoginUser_LoggedIn(sender As Object, e As System.EventArgs) Handles LoginUser.LoggedIn

        If Session("Role") = "Admin" Or Session("Role") = "Customer" Then
            Response.Redirect("../Pages/Home.aspx")
        Else
            Response.Redirect("../Default.aspx")
        End If
    End Sub


End Class