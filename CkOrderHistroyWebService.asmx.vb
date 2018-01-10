Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
 Public Class CkOrderHistroyWebService
    Inherits System.Web.Services.WebService

    Public Class History
        Public Property OrderID As String
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As String)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As String
        Public Property DateOrdered As String
            Get
                Return m_DateOrdered
            End Get
            Set(ByVal value As String)
                m_DateOrdered = value
            End Set
        End Property
        Private m_DateOrdered As String
        Public Property PartNo As String
            Get
                Return m_PartNo
            End Get
            Set(ByVal value As String)
                m_PartNo = value
            End Set
        End Property
        Private m_PartNo As String
        Public Property Description As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property Warranty As String
            Get
                Return m_Warranty
            End Get
            Set(ByVal value As String)
                m_Warranty = value
            End Set
        End Property
        Private m_Warranty As String
        Public Property ETA As String
            Get
                Return m_ETA
            End Get
            Set(ByVal value As String)
                m_ETA = value
            End Set
        End Property
        Private m_ETA As String
        Public Property Adjuster As String
            Get
                Return m_Adjuster
            End Get
            Set(ByVal value As String)
                m_Adjuster = value
            End Set
        End Property
        Private m_Adjuster As String
        Public Property ContractNo As String
            Get
                Return m_ContractNo
            End Get
            Set(ByVal value As String)
                m_ContractNo = value
            End Set
        End Property
        Private m_ContractNo As String
        Public Property AuthNo As String
            Get
                Return m_AuthNo
            End Get
            Set(ByVal value As String)
                m_AuthNo = value
            End Set
        End Property
        Private m_AuthNo As String
        Public Property Owner As String
            Get
                Return m_Owner
            End Get
            Set(ByVal value As String)
                m_Owner = value
            End Set
        End Property
        Private m_Owner As String
        Public Property VinNo As String
            Get
                Return m_VinNo
            End Get
            Set(ByVal value As String)
                m_VinNo = value
            End Set
        End Property
        Private m_VinNo As String
        Public Property Location As String
            Get
                Return m_Location
            End Get
            Set(ByVal value As String)
                m_Location = value
            End Set
        End Property
        Private m_Location As String
        Public Property Status As String
            Get
                Return m_Status
            End Get
            Set(ByVal value As String)
                m_Status = value
            End Set
        End Property
        Private m_Status As String
        Public Property Tracking As String
            Get
                Return m_Tracking
            End Get
            Set(ByVal value As String)
                m_Tracking = value
            End Set
        End Property
        Private m_Tracking As String
        Public Property Shipper As String
            Get
                Return m_Shipper
            End Get
            Set(ByVal value As String)
                m_Shipper = value
            End Set
        End Property
        Private m_Shipper As String
        Public Property CustomerNo As String
            Get
                Return m_CustomerNo
            End Get
            Set(ByVal value As String)
                m_CustomerNo = value
            End Set
        End Property
        Private m_CustomerNo As String
    End Class
    Public Class Order
        Public Property OrderID As String
            Get
                Return m_OrderID
            End Get
            Set(ByVal value As String)
                m_OrderID = value
            End Set
        End Property
        Private m_OrderID As String
        Public Property PartNo As String
            Get
                Return m_PartNo
            End Get
            Set(ByVal value As String)
                m_PartNo = value
            End Set
        End Property
        Private m_PartNo As String
        Public Property Description As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String
        Public Property Warranty As String
            Get
                Return m_Warranty
            End Get
            Set(ByVal value As String)
                m_Warranty = value
            End Set
        End Property
        Private m_Warranty As String
        Public Property Quantity As String
            Get
                Return m_Quantity
            End Get
            Set(ByVal value As String)
                m_Quantity = value
            End Set
        End Property
        Private m_Quantity As String
        Public Property SellPrice As String
            Get
                Return m_SellPrice
            End Get
            Set(ByVal value As String)
                m_SellPrice = value
            End Set
        End Property
        Private m_SellPrice As String
        Public Property CorePrice As String
            Get
                Return m_CorePrice
            End Get
            Set(ByVal value As String)
                m_CorePrice = value
            End Set
        End Property
        Private m_CorePrice As String
        Public Property TrackingNo As String
            Get
                Return m_TrackingNo
            End Get
            Set(ByVal value As String)
                m_TrackingNo = value
            End Set
        End Property
        Private m_TrackingNo As String
    End Class
    <WebMethod()> _
    Public Function GetCustNo(ByVal client As String, ByVal Username As String)
        Dim sqlComm As New SqlCommand
        Dim Custno As String
        Dim UserId As String

        Using conn As New SqlConnection(GetClientConnectionString(client))
            sqlComm = New SqlCommand("SELECT [UserId] FROM [dbo].[aspnet_Users] where UserName = '" & Username & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    UserId = r("UserId").ToString
                End While
            End Using
        End Using

        Using conn As New SqlConnection(GetClientConnectionString(client))
            sqlComm = New SqlCommand("SELECT [CustomerNo] FROM [dbo].[aspnet_Membership] where UserId = '" & UserId & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Custno = r("CustomerNo")
                End While
            End Using
        End Using

        Return Custno


    End Function
    <WebMethod()> _
    Public Function GetHistory(ByVal client As String, ByVal type As String, ByVal Username As String)
        Dim currentcustno As String
        currentcustno = GetCustNo(client, Username)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of History)
        Dim sqlComm As New SqlCommand

        Select Case type
            Case "New"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    sqlComm = New SqlCommand("SELECT Distinct dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblPartOrder.DateEntered, dbo.tblOrder.CustomerNo, dbo.tblOrder.AdjusterName, dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, dbo.tblOrder.AutoOwner, dbo.tblOrder.VinNo, dbo.tblPartOrder.City + N', ' + dbo.tblPartOrder.State AS CityState FROM dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID  WHERE (dbo.tblOrder.CustomerNo = '" & currentcustno & "') AND (dbo.tblPartOrder.ArriveDate IS NULL) AND (dbo.tblPartOrder.ArrivalDate IS NULL) AND (dbo.tblPartOrder.Cancelled = 0) ORDER BY dbo.tblOrder.OrderID OPTION (RECOMPILE)", conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()

                        While r.Read()
                            Dim H As New History()

                           
                                H.OrderID = r("OrderID").ToString


                           
                            If r("DateEntered") Is DBNull.Value Then
                                H.DateOrdered = ""
                            Else
                                H.DateOrdered = r("DateEntered").ToString
                            End If


                            'If r("PartNo") Is DBNull.Value Then
                            '    H.PartNo = ""
                            'Else
                            '    H.PartNo = r("PartNo").ToString
                            'End If

                            'If r("PartDescription") Is DBNull.Value Then
                            '    H.Description = ""
                            'Else
                            '    H.Description = r("PartDescription").ToString
                            'End If

                            'If r("Warranty") Is DBNull.Value Then
                            '    H.Warranty = ""
                            'Else
                            '    H.Warranty = r("Warranty").ToString
                            'End If

                           
                                H.Adjuster = r("AdjusterName").ToString


                           
                                H.CustomerNo = r("CustomerNo").ToString


                           
                                H.ContractNo = r("ContractNo").ToString


                            H.AuthNo = r("AuthorizationNo").ToString


                            H.Owner = r("AutoOwner").ToString


                            H.VinNo = r("VinNo").ToString


                          
                                H.Location = r("CityState").ToString


                            list.Add(H)
                        End While
                    End Using
                End Using

            Case "Inroute"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    sqlComm = New SqlCommand("SELECT Distinct dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblPartOrder.DateEntered, dbo.tblPartOrder.ArrivalDate, dbo.tblOrder.CustomerNo, dbo.tblOrder.AdjusterName, dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, dbo.tblOrder.AutoOwner, dbo.tblOrder.VinNo, dbo.tblPartOrder.City + N', ' + dbo.tblPartOrder.State AS CityState, dbo.tblPartOrder.ShipperStatus, dbo.tblPartOrder.ShipperTrack, dbo.tblCompany.Company FROM dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID LEFT OUTER JOIN dbo.tblCompany ON dbo.tblPartOrder.Shipper = dbo.tblCompany.CompanyID WHERE (dbo.tblOrder.CustomerNo = '" & currentcustno & "') AND (dbo.tblPartOrder.ArriveDate IS NULL) AND (dbo.tblPartOrder.ArrivalDate IS NOT NULL) AND (dbo.tblPartOrder.Cancelled = 0) ORDER BY dbo.tblOrder.OrderID OPTION (RECOMPILE)", conn)

                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()

                        While r.Read()
                            Dim H As New History()

                            If r("OrderID") Is DBNull.Value Then
                                H.OrderID = ""
                            Else
                                H.OrderID = r("OrderID").ToString
                            End If

                            If r("DateEntered") Is DBNull.Value Then
                                H.DateOrdered = ""
                            Else
                                H.DateOrdered = r("DateEntered").ToString
                            End If

                            'If r("PartNo") Is DBNull.Value Then
                            '    H.PartNo = ""
                            'Else
                            '    H.PartNo = r("PartNo").ToString
                            'End If

                            'If r("PartDescription") Is DBNull.Value Then
                            '    H.Description = ""
                            'Else
                            '    H.Description = r("PartDescription").ToString
                            'End If

                            'If r("Warranty") Is DBNull.Value Then
                            '    H.Warranty = ""
                            'Else
                            '    H.Warranty = r("Warranty").ToString
                            'End If

                            If r("AdjusterName") Is DBNull.Value Then
                                H.Adjuster = ""
                            Else
                                H.Adjuster = r("AdjusterName").ToString
                            End If

                            If r("CustomerNo") Is DBNull.Value Then
                                H.CustomerNo = ""
                            Else
                                H.CustomerNo = r("CustomerNo").ToString
                            End If

                            If r("ContractNo") Is DBNull.Value Then
                                H.ContractNo = ""
                            Else
                                H.ContractNo = r("ContractNo").ToString
                            End If

                            If r("AuthorizationNo") Is DBNull.Value Then
                                H.AuthNo = ""
                            Else
                                H.AuthNo = r("AuthorizationNo").ToString
                            End If

                            If r("AutoOwner") Is DBNull.Value Then
                                H.Owner = ""
                            Else
                                H.Owner = r("AutoOwner").ToString
                            End If

                            If r("VinNo") Is DBNull.Value Then
                                H.VinNo = ""
                            Else
                                H.VinNo = r("VinNo").ToString
                            End If

                            If r("CityState") Is DBNull.Value Then
                                H.Location = ""
                            Else
                                H.Location = r("CityState").ToString
                            End If

                            If r("ShipperTrack") Is DBNull.Value Then
                                H.Tracking = ""
                            Else
                                H.Tracking = r("ShipperTrack").ToString
                            End If

                            If r("Company") Is DBNull.Value Then
                                H.Shipper = ""
                            Else
                                H.Shipper = r("Company").ToString
                            End If

                            If r("ShipperStatus") Is DBNull.Value Then
                                H.Status = ""
                            Else
                                H.Status = r("ShipperStatus").ToString
                            End If

                            list.Add(H)
                        End While
                    End Using
                End Using

            Case "Arrived"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    sqlComm = New SqlCommand("SELECT Distinct top 500  dbo.tblOrder.OrderID, dbo.tblPartOrder.DateOrdered, dbo.tblPartOrder.DateEntered, dbo.tblPartOrder.ArriveDate, dbo.tblOrder.CustomerNo, dbo.tblOrder.AdjusterName, dbo.tblOrder.ContractNo, dbo.tblOrder.AuthorizationNo, dbo.tblOrder.AutoOwner, dbo.tblOrder.VinNo, dbo.tblPartOrder.City + N', ' + dbo.tblPartOrder.State AS CityState, dbo.tblPartOrder.ShipperStatus, dbo.tblPartOrder.ShipperTrack, dbo.tblCompany.Company FROM dbo.tblPartOrder INNER JOIN dbo.tblOrder ON dbo.tblPartOrder.OrderID = dbo.tblOrder.OrderID LEFT OUTER JOIN dbo.tblCompany ON dbo.tblPartOrder.Shipper = dbo.tblCompany.CompanyID WHERE (dbo.tblOrder.CustomerNo = '" & currentcustno & "') AND (dbo.tblPartOrder.ArriveDate IS NOT NULL) AND (dbo.tblPartOrder.Cancelled = 0) ORDER BY dbo.tblPartOrder.ArriveDate DESC OPTION (RECOMPILE)", conn)

                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()

                        While r.Read()
                            Dim H As New History()

                            If r("OrderID") Is DBNull.Value Then
                                H.OrderID = ""
                            Else
                                H.OrderID = r("OrderID").ToString
                            End If

                            If r("DateEntered") Is DBNull.Value Then
                                H.DateOrdered = ""
                            Else
                                H.DateOrdered = r("DateEntered").ToString
                            End If

                            'If r("PartNo") Is DBNull.Value Then
                            '    H.PartNo = ""
                            'Else
                            '    H.PartNo = r("PartNo").ToString
                            'End If

                            'If r("PartDescription") Is DBNull.Value Then
                            '    H.Description = ""
                            'Else
                            '    H.Description = r("PartDescription").ToString
                            'End If

                            'If r("Warranty") Is DBNull.Value Then
                            '    H.Warranty = ""
                            'Else
                            '    H.Warranty = r("Warranty").ToString
                            'End If

                            If r("AdjusterName") Is DBNull.Value Then
                                H.Adjuster = ""
                            Else
                                H.Adjuster = r("AdjusterName").ToString
                            End If

                            If r("CustomerNo") Is DBNull.Value Then
                                H.CustomerNo = ""
                            Else
                                H.CustomerNo = r("CustomerNo").ToString
                            End If

                            If r("ContractNo") Is DBNull.Value Then
                                H.ContractNo = ""
                            Else
                                H.ContractNo = r("ContractNo").ToString
                            End If

                            If r("AuthorizationNo") Is DBNull.Value Then
                                H.AuthNo = ""
                            Else
                                H.AuthNo = r("AuthorizationNo").ToString
                            End If

                            If r("AutoOwner") Is DBNull.Value Then
                                H.Owner = ""
                            Else
                                H.Owner = r("AutoOwner").ToString
                            End If

                            If r("VinNo") Is DBNull.Value Then
                                H.VinNo = ""
                            Else
                                H.VinNo = r("VinNo").ToString
                            End If

                            If r("CityState") Is DBNull.Value Then
                                H.Location = ""
                            Else
                                H.Location = r("CityState").ToString
                            End If

                            If r("ShipperTrack") Is DBNull.Value Then
                                H.Tracking = ""
                            Else
                                H.Tracking = r("ShipperTrack").ToString
                            End If

                            If r("Company") Is DBNull.Value Then
                                H.Shipper = ""
                            Else
                                H.Shipper = r("Company").ToString
                            End If

                            list.Add(H)
                        End While
                    End Using
                End Using

        End Select



        Return js.Serialize(list)

    End Function
    <WebMethod()> _
    Public Function GetOrderDetails(ByVal client As String, ByVal OrderID As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Order)
        Dim sqlComm As New SqlCommand
        Using conn As New SqlConnection(GetClientConnectionString(client))
            sqlComm = New SqlCommand("SELECT [OrderID], [PartNo],[PartDescription],[PartDescription2],[Quantity],[CorePrice],[SellPrice],[Warranty], ShipperTrack FROM [dbo].[tblPartOrder] Where [OrderID] = '" & OrderID & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim O As New Order()

                    If r("OrderID") Is DBNull.Value Then
                        O.OrderID = ""
                    Else
                        O.OrderID = r("OrderID").ToString
                    End If

                    If r("PartNo") Is DBNull.Value Then
                        O.PartNo = ""
                    Else
                        O.PartNo = r("PartNo").ToString
                    End If

                    If r("PartDescription") Is DBNull.Value Then
                        If r("PartDescription2") Is DBNull.Value Then
                            O.Description = ""
                        Else
                            O.Description = r("PartDescription2").ToString
                        End If
                    Else
                        If r("PartDescription2") Is DBNull.Value Then
                            O.Description = r("PartDescription").ToString
                        Else
                            O.Description = r("PartDescription").ToString & vbCrLf & r("PartDescription2").ToString
                        End If
                    End If

                    If r("Quantity") Is DBNull.Value Then
                        O.Quantity = ""
                    Else
                        O.Quantity = r("Quantity").ToString
                    End If

                    If r("SellPrice") Is DBNull.Value Then
                        O.SellPrice = ""
                    Else
                        O.SellPrice = r("SellPrice").ToString
                    End If

                    If r("CorePrice") Is DBNull.Value Then
                        O.CorePrice = ""
                    Else
                        O.CorePrice = r("CorePrice").ToString
                    End If

                    If r("Warranty") Is DBNull.Value Then
                        O.Warranty = ""
                    Else
                        O.Warranty = r("Warranty").ToString
                    End If

                    If r("ShipperTrack") Is DBNull.Value Then
                        O.TrackingNo = ""
                    Else
                        O.TrackingNo = r("ShipperTrack").ToString()
                    End If


                    list.Add(O)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function
End Class