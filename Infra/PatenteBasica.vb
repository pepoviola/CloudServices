
Public MustInherit Class PatenteBasica

    Public MustOverride Function getListaCompleta() As List(Of BE.BEPatenteBasica)

    'Public MustOverride Function Validar(ByVal id As String, ByVal beFlia As BE.BEPatenteBasica) As Boolean

    Public MustOverride Function Patentes(ByVal oflia As BE.BEPatenteBasica) As List(Of BE.BEPatenteBasica)

End Class
