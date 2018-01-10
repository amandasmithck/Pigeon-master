Imports System.Web
Imports System.Web.Services
Imports System
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports net.openstack.Core.Domain
Imports net.openstack.Providers.Rackspace

'Imports com.mosso.cloudfiles
'Imports com.mosso.cloudfiles.domain



Public Class DocumentHandler
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Try
            Dim orderid = context.Request.Form("orderid")
            Dim uploadedby = context.Request.Form("uploadedby")
            If context.Request.Files.Count > 0 Then

                'create temp directory if doesnt exist
                Dim path = context.Server.MapPath("~/TempDocs")
                If Directory.Exists(path) = False Then Directory.CreateDirectory(path)

                'save file
                Dim file = context.Request.Files(0)
                Dim filename = System.IO.Path.Combine(path, file.FileName)
                file.SaveAs(filename)
                UploadFile(orderid, file.FileName, filename, uploadedby)

                'response back to front end
                context.Response.ContentType = "text/plain"
                Dim serializer As New JavaScriptSerializer

                Dim result = New With { _
                    Key .name = file.FileName _
                }

                context.Response.Write(serializer.Serialize(result))

            End If
        Catch Ex As Exception
            'context.Response.Write(ex.ToString())
        End Try
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub UploadFile(ByVal orderid As Long, ByVal filename As String, ByVal fullpath As String, ByVal uploadedby As String)

        'now push to cloud

        Dim cloudIdentity = New CloudIdentity()
        cloudIdentity.APIKey = "2d4127e42c1dae7cedcd4918322396de"
        cloudIdentity.Username = "ckautoparts"


        'create container
        Dim cloudFilesProvider = New CloudFilesProvider(cloudIdentity)
        Dim createContainerResponse As ObjectStore = cloudFilesProvider.CreateContainer("ck-" & orderid)
        cloudFilesProvider.EnableCDNOnContainer("ck-" & orderid, 900)
        Dim containerHeader = cloudFilesProvider.GetContainerCDNHeader("ck-" & orderid)



        'update cdn
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim strsql As String = "update tblorder set cdn = '" & containerHeader.CDNUri & "' where orderid = " & orderid
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using
        cloudFilesProvider.CreateObjectFromFile("ck-" & orderid, fullpath)


        'insert in db
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim strsql As String = "insert into tbldocuments(orderid, uploaddate, filename, uploadedby) values (" & orderid & ", { fn now() }, '" & filename & "','" & uploadedby & "')"
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        'delete local file
        File.Delete(fullpath)
    End Sub
End Class