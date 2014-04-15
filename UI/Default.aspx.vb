﻿Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'instancio el modulo de traducciones
        Dim tradManager As Infra.TraductorMgr = New Infra.TraductorMgr()

        If String.IsNullOrEmpty(Session("auth")) Then
            FormsAuthentication.RedirectToLoginPage()
        End If

       

        'Me.welcome_message.Text = "hola mundo!"
        'Dim m As Label = Master.FindControl("menu_about")
        'm.Text = "pepo"
    End Sub

    Private Sub translateContentPage(ByVal ctrl As Control)
        For Each c As Control In ctrl.Controls
            If c.Controls.Count > 0 Then
                translateContentPage(c)
            Else
                If Not c.ID Is Nothing Then
                    MsgBox(c.GetType().ToString() + " " + c.ID)
                End If
            End If
        Next


    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim utils As Utilidades = Utilidades.getUtilidades()

        For Each c As Control In Page.Controls
            utils.translateContentPage(c)
        Next
    End Sub
End Class