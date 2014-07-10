Public Class BEGrupoSeguridad
    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property


    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _fechaIn As DateTime
    Public Property FechaIn() As DateTime
        Get
            Return _fechaIn
        End Get
        Set(ByVal value As DateTime)
            _fechaIn = value
        End Set
    End Property

    Private _fechaOut As DateTime
    Public Property FechaOut() As DateTime
        Get
            Return _fechaOut
        End Get
        Set(ByVal value As DateTime)
            _fechaOut = value
        End Set
    End Property

    Private _cliente As BE.BECliente
    Public Property Cliente() As BE.BECliente
        Get
            Return _cliente
        End Get
        Set(ByVal value As BE.BECliente)
            _cliente = value
        End Set
    End Property

    Private _reglas
    Public Property Reglas() As List(Of BE.BERegla)
        Get
            Return _reglas
        End Get
        Set(ByVal value As List(Of BE.BERegla))
            _reglas = value
        End Set
    End Property





End Class
