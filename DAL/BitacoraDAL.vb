Imports System.Data.SqlClient
'DAL: Bitacora
'
' Singleton

Public Class BitacoraDAL

    'Implementacion del singleton
    Private Shared ReadOnly instance As BitacoraDAL = New BitacoraDAL
    Private Sub New()

    End Sub

    Public Shared Function getBitacoraDal() As BitacoraDAL
        Return instance
    End Function


    'Metodos publicos
    Public Function Log(ByVal bita As BE.Bitacora) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Dim registros As Integer
        Try
            ''genero el DVH
            Dim bitaString As String = String.Empty
            Dim bitaDvh As String = String.Empty
            bitaString += bita.Descripcion + bita.Categoria _
                       + Convert.ToString(bita.Usuario.Id)
            bitaDvh = Criptografia.Crypto.getCrypto().generarMD5(bitaString)


            'obtengo el command
            Dim cmd As IDbCommand = dbManager.getCmd("InsertBitacora")
            'asocio la cx
            cmd.Connection = conn
            'agrego los params [Descrip, categoria, Fecha, IdUsuario, DVH]
            dbManager.addParam(cmd, "@Descrip", bita.Descripcion)
            dbManager.addParam(cmd, "@Categoria", bita.Categoria)
            dbManager.addParam(cmd, "@Fecha", bita.Fecha)
            dbManager.addParam(cmd, "@IdUsuario", bita.Usuario.Id)
            dbManager.addParam(cmd, "@DVH", bitaDvh)
            'abro cx
            conn.Open()
            registros = cmd.ExecuteNonQuery()
            Return (registros > 0)
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    Public Function getBitacora(ByVal filtro As BE.Bitacora, ByVal filtro_hasta As BE.Bitacora) As List(Of BE.Bitacora)
        Dim _lista As New List(Of BE.Bitacora)
        Dim conn As IDbConnection = dbManager.getConnection
        Try

            'obtengo el command
            'Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroBitacora")
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroBitacoraFechas")
            'asocio la cx
            cmd.Connection = conn
            'agrego los params [categoria, IdUsuario, Fecha]
            If Not filtro Is Nothing AndAlso Not String.IsNullOrEmpty(filtro.Categoria) Then
                dbManager.addParam(cmd, "@Categoria", filtro.Categoria)
            Else
                dbManager.addParam(cmd, "@Categoria", DBNull.Value)
            End If
            If Not filtro Is Nothing AndAlso Not filtro.Usuario Is Nothing Then
                dbManager.addParam(cmd, "@Username", filtro.Usuario.Username)
            Else
                dbManager.addParam(cmd, "@Username", DBNull.Value)
            End If

            If Not filtro Is Nothing AndAlso Not DateTime.MinValue = filtro.Fecha Then
                dbManager.addParam(cmd, "@Fecha_desde", filtro.Fecha)
            Else
                dbManager.addParam(cmd, "@Fecha_desde", DBNull.Value)
            End If


            'hasta
            If Not filtro_hasta Is Nothing AndAlso Not DateTime.MinValue = filtro_hasta.Fecha Then
                dbManager.addParam(cmd, "@Fecha_hasta", filtro_hasta.Fecha)
            Else
                dbManager.addParam(cmd, "@Fecha_hasta", DBNull.Value)
            End If


            'abro cx
            conn.Open()
            'ejecuto y obtengo el reader
            'Dim lector As IDataReader = cmd.ExecuteReader()

            ' dataset / dataadapter 
            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            'Do While lector.Read()
            For Each lector As DataRow In dt.Rows
                Dim bitarow As New BE.Bitacora
                Dim bitaUser As New BE.BEUsuario
                bitarow.Categoria = Convert.ToString(lector("Categoria"))
                bitarow.Descripcion = Convert.ToString(lector("Descripcion"))
                bitarow.Id = Convert.ToString(lector("idBitacora"))
                bitarow.Fecha = Convert.ToDateTime(lector("Fecha"))
                bitaUser.Username = Convert.ToString(lector("username"))
                bitaUser.Id = Convert.ToInt32(lector("IdUsuario"))
                bitarow.Usuario = bitaUser
                _lista.Add(bitarow)
            Next

            'Loop
            '    Return _lista

            Return _lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()

            'limpio
            _lista = Nothing
        End Try

        'Return _lista
    End Function



End Class
