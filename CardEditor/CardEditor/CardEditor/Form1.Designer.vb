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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Description_TextBox = New System.Windows.Forms.TextBox()
        Me.SetVar_Btn = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FilesList_ListBox = New System.Windows.Forms.ListBox()
        Me.ListFiles_Btn = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Description_TextBox)
        Me.GroupBox1.Controls.Add(Me.SetVar_Btn)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 338)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(904, 110)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Card Editor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Description:"
        '
        'Description_TextBox
        '
        Me.Description_TextBox.Location = New System.Drawing.Point(86, 38)
        Me.Description_TextBox.Name = "Description_TextBox"
        Me.Description_TextBox.Size = New System.Drawing.Size(494, 20)
        Me.Description_TextBox.TabIndex = 1
        '
        'SetVar_Btn
        '
        Me.SetVar_Btn.Location = New System.Drawing.Point(741, 65)
        Me.SetVar_Btn.Name = "SetVar_Btn"
        Me.SetVar_Btn.Size = New System.Drawing.Size(157, 39)
        Me.SetVar_Btn.TabIndex = 0
        Me.SetVar_Btn.Text = "Set Description"
        Me.SetVar_Btn.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.FilesList_ListBox)
        Me.GroupBox2.Controls.Add(Me.ListFiles_Btn)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(904, 297)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Project Folder:"
        '
        'FilesList_ListBox
        '
        Me.FilesList_ListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilesList_ListBox.FormattingEnabled = True
        Me.FilesList_ListBox.ItemHeight = 39
        Me.FilesList_ListBox.Location = New System.Drawing.Point(20, 19)
        Me.FilesList_ListBox.Name = "FilesList_ListBox"
        Me.FilesList_ListBox.Size = New System.Drawing.Size(878, 199)
        Me.FilesList_ListBox.TabIndex = 4
        '
        'ListFiles_Btn
        '
        Me.ListFiles_Btn.Location = New System.Drawing.Point(741, 252)
        Me.ListFiles_Btn.Name = "ListFiles_Btn"
        Me.ListFiles_Btn.Size = New System.Drawing.Size(157, 39)
        Me.ListFiles_Btn.TabIndex = 3
        Me.ListFiles_Btn.Text = "List Files"
        Me.ListFiles_Btn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(928, 460)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "CardEditor"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Description_TextBox As TextBox
    Friend WithEvents SetVar_Btn As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents FilesList_ListBox As ListBox
    Friend WithEvents ListFiles_Btn As Button
End Class
