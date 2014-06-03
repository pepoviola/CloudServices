Public Class _Default
    Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'instancio el modulo de traducciones
        'Dim tradManager As Infra.TraductorMgr = New Infra.TraductorMgr()

        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
        ElseIf Session("flia_desc") = "cliente" Then
            ' lo mando al home de cliente
            Response.Redirect("/cloud/home.aspx", False)
            Exit Sub

        End If

        'fill fields
        'Dim menu_username As Literal = Master.FindControl("menu_username")
        'menu_username.Text = "Admin"

        'Dim menu_admin As HtmlGenericControl = Master.FindControl("menu_container_admin")
        'menu_admin.Visible = True
        'Me.welcome_message.Text = "hola mundo!"
        'Dim m As Label = Master.FindControl("menu_about")
        'm.Text = "pepo"
    End Sub
    Private _lang As Integer
    Public ReadOnly Property lang
        Get
            Return _lang
        End Get
    End Property
    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

    'Private Sub translateContentPage(ByVal ctrl As Control)
    '    For Each c As Control In ctrl.Controls
    '        If c.Controls.Count > 0 Then
    '            translateContentPage(c)
    '        Else
    '            If Not c.ID Is Nothing Or Not c.ClientID Is Nothing Then
    '                MsgBox(c.GetType().ToString() + " " + c.ID + " ")

    '            End If
    '        End If
    '    Next


    'End Sub


    'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    Dim utils As Utilidades = Utilidades.getUtilidades()

    '    For Each c As Control In Page.Controls
    '        'utils.
    '        translateContentPage(c) ', Session("lang"))
    '    Next
    'End Sub
End Class