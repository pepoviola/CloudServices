Public Class dvsErr
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Public Function translate(ByVal ctrl As String) As String
        Return Utilidades.getUtilidades().translate(ctrl, Session("lang"))
    End Function
End Class