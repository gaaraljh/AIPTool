<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormBomTool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBomTool))
        Panel1 = New Panel()
        TableLayoutPanel5 = New TableLayoutPanel()
        ChkBOnlyWeld = New CheckBox()
        ChkBOnlySheet = New CheckBox()
        BtnSelectAll = New Button()
        ChkBOnlyAssem = New CheckBox()
        BtnExPort = New Button()
        ChkBOnlyPart = New CheckBox()
        ChkBMergePartNumber = New CheckBox()
        ChkBStanPart = New CheckBox()
        CmBoxTheme = New ComboBox()
        CmBoxFunction = New ComboBox()
        ChkBRefePart = New CheckBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        SplitContainer1 = New SplitContainer()
        TableLayoutPanel2 = New TableLayoutPanel()
        DGViewBomList = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Panel3 = New Panel()
        BtnRefreshExcel = New Button()
        Label3 = New Label()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        Label1 = New Label()
        NumericUpDown1 = New NumericUpDown()
        Button2 = New Button()
        Splitter1 = New Splitter()
        Label2 = New Label()
        NumericUpDown2 = New NumericUpDown()
        Button3 = New Button()
        Splitter2 = New Splitter()
        BtnExcFill = New Button()
        Splitter3 = New Splitter()
        Panel2 = New Panel()
        TableLayoutPanel3 = New TableLayoutPanel()
        Panel4 = New Panel()
        Label4 = New Label()
        DGViewPropertyList = New DataGridView()
        Column5 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewButtonColumn()
        TableLayoutPanel4 = New TableLayoutPanel()
        FlowLayoutPanel2 = New FlowLayoutPanel()
        BtnUp = New Button()
        BtnDown = New Button()
        Label5 = New Label()
        CmBoxPropertyList = New ComboBox()
        BtnAddPropety = New Button()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Panel1.SuspendLayout()
        TableLayoutPanel5.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        CType(DGViewBomList, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        TableLayoutPanel3.SuspendLayout()
        Panel4.SuspendLayout()
        CType(DGViewPropertyList, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel4.SuspendLayout()
        FlowLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = SystemColors.Highlight
        Panel1.Controls.Add(TableLayoutPanel5)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(3, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1756, 74)
        Panel1.TabIndex = 0
        ' 
        ' TableLayoutPanel5
        ' 
        TableLayoutPanel5.ColumnCount = 12
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 220F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 220F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 200F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 200F))
        TableLayoutPanel5.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 220F))
        TableLayoutPanel5.Controls.Add(ChkBOnlyWeld, 8, 0)
        TableLayoutPanel5.Controls.Add(ChkBOnlySheet, 7, 0)
        TableLayoutPanel5.Controls.Add(BtnSelectAll, 0, 0)
        TableLayoutPanel5.Controls.Add(ChkBOnlyAssem, 6, 0)
        TableLayoutPanel5.Controls.Add(BtnExPort, 1, 0)
        TableLayoutPanel5.Controls.Add(ChkBOnlyPart, 5, 0)
        TableLayoutPanel5.Controls.Add(ChkBMergePartNumber, 4, 0)
        TableLayoutPanel5.Controls.Add(ChkBStanPart, 2, 0)
        TableLayoutPanel5.Controls.Add(CmBoxTheme, 11, 0)
        TableLayoutPanel5.Controls.Add(CmBoxFunction, 10, 0)
        TableLayoutPanel5.Controls.Add(ChkBRefePart, 3, 0)
        TableLayoutPanel5.Dock = DockStyle.Fill
        TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel5.Margin = New Padding(10)
        TableLayoutPanel5.Name = "TableLayoutPanel5"
        TableLayoutPanel5.Padding = New Padding(10)
        TableLayoutPanel5.RowCount = 1
        TableLayoutPanel5.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel5.Size = New Size(1756, 74)
        TableLayoutPanel5.TabIndex = 22
        ' 
        ' ChkBOnlyWeld
        ' 
        ChkBOnlyWeld.AutoSize = True
        ChkBOnlyWeld.Dock = DockStyle.Fill
        ChkBOnlyWeld.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBOnlyWeld.Location = New System.Drawing.Point(1403, 13)
        ChkBOnlyWeld.Name = "ChkBOnlyWeld"
        ChkBOnlyWeld.Size = New Size(144, 48)
        ChkBOnlyWeld.TabIndex = 19
        ChkBOnlyWeld.Text = "仅焊接件"
        ChkBOnlyWeld.UseVisualStyleBackColor = True
        ' 
        ' ChkBOnlySheet
        ' 
        ChkBOnlySheet.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ChkBOnlySheet.AutoSize = True
        ChkBOnlySheet.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBOnlySheet.Location = New System.Drawing.Point(1253, 13)
        ChkBOnlySheet.Name = "ChkBOnlySheet"
        ChkBOnlySheet.Size = New Size(144, 48)
        ChkBOnlySheet.TabIndex = 18
        ChkBOnlySheet.Text = "仅钣金件"
        ChkBOnlySheet.UseVisualStyleBackColor = True
        ' 
        ' BtnSelectAll
        ' 
        BtnSelectAll.BackColor = Drawing.Color.Transparent
        BtnSelectAll.FlatAppearance.BorderSize = 0
        BtnSelectAll.FlatStyle = FlatStyle.Flat
        BtnSelectAll.Font = New Font("微软雅黑", 16F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnSelectAll.ForeColor = SystemColors.ActiveCaptionText
        BtnSelectAll.Image = CType(resources.GetObject("BtnSelectAll.Image"), Image)
        BtnSelectAll.ImageAlign = ContentAlignment.MiddleLeft
        BtnSelectAll.Location = New System.Drawing.Point(13, 13)
        BtnSelectAll.Name = "BtnSelectAll"
        BtnSelectAll.Size = New Size(214, 48)
        BtnSelectAll.TabIndex = 11
        BtnSelectAll.Text = "Bom工具"
        BtnSelectAll.UseVisualStyleBackColor = False
        ' 
        ' ChkBOnlyAssem
        ' 
        ChkBOnlyAssem.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ChkBOnlyAssem.AutoSize = True
        ChkBOnlyAssem.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBOnlyAssem.Location = New System.Drawing.Point(1103, 13)
        ChkBOnlyAssem.Name = "ChkBOnlyAssem"
        ChkBOnlyAssem.Size = New Size(144, 48)
        ChkBOnlyAssem.TabIndex = 17
        ChkBOnlyAssem.Text = "仅部件"
        ChkBOnlyAssem.UseVisualStyleBackColor = True
        ' 
        ' BtnExPort
        ' 
        BtnExPort.BackColor = Drawing.Color.Transparent
        BtnExPort.FlatStyle = FlatStyle.Flat
        BtnExPort.Font = New Font("微软雅黑", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(134))
        BtnExPort.Location = New System.Drawing.Point(233, 13)
        BtnExPort.Name = "BtnExPort"
        BtnExPort.Size = New Size(209, 48)
        BtnExPort.TabIndex = 12
        BtnExPort.Text = "Excel导出"
        BtnExPort.TextImageRelation = TextImageRelation.ImageAboveText
        BtnExPort.UseVisualStyleBackColor = False
        ' 
        ' ChkBOnlyPart
        ' 
        ChkBOnlyPart.AutoSize = True
        ChkBOnlyPart.Dock = DockStyle.Fill
        ChkBOnlyPart.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBOnlyPart.Location = New System.Drawing.Point(953, 13)
        ChkBOnlyPart.Name = "ChkBOnlyPart"
        ChkBOnlyPart.Size = New Size(144, 48)
        ChkBOnlyPart.TabIndex = 14
        ChkBOnlyPart.Text = "仅零件"
        ChkBOnlyPart.UseVisualStyleBackColor = True
        ' 
        ' ChkBMergePartNumber
        ' 
        ChkBMergePartNumber.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ChkBMergePartNumber.AutoSize = True
        ChkBMergePartNumber.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBMergePartNumber.Location = New System.Drawing.Point(803, 13)
        ChkBMergePartNumber.Name = "ChkBMergePartNumber"
        ChkBMergePartNumber.Size = New Size(144, 48)
        ChkBMergePartNumber.TabIndex = 15
        ChkBMergePartNumber.Text = "零件代号合并"
        ChkBMergePartNumber.UseVisualStyleBackColor = True
        ' 
        ' ChkBStanPart
        ' 
        ChkBStanPart.Anchor = AnchorStyles.Left
        ChkBStanPart.AutoSize = True
        ChkBStanPart.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBStanPart.Location = New System.Drawing.Point(453, 23)
        ChkBStanPart.Name = "ChkBStanPart"
        ChkBStanPart.Size = New Size(126, 28)
        ChkBStanPart.TabIndex = 13
        ChkBStanPart.Text = "隐藏标准件"
        ChkBStanPart.UseVisualStyleBackColor = True
        ' 
        ' CmBoxTheme
        ' 
        CmBoxTheme.Anchor = AnchorStyles.None
        CmBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList
        CmBoxTheme.FlatStyle = FlatStyle.Flat
        CmBoxTheme.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CmBoxTheme.FormattingEnabled = True
        CmBoxTheme.ItemHeight = 24
        CmBoxTheme.Items.AddRange(New Object() {"紫色主题", "蓝色主题", "绿色主题", "红色主题", "橙色主题", "粉色主题", "🌈 呼吸灯", "🌙 暗黑主题", "🎨 自定义主题"})
        CmBoxTheme.Location = New System.Drawing.Point(1548, 21)
        CmBoxTheme.Name = "CmBoxTheme"
        CmBoxTheme.Size = New Size(176, 32)
        CmBoxTheme.TabIndex = 21
        ' 
        ' CmBoxFunction
        ' 
        CmBoxFunction.Anchor = AnchorStyles.None
        CmBoxFunction.DropDownStyle = ComboBoxStyle.DropDownList
        CmBoxFunction.FlatStyle = FlatStyle.Flat
        CmBoxFunction.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CmBoxFunction.FormattingEnabled = True
        CmBoxFunction.ItemHeight = 24
        CmBoxFunction.Items.AddRange(New Object() {"⚙ 功能", "[-] 批量删除属性", "✏ 自定义属性改名", "🔄 属性字符替换", "🔢 写入图号", "📄 属性写入工程图（文字）", "🖨 打印装配顺序工程图", "📤 按属性导出PDF/DWG", "🔗 属性拼接写入", "📊 BOM数量统计写入"})
        CmBoxFunction.Location = New System.Drawing.Point(1342, 21)
        CmBoxFunction.Name = "CmBoxFunction"
        CmBoxFunction.Size = New Size(167, 32)
        CmBoxFunction.TabIndex = 20
        ' 
        ' ChkBRefePart
        ' 
        ChkBRefePart.AutoSize = True
        ChkBRefePart.Dock = DockStyle.Fill
        ChkBRefePart.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ChkBRefePart.Location = New System.Drawing.Point(603, 13)
        ChkBRefePart.Name = "ChkBRefePart"
        ChkBRefePart.Size = New Size(194, 48)
        ChkBRefePart.TabIndex = 16
        ChkBRefePart.Text = "隐藏虚拟及参考"
        ChkBRefePart.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(Panel1, 0, 0)
        TableLayoutPanel1.Controls.Add(SplitContainer1, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 80F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(1762, 963)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.FixedPanel = FixedPanel.Panel2
        SplitContainer1.Location = New System.Drawing.Point(3, 83)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(TableLayoutPanel2)
        SplitContainer1.Panel1MinSize = 800
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.AllowDrop = True
        SplitContainer1.Panel2.AutoScroll = True
        SplitContainer1.Panel2.BackColor = SystemColors.AppWorkspace
        SplitContainer1.Panel2.Controls.Add(Panel2)
        SplitContainer1.Panel2MinSize = 270
        SplitContainer1.Size = New Size(1756, 877)
        SplitContainer1.SplitterDistance = 1353
        SplitContainer1.TabIndex = 1
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 1
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.Controls.Add(DGViewBomList, 0, 0)
        TableLayoutPanel2.Controls.Add(Panel3, 0, 1)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 2
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Absolute, 120F))
        TableLayoutPanel2.Size = New Size(1353, 877)
        TableLayoutPanel2.TabIndex = 0
        ' 
        ' DGViewBomList
        ' 
        DGViewBomList.AllowUserToDeleteRows = False
        DGViewBomList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGViewBomList.Columns.AddRange(New DataGridViewColumn() {Column1, Column2})
        DGViewBomList.Dock = DockStyle.Fill
        DGViewBomList.EnableHeadersVisualStyles = False
        DGViewBomList.Location = New System.Drawing.Point(3, 3)
        DGViewBomList.Name = "DGViewBomList"
        DGViewBomList.RowHeadersVisible = False
        DGViewBomList.RowHeadersWidth = 62
        DGViewBomList.RowTemplate.Height = 28
        DGViewBomList.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGViewBomList.Size = New Size(1347, 751)
        DGViewBomList.TabIndex = 0
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "序号"
        Column1.MinimumWidth = 8
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.Width = 50
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "类型"
        Column2.MinimumWidth = 8
        Column2.Name = "Column2"
        Column2.Width = 150
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(BtnRefreshExcel)
        Panel3.Controls.Add(Label3)
        Panel3.Controls.Add(FlowLayoutPanel1)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New System.Drawing.Point(3, 760)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1347, 114)
        Panel3.TabIndex = 1
        ' 
        ' BtnRefreshExcel
        ' 
        BtnRefreshExcel.Dock = DockStyle.Right
        BtnRefreshExcel.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        BtnRefreshExcel.Location = New System.Drawing.Point(1145, 60)
        BtnRefreshExcel.Name = "BtnRefreshExcel"
        BtnRefreshExcel.Size = New Size(202, 54)
        BtnRefreshExcel.TabIndex = 2
        BtnRefreshExcel.Text = "刷新全表格"
        BtnRefreshExcel.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(17, 91)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 25)
        Label3.TabIndex = 1
        Label3.Text = "Label3"
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(Label1)
        FlowLayoutPanel1.Controls.Add(NumericUpDown1)
        FlowLayoutPanel1.Controls.Add(Button2)
        FlowLayoutPanel1.Controls.Add(Splitter1)
        FlowLayoutPanel1.Controls.Add(Label2)
        FlowLayoutPanel1.Controls.Add(NumericUpDown2)
        FlowLayoutPanel1.Controls.Add(Button3)
        FlowLayoutPanel1.Controls.Add(Splitter2)
        FlowLayoutPanel1.Controls.Add(BtnExcFill)
        FlowLayoutPanel1.Controls.Add(Splitter3)
        FlowLayoutPanel1.Dock = DockStyle.Top
        FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Padding = New Padding(10, 3, 0, 0)
        FlowLayoutPanel1.Size = New Size(1347, 60)
        FlowLayoutPanel1.TabIndex = 0
        FlowLayoutPanel1.TabStop = True
        FlowLayoutPanel1.WrapContents = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New System.Drawing.Point(13, 3)
        Label1.Name = "Label1"
        Label1.Size = New Size(64, 24)
        Label1.TabIndex = 0
        Label1.Text = "全行高"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Font = New Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, CByte(134))
        NumericUpDown1.Location = New System.Drawing.Point(83, 6)
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(121, 31)
        NumericUpDown1.TabIndex = 1
        NumericUpDown1.TextAlign = HorizontalAlignment.Right
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button2.Location = New System.Drawing.Point(210, 6)
        Button2.Name = "Button2"
        Button2.Size = New Size(84, 43)
        Button2.TabIndex = 2
        Button2.Text = "应用"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Splitter1
        ' 
        Splitter1.Location = New System.Drawing.Point(300, 6)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New Size(3, 43)
        Splitter1.TabIndex = 3
        Splitter1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Fill
        Label2.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New System.Drawing.Point(309, 3)
        Label2.Name = "Label2"
        Label2.Size = New Size(64, 49)
        Label2.TabIndex = 4
        Label2.Text = "全列宽"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Font = New Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, CByte(134))
        NumericUpDown2.ForeColor = SystemColors.WindowText
        NumericUpDown2.Location = New System.Drawing.Point(379, 6)
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.Size = New Size(115, 31)
        NumericUpDown2.TabIndex = 5
        NumericUpDown2.TextAlign = HorizontalAlignment.Center
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button3.Location = New System.Drawing.Point(500, 6)
        Button3.Name = "Button3"
        Button3.Size = New Size(107, 43)
        Button3.TabIndex = 6
        Button3.Text = "应用"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Splitter2
        ' 
        Splitter2.Location = New System.Drawing.Point(613, 6)
        Splitter2.Name = "Splitter2"
        Splitter2.Size = New Size(3, 43)
        Splitter2.TabIndex = 7
        Splitter2.TabStop = False
        ' 
        ' BtnExcFill
        ' 
        BtnExcFill.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        BtnExcFill.Location = New System.Drawing.Point(622, 6)
        BtnExcFill.Name = "BtnExcFill"
        BtnExcFill.Size = New Size(146, 43)
        BtnExcFill.TabIndex = 8
        BtnExcFill.Text = "全部自适应"
        BtnExcFill.UseVisualStyleBackColor = True
        ' 
        ' Splitter3
        ' 
        Splitter3.Location = New System.Drawing.Point(774, 6)
        Splitter3.Name = "Splitter3"
        Splitter3.Size = New Size(3, 43)
        Splitter3.TabIndex = 9
        Splitter3.TabStop = False
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Drawing.Color.White
        Panel2.Controls.Add(TableLayoutPanel3)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(399, 877)
        Panel2.TabIndex = 0
        ' 
        ' TableLayoutPanel3
        ' 
        TableLayoutPanel3.BackColor = SystemColors.AppWorkspace
        TableLayoutPanel3.ColumnCount = 1
        TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel3.Controls.Add(Panel4, 0, 0)
        TableLayoutPanel3.Controls.Add(DGViewPropertyList, 0, 1)
        TableLayoutPanel3.Controls.Add(TableLayoutPanel4, 0, 2)
        TableLayoutPanel3.Dock = DockStyle.Fill
        TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel3.Name = "TableLayoutPanel3"
        TableLayoutPanel3.RowCount = 3
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Absolute, 200F))
        TableLayoutPanel3.Size = New Size(399, 877)
        TableLayoutPanel3.TabIndex = 0
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(Label4)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New System.Drawing.Point(3, 3)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(393, 34)
        Panel4.TabIndex = 0
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Dock = DockStyle.Fill
        Label4.Font = New Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New System.Drawing.Point(0, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(72, 27)
        Label4.TabIndex = 0
        Label4.Text = "列管理"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' DGViewPropertyList
        ' 
        DGViewPropertyList.AllowUserToAddRows = False
        DGViewPropertyList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGViewPropertyList.Columns.AddRange(New DataGridViewColumn() {Column5, Column6})
        DGViewPropertyList.Dock = DockStyle.Fill
        DGViewPropertyList.Location = New System.Drawing.Point(3, 43)
        DGViewPropertyList.Name = "DGViewPropertyList"
        DGViewPropertyList.RowHeadersVisible = False
        DGViewPropertyList.RowHeadersWidth = 62
        DGViewPropertyList.RowTemplate.Height = 28
        DGViewPropertyList.Size = New Size(393, 631)
        DGViewPropertyList.TabIndex = 1
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "属性值"
        Column5.MinimumWidth = 8
        Column5.Name = "Column5"
        Column5.Width = 150
        ' 
        ' Column6
        ' 
        Column6.HeaderText = "清除"
        Column6.MinimumWidth = 8
        Column6.Name = "Column6"
        Column6.Width = 150
        ' 
        ' TableLayoutPanel4
        ' 
        TableLayoutPanel4.ColumnCount = 1
        TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel4.Controls.Add(FlowLayoutPanel2, 0, 0)
        TableLayoutPanel4.Controls.Add(Label5, 0, 1)
        TableLayoutPanel4.Controls.Add(CmBoxPropertyList, 0, 2)
        TableLayoutPanel4.Controls.Add(BtnAddPropety, 0, 3)
        TableLayoutPanel4.Dock = DockStyle.Fill
        TableLayoutPanel4.Location = New System.Drawing.Point(3, 680)
        TableLayoutPanel4.Name = "TableLayoutPanel4"
        TableLayoutPanel4.Padding = New Padding(10, 8, 10, 10)
        TableLayoutPanel4.RowCount = 4
        TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Absolute, 60F))
        TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        TableLayoutPanel4.Size = New Size(393, 194)
        TableLayoutPanel4.TabIndex = 2
        ' 
        ' FlowLayoutPanel2
        ' 
        FlowLayoutPanel2.Controls.Add(BtnUp)
        FlowLayoutPanel2.Controls.Add(BtnDown)
        FlowLayoutPanel2.Dock = DockStyle.Fill
        FlowLayoutPanel2.Location = New System.Drawing.Point(13, 11)
        FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        FlowLayoutPanel2.Size = New Size(367, 34)
        FlowLayoutPanel2.TabIndex = 0
        ' 
        ' BtnUp
        ' 
        BtnUp.Location = New System.Drawing.Point(3, 3)
        BtnUp.Name = "BtnUp"
        BtnUp.Size = New Size(120, 30)
        BtnUp.TabIndex = 0
        BtnUp.Text = "▲"
        BtnUp.UseCompatibleTextRendering = True
        BtnUp.UseVisualStyleBackColor = True
        ' 
        ' BtnDown
        ' 
        BtnDown.Location = New System.Drawing.Point(129, 3)
        BtnDown.Name = "BtnDown"
        BtnDown.Size = New Size(120, 30)
        BtnDown.TabIndex = 1
        BtnDown.Text = "▼ "
        BtnDown.UseCompatibleTextRendering = True
        BtnDown.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Dock = DockStyle.Fill
        Label5.Location = New System.Drawing.Point(13, 48)
        Label5.Name = "Label5"
        Label5.Size = New Size(367, 40)
        Label5.TabIndex = 1
        Label5.Text = "添加属性列"
        ' 
        ' CmBoxPropertyList
        ' 
        CmBoxPropertyList.Dock = DockStyle.Fill
        CmBoxPropertyList.FlatStyle = FlatStyle.Flat
        CmBoxPropertyList.Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CmBoxPropertyList.FormattingEnabled = True
        CmBoxPropertyList.Location = New System.Drawing.Point(13, 91)
        CmBoxPropertyList.Name = "CmBoxPropertyList"
        CmBoxPropertyList.Size = New Size(367, 32)
        CmBoxPropertyList.TabIndex = 2
        ' 
        ' BtnAddPropety
        ' 
        BtnAddPropety.Dock = DockStyle.Fill
        BtnAddPropety.Font = New Font("微软雅黑", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnAddPropety.Location = New System.Drawing.Point(13, 151)
        BtnAddPropety.Name = "BtnAddPropety"
        BtnAddPropety.Size = New Size(367, 44)
        BtnAddPropety.TabIndex = 3
        BtnAddPropety.Text = "+ 添加列"
        BtnAddPropety.UseVisualStyleBackColor = True
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "Column3"
        Column3.MinimumWidth = 8
        Column3.Name = "Column3"
        Column3.Width = 150
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Column4"
        Column4.MinimumWidth = 8
        Column4.Name = "Column4"
        Column4.Width = 150
        ' 
        ' FormBomTool
        ' 
        AutoScaleDimensions = New SizeF(144F, 144F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(1762, 963)
        Controls.Add(TableLayoutPanel1)
        MinimumSize = New Size(940, 620)
        Name = "FormBomTool"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Bom工具"
        Panel1.ResumeLayout(False)
        TableLayoutPanel5.ResumeLayout(False)
        TableLayoutPanel5.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        CType(DGViewBomList, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        FlowLayoutPanel1.PerformLayout()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        TableLayoutPanel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        CType(DGViewPropertyList, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel4.ResumeLayout(False)
        TableLayoutPanel4.PerformLayout()
        FlowLayoutPanel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents CmBoxTheme As ComboBox
    Friend WithEvents CmBoxFunction As ComboBox
    Friend WithEvents ChkBOnlyWeld As CheckBox
    Friend WithEvents ChkBOnlySheet As CheckBox
    Friend WithEvents ChkBOnlyAssem As CheckBox
    Friend WithEvents ChkBRefePart As CheckBox
    Friend WithEvents ChkBMergePartNumber As CheckBox
    Friend WithEvents ChkBOnlyPart As CheckBox
    Friend WithEvents ChkBStanPart As CheckBox
    Friend WithEvents BtnExPort As Button
    Friend WithEvents BtnSelectAll As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents DGViewBomList As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Button2 As Button
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents Button3 As Button
    Friend WithEvents Splitter2 As Splitter
    Friend WithEvents BtnExcFill As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Splitter3 As Splitter
    Friend WithEvents BtnRefreshExcel As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents DGViewPropertyList As DataGridView
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents BtnUp As Button
    Friend WithEvents BtnDown As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents CmBoxPropertyList As ComboBox
    Friend WithEvents BtnAddPropety As Button
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewButtonColumn
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
End Class
