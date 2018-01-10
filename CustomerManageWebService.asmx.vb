Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Xml
Imports Pigeon.Pigeon

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class CustomerManageWebService
    Inherits System.Web.Services.WebService
    Public Class Tiers
        Public Property TierID() As String
            Get
                Return m_TierID
            End Get
            Set(ByVal value As String)
                m_TierID = value
            End Set
        End Property
        Private m_TierID As String
        Public Property Tier() As String
            Get
                Return m_Tier
            End Get
            Set(ByVal value As String)
                m_Tier = value
            End Set
        End Property
        Private m_Tier As String
    End Class
    <WebMethod()>
    Public Function GetCustomers(ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)

        Dim strSql As String
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            If client.ToUpper() = "CK" Then
                strSql = "SELECT Company,Customerno from tblCompany where company is not null and customerno is not null and active =1 and type ='Customer' order by Company"
            Else
                strSql = "SELECT Company,Customerno from tblCompany where company is not null and customerno is not null order by Company"
            End If
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CustNo = r("Customerno")
                    c1.Tier = GetCustomerTier(c1.CustNo, client)
                    list.Add(c1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

    <WebMethod()>
    Public Function SearchCustomersByCustNo(ByVal CustNo As String, ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            If client = "CK" Then
                strSql = "SELECT Company,Customerno, Address1, City, State, Zip, Phone from tblCompany where company is not null and customerno = '" & CustNo & "' order by Company"
            Else
                strSql = "SELECT Company,Customerno, Address as Address1, City, State, Zip, Phone from tblCompany where company is not null and customerno = '" & CustNo & "' order by Company"
            End If
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CustNo = r("Customerno")
                    c1.Address = r("Address1").ToString
                    c1.City = r("City").ToString
                    c1.State = r("State").ToString
                    c1.Zip = r("Zip").ToString
                    c1.Phone = r("Phone").ToString
                    c1.Tier = GetCustomerTier(c1.CustNo, client)
                    list.Add(c1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function


    <WebMethod()>
    Public Function SearchCustomers(ByVal searchval As String, ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)


        Dim strSql As String
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            If client.ToUpper() = "CK" Then
                strSql = "SELECT Company,Customerno, Address1, City, State, Zip, Phone from tblCompany where company is not null and customerno is not null and active =1 and type ='Customer' and Company like '" & searchval & "%' order by Company"
            Else
                strSql = "SELECT Company,Customerno, Address as Address1, City, State, Zip, Phone from tblCompany where company is not null and customerno is not null and Company like '" & searchval & "%' order by Company"
            End If

            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CustNo = r("Customerno")
                    c1.Address = r("Address1").ToString
                    c1.City = r("City").ToString
                    c1.State = r("State").ToString
                    c1.Zip = r("Zip").ToString
                    c1.Phone = r("Phone").ToString
                    c1.Client = getClientConnectionStringByCKCustomerNo(c1.CustNo)
                    c1.Tier = GetCustomerTier(c1.CustNo, client)
                    list.Add(c1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

    <WebMethod()>
    Public Function GetAutonation()

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Customer)
        Dim strSql As String

        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            strSql = "SELECT Company, CompanyID FROM dbo.tblCompany WHERE (Type = 'Customer') AND (DealerGroup = N'Autonation') OR (CompanyID = 10341) ORDER BY Company"

            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim c1 As New Customer()
                    c1.Company = r("Company")
                    c1.CompanyID = r("CompanyID")
                    list.Add(c1)
                End While
            End Using
        End Using

        Return js.Serialize(list)

    End Function

    <WebMethod()>
    Public Function GetTiers(ByVal client As String)
        On Error Resume Next
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Tiers)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblTiers.TierID, dbo.tblTiers.Tier FROM dbo.tblTiers WHERE dbo.tblTiers.TierID > 2", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim u1 As New Tiers()
                    u1.TierID = r("TierID")
                    u1.Tier = r("Tier")



                    list.Add(u1)
                End While
            End Using
        End Using




        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function ChangeTier(ByVal userid As String, ByVal TierID As String, ByVal client As String)

        Dim bSuccess As Boolean = False
        Try
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("Update aspnet_Membership set TierID = " & TierID & "  where userid = '" & userid & "'", conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using

            bSuccess = True
        Catch
            'do nothnig

        End Try

        Return bSuccess
    End Function

    <WebMethod()>
    Public Function GetMarkupIndividual(ByVal custno As String, ByVal makegroups As ArrayList, ByVal client As String)
        On Error Resume Next
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Markup)

        Dim i As String
        For Each i In makegroups
            Dim m1 As New Markup
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm2 As New SqlCommand("SELECT top 1 source, markup from tbloemcompany inner join tblMake on tbloemcompany.OEMID = tblMake.OEMID WHERE customerno = '" & custno & "' and tblMake.makegroup = '" & i & "'", conn)
                conn.Open()
                Using r As SqlDataReader = sqlComm2.ExecuteReader()
                    While r.Read()
                        m1.MakeGroup = i
                        m1.Source = r("Source")
                        m1.Markup = FormatPercent(r("Markup"), 1)
                        m1.MarkupVal = FormatNumber(r("Markup") * 100, 2)
                    End While
                End Using
            End Using

            list.Add(m1)
        Next

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function GetMarkupAll(ByVal custno As String, ByVal client As String)
        On Error Resume Next
        Dim strmarkup As Decimal
        Dim strsource As String = String.Empty
        Dim js As New JavaScriptSerializer()

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT top 1 source, markup from tbloemcompany WHERE customerno = '" & custno & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()
                If r.Read() Then
                    strsource = r("Source")
                    strmarkup = r("Markup")
                End If
            End Using
        End Using

        Dim m1 As New Markup
        Dim list As New List(Of Markup)
        m1.Markup = FormatPercent(strmarkup, 1)
        m1.Source = strsource
        m1.MarkupVal = FormatNumber(strmarkup * 100, 2)
        list.Add(m1)

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function SaveMarkupGroup(ByVal custno As String, ByVal makegroup As String, ByVal source As String, ByVal markup As String, ByVal client As String)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("update tblOEMCompany set tblOEMCompany.Markup = '" & markup & "', tblOEMCompany.Source = '" & source & "' from tblOEMCompany, tblMake where tblMake.OEMID = tblOEMCompany.OEMID AND tblMake.MakeGroup = '" & makegroup & "' AND tblOEMCompany.CustomerNo = '" & custno & "'", conn)
            conn.Open()
            sqlComm2.ExecuteNonQuery()
        End Using
        Return True
    End Function

    <WebMethod()>
    Public Function SaveMarkupAll(ByVal custno As String, ByVal source As String, ByVal markup As String, ByVal client As String)

        Dim bSuccess As Boolean = False
        Try
            Dim strmarkup As Decimal = markup / 100
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm2 As New SqlCommand("update tbloemcompany set source = '" & source & "', markup = " & strmarkup & " WHERE customerno = '" & custno & "'", conn)
                conn.Open()
                sqlComm2.ExecuteNonQuery()
            End Using

            bSuccess = True
        Catch
            'Do nothing

        End Try

        Return bSuccess

    End Function

    <WebMethod()>
    Public Function GetUsers(ByVal custno As String, ByVal client As String)
        On Error Resume Next
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Users)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.aspnet_Users.UserId, dbo.aspnet_Users.UserName FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId WHERE (dbo.aspnet_Membership.CustomerNo = '" & custno & "') ORDER BY dbo.aspnet_Users.UserName", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim u1 As New Users()
                    u1.UserID = r("UserID")
                    u1.Username = r("UserName")

                    js.Serialize(u1)

                    list.Add(u1)
                End While
            End Using
        End Using

        Dim response = New ArrayList

        response.Add(list)
        Return js.Serialize(response)

    End Function

    <WebMethod()>
    Public Function GetUser(ByVal userid As String, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of UserInfo)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT userName,CustomerNo, TierID, Email, CASE WHEN isapproved = 1 THEN 'Yes' ELSE 'No' END AS Active, CASE WHEN CanOrder = 1 THEN 'Yes' ELSE 'No' END AS CanOrder FROM dbo.aspnet_Membership as M inner join dbo.aspnet_Users as U on U.userID=M.userID WHERE (M.UserId = '" & userid & "')", conn)
            conn.Open()

            Using r As SqlDataReader = sqlComm.ExecuteReader()

                If r.Read() Then
                    Dim u1 As New UserInfo()
                    Try
                        Dim bIsOnline As Boolean = False
                        Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
                        Dim u As MembershipUser = provider.GetUser(New Guid(userid), bIsOnline)

                        u1.TierID = r("TierID")
                        u1.Password = u.GetPassword()
                        u1.Email = r("Email")
                        u1.Active = r("Active")
                        u1.CustomerNo = r("CustomerNo")
                        u1.CanOrder = r("CanOrder")
                        js.Serialize(u1)

                    Catch ex As Exception
                        u1.Err = ex.Message & "-" & ex.StackTrace
                    End Try
                    list.Add(u1)
                End If
            End Using
        End Using

        Return js.Serialize(list)

    End Function
    <WebMethod()>
    Public Function GetUserID(ByVal username As String, ByVal client As String)

        Dim js As New JavaScriptSerializer()
        Dim list As New List(Of Users)

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))

            Dim sqlComm As New SqlCommand("SELECT UserId FROM dbo.aspnet_Users WHERE (UserName = '" & username & "')", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()
                    Dim u1 As New Users()
                    u1.UserID = r("UserId")
                    js.Serialize(u1)
                    list.Add(u1)
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()>
    Public Function ChangePassword(ByVal userid As String, ByVal password As String, ByVal client As String)

        Dim bSuccess As Boolean = False

        Try
            Dim bIsOnline As Boolean = False
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
            Dim u As MembershipUser = provider.GetUser(New Guid(userid), bIsOnline)

            u.ChangePassword(u.GetPassword, password)
            provider.UpdateUser(u)

            bSuccess = True

        Catch
            'Do Nothing

        End Try

        Return bSuccess

    End Function

    <WebMethod()>
    Public Function ChangeEmail(ByVal userid As String, ByVal email As String, ByVal client As String)

        Dim bSuccess As Boolean = False

        Try
            Dim bIsOnline As Boolean = False
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
            Dim u As MembershipUser = provider.GetUser(New Guid(userid), bIsOnline)

            u.Email = email
            provider.UpdateUser(u)

            bSuccess = True

        Catch
            'Do Nothing

        End Try

        Return bSuccess

    End Function

    <WebMethod()>
    Public Function ChangeActive(ByVal userid As String, ByVal active As Boolean, ByVal client As String)

        Dim bSuccess As Boolean = False

        Try
            Dim bIsOnline As Boolean = False
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
            Dim u As MembershipUser = provider.GetUser(New Guid(userid), bIsOnline)

            u.IsApproved = active
            provider.UpdateUser(u)

            bSuccess = True

        Catch
            'Do Nothing

        End Try

        Return bSuccess

    End Function
    <WebMethod()>
    Public Function ChangeCanOrder(ByVal userid As String, ByVal canorder As Boolean, ByVal client As String)

        On Error GoTo errorz
        Dim strcanorder
        If canorder = True Then
            strcanorder = 1
        Else
            strcanorder = 0
        End If
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Update aspnet_Membership set canorder = " & strcanorder & "  where userid = '" & userid & "'", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        Return True
        Exit Function
errorz:
        Return False
    End Function

    <WebMethod()>
    Public Function ChangeUserComp(ByVal userid As String, ByVal companyID As String, ByVal client As String)

        On Error GoTo errorz

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("Update dbo.aspnet_Membership set CustomerNo = '" & companyID & "'  where userid = '" & userid & "'", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using

        Return True
        Exit Function
errorz:
        Return False
    End Function

    <WebMethod()>
    Public Function GoodCustomerNo(ByVal custno As String, ByVal client As String)
        Dim custcount
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("select count(companyid) from tblcompany where customerno = '" & custno & "'", conn)
            conn.Open()
            custcount = sqlComm.ExecuteScalar()
        End Using

        If custcount = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    <WebMethod()>
    Public Function SaveNewCustomer(ByVal company As String, ByVal custno As String, ByVal address As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal phone As String, ByVal salesman As String, ByVal customertype As String, ByVal companymakes As ArrayList, ByVal client As String)
        'On Error GoTo errorz
        company.Replace("'", "''")
        'insert customer
        Dim strSql As String
        If client = "CK" Then
            strSql = "insert into tblCompany(Company, CustomerNo, Address1, City, State, Zip, Phone, Email, type, active) values ('" & company & "','" & custno & "','" & address & "','" & city & "','" & state & "','" & zip & "','" & phone & "','" & salesman & "', 'Customer', 1); Select scope_identity()"
        Else
            strSql = "insert into tblCompany(Company, CustomerNo, Address, City, State, Zip, Phone, SalesmanEmail, CustomerType, TierID) values ('" & company & "','" & custno & "','" & address & "','" & city & "','" & state & "','" & zip & "','" & phone & "','" & salesman & "', '" & customertype & "', '3'); Select scope_identity()"
        End If

        Dim intCompanyID As Long
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            intCompanyID = sqlComm.ExecuteScalar()
        End Using

        'update parentcompanyid
        If client = "CK" Then
            strSql = "update tblCompany set parentcompanyid=" & intCompanyID & " where companyid=" & intCompanyID
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand(strSql, conn)
                conn.Open()
                sqlComm.ExecuteNonQuery()
            End Using
        End If


        'add default pricing
        If client <> "CK" Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm2 As New SqlCommand("insert into tblOEMCompany(OEMID, CustomerNo, source, Markup) select oemid,'" & custno & "','List','-.250' from tblmake where OEMID is not null group by oemid", conn)
                conn.Open()
                sqlComm2.ExecuteNonQuery()
            End Using
        End If

        If companymakes IsNot Nothing Then
            Dim i As String
            For Each i In companymakes
                Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                    Dim sqlComm3 As New SqlCommand("insert into tblCompanyMake(CustomerNo, Make) values ('" & custno & "', '" & i & "')", conn)
                    conn.Open()
                    sqlComm3.ExecuteNonQuery()
                End Using
            Next
        End If
        Return True
        Exit Function
errorz:
        Return False
    End Function

    <WebMethod()>
    Public Function DeleteCustomer(ByVal companyID As String, ByVal client As String)

        Dim bSuccess As Boolean = False
        Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)

        Try

            'delete all users associated with that company
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim command As New SqlCommand("SELECT dbo.aspnet_Users.UserName FROM dbo.aspnet_Users INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Users.UserId = dbo.aspnet_Membership.UserId WHERE (dbo.aspnet_Membership.CustomerNo = '" & companyID & "') ORDER BY dbo.aspnet_Users.UserName", conn)
                conn.Open()
                Using r As SqlDataReader = command.ExecuteReader()
                    While r.Read()
                        provider.DeleteUser(r("UserName"), True)
                    End While
                End Using

                command = New SqlCommand(String.Format("DELETE FROM tblCompany WHERE CustomerNo ='{0}'", companyID), conn)
                command.ExecuteNonQuery()

                command = New SqlCommand(String.Format("DELETE FROM tblCompanyMake WHERE CustomerNo ='{0}'", companyID), conn)
                command.ExecuteNonQuery()

                command = New SqlCommand(String.Format("DELETE FROM tblOEMCompany WHERE CustomerNo ='{0}'", companyID), conn)
                command.ExecuteNonQuery()
            End Using

            bSuccess = True
        Catch
            'Do Nothing

        End Try

        Return bSuccess
    End Function

    <WebMethod()>
    Public Function GoodUserName(ByVal username As String, ByVal client As String)
        Dim usercount
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("select count(userid) from aspnet_users where username = '" & username & "'", conn)
            conn.Open()
            usercount = sqlComm.ExecuteScalar()
        End Using

        If usercount = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    <WebMethod()>
    Public Function SaveNewUser(ByVal username As String, ByVal custno As String, ByVal email As String, ByVal password As String, ByVal tierid As String, ByVal client As String)

        Try
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)

            Dim status As New MembershipCreateStatus
            Dim userId As System.Guid = Guid.NewGuid()

            Dim user As MembershipUser = provider.CreateUser(username, password, email, Nothing, Nothing, True, userId, status)

            If status <> MembershipCreateStatus.Success Then Return False

            Dim roleProvider As SqlRoleProvider = PidgeonMembership.GetSpecificRoleProvider(client)
            Dim users As String() = {username}
            Dim roles As String() = {"Customer"}

            roleProvider.AddUsersToRoles(users, roles)

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                conn.Open()

                'add customer no and tierid
                Dim command As New SqlCommand(String.Format("Update aspnet_Membership set CustomerNo = '{0}', tierid = '{1}' from aspnet_Membership where userid = '{2}'", custno, tierid, userId), conn)
                command.ExecuteNonQuery()

            End Using

            'EmailCKNewUser(username, password, email, custno, client)

            Return True

        Catch ex As Exception
            Dim js As New JavaScriptSerializer
            Return js.Serialize(ex.Message & "-" & ex.StackTrace)
        End Try
    End Function

    <WebMethod()>
    Public Function SaveNewAdminUser(ByVal username As String, ByVal email As String, ByVal password As String, ByVal tierid As String, ByVal client As String)
        Dim bSuccess As Boolean = False

        Try

            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)

            Dim status As New MembershipCreateStatus
            Dim userId As System.Guid = Guid.NewGuid()

            Dim user As MembershipUser = provider.CreateUser(username, password, email, Nothing, Nothing, True, userId, status)

            If status <> MembershipCreateStatus.Success Then Return False

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand(String.Format("update aspnet_membership set tierid=2 where userid='{0}'", userId), conn)
                conn.Open()
                sqlComm.ExecuteScalar()
            End Using

            Dim roleProvider As SqlRoleProvider = PidgeonMembership.GetSpecificRoleProvider(client)
            Dim users As String() = {username}
            Dim roles As String() = {"Admin"}
            roleProvider.AddUsersToRoles(users, roles)

            bSuccess = True

        Catch

        End Try

        Return bSuccess

    End Function

    <WebMethod()>
    Public Function updateAdmin(ByVal username As String, ByVal email As String, ByVal password As String, ByVal client As String)

        Dim bSuccess As Boolean = False
        Try
            Dim bIsOnline As Boolean = False
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
            Dim userInfo As MembershipUser = provider.GetUser(username, bIsOnline)

            userInfo.Email = email
            provider.UpdateUser(userInfo)

            If password <> "**********" Then
                provider.ChangePassword(username, userInfo.GetPassword, password)
            End If

            bSuccess = True

        Catch 'Do nothing

        End Try

        Return bSuccess

    End Function

    <WebMethod()>
    Public Function getAdminUsersInfo(ByVal Username As String, ByVal client As String)

        Dim js As New JavaScriptSerializer()

        Try
            Dim bIsOnline As Boolean = False
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
            Dim userInfo As MembershipUser = provider.GetUser(Username, bIsOnline)
            Dim Info As New AdminInfo()
            Info.Email = userInfo.Email

            Return js.Serialize(Info)

        Catch
            'Do nothing
        End Try

        Return js.Serialize(False)

    End Function

    <WebMethod()>
    Public Function getAllAdmins(ByVal client As String)

        Try
            Dim js As New JavaScriptSerializer()
            Dim InfoList As New List(Of AdminInfo)

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm As New SqlCommand("DECLARE	@return_value int,@UserId uniqueidentifier EXEC [dbo].[aspnet_UsersInRoles_GetUsersInRoles] @ApplicationName = N'/',@RoleName=N'Admin'", conn)

                conn.Open()
                Using r As SqlDataReader = sqlComm.ExecuteReader()
                    While r.Read()
                        Dim Info As New AdminInfo
                        Info.UserName = r("UserName").ToString()
                        InfoList.Add(Info)
                    End While
                End Using
            End Using

            Return js.Serialize(InfoList)

        Catch Ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()>
    Public Function DeleteUser(ByVal Username As String, ByVal client As String)

        Dim success As Boolean = False

        Try
            Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)
            provider.DeleteUser(Username, True)

            success = True
        Catch
            'Do Nothing

        End Try

        Return success

    End Function

    <WebMethod()>
    Public Function GetCustomerInfo(ByVal custno As String, ByVal client As String)
        On Error Resume Next
        Dim js As New JavaScriptSerializer()
        Dim c As New Customer
        'Dim list As New List(Of Customer)
        GetDefaults(client)
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("SELECT * from tblcompany WHERE customerno = '" & custno & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()
                While r.Read()
                    c.CompanyID = r("CompanyID")
                    c.Company = r("Company")
                    c.City = r("City")
                    c.State = r("State")
                    c.Zip = r("Zip")
                    c.Phone = r("Phone")
                    c.CustNo = custno
                    If client = "CK" Then
                        c.Address = r("Address1")
                    Else
                        c.Address = r("Address")
                        If (IsDBNull(r("CustomerType"))) Then
                            c.CustomerType = ""
                        Else
                            c.CustomerType = r("CustomerType")
                        End If
                        If (IsDBNull(r("SalesmanEmail"))) Then
                            c.SalesmanEmail = clientemails
                        Else
                            c.SalesmanEmail = r("SalesmanEmail")
                        End If
                        c.Autonation = r("Autonation")
                    End If



                End While
            End Using
        End Using

        Dim Makes As New ArrayList()
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm3 As New SqlCommand("SELECT * from tblcompanymake WHERE customerno = '" & custno & "'", conn)
            conn.Open()
            Using r2 As SqlDataReader = sqlComm3.ExecuteReader()
                While r2.Read()
                    Makes.Add(r2("Make"))
                End While
                r2.Close()
            End Using
        End Using
        c.CompanyMakes = Makes

        'list.Add(c)
        Return js.Serialize(c)
    End Function

    <WebMethod()>
    Public Function UpdateCustomerInfo(ByVal CompanyID As String, ByVal CustomerNo As String, ByVal Company As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal Phone As String, ByVal SalesmanEmail As String, ByVal CustomerType As String, ByVal CompanyMakes As ArrayList, ByVal client As String)
        Dim strold As String

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("select customerno from tblcompany where companyid = '" & CompanyID & "'", conn)
            conn.Open()
            strold = sqlComm.ExecuteScalar()
        End Using

        Dim strUpdate As String

        If client = "CK" Then
            strUpdate = "update tblcompany set Company = '" & Company & "', CustomerNo = '" & CustomerNo & "', Address1 = '" & Address & "', City = '" & City & "', State = '" & State & "', Zip = '" & Zip & "', Phone = '" & Phone & "' where CompanyID = '" & CompanyID & "'"
        Else
            strUpdate = "update tblcompany set Company = '" & Company & "', CustomerNo = '" & CustomerNo & "', Address = '" & Address & "', City = '" & City & "', State = '" & State & "', Zip = '" & Zip & "', Phone = '" & Phone & "', SalesmanEmail = '" & SalesmanEmail & "', CustomerType= '" & CustomerType & "' where CompanyID = '" & CompanyID & "'"
        End If
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand(strUpdate, conn)
            conn.Open()
            sqlComm2.ExecuteNonQuery()
        End Using
        If CustomerNo <> strold Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm3 As New SqlCommand("update tbloemcompany set customerno = '" & CustomerNo & "' where customerno = '" & strold & "'", conn)
                conn.Open()
                sqlComm3.ExecuteNonQuery()
            End Using

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm4 As New SqlCommand("update tbloemquotes set customerno = '" & CustomerNo & "' where customerno = '" & strold & "'", conn)
                conn.Open()
                sqlComm4.ExecuteNonQuery()
            End Using


            If client = "CK" Then
                strUpdate = "update tblorder set customerno = '" & CustomerNo & "' where customerno = '" & strold & "'"
            Else
                strUpdate = "update tblorders set customerno = '" & CustomerNo & "' where customerno = '" & strold & "'"
            End If
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm5 As New SqlCommand(strUpdate, conn)
                conn.Open()
                sqlComm5.ExecuteNonQuery()
            End Using

            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm6 As New SqlCommand("update aspnet_membership set customerno = '" & CustomerNo & "' where customerno = '" & strold & "'", conn)
                conn.Open()
                sqlComm6.ExecuteNonQuery()
            End Using
        End If
        'company makes
        If client <> "CK" Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                Dim sqlComm7 As New SqlCommand("DELETE FROM tblCompanyMake WHERE CustomerNo = '" & CustomerNo & "'", conn)
                conn.Open()
                sqlComm7.ExecuteNonQuery()
            End Using

            If CompanyMakes Is Nothing Then
            Else

                Dim i As String
                For Each i In CompanyMakes
                    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
                        Dim sqlComm8 As New SqlCommand("insert into tblCompanyMake(CustomerNo, Make) values ('" & CustomerNo & "', '" & i & "')", conn)
                        conn.Open()
                        sqlComm8.ExecuteNonQuery()
                    End Using
                Next
            End If
        End If
        Return True
    End Function


    Public Class Contact
        Public Property Position() As String
            Get
                Return m_Position
            End Get
            Set(ByVal value As String)
                m_Position = value
            End Set
        End Property
        Private m_Position As String
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String
        Public Property Phone() As String
            Get
                Return m_Phone
            End Get
            Set(ByVal value As String)
                m_Phone = value
            End Set
        End Property
        Private m_Phone As String
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(ByVal value As String)
                m_Email = value
            End Set
        End Property
        Private m_Email As String
    End Class

    <WebMethod()>
    Public Function GetContacts(ByVal Client As String)
        Dim js As New JavaScriptSerializer()
        Dim contacts As New ArrayList

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(Client))
            Dim sqlComm As New SqlCommand("SELECT * FROM tblContacts", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim c As New Contact
                    c.Position = r("Position")
                    c.Name = r("Name")
                    c.Phone = r("Phone")
                    c.Email = r("Email")

                    contacts.Add(c)
                End While
            End Using
        End Using

        Return js.Serialize(contacts)
    End Function

    'Function EmailCKNewUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal custno As String, ByVal client As String)
    '    Dim strcompany As String
    '    Dim strphone As String
    '    Dim straddress As String
    '    Dim strcity As String
    '    Dim strstate As String
    '    Dim strzip As String
    '    GetDefaults(client)
    '    'first get company name

    '    Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
    '        Dim sqlComm As New SqlCommand("SELECT company, phone, address, city, state, zip from tblcompany where customerno = '" & custno & "'", conn)
    '        conn.Open()
    '        Using r As SqlDataReader = sqlComm.ExecuteReader()

    '            While r.Read()
    '                strcompany = r("company")
    '                strphone = r("phone")
    '                straddress = r("address")
    '                strcity = r("city")
    '                strstate = r("state")
    '                strzip = r("zip")

    '            End While

    '        End Using
    '    End Using

    '    Dim mm As New System.Net.Mail.MailMessage(clientckorderemail, clientckorderemail)
    '    mm.Subject = clientwebsitename & " New User"
    '    Dim strbody As String

    '    strbody = "Please create a user in SMTC for this company<br/><br/>"
    '    strbody = strbody & "Company:" & strcompany & "<br/>"
    '    strbody = strbody & "Phone:" & strphone & "<br/>"
    '    strbody = strbody & "Address:" & straddress & "<br/>"
    '    strbody = strbody & "City:" & strcity & "<br/>"
    '    strbody = strbody & "State:" & strstate & "<br/>"
    '    strbody = strbody & "Zip:" & strzip & "<br/>"
    '    strbody = strbody & "Username:" & username & "<br/>"
    '    strbody = strbody & "Password:" & password & "<br/>"
    '    strbody = strbody & "Email:" & email & "<br/><br/>"

    '    mm.Body = strbody
    '    mm.IsBodyHtml = True

    '    Try
    '        Dim smtp As New System.Net.Mail.SmtpClient
    '        smtp.Host = "smtp.emailsrvr.com"
    '        smtp.Timeout = 500000
    '        smtp.Send(mm)
    '        Return True
    '    Catch Ex As Exception
    '        Console.WriteLine(ex)
    '        Return False
    '    End Try

    'End Function

    <WebMethod()>
    Public Function NewCustEmail(ByVal company As String, ByVal address As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal phone As String, ByVal contact As String, ByVal email As String, ByVal salesman As String, ByVal username As String, ByVal password As String, ByVal client As String)
        GetDefaults(client)

        If client = "CK" Then GoTo Email
        Dim RandomClass As New Random()

        Dim strtempcustno As Integer
gettempcustno:
        strtempcustno = RandomClass.Next(1000, 99999)

        'make sure custno not in use
        Dim custcount As Integer
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm4 As New SqlCommand("select count(*) from tblcompany where customerno = '" & strtempcustno & "'", conn)
            conn.Open()
            custcount = sqlComm4.ExecuteScalar()
        End Using

        If custcount > 0 Then GoTo gettempcustno



        'insert customer
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm As New SqlCommand("insert into tblCompany(Company, CustomerNo, Address, City, State, Zip, Phone) values ('" & company & "','" & strtempcustno & "','" & address & "','" & city & "','" & state & "','" & zip & "','" & phone & "')", conn)
            conn.Open()
            sqlComm.ExecuteNonQuery()
        End Using
        'add default pricing
        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm2 As New SqlCommand("insert into tblOEMCompany(OEMID, CustomerNo, source, Markup) select oemid,'" & strtempcustno & "','List','-.250' from tblmake where OEMID is not null group by oemid", conn)
            conn.Open()
            sqlComm2.ExecuteNonQuery()
        End Using

        'create user as inactive
        Dim provider As SqlMembershipProvider = PidgeonMembership.GetSpecificMembershipProvider(client)

        Dim status As New MembershipCreateStatus
        Dim userId As System.Guid = Guid.NewGuid()

        Dim user As MembershipUser = provider.CreateUser(username, password, email, Nothing, Nothing, True, userId, status)

        If status <> MembershipCreateStatus.Success Then Return False

        Dim roleProvider As SqlRoleProvider = PidgeonMembership.GetSpecificRoleProvider(client)
        Dim users As String() = {username}
        Dim roles As String() = {"Customer"}

        roleProvider.AddUsersToRoles(users, roles)

        'add customer no
        Dim intTierid As Long
        Dim intActive As Long
        Select Case client
            Case "Autoway"
                intTierid = 8
                intActive = 0
            Case "GO"
                intTierid = 8
                intActive = 1
            Case "Tracy"
                intTierid = 4
                intActive = 0
        End Select

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sqlComm3 As New SqlCommand("Update aspnet_Membership set CustomerNo = '" & strtempcustno & "', tierid = '" & intTierid & "', isapproved = " & intActive & " from aspnet_Membership, aspnet_Users where aspnet_membership.userid = aspnet_users.userid and aspnet_Users.username = '" & username & "'", conn)

            conn.Open()
            sqlComm3.ExecuteNonQuery()
        End Using

Email:
        'email client


        Dim mm As New System.Net.Mail.MailMessage(clientnewcustomerfromemail, clientnewcustomertoemail)
        If client = "CK" Then
            mm.Subject = clientwebsitename & " Potential New Customer-Please contact them"
        Else
            mm.Subject = clientwebsitename & " New Customer Signup"
        End If

        mm.Body = "Company: " & company & "<br/>" &
            "Address: " & address & "<br/>" &
            "City: " & city & "<br/>" &
            "State: " & state & "<br/>" &
            "Zip: " & zip & "<br/>" &
            "Phone: " & phone & "<br/>" &
            "Contact: " & contact & "<br/>" &
            "Email: " & email & "<br/>" &
            "Username: " & username & "<br/>" &
            "Password: " & password & "<br/>" &
            "Preferred Salesman: " & salesman & "<br/>"

        mm.IsBodyHtml = True

        If clientnewcustomerccemail <> "" And clientnewcustomerccemail <> Nothing And IsDBNull(clientnewcustomerccemail) = False Then
            Dim emailcc() As String
            emailcc = clientnewcustomerccemail.Split(",")
            For x = 0 To emailcc.Count - 1
                mm.CC.Add(emailcc(x))
            Next
        End If
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


    <WebMethod()>
    Public Function GenerateEmail(ByVal CustomerType As String, ByVal CompanyMakes As ArrayList, ByVal client As String)
        Dim js As New JavaScriptSerializer()
        Dim emails As New ArrayList

        Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(client))
            Dim sql = "SELECT tblcompany.customerno, max(loweredemail) as loweredemail FROM aspnet_Membership Left Outer Join tblcompany on tblcompany.CustomerNo = aspnet_Membership.CustomerNo Left Outer Join tblCompanyMake on tblCompany.CustomerNo = tblCompanyMake.CustomerNo WHERE tblcompany.CustomerType = '" & CustomerType & "'"

            If (CompanyMakes IsNot Nothing) Then
                If CompanyMakes.Count > 0 Then
                    sql = sql & " AND ("
                    Dim i = 1
                    For Each Make In CompanyMakes
                        sql = sql & " tblCompanyMake.Make = '" & Make & "'"
                        If (i <> CompanyMakes.Count) Then
                            sql = sql & " OR"
                        End If
                        i = i + 1
                    Next
                    sql = sql & ")"
                End If
            End If

            sql = sql & " group by tblcompany.customerno"

            Dim sqlComm2 As New SqlCommand(sql, conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm2.ExecuteReader()
                While r.Read()
                    emails.Add(r("loweredemail"))
                End While
            End Using
        End Using

        Return js.Serialize(emails)
    End Function
End Class