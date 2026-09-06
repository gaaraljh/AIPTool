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
        Me.btn关闭 = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btn检查更新 = New System.Windows.Forms.Button()
        Me.lblGitCode = New System.Windows.Forms.LinkLabel()
        Me.lblBilibili = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picWeiXin = New System.Windows.Forms.PictureBox()
        CType(Me.picWeiXin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn关闭
        '
        Me.btn关闭.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn关闭.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn关闭.Location = New System.Drawing.Point(411, 397)
        Me.btn关闭.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn关闭.Name = "btn关闭"
        Me.btn关闭.Size = New System.Drawing.Size(112, 47)
        Me.btn关闭.TabIndex = 40
        Me.btn关闭.Text = "关闭"
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(186, 58)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(9, 0, 4, 0)
        Me.lblVersion.MaximumSize = New System.Drawing.Size(0, 27)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(41, 20)
        Me.lblVersion.TabIndex = 39
        Me.lblVersion.Text = "版本"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Location = New System.Drawing.Point(186, 102)
        Me.lblCopyright.Margin = New System.Windows.Forms.Padding(9, 0, 4, 0)
        Me.lblCopyright.MaximumSize = New System.Drawing.Size(0, 27)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(41, 20)
        Me.lblCopyright.TabIndex = 38
        Me.lblCopyright.Text = "版权"
        Me.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.Location = New System.Drawing.Point(186, 143)
        Me.lblCompanyName.Margin = New System.Windows.Forms.Padding(9, 0, 4, 0)
        Me.lblCompanyName.MaximumSize = New System.Drawing.Size(0, 27)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(73, 20)
        Me.lblCompanyName.TabIndex = 37
        Me.lblCompanyName.Text = "公司名称"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProductName
        '
        Me.lblProductName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Location = New System.Drawing.Point(186, 18)
        Me.lblProductName.Margin = New System.Windows.Forms.Padding(9, 0, 4, 0)
        Me.lblProductName.MaximumSize = New System.Drawing.Size(0, 27)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(73, 20)
        Me.lblProductName.TabIndex = 36
        Me.lblProductName.Text = "产品名称"
        Me.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(190, 178)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(9, 5, 4, 5)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDescription.Size = New System.Drawing.Size(326, 196)
        Me.txtDescription.TabIndex = 41
        Me.txtDescription.TabStop = False
        Me.txtDescription.Text = "说明 :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(在运行时，将用应用程序的程序集信息替换这些标签的文本。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "在"
        '
        'btn检查更新
        '
        Me.btn检查更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn检查更新.Location = New System.Drawing.Point(276, 397)
        Me.btn检查更新.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn检查更新.Name = "btn检查更新"
        Me.btn检查更新.Size = New System.Drawing.Size(112, 47)
        Me.btn检查更新.TabIndex = 44
        Me.btn检查更新.Text = "检查更新"
        Me.btn检查更新.UseVisualStyleBackColor = True
        '
        'lblGitCode
        '
        Me.lblGitCode.AutoSize = True
        Me.lblGitCode.Location = New System.Drawing.Point(32, 413)
        Me.lblGitCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGitCode.Name = "lblGitCode"
        Me.lblGitCode.Size = New System.Drawing.Size(60, 20)
        Me.lblGitCode.TabIndex = 46
        Me.lblGitCode.TabStop = True
        Me.lblGitCode.Text = "GitHub"
        '
        'lblBilibili
        '
        Me.lblBilibili.AutoSize = True
        Me.lblBilibili.Location = New System.Drawing.Point(132, 413)
        Me.lblBilibili.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBilibili.Name = "lblBilibili"
        Me.lblBilibili.Size = New System.Drawing.Size(47, 20)
        Me.lblBilibili.TabIndex = 47
        Me.lblBilibili.TabStop = True
        Me.lblBilibili.Text = "Bilibili"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 203)
        Me.Label1.Margin = New System.Windows.Forms.Padding(9, 0, 4, 0)
        Me.Label1.MaximumSize = New System.Drawing.Size(0, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 20)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "微信扫一扫打赏作者"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picWeiXin
        '
        Me.picWeiXin.Location = New System.Drawing.Point(18, 25)
        Me.picWeiXin.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.picWeiXin.Name = "picWeiXin"
        Me.picWeiXin.Size = New System.Drawing.Size(150, 167)
        Me.picWeiXin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picWeiXin.TabIndex = 49
        Me.picWeiXin.TabStop = False
        '
        'FormAbout
        '
        Me.AcceptButton = Me.btn检查更新
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn关闭
        Me.ClientSize = New System.Drawing.Size(537, 458)
        Me.Controls.Add(Me.picWeiXin)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblBilibili)
        Me.Controls.Add(Me.lblGitCode)
        Me.Controls.Add(Me.btn检查更新)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.btn关闭)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.lblProductName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormAbout"
        Me.Padding = New System.Windows.Forms.Padding(14, 13, 14, 13)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "关于"
        Me.TopMost = True
        CType(Me.picWeiXin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
