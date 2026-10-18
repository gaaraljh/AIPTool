Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Inventor
Imports Inventor.AssetTypeEnum
Imports Inventor.BOMStructureEnum
Imports Inventor.DocumentTypeEnum
Imports Inventor.DrawingViewTypeEnum
Imports Inventor.IOMechanismEnum
Imports Inventor.PrintOrientationEnum
Imports Inventor.PropertyTypeEnum
Imports Inventor.SelectionFilterEnum
Imports System.Timers

Public Module PublicParameters
    Public Const AIPTool As String = "AIPTool"

    Public Const GitWeb As String = "https://gitcode.net/leaky114/inventoraddin"
    Public Const Bilibili As String = "https://space.bilibili.com/116515938"
    Public Const Github As String = "https://github.com/gaaraljh/AIPTool"

    Public strLargeSmallIconNames As String = "快速打开,按列表打开文件,保存关闭,关闭,打开工程图,提取iProperty,打开文件夹"
    Public strLargeSmallIconSets As String   '大小图标

    Public WithEvents m_timer As System.Timers.Timer    '计时器

    Public Structure RectangularPoint
        Dim TopLeft As Inventor.Point
        Dim TopRight As Inventor.Point
        Dim BottomRight As Inventor.Point
        Dim BottomLeft As Inventor.Point
        Dim Center As Inventor.Point
        Dim Length As Double
        Dim Width As Double
    End Structure

    Public Structure StockNumPartName
        Dim IsGet As Boolean
        Dim 图号 As String    '
        Dim 零件名称 As String      '
        Dim ERP编码 As String
        Dim 价格 As String
    End Structure

    Public Structure BalloonDate
        Dim Balloon As Balloon
        Dim Position As Point2d
        Dim Angles As Double
    End Structure

    Public ThisApplication As Inventor.Application

    'Public ThisApprenticeApp As Inventor.ApprenticeServerComponent   '学徒服务器

    Public WithEvents ThisApplicationEvents As ApplicationEvents

    Public ClientID As String

    Public DWG As String = ".dwg"
    Public IAM As String = ".iam"
    Public IPT As String = ".ipt"
    Public IDW As String = ".idw"
    Public OLD As String = ".old"
    Public PDF As String = ".pdf"
    Public STP As String = ".stp"
    Public DXF As String = ".dxf"
    Public IPN As String = ".ipn"

    Public BasicFilter As String = "Autodesk Inventor 文件(*.idw;*.iam;*.ipt;*.ipn)|*.idw;*.iam;*.ipt;*.ipn|
