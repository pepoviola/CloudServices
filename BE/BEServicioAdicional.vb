'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEServicioAdicional.vb
''  Implementation of the Class BEServicioAdicional
''  Generated by Enterprise Architect
''  Created on:      01-Jun-2014 12:47:48 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





Public Class BEServicioAdicional
    Inherits BEServicioBase




    Public Function isBkp() As Boolean
        Return (Me.Codigo = "cloud-addon-bkp")
    End Function

    Public Function isSnap() As Boolean
        Return (Me.Codigo = "cloud-addon-snap")
    End Function



End Class ' BEServicioAdicional