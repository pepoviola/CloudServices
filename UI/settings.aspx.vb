Public Class settings
    Inherits System.Web.UI.Page

    Private _lista_idiomas As List(Of BE.Idioma) = New List(Of BE.Idioma)
    Public ReadOnly Property lista_idioma As List(Of BE.Idioma)
        Get
            Return _lista_idiomas
        End Get
    End Property

    Private _msg As String
    Public ReadOnly Property msg As String
        Get
            Return _msg
        End Get
    End Property

    Private _msg_type As String
    Public ReadOnly Property msg_type As String
        Get
            Return _msg_type
        End Get
    End Property

    Private _user_session As BE.BEUsuario = New BE.BEUsuario
    Public ReadOnly Property user_session As BE.BEUsuario
        Get
            Return _user_session
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()

        Else


            Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma
            _lista_idiomas = oInfraIdioma.Filtrar(New BE.Idioma())
            'obtengo el usuario de la session
            Dim oUserInfra As Infra.InfraUsuario = New Infra.InfraUsuario
            _user_session.Username = Session("username")
            _user_session = oUserInfra.Filtrar(_user_session).First()

            If Request.HttpMethod = "POST" Then
                Dim oBita As New BE.Bitacora
                Try
                    _user_session.Nombre = Context.Request.Form.Get("nombre")
                    _user_session.Apellido = Context.Request.Form.Get("apellido")
                    _user_session.Email = Context.Request.Form.Get("email")
                    _user_session.Passwd = Context.Request.Form.Get("password")
                    _user_session.Idioma = New BE.Idioma()
                    _user_session.Idioma.Id = Context.Request.Form.Get("idioma")


                    'preparo bitacora

                    oBita.DVH = "todo"
                    oBita.Fecha = Date.Now
                    oBita.Usuario = _user_session
                    oBita.Categoria = "Usuarios"

                    If oUserInfra.Modificar(_user_session) Then
                        Master.lang = Request.Form.Get("idioma")
                        Session("lang") = Request.Form.Get("idioma")
                        For Each temp_idioma As BE.Idioma In lista_idioma
                            If temp_idioma.Id = _user_session.Idioma.Id Then
                                Session("lang_code") = temp_idioma.Codigo
                                Exit For
                            End If
                        Next
                        _msg = translate("update_settings_ok")
                        _msg_type = "info"
                        oBita.Descripcion = "Se modifcó con exito la configuración del usuario " + _user_session.Username

                    Else
                        _msg = translate("update_settings_err")
                        _msg_type = "error"
                        oBita.Descripcion = "error al modifcar la configuración del usuario " + _user_session.Username
                    End If
                Catch ex As ExceptionsPersonales.CustomException
                    _msg = translate(ex.codigo)
                    _msg_type = "error"
                    oBita.Descripcion = "Ex al modificar la configuración del usuario " + _user_session.Username
                End Try

            End If

        End If

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Master.lang)
    End Function
End Class