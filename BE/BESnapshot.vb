'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BESnapshot.vb
''  Implementation of the Class BESnapshot
''  Generated by Enterprise Architect
''  Created on:      01-Jun-2014 12:47:59 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Public Class BESnapshot
    Inherits BEServicioAdicional


    Sub New()
        Id = 5
    End Sub


    Public Overrides Function isBkp() As Boolean
        Return False
    End Function

    Public Overrides Function isSnap() As Boolean
        Return True
    End Function
End Class ' BESnapshot

