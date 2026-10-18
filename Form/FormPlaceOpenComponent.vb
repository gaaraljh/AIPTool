Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Inventor
Imports System.Runtime.InteropServices

Public Class FormPlaceOpenComponent
    ' 移除未使用的 strCurrentDirectory，或者确保它被正确初始化
    ' Private strCurrentDirectory As String 

    Public strSelectFileFullName As String
    ' 用于存储当前激活装配体的路径，以便判断是否重复插入
    Private strActiveAssemblyPath As String

    ' 缩略图延迟加载：快速切换选中行时只渲染最后一次，减少等待
    Private ThumbnailTimer As System.Windows.Forms.Timer = New System.Windows.Forms.Timer()
    Private oPendingThumbnailDocument As Inventor.Document = Nothing

    Public Sub FormPlaceOpenComponent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.插入零部件32
        Me.TopMost = True
        Dgw列表.AllowUserToResizeRows = False

        ' 初始化缩略图延迟加载计时器
        ThumbnailTimer.Interval = 150
        AddHandler ThumbnailTimer.Tick, AddressOf ThumbnailTimer_Tick


        ' --- 【新增】设置快捷键行为 ---
        Me.AcceptButton = btn插入到部件   ' 按 Enter 键触发此按钮
        Me.CancelButton = BtnClose        ' 按 ESC 键触发此按钮（假设关闭按钮名为 BtnClose）
        ' --------------------------------

        ' 多选提示
        ToolTip1.SetToolTip(Dgw列表, "按住 Ctrl 或 Shift 可多选，Ctrl + A 全选，可一次插入多个零部件")

        ' 初始化 UI 布局
        SyncUILayout()

        ' --- DataGridView 初始化配置 ---
        With Dgw列表
            .AllowUserToAddRows = False       ' 禁止用户手动添加空行
            .AllowUserToDeleteRows = False    ' 禁止删除
            .ReadOnly = True                  ' 只读
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect ' 整行选中
            .MultiSelect = True               ' 允许多选，可一次插入多个零部件
            .ColumnHeadersVisible = True      ' 显示列头
            .RowHeadersVisible = False        ' 隐藏左侧行号

            ' 设置列
            .Columns.Clear()
            .Columns.Add("Name", "文件名")
            .Columns.Add("Type", "类型")
            .Columns.Add("Path", "路径") ' 这一列我们将隐藏，但用于存储数据

            ' 隐藏路径列，但保留数据
            .Columns("Path").Visible = False
            .Columns("Type").Visible = False

            ' 调整列宽
            .Columns("Name").Width = 1000
            '.Columns("Type").Width = 60

            ' 样式优化
            .DefaultCellStyle.SelectionBackColor = System.Drawing.Color.CornflowerBlue
            .DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        End With

        Dim oInventorAssemblyDocument As Inventor.AssemblyDocument
        Dim strActiveAssemblyPath As String

        ' 获取当前激活文档并验证
        Try
            oInventorAssemblyDocument = CType(ThisApplication.ActiveDocument, Inventor.AssemblyDocument)
            strActiveAssemblyPath = oInventorAssemblyDocument.FullDocumentName
        Catch ex As Exception
            MessageBox.Show("请先打开一个装配体文件。")
            Me.Close()
            Exit Sub
        End Try

        Dgw列表.Rows.Clear()

        For Each oDoc As Inventor.Document In ThisApplication.Documents.VisibleDocuments
            ' 跳过当前装配体本身
            If oDoc.FullDocumentName = strActiveAssemblyPath Or oDoc.FullDocumentName = "" Then Continue For

            ' 只处理零件和子装配
            If oDoc.DocumentType <> DocumentTypeEnum.kPartDocumentObject And
           oDoc.DocumentType <> DocumentTypeEnum.kAssemblyDocumentObject Then
                Continue For
            End If

            Dim strFileNameNoExt As String = System.IO.Path.GetFileNameWithoutExtension(oDoc.FullDocumentName)
            Dim strType As String = If(oDoc.DocumentType = DocumentTypeEnum.kPartDocumentObject, "零件", "装配")

            ' 添加行：Name, Type, Path
            Dgw列表.Rows.Add(strFileNameNoExt, strType, oDoc.FullDocumentName)
        Next

        ' 默认选中第一行
        If Dgw列表.Rows.Count > 0 Then
            Dgw列表.Rows(0).Selected = True
            Dgw列表.Focus()
        End If
    End Sub

    Private Sub Dgw列表_SelectionChanged(sender As Object, e As EventArgs) Handles Dgw列表.SelectionChanged
        ' 防止在初始化或清除数据时触发错误
        If Dgw列表.SelectedRows.Count = 0 Then
            oPendingThumbnailDocument = Nothing
            If ThumbnailTimer IsNot Nothing Then ThumbnailTimer.Stop()
            SetThumbnailImage(Nothing)
            Exit Sub
        End If

        ' 获取选中行的路径 (索引为 "Path" 的列)，多选时显示行号最小的一行
        Dim oSelectRow As DataGridViewRow = Dgw列表.SelectedRows(0)
        For Each oRow As DataGridViewRow In Dgw列表.SelectedRows
            If oRow.Index < oSelectRow.Index Then oSelectRow = oRow
        Next

        Dim strFullPath As String = oSelectRow.Cells("Path").Value.ToString()
        strSelectFileFullName = strFullPath

        Dim oInventorDocument As Inventor.Document = Nothing

        ' 通过路径查找对应的 Document 对象
        ' 优化：可以直接遍历 Documents，或者如果性能允许，使用 LINQ
        For Each oDoc As Inventor.Document In ThisApplication.Documents.VisibleDocuments
            If oDoc.FullDocumentName = strFullPath Then
                oInventorDocument = oDoc
                Exit For
            End If
        Next

        ' 延迟生成缩略图：连续切换选中行时只渲染最后一次
        oPendingThumbnailDocument = oInventorDocument
        If ThumbnailTimer IsNot Nothing Then
            ThumbnailTimer.Stop()
            ThumbnailTimer.Start()
        End If
    End Sub

    ''' <summary>
    ''' 延迟生成缩略图（防抖动）
    ''' </summary>
    Private Sub ThumbnailTimer_Tick(sender As Object, e As EventArgs)
        ThumbnailTimer.Stop()
        ShowThumbnail(oPendingThumbnailDocument)
    End Sub

    ''' <summary>
    ''' 显示指定文档的缩略图
    ''' </summary>
    Private Sub ShowThumbnail(ByVal oInventorDocument As Inventor.Document)
        If oInventorDocument Is Nothing Then
            SetThumbnailImage(Nothing)
            Exit Sub
        End If

        ' 采用与 clsBomTool 相同的方式：shell 缩略图接口
        ' 优先从 Inventor 文件二进制流提取内置预览 PNG，失败再用 shell 接口兜底
        Dim img As System.Drawing.Image = GetThumbnail(oInventorDocument.FullDocumentName,
                                                       PictureBox缩略图.ClientSize.Width,
                                                       PictureBox缩略图.ClientSize.Height)

        SetThumbnailImage(img)
    End Sub

    ''' <summary>
    ''' 设置图框显示的图片，并保持比例适应图框
    ''' </summary>
    Private Sub SetThumbnailImage(ByVal img As System.Drawing.Image)
        If img Is Nothing Then
            If PictureBox缩略图.Image IsNot Nothing Then
                PictureBox缩略图.Image.Dispose()
                PictureBox缩略图.Image = Nothing
            End If
            Exit Sub
        End If

        ' 释放旧图片防止内存泄漏
        If PictureBox缩略图.Image IsNot Nothing Then
            PictureBox缩略图.Image.Dispose()
        End If

        ' 保持比例，完整适应图框显示
        PictureBox缩略图.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox缩略图.Image = img
    End Sub

    ''' <summary>
    ''' 获取选中行的文件全名，按行号升序排列并去重
    ''' </summary>
    Private Function GetSelectFullFileNames() As Generic.List(Of String)
        Dim listFullFileNames As New Generic.List(Of String)

        Dim listRows As New Generic.List(Of DataGridViewRow)
        For Each oRow As DataGridViewRow In Dgw列表.SelectedRows
            listRows.Add(oRow)
        Next
        ' 按行号升序，保证插入顺序与列表显示顺序一致
        listRows.Sort(Function(row1 As DataGridViewRow, row2 As DataGridViewRow) row1.Index.CompareTo(row2.Index))

        For Each oRow As DataGridViewRow In listRows
            Dim strFullFileName As String = oRow.Cells("Path").Value.ToString()
            If Not listFullFileNames.Contains(strFullFileName) Then
                listFullFileNames.Add(strFullFileName)
            End If
        Next

        Return listFullFileNames
    End Function

    Private Sub btn插入到部件_Click(sender As Object, e As EventArgs) Handles btn插入到部件.Click
        If Dgw列表.SelectedRows.Count = 0 Then
            MessageBox.Show("请先选择一个或多个零部件。", AIPTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 检查当前是否为装配环境
        If ThisApplication.ActiveDocumentType <> DocumentTypeEnum.kAssemblyDocumentObject Then
            MessageBox.Show("请在装配体环境下运行此命令。", AIPTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 获取所有选中行的路径（支持多选）
        Dim listSelectFullFileNames As Generic.List(Of String) = GetSelectFullFileNames()

        Dim strActiveFullFileName As String = ThisApplication.ActiveDocument.FullDocumentName

        ' 验证文件，排除不存在的文件和当前装配体自身
        Dim listValidFullFileNames As New Generic.List(Of String)
        Dim strInvalidFiles As String = ""

        For Each strFullFileName As String In listSelectFullFileNames
            If Not System.IO.File.Exists(strFullFileName) Then
                strInvalidFiles &= strFullFileName & vbCrLf
                Continue For
            End If

            ' 防止将装配体插入自身
            If strFullFileName = strActiveFullFileName Then Continue For

            listValidFullFileNames.Add(strFullFileName)
        Next

        If strInvalidFiles <> "" Then
            MessageBox.Show("以下文件路径无效或文件不存在：" & vbCrLf & strInvalidFiles,
                            AIPTool, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If listValidFullFileNames.Count = 0 Then
            MessageBox.Show("没有可插入的零部件。", AIPTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim oControlDefinition As Inventor.ControlDefinition = Nothing

        Try
            ' 使用 PostPrivateEvent 触发放置组件命令
            oControlDefinition = ThisApplication.CommandManager.ControlDefinitions.Item("AssemblyPlaceComponentCmd")
        Catch ex As Exception
            MessageBox.Show("未找到放置零部件命令：" & ex.Message, AIPTool, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' 先关闭窗体，避免遮挡交互式放置
        Me.Close()

        Try
            ' 逐个放置选中的零部件：每投递一个文件名后同步执行一次放置命令
            For Each strFullFileName As String In listValidFullFileNames
                ThisApplication.CommandManager.PostPrivateEvent(PrivateEventTypeEnum.kFileNameEvent, strFullFileName)
                oControlDefinition.Execute2(True)
            Next
        Catch ex As Exception
            MessageBox.Show("插入组件时发生错误: " & ex.Message, AIPTool, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormPlaceOpenComponent_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' 清理资源
        If ThumbnailTimer IsNot Nothing Then
            ThumbnailTimer.Stop()
            ThumbnailTimer.Dispose()
            ThumbnailTimer = Nothing
        End If

        If PictureBox缩略图.Image IsNot Nothing Then
            PictureBox缩略图.Image.Dispose()
            PictureBox缩略图.Image = Nothing
        End If
        FormManager.CloseAndDisposeForm(Of FormPlaceOpenComponent)()
    End Sub

    Private Sub Dgw列表_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgw列表.CellDoubleClick
        If e.RowIndex >= 0 Then
            btn插入到部件_Click(sender, e)
        End If
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox缩略图_Click(sender As Object, e As EventArgs) Handles PictureBox缩略图.Click

    End Sub

    Private Sub Dgw列表_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgw列表.CellContentClick

    End Sub
    ' 当窗体大小改变时触发
    Private Sub FormPlaceOpenComponent_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ' 防止在窗体最小化或未初始化时报错
        If Me.WindowState = FormWindowState.Minimized Then Return

        SyncUILayout()
    End Sub

    ' 统一的布局调整函数
    Private Sub SyncUILayout()
        ' 确保控件已初始化
        If Dgw列表 Is Nothing OrElse PictureBox缩略图 Is Nothing Then Return

        ' 【关键】让 DataGridView 的高度等于 PictureBox 的高度
        Dgw列表.Height = PictureBox缩略图.Height

        ' 【可选】如果你希望 DataGridView 的宽度也随窗口变化，可以这样写：
        ' 假设左侧列表宽度固定为 200，右侧 PictureBox 占据剩余空间
        ' Dgw列表.Width = 200 
        ' PictureBox缩略图.Left = 200
        ' PictureBox缩略图.Width = Me.ClientSize.Width - 200

        ' 【可选】如果你希望 DataGridView 填满左侧，而 PictureBox 在右侧自适应：
        ' 这里假设你已经在 Designer 中设置了 Anchor 属性，通常不需要手动改宽度
        ' 但如果需要强制对齐顶部：
        Dgw列表.Top = PictureBox缩略图.Top
    End Sub


    Private Sub FormPlaceOpenComponent_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            btn插入到部件_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.A Then
            ' Ctrl + A 全选，方便批量插入
            Dgw列表.SelectAll()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ===== 缩略图：与 clsBomTool 相同的方式（shell 缩略图接口） =====

    ' Shell32 缩略图 COM 接口
    <ComImport()>
    <Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Private Interface IShellItemImageFactory
        Function GetImage(<[In]> sz As System.Drawing.Size,
                          <[In]> flags As UInteger,
                          <[Out]> ByRef phbm As IntPtr) As Integer
    End Interface

    <DllImport("shell32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SHCreateItemFromParsingName(
        pszPath As String, pbc As IntPtr, riid As System.Guid,
        ByRef ppv As IShellItemImageFactory) As Integer
    End Function

    <DllImport("gdi32.dll")>
    Private Shared Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    ' 优先：从 Inventor 文件二进制流直接提取内置预览图（PNG），不打开文档、不经过 COM、瞬间完成
    Private Function GetInventorThumbnail(filePath As String) As System.Drawing.Image
        Try
            If String.IsNullOrEmpty(filePath) OrElse Not System.IO.File.Exists(filePath) Then Return Nothing
            Dim allBytes() As Byte
            Using fs As New System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite)
                allBytes = New Byte(CInt(fs.Length) - 1) {}
                Dim off As Integer = 0
                Do
                    Dim n As Integer = fs.Read(allBytes, off, allBytes.Length - off)
                    If n <= 0 Then Exit Do
                    off += n
                Loop While off < allBytes.Length
            End Using
            Dim bestStart As Integer = -1
            Dim bestSize As Integer = 0
            Dim pos As Integer = 0
            Do
                Dim found As Integer = -1
                For i As Integer = pos To allBytes.Length - 8
                    If allBytes(i) = &H89 AndAlso allBytes(i + 1) = &H50 AndAlso allBytes(i + 2) = &H4E AndAlso allBytes(i + 3) = &H47 AndAlso
                       allBytes(i + 4) = &HD AndAlso allBytes(i + 5) = &HA AndAlso allBytes(i + 6) = &H1A AndAlso allBytes(i + 7) = &HA Then
                        found = i : Exit For
                    End If
                Next
                If found < 0 Then Exit Do
                Dim cp As Integer = found + 8
                Dim pngEnd As Integer = -1
                Dim cc As Integer = 0
                Do While cp + 8 < allBytes.Length AndAlso cc < 500
                    Dim cLen As Integer = (CInt(allBytes(cp)) << 24) Or (CInt(allBytes(cp + 1)) << 16) Or (CInt(allBytes(cp + 2)) << 8) Or CInt(allBytes(cp + 3))
                    If cLen < 0 OrElse cLen > 20 * 1024 * 1024 Then Exit Do
                    Dim ct As String = System.Text.Encoding.ASCII.GetString(allBytes, cp + 4, 4)
                    If ct = "IEND" Then pngEnd = cp + 12 : Exit Do
                    cp += 12 + cLen
                    cc += 1
                Loop
                If pngEnd > found Then
                    Dim sz As Integer = pngEnd - found
                    If sz > bestSize Then bestSize = sz : bestStart = found
                    pos = pngEnd
                Else
                    pos = found + 8
                End If
            Loop
            If bestStart < 0 OrElse bestSize < 100 Then Return Nothing
            Dim pngBytes(bestSize - 1) As Byte
            Array.Copy(allBytes, bestStart, pngBytes, 0, bestSize)
            Dim tmpPng As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "inv_" & System.IO.Path.GetFileNameWithoutExtension(filePath) & ".png")
            System.IO.File.WriteAllBytes(tmpPng, pngBytes)
            Dim bmp As New System.Drawing.Bitmap(tmpPng)
            Try : System.IO.File.Delete(tmpPng) : Catch : End Try
            Return bmp
        Catch : End Try
        Return Nothing
    End Function

    ' 兜底：Shell32 缩略图接口，按图框尺寸生成缩略图（纯 Win32 API，不经过 Inventor COM）
    Private Function GetShellThumbnail(filePath As String, w As Integer, h As Integer) As System.Drawing.Image
        Try
            If String.IsNullOrEmpty(filePath) OrElse Not System.IO.File.Exists(filePath) Then Return Nothing
            If w <= 0 Then w = 256
            If h <= 0 Then h = 256
            Dim iid As New System.Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")
            Dim factory As IShellItemImageFactory = Nothing
            Dim hr As Integer = SHCreateItemFromParsingName(filePath, IntPtr.Zero, iid, factory)
            If hr = 0 AndAlso factory IsNot Nothing Then
                Dim hBmp As IntPtr = IntPtr.Zero
                Dim sz As New System.Drawing.Size(w, h)
                Dim hr2 As Integer = factory.GetImage(sz, 0UI, hBmp)
                If hr2 = 0 AndAlso hBmp <> IntPtr.Zero Then
                    Dim rawBmp As System.Drawing.Bitmap = System.Drawing.Image.FromHbitmap(hBmp)
                    DeleteObject(hBmp)
                    Dim bmp As New System.Drawing.Bitmap(w, h)
                    Dim gfx As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
                    gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                    gfx.Clear(System.Drawing.Color.White)
                    gfx.DrawImage(rawBmp, 0, 0, w, h)
                    gfx.Dispose() : rawBmp.Dispose()
                    Return bmp
                End If
            End If
        Catch : End Try
        Return Nothing
    End Function

    ' 组合：优先取文件内置预览，失败再用 shell 接口兜底
    Private Function GetThumbnail(filePath As String, w As Integer, h As Integer) As System.Drawing.Image
        Dim img As System.Drawing.Image = GetInventorThumbnail(filePath)
        If img IsNot Nothing Then Return img
        Return GetShellThumbnail(filePath, w, h)
    End Function
End Class