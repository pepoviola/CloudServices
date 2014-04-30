Imports ExceptionsPersonales
Public Class Familia
    Inherits PatenteBasica


    Public Overrides Function getListaCompleta() As System.Collections.Generic.List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        Try
            Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
            _lista = oFliaDAL.getAll
        Catch ex As Exception
            Throw New CustomException("ErrFitrarFlia")
        End Try
        

        Return _lista
    End Function

    Public Overloads Function getListaCompleta(ByVal fliaName As String) As System.Collections.Generic.List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        Try
            Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
            _lista = oFliaDAL.getAll(fliaName)

        Catch ex As Exception
            Throw New CustomException("ErrFitrarFlia")
        End Try
        Return _lista
    End Function


    Public Function getPatentesFromFlia(ByVal oFlia As BE.BEPatenteBasica) As System.Collections.Generic.List(Of BE.BEPatenteBasica)
        Dim _lista As New List(Of BE.BEPatenteBasica)
        Try
            Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
            _lista = oFliaDAL.getPatentes(oFlia)
        Catch ex As Exception
            Throw New CustomException("ErrFitrarFlia")
        End Try
        Return _lista
    End Function

    Public Overrides Function Patentes(ByVal oflia As BE.BEPatenteBasica) As System.Collections.Generic.List(Of BE.BEPatenteBasica)

        Dim _lista As New List(Of BE.BEPatenteBasica)
        Try
            Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
            _lista.AddRange(oFliaDAL.getPatentes(oflia))
        Catch ex As Exception
            Throw New CustomException("ErrFitrarFlia")
        End Try
        Return _lista

    End Function

    Public Function addFamilia(ByVal flia As BE.BEFamilia, ByVal permisos As List(Of Integer)) As Boolean
        'agrego la familia y luego las relaciones en la tabla de permisos
        Dim retorno As Boolean
        Try
            'recorro la lista de permisos y armo el dic
            Dim dictDVH As New Dictionary(Of Integer, String)
            For Each i As Integer In permisos
                dictDVH.Add(i, Criptografia.Crypto.getCrypto.generarMD5(Convert.ToString(flia.codigo) + Convert.ToString(i)))
            Next
            Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
            'if we add a flia it's not native one
            flia.Nativo = 1
            'get the dvh
            flia.DVH = Criptografia.Crypto.getCrypto().generarMD5(flia.descripcion + Convert.ToString(flia.Nativo))
            retorno = oFliaDAL.Save(flia, dictDVH)
            If retorno Then
                If Not DVV.Actualizar("Familia") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
            End If
        Catch ex As Exception
            'lanzar ex personalizada DONE
            'persistir en bitacora
            Throw New CustomException("ErrAgregarFlia")
        End Try
        Return retorno
    End Function

    Public Function delFamilia(ByVal flia As BE.BEFamilia) As Boolean
        Dim registros As Boolean
        Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
        Try
            registros = oFliaDAL.Delete(flia)
            If registros Then
                If Not DVV.Actualizar("Familia") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
            End If

        Catch exsql As SqlClient.SqlException
            If exsql.Number = 547 Then
                Throw New ExceptionsPersonales.CustomException("FliaEnUso")
            Else
                Throw New ExceptionsPersonales.CustomException("ErrEliminarFlia")
            End If
        Catch ex As Exception
            Throw New ExceptionsPersonales.CustomException("ErrEliminarFlia")
        End Try

        Return registros
    End Function

    Public Function modFamilia(ByVal oFlia As BE.BEFamilia, ByVal permisos As List(Of BE.BEPatenteBasica)) As Boolean
        Dim oFliaDAL As DAL.FamiliaDAL = DAL.FamiliaDAL.getFliaDal()
        Try
            'recorro la lista de permisos y armo el dic
            Dim dictDVH As New Dictionary(Of Integer, String)
            For Each i As BE.BEPatenteBasica In permisos
                '    dictDVH.Add(i.codigo, Crypto.getCrypto.generarMD5(Convert.ToString(oFlia.codigo) + Convert.ToString(i.codigo)))
                i.DVH = Criptografia.Crypto.getCrypto.generarMD5(Convert.ToString(oFlia.codigo) + Convert.ToString(i.codigo))
            Next

            If oFliaDAL.Modify(oFlia, permisos) Then
                'bitacora
                Dim obita As New BE.Bitacora("Familia", String.Format("se modificó la familia {0} co néxito", oFlia.descripcion))
                Dim infraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
                infraBita.Log(obita)
                If Not DVV.Actualizar("Familia") Then
                    Throw New ExceptionsPersonales.CustomException("ErrDVV")
                End If
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            'lanzar ex personalizada
            Dim obita As New BE.Bitacora("Familia", String.Format("Error al modificar: {0}", ex.ToString()))
            Dim infraBita As Infra.Bitacora = Infra.Bitacora.getInfraBitacora()
            infraBita.Log(obita)
            Throw (New CustomException("ErrModFlia"))

        End Try
        

    End Function


    'Public Overrides Function Validar(ByVal id As String, ByVal beFlias As BE.BEPatenteBasica) As Boolean

    'End Function
End Class
