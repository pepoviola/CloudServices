Public Class BECloudServerAdvance
    Inherits BE.BECloudServer



    Public Sub New()
        Me.Id = 2
    End Sub


    Public Overrides Sub addAdicional(addon As BEServicioAdicional)
        Me._srv_adicionales.Add(addon)
    End Sub
End Class
