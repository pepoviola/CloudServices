Imports System.Web.Script.Serialization

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

    Private _lista_idiomas As List(Of BE.Idioma) = New List(Of BE.Idioma)
    Public ReadOnly Property lista_idioma As List(Of BE.Idioma)
        Get
            Return _lista_idiomas
        End Get
    End Property

    Private _sistemaEnError As Boolean
    Public ReadOnly Property sistemaEnError
        Get
            Return _sistemaEnError
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

       
        Dim oInfraIdioma As Infra.Idioma = Infra.Idioma.getIdioma
        _lista_idiomas = oInfraIdioma.Filtrar(New BE.Idioma())


        ' lang by querystring
        If Not (String.IsNullOrEmpty(Request.QueryString("lang"))) Then
            ' cheque si existe el idioma
            For Each i As BE.Idioma In lista_idioma
                If i.Id = Request.QueryString("lang") Then
                    Session("lang") = i.Id
                    'Dim cookie As HttpCookie = New HttpCookie("lang", i.Id)


                    Exit For
                End If
            Next

            'Else
            '    If (String.IsNullOrEmpty(Session("lang"))) Then
            '        Session("lang") = 1
            '        'Dim cookie As HttpCookie = New HttpCookie("lang", "1")
            '    End If
        End If

        If (String.IsNullOrEmpty(Session("lang"))) Then
            '    ' default
            Session("lang") = 1
            '    'Dim cookie As HttpCookie = New HttpCookie("lang", "1")
        End If

        ''Response.Cookies.Add(cookie)

        Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        listaErrs = Application("listaErrs")
        If listaErrs.Count > 0 Then
            _sistemaEnError = True
        Else
            _sistemaEnError = False
        End If

        If Request.RequestType = "POST" Then
            ' make login
            login(Request.Form("txt_login_username"), Request.Form("txt_login_passwd"))
        End If



    End Sub

    Protected Sub login(ByVal username As String, ByVal passwd As String)
        Dim oUsuario As BE.BEUsuario = New BE.BEUsuario()
        Try
            oUsuario.Username = username
            oUsuario.Passwd = passwd
            Dim oInfraUsuario As Infra.InfraUsuario = New Infra.InfraUsuario()
            Dim ret As Boolean = oInfraUsuario.validarCredenciales(oUsuario)
            If ret Then
                'fill the session object for this user
                Session("auth") = "OK"
                Session("username") = oUsuario.Username
                Session("user_id") = oUsuario.Id
                Session("lang") = oUsuario.Idioma.Id
                Session("lang_code") = oUsuario.Idioma.Codigo
                Session("flia") = oUsuario.Patente.codigo
                Session("flia_desc") = oUsuario.Patente.descripcion
                'Dim oInfraDVV As Infra.DVV = New Infra.DVV
                'Dim oInfraDVH As Infra.DVH = New Infra.DVH
                'Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
                'For Each tabla As String In New List(Of String) From {"Usuario", "Bitacora", "Familia"}
                'listaErrs.AddRange(oInfraDVH.check(tabla))
                'listaErrs.AddRange(oInfraDVV.check(tabla))
                'Next


                Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
                listaErrs = Application("listaErrs")
                Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
                Session("dvErrs") = jss.Serialize(listaErrs)


                Dim a As String = Session("dvErrs")

                ' if l is bigger than 0
                ' we have an issue with the dvs
                If listaErrs.Count > 0 Then
                    ' errores de validacion
                    ' si el usuario tiene el permiso lo redirecciono
                    ' sino cierro la session y lo mando a login 
                    ' con el mensaje de error correspondiente
                    If Utilidades.getUtilidades().tieneAcceso("dv_mgr", Session("flia")) Then
                        Response.Redirect("/dvsErr.aspx", False)
                        ' control with exit sub
                        ' http://msdn.microsoft.com/en-us/library/a8wa7sdt.aspx
                        '
                        Exit Sub
                    Else
                        ' corto la session y mando el mensaje en una nueva
                        ' Session.Abandon()
                        Session("auth") = Nothing
                        Session("_msg_dv_err") = translate("errores_en_dvs")
                        Response.Redirect("/login.aspx", False)
                        ' control with exit sub
                        ' http://msdn.microsoft.com/en-us/library/a8wa7sdt.aspx
                        '
                        Exit Sub
                    End If

                End If


                If Not String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                    Response.Redirect(Request.QueryString("ReturnUrl"), False)
                Else
                    Response.Redirect("/Default.aspx", False)
                End If
            Else

                If Session("_last_user") = username Then
                    Session("_login_err") = Session("_login_err") + 1
                Else
                    Session("_last_user") = username
                    Session("_login_err") = 1
                End If
            End If

        Catch ex As Exception
            'show system error

        End Try

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

    Public Function translate(ByVal ctrl_id As String, lang As Integer)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

End Class