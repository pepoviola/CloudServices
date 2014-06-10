Public Class recuperar
    Inherits System.Web.UI.Page

    Private _lista_preguntas As List(Of BE.BEPreguntaSecreta) = New List(Of BE.BEPreguntaSecreta)
    Public ReadOnly Property lista_pregs As List(Of BE.BEPreguntaSecreta)
        Get
            Return _lista_preguntas
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (String.IsNullOrEmpty(Session("lang"))) Then
            ' default
            Session("lang") = 1
        End If
        _lista_preguntas = Infra.InfraPreguntaSecreta.getPreguntaSercreta.Filtrar(New BE.BEPreguntaSecreta)
    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function
End Class