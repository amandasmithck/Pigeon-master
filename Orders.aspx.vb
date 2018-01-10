Public Class Orders1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub WebForm1_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Application("Folder") & Application("Client") & "Orders.master"
    End Sub
End Class