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

    Public Function obtenerCliente(ByVal oUser As BE.BEUsuario) As BE.BECliente
        Dim oCli As BE.BECliente = DirectCast(oUser, BE.BECliente)


        Dim lista As List(Of BE.BECliente) = New List(Of BE.BECliente)
        Try
            'objeto dal
            Dim oCliDal As New DAL.DALCliente
            lista = oCliDal.Filtrar(oCli)
            'retorno siempre el primero
            oCli = lista.First()

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrFiltroCliente")
        End Try
        Return oCli

    End Function
End Class
