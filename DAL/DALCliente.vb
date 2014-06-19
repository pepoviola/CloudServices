Imports System.Data.SqlClient
Public Class DALCliente
    Implements ICRUD(Of BE.BECliente)


    Public Function Agregar(t As BE.BECliente) As Boolean Implements ICRUD(Of BE.BECliente).Agregar
        Dim registros As Integer = 0
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                Dim dvhString As String
                dvhString = t.Apellido + t.Nombre + t.Estado + t.Email
                dvhString += t.Username + t.Passwd + Convert.ToString(t.Idioma.Id) + Convert.ToString(t.Patente.codigo)
                Dim userDvh As String = Criptografia.Crypto.getCrypto.generarMD5(dvhString)

                'get cmd
                Dim cmd As IDbCommand = dbManager.getCmd("InsertUsuarioAndOutputId")
                cmd.Connection = conn
                cmd.Transaction = trans

                'agrego los params
                dbManager.addParam(cmd, "@Apellido", t.Apellido)
                dbManager.addParam(cmd, "@Nombre", t.Nombre)
                dbManager.addParam(cmd, "@Username", t.Username)
                dbManager.addParam(cmd, "@Password", t.Passwd)
                dbManager.addParam(cmd, "@email", t.Email)
                dbManager.addParam(cmd, "@IdIdioma", t.Idioma.Id)
                dbManager.addParam(cmd, "@IdPatente", t.Patente.codigo)
                dbManager.addParam(cmd, "@DVH", userDvh)

                Dim idParam As SqlParameter = CType(dbManager.addParam(cmd, "@id"), SqlParameter)
                idParam.Direction = ParameterDirection.Output


                cmd.ExecuteNonQuery()
                Dim idUser As Integer = idParam.Value

                ' ahora agrego la direccion
                ' primero la calle
                ' luego localidad
                Dim cmd_calle As IDbCommand = dbManager.getCmd("IfNotInsertAndOutputCalle")
                cmd_calle.Connection = conn
                cmd_calle.Transaction = trans

                dbManager.addParam(cmd_calle, "@calle", t.Direccion.Calle.ToUpper())
                Dim idParam_calle As SqlParameter = CType(dbManager.addParam(cmd_calle, "@id"), SqlParameter)
                idParam_calle.Direction = ParameterDirection.Output

                cmd_calle.ExecuteNonQuery()
                Dim idCalle As Integer = idParam_calle.Value

                Dim cmd_loc As IDbCommand = dbManager.getCmd("IfNotInsertAndOutputLocalidad")
                cmd_loc.Connection = conn
                cmd_loc.Transaction = trans

                dbManager.addParam(cmd_loc, "@localidad", t.Direccion.Localidad.ToUpper())
                Dim idParam_loc As SqlParameter = CType(dbManager.addParam(cmd_loc, "@id"), SqlParameter)
                idParam_loc.Direction = ParameterDirection.Output

                cmd_loc.ExecuteNonQuery()
                Dim idLoc As Integer = idParam_loc.Value

                Dim cmd_dir As IDbCommand = dbManager.getCmd("InsertDireccion")
                cmd_dir.Connection = conn
                cmd_dir.Transaction = trans

                dbManager.addParam(cmd_dir, "@idCalle", idCalle)
                dbManager.addParam(cmd_dir, "@idLoc", idLoc)
                dbManager.addParam(cmd_dir, "@num", t.Direccion.Numero)

                Dim idParam_dir As SqlParameter = CType(dbManager.addParam(cmd_dir, "@id"), SqlParameter)
                idParam_dir.Direction = ParameterDirection.Output

                cmd_dir.ExecuteNonQuery()
                Dim idDir As Integer = idParam_dir.Value

                ' por ultimo agrego el cliente
                Dim cmd_cli As IDbCommand = dbManager.getCmd("InsertCliente")
                cmd_cli.Connection = conn
                cmd_cli.Transaction = trans

                dbManager.addParam(cmd_cli, "@idUser", idUser)
                dbManager.addParam(cmd_cli, "@idDir", idDir)
                dbManager.addParam(cmd_cli, "@pregunta", t.PregSecreta.Pregunta)
                dbManager.addParam(cmd_cli, "@respuesta", t.PregSecreta.Respuesta)
                cmd_cli.ExecuteNonQuery()

                ' ok
                trans.Commit()
                registros = 1

                Return (registros = 1)

            Catch ex As SqlException
                trans.Rollback()
                If ex.ErrorCode = 2601 Then
                    Throw New ExceptionsPersonales.CustomException("username_en_uso")
                Else
                    Throw ex
                End If

            Catch ex As Exception
                trans.Rollback()

                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        'Return (registros = 1)

    End Function

    Public Function Eliminar(t As BE.BECliente) As Boolean Implements ICRUD(Of BE.BECliente).Eliminar
        Return True
    End Function

    Public Function Filtrar(t As BE.BECliente) As List(Of BE.BECliente) Implements ICRUD(Of BE.BECliente).Filtrar
        Dim list As List(Of BE.BECliente) = New List(Of BE.BECliente)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'get cmd
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroCliente")
            cmd.Connection = conn

            'agrego los params [categoria, IdUsuario, Fecha]
            If Not t Is Nothing AndAlso Not String.IsNullOrEmpty(t.Nombre) Then
                dbManager.addParam(cmd, "@Nombre", t.Nombre)
            Else
                dbManager.addParam(cmd, "@Nombre", DBNull.Value)
            End If
            If Not t Is Nothing AndAlso Not t.Apellido Is Nothing Then
                dbManager.addParam(cmd, "@Apellido", t.Apellido)
            Else
                dbManager.addParam(cmd, "@Apellido", DBNull.Value)
            End If

            If Not t Is Nothing AndAlso Not t.Username Is Nothing Then
                dbManager.addParam(cmd, "@Username", t.Username)
            Else
                dbManager.addParam(cmd, "@Username", DBNull.Value)
            End If

            If Not t Is Nothing AndAlso Not t.Id = 0 Then
                dbManager.addParam(cmd, "@IdUsuario", t.Id)
            Else
                dbManager.addParam(cmd, "@IdUsuario", DBNull.Value)
            End If

            If Not t Is Nothing AndAlso Not t.Email Is Nothing Then
                dbManager.addParam(cmd, "@email", t.Email)
            Else
                dbManager.addParam(cmd, "@email", DBNull.Value)
            End If
            'abro cx
            conn.Open()
            'ejecuto y obtengo el reader
            'Dim lector As IDataReader = cmd.ExecuteReader()
            'Do While lector.Read()

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                Dim oCli As BE.BECliente = New BE.BECliente
                oCli.Nombre = Convert.ToString(lector("Nombre"))
                oCli.Apellido = Convert.ToString(lector("Apellido"))
                oCli.ClienteId = Convert.ToString(lector("Id_Cliente"))

                list.Add(oCli)
            Next
            'Loop

            Return list

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()

            'limpio
            list = Nothing
        End Try
        'Return list
    End Function

    Public Function Modificar(t As BE.BECliente) As Boolean Implements ICRUD(Of BE.BECliente).Modificar
        Return True
    End Function

    Public Function resetClave(ByVal oCli As BE.BECliente) As String
        Dim res As String = ""
        Dim conn As IDbConnection = dbManager.getConnection

        Try

            'get cmd
            Dim cmd As IDbCommand = dbManager.getCmd("SelectUserByEmailAndRespuesta")
            cmd.Connection = conn

            dbManager.addParam(cmd, "@email", oCli.Email)
            dbManager.addParam(cmd, "@pregunta", oCli.PregSecreta.Pregunta)
            dbManager.addParam(cmd, "@respuesta", oCli.PregSecreta.Respuesta)

            'abro cx
            conn.Open()
            'ejecuto y obtengo el reader
            'Dim lector As IDataReader = cmd.ExecuteReader()
            Dim id_usuario As Integer = 0
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                'Do While lector.Read()
                id_usuario = Convert.ToInt32(lector("Id_usuario"))
                'Loop
            Next

            'lector.Close()
            If id_usuario <> 0 Then
                ' esta ok debo generar la entrada en la db
                Dim guid As String = System.Guid.NewGuid().ToString()
                res = guid
                'guardar en la db

                Dim cmd_url As IDbCommand = dbManager.getCmd("InsertResetPasswordURL")
                cmd_url.Connection = conn
                dbManager.addParam(cmd_url, "@id_user", id_usuario)
                dbManager.addParam(cmd_url, "@url", res)
                dbManager.addParam(cmd_url, "@ts", DateTime.Now())
                cmd_url.ExecuteNonQuery()


                'modifico el password
                Dim cmd_update As IDbCommand = dbManager.getCmd("UpdatePasswd")
                cmd_update.Connection = conn
                dbManager.addParam(cmd_update, "@id_user", id_usuario)
                dbManager.addParam(cmd_update, "@passwd", ".!,YThvg<#!><*]+")
                cmd_update.ExecuteNonQuery()

            End If

            Return res
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function


    Public Shared Function getUserForReset(ByVal url As String) As BE.BECliente
        Dim oCli As BE.BECliente = New BE.BECliente
        oCli.Id = 0
        Dim conn As IDbConnection = dbManager.getConnection

        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectUserByUrl")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@url", url)
            dbManager.addParam(cmd, "@ts", DateTime.Now())

            conn.Open()

            'Dim lector As IDataReader = cmd.ExecuteReader()
            'Do While lector.Read()

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                oCli.Id = Convert.ToInt32(lector("Id_user"))
            Next
            'Loop

            If oCli.Id = 0 Then
                Throw New Exception()
            End If


            Return oCli
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            'limio 
            oCli = Nothing
        End Try

        'Return oCli

    End Function

    Public Shared Function recuperarClave(ByVal oCli As BE.BECliente) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Dim afectados As Integer = 0
        Try
            Dim cmd_update As IDbCommand = dbManager.getCmd("UpdatePasswdFor")
            cmd_update.Connection = conn

            cmd_update.Connection = conn
            dbManager.addParam(cmd_update, "@id_user", oCli.Id)
            dbManager.addParam(cmd_update, "@passwd", oCli.Passwd)

            conn.Open()
            afectados = cmd_update.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()

        End Try
        'Return (afectados = 1)
        Return True
    End Function
End Class
