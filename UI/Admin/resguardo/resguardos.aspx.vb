Public Class resguardos
    Inherits System.Web.UI.Page

    Private _read As Boolean
    Public ReadOnly Property read
        Get
            Return _read
        End Get
    End Property
    Private _write As Boolean
    Public ReadOnly Property write
        Get
            Return _write
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

            _read = Utilidades.getUtilidades().tieneAcceso("bkp", Master.user_permisos)
            _write = Utilidades.getUtilidades().tieneAcceso("restore", Master.user_permisos)

            '' si no tiene acceso
            If Not _read And Not _write Then
                Response.Redirect("/")
            End If

            Dim oInfraBkp As Infra.Backup = Infra.Backup.getBkp()
            _lista_bkps = oInfraBkp.filtrar(New BE.BEBackup)

        End If

    End Sub
    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Master.lang)
    End Function
End Class