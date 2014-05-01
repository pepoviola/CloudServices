Public Class BackupDAL
    Implements DAL.ICRUD(Of BE.BEBackup)


    'singleton
    Private Shared ReadOnly instance As BackupDAL = New BackupDAL
    Private Sub New()

    End Sub
    'impl
    Public Shared Function getBkpMgr() As BackupDAL
        Return instance
    End Function
    'end singleton

    'public api

    Public Function realizarBackup(ByVal obkp As BE.BEBackup) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection()
        Try
            'path -> to config
            Dim path As String = Configuration.ConfigurationManager.AppSettings("PathBkp")
            Dim cmd As IDbCommand = dbManager.getCmd()
            cmd.CommandType = CommandType.Text
            Dim databaseName As String = Configuration.ConfigurationManager.AppSettings("DBname")
            Dim databaseFlat As String = path & "\" & obkp.Filename
            cmd.CommandText = "BACKUP DATABASE [" & databaseName & "] TO DISK='" & databaseFlat & "' WITH NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD , STATS = 10 "
            cmd.Connection = conn
            conn.Open()
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function realizarRestore(ByVal obkp As BE.BEBackup) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection()
        Try
            conn.Open()
            ' Dim trans As IDbTransaction = conn.BeginTransaction
            'Try
            'path -> to config
            Dim path As String = Configuration.ConfigurationManager.AppSettings("PathBkp")
            Dim bkpfile As String = path + "\" + obkp.Filename
            Dim cmd1 As IDbCommand = dbManager.getCmd()
            cmd1.Connection = conn
            Dim cmd2 As IDbCommand = dbManager.getCmd()
            cmd2.Connection = conn
            Dim cmd3 As IDbCommand = dbManager.getCmd()
            cmd3.Connection = conn
            Dim cmd4 As IDbCommand = dbManager.getCmd()
            cmd4.Connection = conn

            Dim databaseName As String = Configuration.ConfigurationManager.AppSettings("DBname")
            cmd1.CommandType = CommandType.Text
            'cmd1.Transaction = trans
            cmd2.CommandType = CommandType.Text
            'cmd2.Transaction = trans
            cmd3.CommandType = CommandType.Text
            'cmd3.Transaction = trans
            cmd4.CommandType = CommandType.Text
            'cmd4.Transaction = trans

            'commands
            cmd1.CommandText = "use master"
            cmd2.CommandText = "alter database " + databaseName + " set single_user with rollback immediate"
            'dbManager.addParam(cmd2, "@DBname", databaseName)
            cmd3.CommandText = "alter database " + databaseName + " set multi_user"
            'dbManager.addParam(cmd3, "@DBname", databaseName)
            cmd4.CommandText = "RESTORE DATABASE " + databaseName + " FROM DISK = '" + bkpfile + "' WITH FILE = 1, MOVE '" + databaseName + "' TO '" + path + "\\" + databaseName + "_new.mdf', MOVE N'" + databaseName + "_Log' TO N'" + path + "\\" + databaseName + "_new_Log.ldf', NOUNLOAD"
            'cmd4.CommandText = "RESTORE DATABASE @DBname FROM DISK = @BkpFile WITH FILE = 1, MOVE '@DBName'+'_Data' TO '@Camino\\@DBname_new.mdf', MOVE N'Elastic_Log' TO N'" & BackupDirectory & "\\Elastic_new_Log.ldf', NOUNLOAD"

            cmd1.ExecuteNonQuery()
            cmd2.ExecuteNonQuery()
            cmd3.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()
            'trans.Commit()
            Return True
            'Catch ex As Exception
            '   trans.Rollback()
            '  Throw ex
            'End Try





            '            use(master)
            '            go()
            'alter database Elastic set single_user with rollback immediate
            'alter database Elastic set multi_user
            'RESTORE DATABASE Elastic FROM DISK = N'<bkpfile>' WITH FILE = 1, MOVE N'Elastic' TO N'<bkpDirectory>\\Elastic_new.mdf', MOVE N'Elastic_Log' TO N'<bkpDirectory>\\Elastic_new_Log.ldf', NOUNLOAD


        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function Agregar(ByVal t As BE.BEBackup) As Boolean Implements ICRUD(Of BE.BEBackup).Agregar
        Dim registros As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("InsertBackups")
            cmd.Connection = conn
            dbManager.addParam(cmd, "@filename", t.Filename)
            dbManager.addParam(cmd, "@fecha", t.Fecha)
            dbManager.addParam(cmd, "@idUsuario", t.Usuario.Id)

            conn.Open()
            registros = cmd.ExecuteNonQuery

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return (registros = 1)
    End Function

    Public Function Eliminar(ByVal t As BE.BEBackup) As Boolean Implements ICRUD(Of BE.BEBackup).Eliminar
        Dim ret As Boolean
        Return ret
    End Function

    Public Function Filtrar(ByVal t As BE.BEBackup) As System.Collections.Generic.List(Of BE.BEBackup) Implements ICRUD(Of BE.BEBackup).Filtrar
        Dim lista As New List(Of BE.BEBackup)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroBackups")
            cmd.Connection = conn
            conn.Open()
            Dim lector As IDataReader = cmd.ExecuteReader
            Do While (lector.Read())
                Dim bkp As New BE.BEBackup
                bkp.Fecha = Convert.ToDateTime(lector("Fecha"))
                bkp.Filename = Convert.ToString(lector("Filename"))
                Dim oUser As New BE.BEUsuario
                oUser.Username = Convert.ToString(lector("username"))
                bkp.Usuario = oUser
                bkp.Id = Convert.ToInt32(lector("IdBkp"))
                lista.Add(bkp)
            Loop
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return lista
    End Function

    Public Function Modificar(ByVal t As BE.BEBackup) As Boolean Implements ICRUD(Of BE.BEBackup).Modificar
        Dim ret As Boolean
        Return ret
    End Function
End Class
