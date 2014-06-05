Public Class generar_entorno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
        ElseIf Not Session("flia_desc") = "cliente" Then
            Response.Redirect("/", False)
            Exit Sub
        End If
    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class