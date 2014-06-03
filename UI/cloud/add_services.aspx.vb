Imports System.Web.Script.Serialization

Public Class add_services
    Inherits System.Web.UI.Page

    Private _lista_srv As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
    Private _json_serv As String
    Private _lista_addon As List(Of BE.BEServicioBase) = New List(Of BE.BEServicioBase)
    Private _json_addon As String


    Public ReadOnly Property lista_srv() As List(Of BE.BEServicioBase)
        Get
            Return _lista_srv
        End Get

    End Property

    Public ReadOnly Property json_srv() As String
        Get
            Return _json_serv
        End Get
    End Property

    Public ReadOnly Property lista_addon() As List(Of BE.BEServicioBase)
        Get
            Return _lista_addon
        End Get

    End Property

    Public ReadOnly Property json_addon() As String
        Get
            Return _json_addon
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _lista_srv = BLL.BLServicesFacade.getServicesFacade().ServiciosDisponibles()
        _lista_addon = BLL.BLServicesFacade.getServicesFacade().AdicionalesDisponibles()
        Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
        _json_serv = jss.Serialize(_lista_srv)
        _json_addon = jss.Serialize(_lista_addon)

    End Sub

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class