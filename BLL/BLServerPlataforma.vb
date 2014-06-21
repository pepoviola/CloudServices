'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BLServerPlataforma.vb
''  Implementation of the Class BLServerPlataforma
''  Generated by Enterprise Architect
''  Created on:      21-Jun-2014 12:02:50 AM
''  Original author: pepo
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


Public Class BLServerPlataforma


    ''' 
    ''' <param name="oServer"></param>
    Public Function Actualizar(ByVal oServer As BE.BEServerPlataforma) As Boolean
        Actualizar = False
    End Function

    ''' 
    ''' <param name="oServer"></param>
    Public Function Crear(ByVal oServer As BE.BEServerPlataforma) As Boolean
        Crear = False
    End Function

    ''' 
    ''' <param name="oServer"></param>
    Public Function Eliminar(ByVal oServer As BE.BEServerPlataforma) As Boolean
        Eliminar = False
    End Function

    ''' 
    ''' <param name="oServer"></param>
    Public Function Filtrar(ByVal oServer As BE.BEServerPlataforma) As List(Of BE.BEServerPlataforma)
        Dim servers As List(Of BE.BEServerPlataforma) = New List(Of BE.BEServerPlataforma)
        Dim sDal As DAL.DALServerPlataforma = New DAL.DALServerPlataforma
        Try
            servers = sDal.Filtrar(oServer)
            Return servers
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("Err_filtrar_servers")
        Finally
            sDal = Nothing
            servers = Nothing
        End Try
    End Function


    Public Function CalcularMemoriaLibre(ByVal oServer As BE.BEServerPlataforma) As Integer
        Dim oFacade As BLServicesFacade = BLServicesFacade.getServicesFacade()
        Dim servicios As List(Of BE.BECloudServer) = New List(Of BE.BECloudServer)
        Dim mem_used As Integer = 0
        Try
            servicios = oFacade.obtenerServiciosDeServer(oServer)

            For Each s As BE.BECloudServer In servicios
                mem_used += s.Memoria
            Next
            ' just for check
            If oServer.Memoria = 0 Then
                oServer.Memoria = 192 'default
            End If
            Return (oServer.Memoria - mem_used)
        Catch ex As Exception
            Throw ex
        Finally
            servicios = Nothing
            mem_used = Nothing
        End Try
    End Function

End Class ' BLServerPlataforma

