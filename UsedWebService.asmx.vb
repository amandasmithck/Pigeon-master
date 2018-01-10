Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Pigeon.Pigeon
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization



' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class UsedWebService
    Inherits System.Web.Services.WebService


    <WebMethod()>
    Public Function UsedRequest(ByVal type As String, ByVal price As String, ByVal auth As String, ByVal contract As String, ByVal owner As String, ByVal mileage As String, ByVal year As String, ByVal make As String, ByVal model As String, ByVal facility As String, ByVal address As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal phone As String, ByVal contact As String, ByVal vin As String, ByVal part As String, ByVal partdescgroup As String, ByVal warranty As String, ByVal eocdate As String, ByVal eocmileage As String, ByVal comments As String, ByVal name As String, ByVal email As String, ByVal client As String)

        Dim strcompany As String
        Dim stremail As String
        Dim strphone As String
        Dim straddress As String
        Dim strcustno As String
        Dim strbody As String
        Dim js As New JavaScriptSerializer
        GetDefaults(client)


        Dim strsql As String
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            If client = "CK" Then
                strsql = "SELECT dbo.aspnet_Membership.CustomerNo, dbo.tblCompany.Company, dbo.tblCompany.Phone,  dbo.tblCompany.Address1 + ',' + dbo.tblCompany.City + N',' + dbo.tblCompany.State + N' ' + dbo.tblCompany.Zip as Address, dbo.aspnet_Membership.Email, '' as salesmanemail FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN dbo.tblCompany ON dbo.aspnet_Membership.CustomerNo = dbo.tblCompany.CustomerNo where username = '" & name & "'"
            Else
                strsql = "SELECT dbo.aspnet_Membership.CustomerNo, dbo.tblCompany.Company, dbo.tblCompany.Phone,  dbo.tblCompany.Address + ',' + dbo.tblCompany.City + N',' + dbo.tblCompany.State + N' ' + dbo.tblCompany.Zip AS Address, dbo.aspnet_Membership.Email, salesmanemail  FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId INNER JOIN dbo.tblCompany ON dbo.aspnet_Membership.CustomerNo = dbo.tblCompany.CustomerNo where username = '" & name & "'"
            End If

            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If IsDBNull(r("salesmanemail").ToString) = False Then clientemails = r("salesmanemail").ToString
                    strcompany = r("company").ToString
                    strphone = r("phone").ToString
                    straddress = r("address").ToString
                    stremail = email 'r("email").ToString
                    If client = "CK" Then
                        strcustno = r("CustomerNo").ToString
                    Else
                        strcustno = clientckcustomerno
                    End If

                End While
            End Using
        End Using

        Select Case type
            Case "Order"
                Dim intOrderID As Long
                'order table
                Try
                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim strsqlOrder = "Insert into tblOrder (AuthorizationNo, ContractNo, VInNo, AutoYear, AutoMake, AutoModel, AutoOwner, CustomerNo, DateOrdered, EnteredBy, Mileage, JustaField, AdjusterName, AdjusterEmail, Notes) values ('" & auth & "', '" & contract & "', '" & vin & "', '" & year & "', '" & make & "', '" & model & "', '" & owner.Replace("'", " ") & "', '" & strcustno & "','" & Now() & "', 'Web', '" & mileage & "',1,'" & name & "', '" & email & "', '" & comments.Replace("'", "") & "');SELECT SCOPE_IDENTITY()"
                        Dim sqlCommOrder As New SqlCommand(strsqlOrder, conn)
                        conn.Open()
                        intOrderID = sqlCommOrder.ExecuteScalar()
                    End Using

                    'part order table
                    Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim strsqlPartOrder = "Insert into tblPartOrder(Orderid, Address1, City, Contact, DateEntered, EnteredBy, PartDescription, PartNO, Phone, Quantity, SellPrice, Servicer, State, Vendor, Zip, Warranty, WarrantyDate, WarrantyMileage, PartDescGroup, PartType) values (" & intOrderID & ",'" & address.Replace("'", "") & "','" & city & "','" & contact.Replace("'", " ") & "','" & Now() & "','Web', '" & part & "', '" & Nothing & "','" & phone & "','1','" & price & "','" & facility.Replace("'", "") & "','" & state & "','10827', '" & zip & "','" & warranty & "','" & eocdate & "','" & eocmileage & "','" & partdescgroup & "','Aftermarket')"
                        Dim sqlCommPartOrder As New SqlCommand(strsqlPartOrder, conn)
                        conn.Open()
                        sqlCommPartOrder.ExecuteNonQuery()
                    End Using

                    'email customer
                    Dim mm2 As New System.Net.Mail.MailMessage(clientckorderemail, email)
                    mm2.Subject = clientwebsitename & " Your Powertrain order has been received"

                    strbody = strbody & "Order #: " & intOrderID & "<br/><br/>"

                    strbody = strbody & "Vehicle: " & year & " " & make & " " & model & "<br/>"
                    strbody = strbody & "VIN: " & vin & "<br/>"
                    strbody = strbody & "Part: " & part & "<br/>"
                    strbody = strbody & "Quoted Price: " & price & "<br/>"
                    strbody = strbody & "Auth No: " & auth & "<br/>"
                    strbody = strbody & "Contract No: " & contract & "<br/>"
                    strbody = strbody & "Owner: " & owner & "<br/>"
                    strbody = strbody & "Mileage: " & mileage & "<br/>"
                    strbody = strbody & "Warranty: " & warranty & "<br/>"
                    strbody = strbody & "Repair Facility: " & facility & "<br/>"
                    strbody = strbody & "Address: " & address & "<br/>"
                    strbody = strbody & "City: " & city & "<br/>"
                    strbody = strbody & "State: " & state & "<br/>"
                    strbody = strbody & "Zip: " & zip & "<br/>"
                    strbody = strbody & "Phone: " & phone & "<br/>"
                    strbody = strbody & "Contact: " & contact & "<br/>"
                    strbody = strbody & "Comments: " & comments & "<br/><br/>"
                    mm2.Body = strbody
                    mm2.IsBodyHtml = True
                    Dim smtp As New System.Net.Mail.SmtpClient
                    smtp.Host = "smtp.emailsrvr.com"
                    smtp.Timeout = 500000
                    smtp.Send(mm2)

                    Return js.Serialize(intOrderID)
                Catch Ex As Exception
                    Console.WriteLine(Ex)
                    Return js.Serialize(False)
                End Try



            Case "Quote"


                ' If stremail = "" Then stremail = "sales@ckautoparts.com"
                Dim mm As New System.Net.Mail.MailMessage(email, clientckorderemail)

                'Select Case type
                '    Case "Quote"
                mm.Subject = clientwebsitename & " Used Powertrain Assembly Quote Request"
                '    Case "Order"
                'mm.Subject = clientwebsitename & " Used Powertrain Assembly Order!"
                'End Select



                strbody = strbody & "Company: " & strcompany & "<br/>"
                strbody = strbody & "Address: " & straddress & "<br/>"
                strbody = strbody & "User: " & name & "<br/>"
                strbody = strbody & "Email: " & email & "<br/>"
                strbody = strbody & "Phone: " & strphone & "<br/><br/>"


                strbody = strbody & "Vehicle: " & year & " " & make & " " & model & "<br/>"
                strbody = strbody & "VIN: " & vin & "<br/>"
                strbody = strbody & "Part: " & part & "<br/>"
                'Select Case type
                '    Case "Order"
                '        strbody = strbody & "Quoted Price: " & price & "<br/>"
                '        strbody = strbody & "Auth No: " & auth & "<br/>"
                '        strbody = strbody & "Contract No: " & contract & "<br/>"
                '        strbody = strbody & "Owner: " & owner & "<br/>"
                '        strbody = strbody & "Mileage: " & mileage & "<br/>"
                '        strbody = strbody & "Warranty: " & warranty & "<br/>"
                '        strbody = strbody & "Repair Facility: " & facility & "<br/>"
                '        strbody = strbody & "Address: " & address & "<br/>"
                '        strbody = strbody & "City: " & city & "<br/>"
                '        strbody = strbody & "State: " & state & "<br/>"
                '        strbody = strbody & "Zip: " & zip & "<br/>"
                '        strbody = strbody & "Phone: " & phone & "<br/>"
                '        strbody = strbody & "Contact: " & contact & "<br/>"

                'End Select
                strbody = strbody & "Comments: " & comments & "<br/><br/>"
                mm.Body = strbody
                mm.IsBodyHtml = True


                Try
                    'email client
                    Dim smtp As New System.Net.Mail.SmtpClient
                    smtp.Host = "smtp.emailsrvr.com"
                    smtp.Timeout = 500000
                    smtp.Send(mm)

                    Return js.Serialize(True)
                Catch Ex As Exception
                    Console.WriteLine(Ex)
                    Return js.Serialize(False)
                End Try
        End Select
    End Function
    <WebMethod()>
    Public Function GetPartTypes()
        Dim c As New List(Of CKPartType)
        Dim js As New JavaScriptSerializer
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select description, [group] as partdescgroup from tblpartdesc inner join tblpartdescgroup on tblpartdesc.groupid=tblpartdescgroup.groupid where (description like 'Used%' or description like 'Reman%') and (description <> 'Used Other')  order by description", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim c1 As New CKPartType
                    c1.Part = r("description").ToString
                    c1.PartDescGroup = r("partdescgroup").ToString
                    c.Add(c1)
                End While
            End Using
        End Using
        Return js.Serialize(c)
    End Function


End Class