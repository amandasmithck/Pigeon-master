Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class InventoryWebService
    Inherits System.Web.Services.WebService

    Public Class AvailableStock
        Public Property ID As Long
            Get
                Return m_ID
            End Get
            Set(ByVal value As Long)
                m_ID = value
            End Set
        End Property
        Private m_ID As Long
        Public Property Client As String
            Get
                Return m_Client
            End Get
            Set(ByVal value As String)
                m_Client = value
            End Set
        End Property
        Private m_Client As String
        Public Property Part As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String

        Public Property SN As String
            Get
                Return m_SN
            End Get
            Set(ByVal value As String)
                m_SN = value
            End Set
        End Property
        Private m_SN As String

        Public Property PartType As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String

        Public Property PartTypeID As Long
            Get
                Return m_PartTypeID
            End Get
            Set(ByVal value As Long)
                m_PartTypeID = value
            End Set
        End Property
        Private m_PartTypeID As Long

        Public Property WarehouseID As Long
            Get
                Return m_WarehouseID
            End Get
            Set(ByVal value As Long)
                m_WarehouseID = value
            End Set
        End Property
        Private m_WarehouseID As Long

        Public Property Warehouse As String
            Get
                Return m_Warehouse
            End Get
            Set(ByVal value As String)
                m_Warehouse = value
            End Set
        End Property
        Private m_Warehouse As String

        Public Property CKCost As Decimal
            Get
                Return m_CKCost
            End Get
            Set(ByVal value As Decimal)
                m_CKCost = value
            End Set
        End Property
        Private m_CKCost As Decimal

    End Class
    <WebMethod()>
    Public Function GetAvailableRemanStock(ByVal client As String) As List(Of AvailableStock)
        Dim stock As New List(Of AvailableStock)
        If client <> "CK" Then Return stock

        'loop through each client
        Dim clients As New List(Of String)
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT client from tblPigeonClients", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    clients.Add(r("client"))
                End While
            End Using
        End Using

        For Each c As String In clients
            Try
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(c))
                    Dim sqlComm As New SqlCommand("exec usp_getavailablestock", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim s As New AvailableStock
                            Dim objType As Type = s.GetType()
                            Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                            For Each prop As System.Reflection.PropertyInfo In properties
                                Try
                                    prop.SetValue(s, r(prop.Name), Nothing)

                                Catch ex As Exception

                                End Try
                            Next
                            stock.Add(s)
                        End While
                    End Using
                End Using
            Catch Ex As Exception
            End Try

        Next

        Return stock
    End Function

    <WebMethod()>
    Public Function AssignRemanPart(ByVal orderid As Long, ByVal part As AvailableStock) As Boolean
        If IsNothing(part) Or IsNothing(orderid) Or orderid = 0 Then Return False

        Try
            'update sn and part number
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
                Dim sqlComm As New SqlCommand(String.Format("exec usp_AssignRemanPart {0}, '{1}', {2}, {3}", orderid, part.SN, part.WarehouseID, part.PartTypeID), conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            'assign ckorder to stock
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(part.Client))
                Dim sqlComm As New SqlCommand(String.Format("Update tblStock set ckorderid={0} where id={1}", orderid, part.ID), conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function


    <WebMethod()>
    Public Function AssignRemanPart(ByVal orderid As Long, ByVal availableStock As AvailableStock, ByVal partID As Integer) As Boolean
        If IsNothing(availableStock) Or IsNothing(orderid) Or orderid = 0 Then Return False

        Try
            'update sn and part number
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand(String.Format("exec usp_AssignRemanPart2 {0}, '{1}', {2}, {3}, {4}", orderid, availableStock.SN, availableStock.WarehouseID, availableStock.PartTypeID, partID), conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            'assign ckorder to stock
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
                Dim sqlComm As New SqlCommand(String.Format("Update tblStock set ckorderid={0} where id={1}", orderid, availableStock.ID), conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

End Class