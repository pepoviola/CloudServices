Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class del_usuario
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
                    'create user
                    Dim oUser As BE.BEUsuario = New BE.BEUsuario
                    oUser.Id = context.Request.Form.Get("uid")
                    oUser.Username = context.Request.Form.Get("username")

                    ' el usuario no se puede eliminar a el mismo
                    If oUser.Id = context.Session("user_id") Then
                        Throw New ExceptionsPersonales.CustomException("no_te_puedes_eliminar")
                    End If
                    'preparo bitacora
                    Dim oBita As New BE.Bitacora
                    Dim oBitaUser As New BE.BEUsuario
                    oBitaUser.Id = context.Session("user_id")
                    ' se pasa a la dal esta logica
                    'oBita.DVH = "todo"
                    oBita.Fecha = Date.Now
                    oBita.Usuario = oBitaUser
                    oBita.Categoria = "Usuarios"

                    Dim oInfra As Infra.InfraUsuario = New Infra.InfraUsuario
                    If oInfra.Eliminar(oUser) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("user_del_ok", context.Session("lang")))
                        oBita.Descripcion = "Se eliminó con éxito el usuario " + oUser.Username

                    Else
                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("user_del_err", context.Session("lang")))
                        oBita.Descripcion = "Error al eliminar el usuario " + oUser.Username
                    End If

                    'guardo en bitacora
                    Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                    oInfraBita.Log(oBita)

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