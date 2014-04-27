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

        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        Try
            If context.Request.HttpMethod = "GET" Then
                'obtener los tags
                Dim _tags As List(Of BE.Tag) = New List(Of BE.Tag)
                Dim oInfraTag As Infra.Tag = Infra.Tag.getTagMgr()
                Dim tag_for_filter As BE.Tag = New BE.Tag()
                tag_for_filter.CodIdioma = context.Request.QueryString.Get("codelang")
                _tags = oInfraTag.filtrar(tag_for_filter)
                resp.Add("status", "200")
                resp.Add("tags", _tags)

            ElseIf context.Request.HttpMethod = "POST" Then
                'tengo que guardar los tags
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
                    resp.Add("msg", "")
                Else
                    ' log the error
                    resp.Add("status", "500")
                    resp.Add("msg", "")

                End If
            End If



        Catch ex As Exception
            ' log the error
            resp.Add("status", "500")
            resp.Add("msg", "")
        End Try

        Dim oRes = jss.Serialize(resp)
        context.Response.Write(oRes)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class