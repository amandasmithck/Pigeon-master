Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Pigeon.Pigeon
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports Pigeon.PricingWebService

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PigeonWebService
    Inherits System.Web.Services.WebService
    Public Class Menu
        Public Property ID() As Integer
            Get
                Return m_ID
            End Get
            Set(ByVal value As Integer)
                m_ID = value
            End Set
        End Property
        Private m_ID As Integer
        Public Property ParentID() As Integer
            Get
                Return m_ParentID
            End Get
            Set(ByVal value As Integer)
                m_ParentID = value
            End Set
        End Property
        Private m_ParentID As Integer
        Public Property Menu() As String
            Get
                Return m_Menu
            End Get
            Set(ByVal value As String)
                m_Menu = value
            End Set
        End Property
        Private m_Menu As String
        Public Property URL() As String
            Get
                Return m_URL
            End Get
            Set(ByVal value As String)
                m_URL = value
            End Set
        End Property
        Private m_URL As String
    End Class

    Public Class Client
        Public Property LogoURL() As String
            Get
                Return m_LogoURL
            End Get
            Set(ByVal value As String)
                m_LogoURL = value
            End Set
        End Property
        Private m_LogoURL As String
        Public Property BackgroundImgURL() As String
            Get
                Return m_BackgroundImgURL
            End Get
            Set(ByVal value As String)
                m_BackgroundImgURL = value
            End Set
        End Property
        Private m_BackgroundImgURL As String
        Public Property PromoHTML() As String
            Get
                Return m_PromoHTML
            End Get
            Set(ByVal value As String)
                m_PromoHTML = value
            End Set
        End Property
        Private m_PromoHTML As String
        Public Property Phone() As String
            Get
                Return m_Phone
            End Get
            Set(ByVal value As String)
                m_Phone = value
            End Set
        End Property
        Private m_Phone As String
        Public Property PartLines() As ArrayList
            Get
                Return m_PartLines
            End Get
            Set(ByVal value As ArrayList)
                m_PartLines = value
            End Set
        End Property
        Private m_PartLines As ArrayList
        Public Property Warranties() As ArrayList
            Get
                Return m_Warranties
            End Get
            Set(ByVal value As ArrayList)
                m_Warranties = value
            End Set
        End Property
        Private m_Warranties As ArrayList
    End Class

    Public Class PigeonClients
        Public Property Client() As String
            Get
                Return m_Client
            End Get
            Set(ByVal value As String)
                m_Client = value
            End Set
        End Property
        Private m_Client As String
        Public Property AdminUser() As String
            Get
                Return m_AdminUser
            End Get
            Set(ByVal value As String)
                m_AdminUser = value
            End Set
        End Property
        Public Property CKCustNo As String

        Private m_AdminUser As String
    End Class

    <WebMethod()>
    Public Function GetClientInfo(ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim c As New Client()
        Dim parts = New ArrayList, warranties = New ArrayList
        Try
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                conn.Open()
                Dim sqlComm As New SqlCommand("SELECT LogoURL, PromoHTML, BackgroundImgURL, Phone FROM tblPigeonClients where client = '" & client & "'", conn)
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        c.LogoURL = r("LogoURL").ToString()
                        c.PromoHTML = r("PromoHTML").ToString()
                        c.BackgroundImgURL = r("BackgroundImgURL").ToString()
                        c.Phone = r("Phone").ToString()

                    End While
                End Using

                Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                    conn2.Open()
                    Dim sqlComm2 As New SqlCommand("SELECT * FROM tblPigeonClientsMenu a Where a.Client = '" & client & "' and a.ParentID = (SELECT DISTINCT ID FROM tblPigeonClientsMenu as b WHERE b.Menu = 'Parts Search' AND b.Client = '" & client & "' AND b.Role = 'Customer') AND a.Menu <> 'Used Powertrain Assemblies' and a.visible=1", conn2)
                    Using r As SqlDataReader = sqlComm2.ExecuteReader()
                        While r.Read()
                            parts.Add(r("Menu"))
                        End While
                    End Using
                End Using
            End Using

            Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                conn2.Open()
                Dim sqlComm3 As New SqlCommand("Select Warranty FROM tblWarrantyOptions Group By Warranty", conn2)
                Using r As SqlDataReader = sqlComm3.ExecuteReader()
                    While r.Read()
                        warranties.Add(r("Warranty"))
                    End While
                End Using
            End Using

            c.Warranties = warranties
            c.PartLines = parts

            Return js.Serialize(c)
        Catch ex As Exception
            Return js.Serialize(ex.Message)
        End Try
    End Function

    <WebMethod()>
    Public Function GetMenus(ByVal role As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Menu)

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT id, menu, url, parentid from tblPigeonClientsMenu where client = '" & client & "' and role = '" & role & "' and visible = 1 order by sort", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim m1 As New Menu()
                    m1.ID = r("ID")
                    m1.Menu = r("Menu").ToString
                    m1.URL = r("URL").ToString
                    If (IsDBNull(r("ParentID")) = False) Then m1.ParentID = r("ParentID")

                    list.Add(m1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetWarranty(ByVal parttype As String, ByVal name As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()

        Dim warranties As New List(Of Warranty)

        Dim strtier = GetUserTier(name, client)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Select Href, Warranty, Base, Percentage, Flat from tblWarrantyOptions Where PartType = '" & parttype & "' and tblWarrantyOptions.Tier = '" & strtier & "' order by base desc", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader
                While r.Read()
                    Dim warranty As New Warranty
                    warranty.Warranty = r("Warranty")
                    warranty.Base = r("Base")
                    warranty.Percentage = r("Percentage")
                    warranty.Flat = r("Flat")
                    warranty.Href = r("Href")

                    warranties.Add(warranty)
                End While
            End Using
        End Using

        Return js.Serialize(warranties)
    End Function

    <WebMethod()>
    Public Function GetPigeonClients()
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of PigeonClients)

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT client, AdminUser, CKCustomerNo from tblPigeonClients where client <> 'CK' order by client", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim p1 As New PigeonClients()
                    p1.Client = r("Client")
                    p1.AdminUser = r("AdminUser").ToString()
                    p1.CKCustNo = r("CKCustomerNo")
                    list.Add(p1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Function PlaceOrder(ByVal source As String, ByVal parttype As String, ByVal Parts As List(Of Parts), ByVal name As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal VIN As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal warranty As String, ByVal Auth As String, ByVal Contract As String, ByVal EOCDate As String, ByVal EOCMileage As String, ByVal client As String, ByVal IP As String, ByVal customernumberbehalf As String, ByVal vendor As String, ByVal subtype As String, ByVal Notes As String, ByVal adjuster As String, ByVal email As String, ByVal quoteID As String, ByVal emailItems As String) As String
        Dim js As New JavaScriptSerializer
        Try
            If client = "CK" Then
                Return js.Serialize(ProcessOrderCK(source, parttype, Parts, name, year, make, model, PO, Customer, Mileage, VIN, RepairFacility.Replace("'", " "), Address, City, State, Zip, Phone, Contact, warranty, Auth, Contract, EOCDate, EOCMileage, client, IP, customernumberbehalf, vendor, subtype, Notes, adjuster, email, quoteID))
            Else
                Return js.Serialize(ProcessOrder(source, parttype, Parts, name, year, make, model, PO, Customer, Mileage, VIN, RepairFacility.Replace("'", " "), Address, City, State, Zip, Phone, Contact, warranty, client, IP, customernumberbehalf, vendor, subtype, Notes, email, quoteID, emailItems.Replace("/", "<br/>")))

            End If

        Catch Ex As Exception

            Dim mm As New System.Net.Mail.MailMessage("noreply@ckautoparts.com", "james_obrien@ckautoparts.com")

            mm.Subject = "Error Placing Order"
            mm.Body = "Message: <br />" & Ex.Message & "StackTrace: <br />" & Ex.StackTrace & "Customer That ran into this Issue: <br />" & Customer & "Client That ran into this Issue: <br />" & client & "Contact Email Of Person: <br />" & email
            mm.IsBodyHtml = True


            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)

            Return js.Serialize("Error Placing Order")
        End Try


    End Function
    <WebMethod()>
    Function GetOrderDetails(ByVal orderid As String, ByVal client As String)
        Dim js As New JavaScriptSerializer

        Return js.Serialize(RetrieveOrderDetails(orderid, client))
    End Function

    <WebMethod()>
    Function FinalizeOrder(ByVal orderid As String, ByVal shipfrom As String, ByVal corepickup As String, ByVal sn As String, ByVal po As String, ByVal name As String, ByVal client As String)

        Dim js As New JavaScriptSerializer

        Return js.Serialize(FinalizeCustomerOrder(orderid, shipfrom, corepickup, sn, po, name, client))

    End Function

    <WebMethod()>
    Public Function DecodeVIN(ByVal vin As String)
        Dim js As New JavaScriptSerializer

        Return js.Serialize(GetVehicleFromVIN(Trim(vin)))
    End Function
End Class