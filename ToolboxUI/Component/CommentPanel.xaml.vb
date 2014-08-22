Imports Toolbox.Data
Imports Toolbox.Base.Interface

Public Class CommentPanel
    Implements IValueable(Of Comment)
    Public Property CommentInfo As Comment Implements IValueable(Of Comment).Info
    Public Sub New(ByVal Info As Comment)
        InitializeComponent()
        CommentInfo = Info
        SetValue()
    End Sub


    Private Sub SetValue() Implements IValueable(Of Comment).SetValue
        Text_Comment.Text = CommentInfo.Text
        Text_Info.Text = CommentInfo.ToString
        If CommentInfo.Feeling = Comment.DiscussFeeling.Like Then
            Feeling.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/like.png", UriKind.Absolute))
        ElseIf CommentInfo.Feeling = Comment.DiscussFeeling.Dislike Then
            Feeling.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/dislike.png", UriKind.Absolute))
        Else
            Feeling.Source = Nothing
        End If
    End Sub

    Private Sub ShowFullMessage()
        MsgBox(CommentInfo.ToString & vbCrLf & CommentInfo.Text, MsgBoxStyle.OkOnly, "评论全文")
    End Sub
End Class
