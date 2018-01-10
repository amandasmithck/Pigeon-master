Imports System.Web
Imports Pigeon.Pigeon
Imports System.Web.Services
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Data.SqlClient

Public Class PrintInvoiceHandler
    Implements System.Web.IHttpHandler
    Dim intIndex As Long = 0

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim viewer As Microsoft.Reporting.WebForms.ReportViewer
        Dim localReport As Microsoft.Reporting.WebForms.LocalReport
        
        'viewer = New ReportViewer()
        'localReport = viewer.LocalReport
        'Dim sReport As StringReader = GetReportStream("rptPrintInvoice")
        'Dim dsPrintInvoice = GetData("D89670", "usp_PrintInvoice", "Invoice")
        'Dim dsPrintInvoiceHeader = GetData("D89670", "usp_PrintInvoiceHeader", "InvoiceHeader")

        'With localReport
        '    .LoadReportDefinition(sReport)
        '    .DataSources.Clear()
        '    .DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dsPrintInvoice.Tables("Invoice")))
        '    .DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", dsPrintInvoiceHeader.Tables("InvoiceHeader")))

        'End With

        viewer = New ReportViewer()
        localReport = viewer.LocalReport
        Dim sReport As StringReader = GetReportStream("rptPrintInvoiceBatch")
        Dim subReport As StringReader = GetReportStream("rptPrintInvoice")
        Dim dsPrintInvoiceBatch = GetBatchData("3/1/13", "4/1/13", 142, "usp_PrintInvoiceBatch", "Invoices")

        With localReport
            .LoadReportDefinition(sReport)
            .LoadSubreportDefinition("rptPrintInvoice", subReport)
            .DataSources.Clear()
            .DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dsPrintInvoiceBatch.Tables("Invoices")))


        End With
        AddHandler viewer.LocalReport.SubreportProcessing, AddressOf Me.MySubreportEventHandler
        'Export to PDF
        Dim mimeType As String
        Dim encoding As String
        Dim fileNameExtension As String
        Dim streams As String()
        Dim warnings As Microsoft.Reporting.WebForms.Warning()


        Dim byteFile() As Byte = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, fileNameExtension, streams, warnings)



        context.Response.Clear()
        context.Response.ContentType = "application/pdf"
        context.Response.OutputStream.Write(byteFile, 0, byteFile.Length)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    Private Function GetData(ByVal invoiceno As String, ByVal sp As String, ByVal table As String) As DataSet
        Dim sqlDA As New SqlDataAdapter
        Dim myDS As New DataSet
        Using conn As New SqlConnection(GetClientConnectionString("CK"))
            Dim sqlComm As New SqlCommand(sp, conn)
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar)
            sqlComm.Parameters("@InvoiceNo").Value = invoiceno
            conn.Open()
            sqlDA.SelectCommand = sqlComm
            sqlDA.Fill(myDS, table)


            Return myDS

        End Using
    End Function
    Private Function GetBatchData(ByVal fromdate As String, ByVal todate As String, ByVal companyid As Long, ByVal sp As String, ByVal table As String) As DataSet
        Dim sqlDA As New SqlDataAdapter
        Dim myDS As New DataSet
        Using conn As New SqlConnection(GetClientConnectionString("CK"))
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
        Dim dsPrintInvoice = GetData(e.Parameters("InvoiceNo").Values(0), "usp_PrintInvoice", "Invoice")
        Dim dsPrintInvoiceHeader = GetData(e.Parameters("InvoiceNo").Values(0), "usp_PrintInvoiceHeader", "InvoiceHeader")
        e.DataSources.Add(New ReportDataSource("DataSet1", dsPrintInvoice.Tables("Invoice")))
        e.DataSources.Add(New ReportDataSource("DataSet2", dsPrintInvoiceHeader.Tables("InvoiceHeader")))



    End Sub
End Class