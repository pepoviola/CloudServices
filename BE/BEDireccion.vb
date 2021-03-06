'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEDireccion.vb
''  Implementation of the Class BEDireccion
''  Generated by Enterprise Architect
''  Created on:      01-Jun-2014 12:39:25 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Public Class BEDireccion
    Implements ICloneable


    Private _calle As String
    'Private _dto As String
    Private _id As Integer
    Private _localidad As String
    Private _numero As Integer
    'Private _piso As Integer

    Public Property Calle() As String
        Get
            Return _calle
        End Get
        Set(ByVal Value As String)
            _calle = Value
        End Set
    End Property

    'Public Property Dto() As String
    '    Get
    '        Return _dto
    '    End Get
    '    Set(ByVal Value As String)
    '        _dto = Value
    '    End Set
    'End Property

    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Public Property Localidad() As String
        Get
            Return _localidad
        End Get
        Set(ByVal Value As String)
            _localidad = Value
        End Set
    End Property

    Public Property Numero() As Integer
        Get
            Return _numero
        End Get
        Set(ByVal Value As Integer)
            _numero = Value
        End Set
    End Property

    'Public Property Piso() As Integer
    '    Get
    '        Return _piso
    '    End Get
    '    Set(ByVal Value As Integer)
    '        _piso = Value
    '    End Set
    'End Property


    Public Function Clone() As Object Implements ICloneable.Clone
        Dim e As New BE.BEDireccion
        e = Me.MemberwiseClone()
        Return e
    End Function
End Class ' BEDireccion

