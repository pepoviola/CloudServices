﻿Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization


Public Class cambiar_clave1
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' allow post only
        If context.Request.HttpMethod = "POST" Then
            Try
                Dim oUser As BE.BECliente = New BE.BECliente
                oUser = BLL.BLLCliente.getUserForReset(context.Request.Form.Get("ucode"))
                oUser.Passwd = context.Request.Form.Get("password")

                If BLL.BLLCliente.recuperarClave(oUser) Then
                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("user_mod_ok", context.Session("lang")))
                Else
                    resp.Add("status", "400")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("user_mod_err", context.Session("lang")))
                End If

            Catch ex As ExceptionsPersonales.CustomException
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl("user_mod_err", context.Session("lang")))
            End Try
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