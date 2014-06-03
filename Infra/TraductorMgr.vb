
Public Class TraductorMgr

    '///<summary>
    '/// Clase que da servicio de traduccion
    '///</smmary>
    Public Shared Function TraducirControl(ByVal control As String, ByVal idioma As Integer) As String
        Dim ret As String = String.Empty
        Try
            'voy a la dal
            Dim tagsDal As DAL.TagsDAL = DAL.TagsDAL.gettagDal
            ret = tagsDal.getTagValue(control, idioma)


            ' implementacion de idioma default
            ' español - 1
            ' loop only one time

            If ret = "" Then
                ' busco en el default
                ret = tagsDal.getTagValue(control, 1)
            End If

            ' si no existe es que no existe el tag
            ' lo informo
            If ret = "" Then
                ret = String.Format("No existe el código {0} en el idioma {1} ni en el default", control, idioma)
            End If

        Catch ex As System.Data.SqlClient.SqlException
            'si no me puedo conectar a la db siempre respondo con error de conexión
            ret = String.Format("Error en TRAD MGR [verificar keys]")

        Catch ex As Exception
            'custom ex
            'Throw ex
            ret = String.Format("Error en TRAD MGR")
        End Try
        Return ret
    End Function



End Class
