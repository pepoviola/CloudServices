Public Class CustomException
    Inherits Exception

    Private _codigo As String
    Public Property codigo() As String
        Get
            Return _codigo
        End Get
        Set(ByVal value As String)
            _codigo = value
        End Set
    End Property


    Sub New(ByVal codigo As String)
        Me.codigo = codigo
    End Sub

    Sub New(ByVal codigo As String, ByVal message As String)
        MyBase.New(message)
        Me.codigo = codigo
    End Sub

    Sub New(ByVal codigo As String, ByVal message As String, ByVal inner As Exception)
        MyBase.New(message, inner)
        Me.codigo = codigo
    End Sub

    'Sub new (message As String, innerException As 
End Class
