Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class filtra_usuarios
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            ' only allow post
            If context.Request.HttpMethod = "POST" Then
                Try
                    'creo el usuario apra filtrar
                    Dim oUser As BE.BEUsuario = New BE.BEUsuario
                    If Not String.IsNullOrEmpty(context.Request.Form.Get("id")) Then
                        oUser.Id = context.Request.Form.Get("id")
                    End If

                    If Not String.IsNullOrEmpty(context.Request.Form.Get("username")) Then
                        oUser.Username = context.Request.Form.Get("username")
                    End If

                    If Not String.IsNullOrEmpty(context.Request.Form.Get("nombre")) Then
                        oUser.Nombre = context.Request.Form.Get("nombre")
                    End If

                    If Not String.IsNullOrEmpty(context.Request.Form.Get("apellido")) Then
                        oUser.Username = context.Request.Form.Get("apellido")
                    End If

                    ' filtro en infra
                    Dim oInfra As Infra.InfraUsuario = New Infra.InfraUsuario
                    Dim lista As List(Of BE.BEUsuario) = New List(Of BE.BEUsuario)
                    lista = oInfra.Filtrar(oUser)
                    resp.Add("status", "200")
                    resp.Add("lista", jss.Serialize(lista))

                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))

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