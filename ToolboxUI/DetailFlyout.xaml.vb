Imports System.ComponentModel
Imports Toolbox.Net.Reader
Imports Toolbox.Data
Imports Toolbox.Base.Interface
Imports Toolbox.Net

Public Class DetailFlyout
    Implements IValueable(Of Map)
    Public Sub New(inf As Map)
        InitializeComponent()
        Info = inf
        SetValue()
    End Sub
    Public Sub New(ctl As MapPanel)
        InitializeComponent()
        Info = ctl.MapInfo
        SetValue()
    End Sub
    Public Property Info As Map Implements IValueable(Of Map).Info

    Public Sub SetValue() Implements IValueable(Of Map).SetValue
        Text_Descr.Text = Info.Description
        Image_Item.Source = Info.TargetImage
        Me.Header = Info.Title
    End Sub
    Dim WithEvents bgw2 As New BackgroundWorker
    Dim totallist As New List(Of Comment)
    Private Sub bgw2_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw2.DoWork
        Dim ldr As New CommentReader
        Dim url As String = "http://jx.ballance.cn/toolbox/comment_json.php?ID=" & Info.ID
        totallist = ldr.ReadFromjson(url)
    End Sub
    Public Sub LoadDiscussion() Handles bgw2.RunWorkerCompleted
        Try
            CommentGrid.Children.Clear()
            CommentGrid.RowDefinitions.Clear()

            For i = 0 To totallist.Count - 1
                Dim tt As Integer = i
                Dim p As New CommentPanel(totallist(i))

                Dim r As New RowDefinition
                r.Height = New GridLength(60)
                CommentGrid.RowDefinitions.Add(r)
                CommentGrid.Children.Add(p)
                'CommentGrid.RegisterName("Panel" & i, p)
                p.SetValue(Grid.RowProperty, i)
                p.SetValue(Grid.MarginProperty, New Thickness(0, 0, 0, 0))
                p.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch)
            Next
            ProgressRing.IsActive = False
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
    End Sub
    'Private Sub PictureBox_Image_Click(sender As Object, e As EventArgs) Handles PictureBox_Image.Click
    '    If PictureBox_Image.Image IsNot Nothing Then
    '        Dim frm As New Form4(PictureBox_Image.Image)
    '        frm.Show()
    '    End If
    'End Sub
    Dim waiterComment As New Comment
    Dim WithEvents bgw As New BackgroundWorker
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Post.Click
        Button_Post.IsEnabled = False
        Button_Post.Content = "正在发表..."
        bgw.RunWorkerAsync()
    End Sub

    Private Sub bgw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw.DoWork
        Dim tmp As Integer
        If waiterComment.Feeling = Comment.DiscussFeeling.Like Then
            tmp = 0
        Else
            tmp = 1
        End If
        Dim c As New PostComment(Info.ID, waiterComment.Text, tmp, waiterComment.Score)
        c.Post()
    End Sub

    Private Sub bgw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        ProgressRing.IsActive = True
        bgw2.RunWorkerAsync()
        Button_Post.Content = "发表完成"
        Textbox_Text.Text = ""
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles Me.Loaded
        bgw2.RunWorkerAsync()
        ProgressRing.IsActive = True
    End Sub

    Private Sub TrackBar1_MouseDown(sender As Object, e As MouseEventArgs) Handles Slider_Score.MouseDown
        Label_Score.Content = "评分: " & Slider_Score.Value
        waiterComment.Score = Slider_Score.Value
    End Sub

    Private Sub TrackBar1_ValueChanged(sender As Object, e As EventArgs) Handles Slider_Score.ValueChanged
        Label_Score.Content = "评分: " & Slider_Score.Value
        waiterComment.Score = Slider_Score.Value
    End Sub

    Private Sub Radio_Like_Checked(sender As Object, e As RoutedEventArgs) Handles Radio_Like.Checked
        waiterComment.Feeling = Comment.DiscussFeeling.Like
    End Sub

    Private Sub Radio_Dislike_Checked(sender As Object, e As RoutedEventArgs) Handles Radio_Dislike.Checked
        waiterComment.Feeling = Comment.DiscussFeeling.Dislike
    End Sub

    Private Sub Textbox_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles Textbox_Text.TextChanged
        waiterComment.Text = Textbox_Text.Text
    End Sub
End Class
