
Public Class BEFamilia
    Inherits BEPatenteBasica
    Implements ICloneable


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
    Public Sub New(ByVal cod As Integer, ByVal descrip As String)
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

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim e As BEFamilia = New BEFamilia
        e = Me.MemberwiseClone()
        Return e
    End Function
End Class
