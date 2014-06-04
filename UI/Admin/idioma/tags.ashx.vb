Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

''' <summary>
''' public api for tags
''' 
''' get -> obtiene todos los tags de un idioma
'''     args : codelang in querystring
''' 
''' post -> modifica los tags
''' </summary>
''' <remarks></remarks>
Public Class tags
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If String.IsNullOrEmpty(context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()
        Else
            context.Response.ContentType = "application/json"
            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            Dim resp As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

            If context.Request.HttpMethod = "GET" Then
                'obtener los tags
                'genero la bitacora
                Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                Dim oBita As BE.Bitacora = New BE.Bitacora()
                Dim oUser As BE.BEUsuario = New BE.BEUsuario()
                oUser.Id = context.Session("user_id")
                oBita.Usuario = oUser
                oBita.Categoria = "Idioma"
                oBita.Fecha = Date.Now

                Try
                    Dim _tags As List(Of BE.Tag) = New List(Of BE.Tag)
                    Dim oInfraTag As Infra.Tag = Infra.Tag.getTagMgr()
                    Dim tag_for_filter As BE.Tag = New BE.Tag()
                    tag_for_filter.CodIdioma = context.Request.QueryString.Get("codelang")
                    _tags = oInfraTag.filtrar(tag_for_filter)
                    resp.Add("status", "200")
                    resp.Add("tags", _tags)

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("idioma_get_tags_err", context.Session("lang")))
                End Try

            ElseIf context.Request.HttpMethod = "POST" Then
                'tengo que guardar los tags
                'genero la bitacora
                Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                Dim oBita As BE.Bitacora = New BE.Bitacora()
                Dim oUser As BE.BEUsuario = New BE.BEUsuario()
                oUser.Id = context.Session("user_id")
                oBita.Usuario = oUser
                oBita.Categoria = "Idioma"
                oBita.Fecha = Date.Now
                Try

                    Dim idioma_code As String = context.Request.Form.Get("idioma_code")
                    Dim tags As List(Of BE.Tag) = New List(Of BE.Tag)
                    For Each ctrl As String In context.Request.Form
                        If Not ctrl = "idioma_code" Then
                            Dim oTag As BE.Tag = New BE.Tag()
                            oTag.Id = CInt(ctrl)
                            oTag.Leyenda = context.Request.Form.Get(ctrl)
                            oTag.CodIdioma = idioma_code
                            tags.Add(oTag)
                        End If
                    Next

                    'update
                    Dim oInfra As Infra.Tag = Infra.Tag.getTagMgr
                    If oInfra.modificarVarios(tags) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("idioma_mod_ok", context.Session("lang")))
                        oBita.Descripcion = "Se modificó con éxito el idioma: " + context.Request.Form.Get("idioma_code")
                    Else
                        ' log the error
                        resp.Add("status", "500")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("idioma_mod_err", context.Session("lang")))
                        oBita.Descripcion = "Error al modificar el idioma: " + context.Request.Form.Get("idioma_code")

                    End If
                Catch ex As Exception
                    ' log the error
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("idioma_mod_ex", context.Session("lang")))
                    oBita.Descripcion = "Exception al modificar el idioma: " + context.Request.Form.Get("idioma_code")
                Finally
                    oInfraBita.Log(oBita)
                End Try
            End If


            ' always
            Dim oRes = jss.Serialize(resp)
            context.Response.Write(oRes)

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class