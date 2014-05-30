Imports System.Data.SqlClient

Public Class UsuarioDAL
    Implements ICRUD(Of BE.BEUsuario)


    Public Function validarCredenciales(ByRef oUser As BE.BEUsuarioBase) As Boolean

        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim cmd As IDbCommand = dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SelectUserByUsernameAndPassword"
            cmd.Parameters.Add(New SqlParameter("@username", oUser.Username))
            cmd.Parameters.Add(New SqlParameter("@password", oUser.Passwd))

            Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
            Dim drsql = CType(dr, SqlDataReader)

            'for test only
            'Throw New Exception
            If (drsql.HasRows) Then
                While (drsql.Read())
                    oUser.Id = drsql(0)
                End While

                Return True
            Else
                Return False 'login incorrecto
            End If


        Catch ex As Exception
            Throw ex ' definir jerarquia de excepciones

        Finally
            conn.Close()
        End Try

    End Function


    Public Function getProfile(ByRef oUser As BE.BEUsuarioBase) As Boolean
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            conn.Open()
            Dim cmd As IDbCommand = dbManager.getCmd
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SelectUserById"
            cmd.Parameters.Add(New SqlParameter("@Id", oUser.Id))

            Dim dr As IDataReader = dbManager.getDataFromReader(cmd)
            Dim drsql = CType(dr, SqlDataReader)

            If (drsql.HasRows) Then
                While (drsql.Read())
                    'relleno el usuario

                    'patentes
                    Dim oFlia As New BE.BEFamilia(Convert.ToInt32(drsql("IdFamilia")), Convert.ToString(drsql("Descrip")))
                    Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
                    oFlia.Patentes = oFliaDAL.getPatentes(oFlia)
                    With oUser
                        '.Apellido = Convert.ToString(drsql("Apellido"))
                        '.Nombre = Convert.ToString(drsql("Nombre"))
                        .Estado = Convert.ToString(drsql("Estado"))
                        .Idioma = New BE.Idioma(Convert.ToInt32(drsql("IdIdioma")), Convert.ToString(drsql("codigo")), Convert.ToString(drsql("Descrip")))

                        '.Patente = drsql(8)
                        .Patente = oFlia
                        '.Dvh = drsql(9)
                    End With

                End While

                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw ex

        Finally
            conn.Close()
        End Try

    End Function

