Public Class CloudServices
    Inherits System.Web.UI.MasterPage

    Private _lang As Integer
    Public ReadOnly Property lang
        Get
            Return _lang
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _lang = CInt(Session("lang"))
    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function
 
End Class