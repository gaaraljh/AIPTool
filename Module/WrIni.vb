Module WrIni
    ''' <summary>
    ''' 写配置文件
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConfigFileIniWriteSetting()
        'Save Settings
        On Error Resume Next

        ini.WriteStrINI("iProperty", "MapStochNum", Map_DrawingNnumber, IniFile)
        ini.WriteStrINI("iProperty", "MapPartName", Map_PartName, IniFile)
        ini.WriteStrINI("iProperty", "MapPartNum", Map_ERPCode, IniFile)
        ini.WriteStrINI("iProperty", "Map_Vendor", Map_Vendor, IniFile)
        ini.WriteStrINI("iProperty", "连接符", str连接符, IniFile)

        ini.WriteStrINI("iProperty", "MapMirStochNum", Map_Mir_StochNum, IniFile)
        ini.WriteStrINI("iProperty", "MapMirPartName", Map_Mir_PartName, IniFile)
        ini.WriteStrINI("iProperty", "MapMirERPCode", Map_Mir_ERPCode, IniFile)


        ini.WriteStrINI("比例质量", "MapDrawingScale", Map_DrawingScale, IniFile)
        ini.WriteStrINI("比例质量", "IsSetDrawingScale", IsSetDrawingScale, IniFile)
        ini.WriteStrINI("比例质量", "MapMass", Map_Mass, IniFile)
        ini.WriteStrINI("比例质量", "IsSetMass", IsSetMass, IniFile)

        ini.WriteStrINI("精度", "Mass_Accuracy", Mass_Accuracy, IniFile)
        ini.WriteStrINI("精度", "Area_Accuracy", Area_Accuracy, IniFile)


        ini.WriteStrINI("BOM", "BOMTiTle", BOMTiTle, IniFile)
        ini.WriteStrINI("BOM", "BasicExcelFullFileName", BasicExcelFullFileName, IniFile)
        ini.WriteStrINI("BOM", "Sheet_Name", SheetName, IniFile)
        ini.WriteStrINI("BOM", "Table_Array", TableArrays, IniFile)
        ini.WriteStrINI("BOM", "Col_Index_Num", ColIndexNum, IniFile)
        ini.WriteStrINI("BOM", "BOM编码", strEncoding, IniFile)

        ini.WriteStrINI("模型", "变更工程图扩展名", str变更工程图扩展名, IniFile)
        ini.WriteStrINI("模型", "另存到子文件夹", str另存到子文件夹, IniFile)
        ini.WriteStrINI("模型", "查找文件夹层数", str查找文件夹层数, IniFile)
        ini.WriteStrINI("模型", "检查重复图号", Is检查重复图号, IniFile)
        ini.WriteStrINI("模型", "去除后缀表", str去除后缀表, IniFile)

        ini.WriteStrINI("打印", "MapPrintDay", Map_PrintDay, IniFile)
        ini.WriteStrINI("打印", "IsOpenPrint", IsOpenPrint, IniFile)
        ini.WriteStrINI("打印", "EngineerName", EngineerName, IniFile)
        ini.WriteStrINI("打印", "IsDayAndName", IsDayAndName, IniFile)
        ini.WriteStrINI("打印", "短日期", IsShortData, IniFile)
        ini.WriteStrINI("打印", "Printer", Printer, IniFile)
        ini.WriteStrINI("打印", "IsPaperA3", IsPaperA3, IniFile)
        ini.WriteStrINI("打印", "IsSign", IsSign, IniFile)
        ini.WriteStrINI("打印", "SaveAsDwgPdf", SaveAsDawAndPdf, IniFile)
        ini.WriteStrINI("打印", "PrintSetting", PrintSetting, IniFile)

        ini.WriteStrINI("更新", "CheckUpdate", CheckUpdate, IniFile)
        ini.WriteStrINI("更新", "Server", Server, IniFile)
        ini.WriteStrINI("更新", "ServerExcelFileName", ServerExcelFileName, IniFile)
        ini.WriteStrINI("更新", "SimpleUpdater", SimpleUpdater, IniFile)
        ini.WriteStrINI("更新", "NewVersionTxt", NewVersionTxt, IniFile)

        ini.WriteStrINI("展开图", "展开图模板", str展开图模板, IniFile)
        ini.WriteStrINI("展开图", "向上颜色", str向上颜色, IniFile)
        ini.WriteStrINI("展开图", "向下颜色", str向下颜色, IniFile)
        ini.WriteStrINI("展开图", "向上线型", str向上线型, IniFile)
        ini.WriteStrINI("展开图", "向下线型", str向下线型, IniFile)
        ini.WriteStrINI("展开图", "向上线宽", str向上线宽, IniFile)
        ini.WriteStrINI("展开图", "向下线宽", str向下线宽, IniFile)
        ini.WriteStrINI("展开图", "展开图标注", str展开图标注, IniFile)

        ini.WriteStrINI("展开图", "展开图隐藏螺纹特征", str展开图隐藏螺纹特征, IniFile)
        ini.WriteStrINI("展开图", "标记孔径上限", str标记孔径上限, IniFile)
        'ini.WriteStrINI("展开图", "导出DXF", str导出DXF, Inifile)
        ini.WriteStrINI("展开图", "图号材质", str图号材质, IniFile)
        ini.WriteStrINI("展开图", "工艺文字高", str工艺文字高, IniFile)
        ini.WriteStrINI("展开图", "保存展开图到指定文件夹", str保存展开图到指定文件夹, IniFile)


        ini.WriteStrINI("工程图", "工程图模板", str工程图模板, IniFile)
        ini.WriteStrINI("工程图", "自动展开图", str自动展开图, IniFile)
        ini.WriteStrINI("工程图", "第三视角", str第三视角, IniFile)
        ini.WriteStrINI("工程图", "相切边", str相切边, IniFile)
        ini.WriteStrINI("工程图", "螺纹特征", str螺纹特征, IniFile)
        ini.WriteStrINI("工程图", "标注尺寸", str标注尺寸, IniFile)
        ini.WriteStrINI("工程图", "样式", str样式, IniFile)

        ini.WriteStrINI("工程图", "左视图", str选择视图.str左视图, IniFile)
        ini.WriteStrINI("工程图", "右视图", str选择视图.str右视图, IniFile)
        ini.WriteStrINI("工程图", "俯视图", str选择视图.str俯视图, IniFile)
        ini.WriteStrINI("工程图", "仰视图", str选择视图.str仰视图, IniFile)

        ini.WriteStrINI("工程图", "上边距", str页边距.short上边距, IniFile)
        ini.WriteStrINI("工程图", "下边距", str页边距.short下边距, IniFile)
        ini.WriteStrINI("工程图", "左边距", str页边距.short左边距, IniFile)
        ini.WriteStrINI("工程图", "右边距", str页边距.short右边距, IniFile)

        ini.WriteStrINI("工程图", "部件图框", str部件图框, IniFile)
        ini.WriteStrINI("工程图", "零件图框", str零件图框, IniFile)
        ini.WriteStrINI("工程图", "另存DWG后缀", str增加后缀, IniFile)

        ini.WriteStrINI("工程图", "模型匹配检查", str模型匹配检查, IniFile)
        ini.WriteStrINI("工程图", "钣金厚度检查", str钣金厚度检查, IniFile)
        ini.WriteStrINI("工程图", "钣金厚度前缀", str钣金厚度前缀, IniFile)
        ini.WriteStrINI("工程图", "逆时针序号", str逆时针序号, IniFile)
        ini.WriteStrINI("工程图", "强制横向", str强制横向, IniFile)

        ini.WriteStrINI("主题", "大图标", strLargeSmallIconSets, IniFile)
        ini.WriteStrINI("主题", "BOM重命名中间符号", str中间符号, IniFile)

        ini.WriteStrINI("截图", "宽度", intPitcureWidth, IniFile)
        ini.WriteStrINI("截图", "高度", intPitcureHeight, IniFile)

        ini.WriteStrINI("自动保存", "启用自动保存", str启用自动保存, IniFile)
        ini.WriteStrINI("自动保存", "保存文档类型", str保存文档类型, IniFile)
        ini.WriteStrINI("自动保存", "保存间隔时间", str保存间隔时间, IniFile)

    End Sub

    ''' <summary>
    ''' 读取配置文件
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConfigFileIniReadSetting()
        'On Error Resume Next
        Map_DrawingNnumber = ini.GetStrFromINI("iProperty", "MapStochNum", "库存编号", IniFile)
        Map_PartName = ini.GetStrFromINI("iProperty", "MapPartName", "零件代号", IniFile)
        Map_ERPCode = ini.GetStrFromINI("iProperty", "MapPartNum", "成本中心", IniFile)
        Map_Vendor = ini.GetStrFromINI("iProperty", "Map_Vendor", "供应商", IniFile)
        str连接符 = ini.GetStrFromINI("iProperty", "连接符", "无", IniFile)

        Select Case str连接符
            Case "无"
                char连接符 = “”
            Case "空格"
                char连接符 = “ ”
            Case "点."
                char连接符 = “.”
            Case "短横线-"
                char连接符 = “-”
            Case " 下划线_"
                char连接符 = “_”
        End Select

        Map_Mir_StochNum = ini.GetStrFromINI("iProperty", "MapMirStochNum", "对称件代号", IniFile)
        Map_Mir_PartName = ini.GetStrFromINI("iProperty", "MapMirPartName", "对称件名称", IniFile)
        Map_Mir_ERPCode = ini.GetStrFromINI("iProperty", "MapMirERPCode", "对称件编码", IniFile)

        Map_DrawingScale = ini.GetStrFromINI("比例质量", "MapDrawingScale", "比例", IniFile)
        IsSetDrawingScale = ini.GetStrFromINI("比例质量", "IsSetDrawingScale", "1", IniFile)
        Map_Mass = ini.GetStrFromINI("比例质量", "MapMass", "质量", IniFile)
        IsSetMass = ini.GetStrFromINI("比例质量", "IsSetMass", "-1", IniFile)

        Mass_Accuracy = ini.GetStrFromINI("精度", "Mass_Accuracy", "2", IniFile)
        Area_Accuracy = ini.GetStrFromINI("精度", "Area_Accuracy", "4", IniFile)

        BOMTiTle = ini.GetStrFromINI("BOM", "BOMTiTle", "库存编号|成本中心|零件代号|材料|质量|所属装配代号|数量|总数量|描述|供应商", IniFile)
        BasicExcelFullFileName = ini.GetStrFromINI("BOM", "BasicExcelFullFileName", "", IniFile)
        'CustomExcelFullFileName = ini.GetStrFromINI("", "Map_DrawingNnumber", ""
        SheetName = ini.GetStrFromINI("BOM", "Sheet_Name", "物料", IniFile)
        TableArrays = ini.GetStrFromINI("BOM", "Table_Array", "D,E,F", IniFile)
        ColIndexNum = ini.GetStrFromINI("BOM", "Col_Index_Num", "C", IniFile)
        strEncoding = ini.GetStrFromINI("BOM", "BOM编码", "Default", IniFile)

        Select Case strEncoding
            Case "Default"
                oEncoding = Text.Encoding.Default
            Case "UTF8"
                oEncoding = Text.Encoding.UTF8
        End Select


        str变更工程图扩展名 = ini.GetStrFromINI("模型", "变更工程图扩展名", "-1", IniFile)
        str另存到子文件夹 = ini.GetStrFromINI("模型", "另存到子文件夹", "-1", IniFile)
        str查找文件夹层数 = ini.GetStrFromINI("模型", "查找文件夹层数", "2", IniFile)
        Is检查重复图号 = ini.GetStrFromINI("模型", "检查重复图号", "-1", IniFile)
        str去除后缀表 = ini.GetStrFromINI("模型", "去除后缀表", "_MIR,_镜像", IniFile)

        Map_PrintDay = ini.GetStrFromINI("打印", "MapPrintDay", "打印日期", IniFile)
        IsOpenPrint = ini.GetStrFromINI("打印", "IsOpenPrint", "-1", IniFile)
        EngineerName = ini.GetStrFromINI("打印", "EngineerName", "", IniFile)
        IsDayAndName = ini.GetStrFromINI("打印", "IsDayAndName", "-1", IniFile)
        IsShortData = ini.GetStrFromINI("打印", "短日期 ", "1", IniFile)
        Printer = ini.GetStrFromINI("打印", "Printer", "", IniFile)
        IsPaperA3 = ini.GetStrFromINI("打印", "IsPaperA3", "1", IniFile)
        IsSign = ini.GetStrFromINI("打印", "IsSign", "1", IniFile)
        SaveAsDawAndPdf = ini.GetStrFromINI("打印", "SaveAsDwgPdf", "不另存", IniFile)
        PrintSetting = ini.GetStrFromINI("打印", "PrintSetting", "1101111001", IniFile)

        CheckUpdate = ini.GetStrFromINI("更新", "CheckUpdate", "1", IniFile)
        Server = ini.GetStrFromINI("更新", "Server", "\\Likai-pc\发行版\更新包\", IniFile)

        If Strings.Right(Server, 1) <> "\" Then
            Server = Server & "\"
        End If

        ServerExcelFileName = ini.GetStrFromINI("更新", "ServerExcelFileName", "最新物料编码.xlsx", IniFile)
        SimpleUpdater = ini.GetStrFromINI("更新", "SimpleUpdater", "SimpleUpdater.exe", IniFile)
        NewVersionTxt = ini.GetStrFromINI("更新", "NewVersionTxt", "NewVersion.txt", IniFile)

        str展开图模板 = ini.GetStrFromINI("展开图", "展开图模板", My.Application.Info.DirectoryPath & "\模板.idw", IniFile)

        str向上颜色 = ini.GetStrFromINI("展开图", "向上颜色", "#0000FF", IniFile)
        str向下颜色 = ini.GetStrFromINI("展开图", "向下颜色", "#FF0000", IniFile)

        str向上线型 = ini.GetStrFromINI("展开图", "向上线型", "虚线", IniFile)
        str向下线型 = ini.GetStrFromINI("展开图", "向下线型", "点长画线", IniFile)

        str向上线宽 = ini.GetStrFromINI("展开图", "向上线宽", "0.18", IniFile)
        str向下线宽 = ini.GetStrFromINI("展开图", "向下线宽", "0.18", IniFile)

        str展开图标注 = ini.GetStrFromINI("展开图", "展开图标注", "1", IniFile)

        str展开图隐藏螺纹特征 = ini.GetStrFromINI("展开图", "展开图隐藏螺纹特征", "1", IniFile)
        str标记孔径上限 = ini.GetStrFromINI("展开图", "标记孔径上限", "5", IniFile)
        'str导出DXF = ini.GetStrFromINI("展开图", "导出DXF", "1", Inifile)
        str图号材质 = ini.GetStrFromINI("展开图", "图号材质", "无材质", IniFile)

        str保存展开图到指定文件夹 = ini.GetStrFromINI("展开图", "保存展开图到指定文件夹", "-1", IniFile)

        str工艺文字高 = ini.GetStrFromINI("展开图", "工艺文字高", "2.5", IniFile)

        str工程图模板 = ini.GetStrFromINI("工程图", "工程图模板", My.Application.Info.DirectoryPath & "\模板.idw", IniFile)
        str自动展开图 = ini.GetStrFromINI("工程图", "自动展开图", "0", IniFile)
        str第三视角 = ini.GetStrFromINI("工程图", "第三视角", "0", IniFile)
        str相切边 = ini.GetStrFromINI("工程图", "相切边", "0", IniFile)
        str螺纹特征 = ini.GetStrFromINI("工程图", "螺纹特征", "1", IniFile)
        str标注尺寸 = ini.GetStrFromINI("工程图", "标注尺寸", "1", IniFile)
        str样式 = ini.GetStrFromINI("工程图", "样式", "1", IniFile)


        str选择视图.str左视图 = Val(ini.GetStrFromINI("工程图", "左视图", "1", IniFile))
        str选择视图.str右视图 = Val(ini.GetStrFromINI("工程图", "右视图", "0", IniFile))
        str选择视图.str俯视图 = Val(ini.GetStrFromINI("工程图", "俯视图", "1", IniFile))
        str选择视图.str仰视图 = Val(ini.GetStrFromINI("工程图", "仰视图", "0", IniFile))

        str页边距.short上边距 = Val(ini.GetStrFromINI("工程图", "上边距", "20", IniFile))
        str页边距.short下边距 = Val(ini.GetStrFromINI("工程图", "下边距", "50", IniFile))
        str页边距.short左边距 = Val(ini.GetStrFromINI("工程图", "左边距", "25", IniFile))
        str页边距.short右边距 = Val(ini.GetStrFromINI("工程图", "右边距", "10", IniFile))

        str部件图框 = ini.GetStrFromINI("工程图", "部件图框", "", IniFile)
        str零件图框 = ini.GetStrFromINI("工程图", "零件图框", "", IniFile)
        str增加后缀 = ini.GetStrFromINI("工程图", "另存DWG后缀", "-AutoCAD", IniFile)

        str模型匹配检查 = ini.GetStrFromINI("工程图", "模型匹配检查", "1", IniFile)
        str钣金厚度检查 = ini.GetStrFromINI("工程图", "钣金厚度检查", "-1", IniFile)
        str钣金厚度前缀 = ini.GetStrFromINI("工程图", "钣金厚度前缀", "钢板,steel", IniFile)
        str逆时针序号 = ini.GetStrFromINI("工程图", "逆时针序号", "1", IniFile)
        str强制横向 = ini.GetStrFromINI("工程图", "强制横向", "-1", IniFile)

        int每行数量 = ini.GetStrFromINI("切换文档", "每行数量", "8", IniFile)
        int图框宽度 = ini.GetStrFromINI("切换文档", "图框宽度", "160", IniFile)
        int图框高度 = ini.GetStrFromINI("切换文档", "图框高度", "120", IniFile)
        int图框行间距 = ini.GetStrFromINI("切换文档", "图框行间距", "10", IniFile)
        int图框列间距 = ini.GetStrFromINI("切换文档", "图框列间距", "10", IniFile)

        strLargeSmallIconSets = ini.GetStrFromINI("主题", "大图标", "大,大,大,大,大,大,大", IniFile)
        str中间符号 = ini.GetStrFromINI("主题", "BOM重命名中间符号", " ", IniFile)
        intPitcureWidth = ini.GetStrFromINI("截图", "宽度", "800", IniFile)
        intPitcureHeight = ini.GetStrFromINI("截图", "高度", "600“, IniFile)

        str启用自动保存 = ini.GetStrFromINI("自动保存", "启用自动保存", "0", IniFile)
        str保存文档类型 = ini.GetStrFromINI("自动保存", "保存文档类型", "当前文档", IniFile)
        str保存间隔时间 = ini.GetStrFromINI("自动保存", "保存间隔时间", "10", IniFile)

    End Sub

End Module
