Imports System.Web.Script.Serialization

Public Class regenerate
    Inherits System.Web.UI.Page

   


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If (String.IsNullOrEmpty(Request.Cookies("lang").Value)) Then
            ' default
            Session("lang") = 1
        Else
            Session("lang") = Request.Cookies("lang").Value

        End If


    End Sub

 

    Public Function translate(ByVal ctrl_id As String)
        Return Infra.TraductorMgr.TraducirControl(ctrl_id, Session("lang"))
    End Function

End Class