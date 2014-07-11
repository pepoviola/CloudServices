Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class config_vm_fw
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Or Not context.Session("flia_desc") = "cliente" Then

        Else
            ' only alow post
            If context.Request.HttpMethod = "POST" Then
                Try
                    ' create cloud server
                    Dim oServer As BE.BECloudServerBasic = New BE.BECloudServerBasic
                    oServer.gruposSeguridad = New List(Of BE.BEGrupoSeguridad)
                    'loop params
                    For Each ctrl As String In context.Request.Form
                        If Not ctrl = "sid" Then
                            Dim oSG As BE.BEGrupoSeguridad = New BE.BEGrupoSeguridad
                            oSG.Id = CInt(ctrl)
                            oServer.gruposSeguridad.Add(oSG)
                        Else
                            oServer.Id = context.Request.Form.Get("sid")
                        End If
                    Next
                    Dim oBL As BLL.BLServicesFacade = BLL.BLServicesFacade.getServicesFacade()
                    If oBL.ConfigVMGrupoSeg(oServer) Then
                        'bitacora
                        'preparo bitacora
                        Dim oBita As New BE.Bitacora
                        Dim oBitaUser As New BE.BEUsuario
                        oBitaUser.Id = context.Session("user_id")
                        oBita.Fecha = Date.Now
                        oBita.Usuario = oBitaUser
                        oBita.Categoria = "Grupos Seguridad"
                        oBita.Descripcion = String.Format("El usuario {0}  configuró los grupos de seguridad a la vm-{1}", context.Session("Username"), oServer.Id)
                        'guardo en bitacora
                        Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                        oInfraBita.Log(oBita)

                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("sg_config_ok", context.Session("lang")))
                    End If

                Catch ex As ExceptionsPersonales.CustomException
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("sg_config_err", context.Session("lang")))
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