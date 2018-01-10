Imports System.Web
Imports Pigeon.Pigeon
Imports System.Web.Services
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Data.SqlClient
Public Class ViewInvoice
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim viewer As Microsoft.Reporting.WebForms.ReportViewer
        Dim localReport As Microsoft.Reporting.WebForms.LocalReport

        If Request.QueryString("invoiceid") Is Nothing = False Then 'individual invoice
            viewer = New ReportViewer()
            localReport = viewer.LocalReport
            Dim sReport As StringReader = GetReportStream("rptPrintInvoice")
            Dim dsPrintInvoice = GetData(Request.QueryString("invoiceid"), "usp_PrintInvoice", "Invoice")
            Dim dsPrintInvoiceHeader = GetData(Request.QueryString("invoiceid"), "usp_PrintInvoiceHeader", "InvoiceHeader")
            Dim paramInvoice As New ReportParameter("InvoiceID", Request.QueryString("invoiceid"))

            With localReport
                .LoadReportDefinition(sReport)
                .DataSources.Clear()
                .DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dsPrintInvoice.Tables("Invoice")))
                .DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", dsPrintInvoiceHeader.Tables("InvoiceHeader")))
                .SetParameters(paraminvoice)

            End With
        Else 'batch invoices
            viewer = New ReportViewer()
            localReport = viewer.LocalReport
            Dim sReport As StringReader = GetReportStream("rptPrintInvoiceBatch")
            Dim subReport As StringReader = GetReportStream("rptPrintInvoice")
            Dim dsPrintInvoiceBatch = GetBatchData(Request.QueryString("from"), Request.QueryString("to"), Request.QueryString("companyid"), "usp_PrintInvoiceBatch", "Invoices")


            With localReport
                .LoadReportDefinition(sReport)
                .LoadSubreportDefinition("rptPrintInvoice", subReport)
                .DataSources.Clear()
                .DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dsPrintInvoiceBatch.Tables("Invoices")))


            End With
            AddHandler viewer.LocalReport.SubreportProcessing, AddressOf Me.MySubreportEventHandler

        End If
        'Export to PDF
        Dim mimeType As String = String.Empty
        Dim encoding As String = String.Empty
        Dim fileNameExtension As String = String.Empty
        Dim streams As String()
        Dim warnings As Microsoft.Reporting.WebForms.Warning()


        Dim byteFile() As Byte = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, fileNameExtension, streams, warnings)

        Context.Response.Clear()
        Context.Response.ContentType = "application/pdf"
        Context.Response.OutputStream.Write(byteFile, 0, byteFile.Length)
    End Sub
    Private Function GetData(ByVal invoiceid As String, ByVal sp As String, ByVal table As String) As DataSet
        Dim sqlDA As New SqlDataAdapter
        Dim myDS As New DataSet
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
            Dim sqlComm As New SqlCommand(sp, conn)
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.Add("@InvoiceID", SqlDbType.NVarChar)
            sqlComm.Parameters("@InvoiceID").Value = invoiceid
            conn.Open()
            sqlDA.SelectCommand = sqlComm
            sqlDA.Fill(myDS, table)


            Return myDS

        End Using
    End Function
    Private Function GetBatchData(ByVal fromdate As String, ByVal todate As String, ByVal companyid As Long, ByVal sp As String, ByVal table As String) As DataSet
        Dim sqlDA As New SqlDataAdapter
        Dim myDS As New DataSet
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
            Dim sqlComm As New SqlCommand(sp, conn)
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.Add("@fromdate", SqlDbType.SmallDateTime)
            sqlComm.Parameters("@fromdate").Value = fromdate
            sqlComm.Parameters.Add("@todate", SqlDbType.SmallDateTime)
            sqlComm.Parameters("@todate").Value = todate
            sqlComm.Parameters.Add("@companyid", SqlDbType.Int)
            sqlComm.Parameters("@companyid").Value = companyid
            conn.Open()
            sqlDA.SelectCommand = sqlComm
            sqlDA.Fill(myDS, table)


            Return myDS

        End Using
    End Function
    Private Function GetReportStream(ReportFile As String) As StringReader
        If Not ReportFile.ToLower().EndsWith(".rdlc") Then
            ReportFile += ".rdlc"
        End If
        Dim read As StreamReader = File.OpenText(HttpContext.Current.Server.MapPath("~/" & ReportFile))
        Dim content As String = read.ReadToEnd()
        read.Close()
        read.Dispose()

        Dim sr As New StringReader(content)
        Return sr
    End Function
    Private Sub MySubreportEventHandler(ByVal sender As Object, ByVal e As SubreportProcessingEventArgs)

        e.DataSources.Clear()
        Dim dsPrintInvoice = GetData(e.Parameters("InvoiceID").Values(0), "usp_PrintInvoice", "Invoice")
        Dim dsPrintInvoiceHeader = GetData(e.Parameters("InvoiceID").Values(0), "usp_PrintInvoiceHeader", "InvoiceHeader")
        e.DataSources.Add(New ReportDataSource("DataSet1", dsPrintInvoice.Tables("Invoice")))
        e.DataSources.Add(New ReportDataSource("DataSet2", dsPrintInvoiceHeader.Tables("InvoiceHeader")))



    End Sub
End Class