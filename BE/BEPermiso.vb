
Public Class BEPermiso
    Inherits BEPatenteBasica





    Public Overrides Function Validar(ByVal id As String) As Boolean
        Return (id = Me.descripcion)
    End Function
End Class
