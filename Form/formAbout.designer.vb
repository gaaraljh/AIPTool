Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormAbout
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAbout))
        btn关闭 = New Button()
        lblVersion = New Label()
        lblCopyright = New Label()
        lblCompanyName = New Label()
        lblProductName = New Label()
        txtDescription = New TextBox()
        btn检查更新 = New Button()
        lblGitCode = New LinkLabel()
        lblBilibili = New LinkLabel()
        Label1 = New Label()
        picWeiXin = New PictureBox()
        CType(picWeiXin, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn关闭
        ' 
        btn关闭.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn关闭.DialogResult = DialogResult.Cancel
        btn关闭.Location = New System.Drawing.Point(457, 496)
        btn关闭.Margin = New Padding(4, 6, 4, 6)
        btn关闭.Name = "btn关闭"
        btn关闭.Size = New Size(124, 59)
        btn关闭.TabIndex = 40
        btn关闭.Text = "关闭"
        ' 
        ' lblVersion
        ' 
        lblVersion.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblVersion.AutoSize = True
        lblVersion.Location = New System.Drawing.Point(207, 72)
        lblVersion.Margin = New Padding(10, 0, 4, 0)
        lblVersion.MaximumSize = New Size(0, 34)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(50, 25)
        lblVersion.TabIndex = 39
        lblVersion.Text = "版本"
        lblVersion.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblCopyright
        ' 
        lblCopyright.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCopyright.AutoSize = True
        lblCopyright.Location = New System.Drawing.Point(207, 128)
        lblCopyright.Margin = New Padding(10, 0, 4, 0)
        lblCopyright.MaximumSize = New Size(0, 34)
        lblCopyright.Name = "lblCopyright"
        lblCopyright.Size = New Size(50, 25)
        lblCopyright.TabIndex = 38
        lblCopyright.Text = "版权"
        lblCopyright.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblCompanyName
        ' 
        lblCompanyName.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCompanyName.AutoSize = True
        lblCompanyName.Location = New System.Drawing.Point(207, 179)
        lblCompanyName.Margin = New Padding(10, 0, 4, 0)
        lblCompanyName.MaximumSize = New Size(0, 34)
        lblCompanyName.Name = "lblCompanyName"
        lblCompanyName.Size = New Size(88, 25)
        lblCompanyName.TabIndex = 37
        lblCompanyName.Text = "公司名称"
        lblCompanyName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblProductName
        ' 
        lblProductName.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblProductName.AutoSize = True
        lblProductName.Location = New System.Drawing.Point(207, 22)
        lblProductName.Margin = New Padding(10, 0, 4, 0)
        lblProductName.MaximumSize = New Size(0, 34)
        lblProductName.Name = "lblProductName"
        lblProductName.Size = New Size(88, 25)
        lblProductName.TabIndex = 36
        lblProductName.Text = "产品名称"
        lblProductName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtDescription
        ' 
        txtDescription.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtDescription.Location = New System.Drawing.Point(211, 222)
        txtDescription.Margin = New Padding(10, 6, 4, 6)
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ReadOnly = True
        txtDescription.ScrollBars = ScrollBars.Both
        txtDescription.Size = New Size(362, 244)
        txtDescription.TabIndex = 41
        txtDescription.TabStop = False
        txtDescription.Text = "说明 :" & vbCrLf & vbCrLf & "(在运行时，将用应用程序的程序集信息替换这些标签的文本。" & vbCrLf & "在"
        ' 
        ' btn检查更新
        ' 
        btn检查更新.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn检查更新.Location = New System.Drawing.Point(307, 496)
        btn检查更新.Margin = New Padding(4, 6, 4, 6)
        btn检查更新.Name = "btn检查更新"
        btn检查更新.Size = New Size(124, 59)
        btn检查更新.TabIndex = 44
        btn检查更新.Text = "检查更新"
        btn检查更新.UseVisualStyleBackColor = True
        ' 
        ' lblGitCode
        ' 
        lblGitCode.AutoSize = True
        lblGitCode.Location = New System.Drawing.Point(36, 516)
        lblGitCode.Margin = New Padding(4, 0, 4, 0)
        lblGitCode.Name = "lblGitCode"
        lblGitCode.Size = New Size(68, 25)
        lblGitCode.TabIndex = 46
        lblGitCode.TabStop = True
        lblGitCode.Text = "GitHub"
        ' 
        ' lblBilibili
        ' 
        lblBilibili.AutoSize = True
        lblBilibili.Location = New System.Drawing.Point(147, 516)
        lblBilibili.Margin = New Padding(4, 0, 4, 0)
        lblBilibili.Name = "lblBilibili"
        lblBilibili.Size = New Size(57, 25)
        lblBilibili.TabIndex = 47
        lblBilibili.TabStop = True
        lblBilibili.Text = "Bilibili"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(16, 254)
        Label1.Margin = New Padding(10, 0, 4, 0)
        Label1.MaximumSize = New Size(0, 34)
        Label1.Name = "Label1"
        Label1.Size = New Size(183, 25)
        Label1.TabIndex = 48
        Label1.Text = "微信扫一扫打赏作者"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' picWeiXin
        ' 
        picWeiXin.Location = New System.Drawing.Point(20, 31)
        picWeiXin.Margin = New Padding(4, 6, 4, 6)
        picWeiXin.Name = "picWeiXin"
        picWeiXin.Size = New Size(167, 209)
        picWeiXin.SizeMode = PictureBoxSizeMode.Zoom
        picWeiXin.TabIndex = 49
        picWeiXin.TabStop = False
        ' 
        ' FormAbout
        ' 
        AcceptButton = btn检查更新
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn关闭
        ClientSize = New Size(597, 572)
        Controls.Add(picWeiXin)
        Controls.Add(Label1)
        Controls.Add(lblBilibili)
        Controls.Add(lblGitCode)
        Controls.Add(btn检查更新)
        Controls.Add(txtDescription)
        Controls.Add(btn关闭)
        Controls.Add(lblVersion)
        Controls.Add(lblCopyright)
        Controls.Add(lblCompanyName)
        Controls.Add(lblProductName)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 6, 4, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormAbout"
        Padding = New Padding(16, 16, 16, 16)
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "关于"
        TopMost = True
        CType(picWeiXin, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn关闭 As System.Windows.Forms.Button
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents btn检查更新 As System.Windows.Forms.Button
    Friend WithEvents lblGitCode As System.Windows.Forms.LinkLabel
    Friend WithEvents lblBilibili As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picWeiXin As PictureBox
End Class
