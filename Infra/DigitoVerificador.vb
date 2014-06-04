
Public MustInherit Class DigitoVerificador
    Public MustOverride Function check(ByVal tabla As String) As List(Of Dictionary(Of String, String))

    Public Shared Function verificarDigitos() As List(Of Dictionary(Of String, String))
        ' chequeo de digitos verificadores
        Dim oInfraDVV As Infra.DVV = New Infra.DVV
        Dim oInfraDVH As Infra.DVH = New Infra.DVH
        Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        For Each tabla As String In New List(Of String) From {"Usuario", "Bitacora", "Familia"}
            listaErrs.AddRange(oInfraDVH.check(tabla))
            listaErrs.AddRange(oInfraDVV.check(tabla))
        Next
        Return listaErrs

    End Function
End Class
