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
    'Inherits BEUsuarioBase
    Implements ICloneable


    Private _apellido As String
    Private _nombre As String
    '
    Private _email As String
    Private _estado As String
    Private _id As Integer
    Private _idioma As Idioma
    Private _passwd As String
    Private _patente As BEFamilia
    Private _username As String


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

    '
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal Value As String)
            _email = Value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal Value As String)
            _estado = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Public Property Idioma() As Idioma
        Get
            Return _idioma
        End Get
        Set(ByVal Value As Idioma)
            _idioma = Value
        End Set
    End Property

    Public Sub New()

    End Sub
    <ScriptIgnore()> _
    Public Property Passwd() As String
        Get
            Return _passwd
        End Get
        Set(ByVal Value As String)
            _passwd = Value
        End Set
    End Property

    Public Property Patente() As BEFamilia
        Get
            Return _patente
        End Get
        Set(ByVal Value As BEFamilia)
            _patente = Value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal Value As String)
            _username = Value
        End Set
    End Property

    '


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
