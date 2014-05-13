Public Class dvs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()

        Else
            ' verifico si tiene acceso
            Dim _read As Boolean = Utilidades.getUtilidades().tieneAcceso("dv_mgr", Master.user_permisos)


            ' si no tiene acceso
            If Not _read Then
                Response.Redirect("/")
            End If
        End If

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Master.translate(ctrl_id)
    End Function

End Class