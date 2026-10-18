<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFormatConversion
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFormatConversion))
        Lvw文件列表 = New ListView()
        ColumnHeader文件名 = New ColumnHeader()
        cms右键菜单 = New ContextMenuStrip(components)
        tsmi移出 = New ToolStripMenuItem()
        tsmi清空 = New ToolStripMenuItem()
        ToolStrip文档格式 = New ToolStrip()
        文档格式ToolStripButton = New ToolStripLabel()
        ToolStripButton1 = New ToolStripSeparator()
        工程图ToolStripButton = New ToolStripButton()
        零部件ToolStripButton = New ToolStripButton()
        ToolStripSeparator5 = New ToolStripSeparator()
        静默转换ToolStripButton = New ToolStripButton()
        转换后关闭ToolStripButton = New ToolStripButton()
        替换ToolStripButton = New ToolStripButton()
        移出文件ToolStripButton = New ToolStripButton()
        清空列表ToolStripButton = New ToolStripButton()
        加载方式ToolStrip = New ToolStrip()
        加载方式ToolStripButton = New ToolStripLabel()
        ToolStripSeparator1 = New ToolStripSeparator()
        添加文件ToolStripButton = New ToolStripButton()
        添加文件夹ToolStripButton = New ToolStripButton()
        从部件导入ToolStripButton = New ToolStripButton()
        导入当前部件ToolStripButton = New ToolStripButton()
        导入已打开的文件ToolStripButton = New ToolStripButton()
        转换格式ToolStrip = New ToolStrip()
        转换格式ToolStripLabel = New ToolStripLabel()
        ToolStripSeparator2 = New ToolStripSeparator()
        DWGToolStripButton = New ToolStripButton()
        DXFToolStripButton = New ToolStripButton()
        PDFToolStripButton = New ToolStripButton()
        JPGToolStripButton = New ToolStripButton()
        STPToolStripButton = New ToolStripButton()
        XTToolStripButton = New ToolStripButton()
        ToolStrip1 = New ToolStrip()
        目录设置ToolStripLabel = New ToolStripLabel()
        ToolStripSeparator4 = New ToolStripSeparator()
        格式分类ToolStripButton = New ToolStripButton()
        黑色PDFToolStripButton = New ToolStripButton()
        指定文件夹ToolStripButton = New ToolStripButton()
        浏览ToolStripButton = New ToolStripButton()
        指定文件夹ToolStripTextBox = New ToolStripTextBox()
        开始转换ToolStrip = New ToolStrip()
        开始转换ToolStripLabel = New ToolStripLabel()
        ToolStripSeparator3 = New ToolStripSeparator()
        进度ToolStripProgressBar = New ToolStripProgressBar()
        开始转换ToolStripButton2 = New ToolStripButton()
        关闭ToolStripButton = New ToolStripButton()
        TableLayoutPanel1 = New TableLayoutPanel()
        cms右键菜单.SuspendLayout()
        ToolStrip文档格式.SuspendLayout()
        加载方式ToolStrip.SuspendLayout()
        转换格式ToolStrip.SuspendLayout()
        ToolStrip1.SuspendLayout()
        开始转换ToolStrip.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Lvw文件列表
        ' 
        Lvw文件列表.AllowColumnReorder = True
        Lvw文件列表.AllowDrop = True
        Lvw文件列表.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Lvw文件列表.AutoArrange = False
        Lvw文件列表.Columns.AddRange(New ColumnHeader() {ColumnHeader文件名})
        Lvw文件列表.ContextMenuStrip = cms右键菜单
        Lvw文件列表.FullRowSelect = True
        Lvw文件列表.Location = New System.Drawing.Point(4, 355)
        Lvw文件列表.Margin = New Padding(4, 5, 4, 5)
        Lvw文件列表.Name = "Lvw文件列表"
        Lvw文件列表.Size = New Size(1214, 779)
        Lvw文件列表.Sorting = SortOrder.Ascending
        Lvw文件列表.TabIndex = 37
        Lvw文件列表.UseCompatibleStateImageBehavior = False
        Lvw文件列表.View = System.Windows.Forms.View.Details
        ' 
        ' ColumnHeader文件名
        ' 
        ColumnHeader文件名.Text = "文件名"
        ColumnHeader文件名.Width = 753
        ' 
        ' cms右键菜单
        ' 
        cms右键菜单.ImageScalingSize = New Size(24, 24)
        cms右键菜单.Items.AddRange(New ToolStripItem() {tsmi移出, tsmi清空})
        cms右键菜单.Name = "cmsRemove"
        cms右键菜单.Size = New Size(123, 68)
        ' 
        ' tsmi移出
        ' 
        tsmi移出.Name = "tsmi移出"
        tsmi移出.Size = New Size(122, 32)
        tsmi移出.Text = "移出"
        ' 
        ' tsmi清空
        ' 
        tsmi清空.Name = "tsmi清空"
        tsmi清空.Size = New Size(122, 32)
        tsmi清空.Text = "清空"
        ' 
        ' ToolStrip文档格式
        ' 
        ToolStrip文档格式.GripStyle = ToolStripGripStyle.Hidden
        ToolStrip文档格式.ImageScalingSize = New Size(24, 24)
        ToolStrip文档格式.Items.AddRange(New ToolStripItem() {文档格式ToolStripButton, ToolStripButton1, 工程图ToolStripButton, 零部件ToolStripButton, ToolStripSeparator5, 静默转换ToolStripButton, 转换后关闭ToolStripButton, 替换ToolStripButton, 移出文件ToolStripButton, 清空列表ToolStripButton})
        ToolStrip文档格式.Location = New System.Drawing.Point(0, 232)
        ToolStrip文档格式.Name = "ToolStrip文档格式"
        ToolStrip文档格式.Padding = New Padding(0, 0, 3, 0)
        ToolStrip文档格式.Size = New Size(1222, 58)
        ToolStrip文档格式.TabIndex = 38
        ToolStrip文档格式.Text = "ToolStrip1"
        ' 
        ' 文档格式ToolStripButton
        ' 
        文档格式ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        文档格式ToolStripButton.Image = CType(resources.GetObject("文档格式ToolStripButton.Image"), Image)
        文档格式ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        文档格式ToolStripButton.Name = "文档格式ToolStripButton"
        文档格式ToolStripButton.Size = New Size(88, 53)
        文档格式ToolStripButton.Text = "文档格式"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(6, 58)
        ' 
        ' 工程图ToolStripButton
        ' 
        工程图ToolStripButton.CheckOnClick = True
        工程图ToolStripButton.Image = CType(resources.GetObject("工程图ToolStripButton.Image"), Image)
        工程图ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        工程图ToolStripButton.Name = "工程图ToolStripButton"
        工程图ToolStripButton.Size = New Size(73, 53)
        工程图ToolStripButton.Text = "工程图"
        工程图ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 零部件ToolStripButton
        ' 
        零部件ToolStripButton.CheckOnClick = True
        零部件ToolStripButton.Image = CType(resources.GetObject("零部件ToolStripButton.Image"), Image)
        零部件ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        零部件ToolStripButton.Name = "零部件ToolStripButton"
        零部件ToolStripButton.Size = New Size(73, 53)
        零部件ToolStripButton.Text = "零部件"
        零部件ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(6, 58)
        ' 
        ' 静默转换ToolStripButton
        ' 
        静默转换ToolStripButton.Checked = True
        静默转换ToolStripButton.CheckOnClick = True
        静默转换ToolStripButton.CheckState = CheckState.Checked
        静默转换ToolStripButton.Image = CType(resources.GetObject("静默转换ToolStripButton.Image"), Image)
        静默转换ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        静默转换ToolStripButton.Name = "静默转换ToolStripButton"
        静默转换ToolStripButton.Size = New Size(92, 53)
        静默转换ToolStripButton.Text = "静默转换"
        静默转换ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        静默转换ToolStripButton.ToolTipText = "不显示文件"
        ' 
        ' 转换后关闭ToolStripButton
        ' 
        转换后关闭ToolStripButton.Checked = True
        转换后关闭ToolStripButton.CheckOnClick = True
        转换后关闭ToolStripButton.CheckState = CheckState.Checked
        转换后关闭ToolStripButton.Image = CType(resources.GetObject("转换后关闭ToolStripButton.Image"), Image)
        转换后关闭ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        转换后关闭ToolStripButton.Name = "转换后关闭ToolStripButton"
        转换后关闭ToolStripButton.Size = New Size(111, 53)
        转换后关闭ToolStripButton.Text = "转换后关闭"
        转换后关闭ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        转换后关闭ToolStripButton.ToolTipText = "转换后关闭文件"
        ' 
        ' 替换ToolStripButton
        ' 
        替换ToolStripButton.CheckOnClick = True
        替换ToolStripButton.Image = CType(resources.GetObject("替换ToolStripButton.Image"), Image)
        替换ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        替换ToolStripButton.Name = "替换ToolStripButton"
        替换ToolStripButton.Size = New Size(54, 53)
        替换ToolStripButton.Text = "替换"
        替换ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        替换ToolStripButton.ToolTipText = "替换现有文件"
        ' 
        ' 移出文件ToolStripButton
        ' 
        移出文件ToolStripButton.Image = CType(resources.GetObject("移出文件ToolStripButton.Image"), Image)
        移出文件ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        移出文件ToolStripButton.Name = "移出文件ToolStripButton"
        移出文件ToolStripButton.Size = New Size(92, 53)
        移出文件ToolStripButton.Text = "移出文件"
        移出文件ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        移出文件ToolStripButton.ToolTipText = "移出选择的文件"
        移出文件ToolStripButton.Visible = False
        ' 
        ' 清空列表ToolStripButton
        ' 
        清空列表ToolStripButton.Image = CType(resources.GetObject("清空列表ToolStripButton.Image"), Image)
        清空列表ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        清空列表ToolStripButton.Name = "清空列表ToolStripButton"
        清空列表ToolStripButton.Size = New Size(92, 53)
        清空列表ToolStripButton.Text = "清空列表"
        清空列表ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        清空列表ToolStripButton.Visible = False
        ' 
        ' 加载方式ToolStrip
        ' 
        加载方式ToolStrip.GripStyle = ToolStripGripStyle.Hidden
        加载方式ToolStrip.ImageScalingSize = New Size(24, 24)
        加载方式ToolStrip.Items.AddRange(New ToolStripItem() {加载方式ToolStripButton, ToolStripSeparator1, 添加文件ToolStripButton, 添加文件夹ToolStripButton, 从部件导入ToolStripButton, 导入当前部件ToolStripButton, 导入已打开的文件ToolStripButton})
        加载方式ToolStrip.Location = New System.Drawing.Point(0, 174)
        加载方式ToolStrip.Name = "加载方式ToolStrip"
        加载方式ToolStrip.Padding = New Padding(0, 0, 3, 0)
        加载方式ToolStrip.Size = New Size(1222, 58)
        加载方式ToolStrip.TabIndex = 39
        加载方式ToolStrip.Text = "加载方式"
        ' 
        ' 加载方式ToolStripButton
        ' 
        加载方式ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text
        加载方式ToolStripButton.Image = CType(resources.GetObject("加载方式ToolStripButton.Image"), Image)
        加载方式ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        加载方式ToolStripButton.Name = "加载方式ToolStripButton"
        加载方式ToolStripButton.Size = New Size(88, 53)
        加载方式ToolStripButton.Text = "加载方式"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 58)
        ' 
        ' 添加文件ToolStripButton
        ' 
        添加文件ToolStripButton.Enabled = False
        添加文件ToolStripButton.Image = CType(resources.GetObject("添加文件ToolStripButton.Image"), Image)
        添加文件ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        添加文件ToolStripButton.Name = "添加文件ToolStripButton"
        添加文件ToolStripButton.Size = New Size(92, 53)
        添加文件ToolStripButton.Text = "添加文件"
        添加文件ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 添加文件夹ToolStripButton
        ' 
        添加文件夹ToolStripButton.Enabled = False
        添加文件夹ToolStripButton.Image = CType(resources.GetObject("添加文件夹ToolStripButton.Image"), Image)
        添加文件夹ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        添加文件夹ToolStripButton.Name = "添加文件夹ToolStripButton"
        添加文件夹ToolStripButton.Size = New Size(111, 53)
        添加文件夹ToolStripButton.Text = "添加文件夹"
        添加文件夹ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 从部件导入ToolStripButton
        ' 
        从部件导入ToolStripButton.Enabled = False
        从部件导入ToolStripButton.Image = CType(resources.GetObject("从部件导入ToolStripButton.Image"), Image)
        从部件导入ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        从部件导入ToolStripButton.Name = "从部件导入ToolStripButton"
        从部件导入ToolStripButton.Size = New Size(111, 53)
        从部件导入ToolStripButton.Text = "从部件导入"
        从部件导入ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 导入当前部件ToolStripButton
        ' 
        导入当前部件ToolStripButton.Enabled = False
        导入当前部件ToolStripButton.Image = CType(resources.GetObject("导入当前部件ToolStripButton.Image"), Image)
        导入当前部件ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        导入当前部件ToolStripButton.Name = "导入当前部件ToolStripButton"
        导入当前部件ToolStripButton.Size = New Size(130, 53)
        导入当前部件ToolStripButton.Text = "导入当前部件"
        导入当前部件ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 导入已打开的文件ToolStripButton
        ' 
        导入已打开的文件ToolStripButton.Enabled = False
        导入已打开的文件ToolStripButton.Image = CType(resources.GetObject("导入已打开的文件ToolStripButton.Image"), Image)
        导入已打开的文件ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        导入已打开的文件ToolStripButton.Name = "导入已打开的文件ToolStripButton"
        导入已打开的文件ToolStripButton.Size = New Size(168, 53)
        导入已打开的文件ToolStripButton.Text = "导入已打开的文件"
        导入已打开的文件ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 转换格式ToolStrip
        ' 
        转换格式ToolStrip.GripStyle = ToolStripGripStyle.Hidden
        转换格式ToolStrip.ImageScalingSize = New Size(24, 24)
        转换格式ToolStrip.Items.AddRange(New ToolStripItem() {转换格式ToolStripLabel, ToolStripSeparator2, DWGToolStripButton, DXFToolStripButton, PDFToolStripButton, JPGToolStripButton, STPToolStripButton, XTToolStripButton})
        转换格式ToolStrip.Location = New System.Drawing.Point(0, 116)
        转换格式ToolStrip.Name = "转换格式ToolStrip"
        转换格式ToolStrip.Padding = New Padding(0, 0, 3, 0)
        转换格式ToolStrip.Size = New Size(1222, 58)
        转换格式ToolStrip.TabIndex = 40
        转换格式ToolStrip.Text = "转换格式"
        ' 
        ' 转换格式ToolStripLabel
        ' 
        转换格式ToolStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text
        转换格式ToolStripLabel.Image = CType(resources.GetObject("转换格式ToolStripLabel.Image"), Image)
        转换格式ToolStripLabel.ImageTransparentColor = Drawing.Color.Magenta
        转换格式ToolStripLabel.Name = "转换格式ToolStripLabel"
        转换格式ToolStripLabel.Size = New Size(88, 53)
        转换格式ToolStripLabel.Text = "转换格式"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 58)
        ' 
        ' DWGToolStripButton
        ' 
        DWGToolStripButton.CheckOnClick = True
        DWGToolStripButton.Enabled = False
        DWGToolStripButton.Image = CType(resources.GetObject("DWGToolStripButton.Image"), Image)
        DWGToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        DWGToolStripButton.Name = "DWGToolStripButton"
        DWGToolStripButton.Size = New Size(58, 53)
        DWGToolStripButton.Text = "DWG"
        DWGToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' DXFToolStripButton
        ' 
        DXFToolStripButton.CheckOnClick = True
        DXFToolStripButton.Enabled = False
        DXFToolStripButton.Image = CType(resources.GetObject("DXFToolStripButton.Image"), Image)
        DXFToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        DXFToolStripButton.Name = "DXFToolStripButton"
        DXFToolStripButton.Size = New Size(48, 53)
        DXFToolStripButton.Text = "DXF"
        DXFToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' PDFToolStripButton
        ' 
        PDFToolStripButton.CheckOnClick = True
        PDFToolStripButton.Enabled = False
        PDFToolStripButton.Image = CType(resources.GetObject("PDFToolStripButton.Image"), Image)
        PDFToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        PDFToolStripButton.Name = "PDFToolStripButton"
        PDFToolStripButton.Size = New Size(48, 53)
        PDFToolStripButton.Text = "PDF"
        PDFToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' JPGToolStripButton
        ' 
        JPGToolStripButton.CheckOnClick = True
        JPGToolStripButton.Enabled = False
        JPGToolStripButton.Image = CType(resources.GetObject("JPGToolStripButton.Image"), Image)
        JPGToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        JPGToolStripButton.Name = "JPGToolStripButton"
        JPGToolStripButton.Size = New Size(44, 53)
        JPGToolStripButton.Text = "JPG"
        JPGToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' STPToolStripButton
        ' 
        STPToolStripButton.CheckOnClick = True
        STPToolStripButton.Enabled = False
        STPToolStripButton.Image = CType(resources.GetObject("STPToolStripButton.Image"), Image)
        STPToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        STPToolStripButton.Name = "STPToolStripButton"
        STPToolStripButton.Size = New Size(45, 53)
        STPToolStripButton.Text = "STP"
        STPToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' XTToolStripButton
        ' 
        XTToolStripButton.CheckOnClick = True
        XTToolStripButton.Enabled = False
        XTToolStripButton.Image = CType(resources.GetObject("XTToolStripButton.Image"), Image)
        XTToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        XTToolStripButton.Name = "XTToolStripButton"
        XTToolStripButton.Size = New Size(43, 53)
        XTToolStripButton.Text = "X_T"
        XTToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.GripStyle = ToolStripGripStyle.Hidden
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {目录设置ToolStripLabel, ToolStripSeparator4, 格式分类ToolStripButton, 黑色PDFToolStripButton, 指定文件夹ToolStripButton, 浏览ToolStripButton, 指定文件夹ToolStripTextBox})
        ToolStrip1.Location = New System.Drawing.Point(0, 58)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1222, 58)
        ToolStrip1.TabIndex = 41
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' 目录设置ToolStripLabel
        ' 
        目录设置ToolStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text
        目录设置ToolStripLabel.Image = CType(resources.GetObject("目录设置ToolStripLabel.Image"), Image)
        目录设置ToolStripLabel.ImageTransparentColor = Drawing.Color.Magenta
        目录设置ToolStripLabel.Name = "目录设置ToolStripLabel"
        目录设置ToolStripLabel.Size = New Size(88, 53)
        目录设置ToolStripLabel.Text = "目录设置"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 58)
        ' 
        ' 格式分类ToolStripButton
        ' 
        格式分类ToolStripButton.Checked = True
        格式分类ToolStripButton.CheckOnClick = True
        格式分类ToolStripButton.CheckState = CheckState.Checked
        格式分类ToolStripButton.Image = CType(resources.GetObject("格式分类ToolStripButton.Image"), Image)
        格式分类ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        格式分类ToolStripButton.Name = "格式分类ToolStripButton"
        格式分类ToolStripButton.Size = New Size(92, 53)
        格式分类ToolStripButton.Text = "格式分类"
        格式分类ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        格式分类ToolStripButton.ToolTipText = "按格式分类到子文件夹"
        ' 
        ' 黑色PDFToolStripButton
        ' 
        黑色PDFToolStripButton.Checked = True
        黑色PDFToolStripButton.CheckOnClick = True
        黑色PDFToolStripButton.CheckState = CheckState.Checked
        黑色PDFToolStripButton.Image = CType(resources.GetObject("黑色PDFToolStripButton.Image"), Image)
        黑色PDFToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        黑色PDFToolStripButton.Name = "黑色PDFToolStripButton"
        黑色PDFToolStripButton.Size = New Size(86, 53)
        黑色PDFToolStripButton.Text = "黑色PDF"
        黑色PDFToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        黑色PDFToolStripButton.ToolTipText = "输出PDF为黑色"
        ' 
        ' 指定文件夹ToolStripButton
        ' 
        指定文件夹ToolStripButton.CheckOnClick = True
        指定文件夹ToolStripButton.Image = CType(resources.GetObject("指定文件夹ToolStripButton.Image"), Image)
        指定文件夹ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        指定文件夹ToolStripButton.Name = "指定文件夹ToolStripButton"
        指定文件夹ToolStripButton.Size = New Size(111, 53)
        指定文件夹ToolStripButton.Text = "指定文件夹"
        指定文件夹ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' 浏览ToolStripButton
        ' 
        浏览ToolStripButton.Enabled = False
        浏览ToolStripButton.Image = CType(resources.GetObject("浏览ToolStripButton.Image"), Image)
        浏览ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        浏览ToolStripButton.Name = "浏览ToolStripButton"
        浏览ToolStripButton.Size = New Size(54, 53)
        浏览ToolStripButton.Text = "浏览"
        浏览ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        浏览ToolStripButton.ToolTipText = "浏览指定文件夹"
        ' 
        ' 指定文件夹ToolStripTextBox
        ' 
        指定文件夹ToolStripTextBox.Enabled = False
        指定文件夹ToolStripTextBox.Font = New Font("Microsoft YaHei UI", 9F)
        指定文件夹ToolStripTextBox.Name = "指定文件夹ToolStripTextBox"
        指定文件夹ToolStripTextBox.Size = New Size(611, 58)
        ' 
        ' 开始转换ToolStrip
        ' 
        开始转换ToolStrip.GripStyle = ToolStripGripStyle.Hidden
        开始转换ToolStrip.ImageScalingSize = New Size(24, 24)
        开始转换ToolStrip.Items.AddRange(New ToolStripItem() {开始转换ToolStripLabel, ToolStripSeparator3, 进度ToolStripProgressBar, 开始转换ToolStripButton2, 关闭ToolStripButton})
        开始转换ToolStrip.Location = New System.Drawing.Point(0, 0)
        开始转换ToolStrip.Name = "开始转换ToolStrip"
        开始转换ToolStrip.Padding = New Padding(0, 0, 3, 0)
        开始转换ToolStrip.Size = New Size(1222, 58)
        开始转换ToolStrip.TabIndex = 42
        开始转换ToolStrip.Text = "开始转换"
        ' 
        ' 开始转换ToolStripLabel
        ' 
        开始转换ToolStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text
        开始转换ToolStripLabel.Image = CType(resources.GetObject("开始转换ToolStripLabel.Image"), Image)
        开始转换ToolStripLabel.ImageTransparentColor = Drawing.Color.Magenta
        开始转换ToolStripLabel.Name = "开始转换ToolStripLabel"
        开始转换ToolStripLabel.Size = New Size(88, 53)
        开始转换ToolStripLabel.Text = "开始转换"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 58)
        ' 
        ' 进度ToolStripProgressBar
        ' 
        进度ToolStripProgressBar.AutoSize = False
        进度ToolStripProgressBar.Name = "进度ToolStripProgressBar"
        进度ToolStripProgressBar.Size = New Size(917, 52)
        进度ToolStripProgressBar.Step = 1
        进度ToolStripProgressBar.Style = ProgressBarStyle.Continuous
        ' 
        ' 开始转换ToolStripButton2
        ' 
        开始转换ToolStripButton2.Image = CType(resources.GetObject("开始转换ToolStripButton2.Image"), Image)
        开始转换ToolStripButton2.ImageTransparentColor = Drawing.Color.Magenta
        开始转换ToolStripButton2.Name = "开始转换ToolStripButton2"
        开始转换ToolStripButton2.Size = New Size(92, 53)
        开始转换ToolStripButton2.Text = "开始转换"
        开始转换ToolStripButton2.TextImageRelation = TextImageRelation.ImageAboveText
        开始转换ToolStripButton2.ToolTipText = "开始转换"
        ' 
        ' 关闭ToolStripButton
        ' 
        关闭ToolStripButton.CheckOnClick = True
        关闭ToolStripButton.Image = CType(resources.GetObject("关闭ToolStripButton.Image"), Image)
        关闭ToolStripButton.ImageTransparentColor = Drawing.Color.Magenta
        关闭ToolStripButton.Name = "关闭ToolStripButton"
        关闭ToolStripButton.Size = New Size(54, 53)
        关闭ToolStripButton.Text = "关闭"
        关闭ToolStripButton.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(Lvw文件列表, 0, 5)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 6
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 70F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 70F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 70F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 70F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 70F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(1222, 1139)
        TableLayoutPanel1.TabIndex = 43
        ' 
        ' FormFormatConversion
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1222, 1139)
        Controls.Add(ToolStrip文档格式)
        Controls.Add(加载方式ToolStrip)
        Controls.Add(转换格式ToolStrip)
        Controls.Add(ToolStrip1)
        Controls.Add(开始转换ToolStrip)
        Controls.Add(TableLayoutPanel1)
        Margin = New Padding(4, 5, 4, 5)
        Name = "FormFormatConversion"
        StartPosition = FormStartPosition.CenterScreen
        Text = "格式转换"
        cms右键菜单.ResumeLayout(False)
        ToolStrip文档格式.ResumeLayout(False)
        ToolStrip文档格式.PerformLayout()
        加载方式ToolStrip.ResumeLayout(False)
        加载方式ToolStrip.PerformLayout()
        转换格式ToolStrip.ResumeLayout(False)
        转换格式ToolStrip.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        开始转换ToolStrip.ResumeLayout(False)
        开始转换ToolStrip.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Lvw文件列表 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader文件名 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cms右键菜单 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmi移出 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmi清空 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip文档格式 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 工程图ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 零部件ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 文档格式ToolStripButton As System.Windows.Forms.ToolStripLabel
    Friend WithEvents 加载方式ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents 加载方式ToolStripButton As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 添加文件ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 添加文件夹ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 从部件导入ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 导入当前部件ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 导入已打开的文件ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 转换格式ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents 转换格式ToolStripLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DWGToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DXFToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PDFToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents JPGToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents STPToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents XTToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents 目录设置ToolStripLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 格式分类ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 指定文件夹ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 浏览ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 移出文件ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 清空列表ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 指定文件夹ToolStripTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents 静默转换ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 转换后关闭ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 开始转换ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents 开始转换ToolStripLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 进度ToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents 开始转换ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 关闭ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 替换ToolStripButton As ToolStripButton
    Friend WithEvents 黑色PDFToolStripButton As ToolStripButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
