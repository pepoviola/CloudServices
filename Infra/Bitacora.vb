﻿
'Infra::Bitacora
'
' Manejo de la logica de la bitacora
'
' Singleton

Public Class Bitacora

    'objeto del singleton
    Private Shared ReadOnly instance As Bitacora = New Bitacora()

    Private Sub New()

    End Sub
    'Metodo para obtener el singleton
    Public Shared Function getInfraBitacora() As Bitacora
        Return instance
    End Function


    'Metodos publicos
    '
    ' filtrar : devuelve una coleccion de bitacoras
    Public Function filtrar(ByVal filtro As BE.Bitacora, ByVal filtro_hasta As BE.Bitacora) As List(Of BE.Bitacora)
        Dim _lista As New List(Of BE.Bitacora)
        Try

            'objeto dal
            Dim oDalBita As DAL.BitacoraDAL = DAL.BitacoraDAL.getBitacoraDal()
            _lista = oDalBita.getBitacora(filtro, filtro_hasta)
        Catch ex As Exception
            'Lanzar ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrFiltrarBita")

        End Try
        Return _lista
    End Function

    Public Function Log(ByVal bita As BE.Bitacora) As Boolean
        Dim retorno As Boolean
        Try
            'objeto dal
            Dim oDalBita As DAL.BitacoraDAL = DAL.BitacoraDAL.getBitacoraDal()
            ' lo paso a la DAL
            ' para que el objeto no tenga que tener una propiedad dvh
            ''genero el DVH
            'Dim bitaString As String = String.Empty

            'bitaString += bita.Descripcion + bita.Categoria _
            '           + Convert.ToString(bita.Usuario.Id)
            'bita.DVH = Criptografia.Crypto.getCrypto().generarMD5(bitaString)
            retorno = oDalBita.Log(bita)
            If retorno Then
                'actualizo dvv
                If Not DVV.Actualizar("Bitacora") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
            End If
        Catch ex As Exception
            'lanzar ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrLogBita")
        End Try
        Return retorno
    End Function





End Class
