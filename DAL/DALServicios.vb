Public Class DALServicios
    Private ReadOnly _servers As List(Of Integer) = New List(Of Integer) From {1, 2, 6}

    'Public Function filtrar(ByVal filtro As BE.BEServicioBase) As List(Of BE.BEServicioBase)
    '    Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
    '    Dim conn As IDbConnection = dbManager.getConnection
    '    Try
    '        Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroServiciosTipo")
    '        cmd.Connection = conn
    '        'agrego los params []
    '        If Not filtro Is Nothing AndAlso Not String.IsNullOrEmpty(filtro.Codigo) Then
    '            dbManager.addParam(cmd, "@Codigo", filtro.Codigo)
    '        Else
    '            dbManager.addParam(cmd, "@Codigo", DBNull.Value)
    '        End If

    '        If Not filtro Is Nothing AndAlso Not String.IsNullOrEmpty(filtro.Codigo) Then
    '            dbManager.addParam(cmd, "@Id", filtro.Id)
    '        Else
    '            dbManager.addParam(cmd, "@Id", DBNull.Value)
    '        End If

    '    Catch ex As Exception

    '    End Try
    '    Return lista
    'End Function


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

            Dim lector As IDataReader = cmd.ExecuteReader()
            Do While lector.Read()

                oServ.Nombre = Convert.ToString(lector("Nombre"))
                oServ.Codigo = Convert.ToString(lector("Codigo"))
                oServ.Descripcion = Convert.ToString(lector("Descripcion"))
                oServ.Precio = Convert.ToDouble(lector("Precio"))

            Loop


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
            conn.Open()
            'ejecuto y obtengo el reader
            Dim lector As IDataReader = cmd.ExecuteReader()
            Do While lector.Read()
                Dim srv As BE.BECloudServer = New BE.BECloudServer()

                srv.Id = Convert.ToInt32(lector("Id"))
                srv.Nombre = Convert.ToString(lector("Nombre"))
                srv.Precio = Convert.ToDouble(lector("Precio"))
                srv.Srv_adicionales = New List(Of BE.BEServicioAdicional)        
                lista.Add(srv)
            Loop

            lector.Close()

            For Each srv As BE.BECloudServer In lista
                ' chequeo los adicionales
                Dim cmd_addons As IDbCommand = dbManager.getCmd("SelectAdicionales")
                cmd_addons.Connection = conn
                'agrego los params [id_cliente]         
                dbManager.addParam(cmd_addons, "@Id_padre", srv.Id)

                'ejecuto y obtengo el reader
                Dim lector_addons As IDataReader = cmd_addons.ExecuteReader()
                Do While lector_addons.Read()
                    Dim addon As BE.BEServicioAdicional = New BE.BEServicioAdicional()
                    addon.Id = Convert.ToInt32(lector_addons("Id"))
                    addon.Nombre = Convert.ToString(lector_addons("Nombre"))
                    addon.Precio = Convert.ToDouble(lector_addons("Precio"))

                    srv.Srv_adicionales.Add(addon)
                Loop
                lector_addons.Close()

                
            Next

        Catch ex As Exception
            Throw ex
        End Try
        Return lista
    End Function
End Class
