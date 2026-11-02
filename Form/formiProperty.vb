Imports Inventor
Imports System.Windows.Forms

Public Class FormiProperty

    Private Sub Btn确定_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn确定.Click
        Dim oInventorDocument As Inventor.Document
        oInventorDocument = ThisApplication.ActiveEditDocument

        SetPropitem(oInventorDocument, Map_DrawingNnumber, txt图号.Text)
        SetPropitem(oInventorDocument, Map_PartName, txt文件名.Text)
        SetDescription(oInventorDocument, cmb描述.Text)
        SetPropitem(oInventorDocument, "生产数量", txt生产数量.Text)
        SetVendor(oInventorDocument, cmb供应商.Text)
        SetPropitem(oInventorDocument, Map_Price, IIf(txt价格.Text = "", "0", txt价格.Text))

        If oInventorDocument.DocumentType = Inventor.DocumentTypeEnum.kPartDocumentObject Then
            Dim oPartDocument As Inventor.PartDocument = oInventorDocument

            Dim strMaterialName As String
            strMaterialName = cmb材料.Text.ToString

            Dim oMaterial As Inventor.Material
            oMaterial = oPartDocument.Materials.Item(strMaterialName)

            oPartDocument.ComponentDefinition.Material = oMaterial
            'Else
            '' MessageBox.Show("该功能仅适用于零件！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "更改材料")
            'Exit Sub
        End If
        SetUserPropitem(oInventorDocument, "生产数量", txt生产数量.Text)

        If InStr(oInventorDocument.FullDocumentName, ContentCenterFiles) > 0 Then    '零件库文件自动保存
            oInventorDocument.Save2(True)
        End If

        FormManager.CloseAndDisposeForm(Of FormiProperty)()
    End Sub

    Private Sub Btn取消_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn取消.Click, Me.Closing
        FormManager.CloseAndDisposeForm(Of FormiProperty)()
    End Sub

    Private Sub FrmChangeIpro_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.GLTool48
        Me.TopMost = True

        '加载自定义描述
        LoadCustomDescription(cmb描述)

        Dim toolTip As New ToolTip With {
            .AutoPopDelay = 0,
            .InitialDelay = 0,
            .ReshowDelay = 500
        }
        toolTip.SetToolTip(btn向上1, "交换 图号-文件名")
        toolTip.SetToolTip(btn向上2, "交换 文件名-描述")
        toolTip.SetToolTip(btn查询, "查询ERP编码")
        toolTip.SetToolTip(btn保存描述, "保存描述到配置文件")

        btn向上1.Image = My.Resources.交换16.ToBitmap
        btn向上2.Image = My.Resources.交换16.ToBitmap
        btn查询.Image = My.Resources.查询16.ToBitmap
        btn保存描述.Image = My.Resources.保存关闭16.ToBitmap

        Dim oInventorDocument As Inventor.Document
        oInventorDocument = ThisApplication.ActiveEditDocument

        Me.Text = "iProperty+  " & GetFileNameInfo(oInventorDocument.FullDocumentName).OnlyName

        txt位置.Text = oInventorDocument.FullDocumentName

        Dim oPropertySets As PropertySets
        Dim oPropertySet As PropertySet
        Dim propitem As [Property]

        '=============================================================================
        '采用学徒服务器, 速度更快
        'Dim apprentice As Inventor.ApprenticeServerComponent
        'apprentice = New Inventor.ApprenticeServerComponent

        'Dim apprenticeDoc As Inventor.ApprenticeServerDocument
        'apprenticeDoc = apprentice.Open(oInventorDocument.FullDocumentName)

        'oPropertySets = apprenticeDoc.PropertySets

        '=============================================================================

        oPropertySets = oInventorDocument.PropertySets

        '获取iproperty

        For Each oPropertySet In oPropertySets
            For Each propitem In oPropertySet
                Select Case propitem.DisplayName
                    Case Map_DrawingNnumber, "库存编号", "图号", "Stock Number"
                        txt图号.Text = propitem.Value
                    Case Map_PartName, "文件名", "零件名称", "零件代号", "Part Number", "File Name"
                        txt文件名.Text = propitem.Value
                    Case Map_Describe, "描述", "Description"
                        cmb描述.Text = propitem.Value
                    Case Map_Vendor, "供应商", "Vendor"
                        ' cmb供应商 是 DropDownList，直接赋 Text 在值不在预设项时不会显示
                        SetComboValue(cmb供应商, propitem.Value)
                    Case Map_Price, "成本", "价格", "Cost"
                        txt价格.Text = propitem.Value
                    Case "质量", "重量", "Mass"
                        txt质量.Text = FourFive(propitem.Value * 0.001, Mass_Accuracy) & "Kg"
                End Select
            Next
        Next

        ' 描述：显式从标准“Design Tracking Properties”读取 描述/Description，
        '       避免被其它同名（或空值）属性覆盖导致显示不正确（兼容中英文 Inventor）
        cmb描述.Text = GetDescription(oInventorDocument)

        ' 供应商：同描述，显式从标准“Design Tracking Properties”读取 Vendor（内部名，跨语言一致）。
        '         用 SetComboValue 显示（cmb供应商 是 DropDownList，值不在预设项时需加入再选中）
        SetComboValue(cmb供应商, GetVendor(oInventorDocument))

        ' 生产数量：专门读取自定义（用户定义）属性集中的该项，存在则显示于 txt生产数量
        Try
            Dim oVal As Object = oInventorDocument.PropertySets.Item("User Defined Properties").Item("生产数量").Value
            txt生产数量.Text = If(oVal Is Nothing, "", oVal.ToString())
        Catch
            txt生产数量.Text = ""
        End Try

        If oInventorDocument.DocumentType = Inventor.DocumentTypeEnum.kPartDocumentObject Then
            Dim oInventorPartDocument As Inventor.PartDocument = oInventorDocument
            cmb材料.Items.Clear()

            ' 材料库：读取该零件可用的全部材料
            For Each oMaterial In oInventorPartDocument.Materials
                cmb材料.Items.Add(oMaterial.Name)
            Next
            cmb材料.DropDownStyle = ComboBoxStyle.DropDownList

            ' 当前材料：有则直接显示（无材料时不能取 .Name，否则会中断整个 Load）
            Try
                If oInventorPartDocument.ComponentDefinition.Material IsNot Nothing Then
                    SetComboValue(cmb材料, oInventorPartDocument.ComponentDefinition.Material.Name)
                End If
            Catch
            End Try
        Else
            cmb材料.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' 把值显示在下拉框中：若值不在列表中则先加入再选中。
    ''' 用于 DropDownList 样式的下拉框（直接赋 Text 在值不在预设项时不会显示）。
    ''' </summary>
    Private Sub SetComboValue(ByVal cmb As ComboBox, ByVal value As Object)
        If cmb Is Nothing Then Exit Sub

        Dim strValue As String = If(value Is Nothing, "", value.ToString())
        If strValue = "" Then
            cmb.SelectedIndex = -1
            Exit Sub
        End If

        Dim intIndex As Integer = -1
        For i As Integer = 0 To cmb.Items.Count - 1
            If String.Equals(cmb.Items(i).ToString(), strValue, StringComparison.OrdinalIgnoreCase) Then
                intIndex = i
                Exit For
            End If
        Next

        If intIndex < 0 Then
            cmb.Items.Add(strValue)
            ' Sorted=True 时新增项会重排，重新定位
            For i As Integer = 0 To cmb.Items.Count - 1
                If String.Equals(cmb.Items(i).ToString(), strValue, StringComparison.OrdinalIgnoreCase) Then
                    intIndex = i
                    Exit For
                End If
            Next
        End If

        If intIndex >= 0 Then cmb.SelectedIndex = intIndex
    End Sub

    ''' <summary>
    ''' 读取文件“描述/Description”属性（兼容中英文 Inventor）。
    ''' 优先取“Design Tracking Properties”的 Description（内部名，跨语言一致），
    ''' 兜底扫描所有属性集里首个 DisplayName 为 描述/Description 且非空的值。
    ''' </summary>
    Private Function GetDescription(ByVal oDoc As Inventor.Document) As String
        Try
            Dim ps As PropertySet = oDoc.PropertySets.Item("Design Tracking Properties")
            Try
                Dim oValue As Object = ps.Item("Description").Value
                If oValue IsNot Nothing AndAlso oValue.ToString() <> "" Then Return oValue.ToString()
            Catch
            End Try
        Catch
        End Try

        Try
            For Each pset As PropertySet In oDoc.PropertySets
                For Each p As [Property] In pset
                    If p.DisplayName = "描述" OrElse p.DisplayName = "Description" Then
                        If p.Value IsNot Nothing AndAlso p.Value.ToString() <> "" Then Return p.Value.ToString()
                    End If
                Next
            Next
        Catch
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' 将内容写入文件“描述/Description”属性（兼容中英文 Inventor）。
    ''' 优先写“Design Tracking Properties”的 Description（内部名，跨语言一致），
    ''' 兜底扫描更新所有属性集里首个 DisplayName 为 描述/Description 的属性。
    ''' </summary>
    Private Sub SetDescription(ByVal oDoc As Inventor.Document, ByVal strValue As String)
        Try
            Dim ps As PropertySet = oDoc.PropertySets.Item("Design Tracking Properties")
            ps.Item("Description").Value = strValue
            oDoc.Update()
            Return
        Catch
        End Try

        Try
            For Each pset As PropertySet In oDoc.PropertySets
                For Each p As [Property] In pset
                    If p.DisplayName = "描述" OrElse p.DisplayName = "Description" Then
                        p.Value = strValue
                        oDoc.Update()
                        Return
                    End If
                Next
            Next
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' 读取文件“供应商/Vendor”属性（兼容中英文 Inventor）。
    ''' 优先取“Design Tracking Properties”的 Vendor（内部名，跨语言一致），
    ''' 兜底扫描所有属性集里首个 DisplayName 为 供应商/Vendor 且非空的值。
    ''' </summary>
    Private Function GetVendor(ByVal oDoc As Inventor.Document) As String
        Try
            Dim ps As PropertySet = oDoc.PropertySets.Item("Design Tracking Properties")
            Try
                Dim oValue As Object = ps.Item("Vendor").Value
                If oValue IsNot Nothing AndAlso oValue.ToString() <> "" Then Return oValue.ToString()
            Catch
            End Try
        Catch
        End Try

        Try
            For Each pset As PropertySet In oDoc.PropertySets
                For Each p As [Property] In pset
                    If p.DisplayName = "供应商" OrElse p.DisplayName = "Vendor" Then
                        If p.Value IsNot Nothing AndAlso p.Value.ToString() <> "" Then Return p.Value.ToString()
                    End If
                Next
            Next
        Catch
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' 将内容写入文件“供应商/Vendor”属性（兼容中英文 Inventor）。
    ''' 优先写“Design Tracking Properties”的 Vendor（内部名，跨语言一致），
    ''' 兜底扫描更新所有属性集里首个 DisplayName 为 供应商/Vendor 的属性。
    ''' </summary>
    Private Sub SetVendor(ByVal oDoc As Inventor.Document, ByVal strValue As String)
        Try
            Dim ps As PropertySet = oDoc.PropertySets.Item("Design Tracking Properties")
            ps.Item("Vendor").Value = strValue
            oDoc.Update()
            Return
        Catch
        End Try

        Try
            For Each pset As PropertySet In oDoc.PropertySets
                For Each p As [Property] In pset
                    If p.DisplayName = "供应商" OrElse p.DisplayName = "Vendor" Then
                        p.Value = strValue
                        oDoc.Update()
                        Return
                    End If
                Next
            Next
        Catch
        End Try
    End Sub

    Private Sub Btn向上1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn向上1.Click
        Dim strTemp As String
        strTemp = txt图号.Text
        txt图号.Text = txt文件名.Text
        txt文件名.Text = strTemp
    End Sub

    Private Sub Btn向上2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn向上2.Click
        Dim strTemp As String
        strTemp = txt文件名.Text
        txt文件名.Text = cmb描述.Text
        cmb描述.Text = strTemp
    End Sub

    Private Sub Btn查询_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn查询.Click
        SetStatusBarText()

        If IsInventorOpenDocument() = False Then
            Exit Sub
        End If

        btn查询.Enabled = False

        Dim OInteractionEvents As InteractionEvents = ThisApplication.CommandManager.CreateInteractionEvents
        OInteractionEvents.Start()
        OInteractionEvents.SetCursor(CursorTypeEnum.kCursorTypeWindows, 32514)
        ThisApplication.UserInterfaceManager.DoEvents()


        Dim oInventorDocument As Inventor.Document      '当前文件
        oInventorDocument = ThisApplication.ActiveEditDocument

        Dim strStochNum As String
        Dim strPartNum As String

        strStochNum = txt图号.Text

        strPartNum = FindSrtingInSheet(BasicExcelFullFileName, strStochNum, SheetName, TableArrays, ColIndexNum, 0)
        'If strPartNum <> 0 Then
        '    ' MessageBox.Show("查询到ERP编码：" & strPartNum, MsgBoxStyle.OkOnly, "查询ERP编码")
        '    'SetPropitem(oInventorDocument, Map_ERPCode, strPartNum)
        '    Select Case txtERP编码.Text
        '        Case ""
        '            txtERP编码.Text = strPartNum
        '        Case Else
        '            If txtERP编码.Text <> strPartNum Then
        '                If MessageBox.Show($"查询到不同的ERP编码：{strPartNum}，是否更新？", GLTool，
        '                                   MessageBoxButtons.YesNo， MessageBoxIcon.Question） = DialogResult.Yes Then
        '                    txtERP编码.Text = strPartNum
        '                End If
        '            End If
        '    End Select
        'Else
        '    Me.TopMost = False
        '    MessageBox.Show(”未查询到ERP编码。“, GLTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Me.TopMost = True
        'End If

        btn查询.Enabled = True

        'OInteractionEvents.SetCursor(CursorTypeEnum.kCursorTypeDefault)
        OInteractionEvents.Stop()

    End Sub

    Private Sub Txt价格_MouseClick(sender As Object, e As MouseEventArgs) Handles txt价格.MouseClick
        txt价格.SelectAll()
    End Sub

    Private Sub TxtERP编码_MouseClick(sender As Object, e As MouseEventArgs)
        'txtERP编码.SelectAll()
    End Sub

    ''' <summary>
    ''' 从配置文件加载自定义描述
    ''' </summary>
    ''' <param name="oComboBox">ComboBox对象</param>
    ''' <remarks></remarks>
    Private Sub LoadCustomDescription(oComboBox As ComboBox)
        Dim strDescriptions As String
        Dim arrayDescriptions() As String
        Dim strDescription As String

        strDescriptions = GetStrFromINI("自定义描述", "自定义描述", "", IniFile)

        arrayDescriptions = Strings.Split(strDescriptions, "|")

        For Each strDescription In arrayDescriptions
            If oComboBox.Items.Contains(strDescription) = False Then
                oComboBox.Items.Add(strDescription)
            End If
        Next

    End Sub

    ''' <summary>
    ''' 保存自定义描述到配置文件
    ''' </summary>
    ''' <param name="oComboBox">ComboBox</param>
    ''' <remarks></remarks>
    Private Sub SaveCustomDescription(oComboBox As ComboBox)
        Dim strDescriptions As String = Nothing
        Dim i As Integer

        For i = 1 To oComboBox.Items.Count - 1
            strDescriptions = strDescriptions & "|" & oComboBox.Items(i)
        Next

        WriteStrINI("自定义描述", "自定义描述", strDescriptions, IniFile)

    End Sub

    Private Sub Btn提取文件名_Click(sender As Object, e As EventArgs) Handles btn提取文件名.Click
        Dim oInventorDocument As Inventor.Document      '当前文件
        oInventorDocument = ThisApplication.ActiveEditDocument

        Dim strInventorDocumentFullFileName As String
        strInventorDocumentFullFileName = oInventorDocument.FullFileName

        Dim oStockNumPartName As StockNumPartName
        oStockNumPartName = GetStockNumPartName(strInventorDocumentFullFileName)

        SetPropitem(oInventorDocument, Map_DrawingNnumber, oStockNumPartName.图号)
        SetPropitem(oInventorDocument, Map_PartName, oStockNumPartName.零件名称)

        txt图号.Text = oStockNumPartName.图号
        txt文件名.Text = oStockNumPartName.零件名称

    End Sub
End Class