Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class FormUseriProperty


    ' 创建TextBox数组
    Private textBoxes() As TextBox

    ' 创建ComboBox数组
    Private comboBoxes() As ComboBox

    Private Sub Btn确定_Click(sender As Object, e As EventArgs) Handles btn确定.Click
        Dim oInventorDocument As Inventor.Document
        oInventorDocument = ThisApplication.ActiveEditDocument

        If Me.textBoxes Is Nothing Then Exit Sub

        For i = 0 To Me.textBoxes.Length - 1
            If Me.textBoxes(i).Text = "" Then

            Else
                SetUserPropitem(oInventorDocument, Me.textBoxes(i).Text, Me.comboBoxes(i).Text)
            End If
        Next

        FormManager.CloseAndDisposeForm(Of FormUseriProperty)()
    End Sub

    Private Sub Btn关闭_Click(sender As Object, e As EventArgs) Handles btn关闭.Click, Me.Closing
        FormManager.CloseAndDisposeForm(Of FormUseriProperty)()
    End Sub

    Private Sub 重新读取_Click(sender As Object, e As EventArgs) Handles 重新读取.Click
        Dim oInventorDocument As Inventor.Document
        oInventorDocument = ThisApplication.ActiveEditDocument

        If Me.textBoxes Is Nothing Then Exit Sub

        For intControl = 0 To Me.textBoxes.Length - 1
            Me.comboBoxes(intControl).Text = GetUserPropitem(oInventorDocument, Me.textBoxes(intControl).Text)
        Next

    End Sub

    Private Sub FormUseriProperty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.AIPTool48
        Me.TopMost = True

        Dim toolTip As New ToolTip With {
            .AutoPopDelay = 0,
            .InitialDelay = 0,
            .ReshowDelay = 500
        }
        toolTip.SetToolTip(btn配置文件, "打开配置文件 UseriProperty.ini 。")

        btn配置文件.Image = My.Resources.文件txt16.ToBitmap

        ReloadData()

    End Sub

    Private Sub FormUseriProperty_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        ' 本窗体经 FormManager 以单例方式缓存：点 X 关闭仅隐藏不销毁，
        ' 重开时 Load 不再触发，故每次变为可见时都重新加载配置，避免停留在旧的空白实例。
        If Me.Visible Then ReloadData()
    End Sub

    ''' <summary>
    ''' 重新加载自定义 iProperty 配置（先清空旧控件再重建，支持窗体重开刷新）
    ''' </summary>
    Private Sub ReloadData()
        If textBoxes IsNot Nothing Then
            For Each c As Control In textBoxes
                Me.Controls.Remove(c)
                c.Dispose()
            Next
            textBoxes = Nothing
        End If
        If comboBoxes IsNot Nothing Then
            For Each c As Control In comboBoxes
                Me.Controls.Remove(c)
                c.Dispose()
            Next
            comboBoxes = Nothing
        End If
        LoadiPropertyData()
    End Sub


    ''' <summary>
    ''' 提取 {} 之前的字符串
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ExtractPrefix(input As String) As String
        Dim startIndex As Integer = input.IndexOf("{")
        If startIndex <> -1 Then
            Return input.Substring(0, startIndex).Trim()
        Else
            Return input.Trim()
        End If
    End Function


    ''' <summary>
    ''' 提取 {} 里面的字符串并按逗号分割成数组
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ExtractItems(input As String) As String()
        Dim startIndex As Integer = input.IndexOf("{")
        Dim endIndex As Integer = input.IndexOf("}")
        If startIndex <> -1 AndAlso endIndex <> -1 Then
            Dim itemsString As String = input.Substring(startIndex + 1, endIndex - startIndex - 1)
            Return itemsString.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries).Select(Function(s) s.Trim()).ToArray()
        Else
            Return New String() {}
        End If
    End Function

    ''' <summary>
    ''' 加载自定义数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadiPropertyData()
        ' 解析配置文件路径：优先执行程序集目录（COM 插件更可靠），其次 My.Application.Info.DirectoryPath
        Dim strDirs() As String = {
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            My.Application.Info.DirectoryPath
        }
        Dim strUseriPropertyIniFile As String = Nothing
        For Each d As String In strDirs
            If String.IsNullOrEmpty(d) Then Continue For
            Dim p As String = IO.Path.Combine(d, "UseriProperty.ini")
            If IsFileExists(p) Then
                strUseriPropertyIniFile = p
                Exit For
            End If
        Next
        If strUseriPropertyIniFile Is Nothing Then
            strUseriPropertyIniFile = IO.Path.Combine(strDirs(0), "UseriProperty.ini")
        End If

        DbgLog("[FormUseriProperty] ini路径=" & strUseriPropertyIniFile & " 存在=" & IsFileExists(strUseriPropertyIniFile).ToString())

        If IsFileExists(strUseriPropertyIniFile) = False Then
            MessageBox.Show("无配置文件,请手动配置！", AIPTool， MessageBoxButtons.OK， MessageBoxIcon.Error)

            Using oStreamWriter As New StreamWriter(strUseriPropertyIniFile, False, Encoding.Default)
                oStreamWriter.WriteLine("#不要修改#号行。")
                oStreamWriter.WriteLine("#数据结构为  自定义=自定义iproperty名称{列表数据}，若无列表数组则不设置{}内列表数据。")
                oStreamWriter.WriteLine("#自定义=输入数量{1,2,3,4,5,6,7,8,9,10}")
                oStreamWriter.WriteLine("数量=0")
            End Using

            Exit Sub
        End If

        Dim oInventorDocument As Inventor.Document
        oInventorDocument = ThisApplication.ActiveEditDocument

        ' 一次性读取所有行：先解析“数量=”（与“自定义=”行的先后顺序无关），再据此创建控件并填充
        Dim lines() As String = System.IO.File.ReadAllLines(strUseriPropertyIniFile, Encoding.Default)

        ' 第一遍：解析自定义控件数量（数量= 优先；若缺失或为 0，则按“自定义=”行数推断，避免窗口空白）
        Dim intControlCount As Integer = 0
        Dim intCustomLineCount As Integer = 0
        For Each strLine In lines
            If strLine.StartsWith("数量=") Then
                Integer.TryParse(strLine.Replace("数量=", "").Trim(), intControlCount)
            ElseIf strLine.StartsWith("自定义=") Then
                intCustomLineCount += 1
            End If
        Next

        If intControlCount <= 0 Then intControlCount = intCustomLineCount
        If intControlCount <= 0 Then
            DbgLog("[FormUseriProperty] 控件数为0（未找到 数量= 或 自定义= 行），窗口将空白。首行=" & If(lines.Length > 0, lines(0), "(空文件)"))
            Exit Sub
        End If

        DbgLog("[FormUseriProperty] intControlCount=" & intControlCount & " intCustomLineCount=" & intCustomLineCount)

        ' 仅创建一次控件（数量已确定，避免 ReDim(-1) 越界）
        CreateControls(intControlCount)

        ' 第二遍：填充自定义数据（受数量上限约束，避免索引越界）
        Dim intControl As Integer = 0
        For Each strLine In lines
            If strLine.StartsWith("#") Then Continue For
            If strLine.StartsWith("数量=") Then Continue For

            If strLine.StartsWith("自定义=") Then
                If intControl >= intControlCount Then Exit For

                Dim InputString As String = strLine.Replace("自定义=", "")

                ' 提取 {} 之前的字符串
                Dim striPropertyName As String = ExtractPrefix(InputString)

                ' 提取 {} 里面的字符串并按逗号分割成数组
                Dim striPropertyVales As String() = ExtractItems(InputString)

                Me.textBoxes(intControl).Text = striPropertyName

                For Each striPropertyVale As String In striPropertyVales
                    Me.comboBoxes(intControl).Items.Add(striPropertyVale)
                Next

                Me.comboBoxes(intControl).Text = GetUserPropitem(oInventorDocument, Me.textBoxes(intControl).Text)

                intControl += 1
            End If
        Next
    End Sub


    ''' <summary>
    ''' 创建textbox 和 combox 数组
    ''' </summary>
    ''' <param name="controlCount">数组数量</param>
    ''' <remarks></remarks>
    Private Sub CreateControls(ByVal controlCount As Integer)

        ' 设置初始位置
        Dim leftMarginTextBox As Integer = 12
        Dim leftMarginComboBox As Integer = 132
        Dim topMargin As Integer = 16
        Dim widthTextBox As Integer = 110
        Dim widthComboBox As Integer = 160
        Dim height As Integer = 20
        Dim verticalSpacing As Integer = 10  ' 垂直间距

        ' 创建TextBox数组
        ReDim textBoxes(controlCount - 1)

        ' 创建ComboBox数组
        ReDim comboBoxes(controlCount - 1)


        ' 循环创建TextBox和ComboBox
        For i As Integer = 0 To controlCount - 1
            ' 创建TextBox
            textBoxes(i) = New TextBox()
            With textBoxes(i)
                .Left = leftMarginTextBox
                .Top = topMargin + (i * (height + verticalSpacing))
                .Width = widthTextBox
                .Height = height
            End With
            Me.Controls.Add(textBoxes(i))

            ' 创建ComboBox
            comboBoxes(i) = New ComboBox()
            With comboBoxes(i)
                .Left = leftMarginComboBox
                .Top = topMargin + (i * (height + verticalSpacing))
                .Width = widthComboBox
                .Height = height
            End With
            Me.Controls.Add(comboBoxes(i))
        Next

        Me.Height = textBoxes(controlCount - 1).Top + height + 84

        Me.Show()
    End Sub

    Private Sub Btn配置文件_Click(sender As Object, e As EventArgs) Handles btn配置文件.Click
        Dim strUseriPropertyIniFile As String
        strUseriPropertyIniFile = IO.Path.Combine(My.Application.Info.DirectoryPath, "UseriProperty.ini")

        If IsFileExists(strUseriPropertyIniFile) = False Then
            MessageBox.Show("无配置文件,请手动配置！", AIPTool， MessageBoxButtons.OK， MessageBoxIcon.Error)

            Dim file As New StreamWriter(strUseriPropertyIniFile)
            file.WriteLine("#不要修改#号行。")
            file.WriteLine("#数据结构为  自定义=自定义iproperty名称{列表数据}，若无列表数组则不设置{}内列表数据。")
            file.WriteLine("#自定义=输入数量{1,2,3,4,5,6,7,8,9,10}")
            file.WriteLine("数量=0")
            file.Close()
            Exit Sub
        End If
      ProcessStart(strUseriPropertyIniFile)

    End Sub
End Class