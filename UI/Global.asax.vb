Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Private ReadOnly DummyCacheItemKey As String = "cronJob"

  
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started

        ' chequeo de digitos verificadores
        'Dim oInfraDVV As Infra.DVV = New Infra.DVV
        'Dim oInfraDVH As Infra.DVH = New Infra.DVH
        'Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        'For Each tabla As String In New List(Of String) From {"Usuario", "Bitacora", "Familia"}
        '    listaErrs.AddRange(oInfraDVH.check(tabla))
        '    listaErrs.AddRange(oInfraDVV.check(tabla))
        'Next
        Dim listaErrs As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        listaErrs = Infra.DigitoVerificador.verificarDigitos()

        Application.Set("listaErrs", listaErrs)
        Application.Set("demo", 1)

        'RegisterCacheEntry()


    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
        
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Private Sub RegisterCacheEntry()
        If HttpContext.Current.Cache("DummyCacheItemKey") Is Nothing Then


        Else

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", Nothing,
                DateTime.MaxValue, TimeSpan.FromMinutes(1),
                CacheItemPriority.Normal,
                New CacheItemRemovedCallback(AddressOf Me.CacheItemRemovedCallback))


        End If

    End Sub

    Public Sub CacheItemRemovedCallback(k As String, v As Object, r As CacheItemRemovedReason)

        Try
            BLL.BLTaskMgr.runJob()
        Catch ex As Exception

        End Try
        'DoWork()
    End Sub


End Class