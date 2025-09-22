Imports EPDM.Interop.epdm
Imports Microsoft.Win32.SafeHandles
Imports System.Drawing
Imports System.IO
Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports System.Windows.Forms



Module Program
    Private vault As IEdmVault7

    Sub Main()
        Try
            vault = New EdmVault5()
            vault.LoginAuto("Assemblageddon", 0)
            Console.WriteLine("Connected to vault: " & vault.Name)

            Dim targetPath As String = "C:\Assemblageddon\Grill Assembly"
            Dim originalPath As String = "C:\Source\Grill Assembly"
            AddFolder(originalPath, targetPath)
            CheckInFolderContents(targetPath)
            CheckOutFolder(targetPath)
            UpdateDescriptionToCaps(targetPath)
            CheckOutFolder(targetPath)

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
    Sub AddFolder(originalFolder As String, path As String)
        Try
            Dim batchAdder As IEdmBatchAdd2 = vault.CreateUtility(EdmUtility.EdmUtil_BatchAdd)

            ' Add new folder
            'batchAdder.AddFolderPath(path, 0, EdmBatchAddFolderFlag.Ebaff_Nothing)

            Dim files As New List(Of String)

            files = (New DirectoryInfo(originalFolder)).GetFiles().Select(Function(f) f.FullName).ToList()

            For Each file As String In files

                batchAdder.AddFileFromPathToPath(file, path, 0)

            Next


            'Dim callback As New BatchCallBack(vault)

            Dim callback As New VisualBatchCallBack(vault)

            callback.Show()

            ' Commit
            Dim added() As EdmFileInfo = Nothing
            batchAdder.CommitAdd(0, added, EdmBatchAddFlag.EdmBaf_ReplaceDuplicateFiles, callback)

            Console.WriteLine($"Created {added.Length} files")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
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
                batchUnlocker.CreateTree(0, EdmUnlockBuildTreeFlags.Eubtf_Nothing)
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


Public Class BatchCallBack
    Implements IEdmCallback6

    Public Property Vault As IEdmVault10

    Sub New(vault As IEdmVault10)
        Me.Vault = vault
    End Sub

    Public Sub SetProgressRange(lBarIndex As Integer, lMax As Integer) Implements IEdmCallback6.SetProgressRange
        Console.WriteLine($"Progress Bar {lBarIndex} Max: {lMax}")
    End Sub

    Public Function SetProgress(lBarIndex As Integer, lPos As Integer, bsMsg As String) As Boolean Implements IEdmCallback6.SetProgress
        Console.WriteLine($"Progress Bar {lBarIndex} Position: {lPos} Message: {bsMsg}")
        Return True
    End Function

    Public Sub SetStatusMessage(lBarIndex As Integer, bsMessage As String) Implements IEdmCallback6.SetStatusMessage
        Console.WriteLine($"Status Bar {lBarIndex} Message: {bsMessage}")
    End Sub

    Public Function MsgBox(lParentWnd As Integer, lMsgID As Integer, bsMsg As String, Optional eType As EdmMBoxType = EdmMBoxType.EdmMbt_OKOnly) As EdmMBoxResult Implements IEdmCallback6.MsgBox

        Dim msgBoxRet As EdmMBoxResult = EdmMBoxResult.EdmMbr_OK

        MsgBox = Vault.MsgBox(lParentWnd, bsMsg, eType, msgBoxRet)

    End Function

    Public Sub Resolve(lParentWnd As Integer, ByRef ppoItems() As EdmCmdData) Implements IEdmCallback6.Resolve

        ' allow the developer to handle conflicts here

    End Sub
End Class



