Imports System.Security.Cryptography
Imports System.IO
Imports System.Data.SqlClient
Imports Pigeon.Pigeon

Public Class _Default
    Inherits System.Web.UI.Page
    Private key() As Byte = {}
    Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

    Public Function Decrypt(ByVal stringToDecrypt As String,
    ByVal sEncryptionKey As String) As String
        Dim inputByteArray(stringToDecrypt.Length) As Byte
        Try
            key = System.Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(stringToDecrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV),
                CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'first see if this is from dealer central

        If Request.QueryString("u") <> Nothing Then
            Try
                Response.Cookies.Remove(FormsAuthentication.FormsCookieName)
                Dim user As String = Decrypt(Replace(Replace(Request.QueryString("u"), " ", "+"), "%%", "="), "!#$a34?9")
                If user.Contains("Invalid") Then Exit Sub
                FormsAuthentication.SetAuthCookie(Decrypt(Replace(Replace(Request.QueryString("u"), " ", "+"), "%%", "="), "!#$a34?9"), False)
                Response.Redirect("Pages\Home.aspx")
                Exit Sub
            Catch Ex As Exception
                Exit Sub
            End Try


        End If

        If Not IsPostBack Then Exit Sub

        If Session("Role") = "Admin" Or Session("Role") = "Customer" Then
            Response.Redirect("Pages/Home.aspx")
        End If


    End Sub

    Private Sub LoginUser_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login1.LoggedIn
        Session("UserTier") = Nothing

        Dim login As System.Web.UI.WebControls.Login = DirectCast(sender, System.Web.UI.WebControls.Login)
        Dim userName As String = CType(login.FindControl("UserName"), TextBox).Text

        Dim rp As SqlRoleProvider = PidgeonMembership.GetRoleProvider()
        If IsNothing(rp) Then Throw New Provider.ProviderException(String.Format("Current URL {0} does has no role provider to select", Page.Session("client")))

        'Session("Role") = strRole
        If (rp.IsUserInRole(userName, "Admin") Or rp.IsUserInRole(userName, "Customer")) Then
            Response.Redirect("Pages/Home.aspx")
        End If

    End Sub

    Private Sub Login_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate

        Dim login As System.Web.UI.WebControls.Login = DirectCast(sender, System.Web.UI.WebControls.Login)
        ' Get the user attempting to login

        Dim p As SqlMembershipProvider = PidgeonMembership.GetMembershipProvider()

        If IsNothing(p) Then Throw New Provider.ProviderException(String.Format("Current URL {0} does has no provider to select", Page.Session("CurrentURL")))

        Dim strUserName As String = CType(login.FindControl("UserName"), TextBox).Text
        Dim strPassword As String = CType(login.FindControl("Password"), TextBox).Text

        Dim user As MembershipUser = p.GetUser(strUserName, True)

        If user IsNot Nothing Then
            If p.ValidateUser(strUserName, strPassword) Then
                e.Authenticated = True

                Dim rp As SqlRoleProvider = PidgeonMembership.GetRoleProvider()
                Dim roles As String() = rp.GetRolesForUser(strUserName)

                FormsAuthentication.SetAuthCookie(strUserName, True)
                GlobalFunctions.RenewCurrentUser(roles)

            End If
        End If

    End Sub

    Private Sub Login1_LoggingIn(sender As Object, e As LoginCancelEventArgs) Handles Login1.LoggingIn

    End Sub

    Private Sub Login1_LoginError(sender As Object, e As EventArgs) Handles Login1.LoginError

    End Sub
End Class