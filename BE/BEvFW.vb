'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEvFW.vb
''  Implementation of the Class BEvFW
''  Generated by Enterprise Architect
''  Created on:      01-Jun-2014 12:47:35 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Public Class BEvFW
    Inherits BEServicioBase


    Private _servers As List(Of BECloudServer)


    Public Property Servers() As List(Of BECloudServer)
        Get
            Return _servers
        End Get
        Set(ByVal Value As List(Of BECloudServer))
            _servers = Value
        End Set
    End Property


End Class ' BEvFW
