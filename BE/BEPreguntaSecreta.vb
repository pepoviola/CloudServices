'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BEPreguntaSecreta.vb
''  Implementation of the Class BEPreguntaSecreta
''  Generated by Enterprise Architect
''  Created on:      10-Jun-2014 10:54:37 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Public Class BEPreguntaSecreta


    Private _pregunta As String
    Private _respuesta As String

    Public Property Pregunta() As String
        Get
            Return _pregunta
        End Get
        Set(ByVal Value As String)
            _pregunta = Value
        End Set
    End Property

    Public Property Respuesta() As String
        Get
            Return _respuesta
        End Get
        Set(ByVal Value As String)
            _respuesta = Value
        End Set
    End Property


End Class ' BEPreguntaSecreta
