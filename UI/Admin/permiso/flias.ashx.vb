Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization


Public Class flias
    Implements System.Web.IHttpHandler, IReadOnlySessionState

    Private is_new As Boolean = False
    ''' <summary>
    ''' genera en server side el html de nodos 
    ''' para el formulario de alta de flia
    ''' </summary>
    ''' <param name="root"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function create_nodes(ByVal root As BE.BEPatenteBasica, ByVal nivel As Integer) As String
        Dim res As String = ""
        ' creo el item
        'If check_node(root) Then
        'res += String.Format("<li class='li_item'><div class='div_item'><img class='expand_p' src='/content/img/Minus.png' /><img class='collapse_p' src='/content/img/Plus.png'/></div><div class='div_item'><input id='chbox-{0}' name='{0}' type='checkbox' checked /></div><div class='div_item'>{1}</div>", root.codigo.ToString(), root.descripcion)
        'Else
        If nivel > 0 And is_new Then
            res += String.Format("<li class='li_item'><div class='div_item'><img class='expand_p' src='/content/img/Minus.png' /><img class='collapse_p' src='/content/img/Plus.png'/></div><div class='div_item'><input id='chbox-{0}' name='{0}' type='checkbox' disabled/></div><div class='div_item'>{1}</div>", root.codigo.ToString(), root.descripcion)
        ElseIf nivel > 1 Then
            res += String.Format("<li class='li_item'><div class='div_item'><img class='expand_p' src='/content/img/Minus.png' /><img class='collapse_p' src='/content/img/Plus.png'/></div><div class='div_item'><input id='chbox-{0}' name='{0}' type='checkbox' disabled/></div><div class='div_item'>{1}</div>", root.codigo.ToString(), root.descripcion)
        Else
            res += String.Format("<li class='li_item'><div class='div_item'><img class='expand_p' src='/content/img/Minus.png' /><img class='collapse_p' src='/content/img/Plus.png'/></div><div class='div_item'><input id='chbox-{0}' name='{0}' type='checkbox'/></div><div class='div_item'>{1}</div>", root.codigo.ToString(), root.descripcion)
        End If

        'End If
        If root.GetType().Name = "BEFamilia" Then

            nivel += 1
            ' iterate over
            res += "<ul class='ul_root'>"
            For Each inner As BE.BEPatenteBasica In CType(root, BE.BEFamilia).Patentes
                res += create_nodes(inner, nivel)
            Next
            res += "</ul>"
        End If

        'fin del nodo
        res += "</li>"
        Return res

    End Function
    ''' <summary>
    '''  GET
    '''   si tiene el querystring con el valor de la flia a editar genera el form
    '''   con esos permisos clickeados
    '''   sino genera el form de alta
    ''' </summary>
    ''' <param name="context"></param>
    ''' <remarks></remarks>
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        ' protect the page
        If String.IsNullOrEmpty(context.Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
            'ElseIf 
        Else
            context.Response.ContentType = "application/json"
            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
            If context.Request.HttpMethod = "POST" Then
                ' es modificacion devuelvo json
                'context.Response.ContentType = "application/json"
                'Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
                'Dim resp As Dictionary(Of String, String) = New Dictionary(Of String, String)
                Try
                    ' do stuffs
                    Dim oFlia As BE.BEFamilia = New BE.BEFamilia
                    Dim permisos As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)

                    For Each ctrl As String In context.Request.Form
                        If ctrl = "flia_code" Then
                            oFlia.descripcion = context.Request.Form.Get("flia_code")
                        ElseIf ctrl = "codpat" Then
                            oFlia.codigo = context.Request.Form.Get("codpat")
                        Else
                            If Not ctrl = oFlia.codigo Then
                                Dim p As BE.BEPermiso = New BE.BEPermiso
                                p.codigo = ctrl
                                permisos.Add(p)
                            End If

                        End If
                    Next
                    'preparo bitacora
                    Dim oBita As New BE.Bitacora
                    Dim oBitaUser As New BE.BEUsuario
                    oBitaUser.Id = context.Session("user_id")
                    ' se pasa a la dal esta logica
                    'oBita.DVH = "todo"
                    oBita.Fecha = Date.Now
                    oBita.Usuario = oBitaUser
                    oBita.Categoria = "Familias"

                    Dim oInfraFlia As Infra.Familia = New Infra.Familia()
                    If oInfraFlia.modFamilia(oFlia, permisos) Then
                        resp.Add("status", "200")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_mod_ok", context.Session("lang")))
                        oBita.Descripcion = "Se modificó con exito la familia " + oFlia.descripcion

                    Else
                        resp.Add("status", "400")
                        resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_mod_err", context.Session("lang")))
                        oBita.Descripcion = "Error al modificar la familia " + oFlia.descripcion
                    End If

                    'guardo en bitacora
                    Dim oInfraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                    oInfraBita.Log(oBita)

                Catch ex As Exception
                    resp.Add("status", "500")
                    resp.Add("msg", Infra.TraductorMgr.TraducirControl("flia_mod_exception", context.Session("lang")))
                End Try
                Dim oRes = jss.Serialize(resp)
                context.Response.Write(oRes)

            Else
                'GETs
                Dim oInfraFlia As Infra.Familia = New Infra.Familia()
                Dim listaPatentes As List(Of BE.BEPatenteBasica) = New List(Of BE.BEPatenteBasica)
                listaPatentes = oInfraFlia.getListaCompleta()

                ' flag
                If context.Request.QueryString.Get("new") = "on" Then
                    is_new = True
                End If
                ' respondo solo el html que tengo que cargar
                Dim modal As String = ""
                modal += "<div class='control-group'>"
                modal += "<label class='control-label' for='flia_code'>" + Utilidades.getUtilidades.translate("flia_code", context.Session("lang")) + "</label>"
                modal += "<div class='controls'><input type='text' name='flia_code' id='flia_code'  /></div></div><div class='patentes_nodes'>"
                modal += "<ul class='ul_root toproot'>"
                For Each p As BE.BEPatenteBasica In listaPatentes
                    modal += create_nodes(p, 0)
                Next
                modal += "</ul></div></div>"
                modal += "<script>biddinchecks()</script>"

                resp.Add("status", "200")
                resp.Add("modalbody", modal)
                'context.Response.Write(modal)
                Dim oRes = jss.Serialize(resp)
                context.Response.Write(oRes)
            End If

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class