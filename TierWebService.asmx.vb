Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports Pigeon.Pigeon

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
 Public Class TierWebService
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
        Public Property Local() As String
            Get
                Return m_Local
            End Get
            Set(ByVal value As String)
                m_Local = value
            End Set
        End Property
        Private m_Local As String
        Public Property BaseTiers() As String
            Get
                Return m_BaseTiers
            End Get
            Set(ByVal value As String)
                m_BaseTiers = value
            End Set
        End Property
        Private m_BaseTiers As String
        Public Property CustomerLabel() As String
            Get
                Return m_CustomerLabel
            End Get
            Set(ByVal value As String)
                m_CustomerLabel = value
            End Set
        End Property
        Private m_CustomerLabel As String
        Public Property AdminLabel() As String
            Get
                Return m_AdminLabel
            End Get
            Set(ByVal value As String)
                m_AdminLabel = value
            End Set
        End Property
        Private m_AdminLabel As String
    End Class

    Public Class TiersVisibility
        Public Property ID() As String
            Get
                Return m_ID
            End Get
            Set(ByVal value As String)
                m_ID = value
            End Set
        End Property
        Private m_ID As String
        Public Property TierID() As String
            Get
                Return m_TierID
            End Get
            Set(ByVal value As String)
                m_TierID = value
            End Set
        End Property
        Private m_TierID As String
        Public Property AdditionalTier() As String
            Get
                Return m_AdditionalTier
            End Get
            Set(ByVal value As String)
                m_AdditionalTier = value
            End Set
        End Property
        Private m_AdditionalTier As String
    End Class
    Public Class WarrantyOptions
        Public Property ID() As String
            Get
                Return m_ID
            End Get
            Set(ByVal value As String)
                m_ID = value
            End Set
        End Property
        Private m_ID As String
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property Tier() As String
            Get
                Return m_Tier
            End Get
            Set(ByVal value As String)
                m_Tier = value
            End Set
        End Property
        Private m_Tier As String
        Public Property Warranty() As String
            Get
                Return m_Warranty
            End Get
            Set(ByVal value As String)
                m_Warranty = value
            End Set
        End Property
        Private m_Warranty As String
        Public Property Base() As String
            Get
                Return m_Base
            End Get
            Set(ByVal value As String)
                m_Base = value
            End Set
        End Property
        Private m_Base As String
        Public Property Percentage() As String
            Get
                Return m_Percentage
            End Get
            Set(ByVal value As String)
                m_Percentage = value
            End Set
        End Property
        Private m_Percentage As String
        Public Property Flat() As String
            Get
                Return m_Flat
            End Get
            Set(ByVal value As String)
                m_Flat = value
            End Set
        End Property
        Private m_Flat As String
        Public Property Href() As String
            Get
                Return m_Href
            End Get
            Set(ByVal value As String)
                m_Href = value
            End Set
        End Property
        Private m_Href As String
    End Class

    Public Class TierPrice
        Public Property ID() As String
            Get
                Return m_ID
            End Get
            Set(ByVal value As String)
                m_ID = value
            End Set
        End Property
        Private m_ID As String
        Public Property PartType() As String
            Get
                Return m_PartType
            End Get
            Set(ByVal value As String)
                m_PartType = value
            End Set
        End Property
        Private m_PartType As String
        Public Property TierID() As String
            Get
                Return m_TierID
            End Get
            Set(ByVal value As String)
                m_TierID = value
            End Set
        End Property
        Private m_TierID As String
        Public Property BasePrice() As String
            Get
                Return m_BasePrice
            End Get
            Set(ByVal value As String)
                m_BasePrice = value
            End Set
        End Property
        Private m_BasePrice As String
        Public Property Percentage() As String
            Get
                Return m_Percentage
            End Get
            Set(ByVal value As String)
                m_Percentage = value
            End Set
        End Property
        Private m_Percentage As String
        Public Property Flat() As String
            Get
                Return m_Flat
            End Get
            Set(ByVal value As String)
                m_Flat = value
            End Set
        End Property
        Private m_Flat As String
        Public Property HotBuild() As String
            Get
                Return m_HotBuild
            End Get
            Set(ByVal value As String)
                m_HotBuild = value
            End Set
        End Property
        Private m_HotBuild As String
    End Class
    Public Class TierSelect
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


    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function GetTiers(ByVal client As String)
        Dim Tierlist As New List(Of Tiers)
        Dim js As New JavaScriptSerializer()


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand(" SELECT dbo.tblTiers.TierID, dbo.tblTiers.Tier, dbo.tblTiers.Local, tblTiers_1.Tier AS base, dbo.tblTiers.CustomerLabel, dbo.tblTiers.AdminLabel FROM dbo.tblTiers LEFT OUTER JOIN dbo.tblTiers AS tblTiers_1 ON dbo.tblTiers.BaseTier = tblTiers_1.TierID", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If r("TierID").ToString > 2 Then
                        Dim sqlTier As New Tiers
                        sqlTier.TierID = r("TierID").ToString
                        sqlTier.Tier = r("Tier").ToString
                        sqlTier.Local = r("Local").ToString
                        sqlTier.BaseTiers = r("base").ToString
                        sqlTier.CustomerLabel = r("CustomerLabel").ToString
                        sqlTier.AdminLabel = r("AdminLabel").ToString
                        Tierlist.Add(sqlTier)
                    End If
                End While
            End Using
        End Using

        Return js.Serialize(Tierlist)
    End Function

    <WebMethod()> _
    Public Function GetTiersVisibilty(ByVal client As String)
        Dim list As New List(Of TiersVisibility)
        Dim js As New JavaScriptSerializer()


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblTierVisibility.TierID as MainID, dbo.tblTierVisibility.ID, dbo.tblTiers.Tier AS TierID, tblTiers_1.Tier AS AdditionalTier FROM dbo.tblTiers INNER JOIN dbo.tblTierVisibility ON dbo.tblTiers.TierID = dbo.tblTierVisibility.TierID INNER JOIN dbo.tblTiers AS tblTiers_1 ON dbo.tblTierVisibility.AdditionalTier = tblTiers_1.TierID", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()

                    If r("MainID").ToString > 2 Then
                        Dim sqlTierVisibility As New TiersVisibility
                        sqlTierVisibility.ID = r("ID").ToString
                        sqlTierVisibility.TierID = r("TierID").ToString
                        sqlTierVisibility.AdditionalTier = r("AdditionalTier").ToString
                        list.Add(sqlTierVisibility)
                    End If
                End While


            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function GetWarrantyOptions(ByVal client As String)
        Dim list As New List(Of WarrantyOptions)
        Dim js As New JavaScriptSerializer()


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT     dbo.tblWarrantyOptions.Tier as MainID, dbo.tblTiers.Tier, dbo.tblWarrantyOptions.ID, dbo.tblWarrantyOptions.Warranty, dbo.tblWarrantyOptions.Base, dbo.tblWarrantyOptions.Percentage, dbo.tblWarrantyOptions.Flat, dbo.tblWarrantyOptions.Href, dbo.tblPartType.Description AS PartType FROM dbo.tblTiers INNER JOIN dbo.tblWarrantyOptions ON dbo.tblTiers.TierID = dbo.tblWarrantyOptions.Tier INNER JOIN dbo.tblPartType ON dbo.tblWarrantyOptions.PartType = dbo.tblPartType.Type", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If r("MainID") > 2 Then
                        Dim sqlWarrantyOptions As New WarrantyOptions
                        sqlWarrantyOptions.ID = r("ID").ToString
                        sqlWarrantyOptions.PartType = r("PartType").ToString
                        sqlWarrantyOptions.Tier = r("Tier").ToString
                        sqlWarrantyOptions.Warranty = r("Warranty").ToString
                        sqlWarrantyOptions.Base = r("Base").ToString
                        sqlWarrantyOptions.Percentage = r("Percentage").ToString
                        sqlWarrantyOptions.Flat = r("Flat").ToString
                        sqlWarrantyOptions.Href = r("Href").ToString

                        list.Add(sqlWarrantyOptions)
                    End If
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function GetTierPrice(ByVal client As String)
        Dim list As New List(Of TierPrice)
        Dim js As New JavaScriptSerializer()


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblTierBasePrice.TierID as MainID, dbo.tblTiers.Tier AS TierID, dbo.tblPartType.Description AS PartType, dbo.tblTierBasePrice.ID , dbo.tblTierBasePrice.BasePrice, dbo.tblTierBasePrice.Percentage, dbo.tblTierBasePrice.Flat, dbo.tblTierBasePrice.HotBuild FROM dbo.tblTiers INNER JOIN dbo.tblTierBasePrice ON dbo.tblTiers.TierID = dbo.tblTierBasePrice.TierID INNER JOIN dbo.tblPartType ON dbo.tblTierBasePrice.PartType = dbo.tblPartType.Type", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If r("MainID") > 2 Then
                        Dim sqlTierPrice As New TierPrice
                        sqlTierPrice.ID = r("ID").ToString
                        sqlTierPrice.TierID = r("TierID").ToString
                        sqlTierPrice.PartType = r("PartType").ToString
                        sqlTierPrice.Percentage = r("Percentage").ToString
                        sqlTierPrice.Flat = r("Flat").ToString
                        sqlTierPrice.HotBuild = r("HotBuild").ToString
                        list.Add(sqlTierPrice)
                    End If
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function GetTierSelects(ByVal client As String)
        Dim list As New List(Of TierSelect)
        Dim js As New JavaScriptSerializer()
        Dim TierArray As String = "{ "
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT Tier, TierID FROM tblTiers", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    TierArray &= "'" & r("TierID").ToString & "':'" & r("Tier").ToString & "', "
                End While
            End Using
        End Using
        Dim MyChar() As Char = {",", " "}
        Dim TierSelects As String = TierArray.Trim(MyChar)
        TierSelects &= " }"


        Return js.Serialize(TierSelects)
    End Function
    <WebMethod()> _
    Public Function GetNewTierSelects(ByVal client As String)
        Dim list As New List(Of TierSelect)
        Dim js As New JavaScriptSerializer()

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT Tier, TierID FROM tblTiers where TierID > 2", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    Dim sqlTier As New TierSelect
                    sqlTier.Tier = r("Tier")
                    sqlTier.TierID = r("TierID")
                    list.Add(sqlTier)
                End While
            End Using
        End Using



        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function NewTier(ByVal client As String, ByVal Tier As String, ByVal BaseTier As String, ByVal CustomerLabel As String, ByVal AdminLabel As String)
        Dim NewTierID
        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlTier As New SqlCommand("INSERT INTO tblTiers (Tier, Local, BaseTier, CustomerLabel, AdminLabel) VALUES ('" & Tier & "', '1','" & BaseTier & "','" & CustomerLabel & "','" & AdminLabel & "');select scope_identity()", conn)
            conn.Open()
            NewTierID = sqlTier.ExecuteScalar()

        End Using


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlTierVisibilty As New SqlCommand("INSERT INTO tblTierVisibility (TierID, AdditionalTier) VALUES ('" & NewTierID & "', '1');" _
                                                  & "INSERT INTO tblTierVisibility (TierID, AdditionalTier) VALUES ('" & NewTierID & "', '2');", conn)
            conn.Open()
            sqlTierVisibilty.ExecuteNonQuery()

        End Using

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlTierWarranty As New SqlCommand("INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (1,'" & NewTierID & "', '36/100 $60/hr', '1', '0', '100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (1,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (2,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (2,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (3,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (3,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (4,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (4,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (5,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (5,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (6,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (6,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (7,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (7,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (8,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (8,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (9,'" & NewTierID & "', '36/100 $60/hr', '1', '0','100.00', '36%20Month%20Warranty.pdf'  );" _
                                                    & "INSERT INTO tblWarrantyOptions (PartType, Tier, Warranty, Base, Percentage, Flat, Href) VALUES (9,'" & NewTierID & "', '36/Unlimited shop labor rate', '0', '0','100.00','36%20Unlimited%20Warranty.pdf' );", conn)
            conn.Open()
            sqlTierWarranty.ExecuteNonQuery()

        End Using


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlTierBasePrice As New SqlCommand("INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '1', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '2', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '3', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '4', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '5', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '6', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '7', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '8', 'Cost', '0', '0');" _
                                                  & "INSERT INTO tblTierBasePrice (TierID, PartType, BasePrice, Percentage, Flat) VALUES ('" & NewTierID & "', '9', 'Cost', '0', '0');", conn)
            conn.Open()
            sqlTierBasePrice.ExecuteNonQuery()

        End Using
        Return NewTierID
    End Function

    <WebMethod()> _
    Public Function GetNewTiersVisibilty(ByVal client As String, ByVal NewTierID As String)
        Dim list As New List(Of TiersVisibility)
        Dim js As New JavaScriptSerializer()

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblTierVisibility.TierID as MainID, dbo.tblTierVisibility.ID, dbo.tblTiers.Tier AS TierID, tblTiers_1.Tier AS AdditionalTier FROM dbo.tblTiers INNER JOIN dbo.tblTierVisibility ON dbo.tblTiers.TierID = dbo.tblTierVisibility.TierID INNER JOIN dbo.tblTiers AS tblTiers_1 ON dbo.tblTierVisibility.AdditionalTier = tblTiers_1.TierID WHERE dbo.tblTierVisibility.TierID = " & "'" & NewTierID & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()

                While r.Read()

                    If r("MainID").ToString > 2 Then
                        Dim sqlTierVisibility As New TiersVisibility
                        sqlTierVisibility.ID = r("ID").ToString
                        sqlTierVisibility.TierID = r("TierID").ToString
                        sqlTierVisibility.AdditionalTier = r("AdditionalTier").ToString
                        list.Add(sqlTierVisibility)
                    End If
                End While


            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function GetNewWarrantyOptions(ByVal client As String, ByVal NewTierID As String)
        Dim list As New List(Of WarrantyOptions)
        Dim js As New JavaScriptSerializer()

        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblWarrantyOptions.Tier as MainID, dbo.tblTiers.Tier, dbo.tblWarrantyOptions.ID, dbo.tblWarrantyOptions.Warranty, dbo.tblWarrantyOptions.Base, dbo.tblWarrantyOptions.Percentage, dbo.tblWarrantyOptions.Flat, dbo.tblWarrantyOptions.Href, dbo.tblPartType.Description AS PartType FROM dbo.tblTiers INNER JOIN dbo.tblWarrantyOptions ON dbo.tblTiers.TierID = dbo.tblWarrantyOptions.Tier INNER JOIN dbo.tblPartType ON dbo.tblWarrantyOptions.PartType = dbo.tblPartType.Type  WHERE dbo.tblWarrantyOptions.Tier = " & "'" & NewTierID & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If r("MainID") > 2 Then
                        Dim sqlWarrantyOptions As New WarrantyOptions
                        sqlWarrantyOptions.ID = r("ID").ToString
                        sqlWarrantyOptions.PartType = r("PartType").ToString
                        sqlWarrantyOptions.Tier = r("Tier").ToString
                        sqlWarrantyOptions.Warranty = r("Warranty").ToString
                        sqlWarrantyOptions.Base = r("Base").ToString
                        sqlWarrantyOptions.Percentage = r("Percentage").ToString
                        sqlWarrantyOptions.Flat = r("Flat").ToString
                        sqlWarrantyOptions.Href = r("Href").ToString

                        list.Add(sqlWarrantyOptions)
                    End If
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function GetNewTierPrice(ByVal client As String, ByVal NewTierID As String)
        Dim list As New List(Of TierPrice)
        Dim js As New JavaScriptSerializer()


        Using conn As New SqlConnection(GetClientConnectionString(client))
            Dim sqlComm As New SqlCommand("SELECT dbo.tblTierBasePrice.TierID as MainID, dbo.tblTiers.Tier AS TierID, dbo.tblPartType.Description AS PartType, dbo.tblTierBasePrice.ID , dbo.tblTierBasePrice.BasePrice, dbo.tblTierBasePrice.Percentage, dbo.tblTierBasePrice.Flat, dbo.tblTierBasePrice.HotBuild FROM dbo.tblTiers INNER JOIN dbo.tblTierBasePrice ON dbo.tblTiers.TierID = dbo.tblTierBasePrice.TierID INNER JOIN dbo.tblPartType ON dbo.tblTierBasePrice.PartType = dbo.tblPartType.Type WHERE dbo.tblTierBasePrice.TierID = " & "'" & NewTierID & "'", conn)
            conn.Open()
            Using r As SqlDataReader = sqlComm.ExecuteReader()
                While r.Read()
                    If r("MainID") > 2 Then
                        Dim sqlTierPrice As New TierPrice
                        sqlTierPrice.ID = r("ID").ToString
                        sqlTierPrice.TierID = r("TierID").ToString
                        sqlTierPrice.PartType = r("PartType").ToString
                        sqlTierPrice.Percentage = r("Percentage").ToString
                        sqlTierPrice.Flat = r("Flat").ToString
                        sqlTierPrice.HotBuild = r("HotBuild").ToString
                        list.Add(sqlTierPrice)
                    End If
                End While
            End Using
        End Using

        Return js.Serialize(list)
    End Function

    <WebMethod()> _
    Public Function EditTierInfo(ByVal client As String, ByVal info As String, ByVal col As String, ByVal rowid As String, ByVal table As String)
        Select Case table
            Case "Tier"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlupdate As New SqlCommand("Update tblTiers set " & col & " = '" & info & "' where TierID = '" & rowid & "'", conn)
                    conn.Open()
                    sqlupdate.ExecuteNonQuery()
                    Return True
                End Using
            Case "TierVisibilty"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlupdate As New SqlCommand("Update tblTierVisibility set " & col & " = '" & info & "' where ID = '" & rowid & "'", conn)
                    conn.Open()
                    sqlupdate.ExecuteNonQuery()
                    Return True
                End Using
            Case "WarrantyOptions"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlupdate As New SqlCommand("Update tblWarrantyOptions set " & col & " = '" & info & "' where ID = '" & rowid & "'", conn)
                    conn.Open()
                    sqlupdate.ExecuteNonQuery()
                    Return True
                End Using
            Case "TierPricing"
                Using conn As New SqlConnection(GetClientConnectionString(client))
                    Dim sqlupdate As New SqlCommand("Update tblTierBasePrice set " & col & " = '" & info & "' where ID = '" & rowid & "'", conn)
                    conn.Open()
                    sqlupdate.ExecuteNonQuery()
                    Return True
                End Using
            Case Else
                Return False
        End Select



    End Function

    <WebMethod()>
    Public Function DeleteTier(ByVal TierID As String, ByVal Client As String)
        If TierID > 2 Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(Client))
                Dim sqlTierDelete As New SqlCommand("DELETE FROM tblTiers WHERE TierID = '" & TierID & "';" _
                                                    & "DELETE FROM tblTierVisibility WHERE TierID = '" & TierID & "';" _
                                                    & "DELETE FROM tblWarrantyOptions WHERE Tier = '" & TierID & "';" _
                                                    & "DELETE FROM tblTierBasePrice WHERE TierID = '" & TierID & "';", conn)
                conn.Open()
                sqlTierDelete.ExecuteNonQuery()
            End Using
        Else
            Return False
        End If


        Return True
    End Function

    <WebMethod()>
    Public Function DeleteTierVisibility(ByVal TierVisibilityID As String, ByVal Client As String)
        If TierVisibilityID > 2 Then
            Using conn As New SqlConnection(ConnectionStrings.GetSpecificConnectionString(Client))
                Dim sqlTierVisibilityDelete As New SqlCommand("DELETE FROM tblTierVisibility WHERE ID = '" & TierVisibilityID & "'", conn)
                conn.Open()
                sqlTierVisibilityDelete.ExecuteNonQuery()
            End Using
        Else
            Return False
        End If


        Return True
    End Function
End Class


