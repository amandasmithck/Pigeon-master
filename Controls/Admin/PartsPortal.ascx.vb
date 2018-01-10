Public Class PartsPortal2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.dsCustomers.ConnectionString = ConfigurationManager.ConnectionStrings(Session("ConnectionString")).ConnectionString
        Me.cboCustomer.DataBind()

        Me.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings(Session("ConnectionString")).ConnectionString
        Me.cbomake2.DataBind()

    End Sub

End Class