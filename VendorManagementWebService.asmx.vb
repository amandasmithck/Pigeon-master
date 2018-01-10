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
Public Class VendorManagementWebService
    Inherits System.Web.Services.WebService

#Region "Properties"
    Public Shared ckConnection = ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString

#End Region

#Region "WebMethods"

    <WebMethod()>
    Public Function GetEmptydtoVendor() As dtoVendorInfo

        Return New dtoVendorInfo()
    End Function

    <WebMethod()>
    Public Function DeleteExsitingShippingAddress(ByVal shippingID As Integer) As Boolean
        Try

            Dim deleteQuery As String = "delete [tblVendorShippingAddress]
            where [VendorShippingID]=" & shippingID
            Using conn As New SqlConnection(ckConnection)
                conn.Open()
                Using shippingCommand As New SqlCommand(deleteQuery, conn)
                    shippingCommand.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    <WebMethod()>
    Public Function InsertShippingAddress(ByVal shippingInfo As dtoShippingInfo) As List(Of dtoShippingInfo)

        'Returning a List because the Front in needs a clean one..and there are set varianles on this side.
        Dim returnedShippingAddress As New List(Of dtoShippingInfo)
        returnedShippingAddress.Add(New dtoShippingInfo)
        Try
            'could use same function for updating IF shippingInfo.vendorShippingID<>0
            'Closing Time Needs to Be DateTime fot the time..
            Dim insertQuery = "INSERT INTO [tblVendorShippingAddress]
           ([CompanyID]
           ,[ShippingDirectionTypeID]
           ,[Address1]
           ,[Address2]
           ,[City]
           ,[State]
           ,[Zip]
           ,[ContactPerson]
           ,[BuildingLocation]
           ,[BuildingPartCode]
           ,[LocationDescription]
           ,[OfficeClosingTime])
     VALUES (" & shippingInfo.companyID & "," & shippingInfo.shippingDirectionTypeID & ",'" & shippingInfo.address1 & "','" & shippingInfo.address2 & "','" & shippingInfo.city & "','" & shippingInfo.state & "','" & shippingInfo.zip & "','" & shippingInfo.contact & "'," & shippingInfo.buildingLocation & "," & shippingInfo.buildingPartCode & ",'" & IIf(shippingInfo.locationDescription = Nothing, "", shippingInfo.locationDescription) & "','" & shippingInfo.closingTime & "'); Select SCOPE_IDENTITY();"
            Using conn As New SqlConnection(ckConnection)
                conn.Open()
                Using shippingCommand As New SqlCommand(insertQuery.ToString, conn)
                    shippingInfo.vendorShippingID = Convert.ToInt32(shippingCommand.ExecuteScalar())
                    'Return Convert.ToInt32(shippingCommand.ExecuteScalar())
                End Using
                Using vanityCommand As New SqlCommand("Insert Into tblVanityShippingAddress values(" & shippingInfo.vendorShippingID & ",'" & shippingInfo.vanityName & "'); Select SCOPE_IDENTITY();", conn)
                    shippingInfo.vanityNameID = Convert.ToInt32(vanityCommand.ExecuteScalar())
                End Using
            End Using
            returnedShippingAddress.Add(shippingInfo)
            Return returnedShippingAddress
        Catch ex As Exception

        End Try
    End Function

    <WebMethod()>
    Public Function editExsitingShippingAddress(ByVal shippingInfo As dtoShippingInfo) As Integer
        Dim returnedShippingAddress As New List(Of dtoShippingInfo)
        returnedShippingAddress.Add(New dtoShippingInfo)
        Try
            'Took [CompanyID]=" & shippingInfo.companyID & " out of the update since company will stay the same

            Dim updateQuery = "Update [tblVendorShippingAddress]
           set   
            [CompanyID]=" & shippingInfo.companyID & ",
            [ShippingDirectionTypeID] = " & shippingInfo.shippingDirectionTypeID & ",
            [Address1] ='" & shippingInfo.address1 & "',
            [Address2] ='" & shippingInfo.address2 & "',
            [City] ='" & shippingInfo.city & "',
            [State] ='" & shippingInfo.state & "',
            [Zip] ='" & shippingInfo.zip & "',
            [ContactPerson] ='" & shippingInfo.contact & "',
            [BuildingLocation] ='" & shippingInfo.buildingLocation & "',
            [BuildingPartCode] ='" & shippingInfo.buildingPartCode & "',
            [LocationDescription] ='" & IIf(shippingInfo.locationDescription = Nothing, "", shippingInfo.locationDescription) & "',
            [OfficeClosingTime] ='" & shippingInfo.closingTime & "'
            where [VendorShippingID]=" & shippingInfo.vendorShippingID
            Using conn As New SqlConnection(ckConnection)
                conn.Open()
                Using shippingCommand As New SqlCommand(updateQuery.ToString, conn)
                    shippingCommand.ExecuteNonQuery()
                End Using
                Using vanityCommand As New SqlCommand("update tblVanityShippingAddress Set vanity='" & shippingInfo.vanityName & "' where autoID=" & shippingInfo.vanityNameID, conn)
                    vanityCommand.ExecuteNonQuery()
                End Using
            End Using
            Return shippingInfo.companyID
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function GetVendorInfoByID(ByVal ID As Integer) As dtoVendorInfo
        Dim AllVendorsInfo As New dtoVendorInfo
        Try
            Dim companyInfo As New dtoCompanyInfo
            Dim allExsitingShippingInfo As New List(Of dtoShippingInfo)
            companyInfo.companyID = ID

            Dim getCompanyInfoQuery = "Select company, address1, address2, contact, phone, fax, email, warrantyemail, city, state, zip, vendorOrderMethod, notes, type, netShipping, tax, active from tblCompany where companyID=" & ID
            Dim getExsitingShippingQuery = "Select address1,address2,contactPerson,city,state,zip,officeClosingTime,shippingDirectionTypeID,buildingLocation,buildingPartCode,locationDescription,vendorShippingID,VA.Vanity,VA.AutoID as vanityID from tblVendorShippingAddress as SA inner join tblVanityShippingAddress as VA on SA.VendorShippingID=VA.VendorShippingAddressID where companyID=" & ID
            'Dim getInvoiceGuideOldQuery = ""
            Using conn As New SqlConnection(ckConnection)
                conn.Open()
                Using comm As New SqlCommand(getCompanyInfoQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            companyInfo.active = CType(reader("active"), Boolean)
                            companyInfo.address1 = IIf(reader.IsDBNull(1), "", reader("address1"))
                            companyInfo.address2 = IIf(reader.IsDBNull(2), "", reader("address2"))
                            companyInfo.city = IIf(reader.IsDBNull(8), "", reader("city"))
                            companyInfo.state = IIf(reader.IsDBNull(9), "", reader("state"))
                            companyInfo.zip = IIf(reader.IsDBNull(10), "", reader("zip"))
                            companyInfo.company = IIf(reader.IsDBNull(0), "", reader("company"))
                            companyInfo.phone = IIf(reader.IsDBNull(4), "", reader("phone"))
                            companyInfo.fax = IIf(reader.IsDBNull(5), "", reader("fax"))
                            companyInfo.email = IIf(reader.IsDBNull(6), "", reader("email"))
                            companyInfo.warrantyEmail = IIf(reader.IsDBNull(7), "", reader("warrantyEmail"))
                            'companyInfo.vanityName = IIf(reader.IsDBNull(11), "", reader("VanityShippingName"))
                            companyInfo.vendorOrderMethodID = IIf(reader.IsDBNull(11), "", IIf(reader("vendorOrderMethod") = "Email", Convert.ToInt32(VendorOrderMethods.Email), IIf(reader("vendorOrderMethod") = "FTP", Convert.ToInt32(VendorOrderMethods.FTP), Convert.ToInt32(VendorOrderMethods.Website))))
                            companyInfo.contact = IIf(reader.IsDBNull(3), "", reader("contact"))
                            companyInfo.notes = IIf(reader.IsDBNull(12), "", reader("notes"))
                            companyInfo.type = IIf(reader.IsDBNull(13), "", reader("type"))
                            companyInfo.netShipping = IIf(reader.IsDBNull(14), "", reader("netShipping"))
                            companyInfo.tax = IIf(reader.IsDBNull(15), "", reader("tax"))
                        End While
                    End Using
                End Using
                Using comm As New SqlCommand(getExsitingShippingQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim exsitingShippingInfo As New dtoShippingInfo
                            exsitingShippingInfo.address1 = IIf(reader.IsDBNull(0), "", reader("address1"))
                            exsitingShippingInfo.address2 = IIf(reader.IsDBNull(1), "", reader("address2"))
                            exsitingShippingInfo.city = IIf(reader.IsDBNull(3), "", reader("city"))
                            exsitingShippingInfo.state = IIf(reader.IsDBNull(4), "", reader("state"))
                            exsitingShippingInfo.zip = IIf(reader.IsDBNull(5), "", reader("zip"))
                            exsitingShippingInfo.contact = IIf(reader.IsDBNull(2), "", reader("contactPerson"))
                            exsitingShippingInfo.closingTime = IIf(reader.IsDBNull(6), "", reader("officeClosingTime"))
                            exsitingShippingInfo.shippingDirectionTypeID = IIf(reader.IsDBNull(7), "", reader("shippingDirectionTypeID"))
                            exsitingShippingInfo.buildingLocation = IIf(reader.IsDBNull(8), "", reader("buildingLocation"))
                            exsitingShippingInfo.buildingPartCode = IIf(reader.IsDBNull(9), "", reader("buildingPartCode"))
                            exsitingShippingInfo.locationDescription = IIf(reader.IsDBNull(10), "", reader("locationDescription"))
                            exsitingShippingInfo.vendorShippingID = IIf(reader.IsDBNull(11), "", reader("vendorShippingID"))
                            exsitingShippingInfo.vanityNameID = IIf(reader.IsDBNull(13), "", reader("vanityID"))
                            exsitingShippingInfo.vanityName = IIf(reader.IsDBNull(12), "", reader("Vanity"))

                            exsitingShippingInfo.closingTime.Remove(0, 10)
                            exsitingShippingInfo.shippingDirections = New List(Of dtoShippingDirection)
                            For Each shippingDirectionID As Integer In CType([Enum].GetValues(GetType(ShippingDirectionTypes)), Integer())
                                Dim direction As New dtoShippingDirection
                                direction.ID = shippingDirectionID
                                direction.direction = [Enum].GetName(GetType(ShippingDirectionTypes), shippingDirectionID)
                                exsitingShippingInfo.shippingDirections.Add(direction)
                            Next
                            allExsitingShippingInfo.Add(exsitingShippingInfo)
                            'Finish rest of them!
                        End While
                    End Using
                End Using
            End Using

            AllVendorsInfo.oemDealerInfo = New dtoOEMDealer(ID)
            AllVendorsInfo.companyInfo = companyInfo
            AllVendorsInfo.exsitingShippingInfo = allExsitingShippingInfo
            AllVendorsInfo.invoiceGuideInfo = New dtoInvoiceGuide(ID)
            AllVendorsInfo.exsitingCompanies = PrivateGetAllVendor()
            Return AllVendorsInfo
        Catch ex As Exception

        End Try
        Return AllVendorsInfo
    End Function

    <WebMethod()>
    Public Function InsertUpdateVendor(ByVal vendorInfo As dtoVendorInfo, ByVal isNew As Boolean, ByVal isOEMDealer As Boolean) As Integer
        Try

            vendorInfo.companyInfo.company = Regex.Replace(vendorInfo.companyInfo.company, "'", "''")
            If (Not IsNullOrEmpty(vendorInfo.companyInfo.notes)) Then
                vendorInfo.companyInfo.notes = Regex.Replace(vendorInfo.companyInfo.notes, "'", "''")
            End If

            'If Insertng make sure i get the info that is needed once this goes in to put in other tables for FoeignKeys set it as vendorInfo.companyInfo.companyID
            Dim companyQuery As String = String.Empty
            If isNew Then
                companyQuery = "INSERT INTO [tblCompany]
               ([Company]
               ,[Type]
               ,[Address1]
               ,[Address2]
               ,[City]
               ,[State]
               ,[Zip]
               ,[Phone]
               ,[Fax]
               ,[Contact]
               ,[Email]
               ,[WarrantyEmail]
               ,[Notes]
               ,[Active]
               ,[Tax]
               ,[NetShipping]
               ,[VendorOrderMethod]) VALUES ('" & vendorInfo.companyInfo.company & "','" & vendorInfo.companyInfo.type & "','" & vendorInfo.companyInfo.address1 & "','" & vendorInfo.companyInfo.address2 & "','" & vendorInfo.companyInfo.city & "','" & vendorInfo.companyInfo.state & "','" & vendorInfo.companyInfo.zip & "','" & vendorInfo.companyInfo.phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") & "','" & vendorInfo.companyInfo.fax.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") & "','" & vendorInfo.companyInfo.contact & "','" & vendorInfo.companyInfo.email & "','" & vendorInfo.companyInfo.warrantyEmail & "','" & vendorInfo.companyInfo.notes & "','" & vendorInfo.companyInfo.active & "','" & vendorInfo.companyInfo.tax & "','" & vendorInfo.companyInfo.netShipping & "','" & [Enum].GetName(GetType(VendorOrderMethods), Convert.ToInt32(vendorInfo.companyInfo.vendorOrderMethodID)) & "'); Select SCOPE_IDENTITY(); "

            Else
                companyQuery = "UPDATE [tblCompany]
                       SET 
                         [Company] = '" & vendorInfo.companyInfo.company & "'
                          , [Address1] = '" & vendorInfo.companyInfo.address1 & "'
                          , [Address2] = '" & vendorInfo.companyInfo.address2 & "'
                          , [City] = '" & vendorInfo.companyInfo.city & "'
                          , [State] = '" & vendorInfo.companyInfo.state & "'
                          , [Zip] = '" & vendorInfo.companyInfo.zip & "'
                          , [Phone] = '" & vendorInfo.companyInfo.phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") & "'
                          , [Fax] = '" & vendorInfo.companyInfo.fax.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") & "'
                          , [Contact] = '" & vendorInfo.companyInfo.contact & "'
                          , [Email] = '" & vendorInfo.companyInfo.email & "'
                          , [WarrantyEmail] = '" & vendorInfo.companyInfo.warrantyEmail & "'
                          , [Notes] = '" & vendorInfo.companyInfo.notes & "'
                          , [Active] = '" & vendorInfo.companyInfo.active & "'
                          , [Tax] = '" & vendorInfo.companyInfo.tax & "'
                          , [NetShipping] = '" & vendorInfo.companyInfo.netShipping & "'
                          , [VendorOrderMethod] = '" & [Enum].GetName(GetType(VendorOrderMethods), Convert.ToInt32(vendorInfo.companyInfo.vendorOrderMethodID)) & "'
                     WHERE companyID= " & vendorInfo.companyInfo.companyID

            End If

            Using conn As New SqlConnection(ckConnection)
                conn.Open()
                Using companyCommand As New SqlCommand(companyQuery, conn)
                    If isNew Then
                        vendorInfo.companyInfo.companyID = Convert.ToInt32(companyCommand.ExecuteScalar())
                    Else
                        companyCommand.ExecuteNonQuery()
                    End If
                End Using
                If isNew Then
                    vendorInfo.emptyShippingInfo.companyID = vendorInfo.companyInfo.companyID
                    InsertShippingAddress(vendorInfo.emptyShippingInfo)
                End If

                If Not isNew Then
                    If Not isOEMDealer Then
                        Dim deleteQuery As String = "delete [tblOemDealer]
                        where [CompanyID]=" & vendorInfo.companyInfo.companyID
                        Using oemDealerCommand As New SqlCommand(deleteQuery, conn)
                            oemDealerCommand.ExecuteNonQuery()
                        End Using
                    End If
                End If
                'Remove when Checked
                If isOEMDealer Then
                    Dim deleteQuery As String = "delete [tblOemDealer]
                    where [CompanyID]=" & vendorInfo.companyInfo.companyID
                    Using oemDealerCommand As New SqlCommand(deleteQuery, conn)
                        oemDealerCommand.ExecuteNonQuery()
                    End Using

                    For Each markupInfo As dtoMarkUpInfo In vendorInfo.oemDealerInfo.markUpInfo
                        Dim oemBuilder As New StringBuilder
                        oemBuilder.Append("declare @rowCount int set @rowCount = (select count(distinct tblOEMDealer.OEMID & tblMake.MakeID) from tblOemDealer inner join tblMake on tblOEMDealer.OEMID=tblMake.OEMID where tblOEMDealer.OEMID = ")
                        oemBuilder.Append(markupInfo.oemID)
                        oemBuilder.Append(") declare @recordCount int set @recordCount = (select count(*) from tblOemDealer where tblOEMDealer.OEMID = " & markupInfo.oemID & " And tblOEMDealer.CompanyID = " & vendorInfo.companyInfo.companyID)
                        oemBuilder.Append(" ) If @rowCount = 1")
                        oemBuilder.Append("Insert Into tblOemDealer values (")
                        oemBuilder.Append(markupInfo.oemID & "," & (markupInfo.markUp / 100) & "," & (markupInfo.markUpCustomer / 100) & "," & vendorInfo.companyInfo.companyID & ");")
                        oemBuilder.Append("Else If @recordCount = 0")
                        oemBuilder.Append("Insert Into tblOemDealer values (")
                        oemBuilder.Append(markupInfo.oemID & "," & (markupInfo.markUp / 100) & "," & (markupInfo.markUpCustomer / 100) & "," & vendorInfo.companyInfo.companyID & ");")
                        Using oemDealerCommand As New SqlCommand(oemBuilder.ToString, conn)
                            oemDealerCommand.ExecuteNonQuery()
                        End Using
                    Next
                End If

                Dim invoiceGuideCountQuery = "select count(CompanyID) from tblInvoiceGuideValue where CompanyID = " & vendorInfo.companyInfo.companyID
                Dim invoiceGuideElementQuery = "select count(invoiceGuideElementId) from tblInvoiceGuideElement where ManagementPortalActive = 1"
                Dim invoiceGuideElementCount As Int16
                Dim invoiceGuideCount As Int16
                Dim invoiceGuideDelete = "delete tblInvoiceGuideValue where CompanyID = " & vendorInfo.companyInfo.companyID

                Using invoiceGuideCommand As New SqlCommand(invoiceGuideCountQuery, conn)
                    invoiceGuideCount = Convert.ToInt16(invoiceGuideCommand.ExecuteScalar())
                End Using

                Using invoiceGuideCommand As New SqlCommand(invoiceGuideElementQuery, conn)
                    invoiceGuideElementCount = Convert.ToInt16(invoiceGuideCommand.ExecuteScalar())
                End Using

                If (invoiceGuideCount <= invoiceGuideElementCount) Then
                    isNew = True
                    Using invoiceGuideCommand As New SqlCommand(invoiceGuideDelete, conn)
                        invoiceGuideCommand.ExecuteNonQuery()
                    End Using
                End If

                'Remove when Checked
                For Each invoiceInfo As dtoInvoiceGuideElements In vendorInfo.invoiceGuideInfo.allElements
                    Dim invoiceGuideBuilder As New StringBuilder
                    invoiceGuideBuilder.Append(IIf(isNew, "Insert Into", "Update"))
                    invoiceGuideBuilder.Append(" tblInvoiceGuideValue ")
                    invoiceGuideBuilder.Append(IIf(isNew, "Values(", "Set "))
                    invoiceGuideBuilder.Append(IIf((isNew), invoiceInfo.elementID & "," & vendorInfo.companyInfo.companyID & ",'" _
                        & IIf(invoiceInfo.elementID = 26 Or invoiceInfo.elementID = 29, IIf(invoiceInfo.elementID = 26, [Enum].GetName(GetType(PaymentTypes),
                        vendorInfo.invoiceGuideInfo.chosenPaymentTypeID), [Enum].GetName(GetType(VendorTypes), vendorInfo.invoiceGuideInfo.chosenVendorTypeID)) & "')",
                        IIf(CType(invoiceInfo.elementDataTypeID, DataTypes) = DataTypes.Boolean, CType(invoiceInfo.checkBoxValue, Boolean), invoiceInfo.textValue) & "')"),
                        "Value='" & IIf(invoiceInfo.elementID = 26 Or invoiceInfo.elementID = 29, IIf(invoiceInfo.elementID = 26, [Enum].GetName(GetType(PaymentTypes),
                        vendorInfo.invoiceGuideInfo.chosenPaymentTypeID), [Enum].GetName(GetType(VendorTypes), vendorInfo.invoiceGuideInfo.chosenVendorTypeID)) _
                        & "' where InvoiceGuideElementID=" & invoiceInfo.elementID & " and CompanyID=" & vendorInfo.companyInfo.companyID,
                        IIf(CType(invoiceInfo.elementDataTypeID, DataTypes) = DataTypes.Boolean, CType(invoiceInfo.checkBoxValue, Boolean), invoiceInfo.textValue) _
                        & "' where InvoiceGuideElementID=" & invoiceInfo.elementID & " and CompanyID=" & vendorInfo.companyInfo.companyID)))


                    Using invoiceCommand As New SqlCommand(invoiceGuideBuilder.ToString, conn)
                        invoiceCommand.ExecuteNonQuery()
                    End Using
                Next

                conn.Close()
            End Using

            Return vendorInfo.companyInfo.companyID
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function MirrorCompanyInvoiceGuide(ByVal companyID) As dtoInvoiceGuide
        Return New dtoInvoiceGuide(companyID)
    End Function

    <WebMethod()>
    Public Function MirrorCompanyOEMDealer(ByVal companyID) As dtoOEMDealer
        Return New dtoOEMDealer(companyID)
    End Function

    <WebMethod()>
    Public Function GetAllVendors() As List(Of dtoBasicCompanyInfo)
        Dim list As New List(Of dtoBasicCompanyInfo)
        Try
            Return PrivateGetAllVendor()
        Catch ex As Exception
            Return list
        End Try
    End Function

#End Region

#Region "Private Functions"
    Private Function PrivateGetAllVendor() As List(Of dtoBasicCompanyInfo)
        Dim allVendors As New List(Of dtoBasicCompanyInfo)
        Try
            Dim companiesQuery = "Select companyID, company from [tblCompany] where type='Vendor' order By company asc"
            'Change makesQuery to get for a specific company if there is already a company exsiting and are editing or Possible Mirror..

            Using conn As New SqlConnection(ckConnection)
                conn.Open()
                Using comm As New SqlCommand(companiesQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim companyInfo As New dtoBasicCompanyInfo
                            companyInfo.companyID = reader("companyID")
                            companyInfo.companyName = reader("company")
                            allVendors.Add(companyInfo)
                        End While
                    End Using
                End Using
            End Using
            Return allVendors
        Catch ex As Exception

        End Try

        Return allVendors

    End Function

#End Region

End Class

#Region "Enums"
Public Enum VendorTypes
    OEM = 1
    SmallParts = 2
    AfterMarket = 3
End Enum

#End Region

#Region "Other Classes"
Public Class dtoCompanyInfo
    Public companyID As Integer
    Public company As String
    Public address1 As String
    Public address2 As String
    Public contact As String
    Public phone As String
    Public fax As String
    Public email As String
    Public warrantyEmail As String
    Public city As String
    Public state As String
    Public zip As String
    Public vendorOrderMethodID As Integer
    Public notes As String
    Public type As String
    Public netShipping As String
    Public tax As String
    Public active As String
    Public vendorOrderOptions As List(Of dtoVendorMethod)

    Sub New()
        type = "vendor"
        tax = 0
        netShipping = 25
        vendorOrderOptions = New List(Of dtoVendorMethod)
        active = False
        For Each vendorOrderOption As Integer In CType([Enum].GetValues(GetType(VendorOrderMethods)), Integer())
            If vendorOrderOption <> 0 Then
                Dim vendorMethod As New dtoVendorMethod
                vendorMethod.ID = vendorOrderOption
                vendorMethod.type = [Enum].GetName(GetType(VendorOrderMethods), vendorOrderOption)
                vendorOrderOptions.Add(vendorMethod)
            End If

        Next
    End Sub


End Class

Public Class dtoVendorMethod
    Public ID As Integer
    Public type As String

End Class

Public Class dtoInvoiceGuide
    Public allElements As List(Of dtoInvoiceGuideElements)
    Public allVendorTypes As List(Of dtoVendorTypes)
    Public allPaymentTypes As List(Of dtoPaymentTypes)
    Public companyToMirror As Integer
    Public companiesToMirror As List(Of dtoBasicCompanyInfo)
    Public chosenVendorTypeID As Integer
    Public chosenPaymentTypeID As Integer

    Sub New()

    End Sub

    Sub New(ByVal companyID As Integer)
        'Remove All Alls'
        allElements = New List(Of dtoInvoiceGuideElements)
        allVendorTypes = New List(Of dtoVendorTypes)
        allPaymentTypes = New List(Of dtoPaymentTypes)
        companiesToMirror = New List(Of dtoBasicCompanyInfo)
        companyToMirror = companyID
        chosenPaymentTypeID = -1

        Try
            For Each vendorTypeID As Integer In CType([Enum].GetValues(GetType(VendorTypes)), Integer())
                Dim vendorTypeInfo As New dtoVendorTypes
                vendorTypeInfo.ID = vendorTypeID
                vendorTypeInfo.direction = [Enum].GetName(GetType(VendorTypes), vendorTypeID)
                allVendorTypes.Add(vendorTypeInfo)
            Next
            For Each paymentTypeID As Integer In CType([Enum].GetValues(GetType(PaymentTypes)), Integer())
                Dim paymentTypeInfo As New dtoPaymentTypes
                paymentTypeInfo.ID = paymentTypeID
                paymentTypeInfo.direction = [Enum].GetName(GetType(PaymentTypes), paymentTypeID)
                allPaymentTypes.Add(paymentTypeInfo)
            Next
            Dim companiesQuery = "select distinct company.CompanyID,company.Company from tblInvoiceGuideValue inner join tblCompany as company on tblInvoiceGuideValue.CompanyID=company.CompanyID Order By company.Company"
            Dim checkTblInvGuide = "select count(*) as count from tblInvGuide where companyID=" & companyID
            Dim elementsQuery = IIf(companyID <> -1, "select distinct element.InvoiceGuideElementID, value.Value,element.elementName,element.DataTypeID,element.StatusTypeID,element.ManagementPortalActive,element.DisplayName,element.Description,element.DefaultValue from tblInvoiceGuideValue as value inner join tblInvoiceGuideElement as element on value.invoiceGuideElementID=element.invoiceGuideElementID where companyID=" & companyID, "select distinct element.InvoiceGuideElementID, element.elementName,element.DataTypeID,element.StatusTypeID,element.ManagementPortalActive,element.DisplayName,element.Description,element.DefaultValue from tblInvoiceGuideValue as value inner join tblInvoiceGuideElement as element on value.invoiceGuideElementID=element.invoiceGuideElementID order By element.InvoiceGuideElementID")

            Using conn As New SqlConnection(VendorManagementWebService.ckConnection)
                conn.Open()
                Using comm As New SqlCommand(companiesQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim companyInfo As New dtoBasicCompanyInfo
                            companyInfo.companyID = reader("CompanyID")
                            companyInfo.companyName = reader("Company")
                            companiesToMirror.Add(companyInfo)
                        End While
                    End Using
                End Using
                Using comm As New SqlCommand(elementsQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim elementInfo As New dtoInvoiceGuideElements
                            elementInfo.elementID = reader("InvoiceGuideElementID")
                            elementInfo.elementName = reader("DisplayName").ToString
                            elementInfo.elementDataTypeID = reader("DataTypeID")
                            elementInfo.description = reader("Description").ToString
                            If (CType(reader("ManagementPortalActive").ToString, Boolean)) = True Then
                                If companyID <> -1 Then
                                    If IsDBNull(reader("Value")) = True Or reader("Value").ToString = String.Empty Or reader("Value") = "" Then
                                        If elementInfo.elementID = 29 Then
                                            chosenVendorTypeID = -1
                                        ElseIf elementInfo.elementID = 26 Then
                                            chosenPaymentTypeID = -1
                                        Else
                                            elementInfo.textValue = String.Empty
                                            elementInfo.checkBoxValue = False
                                        End If

                                    Else
                                        If elementInfo.elementDataTypeID = 1 Then

                                            elementInfo.checkBoxValue = CType(reader("Value"), Boolean)

                                        ElseIf elementInfo.elementDataTypeID = 2 Then
                                            If elementInfo.elementID = 29 Then
                                                If reader("Value").ToString = String.Empty Or IsDBNull(reader("Value")) Then
                                                    chosenVendorTypeID = -1
                                                Else
                                                    chosenVendorTypeID = CType([Enum].Parse(GetType(VendorTypes), reader("Value").ToString), Integer)
                                                End If

                                            Else
                                                If reader("Value").ToString = String.Empty Or IsDBNull(reader("Value")) Then
                                                    chosenVendorTypeID = -1
                                                Else
                                                    chosenPaymentTypeID = CType([Enum].Parse(GetType(PaymentTypes), reader("Value").ToString), Integer)
                                                End If
                                            End If
                                        Else
                                            elementInfo.textValue = reader("Value").ToString
                                        End If
                                    End If
                                Else
                                    elementInfo.textValue = reader("DefaultValue").ToString
                                End If
                                allElements.Add(elementInfo)
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class dtoOEMDealer
    'Mirroring Option send back a List of Current CompanyIDs..
    Public companyID As Integer
    Public companyToMirror As Integer
    Public markUpInfo As List(Of dtoMarkUpInfo)
    Public companiesToMirror As List(Of dtoBasicCompanyInfo)

    Sub New()

    End Sub

    Sub New(ByVal companyID As Integer)
        Try

            'Possibly Pass Company ID and if -1 then its New if not its a mirror or an exsiting company
            companiesToMirror = New List(Of dtoBasicCompanyInfo)
            markUpInfo = New List(Of dtoMarkUpInfo)
            Dim companiesQuery = "select distinct company.CompanyID,company.ParentCompanyID,company.Company from tblOEMDealer inner join tblCompany as company on tblOEMDealer.CompanyID=company.CompanyID Order By company.Company"
            'Change makesQuery to get for a specific company if there is already a company exsiting and are editing or Possible Mirror..
            Dim makesQuery = IIf(companyID <> -1, "select tblOEMDealer.OEMID,tblMake.Make, tblOEMDealer.markUp,tblOEMDealer.markupCustomer from tblOEMDealer inner join tblMake on tblOEMDealer.OEMID=tblMake.OEMID where tblOEMDealer.CompanyID=" & companyID, "select distinct tblOEMDealer.OEMID,tblMake.Make from tblOEMDealer inner join tblMake on tblOEMDealer.OEMID=tblMake.OEMID")
            Using conn As New SqlConnection(VendorManagementWebService.ckConnection)
                conn.Open()
                Using comm As New SqlCommand(companiesQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim companyInfo As New dtoBasicCompanyInfo
                            companyInfo.companyID = reader("CompanyID")
                            companyInfo.companyName = reader("Company")
                            companiesToMirror.Add(companyInfo)
                        End While
                    End Using
                End Using
                Using comm As New SqlCommand(makesQuery, conn)
                    Using reader As SqlDataReader = comm.ExecuteReader
                        While reader.Read
                            Dim markUp As New dtoMarkUpInfo
                            If reader("OEMID") = 11 Then
                                If markUpInfo.Where(Function(s) s.oemID = 11).Count < 1 Then
                                    markUp.oemID = reader("OEMID")
                                    markUp.make = "GMC/Hummer"
                                    If companyID <> -1 Then
                                        markUp.markUp = CType(reader("markUp"), Decimal) * 100
                                        markUp.markUpCustomer = CType(reader("markupCustomer"), Decimal) * 100
                                    End If
                                    markUpInfo.Add(markUp)
                                End If
                            Else
                                markUp.oemID = reader("OEMID")
                                markUp.make = reader("Make")
                                If companyID <> -1 Then
                                    markUp.markUp = CType(reader("markUp"), Decimal) * 100
                                    markUp.markUpCustomer = CType(reader("markupCustomer"), Decimal) * 100
                                End If
                                markUpInfo.Add(markUp)
                            End If

                        End While
                    End Using
                End Using
            End Using
            If (companyID <> -1) Then
                companyToMirror = companyID
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class

Public Class dtoShippingInfo
    Public companyID As Integer
    Public address1 As String
    Public address2 As String
    Public contact As String
    Public city As String
    Public state As String
    Public zip As String
    Public closingTime As String
    Public shippingDirectionTypeID As Integer
    Public buildingLocation As Integer
    Public buildingPartCode As Integer
    Public locationDescription As String
    Public vendorShippingID As Integer
    Public vanityName As String
    Public vanityNameID As Integer
    Public shippingDirections As List(Of dtoShippingDirection)

    Sub New()
        buildingLocation = 0
        buildingPartCode = 1
        shippingDirectionTypeID = -1
        closingTime = New DateTime(1977, 8, 31, 17, 0, 0).ToString("hh:mm")
        shippingDirections = New List(Of dtoShippingDirection)
        For Each shippingDirectionID As Integer In CType([Enum].GetValues(GetType(ShippingDirectionTypes)), Integer())
            Dim direction As New dtoShippingDirection
            direction.ID = shippingDirectionID
            direction.direction = [Enum].GetName(GetType(ShippingDirectionTypes), shippingDirectionID)
            shippingDirections.Add(direction)
        Next
    End Sub
End Class

Public Class dtoVendorInfo
    Public companyInfo As dtoCompanyInfo
    Public emptyShippingInfo As dtoShippingInfo
    Public exsitingShippingInfo As List(Of dtoShippingInfo)
    Public oemDealerInfo As dtoOEMDealer
    Public invoiceGuideInfo As dtoInvoiceGuide
    Public exsitingCompanies As List(Of dtoBasicCompanyInfo)

    Sub New()
        companyInfo = New dtoCompanyInfo
        companyInfo.type = "Vendor"
        companyInfo.tax = "0"
        companyInfo.netShipping = "25"
        emptyShippingInfo = New dtoShippingInfo
        exsitingShippingInfo = New List(Of dtoShippingInfo)
        'exsitingShippingInfo.Add(New dtoShippingInfo)
        oemDealerInfo = New dtoOEMDealer(-1)
        invoiceGuideInfo = New dtoInvoiceGuide(-1)
        exsitingCompanies = New List(Of dtoBasicCompanyInfo)
        'exsitingCompanies.Add(New dtoBasicCompanyInfo)
    End Sub
End Class

Public Class dtoBasicCompanyInfo
    Public companyID As Integer
    Public companyName As String

    Sub New()
        companyID = 0
        companyName = String.Empty
    End Sub
End Class

Public Class dtoShippingDirection
    Public ID As Integer
    Public direction As String
End Class

Public Class dtoPaymentTypes
    Public ID As Integer
    Public direction As String
End Class

Public Class dtoVendorTypes
    Public ID As Integer
    Public direction As String
End Class

Public Class dtoMarkUpInfo
    Public oemID As Integer
    Public make As String
    Public markUp As Decimal
    Public markUpCustomer As Decimal
End Class

Public Class dtoInvoiceGuideElements
    Public elementID As Integer
    Public elementName As String
    Public checkBoxValue As Boolean 'could have just used value but this way it keeps me organized
    Public textValue As String
    Public elementDataTypeID As Integer
    Public description As String
End Class

#End Region