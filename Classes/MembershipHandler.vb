Imports Pigeon.Pigeon
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.Profile
Imports System.Web.Security
Public Class MembershipHandler
    Inherits SqlMembershipProvider

    Public Overrides Sub Initialize(name As String, config As NameValueCollection)

        'Try
        '    Dim masterPigeonClasses As New Pigeon1
        '    MyBase.Initialize(name, config) ' Update the Private connection String field In the base Class.
        '    Dim connectionString As String = GetClientConnectionString(masterPigeonClasses.getCurrentClient(HttpContext.Current.Request.Url.Host)(1))
        '    ' DetermineYourConenctionString();
        '    ' Set private property of Membership provider.
        '    Dim connectionStringField = [GetType]().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance Or BindingFlags.NonPublic)
        '    connectionStringField.SetValue(Me, connectionString)

        'Catch ex As Exception

        'End Try

    End Sub
End Class

