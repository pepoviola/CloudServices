Imports System.Management
Imports System.DirectoryServices

Public Class ManageIIS

    Public Shared Sub restart_site()
        Dim root As String = "IIS://" + System.Environment.MachineName + "/W3SVC"



    End Sub
End Class
