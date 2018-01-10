Option Explicit On
Option Strict On

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Public Class DataHelper

#Region "Properties"

    Dim pCommandText As String
    Dim pCommandType As CommandType
    Dim pDirection As ParameterDirection
    Dim pConnStr As String
    Dim pReturnValue As Integer
    Dim pParameters As List(Of SqlParameter)
    Dim pDataTableNames As String()
    Dim poCmd As SqlCommand
    Dim pReturnValues As Hashtable
    Dim oConn As SqlConnection
    Dim oReader As SqlDataReader

    Public Property CommandText() As String
        Get
            Return pCommandText
        End Get
        Set(ByVal value As String)
            pCommandText = value
        End Set
    End Property

    Public Property ConnectionString() As String
        Get
            Return pConnStr
        End Get
        Set(ByVal value As String)
            pConnStr = value
        End Set
    End Property

    Private Property Parameters() As List(Of SqlParameter)
        Get
            If pParameters Is Nothing Then
                pParameters = New List(Of SqlParameter)

            End If

            Return pParameters
        End Get
        Set(ByVal value As List(Of SqlParameter))
            pParameters = value
        End Set
    End Property

    Private Property ParamDirection() As ParameterDirection
        Get
            If pDirection = Nothing Then
                pDirection = ParameterDirection.Input
            End If

            Return pDirection
        End Get
        Set(ByVal value As ParameterDirection)
            pDirection = value

        End Set
    End Property

    Public Property CommandType() As CommandType
        Get
            Return pCommandType
        End Get
        Set(ByVal value As CommandType)
            Me.pCommandType = value

        End Set
    End Property

    Public Property ReturnValue() As Integer
        Get
            Return pReturnValue
        End Get
        Set(ByVal value As Integer)
            pReturnValue = value
        End Set
    End Property

    Public Property ReturnValues() As Hashtable
        Get
            Return pReturnValues
        End Get
        Set(ByVal value As Hashtable)
            pReturnValues = value
        End Set
    End Property


    Public Property DataTableNames() As String()
        Get
            Return pDataTableNames
        End Get
        Set(ByVal value As String())
            pDataTableNames = value
        End Set
    End Property

    Private _oCmd As SqlCommand
    Private Property oCmd As SqlCommand
        Get
            If _oCmd Is Nothing Then
                _oCmd = New SqlCommand()
            End If
            Return _oCmd
        End Get
        Set(value As SqlCommand)
            _oCmd = value
        End Set
    End Property

#End Region


#Region "Constructors"


    Public Sub New()
        'default
        Me.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString.ToString()
    End Sub

    Public Sub New(ByVal connStr As String)
        Me.ConnectionString = connStr
    End Sub

    Public Sub New(ByVal connstr As String, ByVal commandType As CommandType)
        Me.ConnectionString = connstr
        Me.CommandType = commandType
    End Sub

    Public Sub New(ByVal connStr As String, ByVal commandType As CommandType, ByVal paramDirection As ParameterDirection)
        Me.ConnectionString = connStr
        Me.CommandType = commandType
        Me.ParamDirection = paramDirection
    End Sub

    Public Sub New(ByVal connStr As String, ByVal commandType As CommandType, ByVal paramDirection As ParameterDirection, ByVal commandText As String)
        Me.ConnectionString = connStr
        Me.CommandType = commandType
        Me.ParamDirection = paramDirection
        Me.CommandText = commandText

    End Sub


#End Region


