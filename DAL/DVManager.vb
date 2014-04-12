Public Class DVManager
    Public Shared Function getTableStrings(ByVal tabla As String) As Dictionary(Of Integer, Dictionary(Of String, String))

        Dim lista As New Dictionary(Of Integer, Dictionary(Of String, String))
        Dim conn As IDbConnection = dbManager.getConnection()
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("getAllTabla")

            'fix this
            dbManager.addParam(cmd, "@tabla", tabla)
            cmd.Connection = conn
            conn.Open()
            Dim lector As IDataReader = cmd.ExecuteReader
            While (lector.Read())
                Dim q As Integer = lector.FieldCount
                Dim campo As String = String.Empty
                For i As Integer = 1 To (q - 2)
                    campo += Convert.ToString(lector(i))
                Next
                lista.Add(Convert.ToInt32(lector(0)), _
                          New Dictionary(Of String, String) From {{campo, Convert.ToString(lector((q - 1)))}})
            End While
        Catch ex As Exception
        Finally
            conn.Close()
        End Try

        Return lista

    End Function

    Public Shared Function getTableDVHs(ByVal tabla As String) As String
        Dim retorno As String = String.Empty
        Dim conn As IDbConnection = dbManager.getConnection()
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("getFieldFrom")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@tabla", tabla)
            dbManager.addParam(cmd, "@campo", "DVH")
            conn.Open()
            Dim lector As IDataReader = cmd.ExecuteReader
            While (lector.Read())
                retorno += Convert.ToString(lector(0))

            End While
            Return retorno
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Shared Function getDVV(ByVal tabla As String) As String
        Dim ret As String = String.Empty
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("getDVV")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@tabla", tabla)
            conn.Open()
            Dim lector As IDataReader = cmd.ExecuteReader()
            While (lector.Read())
                ret = Convert.ToString(lector(0))
            End While
            Return ret
        Catch ex As Exception
            Throw ex
        Finally
            conn.close()
        End Try
    End Function


    Public Shared Function saveDVV(ByVal tabla As String, ByVal dvv As String) As String
        Dim registros As Integer
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("saveDVV")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@tabla", tabla)
            dbManager.addParam(cmd, "@dvv", dvv)
            conn.Open()
            registros = cmd.ExecuteNonQuery()

            Return (registros = 1)
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

End Class
