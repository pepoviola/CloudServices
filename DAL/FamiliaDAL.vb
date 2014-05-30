Imports System.Data.SqlClient

Public Class FamiliaDAL
    '<summary>
    ' singleton
    '<summary>

    Private Sub New()

    End Sub
    Private Shared instance As FamiliaDAL = New FamiliaDAL

    Public Shared Function getFliaDal() As FamiliaDAL
        Return instance
    End Function

    'Public Function getPermisos(ByVal oFlia As BE.BEFamilia) As BE.BEFamilia
    '    '<doc>
    '    ' from User
    '    '</doc>
    '    ' TODO 

    '    Dim conn As IDbConnection = dbManager.getConnection
    '    Try
    '        conn.Open()
    '        Dim cmd As IDbCommand = dbManager.getCmd
    '        cmd.Connection = conn
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.CommandText = "SelectAccessForUser"
    '        cmd.Parameters.Add(New SqlParameter("@IdPatente", oFlia.codigo))


    '        Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
    '        Dim drsql = CType(dr, SqlDataReader)

    '        Return oFlia
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        conn.Close()
    '    End Try

    'End Function

    Public Function getAll() As List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim cmd As IDbCommand = dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SelectAllFamilys"

            Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
            Dim drsql = CType(dr, SqlDataReader)
            While (drsql.Read)

                _lista.Add(isNative(drsql))

            End While

            Return _lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    Public Function getAll(ByVal Flianame As String) As List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim cmd As IDbCommand = dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SelectAllFamilysWithLike"
            cmd.Parameters.Add(New SqlParameter("@Descrip", Flianame))


            Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
            Dim drsql = CType(dr, SqlDataReader)
            While (drsql.Read)

                _lista.Add(isNative(drsql))

            End While

            Return _lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    Public Function getPatentes(ByVal oFlia As BE.BEPatenteBasica) As List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        'obtengo todas las patentes de la familia y las retorno en la lista
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim cmd As IDbCommand = dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "getPermisosFromFlia"


            cmd.Parameters.Add(New SqlParameter("@IdFlia", oFlia.codigo))
            Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
            Dim drsql = CType(dr, SqlDataReader)
            While (drsql.Read)

                _lista.Add(isNative(drsql))

            End While

            Return _lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function


    Public Function getAllPatentes(ByVal oFlia As BE.BEPatenteBasica) As List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        'obtengo todas las patentes de la familia y las retorno en la lista
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim cmd As IDbCommand = dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "getPermisosFromFlia"


            cmd.Parameters.Add(New SqlParameter("@IdFlia", oFlia.codigo))
            Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
            Dim drsql = CType(dr, SqlDataReader)
            While (drsql.Read)

                _lista.Add(isNative(drsql))

            End While

            Return _lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    Private Function isNative(ByVal drsql As SqlDataReader) As BE.BEPatenteBasica
        'if native == 0 -> it's a native permision
        'else it's a family

        If drsql(2) > 0 Then
            Dim oPatente As New BE.BEFamilia
            oPatente.codigo = drsql(0)
            oPatente.descripcion = drsql(1)
            oPatente.Patentes = Me.getPatentes(oPatente)
            oPatente.Nativo = 1 ' not a native one
            Return oPatente
        Else
            Dim oPatente As New BE.BEPermiso
            oPatente.codigo = drsql(0)
            oPatente.descripcion = drsql(1)
            Return oPatente
        End If

    End Function

    'TODO 
    '   change the return to boolean
    Public Function Save(ByVal flia As BE.BEFamilia, ByVal dicDvh As Dictionary(Of Integer, String)) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection

        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                Dim fliaDvh As String = Criptografia.Crypto.getCrypto().generarMD5(flia.descripcion + Convert.ToString(flia.Nativo))
                'inserto la flia
                Dim cmd As IDbCommand = dbManager.getCmd
                Dim IdFlia As Integer
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "InsertFliaAndOutputId"
                cmd.Transaction = trans
                cmd.Parameters.Add(New SqlParameter("@descrip", flia.descripcion))
                cmd.Parameters.Add(New SqlParameter("@nativo", flia.Nativo))
                cmd.Parameters.Add(New SqlParameter("@dvh", fliaDvh))

                'parametro de salida
                Dim ParametroSalida As New SqlParameter()
                ParametroSalida.Direction = ParameterDirection.Output
                ParametroSalida.ParameterName = "@id"
                ParametroSalida.DbType = DbType.Int32
                cmd.Parameters.Add(ParametroSalida)

                cmd.ExecuteNonQuery()
                IdFlia = ParametroSalida.Value


                Dim id As New Integer
                id = ParametroSalida.Value
                'recorro el diccionario y cargo los permisos
                Dim pair As KeyValuePair(Of Integer, String)
                For Each pair In dicDvh
                    'inserto la flia
                    Dim cmdPermiso As IDbCommand = dbManager.getCmd
                    cmdPermiso.Connection = conn
                    cmdPermiso.CommandType = CommandType.StoredProcedure
                    cmdPermiso.CommandText = "InsertPermisoRel"
                    cmdPermiso.Transaction = trans
                    cmdPermiso.Parameters.Add(New SqlParameter("@Flia", IdFlia))
                    cmdPermiso.Parameters.Add(New SqlParameter("@Permiso", pair.Key))
                    'cmdPermiso.Parameters.Add(New SqlParameter("@dvh", pair.Value))
                    cmdPermiso.ExecuteNonQuery()
                Next
                trans.Commit()
                Return True
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function Modify(ByVal oFlia As BE.BEFamilia, ByVal permisos As List(Of BE.BEPatenteBasica)) As Boolean

        'obtengo el objeto conn
        Dim conn As IDbConnection = dbManager.getConnection
        'first try
        Try
            'open the connection to catch if throw ex
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            'second try for transaction
            Try
                'borro los permisos asignados a esta familia 
                Dim cmd As IDbCommand = dbManager.getCmd("DeleteAllPermisonOfFlia")
                cmd.Connection = conn
                cmd.Transaction = trans
                dbManager.addParam(cmd, "@IdFlia", oFlia.codigo)
                cmd.ExecuteNonQuery()

                '2do paso cargo los permisos nuevos
                For Each permiso In permisos
                    Dim cmd2 As IDbCommand = dbManager.getCmd("InsertPermisoRel")
                    cmd2.Connection = conn
                    cmd2.Transaction = trans
                    dbManager.addParam(cmd2, "@Flia", oFlia.codigo)
                    dbManager.addParam(cmd2, "@Permiso", permiso.codigo)
                    'dbManager.addParam(cmd2, "@dvh", permiso.DVH)
                    cmd2.ExecuteNonQuery()
                Next
                trans.Commit()
                Return True
            Catch ex As Exception
                'si falla la transaccion
                trans.Rollback()
                Return False
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function


    'TODO
    '   change return value
    Public Function Delete(ByVal oFlia As BE.BEFamilia) As Boolean
        Dim registros As Integer = True
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("DeleteFlia")
            'asigno la conn
            cmd.Connection = conn
            dbManager.addParam(cmd, "@IdFlia", oFlia.codigo)
            conn.Open()
            registros = cmd.ExecuteNonQuery()

            'Return (registros > 0)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function
End Class
