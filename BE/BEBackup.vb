
'
' Be :: Backup
'
Public Class BEBackup


    Private _IdBkp As Integer
    Public Property Id() As Integer
        Get
            Return _IdBkp
        End Get
        Set(ByVal value As Integer)
            _IdBkp = value
        End Set
    End Property


    Private _filename As String
    Public Property Filename() As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
        End Set
    End Property


    Private _fecha As DateTime
    Public Property Fecha() As DateTime
        Get
            Return _fecha
        End Get
        Set(ByVal value As DateTime)
            _fecha = value
        End Set
    End Property


    Private _usuario As BEUsuario
    Public Property Usuario() As BEUsuario
        Get
            Return _usuario
        End Get
        Set(ByVal value As BEUsuario)
            _usuario = value
        End Set
    End Property





End Class
