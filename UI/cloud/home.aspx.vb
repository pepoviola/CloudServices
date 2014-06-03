Public Class home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
        ElseIf Not Session("flia_desc") = "cliente" Then
            Response.Redirect("/", False)
            Exit Sub
        End If

        ' busco los servers a vender
        'Dim disponibles As List(Of BE.BEServicioBase) = BLL.BLServicesFacade.getServicesFacade().ServiciosDisponibles


    End Sub


    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class