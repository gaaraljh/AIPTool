Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPlaceOpenComponent
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btn插入到部件 = New System.Windows.Forms.Button()
        Me.PictureBox缩略图 = New System.Windows.Forms.PictureBox()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Dgw列表 = New System.Windows.Forms.DataGridView()
        CType(Me.PictureBox缩略图, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgw列表, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn插入到部件
        '
        Me.btn插入到部件.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn插入到部件.Location = New System.Drawing.Point(525, 442)
        Me.btn插入到部件.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.btn插入到部件.Name = "btn插入到部件"
        Me.btn插入到部件.Size = New System.Drawing.Size(152, 56)
        Me.btn插入到部件.TabIndex = 2
        Me.btn插入到部件.Text = "插入到部件"
        Me.btn插入到部件.UseVisualStyleBackColor = True
        '
        'PictureBox缩略图
        '
        Me.PictureBox缩略图.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox缩略图.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox缩略图.Location = New System.Drawing.Point(385, 20)
        Me.PictureBox缩略图.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.PictureBox缩略图.Name = "PictureBox缩略图"
        Me.PictureBox缩略图.Size = New System.Drawing.Size(321, 400)
        Me.PictureBox缩略图.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox缩略图.TabIndex = 1
        Me.PictureBox缩略图.TabStop = False
        '
        'BtnClose
        '
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.Location = New System.Drawing.Point(13, 501)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(94, 22)
        Me.BtnClose.TabIndex = 4
        Me.BtnClose.Text = "关闭窗口"
        Me.BtnClose.UseVisualStyleBackColor = True
        Me.BtnClose.Visible = False
        '
        'Dgw列表
        '
        Me.Dgw列表.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Dgw列表.BackgroundColor = System.Drawing.SystemColors.Window
        Me.Dgw列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Dgw列表.DefaultCellStyle = DataGridViewCellStyle1
        Me.Dgw列表.GridColor = System.Drawing.SystemColors.Window
        Me.Dgw列表.Location = New System.Drawing.Point(13, 20)
        Me.Dgw列表.Name = "Dgw列表"
        Me.Dgw列表.RowHeadersWidth = 62
        Me.Dgw列表.RowTemplate.Height = 28
        Me.Dgw列表.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Dgw列表.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgw列表.Size = New System.Drawing.Size(329, 400)
        Me.Dgw列表.TabIndex = 5
        '
        'FormPlaceOpenComponent
        '
        Me.AcceptButton = Me.btn插入到部件
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnClose
        Me.ClientSize = New System.Drawing.Size(722, 538)
        Me.Controls.Add(Me.Dgw列表)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.btn插入到部件)
        Me.Controls.Add(Me.PictureBox缩略图)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormPlaceOpenComponent"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "插入打开的零部件"
        CType(Me.PictureBox缩略图, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgw列表, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn插入到部件 As Button
    Friend WithEvents PictureBox缩略图 As PictureBox
    Friend WithEvents BtnClose As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Dgw列表 As DataGridView
End Class
