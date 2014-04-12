Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim v As String = Session("auth")
        Dim b As Boolean = String.IsNullOrEmpty(Session("auth"))

        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
        End If

        Me.welcome_message.Text = "hola mundo!"
        Dim m As Label = Master.FindControl("menu_about")
        m.Text = "pepo"
    End Sub

End Class