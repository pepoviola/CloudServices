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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()
        Else
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
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function
End Class