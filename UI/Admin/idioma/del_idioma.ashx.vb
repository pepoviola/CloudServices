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

        If String.IsNullOrEmpty(context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()
        Else

            context.Response.ContentType = "application/json"
            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            If context.Request.HttpMethod = "POST" And context.Request.Form.Get("idlang") Then
                ' chequeo el permiso

                'genero la bitacora
                Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                Dim oBita As BE.Bitacora = New BE.Bitacora()
                Dim oUser As BE.BEUsuario = New BE.BEUsuario()
                oUser.Id = context.Session("user_id")
                oBita.Usuario = oUser
                oBita.Categoria = "Idioma"
                oBita.Fecha = Date.Now

                Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma()
                Dim oIdioma As BE.Idioma = New BE.Idioma()
                oIdioma.Id = context.Request.Form.Get("idlang")
                Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
                Try
                    If oInfraIdioma.Eliminar(oIdioma) Then
                        resp.Add("status", "200")
                        resp.Add("idlang", oIdioma.Id.ToString())
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("idioma_delete_ok", context.Session("lang")))
                        oBita.Descripcion = "Se eliminio con exito el idioma: " + context.Request.Form.Get("codelang")
                    Else
                        ' deberia escribir en la bitacora
                        ' envio el error
                        resp.Add("status", "400")
                        resp.Add("idlang", oIdioma.Id.ToString())
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("idioma_delete_err", context.Session("lang")))
                        oBita.Descripcion = "Error al eliminar el idioma: " + context.Request.Form.Get("codelang")
                    End If
                    oInfraBita.Log(oBita)
                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))
                    oBita.Descripcion = "Exception al eliminar el idioma: " + context.Request.Form.Get("codelang")
                    oInfraBita.Log(oBita)
                End Try


                Dim oRes = jss.Serialize(resp)
                context.Response.Write(oRes)


            Else
                context.Response.Write("")
            End If

        End If



    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class