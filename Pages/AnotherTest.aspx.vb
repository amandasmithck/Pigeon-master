Imports System.Data.SqlClient
Imports System.Web
Imports Pigeon.Pigeon


Public Class AnotherTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Label1.Text = "Starting!"
        Dim masterPigeonClasses As New Pigeon1
        Dim CurrentPass, TestPass As String
        Dim rValue As String = String.Empty
        Dim intChecked As Integer = 0
        Dim intChanged As Integer = 0
        Dim ALlMembers As MembershipUserCollection = Membership.GetAllUsers()

        For Each U As MembershipUser In Membership.GetAllUsers()
            Try
                Label2.Text = "On User " & U.Email
                intChecked = intChecked + 1
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(ConnectionStrings.GetSpecificConnectionString(HttpContext.Current.Request.Url.Host)))
                    conn.Open()
                    Dim sqlComm As New SqlCommand("Select top 1 aspnet_Membership.PasswordFormat from aspnet_Membership where UserId=(select top 1 aspnet_Users.UserId from aspnet_Users where aspnet_Users.UserName='" + U.UserName + "')", conn)
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            rValue = r(0).ToString
                        End While
                    End Using
                    If (rValue = "0") And U.IsLockedOut = False Then
                        CurrentPass = U.GetPassword()
                        Using sqlComm2 As New SqlCommand("Update aspnet_Membership set aspnet_Membership.PasswordFormat = 2 where UserId=(select top 1 aspnet_Users.UserId from aspnet_Users where aspnet_Users.UserName='" + U.UserName + "')", conn)
                            sqlComm2.ExecuteNonQuery()
                        End Using
                        TestPass = U.ResetPassword()
                        U.ChangePassword(TestPass, CurrentPass)
                        Membership.UpdateUser(U)
                        intChanged = intChanged + 1
                    End If

                End Using
                Label3.Text = "Succeeded!"
                Debug.WriteLine("Checked-" & intChecked & ", Changed-" & intChanged)
            Catch Ex As Exception
                Label4.Text = "ERROR: " & Ex.Message
            End Try
        Next

    End Sub

End Class