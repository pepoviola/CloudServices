Imports System.Data.SqlClient

Public Class DALServiciosAdicionales
    Public Function disponibles() As List(Of BE.BEServicioAdicional)
        Dim lista As List(Of BE.BEServicioAdicional) = New List(Of BE.BEServicioAdicional)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco los servicios disponibles
            Dim cmd As IDbCommand = dbManager.getCmd("SelectAdicionalesDisponibles")
            cmd.Connection = conn
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
                Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector("Codigo"))))
                Dim s As Object = Activator.CreateInstance(t)

                'Dim s As BE.BEServicioAdicional = New BE.BEServicioAdicional
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


    End Function


    Public Function bajaServicio(ByVal oServ As BE.BEServicioAdicional) As Boolean

        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'open
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                'get cmd
                Dim cmd As IDbCommand = dbManager.getCmd("DeleteRelPorAdicional")
                'agrego los params
                dbManager.addParam(cmd, "@IdAdicional", oServ.Id)
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

    Public Sub CrearAdicional(ByVal oServer As BE.BECloudServer, ByVal oAdicional As BE.BEServicioAdicional, ByVal idOv As Integer)
        Dim conn As IDbConnection = dbManager.getConnection
        Dim idParam As SqlParameter
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try


                Dim cmd_addon As IDbCommand = dbManager.getCmd("InsertSrvContratadoAndOutputId")

                'asocio la cx
                cmd_addon.Connection = conn
                cmd_addon.Transaction = trans

                ' agrego los params
                dbManager.addParam(cmd_addon, "@Id_tipo_srv", oAdicional.Id)
                dbManager.addParam(cmd_addon, "@Id_ov", idOv)
                dbManager.addParam(cmd_addon, "@Precio", oAdicional.Precio)
                dbManager.addParam(cmd_addon, "@Id_server_plataforma", oServer.Platform_server.Id)

                idParam = CType(dbManager.addParam(cmd_addon, "@id"), SqlParameter)
                idParam.Direction = ParameterDirection.Output

                cmd_addon.ExecuteNonQuery()

                Dim id_srv_hijo As Integer = idParam.Value

                ' los relaciono
                Dim cmd_rel As IDbCommand = dbManager.getCmd("InsertSrvContratadoRelacion")
                'asocio la cx
                cmd_rel.Connection = conn
                cmd_rel.Transaction = trans
                ' agrego los params
                dbManager.addParam(cmd_rel, "@Id_padre", oServer.Id)
                dbManager.addParam(cmd_rel, "@Id_hijo", id_srv_hijo)

                cmd_rel.ExecuteNonQuery()



                trans.Commit()

            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

End Class
