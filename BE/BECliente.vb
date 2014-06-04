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

    Private _id_cliente As Integer
    Public Property ClienteId() As Integer
        Get
            Return _id_cliente
        End Get
        Set(ByVal value As Integer)
            _id_cliente = value
        End Set
    End Property




End Class
