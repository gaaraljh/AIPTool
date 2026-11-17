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

        '未选中时显示（打开当前文档的工程图）；有选中时，仅当全部为零部件（ComponentOccurrence）才显示，
        '约束、特征、面等非零部件不显示（OpenDrawingFile 只处理 ComponentOccurrence）
        If oDoc.SelectSet.Count > 0 Then
            For Each oSelected As Object In oDoc.SelectSet
                If Not TypeOf oSelected Is ComponentOccurrence Then Return
            Next
        End If

        CommandBar.Controls.AddButton(m_打开工程图_Buttondef, 3)
    End Sub

    Private Sub OnClick()
        OpenDrawingFile()
    End Sub
End Class
