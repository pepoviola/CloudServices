Public Class BECloudServerPro
    Inherits BE.BECloudServer


    Public Sub New()
        Me.Id = 6
    End Sub

    Public Overrides Sub addAdicional(addon As BEServicioAdicional)
        Me._srv_adicionales.Add(addon)
    End Sub
End Class
