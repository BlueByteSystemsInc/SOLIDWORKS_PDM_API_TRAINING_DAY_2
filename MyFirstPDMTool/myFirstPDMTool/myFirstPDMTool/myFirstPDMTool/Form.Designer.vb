<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Connect_Btn = New System.Windows.Forms.Button()
        Me.vaultViews_Combobox = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Undo_Btn = New System.Windows.Forms.Button()
        Me.CheckOut_Btn = New System.Windows.Forms.Button()
        Me.ListAllFiles_Btn = New System.Windows.Forms.Button()
        Me.Folder_TextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Report_TextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Connect_Btn)
        Me.GroupBox1.Controls.Add(Me.vaultViews_Combobox)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(361, 176)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vault Names:"
        '
        'Connect_Btn
        '
        Me.Connect_Btn.Location = New System.Drawing.Point(12, 55)
        Me.Connect_Btn.Name = "Connect_Btn"
        Me.Connect_Btn.Size = New System.Drawing.Size(331, 34)
        Me.Connect_Btn.TabIndex = 4
        Me.Connect_Btn.Text = "Connect..."
        Me.Connect_Btn.UseVisualStyleBackColor = True
        '
        'vaultViews_Combobox
        '
        Me.vaultViews_Combobox.FormattingEnabled = True
        Me.vaultViews_Combobox.Location = New System.Drawing.Point(12, 28)
        Me.vaultViews_Combobox.Name = "vaultViews_Combobox"
        Me.vaultViews_Combobox.Size = New System.Drawing.Size(331, 21)
        Me.vaultViews_Combobox.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Undo_Btn)
        Me.GroupBox2.Controls.Add(Me.CheckOut_Btn)
        Me.GroupBox2.Controls.Add(Me.ListAllFiles_Btn)
        Me.GroupBox2.Controls.Add(Me.Folder_TextBox)
        Me.GroupBox2.Location = New System.Drawing.Point(414, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(374, 176)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Folder:"
        '
        'Undo_Btn
        '
        Me.Undo_Btn.Location = New System.Drawing.Point(249, 55)
        Me.Undo_Btn.Name = "Undo_Btn"
        Me.Undo_Btn.Size = New System.Drawing.Size(111, 34)
        Me.Undo_Btn.TabIndex = 3
        Me.Undo_Btn.Text = "Undo Check Out..."
        Me.Undo_Btn.UseVisualStyleBackColor = True
        '
        'CheckOut_Btn
        '
        Me.CheckOut_Btn.Location = New System.Drawing.Point(132, 55)
        Me.CheckOut_Btn.Name = "CheckOut_Btn"
        Me.CheckOut_Btn.Size = New System.Drawing.Size(111, 34)
        Me.CheckOut_Btn.TabIndex = 2
        Me.CheckOut_Btn.Text = "Check Out Files"
        Me.CheckOut_Btn.UseVisualStyleBackColor = True
        '
        'ListAllFiles_Btn
        '
        Me.ListAllFiles_Btn.Location = New System.Drawing.Point(15, 55)
        Me.ListAllFiles_Btn.Name = "ListAllFiles_Btn"
        Me.ListAllFiles_Btn.Size = New System.Drawing.Size(111, 34)
        Me.ListAllFiles_Btn.TabIndex = 1
        Me.ListAllFiles_Btn.Text = "List Files..."
        Me.ListAllFiles_Btn.UseVisualStyleBackColor = True
        '
        'Folder_TextBox
        '
        Me.Folder_TextBox.Location = New System.Drawing.Point(15, 29)
        Me.Folder_TextBox.Name = "Folder_TextBox"
        Me.Folder_TextBox.Size = New System.Drawing.Size(353, 20)
        Me.Folder_TextBox.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Report_TextBox)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 182)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(788, 256)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Report:"
        '
        'Report_TextBox
        '
        Me.Report_TextBox.AcceptsReturn = True
        Me.Report_TextBox.AcceptsTab = True
        Me.Report_TextBox.Location = New System.Drawing.Point(12, 19)
        Me.Report_TextBox.Multiline = True
        Me.Report_TextBox.Name = "Report_TextBox"
        Me.Report_TextBox.Size = New System.Drawing.Size(762, 214)
        Me.Report_TextBox.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "myFirstPDMTool"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Connect_Btn As Button
    Friend WithEvents vaultViews_Combobox As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Undo_Btn As Button
    Friend WithEvents CheckOut_Btn As Button
    Friend WithEvents ListAllFiles_Btn As Button
    Friend WithEvents Folder_TextBox As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Report_TextBox As TextBox
End Class
