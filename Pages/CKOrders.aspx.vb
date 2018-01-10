Public Class CKOrders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Client") <> "CK" Then Response.Redirect("Orders.aspx")
    End Sub

End Class