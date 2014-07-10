Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class save_fw
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
                    'Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
                    'Dim lista As IEnumerable(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
                    Dim _lista As List(Of BE.BERegla) = New List(Of BE.BERegla)
                    _lista = jss.Deserialize(context.Request.Form.Get("reglas"), GetType(List(Of BE.BERegla)))

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
                    fw.Reglas = _lista
                    fw.FechaIn = DateTime.Now
                    fw.Cliente = oCli

                    Dim BL As BLL.BLLGrupoSeguridad = New BLL.BLLGrupoSeguridad()
                    If BL.Actualziar(fw) Then
                        'bitacora
                        'preparo bitacora
                        Dim oBita As New BE.Bitacora
                        Dim oBitaUser As New BE.BEUsuario
                        oBitaUser.Id = context.Session("user_id")
                        oBita.Fecha = Date.Now
                        oBita.Usuario = oBitaUser
                        oBita.Categoria = "Grupos Seguridad"
                        oBita.Descripcion = "El usuario " + context.Session("Username") + " modificó el grupo de seguridad" + fw.Nombre
                        'guardo en bitacora
                        Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                        oInfraBita.Log(oBita)

                    End If

                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("sg_actualizar_ok", context.Session("lang")))

                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("sg_actualizar_err", context.Session("lang")))
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