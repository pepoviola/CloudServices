Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class cronJob
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Try
            BLL.BLTaskMgr.runJob()
            resp.Add("status", "200")

        Catch ex As Exception
            resp.Add("status", "500")
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