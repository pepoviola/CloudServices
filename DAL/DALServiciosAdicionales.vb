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

        Catch ex As Exception
            Throw ex
        End Try

        Return lista
    End Function

End Class
