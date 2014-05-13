
Public Class DVH
    Inherits DigitoVerificador



    Public Overrides Function check(ByVal tabla As String) As List(Of Dictionary(Of String, String))

        Dim lista As New List(Of Dictionary(Of String, String))
        Dim listaString As New Dictionary(Of Integer, Dictionary(Of String, String))
        'me traigo toda la tabla y la recorro
        'si el dvh es erroneo guardo el id en la lista
        '
        'me traigo un lista de strnings desde la dal
        listaString = DAL.DVManager.getTableStrings(tabla)

        'debo recorrer y verificar la lista
        Dim pair As KeyValuePair(Of Integer, Dictionary(Of String, String))
        For Each pair In listaString

            'Dim n As String = pair.Value.Keys(0)
            Dim n As String = Criptografia.Crypto.getCrypto().generarMD5(pair.Value.Keys(0))
            'CType(pair.Value, KeyValuePair(Of String, String)))
            If Not pair.Value.Item(pair.Value.Keys(0)).Equals(n) Then
                'erro en dvh
                lista.Add(New Dictionary(Of String, String) From {{String.Format("Tabla: {0}", tabla), _
                                                                   String.Format("ID: {0}", pair.Key)}})
            End If
        Next
        Return lista
    End Function


    Function createDVH(ByVal campos As List(Of Object)) As String
        Dim dvh As String
        dvh = String.Empty
        For Each c As Object In campos
            dvh = dvh + Criptografia.Crypto.getCrypto().generarMD5(Convert.ToString(c))
        Next
        Return dvh
    End Function
    
End Class
