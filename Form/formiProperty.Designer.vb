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
        btn确定 = New Button()
        btn取消 = New Button()
        lbl图号 = New Label()
        txt图号 = New System.Windows.Forms.TextBox()
        txt文件名 = New System.Windows.Forms.TextBox()
        btn向上1 = New Button()
        btn向上2 = New Button()
        cmb描述 = New ComboBox()
        cmb材料 = New ComboBox()
        lbl文件名 = New Label()
        lbl描述 = New Label()
        lbl材料 = New Label()
        btn查询 = New Button()
        lbl供应商 = New Label()
        cmb供应商 = New ComboBox()
        txt价格 = New System.Windows.Forms.TextBox()
        lbl价格 = New Label()
        Label1 = New Label()
        txt位置 = New System.Windows.Forms.TextBox()
        btn提取文件名 = New Button()
        txt质量 = New System.Windows.Forms.TextBox()
        lbl质量 = New Label()
        Label2 = New Label()
        txt生产数量 = New ComboBox()
        btn保存描述 = New Button()
        SuspendLayout()
        ' 
        ' btn确定
        ' 
        btn确定.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn确定.Location = New System.Drawing.Point(282, 696)
        btn确定.Margin = New Padding(4, 6, 4, 6)
        btn确定.Name = "btn确定"
        btn确定.Size = New Size(109, 58)
        btn确定.TabIndex = 10
        btn确定.Text = "确定"
        ' 
        ' btn取消
        ' 
        btn取消.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn取消.DialogResult = DialogResult.Cancel
        btn取消.Location = New System.Drawing.Point(403, 696)
        btn取消.Margin = New Padding(4, 6, 4, 6)
        btn取消.Name = "btn取消"
        btn取消.Size = New Size(109, 58)
        btn取消.TabIndex = 11
        btn取消.Text = "关闭"
        ' 
        ' lbl图号
        ' 
        lbl图号.AutoSize = True
        lbl图号.Location = New System.Drawing.Point(20, 46)
        lbl图号.Margin = New Padding(4, 0, 4, 0)
        lbl图号.Name = "lbl图号"
        lbl图号.Size = New Size(89, 25)
        lbl图号.TabIndex = 1
        lbl图号.Text = "图    号："
        ' 
        ' txt图号
        ' 
        txt图号.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt图号.Location = New System.Drawing.Point(131, 38)
        txt图号.Margin = New Padding(4, 6, 4, 6)
        txt图号.Name = "txt图号"
        txt图号.Size = New Size(306, 31)
        txt图号.TabIndex = 0
        ' 
        ' txt文件名
        ' 
        txt文件名.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt文件名.Location = New System.Drawing.Point(131, 103)
        txt文件名.Margin = New Padding(4, 6, 4, 6)
        txt文件名.Name = "txt文件名"
        txt文件名.Size = New Size(306, 31)
        txt文件名.TabIndex = 1
        ' 
        ' btn向上1
        ' 
        btn向上1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn向上1.Location = New System.Drawing.Point(458, 31)
        btn向上1.Margin = New Padding(4, 6, 4, 6)
        btn向上1.Name = "btn向上1"
        btn向上1.Size = New Size(43, 54)
        btn向上1.TabIndex = 7
        btn向上1.UseVisualStyleBackColor = True
        ' 
        ' btn向上2
        ' 
        btn向上2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn向上2.Location = New System.Drawing.Point(458, 96)
        btn向上2.Margin = New Padding(4, 6, 4, 6)
        btn向上2.Name = "btn向上2"
        btn向上2.Size = New Size(43, 54)
        btn向上2.TabIndex = 8
        btn向上2.UseVisualStyleBackColor = True
        ' 
        ' cmb描述
        ' 
        cmb描述.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmb描述.FormattingEnabled = True
        cmb描述.Items.AddRange(New Object() {"", "见本图", "无图", "无图,×", "无图,L="})
        cmb描述.Location = New System.Drawing.Point(133, 229)
        cmb描述.Margin = New Padding(4, 6, 4, 6)
        cmb描述.Name = "cmb描述"
        cmb描述.Size = New Size(306, 33)
        cmb描述.TabIndex = 2
        ' 
        ' cmb材料
        ' 
        cmb材料.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmb材料.FormattingEnabled = True
        cmb材料.Location = New System.Drawing.Point(133, 288)
        cmb材料.Margin = New Padding(4, 6, 4, 6)
        cmb材料.Name = "cmb材料"
        cmb材料.Size = New Size(306, 33)
        cmb材料.Sorted = True
        cmb材料.TabIndex = 3
        ' 
        ' lbl文件名
        ' 
        lbl文件名.AutoSize = True
        lbl文件名.Location = New System.Drawing.Point(20, 111)
        lbl文件名.Margin = New Padding(4, 0, 4, 0)
        lbl文件名.Name = "lbl文件名"
        lbl文件名.Size = New Size(79, 25)
        lbl文件名.TabIndex = 9
        lbl文件名.Text = "名  称："
        ' 
        ' lbl描述
        ' 
        lbl描述.AutoSize = True
        lbl描述.Location = New System.Drawing.Point(22, 237)
        lbl描述.Margin = New Padding(4, 0, 4, 0)
        lbl描述.Name = "lbl描述"
        lbl描述.Size = New Size(89, 25)
        lbl描述.TabIndex = 10
        lbl描述.Text = "描    述："
        ' 
        ' lbl材料
        ' 
        lbl材料.AutoSize = True
        lbl材料.Location = New System.Drawing.Point(22, 296)
        lbl材料.Margin = New Padding(4, 0, 4, 0)
        lbl材料.Name = "lbl材料"
        lbl材料.Size = New Size(89, 25)
        lbl材料.TabIndex = 11
        lbl材料.Text = "材    料："
        ' 
        ' btn查询
        ' 
        btn查询.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn查询.Location = New System.Drawing.Point(458, 411)
        btn查询.Margin = New Padding(4, 6, 4, 6)
        btn查询.Name = "btn查询"
        btn查询.Size = New Size(43, 54)
        btn查询.TabIndex = 9
        btn查询.UseVisualStyleBackColor = True
        btn查询.Visible = False
        ' 
        ' lbl供应商
        ' 
        lbl供应商.AutoSize = True
        lbl供应商.Location = New System.Drawing.Point(20, 356)
        lbl供应商.Margin = New Padding(4, 0, 4, 0)
        lbl供应商.Name = "lbl供应商"
        lbl供应商.Size = New Size(98, 25)
        lbl供应商.TabIndex = 15
        lbl供应商.Text = "供 应 商："
        ' 
        ' cmb供应商
        ' 
        cmb供应商.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmb供应商.DropDownStyle = ComboBoxStyle.DropDownList
        cmb供应商.FormattingEnabled = True
        cmb供应商.Items.AddRange(New Object() {"", "标准件", "看板件", "外购件", "外协件", "自制件"})
        cmb供应商.Location = New System.Drawing.Point(133, 347)
        cmb供应商.Margin = New Padding(4, 6, 4, 6)
        cmb供应商.Name = "cmb供应商"
        cmb供应商.Size = New Size(302, 33)
        cmb供应商.TabIndex = 5
        ' 
        ' txt价格
        ' 
        txt价格.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt价格.Location = New System.Drawing.Point(133, 417)
        txt价格.Margin = New Padding(4, 6, 4, 6)
        txt价格.Name = "txt价格"
        txt价格.Size = New Size(306, 31)
        txt价格.TabIndex = 6
        ' 
        ' lbl价格
        ' 
        lbl价格.AutoSize = True
        lbl价格.Location = New System.Drawing.Point(22, 425)
        lbl价格.Margin = New Padding(4, 0, 4, 0)
        lbl价格.Name = "lbl价格"
        lbl价格.Size = New Size(89, 25)
        lbl价格.TabIndex = 17
        lbl价格.Text = "价    格："
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(22, 564)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(89, 25)
        Label1.TabIndex = 18
        Label1.Text = "位    置："
        ' 
        ' txt位置
        ' 
        txt位置.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt位置.Location = New System.Drawing.Point(130, 556)
        txt位置.Margin = New Padding(4, 6, 4, 6)
        txt位置.Multiline = True
        txt位置.Name = "txt位置"
        txt位置.ReadOnly = True
        txt位置.Size = New Size(382, 116)
        txt位置.TabIndex = 19
        txt位置.TabStop = False
        ' 
        ' btn提取文件名
        ' 
        btn提取文件名.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn提取文件名.Location = New System.Drawing.Point(24, 696)
        btn提取文件名.Margin = New Padding(4, 6, 4, 6)
        btn提取文件名.Name = "btn提取文件名"
        btn提取文件名.Size = New Size(142, 58)
        btn提取文件名.TabIndex = 21
        btn提取文件名.Text = "提取文件名"
        ' 
        ' txt质量
        ' 
        txt质量.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt质量.Location = New System.Drawing.Point(133, 483)
        txt质量.Margin = New Padding(4, 6, 4, 6)
        txt质量.Name = "txt质量"
        txt质量.ReadOnly = True
        txt质量.Size = New Size(306, 31)
        txt质量.TabIndex = 22
        ' 
        ' lbl质量
        ' 
        lbl质量.AutoSize = True
        lbl质量.Location = New System.Drawing.Point(22, 492)
        lbl质量.Margin = New Padding(4, 0, 4, 0)
        lbl质量.Name = "lbl质量"
        lbl质量.Size = New Size(89, 25)
        lbl质量.TabIndex = 23
        lbl质量.Text = "质    量："
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(22, 169)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(89, 25)
        Label2.TabIndex = 27
        Label2.Text = "数    量："
        ' 
        ' txt生产数量
        ' 
        txt生产数量.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txt生产数量.FormattingEnabled = True
        txt生产数量.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        txt生产数量.Location = New System.Drawing.Point(133, 162)
        txt生产数量.Margin = New Padding(4, 6, 4, 6)
        txt生产数量.Name = "txt生产数量"
        txt生产数量.Size = New Size(306, 33)
        txt生产数量.TabIndex = 26
        ' 
        ' btn保存描述
        ' 
        btn保存描述.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn保存描述.Location = New System.Drawing.Point(458, 229)
        btn保存描述.Margin = New Padding(4, 6, 4, 6)
        btn保存描述.Name = "btn保存描述"
        btn保存描述.Size = New Size(43, 54)
        btn保存描述.TabIndex = 20
        btn保存描述.UseVisualStyleBackColor = True
        ' 
        ' FormiProperty
        ' 
        AcceptButton = btn确定
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn取消
        ClientSize = New Size(536, 771)
        Controls.Add(Label2)
        Controls.Add(txt生产数量)
        Controls.Add(txt质量)
        Controls.Add(lbl质量)
        Controls.Add(btn提取文件名)
        Controls.Add(btn保存描述)
        Controls.Add(txt位置)
        Controls.Add(Label1)
        Controls.Add(txt价格)
        Controls.Add(lbl价格)
        Controls.Add(cmb供应商)
        Controls.Add(lbl供应商)
        Controls.Add(btn查询)
        Controls.Add(lbl材料)
        Controls.Add(lbl描述)
        Controls.Add(lbl文件名)
        Controls.Add(cmb材料)
        Controls.Add(cmb描述)
        Controls.Add(btn确定)
        Controls.Add(btn取消)
        Controls.Add(btn向上2)
        Controls.Add(btn向上1)
        Controls.Add(txt文件名)
        Controls.Add(txt图号)
        Controls.Add(lbl图号)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 6, 4, 6)
        MaximizeBox = False
        Name = "FormiProperty"
        StartPosition = FormStartPosition.CenterScreen
        Text = "iProperty+"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()

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
