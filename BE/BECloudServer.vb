'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BECloudServer.vb
''  Implementation of the Class BECloudServer
''  Generated by Enterprise Architect
''  Created on:      01-Jun-2014 12:47:23 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Public Class BECloudServer
    Inherits BE.BEServicioBase


    Private _srv_adicionales As List(Of BEServicioAdicional)


    Public Property Srv_adicionales() As List(Of BEServicioAdicional)
        Get
            Return _srv_adicionales
        End Get
        Set(ByVal Value As List(Of BEServicioAdicional))
            _srv_adicionales = Value
        End Set
    End Property


End Class ' BECloudServer
