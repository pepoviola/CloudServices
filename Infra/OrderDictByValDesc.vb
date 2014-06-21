Public Class OrderDictByValDesc
    Implements IComparer


    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        x = DirectCast(x, Dictionary(Of String, Integer))
        y = DirectCast(y, Dictionary(Of String, Integer))


    End Function
End Class
