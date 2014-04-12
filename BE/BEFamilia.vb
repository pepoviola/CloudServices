
Public Class BEFamilia
    Inherits BEPatenteBasica



    Private _patentes As List(Of BE.BEPatenteBasica)
    Public Property Patentes() As List(Of BE.BEPatenteBasica)
        Get
            Return _patentes
        End Get
        Set(ByVal value As List(Of BE.BEPatenteBasica))
            _patentes = value
        End Set
    End Property

    'overloads
    Public Sub New()

    End Sub
    Public Sub New(ByVal cod, ByVal descrip)
        Me.codigo = cod
        Me.descripcion = descrip
    End Sub


    Public Overrides Function Validar(ByVal id As String) As Boolean
        Dim retorno As Boolean
        For Each f As BEPatenteBasica In Me.Patentes
            If f.Validar(id) Then
                Return True
            End If
        Next
        Return retorno
    End Function
End Class
