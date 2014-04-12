Public Class DV

    Public Shared Function create(ByVal campos As List(Of Object)) As String
        Dim dvh As String
        dvh = String.Empty
        For Each c As Object In campos
            dvh = dvh + Convert.ToString(c)
        Next
        Return Crypto.getCrypto().generarMD5(dvh)
    End Function
End Class
