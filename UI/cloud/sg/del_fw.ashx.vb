Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class del_fw
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
                    'obtengo el cliente de la session
                    Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()

                    Dim oCli As BE.BECliente = New BE.BECliente
                    Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
                    oFiltro.Id = context.Session("user_id")
                    oFiltro.Username = context.Session("username")
                    oCli = blCli.obtenerCliente(oFiltro)

                    Dim fw As BE.BEGrupoSeguridad = New BE.BEGrupoSeguridad
                    fw.Nombre = context.Request.Form.Get("name")
                    fw.Id = context.Request.Form.Get("sid")

                    Dim BL As BLL.BLLGrupoSeguridad = New BLL.BLLGrupoSeguridad()
                    If BL.Eliminar(fw) Then
                        'bitacora
                        'preparo bitacora
                        Dim oBita As New BE.Bitacora
                        Dim oBitaUser As New BE.BEUsuario
                        oBitaUser.Id = context.Session("user_id")
                        oBita.Fecha = Date.Now
                        oBita.Usuario = oBitaUser
                        oBita.Categoria = "Grupos Seguridad"
                        oBita.Descripcion = "El usuario " + context.Session("Username") + " eliminó el grupo de seguridad " + fw.Nombre
                        'guardo en bitacora
                        Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                        oInfraBita.Log(oBita)

                    End If

                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("sg_deleted_ok", context.Session("lang")))

                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("sg_deleted_err", context.Session("lang")))

                Finally
                    Dim oRes = jss.Serialize(resp)
                    context.Response.Write(oRes)
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