Imports EPDM.Interop.epdm

Public Class Form1
    Private vault As IEdmVault7
    Private currentFolder As IEdmFolder5
    Private selectedFile As IEdmFile5

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            vault = New EdmVault5()
            vault.LoginAuto("Assemblageddon", Me.Handle.ToInt32())
        Catch ex As Exception
            MessageBox.Show("Login failed: " & ex.Message)
        End Try
    End Sub

    Private Sub ListFiles_Btn_Click(sender As Object, e As EventArgs) Handles ListFiles_Btn.Click
        Try
            FilesList_ListBox.Items.Clear()

            ' Get the "Speaker" folder
            currentFolder = vault.GetFolderFromPath("C:\Assemblageddon\Speaker")

            If currentFolder Is Nothing Then
                MessageBox.Show("Speaker folder not found!")
                Return
            End If

            Dim pos As IEdmPos5 = currentFolder.GetFirstFilePosition()
            While Not pos.IsNull
                Dim file As IEdmFile5 = currentFolder.GetNextFile(pos)
                If file IsNot Nothing Then
                    FilesList_ListBox.Items.Add(file.Name)
                End If
            End While
        Catch ex As Exception
            MessageBox.Show("Error listing files: " & ex.Message)
        End Try
    End Sub

    Private Sub FilesList_ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FilesList_ListBox.SelectedIndexChanged
        Try
            Dim fileName As String = FilesList_ListBox.SelectedItem.ToString()
            selectedFile = currentFolder.GetFile(fileName)

            If selectedFile Is Nothing Then
                MessageBox.Show("Could not get file object.")
                Return
            End If

            ' Get variables enumerator
            Dim enumVar As IEdmEnumeratorVariable5 = selectedFile.GetEnumeratorVariable()
            Dim val As Object = Nothing
            enumVar.GetVar("Description", "@", val)

            Description_TextBox.Text = If(val IsNot Nothing, val.ToString(), "")
        Catch ex As Exception
            MessageBox.Show("Error loading file data: " & ex.Message)
        End Try
    End Sub

    Private Sub SetVar_Btn_Click(sender As Object, e As EventArgs) Handles SetVar_Btn.Click
        Try
            If selectedFile Is Nothing Then
                MessageBox.Show("No file selected.")
                Return
            End If

            ' Check out file
            selectedFile.LockFile(currentFolder.ID, Me.Handle.ToInt32())

            ' Set description variable
            Dim enumVar As IEdmEnumeratorVariable5 = selectedFile.GetEnumeratorVariable()
            enumVar.SetVar("Description", "@", Description_TextBox.Text)
            enumVar.Flush() ' Push to DB

            ' Check in file
            selectedFile.UnlockFile(Me.Handle.ToInt32(), "Updated via API tool")

            MessageBox.Show("Description updated successfully!")
        Catch ex As Exception
            MessageBox.Show("Error updating variable: " & ex.Message)
        End Try
    End Sub
End Class
