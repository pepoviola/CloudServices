Imports System.Web.Script.Serialization

Public Class detalles_vm
    Inherits System.Web.UI.Page

    Private _cli As BE.BECliente

    Private _eventos As List(Of BE.BEEvento) = New List(Of BE.BEEvento)
    Public ReadOnly Property Eventos As List(Of BE.BEEvento)
        Get
            Return _eventos
        End Get
    End Property
    Private _msg_err As String = ""
    Public ReadOnly Property msg_err As String
        Get
            Return _msg_err
        End Get
    End Property
    Private _vm As BE.BECloudServer
    Public ReadOnly Property VM As BE.BECloudServer
        Get
            Return _vm
        End Get
    End Property

    Private _vm_serializada As String
    Public ReadOnly Property VM_serialziada As String
        Get
            Return _vm_serializada
        End Get
    End Property


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

    Private Function checkAccess() As Boolean
        Try
            'obtengo el cliente de la session
            Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()
            Dim oBL As BLL.BLServicesFacade = BLL.BLServicesFacade.getServicesFacade()

            Dim oCli As BE.BECliente = New BE.BECliente
            Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
            oFiltro.Id = Context.Session("user_id")
            oFiltro.Username = Context.Session("username")
            oCli = blCli.obtenerCliente(oFiltro)
            _cli = oCli
            Return oBL.tieneAccesoVM(oCli, Request.QueryString.Get("id"))

        Catch ex As Exception
            Return False
        End Try

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            'FormsAuthentication.RedirectToLoginPage()
            Response.Redirect("/login.aspx", False)
            Exit Sub
        Else
            If (String.IsNullOrEmpty(Request.QueryString.Get("id")) Or Not checkAccess()) Then
                Response.Redirect("/cloud/home.aspx", False)
                Exit Sub
            End If
        End If
        Dim blsg As BLL.BLLGrupoSeguridad = New BLL.BLLGrupoSeguridad()
        Try
            ' tengo que buscar los eventos de la vm
            Dim filtro As BE.BEEvento = New BE.BEEvento
            filtro.Server = New BE.BECloudServerBasic
            filtro.Server.Id = Request.QueryString.Get("id")
            _eventos = BLL.BLEvento.getEventosVM(filtro)

            ' busco la vm
            Dim oBL As BLL.BLServicesFacade = BLL.BLServicesFacade.getServicesFacade()
            Dim _vms As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
            _vms = oBL.obtenerServiciosDeCliente(_cli)
            _vm = _vms.Find(Function(x) x.Id.ToString() = Request.QueryString.Get("id"))

            Dim oSG As BE.BEGrupoSeguridad = New BE.BEGrupoSeguridad
            oSG.Cliente = _cli
            _sgs = blsg.Filtrar(oSG)

            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            _sgs_serializado = jss.Serialize(_sgs)
            _vm_serializada = jss.Serialize(_vm)
            'obtengo creds
            'Dim creds As Dictionary(Of String, String)
            'creds = BLL.vSphereProxy.getCreds(Request.QueryString.Get("id"))
            '_mor = creds.Item("mor")
            '_tt = creds.Item("tt")
            ' los sg disponibles
            ' la ip
            ' y mostrarlos

        Catch ex As ExceptionsPersonales.CustomException
            _msg_err = translate(ex.codigo)
        End Try
    End Sub
    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class