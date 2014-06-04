'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEOrdenVenta.vb
''  Implementation of the Class BEOrdenVenta
''  Generated by Enterprise Architect
''  Created on:      04-Jun-2014 11:08:55 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


Public Class BEOrdenVenta


    Private _cliente As BECliente
    Private _estado As String
    Private _fecha As DateTime
    Private _id As Integer
    Private _servicios As List(Of BEServicioBase)


    Public Property Cliente() As BECliente
        Get
            Return _cliente
        End Get
        Set(ByVal Value As BECliente)
            _cliente = Value
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

    Public Property Fecha() As DateTime
        Get
            Return _fecha
        End Get
        Set(ByVal Value As DateTime)
            _fecha = Value
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

    Public Property Servicios() As List(Of BEServicioBase)
        Get
            Return _servicios
        End Get
        Set(ByVal Value As List(Of BEServicioBase))
            _servicios = Value
        End Set
    End Property


End Class ' BEOrdenVenta