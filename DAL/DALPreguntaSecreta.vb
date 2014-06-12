Public Class DALPreguntaSecreta
    'singleton
    Private Shared ReadOnly instance As DALPreguntaSecreta = New DALPreguntaSecreta
    Private Sub New()

    End Sub
    'interface
    Public Shared Function getPreguntaSercreta() As DALPreguntaSecreta
        Return instance
    End Function
    'end singleton implementation

    'api public

    Public Function Filtrar(ByVal oPreg As BE.BEPreguntaSecreta) As List(Of BE.BEPreguntaSecreta)
        Dim lista As List(Of BE.BEPreguntaSecreta) = New List(Of BE.BEPreguntaSecreta)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroPreguntas")
            cmd.Connection = conn
            If Not oPreg Is Nothing AndAlso Not String.IsNullOrEmpty(oPreg.Pregunta) Then
                dbManager.addParam(cmd, "@PreguntaTag", oPreg.Pregunta)
            Else
                dbManager.addParam(cmd, "@PreguntaTag", DBNull.Value)
            End If

            conn.Open()
            'dr
            'Dim lector As IDataReader = cmd.ExecuteReader
            'Do While (lector.Read())

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                Dim oPregTo As BE.BEPreguntaSecreta = New BE.BEPreguntaSecreta
                oPregTo.Pregunta = Convert.ToString(lector("PreguntaTag"))
                lista.Add(oPregTo)
            Next
            'Loop
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

        Return lista

    End Function
End Class
