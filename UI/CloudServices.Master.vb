Public Class CloudServices
    Inherits System.Web.UI.MasterPage

    Private _lang As Integer
    Public ReadOnly Property lang
        Get
            Return _lang
        End Get
    End Property

    Private _user_permisos As List(Of String) = New List(Of String)
    Public ReadOnly Property user_permisos As List(Of String)
        Get
            Return _user_permisos
        End Get
    End Property

    Protected Sub Page_init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        _user_permisos = Utilidades.getUtilidades().get_user_permisos(Session("flia"))
        _lang = CInt(Session("lang"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

    Public Function translate(ByVal ctrl_id As String) As String
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

    Public Function tieneAcceso(ByVal ctrl As String) As Boolean
        Return Utilidades.getUtilidades().tieneAcceso(ctrl, _user_permisos)
    End Function

End Class