Public Class BLPreguntaSecreta
    'singleton
    Private Shared ReadOnly instance As BLPreguntaSecreta = New BLPreguntaSecreta
    Private Sub New()

    End Sub
    'interface
    Public Shared Function getPreguntaSercreta() As BLPreguntaSecreta
        Return instance
    End Function
    'end singleton implementation

    'api public
    Public Function Filtrar(ByVal oPreg As BE.BEPreguntaSecreta) As List(Of BE.BEPreguntaSecreta)
        Dim lista As List(Of BE.BEPreguntaSecreta) = New List(Of BE.BEPreguntaSecreta)
        Try
            lista = DAL.DALPreguntaSecreta.getPreguntaSercreta.Filtrar(oPreg)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ERR")
        End Try

        Return lista

    End Function
End Class
