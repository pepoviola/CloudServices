
'infra
Imports System.IO

Public Class Backup
    'singleton
    Private Shared ReadOnly instance As Backup = New Backup
    Private Sub New()

    End Sub
    'interface
    Public Shared Function getBkp() As Backup
        Return instance

    End Function
    'end singleton implementation

    'api public

    Public Function realizarBackup(ByVal obkp As BE.BEBackup) As Boolean
        Try
            Dim _ret As Boolean
            'path -> improve to config file
            Dim path As String = Configuration.ConfigurationManager.AppSettings("PathBkp")
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If




            'obkpDAL
            Dim oBkpDal As DAL.BackupDAL = DAL.BackupDAL.getBkpMgr()
            _ret = oBkpDal.realizarBackup(obkp)

            If _ret Then
                'persist
                oBkpDal.Agregar(obkp)
            End If

            Return _ret

        Catch ex As Exception
            'th custom ex
            Throw New ExceptionsPersonales.CustomException("ex_create_bkp")
        End Try


    End Function

    Public Function realizarRestore(ByVal obkp As BE.BEBackup) As Boolean
        Dim ret As Boolean
        Try

            'dal
            Dim bkpDal As DAL.BackupDAL = DAL.BackupDAL.getBkpMgr()
            ret = bkpDal.realizarRestore(obkp)
            'bita
            'If ret Then
            '    log
            '    Dim obita As New BE.Bitacora("Restore", String.Format("Se realizo el restore desde el archivo: {0} con éxito", obkp.Filename))
            '    infra
            '    Dim oBitaInfra As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
            '    oBitaInfra.Log(obita)

            'Else
            '    ?no deberia estar aqui
            'End If

        Catch ex As Exception
            'custom ex
            Throw ex
        End Try
        Return ret
    End Function
    Public Function filtrar(ByVal filtro As BE.BEBackup) As List(Of BE.BEBackup)
        Dim lista As New List(Of BE.BEBackup)
        Try
            Dim dalBkp As DAL.BackupDAL = DAL.BackupDAL.getBkpMgr()
            lista = dalBkp.Filtrar(filtro)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrFiltrarBkp")
        End Try
        Return lista
    End Function
End Class