Autodesk Inventor 部件(*.iam)|*.iam|
Autodesk Inventor 零件(*.ipt)|*.ipt|
Autodesk Inventor 工程图(*.idw)|*.idw|
Autodesk Inventor 表达视图(*.ipn)|*.ipn|"

    Public ContentCenterFiles As String  '零件库文件夹

    Public DisplayVersion As String

    Public IsAutoSetPartName As Boolean  'true 为进行中，false则退出进程

    Public str查找文件夹层数 As Integer = "2" '返回父文件夹的层数

    Public Is检查重复图号 As Integer   '重命名时是否检查图号重复  '1为检查，0为不检查

    Public str快速打开 As String   ' 快速打开窗口 ， 双击列表item，0是直接打开  ，1 是不打开

    Public Map_DrawingNnumber As String   '映射图号
    Public Map_PartName As String   '映射文件名
    Public Map_ERPCode As String    '映射存货编码
    Public Map_Describe As String = "描述"     '映射描述

    Public str连接符 As String  '连接符
    Public char连接符 As String  '连接符

    Public str去除后缀表 As String     '去除后缀表
    Public str增加后缀 As String    '另存DWG 增加后缀
    Public str中间符号 As String    'BOM重命名中间符号

    Public Map_Mir_StochNum As String   '映射对称件图号
    Public Map_Mir_PartName As String   '映射对称件文件名
    Public Map_Mir_ERPCode As String   '映射对称件编码


    Public Map_DrawingScale As String '映射比例
    Public Map_Mass As String '映射质量

    Public Map_PrintDay As String '映射打印时间
    Public IsOpenPrint As String    '设置打印时间后是否进入打印预览
    Public IsDayAndName As String   '同时签字
    Public IsShortData As String   '短日期

    Public EngineerName As String '工程师

    Public Map_Vendor As String '供应商

    Public Map_Price As String = "成本" '价格

    Public BOMTiTle As String       '导出BOM用的项目


    Public oEncoding As Encoding   'bom导出编码
    Public strEncoding As String

    Public Mass_Accuracy As String '质量精度
    Public Area_Accuracy As String  '面积精度

    Public IsSetDrawingScale As String    '打开工程图时是否写 比例 到ipro   是赋值为1
    Public IsSetMass As String   '打开工程图时是否写 质量 到ipro  是赋值为1

    Public CheckUpdate As String    '启动检查更新

    Public TotalItem As Integer 'BOM序号

    Public OPosition(9) As Inventor.Point   '点
    Public TempPoint(9) As SketchPoint   '临时绘制的点

    Public IsShowUpdateMsg As Boolean    '检查更新时是否显示是最新版本的msgbox

    Public str模型匹配检查 As String   '工程图是否检查模型名  ：1 为检查   0 为不检查
    Public str模型匹配检查标记 As String    '标记打开工程图时，是否检查模型匹配：1为第一次检查，2为跳过检查,3为不检查

    Public str钣金厚度检查 As String      '钣金件工程图是否检查材料板厚是否匹配
    Public str钣金厚度前缀 As String   '钣金厚度前缀，是一个用，分割的数组


    '默认打印设值
    Public Printer As String   '默认打印机

    Public IsPaperA3 As Integer   '1：匹配A3纸，0：按原图纸大小打印
    Public IsSign As Integer       '1：签字，0：不签字
    Public SaveAsDawAndPdf As String

    '批量打印设置
    Public PrintSetting As String

    '快速打开选择的文件
    Public strQuitOpenSelectFileFullName As String


    '展开图设置
    Public str向上线型 As String
    Public str向上线宽 As String
    Public str向上颜色 As String

    Public str向下线型 As String
    Public str向下线宽 As String
    Public str向下颜色 As String

    Public str展开图标注 As String

    Public str展开图隐藏螺纹特征 As String
    Public str标记孔径上限 As String
    'Public str导出DXF As String
    Public str图号材质 As String

    'Public str工艺图范围X As String
    'Public str工艺图范围Y As String
    Public str工艺文字高 As String

    '工程图模板
    Public str工程图模板 As String
    Public strDWG工程图模板 As String

    '展开图模板
    Public str展开图模板 As String

    Public str自动展开图 As String
    Public str第三视角 As String
    Public str相切边 As String
    Public str螺纹特征 As String
    Public str样式 As String   '0 显示隐藏线,1 不显示隐藏线,2着色
    Public str标注尺寸 As String

    Public str保存展开图到指定文件夹 As String

    '工程图图框
    Public str部件图框 As String
    Public str零件图框 As String


    '变更工程图扩展名
    Public str变更工程图扩展名 As String

    '创建截图的宽度
    Public intPitcureWidth As Integer
    '创建截图的高度
    Public intPitcureHeight As Integer

    '自动保存
    Public str启用自动保存 As String
    Public str保存文档类型 As String
    Public str保存间隔时间 As String


    Public Structure 选择视图
        Dim str左视图 As String
        Dim str右视图 As String
        Dim str俯视图 As String
        Dim str仰视图 As String
    End Structure

    Public Structure 页边距
        Dim short上边距 As Double
        Dim short下边距 As Double
        Dim short左边距 As Double
        Dim short右边距 As Double
    End Structure

    Public str页边距 As 页边距
    Public str选择视图 As 选择视图

    Public str另存到子文件夹 As String
    Public str逆时针序号 As String
    Public str强制横向 As String

    Public int每行数量 As Integer = 8  ' 设置每行的数量
    Public int图框宽度 As Integer = 160  ' 设置每个PictureBox的宽度
    Public int图框高度 As Integer = 120  ' 设置每个PictureBox的高度
    Public int图框行间距 As Integer = 20 ' 设置行间距
    Public int图框列间距 As Integer = 20 ' 设置列间距


    '声明并初始化变量
    'Public _ListViewSorter As ClsListViewSorter.EnumSortOrder = ClsListViewSorter.EnumSortOrder.Ascending

    '-------------------------------------------------------------------------------------------------------

    '保存文件时的事件
    'Public Sub ThisApplicationEvents_OnOnSaveDocument(ByVal DocumentObject As Inventor._Document, _
    '                                                ByVal BeforeOrAfter As Inventor.EventTimingEnum, _
    '                                                 ByVal Context As Inventor.NameValueMap, _
    '                                                 ByRef HandlingCode As Inventor.HandlingCodeEnum) Handles ThisApplicationEvents.OnSaveDocument

    'End Sub

    '打开文件时的事件
    Public Sub ThisApplicationEvents_OnOpenDocument(ByVal oInventorDocument As Inventor.Document,
                                    ByVal FullDocumentName As String,
                                   ByVal BeforeOrAfter As Inventor.EventTimingEnum,
                                  ByVal Context As Inventor.NameValueMap,
                                  ByRef HandlingCode As Inventor.HandlingCodeEnum) Handles ThisApplicationEvents.OnOpenDocument


        Try
            '视图全部缩放，这个功能有问题，取消
            'ThisApplication.CommandManager.ControlDefinitions.Item("AppZoomAllCmd").Execute()

            If BeforeOrAfter = EventTimingEnum.kBefore Then
                ' MessageBox.Show("before")
                'str模型匹配检查标记 = 1
            Else

                If oInventorDocument.DocumentType = kPartDocumentObject Then
                    If str钣金厚度检查 = 1 Then

                        Dim IsMatching As Boolean
                        ' MessageBox.Show(oInventorDocument.FullDocumentName)

                        IsMatching = CheckSteelThicknessSub(oInventorDocument)

                        Select Case IsMatching
                            Case True

                            Case False
                                ' MessageBox.Show(FullDocumentName & "  材料与厚度不匹配。", MsgBoxStyle.Information)
                                ThisApplication.Documents.Open(FullDocumentName, True)
                        End Select


                    End If
                End If

                '当打开文件为工程图()
                If oInventorDocument.DocumentType = kDrawingDocumentObject Then
                    '写入主视图比例
                    'if IsSetDrawingScale = 1 Then
                    SetDrawingScale(oInventorDocument)
                    'End if

                    '写入零部件质量
                    'if IsSetMass = 1 Then
                    SetMass(oInventorDocument)
                    'End if

                    If str模型匹配检查 = 1 Then
                        Select Case str模型匹配检查标记
                            Case 1
                                If BeforeOrAfter = EventTimingEnum.kAfter Then
                                    If CheckDrawingDocumentNameToReferencedDocument(oInventorDocument) = False Then
                                        MessageBox.Show("本文件名与模型参考不匹配。", AIPTool， MessageBoxButtons.OK， MessageBoxIcon.Error）
                                    End If
                                End If
                                str模型匹配检查标记 = 2
                            Case 2
                                str模型匹配检查标记 = 1
                            Case 3

                        End Select
                    End If

                End If

            End If

        Catch __ex As Exception
            DbgLog("[Public_ThisApplicationEvents_OnOpenDocument] 未处理异常: " & __ex.GetType().Name & ": " & __ex.Message)
        End Try
    End Sub

    '激活一个文档时的事件
    Public Sub ThisApplicationEvents_OnActivateDocument(ByVal oInventorDocument As Inventor.Document,
                                                        ByVal BeforeOrAfter As Inventor.EventTimingEnum,
                                                        ByVal Context As Inventor.NameValueMap,
                                                        ByRef HandlingCode As Inventor.HandlingCodeEnum) Handles ThisApplicationEvents.OnActivateDocument
        Try
            If BeforeOrAfter = EventTimingEnum.kAfter Then

                '在标题栏中显示当前文档路径()
                ThisApplication.Caption = GetFileNameInfo(oInventorDocument.FullDocumentName).Folder & "\"


                '获取文件只读属性
                Dim oDef1 As ButtonDefinition
                oDef1 = ThisApplication.CommandManager.ControlDefinitions.Item("AIPToolInName文件只读")

                oDef1.Pressed = GetFileReadOnly(oInventorDocument.FullDocumentName)
            Else

            End If

        Catch __ex As Exception
            DbgLog("[Public_ThisApplicationEvents_OnActivateDocument] 未处理异常: " & __ex.GetType().Name & ": " & __ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 保存文件时
    ''' </summary>
    ''' <param name="oInventorDocument"></param>
    ''' <param name="BeforeOrAfter"></param>
    ''' <param name="Context"></param>
    ''' <param name="HandlingCode"></param>
    ''' <remarks></remarks>
    Public Sub ThisApplicationEvents_OnSaveDocument(ByVal oInventorDocument As Inventor.Document,
                                                           ByVal BeforeOrAfter As Inventor.EventTimingEnum,
                                                           ByVal Context As Inventor.NameValueMap,
                                                           ByRef HandlingCode As Inventor.HandlingCodeEnum) Handles ThisApplicationEvents.OnSaveDocument
        Try
            If BeforeOrAfter = EventTimingEnum.kBefore Then

            Else
                '文件已保存，清除其缩略图缓存，下次重新生成
                RemoveImageCache(oInventorDocument.FullDocumentName)

            'If str钣金厚度检查 = 1 Then
            '    CheckSteelThickness()
            'End If
        End If

        Catch __ex As Exception
            DbgLog("[Public_ThisApplicationEvents_OnSaveDocument] 未处理异常: " & __ex.GetType().Name & ": " & __ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 保存为副本
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SaveAsCopy()
        Dim oInventorDocument As Inventor.Document
        oInventorDocument = ThisApplication.ActiveDocument

        If IsInventorOpenDocument() = False Then
            Exit Sub
        End If

        Select Case oInventorDocument.DocumentType
            Case kAssemblyDocumentObject, kPartDocumentObject
                AsmIptDocumentSaveAs()
            Case kDrawingDocumentObject
                DrawingDocumentSaveAs()
        End Select

    End Sub

    ''' <summary>
    ''' 获取图片
    ''' </summary>
    ''' <param name="ColorRGB"></param>
    ''' <returns></returns>
    Public Function GetBitmap(ColorRGB() As Integer) As Bitmap
        Dim bmp As New Bitmap(32, 32)
        For x As Integer = 0 To bmp.Width - 1
            For y As Integer = 0 To bmp.Height - 1
                bmp.SetPixel(x, y, Drawing.Color.FromArgb(ColorRGB(0), ColorRGB(1), ColorRGB(2)))
            Next
        Next
        Return bmp
    End Function

    ''' <summary>
    ''' 将十六进制字符串转换为image
    ''' </summary>
    ''' <param name="str">十六进制字符串</param>
    ''' <returns>image</returns>
    Public Function GetImageFromString(Str As String) As Image
        Dim newImageBytes As Byte() = Enumerable.Range(0, Str.Length \ 2).[Select](Function(x) Convert.ToByte(Str.Substring(x * 2, 2), 16)).ToArray()
        Using stream As New IO.MemoryStream(newImageBytes)
            Return Image.FromStream(stream)
        End Using
    End Function


    ''' <summary>
    ''' 对比返回 图标 大小
    ''' </summary>
    ''' <param name="strButtonName">按钮的 名称</param>
    ''' <returns></returns>
    Public Function GetIconSetByButtonName(ByVal strButtonName As String) As Boolean
        '' 定义图标名称字符串
        'Dim strLargeSmallIconNames As String = "快速打开,按列表打开文件,保存关闭,关闭,打开工程图,提取iProperty,打开文件夹"
        '' 定义图标集合字符串
        'Dim strLargeSmallIconSets As String = "大大大大大大大"

        ' 将图标名称字符串按逗号分割成数组
        Dim iconNames() As String = Split(strLargeSmallIconNames, ",")

        ' 找到输入参数在数组中的索引
        Dim index As Integer = Array.IndexOf(iconNames, strButtonName)

        ' 如果找到索引，则返回对应的图标集合字符，否则返回空字符串或其他错误信息
        If index <> -1 Then
            Dim nthChar As String
            nthChar = Mid(strLargeSmallIconSets, 2 * index + 1, 1)

            Debug.Print(nthChar)

            Return IIf(nthChar = "大", True, False)
        Else
            Return True ' 或者你可以返回一个错误信息，比如 "参数未找到"
        End If
    End Function

    ' 缩略图缓存：Key = 文件全名|宽x高，避免同一文档重复渲染
    Private ReadOnly ImageCache As New Generic.Dictionary(Of String, Drawing.Bitmap)
    Private Const ImageCacheMaxCount As Integer = 40

    ''' <summary>
    ''' 清除指定文档的缩略图缓存（保存文件后调用，避免显示旧缩略图）
    ''' </summary>
    ''' <param name="strFullFileName">文件全名</param>
    Public Sub RemoveImageCache(ByVal strFullFileName As String)
        If String.IsNullOrEmpty(strFullFileName) Then Exit Sub

        Dim listKeys As New Generic.List(Of String)

        For Each strKey As String In ImageCache.Keys
            If strKey.StartsWith(strFullFileName & "|", StringComparison.OrdinalIgnoreCase) Then
                listKeys.Add(strKey)
            End If
        Next

        For Each strKey As String In listKeys
            ImageCache(strKey).Dispose()
            ImageCache.Remove(strKey)
        Next
    End Sub

    ''' <summary>
    ''' 清空全部缩略图缓存
    ''' </summary>
    Public Sub ClearImageCache()
        For Each oBitmap As Drawing.Bitmap In ImageCache.Values
            oBitmap.Dispose()
        Next
        ImageCache.Clear()
    End Sub

    ' 内置预览图磁盘缓存目录
    Private ReadOnly ThumbnailCacheFolder As String = IO.Path.Combine(IO.Path.GetTempPath(), "AIPToolThumbCache")

    ''' <summary>
    ''' 从 Inventor 文件二进制流中直接提取内置的预览图（PNG），无需打开或渲染文档，取到的是文件中最大的一张
    ''' </summary>
    ''' <param name="strFullFileName">文件全名</param>
    ''' <returns>预览图，取不到返回Nothing</returns>
    ''' <remarks></remarks>
    Public Function GetEmbeddedThumbnail(ByVal strFullFileName As String) As Drawing.Bitmap
        Try
            If String.IsNullOrEmpty(strFullFileName) OrElse Not IO.File.Exists(strFullFileName) Then Return Nothing

            '磁盘缓存，避免每次重新解析文件
            If Not IO.Directory.Exists(ThumbnailCacheFolder) Then IO.Directory.CreateDirectory(ThumbnailCacheFolder)

            Dim strCacheKey As String = IO.Path.GetFileNameWithoutExtension(strFullFileName) & "_" &
                                        New IO.FileInfo(strFullFileName).LastWriteTime.Ticks.ToString() & ".png"
            Dim strCachePath As String = IO.Path.Combine(ThumbnailCacheFolder, strCacheKey)

            If IO.File.Exists(strCachePath) Then
                Try
                    Using oCacheStream As New IO.MemoryStream(IO.File.ReadAllBytes(strCachePath))
                        Using oCacheImage As Drawing.Image = Drawing.Image.FromStream(oCacheStream)
                            Return New Drawing.Bitmap(oCacheImage)
                        End Using
                    End Using
                Catch ex As Exception
                End Try
            End If

            '读取文件全部字节
            Dim allBytes() As Byte

            Using oFileStream As New IO.FileStream(strFullFileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
                If oFileStream.Length <= 0 Then Return Nothing

                allBytes = New Byte(CInt(oFileStream.Length) - 1) {}
                Dim intOffset As Integer = 0

                Do
                    Dim intRead As Integer = oFileStream.Read(allBytes, intOffset, allBytes.Length - intOffset)
                    If intRead <= 0 Then Exit Do
                    intOffset += intRead
                Loop While intOffset < allBytes.Length
            End Using

            '查找文件中所有PNG数据块，取体积最大的一个（即分辨率最高的预览图）
            Dim intBestStart As Integer = -1
            Dim intBestSize As Integer = 0
            Dim intPos As Integer = 0

            Do
                Dim intFound As Integer = -1

                For i As Integer = intPos To allBytes.Length - 8
                    If allBytes(i) = &H89 AndAlso allBytes(i + 1) = &H50 AndAlso
                       allBytes(i + 2) = &H4E AndAlso allBytes(i + 3) = &H47 AndAlso
                       allBytes(i + 4) = &HD AndAlso allBytes(i + 5) = &HA AndAlso
                       allBytes(i + 6) = &H1A AndAlso allBytes(i + 7) = &HA Then
                        intFound = i
                        Exit For
                    End If
                Next

                If intFound < 0 Then Exit Do

                '遍历PNG数据块，找到IEND结束位置
                Dim intCursor As Integer = intFound + 8
                Dim intPngEnd As Integer = -1
                Dim intCount As Integer = 0

                Do While intCursor + 8 < allBytes.Length AndAlso intCount < 500
                    Dim intChunkLength As Integer = (CInt(allBytes(intCursor)) << 24) Or
                                                    (CInt(allBytes(intCursor + 1)) << 16) Or
                                                    (CInt(allBytes(intCursor + 2)) << 8) Or
                                                     CInt(allBytes(intCursor + 3))

                    If intChunkLength < 0 OrElse intChunkLength > 20 * 1024 * 1024 Then Exit Do

                    Dim strChunkType As String = System.Text.Encoding.ASCII.GetString(allBytes, intCursor + 4, 4)

                    If strChunkType = "IEND" Then
                        intPngEnd = intCursor + 12
                        Exit Do
                    End If

                    intCursor += 12 + intChunkLength
                    intCount += 1
                Loop

                If intPngEnd > intFound Then
                    Dim intSize As Integer = intPngEnd - intFound

                    If intSize > intBestSize Then
                        intBestSize = intSize
                        intBestStart = intFound
                    End If

                    intPos = intPngEnd
                Else
                    intPos = intFound + 8
                End If
            Loop

            If intBestStart < 0 OrElse intBestSize < 100 Then Return Nothing

            Dim pngBytes(intBestSize - 1) As Byte
            Array.Copy(allBytes, intBestStart, pngBytes, 0, intBestSize)

            Dim oBitmap As Drawing.Bitmap = Nothing

            Using oStream As New IO.MemoryStream(pngBytes)
                Using oTempImage As Drawing.Image = Drawing.Image.FromStream(oStream)
                    oBitmap = New Drawing.Bitmap(oTempImage)
                End Using
            End Using

            '写入磁盘缓存
            If oBitmap IsNot Nothing Then
                Try
                    oBitmap.Save(strCachePath, System.Drawing.Imaging.ImageFormat.Png)
                Catch ex As Exception
                End Try
            End If

            Return oBitmap
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 用文档视图渲染缩略图（缩放至适应图框，无内置预览图时作兜底）
    ''' </summary>
    ''' <param name="oInventorDocument">文件对象</param>
    ''' <param name="intWidth">宽度</param>
    ''' <param name="intHeight">高度</param>
    ''' <returns>缩略图</returns>
    ''' <remarks></remarks>
    Public Function RenderDocumentThumbnail(ByVal oInventorDocument As Inventor.Document,
                                            ByVal intWidth As Integer,
                                            ByVal intHeight As Integer) As Drawing.Bitmap
        Try
            '使用固定名称的 bmp 临时文件，避免 jpg 压缩与解码的开销
            Dim tempFile As String = IO.Path.Combine(IO.Path.GetTempPath(),
                                                     "AIPToolThumbnail" & intWidth & "x" & intHeight & ".bmp")

            Dim oActiveView As Inventor.View
            oActiveView = oInventorDocument.Views.Item(1)

            Dim oCamera As Camera
            oCamera = oActiveView.Camera

            '保存当前视图相机位置，生成缩略图后恢复，避免改变用户当前视图
            Dim oOldEye As Inventor.Point = Nothing
            Dim oOldTarget As Inventor.Point = Nothing
            Dim oOldUpVector As UnitVector = Nothing

            Try
                Dim oTransientGeometry As TransientGeometry = ThisApplication.TransientGeometry
                oOldEye = oTransientGeometry.CreatePoint(oCamera.Eye.X, oCamera.Eye.Y, oCamera.Eye.Z)
                oOldTarget = oTransientGeometry.CreatePoint(oCamera.Target.X, oCamera.Target.Y, oCamera.Target.Z)
                oOldUpVector = oTransientGeometry.CreateUnitVector(oCamera.UpVector.X, oCamera.UpVector.Y, oCamera.UpVector.Z)
            Catch ex As Exception
            End Try

            Try
                '缩放至适应图框大小，使用无动画过渡，比 Apply 快得多
                oCamera.Fit()
                oCamera.ApplyWithoutTransition()

                oCamera.SaveAsBitmap(tempFile, intWidth, intHeight)

                '恢复原视图
                If oOldEye IsNot Nothing Then
                    oCamera.Eye = oOldEye
                    oCamera.Target = oOldTarget
                    oCamera.UpVector = oOldUpVector
                    oCamera.ApplyWithoutTransition()
                End If
            Catch ex As Exception
            End Try

            '一次解码并复制，解除对临时文件的占用
            Dim oBitmap As Drawing.Bitmap = Nothing

            Try
                Using oTempImage As Drawing.Image = Drawing.Image.FromFile(tempFile)
                    oBitmap = New Drawing.Bitmap(oTempImage)
                End Using
            Catch ex As Exception
                oBitmap = Nothing
            End Try

            Try
                IO.File.Delete(tempFile)
            Catch ex As Exception
            End Try

            Return oBitmap
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 获取文档的缩略图（优先读取文件内置的最大预览图，带缓存）
    ''' </summary>
    ''' <param name="oInventorDocument">文件对象</param>
    ''' <param name="intWidth">兜底渲染时使用的宽度，默认400</param>
    ''' <param name="intHeight">兜底渲染时使用的高度，默认300</param>
    ''' <returns>缩略图</returns>
    ''' <remarks></remarks>
    Public Function GetImageFromView(ByVal oInventorDocument As Inventor.Document,
                                     Optional ByVal intWidth As Integer = 400,
                                     Optional ByVal intHeight As Integer = 300) As Drawing.Image

        If oInventorDocument Is Nothing Then Return Nothing

        If intWidth <= 0 Then intWidth = 400
        If intHeight <= 0 Then intHeight = 300

        Dim strKey As String = oInventorDocument.FullDocumentName & "|" & intWidth & "x" & intHeight

        '命中缓存直接返回副本，省去渲染时间
        If ImageCache.ContainsKey(strKey) Then
            Return New Drawing.Bitmap(ImageCache(strKey))
        End If

        '1、优先读取文件内置预览图：不需渲染、速度最快，且是文件中尺寸最大的一张
        Dim oBitmap As Drawing.Bitmap = GetEmbeddedThumbnail(oInventorDocument.FullDocumentName)

        '2、兜底：文件没有内置预览图时，用文档视图渲染
        If oBitmap Is Nothing Then
            oBitmap = RenderDocumentThumbnail(oInventorDocument, intWidth, intHeight)
        End If

        If oBitmap Is Nothing Then Return Nothing

        '写入缓存，返回副本，调用方释放不影响缓存
        If ImageCache.Count >= ImageCacheMaxCount Then ClearImageCache()
        ImageCache(strKey) = oBitmap

        Return New Drawing.Bitmap(oBitmap)
    End Function


    ''' <summary>
    ''' 自动保存文档计时器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    ''' <summary>
    ''' 诊断日志（.NET 8 迁移排障用，定位完成后可删除）。
    ''' .NET 8 下任何线程上的未处理异常都会直接终止进程，因此所有回调都必须包 Try/Catch 并记日志。
    ''' </summary>
    Private m_logHeaderDone As Boolean = False

    Public Sub DbgLog(ByVal msg As String)
        Try
            Dim logPath As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "AIPTool_debug.log")
            If Not m_logHeaderDone Then
                m_logHeaderDone = True
                Dim head As String = "==== PID " & System.Diagnostics.Process.GetCurrentProcess().Id.ToString() &
                                     "  " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " =====" & vbCrLf
                Dim hb() As Byte = System.Text.Encoding.UTF8.GetBytes(head)
                Using hfs As New System.IO.FileStream(logPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite)
                    hfs.Write(hb, 0, hb.Length)
                End Using
            End If
            Dim line As String = DateTime.Now.ToString("HH:mm:ss.fff") & " " & msg & vbCrLf
            Dim buf() As Byte = System.Text.Encoding.UTF8.GetBytes(line)
            Using fs As New System.IO.FileStream(logPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite)
                fs.Write(buf, 0, buf.Length)
            End Using
        Catch : End Try
    End Sub
    Public Sub m_timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles m_timer.Elapsed
        Try
        AutoSaveDocument()
        Catch __ex As Exception
            DbgLog("[Public_m_timer_Elapsed] 未处理异常: " & __ex.GetType().Name & ": " & __ex.Message)
        End Try
    End Sub

End Module
