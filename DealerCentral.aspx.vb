Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Public Class DealerCentral
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Client") = "DealerCentral"
    End Sub

    Private Sub AutowaySelected(grid As GridView)
        Dim row As GridViewRow = grid.SelectedRow

        'get username for that hyperion
        Dim username
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("Autoway"))
            Dim sqlComm As New SqlCommand("select username from tbldealercentral inner join aspnet_Membership on tbldealercentral.Hyperion= aspnet_membership.customerno inner join aspnet_users on aspnet_membership.userid = aspnet_users.userid where hyperion = '" & CType(row.FindControl("lblHyperion"), Label).Text & "'", conn)
            conn.Open()
            username = sqlComm.ExecuteScalar()
        End Using

        Response.Cookies.Remove(FormsAuthentication.FormsCookieName)
        FormsAuthentication.SetAuthCookie(username, False)

        Session("Client") = "Autoway"
        Session("Folder") = "Assets/Autoway/"

        Response.Redirect("Pages\Home.aspx")
    End Sub
    Private Sub GOSelected(grid As GridView)
        Dim row As GridViewRow = grid.SelectedRow

        'get username for that hyperion
        Dim username
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("GO"))
            Dim sqlComm As New SqlCommand("select username from tbldealercentral inner join aspnet_Membership on tbldealercentral.Hyperion= aspnet_membership.customerno inner join aspnet_users on aspnet_membership.userid = aspnet_users.userid where hyperion = '" & CType(row.FindControl("lblHyperion"), Label).Text & "'", conn)
            conn.Open()
            username = sqlComm.ExecuteScalar()
        End Using

        Response.Cookies.Remove(FormsAuthentication.FormsCookieName)
        FormsAuthentication.SetAuthCookie(username, False)

        ConnectionStrings.SetSessionConnectionStringsAndProviders("go")

        Session("Folder") = "Assets/GO/"
        Response.Redirect("Pages\Home.aspx")
    End Sub


    Private Sub grdAL_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdAL.SelectedIndexChanged
        AutowaySelected(grdAL)
    End Sub

    Private Sub grdFL_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdFL.SelectedIndexChanged
        AutowaySelected(grdFL)
    End Sub

    Private Sub grdGA_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdGA.SelectedIndexChanged
        AutowaySelected(grdGA)
    End Sub

    Private Sub grdIL_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdIL.SelectedIndexChanged
        AutowaySelected(grdIL)
    End Sub

    Private Sub grdMD_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdMD.SelectedIndexChanged
        AutowaySelected(grdMD)
    End Sub

    Private Sub grdMN_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdMN.SelectedIndexChanged
        AutowaySelected(grdMN)
    End Sub

    Private Sub grdOH_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdOH.SelectedIndexChanged
        AutowaySelected(grdOH)
    End Sub

    Private Sub grdTN_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdTN.SelectedIndexChanged
        AutowaySelected(grdTN)
    End Sub

    Private Sub grdVA_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdVA.SelectedIndexChanged
        AutowaySelected(grdVA)
    End Sub

    Private Sub grdAZ_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdAZ.SelectedIndexChanged
        GOSelected(grdAZ)
    End Sub

    Private Sub grdCA_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdCA.SelectedIndexChanged
        GOSelected(grdCA)
    End Sub

    Private Sub grdCO_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdCO.SelectedIndexChanged
        GOSelected(grdCO)
    End Sub

    Private Sub grdNV_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdNV.SelectedIndexChanged
        GOSelected(grdNV)
    End Sub

    Private Sub grdTX_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdTX.SelectedIndexChanged
        GOSelected(grdTX)
    End Sub

    Private Sub grdWA_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grdWA.SelectedIndexChanged
        GOSelected(grdWA)
    End Sub
End Class