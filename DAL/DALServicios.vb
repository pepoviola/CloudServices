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

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return lista
    End Function




End Class
