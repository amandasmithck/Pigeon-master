Imports Pigeon.Pigeon
Imports System.Data.SqlClient
Public Class CustomerManage1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Client") = "CK" Then
            dsCustomers.SelectCommand = "SELECT TOP (100) PERCENT CustomerNo, Company FROM dbo.tblCompany where type='Customer' union select '' as CustomerNo, ' Select Customer' ORDER BY Company"
        Else
            dsCustomers.SelectCommand = "SELECT TOP (100) PERCENT CustomerNo, Company FROM dbo.tblCompany union select '' as CustomerNo, ' Select Customer' ORDER BY Company"
        End If

        Me.dsCustomers.ConnectionString = GetClientConnectionString(Session("Client"))
        Me.dsMake.ConnectionString = GetClientConnectionString(Session("Client"))

    End Sub

    Private Sub CompanyImport()
        'get client name
        Try
            Dim client As String = Session("Client").ToString
            FileUpload1.SaveAs(Server.MapPath("newimport.csv"))


            Dim dt As DataTable = BuildDataTableNewCompany(Server.MapPath("newimport.csv"), ",")

            'Creating a new table for storing in database      
            Dim dtForDB As New DataTable()
            dtForDB.Columns.Add("Name")
            dtForDB.Columns.Add("Address")
            dtForDB.Columns.Add("City")
            dtForDB.Columns.Add("State")
            dtForDB.Columns.Add("Zip")
            dtForDB.Columns.Add("Phone")
            dtForDB.Columns.Add("SalesmanEmail")
            dtForDB.Columns.Add("CustomerNo")
            dtForDB.Columns.Add("Tier")
            dtForDB.Columns.Add("DesiredUsername")
            dtForDB.Columns.Add("DesiredPassword")
            dtForDB.Columns.Add("Email")
            Dim x As Integer = 0
            For Each dr As DataRow In dt.Rows

                If x = 0 Then
                Else
                    If dr("Name").ToString().Replace("'", "") <> "" Then
                        dtForDB.Rows.Add(dr("Name").ToString().Replace("'", ""), dr("Address").ToString().Replace("'", ""), dr("City").ToString().Replace("'", ""), dr("State").ToString().Replace("'", ""), dr("Zip").ToString().Replace("'", ""), dr("Phone").ToString().Replace("'", ""), dr("SalesmanEmail").ToString().Replace("'", ""), dr("CustomerNo").ToString().Replace("'", ""), dr("Tier").ToString().Replace("'", ""), dr("DesiredUsername").ToString().Replace("'", ""), dr("DesiredPassword").ToString().Replace("'", ""), dr("Email").ToString().Replace("'", ""))
                    End If
                End If
                x = x + 1
            Next


            'begin company creation

            For Each dr2 As DataRow In dtForDB.Rows
                Dim strcustno As String
                If CompanyExist(dr2("Name").ToString, dr2("Address").ToString, client) = True Then
                    If CompanyHasUsers(dr2("CustomerNo").ToString, client) = True Then
                        GoTo nextrecord
                    Else
                        strcustno = dr2("CustomerNo").ToString
                        GoTo retryusername
                    End If

                End If

                'see if customerno is good

                Dim RandomClass As New Random()
retrycustno:
                If dr2("CustomerNo").ToString <> "" And GoodCustomerNo(dr2("CustomerNo").ToString, client) = True Then
                    strcustno = dr2("CustomerNo").ToString
                Else
                    strcustno = RandomClass.Next(1000, 99999)
                End If
                If GoodCustomerNo(strcustno, client) = False Then GoTo retrycustno

                'Dim strinnetwork As Boolean
                'If dr2("InNetwork").ToString = "" Or UCase(dr2("InNetwork").ToString) = "NO" Or UCase(dr2("InNetwork").ToString) = "FALSE" Or dr2("InNetwork").ToString = "0" Then
                '    strinnetwork = False
                'Else
                '    strinnetwork = True
                'End If

                'insert customer
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim strSql As String
                    If client = "CK" Then
                        strSql = "insert into tblCompany(Company, CustomerNo, Address1, City, State, Zip, Phone, Email, type, active) values ('" & dr2("Name").ToString & "','" & strcustno & "','" & dr2("Address").ToString & "','" & dr2("City").ToString & "','" & dr2("State").ToString & "','" & dr2("Zip").ToString & "','" & dr2("Phone").ToString & "','" & dr2("Email").ToString & "', 'Customer', 1)"
                    Else
                        strSql = "insert into tblCompany(Company, CustomerNo, Address, City, State, Zip, Phone, SalesmanEmail, tierid) values ('" & dr2("Name").ToString & "','" & strcustno & "','" & dr2("Address").ToString & "','" & dr2("City").ToString & "','" & dr2("State").ToString & "','" & dr2("Zip").ToString & "','" & dr2("Phone").ToString & "','" & dr2("SalesmanEmail").ToString & "','" & dr2("Tier").ToString & "')"
                    End If
                    Dim sqlComm As New SqlCommand(strSql, conn)
                    conn.Open()
                    sqlComm.ExecuteNonQuery()
                End Using
                'add default pricing
                If client <> "CK" Then
                    Using conn As New SqlConnection(GetClientConnectionString(client))
                        Dim sqlComm2 As New SqlCommand("insert into tblOEMCompany(OEMID, CustomerNo, source, Markup) select oemid,'" & strcustno & "','List','-.250' from tblmake where OEMID is not null group by oemid", conn)
                        conn.Open()
                        sqlComm2.ExecuteNonQuery()
                    End Using
                End If
