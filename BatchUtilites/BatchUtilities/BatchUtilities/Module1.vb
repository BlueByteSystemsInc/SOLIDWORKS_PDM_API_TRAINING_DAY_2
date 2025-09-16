Imports EPDM.Interop.epdm
Imports System.Runtime.InteropServices

Module Program
    Private vault As IEdmVault7

    Sub Main()
        Try
            vault = New EdmVault5()
            vault.LoginAuto("Assemblageddon", 0)
            Console.WriteLine("Connected to vault: " & vault.Name)

            Dim targetPath As String = "C:\Assemblageddon\Speaker"

            AddFolder(targetPath)
            CheckOutFolder(targetPath)
            UpdateDescriptionToCaps(targetPath)
            CheckInFolderContents(targetPath)

            Console.WriteLine(vbCrLf & "All operations complete. Press any key to exit...")
            Console.ReadKey()

        Catch ex As COMException
            Console.WriteLine("PDM COM Error: " & ex.Message)
        Catch ex As Exception
            Console.WriteLine("General Error: " & ex.Message)
        End Try
    End Sub

    ' ------------------------------------------------------------------------
    ' 1. Add a folder in the vault (using IEdmBatchAdd2)
    ' ------------------------------------------------------------------------
    Sub AddFolder(path As String)
        Try
            Dim batchAdder As IEdmBatchAdd2 = CType(vault.CreateUtility(EdmUtility.EdmUtil_BatchAdd), IEdmBatchAdd2)

            ' Add new folder
            batchAdder.AddFolderPath(path, 0, EdmBatchAddFolderFlag.Ebaff_Nothing)

            ' Commit
            Dim added() As EdmFileInfo = Nothing
            batchAdder.CommitAdd(0, added)

            Console.WriteLine("Created folder: " & path)
        Catch ex As Exception
            Console.WriteLine("Error creating folder: " & ex.Message)
        End Try
    End Sub

    ' ------------------------------------------------------------------------
    ' 2. Check out all files in a folder (using IEdmBatchUnlock2 in Lock mode)
    ' ------------------------------------------------------------------------
    Sub CheckOutFolder(path As String)
        Try
            Dim batchUnlocker As IEdmBatchUnlock2 = CType(vault.CreateUtility(EdmUtility.EdmUtil_BatchUnlock), IEdmBatchUnlock2)
            Dim folder As IEdmFolder5 = vault.GetFolderFromPath(path)
            If folder Is Nothing Then
                Console.WriteLine("Folder not found: " & path)
                Return
            End If

            Dim pos As IEdmPos5 = folder.GetFirstFilePosition()
            Dim selections As New List(Of EdmSelItem)

            While Not pos.IsNull
                Dim file As IEdmFile5 = folder.GetNextFile(pos)
                If file IsNot Nothing AndAlso Not file.IsLocked Then
                    Dim sel As New EdmSelItem
                    sel.mlDocID = file.ID
                    sel.mlProjID = folder.ID
                    selections.Add(sel)
                End If
            End While

            If selections.Count > 0 Then
                batchUnlocker.AddSelection(vault, selections.ToArray())
                batchUnlocker.CreateTree(0, EdmUnlockBuildTreeFlags.Eubtf_MayLock)
                batchUnlocker.UnlockFiles(0, Nothing)
                Console.WriteLine($"Checked out {selections.Count} files in {path}")
            End If
        Catch ex As Exception
            Console.WriteLine("Error checking out files: " & ex.Message)
        End Try
    End Sub

    ' ------------------------------------------------------------------------
    ' 3. Update Description variable to uppercase (using IEdmBatchUpdate2)
    ' ------------------------------------------------------------------------
    Sub UpdateDescriptionToCaps(path As String)
        Try
            Dim batchUpdate As IEdmBatchUpdate2 = CType(vault.CreateUtility(EdmUtility.EdmUtil_BatchUpdate), IEdmBatchUpdate2)
            Dim varMgr As IEdmVariableMgr5 = CType(vault, IEdmVariableMgr5)
            Dim descID As Integer = varMgr.GetVariable("Description").ID

            Dim folder As IEdmFolder5 = vault.GetFolderFromPath(path)
            If folder Is Nothing Then
                Console.WriteLine("Folder not found: " & path)
                Return
            End If

            Dim pos As IEdmPos5 = folder.GetFirstFilePosition()
            While Not pos.IsNull
                Dim file As IEdmFile5 = folder.GetNextFile(pos)
                If file IsNot Nothing Then
                    Dim enumVar As IEdmEnumeratorVariable5 = file.GetEnumeratorVariable()
                    Dim val As Object = Nothing
                    enumVar.GetVar("Description", "@", val)

                    If val IsNot Nothing Then
                        Dim newVal As String = val.ToString().ToUpper()
                        batchUpdate.SetVar(file.ID, descID, newVal, "@", EdmBatchFlags.EdmBatch_Nothing)
                    End If
                End If
            End While

            Dim errors() As EdmBatchError2 = Nothing
            batchUpdate.CommitUpdate(errors, Nothing)

            Console.WriteLine($"Updated descriptions to UPPERCASE in {path}")
        Catch ex As Exception
            Console.WriteLine("Error updating descriptions: " & ex.Message)
        End Try
    End Sub

    ' ------------------------------------------------------------------------
    ' 4. Check in all files in a folder (using IEdmBatchUnlock2 in Unlock mode)
    ' ------------------------------------------------------------------------
    Sub CheckInFolderContents(path As String)
        Try
            Dim batchUnlocker As IEdmBatchUnlock2 = CType(vault.CreateUtility(EdmUtility.EdmUtil_BatchUnlock), IEdmBatchUnlock2)
            Dim folder As IEdmFolder5 = vault.GetFolderFromPath(path)
            If folder Is Nothing Then
                Console.WriteLine("Folder not found: " & path)
                Return
            End If

            Dim pos As IEdmPos5 = folder.GetFirstFilePosition()
            Dim selections As New List(Of EdmSelItem)

            While Not pos.IsNull
                Dim file As IEdmFile5 = folder.GetNextFile(pos)
                If file IsNot Nothing AndAlso file.IsLocked Then
                    Dim sel As New EdmSelItem
                    sel.mlDocID = file.ID
                    sel.mlProjID = folder.ID
                    selections.Add(sel)
                End If
            End While

            If selections.Count > 0 Then
                batchUnlocker.AddSelection(vault, selections.ToArray())
                batchUnlocker.CreateTree(0, EdmUnlockBuildTreeFlags.Eubtf_MayUnlock)
                batchUnlocker.Comment = "Checked in by console app"
                batchUnlocker.UnlockFiles(0, Nothing)
                Console.WriteLine($"Checked in {selections.Count} files in {path}")
            End If
        Catch ex As Exception
            Console.WriteLine("Error checking in files: " & ex.Message)
        End Try
    End Sub

End Module