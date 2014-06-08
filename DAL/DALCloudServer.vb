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
            Dim lector As IDataReader = cmd.ExecuteReader()
            Do While lector.Read()
                ' old implementation
                'Dim s As BE.BECloudServer = New BE.BECloudServer

                Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector("Codigo"))))
                Dim s As BE.BECloudServer = DirectCast(Activator.CreateInstance(t), BE.BECloudServer)

                s.Codigo = Convert.ToString(lector("Codigo"))
                s.Descripcion = Convert.ToString(lector("Descripcion"))
                s.Id = Convert.ToInt32(lector("Id"))
                s.Nombre = Convert.ToString(lector("Nombre"))
                s.Precio = Convert.ToDouble(lector("Precio"))

                lista.Add(s)
            Loop

        Catch ex As Exception
            Throw ex
        End Try

        Return lista

    End Function
End Class
