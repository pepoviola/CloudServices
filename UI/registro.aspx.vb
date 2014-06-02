Public Class registro
    Inherits System.Web.UI.Page

    Private _lista_idiomas As List(Of BE.Idioma) = New List(Of BE.Idioma)
    Public ReadOnly Property lista_idioma As List(Of BE.Idioma)
        Get
            Return _lista_idiomas
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (String.IsNullOrEmpty(Session("lang"))) Then
            ' default
            Session("lang") = 1
        End If

        Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma
        _lista_idiomas = oInfraIdioma.Filtrar(New BE.Idioma())

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function
End Class