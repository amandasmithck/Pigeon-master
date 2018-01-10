Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class TransWebService2
    Inherits System.Web.Services.WebService
    Public Class SMTCUSER
        Public Property PersonID As Long
            Get
                Return m_PersonID
            End Get
            Set(ByVal value As Long)
                m_PersonID = value
            End Set
        End Property
        Private m_PersonID As Long
        Public Property Key As String
            Get
                Return m_Key
            End Get
            Set(ByVal value As String)
                m_Key = value
            End Set
        End Property
        Private m_Key As String



    End Class



    <WebMethod()> _
    Public Function CheckUser(ByVal user As String) As String
        'Dim found As Boolean = False
        'Dim js As New JavaScriptSerializer()
        ''Dim consumeWebService As CertifiedSignIn.People
        ''consumeWebService = New CertifiedSignIn.People
        ''Dim ds As DataSet = consumeWebService.GetPeople("P6G2:DDPR1@F9OL160W5BJ5QV=XU=87LFYR8ZKU3A@VGOMDJ=O")

        ''Dim dt As DataTable = ds.Tables(0)
        ''Dim dc As DataColumn
        ''Dim dr As DataRow

        ''For Each dr In dt.Rows
        ''    If dr(1) = user Then
        ''        found = True
        ''        Return js.Serialize(dr(0))
        ''        Exit Function
        ''    End If

        ''Next
        ''Return js.Serialize(0)
        'Dim s1 As New SMTCUSER

        ''get key
        'Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
        '    Dim sqlComm As New SqlCommand("SELECT certifiedkey from tblPigeonClients where client = '" & client & "'", conn)
        '    conn.Open()
        '    s1.Key = sqlComm.ExecuteScalar
        'End Using

        'Using conn As New SqlConnection(GetClientConnectionString(client))
        '    Dim sqlComm2 As New SqlCommand("SELECT personid from tblSMTCUsers where username = '" & user & "'", conn)
        '    conn.Open()
        '    Using r As SqlDataReader = sqlComm2.ExecuteReader()

        '        While r.Read()

        '            'If r("Username") = user Then
        '            found = True
        '            s1.PersonID = js.Serialize(r("PersonID"))
        '            Return js.Serialize(s1)
        '        end using
        'End Using

        'Exit Function
        ''End If

        'End While
        'end using
        'end using

        'Return js.Serialize(0)
    End Function

End Class