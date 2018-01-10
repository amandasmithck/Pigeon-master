Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports System.Web.Script.Serialization
Imports System.Configuration


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class EmailWebService
    Inherits System.Web.Services.WebService

    Public Class CustomerInfo
        Public CustomerName As String
        Public CustomerNo As String
    End Class

    Public Class Updated
        Public Success As Boolean
        Public OrderID As Int32
        Public MessageError As String
    End Class
    Public Class Emails
        Public MainDate As String
        Public CustomerName As String
        Public EmailType As String 'Change to String when Grabing Actual EmailType
        Public WhoTo As String
        Public OrderID As Int32
        Public Processed As String
        Public isError As String
        Public Shipper As String
        Public TrackingNo As String
        Public ETA As String
        Public WhoEntered As String
        Public ID As Long
    End Class

    <WebMethod>
    Public Function getCustomers()
        Dim js As New JavaScriptSerializer
        Dim AllCustomers As New List(Of CustomerInfo)
        Dim query As String = "select Company,CustomerNo from tblCompany where CustomerNo is not null Order By Company Asc"
        '"select top 1 tblOrder.AutoOwner, tblPartOrder.FreightETA,tblCompany.Company as Company,tblPartOrder.ShipperTrack from tblOrder inner join tblPartOrder on tblOrder.OrderID = tblPartOrder.OrderID inner join tblCompany on tblPartOrder.Shipper = tblCompany.CompanyID where tblOrder.OrderID=" & Email.OrderID
        'Query Jumps from tblOrder to tblPartOrder? to tblCompany..gets the TOP 1 Row
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            conn.Open()
            Using comm As New SqlCommand(query, conn)
                Using reader As SqlDataReader = comm.ExecuteReader()
                    While reader.Read()
                        Dim Customer As New CustomerInfo()
                        Customer.CustomerName = reader.GetString(0)
                        Customer.CustomerNo = reader.GetString(1)
                        AllCustomers.Add(Customer)
                    End While
                End Using
            End Using
        End Using
        Return js.Serialize(AllCustomers)
    End Function

    <WebMethod>
    Public Function AddEmail(ByVal ID)
        'Select Case for the EmailType to Set it and to get Info Like the SSIS?
        Dim js As New JavaScriptSerializer
        Dim UpdateStatus As New Updated()
        Try
            Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
                Dim sqlComm As New SqlCommand("usp_ResendAutoEmail", conn)
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.Add("@userid", SqlDbType.NVarChar).Value = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString()
                sqlComm.Parameters.Add("@id", SqlDbType.Int).Value = ID
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
            UpdateStatus.Success = True
            Return js.Serialize(UpdateStatus)
        Catch Ex As Exception
            UpdateStatus.Success = False
            UpdateStatus.MessageError = ex.Message
            Return js.Serialize(UpdateStatus)
        End Try


    End Function
    <WebMethod>
    Public Function getEmails(ByVal StartDate, ByVal EndDate, ByVal OrderID, ByVal CustomerNo)
        'Select Case for the EmailType to Set it and to get Info Like the SSIS?
        Dim query As String = String.Empty
        Dim SDate As DateTime = Convert.ToDateTime(StartDate)
        Dim EDate As DateTime = Convert.ToDateTime(EndDate).AddDays(1)

        If OrderID <> "" Then
            query = "select top 50 tblautoemail.ID, DateTime, isnull(tblautoemail.userid,'') as UserID, EmailType, SendTo, CC, SendFrom, Subject, isnull(orderid,0) as OrderID, isnull(partid,0) as PartID, Processed, Error,tblEmailTypes.Type,aspnet_Users.UserName from tblAutoEmail inner join tblEmailTypes on tblAutoEmail.EmailType=tblEmailTypes.ID inner join aspnet_Users on tblAutoEmail.userID=aspnet_Users.userID where OrderID=" & Convert.ToInt32(OrderID) & " order by tblautoemail.id desc"
        Else
            If CustomerNo <> 0 Or CustomerNo <> "" Then
                query = "select top 100 tblautoemail.ID, DateTime, isnull(tblautoemail.userid,'') as UserID, EmailType, SendTo, CC, SendFrom, Subject, isnull(tblautoemail.orderid,0) as OrderID, isnull(partid,0) as PartID, Processed, Error,tblEmailTypes.Type,aspnet_Users.UserName from tblAutoEmail inner join tblEmailTypes on tblAutoEmail.EmailType=tblEmailTypes.ID inner join aspnet_Users on tblAutoEmail.userID=aspnet_Users.userID inner join tblOrder on tblAutoEmail.OrderID=tblOrder.OrderID where tblOrder.CustomerNo='" & CustomerNo & "' order by tblautoemail.id desc"
            Else
                query = "select top 50 tblautoemail.ID, DateTime, isnull(tblautoemail.userid,'') as UserID, EmailType, SendTo, CC, SendFrom, Subject, isnull(orderid,0) as OrderID, isnull(partid,0) as PartID, Processed, Error,tblEmailTypes.Type,aspnet_Users.UserName from tblAutoEmail inner join tblEmailTypes on tblAutoEmail.EmailType=tblEmailTypes.ID inner join aspnet_Users on tblAutoEmail.userID=aspnet_Users.userID where DateTime >='" & SDate & "' and DateTime <='" & EDate & "' order by tblautoemail.id desc"

            End If
        End If



        Dim js As New JavaScriptSerializer

        Dim EmailList As New List(Of Emails)
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            conn.Open()
            Using comm As New SqlCommand(query, conn)
                Using reader As SqlDataReader = comm.ExecuteReader()
                    While reader.Read()
                        Dim Email As New Emails()
                        Email.ID = reader("ID")
                        Dim dtMainDate As DateTimeOffset
                        dtMainDate = reader("DateTime")
                        Email.MainDate = dtMainDate.DateTime.ToString()
                        Email.EmailType = reader("Type")
                        Email.WhoTo = reader("SendTo")
                        Email.OrderID = reader("OrderID")
                        If String.IsNullOrEmpty(reader("Processed").ToString()) = False Then
                            Dim dtProcessed As DateTimeOffset
                            dtProcessed = reader("Processed")
                            Email.Processed = dtProcessed.DateTime.ToString()
                            Email.isError = If(CBool(reader("Error")) = True, "Yes", "No")
                        End If
                        Email.WhoEntered = reader("Username")
                        'Dim objType As Type = Email.GetType()
                        'Dim properties As System.Reflection.PropertyInfo() = objType.GetProperties()

                        'For Each prop As System.Reflection.PropertyInfo In properties
                        '    Try

                        '        prop.SetValue(Email, reader(prop.Name), Nothing)


                        '    Catch Ex As Exception

                        '    End Try
                        'Next
                        Email = getOtherInfoForEmailTable(Email)
                        EmailList.Add(Email)
                    End While
                End Using
            End Using
        End Using
        Return js.Serialize(EmailList)
    End Function
    Private Function getOtherInfoForEmailTable(ByRef Email As Emails) As Emails
        Dim query As String = "select top 1  tblCompany.Company, tblPartOrder.FreightETA,tblCompany.Company as Company,tblPartOrder.ShipperTrack from tblOrder inner join tblCompany on tblCompany.CustomerNo=tblOrder.CustomerNo inner join tblPartOrder on tblPartOrder.OrderID=tblOrder.OrderID where tblOrder.OrderID=" & Email.OrderID
        '"select top 1 tblOrder.AutoOwner, tblPartOrder.FreightETA,tblCompany.Company as Company,tblPartOrder.ShipperTrack from tblOrder inner join tblPartOrder on tblOrder.OrderID = tblPartOrder.OrderID inner join tblCompany on tblPartOrder.Shipper = tblCompany.CompanyID where tblOrder.OrderID=" & Email.OrderID
        'Query Jumps from tblOrder to tblPartOrder? to tblCompany..gets the TOP 1 Row
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            conn.Open()
            Using comm As New SqlCommand(query, conn)
                Using reader As SqlDataReader = comm.ExecuteReader()
                    While reader.Read()
                        Email.CustomerName = If(reader.IsDBNull(0), String.Empty, reader.GetString(0))
                        Email.ETA = If(reader.IsDBNull(1), String.Empty, reader.GetDateTime(1).ToShortDateString())
                        Email.Shipper = If(reader.IsDBNull(2), String.Empty, reader.GetString(2))
                        Email.TrackingNo = If(reader.IsDBNull(3), String.Empty, reader.GetString(3))
                    End While
                End Using
            End Using
        End Using

        Return Email

    End Function

End Class