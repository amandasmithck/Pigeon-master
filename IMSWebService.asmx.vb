Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class IMSWebService
    Inherits System.Web.Services.WebService

    Public Class Stock
        Public Property ID As Long
            Get
                Return m_ID
            End Get
            Set(ByVal value As Long)
                m_ID = value
            End Set
        End Property
        Private m_ID As Long
        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property SN() As String
            Get
                Return m_SN
            End Get
            Set(ByVal value As String)
                m_SN = value
            End Set
        End Property
        Private m_SN As String
        Public Property Location() As String
            Get
                Return m_Location
            End Get
            Set(ByVal value As String)
                m_Location = value
            End Set
        End Property
        Private m_Location As String
        Public Property ETA() As String
            Get
                Return m_ETA
            End Get
            Set(ByVal value As String)
                m_ETA = value
            End Set
        End Property
        Private m_ETA As String
        Public Property Arrive() As String
            Get
                Return m_Arrive
            End Get
            Set(ByVal value As String)
                m_Arrive = value
            End Set
        End Property
        Private m_Arrive As String
        Public Property ReturnType() As String
            Get
                Return m_ReturnType
            End Get
            Set(ByVal value As String)
                m_ReturnType = value
            End Set
        End Property
        Private m_ReturnType As String
        Public Property Received() As Boolean
            Get
                Return m_Received
            End Get
            Set(ByVal value As Boolean)
                m_Received = value
            End Set
        End Property
        Private m_Received As Boolean
        Public Property DateReceived() As String
            Get
                Return m_DateReceived
            End Get
            Set(ByVal value As String)
                m_DateReceived = value
            End Set
        End Property
        Private m_DateReceived As String
        Public Property Process() As Boolean
            Get
                Return m_Process
            End Get
            Set(ByVal value As Boolean)
                m_Process = value
            End Set
        End Property
        Private m_Process As Boolean
        Public Property VIN() As String
            Get
                Return m_VIN
            End Get
            Set(ByVal value As String)
                m_VIN = value
            End Set
        End Property
        Private m_VIN As String
        Public Property Mileage() As String
            Get
                Return m_Mileage
            End Get
            Set(ByVal value As String)
                m_Mileage = value
            End Set
        End Property
        Private m_Mileage As String
        Public Property SoldDate() As String
            Get
                Return m_SoldDate
            End Get
            Set(ByVal value As String)
                m_SoldDate = value
            End Set
        End Property
        Private m_SoldDate As String


    End Class
    Public Class PartMove
        Public Property Part() As String
            Get
                Return m_Part
            End Get
            Set(ByVal value As String)
                m_Part = value
            End Set
        End Property
        Private m_Part As String
        Public Property SN() As String
            Get
                Return m_SN
            End Get
            Set(ByVal value As String)
                m_SN = value
            End Set
        End Property
        Private m_SN As String
        Public Property ETA() As String
            Get
                Return m_ETA
            End Get
            Set(ByVal value As String)
                m_ETA = value
            End Set
        End Property
        Private m_ETA As String
        Public Property Location() As String
            Get
                Return m_Location
            End Get
            Set(ByVal value As String)
                m_Location = value
            End Set
        End Property
        Private m_Location As String
        Public Property ReturnType() As String
            Get
                Return m_ReturnType
            End Get
            Set(ByVal value As String)
                m_ReturnType = value
            End Set
        End Property
        Private m_ReturnType As String
        Public Property Source() As String
            Get
                Return m_Source
            End Get
            Set(ByVal value As String)
                m_Source = value
            End Set
        End Property
        Private m_Source As String
        Public Property Dest() As String
            Get
                Return m_Dest
            End Get
            Set(ByVal value As String)
                m_Dest = value
            End Set
        End Property
        Private m_Dest As String
        Public Property DateReceived() As String
            Get
                Return m_DateReceived
            End Get
            Set(ByVal value As String)
                m_DateReceived = value
            End Set
        End Property
        Private m_DateReceived As String
        Public Property Success() As String
            Get
                Return m_Success
            End Get
            Set(ByVal value As String)
                m_Success = value
            End Set
        End Property
        Private m_Success As String
    End Class
    Public Class Bin
        Public Property Bin As String
            Get
                Return m_Bin
            End Get
            Set(ByVal value As String)
                m_Bin = value
            End Set
        End Property
        Private m_Bin As String
    End Class
    'Public Class Warrantyform
    '    Public Property ClaimNo() As String
    '        Get
    '            Return m_Part
    '        End Get
    '        Set(ByVal value As String)
    '            m_Part = value
    '        End Set
    '    End Property
    '    Private m_Part As String
    'End Class
    <WebMethod()>
    Public Function GetBins(ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Bin)


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT * from tblbins order by bin", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim b1 As New Bin()
                    b1.Bin = r("Bin")
                    list.Add(b1)
                End While
            End Using
        End Using
        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function GetStock(ByVal client As String, ByVal type As Integer)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Stock)
        Dim response = New ArrayList
        Dim strsql As String
        If type = 3 Then 'also show transfer cases
            strsql = "SELECT * from tblStock WHERE type = 3 or type = 4 ORDER BY sn"
        ElseIf type = 2 Then 'also showreman turbos
            strsql = "SELECT * from tblStock WHERE type = 2 or type = 11 ORDER BY sn"
        Else
            strsql = "SELECT * from tblStock WHERE type = " & type & " ORDER BY sn"
        End If

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strsql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim s1 As New Stock()
                    s1.ID = r("ID")
                    s1.Part = r("part")
                    s1.SN = r("sn")
                    If IsDBNull(r("location")) = False Then s1.Location = r("location")
                    s1.ETA = r("eta")
                    If IsDBNull(r("arrive")) = False Then s1.Arrive = r("arrive")
                    If IsDBNull(r("returntype")) = False Then s1.ReturnType = r("returntype")
                    s1.Received = r("received")
                    If IsDBNull(r("datereceived")) = False Then s1.DateReceived = r("datereceived")
                    s1.Process = r("Process")
                    If IsDBNull(r("vin")) = False Then s1.VIN = r("vin")
                    If IsDBNull(r("mileage")) = False Then s1.Mileage = r("mileage")
                    If IsDBNull(r("solddate")) = False Then s1.SoldDate = r("solddate")

                    list.Add(s1)
                End While
            End Using
        End Using
        ' Return js.Serialize(list)
        response.Add(list)



        Dim list2 As New List(Of Bin)


        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT * from tblbins order by bin", conn)
            conn.Open()
            Using r2 As SqlDataReader = sqlComm2.ExecuteReader()
                While r2.Read()
                    Dim b1 As New Bin()
                    b1.Bin = r2("Bin")
                    list2.Add(b1)
                End While
            End Using
        End Using
        response.Add(list2)
        Return js.Serialize(response)
    End Function



    <WebMethod()>
    Public Function UpdatePartBin(ByVal SN As String, ByVal NewBin As String, ByVal client As String)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("update tblStock set location = '" & NewBin & "' where sn='" & SN & "'", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        Return True
    End Function

    <WebMethod()>
    Public Function UpdatePart(ByVal SN As String, ByVal Source As String, ByVal Dest As String, ByVal Val As String, ByVal client As String)
        'On Error GoTo errorz
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of PartMove)
        Dim listErr As New List(Of PartMove)
        Dim response = New ArrayList

        GetDefaults(client)

        Select Case Dest

            Case "Inventory"
                If Val = "Cancel" Then 'Cancel Sale
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm As New SqlCommand("update tblStock set returntype =null, received = 0, datereceived=null, process=0,vin=null,mileage=null,solddate=null where sn='" & SN & "'", conn)
                        conn.Open()
                        sqlComm.ExecuteNonQuery()
                    End Using

                    EmailCK(SN, "cancelsale", clientckorderemail)

                Else 'Normal place in inventory

                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm As New SqlCommand("update tblStock set location = '" & Val & "', arrive = '" & Now() & "' where sn='" & SN & "'", conn)
                        conn.Open()
                        sqlComm.ExecuteNonQuery()
                    End Using

                End If


            Case "Field" 'normal sale

                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm As New SqlCommand("update tblStock set returntype = '" & Val & "', solddate =  '" & Now() & "' where sn='" & SN & "'", conn)
                    conn.Open()
                    sqlComm.ExecuteNonQuery()
                End Using

                'get part type
                Dim intPartType As Long
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm As New SqlCommand("select type from tblstock where sn='" & SN & "'", conn)
                    conn.Open()
                    intPartType = sqlComm.ExecuteScalar()
                End Using

                If intPartType = 1 Then
                    If client = "LarryMiller" Then
                        EmailCK(SN, "sold", clientckorderemail)
                    End If
                    Dim consumeWebService As CertifiedSell2.Warehouses
                    consumeWebService = New CertifiedSell2.Warehouses
                    Dim ds As DataSet
                    If SN.Contains("-") = True Then
                        Dim strRealSN
                        strRealSN = SN.Split("-")
                        Try
                            ds = consumeWebService.SellPart(clientcertifiedkey, GetSNCertifiedCode(SN, client), strRealSN(0))
                        Catch ex As Exception
                            GoTo smtcerror
                        End Try
                    Else
                        Try
                            ds = consumeWebService.SellPart(clientcertifiedkey, GetSNCertifiedCode(SN, client), SN)
                        Catch ex As Exception
                            GoTo smtcerror
                        End Try
                    End If


                    Dim dt As DataTable = ds.Tables(0)
                    Dim dc As DataColumn
                    Dim dr As DataRow

                    For Each dr In dt.Rows
                        If dr(0) <> "Success" Then
                            GoTo smtcerror
                        End If
                        Exit For
                    Next


                End If
            Case "Vendor"
                If Val = "Damage" Then 'Damage Unit

                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm As New SqlCommand("update tblStock set returntype= 'damage', received = 1, arrive = '" & Now & "', datereceived = '" & Now() & "' where sn='" & SN & "'", conn)
                        conn.Open()
                        sqlComm.ExecuteNonQuery()
                    End Using

                    EmailCK(SN, "damage", clientckorderemail)
                ElseIf Val = "Unsalable" Then

                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm As New SqlCommand("update tblStock set returntype= 'unsalable' where sn='" & SN & "'", conn)
                        conn.Open()
                        sqlComm.ExecuteNonQuery()
                    End Using
                Else 'normal return received

                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm As New SqlCommand("update tblStock set received = 1, datereceived = '" & Now() & "' where sn='" & SN & "'", conn)
                        conn.Open()
                        sqlComm.ExecuteNonQuery()
                    End Using

                End If

        End Select

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT * from tblStock where sn = '" & SN & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()

                While r.Read()
                    Dim p1 As New PartMove()
                    If IsDBNull(r("ETA")) = False Then p1.ETA = r("ETA")
                    If IsDBNull(r("Location")) = False Then p1.Location = r("Location")
                    p1.Part = r("Part")
                    If IsDBNull(r("ReturnType")) = False Then p1.ReturnType = r("ReturnType")
                    If IsDBNull(r("DateReceived")) = False Then p1.DateReceived = r("DateReceived")
                    p1.SN = SN
                    p1.Source = Source
                    p1.Dest = Dest
                    p1.Success = "1"
                    list.Add(p1)

                End While
            End Using
        End Using

        Return js.Serialize(list)
        Exit Function
