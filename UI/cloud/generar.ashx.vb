Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class generar
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Or Not context.Session("flia_desc") = "cliente" Then
            ' sin response
        Else
            ' only alow post
            If context.Request.HttpMethod = "POST" Then
                Dim ops As Dictionary(Of String, String) = New Dictionary(Of String, String)
                For Each k As String In context.Request.Form.Keys

                    ops.Add(k, context.Request.Form.Get(k))

                Next

                Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
                lista = BLL.BLServicesFacade.getServicesFacade().generarEntorno(ops)
                resp.Add("status", "200")
                resp.Add("s", jss.Serialize(lista))

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