Public Class VisualBatchCallBack
    Inherits Form
    Implements IEdmCallback6

    Private progress As ProgressBar
    Private statusLabel As Windows.Forms.Label

    Public Sub New(vault As IEdmVault5)


        Application.EnableVisualStyles()

        Me.Vault = vault

        ' Window settings
        Me.Text = "Progress Window"
        Me.Width = 400
        Me.Height = 150
        Me.StartPosition = FormStartPosition.CenterScreen

        ' ProgressBar
        progress = New ProgressBar()
        progress.Dock = DockStyle.Top
        progress.Height = 30
        progress.Margin = New Padding(10)
        progress.Style = ProgressBarStyle.Continuous

        ' Label (acts like TextBlock in WPF)
        statusLabel = New Windows.Forms.Label()
        statusLabel.Dock = DockStyle.Top
        statusLabel.Height = 30
        statusLabel.TextAlign = ContentAlignment.MiddleCenter
        statusLabel.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Regular)
        statusLabel.Margin = New Padding(10)
        statusLabel.Text = "Ready..."

        ' Add controls
        Dim panel As New Panel()
        panel.Dock = DockStyle.Fill
        panel.Padding = New Padding(10)
        panel.Controls.Add(statusLabel)
        panel.Controls.Add(progress)

        Me.Controls.Add(panel)


    End Sub

    ''' <summary>
    ''' Updates the progress bar and label message.
    ''' </summary>
    ''' <param name="max">Maximum value of progress bar</param>
    ''' <param name="position">Current progress value</param>
    ''' <param name="message">Status message</param>
    Public Sub UpdateProgress(max As Integer, position As Integer, message As String)
        If max > 0 Then progress.Maximum = max
        If position >= 0 AndAlso position <= progress.Maximum Then
            progress.Value = position
        End If
        statusLabel.Text = message
        Application.DoEvents() ' Flush UI updates
        Me.Update()
    End Sub



    Public Property Vault As IEdmVault10

    Sub New(vault As IEdmVault10)
        Me.Vault = vault
    End Sub

    Public Sub SetProgressRange(lBarIndex As Integer, lMax As Integer) Implements IEdmCallback6.SetProgressRange

        UpdateProgress(lMax, 0, "Starting...")
    End Sub

    Public Function SetProgress(lBarIndex As Integer, lPos As Integer, bsMsg As String) As Boolean Implements IEdmCallback6.SetProgress

        UpdateProgress(progress.Maximum, lPos, bsMsg)

        Return True
    End Function

    Public Sub SetStatusMessage(lBarIndex As Integer, bsMessage As String) Implements IEdmCallback6.SetStatusMessage

        UpdateProgress(progress.Maximum, progress.Value, bsMessage)

    End Sub

    Public Function MsgBox(lParentWnd As Integer, lMsgID As Integer, bsMsg As String, Optional eType As EdmMBoxType = EdmMBoxType.EdmMbt_OKOnly) As EdmMBoxResult Implements IEdmCallback6.MsgBox

        Dim msgBoxRet As EdmMBoxResult = EdmMBoxResult.EdmMbr_OK

        MsgBox = Vault.MsgBox(Me.Handle, bsMsg, eType, msgBoxRet)

    End Function

    Public Sub Resolve(lParentWnd As Integer, ByRef ppoItems() As EdmCmdData) Implements IEdmCallback6.Resolve


        Dim oldMaxValue = progress.Maximum
        Dim oldCurrentValue = progress.Value

        Dim index As Int16

        For Each issue In ppoItems
            index = index + 1
            If issue.mlLongData1 = 0 Then
                Continue For
            End If



            If EnumHelper.HasFlag(issue.mlLongData1, EdmResolveReason.Edmrr_DstExists) Then

                UpdateProgress(ppoItems.Length, index, $"Resolving existing copy issue by deleting... {issue.mbsStrData2}")

                Dim folder As IEdmFolder5 = Nothing
                Dim file As IEdmFile5 = Vault.GetFileFromPath(issue.mbsStrData2, folder)
                folder.DeleteFile(Me.Handle, file.ID, True)


            End If


            UpdateProgress(progress.Maximum, progress.Value, $"Replacing with new copy from {issue.mbsStrData1}")
            issue.mlLongData2 = EdmResolveAction.Edmra_Replace

        Next


        progress.Maximum = oldMaxValue
        progress.Value = oldCurrentValue

    End Sub

End Class


Public Class EnumHelper
    ''' <summary>
    ''' Checks if an integer value contains the specified flag(s).
    ''' </summary>
    Public Shared Function HasFlag(value As Integer, flag As EdmResolveReason) As Boolean
        Return (value And CInt(flag)) = CInt(flag)
    End Function

    ''' <summary>
    ''' Checks if an enum value contains the specified flag(s).
    ''' </summary>
    Public Shared Function HasFlag(value As EdmResolveReason, flag As EdmResolveReason) As Boolean
        Return (value And flag) = flag
    End Function

    ''' <summary>
    ''' Returns all the flags present in a given value.
    ''' </summary>
    Public Shared Function GetFlags(value As EdmResolveReason) As List(Of EdmResolveReason)
        Dim result As New List(Of EdmResolveReason)()
        For Each flag As EdmResolveReason In [Enum].GetValues(GetType(EdmResolveReason))
            If flag <> 0 AndAlso (value And flag) = flag Then
                result.Add(flag)
            End If
        Next
        Return result
    End Function
End Class