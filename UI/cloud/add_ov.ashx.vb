Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class add_ov
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        lista = jss.Deserialize(context.Request.Form.Get("ov"), GetType(List(Of BE.BECloudServer)))




    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class