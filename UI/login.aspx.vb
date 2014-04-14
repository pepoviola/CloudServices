Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    ''' <summary>
    ''' Handler login click
    ''' Creo un objeto usuario (BEUsuario)
    ''' Creo una instancia de InfraUsuario y llamo al metodo ValidarCredenciales
    ''' Genero la entrada en la bitácora con la accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub login_submit_Click(sender As Object, e As EventArgs) Handles login_submit.Click
        Dim oUsuario As BE.BEUsuario = New BE.BEUsuario()
        oUsuario.Username = txtUser.Text
        oUsuario.Passwd = txtPassword.Text
        Dim oInfraUsuario As Infra.InfraUsuario = New Infra.InfraUsuario()
        Dim ret As Boolean = oInfraUsuario.validarCredenciales(oUsuario)
        If ret Then
            'fill the session object for this user
            Session("auth") = "OK"
            If Not String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                Response.Redirect(Request.QueryString("ReturnUrl"))
            Else
                Response.Redirect("/Default.aspx")
            End If
        Else
            'como muestro user y pass incorrecto???

        End If


    End Sub
End Class