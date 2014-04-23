Public Class login
    Inherits System.Web.UI.Page

    'Dim alert_div As HtmlGenericControl
    'Public utils As Utilidades = Utilidades.getUtilidades()

    ''' <summary>
    '''  lleva la cuenta para registrar las veces que hizo login incorrecto
    ''' </summary>
    ''' <remarks></remarks>
    Private _login_err As Integer = 0
    ''' <summary>
    ''' keep track of user try to login
    ''' </summary>
    ''' <remarks></remarks>
    Private _last_user As String = ""

    Public ReadOnly Property login_err
        Get
            Return _login_err
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.RequestType = "POST" Then
            ' make login
            login(Request.Form("txt_login_username"), Request.Form("txt_login_passwd"))


        End If

        'Dim utils As Utilidades = Utilidades.getUtilidades()
        ''TODO: default lang
        'utils.translatePage(Page, 1)
        ''escondo el alert_div
        'alert_div = CType(Page.FindControl("alert_div"), HtmlGenericControl)
        'If Not alert_div Is Nothing Then
        '    alert_div.Visible = False
        'End If



    End Sub

    Protected Sub login(ByVal username As String, ByVal passwd As String)
        Dim oUsuario As BE.BEUsuario = New BE.BEUsuario()
        oUsuario.Username = username
        oUsuario.Passwd = passwd
        Dim oInfraUsuario As Infra.InfraUsuario = New Infra.InfraUsuario()
        Dim ret As Boolean = oInfraUsuario.validarCredenciales(oUsuario)
        If ret Then
            'fill the session object for this user
            Session("auth") = "OK"
            Session("username") = oUsuario.Username
            Session("lang") = oUsuario.Idioma.Id
            Session("lang_code") = oUsuario.Idioma.Codigo
            If Not String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                Response.Redirect(Request.QueryString("ReturnUrl"))
            Else
                Response.Redirect("/Default.aspx")
            End If
        Else

            If _last_user = username Then
                _login_err += 1
            Else
                _last_user = username
                _login_err = 1
            End If
        End If

    End Sub

    Public Function translate(ByVal ctrl_id As String, lang As Integer)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

End Class