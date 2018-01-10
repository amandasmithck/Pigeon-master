Public Class Tier1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub WebForm1_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        Me.MasterPageFile = Session("Folder") & Session("Client") & "Tier.master"

    End Sub

End Class