Imports System.Web.SessionState
Imports System.Reflection
Imports System.Web.HttpRequest
Imports Pigeon.Pigeon
Imports wrangler

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
        Dim UserIPAddress
        UserIPAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If UserIPAddress = "" Then
            UserIPAddress = Request.ServerVariables("REMOTE_ADDR")
        End If

        Session("IP") = UserIPAddress

        ConnectionStrings.SetSessionConnectionStringsAndProviders(HttpContext.Current.Request.Url.Host)
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request

        ' If String.IsNullOrEmpty(Session("Client")) Then


        '  End If
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        Try
            Dim ex As Exception = HttpContext.Current.Server.GetLastError()
            If wrangle(ex) Then
            End If
        Catch Ex As Exception
        End Try

    End Sub
    Private Function wrangle(ByVal ex As Exception) As Boolean

        Try
            Return wrangle(ex, wrangler.enums.SeverityLevelTypes.Moderate, String.Empty, False)
        Catch e As Exception
            Return False
        End Try
    End Function

    Private Function wrangle(ByVal ex As Exception, ByVal severityLevelType As wrangler.enums.SeverityLevelTypes, ByVal userID As String, ByVal requiresImmediateNotification As Boolean) As Boolean

        Try
            Dim errorWrangler As New wrangler.errorWrangler
            errorWrangler.wrangleType = wrangler.enums.WrangledTypes.Error
            errorWrangler.projectType = wrangler.enums.ProjectTypes.CK
            errorWrangler.userID = User.Identity.Name
            errorWrangler.severityLevelType = severityLevelType
            errorWrangler.errorCode = String.Empty
            errorWrangler.errorSummaryMsg = ex.Message
            errorWrangler.stackTrace = ex.StackTrace

            Dim sb As New StringBuilder
            sb.Append("Error Name: " + ex.GetType().FullName + " - Url: " + HttpContext.Current.Request.RawUrl.ToString())
            sb.Append("Target: " + ex.TargetSite.Name + " " + ex.TargetSite.DeclaringType.FullName + " - ")
            sb.Append("Source: " + ex.Source)
            errorWrangler.additionalInfomation = sb.ToString()

            errorWrangler.displayTextForErrorMsg = ex.Message
            errorWrangler.userID = userID
            errorWrangler.requiresImmediateNotification = requiresImmediateNotification
            errorWrangler.dateEntered = DateTime.Now

            Return errorWrangler.wrangleError()

        Catch e As Exception
            Return False
        End Try
    End Function

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends

    End Sub




End Class