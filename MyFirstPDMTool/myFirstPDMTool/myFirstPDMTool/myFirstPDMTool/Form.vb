Imports EPDM.Interop.epdm

Public Class Form1

    Dim Vault As IEdmVault15 = New EdmVault5Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim vaultNames As String() = Nothing


        Dim Views As EdmViewInfo() = Nothing

        Vault.GetVaultViews(Views, False)

        For Each view In Views
            vaultViews_Combobox.Items.Add(view.mbsVaultName)
        Next




    End Sub

    Private Sub Connect_Btn_Click(sender As Object, e As EventArgs) Handles Connect_Btn.Click

        Vault.LoginAuto(vaultViews_Combobox.Text, Me.Handle.ToInt32)

    End Sub

    Private Sub ListAllFiles_Btn_Click(sender As Object, e As EventArgs) Handles ListAllFiles_Btn.Click

        Report_TextBox.Clear()
        ' get the folder 
        Dim folder As IEdmFolder5 = Vault.GetFolderFromPath(Folder_TextBox.Text)

        'traverse the folder structure
        Dim position As IEdmPos5 = folder.GetFirstFilePosition

        While position.IsNull = False

            Dim iteratorFile As IEdmFile5 = folder.GetNextFile(position)

            Report_TextBox.AppendText($"{iteratorFile.Name}{System.Environment.NewLine}")

        End While

    End Sub

    Private Sub CheckOut_Btn_Click(sender As Object, e As EventArgs) Handles CheckOut_Btn.Click
        Report_TextBox.Clear()
        ' get the folder 
        Dim folder As IEdmFolder5 = Vault.GetFolderFromPath(Folder_TextBox.Text)

        'traverse the folder structure
        Dim position As IEdmPos5 = folder.GetFirstFilePosition

        While position.IsNull = False

            Dim iteratorFile As IEdmFile5 = folder.GetNextFile(position)

            Report_TextBox.AppendText($"{iteratorFile.Name}{System.Environment.NewLine}")

            'check the file out 
            iteratorFile.LockFile(folder.ID, Me.Handle)

            'refresh
            iteratorFile.Refresh()

            Report_TextBox.AppendText($"Check out {iteratorFile.Name} = {iteratorFile.IsLocked} {System.Environment.NewLine}")



        End While
    End Sub

    Private Sub Undo_Btn_Click(sender As Object, e As EventArgs) Handles Undo_Btn.Click
        Report_TextBox.Clear()
        ' get the folder 
        Dim folder As IEdmFolder5 = Vault.GetFolderFromPath(Folder_TextBox.Text)

        'traverse the folder structure
        Dim position As IEdmPos5 = folder.GetFirstFilePosition

        While position.IsNull = False

            Dim iteratorFile As IEdmFile5 = folder.GetNextFile(position)

            Report_TextBox.AppendText($"{iteratorFile.Name}{System.Environment.NewLine}")

            'check the file out 
            iteratorFile.UndoLockFile(folder.ID, Me.Handle)

            'refresh
            iteratorFile.Refresh()

            Report_TextBox.AppendText($"Check out {iteratorFile.Name} = {iteratorFile.IsLocked} {System.Environment.NewLine}")



        End While
    End Sub
End Class
