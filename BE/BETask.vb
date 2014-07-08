Public Class BETask
    Private _idTask As Integer
    Public Property IdTask() As Integer
        Get
            Return _idTask
        End Get
        Set(ByVal value As Integer)
            _idTask = value
        End Set
    End Property

    Private _task As Integer
    Public Property Task() As Integer
        Get
            Return _task
        End Get
        Set(ByVal value As Integer)
            _task = value
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

    Private _resultado As String
    Public Property Resultado() As String
        Get
            Return _resultado
        End Get
        Set(ByVal value As String)
            _resultado = value
        End Set
    End Property

    Private _server As BE.BECloudServer
    Public Property Server() As BE.BECloudServer
        Get
            Return _server
        End Get
        Set(ByVal value As BE.BECloudServer)
            _server = value
        End Set
    End Property







End Class
