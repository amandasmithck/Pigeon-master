Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WarrantyWebService
    Inherits System.Web.Services.WebService
    Public Class OrderDetails

        Private _OrderDate As System.String
        Private _AutoYear As System.String
        Private _AutoMake As System.String
        Private _AutoModel As System.String
        Private _Mileage As System.String
        Private _Company As System.String
        Private _Address As System.String
        Private _City As System.String
        Private _State As System.String
        Private _Zip As System.String
        Private _Phone As System.String
        Private _Username As System.String
        Private _Email As System.String
        Private _Shop As System.String
        Private _ShopAddress As System.String
        Private _ShopCity As System.String
        Private _ShopState As System.String
        Private _ShopZip As System.String
        Private _ShopPhone As System.String
        Private _Contact As System.String
        Private _PO As System.String
        Private _AutoOwner As System.String
        Private _Part As System.String
        Private _Description As System.String
        Private _Warranty As System.String
        Private _TheirPrice As System.Decimal
        Private _ListPrice As System.Decimal
        Private _CorePrice As System.Decimal
        Private _OurCost As System.Decimal
        Private _WarrantyCost As System.Decimal

        Public Property OrderDate() As System.String
            Get
                Return _OrderDate
            End Get
            Set(ByVal value As System.String)
                _OrderDate = value
            End Set
        End Property

        Public Property AutoYear() As System.String
            Get
                Return _AutoYear
            End Get
            Set(ByVal value As System.String)
                _AutoYear = value
            End Set
        End Property

        Public Property AutoMake() As System.String
            Get
                Return _AutoMake
            End Get
            Set(ByVal value As System.String)
                _AutoMake = value
            End Set
        End Property

        Public Property AutoModel() As System.String
            Get
                Return _AutoModel
            End Get
            Set(ByVal value As System.String)
                _AutoModel = value
            End Set
        End Property

        Public Property Mileage() As System.String
            Get
                Return _Mileage
            End Get
            Set(ByVal value As System.String)
                _Mileage = value
            End Set
        End Property

        Public Property Company() As System.String
            Get
                Return _Company
            End Get
            Set(ByVal value As System.String)
                _Company = value
            End Set
        End Property

        Public Property Address() As System.String
            Get
                Return _Address
            End Get
            Set(ByVal value As System.String)
                _Address = value
            End Set
        End Property

        Public Property City() As System.String
            Get
                Return _City
            End Get
            Set(ByVal value As System.String)
                _City = value
            End Set
        End Property

        Public Property State() As System.String
            Get
                Return _State
            End Get
            Set(ByVal value As System.String)
                _State = value
            End Set
        End Property

        Public Property Zip() As System.String
            Get
                Return _Zip
            End Get
            Set(ByVal value As System.String)
                _Zip = value
            End Set
        End Property

        Public Property Phone() As System.String
            Get
                Return _Phone
            End Get
            Set(ByVal value As System.String)
                _Phone = value
            End Set
        End Property

        Public Property Username() As System.String
            Get
                Return _Username
            End Get
            Set(ByVal value As System.String)
                _Username = value
            End Set
        End Property

        Public Property Email() As System.String
            Get
                Return _Email
            End Get
            Set(ByVal value As System.String)
                _Email = value
            End Set
        End Property

        Public Property Shop() As System.String
            Get
                Return _Shop
            End Get
            Set(ByVal value As System.String)
                _Shop = value
            End Set
        End Property

        Public Property ShopAddress() As System.String
            Get
                Return _ShopAddress
            End Get
            Set(ByVal value As System.String)
                _ShopAddress = value
            End Set
        End Property

        Public Property ShopCity() As System.String
            Get
                Return _ShopCity
            End Get
            Set(ByVal value As System.String)
                _ShopCity = value
            End Set
        End Property

        Public Property ShopState() As System.String
            Get
                Return _ShopState
            End Get
            Set(ByVal value As System.String)
                _ShopState = value
            End Set
        End Property

        Public Property ShopZip() As System.String
            Get
                Return _ShopZip
            End Get
            Set(ByVal value As System.String)
                _ShopZip = value
            End Set
        End Property

        Public Property ShopPhone() As System.String
            Get
                Return _ShopPhone
            End Get
            Set(ByVal value As System.String)
                _ShopPhone = value
            End Set
        End Property

        Public Property Contact() As System.String
            Get
                Return _Contact
            End Get
            Set(ByVal value As System.String)
                _Contact = value
            End Set
        End Property

        Public Property PO() As System.String
            Get
                Return _PO
            End Get
            Set(ByVal value As System.String)
                _PO = value
            End Set
        End Property

        Public Property AutoOwner() As System.String
            Get
                Return _AutoOwner
            End Get
            Set(ByVal value As System.String)
                _AutoOwner = value
            End Set
        End Property

        Public Property Part() As System.String
            Get
                Return _Part
            End Get
            Set(ByVal value As System.String)
                _Part = value
            End Set
        End Property

        Public Property Description() As System.String
            Get
                Return _Description
            End Get
            Set(ByVal value As System.String)
                _Description = value
            End Set
        End Property

        Public Property Warranty() As System.String
            Get
                Return _Warranty
            End Get
            Set(ByVal value As System.String)
                _Warranty = value
            End Set
        End Property

        Public Property TheirPrice() As System.Decimal
            Get
                Return _TheirPrice
            End Get
            Set(ByVal value As System.Decimal)
                _TheirPrice = value
            End Set
        End Property

        Public Property ListPrice() As System.Decimal
            Get
                Return _ListPrice
            End Get
            Set(ByVal value As System.Decimal)
                _ListPrice = value
            End Set
        End Property

        Public Property CorePrice() As System.Decimal
            Get
                Return _CorePrice
            End Get
            Set(ByVal value As System.Decimal)
                _CorePrice = value
            End Set
        End Property

        Public Property OurCost() As System.Decimal
            Get
                Return _OurCost
            End Get
            Set(ByVal value As System.Decimal)
                _OurCost = value
            End Set
        End Property

        Public Property WarrantyCost() As System.Decimal
            Get
                Return _WarrantyCost
            End Get
            Set(ByVal value As System.Decimal)
                _WarrantyCost = value
            End Set
        End Property

    End Class

    <WebMethod()>
    Public Function WarrantyLookup(ByVal vin As String, ByVal client As String)
        Dim list As New List(Of OrderDetails)
        Dim js As New JavaScriptSerializer


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim strsql As String = "SELECT     convert(nvarchar(50), dbo.tblOrders.OrderDate, 101) as OrderDate, dbo.tblOrders.AutoYear, dbo.tblOrders.AutoMake, dbo.tblOrders.AutoModel, dbo.tblOrders.Mileage, dbo.tblCompany.Company,  dbo.tblCompany.Address, dbo.tblCompany.City, dbo.tblCompany.State, dbo.tblCompany.Zip, dbo.tblCompany.Phone, dbo.tblOrders.Username, dbo.aspnet_Membership.Email, dbo.tblOrders.Shop, dbo.tblOrders.Address AS ShopAddress, dbo.tblOrders.City AS ShopCity, dbo.tblOrders.State AS ShopState, dbo.tblOrders.Zip AS ShopZip, dbo.tblOrders.Phone AS ShopPhone, dbo.tblOrders.Contact, dbo.tblOrders.PO, dbo.tblOrders.AutoOwner, dbo.tblOrderDetails.Part, dbo.tblOrderDetails.Description, dbo.tblOrderDetails.Warranty, dbo.tblOrderDetails.TheirPrice, dbo.tblOrderDetails.ListPrice, dbo.tblOrderDetails.CorePrice,  dbo.tblOrderDetails.WarrantyCost FROM dbo.tblOrders INNER JOIN dbo.tblOrderDetails ON dbo.tblOrders.OrderID = dbo.tblOrderDetails.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrders.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.aspnet_Users ON dbo.tblOrders.Username = dbo.aspnet_Users.UserName INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId where vinno = " & vin.fqq

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim i1 As New OrderDetails
                    Dim objType As Type = i1.GetType()
                    Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                    For Each prop As System.Reflection.PropertyInfo In properties
                        Try
                            prop.SetValue(i1, r(prop.Name), Nothing)

                        Catch ex As Exception

                        End Try
                    Next
                    list.Add(i1)
                End While
            End Using
        End Using

        If client = "AutoNation" Then 'also search GO and Autoway
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("GO"))
                Dim strsql As String = "SELECT     convert(nvarchar(50), dbo.tblOrders.OrderDate, 101) as OrderDate, dbo.tblOrders.AutoYear, dbo.tblOrders.AutoMake, dbo.tblOrders.AutoModel, dbo.tblOrders.Mileage, dbo.tblCompany.Company,  dbo.tblCompany.Address, dbo.tblCompany.City, dbo.tblCompany.State, dbo.tblCompany.Zip, dbo.tblCompany.Phone, dbo.tblOrders.Username, dbo.aspnet_Membership.Email, dbo.tblOrders.Shop, dbo.tblOrders.Address AS ShopAddress, dbo.tblOrders.City AS ShopCity, dbo.tblOrders.State AS ShopState, dbo.tblOrders.Zip AS ShopZip, dbo.tblOrders.Phone AS ShopPhone, dbo.tblOrders.Contact, dbo.tblOrders.PO, dbo.tblOrders.AutoOwner, dbo.tblOrderDetails.Part, dbo.tblOrderDetails.Description, dbo.tblOrderDetails.Warranty, dbo.tblOrderDetails.TheirPrice, dbo.tblOrderDetails.ListPrice, dbo.tblOrderDetails.CorePrice,  dbo.tblOrderDetails.WarrantyCost FROM dbo.tblOrders INNER JOIN dbo.tblOrderDetails ON dbo.tblOrders.OrderID = dbo.tblOrderDetails.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrders.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.aspnet_Users ON dbo.tblOrders.Username = dbo.aspnet_Users.UserName INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId where vinno = " & vin.fqq

                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim i1 As New OrderDetails
                        Dim objType As Type = i1.GetType()
                        Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                        For Each prop As System.Reflection.PropertyInfo In properties
                            Try
                                prop.SetValue(i1, r(prop.Name), Nothing)

                            Catch ex As Exception

                            End Try
                        Next
                        list.Add(i1)
                    End While
                End Using
            End Using

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("AutoWay"))
                Dim strsql As String = "SELECT     convert(nvarchar(50), dbo.tblOrders.OrderDate, 101) as OrderDate, dbo.tblOrders.AutoYear, dbo.tblOrders.AutoMake, dbo.tblOrders.AutoModel, dbo.tblOrders.Mileage, dbo.tblCompany.Company,  dbo.tblCompany.Address, dbo.tblCompany.City, dbo.tblCompany.State, dbo.tblCompany.Zip, dbo.tblCompany.Phone, dbo.tblOrders.Username, dbo.aspnet_Membership.Email, dbo.tblOrders.Shop, dbo.tblOrders.Address AS ShopAddress, dbo.tblOrders.City AS ShopCity, dbo.tblOrders.State AS ShopState, dbo.tblOrders.Zip AS ShopZip, dbo.tblOrders.Phone AS ShopPhone, dbo.tblOrders.Contact, dbo.tblOrders.PO, dbo.tblOrders.AutoOwner, dbo.tblOrderDetails.Part, dbo.tblOrderDetails.Description, dbo.tblOrderDetails.Warranty, dbo.tblOrderDetails.TheirPrice, dbo.tblOrderDetails.ListPrice, dbo.tblOrderDetails.CorePrice,dbo.tblOrderDetails.WarrantyCost FROM dbo.tblOrders INNER JOIN dbo.tblOrderDetails ON dbo.tblOrders.OrderID = dbo.tblOrderDetails.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrders.CustomerNo = dbo.tblCompany.CustomerNo INNER JOIN dbo.aspnet_Users ON dbo.tblOrders.Username = dbo.aspnet_Users.UserName INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId where vinno = " & vin.fqq

                Dim sqlComm As New SqlCommand(strsql, conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim i1 As New OrderDetails
                        Dim objType As Type = i1.GetType()
                        Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                        For Each prop As System.Reflection.PropertyInfo In properties
                            Try
                                prop.SetValue(i1, r(prop.Name), Nothing)

                            Catch ex As Exception

                            End Try
                        Next
                        list.Add(i1)
                    End While
                End Using
            End Using

            If list.Count = 0 Then
                Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                    Dim strsql As String = "SELECT     convert(nvarchar(50), dbo.tblOrder.DateOrdered, 101) as OrderDate, dbo.tblOrder.AutoYear, dbo.tblOrder.AutoMake, dbo.tblOrder.AutoModel, dbo.tblOrder.Mileage, dbo.tblCompany.Company,  dbo.tblCompany.Address1 as Address, dbo.tblCompany.City, dbo.tblCompany.State, dbo.tblCompany.Zip, dbo.tblCompany.Phone, tblOrder.AdjusterName as Username, tblOrder.AdjusterEmail as Email, dbo.tblPartOrder.Servicer as Shop, dbo.tblPartOrder.Address1 AS ShopAddress, dbo.tblPartOrder.City AS ShopCity, dbo.tblPartOrder.State AS ShopState, dbo.tblPartOrder.Zip AS ShopZip, dbo.tblPartOrder.Phone AS ShopPhone, dbo.tblPartOrder.Contact, dbo.tblOrder.AuthorizationNo as PO, dbo.tblOrder.AutoOwner, dbo.tblPartOrder.PartNo as Part, dbo.tblPartOrder.PartDescription as Description, dbo.tblPartOrder.Warranty, dbo.tblPartOrder.SellPrice as TheirPrice, dbo.tblPartOrder.ListPrice, dbo.tblPartOrder.CorePrice, dbo.tblPartOrder.WarrantyCost FROM dbo.tblOrder INNER JOIN dbo.tblPartOrder ON dbo.tblOrder.OrderID = dbo.tblPartOrder.OrderID INNER JOIN dbo.tblCompany ON dbo.tblOrder.CustomerNo = dbo.tblCompany.CustomerNo where vinno = " & vin.fqq & " and (tblOrder.CustomerNo = '33764' or tblOrder.CustomerNo='Go PC80122')"

                    Dim sqlComm As New SqlCommand(strsql, conn)
                    conn.Open()
                    Using r As SqlDataReader = sqlComm.ExecuteReader()
                        While r.Read()
                            Dim i1 As New OrderDetails
                            Dim objType As Type = i1.GetType()
                            Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                            For Each prop As System.Reflection.PropertyInfo In properties
                                Try
                                    prop.SetValue(i1, r(prop.Name), Nothing)

                                Catch ex As Exception

                                End Try
                            Next
                            list.Add(i1)
                        End While
                    End Using
                End Using
            End If
        End If

        Return js.Serialize(list)
    End Function
    <WebMethod()>
    Public Function SendWarranty(ByVal vin As String, ByVal name As String, ByVal email As String, ByVal phone As String, ByVal issue As String, ByVal client As String)

        Dim js As New JavaScriptSerializer
        Try
            Dim mm As New System.Net.Mail.MailMessage(email, "sales@ckautoparts.com")
            mm.Subject = client & " Online Warranty Claim"

            Dim strbody As String = String.Empty
            strbody &= "Name: " & name & "<br/>"
            strbody &= "Phone: " & phone & "<br/><br/>"
            strbody &= "VIN: " & vin & "<br/>"

            strbody &= "Issue: <br/>"
            strbody &= issue & "<br/><br/>"
            mm.Body = strbody
            mm.IsBodyHtml = True
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
            Return js.Serialize(True)
        Catch Ex As Exception
            Return js.Serialize(False)
        End Try
    End Function
    <WebMethod()>
    Public Function GetComparisonInfo() As List(Of vendorInfo)
        Try
            Dim listOfVendorInfo As New List(Of vendorInfo)
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString("CK"))
                conn.Open()
                Dim vendorInfoQuery As String = "select id,name,imageLocation from tblWarrantyComparisonVendorInfo"
                Dim warrantyTypesQuery As String = "select id,type from tblWarrantyComparisonTypes"
                Using vendorInfoComm As New SqlCommand(vendorInfoQuery, conn)
                    Using vendorInfoReader As SqlDataReader = vendorInfoComm.ExecuteReader()
                        While vendorInfoReader.Read()
                            Using warrantyTypeComm As New SqlCommand(warrantyTypesQuery, conn)
                                Using warrantyTypeInfoReader As SqlDataReader = warrantyTypeComm.ExecuteReader()
                                    While warrantyTypeInfoReader.Read()
                                        Dim warrantyPlanInfo As String = "select WT.type,WI.itemName,WC.warrantyValue from tblWarrantyComparisontypes as WT inner join tblwarrantyComparisonItems as WI on WT.ID=WI.warrantyTypeID inner join tblWarrantyComparisonValues as WC on WI.ID=WC.warrantyItemID where WC.vendorID=" & vendorInfoReader("id") & " and WT.ID=" & warrantyTypeInfoReader("id") & " order by  WI.ID,type"
                                        Using warrantyPlanComm As New SqlCommand(warrantyPlanInfo, conn)
                                            Using warrantyPlanReader As SqlDataReader = warrantyPlanComm.ExecuteReader()
                                                Dim vendorInfo As New vendorInfo
                                                vendorInfo.itemInfo = New List(Of warrantyItemInfo)
                                                vendorInfo.vendorID = vendorInfoReader("id")
                                                vendorInfo.vendorName = vendorInfoReader("name").ToString() & "-" & warrantyTypeInfoReader("type").ToString()
                                                vendorInfo.imageLocation = vendorInfoReader("imageLocation").ToString()
                                                vendorInfo.warrantyTypeID = warrantyTypeInfoReader("id")
                                                While warrantyPlanReader.Read()
                                                    Dim warrantyItemInfo As New warrantyItemInfo
                                                    warrantyItemInfo.warrantyTypeID = warrantyTypeInfoReader("id") 'May not need this or the one below..
                                                    warrantyItemInfo.warrantyType = warrantyTypeInfoReader("type").ToString()
                                                    warrantyItemInfo.itemName = warrantyPlanReader("itemName").ToString()
                                                    warrantyItemInfo.itemValue = warrantyPlanReader("warrantyValue").ToString()
                                                    vendorInfo.itemInfo.Add(warrantyItemInfo)
                                                End While
                                                listOfVendorInfo.Add(vendorInfo)
                                            End Using
                                        End Using
                                    End While
                                End Using
                            End Using


                        End While
                    End Using
                End Using

            End Using
            Return listOfVendorInfo.OrderBy(Function(s) s.warrantyTypeID).ToList()
        Catch ex As Exception
            'Wrangle.
            Return New List(Of vendorInfo)
        End Try
    End Function

End Class

Public Class vendorInfo
    Public Property vendorID As Integer
    Public Property vendorName As String
    Public Property imageLocation As String
    Public Property itemInfo As List(Of warrantyItemInfo)
    Public Property warrantyTypeID As Integer
    Public Function vendorInfo()
        itemInfo = New List(Of warrantyItemInfo)
    End Function

End Class
Public Class warrantyItemInfo
    Public Property warrantyTypeID As Integer 'May Not need.
    Public Property warrantyType As String
    Public Property itemName As String
    Public Property itemValue As String
End Class