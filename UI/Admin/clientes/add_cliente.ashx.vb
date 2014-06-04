Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class add_cliente
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "application/json"
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
       
            ' only allow post
            If context.Request.HttpMethod = "POST" Then
            Try

                'create user
                Dim oCli As BE.BECliente = New BE.BECliente
                oCli.Username = context.Request.Form.Get("username")
                oCli.Nombre = context.Request.Form.Get("nombre")
                oCli.Apellido = context.Request.Form.Get("apellido")
                oCli.Email = context.Request.Form.Get("email")
                oCli.Passwd = context.Request.Form.Get("password")
                oCli.Estado = 1 ' meens Activo
                oCli.Patente = New BE.BEFamilia()
                oCli.Patente.codigo = 47 ' cliente
                oCli.Idioma = New BE.Idioma()
                oCli.Idioma.Id = context.Request.Form.Get("idioma")
                oCli.Direccion = New BE.BEDireccion
                oCli.Direccion.Calle = context.Request.Form.Get("calle")
                oCli.Direccion.Numero = context.Request.Form.Get("numero")
                oCli.Direccion.Localidad = context.Request.Form.Get("localidad")

                'preparo bitacora
                Dim oBita As New BE.Bitacora
                Dim oBitaUser As New BE.BEUsuario
                oBitaUser.Id = 1 ' system context.Session("user_id")

                oBita.Fecha = Date.Now
                oBita.Usuario = oBitaUser
                oBita.Categoria = "Clientes"

                Dim oInfra As BLL.BLLCliente = New BLL.BLLCliente
                If oInfra.Agregar(oCli) Then
                   
                    resp.Add("status", "200")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("cli_add_ok", context.Session("lang")))
                    oBita.Descripcion = "Se creó con éxito el cliente " + oCli.Username
                   

                Else
                    resp.Add("status", "400")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("user_add_err", context.Session("lang")))
                    oBita.Descripcion = "Error al crear el usuario " + oCli.Username
                End If
                'guardo en bitacora
                Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                oInfraBita.Log(oBita)


            Catch ex As ExceptionsPersonales.CustomException
                resp.Add("status", "500")
                resp.Add("msg", Infra.TraductorMgr.TraducirControl(ex.codigo, context.Session("lang")))
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