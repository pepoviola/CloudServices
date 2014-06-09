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

    Private _servicios As List(Of BE.BEServicioBase)
    Public Property servicios() As List(Of BE.BEServicioBase)
        Get
            Return _servicios
        End Get
        Set(ByVal value As List(Of BE.BEServicioBase))
            _servicios = value
        End Set
    End Property




End Class
