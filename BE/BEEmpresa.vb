'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEEmpresa.vb
''  Implementation of the Class BEEmpresa
''  Generated by Enterprise Architect
''  Created on:      01-Jun-2014 12:40:00 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Public Class BEEmpresa


    Private _cuit As String
    Private _domicilio As BEDireccion
    Private _id As Integer
    Private _ingresos_brutos As String
    Private _inicio_actividad As Date
    Private _nombre_fantasia As String
    Private _razon_social As String
    Public m_BEDireccion As BEDireccion

    Public Property Cuit() As String
        Get
            Return _cuit
        End Get
        Set(ByVal Value As String)
            _cuit = Value
        End Set
    End Property

    Public Property Domicilio() As BEDireccion
        Get
            Return _domicilio
        End Get
        Set(ByVal Value As BEDireccion)
            _domicilio = Value
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

    Public Property Iinicio_actividad() As Date
        Get
            Return _inicio_actividad
        End Get
        Set(ByVal Value As Date)
            _inicio_actividad = Value
        End Set
    End Property

    Public Property Ingresos_brutos() As String
        Get
            Return _ingresos_brutos
        End Get
        Set(ByVal Value As String)
            _ingresos_brutos = Value
        End Set
    End Property

    Public Property Nombre_fantasia() As String
        Get
            Return _nombre_fantasia
        End Get
        Set(ByVal Value As String)
            _nombre_fantasia = Value
        End Set
    End Property

    Public Property Razon_social() As String
        Get
            Return _razon_social
        End Get
        Set(ByVal Value As String)
            _razon_social = Value
        End Set
    End Property


End Class ' BEEmpresa
