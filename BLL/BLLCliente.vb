Public Class BLLCliente

    Private frase As String = Configuration.ConfigurationManager.AppSettings("frase")

    Public Function Agregar(ByVal oCli As BE.BECliente) As Boolean
        Dim ret As Boolean


        Try
            'objeto dal
            Dim oCliDal As New DAL.DALCliente

            'hash pass
            oCli.Passwd = Criptografia.Crypto.getCrypto().generarMD5(oCli.Passwd)

            'encrypt mail
            oCli.Email = Criptografia.Crypto.getCrypto().CypherTripleDES(oCli.Email, frase, True)

            ret = (oCliDal.Agregar(oCli))

            If ret Then
                'update dvv
                If Not Infra.DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If

            End If
        Catch exCus As ExceptionsPersonales.CustomException
            Throw exCus
        Catch ex As Exception
            'ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrAgregarUsuario")
        End Try
        Return ret

    End Function
End Class
