Public Interface ICRUD(Of T)
    Function Agregar(ByVal t As T) As Boolean
    Function Modificar(ByVal t As T) As Boolean
    Function Eliminar(ByVal t As T) As Boolean
    'Function Listar() As List(Of T)
    Function Filtrar(ByVal t As T) As List(Of T)
    ' Function Conn() As SqlClient.SqlConnection
End Interface