smtcerror:
        Dim mm As New System.Net.Mail.MailMessage(clientckorderemail, clientckorderemail)

        mm.Subject = "Remove from SMTC"
        mm.Body = "Serial number " & SN & " has been sold from the " & client & " IMS. It appears there may have been an issue automatically removing from SMTC. Double check SMTC and remove manually if needed."

        mm.IsBodyHtml = True
        mm.CC.Add("darrellb@ckautoparts.com")
        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
        Catch Ex As Exception
            Console.WriteLine(Ex)
        End Try

errorz:
        Console.WriteLine(Err.Number)
        Using conn2 As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlCommErr As New SqlCommand("SELECT * from tblStock where sn = '" & SN & "'", conn2)
            conn2.Open()
            Using rErr As SqlDataReader = sqlCommErr.ExecuteReader()
                While rErr.Read()
                    Dim pErr As New PartMove()
                    If IsDBNull(rErr("ETA")) = False Then pErr.ETA = rErr("ETA")
                    If IsDBNull(rErr("Location")) = False Then pErr.Location = rErr("Location")
                    pErr.Part = rErr("Part")
                    If IsDBNull(rErr("ReturnType")) = False Then pErr.ReturnType = rErr("ReturnType")
                    If IsDBNull(rErr("DateReceived")) = False Then pErr.DateReceived = rErr("DateReceived")
                    pErr.SN = SN
                    pErr.Source = Source
                    pErr.Dest = Dest
                    pErr.Success = "0"
                    listErr.Add(pErr)
                End While
            End Using
        End Using
        Return js.Serialize(listErr)
    End Function



    <WebMethod()>
    Public Function AddInService(ByVal sn As String, ByVal po As String, ByVal vin As String, ByVal mileage As String, ByVal solddate As String, ByVal client As String)
        Return EmailAddInService(sn, po, vin, mileage, solddate, client)

    End Function
    <WebMethod()>
    Public Function EditInfo(ByVal sn As String, ByVal vin As String, ByVal mileage As String, ByVal solddate As String, ByVal client As String)
        On Error GoTo errorz

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("update tblStock set vin= '" & vin & "', Mileage = '" & mileage & "', solddate = '" & solddate & "' where sn ='" & sn & "'", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using


        Return True

        Exit Function
errorz:
        Return True

    End Function
    <WebMethod()>
    Public Function InitiateWarranty(ByVal ClaimNo As String, ByVal Shop As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal Contact As String, ByVal VIN As String, ByVal SerialNo As String, ByVal MilesInstall As String, ByVal MilesNow As String, ByVal DateInstall As String, ByVal EngineHealth As String, ByVal FluidLevel As String, ByVal FluidCondition As String, ByVal Leaks As String, ByVal Noises As String, ByVal Codes As String, ByVal WarningLights As String, ByVal Complaint As String, ByVal ComplaintTemp As String, ByVal ComplaintMiles As String, ByVal Comments As String, ByVal client As String)
        On Error GoTo errorz
        GetDefaults(client)

        'email ck
        Dim mm As New System.Net.Mail.MailMessage(clientckorderemail, clientckorderemail)
        mm.Subject = clientwebsitename & " Warranty Claim Initiated"
        Dim strbody As String

        strbody = "Warranty Claim Detials Below:<br/><br/>"
        strbody = strbody & "ClaimNo:" & ClaimNo & "<br/>"
        strbody = strbody & "Shop:" & Shop & "<br/>"
        strbody = strbody & "Address:" & Address & "<br/>"
        strbody = strbody & "City:" & City & "<br/>"
        strbody = strbody & "State:" & State & "<br/>"
        strbody = strbody & "Zip:" & Zip & "<br/>"
        strbody = strbody & "Phone:" & Phone & "<br/>"
        strbody = strbody & "Contact:" & Contact & "<br/>"
        strbody = strbody & "VIN:" & VIN & "<br/>"
        strbody = strbody & "SerialNo:" & SerialNo & "<br/>"
        strbody = strbody & "MilesInstall:" & MilesInstall & "<br/>"
        strbody = strbody & "MilesNow:" & MilesNow & "<br/>"
        strbody = strbody & "DateInstall:" & DateInstall & "<br/>"
        strbody = strbody & "EngineHealth:" & EngineHealth & "<br/>"
        strbody = strbody & "FluidLevel:" & FluidLevel & "<br/>"
        strbody = strbody & "FluidConditon:" & FluidCondition & "<br/>"
        strbody = strbody & "Leaks:" & Leaks & "<br/>"
        strbody = strbody & "Noises:" & Noises & "<br/>"
        strbody = strbody & "Codes:" & Codes & "<br/>"
        strbody = strbody & "WarningLights:" & WarningLights & "<br/>"
        strbody = strbody & "Complaint:" & Complaint & "<br/>"
        strbody = strbody & "ComplaintTemp:" & ComplaintTemp & "<br/>"
        strbody = strbody & "ComplaintMiles:" & ComplaintMiles & "<br/>"
        strbody = strbody & "Comments:" & Comments & "<br/>"

        mm.Body = strbody
        mm.IsBodyHtml = True

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Host = "smtp.emailsrvr.com"
        smtp.Timeout = 500000
        smtp.Send(mm)
        Return True

        Exit Function
errorz:
        Return True
    End Function
    '<WebMethod()> _
    Public Function EmailCK(ByVal SN As String, type As String, ByVal clientckorderemail As String)
        Dim strTO As String = String.Empty
        Dim strSubject As String = String.Empty
        Dim strBody As String = String.Empty

        Select Case type
            Case "cancelsale"
                strTO = "andrew_rand@ckautoparts.com"
                strSubject = "Cancelled Transmision Sale"
                strBody = "Serial number " & SN & " has been cancelled. Please go to SMTC and place back in inventory as soon as possible."
            Case "sold"
                strTO = "andrew_rand@ckautoparts.com"
                strSubject = "Sold Transmission"
                strBody = "Serial number " & SN & " has been removed from invetory. Please perform any needed verification."

            Case "damage"
                strTO = "andrew_rand@ckautoparts.com"
                strSubject = "Damaged Transmission Upon Arrival"
                strBody = "Serial number " & SN & " has been noted as damage upon arrival.  Please take appropriate actions."

        End Select

        Dim mm As New System.Net.Mail.MailMessage(clientckorderemail, strTO)
        mm.Subject = strSubject
        mm.Body = strBody
        mm.IsBodyHtml = True
        'mm.CC.Add("darrellb@ckautoparts.com")
        Try
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "smtp.emailsrvr.com"
            smtp.Timeout = 500000
            smtp.Send(mm)
        Catch Ex As Exception
            Console.WriteLine(Ex)
        End Try
    End Function

    Public Function GetSNCertifiedCode(ByVal sn As String, ByVal client As String) As String
        Dim strCode As String
        Dim intWarehouseID As Long

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT warehouseid from tblstock  where sn = '" & sn & "'", conn)
            conn.Open()
            intWarehouseID = sqlComm.ExecuteScalar
        End Using


        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT certifiedcode from tblremanwarehouses where id = " & intWarehouseID, conn)
            conn.Open()
            strCode = sqlComm.ExecuteScalar
        End Using
        Return strCode
    End Function

End Class