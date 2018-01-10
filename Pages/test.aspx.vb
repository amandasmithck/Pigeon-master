Imports Pigeon.CertifiedLookup
Imports Pigeon.Pigeon
Imports System.Data.SqlClient

Public Class test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'inventroy
        'Dim result As DataSet
        'Try
        '    Dim inventorySearch As CertifiedLookup.Lookup = New CertifiedLookup.Lookup()
        '    result = inventorySearch.GetInventoryCTR("B4RY=O=DXHVNEU=8ZDKMJD5YK1TU65ZEA=UP8DW5KM2O0==C4L", "T2088AA")

        'Catch Ex As Exception

        'End Try

        'For Each row As DataRow In result.Tables(0).Rows
        '    Debug.WriteLine(row("uLocation"))
        '    Debug.WriteLine(row("uInventory"))
        'Next row

        'installation kit
        'Dim result As DataSet
        'Try
        '    Dim inventorySearch As CertifiedLookup.Lookup = New CertifiedLookup.Lookup()
        '    result = inventorySearch.GetInstallationKit("B4RY=O=DXHVNEU=8ZDKMJD5YK1TU65ZEA=UP8DW5KM2O0==C4L", "T2088AA")

        'Catch Ex As Exception

        'End Try

        'For Each row As DataRow In result.Tables(0).Rows
        '    Debug.WriteLine(row("uQuantity"))
        '    Debug.WriteLine(row("uPart"))
        '    Debug.WriteLine(row("uDescription"))
        '    Debug.WriteLine(row("uTotalPrice"))
        'Next row




        'Dim UserIPAddress
        'UserIPAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        'If UserIPAddress = "" Then
        '    UserIPAddress = Request.ServerVariables("REMOTE_ADDR")
        'End If


        'Response.Write(UserIPAddress)



        '  SendTracyOrder(Nothing, Nothing, "1998", "Honda", "Accord", "123", "TESTVIN", "123", "456")




        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("select * from tblCertifiedCatalog where vinmatch is null and id >=583 order by id", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim strYear = r("Year").ToString
                    Dim strMake = r("Make").ToString
                    Dim strModel = r("Model").ToString
                    Dim strenginecell = r("Engine").ToString.Split("/")
                    Dim strLiters = Trim(strenginecell(0))
                    Dim strCylinders = Trim(strenginecell(1))
                    Dim strVinCode = Left(Trim(strenginecell(2)), 1)


                    Dim strID = r("ID").ToString
                    Debug.WriteLine(strID)
                    'rules
                    strModel = ApplyModelRules(strModel)
                    strLiters = ApplyLiterRules(strLiters)
                    Dim strVinPlacement = ApplyVinPlacementRules(Trim(strenginecell(2)))

                    'find one matching vin pattern
                    Dim strVin, strsql
                    Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                        If Len(CStr(strVinPlacement)) = 1 Then
                            strsql = "select top 1 vin_pattern from VehicleData.dbo.vin_pattern where year ='" & strYear & "' and make = '" & strMake & "' and model = '" & strModel & "' and def_engine_cylinders = '" & strCylinders & "' and def_engine_size = '" & strLiters & "' and substring(vin_pattern," & strVinPlacement & ",1) = '" & strVinCode & "'"
                        ElseIf Len(CStr(strVinPlacement)) = 2 Then
                            strsql = "select top 1 vin_pattern from VehicleData.dbo.vin_pattern where year ='" & strYear & "' and make = '" & strMake & "' and model = '" & strModel & "' and def_engine_cylinders = '" & strCylinders & "' and def_engine_size = '" & strLiters & "' and substring(vin_pattern," & 6 & ",1) = '" & Left(CStr(strVinPlacement), 1) & "' and substring(vin_pattern," & 7 & ",1) = '" & Right(CStr(strVinPlacement), 1) & "'"
                        End If
                        Dim sqlcomm2 As New SqlCommand(strsql, conn2)
                        conn2.Open()
                        strVin = sqlcomm2.ExecuteScalar
                    End Using

                    If strVin <> Nothing Then
                        If strVin <> "" And strVin <> " " And IsDBNull(strVin) = False Then
                            Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                                Dim sqlcomm2 As New SqlCommand("update tblCertifiedCatalog set vinmatch = '" & strVin & "' where id = " & strID, conn2)
                                conn2.Open()
                                sqlcomm2.ExecuteNonQuery()
                            End Using
                        End If
                    End If

                End While
            End Using
        End Using

    End Sub

    Public Function ApplyModelRules(ByVal model As String) As String

        'Dim splitmodel = model.Split(" ")
        'Dim finalmodel = splitmodel(0)

        Dim finalmodel = model
        finalmodel.Replace("AWD", "")
        finalmodel.Replace("2WD", "")
        finalmodel.Replace("4WD", "")
        finalmodel.Replace("RWD", "")
        finalmodel.Replace("FWD", "")
        finalmodel.Replace("BASe", "")
        finalmodel.Replace("GTS", "")
        finalmodel.Replace("Police", "")
        finalmodel.Replace("VISTA", "")

        finalmodel = finalmodel.Replace("4-Runner", "4Runner")
        finalmodel = finalmodel.Replace("XL-7", "XL7")
        If finalmodel.Contains("SC1") Or finalmodel.Contains("SC2") Or finalmodel.Contains("SL1") Or finalmodel.Contains("SL2") Or finalmodel.Contains("SW1") Or finalmodel.Contains("SW2") Then
            finalmodel.Replace("SC1", "S-series")
            finalmodel.Replace("SC2", "S-series")
            finalmodel.Replace("SL1", "S-series")
            finalmodel.Replace("SL2", "S-series")
            finalmodel.Replace("SW1", "S-series")
            finalmodel.Replace("SW2", "S-series")
        End If
        If finalmodel.Contains("L Series") Then
            finalmodel.Replace("L Series", "L-Series")
        End If
        finalmodel.Replace("trans am", "")
        finalmodel.Replace("Lemans", "Le Mans")
        finalmodel.Replace("88", "Eighty-Eight")
        finalmodel.Replace("98", "Ninety-Eight")
        finalmodel.Replace("F150", "F-150")
        finalmodel.Replace("F250", "F-250")
        finalmodel.Replace("F350", "F-350")
        finalmodel.Replace("F450", "F-450")
        finalmodel.Replace("F550", "F-550")
        finalmodel.Replace("E150", "E-150")
        finalmodel.Replace("E250", "E-250")
        finalmodel.Replace("E350", "E-350")
        finalmodel.Replace("E450", "E-450")
        finalmodel.Replace("E550", "E-550")
        finalmodel.Replace("Beetle", "New Beetle")



        Return Trim(finalmodel)
    End Function

    Public Function ApplyLiterRules(ByVal liters As String) As String
        Dim splitliters = liters.Split(" ")
        If IsNumeric(splitliters(0)) Then
            Return splitliters(0)
        Else
            Return 0
        End If
    End Function
    Public Function ApplyVinPlacementRules(ByVal location As String) As Integer
        Dim intPlacement As Integer
        intPlacement = 8

        If Len(location) > 2 Then
            If IsNumeric(location.Substring(2, 1)) Then intPlacement = location.Substring(2, 1)
        ElseIf Len(location) = 2 Then
            If IsNumeric(Trim(location)) Then intPlacement = Trim(location)
        End If

        Return intPlacement
    End Function
End Class