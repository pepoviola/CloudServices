Imports System.Net.Mail

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

End Class
