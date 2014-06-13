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

        Catch ex As Exception
            Throw ex
        End Try

        Return lista

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
End Class
