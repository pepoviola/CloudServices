Imports System.Data.SqlClient

Public Class DALServicios

    'singleton

    ''' <summary>
    ''' Singleton implementation 
    ''' </summary>
    ''' <remarks></remarks>

    Private Shared ReadOnly instance As DALServicios = New DALServicios
    Private Sub New()
    End Sub

    Public Shared Function getServiciosDAL() As DALServicios
        Return instance
    End Function




    ' completa la info de un servicio
    Public Sub getServiceInfo(ByRef oServ As BE.BEServicioBase)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroServiciosTipo")
            'asocio la cx
            cmd.Connection = conn
            conn.Open()
            If Not oServ Is Nothing AndAlso Not String.IsNullOrEmpty(oServ.Codigo) Then
                dbManager.addParam(cmd, "@Codigo", oServ.Codigo)
            Else
                dbManager.addParam(cmd, "@Codigo", DBNull.Value)
            End If

            If Not oServ Is Nothing AndAlso Not oServ.Id = 0 Then
                dbManager.addParam(cmd, "@Id", oServ.Id)
            Else
                dbManager.addParam(cmd, "@Id", DBNull.Value)
            End If

            'Dim lector As IDataReader = cmd.ExecuteReader()
            'Do While lector.Read()

            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter
            Dim dt As DataTable = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            For Each lector As DataRow In dt.Rows
                oServ.Nombre = Convert.ToString(lector("Nombre"))
                oServ.Codigo = Convert.ToString(lector("Codigo"))
                oServ.Descripcion = Convert.ToString(lector("Descripcion"))
                oServ.Precio = Convert.ToDouble(lector("Precio"))

            Next
            'Loop


        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try


    End Sub


    Public Function obtenerServiciosDeCliente(ByVal filtro As BE.BECliente) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectServersCliente")
            'asocio la cx
            cmd.Connection = conn
            'agrego los params [id_cliente]         
            dbManager.addParam(cmd, "@Id_cli", filtro.ClienteId)
            'abro cx
            'conn.Open()
            'ejecuto y obtengo el reader
            'Dim lector As IDataReader = cmd.ExecuteReader()
            'Do While lector.Read()

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet
            Dim dt As DataTable = New DataTable
            da.Fill(ds)
            For Each lector As DataRow In ds.Tables(0).Rows
                'Dim srv As BE.BECloudServer = New BE.BECloudServer()
                Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector("Codigo"))))
                Dim srv As Object = Activator.CreateInstance(t)
                srv.Id = Convert.ToInt32(lector("Id"))
                srv.Nombre = Convert.ToString(lector("Nombre"))
                srv.Precio = Convert.ToDouble(lector("Precio"))
                srv.Codigo = Convert.ToString(lector("Codigo"))
                srv.vmNombre = Convert.ToString(lector("vmNombre"))
                srv.Estado = Convert.ToString(lector("Estado"))
                'srv.Srv_adicionales = New List(Of BE.BEServicioAdicional)        
                lista.Add(srv)
                'Loop
            Next


            'lector.Close()

            For Each srv As BE.BECloudServer In lista
                ' chequeo los adicionales
                Dim cmd_addons As IDbCommand = dbManager.getCmd("SelectAdicionales")
                cmd_addons.Connection = conn
                'agrego los params [id_cliente]         
                dbManager.addParam(cmd_addons, "@Id_padre", srv.Id)

                'ejecuto y obtengo el reader
                'Dim lector_addons As IDataReader = cmd_addons.ExecuteReader()
                da.SelectCommand = cmd_addons
                ds.Clear()
                dt.Clear()
                da.Fill(dt)
                'Do While lector_addons.Read()
                For Each lector_addons As DataRow In dt.Rows
                    Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector_addons("Codigo"))))
                    Dim addon As Object = Activator.CreateInstance(t)
                    'Dim addon As BE.BEServicioAdicional = New BE.BEServicioAdicional()
                    addon.Id = Convert.ToInt32(lector_addons("Id"))
                    addon.Nombre = Convert.ToString(lector_addons("Nombre"))
                    addon.Precio = Convert.ToDouble(lector_addons("Precio"))
                    addon.Codigo = Convert.ToString(lector_addons("Codigo"))

                    'srv.Srv_adicionales.Add(addon)
                    srv.addAdicional(addon)
                    'Loop
                    'lector_addons.Close()
                Next



            Next

            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            'limpio
            lista = Nothing
        End Try
        'Return lista
    End Function



    Public Function obtenerServiciosPorServer(ByVal filtro As BE.BEServerPlataforma) As List(Of BE.BECloudServer)
        Dim lista As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            Dim cmd As IDbCommand = dbManager.getCmd("SelectServersPorServer")
            'asocio la cx
            cmd.Connection = conn
            'agrego los params [id_cliente]         
            dbManager.addParam(cmd, "@Id", filtro.Id)

            'ado dx
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet
            Dim dt As DataTable = New DataTable
            da.Fill(ds)
            For Each lector As DataRow In ds.Tables(0).Rows
                'Dim srv As BE.BECloudServer = New BE.BECloudServer()
                Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector("Codigo"))))
                Dim srv As Object = Activator.CreateInstance(t)
                srv.Id = Convert.ToInt32(lector("Id"))
                srv.Nombre = Convert.ToString(lector("Nombre"))
                srv.Precio = Convert.ToDouble(lector("Precio"))
                srv.Codigo = Convert.ToString(lector("Codigo"))
                lista.Add(srv)
            Next


            For Each srv As BE.BECloudServer In lista
                ' chequeo los adicionales
                Dim cmd_addons As IDbCommand = dbManager.getCmd("SelectAdicionales")
                cmd_addons.Connection = conn
                dbManager.addParam(cmd_addons, "@Id_padre", srv.Id)

                'ejecuto y obtengo el reader
                'Dim lector_addons As IDataReader = cmd_addons.ExecuteReader()
                da.SelectCommand = cmd_addons
                ds.Clear()
                dt.Clear()
                da.Fill(dt)
                For Each lector_addons As DataRow In dt.Rows
                    Dim t As Type = Type.GetType(String.Format("BE.{0},BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Convert.ToString(lector_addons("Codigo"))))
                    Dim addon As Object = Activator.CreateInstance(t)
                    'Dim addon As BE.BEServicioAdicional = New BE.BEServicioAdicional()
                    addon.Id = Convert.ToInt32(lector_addons("Id"))
                    addon.Nombre = Convert.ToString(lector_addons("Nombre"))
                    addon.Precio = Convert.ToDouble(lector_addons("Precio"))
                    addon.Codigo = Convert.ToString(lector_addons("Codigo"))

                    srv.addAdicional(addon)
                Next

            Next

            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            'limpio
            lista = Nothing
        End Try
    End Function

    Public Sub CrearServicios(ByVal oOV As BE.BEOrdenVenta)
        Dim conn As IDbConnection = dbManager.getConnection
        Dim idParam As SqlParameter
        Try
            conn.Open()
            Dim trans As IDbTransaction = conn.BeginTransaction
            Try

                ' ahora agrego los servicios
                For Each s As BE.BECloudServer In oOV.Servicios
                    Dim cmd_srv As IDbCommand = dbManager.getCmd("InsertSrvContratadoAndOutputId")

                    'asocio la cx
                    cmd_srv.Connection = conn
                    cmd_srv.Transaction = trans

                    ' agrego los params
                    dbManager.addParam(cmd_srv, "@Id_tipo_srv", s.Id)
                    dbManager.addParam(cmd_srv, "@Id_ov", oOV.Id)
                    dbManager.addParam(cmd_srv, "@Precio", s.Precio)
                    dbManager.addParam(cmd_srv, "@Id_server_plataforma", s.Platform_server.Id)

                    idParam = CType(dbManager.addParam(cmd_srv, "@id"), SqlParameter)
                    idParam.Direction = ParameterDirection.Output

                    cmd_srv.ExecuteNonQuery()

                    Dim id_srv_padre As Integer = idParam.Value

                    ' los adicionales
                    If Not s.Srv_adicionales Is Nothing Then


                        For Each addon As BE.BEServicioBase In s.Srv_adicionales
                            Dim cmd_addon As IDbCommand = dbManager.getCmd("InsertSrvContratadoAndOutputId")

                            'asocio la cx
                            cmd_addon.Connection = conn
                            cmd_addon.Transaction = trans

                            ' agrego los params
                            dbManager.addParam(cmd_addon, "@Id_tipo_srv", addon.Id)
                            dbManager.addParam(cmd_addon, "@Id_ov", oOV.Id)
                            dbManager.addParam(cmd_addon, "@Precio", addon.Precio)
                            dbManager.addParam(cmd_addon, "@Id_server_plataforma", s.Platform_server.Id)

                            idParam = CType(dbManager.addParam(cmd_addon, "@id"), SqlParameter)
                            idParam.Direction = ParameterDirection.Output

                            cmd_addon.ExecuteNonQuery()

                            Dim id_srv_hijo As Integer = idParam.Value

                            ' los relaciono
                            Dim cmd_rel As IDbCommand = dbManager.getCmd("InsertSrvContratadoRelacion")
                            'asocio la cx
                            cmd_rel.Connection = conn
                            cmd_rel.Transaction = trans
                            ' agrego los params
                            dbManager.addParam(cmd_rel, "@Id_padre", id_srv_padre)
                            dbManager.addParam(cmd_rel, "@Id_hijo", id_srv_hijo)

                            cmd_rel.ExecuteNonQuery()


                        Next
                    End If
                Next

                trans.Commit()

            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

    End Sub


End Class
