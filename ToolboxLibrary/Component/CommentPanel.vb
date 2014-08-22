Imports Toolbox.Base.Interface
Imports Toolbox.Data

Namespace Component.WindowsForms
    Public Class CommentPanel
        Implements IValueable(Of Comment)
        Public Property CommentInfo As Comment Implements IValueable(Of Comment).Info
        Public Sub New(ByVal Info As Comment)
            InitializeComponent()
            CommentInfo = Info
            SetValue()
        End Sub

        Private Sub SetValue() Implements IValueable(Of Comment).SetValue
            Label_Text.Text = CommentInfo.Text
            Label_Info.Text = CommentInfo.ToString
            If CommentInfo.Feeling = Comment.DiscussFeeling.Like Then
                PictureBox1.Image = My.Resources._like
            ElseIf CommentInfo.Feeling = Comment.DiscussFeeling.Dislike Then
                PictureBox1.Image = My.Resources.dislike
            Else
                PictureBox1.Image = Nothing
            End If
        End Sub

        Private Sub 查看全文ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 查看全文ToolStripMenuItem.Click
            MsgBox(CommentInfo.ToString & vbCrLf & CommentInfo.Text, MsgBoxStyle.OkOnly, "评论全文")
        End Sub
    End Class
End Namespace