
'
' BE :: TAG

Public Class Tag

    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property


    Private _codigo As String
    Public Property Codigo() As String
        Get
            Return _codigo
        End Get
        Set(ByVal value As String)
            _codigo = value
        End Set
    End Property


    Private _dvh As String
   


    Private _codIdioma As String
    Public Property CodIdioma() As String
        Get
            Return _codIdioma
        End Get
        Set(ByVal value As String)
            _codIdioma = value
        End Set
    End Property


    Private _leyenda As String
    Public Property Leyenda() As String
        Get
            Return _leyenda
        End Get
        Set(ByVal value As String)
            _leyenda = value
        End Set
    End Property





End Class
