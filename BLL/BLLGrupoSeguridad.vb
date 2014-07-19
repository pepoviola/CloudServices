Public Class BLLGrupoSeguridad
    Public Function Crear(ByVal oSG As BE.BEGrupoSeguridad) As Boolean
        Dim oDAL As DAL.DALGrupoSeguridad = DAL.DALGrupoSeguridad.getDALGrupo()
        Try
            Return oDAL.crearGrupo(oSG)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("sg_generated_err")
        Finally

        End Try
    End Function

    Public Function Filtrar(ByVal oSG As BE.BEGrupoSeguridad) As List(Of BE.BEGrupoSeguridad)
        Dim oDAL As DAL.DALGrupoSeguridad = DAL.DALGrupoSeguridad.getDALGrupo()
        Dim _lista As List(Of BE.BEGrupoSeguridad) = New List(Of BE.BEGrupoSeguridad)
        Try
            _lista = oDAL.Filtrar(oSG)
            Return _lista
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("sg_generated_err")
        Finally
            _lista = Nothing
        End Try
    End Function

    Function Actualziar(ByVal oSG As BE.BEGrupoSeguridad) As Boolean
        Dim oDAL As DAL.DALGrupoSeguridad = DAL.DALGrupoSeguridad.getDALGrupo()
        Try
            Return oDAL.Actualizar(oSG)
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("sg_actualizar_err")
        Finally

        End Try
    End Function

    Function Eliminar(ByVal oSG As BE.BEGrupoSeguridad) As Boolean
        Dim oDAL As DAL.DALGrupoSeguridad = DAL.DALGrupoSeguridad.getDALGrupo()
        Try
            Return oDAL.Eliminar(oSG)
        Catch exsql As SqlClient.SqlException
            If exsql.Number = 547 Then
                Throw New ExceptionsPersonales.CustomException("SGEnUso")
            Else
                Throw New ExceptionsPersonales.CustomException("sg_deleted_err")
            End If
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("sg_deleted_err")
        Finally

        End Try
    End Function

End Class
