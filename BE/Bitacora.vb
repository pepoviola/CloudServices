
'BE Bitacora
'
'Entidad bitacora
Public Class Bitacora

    


    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _categoria As String
    Public Property Categoria() As String
        Get
            Return _categoria
        End Get
        Set(ByVal value As String)
            _categoria = value
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



    Private _descripcion As String
    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
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


    Private _dvh As String
    Public Property DVH() As String
        Get
            Return _dvh
        End Get
        Set(ByVal value As String)
            _dvh = value
        End Set
    End Property




    'overload
    Public Sub New()

    End Sub

    Public Sub New(ByVal categoria As String, ByVal descrip As String)
        Me.Fecha = DateTime.Now
        Dim oUser As New BEUsuario
        'oUser.IdUsuario = BEUsuario.getUser.IdUsuario
        Me.Usuario = oUser
        Me.Categoria = categoria
        Me.Descripcion = descrip
        Me.DVH = "todo"

    End Sub

End Class
