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
        Return (registros = 1)

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

            If Not t Is Nothing Then
                dbManager.addParam(cmd, "@IdUsuario", t.Id)
            Else
                dbManager.addParam(cmd, "@IdUsuario", DBNull.Value)
            End If

            'abro cx
            conn.Open()
            'ejecuto y obtengo el reader
            Dim lector As IDataReader = cmd.ExecuteReader()
            Do While lector.Read()
                Dim oCli As BE.BECliente = New BE.BECliente
                oCli.Nombre = Convert.ToString(lector("Nombre"))
                oCli.Apellido = Convert.ToString(lector("Apellido"))
                oCli.ClienteId = Convert.ToString(lector("Id_Cliente"))

                list.Add(oCli)
            Loop

        Catch ex As Exception
            Throw ex
        End Try
        Return list
    End Function

    Public Function Modificar(t As BE.BECliente) As Boolean Implements ICRUD(Of BE.BECliente).Modificar
        Return True
    End Function
End Class
