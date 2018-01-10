Imports Pigeon.Pigeon
Imports Pigeon.TransmissionWebService
Imports System.Data.SqlClient

Public Class temp

    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''************************code block to get transmission pricing to send to trademotion********************
        'loop through each part and update core, list and dealer
        Using conn As New SqlConnection(ConnectionStrings.CKConnectionString)
            Dim sqlComm As New SqlCommand("SELECT tmpTradeMotionPricing.part from tmpTradeMotionPricing inner join tblcertifiedpricing on tmpTradeMotionPricing.part=tblcertifiedpricing.part group by tmpTradeMotionPricing.part", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim decCore, decList, decDealer As Decimal
                    Dim strWarningHeader, strWarning As String
                    'core
                    Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlComm2 As New SqlCommand("select core from tblcertifiedpricing where part = '" & r("part") & "'", conn2)
                        conn2.Open()
                        decCore = sqlComm2.ExecuteScalar
                    End Using
                    'tiering
                    Dim p1 As New Pricing
                    'p1 = GetTransmissionPrice(r("part"), "quirkadmin", "quirk")
                    p1 = GetTransmissionPrice(r("part"), "quirkadmin", "quirk", 2) '????
                    'list and 'dealer
                    For Each searchedtier As VisibleTier In p1.tiers
                        If searchedtier.TierID = 1 Then
                            decList = searchedtier.Price
                        End If
                        If searchedtier.TierID = 2 Then
                            decDealer = searchedtier.Price + 100
                        End If
                    Next

                    'warnings
                    'get any warnings
                    strWarningHeader = ""
                    strWarning = ""
                    Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlCommWarning As New SqlCommand("select header,cast(content as nvarchar(999)) as content from tbltransmissionwarnings where part = '" & r("Part") & "' and right(part,1) <> '%' UNION select header,cast(content as nvarchar(999)) as content from tbltransmissionwarnings where left(part,5) = left('" & r("Part") & "',5) and right(part,1) = '%'", conn2)
                        conn2.Open()
                        Using rWarning As SqlDataReader = sqlCommWarning.ExecuteReader()
                            While rWarning.Read()
                                strWarningHeader = rWarning("header").ToString
                                strWarning = rWarning("content").ToString
                            End While
                        End Using
                    End Using
                    'update
                    Using conn2 As New SqlConnection(ConnectionStrings.CKConnectionString)
                        Dim sqlComm2 As New SqlCommand("update tmpTradeMotionPricing set list=" & decList & ", core=" & decCore & ", dealer=" & decDealer & ", warningheader='" & strWarningHeader & "', warning='" & strWarning & "'  where part = '" & r("part") & "'", conn2)
                        conn2.Open()
                        sqlComm2.ExecuteNonQuery()
                    End Using
                End While
            End Using
        End Using
    End Sub

End Class