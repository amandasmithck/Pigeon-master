Imports System.Data.SqlClient
Imports Pigeon.Pigeon

Public Class PartsPortal4
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings(Session("ConnectionString")).ConnectionString

        If Session("Client") <> "CK" Then
            Dim autonation
            Using conn As New SqlConnection(GetClientConnectionString(Session("Client")))
                Dim sqlComm As New SqlCommand("SELECT autonation FROM tblCompany INNER JOIN aspnet_Membership ON tblCompany.CustomerNo = aspnet_Membership.CustomerNo INNER JOIN aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId WHERE aspnet_Users.UserName = '" & Page.User.Identity.Name & "'", conn)
                conn.Open()
                autonation = sqlComm.ExecuteScalar()
            End Using

            If autonation = "1" Then
                Me.autonation.Text = "yes"
            Else
                Me.autonation.Text = "no"
            End If
        End If
    End Sub

End Class