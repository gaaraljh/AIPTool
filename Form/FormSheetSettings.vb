Imports Inventor
Imports System.Windows.Forms

''' <summary>
''' 图纸设置窗体：读取并设置当前图纸（或所有图纸）的名称、纸张尺寸、宽高、标题栏、方向与打印/计数选项。
''' </summary>
Public Class FormSheetSettings

    ' 是否处于"所有图纸"模式
    Private m_isAllSheetsMode As Boolean = False

    ' 纸张尺寸显示名 → Inventor 图纸尺寸枚举
    Private ReadOnly m_sheetSizeMapping As New Dictionary(Of String, DrawingSheetSizeEnum) From {
        {"ANSI A", DrawingSheetSizeEnum.kADrawingSheetSize},
        {"ANSI B", DrawingSheetSizeEnum.kBDrawingSheetSize},
        {"ANSI C", DrawingSheetSizeEnum.kCDrawingSheetSize},
        {"ANSI D", DrawingSheetSizeEnum.kDDrawingSheetSize},
        {"ANSI E", DrawingSheetSizeEnum.kEDrawingSheetSize},
        {"ANSI F", DrawingSheetSizeEnum.kFDrawingSheetSize},
        {"A4", DrawingSheetSizeEnum.kA4DrawingSheetSize},
        {"A3", DrawingSheetSizeEnum.kA3DrawingSheetSize},
        {"A2", DrawingSheetSizeEnum.kA2DrawingSheetSize},
        {"A1", DrawingSheetSizeEnum.kA1DrawingSheetSize},
        {"A0", DrawingSheetSizeEnum.kA0DrawingSheetSize},
        {"Arch A (9x12)", DrawingSheetSizeEnum.k9x12InDrawingSheetSize},
        {"Arch B (12x18)", DrawingSheetSizeEnum.k12x18InDrawingSheetSize},
        {"Arch C (18x24)", DrawingSheetSizeEnum.k18x24InDrawingSheetSize},
        {"Arch D (24x36)", DrawingSheetSizeEnum.k24x36InDrawingSheetSize},
        {"Arch E (36x48)", DrawingSheetSizeEnum.k36x48InDrawingSheetSize},
        {"Arch E1 (30x42)", DrawingSheetSizeEnum.k30x42InDrawingSheetSize},
        {"Custom", DrawingSheetSizeEnum.kCustomDrawingSheetSize}
    }

    ' 标准图纸尺寸对应的短边×长边（单位 mm）。横向=宽长高短，竖向=宽短高长
    Private ReadOnly m_sheetSizeMmMapping As New Dictionary(Of String, Tuple(Of Double, Double)) From {
        {"ANSI A", Tuple.Create(215.9, 279.4)},
        {"ANSI B", Tuple.Create(279.4, 431.8)},
        {"ANSI C", Tuple.Create(431.8, 558.8)},
        {"ANSI D", Tuple.Create(558.8, 863.6)},
        {"ANSI E", Tuple.Create(863.6, 1117.6)},
        {"ANSI F", Tuple.Create(711.2, 1016.0)},
        {"A4", Tuple.Create(210.0, 297.0)},
        {"A3", Tuple.Create(297.0, 420.0)},
        {"A2", Tuple.Create(420.0, 594.0)},
        {"A1", Tuple.Create(594.0, 841.0)},
        {"A0", Tuple.Create(841.0, 1189.0)},
        {"Arch A (9x12)", Tuple.Create(228.6, 304.8)},
        {"Arch B (12x18)", Tuple.Create(304.8, 457.2)},
        {"Arch C (18x24)", Tuple.Create(457.2, 609.6)},
        {"Arch D (24x36)", Tuple.Create(609.6, 914.4)},
        {"Arch E (36x48)", Tuple.Create(914.4, 1219.2)},
        {"Arch E1 (30x42)", Tuple.Create(762.0, 1066.8)}
    }

    ' 加载图纸真实信息期间为 True，抑制"尺寸/方向变更→回填宽高"事件，避免覆盖真实值
    Private m_loadingSheetInfo As Boolean = False

    ''' <summary>窗体加载：填充下拉框并读取当前图纸信息</summary>
    Private Sub FormSheetSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            buttonClearSheetSelection.Visible = False
            Me.Icon = My.Resources.GLTool48
            pictureBoxOrientationPreview.SizeMode = PictureBoxSizeMode.CenterImage
            InitializeSheetSizeComboBox()
            SetCurrentSheetMode(True)
            LoadCurrentSheetInfoToControls()
            SetPreviewImageByTitleBlockLocation(GetSelectedTitleBlockLocation())
        Catch ex As Exception
            MessageBox.Show("图纸设置加载失败：" & ex.Message, "图纸设置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>填充纸张尺寸下拉列表</summary>
    Private Sub InitializeSheetSizeComboBox()
        comboBoxSheetSize.Items.Clear()
        For Each strSizeName As String In m_sheetSizeMapping.Keys
            comboBoxSheetSize.Items.Add(strSizeName)
        Next
    End Sub

    ''' <summary>
    ''' 切换纸张尺寸时，宽高框自动显示该尺寸的标准值（单位 mm）。
    ''' 横向：宽=长边、高=短边；竖向：宽=短边、高=长边。Custom 不自动填。
    ''' </summary>
    Private Sub comboBoxSheetSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxSheetSize.SelectedIndexChanged
        If m_loadingSheetInfo Then Exit Sub
        UpdateNumericEnabledBySize()
        ApplyStandardSheetSizeToNumeric()
    End Sub

    ''' <summary>宽高框是否可编辑：仅选中 Custom 时可编辑，其余标准尺寸一律只读</summary>
    Private Sub UpdateNumericEnabledBySize()
        Dim strSizeName As String = ""
        If comboBoxSheetSize.SelectedItem IsNot Nothing Then strSizeName = comboBoxSheetSize.SelectedItem.ToString()
        Dim blnEditable As Boolean = (strSizeName = "Custom")

        numericUpDownSheetWidth.Enabled = blnEditable
        numericUpDownSheetHeight.Enabled = blnEditable
        labelSheetWidth.Enabled = blnEditable
        labelSheetHeight.Enabled = blnEditable
        labelSheetWidthUnit.Enabled = blnEditable
        labelSheetHeightUnit.Enabled = blnEditable
    End Sub

    ''' <summary>切换图纸方向时，同样按标准尺寸重算宽高显示</summary>
    Private Sub RadioButtonOrientation_CheckedChanged(sender As Object, e As EventArgs) Handles _
        radioButtonOrientationPortrait.CheckedChanged, radioButtonOrientationLandscape.CheckedChanged
        If m_loadingSheetInfo Then Exit Sub
        ApplyStandardSheetSizeToNumeric()
    End Sub

    ''' <summary>按当前选中的标准尺寸与方向，把宽高值填入 NumericUpDown</summary>
    Private Sub ApplyStandardSheetSizeToNumeric()
        Dim strSizeName As String = ""
        If comboBoxSheetSize.SelectedItem IsNot Nothing Then strSizeName = comboBoxSheetSize.SelectedItem.ToString()
        If Not m_sheetSizeMmMapping.ContainsKey(strSizeName) Then Exit Sub   'Custom 或空：不自动改

        Dim dblShort As Double = m_sheetSizeMmMapping(strSizeName).Item1
        Dim dblLong As Double = m_sheetSizeMmMapping(strSizeName).Item2
        Dim dblWidth As Double = dblLong
        Dim dblHeight As Double = dblShort
        If radioButtonOrientationPortrait.Checked Then
            dblWidth = dblShort
            dblHeight = dblLong
        End If

        Dim decWidth As Decimal = ConvertToDecimal(dblWidth)
        Dim decHeight As Decimal = ConvertToDecimal(dblHeight)
        numericUpDownSheetWidth.Maximum = Math.Max(numericUpDownSheetWidth.Maximum, decWidth)
        numericUpDownSheetHeight.Maximum = Math.Max(numericUpDownSheetHeight.Maximum, decHeight)
        numericUpDownSheetWidth.Value = decWidth
        numericUpDownSheetHeight.Value = decHeight
    End Sub

    ''' <summary>切换到"当前图纸"模式</summary>
    Private Sub ButtonSelectCurrentSheet_Click(sender As Object, e As EventArgs) Handles buttonSelectCurrentSheet.Click
        Try
            buttonClearSheetSelection.Visible = False
            SetCurrentSheetMode(True)
            LoadCurrentSheetInfoToControls()
            SetPreviewImageByTitleBlockLocation(GetSelectedTitleBlockLocation())
        Catch ex As Exception
            MessageBox.Show("读取当前图纸信息失败：" & ex.Message, "图纸设置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>切换到"所有图纸"模式</summary>
    Private Sub ButtonSelectAllSheets_Click(sender As Object, e As EventArgs) Handles buttonSelectAllSheets.Click
        Try
            buttonClearSheetSelection.Visible = True
            SetCurrentSheetMode(False)
            '所有图纸模式：标题栏与 Size 从图纸资源加载对应值（不清空），
            '名称/宽高/方向/选项做模式内重置
            Dim oDrawingDocument As DrawingDocument = TryCast(ThisApplication.ActiveDocument, DrawingDocument)
            If oDrawingDocument IsNot Nothing AndAlso oDrawingDocument.ActiveSheet IsNot Nothing Then
                Dim oActiveSheet As Sheet = oDrawingDocument.ActiveSheet

                '标题栏：加载文档中的标题栏定义列表，并选中当前图纸的标题栏
                LoadTitleBlockListToComboBox(oDrawingDocument, oActiveSheet)

                'Size：按当前图纸尺寸选中（作为应用到所有图纸的默认值）
                Dim strSizeName As String = GetSheetSizeDisplayName(oActiveSheet.Size)
                If comboBoxSheetSize.Items.Contains(strSizeName) Then
                    comboBoxSheetSize.SelectedItem = strSizeName
                Else
                    comboBoxSheetSize.SelectedIndex = -1
                End If
            Else
                LoadTitleBlockListToComboBox(Nothing, Nothing)
                comboBoxSheetSize.SelectedIndex = -1
            End If

            '名称/方向/选项重置（标题栏与 Size 保留上面加载的值；宽高由尺寸变更事件回填标准值）
            textBoxSheetName.Clear()
            radioButtonOrientationPortrait.Checked = False
            radioButtonOrientationLandscape.Checked = False
            checkBoxExcludeFromCount.Checked = False
            checkBoxExcludeFromPrint.Checked = False
            radioButtonOrientationBottomRight.Checked = True
            SetPreviewImageByTitleBlockLocation(GetSelectedTitleBlockLocation())
            ApplyStandardSheetSizeToNumeric()
        Catch ex As Exception
            MessageBox.Show("切换模式失败：" & ex.Message, "图纸设置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>"清除选择"按钮：清空所有选择</summary>
    Private Sub ButtonClearSheetSelection_Click(sender As Object, e As EventArgs) Handles buttonClearSheetSelection.Click
        Try
            ClearSheetInfoControls()
        Catch ex As Exception
            MessageBox.Show("清除选择失败：" & ex.Message, "图纸设置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 切换界面模式并更新控件状态：
    ''' 当前图纸界面——图纸名称/Size/标题栏/方向/选项可编辑，高度宽度禁用；
    ''' 所有图纸界面——显示"清除选择"，图纸名称与选项禁用，Size/标题栏/方向可编辑；
    ''' 高度/宽度在两种模式下都禁用（灰色只读），仅随所选 Standard Size 自动显示对应尺寸值。
    ''' </summary>
    Private Sub SetCurrentSheetMode(ByVal isCurrentSheetMode As Boolean)
        m_isAllSheetsMode = Not isCurrentSheetMode
        buttonClearSheetSelection.Visible = Not isCurrentSheetMode

        '图纸名称：仅当前图纸模式可编辑
        textBoxSheetName.Enabled = isCurrentSheetMode

        'Size / 标题栏 / 图纸方向：两种模式均可编辑
        comboBoxSheetSize.Enabled = True
        comboBoxTitleBlock.Enabled = True
        groupBoxSheetOrientation.Enabled = True

        '高度/宽度：仅 Custom 可编辑，标准尺寸只读（由 UpdateNumericEnabledBySize 统一控制）
        UpdateNumericEnabledBySize()

        '选项：仅当前图纸模式可编辑
        groupBoxOptions.Enabled = isCurrentSheetMode
    End Sub

    ''' <summary>读取当前图纸信息并填入控件</summary>
    Private Sub LoadCurrentSheetInfoToControls()
        Dim oDrawingDocument As DrawingDocument = TryCast(ThisApplication.ActiveDocument, DrawingDocument)
        If oDrawingDocument Is Nothing Then Exit Sub
        If oDrawingDocument.ActiveSheet Is Nothing Then Exit Sub

        m_loadingSheetInfo = True
        Try
            LoadCurrentSheetInfoToControlsCore(oDrawingDocument, oDrawingDocument.ActiveSheet)
        Finally
            m_loadingSheetInfo = False
        End Try
        UpdateNumericEnabledBySize()
    End Sub

    Private Sub LoadCurrentSheetInfoToControlsCore(ByVal oDrawingDocument As DrawingDocument, ByVal oActiveSheet As Sheet)

        '图纸名称
        textBoxSheetName.Text = oActiveSheet.Name

        '图纸尺寸：按枚举反查显示名。两个下拉框都是 DropDownList，必须用 SelectedItem 赋值，
        '         用 .Text 赋不存在的值不会选中（显示为空）
        Dim strSizeName As String = GetSheetSizeDisplayName(oActiveSheet.Size)
        If comboBoxSheetSize.Items.Contains(strSizeName) Then
            comboBoxSheetSize.SelectedItem = strSizeName
        Else
            comboBoxSheetSize.SelectedIndex = -1
        End If

        '宽高（Inventor 内部单位为 cm，显示为 mm）。先扩展 NumericUpDown 上限再赋值，
        '避免图纸宽度超过默认上限时抛 ArgumentOutOfRangeException
        Dim decWidth As Decimal = ConvertToDecimal(oActiveSheet.Width * 10.0)
        Dim decHeight As Decimal = ConvertToDecimal(oActiveSheet.Height * 10.0)
        numericUpDownSheetWidth.Maximum = Math.Max(numericUpDownSheetWidth.Maximum, decWidth)
        numericUpDownSheetHeight.Maximum = Math.Max(numericUpDownSheetHeight.Maximum, decHeight)
        numericUpDownSheetWidth.Value = decWidth
        numericUpDownSheetHeight.Value = decHeight

        '标题栏列表
        LoadTitleBlockListToComboBox(oDrawingDocument, oActiveSheet)

        '方向
        If oActiveSheet.Orientation = PageOrientationTypeEnum.kPortraitPageOrientation Then
            radioButtonOrientationPortrait.Checked = True
        Else
            radioButtonOrientationLandscape.Checked = True
        End If
    End Sub

    ''' <summary>从图纸尺寸枚举获取显示名</summary>
    Private Function GetSheetSizeDisplayName(ByVal sheetSize As DrawingSheetSizeEnum) As String
        For Each kvp As KeyValuePair(Of String, DrawingSheetSizeEnum) In m_sheetSizeMapping
            If kvp.Value = sheetSize Then Return kvp.Key
        Next
        Return "Custom"
    End Function

    ''' <summary>安全转换为 Decimal</summary>
    Private Function ConvertToDecimal(ByVal value As Double) As Decimal
        If value < 0 Then Return 0D
        If value > 10000.0 Then Return 10000D
        Return CDec(value)
    End Function

    ''' <summary>加载标题栏定义到下拉框，并选中当前图纸的标题栏</summary>
    Private Sub LoadTitleBlockListToComboBox(ByVal drawingDocument As DrawingDocument, ByVal activeSheet As Sheet)
        comboBoxTitleBlock.Items.Clear()
        If drawingDocument Is Nothing Then Exit Sub
        For Each oTitleBlockDefinition As TitleBlockDefinition In drawingDocument.TitleBlockDefinitions
            comboBoxTitleBlock.Items.Add(oTitleBlockDefinition.Name)
        Next
        If activeSheet Is Nothing Then Exit Sub
        Try
            If activeSheet.TitleBlock IsNot Nothing Then
                Dim strName As String = activeSheet.TitleBlock.Name
                If comboBoxTitleBlock.Items.Contains(strName) Then
                    comboBoxTitleBlock.SelectedItem = strName
                End If
            End If
        Catch
        End Try
    End Sub

    ''' <summary>清空选择/信息控件（保留标题栏定义列表，不清图纸名称）</summary>
    Private Sub ClearSheetInfoControls()
        textBoxSheetName.Clear()
        comboBoxSheetSize.SelectedIndex = -1
        numericUpDownSheetWidth.Value = 0
        numericUpDownSheetHeight.Value = 0
        '标题栏：不清空 Items，只清选中
        comboBoxTitleBlock.SelectedIndex = -1
        radioButtonOrientationPortrait.Checked = False
        radioButtonOrientationLandscape.Checked = False
        radioButtonOrientationBottomRight.Checked = True
        SetPreviewImageByTitleBlockLocation(GetSelectedTitleBlockLocation())
        checkBoxExcludeFromCount.Checked = False
        checkBoxExcludeFromPrint.Checked = False
    End Sub

    ''' <summary>应用设置</summary>
    Private Sub ButtonApply_Click(sender As Object, e As EventArgs) Handles buttonApply.Click
        Try
            Dim oDrawingDocument As DrawingDocument = TryCast(ThisApplication.ActiveDocument, DrawingDocument)
            If oDrawingDocument Is Nothing Then
                MessageBox.Show("当前没有打开工程图。", "图纸设置", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If m_isAllSheetsMode Then
                ApplySettingsToAllSheets(oDrawingDocument)
            Else
                ApplySettingsToSheet(oDrawingDocument, oDrawingDocument.ActiveSheet)
            End If

            oDrawingDocument.Update()
        Catch ex As Exception
            MessageBox.Show("应用图纸设置失败：" & ex.Message, "图纸设置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>应用设置到所有图纸</summary>
    Private Sub ApplySettingsToAllSheets(ByVal drawingDocument As DrawingDocument)
        For Each oSheet As Sheet In drawingDocument.Sheets
            ApplySettingsToSheet(drawingDocument, oSheet)
        Next
    End Sub

    ''' <summary>应用设置到单张图纸</summary>
    Private Sub ApplySettingsToSheet(ByVal drawingDocument As DrawingDocument, ByVal targetSheet As Sheet)
        If targetSheet Is Nothing Then Exit Sub

        '纸张尺寸：DropDownList 用 SelectedItem 读取（.Text 在无选中时为空，易读错）
        Dim strSizeName As String = ""
        If comboBoxSheetSize.SelectedItem IsNot Nothing Then strSizeName = comboBoxSheetSize.SelectedItem.ToString()
        If m_sheetSizeMapping.ContainsKey(strSizeName) Then
            targetSheet.Size = m_sheetSizeMapping(strSizeName)
        End If

        '方向：先改尺寸再改方向，避免横向 A3 被按纵向尺寸裁切
        If radioButtonOrientationPortrait.Checked Then
            targetSheet.Orientation = PageOrientationTypeEnum.kPortraitPageOrientation
        ElseIf radioButtonOrientationLandscape.Checked Then
            targetSheet.Orientation = PageOrientationTypeEnum.kLandscapePageOrientation
        End If

        'Custom：宽高框此时可编辑，把用户输入的 mm 值写回（Inventor 内部单位为 cm）
        If strSizeName = "Custom" Then
            If numericUpDownSheetWidth.Value > 0 Then targetSheet.Width = CDbl(numericUpDownSheetWidth.Value) / 10.0
            If numericUpDownSheetHeight.Value > 0 Then targetSheet.Height = CDbl(numericUpDownSheetHeight.Value) / 10.0
        End If

        '标题栏：DropDownList 用 SelectedItem 读取
        Dim strTitleBlockName As String = ""
        If comboBoxTitleBlock.SelectedItem IsNot Nothing Then strTitleBlockName = comboBoxTitleBlock.SelectedItem.ToString()
        If Not String.IsNullOrEmpty(strTitleBlockName) Then
            ApplyTitleBlockToSheet(drawingDocument, targetSheet, strTitleBlockName)
        End If

        targetSheet.Update()
    End Sub

    ''' <summary>获取当前选择的标题栏位置（四角单选）</summary>
    Private Function GetSelectedTitleBlockLocation() As TitleBlockLocationEnum
        If radioButtonOrientationTopLeft.Checked Then Return TitleBlockLocationEnum.kTopLeftPosition
        If radioButtonOrientationTopRight.Checked Then Return TitleBlockLocationEnum.kTopRightPosition
        If radioButtonOrientationBottomLeft.Checked Then Return TitleBlockLocationEnum.kBottomLeftPosition
        Return TitleBlockLocationEnum.kBottomRightPosition
    End Function

    ''' <summary>按标题栏位置切换预览图片（B=底/T=顶/L=左/R=右）</summary>
    Private Sub SetPreviewImageByTitleBlockLocation(ByVal location As TitleBlockLocationEnum)
        Select Case location
            Case TitleBlockLocationEnum.kTopLeftPosition
                pictureBoxOrientationPreview.Image = My.Resources.titleblock_TL
            Case TitleBlockLocationEnum.kTopRightPosition
                pictureBoxOrientationPreview.Image = My.Resources.titleblock_TR
            Case TitleBlockLocationEnum.kBottomLeftPosition
                pictureBoxOrientationPreview.Image = My.Resources.titleblock_BL
            Case Else
                pictureBoxOrientationPreview.Image = My.Resources.titleblock_BR
        End Select
    End Sub

    ''' <summary>四角位置单选切换时更新预览图片</summary>
    Private Sub RadioButtonTitleBlockLocation_CheckedChanged(sender As Object, e As EventArgs) Handles _
        radioButtonOrientationTopLeft.CheckedChanged, radioButtonOrientationTopRight.CheckedChanged,
        radioButtonOrientationBottomLeft.CheckedChanged, radioButtonOrientationBottomRight.CheckedChanged
        Try
            SetPreviewImageByTitleBlockLocation(GetSelectedTitleBlockLocation())
        Catch
        End Try
    End Sub

    ''' <summary>应用标题栏到图纸</summary>
    Private Sub ApplyTitleBlockToSheet(ByVal drawingDocument As DrawingDocument, ByVal targetSheet As Sheet, ByVal titleBlockName As String)
        Try
            If targetSheet.TitleBlock IsNot Nothing Then
                targetSheet.TitleBlock.Delete()
            End If
            Dim oTitleBlockDefinition As TitleBlockDefinition = drawingDocument.TitleBlockDefinitions.Item(titleBlockName)
            targetSheet.AddTitleBlock(oTitleBlockDefinition, GetSelectedTitleBlockLocation())
        Catch
        End Try
    End Sub

    ''' <summary>取消：关闭窗体</summary>
    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click
        Me.Close()
    End Sub

    Private Sub pictureBoxOrientationPreview_Click(sender As Object, e As EventArgs) Handles pictureBoxOrientationPreview.Click

    End Sub
End Class
