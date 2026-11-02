
Imports Inventor
Imports Inventor.DocumentTypeEnum
Imports Inventor.SelectionFilterEnum
Imports Microsoft
Imports Microsoft.VisualBasic
Imports Microsoft.Win32
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Net
Imports System
Imports System.Timers

Namespace InventorAddIn

    ' .NET 8 的 COM 宿主（comhost）只识别显式标记为 COM 可见的类型，
    ' .NET Framework 时代默认可见，升级后必须显式声明，否则 Inventor 创建实例会失败
    <ComVisible(True)>
    <ClassInterface(ClassInterfaceType.None)>
    <ProgIdAttribute("InventorAddIn.StandardAddInServer")>
    <GuidAttribute("4D6D2EAC-11B9-4BB1-9244-86AB8D5F8014")>
    Public Class StandardAddInServer
        Implements Inventor.ApplicationAddInServer

        Private ReadOnly WithEventsMenu_uiEvents As UserInterfaceEvents
        Private ReadOnly WithEventsMenu_userInputEvents As UserInputEvents

        'Private WithEvents m_timer As System.Timers.Timer    '计时器

#Region "定义按钮"

        '============================================================
        '
        Private WithEvents Menu_ERP反查_Buttondef As ButtonDefinition
        Private WithEvents Menu_按列表打开文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_帮助_Buttondef As ButtonDefinition
        Private WithEvents Menu_保存关闭_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_保存为列表_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_标记展开图螺纹_Buttondef As ButtonDefinition
        Private WithEvents Menu_标准件可见性_Buttondef As ButtonDefinition
        Private WithEvents Menu_修改序号_Buttondef As ButtonDefinition
        Private WithEvents Menu_查询ERP编码_Buttondef As ButtonDefinition
        Private WithEvents Menu_查找缺失文件的部件_Buttondef As ButtonDefinition
        Private WithEvents Menu_查找替换_Buttondef As ButtonDefinition
        Private WithEvents Menu_尺寸居中_Buttondef As ButtonDefinition
        Private WithEvents Menu_尺寸圆整_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_创建工程图_Buttondef As ButtonDefinition
        Private WithEvents Menu_创建工艺_Buttondef As ButtonDefinition
        Private WithEvents Menu_创建截屏_Buttondef As ButtonDefinition
        Private WithEvents Menu_创建展开图_Buttondef As ButtonDefinition
        Private WithEvents Menu_打开ERP数据文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_工程图_Buttondef As ButtonDefinition
        Private WithEvents Menu_打开全部工程图_Buttondef As ButtonDefinition
        Private WithEvents Menu_打开文件夹_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_打开快捷管理_Buttondef As ButtonDefinition
        Private WithEvents Menu_打开指定工程图_Buttondef As ButtonDefinition
        Private WithEvents Menu_导出平面BOMenu_Buttondef As ButtonDefinition
        Private WithEvents Menu_导出为Dwg_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_导出为Dxf_Buttondef As ButtonDefinition
        Private WithEvents Menu_导出为Pdf_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_导出为Stp_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_导入ERP编码到模型_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_导入ERP到BOMenu_Buttondef As ButtonDefinition
        Private WithEvents Menu_动态尺寸_Buttondef As ButtonDefinition
        Private WithEvents Menu_对齐原始坐标面_Buttondef As ButtonDefinition
        Private WithEvents Menu_复制插入_Buttondef As ButtonDefinition
        Private WithEvents Menu_格式转换_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_关闭文档_Buttondef As ButtonDefinition
        Private WithEvents Menu_关于_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_还原旧版文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_技术要求_Buttondef As ButtonDefinition
        Private WithEvents Menu_检查钣金厚度_Buttondef As ButtonDefinition
        Private WithEvents Menu_检查是否有工程图_Buttondef As ButtonDefinition
        Private WithEvents Menu_检查序号完整性_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_居中对齐_Buttondef As ButtonDefinition
        Private WithEvents Menu_快速打开_Buttondef As ButtonDefinition
        Private WithEvents Menu_快速打印_Buttondef As ButtonDefinition
        Private WithEvents Menu_量产iProperty_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_另存为副本_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_批量打印_Buttondef As ButtonDefinition
        Private WithEvents Menu_批量重命名_Buttondef As ButtonDefinition
        Private WithEvents Menu_批量BOM重命名_buttondef As ButtonDefinition
        Private WithEvents Menu_切换文档_Buttondef As ButtonDefinition
        Private WithEvents Menu_清除签字_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_清除日期_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_清除随机颜色_Buttondef As ButtonDefinition
        Private WithEvents Menu_错误清理_Buttondef As ButtonDefinition
        Private WithEvents Menu_清理旧版文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_驱动测量_Buttondef As ButtonDefinition
        Private WithEvents Menu_全部保存_Buttondef As ButtonDefinition
        Private WithEvents Menu_全部尺寸居中_Buttondef As ButtonDefinition
        Private WithEvents Menu_全部另存为_Buttondef As ButtonDefinition
        Private WithEvents Menu_设值随机颜色_Buttondef As ButtonDefinition
        Private WithEvents Menu_设置BOM结构_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_设置比例_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_设置对称件iProperty_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_设置签字_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_设置日期_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_设置只读_Buttondef As ButtonDefinition
        Private WithEvents Menu_输入查询ERP编码_Buttondef As ButtonDefinition
        Private WithEvents Menu_刷新引用_Buttondef As ButtonDefinition
        Private WithEvents Menu_提取iPro重命名_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_替换图框标题栏_Buttondef As ButtonDefinition
        Private WithEvents Menu_替换为库文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_替换衍生_Buttondef As ButtonDefinition
        Private WithEvents Menu_添加直径_Buttondef As ButtonDefinition
        Private WithEvents Menu_统计_Buttondef As ButtonDefinition
        Private WithEvents Menu_文件只读_Buttondef As ButtonDefinition
        Private WithEvents Menu_项目iProperty_Buttondef As ButtonDefinition
        Private WithEvents Menu_新建序号_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_修改部件iProperty_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_修改文件iProperty_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_选项_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_移动指定文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_抑制全部错误的约束_Buttondef As ButtonDefinition
        Private WithEvents Menu_重命名_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_重命名镜像文件_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_重写BOM序号_Buttondef As ButtonDefinition
        Private WithEvents Menu_自定义iProperty_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_自定义签字_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_自定义日期_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_自动生成图号_Buttondef As Inventor.ButtonDefinition
        Private WithEvents Menu_自动重建序号_Buttondef As ButtonDefinition
        Private WithEvents Menu_快捷管理_Buttondef As ButtonDefinition
        Private WithEvents Menu_色卡_Buttondef As ButtonDefinition
        Private WithEvents Menu_孔着色_Buttondef As ButtonDefinition
        Private WithEvents Menu_面着色_Buttondef As ButtonDefinition
        Private WithEvents Menu_清理冗余文件_Buttondef As ButtonDefinition
        Private WithEvents Menu_设置基础数量_Buttondef As ButtonDefinition
        Private WithEvents Menu_复制材料_Buttondef As ButtonDefinition
        Private WithEvents Menu_插入零部件_Buttondef As ButtonDefinition
        Private WithEvents Menu_BOM_Buttondef As ButtonDefinition

        ' BOM 工具：调用 clsBomTool.Run()（代码自建界面，伪模态直到窗体关闭）
        '================================================================================

