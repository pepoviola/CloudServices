Public Class idiomas
    Inherits System.Web.UI.Page

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

    Private _langs As List(Of BE.Idioma) = New List(Of BE.Idioma)
    Public ReadOnly Property langs As List(Of BE.Idioma)
        Get
            Return _langs
        End Get
    End Property

    Private _tags As List(Of BE.Tag) = New List(Of BE.Tag)
    Public ReadOnly Property tags As List(Of BE.Tag)
        Get
            Return _tags
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            '' verifico si tiene acceso
            _read = Utilidades.getUtilidades().tieneAcceso("idioma_read", Master.user_permisos)
            _write = Utilidades.getUtilidades().tieneAcceso("idioma_write", Master.user_permisos)

            '' si no tiene acceso
            If Not _read And Not _write Then
                Response.Redirect("/")
            End If

            Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma()
            _langs = oInfraIdioma.Filtrar(New BE.Idioma())

            Dim oInfraTag As Infra.Tag = Infra.Tag.getTagMgr()
            Dim tag_for_filter As BE.Tag = New BE.Tag()
            tag_for_filter.CodIdioma = Session("lang_code")
            _tags = oInfraTag.filtrar(tag_for_filter)

        End If
    End Sub



    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Master.lang)
    End Function

End Class