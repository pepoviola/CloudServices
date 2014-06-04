Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class add_ov
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        Try

            lista = jss.Deserialize(context.Request.Form.Get("ov"), GetType(List(Of BE.BECloudServer)))

            'obtengo el cliente de la session
            Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()

            Dim oCli As BE.BECliente = New BE.BECliente
            Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
            oFiltro.Id = context.Session("user_id")
            oCli = blCli.obtenerCliente(oFiltro)

            ' con el user y el listado genero la ov
            ' que luego genera los servicios




        Catch ex As ExceptionsPersonales.CustomException

        Catch ex As Exception

        End Try





    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class