Public Class BLMgrReporte
    Private _atenuacion As Double = 0.5

    Private Function _CalcularMesProximo(ByVal Xmes As Double, ByVal Ymes As Double) As Double
        Dim res As Double
        res = (_atenuacion * Xmes) + (1 - _atenuacion) * Ymes
        Return res
    End Function
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
            Dim mes_valor As Dictionary(Of String, Double) = New Dictionary(Of String, Double)
            Dim mes_valor_t As Dictionary(Of String, Double) = New Dictionary(Of String, Double)
            Dim mes_valor_proy As Dictionary(Of String, Double) = New Dictionary(Of String, Double)

            Dim dateNow = DateTime.Now()
            Dim Xmes As Double = 0 ' real
            Dim Ymes As Double = 0 ' proyectado

            ' obtengo las ovs de 6 meses atras maximo
            Dim primer_mes As Boolean = True
            For i As Integer = 6 To 0 Step -1
                Dim Ymes_prox As Double = 0
                Dim fecha_filtro_desde = New DateTime(dateNow.Year, dateNow.AddMonths(-i).Month, 1, 0, 0, 1)
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
                mes_valor.Add(fecha_filtro_desde.Month, total_mes)
                mes_valor_t.Add(fecha_filtro_desde.Month, Ymes)
                Xmes = total_mes
                If primer_mes Then
                    Ymes_prox = Xmes
                    primer_mes = False
                Else
                    Ymes_prox = _CalcularMesProximo(Xmes, Ymes)

                End If
                mes_valor_proy.Add(fecha_filtro_desde.Month, Ymes_prox)
                Ymes = Ymes_prox
            Next


            Dim q As Double = Ymes






        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_get_repo")
        End Try
        Return repo
    End Function
End Class
