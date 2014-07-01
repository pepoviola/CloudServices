Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class generate_url
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' allow post only
        If context.Request.HttpMethod = "POST" Then
            Dim oCliente As BE.BECliente = New BE.BECliente
            oCliente.Email = context.Request.Form.Get("email")
            oCliente.PregSecreta = New BE.BEPreguntaSecreta
            oCliente.PregSecreta.Pregunta = context.Request.Form.Get("pregunta")
            oCliente.PregSecreta.Respuesta = context.Request.Form.Get("respuesta")
            'oCliente.Id = 0
            Try
                Dim blCli As BLL.BLLCliente = New BLL.BLLCliente
                If (blCli.resetClave(oCliente)) Then
                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("se_envio_el_mail", context.Session("lang")))

                Else
                    resp.Add("status", "400")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("datos_no_validos", context.Session("lang")))
                End If


            Catch ex As ExceptionsPersonales.CustomException
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))


            Catch ex As Exception
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl("datos_no_validos", context.Session("lang")))
            End Try

            Dim oRes = jss.Serialize(resp)
            context.Response.Write(oRes)
        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class