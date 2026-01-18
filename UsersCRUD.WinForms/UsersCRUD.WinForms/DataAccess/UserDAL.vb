Imports System.Data.SqlClient
Imports UsersCRUD.WinForms.Entities
Imports UsersCRUD.WinForms.Utilities

Public Class UserDAL

    Public Function GetAll() As List(Of User)
        Dim list As New List(Of User)

        Using cn As New SqlConnection(DbConnection.ConnectionString)
            Using cmd As New SqlCommand("spUsers_List", cn)
                cmd.CommandType = CommandType.StoredProcedure
                cn.Open()

                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        list.Add(New User With {
                            .UserId = CInt(dr("UserId")),
                            .FullName = dr("FullName").ToString(),
                            .Username = dr("Username").ToString(),
                            .Email = dr("Email").ToString(),
                            .Role = CInt(dr("Role")),
                            .IsActive = CBool(dr("IsActive"))
                        })
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function


    Public Sub Insert(user As User)
        Using cn As New SqlConnection(DbConnection.ConnectionString)
            Using cmd As New SqlCommand("spUsers_Insert", cn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = user.FullName
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = user.Email
                cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64).Value = user.PasswordHash
                cmd.Parameters.Add("@Role", SqlDbType.Int).Value = user.Role
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Public Sub Update(user As User)
        Using cn As New SqlConnection(DbConnection.ConnectionString)
            Using cmd As New SqlCommand("spUsers_Update", cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@UserId", user.UserId)
                cmd.Parameters.AddWithValue("@FullName", user.FullName)
                cmd.Parameters.AddWithValue("@Email", user.Email)
                cmd.Parameters.AddWithValue("@Role", user.Role)
                cmd.Parameters.AddWithValue("@IsActive", user.IsActive)

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Public Sub Delete(id As Integer)
        Using cn As New SqlConnection(DbConnection.ConnectionString)
            Using cmd As New SqlCommand("spUsers_Delete", cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@UserId", id)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
