Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class generate_dv
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
                    'If (context.Request.Form.Get("type") = "dvh") Then
                    Dim infra_helper As Infra.helper_dv = New Infra.helper_dv
                    Dim res As String
                    res = infra_helper.generate_dvh_for_table(context.Request.Form.Get("table_name"))
                    'Else
                    'Dim infra_helper As Infra.helper_dv = New Infra.helper_dv
                    'Dim res As String
                    res = infra_helper.generate_dvv_for_table(context.Request.Form.Get("table_name"))
                    'End If

                    ' realizo el chequeo una vez mas
                    ' y actualizo la variable de app
                    Dim oInfraDVV As Infra.DVV = New Infra.DVV
                    Dim oInfraDVH As Infra.DVH = New Infra.DVH
                    Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
                    For Each tabla As String In New List(Of String) From {"Usuario", "Bitacora", "Familia"}
                        listaErrs.AddRange(oInfraDVH.check(tabla))
                        listaErrs.AddRange(oInfraDVV.check(tabla))
                    Next
                    context.Application.Set("listaErrs", listaErrs)
                    '
                    ' fin de chequeo  y actualizacion

                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("generate_ok", context.Session("lang")))
                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("generate_err", context.Session("lang")))

                End Try

                Dim oRes = jss.Serialize(resp)
                context.Response.Write(oRes)
                'HttpRuntime.UnloadAppDomain()

            End If
        End If



    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class