Public Class BEEvento

    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _evento As String
    Public Property Evento() As String
        Get
            Return _evento
        End Get
        Set(ByVal value As String)
            _evento = value
        End Set
    End Property

    Private _descrip As String
    Public Property Descripcion() As String
        Get
            Return _descrip
        End Get
        Set(ByVal value As String)
            _descrip = value
        End Set
    End Property

    Private _fecha As String
    Public Property Fecha() As String
        Get
            Return _fecha
        End Get
        Set(ByVal value As String)
            _fecha = value
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
