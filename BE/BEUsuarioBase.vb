'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEUsuarioBase.vb
''  Implementation of the Class BEUsuarioBase
''  Generated by Enterprise Architect
''  Created on:      11-Apr-2014 2:27:13 PM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Option Explicit On
Option Strict On

Public MustInherit Class BEUsuarioBase


    Private _dvh As String
    Private _email As String
    Private _estado As String
    Private _id As Integer
    Private _idioma As Idioma
    Private _passwd As String
    Private _patente As BEFamilia
    Private _username As String
    Public m_BEFamilia As BEFamilia
    Public m_BEIdioma As Idioma

    Public Property Dvh() As String
        Get
            Return _dvh
        End Get
        Set(ByVal Value As String)
            _dvh = Value
        End Set
    End Property

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


End Class ' BEUsuarioBase

