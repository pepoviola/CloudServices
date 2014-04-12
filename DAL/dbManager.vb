Imports System.Data.SqlClient
Public Class dbManager
    '
    'dbManager
    ' brinda servicios
    ' getConn -> retorna objeto connection
    ' getCmd -> retorna objeto command
    ' addParam -> agrega un parametro

    'Private Const ConnString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=tinytickets;User ID=uai;Password=uai"
    'Private Const ConnString As String = Configuration.ConfigurationManager.ConnectionStrings("Tinytickets").ConnectionString


    Public Shared Function getConnection() As IDbConnection
        Dim ConnString As String = Configuration.ConfigurationManager.ConnectionStrings("Tinytickets").ConnectionString

        Dim conn As SqlConnection = New SqlConnection(ConnString)
        Return conn

    End Function

    Public Shared Function getConnection(ByVal cx As String) As IDbConnection
        Dim ConnString As String = Configuration.ConfigurationManager.ConnectionStrings(cx).ConnectionString

        Dim conn As SqlConnection = New SqlConnection(ConnString)
        'conn.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=tinytickets;User ID=uai;Password=uai"
        Return conn

    End Function
   

    Public Shared Function getCmd() As IDbCommand
        Dim cmd As New SqlCommand
        'cmd.Connection = getConnection()
        Return cmd

    End Function

    Public Shared Function getCmd(ByVal nombreSP As String) As IDbCommand
        Dim cmd As New SqlCommand(nombreSP)
        cmd.CommandType = CommandType.StoredProcedure
        Return cmd
    End Function
    Public Shared Function getCmd(ByVal nombreSP As String, ByVal conn As SqlConnection) As IDbCommand
        Dim cmd As New SqlCommand(nombreSP)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = conn
        'conn.Open()
        Return cmd
    End Function

    Public Shared Function getDataFromReader(ByVal cmd As IDbCommand) As IDataReader
        'ejecuta el comando y devuelve el reader
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader()
        Return dr

    End Function

    Public Shared Function addParam(ByVal cmd As IDbCommand, ByVal paramName As String, ByVal paramValue As Object) As IDbDataParameter

        'cmd.Parameters.Add(New SqlParameter(paramName, paramValue))

        'solucion de diego
        Return CType(cmd, SqlCommand).Parameters.AddWithValue(paramName, paramValue)

        'Return cmd

    End Function

    Public Shared Function addParam(ByVal cmd As IDbCommand, ByVal paramName As String) As IDbDataParameter

        'cmd.Parameters.Add(New SqlParameter(paramName, paramValue))

        'solucion de diego
        Return CType(cmd, SqlCommand).Parameters.Add(paramName, SqlDbType.Int)

        'AddWithValue(paramName, paramValue)

        'Return cmd

    End Function

  

    

End Class
