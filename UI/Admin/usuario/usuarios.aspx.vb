Public Class usuarios
    Inherits System.Web.UI.Page

    Private _lista_usuarios As List(Of BE.BEUsuario) = New List(Of BE.BEUsuario)
    Public ReadOnly Property lista_usuarios As List(Of BE.BEUsuario)
        Get
            Return _lista_usuarios
        End Get
    End Property

    Private _lista_flias As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
    Public ReadOnly Property lista_flia As List(Of BE.BEPatenteBasica)
        Get
            Return _lista_flias
        End Get
    End Property

    Private _lista_idiomas As List(Of BE.Idioma) = New List(Of BE.Idioma)
    Public ReadOnly Property lista_idioma As List(Of BE.Idioma)
        Get
            Return _lista_idiomas
        End Get
    End Property

    Private _errores As String = ""
    Public ReadOnly Property errores As String
        Get
            Return _errores
        End Get
    End Property
    Private _read As Boolean
    Public ReadOnly Property read
        Get
            Return _read
        End Get
    End Property
    Private _write As Boolean
    Public ReadOnly Property write
        Get
            Return _write
        End Get
    End Property


    ''' <summary>
    ''' Si la session no existe lo redirigo al login form
    ''' Si no tiene acceso ( access denied ) lo redirigo al /
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()

        Else
            ' verifico si tiene acceso
            _read = Utilidades.getUtilidades().tieneAcceso("usuario_read", Master.user_permisos)
            _write = Utilidades.getUtilidades().tieneAcceso("usuario_write", Master.user_permisos)

            ' si no tiene acceso
            If Not _read And Not _write Then
                Response.Redirect("/")
            End If

            Try
                Dim oInfraUser As Infra.InfraUsuario = New Infra.InfraUsuario()
                _lista_usuarios = oInfraUser.Filtrar(New BE.BEUsuario())

                Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma
                _lista_idiomas = oInfraIdioma.Filtrar(New BE.Idioma())

                Dim oInfraFlias As Infra.Familia = New Infra.Familia()
                _lista_flias = oInfraFlias.getListaCompleta()

            Catch ex As ExceptionsPersonales.CustomException
                _errores = translate(ex.codigo)
            End Try

            End If

    End Sub
    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Master.lang)
    End Function
End Class