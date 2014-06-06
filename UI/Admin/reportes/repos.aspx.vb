Public Class repos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ahora As DateTime = DateTime.Now.AddMonths(-1)
        Response.Write(ahora.Month)

        Dim r As BE.BEReporte = New BE.BEReporte()
        Dim bllRepo As BLL.BLMgrReporte = New BLL.BLMgrReporte()
        r = bllRepo.CrearReporteProyeccion()

    End Sub

End Class