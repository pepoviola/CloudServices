

Public Class Utilidades

    Private Shared ReadOnly instance As Utilidades = New Utilidades

    Private Sub New()

    End Sub

    Public Shared Function getUtilidades() As Utilidades
        Return instance
    End Function

    Public Function translate(ByVal ctrl_id As String, lang As Integer)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, lang)
    End Function

    Public Sub translateContentPage(ByVal ctrl As Control, lang As Integer)
        For Each c As Control In ctrl.Controls
            If c.Controls.Count > 0 Then
                translateContentPage(c, lang)
            Else
                ' busco labels y botones
                If TypeOf (c) Is Label Then
                    CType(c, Label).Text = Infra.TraductorMgr.TraducirControl(c.ID, lang)
                ElseIf TypeOf (c) Is Button Then
                    CType(c, Button).Text = Infra.TraductorMgr.TraducirControl(c.ID, lang)


                End If
            End If
        Next


    End Sub
    Public Sub translatePage(ByVal p As Page, lang As Integer)
        For Each c As Control In p.Controls
            If c.Controls.Count > 0 Then
                translateContentPage(c, lang)
            Else
                ' busco labels y botones
                If TypeOf (c) Is Label Then
                    CType(c, Label).Text = Infra.TraductorMgr.TraducirControl(c.ID, lang)
                ElseIf TypeOf (c) Is Button Then
                    CType(c, Button).Text = Infra.TraductorMgr.TraducirControl(c.ID, lang)
                Else

                End If
            End If
        Next


    End Sub

    '    Public Function iterateOverNodes(ByVal NodePadre As TreeNode, ByVal item As BE.BEPatenteBasica) As TreeNode 'As List(Of TreeNode)
    '        'Dim _lista As New List(Of TreeNode)
    '        Dim oFlia As New Infra.Familia
    '        For Each subitem In oFlia.getPatentesFromFlia(item)
    '            Dim subNode As New TreeNode
    '            subNode.Text = subitem.descripcion
    '            subNode.Tag = subitem.codigo
    '            NodePadre.Nodes.Add(iterateOverNodes(subNode, subitem))

    '        Next

    '        Return NodePadre
    '        'Return _lista
    '    End Function

    '    Public Function validateEmpytxt(ByVal formulario As Form) As Boolean
    '        'some DRY 
    '        Dim valid As Boolean = True
    '        For Each c As Control In formulario.Controls

    '            If TypeOf (c) Is TextBox Then
    '                If String.IsNullOrEmpty(c.Text) Then
    '                    valid = False
    '                End If
    '            End If

    '        Next

    '        Return valid
    '    End Function

    '    Public Sub traducirForm(ByVal f As Form)
    '        'traducciones
    '        For Each ctrl As Control In f.Controls
    '            If TypeOf (ctrl) Is Label Or TypeOf (ctrl) Is Button Then
    '                ctrl.Text = Infra.TraductorMgr.TraducirControl(ctrl.Name, BE.UsuarioSession.getUser().Idioma.Id)

    '            ElseIf TypeOf (ctrl) Is GroupBox Then
    '                For Each inner As Control In ctrl.Controls
    '                    If TypeOf (inner) Is Label Or TypeOf (inner) Is Button Then
    '                        inner.Text = Infra.TraductorMgr.TraducirControl(inner.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                    End If
    '                Next

    '            ElseIf TypeOf (ctrl) Is MenuStrip Then
    '                'el form es mdi lo recorro
    '                For Each d As ToolStripMenuItem In f.MainMenuStrip.Items
    '                    d.Text = Infra.TraductorMgr.TraducirControl(d.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                    If d.DropDown.Items.Count > 0 Then
    '                        For Each i As ToolStripMenuItem In d.DropDown.Items
    '                            i.Text = Infra.TraductorMgr.TraducirControl(i.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                            If i.DropDown.Items.Count > 0 Then
    '                                For Each j As ToolStripMenuItem In i.DropDown.Items
    '                                    j.Text = Infra.TraductorMgr.TraducirControl(j.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                                Next
    '                            End If
    '                        Next
    '                    End If

    '                Next

    '            End If
    '        Next

    '        f.Text = Infra.TraductorMgr.TraducirControl(f.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '    End Sub


    ' no se deberia usar mas

    Public Function get_user_permisos(ByVal flia_code As String) As List(Of String)
        'devuelve todos los permisos del usuario


        Dim lista_permisos As List(Of String) = New List(Of String)
        If Not flia_code Is Nothing Then
            Dim oFlia As BE.BEFamilia = New BE.BEFamilia
            oFlia.codigo = flia_code
            Dim oFliaInfra As Infra.Familia = New Infra.Familia
            Dim patentes As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
            patentes = oFliaInfra.getPatentesFromFlia(oFlia)

            ' valido los permisos
            For Each p As String In New List(Of String) From _
                {"idioma_read", "idioma_write", "permiso_read", "permiso_write", "usuario_read", _
                 "usuario_write", "bitacora", "dv_mgr", "backup", "restore", "reportes", "plataforma_read", "plataforma_write"}

                For Each flia As BE.BEPatenteBasica In patentes
                    If flia.Validar(p) Then
                        lista_permisos.Add(p)
                    End If
                Next
            Next
        End If

        Return lista_permisos

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ctrl">control a validar</param>
    ''' <param name="permisos">lista de permisos del usuario</param>
    ''' <returns>boolean (si tiene acceso a ese control o no)</returns>
    ''' <remarks></remarks>
    Public Function tieneAcceso(ByVal ctrl As String, ByVal permisos As List(Of String)) As Boolean
        'recorro las familias
        Dim retorno As Boolean = True
        If permisos.IndexOf(ctrl) < 0 Then
            retorno = False
        End If
        Return retorno
    End Function

    ''' <summary>
    '''  Obtengo las patentes de la flia
    ''' Las recorro y si es valida la requerida devuelvo True
    ''' Si termina el bucle devuelve False
    ''' </summary>
    ''' <param name="ctrl">permiso a validar</param>
    ''' <param name="flia_code">familia del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function tieneAcceso(ByVal ctrl As String, ByVal flia_code As String) As Boolean

        Dim retorno As Boolean = False
        Dim oFlia As BE.BEFamilia = New BE.BEFamilia
        oFlia.codigo = flia_code
        Dim oFliaInfra As Infra.Familia = New Infra.Familia
        Dim patentes As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
        patentes = oFliaInfra.getPatentesFromFlia(oFlia)
        For Each flia As BE.BEPatenteBasica In patentes
            If flia.Validar(ctrl) Then
                Return True
            End If
        Next

        'If permisos.IndexOf(ctrl) < 0 Then
        '    retorno = False
        'End If
        Return retorno
    End Function


    '    Public Sub tienePermiso(ByVal f As Form)
    '        'permisos
    '        For Each ctrl As Control In f.Controls
    '            '            If TypeOf (ctrl) Is Label Or TypeOf (ctrl) Is Button Then

    '            If TypeOf (ctrl) Is Button Then
    '                ctrl.Enabled = tieneAcceso(ctrl.Name)
    '                'ctrl.Text = Infra.TraductorMgr.TraducirControl(ctrl.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '            ElseIf TypeOf (ctrl) Is MenuStrip Then
    '                'el form es mdi lo recorro
    '                For Each d As ToolStripMenuItem In f.MainMenuStrip.Items
    '                    'd.Text = Infra.TraductorMgr.TraducirControl(d.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                    If Not d.Name = "PrincipalInicio" Then
    '                        d.Enabled = tieneAcceso(d.Name)
    '                    End If
    '                    'If d.DropDown.Items.Count > 0 Then
    '                    '    For Each i As ToolStripMenuItem In d.DropDown.Items
    '                    '        i.Text = Infra.TraductorMgr.TraducirControl(i.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                    '        If i.DropDown.Items.Count > 0 Then
    '                    '            For Each j As ToolStripMenuItem In i.DropDown.Items
    '                    '                j.Text = Infra.TraductorMgr.TraducirControl(j.Name, BE.UsuarioSession.getUser().Idioma.Id)
    '                    '            Next
    '                    '        End If
    '                    '    Next
    '                    'End If

    '                Next

    '            End If
    '        Next

    '    End Sub

    '    Public Sub traducirFormsAbiertos(ByVal f As Form)
    '        'busco todos los forms abiertos
    '        'el mdi
    '        Me.traducirForm(f)
    '        For Each openForm In f.MdiChildren
    '            Me.traducirForm(openForm)
    '        Next

    '    End Sub
End Class
