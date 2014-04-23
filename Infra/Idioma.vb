
'
' Infra: Idioma
'
' Manejo de la lógica (crear, modificar, filtrar, eliminar, get Tags)
Public Class Idioma
    'Singleton
    Private Shared ReadOnly instance As Idioma = New Idioma()

    Private Sub New()

    End Sub

    'para obtener la instancia
    Public Shared Function getIdioma() As Idioma
        Return instance
    End Function

    'fin de implementacion de singleton

    'api publica

    Public Function Filtrar(ByVal filtro As BE.Idioma) As List(Of BE.Idioma)
        Dim _lista As New List(Of BE.Idioma)

        Try
            'llamo a la dal
            Dim oIdiomaDal As DAL.IdiomaDAL = DAL.IdiomaDAL.getIdiomaDAL()
            _lista = oIdiomaDal.Filtrar(filtro)

        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrFiltrarIdioma")
        End Try
        Return _lista
    End Function

    Public Function obtenerTags(ByVal idioma As BE.Idioma) As List(Of BE.Tag)
        Dim lista As New List(Of BE.Tag)
        Try

        Catch ex As Exception

        End Try
        Return lista
    End Function

    Public Function Agregar(ByVal idioma As BE.Idioma) As Boolean
        Dim ret As Boolean
        Try
            Dim idiomaDal As DAL.IdiomaDAL = DAL.IdiomaDAL.getIdiomaDAL
            ret = idiomaDal.Agregar(idioma)

            ' esto lo tengo que pasar a la ui 
            ' para tener acceso al objeto session


            'If ret Then
            '    'log in bitacora
            '    Dim oBita As New BE.Bitacora("Idiomas", "Se creo ok el idioma: " + idioma.Descripcion)
            '    Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
            '    oInfraBita.Log(oBita)
            'Else
            '    'log in bitacora
            '    Dim oBita As New BE.Bitacora("Idiomas", "Error al crear el idioma: " + idioma.Descripcion)
            '    Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
            '    oInfraBita.Log(oBita)
            'End If
        Catch exCus As ExceptionsPersonales.CustomException
            Throw exCus
        Catch ex As Exception
            'custom ex
            Throw New ExceptionsPersonales.CustomException("ErrAgregarIdioma")
        End Try
        Return ret
    End Function

    Public Function Eliminar(ByVal idioma As BE.Idioma) As Boolean
        Dim ret As Boolean = False
        Dim odal As DAL.IdiomaDAL = DAL.IdiomaDAL.getIdiomaDAL
        Try
            If odal.Eliminar(idioma) Then

                'log in bitacora
                'Dim oBita As New BE.Bitacora("Idiomas", "Se eliminó el idioma: " + idioma.Descripcion)
                'Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
                'oInfraBita.Log(oBita)
                ret = True
                'Else
                'log in bitacora
                'Dim oBita As New BE.Bitacora("Idiomas", "Error al eliminar el idioma: " + idioma.Descripcion)
                'Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
                'oInfraBita.Log(oBita)

            End If


        Catch exsql As SqlClient.SqlException
            If exsql.Number = 547 Then
                Throw New ExceptionsPersonales.CustomException("IdiomaEnUso")
            Else
                Throw New ExceptionsPersonales.CustomException("ErrEliminarIdioma")
            End If
        Catch ex As Exception

            Throw New ExceptionsPersonales.CustomException("ErrEliminarIdioma")

        End Try

        Return ret

    End Function
End Class
