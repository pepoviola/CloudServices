Public Class BLServicesFacade

    ''' <summary>
    ''' Singleton implementation of facade
    ''' </summary>
    ''' <remarks></remarks>

    Private Shared ReadOnly instance As BLServicesFacade = New BLServicesFacade
    Private Sub New()

    End Sub

    Public Shared Function getServicesFacade() As BLServicesFacade
        Return instance
    End Function



    ' Public API
    '
    '

    ''' <summary>
    ''' Lista los servicios disponibles a la venta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ServiciosDisponibles() As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try
            ' busco los servicios disponibles
            ' cloud servers
            Dim oDalSrv As DAL.DALCloudServer = New DAL.DALCloudServer()
            lista.AddRange(oDalSrv.disponibles())
            

        Catch ex As Exception
            'Lanzar ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrObtenerDisponibles")
        End Try

        Return lista
    End Function

    ''' <summary>
    ''' Lista los servicios disponibles a la venta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AdicionalesDisponibles() As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try
            ' busco los servicios disponibles
            ' cloud servers
            Dim oDalSrv As DAL.DALServiciosAdicionales = New DAL.DALServiciosAdicionales
            lista.AddRange(oDalSrv.disponibles())


        Catch ex As Exception
            'Lanzar ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrObtenerDisponibles")
        End Try

        Return lista
    End Function



    Public Function obtenerServiciosDeCliente(ByVal oCli As BE.BECliente) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try
            Dim dal As DAL.DALServicios = New DAL.DALServicios()
            lista = dal.obtenerServiciosDeCliente(oCli)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrObtenerServiciosCli")
        End Try
        Return lista
    End Function

    Public Function generarEntorno(ByVal ops As Dictionary(Of String, String)) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try

            lista = BLEntorno.generarEntorno(ops)
            'recorro la lista y busco la informacion de los servicios
            Dim oDal As DAL.DALServicios = New DAL.DALServicios
            For Each s As BE.BECloudServer In lista
                Dim temp As String = s.Descripcion
                oDal.getServiceInfo(s)
                s.Descripcion = temp
                If Not s.Srv_adicionales Is Nothing Then
                    For Each addon As BE.BEServicioAdicional In s.Srv_adicionales
                        Dim temp_addon As String = addon.Descripcion
                        oDal.getServiceInfo(addon)
                        addon.Descripcion = temp_addon

                    Next

                End If
            Next

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrGenerarEntorno")
        End Try

        Return lista

    End Function
End Class
