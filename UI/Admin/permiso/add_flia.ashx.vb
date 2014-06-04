Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

''' <summary>
'''  Agrega una nueva familia como permiso
''' 
''' </summary>
''' <remarks></remarks>
Public Class add_flia
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
                    Dim permisos As List(Of Integer) = New List(Of Integer)

                    For Each ctrl As String In context.Request.Form
                        If ctrl = "flia_code" Then
                            oFlia.descripcion = context.Request.Form.Get("flia_code")
                        Else
                            permisos.Add(ctrl)
                        End If
                    Next
                    'preparo bitacora
                    Dim oBita As New BE.Bitacora
                    Dim oBitaUser As New BE.BEUsuario
                    oBitaUser.Id = context.Session("user_id")
                    ' se pasa a la dal esta logica
                    'oBita.DVH = "todo"
                    oBita.Fecha = Date.Now
                    oBita.Usuario = oBitaUser
                    oBita.Categoria = "Familias"

                    Dim oInfraFlia As Infra.Familia = New Infra.Familia()
                    If oInfraFlia.addFamilia(oFlia, permisos) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_add_ok", context.Session("lang")))
                        oBita.Descripcion = "Se creó con éxito la familia " + oFlia.descripcion

                    Else
                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_add_err", context.Session("lang")))
                        oBita.Descripcion = "Error al crear la familia " + oFlia.descripcion
                    End If

                    'guardo en bitacora
                    Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                    oInfraBita.Log(oBita)

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_add_exception", context.Session("lang")))
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