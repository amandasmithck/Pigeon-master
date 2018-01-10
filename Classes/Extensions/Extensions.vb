Option Strict On
Option Explicit On

Imports Microsoft.VisualBasic
Imports System.Runtime.CompilerServices
Imports ShipWebServiceClient.ShipServiceWebReference

Namespace CKExtensions

    Public Module ValidationHelper

        <Extension()>
        Public Function IsBool(ByVal aPotentialBool As String) As Boolean
            Dim aBool As Boolean
            Return Boolean.TryParse(aPotentialBool, aBool)
        End Function

        <Extension()>
        Public Function IsInt(ByVal aPotentialInt As String) As Boolean
            Dim aInt As Integer
            Return Integer.TryParse(aPotentialInt, aInt)
        End Function

        <Extension()>
        Public Function IsNullOrEmpty(ByVal aString As String) As Boolean
            If IsDBNull(aString) OrElse aString Is Nothing OrElse aString = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Function



#Region "Data Reader Functions"

        <Extension()>
        Public Function SanitizeString(ByRef dr As System.Data.SqlClient.SqlDataReader, ByVal nameOfColumn As String) As String
            If dr.IsDBNull(dr.GetOrdinal(nameOfColumn)) = True Then
                Return String.Empty
            Else
                Return dr.GetString(dr.GetOrdinal(nameOfColumn))
            End If
        End Function

        <Extension()>
        Public Function SanitizeInteger(ByRef dr As System.Data.SqlClient.SqlDataReader, ByVal nameOfColumn As String) As Integer
            If dr.IsDBNull(dr.GetOrdinal(nameOfColumn)) = True Then
                Return 0
            Else
                Return dr.GetInt32(dr.GetOrdinal(nameOfColumn))
            End If
        End Function

        <Extension()>
        Public Function SanitizeShortDateTime(ByRef dr As System.Data.SqlClient.SqlDataReader, ByVal nameOfColumn As String) As String
            If dr.IsDBNull(dr.GetOrdinal(nameOfColumn)) = True Then
                Return String.Empty
            Else
                Return dr.GetDateTime(dr.GetOrdinal(nameOfColumn)).ToShortDateString()
            End If
        End Function

        <Extension()>
        Public Function SanitizeDecimal(ByRef dr As System.Data.SqlClient.SqlDataReader, ByVal nameOfColumn As String) As Decimal
            If dr.IsDBNull(dr.GetOrdinal(nameOfColumn)) = True Then
                Return 0
            Else
                Return dr.GetDecimal(dr.GetOrdinal(nameOfColumn))
            End If
        End Function

        <Extension()>
        Public Function SanitizeBoolean(ByRef dr As System.Data.SqlClient.SqlDataReader, ByVal nameOfColumn As String) As Boolean
            If dr.IsDBNull(dr.GetOrdinal(nameOfColumn)) = True Then
                Return False
            Else
                Return dr.GetBoolean(dr.GetOrdinal(nameOfColumn))
            End If
        End Function

#End Region

#Region "Fed ExHelper Methods"
        <Extension()>
        Public Function TotalWeight(ByVal listOfRequestedPackageLineItems As List(Of RequestedPackageLineItem)) As Decimal
            Dim tWeight As Decimal = 0

            If listOfRequestedPackageLineItems Is Nothing OrElse listOfRequestedPackageLineItems.Count = 0 Then
                Return 0
            End If

            tWeight = listOfRequestedPackageLineItems.Sum(Function(i) i.Weight.Value)
            Return tWeight
        End Function
#End Region

#Region "List Methods"
        <Extension()>
        Public Function ToCommaDelimitedString(ByVal list As List(Of String)) As String
            If list Is Nothing OrElse list.Count = 0 Then
                Return String.Empty
            End If

            Return String.Join(",", list.ToArray())
        End Function

        <Extension()>
        Public Function ToCommaDelimitedString(ByVal list As List(Of Integer)) As String
            If list Is Nothing OrElse list.Count = 0 Then
                Return String.Empty
            End If

            Return String.Join(",", list.ToArray())
        End Function

#End Region

#Region "String Name Value Pair Management"
        <Extension()>
        Public Function GetValueByName(ByVal aString As String, ByVal name As String, ByVal delimiter As Char) As String
            If IsDBNull(aString) OrElse aString Is Nothing OrElse aString = String.Empty Then
                Return String.Empty
            End If

            If aString.IndexOf(name) = -1 Then
                Return String.Empty
            End If

            'harvest data
            Dim nameValuePairs As String() = aString.Split(delimiter)
            Dim value As String = String.Empty

            For Each s As String In nameValuePairs
                Dim nvp As String() = s.Split(Convert.ToChar("="))
                If nvp(0) = name Then
                    value = nvp(1)
                    Exit For
                End If
            Next

            Return value
        End Function

#End Region

