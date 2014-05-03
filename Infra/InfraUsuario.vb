
Public Class InfraUsuario

    Private frase As String = Configuration.ConfigurationManager.AppSettings("frase")
    Public Function validarCredenciales(ByRef oUser As BE.BEUsuarioBase) As Boolean

        Try
            'recibe un usuario con credenciales par validar y devuelve un entero
            'objeto crypto para password

            Dim crypto As Criptografia.Crypto = Criptografia.Crypto.getCrypto()
            'objeto dal
            Dim oUserDal As New DAL.UsuarioDAL()
            'obj bitacora
            Dim oInfraBita As Infra.Bitacora = Bitacora.getInfraBitacora()

            'encripto la pass
            'Dim sPassword As String = crypto.generarMD5(oUser.Passwd)
            oUser.Passwd = crypto.generarMD5(oUser.Passwd)
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

            dvhString = oUser.Apellido + oUser.Nombre + oUser.Estado + oUser.Email
            dvhString += oUser.Username + oUser.Passwd + Convert.ToString(oUser.Idioma.Id) + Convert.ToString(oUser.Patente.codigo)
            oUser.Dvh = Criptografia.Crypto.getCrypto.generarMD5(dvhString)

            'hash pass
            oUser.Passwd = Criptografia.Crypto.getCrypto().generarMD5(oUser.Passwd)

            'encrypt mail
            oUser.Email = Criptografia.Crypto.getCrypto().CypherTripleDES(oUser.Email, frase, True)

            ret = (oUserDal.Agregar(oUser))
            If ret Then
                'update dvv
                If Not DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
               
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
            'recorro lo lista y desencripto el mail
            For Each user As BE.BEUsuario In _lista
                user.Email = Criptografia.Crypto.getCrypto().DecypherTripleDES(user.Email, frase, True)
            Next
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
            ' si el password esta vacio es porque no se debe cambiar
            ' pero debemos obtener el actual para generar el dvh

            If oUser.Passwd = "" Then
                Dim temp_user As BE.BEUsuario = New BE.BEUsuario
                temp_user.Username = oUser.Username
                Dim l As List(Of BE.BEUsuario) = New List(Of BE.BEUsuario)
                l = Filtrar(temp_user)
                oUser.Passwd = l.First().Passwd
            Else
                ' encrypt new passwd
                oUser.Passwd = Criptografia.Crypto.getCrypto().generarMD5(oUser.Passwd)
            End If

            

            'get dvh 
            Dim dvhString As String

            dvhString = oUser.Apellido + oUser.Nombre + oUser.Estado + oUser.Email
            dvhString += oUser.Username + oUser.Passwd + Convert.ToString(oUser.Idioma.Id) + Convert.ToString(oUser.Patente.codigo)
            oUser.DVH = Criptografia.Crypto.getCrypto.generarMD5(dvhString)
            ret = (oUserDal.Modificar(oUser))
            If ret Then
                'update dvv
                If Not DVV.Actualizar("Usuario") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If            

            End If
        Catch ex As Exception
            'ex personalizada
            Throw New ExceptionsPersonales.CustomException("ex_modif_user", ex.Message)
        End Try
        Return ret

    End Function
End Class