#Region "Public Methods"

    Public Sub AddParameters(ByVal name As String, ByVal value As Object, ByVal type As SqlDbType, ByVal direction As ParameterDirection)

        Dim sqlP As New SqlParameter()

        sqlP.ParameterName = name
        sqlP.Value = value
        sqlP.SqlDbType = type
        sqlP.Direction = direction


        Me.Parameters.Add(sqlP)

    End Sub

    Public Sub clear()

        Me.CommandText = Nothing
        Me.ConnectionString = Nothing
        Me.ReturnValue = Nothing
        Me.DataTableNames = Nothing
        Me.Parameters = Nothing

        If Me.oConn IsNot Nothing Then
            Me.oConn.Close()
        End If

        If Me.oReader IsNot Nothing AndAlso Me.oReader.IsClosed = False Then
            oReader.Close()
        End If

        Me.oCmd.Dispose()
    End Sub

    Public Function ExecuteAndRetrieveDataTable() As DataTable
        Me.SetConn()
        Dim oParam As SqlParameter

        Dim dt As New DataTable()


        Try
            oCmd.Connection = oConn
            oCmd.CommandText = Me.CommandText
            oCmd.CommandType = Me.CommandType

            If Not Me.Parameters Is Nothing AndAlso Me.Parameters.Count > 0 Then

                For Each oParam In Me.Parameters
                    oCmd.Parameters.Add(oParam)
                Next
            End If

            oConn.Open()

            oReader = oCmd.ExecuteReader()

            dt.Load(oReader)

            Return dt

        Catch Ex As Exception
            Throw New ApplicationException(ex.ToString())

        Finally
            oConn.Close()
        End Try


    End Function

    Public Function ExecuteAndRetrieveDataSet() As DataSet

        Me.SetConn()
        Dim oParam As SqlParameter

        Dim ds As New DataSet()

        Try
            oCmd.Connection = oConn
            oCmd.CommandText = Me.CommandText
            oCmd.CommandType = Me.CommandType


            If Not Me.Parameters Is Nothing AndAlso Me.Parameters.Count > 0 Then

                For Each oParam In Me.Parameters
                    oCmd.Parameters.Add(oParam)
                Next
            End If

            oConn.Open()

            oReader = oCmd.ExecuteReader()

            ds.Load(oReader, LoadOption.OverwriteChanges, Me.DataTableNames)

            Return ds

        Catch Ex As Exception
            Throw New ApplicationException(ex.ToString())

        Finally
            oConn.Close()
        End Try


    End Function


    Public Function ExecuteNonQuery() As Boolean
        Me.SetConn()

        Dim oParam As SqlParameter
        Dim increment As Integer = 0

        Me.ReturnValues = New Hashtable()

        oCmd.CommandText = Me.CommandText
        oCmd.CommandType = Me.CommandType
        oCmd.Connection = oConn

        If Not Me.Parameters Is Nothing AndAlso Me.Parameters.Count > 0 Then

            For Each oParam In Me.Parameters
                oCmd.Parameters.Add(oParam)
            Next
        End If

        oConn.Open()

        oCmd.ExecuteNonQuery()

        For Each oParam In Me.Parameters

            If oParam.Direction = ParameterDirection.Output Then
                Me.ReturnValues.Add(oParam.ParameterName, oParam.Value)
                increment = increment + 1
            End If
        Next

        Return True

        oConn.Close()

    End Function

    Public Function ExecuteAndRetrieveDataReader() As SqlDataReader
        Me.SetConn()

        Dim oParam As SqlParameter

        Try
            oCmd.CommandText = Me.CommandText
            oCmd.CommandType = Me.CommandType
            oCmd.Connection = Me.oConn

            For Each oParam In Me.Parameters
                oCmd.Parameters.Add(oParam)
            Next

            oConn.Open()

            oReader = oCmd.ExecuteReader()

            Return oReader

        Catch Ex As Exception
            Return Nothing
        End Try
    End Function




    Public Function ExecuteScalar() As Integer
        SetConn()

        Dim oParam As SqlParameter

        Try
            oCmd.CommandText = Me.CommandText
            oCmd.CommandType = Me.CommandType
            oCmd.Connection = oConn

            For Each oParam In Me.Parameters
                oCmd.Parameters.Add(oParam)
            Next

            oConn.Open()

            Return CInt(oCmd.ExecuteScalar())

        Catch Ex As Exception
            Return Nothing
        Finally
            oConn.Close()
        End Try
    End Function



#End Region

#Region "Private methods"

    Private Sub SetConn()
        Dim connStr As String
        connStr = Me.ConnectionString
        Me.oConn = New SqlConnection(connStr)
    End Sub

    'Public Function ExecuteSP() As DataSet

    '    Dim oConn As SqlConnection = GetConn()
    '    Dim oCmd As New SqlCommand()
    '    Dim oParam As SqlParameter
    '    Dim ds As New DataSet()
    '    Dim dt As New DataTable()
    '    Dim oReader As IDataReader

    '    Try
    '        oCmd.Connection = oConn
    '        oCmd.CommandText = Me.CommandText
    '        oCmd.CommandType = Data.CommandType.StoredProcedure

    '        If Not Me.Parameters Is Nothing AndAlso Me.Parameters.Count > 0 Then

    '            For Each oParam In Me.Parameters
    '                oCmd.Parameters.Add(oParam)
    '            Next
    '        End If

    '        oConn.Open()

    '        oReader = oCmd.ExecuteReader()

    '        dt.Load(oReader)
    '        ds.Tables.Add(dt)

    '        Return ds

    '    Catch Ex As Exception
    '        Throw New ApplicationException(ex.ToString())

    '    Finally
    '        oConn.Close()
    '    End Try


    'End Function

    'Public Function ExecuteSP() As DataSet

    '    Dim oConn As SqlConnection = GetConn()
    '    Dim oCmd As New SqlCommand()
    '    Dim oParam As SqlParameter
    '    Dim oAdapter As SqlClient.SqlDataAdapter
    '    Dim ds As New DataSet()

    '    Try
    '        oCmd.Connection = oConn
    '        oCmd.CommandText = Me.CommandText
    '        oCmd.CommandType = Data.CommandType.StoredProcedure

    '        If Not Me.Parameters Is Nothing AndAlso Me.Parameters.Count > 0 Then

    '            For Each oParam In Me.Parameters
    '                oCmd.Parameters.Add(oParam)
    '            Next
    '        End If

    '        oAdapter = New SqlClient.SqlDataAdapter()
    '        oAdapter.SelectCommand = oCmd

    '        oAdapter.Fill(ds)



    '        Return ds

    '    Catch Ex As Exception
    '        Throw New ApplicationException(ex.ToString())

    '    Finally

    '        oAdapter = Nothing
    '        oConn.Close()
    '    End Try


    'End Function

    'Public Function ExecuteNonQuery() As Boolean
    '    Dim oConn As SqlConnection = GetConn()
    '    Dim oParam As SqlParameter
    '    Dim oCmd As New SqlCommand()


    '    oCmd.CommandText = Me.CommandText
    '    oCmd.CommandType = Me.CommandType
    '    oCmd.Connection = oConn

    '    If Not Me.Parameters Is Nothing AndAlso Me.Parameters.Count > 0 Then

    '        For Each oParam In Me.Parameters
    '            oCmd.Parameters.Add(oParam)
    '        Next
    '    End If

    '    oConn.Open()

    '    oCmd.ExecuteNonQuery()

    '    For Each oParam In Me.Parameters
    '        If oParam.Direction = ParameterDirection.Output Then
    '            Me.ReturnValue = CInt(oParam.Value)
    '            Exit For
    '        End If
    '    Next

    '    Return True

    '    oConn.Close()

    'End Function



#End Region



End Class

