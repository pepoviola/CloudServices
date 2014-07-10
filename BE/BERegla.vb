Public Class BERegla
    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _origen As String
    Public Property Origen() As String
        Get
            Return _origen
        End Get
        Set(ByVal value As String)
            _origen = value
        End Set
    End Property

    Private _ptoDestino As String
    Public Property PtoDestino() As String
        Get
            Return _ptoDestino
        End Get
        Set(ByVal value As String)
            _ptoDestino = value
        End Set
    End Property

    Private _regla As String
    Public Property Regla() As String
        Get
            Return _regla
        End Get
        Set(ByVal value As String)
            _regla = value
        End Set
    End Property




End Class
