Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class save_server
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Or Not Utilidades.getUtilidades().tieneAcceso("plataforma_write", context.Session("flia")) Then
            ' no tiene permiso
            ' respuesta vacia y con codigo 401
            context.Response.Status = 401
            Exit Sub
        Else
            ' only allow post
            If context.Request.HttpMethod = "POST" Then
                Dim oblPlat As BLL.BLServerPlataforma = New BLL.BLServerPlataforma()
                Dim oServer As BE.BEServerPlataforma = New BE.BEServerPlataforma()
                Dim oBitaUser As BE.BEUsuario = New BE.BEUsuario
                Dim oBita As New BE.Bitacora
                Dim oRes As String = String.Empty

                Try
                    oServer.Id = context.Request.Form.Get("sid")
                    oServer.Hostname = context.Request.Form.Get("hostname")
                    oServer.Memoria = context.Request.Form.Get("memoria")
                    oServer.Q_cpu = context.Request.Form.Get("qcpu")

                    'preparo bita
                    oBitaUser.Id = context.Session("user_id")
                    oBita.Fecha = Date.Now
                    oBita.Usuario = oBitaUser
                    oBita.Categoria = "Plataforma"

                    If oblPlat.Actualizar(oServer) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("server_mod_ok", context.Session("lang")))
                        oBita.Descripcion = "Se modificó con éxito el server " + oServer.Hostname
                    Else
                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("server_mod_err", context.Session("lang")))
                        oBita.Descripcion = "Error al modificar el server " + oServer.Hostname
                    End If

                    'guardo en bitacora
                    Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                    oInfraBita.Log(oBita)

                    oRes = jss.Serialize(resp)
                    context.Response.Write(oRes)

                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))
                    oRes = jss.Serialize(resp)
                    context.Response.Write(oRes)

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("server_mod_err", context.Session("lang")))
                    oRes = jss.Serialize(resp)
                    context.Response.Write(oRes)


                Finally
                    'clean
                    oblPlat = Nothing
                    oBitaUser = Nothing
                    oServer = Nothing
                    resp = Nothing
                    oRes = Nothing
                End Try



            End If

        End If
   

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class