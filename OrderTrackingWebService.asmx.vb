Imports System.Web.Services
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
Imports System.Web.Script.Serialization
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class OrderTrackingWebService
    Inherits System.Web.Services.WebService

    Public Class OrderInfo
        Public Property OrderID() As Long
            Get
                Return m_OrderID
            End Get
            Set
                m_OrderID = Value
            End Set
        End Property
        Private m_OrderID As Long
        Public Property DateOrdered() As String
            Get
                Return m_DateOrdered
            End Get
            Set
                m_DateOrdered = Value
            End Set
        End Property
        Private m_DateOrdered As String
        Public Property ContractNo() As String
            Get
                Return m_ContractNo
            End Get
            Set
                m_ContractNo = Value
            End Set
        End Property
        Private m_ContractNo As String
        Public Property AuthorizationNo() As String
            Get
                Return m_AuthorizationNo
            End Get
            Set
                m_AuthorizationNo = Value
            End Set
        End Property
        Private m_AuthorizationNo As String
        Public Property PurchaseOrder() As String
            Get
                Return m_PurchaseOrder
            End Get
            Set
                m_PurchaseOrder = Value
            End Set
        End Property
        Private m_PurchaseOrder As String
        Public Property Vehicle() As String
            Get
                Return m_Vehicle
            End Get
            Set
                m_Vehicle = Value
            End Set
        End Property
        Private m_Vehicle As String
        Public Property VinNo() As String
            Get
                Return m_VinNo
            End Get
            Set
                m_VinNo = Value
            End Set
        End Property
        Private m_VinNo As String
        Public Property Mileage() As String
            Get
                Return m_Mileage
            End Get
            Set
                m_Mileage = Value
            End Set
        End Property
        Private m_Mileage As String
        Public Property AutoOwner() As String
            Get
                Return m_AutoOwner
            End Get
            Set
                m_AutoOwner = Value
            End Set
        End Property
        Private m_AutoOwner As String
        Public Property Company() As String
            Get
                Return m_Company
            End Get
            Set
                m_Company = Value
            End Set
        End Property
        Private m_Company As String
        Public Property AdjusterName() As String
            Get
                Return m_AdjusterName
            End Get
            Set
                m_AdjusterName = Value
            End Set
        End Property
        Private m_AdjusterName As String
        Public Property AdjusterEmail() As String
            Get
                Return m_AdjusterEmail
            End Get
            Set
                m_AdjusterEmail = Value
            End Set
        End Property
        Private m_AdjusterEmail As String
        Public Property Notes() As String
            Get
                Return m_Notes
            End Get
            Set
                m_Notes = Value
            End Set
        End Property
        Private m_Notes As String
        Public Property GrandTotal() As Decimal
            Get
                Return m_GrandTotal
            End Get
            Set
                m_GrandTotal = Value
            End Set
        End Property
        Private m_GrandTotal As Decimal
        Public Property PigeonVisible() As Boolean
            Get
                Return m_PigeonVisible
            End Get
            Set
                m_PigeonVisible = Value
            End Set
        End Property
        Private m_PigeonVisible As Boolean
        Public Property CKVisible() As Boolean
            Get
                Return m_CKVisible
            End Get
            Set
                m_CKVisible = Value
            End Set
        End Property
        Private m_CKVisible As Boolean
    End Class
    Public Class PartInfo

        Public Property Shop() As String
            Get
                Return m_Shop
            End Get
            Set
                m_Shop = Value
            End Set
        End Property
        Private m_Shop As String
        Public Property Contact() As String
            Get
                Return m_Contact
            End Get
            Set
                m_Contact = Value
            End Set
        End Property
        Private m_Contact As String
        Public Property Address() As String
            Get
                Return m_Address
            End Get
            Set
                m_Address = Value
            End Set
        End Property
        Private m_Address As String
        Public Property CityStateZip() As String
            Get
                Return m_CityStateZip
            End Get
            Set
                m_CityStateZip = Value
            End Set
        End Property
        Private m_CityStateZip As String
        Public Property Phone() As String
            Get
                Return m_Phone
            End Get
            Set
                m_Phone = Value
            End Set
        End Property
        Private m_Phone As String
        Public Property PartNo() As String
            Get
                Return m_PartNo
            End Get
            Set
                m_PartNo = Value
            End Set
        End Property
        Private m_PartNo As String
        Public Property PartDescription() As String
            Get
                Return m_PartDescription
            End Get
            Set
                m_PartDescription = Value
            End Set
        End Property
        Private m_PartDescription As String
        Public Property Quantity() As Long
            Get
                Return m_Quantity
            End Get
            Set
                m_Quantity = Value
            End Set
        End Property
        Private m_Quantity As Long
        Public Property Price() As String
            Get
                Return m_Price
            End Get
            Set
                m_Price = Value
            End Set
        End Property
        Private m_Price As String
        Public Property Core() As String
            Get
                Return m_Core
            End Get
            Set
                m_Core = Value
            End Set
        End Property
        Private m_Core As String
        Public Property Status() As String
            Get
                Return m_Status
            End Get
            Set
                m_Status = Value
            End Set
        End Property
        Private m_Status As String
        Public Property Tracking() As String
            Get
                Return m_Tracking
            End Get
            Set
                m_Tracking = Value
            End Set
        End Property
        Private m_Tracking As String
        Public Property ShipperName() As String
            Get
                Return m_ShipperName
            End Get
            Set
                m_ShipperName = Value
            End Set
        End Property
        Private m_ShipperName As String
        Public Property Shipper() As Long
            Get
                Return m_Shipper
            End Get
            Set
                m_Shipper = Value
            End Set
        End Property
        Private m_Shipper As Long
        Public Property ExpShipDate() As String
            Get
                Return m_ExpShipDate
            End Get
            Set
                m_ExpShipDate = Value
            End Set
        End Property
        Private m_ExpShipDate As String
        Public Property ArrivalDate() As String
            Get
                Return m_ArrivalDate
            End Get
            Set
                m_ArrivalDate = Value
            End Set
        End Property
        Private m_ArrivalDate As String
        Public Property ArriveDate() As String
            Get
                Return m_ArriveDate
            End Get
            Set
                m_ArriveDate = Value
            End Set
        End Property
        Private m_ArriveDate As String
    End Class


    <WebMethod>
    Public Function GetOrderInfo(ByVal orderid As Long, ByVal client As String)
        Dim js As New JavaScriptSerializer
        Dim order As New OrderInfo
        'ensure this orderid is for this user
        Dim strCustNo As String = ""
        If (GetUserRole(User.Identity.Name, client) <> "Admin") Then
            strCustNo = GetCustomerNo(User.Identity.Name, client)
            If strCustNo <> GetOrderCustomerNo(orderid, client) Then Return order
        End If
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strQuery As String
            If client = "CK" Then
                order.CKVisible = True
                order.PigeonVisible = False
                strQuery = "select Company,isnull(adjustername, 'None') as adjustername, isnull(adjusteremail,'N/A') as adjusteremail,OrderID,convert(nvarchar(50),dateordered,101) as DateOrdered,isnull(ContractNo,'N/A') as contractno,isnull(AuthorizationNo,'N/A') as authorizationno,Concat(AutoYear,' ',AutoMake,' ',AutoModel) as Vehicle, isnull(VinNo,'Unknown') as VinNo,Mileage,isnull(AutoOwner,'Unknown') as AutoOwner from tblOrder inner join tblCompany on tblOrder.CustomerNo=tblCompany.CustomerNo where orderID=" & orderid
            Else
                order.CKVisible = False
                order.PigeonVisible = True
                strQuery = "select '' as Company,'' as adjustername, email as adjusteremail,OrderID,convert(nvarchar(50),orderdate,101) as DateOrdered,'' as contractno,'' as authorizationno,PO as PurchaseOrder,Concat(AutoYear,' ',AutoMake,' ',AutoModel) as Vehicle, isnull(VinNo,'Unknown') as VinNo,Mileage,isnull(AutoOwner,'Unknown') as AutoOwner from tblOrders where orderID=" & orderid
            End If
            Dim sqlComm As New SqlCommand(strQuery, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim objType As Type = order.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(order, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                End While
            End Using
        End Using

        Return js.Serialize(order)
    End Function

    <WebMethod>
    Public Function GetParts(ByVal orderid As Long, ByVal client As String)
        Dim js As New JavaScriptSerializer
        Dim parts As New List(Of PartInfo)
        'ensure this orderid is for this user
        Dim strCustNo As String = ""
        If (GetUserRole(User.Identity.Name, client) <> "Admin") Then
            strCustNo = GetCustomerNo(User.Identity.Name, client)
            If strCustNo <> GetOrderCustomerNo(orderid, client) Then Return parts
        End If

        Dim strQuery As String
        If client = "CK" Then
            strQuery = "select dateordered, convert(nvarchar(50),expshipdate,101) as expshipdate,convert(nvarchar(50),arrivaldate,101) as arrivaldate,convert(nvarchar(50),arrivedate,101) as arrivedate,partid, partno, partdescription, quantity, servicer as shop, tblpartorder.address1 as address, tblpartorder.city + ', ' + tblpartorder.state + ' ' + tblpartorder.zip as citystatezip, convert(nvarchar(50),convert(money,sellprice)) as price, case when corevalue is null then convert(nvarchar(50),convert(money,coreprice)) else convert(nvarchar(50),convert(money,corevalue)) end as core, shipper, tblcompany.company as shippername,shippertrack as tracking, case when cancelled = 1 then 'Cancelled' else case when shippertrack is not null then 'Shipped' else case when dateordered is not null then 'Processed' else 'Processing' end end end as status from tblpartorder left outer join tblCompany on tblpartorder.shipper = tblcompany.companyid where orderid=" & orderid & " order by partid"
        Else
            strQuery = "SELECT TOP (100) PERCENT dbo.tblOrders.OrderDate AS dateordered, CONVERT(nvarchar(50), PartsManager.dbo.tblPartOrder.ExpShipDate, 101) AS expshipdate, CONVERT(nvarchar(50), PartsManager.dbo.tblPartOrder.ArrivalDate, 101) AS arrivaldate, CONVERT(nvarchar(50), PartsManager.dbo.tblPartOrder.ArriveDate, 101) AS arrivedate, dbo.tblOrderDetails.PartID, dbo.tblOrderDetails.Part AS partno, dbo.tblOrderDetails.Description AS partdescription, dbo.tblOrderDetails.Quantity, dbo.tblOrders.Shop, dbo.tblOrders.Address AS address, dbo.tblOrders.City + ', ' + dbo.tblOrders.State + ' ' + dbo.tblOrders.Zip AS citystatezip, CONVERT(nvarchar(50), CONVERT(money, dbo.tblOrderDetails.TheirPrice)) AS price, CONVERT(nvarchar(50), CONVERT(money, dbo.tblOrderDetails.CorePrice)) AS core, PartsManager.dbo.tblPartOrder.Shipper, PartsManager.dbo.tblCompany.Company AS shippername, PartsManager.dbo.tblPartOrder.ShipperTrack AS tracking, CASE WHEN cancelled = 1 THEN 'Cancelled' ELSE CASE WHEN shippertrack IS NOT NULL THEN 'Shipped' ELSE CASE WHEN tblpartorder.dateordered IS NOT NULL THEN 'Processed' ELSE 'Processing' END END END AS status, PartsManager.dbo.tblPartOrder.PartID AS Expr1 FROM PartsManager.dbo.tblCompany RIGHT OUTER JOIN PartsManager.dbo.tblPartOrder ON PartsManager.dbo.tblCompany.CompanyID = PartsManager.dbo.tblPartOrder.Shipper RIGHT OUTER JOIN dbo.tblOrders INNER JOIN dbo.tblOrderDetails ON dbo.tblOrders.OrderID = dbo.tblOrderDetails.OrderID ON PartsManager.dbo.tblPartOrder.PartID = dbo.tblOrderDetails.CKPartID WHERE (dbo.tblOrders.OrderID = " & orderid & ") ORDER BY dbo.tblOrderDetails.PartID "
        End If
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strQuery, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim part As New PartInfo
                    Dim objType As Type = part.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(part, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                    Select Case part.Shipper
                        Case 38, 49, 2505
                            part.Tracking = String.Format("<a href='https://www.fedex.com/apps/fedextrack/?tracknumbers={0}&cntry_code=us' target='_blank'>{0}</a>", part.Tracking)
                        Case 59
                            part.Tracking = String.Format("<a href='http://www2.rlcarriers.com/freight/shipping/shipment-tracing?pro={0}&docType=PRO' target='_blank'>{0}</a>", part.Tracking)
                        Case 65
                            part.Tracking = String.Format("<a href='http://wwwapps.ups.com/etracking/tracking.cgi?tracknum={0}' target='_blank'>{0}</a>", part.Tracking)
                        Case 72
                            part.Tracking = String.Format("<a href='http://www.saia.com/Tracing/AjaxProstatusByPro.aspx?m=2&UID=&PWD=&SID=VSKYR79152438&PRONum1={0}' target='_blank'>{0}</a>", part.Tracking)
                        Case 907
                            part.Tracking = String.Format("<a href='http://www.cevalogistics.com/ceva-trak?sv={0}' target='_blank'>{0}</a>", part.Tracking)
                    End Select
                    parts.Add(part)
                End While
            End Using
        End Using

        Return js.Serialize(parts)
    End Function


End Class