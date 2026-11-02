Imports Inventor

''' <summary>
''' 右键菜单-打开工程图（零部件环境）：
''' 未选中零部件时打开当前零部件的工程图；选中零部件时打开选中零部件的工程图。
''' 具体打开逻辑复用自带函数 OpenDrawingFile()。
''' </summary>
Public Class ClsRightMouseOpenDrawing
    Private m_打开工程图_Buttondef As ButtonDefinition

    Public Sub New()

        Dim smallPicture As IPictureDisp = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图16.ToBitmap)
        Dim largePicture As IPictureDisp = ClsPictureConverter.ImageToPictureDisp(My.Resources.工程图32.ToBitmap)

        m_打开工程图_Buttondef = ThisApplication.CommandManager.ControlDefinitions.AddButtonDefinition(
            "打开工程图", "InNameRightMouse打开工程图", CommandTypesEnum.kFileOperationsCmdType,
            ClientID, "", , smallPicture, largePicture, ButtonDisplayEnum.kDisplayTextInLearningMode)

        AddHandler m_打开工程图_Buttondef.OnExecute, AddressOf OnClick
        AddHandler ThisApplication.CommandManager.UserInputEvents.OnContextMenu, AddressOf OnContextMenu
    End Sub

    Private Sub OnContextMenu(SelectionDevice As SelectionDeviceEnum, AdditionalInfo As NameValueMap, CommandBar As CommandBar)
        Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument

        '仅零部件环境（零件 / 部件）显示；工程图等不显示
        If oDoc.DocumentType <> DocumentTypeEnum.kPartDocumentObject AndAlso
           oDoc.DocumentType <> DocumentTypeEnum.kAssemblyDocumentObject Then Return

        '选中或未选中零部件都显示（OpenDrawingFile 内部已按 SelectSet 区分处理）
        CommandBar.Controls.AddButton(m_打开工程图_Buttondef, 3)
    End Sub

    Private Sub OnClick()
        OpenDrawingFile()
    End Sub
End Class
