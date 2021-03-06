﻿Public Class BECliente
    Inherits BEUsuario
    Implements ICloneable


    Private _direccion As BE.BEDireccion
    Public Property Direccion() As BE.BEDireccion
        Get
            Return _direccion
        End Get
        Set(ByVal value As BE.BEDireccion)
            _direccion = value
        End Set
    End Property

    Private _id_cliente As Integer
    Public Property ClienteId() As Integer
        Get
            Return _id_cliente
        End Get
        Set(ByVal value As Integer)
            _id_cliente = value
        End Set
    End Property

    Private _servicios As List(Of BE.BEServicioBase)
    Public Property servicios() As List(Of BE.BEServicioBase)
        Get
            Return _servicios
        End Get
        Set(ByVal value As List(Of BE.BEServicioBase))
            _servicios = value
        End Set
    End Property

    Private _preg_secreta As BE.BEPreguntaSecreta
    Public Property PregSecreta() As BE.BEPreguntaSecreta
        Get
            Return _preg_secreta
        End Get
        Set(value As BE.BEPreguntaSecreta)
            _preg_secreta = value
        End Set
    End Property





    'Public Overloads Function Clone() As Object Implements ICloneable.Clone
    '    Dim e As New BE.BECliente
    '    e = Me.MemberwiseClone()
    '    If Not e.Direccion Is Nothing Then
    '        e.Direccion = Me.Direccion.Clone()
    '    End If
    '    Return e

    'End Function
End Class
