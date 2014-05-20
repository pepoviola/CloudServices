Imports System.Data.SqlClient

' DAL :: Idioma
' acceso a la db para los idiomas

Public Class IdiomaDAL
    Implements ICRUD(Of BE.Idioma)


    'singleton
    Private Shared ReadOnly instance As IdiomaDAL = New IdiomaDAL

    Private Sub New()

    End Sub

    'obtener la instancia
    Public Shared Function getIdiomaDAL() As IdiomaDAL
        Return instance
    End Function

    'fin implementacion singleton


    Public Function Agregar(ByVal t As BE.Idioma) As Boolean Implements ICRUD(Of BE.Idioma).Agregar
        Dim retorno As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'transaction?
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                'cmds
                Dim cmd As IDbCommand = dbManager.getCmd("InsertIdiomaAndOutputID")
                cmd.Connection = conn
                cmd.Transaction = trans
                dbManager.addParam(cmd, "@Codigo", t.Codigo)
                dbManager.addParam(cmd, "@Descrip", t.Descripcion)

                Dim idParam As SqlParameter = CType(dbManager.addParam(cmd, "@id"), SqlParameter)
                idParam.Direction = ParameterDirection.Output
                cmd.ExecuteNonQuery()
                Dim idIdioma As Integer = idParam.Value

                'now loop over array
                For Each tag As BE.Tag In t.Tags
                    Dim cmdTag As IDbCommand = dbManager.getCmd("InsertTagIdioma")
                    cmdTag.Connection = conn
                    cmdTag.Transaction = trans
                    dbManager.addParam(cmdTag, "@IdIdioma", idIdioma)
                    dbManager.addParam(cmdTag, "@IdTag", tag.Id)
                    dbManager.addParam(cmdTag, "@Leyenda", tag.Leyenda)
                    cmdTag.ExecuteNonQuery()
                Next

                trans.Commit()
                retorno = True
            Catch ex As Exception
                'ex
                trans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return retorno

    End Function

    Public Function Eliminar(ByVal t As BE.Idioma) As Boolean Implements ICRUD(Of BE.Idioma).Eliminar
        Dim retorno As Boolean
        Dim afectadas As Integer
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("DeleteIdioma")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@idioma", t.Id)
            conn.Open()
            afectadas = cmd.ExecuteNonQuery()
            retorno = True

        Catch ex As Exception
            Throw ex
        End Try
        Return retorno
    End Function

    Public Function Filtrar(ByVal filtro As BE.Idioma) As System.Collections.Generic.List(Of BE.Idioma) Implements ICRUD(Of BE.Idioma).Filtrar
        Dim _lista As New List(Of BE.Idioma)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'obtengo el command
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroIdioma")
            'asocio la cx
            cmd.Connection = conn
            'agrego los params [Codigo, Descrip]
            If Not filtro Is Nothing AndAlso Not String.IsNullOrEmpty(filtro.Codigo) Then
                dbManager.addParam(cmd, "@codigo", filtro.Codigo)
            Else
                dbManager.addParam(cmd, "@codigo", DBNull.Value)
            End If
            If Not filtro Is Nothing AndAlso Not String.IsNullOrEmpty(filtro.Descripcion) Then
                dbManager.addParam(cmd, "@Descrip", filtro.Descripcion)
            Else
                dbManager.addParam(cmd, "@Descrip", DBNull.Value)
            End If
            'ejecuto y obtengo el reader
            conn.Open()
            Dim lector As IDataReader = cmd.ExecuteReader()
            Do While lector.Read()
                Dim oIdioma As New BE.Idioma
                oIdioma.Codigo = Convert.ToString(lector("codigo"))
                oIdioma.Descripcion = Convert.ToString(lector("Descrip"))
                oIdioma.Id = Convert.ToInt32(lector("IdIdioma"))

                _lista.Add(oIdioma)
            Loop
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return _lista
    End Function

    Public Function Modificar(ByVal t As BE.Idioma) As Boolean Implements ICRUD(Of BE.Idioma).Modificar
        Dim retorno As Boolean
        Return retorno

    End Function
End Class
