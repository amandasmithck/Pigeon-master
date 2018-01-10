Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon
Imports Pigeon.TransferCaseWebService
Imports Pigeon.EngineWebService
Imports Pigeon.TransmissionWebService
Imports Pigeon.DifferentialWebService
Imports Pigeon.ManualTransmissionWebService
Imports Pigeon.CertifiedLookup
Imports System.Math
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
 Public Class HomeWebService
    Inherits System.Web.Services.WebService
    Public Class Applications
        Public Property Year() As String
            Get
                Return m_Year
            End Get
            Set(ByVal value As String)
                m_Year = value
            End Set
        End Property
        Private m_Year As String
        Public Property Make() As String
            Get
                Return m_Make
            End Get
            Set(ByVal value As String)
                m_Make = value
            End Set
        End Property
        Private m_Make As String
        Public Property Model() As String
            Get
                Return m_Model
            End Get
            Set(ByVal value As String)
                m_Model = value
            End Set
        End Property
        Private m_Model As String
        Public Property Desc() As String
            Get
                Return m_Desc
            End Get
            Set(ByVal value As String)
                m_Desc = value
            End Set
        End Property
        Private m_Desc As String
        Public Property TagID() As String
            Get
                Return m_TagID
            End Get
            Set(ByVal value As String)
                m_TagID = value
            End Set
        End Property
        Private m_TagID As String
    End Class

    Public Class Announcements
        Public Property Announcement() As String
            Get
                Return m_Announcement
            End Get
            Set(ByVal value As String)
                m_Announcement = value
            End Set
        End Property
        Private m_Announcement As String
    End Class
     <WebMethod()> _
    Public Function SearchTransmissionByPartNo(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(SearchTransmissionByPartNumber(partno, client, name, Nothing))
    End Function

    <WebMethod()> _
    Public Function SearchManualTransmissionByPartNo(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(SearchManualTransmissionByPartNumber(partno, client, name))
    End Function

    <WebMethod()> _
    Public Function SearchEngineByPartNo(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(SearchEngineByPartNumber(partno, client, name))
    End Function


    <WebMethod()> _
    Public Function SearchTransferByPartNo(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(SearchTransferByPartNumber(partno, client, name))
    End Function
    <WebMethod()> _
    Public Function SearchDiffByPartNo(ByVal partno As String, ByVal client As String, ByVal name As String)
        Dim js As New JavaScriptSerializer
        Return js.Serialize(SearchDiffByPartNumber(partno, client, name))
    End Function

    <WebMethod()>
    Public Function GetAnnouncement(ByVal Credential As String, ByVal client As String)
        Dim AnnounceList As New List(Of Announcements)
        Dim js As New JavaScriptSerializer()
        Dim NowDateTime As DateTime = DateTime.Now


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlAnnounceList As New SqlCommand("SELECT Announcement FROM tblAnnouncement WHERE DateStart <= '" & NowDateTime & "' AND DateEnd >= '" & NowDateTime & "' AND Credentials = '" & Credential & "'", conn)
            conn.Open()
            Using r2 As SqlDataReader = sqlAnnounceList.ExecuteReader()

                While r2.Read()
                    Dim response As New Announcements
                    response.Announcement = r2("Announcement")
                    AnnounceList.Add(response)
                End While
            End Using
        End Using


        Return js.Serialize(AnnounceList)
    End Function

End Class