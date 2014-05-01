Public Class add_familia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oInfraFlia As Infra.Familia = New Infra.Familia()
        Dim listaPatentes As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
        listaPatentes = oInfraFlia.getListaCompleta()
        TreeView1.DataSource = listaPatentes

    End Sub

End Class