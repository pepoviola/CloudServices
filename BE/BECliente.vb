Public Class BECliente
    Inherits BEUsuario

    Private _direccion As BE.BEDireccion
    Public Property Direccion() As BE.BEDireccion
        Get
            Return _direccion
        End Get
        Set(ByVal value As BE.BEDireccion)
            _direccion = value
        End Set
    End Property



End Class
