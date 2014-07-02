Public Class BLMgrReporte
    Private _atenuacion As Double = 0.5

    Private Function _CalcularMesProximo(ByVal Xmes As Double, ByVal Ymes As Double) As Double
        Dim res As Double
        res = (_atenuacion * Xmes) + (1 - _atenuacion) * Ymes
        Return res
    End Function


    'singleton class

    Private Shared ReadOnly instance As BLMgrReporte = New BLMgrReporte()
    Private Sub New()

    End Sub

    Public Shared Function getRepoMgr() As BLMgrReporte
        Return instance
    End Function
    'end singleton implementation

    Public Function CrearReporteProyeccion() As BE.BEReporte
        Dim repo As BE.BEReporte = New BE.BEReporte
        Try
            ' primero debo obtener las ovs
            ' mensualizarlas
            ' y luego calcular
            ' utlizando el metodo de atenuacion exponencial
            ' formula : Yt+1 = A * Xt + ( 1 - A ) * Yt

            ' donde
            ' Yt : pronostico hecho previamente para el período actual
            ' Xt : valor real para el período
            ' A : constante de atenuación ( 0,2 )
            ' Yt+1 : pronostico futuro

            ' ovs
            Dim BLov As BLOrdenVenta = New BLOrdenVenta
            Dim ovs As List(Of BE.BEOrdenVenta) = New List(Of BE.BEOrdenVenta)
            Dim dicData As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))



            Dim dateNow = DateTime.Now()
            Dim Xmes As Double = 0 ' real
            Dim Ymes As Double = 0 ' proyectado

            ' obtengo las ovs de 6 meses atras maximo
            Dim primer_mes As Boolean = True
            For i As Integer = 6 To 0 Step -1
                Dim Ymes_prox As Double = 0
                Dim dateAgo As DateTime = dateNow.AddMonths(-i)
                Dim fecha_filtro_desde = New DateTime(dateAgo.Year, dateAgo.Month, 1, 0, 0, 1)
                Dim filtro As BE.BEOrdenVenta = New BE.BEOrdenVenta
                filtro.Fecha = fecha_filtro_desde
                ovs = BLov.FiltrarMes(filtro)
                ' obtengo la cantidad $$ mensual
                Dim total_mes As Double = 0
                For Each ov As BE.BEOrdenVenta In ovs
                    For Each s As BE.BEServicioBase In ov.Servicios
                        total_mes += s.Precio
                    Next

                Next

                Xmes = total_mes
                Dim dicData_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
                If primer_mes And Xmes > 0 Then
                    Ymes_prox = Xmes
                    primer_mes = False
                    dicData_mes.Add("proy", Nothing)
                    dicData_mes.Add("real", Xmes.ToString)
                    dicData.Add(fecha_filtro_desde.ToString("MM/yyyy"), dicData_mes)
                Else
                    If Xmes > 0 Then
                        dicData_mes.Add("proy", Ymes)
                        dicData_mes.Add("real", Xmes.ToString)
                        dicData.Add(fecha_filtro_desde.ToString("MM/yyyy"), dicData_mes)
                    End If

                    Ymes_prox = _CalcularMesProximo(Xmes, Ymes)

                End If

                Ymes = Ymes_prox
            Next

            Dim dicDataLast_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
            dicDataLast_mes.Add("proy", Ymes)
            dicDataLast_mes.Add("real", Nothing)
            dicData.Add(dateNow.AddMonths(+1).ToString("MM/yyyy"), dicDataLast_mes)

            repo.Titulo = "Ventas_Totales_Proyectadas"
            repo.Cuerpo = dicData
            repo.Footer = "nuevas_ventas_agrupadas_por_plata_por_mes"

            Return repo

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")

        Finally
            repo = Nothing
        End Try
        'Return repo
    End Function

    Public Function CrearReporteVentas() As BE.BEReporte
        Dim repo As BE.BEReporte = New BE.BEReporte
        Try
            ' ovs
            Dim BLov As BLOrdenVenta = New BLOrdenVenta
            Dim ovs As List(Of BE.BEOrdenVenta) = New List(Of BE.BEOrdenVenta)
            Dim dicData As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))

            Dim dateNow = DateTime.Now()
            Dim Xmes As Double = 0 ' real
            Dim Ymes As Double = 0 ' proyectado

            ' obtengo las ovs de 6 meses atras maximo
            Dim primer_mes As Boolean = True
            For i As Integer = 6 To 0 Step -1
                Dim Ymes_prox As Double = 0
                Dim dateAgo As DateTime = dateNow.AddMonths(-i)
                Dim fecha_filtro_desde = New DateTime(dateAgo.Year, dateAgo.Month, 1, 0, 0, 1)
                Dim filtro As BE.BEOrdenVenta = New BE.BEOrdenVenta
                filtro.Fecha = fecha_filtro_desde
                ovs = BLov.FiltrarMes(filtro)
                ' obtengo la cantidad $$ mensual
                Dim total_mes As Double = 0
                For Each ov As BE.BEOrdenVenta In ovs
                    For Each s As BE.BEServicioBase In ov.Servicios
                        total_mes += 1
                    Next

                Next

                Xmes = total_mes
                Dim data_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
                If primer_mes And Xmes > 0 Then
                    Ymes_prox = Xmes
                    primer_mes = False
                    data_mes.Add("proy", Nothing)
                    data_mes.Add("real", Xmes.ToString)
                    dicData.Add(fecha_filtro_desde.ToString("MM/yyyy"), data_mes)
                Else
                    If Xmes > 0 Then
                        data_mes.Add("proy", Ymes)
                        data_mes.Add("real", Xmes.ToString)
                        dicData.Add(fecha_filtro_desde.ToString("MM/yyyy"), data_mes)
                    End If

                    Ymes_prox = _CalcularMesProximo(Xmes, Ymes)

                End If

                Ymes = Ymes_prox
            Next

            Dim dataLast_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
            dataLast_mes.Add("proy", Ymes)
            dataLast_mes.Add("real", Nothing)
            dicData.Add(dateNow.AddMonths(+1).ToString("MM/yyyy"), dataLast_mes)

            repo.Titulo = "Ventas_Totales_Proyectadas"
            repo.Cuerpo = dicData
            repo.Footer = "nuevas_ventas_agrupadas_por_mes"

            Return repo

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        Finally
            repo = Nothing
        End Try

        'Return repo
    End Function




    Public Function CrearReporteVentasPorTipo() As BE.BEReporte
        Dim repo As BE.BEReporte = New BE.BEReporte
        Try
            ' ovs
            Dim BLov As BLOrdenVenta = New BLOrdenVenta
            Dim ovs As List(Of BE.BEOrdenVenta) = New List(Of BE.BEOrdenVenta)
            Dim dicData As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))

            Dim dateNow = DateTime.Now()
            Dim Xmes As Double = 0 ' real
            Dim Ymes As Double = 0 ' proyectado

            ' obtengo las ovs de 6 meses atras maximo
            Dim primer_mes As Boolean = True
            For i As Integer = 6 To 0 Step -1
                'Dim Ymes_prox As Double = 0
                Dim dateAgo As DateTime = dateNow.AddMonths(-i)
                Dim fecha_filtro_desde = New DateTime(dateAgo.Year, dateAgo.Month, 1, 0, 0, 1)
                Dim filtro As BE.BEOrdenVenta = New BE.BEOrdenVenta
                filtro.Fecha = fecha_filtro_desde
                ovs = BLov.FiltrarMes(filtro)
                ' obtengo la cantidad $$ mensual
                'Dim total_mes As Double = 0
                'Dim t As String

                ' inicializo los valores del dict para este mes
                Dim dic_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
                dic_mes.Add("BECloudServerBasic", 0)
                dic_mes.Add("BECloudServerAdvance", 0)
                dic_mes.Add("BECloudServerPro", 0)
                dic_mes.Add("BEBackupService", 0)
                dic_mes.Add("BESnapshot", 0)

                For Each ov As BE.BEOrdenVenta In ovs
                    For Each s As BE.BEServicioBase In ov.Servicios
                        'total_mes += 1
                        Dim s_name As String = s.GetType().Name        
                        dic_mes.Item(s_name) = dic_mes.Item(s_name) + 1
                    Next

                Next

                dicData.Add(fecha_filtro_desde.ToString("MM/yyyy"), dic_mes)
            Next

            'Dim dd_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
            'dd_mes.Add("proy", Ymes)
            'dd_mes.Add("real", Nothing)
            'dicData.Add(dateNow.AddMonths(+1).ToString("MM/yyyy"), dd_mes)

            repo.Titulo = "Ventas_Totales_Por_Tipo"
            repo.Cuerpo = dicData
            repo.Footer = "nuevas_ventas_agrupadas_por_mes"

            Return repo

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        Finally
            repo = Nothing
        End Try


    End Function

    Public Function CrearReporteUsoPorServer() As BE.BEReporte
        Dim oSrvPlataforma As BE.BEServerPlataforma = New BE.BEServerPlataforma
        Dim repo As BE.BEReporte = New BE.BEReporte
        Dim listado_srv As List(Of BE.BEServerPlataforma)
        Dim oBLServers As BLServerPlataforma = New BLServerPlataforma
        Dim oBlServicios As BLServicesFacade = BLServicesFacade.getServicesFacade()
        Dim dicData As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
        Dim lista_servicios As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        Try
            ' obtengo el listado de servers fisicos
            listado_srv = oBLServers.Filtrar(oSrvPlataforma)
            ' lleno la propiedad
            For Each oServer In listado_srv
                lista_servicios = oBlServicios.obtenerServiciosDeServer(oServer)
                Dim dicServer As New Dictionary(Of String, String)
                dicServer.Add("mem_total", oServer.Memoria.ToString())
                Dim mem_usada As Integer = 0
                For Each servicio As BE.BECloudServer In lista_servicios
                    mem_usada += servicio.Memoria
                Next
                dicServer.Add("mem_usada", mem_usada.ToString())
                dicData.Add(oServer.Hostname, dicServer)
            Next

            repo.Cuerpo = dicData
            repo.Titulo = "uso_servers_fisicos"
            repo.Footer = "uso_memoria_servers_fisicos_porcien"
            Return repo
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        Finally
            repo = Nothing
            dicData = Nothing
            oBLServers = Nothing
            listado_srv = Nothing
            oSrvPlataforma = Nothing
            lista_servicios = Nothing
        End Try


    End Function


    Public Function CrearReporteCapacidad() As BE.BEReporte
        Dim oSrvPlataforma As BE.BEServerPlataforma = New BE.BEServerPlataforma
        Dim repo As BE.BEReporte = New BE.BEReporte
        Dim listado_srv As List(Of BE.BEServerPlataforma)
        Dim oBLServers As BLServerPlataforma = New BLServerPlataforma
        Dim oBlServicios As BLServicesFacade = BLServicesFacade.getServicesFacade()
        Dim dicData As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
        Dim subData As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim lista_servicios As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        'totales
        Dim total_mem As Integer = 0
        Dim mem_used As Integer = 0
        Dim proyected_mem As Integer = 0

        Dim repo_ventas As BE.BEReporte = New BE.BEReporte
        Dim dateNow = DateTime.Now()
        Dim oBasic As BE.BECloudServerBasic = New BE.BECloudServerBasic
        Dim oAdvance As BE.BECloudServerAdvance = New BE.BECloudServerAdvance
        Dim oPro As BE.BECloudServerPro = New BE.BECloudServerPro

        Try
            ' obtengo el total de memoria y el total consumido
            listado_srv = oBLServers.Filtrar(oSrvPlataforma)
            ' lleno la propiedad
            For Each oServer In listado_srv
                lista_servicios = oBlServicios.obtenerServiciosDeServer(oServer)
                total_mem += oServer.Memoria
                For Each servicio As BE.BECloudServer In lista_servicios
                    mem_used += servicio.Memoria
                Next
            Next

            ' me traigo el reporte de ventas con proyeccion
            repo_ventas = CrearReporteVentas()
            Dim ventas_proy = repo_ventas.Cuerpo.Item(dateNow.AddMonths(+1).ToString("MM/yyyy")).Item("proy")

            ' calculamos el 50% de ventas de basic (2)
            ' el 40 de advance (4) y el 10 de pro (16)
            Dim mem_basic = CInt((ventas_proy * 0.5) * oBasic.Memoria)
            Dim mem_adv = CInt((ventas_proy * 0.4) * oAdvance.Memoria)
            Dim mem_pro = CInt((ventas_proy * 0.1) * oPro.Memoria)
            proyected_mem = mem_basic + mem_adv + mem_pro

            subData.Add("total", total_mem.ToString())
            subData.Add("usada", mem_used.ToString())
            subData.Add("proy", proyected_mem.ToString())
            subData.Add("libre", (total_mem - mem_used - proyected_mem).ToString())

            dicData.Add("plataforma", subData)
            repo.Cuerpo = dicData
            repo.Titulo = "Repo_capacity"
            repo.Footer = "Repo_capacity_proyectada"
            Return repo

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        Finally
            repo = Nothing
            dicData = Nothing
            oBLServers = Nothing
            listado_srv = Nothing
            oSrvPlataforma = Nothing
            lista_servicios = Nothing
            repo_ventas = Nothing
            oAdvance = Nothing
            oPro = Nothing
            oBasic = Nothing
        End Try
    End Function


End Class
