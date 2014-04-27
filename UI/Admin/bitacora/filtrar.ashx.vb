Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class filtrar
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    ''' <summary>
    ''' Filtra la bitacora segun:
    '''  usuario / categoria / fecha
    ''' 
    ''' serializa la respuesta en json
    ''' </summary>
    ''' <param name="context"></param>
    ''' <remarks></remarks>
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        ' protect the page
        'If String.IsNullOrEmpty(context.Session("auth")) Then
        'FormsAuthentication.RedirectToLoginPage()
        'ElseIf si tiene acceso
        'Else
        Dim resp As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        Dim oRes As String = ""
        Try
            'busco los filtros
            Dim oBita As BE.Bitacora = New BE.Bitacora()
            If Not String.IsNullOrEmpty(context.Request.Form.Get("bita_filtro_usuario")) Then
                Dim oUser As BE.BEUsuario = New BE.BEUsuario()
                oUser.Username = context.Request.Form.Get("bita_filtro_usuario")
                oBita.Usuario = oUser
            End If
            If Not String.IsNullOrEmpty(context.Request.Form.Get("bita_filtro_categoria")) Then
                oBita.Categoria = context.Request.Form.Get("bita_filtro_categoria")
            End If

            Dim oInfra As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
            Dim lista As List(Of BE.Bitacora) = New List(Of BE.Bitacora)
            lista = oInfra.filtrar(oBita)
            resp.Add("status", "200")
            resp.Add("rows", lista)
            oRes = jss.Serialize(resp)


        Catch ex As ExceptionsPersonales.CustomException
            'send el error

        End Try
        context.Response.Write(oRes)
        'End If



    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class