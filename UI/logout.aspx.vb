Public Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' protect the page
            If String.IsNullOrEmpty(Session("auth")) Then
                FormsAuthentication.RedirectToLoginPage()
            Else
                Dim oInfra As Infra.Bitacora = Infra.Bitacora.getInfraBitacora
                Dim oBita As BE.Bitacora = New BE.Bitacora
                Dim oUser As BE.BEUsuario = New BE.BEUsuario()
                oUser.Username = Session("username")
                oUser.Id = Session("user_id")
                oBita.Usuario = oUser
                oBita.Fecha = Date.Now
                oBita.Categoria = "Logout"
                oBita.Descripcion = "El usuario cerro la session"
                oInfra.Log(oBita)
                Session.Abandon()
                Response.Redirect(FormsAuthentication.LoginUrl)
            End If

        Catch ex As ExceptionsPersonales.CustomException
            ' al ser logout no se lo muestro
            Session.Abandon()
            Response.Redirect(FormsAuthentication.LoginUrl)

        End Try
    End Sub

End Class