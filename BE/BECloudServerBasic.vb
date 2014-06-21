Public Class BECloudServerBasic
    Inherits BE.BECloudServer


    Sub New()
        Me.Id = 1
        Me.Memoria = 2
        Me.Qcpu = 1
    End Sub

    Public Overrides Sub addAdicional(addon As BEServicioAdicional)
        Me._srv_adicionales.Add(addon)
    End Sub
End Class
