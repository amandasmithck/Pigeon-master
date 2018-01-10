Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Web.HttpContext

Public Class GlobalFunctions

    Public Shared Sub RenewCurrentUser(roles As String())

        Dim authCookie As System.Web.HttpCookie = System.Web.HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)

        If authCookie Is Nothing Then Exit Sub

        Dim authTicket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(authCookie.Value)

        If Not IsNothing(authTicket) AndAlso Not authTicket.Expired Then

            Dim newAuthTicket As FormsAuthenticationTicket = authTicket
            If FormsAuthentication.SlidingExpiration Then
                newAuthTicket = FormsAuthentication.RenewTicketIfOld(authTicket)
            End If

            System.Web.HttpContext.Current.User =
            New System.Security.Principal.GenericPrincipal(New FormsIdentity(newAuthTicket), roles)

        End If

    End Sub

    Public Shared Function ExecuteAdHocSql(dbConnectionChoice As String, sql As String) As DataSet

        Dim ds As New DataSet()
        Dim da As New SqlDataAdapter()

        Using connection As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(dbConnectionChoice))
            Dim command As New SqlCommand(sql, connection)
            command.CommandTimeout = 180

            da.SelectCommand = command
            da.Fill(ds)

        End Using
        Return ds
    End Function
    Public Shared Function ExecuteSqlStoredProcedure(dbConnectionChoice As String, storedProcedure As String, Optional parameters() As SqlParameter = Nothing) As DataSet

        Dim ds As New DataSet()
        Dim da As New SqlDataAdapter()

        Using connection As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(dbConnectionChoice))
            Dim command As New SqlCommand(storedProcedure, connection)
            command.CommandType = CommandType.StoredProcedure
            command.CommandTimeout = 180

            If Not IsNothing(parameters) Then
                command.Parameters.AddRange(parameters)
            End If

            da.SelectCommand = command
            da.Fill(ds)
        End Using

        Return ds

    End Function

    Public Shared Function ExecuteSqlScalar(dbConnectionChoice As String, sql As String) As Object

        Dim returnVariable As Object

        Using connection As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(dbConnectionChoice))
            Dim command As New SqlCommand(sql, connection)
            connection.Open()
            returnVariable = command.ExecuteScalar()
        End Using

        Return returnVariable
    End Function



End Class
