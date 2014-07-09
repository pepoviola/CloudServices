Public Class BLEvento

    Public Shared Function getEventosVM(ByVal ev As BE.BEEvento) As List(Of BE.BEEvento)
        Dim oDAL As DAL.DALEvento = DAL.DALEvento.getDALEvento()
        Try
            Return oDAL.filtrar(ev)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrObtenerServiciosCli")
        End Try
    End Function

    Public Shared Sub crearEventoVM(ByVal ev As BE.BEEvento)
        Dim oDAL As DAL.DALEvento = DAL.DALEvento.getDALEvento()
        Try
            oDAL.crearEvento(ev)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrObtenerServiciosCli")
        End Try
    End Sub

End Class
