Imports System.Data.SqlClient
Imports Pigeon.Pigeon

Public Class PartsPortal3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Client") = "CK" Then Response.Redirect("CKPartsPortal.aspx")
        If Session("Client") = "AutoNation" Then
            Me.CustomerPartsPortal.Visible = False
        ElseIf Page.User.IsInRole("Admin") Then
            Me.CustomerPartsPortal.Visible = False
        ElseIf Page.User.IsInRole("Customer") Then
            Me.AdminPartsPortal.Visible = False
        Else
            Me.CustomerPartsPortal.Visible = False
            Me.AdminPartsPortal.Visible = False
        End If
    End Sub

    Private Sub WebForm1_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

    End Sub

End Class