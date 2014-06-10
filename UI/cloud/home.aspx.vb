Imports BE
Public Class home
    Inherits System.Web.UI.Page

    Private _servicios_contratados As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
    Public ReadOnly Property Servicios_contratados As List(Of BE.BEServicioBase)
        Get
            Return _servicios_contratados
        End Get
    End Property

    Private _msg_err As String = ""
    Public ReadOnly Property msg_err As String
        Get
            Return _msg_err
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim a As BE.BECloudServer = New BE.BECloudServerAdvance
        'Response.Write(a.GetType().FullName)
        'Response.Write("<br>")
        'Response.Write(a.GetType().AssemblyQualifiedName)

        'Dim t As Type = Type.GetType("BE.BECloudServerAdvance,BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
        'Dim srv As Object = Activator.CreateInstance(t)

        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            Exit Sub
        ElseIf Not Session("flia_desc") = "cliente" Then
            Response.Redirect("/", False)
            Exit Sub
        End If

        Try
            'obtengo el cliente de la session
            Dim blCli As BLL.BLLCliente = New BLL.BLLCliente()

            Dim oCli As BE.BECliente = New BE.BECliente
            Dim oFiltro As BE.BEUsuario = New BE.BEUsuario
            oFiltro.Id = Context.Session("user_id")
            oFiltro.Username = Context.Session("username")
            oCli = blCli.obtenerCliente(oFiltro)

            ' busco los servicios de este cliente

            _servicios_contratados = BLL.BLServicesFacade.getServicesFacade().obtenerServiciosDeCliente(oCli)

        Catch ex As ExceptionsPersonales.CustomException
            ' guardo en la bitacora
            'preparo bitacora
            Dim oBita As New BE.Bitacora
            Dim oBitaUser As New BE.BEUsuario
            oBitaUser.Id = Context.Session("user_id")
            oBita.Fecha = Date.Now
            oBita.Usuario = oBitaUser
            oBita.Categoria = "Exceptions"
            oBita.Descripcion = "Error al listar los servicios del cliente"
            'guardo en bitacora
            Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
            oInfraBita.Log(oBita)

            _msg_err = translate(ex.codigo)
        End Try

    End Sub


    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class