Public Class BECloudServerPro
    Inherits BE.BECloudServer


    Public Sub New()
        Me.Id = 6
        Me.Memoria = 16
        Me.Qcpu = 4
    End Sub

    Public Overrides Sub addAdicional(addon As BEServicioAdicional)
        Me._srv_adicionales.Add(addon)
    End Sub
End Class
