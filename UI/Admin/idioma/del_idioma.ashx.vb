Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization
''' <summary>
''' Elimina el idioma
''' POST: 
''' envia el id del idioma
''' </summary>
''' <remarks></remarks>


Public Class del_idioma
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        If context.Request.HttpMethod = "POST" And context.Request.Form.Get("idlang") Then
            ' chequeo la session y el permiso
            Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma()
            Dim oIdioma As BE.Idioma = New BE.Idioma()
            oIdioma.Id = context.Request.Form.Get("idlang")
            If oInfraIdioma.Eliminar(oIdioma) Then
                Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
                resp.Add("status", "200")
                resp.Add("idlang", oIdioma.Id.ToString())
                Dim oRes = jss.Serialize(resp)
                context.Response.Write(oRes)
            Else

            End If


        Else
            context.Response.Write("")
        End If




    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class