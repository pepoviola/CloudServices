Public Class TagsDAL
    Implements ICRUD(Of BE.Tag)

    'singleton
    Private Shared ReadOnly instance As TagsDAL = New TagsDAL
    Private Sub New()

    End Sub
    Public Shared Function gettagDal()
        Return instance
    End Function
    'end implement tag singleton

    'publia api

    Public Function Agregar(ByVal t As BE.Tag) As Boolean Implements ICRUD(Of BE.Tag).Agregar
        Dim ret As Boolean
        Return ret
    End Function

    Public Function Eliminar(ByVal t As BE.Tag) As Boolean Implements ICRUD(Of BE.Tag).Eliminar
        Dim ret As Boolean
        Return ret
    End Function

    Public Function Filtrar(ByVal t As BE.Tag) As System.Collections.Generic.List(Of BE.Tag) Implements ICRUD(Of BE.Tag).Filtrar
        Dim lista As New List(Of BE.Tag)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroIdiomaTags")
            cmd.Connection = conn
            If Not t Is Nothing AndAlso Not String.IsNullOrEmpty(t.CodIdioma) Then
                dbManager.addParam(cmd, "@CodIdioma", t.CodIdioma)
            Else
                dbManager.addParam(cmd, "@CodIdioma", DBNull.Value)
            End If
            If Not t Is Nothing AndAlso Not (t.Id < 1) Then
                dbManager.addParam(cmd, "@IdTag", t.Id)
            Else
                dbManager.addParam(cmd, "@IdTag", DBNull.Value)
            End If
            conn.Open()
            'dr
            'Dim lector As IDataReader = cmd.ExecuteReader
            'Do While (lector.Read())

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                Dim otag As New BE.Tag
                otag.Codigo = Convert.ToString(lector("Codigo"))
                otag.Id = Convert.ToInt32(lector("IdTag"))
                otag.Leyenda = Convert.ToString(lector("Leyenda"))
                lista.Add(otag)
                'Loop
            Next
        Catch ex As Exception
        Finally
            conn.Close()
        End Try
        Return lista
    End Function

    Public Function Modificar(ByVal t As BE.Tag) As Boolean Implements ICRUD(Of BE.Tag).Modificar
        Dim ret As Boolean
        Return ret
    End Function

    Public Function ModificarVarios(ByVal t As List(Of BE.Tag)) As Boolean
        Dim ret As Boolean = True
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                For Each tag As BE.Tag In t
                    Dim cmd As IDbCommand = dbManager.getCmd("UpdateLeyenda")
                    cmd.Connection = conn
                    dbManager.addParam(cmd, "@IdTag", tag.Id)
                    dbManager.addParam(cmd, "@CodIdioma", tag.CodIdioma)
                    dbManager.addParam(cmd, "@Leyenda", tag.Leyenda)
                    'conn open
                    cmd.Transaction = trans
                    cmd.ExecuteNonQuery()

                Next
                trans.Commit()

            Catch ex As Exception
                'rollback
                trans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return ret
    End Function


    Public Function getTagValue(ByVal tag As String, ByVal idioma As Integer) As String
        ' habilitar en forma develop para ver los tags
        'Dim ret As String = String.Format("No existe el código {0} en el idioma {1}", tag, idioma)
        Dim ret As String = ""
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectLeyenda")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@Tag", tag)
            dbManager.addParam(cmd, "@IdIdioma", idioma)
            'open
            conn.Open()
            'Dim lector As IDataReader = cmd.ExecuteReader
            'Do While (lector.Read())
            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                ret = Convert.ToString(lector("Leyenda"))
                'Loop
            Next
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return ret
    End Function

    Public Function obtenerTodos() As List(Of BE.Tag)
        Dim lista As New List(Of BE.Tag)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectAllTags")
            cmd.Connection = conn
            conn.Open()
            'dr
            'Dim lector As IDataReader = cmd.ExecuteReader
            'Do While (lector.Read())

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                Dim otag As New BE.Tag
                otag.Codigo = Convert.ToString(lector("Codigo"))
                otag.Id = Convert.ToInt32(lector("IdTag"))
                lista.Add(otag)
                'Loop
            Next
        Catch ex As Exception
        Finally
            conn.Close()
        End Try
        Return lista
    End Function
End Class
