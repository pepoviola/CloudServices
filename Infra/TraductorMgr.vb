
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
