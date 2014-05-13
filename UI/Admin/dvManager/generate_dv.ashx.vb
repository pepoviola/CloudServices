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
                    Dim infra As Infra.helper_dv = New Infra.helper_dv
                    Dim res As String
                    res = infra.generate_dvh_for_table(context.Request.Form.Get("table_name"))
                Catch ex As Exception

                End Try

            End If
        End If



    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class