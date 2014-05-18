Public Class index
    Inherits System.Web.UI.Page

    Private _read As Boolean
    Public ReadOnly Property read
        Get
            Return _read
        End Get
    End Property
    

    Private _lista_bita As List(Of BE.Bitacora) = New List(Of BE.Bitacora)
    Public ReadOnly Property lista_bita() As List(Of BE.Bitacora)
        Get
            Return _lista_bita
        End Get
    End Property

    ''' <summary>
    ''' Index para bitacora
    '''   Obtiene los ultimos x rows de la bitacora
    '''   
    '''   1ro debo validar la session
    '''   2do que tenga permiso para esta pagina
    '''   3ro traducirla y mostrarla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            '' verifico si tiene acceso

            ' old implementation
            '_read = Utilidades.getUtilidades().tieneAcceso("bitacora", Master.user_permisos)
            _read = Utilidades.getUtilidades().tieneAcceso("bitacora", Session("flia"))


            '' si no tiene acceso
            If Not _read Then
                Response.Redirect("/", False)
                Exit Sub
            End If

            ' make it with ajax
            'Try
            ' obtengo la bitacora
            'Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
            '_lista_bita = oInfraBita.filtrar(New BE.Bitacora())

            'Catch ex As ExceptionsPersonales.CustomException
            ' redirect to error page
            'End Try

        End If

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Master.lang)
    End Function

End Class