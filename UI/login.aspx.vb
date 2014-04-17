Public Class login
    Inherits System.Web.UI.Page

    Dim alert_div As HtmlGenericControl
    Public utils As Utilidades = Utilidades.getUtilidades()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 

        Dim utils As Utilidades = Utilidades.getUtilidades()
        ''TODO: default lang
        'utils.translatePage(Page, 1)
        ''escondo el alert_div
        'alert_div = CType(Page.FindControl("alert_div"), HtmlGenericControl)
        'If Not alert_div Is Nothing Then
        '    alert_div.Visible = False
        'End If

 

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
        oUsuario.Username = txt_login_username.Text
        oUsuario.Passwd = txt_login_passwd.Text
        Dim oInfraUsuario As Infra.InfraUsuario = New Infra.InfraUsuario()
        Dim ret As Boolean = oInfraUsuario.validarCredenciales(oUsuario)
        If ret Then
            'fill the session object for this user
            Session("auth") = "OK"
            Session("username") = oUsuario.Username
            Session("lang") = oUsuario.Idioma.Id
            If Not String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                Response.Redirect(Request.QueryString("ReturnUrl"))
            Else
                Response.Redirect("/Default.aspx")
            End If
        Else

            If Not alert_div Is Nothing Then
                alert_div.Visible = True
            End If
            'focus the textbox
            txt_login_username.Focus()
        End If


    End Sub


    Public Function translate(ByVal ctrl_id As String, lang As Integer)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

End Class