#End Region

        ' Inventor application object.
        'Private   ThisApplication As Inventor.Application


        Public Sub Activate(ByVal addInSiteObject As Inventor.ApplicationAddInSite, ByVal firstTime As Boolean) Implements Inventor.ApplicationAddInServer.Activate

            Try

                ' This method is called by Inventor when it loads the AddIn.
                ' The AddInSiteObject provides access to the Inventor Application object.
                ' The FirstTime flag indicates if the AddIn is loaded for the first time.
                'On Error Resume Next

                ' Initialize AddIn members.
                ThisApplication = addInSiteObject.Application

                '打开文件时事件
                ThisApplicationEvents = ThisApplication.ApplicationEvents
                AddHandler ThisApplicationEvents.OnOpenDocument, AddressOf ThisApplicationEvents_OnOpenDocument
                AddHandler ThisApplicationEvents.OnActivateDocument, AddressOf ThisApplicationEvents_OnActivateDocument
                AddHandler ThisApplicationEvents.OnSaveDocument, AddressOf ThisApplicationEvents_OnSaveDocument

                '创建右键菜单-全部可见
                Dim buttonRightMouseShowAll As New ClsRightMouseShowAll

                '创建右键菜单-设置图纸大小
                Dim buttonRightMouseSetDrawingSize As New ClsRightMouseSetDrawingSize

                '创建右键菜单-关闭自适应
                Dim buttonRightMouseTurnOffAdaptivity As New ClsRightMouseTurnOffAdaptivity

                '创建右键菜单-阵列尺寸
                Dim buttonRightMouseZhenlieChicun As New ClsRightMouseZhenlieChicun

                '创建右键菜单-打开父部件
                Dim buttonRightMouseOpenParentAssembly As New ClsRightMouseOpenParentAssembly

                '创建右键菜单-打开工程图（零部件中未选中打开当前、选中则打开选中的）
                Dim buttonRightMouseOpenDrawing As New ClsRightMouseOpenDrawing

                '创建右键菜单-选择线全选零件
                Dim buttonSelectPartCurveInDrawing As New ClsRightMouseSelectPartCurveInDrawing

                '创建右键菜单-在浏览器中查找  在工程图选中边，在浏览器中选择对应的零件
                Dim buttonSelectPartInBrowser As New ClsRightMouseSelectPartInBrowser

                '创建右键菜单-打开选择的组件
                'Dim buttonOpenSelectComponentOccurrences As New
                '
                '创建右键菜单-重命名
                'Dim buttonOpenSelectComponentOccurrences As New ClsRightMouseOpenSelectComponentOccurrences

                '创建右键菜单-切换选择
                Dim buttonChangeSelectionPriority As New ClsRightMouseChangeSelectionPriority


                '当前inventor 版本
                DisplayVersion = ThisApplication.SoftwareVersion.DisplayVersion

                '初始化零件库
                ContentCenterFiles = ThisApplication.FileOptions.ContentCenterPath
                'MsgBox(ContentCenterFiles)

                '初始化模型匹配检查标记
                str模型匹配检查标记 = 1




                IniFile = IO.Path.Combine(My.Application.Info.DirectoryPath, "ConfigFile.ini")
                If IsFileExists(IniFile) = False Then
                    '初始化默认值
                    'WrXml.ConfigFileDefaultValue()

                    '获取自定义值
                    'WrXml.ConfigFileXmlReadSetting()

                    WrIni.ConfigFileIniWriteSetting()
                End If

                WrIni.ConfigFileIniReadSetting()

                '时间计数器
                m_timer = New System.Timers.Timer
                m_timer.Interval = Integer.Parse(str保存间隔时间 * 60 * 1000)
                m_timer.AutoReset = True
                m_timer.Start()


                ' 检查更新挪到下方后台任务执行，避免拖慢 Activate

                'NewUpdater.UpDater2(False)

                ''IsShowUpdateMsg = False
                ''Dim frmupdate As New frmUpdate
                ''frmupdate.Visible = False
                ''frmupdate.ShowDialog()

                ''If NewUpdater.CreateUpdate() = True Then

                'If NewUpdater.GetGitVersion = "New" Then
                '    'If MsgBox("检查到InAI新版：" & NewVersion & "，是否下载？", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "更新") = MsgBoxResult.Yes Then
                '    '    'Process.Start("https://github.com/leaky114/InAI/tree/master/Release")
                '    '    'Process.Start("https://www.aliyundrive.com/s/C3CerE58Fkn")

                '    '    Dim strUpdaterForInAI As String
                '    '    strUpdaterForInAI = My.Application.Info.DirectoryPath & "\UpdaterForInAI.exe"

                '    '    If IsFileExIsts(strUpdaterForInAI) = False Then
                '    '        Dim strNetUpdaterForInAI As String = ""
                '    '        DownNetFile(strNetUpdaterForInAI, strUpdaterForInAI)
                '    '    End If

                '    '    Process.Start(strUpdaterForInAI)
                '    'End If
                'End If

                'If NewUpdater.CheckNewVesion = "New" Then

                '    '释放更新程序
                '    'NewUpdater.CreateUpdateExe()

                '    NewUpdater.UpDate3()

                'End If


                '本地路径准备（瞬时，保持同步；后续菜单仍要用到这两个路径）
                If IsFileExists(BasicExcelFullFileName) = False Then
                    BasicExcelFullFileName = IO.Path.Combine(My.Application.Info.DirectoryPath, ServerExcelFileName)
                End If
                If BasicExcelFullFileName = "" Then
                    BasicExcelFullFileName = IO.Path.Combine(My.Application.Info.DirectoryPath, ServerExcelFileName)
                End If

                Dim documentURL As String = Server & ServerExcelFileName
                Dim strHelpFullFileName As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "帮助.pdf")

                '更新检查与网络下载改到后台线程：
                '网络不可达时 IsFileExists(UNC) 会阻塞 5 秒以上，拖慢 Activate；
                '调试器附加后整体启动更慢，容易触发 Inventor 插件加载超时而直接退出进程，故异步执行。
                Dim bgExcelUrl As String = documentURL
                Dim bgHelpUrl As String = Server & "帮助.pdf"
                Dim bgExcelPath As String = BasicExcelFullFileName
                Dim bgHelpPath As String = strHelpFullFileName
                Try
                    System.Threading.Tasks.Task.Run(Sub()
                                                        Try
                                                            If CheckUpdate = "1" Then
                                                                NewUpdater.ShellXHUpdater("0")
                                                            End If
                                                            If IsFileExists(bgExcelUrl) = True Then
                                                                Using wc As New System.Net.WebClient
                                                                    wc.DownloadFile(bgExcelUrl, bgExcelPath)
                                                                End Using
                                                            End If
                                                            If IsFileExists(bgHelpUrl) = True Then
                                                                Using wc2 As New System.Net.WebClient
                                                                    wc2.DownloadFile(bgHelpUrl, bgHelpPath)
                                                                End Using
                                                            End If
                                                        Catch exTask As Exception
                                                        End Try
                                                    End Sub)
                Catch exStart As Exception
                End Try

                '创建按钮
                CreatUI(firstTime)


            Catch ex As Exception
                Throw
            End Try

        End Sub


        Public Sub New()
        End Sub

        'Private Sub m_timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles m_timer.Elapsed
        '    AutoSaveDocument()
        'End Sub
        Public Sub CreatUI(ByVal bfirstTime As Boolean)
            On Error Resume Next
            Dim oCommandManager As CommandManager
            Dim oUserInterfaceManager As UserInterfaceManager

            Dim smallPicture As stdole.IPictureDisp
            Dim largePicture As stdole.IPictureDisp

            'Try
            oCommandManager = ThisApplication.CommandManager
            oUserInterfaceManager = ThisApplication.UserInterfaceManager

            'clientId = Me.GetType().GUID.ToString("B")
            ClientID = AddInGuid(GetType(StandardAddInServer))
            '---------------------------------------------------------------------------------------
            'create button's definition here
            'Menu_修改文件iProperty_Buttondef = New 修改文件iProperty_Button("修改文件iProperty", "InName修改文件iProperty", _
            '                       CommandTypesEnum.kShapeEditCmdType, clientId, , , , , )
            '1
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.修改文件iProperty16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.修改文件iProperty32.ToBitmap)

            Menu_修改文件iProperty_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("修改文件iProperty", "GLToolInName修改文件iProperty",
                                  CommandTypesEnum.kShapeEditCmdType, ClientID, , "以第一个汉字或空格为标识，提取文件名中的图号和名称，根据【iProperty映射】的设置，写入【iProperty】【项目】中的字段。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '2
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.修改部件iProperty16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.修改部件iProperty32.ToBitmap)
            Menu_修改部件iProperty_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("修改部件iProperty", "GLToolInName修改部件iProperty",
                                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "根据【BOM表】【结构化】的数据，设置下一级或所有级文件的iProperty。注意：将忽略零件库和参考文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '3
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.更改文件名16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.更改文件名32.ToBitmap)
            Menu_重命名_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("重命名", "GLToolInName更改文件名",
                                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在部件文件中选择一个文件，修改其文件名，将替换选中的文件或全部文件。若在与文件同一文件夹中存在其同名工程图，将生成新的工程图。注意：将忽略零件库。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '4
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.提取iPro修改文件名16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.提取iPro修改文件名32.ToBitmap)
            Menu_提取iPro重命名_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("提取iPro重命名", "GLToolInName提取iPro修改文件名",
                   CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在部件文件中选择一个文件，根据【iProperty映射】设置，提取【iProperty】【项目】中的字段，修改选取文件的文件名。与【修改部件iProperty】反向。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '5
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.生成图号16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.生成图号32.ToBitmap)
            Menu_自动生成图号_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("自动生成图号", "GLToolInName自动生成图号",
                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "根据第一级部件的图号，设置子级部件或零件的图号变化规则，重命名其文件名。对于新设计先命名后图号特别有用。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '6
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.更改镜像文件名16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.更改镜像文件名32.ToBitmap)
            Menu_重命名镜像文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("重命名镜像文件", "GLToolInName更改镜像文件名",
                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在部件中选择一个是镜像生成的文件，修改其文件名，将原基础文件改为临时文件，重新手动指定其基础文件，后还原基础文件。对于基础文件和镜像文件都需修改的文件很有用。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '7
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.关闭文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.关闭文件32.ToBitmap)
            Menu_关闭文档_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("关闭", "GLToolInName关闭当前文档",
                                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "关闭当前文档。 ", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件夹16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件夹32.ToBitmap)
            ' Create the button definition.
            Menu_打开文件夹_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("打开文件夹", "GLToolInName打开文件夹",
                           CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开当前文件所在的文件夹。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '8
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.保存关闭16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.保存关闭32.ToBitmap)
            Menu_保存关闭_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("保存关闭", "GLToolInName保存关闭",
                   CommandTypesEnum.kShapeEditCmdType, ClientID, "", "保存并关闭当前打开的文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '9
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.文件Pdf16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.文件Pdf32.ToBitmap)
            Menu_导出为Pdf_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导出为Pdf", "GLToolInName导出为Pdf",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将当前文件导出为Pdf格式。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '10
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.文件Dwg16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.文件Dwg32.ToBitmap)
            Menu_导出为Dwg_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导出为Dwg", "GLToolInName导出为Dwg",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将当前文件导出为Dwg格式。可能 因为设置的不同而是 Zip 格式的，也将其打开。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '11
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置BOM结构16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置BOM结构32.ToBitmap)
            Menu_设置BOM结构_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("设置虚拟件", "GLToolInName设置BOM结构",
                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在部件中使用，输入序号，将部件的所有子级设置为对应的BOM结构。对与液压管等特别有用。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '12
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.格式转换16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.格式转换32.ToBitmap)
            Menu_自定义iProperty_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("自定义iProperty", "GLToolInName打开自定义iProperty",
                   CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开自定义iProperty窗口", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '13
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.对称件iProperty16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.对称件iProperty32.ToBitmap)
            Menu_设置对称件iProperty_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("对称件iProperty", "GLToolInName对称件iProperty",
                                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "选择一个文件，根据【对称件iProperty映射】的设置，将其iProperty写入【iProperty】【自定义】字段。对于对称件的工程图很有用。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '14
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.检查序号完整性16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.检查序号完整性32.ToBitmap)
            Menu_检查序号完整性_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("检查序号完整性", "GLToolInName检查序号完整性",
                           CommandTypesEnum.kShapeEditCmdType, ClientID, "", "检查工程图的序号是否标注完整。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '15
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.新建序号16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.新建序号32.ToBitmap)
            Menu_新建序号_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("新建序号", "GLToolInName新建序号",
                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "重新对工程图的序号进行变化，使寻找序号更简便。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '16
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置日期16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置日期32.ToBitmap)
            Menu_设置日期_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("设置日期", "GLToolInName设置日期",
                                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "根据【签字】【打印日期】的设置，将当前日期写入【iProperty】【自定义】字段。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '17
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.替换衍生32.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.替换衍生16.ToBitmap)
            Menu_替换衍生_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("替换基础文件", "GLToolInName替换基础文件",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "替换衍生零件的基础文件，请选择一个新的基础文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '18
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.抑制约束16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.抑制约束16.ToBitmap)
            Menu_抑制全部错误的约束_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("抑制错误约束", "GLToolInName自定义日期",
           CommandTypesEnum.kShapeEditCmdType, ClientID, "", " 抑制全部错误的约束", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '19
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置签字16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置签字32.ToBitmap)
            Menu_设置签字_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("设置签字", "GLToolInName设置签字",
                     CommandTypesEnum.kShapeEditCmdType, ClientID, "", "根据【签字】【工程师】的设置，将当前日期写入【iProperty】【自定义】字段。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '20
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.清除签字16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.清除签字32.ToBitmap)
            Menu_清除签字_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("清除签字", "GLToolInName清除签字",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '21
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.自定义签字16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.自定义签字32.ToBitmap)
            Menu_自定义签字_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("自定义签字", "GLToolInName自定义签字",
                  CommandTypesEnum.kShapeEditCmdType, ClientID, "", "", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '22
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置32.ToBitmap)
            Menu_选项_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("选项", "GLToolInName选项",
                     CommandTypesEnum.kShapeEditCmdType, ClientID, "打开【选项】窗口，对插件进行配置。", "", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '23
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.量产iProperty16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.量产iProperty32.ToBitmap)
            Menu_量产iProperty_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("量产iProperty", "GLToolInName量产iProperty",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开【量产iProperty】窗口，对多个文件设置【iProperty】。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '24
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.格式转换16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.格式转换32.ToBitmap)
            Menu_格式转换_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("格式转换", "GLToolInName格式转换",
                 CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开【格式转换】窗口，批量转换Inventor文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '25
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.关于16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.关于32.ToBitmap)
            Menu_关于_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("关于", "GLToolInName关于",
                 CommandTypesEnum.kShapeEditCmdType, ClientID, "", "", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '26
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.全部保存16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.全部保存32.ToBitmap)
            Menu_全部保存_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("全部保存", "GLToolInName全部保存",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "保存并关闭打开的所有部件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '27
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查询16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查询32.ToBitmap)
            Menu_输入查询ERP编码_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("查询ERP编码", "GLToolInName输入查询ERP编码",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "输入图号或名称查询ERP编码。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '28
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.标题栏16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.标题栏32.ToBitmap)
            Menu_替换图框标题栏_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("替换图框标题栏", "GLToolInName替换图框标题栏",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "以Inventor安装文件夹下的 模板.idw 文件为基础，替换图框标题栏。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '29
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.自动重建序号16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.自动重建序号32.ToBitmap)
            Menu_自动重建序号_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("自动重建序号", "GLToolInName自动重建序号",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "按极角排序（环形）自动重建序号。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '30
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建展开16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建展开16.ToBitmap)
            Menu_创建展开图_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("创建展开图", "GLToolInName创建展开图",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在部件中批量或在零件中创建钣金零件展开图，保存到子目录《钣金展开》。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '31
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.刷新浏览器16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.刷新浏览器32.ToBitmap)
            Menu_刷新引用_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("刷新引用", "GLToolInName刷新引用",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "刷新浏览器中的引用名称。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '32
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.清理旧版文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.清理旧版文件32.ToBitmap)
            Menu_清理旧版文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("清理旧版文件", "GLToolInName清理旧版文件",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "扫描指定路径下的旧版文件，删除之。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '33
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.添加直径16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.添加直径32.ToBitmap)
            Menu_添加直径_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("添加直径", "GLToolInName添加直径",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在尺寸前添加 Φ。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '35
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.检查是否有工程图16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.检查是否有工程图32.ToBitmap)
            Menu_检查是否有工程图_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("检查是否有工程图", "GLToolInName检查是否有工程图",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "检查部件中指定图号的文件是否有对应的工程图。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '36
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图32.ToBitmap)
            Menu_打开全部工程图_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("打开全部工程图", "GLToolInName打开全部工程图",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开部件中所有子级对应的工程图。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '37
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.部件替换文件名16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.部件替换文件名32.ToBitmap)
            Menu_批量重命名_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("批量重命名", "GLToolInName批量重命名",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "批量重命名，可替换，添加前缀，后缀。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '38
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导出平面BOM16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导出平面BOM32.ToBitmap)
            Menu_导出平面BOMenu_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导出平面BOM", "GLToolInName导出平面BOM",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "根据BOM导出项目，导出平面性的BOM为Excel文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '39
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.还原旧版文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.还原旧版文件32.ToBitmap)
            Menu_还原旧版文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("还原旧版文件", "GLToolInName还原旧版文件",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "还原更改文件名后的 .old 文件为原始文件名。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '40
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.保存副本16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.保存副本32.ToBitmap)
            Menu_另存为副本_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("另存为副本", "GLToolInName另存为副本",
                                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将激活的文件另存为副本，并链接工程图", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.帮助16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.帮助32.ToBitmap)
            Menu_帮助_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("帮助", "GLToolInName帮助",
                                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开帮助文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '36
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图32.ToBitmap)
            Menu_打开指定工程图_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("打开指定工程图", "GLToolInName打开指定工程图",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开部件中所有包含指定图号子级的对应的工程图。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '41
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.移动指定文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.移动指定文件32.ToBitmap)
            Menu_移动指定文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("移动指定文件", "GLToolInName移动指定文件",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "移到组件中指定前缀的零件和组件到当前组件所在文件夹。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '42
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.对齐原始坐标面16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.对齐原始坐标面32.ToBitmap)
            Menu_对齐原始坐标面_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("对齐坐标系", "GLToolInName对齐原始坐标面",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "选择部件中的两个部件或零件，使其原始坐标面对齐。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '43
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查找缺失文件的部件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查找缺失文件的部件32.ToBitmap)
            Menu_查找缺失文件的部件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("查找缺失文件的部件", "GLToolInName查找缺失文件的部件",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "查找当前部件中缺失的文件，并打开缺失文件的父级部件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '44
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.统计16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.统计32.ToBitmap)
            Menu_统计_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("统计", "GLToolInName统计",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "选择部件中的部件或零件，统计总质量，面积，焊缝。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '45
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.IPro16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.IPro32.ToBitmap)
            Menu_项目iProperty_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("iProperty+", "GLToolInName自定义IPro",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "自定义设置iProperty。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '44:
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建工程图16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建工程图32.ToBitmap)
            Menu_创建工程图_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("创建工程图", "GLToolInName创建工程图",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "创建一个工程图，需要设置一个模板文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '47
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.尺寸圆整16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.尺寸圆整32.ToBitmap)
            Menu_尺寸圆整_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("尺寸圆整", "GLToolInName尺寸圆整",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "四舍五入尺寸小数位到个位。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '48
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导入材料编码16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导入材料编码32.ToBitmap)
            Menu_导入ERP编码到模型_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导入ERP编码到模型", "GLToolInName导入存货编码",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "导入ERP编码。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '49
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置材料编码16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设置材料编码32.ToBitmap)
            Menu_ERP反查_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("ERP编码反查", "GLToolInNameERP反查",
               CommandTypesEnum.kShapeEditCmdType, ClientID, "", "输入ERP编码，查询数据。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '50
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图32.ToBitmap)
            Menu_工程图_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("工程图", "GLToolInName打开工程图",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开当前文件或部件中选择的文件对应的工程图。 ", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '51
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.批量打印16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.批量打印32.ToBitmap)
            Menu_批量打印_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("批量打印", "GLToolInName批量打印",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "批量打印工程图。 ", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '52
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.重写BOM16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.重写BOM32.ToBitmap)
            Menu_重写BOM序号_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("重写BOM序号", "GLToolInName重写BOM序号",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "重写BOM序号并按序号升序排序。 ", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '53:
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查询16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查询32.ToBitmap)
            Menu_查询ERP编码_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("查询ERP编码", "GLToolInName查询ERP编码",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在Excel数据表中查询ERP编码。 ", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '54
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开ERP数据文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开ERP数据文件32.ToBitmap)
            Menu_打开ERP数据文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("打开ERP数据文件", "GLToolInName打开ERP数据文件",
              CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开ERP数据文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '55
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件32.ToBitmap)
            Menu_快速打开_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("快速打开", "GLToolInName快速打开",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在当前项目中快速打开指定文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '56
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导入ERP到BOM16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导入ERP到BOM32.ToBitmap)
            Menu_导入ERP到BOMenu_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导入ERP编码到Excel", "GLToolInName导入ERP到Excel",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "导入ERP编码到Excel文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            '57
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.快速打印16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.快速打印32.ToBitmap)
            Menu_快速打印_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("快速打印", "GLToolInName快速打印",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "按默认设值立即打印本工程图。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设值随机颜色16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.设值随机颜色32.ToBitmap)
            Menu_设值随机颜色_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("设值随机颜色", "GLToolInName设值随机颜色",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "设值部件中零件为随机颜色。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.清净世界16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.清净世界32.ToBitmap)
            Menu_错误清理_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("错误清理", "GLToolInName错误清理",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "删除部件和工程图设计医生提示的错误。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.技术要求16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.技术要求32.ToBitmap)
            Menu_技术要求_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("技术要求", "GLToolInName技术要求",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开技术要求。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.居中对齐16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.居中对齐32.ToBitmap)
            Menu_居中对齐_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("居中对齐", "GLToolInName居中对齐",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "已两个零件的两组平行边的距离为偏移量，添加平齐约束。",
                smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.尺寸居中16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.尺寸居中32.ToBitmap)
            Menu_尺寸居中_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("尺寸居中", "GLToolInName尺寸居中",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将选择的尺寸文本居中。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.全部尺寸居中16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.全部尺寸居中32.ToBitmap)
            Menu_全部尺寸居中_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("全部居中", "GLToolInName全部尺寸居中",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将全部尺寸文本居中。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.克隆插入组件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.克隆插入组件32.ToBitmap)
            Menu_复制插入_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("复制插入", "GLToolInName复制插入",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "复制选择的组件，并插入选择的孔。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.可写16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.可写32.ToBitmap)
            Menu_设置只读_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("设置只读", "GLToolInName设置只读",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开设置只读窗口。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.可写16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.可写32.ToBitmap)
            Menu_文件只读_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("文件只读", "GLToolInName文件只读",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "单击设置当前编辑文件属性，被按下时为文件只读。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.标准件可见性16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.标准件可见性32.ToBitmap)
            Menu_标准件可见性_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("标准件可见性", "GLToolInName标准件可见性",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "一键显示隐藏所有标准件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.替换为库文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.替换为库文件32.ToBitmap)
            Menu_替换为库文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("替换为库文件", "GLToolInName替换为库文件",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将组件中的自定义标准件替换为库文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.编辑尺寸16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.编辑尺寸32.ToBitmap)
            Menu_动态尺寸_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("动态尺寸", "GLToolInName动态尺寸",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "动态编辑草图尺寸，实时更新到模型。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查找替换16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.查找替换32.ToBitmap)
            Menu_查找替换_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("查找替换", "GLToolInName查找替换",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "在部件中选择一个零部件，查询当前项目和资源中心文件夹，替换相同文件名的文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.驱动测量16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.驱动测量32.ToBitmap)
            Menu_驱动测量_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("驱动测量", "GLToolInName驱动测量",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "先选择2个对象，再选择1个尺寸驱动，动态测量2个对象之间的最小距离。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建截图16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建截图32.ToBitmap)
            Menu_创建截屏_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("创建截屏", "GLToolInName创建截屏",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "创建截屏图，保存到 ..桌面\截图 文件夹。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.按列表打开16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.按列表打开32.ToBitmap)
            Menu_按列表打开文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("按列表打开文件", "GLToolInName按列表打开文件",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开一个txt文本文件，按行读取文件名，在当前项目中查找并打开。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件32.ToBitmap)
            Menu_切换文档_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("切换文档", "GLToolInName切换文档",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开一个文档选择窗口。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)
            Menu_切换文档_Buttondef.DefaultShortcut = "QQ"

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.文件Dxf16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.文件dxf32.ToBitmap)
            Menu_导出为Dxf_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导出为Dxf", "GLToolInName导出为Dxf",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将当前文件导出为Dxf格式。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.标记展开图螺纹16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.标记展开图螺纹32.ToBitmap)
            Menu_标记展开图螺纹_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("简化孔", "GLToolInName简化展开图孔",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "简化钣金展开图中孔的螺纹，按直径转换为标记孔。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.按列表打开16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.按列表打开32.ToBitmap)
            Menu_保存为列表_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("保存为列表", "GLToolInName保存为列表",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "将当前激活的文件路径保存为列表，下次批量打开。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建展开16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.创建展开32.ToBitmap)
            Menu_创建工艺_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("创建工艺", "GLToolInName工艺",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开创建工艺图窗口。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.其他格式16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.其他格式32.ToBitmap)
            Menu_导出为Stp_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("导出为Stp", "GLToolInName导出为Stp",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "零部件导出为Stp文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.板厚16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.板厚32.ToBitmap)
            Menu_检查钣金厚度_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("检查钣金厚度", "GLToolInName检查钣金厚度",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "检查钣金厚度是否和材料匹配。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)


            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件夹16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.打开文件夹32.ToBitmap)
            Menu_打开快捷管理_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("打开文件浏览器", "GLToolInName打开文件浏览器",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开文件浏览器，管理当前部件所在文件夹。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导出平面BOM16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导出平面BOM32.ToBitmap)
            Menu_批量BOM重命名_buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("批量BOM重命名", "GLToolInName批量BOM重命名",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开文件浏览器，管理当前部件所在文件夹。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.快捷管理16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.快捷管理32.ToBitmap)
            Menu_快捷管理_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("快捷管理", "GLToolInName快捷管理",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开快捷管理。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)


            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.色卡16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.色卡32.ToBitmap)
            Menu_色卡_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("色卡", "GLToolInName色卡",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开色卡。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)


            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.修改序号16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.修改序号32.ToBitmap)
            Menu_修改序号_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("修改序号", "GLToolInName修改序号",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "修改一个选择的序号。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.孔颜色16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.孔颜色32.ToBitmap)
            Menu_孔着色_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("孔着色", "GLToolInName孔着色",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "按直径设置零件中孔的颜色。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)


            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.面着色16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.面着色32.ToBitmap)
            Menu_面着色_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("面着色", "GLToolInName面着色",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "按零件中面或特征的颜色。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.冗余文件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.冗余文件32.ToBitmap)
            Menu_清理冗余文件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("清理冗余文件", "GLToolInName清理冗余文件", CommandTypesEnum.kShapeEditCmdType, ClientID, "", "清理组件所在文件夹中无用的冗余文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.基础数量16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.基础数量32.ToBitmap)
            Menu_设置基础数量_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("基础数量", "GLToolInName设置基础数量", CommandTypesEnum.kShapeEditCmdType, ClientID, "", "设置基础数量为【每个】。如果是部件就设置所有引用文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.复制材料16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.复制材料32.ToBitmap)
            Menu_复制材料_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("复制材料", "GLToolInName复制材料", CommandTypesEnum.kShapeEditCmdType, ClientID, "", "从基础文件复制材料到镜像文件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.插入零部件16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.插入零部件32.ToBitmap)
            Menu_插入零部件_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("插入零部件", "GLToolInName插入零部件",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "插入打开的零部件。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

            'BOM 工具
            smallPicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导出平面BOM16.ToBitmap)
            largePicture = ClsPictureConverter.ImageToPictureDisp(My.Resources.导出平面BOM32.ToBitmap)
            Menu_BOM_Buttondef = oCommandManager.ControlDefinitions.AddButtonDefinition("BOM工具", "GLToolInNameBOM",
                CommandTypesEnum.kShapeEditCmdType, ClientID, "", "打开 BOM 工具：BOM 采集、属性批量操作、图号写入、工程图打印、PDF/DWG 导出、Excel 导入导出。需在部件（.iam）中运行。", smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)


            '' 1. 确保定义已创建
            'If Menu_插入零部件_Buttondef Is Nothing OrElse Menu_快捷管理_Buttondef Is Nothing Then
            '    'MsgBox("按钮定义未初始化")
            '    Exit Sub
            'End If

            '' 2. 清除旧的关联（防止多次加载插件时重复添加）
            'While Menu_插入零部件_Buttondef.AssociatedControls.Count > 0
            '    Menu_插入零部件_Buttondef.AssociatedControls.Remove(1)
            'End While

            '' 3. 建立新的关联
            'Menu_插入零部件_Buttondef.AssociatedControls.Add(Menu_快捷管理_Buttondef)

            '''4. 验证关联是否成功
            ''If Menu_插入零部件_Buttondef.AssociatedControls.Count = 0 Then
            ''    MsgBox("关联失败")
            ''    Exit Sub
            ''End If

            If bfirstTime Then

                '==========================================

                If oUserInterfaceManager.InterfaceStyle = InterfaceStyleEnum.kRibbonInterface Then
                    Dim oRibbon As Inventor.Ribbon
                    Dim oRibbonTab As Inventor.RibbonTab
                    Dim oRibbonPanel As Inventor.RibbonPanel

                    Dim oButtonDefinitions As Inventor.ObjectCollection
                    oButtonDefinitions = ThisApplication.TransientObjects.CreateObjectCollection
                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    '启动环境
                    oRibbon = oUserInterfaceManager.Ribbons.Item("ZeroDoc")

                    '快速入门

                    If ThisApplication.SoftwareVersion.DisplayVersion < 2020 Then
                        oRibbonTab = oRibbon.RibbonTabs.Item("id_GetStarted")
                    Else
                        oRibbonTab = oRibbon.RibbonTabs.Item("id_TabTools")
                    End If

                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)

                    oRibbonPanel.CommandControls.AddButton(Menu_快速打开_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_按列表打开文件_Buttondef, True)
                    'oRibbonPanel.CommandControls.AddButton(Menu_格式转换_Buttondef, True)

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_选项_Buttondef)
                    oButtonDefinitions.Add(Menu_格式转换_Buttondef)
                    oButtonDefinitions.Add(Menu_还原旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_批量打印_Buttondef)
                    oButtonDefinitions.Add(Menu_清理旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_快捷管理_Buttondef)
                    oButtonDefinitions.Add(Menu_关于_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_选项_Buttondef, oButtonDefinitions, True)




                    '==========================================

                    '+++++++++++++++++++++++++++++++++++
                    '部件环境
                    oRibbon = oUserInterfaceManager.Ribbons.Item("Assembly")

                    '工具选项卡
                    'oRibbonTab = oRibbon.RibbonTabs.Item("id_TabTools")

                    '装配选项卡
                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabAssemble")

                    '在零部件栏添加
                    oRibbonPanel = oRibbonTab.RibbonPanels.Item("id_PanelA_AssembleComponent")
                    ' 分割按钮：主按钮=插入零部件（可直接点击）；下拉=快捷管理（点主按钮旁下拉箭头展开，弹层位于主按钮下方）
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_快捷管理_Buttondef)
                    oButtonDefinitions.Add(Menu_插入零部件_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_插入零部件_Buttondef, oButtonDefinitions, True)

                    oRibbonPanel.CommandControls("AssemblyReplaceSplit").ChildControls.AddButton(Menu_查找替换_Buttondef)

                    '在关系栏添加
                    oRibbonPanel = oRibbonTab.RibbonPanels.Item("id_PanelA_AssembleRelationships")

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_居中对齐_Buttondef)
                    oButtonDefinitions.Add(Menu_对齐原始坐标面_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_居中对齐_Buttondef, oButtonDefinitions, False)

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_错误清理_Buttondef)
                    oButtonDefinitions.Add(Menu_复制插入_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_错误清理_Buttondef, oButtonDefinitions, False)

                    '在阵列栏添加
                    oRibbonPanel = oRibbonTab.RibbonPanels.Item("id_PanelA_AssemblePattern")
                    'oRibbonPanel.CommandControls.AddButton(Menu_复制插入_Buttondef)


                    '在测量栏添加
                    oRibbonPanel = oRibbonTab.RibbonPanels.Item("id_PanelA_ToolsMeasure")
                    oRibbonPanel.CommandControls.AddButton(Menu_驱动测量_Buttondef)

                    ' Create a new panel on the Assemble tab.

                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)
                    ' Add the buttons to the tab, with the first and last ones being large and the other two small.


                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_打开文件夹_Buttondef)
                    oButtonDefinitions.Add(Menu_错误清理_Buttondef)
                    oButtonDefinitions.Add(Menu_清理冗余文件_Buttondef)
                    oButtonDefinitions.Add(Menu_快捷管理_Buttondef)
                    oButtonDefinitions.Add(Menu_快速打开_Buttondef)
                    oButtonDefinitions.Add(Menu_按列表打开文件_Buttondef)
                    oButtonDefinitions.Add(Menu_全部保存_Buttondef)
                    oButtonDefinitions.Add(Menu_移动指定文件_Buttondef)
                    oButtonDefinitions.Add(Menu_查找缺失文件的部件_Buttondef)
                    oButtonDefinitions.Add(Menu_设置只读_Buttondef)
                    oButtonDefinitions.Add(Menu_标准件可见性_Buttondef)
                    oButtonDefinitions.Add(Menu_设值随机颜色_Buttondef)
                    oButtonDefinitions.Add(Menu_替换为库文件_Buttondef)
                    oButtonDefinitions.Add(Menu_刷新引用_Buttondef)
                    oButtonDefinitions.Add(Menu_移动指定文件_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_打开文件夹_Buttondef, oButtonDefinitions, False)



                    '==========================================
                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_创建工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_创建展开图_Buttondef)
                    oButtonDefinitions.Add(Menu_创建截屏_Buttondef)
                    oButtonDefinitions.Add(Menu_检查是否有工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_打开指定工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_文件只读_Buttondef)
                    oButtonDefinitions.Add(Menu_动态尺寸_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_文件只读_Buttondef, oButtonDefinitions, False)
                    '==========================================

                    '==============================================

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_格式转换_Buttondef)
                    oButtonDefinitions.Add(Menu_批量打印_Buttondef)
                    oButtonDefinitions.Add(Menu_导出为Stp_Buttondef)
                    oButtonDefinitions.Add(Menu_另存为副本_Buttondef)
                    oButtonDefinitions.Add(Menu_还原旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_清理旧版文件_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_格式转换_Buttondef, oButtonDefinitions, False)




                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_重命名_Buttondef)
                    oButtonDefinitions.Add(Menu_重命名镜像文件_Buttondef)
                    oButtonDefinitions.Add(Menu_提取iPro重命名_Buttondef)
                    oButtonDefinitions.Add(Menu_批量重命名_Buttondef)
                    oButtonDefinitions.Add(Menu_批量BOM重命名_buttondef)

                    oRibbonPanel.CommandControls.AddSplitButton(Menu_重命名_Buttondef, oButtonDefinitions, False)
                    '==========================================


                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_BOM_Buttondef)
                    oButtonDefinitions.Add(Menu_自动生成图号_Buttondef)
                    oButtonDefinitions.Add(Menu_设值随机颜色_Buttondef)
                    oButtonDefinitions.Add(Menu_设置基础数量_Buttondef)
                    oButtonDefinitions.Add(Menu_设置BOM结构_Buttondef)
                    oButtonDefinitions.Add(Menu_统计_Buttondef)
                    oButtonDefinitions.Add(Menu_保存为列表_Buttondef)
                    oButtonDefinitions.Add(Menu_选项_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_BOM_Buttondef, oButtonDefinitions, False)


                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_修改文件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_修改部件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_量产iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_自定义iProperty_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_项目iProperty_Buttondef, oButtonDefinitions, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_工程图_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_保存关闭_Buttondef, True)



                    '设计选项卡
                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabDesign")



                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)
                    ' Add the buttons to the tab, with the first and last ones being large and the other two small.
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_打开文件夹_Buttondef)
                    oButtonDefinitions.Add(Menu_错误清理_Buttondef)
                    oButtonDefinitions.Add(Menu_清理冗余文件_Buttondef)
                    oButtonDefinitions.Add(Menu_快捷管理_Buttondef)
                    oButtonDefinitions.Add(Menu_快速打开_Buttondef)
                    oButtonDefinitions.Add(Menu_按列表打开文件_Buttondef)
                    oButtonDefinitions.Add(Menu_全部保存_Buttondef)
                    oButtonDefinitions.Add(Menu_移动指定文件_Buttondef)
                    oButtonDefinitions.Add(Menu_查找缺失文件的部件_Buttondef)
                    oButtonDefinitions.Add(Menu_设置只读_Buttondef)
                    oButtonDefinitions.Add(Menu_标准件可见性_Buttondef)
                    oButtonDefinitions.Add(Menu_设值随机颜色_Buttondef)
                    oButtonDefinitions.Add(Menu_替换为库文件_Buttondef)
                    oButtonDefinitions.Add(Menu_刷新引用_Buttondef)
                    oButtonDefinitions.Add(Menu_移动指定文件_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_打开文件夹_Buttondef, oButtonDefinitions, False)



                    '==========================================
                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_创建工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_创建展开图_Buttondef)
                    oButtonDefinitions.Add(Menu_创建截屏_Buttondef)
                    oButtonDefinitions.Add(Menu_检查是否有工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_打开指定工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_文件只读_Buttondef)
                    oButtonDefinitions.Add(Menu_动态尺寸_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_文件只读_Buttondef, oButtonDefinitions, False)
                    '==========================================

                    '==============================================

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_格式转换_Buttondef)
                    oButtonDefinitions.Add(Menu_批量打印_Buttondef)
                    oButtonDefinitions.Add(Menu_导出为Stp_Buttondef)
                    oButtonDefinitions.Add(Menu_另存为副本_Buttondef)
                    oButtonDefinitions.Add(Menu_还原旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_清理旧版文件_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_格式转换_Buttondef, oButtonDefinitions, False)




                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_重命名_Buttondef)
                    oButtonDefinitions.Add(Menu_重命名镜像文件_Buttondef)
                    oButtonDefinitions.Add(Menu_提取iPro重命名_Buttondef)
                    oButtonDefinitions.Add(Menu_批量重命名_Buttondef)
                    oButtonDefinitions.Add(Menu_批量BOM重命名_buttondef)

                    oRibbonPanel.CommandControls.AddSplitButton(Menu_重命名_Buttondef, oButtonDefinitions, False)
                    '==========================================


                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_BOM_Buttondef)
                    oButtonDefinitions.Add(Menu_自动生成图号_Buttondef)
                    oButtonDefinitions.Add(Menu_设值随机颜色_Buttondef)
                    oButtonDefinitions.Add(Menu_设置基础数量_Buttondef)
                    oButtonDefinitions.Add(Menu_设置BOM结构_Buttondef)
                    oButtonDefinitions.Add(Menu_统计_Buttondef)
                    oButtonDefinitions.Add(Menu_保存为列表_Buttondef)
                    oButtonDefinitions.Add(Menu_选项_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_BOM_Buttondef, oButtonDefinitions, False)


                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_修改文件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_修改部件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_自定义iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_量产iProperty_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_项目iProperty_Buttondef, oButtonDefinitions, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_工程图_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_保存关闭_Buttondef, True)


                    '+++++++++++++++++++++++++++++++++++++

                    '零件环境
                    oRibbon = oUserInterfaceManager.Ribbons.Item("Part")

                    '工具选项卡
                    'oRibbonTab = oRibbon.RibbonTabs.Item("id_TabTools")

                    '模型选项卡
                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabModel")

                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_打开文件夹_Buttondef)
                    oButtonDefinitions.Add(Menu_快捷管理_Buttondef)
                    oButtonDefinitions.Add(Menu_全部保存_Buttondef)
                    oButtonDefinitions.Add(Menu_还原旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_清理旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_替换衍生_Buttondef)
                    oButtonDefinitions.Add(Menu_快速打开_Buttondef)
                    oButtonDefinitions.Add(Menu_按列表打开文件_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_打开文件夹_Buttondef, oButtonDefinitions, False)

                    '==============================================

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_格式转换_Buttondef)
                    oButtonDefinitions.Add(Menu_保存为列表_Buttondef)
                    oButtonDefinitions.Add(Menu_批量打印_Buttondef)
                    oButtonDefinitions.Add(Menu_修改文件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_自定义iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_文件只读_Buttondef)
                    oButtonDefinitions.Add(Menu_动态尺寸_Buttondef)
                    oButtonDefinitions.Add(Menu_选项_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_格式转换_Buttondef, oButtonDefinitions, False)
                    '=============================================================



                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_导出为Stp_Buttondef)
                    oButtonDefinitions.Add(Menu_复制材料_Buttondef)
                    oButtonDefinitions.Add(Menu_色卡_Buttondef)
                    oButtonDefinitions.Add(Menu_孔着色_Buttondef)
                    oButtonDefinitions.Add(Menu_面着色_Buttondef)
                    oButtonDefinitions.Add(Menu_创建工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_创建截屏_Buttondef)
                    oButtonDefinitions.Add(Menu_另存为副本_Buttondef)

                    oRibbonPanel.CommandControls.AddSplitButton(Menu_导出为Stp_Buttondef, oButtonDefinitions, False)

                    oButtonDefinitions.Clear()
                    oRibbonPanel.CommandControls.AddButton(Menu_项目iProperty_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_工程图_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_保存关闭_Buttondef, True)

                    oButtonDefinitions.Clear()
                    '=============================================================

                    '钣金选项卡+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabSheetMetal")

                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)

                    oButtonDefinitions.Clear()

                    oButtonDefinitions.Add(Menu_打开文件夹_Buttondef)
                    oButtonDefinitions.Add(Menu_快捷管理_Buttondef)
                    oButtonDefinitions.Add(Menu_快速打开_Buttondef)
                    oButtonDefinitions.Add(Menu_全部保存_Buttondef)
                    oButtonDefinitions.Add(Menu_替换衍生_Buttondef)
                    oButtonDefinitions.Add(Menu_还原旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_清理旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_修改文件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_量产iProperty_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_打开文件夹_Buttondef, oButtonDefinitions, False)

                    '========================================================

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_格式转换_Buttondef)
                    oButtonDefinitions.Add(Menu_保存为列表_Buttondef)
                    oButtonDefinitions.Add(Menu_批量打印_Buttondef)
                    oButtonDefinitions.Add(Menu_修改文件iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_自定义iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_文件只读_Buttondef)
                    oButtonDefinitions.Add(Menu_动态尺寸_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_格式转换_Buttondef, oButtonDefinitions, False)
                    '=============================================================
                    oButtonDefinitions.Clear()

                    oButtonDefinitions.Add(Menu_导出为Stp_Buttondef)
                    oButtonDefinitions.Add(Menu_色卡_Buttondef)
                    oButtonDefinitions.Add(Menu_孔着色_Buttondef)
                    oButtonDefinitions.Add(Menu_面着色_Buttondef)
                    oButtonDefinitions.Add(Menu_创建工程图_Buttondef)
                    oButtonDefinitions.Add(Menu_创建截屏_Buttondef)
                    oButtonDefinitions.Add(Menu_另存为副本_Buttondef)

                    oRibbonPanel.CommandControls.AddSplitButton(Menu_导出为Stp_Buttondef, oButtonDefinitions, False)



                    oButtonDefinitions.Clear()
                    oRibbonPanel.CommandControls.AddButton(Menu_项目iProperty_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_工程图_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_保存关闭_Buttondef, True)

                    '二维草图+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabSketch")

                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_动态尺寸_Buttondef, oButtonDefinitions, False)



                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    '工程图环境
                    oRibbon = oUserInterfaceManager.Ribbons.Item("Drawing")
                    'oRibbonTab = oRibbon.RibbonTabs.Item("id_TabPlaceViews")

                    '在放置视图选项卡中添加

                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabPlaceViews")
                    oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "GLTool", ClientID)


                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_错误清理_Buttondef)
                    oButtonDefinitions.Add(Menu_文件只读_Buttondef)

                    oRibbonPanel.CommandControls.AddSplitButton(Menu_错误清理_Buttondef, oButtonDefinitions, False)
                    '==========================================

                    '==========================================

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_导出为Dwg_Buttondef)
                    oButtonDefinitions.Add(Menu_导出为Pdf_Buttondef)
                    oButtonDefinitions.Add(Menu_导出为Dxf_Buttondef)
                    oButtonDefinitions.Add(Menu_标记展开图螺纹_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_导出为Dwg_Buttondef, oButtonDefinitions, False)
                    '==========================================



                    '==========================================
                    '==========================================


                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_打开文件夹_Buttondef)
                    oButtonDefinitions.Add(Menu_全部保存_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_打开文件夹_Buttondef, oButtonDefinitions, False)
                    '==========================================
                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_格式转换_Buttondef)
                    oButtonDefinitions.Add(Menu_快速打印_Buttondef)
                    oButtonDefinitions.Add(Menu_批量打印_Buttondef)
                    oButtonDefinitions.Add(Menu_错误清理_Buttondef)
                    oButtonDefinitions.Add(Menu_量产iProperty_Buttondef)
                    oButtonDefinitions.Add(Menu_还原旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_清理旧版文件_Buttondef)
                    oButtonDefinitions.Add(Menu_选项_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_格式转换_Buttondef, oButtonDefinitions, False)
                    '==========================================

                    ' oRibbonPanel = oRibbonTab.RibbonPanels.Add(GLTool, "ShinMaywaPartPanel", ClientID)

                    oButtonDefinitions.Clear()
                    oRibbonPanel.CommandControls.AddButton(Menu_技术要求_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_保存关闭_Buttondef, True)
                    '==========================================

                    '在标注选项卡中添加
                    oRibbonTab = oRibbon.RibbonTabs.Item("id_TabAnnotate")
                    '在尺寸栏添加
                    oRibbonPanel = oRibbonTab.RibbonPanels.Item("id_PanelD_AnnotateDimension")

                    oRibbonPanel.CommandControls.AddButton(Menu_添加直径_Buttondef, False)
                    oRibbonPanel.CommandControls.AddButton(Menu_尺寸圆整_Buttondef, False)

                    oRibbonPanel.CommandControls.AddButton(Menu_尺寸居中_Buttondef, False)
                    oRibbonPanel.CommandControls.AddButton(Menu_全部尺寸居中_Buttondef, False)

                    '在表格栏添加
                    oRibbonPanel = oRibbonTab.RibbonPanels.Item("id_PanelD_AnnotateTable")

                    oButtonDefinitions.Add(Menu_错误清理_Buttondef)
                    oButtonDefinitions.Add(Menu_设置对称件iProperty_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_错误清理_Buttondef, oButtonDefinitions, False)

                    oRibbonPanel.CommandControls.AddButton(Menu_替换图框标题栏_Buttondef, False)

                    oButtonDefinitions.Clear()
                    oButtonDefinitions.Add(Menu_检查序号完整性_Buttondef)
                    oButtonDefinitions.Add(Menu_新建序号_Buttondef)
                    oButtonDefinitions.Add(Menu_自动重建序号_Buttondef)
                    oButtonDefinitions.Add(Menu_重写BOM序号_Buttondef)
                    oRibbonPanel.CommandControls.AddSplitButton(Menu_检查序号完整性_Buttondef, oButtonDefinitions, False)
                    oRibbonPanel.CommandControls.AddButton(Menu_错误清理_Buttondef, False)
                    oButtonDefinitions.Clear()
                    oRibbonPanel.CommandControls.AddButton(Menu_技术要求_Buttondef, True)
                    oRibbonPanel.CommandControls.AddButton(Menu_保存关闭_Buttondef, True)
                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                    Dim obutton As ButtonDefinition
                    obutton = ThisApplication.CommandManager.ControlDefinitions.Item("GLToolInName快捷管理")
                    If obutton.OverrideShortcut = "" Then
                        obutton.OverrideShortcut = "FF"
                    End If


                ElseIf oUserInterfaceManager.InterfaceStyle = InterfaceStyleEnum.kClassicInterface Then

                End If
            End If


            'If bfirstTime Then ' Get the part features command bar.
            '    Dim CommandBar As Inventor.CommandBar

            '    ' Add a button to the command bar, defaulting to the end position.
            '    '部件环境
            '    CommandBar = ThisApplication.UserInterfaceManager.CommandBars.Item("AMxAssemblyPanelCmdBar")

            '    CommandBar.Controls.AddButton(Menu_修改文件iProperty_Buttondef)
            '    ''零件环境
            '    CommandBar = ThisApplication.UserInterfaceManager.CommandBars.Item("PMxPartFeatureCmdBar")
            '    ''工程图环境
            '    CommandBar = ThisApplication.UserInterfaceManager.CommandBars.Item("DLxDrawingAnnotationPanelCmdBar")

            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try
        End Sub


#Region "以下按钮事件"
        '------------------------------------------------------
        Private Sub Menu_修改文件iProperty_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_修改文件iProperty_Buttondef.OnExecute
            Try
                SetDocumentIpropertyFromFileName()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_修改部件iProperty_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_修改部件iProperty_Buttondef.OnExecute
            Try
                SetDocumentsInAssIpropertyFromFileName()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_重命名_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_重命名_Buttondef.OnExecute
            Try
                RenamePartFileNameInAssembly()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_提取iPro重命名_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_提取iPro重命名_Buttondef.OnExecute
            Try
                GetIpropertyToRename()

            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_自动生成图号_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_自动生成图号_Buttondef.OnExecute
            Try
                FormAutoPartNumberShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_重命名镜像文件_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_重命名镜像文件_Buttondef.OnExecute
            Try
                RenameMirrorPartFileNameInAssembly()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_打开文件夹_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_打开文件夹_Buttondef.OnExecute
            Try
                OpenFolderwithDocument()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_保存关闭_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_保存关闭_Buttondef.OnExecute
            Try
                SaveClose()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_导出为Pdf_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_导出为Pdf_Buttondef.OnExecute
            Try
                DrawingSaveAsPdf()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_导出为Dwg_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_导出为Dwg_Buttondef.OnExecute
            Try
                DrawingSaveAsDwg()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_设置BOM结构_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_设置BOM结构_Buttondef.OnExecute
            Try
                SetBOMStructuret()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_设置对称件iProperty_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_设置对称件iProperty_Buttondef.OnExecute
            Try
                SetDrawingMirPartIPro()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_序号完整性_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_检查序号完整性_Buttondef.OnExecute
            Try
                CheckSerialNumber()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_新建序号_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_新建序号_Buttondef.OnExecute
            Try
                CreateNewSequenceNumber()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_设置签字_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_设置签字_Buttondef.OnExecute
            Try
                SetUpSigning()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_清除签字_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_清除签字_Buttondef.OnExecute
            Try
                ClearSignature()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_自定义签字_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_自定义签字_Buttondef.OnExecute
            Try
                FormCustomSignatureShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_选项_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_选项_Buttondef.OnExecute
            Try
                FormOptionshow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_量产iProperty_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_量产iProperty_Buttondef.OnExecute
            Try
                FormMassiPopertiesshow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_批量打印_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_批量打印_Buttondef.OnExecute
            Try
                FormBulkPrintShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_格式转换_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_格式转换_Buttondef.OnExecute
            Try
                FormFormatConversionShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_还原旧版文件_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_还原旧版文件_Buttondef.OnExecute
            Try
                RestoreOldVersion()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_关于_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_关于_Buttondef.OnExecute
            Try
                Dim frmAbout As New FormAbout
                frmAbout.ShowDialog()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_帮助_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_帮助_Buttondef.OnExecute
            Try
                Dim strHelpFullFileName As String
                strHelpFullFileName = IO.Path.Combine(My.Application.Info.DirectoryPath, "帮助.pdf")
                Process.Start(strHelpFullFileName)
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_全部保存_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_全部保存_Buttondef.OnExecute
            Try
                FormSaveCloseAllDocumentShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_关闭文档_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_关闭文档_Buttondef.OnExecute
            Try
                CloseDocument()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_添加直径_Buttondef_OnExecute() Handles Menu_添加直径_Buttondef.OnExecute
            Try
                AddDiameter()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_检查是否有工程图_Buttondef_OnExecute() Handles Menu_检查是否有工程图_Buttondef.OnExecute
            Try
                CheckIsInvHaveDrawing()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_打开指定工程图_Buttondef_OnExecute() Handles Menu_打开指定工程图_Buttondef.OnExecute
            Try
                OpenAllDrwInAsm()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_批量重命名_Buttondef_OnExecute() Handles Menu_批量重命名_Buttondef.OnExecute
            Try
                FormBatchChangeFileNamesShow()
            Catch __ex As Exception
            End Try
        End Sub

        ' 导出BOM平面性
        Private Sub Menu_导出平面BOMenu_Buttondef_OnExecute() Handles Menu_导出平面BOMenu_Buttondef.OnExecute
            Try
                ExportBOMAsFlat()
            Catch __ex As Exception
            End Try
        End Sub

        ' 刷新引用名称
        Private Sub Menu_刷新引用_Buttondef_OnExecute() Handles Menu_刷新引用_Buttondef.OnExecute
            Try
                RefreshTreeNodeName()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_清理旧版文件_Buttondef_OnExecute() Handles Menu_清理旧版文件_Buttondef.OnExecute
            Try
                CleanUpLegacyFiles()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_移动指定文件_Buttondef_OnExecute() Handles Menu_移动指定文件_Buttondef.OnExecute
            Try
                ' MovesSpecifiedFile()
                FormMovesSpecifiedFileShow()

            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_对齐原始坐标面_Buttondef_OnExecute() Handles Menu_对齐原始坐标面_Buttondef.OnExecute
            Try
                FlushXYZPlane()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_查找缺失文件的部件_Buttondef_OnExecute() Handles Menu_查找缺失文件的部件_Buttondef.OnExecute
            Try
                GetAsmMissDocument()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_统计_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_统计_Buttondef.OnExecute
            Try
                FormStatisticalShow()
            Catch __ex As Exception
            End Try
        End Sub

        'BOM工具 工具（需在部件环境中运行，调用 clsBomTool）
        Private Sub Menu_BOM_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_BOM_Buttondef.OnExecute
            Try
                ' 调用 clsBomTool 的代码自建界面 BOM 工具（Run 内部自建窗体并显示，伪模态直到关闭）
                Dim oBom As New clsBomTool()
                oBom.Run()
            Catch ex As Exception
                MsgBox("BOM工具 运行失败：" & ex.Message, MsgBoxStyle.Exclamation, "BOM工具 ")
            End Try
        End Sub

        Private Sub Menu_项目iProperty_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_项目iProperty_Buttondef.OnExecute
            Try
                FormiPropertyShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_自定义iProperty_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_自定义iProperty_Buttondef.OnExecute
            Try
                FormUseriPropertyShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_尺寸圆整_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_尺寸圆整_Buttondef.OnExecute
            Try
                DimensionRounding()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_导入ERP编码到模型_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_导入ERP编码到模型_Buttondef.OnExecute
            Try
                FormImportERPCodeToIamShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_工程图_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_工程图_Buttondef.OnExecute
            Try
                OpenDrawingFile()
            Catch __ex As Exception
            End Try
        End Sub

        'BOM表保存新的替换项和按序号排序
        Private Sub Menu_重写BOM序号_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_重写BOM序号_Buttondef.OnExecute
            Try
                ReWriteBOM()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_打开ERP数据文件_Buttondef_OnExecute() Handles Menu_打开ERP数据文件_Buttondef.OnExecute
            Try
                OpenBasicExcel()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_输入查询ERP编码_Buttondef_OnExecute() Handles Menu_输入查询ERP编码_Buttondef.OnExecute
            Try
                FormSearchERPCodeShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_导入ERP到BOMenu_Buttondef_OnExecute() Handles Menu_导入ERP到BOMenu_Buttondef.OnExecute
            Try
                FormImportERPCodeToExcelshow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_快速打开_Buttondef_OnExecute() Handles Menu_快速打开_Buttondef.OnExecute
            Try
                QuitOpen()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_自动重建序号_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_自动重建序号_Buttondef.OnExecute
            Try
                RebuildRingSerialNumber()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_替换图框标题栏_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_替换图框标题栏_Buttondef.OnExecute
            Try
                ReplaceBorderTitleBlock()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_快速打印_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_快速打印_Buttondef.OnExecute
            Try
                QuitPrint()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_ERP反查_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_ERP反查_Buttondef.OnExecute
            Try
                FormReverseCheckERPCodesShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_设值随机颜色_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_设值随机颜色_Buttondef.OnExecute
            Try
                SetClearRandomColor()
                'SetRandomColor()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_错误清理_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_错误清理_Buttondef.OnExecute
            Try
                ClearErrorTagging()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_技术要求_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_技术要求_Buttondef.OnExecute
            Try
                FormSpecificationShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_居中对齐_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_居中对齐_Buttondef.OnExecute
            Try
                AlignComponentsInTheCenter()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_全部尺寸居中_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_全部尺寸居中_Buttondef.OnExecute
            Try
                CenterAllDimensions()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_尺寸居中_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_尺寸居中_Buttondef.OnExecute
            Try
                CenterDimensions()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_创建工艺_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_创建工艺_Buttondef.OnExecute
            Try
                FormFlatPatternShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_设置只读_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_设置只读_Buttondef.OnExecute
            Try
                FormSetReadOnlyShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_文件只读_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_文件只读_Buttondef.OnExecute
            Try
                SetWriteOnly()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_标准件可见性_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_标准件可见性_Buttondef.OnExecute
            Try
                SetStandIptVisible()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_替换为库文件_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_替换为库文件_Buttondef.OnExecute
            Try
                ReplaceWithContentCenterFile()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_动态尺寸_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_动态尺寸_Buttondef.OnExecute
            Try
                FormEditDimensionShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_创建展开图_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_创建展开图_Buttondef.OnExecute
            Try
                CreateFlatDrawingDocument()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_查找替换_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_查找替换_Buttondef.OnExecute
            Try
                FindAndReplace()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_替换衍生_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_替换衍生_Buttondef.OnExecute
            Try
                ReplaceDerivedPart()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_创建工程图_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_创建工程图_Buttondef.OnExecute
            Try
                CreatNewDrawingDocument()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_抑制全部错误的约束_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_抑制全部错误的约束_Buttondef.OnExecute
            Try
                SuppressAllUnhealthConstraints()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_驱动测量_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_驱动测量_Buttondef.OnExecute
            Try
                FormDim2ObjectShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_另存为副本_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_另存为副本_Buttondef.OnExecute
            Try
                SaveAsCopy()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_创建截屏_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_创建截屏_Buttondef.OnExecute
            Try
                CreatJpg()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_按列表打开文件_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_按列表打开文件_Buttondef.OnExecute
            Try
                OpenFilesWithList()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_切换文档_Buttondef_Buttondef_OnExecute(ByVal Context As Inventor.NameValueMap) Handles Menu_切换文档_Buttondef.OnExecute
            Try
                FormSwitchLablesShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_另存为Dxf_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_导出为Dxf_Buttondef.OnExecute
            Try
                DrawingSaveAsDxf()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_标记展开图螺纹_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_标记展开图螺纹_Buttondef.OnExecute
            Try
                MarkCircleInFlatDrawing()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_保存为列表_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_保存为列表_Buttondef.OnExecute
            Try
                SaveFilessList()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_复制插入_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_复制插入_Buttondef.OnExecute
            Try
                'CloneComponentAndInsertConstraint()

                FormCloneComponentShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_导出为Stp_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_导出为Stp_Buttondef.OnExecute
            Try
                AsmIptSaveAsStp()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_检查钣金厚度_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_检查钣金厚度_Buttondef.OnExecute
            Try
                CheckSteelThicknessInAssembly()
            Catch __ex As Exception
            End Try
        End Sub


        Private Sub Menu_打开快捷管理_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_打开快捷管理_Buttondef.OnExecute
            Try
                FormExplorerShow()
            Catch __ex As Exception
            End Try
        End Sub


        Private Sub Menu_批量BOM重命名_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_批量BOM重命名_buttondef.OnExecute
            Try
                FormiPropertyToFileNameShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_快捷管理_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_快捷管理_Buttondef.OnExecute
            Try
                FormExplorerShow()

            Catch __ex As Exception
            End Try
        End Sub


        Private Sub Menu_色卡_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_色卡_Buttondef.OnExecute
            Try
                FormPantoneShow()

            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_修改序号_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_修改序号_Buttondef.OnExecute
            Try
                ModifySerialNumber()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_孔着色_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_孔着色_Buttondef.OnExecute
            Try
                SameDiameterHoleColoring()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_面着色_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_面着色_Buttondef.OnExecute
            Try
                FormFaceColoringShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_清理冗余文件_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_清理冗余文件_Buttondef.OnExecute
            Try
                FormCleanUpRedundantFilesShow()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_设置基础数量_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_设置基础数量_Buttondef.OnExecute
            Try
                SetBaseQuantityByEach()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_复制材料_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_复制材料_Buttondef.OnExecute
            Try
                CopyMaterialFromBasicPart()
            Catch __ex As Exception
            End Try
        End Sub

        Private Sub Menu_插入零部件_Buttondef_OnExecute(Context As NameValueMap) Handles Menu_插入零部件_Buttondef.OnExecute
            Try
                FormPlaceOpenComponentShow()
            Catch __ex As Exception
            End Try
        End Sub

#End Region
        '==================================================================================
        ''
        '
        '===============================================

        Public Sub Deactivate() Implements Inventor.ApplicationAddInServer.Deactivate


            ' This method is called by Inventor when the AddIn is unloaded.
            ' The AddIn will be unloaded either manually by the user or
            ' when the Inventor session is terminated.

            ' TODO:  Add ApplicationAddInServer.Deactivate implementation

            ' Release objects.
            Marshal.ReleaseComObject(ThisApplication)
            ThisApplication = Nothing

            System.GC.WaitForPendingFinalizers()
            System.GC.Collect()


        End Sub

        Public ReadOnly Property Automation() As Object Implements Inventor.ApplicationAddInServer.Automation

            ' This property is provided to allow the AddIn to expose an API
            ' of its own to other programs. Typically, this  would be done by
            ' implementing the AddIn's API interface in a class and returning
            ' that class object through this property.

            Get
                Return Nothing
            End Get

        End Property

        Public Sub ExecuteCommand(ByVal commandID As Integer) Implements Inventor.ApplicationAddInServer.ExecuteCommand

            ' Note:this method is now obsolete, you should use the
            ' ControlDefinition functionality for implementing commands.

        End Sub



#Region "COM Registration"

        ' Registers this class as an AddIn for Inventor.
        ' This function is called when the assembly is registered for COM.
        <ComRegisterFunctionAttribute()>
        Public Shared Sub Register(ByVal t As Type)

            Dim clssRoot As RegistryKey = Registry.ClassesRoot
            Dim clsid As RegistryKey = Nothing
            Dim subKey As RegistryKey = Nothing

            Try
                clsid = clssRoot.CreateSubKey("CLSID\" + AddInGuid(t))
                clsid.SetValue(Nothing, "InventorAddIn")
                subKey = clsid.CreateSubKey("Implemented Categories\{39AD2B5C-7A29-11D6-8E0A-0010B541CAA8}")
                subKey.Close()

                subKey = clsid.CreateSubKey("Settings")
                subKey.SetValue("AddInType", "Standard")
                subKey.SetValue("LoadOnStartUp", "1")

                'subKey.SetValue("SupportedSoftwareVersionLessThan", "")
                subKey.SetValue("SupportedSoftwareVersionGreaterThan", "14..")
                'subKey.SetValue("SupportedSoftwareVersionEqualTo", "")
                'subKey.SetValue("SupportedSoftwareVersionNotEqualTo", "")
                'subKey.SetValue("Hidden", "0")
                'subKey.SetValue("UserUnloadable", "1")
                subKey.SetValue("Version", 0)
                subKey.Close()

                subKey = clsid.CreateSubKey("Description")
                subKey.SetValue(Nothing, "InventorAddIn")
            Catch ex As Exception
                System.Diagnostics.Trace.Assert(False)
            Finally
                If subKey IsNot Nothing Then subKey.Close()
                If clsid IsNot Nothing Then clsid.Close()
                If clssRoot IsNot Nothing Then clssRoot.Close()
            End Try

        End Sub

        ' Unregisters this class as an AddIn for Inventor.
        ' This function is called when the assembly is unregistered.
        <ComUnregisterFunctionAttribute()>
        Public Shared Sub Unregister(ByVal t As Type)

            Dim clssRoot As RegistryKey = Registry.ClassesRoot
            Dim clsid As RegistryKey = Nothing

            Try
                clssRoot = Microsoft.Win32.Registry.ClassesRoot
                clsid = clssRoot.OpenSubKey("CLSID\" + AddInGuid(t), True)
                clsid.SetValue(Nothing, "")
                clsid.DeleteSubKeyTree("Implemented Categories\{39AD2B5C-7A29-11D6-8E0A-0010B541CAA8}")
                clsid.DeleteSubKeyTree("Settings")
                clsid.DeleteSubKeyTree("Description")
            Catch
            Finally
                If clsid IsNot Nothing Then clsid.Close()
                If clssRoot IsNot Nothing Then clssRoot.Close()
            End Try

        End Sub

        ' This property uses reflection to get the value for the GuidAttribute attached to the class.
        Public Shared ReadOnly Property AddInGuid(ByVal t As Type) As String

            Get
                Dim guid As String = ""
                Try
                    Dim customAttributes() As Object = t.GetCustomAttributes(GetType(GuidAttribute), False)
                    Dim guidAttribute As GuidAttribute = CType(customAttributes(0), GuidAttribute)
                    guid = "{" + guidAttribute.Value.ToString() + "}"
                Finally
                    AddInGuid = guid
                End Try
            End Get

        End Property

#End Region



    End Class

End Namespace