﻿Public Class BLLCliente

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
        Dim oCli As BE.BECliente = New BE.BECliente
        Dim oFiltro As BE.BECliente = New BE.BECliente
        oFiltro.Id = oUser.Id
        oFiltro.Nombre = oUser.Nombre
        oFiltro.Apellido = oUser.Apellido
        oFiltro.Username = oUser.Username
        oFiltro.Email = oUser.Email



        Dim lista As List(Of BE.BECliente) = New List(Of BE.BECliente)
        Try
            'objeto dal
            Dim oCliDal As New DAL.DALCliente
            lista = oCliDal.Filtrar(oFiltro)
            'retorno siempre el primero
            oCli = lista.First()

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrFiltroCliente")
        End Try
        Return oCli

    End Function

    Public Function resetClave(ByVal oFiltro As BE.BECliente) As Boolean
        Dim ret As Boolean = True
        Try
            Dim oCli As BE.BECliente = obtenerCliente(oFiltro)
            oCli.PregSecreta = oFiltro.PregSecreta
            'encrypt mail
            oCli.Email = Criptografia.Crypto.getCrypto().CypherTripleDES(oFiltro.Email, frase, True)
            Dim oDAL As DAL.DALCliente = New DAL.DALCliente()
            Dim url As String = oDAL.resetClave(oCli)
            If Not url = "" Then

                'preparo bitacora
                Dim oBita As New BE.Bitacora
                Dim oBitaUser As New BE.BEUsuario
                oBitaUser.Id = 1 ' system
                oBita.Fecha = Date.Now
                oBita.Usuario = oBitaUser
                oBita.Categoria = "Reset clave"
                oBita.Descripcion = "El sistema generó la url de recupero para el cliente " + oCli.Nombre
                'guardo en bitacora
                Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                oInfraBita.Log(oBita)

                Dim oDvh As Infra.helper_dv = New Infra.helper_dv
                oDvh.generate_dvh_for_table("Usuario")
                'update dvv
                If Not Infra.DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If

                Return True

            Else
                Return False

            End If

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ERR_reset")
        End Try
        Return ret
    End Function

    Public Shared Function getUserForReset(ByVal url As String) As BE.BECliente
        Dim oUser As BE.BECliente = New BE.BECliente
        Try
            oUser = DAL.DALCliente.getUserForReset(url)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ERR_url")
        End Try
        Return oUser
    End Function


    Public Shared Function recuperarClave(ByVal oCli As BE.BECliente) As Boolean
        Dim ret As Boolean = True
        Try
            'hash pass
            oCli.Passwd = Criptografia.Crypto.getCrypto().generarMD5(oCli.Passwd)

            ret = DAL.DALCliente.recuperarClave(oCli)
            If ret Then
                Dim oDvh As Infra.helper_dv = New Infra.helper_dv
                oDvh.generate_dvh_for_table("Usuario")
                'update dvv
                If Not Infra.DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
            End If
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ERR_reset")
        End Try

        Return ret
    End Function
End Class
