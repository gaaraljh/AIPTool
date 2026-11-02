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
        components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPlaceOpenComponent))
        btn插入到部件 = New Button()
        PictureBox缩略图 = New PictureBox()
        BtnClose = New Button()
        ToolTip1 = New ToolTip(components)
        Dgw列表 = New DataGridView()
        CType(PictureBox缩略图, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Dgw列表, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn插入到部件
        ' 
        btn插入到部件.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn插入到部件.DialogResult = DialogResult.Cancel
        btn插入到部件.Location = New System.Drawing.Point(758, 524)
        btn插入到部件.Margin = New Padding(4, 8, 4, 8)
        btn插入到部件.Name = "btn插入到部件"
        btn插入到部件.Size = New Size(169, 54)
        btn插入到部件.TabIndex = 2
        btn插入到部件.Text = "插入到部件"
        btn插入到部件.UseVisualStyleBackColor = True
        ' 
        ' PictureBox缩略图
        ' 
        PictureBox缩略图.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PictureBox缩略图.Location = New System.Drawing.Point(481, 25)
        PictureBox缩略图.Margin = New Padding(4, 8, 4, 8)
        PictureBox缩略图.Name = "PictureBox缩略图"
        PictureBox缩略图.Size = New Size(444, 483)
        PictureBox缩略图.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox缩略图.TabIndex = 1
        PictureBox缩略图.TabStop = False
        ' 
        ' BtnClose
        ' 
        BtnClose.DialogResult = DialogResult.Cancel
        BtnClose.Location = New System.Drawing.Point(14, 627)
        BtnClose.Margin = New Padding(4, 8, 4, 8)
        BtnClose.Name = "BtnClose"
        BtnClose.Size = New Size(104, 28)
        BtnClose.TabIndex = 4
        BtnClose.Text = "关闭窗口"
        BtnClose.UseVisualStyleBackColor = True
        BtnClose.Visible = False
        ' 
        ' Dgw列表
        ' 
        Dgw列表.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Dgw列表.BackgroundColor = SystemColors.Window
        Dgw列表.BorderStyle = BorderStyle.None
        Dgw列表.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.Padding = New Padding(0, 1, 0, 1)
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        Dgw列表.DefaultCellStyle = DataGridViewCellStyle1
        Dgw列表.GridColor = SystemColors.Window
        Dgw列表.Location = New System.Drawing.Point(14, 25)
        Dgw列表.Margin = New Padding(3, 4, 3, 4)
        Dgw列表.Name = "Dgw列表"
        Dgw列表.RowHeadersWidth = 62
        Dgw列表.RowTemplate.Height = 28
        Dgw列表.ScrollBars = ScrollBars.Vertical
        Dgw列表.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Dgw列表.Size = New Size(444, 483)
        Dgw列表.TabIndex = 5
        ' 
        ' FormPlaceOpenComponent
        ' 
        AcceptButton = btn插入到部件
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = BtnClose
        ClientSize = New Size(940, 584)
        Controls.Add(Dgw列表)
        Controls.Add(BtnClose)
        Controls.Add(btn插入到部件)
        Controls.Add(PictureBox缩略图)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        Margin = New Padding(4, 8, 4, 8)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormPlaceOpenComponent"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "插入打开的零部件"
        CType(PictureBox缩略图, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Dgw列表, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents btn插入到部件 As Button
    Friend WithEvents PictureBox缩略图 As PictureBox
    Friend WithEvents BtnClose As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Dgw列表 As DataGridView
End Class
