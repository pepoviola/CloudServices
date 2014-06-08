Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization


Public Class repo_data
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        Dim flia As String = context.Session("flia")
        If Not String.IsNullOrEmpty(context.Session("auth")) And Utilidades.getUtilidades().tieneAcceso("reportes", flia) Then
            Try
                If context.Request.HttpMethod = "POST" Then
                    Dim repo As BE.BEReporte = New BE.BEReporte()
                    'Dim bllRepo As BLL.BLMgrReporte = New BLL.BLMgrReporte()

                    If context.Request.Form.Get("type") = "pesos" Then
                        repo = BLL.BLMgrReporte.getTagMgr().CrearReporteProyeccion()
                       


                    ElseIf context.Request.Form.Get("type") = "q_ventas" Then
                        repo = BLL.BLMgrReporte.getTagMgr().CrearReporteVentas()
                     

                    ElseIf context.Request.Form.Get("type") = "q_ventas_por" Then

                        repo = BLL.BLMgrReporte.getTagMgr().CrearReporteVentasPorTipo()
                    Else


                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("repo_generado_err", context.Session("lang")))

                    End If

                    repo.Titulo = Infra.TraductorMgr.TraducirControl(repo.Titulo, context.Session("lang"))
                    repo.Footer = Infra.TraductorMgr.TraducirControl(repo.Footer, context.Session("lang"))
                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("repo_generado_ok", context.Session("lang")))
                    resp.Add("repo", repo)

                End If
            Catch ex As ExceptionsPersonales.CustomException
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))
            Finally
                Dim oRes As String = ""
                oRes = jss.Serialize(resp)
                context.Response.Write(oRes)
            End Try
        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class