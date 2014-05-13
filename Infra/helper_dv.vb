Public Class helper_dv
    Public Function generate_dvh_for_table(ByVal table As String) As String
        Dim res As String = ""
        Dim lista As New Dictionary(Of Integer, Dictionary(Of String, String))
        lista = DAL.DVManager.getTableStrings(table)
        Dim pair As KeyValuePair(Of Integer, Dictionary(Of String, String))
        Dim conn As IDbConnection = DAL.dbManager.getConnection
        conn.Open()
        Dim listaERR As New List(Of Integer)
        For Each pair In lista
            Dim n As String = Criptografia.Crypto.getCrypto().generarMD5(pair.Value.Keys(0))
            Dim cmd As IDbCommand = DAL.dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            Dim id As String = String.Empty
            If table = "Usuario" Then
                id = "IdUsuario"
            ElseIf table = "Bitacora" Then
                id = "idBitacora"
            ElseIf table = "Tag" Then
                id = "IdTag"
            ElseIf table = "Familia" Then
                id = "IdFamilia"
            ElseIf table = "Locacion" Then
                id = "IdLocacion"
            ElseIf table = "Evento" Then
                id = "IdEvento"
            End If
            cmd.CommandText = "update " + table + " set DVH = '"
            cmd.CommandText += n
            cmd.CommandText += "' where " + id + " = "
            cmd.CommandText += Convert.ToString(pair.Key)
            'DAL.dbManager.addParam(cmd, "@dvh", n)

            Dim ret As Integer = cmd.ExecuteNonQuery()
            If ret <> 1 Then
                listaERR.Add(pair.Key)
            End If
            'MessageBox.Show(String.Format("{0} - {1} - {2}", pair.Key, n, ret))
        Next

        If listaERR.Count > 0 Then

            For Each i As Integer In listaERR
                res += i + "-"

            Next
        End If
        Return res
    End Function

End Class
