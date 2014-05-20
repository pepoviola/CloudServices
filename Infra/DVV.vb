
Public Class DVV
    Inherits DigitoVerificador


    Public Overrides Function check(ByVal tabla As String) As List(Of Dictionary(Of String, String))
        Dim lista As New List(Of Dictionary(Of String, String))
        Try
            'get md5 from tables
            Dim dvv As String = Criptografia.Crypto.getCrypto.generarMD5(DAL.DVManager.getTableDVHs(tabla))
            'now we have to compare with dvv table
            Dim dvvInTable As String = DAL.DVManager.getDVV(tabla)
            If Not dvv.Equals(dvvInTable) Then
                'lista.Add(New Dictionary(Of String, String) From {{String.Format("Err DVV "), _
                '                                                   String.Format("Tabla: {0}", tabla)}})
                lista.Add(New Dictionary(Of String, String) From {{String.Format("DVV "), _
                                                                   String.Format("{0}", tabla)}})
            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return lista
    End Function

    Public Shared Function Actualizar(ByVal tabla As String) As Boolean
        Dim ret As Boolean
        Try
            'get md5 from tables
            Dim dvv As String = Criptografia.Crypto.getCrypto.generarMD5(DAL.DVManager.getTableDVHs(tabla))
            'and save
            ret = DAL.DVManager.saveDVV(tabla, dvv)
        Catch ex As Exception
            Throw ex
        End Try
        Return ret

    End Function
End Class
