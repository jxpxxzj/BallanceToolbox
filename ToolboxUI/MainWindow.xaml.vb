Imports MahApps.Metro.Controls.Dialogs
Imports System.ComponentModel
Imports Toolbox.Data
Imports Toolbox.Net.Reader
Imports Toolbox.Helper
Class MainWindow
#Region "资源"
    Public Property FullMapList As New List(Of Map)
    Private ShowMapList As New List(Of Map)
    Private FileList As New List(Of IO.FileInfo)
#End Region
#Region "连接与界面创建"
    Dim ldr As New MapReader
    Dim WithEvents bgw As New BackgroundWorker

    Private Sub LoadList() Handles bgw.DoWork
        FullMapList = ldr.ReadFromjson("http://jx.ballance.cn/Toolbox/Map_json.php")
        ShowMapList = FullMapList
    End Sub
    Private Sub bgw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        PostMessage("加载完毕.开始创建元素...")
        LoadMapPanel()
        PostMessage("元素创建完成.")
        ChangeTheme(2)

        Dim c As List(Of Map) = FindBestMap()
        FW.Items.Clear()

        For i = 0 To 4
            Dim gd As New Grid
            gd.Background = New ImageBrush(c(i).TargetImage)
            gd.Tag = c(i).Title & "  下载次数:" & c(i).DownloadCount & "  评分: " & Math.Round(c(i).Score / c(i).VoteCount, 2)

            FW.Items.Add(gd)
        Next
        FW.SelectedIndex = 0
    End Sub
    Private Sub LoadMapPanel()
        PanelGrid.Children.Clear()
        PanelGrid.RowDefinitions.Clear()
        For i = 0 To ShowMapList.Count - 1
            Dim tt As Integer = i
            Dim p As New MapPanel(ShowMapList(i))

            Dim r As New RowDefinition
            r.Height = New GridLength(110)
            PanelGrid.RowDefinitions.Add(r)

            PanelGrid.Children.Add(p)
            'PanelGrid.RegisterName("Panel" & i, p)
            p.SetValue(Grid.RowProperty, i)
            p.SetValue(Grid.MarginProperty, New Thickness(10, 0, 10, 0))
            p.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch)
            AddHandler p.Detail, AddressOf ShowDetails
            AddHandler p.Download, AddressOf StartDownload
        Next

    End Sub
#End Region
#Region "发现"
    Private Function FindBestMap() As List(Of Map)
        Dim cust = From s1 In FullMapList
                   Order By s1.DownloadCount Descending

        Return cust.ToList

    End Function
    Private Sub FW_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles FW.SelectionChanged
        If FW.SelectedIndex <> -1 Then
            Dim x As Grid = CType(FW.Items(FW.SelectedIndex), Grid)
            FW.BannerText = x.Tag
        End If
    End Sub
#End Region
#Region "项目事件"
    Private Sub StartDownload(sender As Object, e As EventArgs)
        Dim m As Map = CType(sender, MapPanel).MapInfo
        Dim dl As New DownloadHelper(m)
        Dim p As New DownloadPanel(dl, Me)
        Dim r As New RowDefinition
        r.Height = New GridLength(36)
        DownloadGrid.RowDefinitions.Add(r)
        DownloadGrid.Children.Add(p)
        p.SetValue(Grid.RowProperty, DownloadGrid.Children.Count - 1)
        p.SetValue(Grid.MarginProperty, New Thickness(0, 0, 0, 0))
        p.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch)
        dl.Start()
        PostMessage("开始下载:" & m.Title)
    End Sub
    Private Sub ShowDetails(sender As Object, e As EventArgs)
        Dim m As Map = CType(sender, MapPanel).MapInfo
        Me.Flyouts.Items(0) = New DetailFlyout(m)
        Dim fo As Flyout = Me.Flyouts.Items(0)
        fo.IsOpen = Not (fo.IsOpen)
    End Sub
#End Region
#Region "界面事件"
    Private Sub Settings()
        MsgBox("Test")
    End Sub
    Private Sub About(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Icon = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/icon.png", UriKind.Absolute))
        System.Threading.Thread.Sleep(1000)
        PostMessage("正在连接服务器...")
        bgw.RunWorkerAsync()
    End Sub
    Public Sub ChangeTheme(ByVal index As Integer)
        Dim a = Metro.ThemeManager.DefaultAccents(index)
        Metro.ThemeManager.ChangeTheme(Application.Current, a, Metro.Theme.Light)
    End Sub
    Public Sub PostMessage(ByVal Text As String)
        tranl.Content = Text
    End Sub
#End Region

#Region "列表"
#Region "搜索"
    Dim WithEvents searchbgw As New BackgroundWorker
    Private searchText As String
    Private typeIndex As Integer
    Private Sub Toolbar_Search_Click(sender As Object, e As RoutedEventArgs) Handles Toolbar_Search.Click
        PostMessage("正在搜索...")
        searchText = UCase(Toolbar_SearchText.Text)
        typeIndex = Toolbar_Type.SelectedIndex
        searchbgw.RunWorkerAsync()
    End Sub

    Private Sub searchbgw_DoWork(sender As Object, e As DoWorkEventArgs) Handles searchbgw.DoWork
        Dim cust = From s1 In FullMapList
          Where (InStr(UCase(s1.Title), searchText) <> 0) Or (InStr(UCase(s1.Creator), searchText) <> 0) Or (InStr(UCase(s1.Description), searchText) <> 0)
          Where (typeIndex = 0) Or (s1.Type = typeIndex - 1)
          Order By s1.Score / s1.VoteCount Descending
        'Order By Function()
        '             Select Case Toolbar_Order.SelectedIndex
        '                 Case 0
        '                     Return s1.Title
        '                 Case 1
        '                     Return s1.Score / s1.VoteCount
        '                 Case 2
        '                     Return s1.DownloadCount
        '                 Case Else
        '                     Return s1.Title
        '             End Select
        '         End Function Descending
        ShowMapList = cust.ToList
    End Sub

    Private Sub searchbgw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles searchbgw.RunWorkerCompleted
        PostMessage("搜索到 " & ShowMapList.Count & " 条记录.")
        LoadMapPanel()
    End Sub
#End Region
#End Region
#Region "下载"
    Private Sub Toolbar_Clear_Click(sender As Object, e As RoutedEventArgs)
        DownloadGrid.Children.Clear()
        DownloadGrid.RowDefinitions.Clear()
    End Sub

    Private Sub Toolbar_Open_Click(sender As Object, e As RoutedEventArgs) Handles Toolbar_Open.Click
        Microsoft.VisualBasic.Shell("explorer.exe " & Windows.Forms.Application.StartupPath & "\Download", AppWinStyle.NormalFocus)
    End Sub
#End Region
#Region "管理"
    Private Sub Mana_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles Mana_Refresh.Click
        Dim s As New IO.DirectoryInfo(Forms.Application.StartupPath & "\Download")
        FileList = s.GetFiles.ToList
        FileGrid.ItemsSource = FileList
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim s As MessageBoxResult = MessageBox.Show("确定安装此地图?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Information)
        If s = MessageBoxResult.OK Then
            MsgBox("ok")
        Else
            MsgBox("cancel")
        End If
    End Sub
#End Region

End Class