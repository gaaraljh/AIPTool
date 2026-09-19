
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

Public Class clsBomTool

	' 应用程序图标（从 Inventor 主程序提取，供各对话框共用）
	Dim g_appIcon As System.Drawing.Icon = Nothing







	' ╔══════════════════════════════════════════════════════════════════════╗
	' ║  BOM工具 工具  —  Inventor iLogic 外部规则（VB.NET WinForms / GDI+）  ║
	' ║  功能：BOM 采集、属性批量操作、图号写入、工程图打印、PDF/DWG 导出、  ║
	' ║        Excel 导入导出、主题系统、缩略图、俄罗斯方块                   ║
	' ╠══════════════════════════════════════════════════════════════════════╣
	' ║  文件结构目录（行号为大致位置，便于快速定位）                        ║
	' ║                                                                      ║
	' ║  一、Win32 P/Invoke 与接口声明 ............ L18 - L130               ║
	' ║      shell32/gdi32/user32 API、SHBrowseForFolder、WMF/EMF            ║
	' ║  二、DPI 缩放与全局常量 ................... L110 - L238              ║
	' ║      GetDpiScale / Scaled / InitScaledConstants                      ║
	' ║  三、字体、占位符工具 ..................... L239 - L306              ║
	' ║      GetSafeFontName / SetPlaceholder                                ║
	' ║  四、功能对话框（图号/工程图写入） ........ L307 - L1762             ║
	' ║      ShowAutoDrawingNoDialog / ShowWritePropToDrawingDialog          ║
	' ║  五、属性读写安全封装 ..................... L1763 - L1789             ║
	' ║      SafeWriteUDP / GetUDP                                          ║
	' ║  六、缩略图弹窗与网格滚动 ................. L1790 - L1852             ║
	' ║      ShowThumbPopup / OnGridMouseWheel / OnGridScroll                 ║
	' ║  七、零件检测与工具函数 ................... L1853 - L2219             ║
	' ║      IsMirrorPart / FindDrawingInProject / GetDocInfo / LoadTypeIcons ║
	' ║  八、BOM 数据转换与网格刷新 ............... L2220 - L2488             ║
	' ║      BomStructToStr / RefreshGridRows / OnTypeCellPainting            ║
	' ║  九、ini 设置读写 ......................... L2580 - L2897             ║
	' ║      GetSettingsPath / ReadSection / SaveSettings / LoadSettings      ║
	' ║  十、文件重命名 ........................... L2909 - L3028             ║
	' ║      RenameFileWithDrawing                                          ║
	' ║  十一、Excel 导出与回写 ................... L3033 - L3552             ║
	' ║      OnExportBomExcel / WriteXlsxToInventor / XLSX ZIP 操作           ║
	' ║  十二、颜色/字体工厂函数 .................. L3572 - L3698             ║
	' ║      OrangeMid / PanelBg / ToolBarBg / C / MakeFont                   ║
	' ║  十三、主题系统（9主题/呼吸灯/暗黑/自定义） ...... L3699 - L4122             ║
	' ║      ApplyThemeToForm / StartBreathTheme / BreathTick / PnlTopPaint   ║
	' ║  十四、缩略图生成（HQ/Shell/Inventor） ..... L4123 - L4312             ║
	' ║      GetInventorThumbnail / GetHQThumbnail / CleanThumbnailTemp       ║
	' ║  十五、网格事件（排序/点击/复制粘贴） ..... L4313 - L4904             ║
	' ║      OnGridSortCompare / OnCellClick / OnGridCopy / OnGridPaste       ║
	' ║  十六、全量刷新与缩略图线程 ............... L4905 - L5080             ║
	' ║      DoFullRefresh / GenerateThumbnails                              ║
	' ║  ★ Main 入口（窗体创建/UI布局/事件绑定） .. L5081 - L6196             ║
	' ║  十七、单元格值变更与行列尺寸 ............. L6197 - L6450             ║
	' ║      OnCellValueChanged / OnApplyRowHeight / OnAutoAllColWidth        ║
	' ║  十八、列构建与列管理 ..................... L6451 - L6853             ║
	' ║      BuildGridColumn / AddCheckRow / OnAddCustomCol / OnRemoveCol     ║
	' ║  十九、属性写入磁盘 ....................... L6854 - L7480             ║
	' ║      FastWriteRow / OnWriteClick                                     ║
	' ║  二十、数据采集与层级编号 ................. L7525 - L7737             ║
	' ║      CollectRows / CalcLevelNumbers                                  ║
	' ║  二十一、批量属性操作对话框 ............... L7738 - L8601             ║
	' ║      ShowReplaceValueDialog / ShowRenamePropDialog /                 ║
	' ║      ShowBatchDeletePropDialog                                       ║
	' ║  二十二、庆祝烟花 ......................... L8602 - L8719             ║
	' ║  二十三、打印/拼接/导出/数量统计对话框 .... L8720 - L10685            ║
	' ║      ShowPrintAssemblyDialog / ShowPropConcatDialog /                ║
	' ║      ShowExportByPropDialog / ShowBOMQuantityDialog /                ║
	' ║      CollectIdwFiles / PrintOneDrawing                               ║
	' ║  二十四、俄罗斯方块 ....................... L10686 - L11093           ║
	' ╚══════════════════════════════════════════════════════════════════════╝
	<ComImport>
	<Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")>
	<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
	Interface IShellItemImageFactory
		Function GetImage(<[In]> sz As System.Drawing.Size,
		<[In]> flags As UInteger,
		<Out> ByRef phbm As IntPtr) As Integer
	End Interface

	<DllImport("shell32.dll", CharSet:=CharSet.Unicode)>
	Shared Function SHCreateItemFromParsingName(
		pszPath As String, pbc As IntPtr, riid As System.Guid,
		ByRef ppv As IShellItemImageFactory) As Integer
	End Function

	<DllImport("gdi32.dll")>
	Shared Function DeleteObject(hObject As IntPtr) As Boolean
	End Function



	<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
	Structure SHFILEINFO2
		Public hIcon As IntPtr
		Public iIcon As Integer
		Public dwAttributes As UInteger
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
		Public szDisplayName As String
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
		Public szTypeName As String
	End Structure

	Const SHGFI_ICON2 As UInteger = &H100
	Const SHGFI_LARGEICON2 As UInteger = &H0
	Const SHGFI_USEFILEATTRIBUTES2 As UInteger = &H10
	Const FILE_ATTRIBUTE_NORMAL2 As UInteger = &H80

	<DllImport("shell32.dll", CharSet:=CharSet.Auto)>
	Shared Function SHGetFileInfo2(pszPath As String, dwFileAttributes As UInteger,
		ByRef psfi As SHFILEINFO2, cbSizeFileInfo As UInteger, uFlags As UInteger) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)>
	Shared Function DestroyIcon2(hIcon As IntPtr) As Boolean
	End Function


	<DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="GetClassLongPtrW")>
	Shared Function GetClassLongPtrW(hWnd As IntPtr, nIndex As Integer) As IntPtr
	End Function

	' ── WMF/EMF P/Invoke ──'
	<DllImport("gdi32.dll")>
	Shared Function CopyMetaFile(hmf As IntPtr, lpszFile As String) As IntPtr
	End Function
	<DllImport("gdi32.dll")>
	Shared Function PlayMetaFile(hdc As IntPtr, hmf As IntPtr) As Boolean
	End Function
	<DllImport("gdi32.dll")>
	Shared Function DeleteMetaFile(hmf As IntPtr) As Boolean
	End Function
	<DllImport("gdi32.dll")>
	Shared Function GetMetaFileBitsEx(hmf As IntPtr, nSize As UInteger, lpvData As IntPtr) As UInteger
	End Function
	<DllImport("gdi32.dll")>
	Shared Function SetWinMetaFileBits(nSize As UInteger, lpMeta16Data() As Byte, hdcRef As IntPtr, lpMFP As IntPtr) As IntPtr
	End Function

	' ── Shell32文件夹浏览 ──'
	<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
	Structure BROWSEINFO2
		Public hwndOwner As IntPtr
		Public pidlRoot As IntPtr
		Public pszDisplayName As String
		Public lpszTitle As String
		Public ulFlags As UInteger
		Public lpfn As IntPtr
		Public lParam As IntPtr
		Public iImage As Integer
	End Structure
	<DllImport("shell32.dll", CharSet:=CharSet.Auto)>
	Shared Function SHBrowseForFolder(ByRef lpbi As BROWSEINFO2) As IntPtr
	End Function
	<DllImport("shell32.dll", CharSet:=CharSet.Auto, EntryPoint:="SHGetPathFromIDList")>
	Shared Function SHGetPathFromIDList2(pidl As IntPtr, pszPath As System.Text.StringBuilder) As Boolean
	End Function
	<DllImport("ole32.dll", EntryPoint:="CoTaskMemFree")>
	Shared Sub CoTaskMemFree2(pv As IntPtr)
	End Sub

	Shared g_dpiScale As Single = 0.0!

	Shared Function GetDpiScale() As Single
		If g_dpiScale > 0.0! Then Return g_dpiScale
		Try
			Dim bmp As New System.Drawing.Bitmap(1, 1)
			Dim g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
			Dim dpi As Single = g.DpiX
			g.Dispose() : bmp.Dispose()
			If dpi < 72 Then dpi = 96
			g_dpiScale = dpi / 96.0!
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			g_dpiScale = 1.0!
		End Try
		Return g_dpiScale
	End Function

	Shared Function Scaled(logicalPixels As Integer) As Integer
		Return CInt(Math.Round(logicalPixels * GetDpiScale()))
	End Function

	Class BomDataRow
		Public FilePath As String
		Public FileName As String
		Public OldFileName As String
		Public DocType As String
		Public Qty As Integer
		Public DrawingNo As String
		Public PartNumber As String
		Public Category As String
		Public Material As String
		Public Volume As String
		Public Description As String
		Public Thumbnail As System.Drawing.Image
		Public Custom As New Dictionary(Of String, String)
		Public IsContentCenter As Boolean
		Public IsSheetMetal As Boolean
		Public IsLocalStandard As Boolean
		Public IsStdIcon As Boolean
		Public HasDrawing As String
		Public PartType As String
		Public BomStruct As Integer
		Public ParentFilePath As String
		Public IsWeldment As Boolean
		Public IsMissing As Boolean
		Public LevelNo As String
		Public FixedIdx As Integer = 0
		Public ChangedCols As New HashSet(Of String)
	End Class

	Class ColDef
		Public ColName As String
		Public Header As String
		Public IsReadOnly As Boolean
		Public IsFixed As Boolean
		Public IsCustom As Boolean
		Public IsImage As Boolean
		Public PropName As String
		Public InitWidth As Integer
		Public CenterAlign As Boolean
	End Class

	Dim g_rows As List(Of BomDataRow)
	Dim g_occMap As Dictionary(Of String, List(Of ComponentOccurrence)) = Nothing
	Dim g_occMapDirty As Boolean = True
	Dim g_asmDoc As AssemblyDocument
	Dim g_grid As DataGridView
	Dim g_status As Label
	Dim g_colDefs As List(Of ColDef)
	Dim g_chkPanel As Panel
	Dim g_chkFlow As FlowLayoutPanel
	Dim g_txtProp As System.Windows.Forms.ComboBox
	Dim g_customIdx As Integer = 0
	Dim g_txtRowH As System.Windows.Forms.TextBox
	Dim g_txtColW As System.Windows.Forms.TextBox
	Dim g_frm As Form
	Dim g_settingsFile As String = ""
	Dim g_rightClickRowIdx As Integer = -1
	Dim g_chkSkipCC As CheckBox
	Dim g_chkSkipVirtual As CheckBox
	Dim g_chkMergePno As CheckBox
	Dim g_typeIcons As New Dictionary(Of String, System.Drawing.Image)
	Dim g_clipboardList As New List(Of String)()
	Dim g_clipboardColNames As New List(Of String)()
	Dim g_pasteTargetCells As New List(Of DataGridViewCell)()
	Dim g_chkOnlyPart As CheckBox
	Dim g_chkOnlyAssy As CheckBox
	Dim g_chkOnlySheet As CheckBox
	Dim g_chkOnlyWeld As CheckBox
	Dim g_cmbTheme As System.Windows.Forms.ComboBox
	Dim g_cmbTools As System.Windows.Forms.ComboBox
	Dim g_btnExcelExport As System.Windows.Forms.Button
	Dim g_btnRefresh As System.Windows.Forms.Button

	Dim g_thumbPopup As Form = Nothing
	Dim g_thumbCancelFlag As Boolean = False
	Dim g_thumbThread As System.Threading.Thread = Nothing
	Dim g_thumbPopRow As Integer = -1

	' ★ 修改标志：用户编辑过单元格内容则为 True
	Dim g_isDirty As Boolean = False
	Dim g_pendingSaveDocs As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
	Dim g_sortedOrder As New List(Of String)()
	Dim g_isRefreshing As Boolean = False
	Dim g_isLoading As Boolean = False
	Dim g_hoverAssyFp As String = ""
	Dim g_lockedAssyFp As String = ""
	Dim g_dragColIdx As Integer = -1
	Dim g_selectedChkRow As Panel = Nothing
	' ★ 防止关闭时重入（写入后再次触发 FormClosing）
	Dim g_isClosing As Boolean = False
	Dim g_watchThread As System.Threading.Thread = Nothing  ' Excel编辑监听线程
	Dim g_watchStopFlag As Boolean = False                    ' 停止监听标志

	Const L_RIGHT_W As Integer = 290
	Const L_TOP_H As Integer = 38
	Const L_BOT_H As Integer = 82
	Const L_GAP As Integer = 2
	Const L_THUMB_W As Integer = 1600
	Const L_THUMB_H As Integer = 1600
	Const L_DEFAULT_ROW_H As Integer = 40
	Const L_PANEL_PAD As Integer = 20
	Const L_SCROLL_W As Integer = 20

	Dim RIGHT_W As Integer
	Dim TOP_H As Integer
	Dim BOT_H As Integer
	Dim GAP As Integer
	Dim THUMB_W As Integer
	Dim THUMB_H As Integer
	Dim DEFAULT_ROW_H As Integer
	Dim PANEL_PAD As Integer
	Dim SCROLL_W As Integer

	Sub InitScaledConstants()
		RIGHT_W = Scaled(L_RIGHT_W)
		TOP_H = Scaled(L_TOP_H)
		BOT_H = Scaled(L_BOT_H)
		GAP = Scaled(L_GAP)
		THUMB_W = Scaled(L_THUMB_W)
		THUMB_H = Scaled(L_THUMB_H)
		DEFAULT_ROW_H = Scaled(L_DEFAULT_ROW_H)
		PANEL_PAD = Scaled(L_PANEL_PAD)
		SCROLL_W = Scaled(L_SCROLL_W)
		' 自动检测可用字体，兼容台湾/香港/海外版本（无微软雅黑时自动切换）
		g_fontName = GetSafeFontName()
	End Sub

	' 自动检测系统可用中文字体，优先雅黑，台湾版用正黑體，最后用系统默认
	Function GetSafeFontName() As String
		Dim candidates() As String = {
		"Microsoft YaHei",
		"Microsoft JhengHei",
		"Microsoft JhengHei UI",
		"PMingLiU",
		"MingLiU",
		"Yu Gothic UI",
		"Meiryo UI"
		}
		Try
			Dim installedFonts As New System.Drawing.Text.InstalledFontCollection()
			Dim fontNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			For Each ff As System.Drawing.FontFamily In installedFonts.Families
				fontNames.Add(ff.Name)
			Next
			For Each candidate As String In candidates
				If fontNames.Contains(candidate) Then
					Return candidate
				End If
			Next
		Catch : End Try
		Return System.Drawing.SystemFonts.DefaultFont.Name
	End Function

	Const GUID_DTP As String = "{32853F0F-3444-11D1-9E93-0060B03C1CA6}"
	Const GUID_UDP As String = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"
	Const GUID_SUMMARY As String = "{F29F85E0-4FF9-1068-AB91-08002B27B3D9}"
	Const GUID_DOCSUMMARY As String = "{D5CDD502-2E9C-101B-9397-08002B2CF9AE}"
	Const MULTI_VAL As String = "*多种"
	Const FONT_NAME As String = "Microsoft YaHei"
	Dim g_fontName As String = "Microsoft YaHei"
	Dim g_theme As Integer = 0  ' 0=紫 1=蓝 2=绿 3=红 4=橙 5=粉 6=呼吸灯彩虹 7=暗黑 8=自定义
	' ── 自定义主题颜色（g_theme = 8 时生效）──
	Dim g_custTitleBar As System.Drawing.Color = System.Drawing.Color.FromArgb(108, 92, 231)   ' 上下标题（顶部栏/底部栏）
	Dim g_custColHeader As System.Drawing.Color = System.Drawing.Color.FromArgb(108, 92, 231)  ' 表格列头
	Dim g_custColMgr As System.Drawing.Color = System.Drawing.Color.FromArgb(243, 240, 255)    ' 右边列管理控件
	Dim g_custFuncDrop As System.Drawing.Color = System.Drawing.Color.FromArgb(228, 222, 252)  ' 功能下拉
	Dim g_custWinBg As System.Drawing.Color = System.Drawing.Color.FromArgb(245, 243, 255)     ' 窗口背景色
	Dim g_custExcelBtn As System.Drawing.Color = System.Drawing.Color.FromArgb(159, 149, 239)  ' Excel导出按钮
	Dim g_custRefresh As System.Drawing.Color = System.Drawing.Color.FromArgb(225, 112, 85)    ' 刷新按钮
	Dim g_custRowAlt As System.Drawing.Color = System.Drawing.Color.FromArgb(253, 252, 255)    ' 间隔行颜色（隔行条纹）
	Dim g_custRowWhite As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 255, 255)  ' 白底行颜色（间隔白底）
	Dim g_custCellText As System.Drawing.Color = System.Drawing.Color.FromArgb(0, 0, 0)        ' 表格文字颜色
	Dim g_pnlTop As System.Windows.Forms.Panel = Nothing
	Dim g_pnlBottom As System.Windows.Forms.Panel = Nothing
	Dim breathTimer As System.Windows.Forms.Timer = Nothing
	Dim breathHue As Single = 0.0F

	' ★ 模拟TextBox PlaceholderText（兼容旧版.NET）
	Function MakeStepHelpBtn(helpTitle As String, helpText As String, onTitleBar As Boolean) As System.Windows.Forms.Button
		Dim b As New System.Windows.Forms.Button()
		b.Text = "使用说明"
		b.Size = New System.Drawing.Size(Scaled(72), Scaled(24))
		b.FlatStyle = FlatStyle.Flat
		b.FlatAppearance.BorderSize = 0
		b.Font = MakeFont("Microsoft YaHei", 8.5!)
		b.Cursor = Cursors.Hand
		If onTitleBar Then
			b.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
			b.ForeColor = System.Drawing.Color.FromArgb(0, 90, 170)
		Else
			b.BackColor = OrangeMid()
			b.ForeColor = System.Drawing.Color.White
		End If
		AddHandler b.Click, Sub(s2, e2) ShowStepHelp(helpTitle, helpText)
		Return b
	End Function

	Sub ShowStepHelp(dlgTitle As String, helpText As String)
		Try
			Dim hd As New Form()
			hd.Text = dlgTitle
			hd.Size = New System.Drawing.Size(Scaled(480), Scaled(460))
			hd.StartPosition = FormStartPosition.CenterScreen
			hd.MinimizeBox = False
			hd.MaximizeBox = False
			hd.Font = MakeFont("Microsoft YaHei", 9.0!)
			hd.BackColor = C(252, 252, 252)
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then hd.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then hd.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch ' 忽略
			End Try
			Dim txt As New System.Windows.Forms.TextBox()
			txt.Multiline = True
			txt.ReadOnly = True
			txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			txt.WordWrap = True
			txt.Location = New System.Drawing.Point(Scaled(10), Scaled(10))
			txt.Size = New System.Drawing.Size(hd.ClientSize.Width - Scaled(20), hd.ClientSize.Height - Scaled(60))
			txt.Font = MakeFont("Microsoft YaHei", 9.0!)
			txt.BackColor = C(248, 248, 250)
			txt.Text = helpText
			hd.Controls.Add(txt)
			AddHandler hd.Shown, Sub(s2, e2) txt.DeselectAll()
			Dim btnOk As New Button()
			btnOk.Text = "我知道了"
			btnOk.Size = New System.Drawing.Size(Scaled(100), Scaled(28))
			btnOk.Location = New System.Drawing.Point((hd.ClientSize.Width - Scaled(100)) \ 2, hd.ClientSize.Height - Scaled(40))
			btnOk.FlatStyle = FlatStyle.Flat
			btnOk.BackColor = OrangeMid()
			btnOk.ForeColor = System.Drawing.Color.White
			AddHandler btnOk.Click, Sub(s2, e2) hd.Close()
			hd.Controls.Add(btnOk)
			hd.ShowDialog()
		Catch
		End Try
	End Sub

	Sub SetPlaceholder(tb As System.Windows.Forms.TextBox, hint As String)
		tb.ForeColor = System.Drawing.Color.Gray
		tb.Text = hint
		AddHandler tb.Enter, Sub(s, E)
								 Dim t As System.Windows.Forms.TextBox = CType(s, System.Windows.Forms.TextBox)
								 If t.Text = hint Then t.Text = "" : t.ForeColor = System.Drawing.Color.Black
							 End Sub
		AddHandler tb.Leave, Sub(s, E)
								 Dim t As System.Windows.Forms.TextBox = CType(s, System.Windows.Forms.TextBox)
								 If String.IsNullOrEmpty(t.Text.Trim()) Then t.ForeColor = System.Drawing.Color.Gray : t.Text = hint
							 End Sub
	End Sub

	' ── 自动写图号 WinForm 界面 ──
	Sub ShowAutoDrawingNoDialog()
		' 用于跨越ShowDialog边界传递控件值的变量（必须在函数最开头声明）
		Dim _savedPrefix As String = ""
		Dim _savedSep As String = "."
		Dim _savedOverwrite As String = "true"
		Dim _savedSkipCC As String = "true"
		Dim _savedSkipPropName As String = ""
		Dim _savedSkipPropVal As String = ""
		Dim _savedOnlyPropName As String = ""
		Dim _savedOnlyPropVal As String = ""
		Dim _savedWriteTarget As String = "custom"
		Try
			' 收集可操作行（去重，跳过丢失）
			Dim allRows As New List(Of BomDataRow)()
			Dim seenFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			For Each rr As BomDataRow In g_rows
				If rr.IsMissing Then Continue For
				If seenFp.Contains(rr.FilePath) Then Continue For
				seenFp.Add(rr.FilePath) : allRows.Add(rr)
			Next
			If allRows.Count = 0 Then
				MessageBox.Show("没有可操作的零部件行。", "写入图号", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Return
			End If
			' 按层级排序（数值比较，避免 1.10 < 1.2 的字符串排序问题）
			allRows.Sort(Function(a, b)
							 Dim la As String = If(String.IsNullOrEmpty(a.LevelNo), "0", a.LevelNo)
							 Dim lb As String = If(String.IsNullOrEmpty(b.LevelNo), "0", b.LevelNo)
							 Dim partsA() As String = la.Split(".".ToCharArray())
							 Dim partsB() As String = lb.Split(".".ToCharArray())
							 Dim maxLen As Integer = Math.Max(partsA.Length, partsB.Length)
							 For idx As Integer = 0 To maxLen - 1
								 Dim na As Integer = 0 : Dim nb As Integer = 0
								 If idx < partsA.Length Then Integer.TryParse(partsA(idx), na)
								 If idx < partsB.Length Then Integer.TryParse(partsB(idx), nb)
								 If na <> nb Then Return na.CompareTo(nb)
							 Next
							 Return 0
						 End Function)
			' ── 构建窗体 ──
			Dim frm As New Form()
			frm.Text = "写入图号"
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then frm.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then frm.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch : End Try
			frm.Width = Scaled(580)
			frm.Height = Scaled(470)
			frm.StartPosition = FormStartPosition.CenterScreen
			frm.FormBorderStyle = FormBorderStyle.FixedDialog
			frm.MaximizeBox = False : frm.MinimizeBox = False
			frm.Font = MakeFont(g_fontName, 9.5!)
			frm.BackColor = System.Drawing.Color.FromArgb(245, 248, 252)
			' 标题栏
			Dim pnlTitle As New Panel()
			pnlTitle.Dock = DockStyle.Top : pnlTitle.Height = Scaled(40)
			pnlTitle.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
			Dim lblT As New Label()
			lblT.Text = "  写入图号" : lblT.Dock = DockStyle.Fill
			lblT.ForeColor = System.Drawing.Color.White
			lblT.Font = MakeFontBold(g_fontName, 11.0!)
			lblT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnlTitle.Controls.Add(lblT)
			Dim helpDno As String = "【功能说明】" & vbCrLf &
		   "  按装配层级和前缀规则，自动给所有零件编号并写入图号属性。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 输入图号前缀（如项目代号，可留空）" & vbCrLf &
		   "  2. 选择分隔符（. 或 - 等）" & vbCrLf &
		   "  3. 选择写入目标：自定义属性 或 内置零件代号" & vbCrLf &
		   "  4. 勾选是否覆盖已有图号" & vbCrLf &
		   "  5. 可设置跳过/只处理某属性值的零件（高级筛选）" & vbCrLf &
		   "  6. 点「开始写入」，完成后弹窗提示数量" & vbCrLf & vbCrLf &
		   "【编号规则】" & vbCrLf &
		   "  · 按装配层级编号，如 1、1.1、1.1.1、2、2.1" & vbCrLf &
		   "  · 同一层级按装配树顺序递增" & vbCrLf &
		   "  · 标准件默认跳过"
			Dim btnDnoHelp As System.Windows.Forms.Button = MakeStepHelpBtn("写入图号 - 使用说明", helpDno, True)
			btnDnoHelp.Location = New System.Drawing.Point(pnlTitle.Width - Scaled(82), (pnlTitle.Height - Scaled(24)) \ 2)
			btnDnoHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
			pnlTitle.Controls.Add(btnDnoHelp)
			' 主体（先Add Fill，再Add Top，WinForms Dock布局正确）
			Dim pnl As New Panel()
			pnl.Dock = DockStyle.Fill
			pnl.Padding = New Padding(0, 0, 0, 0)
			pnl.BackColor = System.Drawing.Color.FromArgb(245, 248, 252)
			frm.Controls.Add(pnl)
			frm.Controls.Add(pnlTitle)
			Dim y As Integer = Scaled(12)
			Dim lm As Integer = Scaled(16)
			' 前缀标签（行内，紧贴输入框左侧）
			Dim lblPre As New Label()
			lblPre.Text = "前缀："
			lblPre.Location = New System.Drawing.Point(lm, y + Scaled(4))
			lblPre.Size = New System.Drawing.Size(Scaled(46), Scaled(22))
			lblPre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			lblPre.Font = MakeFontBold(g_fontName, 9.5!)
			pnl.Controls.Add(lblPre)
			' 前缀输入框（紧跟标签右侧）
			Dim txtPrefix As New System.Windows.Forms.TextBox()
			txtPrefix.Location = New System.Drawing.Point(lm + Scaled(50), y)
			txtPrefix.Size = New System.Drawing.Size(Scaled(360), Scaled(28))
			txtPrefix.Font = MakeFont(g_fontName, 10.5!)
			txtPrefix.BorderStyle = BorderStyle.FixedSingle
			txtPrefix.BackColor = System.Drawing.Color.White
			SetPlaceholder(txtPrefix, "请输入图号前缀，如：DWJ- 或 总装-")
			pnl.Controls.Add(txtPrefix) : y += Scaled(36)

			' ── 分隔符选择 ──
			Dim lblSep As New Label()
			lblSep.Text = "层级分隔符："
			lblSep.Location = New System.Drawing.Point(lm, y)
			lblSep.Size = New System.Drawing.Size(Scaled(90), Scaled(26))
			lblSep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnl.Controls.Add(lblSep)

			Dim rdoDot As New RadioButton()
			rdoDot.Text = ". 点（如：01.01）"
			rdoDot.Location = New System.Drawing.Point(lm + Scaled(94), y)
			rdoDot.Size = New System.Drawing.Size(Scaled(150), Scaled(26))
			rdoDot.Checked = True
			pnl.Controls.Add(rdoDot)

			Dim rdoDash As New RadioButton()
			rdoDash.Text = "- 横杠（如：01-01）"
			rdoDash.Location = New System.Drawing.Point(lm + Scaled(250), y)
			rdoDash.Size = New System.Drawing.Size(Scaled(180), Scaled(26))
			pnl.Controls.Add(rdoDash) : y += Scaled(32)

			' 规则说明
			Dim lblRule As New Label()
			lblRule.Text = "规则：总装→前缀.00  子部件→父图号.00  零件→父图号.01/.02..."
			lblRule.Location = New System.Drawing.Point(lm, y)
			lblRule.Size = New System.Drawing.Size(Scaled(520), Scaled(20))
			lblRule.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100)
			lblRule.Font = MakeFont(g_fontName, 8.5!)
			pnl.Controls.Add(lblRule) : y += Scaled(28)
			' 覆盖选项
			Dim chkOvr As New CheckBox()
			chkOvr.Text = "覆盖已有图号" : chkOvr.Checked = True
			chkOvr.Location = New System.Drawing.Point(lm, y)
			chkOvr.Size = New System.Drawing.Size(Scaled(160), Scaled(22))
			pnl.Controls.Add(chkOvr)
			Dim chkSkip As New CheckBox()
			chkSkip.Text = "跳过标准件" : chkSkip.Checked = True
			chkSkip.Location = New System.Drawing.Point(lm + Scaled(170), y)
			chkSkip.Size = New System.Drawing.Size(Scaled(140), Scaled(22))
			pnl.Controls.Add(chkSkip) : y += Scaled(32)

			' ── 跳过指定属性行 ──
			Dim lblSkipProp As New Label()
			lblSkipProp.Text = "跳过条件：属性"
			lblSkipProp.Location = New System.Drawing.Point(lm, y)
			lblSkipProp.Size = New System.Drawing.Size(Scaled(110), Scaled(24))
			lblSkipProp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnl.Controls.Add(lblSkipProp)
			Dim cmbSkipProp As New System.Windows.Forms.ComboBox()
			cmbSkipProp.Location = New System.Drawing.Point(lm + Scaled(114), y)
			cmbSkipProp.Size = New System.Drawing.Size(Scaled(160), Scaled(24))
			cmbSkipProp.Font = MakeFont(g_fontName, 9.0!)
			cmbSkipProp.DropDownStyle = ComboBoxStyle.DropDown
			cmbSkipProp.BackColor = System.Drawing.Color.White
			' ── 填充属性下拉：① 类别优先 ② 系统属性 ③ 自定义属性（动态收集）──
			' 系统属性名集合（用于同名检测）
			Dim sysSet As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			Dim sysPropList() As String = {
		"类别", "描述", "材料", "零件代号", "供应商",
		"设计者", "工程师", "审核人", "项目", "成本中心",
		"用户状态", "标准", "质量", "密度", "体积",
		"表面积", "库存编号", "制造商", "标准版本", "注释",
		"统计数量 [cQty]"}
			For Each sp As String In sysPropList
				sysSet.Add(sp)
			Next
			' 动态收集所有自定义属性（遍历装配所有零件）
			Dim customPropSet As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			Try
				Dim stkP As New Stack(Of ComponentOccurrence)()
				Dim topListP As New System.Collections.Generic.List(Of ComponentOccurrence)()
				For Each occP As ComponentOccurrence In g_asmDoc.ComponentDefinition.Occurrences
					topListP.Add(occP)
				Next
				For _ip As Integer = topListP.Count - 1 To 0 Step -1
					stkP.Push(topListP(_ip))
				Next
				Dim seenP As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
				Do While stkP.Count > 0
					Dim occP As ComponentOccurrence = stkP.Pop()
					If occP.Suppressed Then Continue Do
					Try
						Dim ddP As Document = occP.Definition.Document
						Dim fpP As String = ddP.FullDocumentName
						If Not seenP.Contains(fpP) Then
							seenP.Add(fpP)
							Try
								Dim udpP As PropertySet = ddP.PropertySets.Item(GUID_UDP)
								For Each pP As Inventor.Property In udpP
									customPropSet.Add(pP.Name)
								Next
							Catch : End Try
							If occP.DefinitionDocumentType = kAssemblyDocumentObject Then
								For Each childP As ComponentOccurrence In occP.SubOccurrences
									Try
										If String.Compare(childP.Definition.Document.FullDocumentName, fpP, True) <> 0 Then stkP.Push(childP)
									Catch : End Try
								Next
							End If
						End If
					Catch : End Try
				Loop
			Catch : End Try
			' 填充下拉：空 → ① 类别 → ② 其他系统属性 → ③ 自定义属性
			cmbSkipProp.Items.Add("")
			cmbSkipProp.Items.Add("类别")
			Dim otherSys() As String = {"描述", "材料", "零件代号", "供应商", "设计者", "工程师", "审核人", "项目", "成本中心", "用户状态", "标准", "质量(kg)", "密度", "体积(cm³)", "表面积(cm²)", "库存编号", "制造商", "标准版本", "注释"}
			For Each sp2 As String In otherSys
				cmbSkipProp.Items.Add(sp2)
			Next
			For Each cpn As String In customPropSet
				If sysSet.Contains(cpn) Then
					cmbSkipProp.Items.Add(cpn & "（自定义）")
				Else
					cmbSkipProp.Items.Add(cpn)
				End If
			Next
			cmbSkipProp.SelectedIndex = 0
			pnl.Controls.Add(cmbSkipProp)
			Dim lblSkipEq As New Label()
			lblSkipEq.Text = "值："
			lblSkipEq.Location = New System.Drawing.Point(lm + Scaled(280), y + Scaled(4))
			lblSkipEq.Size = New System.Drawing.Size(Scaled(30), Scaled(20))
			lblSkipEq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnl.Controls.Add(lblSkipEq)
			Dim txtSkipVal As New System.Windows.Forms.TextBox()
			txtSkipVal.Location = New System.Drawing.Point(lm + Scaled(312), y)
			txtSkipVal.Size = New System.Drawing.Size(Scaled(140), Scaled(24))
			txtSkipVal.Font = MakeFont(g_fontName, 9.0!)
			txtSkipVal.BorderStyle = BorderStyle.FixedSingle
			txtSkipVal.BackColor = System.Drawing.Color.White
			SetPlaceholder(txtSkipVal, "点击▼选择或手动输入")
			pnl.Controls.Add(txtSkipVal)
			' 下拉按钮
			Dim btnSkipDrop As New Button()
			btnSkipDrop.Text = "▼"
			btnSkipDrop.Location = New System.Drawing.Point(lm + Scaled(454), y)
			btnSkipDrop.Size = New System.Drawing.Size(Scaled(24), Scaled(24))
			btnSkipDrop.FlatStyle = FlatStyle.Flat
			btnSkipDrop.FlatAppearance.BorderSize = 1
			btnSkipDrop.Font = MakeFont(g_fontName, 7.0!)
			btnSkipDrop.BackColor = System.Drawing.Color.FromArgb(230, 230, 230)
			pnl.Controls.Add(btnSkipDrop)
			' 下拉勾选逻辑（ToolStripDropDown）
			Dim skipDrop As New ToolStripDropDown()
			skipDrop.AutoClose = False
			Dim skipClbHost As New ToolStripControlHost(New CheckedListBox())
			Dim skipClb As CheckedListBox = CType(skipClbHost.Control, CheckedListBox)
			skipClb.CheckOnClick = True
			skipClb.Font = MakeFont(g_fontName, 9.0!)
			skipClb.Width = Scaled(180) : skipClb.Height = Scaled(160)
			skipClbHost.AutoSize = False
			skipClbHost.Size = New System.Drawing.Size(Scaled(180), Scaled(160))
			skipDrop.Items.Add(skipClbHost)
			' 确定按钮行
			Dim skipBtnHost As New ToolStripControlHost(New Button())
			Dim skipBtnOK As Button = CType(skipBtnHost.Control, Button)
			skipBtnOK.Text = "确定" : skipBtnOK.Width = Scaled(180) : skipBtnOK.Height = Scaled(26)
			skipBtnOK.FlatStyle = FlatStyle.Flat
			skipBtnOK.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
			skipBtnOK.ForeColor = System.Drawing.Color.White
			skipBtnHost.AutoSize = False
			skipBtnHost.Size = New System.Drawing.Size(Scaled(180), Scaled(26))
			skipDrop.Items.Add(skipBtnHost)
			' 点击确定：拼接选中值到输入框
			AddHandler skipBtnOK.Click, Sub(sb, se)
											Dim sel As New List(Of String)()
											For Each ci As Object In skipClb.CheckedItems
												sel.Add(CStr(ci))
											Next
											txtSkipVal.Text = String.Join(",", sel.ToArray())
											skipDrop.Close()
										End Sub
			' 点击▼按钮：填充值列表并展开下拉
			AddHandler btnSkipDrop.Click, Sub(sd, se)
											  If skipDrop.Visible Then
												  skipDrop.Close() : Return
											  End If
											  Dim pn As String = cmbSkipProp.Text.Replace("（自定义）", "").Trim()
											  skipClb.Items.Clear()
											  If Not String.IsNullOrEmpty(pn) Then
												  Dim vals As List(Of String) = CollectPropValues(allRows, pn)
												  Dim curVals() As String = txtSkipVal.Text.Split(",".ToCharArray())
												  For Each v As String In vals
													  Dim chk As Boolean = Array.Exists(curVals, Function(x) x.Trim().ToLower() = v.ToLower())
													  skipClb.Items.Add(v, chk)
												  Next
											  End If
											  skipDrop.Show(btnSkipDrop, New System.Drawing.Point(0, btnSkipDrop.Height))
										  End Sub
			' 点击输入框也触发下拉
			AddHandler txtSkipVal.Click, Sub(st, se)
											 btnSkipDrop.PerformClick()
										 End Sub
			' 属性切换时清空值' & vbCrLf &
			AddHandler cmbSkipProp.SelectedIndexChanged, Sub(sc, ec)
															 If cmbSkipProp.SelectedIndex >= 0 Then txtSkipVal.Text = ""
														 End Sub
			pnl.Controls.Add(txtSkipVal) : y += Scaled(32)
			Dim lblSkipHint As New Label()
			lblSkipHint.Text = "跳过：属性值包含指定值则跳过（写入条件已满足时才判断）"
			lblSkipHint.Location = New System.Drawing.Point(lm, y)
			lblSkipHint.Size = New System.Drawing.Size(Scaled(520), Scaled(18))
			lblSkipHint.ForeColor = System.Drawing.Color.FromArgb(140, 140, 140)
			lblSkipHint.Font = MakeFont(g_fontName, 8.0!)
			pnl.Controls.Add(lblSkipHint) : y += Scaled(24)

			' ── 根据属性值写入（只写入指定属性值的零件）──
			Dim lblOnlyProp As New Label()
			lblOnlyProp.Text = "写入条件：属性"
			lblOnlyProp.Location = New System.Drawing.Point(lm, y)
			lblOnlyProp.Size = New System.Drawing.Size(Scaled(110), Scaled(24))
			lblOnlyProp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnl.Controls.Add(lblOnlyProp)

			Dim cmbOnlyProp As New System.Windows.Forms.ComboBox()
			cmbOnlyProp.Location = New System.Drawing.Point(lm + Scaled(114), y)
			cmbOnlyProp.Size = New System.Drawing.Size(Scaled(160), Scaled(24))
			cmbOnlyProp.Font = MakeFont(g_fontName, 9.0!)
			cmbOnlyProp.DropDownStyle = ComboBoxStyle.DropDown
			cmbOnlyProp.BackColor = System.Drawing.Color.White
			' 复用 cmbSkipProp 相同的属性列表
			For Each itm As Object In cmbSkipProp.Items
				cmbOnlyProp.Items.Add(itm)
			Next
			cmbOnlyProp.SelectedIndex = 0
			pnl.Controls.Add(cmbOnlyProp)

			Dim lblOnlyEq As New Label()
			lblOnlyEq.Text = "值："
			lblOnlyEq.Location = New System.Drawing.Point(lm + Scaled(280), y + Scaled(4))
			lblOnlyEq.Size = New System.Drawing.Size(Scaled(30), Scaled(20))
			lblOnlyEq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnl.Controls.Add(lblOnlyEq)
			Dim txtOnlyVal As New System.Windows.Forms.TextBox()
			txtOnlyVal.Location = New System.Drawing.Point(lm + Scaled(312), y)
			txtOnlyVal.Size = New System.Drawing.Size(Scaled(140), Scaled(24))
			txtOnlyVal.Font = MakeFont(g_fontName, 9.0!)
			txtOnlyVal.BorderStyle = BorderStyle.FixedSingle
			txtOnlyVal.BackColor = System.Drawing.Color.White
			SetPlaceholder(txtOnlyVal, "点击▼选择或手动输入")
			pnl.Controls.Add(txtOnlyVal)
			Dim btnOnlyDrop As New Button()
			btnOnlyDrop.Text = "▼"
			btnOnlyDrop.Location = New System.Drawing.Point(lm + Scaled(454), y)
			btnOnlyDrop.Size = New System.Drawing.Size(Scaled(24), Scaled(24))
			btnOnlyDrop.FlatStyle = FlatStyle.Flat
			btnOnlyDrop.FlatAppearance.BorderSize = 1
			btnOnlyDrop.Font = MakeFont(g_fontName, 7.0!)
			btnOnlyDrop.BackColor = System.Drawing.Color.FromArgb(230, 230, 230)
			pnl.Controls.Add(btnOnlyDrop)
			Dim onlyDrop As New ToolStripDropDown()
			onlyDrop.AutoClose = False
			Dim onlyClbHost As New ToolStripControlHost(New CheckedListBox())
			Dim onlyClb As CheckedListBox = CType(onlyClbHost.Control, CheckedListBox)
			onlyClb.CheckOnClick = True
			onlyClb.Font = MakeFont(g_fontName, 9.0!)
			onlyClb.Width = Scaled(180) : onlyClb.Height = Scaled(160)
			onlyClbHost.AutoSize = False
			onlyClbHost.Size = New System.Drawing.Size(Scaled(180), Scaled(160))
			onlyDrop.Items.Add(onlyClbHost)
			Dim onlyBtnHost As New ToolStripControlHost(New Button())
			Dim onlyBtnOK As Button = CType(onlyBtnHost.Control, Button)
			onlyBtnOK.Text = "确定" : onlyBtnOK.Width = Scaled(180) : onlyBtnOK.Height = Scaled(26)
			onlyBtnOK.FlatStyle = FlatStyle.Flat
			onlyBtnOK.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
			onlyBtnOK.ForeColor = System.Drawing.Color.White
			onlyBtnHost.AutoSize = False
			onlyBtnHost.Size = New System.Drawing.Size(Scaled(180), Scaled(26))
			onlyDrop.Items.Add(onlyBtnHost)
			AddHandler onlyBtnOK.Click, Sub(ob, oe)
											Dim sel2 As New List(Of String)()
											For Each ci2 As Object In onlyClb.CheckedItems
												sel2.Add(CStr(ci2))
											Next
											txtOnlyVal.Text = String.Join(",", sel2.ToArray())
											onlyDrop.Close()
										End Sub
			AddHandler btnOnlyDrop.Click, Sub(od2, oe2)
											  If onlyDrop.Visible Then
												  onlyDrop.Close() : Return
											  End If
											  Dim pn2 As String = cmbOnlyProp.Text.Replace("（自定义）", "").Trim()
											  onlyClb.Items.Clear()
											  If Not String.IsNullOrEmpty(pn2) Then
												  Dim vals2 As List(Of String) = CollectPropValues(allRows, pn2)
												  Dim curVals2() As String = txtOnlyVal.Text.Split(",".ToCharArray())
												  For Each v2 As String In vals2
													  Dim chk2 As Boolean = Array.Exists(curVals2, Function(x2) x2.Trim().ToLower() = v2.ToLower())
													  onlyClb.Items.Add(v2, chk2)
												  Next
											  End If
											  onlyDrop.Show(btnOnlyDrop, New System.Drawing.Point(0, btnOnlyDrop.Height))
										  End Sub
			AddHandler txtOnlyVal.Click, Sub(ot2, oe3)
											 btnOnlyDrop.PerformClick()
										 End Sub
			AddHandler cmbOnlyProp.SelectedIndexChanged, Sub(sc2, ec2)
															 If cmbOnlyProp.SelectedIndex >= 0 Then txtOnlyVal.Text = ""
														 End Sub
			pnl.Controls.Add(txtOnlyVal) : y += Scaled(32)

			Dim lblOnlyHint As New Label()
			lblOnlyHint.Text = "写入：只写入属性值包含指定值的零件（优先级高于跳过条件）"
			lblOnlyHint.Location = New System.Drawing.Point(lm, y)
			lblOnlyHint.Size = New System.Drawing.Size(Scaled(520), Scaled(18))
			lblOnlyHint.ForeColor = System.Drawing.Color.FromArgb(140, 140, 140)
			lblOnlyHint.Font = MakeFont(g_fontName, 8.0!)
			pnl.Controls.Add(lblOnlyHint) : y += Scaled(24)
			' ── 写入目标选项 ──
			Dim lblTarget As New Label()
			lblTarget.Text = "写入到："
			lblTarget.Location = New System.Drawing.Point(lm, y + Scaled(4))
			lblTarget.Size = New System.Drawing.Size(Scaled(60), Scaled(22))
			lblTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnl.Controls.Add(lblTarget)
			Dim rdoCustomProp As New RadioButton()
			rdoCustomProp.Text = "自定义属性【图号】（默认）"
			rdoCustomProp.Location = New System.Drawing.Point(lm + Scaled(64), y)
			rdoCustomProp.Size = New System.Drawing.Size(Scaled(210), Scaled(26))
			rdoCustomProp.Checked = True
			rdoCustomProp.Font = MakeFont(g_fontName, 9.0!)
			pnl.Controls.Add(rdoCustomProp)
			Dim rdoPartNo As New RadioButton()
			rdoPartNo.Text = "零件代号"
			rdoPartNo.Location = New System.Drawing.Point(lm + Scaled(280), y)
			rdoPartNo.Size = New System.Drawing.Size(Scaled(120), Scaled(26))
			rdoPartNo.Font = MakeFont(g_fontName, 9.0!)
			pnl.Controls.Add(rdoPartNo) : y += Scaled(32)

			' 写入按钮
			Dim btnOK As New Button()
			btnOK.Text = "写入图号" : btnOK.Size = New System.Drawing.Size(Scaled(120), Scaled(32))
			btnOK.Location = New System.Drawing.Point(lm + Scaled(160), y)
			btnOK.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
			btnOK.ForeColor = System.Drawing.Color.White
			btnOK.FlatStyle = FlatStyle.Flat
			btnOK.FlatAppearance.BorderSize = 0
			btnOK.Font = MakeFontBold(g_fontName, 9.5!)
			pnl.Controls.Add(btnOK)
			Dim btnCancel As New Button()
			btnCancel.Text = "取消" : btnCancel.Size = New System.Drawing.Size(Scaled(80), Scaled(32))
			btnCancel.Location = New System.Drawing.Point(lm + Scaled(300), y)
			btnCancel.BackColor = C(200, 50, 50)
			btnCancel.ForeColor = System.Drawing.Color.White
			btnCancel.FlatStyle = FlatStyle.Flat
			btnCancel.FlatAppearance.BorderSize = 0
			pnl.Controls.Add(btnCancel)
			' ── 图号生成逻辑 ──
			Dim confirmed As Boolean = False
			AddHandler btnOK.Click, Sub(s, ev)
										If String.IsNullOrEmpty(txtPrefix.Text.Trim()) Then
											MessageBox.Show("请先输入图号前缀，如：DWJ- 或 总装-！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
											Return
										End If
										' 窗口关闭前先把控件值存到外部变量（关闭后控件可能被Dispose）
										_savedPrefix = txtPrefix.Text.Trim()
										_savedSep = If(rdoDash.Checked, "-", ".")
										_savedOverwrite = chkOvr.Checked.ToString().ToLower()
										_savedSkipCC = chkSkip.Checked.ToString().ToLower()
										_savedSkipPropName = cmbSkipProp.Text.Trim()
										_savedSkipPropVal = txtSkipVal.Text.Trim()
										_savedOnlyPropName = cmbOnlyProp.Text.Trim()
										_savedOnlyPropVal = txtOnlyVal.Text.Trim()
										_savedWriteTarget = If(rdoPartNo.Checked, "partno", "custom")
										confirmed = True
										frm.Close()
									End Sub
			AddHandler btnCancel.Click, Sub(s, ev)
											_savedPrefix = txtPrefix.Text.Trim()
											_savedSep = If(rdoDash.Checked, "-", ".")
											_savedOverwrite = chkOvr.Checked.ToString().ToLower()
											_savedSkipCC = chkSkip.Checked.ToString().ToLower()
											_savedSkipPropName = cmbSkipProp.Text.Trim()
											_savedSkipPropVal = txtSkipVal.Text.Trim()
											_savedOnlyPropName = cmbOnlyProp.Text.Trim()
											_savedOnlyPropVal = txtOnlyVal.Text.Trim()
											_savedWriteTarget = If(rdoPartNo.Checked, "partno", "custom")
											frm.Close()
										End Sub
			AddHandler txtPrefix.KeyDown, Sub(s, ev)
											  If ev.KeyCode = Keys.Enter Then btnOK.PerformClick()
										  End Sub
			' ── 从ini读取上次图号设置 ──
			Try
				Dim _dnoIniPath As String = GetSettingsPath()
				Dim dnoSec As Dictionary(Of String, String) = ReadSection(_dnoIniPath, "DrawingNo")
				If dnoSec.Count > 0 Then
					' 前缀：始终恢复
					If dnoSec.ContainsKey("Prefix") Then txtPrefix.Text = dnoSec("Prefix")
					' 分隔符
					If dnoSec.ContainsKey("Separator") Then
						rdoDash.Checked = (dnoSec("Separator") = "-")
						rdoDot.Checked = (dnoSec("Separator") <> "-")
					End If
					' 覆盖/跳过标准件
					If dnoSec.ContainsKey("Overwrite") Then chkOvr.Checked = (dnoSec("Overwrite").ToLower() = "true")
					If dnoSec.ContainsKey("SkipCC") Then chkSkip.Checked = (dnoSec("SkipCC").ToLower() = "true")
					' 跳过条件：先暂时解绑SelectedIndexChanged，避免清空值
					Dim savedSkipProp As String = If(dnoSec.ContainsKey("SkipPropName"), dnoSec("SkipPropName"), "")
					Dim savedSkipVal As String = If(dnoSec.ContainsKey("SkipPropVal"), dnoSec("SkipPropVal"), "")
					If Not String.IsNullOrEmpty(savedSkipProp) Then
						cmbSkipProp.Text = savedSkipProp
					End If
					txtSkipVal.Text = savedSkipVal
					' 写入条件：同样处理
					Dim savedOnlyProp As String = If(dnoSec.ContainsKey("OnlyPropName"), dnoSec("OnlyPropName"), "")
					Dim savedOnlyVal As String = If(dnoSec.ContainsKey("OnlyPropVal"), dnoSec("OnlyPropVal"), "")
					If Not String.IsNullOrEmpty(savedOnlyProp) Then
						cmbOnlyProp.Text = savedOnlyProp
					End If
					txtOnlyVal.Text = savedOnlyVal
				End If
			Catch : End Try



			frm.ShowDialog()

			' ── ShowDialog返回后立即保存图号设置到ini（控件仍在内存）──
			Try
				Dim _dnoSavePath As String = GetSettingsPath()
				Dim iniDir2 As String = System.IO.Path.GetDirectoryName(_dnoSavePath)
				If Not String.IsNullOrEmpty(iniDir2) AndAlso Not System.IO.Directory.Exists(iniDir2) Then
					System.IO.Directory.CreateDirectory(iniDir2)
				End If
				Dim iniLines2 As New List(Of String)()
				If System.IO.File.Exists(_dnoSavePath) Then
					Dim allLines2() As String = System.IO.File.ReadAllLines(_dnoSavePath, System.Text.Encoding.UTF8)
					Dim inDnoSec2 As Boolean = False
					For Each ln2 As String In allLines2
						If ln2.Trim().ToLower() = "[drawingno]" Then inDnoSec2 = True : Continue For
						If ln2.Trim().StartsWith("[") AndAlso ln2.Trim().ToLower() <> "[drawingno]" Then inDnoSec2 = False
						If Not inDnoSec2 Then iniLines2.Add(ln2)
					Next
				End If
				iniLines2.Add("")
				iniLines2.Add("[DrawingNo]")
				iniLines2.Add("Prefix=" & _savedPrefix)
				iniLines2.Add("Separator=" & _savedSep)
				iniLines2.Add("Overwrite=" & _savedOverwrite)
				iniLines2.Add("SkipCC=" & _savedSkipCC)
				iniLines2.Add("SkipPropName=" & _savedSkipPropName)
				iniLines2.Add("SkipPropVal=" & _savedSkipPropVal)
				iniLines2.Add("OnlyPropName=" & _savedOnlyPropName)
				iniLines2.Add("OnlyPropVal=" & _savedOnlyPropVal)
				Dim iniContent2 As String = String.Join(vbCrLf, iniLines2.ToArray())
				System.IO.File.WriteAllText(_dnoSavePath, iniContent2, System.Text.Encoding.UTF8)
			Catch ex3 As Exception

			End Try
			If Not confirmed Then
				SetStatus("  已取消写图号", C(110, 55, 0)) : Return
			End If
			' ── 读取跳过属性条件 ──
			' 读取分隔符
			Dim sep As String = If(rdoDot.Checked, ".", "-")
			Dim skipPropName As String = cmbSkipProp.Text.Trim()
			Dim skipPropVal As String = txtSkipVal.Text.Trim()
			Dim useSkipProp As Boolean = Not String.IsNullOrEmpty(skipPropName) AndAlso Not String.IsNullOrEmpty(skipPropVal)
			' 仅写入条件
			Dim onlyPropName As String = cmbOnlyProp.Text.Trim()
			Dim onlyPropVal As String = txtOnlyVal.Text.Trim()
			Dim useOnlyProp As Boolean = Not String.IsNullOrEmpty(onlyPropName) AndAlso Not String.IsNullOrEmpty(onlyPropVal)

			' ── 读取覆盖和标准件选项 ──
			Dim skipCC As Boolean = chkSkip.Checked
			Dim overwrite As Boolean = chkOvr.Checked
			Dim writeToPartNo As Boolean = (_savedWriteTarget = "partno")

			' ── 冲突检测：跳过条件和写入条件有相同值则不执行 ──
			If useSkipProp AndAlso useOnlyProp Then
				' 同一属性且有交集值
				Dim skipVals() As String = skipPropVal.Split(",".ToCharArray())
				Dim onlyVals() As String = onlyPropVal.Split(",".ToCharArray())
				Dim conflicts As New List(Of String)()
				For Each sv As String In skipVals
					Dim svt As String = sv.Trim().ToLower()
					If String.IsNullOrEmpty(svt) Then Continue For
					For Each ov As String In onlyVals
						Dim ovt As String = ov.Trim().ToLower()
						If String.IsNullOrEmpty(ovt) Then Continue For
						' 任意一方包含另一方则视为冲突
						If svt.Contains(ovt) OrElse ovt.Contains(svt) Then
							conflicts.Add(sv.Trim() & " / " & ov.Trim())
						End If
					Next
				Next
				If conflicts.Count > 0 Then
					Dim conflictMsg As String =
			"跳过条件 和 写入条件 存在相同或包含的值，无法执行写入！" & vbCrLf &
			vbCrLf & "冲突值：" & vbCrLf &
			String.Join(vbCrLf, conflicts.ToArray()) & vbCrLf &
			vbCrLf & "请修改后重试。"
					MessageBox.Show(conflictMsg, "写入图号 — 条件冲突", MessageBoxButtons.OK, MessageBoxIcon.Warning)
					SetStatus("  条件冲突，已取消写入", C(200, 60, 0))
					Return
				End If
			End If
			' 读取前缀（供确认弹窗和图号生成使用）
			Dim prefix As String = txtPrefix.Text.Trim()

			' ── 写入前确认弹窗 ──
			Dim _targetDesc As String = If(writeToPartNo, "零件代号（Part Number）", "自定义属性【图号】")
			Dim confirmMsg As String =
	"即将写入图号，此操作不可撤销！" & vbCrLf &
	vbCrLf & "写入目标：" & _targetDesc & vbCrLf &
	"前缀：" & prefix & vbCrLf &
	"共 " & allRows.Count & " 个零部件" & vbCrLf &
	vbCrLf & "是否继续写入？"
			Dim confirmResult As DialogResult = MessageBox.Show(
	confirmMsg, "写入图号 — 确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If confirmResult <> DialogResult.Yes Then
				SetStatus("  已取消写入", C(110, 55, 0)) : Return
			End If

			' ── 第1步：先清空所有行图号（跳过标准件/只读件）──
			For Each rr As BomDataRow In allRows
				' ★ 标准件/只读件：勾选跳过标准件时跳过清空
				If skipCC AndAlso (rr.IsContentCenter OrElse rr.IsStdIcon OrElse rr.IsLocalStandard) Then Continue For
				' ★ 未勾选覆盖：已有图号则跳过清空
				If Not overwrite AndAlso Not String.IsNullOrEmpty(rr.DrawingNo) Then Continue For
				' ★ 条件判断：写入条件优先级高于跳过条件
				' 有写入条件且满足 → 直接通过（不再判断跳过条件）
				' 有写入条件但不满足 → 跳过；无写入条件 → 判断跳过条件
				If useOnlyProp Then
					Dim _ov As String = GetRowPropVal(rr, onlyPropName).ToLower()
					Dim _onlyMatch As Boolean = False
					For Each _ov2 As String In onlyPropVal.Split(",".ToCharArray())
						If Not String.IsNullOrEmpty(_ov2.Trim()) AndAlso _ov.Contains(_ov2.Trim().ToLower()) Then _onlyMatch = True : Exit For
					Next
					If Not _onlyMatch Then Continue For
					' 满足写入条件，直接通过，不判断跳过条件
				Else
					If useSkipProp Then
						Dim _sv As String = GetRowPropVal(rr, skipPropName).ToLower()
						Dim _skipMatch As Boolean = False
						For Each _sv2 As String In skipPropVal.Split(",".ToCharArray())
							If Not String.IsNullOrEmpty(_sv2.Trim()) AndAlso _sv.Contains(_sv2.Trim().ToLower()) Then _skipMatch = True : Exit For
						Next
						If _skipMatch Then Continue For
					End If
				End If
				rr.DrawingNo = ""
				Try
					Dim tDocC As Document = Nothing
					For Each od As Document In ThisApplication.Documents
						If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then tDocC = od : Exit For
					Next
					If tDocC Is Nothing AndAlso System.IO.File.Exists(rr.FilePath) Then
						tDocC = ThisApplication.Documents.Open(rr.FilePath, False)
					End If
					If tDocC IsNot Nothing Then
						Dim udpC As PropertySet = GetUDP(tDocC)
						If udpC IsNot Nothing Then
							' 检查属性是否存在，存在则清空，不存在则创建空属性
							Dim propExists As Boolean = False
							For Each p As Inventor.Property In udpC
								If String.Compare(p.Name, "图号", True) = 0 Then
									p.Value = "" : propExists = True : Exit For
								End If
							Next
							If Not propExists Then udpC.Add("", "图号")
						End If
					End If
				Catch : End Try
			Next

			' ── 生成图号映射（按装配树递归，每个文件只生成一次图号）──
			' prefix 已在前面读取
			Dim dnoMap As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
			Dim partCounterMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
			Dim topCounter As Integer = 0

			' 按装配树DFS递归生成图号，parentDno="表示顶层
			Dim AssignDno As Action(Of String, String) = Nothing
			AssignDno = Sub(parentFp As String, parentDno As String)
							' 找出所有直接子节点（ParentFilePath = parentFp），按g_rows顺序
							For Each rr As BomDataRow In allRows
								If String.Compare(rr.ParentFilePath, parentFp, True) <> 0 Then Continue For
								If dnoMap.ContainsKey(rr.FilePath) Then Continue For
								If skipCC AndAlso (rr.IsContentCenter OrElse rr.IsStdIcon OrElse rr.IsLocalStandard) Then Continue For
								' ★ 条件判断：只决定是否写入图号，不阻止递归子节点
								Dim _shouldWrite As Boolean = True
								If useOnlyProp Then
									' 有写入条件：满足才写入（优先级高，满足则不判断跳过）
									Dim _ov As String = GetRowPropVal(rr, onlyPropName).ToLower()
									Dim _onlyMatch As Boolean = False
									For Each _ov2 As String In onlyPropVal.Split(",".ToCharArray())
										If Not String.IsNullOrEmpty(_ov2.Trim()) AndAlso _ov.Contains(_ov2.Trim().ToLower()) Then _onlyMatch = True : Exit For
									Next
									If Not _onlyMatch Then _shouldWrite = False
								ElseIf useSkipProp Then
									' 无写入条件，判断跳过条件
									Dim _sv As String = GetRowPropVal(rr, skipPropName).ToLower()
									Dim _skipMatch As Boolean = False
									For Each _sv2 As String In skipPropVal.Split(",".ToCharArray())
										If Not String.IsNullOrEmpty(_sv2.Trim()) AndAlso _sv.Contains(_sv2.Trim().ToLower()) Then _skipMatch = True : Exit For
									Next
									If _skipMatch Then _shouldWrite = False
								End If
								Dim newDno As String = ""
								If String.IsNullOrEmpty(parentDno) Then
									' 顶层
									topCounter += 1
									If rr.DocType = "部件" OrElse rr.DocType = "焊接" Then
										newDno = prefix & topCounter.ToString("00") & sep & "00"
									Else
										Dim pk As String = "top|" & topCounter
										If Not partCounterMap.ContainsKey(pk) Then partCounterMap(pk) = 0
										partCounterMap(pk) += 1
										newDno = prefix & topCounter.ToString("00") & sep & partCounterMap(pk).ToString("00")
									End If
								Else
									' 子节点：去掉父图号末尾的.00再拼接
									Dim baseDno As String = parentDno
									If baseDno.EndsWith(".00") Then baseDno = baseDno.Substring(0, baseDno.Length - 3)
									If baseDno.EndsWith("-00") Then baseDno = baseDno.Substring(0, baseDno.Length - 3)
									' 子部件和子零件都用计数器，避免和父部件重复
									If Not partCounterMap.ContainsKey(parentFp) Then partCounterMap(parentFp) = 0
									partCounterMap(parentFp) += 1
									If rr.DocType = "部件" OrElse rr.DocType = "焊接" Then
										newDno = baseDno & sep & partCounterMap(parentFp).ToString("00") & sep & "00"
									Else
										newDno = baseDno & sep & partCounterMap(parentFp).ToString("00")
									End If
								End If
								' 满足写入条件才加入dnoMap，但部件始终递归子节点
								If _shouldWrite Then
									dnoMap(rr.FilePath) = newDno
								End If
								' 部件无论是否写入，都递归其子节点（子节点有自己的条件判断）
								If rr.DocType = "部件" OrElse rr.DocType = "焊接" Then
									AssignDno(rr.FilePath, If(_shouldWrite, newDno, parentDno))
								End If
							Next
						End Sub
			' 从顶层开始（ParentFilePath="）
			AssignDno("", "")

			' ── 写入 ──
			Dim successCount As Integer = 0 : Dim skipCount As Integer = 0 : Dim failCount As Integer = 0
			For Each rr As BomDataRow In allRows
				If Not dnoMap.ContainsKey(rr.FilePath) Then Continue For
				If skipCC AndAlso (rr.IsContentCenter OrElse rr.IsStdIcon OrElse rr.IsLocalStandard) Then Continue For
				Dim newDno As String = dnoMap(rr.FilePath)
				If Not overwrite AndAlso Not String.IsNullOrEmpty(rr.DrawingNo) Then skipCount += 1 : Continue For
				Try
					rr.DrawingNo = newDno
					Dim tDoc As Document = Nothing
					For Each od As Document In ThisApplication.Documents
						If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then tDoc = od : Exit For
					Next
					If tDoc Is Nothing AndAlso System.IO.File.Exists(rr.FilePath) Then
						tDoc = ThisApplication.Documents.Open(rr.FilePath, False)
					End If
					If tDoc IsNot Nothing Then
						If writeToPartNo Then
							' 写入零件代号
							Try
								Dim dtp As PropertySet = tDoc.PropertySets.Item(GUID_DTP)
								dtp.Item("Part Number").Value = newDno
								rr.PartNumber = newDno
							Catch : End Try
						Else
							' 写入自定义属性【图号】（不存在则自动创建）
							Dim udp As PropertySet = GetUDP(tDoc)
							If udp IsNot Nothing Then SafeWriteUDP(udp, "图号", newDno)
						End If
					End If
					successCount += 1
				Catch ex As Exception
					failCount += 1
				End Try
			Next
			RefreshGridRows() : g_isDirty = True
			Dim msg As String = "图号写入完成！（写入到：" & _targetDesc & "）" & vbCrLf & "成功：" & successCount & " 个"
			If skipCount > 0 Then msg &= vbCrLf & "跳过（已有图号）：" & skipCount & " 个"
			If failCount > 0 Then msg &= vbCrLf & "失败：" & failCount & " 个"
			SetStatus("  图号已写入 " & successCount & " 个零部件", C(0, 130, 55))
			MessageBox.Show(msg, "写入图号 — 完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
			DoFullRefresh()
		Catch ex As Exception
			MessageBox.Show("写入图号失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	' ★ 收集指定属性在所有BomDataRow中的所有不重复值
	Function CollectPropValues(rows As List(Of BomDataRow), propName As String) As List(Of String)
		Dim result As New List(Of String)()
		Dim seen As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
		For Each rr As BomDataRow In rows
			Dim v As String = GetRowPropVal(rr, propName).Trim()
			If Not String.IsNullOrEmpty(v) AndAlso Not seen.Contains(v) Then
				seen.Add(v) : result.Add(v)
			End If
		Next
		result.Sort()
		Return result
	End Function

	' ★ 从BomDataRow内存读取属性值（优先内置字段，再Custom字典，最后ReadAnyProp）
	Function GetRowPropVal(rr As BomDataRow, propName As String) As String
		If String.IsNullOrEmpty(propName) Then Return ""
		Dim pn As String = propName.Replace("（自定义）", "").Trim()
		' 优先从内置字段读（已在内存，无需打开文件）
		Select Case pn.ToLower()
			Case "类别", "category" : Return If(rr.Category Is Nothing, "", rr.Category)
			Case "材料", "material" : Return If(rr.Material Is Nothing, "", rr.Material)
			Case "描述", "description" : Return If(rr.Description Is Nothing, "", rr.Description)
			Case "零件代号", "part number" : Return If(rr.PartNumber Is Nothing, "", rr.PartNumber)
			Case "图号" : Return If(rr.DrawingNo Is Nothing, "", rr.DrawingNo)
		End Select
		' 再从Custom字典读（自定义属性已加载到内存）
		If rr.Custom.ContainsKey(pn) Then Return If(rr.Custom(pn) Is Nothing, "", rr.Custom(pn))
		' 最后才打开文件读（兜底）
		Return ReadAnyProp(rr.FilePath, pn)
	End Function

	' ── 属性写入工程图 界面 ──
	Sub ShowWritePropToDrawingDialog()
		Try
			' ── ini：记忆上次属性选择 ──
			Dim iniPath5 As String = System.IO.Path.Combine(
			System.IO.Path.GetDirectoryName(g_asmDoc.FullDocumentName), ".dwj_prop2dwg.ini")
			Dim dlg As New Form()
			dlg.Text = "属性写入工程图（文字）"
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then dlg.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then dlg.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch : End Try
			dlg.Size = New System.Drawing.Size(Scaled(580), Scaled(730))
			dlg.StartPosition = FormStartPosition.CenterScreen
			dlg.MinimizeBox = False : dlg.MaximizeBox = False
			dlg.ControlBox = True
			dlg.KeyPreview = True
			dlg.FormBorderStyle = FormBorderStyle.Sizable
			dlg.Font = MakeFont(g_fontName, 9.0!)
			dlg.BackColor = C(252, 252, 252)
			Dim pad As Integer = Scaled(10)
			Dim cw As Integer = dlg.ClientSize.Width - pad * 2
			' ── 标题栏 ──
			Dim pnlTitle As New Panel()
			pnlTitle.Dock = DockStyle.Top : pnlTitle.Height = Scaled(40)
			pnlTitle.BackColor = OrangeMid()
			Dim lblT As New Label()
			lblT.Text = "  📄 属性写入工程图（文字）" : lblT.Dock = DockStyle.Fill
			lblT.ForeColor = System.Drawing.Color.White
			lblT.Font = MakeFontBold(g_fontName, 11.0!)
			lblT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnlTitle.Controls.Add(lblT)
			Dim helpWriteDwg As String = "【功能说明】" & vbCrLf &
		   "  把零件的属性值写入对应工程图标题栏的指定文字位置。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 在左侧勾选要写入哪些属性（如图号、名称、材料、数量）" & vbCrLf &
		   "  2. 设置工程图里目标文字的匹配方式（按文字内容定位）" & vbCrLf &
		   "  3. 勾选是否跳过标准件/Content Center 零件" & vbCrLf &
		   "  4. 点「开始写入」" & vbCrLf &
		   "  5. 完成后弹窗提示成功/失败数量" & vbCrLf & vbCrLf &
		   "【常见问题】" & vbCrLf &
		   "  · 提示找不到工程图：该零件没有同名 .idw" & vbCrLf &
		   "  · 写入位置不对：检查标题栏文字是否与匹配内容一致" & vbCrLf &
		   "  · 工程图只读时无法写入，会在报告里列出"
			Dim btnWriteDwgHelp As System.Windows.Forms.Button = MakeStepHelpBtn("属性写入工程图 - 使用说明", helpWriteDwg, True)
			btnWriteDwgHelp.Location = New System.Drawing.Point(pnlTitle.Width - Scaled(82), (pnlTitle.Height - Scaled(24)) \ 2)
			btnWriteDwgHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
			pnlTitle.Controls.Add(btnWriteDwgHelp)
			dlg.Controls.Add(pnlTitle)
			' ── 属性名标签+输入框 ──
			Dim lblProp As New Label()
			lblProp.Text = "选择要写入工程图的属性（双击列表可多次添加）："
			lblProp.Location = New System.Drawing.Point(pad, Scaled(80))
			lblProp.Size = New System.Drawing.Size(cw, Scaled(20))
			lblProp.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblProp)
			Dim txtProps As New System.Windows.Forms.TextBox()
			txtProps.Location = New System.Drawing.Point(pad, Scaled(100))
			txtProps.Size = New System.Drawing.Size(cw, Scaled(24))
			txtProps.Font = MakeFont(g_fontName, 9.5!)
			SetPlaceholder(txtProps, "双击下方列表选择属性名")
			dlg.Controls.Add(txtProps)
			' ── 按钮行 ──
			' ── 类型过滤（互斥，优先级最高）──
			Dim chkOnlyPart5 As New CheckBox()
			chkOnlyPart5.Text = "仅零件" : chkOnlyPart5.Checked = True : chkOnlyPart5.Location = New System.Drawing.Point(pad, Scaled(50))
			chkOnlyPart5.Size = New System.Drawing.Size(Scaled(80), Scaled(22))
			dlg.Controls.Add(chkOnlyPart5)
			Dim chkOnlyAssy5 As New CheckBox()
			chkOnlyAssy5.Text = "仅部件" : chkOnlyAssy5.Location = New System.Drawing.Point(pad + Scaled(84), Scaled(50))
			chkOnlyAssy5.Size = New System.Drawing.Size(Scaled(80), Scaled(22))
			dlg.Controls.Add(chkOnlyAssy5)
			Dim chkOnlyWeld5 As New CheckBox()
			chkOnlyWeld5.Text = "仅焊接件" : chkOnlyWeld5.Location = New System.Drawing.Point(pad + Scaled(168), Scaled(50))
			chkOnlyWeld5.Size = New System.Drawing.Size(Scaled(80), Scaled(22))
			dlg.Controls.Add(chkOnlyWeld5)
			Dim lblTypeHint5 As New Label()
			lblTypeHint5.Text = "类型过滤优先级最高，三者互斥，不勾选=全部"
			lblTypeHint5.Location = New System.Drawing.Point(pad + Scaled(256), Scaled(52))
			lblTypeHint5.Size = New System.Drawing.Size(Scaled(270), Scaled(18))
			lblTypeHint5.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120)
			lblTypeHint5.Font = MakeFont(g_fontName, 8.0!)
			dlg.Controls.Add(lblTypeHint5)
			' 互斥逻辑
			AddHandler chkOnlyPart5.CheckedChanged, Sub(cp5, ep5)
														If chkOnlyPart5.Checked Then chkOnlyAssy5.Checked = False : chkOnlyWeld5.Checked = False
													End Sub
			AddHandler chkOnlyAssy5.CheckedChanged, Sub(ca5, ea5)
														If chkOnlyAssy5.Checked Then chkOnlyPart5.Checked = False : chkOnlyWeld5.Checked = False
													End Sub
			AddHandler chkOnlyWeld5.CheckedChanged, Sub(cw5, ew5)
														If chkOnlyWeld5.Checked Then chkOnlyPart5.Checked = False : chkOnlyAssy5.Checked = False
													End Sub
			' ── 标准件跳过 ──
			Dim chkSkipCC5 As New CheckBox()
			chkSkipCC5.Text = "跳过标准件" : chkSkipCC5.Checked = True
			chkSkipCC5.Location = New System.Drawing.Point(pad, Scaled(130))
			chkSkipCC5.Size = New System.Drawing.Size(Scaled(120), Scaled(22))
			dlg.Controls.Add(chkSkipCC5)
			' ── 跳过条件 ──
			Dim lblSkip5 As New Label()
			lblSkip5.Text = "跳过条件：属性" : lblSkip5.Location = New System.Drawing.Point(pad, Scaled(158))
			lblSkip5.Size = New System.Drawing.Size(Scaled(100), Scaled(22)) : lblSkip5.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblSkip5)
			Dim cmbSkip5 As New System.Windows.Forms.ComboBox()
			cmbSkip5.Location = New System.Drawing.Point(pad + Scaled(104), Scaled(156))
			cmbSkip5.Size = New System.Drawing.Size(Scaled(130), Scaled(22))
			cmbSkip5.DropDownStyle = ComboBoxStyle.DropDown : cmbSkip5.BackColor = System.Drawing.Color.White
			Dim lblSkipEq5 As New Label()
			lblSkipEq5.Text = "包含" : lblSkipEq5.Location = New System.Drawing.Point(pad + Scaled(238), Scaled(158))
			lblSkipEq5.Size = New System.Drawing.Size(Scaled(40), Scaled(22)) : lblSkipEq5.ForeColor = System.Drawing.Color.Black
			Dim txtSkipVal5 As New System.Windows.Forms.TextBox()
			txtSkipVal5.Location = New System.Drawing.Point(pad + Scaled(280), Scaled(156))
			txtSkipVal5.Size = New System.Drawing.Size(Scaled(130), Scaled(22))
			SetPlaceholder(txtSkipVal5, "点击▼选择或手动输入")
			Dim btnSkipDrop5 As New Button()
			btnSkipDrop5.Text = "▼" : btnSkipDrop5.Location = New System.Drawing.Point(pad + Scaled(414), Scaled(156))
			btnSkipDrop5.Size = New System.Drawing.Size(Scaled(22), Scaled(22))
			btnSkipDrop5.FlatStyle = FlatStyle.Flat : btnSkipDrop5.Font = MakeFont(g_fontName, 7.0!)
			btnSkipDrop5.BackColor = System.Drawing.Color.FromArgb(230, 230, 230)
			Dim skipDrop5 As New ToolStripDropDown() : skipDrop5.AutoClose = False
			Dim skipClbHost5 As New ToolStripControlHost(New CheckedListBox())
			Dim skipClb5 As CheckedListBox = CType(skipClbHost5.Control, CheckedListBox)
			skipClb5.CheckOnClick = True : skipClb5.Font = MakeFont(g_fontName, 9.0!)
			skipClb5.Width = Scaled(180) : skipClb5.Height = Scaled(160)
			skipClbHost5.AutoSize = False : skipClbHost5.Size = New System.Drawing.Size(Scaled(180), Scaled(160))
			skipDrop5.Items.Add(skipClbHost5)
			Dim skipBtnHost5 As New ToolStripControlHost(New Button())
			Dim skipBtnOK5 As Button = CType(skipBtnHost5.Control, Button)
			skipBtnOK5.Text = "确定" : skipBtnOK5.Width = Scaled(180) : skipBtnOK5.Height = Scaled(26)
			skipBtnOK5.FlatStyle = FlatStyle.Flat : skipBtnOK5.BackColor = OrangeMid() : skipBtnOK5.ForeColor = System.Drawing.Color.White
			skipBtnHost5.AutoSize = False : skipBtnHost5.Size = New System.Drawing.Size(Scaled(180), Scaled(26))
			skipDrop5.Items.Add(skipBtnHost5)
			AddHandler skipBtnOK5.Click, Sub(sb5, se5)
											 Dim sel5 As New List(Of String)()
											 For Each ci5 As Object In skipClb5.CheckedItems : sel5.Add(CStr(ci5)) : Next
											 txtSkipVal5.Text = String.Join(",", sel5.ToArray())
											 txtSkipVal5.ForeColor = System.Drawing.Color.Black
											 skipDrop5.Close()
										 End Sub
			AddHandler btnSkipDrop5.Click, Sub(sd5, se5)
											   If skipDrop5.Visible Then skipDrop5.Close() : Return
											   Dim pn5d As String = cmbSkip5.Text.Trim()
											   skipClb5.Items.Clear()
											   If Not String.IsNullOrEmpty(pn5d) Then
												   Dim vals5d As List(Of String) = CollectPropValues(g_rows, pn5d)
												   Dim curVals5 As String() = txtSkipVal5.Text.Split(",".ToCharArray())
												   For Each v5d As String In vals5d
													   Dim chk5 As Boolean = Array.Exists(curVals5, Function(x5) x5.Trim().ToLower() = v5d.ToLower())
													   skipClb5.Items.Add(v5d, chk5)
												   Next
											   End If
											   skipDrop5.Show(btnSkipDrop5, New System.Drawing.Point(0, btnSkipDrop5.Height))
										   End Sub
			AddHandler txtSkipVal5.Click, Sub(st5, se5)
											  btnSkipDrop5.PerformClick()
										  End Sub
			AddHandler cmbSkip5.SelectedIndexChanged, Sub(sc5, ec5)
														  If cmbSkip5.SelectedIndex >= 0 Then txtSkipVal5.Text = ""
													  End Sub
			dlg.Controls.Add(cmbSkip5) : dlg.Controls.Add(lblSkipEq5) : dlg.Controls.Add(txtSkipVal5) : dlg.Controls.Add(btnSkipDrop5)
			Dim lblSkipHint5 As New Label()
			lblSkipHint5.Text = "跳过：属性值包含指定值则跳过（写入条件已满足时才判断）"
			lblSkipHint5.Location = New System.Drawing.Point(pad, Scaled(180))
			lblSkipHint5.Size = New System.Drawing.Size(cw, Scaled(16))
			lblSkipHint5.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120)
			lblSkipHint5.Font = MakeFont(g_fontName, 8.0!)
			dlg.Controls.Add(lblSkipHint5)
			' ── 写入条件 ──
			Dim lblOnly5 As New Label()
			lblOnly5.Text = "写入条件：属性" : lblOnly5.Location = New System.Drawing.Point(pad, Scaled(200))
			lblOnly5.Size = New System.Drawing.Size(Scaled(100), Scaled(22)) : lblOnly5.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblOnly5)
			Dim cmbOnly5 As New System.Windows.Forms.ComboBox()
			cmbOnly5.Location = New System.Drawing.Point(pad + Scaled(104), Scaled(198))
			cmbOnly5.Size = New System.Drawing.Size(Scaled(130), Scaled(22))
			cmbOnly5.DropDownStyle = ComboBoxStyle.DropDown : cmbOnly5.BackColor = System.Drawing.Color.White
			Dim lblOnlyEq5 As New Label()
			lblOnlyEq5.Text = "包含" : lblOnlyEq5.Location = New System.Drawing.Point(pad + Scaled(238), Scaled(200))
			lblOnlyEq5.Size = New System.Drawing.Size(Scaled(40), Scaled(22)) : lblOnlyEq5.ForeColor = System.Drawing.Color.Black
			Dim txtOnlyVal5 As New System.Windows.Forms.TextBox()
			txtOnlyVal5.Location = New System.Drawing.Point(pad + Scaled(280), Scaled(198))
			txtOnlyVal5.Size = New System.Drawing.Size(Scaled(130), Scaled(22))
			SetPlaceholder(txtOnlyVal5, "点击▼选择或手动输入")
			Dim btnOnlyDrop5 As New Button()
			btnOnlyDrop5.Text = "▼" : btnOnlyDrop5.Location = New System.Drawing.Point(pad + Scaled(414), Scaled(198))
			btnOnlyDrop5.Size = New System.Drawing.Size(Scaled(22), Scaled(22))
			btnOnlyDrop5.FlatStyle = FlatStyle.Flat : btnOnlyDrop5.Font = MakeFont(g_fontName, 7.0!)
			btnOnlyDrop5.BackColor = System.Drawing.Color.FromArgb(230, 230, 230)
			Dim onlyDrop5 As New ToolStripDropDown() : onlyDrop5.AutoClose = False
			Dim onlyClbHost5 As New ToolStripControlHost(New CheckedListBox())
			Dim onlyClb5 As CheckedListBox = CType(onlyClbHost5.Control, CheckedListBox)
			onlyClb5.CheckOnClick = True : onlyClb5.Font = MakeFont(g_fontName, 9.0!)
			onlyClb5.Width = Scaled(180) : onlyClb5.Height = Scaled(160)
			onlyClbHost5.AutoSize = False : onlyClbHost5.Size = New System.Drawing.Size(Scaled(180), Scaled(160))
			onlyDrop5.Items.Add(onlyClbHost5)
			Dim onlyBtnHost5 As New ToolStripControlHost(New Button())
			Dim onlyBtnOK5 As Button = CType(onlyBtnHost5.Control, Button)
			onlyBtnOK5.Text = "确定" : onlyBtnOK5.Width = Scaled(180) : onlyBtnOK5.Height = Scaled(26)
			onlyBtnOK5.FlatStyle = FlatStyle.Flat : onlyBtnOK5.BackColor = OrangeMid() : onlyBtnOK5.ForeColor = System.Drawing.Color.White
			onlyBtnHost5.AutoSize = False : onlyBtnHost5.Size = New System.Drawing.Size(Scaled(180), Scaled(26))
			onlyDrop5.Items.Add(onlyBtnHost5)
			AddHandler onlyBtnOK5.Click, Sub(ob5, oe5)
											 Dim sel5o As New List(Of String)()
											 For Each ci5o As Object In onlyClb5.CheckedItems : sel5o.Add(CStr(ci5o)) : Next
											 txtOnlyVal5.Text = String.Join(",", sel5o.ToArray())
											 txtOnlyVal5.ForeColor = System.Drawing.Color.Black
											 onlyDrop5.Close()
										 End Sub
			AddHandler btnOnlyDrop5.Click, Sub(od5, oe5)
											   If onlyDrop5.Visible Then onlyDrop5.Close() : Return
											   Dim pn5o As String = cmbOnly5.Text.Trim()
											   onlyClb5.Items.Clear()
											   If Not String.IsNullOrEmpty(pn5o) Then
												   Dim vals5o As List(Of String) = CollectPropValues(g_rows, pn5o)
												   Dim curVals5o As String() = txtOnlyVal5.Text.Split(",".ToCharArray())
												   For Each v5o As String In vals5o
													   Dim chk5o As Boolean = Array.Exists(curVals5o, Function(x5o) x5o.Trim().ToLower() = v5o.ToLower())
													   onlyClb5.Items.Add(v5o, chk5o)
												   Next
											   End If
											   onlyDrop5.Show(btnOnlyDrop5, New System.Drawing.Point(0, btnOnlyDrop5.Height))
										   End Sub
			AddHandler txtOnlyVal5.Click, Sub(ot5, oe5)
											  btnOnlyDrop5.PerformClick()
										  End Sub
			AddHandler cmbOnly5.SelectedIndexChanged, Sub(sc5o, ec5o)
														  If cmbOnly5.SelectedIndex >= 0 Then txtOnlyVal5.Text = ""
													  End Sub
			dlg.Controls.Add(cmbOnly5) : dlg.Controls.Add(lblOnlyEq5) : dlg.Controls.Add(txtOnlyVal5) : dlg.Controls.Add(btnOnlyDrop5)
			Dim lblOnlyHint5 As New Label()
			lblOnlyHint5.Text = "写入：只写入属性值包含指定值的零件（优先级高于上方跳过条件，满足则不判断跳过条件）"
			lblOnlyHint5.Location = New System.Drawing.Point(pad, Scaled(222))
			lblOnlyHint5.Size = New System.Drawing.Size(cw, Scaled(16))
			lblOnlyHint5.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120)
			lblOnlyHint5.Font = MakeFont(g_fontName, 8.0!)
			dlg.Controls.Add(lblOnlyHint5)
			' 填充跳过/写入条件下拉（系统属性+自定义属性）
			' ── 填充跳过/写入条件下拉：固定系统属性 + 所有Custom属性 ──
			Dim condSeenE As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			cmbSkip5.Items.Add("") : cmbOnly5.Items.Add("")
			' 固定系统属性（BomDataRow内置字段）'
			Dim fixedSys5() As String = {"类别", "描述", "材料", "零件代号", "图号", "BOM结构", "文件名"}
			For Each fs5 As String In fixedSys5
				If Not condSeenE.Contains(fs5) Then
					condSeenE.Add(fs5)
					cmbSkip5.Items.Add(fs5) : cmbOnly5.Items.Add(fs5)
				End If
			Next
			' 从所有行Custom字典收集自定义属性'
			For Each rr5c As BomDataRow In g_rows
				For Each kv5c As KeyValuePair(Of String, String) In rr5c.Custom
					If Not String.IsNullOrEmpty(kv5c.Key) AndAlso Not condSeenE.Contains(kv5c.Key) Then
						condSeenE.Add(kv5c.Key)
						cmbSkip5.Items.Add(kv5c.Key) : cmbOnly5.Items.Add(kv5c.Key)
					End If
				Next
			Next
			' ── 文字位置和字体设置 ──
			Dim lblPos5 As New Label()
			lblPos5.Text = "文字位置："
			lblPos5.Location = New System.Drawing.Point(pad, Scaled(244))
			lblPos5.Size = New System.Drawing.Size(Scaled(100), Scaled(22))
			lblPos5.Font = MakeFontBold(g_fontName, 8.5!)
			dlg.Controls.Add(lblPos5)
			Dim lblX5 As New Label() : lblX5.Text = "X:"
			lblX5.Location = New System.Drawing.Point(pad + Scaled(104), Scaled(246))
			lblX5.Size = New System.Drawing.Size(Scaled(18), Scaled(20))
			dlg.Controls.Add(lblX5)
			Dim txtX5 As New System.Windows.Forms.TextBox()
			txtX5.Text = "1.5"
			txtX5.Location = New System.Drawing.Point(pad + Scaled(122), Scaled(244))
			txtX5.Size = New System.Drawing.Size(Scaled(50), Scaled(22))
			txtX5.Font = MakeFont(g_fontName, 8.5!)
			dlg.Controls.Add(txtX5)
			Dim lblY5 As New Label() : lblY5.Text = "Y:"
			lblY5.Location = New System.Drawing.Point(pad + Scaled(178), Scaled(246))
			lblY5.Size = New System.Drawing.Size(Scaled(18), Scaled(20))
			dlg.Controls.Add(lblY5)
			Dim txtY5 As New System.Windows.Forms.TextBox()
			txtY5.Text = "2.0"
			txtY5.Location = New System.Drawing.Point(pad + Scaled(196), Scaled(244))
			txtY5.Size = New System.Drawing.Size(Scaled(50), Scaled(22))
			txtY5.Font = MakeFont(g_fontName, 8.5!)
			dlg.Controls.Add(txtY5)
			Dim lblFs5 As New Label() : lblFs5.Text = "字号:"
			lblFs5.Location = New System.Drawing.Point(pad + Scaled(252), Scaled(246))
			lblFs5.Size = New System.Drawing.Size(Scaled(36), Scaled(20))
			dlg.Controls.Add(lblFs5)
			Dim cmbFs5 As New System.Windows.Forms.ComboBox()
			cmbFs5.Location = New System.Drawing.Point(pad + Scaled(290), Scaled(244))
			cmbFs5.Size = New System.Drawing.Size(Scaled(60), Scaled(22))
			cmbFs5.Font = MakeFont(g_fontName, 8.5!)
			cmbFs5.DropDownStyle = ComboBoxStyle.DropDownList
			For Each fsVal As String In New String() {"1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0", "6.0", "7.0", "8.0", "10.0"}
				cmbFs5.Items.Add(fsVal)
			Next
			cmbFs5.SelectedItem = "3.5"
			dlg.Controls.Add(cmbFs5)
			' ── 说明 ──
			Dim lblHint5 As New Label()
			lblHint5.Text = "说明：把选中属性的值 写入工程图文字。X/Y为图纸坐标（cm），字号单位mm,左下角坐标（0，0）。"
			lblHint5.Location = New System.Drawing.Point(pad, Scaled(268))
			lblHint5.Size = New System.Drawing.Size(Scaled(540), Scaled(18))
			lblHint5.Font = MakeFont(g_fontName, 7.5!)
			lblHint5.ForeColor = C(120, 120, 120)
			dlg.Controls.Add(lblHint5)
			Dim btnExec As New Button()
			btnExec.Text = "执行写入" : btnExec.Size = New System.Drawing.Size(Scaled(100), Scaled(28))
			btnExec.Location = New System.Drawing.Point(pad, Scaled(292))
			btnExec.FlatStyle = FlatStyle.Flat
			btnExec.BackColor = OrangeMid() : btnExec.ForeColor = System.Drawing.Color.White
			btnExec.Font = MakeFontBold(g_fontName, 8.5!)
			dlg.Controls.Add(btnExec)
			Dim btnClose As New Button()
			btnClose.Text = "关闭" : btnClose.Size = New System.Drawing.Size(Scaled(80), Scaled(28))
			btnClose.Location = New System.Drawing.Point(pad + Scaled(108), Scaled(292))
			btnClose.FlatStyle = FlatStyle.Flat
			btnClose.BackColor = C(120, 60, 180) : btnClose.ForeColor = System.Drawing.Color.White
			AddHandler btnClose.Click, Sub(s, e) dlg.Close()
			dlg.Controls.Add(btnClose)
			' ── 属性列表标签 ──
			Dim lblResult As New Label()
			lblResult.Text = "属性列表（双击添加到上方输入框，可多次添加）："
			lblResult.Location = New System.Drawing.Point(pad, Scaled(328))
			lblResult.Size = New System.Drawing.Size(cw, Scaled(18))
			lblResult.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblResult)
			' ── 属性列表 ──
			Dim lstResult As New System.Windows.Forms.ListBox()
			lstResult.Location = New System.Drawing.Point(pad, Scaled(348))
			lstResult.Size = New System.Drawing.Size(cw, dlg.ClientSize.Height - Scaled(348) - pad)
			lstResult.Font = MakeFont(g_fontName, 9.0!)
			lstResult.BackColor = C(240, 246, 255)
			lstResult.SelectionMode = SelectionMode.One
			lstResult.Items.Add("正在扫描属性...")
			' 双击：追加到输入框（允许多次添加）
			AddHandler lstResult.DoubleClick, Sub(s, e)
												  If lstResult.SelectedItem Is Nothing Then Return
												  Dim picked As String = CStr(lstResult.SelectedItem).Trim()
												  If String.IsNullOrEmpty(picked) OrElse picked.StartsWith("──") OrElse picked.StartsWith("正在") Then Return
												  ' 去掉分组前缀 [系统] 等
												  Dim bracketEnd As Integer = picked.IndexOf("] ")
												  If bracketEnd >= 0 Then picked = picked.Substring(bracketEnd + 2).Trim()
												  Dim cur As String = txtProps.Text.Trim()
												  If cur = "双击下方列表选择属性名" OrElse String.IsNullOrEmpty(cur) Then
													  txtProps.Text = picked : txtProps.ForeColor = System.Drawing.Color.Black
												  Else
													  txtProps.Text = cur & "," & picked
												  End If
											  End Sub
			dlg.Controls.Add(lstResult)
			' 调整属性列表位置（往下移，为条件控件腾空间）
			lblResult.Location = New System.Drawing.Point(pad, Scaled(328))
			lstResult.Location = New System.Drawing.Point(pad, Scaled(348))
			lstResult.Size = New System.Drawing.Size(cw, dlg.ClientSize.Height - Scaled(348) - pad)
			' ── 执行写入 ──
			AddHandler btnExec.Click, Sub(s, e)
										  Dim propInput As String = txtProps.Text.Trim()
										  If propInput = "双击下方列表选择属性名" OrElse String.IsNullOrEmpty(propInput) Then
											  MessageBox.Show("请先选择要写入的属性名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
										  End If
										  ' 读取类型过滤状态到局部变量（避免Lambda闭包问题）
										  Dim _onlyPart5 As Boolean = chkOnlyPart5.Checked
										  Dim _onlyAssy5 As Boolean = chkOnlyAssy5.Checked
										  Dim _onlyWeld5 As Boolean = chkOnlyWeld5.Checked
										  Dim _skipCC5 As Boolean = chkSkipCC5.Checked
										  Dim _skipPN5x As String = cmbSkip5.Text.Trim()
										  Dim _skipPV5x As String = txtSkipVal5.Text.Trim()
										  Dim _onlyPN5x As String = cmbOnly5.Text.Trim()
										  Dim _onlyPV5x As String = txtOnlyVal5.Text.Trim()
										  ' 保存属性选择到ini
										  Try : System.IO.File.WriteAllText(iniPath5, propInput, System.Text.Encoding.UTF8) : Catch : End Try
										  ' 解析属性名列表（逗号分隔）
										  Dim propNames() As String = propInput.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
										  ' ── 执行前扫描：检查哪些零件缺少属性 ──
										  Dim missingList As New System.Text.StringBuilder()
										  Dim scanSeen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rrScan As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rrScan.FilePath) OrElse rrScan.IsMissing Then Continue For
											  If scanSeen.Contains(rrScan.FilePath) Then Continue For
											  scanSeen.Add(rrScan.FilePath)
											  Dim missingProps As New List(Of String)()
											  For Each pnScan As String In propNames
												  Dim pnScanT As String = pnScan.Trim()
												  Dim hasVal As Boolean = False
												  Select Case pnScanT.ToLower()
													  Case "零件代号", "part number" : hasVal = Not String.IsNullOrEmpty(rrScan.PartNumber)
													  Case "描述", "description" : hasVal = Not String.IsNullOrEmpty(rrScan.Description)
													  Case "材料", "material" : hasVal = Not String.IsNullOrEmpty(rrScan.Material)
													  Case "类别", "category" : hasVal = Not String.IsNullOrEmpty(rrScan.Category)
													  Case "图号" : hasVal = Not String.IsNullOrEmpty(rrScan.DrawingNo)
													  Case Else : hasVal = rrScan.Custom.ContainsKey(pnScanT) AndAlso Not String.IsNullOrEmpty(rrScan.Custom(pnScanT))
												  End Select
												  If Not hasVal Then missingProps.Add(pnScanT)
											  Next
											  If missingProps.Count > 0 Then
												  missingList.AppendLine(rrScan.FileName & "  缺少: " & String.Join(", ", missingProps.ToArray()))
											  End If
										  Next
										  ' 缺失属性自动跳过，不弹窗阻断'
										  Dim successCount As Integer = 0
										  Dim skipCount As Integer = 0
										  Dim failCount As Integer = 0
										  Dim noDrawingCount As Integer = 0
										  Try
											  Dim seenFp5 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
											  For Each rr5 As BomDataRow In g_rows
												  If String.IsNullOrEmpty(rr5.FilePath) OrElse rr5.IsMissing Then Continue For
												  If seenFp5.Contains(rr5.FilePath) Then Continue For
												  seenFp5.Add(rr5.FilePath)
												  ' ── 类型过滤（优先级最高）──
												  If _onlyPart5 Then
													  If rr5.DocType <> "零件" Then Continue For
												  ElseIf _onlyAssy5 Then
													  If rr5.DocType <> "部件" Then Continue For
												  ElseIf _onlyWeld5 Then
													  If Not rr5.IsWeldment Then Continue For
												  End If
												  ' ── 条件判断（移植自写入图号）──
												  If chkSkipCC5.Checked AndAlso (rr5.IsContentCenter OrElse rr5.IsLocalStandard) Then Continue For
												  Dim skipPN5 As String = cmbSkip5.Text.Trim()
												  Dim skipPV5 As String = txtSkipVal5.Text.Trim()
												  If skipPV5 = "点击▼选择或手动输入" Then skipPV5 = ""
												  Dim useSkip5 As Boolean = Not String.IsNullOrEmpty(skipPN5) AndAlso Not String.IsNullOrEmpty(skipPV5)
												  Dim onlyPN5 As String = cmbOnly5.Text.Trim()
												  Dim onlyPV5 As String = txtOnlyVal5.Text.Trim()
												  If onlyPV5 = "点击▼选择或手动输入" Then onlyPV5 = ""
												  Dim useOnly5 As Boolean = Not String.IsNullOrEmpty(onlyPN5) AndAlso Not String.IsNullOrEmpty(onlyPV5)
												  If useOnly5 Then
													  Dim _ov5 As String = GetRowPropVal(rr5, onlyPN5).ToLower()
													  Dim _om5 As Boolean = False
													  For Each _ov5x As String In onlyPV5.Split(",".ToCharArray())
														  If Not String.IsNullOrEmpty(_ov5x.Trim()) AndAlso _ov5.Contains(_ov5x.Trim().ToLower()) Then _om5 = True : Exit For
													  Next
													  If Not _om5 Then Continue For
												  Else
													  If useSkip5 Then
														  Dim _sv5 As String = GetRowPropVal(rr5, skipPN5).ToLower()
														  Dim _sm5 As Boolean = False
														  For Each _sv5x As String In skipPV5.Split(",".ToCharArray())
															  If Not String.IsNullOrEmpty(_sv5x.Trim()) AndAlso _sv5.Contains(_sv5x.Trim().ToLower()) Then _sm5 = True : Exit For
														  Next
														  If _sm5 Then Continue For
													  End If
												  End If
												  ' 找对应工程图（同目录同名.idw/.dwg）
												  Dim partDir As String = System.IO.Path.GetDirectoryName(rr5.FilePath)
												  Dim partBase As String = System.IO.Path.GetFileNameWithoutExtension(rr5.FilePath)
												  Dim dwgPath As String = ""
												  For Each ext5 As String In New String() {".idw", ".dwg"}
													  Dim candidate As String = System.IO.Path.Combine(partDir, partBase & ext5)
													  If System.IO.File.Exists(candidate) Then dwgPath = candidate : Exit For
												  Next
												  If String.IsNullOrEmpty(dwgPath) Then dwgPath = FindDrawingInProject(partBase)
												  If String.IsNullOrEmpty(dwgPath) Then noDrawingCount += 1 : Continue For
												  Try
													  ' 打开工程图'
													  Dim dwgDoc As DrawingDocument = Nothing
													  For Each od5 As Document In ThisApplication.Documents
														  If String.Compare(od5.FullDocumentName, dwgPath, True) = 0 Then
															  dwgDoc = CType(od5, DrawingDocument) : Exit For
														  End If
													  Next
													  If dwgDoc Is Nothing Then
														  dwgDoc = CType(ThisApplication.Documents.Open(dwgPath, True), DrawingDocument)
													  End If
													  If dwgDoc Is Nothing Then failCount += 1 : Continue For
													  Dim sheet5 As Sheet = dwgDoc.ActiveSheet
													  Dim txtLines5 As New System.Text.StringBuilder()
													  For Each pn5 As String In propNames
														  Dim pn5t As String = pn5.Trim()
														  If String.IsNullOrEmpty(pn5t) Then Continue For
														  Dim propVal5 As String = ""
														  Select Case pn5t.ToLower()
															  Case "零件代号", "part number" : propVal5 = If(rr5.PartNumber Is Nothing, "", rr5.PartNumber)
															  Case "描述", "description" : propVal5 = If(rr5.Description Is Nothing, "", rr5.Description)
															  Case "材料", "material" : propVal5 = If(rr5.Material Is Nothing, "", rr5.Material)
															  Case "类别", "category" : propVal5 = If(rr5.Category Is Nothing, "", rr5.Category)
															  Case "图号" : propVal5 = If(rr5.DrawingNo Is Nothing, "", rr5.DrawingNo)
															  Case Else
																  If rr5.Custom.ContainsKey(pn5t) Then propVal5 = If(rr5.Custom(pn5t) Is Nothing, "", rr5.Custom(pn5t))
																  If String.IsNullOrEmpty(propVal5) AndAlso System.IO.File.Exists(rr5.FilePath) Then
																	  Try
																		  Dim tDoc5 As Document = Nothing
																		  For Each od5x As Document In ThisApplication.Documents
																			  If String.Compare(od5x.FullDocumentName, rr5.FilePath, True) = 0 Then tDoc5 = od5x : Exit For
																		  Next
																		  If tDoc5 Is Nothing Then tDoc5 = ThisApplication.Documents.Open(rr5.FilePath, False)
																		  If tDoc5 IsNot Nothing Then
																			  For Each ps5 As PropertySet In tDoc5.PropertySets
																				  Try
																					  Dim p5 As Inventor.Property = ps5.Item(pn5t)
																					  propVal5 = CStr(p5.Value) : Exit For
																				  Catch : End Try
																			  Next
																		  End If
																	  Catch : End Try
																  End If
														  End Select
														  If String.IsNullOrEmpty(propVal5) Then Continue For
														  If txtLines5.Length > 0 Then txtLines5.Append("    ")
														  txtLines5.Append(pn5t & ": " & propVal5)
													  Next
													  If txtLines5.Length > 0 Then
														  Dim oTG5 As TransientGeometry = ThisApplication.TransientGeometry
														  Dim posX5 As Double = 1.5 : Dim posY5 As Double = 2.0 : Dim fontSize5 As Double = 3.5
														  Try : posX5 = Double.Parse(txtX5.Text.Trim()) : Catch : End Try
														  Try : posY5 = Double.Parse(txtY5.Text.Trim()) : Catch : End Try
														  Try : fontSize5 = Double.Parse(CStr(cmbFs5.SelectedItem)) : Catch : End Try
														  Dim oPoint5 As Point2d = oTG5.CreatePoint2d(posX5, posY5)
														  Dim fontSizeCm5 As Double = fontSize5 / 10.0
														  Dim styledText5 As String = "<StyleOverride FontSize='" & fontSizeCm5.ToString("0.000") & "'>" & txtLines5.ToString() & "</StyleOverride>"
														  Dim oNote5 As GeneralNote = sheet5.DrawingNotes.GeneralNotes.AddFitted(oPoint5, styledText5)
													  End If
													  Try
														  dwgDoc.Save()
														  dwgDoc.Close(False)
													  Catch : End Try
													  successCount += 1
												  Catch ex5 As Exception
													  failCount += 1
												  End Try
											  Next
											  Dim msg5 As String = "写入完成！" & vbCrLf &
	"成功：" & successCount & " 个工程图" & vbCrLf &
	"无工程图：" & noDrawingCount & " 个" & vbCrLf &
	"失败：" & failCount & " 个"
											  MessageBox.Show(msg5, "属性写入工程图 — 完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
											  SetStatus("  属性已写入 " & successCount & " 个工程图", C(0, 130, 55))
										  Catch ex5 As Exception
											  MessageBox.Show("执行失败：" & ex5.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
										  End Try
									  End Sub
			' ── 窗口加载时自动扫描属性 ──
			AddHandler dlg.Shown, Sub(ss, es)
									  Try
										  ' 从ini读取上次所有设置
										  If System.IO.File.Exists(iniPath5) Then
											  Try
												  Dim sec5 As Dictionary(Of String, String) = ReadSection(iniPath5, "PropToDrawing")
												  If sec5.Count > 0 Then
													  If sec5.ContainsKey("Props") AndAlso Not String.IsNullOrEmpty(sec5("Props")) Then
														  txtProps.Text = sec5("Props") : txtProps.ForeColor = System.Drawing.Color.Black
													  End If
													  If sec5.ContainsKey("SkipCC") Then chkSkipCC5.Checked = (sec5("SkipCC").ToLower() = "true")
													  If sec5.ContainsKey("OnlyPart") Then chkOnlyPart5.Checked = (sec5("OnlyPart").ToLower() = "true")
													  If sec5.ContainsKey("OnlyAssy") Then chkOnlyAssy5.Checked = (sec5("OnlyAssy").ToLower() = "true")
													  If sec5.ContainsKey("OnlyWeld") Then chkOnlyWeld5.Checked = (sec5("OnlyWeld").ToLower() = "true")
													  If sec5.ContainsKey("SkipPropName") Then cmbSkip5.Text = sec5("SkipPropName")
													  If sec5.ContainsKey("SkipPropVal") AndAlso Not String.IsNullOrEmpty(sec5("SkipPropVal")) Then
														  txtSkipVal5.Text = sec5("SkipPropVal") : txtSkipVal5.ForeColor = System.Drawing.Color.Black
													  End If
													  If sec5.ContainsKey("OnlyPropName") Then cmbOnly5.Text = sec5("OnlyPropName")
													  If sec5.ContainsKey("OnlyPropVal") AndAlso Not String.IsNullOrEmpty(sec5("OnlyPropVal")) Then
														  txtOnlyVal5.Text = sec5("OnlyPropVal") : txtOnlyVal5.ForeColor = System.Drawing.Color.Black
													  End If
												  End If
											  Catch : End Try
										  End If
										  lstResult.Items.Clear()
										  Dim sysProps As New List(Of String)()
										  Dim custProps As New List(Of String)()
										  Dim seenSys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim seenCust As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim seenFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rr As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rr.FilePath) OrElse rr.IsMissing Then Continue For
											  If seenFp.Contains(rr.FilePath) Then Continue For
											  seenFp.Add(rr.FilePath)
											  Try
												  Dim tDoc As Document = Nothing
												  For Each od As Document In ThisApplication.Documents
													  If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then tDoc = od : Exit For
												  Next
												  If tDoc Is Nothing AndAlso System.IO.File.Exists(rr.FilePath) Then
													  tDoc = ThisApplication.Documents.Open(rr.FilePath, False)
												  End If
												  If tDoc Is Nothing Then Continue For
												  ' 系统属性集（设计跟踪+概要）
												  For Each psGuid As String In New String() {GUID_DTP, GUID_SUMMARY, GUID_DOCSUMMARY}
													  Try
														  Dim ps As PropertySet = tDoc.PropertySets.Item(psGuid)
														  For Each p As Inventor.Property In ps
															  Try
																  Dim pdn As String = p.DisplayName.Trim()
																  If String.IsNullOrEmpty(pdn) Then pdn = p.Name.Trim()
																  If Not seenSys.Contains(pdn) Then seenSys.Add(pdn) : sysProps.Add(pdn)
															  Catch : End Try
														  Next
													  Catch : End Try
												  Next
												  ' 自定义属性集（UDP）' & vbCrLf &
												  Try
													  Dim udp As PropertySet = tDoc.PropertySets.Item(GUID_UDP)
													  For Each p As Inventor.Property In udp
														  Try
															  Dim pdn As String = p.DisplayName.Trim()
															  If String.IsNullOrEmpty(pdn) Then pdn = p.Name.Trim()
															  If Not seenCust.Contains(pdn) Then seenCust.Add(pdn) : custProps.Add(pdn)
														  Catch : End Try
													  Next
												  Catch : End Try
											  Catch : End Try
										  Next
										  ' 填充列表
										  sysProps.Sort() : custProps.Sort()
										  If sysProps.Count > 0 Then
											  lstResult.Items.Add("── 系统属性 ──")
											  For Each p As String In sysProps : lstResult.Items.Add(p) : Next
										  End If
										  If custProps.Count > 0 Then
											  lstResult.Items.Add("── 自定义属性 ──")
											  For Each p As String In custProps : lstResult.Items.Add(p) : Next
										  End If
										  If lstResult.Items.Count = 0 Then lstResult.Items.Add("（未找到任何属性）")
										  ' ── 同步填充跳过/写入条件下拉（与lstResult同步）──
										  cmbSkip5.Items.Clear() : cmbOnly5.Items.Clear()
										  cmbSkip5.Items.Add("") : cmbOnly5.Items.Add("")
										  For Each sp6 As String In sysProps
											  cmbSkip5.Items.Add(sp6) : cmbOnly5.Items.Add(sp6)
										  Next
										  For Each cp6 As String In custProps
											  cmbSkip5.Items.Add(cp6) : cmbOnly5.Items.Add(cp6)
										  Next
									  Catch ex2 As Exception
										  lstResult.Items.Clear() : lstResult.Items.Add("扫描失败: " & ex2.Message)
									  End Try
								  End Sub
			' 关闭时保存属性到ini
			AddHandler dlg.FormClosing, Sub(sf, ef)
											ef.Cancel = False
											Try
												Dim iniLines5 As New List(Of String)()
												Dim cur5 As String = txtProps.Text.Trim()
												If cur5 = "双击下方列表选择属性名" Then cur5 = ""
												Dim sv5 As String = txtSkipVal5.Text.Trim()
												If sv5 = "点击▼选择或手动输入" Then sv5 = ""
												Dim ov5 As String = txtOnlyVal5.Text.Trim()
												If ov5 = "点击▼选择或手动输入" Then ov5 = ""
												iniLines5.Add("[PropToDrawing]")
												iniLines5.Add("Props=" & cur5)
												iniLines5.Add("SkipCC=" & chkSkipCC5.Checked.ToString().ToLower())
												iniLines5.Add("OnlyPart=" & chkOnlyPart5.Checked.ToString().ToLower())
												iniLines5.Add("OnlyAssy=" & chkOnlyAssy5.Checked.ToString().ToLower())
												iniLines5.Add("OnlyWeld=" & chkOnlyWeld5.Checked.ToString().ToLower())
												iniLines5.Add("SkipPropName=" & cmbSkip5.Text.Trim())
												iniLines5.Add("SkipPropVal=" & sv5)
												iniLines5.Add("OnlyPropName=" & cmbOnly5.Text.Trim())
												iniLines5.Add("OnlyPropVal=" & ov5)
												System.IO.File.WriteAllText(iniPath5, String.Join(vbCrLf, iniLines5.ToArray()), System.Text.Encoding.UTF8)
											Catch : End Try
										End Sub
			AddHandler dlg.KeyDown, Sub(sk, ek)
										If ek.KeyCode = Keys.Escape Then dlg.Close()
									End Sub
			dlg.ShowDialog()
		Catch ex As Exception
			MessageBox.Show("界面打开失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	' ★ 安全写入自定义属性（先检查是否存在，不存在则创建）
	Sub SafeWriteUDP(udp As PropertySet, propName As String, propValue As String)
		Try
			Dim exists As Boolean = False
			For Each p As Inventor.Property In udp
				If String.Compare(p.Name, propName, True) = 0 Then
					exists = True : Exit For
				End If
			Next
			If exists Then
				udp.Item(propName).Value = propValue
			Else
				udp.Add(propValue, propName)
			End If
		Catch ex As Exception

		End Try
	End Sub

	Function GetUDP(tDoc As Document) As PropertySet
		Try
			Return tDoc.PropertySets.Item("{D5CDD505-2E9C-101B-9397-08002B2CF9AE}")
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return Nothing
		End Try
	End Function

	' ── 缩略图悬停弹出大图 ──
	Sub ShowThumbPopup(img As System.Drawing.Image, rowIdx As Integer)
		Try
			CloseThumbPopup()
			g_thumbPopRow = rowIdx
			' 弹窗尺寸：最大 480x360，等比缩放
			Dim maxW As Integer = Scaled(480)
			Dim maxH As Integer = Scaled(360)
			Dim sc As Double = Math.Min(CDbl(maxW) / img.Width, CDbl(maxH) / img.Height)
			Dim dW As Integer = CInt(img.Width * sc)
			Dim dH As Integer = CInt(img.Height * sc)
			' 创建无边框弹窗
			Dim pop As New Form()
			pop.FormBorderStyle = FormBorderStyle.None
			pop.StartPosition = FormStartPosition.Manual
			pop.ShowInTaskbar = False
			pop.TopMost = True
			pop.BackColor = System.Drawing.Color.White
			pop.Size = New System.Drawing.Size(dW + 4, dH + 4)
			' 定位：跟随鼠标，偏移避免遮挡
			Dim cur As System.Drawing.Point = System.Windows.Forms.Cursor.Position
			Dim scr As System.Drawing.Rectangle = System.Windows.Forms.Screen.FromPoint(cur).WorkingArea
			Dim px As Integer = cur.X + Scaled(20)
			Dim py As Integer = cur.Y + Scaled(20)
			If px + dW + 4 > scr.Right Then px = cur.X - dW - Scaled(20)
			If py + dH + 4 > scr.Bottom Then py = cur.Y - dH - Scaled(20)
			pop.Location = New System.Drawing.Point(px, py)
			' 用 Paint 事件手绘图片（避免 PictureBox 被安全扫描拦截）
			Dim popImg As System.Drawing.Image = img
			Dim popDW As Integer = dW
			Dim popDH As Integer = dH
			AddHandler pop.Paint, Sub(s2, e2)
									  e2.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
									  e2.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
									  e2.Graphics.FillRectangle(System.Drawing.Brushes.White, 0, 0, popDW + 4, popDH + 4)
									  e2.Graphics.DrawImage(popImg, 2, 2, popDW, popDH)
									  e2.Graphics.DrawRectangle(New System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 160, 160), 1),
			0, 0, popDW + 3, popDH + 3)
								  End Sub
			g_thumbPopup = pop
			pop.Show()
		Catch : End Try
	End Sub

	Sub CloseThumbPopup()
		Try
			If g_thumbPopup IsNot Nothing Then
				g_thumbPopup.Close()
				g_thumbPopup.Dispose()
				g_thumbPopup = Nothing
			End If
			g_thumbPopRow = -1
		Catch : End Try
	End Sub
	' 滚轮滚动：立即关闭缩略图弹窗，防止错位
	Sub OnGridMouseWheel(sender As Object, e As MouseEventArgs)
		CloseThumbPopup()
	End Sub

	' 滚动条滚动：立即关闭缩略图弹窗
	Sub OnGridScroll(sender As Object, e As ScrollEventArgs)
		CloseThumbPopup()
	End Sub

	Function StripInvExt(fileName As String) As String
		If String.IsNullOrEmpty(fileName) Then Return fileName
		Dim lower As String = fileName.ToLower().TrimEnd()
		For Each ext As String In New String() {".ipt", ".iam", ".idw", ".dwg"}
			If lower.EndsWith(ext) Then
				Return fileName.Substring(0, fileName.Length - ext.Length)
			End If
		Next
		Return fileName
	End Function

	Function ColorRenameModified() As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(255, 230, 180)
	End Function
	Function ColorDuplicate() As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(255, 255, 100)
	End Function
	Function ColorMirror() As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(100, 220, 220)
	End Function
	Function ColorRenameFail() As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(255, 200, 200)
	End Function

	Function CheckDocumentType() As Boolean
		Try
			Dim doc As Document = ThisApplication.ActiveEditDocument
			If doc Is Nothing Then
				MessageBox.Show("未检测到活动文档。BOM工具 需要在【部件（.iam）】中运行。",
				"BOM工具 — 文档类型错误", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Return False
			End If
			If doc.DocumentType <> kAssemblyDocumentObject Then
				Dim docTypeName As String = "未知类型"
				Select Case doc.DocumentType
					Case kPartDocumentObject : docTypeName = "零件（.ipt）"
					Case kDrawingDocumentObject : docTypeName = "工程图（.idw / .dwg）"
					Case kPresentationDocumentObject : docTypeName = "表达视图（.ipn）"
				End Select
				MessageBox.Show("当前文档是【" & docTypeName & "】，无法运行 BOM工具。" & vbCrLf &
				"BOM工具 只能在【部件（.iam）】中运行，请切换后再执行。",
				"BOM工具 — 仅支持部件", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Return False
			End If
			Return True
		Catch ex As Exception
			MessageBox.Show("文档类型检查失败：" & ex.Message,
			"BOM工具 — 错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
		End Try
	End Function

	Function IsMirrorPart(filePath As String) As Boolean
		Try
			Dim tDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If String.Compare(od.FullDocumentName, filePath, True) = 0 Then
					tDoc = od : Exit For
				End If
			Next
			If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(filePath, False)
			If tDoc.DocumentType <> kPartDocumentObject Then Return False
			Dim partDoc As PartDocument = CType(tDoc, PartDocument)
			Dim partDef As PartComponentDefinition = partDoc.ComponentDefinition
			For Each feat As PartFeature In partDef.Features
				If CInt(feat.Type) = 36 Then Return True
			Next
			Return False
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return False
		End Try
	End Function

	Function FindDrawingInProject(partBase As String) As String
		Try
			Dim projDir As String = System.IO.Path.GetDirectoryName(ThisApplication.DesignProjectManager.ActiveDesignProject.FullFileName)
			If Not String.IsNullOrEmpty(projDir) AndAlso System.IO.Directory.Exists(projDir) Then
				For Each ext As String In New String() {".idw", ".dwg"}
					Dim found() As String = System.IO.Directory.GetFiles(projDir, partBase & ext, System.IO.SearchOption.AllDirectories)
					For Each f As String In found
						If Not f.ToLower().Contains("\oldversions\") Then Return f
					Next
				Next
			End If
		Catch : End Try
		Return ""
	End Function

	Function CheckDrawingExists(filePath As String) As String
		Try
			Dim dir As String = System.IO.Path.GetDirectoryName(filePath)
			Dim base As String = System.IO.Path.GetFileNameWithoutExtension(filePath)
			If System.IO.File.Exists(System.IO.Path.Combine(dir, base & ".idw")) OrElse
				System.IO.File.Exists(System.IO.Path.Combine(dir, base & ".dwg")) Then
				Return ChrW(26377) & ChrW(22270)
			End If
			Return ""
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return ""
		End Try
	End Function

	' 一次打开文件，同时获取类型和钣金信息，避免重复打开
	Sub GetDocInfo(filePath As String, docType As String,
		ByRef partType As String, ByRef isSheetMetal As Boolean,
		Optional ByRef isWeldment As Boolean = False)
		partType = docType
		isSheetMetal = False
		isWeldment = False
		Try
			Dim tDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If String.Compare(od.FullDocumentName, filePath, True) = 0 Then
					tDoc = od : Exit For
				End If
			Next
			If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(filePath, False)
			If tDoc.DocumentType = kPartDocumentObject Then
				Dim pd As PartDocument = CType(tDoc, PartDocument)
				If TypeOf pd.ComponentDefinition Is SheetMetalComponentDefinition Then
					partType = ChrW(38051) & ChrW(37329)
					isSheetMetal = True
				Else
					partType = ChrW(38646) & ChrW(20214)
				End If
			ElseIf tDoc.DocumentType = kAssemblyDocumentObject Then
				Dim ad As AssemblyDocument = CType(tDoc, AssemblyDocument)
				If TypeOf ad.ComponentDefinition Is WeldmentComponentDefinition Then
					partType = ChrW(28938) & ChrW(25509)
					isWeldment = True
				Else
					partType = ChrW(37096) & ChrW(20214)
				End If
			End If
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub LoadMaterialList()
		Try
			If Not g_grid.Columns.Contains("cMat") Then Return
			Dim matCol As DataGridViewComboBoxColumn = CType(g_grid.Columns("cMat"), DataGridViewComboBoxColumn)
			matCol.Items.Clear()
			matCol.Items.Add("")
			' 先加当前BOM里已有的材料
			Dim seen As New HashSet(Of String)
			For Each rr As BomDataRow In g_rows
				If Not String.IsNullOrEmpty(rr.Material) AndAlso Not seen.Contains(rr.Material) Then
					seen.Add(rr.Material)
					matCol.Items.Add(rr.Material)
				End If
			Next
			' 再加材料库里的材料
			Try
				Dim matLibs As Object = ThisApplication.AssetLibraries
				For Each assetLib As Object In matLibs
					Try
						Dim libMats As Object = assetLib.MaterialAssets
						For Each mat As Object In libMats
							Dim matName As String = CStr(mat.DisplayName)
							If Not seen.Contains(matName) Then
								seen.Add(matName)
								matCol.Items.Add(matName)
							End If
						Next
					Catch : End Try
				Next
			Catch : End Try
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub LoadTypeIcons()
		Try
			g_typeIcons.Clear()
			Dim curRowH As Integer = If(g_grid IsNot Nothing, g_grid.RowTemplate.Height, DEFAULT_ROW_H)
			Dim sz As Integer = Math.Max(16, Math.Min(curRowH - 4, 48))
			Dim szSM As Integer = Math.Max(12, CInt(sz * 0.82))

			' 零件图标：提取32x32系统图标再高质量缩放，避免模糊
			Dim iptPath As String = ""
			For Each rr As BomDataRow In g_rows
				If rr.FilePath.ToLower().EndsWith(".ipt") Then iptPath = rr.FilePath : Exit For
			Next
			If Not String.IsNullOrEmpty(iptPath) AndAlso System.IO.File.Exists(iptPath) Then
				Dim icoP As System.Drawing.Icon = System.Drawing.Icon.ExtractAssociatedIcon(iptPath)
				' 先转成32x32 Bitmap，再高质量缩放到目标尺寸
				Dim raw32 As New System.Drawing.Bitmap(32, 32)
				Dim gRaw As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(raw32)
				gRaw.DrawIcon(icoP, New System.Drawing.Rectangle(0, 0, 32, 32))
				gRaw.Dispose()
				Dim bmpPart As New System.Drawing.Bitmap(sz, sz)
				Dim gPart As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpPart)
				gPart.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
				gPart.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				gPart.DrawImage(raw32, 0, 0, sz, sz)
				gPart.Dispose() : raw32.Dispose()
				g_typeIcons(ChrW(38646) & ChrW(20214)) = bmpPart
				' 标准件图标：螺栓侧面正视（六角头侧面+螺杆+螺纹）
				Dim bmpCC As New System.Drawing.Bitmap(sz, sz)
				Dim gCC As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpCC)
				gCC.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				gCC.Clear(System.Drawing.Color.Transparent)
				Dim bFill As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(185, 185, 205))
				Dim bFillDark As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(140, 140, 165))
				Dim bPen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(70, 70, 100), 1)
				Dim bHiPen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(235, 235, 250), 1)
				Dim bThPen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(90, 90, 120), 1)
				Dim cx2 As Single = sz / 2.0!
				Dim pad As Single = sz * 0.12!
				' ── 六角头侧面（带倒角的矩形）──
				Dim hW As Single = sz * 0.68!
				Dim hH As Single = sz * 0.26!
				Dim hX As Single = cx2 - hW / 2
				Dim hY As Single = pad
				Dim chamfer As Single = sz * 0.055!
				' 六角头带倒角路径（8点多边形）
				Dim headPts() As System.Drawing.PointF = {
				New System.Drawing.PointF(hX + chamfer, hY),
				New System.Drawing.PointF(hX + hW - chamfer, hY),
				New System.Drawing.PointF(hX + hW, hY + chamfer),
				New System.Drawing.PointF(hX + hW, hY + hH - chamfer),
				New System.Drawing.PointF(hX + hW - chamfer, hY + hH),
				New System.Drawing.PointF(hX + chamfer, hY + hH),
				New System.Drawing.PointF(hX, hY + hH - chamfer),
				New System.Drawing.PointF(hX, hY + chamfer)
				}
				gCC.FillPolygon(bFill, headPts)
				gCC.DrawPolygon(bPen, headPts)
				' 六角头两道分割线（模拟侧面三个面）
				Dim lineOffset As Single = hW * 0.22!
				gCC.DrawLine(bPen, cx2 - lineOffset, hY + chamfer, cx2 - lineOffset, hY + hH)
				gCC.DrawLine(bPen, cx2 + lineOffset, hY + chamfer, cx2 + lineOffset, hY + hH)
				' 高光（左上边缘）
				gCC.DrawLine(bHiPen, hX + chamfer + 1, hY + 1, hX + hW - chamfer - 1, hY + 1)
				gCC.DrawLine(bHiPen, hX + 1, hY + chamfer + 1, hX + 1, hY + hH - chamfer - 1)
				' ── 螺杆 ──
				Dim rodW As Single = sz * 0.22!
				Dim rodTop As Single = hY + hH
				Dim rodBot As Single = sz - sz * 0.12!
				Dim rodX As Single = cx2 - rodW / 2
				gCC.FillRectangle(bFillDark, rodX, rodTop, rodW, rodBot - rodTop)
				gCC.DrawRectangle(bPen, rodX, rodTop, rodW, rodBot - rodTop)
				' 螺纹（锯齿线，左右交替）
				Dim rodLen As Single = rodBot - rodTop
				Dim threadCount As Integer = Math.Max(3, CInt(rodLen / (sz * 0.12!)))
				Dim threadStep As Single = rodLen / (threadCount + 1)
				For k As Integer = 1 To threadCount
					Dim ty2 As Single = rodTop + threadStep * k
					' 螺杆中间横线（螺纹槽）
					gCC.DrawLine(bThPen, rodX + 1, ty2, rodX + rodW - 1, ty2)
					' 左侧锯齿
					gCC.DrawLine(bThPen, rodX, ty2, rodX - sz * 0.045!, ty2 - threadStep * 0.3!)
					gCC.DrawLine(bThPen, rodX - sz * 0.045!, ty2 - threadStep * 0.3!, rodX, ty2 - threadStep * 0.6!)
					' 右侧锯齿
					gCC.DrawLine(bThPen, rodX + rodW, ty2, rodX + rodW + sz * 0.045!, ty2 - threadStep * 0.3!)
					gCC.DrawLine(bThPen, rodX + rodW + sz * 0.045!, ty2 - threadStep * 0.3!, rodX + rodW, ty2 - threadStep * 0.6!)
				Next
				' 螺杆高光
				gCC.DrawLine(bHiPen, rodX + 1, rodTop + 1, rodX + 1, rodBot - 1)
				gCC.Dispose()
				g_typeIcons(ChrW(26631) & ChrW(20934) & ChrW(20214)) = bmpCC
			End If

			' 钣金图标：L折弯，外拐角圆角，内拐角直角，端头直角
			Dim bmpSM As New System.Drawing.Bitmap(sz, sz)
			Dim gSM As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpSM)
			gSM.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
			gSM.Clear(System.Drawing.Color.Transparent)
			Dim smFill As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(103, 177, 230))
			Dim smPen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(50, 130, 190), 1)
			Dim smHi As New System.Drawing.Pen(System.Drawing.Color.FromArgb(180, 220, 245), 1)
			Dim t As Integer = Math.Max(2, szSM \ 6)
			Dim m As Integer = Math.Max(1, szSM \ 10)
			Dim rc As Integer = Math.Max(1, szSM \ 7)
			Dim ox As Integer = (sz - szSM) \ 2
			Dim oy As Integer = (sz - szSM) \ 2
			' 坐标
			Dim x1 As Integer = ox + m
			Dim x2 As Integer = ox + szSM - m - t
			Dim x3 As Integer = ox + szSM - m
			Dim y1 As Integer = oy + m
			Dim y2 As Integer = oy + m + t
			Dim y3 As Integer = oy + szSM - m
			' 单一路径画L形：
			' 从水平板左上角顺时针，外拐角（右上）加圆角，其余全直角
			Dim smPath As New System.Drawing.Drawing2D.GraphicsPath()
			smPath.AddLine(x1, y1, x2 - rc, y1)
			smPath.AddArc(x2 - rc, y1, rc * 2, rc * 2, 270, 90)
			smPath.AddLine(x3, y1 + rc, x3, y3)
			smPath.AddLine(x3, y3, x2, y3)
			smPath.AddLine(x2, y3, x2, y2)
			smPath.AddLine(x2, y2, x1, y2)
			smPath.AddLine(x1, y2, x1, y1)
			smPath.CloseFigure()
			gSM.FillPath(smFill, smPath)
			gSM.DrawPath(smPen, smPath)
			gSM.DrawLine(smHi, x1 + 1, y1 + 1, x2 - rc - 1, y1 + 1)
			gSM.Dispose()
			g_typeIcons(ChrW(38051) & ChrW(37329)) = bmpSM

			' 部件图标：提取32x32系统图标再高质量缩放
			Dim iamPath As String = ""
			For Each rr As BomDataRow In g_rows
				If rr.FilePath.ToLower().EndsWith(".iam") Then iamPath = rr.FilePath : Exit For
			Next
			If Not String.IsNullOrEmpty(iamPath) AndAlso System.IO.File.Exists(iamPath) Then
				Dim icoA As System.Drawing.Icon = System.Drawing.Icon.ExtractAssociatedIcon(iamPath)
				Dim raw32A As New System.Drawing.Bitmap(32, 32)
				Dim gRawA As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(raw32A)
				gRawA.DrawIcon(icoA, New System.Drawing.Rectangle(0, 0, 32, 32))
				gRawA.Dispose()
				Dim bmpAssy As New System.Drawing.Bitmap(sz, sz)
				Dim gAssy As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpAssy)
				gAssy.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
				gAssy.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				gAssy.DrawImage(raw32A, 0, 0, sz, sz)
				gAssy.Dispose() : raw32A.Dispose()
				g_typeIcons(ChrW(37096) & ChrW(20214)) = bmpAssy
			End If
			' ★ 焊接件图标：直角L形 + 拐角小三角（模拟焊缝），橙红色
			' 系统图标与普通部件相同无法区分，故手绘专属图标
			Dim bmpWeld As New System.Drawing.Bitmap(sz, sz)
			Dim gWeld As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpWeld)
			gWeld.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
			gWeld.Clear(System.Drawing.Color.Transparent)
			Dim wFill As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(195, 75, 15))
			Dim wPen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(130, 45, 5), 1)
			Dim wHi As New System.Drawing.Pen(System.Drawing.Color.FromArgb(240, 160, 100), 1)
			Dim wt As Integer = Math.Max(2, szSM \ 6)
			Dim wm As Integer = Math.Max(1, szSM \ 10)
			Dim wox As Integer = (sz - szSM) \ 2
			Dim woy As Integer = (sz - szSM) \ 2
			' L形坐标（与钣金完全相同，只是拐角改为直角）
			Dim wx1 As Integer = wox + wm
			Dim wx2 As Integer = wox + szSM - wm - wt
			Dim wx3 As Integer = wox + szSM - wm
			Dim wy1 As Integer = woy + wm
			Dim wy2 As Integer = woy + wm + wt
			Dim wy3 As Integer = woy + szSM - wm
			' 直角L形路径（所有拐角均为直角，无圆角）
			Dim wPath As New System.Drawing.Drawing2D.GraphicsPath()
			wPath.AddLine(wx1, wy1, wx3, wy1)
			wPath.AddLine(wx3, wy1, wx3, wy3)
			wPath.AddLine(wx3, wy3, wx2, wy3)
			wPath.AddLine(wx2, wy3, wx2, wy2)
			wPath.AddLine(wx2, wy2, wx1, wy2)
			wPath.AddLine(wx1, wy2, wx1, wy1)
			wPath.CloseFigure()
			gWeld.FillPath(wFill, wPath)
			gWeld.DrawPath(wPen, wPath)
			gWeld.DrawLine(wHi, wx1 + 1, wy1 + 1, wx3 - 1, wy1 + 1)
			' 焊缝小三角：在直角拐角处（wx2,wy2），向内画实心三角
			Dim triSz As Integer = Math.Max(5, szSM \ 3)
			Dim triFill As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(130, 45, 5))
			' 三角绕直角顶点(wx2,wy2)旋转180°：嵌入L形内凹角，两边贴内壁
			Dim triPts() As System.Drawing.PointF = {
			New System.Drawing.PointF(wx2, wy2),
			New System.Drawing.PointF(wx2 - triSz, wy2),
			New System.Drawing.PointF(wx2, wy2 + triSz)
			}
			gWeld.FillPolygon(triFill, triPts)
			gWeld.Dispose()
			g_typeIcons(ChrW(28938) & ChrW(25509)) = bmpWeld
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Function BomStructToStr(val As Integer) As String
		Select Case val
			Case 51970 : Return ChrW(26222) & ChrW(36890) & ChrW(20214)
			Case 51971 : Return ChrW(34394) & ChrW(25311) & ChrW(20214)
			Case 51972 : Return ChrW(21442) & ChrW(32771)
			Case 51973 : Return ChrW(22806) & ChrW(36141) & ChrW(20214)
			Case 51974 : Return ChrW(19981) & ChrW(21487) & ChrW(25286) & ChrW(20998) & ChrW(20214)
			Case Else : Return ChrW(26222) & ChrW(36890) & ChrW(20214)
		End Select
	End Function

	Function StrToBomStruct(s As String) As Integer
		Dim n As String = If(s Is Nothing, "", s.Trim())
		If n = ChrW(26222) & ChrW(36890) & ChrW(20214) Then Return 51970
		If n = ChrW(34394) & ChrW(25311) & ChrW(20214) Then Return 51971
		If n = ChrW(21442) & ChrW(32771) Then Return 51972
		If n = ChrW(22806) & ChrW(36141) & ChrW(20214) Then Return 51973
		If n = ChrW(19981) & ChrW(21487) & ChrW(25286) & ChrW(20998) & ChrW(20214) Then Return 51974
		Return 51970
	End Function

	Function GetDisplayPartNumber(r As BomDataRow) As String
		If g_chkMergePno IsNot Nothing AndAlso g_chkMergePno.Checked Then
			Dim pno As String = If(r.PartNumber Is Nothing, "", r.PartNumber.Trim())
			If String.IsNullOrEmpty(pno) Then
				Return System.IO.Path.GetFileNameWithoutExtension(r.FileName)
			End If
			Return pno
		Else
			Return If(r.PartNumber Is Nothing, "", r.PartNumber)
		End If
	End Function

	Function MergeStringField(displayPno As String,
		getVal As Func(Of BomDataRow, String)) As String
		Dim vals As New List(Of String)
		For Each r2 As BomDataRow In g_rows
			If GetDisplayPartNumber(r2) <> displayPno Then Continue For
			Dim v As String = getVal(r2)
			If v Is Nothing Then v = ""
			If Not vals.Contains(v) Then vals.Add(v)
		Next
		If vals.Count <= 1 Then Return If(vals.Count = 1, vals(0), "")
		Return MULTI_VAL
	End Function

	' ★ 根据当前行高等比计算缩略图列宽（图片原始比例 800:600 = 4:3）
	Function CalcThumbColWidth() As Integer
		Dim rowH As Integer = If(g_grid IsNot Nothing, g_grid.RowTemplate.Height, DEFAULT_ROW_H)
		' 等比缩放：宽 = 行高 × (THUMB_W / THUMB_H)，最小30，最大600
		Dim w As Integer = CInt(Math.Round(rowH * CDbl(L_THUMB_W) / CDbl(L_THUMB_H)))
		Return Math.Max(30, Math.Min(w, 600))
	End Function

	Sub SetStatus(msg As String, clr As System.Drawing.Color)
		If g_status Is Nothing Then Return
		g_status.Text = msg
		g_status.ForeColor = clr
	End Sub

	Sub RefreshGridRows()
		g_isRefreshing = True
		If g_grid IsNot Nothing Then RemoveHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		g_grid.SuspendLayout()
		' ★ 临时禁用当前排序列，防止 Rows.Add 时触发排序打乱顺序
		Dim _sortedColName As String = ""
		Dim _sortedColMode As System.Windows.Forms.DataGridViewColumnSortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
		Try
			If g_grid.SortedColumn IsNot Nothing Then
				_sortedColName = g_grid.SortedColumn.Name
				_sortedColMode = g_grid.SortedColumn.SortMode
				g_grid.SortedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
			End If
		Catch : End Try
		g_grid.Rows.Clear()
		Dim hideCC As Boolean = (g_chkSkipCC IsNot Nothing AndAlso g_chkSkipCC.Checked)
		Dim skipVirtual As Boolean = (g_chkSkipVirtual IsNot Nothing AndAlso g_chkSkipVirtual.Checked)
		Dim merging As Boolean = (g_chkMergePno IsNot Nothing AndAlso g_chkMergePno.Checked)
		Dim onlyPart As Boolean = (g_chkOnlyPart IsNot Nothing AndAlso g_chkOnlyPart.Checked)
		Dim onlyAssy As Boolean = (g_chkOnlyAssy IsNot Nothing AndAlso g_chkOnlyAssy.Checked)
		Dim onlySheet As Boolean = (g_chkOnlySheet IsNot Nothing AndAlso g_chkOnlySheet.Checked)
		Dim onlyWeld As Boolean = (g_chkOnlyWeld IsNot Nothing AndAlso g_chkOnlyWeld.Checked)
		Dim shownPno As New HashSet(Of String)
		Dim idx As Integer = 0

		For i As Integer = 0 To g_rows.Count - 1
			Dim r As BomDataRow = g_rows(i)
			If hideCC AndAlso (r.IsContentCenter OrElse r.IsStdIcon) Then Continue For
			If skipVirtual AndAlso (r.BomStruct = 51971 OrElse r.BomStruct = 51972) Then Continue For
			If onlyPart AndAlso r.DocType <> "零件" Then Continue For
			If onlyAssy AndAlso r.DocType <> "部件" Then Continue For
			If onlySheet AndAlso Not r.IsSheetMetal Then Continue For
			If onlyWeld AndAlso Not r.IsWeldment Then Continue For
			Dim displayPno As String = GetDisplayPartNumber(r)
			If merging Then
				If shownPno.Contains(displayPno) Then Continue For
				shownPno.Add(displayPno)
			End If
			idx += 1

			Dim dispFile As String = r.FileName
			Dim dispDno As String = r.DrawingNo
			Dim dispCat As String = r.Category
			Dim dispBom As String = BomStructToStr(r.BomStruct)
			Dim dispMat As String = r.Material
			Dim dispDesc As String = r.Description
			Dim dispQty As Integer = r.Qty
			Dim dispThumb As System.Drawing.Image = r.Thumbnail

			If merging Then
				dispQty = 0
				For Each r2 As BomDataRow In g_rows
					If GetDisplayPartNumber(r2) = displayPno Then dispQty += r2.Qty
				Next
				dispFile = MergeStringField(displayPno, Function(rx) rx.FileName)
				dispDno = MergeStringField(displayPno, Function(rx) rx.DrawingNo)
				dispCat = MergeStringField(displayPno, Function(rx) rx.Category)
				dispBom = MergeStringField(displayPno, Function(rx) BomStructToStr(rx.BomStruct))
				dispMat = MergeStringField(displayPno, Function(rx) rx.Material)
				dispDesc = MergeStringField(displayPno, Function(rx) rx.Description)
				Dim thumbKeys As New List(Of String)
				For Each r2 As BomDataRow In g_rows
					If GetDisplayPartNumber(r2) <> displayPno Then Continue For
					If Not thumbKeys.Contains(r2.FilePath) Then thumbKeys.Add(r2.FilePath)
				Next
				If thumbKeys.Count > 1 Then dispThumb = Nothing
			End If

			Dim rowIdx As Integer = g_grid.Rows.Add()
			g_grid.Rows(rowIdx).Tag = i
			Dim rowCells As DataGridViewCellCollection = g_grid.Rows(rowIdx).Cells
			If g_grid.Columns.Contains("cIdx") Then rowCells("cIdx").Value = r.FixedIdx
			If g_grid.Columns.Contains("cType") Then
				Dim typeVal As String = r.PartType
				If (r.IsStdIcon OrElse r.IsContentCenter) AndAlso
					g_typeIcons.ContainsKey(ChrW(26631) & ChrW(20934) & ChrW(20214)) Then
					typeVal = ChrW(26631) & ChrW(20934) & ChrW(20214)
				End If
				rowCells("cType").Value = typeVal
			End If
			If g_grid.Columns.Contains("cFile") Then rowCells("cFile").Value = dispFile
			If g_grid.Columns.Contains("cDno") Then rowCells("cDno").Value = dispDno
			If g_grid.Columns.Contains("cPno") Then rowCells("cPno").Value = displayPno
			If g_grid.Columns.Contains("cCat") Then rowCells("cCat").Value = dispCat
			If g_grid.Columns.Contains("cQty") Then rowCells("cQty").Value = dispQty
			If g_grid.Columns.Contains("cMat") Then
				Try : rowCells("cMat").Value = dispMat : Catch : End Try
			End If
			If g_grid.Columns.Contains("cDesc") Then rowCells("cDesc").Value = dispDesc
			If g_grid.Columns.Contains("cBom") Then
				Try : rowCells("cBom").Value = dispBom : Catch : End Try
			End If
			If g_grid.Columns.Contains("cDwg") Then rowCells("cDwg").Value = r.HasDrawing
			If g_grid.Columns.Contains("cMat") AndAlso TypeOf g_grid.Columns("cMat") Is DataGridViewComboBoxColumn Then
				Dim matCb As DataGridViewComboBoxColumn = CType(g_grid.Columns("cMat"), DataGridViewComboBoxColumn)
				Dim matVal As String = SafeStr(g_grid.Rows(rowIdx).Cells("cMat").Value)
				If Not String.IsNullOrEmpty(matVal) AndAlso Not matCb.Items.Contains(matVal) Then matCb.Items.Add(matVal)
			End If
			If g_grid.Columns.Contains("cBom") AndAlso TypeOf g_grid.Columns("cBom") Is DataGridViewComboBoxColumn Then
				Dim bomCb As DataGridViewComboBoxColumn = CType(g_grid.Columns("cBom"), DataGridViewComboBoxColumn)
				Dim bomVal As String = BomStructToStr(r.BomStruct)
				If Not String.IsNullOrEmpty(bomVal) AndAlso Not bomCb.Items.Contains(bomVal) Then bomCb.Items.Add(bomVal)
				Try : g_grid.Rows(rowIdx).Cells("cBom").Value = bomVal : Catch : End Try
			End If
			If dispThumb IsNot Nothing AndAlso g_grid.Columns.Contains("cThumb") Then
				g_grid.Rows(rowIdx).Cells("cThumb").Value = dispThumb
			End If
			If merging Then
				For Each cd As ColDef In g_colDefs
					If Not cd.IsCustom Then Continue For
					If Not g_grid.Columns.Contains(cd.ColName) Then Continue For
					Dim vals As New List(Of String)
					For Each r2 As BomDataRow In g_rows
						If GetDisplayPartNumber(r2) <> displayPno Then Continue For
						Dim v As String = ""
						If r2.Custom.ContainsKey(cd.PropName) Then v = r2.Custom(cd.PropName)
						If Not vals.Contains(v) Then vals.Add(v)
					Next
					g_grid.Rows(rowIdx).Cells(cd.ColName).Value = If(vals.Count <= 1, If(vals.Count = 1, vals(0), ""), MULTI_VAL)
				Next
			Else
				For Each cd As ColDef In g_colDefs
					If Not cd.IsCustom Then Continue For
					If Not g_grid.Columns.Contains(cd.ColName) Then Continue For
					Dim v As String = ""
					If r.Custom.ContainsKey(cd.PropName) Then v = r.Custom(cd.PropName)
					g_grid.Rows(rowIdx).Cells(cd.ColName).Value = v
				Next
			End If
			' 普通行文字色：自定义主题所有行显式统一为文字色（不依赖网格级继承，保证生效）
			If g_theme = 8 Then
				g_grid.Rows(rowIdx).DefaultCellStyle.ForeColor = g_custCellText
			End If
			If r.DocType = "部件" Then
				' 部件行：自定义已统一文字色，其它主题固定语义色（焊接红/装配绿）
				Dim rowSt As DataGridViewCellStyle = g_grid.Rows(rowIdx).DefaultCellStyle
				If g_theme <> 8 Then
					If r.IsWeldment Then
						rowSt.ForeColor = WeldRed()
					Else
						rowSt.ForeColor = AssyGreen()
					End If
				End If
				rowSt.Font = MakeFontBold(CStr(g_fontName), 9.0!)
			End If
			If r.IsContentCenter OrElse r.IsLocalStandard Then
				Dim roSt As DataGridViewCellStyle = g_grid.Rows(rowIdx).DefaultCellStyle
				If g_theme = 8 Then
					roSt.BackColor = g_custRowWhite
				Else
					roSt.BackColor = System.Drawing.Color.White
				End If
				If g_theme = 8 Then
					roSt.ForeColor = g_custCellText
				Else
					roSt.ForeColor = C(160, 160, 160)
				End If
				roSt.Font = MakeFont(CStr(g_fontName), 9.0!)
				For Each cell As DataGridViewCell In g_grid.Rows(rowIdx).Cells : cell.ReadOnly = True : Next
			End If
			If r.IsMissing Then
				Dim purpleBg As System.Drawing.Color = C(120, 60, 180)
				Dim whiteFg As System.Drawing.Color = C(255, 255, 255)
				Dim misFont As System.Drawing.Font = MakeFont(CStr(g_fontName), 9.0!)
				For Each cell As DataGridViewCell In g_grid.Rows(rowIdx).Cells
					cell.Style.BackColor = purpleBg : cell.Style.ForeColor = whiteFg
					cell.Style.Font = misFont
					cell.Style.SelectionBackColor = C(90, 40, 150) : cell.Style.SelectionForeColor = whiteFg
					cell.ReadOnly = True
				Next
				If g_grid.Columns.Contains("cFile") Then
					g_grid.Rows(rowIdx).Cells("cFile").Value = r.FileName & "  ⚠ 丢失文件"
				End If
			End If
		Next

		If g_status IsNot Nothing Then
			Dim total As Integer = g_rows.Count
			Dim ccCount As Integer = 0
			For Each r As BomDataRow In g_rows
				If r.IsContentCenter OrElse r.IsStdIcon Then ccCount += 1
			Next
			Dim shown As Integer = If(hideCC, total - ccCount, total)
			If merging Then shown = shownPno.Count
			Dim msg As String
			If hideCC AndAlso ccCount > 0 Then
				msg = "  " & shown.ToString() & " 个（隐藏 " & ccCount.ToString() & " 个标准件）"
			Else
				msg = "  共 " & shown.ToString() & " 个唯一零部件"
			End If
			If onlyPart Then msg = ChrW(32) & ChrW(32) & ChrW(20165) & ChrW(38646) & ChrW(20214) & ChrW(58) & shown.ToString() & ChrW(20491)
			If onlyAssy Then msg = ChrW(32) & ChrW(32) & ChrW(20165) & ChrW(37096) & ChrW(20214) & ChrW(58) & shown.ToString() & ChrW(20491)
			If onlySheet Then msg = ChrW(32) & ChrW(32) & ChrW(20165) & ChrW(38051) & ChrW(37329) & ChrW(20214) & ChrW(58) & shown.ToString() & ChrW(20491)
			If onlyWeld Then msg = "  仅焊接件:" & shown.ToString() & "个"
			SetStatus(msg, C(110, 55, 0))
		End If
		' ★ 恢复排序列的 SortMode
		Try
			If Not String.IsNullOrEmpty(_sortedColName) AndAlso g_grid.Columns.Contains(_sortedColName) Then
				g_grid.Columns(_sortedColName).SortMode = _sortedColMode
			End If
		Catch : End Try
		If g_grid IsNot Nothing Then AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		g_grid.ResumeLayout(False)
		g_isRefreshing = False
		If g_theme = 7 Then
			Try
				g_grid.BackgroundColor = System.Drawing.Color.FromArgb(110, 112, 120)
				For ri As Integer = 0 To g_grid.Rows.Count - 1
					Dim r As System.Windows.Forms.DataGridViewRow = g_grid.Rows(ri)
					If ri Mod 2 = 0 Then
						r.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(145, 147, 155)
					Else
						r.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(155, 157, 165)
					End If
					r.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 240)
					r.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(60, 75, 110)
					r.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
				Next
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
		End If
	End Sub

	Sub OnTypeCellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
		If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Return
		' ── 缩略图列：填满单元格，无任何白边 ──
		If g_grid.Columns.Contains("cThumb") AndAlso g_grid.Columns("cThumb").Index = e.ColumnIndex Then
			e.PaintBackground(e.ClipBounds, True)
			Dim img As System.Drawing.Image = Nothing
			Try : img = CType(e.Value, System.Drawing.Image) : Catch : End Try
			If img IsNot Nothing Then
				Dim pad As Integer = 1
				Dim cw As Integer = e.CellBounds.Width - pad * 2
				Dim ch As Integer = e.CellBounds.Height - pad * 2
				Dim sc As Double = Math.Max(CDbl(cw) / img.Width, CDbl(ch) / img.Height)
				Dim dw As Integer = CInt(img.Width * sc)
				Dim dh As Integer = CInt(img.Height * sc)
				Dim dx As Integer = e.CellBounds.X + pad + (cw - dw) \ 2 - 1
				Dim dy As Integer = e.CellBounds.Y + pad + (ch - dh) \ 2
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
				Dim _cellRc As System.Drawing.Rectangle = e.CellBounds
				_cellRc.Inflate(-2, -2)
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
				e.Graphics.DrawImage(img, _cellRc)
			End If
			e.Handled = True
			Return
		End If
		If g_grid.Columns(e.ColumnIndex).Name <> "cType" Then Return
		Dim cellVal As String = SafeStr(e.Value)
		e.PaintBackground(e.ClipBounds, True)
		Dim iconSz As Integer = Math.Max(16, Math.Min(e.CellBounds.Height - 4, 48))
		Dim iconX As Integer = e.CellBounds.X + 2
		Dim iconY As Integer = e.CellBounds.Y + (e.CellBounds.Height - iconSz) \ 2
		If g_typeIcons.ContainsKey(cellVal) Then
			' 零件图标向右偏移3px，视觉上更居中
			Dim drawX As Integer = iconX
			If cellVal = ChrW(38646) & ChrW(20214) Then drawX = iconX + Scaled(3)
			e.Graphics.DrawImage(g_typeIcons(cellVal), drawX, iconY, iconSz, iconSz)
		End If
		Dim txtX As Integer = iconX + iconSz + 3
		Dim txtW As Integer = e.CellBounds.Right - txtX - 2
		Dim txtRect As New System.Drawing.RectangleF(txtX, e.CellBounds.Y, txtW, e.CellBounds.Height)
		Dim sf As New System.Drawing.StringFormat()
		sf.Alignment = System.Drawing.StringAlignment.Near
		sf.LineAlignment = System.Drawing.StringAlignment.Center
		sf.Trimming = System.Drawing.StringTrimming.EllipsisCharacter
		Dim txtClr As System.Drawing.Color = If(e.State And DataGridViewElementStates.Selected, e.CellStyle.SelectionForeColor, e.CellStyle.ForeColor)
		e.Graphics.DrawString(cellVal, e.CellStyle.Font, New System.Drawing.SolidBrush(txtClr), txtRect, sf)
		e.Handled = True
	End Sub

	Sub OnSkipCCChanged(sender As Object, e As EventArgs)
		RefreshGridRows()
	End Sub

	Sub OnSkipVirtualChanged(sender As Object, e As EventArgs)
		RefreshGridRows()
		SaveSettings()
	End Sub

	Sub OnMergePnoChanged(sender As Object, e As EventArgs)
		RefreshGridRows()
		If g_chkMergePno.Checked Then
			SetStatus("  合并已启用（不同属性显示 *多种）", C(0, 100, 60))
		Else
			SetStatus("  合并已关闭", C(110, 55, 0))
		End If
	End Sub

	Sub OnFilterModeChanged(sender As Object, e As EventArgs)
		Dim chk As CheckBox = CType(sender, CheckBox)
		If chk.Checked Then
			If chk Is g_chkOnlyPart Then
				If g_chkOnlyAssy IsNot Nothing Then g_chkOnlyAssy.Checked = False
				If g_chkOnlySheet IsNot Nothing Then g_chkOnlySheet.Checked = False
				If g_chkOnlyWeld IsNot Nothing Then g_chkOnlyWeld.Checked = False
			ElseIf chk Is g_chkOnlyAssy Then
				If g_chkOnlyPart IsNot Nothing Then g_chkOnlyPart.Checked = False
				If g_chkOnlySheet IsNot Nothing Then g_chkOnlySheet.Checked = False
				If g_chkOnlyWeld IsNot Nothing Then g_chkOnlyWeld.Checked = False
			ElseIf chk Is g_chkOnlySheet Then
				If g_chkOnlyPart IsNot Nothing Then g_chkOnlyPart.Checked = False
				If g_chkOnlyAssy IsNot Nothing Then g_chkOnlyAssy.Checked = False
				If g_chkOnlyWeld IsNot Nothing Then g_chkOnlyWeld.Checked = False
			ElseIf chk Is g_chkOnlyWeld Then
				If g_chkOnlyPart IsNot Nothing Then g_chkOnlyPart.Checked = False
				If g_chkOnlyAssy IsNot Nothing Then g_chkOnlyAssy.Checked = False
				If g_chkOnlySheet IsNot Nothing Then g_chkOnlySheet.Checked = False
			End If
		End If
		RefreshGridRows()
	End Sub

	Function GetOrigRowIdx(gridRowIdx As Integer) As Integer
		If gridRowIdx < 0 OrElse gridRowIdx >= g_grid.Rows.Count Then Return -1
		Dim tag As Object = g_grid.Rows(gridRowIdx).Tag
		If tag Is Nothing Then Return -1
		Return CInt(tag)
	End Function

	Function GetSettingsPath() As String
		' 存到每个用户自己的 AppData\Roaming\DWJ_BOM 目录
		' 跨机器通用：每台机器每个用户都有独立的设置文件
		Try
			Dim appData As String = System.Environment.GetFolderPath(
			System.Environment.SpecialFolder.ApplicationData)
			Dim iniDir As String = System.IO.Path.Combine(appData, "DWJ_BOM")
			If Not System.IO.Directory.Exists(iniDir) Then
				System.IO.Directory.CreateDirectory(iniDir)
			End If
			Return System.IO.Path.Combine(iniDir, "DWJ_BOM_Settings.ini")
		Catch : End Try
		' 备用：用户文档目录
		Return System.IO.Path.Combine(
		System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
		"DWJ_BOM_Settings.ini")
	End Function

	Function ReadSection(filePath As String, section As String) As Dictionary(Of String, String)
		Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
		If Not System.IO.File.Exists(filePath) Then Return result
		Try
			Dim inSection As Boolean = False
			For Each line As String In System.IO.File.ReadAllLines(filePath, System.Text.Encoding.UTF8)
				Dim trimmed As String = line.Trim()
				If trimmed.StartsWith("[") AndAlso trimmed.EndsWith("]") Then
					inSection = (trimmed.ToLower() = "[" & section.ToLower() & "]")
					Continue For
				End If
				If inSection AndAlso trimmed.Contains("=") Then
					Dim eqIdx As Integer = trimmed.IndexOf("="c)
					Dim key As String = trimmed.Substring(0, eqIdx).Trim()
					Dim val As String = trimmed.Substring(eqIdx + 1).Trim()
					If Not String.IsNullOrEmpty(key) Then result(key) = val
				End If
			Next
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
		Return result
	End Function

	Function SaveThemeIni() As Boolean
		Try
			If String.IsNullOrEmpty(g_settingsFile) Then g_settingsFile = GetSettingsPath()
			Dim content As String = ""
			If System.IO.File.Exists(g_settingsFile) Then
				content = System.IO.File.ReadAllText(g_settingsFile, System.Text.Encoding.UTF8)
			End If
			If Not content.Contains("[Options]") Then
				content &= vbCrLf & "[Options]" & vbCrLf
			End If
			Dim keys() As String = {"Theme", "CustTitleBar", "CustColHeader", "CustColMgr", "CustFuncDrop", "CustWinBg", "CustExcelBtn", "CustRefresh", "CustRowAlt", "CustRowWhite", "CustCellText"}
			For Each k As String In keys
				Dim val As String = ""
				Select Case k
					Case "Theme" : val = g_theme.ToString()
					Case "CustTitleBar" : val = ColorToStr(g_custTitleBar)
					Case "CustColHeader" : val = ColorToStr(g_custColHeader)
					Case "CustColMgr" : val = ColorToStr(g_custColMgr)
					Case "CustFuncDrop" : val = ColorToStr(g_custFuncDrop)
					Case "CustWinBg" : val = ColorToStr(g_custWinBg)
					Case "CustExcelBtn" : val = ColorToStr(g_custExcelBtn)
					Case "CustRefresh" : val = ColorToStr(g_custRefresh)
					Case "CustRowAlt" : val = ColorToStr(g_custRowAlt)
					Case "CustRowWhite" : val = ColorToStr(g_custRowWhite)
					Case "CustCellText" : val = ColorToStr(g_custCellText)
				End Select
				If System.Text.RegularExpressions.Regex.IsMatch(content, "(?m)^" & k & "=") Then
					content = System.Text.RegularExpressions.Regex.Replace(content, "(?m)^" & k & "=.*$", k & "=" & val)
				Else
					Dim optIdx As Integer = content.IndexOf("[Options]")
					Dim nextSec As Integer = content.IndexOf(vbCrLf & "[", optIdx + 1)
					If nextSec < 0 Then nextSec = content.Length
					content = content.Substring(0, nextSec) & k & "=" & val & vbCrLf & content.Substring(nextSec)
				End If
			Next
			System.IO.File.WriteAllText(g_settingsFile, content, System.Text.Encoding.UTF8)
			Return True
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return False
		End Try
	End Function

	Sub SaveSettings()
		' 确保路径有效
		If String.IsNullOrEmpty(g_settingsFile) Then g_settingsFile = GetSettingsPath()
		Try
			Dim sb As New System.Text.StringBuilder()
			sb.AppendLine("[CustomCols]")
			Dim customNames As New List(Of String)
			For Each cd As ColDef In g_colDefs
				If cd.IsCustom Then customNames.Add(cd.Header & "|" & cd.PropName)
			Next
			sb.AppendLine("cols=" & String.Join(",", customNames.ToArray()))
			' 保存内置可选列状态（存在=true，已删除=false）
			sb.AppendLine("[BuiltinCols]")
			Dim builtinOptional() As String = {"cFile", "cDno", "cPno", "cCat", "cQty", "cMat", "cDesc", "cBom", "cDwg", "cThumb"}
			For Each bcn As String In builtinOptional
				Dim exists As Boolean = g_grid IsNot Nothing AndAlso g_grid.Columns.Contains(bcn)
				sb.AppendLine(bcn & "=" & exists.ToString().ToLower())
			Next
			sb.AppendLine("[ColOrder]")
			For Each cd As ColDef In g_colDefs
				If g_grid IsNot Nothing AndAlso g_grid.Columns.Contains(cd.ColName) Then
					Dim col As DataGridViewColumn = g_grid.Columns(cd.ColName)
					sb.AppendLine(cd.ColName & "=" & col.DisplayIndex.ToString() & "," & col.Width.ToString())
				End If
			Next
			sb.AppendLine("[Options]")
			Dim hideCC As Boolean = (g_chkSkipCC IsNot Nothing AndAlso g_chkSkipCC.Checked)
			Dim onlyPart As Boolean = (g_chkOnlyPart IsNot Nothing AndAlso g_chkOnlyPart.Checked)
			Dim onlyAssy As Boolean = (g_chkOnlyAssy IsNot Nothing AndAlso g_chkOnlyAssy.Checked)
			Dim onlySheet As Boolean = (g_chkOnlySheet IsNot Nothing AndAlso g_chkOnlySheet.Checked)
			Dim onlyWeld2 As Boolean = (g_chkOnlyWeld IsNot Nothing AndAlso g_chkOnlyWeld.Checked)
			Dim skipVirt As Boolean = (g_chkSkipVirtual IsNot Nothing AndAlso g_chkSkipVirtual.Checked)
			sb.AppendLine("HideContentCenter=" & hideCC.ToString().ToLower())
			sb.AppendLine("SkipVirtual=" & skipVirt.ToString().ToLower())
			sb.AppendLine("OnlyPart=" & onlyPart.ToString().ToLower())
			sb.AppendLine("OnlyAssy=" & onlyAssy.ToString().ToLower())
			sb.AppendLine("OnlySheet=" & onlySheet.ToString().ToLower())
			sb.AppendLine("OnlyWeld=" & onlyWeld2.ToString().ToLower())
			Dim savedRowH As Integer = g_grid.RowTemplate.Height
			Dim savedColW As String = If(g_txtColW IsNot Nothing, g_txtColW.Text.Trim(), "100")
			sb.AppendLine("RowHeight=" & savedRowH.ToString())
			sb.AppendLine("ColWidth=" & savedColW)
			sb.AppendLine("Theme=" & g_theme.ToString())
			sb.AppendLine("CustTitleBar=" & ColorToStr(g_custTitleBar))
			sb.AppendLine("CustColHeader=" & ColorToStr(g_custColHeader))
			sb.AppendLine("CustColMgr=" & ColorToStr(g_custColMgr))
			sb.AppendLine("CustFuncDrop=" & ColorToStr(g_custFuncDrop))
			sb.AppendLine("CustWinBg=" & ColorToStr(g_custWinBg))
			sb.AppendLine("CustExcelBtn=" & ColorToStr(g_custExcelBtn))
			sb.AppendLine("CustRefresh=" & ColorToStr(g_custRefresh))
			sb.AppendLine("CustRowAlt=" & ColorToStr(g_custRowAlt))
			sb.AppendLine("CustRowWhite=" & ColorToStr(g_custRowWhite))
			sb.AppendLine("CustCellText=" & ColorToStr(g_custCellText))
			' 保留已有的 [PropConcat] 节'
			Try
				If System.IO.File.Exists(g_settingsFile) Then
					Dim existContent As String = System.IO.File.ReadAllText(g_settingsFile, System.Text.Encoding.UTF8)
					Dim pcIdx As Integer = existContent.IndexOf("[PropConcat]", StringComparison.OrdinalIgnoreCase)
					If pcIdx >= 0 Then
						Dim pcEnd As Integer = existContent.IndexOf(vbCrLf & "[", pcIdx + 1)
						Dim pcSection As String = If(pcEnd >= 0, existContent.Substring(pcIdx, pcEnd - pcIdx), existContent.Substring(pcIdx))
						sb.AppendLine("")
						sb.Append(pcSection.Trim())
						sb.AppendLine("")
					End If
				End If
			Catch : End Try
			System.IO.File.WriteAllText(g_settingsFile, sb.ToString(), System.Text.Encoding.UTF8)
		Catch ex As Exception

		End Try
	End Sub

	Sub LoadSettings()
		If Not System.IO.File.Exists(g_settingsFile) Then Return
		g_isLoading = True
		Try
			Dim customSec As Dictionary(Of String, String) = ReadSection(g_settingsFile, "CustomCols")
			Dim colsVal As String = ""
			If customSec.ContainsKey("cols") Then colsVal = customSec("cols")
			If Not String.IsNullOrEmpty(colsVal) Then
				For Each propName As String In colsVal.Split(","c)
					Dim entry As String = propName.Trim()
					If String.IsNullOrEmpty(entry) Then Continue For
					Dim pn As String = entry
					Dim pnProp As String = entry
					Dim pipeIdx As Integer = entry.IndexOf("|")
					If pipeIdx >= 0 Then
						pn = entry.Substring(0, pipeIdx).Trim()
						pnProp = entry.Substring(pipeIdx + 1).Trim()
					End If
					If String.IsNullOrEmpty(pnProp) Then Continue For
					Dim already As Boolean = False
					For Each cd As ColDef In g_colDefs
						If cd.IsCustom AndAlso cd.PropName = pnProp Then already = True : Exit For
					Next
					If already Then Continue For
					Dim isLvNo As Boolean = (pnProp = "级别序号" OrElse pnProp = "LevelNo")
					For Each bomRow As BomDataRow In g_rows
						If isLvNo Then
							bomRow.Custom(pnProp) = If(String.IsNullOrEmpty(bomRow.LevelNo), "", bomRow.LevelNo)
						Else
							bomRow.Custom(pnProp) = ReadAnyProp(bomRow.FilePath, pnProp)
						End If
					Next
					g_customIdx += 1
					Dim newColName As String = "cCustom" & g_customIdx
					Dim newCd As New ColDef()
					newCd.ColName = newColName : newCd.Header = pn
					newCd.IsReadOnly = isLvNo : newCd.IsFixed = False
					newCd.IsCustom = True : newCd.IsImage = False
					newCd.PropName = pnProp : newCd.InitWidth = Scaled(120)
					newCd.CenterAlign = False
					g_colDefs.Add(newCd)
					Dim grayBg As System.Drawing.Color = System.Drawing.Color.White
					BuildGridColumn(newCd, grayBg)
					If g_grid.Columns.Contains("cThumb") Then
						If g_grid.Columns.Contains("cThumb") Then g_grid.Columns("cThumb").DisplayIndex = g_grid.Columns.Count - 1
					End If
					AddCheckRow(newCd)
				Next
			End If
			' 恢复内置可选列状态
			Dim builtinSec As Dictionary(Of String, String) = ReadSection(g_settingsFile, "BuiltinCols")
			Dim builtinOptCols() As String = {"cFile", "cDno", "cPno", "cCat", "cQty", "cMat", "cDesc", "cBom", "cDwg", "cThumb"}
			For Each bcn As String In builtinOptCols
				If builtinSec.ContainsKey(bcn) Then
					Dim shouldExist As Boolean = (builtinSec(bcn).ToLower() = "true")
					Dim doesExist As Boolean = g_grid.Columns.Contains(bcn)
					If shouldExist AndAlso Not doesExist Then
						RestoreBuiltinCol(bcn)
					ElseIf Not shouldExist AndAlso doesExist Then
						' 删除不应存在的列
						If g_grid IsNot Nothing Then RemoveHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
						If g_grid.Columns.Contains(bcn) Then g_grid.Columns.Remove(bcn)
						If g_grid IsNot Nothing Then AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
						For Each cd2 As ColDef In g_colDefs
							If cd2.ColName = bcn Then g_colDefs.Remove(cd2) : Exit For
						Next
						For Each ctrl2 As Control In g_chkFlow.Controls
							If ctrl2.Tag IsNot Nothing AndAlso CStr(ctrl2.Tag) = bcn Then
								g_chkFlow.Controls.Remove(ctrl2) : Exit For
							End If
						Next
					End If
				End If
			Next
			' ColVisible节已废弃（改为有列=显示，无列=不显示）
			Dim orderSec As Dictionary(Of String, String) = ReadSection(g_settingsFile, "ColOrder")
			If orderSec.Count > 0 Then
				Dim orderList As New List(Of KeyValuePair(Of String, Integer))
				For Each kv As KeyValuePair(Of String, String) In orderSec
					Dim di As Integer
					' 格式：displayIdx 或 displayIdx,width，只取第一段解析 displayIdx
					Dim diStr As String = kv.Value
					Dim commaIdx As Integer = diStr.IndexOf(",".ToCharArray()(0))
					If commaIdx >= 0 Then diStr = diStr.Substring(0, commaIdx).Trim()
					If g_grid.Columns.Contains(kv.Key) AndAlso Integer.TryParse(diStr, di) Then
						orderList.Add(New KeyValuePair(Of String, Integer)(kv.Key, di))
					End If
				Next
				orderList.Sort(Function(a, b) a.Value.CompareTo(b.Value))
				' 恢复 DisplayIndex 和列宽
				For Each kv As KeyValuePair(Of String, Integer) In orderList
					Try
						If g_grid.Columns(kv.Key).Frozen Then Continue For
						Dim frozenCnt As Integer = 0
						For Each fc As DataGridViewColumn In g_grid.Columns
							If fc.Frozen Then frozenCnt += 1
						Next
						Dim safeIdx As Integer = Math.Max(frozenCnt, Math.Min(kv.Value, g_grid.Columns.Count - 1))
						g_grid.Columns(kv.Key).DisplayIndex = safeIdx
						' 恢复列宽（格式 displayIdx,width）
						If orderSec.ContainsKey(kv.Key) Then
							Dim parts() As String = orderSec(kv.Key).Split(",".ToCharArray())
							If parts.Length >= 2 Then
								Dim savedW As Integer
								If Integer.TryParse(parts(1).Trim(), savedW) AndAlso savedW >= 10 AndAlso savedW <= 3000 Then
									g_grid.Columns(kv.Key).Width = savedW
								End If
							End If
						End If
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				Next
				' 同步右边列管理面板顺序
				SyncChkFlowOrder()
			End If
			Dim optSec As Dictionary(Of String, String) = ReadSection(g_settingsFile, "Options")
			If g_chkSkipCC IsNot Nothing Then
				If optSec.ContainsKey("HideContentCenter") Then
					g_chkSkipCC.Checked = (optSec("HideContentCenter").ToLower() = "true")
				ElseIf optSec.ContainsKey("SkipContentCenter") Then
					g_chkSkipCC.Checked = (optSec("SkipContentCenter").ToLower() = "true")
				End If
			End If
			If g_chkSkipVirtual IsNot Nothing Then
				If optSec.ContainsKey("SkipVirtual") Then
					g_chkSkipVirtual.Checked = (optSec("SkipVirtual").ToLower() = "true")
				End If
			End If
			If g_chkOnlyPart IsNot Nothing Then
				If optSec.ContainsKey("OnlyPart") Then
					g_chkOnlyPart.Checked = (optSec("OnlyPart").ToLower() = "true")
				End If
			End If
			If g_chkOnlyAssy IsNot Nothing Then
				If optSec.ContainsKey("OnlyAssy") Then
					g_chkOnlyAssy.Checked = (optSec("OnlyAssy").ToLower() = "true")
				End If
			End If
			If g_chkOnlySheet IsNot Nothing Then
				If optSec.ContainsKey("OnlySheet") Then
					g_chkOnlySheet.Checked = (optSec("OnlySheet").ToLower() = "true")
				End If
			End If
			If g_chkOnlyWeld IsNot Nothing Then
				If optSec.ContainsKey("OnlyWeld") Then
					g_chkOnlyWeld.Checked = (optSec("OnlyWeld").ToLower() = "true")
				End If
			End If
			If optSec.ContainsKey("RowHeight") Then
				Dim rh As Integer
				If Integer.TryParse(optSec("RowHeight"), rh) AndAlso rh >= 10 AndAlso rh <= 500 Then
					g_grid.RowTemplate.Height = rh
					For Each dgvRow As DataGridViewRow In g_grid.Rows
						dgvRow.Height = rh
					Next
					If g_txtRowH IsNot Nothing Then g_txtRowH.Text = rh.ToString()
				End If
			End If
			If optSec.ContainsKey("ColWidth") Then
				Dim cw As Integer
				If Integer.TryParse(optSec("ColWidth"), cw) AndAlso cw >= 10 AndAlso cw <= 2000 Then
					If g_txtColW IsNot Nothing Then g_txtColW.Text = cw.ToString()
				End If
			End If
			If optSec.ContainsKey("Theme") Then
				Dim th As Integer
				If Integer.TryParse(optSec("Theme"), th) AndAlso th >= 0 AndAlso th <= 8 Then
					g_theme = th
					If g_cmbTheme IsNot Nothing AndAlso Not g_cmbTheme.IsDisposed AndAlso g_cmbTheme.SelectedIndex <> g_theme Then g_cmbTheme.SelectedIndex = g_theme
				End If
			End If
			' 自定义主题颜色
			g_custTitleBar = StrToColor(optSec, "CustTitleBar", System.Drawing.Color.FromArgb(108, 92, 231))
			g_custColHeader = StrToColor(optSec, "CustColHeader", System.Drawing.Color.FromArgb(108, 92, 231))
			g_custColMgr = StrToColor(optSec, "CustColMgr", System.Drawing.Color.FromArgb(243, 240, 255))
			g_custFuncDrop = StrToColor(optSec, "CustFuncDrop", System.Drawing.Color.FromArgb(228, 222, 252))
			g_custWinBg = StrToColor(optSec, "CustWinBg", System.Drawing.Color.FromArgb(245, 243, 255))
			g_custExcelBtn = StrToColor(optSec, "CustExcelBtn", System.Drawing.Color.FromArgb(159, 149, 239))
			g_custRefresh = StrToColor(optSec, "CustRefresh", System.Drawing.Color.FromArgb(225, 112, 85))
			g_custRowAlt = StrToColor(optSec, "CustRowAlt", System.Drawing.Color.FromArgb(253, 252, 255))
			g_custRowWhite = StrToColor(optSec, "CustRowWhite", System.Drawing.Color.FromArgb(255, 255, 255))
			g_custCellText = StrToColor(optSec, "CustCellText", System.Drawing.Color.FromArgb(0, 0, 0))
			RefreshGridRows()
		Catch ex As Exception

		Finally
			g_isLoading = False
			g_isDirty = False
		End Try
	End Sub

	' ★ 关闭窗口时的处理：有修改则询问是否写入，无修改直接退出
	Sub OnFormClosing(sender As Object, e As FormClosingEventArgs)
		' 保存列设置
		SaveSettings()
		CloseThumbPopup()
		CleanThumbnailTemp()

		' 清空待保存队列（属性已写入内存，Inventor 关闭时会提示用户保存）
		g_pendingSaveDocs.Clear()
		g_isDirty = False
	End Sub

	Function RenameFileWithDrawing(oldPath As String, newBaseName As String,
		ByRef errMsg As String) As Boolean
		errMsg = ""
		Try
			Dim dir As String = System.IO.Path.GetDirectoryName(oldPath)
			Dim oldBase As String = System.IO.Path.GetFileNameWithoutExtension(oldPath)
			Dim ext As String = System.IO.Path.GetExtension(oldPath)
			Dim cleanNew As String = newBaseName.Trim()
			If String.IsNullOrEmpty(cleanNew) Then
				errMsg = "新文件名为空" : Return False
			End If
			If cleanNew = oldBase Then Return True
			Dim newPath As String = System.IO.Path.Combine(dir, cleanNew & ext)
			If System.IO.File.Exists(newPath) Then
				errMsg = "目标文件已存在：" & cleanNew & ext : Return False
			End If
			Dim oDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If String.Compare(od.FullDocumentName, oldPath, True) = 0 Then
					oDoc = od : Exit For
				End If
			Next
			If oDoc Is Nothing Then oDoc = ThisApplication.Documents.Open(oldPath, True)
			oDoc.SaveAs(newPath, False)

			For Each od As Document In ThisApplication.Documents
				If od.DocumentType <> kAssemblyDocumentObject Then Continue For
				Try
					Dim asmDef As AssemblyComponentDefinition = CType(od, AssemblyDocument).ComponentDefinition
					Dim stk As New Stack(Of ComponentOccurrence)
					For Each topOcc As ComponentOccurrence In asmDef.Occurrences
						stk.Push(topOcc)
					Next
					Do While stk.Count > 0
						Dim oOcc As ComponentOccurrence = stk.Pop()
						Try
							If oOcc.Suppressed Then Continue Do
							Dim defDoc As Document = oOcc.Definition.Document
							If String.Compare(defDoc.FullDocumentName, oldPath, True) = 0 OrElse
								String.Compare(defDoc.FullDocumentName, newPath, True) = 0 Then
								oOcc.Replace(newPath, True)
							End If
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						End Try
						If oOcc.DefinitionDocumentType = kAssemblyDocumentObject Then
							Try
								For Each subOcc As ComponentOccurrence In oOcc.SubOccurrences
									stk.Push(subOcc)
								Next
							Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
							End Try
						End If
					Loop
					If od.Dirty Then od.Save()
				Catch ex As Exception

				End Try
			Next
			Try
				System.IO.File.Delete(oldPath)
			Catch ex As Exception

			End Try
			' 搜索工程图：先同目录，再项目工作空间递归搜索
			Dim dwgSearchDirs As New List(Of String)
			dwgSearchDirs.Add(dir)
			Try
				Dim proj As DesignProject = ThisApplication.DesignProjectManager.ActiveDesignProject
				Dim wsPath As String = proj.WorkspacePath
				If Not String.IsNullOrEmpty(wsPath) AndAlso System.IO.Directory.Exists(wsPath) Then
					If String.Compare(wsPath, dir, True) <> 0 Then dwgSearchDirs.Add(wsPath)
				End If
			Catch : End Try
			Dim dwgExts() As String = {".idw", ".dwg"}
			For Each dwgExt As String In dwgExts
				Dim oldDwg As String = ""
				' 先在同目录找
				Dim sameDirDwg As String = System.IO.Path.Combine(dir, oldBase & dwgExt)
				If System.IO.File.Exists(sameDirDwg) Then
					oldDwg = sameDirDwg
				Else
					' 在项目工作空间递归搜索
					For Each sd As String In dwgSearchDirs
						If String.Compare(sd, dir, True) = 0 Then Continue For
						Try
							Dim found() As String = System.IO.Directory.GetFiles(sd, oldBase & dwgExt, System.IO.SearchOption.AllDirectories)
							If found.Length > 0 Then oldDwg = found(0) : Exit For
						Catch : End Try
					Next
				End If
				If String.IsNullOrEmpty(oldDwg) Then Continue For
				Dim newDwgDir As String = System.IO.Path.GetDirectoryName(oldDwg)
				Dim newDwg As String = System.IO.Path.Combine(newDwgDir, cleanNew & dwgExt)
				If System.IO.File.Exists(newDwg) Then Continue For
				Try
					System.IO.File.Copy(oldDwg, newDwg)
					Dim oDrawDoc As DrawingDocument = CType(ThisApplication.Documents.Open(newDwg, True), DrawingDocument)
					Try
						Dim oFD As FileDescriptor = oDrawDoc.File.ReferencedFileDescriptors(1)
						oFD.ReplaceReference(newPath)
					Catch ex As Exception

					End Try
					oDrawDoc.Save()
					oDrawDoc.Close(False)
					System.IO.File.Delete(oldDwg)

				Catch ex As Exception

				End Try
			Next
			Return True
		Catch ex As Exception
			errMsg = ex.Message : Return False
		End Try
	End Function

	'     ── Excel编辑：直接读写xlsx，完全不启动Excel进程，速度极快 ──
	'     ★ 新版：ZipArchive直写xlsx，Process.Start打开，无需Excel进程
	'     ★ XML属性名用 Xq() 辅助函数在运行时替换，避免iLogic单位扫描误报
	Function Xq(s As String) As String
		Return s.Replace("~", ChrW(34))
	End Function

	Sub OnExportBomExcel(sender As Object, E As EventArgs)
		' ── 弹窗选择保存路径 ──
		Dim asmName As String = System.IO.Path.GetFileNameWithoutExtension(g_asmDoc.FullDocumentName)
		Dim sfd As New System.Windows.Forms.SaveFileDialog()
		sfd.Filter = "Excel 文件|*.xlsx"
		sfd.FileName = asmName & "_BOM.xlsx"
		sfd.InitialDirectory = System.IO.Path.GetDirectoryName(g_asmDoc.FullDocumentName)
		If sfd.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then Return
		Dim savePath As String = sfd.FileName
		SetStatus("  正在导出 Excel...", C(0, 100, 180))
		g_frm.Refresh()
		Try
			' ── 1. 构建列定义（按窗口表格显示顺序，列宽对应）──
			Dim colNames As New List(Of String)()
			Dim colHdrs As New List(Of String)()
			colNames.Add("__fp__") : colHdrs.Add("完整路径（勿修改）")
			Dim sortedCols As New List(Of ColDef)(g_colDefs)
			sortedCols.Sort(Function(a, b)
								Dim da As Integer = If(g_grid.Columns.Contains(a.ColName), g_grid.Columns(a.ColName).DisplayIndex, 999)
								Dim db As Integer = If(g_grid.Columns.Contains(b.ColName), g_grid.Columns(b.ColName).DisplayIndex, 999)
								Return da.CompareTo(db)
							End Function)
			Dim colWidths As New Dictionary(Of Integer, Double)()
			colWidths(0) = 50  ' 隐藏的路径列
			Dim _ci As Integer = 1
			For Each cd As ColDef In sortedCols
				If Not g_grid.Columns.Contains(cd.ColName) Then Continue For
				If cd.IsImage Then
					colNames.Add("__thumb__") : colHdrs.Add(cd.Header)
					colWidths(_ci) = Math.Round(g_grid.Columns(cd.ColName).Width / 7.0, 2)
					_ci += 1 : Continue For
				End If
				Dim hdr As String = cd.Header
				If hdr.EndsWith(" " & ChrW(9998)) Then hdr = hdr.Substring(0, hdr.Length - 2).Trim()
				colWidths(_ci) = Math.Round(g_grid.Columns(cd.ColName).Width / 7.0, 2)
				Select Case cd.ColName
					Case "cIdx" : colNames.Add("__idx__") : colHdrs.Add(hdr)
					Case "cType" : colNames.Add("__type__") : colHdrs.Add(hdr)
					Case "cFile" : colNames.Add("FileName") : colHdrs.Add(hdr)
					Case "cDno" : colNames.Add("DrawingNo") : colHdrs.Add(hdr)
					Case "cPno" : colNames.Add("PartNumber") : colHdrs.Add(hdr)
					Case "cCat" : colNames.Add("Category") : colHdrs.Add(hdr)
					Case "cQty" : colNames.Add("__qty__") : colHdrs.Add(hdr)
					Case "cMat" : colNames.Add("Material") : colHdrs.Add(hdr)
					Case "cDesc" : colNames.Add("Description") : colHdrs.Add(hdr)
					Case "cBom" : colNames.Add("__bom__") : colHdrs.Add(hdr)
					Case "cDwg" : colNames.Add("__dwg__") : colHdrs.Add(hdr)
					Case Else
						If cd.IsCustom Then colNames.Add("__custom__" & cd.PropName) : colHdrs.Add(hdr)
				End Select
				_ci += 1
			Next
			Dim colCount As Integer = colNames.Count
			' ── 2. 收集当前表格显示的行（按Tag索引从g_rows取数据）──
			Dim seenFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			Dim uniqueRows As New List(Of BomDataRow)()
			For Each dgvRow As DataGridViewRow In g_grid.Rows
				If dgvRow.IsNewRow Then Continue For
				If dgvRow.Tag Is Nothing Then Continue For
				Dim ri As Integer = CInt(dgvRow.Tag)
				If ri < 0 OrElse ri >= g_rows.Count Then Continue For
				Dim r As BomDataRow = g_rows(ri)
				If seenFp.Contains(r.FilePath) Then Continue For
				seenFp.Add(r.FilePath) : uniqueRows.Add(r)
			Next
			Dim dataRowCount As Integer = uniqueRows.Count
			' ── 3. 构建 sharedStrings 和单元格数据 ──
			Dim strList As New List(Of String)()
			Dim strMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
			Dim colLetters As New List(Of String)()
			For ci As Integer = 0 To colCount - 1
				colLetters.Add(ColLetter(ci))
				If Not strMap.ContainsKey(colHdrs(ci)) Then strMap(colHdrs(ci)) = strList.Count : strList.Add(colHdrs(ci))
			Next
			Dim cellData(dataRowCount, colCount - 1) As String
			For ci As Integer = 0 To colCount - 1 : cellData(0, ci) = colHdrs(ci) : Next
			For ri As Integer = 0 To dataRowCount - 1
				Dim r As BomDataRow = uniqueRows(ri)
				For ci As Integer = 0 To colCount - 1
					Dim cn As String = colNames(ci)
					Dim v As String = ""
					Select Case cn
						Case "__fp__" : v = r.FilePath
						Case "__idx__" : v = ""
						Case "__type__" : v = r.PartType
						Case "FileName" : v = System.IO.Path.GetFileNameWithoutExtension(r.FileName)
						Case "DrawingNo" : v = If(r.DrawingNo Is Nothing, "", r.DrawingNo)
						Case "PartNumber" : v = If(r.PartNumber Is Nothing, "", r.PartNumber)
						Case "Category" : v = If(r.Category Is Nothing, "", r.Category)
						Case "__qty__" : v = r.Qty.ToString()
						Case "Material" : v = If(r.Material Is Nothing, "", r.Material)
						Case "Description" : v = If(r.Description Is Nothing, "", r.Description)
						Case "__bom__" : v = BomStructToStr(r.BomStruct)
						Case "__dwg__" : v = r.HasDrawing
						Case Else
							If cn.StartsWith("__custom__") Then
								Dim pn As String = cn.Substring(10)
								If r.Custom.ContainsKey(pn) Then v = r.Custom(pn)
							End If
					End Select
					cellData(ri + 1, ci) = v
					If Not strMap.ContainsKey(v) Then strMap(v) = strList.Count : strList.Add(v)
				Next
			Next
			' ── 4. 生成 xlsx XML ──
			Dim nsOX As String = "http://schemas.openxmlformats.org/"
			Dim nsSS As String = nsOX & "spreadsheetml/2006/main"
			Dim nsRel As String = nsOX & "officeDocument/2006/relationships"
			Dim nsPkg As String = nsOX & "package/2006/relationships"
			Dim nsWS As String = nsOX & "officeDocument/2006/relationships/worksheet"
			Dim nsSh As String = nsOX & "officeDocument/2006/relationships/sharedStrings"
			Dim nsSt As String = nsOX & "officeDocument/2006/relationships/styles"
			Dim nsOD As String = nsOX & "officeDocument/2006/relationships/officeDocument"
			Dim nsCT As String = "http://schemas.openxmlformats.org/package/2006/content-types"
			Dim xmlPI As String = Xq("<?xml version=~1.0~ encoding=~UTF-8~ standalone=~yes~?>")
			' sharedStrings
			Dim ssSb As New System.Text.StringBuilder()
			ssSb.Append(xmlPI)
			ssSb.Append(Xq("<sst xmlns=~") & nsSS & Xq("~ count=~") & strList.Count & Xq("~ uniqueCount=~") & strList.Count & Xq("~>"))
			For Each s As String In strList
				Dim esc As String = s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
				ssSb.Append(Xq("<si><t xml:space=~preserve~>") & esc & "</t></si>")
			Next
			ssSb.Append("</sst>")
			' worksheet
			Dim shSb As New System.Text.StringBuilder()
			shSb.Append(xmlPI)
			shSb.Append(Xq("<worksheet xmlns=~") & nsSS & Xq("~ xmlns:r=~") & nsRel & Xq("~>"))
			shSb.Append(Xq("<sheetViews><sheetView workbookViewId=~0~><pane ySplit=~1~ topLeftCell=~A2~ activePane=~bottomLeft~ state=~frozen~/></sheetView></sheetViews>"))
			shSb.Append("<cols>")
			shSb.Append(Xq("<col min=~1~ max=~1~ width=~50~ customWidth=~1~ hidden=~1~/>"))
			For ci As Integer = 1 To colCount - 1
				Dim colW2 As Double = If(colWidths.ContainsKey(ci), colWidths(ci), 18)
				shSb.Append(Xq("<col min=~") & (ci + 1) & Xq("~ max=~") & (ci + 1) & Xq("~ width=~") & colW2.ToString("0.00") & Xq("~ customWidth=~1~/>"))
			Next
			shSb.Append("</cols><sheetData>")
			Dim editRowHPt As Double = Math.Max(18, Math.Round(If(g_grid.Rows.Count > 0, g_grid.Rows(0).Height, g_grid.RowTemplate.Height) * 0.75, 2))
			' 表头行
			shSb.Append(Xq("<row r=~1~ ht=~" & editRowHPt & "~ customHeight=~1~>"))
			For ci As Integer = 0 To colCount - 1
				shSb.Append(Xq("<c r=~") & colLetters(ci) & Xq("1~ t=~s~ s=~1~><v>") & strMap(colHdrs(ci)) & "</v></c>")
			Next
			shSb.Append("</row>")
			' 数据行
			For ri As Integer = 0 To dataRowCount - 1
				Dim rowNum As Integer = ri + 2
				Dim r As BomDataRow = uniqueRows(ri)
				Dim isAssy As Boolean = (r.DocType = "部件")
				shSb.Append(Xq("<row r=~") & rowNum & Xq("~ ht=~" & editRowHPt & "~ customHeight=~1~>"))
				For ci As Integer = 0 To colCount - 1
					Dim cn As String = colNames(ci)
					If cn = "__thumb__" Then Continue For
					Dim v As String = cellData(ri + 1, ci)
					Dim sIdx As Integer = 0
					If ci = 0 Then
						sIdx = 2  ' 路径列：灰色隐藏
					ElseIf cn = "__idx__" OrElse cn = "__type__" OrElse cn = "__qty__" OrElse cn = "__bom__" OrElse cn = "__dwg__" Then
						sIdx = 3  ' 只读列：灰色
					ElseIf cn = "FileName" Then
						sIdx = 4  ' 文件名列：蓝色
					End If
					If isAssy Then sIdx = 5  ' 部件行：绿色
					If sIdx = 0 Then sIdx = 7  ' 可编辑列：白底带边框
					shSb.Append(Xq("<c r=~") & colLetters(ci) & rowNum & Xq("~ t=~s~") & Xq(" s=~") & sIdx & Xq("~") & "><v>" & strMap(v) & "</v></c>")
				Next
				shSb.Append("</row>")
			Next
			' 说明行
			Dim noteRow As Integer = dataRowCount + 3
			Dim noteStr As String = "说明：修改可编辑列后保存，实时写入 Inventor 零件属性。灰色列只读，第1列路径勿修改。"
			If Not strMap.ContainsKey(noteStr) Then strMap(noteStr) = strList.Count : strList.Add(noteStr)
			shSb.Append(Xq("<row r=~") & noteRow & Xq("~>"))
			shSb.Append(Xq("<c r=~A") & noteRow & Xq("~ t=~s~ s=~6~><v>") & strMap(noteStr) & "</v></c>")
			shSb.Append("</row></sheetData></worksheet>")
			' styles
			Dim stSb As New System.Text.StringBuilder()
			stSb.Append(xmlPI)
			stSb.Append(Xq("<styleSheet xmlns=~") & nsSS & Xq("~>"))
			stSb.Append("<fonts>")
			stSb.Append(Xq("<font><sz val=~10~/><name val=~Microsoft YaHei~/></font>"))
			stSb.Append(Xq("<font><sz val=~10~/><b/><name val=~Microsoft YaHei~/><color rgb=~FFFFFFFF~/></font>"))
			stSb.Append(Xq("<font><sz val=~10~/><name val=~Microsoft YaHei~/><color rgb=~FF787878~/></font>"))
			stSb.Append(Xq("<font><sz val=~10~/><name val=~Microsoft YaHei~/><color rgb=~FF787878~/></font>"))
			stSb.Append(Xq("<font><sz val=~10~/><name val=~Microsoft YaHei~/><color rgb=~FF0070C0~/></font>"))
			stSb.Append(Xq("<font><sz val=~10~/><b/><name val=~Microsoft YaHei~/><color rgb=~FF006400~/></font>"))
			stSb.Append(Xq("<font><sz val=~10~/><i/><name val=~Microsoft YaHei~/><color rgb=~FF003C3C~/></font>"))
			stSb.Append("</fonts>")
			stSb.Append("<fills>")
			stSb.Append(Xq("<fill><patternFill patternType=~none~/></fill>"))
			stSb.Append(Xq("<fill><patternFill patternType=~gray125~/></fill>"))
			stSb.Append(Xq("<fill><patternFill patternType=~solid~><fgColor rgb=~FF404040~/></patternFill></fill>"))
			stSb.Append(Xq("<fill><patternFill patternType=~solid~><fgColor rgb=~FFEFEFEF~/></patternFill></fill>"))
			stSb.Append(Xq("<fill><patternFill patternType=~solid~><fgColor rgb=~FFD0E8FF~/></patternFill></fill>"))
			stSb.Append(Xq("<fill><patternFill patternType=~solid~><fgColor rgb=~FFD0FFD0~/></patternFill></fill>"))
			stSb.Append(Xq("<fill><patternFill patternType=~none~/></fill>"))
			stSb.Append("</fills>")
			stSb.Append(Xq("<borders count=~2~><border><left/><right/><top/><bottom/><diagonal/></border><border><left style=~thin~><color rgb=~FF000000~/></left><right style=~thin~><color rgb=~FF000000~/></right><top style=~thin~><color rgb=~FF000000~/></top><bottom style=~thin~><color rgb=~FF000000~/></bottom><diagonal/></border></borders>"))
			stSb.Append(Xq("<cellStyleXfs count=~1~><xf numFmtId=~0~ fontId=~0~ fillId=~0~ borderId=~0~/></cellStyleXfs>"))
			stSb.Append("<cellXfs>")
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~0~ fillId=~0~ borderId=~1~ xfId=~0~ applyBorder=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~1~ fillId=~2~ borderId=~1~ xfId=~0~ applyBorder=~1~ applyFont=~1~ applyFill=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~2~ fillId=~3~ borderId=~1~ xfId=~0~ applyBorder=~1~ applyFont=~1~ applyFill=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~3~ fillId=~3~ borderId=~1~ xfId=~0~ applyBorder=~1~ applyFont=~1~ applyFill=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~4~ fillId=~4~ borderId=~1~ xfId=~0~ applyBorder=~1~ applyFont=~1~ applyFill=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~5~ fillId=~5~ borderId=~1~ xfId=~0~ applyBorder=~1~ applyFont=~1~ applyFill=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~6~ fillId=~0~ borderId=~1~ xfId=~0~ applyBorder=~1~ applyFont=~1~/>"))
			stSb.Append(Xq("<xf numFmtId=~0~ fontId=~0~ fillId=~0~ borderId=~1~ xfId=~0~ applyBorder=~1~/>"))
			stSb.Append("</cellXfs></styleSheet>")
			' 包结构 XML
			Dim ctXml As String = xmlPI & Xq("<Types xmlns=~") & nsCT & Xq("~>") &
			Xq("<Default Extension=~rels~ ContentType=~application/vnd.openxmlformats-package.relationships+xml~/>") &
			Xq("<Default Extension=~xml~ ContentType=~application/xml~/>") &
			Xq("<Override PartName=~/xl/workbook.xml~ ContentType=~application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml~/>") &
			Xq("<Override PartName=~/xl/worksheets/sheet1.xml~ ContentType=~application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml~/>") &
			Xq("<Override PartName=~/xl/sharedStrings.xml~ ContentType=~application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml~/>") &
			Xq("<Override PartName=~/xl/styles.xml~ ContentType=~application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml~/>") &
			"</Types>"
			Dim relsXml As String = xmlPI & Xq("<Relationships xmlns=~") & nsPkg & Xq("~>") &
			Xq("<Relationship Id=~rId1~ Type=~") & nsOD & Xq("~ Target=~xl/workbook.xml~/>") & "</Relationships>"
			Dim wbXml As String = xmlPI & Xq("<workbook xmlns=~") & nsSS & Xq("~ xmlns:r=~") & nsRel & Xq("~>") &
			Xq("<sheets><sheet name=~BOM编辑~ sheetId=~1~ r:id=~rId1~/></sheets></workbook>")
			Dim wbRelsXml As String = xmlPI & Xq("<Relationships xmlns=~") & nsPkg & Xq("~>") &
			Xq("<Relationship Id=~rId1~ Type=~") & nsWS & Xq("~ Target=~worksheets/sheet1.xml~/>") &
			Xq("<Relationship Id=~rId2~ Type=~") & nsSh & Xq("~ Target=~sharedStrings.xml~/>") &
			Xq("<Relationship Id=~rId3~ Type=~") & nsSt & Xq("~ Target=~styles.xml~/>") & "</Relationships>"
			' ── 5. 写入 xlsx 文件 ──
			Using fs As New System.IO.FileStream(savePath, System.IO.FileMode.Create)
				Using zip As New ZipArchive(fs, ZipArchiveMode.Create)
					WriteXlsxEntry(zip, "[Content_Types].xml", ctXml)
					WriteXlsxEntry(zip, "_rels/.rels", relsXml)
					WriteXlsxEntry(zip, "xl/workbook.xml", wbXml)
					WriteXlsxEntry(zip, "xl/_rels/workbook.xml.rels", wbRelsXml)
					WriteXlsxEntry(zip, "xl/styles.xml", stSb.ToString())
					WriteXlsxEntry(zip, "xl/sharedStrings.xml", ssSb.ToString())
					WriteXlsxEntry(zip, "xl/worksheets/sheet1.xml", shSb.ToString())
				End Using
			End Using
			' ── 6. 插入缩略图（原有方法：ZipArchive Update 模式追加图片）──
			If g_grid.Columns.Contains("cThumb") AndAlso g_grid.Columns("cThumb").Visible Then
				Dim thumbColPos As Integer = -1
				For ci As Integer = 0 To colNames.Count - 1
					If colNames(ci) = "__thumb__" Then thumbColPos = ci : Exit For
				Next
				If thumbColPos >= 0 Then
					' 收集缩略图（按 uniqueRows 顺序）
					Dim thumbList As New List(Of System.Drawing.Image)()
					For Each rr As BomDataRow In uniqueRows
						If rr.Thumbnail Is Nothing AndAlso Not String.IsNullOrEmpty(rr.FilePath) Then
							rr.Thumbnail = GetInventorThumbnail(rr.FilePath)
							If rr.Thumbnail Is Nothing Then rr.Thumbnail = GetShellThumbnail(rr.FilePath)
						End If
						thumbList.Add(rr.Thumbnail)
					Next
					If thumbList.Any(Function(img) img IsNot Nothing) Then
						Dim nsDr5 As String = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing"
						Dim nsDrA5 As String = "http://schemas.openxmlformats.org/drawingml/2006/main"
						Dim nsImg5 As String = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image"
						Dim nsDrRel5 As String = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing"
						Dim drSb5 As New System.Text.StringBuilder()
						Dim drRel5 As New System.Text.StringBuilder()
						drSb5.Append((xmlPI & "<xdr:wsDr xmlns:xdr=~" & nsDr5 & "~ xmlns:a=~" & nsDrA5 & "~ xmlns:r=~" & nsRel & "~>").Replace("~", ChrW(34)))
						drRel5.Append((xmlPI & "<Relationships xmlns=~" & nsPkg & "~>").Replace("~", ChrW(34)))
						Dim imgIdx5 As Integer = 0
						For ri5 As Integer = 0 To thumbList.Count - 1
							If thumbList(ri5) Is Nothing Then Continue For
							imgIdx5 += 1
							Dim rId5 As String = "rId" & imgIdx5
							Dim imgName5 As String = "img" & imgIdx5
							drSb5.Append(("<xdr:twoCellAnchor editAs=~oneCell~>").Replace("~", ChrW(34)))
							drSb5.Append("<xdr:from><xdr:col>" & thumbColPos & "</xdr:col><xdr:colOff>0</xdr:colOff><xdr:row>" & (ri5 + 1) & "</xdr:row><xdr:rowOff>0</xdr:rowOff></xdr:from>")
							drSb5.Append("<xdr:to><xdr:col>" & (thumbColPos + 1) & "</xdr:col><xdr:colOff>0</xdr:colOff><xdr:row>" & (ri5 + 2) & "</xdr:row><xdr:rowOff>0</xdr:rowOff></xdr:to>")
							drSb5.Append(("<xdr:pic><xdr:nvPicPr><xdr:cNvPr id=~" & imgIdx5 & "~ name=~" & imgName5 & "~/><xdr:cNvPicPr/></xdr:nvPicPr>").Replace("~", ChrW(34)))
							drSb5.Append(("<xdr:blipFill><a:blip r:embed=~" & rId5 & "~/><a:stretch><a:fillRect/></a:stretch></xdr:blipFill>").Replace("~", ChrW(34)))
							drSb5.Append(("<xdr:spPr><a:xfrm><a:off x=~0~ y=~0~/><a:ext cx=~0~ cy=~0~/></a:xfrm><a:prstGeom prst=~rect~><a:avLst/></a:prstGeom></xdr:spPr></xdr:pic><xdr:clientData/></xdr:twoCellAnchor>").Replace("~", ChrW(34)))
							drRel5.Append(("<Relationship Id=~" & rId5 & "~ Type=~" & nsImg5 & "~ Target=~../media/" & imgName5 & ".png~/>").Replace("~", ChrW(34)))
							Dim imgMs5 As New System.IO.MemoryStream()
							thumbList(ri5).Save(imgMs5, System.Drawing.Imaging.ImageFormat.Png)
							Using fs3 As New System.IO.FileStream(savePath, System.IO.FileMode.Open)
								Using zip3 As New ZipArchive(fs3, ZipArchiveMode.Update)
									Dim imgEntry5 As ZipArchiveEntry = zip3.CreateEntry("xl/media/" & imgName5 & ".png")
									Using imgStream5 As System.IO.Stream = imgEntry5.Open()
										Dim imgBytes5() As Byte = imgMs5.ToArray()
										imgStream5.Write(imgBytes5, 0, imgBytes5.Length)
									End Using
								End Using
							End Using
							imgMs5.Dispose()
						Next
						drSb5.Append("</xdr:wsDr>")
						drRel5.Append("</Relationships>")
						' 追加 drawing XML 和关系文件
						Using fs4 As New System.IO.FileStream(savePath, System.IO.FileMode.Open)
							Using zip4 As New ZipArchive(fs4, ZipArchiveMode.Update)
								WriteXlsxEntry(zip4, "xl/drawings/drawing1.xml", drSb5.ToString())
								WriteXlsxEntry(zip4, "xl/drawings/_rels/drawing1.xml.rels", drRel5.ToString())
								WriteXlsxEntry(zip4, "xl/worksheets/_rels/sheet1.xml.rels",
								(xmlPI & "<Relationships xmlns=~" & nsPkg & "~><Relationship Id=~rId1~ Type=~" & nsDrRel5 & "~ Target=~../drawings/drawing1.xml~/></Relationships>").Replace("~", ChrW(34)))
								' 更新 sheet1.xml 加 <drawing> 引用
								Dim shEntry5 As ZipArchiveEntry = zip4.GetEntry("xl/worksheets/sheet1.xml")
								If shEntry5 IsNot Nothing Then
									Dim shXml5 As String
									Using sr5 As New System.IO.StreamReader(shEntry5.Open()) : shXml5 = sr5.ReadToEnd() : End Using
									shXml5 = shXml5.Replace("</worksheet>", "<drawing r:id=~rId1~/></worksheet>".Replace("~", ChrW(34)))
									shEntry5.Delete()
									WriteXlsxEntry(zip4, "xl/worksheets/sheet1.xml", shXml5)
								End If
								' 更新 ContentTypes
								Dim ctEntry5 As ZipArchiveEntry = zip4.GetEntry("[Content_Types].xml")
								If ctEntry5 IsNot Nothing Then
									Dim ctXml5 As String
									Using sr5 As New System.IO.StreamReader(ctEntry5.Open()) : ctXml5 = sr5.ReadToEnd() : End Using
									If Not ctXml5.Contains(".png") Then ctXml5 = ctXml5.Replace("</Types>", Xq("<Default Extension=~png~ ContentType=~image/png~/>") & "</Types>")
									If Not ctXml5.Contains("drawing") Then ctXml5 = ctXml5.Replace("</Types>", Xq("<Override PartName=~/xl/drawings/drawing1.xml~ ContentType=~application/vnd.openxmlformats-officedocument.drawing+xml~/>") & "</Types>")
									ctEntry5.Delete()
									WriteXlsxEntry(zip4, "[Content_Types].xml", ctXml5)
								End If
							End Using
						End Using
					End If
				End If
			End If
			' ── 导出完成 ──
			SetStatus("  Excel 已导出: " & savePath, C(0, 130, 55))
			g_frm.Refresh()
			System.Windows.Forms.MessageBox.Show("Excel 已导出到：" & vbCrLf & savePath, "完成", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
		Catch ex As Exception
			SetStatus("  导出失败: " & ex.Message, C(200, 0, 0))
		End Try
	End Sub

	Sub ReleaseObj2(obj As Object)
		Try
			If obj IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
		Catch : End Try
	End Sub

	' ── WriteXlsxToInventor：从xlsx读取修改值，直接写Inventor属性，完全不碰g_rows和表格 ──
	Sub WriteXlsxToInventor(xlsxPath As String, colNames As List(Of String))
		Try
			Dim ss As New List(Of String)()
			Using fs As New System.IO.FileStream(xlsxPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
				Using zip As New ZipArchive(fs, ZipArchiveMode.Read)
					Dim ssE As ZipArchiveEntry = zip.GetEntry("xl/sharedStrings.xml")
					If ssE IsNot Nothing Then
						Dim ssXml As String
						Using sr As New System.IO.StreamReader(ssE.Open(), System.Text.Encoding.UTF8)
							ssXml = sr.ReadToEnd()
						End Using
						Dim p As Integer = 0
						Do
							Dim s1 As Integer = ssXml.IndexOf("<si>", p)
							If s1 < 0 Then s1 = ssXml.IndexOf("<si ", p)
							If s1 < 0 Then Exit Do
							Dim e1 As Integer = ssXml.IndexOf("</si>", s1)
							If e1 < 0 Then Exit Do
							Dim blk As String = ssXml.Substring(s1, e1 - s1 + 5)
							Dim sb2 As New System.Text.StringBuilder()
							Dim tp As Integer = 0
							Do
								Dim ts As Integer = blk.IndexOf("<t", tp)
								If ts < 0 Then Exit Do
								Dim tg As Integer = blk.IndexOf(">", ts)
								If tg < 0 Then Exit Do
								Dim te As Integer = blk.IndexOf("</t>", tg)
								If te < 0 Then Exit Do
								sb2.Append(blk.Substring(tg + 1, te - tg - 1))
								tp = te + 4
							Loop
							ss.Add(sb2.ToString().Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">"))
							p = e1 + 5
						Loop
					End If
					Dim shE As ZipArchiveEntry = zip.GetEntry("xl/worksheets/sheet1.xml")
					If shE Is Nothing Then Return
					Dim shXml As String
					Using sr2 As New System.IO.StreamReader(shE.Open(), System.Text.Encoding.UTF8)
						shXml = sr2.ReadToEnd()
					End Using
					Dim colMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
					Dim rp As Integer = 0
					Dim headerDone As Boolean = False
					Do
						Dim rs As Integer = shXml.IndexOf("<row ", rp)
						If rs < 0 Then Exit Do
						Dim re As Integer = shXml.IndexOf("</row>", rs)
						If re < 0 Then Exit Do
						Dim rowXml As String = shXml.Substring(rs, re - rs + 6)
						rp = re + 6
						Dim rNum As Integer = 0
						Dim riA As Integer = rowXml.IndexOf(" r=" & ChrW(34))
						If riA >= 0 Then
							Dim riB As Integer = rowXml.IndexOf(ChrW(34), riA + 4)
							If riB > riA + 4 Then Integer.TryParse(rowXml.Substring(riA + 4, riB - riA - 4), rNum)
						End If
						If rNum = 1 Then
							Dim cp As Integer = 0
							Do
								Dim cs As Integer = rowXml.IndexOf("<c ", cp)
								If cs < 0 Then Exit Do
								Dim ce As Integer = rowXml.IndexOf("</c>", cs)
								If ce < 0 Then Exit Do
								Dim cx As String = rowXml.Substring(cs, ce - cs + 4)
								cp = ce + 4
								Dim cref As String = ""
								Dim ri2 As Integer = cx.IndexOf(" r=" & ChrW(34))
								If ri2 >= 0 Then
									Dim re2 As Integer = cx.IndexOf(ChrW(34), ri2 + 4)
									If re2 > ri2 + 4 Then cref = cx.Substring(ri2 + 4, re2 - ri2 - 4)
								End If
								Dim clet As String = ""
								For Each ch1 As Char In cref
									If Char.IsLetter(ch1) Then clet &= ch1 Else Exit For
								Next
								Dim cidx As Integer = 0
								For Each ch2 As Char In clet
									cidx = cidx * 26 + (AscW(Char.ToUpper(ch2)) - 64)
								Next
								cidx -= 1
								If cidx >= 0 AndAlso cidx < colNames.Count Then colMap(clet) = cidx
							Loop
							headerDone = True
							Continue Do
						End If
						If Not headerDone OrElse rNum < 2 Then Continue Do
						Dim vals(colNames.Count - 1) As String
						For ii As Integer = 0 To colNames.Count - 1 : vals(ii) = "" : Next
						Dim cp2 As Integer = 0
						Do
							Dim cs2 As Integer = rowXml.IndexOf("<c ", cp2)
							If cs2 < 0 Then Exit Do
							Dim ce2 As Integer = rowXml.IndexOf("</c>", cs2)
							If ce2 < 0 Then Exit Do
							Dim cx2 As String = rowXml.Substring(cs2, ce2 - cs2 + 4)
							cp2 = ce2 + 4
							Dim cref2 As String = ""
							Dim ri3 As Integer = cx2.IndexOf(" r=" & ChrW(34))
							If ri3 >= 0 Then
								Dim re3 As Integer = cx2.IndexOf(ChrW(34), ri3 + 4)
								If re3 > ri3 + 4 Then cref2 = cx2.Substring(ri3 + 4, re3 - ri3 - 4)
							End If
							Dim clet2 As String = ""
							For Each ch3 As Char In cref2
								If Char.IsLetter(ch3) Then clet2 &= ch3 Else Exit For
							Next
							If Not colMap.ContainsKey(clet2) Then Continue Do
							Dim ci2 As Integer = colMap(clet2)
							Dim vs As Integer = cx2.IndexOf("<v>")
							Dim ve As Integer = cx2.IndexOf("</v>")
							If vs >= 0 AndAlso ve > vs Then
								Dim vstr As String = cx2.Substring(vs + 3, ve - vs - 3)
								Dim ta As Integer = cx2.IndexOf(" t=" & ChrW(34) & "s" & ChrW(34))
								If ta >= 0 AndAlso ta < vs Then
									Dim vi As Integer = -1
									If Integer.TryParse(vstr, vi) AndAlso vi >= 0 AndAlso vi < ss.Count Then vals(ci2) = ss(vi)
								Else
									vals(ci2) = vstr
								End If
							End If
						Loop
						' ── 第0列是完整路径，直接写Inventor属性 ──
						Dim fp As String = vals(0).Trim()
						If String.IsNullOrEmpty(fp) OrElse Not System.IO.File.Exists(fp) Then Continue Do
						Dim tDoc As Document = Nothing
						For Each od As Document In ThisApplication.Documents
							If String.Compare(od.FullDocumentName, fp, True) = 0 Then tDoc = od : Exit For
						Next
						If tDoc Is Nothing Then
							Try
								tDoc = ThisApplication.Documents.Open(fp, False)
							Catch : End Try
						End If
						If tDoc Is Nothing Then Continue Do
						Try
							Dim ps As PropertySets = tDoc.PropertySets
							Dim dtp As PropertySet = ps.Item(GUID_DTP)
							Dim udp As PropertySet = GetUDP(tDoc)
							For wi As Integer = 1 To colNames.Count - 1
								Dim cn As String = colNames(wi)
								Dim v As String = vals(wi)
								Select Case cn
									Case "__fp__", "__idx__", "__type__", "__qty__", "__bom__", "__dwg__", "FileName", "__thumb__"
										Continue For
								End Select
								Try
									Select Case cn
										Case "DrawingNo"
											If udp IsNot Nothing Then SafeWriteUDP(udp, "图号", v)
										Case "PartNumber"
											dtp.Item("Part Number").Value = v
										Case "Category"
											Try
												ps.Item(GUID_DOCSUMMARY).Item("Category").Value = v
											Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
												Try : ps.Item(GUID_DOCSUMMARY).Add(v, "Category") : Catch : End Try
											End Try
										Case "Material"
											If tDoc.DocumentType = kPartDocumentObject Then
												Try : dtp.Item("Material").Value = v : Catch : End Try
											End If
										Case "Description"
											dtp.Item("Description").Value = v
										Case Else
											If cn.StartsWith("__custom__") Then
												Dim propName As String = cn.Substring(10)
												If udp IsNot Nothing Then SafeWriteUDP(udp, propName, v)
											End If
									End Select
								Catch : End Try
							Next
						Catch : End Try
					Loop
				End Using
			End Using
		Catch ex As Exception
			SetStatus("  写入属性失败: " & ex.Message, C(200, 0, 0))
		End Try
	End Sub



	Sub WriteXlsxEntry(zip As ZipArchive, entryName As String, content As String)
		Dim e As ZipArchiveEntry = zip.CreateEntry(entryName)
		Using sw As New System.IO.StreamWriter(e.Open(), New System.Text.UTF8Encoding(False))
			sw.Write(content)
		End Using
	End Sub

	Function ColLetter(colIdx As Integer) As String
		Dim result As String = ""
		Dim n As Integer = colIdx + 1
		Do While n > 0
			Dim colRem As Integer = (n - 1) Mod 26
			result = ChrW(65 + colRem) & result
			n = (n - 1) \ 26
		Loop
		Return result
	End Function


	Function CsvEscape(val As String) As String
		If String.IsNullOrEmpty(val) Then Return ""
		If val.Contains(",") OrElse val.Contains("""") OrElse
			val.Contains(vbCr) OrElse val.Contains(vbLf) Then
			Return """" & val.Replace("""", """""") & """"
		End If
		Return val
	End Function

	Function OrangeMid() As System.Drawing.Color
		Select Case g_theme
			Case 1 : Return System.Drawing.Color.FromArgb(0, 150, 200)
			Case 2 : Return System.Drawing.Color.FromArgb(30, 140, 110)
			Case 3 : Return System.Drawing.Color.FromArgb(220, 60, 70)
			Case 4 : Return System.Drawing.Color.FromArgb(255, 140, 0)
			Case 5 : Return System.Drawing.Color.FromArgb(230, 80, 160)
			Case 7 : Return System.Drawing.Color.FromArgb(90, 95, 115)
			Case 8 : Return g_custTitleBar
			Case Else : Return System.Drawing.Color.FromArgb(108, 92, 231)
		End Select
	End Function
	Function OrangeLight() As System.Drawing.Color
		Select Case g_theme
			Case 1 : Return System.Drawing.Color.FromArgb(0, 100, 190)
			Case 2 : Return System.Drawing.Color.FromArgb(20, 110, 85)
			Case 3 : Return System.Drawing.Color.FromArgb(180, 40, 50)
			Case 4 : Return System.Drawing.Color.FromArgb(200, 110, 0)
			Case 5 : Return System.Drawing.Color.FromArgb(190, 50, 130)
			Case 7 : Return System.Drawing.Color.FromArgb(120, 125, 145)
			Case 8 : Return LightenColor(g_custTitleBar, 0.25)
			Case Else : Return System.Drawing.Color.FromArgb(85, 70, 200)
		End Select
	End Function
	Function ExcelBtnBg() As System.Drawing.Color
		Dim c As System.Drawing.Color = OrangeMid()
		Return System.Drawing.Color.FromArgb(
			CInt(c.R + (255 - c.R) * 0.35),
			CInt(c.G + (255 - c.G) * 0.35),
			CInt(c.B + (255 - c.B) * 0.35))
	End Function
	Function LogoBg() As System.Drawing.Color
		Return OrangeMid()
	End Function
	Function LogoFg() As System.Drawing.Color
		Return System.Drawing.Color.White
	End Function
	Function PanelTitleBg() As System.Drawing.Color
		Return OrangeMid()
	End Function
	Function PanelBg() As System.Drawing.Color
		Select Case g_theme
			Case 1 : Return System.Drawing.Color.FromArgb(235, 245, 255)
			Case 2 : Return System.Drawing.Color.FromArgb(235, 248, 244)
			Case 3 : Return System.Drawing.Color.FromArgb(255, 240, 240)
			Case 4 : Return System.Drawing.Color.FromArgb(255, 245, 235)
			Case 5 : Return System.Drawing.Color.FromArgb(255, 240, 248)
			Case 7 : Return System.Drawing.Color.FromArgb(90, 95, 115)
			Case 8 : Return g_custColMgr
			Case Else : Return System.Drawing.Color.FromArgb(243, 240, 255)
		End Select
	End Function
	Function PanelAddBg() As System.Drawing.Color
		Select Case g_theme
			Case 1 : Return System.Drawing.Color.FromArgb(210, 230, 255)
			Case 2 : Return System.Drawing.Color.FromArgb(205, 230, 220)
			Case 3 : Return System.Drawing.Color.FromArgb(250, 215, 215)
			Case 4 : Return System.Drawing.Color.FromArgb(255, 228, 195)
			Case 5 : Return System.Drawing.Color.FromArgb(250, 215, 235)
			Case 7 : Return System.Drawing.Color.FromArgb(90, 95, 115)
			Case 8 : Return g_custColMgr
			Case Else : Return System.Drawing.Color.FromArgb(228, 222, 252)
		End Select
	End Function
	Function BtnAddBg() As System.Drawing.Color
		Return OrangeMid()
	End Function
	Function CustomColFg() As System.Drawing.Color
		Select Case g_theme
			Case 1 : Return System.Drawing.Color.FromArgb(0, 60, 160)
			Case 2 : Return System.Drawing.Color.FromArgb(10, 90, 70)
			Case 3 : Return System.Drawing.Color.FromArgb(160, 30, 40)
			Case 4 : Return System.Drawing.Color.FromArgb(180, 100, 0)
			Case 5 : Return System.Drawing.Color.FromArgb(180, 40, 110)
			Case 7 : Return System.Drawing.Color.FromArgb(200, 205, 220)
			Case 8 : Return ContrastFg(g_custColMgr)
			Case Else : Return System.Drawing.Color.FromArgb(80, 50, 180)
		End Select
	End Function
	Function AssyGreen() As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(0, 184, 148)
	End Function
	Function WeldRed() As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(225, 112, 85)
	End Function
	Function ToolBarBg() As System.Drawing.Color
		Select Case g_theme
			Case 1 : Return System.Drawing.Color.FromArgb(230, 240, 255)
			Case 2 : Return System.Drawing.Color.FromArgb(230, 242, 236)
			Case 3 : Return System.Drawing.Color.FromArgb(252, 230, 230)
			Case 4 : Return System.Drawing.Color.FromArgb(255, 238, 225)
			Case 5 : Return System.Drawing.Color.FromArgb(252, 228, 242)
			Case 7 : Return System.Drawing.Color.FromArgb(90, 95, 115)
			Case 8 : Return g_custTitleBar
			Case Else : Return System.Drawing.Color.FromArgb(238, 235, 252)
		End Select
	End Function

	Function C(r As Integer, g As Integer, b As Integer) As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(r, g, b)
	End Function
	Function ColorToStr(c As System.Drawing.Color) As String
		Return c.R.ToString() & "," & c.G.ToString() & "," & c.B.ToString()
	End Function
	Function StrToColor(sec As Dictionary(Of String, String), key As String, def As System.Drawing.Color) As System.Drawing.Color
		If sec IsNot Nothing AndAlso sec.ContainsKey(key) Then
			Dim parts As String() = sec(key).Split(","c)
			Dim r As Integer, g As Integer, b As Integer
			If parts.Length = 3 AndAlso Integer.TryParse(parts(0).Trim(), r) AndAlso Integer.TryParse(parts(1).Trim(), g) AndAlso Integer.TryParse(parts(2).Trim(), b) Then
				If r >= 0 AndAlso r <= 255 AndAlso g >= 0 AndAlso g <= 255 AndAlso b >= 0 AndAlso b <= 255 Then
					Return System.Drawing.Color.FromArgb(r, g, b)
				End If
			End If
		End If
		Return def
	End Function
	Function LightenColor(c As System.Drawing.Color, f As Double) As System.Drawing.Color
		Return System.Drawing.Color.FromArgb(
			CInt(c.R + (255 - c.R) * f),
			CInt(c.G + (255 - c.G) * f),
			CInt(c.B + (255 - c.B) * f))
	End Function
	Function ContrastFg(bg As System.Drawing.Color) As System.Drawing.Color
		If bg.GetBrightness() < 0.5 Then Return System.Drawing.Color.White
		Return System.Drawing.Color.Black
	End Function
	Function MakeFont(nm As String, sz As Single) As System.Drawing.Font
		Try : Return New System.Drawing.Font(CStr(nm), sz) : Catch : End Try
		Return New System.Drawing.Font(CStr(System.Drawing.SystemFonts.DefaultFont.Name), sz)
	End Function
	Function MakeFontBold(nm As String, sz As Single) As System.Drawing.Font
		Try : Return New System.Drawing.Font(CStr(nm), sz, System.Drawing.FontStyle.Bold) : Catch : End Try
		Return New System.Drawing.Font(CStr(System.Drawing.SystemFonts.DefaultFont.Name), sz, System.Drawing.FontStyle.Bold)
	End Function

	Function MeasureTextWidth(Text As String, font As System.Drawing.Font) As Integer
		If String.IsNullOrEmpty(Text) Then Return 0
		Try
			Dim tmpBmp As New System.Drawing.Bitmap(1, 1)
			Dim gfx As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(tmpBmp)
			Dim mw As Integer = CInt(gfx.MeasureString(Text, font).Width)
			gfx.Dispose()
			tmpBmp.Dispose()
			Return mw
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return Text.Length * 8
		End Try
	End Function

	Sub ApplyThemeToForm()
		Try
			' 先清掉每列列头单独设置的颜色（自定义/呼吸灯残留），让默认列头样式生效
			Try
				If g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
					For Each gc As System.Windows.Forms.DataGridViewColumn In g_grid.Columns
						gc.HeaderCell.Style.BackColor = System.Drawing.Color.Empty
						gc.HeaderCell.Style.ForeColor = System.Drawing.Color.Empty
					Next
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			' 切换主题时先复位网格级文字色，防止自定义文字色残留（行级语义色由刷新/ApplyRowThemeColors 设置，不动）
			Try
				If g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
					g_grid.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty
					g_grid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Empty
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			If g_theme = 8 Then
				ApplyCustomTheme()
				ApplyRowThemeColors()
				Return
			End If
			' 离开自定义主题时，先把顶栏控件复位为默认底色，再交给下面的主题匹配重绘
			Try
				If g_pnlTop IsNot Nothing Then
					For Each tc As System.Windows.Forms.Control In g_pnlTop.Controls
						Try
							If TypeOf tc Is System.Windows.Forms.ComboBox Then
								' 功能下拉保持默认浅紫，主题下拉保持白底
								If tc Is g_cmbTools Then tc.BackColor = C(228, 222, 252)
							ElseIf TypeOf tc Is System.Windows.Forms.Panel Then
								' Logo 图标面板
								tc.BackColor = C(80, 60, 200)
							ElseIf TypeOf tc Is System.Windows.Forms.Button OrElse TypeOf tc Is System.Windows.Forms.CheckBox OrElse TypeOf tc Is System.Windows.Forms.Label Then
								tc.BackColor = OrangeMid()
							End If
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						End Try
					Next
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			Dim mains As System.Drawing.Color() = New System.Drawing.Color() {
				System.Drawing.Color.FromArgb(108, 92, 231),
				System.Drawing.Color.FromArgb(0, 150, 200),
				System.Drawing.Color.FromArgb(30, 140, 110),
				System.Drawing.Color.FromArgb(220, 60, 70),
				System.Drawing.Color.FromArgb(255, 140, 0),
				System.Drawing.Color.FromArgb(230, 80, 160),
				System.Drawing.Color.FromArgb(180, 120, 220),
				System.Drawing.Color.FromArgb(90, 95, 115)
			}
			Dim panelBgs As System.Drawing.Color() = New System.Drawing.Color() {
				System.Drawing.Color.FromArgb(243, 240, 255),
				System.Drawing.Color.FromArgb(235, 245, 255),
				System.Drawing.Color.FromArgb(235, 248, 244),
				System.Drawing.Color.FromArgb(255, 240, 240),
				System.Drawing.Color.FromArgb(255, 245, 235),
				System.Drawing.Color.FromArgb(255, 240, 248),
				System.Drawing.Color.FromArgb(255, 225, 245),
				System.Drawing.Color.FromArgb(90, 95, 115)
			}
			Dim barBgs As System.Drawing.Color() = New System.Drawing.Color() {
				System.Drawing.Color.FromArgb(238, 235, 252),
				System.Drawing.Color.FromArgb(230, 240, 255),
				System.Drawing.Color.FromArgb(230, 242, 236),
				System.Drawing.Color.FromArgb(252, 230, 230),
				System.Drawing.Color.FromArgb(255, 238, 225),
				System.Drawing.Color.FromArgb(252, 228, 242),
				System.Drawing.Color.FromArgb(240, 225, 250),
				System.Drawing.Color.FromArgb(90, 95, 115)
			}
			Dim winBgs As System.Drawing.Color() = New System.Drawing.Color() {
				System.Drawing.Color.FromArgb(245, 243, 255),
				System.Drawing.Color.FromArgb(252, 252, 252),
				System.Drawing.Color.FromArgb(244, 248, 245),
				System.Drawing.Color.FromArgb(252, 245, 245),
				System.Drawing.Color.FromArgb(255, 248, 240),
				System.Drawing.Color.FromArgb(255, 245, 250),
				System.Drawing.Color.FromArgb(248, 240, 255),
				System.Drawing.Color.FromArgb(90, 95, 115)
			}
			Dim logoBgs As System.Drawing.Color() = New System.Drawing.Color() {
				System.Drawing.Color.FromArgb(80, 60, 200),
				System.Drawing.Color.FromArgb(0, 120, 170),
				System.Drawing.Color.FromArgb(20, 100, 80),
				System.Drawing.Color.FromArgb(180, 40, 50),
				System.Drawing.Color.FromArgb(200, 110, 0),
				System.Drawing.Color.FromArgb(190, 50, 130),
				System.Drawing.Color.FromArgb(150, 80, 200),
				System.Drawing.Color.FromArgb(70, 75, 95)
			}
			g_frm.BackColor = winBgs(g_theme)
			For Each ctrl As System.Windows.Forms.Control In g_frm.Controls
				Try
					ApplyThemeControl(ctrl, mains, panelBgs, barBgs, logoBgs)
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				End Try
			Next
			Try
				g_grid.ColumnHeadersDefaultCellStyle.BackColor = mains(g_theme)
				g_grid.AlternatingRowsDefaultCellStyle.BackColor = panelBgs(g_theme)
				g_grid.BackgroundColor = panelBgs(g_theme)
				If g_theme = 7 Then
					g_grid.BackgroundColor = System.Drawing.Color.FromArgb(110, 112, 120)
					g_grid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(155, 157, 165)
					g_grid.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(145, 147, 155)
					g_grid.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 240)
					g_grid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(60, 75, 110)
					g_grid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
					Try
						For Each r As System.Windows.Forms.DataGridViewRow In g_grid.Rows
							r.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(60, 75, 110)
							r.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
						Next
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				Else
					g_grid.DefaultCellStyle.BackColor = System.Drawing.Color.Empty
					g_grid.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty
					g_grid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Empty
					g_grid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Empty
					Try
						For Each r As System.Windows.Forms.DataGridViewRow In g_grid.Rows
							r.DefaultCellStyle.BackColor = System.Drawing.Color.Empty
							r.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty
							r.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Empty
							r.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Empty
						Next
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				End If
				If g_theme = 7 Then
					Try
						For ri As Integer = 0 To g_grid.Rows.Count - 1
							Dim r As System.Windows.Forms.DataGridViewRow = g_grid.Rows(ri)
							If ri Mod 2 = 0 Then
								r.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(145, 147, 155)
							Else
								r.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(155, 157, 165)
							End If
							r.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(235, 235, 240)
						Next
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				End If
				ApplyRowThemeColors()
				g_grid.Invalidate()
				Try
					If g_chkFlow IsNot Nothing Then
						g_chkFlow.BackColor = PanelBg()
						For Each rowPnl As System.Windows.Forms.Control In g_chkFlow.Controls
							If TypeOf rowPnl Is System.Windows.Forms.Panel Then
								If rowPnl Is g_selectedChkRow Then
									Dim selClr As System.Drawing.Color = If(g_theme = 7, System.Drawing.Color.FromArgb(60, 75, 110), System.Drawing.Color.FromArgb(180, 220, 180))
									rowPnl.BackColor = selClr
									For Each c2 As System.Windows.Forms.Control In rowPnl.Controls
										If Not TypeOf c2 Is System.Windows.Forms.Button Then
											c2.BackColor = selClr
											If TypeOf c2 Is System.Windows.Forms.Label Then c2.ForeColor = If(g_theme = 7, System.Drawing.Color.White, System.Drawing.Color.Black)
										End If
									Next
								Else
									rowPnl.BackColor = PanelBg()
									For Each c2 As System.Windows.Forms.Control In rowPnl.Controls
										If Not TypeOf c2 Is System.Windows.Forms.Button Then
											c2.BackColor = PanelBg()
											If TypeOf c2 Is System.Windows.Forms.Label Then c2.ForeColor = System.Drawing.Color.Black
										End If
									Next
								End If
							End If
						Next
					End If
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				End Try
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			Try
				For Each c As System.Windows.Forms.Control In g_frm.Controls
					If c.Text = "Excel导出" Then
						Dim bb As System.Windows.Forms.Button = CType(c, System.Windows.Forms.Button)
						bb.BackColor = ExcelBtnBg()
						bb.FlatAppearance.BorderColor = OrangeLight()
					End If
					If c.HasChildren Then
						For Each cc As System.Windows.Forms.Control In c.Controls
							If cc.Text = "Excel导出" Then
								Dim bb2 As System.Windows.Forms.Button = CType(cc, System.Windows.Forms.Button)
								bb2.BackColor = ExcelBtnBg()
								bb2.FlatAppearance.BorderColor = OrangeLight()
							End If
						Next
					End If
				Next
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			g_frm.Refresh()
			' 递归重置底部工具栏背景
			If g_pnlBottom IsNot Nothing AndAlso Not g_pnlBottom.IsDisposed Then
				g_pnlBottom.BackColor = ToolBarBg()
				ResetPanelRecursive(g_pnlBottom, ToolBarBg())
			End If
			' 重置列管理面板（呼吸灯/暗黑切回后恢复）
			Try
				If g_chkPanel IsNot Nothing Then
					g_chkPanel.BackColor = PanelBg()
					For Each pc As System.Windows.Forms.Control In g_chkPanel.Controls
						Try
							If pc.Name = "pnlUpDown" OrElse (TypeOf pc Is System.Windows.Forms.Panel AndAlso pc.Controls.Count > 0 AndAlso TypeOf pc.Controls(0) Is System.Windows.Forms.Button AndAlso pc.Controls(0).Text = "▲") Then
								pc.BackColor = PanelBg()
								For Each bc As System.Windows.Forms.Control In pc.Controls
									If TypeOf bc Is System.Windows.Forms.Button Then bc.BackColor = PanelTitleBg()
								Next
							ElseIf TypeOf pc Is System.Windows.Forms.Label AndAlso pc.Text.Length > 0 Then
								pc.BackColor = PanelTitleBg()
								pc.ForeColor = System.Drawing.Color.White
							ElseIf pc.Name = "pnlAdd" OrElse (TypeOf pc Is System.Windows.Forms.Panel AndAlso pc.Controls.Count > 0 AndAlso TypeOf pc.Controls(0) Is System.Windows.Forms.Label) Then
								Dim addBg As System.Drawing.Color = PanelAddBg()
								pc.BackColor = addBg
								For Each ac As System.Windows.Forms.Control In pc.Controls
									If TypeOf ac Is System.Windows.Forms.Label OrElse TypeOf ac Is System.Windows.Forms.TextBox OrElse TypeOf ac Is System.Windows.Forms.ComboBox Then
										ac.BackColor = addBg
										If TypeOf ac Is System.Windows.Forms.Label Then ac.ForeColor = If(g_theme = 7, System.Drawing.Color.White, System.Drawing.Color.Black)
									ElseIf TypeOf ac Is System.Windows.Forms.Button Then
										ac.BackColor = OrangeMid()
									End If
								Next
							End If
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						End Try
					Next
					' 重置列管理行
					If g_chkFlow IsNot Nothing Then
						For Each rowPnl As System.Windows.Forms.Control In g_chkFlow.Controls
							If TypeOf rowPnl Is System.Windows.Forms.Panel Then
								If rowPnl Is g_selectedChkRow Then
									Dim selClr As System.Drawing.Color = If(g_theme = 7, System.Drawing.Color.FromArgb(60, 75, 110), System.Drawing.Color.FromArgb(180, 220, 180))
									rowPnl.BackColor = selClr
								Else
									rowPnl.BackColor = PanelBg()
								End If
								For Each cc As System.Windows.Forms.Control In rowPnl.Controls
									If TypeOf cc Is System.Windows.Forms.Button AndAlso cc.Text = "X" Then
										cc.BackColor = C(200, 50, 50)
									ElseIf Not TypeOf cc Is System.Windows.Forms.Button Then
										cc.BackColor = rowPnl.BackColor
										If TypeOf cc Is System.Windows.Forms.Label Then cc.ForeColor = If(g_theme = 7, System.Drawing.Color.White, System.Drawing.Color.Black)
									End If
								Next
							End If
						Next
						g_chkFlow.BackColor = PanelBg()
					End If
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			g_frm.Invalidate(True)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub ResetPanelRecursive(parent As System.Windows.Forms.Control, bgClr As System.Drawing.Color)
		Try
			For Each c As System.Windows.Forms.Control In parent.Controls
				Try
					If TypeOf c Is System.Windows.Forms.Label OrElse TypeOf c Is System.Windows.Forms.TextBox Then
						c.BackColor = bgClr
					ElseIf TypeOf c Is System.Windows.Forms.Button Then
						c.BackColor = OrangeMid()
					ElseIf c.HasChildren Then
						ResetPanelRecursive(c, bgClr)
					End If
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				End Try
			Next
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub ApplyThemeControl(ctrl As System.Windows.Forms.Control, mains As System.Drawing.Color(), panelBgs As System.Drawing.Color(), barBgs As System.Drawing.Color(), logoBgs As System.Drawing.Color())
		Try
			Dim cur As System.Drawing.Color = ctrl.BackColor
			For t As Integer = 0 To 7
				If cur.ToArgb() = mains(t).ToArgb() Then
					ctrl.BackColor = mains(g_theme)
					Exit For
				End If
				If cur.ToArgb() = panelBgs(t).ToArgb() Then
					ctrl.BackColor = panelBgs(g_theme)
					Exit For
				End If
				If cur.ToArgb() = barBgs(t).ToArgb() Then
					ctrl.BackColor = barBgs(g_theme)
					Exit For
				End If
				If cur.ToArgb() = logoBgs(t).ToArgb() Then
					ctrl.BackColor = logoBgs(g_theme)
					Exit For
				End If
			Next
			If ctrl.HasChildren Then
				For Each cc As System.Windows.Forms.Control In ctrl.Controls
					ApplyThemeControl(cc, mains, panelBgs, barBgs, logoBgs)
				Next
			End If
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	' ═══ 自定义主题（g_theme = 8）═══
	Sub ApplyCustomTheme()
		Try
			' 1. 窗口背景色
			g_frm.BackColor = g_custWinBg
			' 2. 上下标题（顶部栏 + 底部工具栏，同一颜色）
			If g_pnlTop IsNot Nothing Then
				g_pnlTop.BackColor = g_custTitleBar
				For Each c As System.Windows.Forms.Control In g_pnlTop.Controls
					Try
						If TypeOf c Is System.Windows.Forms.ComboBox Then
							' 功能下拉用自定义功能下拉色，主题下拉保持白底
							If c Is g_cmbTools Then c.BackColor = g_custFuncDrop
						ElseIf TypeOf c Is System.Windows.Forms.Button OrElse TypeOf c Is System.Windows.Forms.CheckBox OrElse TypeOf c Is System.Windows.Forms.Label OrElse TypeOf c Is System.Windows.Forms.Panel Then
							c.BackColor = g_custTitleBar
						End If
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				Next
			End If
			If g_pnlBottom IsNot Nothing Then
				g_pnlBottom.BackColor = g_custTitleBar
				ResetPanelRecursive(g_pnlBottom, g_custTitleBar)
			End If
			' 3. 功能下拉
			If g_cmbTools IsNot Nothing Then g_cmbTools.BackColor = g_custFuncDrop
			' 4. 表格列头
			If g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
				g_grid.ColumnHeadersDefaultCellStyle.BackColor = g_custColHeader
				g_grid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
				For Each gc As System.Windows.Forms.DataGridViewColumn In g_grid.Columns
					Try
						gc.HeaderCell.Style.BackColor = g_custColHeader
						gc.HeaderCell.Style.ForeColor = System.Drawing.Color.White
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				Next
				' 行底色与文字颜色（间隔行 / 白底行 / 文字色独立设置）
				g_grid.BackgroundColor = g_custWinBg
				g_grid.AlternatingRowsDefaultCellStyle.BackColor = g_custRowAlt
				g_grid.DefaultCellStyle.BackColor = g_custRowWhite
				g_grid.DefaultCellStyle.ForeColor = g_custCellText
				Try
					For Each r As System.Windows.Forms.DataGridViewRow In g_grid.Rows
						r.DefaultCellStyle.BackColor = System.Drawing.Color.Empty
						r.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty
					Next
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				End Try
			End If
			' 5. 右边列管理控件（整块同色）
			If g_chkPanel IsNot Nothing Then
				g_chkPanel.BackColor = g_custColMgr
				For Each pc As System.Windows.Forms.Control In g_chkPanel.Controls
					Try
						pc.BackColor = g_custColMgr
						If TypeOf pc Is System.Windows.Forms.Label Then pc.ForeColor = System.Drawing.Color.White
						If pc.HasChildren Then
							For Each cc As System.Windows.Forms.Control In pc.Controls
								Try
									If TypeOf cc Is System.Windows.Forms.Button Then
										If cc.Text = "X" Then cc.BackColor = C(200, 50, 50) Else cc.BackColor = g_custColMgr
										cc.ForeColor = System.Drawing.Color.White
									ElseIf TypeOf cc Is System.Windows.Forms.Label OrElse TypeOf cc Is System.Windows.Forms.ComboBox OrElse TypeOf cc Is System.Windows.Forms.TextBox Then
										cc.BackColor = g_custColMgr
									End If
								Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
								End Try
							Next
						End If
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
				Next
				If g_chkFlow IsNot Nothing Then
					g_chkFlow.BackColor = g_custColMgr
					For Each rowPnl As System.Windows.Forms.Control In g_chkFlow.Controls
						Try
							If TypeOf rowPnl Is System.Windows.Forms.Panel Then
								rowPnl.BackColor = g_custColMgr
								For Each cc As System.Windows.Forms.Control In rowPnl.Controls
									Try
										If TypeOf cc Is System.Windows.Forms.Button Then
											If cc.Text = "X" Then cc.BackColor = C(200, 50, 50) Else cc.BackColor = g_custColMgr
											cc.ForeColor = System.Drawing.Color.White
										Else
											cc.BackColor = g_custColMgr
											If TypeOf cc Is System.Windows.Forms.Label Then cc.ForeColor = ContrastFg(g_custColMgr)
										End If
									Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
									End Try
								Next
							End If
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						End Try
					Next
					' 恢复当前选中行高亮
					If g_selectedChkRow IsNot Nothing AndAlso Not g_selectedChkRow.IsDisposed Then
						g_selectedChkRow.BackColor = C(180, 220, 180)
						For Each cc As System.Windows.Forms.Control In g_selectedChkRow.Controls
							Try
								If Not TypeOf cc Is System.Windows.Forms.Button Then
									cc.BackColor = C(180, 220, 180)
									If TypeOf cc Is System.Windows.Forms.Label Then cc.ForeColor = System.Drawing.Color.Black
								End If
							Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
							End Try
						Next
					End If
				End If
			End If
			' 6. Excel导出按钮
			If g_btnExcelExport IsNot Nothing AndAlso Not g_btnExcelExport.IsDisposed Then
				g_btnExcelExport.BackColor = g_custExcelBtn
				g_btnExcelExport.FlatAppearance.BorderColor = LightenColor(g_custExcelBtn, 0.3)
			End If
			Try
				For Each c As System.Windows.Forms.Control In g_frm.Controls
					If c.Text = "Excel导出" Then
						Dim bb As System.Windows.Forms.Button = CType(c, System.Windows.Forms.Button)
						bb.BackColor = g_custExcelBtn
					End If
					If c.HasChildren Then
						For Each cc As System.Windows.Forms.Control In c.Controls
							If cc.Text = "Excel导出" Then
								Dim bb2 As System.Windows.Forms.Button = CType(cc, System.Windows.Forms.Button)
								bb2.BackColor = g_custExcelBtn
							End If
						Next
					End If
				Next
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			' 7. 刷新按钮
			If g_btnRefresh IsNot Nothing AndAlso Not g_btnRefresh.IsDisposed Then
				g_btnRefresh.BackColor = g_custRefresh
			End If
			g_frm.Refresh()
			g_frm.Invalidate(True)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	' 行级语义色重设：自定义主题所有行统一文字色，其它主题部件绿/红、标准件灰；主题切换/文字色修改后立即生效
	Sub ApplyRowThemeColors()
		Try
			If g_theme = 7 Then Return ' 暗黑主题行色由暗黑分支统一处理
			If g_grid Is Nothing OrElse g_grid.IsDisposed Then Return
			For Each gr As System.Windows.Forms.DataGridViewRow In g_grid.Rows
				Try
					If gr.Tag Is Nothing Then Continue For
					Dim srcIdx As Integer = CInt(gr.Tag)
					If srcIdx < 0 OrElse srcIdx >= g_rows.Count Then Continue For
					Dim br As BomDataRow = g_rows(srcIdx)
					' 所有行：自定义主题显式统一文字色（不依赖网格级继承，保证生效）
					Dim rowSt As DataGridViewCellStyle = gr.DefaultCellStyle
					If g_theme = 8 Then
						rowSt.ForeColor = g_custCellText
					End If
					If br.DocType = "部件" Then
						' 部件行：自定义已统一文字色，其它主题固定语义色（焊接红/装配绿）
						If g_theme <> 8 Then
							If br.IsWeldment Then
								rowSt.ForeColor = WeldRed()
							Else
								rowSt.ForeColor = AssyGreen()
							End If
						End If
						rowSt.Font = MakeFontBold(CStr(g_fontName), 9.0!)
					End If
					If br.IsContentCenter OrElse br.IsLocalStandard Then
						' 标准件/中心件行：自定义已统一文字色，其它主题灰色
						If g_theme = 8 Then
							rowSt.BackColor = g_custRowWhite
						Else
							rowSt.BackColor = System.Drawing.Color.White
							rowSt.ForeColor = C(160, 160, 160)
						End If
					End If
					' 丢失行 cell 级紫底白字由刷新时设置，此处不动（保持警示样式）
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				End Try
			Next
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Function ShowCustomThemeDialog() As Boolean
		Try
			Dim dlg As New Form()
			dlg.Text = "自定义主题颜色"
			dlg.FormBorderStyle = FormBorderStyle.FixedDialog
			dlg.StartPosition = FormStartPosition.CenterScreen
			dlg.ClientSize = New System.Drawing.Size(Scaled(780), Scaled(600))
			dlg.MinimizeBox = False
			dlg.MaximizeBox = False
			dlg.Font = MakeFont(CStr(g_fontName), 9.0!)
			dlg.BackColor = C(248, 248, 250)
			Try
				If g_appIcon IsNot Nothing Then dlg.Icon = g_appIcon
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			' ── 10 个设置项 ──
			Dim cols(9) As System.Drawing.Color
			cols(0) = g_custTitleBar
			cols(1) = g_custColHeader
			cols(2) = g_custColMgr
			cols(3) = g_custFuncDrop
			cols(4) = g_custWinBg
			cols(5) = g_custExcelBtn
			cols(6) = g_custRefresh
			cols(7) = g_custRowAlt
			cols(8) = g_custRowWhite
			cols(9) = g_custCellText
			Dim names() As String = {"上下标题（顶部栏/底部栏）", "表格列头", "列管理控件", "功能下拉", "窗口背景色", "Excel导出按钮", "刷新按钮", "间隔行颜色（条纹）", "白底行颜色", "表格文字颜色"}
			Dim selIdx As Integer = 0
			Dim rowLbls(9) As Label
			Dim rowSwatches(9) As Button
			Dim curHue As Single = 0.0F, curS As Single = 1.0F, curV As Single = 1.0F
			Dim updatingRGB As Boolean = False
			' 回调委托先声明（本编译器要求"先声明后引用"）
			Dim RefreshRows As System.Action = Nothing
			Dim SetCurrentColor As System.Action(Of System.Drawing.Color) = Nothing
			Dim UpdateSV As System.Action(Of Integer, Integer) = Nothing
			Dim UpdateHue As System.Action(Of Integer) = Nothing
			Dim ApplyAll As System.Action = Nothing
			RgbToHsv(cols(0), curHue, curS, curV)
			' ── 当前设置项指示（告知正在改哪个控件）──
			Dim lblCur As New Label()
			lblCur.Location = New System.Drawing.Point(Scaled(370), Scaled(12))
			lblCur.Size = New System.Drawing.Size(Scaled(400), Scaled(20))
			lblCur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			lblCur.ForeColor = C(0, 102, 204)
			lblCur.Font = MakeFontBold(CStr(g_fontName), 9.0!)
			dlg.Controls.Add(lblCur)
			' ── 左侧设置行 ──
			For i As Integer = 0 To 9
				Dim iCopy As Integer = i
				Dim lbl As New Label()
				lbl.Text = names(i)
				lbl.Location = New System.Drawing.Point(Scaled(16), Scaled(16) + i * Scaled(30))
				lbl.Size = New System.Drawing.Size(Scaled(212), Scaled(26))
				lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
				lbl.Cursor = Cursors.Hand
				AddHandler lbl.Click, Sub(s2 As Object, e2 As EventArgs)
										  selIdx = iCopy
										  SetCurrentColor(cols(selIdx))
										  RefreshRows()
									  End Sub
				dlg.Controls.Add(lbl)
				rowLbls(i) = lbl
				Dim sw As New Button()
				sw.Location = New System.Drawing.Point(Scaled(234), Scaled(16) + i * Scaled(30))
				sw.Size = New System.Drawing.Size(Scaled(110), Scaled(26))
				sw.FlatStyle = FlatStyle.Flat
				sw.Text = ""
				sw.BackColor = cols(i)
				sw.Cursor = Cursors.Hand
				AddHandler sw.Click, Sub(s2 As Object, e2 As EventArgs)
										 selIdx = iCopy
										 SetCurrentColor(cols(selIdx))
										 RefreshRows()
									 End Sub
				dlg.Controls.Add(sw)
				rowSwatches(i) = sw
			Next
			RefreshRows = Sub()
							  Try
								  lblCur.Text = "当前设置：" & names(selIdx)
							  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
							  End Try
							  For ri As Integer = 0 To 9
								  Try
									  If ri = selIdx Then
										  rowLbls(ri).ForeColor = C(0, 102, 204)
										  rowLbls(ri).Font = MakeFontBold(CStr(g_fontName), 9.0!)
										  rowSwatches(ri).FlatAppearance.BorderSize = 2
										  rowSwatches(ri).FlatAppearance.BorderColor = C(0, 102, 204)
									  Else
										  rowLbls(ri).ForeColor = System.Drawing.Color.Black
										  rowLbls(ri).Font = MakeFont(CStr(g_fontName), 9.0!)
										  rowSwatches(ri).FlatAppearance.BorderSize = 1
										  rowSwatches(ri).FlatAppearance.BorderColor = System.Drawing.Color.Gray
									  End If
								  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
								  End Try
							  Next
						  End Sub
			' ── 右侧取色区：S/V 渐变方块 + 色相条（点击/拖动直接取色）──
			Dim pnlSV As New Panel()
			pnlSV.Location = New System.Drawing.Point(Scaled(370), Scaled(36))
			pnlSV.Size = New System.Drawing.Size(Scaled(260), Scaled(260))
			pnlSV.BackColor = System.Drawing.Color.White
			pnlSV.BorderStyle = BorderStyle.FixedSingle
			dlg.Controls.Add(pnlSV)
			Dim pnlHue As New Panel()
			pnlHue.Location = New System.Drawing.Point(Scaled(370) + Scaled(260) + Scaled(10), Scaled(36))
			pnlHue.Size = New System.Drawing.Size(Scaled(28), Scaled(260))
			pnlHue.BorderStyle = BorderStyle.FixedSingle
			dlg.Controls.Add(pnlHue)
			' 双缓冲：消除拖动取色时的闪烁
			Try
				Dim st As System.Type = pnlSV.GetType()
				Dim setStM As System.Reflection.MethodInfo = st.GetMethod("SetStyle", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
				Dim flags As System.Windows.Forms.ControlStyles = CType([Enum].Parse(GetType(System.Windows.Forms.ControlStyles), "AllPaintingInWmPaint, OptimizedDoubleBuffer, UserPaint, ResizeRedraw"), System.Windows.Forms.ControlStyles)
				If setStM IsNot Nothing Then
					setStM.Invoke(pnlSV, New Object() {flags, True})
					setStM.Invoke(pnlHue, New Object() {flags, True})
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			Dim lblHint As New Label()
			lblHint.Text = "点击 / 拖动取色，双击直接确定"
			lblHint.Location = New System.Drawing.Point(Scaled(370), Scaled(302))
			lblHint.Size = New System.Drawing.Size(Scaled(300), Scaled(18))
			lblHint.ForeColor = System.Drawing.Color.Gray
			dlg.Controls.Add(lblHint)
			AddHandler pnlSV.Paint, Sub(s As Object, e As System.Windows.Forms.PaintEventArgs)
										Dim p As Panel = CType(s, Panel)
										If p.Width <= 0 OrElse p.Height <= 0 Then Return
										Dim rect As New System.Drawing.Rectangle(0, 0, p.Width, p.Height)
										Using br As New System.Drawing.Drawing2D.LinearGradientBrush(rect, System.Drawing.Color.White, HsvToColor(curHue, 1.0F, 1.0F), System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
											e.Graphics.FillRectangle(br, rect)
										End Using
										Using br2 As New System.Drawing.Drawing2D.LinearGradientBrush(rect, System.Drawing.Color.FromArgb(0, 0, 0, 0), System.Drawing.Color.Black, System.Drawing.Drawing2D.LinearGradientMode.Vertical)
											e.Graphics.FillRectangle(br2, rect)
										End Using
										Dim px As Integer = CInt(curS * p.Width)
										Dim py As Integer = CInt((1.0F - curV) * p.Height)
										Using penW As New System.Drawing.Pen(System.Drawing.Color.White, 2.0F)
											e.Graphics.DrawEllipse(penW, px - 6, py - 6, 12, 12)
										End Using
										Using penB As New System.Drawing.Pen(System.Drawing.Color.Black, 1.0F)
											e.Graphics.DrawEllipse(penB, px - 6, py - 6, 12, 12)
										End Using
									End Sub
			AddHandler pnlHue.Paint, Sub(s As Object, e As System.Windows.Forms.PaintEventArgs)
										 Dim p As Panel = CType(s, Panel)
										 If p.Width <= 0 OrElse p.Height <= 0 Then Return
										 Dim rect As New System.Drawing.Rectangle(0, 0, p.Width, p.Height)
										 Using lb As New System.Drawing.Drawing2D.LinearGradientBrush(rect, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Drawing2D.LinearGradientMode.Vertical)
											 Dim bl As New System.Drawing.Drawing2D.ColorBlend()
											 bl.Colors = New System.Drawing.Color() {System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Lime, System.Drawing.Color.Cyan, System.Drawing.Color.Blue, System.Drawing.Color.Magenta, System.Drawing.Color.Red}
											 bl.Positions = New Single() {0.0F, 1.0F / 6.0F, 2.0F / 6.0F, 3.0F / 6.0F, 4.0F / 6.0F, 5.0F / 6.0F, 1.0F}
											 lb.InterpolationColors = bl
											 e.Graphics.FillRectangle(lb, rect)
										 End Using
										 Dim py As Integer = CInt((curHue / 360.0F) * p.Height)
										 Using penW As New System.Drawing.Pen(System.Drawing.Color.White, 2.0F)
											 e.Graphics.DrawLine(penW, 0, py, p.Width, py)
										 End Using
										 Using penB As New System.Drawing.Pen(System.Drawing.Color.Black, 1.0F)
											 e.Graphics.DrawLine(penB, 0, py, p.Width, py)
										 End Using
									 End Sub
			Dim svDragging As Boolean = False
			AddHandler pnlSV.MouseDown, Sub(s As Object, e As System.Windows.Forms.MouseEventArgs)
											If e.Button <> System.Windows.Forms.MouseButtons.Left Then Return
											svDragging = True
											pnlSV.Capture = True
											UpdateSV(e.X, e.Y)
										End Sub
			AddHandler pnlSV.MouseMove, Sub(s As Object, e As System.Windows.Forms.MouseEventArgs)
											If svDragging Then UpdateSV(e.X, e.Y)
										End Sub
			AddHandler pnlSV.MouseUp, Sub(s As Object, e As System.Windows.Forms.MouseEventArgs)
										  svDragging = False
										  pnlSV.Capture = False
									  End Sub
			AddHandler pnlSV.DoubleClick, Sub(s2 As Object, e2 As EventArgs)
											  ApplyAll()
											  dlg.DialogResult = DialogResult.OK
											  dlg.Close()
										  End Sub
			AddHandler pnlHue.MouseDown, Sub(s As Object, e As System.Windows.Forms.MouseEventArgs)
											 If e.Button <> System.Windows.Forms.MouseButtons.Left Then Return
											 pnlHue.Capture = True
											 UpdateHue(e.Y)
										 End Sub
			AddHandler pnlHue.MouseMove, Sub(s As Object, e As System.Windows.Forms.MouseEventArgs)
											 If e.Button = System.Windows.Forms.MouseButtons.Left Then UpdateHue(e.Y)
										 End Sub
			AddHandler pnlHue.DoubleClick, Sub(s2 As Object, e2 As EventArgs)
											   ApplyAll()
											   dlg.DialogResult = DialogResult.OK
											   dlg.Close()
										   End Sub
			UpdateSV = Sub(x As Integer, y As Integer)
						   If pnlSV.Width <= 0 OrElse pnlSV.Height <= 0 Then Return
						   If x < 0 Then x = 0
						   If y < 0 Then y = 0
						   If x > pnlSV.Width Then x = pnlSV.Width
						   If y > pnlSV.Height Then y = pnlSV.Height
						   curS = CSng(x) / CSng(pnlSV.Width)
						   curV = 1.0F - CSng(y) / CSng(pnlSV.Height)
						   SetCurrentColor(HsvToColor(curHue, curS, curV))
					   End Sub
			UpdateHue = Sub(y As Integer)
							If pnlHue.Height <= 0 Then Return
							If y < 0 Then y = 0
							If y > pnlHue.Height Then y = pnlHue.Height
							curHue = CSng(y) / CSng(pnlHue.Height) * 360.0F
							SetCurrentColor(HsvToColor(curHue, curS, curV))
						End Sub
			' ── RGB 输入与预览 ──
			Dim lblR As New Label()
			lblR.Text = "R"
			lblR.Location = New System.Drawing.Point(Scaled(370), Scaled(336))
			lblR.Size = New System.Drawing.Size(Scaled(18), Scaled(22))
			lblR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			dlg.Controls.Add(lblR)
			Dim txtR As New System.Windows.Forms.TextBox()
			txtR.Location = New System.Drawing.Point(Scaled(390), Scaled(334))
			txtR.Size = New System.Drawing.Size(Scaled(46), Scaled(24))
			txtR.MaxLength = 3
			dlg.Controls.Add(txtR)
			Dim lblG As New Label()
			lblG.Text = "G"
			lblG.Location = New System.Drawing.Point(Scaled(450), Scaled(336))
			lblG.Size = New System.Drawing.Size(Scaled(18), Scaled(22))
			lblG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			dlg.Controls.Add(lblG)
			Dim txtG As New System.Windows.Forms.TextBox()
			txtG.Location = New System.Drawing.Point(Scaled(470), Scaled(334))
			txtG.Size = New System.Drawing.Size(Scaled(46), Scaled(24))
			txtG.MaxLength = 3
			dlg.Controls.Add(txtG)
			Dim lblB As New Label()
			lblB.Text = "B"
			lblB.Location = New System.Drawing.Point(Scaled(530), Scaled(336))
			lblB.Size = New System.Drawing.Size(Scaled(18), Scaled(22))
			lblB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			dlg.Controls.Add(lblB)
			Dim txtB As New System.Windows.Forms.TextBox()
			txtB.Location = New System.Drawing.Point(Scaled(550), Scaled(334))
			txtB.Size = New System.Drawing.Size(Scaled(46), Scaled(24))
			txtB.MaxLength = 3
			dlg.Controls.Add(txtB)
			Dim pnlPrev As New Panel()
			pnlPrev.Location = New System.Drawing.Point(Scaled(610), Scaled(332))
			pnlPrev.Size = New System.Drawing.Size(Scaled(70), Scaled(28))
			pnlPrev.BackColor = cols(0)
			pnlPrev.BorderStyle = BorderStyle.FixedSingle
			dlg.Controls.Add(pnlPrev)
			Dim lblHex As New Label()
			lblHex.Location = New System.Drawing.Point(Scaled(370), Scaled(366))
			lblHex.Size = New System.Drawing.Size(Scaled(180), Scaled(20))
			lblHex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			dlg.Controls.Add(lblHex)
			SetCurrentColor = Sub(nc As System.Drawing.Color)
								  RgbToHsv(nc, curHue, curS, curV)
								  cols(selIdx) = nc
								  rowSwatches(selIdx).BackColor = nc
								  pnlPrev.BackColor = nc
								  lblHex.Text = "#" & nc.R.ToString("X2") & nc.G.ToString("X2") & nc.B.ToString("X2")
								  updatingRGB = True
								  Try
									  txtR.Text = nc.R.ToString()
									  txtG.Text = nc.G.ToString()
									  txtB.Text = nc.B.ToString()
								  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
								  End Try
								  updatingRGB = False
								  pnlSV.Invalidate()
								  pnlHue.Invalidate()
							  End Sub
			AddHandler txtR.TextChanged, Sub(s2 As Object, e2 As EventArgs)
											 If updatingRGB Then Return
											 Dim rr, gg, bb As Integer
											 If Integer.TryParse(txtR.Text.Trim(), rr) AndAlso Integer.TryParse(txtG.Text.Trim(), gg) AndAlso Integer.TryParse(txtB.Text.Trim(), bb) Then
												 If rr >= 0 AndAlso rr <= 255 AndAlso gg >= 0 AndAlso gg <= 255 AndAlso bb >= 0 AndAlso bb <= 255 Then
													 SetCurrentColor(System.Drawing.Color.FromArgb(rr, gg, bb))
												 End If
											 End If
										 End Sub
			AddHandler txtG.TextChanged, Sub(s2 As Object, e2 As EventArgs)
											 If updatingRGB Then Return
											 Dim rr, gg, bb As Integer
											 If Integer.TryParse(txtR.Text.Trim(), rr) AndAlso Integer.TryParse(txtG.Text.Trim(), gg) AndAlso Integer.TryParse(txtB.Text.Trim(), bb) Then
												 If rr >= 0 AndAlso rr <= 255 AndAlso gg >= 0 AndAlso gg <= 255 AndAlso bb >= 0 AndAlso bb <= 255 Then
													 SetCurrentColor(System.Drawing.Color.FromArgb(rr, gg, bb))
												 End If
											 End If
										 End Sub
			AddHandler txtB.TextChanged, Sub(s2 As Object, e2 As EventArgs)
											 If updatingRGB Then Return
											 Dim rr, gg, bb As Integer
											 If Integer.TryParse(txtR.Text.Trim(), rr) AndAlso Integer.TryParse(txtG.Text.Trim(), gg) AndAlso Integer.TryParse(txtB.Text.Trim(), bb) Then
												 If rr >= 0 AndAlso rr <= 255 AndAlso gg >= 0 AndAlso gg <= 255 AndAlso bb >= 0 AndAlso bb <= 255 Then
													 SetCurrentColor(System.Drawing.Color.FromArgb(rr, gg, bb))
												 End If
											 End If
										 End Sub
			' ── 快速常用色（点击即应用到当前设置项）──
			Dim lblQuick As New Label()
			lblQuick.Text = "快速取色："
			lblQuick.Location = New System.Drawing.Point(Scaled(16), Scaled(436))
			lblQuick.Size = New System.Drawing.Size(Scaled(90), Scaled(18))
			dlg.Controls.Add(lblQuick)
			Dim quickCols() As System.Drawing.Color = {
				System.Drawing.Color.FromArgb(255, 255, 255), System.Drawing.Color.FromArgb(0, 0, 0), System.Drawing.Color.FromArgb(128, 128, 128), System.Drawing.Color.FromArgb(64, 64, 64),
				System.Drawing.Color.FromArgb(220, 60, 70), System.Drawing.Color.FromArgb(255, 140, 0), System.Drawing.Color.FromArgb(255, 215, 0), System.Drawing.Color.FromArgb(30, 140, 110),
				System.Drawing.Color.FromArgb(0, 150, 200), System.Drawing.Color.FromArgb(0, 51, 255), System.Drawing.Color.FromArgb(108, 92, 231), System.Drawing.Color.FromArgb(230, 80, 160),
				System.Drawing.Color.FromArgb(243, 240, 255), System.Drawing.Color.FromArgb(90, 95, 115), System.Drawing.Color.FromArgb(0, 100, 190), System.Drawing.Color.FromArgb(139, 69, 19)
			}
			Dim qCellW As Integer = Scaled(52), qCellH As Integer = Scaled(24), qGap As Integer = Scaled(4)
			For qi As Integer = 0 To quickCols.Length - 1
				Dim qcol As System.Drawing.Color = quickCols(qi)
				Dim qBtn As New Button()
				qBtn.Location = New System.Drawing.Point(Scaled(16) + (qi Mod 8) * (qCellW + qGap), Scaled(454) + (qi \ 8) * (qCellH + qGap))
				qBtn.Size = New System.Drawing.Size(qCellW, qCellH)
				qBtn.FlatStyle = FlatStyle.Flat
				qBtn.BackColor = qcol
				AddHandler qBtn.Click, Sub(s2 As Object, e2 As EventArgs)
										   SetCurrentColor(qcol)
									   End Sub
				dlg.Controls.Add(qBtn)
			Next
			' ── 底部按钮 ──
			Dim btnReset As New Button()
			btnReset.Text = "恢复默认"
			btnReset.Location = New System.Drawing.Point(Scaled(16), Scaled(548))
			btnReset.Size = New System.Drawing.Size(Scaled(110), Scaled(34))
			btnReset.FlatStyle = FlatStyle.Flat
			btnReset.BackColor = C(140, 140, 150)
			btnReset.ForeColor = System.Drawing.Color.White
			btnReset.Cursor = Cursors.Hand
			AddHandler btnReset.Click, Sub(s2 As Object, e2 As EventArgs)
										   cols(0) = System.Drawing.Color.FromArgb(108, 92, 231)
										   cols(1) = System.Drawing.Color.FromArgb(108, 92, 231)
										   cols(2) = System.Drawing.Color.FromArgb(243, 240, 255)
										   cols(3) = System.Drawing.Color.FromArgb(228, 222, 252)
										   cols(4) = System.Drawing.Color.FromArgb(245, 243, 255)
										   cols(5) = System.Drawing.Color.FromArgb(159, 149, 239)
										   cols(6) = System.Drawing.Color.FromArgb(225, 112, 85)
										   cols(7) = System.Drawing.Color.FromArgb(253, 252, 255)
										   cols(8) = System.Drawing.Color.FromArgb(255, 255, 255)
										   cols(9) = System.Drawing.Color.FromArgb(0, 0, 0)
										   For ri As Integer = 0 To 9
											   rowSwatches(ri).BackColor = cols(ri)
										   Next
										   SetCurrentColor(cols(selIdx))
										   RefreshRows()
									   End Sub
			dlg.Controls.Add(btnReset)
			ApplyAll = Sub()
						   g_custTitleBar = cols(0)
						   g_custColHeader = cols(1)
						   g_custColMgr = cols(2)
						   g_custFuncDrop = cols(3)
						   g_custWinBg = cols(4)
						   g_custExcelBtn = cols(5)
						   g_custRefresh = cols(6)
						   g_custRowAlt = cols(7)
						   g_custRowWhite = cols(8)
						   g_custCellText = cols(9)
					   End Sub
			Dim btnOk As New Button()
			btnOk.Text = "确定"
			btnOk.Location = New System.Drawing.Point(Scaled(566), Scaled(548))
			btnOk.Size = New System.Drawing.Size(Scaled(96), Scaled(34))
			btnOk.FlatStyle = FlatStyle.Flat
			btnOk.BackColor = OrangeMid()
			btnOk.ForeColor = System.Drawing.Color.White
			btnOk.Cursor = Cursors.Hand
			AddHandler btnOk.Click, Sub(s2 As Object, e2 As EventArgs)
										ApplyAll()
										dlg.DialogResult = DialogResult.OK
										dlg.Close()
									End Sub
			dlg.Controls.Add(btnOk)
			Dim btnCancel As New Button()
			btnCancel.Text = "取消"
			btnCancel.Location = New System.Drawing.Point(Scaled(672), Scaled(548))
			btnCancel.Size = New System.Drawing.Size(Scaled(96), Scaled(34))
			btnCancel.FlatStyle = FlatStyle.Flat
			btnCancel.BackColor = C(120, 120, 130)
			btnCancel.ForeColor = System.Drawing.Color.White
			btnCancel.Cursor = Cursors.Hand
			AddHandler btnCancel.Click, Sub(s2 As Object, e2 As EventArgs)
											dlg.DialogResult = DialogResult.Cancel
											dlg.Close()
										End Sub
			dlg.Controls.Add(btnCancel)
			SetCurrentColor(cols(selIdx))
			RefreshRows()
			Return (dlg.ShowDialog() = DialogResult.OK)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return False
		End Try
	End Function
	' RGB → HSV 辅助（取色光标定位用）
	Sub RgbToHsv(c As System.Drawing.Color, ByRef h As Single, ByRef s As Single, ByRef v As Single)
		Dim r As Single = CSng(c.R) / 255.0F
		Dim g As Single = CSng(c.G) / 255.0F
		Dim b As Single = CSng(c.B) / 255.0F
		Dim mx As Single = Math.Max(r, Math.Max(g, b))
		Dim mn As Single = Math.Min(r, Math.Min(g, b))
		Dim d As Single = mx - mn
		v = mx
		If mx > 0.0F Then s = d / mx Else s = 0.0F
		If d = 0.0F Then
			h = 0.0F
		ElseIf mx = r Then
			h = 60.0F * (((g - b) / d) Mod 6.0F)
		ElseIf mx = g Then
			h = 60.0F * ((b - r) / d + 2.0F)
		Else
			h = 60.0F * ((r - g) / d + 4.0F)
		End If
		If h < 0.0F Then h += 360.0F
	End Sub

	' ═══ 呼吸灯彩虹主题 ═══
	Function HsvToColor(h As Single, sat As Single, val As Single) As System.Drawing.Color
		h = ((h Mod 360.0F) + 360.0F) Mod 360.0F
		Dim c As Single = val * sat
		Dim x As Single = c * (1.0F - Math.Abs((h / 60.0F) Mod 2.0F - 1.0F))
		Dim m As Single = val - c
		Dim r As Single = 0.0F, g As Single = 0.0F, b As Single = 0.0F
		If h < 60.0F Then
			r = c : g = x : b = 0.0F
		ElseIf h < 120.0F Then
			r = x : g = c : b = 0.0F
		ElseIf h < 180.0F Then
			r = 0.0F : g = c : b = x
		ElseIf h < 240.0F Then
			r = 0.0F : g = x : b = c
		ElseIf h < 300.0F Then
			r = x : g = 0.0F : b = c
		Else
			r = c : g = 0.0F : b = x
		End If
		Return System.Drawing.Color.FromArgb(255, CInt((r + m) * 255.0F), CInt((g + m) * 255.0F), CInt((b + m) * 255.0F))
	End Function

	Sub StartBreathTheme()
		Try
			If breathTimer Is Nothing Then
				breathTimer = New System.Windows.Forms.Timer()
				breathTimer.Interval = 60
				AddHandler breathTimer.Tick, AddressOf BreathTick
			End If
			If g_pnlTop IsNot Nothing Then TransparentizeTop(g_pnlTop)
			If g_pnlBottom IsNot Nothing Then TransparentizeTop(g_pnlBottom)
			' 一次性设置固定彩虹列头/淡彩背景
			Try
				If g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
					Dim visCols As Integer = 0
					For Each gc As System.Windows.Forms.DataGridViewColumn In g_grid.Columns
						If gc.Visible Then visCols += 1
					Next
					If visCols > 0 Then
						Dim ci As Integer = 0
						For Each gc As System.Windows.Forms.DataGridViewColumn In g_grid.Columns
							If Not gc.Visible Then Continue For
							Dim h As Single = CSng(ci) * 360.0F / CSng(visCols)
							gc.HeaderCell.Style.BackColor = HsvToColor(h, 0.4F, 0.92F)
							ci += 1
						Next
					End If
					g_grid.AlternatingRowsDefaultCellStyle.BackColor = C(238, 240, 248)
					g_grid.BackgroundColor = C(248, 248, 252)
				End If
				If g_chkPanel IsNot Nothing Then g_chkPanel.BackColor = PanelBg()
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			breathTimer.Start()
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub TransparentizeTop(parent As System.Windows.Forms.Control)
		Try
			For Each c As System.Windows.Forms.Control In parent.Controls
				Try
					c.BackColor = System.Drawing.Color.Transparent
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				End Try
				If c.HasChildren Then TransparentizeTop(c)
			Next
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub StopBreathTheme()
		Try
			If breathTimer IsNot Nothing Then breathTimer.Stop()
			' 复位顶/底条底色并清掉残留渐变
			If g_pnlTop IsNot Nothing Then
				g_pnlTop.BackColor = OrangeMid()
				g_pnlTop.Invalidate()
			End If
			If g_pnlBottom IsNot Nothing Then
				g_pnlBottom.BackColor = ToolBarBg()
				g_pnlBottom.Invalidate()
			End If
			Try
				g_chkPanel.BackColor = PanelBg()
				g_chkPanel.Invalidate()
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			' 清掉列头单独设的彩虹色，让默认主题色生效
			Try
				If g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
					For Each gc As System.Windows.Forms.DataGridViewColumn In g_grid.Columns
						gc.HeaderCell.Style.BackColor = System.Drawing.Color.Empty
					Next
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub BreathTick(sender As Object, e As EventArgs)
		Try
			breathHue = (breathHue + 1.2F) Mod 360.0F
			If g_pnlTop IsNot Nothing Then g_pnlTop.Invalidate()
			If g_pnlBottom IsNot Nothing Then g_pnlBottom.Invalidate()
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub PnlTopPaint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
		Try
			If g_theme <> 6 Then Return
			Dim p As System.Windows.Forms.Panel = CType(sender, System.Windows.Forms.Panel)
			If p.Width <= 0 OrElse p.Height <= 0 Then Return
			Dim rect As New System.Drawing.Rectangle(0, 0, p.Width, p.Height)
			Dim c1 As System.Drawing.Color = HsvToColor(breathHue, 0.72F, 0.95F)
			Dim c2 As System.Drawing.Color = HsvToColor((breathHue + 60.0F) Mod 360.0F, 0.72F, 0.95F)
			Dim c3 As System.Drawing.Color = HsvToColor((breathHue + 140.0F) Mod 360.0F, 0.72F, 0.95F)
			Using br As New System.Drawing.Drawing2D.LinearGradientBrush(rect, c1, c3, 0.0F)
				Dim bl As New System.Drawing.Drawing2D.ColorBlend()
				bl.Colors = New System.Drawing.Color() {c1, c2, c3}
				bl.Positions = New Single() {0.0F, 0.5F, 1.0F}
				br.InterpolationColors = bl
				e.Graphics.FillRectangle(br, rect)
			End Using
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub PnlBottomPaint(sender As Object, e As System.Windows.Forms.PaintEventArgs)
		Try
			If g_theme <> 6 Then Return
			Dim p As System.Windows.Forms.Panel = CType(sender, System.Windows.Forms.Panel)
			If p.Width <= 0 OrElse p.Height <= 0 Then Return
			Dim rect As New System.Drawing.Rectangle(0, 0, p.Width, p.Height)
			Dim c1 As System.Drawing.Color = HsvToColor(breathHue, 0.65F, 0.92F)
			Dim c2 As System.Drawing.Color = HsvToColor((breathHue + 60.0F) Mod 360.0F, 0.65F, 0.92F)
			Dim c3 As System.Drawing.Color = HsvToColor((breathHue + 140.0F) Mod 360.0F, 0.65F, 0.92F)
			Using br As New System.Drawing.Drawing2D.LinearGradientBrush(rect, c1, c3, 0.0F)
				Dim bl As New System.Drawing.Drawing2D.ColorBlend()
				bl.Colors = New System.Drawing.Color() {c1, c2, c3}
				bl.Positions = New Single() {0.0F, 0.5F, 1.0F}
				br.InterpolationColors = bl
				e.Graphics.FillRectangle(br, rect)
			End Using
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Function CheckEnvironment() As Boolean
		Dim sb As New System.Text.StringBuilder()
		sb.AppendLine("BOM工具 - 环境检测报告")
		sb.AppendLine("═══════════════════════════════")
		Dim clrVer As String = System.Environment.Version.ToString()
		sb.AppendLine("CLR: " & clrVer)
		Dim invVer As String = ""
		Try : invVer = ThisApplication.SoftwareVersion.DisplayVersion : Catch : invVer = "未知" : End Try
		sb.AppendLine("Inventor: " & invVer)
		Dim allOk As Boolean = True
		Try
			Dim testLbl As New System.Windows.Forms.Label()
			testLbl.Dispose()
			sb.AppendLine("System.Windows.Forms: OK")
		Catch ex As Exception
			sb.AppendLine("System.Windows.Forms: 不可用 - " & ex.Message) : allOk = False
		End Try
		Try
			Dim testBmp As New System.Drawing.Bitmap(1, 1)
			testBmp.Dispose()
			sb.AppendLine("System.Drawing: OK")
		Catch ex As Exception
			sb.AppendLine("System.Drawing: 不可用 - " & ex.Message) : allOk = False
		End Try
		Dim secOn As Boolean = False
		Try
			secOn = ThisApplication.iLogicOptions.EnableRuleSecurityInspection
		Catch : End Try
		If secOn Then
			sb.AppendLine("iLogic 安全扫描: 已启用 - 必须关闭")
			allOk = False
		Else
			sb.AppendLine("iLogic 安全扫描: 已关闭 OK")
		End If
		If Not allOk Then
			System.Windows.Forms.MessageBox.Show(sb.ToString(), "BOM工具 - 环境检测失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
			Return False
		End If
		Return True
	End Function

	' Shell32 缩略图：纯 Win32 API，不经过 Inventor COM，完全不转圈
	' ── 从 Inventor 文件二进制流直接提取内置预览图（PNG），不需要打开文档，瞬间完成 ──
	Function GetInventorThumbnail(filePath As String) As System.Drawing.Image
		Try
			' ★ 先查磁盘缓存（避免每次重新解析文件）
			Dim cacheDir As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DWJ_ThumbCache")
			If Not System.IO.Directory.Exists(cacheDir) Then System.IO.Directory.CreateDirectory(cacheDir)
			Dim cacheKey As String = System.IO.Path.GetFileNameWithoutExtension(filePath) & "_" & New System.IO.FileInfo(filePath).LastWriteTime.Ticks.ToString() & ".png"
			Dim cachePath As String = System.IO.Path.Combine(cacheDir, cacheKey)
			If System.IO.File.Exists(cachePath) Then
				Try : Return New System.Drawing.Bitmap(cachePath) : Catch : End Try
			End If
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
			' ★ 写磁盘缓存
			Try : bmp.Save(cachePath, System.Drawing.Imaging.ImageFormat.Png) : Catch : End Try
			Return bmp
		Catch : End Try
		Return Nothing
	End Function

	Function GetShellThumbnail(filePath As String) As System.Drawing.Image
		Try
			If String.IsNullOrEmpty(filePath) OrElse Not System.IO.File.Exists(filePath) Then Return Nothing
			Dim iid As New System.Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")
			Dim factory As IShellItemImageFactory = Nothing
			Dim hr As Integer = SHCreateItemFromParsingName(filePath, IntPtr.Zero, iid, factory)
			If hr = 0 AndAlso factory IsNot Nothing Then
				Dim hBmp As IntPtr = IntPtr.Zero
				Dim sz As New System.Drawing.Size(THUMB_W, THUMB_H)
				Dim hr2 As Integer = factory.GetImage(sz, 0UI, hBmp)
				If hr2 = 0 AndAlso hBmp <> IntPtr.Zero Then
					Dim rawBmp As System.Drawing.Bitmap = System.Drawing.Image.FromHbitmap(hBmp)
					DeleteObject(hBmp)
					Dim bmp As New System.Drawing.Bitmap(THUMB_W, THUMB_H)
					Dim gfx As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
					gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
					gfx.Clear(System.Drawing.Color.White)
					gfx.DrawImage(rawBmp, 0, 0, THUMB_W, THUMB_H)
					gfx.Dispose() : rawBmp.Dispose()
					Return bmp
				End If
			End If
		Catch : End Try
		Return Nothing
	End Function

	' 缩略图：SaveAs PNG 截图，SilentOperation 全程静默，不转圈
	Function GetHQThumbnail(filePath As String) As System.Drawing.Image
		Try
			If String.IsNullOrEmpty(filePath) Then Return Nothing
			' 找已打开的文档
			Dim tDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If String.Compare(od.FullDocumentName, filePath, True) = 0 Then
					tDoc = od : Exit For
				End If
			Next
			' 未打开则静默打开（不转圈）
			If tDoc Is Nothing AndAlso System.IO.File.Exists(filePath) Then
				Try
					ThisApplication.SilentOperation = True
					tDoc = ThisApplication.Documents.Open(filePath, False)
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				Finally
					ThisApplication.SilentOperation = False
				End Try
			End If
			If tDoc Is Nothing Then Return Nothing
			' SaveAs PNG 静默截图
			Dim tmpDir As String = "C:\DWJ_BOM_TEMP"
			If Not System.IO.Directory.Exists(tmpDir) Then System.IO.Directory.CreateDirectory(tmpDir)
			Dim tmpPng As String = System.IO.Path.Combine(tmpDir,
			System.IO.Path.GetFileNameWithoutExtension(filePath) & ".png")
			Try
				ThisApplication.SilentOperation = True
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
				tDoc.SaveAs(tmpPng, True)
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Finally
				ThisApplication.SilentOperation = False
			End Try
			If Not System.IO.File.Exists(tmpPng) Then Return Nothing
			' 读取 PNG，等比缩放居中，白底
			Dim rb As New System.Drawing.Bitmap(tmpPng)
			Dim bmp As New System.Drawing.Bitmap(THUMB_W, THUMB_H)
			Dim gfx As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
			gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
			gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
			gfx.Clear(System.Drawing.Color.White)
			Dim sc As Double = Math.Min(CDbl(THUMB_W) / rb.Width, CDbl(THUMB_H) / rb.Height)
			Dim dW As Integer = CInt(rb.Width * sc)
			Dim dH As Integer = CInt(rb.Height * sc)
			gfx.DrawImage(rb, CInt((THUMB_W - dW) \ 2), CInt((THUMB_H - dH) \ 2), dW, dH)
			gfx.Dispose() : rb.Dispose()
			Return bmp
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
		Return Nothing
	End Function

	Sub CleanThumbnailTemp()
		Try
			Dim tmpDir As String = "C:\DWJ_BOM_TEMP"
			If System.IO.Directory.Exists(tmpDir) Then
				System.IO.Directory.Delete(tmpDir, True)
			End If
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub OnColumnOrderChanged(sender As Object, e As DataGridViewColumnEventArgs)
		' 防止冻结列被拖动改变位置
		If e.Column.Frozen Then
			' 冻结列不应该被移动，忽略
			Return
		End If
		' 左边列顺序改变，同步右边列管理顺序
		SyncChkFlowOrder()
		' 加载设置中不触发保存
		If g_isLoading Then Return
		SaveSettings()
	End Sub

	Sub OnColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs)
		' 加载设置中不触发保存（避免覆盖正在恢复的值）
		If g_isLoading Then Return
		SaveSettings()
	End Sub

	Sub OnCellMouseEnter(sender As Object, e As DataGridViewCellEventArgs)
		If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
		' ── 缩略图列：悬停弹出大图 ──
		If g_grid.Columns.Contains("cThumb") AndAlso
			g_grid.Columns("cThumb").Index = e.ColumnIndex Then
			' 每次进入都重新取当前行缩略图，防止滚动后行内容错位
			CloseThumbPopup()
			Dim origIdx2 As Integer = GetOrigRowIdx(e.RowIndex)
			If origIdx2 >= 0 AndAlso origIdx2 < g_rows.Count Then
				Dim thumb As System.Drawing.Image = g_rows(origIdx2).Thumbnail
				If thumb IsNot Nothing Then
					ShowThumbPopup(thumb, e.RowIndex)
				End If
			End If
			Return
		End If
		' ── 原有 cType 列部件高亮逻辑 ──
		If Not g_grid.Columns.Contains("cType") Then Return
		If g_grid.Columns("cType").Index <> e.ColumnIndex Then Return
		Dim origIdx As Integer = GetOrigRowIdx(e.RowIndex)
		If origIdx < 0 OrElse origIdx >= g_rows.Count Then Return
		Dim r As BomDataRow = g_rows(origIdx)
		If r.DocType <> "部件" Then Return
		' 有锁定时不覆盖
		If Not String.IsNullOrEmpty(g_lockedAssyFp) Then Return
		If g_hoverAssyFp = r.FilePath Then Return
		' 清除上一次悬停高亮
		If Not String.IsNullOrEmpty(g_hoverAssyFp) Then HighlightChildRows(g_hoverAssyFp, False)
		g_hoverAssyFp = r.FilePath
		HighlightChildRows(r.FilePath, True)
	End Sub

	Sub OnCellMouseLeave(sender As Object, e As DataGridViewCellEventArgs)
		' 离开缩略图列：关闭弹窗
		If g_grid.Columns.Contains("cThumb") AndAlso
			e.ColumnIndex >= 0 AndAlso
			g_grid.Columns("cThumb").Index = e.ColumnIndex Then
			CloseThumbPopup()
			Return
		End If
		' 原有逻辑
		If Not String.IsNullOrEmpty(g_lockedAssyFp) Then Return
		If String.IsNullOrEmpty(g_hoverAssyFp) Then Return
		HighlightChildRows(g_hoverAssyFp, False)
		g_hoverAssyFp = ""
	End Sub

	Sub OnGridSortCompare(sender As Object, e As DataGridViewSortCompareEventArgs)
		Try
			Dim v1 As String = If(e.CellValue1 Is Nothing, "", CStr(e.CellValue1).Trim())
			Dim v2 As String = If(e.CellValue2 Is Nothing, "", CStr(e.CellValue2).Trim())
			If String.IsNullOrEmpty(v1) AndAlso String.IsNullOrEmpty(v2) Then e.SortResult = 0 : e.Handled = True : Return
			If String.IsNullOrEmpty(v1) Then e.SortResult = 1 : e.Handled = True : Return
			If String.IsNullOrEmpty(v2) Then e.SortResult = -1 : e.Handled = True : Return
			' 统一分段比较：纯数字当单段，含点的按点分段，其余按字符分段'
			Dim p1() As String = v1.Split(".".ToCharArray())
			Dim p2() As String = v2.Split(".".ToCharArray())
			Dim maxLen As Integer = Math.Max(p1.Length, p2.Length)
			For _pi As Integer = 0 To maxLen - 1
				Dim s1 As String = If(_pi < p1.Length, p1(_pi).Trim(), "")
				Dim s2 As String = If(_pi < p2.Length, p2(_pi).Trim(), "")
				If String.IsNullOrEmpty(s1) AndAlso String.IsNullOrEmpty(s2) Then Continue For
				If String.IsNullOrEmpty(s1) Then e.SortResult = -1 : e.Handled = True : Return
				If String.IsNullOrEmpty(s2) Then e.SortResult = 1 : e.Handled = True : Return
				Dim seg1 As Integer = 0 : Dim seg2 As Integer = 0
				Dim b1 As Boolean = Integer.TryParse(s1, seg1)
				Dim b2 As Boolean = Integer.TryParse(s2, seg2)
				Dim segCmp As Integer
				If b1 AndAlso b2 Then
					segCmp = seg1.CompareTo(seg2)
				Else
					Dim ci As System.Globalization.CompareInfo = System.Globalization.CultureInfo.GetCultureInfo("zh-CN").CompareInfo
					segCmp = ci.Compare(s1, s2, System.Globalization.CompareOptions.IgnoreCase)
				End If
				If segCmp <> 0 Then e.SortResult = segCmp : e.Handled = True : Return
			Next
			e.SortResult = 0 : e.Handled = True
		Catch : e.Handled = False : End Try
	End Sub

	Sub OnCellClick(sender As Object, e As DataGridViewCellEventArgs)
		If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
		Dim isTypeCol As Boolean = g_grid.Columns.Contains("cType") AndAlso
		g_grid.Columns("cType").Index = e.ColumnIndex
		If isTypeCol Then
			Dim origIdx As Integer = GetOrigRowIdx(e.RowIndex)
			If origIdx >= 0 AndAlso origIdx < g_rows.Count Then
				Dim r As BomDataRow = g_rows(origIdx)
				If r.DocType = "部件" Then
					' 点击部件 cType 格子：锁定高亮
					If Not String.IsNullOrEmpty(g_lockedAssyFp) Then HighlightChildRows(g_lockedAssyFp, False)
					g_lockedAssyFp = r.FilePath
					g_hoverAssyFp = r.FilePath
					HighlightChildRows(r.FilePath, True)
					Return
				End If
			End If
		End If
		' 点击其他格子：解锁
		If Not String.IsNullOrEmpty(g_lockedAssyFp) Then
			HighlightChildRows(g_lockedAssyFp, False)
			g_lockedAssyFp = ""
			g_hoverAssyFp = ""
		End If
	End Sub

	Sub HighlightChildRows(assyFp As String, highlight As Boolean)
		Try
			Dim lightGreen As System.Drawing.Color = System.Drawing.Color.FromArgb(220, 255, 220)
			' BFS 收集所有后代 FilePath（包含子部件及其子部件的所有层级）
			Dim descendants As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			Dim queue As New Queue(Of String)()
			queue.Enqueue(assyFp)
			Do While queue.Count > 0
				Dim curFp As String = queue.Dequeue()
				For Each rr As BomDataRow In g_rows
					If String.Compare(rr.ParentFilePath, curFp, True) = 0 Then
						If Not descendants.Contains(rr.FilePath) Then
							descendants.Add(rr.FilePath)
							' 如果子节点也是部件，继续向下搜索
							If rr.DocType = "部件" Then queue.Enqueue(rr.FilePath)
						End If
					End If
				Next
			Loop
			' 批量高亮/恢复所有后代行
			For Each dgvRow As DataGridViewRow In g_grid.Rows
				Dim oi As Integer = GetOrigRowIdx(dgvRow.Index)
				If oi < 0 OrElse oi >= g_rows.Count Then Continue For
				Dim rr As BomDataRow = g_rows(oi)
				If Not descendants.Contains(rr.FilePath) Then Continue For
				If highlight Then
					dgvRow.DefaultCellStyle.BackColor = lightGreen
				Else
					dgvRow.DefaultCellStyle.BackColor = System.Drawing.Color.Empty
				End If
			Next
			g_grid.Invalidate()
		Catch : End Try
	End Sub

	Sub SyncChkFlowOrder()
		Try
			If g_chkFlow Is Nothing Then Return
			' 按 DisplayIndex 排序，重新排列 g_chkFlow 里的行
			Dim colOrder As New List(Of KeyValuePair(Of Integer, String))
			For Each col As DataGridViewColumn In g_grid.Columns
				colOrder.Add(New KeyValuePair(Of Integer, String)(col.DisplayIndex, col.Name))
			Next
			colOrder.Sort(Function(a, b) a.Key.CompareTo(b.Key))
			' 按新顺序重排 g_chkFlow 里的控件
			Dim pos As Integer = 0
			For Each kv As KeyValuePair(Of Integer, String) In colOrder
				For Each ctrl As Control In g_chkFlow.Controls
					If ctrl.Tag IsNot Nothing AndAlso CStr(ctrl.Tag) = kv.Value Then
						g_chkFlow.Controls.SetChildIndex(ctrl, pos)
						pos += 1
						Exit For
					End If
				Next
			Next
		Catch : End Try
	End Sub

	Sub OnChkBoxClick(sender As Object, e As EventArgs)
		Dim chk As CheckBox = CType(sender, CheckBox)
		' 获取鼠标在CheckBox上的位置
		Dim mousePos As System.Drawing.Point = chk.PointToClient(System.Windows.Forms.Cursor.Position)
		' 方框宽度约为16-20px，只有点在方框区域才切换勾选
		If mousePos.X <= Scaled(20) Then
			chk.Checked = Not chk.Checked
		End If
		' 无论点哪里都触发高亮
		OnChkRowClick(sender, e)
	End Sub

	Sub OnChkRowClick(sender As Object, e As EventArgs)
		' 找到点击的行Panel
		Dim clickedPnl As Panel = Nothing
		If TypeOf sender Is Panel Then
			clickedPnl = CType(sender, Panel)
		ElseIf TypeOf sender Is Label Then
			If TypeOf CType(sender, Label).Parent Is Panel Then
				clickedPnl = CType(CType(sender, Label).Parent, Panel)
			End If
		ElseIf TypeOf sender Is CheckBox Then
			clickedPnl = CType(CType(sender, CheckBox).Parent, Panel)
		End If
		If clickedPnl Is Nothing Then Return
		' 取消之前选中的高亮
		If g_selectedChkRow IsNot Nothing Then
			Dim normalBg As System.Drawing.Color = PanelBg()
			g_selectedChkRow.BackColor = normalBg
			For Each ctrl As Control In g_selectedChkRow.Controls
				If Not TypeOf ctrl Is Button Then
					ctrl.BackColor = normalBg
					If TypeOf ctrl Is Label Then ctrl.ForeColor = If(g_theme = 7, System.Drawing.Color.White, If(g_theme = 8, ContrastFg(g_custColMgr), System.Drawing.Color.Black))
				Else
					If ctrl.Text = "X" Then
						ctrl.BackColor = C(200, 50, 50)
					End If
				End If
			Next
		End If
		' 高亮当前选中行
		g_selectedChkRow = clickedPnl
		Dim selC As System.Drawing.Color = If(g_theme = 7, System.Drawing.Color.FromArgb(60, 75, 110), System.Drawing.Color.FromArgb(180, 220, 180))
		clickedPnl.BackColor = selC
		For Each ctrl As Control In clickedPnl.Controls
			If Not TypeOf ctrl Is Button Then
				ctrl.BackColor = selC
				If TypeOf ctrl Is Label Then ctrl.ForeColor = If(g_theme = 7, System.Drawing.Color.White, If(g_theme = 8, ContrastFg(g_custColMgr), System.Drawing.Color.Black))
			ElseIf ctrl.Text = "X" Then
				ctrl.BackColor = C(200, 50, 50)
			End If
		Next

	End Sub
	Sub OnMoveColUp(sender As Object, e As EventArgs)
		MoveSelectedCol(-1)
	End Sub

	Sub OnMoveColDown(sender As Object, e As EventArgs)
		MoveSelectedCol(1)
	End Sub

	Sub MoveSelectedCol(direction As Integer)
		If g_selectedChkRow Is Nothing Then Return
		Dim colName As String = SafeStr(g_selectedChkRow.Tag)
		If String.IsNullOrEmpty(colName) Then Return
		If Not g_grid.Columns.Contains(colName) Then Return
		Dim col As DataGridViewColumn = g_grid.Columns(colName)
		Dim curIdx As Integer = col.DisplayIndex
		Dim newIdx As Integer = curIdx + direction
		' 不能移到冻结列（序号+类型）前面
		Dim frozenCount As Integer = 0
		For Each fc As DataGridViewColumn In g_grid.Columns
			If fc.Frozen Then frozenCount += 1
		Next
		If newIdx < frozenCount OrElse newIdx >= g_grid.Columns.Count Then Return
		' 冻结列（序号/类型）本身不能移动
		If col.Frozen Then Return
		' 额外保护：cIdx 和 cType 绝对不能移动
		If colName = "cIdx" OrElse colName = "cType" Then Return
		' 移动列
		col.DisplayIndex = newIdx
		' 同步右边列管理顺序（由ColumnDisplayIndexChanged事件触发SyncChkFlowOrder）
		SyncChkFlowOrder()
		' 保存设置
		SaveSettings()
		SetStatus("  " & ChrW(24050) & ChrW(31227) & ChrW(21160) & ": " & col.HeaderText, C(0, 100, 60))
	End Sub

	Sub DoClearSelection()
		If g_grid Is Nothing Then Return
		If g_grid.IsCurrentCellInEditMode Then g_grid.CancelEdit()
		g_grid.ClearSelection()
		SetStatus("  已取消选择", C(110, 55, 0))
	End Sub

	Sub OnFormKeyDown(sender As Object, e As KeyEventArgs)
		If e.KeyCode = Keys.Escape Then
			' ESC：解锁部件高亮
			If Not String.IsNullOrEmpty(g_lockedAssyFp) Then
				HighlightChildRows(g_lockedAssyFp, False)
				g_lockedAssyFp = ""
				g_hoverAssyFp = ""
			End If
			DoClearSelection()
			e.Handled = True
			e.SuppressKeyPress = True
		ElseIf e.Control AndAlso e.KeyCode = Keys.C Then
			' Ctrl+C 复制当前单元格
			If g_grid IsNot Nothing AndAlso Not g_grid.IsCurrentCellInEditMode Then
				OnGridCopy(Nothing, EventArgs.Empty)
				e.Handled = True
				e.SuppressKeyPress = True
			End If
		ElseIf e.Control AndAlso e.KeyCode = Keys.V Then
			' Ctrl+V 粘贴到当前单元格
			If g_grid IsNot Nothing AndAlso Not g_grid.IsCurrentCellInEditMode Then
				' 先快照当前选区（Ctrl键按下时DataGridView可能改变SelectedCells）
				g_pasteTargetCells.Clear()
				For Each _pc As DataGridViewCell In g_grid.SelectedCells
					g_pasteTargetCells.Add(_pc)
				Next
				OnGridPaste(Nothing, EventArgs.Empty)
				e.Handled = True
				e.SuppressKeyPress = True
			End If
		ElseIf e.KeyCode = Keys.Delete Then
			' Delete：清空选中单元格内容（不进入编辑模式）
			If g_grid IsNot Nothing AndAlso Not g_grid.IsCurrentCellInEditMode Then
				Dim clearedAny As Boolean = False
				For Each cell As DataGridViewCell In g_grid.SelectedCells
					If cell.ReadOnly Then Continue For
					cell.Value = ""
					clearedAny = True
				Next
				If clearedAny Then
					g_isDirty = True
					SetStatus("  已清空选中单元格", C(110, 55, 0))
				End If
				e.Handled = True
				e.SuppressKeyPress = True
			End If
		End If
	End Sub

	Sub OnLogoClick(sender As Object, e As EventArgs)
		If g_grid Is Nothing OrElse g_grid.Rows.Count = 0 Then Return
		g_grid.SelectAll()
		g_grid.Focus()
		SetStatus("  已全选 " & g_grid.Rows.Count.ToString() & " 行", C(0, 100, 180))
	End Sub

	Sub OnGridMouseDown(sender As Object, e As MouseEventArgs)
		If e.Button <> MouseButtons.Right Then Return
		Dim hit As DataGridView.HitTestInfo = g_grid.HitTest(e.X, e.Y)
		If hit.RowIndex >= 0 AndAlso hit.RowIndex < g_grid.Rows.Count Then
			g_rightClickRowIdx = hit.RowIndex
			' 右键行已在多选中：保留全部多选，不清除
			Dim alreadySelected As Boolean = False
			For Each cell As DataGridViewCell In g_grid.SelectedCells
				If cell.RowIndex = hit.RowIndex Then alreadySelected = True : Exit For
			Next
			If Not alreadySelected Then
				' 右键行不在多选中：切换为单选该行
				g_grid.ClearSelection()
				g_grid.Rows(hit.RowIndex).Selected = True
			End If
		Else
			g_rightClickRowIdx = -1
		End If
	End Sub

	Sub OnGridOpenDoc(sender As Object, e As EventArgs)
		' 收集所有选中行的唯一 FilePath（去重）
		Dim toOpen As New List(Of String)()
		Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
		' 先加右键所在行
		If g_rightClickRowIdx >= 0 Then
			Dim oi As Integer = GetOrigRowIdx(g_rightClickRowIdx)
			If oi >= 0 AndAlso oi < g_rows.Count Then
				Dim fp As String = g_rows(oi).FilePath
				If Not String.IsNullOrEmpty(fp) AndAlso Not seen.Contains(fp) Then
					seen.Add(fp) : toOpen.Add(fp)
				End If
			End If
		End If
		' 再加所有选中格子所在行
		For Each cell As DataGridViewCell In g_grid.SelectedCells
			Dim oi As Integer = GetOrigRowIdx(cell.RowIndex)
			If oi < 0 OrElse oi >= g_rows.Count Then Continue For
			Dim fp As String = g_rows(oi).FilePath
			If Not String.IsNullOrEmpty(fp) AndAlso Not seen.Contains(fp) Then
				seen.Add(fp) : toOpen.Add(fp)
			End If
		Next
		If toOpen.Count = 0 Then Return
		' 检查文件是否存在
		Dim missing As New List(Of String)()
		For Each fp As String In toOpen
			If Not System.IO.File.Exists(fp) Then missing.Add(System.IO.Path.GetFileName(fp))
		Next
		If missing.Count > 0 Then
			MessageBox.Show("以下文件不存在：" & vbCrLf & String.Join(vbCrLf, missing.ToArray()), "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End If
		' 批量打开
		Dim errList As New List(Of String)()
		For Each fp As String In toOpen
			If Not System.IO.File.Exists(fp) Then Continue For
			Try
				ThisApplication.Documents.Open(fp, True)
			Catch ex As Exception
				errList.Add(System.IO.Path.GetFileName(fp) & ": " & ex.Message)
			End Try
		Next
		If errList.Count > 0 Then
			MessageBox.Show("部分文件打开失败：" & vbCrLf & String.Join(vbCrLf, errList.ToArray()), "BOM工具", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End If
		' 打开后关闭 BOM 窗口
		g_frm.Close()
	End Sub

	' ★ 检查由本规则打开的文档是否有属性改动，有则提示是否写回 BOM 表

	Sub OnGridCopy(sender As Object, e As EventArgs)
		' ── 整列复制：按列分组，每列按行顺序收集值 ──
		g_clipboardList.Clear()
		g_clipboardColNames.Clear()
		If g_grid.SelectedCells.Count = 0 Then Return

		' 收集选中单元格，按列分组（保持列的显示顺序）
		Dim colOrder As New SortedDictionary(Of Integer, List(Of DataGridViewCell))()
		For Each cell As DataGridViewCell In g_grid.SelectedCells
			Dim dispIdx As Integer = g_grid.Columns(cell.ColumnIndex).DisplayIndex
			If Not colOrder.ContainsKey(dispIdx) Then colOrder(dispIdx) = New List(Of DataGridViewCell)()
			colOrder(dispIdx).Add(cell)
		Next

		' 每列内按行号升序排列，然后依次存入 g_clipboardList
		Dim totalCells As Integer = 0
		For Each kv As KeyValuePair(Of Integer, List(Of DataGridViewCell)) In colOrder
			kv.Value.Sort(Function(a, b) a.RowIndex.CompareTo(b.RowIndex))
			Dim colName As String = g_grid.Columns(kv.Value(0).ColumnIndex).Name
			g_clipboardColNames.Add(colName)
			For Each cell As DataGridViewCell In kv.Value
				g_clipboardList.Add(SafeStr(cell.Value))
				totalCells += 1
			Next
		Next

		' 同时写入系统剪贴板（Tab分隔列，换行分隔行，与Excel兼容）
		Try
			Dim rowCount As Integer = If(colOrder.Count > 0, colOrder.Values(0).Count, 0)
			Dim sb As New System.Text.StringBuilder()
			For ri As Integer = 0 To rowCount - 1
				Dim parts As New List(Of String)()
				Dim ci As Integer = 0
				For Each kv2 As KeyValuePair(Of Integer, List(Of DataGridViewCell)) In colOrder
					If ri < kv2.Value.Count Then parts.Add(SafeStr(kv2.Value(ri).Value)) Else parts.Add("")
					ci += 1
				Next
				sb.AppendLine(String.Join(ChrW(9), parts.ToArray()))
			Next
			Dim cbStr As String = sb.ToString().TrimEnd()
			System.Windows.Forms.Clipboard.SetText(If(String.IsNullOrEmpty(cbStr), " ", cbStr))
		Catch : End Try

		Dim colCount As Integer = g_clipboardColNames.Count
		Dim rowsPerCol As Integer = If(colCount > 0, g_clipboardList.Count \ colCount, 0)
		SetStatus("  已复制 " & colCount & " 列 × " & rowsPerCol & " 行", C(0, 100, 60))
	End Sub

	Sub OnGridPaste(sender As Object, e As EventArgs)
		' ── 整列粘贴：列数、行数均不匹配则提示退出 ──
		' 优先用 Ctrl+V 快照的选区；右键菜单调用时快照为空，则用当前 SelectedCells
		Dim targetCells As New List(Of DataGridViewCell)()
		If g_pasteTargetCells.Count > 0 Then
			targetCells.AddRange(g_pasteTargetCells)
			g_pasteTargetCells.Clear()
		Else
			For Each _sc As DataGridViewCell In g_grid.SelectedCells
				targetCells.Add(_sc)
			Next
		End If
		If targetCells.Count = 0 Then Return

		' 如果内部剪贴板为空（从未复制过），退出
		If g_clipboardColNames.Count = 0 OrElse g_clipboardList.Count = 0 Then
			SetStatus("  剪贴板为空，请先用 Ctrl+C 复制", C(180, 60, 0))
			Return
		End If

		' 收集目标选区，按列分组（保持列的显示顺序）
		Dim dstColOrder As New SortedDictionary(Of Integer, List(Of DataGridViewCell))()
		For Each cell As DataGridViewCell In targetCells
			Dim dispIdx As Integer = g_grid.Columns(cell.ColumnIndex).DisplayIndex
			If Not dstColOrder.ContainsKey(dispIdx) Then dstColOrder(dispIdx) = New List(Of DataGridViewCell)()
			dstColOrder(dispIdx).Add(cell)
		Next

		Dim srcColCount As Integer = g_clipboardColNames.Count
		Dim dstColCount As Integer = dstColOrder.Count
		Dim srcRowCount As Integer = g_clipboardList.Count \ srcColCount

		' 目标行数：取各列中行数的最大值（各列选的行数应一致，取最大保证完整检测）
		Dim dstRowCount As Integer = 0
		For Each kv As KeyValuePair(Of Integer, List(Of DataGridViewCell)) In dstColOrder
			If kv.Value.Count > dstRowCount Then dstRowCount = kv.Value.Count
		Next

		' ── 单格复制：允许粘贴到任意多个目标格，内容全部填为复制的那一个值 ──
		Dim isSingleCell As Boolean = (srcColCount = 1 AndAlso srcRowCount = 1)
		If isSingleCell AndAlso (dstColCount >= 1 OrElse dstRowCount >= 1) Then
			Dim singleVal As String = g_clipboardList(0)
			Dim singleColName As String = g_clipboardColNames(0)
			Dim writtenSingle As Integer = 0 : Dim skippedSingle As Integer = 0
			For Each cell As DataGridViewCell In targetCells
				If cell.ReadOnly Then skippedSingle += 1 : Continue For
				Dim colName As String = g_grid.Columns(cell.ColumnIndex).Name
				cell.Value = singleVal
				Dim origIdx As Integer = GetOrigRowIdx(cell.RowIndex)
				If origIdx >= 0 AndAlso origIdx < g_rows.Count Then
					Dim rr As BomDataRow = g_rows(origIdx)
					Select Case colName
						Case "cPno" : rr.PartNumber = singleVal
						Case "cDesc" : rr.Description = singleVal
						Case "cMat" : rr.Material = singleVal
						Case "cCat" : rr.Category = singleVal
						Case "cDno" : rr.DrawingNo = singleVal
						Case Else
							For Each cd As ColDef In g_colDefs
								If cd.IsCustom AndAlso cd.ColName = colName Then
									rr.Custom(cd.PropName) = singleVal : Exit For
								End If
							Next
					End Select
					rr.ChangedCols.Add(colName)
				End If
				writtenSingle += 1
			Next
			g_isDirty = True
			SetStatus("  已粘贴到 " & writtenSingle & " 个单元格", C(0, 130, 55))
			Return
		End If
		' ── 列数或行数不匹配：弹窗提示并退出 ──
		Dim colMismatch As Boolean = (dstColCount <> srcColCount)
		Dim rowMismatch As Boolean = (dstRowCount <> srcRowCount)
		If colMismatch OrElse rowMismatch Then
			Dim msgLines As New System.Text.StringBuilder()
			msgLines.AppendLine("复制的内容：" & srcColCount & " 列 × " & srcRowCount & " 行")
			msgLines.AppendLine("目标选区：  " & dstColCount & " 列 × " & dstRowCount & " 行")
			msgLines.AppendLine("")
			If colMismatch AndAlso rowMismatch Then
				msgLines.AppendLine("列数和行数均不匹配，无法粘贴。")
			ElseIf colMismatch Then
				msgLines.AppendLine("列数不匹配，无法粘贴。")
			Else
				msgLines.AppendLine("行数不匹配，无法粘贴。")
			End If
			msgLines.Append("请重新选择 " & srcColCount & " 列 × " & srcRowCount & " 行 的目标区域后再粘贴。")
			MessageBox.Show(msgLines.ToString(),
			"BOM工具 — 粘贴区域不匹配", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Dim hint As String = "  粘贴失败：复制 " & srcColCount & "列×" & srcRowCount & "行，目标 " & dstColCount & "列×" & dstRowCount & "行，不匹配"
			SetStatus(hint, C(180, 60, 0))
			Return
		End If

		' ── 每列内按行号升序排列 ──
		For Each kv As KeyValuePair(Of Integer, List(Of DataGridViewCell)) In dstColOrder
			kv.Value.Sort(Function(a, b) a.RowIndex.CompareTo(b.RowIndex))
		Next

		' ── 按列顺序逐行写入 ──
		Dim writtenCells As Integer = 0
		Dim skippedRO As Integer = 0
		Dim colIdx As Integer = 0
		For Each kv As KeyValuePair(Of Integer, List(Of DataGridViewCell)) In dstColOrder
			Dim dstRows As List(Of DataGridViewCell) = kv.Value
			For ri As Integer = 0 To dstRows.Count - 1
				Dim srcIdx As Integer = colIdx * srcRowCount + ri
				If srcIdx >= g_clipboardList.Count Then Continue For
				Dim cell As DataGridViewCell = dstRows(ri)
				If cell.ReadOnly Then skippedRO += 1 : Continue For
				cell.Value = g_clipboardList(srcIdx)
				writtenCells += 1
			Next
			colIdx += 1
		Next

		If writtenCells > 0 Then
			g_isDirty = True
			Dim msg As String = "  已粘贴 " & srcColCount & " 列 × " & srcRowCount & " 行（共 " & writtenCells & " 格）"
			If skippedRO > 0 Then msg &= "，跳过只读 " & skippedRO & " 格"
			SetStatus(msg, C(0, 100, 60))
		ElseIf skippedRO > 0 Then
			SetStatus("  选中列均为只读，无法粘贴", C(180, 60, 0))
		End If
	End Sub


	Sub DoFullRefresh()
		SetStatus("  正在刷新全表格...", C(0, 100, 180))

		' ── 1. 保存排序列、方向、锚点 ──
		Dim _anchorFp As String = ""
		Try
			If g_grid.SortedColumn IsNot Nothing AndAlso g_grid.SortOrder <> System.Windows.Forms.SortOrder.None Then
			End If
			If g_grid.Rows.Count > 0 Then
				Dim fi As Integer = Math.Max(0, g_grid.FirstDisplayedScrollingRowIndex)
				Dim oi As Integer = GetOrigRowIdx(fi)
				If oi >= 0 AndAlso oi < g_rows.Count Then _anchorFp = g_rows(oi).FilePath
			End If
		Catch : End Try

		' ── 2. 保存缩略图缓存 ──
		Dim thumbCache As New Dictionary(Of String, System.Drawing.Image)(StringComparer.OrdinalIgnoreCase)
		For Each oldRow As BomDataRow In g_rows
			If oldRow.Thumbnail IsNot Nothing AndAlso Not String.IsNullOrEmpty(oldRow.FilePath) Then
				thumbCache(oldRow.FilePath) = oldRow.Thumbnail
			End If
		Next

		' ── 3. 停止缩略图线程 ──
		g_thumbCancelFlag = True
		If g_thumbThread IsNot Nothing AndAlso g_thumbThread.IsAlive Then g_thumbThread.Join(500)
		g_thumbCancelFlag = False

		' ── 4. 收集新数据 ──
		Dim newRows As List(Of BomDataRow) = CollectRows()
		Dim newMap As New Dictionary(Of String, BomDataRow)(StringComparer.OrdinalIgnoreCase)
		For Each nr As BomDataRow In newRows
			If Not newMap.ContainsKey(nr.FilePath) Then newMap(nr.FilePath) = nr
		Next
		Dim ordered As New List(Of BomDataRow)()
		Dim usedFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
		For Each oldR As BomDataRow In g_rows
			If newMap.ContainsKey(oldR.FilePath) Then
				Dim _nr As BomDataRow = newMap(oldR.FilePath)
				_nr.FixedIdx = oldR.FixedIdx
				ordered.Add(_nr) : usedFp.Add(oldR.FilePath)
			End If
		Next
		For Each nr As BomDataRow In newRows
			If Not usedFp.Contains(nr.FilePath) Then ordered.Add(nr)
		Next
		g_rows = ordered
		' ── 4b. 若有记忆排序，按 g_sortedOrder 重排 ──
		If g_sortedOrder.Count > 0 Then
			Dim soMap As New Dictionary(Of String, BomDataRow)(StringComparer.OrdinalIgnoreCase)
			For Each row As BomDataRow In g_rows
				If Not soMap.ContainsKey(row.FilePath) Then soMap(row.FilePath) = row
			Next
			Dim soList As New List(Of BomDataRow)()
			Dim soUsed As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			For Each fp As String In g_sortedOrder
				If soMap.ContainsKey(fp) AndAlso Not soUsed.Contains(fp) Then
					soList.Add(soMap(fp)) : soUsed.Add(fp)
				End If
			Next
			' 追加新增的行（不在记忆顺序里的）
			For Each row As BomDataRow In g_rows
				If Not soUsed.Contains(row.FilePath) Then soList.Add(row)
			Next
			g_rows = soList
		End If
		g_occMapDirty = True

		' ── 5. 重新读取自定义列属性 ──
		For Each bomRow As BomDataRow In g_rows : bomRow.Custom.Clear() : Next
		For Each cd As ColDef In g_colDefs
			If Not cd.IsCustom Then Continue For
			Dim isLvNo As Boolean = (cd.PropName = "级别序号" OrElse cd.PropName = "LevelNo")
			For Each bomRow As BomDataRow In g_rows
				If isLvNo Then
					bomRow.Custom(cd.PropName) = If(String.IsNullOrEmpty(bomRow.LevelNo), "", bomRow.LevelNo)
				Else
					bomRow.Custom(cd.PropName) = ReadAnyProp(bomRow.FilePath, cd.PropName)
				End If
			Next
		Next
		LoadTypeIcons()
		LoadMaterialList()

		' ── 6. 重建表格 ──
		RefreshGridRows()

		' ── 7. 回填缩略图 ──
		For Each nr As BomDataRow In g_rows
			If thumbCache.ContainsKey(nr.FilePath) Then nr.Thumbnail = thumbCache(nr.FilePath)
		Next
		If g_grid.Columns.Contains("cThumb") Then
			For Each dgvRow As DataGridViewRow In g_grid.Rows
				Dim oi As Integer = GetOrigRowIdx(dgvRow.Index)
				If oi >= 0 AndAlso oi < g_rows.Count Then dgvRow.Cells("cThumb").Value = g_rows(oi).Thumbnail
			Next
		End If

		' ── 8. BeginInvoke：恢复排序 + 滚动 ──
		Dim capAnchor As String = _anchorFp
		g_grid.BeginInvoke(New System.Action(Sub()
												 Try
													 ' 再恢复滚动（排序完成后按路径找行）
													 If Not String.IsNullOrEmpty(capAnchor) AndAlso g_grid.Rows.Count > 0 Then
														 For ri As Integer = 0 To g_grid.Rows.Count - 1
															 Dim oi As Integer = GetOrigRowIdx(ri)
															 If oi >= 0 AndAlso oi < g_rows.Count AndAlso
						String.Compare(g_rows(oi).FilePath, capAnchor, True) = 0 Then
																 g_grid.FirstDisplayedScrollingRowIndex = ri
																 Exit For
															 End If
														 Next
													 End If
												 Catch : End Try
											 End Sub))


		SetStatus("  刷新完成，共 " & g_rows.Count & " 个唯一零部件", C(0, 130, 55))
	End Sub

	Sub GenerateThumbnails()
		If Not g_grid.Columns.Contains("cThumb") OrElse Not g_grid.Columns("cThumb").Visible Then
			SetStatus("  缩略图列未显示，请先在列管理中开启", C(180, 80, 0))
			Return
		End If
		SetStatus("  正在生成缩略图...", C(0, 100, 180))
		g_frm.Refresh()
		g_thumbCancelFlag = True
		If g_thumbThread IsNot Nothing AndAlso g_thumbThread.IsAlive Then g_thumbThread.Join(500)
		g_thumbCancelFlag = False
		Dim total As Integer = g_rows.Count
		Dim tNew As New System.Threading.Thread(Sub()
													Try
														Dim done As Integer = 0
														For Each rr As BomDataRow In g_rows
															If g_thumbCancelFlag Then Exit For
															If rr.Thumbnail Is Nothing Then
																' 优先从文件二进制流读取内置预览图（最快，不需要打开文档）
																rr.Thumbnail = GetInventorThumbnail(rr.FilePath)
																' 其次用 Shell API（读取 Windows 缓存）
																If rr.Thumbnail Is Nothing Then rr.Thumbnail = GetShellThumbnail(rr.FilePath)
																' 最后用截图兜底
																If rr.Thumbnail Is Nothing Then rr.Thumbnail = GetHQThumbnail(rr.FilePath)
															End If
															done += 1
															If rr.Thumbnail IsNot Nothing AndAlso g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
																Dim capRr As BomDataRow = rr
																Dim capDone As Integer = done
																g_grid.Invoke(Sub()
																				  Try
																					  For Each dgvRow As DataGridViewRow In g_grid.Rows
																						  Dim oi As Integer = GetOrigRowIdx(dgvRow.Index)
																						  If oi >= 0 AndAlso oi < g_rows.Count AndAlso g_rows(oi) Is capRr Then
																							  dgvRow.Cells("cThumb").Value = capRr.Thumbnail : Exit For
																						  End If
																					  Next
																					  SetStatus("  缩略图: " & capDone & " / " & total, C(0, 100, 180))
																				  Catch : End Try
																			  End Sub)
															End If
															System.Threading.Thread.Sleep(10)
														Next
														If g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
															g_grid.Invoke(Sub()
																			  SetStatus("  缩略图生成完成，共 " & done & " 个", C(0, 130, 55))
																		  End Sub)
														End If
													Catch : End Try
												End Sub)
		tNew.IsBackground = True
		g_thumbThread = tNew
		tNew.Start()
	End Sub



	Public Sub Run()

		If Not CheckDocumentType() Then Return
		If Not CheckEnvironment() Then Return

		InitScaledConstants()

		g_settingsFile = GetSettingsPath()
		g_asmDoc = CType(ThisApplication.ActiveEditDocument, AssemblyDocument)
		g_rows = CollectRows()

		' 统计丢失文件，弹窗提示
		Dim missingList As New List(Of String)()
		For Each mr As BomDataRow In g_rows
			If mr.IsMissing Then missingList.Add(mr.FileName)
		Next
		If missingList.Count > 0 Then
			MessageBox.Show("以下 " & missingList.Count & " 个文件丢失（找不到引用），将以紫色显示，属性为空：" &
			vbCrLf & String.Join(vbCrLf, missingList.ToArray()),
			"BOM工具 — 丢失文件", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End If

		g_colDefs = New List(Of ColDef)
		AddBuiltinCol("cIdx", "序号", True, True, Scaled(46), True, False)
		AddBuiltinCol("cType", "类型", True, True, Scaled(90), False, False)
		AddBuiltinCol("cFile", "文件名（图纸须项目路径内）", False, False, Scaled(280), False, False)
		AddBuiltinCol("cDno", "图号", False, False, Scaled(100), False, False)
		AddBuiltinCol("cPno", "零件代号", False, False, Scaled(120), False, False)
		AddBuiltinCol("cCat", "类别", False, False, Scaled(90), False, False)
		AddBuiltinCol("cQty", "统计数量", True, False, Scaled(60), True, False)
		AddBuiltinCol("cMat", "材料", False, False, Scaled(100), False, False)
		AddBuiltinCol("cDesc", "描述", False, False, Scaled(160), False, False)
		AddBuiltinCol("cBom", "BOM" & ChrW(32467) & ChrW(26500), False, False, Scaled(80), True, False)
		AddBuiltinCol("cDwg", ChrW(24037) & ChrW(31243) & ChrW(22270) & ChrW(26159) & ChrW(21542) & ChrW(26377), True, False, Scaled(90), True, False)
		AddBuiltinCol("cThumb", "缩略图", True, False, CalcThumbColWidth(), True, True)

		Dim clientW As Integer = 2000 + L_RIGHT_W + L_GAP * 3
		Dim clientH As Integer = 1200
		' 防止窗口超出屏幕可用区域
		Dim wa As System.Drawing.Rectangle = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea
		If clientW > wa.Width - 60 Then clientW = wa.Width - 60
		If clientH > wa.Height - 60 Then clientH = wa.Height - 60

		Dim gridX As Integer = 0
		Dim gridY As Integer = TOP_H + GAP
		Dim gridW As Integer = clientW - RIGHT_W - GAP * 2
		Dim gridH As Integer = clientH - TOP_H - BOT_H - GAP * 3

		Dim chkX As Integer = clientW - RIGHT_W - GAP
		Dim chkY As Integer = TOP_H + GAP
		Dim chkH As Integer = clientH - chkY

		Dim botX As Integer = 0
		Dim botY As Integer = gridY + gridH + GAP
		Dim botW As Integer = gridW

		g_frm = New Form()
		g_frm.Text = "BOM工具  -  " & g_asmDoc.DisplayName
		' 设置标题栏图标（从Inventor exe提取）
		Try
			Dim invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
			If System.IO.File.Exists(invExe) Then
				g_appIcon = System.Drawing.Icon.ExtractAssociatedIcon(invExe)
				g_frm.Icon = g_appIcon
			End If
		Catch : End Try
		g_frm.AutoScaleMode = AutoScaleMode.Dpi
		g_frm.ClientSize = New System.Drawing.Size(clientW, clientH)
		g_frm.StartPosition = FormStartPosition.CenterScreen
		g_frm.MinimizeBox = True
		g_frm.MaximizeBox = True
		g_frm.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_frm.BackColor = C(245, 243, 255)
		g_frm.KeyPreview = True
		AddHandler g_frm.KeyDown, AddressOf OnFormKeyDown
		' ★ 使用完整的 FormClosing 处理，替换原来的简单 SaveSettings 调用
		AddHandler g_frm.FormClosing, AddressOf OnFormClosing

		' ── 顶部工具栏 ──
		Dim pnlTop As New Panel()
		pnlTop.Location = New System.Drawing.Point(0, 0)
		pnlTop.Size = New System.Drawing.Size(clientW, TOP_H)
		pnlTop.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
		pnlTop.BackColor = OrangeMid()
		g_pnlTop = pnlTop
		' 双缓冲防闪烁
		Try
			Dim t As System.Type = g_frm.GetType()
			Dim pi As System.Reflection.PropertyInfo = t.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
			If pi IsNot Nothing Then pi.SetValue(g_frm, True, Nothing)
			Dim pt As System.Type = pnlTop.GetType()
			Dim pi2 As System.Reflection.PropertyInfo = pt.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
			If pi2 IsNot Nothing Then pi2.SetValue(pnlTop, True, Nothing)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
		AddHandler pnlTop.Paint, AddressOf PnlTopPaint
		g_frm.Controls.Add(pnlTop)

		' ── Logo 图标（BOM工具 左边的图标）──
		Dim pnlLogoIcon As New System.Windows.Forms.Panel()
		pnlLogoIcon.Location = New System.Drawing.Point(0, 0)
		pnlLogoIcon.Size = New System.Drawing.Size(Scaled(38), TOP_H)
		pnlLogoIcon.BackColor = C(80, 60, 200)
		pnlLogoIcon.Cursor = Cursors.Hand
		Try
			Dim pl As System.Type = pnlLogoIcon.GetType()
			Dim pil As System.Reflection.PropertyInfo = pl.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
			If pil IsNot Nothing Then pil.SetValue(pnlLogoIcon, True, Nothing)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
		AddHandler pnlLogoIcon.Paint, Sub(ps As Object, pe As System.Windows.Forms.PaintEventArgs)
										  Dim g As System.Drawing.Graphics = pe.Graphics
										  g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
										  Dim cx As Integer = pnlLogoIcon.Width \ 2
										  Dim cy As Integer = pnlLogoIcon.Height \ 2
										  Dim r As Integer = Scaled(13)
										  ' 外圆（深蓝）
										  g.FillEllipse(New System.Drawing.SolidBrush(C(255, 255, 255)), cx - r, cy - r, r * 2, r * 2)
										  ' 内部：3x3 小格子（白色，模拟表格）
										  Dim cellSize As Integer = Scaled(4)
										  Dim gap As Integer = Scaled(1)
										  Dim logoGridW As Integer = cellSize * 3 + gap * 2
										  Dim gx As Integer = cx - logoGridW \ 2
										  Dim gy As Integer = cy - logoGridW \ 2
										  For row As Integer = 0 To 2
											  For col As Integer = 0 To 2
												  Dim rx As Integer = gx + col * (cellSize + gap)
												  Dim ry As Integer = gy + row * (cellSize + gap)
												  Dim fillClr As System.Drawing.Color = If(row = 0, C(0, 150, 200), C(180, 220, 240))
												  g.FillRectangle(New System.Drawing.SolidBrush(fillClr), rx, ry, cellSize, cellSize)
											  Next
										  Next
									  End Sub
		AddHandler pnlLogoIcon.Click, AddressOf OnLogoClick
		pnlTop.Controls.Add(pnlLogoIcon)

		Dim lblLogo As New Label()
		lblLogo.Text = "BOM工具"
		lblLogo.Location = New System.Drawing.Point(Scaled(38), 0)
		lblLogo.Size = New System.Drawing.Size(Scaled(102), TOP_H)
		lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		lblLogo.ForeColor = LogoFg()
		lblLogo.Font = MakeFontBold(CStr(g_fontName), 13.0!)
		lblLogo.BackColor = LogoBg()
		lblLogo.Cursor = Cursors.Hand
		AddHandler lblLogo.Click, AddressOf OnLogoClick
		pnlTop.Controls.Add(lblLogo)


		Dim btnRename As New Button()
		btnRename.Text = "Excel导出"
		btnRename.Size = New System.Drawing.Size(Scaled(110), Scaled(28))
		btnRename.Location = New System.Drawing.Point(Scaled(148), (TOP_H - Scaled(28)) \ 2)
		btnRename.FlatStyle = FlatStyle.Flat
		btnRename.FlatAppearance.BorderColor = OrangeLight()
		btnRename.BackColor = ExcelBtnBg()
		btnRename.ForeColor = System.Drawing.Color.White
		btnRename.Font = MakeFontBold(CStr(g_fontName), 9.0!)
		btnRename.Cursor = Cursors.Hand
		AddHandler btnRename.Click, AddressOf OnExportBomExcel
		g_btnExcelExport = btnRename
		Dim ttBomExcel As New ToolTip()
		ttBomExcel.InitialDelay = 500
		ttBomExcel.AutoPopDelay = 12000
		ttBomExcel.ShowAlways = True
		ttBomExcel.SetToolTip(btnRename,
	"【Excel 编辑操作流程】" & vbCrLf &
	"1. 点击按钮，自动生成 Excel 并打开" & vbCrLf &
	"2. 记得另存为 吴飞and罗林同志，切勿修改，另存之后直接关闭切勿修改保存")
		pnlTop.Controls.Add(btnRename)

		g_chkSkipCC = New CheckBox()
		g_chkSkipCC.Text = ChrW(38544) & ChrW(34255) & ChrW(26631) & ChrW(20934) & ChrW(20214)
		g_chkSkipCC.Checked = False
		g_chkSkipCC.Size = New System.Drawing.Size(Scaled(100), TOP_H)
		g_chkSkipCC.Location = New System.Drawing.Point(Scaled(274), 0)
		g_chkSkipCC.ForeColor = System.Drawing.Color.White
		g_chkSkipCC.BackColor = OrangeMid()
		g_chkSkipCC.Font = MakeFont(CStr(g_fontName), 9.0!)
		AddHandler g_chkSkipCC.CheckedChanged, AddressOf OnSkipCCChanged

		Dim ttCC As New ToolTip()
		ttCC.InitialDelay = 1000
		ttCC.AutoPopDelay = 8000
		ttCC.ShowAlways = True
		ttCC.SetToolTip(g_chkSkipCC,
	ChrW(21028) & ChrW(26029) & ChrW(26631) & ChrW(20934) & ChrW(20214) & ChrW(30340) & ChrW(21028) & ChrW(26029) & ChrW(20381) & ChrW(25454) & ChrW(65306) & vbCrLf &
	ChrW(8226) & " IsContentCenter=True " & ChrW(65288) & ChrW(36164) & ChrW(28304) & ChrW(20013) & ChrW(24515) & ChrW(30452) & ChrW(25509) & ChrW(25554) & ChrW(20837) & ChrW(65289) & vbCrLf &
	ChrW(8226) & " Category=" & ChrW(26631) & ChrW(20934) & ChrW(20214) & ChrW(65288) & ChrW(31867) & ChrW(21035) & ChrW(23646) & ChrW(24615) & ChrW(65289) & vbCrLf &
	ChrW(8226) & " " & ChrW(25991) & ChrW(20214) & ChrW(36335) & ChrW(24452) & ChrW(21253) & ChrW(21547) & " Content Center Files" & vbCrLf &
	ChrW(8226) & " BOMStructure=kPurchasedBOMStructure")
		pnlTop.Controls.Add(g_chkSkipCC)

		g_chkSkipVirtual = New CheckBox()
		g_chkSkipVirtual.Text = ChrW(38544) & ChrW(34255) & ChrW(34394) & ChrW(25311) & ChrW(21450) & ChrW(21442) & ChrW(32771)
		g_chkSkipVirtual.Checked = False
		g_chkSkipVirtual.Size = New System.Drawing.Size(Scaled(128), TOP_H)
		g_chkSkipVirtual.Location = New System.Drawing.Point(Scaled(274) + Scaled(100) + Scaled(2), 0)
		g_chkSkipVirtual.ForeColor = System.Drawing.Color.White
		g_chkSkipVirtual.BackColor = OrangeMid()
		g_chkSkipVirtual.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_chkSkipVirtual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_chkSkipVirtual.Cursor = Cursors.Hand
		AddHandler g_chkSkipVirtual.CheckedChanged, AddressOf OnSkipVirtualChanged
		Dim ttVirt As New ToolTip()
		ttVirt.InitialDelay = 500 : ttVirt.AutoPopDelay = 8000 : ttVirt.ShowAlways = True
		ttVirt.SetToolTip(g_chkSkipVirtual,
	"隐藏 BOM 结构为以下两种的行：" & vbCrLf &
	"• 虚拟件（BOMStructure = kPhantomBOMStructure）" & vbCrLf &
	"• 参考（BOMStructure = kReferenceBOMStructure）")
		pnlTop.Controls.Add(g_chkSkipVirtual)

		g_chkMergePno = New CheckBox()
		g_chkMergePno.Text = "零件代号合并"
		g_chkMergePno.Checked = False
		g_chkMergePno.Size = New System.Drawing.Size(Scaled(112), TOP_H)
		g_chkMergePno.Location = New System.Drawing.Point(Scaled(274) + Scaled(100) + Scaled(2) + Scaled(128) + Scaled(2), 0)
		g_chkMergePno.ForeColor = System.Drawing.Color.White
		g_chkMergePno.BackColor = OrangeMid()
		g_chkMergePno.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_chkMergePno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_chkMergePno.Cursor = Cursors.Hand
		Dim tt As New ToolTip()
		tt.SetToolTip(g_chkMergePno, "相同零件代号合并为一行；不同属性显示 *多种；缩略图不同则显示空白")
		AddHandler g_chkMergePno.CheckedChanged, AddressOf OnMergePnoChanged
		pnlTop.Controls.Add(g_chkMergePno)

		' filter mode checkboxes
		Dim fsx As Integer = Scaled(274) + Scaled(100) + Scaled(2) + Scaled(128) + Scaled(2) + Scaled(112) + Scaled(6)

		g_chkOnlyPart = New CheckBox()
		g_chkOnlyPart.Text = ChrW(20165) & ChrW(38646) & ChrW(20214)
		g_chkOnlyPart.Checked = False
		g_chkOnlyPart.Size = New System.Drawing.Size(Scaled(68), TOP_H)
		g_chkOnlyPart.Location = New System.Drawing.Point(fsx, 0)
		g_chkOnlyPart.ForeColor = System.Drawing.Color.White
		g_chkOnlyPart.BackColor = OrangeMid()
		g_chkOnlyPart.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_chkOnlyPart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_chkOnlyPart.Cursor = Cursors.Hand
		AddHandler g_chkOnlyPart.CheckedChanged, AddressOf OnFilterModeChanged
		Dim ttPart As New ToolTip()
		ttPart.InitialDelay = 1000 : ttPart.AutoPopDelay = 6000 : ttPart.ShowAlways = True
		ttPart.SetToolTip(g_chkOnlyPart,
	ChrW(20165) & ChrW(26174) & ChrW(31034) & ChrW(38646) & ChrW(20214) & vbCrLf &
	ChrW(8226) & " " & ChrW(26222) & ChrW(36890) & ChrW(38646) & ChrW(20214) & ChrW(65288) & ".ipt" & ChrW(65289) & vbCrLf &
	ChrW(8226) & " " & ChrW(38051) & ChrW(37329) & ChrW(38646) & ChrW(20214) & ChrW(20063) & ChrW(31639) & ChrW(22312) & ChrW(20869))
		pnlTop.Controls.Add(g_chkOnlyPart)

		g_chkOnlyAssy = New CheckBox()
		g_chkOnlyAssy.Text = ChrW(20165) & ChrW(37096) & ChrW(20214)
		g_chkOnlyAssy.Checked = False
		g_chkOnlyAssy.Size = New System.Drawing.Size(Scaled(68), TOP_H)
		g_chkOnlyAssy.Location = New System.Drawing.Point(fsx + Scaled(70), 0)
		g_chkOnlyAssy.ForeColor = System.Drawing.Color.White
		g_chkOnlyAssy.BackColor = OrangeMid()
		g_chkOnlyAssy.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_chkOnlyAssy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_chkOnlyAssy.Cursor = Cursors.Hand
		AddHandler g_chkOnlyAssy.CheckedChanged, AddressOf OnFilterModeChanged
		Dim ttAssy As New ToolTip()
		ttAssy.InitialDelay = 1000 : ttAssy.AutoPopDelay = 6000 : ttAssy.ShowAlways = True
		ttAssy.SetToolTip(g_chkOnlyAssy,
	ChrW(20165) & ChrW(26174) & ChrW(31034) & ChrW(37096) & ChrW(20214) & vbCrLf &
	ChrW(8226) & " " & ChrW(26222) & ChrW(36890) & ChrW(35013) & ChrW(37197) & ChrW(20307) & ChrW(65288) & ".iam" & ChrW(65289) & vbCrLf &
	ChrW(8226) & " " & ChrW(28938) & ChrW(25509) & ChrW(20214) & ChrW(20063) & ChrW(31639) & ChrW(22312) & ChrW(20869))
		pnlTop.Controls.Add(g_chkOnlyAssy)

		g_chkOnlySheet = New CheckBox()
		g_chkOnlySheet.Text = ChrW(20165) & ChrW(38051) & ChrW(37329) & ChrW(20214)
		g_chkOnlySheet.Checked = False
		g_chkOnlySheet.Size = New System.Drawing.Size(Scaled(82), TOP_H)
		g_chkOnlySheet.Location = New System.Drawing.Point(fsx + Scaled(140), 0)
		g_chkOnlySheet.ForeColor = C(180, 230, 255)
		g_chkOnlySheet.BackColor = OrangeMid()
		g_chkOnlySheet.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_chkOnlySheet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_chkOnlySheet.Cursor = Cursors.Hand
		AddHandler g_chkOnlySheet.CheckedChanged, AddressOf OnFilterModeChanged
		Dim ttSheet As New ToolTip()
		ttSheet.InitialDelay = 1000 : ttSheet.AutoPopDelay = 6000 : ttSheet.ShowAlways = True
		ttSheet.SetToolTip(g_chkOnlySheet,
	ChrW(20165) & ChrW(26174) & ChrW(31034) & ChrW(38051) & ChrW(37329) & ChrW(20214) & vbCrLf &
	ChrW(8226) & " SheetMetalComponentDefinition" & vbCrLf &
	ChrW(8226) & " " & ChrW(38051) & ChrW(37329) & ChrW(22411) & ChrW(38646) & ChrW(20214))
		pnlTop.Controls.Add(g_chkOnlySheet)

		g_chkOnlyWeld = New CheckBox()
		g_chkOnlyWeld.Text = ChrW(20165) & ChrW(28938) & ChrW(25509) & ChrW(20214)
		g_chkOnlyWeld.Checked = False
		g_chkOnlyWeld.Size = New System.Drawing.Size(Scaled(80), TOP_H)
		g_chkOnlyWeld.Location = New System.Drawing.Point(fsx + Scaled(144) + Scaled(82), 0)
		g_chkOnlyWeld.BackColor = OrangeMid()
		g_chkOnlyWeld.Font = MakeFont(CStr(g_fontName), 9.0!)
		g_chkOnlyWeld.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_chkOnlyWeld.Cursor = Cursors.Hand
		AddHandler g_chkOnlyWeld.CheckedChanged, AddressOf OnFilterModeChanged
		Dim ttWeld As New ToolTip()
		ttWeld.InitialDelay = 1000 : ttWeld.AutoPopDelay = 6000 : ttWeld.ShowAlways = True
		ttWeld.SetToolTip(g_chkOnlyWeld, "仅显示焊接件（WeldmentComponentDefinition）")
		pnlTop.Controls.Add(g_chkOnlyWeld)

		' ── 顶栏右侧功能下拉列表 ──
		Dim cmbTools As New System.Windows.Forms.ComboBox()
		cmbTools.Size = New System.Drawing.Size(Scaled(165), Scaled(28))
		cmbTools.Location = New System.Drawing.Point(clientW - RIGHT_W - GAP, (TOP_H - Scaled(28)) \ 2)
		cmbTools.Anchor = AnchorStyles.Right Or AnchorStyles.Top
		cmbTools.DropDownStyle = ComboBoxStyle.DropDownList
		cmbTools.Font = MakeFont(CStr(g_fontName), 9.5!)
		cmbTools.BackColor = C(228, 222, 252)
		cmbTools.Items.Add("⚙ 功能")
		cmbTools.Items.Add("[-] 批量删除属性")
		cmbTools.Items.Add("✏ 自定义属性改名")
		cmbTools.Items.Add("🔄 属性字符替换")
		cmbTools.Items.Add("🔢 写入图号")
		cmbTools.Items.Add("📄 属性写入工程图（文字）")
		cmbTools.Items.Add("🖨 打印装配顺序工程图")
		cmbTools.Items.Add("📤 按属性导出PDF/DWG")
		cmbTools.Items.Add("🔗 属性拼接写入")
		cmbTools.Items.Add("📊 BOM数量统计写入")
		cmbTools.Items.Add("🎮 俄罗斯方块")
		cmbTools.SelectedIndex = 0
		g_cmbTools = cmbTools
		Dim cmbToolsResetting As Boolean = False
		AddHandler cmbTools.SelectedIndexChanged, Sub(s2 As Object, e2 As EventArgs)
													  If cmbToolsResetting Then Return
													  Dim selIdx As Integer = cmbTools.SelectedIndex
													  If selIdx = 0 Then Return
													  cmbToolsResetting = True
													  cmbTools.SelectedIndex = 0
													  cmbToolsResetting = False
													  If selIdx = 1 Then
														  ShowBatchDeletePropDialog()
													  ElseIf selIdx = 2 Then
														  ShowRenamePropDialog()
													  ElseIf selIdx = 3 Then
														  ShowReplaceValueDialog()
													  ElseIf selIdx = 4 Then
														  ShowAutoDrawingNoDialog()
													  ElseIf selIdx = 5 Then
														  ShowWritePropToDrawingDialog()
													  ElseIf selIdx = 6 Then
														  Me.ShowPrintAssemblyDialog()
													  ElseIf selIdx = 7 Then
														  Me.ShowExportByPropDialog()
													  ElseIf selIdx = 8 Then
														  Me.ShowPropConcatDialog()
													  ElseIf selIdx = 9 Then
														  Me.ShowBOMQuantityDialog()
													  ElseIf selIdx = 10 Then
														  Me.ShowTetrisGame()
													  End If
												  End Sub
		pnlTop.Controls.Add(cmbTools)

		' ── 功能说明按钮 ──
		Dim btnHelpTools As New Button()
		btnHelpTools.Text = "?"
		btnHelpTools.Size = New System.Drawing.Size(Scaled(28), Scaled(28))
		btnHelpTools.Location = New System.Drawing.Point(cmbTools.Left - Scaled(33), (TOP_H - Scaled(28)) \ 2)
		btnHelpTools.Anchor = AnchorStyles.Right Or AnchorStyles.Top
		btnHelpTools.FlatStyle = FlatStyle.Flat
		btnHelpTools.FlatAppearance.BorderSize = 0
		btnHelpTools.BackColor = OrangeMid()
		btnHelpTools.ForeColor = System.Drawing.Color.White
		btnHelpTools.Font = MakeFontBold(CStr(g_fontName), 11.0!)
		btnHelpTools.Cursor = Cursors.Hand
		AddHandler btnHelpTools.Click, Sub(s, E)
										   Dim hDlg As New Form()
										   hDlg.Text = "BOM工具 工具使用说明"
										   hDlg.Size = New System.Drawing.Size(Scaled(520), Scaled(460))
										   hDlg.StartPosition = FormStartPosition.CenterScreen
										   hDlg.MinimizeBox = False
										   hDlg.MaximizeBox = False
										   hDlg.Font = MakeFont("Microsoft YaHei", 9.0!)
										   hDlg.BackColor = C(252, 252, 252)
										   Try
											   Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
											   If g_appIcon IsNot Nothing Then hDlg.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then hDlg.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
										   Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
										   End Try
										   Dim txtHelp As New System.Windows.Forms.TextBox()
										   txtHelp.Multiline = True
										   txtHelp.ReadOnly = True
										   txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
										   txtHelp.WordWrap = True
										   txtHelp.Location = New System.Drawing.Point(Scaled(10), Scaled(10))
										   txtHelp.Size = New System.Drawing.Size(hDlg.ClientSize.Width - Scaled(20), hDlg.ClientSize.Height - Scaled(60))
										   txtHelp.Font = MakeFont("Microsoft YaHei", 9.0!)
										   txtHelp.BackColor = C(248, 248, 250)
										   txtHelp.Text =
			"══════════════ BOM工具 工具使用说明 ══════════════" & vbCrLf &
			"" & vbCrLf &
			"【工具用途】" & vbCrLf &
			"  在 Inventor 装配环境下一键采集 BOM 清单，批量编辑零件属性，" & vbCrLf &
			"  自动写入图号、打印工程图、导出 PDF/DWG、与 Excel 互导。" & vbCrLf &
			"" & vbCrLf &
			"【启动条件】" & vbCrLf &
			"  1. 必须先打开一个装配体（.iam）并设为当前文档" & vbCrLf &
			"  2. 在 iLogic 浏览器里运行本规则（或绑定到按钮）" & vbCrLf &
			"  3. 工具窗口打开后仍可操作 Inventor（非模态）" & vbCrLf &
			"" & vbCrLf &
			"【主界面布局】" & vbCrLf &
			"  · 顶部标题栏：功能下拉、主题切换、Excel 导出/导入、刷新" & vbCrLf &
			"  · 中间表格区：BOM 数据，可直接双击单元格编辑" & vbCrLf &
			"  · 右侧列管理：勾选要显示的列、调整列顺序、添加自定义列" & vbCrLf &
			"  · 底部工具栏：筛选条件、行高列宽设置、写入属性按钮" & vbCrLf &
			"" & vbCrLf &
			"【标准工作流程】" & vbCrLf &
			"  第1步：打开装配体，运行规则，自动采集全部零件" & vbCrLf &
			"  第2步：在右侧列管理勾选需要的列（文件名/图号/材料等）" & vbCrLf &
			"  第3步：在表格里直接编辑，或用顶部功能批量处理" & vbCrLf &
			"  第4步：点底部""写入属性""把修改保存到零件文件" & vbCrLf &
			"  第5步：需要时导出 Excel / PDF / 打印工程图" & vbCrLf &
			"" & vbCrLf &
			"【功能下拉一览】（每个功能窗口内都有""使用说明""按钮）" & vbCrLf &
			"  [-] 批量删除属性：删除零件里多余的自定义属性" & vbCrLf &
			"  ✏ 自定义属性改名：把旧属性名整体改成新名" & vbCrLf &
			"  🔄 属性字符替换：批量查找替换文字，可重命名文件" & vbCrLf &
			"  🔢 写入图号：按规则自动编号" & vbCrLf &
			"  📄 属性写入工程图：把属性值填到标题栏" & vbCrLf &
			"  🖨 打印装配顺序：按装配顺序批量打印" & vbCrLf &
			"  📤 按属性导出PDF/DWG：筛选后批量导出" & vbCrLf &
			"  🔗 属性拼接写入：多个字段拼成一个字段" & vbCrLf &
			"  📊 BOM数量统计：统计并写入零件数量" & vbCrLf &
			"" & vbCrLf &
			"【注意事项】" & vbCrLf &
			"  · 修改后必须点""写入属性""才会真正保存到零件" & vbCrLf &
			"  · 重命名文件会自动处理关联工程图，请勿手动改名" & vbCrLf &
			"  · 标准件/只读零件的部分操作会被跳过并在报告里提示" & vbCrLf &
			"  · 主题和列设置自动保存在 ini，下次启动自动恢复" & vbCrLf &
			"  · 主题下拉可选「🎨 自定义主题」：上下标题/表格列头/列管理控件/" & vbCrLf &
			"    功能下拉/窗口背景/Excel导出按钮/刷新按钮/间隔行/白底行/表格文字 均可单独设置颜色"
										   hDlg.Controls.Add(txtHelp)
										   AddHandler hDlg.Shown, Sub(s2, e2) txtHelp.DeselectAll()
										   Dim btnHelpOk As New Button()
										   btnHelpOk.Text = "确定"
										   btnHelpOk.Size = New System.Drawing.Size(Scaled(90), Scaled(28))
										   btnHelpOk.Location = New System.Drawing.Point((hDlg.ClientSize.Width - Scaled(90)) \ 2, hDlg.ClientSize.Height - Scaled(40))
										   btnHelpOk.FlatStyle = FlatStyle.Flat
										   btnHelpOk.BackColor = OrangeMid()
										   btnHelpOk.ForeColor = System.Drawing.Color.White
										   AddHandler btnHelpOk.Click, Sub(s2, e2) hDlg.Close()
										   hDlg.Controls.Add(btnHelpOk)
										   hDlg.ShowDialog()
									   End Sub
		pnlTop.Controls.Add(btnHelpTools)
		' 主题下拉
		Dim cmbTheme As New System.Windows.Forms.ComboBox()
		cmbTheme.Size = New System.Drawing.Size(Scaled(108), Scaled(28))
		cmbTheme.Location = New System.Drawing.Point(clientW - RIGHT_W - GAP + Scaled(172), (TOP_H - Scaled(28)) \ 2)
		cmbTheme.Anchor = AnchorStyles.Right Or AnchorStyles.Top
		cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList
		cmbTheme.Font = MakeFont(CStr(g_fontName), 9.0!)
		cmbTheme.BackColor = System.Drawing.Color.White
		cmbTheme.Items.Add("紫色主题")
		cmbTheme.Items.Add("蓝色主题")
		cmbTheme.Items.Add("绿色主题")
		cmbTheme.Items.Add("红色主题")
		cmbTheme.Items.Add("橙色主题")
		cmbTheme.Items.Add("粉色主题")
		cmbTheme.Items.Add("🌈 呼吸灯")
		cmbTheme.Items.Add("🌙 暗黑主题")
		cmbTheme.Items.Add("🎨 自定义主题")
		g_cmbTheme = cmbTheme
		cmbTheme.SelectedIndex = g_theme
		AddHandler cmbTheme.SelectedIndexChanged, Sub(o As Object, ev As EventArgs)
													  Dim idx As Integer = CType(o, System.Windows.Forms.ComboBox).SelectedIndex
													  If idx = 6 Then
														  g_theme = 6
														  ApplyThemeToForm()
														  StartBreathTheme()
														  SaveThemeIni()
													  ElseIf idx = 8 Then
														  If g_isLoading Then
															  ' 载入阶段：直接应用已保存的自定义颜色，不弹窗
															  g_theme = 8
															  StopBreathTheme()
															  ApplyThemeToForm()
															  Return
														  End If
														  Dim prevTheme As Integer = g_theme
														  g_theme = 8
														  If ShowCustomThemeDialog() Then
															  StopBreathTheme()
															  Try
																  ApplyThemeToForm()
															  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
															  End Try
															  Try
																  SaveThemeIni()
															  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
															  End Try
														  Else
															  ' 取消：回到之前的主题
															  g_theme = prevTheme
															  cmbTheme.SelectedIndex = prevTheme
														  End If
													  Else
														  g_theme = idx
														  StopBreathTheme()
														  ApplyThemeToForm()
														  SaveThemeIni()
													  End If
												  End Sub
		pnlTop.Controls.Add(cmbTheme)

		' 自定义主题设置按钮（主题下拉左边）
		Dim btnThemeSet As New Button()
		btnThemeSet.Text = "🎨"
		btnThemeSet.Size = New System.Drawing.Size(Scaled(28), Scaled(28))
		btnThemeSet.Location = New System.Drawing.Point(cmbTheme.Left - Scaled(33), (TOP_H - Scaled(28)) \ 2)
		btnThemeSet.Anchor = AnchorStyles.Right Or AnchorStyles.Top
		btnThemeSet.FlatStyle = FlatStyle.Flat
		btnThemeSet.FlatAppearance.BorderSize = 0
		btnThemeSet.BackColor = OrangeMid()
		btnThemeSet.ForeColor = System.Drawing.Color.White
		btnThemeSet.Font = MakeFont(CStr(g_fontName), 10.0!)
		btnThemeSet.Cursor = Cursors.Hand
		Dim ttThemeSet As New ToolTip()
		ttThemeSet.InitialDelay = 500
		ttThemeSet.AutoPopDelay = 6000
		ttThemeSet.ShowAlways = True
		ttThemeSet.SetToolTip(btnThemeSet, "自定义主题颜色设置（上下标题/列头/列管理/功能下拉/窗口背景/Excel按钮/刷新/间隔行/白底行/表格文字）")
		AddHandler btnThemeSet.Click, Sub(s As Object, e As EventArgs)
										  If g_theme = 8 Then
											  ' 已在自定义主题：直接再次打开设置
											  If ShowCustomThemeDialog() Then
												  StopBreathTheme()
												  Try
													  ApplyThemeToForm()
												  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
												  End Try
												  Try
													  SaveThemeIni()
												  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
												  End Try
											  End If
										  Else
											  ' 自动切到自定义主题（下拉切换会弹出颜色设置）
											  cmbTheme.SelectedIndex = 8
										  End If
									  End Sub
		pnlTop.Controls.Add(btnThemeSet)

		' ── 右侧列管理面板 ──
		g_chkPanel = New Panel()
		g_chkPanel.Location = New System.Drawing.Point(chkX, chkY)
		g_chkPanel.Size = New System.Drawing.Size(RIGHT_W, chkH)
		g_chkPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Bottom
		g_chkPanel.BackColor = PanelBg()
		g_frm.Controls.Add(g_chkPanel)

		Dim lblPanelTitle As New Label()
		lblPanelTitle.Text = "列管理"
		lblPanelTitle.Location = New System.Drawing.Point(0, 0)
		lblPanelTitle.Size = New System.Drawing.Size(RIGHT_W, Scaled(26))
		lblPanelTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
		lblPanelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		lblPanelTitle.BackColor = PanelTitleBg()
		lblPanelTitle.ForeColor = System.Drawing.Color.White
		lblPanelTitle.Font = MakeFontBold(CStr(g_fontName), 9.0!)
		g_chkPanel.Controls.Add(lblPanelTitle)

		Dim addInnerW As Integer = RIGHT_W - PANEL_PAD * 2

		' ── 上下移动按钮区域 ──
		Dim pnlUpDown As New Panel()
		pnlUpDown.Size = New System.Drawing.Size(RIGHT_W, Scaled(30))
		pnlUpDown.Location = New System.Drawing.Point(0, chkH - Scaled(130))
		pnlUpDown.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		pnlUpDown.BackColor = PanelBg()
		g_chkPanel.Controls.Add(pnlUpDown)
		Dim btnUp As New Button()
		btnUp.Text = "▲"
		btnUp.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
		btnUp.Location = New System.Drawing.Point(Scaled(4), Scaled(2))
		btnUp.FlatStyle = FlatStyle.Flat
		btnUp.BackColor = PanelTitleBg()
		btnUp.ForeColor = System.Drawing.Color.White
		btnUp.Font = MakeFont(FONT_NAME, 9.0!)
		AddHandler btnUp.Click, AddressOf OnMoveColUp
		pnlUpDown.Controls.Add(btnUp)
		Dim btnDown As New Button()
		btnDown.Text = "▼"
		btnDown.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
		btnDown.Location = New System.Drawing.Point(Scaled(68), Scaled(2))
		btnDown.FlatStyle = FlatStyle.Flat
		btnDown.BackColor = PanelTitleBg()
		btnDown.ForeColor = System.Drawing.Color.White
		btnDown.Font = MakeFont(FONT_NAME, 9.0!)
		AddHandler btnDown.Click, AddressOf OnMoveColDown
		pnlUpDown.Controls.Add(btnDown)

		Dim pnlAdd As New Panel()
		pnlAdd.Size = New System.Drawing.Size(RIGHT_W, Scaled(100))
		pnlAdd.Location = New System.Drawing.Point(0, chkH - Scaled(100))
		pnlAdd.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		pnlAdd.BackColor = PanelAddBg()
		g_chkPanel.Controls.Add(pnlAdd)

		Dim lblAddTitle As New Label()
		lblAddTitle.Text = ChrW(28155) & ChrW(21152) & ChrW(23646) & ChrW(24615) & ChrW(21015) & "  " & ChrW(40) & ChrW(21015) & ChrW(22836) & ChrW(21517) & ChrW(32) & ChrW(23646) & ChrW(24615) & ChrW(21517) & ChrW(41)
		lblAddTitle.Location = New System.Drawing.Point(PANEL_PAD, Scaled(4))
		lblAddTitle.Size = New System.Drawing.Size(addInnerW, Scaled(18))
		lblAddTitle.ForeColor = System.Drawing.Color.Black
		lblAddTitle.BackColor = PanelAddBg()
		pnlAdd.Controls.Add(lblAddTitle)

		g_txtProp = New System.Windows.Forms.ComboBox()
		g_txtProp.Location = New System.Drawing.Point(PANEL_PAD, Scaled(26))
		g_txtProp.Size = New System.Drawing.Size(addInnerW, Scaled(22))
		g_txtProp.DropDownStyle = ComboBoxStyle.DropDown
		g_txtProp.AutoCompleteMode = AutoCompleteMode.SuggestAppend
		g_txtProp.AutoCompleteSource = AutoCompleteSource.ListItems
		' 添加默认提示项
		' Inventor常用属性（中文显示，实际用英文属性名查询）
		Dim invProps() As String = {
	"零件代号 (Part Number)",
	"描述 (Description)",
	"材料 (Material)",
	"质量(kg) (Mass)",
	"供应商 (Vendor)",
	"成本 (Cost)",
	"库存编号 (Stock Number)",
	"标准 (Standard)",
	"设计者 (Designer)",
	"工程师 (Engineer)",
	"审核 (Authority)",
	"创建时间 (Creation Time)",
	"项目 (Project)",
	"公司 (Company)",
	"类别 (Category)",
	"关键字 (Keywords)",
	"注释 (Comments)",
	"标题 (Title)",
	"表面积(cm²) (SurfaceArea)",
	"体积(cm³) (Volume)",
	"密度 (Density)",
	"图号",
	"级别序号",
	"── 内置可选列 ──",
	"统计数量 [cQty]",
	"文件名 [cFile]",
	"工程图是否有 [cDwg]",
	"BOM结构 [cBom]",
	"缩略图 [cThumb]",
	"── 自定义属性 ──",
	"数量"
	}
		For Each p As String In invProps
			g_txtProp.Items.Add(p)
		Next
		' ── 扫描所有零件文件的自定义属性，追加到下拉列表底部 ──
		Try
			If g_rows IsNot Nothing AndAlso g_rows.Count > 0 Then
				Dim custSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
				For Each existItem As Object In g_txtProp.Items
					custSet.Add(CStr(existItem))
				Next
				Dim newCustProps As New System.Collections.Generic.List(Of String)()
				Dim seenFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
				Dim udpGuid As String = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"
				For Each rr As BomDataRow In g_rows
					If String.IsNullOrEmpty(rr.FilePath) OrElse rr.IsMissing Then Continue For
					If seenFp.Contains(rr.FilePath) Then Continue For
					seenFp.Add(rr.FilePath)
					' 先从内存Custom字典读'
					For Each kv As System.Collections.Generic.KeyValuePair(Of String, String) In rr.Custom
						If Not String.IsNullOrEmpty(kv.Key) AndAlso Not custSet.Contains(kv.Key) Then
							custSet.Add(kv.Key) : newCustProps.Add(kv.Key)
						End If
					Next
					' 再从文件读UDP（确保找全）'
					Try
						Dim tDoc As Document = Nothing
						For Each od As Document In ThisApplication.Documents
							If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then tDoc = od : Exit For
						Next
						If tDoc Is Nothing AndAlso System.IO.File.Exists(rr.FilePath) Then
							tDoc = ThisApplication.Documents.Open(rr.FilePath, False)
						End If
						If tDoc IsNot Nothing Then
							Try
								Dim udp As PropertySet = tDoc.PropertySets.Item(udpGuid)
								For Each prop As Inventor.Property In udp
									Try
										Dim pn As String = prop.DisplayName.Trim()
										If String.IsNullOrEmpty(pn) Then pn = prop.Name.Trim()
										If Not String.IsNullOrEmpty(pn) AndAlso Not custSet.Contains(pn) Then
											custSet.Add(pn) : newCustProps.Add(pn)
										End If
									Catch : End Try
								Next
							Catch : End Try
							' 读完关闭（如果是本次打开的）'
						End If
					Catch : End Try
				Next
				If newCustProps.Count > 0 Then
					newCustProps.Sort()
					For Each cp As String In newCustProps : g_txtProp.Items.Add(cp) : Next
				End If
			End If
		Catch : End Try
		' ── 灰色提示文字：用WndProc消息钩子实现（ComboBox无PlaceholderText）──
		Dim hintText As String = "选择或输入属性名..."
		AddHandler g_txtProp.DrawItem, Sub(ds As Object, de As DrawItemEventArgs)
										   ' 不处理列表项绘制，只处理编辑框
									   End Sub
		' 用TextBox子控件实现提示（ComboBox内部有TextBox）
		AddHandler g_txtProp.Enter, Sub(se As Object, ee As EventArgs)
										' 获得焦点时如果是提示文字则清空
										If g_txtProp.Text = hintText Then
											g_txtProp.Text = ""
											g_txtProp.ForeColor = System.Drawing.Color.Black
										End If
									End Sub
		AddHandler g_txtProp.Leave, Sub(sl As Object, el As EventArgs)
										' 失去焦点且为空时显示提示
										If String.IsNullOrEmpty(g_txtProp.Text.Trim()) Then
											g_txtProp.ForeColor = System.Drawing.Color.Gray
											g_txtProp.Text = hintText
										End If
									End Sub
		' 初始显示提示文字
		g_txtProp.ForeColor = System.Drawing.Color.Gray
		g_txtProp.Text = hintText

		pnlAdd.Controls.Add(g_txtProp)

		Dim btnAdd As New Button()
		btnAdd.Text = "+ 添加列"
		btnAdd.Location = New System.Drawing.Point(PANEL_PAD, Scaled(54))
		btnAdd.Size = New System.Drawing.Size(addInnerW, Scaled(36))
		btnAdd.FlatStyle = FlatStyle.Flat
		btnAdd.BackColor = BtnAddBg()
		btnAdd.ForeColor = System.Drawing.Color.White
		btnAdd.Font = MakeFontBold(CStr(g_fontName), 8.5!)
		AddHandler btnAdd.Click, AddressOf OnAddCustomCol
		pnlAdd.Controls.Add(btnAdd)

		g_chkFlow = New FlowLayoutPanel()
		g_chkFlow.Location = New System.Drawing.Point(0, Scaled(26))
		g_chkFlow.Size = New System.Drawing.Size(RIGHT_W, chkH - Scaled(26) - Scaled(130))
		g_chkFlow.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		g_chkFlow.FlowDirection = FlowDirection.TopDown
		g_chkFlow.WrapContents = False
		g_chkFlow.AutoScroll = True
		g_chkFlow.BackColor = PanelBg()
		g_chkFlow.Padding = New Padding(Scaled(4), Scaled(4), 0, 0)
		g_chkPanel.Controls.Add(g_chkFlow)

		For Each cd As ColDef In g_colDefs
			If cd.IsFixed Then Continue For
			AddCheckRow(cd)
		Next

		' ── 底部工具栏 ──
		Dim pnlBottom As New Panel()
		pnlBottom.Location = New System.Drawing.Point(botX, botY)
		pnlBottom.Size = New System.Drawing.Size(botW, BOT_H)
		pnlBottom.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		pnlBottom.BackColor = ToolBarBg()
		g_pnlBottom = pnlBottom
		Try
			Dim pb As System.Type = pnlBottom.GetType()
			Dim pib As System.Reflection.PropertyInfo = pb.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
			If pib IsNot Nothing Then pib.SetValue(pnlBottom, True, Nothing)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
		AddHandler pnlBottom.Paint, AddressOf PnlBottomPaint
		g_frm.Controls.Add(pnlBottom)

		Dim tx As Integer = Scaled(10)

		Dim lblRowH As New Label()
		lblRowH.Text = ChrW(20840) & ChrW(34892) & ChrW(39640)
		lblRowH.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
		lblRowH.Location = New System.Drawing.Point(tx, Scaled(5))
		lblRowH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		lblRowH.ForeColor = System.Drawing.Color.Black
		pnlBottom.Controls.Add(lblRowH)
		tx += Scaled(62)

		g_txtRowH = New System.Windows.Forms.TextBox()
		g_txtRowH.Size = New System.Drawing.Size(Scaled(46), Scaled(22))
		g_txtRowH.Location = New System.Drawing.Point(tx, Scaled(7))
		g_txtRowH.Text = DEFAULT_ROW_H.ToString()
		g_txtRowH.TextAlign = HorizontalAlignment.Center
		pnlBottom.Controls.Add(g_txtRowH)
		tx += Scaled(50)

		Dim btnApplyRowH As New Button()
		btnApplyRowH.Text = "应用"
		btnApplyRowH.Size = New System.Drawing.Size(Scaled(44), Scaled(22))
		btnApplyRowH.Location = New System.Drawing.Point(tx, Scaled(7))
		btnApplyRowH.FlatStyle = FlatStyle.Flat
		btnApplyRowH.BackColor = OrangeMid()
		btnApplyRowH.ForeColor = System.Drawing.Color.White
		btnApplyRowH.Font = MakeFont(CStr(g_fontName), 8.0!)
		AddHandler btnApplyRowH.Click, AddressOf OnApplyRowHeight
		pnlBottom.Controls.Add(btnApplyRowH)
		tx += Scaled(52)

		Dim sep1 As New Label()
		sep1.BorderStyle = BorderStyle.Fixed3D
		sep1.Size = New System.Drawing.Size(1, Scaled(26))
		sep1.Location = New System.Drawing.Point(tx, Scaled(5))
		pnlBottom.Controls.Add(sep1)
		tx += Scaled(10)

		Dim lblColW As New Label()
		lblColW.Text = ChrW(20840) & ChrW(21015) & ChrW(23485)
		lblColW.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
		lblColW.Location = New System.Drawing.Point(tx, Scaled(5))
		lblColW.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		lblColW.ForeColor = System.Drawing.Color.Black
		pnlBottom.Controls.Add(lblColW)
		tx += Scaled(62)

		g_txtColW = New System.Windows.Forms.TextBox()
		g_txtColW.Size = New System.Drawing.Size(Scaled(46), Scaled(22))
		g_txtColW.Location = New System.Drawing.Point(tx, Scaled(7))
		g_txtColW.Text = "100"
		g_txtColW.TextAlign = HorizontalAlignment.Center
		pnlBottom.Controls.Add(g_txtColW)
		tx += Scaled(50)

		Dim btnApplyColW As New Button()
		btnApplyColW.Text = "应用"
		btnApplyColW.Size = New System.Drawing.Size(Scaled(44), Scaled(22))
		btnApplyColW.Location = New System.Drawing.Point(tx, Scaled(7))
		btnApplyColW.FlatStyle = FlatStyle.Flat
		btnApplyColW.BackColor = OrangeMid()
		btnApplyColW.ForeColor = System.Drawing.Color.White
		btnApplyColW.Font = MakeFont(CStr(g_fontName), 8.0!)
		AddHandler btnApplyColW.Click, AddressOf OnApplyAllColWidth
		pnlBottom.Controls.Add(btnApplyColW)
		tx += Scaled(52)

		Dim sep2 As New Label()
		sep2.BorderStyle = BorderStyle.Fixed3D
		sep2.Size = New System.Drawing.Size(1, Scaled(26))
		sep2.Location = New System.Drawing.Point(tx, Scaled(5))
		pnlBottom.Controls.Add(sep2)
		tx += Scaled(10)

		Dim btnAutoAll As New Button()
		btnAutoAll.Text = "全部列自适应"
		btnAutoAll.Size = New System.Drawing.Size(Scaled(120), Scaled(22))
		btnAutoAll.Location = New System.Drawing.Point(tx, Scaled(7))
		btnAutoAll.FlatStyle = FlatStyle.Flat
		btnAutoAll.BackColor = C(108, 92, 231)
		btnAutoAll.ForeColor = System.Drawing.Color.White
		btnAutoAll.Font = MakeFont(CStr(g_fontName), 8.5!)
		AddHandler btnAutoAll.Click, AddressOf OnAutoAllColWidth
		pnlBottom.Controls.Add(btnAutoAll)

		Dim sep3 As New Label()
		sep3.BorderStyle = BorderStyle.Fixed3D
		' btnThumb 已移至顶部功能栏

		Dim sepH As New Label()
		sepH.BorderStyle = BorderStyle.Fixed3D
		sepH.Size = New System.Drawing.Size(2000, 1)
		sepH.Location = New System.Drawing.Point(0, Scaled(36))
		pnlBottom.Controls.Add(sepH)

		g_status = New Label()
		g_status.AutoSize = False
		g_status.Width = Scaled(360)
		g_status.Height = Scaled(34)
		g_status.Location = New System.Drawing.Point(Scaled(8), Scaled(42))
		g_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		g_status.Text = "  共 " & g_rows.Count.ToString() & " 个唯一零部件"
		g_status.ForeColor = C(80, 50, 180)
		pnlBottom.Controls.Add(g_status)

		Dim btnCancel As New Button()
		btnCancel.Text = ""
		btnCancel.Size = New System.Drawing.Size(Scaled(160), Scaled(28))
		' 太极旋转角度和Timer
		Dim taijiAngle As Single = 0
		Dim bmpTaijiCache As System.Drawing.Bitmap = Nothing
		Dim bmpTaijiSize As Integer = 0
		Dim taijiTimer As New System.Windows.Forms.Timer()
		taijiTimer.Interval = 30
		AddHandler taijiTimer.Tick, Sub(st, et)
										taijiAngle = (taijiAngle + 3) Mod 360
										btnCancel.Invalidate()
									End Sub
		taijiTimer.Start()
		' 窗口关闭时停止Timer
		AddHandler g_frm.FormClosed, Sub(sfc, efc)
										 taijiTimer.Stop() : taijiTimer.Dispose()
									 End Sub
		AddHandler btnCancel.Paint, Sub(sp As Object, ep As System.Windows.Forms.PaintEventArgs)
										Dim btn As Button = CType(sp, Button)
										ep.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
										' 画☯大图标（旋转90度）+ 文字，整体居中
										Dim iconFont As New System.Drawing.Font(g_fontName, 16.0!, System.Drawing.FontStyle.Regular)
										Dim txtFont As New System.Drawing.Font(g_fontName, 8.5!, System.Drawing.FontStyle.Regular)
										Dim iconStr As String = ChrW(9775)
										Dim txtStr As String = " 刷新全表格 V59.2.0"
										Dim iconSz As System.Drawing.SizeF = ep.Graphics.MeasureString(iconStr, iconFont)
										Dim txtSz As System.Drawing.SizeF = ep.Graphics.MeasureString(txtStr, txtFont)
										Dim bmpSz As Integer = CInt(Math.Max(iconSz.Width, iconSz.Height)) + 8
										If bmpSz < 32 Then bmpSz = 32
										Dim totalW As Single = bmpSz + txtSz.Width - 4
										Dim startX As Single = (btn.Width - totalW) / 2
										Dim txtY As Single = (btn.Height - txtSz.Height) / 2
										' 旋转画☯（位图方式，中心完全可控）
										Dim cx As Single = startX + bmpSz / 2.0F
										Dim cy As Single = btn.Height / 2.0F
										If bmpTaijiCache Is Nothing OrElse bmpTaijiSize <> bmpSz Then
											If bmpTaijiCache IsNot Nothing Then bmpTaijiCache.Dispose()
											bmpTaijiSize = bmpSz
											bmpTaijiCache = New System.Drawing.Bitmap(bmpSz, bmpSz, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
											Dim gBmp As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpTaijiCache)
											gBmp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
											gBmp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
											Dim sfC2 As New System.Drawing.StringFormat()
											sfC2.Alignment = System.Drawing.StringAlignment.Center
											sfC2.LineAlignment = System.Drawing.StringAlignment.Center
											gBmp.DrawString(iconStr, iconFont, System.Drawing.Brushes.White, New System.Drawing.RectangleF(0, 0, bmpSz, bmpSz), sfC2)
											gBmp.Dispose()
										End If
										ep.Graphics.TranslateTransform(cx, cy)
										ep.Graphics.RotateTransform(taijiAngle)
										ep.Graphics.DrawImage(bmpTaijiCache, -bmpSz / 2.0F, -bmpSz / 2.0F, bmpSz, bmpSz)
										ep.Graphics.ResetTransform()
										' 画文字
										ep.Graphics.DrawString(txtStr, txtFont, System.Drawing.Brushes.White, startX + bmpSz - 6, txtY)
										iconFont.Dispose() : txtFont.Dispose()
									End Sub
		btnCancel.Location = New System.Drawing.Point(botW - Scaled(170), (BOT_H - Scaled(36) - Scaled(28)) \ 2 + Scaled(36) + Scaled(4))
		btnCancel.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom
		btnCancel.FlatStyle = FlatStyle.Flat
		btnCancel.BackColor = C(225, 112, 85)
		btnCancel.ForeColor = System.Drawing.Color.White
		AddHandler btnCancel.Click, Sub(s, e)
										DoFullRefresh()
									End Sub
		g_btnRefresh = btnCancel
		pnlBottom.Controls.Add(btnCancel)



		' ── 主表格 ──
		g_grid = New DataGridView()
		Try
			Dim gd As System.Type = g_grid.GetType()
			Dim pigd As System.Reflection.PropertyInfo = gd.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
			If pigd IsNot Nothing Then pigd.SetValue(g_grid, True, Nothing)
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
		g_grid.Location = New System.Drawing.Point(gridX, gridY)
		g_grid.Size = New System.Drawing.Size(gridW, gridH)
		g_grid.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		g_grid.AllowUserToAddRows = False
		g_grid.AllowUserToDeleteRows = False
		g_grid.RowHeadersVisible = False
		g_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
		g_grid.SelectionMode = DataGridViewSelectionMode.CellSelect
		g_grid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
		g_grid.EnableHeadersVisualStyles = False
		g_grid.ColumnHeadersHeight = Scaled(30)
		g_grid.RowTemplate.Height = DEFAULT_ROW_H
		g_grid.AllowUserToOrderColumns = True

		Dim grid_hdrSt As DataGridViewCellStyle = g_grid.ColumnHeadersDefaultCellStyle
		grid_hdrSt.BackColor = OrangeMid()
		grid_hdrSt.ForeColor = System.Drawing.Color.White
		grid_hdrSt.Font = MakeFontBold(CStr(g_fontName), 9.0!)

		Dim grid_altSt As DataGridViewCellStyle = g_grid.AlternatingRowsDefaultCellStyle
		grid_altSt.BackColor = C(243, 240, 255)

		Dim grid_defSt As DataGridViewCellStyle = g_grid.DefaultCellStyle
		grid_defSt.SelectionBackColor = C(180, 165, 245)
		grid_defSt.SelectionForeColor = C(255, 255, 255)

		g_frm.Controls.Add(g_grid)

		Dim grayBg As System.Drawing.Color = System.Drawing.Color.White
		For Each cd As ColDef In g_colDefs
			BuildGridColumn(cd, grayBg)
		Next

		' cFile 列不再单独设白底，跟随主题（白底行/间隔行/文字色）自动生效
		' 所有列默认可见
		For Each col As DataGridViewColumn In g_grid.Columns
			col.Visible = True
		Next

		RefreshGridRows()
		LoadTypeIcons()
		LoadMaterialList()
		AddHandler g_grid.CellPainting, AddressOf OnTypeCellPainting
		AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		AddHandler g_grid.CellEndEdit, AddressOf OnCellLeave_Pno
		' 忽略ComboBox值无效错误（值不在列表时不报错）
		AddHandler g_grid.DataError, AddressOf OnGridDataError

		Dim ctxMenu As New ContextMenuStrip()
		ctxMenu.Font = MakeFont(CStr(g_fontName), 9.5!)
		Dim menuOpen As New ToolStripMenuItem("打开选中零部件（支持多选）")
		menuOpen.Font = MakeFont(CStr(g_fontName), 9.5!)
		AddHandler menuOpen.Click, AddressOf OnGridOpenDoc
		ctxMenu.Items.Add(menuOpen)
		ctxMenu.Items.Add(New ToolStripSeparator())
		Dim menuOpenDwg As New ToolStripMenuItem("打开工程图（支持多选）")
		menuOpenDwg.Font = MakeFont(CStr(g_fontName), 9.5!)
		AddHandler menuOpenDwg.Click, Sub(s, e)
										  ' 收集所有选中行（支持多选）
										  Dim selRows As New HashSet(Of Integer)()
										  For Each cell As DataGridViewCell In g_grid.SelectedCells
											  selRows.Add(Cell.RowIndex)
										  Next
										  If selRows.Count = 0 Then
											  Dim ci As Integer = If(g_grid.CurrentCell IsNot Nothing, g_grid.CurrentCell.RowIndex, -1)
											  If ci >= 0 Then selRows.Add(ci)
										  End If
										  If selRows.Count = 0 Then Return
										  Dim openCount As Integer = 0
										  Dim noDrawList As New List(Of String)()
										  Dim seenFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rowIdx As Integer In selRows
											  Dim origIdx As Integer = GetOrigRowIdx(rowIdx)
											  If origIdx < 0 OrElse origIdx >= g_rows.Count Then Continue For
											  Dim rr As BomDataRow = g_rows(origIdx)
											  If String.IsNullOrEmpty(rr.FilePath) OrElse rr.IsMissing Then Continue For
											  If seenFp.Contains(rr.FilePath) Then Continue For
											  seenFp.Add(rr.FilePath)
											  Dim partDir As String = System.IO.Path.GetDirectoryName(rr.FilePath)
											  Dim partBase As String = System.IO.Path.GetFileNameWithoutExtension(rr.FilePath)
											  Dim dwgPath As String = ""
											  For Each ext As String In New String() {".idw", ".dwg"}
												  Dim candidate As String = System.IO.Path.Combine(partDir, partBase & ext)
												  If System.IO.File.Exists(candidate) Then dwgPath = candidate : Exit For
											  Next
											  If String.IsNullOrEmpty(dwgPath) Then dwgPath = FindDrawingInProject(partBase)
											  If String.IsNullOrEmpty(dwgPath) Then
												  noDrawList.Add(partBase)
												  Continue For
											  End If

											  Try
												  ThisApplication.Documents.Open(dwgPath, True)
												  openCount += 1
											  Catch : End Try
										  Next
										  ' 结果提示
										  If openCount > 0 AndAlso noDrawList.Count = 0 Then
											  SetStatus("  已打开 " & openCount & " 个工程图", C(0, 130, 55))
										  ElseIf openCount = 0 AndAlso noDrawList.Count > 0 Then
											  MessageBox.Show("未找到工程图：" & vbCrLf & String.Join(vbCrLf, noDrawList.ToArray()), "没有工程图", MessageBoxButtons.OK, MessageBoxIcon.Information)
										  ElseIf noDrawList.Count > 0 Then
											  SetStatus("  已打开 " & openCount & " 个，" & noDrawList.Count & " 个无工程图", C(150, 100, 0))
										  End If
										  ' 打开工程图后            ' 打开工程图后关闭BOM窗口'
										  If openCount > 0 Then
											  Dim _frmRef As System.Windows.Forms.Form = g_frm
											  Dim _t As New System.Windows.Forms.Timer()
											  _t.Interval = 150
											  AddHandler _t.Tick, Sub(ts, te)
																	  _t.Stop()
																	  Try : _frmRef.Close() : Catch : End Try
																  End Sub
											  _t.Start()
										  End If
									  End Sub
		ctxMenu.Items.Add(menuOpenDwg)
		ctxMenu.Items.Add(New ToolStripSeparator())
		Dim menuCopy As New ToolStripMenuItem(ChrW(22797) & ChrW(21046) & "  Ctrl+C")
		menuCopy.Font = MakeFont(CStr(g_fontName), 9.5!)
		AddHandler menuCopy.Click, AddressOf OnGridCopy
		ctxMenu.Items.Add(menuCopy)
		Dim menuPaste As New ToolStripMenuItem(ChrW(31896) & ChrW(36148) & "  Ctrl+V")
		menuPaste.Font = MakeFont(CStr(g_fontName), 9.5!)
		AddHandler menuPaste.Click, AddressOf OnGridPaste
		ctxMenu.Items.Add(menuPaste)
		g_grid.ContextMenuStrip = ctxMenu
		AddHandler g_grid.MouseDown, AddressOf OnGridMouseDown
		AddHandler g_grid.ColumnDisplayIndexChanged, AddressOf OnColumnOrderChanged
		AddHandler g_grid.ColumnWidthChanged, AddressOf OnColumnWidthChanged
		AddHandler g_grid.CellMouseEnter, AddressOf OnCellMouseEnter
		AddHandler g_grid.CellMouseLeave, AddressOf OnCellMouseLeave
		AddHandler g_grid.CellClick, AddressOf OnCellClick
		AddHandler g_grid.MouseWheel, AddressOf OnGridMouseWheel
		AddHandler g_grid.Scroll, AddressOf OnGridScroll
		AddHandler g_grid.SortCompare, AddressOf OnGridSortCompare
		AddHandler g_grid.ColumnHeaderMouseClick, Sub(shc As Object, ehc As DataGridViewCellMouseEventArgs)
													  g_grid.BeginInvoke(New System.Action(Sub()
																							   Try
																								   g_sortedOrder.Clear()
																								   For Each dgvRow As DataGridViewRow In g_grid.Rows
																									   Dim oi As Integer = GetOrigRowIdx(dgvRow.Index)
																									   If oi >= 0 AndAlso oi < g_rows.Count Then
																										   Dim fp As String = g_rows(oi).FilePath
																										   If Not g_sortedOrder.Contains(fp) Then g_sortedOrder.Add(fp)
																									   End If
																								   Next
																							   Catch : End Try
																						   End Sub))
												  End Sub

		AddHandler g_frm.Shown, Sub(s, e)
									LoadSettings()
									ApplyThemeToForm()
									If g_theme = 6 Then StartBreathTheme()
									g_grid.Focus()
									If System.IO.File.Exists(g_settingsFile) Then
										SetStatus("  已恢复上次列设置", C(0, 100, 60))
									End If
									' 启动时自动加载内置预览图（从文件二进制流读取，不需要打开文档，极快）
									If g_grid.Columns.Contains("cThumb") AndAlso g_grid.Columns("cThumb").Visible Then
										Dim tAuto As New System.Threading.Thread(Sub()
																					 Try
																						 System.Threading.Thread.Sleep(200)
																						 For Each rr As BomDataRow In g_rows
																							 If rr.Thumbnail IsNot Nothing Then Continue For
																							 If String.IsNullOrEmpty(rr.FilePath) Then Continue For
																							 ' 从文件二进制流读取内置预览图（瞬间完成）
																							 rr.Thumbnail = GetInventorThumbnail(rr.FilePath)
																							 If rr.Thumbnail Is Nothing Then rr.Thumbnail = GetShellThumbnail(rr.FilePath)
																							 If rr.Thumbnail IsNot Nothing AndAlso g_grid IsNot Nothing AndAlso Not g_grid.IsDisposed Then
																								 Dim capturedRr As BomDataRow = rr
																								 g_grid.Invoke(Sub()
																												   Try
																													   For Each dgvRow As DataGridViewRow In g_grid.Rows
																														   Dim oi As Integer = GetOrigRowIdx(dgvRow.Index)
																														   If oi >= 0 AndAlso oi < g_rows.Count AndAlso g_rows(oi) Is capturedRr Then
																															   If g_grid.Columns.Contains("cThumb") Then dgvRow.Cells("cThumb").Value = capturedRr.Thumbnail
																															   Exit For
																														   End If
																													   Next
																												   Catch : End Try
																											   End Sub)
																							 End If
																							 System.Threading.Thread.Sleep(5)
																						 Next
																					 Catch : End Try
																				 End Sub)
										tAuto.IsBackground = True
										tAuto.Start()
									End If
								End Sub

		g_frm.Show()  ' 非模态：打开BOM窗后仍可操作Inventor
		Do While g_frm IsNot Nothing AndAlso Not g_frm.IsDisposed
			System.Threading.Thread.Sleep(15)
			System.Windows.Forms.Application.DoEvents()
		Loop

	End Sub

	' ★ 单元格内容变化时标记 g_isDirty
	Sub OnGridDataError(sender As Object, e As DataGridViewDataErrorEventArgs)
		' 忽略ComboBox值无效错误，不弹出对话框
		e.ThrowException = False
		e.Cancel = True
	End Sub

	' ★ 零件代号专用写入：离开 cPno 单元格时立即写入并保存
	' ★ 零件代号专用写入：离开 cPno 单元格时立即写入并保存
	Sub OnCellLeave_Pno(sender As Object, e As DataGridViewCellEventArgs)
		Try
			If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
			If g_isRefreshing OrElse g_isLoading Then Return
			' 找当前编辑列对应的 ColDef
			Dim editColName As String = g_grid.Columns(e.ColumnIndex).Name
			' 判断是否是零件代号列：
			' 条件1：内置 cPno 列
			' 条件2：自定义列且 PropName = "Part Number"
			Dim isPnoCol As Boolean = False
			Dim propNameForWrite As String = "Part Number"
			If editColName = "cPno" Then
				isPnoCol = True
			Else
				For Each cd As ColDef In g_colDefs
					If cd.ColName = editColName AndAlso cd.IsCustom AndAlso
						String.Compare(cd.PropName, "Part Number", True) = 0 Then
						isPnoCol = True : Exit For
					End If
				Next
			End If
			If Not isPnoCol Then Return
			' 找对应 BomDataRow
			Dim origIdx As Integer = GetOrigRowIdx(e.RowIndex)
			If origIdx < 0 OrElse origIdx >= g_rows.Count Then Return
			Dim rr As BomDataRow = g_rows(origIdx)
			If rr.IsContentCenter OrElse rr.IsLocalStandard OrElse rr.IsMissing Then Return
			' 读新值
			Dim cellVal As String = SafeStr(g_grid.Rows(e.RowIndex).Cells(editColName).Value).Trim()
			Dim oldVal As String = If(rr.PartNumber Is Nothing, "", rr.PartNumber.Trim())
			If cellVal = oldVal OrElse String.IsNullOrEmpty(cellVal) Then Return
			' 找文档
			Dim tDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then
					tDoc = od : Exit For
				End If
			Next
			If tDoc Is Nothing AndAlso System.IO.File.Exists(rr.FilePath) Then
				tDoc = ThisApplication.Documents.Open(rr.FilePath, True)
			End If
			If tDoc Is Nothing Then
				SetStatus("  ✗ 无法打开: " & rr.FileName, C(180, 60, 0)) : Return
			End If
			' 写入 Part Number 到 Design Tracking Properties
			Try
				tDoc.PropertySets.Item(GUID_DTP).Item("Part Number").Value = cellVal
				rr.PartNumber = cellVal
				' 同步更新内置 cPno 列（如果存在）
				If g_grid.Columns.Contains("cPno") Then
					g_grid.Rows(e.RowIndex).Cells("cPno").Value = cellVal
				End If
			Catch exW As Exception
				SetStatus("  ✗ 零件代号写入失败: " & exW.Message, C(180, 60, 0)) : Return
			End Try
			' 保存
			Try
				tDoc.Save()
				SetStatus("  ✓ 零件代号已保存: " & rr.FileName & "  [" & oldVal & " → " & cellVal & "]", C(0, 140, 60))
			Catch exSave As Exception
				SetStatus("  ⚠ 已写入未保存: " & exSave.Message, C(180, 100, 0))
			End Try
		Catch ex As Exception
			SetStatus("  ✗ 零件代号异常: " & ex.Message, C(180, 60, 0))
		End Try
	End Sub

	Sub OnCellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
		' ★ 防止递归：方法执行期间先摘除事件，结束时重新挂载
		If g_grid IsNot Nothing Then RemoveHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		Try
			If g_isRefreshing OrElse g_isLoading Then Return  ' ★ 刷新/加载期间跳过，防止误触发
			System.Diagnostics.Debug.WriteLine("[CVC] row=" & e.RowIndex & " col=" & e.ColumnIndex & " colName=" & If(e.ColumnIndex >= 0 AndAlso e.ColumnIndex < g_grid.Columns.Count, g_grid.Columns(e.ColumnIndex).Name, "??") & " val=" & If(e.RowIndex >= 0 AndAlso e.RowIndex < g_grid.Rows.Count AndAlso e.ColumnIndex >= 0 AndAlso e.ColumnIndex < g_grid.Columns.Count, SafeStr(g_grid.Rows(e.RowIndex).Cells(e.ColumnIndex).Value), "??") & " isRefreshing=" & g_isRefreshing)
			If e.RowIndex < 0 OrElse e.RowIndex >= g_grid.Rows.Count Then Return
			If e.ColumnIndex < 0 OrElse e.ColumnIndex >= g_grid.Columns.Count Then Return
			Dim colName As String = g_grid.Columns(e.ColumnIndex).Name
			Dim roIdx As Integer = GetOrigRowIdx(e.RowIndex)
			If roIdx >= 0 AndAlso roIdx < g_rows.Count Then
				Dim roRow As BomDataRow = g_rows(roIdx)
				If roRow.IsContentCenter OrElse roRow.IsLocalStandard Then Return
			End If
			If colName = "cFile" Then
				Dim origIdx As Integer = GetOrigRowIdx(e.RowIndex)
				If origIdx < 0 Then Return
				Dim r As BomDataRow = g_rows(origIdx)
				Dim cellVal As String = SafeStr(g_grid.Rows(e.RowIndex).Cells("cFile").Value)
				Dim origName As String = r.OldFileName
				Dim newBase As String = StripInvExt(cellVal.Trim())
				Dim oldBase As String = StripInvExt(origName)
				If String.IsNullOrEmpty(newBase) OrElse newBase = oldBase Then
					g_grid.Rows(e.RowIndex).Cells("cFile").Value = origName
					g_grid.Rows(e.RowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Empty
					Return
				End If
				Dim dir As String = System.IO.Path.GetDirectoryName(r.FilePath)
				Dim ext As String = System.IO.Path.GetExtension(r.FilePath)
				Dim newPath As String = System.IO.Path.Combine(dir, newBase & ext)
				If System.IO.File.Exists(newPath) Then
					g_grid.Rows(e.RowIndex).DefaultCellStyle.BackColor = ColorDuplicate() : Return
				End If
				' ★ 直接执行改名（不弹窗确认，实时改名）
				If IsMirrorPart(r.FilePath) Then
					g_grid.Rows(e.RowIndex).Cells("cFile").Value = origName
					g_grid.Rows(e.RowIndex).DefaultCellStyle.BackColor = ColorMirror()
					SetStatus("  ✗ 镜像零件不允许改名: " & origName, C(180, 60, 0))
					Return
				End If
				Dim errMsg As String = ""
				Dim ok As Boolean = RenameFileWithDrawing(r.FilePath, newBase, errMsg)
				If ok Then
					Dim newFp As String = System.IO.Path.Combine(dir, newBase & ext)
					r.FilePath = newFp : r.OldFileName = newBase & ext : r.FileName = newBase & ext
					g_grid.Rows(e.RowIndex).Cells("cFile").Value = newBase & ext
					g_grid.Rows(e.RowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Empty
					g_occMapDirty = True
					SetStatus("  ✓ 已改名: " & origName & " → " & newBase & ext, C(0, 130, 55))
				Else
					g_grid.Rows(e.RowIndex).Cells("cFile").Value = origName
					g_grid.Rows(e.RowIndex).DefaultCellStyle.BackColor = ColorRenameFail()
					SetStatus("  ✗ 改名失败: " & errMsg, C(180, 60, 0))
				End If
				If Not g_isLoading Then g_isDirty = True
				Return
			End If
			If Not g_grid.Columns(e.ColumnIndex).ReadOnly AndAlso Not g_isRefreshing AndAlso Not g_isLoading Then
				g_isDirty = True
				Dim chgIdx As Integer = GetOrigRowIdx(e.RowIndex)
				If chgIdx >= 0 AndAlso chgIdx < g_rows.Count Then
					Dim rr As BomDataRow = g_rows(chgIdx)
					' 先同步当前行的内存值（只同步这一行，不遍历全部）
					Dim dgvRow As DataGridViewRow = g_grid.Rows(e.RowIndex)
					Dim _rawCFile As String = If(g_grid.Columns.Contains("cFile"), SafeStr(dgvRow.Cells("cFile").Value), rr.FileName)
					rr.FileName = StripInvExt(_rawCFile.Trim()) & System.IO.Path.GetExtension(rr.FilePath)
					rr.DrawingNo = If(g_grid.Columns.Contains("cDno"), SafeStr(dgvRow.Cells("cDno").Value), rr.DrawingNo)
					Dim cellPno As String = If(g_grid.Columns.Contains("cPno"), SafeStr(dgvRow.Cells("cPno").Value), If(rr.PartNumber Is Nothing, "", rr.PartNumber))
					If cellPno <> MULTI_VAL Then rr.PartNumber = cellPno
					Dim cv_cat As String = If(g_grid.Columns.Contains("cCat"), SafeStr(dgvRow.Cells("cCat").Value), rr.Category)
					Dim cv_mat As String = If(g_grid.Columns.Contains("cMat"), SafeStr(dgvRow.Cells("cMat").Value), rr.Material)
					Dim cv_desc As String = If(g_grid.Columns.Contains("cDesc"), SafeStr(dgvRow.Cells("cDesc").Value), rr.Description)
					If cv_cat <> MULTI_VAL Then rr.Category = cv_cat
					If cv_mat <> MULTI_VAL Then rr.Material = cv_mat
					If cv_desc <> MULTI_VAL Then rr.Description = cv_desc
					If g_grid.Columns.Contains("cBom") Then
						Dim cv_bom As String = SafeStr(dgvRow.Cells("cBom").Value)
						If cv_bom <> MULTI_VAL AndAlso Not String.IsNullOrEmpty(cv_bom) Then rr.BomStruct = StrToBomStruct(cv_bom)
					End If
					For Each cd As ColDef In g_colDefs
						If Not cd.IsCustom Then Continue For
						If g_grid.Columns.Contains(cd.ColName) Then
							Dim cv As String = SafeStr(dgvRow.Cells(cd.ColName).Value)
							If cv <> MULTI_VAL Then rr.Custom(cd.PropName) = cv
						End If
					Next
					rr.ChangedCols.Add(colName)
					' ★ 直接写入Inventor（跳过改名检查，只写当前行）
					Try
						FastWriteRow(rr)
					Catch ex As Exception
						SetStatus("  写入失败: " & ex.Message, C(180, 60, 0))
					End Try
				End If
			End If
		Catch exOuter As Exception
		Finally
			' ★ 无论正常/异常/Return，都重新挂载事件
			If g_grid IsNot Nothing Then AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		End Try
	End Sub


	Sub OnApplyRowHeight(sender As Object, e As EventArgs)
		Dim hVal As Integer
		If Not Integer.TryParse(g_txtRowH.Text.Trim(), hVal) OrElse hVal < 10 OrElse hVal > 500 Then
			MessageBox.Show(ChrW(35831) & ChrW(36755) & ChrW(20837) & ChrW(26377) & ChrW(25928) & ChrW(34892) & ChrW(39640) & ChrW(65288) & "10 ~ 500" & ChrW(65289) & ChrW(12290), ChrW(25552) & ChrW(31034), MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If
		g_grid.RowTemplate.Height = hVal
		For Each dgvRow As DataGridViewRow In g_grid.Rows
			dgvRow.Height = hVal
		Next
		LoadTypeIcons()
		' ★ 行高变化时同步更新缩略图列宽（等比缩放）
		If g_grid.Columns.Contains("cThumb") Then
			g_grid.Columns("cThumb").Width = CalcThumbColWidth()
		End If
		g_grid.Invalidate()
		SaveSettings()
		SetStatus("  " & ChrW(34892) & ChrW(39640) & ": " & hVal.ToString(), C(110, 55, 0))
	End Sub

	Sub OnApplyAllColWidth(sender As Object, e As EventArgs)
		Dim wVal As Integer
		If Not Integer.TryParse(g_txtColW.Text.Trim(), wVal) OrElse wVal < 10 OrElse wVal > 2000 Then
			MessageBox.Show(ChrW(35831) & ChrW(36755) & ChrW(20837) & ChrW(26377) & ChrW(25928) & ChrW(21015) & ChrW(23485) & ChrW(65288) & "10 ~ 2000" & ChrW(65289) & ChrW(12290), ChrW(25552) & ChrW(31034), MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If
		g_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
		For Each col As DataGridViewColumn In g_grid.Columns
			' 序号(cIdx)/类型(cType)/缩略图(cThumb)列不参与全列宽调整
			If col.Name = "cIdx" OrElse col.Name = "cType" OrElse col.Name = "cThumb" Then Continue For
			If col.Visible Then col.Width = wVal
		Next
		g_grid.Invalidate()
		SaveSettings()
		SetStatus("  " & ChrW(21015) & ChrW(23485) & ": " & wVal.ToString(), C(110, 55, 0))
	End Sub

	Sub OnAutoAllColWidth(sender As Object, e As EventArgs)
		g_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
		Dim cellFont As System.Drawing.Font = g_grid.Font
		Dim hdrFont As System.Drawing.Font = g_grid.ColumnHeadersDefaultCellStyle.Font
		If hdrFont Is Nothing Then hdrFont = cellFont
		' 图标宽度：类型列图标占用的宽度
		Dim curRowH As Integer = g_grid.RowTemplate.Height
		Dim iconSz As Integer = Math.Max(16, Math.Min(curRowH - 4, 48))
		For Each col As DataGridViewColumn In g_grid.Columns
			If Not col.Visible Then Continue For
			' 缩略图列：跳过自适应，单独按行高等比设宽
			If col.Name = "cThumb" Then
				col.Width = CalcThumbColWidth()
				Continue For
			End If
			If TypeOf col Is DataGridViewImageColumn Then Continue For
			' 列头宽度
			Dim colMaxW As Integer = MeasureTextWidth(col.HeaderText, hdrFont) + Scaled(20)
			' 类型列：额外加图标宽度
			Dim isTypeCol As Boolean = (col.Name = "cType")
			Dim iconExtra As Integer = If(isTypeCol, iconSz + Scaled(6), 0)
			For Each dgvRow As DataGridViewRow In g_grid.Rows
				Dim cellObj As Object = dgvRow.Cells(col.Index).Value
				If cellObj Is Nothing Then Continue For
				If Not TypeOf cellObj Is String Then Continue For
				Dim cellStr As String = CStr(cellObj)
				If String.IsNullOrEmpty(cellStr) Then Continue For
				Dim colCw As Integer = MeasureTextWidth(cellStr, cellFont) + iconExtra + Scaled(16)
				If colCw > colMaxW Then colMaxW = colCw
			Next
			' 类型列最小宽度保证图标+文字都能显示
			If isTypeCol Then colMaxW = Math.Max(colMaxW, iconSz + Scaled(40))
			col.Width = colMaxW
		Next
		g_grid.Invalidate()
		SaveSettings()
		SetStatus("  OK", C(110, 55, 0))
	End Sub

	Sub AddBuiltinCol(nm As String, hdr As String, ro As Boolean, fixed As Boolean,
		initW As Integer, ctr As Boolean, isImg As Boolean)
		Dim cd As New ColDef()
		cd.ColName = nm : cd.Header = hdr
		cd.IsReadOnly = ro : cd.IsFixed = fixed
		cd.IsCustom = False : cd.IsImage = isImg
		cd.InitWidth = initW : cd.CenterAlign = ctr
		g_colDefs.Add(cd)
	End Sub

	Sub BuildGridColumn(cd As ColDef, grayBg As System.Drawing.Color)
		If cd.IsImage Then
			Dim imgCol As New DataGridViewImageColumn()
			imgCol.Name = cd.ColName
			imgCol.HeaderText = cd.Header
			imgCol.ReadOnly = True
			imgCol.Width = cd.InitWidth
			imgCol.Visible = True
			imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom
			Dim imgSt As DataGridViewCellStyle = imgCol.DefaultCellStyle
			' 缩略图列底色跟随主题（白底行/间隔行设置）
			imgSt.NullValue = Nothing
			g_grid.Columns.Add(imgCol)
		ElseIf cd.ColName = "cMat" Then
			' 材料列：下拉选择（材料列表在LoadMaterialList里填充）
			Dim matCol As New DataGridViewComboBoxColumn()
			matCol.Name = cd.ColName
			matCol.HeaderText = cd.Header & " ✎"
			matCol.ReadOnly = False
			matCol.Width = cd.InitWidth
			matCol.Visible = True
			matCol.FlatStyle = FlatStyle.Flat
			matCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
			matCol.AutoComplete = True
			matCol.SortMode = DataGridViewColumnSortMode.Automatic
			g_grid.Columns.Add(matCol)
		ElseIf cd.ColName = "cBom" Then
			' BOM结构列：下拉选择
			Dim cbCol As New DataGridViewComboBoxColumn()
			cbCol.Name = cd.ColName
			cbCol.HeaderText = cd.Header
			cbCol.ReadOnly = False
			cbCol.Width = cd.InitWidth
			cbCol.Visible = True
			cbCol.FlatStyle = FlatStyle.Flat
			cbCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
			' 添加下拉选项（与Inventor官方一致）
			cbCol.Items.Add(ChrW(26222) & ChrW(36890) & ChrW(20214))
			cbCol.Items.Add(ChrW(34394) & ChrW(25311) & ChrW(20214))
			cbCol.Items.Add(ChrW(21442) & ChrW(32771))
			cbCol.Items.Add(ChrW(19981) & ChrW(21487) & ChrW(25286) & ChrW(20998) & ChrW(20214))
			cbCol.Items.Add(ChrW(22806) & ChrW(36141) & ChrW(20214))
			Dim cbSt As DataGridViewCellStyle = cbCol.DefaultCellStyle
			cbSt.Alignment = DataGridViewContentAlignment.MiddleCenter
			cbCol.SortMode = DataGridViewColumnSortMode.Automatic
			g_grid.Columns.Add(cbCol)
		Else
			Dim col As New DataGridViewTextBoxColumn()
			col.Name = cd.ColName
			col.HeaderText = If(cd.IsReadOnly, cd.Header, cd.Header & " ✎")
			col.ReadOnly = cd.IsReadOnly
			col.Width = cd.InitWidth
			col.Visible = True
			If cd.CenterAlign Then
				Dim colSt As DataGridViewCellStyle = col.DefaultCellStyle
				colSt.Alignment = DataGridViewContentAlignment.MiddleCenter
				' 列头也居中'
				col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
			End If
			If cd.IsReadOnly Then
				' 只读列不再强制白底，跟随主题（白底行/间隔行/文字色）自动生效
			End If
			g_grid.Columns.Add(col)
			' 序号和类型列冻结固定，不可被其他列越过
			If cd.ColName = "cIdx" OrElse cd.ColName = "cType" Then
				If g_grid.Columns.Contains(cd.ColName) Then
					g_grid.Columns(cd.ColName).Frozen = True
				End If
			End If
		End If
	End Sub

	Sub AddCheckRow(cd As ColDef)
		Dim rowPnl As New Panel()
		Dim rowW As Integer = RIGHT_W - Scaled(4) - SCROLL_W
		rowPnl.Size = New System.Drawing.Size(rowW, Scaled(24))
		rowPnl.Tag = cd.ColName
		rowPnl.BackColor = PanelBg()
		Dim delBtnW As Integer = Scaled(28)
		Dim lblW As Integer = rowW - delBtnW - Scaled(4)
		' 列名Label（替代CheckBox）
		Dim lbl As New Label()
		lbl.Text = cd.Header.Replace(" ✎", "").Trim()
		lbl.Tag = cd.ColName
		lbl.Size = New System.Drawing.Size(lblW, Scaled(22))
		lbl.Location = New System.Drawing.Point(Scaled(2), Scaled(1))
		lbl.BackColor = PanelBg()
		lbl.ForeColor = If(g_theme = 8, ContrastFg(g_custColMgr), System.Drawing.Color.Black)
		lbl.AutoEllipsis = True
		lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		AddHandler rowPnl.Click, AddressOf OnChkRowClick
		AddHandler lbl.Click, AddressOf OnChkRowClick
		rowPnl.Controls.Add(lbl)
		' X删除按钮（cIdx/cType 固定列除外，其余全部可删）
		If cd.ColName <> "cIdx" AndAlso cd.ColName <> "cType" Then
			Dim btnDel As New Button()
			btnDel.Text = "X"
			btnDel.Size = New System.Drawing.Size(delBtnW - Scaled(2), Scaled(22))
			btnDel.Location = New System.Drawing.Point(rowW - delBtnW, Scaled(1))
			btnDel.FlatStyle = FlatStyle.Flat
			btnDel.BackColor = C(200, 50, 50)
			btnDel.ForeColor = System.Drawing.Color.White
			btnDel.Tag = cd.ColName
			AddHandler btnDel.Click, AddressOf OnRemoveCol
			rowPnl.Controls.Add(btnDel)
		End If
		g_chkFlow.Controls.Add(rowPnl)
	End Sub

	Sub OnColCheckChanged(sender As Object, e As EventArgs)
		' 已废弃（无CheckBox），保留空函数
	End Sub

	Sub RestoreBuiltinCol(colName As String)
		' 检查是否已存在
		If g_grid.Columns.Contains(colName) Then
			MessageBox.Show(ChrW(21015) & " [" & colName & "] " & ChrW(24050) & ChrW(23384) & ChrW(22312) & ChrW(65292) & ChrW(26080) & ChrW(38656) & ChrW(37325) & ChrW(22797) & ChrW(28155) & ChrW(21152) & ChrW(12290), ChrW(25552) & ChrW(31034), MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If
		' 找到对应的ColDef（可能已从g_colDefs删除）
		Dim cd As ColDef = Nothing
		For Each existCd As ColDef In g_colDefs
			If existCd.ColName = colName Then cd = existCd : Exit For
		Next
		' 如果ColDef不存在，重新创建
		If cd Is Nothing Then
			cd = New ColDef()
			cd.ColName = colName : cd.IsFixed = False : cd.IsCustom = False : cd.IsImage = False
			Select Case colName
				Case "cFile" : cd.Header = ChrW(25991) & ChrW(20214) & ChrW(21517) : cd.IsReadOnly = False : cd.InitWidth = Scaled(280) : cd.CenterAlign = False
				Case "cDno" : cd.Header = ChrW(22270) & ChrW(21495) : cd.IsReadOnly = False : cd.InitWidth = Scaled(100) : cd.CenterAlign = False
				Case "cPno" : cd.Header = ChrW(38646) & ChrW(20214) & ChrW(20195) & ChrW(21495) : cd.IsReadOnly = False : cd.InitWidth = Scaled(120) : cd.CenterAlign = False
				Case "cCat" : cd.Header = ChrW(31867) & ChrW(21035) : cd.IsReadOnly = False : cd.InitWidth = Scaled(90) : cd.CenterAlign = False
				Case "cQty" : cd.Header = ChrW(25968) & ChrW(37327) : cd.IsReadOnly = True : cd.InitWidth = Scaled(60) : cd.CenterAlign = True
				Case "cMat" : cd.Header = ChrW(26448) & ChrW(26009) : cd.IsReadOnly = False : cd.InitWidth = Scaled(100) : cd.CenterAlign = False
				Case "cDesc" : cd.Header = ChrW(25551) & ChrW(36848) : cd.IsReadOnly = False : cd.InitWidth = Scaled(160) : cd.CenterAlign = False
				Case "cDwg" : cd.Header = ChrW(24037) & ChrW(31243) & ChrW(22270) & ChrW(26159) & ChrW(21542) & ChrW(26377) : cd.IsReadOnly = True : cd.InitWidth = Scaled(90) : cd.CenterAlign = True
				Case "cBom" : cd.Header = "BOM" & ChrW(32467) & ChrW(26500) : cd.IsReadOnly = False : cd.InitWidth = Scaled(80) : cd.CenterAlign = True
				Case "cThumb" : cd.Header = ChrW(32553) & ChrW(30053) & ChrW(22270) : cd.IsReadOnly = True : cd.InitWidth = CalcThumbColWidth() : cd.CenterAlign = True : cd.IsImage = True
				Case Else : Return
			End Select
			g_colDefs.Add(cd)
		End If
		' 重建列
		Dim grayBg As System.Drawing.Color = System.Drawing.Color.White
		BuildGridColumn(cd, grayBg)
		' 加到列管理面板
		AddCheckRow(cd)
		' 刷新数据
		RefreshGridRows()
		SyncChkFlowOrder()
		SaveSettings()
		SetStatus(ChrW(24050) & ChrW(25110) & ChrW(22797) & ": " & colName, C(0, 130, 55))
	End Sub

	Sub OnAddCustomCol(sender As Object, e As EventArgs)
		Dim rawInput As String = g_txtProp.Text.Trim()
		If rawInput = "选择或输入属性名..." Then rawInput = ""
		' 跳过提示项和分隔符
		If String.IsNullOrEmpty(rawInput) Then Return
		If rawInput.StartsWith(ChrW(35831) & ChrW(36873)) Then Return
		If rawInput.StartsWith("──") Then Return
		' 检查是否是内置可选列格式 '列名 [cXxx]'
		Dim bracketStart As Integer = rawInput.LastIndexOf("[")
		Dim bracketEnd As Integer = rawInput.LastIndexOf("]")
		If bracketStart >= 0 AndAlso bracketEnd > bracketStart Then
			Dim builtinColName As String = rawInput.Substring(bracketStart + 1, bracketEnd - bracketStart - 1).Trim()
			' 恢复内置列
			RestoreBuiltinCol(builtinColName)
			g_txtProp.Text = ""
			Return
		End If
		' 解析格式：'中文 (英文属性名)' 或 '纯中文' 或 '纯英文'
		Dim propName As String = rawInput
		Dim displayName As String = rawInput
		Dim parenStart As Integer = rawInput.LastIndexOf("(")
		Dim parenEnd As Integer = rawInput.LastIndexOf(")")
		If parenStart >= 0 AndAlso parenEnd > parenStart Then
			propName = rawInput.Substring(parenStart + 1, parenEnd - parenStart - 1).Trim()
			displayName = rawInput.Substring(0, parenStart).Trim()
		End If
		If String.IsNullOrEmpty(propName) Then
			MessageBox.Show("请先输入 iProperty 属性名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If
		' ★ 先检查列名是否已在表格中（含隐藏列）
		' 手动输入时检查带（自定义）的列头，下拉选择时检查原始列头
		Dim checkHeader As String = If(g_txtProp.SelectedIndex < 0,
		rawInput & ChrW(65288) & ChrW(33258) & ChrW(23450) & ChrW(20041) & ChrW(65289),
		displayName)
		For Each existCol As DataGridViewColumn In g_grid.Columns
			If String.Compare(existCol.HeaderText.Replace(" ✎", "").Trim(), checkHeader, True) = 0 Then
				MessageBox.Show("列 [" & checkHeader & "] 已存在，无需重复添加。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Return
			End If
		Next
		For Each cd As ColDef In g_colDefs
			' 自定义列：检查 PropName 和 Header 都不重复
			' 内置列：只检查 PropName，允许自定义列与内置列同名（如自定义"材料"与内置材料列共存）
			Dim cdHeader As String = cd.Header.Replace(" ✎", "").Trim()
			If cd.IsCustom Then
				If cd.PropName = propName OrElse cdHeader = displayName Then
					MessageBox.Show(ChrW(23646) & ChrW(24615) & " [" & displayName & "] " & ChrW(24050) & ChrW(23384) & ChrW(22312) & ChrW(65292) & ChrW(26080) & ChrW(38656) & ChrW(37325) & ChrW(22797) & ChrW(28155) & ChrW(21152) & ChrW(12290), ChrW(25552) & ChrW(31034), MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Return
				End If
			Else
				' 内置列只检查 PropName，不拦截同名自定义属性
				If cd.PropName = propName Then
					MessageBox.Show(ChrW(23646) & ChrW(24615) & " [" & displayName & "] " & ChrW(24050) & ChrW(23384) & ChrW(22312) & ChrW(65292) & ChrW(26080) & ChrW(38656) & ChrW(37325) & ChrW(22797) & ChrW(28155) & ChrW(21152) & ChrW(12290), ChrW(25552) & ChrW(31034), MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Return
				End If
			End If
		Next
		' ★ 特殊列：级别序号 — 直接从 BomDataRow.LevelNo 取值，不查 iProperty
		Dim isLevelNo As Boolean = (propName = "级别序号" OrElse propName = "LevelNo")
		For Each bomRow As BomDataRow In g_rows
			If isLevelNo Then
				BOMRow.Custom(propName) = If(String.IsNullOrEmpty(BOMRow.LevelNo), "", BOMRow.LevelNo)
			Else
				BOMRow.Custom(propName) = ReadAnyProp(BOMRow.FilePath, propName)
			End If
		Next
		g_customIdx += 1
		Dim newColName As String = "cCustom" & g_customIdx
		Dim newCd As New ColDef()
		' 系统标准属性不加后缀，纯自定义属性加（自定义）
		Dim sysPropNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
		"Part Number", "Description", "Material", "Mass", "Vendor", "Cost",
		"Stock Number", "Standard", "Designer", "Engineer", "Authority",
		"Creation Time", "Project", "Company", "Category", "Keywords",
		"Comments", "Title", "SurfaceArea", "Volume", "Density",
		"图号", "数量", "级别序号", "LevelNo",
		"质量", "体积", "表面积", "密度", "类别", "描述", "材料",
		"零件代号", "供应商", "设计者", "工程师", "审核人",
		"项目", "成本中心", "用户状态", "标准", "库存编号",
		"制造商", "标准版本", "注释"
		}
		' 手动输入（SelectedIndex=-1）一律加（自定义），下拉选择才按系统属性判断
		Dim isManualInput As Boolean = (g_txtProp.SelectedIndex < 0)
		Dim colHeader As String
		If isManualInput Then
			' 手动输入：一律加（自定义）
			colHeader = displayName & ChrW(65288) & ChrW(33258) & ChrW(23450) & ChrW(20041) & ChrW(65289)
			' 手动输入时propName也用原始输入（不走括号解析的英文名）
			propName = rawInput
			displayName = rawInput
		Else
			' 下拉选择：系统属性不加（自定义）
			colHeader = If(sysPropNames.Contains(propName), displayName, displayName & ChrW(65288) & ChrW(33258) & ChrW(23450) & ChrW(20041) & ChrW(65289))
		End If
		newCd.ColName = newColName : newCd.Header = colHeader
		newCd.IsReadOnly = isLevelNo : newCd.IsFixed = False
		newCd.IsCustom = True : newCd.IsImage = False
		newCd.PropName = propName : newCd.InitWidth = Scaled(120)
		newCd.CenterAlign = False
		g_colDefs.Add(newCd)
		Dim grayBg As System.Drawing.Color = System.Drawing.Color.White
		BuildGridColumn(newCd, grayBg)
		' 新列放在缩略图前
		If g_grid.Columns.Contains("cThumb") Then
			If g_grid.Columns.Contains("cThumb") Then g_grid.Columns("cThumb").DisplayIndex = g_grid.Columns.Count - 1
		End If
		' ★ 自动把新自定义属性写入所有可写零件（不存在则创建空值，已存在则跳过，只读零件跳过）
		Dim autoWriteCount As Integer = 0
		Dim autoSkipRO As Integer = 0
		Dim autoSkipExist As Integer = 0
		If Not isLevelNo Then
			Dim processedFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
			For Each bomRow As BomDataRow In g_rows
				If String.IsNullOrEmpty(BOMRow.FilePath) Then Continue For
				If processedFp.Contains(BOMRow.FilePath) Then Continue For
				processedFp.Add(BOMRow.FilePath)
				' 跳过只读零件（标准件/资源中心/本地标准件）
				If BOMRow.IsContentCenter OrElse BOMRow.IsLocalStandard Then
					autoSkipRO += 1 : Continue For
				End If
				' 跳过丢失文件
				If BOMRow.IsMissing Then Continue For
				' 写入自定义属性（已存在则跳过，不覆盖；标准属性集里有则跳过）
				Try
					Dim tDoc As Document = Nothing
					For Each od As Document In ThisApplication.Documents
						If String.Compare(od.FullDocumentName, BOMRow.FilePath, True) = 0 Then
							tDoc = od : Exit For
						End If
					Next
					If tDoc Is Nothing Then
						tDoc = ThisApplication.Documents.Open(BOMRow.FilePath, False)
					End If
					If tDoc IsNot Nothing Then
						Dim ps2 As PropertySets = tDoc.PropertySets
						' 先查4个属性集，任意一个找到该属性名就算已存在，跳过
						Dim existsInAny As Boolean = False
						Dim allSets() As String = {GUID_DTP, GUID_UDP, GUID_DOCSUMMARY, GUID_SUMMARY}
						For Each sg As String In allSets
							Try
								Dim chkSet As PropertySet = ps2.Item(sg)
								Try
									Dim chkProp As Object = chkSet.Item(propName)
									existsInAny = True : Exit For
								Catch : End Try
							Catch : End Try
						Next
						If existsInAny Then
							autoSkipExist += 1
						Else
							' 4个属性集都没有，在UDP里新建空属性
							Dim udpSet As PropertySet = Nothing
							Try : udpSet = ps2.Item(GUID_UDP) : Catch : End Try
							If udpSet IsNot Nothing Then
								udpSet.Add("", propName)
								autoWriteCount += 1
								BOMRow.Custom(propName) = ""
							End If
						End If
					End If
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					' 写入失败（文件只读/锁定），静默跳过
				End Try
			Next
		End If
		RefreshGridRows()
		AddCheckRow(newCd)
		' 同步列管理顺序
		SyncChkFlowOrder()
		SaveSettings()
		g_txtProp.ForeColor = System.Drawing.Color.Gray
		g_txtProp.Text = "选择或输入属性名..."
		' 状态栏显示写入结果
		If Not isLevelNo AndAlso autoWriteCount > 0 Then
			Dim autoMsg As String = "  已添加: " & propName &
			"（自动写入 " & autoWriteCount & " 个零件"
			If autoSkipRO > 0 Then autoMsg &= "，跳过只读 " & autoSkipRO & " 个"
			If autoSkipExist > 0 Then autoMsg &= "，已有属性 " & autoSkipExist & " 个"
			autoMsg &= "）"
			SetStatus(autoMsg, C(0, 130, 55))
		ElseIf Not isLevelNo AndAlso autoSkipExist > 0 AndAlso autoWriteCount = 0 Then
			SetStatus("  已添加: " & propName & "（所有零件已有此属性）", C(0, 130, 55))
		Else
			SetStatus("  已添加: " & propName, C(0, 130, 55))
		End If
	End Sub

	Sub OnRemoveCol(sender As Object, e As EventArgs)
		Dim btnDel As Button = CType(sender, Button)
		Dim colName As String = CStr(btnDel.Tag)
		' 从表格删除列
		If g_grid IsNot Nothing Then RemoveHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		If g_grid.Columns.Contains(colName) Then g_grid.Columns.Remove(colName)
		If g_grid IsNot Nothing Then AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		' 从colDefs删除
		Dim toRemoveCd As ColDef = Nothing
		For Each cd As ColDef In g_colDefs
			If cd.ColName = colName Then toRemoveCd = cd : Exit For
		Next
		If toRemoveCd IsNot Nothing Then g_colDefs.Remove(toRemoveCd)
		' 从列管理面板删除
		Dim toRemoveCtrl As Control = Nothing
		For Each ctrl As Control In g_chkFlow.Controls
			If ctrl.Tag IsNot Nothing AndAlso CStr(ctrl.Tag) = colName Then toRemoveCtrl = ctrl : Exit For
		Next
		If toRemoveCtrl IsNot Nothing Then g_chkFlow.Controls.Remove(toRemoveCtrl)
		' 如果是自定义列，也从g_rows的Custom里清理
		If toRemoveCd IsNot Nothing AndAlso toRemoveCd.IsCustom Then
			For Each rr As BomDataRow In g_rows
				If rr.Custom.ContainsKey(toRemoveCd.PropName) Then rr.Custom.Remove(toRemoveCd.PropName)
			Next
		End If
		SaveSettings()
		SetStatus("  " & ChrW(24050) & ChrW(31227) & ChrW(38500) & ": " & colName, C(150, 60, 0))
	End Sub

	Sub OnRemoveCustomCol(sender As Object, e As EventArgs)
		Dim btnDel As Button = CType(sender, Button)
		Dim colName As String = CStr(btnDel.Tag)
		If g_grid IsNot Nothing Then RemoveHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		If g_grid.Columns.Contains(colName) Then g_grid.Columns.Remove(colName)
		If g_grid IsNot Nothing Then AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
		Dim toRemoveCd As ColDef = Nothing
		For Each cd As ColDef In g_colDefs
			If cd.ColName = colName Then toRemoveCd = cd : Exit For
		Next
		If toRemoveCd IsNot Nothing Then g_colDefs.Remove(toRemoveCd)
		Dim toRemoveCtrl As Control = Nothing
		For Each ctrl As Control In g_chkFlow.Controls
			If ctrl.Tag IsNot Nothing AndAlso CStr(ctrl.Tag) = colName Then toRemoveCtrl = ctrl : Exit For
		Next
		If toRemoveCtrl IsNot Nothing Then g_chkFlow.Controls.Remove(toRemoveCtrl)
		SetStatus("  已移除: " & colName, C(150, 60, 0))
	End Sub

	' ★ FastWriteRow：只写单行，跳过改名和全行遍历，性能优化
	Sub FastWriteRow(bomRow As BomDataRow)
		If bomRow Is Nothing Then Return
		' 标准件：只允许写BOM结构，跳过其他属性写入'
		Dim isStdPart As Boolean = bomRow.IsContentCenter OrElse bomRow.IsLocalStandard
		If isStdPart AndAlso Not bomRow.ChangedCols.Contains("cBom") Then Return
		If bomRow.ChangedCols.Count = 0 Then Return
		' 确保occMap缓存有效
		If g_occMap Is Nothing OrElse g_occMapDirty Then
			g_occMap = New Dictionary(Of String, List(Of ComponentOccurrence))(StringComparer.OrdinalIgnoreCase)
			Try
				Dim stk2 As New Stack(Of ComponentOccurrence)
				For Each topOcc As ComponentOccurrence In g_asmDoc.ComponentDefinition.Occurrences
					stk2.Push(topOcc)
				Next
				Do While stk2.Count > 0
					Dim oOcc As ComponentOccurrence = stk2.Pop()
					If oOcc.Suppressed Then Continue Do
					Dim defDoc As Document = oOcc.Definition.Document
					Dim defFp As String = If(String.IsNullOrEmpty(defDoc.FullDocumentName), defDoc.FullDisplayName, defDoc.FullDocumentName)
					Dim fpKey As String = defFp.ToLower()
					If Not g_occMap.ContainsKey(fpKey) Then g_occMap(fpKey) = New List(Of ComponentOccurrence)()
					g_occMap(fpKey).Add(oOcc)
					If oOcc.DefinitionDocumentType = kAssemblyDocumentObject Then
						For Each subOcc As ComponentOccurrence In oOcc.SubOccurrences
							stk2.Push(subOcc)
						Next
					End If
				Loop
			Catch : End Try
			g_occMapDirty = False
		End If
		Dim occMap As Dictionary(Of String, List(Of ComponentOccurrence)) = g_occMap
		Dim failCols As New List(Of String)()
		Dim errList As New List(Of String)()
		Dim okCount As Integer = 0
		Try
			Dim tDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If String.Compare(od.FullDocumentName, bomRow.FilePath, True) = 0 Then tDoc = od : Exit For
			Next
			If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(bomRow.FilePath, False)
			Dim ps As PropertySets = tDoc.PropertySets
			Dim dtp As PropertySet = ps.Item(GUID_DTP)
			If bomRow.ChangedCols.Contains("cPno") Then
				Try
					dtp.Item("Part Number").Value = bomRow.PartNumber
				Catch : failCols.Add("cPno") : End Try
			End If
			If bomRow.ChangedCols.Contains("cMat") Then
				' 材料属性只有零件(.ipt)才可写，部件/焊接件跳过
				If tDoc.DocumentType = kPartDocumentObject Then
					Try
						dtp.Item("Material").Value = bomRow.Material
					Catch ex2 As System.Runtime.InteropServices.COMException
						' E_FAIL(0x80004005)：属性被Inventor锁定（焊接件/派生件），静默跳过
						If (ex2.ErrorCode And &HFFFF) <> &H4005 Then
							failCols.Add("cMat")
						End If
					Catch ex2 As Exception
						failCols.Add("cMat")
					End Try
				End If
			End If
			' 同时设置ActiveMaterial（只在cMat被修改时执行，完全隔离不影响cMat报告）
			If bomRow.ChangedCols.Contains("cMat") Then
				Try
					If Not String.IsNullOrEmpty(bomRow.Material) AndAlso
					tDoc.DocumentType = kPartDocumentObject Then
						Dim partDoc As PartDocument = CType(tDoc, PartDocument)
						Dim matFound As Boolean = False
						For Each mat As Object In partDoc.MaterialAssets
							If String.Compare(CStr(mat.DisplayName), bomRow.Material, True) = 0 Then
								partDoc.ActiveMaterial = mat
								matFound = True : Exit For
							End If
						Next
						If Not matFound Then
							For Each assetLib As Object In ThisApplication.AssetLibraries
								If matFound Then Exit For
								Try
									For Each mat As Object In assetLib.MaterialAssets
										If String.Compare(CStr(mat.DisplayName), bomRow.Material, True) = 0 Then
											partDoc.ActiveMaterial = mat
											matFound = True : Exit For
										End If
									Next
								Catch : End Try
							Next
						End If
					End If
				Catch : End Try
			End If
			If bomRow.ChangedCols.Contains("cDesc") Then
				Try
					dtp.Item("Description").Value = bomRow.Description
				Catch : failCols.Add("cDesc") : End Try
			End If
			Dim udp As PropertySet = ps.Item(GUID_UDP)
			If bomRow.ChangedCols.Contains("cDno") Then
				Try
					Try
						SafeWriteUDP(udp, "图号", bomRow.DrawingNo)
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程

					End Try
				Catch : failCols.Add("cDno") : End Try
			End If
			If bomRow.ChangedCols.Contains("cCat") Then
				Try
					Dim dsp As PropertySet = ps.Item(GUID_DOCSUMMARY)
					Try
						dsp.Item("Category").Value = bomRow.Category
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						Try : dsp.Add(bomRow.Category, "Category") : Catch : End Try
					End Try
				Catch : failCols.Add("cCat") : End Try
			End If
			For Each kvp As KeyValuePair(Of String, String) In bomRow.Custom
				' 自定义列：列名格式为 cCustomN，检查是否在 ChangedCols 里
				Dim customColChanged As Boolean = False
				For Each cd As ColDef In g_colDefs
					If cd.IsCustom AndAlso cd.PropName = kvp.Key AndAlso bomRow.ChangedCols.Contains(cd.ColName) Then
						customColChanged = True : Exit For
					End If
				Next
				If Not customColChanged Then Continue For
				Try
					' ★ 先查标准属性集(DTP/DOCSUMMARY/SUMMARY)，找到就写；找不到才写/建UDP
					Dim writtenToStd As Boolean = False
					Dim stdSets() As String = {GUID_DTP, GUID_DOCSUMMARY, GUID_SUMMARY}
					For Each setGuid As String In stdSets
						Try
							Dim stdSet As PropertySet = ps.Item(setGuid)
							Try
								stdSet.Item(kvp.Key).Value = kvp.Value
								writtenToStd = True : Exit For
							Catch : End Try
						Catch : End Try
					Next
					If Not writtenToStd Then
						Try
							SafeWriteUDP(udp, kvp.Key, kvp.Value)
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程

						End Try
					End If
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					failCols.Add("__custom__" & kvp.Key)
				End Try
			Next
			' 写入BOM结构（快速：直接通过ComponentOccurrence，无需BOM表遍历）
			If bomRow.ChangedCols.Contains("cBom") Then
				Try
					Dim bomEnum2 As BOMStructureEnum
					Select Case bomRow.BomStruct
						Case 51970 : bomEnum2 = BOMStructureEnum.kNormalBOMStructure
						Case 51971 : bomEnum2 = BOMStructureEnum.kPhantomBOMStructure
						Case 51972 : bomEnum2 = BOMStructureEnum.kReferenceBOMStructure
						Case 51973 : bomEnum2 = BOMStructureEnum.kPurchasedBOMStructure
						Case 51974 : bomEnum2 = BOMStructureEnum.kInseparableBOMStructure
						Case Else : bomEnum2 = BOMStructureEnum.kNormalBOMStructure
					End Select
					' ★ 通过文档ComponentDefinition写入（Occurrence.BOMStructure只读）'
					Dim bomWriteOK As Boolean = False
					Dim bomTDoc2 As Document = Nothing
					Try
						' 优先从g_occMap缓存拿文档，避免遍历所有文档'
						If g_occMap IsNot Nothing AndAlso g_occMap.ContainsKey(bomRow.FilePath) Then
							Dim occList As List(Of ComponentOccurrence) = g_occMap(bomRow.FilePath)
							If occList.Count > 0 Then
								Try : bomTDoc2 = occList(0).Definition.Document : Catch : End Try
							End If
						End If
						' 备用：遍历已打开文档'
						If bomTDoc2 Is Nothing Then
							For Each od3 As Document In ThisApplication.Documents
								If String.Compare(od3.FullDocumentName, bomRow.FilePath, True) = 0 Then
									bomTDoc2 = od3 : Exit For
								End If
							Next
						End If
						' 最后：静默打开'
						If bomTDoc2 Is Nothing AndAlso System.IO.File.Exists(bomRow.FilePath) Then
							bomTDoc2 = ThisApplication.Documents.Open(bomRow.FilePath, False)
						End If
						If bomTDoc2 IsNot Nothing Then
							If bomTDoc2.DocumentType = kPartDocumentObject Then
								CType(bomTDoc2, PartDocument).ComponentDefinition.BOMStructure = bomEnum2
								bomWriteOK = True
							ElseIf bomTDoc2.DocumentType = kAssemblyDocumentObject Then
								CType(bomTDoc2, AssemblyDocument).ComponentDefinition.BOMStructure = bomEnum2
								bomWriteOK = True
							End If
						End If
					Catch : End Try
					' Save零件文档本身（非标准件才Save，避免只读弹窗）'
					If bomWriteOK AndAlso bomTDoc2 IsNot Nothing Then
						If Not bomRow.IsContentCenter AndAlso Not bomRow.IsLocalStandard Then
							g_pendingSaveDocs.Add(bomRow.FilePath)
						End If
					End If
				Catch : End Try
			End If
			' 标记失败的单元格为橙色
			If failCols.Count > 0 Then
				For Each dgvRow2 As DataGridViewRow In g_grid.Rows
					Dim oi2 As Integer = GetOrigRowIdx(dgvRow2.Index)
					If oi2 < 0 OrElse oi2 >= g_rows.Count Then Continue For
					If String.Compare(g_rows(oi2).FilePath, bomRow.FilePath, True) <> 0 Then Continue For
					For Each fc As String In failCols
						Dim colName As String = fc
						If fc.StartsWith("__custom__") Then
							Dim propName As String = fc.Substring(10)
							For Each cd As ColDef In g_colDefs
								If cd.IsCustom AndAlso cd.PropName = propName Then colName = cd.ColName : Exit For
							Next
						End If
						If g_grid.Columns.Contains(colName) Then
							dgvRow2.Cells(colName).Style.BackColor = System.Drawing.Color.FromArgb(255, 140, 0)
						End If
					Next
					errList.Add(bomRow.FileName & " - " & String.Join(",", failCols.ToArray()))
					Exit For
				Next
			End If
			okCount += 1
		Catch ex As Exception
			' 显示完整错误：文件名 + 异常类型 + 消息，方便定位问题
			Dim exDetail As String = "[" & ex.GetType().Name & "] " & ex.Message
			errList.Add(bomRow.FileName & " - " & exDetail)
		End Try
		bomRow.ChangedCols.Clear()
		' ★ 批量Save：保存所有修改过BOM结构的零件文档'
		If g_pendingSaveDocs.Count > 0 Then
			For Each od4 As Document In ThisApplication.Documents
				If g_pendingSaveDocs.Contains(od4.FullDocumentName) Then
					Try
						' 静默保存：不弹窗'
						If Not od4.IsModifiable Then Continue For
						od4.Save()
					Catch : End Try
				End If
			Next
			g_pendingSaveDocs.Clear()
		End If
	End Sub

	Sub OnWriteClick(sender As Object, e As EventArgs)
		g_grid.EndEdit()
		For Each dgvRow As DataGridViewRow In g_grid.Rows
			Dim origIdx As Integer = GetOrigRowIdx(dgvRow.Index)
			If origIdx >= 0 AndAlso origIdx < g_rows.Count Then
				If g_rows(origIdx).ChangedCols.Count = 0 Then Continue For
			End If
			If origIdx < 0 Then Continue For
			Dim r As BomDataRow = g_rows(origIdx)
			Dim _rawCFile As String = If(g_grid.Columns.Contains("cFile"), SafeStr(dgvRow.Cells("cFile").Value), r.FileName)
			r.FileName = StripInvExt(_rawCFile.Trim()) & System.IO.Path.GetExtension(r.FilePath)
			r.DrawingNo = If(g_grid.Columns.Contains("cDno"), SafeStr(dgvRow.Cells("cDno").Value), r.DrawingNo)
			Dim cellPno As String = If(g_grid.Columns.Contains("cPno"), SafeStr(dgvRow.Cells("cPno").Value), If(r.PartNumber Is Nothing, "", r.PartNumber))
			If g_chkMergePno IsNot Nothing AndAlso g_chkMergePno.Checked Then
				Dim origPno As String = If(r.PartNumber Is Nothing, "", r.PartNumber.Trim())
				Dim fileBase As String = StripInvExt(r.FileName)
				If cellPno = fileBase AndAlso String.IsNullOrEmpty(origPno) Then
					r.PartNumber = ""
				Else
					r.PartNumber = cellPno
				End If
			Else
				r.PartNumber = cellPno
			End If
			Dim cv_cat As String = If(g_grid.Columns.Contains("cCat"), SafeStr(dgvRow.Cells("cCat").Value), r.Category)
			Dim cv_mat As String = If(g_grid.Columns.Contains("cMat"), SafeStr(dgvRow.Cells("cMat").Value), r.Material)
			Dim cv_desc As String = If(g_grid.Columns.Contains("cDesc"), SafeStr(dgvRow.Cells("cDesc").Value), r.Description)
			If cv_cat <> MULTI_VAL Then r.Category = cv_cat
			If cv_mat <> MULTI_VAL Then r.Material = cv_mat
			If cv_desc <> MULTI_VAL Then r.Description = cv_desc
			If g_grid.Columns.Contains("cBom") Then
				Dim cv_bom As String = SafeStr(dgvRow.Cells("cBom").Value)
				If cv_bom <> MULTI_VAL AndAlso Not String.IsNullOrEmpty(cv_bom) Then
					r.BomStruct = StrToBomStruct(cv_bom)
				End If
			End If
			For Each cd As ColDef In g_colDefs
				If Not cd.IsCustom Then Continue For
				If g_grid.Columns.Contains(cd.ColName) Then
					Dim cv As String = SafeStr(dgvRow.Cells(cd.ColName).Value)
					If cv <> MULTI_VAL Then r.Custom(cd.PropName) = cv
				End If
			Next
		Next

		Dim hasRename As Boolean = False
		For Each dgvRow As DataGridViewRow In g_grid.Rows
			Dim origIdx As Integer = GetOrigRowIdx(dgvRow.Index)
			If origIdx < 0 Then Continue For
			Dim r As BomDataRow = g_rows(origIdx)
			Dim newBase As String = StripInvExt(r.FileName.Trim())
			Dim oldBase As String = StripInvExt(r.OldFileName)
			If String.IsNullOrEmpty(newBase) OrElse newBase = oldBase Then Continue For
			Dim rowBg As System.Drawing.Color = dgvRow.DefaultCellStyle.BackColor
			If rowBg = ColorDuplicate() Then
				dgvRow.Cells("cFile").Value = r.OldFileName : r.FileName = r.OldFileName : Continue For
			End If
			If rowBg = ColorMirror() Then
				dgvRow.Cells("cFile").Value = r.OldFileName : r.FileName = r.OldFileName : Continue For
			End If
			hasRename = True
		Next

		If hasRename Then
			Dim dlgResult As DialogResult = MessageBox.Show(
			"检测到「文件名」列有修改，将执行改名操作。" & vbCrLf &
			"请确保所有包含该零件的装配体都已在 Inventor 中打开！" & vbCrLf &
			"建议操作前备份整个项目文件夹。" & vbCrLf & vbCrLf & "确认继续？",
			"确认改名操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
			If dlgResult <> DialogResult.Yes Then
				SetStatus("  已取消改名，继续写入属性...", C(150, 60, 0))
				' 取消改名：跳过改名，但继续执行属性写入，不重置 g_isClosing
				hasRename = False
			End If
		End If

		Dim renameCount As Integer = 0
		Dim renameErrors As New List(Of String)

		For Each dgvRow As DataGridViewRow In g_grid.Rows
			Dim origIdx As Integer = GetOrigRowIdx(dgvRow.Index)
			If origIdx < 0 Then Continue For
			Dim r As BomDataRow = g_rows(origIdx)
			Dim newBase As String = StripInvExt(r.FileName.Trim())
			Dim oldBase As String = StripInvExt(r.OldFileName)
			If String.IsNullOrEmpty(newBase) OrElse newBase = oldBase Then Continue For
			Dim rowBg As System.Drawing.Color = dgvRow.DefaultCellStyle.BackColor
			If rowBg = ColorDuplicate() OrElse rowBg = ColorMirror() Then Continue For
			SetStatus("  改名: " & r.OldFileName & " -> " & newBase, C(110, 55, 0))
			g_frm.Refresh()
			If IsMirrorPart(r.FilePath) Then
				dgvRow.Cells("cFile").Value = r.OldFileName
				dgvRow.DefaultCellStyle.BackColor = ColorMirror()
				r.FileName = r.OldFileName
				renameErrors.Add(r.OldFileName & " - 镜像零件，不允许改名")
				Continue For
			End If
			Dim errMsg As String = ""
			Dim ok As Boolean = RenameFileWithDrawing(r.FilePath, newBase, errMsg)
			If ok Then
				Dim dir As String = System.IO.Path.GetDirectoryName(r.FilePath)
				Dim ext As String = System.IO.Path.GetExtension(r.FilePath)
				Dim newFp As String = System.IO.Path.Combine(dir, newBase & ext)
				r.FilePath = newFp : r.OldFileName = newBase & ext
				dgvRow.Cells("cFile").Value = newBase & ext
				dgvRow.DefaultCellStyle.BackColor = System.Drawing.Color.Empty
				renameCount += 1
			Else
				Dim dir2 As String = System.IO.Path.GetDirectoryName(r.FilePath)
				Dim ext2 As String = System.IO.Path.GetExtension(r.FilePath)
				Dim newPath2 As String = System.IO.Path.Combine(dir2, newBase & ext2)
				dgvRow.Cells("cFile").Value = r.OldFileName
				dgvRow.DefaultCellStyle.BackColor = If(System.IO.File.Exists(newPath2), ColorDuplicate(), ColorRenameFail())
				r.FileName = r.OldFileName
				renameErrors.Add(r.OldFileName & " -> " & newBase & " 失败: " & errMsg)
			End If
		Next

		Dim okCount As Integer = 0
		Dim errList As New List(Of String)
		' ★ 预建 FilePath→Occurrence 字典，避免在循环内重复遍历装配树
		' ★ 使用缓存的occMap，避免每次重建装配树（性能优化）
		If g_occMap Is Nothing OrElse g_occMapDirty Then
			g_occMap = New Dictionary(Of String, List(Of ComponentOccurrence))(StringComparer.OrdinalIgnoreCase)
			Try
				Dim stk2 As New Stack(Of ComponentOccurrence)
				For Each topOcc As ComponentOccurrence In g_asmDoc.ComponentDefinition.Occurrences
					stk2.Push(topOcc)
				Next
				Do While stk2.Count > 0
					Dim oOcc As ComponentOccurrence = stk2.Pop()
					If oOcc.Suppressed Then Continue Do
					Dim defDoc As Document = oOcc.Definition.Document
					Dim defFp As String = If(String.IsNullOrEmpty(defDoc.FullDocumentName), defDoc.FullDisplayName, defDoc.FullDocumentName)
					Dim fpKey As String = defFp.ToLower()
					If Not g_occMap.ContainsKey(fpKey) Then g_occMap(fpKey) = New List(Of ComponentOccurrence)()
					g_occMap(fpKey).Add(oOcc)
					If oOcc.DefinitionDocumentType = kAssemblyDocumentObject Then
						For Each subOcc As ComponentOccurrence In oOcc.SubOccurrences
							stk2.Push(subOcc)
						Next
					End If
				Loop
			Catch : End Try
			g_occMapDirty = False
		End If
		Dim occMap As Dictionary(Of String, List(Of ComponentOccurrence)) = g_occMap
		For Each bomRow As BomDataRow In g_rows
			' 只读行（ContentCenter标准件或文件只读）直接跳过，不写入也不报错
			If BOMRow.IsContentCenter OrElse BOMRow.IsLocalStandard Then Continue For
			Dim failCols As New List(Of String)()
			' ChangedCols 为空说明该行没有属性修改（如只改名），直接跳过属性写入
			If BOMRow.ChangedCols.Count = 0 Then Continue For
			Try
				Dim tDoc As Document = Nothing
				For Each od As Document In ThisApplication.Documents
					If String.Compare(od.FullDocumentName, BOMRow.FilePath, True) = 0 Then tDoc = od : Exit For
				Next
				If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(BOMRow.FilePath, False)
				Dim ps As PropertySets = tDoc.PropertySets
				Dim dtp As PropertySet = ps.Item(GUID_DTP)
				If BOMRow.ChangedCols.Contains("cPno") Then
					Try
						dtp.Item("Part Number").Value = BOMRow.PartNumber
					Catch : failCols.Add("cPno") : End Try
				End If
				If BOMRow.ChangedCols.Contains("cMat") Then
					' 材料属性只有零件(.ipt)才可写，部件/焊接件跳过
					If tDoc.DocumentType = kPartDocumentObject Then
						Try
							dtp.Item("Material").Value = BOMRow.Material
						Catch ex2 As System.Runtime.InteropServices.COMException
							' E_FAIL(0x80004005)：属性被Inventor锁定（焊接件/派生件），静默跳过
							If (ex2.ErrorCode And &HFFFF) <> &H4005 Then
								failCols.Add("cMat")
							End If
						Catch ex2 As Exception
							failCols.Add("cMat")
						End Try
					End If
				End If
				' 同时设置ActiveMaterial（只在cMat被修改时执行，完全隔离不影响cMat报告）
				If BOMRow.ChangedCols.Contains("cMat") Then
					Try
						If Not String.IsNullOrEmpty(BOMRow.Material) AndAlso
						tDoc.DocumentType = kPartDocumentObject Then
							Dim partDoc As PartDocument = CType(tDoc, PartDocument)
							Dim matFound As Boolean = False
							For Each mat As Object In partDoc.MaterialAssets
								If String.Compare(CStr(mat.DisplayName), BOMRow.Material, True) = 0 Then
									partDoc.ActiveMaterial = mat
									matFound = True : Exit For
								End If
							Next
							If Not matFound Then
								For Each assetLib As Object In ThisApplication.AssetLibraries
									If matFound Then Exit For
									Try
										For Each mat As Object In assetLib.MaterialAssets
											If String.Compare(CStr(mat.DisplayName), BOMRow.Material, True) = 0 Then
												partDoc.ActiveMaterial = mat
												matFound = True : Exit For
											End If
										Next
									Catch : End Try
								Next
							End If
						End If
					Catch : End Try
				End If
				If BOMRow.ChangedCols.Contains("cDesc") Then
					Try
						dtp.Item("Description").Value = BOMRow.Description
					Catch : failCols.Add("cDesc") : End Try
				End If
				Dim udp As PropertySet = ps.Item(GUID_UDP)
				If BOMRow.ChangedCols.Contains("cDno") Then
					Try
						Try
							SafeWriteUDP(udp, "图号", BOMRow.DrawingNo)
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程

						End Try
					Catch : failCols.Add("cDno") : End Try
				End If
				If BOMRow.ChangedCols.Contains("cCat") Then
					Try
						Dim dsp As PropertySet = ps.Item(GUID_DOCSUMMARY)
						Try
							dsp.Item("Category").Value = BOMRow.Category
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
							Try : dsp.Add(BOMRow.Category, "Category") : Catch : End Try
						End Try
					Catch : failCols.Add("cCat") : End Try
				End If
				For Each kvp As KeyValuePair(Of String, String) In BOMRow.Custom
					' 自定义列：列名格式为 cCustomN，检查是否在 ChangedCols 里
					Dim customColChanged As Boolean = False
					For Each cd As ColDef In g_colDefs
						If cd.IsCustom AndAlso cd.PropName = kvp.Key AndAlso BOMRow.ChangedCols.Contains(cd.ColName) Then
							customColChanged = True : Exit For
						End If
					Next
					If Not customColChanged Then Continue For
					Try
						' ★ 先查标准属性集(DTP/DOCSUMMARY/SUMMARY)，找到就写；找不到才写/建UDP
						Dim writtenToStd As Boolean = False
						Dim stdSets() As String = {GUID_DTP, GUID_DOCSUMMARY, GUID_SUMMARY}
						For Each setGuid As String In stdSets
							Try
								Dim stdSet As PropertySet = ps.Item(setGuid)
								Try
									stdSet.Item(kvp.Key).Value = kvp.Value
									writtenToStd = True : Exit For
								Catch : End Try
							Catch : End Try
						Next
						If Not writtenToStd Then
							Try
								SafeWriteUDP(udp, kvp.Key, kvp.Value)
							Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程

							End Try
						End If
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						failCols.Add("__custom__" & kvp.Key)
					End Try
				Next
				' 写入BOM结构（Stack遍历BOMRow子行，bomWriteOK验证，Save在Exit For前）
				If BOMRow.ChangedCols.Contains("cBom") Then
					Try
						Dim bomEnum2 As BOMStructureEnum
						Select Case BOMRow.BomStruct
							Case 51970 : bomEnum2 = BOMStructureEnum.kNormalBOMStructure
							Case 51971 : bomEnum2 = BOMStructureEnum.kPhantomBOMStructure
							Case 51972 : bomEnum2 = BOMStructureEnum.kReferenceBOMStructure
							Case 51973 : bomEnum2 = BOMStructureEnum.kPurchasedBOMStructure
							Case 51974 : bomEnum2 = BOMStructureEnum.kInseparableBOMStructure
							Case Else : bomEnum2 = BOMStructureEnum.kNormalBOMStructure
						End Select
						For Each od3 As Document In ThisApplication.Documents
							If od3.DocumentType <> kAssemblyDocumentObject Then Continue For
							Try
								Dim ad3 As AssemblyDocument = CType(od3, AssemblyDocument)
								Dim bom3 As BOM = ad3.ComponentDefinition.BOM
								If Not bom3.StructuredViewEnabled Then bom3.StructuredViewEnabled = True
								For Each bvObj3 As Object In bom3.BOMViews
									Try
										Dim bvTyped3 As BOMView = CType(bvObj3, BOMView)
										If bvTyped3.ViewType = BOMViewTypeEnum.kPartsOnlyBOMViewType Then Continue For
										Dim bomWriteOK As Boolean = False
										Dim rowStk As New Stack(Of Object)()
										For Each r3 As Object In bvTyped3.BOMRows : rowStk.Push(r3) : Next
										Do While rowStk.Count > 0
											Dim rowObj3 As Object = rowStk.Pop()
											Try
												Dim invRow3 As BOMRow = CType(rowObj3, BOMRow)
												Dim rowFp3 As String = ""
												Try : rowFp3 = invRow3.ComponentDefinitions.Item(1).Document.FullDocumentName : Catch : End Try
												If String.Compare(rowFp3, BOMRow.FilePath, True) = 0 AndAlso Not bomWriteOK Then
													invRow3.BOMStructure = bomEnum2
													If CInt(invRow3.BOMStructure) = CInt(bomEnum2) Then bomWriteOK = True
												End If
												Try : For Each cr As Object In invRow3.ChildRows : rowStk.Push(cr) : Next : Catch : End Try
											Catch : End Try
										Loop
										' ★ Save 必须在 Exit For 之前
										Try : ad3.Save() : Catch : End Try
										If bomWriteOK Then Exit For
									Catch : End Try
								Next
							Catch : End Try
						Next
					Catch : End Try
				End If
				' 标记失败的单元格为橙色
				If failCols.Count > 0 Then
					For Each dgvRow2 As DataGridViewRow In g_grid.Rows
						Dim oi2 As Integer = GetOrigRowIdx(dgvRow2.Index)
						If oi2 < 0 OrElse oi2 >= g_rows.Count Then Continue For
						If String.Compare(g_rows(oi2).FilePath, BOMRow.FilePath, True) <> 0 Then Continue For
						For Each fc As String In failCols
							Dim colName As String = fc
							If fc.StartsWith("__custom__") Then
								Dim propName As String = fc.Substring(10)
								For Each cd As ColDef In g_colDefs
									If cd.IsCustom AndAlso cd.PropName = propName Then colName = cd.ColName : Exit For
								Next
							End If
							If g_grid.Columns.Contains(colName) Then
								dgvRow2.Cells(colName).Style.BackColor = System.Drawing.Color.FromArgb(255, 140, 0)
							End If
						Next
						errList.Add(BOMRow.FileName & " - " & String.Join(",", failCols.ToArray()))
						Exit For
					Next
				End If
				okCount += 1
			Catch ex As Exception
				' 显示完整错误：文件名 + 异常类型 + 消息，方便定位问题
				Dim exDetail As String = "[" & ex.GetType().Name & "] " & ex.Message
				errList.Add(BOMRow.FileName & " - " & exDetail)
			End Try
		Next

		' ★ 写入成功后重置修改标志
		g_isDirty = False

		Dim sb As New System.Text.StringBuilder()
		If hasRename Then
			sb.Append("  改名: " & renameCount.ToString() & " 成功")
			If renameErrors.Count > 0 Then sb.Append(", " & renameErrors.Count.ToString() & " 失败")
			sb.Append("  |  ")
		End If
		sb.Append("写入: " & okCount.ToString() & " 成功")
		If errList.Count > 0 Then sb.Append(", " & errList.Count.ToString() & " 失败")
		Dim finalClr As System.Drawing.Color = If(renameErrors.Count > 0 OrElse errList.Count > 0, C(180, 60, 0), C(0, 130, 55))
		SetStatus(sb.ToString(), finalClr)

		If errList.Count > 0 Then
			Dim errSb As New System.Text.StringBuilder()
			errSb.AppendLine("【属性写入失败】")
			For Each msg As String In errList
				errSb.AppendLine("  " & msg)
			Next
			MessageBox.Show(errSb.ToString(), "部分操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End If
		If renameErrors.Count > 0 Then
			Dim rnSb As New System.Text.StringBuilder()
			rnSb.AppendLine("【改名失败】共 " & renameErrors.Count & " 个文件：")
			rnSb.AppendLine("")
			For Each msg As String In renameErrors
				rnSb.AppendLine("  " & msg)
			Next
			rnSb.AppendLine("")
			rnSb.AppendLine("（文件可能被只读锁定或被其他程序占用）")
			MessageBox.Show(rnSb.ToString(), "部分改名失败", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End If
	End Sub

	Function ReadAnyProp(filePath As String, propName As String) As String
		Try
			Dim tDoc As Document = Nothing
			For Each od As Document In ThisApplication.Documents
				If od.FullDocumentName = filePath Then tDoc = od : Exit For
			Next
			If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(filePath, False)
			Dim ps As PropertySets = tDoc.PropertySets
			Dim rawVal As Object = Nothing
			Try : rawVal = ps.Item(GUID_DTP).Item(propName).Value : Catch : End Try
			If rawVal Is Nothing Then Try : rawVal = ps.Item(GUID_UDP).Item(propName).Value : Catch : End Try
			If rawVal Is Nothing Then Try : rawVal = ps.Item(GUID_DOCSUMMARY).Item(propName).Value : Catch : End Try
			If rawVal Is Nothing Then Try : rawVal = ps.Item(GUID_SUMMARY).Item(propName).Value : Catch : End Try
			If rawVal Is Nothing Then Return ""
			' ★ 质量/体积/表面积：保留3位小数并加单位
			Dim pnLower As String = propName.ToLower().Trim()
			If pnLower = "mass" OrElse pnLower = "质量" Then
				Try
					Dim dm As Double = CDbl(rawVal)
					' iProperty质量单位是克(g)，除以1000转换为千克(kg)
					Return Math.Round(dm / 1000.0, 3).ToString("0.000")
				Catch : End Try
			End If
			If pnLower = "volume" OrElse pnLower = "体积" Then
				Try
					Dim dv As Double = CDbl(rawVal)
					Return Math.Round(dv, 3).ToString("0.000")
				Catch : End Try
			End If
			If pnLower = "surfacearea" OrElse pnLower = "表面积" Then
				Try
					Dim ds As Double = CDbl(rawVal)
					Return Math.Round(ds, 3).ToString("0.000")
				Catch : End Try
			End If
			Return CStr(rawVal)
		Catch : End Try
		Return ""
	End Function

	Function SafeStr(val As Object) As String
		If val Is Nothing Then Return "" Else Return CStr(val)
	End Function

	Function CollectRows() As List(Of BomDataRow)
		Dim oTopDef As AssemblyComponentDefinition = g_asmDoc.ComponentDefinition
		Dim result As New List(Of BomDataRow)()
		Dim seen As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
		Dim qtyMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
		Dim ccMap As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
		Dim bomStructMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
		Dim drawingCache As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
		Dim roCache As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)

		' ── 单次遍历：同时统计数量、CC标志、BOM结构、并构建BomDataRow ──
		Dim stk As New Stack(Of KeyValuePair(Of ComponentOccurrence, String))()
		Dim topList As New List(Of ComponentOccurrence)()
		For Each topOcc As ComponentOccurrence In oTopDef.Occurrences
			topList.Add(topOcc)
		Next
		For _i As Integer = topList.Count - 1 To 0 Step -1
			stk.Push(New KeyValuePair(Of ComponentOccurrence, String)(topList(_i), ""))
		Next

		Do While stk.Count > 0
			Dim kvOcc As KeyValuePair(Of ComponentOccurrence, String) = stk.Pop()
			Dim oOcc As ComponentOccurrence = kvOcc.Key
			Dim parentFp As String = kvOcc.Value
			If oOcc.Suppressed Then Continue Do

			' ── 获取文档引用（装配树已加载，直接用，不需要 Open）──
			Dim dd As Document = Nothing
			Dim fp As String = ""
			Try
				dd = oOcc.Definition.Document
				fp = If(String.IsNullOrEmpty(dd.FullDocumentName), dd.FullDisplayName, dd.FullDocumentName)
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				' 丢失文件
				Dim missingName As String = oOcc.Name
				Dim missingFp As String = "__missing__" & missingName
				qtyMap(missingFp) = If(qtyMap.ContainsKey(missingFp), qtyMap(missingFp) + 1, 1)
				If Not seen.ContainsKey(missingFp) Then
					Dim misRow As New BomDataRow()
					misRow.FilePath = missingFp : misRow.FileName = missingName
					misRow.OldFileName = missingName : misRow.DocType = "零件"
					misRow.PartType = "零件" : misRow.IsMissing = True
					misRow.BomStruct = 51970 : misRow.ParentFilePath = parentFp
					misRow.FixedIdx = result.Count + 1 : seen(missingFp) = result.Count : result.Add(misRow)
				End If
				Continue Do
			End Try
			If String.IsNullOrEmpty(fp) Then Continue Do

			' ── 统计数量（每次出现都计数）──
			qtyMap(fp) = If(qtyMap.ContainsKey(fp), qtyMap(fp) + 1, 1)

			' ── CC / BOMStructure（只取第一次）──
			If Not ccMap.ContainsKey(fp) Then
				Try : ccMap(fp) = oOcc.IsContentCenter : Catch : ccMap(fp) = False : End Try
			End If
			' 取优先级最高的BOMStructure（普通>外购>不可拆分>虚拟>参考）'
			Try
				Dim rawBS As Integer = CInt(oOcc.BOMStructure)
				Dim bsPri As Integer = 0
				Select Case rawBS
					Case 51970 : bsPri = 5
					Case 51973 : bsPri = 4
					Case 51974 : bsPri = 3
					Case 51971 : bsPri = 2
					Case 51972 : bsPri = 1
				End Select
				If Not bomStructMap.ContainsKey(fp) Then
					bomStructMap(fp) = rawBS
				Else
					Dim exBS As Integer = bomStructMap(fp)
					Dim exPri As Integer = 0
					Select Case exBS
						Case 51970 : exPri = 5
						Case 51973 : exPri = 4
						Case 51974 : exPri = 3
						Case 51971 : exPri = 2
						Case 51972 : exPri = 1
					End Select
					If bsPri > exPri Then bomStructMap(fp) = rawBS
				End If
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				If Not bomStructMap.ContainsKey(fp) Then bomStructMap(fp) = 51970
			End Try

			' ── 子装配：压栈子节点 ──
			If oOcc.DefinitionDocumentType = kAssemblyDocumentObject Then
				Dim childList As New List(Of ComponentOccurrence)()
				For Each childOcc As ComponentOccurrence In oOcc.SubOccurrences
					Try
						If String.Compare(childOcc.Definition.Document.FullDocumentName, fp, True) <> 0 Then
							childList.Add(childOcc)
						End If
					Catch : childList.Add(childOcc) : End Try
				Next
				For _ic As Integer = childList.Count - 1 To 0 Step -1
					stk.Push(New KeyValuePair(Of ComponentOccurrence, String)(childList(_ic), fp))
				Next
			End If

			' ── 已处理过的文件跳过属性读取 ──
			If seen.ContainsKey(fp) Then Continue Do

			' ── 构建 BomDataRow（每个唯一文件只做一次）──
			Dim bomRow As New BomDataRow()
			bomRow.FilePath = fp
			bomRow.FileName = System.IO.Path.GetFileName(fp)
			bomRow.OldFileName = bomRow.FileName
			bomRow.DocType = If(oOcc.DefinitionDocumentType = kAssemblyDocumentObject, "部件", "零件")
			bomRow.Thumbnail = Nothing
			bomRow.IsContentCenter = If(ccMap.ContainsKey(fp), ccMap(fp), False)
			bomRow.ParentFilePath = parentFp

			' ── 类型判断：直接用已有 dd，不再 Open ──
			Try
				If dd.DocumentType = kPartDocumentObject Then
					Dim pd As PartDocument = CType(dd, PartDocument)
					If TypeOf pd.ComponentDefinition Is SheetMetalComponentDefinition Then
						bomRow.PartType = ChrW(38051) & ChrW(37329) : bomRow.IsSheetMetal = True
					Else
						bomRow.PartType = ChrW(38646) & ChrW(20214)
					End If
				ElseIf dd.DocumentType = kAssemblyDocumentObject Then
					Dim ad As AssemblyDocument = CType(dd, AssemblyDocument)
					If TypeOf ad.ComponentDefinition Is WeldmentComponentDefinition Then
						bomRow.PartType = ChrW(28938) & ChrW(25509) : bomRow.IsWeldment = True
					Else
						bomRow.PartType = ChrW(37096) & ChrW(20214)
					End If
				End If
			Catch : End Try

			' ── 工程图检测（缓存，避免重复磁盘IO）──
			If Not drawingCache.ContainsKey(fp) Then
				drawingCache(fp) = CheckDrawingExists(fp)
			End If
			bomRow.HasDrawing = drawingCache(fp)

			' ── 只读文件检测（缓存）──
			If Not roCache.ContainsKey(fp) Then
				Dim isRO As Boolean = False
				Try
					If System.IO.File.Exists(fp) Then
						Dim attrs As System.IO.FileAttributes = System.IO.File.GetAttributes(fp)
						isRO = (attrs And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly
					End If
				Catch : End Try
				roCache(fp) = isRO
			End If

			' ── 属性读取：直接用 dd.PropertySets，不再 Open ──
			Try
				Dim ps As PropertySets = dd.PropertySets
				Dim dtp As PropertySet = ps.Item(GUID_DTP)
				Try : bomRow.PartNumber = CStr(dtp.Item("Part Number").Value) : Catch : bomRow.PartNumber = "" : End Try
				Try : bomRow.Material = CStr(dtp.Item("Material").Value) : Catch : End Try
				Try : bomRow.Description = CStr(dtp.Item("Description").Value) : Catch : End Try
				Try
					bomRow.DrawingNo = CStr(ps.Item(GUID_UDP).Item(ChrW(22270) & ChrW(21495)).Value)
				Catch : bomRow.DrawingNo = "" : End Try
				Try
					bomRow.Category = CStr(ps.Item(GUID_DOCSUMMARY).Item("Category").Value)
				Catch : bomRow.Category = "" : End Try
			Catch : bomRow.PartNumber = "" : End Try

			' ── 标准件判断 ──
			Dim catVal As String = If(bomRow.Category Is Nothing, "", bomRow.Category).Trim()
			Dim fpLower As String = fp.ToLower()
			Dim inCCDir As Boolean = fpLower.Contains("content center files") OrElse
			fpLower.Contains(ChrW(36164) & ChrW(28304) & ChrW(20013) & ChrW(24515)) OrElse
			fpLower.Contains("zh-cn")
			Dim bomStructVal As Integer = If(bomStructMap.ContainsKey(fp), bomStructMap(fp), 51970)
			bomRow.IsStdIcon = bomRow.IsContentCenter OrElse inCCDir OrElse (catVal = ChrW(26631) & ChrW(20934) & ChrW(20214))
			bomRow.IsLocalStandard = inCCDir OrElse roCache(fp)
			bomRow.BomStruct = bomStructVal

			seen(fp) = result.Count
			bomRow.FixedIdx = result.Count + 1
			result.Add(bomRow)
		Loop

		' ── 回填数量（遍历完成后统一更新）──
		For Each row As BomDataRow In result
			If qtyMap.ContainsKey(Row.FilePath) Then Row.Qty = qtyMap(Row.FilePath)
		Next

		CalcLevelNumbers(result)
		Return result
	End Function

	Sub CalcLevelNumbers(rows As List(Of BomDataRow))
		Dim topCounter As Integer = 0
		For Each r As BomDataRow In rows
			If String.IsNullOrEmpty(r.ParentFilePath) Then
				topCounter += 1
				r.LevelNo = topCounter.ToString()
				AssignChildLevelNos(rows, r.FilePath, r.LevelNo)
			End If
		Next
	End Sub

	Sub AssignChildLevelNos(rows As List(Of BomDataRow), parentFp As String, parentNo As String)
		Dim childCounter As Integer = 0
		For Each r As BomDataRow In rows
			If String.Compare(r.ParentFilePath, parentFp, True) = 0 Then
				childCounter += 1
				r.LevelNo = parentNo & "." & childCounter.ToString()
				AssignChildLevelNos(rows, r.FilePath, r.LevelNo)
			End If
		Next
	End Sub


	Sub ShowReplaceValueDialog()
		Try
			Dim dlg As New Form()
			dlg.Text = "属性值批量替换字符"
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then dlg.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then dlg.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch : End Try
			dlg.Size = New System.Drawing.Size(Scaled(580), Scaled(520))
			dlg.StartPosition = FormStartPosition.CenterScreen
			dlg.MinimizeBox = False
			dlg.MaximizeBox = False
			dlg.Font = MakeFont("Microsoft YaHei", 9.0!)
			dlg.BackColor = C(252, 252, 252)

			Dim pad As Integer = Scaled(10)
			Dim cw As Integer = dlg.ClientSize.Width - pad * 2
			Dim hw As Integer = (cw - Scaled(8)) \ 2

			' ── 第一行：目标 ──
			Dim lblProp As New Label()
			lblProp.Text = "替换对象（双击下方列表选择列）："
			lblProp.Location = New System.Drawing.Point(pad, pad)
			lblProp.Size = New System.Drawing.Size(cw, Scaled(20))
			lblProp.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblProp)

			Dim txtPropName As New System.Windows.Forms.TextBox()
			txtPropName.Location = New System.Drawing.Point(pad, pad + Scaled(22))
			txtPropName.Size = New System.Drawing.Size(cw, Scaled(24))
			txtPropName.Font = MakeFont("Microsoft YaHei", 9.5!)
			SetPlaceholder(txtPropName, "双击下方列表选择列")
			dlg.Controls.Add(txtPropName)

			' ── 第二行：查找 / 替换 ──
			Dim lblFind As New Label()
			lblFind.Text = "查找字符："
			lblFind.Location = New System.Drawing.Point(pad, pad + Scaled(54))
			lblFind.Size = New System.Drawing.Size(hw, Scaled(20))
			lblFind.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblFind)

			Dim txtFind As New System.Windows.Forms.TextBox()
			txtFind.Location = New System.Drawing.Point(pad, pad + Scaled(74))
			txtFind.Size = New System.Drawing.Size(hw, Scaled(24))
			txtFind.Font = MakeFont("Microsoft YaHei", 9.5!)
			SetPlaceholder(txtFind, "要查找的字符")
			dlg.Controls.Add(txtFind)

			Dim lblReplace As New Label()
			lblReplace.Text = "替换为："
			lblReplace.Location = New System.Drawing.Point(pad + hw + Scaled(8), pad + Scaled(54))
			lblReplace.Size = New System.Drawing.Size(hw, Scaled(20))
			lblReplace.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblReplace)

			Dim txtReplace As New System.Windows.Forms.TextBox()
			txtReplace.Location = New System.Drawing.Point(pad + hw + Scaled(8), pad + Scaled(74))
			txtReplace.Size = New System.Drawing.Size(hw, Scaled(24))
			txtReplace.Font = MakeFont("Microsoft YaHei", 9.5!)
			SetPlaceholder(txtReplace, "替换后的字符（留空=删除）")
			dlg.Controls.Add(txtReplace)

			' ── 按钮行 ──
			Dim btnExec As New Button()
			btnExec.Text = "执行替换"
			btnExec.Size = New System.Drawing.Size(Scaled(100), Scaled(28))
			btnExec.Location = New System.Drawing.Point(pad, pad + Scaled(108))
			btnExec.FlatStyle = FlatStyle.Flat
			btnExec.BackColor = OrangeMid()
			btnExec.ForeColor = System.Drawing.Color.White
			btnExec.Font = MakeFontBold("Microsoft YaHei", 8.5!)
			dlg.Controls.Add(btnExec)

			Dim btnClose As New Button()
			btnClose.Text = "关闭"
			btnClose.Size = New System.Drawing.Size(Scaled(80), Scaled(28))
			btnClose.Location = New System.Drawing.Point(pad + Scaled(108), pad + Scaled(108))
			btnClose.FlatStyle = FlatStyle.Flat
			btnClose.BackColor = C(180, 180, 180)
			btnClose.ForeColor = C(50, 50, 50)
			AddHandler btnClose.Click, Sub(s, E) dlg.Close()
			dlg.Controls.Add(btnClose)
			Dim helpReplace As String = "【功能说明】" & vbCrLf &
		   "  对指定列做批量查找替换。选「文件名」列会真正重命名磁盘文件，" & vbCrLf &
		   "  选其他属性列只改属性值。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 在下方「可替换列列表」双击要处理的列" & vbCrLf &
		   "  2. 在「查找字符」框输入要被替换的文字" & vbCrLf &
		   "  3. 在「替换为」框输入新文字（留空=删除该字符）" & vbCrLf &
		   "  4. 点「执行替换」" & vbCrLf &
		   "  5. 完成后弹窗列出成功/失败明细（带滚动条）" & vbCrLf & vbCrLf &
		   "【文件名列特别说明】" & vbCrLf &
		   "  · 会真正重命名文件并同步改关联工程图名" & vbCrLf &
		   "  · 同名冲突、镜像零件会判定失败并说明原因" & vbCrLf & vbCrLf &
		   "【普通属性列说明】" & vbCrLf &
		   "  · 只读零件无法修改，会在报告里列出"
			Dim btnReplaceHelp As System.Windows.Forms.Button = MakeStepHelpBtn("属性字符替换 - 使用说明", helpReplace, False)
			btnReplaceHelp.Location = New System.Drawing.Point(pad + Scaled(196), pad + Scaled(108))
			btnReplaceHelp.Height = Scaled(28)
			dlg.Controls.Add(btnReplaceHelp)

			' ── 列表 ──
			Dim lblResult As New Label()
			lblResult.Text = "可替换列列表（双击选择）："
			lblResult.Location = New System.Drawing.Point(pad, pad + Scaled(146))
			lblResult.Size = New System.Drawing.Size(cw, Scaled(18))
			lblResult.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblResult)

			Dim lstResult As New System.Windows.Forms.ListBox()
			lstResult.Location = New System.Drawing.Point(pad, pad + Scaled(166))
			lstResult.Size = New System.Drawing.Size(cw, dlg.ClientSize.Height - pad - Scaled(166) - pad)
			lstResult.Font = MakeFont("Microsoft YaHei", 9.0!)
			lstResult.BackColor = C(245, 250, 245)
			lstResult.SelectionMode = SelectionMode.One
			lstResult.Items.Add("正在扫描...")
			AddHandler lstResult.DoubleClick, Sub(s, E)
												  If lstResult.SelectedItem Is Nothing Then Return
												  Dim picked As String = CStr(lstResult.SelectedItem).Trim()
												  If picked.StartsWith("──") Then Return
												  txtPropName.Text = picked
												  txtFind.Focus()
											  End Sub
			dlg.Controls.Add(lstResult)

			' ── 打开时扫描：只收集可替换列 ──
			AddHandler dlg.Shown, Sub(ss, es)
									  Try
										  lstResult.Items.Clear()

										  ' ★【修改点1】固定排除的不可替换列
										  Dim skipCols As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  skipCols.Add("cThumb") ' 缩略图（图片）
										  skipCols.Add("cIdx")   ' 序号
										  skipCols.Add("cBom")   ' BOM结构
										  skipCols.Add("cType")  ' 类型（图标）

										  Dim colCount As Integer = 0
										  lstResult.Items.Add("── 可替换表格列 ──")
										  If g_grid IsNot Nothing AndAlso g_grid.Columns.Count > 0 Then
											  For Each col As DataGridViewColumn In g_grid.Columns
												  ' 跳过明确排除的列
												  If skipCols.Contains(col.Name) Then Continue For
												  ' 跳过只读列（统计数量、工程图是否有等自动计算/只读列）
												  If col.ReadOnly Then Continue For
												  ' 只保留可文字替换的文本框列（排除图片、按钮、下拉等）
												  If TypeOf col IsNot DataGridViewTextBoxColumn Then Continue For
												  Dim colName As String = col.Name
												  Dim colTitle As String = col.HeaderText
												  If String.IsNullOrEmpty(colTitle) Then colTitle = colName
												  lstResult.Items.Add("[列] " & colTitle & "|" & colName)
												  colCount += 1
											  Next
										  End If
										  If colCount = 0 Then lstResult.Items.Add("（无可替换列）")
									  Catch ex2 As Exception
										  lstResult.Items.Clear() : lstResult.Items.Add("扫描失败：" & ex2.Message)
									  End Try
								  End Sub

			' ── 执行替换 ──
			AddHandler btnExec.Click, Sub(s, E)
										  Dim propName2 As String = txtPropName.Text.Trim()
										  Dim findStr As String = txtFind.Text
										  If findStr = "要查找的字符" Then findStr = ""
										  Dim replStr As String = txtReplace.Text
										  If replStr = "替换后的字符（留空=删除）" Then replStr = ""

										  If String.IsNullOrEmpty(propName2) OrElse propName2 = "双击下方列表选择列" Then
											  MessageBox.Show("请先双击下方列表选择列！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
										  End If
										  If String.IsNullOrEmpty(findStr) Then
											  MessageBox.Show("请输入要查找的字符。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
										  End If

										  ' ★【修改点2】解析列键
										  Dim colKey As String = ""
										  If propName2.StartsWith("[列]", StringComparison.OrdinalIgnoreCase) Then
											  Dim barIdx As Integer = propName2.IndexOf("|")
											  If barIdx >= 0 Then
												  colKey = propName2.Substring(barIdx + 1).Trim()
											  Else
												  MessageBox.Show("无法识别该列表项（缺少列键）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
											  End If
										  End If

										  If colKey = "" Then
											  MessageBox.Show("请从列表选择一列。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
										  End If

										  ' ★【修改点3】对所选列的所有单元格做文字替换
										  Dim okCount As Integer = 0
										  Dim failList As New List(Of String)
										  If g_grid IsNot Nothing AndAlso g_grid.Columns.Contains(colKey) Then
											  For Each dgvRow As DataGridViewRow In g_grid.Rows
												  If dgvRow.IsNewRow Then Continue For
												  Dim Cell As DataGridViewCell = dgvRow.Cells(colKey)
												  Dim oldTxt As String = ""
												  If Cell.Value IsNot Nothing Then oldTxt = CStr(Cell.Value)
												  If String.IsNullOrEmpty(oldTxt) Then Continue For
												  Dim newTxt As String = oldTxt.Replace(findStr, replStr)
												  If newTxt <> oldTxt Then
													  If colKey = "cFile" Then
														  ' 文件名列：真正重命名磁盘文件
														  Dim origIdx As Integer = GetOrigRowIdx(dgvRow.Index)
														  If origIdx < 0 Then
															  failList.Add("行" & (dgvRow.Index + 1).ToString() & " 无法定位原始行")
														  Else
															  Dim r As BomDataRow = g_rows(origIdx)
															  Try
																  Dim ext As String = System.IO.Path.GetExtension(r.FilePath)
																  Dim newBase As String = StripInvExt(newTxt.Trim())
																  If String.IsNullOrEmpty(newBase) Then
																	  failList.Add(r.FileName & ": 新文件名为空")
																  Else
																	  If IsMirrorPart(r.FilePath) Then
																		  failList.Add(r.FileName & ": 镜像零件不允许改名")
																	  Else
																		  Dim dir As String = System.IO.Path.GetDirectoryName(r.FilePath)
																		  Dim newPath As String = System.IO.Path.Combine(dir, newBase & ext)
																		  If System.IO.File.Exists(newPath) AndAlso String.Compare(newPath, r.FilePath, True) <> 0 Then
																			  failList.Add(r.FileName & " -> " & newBase & ext & "  原因：目标文件已存在（同名冲突）")
																		  Else
																			  Dim errMsg As String = ""
																			  Dim okR As Boolean = RenameFileWithDrawing(r.FilePath, newBase, errMsg)
																			  If okR Then
																				  r.FilePath = newPath : r.OldFileName = newBase & ext : r.FileName = newBase & ext
																				  dgvRow.Cells("cFile").Value = newBase & ext
																				  okCount += 1
																			  Else
																				  failList.Add(r.FileName & " -> " & newBase & ext & "  原因：" & errMsg)
																			  End If
																		  End If
																	  End If
																  End If
															  Catch exR As Exception
																  failList.Add("行" & (dgvRow.Index + 1).ToString() & " 改名异常: " & exR.Message)
															  End Try
														  End If
													  Else
														  ' 普通属性列：检测只读后改单元格
														  Dim rowTag As String = ""
														  Try
															  If dgvRow.Cells.Count > 1 AndAlso dgvRow.Cells(1).Value IsNot Nothing Then rowTag = CStr(dgvRow.Cells(1).Value)
															  If String.IsNullOrEmpty(rowTag) Then rowTag = "行" & (dgvRow.Index + 1).ToString()
														  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
															  rowTag = "行" & (dgvRow.Index + 1).ToString()
														  End Try
														  ' 检测文件是否只读
														  Dim skipWrite As Boolean = False
														  Dim failReason As String = ""
														  Try
															  Dim oIdx As Integer = GetOrigRowIdx(dgvRow.Index)
															  If oIdx >= 0 AndAlso oIdx < g_rows.Count Then
																  Dim rr As BomDataRow = g_rows(oIdx)
																  If rr.IsContentCenter OrElse rr.IsLocalStandard Then
																	  skipWrite = True : failReason = "标准件/ContentCenter，不允许修改属性"
																  Else
																	  Try
																		  If System.IO.File.Exists(rr.FilePath) Then
																			  Dim fa As System.IO.FileAttributes = System.IO.File.GetAttributes(rr.FilePath)
																			  If (fa And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly Then
																				  skipWrite = True : failReason = "文件只读，属性无法修改"
																			  End If
																		  End If
																	  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
																	  End Try
																  End If
															  End If
														  Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
														  End Try
														  If skipWrite Then
															  failList.Add("[" & rowTag & "] " & oldTxt & " -> " & newTxt & "  原因：" & failReason)
														  Else
															  Try
																  Cell.Value = newTxt
																  okCount += 1
															  Catch exCell As Exception
																  failList.Add("[" & rowTag & "] " & oldTxt & " -> " & newTxt & "  原因：" & exCell.Message)
															  End Try
														  End If
													  End If
												  End If
											  Next
										  Else
											  failList.Add("列不存在或表格为空：[" & colKey & "]")
										  End If

										  ' === 结果弹窗（带滚动条，固定大小）===
										  Dim rptDlg As New Form()
										  rptDlg.Text = "替换结果"
										  rptDlg.Size = New System.Drawing.Size(Scaled(560), Scaled(420))
										  rptDlg.StartPosition = FormStartPosition.CenterScreen
										  rptDlg.MinimizeBox = False
										  rptDlg.MaximizeBox = False
										  rptDlg.Font = MakeFont("Microsoft YaHei", 9.0!)
										  rptDlg.BackColor = C(252, 252, 252)
										  Dim rptPad As Integer = Scaled(10)
										  Dim lblSum As New Label()
										  lblSum.Text = "表格列 [" & colKey & "] 替换完成，成功 " & okCount & " 个，失败 " & failList.Count & " 个。"
										  lblSum.Location = New System.Drawing.Point(rptPad, rptPad)
										  lblSum.Size = New System.Drawing.Size(rptDlg.ClientSize.Width - rptPad * 2, Scaled(22))
										  lblSum.ForeColor = If(failList.Count = 0, C(0, 110, 50), C(180, 30, 30))
										  rptDlg.Controls.Add(lblSum)
										  Dim txtRpt As New System.Windows.Forms.TextBox()
										  txtRpt.Multiline = True
										  txtRpt.ReadOnly = True
										  txtRpt.ScrollBars = System.Windows.Forms.ScrollBars.Both
										  txtRpt.WordWrap = False
										  txtRpt.Location = New System.Drawing.Point(rptPad, rptPad + Scaled(28))
										  txtRpt.Size = New System.Drawing.Size(rptDlg.ClientSize.Width - rptPad * 2, rptDlg.ClientSize.Height - rptPad * 2 - Scaled(60))
										  txtRpt.Font = MakeFont("Consolas", 9.0!)
										  If failList.Count = 0 Then
											  txtRpt.Text = "全部成功，无失败项。"
										  Else
											  txtRpt.Text = "失败明细：" & vbCrLf & String.Join(vbCrLf, failList.ToArray())
										  End If
										  rptDlg.Controls.Add(txtRpt)
										  Dim btnRptOk As New Button()
										  btnRptOk.Text = "确定"
										  btnRptOk.Size = New System.Drawing.Size(Scaled(90), Scaled(28))
										  btnRptOk.Location = New System.Drawing.Point((rptDlg.ClientSize.Width - Scaled(90)) \ 2, rptDlg.ClientSize.Height - Scaled(40))
										  btnRptOk.FlatStyle = FlatStyle.Flat
										  btnRptOk.BackColor = OrangeMid()
										  btnRptOk.ForeColor = System.Drawing.Color.White
										  AddHandler btnRptOk.Click, Sub(s2, e2) rptDlg.Close()
										  rptDlg.Controls.Add(btnRptOk)
										  rptDlg.ShowDialog()

										  If okCount > 0 Then
											  SetStatus("  表格列 [" & colKey & "] 替换完成，修改 " & okCount & " 个单元格", C(0, 130, 55))
										  End If
									  End Sub

			dlg.ShowDialog()
		Catch ex As Exception
			MessageBox.Show("属性值替换对话框错误：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub




	Sub ShowRenamePropDialog()
		Try
			Dim dlg As New Form()
			dlg.Text = "自定义属性改名"
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then dlg.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then dlg.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch : End Try
			dlg.Size = New System.Drawing.Size(Scaled(580), Scaled(480))
			dlg.StartPosition = FormStartPosition.CenterScreen
			dlg.MinimizeBox = False
			dlg.MaximizeBox = False
			dlg.Font = MakeFont("Microsoft YaHei", 9.0!)
			dlg.BackColor = C(252, 252, 252)

			Dim pad As Integer = Scaled(10)
			Dim cw As Integer = dlg.ClientSize.Width - pad * 2
			Dim hw As Integer = (cw - Scaled(8)) \ 2

			' ── 旧名称标签 + 输入框 ──
			Dim lblOld As New Label()
			lblOld.Text = "旧属性名（双击列表添加）："
			lblOld.Location = New System.Drawing.Point(pad, pad)
			lblOld.Size = New System.Drawing.Size(hw, Scaled(20))
			lblOld.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblOld)

			Dim txtOld As New System.Windows.Forms.TextBox()
			txtOld.Location = New System.Drawing.Point(pad, pad + Scaled(24))
			txtOld.Size = New System.Drawing.Size(hw, Scaled(24))
			txtOld.Font = MakeFont("Microsoft YaHei", 9.5!)
			SetPlaceholder(txtOld, "双击下方列表选择")
			dlg.Controls.Add(txtOld)

			' ── 新名称标签 + 输入框 ──
			Dim lblNew As New Label()
			lblNew.Text = "新属性名："
			lblNew.Location = New System.Drawing.Point(pad + hw + Scaled(8), pad)
			lblNew.Size = New System.Drawing.Size(hw, Scaled(20))
			lblNew.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblNew)

			Dim txtNew As New System.Windows.Forms.TextBox()
			txtNew.Location = New System.Drawing.Point(pad + hw + Scaled(8), pad + Scaled(24))
			txtNew.Size = New System.Drawing.Size(hw, Scaled(24))
			txtNew.Font = MakeFont("Microsoft YaHei", 9.5!)
			SetPlaceholder(txtNew, "输入新属性名")
			dlg.Controls.Add(txtNew)

			' ── 按钮行 ──

			Dim btnRename As New Button()
			btnRename.Text = "执行改名"
			btnRename.Size = New System.Drawing.Size(Scaled(100), Scaled(28))
			btnRename.Location = New System.Drawing.Point(pad, pad + Scaled(56))
			btnRename.FlatStyle = FlatStyle.Flat
			btnRename.BackColor = OrangeMid()
			btnRename.ForeColor = System.Drawing.Color.White
			btnRename.Font = MakeFontBold("Microsoft YaHei", 8.5!)
			dlg.Controls.Add(btnRename)

			Dim btnClose As New Button()
			btnClose.Text = "关闭"
			btnClose.Size = New System.Drawing.Size(Scaled(80), Scaled(28))
			btnClose.Location = New System.Drawing.Point(pad + Scaled(108), pad + Scaled(56))
			btnClose.FlatStyle = FlatStyle.Flat
			btnClose.BackColor = C(180, 180, 180)
			btnClose.ForeColor = C(50, 50, 50)
			AddHandler btnClose.Click, Sub(s, e) dlg.Close()
			dlg.Controls.Add(btnClose)
			Dim helpRenameProp As String = "【功能说明】" & vbCrLf &
		   "  把所有零件里的某个自定义属性整体改名为新名称（属性值不变）。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 在下方属性列表双击旧属性名，自动填入「旧属性名」框" & vbCrLf &
		   "  2. 在「新属性名」框输入要改成的名字" & vbCrLf &
		   "  3. 点「执行改名」" & vbCrLf &
		   "  4. 完成后弹窗提示成功/失败数量" & vbCrLf & vbCrLf &
		   "【常见问题】" & vbCrLf &
		   "  · 提示「新属性名已存在」：该零件已有同名属性，会跳过" & vbCrLf &
		   "  · 只读零件无法改名，会在报告里列出" & vbCrLf &
		   "  · 改名只改属性名，不影响属性值和零件文件"
			Dim btnRenamePropHelp As System.Windows.Forms.Button = MakeStepHelpBtn("自定义属性改名 - 使用说明", helpRenameProp, False)
			btnRenamePropHelp.Location = New System.Drawing.Point(pad + Scaled(196), pad + Scaled(56))
			btnRenamePropHelp.Height = Scaled(28)
			dlg.Controls.Add(btnRenamePropHelp)

			' ── 属性列表标签 ──
			Dim lblResult As New Label()
			lblResult.Text = "自定义属性列表（双击添加到旧名称输入框）："
			lblResult.Location = New System.Drawing.Point(pad, pad + Scaled(94))
			lblResult.Size = New System.Drawing.Size(cw, Scaled(18))
			lblResult.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblResult)

			' ── 属性 ListBox ──
			Dim lstResult As New System.Windows.Forms.ListBox()
			lstResult.Location = New System.Drawing.Point(pad, pad + Scaled(114))
			lstResult.Size = New System.Drawing.Size(cw, dlg.ClientSize.Height - pad - Scaled(114) - pad)
			lstResult.Font = MakeFont("Microsoft YaHei", 9.0!)
			lstResult.BackColor = C(245, 250, 245)
			lstResult.SelectionMode = SelectionMode.One
			lstResult.Items.Add("正在扫描自定义属性...")
			' 双击：把选中项填入旧名称输入框
			AddHandler lstResult.DoubleClick, Sub(s, e)
												  If lstResult.SelectedItem Is Nothing Then Return
												  txtOld.Text = CStr(lstResult.SelectedItem).Trim()
												  txtNew.Focus()
											  End Sub
			dlg.Controls.Add(lstResult)


			' ── 打开时自动扫描自定义属性 ──
			AddHandler dlg.Shown, Sub(ss, es)
									  Try
										  Dim propSet4 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim seen4 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rr4 As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rr4.FilePath) OrElse rr4.IsMissing Then Continue For
											  If seen4.Contains(rr4.FilePath) Then Continue For
											  seen4.Add(rr4.FilePath)
											  Try
												  Dim tDoc4 As Document = Nothing
												  For Each od4 As Document In ThisApplication.Documents
													  If String.Compare(od4.FullDocumentName, rr4.FilePath, True) = 0 Then tDoc4 = od4 : Exit For
												  Next
												  If tDoc4 Is Nothing AndAlso System.IO.File.Exists(rr4.FilePath) Then
													  tDoc4 = ThisApplication.Documents.Open(rr4.FilePath, False)
												  End If
												  If tDoc4 Is Nothing Then Continue For
												  Try
													  Dim udp4 As PropertySet = tDoc4.PropertySets.Item(GUID_UDP)
													  For Each prop4 As Inventor.Property In udp4
														  Try
															  Dim pdn4 As String = prop4.DisplayName.Trim()
															  If String.IsNullOrEmpty(pdn4) Then pdn4 = prop4.Name.Trim()
															  propSet4.Add(pdn4)
														  Catch : End Try
													  Next
												  Catch : End Try
											  Catch : End Try
										  Next
										  lstResult.Items.Clear()
										  If propSet4.Count = 0 Then
											  lstResult.Items.Add("（未找到任何自定义属性）")
										  Else
											  Dim sortedList4 As New List(Of String)(propSet4)
											  sortedList4.Sort()
											  For Each pn4 As String In sortedList4 : lstResult.Items.Add(pn4) : Next
										  End If
									  Catch ex4 As Exception
										  lstResult.Items.Clear() : lstResult.Items.Add("扫描失败：" & ex4.Message)
									  End Try
								  End Sub

			' ── 执行改名按钮 ──
			AddHandler btnRename.Click, Sub(s, e)
											Dim oldName As String = txtOld.Text.Trim()
											Dim newName As String = txtNew.Text.Trim()
											If String.IsNullOrEmpty(oldName) OrElse oldName = "双击下方列表选择" Then
												MessageBox.Show("请先双击下方列表选择要改名的属性！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
											End If
											If String.IsNullOrEmpty(newName) Then
												MessageBox.Show("请输入新属性名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
											End If
											If String.Compare(oldName, newName, True) = 0 Then
												MessageBox.Show("新旧名称相同，无需改名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
											End If
											Dim confirm As DialogResult = MessageBox.Show(
	"确认将所有零部件的自定义属性 [" & oldName & "] 改名为 [" & newName & "]？",
	"确认改名", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
											If confirm <> DialogResult.Yes Then Return
											Dim okCount As Integer = 0
											Dim failList As New List(Of String)
											Dim seen3 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
											For Each rr As BomDataRow In g_rows
												If seen3.Contains(rr.FilePath) Then Continue For
												seen3.Add(rr.FilePath)
												Try
													Dim tDoc As Document = Nothing
													For Each od As Document In ThisApplication.Documents
														If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then
															tDoc = od : Exit For
														End If
													Next
													If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(rr.FilePath, False)
													Dim udp As PropertySet = tDoc.PropertySets.Item(GUID_UDP)
													Try
														Dim oldProp As Inventor.Property = CType(udp.Item(oldName), Inventor.Property)
														Dim oldVal As String = ""
														Try : oldVal = CStr(oldProp.Value) : Catch : End Try
														' 删除旧属性，新建同名属性
														oldProp.Delete()

														udp.Add(oldVal, newName)
														' 保存文件使改名实时写入磁盘
														Try : tDoc.Save() : Catch : End Try
														okCount += 1

													Catch : End Try
												Catch ex As Exception
													failList.Add(System.IO.Path.GetFileName(rr.FilePath) & ": " & ex.Message)
												End Try
											Next
											Dim msg As String = "改名完成，共处理 " & okCount & " 个文件。"
											If failList.Count > 0 Then msg &= vbCrLf & "失败 " & failList.Count & " 个：" & vbCrLf & String.Join(vbCrLf, failList.ToArray())
											MessageBox.Show(msg, "属性改名结果", MessageBoxButtons.OK, MessageBoxIcon.Information)
											' ★ 同步更新表格列和右侧列管理中的列标题
											If okCount > 0 Then
												For Each cd As ColDef In g_colDefs
													If cd.IsCustom AndAlso String.Compare(cd.PropName, oldName, True) = 0 Then
														cd.PropName = newName
														cd.Header = newName
														' 更新表格列标题
														If g_grid IsNot Nothing AndAlso g_grid.Columns.Contains(cd.ColName) Then
															g_grid.Columns(cd.ColName).HeaderText = newName & " ✎"
														End If
														' 更新右侧列管理行标签
														If g_chkFlow IsNot Nothing Then
															For Each ctrl As Control In g_chkFlow.Controls
																If ctrl.Tag IsNot Nothing AndAlso CStr(ctrl.Tag) = cd.ColName Then
																	For Each child As Control In ctrl.Controls
																		If TypeOf child Is Label Then
																			CType(child, Label).Text = newName
																			Exit For
																		End If
																	Next
																	Exit For
																End If
															Next
														End If
														' 更新 BomDataRow.Custom 键名
														For Each rr As BomDataRow In g_rows
															If rr.Custom.ContainsKey(oldName) Then
																Dim v As String = rr.Custom(oldName)
																rr.Custom.Remove(oldName)
																rr.Custom(newName) = v
															End If
														Next
														Exit For
													End If
												Next
												SaveSettings()
												SetStatus("  属性 [" & oldName & "] 已改名为 [" & newName & "]", C(0, 130, 55))
											End If
											' 改名后重新扫描属性列表
											lstResult.Items.Clear()
											lstResult.Items.Add("正在重新扫描...")
											Try
												Dim _propSetR As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
												Dim _seenR As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
												For Each _rrR As BomDataRow In g_rows
													If String.IsNullOrEmpty(_rrR.FilePath) OrElse _rrR.IsMissing Then Continue For
													If _seenR.Contains(_rrR.FilePath) Then Continue For
													_seenR.Add(_rrR.FilePath)
													Try
														Dim _tDocR As Document = Nothing
														For Each _odR As Document In ThisApplication.Documents
															If String.Compare(_odR.FullDocumentName, _rrR.FilePath, True) = 0 Then _tDocR = _odR : Exit For
														Next
														If _tDocR Is Nothing AndAlso System.IO.File.Exists(_rrR.FilePath) Then _tDocR = ThisApplication.Documents.Open(_rrR.FilePath, False)
														If _tDocR Is Nothing Then Continue For
														Dim _udpR As PropertySet = GetUDP(_tDocR)
														If _udpR Is Nothing Then Continue For
														For Each _pR As Inventor.Property In _udpR
															Try : _propSetR.Add(_pR.Name) : Catch : End Try
														Next
													Catch : End Try
												Next
												lstResult.Items.Clear()
												Dim _sortedR As New List(Of String)(_propSetR)
												_sortedR.Sort()
												For Each _pnR As String In _sortedR
													lstResult.Items.Add(_pnR)
												Next
												If lstResult.Items.Count = 0 Then lstResult.Items.Add("（无自定义属性）")
											Catch : End Try
										End Sub

			dlg.ShowDialog()
		Catch ex As Exception
			MessageBox.Show("属性改名对话框错误：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub ShowBatchDeletePropDialog()
		Try
			Dim dlg As New Form()
			dlg.Text = "批量删除自定义属性"
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then dlg.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then dlg.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch : End Try
			dlg.Size = New System.Drawing.Size(Scaled(560), Scaled(480))
			dlg.StartPosition = FormStartPosition.CenterScreen
			dlg.MinimizeBox = False
			dlg.MaximizeBox = False
			dlg.Font = MakeFont("Microsoft YaHei", 9.0!)
			dlg.BackColor = C(252, 252, 252)

			Dim pad As Integer = Scaled(10)
			Dim cw As Integer = dlg.ClientSize.Width - pad * 2

			' ── 说明标签 ──
			Dim lblTip As New Label()
			lblTip.Text = "输入要删除的自定义属性名（多个用英文逗号分隔）："
			lblTip.Location = New System.Drawing.Point(pad, pad)
			lblTip.Size = New System.Drawing.Size(cw, Scaled(20))
			lblTip.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblTip)

			' ── 属性名输入框 ──
			Dim txtProp As New System.Windows.Forms.TextBox()
			txtProp.Location = New System.Drawing.Point(pad, pad + Scaled(24))
			txtProp.Size = New System.Drawing.Size(cw, Scaled(24))
			txtProp.Font = MakeFont("Microsoft YaHei", 9.5!)
			SetPlaceholder(txtProp, "例：图号,备注,供应商")
			dlg.Controls.Add(txtProp)

			' ── 按钮行 ──

			Dim btnDel As New Button()
			btnDel.Text = "执行删除"
			btnDel.Size = New System.Drawing.Size(Scaled(100), Scaled(28))
			btnDel.Location = New System.Drawing.Point(pad, pad + Scaled(56))
			btnDel.FlatStyle = FlatStyle.Flat
			btnDel.BackColor = C(200, 50, 50)
			btnDel.ForeColor = System.Drawing.Color.White
			btnDel.Font = MakeFontBold("Microsoft YaHei", 8.5!)
			dlg.Controls.Add(btnDel)

			Dim btnClose As New Button()
			btnClose.Text = "关闭"
			btnClose.Size = New System.Drawing.Size(Scaled(80), Scaled(28))
			btnClose.Location = New System.Drawing.Point(pad + Scaled(108), pad + Scaled(56))
			btnClose.FlatStyle = FlatStyle.Flat
			btnClose.BackColor = C(180, 180, 180)
			btnClose.ForeColor = C(50, 50, 50)
			AddHandler btnClose.Click, Sub(s, e) dlg.Close()
			dlg.Controls.Add(btnClose)
			Dim helpDelProp As String = "【功能说明】" & vbCrLf &
		   "  从所有零件里删除指定的自定义属性（不影响内置属性）。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 在下方属性列表里双击要删除的属性名（可多选后双击）" & vbCrLf &
		   "     也可直接在输入框手动输入，多个属性用英文逗号分隔" & vbCrLf &
		   "  2. 核对输入框里的属性名" & vbCrLf &
		   "  3. 点「执行删除」" & vbCrLf &
		   "  4. 完成后弹窗提示删除了多少个、跳过多少个" & vbCrLf & vbCrLf &
		   "【注意】" & vbCrLf &
		   "  · 删除属性不可撤销，建议先备份" & vbCrLf &
		   "  · 只读零件无法删除，会在报告里列出" & vbCrLf &
		   "  · 内置属性（图号/材料/质量等）不受影响"
			Dim btnDelPropHelp As System.Windows.Forms.Button = MakeStepHelpBtn("批量删除属性 - 使用说明", helpDelProp, False)
			btnDelPropHelp.Location = New System.Drawing.Point(pad + Scaled(196), pad + Scaled(56))
			btnDelPropHelp.Height = Scaled(28)
			dlg.Controls.Add(btnDelPropHelp)

			' ── 结果文本框（只读，可复制）──
			Dim lblResult As New Label()
			lblResult.Text = "自定义属性列表（双击直接添加到输入框，可多选）："
			lblResult.Location = New System.Drawing.Point(pad, pad + Scaled(94))
			lblResult.Size = New System.Drawing.Size(cw, Scaled(18))
			lblResult.ForeColor = System.Drawing.Color.Black
			dlg.Controls.Add(lblResult)

			Dim lstResult As New System.Windows.Forms.ListBox()
			lstResult.Location = New System.Drawing.Point(pad, pad + Scaled(114))
			lstResult.Size = New System.Drawing.Size(cw, dlg.ClientSize.Height - pad - Scaled(114) - pad)
			lstResult.Font = MakeFont("Microsoft YaHei", 9.0!)
			lstResult.BackColor = C(245, 250, 245)
			lstResult.SelectionMode = SelectionMode.MultiExtended
			lstResult.Items.Add("正在扫描自定义属性...")
			' 双击：把选中项追加到输入框
			AddHandler lstResult.DoubleClick, Sub(s, e)
												  If lstResult.SelectedItem Is Nothing Then Return
												  Dim picked As String = CStr(lstResult.SelectedItem).Trim()
												  If String.IsNullOrEmpty(picked) Then Return
												  Dim cur As String = txtProp.Text.Trim()
												  ' 过滤提示文字
												  If cur = "例：图号,备注,供应商" Then cur = "" : txtProp.ForeColor = System.Drawing.Color.Black
												  If String.IsNullOrEmpty(cur) Then
													  txtProp.Text = picked
												  Else
													  ' 避免重复添加
													  Dim existing() As String = cur.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
													  Dim alreadyIn As Boolean = False
													  For Each ex2 As String In existing
														  If String.Compare(ex2.Trim(), picked, True) = 0 Then alreadyIn = True : Exit For
													  Next
													  If Not alreadyIn Then txtProp.Text = cur & "," & picked
												  End If
											  End Sub
			dlg.Controls.Add(lstResult)


			' ── 打开时自动扫描自定义属性 ──
			AddHandler dlg.Shown, Sub(ss, es)
									  Try
										  Dim propSet2 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim seen3 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rr3 As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rr3.FilePath) OrElse rr3.IsMissing Then Continue For
											  If seen3.Contains(rr3.FilePath) Then Continue For
											  seen3.Add(rr3.FilePath)
											  Try
												  Dim tDoc3 As Document = Nothing
												  For Each od3 As Document In ThisApplication.Documents
													  If String.Compare(od3.FullDocumentName, rr3.FilePath, True) = 0 Then tDoc3 = od3 : Exit For
												  Next
												  If tDoc3 Is Nothing AndAlso System.IO.File.Exists(rr3.FilePath) Then
													  tDoc3 = ThisApplication.Documents.Open(rr3.FilePath, False)
												  End If
												  If tDoc3 Is Nothing Then Continue For
												  Try
													  Dim udp3 As PropertySet = tDoc3.PropertySets.Item(GUID_UDP)
													  For Each prop3 As Inventor.Property In udp3
														  Try
															  Dim pdn3 As String = prop3.DisplayName.Trim()
															  If String.IsNullOrEmpty(pdn3) Then pdn3 = prop3.Name.Trim()
															  propSet2.Add(pdn3)
														  Catch : End Try
													  Next
												  Catch : End Try
											  Catch : End Try
										  Next
										  lstResult.Items.Clear()
										  If propSet2.Count = 0 Then
											  lstResult.Items.Add("（未找到任何自定义属性）")
										  Else
											  Dim sortedList3 As New List(Of String)(propSet2)
											  sortedList3.Sort()
											  For Each pn3 As String In sortedList3 : lstResult.Items.Add(pn3) : Next
										  End If
									  Catch ex3 As Exception
										  lstResult.Items.Clear() : lstResult.Items.Add("扫描失败：" & ex3.Message)
									  End Try
								  End Sub

			' ── 执行删除按钮 ──
			AddHandler btnDel.Click, Sub(s, e)
										 Dim input As String = txtProp.Text.Trim()
										 If String.IsNullOrEmpty(input) OrElse input = ChrW(20363) & ChrW(65306) & ChrW(22270) & ChrW(21495) & "," & ChrW(22791) & ChrW(27880) & "," & ChrW(20379) & ChrW(24212) & ChrW(21830) Then
											 MessageBox.Show("请先双击下方列表添加要删除的属性名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
											 Return
										 End If
										 Dim names() As String = input.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
										 Dim cleanNames As New List(Of String)
										 For Each nm As String In names
											 Dim t As String = nm.Trim()
											 If Not String.IsNullOrEmpty(t) Then cleanNames.Add(t)
										 Next
										 If cleanNames.Count = 0 Then
											 MessageBox.Show("请先双击下方列表添加要删除的属性名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
											 Return
										 End If
										 Dim confirm As DialogResult = MessageBox.Show(
	"确认从所有零部件中删除以下自定义属性？" & vbCrLf & String.Join(vbCrLf, cleanNames.ToArray()) & vbCrLf & vbCrLf & "此操作不可撤销！",
	"确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
										 If confirm <> DialogResult.Yes Then Return
										 Dim okCount As Integer = 0
										 Dim failList As New List(Of String)
										 Dim seen3 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										 For Each rr As BomDataRow In g_rows
											 If seen3.Contains(rr.FilePath) Then Continue For
											 seen3.Add(rr.FilePath)
											 Try
												 Dim tDoc As Document = Nothing
												 For Each od As Document In ThisApplication.Documents
													 If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then
														 tDoc = od : Exit For
													 End If
												 Next
												 If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(rr.FilePath, False)
												 Dim udp As PropertySet = tDoc.PropertySets.Item(GUID_UDP)
												 For Each nm As String In cleanNames
													 Try
														 udp.Item(nm).Delete()
														 okCount += 1
													 Catch : End Try
												 Next
											 Catch ex As Exception
												 failList.Add(System.IO.Path.GetFileName(rr.FilePath) & ": " & ex.Message)
											 End Try
										 Next
										 Dim msg As String = "删除完成，共删除 " & okCount & " 个属性。"
										 If failList.Count > 0 Then msg &= vbCrLf & "失败 " & failList.Count & " 个文件：" & vbCrLf & String.Join(vbCrLf, failList.ToArray())
										 MessageBox.Show(msg, "批量删除结果", MessageBoxButtons.OK, MessageBoxIcon.Information)
										 ' ★ 删除后同步：移除表格中对应的自定义列 + 右侧列管理行
										 If okCount > 0 Then
											 For Each nm As String In cleanNames
												 ' 找到 PropName 匹配的 ColDef
												 Dim toRemove As ColDef = Nothing
												 For Each cd As ColDef In g_colDefs
													 If cd.IsCustom AndAlso String.Compare(cd.PropName, nm, True) = 0 Then
														 toRemove = cd : Exit For
													 End If
												 Next
												 If toRemove Is Nothing Then Continue For
												 ' 1. 删除表格列
												 If g_grid IsNot Nothing AndAlso g_grid.Columns.Contains(toRemove.ColName) Then
													 If g_grid IsNot Nothing Then RemoveHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
													 g_grid.Columns.Remove(toRemove.ColName)
													 If g_grid IsNot Nothing Then AddHandler g_grid.CellValueChanged, AddressOf OnCellValueChanged
												 End If
												 ' 2. 删除右侧列管理行
												 If g_chkFlow IsNot Nothing Then
													 For Each ctrl As Control In g_chkFlow.Controls
														 If ctrl.Tag IsNot Nothing AndAlso CStr(ctrl.Tag) = toRemove.ColName Then
															 g_chkFlow.Controls.Remove(ctrl)
															 Exit For
														 End If
													 Next
												 End If
												 ' 3. 从 g_colDefs 移除
												 g_colDefs.Remove(toRemove)
												 ' 4. 清除 BomDataRow.Custom 中的对应数据
												 For Each rr As BomDataRow In g_rows
													 If rr.Custom.ContainsKey(toRemove.PropName) Then
														 rr.Custom.Remove(toRemove.PropName)
													 End If
												 Next
											 Next
											 ' 5. 保存设置（列管理状态持久化）
											 SaveSettings()
											 SetStatus("  已删除 " & cleanNames.Count & " 个自定义属性列", C(0, 130, 55))
										 End If
										 ' 删除后重新扫描属性列表
										 lstResult.Items.Clear()
										 lstResult.Items.Add("正在重新扫描...")
										 Try
											 Dim propSet2b As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
											 Dim seen3b As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
											 For Each rr3b As BomDataRow In g_rows
												 If String.IsNullOrEmpty(rr3b.FilePath) OrElse rr3b.IsMissing Then Continue For
												 If seen3b.Contains(rr3b.FilePath) Then Continue For
												 seen3b.Add(rr3b.FilePath)
												 Try
													 Dim tDoc3b As Document = Nothing
													 For Each od3b As Document In ThisApplication.Documents
														 If String.Compare(od3b.FullDocumentName, rr3b.FilePath, True) = 0 Then tDoc3b = od3b : Exit For
													 Next
													 If tDoc3b Is Nothing AndAlso System.IO.File.Exists(rr3b.FilePath) Then
														 tDoc3b = ThisApplication.Documents.Open(rr3b.FilePath, False)
													 End If
													 If tDoc3b Is Nothing Then Continue For
													 Dim udp3b As PropertySet = GetUDP(tDoc3b)
													 If udp3b Is Nothing Then Continue For
													 For Each p3b As Inventor.Property In udp3b
														 Try : propSet2b.Add(p3b.Name) : Catch : End Try
													 Next
												 Catch : End Try
											 Next
											 lstResult.Items.Clear()
											 Dim sortedProps As New List(Of String)(propSet2b)
											 sortedProps.Sort()
											 For Each pn3b As String In sortedProps
												 lstResult.Items.Add(pn3b)
											 Next
											 If lstResult.Items.Count = 0 Then lstResult.Items.Add("（无自定义属性）")
										 Catch ex3b As Exception
											 lstResult.Items.Clear() : lstResult.Items.Add("扫描失败：" & ex3b.Message)
										 End Try
									 End Sub

			dlg.ShowDialog()
		Catch ex As Exception
			MessageBox.Show("批量删除对话框错误：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub ShowFireworks(ownerFrm As Form)
		Try
			' 全屏透明置顶窗口
			Dim fw As New Form()
			fw.FormBorderStyle = FormBorderStyle.None
			fw.BackColor = System.Drawing.Color.Black
			fw.TransparencyKey = System.Drawing.Color.Black
			fw.TopMost = True
			fw.ShowInTaskbar = False
			fw.Bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
			fw.Cursor = Cursors.Default

			' 粒子结构
			Dim rnd As New Random()
			Dim pCount As Integer = 180
			Dim px() As Single = New Single(pCount - 1) {}
			Dim py() As Single = New Single(pCount - 1) {}
			Dim pvx() As Single = New Single(pCount - 1) {}
			Dim pvy() As Single = New Single(pCount - 1) {}
			Dim palpha() As Integer = New Integer(pCount - 1) {}
			Dim pcolor() As System.Drawing.Color = New System.Drawing.Color(pCount - 1) {}
			Dim psize() As Single = New Single(pCount - 1) {}

			' 烟花颜色表
			Dim colors() As System.Drawing.Color = {
			System.Drawing.Color.FromArgb(255, 80, 80),
			System.Drawing.Color.FromArgb(255, 200, 0),
			System.Drawing.Color.FromArgb(0, 220, 80),
			System.Drawing.Color.FromArgb(103, 177, 230),
			System.Drawing.Color.FromArgb(255, 120, 0),
			System.Drawing.Color.FromArgb(220, 80, 255),
			System.Drawing.Color.White
			}

			' 多个发射中心（屏幕随机位置）
			Dim sw As Integer = fw.Width
			Dim sh As Integer = fw.Height
			Dim burstCount As Integer = 5
			Dim perBurst As Integer = pCount \ burstCount
			For b As Integer = 0 To burstCount - 1
				Dim cx As Single = CSng(rnd.Next(sw \ 4, sw * 3 \ 4))
				Dim cy As Single = CSng(rnd.Next(sh \ 5, sh * 2 \ 3))
				Dim clr As System.Drawing.Color = colors(rnd.Next(colors.Length))
				For i As Integer = b * perBurst To Math.Min((b + 1) * perBurst - 1, pCount - 1)
					Dim angle As Double = rnd.NextDouble() * Math.PI * 2
					Dim speed As Single = CSng(rnd.NextDouble() * 8 + 2)
					px(i) = cx
					py(i) = cy
					pvx(i) = CSng(Math.Cos(angle) * speed)
					pvy(i) = CSng(Math.Sin(angle) * speed)
					palpha(i) = 255
					pcolor(i) = clr
					psize(i) = CSng(rnd.NextDouble() * 3 + 2)
				Next
			Next

			' 绘制事件
			AddHandler fw.Paint, Sub(s, e)
									 Dim g As System.Drawing.Graphics = e.Graphics
									 g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
									 For i As Integer = 0 To pCount - 1
										 If palpha(i) <= 0 Then Continue For
										 Dim clr As System.Drawing.Color = System.Drawing.Color.FromArgb(
				palpha(i), pcolor(i).R, pcolor(i).G, pcolor(i).B)
										 Using br As New System.Drawing.SolidBrush(clr)
											 Dim sz As Single = psize(i)
											 g.FillEllipse(br, px(i) - sz / 2, py(i) - sz / 2, sz, sz)
										 End Using
									 Next
								 End Sub

			' 物理更新 Timer（每帧16ms ≈ 60fps）
			Dim fwTimer As New System.Windows.Forms.Timer()
			fwTimer.Interval = 16
			AddHandler fwTimer.Tick, Sub(s, e)
										 For i As Integer = 0 To pCount - 1
											 If palpha(i) <= 0 Then Continue For
											 px(i) += pvx(i)
											 py(i) += pvy(i)
											 pvy(i) += 0.25!
											 pvx(i) *= 0.97!
											 pvy(i) *= 0.97!
											 palpha(i) = Math.Max(0, palpha(i) - 4)
										 Next
										 fw.Invalidate()
									 End Sub
			fwTimer.Start()

			' 2秒后关闭
			Dim closeTimer As New System.Windows.Forms.Timer()
			closeTimer.Interval = 2000
			AddHandler closeTimer.Tick, Sub(s, e)
											closeTimer.Stop()
											fwTimer.Stop()
											fwTimer.Dispose()
											closeTimer.Dispose()
											fw.Close()
											fw.Dispose()
										End Sub
			closeTimer.Start()

			' 点击烟花窗口直接关闭
			AddHandler fw.MouseClick, Sub(s, e)
										  closeTimer.Stop() : fwTimer.Stop()
										  closeTimer.Dispose() : fwTimer.Dispose()
										  fw.Close() : fw.Dispose()
									  End Sub

			fw.Show()
		Catch : End Try
	End Sub
	' ══════════════════════════════════════════




	' ══════════════════════════════════════════
	' 打印装配顺序工程图（带进度条界面）
	Sub ShowPrintAssemblyDialog()
		Try
			Dim oApp As Inventor.Application = ThisApplication
			Dim oDoc = oApp.ActiveDocument
			If Not oDoc.DocumentType = kAssemblyDocumentObject Then
				MessageBox.Show("必须在部件(总装)中运行！", "DWJ")
				Return
			End If
			Dim idwList As New List(Of String)()
			Dim printed As New List(Of String)()
			Dim oFullFileName = oDoc.FullFileName
			Dim topIdw = System.IO.Path.ChangeExtension(oFullFileName, ".idw")
			If System.IO.File.Exists(topIdw) Then idwList.Add(topIdw)
			printed.Add(oFullFileName)
			CollectIdwFiles(oDoc.ComponentDefinition, printed, idwList)
			If idwList.Count = 0 Then
				MessageBox.Show("未找到任何工程图文件！", "DWJ")
				Return
			End If
			Dim frmPrint As New Form()
			frmPrint.Text = "打印装配顺序工程图"
			frmPrint.Width = Scaled(600) : frmPrint.Height = Scaled(420)
			frmPrint.StartPosition = FormStartPosition.CenterScreen
			frmPrint.FormBorderStyle = FormBorderStyle.FixedDialog
			frmPrint.MaximizeBox = False : frmPrint.MinimizeBox = False
			frmPrint.Font = MakeFont(g_fontName, 9.5!) : frmPrint.BackColor = C(245, 248, 252)
			' ── 标题栏 ──
			Dim pnlTitle As New Panel()
			pnlTitle.Dock = DockStyle.Top : pnlTitle.Height = Scaled(40) : pnlTitle.BackColor = OrangeMid()
			Dim lblTitleBar As New Label()
			lblTitleBar.Text = "  🖨 打印装配顺序工程图" : lblTitleBar.Dock = DockStyle.Fill
			lblTitleBar.ForeColor = System.Drawing.Color.White : lblTitleBar.Font = MakeFontBold(g_fontName, 11.0!)
			lblTitleBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnlTitle.Controls.Add(lblTitleBar)
			Dim helpPrint As String = "【功能说明】" & vbCrLf &
		   "  按装配树从上到下的顺序，批量打印（或导出PDF）所有零件的工程图。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 点「选择打印机」挑选要用的打印机（或选「导出PDF」模式）" & vbCrLf &
		   "  2. 设置打印备注文字（会盖在工程图上，可留空）" & vbCrLf &
		   "  3. 设置备注位置 X/Y 和字号" & vbCrLf &
		   "  4. 勾选是否加打印时间" & vbCrLf &
		   "  5. PDF 模式需选择保存文件夹" & vbCrLf &
		   "  6. 点「开始打印/导出」" & vbCrLf & vbCrLf &
		   "【注意】" & vbCrLf &
		   "  · 打印顺序 = 装配树顺序，便于按图施工" & vbCrLf &
		   "  · 没有工程图的零件会跳过并在报告里列出" & vbCrLf &
		   "  · 纸张大小默认取工程图自身设置，可手动强制指定"
			Dim btnPrintHelp As System.Windows.Forms.Button = MakeStepHelpBtn("打印装配顺序工程图 - 使用说明", helpPrint, True)
			btnPrintHelp.Location = New System.Drawing.Point(pnlTitle.Width - Scaled(82), (pnlTitle.Height - Scaled(24)) \ 2)
			btnPrintHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
			pnlTitle.Controls.Add(btnPrintHelp)
			Dim pnl As New Panel()
			pnl.Dock = DockStyle.Fill : pnl.BackColor = C(245, 248, 252)
			frmPrint.Controls.Add(pnl) : frmPrint.Controls.Add(pnlTitle)
			Dim y As Integer = Scaled(12) : Dim lm As Integer = Scaled(16)
			' ── 打印机选择 ──
			Dim lblPrinter As New Label()
			lblPrinter.Text = "打印机：" : lblPrinter.Location = New System.Drawing.Point(lm, y + Scaled(4))
			lblPrinter.Size = New System.Drawing.Size(Scaled(70), Scaled(22)) : lblPrinter.Font = MakeFontBold(g_fontName, 9.5!)
			pnl.Controls.Add(lblPrinter)
			Dim cmbPrinter As New System.Windows.Forms.ComboBox()
			cmbPrinter.Location = New System.Drawing.Point(lm + Scaled(74), y) : cmbPrinter.Size = New System.Drawing.Size(Scaled(476), Scaled(26))
			cmbPrinter.Font = MakeFont(g_fontName, 9.5!) : cmbPrinter.DropDownStyle = ComboBoxStyle.DropDownList
			cmbPrinter.Items.Add("[导出PDF] 自动保存到工程图同目录")
			For Each printerName As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
				cmbPrinter.Items.Add(printerName)
			Next
			Dim defPrinter As New System.Drawing.Printing.PrinterSettings()
			cmbPrinter.SelectedIndex = 0
			pnl.Controls.Add(cmbPrinter) : y += Scaled(36)
			' ── 纸张 ──
			Dim lblPaper As New Label()
			lblPaper.Text = "纸张：" : lblPaper.Location = New System.Drawing.Point(lm, y + Scaled(4))
			lblPaper.Size = New System.Drawing.Size(Scaled(44), Scaled(22)) : pnl.Controls.Add(lblPaper)
			Dim cmbPaper As New System.Windows.Forms.ComboBox()
			cmbPaper.Location = New System.Drawing.Point(lm + Scaled(48), y) : cmbPaper.Size = New System.Drawing.Size(Scaled(150), Scaled(26))
			cmbPaper.Font = MakeFont(g_fontName, 9.5!) : cmbPaper.DropDownStyle = ComboBoxStyle.DropDownList
			cmbPaper.Items.Add("按图纸原始规格") : cmbPaper.Items.Add("全部输出A4") : cmbPaper.Items.Add("全部输出A3")
			cmbPaper.SelectedIndex = 0 : pnl.Controls.Add(cmbPaper) : y += Scaled(36)
			' ── 状态标签 + 进度条 ──
			Dim lblTitle As New Label()
			lblTitle.Text = "共找到 " & idwList.Count & " 份工程图，点击开始打印"
			lblTitle.Location = New System.Drawing.Point(lm, y) : lblTitle.Size = New System.Drawing.Size(Scaled(554), Scaled(22))
			lblTitle.Font = MakeFontBold(g_fontName, 9.5!) : pnl.Controls.Add(lblTitle) : y += Scaled(26)
			Dim lblCurrent As New Label()
			lblCurrent.Text = "等待开始..." : lblCurrent.Location = New System.Drawing.Point(lm, y)
			lblCurrent.Size = New System.Drawing.Size(Scaled(554), Scaled(20)) : lblCurrent.Font = MakeFont(g_fontName, 9.0!)
			lblCurrent.ForeColor = C(100, 100, 100) : pnl.Controls.Add(lblCurrent) : y += Scaled(24)
			Dim pgBar As New System.Windows.Forms.ProgressBar()
			pgBar.Location = New System.Drawing.Point(lm, y) : pgBar.Size = New System.Drawing.Size(Scaled(554), Scaled(20))
			pgBar.Minimum = 0 : pgBar.Maximum = idwList.Count : pgBar.Value = 0
			pgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous : pnl.Controls.Add(pgBar) : y += Scaled(24)
			Dim lblProgress As New Label()
			lblProgress.Text = "0 / " & idwList.Count : lblProgress.Location = New System.Drawing.Point(lm, y)
			lblProgress.Size = New System.Drawing.Size(Scaled(200), Scaled(20)) : lblProgress.Font = MakeFont(g_fontName, 9.0!)
			pnl.Controls.Add(lblProgress) : y += Scaled(28)
			' ── 打印说明 ──
			Dim chkNote As New CheckBox()
			chkNote.Text = "添加打印说明" : chkNote.Location = New System.Drawing.Point(lm, y)
			chkNote.Size = New System.Drawing.Size(Scaled(110), Scaled(22)) : chkNote.Font = MakeFont(g_fontName, 9.5!)
			pnl.Controls.Add(chkNote)
			Dim lblFontSize As New Label()
			lblFontSize.Text = "字号：" : lblFontSize.Location = New System.Drawing.Point(lm + Scaled(118), y + Scaled(3))
			lblFontSize.Size = New System.Drawing.Size(Scaled(36), Scaled(20)) : pnl.Controls.Add(lblFontSize)
			Dim cmbFontSize As New System.Windows.Forms.ComboBox()
			cmbFontSize.Location = New System.Drawing.Point(lm + Scaled(156), y) : cmbFontSize.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
			cmbFontSize.Font = MakeFont(g_fontName, 9.0!) : cmbFontSize.DropDownStyle = ComboBoxStyle.DropDownList
			For Each fs As Integer In New Integer() {6, 7, 8, 9, 10, 11, 12, 14, 16, 18} : cmbFontSize.Items.Add(fs) : Next
			cmbFontSize.SelectedItem = 8 : pnl.Controls.Add(cmbFontSize)
			Dim chkTime As New CheckBox()
			chkTime.Text = "打印时间" : chkTime.Location = New System.Drawing.Point(lm + Scaled(224), y)
			chkTime.Size = New System.Drawing.Size(Scaled(90), Scaled(22)) : chkTime.Font = MakeFont(g_fontName, 9.5!) : chkTime.Checked = False
			pnl.Controls.Add(chkTime) : y += Scaled(28)
			Dim txtNote As New System.Windows.Forms.TextBox()
			txtNote.Location = New System.Drawing.Point(lm, y) : txtNote.Size = New System.Drawing.Size(Scaled(554), Scaled(26))
			txtNote.Font = MakeFont(g_fontName, 9.5!) : SetPlaceholder(txtNote, "请输入打印说明（勾选后生效）") : txtNote.Enabled = False
			pnl.Controls.Add(txtNote) : y += Scaled(32)
			' ── 坐标 ──
			Dim lblPos As New Label()
			lblPos.Text = "打印说明文字位置（工程图坐标，单位cm）：" : lblPos.Location = New System.Drawing.Point(lm, y)
			lblPos.Size = New System.Drawing.Size(Scaled(400), Scaled(20)) : lblPos.Font = MakeFont(g_fontName, 8.0!) : lblPos.ForeColor = C(120, 120, 120)
			pnl.Controls.Add(lblPos) : y += Scaled(22)
			Dim lblX As New Label() : lblX.Text = "X：" : lblX.Location = New System.Drawing.Point(lm, y + Scaled(3))
			lblX.Size = New System.Drawing.Size(Scaled(22), Scaled(20)) : pnl.Controls.Add(lblX)
			Dim txtX As New System.Windows.Forms.TextBox()
			txtX.Text = "0.5" : txtX.Location = New System.Drawing.Point(lm + Scaled(24), y) : txtX.Size = New System.Drawing.Size(Scaled(55), Scaled(26))
			txtX.Font = MakeFont(g_fontName, 9.5!) : pnl.Controls.Add(txtX)
			Dim lblY As New Label() : lblY.Text = "Y：" : lblY.Location = New System.Drawing.Point(lm + Scaled(88), y + Scaled(3))
			lblY.Size = New System.Drawing.Size(Scaled(22), Scaled(20)) : pnl.Controls.Add(lblY)
			Dim txtY As New System.Windows.Forms.TextBox()
			txtY.Text = "0.5" : txtY.Location = New System.Drawing.Point(lm + Scaled(112), y) : txtY.Size = New System.Drawing.Size(Scaled(55), Scaled(26))
			txtY.Font = MakeFont(g_fontName, 9.5!) : pnl.Controls.Add(txtY)
			Dim lblPosHint As New Label()
			lblPosHint.Text = "图纸左下角坐标≈(0.0,0.0)" : lblPosHint.Location = New System.Drawing.Point(lm + Scaled(176), y + Scaled(4))
			lblPosHint.Size = New System.Drawing.Size(Scaled(300), Scaled(20)) : lblPosHint.Font = MakeFont(g_fontName, 7.5!) : lblPosHint.ForeColor = C(150, 150, 150)
			pnl.Controls.Add(lblPosHint) : y += Scaled(32)
			AddHandler chkNote.CheckedChanged, Sub(s5, e5)
												   txtNote.Enabled = chkNote.Checked
											   End Sub
			' ── 底部按钮 ──
			Dim btnList As New Button()
			btnList.Text = "查看打印列表" : btnList.Location = New System.Drawing.Point(lm, y)
			btnList.Size = New System.Drawing.Size(Scaled(120), Scaled(32)) : btnList.Font = MakeFont(g_fontName, 9.5!)
			btnList.FlatStyle = FlatStyle.Flat : pnl.Controls.Add(btnList)
			Dim btnHelp As New Button()
			btnHelp.Text = "使用说明" : btnHelp.Location = New System.Drawing.Point(lm + Scaled(128), y)
			btnHelp.Size = New System.Drawing.Size(Scaled(90), Scaled(32)) : btnHelp.Font = MakeFont(g_fontName, 9.5!)
			btnHelp.FlatStyle = FlatStyle.Flat : pnl.Controls.Add(btnHelp)
			Dim btnStart As New Button()
			Dim btnCancel As New Button()
			btnCancel.Text = "取消" : btnCancel.Location = New System.Drawing.Point(lm + Scaled(460), y)
			btnCancel.Size = New System.Drawing.Size(Scaled(80), Scaled(32)) : btnCancel.Font = MakeFont(g_fontName, 9.5!)
			btnCancel.FlatStyle = FlatStyle.Flat : pnl.Controls.Add(btnCancel)
			AddHandler btnList.Click, Sub(s3, e3)
										  Dim sb3 As New System.Text.StringBuilder()
										  sb3.AppendLine("打印顺序列表（共 " & idwList.Count & " 份）：")
										  sb3.AppendLine("")
										  For listIdx As Integer = 0 To idwList.Count - 1
											  sb3.AppendLine((listIdx + 1) & ". " & System.IO.Path.GetFileNameWithoutExtension(idwList(listIdx)))
										  Next
										  MessageBox.Show(sb3.ToString(), "打印顺序列表")
									  End Sub
			AddHandler btnHelp.Click, Sub(s4, e4)
										  Dim helpText As New System.Text.StringBuilder()
										  helpText.AppendLine("【打印装配顺序工程图 使用说明】")
										  helpText.AppendLine("")
										  helpText.AppendLine("【使用前提】")
										  helpText.AppendLine("  1. 必须在总装部件（.iam）中运行")
										  helpText.AppendLine("  2. 工程图文件（.idw）须与部件同名、同目录")
										  helpText.AppendLine("")
										  helpText.AppendLine("【打印顺序】")
										  helpText.AppendLine("  1. 总装工程图（最先打印）")
										  helpText.AppendLine("  2. 按装配树顺序依次打印子装配图纸")
										  helpText.AppendLine("  3. 最后打印各零件图纸")
										  helpText.AppendLine("  ※ 相同零件只打印一次")
										  helpText.AppendLine("")
										  helpText.AppendLine("【操作步骤】")
										  helpText.AppendLine("  1. 在总装部件中打开本工具")
										  helpText.AppendLine("  2. 点击【查看打印列表】确认打印顺序")
										  helpText.AppendLine("  3. 确认无误后点击【开始打印】")
										  helpText.AppendLine("  4. 进度条显示当前打印进度")
										  helpText.AppendLine("  5. 打印完成后点击【关闭】")
										  helpText.AppendLine("")
										  helpText.AppendLine("【注意事项】")
										  helpText.AppendLine("  · 打印时间戳会自动添加到图纸左下角")
										  helpText.AppendLine("  · 打印完成后时间戳自动删除")
										  helpText.AppendLine("  · 默认A4纸，自动判断横/纵向")
										  helpText.AppendLine("  · 全黑打印模式")
										  helpText.AppendLine("  · 可随时点击【取消】中断打印")
										  MessageBox.Show(helpText.ToString(), "使用说明")
									  End Sub
			btnStart.Text = "开始打印"
			btnStart.Location = New System.Drawing.Point(lm + Scaled(360), y)
			btnStart.Size = New System.Drawing.Size(Scaled(90), Scaled(32))
			btnStart.Font = MakeFont(g_fontName, 9.5!)
			btnStart.BackColor = OrangeMid()
			btnStart.ForeColor = System.Drawing.Color.White
			btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			pnl.Controls.Add(btnStart)
			Dim isCancelled As Boolean = False
			AddHandler btnCancel.Click, Sub(s2, e2)
											isCancelled = True
											frmPrint.Close()
										End Sub
			AddHandler btnStart.Click, Sub(s2, e2)
										   btnStart.Enabled = False
										   lblTitle.Text = "正在打印，请勿关闭窗口..."
										   ' PDF模式：第一次弹出目录选择，后续全部保存到该目录'
										   Dim pdfSaveDir As String = ""
										   If CStr(cmbPrinter.SelectedItem).StartsWith("[导出PDF]") Then
											   Dim defaultPdfDir As String = System.IO.Path.GetDirectoryName(oDoc.FullFileName)
											   Dim inputDir As String = ShowFolderInputDialog("选择PDF保存目录", defaultPdfDir)
											   If inputDir Is Nothing Then btnStart.Enabled = True : Return
											   If String.IsNullOrEmpty(inputDir) Then inputDir = defaultPdfDir
											   If Not System.IO.Directory.Exists(inputDir) Then
												   MessageBox.Show("目录不存在：" & inputDir, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
												   btnStart.Enabled = True : Return
											   End If
											   pdfSaveDir = inputDir
										   End If
										   For printIdx As Integer = 0 To idwList.Count - 1
											   If isCancelled Then Exit For
											   Dim idwPath = idwList(printIdx)
											   lblCurrent.Text = "正在打印: " & System.IO.Path.GetFileName(idwPath)
											   lblProgress.Text = (printIdx + 1) & " / " & idwList.Count
											   frmPrint.Refresh()
											   Try
												   Dim px As Double = 0.5 : Dim py As Double = 0.5
												   Try : px = CDbl(txtX.Text) : Catch : End Try
												   Try : py = CDbl(txtY.Text) : Catch : End Try
												   PrintOneDrawing(idwPath, If(chkNote.Checked, txtNote.Text, ""), chkTime.Checked, px, py, If(cmbPrinter.SelectedItem IsNot Nothing, CStr(cmbPrinter.SelectedItem), ""), If(cmbFontSize.SelectedItem IsNot Nothing, CDbl(cmbFontSize.SelectedItem), 8.0), pdfSaveDir, If(cmbPaper.SelectedItem IsNot Nothing, CStr(cmbPaper.SelectedItem), "按图纸原始规格"))
											   Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
											   End Try
											   pgBar.Value = printIdx + 1
											   frmPrint.Refresh()
										   Next
										   If Not isCancelled Then
											   lblTitle.Text = "完成！共 " & idwList.Count & " 份"
											   If CStr(cmbPrinter.SelectedItem).StartsWith("[导出PDF]") Then
												   Dim pdfDir As String = System.IO.Path.GetDirectoryName(idwList(0))
												   lblCurrent.Text = "PDF已保存到: " & pdfDir
											   Else
												   lblCurrent.Text = ""
											   End If
											   btnCancel.Text = "关闭"
										   End If
									   End Sub
			frmPrint.ShowDialog()
		Catch ex As Exception
			MessageBox.Show("打印失败: " & ex.Message, "DWJ")
		End Try
	End Sub

	' ══════════════════════════════════════════
	' 按属性导出PDF/DWG
	' ══════════════════════════════════════════
	' 按属性导出PDF/DWG

	' ══════════════════════════════════════════
	' 属性拼接写入
	Sub ShowPropConcatDialog()
		Try
			If g_rows Is Nothing OrElse g_rows.Count = 0 Then
				MessageBox.Show("BOM表为空，请先加载数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
			End If
			If g_dpiScale <= 0 Then InitScaledConstants()
			' ── 属性列表在Shown事件中加载 ──
			Dim propList As New System.Collections.Generic.List(Of String)()
			' ── 窗体 ──
			Dim frm As New Form()
			frm.Text = "属性拼接写入"
			frm.Width = Scaled(940) : frm.Height = Scaled(480)
			frm.StartPosition = FormStartPosition.CenterScreen
			frm.FormBorderStyle = FormBorderStyle.FixedDialog
			frm.MaximizeBox = False : frm.MinimizeBox = False
			frm.Font = MakeFont(g_fontName, 9.5!) : frm.BackColor = C(245, 248, 252)
			' ── 标题栏 ──
			Dim pnlTitle As New Panel()
			pnlTitle.Dock = DockStyle.Top : pnlTitle.Height = Scaled(40) : pnlTitle.BackColor = OrangeMid()
			Dim lblT As New Label()
			lblT.Text = "  🔗 属性拼接写入" : lblT.Dock = DockStyle.Fill
			lblT.ForeColor = System.Drawing.Color.White : lblT.Font = MakeFontBold(g_fontName, 11.0!)
			lblT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnlTitle.Controls.Add(lblT)
			Dim helpConcat As String = "【功能说明】" & vbCrLf &
		   "  把多个属性字段按顺序和分隔符拼成一段文字，写入一个目标属性。" & vbCrLf &
		   "  例如：图号 + 「-」 + 名称 + 「(」 + 材料 + 「)」。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 在左下方「可用字段」双击要拼接的属性，添加到右侧「拼接顺序」" & vbCrLf &
		   "  2. 用上下箭头调整字段先后顺序" & vbCrLf &
		   "  3. 在「分隔符」里输入字段之间的符号（如 - _ / 空格，可留空）" & vbCrLf &
		   "  4. 在「目标属性」里输入或选择写入到哪个属性" & vbCrLf &
		   "  5. 勾选「是否覆盖已有值」" & vbCrLf &
		   "  6. 点「开始拼接写入」" & vbCrLf & vbCrLf &
		   "【提示】" & vbCrLf &
		   "  · 字段顺序决定拼接结果，注意调整" & vbCrLf &
		   "  · 某个零件缺少某字段时该字段按空处理，不会报错"
			Dim btnConcatHelp As System.Windows.Forms.Button = MakeStepHelpBtn("属性拼接写入 - 使用说明", helpConcat, True)
			btnConcatHelp.Location = New System.Drawing.Point(pnlTitle.Width - Scaled(82), (pnlTitle.Height - Scaled(24)) \ 2)
			btnConcatHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
			pnlTitle.Controls.Add(btnConcatHelp)
			Dim pnl As New Panel()
			pnl.Dock = DockStyle.Fill : pnl.BackColor = C(245, 248, 252)
			frm.Controls.Add(pnl) : frm.Controls.Add(pnlTitle)
			Dim lm As Integer = Scaled(16) : Dim y As Integer = Scaled(12)
			' ── 左侧属性列表 ──
			' ── 左侧列表标题：可用属性（双击添加）：──'
			Dim lblProps1 As New Label()
			lblProps1.Text = "可用属性（"
			lblProps1.AutoSize = True
			lblProps1.Location = New System.Drawing.Point(lm, y + Scaled(1))
			lblProps1.Font = MakeFontBold(g_fontName, 9.0!)
			lblProps1.ForeColor = System.Drawing.Color.Black
			pnl.Controls.Add(lblProps1)
			Dim lblProps2 As New Label()
			lblProps2.Text = "双击添加"
			lblProps2.AutoSize = True
			lblProps2.Location = New System.Drawing.Point(lm + lblProps1.PreferredWidth, y + Scaled(1))
			lblProps2.Font = MakeFontBold(g_fontName, 9.0!)
			lblProps2.ForeColor = System.Drawing.Color.FromArgb(210, 100, 0)
			pnl.Controls.Add(lblProps2)
			Dim lblProps3 As New Label()
			lblProps3.Text = "）："
			lblProps3.AutoSize = True
			lblProps3.Location = New System.Drawing.Point(lm + lblProps1.PreferredWidth + lblProps2.PreferredWidth, y + Scaled(1))
			lblProps3.Font = MakeFontBold(g_fontName, 9.0!)
			lblProps3.ForeColor = System.Drawing.Color.Black
			pnl.Controls.Add(lblProps3)
			' 双击添加 闪烁 Timer（橙色↔绿色）'
			Dim blinkTimer As New System.Windows.Forms.Timer()
			blinkTimer.Interval = 600
			Dim blinkState As Boolean = False
			AddHandler blinkTimer.Tick, Sub(bt, be)
											blinkState = Not blinkState
											lblProps2.ForeColor = If(blinkState, System.Drawing.Color.FromArgb(0, 160, 60), System.Drawing.Color.FromArgb(210, 100, 0))
										End Sub
			blinkTimer.Start()
			AddHandler frm.FormClosed, Sub(fc, fe)
										   blinkTimer.Stop() : blinkTimer.Dispose()
									   End Sub
			Dim lstProps As New System.Windows.Forms.ListBox()
			lstProps.Location = New System.Drawing.Point(lm, y + Scaled(22))
			lstProps.Size = New System.Drawing.Size(Scaled(160), Scaled(340))
			lstProps.Font = MakeFont(g_fontName, 9.0!)
			lstProps.BorderStyle = BorderStyle.FixedSingle
			lstProps.BackColor = System.Drawing.Color.White
			' 属性列表在Shown事件填充'
			pnl.Controls.Add(lstProps)
			' ── 右侧区域参数 ──
			Dim rx As Integer = lm + Scaled(196)
			Dim rw As Integer = Scaled(680)
			Dim longW As Integer = Scaled(110)
			Dim shortW As Integer = Scaled(54)
			Dim gap As Integer = Scaled(4)
			Dim ry As Integer = y + Scaled(22)
			Dim ry2 As Integer = ry + Scaled(62)
			' 第一行：索引0~3（4个长框+3个间隔+拐弯箭头）'
			' 第二行：索引4~6（3个长框+3个间隔）'
			Dim txtFields(7) As System.Windows.Forms.TextBox
			Dim cmbSeps(7) As System.Windows.Forms.ComboBox
			Dim sepOptions() As String = {"空白", "空格", "·", "-", "_", "/", "|", "+", ",", "；"}
			' ── 第一行：0,1,2,3 ──
			Dim curX As Integer = rx
			For i As Integer = 0 To 3
				Dim txt As New System.Windows.Forms.TextBox()
				txt.Location = New System.Drawing.Point(curX, ry)
				txt.Size = New System.Drawing.Size(longW, Scaled(26))
				txt.Font = MakeFont(g_fontName, 9.0!) : txt.BackColor = C(255, 255, 255)
				txt.ReadOnly = True : txt.Tag = i : pnl.Controls.Add(txt) : txtFields(i) = txt
				curX += longW + gap
				If i < 4 Then
					Dim cmb As New System.Windows.Forms.ComboBox()
					cmb.Location = New System.Drawing.Point(curX, ry)
					cmb.Size = New System.Drawing.Size(shortW, Scaled(26))
					cmb.Font = MakeFont(g_fontName, 8.5!) : cmb.DropDownStyle = ComboBoxStyle.DropDownList
					For Each sep As String In sepOptions : cmb.Items.Add(sep) : Next
					cmb.SelectedIndex = 0 : pnl.Controls.Add(cmb) : cmbSeps(i) = cmb
					curX += shortW + gap
				End If
			Next
			' ── 第一行末输入框右边中点 → S形起点 短横线 ──
			Dim lineRow1 As New Panel()
			lineRow1.BackColor = OrangeMid()
			' 第一行末输入框右边X = curX - gap，中点Y = ry + Scaled(8)'
			lineRow1.Location = New System.Drawing.Point(curX - gap, ry + Scaled(8))
			lineRow1.Size = New System.Drawing.Size(Scaled(10) + gap, Scaled(2))
			pnl.Controls.Add(lineRow1)
			' ── S形连接：第一行尾中点 → 第二行头中点 ──
			Dim tailX As Integer = curX + Scaled(10)
			Dim headX As Integer = rx - Scaled(30)
			Dim midY As Integer = ry + Scaled(13) + (ry2 - ry) \ 2
			' 竖线1：第一行尾中点向下到中间'
			Dim lineV1 As New Panel()
			lineV1.BackColor = OrangeMid()
			lineV1.Location = New System.Drawing.Point(tailX, ry + Scaled(8))
			lineV1.Size = New System.Drawing.Size(Scaled(2), midY - (ry + Scaled(8)))
			pnl.Controls.Add(lineV1)
			' 横线：从第一行尾到第二行头（中间高度）'
			Dim lineH As New Panel()
			lineH.BackColor = OrangeMid()
			lineH.Location = New System.Drawing.Point(headX, midY)
			lineH.Size = New System.Drawing.Size(tailX - headX + Scaled(2), Scaled(2))
			pnl.Controls.Add(lineH)
			' 竖线2：中间向下到第二行头中点'
			Dim lineV2 As New Panel()
			lineV2.BackColor = OrangeMid()
			lineV2.Location = New System.Drawing.Point(headX, midY)
			lineV2.Size = New System.Drawing.Size(Scaled(2), ry2 + Scaled(8) - midY)
			pnl.Controls.Add(lineV2)
			' ── 第二行首输入框左边 → S形终点 短横线 ──
			Dim lineRow2 As New Panel()
			lineRow2.BackColor = OrangeMid()
			lineRow2.Location = New System.Drawing.Point(headX, ry2 + Scaled(8))
			lineRow2.Size = New System.Drawing.Size(rx - Scaled(12) - headX, Scaled(2))
			pnl.Controls.Add(lineRow2)
			' ── 右箭头（第二行末端）──
			Dim lblArrowR As New Label()
			lblArrowR.Text = "▶"
			lblArrowR.Location = New System.Drawing.Point(rx - Scaled(12), ry2 + Scaled(1))
			lblArrowR.Size = New System.Drawing.Size(Scaled(12), Scaled(14))
			lblArrowR.Font = MakeFontBold(g_fontName, 9.0!) : lblArrowR.ForeColor = C(0, 120, 215)
			lblArrowR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			pnl.Controls.Add(lblArrowR)
			' ── 第二行：4,5,6 ──
			curX = rx
			For i As Integer = 4 To 7
				Dim txt2 As New System.Windows.Forms.TextBox()
				txt2.Location = New System.Drawing.Point(curX, ry2)
				txt2.Size = New System.Drawing.Size(longW, Scaled(26))
				txt2.Font = MakeFont(g_fontName, 9.0!) : txt2.BackColor = C(255, 255, 255)
				txt2.ReadOnly = True : txt2.Tag = i : pnl.Controls.Add(txt2) : txtFields(i) = txt2
				curX += longW + gap
				Dim cmb2 As New System.Windows.Forms.ComboBox()
				cmb2.Location = New System.Drawing.Point(curX, ry2)
				cmb2.Size = New System.Drawing.Size(shortW, Scaled(26))
				cmb2.Font = MakeFont(g_fontName, 8.5!) : cmb2.DropDownStyle = ComboBoxStyle.DropDownList
				For Each sep As String In sepOptions : cmb2.Items.Add(sep) : Next
				cmb2.SelectedIndex = 0 : pnl.Controls.Add(cmb2) : cmbSeps(i) = cmb2
				curX += shortW + gap
			Next
			' ── 说明标签 ──
			Dim lblHint As New Label()
			lblHint.Text = "双击左侧属性名填入输入框；窄下拉选择间隔符；（无）=不插入间隔"
			lblHint.Location = New System.Drawing.Point(rx, ry2 + Scaled(32))
			lblHint.Size = New System.Drawing.Size(rw, Scaled(20))
			lblHint.Font = MakeFont(g_fontName, 8.0!) : lblHint.ForeColor = C(120, 120, 120)
			pnl.Controls.Add(lblHint)
			' ── 底部：写入目标属性 + 执行按钮 ──
			Dim by2 As Integer = ry2 + Scaled(62)
			Dim lblDst As New Label()
			lblDst.Text = "写入属性："
			lblDst.Location = New System.Drawing.Point(rx, by2 + Scaled(4))
			lblDst.Size = New System.Drawing.Size(Scaled(70), Scaled(22))
			lblDst.Font = MakeFontBold(g_fontName, 9.5!) : pnl.Controls.Add(lblDst)
			Dim txtDst As New System.Windows.Forms.TextBox()
			txtDst.Location = New System.Drawing.Point(rx + Scaled(74), by2)
			txtDst.Size = New System.Drawing.Size(Scaled(200), Scaled(26))
			txtDst.Font = MakeFont(g_fontName, 9.5!)
			txtDst.BackColor = System.Drawing.Color.White
			txtDst.BorderStyle = BorderStyle.FixedSingle
			pnl.Controls.Add(txtDst)
			Dim focusedTxtIdx As Integer = 0
			AddHandler lstProps.DoubleClick, Sub(sd, ed)
												 If lstProps.SelectedItem Is Nothing Then Return
												 Dim propName As String = CStr(lstProps.SelectedItem).Trim()
												 If propName.StartsWith("──") Then Return
												 If propName.StartsWith("[系统] ") Then propName = propName.Substring(5).Trim()
												 If propName.StartsWith("[自定义] ") Then propName = propName.Substring(6).Trim()
												 If focusedTxtIdx = -1 Then
													 txtDst.Text = propName : txtDst.BackColor = System.Drawing.Color.White : focusedTxtIdx = 0
												 Else
													 txtFields(focusedTxtIdx).Text = propName
													 Dim nextEmpty As Integer = -1
													 For _ni As Integer = focusedTxtIdx + 1 To 7
														 If String.IsNullOrEmpty(txtFields(_ni).Text.Trim()) Then nextEmpty = _ni : Exit For
													 Next
													 If nextEmpty >= 0 Then
														 focusedTxtIdx = nextEmpty : txtFields(focusedTxtIdx).Focus()
													 Else
														 txtDst.Focus()
													 End If
												 End If
											 End Sub
			' 双击长输入框清空'
			For _di As Integer = 0 To 7
				Dim _capturedDi As Integer = _di
				AddHandler txtFields(_di).DoubleClick, Sub(sdc, edc)
														   txtFields(_capturedDi).Text = "" : focusedTxtIdx = _capturedDi
													   End Sub
				AddHandler txtFields(_di).Enter, Sub(sce, ece)
													 focusedTxtIdx = _capturedDi : txtDst.BackColor = System.Drawing.Color.White
												 End Sub
			Next
			' txtDst 点击/获得焦点：设为-1（写入属性模式）'
			AddHandler txtDst.Click, Sub(sc, ec)
										 focusedTxtIdx = -1
										 txtDst.BackColor = System.Drawing.Color.FromArgb(255, 255, 200)
									 End Sub
			AddHandler txtDst.Enter, Sub(se2, ee2)
										 focusedTxtIdx = -1
										 txtDst.BackColor = System.Drawing.Color.FromArgb(255, 255, 200)
									 End Sub
			AddHandler txtDst.DoubleClick, Sub(sdd, edd)
											   txtDst.Text = "" : txtDst.BackColor = System.Drawing.Color.White : focusedTxtIdx = 0
										   End Sub
			Dim btnExec As New Button()
			btnExec.Text = "☆ 执行拼接写入 ☆"
			btnExec.Location = New System.Drawing.Point(rx + Scaled(290), by2)
			btnExec.Size = New System.Drawing.Size(Scaled(160), Scaled(34))
			btnExec.BackColor = OrangeMid() : btnExec.ForeColor = System.Drawing.Color.White
			btnExec.FlatStyle = FlatStyle.Flat : btnExec.FlatAppearance.BorderSize = 0
			btnExec.Font = MakeFontBold(g_fontName, 9.5!) : pnl.Controls.Add(btnExec)
			Dim lblRes As New Label()
			lblRes.Text = ""
			Dim lblProgress As New Label()
			lblProgress.Text = ""
			lblProgress.Location = New System.Drawing.Point(rx, by2 + Scaled(40))
			lblProgress.Size = New System.Drawing.Size(Scaled(300), Scaled(20))
			lblProgress.Font = MakeFont(g_fontName, 8.5!)
			lblProgress.ForeColor = C(0, 100, 200)
			pnl.Controls.Add(lblProgress)
			lblRes.Location = New System.Drawing.Point(rx + Scaled(310), by2 + Scaled(40))
			lblRes.Size = New System.Drawing.Size(rw - Scaled(310), Scaled(22))
			lblRes.Font = MakeFont(g_fontName, 9.0!) : pnl.Controls.Add(lblRes)
			' ── 进度条（Panel自绘）──'
			Dim pbBg As New Panel()
			' 进度条标签'
			pbBg.Location = New System.Drawing.Point(rx, by2 + Scaled(64))
			pbBg.Size = New System.Drawing.Size(rw, Scaled(20))
			pbBg.BackColor = C(235, 235, 235) : pbBg.Visible = True
			pbBg.BorderStyle = BorderStyle.FixedSingle
			pnl.Controls.Add(pbBg)
			Dim pbFg As New Panel()
			pbFg.Location = New System.Drawing.Point(0, 0)
			pbFg.Size = New System.Drawing.Size(0, Scaled(20))
			pbFg.BackColor = OrangeMid()
			pbBg.Controls.Add(pbFg)
			' 进度条内部左上角标签'
			Dim lblPbTitle As New Label()
			lblPbTitle.Text = "进度条"
			lblPbTitle.AutoSize = True
			lblPbTitle.Location = New System.Drawing.Point(Scaled(4), Scaled(2))
			lblPbTitle.Font = MakeFont(g_fontName, 7.5!)
			lblPbTitle.ForeColor = System.Drawing.Color.Black
			lblPbTitle.BackColor = System.Drawing.Color.Transparent
			pbBg.Controls.Add(lblPbTitle)
			Dim pbTotal As Integer = 1 : Dim pbCurrent As Integer = 0
			' ── 使用说明面板 ──
			Dim pnlHelp As New Panel()
			pnlHelp.Location = New System.Drawing.Point(rx, by2 + Scaled(94))
			pnlHelp.Size = New System.Drawing.Size(rw, Scaled(122))
			pnlHelp.BackColor = C(245, 248, 252)
			pnlHelp.BorderStyle = BorderStyle.None
			pnl.Controls.Add(pnlHelp)
			Dim lblHelpTitle As New Label()
			lblHelpTitle.Text = "  使用说明"
			lblHelpTitle.Location = New System.Drawing.Point(Scaled(6), Scaled(4))
			lblHelpTitle.Size = New System.Drawing.Size(rw - Scaled(12), Scaled(20))
			lblHelpTitle.Font = MakeFontBold(g_fontName, 9.5!)
			lblHelpTitle.ForeColor = C(0, 80, 160)
			pnlHelp.Controls.Add(lblHelpTitle)
			Dim helpLines() As String = {
	"  1. 左侧列表双击属性名 → 依次填入上方输入框（最多8个）",
	"  3. 窄下拉框选分隔符：空白=不插入  空格=插入空格  其余直接插入",
	"  4. 双击输入框可清空内容；连续点击执行可叠加写入",
	"  5. 点击 ☆ 执行拼接写入 ☆ → 批量写入所有零部件目标属性"
	}
			Dim helpY As Integer = Scaled(26)
			For Each helpLine As String In helpLines
				Dim lblLine As New Label()
				lblLine.Text = helpLine
				lblLine.Location = New System.Drawing.Point(Scaled(4), helpY)
				lblLine.Size = New System.Drawing.Size(rw - Scaled(12), Scaled(16))
				lblLine.Font = MakeFont(g_fontName, 8.5!)
				lblLine.ForeColor = C(50, 50, 80)
				pnlHelp.Controls.Add(lblLine)
				helpY += Scaled(16)
			Next
			' ── 执行逻辑 ──
			AddHandler btnExec.Click, Sub(se, ee)
										  Dim dstProp As String = txtDst.Text.Trim()
										  If String.IsNullOrEmpty(dstProp) Then
											  MessageBox.Show("请先选择写入属性！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
										  End If
										  Dim hasAny As Boolean = False
										  For Each tf As System.Windows.Forms.TextBox In txtFields
											  If Not String.IsNullOrEmpty(tf.Text.Trim()) Then hasAny = True : Exit For
										  Next
										  If Not hasAny Then
											  MessageBox.Show("请至少填入一个属性名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
										  End If
										  Dim okCnt As Integer = 0 : Dim failCnt As Integer = 0
										  Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim openedDocs As New System.Collections.Generic.List(Of Document)()
										  ' 统计总数，初始化进度条'
										  Dim totalCnt As Integer = 0
										  For Each rrC As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rrC.FilePath) OrElse rrC.IsMissing Then Continue For
											  If Not seen.Contains(rrC.FilePath) Then totalCnt += 1
										  Next
										  seen.Clear()
										  pbTotal = If(totalCnt > 0, totalCnt, 1) : pbCurrent = 0
										  pbFg.Width = 0 : pbBg.Refresh()
										  lblRes.Text = "正在写入..." : lblRes.ForeColor = C(0, 100, 200)
										  lblProgress.Text = "准备中..."
										  btnExec.Enabled = False
										  Try
											  For Each rr As BomDataRow In g_rows
												  If String.IsNullOrEmpty(rr.FilePath) OrElse rr.IsMissing Then Continue For
												  If seen.Contains(rr.FilePath) Then Continue For
												  seen.Add(rr.FilePath)
												  ' ── 打开文件，读属性拼接，写入，关闭（只打开一次）──'
												  Try
													  Dim rwDoc As Document = Nothing
													  Dim rwWasOpen As Boolean = False
													  For Each od As Document In ThisApplication.Documents
														  If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then rwDoc = od : rwWasOpen = True : Exit For
													  Next
													  If rwDoc Is Nothing AndAlso System.IO.File.Exists(rr.FilePath) Then
														  rwDoc = ThisApplication.Documents.Open(rr.FilePath, False)
													  End If
													  If rwDoc IsNot Nothing Then
														  ' ── 读取所有属性到字典 ──'
														  Dim fileProps As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
														  For Each psR As PropertySet In rwDoc.PropertySets
															  Try
																  For Each propR As Inventor.Property In psR
																	  Try
																		  Dim pnR As String = propR.DisplayName.Trim()
																		  If Not String.IsNullOrEmpty(pnR) AndAlso Not fileProps.ContainsKey(pnR) Then fileProps(pnR) = CStr(propR.Value)
																		  Dim nmR As String = propR.Name.Trim()
																		  If Not String.IsNullOrEmpty(nmR) AndAlso Not fileProps.ContainsKey(nmR) Then fileProps(nmR) = CStr(propR.Value)
																	  Catch : End Try
																  Next
															  Catch : End Try
														  Next
														  ' ── 中英文属性名互映，确保中文名也能查到 ──'
														  Dim cnNames() As String = {"零件代号", "描述", "材料", "类别", "修订版本", "设计者", "工程师", "成本", "标准", "供应商", "制造商", "项目", "状态", "备注"}
														  Dim enNames() As String = {"Part Number", "Description", "Material", "Category", "Revision", "Designer", "Engineer", "Cost", "Standard", "Vendor", "Manufacturer", "Project", "Status", "Comments"}
														  For _mi As Integer = 0 To cnNames.Length - 1
															  Dim cn As String = cnNames(_mi) : Dim en As String = enNames(_mi)
															  If fileProps.ContainsKey(en) AndAlso Not fileProps.ContainsKey(cn) Then fileProps(cn) = fileProps(en)
															  If fileProps.ContainsKey(cn) AndAlso Not fileProps.ContainsKey(en) Then fileProps(en) = fileProps(cn)
														  Next
														  ' ── 拼接 ──'
														  Dim sb As New System.Text.StringBuilder()
														  Dim prevFilledIdx As Integer = -1
														  For _i As Integer = 0 To 7
															  Dim _pn As String = txtFields(_i).Text.Trim()
															  If String.IsNullOrEmpty(_pn) Then Continue For
															  Dim _pv As String = If(fileProps.ContainsKey(_pn), fileProps(_pn), "")
															  If prevFilledIdx >= 0 Then
																  For _si As Integer = prevFilledIdx To _i - 1
																	  If _si < cmbSeps.Length AndAlso cmbSeps(_si) IsNot Nothing Then
																		  Dim sv As String = CStr(cmbSeps(_si).SelectedItem)
																		  If sv = "空格" Then sv = " "
																		  If sv <> "空白" Then sb.Append(sv)
																	  End If
																  Next
															  End If
															  sb.Append(_pv)
															  prevFilledIdx = _i
														  Next
														  Dim res As String = sb.ToString()
														  ' ── 写入目标属性 ──'
														  Dim written As Boolean = False
														  For Each gw As String In New String() {"{32853F0F-3444-11D1-9E93-0060B03C1CA6}", "{F29F85E0-4FF9-1068-AB91-08002B27B3D9}", "{D5CDD502-2E9C-101B-9397-08002B2CF9AE}"}
															  Try
																  Dim psW As PropertySet = rwDoc.PropertySets.Item(gw)
																  For Each propW As Inventor.Property In psW
																	  Try
																		  If String.Compare(propW.DisplayName, dstProp, True) = 0 OrElse String.Compare(propW.Name, dstProp, True) = 0 Then
																			  propW.Value = res : written = True : Exit For
																		  End If
																	  Catch : End Try
																  Next
																  If written Then Exit For
															  Catch : End Try
														  Next
														  If Not written Then
															  Dim udp As PropertySet = GetUDP(rwDoc)
															  If udp IsNot Nothing Then SafeWriteUDP(udp, dstProp, res) : written = True
														  End If
														  If written Then
															  rwDoc.Save()
															  ' 同步更新内存'
															  Select Case dstProp.ToLower()
																  Case "零件代号", "part number" : rr.PartNumber = res
																  Case "描述", "description" : rr.Description = res
																  Case "材料", "material" : rr.Material = res
																  Case "类别", "category" : rr.Category = res
																  Case "图号" : rr.DrawingNo = res
																  Case Else : rr.Custom(dstProp) = res
															  End Select
															  okCnt += 1
															  pbCurrent += 1 : pbFg.Width = CInt(pbBg.Width * pbCurrent / pbTotal) : pbBg.Refresh()
															  lblProgress.Text = "正在写入：" & pbCurrent & " / " & pbTotal & " 个"
														  Else
															  failCnt += 1
														  End If
														  If Not rwWasOpen Then openedDocs.Add(rwDoc)
													  End If
												  Catch : failCnt += 1 : End Try
											  Next
											  lblProgress.Text = ""
											  lblRes.Text = "完成：成功 " & okCnt & " 个，失败 " & failCnt & " 个"
											  lblRes.ForeColor = If(failCnt = 0, C(0, 130, 55), C(180, 60, 0))
											  pbFg.Width = pbBg.Width : pbBg.Refresh()
											  btnExec.Enabled = True
											  ' 统一关闭本次打开的文件'
											  For Each docToClose As Document In openedDocs
												  Try : docToClose.Close(True) : Catch : End Try
											  Next
											  If okCnt > 0 Then DoFullRefresh()
										  Catch exO As Exception
											  MessageBox.Show("执行失败：" & exO.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
											  btnExec.Enabled = True
										  End Try
									  End Sub
			' ── Shown事件：扫描所有零件自定义属性+系统属性 ──
			AddHandler frm.Shown, Sub(ss, es)
									  Try
										  lstProps.Items.Clear()
										  ' ── 直接从 g_rows 内存读取，完全不打开文件 ──'
										  Dim sysPropSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim custPropSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  ' 固定系统属性名（BomDataRow内置字段）'
										  For Each sp As String In New String() {
			"零件代号", "Part Number", "描述", "Description", "材料", "Material",
			"类别", "Category", "图号", "修订版本", "设计者", "工程师",
			"成本", "标准", "供应商", "制造商", "项目", "状态", "备注",
			"质量", "密度", "体积", "面积", "重量", "库存编号", "注释"}
											  sysPropSet.Add(sp)
										  Next
										  ' 从 g_rows.Custom 字典收集用户自定义属性（已在内存，无需打开文件）'
										  Dim seenFp As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rr As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rr.FilePath) OrElse rr.IsMissing Then Continue For
											  If seenFp.Contains(rr.FilePath) Then Continue For
											  seenFp.Add(rr.FilePath)
											  For Each kv As System.Collections.Generic.KeyValuePair(Of String, String) In rr.Custom
												  If Not String.IsNullOrEmpty(kv.Key) AndAlso Not sysPropSet.Contains(kv.Key) Then
													  custPropSet.Add(kv.Key)
												  End If
											  Next
										  Next
										  ' ── 填充列表 ──'
										  If sysPropSet.Count > 0 Then
											  lstProps.Items.Add("── 系统属性 ──")
											  Dim sysList As New System.Collections.Generic.List(Of String)(sysPropSet) : sysList.Sort()
											  For Each p As String In sysList : lstProps.Items.Add("[系统] " & p) : Next
										  End If
										  If custPropSet.Count > 0 Then
											  lstProps.Items.Add("── 自定义属性 ──")
											  Dim custList As New System.Collections.Generic.List(Of String)(custPropSet) : custList.Sort()
											  For Each p As String In custList : lstProps.Items.Add("[自定义] " & p) : Next
										  Else
											  lstProps.Items.Add("（未找到用户自定义属性）")
										  End If
									  Catch : End Try
								  End Sub
			' ── 从 ini 恢复上次输入框内容 ──'
			Try
				Dim iniPath2 As String = GetSettingsPath()
				Dim sec As Dictionary(Of String, String) = ReadSection(iniPath2, "PropConcat")
				If sec.Count > 0 Then
					For _ri As Integer = 0 To 7
						Dim key As String = "Field" & _ri
						If sec.ContainsKey(key) AndAlso Not String.IsNullOrEmpty(sec(key)) Then txtFields(_ri).Text = sec(key)
					Next
					If sec.ContainsKey("DstProp") AndAlso Not String.IsNullOrEmpty(sec("DstProp")) Then txtDst.Text = sec("DstProp")
					For _ci As Integer = 0 To 6
						Dim sepKey As String = "Sep" & _ci
						If sec.ContainsKey(sepKey) AndAlso Not String.IsNullOrEmpty(sec(sepKey)) Then
							If cmbSeps(_ci) IsNot Nothing Then
								Dim idx As Integer = cmbSeps(_ci).Items.IndexOf(sec(sepKey))
								If idx >= 0 Then cmbSeps(_ci).SelectedIndex = idx
							End If
						End If
					Next
				End If
			Catch : End Try
			frm.ShowDialog()
			' ── ShowDialog 返回后保存到 ini ──'
			Try
				Dim iniPathS As String = GetSettingsPath()
				Dim iniLinesS As New System.Collections.Generic.List(Of String)()
				If System.IO.File.Exists(iniPathS) Then
					Dim allLS() As String = System.IO.File.ReadAllLines(iniPathS, System.Text.Encoding.UTF8)
					Dim inSecS As Boolean = False
					For Each lnS As String In allLS
						If lnS.Trim().ToLower() = "[propconcat]" Then inSecS = True : Continue For
						If lnS.Trim().StartsWith("[") AndAlso lnS.Trim().ToLower() <> "[propconcat]" Then inSecS = False
						If Not inSecS Then iniLinesS.Add(lnS)
					Next
				End If
				iniLinesS.Add("")
				iniLinesS.Add("[PropConcat]")
				For _si As Integer = 0 To 7
					iniLinesS.Add("Field" & _si & "=" & txtFields(_si).Text.Trim())
				Next
				For _ci As Integer = 0 To 6
					Dim svS As String = ""
					If cmbSeps(_ci) IsNot Nothing Then svS = CStr(cmbSeps(_ci).SelectedItem)
					iniLinesS.Add("Sep" & _ci & "=" & svS)
				Next
				iniLinesS.Add("DstProp=" & txtDst.Text.Trim())
				System.IO.File.WriteAllText(iniPathS, String.Join(vbCrLf, iniLinesS.ToArray()), System.Text.Encoding.UTF8)
			Catch : End Try
		Catch ex As Exception
			MessageBox.Show("属性拼接写入错误：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub ShowExportByPropDialog()
		Try
			If g_rows Is Nothing OrElse g_rows.Count = 0 Then
				MessageBox.Show("BOM表为空，请先加载数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Return
			End If
			' 确保DPI缩放已初始化'
			If g_dpiScale <= 0 Then InitScaledConstants()
			' ── 窗体 ──
			Dim frm As New Form()
			frm.Text = "根据属性导出PDF/DWG"
			Try
				Dim _invExe As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
				If g_appIcon IsNot Nothing Then frm.Icon = g_appIcon Else If System.IO.File.Exists(_invExe) Then frm.Icon = System.Drawing.Icon.ExtractAssociatedIcon(_invExe)
			Catch : End Try
			frm.Width = Scaled(560)
			frm.Height = Scaled(300)
			frm.StartPosition = FormStartPosition.CenterScreen
			frm.FormBorderStyle = FormBorderStyle.FixedDialog
			frm.MaximizeBox = False : frm.MinimizeBox = False
			frm.Font = MakeFont(g_fontName, 9.5!)
			frm.BackColor = C(245, 248, 252)
			' ── 标题栏 ──
			Dim pnlTitle As New Panel()
			pnlTitle.Dock = DockStyle.Top : pnlTitle.Height = Scaled(40)
			pnlTitle.BackColor = OrangeMid()
			Dim lblT As New Label()
			lblT.Text = "  根据属性导出PDF/DWG"
			lblT.Dock = DockStyle.Fill
			lblT.ForeColor = System.Drawing.Color.White
			lblT.Font = MakeFontBold(g_fontName, 11.0!)
			lblT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			pnlTitle.Controls.Add(lblT)
			Dim helpExport As String = "【功能说明】" & vbCrLf &
		   "  按某个零件属性的值筛选零件，把它们的工程图批量导出为 PDF 或 DWG。" & vbCrLf & vbCrLf &
		   "【操作步骤】" & vbCrLf &
		   "  1. 在「筛选属性」里选择用哪个属性筛选（如 项目名称、图号）" & vbCrLf &
		   "  2. 在右侧勾选要导出的属性值（可多选、可全选）" & vbCrLf &
		   "  3. 选择导出格式：PDF 或 DWG" & vbCrLf &
		   "  4. 点「浏览」选择导出到哪个文件夹" & vbCrLf &
		   "  5. 点「开始导出」，完成后弹窗提示成功/失败数量" & vbCrLf & vbCrLf &
		   "【常见问题】" & vbCrLf &
		   "  · 提示找不到工程图：该零件没有同名 .idw 文件" & vbCrLf &
		   "  · 导出的 DWG 用 AutoCAD 打开：Inventor 导出时已做兼容" & vbCrLf &
		   "  · 标准件默认跳过，可在选项里关闭"
			Dim btnExportHelp As System.Windows.Forms.Button = MakeStepHelpBtn("按属性导出PDF/DWG - 使用说明", helpExport, True)
			btnExportHelp.Location = New System.Drawing.Point(pnlTitle.Width - Scaled(82), (pnlTitle.Height - Scaled(24)) \ 2)
			btnExportHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
			pnlTitle.Controls.Add(btnExportHelp)
			' ── 主体面板（先Add Fill，再Add Top）──
			Dim pnl As New Panel()
			pnl.Dock = DockStyle.Fill
			pnl.BackColor = C(245, 248, 252)
			frm.Controls.Add(pnl)
			frm.Controls.Add(pnlTitle)
			Dim y As Integer = Scaled(12)
			Dim lm As Integer = Scaled(16)
			' ── 属性选择行 ──
			Dim lblProp As New Label()
			lblProp.Text = "根据属性："
			lblProp.Location = New System.Drawing.Point(lm, y + Scaled(4))
			lblProp.Size = New System.Drawing.Size(Scaled(70), Scaled(22))
			lblProp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			lblProp.Font = MakeFontBold(g_fontName, 9.5!)
			pnl.Controls.Add(lblProp)
			' 属性名下拉
			Dim cmbProp As New System.Windows.Forms.ComboBox()
			cmbProp.Location = New System.Drawing.Point(lm + Scaled(74), y)
			cmbProp.Size = New System.Drawing.Size(Scaled(160), Scaled(26))
			cmbProp.Font = MakeFont(g_fontName, 9.5!)
			cmbProp.DropDownStyle = ComboBoxStyle.DropDown
			cmbProp.BackColor = System.Drawing.Color.White
			pnl.Controls.Add(cmbProp)
			' cmbProp在Shown事件里填充（扫描所有零件属性）'
			' 属性值输入框
			Dim txtVal As New System.Windows.Forms.TextBox()
			txtVal.Location = New System.Drawing.Point(lm + Scaled(242), y)
			txtVal.Size = New System.Drawing.Size(Scaled(160), Scaled(26))
			txtVal.Font = MakeFont(g_fontName, 9.5!)
			txtVal.BorderStyle = BorderStyle.FixedSingle
			txtVal.BackColor = System.Drawing.Color.White
			txtVal.Text = "点击▼选择，或手动输入（逗号分隔多值）"
			txtVal.ForeColor = System.Drawing.Color.Gray
			AddHandler txtVal.Enter, Sub(stv, etv)
										 Dim ttv As System.Windows.Forms.TextBox = CType(stv, System.Windows.Forms.TextBox)
										 If ttv.Text = "点击▼选择，或手动输入（逗号分隔多值）" Then ttv.Text = "" : ttv.ForeColor = System.Drawing.Color.Black
									 End Sub
			AddHandler txtVal.Leave, Sub(stv2, etv2)
										 Dim ttv2 As System.Windows.Forms.TextBox = CType(stv2, System.Windows.Forms.TextBox)
										 If String.IsNullOrEmpty(ttv2.Text.Trim()) Then ttv2.ForeColor = System.Drawing.Color.Gray : ttv2.Text = "点击▼选择，或手动输入（逗号分隔多值）"
									 End Sub
			pnl.Controls.Add(txtVal)
			' ▼按钮
			Dim btnDrop As New Button()
			btnDrop.Text = "▼"
			btnDrop.Location = New System.Drawing.Point(lm + Scaled(404), y)
			btnDrop.Size = New System.Drawing.Size(Scaled(24), Scaled(26))
			btnDrop.FlatStyle = FlatStyle.Flat
			btnDrop.FlatAppearance.BorderSize = 1
			btnDrop.Font = MakeFont(g_fontName, 7.0!)
			btnDrop.BackColor = C(230, 230, 230)
			pnl.Controls.Add(btnDrop)
			' ── 下拉勾选 ──
			Dim valDrop As New ToolStripDropDown()
			valDrop.AutoClose = False
			Dim valClbHost As New ToolStripControlHost(New CheckedListBox())
			Dim valClb As CheckedListBox = CType(valClbHost.Control, CheckedListBox)
			valClb.CheckOnClick = True
			valClb.Font = MakeFont(g_fontName, 9.0!)
			valClb.Width = Scaled(200) : valClb.Height = Scaled(160)
			valClbHost.AutoSize = False
			valClbHost.Size = New System.Drawing.Size(Scaled(200), Scaled(160))
			valDrop.Items.Add(valClbHost)
			Dim valBtnHost As New ToolStripControlHost(New Button())
			Dim valBtnOK As Button = CType(valBtnHost.Control, Button)
			valBtnOK.Text = "确定" : valBtnOK.Width = Scaled(200) : valBtnOK.Height = Scaled(26)
			valBtnOK.FlatStyle = FlatStyle.Flat
			valBtnOK.BackColor = OrangeMid()
			valBtnOK.ForeColor = System.Drawing.Color.White
			valBtnHost.AutoSize = False
			valBtnHost.Size = New System.Drawing.Size(Scaled(200), Scaled(26))
			valDrop.Items.Add(valBtnHost)
			' 确定：拼接选中值到输入框
			AddHandler valBtnOK.Click, Sub(sb, se)
										   Dim sel As New List(Of String)()
										   For Each ci As Object In valClb.CheckedItems : sel.Add(CStr(ci)) : Next
										   If sel.Count = 0 Then
											   txtVal.Text = "" : txtVal.ForeColor = System.Drawing.Color.Gray
											   txtVal.Text = "（全部导出）"
										   Else
											   txtVal.Text = String.Join(",", sel.ToArray())
											   txtVal.ForeColor = System.Drawing.Color.Black
										   End If
										   valDrop.Close()
									   End Sub
			' ▼点击：填充属性值列表
			AddHandler btnDrop.Click, Sub(sd, se)
										  If valDrop.Visible Then valDrop.Close() : Return
										  Dim pnCur As String = cmbProp.Text.Trim()
										  valClb.Items.Clear()
										  If Not String.IsNullOrEmpty(pnCur) Then
											  Dim vals As List(Of String) = CollectPropValues(g_rows, pnCur)
											  Dim curVals() As String = txtVal.Text.Split(",".ToCharArray())
											  For Each v As String In vals
												  Dim chk As Boolean = Array.Exists(curVals, Function(x) x.Trim().ToLower() = v.ToLower())
												  valClb.Items.Add(v, chk)
											  Next
										  End If
										  valDrop.Show(btnDrop, New System.Drawing.Point(0, btnDrop.Height))
									  End Sub
			' 点输入框也触发下拉
			' 点输入框也触发下拉
			AddHandler txtVal.Click, Sub(st, se)
										 btnDrop.PerformClick()
									 End Sub
			' 属性切换时清空值
			AddHandler cmbProp.SelectedIndexChanged, Sub(sc, ec)
														 If cmbProp.SelectedIndex >= 0 Then
															 txtVal.Text = "" : txtVal.ForeColor = System.Drawing.Color.Black
														 End If
													 End Sub
			Dim y2 As Integer = y + Scaled(44)
			Dim lblFmt As New Label()
			lblFmt.Text = "导出格式："
			lblFmt.Location = New System.Drawing.Point(lm, y2 + Scaled(4))
			lblFmt.Size = New System.Drawing.Size(Scaled(70), Scaled(22))
			lblFmt.Font = MakeFontBold(g_fontName, 9.5!)
			pnl.Controls.Add(lblFmt)
			Dim chkPDF As New CheckBox()
			chkPDF.Text = "PDF"
			chkPDF.Checked = True
			chkPDF.Location = New System.Drawing.Point(lm + Scaled(74), y2)
			chkPDF.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
			chkPDF.Font = MakeFont(g_fontName, 9.5!)
			pnl.Controls.Add(chkPDF)
			Dim chkDWG As New CheckBox()
			chkDWG.Text = "DWG"
			chkDWG.Checked = False
			chkDWG.Location = New System.Drawing.Point(lm + Scaled(140), y2)
			chkDWG.Size = New System.Drawing.Size(Scaled(60), Scaled(26))
			chkDWG.Font = MakeFont(g_fontName, 9.5!)
			pnl.Controls.Add(chkDWG)
			Dim y3 As Integer = y2 + Scaled(40)
			Dim btnExport As New Button()
			btnExport.Text = "☆执行导出☆"
			btnExport.Location = New System.Drawing.Point(lm, y3)
			btnExport.Size = New System.Drawing.Size(Scaled(130), Scaled(34))
			btnExport.BackColor = OrangeMid()
			btnExport.ForeColor = System.Drawing.Color.White
			btnExport.FlatStyle = FlatStyle.Flat
			btnExport.FlatAppearance.BorderSize = 0
			btnExport.Font = MakeFontBold(g_fontName, 9.5!)
			pnl.Controls.Add(btnExport)
			frm.Height = Scaled(420)
			' ── 说明文字 ──
			Dim y4 As Integer = y3 + Scaled(44)
			Dim lblHint As New Label()
			lblHint.Text = "说明：选择属性和属性值，找到同目录同名工程图导出。" & vbCrLf & "        属性选【不限属性】或属性值留空=导出全部工程图；多值用逗号分隔。"
			lblHint.Location = New System.Drawing.Point(lm, y4)
			lblHint.Size = New System.Drawing.Size(Scaled(510), Scaled(36))
			lblHint.Font = MakeFont(g_fontName, 8.0!)
			lblHint.ForeColor = C(120, 120, 120)
			pnl.Controls.Add(lblHint)
			' ── 进度条 ──'
			Dim y5 As Integer = y4 + Scaled(40)
			Dim pgExport As New System.Windows.Forms.ProgressBar()
			pgExport.Location = New System.Drawing.Point(lm, y5)
			pgExport.Size = New System.Drawing.Size(Scaled(510), Scaled(18))
			pgExport.Minimum = 0 : pgExport.Maximum = 100 : pgExport.Value = 0
			pgExport.Style = ProgressBarStyle.Continuous
			pnl.Controls.Add(pgExport)
			Dim lblProgress As New Label()
			lblProgress.Text = ""
			lblProgress.Location = New System.Drawing.Point(lm, y5 + Scaled(20))
			lblProgress.Size = New System.Drawing.Size(Scaled(510), Scaled(18))
			lblProgress.Font = MakeFont(g_fontName, 8.0!)
			lblProgress.ForeColor = C(0, 120, 215)
			pnl.Controls.Add(lblProgress)
			AddHandler btnExport.Click, Sub(se, ee)
											' ── 验证 ──
											Dim selProp As String = cmbProp.Text.Trim()
											Dim selVal As String = txtVal.Text.Trim()
											If selVal = "点击▼选择，或手动输入（逗号分隔多值）" Then selVal = ""
											Dim exportAllProp As Boolean = String.IsNullOrEmpty(selProp) OrElse selProp = "（不限属性，全部导出）" OrElse selProp = "──────────────"
											If Not exportAllProp AndAlso String.IsNullOrEmpty(selProp) Then
												MessageBox.Show("请先选择筛选属性！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
											End If
											' selVal为空或全部导出标记则不过滤属性值'
											If Not chkPDF.Checked AndAlso Not chkDWG.Checked Then
												MessageBox.Show("请至少勾选一种导出格式（PDF或DWG）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
											End If
											' ── 收集匹配的工程图 ──
											Dim matchedDwgs As New List(Of String)()
											Dim seenFpX As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
											Dim exportAll As Boolean = exportAllProp OrElse String.IsNullOrEmpty(selVal) OrElse selVal = "（全部导出）"
											Dim selVals() As String = If(exportAll, New String() {}, selVal.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
											For Each rrX As BomDataRow In g_rows
												If rrX.IsMissing OrElse String.IsNullOrEmpty(rrX.FilePath) Then Continue For
												If seenFpX.Contains(rrX.FilePath) Then Continue For
												seenFpX.Add(rrX.FilePath)
												If Not exportAll AndAlso Not exportAllProp Then
													Dim vX As String = GetRowPropVal(rrX, selProp).Trim()
													Dim matched As Boolean = False
													For Each sv As String In selVals
														If String.Compare(vX, sv.Trim(), True) = 0 Then matched = True : Exit For
													Next
													If Not matched Then Continue For
												End If
												Dim dirX As String = System.IO.Path.GetDirectoryName(rrX.FilePath)
												Dim baseX As String = System.IO.Path.GetFileNameWithoutExtension(rrX.FilePath)
												For Each extX As String In New String() {".idw", ".dwg"}
													Dim dwgPathX As String = System.IO.Path.Combine(dirX, baseX & extX)
													If System.IO.File.Exists(dwgPathX) AndAlso Not matchedDwgs.Contains(dwgPathX) Then
														matchedDwgs.Add(dwgPathX) : Exit For
													End If
												Next
												' 同目录没找到，搜索项目目录'
												If matchedDwgs.Count = 0 OrElse Not matchedDwgs(matchedDwgs.Count - 1).ToLower().EndsWith(baseX.ToLower() & ".idw") AndAlso Not matchedDwgs(matchedDwgs.Count - 1).ToLower().EndsWith(baseX.ToLower() & ".dwg") Then
													Dim projFoundX As String = FindDrawingInProject(baseX)
													If Not String.IsNullOrEmpty(projFoundX) AndAlso Not matchedDwgs.Contains(projFoundX) Then matchedDwgs.Add(projFoundX)
												End If
											Next
											If matchedDwgs.Count = 0 Then
												MessageBox.Show("未找到匹配的工程图。" & vbCrLf & "属性：" & selProp & " = " & selVal, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
											End If
											' ── 选择输出路径（自定义对话框）──
											Dim defaultDir As String = System.IO.Path.GetDirectoryName(g_asmDoc.FullDocumentName)
											Dim outDir As String = ShowFolderInputDialog("选择PDF/DWG输出目录", defaultDir)
											If outDir Is Nothing Then Return
											If Not System.IO.Directory.Exists(outDir) Then
												MessageBox.Show("目录不存在：" & outDir, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return
											End If
											' ── 确认 ──
											Dim fmtStr As String = If(chkPDF.Checked AndAlso chkDWG.Checked, "PDF + DWG", If(chkPDF.Checked, "PDF", "DWG"))
											Dim confirmMsg As String = "即将导出 " & matchedDwgs.Count & " 个工程图" & vbCrLf &
	"格式：" & fmtStr & vbCrLf & "输出目录：" & outDir & vbCrLf & vbCrLf & "是否继续？"
											If MessageBox.Show(confirmMsg, "按属性导出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
											' ── 导出 ──
											Dim pdfTransId As String = "{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"
											Dim dwgTransId As String = "{C24E3AC2-122E-11D5-8E91-0010B541CD80}"
											Dim successCnt As Integer = 0 : Dim failCnt As Integer = 0
											Dim exportIdx As Integer = 0
											pgExport.Minimum = 0
											pgExport.Maximum = matchedDwgs.Count + 1
											pgExport.Value = 0
											lblProgress.Text = "准备导出 " & matchedDwgs.Count & " 个工程图..."
											frm.Refresh()
											For Each dwgPathX2 As String In matchedDwgs
												Dim dwgDocX As DrawingDocument = Nothing
												exportIdx += 1
												' 每个文件开始时更新文字和进度'
												lblProgress.Text = "正在处理 " & exportIdx & " / " & matchedDwgs.Count & "：" & System.IO.Path.GetFileNameWithoutExtension(dwgPathX2)
												pgExport.Value = exportIdx - 1
												frm.Refresh()
												System.Windows.Forms.Application.DoEvents()
												Dim wasOpenX As Boolean = False
												Try
													For Each odX As Document In ThisApplication.Documents
														If String.Compare(odX.FullDocumentName, dwgPathX2, True) = 0 Then
															dwgDocX = CType(odX, DrawingDocument) : wasOpenX = True : Exit For
														End If
													Next
													If dwgDocX Is Nothing Then
														dwgDocX = CType(ThisApplication.Documents.Open(dwgPathX2, False), DrawingDocument)
													End If
													Dim baseNameX As String = System.IO.Path.GetFileNameWithoutExtension(dwgPathX2)
													Dim oCtxX As Object = ThisApplication.TransientObjects.CreateTranslationContext()
													oCtxX.Type = kFileBrowseIOMechanism
													Dim oOptsX As Object = ThisApplication.TransientObjects.CreateNameValueMap()
													Dim oDataX As Object = ThisApplication.TransientObjects.CreateDataMedium()
													If chkPDF.Checked Then
														Try
															Dim pdfTransX As Object = ThisApplication.ApplicationAddIns.ItemById(pdfTransId)
															oDataX.FileName = System.IO.Path.Combine(outDir, baseNameX & ".pdf")
															pdfTransX.SaveCopyAs(dwgDocX, oCtxX, oOptsX, oDataX)
														Catch : End Try
													End If
													If chkDWG.Checked Then
														Try
															Dim dwgTransX As Object = ThisApplication.ApplicationAddIns.ItemById(dwgTransId)
															oDataX.FileName = System.IO.Path.Combine(outDir, baseNameX & ".dwg")
															dwgTransX.SaveCopyAs(dwgDocX, oCtxX, oOptsX, oDataX)
														Catch : End Try
													End If
													successCnt += 1
													' 绕过WinForms动画：先超出再设回'
													If exportIdx >= matchedDwgs.Count Then
														pgExport.Value = pgExport.Maximum
														pgExport.Value = pgExport.Maximum - 1
														pgExport.Value = pgExport.Maximum
													Else
														pgExport.Value = exportIdx + 1
														pgExport.Value = exportIdx
													End If
													frm.Refresh()
												Catch exX As Exception
													failCnt += 1
													If exportIdx >= matchedDwgs.Count Then
														pgExport.Value = pgExport.Maximum
														pgExport.Value = pgExport.Maximum - 1
														pgExport.Value = pgExport.Maximum
													Else
														pgExport.Value = exportIdx + 1
														pgExport.Value = exportIdx
													End If
													frm.Refresh()
												Finally
													If Not wasOpenX AndAlso dwgDocX IsNot Nothing Then
														Try : dwgDocX.Close(False) : Catch : End Try
													End If
												End Try
											Next
											' ── 结果 ──
											Dim resultMsg As String = "导出完成！" & vbCrLf &
	"成功：" & successCnt & " 个" & vbCrLf &
	"失败：" & failCnt & " 个" & vbCrLf &
	"输出目录：" & outDir
											pgExport.Maximum = matchedDwgs.Count
											pgExport.Value = matchedDwgs.Count
											lblProgress.Text = "✅ 导出完成！成功:" & successCnt & " 失败:" & failCnt
											frm.Refresh()
											MessageBox.Show(resultMsg, "按属性导出", MessageBoxButtons.OK, MessageBoxIcon.Information)
											SetStatus("  已导出 " & successCnt & " 个工程图到 " & outDir, C(0, 130, 55))
										End Sub
			' ── Shown事件：扫描所有零件属性，填充cmbProp ──
			AddHandler frm.Shown, Sub(ss, es)
									  Try
										  Dim sysPropsE As New List(Of String)()
										  Dim custPropsE As New List(Of String)()
										  Dim seenSysE As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim seenCustE As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim seenFpE As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  ' 类别排第一'
										  sysPropsE.Add("类别") : seenSysE.Add("类别")
										  If g_rows Is Nothing Then Return
										  For Each rrE As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rrE.FilePath) OrElse rrE.IsMissing Then Continue For
											  If seenFpE.Contains(rrE.FilePath) Then Continue For
											  seenFpE.Add(rrE.FilePath)
											  Try
												  Dim tDocE As Document = Nothing
												  For Each odE As Document In ThisApplication.Documents
													  If String.Compare(odE.FullDocumentName, rrE.FilePath, True) = 0 Then tDocE = odE : Exit For
												  Next
												  If tDocE Is Nothing AndAlso System.IO.File.Exists(rrE.FilePath) Then
													  tDocE = ThisApplication.Documents.Open(rrE.FilePath, False)
												  End If
												  If tDocE Is Nothing Then Continue For
												  ' 系统属性集'
												  For Each psGuidE As String In New String() {GUID_DTP, GUID_SUMMARY, GUID_DOCSUMMARY}
													  Try
														  Dim psE As PropertySet = tDocE.PropertySets.Item(psGuidE)
														  For Each pE As Inventor.Property In psE
															  Try
																  Dim pdnE As String = pE.DisplayName.Trim()
																  If String.IsNullOrEmpty(pdnE) Then pdnE = pE.Name.Trim()
																  If Not seenSysE.Contains(pdnE) Then seenSysE.Add(pdnE) : sysPropsE.Add(pdnE)
															  Catch : End Try
														  Next
													  Catch : End Try
												  Next
												  ' 自定义属性集'
												  Try
													  Dim udpE As PropertySet = tDocE.PropertySets.Item(GUID_UDP)
													  For Each pE2 As Inventor.Property In udpE
														  Try
															  Dim pdnE2 As String = pE2.DisplayName.Trim()
															  If String.IsNullOrEmpty(pdnE2) Then pdnE2 = pE2.Name.Trim()
															  If Not seenCustE.Contains(pdnE2) Then seenCustE.Add(pdnE2) : custPropsE.Add(pdnE2)
														  Catch : End Try
													  Next
												  Catch : End Try
											  Catch : End Try
										  Next
										  ' 填充cmbProp（空=全部导出，类别第一，系统属性，分隔线，自定义属性）'
										  cmbProp.Items.Clear()
										  cmbProp.Items.Add("（不限属性，全部导出）")
										  ' 类别排第一'
										  cmbProp.Items.Add("类别")
										  ' 其余系统属性排序后加入'
										  sysPropsE.Remove("类别")
										  sysPropsE.Sort()
										  For Each spE As String In sysPropsE : cmbProp.Items.Add(spE) : Next
										  If custPropsE.Count > 0 Then
											  cmbProp.Items.Add("──────────────")
											  custPropsE.Sort()
											  For Each cpE As String In custPropsE : cmbProp.Items.Add(cpE) : Next
										  End If
										  If cmbProp.Items.Count > 0 Then cmbProp.SelectedIndex = 0
										  ' 初始化valClb（类别的所有值）'
										  If cmbProp.Items.Count > 0 AndAlso valClb IsNot Nothing Then
											  Try
												  Dim initVals As List(Of String) = CollectPropValues(g_rows, CStr(cmbProp.Items(0)))
												  valClb.Items.Clear()
												  For Each iv As String In initVals : valClb.Items.Add(iv, False) : Next
											  Catch : End Try
										  End If
									  Catch : End Try
								  End Sub
			' ── Shown事件：扫描所有零件自定义属性+系统属性 ──
			Dim _lstProps As System.Windows.Forms.ListBox = Nothing
			AddHandler frm.Shown, Sub(ss, es)
									  Try
										  If _lstProps Is Nothing Then Return
										  _lstProps.Items.Clear()
										  Dim sysPropSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  Dim custPropSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  ' 系统属性固定列表（全量中文）'
										  For Each sp As String In New String() {
			"零件代号", "描述", "材料", "类别", "图号",
			"修订版本", "设计者", "工程师", "成本", "标准",
			"供应商", "制造商", "项目", "状态", "备注",
			"质量", "密度", "体积", "面积", "重量",
			"钣金厚度", "折弯半径", "展开长度", "展开宽度",
			"展开面积", "折弯系数", "折弯扣除",
			"作者", "公司", "标题", "主题", "关键字",
			"创建日期", "修改日期", "保存日期",
			"文件名", "文件路径", "文档类型"
			}
											  sysPropSet.Add(sp)
										  Next
										  ' 从BOM列定义补充所有已知属性名'
										  If g_colDefs IsNot Nothing Then
											  For Each cd As ColDef In g_colDefs
												  If Not String.IsNullOrEmpty(cd.ColName) Then
													  If sysPropSet.Contains(cd.ColName) Then Continue For
													  custPropSet.Add(cd.ColName)
												  End If
											  Next
										  End If
										  ' 扫描所有零件文件的自定义属性'
										  Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
										  For Each rr As BomDataRow In g_rows
											  If String.IsNullOrEmpty(rr.FilePath) OrElse rr.IsMissing Then Continue For
											  If seen.Contains(rr.FilePath) Then Continue For
											  seen.Add(rr.FilePath)
											  ' 从BomDataRow.Custom取自定义属性'
											  For Each kv As System.Collections.Generic.KeyValuePair(Of String, String) In rr.Custom
												  If Not sysPropSet.Contains(kv.Key) Then custPropSet.Add(kv.Key)
											  Next
											  ' 尝试打开文件读取所有PropertySets'
											  Try
												  Dim tDoc As Document = Nothing
												  For Each od As Document In ThisApplication.Documents
													  If String.Compare(od.FullDocumentName, rr.FilePath, True) = 0 Then tDoc = od : Exit For
												  Next
												  If tDoc Is Nothing Then tDoc = ThisApplication.Documents.Open(rr.FilePath, False)
												  If tDoc IsNot Nothing Then
													  For Each pset As PropertySet In tDoc.PropertySets
														  If pset.DisplayName = "User Defined Properties" Then
															  For Each prop As Object In pset
																  If Not sysPropSet.Contains(CType(prop, Inventor.Property).DisplayName) Then custPropSet.Add(CType(prop, Inventor.Property).DisplayName)
															  Next
														  End If
													  Next
												  End If
											  Catch : End Try
										  Next
										  ' ── 填充列表：系统属性分组 + 自定义属性分组 ──'
										  _lstProps.Items.Add("── 系统属性 ──")
										  Dim sysList As New System.Collections.Generic.List(Of String)(sysPropSet) : sysList.Sort()
										  For Each p As String In sysList : _lstProps.Items.Add(p) : Next
										  _lstProps.Items.Add("── 自定义属性 ──")
										  Dim custList As New System.Collections.Generic.List(Of String)(custPropSet) : custList.Sort()
										  For Each p As String In custList : _lstProps.Items.Add(p) : Next
									  Catch : End Try
								  End Sub
			frm.ShowDialog()
		Catch ex As Exception
			MessageBox.Show("界面打开失败：" & ex.Message & vbCrLf & ex.StackTrace, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Sub ShowBOMQuantityDialog()
		Try
			Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
			If oDoc Is Nothing Then
				System.Windows.Forms.MessageBox.Show("请先打开一个部件文档！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
				Return
			End If
			If oDoc.DocumentType <> Inventor.DocumentTypeEnum.kAssemblyDocumentObject Then
				System.Windows.Forms.MessageBox.Show("请在部件(Assembly)环境中运行本工具！", "环境错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
				Return
			End If
			Dim frm As New BOMQuantityForm(CType(oDoc, Inventor.AssemblyDocument), ThisApplication)
			frm.Show()  ' 非模态：打开统计窗后仍可操作Inventor
			Do While Not frm.IsDisposed
				System.Threading.Thread.Sleep(15)
				System.Windows.Forms.Application.DoEvents()
			Loop
		Catch ex As Exception
			System.Windows.Forms.MessageBox.Show("BOM数量统计工具错误：" & ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
		End Try
	End Sub

	Sub CollectIdwFiles(ByVal oDef As ComponentDefinition, ByVal printed As List(Of String), ByVal idwList As List(Of String))
		Try
			For Each occ As ComponentOccurrence In oDef.Occurrences
				If occ.Suppressed Then Continue For
				Dim fn As String = occ.Definition.Document.FullFileName
				Dim idw As String = System.IO.Path.ChangeExtension(fn, ".idw")
				' 用idw路径去重，避免同一工程图重复打印'
				If System.IO.File.Exists(idw) AndAlso Not idwList.Contains(idw) Then
					idwList.Add(idw)
				End If
				' 用部件路径去重，避免重复遍历同一子装配'
				If occ.SubOccurrences.Count > 0 AndAlso Not printed.Contains(fn) Then
					printed.Add(fn)
					CollectIdwFiles(occ.Definition, printed, idwList)
				End If
			Next
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
		End Try
	End Sub

	Sub PrintOneDrawing(ByVal pathidw As String, ByVal noteText As String, ByVal addTime As Boolean, ByVal posX As Double, ByVal posY As Double, ByVal printerName As String, ByVal fontSize As Double, ByVal pdfSaveDir As String, ByVal paperOverride As String)
		Dim oApp2 As Inventor.Application = ThisApplication
		Dim oDrawDoc = oApp2.Documents.Open(pathidw, True)
		Try
			Dim oSheet = oDrawDoc.ActiveSheet
			Dim oPoint = oApp2.TransientGeometry.CreatePoint2d(posX, posY)
			Dim stampText As String = ""
			If noteText <> "" Then stampText = noteText & "  "
			If addTime Then stampText = stampText & CStr(Now())
			' 记录添加的文字对象引用，打印后精确删除'
			Dim oStampNote As Object = Nothing
			If stampText <> "" Then
				oStampNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oPoint, stampText)
			End If
			Dim oPrintMgr As DrawingPrintManager = CType(oDrawDoc.PrintManager, DrawingPrintManager)
			If printerName.StartsWith("[导出PDF]") Then
				' PDF导出：自动保存到工程图同目录，文件名与工程图相同'
				' PDF导出到用户选择目录'
				Dim pdfName As String = System.IO.Path.GetFileNameWithoutExtension(pathidw) & ".pdf"
				Dim pdfPath As String = System.IO.Path.Combine(pdfSaveDir, pdfName)
				Try
					Dim oPdfAddin As TranslatorAddIn = CType(oApp2.ApplicationAddIns.ItemById("{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"), TranslatorAddIn)
					Dim oContext As TranslationContext = oApp2.TransientObjects.CreateTranslationContext()
					oContext.Type = kFileBrowseIOMechanism
					Dim oOptions As NameValueMap = oApp2.TransientObjects.CreateNameValueMap()
					oOptions.Add("All_Color_AS_Black", True)
					oOptions.Add("Vector_Resolution", 400)
					oOptions.Add("Sheet_Range", kPrintAllSheets)
					Dim oData As DataMedium = oApp2.TransientObjects.CreateDataMedium()
					oData.FileName = pdfPath
					oPdfAddin.SaveCopyAs(oDrawDoc, oContext, oOptions, oData)
				Catch exPdf As Exception
					MessageBox.Show("PDF导出失败: " & exPdf.Message & vbCrLf & "路径: " & pdfPath, "DWJ")
				End Try
			Else
				' 打印机打印：设置纸张和缩放'
				oPrintMgr.AllColorsAsBlack = True
				oPrintMgr.NumberOfCopies = 1
				If oSheet.Width > oSheet.Height Then
					oPrintMgr.Orientation = 13570
				Else
					oPrintMgr.Orientation = 13569
				End If
				oPrintMgr.PrintRange = kPrintAllSheets
				oPrintMgr.ScaleMode = PrintScaleModeEnum.kPrintBestFitScale
				' 纸张：强制指定或按原始规格'
				If paperOverride = "全部输出A3" Then
					oPrintMgr.PaperSize = PaperSizeEnum.kPaperSizeA3
				ElseIf paperOverride = "全部输出A4" Then
					oPrintMgr.PaperSize = PaperSizeEnum.kPaperSizeA4
				Else
					oPrintMgr.PaperSize = PaperSizeEnum.kPaperSizeDefault
				End If
				' Printer最后设置'
				If printerName <> "" Then oPrintMgr.Printer = printerName
				oPrintMgr.SubmitPrint()
			End If
			' 打印/导出完成后立即删除添加的文字（用对象引用，精确可靠）'
			If oStampNote IsNot Nothing Then
				Try : oStampNote.Delete() : Catch : End Try
				oStampNote = Nothing
			End If
		Finally
			oDrawDoc.Close(False)
		End Try
	End Sub

	Function ShowFolderInputDialog(title As String, defaultPath As String) As String
		Try
			Dim bi As New BROWSEINFO2()
			bi.lpszTitle = title
			bi.ulFlags = 1
			Dim pidl As IntPtr = SHBrowseForFolder(bi)
			If pidl = IntPtr.Zero Then Return Nothing
			Dim sb As New System.Text.StringBuilder(260)
			SHGetPathFromIDList2(pidl, sb)
			CoTaskMemFree2(pidl)
			Dim result As String = sb.ToString()
			If String.IsNullOrEmpty(result) Then Return defaultPath
			Return result
		Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			Return Nothing
		End Try
	End Function



	' PartInfo：BOMQuantityForm 使用的零件信息类
	Public Class PartInfo
		Public FileName As String
		Public FullPath As String
		Public SingleQty As Integer
		Public CurrentSingleQty As String
		Public CurrentProdQty As String
		Public CurrentDeviceQty As String
		Public CalcProdQty As Integer
		Public IsDuplicateName As Boolean
		Public IsModifiable As Boolean
		Public OccurrenceList As New List(Of Inventor.ComponentOccurrence)
		Public RowIndex As Integer = -1
	End Class

	Public Class BOMQuantityForm
		Inherits System.Windows.Forms.Form

		' --- 控件声明 ---
		Private lblTitle As System.Windows.Forms.Label
		Private lblDeviceQty As System.Windows.Forms.Label
		Private numDeviceQty As System.Windows.Forms.NumericUpDown
		Private chkPreviewOnly As System.Windows.Forms.CheckBox
		Private dgvParts As System.Windows.Forms.DataGridView
		Private btnRefresh As System.Windows.Forms.Button
		Private btnHighlightInventor As System.Windows.Forms.Button
		Private btnApply As System.Windows.Forms.Button
		Private btnClose As System.Windows.Forms.Button
		Private lblStatus As System.Windows.Forms.Label
		Private lblDuplicateWarning As System.Windows.Forms.Label
		Private progressBar As System.Windows.Forms.ProgressBar

		' --- 数据字段 ---
		Private openDoc As Inventor.AssemblyDocument
		Private partsList As New List(Of PartInfo)
		Private oBOMQTY As String = "单套数量"
		Private oBOMORDERQTY As String = "总数量"
		Private oNORDER As String = "设备套数"
		Private inventorApp As Inventor.Application

		' --- 构造函数 ---
		Public Sub New(doc As Inventor.AssemblyDocument, app As Inventor.Application)
			openDoc = doc
			inventorApp = app
			InitializeForm()
			LoadPartsData()
		End Sub

		' --- 窗体初始化 ---
		Private Sub InitializeForm()
			' ★ DPI 缩放因子（96=标准，192=200%，4K高分屏自动适配）
			Dim dpiScale As Single = 1.0!
			Try
				dpiScale = CSng(Me.DeviceDpi) / 96.0!
			Catch : End Try
			If dpiScale < 1.0! Then dpiScale = 1.0!
			Dim S As Func(Of Integer, Integer) = Function(v) CInt(v * dpiScale)
			Me.Text = "BOM 数量统计工具 - 增强版 (Inventor 2016+)"
			Me.Size = New System.Drawing.Size(1150, 720)
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
			Me.MinimumSize = New System.Drawing.Size(1170, 600)
			' ★ DPI 感知：让 WinForms 按屏幕 DPI 自动缩放所有控件（4K/高分屏适配）
			Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

			Me.Font = New System.Drawing.Font("微软雅黑", 9)
			Me.BackColor = System.Drawing.Color.FromArgb(250, 250, 250)

			' 标题标签
			lblTitle = New System.Windows.Forms.Label()
			lblTitle.Text = "BOM 零部件数量统计与属性写入工具"
			lblTitle.Font = New System.Drawing.Font("微软雅黑", 14, System.Drawing.FontStyle.Bold)
			lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204)
			lblTitle.AutoSize = True
			lblTitle.Location = New System.Drawing.Point(S(20), S(15))
			Me.Controls.Add(lblTitle)

			' 设备套数输入
			lblDeviceQty = New System.Windows.Forms.Label()
			lblDeviceQty.Text = "设备套数："
			lblDeviceQty.AutoSize = True
			lblDeviceQty.Location = New System.Drawing.Point(S(20), S(55))
			lblDeviceQty.Font = New System.Drawing.Font("微软雅黑", 10, System.Drawing.FontStyle.Bold)
			Me.Controls.Add(lblDeviceQty)

			numDeviceQty = New System.Windows.Forms.NumericUpDown()
			numDeviceQty.Location = New System.Drawing.Point(S(100), S(52))
			numDeviceQty.Size = New System.Drawing.Size(S(80), S(25))
			numDeviceQty.Minimum = 1
			numDeviceQty.Maximum = 99999
			numDeviceQty.Value = 1
			numDeviceQty.Font = New System.Drawing.Font("微软雅黑", 10)
			AddHandler numDeviceQty.ValueChanged, AddressOf NumDeviceQty_ValueChanged
			Me.Controls.Add(numDeviceQty)

			' 仅预览复选框
			chkPreviewOnly = New System.Windows.Forms.CheckBox()
			chkPreviewOnly.Text = "仅预览，不写入属性（先核对数据）"
			chkPreviewOnly.Location = New System.Drawing.Point(S(200), S(54))
			chkPreviewOnly.AutoSize = True
			chkPreviewOnly.Checked = True
			chkPreviewOnly.ForeColor = System.Drawing.Color.DarkOrange
			chkPreviewOnly.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			Me.Controls.Add(chkPreviewOnly)

			' 刷新按钮
			btnRefresh = New System.Windows.Forms.Button()
			btnRefresh.Text = "刷新数据"
			btnRefresh.Location = New System.Drawing.Point(S(460), S(50))
			btnRefresh.Size = New System.Drawing.Size(S(120), S(30))
			btnRefresh.BackColor = System.Drawing.Color.FromArgb(0, 122, 204)
			btnRefresh.ForeColor = System.Drawing.Color.White
			btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			btnRefresh.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
			Me.Controls.Add(btnRefresh)

			' 亮显按钮
			btnHighlightInventor = New System.Windows.Forms.Button()
			btnHighlightInventor.Text = "在Inventor中亮显选中项"
			btnHighlightInventor.Location = New System.Drawing.Point(S(588), S(50))
			btnHighlightInventor.Size = New System.Drawing.Size(S(180), S(30))
			btnHighlightInventor.BackColor = System.Drawing.Color.FromArgb(255, 140, 0)
			btnHighlightInventor.ForeColor = System.Drawing.Color.White
			btnHighlightInventor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			btnHighlightInventor.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			AddHandler btnHighlightInventor.Click, AddressOf BtnHighlight_Click
			Me.Controls.Add(btnHighlightInventor)

			' 应用按钮
			btnApply = New System.Windows.Forms.Button()
			btnApply.Text = "写入属性"
			btnApply.Location = New System.Drawing.Point(S(776), S(50))
			btnApply.Size = New System.Drawing.Size(S(100), S(30))
			btnApply.BackColor = System.Drawing.Color.FromArgb(46, 125, 50)
			btnApply.ForeColor = System.Drawing.Color.White
			btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			btnApply.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			AddHandler btnApply.Click, AddressOf BtnApply_Click
			Me.Controls.Add(btnApply)

			' 关闭按钮
			btnClose = New System.Windows.Forms.Button()
			btnClose.Text = "关闭"
			btnClose.Location = New System.Drawing.Point(S(884), S(50))
			btnClose.Size = New System.Drawing.Size(S(90), S(30))
			btnClose.BackColor = System.Drawing.Color.FromArgb(211, 47, 47)
			btnClose.ForeColor = System.Drawing.Color.White
			btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			btnClose.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			AddHandler btnClose.Click, AddressOf BtnClose_Click
			Me.Controls.Add(btnClose)
			' 使用说明按钮
			Dim btnQtyHelp As New System.Windows.Forms.Button()
			btnQtyHelp.Text = "使用说明"
			btnQtyHelp.Location = New System.Drawing.Point(S(982), S(50))
			btnQtyHelp.Anchor = System.Windows.Forms.AnchorStyles.Top
			btnQtyHelp.Size = New System.Drawing.Size(S(72), S(30))
			btnQtyHelp.BackColor = System.Drawing.Color.FromArgb(255, 152, 0)
			btnQtyHelp.ForeColor = System.Drawing.Color.White
			btnQtyHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			btnQtyHelp.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			AddHandler btnQtyHelp.Click, Sub(s2, e2)
											 Dim hd As New System.Windows.Forms.Form()
											 hd.Text = "BOM数量统计 - 使用说明"
											 hd.Size = New System.Drawing.Size(500, 480)
											 hd.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
											 hd.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
											 hd.MaximizeBox = False
											 hd.MinimizeBox = False
											 Dim tx As New System.Windows.Forms.TextBox()
											 tx.Multiline = True
											 tx.ReadOnly = True
											 tx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
											 tx.Dock = System.Windows.Forms.DockStyle.Fill
											 tx.Font = New System.Drawing.Font("微软雅黑", 10)
											 tx.BackColor = System.Drawing.Color.White
											 tx.Text = "【功能说明】" & vbCrLf &
				   "  统计装配体里每个零件的单套数量，乘以设备套数得到总数量，" & vbCrLf &
				   "  并把数量写入零件属性。" & vbCrLf & vbCrLf &
				   "【操作步骤】" & vbCrLf &
				   "  1. 在顶部「设备套数」输入本设备要做几套" & vbCrLf &
				   "  2. 表格自动列出每个零件的单套数量" & vbCrLf &
				   "  3. 总数量 = 单套数量 × 设备套数，自动计算" & vbCrLf &
				   "  4. 可勾选「仅预览不写入」先核对" & vbCrLf &
				   "  5. 点「刷新」可重新采集" & vbCrLf &
				   "  6. 点「在Inventor中高亮」可定位选中零件" & vbCrLf &
				   "  7. 核对无误后点「写入属性」保存到零件" & vbCrLf & vbCrLf &
				   "【注意】" & vbCrLf &
				   "  · 数量统计含装配体中所有层级（已展开）" & vbCrLf &
				   "  · 标准件、抑制零件默认跳过" & vbCrLf &
				   "  · 只读零件无法写入，会在状态栏提示" & vbCrLf &
				   "  · 写入后请保存零件文件"
											 hd.Controls.Add(tx)
											 Dim okb As New System.Windows.Forms.Button()
											 okb.Text = "我知道了"
											 okb.Dock = System.Windows.Forms.DockStyle.Bottom
											 okb.Height = 40
											 AddHandler okb.Click, Sub(s3, e3) hd.Close()
											 hd.Controls.Add(okb)
											 hd.ShowDialog()
										 End Sub
			Me.Controls.Add(btnQtyHelp)
			dgvParts = New System.Windows.Forms.DataGridView()
			dgvParts.Location = New System.Drawing.Point(S(20), S(90))
			dgvParts.Size = New System.Drawing.Size(S(1110), S(495))
			dgvParts.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
			dgvParts.AllowUserToAddRows = False
			dgvParts.AllowUserToDeleteRows = False
			dgvParts.ReadOnly = True
			dgvParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
			dgvParts.MultiSelect = False
			dgvParts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
			dgvParts.RowHeadersVisible = False
			dgvParts.BackgroundColor = System.Drawing.Color.White
			dgvParts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			dgvParts.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 102, 204)
			dgvParts.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
			dgvParts.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			dgvParts.ColumnHeadersHeight = 30
			dgvParts.DefaultCellStyle.Font = New System.Drawing.Font("微软雅黑", 9)
			dgvParts.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255)
			dgvParts.EnableHeadersVisualStyles = False
			AddHandler dgvParts.CellDoubleClick, AddressOf DgvParts_CellDoubleClick
			AddHandler dgvParts.SelectionChanged, AddressOf DgvParts_SelectionChanged
			Me.Controls.Add(dgvParts)

			lblDuplicateWarning = New System.Windows.Forms.Label()
			lblDuplicateWarning.Text = ""
			lblDuplicateWarning.Location = New System.Drawing.Point(S(20), S(592))
			lblDuplicateWarning.Size = New System.Drawing.Size(S(1090), S(22))
			lblDuplicateWarning.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
			lblDuplicateWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			lblDuplicateWarning.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
			Me.Controls.Add(lblDuplicateWarning)

			' 进度条
			progressBar = New System.Windows.Forms.ProgressBar()
			progressBar.Location = New System.Drawing.Point(S(20), S(620))
			progressBar.Size = New System.Drawing.Size(S(1090), S(20))
			progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
			progressBar.Visible = False
			Me.Controls.Add(progressBar)

			' 状态栏
			lblStatus = New System.Windows.Forms.Label()
			lblStatus.Text = "就绪"
			lblStatus.AutoSize = True
			lblStatus.Location = New System.Drawing.Point(S(20), S(645))
			lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
			lblStatus.Font = New System.Drawing.Font("微软雅黑", 9)
			Me.Controls.Add(lblStatus)
		End Sub

		' ==================== 属性读取辅助函数 ====================
		Private Function GetCustomProperty(doc As Inventor.Document, propName As String) As String
			Try
				Dim propSets As Inventor.PropertySets = doc.PropertySets
				Dim customSet As Inventor.PropertySet = propSets.Item("Inventor User Defined Properties")
				Dim prop As Inventor.Property = customSet.Item(propName)
				Return prop.Value.ToString()
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				Return "(无)"
			End Try
		End Function

		Private Sub SetCustomProperty(doc As Inventor.Document, propName As String, value As Object)
			Dim propSets As Inventor.PropertySets = doc.PropertySets
			Dim customSet As Inventor.PropertySet
			Try
				customSet = propSets.Item("Inventor User Defined Properties")
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				customSet = propSets.Add("Inventor User Defined Properties")
			End Try
			Try
				Dim prop As Inventor.Property = customSet.Item(propName)
				prop.Value = value
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
				customSet.Add(value, propName)
			End Try
		End Sub

		' ==================== 数据加载 ====================
		Private Sub LoadPartsData()
			Try
				lblStatus.Text = "正在加载零部件数据..."
				Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
				partsList.Clear()
				dgvParts.Columns.Clear()

				' 定义列
				dgvParts.Columns.Add("RowNo", "序号")
				dgvParts.Columns.Add("FileName", "文件名")
				dgvParts.Columns.Add("FullPath", "完整路径")
				dgvParts.Columns.Add("SingleQty", "单套数量")
				dgvParts.Columns.Add("CurSingleQty", "当前单套数量")
				dgvParts.Columns.Add("ProdQty", "总数量")
				dgvParts.Columns.Add("CurProdQty", "当前生产数量")
				dgvParts.Columns.Add("DeviceQty", "计算设备套数")
				dgvParts.Columns.Add("CurDeviceQty", "当前设备套数")
				dgvParts.Columns.Add("IsDuplicate", "同名警告")
				dgvParts.Columns.Add("Status", "状态")

				dgvParts.Columns("RowNo").FillWeight = 6
				dgvParts.Columns("FileName").FillWeight = 14
				dgvParts.Columns("FullPath").FillWeight = 26
				dgvParts.Columns("SingleQty").FillWeight = 8
				dgvParts.Columns("CurSingleQty").FillWeight = 10
				dgvParts.Columns("ProdQty").FillWeight = 10
				dgvParts.Columns("CurProdQty").FillWeight = 10
				dgvParts.Columns("DeviceQty").FillWeight = 8
				dgvParts.Columns("CurDeviceQty").FillWeight = 10
				dgvParts.Columns("IsDuplicate").FillWeight = 8
				dgvParts.Columns("Status").FillWeight = 8

				Dim assemblyDef As Inventor.AssemblyComponentDefinition = openDoc.ComponentDefinition
				Dim deviceQty As Integer = CInt(numDeviceQty.Value)
				Dim fileNameDict As New Dictionary(Of String, Integer)
				Dim rowIndex As Integer = 0

				' 第一次遍历：收集文件名用于同名检测（跳过虚拟件和参考件）
				For Each docFile As Inventor.Document In openDoc.AllReferencedDocuments
					Try
						Dim bs0 As Integer = CInt(docFile.ComponentDefinition.BOMStructure)
						If bs0 = 51971 OrElse bs0 = 51972 Then Continue For
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try
					Dim fName As String = System.IO.Path.GetFileName(docFile.FullFileName)
					If fileNameDict.ContainsKey(fName) Then
						fileNameDict(fName) = fileNameDict(fName) + 1
					Else
						fileNameDict.Add(fName, 1)
					End If
				Next

				' 第二次遍历：填充数据（跳过虚拟件51971和参考件51972）
				For Each docFile As Inventor.Document In openDoc.AllReferencedDocuments
					' 跳过虚拟件和参考件
					Try
						Dim bs As Integer = CInt(docFile.ComponentDefinition.BOMStructure)
						If bs = 51971 OrElse bs = 51972 Then Continue For
					Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					End Try

					Dim info As New PartInfo()
					info.FullPath = docFile.FullFileName
					info.FileName = System.IO.Path.GetFileName(docFile.FullFileName)
					info.IsModifiable = docFile.IsModifiable

					Dim occs As Inventor.ComponentOccurrencesEnumerator = Nothing
					Try
						occs = assemblyDef.Occurrences.AllReferencedOccurrences(docFile)
					Catch
						occs = Nothing
					End Try
					If occs IsNot Nothing Then
						Try
							info.SingleQty = occs.Count
						Catch
							info.SingleQty = 0
						End Try
					Else
						info.SingleQty = 0
					End If
					info.CalcProdQty = info.SingleQty * deviceQty

					info.CurrentSingleQty = GetCustomProperty(docFile, oBOMQTY)
					info.CurrentProdQty = GetCustomProperty(docFile, oBOMORDERQTY)
					info.CurrentDeviceQty = GetCustomProperty(docFile, oNORDER)

					If fileNameDict.ContainsKey(info.FileName) AndAlso fileNameDict(info.FileName) > 1 Then
						info.IsDuplicateName = True
					Else
						info.IsDuplicateName = False
					End If

					If occs IsNot Nothing Then
						Try
							For Each occ As Inventor.ComponentOccurrence In occs
								info.OccurrenceList.Add(occ)
							Next
						Catch
						End Try
					End If

					info.RowIndex = rowIndex
					partsList.Add(info)
					rowIndex = rowIndex + 1
				Next

				' 装配体本身
				Dim assyInfo As New PartInfo()
				assyInfo.FullPath = openDoc.FullFileName
				assyInfo.FileName = System.IO.Path.GetFileName(openDoc.FullFileName)
				assyInfo.IsModifiable = openDoc.IsModifiable
				assyInfo.SingleQty = 1
				assyInfo.CalcProdQty = deviceQty
				assyInfo.CurrentSingleQty = GetCustomProperty(openDoc, oBOMQTY)
				assyInfo.CurrentProdQty = GetCustomProperty(openDoc, oBOMORDERQTY)
				assyInfo.CurrentDeviceQty = GetCustomProperty(openDoc, oNORDER)
				assyInfo.IsDuplicateName = False
				assyInfo.RowIndex = rowIndex
				partsList.Add(assyInfo)

				' 填充 DataGridView
				Dim duplicateCount As Integer = 0
				For Each info As PartInfo In partsList
					Dim statusText As String = "正常"
					If Not info.IsModifiable Then
						statusText = "只读"
					End If
					If info.IsDuplicateName Then
						statusText = "同名冲突"
						duplicateCount = duplicateCount + 1
					End If

					Dim row As String() = New String() {
					(info.RowIndex + 1).ToString(),
					info.FileName,
					info.FullPath,
					info.SingleQty.ToString(),
					info.CurrentSingleQty,
					info.CalcProdQty.ToString(),
					info.CurrentProdQty,
					deviceQty.ToString(),
					info.CurrentDeviceQty,
					If(info.IsDuplicateName, "同名", ""),
					statusText
					}
					dgvParts.Rows.Add(row)

					Dim rowIdx As Integer = dgvParts.Rows.Count - 1
					If info.IsDuplicateName Then
						dgvParts.Rows(rowIdx).DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 235, 238)
						dgvParts.Rows(rowIdx).DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed
						dgvParts.Rows(rowIdx).DefaultCellStyle.Font = New System.Drawing.Font("微软雅黑", 9, System.Drawing.FontStyle.Bold)
					End If
					If Not info.IsModifiable Then
						dgvParts.Rows(rowIdx).DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245)
						dgvParts.Rows(rowIdx).DefaultCellStyle.ForeColor = System.Drawing.Color.Gray
					End If
				Next

				If duplicateCount > 0 Then
					lblDuplicateWarning.Text = String.Format("检测到 {0} 个同名零部件（不同路径相同文件名），已在列表中用红色标记，请核对避免混淆！", duplicateCount)
				Else
					lblDuplicateWarning.Text = "未检测到同名零部件冲突"
					lblDuplicateWarning.ForeColor = System.Drawing.Color.Green
				End If

				lblStatus.Text = String.Format("共加载 {0} 个零部件，设备套数 = {1}", partsList.Count, deviceQty)
			Catch ex As Exception
				System.Windows.Forms.MessageBox.Show("加载数据时出错：" & ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
				lblStatus.Text = "加载失败"
			Finally
				Me.Cursor = System.Windows.Forms.Cursors.Default
			End Try
		End Sub

		' ==================== 事件处理 ====================
		Private Sub NumDeviceQty_ValueChanged(sender As Object, e As System.EventArgs)
			LoadPartsData()
		End Sub

		Private Sub BtnRefresh_Click(sender As Object, e As System.EventArgs)
			LoadPartsData()
		End Sub

		Private Sub BtnClose_Click(sender As Object, e As System.EventArgs)
			Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.Close()
		End Sub

		Private Sub DgvParts_SelectionChanged(sender As Object, e As System.EventArgs)
			If dgvParts.SelectedRows.Count > 0 Then
				Dim idx As Integer = dgvParts.SelectedRows(0).Index
				If idx >= 0 AndAlso idx < partsList.Count Then
					lblStatus.Text = String.Format("已选中: {0} | 路径: {1}", partsList(idx).FileName, partsList(idx).FullPath)
				End If
			End If
		End Sub

		Private Sub DgvParts_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
			If e.RowIndex >= 0 Then
				BtnHighlight_Click(Nothing, Nothing)
			End If
		End Sub

		' ==================== 亮显功能 ====================
		Private Sub BtnHighlight_Click(sender As Object, e As System.EventArgs)
			If dgvParts.SelectedRows.Count = 0 Then
				System.Windows.Forms.MessageBox.Show("请先在列表中选中要亮显的零部件！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
				Return
			End If

			Dim idx As Integer = dgvParts.SelectedRows(0).Index
			If idx < 0 OrElse idx >= partsList.Count Then Return

			Dim info As PartInfo = partsList(idx)
			Try
				lblStatus.Text = "正在亮显: " & info.FileName & " ..."
				Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

				openDoc.SelectSet.Clear()

				If info.FullPath = openDoc.FullFileName Then
					inventorApp.ActiveView.Fit()
					lblStatus.Text = "已定位到装配体根文档"
					Return
				End If

				Dim selectedCount As Integer = 0
				If info.OccurrenceList.Count > 0 Then
					For Each occ As Inventor.ComponentOccurrence In info.OccurrenceList
						Try
							openDoc.SelectSet.Select(occ)
							selectedCount = selectedCount + 1
						Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
						End Try
					Next
				End If

				If selectedCount > 0 Then
					inventorApp.ActiveView.Fit()
					lblStatus.Text = String.Format("已亮显 [{0}]，共选中 {1} 个实例", info.FileName, selectedCount)
				Else
					System.Windows.Forms.MessageBox.Show("未能在装配树中找到该零部件的引用实例，可能为虚拟组件或已被抑制。", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
					lblStatus.Text = "亮显失败：未找到引用实例"
				End If
			Catch ex As Exception
				System.Windows.Forms.MessageBox.Show("亮显时出错：" & ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
				lblStatus.Text = "亮显出错"
			Finally
				Me.Cursor = System.Windows.Forms.Cursors.Default
			End Try
		End Sub

		' ==================== 写入属性 ====================
		Private Sub BtnApply_Click(sender As Object, e As System.EventArgs)
			If chkPreviewOnly.Checked Then
				System.Windows.Forms.MessageBox.Show("当前为【仅预览模式】，请取消勾选 仅预览，不写入属性 后再执行写入。", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
				Return
			End If

			Dim deviceQty As Integer = CInt(numDeviceQty.Value)
			Dim writeCount As Integer = 0
			Dim skipCount As Integer = 0
			Dim errorCount As Integer = 0

			Try
				lblStatus.Text = "正在写入属性..."
				Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
				progressBar.Visible = True
				progressBar.Maximum = partsList.Count
				progressBar.Value = 0

				For Each info As PartInfo In partsList
					progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum)
					If info.FullPath = openDoc.FullFileName Then
						Continue For
					End If

					If Not info.IsModifiable Then
						skipCount = skipCount + 1
						Continue For
					End If

					Try
						Dim doc As Inventor.Document = Nothing
						For Each d As Inventor.Document In openDoc.AllReferencedDocuments
							If d.FullFileName = info.FullPath Then
								doc = d
								Exit For
							End If
						Next
						If doc Is Nothing Then
							errorCount = errorCount + 1
							Continue For
						End If

						Dim singleQty As Integer = info.SingleQty
						Dim prodQty As Integer = singleQty * deviceQty

						SetCustomProperty(doc, oBOMQTY, singleQty)
						SetCustomProperty(doc, oBOMORDERQTY, prodQty)
						SetCustomProperty(doc, oNORDER, deviceQty)
						writeCount = writeCount + 1
					Catch ex As Exception
						errorCount = errorCount + 1
					End Try
				Next

				Try
					Dim assyInfo As PartInfo = partsList(partsList.Count - 1)
					If assyInfo.FullPath = openDoc.FullFileName AndAlso assyInfo.IsModifiable Then
						SetCustomProperty(openDoc, oBOMQTY, 1)
						SetCustomProperty(openDoc, oBOMORDERQTY, deviceQty)
						SetCustomProperty(openDoc, oNORDER, deviceQty)
						writeCount = writeCount + 1
					End If
				Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
					errorCount = errorCount + 1
				End Try

				Dim msg As String = String.Format("写入完成！成功: {0} 个, 跳过(只读): {1} 个, 失败: {2} 个", writeCount, skipCount, errorCount)
				System.Windows.Forms.MessageBox.Show(msg, "完成", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
				lblStatus.Text = String.Format("写入完成 | 成功:{0} 跳过:{1} 失败:{2}", writeCount, skipCount, errorCount)

				LoadPartsData()
			Catch ex As Exception
				System.Windows.Forms.MessageBox.Show("写入属性时出错：" & ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
				lblStatus.Text = "写入失败"
			Finally
				Me.Cursor = System.Windows.Forms.Cursors.Default
				progressBar.Visible = False
			End Try
		End Sub
	End Class


	' ═══════════════════════════════════════════════════════
	'  TETRIS - Neon Edition
	'  Left/Right Move  Down Soft Drop  SPACE Rotate  Up Hard Drop  P Pause  R Restart
	' ═══════════════════════════════════════════════════════
	Sub ShowTetrisGame()
		Try
			Const COLS As Integer = 10
			Const ROWS As Integer = 20
			Const CELL As Integer = 30

			Dim f As New System.Windows.Forms.Form()
			f.Text = "TETRIS"
			f.ClientSize = New System.Drawing.Size(COLS * CELL + 200, ROWS * CELL + 70)
			f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			f.MaximizeBox = False
			f.BackColor = System.Drawing.Color.FromArgb(12, 12, 30)
			f.KeyPreview = True
			' 自绘 T 形图标（标题栏左上角）
			' 自绘 T 形图标（标题栏左上角）
			Try
				Dim iconBmp As New System.Drawing.Bitmap(32, 32, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
				Dim gb As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(iconBmp)
				gb.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				gb.Clear(System.Drawing.Color.Transparent)
				Dim tb As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 220, 255))
				gb.FillRectangle(tb, 4, 6, 24, 8)
				gb.FillRectangle(tb, 12, 13, 8, 14)
				tb.Dispose()
				gb.Dispose()
				f.Icon = System.Drawing.Icon.FromHandle(iconBmp.GetHicon())
			Catch ' 忽略：iLogic环境下此操作可能失败，不打断流程
			End Try
			' 双缓冲（DoubleBuffered 是 protected，用反射设置）
			Dim dbProp As System.Reflection.PropertyInfo = f.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Public)
			If dbProp IsNot Nothing Then dbProp.SetValue(f, True, Nothing)

			' Shapes stored as ArrayList of Integer()()
			Dim shapes As New System.Collections.ArrayList()
			shapes.Add(New Integer()() {New Integer() {1, 1, 1, 1}})
			shapes.Add(New Integer()() {New Integer() {1, 1}, New Integer() {1, 1}})
			shapes.Add(New Integer()() {New Integer() {0, 1, 0}, New Integer() {1, 1, 1}})
			shapes.Add(New Integer()() {New Integer() {0, 1, 1}, New Integer() {1, 1, 0}})
			shapes.Add(New Integer()() {New Integer() {1, 1, 0}, New Integer() {0, 1, 1}})
			shapes.Add(New Integer()() {New Integer() {1, 0, 0}, New Integer() {1, 1, 1}})
			shapes.Add(New Integer()() {New Integer() {0, 0, 1}, New Integer() {1, 1, 1}})

			Dim neonColors(6) As System.Drawing.Color
			neonColors(0) = System.Drawing.Color.FromArgb(0, 255, 255)
			neonColors(1) = System.Drawing.Color.FromArgb(255, 230, 0)
			neonColors(2) = System.Drawing.Color.FromArgb(200, 0, 255)
			neonColors(3) = System.Drawing.Color.FromArgb(0, 255, 120)
			neonColors(4) = System.Drawing.Color.FromArgb(255, 30, 80)
			neonColors(5) = System.Drawing.Color.FromArgb(0, 130, 255)
			neonColors(6) = System.Drawing.Color.FromArgb(255, 140, 0)

			Dim board(ROWS - 1, COLS - 1) As Integer
			Dim curType As Integer = 0
			Dim curRot As Integer = 0
			Dim curX As Integer = 0
			Dim curY As Integer = 0
			Dim nextType As Integer = 0
			Dim score As Integer = 0
			Dim lines As Integer = 0
			Dim level As Integer = 1
			Dim gameOver As Boolean = False
			Dim paused As Boolean = False
			Dim flashRows As New System.Collections.Generic.HashSet(Of Integer)()
			Dim tickCount As Integer = 0
			Dim piecesDropped As Integer = 0
			Dim rnd As New System.Random()
			nextType = rnd.Next(7)

			Dim GetShape As System.Func(Of Integer, Integer, Integer()()) = Function(t As Integer, rot As Integer)
																				Dim s As Integer()() = CType(shapes(t), Integer()())
																				Dim cur As Integer()() = s
																				For r As Integer = 1 To rot
																					Dim ch As Integer = cur.Length
																					Dim cw As Integer = cur(0).Length
																					Dim nxt As Integer()() = New Integer(cw - 1)() {}
																					For i As Integer = 0 To cw - 1
																						nxt(i) = New Integer(ch - 1) {}
																						For j As Integer = 0 To ch - 1
																							nxt(i)(j) = cur(ch - 1 - j)(i)
																						Next
																					Next
																					cur = nxt
																				Next
																				Return cur
																			End Function

			Dim Collides As System.Func(Of Integer, Integer, Integer, Boolean) = Function(x As Integer, y As Integer, rot As Integer)
																					 Dim sh As Integer()() = GetShape(curType, rot)
																					 For i As Integer = 0 To sh.Length - 1
																						 For j As Integer = 0 To sh(i).Length - 1
																							 If sh(i)(j) = 0 Then Continue For
																							 Dim bx As Integer = x + j
																							 Dim By As Integer = y + i
																							 If bx < 0 OrElse bx >= COLS OrElse By >= ROWS Then Return True
																							 If By >= 0 AndAlso board(By, bx) <> 0 Then Return True
																						 Next
																					 Next
																					 Return False
																				 End Function

			Dim SpawnNew As System.Action = Sub()
												curType = nextType
												nextType = rnd.Next(7)
												curRot = 0
												Dim sh As Integer()() = GetShape(curType, 0)
												curX = (COLS - sh(0).Length) \ 2
												curY = 0
												If Collides(curX, curY, curRot) Then gameOver = True
											End Sub

			Dim LockPiece As System.Action = Sub()
												 piecesDropped += 1
												 Dim sh As Integer()() = GetShape(curType, curRot)
												 For i As Integer = 0 To sh.Length - 1
													 For j As Integer = 0 To sh(i).Length - 1
														 If sh(i)(j) = 0 Then Continue For
														 Dim By As Integer = curY + i
														 Dim bx As Integer = curX + j
														 If By >= 0 AndAlso By < ROWS AndAlso bx >= 0 AndAlso bx < COLS Then
															 board(By, bx) = curType + 1
														 End If
													 Next
												 Next
												 For y As Integer = 0 To ROWS - 1
													 Dim full As Boolean = True
													 For x As Integer = 0 To COLS - 1
														 If board(y, x) = 0 Then full = False : Exit For
													 Next
													 If full Then flashRows.Add(y)
												 Next
												 If flashRows.Count > 0 Then
													 Dim n As Integer = flashRows.Count
													 score += n * 100 * level
													 lines += n
													 level = 1 + lines \ 10
													 Dim flashTimer As New System.Windows.Forms.Timer()
													 flashTimer.Interval = 200
													 AddHandler flashTimer.Tick, Sub(ss As Object, ee As System.EventArgs)
																					 flashTimer.Stop() : flashTimer.Dispose()
																					 Dim sortedRows As New System.Collections.Generic.List(Of Integer)(flashRows)
																					 sortedRows.Sort()
																					 For Each y As Integer In sortedRows
																						 For yy As Integer = y To 1 Step -1
																							 For x As Integer = 0 To COLS - 1
																								 board(yy, x) = board(yy - 1, x)
																							 Next
																						 Next
																						 For x As Integer = 0 To COLS - 1 : board(0, x) = 0 : Next
																					 Next
																					 flashRows.Clear()
																					 SpawnNew()
																					 f.Invalidate()
																				 End Sub
													 flashTimer.Start()
												 Else
													 SpawnNew()
												 End If
												 f.Invalidate()
											 End Sub

			Dim TryMove As System.Action(Of Integer, Integer) = Sub(dx As Integer, dy As Integer)
																	If gameOver OrElse paused Then Return
																	If Not Collides(curX + dx, curY + dy, curRot) Then
																		curX += dx : curY += dy
																	ElseIf dy > 0 Then
																		LockPiece()
																	End If
																	f.Invalidate()
																End Sub

			Dim TryRotate As System.Action = Sub()
												 If gameOver OrElse paused Then Return
												 Dim nr As Integer = (curRot + 1) Mod 4
												 For Each kick As Integer In New Integer() {0, -1, 1, -2, 2}
													 If Not Collides(curX + kick, curY, nr) Then
														 curRot = nr : curX += kick : Exit Sub
													 End If
												 Next
											 End Sub

			Dim HardDrop As System.Action = Sub()
												If gameOver OrElse paused Then Return
												Do Until Collides(curX, curY + 1, curRot)
													curY += 1 : score += 2
												Loop
												LockPiece()
												f.Invalidate()
											End Sub

			SpawnNew()

			AddHandler f.Paint, Sub(sender As Object, e As System.Windows.Forms.PaintEventArgs)
									Dim g As System.Drawing.Graphics = e.Graphics
									g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
									g.Clear(System.Drawing.Color.FromArgb(12, 12, 30))
									' === 左上角 TETRIS Logo ===
									Dim logoCell As Integer = 12
									Dim logoX As Integer = 15
									Dim logoY As Integer = 8
									Dim logoDefs As Integer()()() = New Integer()()() {
				New Integer()() {New Integer() {0, 1, 0}, New Integer() {1, 1, 1}},
				New Integer()() {New Integer() {1, 1}, New Integer() {1, 1}},
				New Integer()() {New Integer() {0, 0, 1}, New Integer() {1, 1, 1}},
				New Integer()() {New Integer() {1, 1, 0}, New Integer() {0, 1, 1}}
			}
									Dim logoCol As Integer() = New Integer() {200, 0, 255, 0, 255, 120, 255, 30, 80, 0, 130, 255}
									For li As Integer = 0 To 3
										Dim shape As Integer()() = logoDefs(li)
										Dim bx0 As Integer = logoX + li * (logoCell * 3 + 4)
										For ri As Integer = 0 To shape.Length - 1
											For ci As Integer = 0 To shape(ri).Length - 1
												If shape(ri)(ci) = 0 Then Continue For
												Dim lrc As New System.Drawing.Rectangle(bx0 + ci * logoCell, logoY + ri * logoCell, logoCell - 1, logoCell - 1)
												Dim lc As System.Drawing.Color = System.Drawing.Color.FromArgb(logoCol(li * 3), logoCol(li * 3 + 1), logoCol(li * 3 + 2))
												Using lb As New System.Drawing.SolidBrush(lc)
													g.FillRectangle(lb, lrc)
												End Using
												Using lp As New System.Drawing.Pen(System.Drawing.Color.White, 1.0!)
													g.DrawRectangle(lp, lrc)
												End Using
											Next
										Next
									Next
									Dim boardX As Integer = 15
									Dim boardY As Integer = 45

									Using bgBrush As New System.Drawing.Drawing2D.LinearGradientBrush(
				New System.Drawing.Rectangle(boardX, boardY, COLS * CELL, ROWS * CELL),
				System.Drawing.Color.FromArgb(20, 20, 50), System.Drawing.Color.FromArgb(40, 10, 60), 90.0!)
										g.FillRectangle(bgBrush, boardX, boardY, COLS * CELL, ROWS * CELL)
									End Using

									Using gridPen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(40, 60, 120))
										For x As Integer = 0 To COLS
											g.DrawLine(gridPen, boardX + x * CELL, boardY, boardX + x * CELL, boardY + ROWS * CELL)
										Next
										For y As Integer = 0 To ROWS
											g.DrawLine(gridPen, boardX, boardY + y * CELL, boardX + COLS * CELL, boardY + y * CELL)
										Next
									End Using

									' Draw one block
									Dim drawBlk As System.Action(Of Integer, Integer, System.Drawing.Color) = Sub(px As Integer, py As Integer, c As System.Drawing.Color)
																												  Dim rc As New System.Drawing.Rectangle(boardX + px * CELL + 2, boardY + py * CELL + 2, CELL - 4, CELL - 4)
																												  Using glow As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(60, c))
																													  g.FillRectangle(glow, rc.X - 3, rc.Y - 3, rc.Width + 6, rc.Height + 6)
																												  End Using
																												  Using br As New System.Drawing.SolidBrush(c)
																													  g.FillRectangle(br, rc)
																												  End Using
																												  Using hi As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(180, 255, 255, 255))
																													  g.FillRectangle(hi, rc.X + 3, rc.Y + 3, rc.Width - 6, 5)
																												  End Using
																												  Using bp As New System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 255, 255, 255), 1.5!)
																													  g.DrawRectangle(bp, rc)
																												  End Using
																											  End Sub

									For y As Integer = 0 To ROWS - 1
										For x As Integer = 0 To COLS - 1
											If board(y, x) = 0 Then Continue For
											Dim col As System.Drawing.Color = neonColors(board(y, x) - 1)
											If flashRows.Contains(y) AndAlso (tickCount \ 4) Mod 2 = 0 Then col = System.Drawing.Color.White
											drawBlk(x, y, col)
										Next
									Next

									If Not gameOver Then
										Dim curShape As Integer()() = GetShape(curType, curRot)
										Dim cc As System.Drawing.Color = neonColors(curType)
										For i As Integer = 0 To curShape.Length - 1
											For j As Integer = 0 To curShape(i).Length - 1
												If curShape(i)(j) = 0 Then Continue For
												Dim bx As Integer = curX + j
												Dim By As Integer = curY + i
												If By >= 0 Then drawBlk(bx, By, cc)
											Next
										Next
									End If

									Dim infoX As Integer = boardX + COLS * CELL + 20
									Dim fnt As System.Drawing.Font = MakeFontBold(CStr(g_fontName), 11.0!)
									Dim fntTitle As System.Drawing.Font = MakeFontBold(CStr(g_fontName), 15.0!)
									Dim titleBrush As System.Drawing.Brush = System.Drawing.Brushes.Cyan
									If g_theme = 1 Then titleBrush = System.Drawing.Brushes.DeepSkyBlue
									If g_theme = 2 Then titleBrush = System.Drawing.Brushes.MediumSeaGreen
									g.DrawString("TETRIS", fntTitle, titleBrush, CSng(infoX), 20.0!)
									g.DrawString("-------", fnt, System.Drawing.Brushes.Gray, CSng(infoX), 50.0!)
									g.DrawString("Score: " & score, fnt, System.Drawing.Brushes.White, CSng(infoX), 72.0!)
									g.DrawString("Lines: " & lines, fnt, System.Drawing.Brushes.White, CSng(infoX), 95.0!)
									g.DrawString("Level: " & level, fnt, System.Drawing.Brushes.Yellow, CSng(infoX), 118.0!)
									g.DrawString("-------", fnt, System.Drawing.Brushes.Gray, CSng(infoX), 140.0!)
									g.DrawString("Next:", fnt, System.Drawing.Brushes.LightGreen, CSng(infoX), 158.0!)

									Dim ns As Integer()() = GetShape(nextType, 0)
									Dim nc As System.Drawing.Color = neonColors(nextType)
									Dim previewSize As Integer = 22
									Dim pStartX As Integer = infoX + 10
									Dim pStartY As Integer = 190
									For i As Integer = 0 To ns.Length - 1
										For j As Integer = 0 To ns(i).Length - 1
											If ns(i)(j) = 0 Then Continue For
											Dim pr As New System.Drawing.Rectangle(pStartX + j * previewSize, pStartY + i * previewSize, previewSize - 3, previewSize - 3)
											Using br As New System.Drawing.SolidBrush(nc) : g.FillRectangle(br, pr) : End Using
											Using bp As New System.Drawing.Pen(System.Drawing.Color.White, 1.0!) : g.DrawRectangle(bp, pr) : End Using
										Next
									Next

									g.DrawString("-------", fnt, System.Drawing.Brushes.Gray, CSng(infoX), 280.0!)
									g.DrawString("Left/Right Move", fnt, System.Drawing.Brushes.LightGray, CSng(infoX), 300.0!)
									g.DrawString("Down Soft Drop", fnt, System.Drawing.Brushes.LightGray, CSng(infoX), 322.0!)
									g.DrawString("UP Rotate", fnt, System.Drawing.Brushes.LightGreen, CSng(infoX), 344.0!)
									g.DrawString("Space Hard Drop", fnt, System.Drawing.Brushes.LightGray, CSng(infoX), 366.0!)
									g.DrawString("P Pause", fnt, System.Drawing.Brushes.LightGray, CSng(infoX), 388.0!)
									g.DrawString("R Restart", fnt, System.Drawing.Brushes.LightGray, CSng(infoX), 410.0!)
									fnt.Dispose()
									fntTitle.Dispose()

									If gameOver Then
										Using dimBrush As New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(180, 0, 0, 0))
											g.FillRectangle(dimBrush, boardX, boardY, COLS * CELL, ROWS * CELL)
										End Using
										Dim fntGo As System.Drawing.Font = MakeFontBold(CStr(g_fontName), 22.0!)
										Dim fntSm As System.Drawing.Font = MakeFont(CStr(g_fontName), 10.0!)
										Dim msg As String = "GAME OVER"
										Dim sz As System.Drawing.SizeF = g.MeasureString(msg, fntGo)
										g.DrawString(msg, fntGo, System.Drawing.Brushes.Red, CSng(boardX + (COLS * CELL - sz.Width) / 2.0!), CSng(boardY + ROWS * CELL / 2 - 40))
										Dim msg2 As String = "Press R to restart"
										Dim sz2 As System.Drawing.SizeF = g.MeasureString(msg2, fntSm)
										g.DrawString(msg2, fntSm, System.Drawing.Brushes.White, CSng(boardX + (COLS * CELL - sz2.Width) / 2.0!), CSng(boardY + ROWS * CELL / 2 + 10))
										fntGo.Dispose()
										fntSm.Dispose()
									End If

									If paused AndAlso Not gameOver Then
										Dim fntP As System.Drawing.Font = MakeFontBold(CStr(g_fontName), 18.0!)
										Dim msg As String = "PAUSED"
										Dim sz As System.Drawing.SizeF = g.MeasureString(msg, fntP)
										g.DrawString(msg, fntP, System.Drawing.Brushes.Yellow, CSng(boardX + (COLS * CELL - sz.Width) / 2.0!), CSng(boardY + ROWS * CELL / 2 - 20))
										fntP.Dispose()
									End If
								End Sub

			AddHandler f.KeyDown, Sub(sender As Object, e As System.Windows.Forms.KeyEventArgs)
									  If gameOver Then
										  If e.KeyCode = System.Windows.Forms.Keys.R Then
											  For y As Integer = 0 To ROWS - 1
												  For x As Integer = 0 To COLS - 1 : board(y, x) = 0 : Next
											  Next
											  score = 0 : lines = 0 : level = 1 : gameOver = False : paused = False : piecesDropped = 0
											  flashRows.Clear()
											  nextType = rnd.Next(7)
											  SpawnNew()
										  End If
										  f.Invalidate() : e.SuppressKeyPress = True
										  Return
									  End If
									  Select Case e.KeyCode
										  Case System.Windows.Forms.Keys.Left : TryMove(-1, 0)
										  Case System.Windows.Forms.Keys.Right : TryMove(1, 0)
										  Case System.Windows.Forms.Keys.Down : TryMove(0, 1) : score += 1
										  Case System.Windows.Forms.Keys.Up : TryRotate()
										  Case System.Windows.Forms.Keys.Space : HardDrop()
										  Case System.Windows.Forms.Keys.P : paused = Not paused : f.Invalidate()
										  Case System.Windows.Forms.Keys.R
											  For y As Integer = 0 To ROWS - 1
												  For x As Integer = 0 To COLS - 1 : board(y, x) = 0 : Next
											  Next
											  score = 0 : lines = 0 : level = 1 : gameOver = False : paused = False : piecesDropped = 0
											  flashRows.Clear()
											  nextType = rnd.Next(7)
											  SpawnNew()
									  End Select
									  e.SuppressKeyPress = True
								  End Sub

			Dim timer As New System.Windows.Forms.Timer()
			timer.Interval = 500
			AddHandler timer.Tick, Sub(sender As Object, e As System.EventArgs)
									   tickCount += 1
									   If flashRows.Count > 0 Then f.Invalidate() : Return
									   If gameOver OrElse paused Then Return
									   If Not Collides(curX, curY + 1, curRot) Then
										   curY += 1
									   Else
										   LockPiece()
									   End If
									   f.Invalidate()
									   Dim speedMult As Double = 1.0 + (piecesDropped \ 10) * 0.2
									   Dim newInterval As Integer = System.Math.Max(80, CInt(500.0 / speedMult))
									   If newInterval <> timer.Interval Then timer.Interval = newInterval
								   End Sub
			timer.Start()

			AddHandler f.FormClosed, Sub(sender As Object, e As System.Windows.Forms.FormClosedEventArgs)
										 timer.Stop() : timer.Dispose()
									 End Sub

			f.ShowDialog()
		Catch ex As Exception
			System.Windows.Forms.MessageBox.Show("Tetris error: " & ex.Message & vbCrLf & ex.StackTrace, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
		End Try
	End Sub


End Class
