Imports System.Security.Cryptography
Imports System.Text

Public Class SecurityHelper

    Public Shared Function HashPassword(password As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
        End Using
    End Function

End Class

