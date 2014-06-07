Imports System.Web.Script.Serialization

Public Class repos
    Inherits System.Web.UI.Page
    Private _read As Boolean
    Public ReadOnly Property read
        Get
            Return _read
        End Get
    End Property
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            '' verifico si tiene acceso
            _read = Utilidades.getUtilidades().tieneAcceso("reportes", Session("flia"))

            '' si no tiene acceso
            If Not _read Then
                Response.Redirect("/", False)
                Exit Sub
            End If
        End If

    End Sub


    Public Function translate(ByVal ctrl_id As String)

        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class