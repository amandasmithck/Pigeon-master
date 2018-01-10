Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class QuickPricingWebService
    Inherits System.Web.Services.WebService
    Public Class QuickPrice
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set(ByVal value As String)
                m_Company = value
            End Set
        End Property
        Private m_Company As String
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property Family() As String
            Get
                Return m_Family
            End Get
            Set(ByVal value As String)
                m_Family = value
            End Set
        End Property
        Private m_Family As String
        Public Property Price() As Decimal
            Get
                Return m_Price
            End Get
            Set(ByVal value As Decimal)
                m_Price = value
            End Set
        End Property
        Private m_Price As Decimal
    End Class
    <WebMethod()> _
    Public Function GetPricing(ByVal client As String)
        Dim q As New List(Of QuickPrice)
        Dim js As New JavaScriptSerializer
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("select company, parttype,family,price from tblpricelist inner join tblcompany on tblpricelist.customerno=tblcompany.Customerno order by company, parttype, family", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim q1 As New QuickPrice
                    q1.Company = r("company").ToString
                    q1.PartType = r("parttype").ToString
                    q1.Family = r("family").ToString
                    q1.Price = r("price").ToString
                    q.Add(q1)
                End While
            End Using
        End Using

        Return js.Serialize(q)
    End Function

End Class