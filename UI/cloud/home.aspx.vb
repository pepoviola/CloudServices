Public Class home
    Inherits System.Web.UI.Page

    Private _servicios_contratados As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
    Public ReadOnly Property Servicios_contratados As List(Of BE.BEServicioBase)
        Get
            Return _servicios_contratados
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

        'obtengo el cliente de la session
        Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()

        Dim oCli As BE.BECliente = New BE.BECliente
        Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
        oFiltro.Id = Context.Session("user_id")
        oCli = blCli.obtenerCliente(oFiltro)

        ' busco los servicios de este cliente

        _servicios_contratados = BLL.BLServicesFacade.getServicesFacade().obtenerServiciosDeCliente(oCli)



    End Sub


    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class