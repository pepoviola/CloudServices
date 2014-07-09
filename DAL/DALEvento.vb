Imports System.Data.SqlClient
Public Class DALEvento
    'singleton
    Private Shared ReadOnly instance As DALEvento = New DAL.DALEvento()
    Private Sub New()

    End Sub
    'interface
    Public Shared Function getDALEvento() As DALEvento
        Return instance
    End Function
    'end singleton implementation

    'api public

    Public Function filtrar(ByVal ev As BE.BEEvento) As List(Of BE.BEEvento)
        Dim _lista As List(Of BE.BEEvento) = New List(Of BE.BEEvento)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco las task
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroEventos")
            'agrego los params

            If Not ev Is Nothing AndAlso ev.Server.Id > 0 Then
                dbManager.addParam(cmd, "@idserver", ev.Server.Id)
            Else
                dbManager.addParam(cmd, "@idserver", DBNull.Value)
            End If


            cmd.Connection = conn
            'abro cx
            conn.Open()

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                Dim evRow As BE.BEEvento = New BE.BEEvento
                evRow.Id = Convert.ToInt32(lector("Id"))
                evRow.Evento = Convert.ToString(lector("Evento"))
                evRow.Descripcion = Convert.ToString(lector("Descripcion"))
                evRow.Fecha = Convert.ToDateTime(lector("FechaEvento"))
                evRow.Server = New BE.BECloudServerBasic()
                evRow.Server.Id = ev.Server.Id


                _lista.Add(evRow)
            Next

            Return _lista
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()
            _lista = Nothing
        End Try
    End Function


    Public Sub crearEvento(ByVal ev As BE.BEEvento)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco las task
            Dim cmd As IDbCommand = dbManager.getCmd("insertEvento")
            'agrego los params


            dbManager.addParam(cmd, "@ev", ev.Evento)
            dbManager.addParam(cmd, "@descrip", ev.Descripcion)
            dbManager.addParam(cmd, "@fecha", ev.Fecha)
            dbManager.addParam(cmd, "@idserver", ev.Server.Id)

            

            cmd.Connection = conn
            'abro cx
            conn.Open()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()

        End Try

    End Sub
End Class
