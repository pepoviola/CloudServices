Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class del_flias
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
                    ' do stuffs
                    Dim oFlia As BE.BEFamilia = New BE.BEFamilia
                    oFlia.codigo = context.Request.Form.Get("flia_code")
                    Dim oInfraFlia As Infra.Familia = New Infra.Familia()
                    'preparo bitacora
                    Dim oBita As New BE.Bitacora
                    Dim oBitaUser As New BE.BEUsuario
                    oBitaUser.Id = context.Session("user_id")
                    ' se pasa a la dal esta logica
                    'oBita.DVH = "todo"
                    oBita.Fecha = Date.Now
                    oBita.Usuario = oBitaUser
                    oBita.Categoria = "Familias"

                    If oInfraFlia.delFamilia(oFlia) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_del_ok", context.Session("lang")))
                        oBita.Descripcion = "Se elimino con exito la familia " + oFlia.descripcion
                    Else
                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_del_err", context.Session("lang")))
                        oBita.Descripcion = "Error al eliminar la familia " + oFlia.descripcion
                    End If

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