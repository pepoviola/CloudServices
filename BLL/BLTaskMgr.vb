

Public Class BLTaskMgr
    Public Shared Sub runJob()
        Dim oDAL As DAL.DALTaskMgr = DAL.DALTaskMgr.getTaskMgr()
        Dim oDALServer As DAL.DALCloudServer = New DAL.DALCloudServer()
        Dim oDALEv As DAL.DALEvento = DAL.DALEvento.getDALEvento()
        Dim oEv As BE.BEEvento = New BE.BEEvento
        Dim _lista As List(Of BE.BETask) = New List(Of BE.BETask)
        Dim filtro As BE.BETask = New BE.BETask
        Try
            'busco las task
            _lista = oDAL.filtrar(filtro)
            For Each Task As BE.BETask In _lista
                'llamo al proxy para ver el resultado
                Dim res As Integer = vSphereProxy.CheckTask(Task.Task)
                If res > 2 Then
                    Task.Server.Estado = res
                    Task.FechaOut = DateTime.Now
                    Task.Resultado = res
                    oDALServer.updateVM(Task.Server)
                    oDAL.actualizar(Task)
                    ' creo el evento
                    oEv.Evento = "startup"
                    oEv.Descripcion = "Se creó la vm"
                    oEv.Fecha = DateTime.Now
                    oEv.Server = Task.Server
                    oDALEv.crearEvento(oEv)
                End If
            Next
        Catch ex As Exception
        Finally
            _lista = Nothing
            oDALServer = Nothing
            oEv = Nothing
        End Try
    End Sub

End Class
