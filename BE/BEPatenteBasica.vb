
Public MustInherit Class BEPatenteBasica


    Private _codigo As Integer
    Public Property codigo As Integer
        Get
            Return _codigo
        End Get
        Set(ByVal value As Integer)
            _codigo = value
        End Set
    End Property



    Private _descripcion As String
    Public Property descripcion As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property


    ' no es una propiedad de la clase
    ' hago el calculo en la dal

    'Private _dvh As String
    'Public Property DVH() As String
    '    Get
    '        Return _dvh
    '    End Get
    '    Set(ByVal value As String)
    '        _dvh = value
    '    End Set
    'End Property


    Private _nativo As Integer
    Public Property Nativo() As Integer
        Get
            Return _nativo
        End Get
        Set(ByVal value As Integer)
            _nativo = value
        End Set
    End Property



    Public MustOverride Function Validar(ByVal id As String) As Boolean

    'overloads
    Public Sub New()

    End Sub
    Public Sub New(ByVal cod, ByVal descrip)
        Me.codigo = cod
        Me.descripcion = descrip
    End Sub


    Public Overrides Function ToString() As String
        Return String.Format("{0} ({1})", Me.descripcion, Me.codigo)
        'Return Me.Descripcion + " (" + Me.Id + ")"
    End Function

    




End Class
