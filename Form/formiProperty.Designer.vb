<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormiProperty
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
        Me.btn确定 = New System.Windows.Forms.Button()
        Me.btn取消 = New System.Windows.Forms.Button()
        Me.lbl图号 = New System.Windows.Forms.Label()
        Me.txt图号 = New System.Windows.Forms.TextBox()
        Me.txt文件名 = New System.Windows.Forms.TextBox()
        Me.btn向上1 = New System.Windows.Forms.Button()
        Me.btn向上2 = New System.Windows.Forms.Button()
        Me.cmb描述 = New System.Windows.Forms.ComboBox()
        Me.cmb材料 = New System.Windows.Forms.ComboBox()
        Me.lbl文件名 = New System.Windows.Forms.Label()
        Me.lbl描述 = New System.Windows.Forms.Label()
        Me.lbl材料 = New System.Windows.Forms.Label()
        Me.btn查询 = New System.Windows.Forms.Button()
        Me.lbl供应商 = New System.Windows.Forms.Label()
        Me.cmb供应商 = New System.Windows.Forms.ComboBox()
        Me.txt价格 = New System.Windows.Forms.TextBox()
        Me.lbl价格 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt位置 = New System.Windows.Forms.TextBox()
        Me.btn提取文件名 = New System.Windows.Forms.Button()
        Me.txt质量 = New System.Windows.Forms.TextBox()
        Me.lbl质量 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt生产数量 = New System.Windows.Forms.ComboBox()
        Me.btn保存描述 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn确定
        '
        Me.btn确定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn确定.Location = New System.Drawing.Point(254, 501)
        Me.btn确定.Margin = New System.Windows.Forms.Padding(4)
        Me.btn确定.Name = "btn确定"
        Me.btn确定.Size = New System.Drawing.Size(98, 42)
        Me.btn确定.TabIndex = 10
        Me.btn确定.Text = "确定"
        '
        'btn取消
        '
        Me.btn取消.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn取消.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn取消.Location = New System.Drawing.Point(363, 501)
        Me.btn取消.Margin = New System.Windows.Forms.Padding(4)
        Me.btn取消.Name = "btn取消"
        Me.btn取消.Size = New System.Drawing.Size(98, 42)
        Me.btn取消.TabIndex = 11
        Me.btn取消.Text = "关闭"
        '
        'lbl图号
        '
        Me.lbl图号.AutoSize = True
        Me.lbl图号.Location = New System.Drawing.Point(18, 33)
        Me.lbl图号.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl图号.Name = "lbl图号"
        Me.lbl图号.Size = New System.Drawing.Size(98, 18)
        Me.lbl图号.TabIndex = 1
        Me.lbl图号.Text = "图    号："
        '
        'txt图号
        '
        Me.txt图号.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt图号.Location = New System.Drawing.Point(118, 27)
        Me.txt图号.Margin = New System.Windows.Forms.Padding(4)
        Me.txt图号.Name = "txt图号"
        Me.txt图号.Size = New System.Drawing.Size(276, 28)
        Me.txt图号.TabIndex = 0
        '
        'txt文件名
        '
        Me.txt文件名.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt文件名.Location = New System.Drawing.Point(118, 74)
        Me.txt文件名.Margin = New System.Windows.Forms.Padding(4)
        Me.txt文件名.Name = "txt文件名"
        Me.txt文件名.Size = New System.Drawing.Size(276, 28)
        Me.txt文件名.TabIndex = 1
        '
        'btn向上1
        '
        Me.btn向上1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn向上1.Location = New System.Drawing.Point(412, 22)
        Me.btn向上1.Margin = New System.Windows.Forms.Padding(4)
        Me.btn向上1.Name = "btn向上1"
        Me.btn向上1.Size = New System.Drawing.Size(39, 39)
        Me.btn向上1.TabIndex = 7
        Me.btn向上1.UseVisualStyleBackColor = True
        '
        'btn向上2
        '
        Me.btn向上2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn向上2.Location = New System.Drawing.Point(412, 69)
        Me.btn向上2.Margin = New System.Windows.Forms.Padding(4)
        Me.btn向上2.Name = "btn向上2"
        Me.btn向上2.Size = New System.Drawing.Size(39, 39)
        Me.btn向上2.TabIndex = 8
        Me.btn向上2.UseVisualStyleBackColor = True
        '
        'cmb描述
        '
        Me.cmb描述.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb描述.FormattingEnabled = True
        Me.cmb描述.Items.AddRange(New Object() {"", "见本图", "无图", "无图,×", "无图,L="})
        Me.cmb描述.Location = New System.Drawing.Point(120, 165)
        Me.cmb描述.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb描述.Name = "cmb描述"
        Me.cmb描述.Size = New System.Drawing.Size(276, 26)
        Me.cmb描述.TabIndex = 2
        '
        'cmb材料
        '
        Me.cmb材料.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb材料.FormattingEnabled = True
        Me.cmb材料.Location = New System.Drawing.Point(120, 207)
        Me.cmb材料.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb材料.Name = "cmb材料"
        Me.cmb材料.Size = New System.Drawing.Size(276, 26)
        Me.cmb材料.Sorted = True
        Me.cmb材料.TabIndex = 3
        '
        'lbl文件名
        '
        Me.lbl文件名.AutoSize = True
        Me.lbl文件名.Location = New System.Drawing.Point(18, 80)
        Me.lbl文件名.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl文件名.Name = "lbl文件名"
        Me.lbl文件名.Size = New System.Drawing.Size(98, 18)
        Me.lbl文件名.TabIndex = 9
        Me.lbl文件名.Text = "文 件 名："
        '
        'lbl描述
        '
        Me.lbl描述.AutoSize = True
        Me.lbl描述.Location = New System.Drawing.Point(20, 171)
        Me.lbl描述.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl描述.Name = "lbl描述"
        Me.lbl描述.Size = New System.Drawing.Size(98, 18)
        Me.lbl描述.TabIndex = 10
        Me.lbl描述.Text = "描    述："
        '
        'lbl材料
        '
        Me.lbl材料.AutoSize = True
        Me.lbl材料.Location = New System.Drawing.Point(20, 213)
        Me.lbl材料.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl材料.Name = "lbl材料"
        Me.lbl材料.Size = New System.Drawing.Size(98, 18)
        Me.lbl材料.TabIndex = 11
        Me.lbl材料.Text = "材    料："
        '
        'btn查询
        '
        Me.btn查询.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn查询.Location = New System.Drawing.Point(412, 296)
        Me.btn查询.Margin = New System.Windows.Forms.Padding(4)
        Me.btn查询.Name = "btn查询"
        Me.btn查询.Size = New System.Drawing.Size(39, 39)
        Me.btn查询.TabIndex = 9
        Me.btn查询.UseVisualStyleBackColor = True
        Me.btn查询.Visible = False
        '
        'lbl供应商
        '
        Me.lbl供应商.AutoSize = True
        Me.lbl供应商.Location = New System.Drawing.Point(18, 256)
        Me.lbl供应商.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl供应商.Name = "lbl供应商"
        Me.lbl供应商.Size = New System.Drawing.Size(98, 18)
        Me.lbl供应商.TabIndex = 15
        Me.lbl供应商.Text = "供 应 商："
        '
        'cmb供应商
        '
        Me.cmb供应商.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb供应商.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb供应商.FormattingEnabled = True
        Me.cmb供应商.Items.AddRange(New Object() {"", "标准件", "看板件", "外购件", "外协件", "自制件"})
        Me.cmb供应商.Location = New System.Drawing.Point(120, 250)
        Me.cmb供应商.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb供应商.Name = "cmb供应商"
        Me.cmb供应商.Size = New System.Drawing.Size(272, 26)
        Me.cmb供应商.TabIndex = 5
        '
        'txt价格
        '
        Me.txt价格.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt价格.Location = New System.Drawing.Point(120, 300)
        Me.txt价格.Margin = New System.Windows.Forms.Padding(4)
        Me.txt价格.Name = "txt价格"
        Me.txt价格.Size = New System.Drawing.Size(276, 28)
        Me.txt价格.TabIndex = 6
        '
        'lbl价格
        '
        Me.lbl价格.AutoSize = True
        Me.lbl价格.Location = New System.Drawing.Point(20, 306)
        Me.lbl价格.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl价格.Name = "lbl价格"
        Me.lbl价格.Size = New System.Drawing.Size(98, 18)
        Me.lbl价格.TabIndex = 17
        Me.lbl价格.Text = "价    格："
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 406)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 18)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "位    置："
        '
        'txt位置
        '
        Me.txt位置.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt位置.Location = New System.Drawing.Point(117, 400)
        Me.txt位置.Margin = New System.Windows.Forms.Padding(4)
        Me.txt位置.Multiline = True
        Me.txt位置.Name = "txt位置"
        Me.txt位置.ReadOnly = True
        Me.txt位置.Size = New System.Drawing.Size(344, 85)
        Me.txt位置.TabIndex = 19
        Me.txt位置.TabStop = False
        '
        'btn提取文件名
        '
        Me.btn提取文件名.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn提取文件名.Location = New System.Drawing.Point(22, 501)
        Me.btn提取文件名.Margin = New System.Windows.Forms.Padding(4)
        Me.btn提取文件名.Name = "btn提取文件名"
        Me.btn提取文件名.Size = New System.Drawing.Size(128, 42)
        Me.btn提取文件名.TabIndex = 21
        Me.btn提取文件名.Text = "提取文件名"
        '
        'txt质量
        '
        Me.txt质量.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt质量.Location = New System.Drawing.Point(120, 348)
        Me.txt质量.Margin = New System.Windows.Forms.Padding(4)
        Me.txt质量.Name = "txt质量"
        Me.txt质量.ReadOnly = True
        Me.txt质量.Size = New System.Drawing.Size(276, 28)
        Me.txt质量.TabIndex = 22
        '
        'lbl质量
        '
        Me.lbl质量.AutoSize = True
        Me.lbl质量.Location = New System.Drawing.Point(20, 354)
        Me.lbl质量.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl质量.Name = "lbl质量"
        Me.lbl质量.Size = New System.Drawing.Size(98, 18)
        Me.lbl质量.TabIndex = 23
        Me.lbl质量.Text = "质    量："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 122)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 18)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "数    量："
        '
        'txt生产数量
        '
        Me.txt生产数量.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt生产数量.FormattingEnabled = True
        Me.txt生产数量.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.txt生产数量.Location = New System.Drawing.Point(120, 117)
        Me.txt生产数量.Margin = New System.Windows.Forms.Padding(4)
        Me.txt生产数量.Name = "txt生产数量"
        Me.txt生产数量.Size = New System.Drawing.Size(276, 26)
        Me.txt生产数量.TabIndex = 26
        '
        'btn保存描述
        '
        Me.btn保存描述.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn保存描述.Location = New System.Drawing.Point(430, 165)
        Me.btn保存描述.Margin = New System.Windows.Forms.Padding(4)
        Me.btn保存描述.Name = "btn保存描述"
        Me.btn保存描述.Size = New System.Drawing.Size(39, 39)
        Me.btn保存描述.TabIndex = 20
        Me.btn保存描述.UseVisualStyleBackColor = True
        '
        'FormiProperty
        '
        Me.AcceptButton = Me.btn确定
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn取消
        Me.ClientSize = New System.Drawing.Size(482, 555)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt生产数量)
        Me.Controls.Add(Me.txt质量)
        Me.Controls.Add(Me.lbl质量)
        Me.Controls.Add(Me.btn提取文件名)
        Me.Controls.Add(Me.btn保存描述)
        Me.Controls.Add(Me.txt位置)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt价格)
        Me.Controls.Add(Me.lbl价格)
        Me.Controls.Add(Me.cmb供应商)
        Me.Controls.Add(Me.lbl供应商)
        Me.Controls.Add(Me.btn查询)
        Me.Controls.Add(Me.lbl材料)
        Me.Controls.Add(Me.lbl描述)
        Me.Controls.Add(Me.lbl文件名)
        Me.Controls.Add(Me.cmb材料)
        Me.Controls.Add(Me.cmb描述)
        Me.Controls.Add(Me.btn确定)
        Me.Controls.Add(Me.btn取消)
        Me.Controls.Add(Me.btn向上2)
        Me.Controls.Add(Me.btn向上1)
        Me.Controls.Add(Me.txt文件名)
        Me.Controls.Add(Me.txt图号)
        Me.Controls.Add(Me.lbl图号)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FormiProperty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "iProperty+"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn确定 As System.Windows.Forms.Button
    Friend WithEvents btn取消 As System.Windows.Forms.Button
    Friend WithEvents lbl图号 As System.Windows.Forms.Label
    Friend WithEvents txt图号 As System.Windows.Forms.TextBox
    Friend WithEvents txt文件名 As System.Windows.Forms.TextBox
    Friend WithEvents btn向上1 As System.Windows.Forms.Button
    Friend WithEvents btn向上2 As System.Windows.Forms.Button
    Friend WithEvents cmb描述 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb材料 As System.Windows.Forms.ComboBox
    Friend WithEvents lbl文件名 As System.Windows.Forms.Label
    Friend WithEvents lbl描述 As System.Windows.Forms.Label
    Friend WithEvents lbl材料 As System.Windows.Forms.Label
    Friend WithEvents btn查询 As System.Windows.Forms.Button
    Friend WithEvents lbl供应商 As System.Windows.Forms.Label
    Friend WithEvents cmb供应商 As System.Windows.Forms.ComboBox
    Friend WithEvents txt价格 As System.Windows.Forms.TextBox
    Friend WithEvents lbl价格 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt位置 As System.Windows.Forms.TextBox
    Friend WithEvents btn提取文件名 As System.Windows.Forms.Button
    Friend WithEvents txt质量 As System.Windows.Forms.TextBox
    Friend WithEvents lbl质量 As System.Windows.Forms.Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt生产数量 As ComboBox
    Friend WithEvents btn保存描述 As Button
End Class
