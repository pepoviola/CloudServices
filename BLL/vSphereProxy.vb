Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization







Public Class vSphereProxy

    Public Shared Function CreateVM(ByVal idvm As Integer, vmtype As String) As Integer
        Try
            Dim sURL As String
            sURL = String.Format("http://190.210.166.139/nachito/UAI/vm-{0}/{1}/create", idvm, 1)

            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(sURL)

            ' HABILITAR PARA EL PROXY DE LA UAI
            'Dim myProxy As New WebProxy("10.1.200.251", 80)
            'myProxy.BypassProxyOnLocal = True

            'wrGETURL.Proxy = myProxy



            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim tLine As String = String.Empty
            Dim i As Integer = 0

            Do While Not sLine Is Nothing
                i += 1
                sLine = objReader.ReadLine
                tLine += sLine
                'If Not sLine Is Nothing Then
                'Console.WriteLine("{0}:{1}", i, sLine)
                'End If
            Loop

            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            'Dim lista As IEnumerable(Of Object) = New List(Of Object)
            Dim lista As Dictionary(Of String, String) = New Dictionary(Of String, String)
            lista = jss.Deserialize(tLine, GetType(Dictionary(Of String, String)))

            If lista.Item("status") = 200 Then
                Return lista.Item("task")
            Else
                Throw New Exception
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CheckTask(ByVal idTask As Integer) As Integer
        Try
            Dim sURL As String
            sURL = String.Format("http://190.210.166.139/nachito/UAI/task/{0}", idTask)

            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(sURL)

            ' HABILITAR PARA EL PROXY DE LA UAI
            'Dim myProxy As New WebProxy("10.1.200.251", 80)
            'myProxy.BypassProxyOnLocal = True

            'wrGETURL.Proxy = myProxy



            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim tLine As String = String.Empty
            Dim i As Integer = 0

            Do While Not sLine Is Nothing
                i += 1
                sLine = objReader.ReadLine
                tLine += sLine
                'If Not sLine Is Nothing Then
                'Console.WriteLine("{0}:{1}", i, sLine)
                'End If
            Loop

            Dim jss As JavaScriptSerializer = New JavaScriptSerializer()
            'Dim lista As IEnumerable(Of Object) = New List(Of Object)
            Dim lista As Dictionary(Of String, String) = New Dictionary(Of String, String)
            lista = jss.Deserialize(tLine, GetType(Dictionary(Of String, String)))

            If lista.Item("status") = 200 Then
                Return lista.Item("msg")
            Else
                Throw New Exception
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
