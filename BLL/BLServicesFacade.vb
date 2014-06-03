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



End Class
