Public Class resguardos
    Inherits System.Web.UI.Page

    Private _bkpAccess As Boolean
    Public ReadOnly Property bkpAccess
        Get
            Return _bkpAccess
        End Get
    End Property
    Private _restoreAccess As Boolean
    Public ReadOnly Property restoreAccess
        Get
            Return _restoreAccess
        End Get
    End Property

    Private _lista_bkps As List(Of BE.BEBackup) = New List(Of BE.BEBackup)
    Public ReadOnly Property lista_bkps As List(Of BE.BEBackup)
        Get
            Return _lista_bkps
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Context.Session("auth")) Then
            ' redirect to login page
            FormsAuthentication.RedirectToLoginPage()
        Else
            '' verifico si tiene acceso

            _bkpAccess = Utilidades.getUtilidades().tieneAcceso("backup", Session("flia"))
            _restoreAccess = Utilidades.getUtilidades().tieneAcceso("restore", Session("flia"))

            '_bkpAccess = Utilidades.getUtilidades().tieneAcceso("bkp", Master.user_permisos)
            '_restoreAccess = Utilidades.getUtilidades().tieneAcceso("restore", Master.user_permisos)

            '' si no tiene acceso
            If Not _restoreAccess And Not _bkpAccess Then
                Response.Redirect("/", False)
                Exit Sub
            End If

            Dim oInfraBkp As Infra.Backup = Infra.Backup.getBkp()
            _lista_bkps = oInfraBkp.filtrar(New BE.BEBackup)

        End If

    End Sub
    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Master.lang)
    End Function
End Class