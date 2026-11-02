<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSpecification
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
        btn关闭 = New Button()
        GroupBox基础数据 = New GroupBox()
        ToolStrip基础数据 = New ToolStrip()
        配置文件ToolStripButton = New ToolStripButton()
        保存基础数据ToolStripButton = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        添加基础数据ToolStripButton = New ToolStripButton()
        修改基础数据ToolStripButton = New ToolStripButton()
        删除基础数据ToolStripButton = New ToolStripButton()
        TreeView基础数据树 = New TreeView()
        lst基础数据列表 = New ListBox()
        GroupBox技术要求 = New GroupBox()
        GroupBox导入自定义 = New GroupBox()
        txt导入文本 = New System.Windows.Forms.TextBox()
        Label1 = New Label()
        ToolStrip自定义数据 = New ToolStrip()
        新建自定义ToolStripButton = New ToolStripButton()
        保存自定义ToolStripButton = New ToolStripButton()
        导入ToolStripButton = New ToolStripSplitButton()
        导入文件ToolStripMenuItem = New ToolStripMenuItem()
        导输入文本ToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        添加自定义ToolStripButton = New ToolStripButton()
        插入自定义ToolStripButton = New ToolStripButton()
        修改自定义ToolStripButton = New ToolStripButton()
        删除选中项ToolStripButton = New ToolStripButton()
        ToolStripButton6 = New ToolStripSeparator()
        去除编号ToolStripButton1 = New ToolStripButton()
        字体ToolStripButton = New ToolStripButton()
        间距ToolStripTextBox = New ToolStripTextBox()
        上移ToolStripButton = New ToolStripButton()
        下移ToolStripButton = New ToolStripButton()
        TreeView自定义 = New TreeView()
        lst技术要求文本 = New ListBox()
        txt标题文本 = New System.Windows.Forms.TextBox()
        btn取消导入 = New Button()
        btn确定导入 = New Button()
        btn插入 = New Button()
        GroupBox基础数据.SuspendLayout()
        ToolStrip基础数据.SuspendLayout()
        GroupBox技术要求.SuspendLayout()
        GroupBox导入自定义.SuspendLayout()
        ToolStrip自定义数据.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn关闭
        ' 
        btn关闭.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn关闭.DialogResult = DialogResult.Cancel
        btn关闭.Location = New System.Drawing.Point(1232, 1126)
        btn关闭.Margin = New Padding(4, 6, 4, 6)
        btn关闭.Name = "btn关闭"
        btn关闭.Size = New Size(124, 58)
        btn关闭.TabIndex = 3
        btn关闭.Text = "关闭"
        ' 
        ' GroupBox基础数据
        ' 
        GroupBox基础数据.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox基础数据.Controls.Add(ToolStrip基础数据)
        GroupBox基础数据.Controls.Add(TreeView基础数据树)
        GroupBox基础数据.Controls.Add(lst基础数据列表)
        GroupBox基础数据.Font = New Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GroupBox基础数据.Location = New System.Drawing.Point(20, 17)
        GroupBox基础数据.Margin = New Padding(4, 6, 4, 6)
        GroupBox基础数据.Name = "GroupBox基础数据"
        GroupBox基础数据.Padding = New Padding(4, 6, 4, 6)
        GroupBox基础数据.Size = New Size(1336, 555)
        GroupBox基础数据.TabIndex = 0
        GroupBox基础数据.TabStop = False
        GroupBox基础数据.Text = "基础数据"
        ' 
        ' ToolStrip基础数据
        ' 
        ToolStrip基础数据.Font = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ToolStrip基础数据.ImageScalingSize = New Size(24, 24)
        ToolStrip基础数据.Items.AddRange(New ToolStripItem() {配置文件ToolStripButton, 保存基础数据ToolStripButton, ToolStripSeparator2, 添加基础数据ToolStripButton, 修改基础数据ToolStripButton, 删除基础数据ToolStripButton})
        ToolStrip基础数据.Location = New System.Drawing.Point(4, 36)
        ToolStrip基础数据.Name = "ToolStrip基础数据"
        ToolStrip基础数据.Padding = New Padding(0, 0, 3, 0)
        ToolStrip基础数据.Size = New Size(1328, 31)
        ToolStrip基础数据.TabIndex = 0
        ToolStrip基础数据.Text = "ToolStrip2"
        ' 
        ' 配置文件ToolStripButton
        ' 
        配置文件ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        配置文件ToolStripButton.Font = New Font("宋体", 11F)
        配置文件ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        配置文件ToolStripButton.Name = "配置文件ToolStripButton"
        配置文件ToolStripButton.Size = New Size(58, 26)
        配置文件ToolStripButton.Text = "文件"
        配置文件ToolStripButton.ToolTipText = "打开配置文件"
        ' 
        ' 保存基础数据ToolStripButton
        ' 
        保存基础数据ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        保存基础数据ToolStripButton.Font = New Font("宋体", 11F)
        保存基础数据ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        保存基础数据ToolStripButton.Name = "保存基础数据ToolStripButton"
        保存基础数据ToolStripButton.Size = New Size(58, 26)
        保存基础数据ToolStripButton.Text = "保存"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 31)
        ' 
        ' 添加基础数据ToolStripButton
        ' 
        添加基础数据ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        添加基础数据ToolStripButton.Font = New Font("宋体", 11F)
        添加基础数据ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        添加基础数据ToolStripButton.Name = "添加基础数据ToolStripButton"
        添加基础数据ToolStripButton.Size = New Size(58, 26)
        添加基础数据ToolStripButton.Text = "添加"
        ' 
        ' 修改基础数据ToolStripButton
        ' 
        修改基础数据ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        修改基础数据ToolStripButton.Font = New Font("宋体", 11F)
        修改基础数据ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        修改基础数据ToolStripButton.Name = "修改基础数据ToolStripButton"
        修改基础数据ToolStripButton.Size = New Size(58, 26)
        修改基础数据ToolStripButton.Text = "修改"
        ' 
        ' 删除基础数据ToolStripButton
        ' 
        删除基础数据ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        删除基础数据ToolStripButton.Font = New Font("宋体", 11F)
        删除基础数据ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        删除基础数据ToolStripButton.Name = "删除基础数据ToolStripButton"
        删除基础数据ToolStripButton.Size = New Size(58, 26)
        删除基础数据ToolStripButton.Text = "删除"
        ' 
        ' TreeView基础数据树
        ' 
        TreeView基础数据树.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        TreeView基础数据树.Font = New Font("Segoe UI", 11F)
        TreeView基础数据树.Location = New System.Drawing.Point(22, 92)
        TreeView基础数据树.Margin = New Padding(4, 6, 4, 6)
        TreeView基础数据树.Name = "TreeView基础数据树"
        TreeView基础数据树.Size = New Size(346, 456)
        TreeView基础数据树.TabIndex = 1
        ' 
        ' lst基础数据列表
        ' 
        lst基础数据列表.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lst基础数据列表.Font = New Font("Segoe UI", 11F)
        lst基础数据列表.FormattingEnabled = True
        lst基础数据列表.HorizontalScrollbar = True
        lst基础数据列表.ItemHeight = 30
        lst基础数据列表.Location = New System.Drawing.Point(412, 94)
        lst基础数据列表.Margin = New Padding(4, 6, 4, 6)
        lst基础数据列表.Name = "lst基础数据列表"
        lst基础数据列表.Size = New Size(902, 454)
        lst基础数据列表.TabIndex = 2
        ' 
        ' GroupBox技术要求
        ' 
        GroupBox技术要求.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox技术要求.Controls.Add(GroupBox导入自定义)
        GroupBox技术要求.Controls.Add(Label1)
        GroupBox技术要求.Controls.Add(ToolStrip自定义数据)
        GroupBox技术要求.Controls.Add(TreeView自定义)
        GroupBox技术要求.Controls.Add(lst技术要求文本)
        GroupBox技术要求.Controls.Add(txt标题文本)
        GroupBox技术要求.Font = New Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GroupBox技术要求.Location = New System.Drawing.Point(24, 584)
        GroupBox技术要求.Margin = New Padding(4, 6, 4, 6)
        GroupBox技术要求.Name = "GroupBox技术要求"
        GroupBox技术要求.Padding = New Padding(4, 6, 4, 6)
        GroupBox技术要求.Size = New Size(1336, 519)
        GroupBox技术要求.TabIndex = 1
        GroupBox技术要求.TabStop = False
        GroupBox技术要求.Text = "技术要求"
        ' 
        ' GroupBox导入自定义
        ' 
        GroupBox导入自定义.Controls.Add(txt导入文本)
        GroupBox导入自定义.Font = New Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GroupBox导入自定义.Location = New System.Drawing.Point(22, 107)
        GroupBox导入自定义.Margin = New Padding(4, 6, 4, 6)
        GroupBox导入自定义.Name = "GroupBox导入自定义"
        GroupBox导入自定义.Padding = New Padding(4, 6, 4, 6)
        GroupBox导入自定义.Size = New Size(372, 412)
        GroupBox导入自定义.TabIndex = 19
        GroupBox导入自定义.TabStop = False
        GroupBox导入自定义.Text = "导入"
        GroupBox导入自定义.Visible = False
        ' 
        ' txt导入文本
        ' 
        txt导入文本.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txt导入文本.Font = New Font("Segoe UI", 11F)
        txt导入文本.Location = New System.Drawing.Point(11, 42)
        txt导入文本.Margin = New Padding(4, 6, 4, 6)
        txt导入文本.Multiline = True
        txt导入文本.Name = "txt导入文本"
        txt导入文本.Size = New Size(335, 335)
        txt导入文本.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(33, 104)
        Label1.Name = "Label1"
        Label1.Size = New Size(82, 30)
        Label1.TabIndex = 18
        Label1.Text = "模板："
        ' 
        ' ToolStrip自定义数据
        ' 
        ToolStrip自定义数据.Font = New Font("宋体", 11F)
        ToolStrip自定义数据.ImageScalingSize = New Size(24, 24)
        ToolStrip自定义数据.Items.AddRange(New ToolStripItem() {新建自定义ToolStripButton, 保存自定义ToolStripButton, 导入ToolStripButton, ToolStripSeparator1, 添加自定义ToolStripButton, 插入自定义ToolStripButton, 修改自定义ToolStripButton, 删除选中项ToolStripButton, ToolStripButton6, 去除编号ToolStripButton1, 字体ToolStripButton, 间距ToolStripTextBox, 上移ToolStripButton, 下移ToolStripButton})
        ToolStrip自定义数据.Location = New System.Drawing.Point(4, 36)
        ToolStrip自定义数据.Name = "ToolStrip自定义数据"
        ToolStrip自定义数据.Padding = New Padding(0, 0, 3, 0)
        ToolStrip自定义数据.Size = New Size(1328, 35)
        ToolStrip自定义数据.TabIndex = 0
        ToolStrip自定义数据.Text = "ToolStrip1"
        ' 
        ' 新建自定义ToolStripButton
        ' 
        新建自定义ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        新建自定义ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        新建自定义ToolStripButton.Name = "新建自定义ToolStripButton"
        新建自定义ToolStripButton.Size = New Size(58, 30)
        新建自定义ToolStripButton.Text = "新建"
        ' 
        ' 保存自定义ToolStripButton
        ' 
        保存自定义ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        保存自定义ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        保存自定义ToolStripButton.Name = "保存自定义ToolStripButton"
        保存自定义ToolStripButton.Size = New Size(58, 30)
        保存自定义ToolStripButton.Text = "保存"
        ' 
        ' 导入ToolStripButton
        ' 
        导入ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        导入ToolStripButton.DropDownItems.AddRange(New ToolStripItem() {导入文件ToolStripMenuItem, 导输入文本ToolStripMenuItem})
        导入ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        导入ToolStripButton.Name = "导入ToolStripButton"
        导入ToolStripButton.Size = New Size(75, 30)
        导入ToolStripButton.Text = "导入"
        导入ToolStripButton.ToolTipText = "从文本导入"
        ' 
        ' 导入文件ToolStripMenuItem
        ' 
        导入文件ToolStripMenuItem.Name = "导入文件ToolStripMenuItem"
        导入文件ToolStripMenuItem.Size = New Size(220, 34)
        导入文件ToolStripMenuItem.Text = "从文件导入"
        导入文件ToolStripMenuItem.ToolTipText = "从文本文件导入，每行对应导入"
        ' 
        ' 导输入文本ToolStripMenuItem
        ' 
        导输入文本ToolStripMenuItem.Name = "导输入文本ToolStripMenuItem"
        导输入文本ToolStripMenuItem.Size = New Size(220, 34)
        导输入文本ToolStripMenuItem.Text = "从文本导入"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 35)
        ' 
        ' 添加自定义ToolStripButton
        ' 
        添加自定义ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        添加自定义ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        添加自定义ToolStripButton.Name = "添加自定义ToolStripButton"
        添加自定义ToolStripButton.Size = New Size(58, 30)
        添加自定义ToolStripButton.Text = "添加"
        ' 
        ' 插入自定义ToolStripButton
        ' 
        插入自定义ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        插入自定义ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        插入自定义ToolStripButton.Name = "插入自定义ToolStripButton"
        插入自定义ToolStripButton.Size = New Size(58, 30)
        插入自定义ToolStripButton.Text = "插入"
        插入自定义ToolStripButton.ToolTipText = "插入基础数据"
        ' 
        ' 修改自定义ToolStripButton
        ' 
        修改自定义ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        修改自定义ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        修改自定义ToolStripButton.Name = "修改自定义ToolStripButton"
        修改自定义ToolStripButton.Size = New Size(58, 30)
        修改自定义ToolStripButton.Text = "修改"
        ' 
        ' 删除选中项ToolStripButton
        ' 
        删除选中项ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        删除选中项ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        删除选中项ToolStripButton.Name = "删除选中项ToolStripButton"
        删除选中项ToolStripButton.Size = New Size(124, 30)
        删除选中项ToolStripButton.Text = "删除选中项"
        ' 
        ' ToolStripButton6
        ' 
        ToolStripButton6.Name = "ToolStripButton6"
        ToolStripButton6.Size = New Size(6, 35)
        ' 
        ' 去除编号ToolStripButton1
        ' 
        去除编号ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text
        去除编号ToolStripButton1.ImageTransparentColor = Drawing.Color.Magenta
        去除编号ToolStripButton1.Name = "去除编号ToolStripButton1"
        去除编号ToolStripButton1.Size = New Size(102, 30)
        去除编号ToolStripButton1.Text = "去除编号"
        ' 
        ' 字体ToolStripButton
        ' 
        字体ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        字体ToolStripButton.Font = New Font("仿宋", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        字体ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        字体ToolStripButton.Name = "字体ToolStripButton"
        字体ToolStripButton.Size = New Size(58, 30)
        字体ToolStripButton.Text = "仿宋"
        ' 
        ' 间距ToolStripTextBox
        ' 
        间距ToolStripTextBox.Font = New Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        间距ToolStripTextBox.Name = "间距ToolStripTextBox"
        间距ToolStripTextBox.Size = New Size(47, 35)
        间距ToolStripTextBox.Text = "1"
        间距ToolStripTextBox.ToolTipText = "间距"
        ' 
        ' 上移ToolStripButton
        ' 
        上移ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        上移ToolStripButton.Font = New Font("宋体", 11F)
        上移ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        上移ToolStripButton.Name = "上移ToolStripButton"
        上移ToolStripButton.Size = New Size(58, 30)
        上移ToolStripButton.Text = "上移"
        上移ToolStripButton.ToolTipText = "选择行向上移动"
        ' 
        ' 下移ToolStripButton
        ' 
        下移ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        下移ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        下移ToolStripButton.Name = "下移ToolStripButton"
        下移ToolStripButton.Size = New Size(58, 30)
        下移ToolStripButton.Text = "下移"
        下移ToolStripButton.ToolTipText = "选择行向下移动"
        ' 
        ' TreeView自定义
        ' 
        TreeView自定义.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        TreeView自定义.Font = New Font("Segoe UI", 11F)
        TreeView自定义.Location = New System.Drawing.Point(22, 150)
        TreeView自定义.Margin = New Padding(4, 6, 4, 6)
        TreeView自定义.Name = "TreeView自定义"
        TreeView自定义.Size = New Size(346, 334)
        TreeView自定义.TabIndex = 17
        ' 
        ' lst技术要求文本
        ' 
        lst技术要求文本.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lst技术要求文本.Font = New Font("Segoe UI", 11F)
        lst技术要求文本.FormattingEnabled = True
        lst技术要求文本.HorizontalScrollbar = True
        lst技术要求文本.ItemHeight = 30
        lst技术要求文本.Location = New System.Drawing.Point(412, 150)
        lst技术要求文本.Margin = New Padding(4, 6, 4, 6)
        lst技术要求文本.Name = "lst技术要求文本"
        lst技术要求文本.Size = New Size(902, 334)
        lst技术要求文本.TabIndex = 1
        ' 
        ' txt标题文本
        ' 
        txt标题文本.Font = New Font("Segoe UI", 11F)
        txt标题文本.Location = New System.Drawing.Point(412, 94)
        txt标题文本.Margin = New Padding(4, 6, 4, 6)
        txt标题文本.Name = "txt标题文本"
        txt标题文本.Size = New Size(160, 37)
        txt标题文本.TabIndex = 11
        txt标题文本.Text = "技术要求："
        ' 
        ' btn取消导入
        ' 
        btn取消导入.Location = New System.Drawing.Point(185, 1115)
        btn取消导入.Margin = New Padding(4, 6, 4, 6)
        btn取消导入.Name = "btn取消导入"
        btn取消导入.Size = New Size(124, 58)
        btn取消导入.TabIndex = 2
        btn取消导入.Text = "取消"
        btn取消导入.UseVisualStyleBackColor = True
        btn取消导入.Visible = False
        ' 
        ' btn确定导入
        ' 
        btn确定导入.Location = New System.Drawing.Point(53, 1115)
        btn确定导入.Margin = New Padding(4, 6, 4, 6)
        btn确定导入.Name = "btn确定导入"
        btn确定导入.Size = New Size(124, 58)
        btn确定导入.TabIndex = 1
        btn确定导入.Text = "确定"
        btn确定导入.UseVisualStyleBackColor = True
        btn确定导入.Visible = False
        ' 
        ' btn插入
        ' 
        btn插入.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn插入.Location = New System.Drawing.Point(1100, 1126)
        btn插入.Margin = New Padding(4, 6, 4, 6)
        btn插入.Name = "btn插入"
        btn插入.Size = New Size(124, 58)
        btn插入.TabIndex = 2
        btn插入.Text = "插入"
        ' 
        ' FormSpecification
        ' 
        AcceptButton = btn插入
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn关闭
        ClientSize = New Size(1376, 1199)
        Controls.Add(btn确定导入)
        Controls.Add(btn取消导入)
        Controls.Add(GroupBox技术要求)
        Controls.Add(GroupBox基础数据)
        Controls.Add(btn关闭)
        Controls.Add(btn插入)
        Margin = New Padding(4, 6, 4, 6)
        MaximizeBox = False
        Name = "FormSpecification"
        StartPosition = FormStartPosition.CenterScreen
        Text = "技术要求"
        GroupBox基础数据.ResumeLayout(False)
        GroupBox基础数据.PerformLayout()
        ToolStrip基础数据.ResumeLayout(False)
        ToolStrip基础数据.PerformLayout()
        GroupBox技术要求.ResumeLayout(False)
        GroupBox技术要求.PerformLayout()
        GroupBox导入自定义.ResumeLayout(False)
        GroupBox导入自定义.PerformLayout()
        ToolStrip自定义数据.ResumeLayout(False)
        ToolStrip自定义数据.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents btn关闭 As System.Windows.Forms.Button
    Friend WithEvents GroupBox基础数据 As System.Windows.Forms.GroupBox
    Friend WithEvents lst基础数据列表 As System.Windows.Forms.ListBox
    Friend WithEvents TreeView基础数据树 As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox技术要求 As System.Windows.Forms.GroupBox
    Friend WithEvents txt标题文本 As System.Windows.Forms.TextBox
    Friend WithEvents lst技术要求文本 As System.Windows.Forms.ListBox
    Friend WithEvents TreeView自定义 As System.Windows.Forms.TreeView
    Friend WithEvents ToolStrip自定义数据 As System.Windows.Forms.ToolStrip
    Friend WithEvents 新建自定义ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 保存自定义ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 添加自定义ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 插入自定义ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 修改自定义ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 删除选中项ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 字体ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn插入 As System.Windows.Forms.Button
    Friend WithEvents ToolStrip基础数据 As System.Windows.Forms.ToolStrip
    Friend WithEvents 保存基础数据ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 添加基础数据ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 修改基础数据ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 删除基础数据ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 间距ToolStripTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents 配置文件ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 上移ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 下移ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox导入自定义 As System.Windows.Forms.GroupBox
    Friend WithEvents txt导入文本 As System.Windows.Forms.TextBox
    Friend WithEvents 导入ToolStripButton As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents 导入文件ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 导输入文本ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btn取消导入 As System.Windows.Forms.Button
    Friend WithEvents btn确定导入 As System.Windows.Forms.Button
    Friend WithEvents 去除编号ToolStripButton1 As ToolStripButton
    Friend WithEvents Label1 As Label
End Class
