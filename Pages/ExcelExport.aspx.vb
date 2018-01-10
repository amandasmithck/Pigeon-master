Public Class ExcelExport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=data.xls")
        Response.ContentType = "application/ms-excel"
        Response.Charset = ""
        Page.EnableViewState = False
        Dim sw As New System.IO.StringWriter()
        Dim hw As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(sw)
        hw.WriteLine(Request.Form("datatodisplay").ToString)

        Response.Write(sw.ToString())
        Response.End()
        Response.Flush()
    End Sub

End Class