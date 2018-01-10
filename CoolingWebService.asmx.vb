Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Globalization
Imports System.Data.SqlClient
Imports Pigeon.Pigeon



' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CoolingWebService
    Inherits System.Web.Services.WebService

    Public Class Parts
        Public Property PartNumber() As String
            Get
                Return m_PartNumber
            End Get
            Set(ByVal value As String)
                m_PartNumber = value
            End Set
        End Property
        Private m_PartNumber As String
        Public Property SalePrice() As String
            Get
                Return m_SalePrice
            End Get
            Set(ByVal value As String)
                m_SalePrice = value
            End Set
        End Property
        Private m_SalePrice As String

    End Class
    <WebMethod()> _
    Public Function GetCatalog(an As String, aid As String, zip As String, vid As String) As String
        Dim SearchUrl As String = "http://www.performanceradiator.com/ws_cat/catFetch.php?an={0}&aid={1}&zip={2}&vid={3}"
        Dim formattedUri As String = [String].Format(CultureInfo.InvariantCulture, SearchUrl, an, aid, zip, vid)

        Dim webRequest As HttpWebRequest = GetWebRequest(formattedUri)
        Dim response As HttpWebResponse = DirectCast(webRequest.GetResponse(), HttpWebResponse)
        Dim jsonResponse As String = String.Empty
        Using sr As New StreamReader(response.GetResponseStream())
            jsonResponse = sr.ReadToEnd()
        End Using
        Return System.Text.Encoding.UTF8.GetString(FromBase64(jsonResponse))

    End Function


    <WebMethod()>
    Public Function SendEmail(ByVal Parts As Parts(), ByVal VIN As String, ByVal Name As String, ByVal PO As String, ByVal Customer As String, ByVal Mileage As String, ByVal RepairFacility As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal client As String)

        'get salesman email, customername 


        Dim strcompany As String
        Dim stremail As String
        Dim strphone As String
        Dim straddress As String
        Dim strAutonation As Boolean


        GetDefaults(client)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Membership.CustomerNo, dbo.tblCompany.SalesmanEmail, dbo.tblCompany.Company, dbo.tblCompany.Phone,  dbo.tblCompany.Address + ',' + dbo.tblCompany.City + N',' + dbo.tblCompany.State + N' ' + dbo.tblCompany.Zip AS Address, dbo.aspnet_Membership.Email, dbo.tblCompany.Autonation FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN dbo.tblCompany ON dbo.aspnet_Membership.CustomerNo = dbo.tblCompany.CustomerNo where username = '" & Name & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If IsDBNull(r("salesmanemail").ToString) = False Then clientemails = r("salesmanemail").ToString
                    strcompany = r("company").ToString
                    strphone = r("phone").ToString
                    straddress = r("address").ToString
                    stremail = r("email").ToString
                    strAutonation = CType(r("Autonation").ToString, Boolean)

                End While
            End Using
        End Using

        'split emails if any

        Dim emailtocc() As String
        emailtocc = clientemails.Split(",")

        Dim mm As New System.Net.Mail.MailMessage(stremail, emailtocc(0))
        mm.Subject = clientwebsitename & " Cooling Order"
        Dim strbody As String

        strbody &= "Company: " & strcompany & "<br/>"
        strbody &= "Address: " & straddress & "<br/>"
        strbody &= "User: " & Customer & "<br/>"
        strbody &= "Phone: " & strphone & "<br/><br/>"


        strbody &= "VIN: " & VIN & "<br/>"
        strbody &= "ZIP: " & Zip & "<br/>"

        For Each thepart As Parts In Parts
            strbody &= "Part No:" & thepart.PartNumber & "-" & thepart.SalePrice & "<br/>"
        Next

        strbody &= "PO: " & PO & "<br/>"
        strbody &= "Customer Last Name: " & Customer & "<br/>"
        strbody &= "Mileage: " & Mileage & "<br/>"
        strbody &= "VIN: " & VIN & "<br/>"
        strbody &= "Repair Facility: " & RepairFacility & "<br/>"
        strbody &= "Contact Info: <br/>"
        strbody &= Contact & "<br/>"
        strbody &= Address & "<br/>"
        strbody &= City & "," & State & " " & Zip & "<br/>"
        strbody &= Phone & "<br/>"


        mm.Body = strbody
        mm.IsBodyHtml = True

        For x As Integer = 1 To emailtocc.Count - 1
            mm.CC.Add(emailtocc(x))
        Next

        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
            EmailCustomer(stremail, Parts, clientnoreplyemail, clientwebsitename)
            Return True
        Catch Ex As Exception
            Console.WriteLine(Ex)
            Return False
        End Try
    End Function

    Function EmailCustomer(ByVal adjemail As String, ByVal Parts As Parts(), noreplyemail As String, websitename As String)

        Dim mm As New System.Net.Mail.MailMessage(noreplyemail, adjemail)
        mm.Subject = websitename & " Cooling System Order Received"
        Dim strbody As String = String.Empty


        For Each thepart As Parts In Parts
            strbody &= "Part No:" & thepart.PartNumber & "-" & thepart.SalePrice & "<br/>"
        Next


        strbody &= "<br/><br/> Your order will be processed as soon as possible and you will be contacted with any issues."
        mm.Body = strbody
        mm.IsBodyHtml = True

        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
            Return True
        Catch Ex As Exception
            Console.WriteLine(Ex)
            Return False
        End Try

    End Function
End Class