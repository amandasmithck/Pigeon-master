Public Class Orders1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Client") = "CK" Then Response.Redirect("CKOrders.aspx")
    End Sub

    Private Sub WebForm1_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit

    End Sub
End Class