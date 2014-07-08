Imports System.Data.SqlClient
Public Class DALTaskMgr
    'singleton
    Private Shared ReadOnly instance As DALTaskMgr = New DALTaskMgr()
    Private Sub New()

    End Sub
    'interface
    Public Shared Function getTaskMgr() As DALTaskMgr
        Return instance
    End Function
    'end singleton implementation

    'api public

    Public Function filtrar(ByVal t As BE.BETask) As List(Of BE.BETask)
        Dim _lista As List(Of BE.BETask) = New List(Of BE.BETask)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco las task
            'Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroTask")
            'agrego los params

            'If Not t Is Nothing AndAlso Not DateTime.MinValue = t.FechaOut Then
            '    dbManager.addParam(cmd, "@fechaOut", t.FechaOut)
            'Else
            '    dbManager.addParam(cmd, "@fechaOut", DBNull.Value)
            'End If

            'If Not t Is Nothing AndAlso t.IdTask > 0 Then
            '    dbManager.addParam(cmd, "@id", t.IdTask)
            'Else
            '    dbManager.addParam(cmd, "@id", DBNull.Value)
            'End If

            Dim cmd As SqlCommand = New SqlCommand("select *  from vmProvisioning where FechaOut is NULL")

            cmd.Connection = conn
            'abro cx
            conn.Open()

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                Dim taskRow As BE.BETask = New BE.BETask
                taskRow.IdTask = Convert.ToInt32(lector("IdTask"))
                taskRow.Task = Convert.ToInt32(lector("Task"))
                taskRow.FechaIn = Convert.ToDateTime(lector("FechaIn"))
                If Not lector("FechaOut") Is DBNull.Value Then
                    taskRow.FechaOut = Convert.ToDateTime(lector("FechaOut"))
                End If
                If Not lector("Resultado") Is DBNull.Value Then
                    taskRow.Resultado = Convert.ToString(lector("Resultado"))
                End If

                taskRow.Server = New BE.BECloudServerBasic()
                taskRow.Server.Id = Convert.ToInt32(lector("Suscription"))
                _lista.Add(taskRow)
            Next

            Return _lista
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()
            _lista = Nothing
        End Try
    End Function

    Public Function actualizar(ByVal t As BE.BETask) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco las task
            'Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroTask")
            'agrego los params

            'If Not t Is Nothing AndAlso Not DateTime.MinValue = t.FechaOut Then
            '    dbManager.addParam(cmd, "@fechaOut", t.FechaOut)
            'Else
            '    dbManager.addParam(cmd, "@fechaOut", DBNull.Value)
            'End If

            'If Not t Is Nothing AndAlso t.IdTask > 0 Then
            '    dbManager.addParam(cmd, "@id", t.IdTask)
            'Else
            '    dbManager.addParam(cmd, "@id", DBNull.Value)
            'End If

            Dim q As String = String.Format("select * from vmProvisioning where IdTask = {0}", t.IdTask)
            Dim cmd As SqlCommand = New SqlCommand(q)
            cmd.Connection = conn
            'abro cx
            conn.Open()

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim cmdBuilder As SqlCommandBuilder = New SqlCommandBuilder(da)
            da.UpdateCommand = cmdBuilder.GetUpdateCommand
            da.InsertCommand = cmdBuilder.GetInsertCommand
            da.DeleteCommand = cmdBuilder.GetDeleteCommand

            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                lector("FechaOut") = t.FechaOut
                lector("Resultado") = t.Resultado
            Next

            da.Update(dt)

            Return True
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()

        End Try
    End Function
End Class
