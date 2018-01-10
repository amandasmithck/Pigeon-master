Public Class CKPartsPortal1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub

    Protected Sub cboCompany_DataBind(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.DataBound
        Dim dt As New System.Data.DataTable()
        Dim dv = CType(dsCustomers.Select(DataSourceSelectArguments.Empty), System.Data.DataView)

        dt = dv.ToTable()
        For Each row As System.Data.DataRow In dt.Rows
            Me.cboCompany.Items.FindByValue(row.ItemArray(1).ToString()).Attributes.Add("ChargeOEMEOC", row.ItemArray(0).ToString())
        Next
    End Sub
End Class