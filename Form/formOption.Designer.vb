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
        components = New System.ComponentModel.Container()
        btn确定 = New Button()
        btn关闭 = New Button()
        chk检查更新 = New CheckBox()
        ContextMenuStrip配置文件 = New ContextMenuStrip(components)
        ToolStripMenuItem全局 = New ToolStripMenuItem()
        ToolStripMenuItem图框替换 = New ToolStripMenuItem()
        ToolStripMenuItem安装目录 = New ToolStripMenuItem()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        TabPage其他 = New TabPage()
        Label4 = New Label()
        cbx中间符号 = New ComboBox()
        GroupBox自动保存 = New GroupBox()
        cmb时间间隔 = New ComboBox()
        chk启用自动保存 = New CheckBox()
        Label2 = New Label()
        lbl保存文档 = New Label()
        cmb保存文档 = New ComboBox()
        GroupBox截图 = New GroupBox()
        txt高度 = New TextBox()
        txt宽度 = New TextBox()
        lbl高度 = New Label()
        lbl宽度 = New Label()
        TabPage展开图 = New TabPage()
        chk保存展开图到指定文件夹 = New CheckBox()
        txt工艺文字高 = New TextBox()
        txt标记孔径上限 = New TextBox()
        lbl工艺文字高 = New Label()
        cmb图号材质 = New ComboBox()
        lbl图号材质 = New Label()
        lbl标记孔径上限 = New Label()
        chk展开图隐藏螺纹特征 = New CheckBox()
        GroupBox展开图模板 = New GroupBox()
        btn展开图模板 = New Button()
        txt展开图模板 = New TextBox()
        lbl展开图模板 = New Label()
        chk展开图标注 = New CheckBox()
        GroupBox下 = New GroupBox()
        cmb向下线宽 = New ComboBox()
        cmb向下线型 = New ComboBox()
        lbl颜色下 = New Label()
        btn向下颜色 = New Button()
        lbl线宽下 = New Label()
        lbl线型下 = New Label()
        GroupBox上 = New GroupBox()
        cmb向上线宽 = New ComboBox()
        cmb向上线型 = New ComboBox()
        lbl颜色上 = New Label()
        btn向上颜色 = New Button()
        lbl线宽上 = New Label()
        lbl线型上 = New Label()
        TabPage工程图 = New TabPage()
        chk强制横向 = New CheckBox()
        chk逆时针序号 = New CheckBox()
        chk模型匹配检查 = New CheckBox()
        GroupBox标题栏 = New GroupBox()
        Label3 = New Label()
        cbx增加后缀 = New ComboBox()
        txt零件图框 = New TextBox()
        txt部件图框 = New TextBox()
        lbl零件图框 = New Label()
        lbl部件图框 = New Label()
        GroupBox页边距 = New GroupBox()
        NumericUpDown页边距右 = New NumericUpDown()
        NumericUpDown页边距左 = New NumericUpDown()
        NumericUpDown页边距下 = New NumericUpDown()
        NumericUpDown页边距上 = New NumericUpDown()
        lb页边距 = New Label()
        GroupBox视图 = New GroupBox()
        chk标注尺寸 = New CheckBox()
        lbl前视图 = New Label()
        chk左视图 = New CheckBox()
        chk俯视图 = New CheckBox()
        chk右视图 = New CheckBox()
        chk仰视图 = New CheckBox()
        chk工程图螺纹特征 = New CheckBox()
        rdo显示隐藏线 = New RadioButton()
        rdo不显示隐藏线 = New RadioButton()
        chk相切边 = New CheckBox()
        chk钣金自动展开 = New CheckBox()
        chk第三视角 = New CheckBox()
        GroupBox工程图模板 = New GroupBox()
        btn选择工程图模板 = New Button()
        txt工程图模板 = New TextBox()
        lbl模板工程图 = New Label()
        TabPage模型 = New TabPage()
        txtBOM显示列 = New TextBox()
        txt钣金厚度前缀 = New TextBox()
        Label5 = New Label()
        txt去除后缀 = New TextBox()
        chk钣金厚度检查 = New CheckBox()
        lbl去除后缀 = New Label()
        chk检查重复图号 = New CheckBox()
        lbl查找文件夹层数 = New Label()
        NUD查找文件夹层数 = New NumericUpDown()
        chk另存到子文件夹 = New CheckBox()
        chk备份工程图 = New CheckBox()
        GroupBoxERP查询 = New GroupBox()
        btn选择erp数据库 = New Button()
        btn更新数据库 = New Button()
        btn打开erp数据库 = New Button()
        txt查询列 = New TextBox()
        lbl编码列 = New Label()
        txt查找范围 = New TextBox()
        lbl查询列 = New Label()
        txt基础数据文件 = New TextBox()
        lbl基础数据文件 = New Label()
        GroupBoxBOM导出项目 = New GroupBox()
        chk使用UTF8编码 = New CheckBox()
        btn清除 = New Button()
        btn还原 = New Button()
        btn添加 = New Button()
        cmb添加 = New ComboBox()
        txtBOM导出项 = New TextBox()
        lblBOM导出项目 = New Label()
        TabPage常规 = New TabPage()
        GroupBox快速打印 = New GroupBox()
        cmb另存为 = New ComboBox()
        cmb打印机 = New ComboBox()
        chk匹配A3纸 = New CheckBox()
        chk签字 = New CheckBox()
        lbl打印机 = New Label()
        GroupBox精度设置 = New GroupBox()
        cmb面积精度 = New ComboBox()
        lbl面积精度 = New Label()
        cmb质量精度 = New ComboBox()
        lbl质量精度 = New Label()
        GroupBox签字 = New GroupBox()
        chk短日期 = New CheckBox()
        chk同时签字 = New CheckBox()
        txt工程师 = New TextBox()
        lbl工程师 = New Label()
        chk签字后打印 = New CheckBox()
        txt打印日期 = New TextBox()
        lbl打印日期 = New Label()
        GroupBox比例映射 = New GroupBox()
        txt图号 = New TextBox()
        txt比例 = New TextBox()
        lbl质量 = New Label()
        lbl比例 = New Label()
        btn配置文件 = New Button()
        GroupBox对称零件iProperty映射 = New GroupBox()
        txt对称件编码映射 = New TextBox()
        lbl对称件编码 = New Label()
        txt对称件文件名映射 = New TextBox()
        txt对称件图号映射 = New TextBox()
        lbl对称件文件名 = New Label()
        lbl对称件图号 = New Label()
        GroupBoxiProperty映射 = New GroupBox()
        cmb连接符 = New ComboBox()
        lbl连接符 = New Label()
        cmb供应商 = New ComboBox()
        lbl采购来源 = New Label()
        cmb存货编码 = New ComboBox()
        lbl存货编码 = New Label()
        lbl文件名 = New Label()
        cmb文件名 = New ComboBox()
        lbl图号 = New Label()
        cmb图号 = New ComboBox()
        TabControl = New TabControl()
        Btn添加2 = New Button()
        ContextMenuStrip配置文件.SuspendLayout()
        TabPage其他.SuspendLayout()
        GroupBox自动保存.SuspendLayout()
        GroupBox截图.SuspendLayout()
        TabPage展开图.SuspendLayout()
        GroupBox展开图模板.SuspendLayout()
        GroupBox下.SuspendLayout()
        GroupBox上.SuspendLayout()
        TabPage工程图.SuspendLayout()
        GroupBox标题栏.SuspendLayout()
        GroupBox页边距.SuspendLayout()
        CType(NumericUpDown页边距右, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown页边距左, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown页边距下, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown页边距上, System.ComponentModel.ISupportInitialize).BeginInit()
        GroupBox视图.SuspendLayout()
        GroupBox工程图模板.SuspendLayout()
        TabPage模型.SuspendLayout()
        CType(NUD查找文件夹层数, System.ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxERP查询.SuspendLayout()
        GroupBoxBOM导出项目.SuspendLayout()
        TabPage常规.SuspendLayout()
        GroupBox快速打印.SuspendLayout()
        GroupBox精度设置.SuspendLayout()
        GroupBox签字.SuspendLayout()
        GroupBox比例映射.SuspendLayout()
        GroupBox对称零件iProperty映射.SuspendLayout()
        GroupBoxiProperty映射.SuspendLayout()
        TabControl.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn确定
        ' 
        btn确定.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn确定.Location = New System.Drawing.Point(767, 716)
        btn确定.Margin = New Padding(4, 5, 4, 5)
        btn确定.Name = "btn确定"
        btn确定.Size = New Size(124, 59)
        btn确定.TabIndex = 1
        btn确定.Text = "确定"
        ' 
        ' btn关闭
        ' 
        btn关闭.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn关闭.DialogResult = DialogResult.Cancel
        btn关闭.Location = New System.Drawing.Point(918, 716)
        btn关闭.Margin = New Padding(4, 5, 4, 5)
        btn关闭.Name = "btn关闭"
        btn关闭.Size = New Size(124, 59)
        btn关闭.TabIndex = 2
        btn关闭.Text = "关闭"
        ' 
        ' chk检查更新
        ' 
        chk检查更新.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        chk检查更新.AutoSize = True
        chk检查更新.Checked = True
        chk检查更新.CheckState = CheckState.Checked
        chk检查更新.Location = New System.Drawing.Point(42, 733)
        chk检查更新.Margin = New Padding(4, 5, 4, 5)
        chk检查更新.Name = "chk检查更新"
        chk检查更新.Size = New Size(171, 29)
        chk检查更新.TabIndex = 3
        chk检查更新.Text = "启动时检查更新"
        chk检查更新.UseVisualStyleBackColor = True
        ' 
        ' ContextMenuStrip配置文件
        ' 
        ContextMenuStrip配置文件.ImageScalingSize = New Size(24, 24)
        ContextMenuStrip配置文件.Items.AddRange(New ToolStripItem() {ToolStripMenuItem全局, ToolStripMenuItem图框替换, ToolStripMenuItem安装目录})
        ContextMenuStrip配置文件.Name = "ContextMenuStrip配置文件"
        ContextMenuStrip配置文件.Size = New Size(199, 100)
        ' 
        ' ToolStripMenuItem全局
        ' 
        ToolStripMenuItem全局.Name = "ToolStripMenuItem全局"
        ToolStripMenuItem全局.Size = New Size(198, 32)
        ToolStripMenuItem全局.Text = "全局配置文件"
        ' 
        ' ToolStripMenuItem图框替换
        ' 
        ToolStripMenuItem图框替换.Name = "ToolStripMenuItem图框替换"
        ToolStripMenuItem图框替换.Size = New Size(198, 32)
        ToolStripMenuItem图框替换.Text = "图框替换..."
        ' 
        ' ToolStripMenuItem安装目录
        ' 
        ToolStripMenuItem安装目录.Name = "ToolStripMenuItem安装目录"
        ToolStripMenuItem安装目录.Size = New Size(198, 32)
        ToolStripMenuItem安装目录.Text = "安装目录"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(0, 63)
        Label1.Name = "Label1"
        Label1.Size = New Size(77, 12)
        Label1.TabIndex = 7
        Label1.Text = "22222222222222222"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New System.Drawing.Point(103, 59)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(408, 31)
        TextBox1.TabIndex = 8
        ' 
        ' Button1
        ' 
        Button1.Location = New System.Drawing.Point(519, 57)
        Button1.Name = "Button1"
        Button1.Size = New Size(52, 25)
        Button1.TabIndex = 17
        Button1.Text = "选择"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TabPage其他
        ' 
        TabPage其他.Controls.Add(Label4)
        TabPage其他.Controls.Add(cbx中间符号)
        TabPage其他.Controls.Add(GroupBox自动保存)
        TabPage其他.Controls.Add(GroupBox截图)
        TabPage其他.Location = New System.Drawing.Point(4, 34)
        TabPage其他.Margin = New Padding(4, 5, 4, 5)
        TabPage其他.Name = "TabPage其他"
        TabPage其他.Size = New Size(1032, 640)
        TabPage其他.TabIndex = 4
        TabPage其他.Text = "其他"
        TabPage其他.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(527, 601)
        Label4.Name = "Label4"
        Label4.Size = New Size(204, 25)
        Label4.TabIndex = 50
        Label4.Text = "BOM重命名中间符号："
        ' 
        ' cbx中间符号
        ' 
        cbx中间符号.FormattingEnabled = True
        cbx中间符号.Items.AddRange(New Object() {""" """, "-", " "})
        cbx中间符号.Location = New System.Drawing.Point(737, 601)
        cbx中间符号.Margin = New Padding(3, 4, 3, 4)
        cbx中间符号.Name = "cbx中间符号"
        cbx中间符号.Size = New Size(126, 33)
        cbx中间符号.TabIndex = 49
        ' 
        ' GroupBox自动保存
        ' 
        GroupBox自动保存.Controls.Add(cmb时间间隔)
        GroupBox自动保存.Controls.Add(chk启用自动保存)
        GroupBox自动保存.Controls.Add(Label2)
        GroupBox自动保存.Controls.Add(lbl保存文档)
        GroupBox自动保存.Controls.Add(cmb保存文档)
        GroupBox自动保存.Location = New System.Drawing.Point(6, 430)
        GroupBox自动保存.Margin = New Padding(4, 5, 4, 5)
        GroupBox自动保存.Name = "GroupBox自动保存"
        GroupBox自动保存.Padding = New Padding(4, 5, 4, 5)
        GroupBox自动保存.Size = New Size(293, 214)
        GroupBox自动保存.TabIndex = 34
        GroupBox自动保存.TabStop = False
        GroupBox自动保存.Text = "自动保存"
        ' 
        ' cmb时间间隔
        ' 
        cmb时间间隔.FormattingEnabled = True
        cmb时间间隔.Items.AddRange(New Object() {"10", "120", "20", "30", "40", "50", "60", "90"})
        cmb时间间隔.Location = New System.Drawing.Point(56, 154)
        cmb时间间隔.Margin = New Padding(4, 5, 4, 5)
        cmb时间间隔.Name = "cmb时间间隔"
        cmb时间间隔.Size = New Size(107, 33)
        cmb时间间隔.TabIndex = 28
        cmb时间间隔.Text = "30"
        ' 
        ' chk启用自动保存
        ' 
        chk启用自动保存.AutoSize = True
        chk启用自动保存.Location = New System.Drawing.Point(17, 45)
        chk启用自动保存.Margin = New Padding(4, 5, 4, 5)
        chk启用自动保存.Name = "chk启用自动保存"
        chk启用自动保存.Size = New Size(152, 29)
        chk启用自动保存.TabIndex = 22
        chk启用自动保存.Text = "启用自动保存"
        chk启用自动保存.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(17, 162)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(172, 25)
        Label2.TabIndex = 26
        Label2.Text = "每             分钟保存"
        ' 
        ' lbl保存文档
        ' 
        lbl保存文档.AutoSize = True
        lbl保存文档.Location = New System.Drawing.Point(17, 98)
        lbl保存文档.Margin = New Padding(4, 0, 4, 0)
        lbl保存文档.Name = "lbl保存文档"
        lbl保存文档.Size = New Size(69, 25)
        lbl保存文档.TabIndex = 23
        lbl保存文档.Text = "保存："
        ' 
        ' cmb保存文档
        ' 
        cmb保存文档.DropDownStyle = ComboBoxStyle.DropDownList
        cmb保存文档.FormattingEnabled = True
        cmb保存文档.Items.AddRange(New Object() {"当前文档", "所有可见的文档", "所有打开的文档"})
        cmb保存文档.Location = New System.Drawing.Point(89, 89)
        cmb保存文档.Margin = New Padding(4, 5, 4, 5)
        cmb保存文档.Name = "cmb保存文档"
        cmb保存文档.Size = New Size(184, 33)
        cmb保存文档.TabIndex = 24
        ' 
        ' GroupBox截图
        ' 
        GroupBox截图.Controls.Add(txt高度)
        GroupBox截图.Controls.Add(txt宽度)
        GroupBox截图.Controls.Add(lbl高度)
        GroupBox截图.Controls.Add(lbl宽度)
        GroupBox截图.Location = New System.Drawing.Point(307, 443)
        GroupBox截图.Margin = New Padding(4, 5, 4, 5)
        GroupBox截图.Name = "GroupBox截图"
        GroupBox截图.Padding = New Padding(4, 5, 4, 5)
        GroupBox截图.Size = New Size(213, 192)
        GroupBox截图.TabIndex = 33
        GroupBox截图.TabStop = False
        GroupBox截图.Text = "截图"
        ' 
        ' txt高度
        ' 
        txt高度.Location = New System.Drawing.Point(78, 114)
        txt高度.Margin = New Padding(4, 5, 4, 5)
        txt高度.Name = "txt高度"
        txt高度.Size = New Size(107, 31)
        txt高度.TabIndex = 1
        ' 
        ' txt宽度
        ' 
        txt宽度.Location = New System.Drawing.Point(78, 46)
        txt宽度.Margin = New Padding(4, 5, 4, 5)
        txt宽度.Name = "txt宽度"
        txt宽度.Size = New Size(107, 31)
        txt宽度.TabIndex = 0
        ' 
        ' lbl高度
        ' 
        lbl高度.AutoSize = True
        lbl高度.Location = New System.Drawing.Point(10, 114)
        lbl高度.Margin = New Padding(4, 0, 4, 0)
        lbl高度.Name = "lbl高度"
        lbl高度.Size = New Size(69, 25)
        lbl高度.TabIndex = 9
        lbl高度.Text = "高度："
        ' 
        ' lbl宽度
        ' 
        lbl宽度.AutoSize = True
        lbl宽度.Location = New System.Drawing.Point(10, 52)
        lbl宽度.Margin = New Padding(4, 0, 4, 0)
        lbl宽度.Name = "lbl宽度"
        lbl宽度.Size = New Size(69, 25)
        lbl宽度.TabIndex = 8
        lbl宽度.Text = "宽度："
        ' 
        ' TabPage展开图
        ' 
        TabPage展开图.Controls.Add(chk保存展开图到指定文件夹)
        TabPage展开图.Controls.Add(txt工艺文字高)
        TabPage展开图.Controls.Add(txt标记孔径上限)
        TabPage展开图.Controls.Add(lbl工艺文字高)
        TabPage展开图.Controls.Add(cmb图号材质)
        TabPage展开图.Controls.Add(lbl图号材质)
        TabPage展开图.Controls.Add(lbl标记孔径上限)
        TabPage展开图.Controls.Add(chk展开图隐藏螺纹特征)
        TabPage展开图.Controls.Add(GroupBox展开图模板)
        TabPage展开图.Controls.Add(chk展开图标注)
        TabPage展开图.Controls.Add(GroupBox下)
        TabPage展开图.Controls.Add(GroupBox上)
        TabPage展开图.Location = New System.Drawing.Point(4, 34)
        TabPage展开图.Margin = New Padding(4, 5, 4, 5)
        TabPage展开图.Name = "TabPage展开图"
        TabPage展开图.Size = New Size(1032, 640)
        TabPage展开图.TabIndex = 3
        TabPage展开图.Text = "展开图"
        TabPage展开图.UseVisualStyleBackColor = True
        ' 
        ' chk保存展开图到指定文件夹
        ' 
        chk保存展开图到指定文件夹.AutoSize = True
        chk保存展开图到指定文件夹.Location = New System.Drawing.Point(503, 541)
        chk保存展开图到指定文件夹.Margin = New Padding(4, 5, 4, 5)
        chk保存展开图到指定文件夹.Name = "chk保存展开图到指定文件夹"
        chk保存展开图到指定文件夹.Size = New Size(247, 29)
        chk保存展开图到指定文件夹.TabIndex = 41
        chk保存展开图到指定文件夹.Text = "保存展开图到指定文件夹"
        chk保存展开图到指定文件夹.UseVisualStyleBackColor = True
        ' 
        ' txt工艺文字高
        ' 
        txt工艺文字高.Location = New System.Drawing.Point(629, 472)
        txt工艺文字高.Margin = New Padding(4, 5, 4, 5)
        txt工艺文字高.Name = "txt工艺文字高"
        txt工艺文字高.Size = New Size(57, 31)
        txt工艺文字高.TabIndex = 40
        txt工艺文字高.Text = "0"
        ' 
        ' txt标记孔径上限
        ' 
        txt标记孔径上限.Location = New System.Drawing.Point(327, 472)
        txt标记孔径上限.Margin = New Padding(4, 5, 4, 5)
        txt标记孔径上限.Name = "txt标记孔径上限"
        txt标记孔径上限.Size = New Size(57, 31)
        txt标记孔径上限.TabIndex = 31
        txt标记孔径上限.Text = "0"
        ' 
        ' lbl工艺文字高
        ' 
        lbl工艺文字高.AutoSize = True
        lbl工艺文字高.Location = New System.Drawing.Point(500, 480)
        lbl工艺文字高.Margin = New Padding(4, 0, 4, 0)
        lbl工艺文字高.Name = "lbl工艺文字高"
        lbl工艺文字高.Size = New Size(107, 25)
        lbl工艺文字高.TabIndex = 39
        lbl工艺文字高.Text = "工艺文字高"
        ' 
        ' cmb图号材质
        ' 
        cmb图号材质.DropDownStyle = ComboBoxStyle.DropDownList
        cmb图号材质.FormattingEnabled = True
        cmb图号材质.Items.AddRange(New Object() {"无材质", "材质前缀", "材质后缀"})
        cmb图号材质.Location = New System.Drawing.Point(327, 534)
        cmb图号材质.Margin = New Padding(4, 5, 4, 5)
        cmb图号材质.Name = "cmb图号材质"
        cmb图号材质.Size = New Size(140, 33)
        cmb图号材质.TabIndex = 34
        ' 
        ' lbl图号材质
        ' 
        lbl图号材质.AutoSize = True
        lbl图号材质.Location = New System.Drawing.Point(187, 541)
        lbl图号材质.Margin = New Padding(4, 0, 4, 0)
        lbl图号材质.Name = "lbl图号材质"
        lbl图号材质.Size = New Size(126, 25)
        lbl图号材质.TabIndex = 33
        lbl图号材质.Text = "图号添加材质"
        ' 
        ' lbl标记孔径上限
        ' 
        lbl标记孔径上限.AutoSize = True
        lbl标记孔径上限.Location = New System.Drawing.Point(187, 480)
        lbl标记孔径上限.Margin = New Padding(4, 0, 4, 0)
        lbl标记孔径上限.Name = "lbl标记孔径上限"
        lbl标记孔径上限.Size = New Size(126, 25)
        lbl标记孔径上限.TabIndex = 30
        lbl标记孔径上限.Text = "标记孔径上限"
        ' 
        ' chk展开图隐藏螺纹特征
        ' 
        chk展开图隐藏螺纹特征.AutoSize = True
        chk展开图隐藏螺纹特征.Location = New System.Drawing.Point(20, 538)
        chk展开图隐藏螺纹特征.Margin = New Padding(4, 5, 4, 5)
        chk展开图隐藏螺纹特征.Name = "chk展开图隐藏螺纹特征"
        chk展开图隐藏螺纹特征.Size = New Size(114, 29)
        chk展开图隐藏螺纹特征.TabIndex = 26
        chk展开图隐藏螺纹特征.Text = "隐藏螺纹"
        chk展开图隐藏螺纹特征.UseVisualStyleBackColor = True
        ' 
        ' GroupBox展开图模板
        ' 
        GroupBox展开图模板.Controls.Add(btn展开图模板)
        GroupBox展开图模板.Controls.Add(txt展开图模板)
        GroupBox展开图模板.Controls.Add(lbl展开图模板)
        GroupBox展开图模板.Location = New System.Drawing.Point(18, 29)
        GroupBox展开图模板.Margin = New Padding(4, 5, 4, 5)
        GroupBox展开图模板.Name = "GroupBox展开图模板"
        GroupBox展开图模板.Padding = New Padding(4, 5, 4, 5)
        GroupBox展开图模板.Size = New Size(990, 121)
        GroupBox展开图模板.TabIndex = 25
        GroupBox展开图模板.TabStop = False
        GroupBox展开图模板.Text = "展开图模板"
        ' 
        ' btn展开图模板
        ' 
        btn展开图模板.Location = New System.Drawing.Point(917, 36)
        btn展开图模板.Margin = New Padding(4, 5, 4, 5)
        btn展开图模板.Name = "btn展开图模板"
        btn展开图模板.Size = New Size(42, 52)
        btn展开图模板.TabIndex = 0
        btn展开图模板.UseVisualStyleBackColor = True
        ' 
        ' txt展开图模板
        ' 
        txt展开图模板.Location = New System.Drawing.Point(133, 39)
        txt展开图模板.Margin = New Padding(4, 5, 4, 5)
        txt展开图模板.Name = "txt展开图模板"
        txt展开图模板.ReadOnly = True
        txt展开图模板.Size = New Size(764, 31)
        txt展开图模板.TabIndex = 8
        ' 
        ' lbl展开图模板
        ' 
        lbl展开图模板.AutoSize = True
        lbl展开图模板.Location = New System.Drawing.Point(11, 48)
        lbl展开图模板.Margin = New Padding(4, 0, 4, 0)
        lbl展开图模板.Name = "lbl展开图模板"
        lbl展开图模板.Size = New Size(126, 25)
        lbl展开图模板.TabIndex = 7
        lbl展开图模板.Text = "展开图模板："
        ' 
        ' chk展开图标注
        ' 
        chk展开图标注.AutoSize = True
        chk展开图标注.Location = New System.Drawing.Point(20, 478)
        chk展开图标注.Margin = New Padding(4, 5, 4, 5)
        chk展开图标注.Name = "chk展开图标注"
        chk展开图标注.Size = New Size(133, 29)
        chk展开图标注.TabIndex = 0
        chk展开图标注.Text = "展开图标注"
        chk展开图标注.UseVisualStyleBackColor = True
        ' 
        ' GroupBox下
        ' 
        GroupBox下.Controls.Add(cmb向下线宽)
        GroupBox下.Controls.Add(cmb向下线型)
        GroupBox下.Controls.Add(lbl颜色下)
        GroupBox下.Controls.Add(btn向下颜色)
        GroupBox下.Controls.Add(lbl线宽下)
        GroupBox下.Controls.Add(lbl线型下)
        GroupBox下.Location = New System.Drawing.Point(463, 166)
        GroupBox下.Margin = New Padding(4, 5, 4, 5)
        GroupBox下.Name = "GroupBox下"
        GroupBox下.Padding = New Padding(4, 5, 4, 5)
        GroupBox下.Size = New Size(390, 271)
        GroupBox下.TabIndex = 1
        GroupBox下.TabStop = False
        GroupBox下.Text = "下"
        ' 
        ' cmb向下线宽
        ' 
        cmb向下线宽.DropDownStyle = ComboBoxStyle.DropDownList
        cmb向下线宽.FormattingEnabled = True
        cmb向下线宽.Items.AddRange(New Object() {"0.18", "0.25", "0.35", "0.5", "0.7", "1", "1.4", "2"})
        cmb向下线宽.Location = New System.Drawing.Point(107, 109)
        cmb向下线宽.Margin = New Padding(4, 5, 4, 5)
        cmb向下线宽.Name = "cmb向下线宽"
        cmb向下线宽.Size = New Size(231, 33)
        cmb向下线宽.TabIndex = 1
        ' 
        ' cmb向下线型
        ' 
        cmb向下线型.DropDownStyle = ComboBoxStyle.DropDownList
        cmb向下线型.FormattingEnabled = True
        cmb向下线型.Items.AddRange(New Object() {"实线", "虚线", "点长划线", "双点长划线", "三点长划线"})
        cmb向下线型.Location = New System.Drawing.Point(107, 39)
        cmb向下线型.Margin = New Padding(4, 5, 4, 5)
        cmb向下线型.Name = "cmb向下线型"
        cmb向下线型.Size = New Size(231, 33)
        cmb向下线型.TabIndex = 0
        ' 
        ' lbl颜色下
        ' 
        lbl颜色下.AutoSize = True
        lbl颜色下.Location = New System.Drawing.Point(27, 188)
        lbl颜色下.Margin = New Padding(4, 0, 4, 0)
        lbl颜色下.Name = "lbl颜色下"
        lbl颜色下.Size = New Size(50, 25)
        lbl颜色下.TabIndex = 3
        lbl颜色下.Text = "颜色"
        ' 
        ' btn向下颜色
        ' 
        btn向下颜色.Location = New System.Drawing.Point(109, 171)
        btn向下颜色.Margin = New Padding(4, 5, 4, 5)
        btn向下颜色.Name = "btn向下颜色"
        btn向下颜色.Size = New Size(63, 54)
        btn向下颜色.TabIndex = 2
        btn向下颜色.UseVisualStyleBackColor = True
        ' 
        ' lbl线宽下
        ' 
        lbl线宽下.AutoSize = True
        lbl线宽下.Location = New System.Drawing.Point(27, 121)
        lbl线宽下.Margin = New Padding(4, 0, 4, 0)
        lbl线宽下.Name = "lbl线宽下"
        lbl线宽下.Size = New Size(50, 25)
        lbl线宽下.TabIndex = 1
        lbl线宽下.Text = "线宽"
        ' 
        ' lbl线型下
        ' 
        lbl线型下.AutoSize = True
        lbl线型下.Location = New System.Drawing.Point(27, 54)
        lbl线型下.Margin = New Padding(4, 0, 4, 0)
        lbl线型下.Name = "lbl线型下"
        lbl线型下.Size = New Size(50, 25)
        lbl线型下.TabIndex = 0
        lbl线型下.Text = "线型"
        ' 
        ' GroupBox上
        ' 
        GroupBox上.Controls.Add(cmb向上线宽)
        GroupBox上.Controls.Add(cmb向上线型)
        GroupBox上.Controls.Add(lbl颜色上)
        GroupBox上.Controls.Add(btn向上颜色)
        GroupBox上.Controls.Add(lbl线宽上)
        GroupBox上.Controls.Add(lbl线型上)
        GroupBox上.Location = New System.Drawing.Point(18, 166)
        GroupBox上.Margin = New Padding(4, 5, 4, 5)
        GroupBox上.Name = "GroupBox上"
        GroupBox上.Padding = New Padding(4, 5, 4, 5)
        GroupBox上.Size = New Size(390, 271)
        GroupBox上.TabIndex = 0
        GroupBox上.TabStop = False
        GroupBox上.Text = "上"
        ' 
        ' cmb向上线宽
        ' 
        cmb向上线宽.DropDownStyle = ComboBoxStyle.DropDownList
        cmb向上线宽.FormattingEnabled = True
        cmb向上线宽.Items.AddRange(New Object() {"0.18", "0.25", "0.35", "0.5", "0.7", "1", "1.4", "2"})
        cmb向上线宽.Location = New System.Drawing.Point(109, 112)
        cmb向上线宽.Margin = New Padding(4, 5, 4, 5)
        cmb向上线宽.Name = "cmb向上线宽"
        cmb向上线宽.Size = New Size(231, 33)
        cmb向上线宽.TabIndex = 1
        ' 
        ' cmb向上线型
        ' 
        cmb向上线型.DropDownStyle = ComboBoxStyle.DropDownList
        cmb向上线型.FormattingEnabled = True
        cmb向上线型.Items.AddRange(New Object() {"实线", "虚线", "点长划线", "双点长划线", "三点长划线"})
        cmb向上线型.Location = New System.Drawing.Point(109, 46)
        cmb向上线型.Margin = New Padding(4, 5, 4, 5)
        cmb向上线型.Name = "cmb向上线型"
        cmb向上线型.Size = New Size(231, 33)
        cmb向上线型.TabIndex = 0
        ' 
        ' lbl颜色上
        ' 
        lbl颜色上.AutoSize = True
        lbl颜色上.Location = New System.Drawing.Point(27, 188)
        lbl颜色上.Margin = New Padding(4, 0, 4, 0)
        lbl颜色上.Name = "lbl颜色上"
        lbl颜色上.Size = New Size(50, 25)
        lbl颜色上.TabIndex = 3
        lbl颜色上.Text = "颜色"
        ' 
        ' btn向上颜色
        ' 
        btn向上颜色.Location = New System.Drawing.Point(109, 172)
        btn向上颜色.Margin = New Padding(4, 5, 4, 5)
        btn向上颜色.Name = "btn向上颜色"
        btn向上颜色.Size = New Size(63, 54)
        btn向上颜色.TabIndex = 2
        btn向上颜色.UseVisualStyleBackColor = True
        ' 
        ' lbl线宽上
        ' 
        lbl线宽上.AutoSize = True
        lbl线宽上.Location = New System.Drawing.Point(27, 121)
        lbl线宽上.Margin = New Padding(4, 0, 4, 0)
        lbl线宽上.Name = "lbl线宽上"
        lbl线宽上.Size = New Size(50, 25)
        lbl线宽上.TabIndex = 1
        lbl线宽上.Text = "线宽"
        ' 
        ' lbl线型上
        ' 
        lbl线型上.AutoSize = True
        lbl线型上.Location = New System.Drawing.Point(27, 54)
        lbl线型上.Margin = New Padding(4, 0, 4, 0)
        lbl线型上.Name = "lbl线型上"
        lbl线型上.Size = New Size(50, 25)
        lbl线型上.TabIndex = 0
        lbl线型上.Text = "线型"
        ' 
        ' TabPage工程图
        ' 
        TabPage工程图.Controls.Add(chk强制横向)
        TabPage工程图.Controls.Add(chk逆时针序号)
        TabPage工程图.Controls.Add(chk模型匹配检查)
        TabPage工程图.Controls.Add(GroupBox标题栏)
        TabPage工程图.Controls.Add(GroupBox页边距)
        TabPage工程图.Controls.Add(GroupBox视图)
        TabPage工程图.Controls.Add(GroupBox工程图模板)
        TabPage工程图.Location = New System.Drawing.Point(4, 34)
        TabPage工程图.Margin = New Padding(4, 5, 4, 5)
        TabPage工程图.Name = "TabPage工程图"
        TabPage工程图.Size = New Size(1032, 640)
        TabPage工程图.TabIndex = 2
        TabPage工程图.Text = "工程图"
        TabPage工程图.UseVisualStyleBackColor = True
        ' 
        ' chk强制横向
        ' 
        chk强制横向.Anchor = AnchorStyles.None
        chk强制横向.AutoSize = True
        chk强制横向.Location = New System.Drawing.Point(687, 538)
        chk强制横向.Margin = New Padding(4, 5, 4, 5)
        chk强制横向.Name = "chk强制横向"
        chk强制横向.Size = New Size(114, 29)
        chk强制横向.TabIndex = 35
        chk强制横向.Text = "强制横向"
        chk强制横向.UseVisualStyleBackColor = True
        ' 
        ' chk逆时针序号
        ' 
        chk逆时针序号.AutoSize = True
        chk逆时针序号.Location = New System.Drawing.Point(863, 471)
        chk逆时针序号.Margin = New Padding(4, 5, 4, 5)
        chk逆时针序号.Name = "chk逆时针序号"
        chk逆时针序号.Size = New Size(133, 29)
        chk逆时针序号.TabIndex = 34
        chk逆时针序号.Text = "逆时针序号"
        chk逆时针序号.UseVisualStyleBackColor = True
        ' 
        ' chk模型匹配检查
        ' 
        chk模型匹配检查.Anchor = AnchorStyles.None
        chk模型匹配检查.AutoSize = True
        chk模型匹配检查.Location = New System.Drawing.Point(687, 471)
        chk模型匹配检查.Margin = New Padding(4, 5, 4, 5)
        chk模型匹配检查.Name = "chk模型匹配检查"
        chk模型匹配检查.Size = New Size(152, 29)
        chk模型匹配检查.TabIndex = 33
        chk模型匹配检查.Text = "模型匹配检查"
        chk模型匹配检查.UseVisualStyleBackColor = True
        ' 
        ' GroupBox标题栏
        ' 
        GroupBox标题栏.Controls.Add(Label3)
        GroupBox标题栏.Controls.Add(cbx增加后缀)
        GroupBox标题栏.Controls.Add(txt零件图框)
        GroupBox标题栏.Controls.Add(txt部件图框)
        GroupBox标题栏.Controls.Add(lbl零件图框)
        GroupBox标题栏.Controls.Add(lbl部件图框)
        GroupBox标题栏.Location = New System.Drawing.Point(717, 166)
        GroupBox标题栏.Margin = New Padding(4, 5, 4, 5)
        GroupBox标题栏.Name = "GroupBox标题栏"
        GroupBox标题栏.Padding = New Padding(4, 5, 4, 5)
        GroupBox标题栏.Size = New Size(284, 255)
        GroupBox标题栏.TabIndex = 32
        GroupBox标题栏.TabStop = False
        GroupBox标题栏.Text = "标题栏"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(10, 198)
        Label3.Name = "Label3"
        Label3.Size = New Size(149, 25)
        Label3.TabIndex = 46
        Label3.Text = "另存DWG后缀："
        ' 
        ' cbx增加后缀
        ' 
        cbx增加后缀.FormattingEnabled = True
        cbx增加后缀.Items.AddRange(New Object() {"-AutoCAD", "-CAD", "-模型"})
        cbx增加后缀.Location = New System.Drawing.Point(148, 192)
        cbx增加后缀.Margin = New Padding(3, 4, 3, 4)
        cbx增加后缀.Name = "cbx增加后缀"
        cbx增加后缀.Size = New Size(126, 33)
        cbx增加后缀.TabIndex = 45
        ' 
        ' txt零件图框
        ' 
        txt零件图框.Location = New System.Drawing.Point(78, 114)
        txt零件图框.Margin = New Padding(4, 5, 4, 5)
        txt零件图框.Name = "txt零件图框"
        txt零件图框.Size = New Size(186, 31)
        txt零件图框.TabIndex = 1
        ' 
        ' txt部件图框
        ' 
        txt部件图框.Location = New System.Drawing.Point(78, 46)
        txt部件图框.Margin = New Padding(4, 5, 4, 5)
        txt部件图框.Name = "txt部件图框"
        txt部件图框.Size = New Size(186, 31)
        txt部件图框.TabIndex = 0
        ' 
        ' lbl零件图框
        ' 
        lbl零件图框.AutoSize = True
        lbl零件图框.Location = New System.Drawing.Point(10, 114)
        lbl零件图框.Margin = New Padding(4, 0, 4, 0)
        lbl零件图框.Name = "lbl零件图框"
        lbl零件图框.Size = New Size(69, 25)
        lbl零件图框.TabIndex = 9
        lbl零件图框.Text = "零件："
        ' 
        ' lbl部件图框
        ' 
        lbl部件图框.AutoSize = True
        lbl部件图框.Location = New System.Drawing.Point(10, 52)
        lbl部件图框.Margin = New Padding(4, 0, 4, 0)
        lbl部件图框.Name = "lbl部件图框"
        lbl部件图框.Size = New Size(69, 25)
        lbl部件图框.TabIndex = 8
        lbl部件图框.Text = "部件："
        ' 
        ' GroupBox页边距
        ' 
        GroupBox页边距.Controls.Add(NumericUpDown页边距右)
        GroupBox页边距.Controls.Add(NumericUpDown页边距左)
        GroupBox页边距.Controls.Add(NumericUpDown页边距下)
        GroupBox页边距.Controls.Add(NumericUpDown页边距上)
        GroupBox页边距.Controls.Add(lb页边距)
        GroupBox页边距.Location = New System.Drawing.Point(22, 445)
        GroupBox页边距.Margin = New Padding(4, 5, 4, 5)
        GroupBox页边距.Name = "GroupBox页边距"
        GroupBox页边距.Padding = New Padding(4, 5, 4, 5)
        GroupBox页边距.Size = New Size(651, 120)
        GroupBox页边距.TabIndex = 31
        GroupBox页边距.TabStop = False
        GroupBox页边距.Text = "页边距"
        ' 
        ' NumericUpDown页边距右
        ' 
        NumericUpDown页边距右.Location = New System.Drawing.Point(550, 48)
        NumericUpDown页边距右.Margin = New Padding(4, 5, 4, 5)
        NumericUpDown页边距右.Name = "NumericUpDown页边距右"
        NumericUpDown页边距右.Size = New Size(82, 31)
        NumericUpDown页边距右.TabIndex = 3
        NumericUpDown页边距右.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' NumericUpDown页边距左
        ' 
        NumericUpDown页边距左.Location = New System.Drawing.Point(387, 48)
        NumericUpDown页边距左.Margin = New Padding(4, 5, 4, 5)
        NumericUpDown页边距左.Name = "NumericUpDown页边距左"
        NumericUpDown页边距左.Size = New Size(82, 31)
        NumericUpDown页边距左.TabIndex = 2
        NumericUpDown页边距左.Value = New Decimal(New Integer() {30, 0, 0, 0})
        ' 
        ' NumericUpDown页边距下
        ' 
        NumericUpDown页边距下.Location = New System.Drawing.Point(223, 48)
        NumericUpDown页边距下.Margin = New Padding(4, 5, 4, 5)
        NumericUpDown页边距下.Name = "NumericUpDown页边距下"
        NumericUpDown页边距下.Size = New Size(82, 31)
        NumericUpDown页边距下.TabIndex = 1
        NumericUpDown页边距下.Value = New Decimal(New Integer() {35, 0, 0, 0})
        ' 
        ' NumericUpDown页边距上
        ' 
        NumericUpDown页边距上.Location = New System.Drawing.Point(60, 48)
        NumericUpDown页边距上.Margin = New Padding(4, 5, 4, 5)
        NumericUpDown页边距上.Name = "NumericUpDown页边距上"
        NumericUpDown页边距上.Size = New Size(82, 31)
        NumericUpDown页边距上.TabIndex = 0
        NumericUpDown页边距上.Value = New Decimal(New Integer() {20, 0, 0, 0})
        ' 
        ' lb页边距
        ' 
        lb页边距.AutoSize = True
        lb页边距.Location = New System.Drawing.Point(17, 55)
        lb页边距.Margin = New Padding(4, 0, 4, 0)
        lb页边距.Name = "lb页边距"
        lb页边距.Size = New Size(303, 25)
        lb页边距.TabIndex = 4
        lb页边距.Text = "上               下              左              右"
        ' 
        ' GroupBox视图
        ' 
        GroupBox视图.Controls.Add(chk标注尺寸)
        GroupBox视图.Controls.Add(lbl前视图)
        GroupBox视图.Controls.Add(chk左视图)
        GroupBox视图.Controls.Add(chk俯视图)
        GroupBox视图.Controls.Add(chk右视图)
        GroupBox视图.Controls.Add(chk仰视图)
        GroupBox视图.Controls.Add(chk工程图螺纹特征)
        GroupBox视图.Controls.Add(rdo显示隐藏线)
        GroupBox视图.Controls.Add(rdo不显示隐藏线)
        GroupBox视图.Controls.Add(chk相切边)
        GroupBox视图.Controls.Add(chk钣金自动展开)
        GroupBox视图.Controls.Add(chk第三视角)
        GroupBox视图.Location = New System.Drawing.Point(18, 166)
        GroupBox视图.Margin = New Padding(4, 5, 4, 5)
        GroupBox视图.Name = "GroupBox视图"
        GroupBox视图.Padding = New Padding(4, 5, 4, 5)
        GroupBox视图.Size = New Size(689, 255)
        GroupBox视图.TabIndex = 30
        GroupBox视图.TabStop = False
        GroupBox视图.Text = "视图"
        ' 
        ' chk标注尺寸
        ' 
        chk标注尺寸.AutoSize = True
        chk标注尺寸.Location = New System.Drawing.Point(322, 50)
        chk标注尺寸.Margin = New Padding(4, 5, 4, 5)
        chk标注尺寸.Name = "chk标注尺寸"
        chk标注尺寸.Size = New Size(114, 29)
        chk标注尺寸.TabIndex = 2
        chk标注尺寸.Text = "标注尺寸"
        chk标注尺寸.UseVisualStyleBackColor = True
        ' 
        ' lbl前视图
        ' 
        lbl前视图.AutoSize = True
        lbl前视图.Location = New System.Drawing.Point(493, 122)
        lbl前视图.Margin = New Padding(4, 0, 4, 0)
        lbl前视图.Name = "lbl前视图"
        lbl前视图.Size = New Size(69, 25)
        lbl前视图.TabIndex = 43
        lbl前视图.Text = "前视图"
        ' 
        ' chk左视图
        ' 
        chk左视图.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chk左视图.AutoSize = True
        chk左视图.Location = New System.Drawing.Point(383, 120)
        chk左视图.Margin = New Padding(4, 5, 4, 5)
        chk左视图.Name = "chk左视图"
        chk左视图.RightToLeft = RightToLeft.Yes
        chk左视图.Size = New Size(95, 29)
        chk左视图.TabIndex = 8
        chk左视图.Text = "左视图"
        chk左视图.UseVisualStyleBackColor = True
        ' 
        ' chk俯视图
        ' 
        chk俯视图.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chk俯视图.AutoSize = True
        chk俯视图.Location = New System.Drawing.Point(488, 198)
        chk俯视图.Margin = New Padding(4, 5, 4, 5)
        chk俯视图.Name = "chk俯视图"
        chk俯视图.Size = New Size(95, 29)
        chk俯视图.TabIndex = 9
        chk俯视图.Text = "俯视图"
        chk俯视图.UseVisualStyleBackColor = True
        ' 
        ' chk右视图
        ' 
        chk右视图.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chk右视图.AutoSize = True
        chk右视图.Location = New System.Drawing.Point(577, 120)
        chk右视图.Margin = New Padding(4, 5, 4, 5)
        chk右视图.Name = "chk右视图"
        chk右视图.Size = New Size(95, 29)
        chk右视图.TabIndex = 10
        chk右视图.Text = "右视图"
        chk右视图.UseVisualStyleBackColor = True
        ' 
        ' chk仰视图
        ' 
        chk仰视图.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chk仰视图.AutoSize = True
        chk仰视图.Location = New System.Drawing.Point(488, 50)
        chk仰视图.Margin = New Padding(4, 5, 4, 5)
        chk仰视图.Name = "chk仰视图"
        chk仰视图.Size = New Size(95, 29)
        chk仰视图.TabIndex = 7
        chk仰视图.Text = "仰视图"
        chk仰视图.UseVisualStyleBackColor = True
        ' 
        ' chk工程图螺纹特征
        ' 
        chk工程图螺纹特征.AutoSize = True
        chk工程图螺纹特征.Location = New System.Drawing.Point(184, 112)
        chk工程图螺纹特征.Margin = New Padding(4, 5, 4, 5)
        chk工程图螺纹特征.Name = "chk工程图螺纹特征"
        chk工程图螺纹特征.Size = New Size(114, 29)
        chk工程图螺纹特征.TabIndex = 6
        chk工程图螺纹特征.Text = "螺纹特征"
        chk工程图螺纹特征.UseVisualStyleBackColor = True
        ' 
        ' rdo显示隐藏线
        ' 
        rdo显示隐藏线.AutoSize = True
        rdo显示隐藏线.Location = New System.Drawing.Point(184, 179)
        rdo显示隐藏线.Margin = New Padding(4, 5, 4, 5)
        rdo显示隐藏线.Name = "rdo显示隐藏线"
        rdo显示隐藏线.Size = New Size(132, 29)
        rdo显示隐藏线.TabIndex = 5
        rdo显示隐藏线.TabStop = True
        rdo显示隐藏线.Text = "显示隐藏线"
        rdo显示隐藏线.UseVisualStyleBackColor = True
        ' 
        ' rdo不显示隐藏线
        ' 
        rdo不显示隐藏线.AutoSize = True
        rdo不显示隐藏线.Location = New System.Drawing.Point(17, 179)
        rdo不显示隐藏线.Margin = New Padding(4, 5, 4, 5)
        rdo不显示隐藏线.Name = "rdo不显示隐藏线"
        rdo不显示隐藏线.Size = New Size(151, 29)
        rdo不显示隐藏线.TabIndex = 4
        rdo不显示隐藏线.TabStop = True
        rdo不显示隐藏线.Text = "不显示隐藏线"
        rdo不显示隐藏线.UseVisualStyleBackColor = True
        ' 
        ' chk相切边
        ' 
        chk相切边.AutoSize = True
        chk相切边.Location = New System.Drawing.Point(184, 50)
        chk相切边.Margin = New Padding(4, 5, 4, 5)
        chk相切边.Name = "chk相切边"
        chk相切边.Size = New Size(95, 29)
        chk相切边.TabIndex = 1
        chk相切边.Text = "相切边"
        chk相切边.UseVisualStyleBackColor = True
        ' 
        ' chk钣金自动展开
        ' 
        chk钣金自动展开.AutoSize = True
        chk钣金自动展开.Location = New System.Drawing.Point(16, 50)
        chk钣金自动展开.Margin = New Padding(4, 5, 4, 5)
        chk钣金自动展开.Name = "chk钣金自动展开"
        chk钣金自动展开.Size = New Size(152, 29)
        chk钣金自动展开.TabIndex = 0
        chk钣金自动展开.Text = "钣金自动展开"
        chk钣金自动展开.UseVisualStyleBackColor = True
        ' 
        ' chk第三视角
        ' 
        chk第三视角.AutoSize = True
        chk第三视角.Location = New System.Drawing.Point(16, 112)
        chk第三视角.Margin = New Padding(4, 5, 4, 5)
        chk第三视角.Name = "chk第三视角"
        chk第三视角.Size = New Size(114, 29)
        chk第三视角.TabIndex = 3
        chk第三视角.Text = "第三视角"
        chk第三视角.UseVisualStyleBackColor = True
        ' 
        ' GroupBox工程图模板
        ' 
        GroupBox工程图模板.Controls.Add(btn选择工程图模板)
        GroupBox工程图模板.Controls.Add(txt工程图模板)
        GroupBox工程图模板.Controls.Add(lbl模板工程图)
        GroupBox工程图模板.Location = New System.Drawing.Point(18, 29)
        GroupBox工程图模板.Margin = New Padding(4, 5, 4, 5)
        GroupBox工程图模板.Name = "GroupBox工程图模板"
        GroupBox工程图模板.Padding = New Padding(4, 5, 4, 5)
        GroupBox工程图模板.Size = New Size(990, 121)
        GroupBox工程图模板.TabIndex = 24
        GroupBox工程图模板.TabStop = False
        GroupBox工程图模板.Text = "工程图模板"
        ' 
        ' btn选择工程图模板
        ' 
        btn选择工程图模板.Location = New System.Drawing.Point(917, 36)
        btn选择工程图模板.Margin = New Padding(4, 5, 4, 5)
        btn选择工程图模板.Name = "btn选择工程图模板"
        btn选择工程图模板.Size = New Size(42, 52)
        btn选择工程图模板.TabIndex = 0
        btn选择工程图模板.UseVisualStyleBackColor = True
        ' 
        ' txt工程图模板
        ' 
        txt工程图模板.Location = New System.Drawing.Point(133, 39)
        txt工程图模板.Margin = New Padding(4, 5, 4, 5)
        txt工程图模板.Name = "txt工程图模板"
        txt工程图模板.ReadOnly = True
        txt工程图模板.Size = New Size(764, 31)
        txt工程图模板.TabIndex = 8
        txt工程图模板.TabStop = False
        ' 
        ' lbl模板工程图
        ' 
        lbl模板工程图.AutoSize = True
        lbl模板工程图.Location = New System.Drawing.Point(11, 48)
        lbl模板工程图.Margin = New Padding(4, 0, 4, 0)
        lbl模板工程图.Name = "lbl模板工程图"
        lbl模板工程图.Size = New Size(126, 25)
        lbl模板工程图.TabIndex = 7
        lbl模板工程图.Text = "工程图模板："
        ' 
        ' TabPage模型
        ' 
        TabPage模型.Controls.Add(Btn添加2)
        TabPage模型.Controls.Add(txtBOM显示列)
        TabPage模型.Controls.Add(txt钣金厚度前缀)
        TabPage模型.Controls.Add(Label5)
        TabPage模型.Controls.Add(txt去除后缀)
        TabPage模型.Controls.Add(chk钣金厚度检查)
        TabPage模型.Controls.Add(lbl去除后缀)
        TabPage模型.Controls.Add(chk检查重复图号)
        TabPage模型.Controls.Add(lbl查找文件夹层数)
        TabPage模型.Controls.Add(NUD查找文件夹层数)
        TabPage模型.Controls.Add(chk另存到子文件夹)
        TabPage模型.Controls.Add(chk备份工程图)
        TabPage模型.Controls.Add(GroupBoxERP查询)
        TabPage模型.Controls.Add(GroupBoxBOM导出项目)
        TabPage模型.Location = New System.Drawing.Point(4, 34)
        TabPage模型.Margin = New Padding(4, 5, 4, 5)
        TabPage模型.Name = "TabPage模型"
        TabPage模型.Padding = New Padding(4, 5, 4, 5)
        TabPage模型.Size = New Size(1032, 640)
        TabPage模型.TabIndex = 1
        TabPage模型.Text = "模型"
        TabPage模型.UseVisualStyleBackColor = True
        ' 
        ' txtBOM显示列
        ' 
        txtBOM显示列.Location = New System.Drawing.Point(151, 594)
        txtBOM显示列.Margin = New Padding(4, 5, 4, 5)
        txtBOM显示列.Name = "txtBOM显示列"
        txtBOM显示列.Size = New Size(686, 31)
        txtBOM显示列.TabIndex = 6
        txtBOM显示列.Text = "项目序号|库存编号|成本中心|零件代号|材料|质量|所属装配代号|数量|总数量|描述"
        ' 
        ' txt钣金厚度前缀
        ' 
        txt钣金厚度前缀.Location = New System.Drawing.Point(418, 534)
        txt钣金厚度前缀.Margin = New Padding(4, 5, 4, 5)
        txt钣金厚度前缀.Name = "txt钣金厚度前缀"
        txt钣金厚度前缀.Size = New Size(161, 31)
        txt钣金厚度前缀.TabIndex = 38
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(13, 594)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(128, 25)
        Label5.TabIndex = 7
        Label5.Text = "BOM显示列："
        ' 
        ' txt去除后缀
        ' 
        txt去除后缀.Location = New System.Drawing.Point(349, 475)
        txt去除后缀.Margin = New Padding(4, 5, 4, 5)
        txt去除后缀.Name = "txt去除后缀"
        txt去除后缀.Size = New Size(187, 31)
        txt去除后缀.TabIndex = 29
        txt去除后缀.Text = "_MIR;_镜像"
        ' 
        ' chk钣金厚度检查
        ' 
        chk钣金厚度检查.Anchor = AnchorStyles.None
        chk钣金厚度检查.AutoSize = True
        chk钣金厚度检查.Checked = True
        chk钣金厚度检查.CheckState = CheckState.Checked
        chk钣金厚度检查.Location = New System.Drawing.Point(256, 546)
        chk钣金厚度检查.Margin = New Padding(4, 5, 4, 5)
        chk钣金厚度检查.Name = "chk钣金厚度检查"
        chk钣金厚度检查.Size = New Size(152, 29)
        chk钣金厚度检查.TabIndex = 37
        chk钣金厚度检查.Text = "钣金厚度检查"
        chk钣金厚度检查.UseVisualStyleBackColor = True
        ' 
        ' lbl去除后缀
        ' 
        lbl去除后缀.AutoSize = True
        lbl去除后缀.Location = New System.Drawing.Point(253, 484)
        lbl去除后缀.Margin = New Padding(4, 0, 4, 0)
        lbl去除后缀.Name = "lbl去除后缀"
        lbl去除后缀.Size = New Size(88, 25)
        lbl去除后缀.TabIndex = 28
        lbl去除后缀.Text = "去除后缀"
        ' 
        ' chk检查重复图号
        ' 
        chk检查重复图号.AutoSize = True
        chk检查重复图号.Location = New System.Drawing.Point(27, 534)
        chk检查重复图号.Margin = New Padding(4, 5, 4, 5)
        chk检查重复图号.Name = "chk检查重复图号"
        chk检查重复图号.Size = New Size(152, 29)
        chk检查重复图号.TabIndex = 27
        chk检查重复图号.Text = "检查重复图号"
        chk检查重复图号.UseVisualStyleBackColor = True
        ' 
        ' lbl查找文件夹层数
        ' 
        lbl查找文件夹层数.AutoSize = True
        lbl查找文件夹层数.Location = New System.Drawing.Point(251, 428)
        lbl查找文件夹层数.Margin = New Padding(4, 0, 4, 0)
        lbl查找文件夹层数.Name = "lbl查找文件夹层数"
        lbl查找文件夹层数.Size = New Size(145, 25)
        lbl查找文件夹层数.TabIndex = 26
        lbl查找文件夹层数.Text = "查找文件夹层数"
        ' 
        ' NUD查找文件夹层数
        ' 
        NUD查找文件夹层数.Location = New System.Drawing.Point(403, 420)
        NUD查找文件夹层数.Margin = New Padding(4, 5, 4, 5)
        NUD查找文件夹层数.Name = "NUD查找文件夹层数"
        NUD查找文件夹层数.Size = New Size(51, 31)
        NUD查找文件夹层数.TabIndex = 25
        NUD查找文件夹层数.TextAlign = HorizontalAlignment.Center
        NUD查找文件夹层数.Value = New Decimal(New Integer() {2, 0, 0, 0})
        ' 
        ' chk另存到子文件夹
        ' 
        chk另存到子文件夹.AutoSize = True
        chk另存到子文件夹.Location = New System.Drawing.Point(27, 479)
        chk另存到子文件夹.Margin = New Padding(4, 5, 4, 5)
        chk另存到子文件夹.Name = "chk另存到子文件夹"
        chk另存到子文件夹.Size = New Size(171, 29)
        chk另存到子文件夹.TabIndex = 24
        chk另存到子文件夹.Text = "另存到子文件夹"
        chk另存到子文件夹.UseVisualStyleBackColor = True
        ' 
        ' chk备份工程图
        ' 
        chk备份工程图.AutoSize = True
        chk备份工程图.Location = New System.Drawing.Point(27, 425)
        chk备份工程图.Margin = New Padding(4, 5, 4, 5)
        chk备份工程图.Name = "chk备份工程图"
        chk备份工程图.Size = New Size(133, 29)
        chk备份工程图.TabIndex = 23
        chk备份工程图.Text = "备份工程图"
        chk备份工程图.UseVisualStyleBackColor = True
        ' 
        ' GroupBoxERP查询
        ' 
        GroupBoxERP查询.Controls.Add(btn选择erp数据库)
        GroupBoxERP查询.Controls.Add(btn更新数据库)
        GroupBoxERP查询.Controls.Add(btn打开erp数据库)
        GroupBoxERP查询.Controls.Add(txt查询列)
        GroupBoxERP查询.Controls.Add(lbl编码列)
        GroupBoxERP查询.Controls.Add(txt查找范围)
        GroupBoxERP查询.Controls.Add(lbl查询列)
        GroupBoxERP查询.Controls.Add(txt基础数据文件)
        GroupBoxERP查询.Controls.Add(lbl基础数据文件)
        GroupBoxERP查询.Location = New System.Drawing.Point(10, 225)
        GroupBoxERP查询.Margin = New Padding(4, 5, 4, 5)
        GroupBoxERP查询.Name = "GroupBoxERP查询"
        GroupBoxERP查询.Padding = New Padding(4, 5, 4, 5)
        GroupBoxERP查询.Size = New Size(990, 175)
        GroupBoxERP查询.TabIndex = 22
        GroupBoxERP查询.TabStop = False
        GroupBoxERP查询.Text = "ERP查询"
        ' 
        ' btn选择erp数据库
        ' 
        btn选择erp数据库.Location = New System.Drawing.Point(863, 34)
        btn选择erp数据库.Margin = New Padding(4, 5, 4, 5)
        btn选择erp数据库.Name = "btn选择erp数据库"
        btn选择erp数据库.Size = New Size(43, 54)
        btn选择erp数据库.TabIndex = 0
        btn选择erp数据库.UseVisualStyleBackColor = True
        ' 
        ' btn更新数据库
        ' 
        btn更新数据库.Location = New System.Drawing.Point(678, 105)
        btn更新数据库.Margin = New Padding(4, 5, 4, 5)
        btn更新数据库.Name = "btn更新数据库"
        btn更新数据库.Size = New Size(137, 52)
        btn更新数据库.TabIndex = 5
        btn更新数据库.Text = "更新数据库"
        btn更新数据库.UseVisualStyleBackColor = True
        ' 
        ' btn打开erp数据库
        ' 
        btn打开erp数据库.Location = New System.Drawing.Point(837, 105)
        btn打开erp数据库.Margin = New Padding(4, 5, 4, 5)
        btn打开erp数据库.Name = "btn打开erp数据库"
        btn打开erp数据库.Size = New Size(137, 52)
        btn打开erp数据库.TabIndex = 6
        btn打开erp数据库.Text = "打开数据库"
        btn打开erp数据库.UseVisualStyleBackColor = True
        ' 
        ' txt查询列
        ' 
        txt查询列.Location = New System.Drawing.Point(403, 111)
        txt查询列.Margin = New Padding(4, 5, 4, 5)
        txt查询列.Name = "txt查询列"
        txt查询列.Size = New Size(146, 31)
        txt查询列.TabIndex = 4
        ' 
        ' lbl编码列
        ' 
        lbl编码列.AutoSize = True
        lbl编码列.Location = New System.Drawing.Point(311, 120)
        lbl编码列.Margin = New Padding(4, 0, 4, 0)
        lbl编码列.Name = "lbl编码列"
        lbl编码列.Size = New Size(88, 25)
        lbl编码列.TabIndex = 3
        lbl编码列.Text = "编码列："
        ' 
        ' txt查找范围
        ' 
        txt查找范围.Location = New System.Drawing.Point(124, 111)
        txt查找范围.Margin = New Padding(4, 5, 4, 5)
        txt查找范围.Name = "txt查找范围"
        txt查找范围.Size = New Size(146, 31)
        txt查找范围.TabIndex = 2
        ' 
        ' lbl查询列
        ' 
        lbl查询列.AutoSize = True
        lbl查询列.Location = New System.Drawing.Point(13, 120)
        lbl查询列.Margin = New Padding(4, 0, 4, 0)
        lbl查询列.Name = "lbl查询列"
        lbl查询列.Size = New Size(88, 25)
        lbl查询列.TabIndex = 1
        lbl查询列.Text = "查询列："
        ' 
        ' txt基础数据文件
        ' 
        txt基础数据文件.Location = New System.Drawing.Point(170, 39)
        txt基础数据文件.Margin = New Padding(4, 5, 4, 5)
        txt基础数据文件.Name = "txt基础数据文件"
        txt基础数据文件.ReadOnly = True
        txt基础数据文件.Size = New Size(677, 31)
        txt基础数据文件.TabIndex = 8
        txt基础数据文件.TabStop = False
        ' 
        ' lbl基础数据文件
        ' 
        lbl基础数据文件.AutoSize = True
        lbl基础数据文件.Location = New System.Drawing.Point(11, 48)
        lbl基础数据文件.Margin = New Padding(4, 0, 4, 0)
        lbl基础数据文件.Name = "lbl基础数据文件"
        lbl基础数据文件.Size = New Size(145, 25)
        lbl基础数据文件.TabIndex = 7
        lbl基础数据文件.Text = "基础数据文件："
        ' 
        ' GroupBoxBOM导出项目
        ' 
        GroupBoxBOM导出项目.Controls.Add(chk使用UTF8编码)
        GroupBoxBOM导出项目.Controls.Add(btn清除)
        GroupBoxBOM导出项目.Controls.Add(btn还原)
        GroupBoxBOM导出项目.Controls.Add(btn添加)
        GroupBoxBOM导出项目.Controls.Add(cmb添加)
        GroupBoxBOM导出项目.Controls.Add(txtBOM导出项)
        GroupBoxBOM导出项目.Controls.Add(lblBOM导出项目)
        GroupBoxBOM导出项目.Location = New System.Drawing.Point(10, 38)
        GroupBoxBOM导出项目.Margin = New Padding(4, 5, 4, 5)
        GroupBoxBOM导出项目.Name = "GroupBoxBOM导出项目"
        GroupBoxBOM导出项目.Padding = New Padding(4, 5, 4, 5)
        GroupBoxBOM导出项目.Size = New Size(990, 175)
        GroupBoxBOM导出项目.TabIndex = 21
        GroupBoxBOM导出项目.TabStop = False
        GroupBoxBOM导出项目.Text = "BOM导出"
        ' 
        ' chk使用UTF8编码
        ' 
        chk使用UTF8编码.AutoSize = True
        chk使用UTF8编码.Location = New System.Drawing.Point(247, 109)
        chk使用UTF8编码.Margin = New Padding(4, 5, 4, 5)
        chk使用UTF8编码.Name = "chk使用UTF8编码"
        chk使用UTF8编码.Size = New Size(154, 29)
        chk使用UTF8编码.TabIndex = 5
        chk使用UTF8编码.Text = "使用UTF8编码"
        chk使用UTF8编码.UseVisualStyleBackColor = True
        ' 
        ' btn清除
        ' 
        btn清除.Location = New System.Drawing.Point(129, 98)
        btn清除.Margin = New Padding(4, 5, 4, 5)
        btn清除.Name = "btn清除"
        btn清除.Size = New Size(89, 52)
        btn清除.TabIndex = 2
        btn清除.Text = "清除"
        btn清除.UseVisualStyleBackColor = True
        ' 
        ' btn还原
        ' 
        btn还原.Location = New System.Drawing.Point(23, 98)
        btn还原.Margin = New Padding(4, 5, 4, 5)
        btn还原.Name = "btn还原"
        btn还原.Size = New Size(89, 52)
        btn还原.TabIndex = 1
        btn还原.Text = "还原"
        btn还原.UseVisualStyleBackColor = True
        ' 
        ' btn添加
        ' 
        btn添加.Location = New System.Drawing.Point(876, 98)
        btn添加.Margin = New Padding(4, 5, 4, 5)
        btn添加.Name = "btn添加"
        btn添加.Size = New Size(89, 52)
        btn添加.TabIndex = 4
        btn添加.Text = "添加"
        btn添加.UseVisualStyleBackColor = True
        ' 
        ' cmb添加
        ' 
        cmb添加.DropDownStyle = ComboBoxStyle.DropDownList
        cmb添加.FormattingEnabled = True
        cmb添加.Location = New System.Drawing.Point(558, 104)
        cmb添加.Margin = New Padding(4, 5, 4, 5)
        cmb添加.Name = "cmb添加"
        cmb添加.Size = New Size(281, 33)
        cmb添加.Sorted = True
        cmb添加.TabIndex = 3
        ' 
        ' txtBOM导出项
        ' 
        txtBOM导出项.Location = New System.Drawing.Point(143, 39)
        txtBOM导出项.Margin = New Padding(4, 5, 4, 5)
        txtBOM导出项.Name = "txtBOM导出项"
        txtBOM导出项.Size = New Size(824, 31)
        txtBOM导出项.TabIndex = 0
        txtBOM导出项.Text = "项目序号|库存编号|成本中心|零件代号|材料|质量|所属装配代号|数量|总数量|描述"
        ' 
        ' lblBOM导出项目
        ' 
        lblBOM导出项目.AutoSize = True
        lblBOM导出项目.Location = New System.Drawing.Point(11, 46)
        lblBOM导出项目.Margin = New Padding(4, 0, 4, 0)
        lblBOM导出项目.Name = "lblBOM导出项目"
        lblBOM导出项目.Size = New Size(147, 25)
        lblBOM导出项目.TabIndex = 4
        lblBOM导出项目.Text = "BOM导出项目："
        ' 
        ' TabPage常规
        ' 
        TabPage常规.Controls.Add(GroupBox快速打印)
        TabPage常规.Controls.Add(GroupBox精度设置)
        TabPage常规.Controls.Add(GroupBox签字)
        TabPage常规.Controls.Add(GroupBox比例映射)
        TabPage常规.Controls.Add(btn配置文件)
        TabPage常规.Controls.Add(GroupBox对称零件iProperty映射)
        TabPage常规.Controls.Add(GroupBoxiProperty映射)
        TabPage常规.Location = New System.Drawing.Point(4, 34)
        TabPage常规.Margin = New Padding(4, 5, 4, 5)
        TabPage常规.Name = "TabPage常规"
        TabPage常规.Padding = New Padding(4, 5, 4, 5)
        TabPage常规.Size = New Size(1032, 640)
        TabPage常规.TabIndex = 0
        TabPage常规.Text = "常规"
        TabPage常规.UseVisualStyleBackColor = True
        ' 
        ' GroupBox快速打印
        ' 
        GroupBox快速打印.Controls.Add(cmb另存为)
        GroupBox快速打印.Controls.Add(cmb打印机)
        GroupBox快速打印.Controls.Add(chk匹配A3纸)
        GroupBox快速打印.Controls.Add(chk签字)
        GroupBox快速打印.Controls.Add(lbl打印机)
        GroupBox快速打印.Location = New System.Drawing.Point(667, 296)
        GroupBox快速打印.Margin = New Padding(4, 5, 4, 5)
        GroupBox快速打印.Name = "GroupBox快速打印"
        GroupBox快速打印.Padding = New Padding(4, 5, 4, 5)
        GroupBox快速打印.Size = New Size(344, 252)
        GroupBox快速打印.TabIndex = 22
        GroupBox快速打印.TabStop = False
        GroupBox快速打印.Text = "快速打印"
        ' 
        ' cmb另存为
        ' 
        cmb另存为.DropDownStyle = ComboBoxStyle.DropDownList
        cmb另存为.FormattingEnabled = True
        cmb另存为.Items.AddRange(New Object() {"不另存", "另存为dwg", "另存为pdf", "另存为dwg和pdf"})
        cmb另存为.Location = New System.Drawing.Point(17, 172)
        cmb另存为.Margin = New Padding(4, 5, 4, 5)
        cmb另存为.Name = "cmb另存为"
        cmb另存为.Size = New Size(234, 33)
        cmb另存为.TabIndex = 3
        ' 
        ' cmb打印机
        ' 
        cmb打印机.DropDownStyle = ComboBoxStyle.DropDownList
        cmb打印机.ForeColor = SystemColors.WindowText
        cmb打印机.FormattingEnabled = True
        cmb打印机.Location = New System.Drawing.Point(96, 48)
        cmb打印机.Margin = New Padding(4, 5, 4, 5)
        cmb打印机.Name = "cmb打印机"
        cmb打印机.Size = New Size(233, 33)
        cmb打印机.Sorted = True
        cmb打印机.TabIndex = 0
        ' 
        ' chk匹配A3纸
        ' 
        chk匹配A3纸.Anchor = AnchorStyles.None
        chk匹配A3纸.AutoSize = True
        chk匹配A3纸.Checked = True
        chk匹配A3纸.CheckState = CheckState.Checked
        chk匹配A3纸.Location = New System.Drawing.Point(23, 114)
        chk匹配A3纸.Margin = New Padding(4, 5, 4, 5)
        chk匹配A3纸.Name = "chk匹配A3纸"
        chk匹配A3纸.Size = New Size(117, 29)
        chk匹配A3纸.TabIndex = 1
        chk匹配A3纸.Text = "匹配A3纸"
        chk匹配A3纸.UseVisualStyleBackColor = True
        ' 
        ' chk签字
        ' 
        chk签字.Anchor = AnchorStyles.None
        chk签字.AutoSize = True
        chk签字.Checked = True
        chk签字.CheckState = CheckState.Checked
        chk签字.Location = New System.Drawing.Point(173, 114)
        chk签字.Margin = New Padding(4, 5, 4, 5)
        chk签字.Name = "chk签字"
        chk签字.Size = New Size(76, 29)
        chk签字.TabIndex = 2
        chk签字.Text = "签字"
        chk签字.UseVisualStyleBackColor = True
        ' 
        ' lbl打印机
        ' 
        lbl打印机.AutoSize = True
        lbl打印机.Location = New System.Drawing.Point(13, 54)
        lbl打印机.Margin = New Padding(4, 0, 4, 0)
        lbl打印机.Name = "lbl打印机"
        lbl打印机.Size = New Size(88, 25)
        lbl打印机.TabIndex = 9
        lbl打印机.Text = "打印机："
        ' 
        ' GroupBox精度设置
        ' 
        GroupBox精度设置.Controls.Add(cmb面积精度)
        GroupBox精度设置.Controls.Add(lbl面积精度)
        GroupBox精度设置.Controls.Add(cmb质量精度)
        GroupBox精度设置.Controls.Add(lbl质量精度)
        GroupBox精度设置.Location = New System.Drawing.Point(338, 434)
        GroupBox精度设置.Margin = New Padding(4, 5, 4, 5)
        GroupBox精度设置.Name = "GroupBox精度设置"
        GroupBox精度设置.Padding = New Padding(4, 5, 4, 5)
        GroupBox精度设置.Size = New Size(300, 162)
        GroupBox精度设置.TabIndex = 0
        GroupBox精度设置.TabStop = False
        GroupBox精度设置.Text = "精度设置"
        ' 
        ' cmb面积精度
        ' 
        cmb面积精度.DropDownStyle = ComboBoxStyle.DropDownList
        cmb面积精度.FormattingEnabled = True
        cmb面积精度.Items.AddRange(New Object() {"0", "0.1", "0.01", "0.001", "0.0001", "0.00001"})
        cmb面积精度.Location = New System.Drawing.Point(133, 95)
        cmb面积精度.Margin = New Padding(4, 5, 4, 5)
        cmb面积精度.Name = "cmb面积精度"
        cmb面积精度.Size = New Size(107, 33)
        cmb面积精度.TabIndex = 1
        ' 
        ' lbl面积精度
        ' 
        lbl面积精度.AutoSize = True
        lbl面积精度.Location = New System.Drawing.Point(18, 102)
        lbl面积精度.Margin = New Padding(4, 0, 4, 0)
        lbl面积精度.Name = "lbl面积精度"
        lbl面积精度.Size = New Size(107, 25)
        lbl面积精度.TabIndex = 11
        lbl面积精度.Text = "面积精度："
        ' 
        ' cmb质量精度
        ' 
        cmb质量精度.DropDownStyle = ComboBoxStyle.DropDownList
        cmb质量精度.FormattingEnabled = True
        cmb质量精度.Items.AddRange(New Object() {"0", "0.1", "0.01", "0.001"})
        cmb质量精度.Location = New System.Drawing.Point(133, 38)
        cmb质量精度.Margin = New Padding(4, 5, 4, 5)
        cmb质量精度.Name = "cmb质量精度"
        cmb质量精度.Size = New Size(107, 33)
        cmb质量精度.TabIndex = 0
        ' 
        ' lbl质量精度
        ' 
        lbl质量精度.AutoSize = True
        lbl质量精度.Location = New System.Drawing.Point(18, 46)
        lbl质量精度.Margin = New Padding(4, 0, 4, 0)
        lbl质量精度.Name = "lbl质量精度"
        lbl质量精度.Size = New Size(107, 25)
        lbl质量精度.TabIndex = 9
        lbl质量精度.Text = "质量精度："
        ' 
        ' GroupBox签字
        ' 
        GroupBox签字.Controls.Add(chk短日期)
        GroupBox签字.Controls.Add(chk同时签字)
        GroupBox签字.Controls.Add(txt工程师)
        GroupBox签字.Controls.Add(lbl工程师)
        GroupBox签字.Controls.Add(chk签字后打印)
        GroupBox签字.Controls.Add(txt打印日期)
        GroupBox签字.Controls.Add(lbl打印日期)
        GroupBox签字.Location = New System.Drawing.Point(667, 21)
        GroupBox签字.Margin = New Padding(4, 5, 4, 5)
        GroupBox签字.Name = "GroupBox签字"
        GroupBox签字.Padding = New Padding(4, 5, 4, 5)
        GroupBox签字.Size = New Size(344, 261)
        GroupBox签字.TabIndex = 19
        GroupBox签字.TabStop = False
        GroupBox签字.Text = "签字"
        ' 
        ' chk短日期
        ' 
        chk短日期.AutoSize = True
        chk短日期.Location = New System.Drawing.Point(23, 205)
        chk短日期.Margin = New Padding(4, 5, 4, 5)
        chk短日期.Name = "chk短日期"
        chk短日期.Size = New Size(95, 29)
        chk短日期.TabIndex = 9
        chk短日期.Text = "短日期"
        chk短日期.UseVisualStyleBackColor = True
        ' 
        ' chk同时签字
        ' 
        chk同时签字.AutoSize = True
        chk同时签字.Location = New System.Drawing.Point(213, 161)
        chk同时签字.Margin = New Padding(4, 5, 4, 5)
        chk同时签字.Name = "chk同时签字"
        chk同时签字.Size = New Size(114, 29)
        chk同时签字.TabIndex = 3
        chk同时签字.Text = "同时签字"
        chk同时签字.UseVisualStyleBackColor = True
        chk同时签字.Visible = False
        ' 
        ' txt工程师
        ' 
        txt工程师.Location = New System.Drawing.Point(138, 96)
        txt工程师.Margin = New Padding(4, 5, 4, 5)
        txt工程师.Name = "txt工程师"
        txt工程师.Size = New Size(161, 31)
        txt工程师.TabIndex = 1
        ' 
        ' lbl工程师
        ' 
        lbl工程师.AutoSize = True
        lbl工程师.Location = New System.Drawing.Point(20, 104)
        lbl工程师.Margin = New Padding(4, 0, 4, 0)
        lbl工程师.Name = "lbl工程师"
        lbl工程师.Size = New Size(88, 25)
        lbl工程师.TabIndex = 8
        lbl工程师.Text = "工程师："
        ' 
        ' chk签字后打印
        ' 
        chk签字后打印.AutoSize = True
        chk签字后打印.Location = New System.Drawing.Point(23, 161)
        chk签字后打印.Margin = New Padding(4, 5, 4, 5)
        chk签字后打印.Name = "chk签字后打印"
        chk签字后打印.Size = New Size(171, 29)
        chk签字后打印.TabIndex = 2
        chk签字后打印.Text = "签字后打开打印"
        chk签字后打印.UseVisualStyleBackColor = True
        ' 
        ' txt打印日期
        ' 
        txt打印日期.Location = New System.Drawing.Point(138, 39)
        txt打印日期.Margin = New Padding(4, 5, 4, 5)
        txt打印日期.Name = "txt打印日期"
        txt打印日期.Size = New Size(161, 31)
        txt打印日期.TabIndex = 0
        ' 
        ' lbl打印日期
        ' 
        lbl打印日期.AutoSize = True
        lbl打印日期.Location = New System.Drawing.Point(20, 48)
        lbl打印日期.Margin = New Padding(4, 0, 4, 0)
        lbl打印日期.Name = "lbl打印日期"
        lbl打印日期.Size = New Size(107, 25)
        lbl打印日期.TabIndex = 4
        lbl打印日期.Text = "打印日期："
        ' 
        ' GroupBox比例映射
        ' 
        GroupBox比例映射.Controls.Add(txt图号)
        GroupBox比例映射.Controls.Add(txt比例)
        GroupBox比例映射.Controls.Add(lbl质量)
        GroupBox比例映射.Controls.Add(lbl比例)
        GroupBox比例映射.Location = New System.Drawing.Point(338, 259)
        GroupBox比例映射.Margin = New Padding(4, 5, 4, 5)
        GroupBox比例映射.Name = "GroupBox比例映射"
        GroupBox比例映射.Padding = New Padding(4, 5, 4, 5)
        GroupBox比例映射.Size = New Size(300, 162)
        GroupBox比例映射.TabIndex = 18
        GroupBox比例映射.TabStop = False
        GroupBox比例映射.Text = "其他映射"
        ' 
        ' txt图号
        ' 
        txt图号.Location = New System.Drawing.Point(90, 95)
        txt图号.Margin = New Padding(4, 5, 4, 5)
        txt图号.Name = "txt图号"
        txt图号.Size = New Size(104, 31)
        txt图号.TabIndex = 6
        ' 
        ' txt比例
        ' 
        txt比例.Location = New System.Drawing.Point(90, 38)
        txt比例.Margin = New Padding(4, 5, 4, 5)
        txt比例.Name = "txt比例"
        txt比例.Size = New Size(104, 31)
        txt比例.TabIndex = 0
        ' 
        ' lbl质量
        ' 
        lbl质量.AutoSize = True
        lbl质量.Location = New System.Drawing.Point(18, 102)
        lbl质量.Margin = New Padding(4, 0, 4, 0)
        lbl质量.Name = "lbl质量"
        lbl质量.Size = New Size(69, 25)
        lbl质量.TabIndex = 0
        lbl质量.Text = "质量："
        ' 
        ' lbl比例
        ' 
        lbl比例.AutoSize = True
        lbl比例.Location = New System.Drawing.Point(20, 46)
        lbl比例.Margin = New Padding(4, 0, 4, 0)
        lbl比例.Name = "lbl比例"
        lbl比例.Size = New Size(69, 25)
        lbl比例.TabIndex = 4
        lbl比例.Text = "比例："
        ' 
        ' btn配置文件
        ' 
        btn配置文件.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btn配置文件.ContextMenuStrip = ContextMenuStrip配置文件
        btn配置文件.Location = New System.Drawing.Point(10, 541)
        btn配置文件.Margin = New Padding(4, 5, 4, 5)
        btn配置文件.Name = "btn配置文件"
        btn配置文件.Size = New Size(124, 59)
        btn配置文件.TabIndex = 0
        btn配置文件.Text = "配置文件"
        btn配置文件.UseVisualStyleBackColor = True
        ' 
        ' GroupBox对称零件iProperty映射
        ' 
        GroupBox对称零件iProperty映射.Controls.Add(txt对称件编码映射)
        GroupBox对称零件iProperty映射.Controls.Add(lbl对称件编码)
        GroupBox对称零件iProperty映射.Controls.Add(txt对称件文件名映射)
        GroupBox对称零件iProperty映射.Controls.Add(txt对称件图号映射)
        GroupBox对称零件iProperty映射.Controls.Add(lbl对称件文件名)
        GroupBox对称零件iProperty映射.Controls.Add(lbl对称件图号)
        GroupBox对称零件iProperty映射.Location = New System.Drawing.Point(338, 21)
        GroupBox对称零件iProperty映射.Margin = New Padding(4, 5, 4, 5)
        GroupBox对称零件iProperty映射.Name = "GroupBox对称零件iProperty映射"
        GroupBox对称零件iProperty映射.Padding = New Padding(4, 5, 4, 5)
        GroupBox对称零件iProperty映射.Size = New Size(300, 220)
        GroupBox对称零件iProperty映射.TabIndex = 17
        GroupBox对称零件iProperty映射.TabStop = False
        GroupBox对称零件iProperty映射.Text = "对称零件iProperty映射"
        ' 
        ' txt对称件编码映射
        ' 
        txt对称件编码映射.Location = New System.Drawing.Point(129, 155)
        txt对称件编码映射.Margin = New Padding(4, 5, 4, 5)
        txt对称件编码映射.Name = "txt对称件编码映射"
        txt对称件编码映射.Size = New Size(142, 31)
        txt对称件编码映射.TabIndex = 5
        ' 
        ' lbl对称件编码
        ' 
        lbl对称件编码.AutoSize = True
        lbl对称件编码.Location = New System.Drawing.Point(36, 166)
        lbl对称件编码.Margin = New Padding(4, 0, 4, 0)
        lbl对称件编码.Name = "lbl对称件编码"
        lbl对称件编码.Size = New Size(79, 25)
        lbl对称件编码.TabIndex = 6
        lbl对称件编码.Text = "编  码："
        ' 
        ' txt对称件文件名映射
        ' 
        txt对称件文件名映射.Location = New System.Drawing.Point(129, 102)
        txt对称件文件名映射.Margin = New Padding(4, 5, 4, 5)
        txt对称件文件名映射.Name = "txt对称件文件名映射"
        txt对称件文件名映射.Size = New Size(142, 31)
        txt对称件文件名映射.TabIndex = 1
        ' 
        ' txt对称件图号映射
        ' 
        txt对称件图号映射.Location = New System.Drawing.Point(129, 41)
        txt对称件图号映射.Margin = New Padding(4, 5, 4, 5)
        txt对称件图号映射.Name = "txt对称件图号映射"
        txt对称件图号映射.Size = New Size(142, 31)
        txt对称件图号映射.TabIndex = 0
        ' 
        ' lbl对称件文件名
        ' 
        lbl对称件文件名.AutoSize = True
        lbl对称件文件名.Location = New System.Drawing.Point(36, 109)
        lbl对称件文件名.Margin = New Padding(4, 0, 4, 0)
        lbl对称件文件名.Name = "lbl对称件文件名"
        lbl对称件文件名.Size = New Size(88, 25)
        lbl对称件文件名.TabIndex = 4
        lbl对称件文件名.Text = "文件名："
        ' 
        ' lbl对称件图号
        ' 
        lbl对称件图号.AutoSize = True
        lbl对称件图号.Location = New System.Drawing.Point(36, 50)
        lbl对称件图号.Margin = New Padding(4, 0, 4, 0)
        lbl对称件图号.Name = "lbl对称件图号"
        lbl对称件图号.Size = New Size(79, 25)
        lbl对称件图号.TabIndex = 3
        lbl对称件图号.Text = "图  号："
        ' 
        ' GroupBoxiProperty映射
        ' 
        GroupBoxiProperty映射.Controls.Add(cmb连接符)
        GroupBoxiProperty映射.Controls.Add(lbl连接符)
        GroupBoxiProperty映射.Controls.Add(cmb供应商)
        GroupBoxiProperty映射.Controls.Add(lbl采购来源)
        GroupBoxiProperty映射.Controls.Add(cmb存货编码)
        GroupBoxiProperty映射.Controls.Add(lbl存货编码)
        GroupBoxiProperty映射.Controls.Add(lbl文件名)
        GroupBoxiProperty映射.Controls.Add(cmb文件名)
        GroupBoxiProperty映射.Controls.Add(lbl图号)
        GroupBoxiProperty映射.Controls.Add(cmb图号)
        GroupBoxiProperty映射.Location = New System.Drawing.Point(10, 21)
        GroupBoxiProperty映射.Margin = New Padding(4, 5, 4, 5)
        GroupBoxiProperty映射.Name = "GroupBoxiProperty映射"
        GroupBoxiProperty映射.Padding = New Padding(4, 5, 4, 5)
        GroupBoxiProperty映射.Size = New Size(318, 350)
        GroupBoxiProperty映射.TabIndex = 16
        GroupBoxiProperty映射.TabStop = False
        GroupBoxiProperty映射.Text = "iProperty映射"
        ' 
        ' cmb连接符
        ' 
        cmb连接符.DropDownStyle = ComboBoxStyle.DropDownList
        cmb连接符.FormattingEnabled = True
        cmb连接符.Items.AddRange(New Object() {"无", "空格", "点.", "短横线-", "下划线_"})
        cmb连接符.Location = New System.Drawing.Point(136, 284)
        cmb连接符.Margin = New Padding(4, 5, 4, 5)
        cmb连接符.Name = "cmb连接符"
        cmb连接符.Size = New Size(148, 33)
        cmb连接符.TabIndex = 38
        ' 
        ' lbl连接符
        ' 
        lbl连接符.AutoSize = True
        lbl连接符.Location = New System.Drawing.Point(27, 291)
        lbl连接符.Margin = New Padding(4, 0, 4, 0)
        lbl连接符.Name = "lbl连接符"
        lbl连接符.Size = New Size(88, 25)
        lbl连接符.TabIndex = 37
        lbl连接符.Text = "连接符："
        ' 
        ' cmb供应商
        ' 
        cmb供应商.DropDownStyle = ComboBoxStyle.DropDownList
        cmb供应商.FormattingEnabled = True
        cmb供应商.Items.AddRange(New Object() {"成本中心", "供应商", "库存编号", "零件代号", "描述", "项目", "预估成本"})
        cmb供应商.Location = New System.Drawing.Point(142, 220)
        cmb供应商.Margin = New Padding(4, 5, 4, 5)
        cmb供应商.Name = "cmb供应商"
        cmb供应商.Size = New Size(141, 33)
        cmb供应商.Sorted = True
        cmb供应商.TabIndex = 3
        ' 
        ' lbl采购来源
        ' 
        lbl采购来源.AutoSize = True
        lbl采购来源.Location = New System.Drawing.Point(27, 225)
        lbl采购来源.Margin = New Padding(4, 0, 4, 0)
        lbl采购来源.Name = "lbl采购来源"
        lbl采购来源.Size = New Size(107, 25)
        lbl采购来源.TabIndex = 7
        lbl采购来源.Text = "采购来源："
        ' 
        ' cmb存货编码
        ' 
        cmb存货编码.DropDownStyle = ComboBoxStyle.DropDownList
        cmb存货编码.FormattingEnabled = True
        cmb存货编码.Items.AddRange(New Object() {"成本中心", "供应商", "库存编号", "零件代号", "描述", "项目", "预估成本"})
        cmb存货编码.Location = New System.Drawing.Point(142, 155)
        cmb存货编码.Margin = New Padding(4, 5, 4, 5)
        cmb存货编码.Name = "cmb存货编码"
        cmb存货编码.Size = New Size(141, 33)
        cmb存货编码.Sorted = True
        cmb存货编码.TabIndex = 2
        ' 
        ' lbl存货编码
        ' 
        lbl存货编码.AutoSize = True
        lbl存货编码.Location = New System.Drawing.Point(27, 166)
        lbl存货编码.Margin = New Padding(4, 0, 4, 0)
        lbl存货编码.Name = "lbl存货编码"
        lbl存货编码.Size = New Size(107, 25)
        lbl存货编码.TabIndex = 5
        lbl存货编码.Text = "存货编码："
        ' 
        ' lbl文件名
        ' 
        lbl文件名.AutoSize = True
        lbl文件名.Location = New System.Drawing.Point(27, 109)
        lbl文件名.Margin = New Padding(4, 0, 4, 0)
        lbl文件名.Name = "lbl文件名"
        lbl文件名.Size = New Size(88, 25)
        lbl文件名.TabIndex = 4
        lbl文件名.Text = "文件名："
        ' 
        ' cmb文件名
        ' 
        cmb文件名.DropDownStyle = ComboBoxStyle.DropDownList
        cmb文件名.FormattingEnabled = True
        cmb文件名.Items.AddRange(New Object() {"库存编号", "零件代号"})
        cmb文件名.Location = New System.Drawing.Point(142, 102)
        cmb文件名.Margin = New Padding(4, 5, 4, 5)
        cmb文件名.Name = "cmb文件名"
        cmb文件名.Size = New Size(142, 33)
        cmb文件名.Sorted = True
        cmb文件名.TabIndex = 1
        ' 
        ' lbl图号
        ' 
        lbl图号.AutoSize = True
        lbl图号.Location = New System.Drawing.Point(27, 50)
        lbl图号.Margin = New Padding(4, 0, 4, 0)
        lbl图号.Name = "lbl图号"
        lbl图号.Size = New Size(79, 25)
        lbl图号.TabIndex = 3
        lbl图号.Text = "图  号："
        ' 
        ' cmb图号
        ' 
        cmb图号.DropDownStyle = ComboBoxStyle.DropDownList
        cmb图号.Items.AddRange(New Object() {"库存编号", "零件代号"})
        cmb图号.Location = New System.Drawing.Point(142, 41)
        cmb图号.Margin = New Padding(4, 5, 4, 5)
        cmb图号.Name = "cmb图号"
        cmb图号.Size = New Size(142, 33)
        cmb图号.Sorted = True
        cmb图号.TabIndex = 0
        ' 
        ' TabControl
        ' 
        TabControl.Controls.Add(TabPage常规)
        TabControl.Controls.Add(TabPage模型)
        TabControl.Controls.Add(TabPage工程图)
        TabControl.Controls.Add(TabPage展开图)
        TabControl.Controls.Add(TabPage其他)
        TabControl.Location = New System.Drawing.Point(3, 22)
        TabControl.Margin = New Padding(4, 5, 4, 5)
        TabControl.Name = "TabControl"
        TabControl.SelectedIndex = 0
        TabControl.Size = New Size(1040, 678)
        TabControl.TabIndex = 0
        ' 
        ' Btn添加2
        ' 
        Btn添加2.Location = New System.Drawing.Point(886, 573)
        Btn添加2.Margin = New Padding(4, 5, 4, 5)
        Btn添加2.Name = "Btn添加2"
        Btn添加2.Size = New Size(89, 52)
        Btn添加2.TabIndex = 39
        Btn添加2.Text = "添加"
        Btn添加2.UseVisualStyleBackColor = True
        ' 
        ' FormOption
        ' 
        AcceptButton = btn确定
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn关闭
        ClientSize = New Size(1063, 795)
        Controls.Add(TabControl)
        Controls.Add(btn确定)
        Controls.Add(btn关闭)
        Controls.Add(chk检查更新)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        Name = "FormOption"
        StartPosition = FormStartPosition.CenterScreen
        Text = "应用程序选项"
        ContextMenuStrip配置文件.ResumeLayout(False)
        TabPage其他.ResumeLayout(False)
        TabPage其他.PerformLayout()
        GroupBox自动保存.ResumeLayout(False)
        GroupBox自动保存.PerformLayout()
        GroupBox截图.ResumeLayout(False)
        GroupBox截图.PerformLayout()
        TabPage展开图.ResumeLayout(False)
        TabPage展开图.PerformLayout()
        GroupBox展开图模板.ResumeLayout(False)
        GroupBox展开图模板.PerformLayout()
        GroupBox下.ResumeLayout(False)
        GroupBox下.PerformLayout()
        GroupBox上.ResumeLayout(False)
        GroupBox上.PerformLayout()
        TabPage工程图.ResumeLayout(False)
        TabPage工程图.PerformLayout()
        GroupBox标题栏.ResumeLayout(False)
        GroupBox标题栏.PerformLayout()
        GroupBox页边距.ResumeLayout(False)
        GroupBox页边距.PerformLayout()
        CType(NumericUpDown页边距右, System.ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown页边距左, System.ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown页边距下, System.ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown页边距上, System.ComponentModel.ISupportInitialize).EndInit()
        GroupBox视图.ResumeLayout(False)
        GroupBox视图.PerformLayout()
        GroupBox工程图模板.ResumeLayout(False)
        GroupBox工程图模板.PerformLayout()
        TabPage模型.ResumeLayout(False)
        TabPage模型.PerformLayout()
        CType(NUD查找文件夹层数, System.ComponentModel.ISupportInitialize).EndInit()
        GroupBoxERP查询.ResumeLayout(False)
        GroupBoxERP查询.PerformLayout()
        GroupBoxBOM导出项目.ResumeLayout(False)
        GroupBoxBOM导出项目.PerformLayout()
        TabPage常规.ResumeLayout(False)
        GroupBox快速打印.ResumeLayout(False)
        GroupBox快速打印.PerformLayout()
        GroupBox精度设置.ResumeLayout(False)
        GroupBox精度设置.PerformLayout()
        GroupBox签字.ResumeLayout(False)
        GroupBox签字.PerformLayout()
        GroupBox比例映射.ResumeLayout(False)
        GroupBox比例映射.PerformLayout()
        GroupBox对称零件iProperty映射.ResumeLayout(False)
        GroupBox对称零件iProperty映射.PerformLayout()
        GroupBoxiProperty映射.ResumeLayout(False)
        GroupBoxiProperty映射.PerformLayout()
        TabControl.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

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
    Friend WithEvents txtBOM显示列 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn添加2 As Button
End Class