#Region "comentado"


    'Implements ICRUD(Of InfraEntidades.InfraEnUsuario)




    ' ''Dim conn As ConnectionManager = ConnectionManager.getConnection
    ' ''Dim cmd = New SqlCommand("select CURRENT_TIMESTAMP",conn
    ''Public Function getData() As Integer

    ''    '  SqlConnection(conn = SingletonConnection.Instance.SqlConnection)
    ''    ' As New SqlConnection( _
    ''    '"Server=MSSQL1;uid=xxx;pwd=xxx;database=master")
    ''    Dim retorno As Integer
    ''    'Using 
    ''    Dim connection As SqlConnection = ConnectionManager.getConnection().SqlConn
    ''    connection.Open()
    ''    Dim Command As New SqlCommand
    ''    Command.CommandText = "use tinytickets"
    ''    Command.Connection = connection
    ''    retorno = Command.ExecuteNonQuery()
    ''    'End Using
    ''    Return retorno


    ''End Function
    'Private _sqlStr As String = "Data Source=.\SQLEXPRESS;Initial Catalog=tinytickets;User ID=uai;Password=uai"
    ''Private _conn As SqlConnection = New SqlConnection(sqlStr)
    'Private _cx As SqlConnection

    'Private Function GetConn() As System.Data.SqlClient.SqlConnection
    '    _cx = New SqlConnection(_sqlStr)
    '    Return _cx
    'End Function

    'Public Function login(ByVal oUserEnt As InfraEntidades.InfraEnUsuario) As Boolean
    '    'retorna true or false == login ok || login err
    '    Dim cx = GetConn()
    '    Using cx
    '        Try
    '            cx.Open()
    '            Dim cmd = cx.CreateCommand()
    '            cmd.CommandText = "select * from Usuario where username = @username and password = @password"
    '            cmd.CommandType = CommandType.Text
    '            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = oUserEnt.Username
    '            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = oUserEnt.Password


    '            Dim dr As SqlDataReader = cmd.ExecuteReader()
    '            Return (dr.HasRows)

    '        Catch ex As Exception

    '            Return False
    '        End Try


    '    End Using
    '    Return False

    'End Function

    'Public Function Agregar(ByVal t As InfraEntidades.InfraEnUsuario) As Boolean Implements ICRUD(Of InfraEntidades.InfraEnUsuario).Agregar

    '    'retorna true or false == login ok || login err
    '    Dim cx = GetConn()
    '    Using cx
    '        Try
    '            cx.Open()
    '            Dim cmd = cx.CreateCommand()
    '            cmd.CommandText = "insert into Usuario (Apellido,Nombre, Estado, Legajo, Username, Password,IdIdioma, IdPatente, DVH) values (@Apellido,@Nombre,@Estado,@Legajo,@Username,@Password, " & _
    '                              "@IdIdioma, @IdPatente,@DVH)"
    '            cmd.CommandType = CommandType.Text
    '            cmd.Parameters.Add("@Apellido", SqlDbType.NVarChar, 50).Value = t.Apellido
    '            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = t.Nombre
    '            cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 50).Value = t.Estado
    '            cmd.Parameters.Add("@Legajo", SqlDbType.Int).Value = t.Legajo
    '            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = t.Username
    '            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = t.Password
    '            cmd.Parameters.Add("@IdIdioma", SqlDbType.Int).Value = t.Idioma
    '            cmd.Parameters.Add("@IdPatente", SqlDbType.Int).Value = t.Patente
    '            cmd.Parameters.Add("@DVH", SqlDbType.NVarChar, 50).Value = t.DVH



    '            Dim retorno As Integer
    '            retorno = cmd.ExecuteNonQuery()

    '            'If retorno > 0 Then
    '            '    Return True
    '            'End If
    '            Return retorno

    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '            'Return False
    '        End Try


    '    End Using
    '    Return False

    '    Return True
    'End Function

    'Public Function Eliminar(ByVal t As InfraEntidades.InfraEnUsuario) As Boolean Implements ICRUD(Of InfraEntidades.InfraEnUsuario).Eliminar
    '    Return True
    'End Function

    'Public Function Filtrar(ByVal t As InfraEntidades.InfraEnUsuario) As System.Collections.Generic.List(Of InfraEntidades.InfraEnUsuario) Implements ICRUD(Of InfraEntidades.InfraEnUsuario).Filtrar
    '    Dim lista As New List(Of InfraEntidades.InfraEnUsuario)
    '    Return lista
    'End Function

    'Public Function Listar() As System.Collections.Generic.List(Of InfraEntidades.InfraEnUsuario) Implements ICRUD(Of InfraEntidades.InfraEnUsuario).Listar
    '    Dim lista As New List(Of InfraEntidades.InfraEnUsuario)
    '    Return lista
    'End Function

    'Public Function Modificar(ByVal t As InfraEntidades.InfraEnUsuario) As Boolean Implements ICRUD(Of InfraEntidades.InfraEnUsuario).Modificar
    '    Return True

    'End Function

