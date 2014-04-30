
Public Class Permiso
    Inherits PatenteBasica

    Public Overrides Function getListaCompleta() As System.Collections.Generic.List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        Return _lista
    End Function

    Public Overrides Function Patentes(ByVal oflia As BE.BEPatenteBasica) As System.Collections.Generic.List(Of BE.BEPatenteBasica)

        Dim _lista As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
        _lista.Add(oflia)
        Return _lista
    End Function


    'Public Overrides Function Validar(ByVal id As String, ByVal beFlia As BE.BEPatenteBasica) As Boolean

    'End Function
End Class
