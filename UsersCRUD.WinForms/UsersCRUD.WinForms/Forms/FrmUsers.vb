Imports UsersCRUD.WinForms.Business
Imports UsersCRUD.WinForms.Entities

Public Class FrmUsers

    Private bl As New UserBL()
    Private currentId As Integer = 0

    Private Sub FrmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRoles()
        LoadUsers()
    End Sub

    Private Sub LoadRoles()
        cboRole.Items.Clear()
        cboRole.Items.Add("Administrator")
        cboRole.Items.Add("Standard User")
        cboRole.SelectedIndex = 0
    End Sub

    Private Sub LoadUsers()
        dgvUsers.AutoGenerateColumns = True
        dgvUsers.DataSource = bl.GetUsers()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateForm() Then Exit Sub

        Dim u As New User()
        u.FullName = txtFullName.Text.Trim()
        u.Username = txtUsername.Text.Trim()
        u.Email = txtEmail.Text.Trim()
        u.PasswordHash = txtPassword.Text.Trim()
        u.Role = cboRole.SelectedIndex
        u.IsActive = chkIsActive.Checked

        Try
            bl.CreateUser(u)
            LoadUsers()
            ClearForm()
            MessageBox.Show("User created successfully.")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If currentId = 0 Then Exit Sub

        Dim u As New User()
        u.UserId = currentId
        u.FullName = txtFullName.Text.Trim()
        u.Email = txtEmail.Text.Trim()
        u.Role = cboRole.SelectedIndex
        u.IsActive = chkIsActive.Checked

        bl.UpdateUser(u)
        LoadUsers()
        ClearForm()
        MessageBox.Show("User updated successfully.")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If currentId = 0 Then Exit Sub

        bl.RemoveUser(currentId)
        LoadUsers()
        ClearForm()
        MessageBox.Show("User deleted.")
    End Sub

    Private Sub dgvUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellClick
        If e.RowIndex >= 0 Then
            Dim row = dgvUsers.Rows(e.RowIndex)
            currentId = CInt(row.Cells("UserId").Value)
            txtFullName.Text = row.Cells("FullName").Value.ToString()
            txtUsername.Text = row.Cells("Username").Value.ToString()
            txtEmail.Text = row.Cells("Email").Value.ToString()
            cboRole.SelectedIndex = CInt(row.Cells("Role").Value)
            chkIsActive.Checked = CBool(row.Cells("IsActive").Value)
        End If
    End Sub

    Private Function ValidateForm() As Boolean
        If txtFullName.Text = "" Or txtUsername.Text = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Please fill required fields.")
            Return False
        End If
        Return True
    End Function

    Private Sub ClearForm()
        currentId = 0
        txtFullName.Clear()
        txtUsername.Clear()
        txtEmail.Clear()
        txtPassword.Clear()
        chkIsActive.Checked = True

        If cboRole.Items.Count > 0 Then
            cboRole.SelectedIndex = 0
        End If
    End Sub

    Private Sub chkHidePassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkHidePassword.CheckedChanged
        txtPassword.UseSystemPasswordChar = chkHidePassword.Checked
    End Sub



End Class