#End Region

    Public Function Agregar(ByVal t As BE.BEUsuario) As Boolean Implements ICRUD(Of BE.BEUsuario).Agregar
        Dim registros As Integer
        Dim conn As IDbConnection = dbManager.getConnection
        Try

            Dim dvhString As String
            dvhString = t.Apellido + t.Nombre + t.Estado + t.Email
            dvhString += t.Username + t.Passwd + Convert.ToString(t.Idioma.Id) + Convert.ToString(t.Patente.codigo)
            Dim userDvh As String = Criptografia.Crypto.getCrypto.generarMD5(dvhString)

            'get cmd
            Dim cmd As IDbCommand = dbManager.getCmd("InsertUsuario")
            'agrego los params
            dbManager.addParam(cmd, "@Apellido", t.Apellido)
            dbManager.addParam(cmd, "@Nombre", t.Nombre)
            dbManager.addParam(cmd, "@Username", t.Username)
            dbManager.addParam(cmd, "@Password", t.Passwd)
            dbManager.addParam(cmd, "@email", t.Email)
            dbManager.addParam(cmd, "@IdIdioma", t.Idioma.Id)
            dbManager.addParam(cmd, "@IdPatente", t.Patente.codigo)
            dbManager.addParam(cmd, "@DVH", userDvh)
            'open
            conn.Open()
            cmd.Connection = conn
            registros = cmd.ExecuteNonQuery()

        Catch ex As Exception
        Finally
            conn.Close()
        End Try
        Return (registros = 1)
    End Function

    Public Function Eliminar(ByVal t As BE.BEUsuario) As Boolean Implements ICRUD(Of BE.BEUsuario).Eliminar
        Dim registros As Integer
        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'get cmd
            Dim cmd As IDbCommand = dbManager.getCmd("DeleteUsuario2")
            'agrego los params
            dbManager.addParam(cmd, "@IdUsuario", t.Id)
            'open
            conn.Open()
            cmd.Connection = conn
            registros = cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return (registros = 1)
    End Function

    Public Function Filtrar(ByVal t As BE.BEUsuario) As System.Collections.Generic.List(Of BE.BEUsuario) Implements ICRUD(Of BE.BEUsuario).Filtrar
        Dim _lista As New List(Of BE.BEUsuario)


        Dim conn As IDbConnection = dbManager.getConnection
        Try
            'get cmd
            Dim cmd As IDbCommand = dbManager.getCmd("SelectFiltroUsuarios")
            'agrego los params

            If Not t Is Nothing AndAlso Not String.IsNullOrEmpty(t.Apellido) Then
                dbManager.addParam(cmd, "@Apellido", t.Apellido)
            Else
                dbManager.addParam(cmd, "@Apellido", DBNull.Value)
            End If
            If Not t Is Nothing AndAlso Not String.IsNullOrEmpty(t.Nombre) Then
                dbManager.addParam(cmd, "@Nombre", t.Nombre)
            Else
                dbManager.addParam(cmd, "@Nombre", DBNull.Value)
            End If
            If Not t Is Nothing AndAlso Not String.IsNullOrEmpty(t.Username) Then
                dbManager.addParam(cmd, "@Username", t.Username)
            Else
                dbManager.addParam(cmd, "@Username", DBNull.Value)
            End If
            If Not t Is Nothing AndAlso Not t.Idioma Is Nothing AndAlso Not String.IsNullOrEmpty(t.Idioma.Id) Then
                dbManager.addParam(cmd, "@IdIdioma", t.Idioma.Id)
            Else
                dbManager.addParam(cmd, "@IdIdioma", DBNull.Value)
            End If
            'open
            conn.Open()
            cmd.Connection = conn
            Dim lector As SqlDataReader = cmd.ExecuteReader()
            Do While (lector.Read())
                Dim oUser As New BE.BEUsuario
                oUser.Id = Convert.ToInt32(lector("IdUsuario"))
                oUser.Apellido = Convert.ToString(lector("Apellido"))
                oUser.Nombre = Convert.ToString(lector("Nombre"))
                oUser.Username = Convert.ToString(lector("Username"))
                oUser.Passwd = Convert.ToString(lector("Password"))
                oUser.Email = Convert.ToString(lector("Email"))
                'oUser.Dvh = Convert.ToString(lector("DVH"))
                oUser.Estado = Convert.ToString(lector("Estado"))
                oUser.Idioma = New BE.Idioma(Convert.ToInt32(lector("IdIdioma")), _
                                             Convert.ToString(lector("codigo")),
                                             Convert.ToString(lector("Descrip")))
                oUser.Patente = New BE.BEFamilia(Convert.ToInt32(lector("IdPatente")), _
                                                 Convert.ToString(lector("DescripFlia")))

                _lista.Add(oUser)
            Loop

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try

        Return _lista
    End Function

    Public Function Modificar(ByVal t As BE.BEUsuario) As Boolean Implements ICRUD(Of BE.BEUsuario).Modificar
        Dim registros As Integer
        Dim conn As IDbConnection = dbManager.getConnection
        Try

            Dim dvhString As String
            dvhString = t.Apellido + t.Nombre + t.Estado + t.Email
            dvhString += t.Username + t.Passwd + Convert.ToString(t.Idioma.Id) + Convert.ToString(t.Patente.codigo)
            Dim userDvh As String = Criptografia.Crypto.getCrypto.generarMD5(dvhString)

            'get cmd
            Dim cmd As IDbCommand = dbManager.getCmd("UpdateUsuario")
            'agrego los params
            dbManager.addParam(cmd, "@IdUsuario", t.Id)
            dbManager.addParam(cmd, "@Apellido", t.Apellido)
            dbManager.addParam(cmd, "@Nombre", t.Nombre)
            dbManager.addParam(cmd, "@Username", t.Username)
            dbManager.addParam(cmd, "@Password", t.Passwd)
            dbManager.addParam(cmd, "@Email", t.Email)
            dbManager.addParam(cmd, "@Estado", t.Estado)
            dbManager.addParam(cmd, "@IdIdioma", t.Idioma.Id)
            dbManager.addParam(cmd, "@IdPatente", t.Patente.codigo)
            dbManager.addParam(cmd, "@DVH", userDvh)
            'open
            conn.Open()
            cmd.Connection = conn
            registros = cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
        End Try
        Return (registros = 1)
    End Function
End Class
