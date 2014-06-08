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

    Public Shared Function getTagMgr() As BLMgrReporte
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
            Dim b As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))



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
                Dim d_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
                If primer_mes And Xmes > 0 Then
                    Ymes_prox = Xmes
                    primer_mes = False
                    d_mes.Add("proy", Nothing)
                    d_mes.Add("real", Xmes.ToString)
                    b.Add(fecha_filtro_desde.ToString("MM/yyyy"), d_mes)
                Else
                    If Xmes > 0 Then
                        d_mes.Add("proy", Ymes)
                        d_mes.Add("real", Xmes.ToString)
                        b.Add(fecha_filtro_desde.ToString("MM/yyyy"), d_mes)
                    End If

                    Ymes_prox = _CalcularMesProximo(Xmes, Ymes)

                End If

                Ymes = Ymes_prox
            Next

            Dim dd_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
            dd_mes.Add("proy", Ymes)
            dd_mes.Add("real", Nothing)
            b.Add(dateNow.AddMonths(+1).ToString("MM/yyyy"), dd_mes)

            repo.Titulo = "Ventas_Totales_Proyectadas"
            repo.Cuerpo = b
            repo.Footer = "nuevas_ventas_agrupadas_por_plata_por_mes"

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        End Try
        Return repo
    End Function

    Public Function CrearReporteVentas() As BE.BEReporte
        Dim repo As BE.BEReporte = New BE.BEReporte
        Try
            ' ovs
            Dim BLov As BLOrdenVenta = New BLOrdenVenta
            Dim ovs As List(Of BE.BEOrdenVenta) = New List(Of BE.BEOrdenVenta)
            Dim b As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))

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
                Dim d_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
                If primer_mes And Xmes > 0 Then
                    Ymes_prox = Xmes
                    primer_mes = False
                    d_mes.Add("proy", Nothing)
                    d_mes.Add("real", Xmes.ToString)
                    b.Add(fecha_filtro_desde.ToString("MM/yyyy"), d_mes)
                Else
                    If Xmes > 0 Then
                        d_mes.Add("proy", Ymes)
                        d_mes.Add("real", Xmes.ToString)
                        b.Add(fecha_filtro_desde.ToString("MM/yyyy"), d_mes)
                    End If

                    Ymes_prox = _CalcularMesProximo(Xmes, Ymes)

                End If

                Ymes = Ymes_prox
            Next

            Dim dd_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
            dd_mes.Add("proy", Ymes)
            dd_mes.Add("real", Nothing)
            b.Add(dateNow.AddMonths(+1).ToString("MM/yyyy"), dd_mes)

            repo.Titulo = "Ventas_Totales_Proyectadas"
            repo.Cuerpo = b
            repo.Footer = "nuevas_ventas_agrupadas_por_mes"

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        End Try

        Return repo
    End Function




    Public Function CrearReporteVentasPorTipo() As BE.BEReporte
        Dim repo As BE.BEReporte = New BE.BEReporte
        Try
            ' ovs
            Dim BLov As BLOrdenVenta = New BLOrdenVenta
            Dim ovs As List(Of BE.BEOrdenVenta) = New List(Of BE.BEOrdenVenta)
            Dim b As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))

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
                Dim total_mes As Double = 0
                Dim t As String
                For Each ov As BE.BEOrdenVenta In ovs
                    For Each s As BE.BEServicioBase In ov.Servicios
                        total_mes += 1
                        t = s.GetType().Name
                        t = s.GetType().FullName

                    Next

                Next

                'Xmes = total_mes
                'Dim d_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
                'If primer_mes And Xmes > 0 Then
                '    Ymes_prox = Xmes
                '    primer_mes = False
                '    d_mes.Add("proy", Nothing)
                '    d_mes.Add("real", Xmes.ToString)
                '    b.Add(fecha_filtro_desde.ToString("MM/yyyy"), d_mes)
                'Else
                '    If Xmes > 0 Then
                '        d_mes.Add("proy", Ymes)
                '        d_mes.Add("real", Xmes.ToString)
                '        b.Add(fecha_filtro_desde.ToString("MM/yyyy"), d_mes)
                '    End If

                '    Ymes_prox = _CalcularMesProximo(Xmes, Ymes)

                'End If

                'Ymes = Ymes_prox
            Next

            Dim dd_mes As Dictionary(Of String, String) = New Dictionary(Of String, String)
            dd_mes.Add("proy", Ymes)
            dd_mes.Add("real", Nothing)
            b.Add(dateNow.AddMonths(+1).ToString("MM/yyyy"), dd_mes)

            repo.Titulo = "Ventas_Totales_Proyectadas"
            repo.Cuerpo = b
            repo.Footer = "nuevas_ventas_agrupadas_por_mes"

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        End Try

        Return repo
    End Function

End Class
