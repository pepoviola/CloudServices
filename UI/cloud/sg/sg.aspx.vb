Imports System.Web.Script.Serialization
Public Class sg
    Inherits System.Web.UI.Page

    Private _sgs As List(Of BE.BEGrupoSeguridad) = New List(Of BE.BEGrupoSeguridad)
    Public ReadOnly Property sgs As List(Of BE.BEGrupoSeguridad)
        Get
            Return _sgs
        End Get
    End Property

    Private _sgs_serializado As String = String.Empty
    Public ReadOnly Property sgs_serializado As String
        Get
            Return _sgs_serializado
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            Exit Sub
        ElseIf Not Session("flia_desc") = "cliente" Then
            Response.Redirect("/", False)
            Exit Sub
        End If
        Dim blsg As BLL.BLLGrupoSeguridad = New BLL.BLLGrupoSeguridad()
        Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()
        Try

            'obtengo el cliente de la session


            Dim oCli As BE.BECliente = New BE.BECliente
            Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
            oFiltro.Id = Context.Session("user_id")
            oFiltro.Username = Context.Session("username")
            oCli = blCli.obtenerCliente(oFiltro)

            Dim oSG As BE.BEGrupoSeguridad = New BE.BEGrupoSeguridad
            oSG.Cliente = oCli
            _sgs = blsg.Filtrar(oSG)

            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            _sgs_serializado = jss.Serialize(_sgs)

        Catch ex As Exception
        Finally
            blsg = Nothing
            blCli = Nothing

        End Try
    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function
End Class