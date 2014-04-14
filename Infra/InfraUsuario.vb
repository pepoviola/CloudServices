
Public Class InfraUsuario

    Public Function validarCredenciales(ByVal oUser As BE.BEUsuarioBase) As Boolean

        Try
            'recibe un usuario con credenciales par validar y devuelve un entero
            'objeto crypto para password

            Dim crypto As Criptografia.Crypto = Criptografia.Crypto.getCrypto()
            'objeto dal
            Dim oUserDal As New DAL.UsuarioDAL()
            'obj bitacora
            Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()

            'encripto la pass
            'Dim sPassword As String = crypto.generarMD5(oUser.Password)
            'oUser.Passwd = crypto.generarMD5(oUser.Passwd)
            'comparo el login
            Dim retorno = oUserDal.validarCredenciales(oUser)
            If retorno Then
                'debo traer el perfil del usuario
                oUserDal.getProfile(oUser)
                'Listo cargue el perfil
                'guardo en bitacora, creo el obj bitacora
                Dim oBita As New BE.Bitacora
                'Dim oBitaUser As New BE.BEUsuario
                oBita.Categoria = "Login"
                oBita.Descripcion = "Ingreso Correcto"
                oBita.Fecha = Date.Now
                'oBitaUser.Id = oUser.Id
                oBita.Usuario = oUser
                oBita.DVH = "todo"

                oInfraBita.Log(oBita)
            Else
                Return retorno
            End If

            Return True
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrObtenerUsuario")
        End Try

    End Function


    Public Function Agregar(ByVal oUser As BE.BEUsuario) As Boolean
        Dim ret As Boolean
        Try
            'objeto dal
            Dim oUserDal As New DAL.UsuarioDAL
            'get dvh 
            Dim dvhString As String
            'Convert.ToString(oUser.IdUsuario) +
            dvhString = oUser.Apellido + oUser.Nombre + oUser.Estado
            dvhString += oUser.Username + oUser.Passwd + Convert.ToString(oUser.Idioma.Id) + Convert.ToString(oUser.Patente.codigo)
            oUser.DVH = Criptografia.Crypto.getCrypto.generarMD5(dvhString)
            ret = (oUserDal.Agregar(oUser))
            If ret Then
                'update dvv
                If Not DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
                'log in bitacora
                Dim oBita As New BE.Bitacora("Usuarios", "Se creo ok el usuario: " + oUser.Username)
                Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
                oInfraBita.Log(oBita)
            End If
        Catch exCus As ExceptionsPersonales.CustomException
            Throw exCus
        Catch ex As Exception
            'ex personalizada
            Throw New ExceptionsPersonales.CustomException("ErrAgregarUsuario")
        End Try
        Return ret

    End Function

    'Public Function login(ByVal oUser As InfraEntidades.InfraEnUsuario) As Boolean
    '    'objeto crypto para password
    '    Dim crypto As New Crypto()
    '    'objeto dal
    '    Dim oUserDal As New DAL.UsuarioDAL()

    '    'encripto la pass
    '    'Dim sPassword As String = crypto.generarMD5(oUser.Password)
    '    oUser.Password = crypto.generarMD5(oUser.Password)
    '    'comparo el login
    '    'Return oUserDal.login(username, sPassword)
    '    Return oUserDal.login(oUser)
    '    'Return True

    'End Function

    'Public Function Agregar(ByVal oUser As InfraEntidades.InfraEnUsuario) As String


    '    Dim oUserDal As New DAL.UsuarioDAL
    '    Try
    '        Dim ret As Boolean = oUserDal.Agregar(oUser)
    '        Return ret.ToString

    '    Catch ex As Exception
    '        Return ex.Message

    '    End Try


    'End Function

    'Public Function loadProfile(ByVal username) As InfraEntidades.InfraEnUsuario
    '    Dim user As New InfraEntidades.InfraEnUsuario
    '    Return user
    'End Function

    Public Function Filtrar(ByVal filtro As BE.BEUsuario) As List(Of BE.BEUsuario)
        Try
            Dim _lista As New List(Of BE.BEUsuario)
            Dim dal As New DAL.UsuarioDAL
            _lista = dal.Filtrar(filtro)
            Return _lista
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrFiltrarUsers")

        End Try

    End Function

    Public Function Eliminar(ByVal user As BE.BEUsuario) As Boolean
        Dim retorno As Boolean
        Try
            Dim dal As New DAL.UsuarioDAL
            retorno = dal.Eliminar(user)
            If retorno Then
                'bitacora
                Dim oBita As New BE.Bitacora("Usuarios", "Se eliminó con éxito el usuario: " + user.Username)
                Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
                oInfraBita.Log(oBita)
                If Not DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
            Else
                Dim oBita As New BE.Bitacora("Usuarios", "Error al eliminar el usuario: " + user.Username)
                Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
                oInfraBita.Log(oBita)
            End If
           
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrEliminarUsers")

        End Try
        Return retorno
    End Function

    Public Function Modificar(ByVal oUser As BE.BEUsuario) As Boolean

        Dim ret As Boolean
        Try
            'objeto dal
            Dim oUserDal As New DAL.UsuarioDAL
            'get dvh 
            Dim dvhString As String

            dvhString = oUser.Apellido + oUser.Nombre + oUser.Estado
            dvhString += oUser.Username + oUser.Passwd + Convert.ToString(oUser.Idioma.Id) + Convert.ToString(oUser.Patente.codigo)
            oUser.DVH = Criptografia.Crypto.getCrypto.generarMD5(dvhString)
            ret = (oUserDal.Modificar(oUser))
            If ret Then
                'update dvv
                If Not DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
                'log in bitacora
                Dim oBita As New BE.Bitacora("Usuarios", "Se modificó el usuario: " + oUser.Username)
                Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()
                oInfraBita.Log(oBita)

            End If
        Catch ex As Exception
            'ex personalizada
        End Try
        Return ret

    End Function
End Class
