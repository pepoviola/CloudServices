'
' BE: Idioma

Public Class Idioma


    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
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


    Private _descripcion As String
    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    'tags

    Private _tags As List(Of Tag)
    Public Property Tags() As List(Of Tag)
        Get
            Return _tags
        End Get
        Set(ByVal value As List(Of Tag))
            _tags = value
        End Set
    End Property


    'overloads
    Public Sub New()

    End Sub
    Public Sub New(ByVal id As Integer, ByVal cod As String, ByVal descrip As String)
        Me.Id = id
        Me.Codigo = cod
        Me.Descripcion = descrip
    End Sub
    ' overrides

    Public Overrides Function ToString() As String
        Return String.Format("{0}", Me.Codigo)
    End Function

End Class
