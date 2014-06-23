Public Class platform
    Inherits System.Web.UI.Page
    Private _read As Boolean
    Public ReadOnly Property readAccess
        Get
            Return _read
        End Get
    End Property
    Private _write As Boolean
    Public ReadOnly Property writeAccess
        Get
            Return _write
        End Get
    End Property

    Private _servers As List(Of BE.BEServerPlataforma) = New List(Of BE.BEServerPlataforma)
    Public ReadOnly Property listaServers As List(Of BE.BEServerPlataforma)
        Get
            Return _servers
        End Get
    End Property

    Private _err As String = String.Empty
    Public ReadOnly Property getErr As String
        Get
            Return _err
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            '' verifico si tiene acceso
            _read = Utilidades.getUtilidades().tieneAcceso("plataforma_read", Session("flia"))
            _write = Utilidades.getUtilidades().tieneAcceso("plataforma_write", Session("flia"))

            ' si no tiene acceso
            If Not _write Then
                If Not _read Then
                    Response.Redirect("/", False)
                    Exit Sub
                End If
            End If

            Dim oBLplat As BLL.BLServerPlataforma = New BLL.BLServerPlataforma()
            Dim oServer As BE.BEServerPlataforma = New BE.BEServerPlataforma()
            Try
                _servers = oBLplat.Filtrar(oServer)
            Catch ex As ExceptionsPersonales.CustomException
                _err = translate(ex.codigo)
            Finally
                oBLplat = Nothing
                oServer = Nothing
            End Try

        End If

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function
End Class