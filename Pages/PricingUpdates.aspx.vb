Imports Pigeon.Pigeon
Public Class pricing_1

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Client") <> "CK" Or Not PidgeonMembership.CurrentUserIsInRole("Admin") Then
            Response.Redirect("Home.aspx")
        End If
    End Sub

End Class


