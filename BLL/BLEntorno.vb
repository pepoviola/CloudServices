Public Class BLEntorno

    Public Shared Function generarEntorno(ByVal opts As Dictionary(Of String, String)) As List(Of BE.BEServicioBase)
        Dim lista As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
        Try
            ' calculo de cantidad de servers
            '
            ' memoria de los server
            ' 2 -  basic (1)
            ' 4 -  advance (2)
            ' 16 - pro (6)

            ' busco la informacion
            Dim visitas As Double = opts.Item("visitas")
            Dim db As Boolean = opts.ContainsKey("db")
            Dim email As Boolean = opts.ContainsKey("email")
            Dim bkp As Boolean = opts.ContainsKey("bkp")
            Dim snap As Boolean = opts.ContainsKey("snap")
            Dim ha As Boolean = opts.ContainsKey("ha")

            Dim q As Integer = 1
            If ha Then
                q = 2
            End If

            ' logic
            Dim factor As Double
            factor = (visitas / 10000)
            If factor > 4 Then
                Dim s As Double = (factor / 20)
                '
                For i = 0 To CInt(s) + 1
                    Dim oSrv As New BE.BECloudServerPro
                    'oSrv.Id = 6
                    oSrv.Descripcion = "web"
                    lista.Add(oSrv)
                Next

                ' ha web
                If ha Then
                    If lista.Count < 2 Then
                        Dim oSrv As New BE.BECloudServerPro
                        'oSrv.Id = 6
                        oSrv.Descripcion = "web"
                        lista.Add(oSrv)
                    End If
                End If

                If db Then

                    For j = 1 To q
                        Dim oSrv_db As BE.BECloudServer = New BE.BECloudServerPro
                        'oSrv_db.Id = 6
                        oSrv_db.Descripcion = "db"
                        lista.Add(oSrv_db)
                    Next

                End If

            Else
                If factor < 2 Then
                    For j = 1 To q
                        Dim oSrv As New BE.BECloudServerBasic
                        'oSrv.Id = 1
                        oSrv.Descripcion = "web"
                        lista.Add(oSrv)
                    Next

                    If db Then

                        For j = 1 To q
                            Dim oSrv_db As BE.BECloudServer = New BE.BECloudServerBasic
                            'oSrv_db.Id = 1
                            oSrv_db.Descripcion = "db"
                            lista.Add(oSrv_db)
                        Next

                    End If
                Else
                    For j = 1 To q
                        Dim oSrv As New BE.BECloudServerAdvance
                        'oSrv.Id = 2
                        oSrv.Descripcion = "web"
                        lista.Add(oSrv)
                    Next
 

                    If db Then

                        For j = 1 To q
                            Dim oSrv_db As BE.BECloudServer = New BE.BECloudServerAdvance
                            'oSrv_db.Id = 2
                            oSrv_db.Descripcion = "db"
                            lista.Add(oSrv_db)
                        Next

                    End If
                End If

            End If


            ' email 
            If email Then
                For j = 1 To q
                    ' creo el server de mail chico
                    Dim oSrv_email As New BE.BECloudServerBasic
                    'oSrv_email.Id = 1
                    oSrv_email.Descripcion = "email"
                    lista.Add(oSrv_email)
                Next
               

            End If

            ' si tiene snap o backup
            If snap Or bkp Then
                For Each serv As BE.BECloudServer In lista
                    serv.Srv_adicionales = New List(Of BE.BEServicioAdicional)
                    If snap Then
                        serv.Srv_adicionales.Add(New BE.BESnapshot)
                    End If

                    If bkp Then
                        serv.Srv_adicionales.Add(New BE.BEBackupService)
                    End If

                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return lista

    End Function
End Class
