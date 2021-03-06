﻿Public Class BLServicesFacade

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
            Dim oDALSG As DAL.DALGrupoSeguridad = DAL.DALGrupoSeguridad.getDALGrupo()
            lista = oDal.obtenerServiciosDeCliente(oCli)

            ' decoro con los grupos de seguridad
            For Each s As BE.BECloudServer In lista
                s.gruposSeguridad = New List(Of BE.BEGrupoSeguridad)
                s.gruposSeguridad = oDALSG.obtenerSGsPorVM(s)
            Next

            Return lista

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrObtenerServiciosCli")

        Finally
            lista = Nothing
        End Try
        'Return lista
    End Function

    Public Function obtenerServiciosDeServer(ByVal oServerPlataforma As BE.BEServerPlataforma) As List(Of BE.BECloudServer)
        Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        Try
            Dim oDal As DAL.DALServicios = DAL.DALServicios.getServiciosDAL() 'New DAL.DALServicios()
            lista = oDal.obtenerServiciosPorServer(oServerPlataforma)

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

    Public Sub CrearServicios(ByVal oOV As BE.BEOrdenVenta)
        'Dim oDal As DAL.DALServicios = DAL.DALServicios.getServiciosDAL()
        Dim oDALServer As DAL.DALCloudServer = New DAL.DALCloudServer()
        Dim oDALAddon As DAL.DALServiciosAdicionales = New DAL.DALServiciosAdicionales()
        Try
            'oDal.CrearServicios(oOV)
            ' recorro la ov
            For Each s As BE.BECloudServer In oOV.Servicios
                Dim idServer As Integer = oDALServer.CrearServerContratado(s, oOV)
                'actualizo el id del server con referencia a la db
                s.Id = idServer
                'llamo al proxy para levantar la vm
                Dim task As Integer = vSphereProxy.CreateVM(idServer, s.Codigo)
                'guardo la vm y la tarea
                Dim res As Boolean
                res = oDALServer.CrearVM(s, oOV, task)
                'addon
                If Not s.Srv_adicionales Is Nothing Then
                    For Each addon As BE.BEServicioBase In s.Srv_adicionales
                        oDALAddon.CrearAdicional(s, addon, oOV.Id)
                    Next
                End If
            Next
        Catch ex As Exception
            Throw ex
        Finally
            oDALAddon = Nothing
            oDALServer = Nothing
        End Try

    End Sub

    Public Function tieneAccesoVM(ByVal oCli As BE.BECliente, vmId As Integer) As Boolean
        Try
            'obtengo el listado
            Dim _lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
            _lista = obtenerServiciosDeCliente(oCli)
            For Each s As BE.BECloudServer In _lista
                If s.Id = vmId Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConfigVMGrupoSeg(ByVal oServer As BE.BECloudServer) As Boolean
        Dim oDAL As DAL.DALCloudServer = New DAL.DALCloudServer

        Try
            oDAL.ConfigSegGrp(oServer)
            Return True
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("sg_config_err")
        Finally
            oDAL = Nothing
        End Try
    End Function
End Class