retryusername:
                'see if username is good
                Dim strusername As String
                Dim RandomClass2 As New Random()


                If dr2("DesiredUsername").ToString <> "" And GoodUserName(dr2("DesiredUsername").ToString, client) = True Then
                    strusername = dr2("DesiredUsername").ToString
                Else
                    strusername = Left(dr2("Name").ToString, 2) & RandomClass2.Next(1000, 99999)
                End If
                If GoodUserName(strcustno, client) = False Then GoTo retryusername



                'insert user
                Dim strpassword As String
                Dim RandomClass3 As New Random()
                If dr2("DesiredPassword").ToString <> "" And Len(dr2("DesiredPassword").ToString) >= 6 Then
                    strpassword = dr2("DesiredPassword").ToString
                Else
                    strpassword = RandomClass3.Next(100000, 999999)
                End If

                Dim stremail As String
                If dr2("Email").ToString <> "" Then
                    stremail = dr2("Email").ToString
                Else
                    stremail = "needemail@please.com"
                End If

                Dim user As MembershipUser = Membership.CreateUser(strusername, strpassword)
                user.Email = stremail
                Membership.UpdateUser(user)

                'add to role
                Roles.AddUserToRole(strusername, "Customer")
                'add customer no
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlComm3 As New SqlCommand("Update aspnet_Membership set CustomerNo = '" & strcustno & "', tierid = '" & dr2("Tier").ToString & "' from aspnet_Membership, aspnet_Users where aspnet_membership.userid = aspnet_users.userid and aspnet_Users.username = '" & strusername & "'", conn)

                    conn.Open()
                    sqlComm3.ExecuteNonQuery()
                End Using
nextrecord:
            Next

            Response.Redirect("CustomerManage.aspx")
        Catch Ex As Exception

        End Try
    End Sub

    Private Function BuildDataTableNewCompany(ByVal fileFullPath As String, ByVal seperator As Char) As DataTable
        Dim myTable As New DataTable("MyTable")
        Dim i As Integer = 0
        Dim myRow As DataRow = Nothing
        Dim fieldValues As String() = Nothing
        'string FileToRead = Server.MapPath(fileFullPath);      
        Dim myReader As System.IO.StreamReader = Nothing
        Try
            'Open file and read first line to determine how many fields there are.      
            myReader = System.IO.File.OpenText(fileFullPath)
            'string ReadContents = myReader.ReadToEnd();      
            fieldValues = myReader.ReadLine().Split(seperator)
            'Create data columns accordingly      
            'For i = 0 To fieldValues.Length - 1
            '    myTable.Columns.Add(New DataColumn(fieldValues(i)))
            'Next
            myTable.Columns.Add("Name")
            myTable.Columns.Add("Address")
            myTable.Columns.Add("City")
            myTable.Columns.Add("State")
            myTable.Columns.Add("Zip")
            myTable.Columns.Add("Phone")
            myTable.Columns.Add("SalesmanEmail")
            myTable.Columns.Add("CustomerNo")
            myTable.Columns.Add("Tier")
            myTable.Columns.Add("DesiredUsername")
            myTable.Columns.Add("DesiredPassword")
            myTable.Columns.Add("Email")

            'Adding the first line of data to data table      
            myRow = myTable.NewRow()
            For i = 0 To fieldValues.Length - 1
                myRow(i) = fieldValues(i).ToString()
            Next
            myTable.Rows.Add(myRow)
            'Now reading the rest of the data to data table      

            While myReader.Peek() <> -1
                fieldValues = myReader.ReadLine().Split(seperator)
                myRow = myTable.NewRow()
                For i = 0 To fieldValues.Length - 1
                    myRow(i) = fieldValues(i).ToString()
                Next
                myTable.Rows.Add(myRow)
            End While
        Finally
            myReader.Close()
        End Try

        Return myTable
    End Function

    Public Function GoodCustomerNo(ByVal custno As String, ByVal client As String)
        Dim custcount
        Using conn As New SqlConnection(GetClientConnectionString(client))
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

    Public Function CompanyExist(ByVal company As String, ByVal address As String, ByVal client As String)
        Dim custcount
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim strSql As String
            If client = "CK" Then
                strSql = "select count(companyid) from tblcompany where company = '" & company & "' and address1 = '" & address & "'"
            Else
                strSql = "select count(companyid) from tblcompany where company = '" & company & "' and address = '" & address & "'"
            End If
            Dim sqlComm As New SqlCommand(strSql, conn)
            conn.Open()
            custcount = sqlComm.ExecuteScalar()
        End Using

        If custcount = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CompanyHasUsers(ByVal custno As String, ByVal client As String)
        Dim custcount
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("select count(*) from aspnet_membership where customerno = '" & custno & "'", conn)
            conn.Open()
            custcount = sqlComm.ExecuteScalar()
        End Using

        If custcount = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function GoodUserName(ByVal username As String, ByVal client As String)
        Dim usercount
        Using conn As New SqlConnection(GetClientConnectionString(client))
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

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        CompanyImport()
    End Sub

End Class