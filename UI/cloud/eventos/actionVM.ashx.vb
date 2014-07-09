Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class actionVM
    Implements System.Web.IHttpHandler, IReadOnlySessionState

   

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Or Not context.Session("flia_desc") = "cliente" Then

        Else

            Try
                'obtengo el cliente de la session
                Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()
                Dim oBL As BLL.BLServicesFacade = BLL.BLServicesFacade.getServicesFacade()

                Dim oCli As BE.BECliente = New BE.BECliente
                Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
                oFiltro.Id = context.Session("user_id")
                oFiltro.Username = context.Session("username")
                oCli = blCli.obtenerCliente(oFiltro)

                If oBL.tieneAccesoVM(oCli, context.Request.Form.Get("id")) Then
                    ' tiene acceso ejecuto la tarea
                    If BLL.vSphereProxy.actionVM(context.Request.Form.Get("id"), context.Request.Form.Get("action")) Then
                        'guardo el evento y en la bitacora
                        Dim des As String = "El usuario " + context.Session("Username") + "realizó la acción " + context.Request.Form.Get("action")
                        des += " sobre la vm-" + context.Request.Form.Get("id")

                        ' creo el evento
                        Dim oEv As BE.BEEvento = New BE.BEEvento
                        oEv.Evento = context.Request.Form.Get("action")
                        oEv.Descripcion = "se realizó la acción sobre la vm"
                        oEv.Fecha = DateTime.Now
                        oEv.Server = New BE.BECloudServerBasic()
                        oEv.Server.Id = context.Request.Form.Get("id")
                        BLL.BLEvento.crearEventoVM(oEv)

                        'preparo bitacora
                        Dim oBita As New BE.Bitacora
                        Dim oBitaUser As New BE.BEUsuario
                        oBitaUser.Id = context.Session("user_id")
                        oBita.Fecha = Date.Now
                        oBita.Usuario = oBitaUser
                        oBita.Categoria = "Servicios"
                        oBita.Descripcion = des
                        'guardo en bitacora
                        Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                        oInfraBita.Log(oBita)
                    End If
                End If

                resp.Add("status", "200")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl("action_ok", context.Session("lang")))

            Catch ex As ExceptionsPersonales.CustomException
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))

            Catch ex As Exception
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl("action_err", context.Session("lang")))
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