#Region "Exception Helpers"
        <Extension()>
        Public Function wrangle(ByVal ex As Exception) As Boolean

            Try
                Return wrangle(ex, wrangler.enums.SeverityLevelTypes.Moderate, String.Empty, False, "WIS")
            Catch e As Exception
                Return False
            End Try
        End Function
        <Extension()>
        Public Function wrangle(ByVal ex As Exception, ByVal severityLevelType As wrangler.enums.SeverityLevelTypes, ByVal userID As String, ByVal requiresImmediateNotification As Boolean) As Boolean
            Try
                Dim errorWrangler As New wrangler.errorWrangler
                errorWrangler.wrangleType = wrangler.enums.WrangledTypes.Error
                errorWrangler.projectType = wrangler.enums.ProjectTypes.WIS
                errorWrangler.additionalInfomation = "WIS"
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
                'errorWrangler.projectSubTypeIdentifier = "Pigeon"
                Return errorWrangler.wrangleError()
            Catch e As Exception
                Return False
            End Try
        End Function
        <Extension()>
        Public Function wrangle(ByVal ex As Exception, ByVal projectType As wrangler.enums.ProjectTypes, ByVal severityLevelType As wrangler.enums.SeverityLevelTypes, ByVal userID As String, ByVal requiresImmediateNotification As Boolean) As Boolean
            Try
                Dim errorWrangler As New wrangler.errorWrangler
                errorWrangler.wrangleType = wrangler.enums.WrangledTypes.Error
                errorWrangler.projectType = wrangler.enums.ProjectTypes.WIS
                errorWrangler.additionalInfomation = "WIS"
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
                'errorWrangler.projectSubTypeIdentifier = "Pigeon"
                Return errorWrangler.wrangleError()
            Catch e As Exception
                Return False
            End Try
        End Function
        <Extension()>
        Public Function wrangle(ByVal ex As Exception, ByVal severityLevelType As wrangler.enums.SeverityLevelTypes, ByVal userID As String, ByVal requiresImmediateNotification As Boolean, ByVal additionalInformation As String) As Boolean
            Try
                Dim errorWrangler As New wrangler.errorWrangler
                errorWrangler.wrangleType = wrangler.enums.WrangledTypes.Error
                errorWrangler.projectType = wrangler.enums.ProjectTypes.WIS
                errorWrangler.severityLevelType = severityLevelType
                errorWrangler.errorCode = String.Empty
                errorWrangler.errorSummaryMsg = ex.Message
                errorWrangler.stackTrace = ex.StackTrace
                Dim sb As New StringBuilder
                sb.Append("Error Name: " + ex.GetType().FullName + " - Url: " + HttpContext.Current.Request.RawUrl.ToString())
                sb.Append("Target: " + ex.TargetSite.Name + " " + ex.TargetSite.DeclaringType.FullName + " - ")
                sb.Append("Source: " + ex.Source)
                errorWrangler.additionalInfomation = IIf(additionalInformation.IsNullOrEmpty(), sb.ToString(), "Custom Additional Info - " + additionalInformation + " " + sb.ToString()).ToString()
                'errorWrangler.projectSubTypeIdentifier = "Pigeon"
                errorWrangler.displayTextForErrorMsg = ex.Message
                errorWrangler.userID = userID
                errorWrangler.requiresImmediateNotification = requiresImmediateNotification
                errorWrangler.dateEntered = DateTime.Now
                Return errorWrangler.wrangleError()
            Catch e As Exception
                Return False
            End Try
        End Function
        <Extension()>
        Public Function wrangle(ByVal ex As Exception, ByVal severityLevelType As wrangler.enums.SeverityLevelTypes, ByVal userID As String, ByVal requiresImmediateNotification As Boolean, ByVal additionalInformation As String, ByVal projectSubTypeIdentifier As String) As Boolean
            Try
                Dim errorWrangler As New wrangler.errorWrangler
                errorWrangler.wrangleType = wrangler.enums.WrangledTypes.Error
                errorWrangler.projectType = wrangler.enums.ProjectTypes.WIS
                errorWrangler.severityLevelType = severityLevelType
                errorWrangler.errorCode = String.Empty
                errorWrangler.errorSummaryMsg = ex.Message
                errorWrangler.stackTrace = ex.StackTrace
                Dim sb As New StringBuilder
                sb.Append("Error Name: " + ex.GetType().FullName + " - Url: " + HttpContext.Current.Request.RawUrl.ToString())
                sb.Append("Target: " + ex.TargetSite.Name + " " + ex.TargetSite.DeclaringType.FullName + " - ")
                sb.Append("Source: " + ex.Source)
                errorWrangler.additionalInfomation = IIf(additionalInformation.IsNullOrEmpty(), sb.ToString(), "Custom Additional Info - " + additionalInformation + " " + sb.ToString()).ToString()
                errorWrangler.displayTextForErrorMsg = ex.Message
                errorWrangler.userID = userID
                errorWrangler.requiresImmediateNotification = requiresImmediateNotification
                errorWrangler.dateEntered = DateTime.Now
                'errorWrangler.projectSubTypeIdentifier = projectSubTypeIdentifier
                Return errorWrangler.wrangleError()
            Catch e As Exception
                Return False
            End Try
        End Function
#End Region

    End Module


End Namespace

