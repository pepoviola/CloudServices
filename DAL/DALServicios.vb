Public Class DALServicios

    Public Function filtrar(ByVal filtro As BE.BEServicioBase) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroServiciosCodigo")
            cmd.Connection = conn
            'agrego los params []
            If Not filtro Is Nothing AndAlso Not String.IsNullOrEmpty(filtro.Codigo) Then
                dbManager.addParam(cmd, "@Codigo", filtro.Codigo)
            Else
                dbManager.addParam(cmd, "@Codigo", DBNull.Value)
            End If


        Catch ex As Exception

        End Try
        Return lista
    End Function
End Class
