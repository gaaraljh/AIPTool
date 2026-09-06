Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormOption
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
        Me.components = New System.ComponentModel.Container()
        Me.btn确定 = New System.Windows.Forms.Button()
        Me.btn关闭 = New System.Windows.Forms.Button()
        Me.chk检查更新 = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip配置文件 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem全局 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem图框替换 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem安装目录 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabPage其他 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbx中间符号 = New System.Windows.Forms.ComboBox()
        Me.GroupBox自动保存 = New System.Windows.Forms.GroupBox()
        Me.cmb时间间隔 = New System.Windows.Forms.ComboBox()
        Me.chk启用自动保存 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl保存文档 = New System.Windows.Forms.Label()
        Me.cmb保存文档 = New System.Windows.Forms.ComboBox()
        Me.GroupBox截图 = New System.Windows.Forms.GroupBox()
        Me.txt高度 = New System.Windows.Forms.TextBox()
        Me.txt宽度 = New System.Windows.Forms.TextBox()
        Me.lbl高度 = New System.Windows.Forms.Label()
        Me.lbl宽度 = New System.Windows.Forms.Label()
        Me.lvw设置图标大小 = New System.Windows.Forms.ListView()
        Me.ch按钮 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ch图标大小 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage展开图 = New System.Windows.Forms.TabPage()
        Me.chk保存展开图到指定文件夹 = New System.Windows.Forms.CheckBox()
        Me.txt工艺文字高 = New System.Windows.Forms.TextBox()
        Me.txt标记孔径上限 = New System.Windows.Forms.TextBox()
        Me.lbl工艺文字高 = New System.Windows.Forms.Label()
        Me.cmb图号材质 = New System.Windows.Forms.ComboBox()
        Me.lbl图号材质 = New System.Windows.Forms.Label()
        Me.lbl标记孔径上限 = New System.Windows.Forms.Label()
        Me.chk展开图隐藏螺纹特征 = New System.Windows.Forms.CheckBox()
        Me.GroupBox展开图模板 = New System.Windows.Forms.GroupBox()
        Me.btn展开图模板 = New System.Windows.Forms.Button()
        Me.txt展开图模板 = New System.Windows.Forms.TextBox()
        Me.lbl展开图模板 = New System.Windows.Forms.Label()
        Me.chk展开图标注 = New System.Windows.Forms.CheckBox()
        Me.GroupBox下 = New System.Windows.Forms.GroupBox()
        Me.cmb向下线宽 = New System.Windows.Forms.ComboBox()
        Me.cmb向下线型 = New System.Windows.Forms.ComboBox()
        Me.lbl颜色下 = New System.Windows.Forms.Label()
        Me.btn向下颜色 = New System.Windows.Forms.Button()
        Me.lbl线宽下 = New System.Windows.Forms.Label()
        Me.lbl线型下 = New System.Windows.Forms.Label()
        Me.GroupBox上 = New System.Windows.Forms.GroupBox()
        Me.cmb向上线宽 = New System.Windows.Forms.ComboBox()
        Me.cmb向上线型 = New System.Windows.Forms.ComboBox()
        Me.lbl颜色上 = New System.Windows.Forms.Label()
        Me.btn向上颜色 = New System.Windows.Forms.Button()
        Me.lbl线宽上 = New System.Windows.Forms.Label()
        Me.lbl线型上 = New System.Windows.Forms.Label()
        Me.TabPage工程图 = New System.Windows.Forms.TabPage()
        Me.chk强制横向 = New System.Windows.Forms.CheckBox()
        Me.chk逆时针序号 = New System.Windows.Forms.CheckBox()
        Me.chk模型匹配检查 = New System.Windows.Forms.CheckBox()
        Me.GroupBox标题栏 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbx增加后缀 = New System.Windows.Forms.ComboBox()
        Me.txt零件图框 = New System.Windows.Forms.TextBox()
        Me.txt部件图框 = New System.Windows.Forms.TextBox()
        Me.lbl零件图框 = New System.Windows.Forms.Label()
        Me.lbl部件图框 = New System.Windows.Forms.Label()
        Me.GroupBox页边距 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown页边距右 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown页边距左 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown页边距下 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown页边距上 = New System.Windows.Forms.NumericUpDown()
        Me.lb页边距 = New System.Windows.Forms.Label()
        Me.GroupBox视图 = New System.Windows.Forms.GroupBox()
        Me.chk标注尺寸 = New System.Windows.Forms.CheckBox()
        Me.lbl前视图 = New System.Windows.Forms.Label()
        Me.chk左视图 = New System.Windows.Forms.CheckBox()
        Me.chk俯视图 = New System.Windows.Forms.CheckBox()
        Me.chk右视图 = New System.Windows.Forms.CheckBox()
        Me.chk仰视图 = New System.Windows.Forms.CheckBox()
        Me.chk工程图螺纹特征 = New System.Windows.Forms.CheckBox()
        Me.rdo显示隐藏线 = New System.Windows.Forms.RadioButton()
        Me.rdo不显示隐藏线 = New System.Windows.Forms.RadioButton()
        Me.chk相切边 = New System.Windows.Forms.CheckBox()
        Me.chk钣金自动展开 = New System.Windows.Forms.CheckBox()
        Me.chk第三视角 = New System.Windows.Forms.CheckBox()
        Me.GroupBox工程图模板 = New System.Windows.Forms.GroupBox()
        Me.btn选择工程图模板 = New System.Windows.Forms.Button()
        Me.txt工程图模板 = New System.Windows.Forms.TextBox()
        Me.lbl模板工程图 = New System.Windows.Forms.Label()
        Me.TabPage模型 = New System.Windows.Forms.TabPage()
        Me.txt钣金厚度前缀 = New System.Windows.Forms.TextBox()
        Me.txt去除后缀 = New System.Windows.Forms.TextBox()
        Me.chk钣金厚度检查 = New System.Windows.Forms.CheckBox()
        Me.lbl去除后缀 = New System.Windows.Forms.Label()
        Me.chk检查重复图号 = New System.Windows.Forms.CheckBox()
        Me.lbl查找文件夹层数 = New System.Windows.Forms.Label()
        Me.NUD查找文件夹层数 = New System.Windows.Forms.NumericUpDown()
        Me.chk另存到子文件夹 = New System.Windows.Forms.CheckBox()
        Me.chk备份工程图 = New System.Windows.Forms.CheckBox()
        Me.GroupBoxERP查询 = New System.Windows.Forms.GroupBox()
        Me.btn选择erp数据库 = New System.Windows.Forms.Button()
        Me.btn更新数据库 = New System.Windows.Forms.Button()
        Me.btn打开erp数据库 = New System.Windows.Forms.Button()
        Me.txt查询列 = New System.Windows.Forms.TextBox()
        Me.lbl编码列 = New System.Windows.Forms.Label()
        Me.txt查找范围 = New System.Windows.Forms.TextBox()
        Me.lbl查询列 = New System.Windows.Forms.Label()
        Me.txt基础数据文件 = New System.Windows.Forms.TextBox()
        Me.lbl基础数据文件 = New System.Windows.Forms.Label()
        Me.GroupBoxBOM导出项目 = New System.Windows.Forms.GroupBox()
        Me.chk使用UTF8编码 = New System.Windows.Forms.CheckBox()
        Me.btn清除 = New System.Windows.Forms.Button()
        Me.btn还原 = New System.Windows.Forms.Button()
        Me.btn添加 = New System.Windows.Forms.Button()
        Me.cmb添加 = New System.Windows.Forms.ComboBox()
        Me.txtBOM导出项 = New System.Windows.Forms.TextBox()
        Me.lblBOM导出项目 = New System.Windows.Forms.Label()
        Me.TabPage常规 = New System.Windows.Forms.TabPage()
        Me.GroupBox快速打印 = New System.Windows.Forms.GroupBox()
        Me.cmb另存为 = New System.Windows.Forms.ComboBox()
        Me.cmb打印机 = New System.Windows.Forms.ComboBox()
        Me.chk匹配A3纸 = New System.Windows.Forms.CheckBox()
        Me.chk签字 = New System.Windows.Forms.CheckBox()
        Me.lbl打印机 = New System.Windows.Forms.Label()
        Me.GroupBox精度设置 = New System.Windows.Forms.GroupBox()
        Me.cmb面积精度 = New System.Windows.Forms.ComboBox()
        Me.lbl面积精度 = New System.Windows.Forms.Label()
        Me.cmb质量精度 = New System.Windows.Forms.ComboBox()
        Me.lbl质量精度 = New System.Windows.Forms.Label()
        Me.GroupBox签字 = New System.Windows.Forms.GroupBox()
        Me.chk短日期 = New System.Windows.Forms.CheckBox()
        Me.chk同时签字 = New System.Windows.Forms.CheckBox()
        Me.txt工程师 = New System.Windows.Forms.TextBox()
        Me.lbl工程师 = New System.Windows.Forms.Label()
        Me.chk签字后打印 = New System.Windows.Forms.CheckBox()
        Me.txt打印日期 = New System.Windows.Forms.TextBox()
        Me.lbl打印日期 = New System.Windows.Forms.Label()
        Me.GroupBox比例映射 = New System.Windows.Forms.GroupBox()
        Me.txt图号 = New System.Windows.Forms.TextBox()
        Me.txt比例 = New System.Windows.Forms.TextBox()
        Me.lbl质量 = New System.Windows.Forms.Label()
        Me.lbl比例 = New System.Windows.Forms.Label()
        Me.btn配置文件 = New System.Windows.Forms.Button()
        Me.GroupBox对称零件iProperty映射 = New System.Windows.Forms.GroupBox()
        Me.txt对称件编码映射 = New System.Windows.Forms.TextBox()
        Me.lbl对称件编码 = New System.Windows.Forms.Label()
        Me.txt对称件文件名映射 = New System.Windows.Forms.TextBox()
        Me.txt对称件图号映射 = New System.Windows.Forms.TextBox()
        Me.lbl对称件文件名 = New System.Windows.Forms.Label()
        Me.lbl对称件图号 = New System.Windows.Forms.Label()
        Me.GroupBoxiProperty映射 = New System.Windows.Forms.GroupBox()
        Me.cmb连接符 = New System.Windows.Forms.ComboBox()
        Me.lbl连接符 = New System.Windows.Forms.Label()
        Me.cmb供应商 = New System.Windows.Forms.ComboBox()
        Me.lbl采购来源 = New System.Windows.Forms.Label()
        Me.cmb存货编码 = New System.Windows.Forms.ComboBox()
        Me.lbl存货编码 = New System.Windows.Forms.Label()
        Me.lbl文件名 = New System.Windows.Forms.Label()
        Me.cmb文件名 = New System.Windows.Forms.ComboBox()
        Me.lbl图号 = New System.Windows.Forms.Label()
        Me.cmb图号 = New System.Windows.Forms.ComboBox()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.ContextMenuStrip配置文件.SuspendLayout()
        Me.TabPage其他.SuspendLayout()
        Me.GroupBox自动保存.SuspendLayout()
        Me.GroupBox截图.SuspendLayout()
        Me.TabPage展开图.SuspendLayout()
        Me.GroupBox展开图模板.SuspendLayout()
        Me.GroupBox下.SuspendLayout()
        Me.GroupBox上.SuspendLayout()
        Me.TabPage工程图.SuspendLayout()
        Me.GroupBox标题栏.SuspendLayout()
        Me.GroupBox页边距.SuspendLayout()
        CType(Me.NumericUpDown页边距右, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown页边距左, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown页边距下, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown页边距上, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox视图.SuspendLayout()
        Me.GroupBox工程图模板.SuspendLayout()
        Me.TabPage模型.SuspendLayout()
        CType(Me.NUD查找文件夹层数, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxERP查询.SuspendLayout()
        Me.GroupBoxBOM导出项目.SuspendLayout()
        Me.TabPage常规.SuspendLayout()
        Me.GroupBox快速打印.SuspendLayout()
        Me.GroupBox精度设置.SuspendLayout()
        Me.GroupBox签字.SuspendLayout()
        Me.GroupBox比例映射.SuspendLayout()
        Me.GroupBox对称零件iProperty映射.SuspendLayout()
        Me.GroupBoxiProperty映射.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn确定
        '
        Me.btn确定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn确定.Location = New System.Drawing.Point(690, 516)
        Me.btn确定.Margin = New System.Windows.Forms.Padding(4)
        Me.btn确定.Name = "btn确定"
        Me.btn确定.Size = New System.Drawing.Size(112, 42)
        Me.btn确定.TabIndex = 1
        Me.btn确定.Text = "确定"
        '
        'btn关闭
        '
        Me.btn关闭.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn关闭.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn关闭.Location = New System.Drawing.Point(826, 516)
        Me.btn关闭.Margin = New System.Windows.Forms.Padding(4)
        Me.btn关闭.Name = "btn关闭"
        Me.btn关闭.Size = New System.Drawing.Size(112, 42)
        Me.btn关闭.TabIndex = 2
        Me.btn关闭.Text = "关闭"
        '
        'chk检查更新
        '
        Me.chk检查更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chk检查更新.AutoSize = True
        Me.chk检查更新.Checked = True
        Me.chk检查更新.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk检查更新.Location = New System.Drawing.Point(38, 527)
        Me.chk检查更新.Margin = New System.Windows.Forms.Padding(4)
        Me.chk检查更新.Name = "chk检查更新"
        Me.chk检查更新.Size = New System.Drawing.Size(160, 22)
        Me.chk检查更新.TabIndex = 3
        Me.chk检查更新.Text = "启动时检查更新"
        Me.chk检查更新.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip配置文件
        '
        Me.ContextMenuStrip配置文件.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip配置文件.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem全局, Me.ToolStripMenuItem图框替换, Me.ToolStripMenuItem安装目录})
        Me.ContextMenuStrip配置文件.Name = "ContextMenuStrip配置文件"
        Me.ContextMenuStrip配置文件.Size = New System.Drawing.Size(189, 94)
        '
        'ToolStripMenuItem全局
        '
        Me.ToolStripMenuItem全局.Name = "ToolStripMenuItem全局"
        Me.ToolStripMenuItem全局.Size = New System.Drawing.Size(188, 30)
        Me.ToolStripMenuItem全局.Text = "全局配置文件"
        '
        'ToolStripMenuItem图框替换
        '
        Me.ToolStripMenuItem图框替换.Name = "ToolStripMenuItem图框替换"
        Me.ToolStripMenuItem图框替换.Size = New System.Drawing.Size(188, 30)
        Me.ToolStripMenuItem图框替换.Text = "图框替换..."
        '
        'ToolStripMenuItem安装目录
        '
        Me.ToolStripMenuItem安装目录.Name = "ToolStripMenuItem安装目录"
        Me.ToolStripMenuItem安装目录.Size = New System.Drawing.Size(188, 30)
        Me.ToolStripMenuItem安装目录.Text = "安装目录"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "22222222222222222"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(103, 59)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(408, 28)
        Me.TextBox1.TabIndex = 8
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(519, 57)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(52, 25)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "选择"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TabPage其他
        '
        Me.TabPage其他.Controls.Add(Me.Label4)
        Me.TabPage其他.Controls.Add(Me.cbx中间符号)
        Me.TabPage其他.Controls.Add(Me.GroupBox自动保存)
        Me.TabPage其他.Controls.Add(Me.GroupBox截图)
        Me.TabPage其他.Controls.Add(Me.lvw设置图标大小)
        Me.TabPage其他.Location = New System.Drawing.Point(4, 28)
        Me.TabPage其他.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage其他.Name = "TabPage其他"
        Me.TabPage其他.Size = New System.Drawing.Size(928, 456)
        Me.TabPage其他.TabIndex = 4
        Me.TabPage其他.Text = "其他"
        Me.TabPage其他.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(394, 362)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(179, 18)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "BOM重命名中间符号："
        '
        'cbx中间符号
        '
        Me.cbx中间符号.FormattingEnabled = True
        Me.cbx中间符号.Items.AddRange(New Object() {""" """, "-", " "})
        Me.cbx中间符号.Location = New System.Drawing.Point(577, 359)
        Me.cbx中间符号.Name = "cbx中间符号"
        Me.cbx中间符号.Size = New System.Drawing.Size(114, 26)
        Me.cbx中间符号.TabIndex = 49
        '
        'GroupBox自动保存
        '
        Me.GroupBox自动保存.Controls.Add(Me.cmb时间间隔)
        Me.GroupBox自动保存.Controls.Add(Me.chk启用自动保存)
        Me.GroupBox自动保存.Controls.Add(Me.Label2)
        Me.GroupBox自动保存.Controls.Add(Me.lbl保存文档)
        Me.GroupBox自动保存.Controls.Add(Me.cmb保存文档)
        Me.GroupBox自动保存.Location = New System.Drawing.Point(366, 14)
        Me.GroupBox自动保存.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox自动保存.Name = "GroupBox自动保存"
        Me.GroupBox自动保存.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox自动保存.Size = New System.Drawing.Size(264, 154)
        Me.GroupBox自动保存.TabIndex = 34
        Me.GroupBox自动保存.TabStop = False
        Me.GroupBox自动保存.Text = "自动保存"
        '
        'cmb时间间隔
        '
        Me.cmb时间间隔.FormattingEnabled = True
        Me.cmb时间间隔.Items.AddRange(New Object() {"10", "120", "20", "30", "40", "50", "60", "90"})
        Me.cmb时间间隔.Location = New System.Drawing.Point(50, 111)
        Me.cmb时间间隔.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb时间间隔.Name = "cmb时间间隔"
        Me.cmb时间间隔.Size = New System.Drawing.Size(97, 26)
        Me.cmb时间间隔.TabIndex = 28
        Me.cmb时间间隔.Text = "30"
        '
        'chk启用自动保存
        '
        Me.chk启用自动保存.AutoSize = True
        Me.chk启用自动保存.Location = New System.Drawing.Point(15, 32)
        Me.chk启用自动保存.Margin = New System.Windows.Forms.Padding(4)
        Me.chk启用自动保存.Name = "chk启用自动保存"
        Me.chk启用自动保存.Size = New System.Drawing.Size(142, 22)
        Me.chk启用自动保存.TabIndex = 22
        Me.chk启用自动保存.Text = "启用自动保存"
        Me.chk启用自动保存.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 117)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 18)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "每             分钟保存"
        '
        'lbl保存文档
        '
        Me.lbl保存文档.AutoSize = True
        Me.lbl保存文档.Location = New System.Drawing.Point(15, 70)
        Me.lbl保存文档.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl保存文档.Name = "lbl保存文档"
        Me.lbl保存文档.Size = New System.Drawing.Size(62, 18)
        Me.lbl保存文档.TabIndex = 23
        Me.lbl保存文档.Text = "保存："
        '
        'cmb保存文档
        '
        Me.cmb保存文档.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb保存文档.FormattingEnabled = True
        Me.cmb保存文档.Items.AddRange(New Object() {"当前文档", "所有可见的文档", "所有打开的文档"})
        Me.cmb保存文档.Location = New System.Drawing.Point(80, 64)
        Me.cmb保存文档.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb保存文档.Name = "cmb保存文档"
        Me.cmb保存文档.Size = New System.Drawing.Size(166, 26)
        Me.cmb保存文档.TabIndex = 24
        '
        'GroupBox截图
        '
        Me.GroupBox截图.Controls.Add(Me.txt高度)
        Me.GroupBox截图.Controls.Add(Me.txt宽度)
        Me.GroupBox截图.Controls.Add(Me.lbl高度)
        Me.GroupBox截图.Controls.Add(Me.lbl宽度)
        Me.GroupBox截图.Location = New System.Drawing.Point(366, 177)
        Me.GroupBox截图.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox截图.Name = "GroupBox截图"
        Me.GroupBox截图.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox截图.Size = New System.Drawing.Size(192, 128)
        Me.GroupBox截图.TabIndex = 33
        Me.GroupBox截图.TabStop = False
        Me.GroupBox截图.Text = "截图"
        '
        'txt高度
        '
        Me.txt高度.Location = New System.Drawing.Point(70, 82)
        Me.txt高度.Margin = New System.Windows.Forms.Padding(4)
        Me.txt高度.Name = "txt高度"
        Me.txt高度.Size = New System.Drawing.Size(97, 28)
        Me.txt高度.TabIndex = 1
        '
        'txt宽度
        '
        Me.txt宽度.Location = New System.Drawing.Point(70, 33)
        Me.txt宽度.Margin = New System.Windows.Forms.Padding(4)
        Me.txt宽度.Name = "txt宽度"
        Me.txt宽度.Size = New System.Drawing.Size(97, 28)
        Me.txt宽度.TabIndex = 0
        '
        'lbl高度
        '
        Me.lbl高度.AutoSize = True
        Me.lbl高度.Location = New System.Drawing.Point(9, 82)
        Me.lbl高度.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl高度.Name = "lbl高度"
        Me.lbl高度.Size = New System.Drawing.Size(62, 18)
        Me.lbl高度.TabIndex = 9
        Me.lbl高度.Text = "高度："
        '
        'lbl宽度
        '
        Me.lbl宽度.AutoSize = True
        Me.lbl宽度.Location = New System.Drawing.Point(9, 38)
        Me.lbl宽度.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl宽度.Name = "lbl宽度"
        Me.lbl宽度.Size = New System.Drawing.Size(62, 18)
        Me.lbl宽度.TabIndex = 8
        Me.lbl宽度.Text = "宽度："
        '
        'lvw设置图标大小
        '
        Me.lvw设置图标大小.AllowDrop = True
        Me.lvw设置图标大小.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvw设置图标大小.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ch按钮, Me.ch图标大小})
        Me.lvw设置图标大小.FullRowSelect = True
        Me.lvw设置图标大小.HideSelection = False
        Me.lvw设置图标大小.Location = New System.Drawing.Point(14, 14)
        Me.lvw设置图标大小.Margin = New System.Windows.Forms.Padding(4)
        Me.lvw设置图标大小.Name = "lvw设置图标大小"
        Me.lvw设置图标大小.Size = New System.Drawing.Size(342, 422)
        Me.lvw设置图标大小.TabIndex = 1
        Me.lvw设置图标大小.TabStop = False
        Me.lvw设置图标大小.UseCompatibleStateImageBehavior = False
        Me.lvw设置图标大小.View = System.Windows.Forms.View.Details
        '
        'ch按钮
        '
        Me.ch按钮.Text = "按钮"
        Me.ch按钮.Width = 127
        '
        'ch图标大小
        '
        Me.ch图标大小.Text = "图标大小"
        Me.ch图标大小.Width = 80
        '
        'TabPage展开图
        '
        Me.TabPage展开图.Controls.Add(Me.chk保存展开图到指定文件夹)
        Me.TabPage展开图.Controls.Add(Me.txt工艺文字高)
        Me.TabPage展开图.Controls.Add(Me.txt标记孔径上限)
        Me.TabPage展开图.Controls.Add(Me.lbl工艺文字高)
        Me.TabPage展开图.Controls.Add(Me.cmb图号材质)
        Me.TabPage展开图.Controls.Add(Me.lbl图号材质)
        Me.TabPage展开图.Controls.Add(Me.lbl标记孔径上限)
        Me.TabPage展开图.Controls.Add(Me.chk展开图隐藏螺纹特征)
        Me.TabPage展开图.Controls.Add(Me.GroupBox展开图模板)
        Me.TabPage展开图.Controls.Add(Me.chk展开图标注)
        Me.TabPage展开图.Controls.Add(Me.GroupBox下)
        Me.TabPage展开图.Controls.Add(Me.GroupBox上)
        Me.TabPage展开图.Location = New System.Drawing.Point(4, 28)
        Me.TabPage展开图.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage展开图.Name = "TabPage展开图"
        Me.TabPage展开图.Size = New System.Drawing.Size(928, 456)
        Me.TabPage展开图.TabIndex = 3
        Me.TabPage展开图.Text = "展开图"
        Me.TabPage展开图.UseVisualStyleBackColor = True
        '
        'chk保存展开图到指定文件夹
        '
        Me.chk保存展开图到指定文件夹.AutoSize = True
        Me.chk保存展开图到指定文件夹.Location = New System.Drawing.Point(453, 390)
        Me.chk保存展开图到指定文件夹.Margin = New System.Windows.Forms.Padding(4)
        Me.chk保存展开图到指定文件夹.Name = "chk保存展开图到指定文件夹"
        Me.chk保存展开图到指定文件夹.Size = New System.Drawing.Size(232, 22)
        Me.chk保存展开图到指定文件夹.TabIndex = 41
        Me.chk保存展开图到指定文件夹.Text = "保存展开图到指定文件夹"
        Me.chk保存展开图到指定文件夹.UseVisualStyleBackColor = True
        '
        'txt工艺文字高
        '
        Me.txt工艺文字高.Location = New System.Drawing.Point(566, 340)
        Me.txt工艺文字高.Margin = New System.Windows.Forms.Padding(4)
        Me.txt工艺文字高.Name = "txt工艺文字高"
        Me.txt工艺文字高.Size = New System.Drawing.Size(52, 28)
        Me.txt工艺文字高.TabIndex = 40
        Me.txt工艺文字高.Text = "0"
        '
        'txt标记孔径上限
        '
        Me.txt标记孔径上限.Location = New System.Drawing.Point(294, 340)
        Me.txt标记孔径上限.Margin = New System.Windows.Forms.Padding(4)
        Me.txt标记孔径上限.Name = "txt标记孔径上限"
        Me.txt标记孔径上限.Size = New System.Drawing.Size(52, 28)
        Me.txt标记孔径上限.TabIndex = 31
        Me.txt标记孔径上限.Text = "0"
        '
        'lbl工艺文字高
        '
        Me.lbl工艺文字高.AutoSize = True
        Me.lbl工艺文字高.Location = New System.Drawing.Point(450, 346)
        Me.lbl工艺文字高.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl工艺文字高.Name = "lbl工艺文字高"
        Me.lbl工艺文字高.Size = New System.Drawing.Size(98, 18)
        Me.lbl工艺文字高.TabIndex = 39
        Me.lbl工艺文字高.Text = "工艺文字高"
        '
        'cmb图号材质
        '
        Me.cmb图号材质.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb图号材质.FormattingEnabled = True
        Me.cmb图号材质.Items.AddRange(New Object() {"无材质", "材质前缀", "材质后缀"})
        Me.cmb图号材质.Location = New System.Drawing.Point(294, 384)
        Me.cmb图号材质.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb图号材质.Name = "cmb图号材质"
        Me.cmb图号材质.Size = New System.Drawing.Size(126, 26)
        Me.cmb图号材质.TabIndex = 34
        '
        'lbl图号材质
        '
        Me.lbl图号材质.AutoSize = True
        Me.lbl图号材质.Location = New System.Drawing.Point(168, 390)
        Me.lbl图号材质.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl图号材质.Name = "lbl图号材质"
        Me.lbl图号材质.Size = New System.Drawing.Size(116, 18)
        Me.lbl图号材质.TabIndex = 33
        Me.lbl图号材质.Text = "图号添加材质"
        '
        'lbl标记孔径上限
        '
        Me.lbl标记孔径上限.AutoSize = True
        Me.lbl标记孔径上限.Location = New System.Drawing.Point(168, 346)
        Me.lbl标记孔径上限.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl标记孔径上限.Name = "lbl标记孔径上限"
        Me.lbl标记孔径上限.Size = New System.Drawing.Size(116, 18)
        Me.lbl标记孔径上限.TabIndex = 30
        Me.lbl标记孔径上限.Text = "标记孔径上限"
        '
        'chk展开图隐藏螺纹特征
        '
        Me.chk展开图隐藏螺纹特征.AutoSize = True
        Me.chk展开图隐藏螺纹特征.Location = New System.Drawing.Point(18, 387)
        Me.chk展开图隐藏螺纹特征.Margin = New System.Windows.Forms.Padding(4)
        Me.chk展开图隐藏螺纹特征.Name = "chk展开图隐藏螺纹特征"
        Me.chk展开图隐藏螺纹特征.Size = New System.Drawing.Size(106, 22)
        Me.chk展开图隐藏螺纹特征.TabIndex = 26
        Me.chk展开图隐藏螺纹特征.Text = "隐藏螺纹"
        Me.chk展开图隐藏螺纹特征.UseVisualStyleBackColor = True
        '
        'GroupBox展开图模板
        '
        Me.GroupBox展开图模板.Controls.Add(Me.btn展开图模板)
        Me.GroupBox展开图模板.Controls.Add(Me.txt展开图模板)
        Me.GroupBox展开图模板.Controls.Add(Me.lbl展开图模板)
        Me.GroupBox展开图模板.Location = New System.Drawing.Point(16, 21)
        Me.GroupBox展开图模板.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox展开图模板.Name = "GroupBox展开图模板"
        Me.GroupBox展开图模板.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox展开图模板.Size = New System.Drawing.Size(891, 87)
        Me.GroupBox展开图模板.TabIndex = 25
        Me.GroupBox展开图模板.TabStop = False
        Me.GroupBox展开图模板.Text = "展开图模板"
        '
        'btn展开图模板
        '
        Me.btn展开图模板.Location = New System.Drawing.Point(825, 26)
        Me.btn展开图模板.Margin = New System.Windows.Forms.Padding(4)
        Me.btn展开图模板.Name = "btn展开图模板"
        Me.btn展开图模板.Size = New System.Drawing.Size(38, 38)
        Me.btn展开图模板.TabIndex = 0
        Me.btn展开图模板.UseVisualStyleBackColor = True
        '
        'txt展开图模板
        '
        Me.txt展开图模板.Location = New System.Drawing.Point(120, 28)
        Me.txt展开图模板.Margin = New System.Windows.Forms.Padding(4)
        Me.txt展开图模板.Name = "txt展开图模板"
        Me.txt展开图模板.ReadOnly = True
        Me.txt展开图模板.Size = New System.Drawing.Size(688, 28)
        Me.txt展开图模板.TabIndex = 8
        '
        'lbl展开图模板
        '
        Me.lbl展开图模板.AutoSize = True
        Me.lbl展开图模板.Location = New System.Drawing.Point(10, 34)
        Me.lbl展开图模板.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl展开图模板.Name = "lbl展开图模板"
        Me.lbl展开图模板.Size = New System.Drawing.Size(116, 18)
        Me.lbl展开图模板.TabIndex = 7
        Me.lbl展开图模板.Text = "展开图模板："
        '
        'chk展开图标注
        '
        Me.chk展开图标注.AutoSize = True
        Me.chk展开图标注.Location = New System.Drawing.Point(18, 344)
        Me.chk展开图标注.Margin = New System.Windows.Forms.Padding(4)
        Me.chk展开图标注.Name = "chk展开图标注"
        Me.chk展开图标注.Size = New System.Drawing.Size(124, 22)
        Me.chk展开图标注.TabIndex = 0
        Me.chk展开图标注.Text = "展开图标注"
        Me.chk展开图标注.UseVisualStyleBackColor = True
        '
        'GroupBox下
        '
        Me.GroupBox下.Controls.Add(Me.cmb向下线宽)
        Me.GroupBox下.Controls.Add(Me.cmb向下线型)
        Me.GroupBox下.Controls.Add(Me.lbl颜色下)
        Me.GroupBox下.Controls.Add(Me.btn向下颜色)
        Me.GroupBox下.Controls.Add(Me.lbl线宽下)
        Me.GroupBox下.Controls.Add(Me.lbl线型下)
        Me.GroupBox下.Location = New System.Drawing.Point(417, 120)
        Me.GroupBox下.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox下.Name = "GroupBox下"
        Me.GroupBox下.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox下.Size = New System.Drawing.Size(351, 195)
        Me.GroupBox下.TabIndex = 1
        Me.GroupBox下.TabStop = False
        Me.GroupBox下.Text = "下"
        '
        'cmb向下线宽
        '
        Me.cmb向下线宽.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb向下线宽.FormattingEnabled = True
        Me.cmb向下线宽.Items.AddRange(New Object() {"0.18", "0.25", "0.35", "0.5", "0.7", "1", "1.4", "2"})
        Me.cmb向下线宽.Location = New System.Drawing.Point(96, 78)
        Me.cmb向下线宽.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb向下线宽.Name = "cmb向下线宽"
        Me.cmb向下线宽.Size = New System.Drawing.Size(208, 26)
        Me.cmb向下线宽.TabIndex = 1
        '
        'cmb向下线型
        '
        Me.cmb向下线型.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb向下线型.FormattingEnabled = True
        Me.cmb向下线型.Items.AddRange(New Object() {"实线", "虚线", "点长划线", "双点长划线", "三点长划线"})
        Me.cmb向下线型.Location = New System.Drawing.Point(96, 28)
        Me.cmb向下线型.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb向下线型.Name = "cmb向下线型"
        Me.cmb向下线型.Size = New System.Drawing.Size(208, 26)
        Me.cmb向下线型.TabIndex = 0
        '
        'lbl颜色下
        '
        Me.lbl颜色下.AutoSize = True
        Me.lbl颜色下.Location = New System.Drawing.Point(24, 135)
        Me.lbl颜色下.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl颜色下.Name = "lbl颜色下"
        Me.lbl颜色下.Size = New System.Drawing.Size(44, 18)
        Me.lbl颜色下.TabIndex = 3
        Me.lbl颜色下.Text = "颜色"
        '
        'btn向下颜色
        '
        Me.btn向下颜色.Location = New System.Drawing.Point(98, 123)
        Me.btn向下颜色.Margin = New System.Windows.Forms.Padding(4)
        Me.btn向下颜色.Name = "btn向下颜色"
        Me.btn向下颜色.Size = New System.Drawing.Size(57, 39)
        Me.btn向下颜色.TabIndex = 2
        Me.btn向下颜色.UseVisualStyleBackColor = True
        '
        'lbl线宽下
        '
        Me.lbl线宽下.AutoSize = True
        Me.lbl线宽下.Location = New System.Drawing.Point(24, 87)
        Me.lbl线宽下.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl线宽下.Name = "lbl线宽下"
        Me.lbl线宽下.Size = New System.Drawing.Size(44, 18)
        Me.lbl线宽下.TabIndex = 1
        Me.lbl线宽下.Text = "线宽"
        '
        'lbl线型下
        '
        Me.lbl线型下.AutoSize = True
        Me.lbl线型下.Location = New System.Drawing.Point(24, 39)
        Me.lbl线型下.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl线型下.Name = "lbl线型下"
        Me.lbl线型下.Size = New System.Drawing.Size(44, 18)
        Me.lbl线型下.TabIndex = 0
        Me.lbl线型下.Text = "线型"
        '
        'GroupBox上
        '
        Me.GroupBox上.Controls.Add(Me.cmb向上线宽)
        Me.GroupBox上.Controls.Add(Me.cmb向上线型)
        Me.GroupBox上.Controls.Add(Me.lbl颜色上)
        Me.GroupBox上.Controls.Add(Me.btn向上颜色)
        Me.GroupBox上.Controls.Add(Me.lbl线宽上)
        Me.GroupBox上.Controls.Add(Me.lbl线型上)
        Me.GroupBox上.Location = New System.Drawing.Point(16, 120)
        Me.GroupBox上.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox上.Name = "GroupBox上"
        Me.GroupBox上.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox上.Size = New System.Drawing.Size(351, 195)
        Me.GroupBox上.TabIndex = 0
        Me.GroupBox上.TabStop = False
        Me.GroupBox上.Text = "上"
        '
        'cmb向上线宽
        '
        Me.cmb向上线宽.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb向上线宽.FormattingEnabled = True
        Me.cmb向上线宽.Items.AddRange(New Object() {"0.18", "0.25", "0.35", "0.5", "0.7", "1", "1.4", "2"})
        Me.cmb向上线宽.Location = New System.Drawing.Point(98, 81)
        Me.cmb向上线宽.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb向上线宽.Name = "cmb向上线宽"
        Me.cmb向上线宽.Size = New System.Drawing.Size(208, 26)
        Me.cmb向上线宽.TabIndex = 1
        '
        'cmb向上线型
        '
        Me.cmb向上线型.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb向上线型.FormattingEnabled = True
        Me.cmb向上线型.Items.AddRange(New Object() {"实线", "虚线", "点长划线", "双点长划线", "三点长划线"})
        Me.cmb向上线型.Location = New System.Drawing.Point(98, 33)
        Me.cmb向上线型.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb向上线型.Name = "cmb向上线型"
        Me.cmb向上线型.Size = New System.Drawing.Size(208, 26)
        Me.cmb向上线型.TabIndex = 0
        '
        'lbl颜色上
        '
        Me.lbl颜色上.AutoSize = True
        Me.lbl颜色上.Location = New System.Drawing.Point(24, 135)
        Me.lbl颜色上.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl颜色上.Name = "lbl颜色上"
        Me.lbl颜色上.Size = New System.Drawing.Size(44, 18)
        Me.lbl颜色上.TabIndex = 3
        Me.lbl颜色上.Text = "颜色"
        '
        'btn向上颜色
        '
        Me.btn向上颜色.Location = New System.Drawing.Point(98, 124)
        Me.btn向上颜色.Margin = New System.Windows.Forms.Padding(4)
        Me.btn向上颜色.Name = "btn向上颜色"
        Me.btn向上颜色.Size = New System.Drawing.Size(57, 39)
        Me.btn向上颜色.TabIndex = 2
        Me.btn向上颜色.UseVisualStyleBackColor = True
        '
        'lbl线宽上
        '
        Me.lbl线宽上.AutoSize = True
        Me.lbl线宽上.Location = New System.Drawing.Point(24, 87)
        Me.lbl线宽上.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl线宽上.Name = "lbl线宽上"
        Me.lbl线宽上.Size = New System.Drawing.Size(44, 18)
        Me.lbl线宽上.TabIndex = 1
        Me.lbl线宽上.Text = "线宽"
        '
        'lbl线型上
        '
        Me.lbl线型上.AutoSize = True
        Me.lbl线型上.Location = New System.Drawing.Point(24, 39)
        Me.lbl线型上.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl线型上.Name = "lbl线型上"
        Me.lbl线型上.Size = New System.Drawing.Size(44, 18)
        Me.lbl线型上.TabIndex = 0
        Me.lbl线型上.Text = "线型"
        '
        'TabPage工程图
        '
        Me.TabPage工程图.Controls.Add(Me.chk强制横向)
        Me.TabPage工程图.Controls.Add(Me.chk逆时针序号)
        Me.TabPage工程图.Controls.Add(Me.chk模型匹配检查)
        Me.TabPage工程图.Controls.Add(Me.GroupBox标题栏)
        Me.TabPage工程图.Controls.Add(Me.GroupBox页边距)
        Me.TabPage工程图.Controls.Add(Me.GroupBox视图)
        Me.TabPage工程图.Controls.Add(Me.GroupBox工程图模板)
        Me.TabPage工程图.Location = New System.Drawing.Point(4, 28)
        Me.TabPage工程图.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage工程图.Name = "TabPage工程图"
        Me.TabPage工程图.Size = New System.Drawing.Size(928, 456)
        Me.TabPage工程图.TabIndex = 2
        Me.TabPage工程图.Text = "工程图"
        Me.TabPage工程图.UseVisualStyleBackColor = True
        '
        'chk强制横向
        '
        Me.chk强制横向.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chk强制横向.AutoSize = True
        Me.chk强制横向.Location = New System.Drawing.Point(618, 387)
        Me.chk强制横向.Margin = New System.Windows.Forms.Padding(4)
        Me.chk强制横向.Name = "chk强制横向"
        Me.chk强制横向.Size = New System.Drawing.Size(106, 22)
        Me.chk强制横向.TabIndex = 35
        Me.chk强制横向.Text = "强制横向"
        Me.chk强制横向.UseVisualStyleBackColor = True
        '
        'chk逆时针序号
        '
        Me.chk逆时针序号.AutoSize = True
        Me.chk逆时针序号.Location = New System.Drawing.Point(777, 339)
        Me.chk逆时针序号.Margin = New System.Windows.Forms.Padding(4)
        Me.chk逆时针序号.Name = "chk逆时针序号"
        Me.chk逆时针序号.Size = New System.Drawing.Size(124, 22)
        Me.chk逆时针序号.TabIndex = 34
        Me.chk逆时针序号.Text = "逆时针序号"
        Me.chk逆时针序号.UseVisualStyleBackColor = True
        '
        'chk模型匹配检查
        '
        Me.chk模型匹配检查.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chk模型匹配检查.AutoSize = True
        Me.chk模型匹配检查.Location = New System.Drawing.Point(618, 339)
        Me.chk模型匹配检查.Margin = New System.Windows.Forms.Padding(4)
        Me.chk模型匹配检查.Name = "chk模型匹配检查"
        Me.chk模型匹配检查.Size = New System.Drawing.Size(142, 22)
        Me.chk模型匹配检查.TabIndex = 33
        Me.chk模型匹配检查.Text = "模型匹配检查"
        Me.chk模型匹配检查.UseVisualStyleBackColor = True
        '
        'GroupBox标题栏
        '
        Me.GroupBox标题栏.Controls.Add(Me.Label3)
        Me.GroupBox标题栏.Controls.Add(Me.cbx增加后缀)
        Me.GroupBox标题栏.Controls.Add(Me.txt零件图框)
        Me.GroupBox标题栏.Controls.Add(Me.txt部件图框)
        Me.GroupBox标题栏.Controls.Add(Me.lbl零件图框)
        Me.GroupBox标题栏.Controls.Add(Me.lbl部件图框)
        Me.GroupBox标题栏.Location = New System.Drawing.Point(645, 120)
        Me.GroupBox标题栏.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox标题栏.Name = "GroupBox标题栏"
        Me.GroupBox标题栏.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox标题栏.Size = New System.Drawing.Size(256, 184)
        Me.GroupBox标题栏.TabIndex = 32
        Me.GroupBox标题栏.TabStop = False
        Me.GroupBox标题栏.Text = "标题栏"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 18)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "另存DWG后缀："
        '
        'cbx增加后缀
        '
        Me.cbx增加后缀.FormattingEnabled = True
        Me.cbx增加后缀.Items.AddRange(New Object() {"-AutoCAD", "-CAD", "-模型"})
        Me.cbx增加后缀.Location = New System.Drawing.Point(133, 139)
        Me.cbx增加后缀.Name = "cbx增加后缀"
        Me.cbx增加后缀.Size = New System.Drawing.Size(114, 26)
        Me.cbx增加后缀.TabIndex = 45
        '
        'txt零件图框
        '
        Me.txt零件图框.Location = New System.Drawing.Point(70, 82)
        Me.txt零件图框.Margin = New System.Windows.Forms.Padding(4)
        Me.txt零件图框.Name = "txt零件图框"
        Me.txt零件图框.Size = New System.Drawing.Size(168, 28)
        Me.txt零件图框.TabIndex = 1
        '
        'txt部件图框
        '
        Me.txt部件图框.Location = New System.Drawing.Point(70, 33)
        Me.txt部件图框.Margin = New System.Windows.Forms.Padding(4)
        Me.txt部件图框.Name = "txt部件图框"
        Me.txt部件图框.Size = New System.Drawing.Size(168, 28)
        Me.txt部件图框.TabIndex = 0
        '
        'lbl零件图框
        '
        Me.lbl零件图框.AutoSize = True
        Me.lbl零件图框.Location = New System.Drawing.Point(9, 82)
        Me.lbl零件图框.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl零件图框.Name = "lbl零件图框"
        Me.lbl零件图框.Size = New System.Drawing.Size(62, 18)
        Me.lbl零件图框.TabIndex = 9
        Me.lbl零件图框.Text = "零件："
        '
        'lbl部件图框
        '
        Me.lbl部件图框.AutoSize = True
        Me.lbl部件图框.Location = New System.Drawing.Point(9, 38)
        Me.lbl部件图框.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl部件图框.Name = "lbl部件图框"
        Me.lbl部件图框.Size = New System.Drawing.Size(62, 18)
        Me.lbl部件图框.TabIndex = 8
        Me.lbl部件图框.Text = "部件："
        '
        'GroupBox页边距
        '
        Me.GroupBox页边距.Controls.Add(Me.NumericUpDown页边距右)
        Me.GroupBox页边距.Controls.Add(Me.NumericUpDown页边距左)
        Me.GroupBox页边距.Controls.Add(Me.NumericUpDown页边距下)
        Me.GroupBox页边距.Controls.Add(Me.NumericUpDown页边距上)
        Me.GroupBox页边距.Controls.Add(Me.lb页边距)
        Me.GroupBox页边距.Location = New System.Drawing.Point(20, 320)
        Me.GroupBox页边距.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox页边距.Name = "GroupBox页边距"
        Me.GroupBox页边距.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox页边距.Size = New System.Drawing.Size(586, 86)
        Me.GroupBox页边距.TabIndex = 31
        Me.GroupBox页边距.TabStop = False
        Me.GroupBox页边距.Text = "页边距"
        '
        'NumericUpDown页边距右
        '
        Me.NumericUpDown页边距右.Location = New System.Drawing.Point(495, 34)
        Me.NumericUpDown页边距右.Margin = New System.Windows.Forms.Padding(4)
        Me.NumericUpDown页边距右.Name = "NumericUpDown页边距右"
        Me.NumericUpDown页边距右.Size = New System.Drawing.Size(74, 28)
        Me.NumericUpDown页边距右.TabIndex = 3
        Me.NumericUpDown页边距右.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'NumericUpDown页边距左
        '
        Me.NumericUpDown页边距左.Location = New System.Drawing.Point(348, 34)
        Me.NumericUpDown页边距左.Margin = New System.Windows.Forms.Padding(4)
        Me.NumericUpDown页边距左.Name = "NumericUpDown页边距左"
        Me.NumericUpDown页边距左.Size = New System.Drawing.Size(74, 28)
        Me.NumericUpDown页边距左.TabIndex = 2
        Me.NumericUpDown页边距左.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'NumericUpDown页边距下
        '
        Me.NumericUpDown页边距下.Location = New System.Drawing.Point(201, 34)
        Me.NumericUpDown页边距下.Margin = New System.Windows.Forms.Padding(4)
        Me.NumericUpDown页边距下.Name = "NumericUpDown页边距下"
        Me.NumericUpDown页边距下.Size = New System.Drawing.Size(74, 28)
        Me.NumericUpDown页边距下.TabIndex = 1
        Me.NumericUpDown页边距下.Value = New Decimal(New Integer() {35, 0, 0, 0})
        '
        'NumericUpDown页边距上
        '
        Me.NumericUpDown页边距上.Location = New System.Drawing.Point(54, 34)
        Me.NumericUpDown页边距上.Margin = New System.Windows.Forms.Padding(4)
        Me.NumericUpDown页边距上.Name = "NumericUpDown页边距上"
        Me.NumericUpDown页边距上.Size = New System.Drawing.Size(74, 28)
        Me.NumericUpDown页边距上.TabIndex = 0
        Me.NumericUpDown页边距上.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'lb页边距
        '
        Me.lb页边距.AutoSize = True
        Me.lb页边距.Location = New System.Drawing.Point(15, 40)
        Me.lb页边距.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb页边距.Name = "lb页边距"
        Me.lb页边距.Size = New System.Drawing.Size(467, 18)
        Me.lb页边距.TabIndex = 4
        Me.lb页边距.Text = "上               下              左              右"
        '
        'GroupBox视图
        '
        Me.GroupBox视图.Controls.Add(Me.chk标注尺寸)
        Me.GroupBox视图.Controls.Add(Me.lbl前视图)
        Me.GroupBox视图.Controls.Add(Me.chk左视图)
        Me.GroupBox视图.Controls.Add(Me.chk俯视图)
        Me.GroupBox视图.Controls.Add(Me.chk右视图)
        Me.GroupBox视图.Controls.Add(Me.chk仰视图)
        Me.GroupBox视图.Controls.Add(Me.chk工程图螺纹特征)
        Me.GroupBox视图.Controls.Add(Me.rdo显示隐藏线)
        Me.GroupBox视图.Controls.Add(Me.rdo不显示隐藏线)
        Me.GroupBox视图.Controls.Add(Me.chk相切边)
        Me.GroupBox视图.Controls.Add(Me.chk钣金自动展开)
        Me.GroupBox视图.Controls.Add(Me.chk第三视角)
        Me.GroupBox视图.Location = New System.Drawing.Point(16, 120)
        Me.GroupBox视图.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox视图.Name = "GroupBox视图"
        Me.GroupBox视图.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox视图.Size = New System.Drawing.Size(620, 184)
        Me.GroupBox视图.TabIndex = 30
        Me.GroupBox视图.TabStop = False
        Me.GroupBox视图.Text = "视图"
        '
        'chk标注尺寸
        '
        Me.chk标注尺寸.AutoSize = True
        Me.chk标注尺寸.Location = New System.Drawing.Point(290, 36)
        Me.chk标注尺寸.Margin = New System.Windows.Forms.Padding(4)
        Me.chk标注尺寸.Name = "chk标注尺寸"
        Me.chk标注尺寸.Size = New System.Drawing.Size(106, 22)
        Me.chk标注尺寸.TabIndex = 2
        Me.chk标注尺寸.Text = "标注尺寸"
        Me.chk标注尺寸.UseVisualStyleBackColor = True
        '
        'lbl前视图
        '
        Me.lbl前视图.AutoSize = True
        Me.lbl前视图.Location = New System.Drawing.Point(444, 88)
        Me.lbl前视图.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl前视图.Name = "lbl前视图"
        Me.lbl前视图.Size = New System.Drawing.Size(62, 18)
        Me.lbl前视图.TabIndex = 43
        Me.lbl前视图.Text = "前视图"
        '
        'chk左视图
        '
        Me.chk左视图.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk左视图.AutoSize = True
        Me.chk左视图.Location = New System.Drawing.Point(342, 86)
        Me.chk左视图.Margin = New System.Windows.Forms.Padding(4)
        Me.chk左视图.Name = "chk左视图"
        Me.chk左视图.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chk左视图.Size = New System.Drawing.Size(88, 22)
        Me.chk左视图.TabIndex = 8
        Me.chk左视图.Text = "左视图"
        Me.chk左视图.UseVisualStyleBackColor = True
        '
        'chk俯视图
        '
        Me.chk俯视图.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk俯视图.AutoSize = True
        Me.chk俯视图.Location = New System.Drawing.Point(436, 142)
        Me.chk俯视图.Margin = New System.Windows.Forms.Padding(4)
        Me.chk俯视图.Name = "chk俯视图"
        Me.chk俯视图.Size = New System.Drawing.Size(88, 22)
        Me.chk俯视图.TabIndex = 9
        Me.chk俯视图.Text = "俯视图"
        Me.chk俯视图.UseVisualStyleBackColor = True
        '
        'chk右视图
        '
        Me.chk右视图.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk右视图.AutoSize = True
        Me.chk右视图.Location = New System.Drawing.Point(516, 86)
        Me.chk右视图.Margin = New System.Windows.Forms.Padding(4)
        Me.chk右视图.Name = "chk右视图"
        Me.chk右视图.Size = New System.Drawing.Size(88, 22)
        Me.chk右视图.TabIndex = 10
        Me.chk右视图.Text = "右视图"
        Me.chk右视图.UseVisualStyleBackColor = True
        '
        'chk仰视图
        '
        Me.chk仰视图.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk仰视图.AutoSize = True
        Me.chk仰视图.Location = New System.Drawing.Point(436, 36)
        Me.chk仰视图.Margin = New System.Windows.Forms.Padding(4)
        Me.chk仰视图.Name = "chk仰视图"
        Me.chk仰视图.Size = New System.Drawing.Size(88, 22)
        Me.chk仰视图.TabIndex = 7
        Me.chk仰视图.Text = "仰视图"
        Me.chk仰视图.UseVisualStyleBackColor = True
        '
        'chk工程图螺纹特征
        '
        Me.chk工程图螺纹特征.AutoSize = True
        Me.chk工程图螺纹特征.Location = New System.Drawing.Point(166, 81)
        Me.chk工程图螺纹特征.Margin = New System.Windows.Forms.Padding(4)
        Me.chk工程图螺纹特征.Name = "chk工程图螺纹特征"
        Me.chk工程图螺纹特征.Size = New System.Drawing.Size(106, 22)
        Me.chk工程图螺纹特征.TabIndex = 6
        Me.chk工程图螺纹特征.Text = "螺纹特征"
        Me.chk工程图螺纹特征.UseVisualStyleBackColor = True
        '
        'rdo显示隐藏线
        '
        Me.rdo显示隐藏线.AutoSize = True
        Me.rdo显示隐藏线.Location = New System.Drawing.Point(166, 129)
        Me.rdo显示隐藏线.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo显示隐藏线.Name = "rdo显示隐藏线"
        Me.rdo显示隐藏线.Size = New System.Drawing.Size(123, 22)
        Me.rdo显示隐藏线.TabIndex = 5
        Me.rdo显示隐藏线.TabStop = True
        Me.rdo显示隐藏线.Text = "显示隐藏线"
        Me.rdo显示隐藏线.UseVisualStyleBackColor = True
        '
        'rdo不显示隐藏线
        '
        Me.rdo不显示隐藏线.AutoSize = True
        Me.rdo不显示隐藏线.Location = New System.Drawing.Point(15, 129)
        Me.rdo不显示隐藏线.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo不显示隐藏线.Name = "rdo不显示隐藏线"
        Me.rdo不显示隐藏线.Size = New System.Drawing.Size(141, 22)
        Me.rdo不显示隐藏线.TabIndex = 4
        Me.rdo不显示隐藏线.TabStop = True
        Me.rdo不显示隐藏线.Text = "不显示隐藏线"
        Me.rdo不显示隐藏线.UseVisualStyleBackColor = True
        '
        'chk相切边
        '
        Me.chk相切边.AutoSize = True
        Me.chk相切边.Location = New System.Drawing.Point(166, 36)
        Me.chk相切边.Margin = New System.Windows.Forms.Padding(4)
        Me.chk相切边.Name = "chk相切边"
        Me.chk相切边.Size = New System.Drawing.Size(88, 22)
        Me.chk相切边.TabIndex = 1
        Me.chk相切边.Text = "相切边"
        Me.chk相切边.UseVisualStyleBackColor = True
        '
        'chk钣金自动展开
        '
        Me.chk钣金自动展开.AutoSize = True
        Me.chk钣金自动展开.Location = New System.Drawing.Point(14, 36)
        Me.chk钣金自动展开.Margin = New System.Windows.Forms.Padding(4)
        Me.chk钣金自动展开.Name = "chk钣金自动展开"
        Me.chk钣金自动展开.Size = New System.Drawing.Size(142, 22)
        Me.chk钣金自动展开.TabIndex = 0
        Me.chk钣金自动展开.Text = "钣金自动展开"
        Me.chk钣金自动展开.UseVisualStyleBackColor = True
        '
        'chk第三视角
        '
        Me.chk第三视角.AutoSize = True
        Me.chk第三视角.Location = New System.Drawing.Point(14, 81)
        Me.chk第三视角.Margin = New System.Windows.Forms.Padding(4)
        Me.chk第三视角.Name = "chk第三视角"
        Me.chk第三视角.Size = New System.Drawing.Size(106, 22)
        Me.chk第三视角.TabIndex = 3
        Me.chk第三视角.Text = "第三视角"
        Me.chk第三视角.UseVisualStyleBackColor = True
        '
        'GroupBox工程图模板
        '
        Me.GroupBox工程图模板.Controls.Add(Me.btn选择工程图模板)
        Me.GroupBox工程图模板.Controls.Add(Me.txt工程图模板)
        Me.GroupBox工程图模板.Controls.Add(Me.lbl模板工程图)
        Me.GroupBox工程图模板.Location = New System.Drawing.Point(16, 21)
        Me.GroupBox工程图模板.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox工程图模板.Name = "GroupBox工程图模板"
        Me.GroupBox工程图模板.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox工程图模板.Size = New System.Drawing.Size(891, 87)
        Me.GroupBox工程图模板.TabIndex = 24
        Me.GroupBox工程图模板.TabStop = False
        Me.GroupBox工程图模板.Text = "工程图模板"
        '
        'btn选择工程图模板
        '
        Me.btn选择工程图模板.Location = New System.Drawing.Point(825, 26)
        Me.btn选择工程图模板.Margin = New System.Windows.Forms.Padding(4)
        Me.btn选择工程图模板.Name = "btn选择工程图模板"
        Me.btn选择工程图模板.Size = New System.Drawing.Size(38, 38)
        Me.btn选择工程图模板.TabIndex = 0
        Me.btn选择工程图模板.UseVisualStyleBackColor = True
        '
        'txt工程图模板
        '
        Me.txt工程图模板.Location = New System.Drawing.Point(120, 28)
        Me.txt工程图模板.Margin = New System.Windows.Forms.Padding(4)
        Me.txt工程图模板.Name = "txt工程图模板"
        Me.txt工程图模板.ReadOnly = True
        Me.txt工程图模板.Size = New System.Drawing.Size(688, 28)
        Me.txt工程图模板.TabIndex = 8
        Me.txt工程图模板.TabStop = False
        '
        'lbl模板工程图
        '
        Me.lbl模板工程图.AutoSize = True
        Me.lbl模板工程图.Location = New System.Drawing.Point(10, 34)
        Me.lbl模板工程图.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl模板工程图.Name = "lbl模板工程图"
        Me.lbl模板工程图.Size = New System.Drawing.Size(116, 18)
        Me.lbl模板工程图.TabIndex = 7
        Me.lbl模板工程图.Text = "工程图模板："
        '
        'TabPage模型
        '
        Me.TabPage模型.Controls.Add(Me.txt钣金厚度前缀)
        Me.TabPage模型.Controls.Add(Me.txt去除后缀)
        Me.TabPage模型.Controls.Add(Me.chk钣金厚度检查)
        Me.TabPage模型.Controls.Add(Me.lbl去除后缀)
        Me.TabPage模型.Controls.Add(Me.chk检查重复图号)
        Me.TabPage模型.Controls.Add(Me.lbl查找文件夹层数)
        Me.TabPage模型.Controls.Add(Me.NUD查找文件夹层数)
        Me.TabPage模型.Controls.Add(Me.chk另存到子文件夹)
        Me.TabPage模型.Controls.Add(Me.chk备份工程图)
        Me.TabPage模型.Controls.Add(Me.GroupBoxERP查询)
        Me.TabPage模型.Controls.Add(Me.GroupBoxBOM导出项目)
        Me.TabPage模型.Location = New System.Drawing.Point(4, 28)
        Me.TabPage模型.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage模型.Name = "TabPage模型"
        Me.TabPage模型.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage模型.Size = New System.Drawing.Size(928, 456)
        Me.TabPage模型.TabIndex = 1
        Me.TabPage模型.Text = "模型"
        Me.TabPage模型.UseVisualStyleBackColor = True
        '
        'txt钣金厚度前缀
        '
        Me.txt钣金厚度前缀.Location = New System.Drawing.Point(376, 384)
        Me.txt钣金厚度前缀.Margin = New System.Windows.Forms.Padding(4)
        Me.txt钣金厚度前缀.Name = "txt钣金厚度前缀"
        Me.txt钣金厚度前缀.Size = New System.Drawing.Size(145, 28)
        Me.txt钣金厚度前缀.TabIndex = 38
        '
        'txt去除后缀
        '
        Me.txt去除后缀.Location = New System.Drawing.Point(314, 342)
        Me.txt去除后缀.Margin = New System.Windows.Forms.Padding(4)
        Me.txt去除后缀.Name = "txt去除后缀"
        Me.txt去除后缀.Size = New System.Drawing.Size(169, 28)
        Me.txt去除后缀.TabIndex = 29
        Me.txt去除后缀.Text = "_MIR;_镜像"
        '
        'chk钣金厚度检查
        '
        Me.chk钣金厚度检查.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chk钣金厚度检查.AutoSize = True
        Me.chk钣金厚度检查.Checked = True
        Me.chk钣金厚度检查.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk钣金厚度检查.Location = New System.Drawing.Point(230, 393)
        Me.chk钣金厚度检查.Margin = New System.Windows.Forms.Padding(4)
        Me.chk钣金厚度检查.Name = "chk钣金厚度检查"
        Me.chk钣金厚度检查.Size = New System.Drawing.Size(142, 22)
        Me.chk钣金厚度检查.TabIndex = 37
        Me.chk钣金厚度检查.Text = "钣金厚度检查"
        Me.chk钣金厚度检查.UseVisualStyleBackColor = True
        '
        'lbl去除后缀
        '
        Me.lbl去除后缀.AutoSize = True
        Me.lbl去除后缀.Location = New System.Drawing.Point(228, 348)
        Me.lbl去除后缀.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl去除后缀.Name = "lbl去除后缀"
        Me.lbl去除后缀.Size = New System.Drawing.Size(80, 18)
        Me.lbl去除后缀.TabIndex = 28
        Me.lbl去除后缀.Text = "去除后缀"
        '
        'chk检查重复图号
        '
        Me.chk检查重复图号.AutoSize = True
        Me.chk检查重复图号.Location = New System.Drawing.Point(24, 384)
        Me.chk检查重复图号.Margin = New System.Windows.Forms.Padding(4)
        Me.chk检查重复图号.Name = "chk检查重复图号"
        Me.chk检查重复图号.Size = New System.Drawing.Size(142, 22)
        Me.chk检查重复图号.TabIndex = 27
        Me.chk检查重复图号.Text = "检查重复图号"
        Me.chk检查重复图号.UseVisualStyleBackColor = True
        '
        'lbl查找文件夹层数
        '
        Me.lbl查找文件夹层数.AutoSize = True
        Me.lbl查找文件夹层数.Location = New System.Drawing.Point(226, 308)
        Me.lbl查找文件夹层数.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl查找文件夹层数.Name = "lbl查找文件夹层数"
        Me.lbl查找文件夹层数.Size = New System.Drawing.Size(134, 18)
        Me.lbl查找文件夹层数.TabIndex = 26
        Me.lbl查找文件夹层数.Text = "查找文件夹层数"
        '
        'NUD查找文件夹层数
        '
        Me.NUD查找文件夹层数.Location = New System.Drawing.Point(363, 302)
        Me.NUD查找文件夹层数.Margin = New System.Windows.Forms.Padding(4)
        Me.NUD查找文件夹层数.Name = "NUD查找文件夹层数"
        Me.NUD查找文件夹层数.Size = New System.Drawing.Size(46, 28)
        Me.NUD查找文件夹层数.TabIndex = 25
        Me.NUD查找文件夹层数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NUD查找文件夹层数.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'chk另存到子文件夹
        '
        Me.chk另存到子文件夹.AutoSize = True
        Me.chk另存到子文件夹.Location = New System.Drawing.Point(24, 345)
        Me.chk另存到子文件夹.Margin = New System.Windows.Forms.Padding(4)
        Me.chk另存到子文件夹.Name = "chk另存到子文件夹"
        Me.chk另存到子文件夹.Size = New System.Drawing.Size(160, 22)
        Me.chk另存到子文件夹.TabIndex = 24
        Me.chk另存到子文件夹.Text = "另存到子文件夹"
        Me.chk另存到子文件夹.UseVisualStyleBackColor = True
        '
        'chk备份工程图
        '
        Me.chk备份工程图.AutoSize = True
        Me.chk备份工程图.Location = New System.Drawing.Point(24, 306)
        Me.chk备份工程图.Margin = New System.Windows.Forms.Padding(4)
        Me.chk备份工程图.Name = "chk备份工程图"
        Me.chk备份工程图.Size = New System.Drawing.Size(124, 22)
        Me.chk备份工程图.TabIndex = 23
        Me.chk备份工程图.Text = "备份工程图"
        Me.chk备份工程图.UseVisualStyleBackColor = True
        '
        'GroupBoxERP查询
        '
        Me.GroupBoxERP查询.Controls.Add(Me.btn选择erp数据库)
        Me.GroupBoxERP查询.Controls.Add(Me.btn更新数据库)
        Me.GroupBoxERP查询.Controls.Add(Me.btn打开erp数据库)
        Me.GroupBoxERP查询.Controls.Add(Me.txt查询列)
        Me.GroupBoxERP查询.Controls.Add(Me.lbl编码列)
        Me.GroupBoxERP查询.Controls.Add(Me.txt查找范围)
        Me.GroupBoxERP查询.Controls.Add(Me.lbl查询列)
        Me.GroupBoxERP查询.Controls.Add(Me.txt基础数据文件)
        Me.GroupBoxERP查询.Controls.Add(Me.lbl基础数据文件)
        Me.GroupBoxERP查询.Location = New System.Drawing.Point(9, 162)
        Me.GroupBoxERP查询.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxERP查询.Name = "GroupBoxERP查询"
        Me.GroupBoxERP查询.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxERP查询.Size = New System.Drawing.Size(891, 126)
        Me.GroupBoxERP查询.TabIndex = 22
        Me.GroupBoxERP查询.TabStop = False
        Me.GroupBoxERP查询.Text = "ERP查询"
        '
        'btn选择erp数据库
        '
        Me.btn选择erp数据库.Location = New System.Drawing.Point(777, 24)
        Me.btn选择erp数据库.Margin = New System.Windows.Forms.Padding(4)
        Me.btn选择erp数据库.Name = "btn选择erp数据库"
        Me.btn选择erp数据库.Size = New System.Drawing.Size(39, 39)
        Me.btn选择erp数据库.TabIndex = 0
        Me.btn选择erp数据库.UseVisualStyleBackColor = True
        '
        'btn更新数据库
        '
        Me.btn更新数据库.Location = New System.Drawing.Point(610, 76)
        Me.btn更新数据库.Margin = New System.Windows.Forms.Padding(4)
        Me.btn更新数据库.Name = "btn更新数据库"
        Me.btn更新数据库.Size = New System.Drawing.Size(123, 38)
        Me.btn更新数据库.TabIndex = 5
        Me.btn更新数据库.Text = "更新数据库"
        Me.btn更新数据库.UseVisualStyleBackColor = True
        '
        'btn打开erp数据库
        '
        Me.btn打开erp数据库.Location = New System.Drawing.Point(753, 76)
        Me.btn打开erp数据库.Margin = New System.Windows.Forms.Padding(4)
        Me.btn打开erp数据库.Name = "btn打开erp数据库"
        Me.btn打开erp数据库.Size = New System.Drawing.Size(123, 38)
        Me.btn打开erp数据库.TabIndex = 6
        Me.btn打开erp数据库.Text = "打开数据库"
        Me.btn打开erp数据库.UseVisualStyleBackColor = True
        '
        'txt查询列
        '
        Me.txt查询列.Location = New System.Drawing.Point(363, 80)
        Me.txt查询列.Margin = New System.Windows.Forms.Padding(4)
        Me.txt查询列.Name = "txt查询列"
        Me.txt查询列.Size = New System.Drawing.Size(132, 28)
        Me.txt查询列.TabIndex = 4
        '
        'lbl编码列
        '
        Me.lbl编码列.AutoSize = True
        Me.lbl编码列.Location = New System.Drawing.Point(280, 86)
        Me.lbl编码列.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl编码列.Name = "lbl编码列"
        Me.lbl编码列.Size = New System.Drawing.Size(80, 18)
        Me.lbl编码列.TabIndex = 3
        Me.lbl编码列.Text = "编码列："
        '
        'txt查找范围
        '
        Me.txt查找范围.Location = New System.Drawing.Point(112, 80)
        Me.txt查找范围.Margin = New System.Windows.Forms.Padding(4)
        Me.txt查找范围.Name = "txt查找范围"
        Me.txt查找范围.Size = New System.Drawing.Size(132, 28)
        Me.txt查找范围.TabIndex = 2
        '
        'lbl查询列
        '
        Me.lbl查询列.AutoSize = True
        Me.lbl查询列.Location = New System.Drawing.Point(12, 86)
        Me.lbl查询列.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl查询列.Name = "lbl查询列"
        Me.lbl查询列.Size = New System.Drawing.Size(80, 18)
        Me.lbl查询列.TabIndex = 1
        Me.lbl查询列.Text = "查询列："
        '
        'txt基础数据文件
        '
        Me.txt基础数据文件.Location = New System.Drawing.Point(153, 28)
        Me.txt基础数据文件.Margin = New System.Windows.Forms.Padding(4)
        Me.txt基础数据文件.Name = "txt基础数据文件"
        Me.txt基础数据文件.ReadOnly = True
        Me.txt基础数据文件.Size = New System.Drawing.Size(610, 28)
        Me.txt基础数据文件.TabIndex = 8
        Me.txt基础数据文件.TabStop = False
        '
        'lbl基础数据文件
        '
        Me.lbl基础数据文件.AutoSize = True
        Me.lbl基础数据文件.Location = New System.Drawing.Point(10, 34)
        Me.lbl基础数据文件.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl基础数据文件.Name = "lbl基础数据文件"
        Me.lbl基础数据文件.Size = New System.Drawing.Size(134, 18)
        Me.lbl基础数据文件.TabIndex = 7
        Me.lbl基础数据文件.Text = "基础数据文件："
        '
        'GroupBoxBOM导出项目
        '
        Me.GroupBoxBOM导出项目.Controls.Add(Me.chk使用UTF8编码)
        Me.GroupBoxBOM导出项目.Controls.Add(Me.btn清除)
        Me.GroupBoxBOM导出项目.Controls.Add(Me.btn还原)
        Me.GroupBoxBOM导出项目.Controls.Add(Me.btn添加)
        Me.GroupBoxBOM导出项目.Controls.Add(Me.cmb添加)
        Me.GroupBoxBOM导出项目.Controls.Add(Me.txtBOM导出项)
        Me.GroupBoxBOM导出项目.Controls.Add(Me.lblBOM导出项目)
        Me.GroupBoxBOM导出项目.Location = New System.Drawing.Point(9, 27)
        Me.GroupBoxBOM导出项目.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxBOM导出项目.Name = "GroupBoxBOM导出项目"
        Me.GroupBoxBOM导出项目.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxBOM导出项目.Size = New System.Drawing.Size(891, 126)
        Me.GroupBoxBOM导出项目.TabIndex = 21
        Me.GroupBoxBOM导出项目.TabStop = False
        Me.GroupBoxBOM导出项目.Text = "BOM导出"
        '
        'chk使用UTF8编码
        '
        Me.chk使用UTF8编码.AutoSize = True
        Me.chk使用UTF8编码.Location = New System.Drawing.Point(222, 78)
        Me.chk使用UTF8编码.Margin = New System.Windows.Forms.Padding(4)
        Me.chk使用UTF8编码.Name = "chk使用UTF8编码"
        Me.chk使用UTF8编码.Size = New System.Drawing.Size(142, 22)
        Me.chk使用UTF8编码.TabIndex = 5
        Me.chk使用UTF8编码.Text = "使用UTF8编码"
        Me.chk使用UTF8编码.UseVisualStyleBackColor = True
        '
        'btn清除
        '
        Me.btn清除.Location = New System.Drawing.Point(116, 70)
        Me.btn清除.Margin = New System.Windows.Forms.Padding(4)
        Me.btn清除.Name = "btn清除"
        Me.btn清除.Size = New System.Drawing.Size(80, 38)
        Me.btn清除.TabIndex = 2
        Me.btn清除.Text = "清除"
        Me.btn清除.UseVisualStyleBackColor = True
        '
        'btn还原
        '
        Me.btn还原.Location = New System.Drawing.Point(21, 70)
        Me.btn还原.Margin = New System.Windows.Forms.Padding(4)
        Me.btn还原.Name = "btn还原"
        Me.btn还原.Size = New System.Drawing.Size(80, 38)
        Me.btn还原.TabIndex = 1
        Me.btn还原.Text = "还原"
        Me.btn还原.UseVisualStyleBackColor = True
        '
        'btn添加
        '
        Me.btn添加.Location = New System.Drawing.Point(788, 70)
        Me.btn添加.Margin = New System.Windows.Forms.Padding(4)
        Me.btn添加.Name = "btn添加"
        Me.btn添加.Size = New System.Drawing.Size(80, 38)
        Me.btn添加.TabIndex = 4
        Me.btn添加.Text = "添加"
        Me.btn添加.UseVisualStyleBackColor = True
        '
        'cmb添加
        '
        Me.cmb添加.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb添加.FormattingEnabled = True
        Me.cmb添加.Location = New System.Drawing.Point(502, 75)
        Me.cmb添加.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb添加.Name = "cmb添加"
        Me.cmb添加.Size = New System.Drawing.Size(253, 26)
        Me.cmb添加.Sorted = True
        Me.cmb添加.TabIndex = 3
        '
        'txtBOM导出项
        '
        Me.txtBOM导出项.Location = New System.Drawing.Point(129, 28)
        Me.txtBOM导出项.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBOM导出项.Name = "txtBOM导出项"
        Me.txtBOM导出项.Size = New System.Drawing.Size(742, 28)
        Me.txtBOM导出项.TabIndex = 0
        Me.txtBOM导出项.Text = "项目序号|库存编号|成本中心|零件代号|材料|质量|所属装配代号|数量|总数量|描述"
        '
        'lblBOM导出项目
        '
        Me.lblBOM导出项目.AutoSize = True
        Me.lblBOM导出项目.Location = New System.Drawing.Point(10, 33)
        Me.lblBOM导出项目.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBOM导出项目.Name = "lblBOM导出项目"
        Me.lblBOM导出项目.Size = New System.Drawing.Size(125, 18)
        Me.lblBOM导出项目.TabIndex = 4
        Me.lblBOM导出项目.Text = "BOM导出项目："
        '
        'TabPage常规
        '
        Me.TabPage常规.Controls.Add(Me.GroupBox快速打印)
        Me.TabPage常规.Controls.Add(Me.GroupBox精度设置)
        Me.TabPage常规.Controls.Add(Me.GroupBox签字)
        Me.TabPage常规.Controls.Add(Me.GroupBox比例映射)
        Me.TabPage常规.Controls.Add(Me.btn配置文件)
        Me.TabPage常规.Controls.Add(Me.GroupBox对称零件iProperty映射)
        Me.TabPage常规.Controls.Add(Me.GroupBoxiProperty映射)
        Me.TabPage常规.Location = New System.Drawing.Point(4, 28)
        Me.TabPage常规.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage常规.Name = "TabPage常规"
        Me.TabPage常规.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage常规.Size = New System.Drawing.Size(928, 456)
        Me.TabPage常规.TabIndex = 0
        Me.TabPage常规.Text = "常规"
        Me.TabPage常规.UseVisualStyleBackColor = True
        '
        'GroupBox快速打印
        '
        Me.GroupBox快速打印.Controls.Add(Me.cmb另存为)
        Me.GroupBox快速打印.Controls.Add(Me.cmb打印机)
        Me.GroupBox快速打印.Controls.Add(Me.chk匹配A3纸)
        Me.GroupBox快速打印.Controls.Add(Me.chk签字)
        Me.GroupBox快速打印.Controls.Add(Me.lbl打印机)
        Me.GroupBox快速打印.Location = New System.Drawing.Point(600, 213)
        Me.GroupBox快速打印.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox快速打印.Name = "GroupBox快速打印"
        Me.GroupBox快速打印.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox快速打印.Size = New System.Drawing.Size(310, 182)
        Me.GroupBox快速打印.TabIndex = 22
        Me.GroupBox快速打印.TabStop = False
        Me.GroupBox快速打印.Text = "快速打印"
        '
        'cmb另存为
        '
        Me.cmb另存为.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb另存为.FormattingEnabled = True
        Me.cmb另存为.Items.AddRange(New Object() {"不另存", "另存为dwg", "另存为pdf", "另存为dwg和pdf"})
        Me.cmb另存为.Location = New System.Drawing.Point(15, 124)
        Me.cmb另存为.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb另存为.Name = "cmb另存为"
        Me.cmb另存为.Size = New System.Drawing.Size(211, 26)
        Me.cmb另存为.TabIndex = 3
        '
        'cmb打印机
        '
        Me.cmb打印机.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb打印机.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmb打印机.FormattingEnabled = True
        Me.cmb打印机.Location = New System.Drawing.Point(86, 34)
        Me.cmb打印机.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb打印机.Name = "cmb打印机"
        Me.cmb打印机.Size = New System.Drawing.Size(210, 26)
        Me.cmb打印机.Sorted = True
        Me.cmb打印机.TabIndex = 0
        '
        'chk匹配A3纸
        '
        Me.chk匹配A3纸.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chk匹配A3纸.AutoSize = True
        Me.chk匹配A3纸.Checked = True
        Me.chk匹配A3纸.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk匹配A3纸.Location = New System.Drawing.Point(21, 82)
        Me.chk匹配A3纸.Margin = New System.Windows.Forms.Padding(4)
        Me.chk匹配A3纸.Name = "chk匹配A3纸"
        Me.chk匹配A3纸.Size = New System.Drawing.Size(106, 22)
        Me.chk匹配A3纸.TabIndex = 1
        Me.chk匹配A3纸.Text = "匹配A3纸"
        Me.chk匹配A3纸.UseVisualStyleBackColor = True
        '
        'chk签字
        '
        Me.chk签字.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chk签字.AutoSize = True
        Me.chk签字.Checked = True
        Me.chk签字.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk签字.Location = New System.Drawing.Point(156, 82)
        Me.chk签字.Margin = New System.Windows.Forms.Padding(4)
        Me.chk签字.Name = "chk签字"
        Me.chk签字.Size = New System.Drawing.Size(70, 22)
        Me.chk签字.TabIndex = 2
        Me.chk签字.Text = "签字"
        Me.chk签字.UseVisualStyleBackColor = True
        '
        'lbl打印机
        '
        Me.lbl打印机.AutoSize = True
        Me.lbl打印机.Location = New System.Drawing.Point(12, 39)
        Me.lbl打印机.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl打印机.Name = "lbl打印机"
        Me.lbl打印机.Size = New System.Drawing.Size(80, 18)
        Me.lbl打印机.TabIndex = 9
        Me.lbl打印机.Text = "打印机："
        '
        'GroupBox精度设置
        '
        Me.GroupBox精度设置.Controls.Add(Me.cmb面积精度)
        Me.GroupBox精度设置.Controls.Add(Me.lbl面积精度)
        Me.GroupBox精度设置.Controls.Add(Me.cmb质量精度)
        Me.GroupBox精度设置.Controls.Add(Me.lbl质量精度)
        Me.GroupBox精度设置.Location = New System.Drawing.Point(304, 312)
        Me.GroupBox精度设置.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox精度设置.Name = "GroupBox精度设置"
        Me.GroupBox精度设置.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox精度设置.Size = New System.Drawing.Size(270, 117)
        Me.GroupBox精度设置.TabIndex = 0
        Me.GroupBox精度设置.TabStop = False
        Me.GroupBox精度设置.Text = "精度设置"
        '
        'cmb面积精度
        '
        Me.cmb面积精度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb面积精度.FormattingEnabled = True
        Me.cmb面积精度.Items.AddRange(New Object() {"0", "0.1", "0.01", "0.001", "0.0001", "0.00001"})
        Me.cmb面积精度.Location = New System.Drawing.Point(120, 68)
        Me.cmb面积精度.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb面积精度.Name = "cmb面积精度"
        Me.cmb面积精度.Size = New System.Drawing.Size(97, 26)
        Me.cmb面积精度.TabIndex = 1
        '
        'lbl面积精度
        '
        Me.lbl面积精度.AutoSize = True
        Me.lbl面积精度.Location = New System.Drawing.Point(16, 74)
        Me.lbl面积精度.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl面积精度.Name = "lbl面积精度"
        Me.lbl面积精度.Size = New System.Drawing.Size(98, 18)
        Me.lbl面积精度.TabIndex = 11
        Me.lbl面积精度.Text = "面积精度："
        '
        'cmb质量精度
        '
        Me.cmb质量精度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb质量精度.FormattingEnabled = True
        Me.cmb质量精度.Items.AddRange(New Object() {"0", "0.1", "0.01", "0.001"})
        Me.cmb质量精度.Location = New System.Drawing.Point(120, 27)
        Me.cmb质量精度.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb质量精度.Name = "cmb质量精度"
        Me.cmb质量精度.Size = New System.Drawing.Size(97, 26)
        Me.cmb质量精度.TabIndex = 0
        '
        'lbl质量精度
        '
        Me.lbl质量精度.AutoSize = True
        Me.lbl质量精度.Location = New System.Drawing.Point(16, 33)
        Me.lbl质量精度.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl质量精度.Name = "lbl质量精度"
        Me.lbl质量精度.Size = New System.Drawing.Size(98, 18)
        Me.lbl质量精度.TabIndex = 9
        Me.lbl质量精度.Text = "质量精度："
        '
        'GroupBox签字
        '
        Me.GroupBox签字.Controls.Add(Me.chk短日期)
        Me.GroupBox签字.Controls.Add(Me.chk同时签字)
        Me.GroupBox签字.Controls.Add(Me.txt工程师)
        Me.GroupBox签字.Controls.Add(Me.lbl工程师)
        Me.GroupBox签字.Controls.Add(Me.chk签字后打印)
        Me.GroupBox签字.Controls.Add(Me.txt打印日期)
        Me.GroupBox签字.Controls.Add(Me.lbl打印日期)
        Me.GroupBox签字.Location = New System.Drawing.Point(600, 15)
        Me.GroupBox签字.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox签字.Name = "GroupBox签字"
        Me.GroupBox签字.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox签字.Size = New System.Drawing.Size(310, 188)
        Me.GroupBox签字.TabIndex = 19
        Me.GroupBox签字.TabStop = False
        Me.GroupBox签字.Text = "签字"
        '
        'chk短日期
        '
        Me.chk短日期.AutoSize = True
        Me.chk短日期.Location = New System.Drawing.Point(21, 148)
        Me.chk短日期.Margin = New System.Windows.Forms.Padding(4)
        Me.chk短日期.Name = "chk短日期"
        Me.chk短日期.Size = New System.Drawing.Size(88, 22)
        Me.chk短日期.TabIndex = 9
        Me.chk短日期.Text = "短日期"
        Me.chk短日期.UseVisualStyleBackColor = True
        '
        'chk同时签字
        '
        Me.chk同时签字.AutoSize = True
        Me.chk同时签字.Location = New System.Drawing.Point(192, 116)
        Me.chk同时签字.Margin = New System.Windows.Forms.Padding(4)
        Me.chk同时签字.Name = "chk同时签字"
        Me.chk同时签字.Size = New System.Drawing.Size(106, 22)
        Me.chk同时签字.TabIndex = 3
        Me.chk同时签字.Text = "同时签字"
        Me.chk同时签字.UseVisualStyleBackColor = True
        Me.chk同时签字.Visible = False
        '
        'txt工程师
        '
        Me.txt工程师.Location = New System.Drawing.Point(124, 69)
        Me.txt工程师.Margin = New System.Windows.Forms.Padding(4)
        Me.txt工程师.Name = "txt工程师"
        Me.txt工程师.Size = New System.Drawing.Size(145, 28)
        Me.txt工程师.TabIndex = 1
        '
        'lbl工程师
        '
        Me.lbl工程师.AutoSize = True
        Me.lbl工程师.Location = New System.Drawing.Point(18, 75)
        Me.lbl工程师.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl工程师.Name = "lbl工程师"
        Me.lbl工程师.Size = New System.Drawing.Size(80, 18)
        Me.lbl工程师.TabIndex = 8
        Me.lbl工程师.Text = "工程师："
        '
        'chk签字后打印
        '
        Me.chk签字后打印.AutoSize = True
        Me.chk签字后打印.Location = New System.Drawing.Point(21, 116)
        Me.chk签字后打印.Margin = New System.Windows.Forms.Padding(4)
        Me.chk签字后打印.Name = "chk签字后打印"
        Me.chk签字后打印.Size = New System.Drawing.Size(160, 22)
        Me.chk签字后打印.TabIndex = 2
        Me.chk签字后打印.Text = "签字后打开打印"
        Me.chk签字后打印.UseVisualStyleBackColor = True
        '
        'txt打印日期
        '
        Me.txt打印日期.Location = New System.Drawing.Point(124, 28)
        Me.txt打印日期.Margin = New System.Windows.Forms.Padding(4)
        Me.txt打印日期.Name = "txt打印日期"
        Me.txt打印日期.Size = New System.Drawing.Size(145, 28)
        Me.txt打印日期.TabIndex = 0
        '
        'lbl打印日期
        '
        Me.lbl打印日期.AutoSize = True
        Me.lbl打印日期.Location = New System.Drawing.Point(18, 34)
        Me.lbl打印日期.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl打印日期.Name = "lbl打印日期"
        Me.lbl打印日期.Size = New System.Drawing.Size(98, 18)
        Me.lbl打印日期.TabIndex = 4
        Me.lbl打印日期.Text = "打印日期："
        '
        'GroupBox比例映射
        '
        Me.GroupBox比例映射.Controls.Add(Me.txt图号)
        Me.GroupBox比例映射.Controls.Add(Me.txt比例)
        Me.GroupBox比例映射.Controls.Add(Me.lbl质量)
        Me.GroupBox比例映射.Controls.Add(Me.lbl比例)
        Me.GroupBox比例映射.Location = New System.Drawing.Point(304, 186)
        Me.GroupBox比例映射.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox比例映射.Name = "GroupBox比例映射"
        Me.GroupBox比例映射.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox比例映射.Size = New System.Drawing.Size(270, 117)
        Me.GroupBox比例映射.TabIndex = 18
        Me.GroupBox比例映射.TabStop = False
        Me.GroupBox比例映射.Text = "其他映射"
        '
        'txt图号
        '
        Me.txt图号.Location = New System.Drawing.Point(81, 68)
        Me.txt图号.Margin = New System.Windows.Forms.Padding(4)
        Me.txt图号.Name = "txt图号"
        Me.txt图号.Size = New System.Drawing.Size(94, 28)
        Me.txt图号.TabIndex = 6
        '
        'txt比例
        '
        Me.txt比例.Location = New System.Drawing.Point(81, 27)
        Me.txt比例.Margin = New System.Windows.Forms.Padding(4)
        Me.txt比例.Name = "txt比例"
        Me.txt比例.Size = New System.Drawing.Size(94, 28)
        Me.txt比例.TabIndex = 0
        '
        'lbl质量
        '
        Me.lbl质量.AutoSize = True
        Me.lbl质量.Location = New System.Drawing.Point(16, 74)
        Me.lbl质量.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl质量.Name = "lbl质量"
        Me.lbl质量.Size = New System.Drawing.Size(62, 18)
        Me.lbl质量.TabIndex = 0
        Me.lbl质量.Text = "质量："
        '
        'lbl比例
        '
        Me.lbl比例.AutoSize = True
        Me.lbl比例.Location = New System.Drawing.Point(18, 33)
        Me.lbl比例.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl比例.Name = "lbl比例"
        Me.lbl比例.Size = New System.Drawing.Size(62, 18)
        Me.lbl比例.TabIndex = 4
        Me.lbl比例.Text = "比例："
        '
        'btn配置文件
        '
        Me.btn配置文件.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn配置文件.ContextMenuStrip = Me.ContextMenuStrip配置文件
        Me.btn配置文件.Location = New System.Drawing.Point(9, 390)
        Me.btn配置文件.Margin = New System.Windows.Forms.Padding(4)
        Me.btn配置文件.Name = "btn配置文件"
        Me.btn配置文件.Size = New System.Drawing.Size(112, 42)
        Me.btn配置文件.TabIndex = 0
        Me.btn配置文件.Text = "配置文件"
        Me.btn配置文件.UseVisualStyleBackColor = True
        '
        'GroupBox对称零件iProperty映射
        '
        Me.GroupBox对称零件iProperty映射.Controls.Add(Me.txt对称件编码映射)
        Me.GroupBox对称零件iProperty映射.Controls.Add(Me.lbl对称件编码)
        Me.GroupBox对称零件iProperty映射.Controls.Add(Me.txt对称件文件名映射)
        Me.GroupBox对称零件iProperty映射.Controls.Add(Me.txt对称件图号映射)
        Me.GroupBox对称零件iProperty映射.Controls.Add(Me.lbl对称件文件名)
        Me.GroupBox对称零件iProperty映射.Controls.Add(Me.lbl对称件图号)
        Me.GroupBox对称零件iProperty映射.Location = New System.Drawing.Point(304, 15)
        Me.GroupBox对称零件iProperty映射.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox对称零件iProperty映射.Name = "GroupBox对称零件iProperty映射"
        Me.GroupBox对称零件iProperty映射.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox对称零件iProperty映射.Size = New System.Drawing.Size(270, 158)
        Me.GroupBox对称零件iProperty映射.TabIndex = 17
        Me.GroupBox对称零件iProperty映射.TabStop = False
        Me.GroupBox对称零件iProperty映射.Text = "对称零件iProperty映射"
        '
        'txt对称件编码映射
        '
        Me.txt对称件编码映射.Location = New System.Drawing.Point(116, 112)
        Me.txt对称件编码映射.Margin = New System.Windows.Forms.Padding(4)
        Me.txt对称件编码映射.Name = "txt对称件编码映射"
        Me.txt对称件编码映射.Size = New System.Drawing.Size(128, 28)
        Me.txt对称件编码映射.TabIndex = 5
        '
        'lbl对称件编码
        '
        Me.lbl对称件编码.AutoSize = True
        Me.lbl对称件编码.Location = New System.Drawing.Point(32, 120)
        Me.lbl对称件编码.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl对称件编码.Name = "lbl对称件编码"
        Me.lbl对称件编码.Size = New System.Drawing.Size(80, 18)
        Me.lbl对称件编码.TabIndex = 6
        Me.lbl对称件编码.Text = "编  码："
        '
        'txt对称件文件名映射
        '
        Me.txt对称件文件名映射.Location = New System.Drawing.Point(116, 74)
        Me.txt对称件文件名映射.Margin = New System.Windows.Forms.Padding(4)
        Me.txt对称件文件名映射.Name = "txt对称件文件名映射"
        Me.txt对称件文件名映射.Size = New System.Drawing.Size(128, 28)
        Me.txt对称件文件名映射.TabIndex = 1
        '
        'txt对称件图号映射
        '
        Me.txt对称件图号映射.Location = New System.Drawing.Point(116, 30)
        Me.txt对称件图号映射.Margin = New System.Windows.Forms.Padding(4)
        Me.txt对称件图号映射.Name = "txt对称件图号映射"
        Me.txt对称件图号映射.Size = New System.Drawing.Size(128, 28)
        Me.txt对称件图号映射.TabIndex = 0
        '
        'lbl对称件文件名
        '
        Me.lbl对称件文件名.AutoSize = True
        Me.lbl对称件文件名.Location = New System.Drawing.Point(32, 78)
        Me.lbl对称件文件名.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl对称件文件名.Name = "lbl对称件文件名"
        Me.lbl对称件文件名.Size = New System.Drawing.Size(80, 18)
        Me.lbl对称件文件名.TabIndex = 4
        Me.lbl对称件文件名.Text = "文件名："
        '
        'lbl对称件图号
        '
        Me.lbl对称件图号.AutoSize = True
        Me.lbl对称件图号.Location = New System.Drawing.Point(32, 36)
        Me.lbl对称件图号.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl对称件图号.Name = "lbl对称件图号"
        Me.lbl对称件图号.Size = New System.Drawing.Size(80, 18)
        Me.lbl对称件图号.TabIndex = 3
        Me.lbl对称件图号.Text = "图  号："
        '
        'GroupBoxiProperty映射
        '
        Me.GroupBoxiProperty映射.Controls.Add(Me.cmb连接符)
        Me.GroupBoxiProperty映射.Controls.Add(Me.lbl连接符)
        Me.GroupBoxiProperty映射.Controls.Add(Me.cmb供应商)
        Me.GroupBoxiProperty映射.Controls.Add(Me.lbl采购来源)
        Me.GroupBoxiProperty映射.Controls.Add(Me.cmb存货编码)
        Me.GroupBoxiProperty映射.Controls.Add(Me.lbl存货编码)
        Me.GroupBoxiProperty映射.Controls.Add(Me.lbl文件名)
        Me.GroupBoxiProperty映射.Controls.Add(Me.cmb文件名)
        Me.GroupBoxiProperty映射.Controls.Add(Me.lbl图号)
        Me.GroupBoxiProperty映射.Controls.Add(Me.cmb图号)
        Me.GroupBoxiProperty映射.Location = New System.Drawing.Point(9, 15)
        Me.GroupBoxiProperty映射.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxiProperty映射.Name = "GroupBoxiProperty映射"
        Me.GroupBoxiProperty映射.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxiProperty映射.Size = New System.Drawing.Size(286, 252)
        Me.GroupBoxiProperty映射.TabIndex = 16
        Me.GroupBoxiProperty映射.TabStop = False
        Me.GroupBoxiProperty映射.Text = "iProperty映射"
        '
        'cmb连接符
        '
        Me.cmb连接符.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb连接符.FormattingEnabled = True
        Me.cmb连接符.Items.AddRange(New Object() {"无", "空格", "点.", "短横线-", "下划线_"})
        Me.cmb连接符.Location = New System.Drawing.Point(122, 204)
        Me.cmb连接符.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb连接符.Name = "cmb连接符"
        Me.cmb连接符.Size = New System.Drawing.Size(134, 26)
        Me.cmb连接符.TabIndex = 38
        '
        'lbl连接符
        '
        Me.lbl连接符.AutoSize = True
        Me.lbl连接符.Location = New System.Drawing.Point(24, 210)
        Me.lbl连接符.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl连接符.Name = "lbl连接符"
        Me.lbl连接符.Size = New System.Drawing.Size(80, 18)
        Me.lbl连接符.TabIndex = 37
        Me.lbl连接符.Text = "连接符："
        '
        'cmb供应商
        '
        Me.cmb供应商.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb供应商.FormattingEnabled = True
        Me.cmb供应商.Items.AddRange(New Object() {"成本中心", "供应商", "库存编号", "零件代号", "描述", "项目", "预估成本"})
        Me.cmb供应商.Location = New System.Drawing.Point(128, 158)
        Me.cmb供应商.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb供应商.Name = "cmb供应商"
        Me.cmb供应商.Size = New System.Drawing.Size(127, 26)
        Me.cmb供应商.Sorted = True
        Me.cmb供应商.TabIndex = 3
        '
        'lbl采购来源
        '
        Me.lbl采购来源.AutoSize = True
        Me.lbl采购来源.Location = New System.Drawing.Point(24, 162)
        Me.lbl采购来源.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl采购来源.Name = "lbl采购来源"
        Me.lbl采购来源.Size = New System.Drawing.Size(98, 18)
        Me.lbl采购来源.TabIndex = 7
        Me.lbl采购来源.Text = "采购来源："
        '
        'cmb存货编码
        '
        Me.cmb存货编码.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb存货编码.FormattingEnabled = True
        Me.cmb存货编码.Items.AddRange(New Object() {"成本中心", "供应商", "库存编号", "零件代号", "描述", "项目", "预估成本"})
        Me.cmb存货编码.Location = New System.Drawing.Point(128, 112)
        Me.cmb存货编码.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb存货编码.Name = "cmb存货编码"
        Me.cmb存货编码.Size = New System.Drawing.Size(127, 26)
        Me.cmb存货编码.Sorted = True
        Me.cmb存货编码.TabIndex = 2
        '
        'lbl存货编码
        '
        Me.lbl存货编码.AutoSize = True
        Me.lbl存货编码.Location = New System.Drawing.Point(24, 120)
        Me.lbl存货编码.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl存货编码.Name = "lbl存货编码"
        Me.lbl存货编码.Size = New System.Drawing.Size(98, 18)
        Me.lbl存货编码.TabIndex = 5
        Me.lbl存货编码.Text = "存货编码："
        '
        'lbl文件名
        '
        Me.lbl文件名.AutoSize = True
        Me.lbl文件名.Location = New System.Drawing.Point(24, 78)
        Me.lbl文件名.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl文件名.Name = "lbl文件名"
        Me.lbl文件名.Size = New System.Drawing.Size(80, 18)
        Me.lbl文件名.TabIndex = 4
        Me.lbl文件名.Text = "文件名："
        '
        'cmb文件名
        '
        Me.cmb文件名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb文件名.FormattingEnabled = True
        Me.cmb文件名.Items.AddRange(New Object() {"库存编号", "零件代号"})
        Me.cmb文件名.Location = New System.Drawing.Point(128, 74)
        Me.cmb文件名.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb文件名.Name = "cmb文件名"
        Me.cmb文件名.Size = New System.Drawing.Size(128, 26)
        Me.cmb文件名.Sorted = True
        Me.cmb文件名.TabIndex = 1
        '
        'lbl图号
        '
        Me.lbl图号.AutoSize = True
        Me.lbl图号.Location = New System.Drawing.Point(24, 36)
        Me.lbl图号.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl图号.Name = "lbl图号"
        Me.lbl图号.Size = New System.Drawing.Size(80, 18)
        Me.lbl图号.TabIndex = 3
        Me.lbl图号.Text = "图  号："
        '
        'cmb图号
        '
        Me.cmb图号.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb图号.Items.AddRange(New Object() {"库存编号", "零件代号"})
        Me.cmb图号.Location = New System.Drawing.Point(128, 30)
        Me.cmb图号.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb图号.Name = "cmb图号"
        Me.cmb图号.Size = New System.Drawing.Size(128, 26)
        Me.cmb图号.Sorted = True
        Me.cmb图号.TabIndex = 0
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage常规)
        Me.TabControl.Controls.Add(Me.TabPage模型)
        Me.TabControl.Controls.Add(Me.TabPage工程图)
        Me.TabControl.Controls.Add(Me.TabPage展开图)
        Me.TabControl.Controls.Add(Me.TabPage其他)
        Me.TabControl.Location = New System.Drawing.Point(3, 16)
        Me.TabControl.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(936, 488)
        Me.TabControl.TabIndex = 0
        '
        'FormOption
        '
        Me.AcceptButton = Me.btn确定
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn关闭
        Me.ClientSize = New System.Drawing.Size(957, 572)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.btn确定)
        Me.Controls.Add(Me.btn关闭)
        Me.Controls.Add(Me.chk检查更新)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FormOption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "应用程序选项"
        Me.ContextMenuStrip配置文件.ResumeLayout(False)
        Me.TabPage其他.ResumeLayout(False)
        Me.TabPage其他.PerformLayout()
        Me.GroupBox自动保存.ResumeLayout(False)
        Me.GroupBox自动保存.PerformLayout()
        Me.GroupBox截图.ResumeLayout(False)
        Me.GroupBox截图.PerformLayout()
        Me.TabPage展开图.ResumeLayout(False)
        Me.TabPage展开图.PerformLayout()
        Me.GroupBox展开图模板.ResumeLayout(False)
        Me.GroupBox展开图模板.PerformLayout()
        Me.GroupBox下.ResumeLayout(False)
        Me.GroupBox下.PerformLayout()
        Me.GroupBox上.ResumeLayout(False)
        Me.GroupBox上.PerformLayout()
        Me.TabPage工程图.ResumeLayout(False)
        Me.TabPage工程图.PerformLayout()
        Me.GroupBox标题栏.ResumeLayout(False)
        Me.GroupBox标题栏.PerformLayout()
        Me.GroupBox页边距.ResumeLayout(False)
        Me.GroupBox页边距.PerformLayout()
        CType(Me.NumericUpDown页边距右, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown页边距左, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown页边距下, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown页边距上, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox视图.ResumeLayout(False)
        Me.GroupBox视图.PerformLayout()
        Me.GroupBox工程图模板.ResumeLayout(False)
        Me.GroupBox工程图模板.PerformLayout()
        Me.TabPage模型.ResumeLayout(False)
        Me.TabPage模型.PerformLayout()
        CType(Me.NUD查找文件夹层数, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxERP查询.ResumeLayout(False)
        Me.GroupBoxERP查询.PerformLayout()
        Me.GroupBoxBOM导出项目.ResumeLayout(False)
        Me.GroupBoxBOM导出项目.PerformLayout()
        Me.TabPage常规.ResumeLayout(False)
        Me.GroupBox快速打印.ResumeLayout(False)
        Me.GroupBox快速打印.PerformLayout()
        Me.GroupBox精度设置.ResumeLayout(False)
        Me.GroupBox精度设置.PerformLayout()
        Me.GroupBox签字.ResumeLayout(False)
        Me.GroupBox签字.PerformLayout()
        Me.GroupBox比例映射.ResumeLayout(False)
        Me.GroupBox比例映射.PerformLayout()
        Me.GroupBox对称零件iProperty映射.ResumeLayout(False)
        Me.GroupBox对称零件iProperty映射.PerformLayout()
        Me.GroupBoxiProperty映射.ResumeLayout(False)
        Me.GroupBoxiProperty映射.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn确定 As System.Windows.Forms.Button
    Friend WithEvents btn关闭 As System.Windows.Forms.Button
    Friend WithEvents chk检查更新 As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip配置文件 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem全局 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem图框替换 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem安装目录 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage其他 As TabPage
    Friend WithEvents GroupBox自动保存 As GroupBox
    Friend WithEvents cmb时间间隔 As ComboBox
    Friend WithEvents chk启用自动保存 As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbl保存文档 As Label
    Friend WithEvents cmb保存文档 As ComboBox
    Friend WithEvents GroupBox截图 As GroupBox
    Friend WithEvents txt高度 As TextBox
    Friend WithEvents txt宽度 As TextBox
    Friend WithEvents lbl高度 As Label
    Friend WithEvents lbl宽度 As Label
    Friend WithEvents lvw设置图标大小 As ListView
    Friend WithEvents ch按钮 As ColumnHeader
    Friend WithEvents ch图标大小 As ColumnHeader
    Friend WithEvents TabPage展开图 As TabPage
    Friend WithEvents chk保存展开图到指定文件夹 As CheckBox
    Friend WithEvents txt工艺文字高 As TextBox
    Friend WithEvents txt标记孔径上限 As TextBox
    Friend WithEvents lbl工艺文字高 As Label
    Friend WithEvents cmb图号材质 As ComboBox
    Friend WithEvents lbl图号材质 As Label
    Friend WithEvents lbl标记孔径上限 As Label
    Friend WithEvents chk展开图隐藏螺纹特征 As CheckBox
    Friend WithEvents GroupBox展开图模板 As GroupBox
    Friend WithEvents btn展开图模板 As Button
    Friend WithEvents txt展开图模板 As TextBox
    Friend WithEvents lbl展开图模板 As Label
    Friend WithEvents chk展开图标注 As CheckBox
    Friend WithEvents GroupBox下 As GroupBox
    Friend WithEvents cmb向下线宽 As ComboBox
    Friend WithEvents cmb向下线型 As ComboBox
    Friend WithEvents lbl颜色下 As Label
    Friend WithEvents btn向下颜色 As Button
    Friend WithEvents lbl线宽下 As Label
    Friend WithEvents lbl线型下 As Label
    Friend WithEvents GroupBox上 As GroupBox
    Friend WithEvents cmb向上线宽 As ComboBox
    Friend WithEvents cmb向上线型 As ComboBox
    Friend WithEvents lbl颜色上 As Label
    Friend WithEvents btn向上颜色 As Button
    Friend WithEvents lbl线宽上 As Label
    Friend WithEvents lbl线型上 As Label
    Friend WithEvents TabPage工程图 As TabPage
    Friend WithEvents chk强制横向 As CheckBox
    Friend WithEvents chk逆时针序号 As CheckBox
    Friend WithEvents chk模型匹配检查 As CheckBox
    Friend WithEvents GroupBox标题栏 As GroupBox
    Friend WithEvents txt零件图框 As TextBox
    Friend WithEvents txt部件图框 As TextBox
    Friend WithEvents lbl零件图框 As Label
    Friend WithEvents lbl部件图框 As Label
    Friend WithEvents GroupBox页边距 As GroupBox
    Friend WithEvents NumericUpDown页边距右 As NumericUpDown
    Friend WithEvents NumericUpDown页边距左 As NumericUpDown
    Friend WithEvents NumericUpDown页边距下 As NumericUpDown
    Friend WithEvents NumericUpDown页边距上 As NumericUpDown
    Friend WithEvents lb页边距 As Label
    Friend WithEvents GroupBox视图 As GroupBox
    Friend WithEvents chk标注尺寸 As CheckBox
    Friend WithEvents lbl前视图 As Label
    Friend WithEvents chk左视图 As CheckBox
    Friend WithEvents chk俯视图 As CheckBox
    Friend WithEvents chk右视图 As CheckBox
    Friend WithEvents chk仰视图 As CheckBox
    Friend WithEvents chk工程图螺纹特征 As CheckBox
    Friend WithEvents rdo显示隐藏线 As RadioButton
    Friend WithEvents rdo不显示隐藏线 As RadioButton
    Friend WithEvents chk相切边 As CheckBox
    Friend WithEvents chk钣金自动展开 As CheckBox
    Friend WithEvents chk第三视角 As CheckBox
    Friend WithEvents GroupBox工程图模板 As GroupBox
    Friend WithEvents btn选择工程图模板 As Button
    Friend WithEvents txt工程图模板 As TextBox
    Friend WithEvents lbl模板工程图 As Label
    Friend WithEvents TabPage模型 As TabPage
    Friend WithEvents txt钣金厚度前缀 As TextBox
    Friend WithEvents txt去除后缀 As TextBox
    Friend WithEvents chk钣金厚度检查 As CheckBox
    Friend WithEvents lbl去除后缀 As Label
    Friend WithEvents chk检查重复图号 As CheckBox
    Friend WithEvents lbl查找文件夹层数 As Label
    Friend WithEvents NUD查找文件夹层数 As NumericUpDown
    Friend WithEvents chk另存到子文件夹 As CheckBox
    Friend WithEvents chk备份工程图 As CheckBox
    Friend WithEvents GroupBoxERP查询 As GroupBox
    Friend WithEvents btn选择erp数据库 As Button
    Friend WithEvents btn更新数据库 As Button
    Friend WithEvents btn打开erp数据库 As Button
    Friend WithEvents txt查询列 As TextBox
    Friend WithEvents lbl编码列 As Label
    Friend WithEvents txt查找范围 As TextBox
    Friend WithEvents lbl查询列 As Label
    Friend WithEvents txt基础数据文件 As TextBox
    Friend WithEvents lbl基础数据文件 As Label
    Friend WithEvents GroupBoxBOM导出项目 As GroupBox
    Friend WithEvents chk使用UTF8编码 As CheckBox
    Friend WithEvents btn清除 As Button
    Friend WithEvents btn还原 As Button
    Friend WithEvents btn添加 As Button
    Friend WithEvents cmb添加 As ComboBox
    Friend WithEvents txtBOM导出项 As TextBox
    Friend WithEvents lblBOM导出项目 As Label
    Friend WithEvents TabPage常规 As TabPage
    Friend WithEvents GroupBox快速打印 As GroupBox
    Friend WithEvents cmb另存为 As ComboBox
    Friend WithEvents cmb打印机 As ComboBox
    Friend WithEvents chk匹配A3纸 As CheckBox
    Friend WithEvents chk签字 As CheckBox
    Friend WithEvents lbl打印机 As Label
    Friend WithEvents GroupBox精度设置 As GroupBox
    Friend WithEvents cmb面积精度 As ComboBox
    Friend WithEvents lbl面积精度 As Label
    Friend WithEvents cmb质量精度 As ComboBox
    Friend WithEvents lbl质量精度 As Label
    Friend WithEvents GroupBox签字 As GroupBox
    Friend WithEvents chk短日期 As CheckBox
    Friend WithEvents chk同时签字 As CheckBox
    Friend WithEvents txt工程师 As TextBox
    Friend WithEvents lbl工程师 As Label
    Friend WithEvents chk签字后打印 As CheckBox
    Friend WithEvents txt打印日期 As TextBox
    Friend WithEvents lbl打印日期 As Label
    Friend WithEvents GroupBox比例映射 As GroupBox
    Friend WithEvents txt图号 As TextBox
    Friend WithEvents txt比例 As TextBox
    Friend WithEvents lbl质量 As Label
    Friend WithEvents lbl比例 As Label
    Friend WithEvents btn配置文件 As Button
    Friend WithEvents GroupBox对称零件iProperty映射 As GroupBox
    Friend WithEvents txt对称件编码映射 As TextBox
    Friend WithEvents lbl对称件编码 As Label
    Friend WithEvents txt对称件文件名映射 As TextBox
    Friend WithEvents txt对称件图号映射 As TextBox
    Friend WithEvents lbl对称件文件名 As Label
    Friend WithEvents lbl对称件图号 As Label
    Friend WithEvents GroupBoxiProperty映射 As GroupBox
    Friend WithEvents cmb供应商 As ComboBox
    Friend WithEvents lbl采购来源 As Label
    Friend WithEvents cmb存货编码 As ComboBox
    Friend WithEvents lbl存货编码 As Label
    Friend WithEvents lbl文件名 As Label
    Friend WithEvents cmb文件名 As ComboBox
    Friend WithEvents lbl图号 As Label
    Friend WithEvents cmb图号 As ComboBox
    Friend WithEvents TabControl As TabControl
    Friend WithEvents cmb连接符 As ComboBox
    Friend WithEvents lbl连接符 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbx增加后缀 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbx中间符号 As ComboBox
End Class
