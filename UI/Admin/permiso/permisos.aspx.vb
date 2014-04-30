Public Class permisos
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' _lang is used to translate the page
    ''' the value comes from the session
    ''' </summary>
    ''' <remarks></remarks>
    Private _lang As Integer
    Public ReadOnly Property lang As Integer
        Get
            Return _lang
        End Get
    End Property

    Private _langs As List(Of BE.Idioma) = New List(Of BE.Idioma)
    Public ReadOnly Property langs As List(Of BE.Idioma)
        Get
            Return _langs
        End Get
    End Property

    Private _listaPatentes As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
    Public ReadOnly Property listaPatentes As List(Of BE.BEPatenteBasica)
        Get
            Return _listaPatentes
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' protect the page
        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            _lang = Session("lang")
            'obtengo todas las patentes
            Dim oInfraFlia As Infra.Familia = New Infra.Familia()
            _listaPatentes = oInfraFlia.getListaCompleta()
        End If
    End Sub


    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

    ''' <summary>
    ''' genera en server side el html de nodos 
    ''' para el formulario de alta de flia
    ''' </summary>
    ''' <param name="root"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function create_nodes(ByVal root As BE.BEPatenteBasica) As String
        Dim res As String = ""
        ' creo el item
        res += String.Format("<li class='li_item'><div class='div_item'><img class='expand' src='/content/img/Minus.png' /><img class='collapse' src='/content/img/Plus.png'/></div><div class='div_item'><input id='chbox-{0}' name='{0}' type='checkbox' /></div><div class='div_item'>{1}</div>", root.codigo.ToString(), root.descripcion)
        If root.GetType().Name = "BEFamilia" Then

            ' iterate over
            res += "<ul class='ul_root'>"
            For Each inner As BE.BEPatenteBasica In CType(root, BE.BEFamilia).Patentes
                res += create_nodes(inner)
            Next
            res += "</ul>"
        End If

        'fin del nodo
        res += "</li>"
        Return res

    End Function

End Class