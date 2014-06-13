Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class del_servicio
    Implements System.Web.IHttpHandler, IReadOnlySessionState


    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Or Not context.Session("flia_desc") = "cliente" Then

        Else

            ' only alow post
            If context.Request.HttpMethod = "POST" Then
                Try

                    ' instancio el objeto dinamicamente
                    Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", _
                                                               context.Request.Form.Get("codigo")))
                    Dim oServicio = Activator.CreateInstance(t)
                    oServicio.Id = context.Request.Form.Get("sid")
                    'Dim oBllServ As BLL.BLServicesFacade =
                    If BLL.BLServicesFacade.getServicesFacade().bajaServicio(oServicio) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("baja_realizada", context.Session("lang")))
                    Else
                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("ERR_baja", context.Session("lang")))
                    End If


                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))
                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("ERR_baja", context.Session("lang")))
                End Try

                Dim oRes = jss.Serialize(resp)
                context.Response.Write(oRes)
            End If

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class