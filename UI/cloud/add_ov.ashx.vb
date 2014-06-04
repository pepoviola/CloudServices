Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class add_ov
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
                'Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
                Dim lista As IEnumerable(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
                Try

                    lista = jss.Deserialize(context.Request.Form.Get("ov"), GetType(List(Of BE.BECloudServer)))
                    Dim lista_generica As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
                    lista_generica.AddRange(lista)


                    'obtengo el cliente de la session
                    Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()

                    Dim oCli As BE.BECliente = New BE.BECliente
                    Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
                    oFiltro.Id = context.Session("user_id")
                    oCli = blCli.obtenerCliente(oFiltro)

                    ' con el user y el listado genero la ov
                    ' que luego genera los servicios
                    Dim oOV As BE.BEOrdenVenta = New BE.BEOrdenVenta()
                    oOV.Cliente = oCli
                    oOV.Fecha = Date.Now()
                    oOV.Servicios = lista_generica
                    oOV.Estado = "Activo"

                    ' lo mando a la BLL
                    Dim oBll As BLL.BLOrdenVenta = New BLL.BLOrdenVenta()
                    If oBll.Crear(oOV) Then
                        'preparo bitacora
                        Dim oBita As New BE.Bitacora
                        Dim oBitaUser As New BE.BEUsuario
                        oBitaUser.Id = context.Session("user_id")
                        oBita.Fecha = Date.Now
                        oBita.Usuario = oBitaUser
                        oBita.Categoria = "Servicios"
                        oBita.Descripcion = "El usuario " + context.Session("Username") + " contrató los servicios : [] "
                        'guardo en bitacora
                        Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                        oInfraBita.Log(oBita)

                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("ov_generated_ok", context.Session("lang")))
                    End If


                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))
                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("ErrCrearOV", context.Session("lang")))
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