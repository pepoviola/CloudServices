Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class dataVM
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

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

                    Dim vmdata As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
                    vmdata = BLL.vSphereProxy.getData(context.Request.Form.Get("id"))
                    resp.Add("status", "200")
                    resp.Add("proxy_res", vmdata)
                End If

            Catch ex As ExceptionsPersonales.CustomException
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))

            Catch ex As Exception
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl("no_data", context.Session("lang")))
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