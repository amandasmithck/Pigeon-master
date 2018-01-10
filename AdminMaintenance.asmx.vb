Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.CKExtensions


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class AdminMaintenance
    Inherits System.Web.Services.WebService

    Public Class PartOrder

        'Private _PartID As Integer
        'Public Property PartID As Integer
        '    Get
        '        Return _PartID
        '    End Get
        '    Set(value As Integer)
        '        _PartID = value
        '    End Set
        'End Property
        Public Property PartID As Integer


        Private _orderID As Integer
        Public Property OrderID As Integer
            Get
                Return _orderID
            End Get
            Set(value As Integer)
                _orderID = value
            End Set
        End Property

        Private _PartNo As System.String
        Public Property PartNo As String
            Get
                Return _PartNo
            End Get
            Set(value As String)
                _PartNo = value
            End Set
        End Property
        Public Property PartDescription As String
        Public Property ArriveDate As String
        Public Property Defect As Boolean
        Public Property Incorrect As Boolean
    End Class
    Public Class OrderInfo

        Private _PartID As System.Int32
        Private _OrderID As System.Int32
        Private _PartNo As System.String
        Private _PartDescription As System.String
        Private _ArriveDate As System.DateTime
        Private _Defect As System.String
        Private _Incorrect As System.Boolean
    End Class


    <WebMethod()>
    Public Function GetOrderPartsCollection(ByVal orderid As String, ByVal client As String) 'As List(Of PartOrder)
        'Dim list As ArrayList = Nothing


        Dim strsql As String = "SELECT dbo.tblPartOrder.PartDescription, dbo.tblPartOrder.Incorrect, dbo.tblPartOrder.Defect, dbo.tblPartOrder.PartNo, dbo.tblPartOrder.PartID, dbo.tblPartOrder.ArriveDate from tblPartOrder WHERE dbo.tblPartOrder.OrderID = " & orderid
        Dim ds As DataSet = GlobalFunctions.ExecuteAdHocSql(client, strsql)


        Dim list = From r As DataRow In ds.Tables(0).Rows
                   Select New With {
                                                .PartDescription = r("PartDescription"),
                                                .Incorrect = If(IsDBNull(r("Incorrect")), False, r("Incorrect")),
                                                .Defect = If(IsDBNull(r("Defect")), False, r("Defect")),
                                                .PartNo = r("PartNo"),
                                                .PartID = r("PartID"),
                                                .ArriveDate = LinqDate(r("ArriveDate"))
                                                    }

        Try

            'list = (From r As DataRow In ds.Tables(0).Rows
            '        Select New PartOrder With {
            '                                        .PartDescription = r("PartDescription"),
            '                                        .Incorrect = r("Incorrect"),
            '                                        .Defect = r("Defect"),
            '                                        .PartNo = r("PartNo"),
            '                                        .PartID = r("PartID"),
            '                                        .ArriveDate = If(IsDBNull(r("ArriveDate")), Nothing, r("ArriveDate"))
            '                                          }).ToList()


            'Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            '    Dim sqlComm As New SqlCommand(strsql, conn)
            '    conn.Open()

            '    Using r As SqlDataReader = sqlComm.ExecuteReader()
            '        While r.Read()
            '            Dim p1 As New PartOrder
            '            p1.PartDescription = r("PartDescription")
            '            p1.Incorrect = r("Incorrect")
            '            p1.Defect = r("Defect")
            '            p1.PartNo = r("PartNo")
            '            p1.PartID = r("PartID")
            '            If r("ArriveDate") == "" Then
            '                p1.ArriveDate = If(r("ArriveDate") = DBNull.Value, String.Empty, r("ArriveDate").ToString())
            '            End If

            '            'Dim objType As Type = p1.GetType()
            '            'Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

            '            'For Each prop As System.Reflection.PropertyInfo In properties
            '            '    Try
            '            '        prop.SetValue(p1, r(prop.Name), Nothing)
            '            '    Catch ex As Exception
            '            '        Continue For
            '            '    End Try
            '            'Next
            '            list.Add(p1)
            '        End While
            '    End Using
            'End Using

        Catch ex As Exception
            Dim s As String = ex.Message
        End Try

        Return list
    End Function

    Private Function LinqDate(val As Object) As String
        If IsDBNull(val) Then Return Nothing
        Return (CDate(val)).ToString("MM/dd/yyyy")
    End Function

    <WebMethod()>
    Public Function PartOrderMaintenaceUpdate(ByVal part As PartOrder, ByVal client As String) As String
        Try
            Dim list As New List(Of OrderInfo)
            Dim js As New JavaScriptSerializer
            Dim sp As New System.Text.StringBuilder

            sp.Append("update tblPartOrder ")
            sp.Append("set defect =  " & IIf(part.Defect = True, 1, 0))
            sp.Append(", incorrect = " & IIf(part.Incorrect = True, 1, 0))
            ' sp.Append(", arriveDate = " & IIf(part.ArriveDate = "null", "null", " ' " + part.ArriveDate + " ' "))
            sp.Append(", arriveDate = " & IIf(String.IsNullOrEmpty(part.ArriveDate), "null ", " ' " + part.ArriveDate + " ' "))
            sp.Append("where partID = " & " ' " + part.PartID.ToString() + " ' ")

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand(sp.ToString(), conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()

            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


End Class