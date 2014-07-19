Imports System.Data.SqlClient

Public Class DALGrupoSeguridad
    'singleton
    Private Shared ReadOnly instance As DALGrupoSeguridad = New DAL.DALGrupoSeguridad()
    Private Sub New()

    End Sub
    'interface
    Public Shared Function getDALGrupo() As DALGrupoSeguridad
        Return instance
    End Function
    'end singleton implementation

    'api public

    Public Function crearGrupo(ByVal oSG As BE.BEGrupoSeguridad) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                ' busco las task
                Dim cmd As IDbCommand = dbManager.getCmd("insertGrupoAndOutputId")
                'agrego los params


                dbManager.addParam(cmd, "@name", oSG.Nombre)
                dbManager.addParam(cmd, "@fechaIn", oSG.FechaIn)
                dbManager.addParam(cmd, "@idCli", oSG.Cliente.ClienteId)

                Dim idParam As SqlParameter = CType(dbManager.addParam(cmd, "@id"), SqlParameter)
                idParam.Direction = ParameterDirection.Output


                cmd.Connection = conn
                cmd.Transaction = trans

                cmd.ExecuteNonQuery()

                Dim idFW As Integer = idParam.Value

                ' ahora agrego las reglas

                For Each r As BE.BERegla In oSG.Reglas
                    Dim cmd_regla As IDbCommand = dbManager.getCmd("InsertRegla")

                    'agrego los params
                    dbManager.addParam(cmd_regla, "@origen", r.Origen)
                    dbManager.addParam(cmd_regla, "@pto", r.PtoDestino)
                    dbManager.addParam(cmd_regla, "@regla", r.Regla)
                    dbManager.addParam(cmd_regla, "@idGrp", idFW)

                    cmd_regla.Connection = conn
                    cmd_regla.Transaction = trans

                    cmd_regla.ExecuteNonQuery()

                Next


                trans.Commit()
                Return True

            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()

        End Try

    End Function

    Function Filtrar(oSG As BE.BEGrupoSeguridad) As List(Of BE.BEGrupoSeguridad)
        Dim conn As IDbConnection = dbManager.getConnection
        Dim _lista As List(Of BE.BEGrupoSeguridad) = New List(Of BE.BEGrupoSeguridad)

        Try
            ' busco las task
            Dim cmd As IDbCommand = dbManager.getCmd("selectFiltroGrupo")
            cmd.Connection = conn
            'agrego los params

            If Not oSG Is Nothing AndAlso Not String.IsNullOrEmpty(oSG.Nombre) Then
                dbManager.addParam(cmd, "@name", oSG.Nombre)
            Else
                dbManager.addParam(cmd, "@name", DBNull.Value)
            End If

            If Not oSG Is Nothing AndAlso Not oSG.Cliente Is Nothing AndAlso Not oSG.Cliente.ClienteId = 0 Then
                dbManager.addParam(cmd, "@idCli", oSG.Cliente.ClienteId)
            Else
                dbManager.addParam(cmd, "@idCli", DBNull.Value)
            End If

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            Dim dt_rules As DataTable = New DataTable
            da.Fill(dt)

            For Each lector As DataRow In dt.Rows
                Dim sg As BE.BEGrupoSeguridad = New BE.BEGrupoSeguridad
                sg.Id = Convert.ToInt32(lector("IdGrupo"))
                sg.Nombre = Convert.ToString(lector("Nombre"))
                sg.FechaIn = Convert.ToDateTime(lector("FechaIn"))
                sg.Reglas = New List(Of BE.BERegla)

                Dim cmd_rules As IDbCommand = dbManager.getCmd("getRulesById")
                cmd_rules.Connection = conn
                dbManager.addParam(cmd_rules, "@idGrp", sg.Id)

                dt_rules.Rows.Clear()
                da.SelectCommand = cmd_rules
                da.Fill(dt_rules)

                For Each r As DataRow In dt_rules.Rows
                    Dim rule As BE.BERegla = New BE.BERegla
                    rule.Id = Convert.ToInt32(r("IdRegla"))
                    rule.Origen = Convert.ToString(r("Origen"))
                    rule.PtoDestino = Convert.ToString(r("PtoDestino"))
                    rule.Regla = Convert.ToString(r("Regla"))
                    sg.Reglas.Add(rule)
                Next


                _lista.Add(sg)
            Next

            Return _lista

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            _lista = Nothing
        End Try
    End Function

    Function Actualizar(oSG As BE.BEGrupoSeguridad) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                ' busco las task
                Dim cmd As IDbCommand = dbManager.getCmd("deleteRulesByIdGrp")
                'agrego los params


                dbManager.addParam(cmd, "@idGrp", oSG.Id)
                
                cmd.Connection = conn
                cmd.Transaction = trans

                cmd.ExecuteNonQuery()

                ' ahora agrego las reglas

                For Each r As BE.BERegla In oSG.Reglas
                    Dim cmd_regla As IDbCommand = dbManager.getCmd("InsertRegla")

                    'agrego los params
                    dbManager.addParam(cmd_regla, "@origen", r.Origen)
                    dbManager.addParam(cmd_regla, "@pto", r.PtoDestino)
                    dbManager.addParam(cmd_regla, "@regla", r.Regla)
                    dbManager.addParam(cmd_regla, "@idGrp", oSG.Id)

                    cmd_regla.Connection = conn
                    cmd_regla.Transaction = trans

                    cmd_regla.ExecuteNonQuery()

                Next

                trans.Commit()
                Return True

            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()

        End Try
    End Function

    Public Function obtenerSGsPorVM(ByVal oServer As BE.BECloudServer) As List(Of BE.BEGrupoSeguridad)
        Dim _lista As List(Of BE.BEGrupoSeguridad) = New List(Of BE.BEGrupoSeguridad)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            ' busco las task
            Dim cmd As IDbCommand = dbManager.getCmd("selectSGByVM")
            cmd.Connection = conn
            'agrego los params

            dbManager.addParam(cmd, "@susc", oServer.Id)

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            Dim dt_rules As DataTable = New DataTable
            da.Fill(dt)

            For Each lector As DataRow In dt.Rows
                Dim sg As BE.BEGrupoSeguridad = New BE.BEGrupoSeguridad
                sg.Id = Convert.ToInt32(lector("IdGrupo"))
                sg.Nombre = Convert.ToString(lector("Nombre"))
                sg.FechaIn = Convert.ToDateTime(lector("FechaIn"))
                sg.Reglas = New List(Of BE.BERegla)

                Dim cmd_rules As IDbCommand = dbManager.getCmd("getRulesById")
                cmd_rules.Connection = conn
                dbManager.addParam(cmd_rules, "@idGrp", sg.Id)

                dt_rules.Rows.Clear()
                da.SelectCommand = cmd_rules
                da.Fill(dt_rules)

                For Each r As DataRow In dt_rules.Rows
                    Dim rule As BE.BERegla = New BE.BERegla
                    rule.Id = Convert.ToInt32(r("IdRegla"))
                    rule.Origen = Convert.ToString(r("Origen"))
                    rule.PtoDestino = Convert.ToString(r("PtoDestino"))
                    rule.Regla = Convert.ToString(r("Regla"))
                    sg.Reglas.Add(rule)
                Next


                _lista.Add(sg)
            Next
            Return _lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            _lista = Nothing
        End Try

    End Function

    Function Eliminar(oSG As BE.BEGrupoSeguridad) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try
                ' busco las task
                Dim cmd As IDbCommand = dbManager.getCmd("deleteRulesByIdGrp")
                'agrego los params


                dbManager.addParam(cmd, "@idGrp", oSG.Id)

                cmd.Connection = conn
                cmd.Transaction = trans

                cmd.ExecuteNonQuery()

                ' ahora elimino el SG


                Dim cmd_sg As IDbCommand = dbManager.getCmd("DeleteSG")

                'agrego los params
                dbManager.addParam(cmd_sg, "@idGrp", oSG.Id)

                cmd_sg.Connection = conn
                cmd_sg.Transaction = trans

                cmd_sg.ExecuteNonQuery()


                trans.Commit()
                Return True

            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()

        End Try
    End Function

End Class
