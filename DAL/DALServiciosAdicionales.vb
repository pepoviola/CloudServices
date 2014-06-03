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
            Dim lector As IDataReader = cmd.ExecuteReader()
            Do While lector.Read()


                Dim s As BE.BEServicioAdicional = New BE.BEServicioAdicional
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
