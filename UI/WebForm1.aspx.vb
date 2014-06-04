Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Application.Set("demo", 0)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'ManageIIS.stopt_site()
            Application.Set("demo", 0)
            HttpRuntime.UnloadAppDomain()


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ManageIIS.start_site()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class