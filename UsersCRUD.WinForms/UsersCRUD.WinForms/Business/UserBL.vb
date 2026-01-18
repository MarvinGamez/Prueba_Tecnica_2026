Imports UsersCRUD.WinForms.DataAccess
Imports UsersCRUD.WinForms.Entities
Imports UsersCRUD.WinForms.Utilities

Public Class UserBL

    Private ReadOnly dal As New UserDAL()

    Public Function GetUsers() As List(Of User)
        Return dal.GetAll()
    End Function

    Public Sub CreateUser(user As User)
        Try
            user.PasswordHash = SecurityHelper.HashPassword(user.PasswordHash)
            dal.Insert(user)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub UpdateUser(user As User)
        dal.Update(user)
    End Sub

    Public Sub RemoveUser(id As Integer)
        dal.Delete(id)
    End Sub

End Class

