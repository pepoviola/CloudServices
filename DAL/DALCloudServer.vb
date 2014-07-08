Imports System.Data.SqlClient

Public Class DALCloudServer

    Public Function disponibles() As List(Of BE.BECloudServer)
        Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco los servicios disponibles
            Dim cmd As IDbCommand = dbManager.getCmd("SelectCloudServersDisponibles")
            cmd.Connection = conn
            'abro cx
            conn.Open()
            'ejecuto y obtengo el reader
            'Dim lector As IDataReader = cmd.ExecuteReader()
            'Do While lector.Read()
            ' old implementation
            'Dim s As BE.BECloudServer = New BE.BECloudServer

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows

                Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector("Codigo"))))
                Dim s As BE.BECloudServer = DirectCast(Activator.CreateInstance(t), BE.BECloudServer)

                s.Codigo = Convert.ToString(lector("Codigo"))
                s.Descripcion = Convert.ToString(lector("Descripcion"))
                s.Id = Convert.ToInt32(lector("Id"))
                s.Nombre = Convert.ToString(lector("Nombre"))
                s.Precio = Convert.ToDouble(lector("Precio"))

                lista.Add(s)
            Next
            'Loop

            Return lista
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()

            'limpio
            lista = Nothing
        End Try

        'Return lista

    End Function


    Public Function bajaServicio(ByVal oServ As BE.BECloudServer) As Boolean

        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'open
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                'get cmd
                Dim cmd As IDbCommand = dbManager.getCmd("DeleteRelPorServer")
                'agrego los params
                dbManager.addParam(cmd, "@IdServer", oServ.Id)
                cmd.Connection = conn
                cmd.Transaction = trans
                cmd.ExecuteNonQuery()

                ' ahora bajo el servicio
                cmd = dbManager.getCmd("BajaServicio")
                'agrego los params
                dbManager.addParam(cmd, "@Id", oServ.Id)
                dbManager.addParam(cmd, "@Fecha", DateTime.Now())
                cmd.Connection = conn
                cmd.Transaction = trans
                cmd.ExecuteNonQuery()

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

    Public Function CrearServerContratado(ByVal oServer As BE.BECloudServer, ByVal oOV As BE.BEOrdenVenta) As Integer

        Dim conn As IDbConnection = dbManager.getConnection
        Dim idParam As SqlParameter
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try

                Dim cmd_srv As IDbCommand = dbManager.getCmd("InsertSrvContratadoAndOutputId")

                'asocio la cx
                cmd_srv.Connection = conn
                cmd_srv.Transaction = trans


                ' agrego los params
                dbManager.addParam(cmd_srv, "@Id_tipo_srv", oServer.Id)
                dbManager.addParam(cmd_srv, "@Id_ov", oOV.Id)
                dbManager.addParam(cmd_srv, "@Precio", oServer.Precio)
                dbManager.addParam(cmd_srv, "@Id_server_plataforma", oServer.Platform_server.Id)

                idParam = CType(dbManager.addParam(cmd_srv, "@id"), SqlParameter)
                idParam.Direction = ParameterDirection.Output

                cmd_srv.ExecuteNonQuery()

                Dim id_srv_padre As Integer = idParam.Value



                ''agrego la vm
                'Dim cmd As IDbCommand = dbManager.getCmd("InsertVM")
                ''asocio la cx
                'cmd.Connection = conn
                'cmd.Transaction = trans

                '' agrego los params
                'If oServer.Nombre Is Nothing Then
                '    dbManager.addParam(cmd, "@vmName", String.Format("vm-{0}", id_srv_padre))
                'Else
                '    dbManager.addParam(cmd, "@vmName", oServer.Nombre)
                'End If

                'dbManager.addParam(cmd, "@mem", oServer.Memoria)
                'dbManager.addParam(cmd, "@cpu", oServer.Qcpu)
                'dbManager.addParam(cmd, "@so", "CloudOS")
                'dbManager.addParam(cmd, "@idCli", oOV.Cliente.ClienteId)
                'dbManager.addParam(cmd, "@idSrv", id_srv_padre)



                'cmd.ExecuteNonQuery()




                trans.Commit()
                Return id_srv_padre

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

    Public Function CrearVM(ByVal oServer As BE.BECloudServer, ByVal oOV As BE.BEOrdenVenta, ByVal task As Integer) As Boolean

        Dim conn As IDbConnection = dbManager.getConnection
        Dim idParam As SqlParameter
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try


                'agrego la vm
                Dim cmd As IDbCommand = dbManager.getCmd("InsertVM")
                'asocio la cx
                cmd.Connection = conn
                cmd.Transaction = trans

                ' agrego los params
                If oServer.Nombre Is Nothing Then
                    dbManager.addParam(cmd, "@vmName", String.Format("vm-{0}", oServer.Id))
                Else
                    dbManager.addParam(cmd, "@vmName", oServer.Nombre)
                End If

                dbManager.addParam(cmd, "@mem", oServer.Memoria)
                dbManager.addParam(cmd, "@cpu", oServer.Qcpu)
                dbManager.addParam(cmd, "@so", "CloudOS")
                dbManager.addParam(cmd, "@idCli", oOV.Cliente.ClienteId)
                dbManager.addParam(cmd, "@idSrv", oServer.Id)



                cmd.ExecuteNonQuery()

                ' agrego la tarea
                cmd = dbManager.getCmd("InsertTask")
                'asocio la cx
                cmd.Connection = conn
                cmd.Transaction = trans

                ' agrego los params

                dbManager.addParam(cmd, "@task", task)
                dbManager.addParam(cmd, "@fechaIn", DateTime.Now)
                dbManager.addParam(cmd, "@susc", oServer.Id)

                cmd.ExecuteNonQuery()


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

    Public Sub updateVM(ByVal oServer As BE.BECloudServer)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()

            Dim cmd As IDbCommand = dbManager.getCmd("updateVM")
            'asocio la cx
            cmd.Connection = conn

            dbManager.addParam(cmd, "@susc", oServer.Id)
            dbManager.addParam(cmd, "@estado", oServer.Estado)

            cmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

End Class
