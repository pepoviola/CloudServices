'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEServerPlataforma.vb
''  Implementation of the Class BEServerPlataforma
''  Generated by Enterprise Architect
''  Created on:      17-Jun-2014 11:50:38 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




Public Class BEServerPlataforma


    Private _hostname As String
    Private _memoria As Integer
    Private _q_cpu As Integer

    Public Property Hostname() As String
        Get
            Return _hostname
        End Get
        Set(ByVal Value As String)
            _hostname = Value
        End Set
    End Property

    Public Property Memoria() As Integer
        Get
            Return _memoria
        End Get
        Set(ByVal Value As Integer)
            _memoria = Value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Property Q_cpu() As Integer
        Get
            Return _q_cpu
        End Get
        Set(ByVal Value As Integer)
            _q_cpu = Value
        End Set
    End Property


End Class ' BEServerPlataforma