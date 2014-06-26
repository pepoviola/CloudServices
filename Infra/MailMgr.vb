Imports System.Net.Mail
Imports System.Net
Imports System.IO

Public Class MailMgr

    Public Shared Sub sendMail(ByVal body As String, ByVal mail_to As String, ByVal subject As String)

        Dim _username As String = "cloudservices.tfi@gmail.com"
        Dim _pass As String = "TFIUAI2014"
        Dim _host As String = "smtp.gmail.com"
        Dim _port As Integer = 587

        Try
            Dim smtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            smtpServer.Credentials = New Net.NetworkCredential(_username, _pass)
            smtpServer.Port = _port
            smtpServer.Host = _host
            smtpServer.EnableSsl = True

            mail.From = New MailAddress(_username)
            mail.To.Add(mail_to)
            mail.Subject = subject
            mail.Body = body
            smtpServer.Send(mail)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function sendMailByProxy(ByVal body As String, ByVal mail_to As String, ByVal subject As String) As Boolean
        Try
            Dim sURL As String
            sURL = String.Format("http://www.evopig.com.ar/pepo/envia.php?mail={0}&mensaje={1}", mail_to, body)

            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(sURL)

            'Dim myProxy As New WebProxy("myproxy", 80)
            'myProxy.BypassProxyOnLocal = True

            'wrGETURL.Proxy = myProxy
            'o
            wrGETURL.Proxy = WebProxy.GetDefaultProxy()

            'Dim r As WebResponse = wrGETURL.GetResponse()
            'Dim oStream As StreamReader = New StreamReader(r.GetResponseStream())
            'r.GetResponseStream()

            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim i As Integer = 0

            Do While Not sLine Is Nothing
                i += 1
                sLine = objReader.ReadLine
                'If Not sLine Is Nothing Then
                'Console.WriteLine("{0}:{1}", i, sLine)
                'End If
            Loop

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
