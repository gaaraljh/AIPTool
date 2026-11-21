<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSheetSettings
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSheetSettings))
        buttonSelectCurrentSheet = New Button()
        buttonSelectAllSheets = New Button()
        buttonClearSheetSelection = New Button()
        groupBoxSheetFormat = New GroupBox()
        labelSheetName = New Label()
        textBoxSheetName = New System.Windows.Forms.TextBox()
        labelSheetSize = New Label()
        comboBoxSheetSize = New ComboBox()
        labelSheetHeight = New Label()
        numericUpDownSheetHeight = New NumericUpDown()
        labelSheetHeightUnit = New Label()
        labelSheetWidth = New Label()
        numericUpDownSheetWidth = New NumericUpDown()
        labelSheetWidthUnit = New Label()
        labelTitleBlock = New Label()
        comboBoxTitleBlock = New ComboBox()
        groupBoxSheetOrientation = New GroupBox()
        panelTitleBlockPosition = New Panel()
        pictureBoxOrientationPreview = New PictureBox()
        radioButtonOrientationTopLeft = New RadioButton()
        radioButtonOrientationTopRight = New RadioButton()
        radioButtonOrientationBottomLeft = New RadioButton()
        radioButtonOrientationBottomRight = New RadioButton()
        radioButtonOrientationPortrait = New RadioButton()
        radioButtonOrientationLandscape = New RadioButton()
        groupBoxOptions = New GroupBox()
        checkBoxExcludeFromCount = New CheckBox()
        checkBoxExcludeFromPrint = New CheckBox()
        buttonApply = New Button()
        buttonCancel = New Button()
        groupBoxSheetFormat.SuspendLayout()
        CType(numericUpDownSheetHeight, ComponentModel.ISupportInitialize).BeginInit()
        CType(numericUpDownSheetWidth, ComponentModel.ISupportInitialize).BeginInit()
        groupBoxSheetOrientation.SuspendLayout()
        panelTitleBlockPosition.SuspendLayout()
        CType(pictureBoxOrientationPreview, ComponentModel.ISupportInitialize).BeginInit()
        groupBoxOptions.SuspendLayout()
        SuspendLayout()
        ' 
        ' buttonSelectCurrentSheet
        ' 
        buttonSelectCurrentSheet.Location = New System.Drawing.Point(40, 59)
        buttonSelectCurrentSheet.Margin = New Padding(4)
        buttonSelectCurrentSheet.Name = "buttonSelectCurrentSheet"
        buttonSelectCurrentSheet.Size = New Size(229, 47)
        buttonSelectCurrentSheet.TabIndex = 0
        buttonSelectCurrentSheet.Text = "当前图纸"
        buttonSelectCurrentSheet.UseVisualStyleBackColor = True
        ' 
        ' buttonSelectAllSheets
        ' 
        buttonSelectAllSheets.Location = New System.Drawing.Point(293, 59)
        buttonSelectAllSheets.Margin = New Padding(4)
        buttonSelectAllSheets.Name = "buttonSelectAllSheets"
        buttonSelectAllSheets.Size = New Size(229, 47)
        buttonSelectAllSheets.TabIndex = 1
        buttonSelectAllSheets.Text = "所有图纸"
        buttonSelectAllSheets.UseVisualStyleBackColor = True
        ' 
        ' buttonClearSheetSelection
        ' 
        buttonClearSheetSelection.Location = New System.Drawing.Point(714, 59)
        buttonClearSheetSelection.Margin = New Padding(4)
        buttonClearSheetSelection.Name = "buttonClearSheetSelection"
        buttonClearSheetSelection.Size = New Size(229, 47)
        buttonClearSheetSelection.TabIndex = 2
        buttonClearSheetSelection.Text = "清除选择"
        buttonClearSheetSelection.UseVisualStyleBackColor = True
        buttonClearSheetSelection.Visible = False
        ' 
        ' groupBoxSheetFormat
        ' 
        groupBoxSheetFormat.Controls.Add(labelSheetName)
        groupBoxSheetFormat.Controls.Add(textBoxSheetName)
        groupBoxSheetFormat.Controls.Add(labelSheetSize)
        groupBoxSheetFormat.Controls.Add(comboBoxSheetSize)
        groupBoxSheetFormat.Controls.Add(labelSheetHeight)
        groupBoxSheetFormat.Controls.Add(numericUpDownSheetHeight)
        groupBoxSheetFormat.Controls.Add(labelSheetHeightUnit)
        groupBoxSheetFormat.Controls.Add(labelSheetWidth)
        groupBoxSheetFormat.Controls.Add(numericUpDownSheetWidth)
        groupBoxSheetFormat.Controls.Add(labelSheetWidthUnit)
        groupBoxSheetFormat.Location = New System.Drawing.Point(40, 135)
        groupBoxSheetFormat.Margin = New Padding(4)
        groupBoxSheetFormat.Name = "groupBoxSheetFormat"
        groupBoxSheetFormat.Padding = New Padding(4)
        groupBoxSheetFormat.Size = New Size(407, 574)
        groupBoxSheetFormat.TabIndex = 3
        groupBoxSheetFormat.TabStop = False
        groupBoxSheetFormat.Text = "格式"
        ' 
        ' labelSheetName
        ' 
        labelSheetName.AutoSize = True
        labelSheetName.Location = New System.Drawing.Point(17, 41)
        labelSheetName.Margin = New Padding(4, 0, 4, 0)
        labelSheetName.Name = "labelSheetName"
        labelSheetName.Size = New Size(88, 25)
        labelSheetName.TabIndex = 0
        labelSheetName.Text = "图纸名称"
        ' 
        ' textBoxSheetName
        ' 
        textBoxSheetName.Location = New System.Drawing.Point(17, 74)
        textBoxSheetName.Margin = New Padding(4)
        textBoxSheetName.Name = "textBoxSheetName"
        textBoxSheetName.Size = New Size(298, 31)
        textBoxSheetName.TabIndex = 1
        ' 
        ' labelSheetSize
        ' 
        labelSheetSize.AutoSize = True
        labelSheetSize.Location = New System.Drawing.Point(17, 135)
        labelSheetSize.Margin = New Padding(4, 0, 4, 0)
        labelSheetSize.Name = "labelSheetSize"
        labelSheetSize.Size = New Size(43, 25)
        labelSheetSize.TabIndex = 2
        labelSheetSize.Text = "Size"
        ' 
        ' comboBoxSheetSize
        ' 
        comboBoxSheetSize.DropDownStyle = ComboBoxStyle.DropDownList
        comboBoxSheetSize.Location = New System.Drawing.Point(17, 168)
        comboBoxSheetSize.Margin = New Padding(4)
        comboBoxSheetSize.Name = "comboBoxSheetSize"
        comboBoxSheetSize.Size = New Size(298, 33)
        comboBoxSheetSize.TabIndex = 3
        ' 
        ' labelSheetHeight
        ' 
        labelSheetHeight.AutoSize = True
        labelSheetHeight.Location = New System.Drawing.Point(17, 235)
        labelSheetHeight.Margin = New Padding(4, 0, 4, 0)
        labelSheetHeight.Name = "labelSheetHeight"
        labelSheetHeight.Size = New Size(50, 25)
        labelSheetHeight.TabIndex = 4
        labelSheetHeight.Text = "高度"
        ' 
        ' numericUpDownSheetHeight
        ' 
        numericUpDownSheetHeight.DecimalPlaces = 2
        numericUpDownSheetHeight.Location = New System.Drawing.Point(17, 268)
        numericUpDownSheetHeight.Margin = New Padding(4)
        numericUpDownSheetHeight.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        numericUpDownSheetHeight.Name = "numericUpDownSheetHeight"
        numericUpDownSheetHeight.Size = New Size(200, 31)
        numericUpDownSheetHeight.TabIndex = 5
        ' 
        ' labelSheetHeightUnit
        ' 
        labelSheetHeightUnit.AutoSize = True
        labelSheetHeightUnit.Location = New System.Drawing.Point(229, 271)
        labelSheetHeightUnit.Margin = New Padding(4, 0, 4, 0)
        labelSheetHeightUnit.Name = "labelSheetHeightUnit"
        labelSheetHeightUnit.Size = New Size(44, 25)
        labelSheetHeightUnit.TabIndex = 6
        labelSheetHeightUnit.Text = "mm"
        ' 
        ' labelSheetWidth
        ' 
        labelSheetWidth.AutoSize = True
        labelSheetWidth.Location = New System.Drawing.Point(17, 332)
        labelSheetWidth.Margin = New Padding(4, 0, 4, 0)
        labelSheetWidth.Name = "labelSheetWidth"
        labelSheetWidth.Size = New Size(50, 25)
        labelSheetWidth.TabIndex = 7
        labelSheetWidth.Text = "宽度"
        ' 
        ' numericUpDownSheetWidth
        ' 
        numericUpDownSheetWidth.DecimalPlaces = 2
        numericUpDownSheetWidth.Location = New System.Drawing.Point(17, 365)
        numericUpDownSheetWidth.Margin = New Padding(4)
        numericUpDownSheetWidth.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        numericUpDownSheetWidth.Name = "numericUpDownSheetWidth"
        numericUpDownSheetWidth.Size = New Size(200, 31)
        numericUpDownSheetWidth.TabIndex = 8
        ' 
        ' labelSheetWidthUnit
        ' 
        labelSheetWidthUnit.AutoSize = True
        labelSheetWidthUnit.Location = New System.Drawing.Point(229, 368)
        labelSheetWidthUnit.Margin = New Padding(4, 0, 4, 0)
        labelSheetWidthUnit.Name = "labelSheetWidthUnit"
        labelSheetWidthUnit.Size = New Size(44, 25)
        labelSheetWidthUnit.TabIndex = 9
        labelSheetWidthUnit.Text = "mm"
        ' 
        ' labelTitleBlock
        ' 
        labelTitleBlock.AutoSize = True
        labelTitleBlock.Location = New System.Drawing.Point(486, 135)
        labelTitleBlock.Margin = New Padding(4, 0, 4, 0)
        labelTitleBlock.Name = "labelTitleBlock"
        labelTitleBlock.Size = New Size(69, 25)
        labelTitleBlock.TabIndex = 10
        labelTitleBlock.Text = "标题栏"
        ' 
        ' comboBoxTitleBlock
        ' 
        comboBoxTitleBlock.DropDownStyle = ComboBoxStyle.DropDownList
        comboBoxTitleBlock.Location = New System.Drawing.Point(486, 168)
        comboBoxTitleBlock.Margin = New Padding(4)
        comboBoxTitleBlock.Name = "comboBoxTitleBlock"
        comboBoxTitleBlock.Size = New Size(427, 33)
        comboBoxTitleBlock.TabIndex = 11
        ' 
        ' groupBoxSheetOrientation
        ' 
        groupBoxSheetOrientation.Controls.Add(panelTitleBlockPosition)
        groupBoxSheetOrientation.Controls.Add(radioButtonOrientationPortrait)
        groupBoxSheetOrientation.Controls.Add(radioButtonOrientationLandscape)
        groupBoxSheetOrientation.Location = New System.Drawing.Point(469, 235)
        groupBoxSheetOrientation.Margin = New Padding(4)
        groupBoxSheetOrientation.Name = "groupBoxSheetOrientation"
        groupBoxSheetOrientation.Padding = New Padding(4)
        groupBoxSheetOrientation.Size = New Size(486, 279)
        groupBoxSheetOrientation.TabIndex = 12
        groupBoxSheetOrientation.TabStop = False
        groupBoxSheetOrientation.Text = "图纸方向"
        ' 
        ' panelTitleBlockPosition
        ' 
        panelTitleBlockPosition.Controls.Add(pictureBoxOrientationPreview)
        panelTitleBlockPosition.Controls.Add(radioButtonOrientationTopLeft)
        panelTitleBlockPosition.Controls.Add(radioButtonOrientationTopRight)
        panelTitleBlockPosition.Controls.Add(radioButtonOrientationBottomLeft)
        panelTitleBlockPosition.Controls.Add(radioButtonOrientationBottomRight)
        panelTitleBlockPosition.Location = New System.Drawing.Point(0, 31)
        panelTitleBlockPosition.Name = "panelTitleBlockPosition"
        panelTitleBlockPosition.Size = New Size(270, 241)
        panelTitleBlockPosition.TabIndex = 7
        ' 
        ' pictureBoxOrientationPreview
        ' 
        pictureBoxOrientationPreview.Image = CType(resources.GetObject("pictureBoxOrientationPreview.Image"), Image)
        pictureBoxOrientationPreview.Location = New System.Drawing.Point(57, 71)
        pictureBoxOrientationPreview.Margin = New Padding(4)
        pictureBoxOrientationPreview.Name = "pictureBoxOrientationPreview"
        pictureBoxOrientationPreview.Size = New Size(147, 106)
        pictureBoxOrientationPreview.SizeMode = PictureBoxSizeMode.CenterImage
        pictureBoxOrientationPreview.TabIndex = 0
        pictureBoxOrientationPreview.TabStop = False
        ' 
        ' radioButtonOrientationTopLeft
        ' 
        radioButtonOrientationTopLeft.AutoSize = True
        radioButtonOrientationTopLeft.Location = New System.Drawing.Point(29, 27)
        radioButtonOrientationTopLeft.Margin = New Padding(4)
        radioButtonOrientationTopLeft.Name = "radioButtonOrientationTopLeft"
        radioButtonOrientationTopLeft.Size = New Size(21, 20)
        radioButtonOrientationTopLeft.TabIndex = 1
        radioButtonOrientationTopLeft.UseVisualStyleBackColor = True
        ' 
        ' radioButtonOrientationTopRight
        ' 
        radioButtonOrientationTopRight.AutoSize = True
        radioButtonOrientationTopRight.Location = New System.Drawing.Point(218, 27)
        radioButtonOrientationTopRight.Margin = New Padding(4)
        radioButtonOrientationTopRight.Name = "radioButtonOrientationTopRight"
        radioButtonOrientationTopRight.Size = New Size(21, 20)
        radioButtonOrientationTopRight.TabIndex = 2
        radioButtonOrientationTopRight.UseVisualStyleBackColor = True
        ' 
        ' radioButtonOrientationBottomLeft
        ' 
        radioButtonOrientationBottomLeft.AutoSize = True
        radioButtonOrientationBottomLeft.Location = New System.Drawing.Point(29, 190)
        radioButtonOrientationBottomLeft.Margin = New Padding(4)
        radioButtonOrientationBottomLeft.Name = "radioButtonOrientationBottomLeft"
        radioButtonOrientationBottomLeft.Size = New Size(21, 20)
        radioButtonOrientationBottomLeft.TabIndex = 3
        radioButtonOrientationBottomLeft.UseVisualStyleBackColor = True
        ' 
        ' radioButtonOrientationBottomRight
        ' 
        radioButtonOrientationBottomRight.AutoSize = True
        radioButtonOrientationBottomRight.Checked = True
        radioButtonOrientationBottomRight.Location = New System.Drawing.Point(218, 190)
        radioButtonOrientationBottomRight.Margin = New Padding(4)
        radioButtonOrientationBottomRight.Name = "radioButtonOrientationBottomRight"
        radioButtonOrientationBottomRight.Size = New Size(21, 20)
        radioButtonOrientationBottomRight.TabIndex = 4
        radioButtonOrientationBottomRight.TabStop = True
        radioButtonOrientationBottomRight.UseVisualStyleBackColor = True
        ' 
        ' radioButtonOrientationPortrait
        ' 
        radioButtonOrientationPortrait.AutoSize = True
        radioButtonOrientationPortrait.Location = New System.Drawing.Point(310, 93)
        radioButtonOrientationPortrait.Margin = New Padding(4)
        radioButtonOrientationPortrait.Name = "radioButtonOrientationPortrait"
        radioButtonOrientationPortrait.Size = New Size(75, 29)
        radioButtonOrientationPortrait.TabIndex = 5
        radioButtonOrientationPortrait.TabStop = True
        radioButtonOrientationPortrait.Text = "竖向"
        radioButtonOrientationPortrait.UseVisualStyleBackColor = True
        ' 
        ' radioButtonOrientationLandscape
        ' 
        radioButtonOrientationLandscape.AutoSize = True
        radioButtonOrientationLandscape.Location = New System.Drawing.Point(310, 179)
        radioButtonOrientationLandscape.Margin = New Padding(4)
        radioButtonOrientationLandscape.Name = "radioButtonOrientationLandscape"
        radioButtonOrientationLandscape.Size = New Size(75, 29)
        radioButtonOrientationLandscape.TabIndex = 6
        radioButtonOrientationLandscape.Text = "横向"
        radioButtonOrientationLandscape.UseVisualStyleBackColor = True
        ' 
        ' groupBoxOptions
        ' 
        groupBoxOptions.Controls.Add(checkBoxExcludeFromCount)
        groupBoxOptions.Controls.Add(checkBoxExcludeFromPrint)
        groupBoxOptions.Location = New System.Drawing.Point(469, 532)
        groupBoxOptions.Margin = New Padding(4)
        groupBoxOptions.Name = "groupBoxOptions"
        groupBoxOptions.Padding = New Padding(4)
        groupBoxOptions.Size = New Size(486, 176)
        groupBoxOptions.TabIndex = 13
        groupBoxOptions.TabStop = False
        groupBoxOptions.Text = "选项"
        ' 
        ' checkBoxExcludeFromCount
        ' 
        checkBoxExcludeFromCount.AutoSize = True
        checkBoxExcludeFromCount.Location = New System.Drawing.Point(29, 44)
        checkBoxExcludeFromCount.Margin = New Padding(4)
        checkBoxExcludeFromCount.Name = "checkBoxExcludeFromCount"
        checkBoxExcludeFromCount.Size = New Size(114, 29)
        checkBoxExcludeFromCount.TabIndex = 0
        checkBoxExcludeFromCount.Text = "不予计数"
        checkBoxExcludeFromCount.UseVisualStyleBackColor = True
        ' 
        ' checkBoxExcludeFromPrint
        ' 
        checkBoxExcludeFromPrint.AutoSize = True
        checkBoxExcludeFromPrint.Location = New System.Drawing.Point(29, 96)
        checkBoxExcludeFromPrint.Margin = New Padding(4)
        checkBoxExcludeFromPrint.Name = "checkBoxExcludeFromPrint"
        checkBoxExcludeFromPrint.Size = New Size(114, 29)
        checkBoxExcludeFromPrint.TabIndex = 1
        checkBoxExcludeFromPrint.Text = "不予打印"
        checkBoxExcludeFromPrint.UseVisualStyleBackColor = True
        ' 
        ' buttonApply
        ' 
        buttonApply.Location = New System.Drawing.Point(557, 735)
        buttonApply.Margin = New Padding(4)
        buttonApply.Name = "buttonApply"
        buttonApply.Size = New Size(186, 47)
        buttonApply.TabIndex = 14
        buttonApply.Text = "应用"
        buttonApply.UseVisualStyleBackColor = True
        ' 
        ' buttonCancel
        ' 
        buttonCancel.Location = New System.Drawing.Point(779, 735)
        buttonCancel.Margin = New Padding(4)
        buttonCancel.Name = "buttonCancel"
        buttonCancel.Size = New Size(186, 47)
        buttonCancel.TabIndex = 15
        buttonCancel.Text = "取消"
        buttonCancel.UseVisualStyleBackColor = True
        ' 
        ' FormSheetSettings
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1000, 801)
        Controls.Add(buttonSelectCurrentSheet)
        Controls.Add(buttonSelectAllSheets)
        Controls.Add(buttonClearSheetSelection)
        Controls.Add(groupBoxSheetFormat)
        Controls.Add(labelTitleBlock)
        Controls.Add(comboBoxTitleBlock)
        Controls.Add(groupBoxSheetOrientation)
        Controls.Add(groupBoxOptions)
        Controls.Add(buttonApply)
        Controls.Add(buttonCancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        Name = "FormSheetSettings"
        StartPosition = FormStartPosition.CenterScreen
        Text = "图纸设置"
        groupBoxSheetFormat.ResumeLayout(False)
        groupBoxSheetFormat.PerformLayout()
        CType(numericUpDownSheetHeight, ComponentModel.ISupportInitialize).EndInit()
        CType(numericUpDownSheetWidth, ComponentModel.ISupportInitialize).EndInit()
        groupBoxSheetOrientation.ResumeLayout(False)
        groupBoxSheetOrientation.PerformLayout()
        panelTitleBlockPosition.ResumeLayout(False)
        panelTitleBlockPosition.PerformLayout()
        CType(pictureBoxOrientationPreview, ComponentModel.ISupportInitialize).EndInit()
        groupBoxOptions.ResumeLayout(False)
        groupBoxOptions.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents buttonSelectCurrentSheet As System.Windows.Forms.Button
    Friend WithEvents buttonSelectAllSheets As System.Windows.Forms.Button
    Friend WithEvents buttonClearSheetSelection As System.Windows.Forms.Button
    Friend WithEvents groupBoxSheetFormat As System.Windows.Forms.GroupBox
    Friend WithEvents labelSheetName As System.Windows.Forms.Label
    Friend WithEvents textBoxSheetName As System.Windows.Forms.TextBox
    Friend WithEvents labelSheetSize As System.Windows.Forms.Label
    Friend WithEvents comboBoxSheetSize As System.Windows.Forms.ComboBox
    Friend WithEvents labelSheetHeight As System.Windows.Forms.Label
    Friend WithEvents numericUpDownSheetHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents labelSheetHeightUnit As System.Windows.Forms.Label
    Friend WithEvents labelSheetWidth As System.Windows.Forms.Label
    Friend WithEvents numericUpDownSheetWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents labelSheetWidthUnit As System.Windows.Forms.Label
    Friend WithEvents labelTitleBlock As System.Windows.Forms.Label
    Friend WithEvents comboBoxTitleBlock As System.Windows.Forms.ComboBox
    Friend WithEvents groupBoxSheetOrientation As System.Windows.Forms.GroupBox
    Friend WithEvents panelTitleBlockPosition As System.Windows.Forms.Panel
    Friend WithEvents pictureBoxOrientationPreview As System.Windows.Forms.PictureBox
    Friend WithEvents radioButtonOrientationTopLeft As System.Windows.Forms.RadioButton
    Friend WithEvents radioButtonOrientationTopRight As System.Windows.Forms.RadioButton
    Friend WithEvents radioButtonOrientationBottomLeft As System.Windows.Forms.RadioButton
    Friend WithEvents radioButtonOrientationBottomRight As System.Windows.Forms.RadioButton
    Friend WithEvents radioButtonOrientationPortrait As System.Windows.Forms.RadioButton
    Friend WithEvents radioButtonOrientationLandscape As System.Windows.Forms.RadioButton
    Friend WithEvents groupBoxOptions As System.Windows.Forms.GroupBox
    Friend WithEvents checkBoxExcludeFromCount As System.Windows.Forms.CheckBox
    Friend WithEvents checkBoxExcludeFromPrint As System.Windows.Forms.CheckBox
    Friend WithEvents buttonApply As System.Windows.Forms.Button
    Friend WithEvents buttonCancel As System.Windows.Forms.Button
End Class
