'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEUsuario.vb
''  Implementation of the Class BEUsuario
''  Generated by Enterprise Architect
''  Created on:      11-Apr-2014 2:27:24 PM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Web.Script.Serialization




<Serializable()> _
Public Class BEUsuario
    Inherits BEUsuarioBase
    Implements ICloneable


    Private _apellido As String
    Private _nombre As String


    Public Property Apellido() As String
        Get
            Return _apellido
        End Get
        Set(ByVal Value As String)
            _apellido = Value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal Value As String)
            _nombre = Value
        End Set
    End Property



    Public Function Clone() As Object Implements ICloneable.Clone
        Dim e As New BE.BEUsuario()
        e = Me.MemberwiseClone()
        If Not Me.Idioma Is Nothing Then
            e.Idioma = Me.Idioma.Clone()
        End If

        If Not Me.Patente Is Nothing Then
            e.Patente = Me.Patente.Clone()
        End If

        Return e
    End Function
End Class ' BEUsuario
