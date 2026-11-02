Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Inventor

''' <summary>
''' 插入打开的零部件。
''' 逻辑移植自 C# 参考项目 PlaceOpenComponent：
'''   - GetOpenDocuments：遍历 Documents（所有已打开文档），只取零件/装配，且排除当前激活文档；
'''   - GetThumbnail   ：优先取 iProperties 的 Summary Information / Thumbnail（IPictureDisp），
'''                       失败时按类型给默认图标（部件/零件）；
'''   - 放置           ：PostPrivateEvent(kFileNameEvent, 路径) + AssemblyPlaceComponentCmd.Execute2(True)。
''' </summary>
Public Class FormPlaceOpenComponent

    ''' <summary>打开的零部件信息（对应 C# 的 InvDoc）</summary>
    Private Class InvDoc
        Public ReadOnly Name As String
        Public ReadOnly FilePath As String
        Public ReadOnly Thumbnail As Image

        Public Sub New(name As String, filePath As String, thumbnail As Image)
            Me.Name = name
            Me.FilePath = filePath
            Me.Thumbnail = thumbnail
        End Sub
    End Class

    Public strSelectFileFullName As String

    Public Sub FormPlaceOpenComponent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.插入零部件32
        Me.TopMost = True
        Dgw列表.AllowUserToResizeRows = False

        ' 快捷键：Enter 插入、ESC 关闭
        Me.AcceptButton = btn插入到部件
        Me.CancelButton = BtnClose

        ToolTip1.SetToolTip(Dgw列表, "按住 Ctrl 或 Shift 可多选，Ctrl + A 全选，可一次插入多个零部件")

        SyncUILayout()

        With Dgw列表
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False

            .Columns.Clear()
            .Columns.Add("Name", "文件名")
            .Columns.Add("Type", "类型")
            .Columns.Add("Path", "路径")
            .Columns("Path").Visible = False
            .Columns("Type").Visible = False
            .Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            .DefaultCellStyle.SelectionBackColor = System.Drawing.Color.CornflowerBlue
            .DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        End With

        Dgw列表.Rows.Clear()

        ' ===== 对应 C# Extensions.GetOpenDocuments =====
        ' 必须用 VisibleDocuments：它只包含"有窗口、真正被打开"的文档；
        ' Documents 会把装配体内部自动加载的引用零件也包含进来，导致列出全部零部件。
        Dim oActiveDoc As Inventor.Document = Nothing
        Dim strActivePath As String = ""
        Try
            oActiveDoc = ThisApplication.ActiveDocument
            If oActiveDoc IsNot Nothing Then strActivePath = oActiveDoc.FullFileName
        Catch
        End Try

        For Each oDoc As Inventor.Document In ThisApplication.Documents.VisibleDocuments
            ' 单个文档出错不能中断整个列表
            Try
                If oDoc.DocumentType = DocumentTypeEnum.kPartDocumentObject OrElse
                   oDoc.DocumentType = DocumentTypeEnum.kAssemblyDocumentObject Then

                    Dim strFile As String = ""
                    Try : strFile = oDoc.FullFileName : Catch : End Try

                    ' 排除当前激活文档（引用比较 + 路径比较双保险）
                    Dim isActive As Boolean = False
                    If oActiveDoc IsNot Nothing AndAlso oDoc Is oActiveDoc Then isActive = True
                    If strActivePath <> "" AndAlso String.Equals(strFile, strActivePath, StringComparison.OrdinalIgnoreCase) Then isActive = True

                    If Not isActive AndAlso strFile <> "" Then
                        ' 显示名去掉扩展名
                        Dim strName As String = System.IO.Path.GetFileNameWithoutExtension(strFile)
                        If strName = "" Then strName = oDoc.DisplayName

                        Dim strType As String = If(oDoc.DocumentType = DocumentTypeEnum.kPartDocumentObject, "零件", "装配")
                        Dim img As Image = GetDocThumbnail(oDoc)

                        Dim intRow As Integer = Dgw列表.Rows.Add(strName, strType, strFile)
                        Dgw列表.Rows(intRow).Tag = New InvDoc(strName, strFile, img)
                    End If
                End If
            Catch
            End Try
        Next

        ' 默认选中第一行，并显式刷新缩略图
        ' （Rows.Add 时的 SelectionChanged 早于 Tag 赋值，且已选中时再设 Selected=True 不会触发事件）
        If Dgw列表.Rows.Count > 0 Then
            Dgw列表.ClearSelection()
            Dgw列表.CurrentCell = Dgw列表.Rows(0).Cells(0)
            Dgw列表.Rows(0).Selected = True
            ShowSelectedThumbnail()
            Dgw列表.Focus()
        End If
    End Sub

    ''' <summary>
    ''' 对应 C# Extensions.GetThumbnail：
    ''' 优先从 iProperties(Summary Information/Thumbnail) 取 IPictureDisp 转 Image；
    ''' 失败时按文档类型返回默认图标。
    ''' </summary>
    Private Function GetDocThumbnail(ByVal oDoc As Inventor.Document) As Image
        ' 1) 优先取文档内置缩略图
        Try
            Dim oValue As Object = oDoc.PropertySets.Item("Summary Information").Item("Thumbnail").Value
            If oValue IsNot Nothing Then
                Dim oPicture As stdole.IPictureDisp = TryCast(oValue, stdole.IPictureDisp)
                If oPicture IsNot Nothing Then
                    Dim img As Image = ClsPictureConverter.PictureDispToImage(oPicture)
                    If img IsNot Nothing Then Return img
                End If
            End If
        Catch
        End Try

        ' 2) 失败时按类型给默认图标（对应 C# 的 Resources.iam.png / Resources.ipt.png）
        Try
            If oDoc.DocumentType = DocumentTypeEnum.kAssemblyDocumentObject Then
                Return My.Resources.部件32.ToBitmap()
            Else
                Return My.Resources.零件32.ToBitmap()
            End If
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub Dgw列表_SelectionChanged(sender As Object, e As EventArgs) Handles Dgw列表.SelectionChanged
        ShowSelectedThumbnail()
    End Sub

    ''' <summary>显示当前选中行（多选时取行号最小的一行）的缩略图</summary>
    Private Sub ShowSelectedThumbnail()
        If Dgw列表.SelectedRows.Count = 0 Then
            SetThumbnailImage(Nothing)
            Exit Sub
        End If

        ' 多选时显示行号最小的一行
        Dim oSelectRow As DataGridViewRow = Dgw列表.SelectedRows(0)
        For Each oRow As DataGridViewRow In Dgw列表.SelectedRows
            If oRow.Index < oSelectRow.Index Then oSelectRow = oRow
        Next

        Dim invDoc As InvDoc = TryCast(oSelectRow.Tag, InvDoc)
        If invDoc Is Nothing Then
            SetThumbnailImage(Nothing)
            Exit Sub
        End If

        strSelectFileFullName = invDoc.FilePath
        SetThumbnailImage(invDoc.Thumbnail)
    End Sub

    ''' <summary>设置缩略图（缩略图已缓存在 InvDoc 中，切换时不再重复生成）</summary>
    Private Sub SetThumbnailImage(ByVal img As Image)
        PictureBox缩略图.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox缩略图.Image = img
    End Sub

    ''' <summary>获取选中行的文件全名，按行号升序排列并去重</summary>
    Private Function GetSelectFullFileNames() As List(Of String)
        Dim listFullFileNames As New List(Of String)

        Dim listRows As New List(Of DataGridViewRow)
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
            MessageBox.Show("请先选择一个或多个零部件。", GLTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 检查当前是否为装配环境
        If ThisApplication.ActiveDocumentType <> DocumentTypeEnum.kAssemblyDocumentObject Then
            MessageBox.Show("请在装配体环境下运行此命令。", GLTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 获取所有选中行的路径（支持多选）
        Dim listSelectFullFileNames As List(Of String) = GetSelectFullFileNames()

        Dim strActiveFullFileName As String = ThisApplication.ActiveDocument.FullFileName

        ' 验证文件，排除不存在的文件和当前装配体自身
        Dim listValidFullFileNames As New List(Of String)
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
                            GLTool, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If listValidFullFileNames.Count = 0 Then
            MessageBox.Show("没有可插入的零部件。", GLTool, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim oControlDefinition As Inventor.ControlDefinition = Nothing

        Try
            ' 使用 PostPrivateEvent 触发放置组件命令（与 C# 参考项目一致）
            oControlDefinition = ThisApplication.CommandManager.ControlDefinitions.Item("AssemblyPlaceComponentCmd")
        Catch ex As Exception
            MessageBox.Show("未找到放置零部件命令：" & ex.Message, GLTool, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("插入组件时发生错误: " & ex.Message, GLTool, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormPlaceOpenComponent_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' 释放缩略图，避免 GDI 资源泄漏（同一张图只释放一次）
        Try
            If Dgw列表 IsNot Nothing Then
                Dim disposedImages As New HashSet(Of Image)()
                For Each oRow As DataGridViewRow In Dgw列表.Rows
                    Dim invDoc As InvDoc = TryCast(oRow.Tag, InvDoc)
                    If invDoc IsNot Nothing AndAlso invDoc.Thumbnail IsNot Nothing AndAlso disposedImages.Add(invDoc.Thumbnail) Then
                        invDoc.Thumbnail.Dispose()
                    End If
                Next
            End If
        Catch
        End Try

        PictureBox缩略图.Image = Nothing
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

    Private Sub FormPlaceOpenComponent_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then Return
        SyncUILayout()
    End Sub

    ' 统一的布局调整函数
    Private Sub SyncUILayout()
        If Dgw列表 Is Nothing OrElse PictureBox缩略图 Is Nothing Then Return

        ' 缩略图始终为正方形（以较小边为准）
        Dim intSide As Integer = Math.Min(PictureBox缩略图.Width, PictureBox缩略图.Height)
        If intSide > 0 Then
            PictureBox缩略图.Width = intSide
            PictureBox缩略图.Height = intSide
        End If

        ' 左侧列表与右侧缩略图保持相同尺寸，并对齐顶部
        Dgw列表.Top = PictureBox缩略图.Top
        Dgw列表.Width = PictureBox缩略图.Width
        Dgw列表.Height = PictureBox缩略图.Height
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

End Class
