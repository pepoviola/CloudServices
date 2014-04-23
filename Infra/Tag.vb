
'
' Infra :: Tag
Public Class Tag

    'singleton class

    Private Shared ReadOnly instance As Tag = New Tag
    Private Sub New()

    End Sub

    Public Shared Function getTagMgr() As Tag
        Return instance
    End Function
    'end singleton implementation

    'public api
    Public Function filtrar(ByVal filtro As BE.Tag) As List(Of BE.Tag)
        Dim lista As New List(Of BE.Tag)
        Try
            Dim tagDal As DAL.TagsDAL = DAL.TagsDAL.gettagDal()
            lista = tagDal.Filtrar(filtro)
        Catch ex As Exception
            'custom ex
            Throw ex
        End Try
        Return lista

    End Function

    Public Function obtenerTodos() As List(Of BE.Tag)
        Dim lista As New List(Of BE.Tag)
        Try
            Dim tagDal As DAL.TagsDAL = DAL.TagsDAL.gettagDal()
            lista = tagDal.obtenerTodos()
        Catch ex As Exception
            'custom ex
            Throw ex
        End Try
        Return lista

    End Function

    Public Function modificarVarios(ByVal lista As List(Of BE.Tag)) As Boolean
        Try
            'dal
            Dim odal As DAL.TagsDAL = DAL.TagsDAL.gettagDal
            If odal.ModificarVarios(lista) Then
                'bitacora
                Return True
            Else
                'bitacora
                Return False
            End If

        Catch ex As Exception
            'custom ex
            Throw ex
        End Try
    End Function
End Class
