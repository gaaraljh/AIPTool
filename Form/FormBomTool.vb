
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Inventor
Imports Inventor.BOMStructureEnum
Imports Inventor.BOMViewTypeEnum
Imports Inventor.DocumentTypeEnum
Imports Inventor.IOMechanismEnum
Imports Inventor.PaperSizeEnum
Imports Inventor.PrintRangeEnum
Imports Inventor.PrintScaleModeEnum

''' <summary>
''' BOM工具 主窗体（布局见 FormBomTool.Designer.vb，勿在代码里创建/摆放控件）
''' 功能：打开时读取当前装配体全部零部件，按层级顺序显示在 DGViewBomList，
'''       “类型”列按 部件/零件/焊件/钣金件/标准件 显示对应图标与文字，
'''       并列显示 文件名、库存编号、零件编号、统计数量、材料、描述、BOM结构、是否有工程图、缩略图。
''' </summary>
Public Class FormBomTool

    ' ══════════════════════════════════════════════════════════════════════
    '  数据行
    ' ══════════════════════════════════════════════════════════════════════
    Private Class BomRow
        Public Property LevelNo As String = ""      ' 层级序号：1 / 1.2 / 1.2.3
        Public Property Level As Integer = 1        ' 层级深度
        Public Property PartType As String = "零件"  ' 部件/零件/焊件/钣金件/标准件
        Public Property FileName As String = ""
        Public Property FilePath As String = ""
        Public Property StockNumber As String = ""  ' 库存编号
        Public Property PartNumber As String = ""   ' 零件编号
        Public Property Qty As Integer = 1          ' 统计数量（整个装配体中出现次数）
        Public Property Material As String = ""
        Public Property Description As String = ""
        Public Property BomStruct As String = "普通" ' BOM结构
        Public Property HasDrawing As String = "无" ' 是否有工程图
        Public Property Thumbnail As System.Drawing.Image = Nothing
        Public Property IsMissing As Boolean = False
    End Class

    ' ── 列名常量 ──
    Private Const COL_IDX As String = "cIdx"
    Private Const COL_TYPE As String = "cType"
    Private Const COL_FILE As String = "cFile"
    Private Const COL_STOCK As String = "cStock"
    Private Const COL_PNO As String = "cPno"
    Private Const COL_QTY As String = "cQty"
    Private Const COL_MAT As String = "cMat"
    Private Const COL_DESC As String = "cDesc"
    Private Const COL_BOM As String = "cBom"
    Private Const COL_DWG As String = "cDwg"
    Private Const COL_THUMB As String = "cThumb"

    ' 属性集 GUID
    Private Const GUID_DTP As String = "{32853F0F-3444-11D1-9E93-0060B03C1CA6}"   ' Design Tracking
    Private Const GUID_UDP As String = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"   ' User Defined

    ' 层级展开上限，防止超大装配体把界面卡死
    Private Const MAX_ROWS As Integer = 6000

    Private m_asmDoc As AssemblyDocument = Nothing
    Private m_rows As New List(Of BomRow)
    Private m_typeIcons As New Dictionary(Of String, System.Drawing.Image)(StringComparer.OrdinalIgnoreCase)
    Private m_rowH As Integer = 34
    Private m_thumbBusy As Boolean = False

    ' ══════════════════════════════════════════════════════════════════════
    '  窗体加载：读取装配体 → 建列 → 绑定数据
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub FormBomTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            Me.StartPosition = FormStartPosition.CenterScreen

            If ThisApplication Is Nothing OrElse ThisApplication.ActiveEditDocument Is Nothing Then
                MessageBox.Show("请先打开一个装配体（.iam）文档。", "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
                Return
            End If
            If Not TypeOf ThisApplication.ActiveEditDocument Is AssemblyDocument Then
                MessageBox.Show("BOM工具需要在装配体（.iam）环境中运行。", "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
                Return
            End If

            m_asmDoc = CType(ThisApplication.ActiveEditDocument, AssemblyDocument)
            Me.Text = "BOM工具  -  " & m_asmDoc.DisplayName

            m_rowH = DGViewBomList.RowTemplate.Height
            If m_rowH < 24 Then m_rowH = 34
            If m_rowH > 80 Then m_rowH = 80
            DGViewBomList.RowTemplate.Height = m_rowH

            InitBomData()
            BuildColumns()
            BindRows()

            Label3.Text = "  共 " & m_rows.Count.ToString() & " 个零部件（按层级顺序）"
        Catch ex As Exception
            MessageBox.Show("BOM工具加载失败：" & ex.Message, "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    ' ══════════════════════════════════════════════════════════════════════
    '  数据采集
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub InitBomData()
        Dim oldCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            m_rows = CollectHierarchy()
            LoadTypeIcons()
        Finally
            Me.Cursor = oldCursor
        End Try
    End Sub

    ''' <summary>按层级顺序遍历装配体，返回全部零部件行</summary>
    Private Function CollectHierarchy() As List(Of BomRow)
        Dim rows As New List(Of BomRow)
        Dim topDef As AssemblyComponentDefinition = m_asmDoc.ComponentDefinition

        ' 第一遍：统计每个文件在整个装配体中的出现次数（统计数量）
        Dim qtyMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        CountOccurs(topDef.Occurrences, qtyMap)

        ' 第二遍：按层级顺序输出
        WalkLevel(topDef.Occurrences, 1, "", rows, qtyMap)
        Return rows
    End Function

    Private Sub CountOccurs(occs As ComponentOccurrences, qtyMap As Dictionary(Of String, Integer))
        For Each occ As ComponentOccurrence In occs
            If occ.Suppressed Then Continue For
            Dim fp As String = GetOccFilePath(occ)
            If Not String.IsNullOrEmpty(fp) Then
                If qtyMap.ContainsKey(fp) Then qtyMap(fp) += 1 Else qtyMap.Add(fp, 1)
            End If
            If occ.DefinitionDocumentType = kAssemblyDocumentObject Then
                Try : CountOccurs(occ.SubOccurrences, qtyMap) : Catch : End Try
            End If
        Next
    End Sub

    Private Sub WalkLevel(occs As ComponentOccurrences, level As Integer, parentNo As String,
                          rows As List(Of BomRow), qtyMap As Dictionary(Of String, Integer))
        Dim n As Integer = 0
        For Each occ As ComponentOccurrence In occs
            If occ.Suppressed Then Continue For
            If rows.Count >= MAX_ROWS Then Return
            n += 1
            Dim no As String = If(String.IsNullOrEmpty(parentNo), n.ToString(), parentNo & "." & n.ToString())

            Dim row As BomRow = BuildRow(occ, qtyMap)
            If row IsNot Nothing Then
                row.Level = level
                row.LevelNo = no
                rows.Add(row)
            End If

            If occ.DefinitionDocumentType = kAssemblyDocumentObject Then
                Try
                    ' 自引用保护：跳过引用自身的子节点
                    Dim selfFp As String = GetOccFilePath(occ)
                    Dim childList As New List(Of ComponentOccurrence)
                    For Each childOcc As ComponentOccurrence In occ.SubOccurrences
                        Dim cfp As String = GetOccFilePath(childOcc)
                        If String.IsNullOrEmpty(cfp) OrElse String.Compare(cfp, selfFp, True) <> 0 Then childList.Add(childOcc)
                    Next
                    WalkLevelItems(childList, level + 1, no, rows, qtyMap)
                Catch : End Try
            End If
        Next
    End Sub

    Private Sub WalkLevelItems(items As List(Of ComponentOccurrence), level As Integer, parentNo As String,
                               rows As List(Of BomRow), qtyMap As Dictionary(Of String, Integer))
        Dim n As Integer = 0
        For Each occ As ComponentOccurrence In items
            If occ.Suppressed Then Continue For
            If rows.Count >= MAX_ROWS Then Return
            n += 1
            Dim no As String = If(String.IsNullOrEmpty(parentNo), n.ToString(), parentNo & "." & n.ToString())

            Dim row As BomRow = BuildRow(occ, qtyMap)
            If row IsNot Nothing Then
                row.Level = level
                row.LevelNo = no
                rows.Add(row)
            End If

            If occ.DefinitionDocumentType = kAssemblyDocumentObject Then
                Try
                    Dim selfFp As String = GetOccFilePath(occ)
                    Dim childList As New List(Of ComponentOccurrence)
                    For Each childOcc As ComponentOccurrence In occ.SubOccurrences
                        Dim cfp As String = GetOccFilePath(childOcc)
                        If String.IsNullOrEmpty(cfp) OrElse String.Compare(cfp, selfFp, True) <> 0 Then childList.Add(childOcc)
                    Next
                    WalkLevelItems(childList, level + 1, no, rows, qtyMap)
                Catch : End Try
            End If
        Next
    End Sub

    ''' <summary>由一个 Occurrence 构建数据行（含属性读取）</summary>
    Private Function BuildRow(occ As ComponentOccurrence, qtyMap As Dictionary(Of String, Integer)) As BomRow
        Dim row As New BomRow()
        Dim fp As String = ""
        Dim doc As Document = Nothing
        Try
            doc = occ.Definition.Document
            fp = doc.FullDocumentName
            If String.IsNullOrEmpty(fp) Then fp = doc.FullDisplayName
        Catch : End Try

        If String.IsNullOrEmpty(fp) Then
            ' 引用丢失
            row.IsMissing = True
            row.FileName = occ.Name
            row.PartType = "零件"
            row.Qty = 1
            Return row
        End If

        row.FilePath = fp
        row.FileName = System.IO.Path.GetFileNameWithoutExtension(fp)

        Dim isCC As Boolean = False
        Try : isCC = occ.IsContentCenter : Catch : End Try

        row.PartType = GetPartType(doc, isCC)
        If qtyMap.ContainsKey(fp) Then row.Qty = qtyMap(fp) Else row.Qty = 1
        row.BomStruct = BomStructToText(occ)
        row.HasDrawing = If(HasDrawingFile(fp), "有", "无")
        row.StockNumber = ReadProp(doc, "Stock Number")
        row.PartNumber = ReadProp(doc, "Part Number")
        row.Material = ReadProp(doc, "Material")
        row.Description = ReadProp(doc, "Description")
        Return row
    End Function

    Private Function GetOccFilePath(occ As ComponentOccurrence) As String
        Try
            Dim d As Document = occ.Definition.Document
            Dim fp As String = d.FullDocumentName
            If String.IsNullOrEmpty(fp) Then fp = d.FullDisplayName
            Return fp
        Catch : End Try
        Return ""
    End Function

    ''' <summary>类型：部件 / 零件 / 焊件 / 钣金件 / 标准件</summary>
    Private Function GetPartType(doc As Document, isCC As Boolean) As String
        If doc Is Nothing Then Return "零件"
        Try
            If doc.DocumentType = kPartDocumentObject Then
                Dim pd As PartDocument = CType(doc, PartDocument)
                Dim cd As ComponentDefinition = pd.ComponentDefinition
                If TypeOf cd Is WeldmentComponentDefinition Then Return "焊件"
                If TypeOf cd Is SheetMetalComponentDefinition Then Return "钣金件"
                If isCC Then Return "标准件"
                Return "零件"
            ElseIf doc.DocumentType = kAssemblyDocumentObject Then
                Dim ad As AssemblyDocument = CType(doc, AssemblyDocument)
                Dim cd2 As ComponentDefinition = ad.ComponentDefinition
                If TypeOf cd2 Is WeldmentComponentDefinition Then Return "焊件"
                If isCC Then Return "标准件"
                Return "部件"
            End If
        Catch : End Try
        Return "零件"
    End Function

    Private Function BomStructToText(occ As ComponentOccurrence) As String
        Try
            Select Case CInt(occ.BOMStructure)
                Case 51971 : Return "外购"
                Case 51972 : Return "不可拆分"
                Case 51973 : Return "虚拟件"
                Case 51974 : Return "参考件"
                Case Else : Return "普通"
            End Select
        Catch : End Try
        Return "普通"
    End Function

    Private Function HasDrawingFile(fp As String) As Boolean
        Try
            Dim dir As String = System.IO.Path.GetDirectoryName(fp)
            Dim baseName As String = System.IO.Path.GetFileNameWithoutExtension(fp)
            If String.IsNullOrEmpty(dir) OrElse String.IsNullOrEmpty(baseName) Then Return False
            For Each ext As String In New String() {".idw", ".dwg"}
                If System.IO.File.Exists(System.IO.Path.Combine(dir, baseName & ext)) Then Return True
            Next
        Catch : End Try
        Return False
    End Function

    ''' <summary>读取 iProperty（先 Design Tracking，再 User Defined）</summary>
    Private Function ReadProp(doc As Document, propName As String) As String
        If doc Is Nothing OrElse String.IsNullOrEmpty(propName) Then Return ""
        Try
            Dim ps As PropertySets = doc.PropertySets
            Dim val As Object = Nothing
            Try : val = ps.Item(GUID_DTP).Item(propName).Value : Catch : End Try
            If val Is Nothing Then Try : val = ps.Item(GUID_UDP).Item(propName).Value : Catch : End Try
            If val Is Nothing Then Return ""
            Return SafeStr(val)
        Catch : End Try
        Return ""
    End Function

    Private Function SafeStr(val As Object) As String
        If val Is Nothing Then Return ""
        Try : Return Convert.ToString(val).Trim() : Catch : Return "" : End Try
    End Function

    ' ══════════════════════════════════════════════════════════════════════
    '  类型图标（GDI+ 绘制，不依赖外部资源）
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub LoadTypeIcons()
        m_typeIcons.Clear()
        Dim sz As Integer = Math.Max(16, Math.Min(m_rowH - 6, 40))
        For Each kind As String In New String() {"部件", "零件", "焊件", "钣金件", "标准件"}
            Try : m_typeIcons(kind) = MakeTypeIcon(kind, sz) : Catch : End Try
        Next
    End Sub

    Private Function MakeTypeIcon(kind As String, sz As Integer) As System.Drawing.Bitmap
        Dim bmp As New System.Drawing.Bitmap(sz, sz, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            g.Clear(System.Drawing.Color.Transparent)

            Dim body As System.Drawing.Color
            Select Case kind
                Case "部件" : body = System.Drawing.Color.FromArgb(0, 120, 215)
                Case "零件" : body = System.Drawing.Color.FromArgb(30, 150, 90)
                Case "焊件" : body = System.Drawing.Color.FromArgb(230, 120, 30)
                Case "钣金件" : body = System.Drawing.Color.FromArgb(235, 190, 40)
                Case Else : body = System.Drawing.Color.FromArgb(140, 140, 165)   ' 标准件
            End Select

            Using br As New System.Drawing.SolidBrush(body)
                Select Case kind
                    Case "标准件"
                        ' 螺栓：六角头 + 螺杆 + 螺纹
                        Dim hw As Single = sz * 0.7!
                        Dim hh As Single = sz * 0.28!
                        g.FillRectangle(br, CSng((sz - hw) / 2), CSng(sz * 0.08), hw, hh)
                        g.FillRectangle(br, CSng(sz * 0.42), CSng(sz * 0.36), CSng(sz * 0.16), CSng(sz * 0.56))
                        Using pn As New System.Drawing.Pen(System.Drawing.Color.FromArgb(70, 70, 100), CSng(Math.Max(1.0!, sz * 0.05!)))
                            For i As Integer = 0 To 3
                                Dim yy As Single = sz * (0.42! + i * 0.13!)
                                g.DrawLine(pn, CSng(sz * 0.42), yy, CSng(sz * 0.58), yy)
                            Next
                        End Using
                    Case "焊件"
                        g.FillRectangle(br, 1, 1, sz - 2, sz - 2)
                        Using pn As New System.Drawing.Pen(System.Drawing.Color.White, CSng(Math.Max(1.0!, sz * 0.07!)))
                            For i As Integer = 0 To 2
                                Dim x0 As Single = sz * (0.2! + i * 0.25!)
                                g.DrawLine(pn, x0, CSng(sz * 0.25), x0, CSng(sz * 0.75))
                            Next
                        End Using
                    Case "钣金件"
                        g.FillRectangle(br, 1, 1, sz - 2, sz - 2)
                        Using pn As New System.Drawing.Pen(System.Drawing.Color.White, CSng(Math.Max(1.0!, sz * 0.07!)))
                            g.DrawLine(pn, CSng(sz * 0.2), CSng(sz * 0.5), CSng(sz * 0.8), CSng(sz * 0.5))
                            g.DrawLine(pn, CSng(sz * 0.5), CSng(sz * 0.2), CSng(sz * 0.5), CSng(sz * 0.8))
                        End Using
                    Case "部件"
                        g.FillRectangle(br, 1, 1, sz - 2, sz - 2)
                        Using br2 As New System.Drawing.SolidBrush(System.Drawing.Color.White)
                            Dim c As Single = sz * 0.22!
                            g.FillRectangle(br2, CSng(sz * 0.16), CSng(sz * 0.16), c, c)
                            g.FillRectangle(br2, CSng(sz * 0.16), CSng(sz * 0.62), c, c)
                            g.FillRectangle(br2, CSng(sz * 0.62), CSng(sz * 0.16), c, c)
                        End Using
                    Case Else   ' 零件
                        g.FillRectangle(br, 1, 1, sz - 2, sz - 2)
                        Using br2 As New System.Drawing.SolidBrush(System.Drawing.Color.White)
                            g.FillEllipse(br2, CSng(sz * 0.34), CSng(sz * 0.34), CSng(sz * 0.32), CSng(sz * 0.32))
                        End Using
                End Select
            End Using

            Using pnB As New System.Drawing.Pen(System.Drawing.Color.FromArgb(60, 60, 80))
                g.DrawRectangle(pnB, 0, 0, sz - 1, sz - 1)
            End Using
        End Using
        Return bmp
    End Function

    ' ══════════════════════════════════════════════════════════════════════
    '  表格列与数据绑定
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub BuildColumns()
        Try : RemoveHandler DGViewBomList.CellPainting, AddressOf OnTypeCellPainting : Catch : End Try

        DGViewBomList.Columns.Clear()
        DGViewBomList.AllowUserToAddRows = False
        DGViewBomList.AllowUserToDeleteRows = False
        DGViewBomList.RowHeadersVisible = False
        DGViewBomList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        DGViewBomList.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGViewBomList.MultiSelect = False
        DGViewBomList.EnableHeadersVisualStyles = False

        AddTextCol(COL_IDX, "序号", 70, True)
        AddTextCol(COL_TYPE, "类型", 110, True)
        AddTextCol(COL_FILE, "文件名", 260, False)
        AddTextCol(COL_STOCK, "库存编号", 110, False)
        AddTextCol(COL_PNO, "零件编号", 120, False)
        AddTextCol(COL_QTY, "统计数量", 80, True)
        AddTextCol(COL_MAT, "材料", 110, False)
        AddTextCol(COL_DESC, "描述", 180, False)
        AddTextCol(COL_BOM, "BOM结构", 90, True)
        AddTextCol(COL_DWG, "是否有工程图", 110, True)

        Dim imgCol As New DataGridViewImageColumn()
        imgCol.Name = COL_THUMB
        imgCol.HeaderText = "缩略图"
        imgCol.ReadOnly = True
        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom
        imgCol.Width = CInt(m_rowH * 1.6)
        DGViewBomList.Columns.Add(imgCol)

        Dim hdr As DataGridViewCellStyle = DGViewBomList.ColumnHeadersDefaultCellStyle
        hdr.BackColor = System.Drawing.Color.FromArgb(108, 92, 231)
        hdr.ForeColor = System.Drawing.Color.White
        hdr.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGViewBomList.ColumnHeadersHeight = Math.Max(28, m_rowH)

        AddHandler DGViewBomList.CellPainting, AddressOf OnTypeCellPainting
    End Sub

    Private Sub AddTextCol(name As String, header As String, width As Integer, isReadOnly As Boolean)
        Dim col As New DataGridViewTextBoxColumn()
        col.Name = name
        col.HeaderText = header
        col.ReadOnly = isReadOnly
        col.Width = width
        col.SortMode = DataGridViewColumnSortMode.NotSortable
        DGViewBomList.Columns.Add(col)
    End Sub

    Private Sub BindRows()
        DGViewBomList.Rows.Clear()
        Dim boldFont As System.Drawing.Font = New System.Drawing.Font(DGViewBomList.Font, FontStyle.Bold)

        For Each row As BomRow In m_rows
            Dim i As Integer = DGViewBomList.Rows.Add()
            Dim dr As DataGridViewRow = DGViewBomList.Rows(i)
            dr.Tag = row
            dr.Height = m_rowH

            dr.Cells(COL_IDX).Value = row.LevelNo
            dr.Cells(COL_TYPE).Value = row.PartType
            dr.Cells(COL_FILE).Value = row.FileName
            dr.Cells(COL_STOCK).Value = row.StockNumber
            dr.Cells(COL_PNO).Value = row.PartNumber
            dr.Cells(COL_QTY).Value = row.Qty
            dr.Cells(COL_MAT).Value = row.Material
            dr.Cells(COL_DESC).Value = row.Description
            dr.Cells(COL_BOM).Value = row.BomStruct
            dr.Cells(COL_DWG).Value = row.HasDrawing

            ' 层级缩进：按层级深度缩进文件名
            Dim pad As Integer = Math.Min(Math.Max(row.Level - 1, 0), 6) * 10
            If pad > 0 Then dr.Cells(COL_FILE).Style.Padding = New Padding(pad, 0, 0, 0)

            ' 顶层（直属装配体）加粗
            If row.Level = 1 Then dr.DefaultCellStyle.Font = boldFont
            ' 丢失文件标紫
            If row.IsMissing Then
                dr.Cells(COL_FILE).Style.ForeColor = System.Drawing.Color.Purple
                dr.Cells(COL_FILE).Value = row.FileName & "（丢失）"
            End If
        Next

        LoadThumbnailsAsync()
    End Sub

    ' ══════════════════════════════════════════════════════════════════════
    '  “类型”列：图标 + 文字
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub OnTypeCellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        Try
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
            If DGViewBomList.Columns(e.ColumnIndex).Name <> COL_TYPE Then Return
            Dim dr As DataGridViewRow = DGViewBomList.Rows(e.RowIndex)
            Dim row As BomRow = TryCast(dr.Tag, BomRow)
            If row Is Nothing Then Return

            e.Handled = True
            e.PaintBackground(e.CellBounds, True)

            Dim img As System.Drawing.Image = Nothing
            If Not m_typeIcons.TryGetValue(row.PartType, img) Then img = Nothing

            Dim txt As String = row.PartType
            Dim fnt As System.Drawing.Font = If(dr.DefaultCellStyle.Font, If(e.CellStyle.Font, DGViewBomList.Font))
            If fnt Is Nothing Then fnt = DGViewBomList.Font
            Dim sz As System.Drawing.SizeF = e.Graphics.MeasureString(txt, fnt)
            Dim iconW As Integer = If(img Is Nothing, 0, img.Width)
            Dim x As Integer = e.CellBounds.X + 6
            Dim y As Integer = e.CellBounds.Y + (e.CellBounds.Height - iconW) \ 2
            If img IsNot Nothing AndAlso iconW > 0 Then
                e.Graphics.DrawImage(img, New System.Drawing.Rectangle(x, y, iconW, iconW))
            End If
            Dim fg As System.Drawing.Color = e.CellStyle.ForeColor
            If fg.ToArgb() = System.Drawing.Color.Empty.ToArgb() Then fg = DGViewBomList.DefaultCellStyle.ForeColor
            If fg.ToArgb() = System.Drawing.Color.Empty.ToArgb() Then fg = System.Drawing.Color.Black
            Using br As New System.Drawing.SolidBrush(fg)
                e.Graphics.DrawString(txt, fnt, br, CSng(x + iconW + 5),
                                      CSng(e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2))
            End Using
        Catch : End Try
    End Sub

    ' ══════════════════════════════════════════════════════════════════════
    '  缩略图（后台线程读取文件内置预览图）
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub LoadThumbnailsAsync()
        If m_thumbBusy Then Return
        m_thumbBusy = True
        Dim th As New System.Threading.Thread(AddressOf ThumbThreadProc)
        th.IsBackground = True
        th.Start()
    End Sub

    Private Sub ThumbThreadProc()
        Try
            Dim done As New Dictionary(Of String, System.Drawing.Image)(StringComparer.OrdinalIgnoreCase)
            Dim px As Integer = Math.Max(48, m_rowH * 2)
            For Each row As BomRow In m_rows
                If String.IsNullOrEmpty(row.FilePath) OrElse row.IsMissing Then Continue For
                If done.ContainsKey(row.FilePath) Then
                    row.Thumbnail = done(row.FilePath)
                    Continue For
                End If
                Dim img As System.Drawing.Image = GetShellThumbnail(row.FilePath, px)
                If img IsNot Nothing Then
                    done(row.FilePath) = img
                    row.Thumbnail = img
                    UpdateThumbCell(row, img)
                End If
            Next
        Catch : End Try
        m_thumbBusy = False
    End Sub

    Private Sub UpdateThumbCell(row As BomRow, img As System.Drawing.Image)
        Try
            If DGViewBomList.IsHandleCreated AndAlso DGViewBomList.InvokeRequired Then
                DGViewBomList.BeginInvoke(New Action(Of BomRow, System.Drawing.Image)(AddressOf UpdateThumbCell), row, img)
                Return
            End If
            For Each dr As DataGridViewRow In DGViewBomList.Rows
                If ReferenceEquals(dr.Tag, row) Then
                    dr.Cells(COL_THUMB).Value = img
                    Exit For
                End If
            Next
        Catch : End Try
    End Sub

    Private Function GetShellThumbnail(filePath As String, size As Integer) As System.Drawing.Image
        Dim factory As IShellItemImageFactory = Nothing
        Try
            If Not System.IO.File.Exists(filePath) Then Return Nothing
            Dim riid As New Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")
            Dim hr As Integer = SHCreateItemFromParsingName(filePath, IntPtr.Zero, riid, factory)
            If hr <> 0 OrElse factory Is Nothing Then Return Nothing
            Dim hbm As IntPtr = IntPtr.Zero
            hr = factory.GetImage(New System.Drawing.Size(size, size), 1UI, hbm)   ' SIIGBF_THUMBNAILONLY
            If hr <> 0 OrElse hbm = IntPtr.Zero Then Return Nothing
            Dim src As System.Drawing.Bitmap = System.Drawing.Bitmap.FromHbitmap(hbm)
            Dim copy As New System.Drawing.Bitmap(src)
            src.Dispose()
            DeleteObject(hbm)
            Return copy
        Catch : End Try
        Return Nothing
    End Function

    ' ══════════════════════════════════════════════════════════════════════
    '  刷新按钮：重新读取装配体
    ' ══════════════════════════════════════════════════════════════════════
    Private Sub BtnRefreshExcel_Click(sender As Object, e As EventArgs) Handles BtnRefreshExcel.Click
        Try
            If ThisApplication Is Nothing OrElse Not TypeOf ThisApplication.ActiveEditDocument Is AssemblyDocument Then
                MessageBox.Show("请先打开一个装配体（.iam）文档。", "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            m_asmDoc = CType(ThisApplication.ActiveEditDocument, AssemblyDocument)
            Me.Text = "BOM工具  -  " & m_asmDoc.DisplayName
            InitBomData()
            BuildColumns()
            BindRows()
            Label3.Text = "  共 " & m_rows.Count.ToString() & " 个零部件（按层级顺序）"
        Catch ex As Exception
            MessageBox.Show("刷新失败：" & ex.Message, "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ══════════════════════════════════════════════════════════════════════
    '  Shell 缩略图接口
    ' ══════════════════════════════════════════════════════════════════════
    <ComImport()>
    <Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")>
    <System.Runtime.InteropServices.InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Private Interface IShellItemImageFactory
        Function GetImage(<[In]()> ByVal size As System.Drawing.Size,
                          <[In]()> ByVal flags As UInteger,
                          <Out()> ByRef phbm As IntPtr) As Integer
    End Interface

    <DllImport("shell32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SHCreateItemFromParsingName(
        <[In]()> ByVal pszPath As String,
        ByVal pbc As IntPtr,
        <[In]()> ByRef riid As Guid,
        <Out(), MarshalAs(UnmanagedType.Interface)> ByRef ppv As IShellItemImageFactory) As Integer
    End Function

    <DllImport("gdi32.dll")>
    Private Shared Function DeleteObject(<[In]()> ByVal hObject As IntPtr) As Boolean
    End Function

End Class
