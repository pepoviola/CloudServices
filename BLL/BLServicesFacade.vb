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

            Return lista

        Catch ex As Exception
            'Lanzar ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrObtenerDisponibles")
        Finally
            lista = Nothing
        End Try

        'Return lista
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

            Return lista

        Catch ex As Exception
            'Lanzar ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrObtenerDisponibles")
        Finally
            lista = Nothing
        End Try

        'Return lista
    End Function



    Public Function obtenerServiciosDeCliente(ByVal oCli As BE.BECliente) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try
            Dim oDal As DAL.DALServicios = DAL.DALServicios.getServiciosDAL() 'New DAL.DALServicios()
            lista = oDal.obtenerServiciosDeCliente(oCli)

            Return lista

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrObtenerServiciosCli")

        Finally
            lista = Nothing
        End Try
        'Return lista
    End Function

    Public Function generarEntorno(ByVal ops As Dictionary(Of String, String)) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try

            lista = BLEntorno.generarEntorno(ops)
            'recorro la lista y busco la informacion de los servicios
            Dim oDal As DAL.DALServicios = DAL.DALServicios.getServiciosDAL() 'New DAL.DALServicios
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


            Return lista

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrGenerarEntorno")

        Finally
            lista = Nothing
        End Try

        'Return lista

    End Function

    Public Function bajaServicio(ByVal oServ As BE.BEServicioBase) As Boolean
        Dim res As Boolean = True
        Try
            If TypeOf oServ Is BE.BECloudServer Then
                Dim oDal As DAL.DALCloudServer = New DAL.DALCloudServer
                res = oDal.bajaServicio(oServ)

            Else
                Dim oDal As DAL.DALServiciosAdicionales = New DAL.DALServiciosAdicionales
                res = oDal.bajaServicio(oServ)

            End If
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ERR_baja")
        End Try
        Return res

    End Function
End Class
