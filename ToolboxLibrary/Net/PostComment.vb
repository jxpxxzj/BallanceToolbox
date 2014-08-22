Imports System.Net
Imports System.Text
Imports System.IO
Imports Toolbox.Data
Namespace Net
    Public Class PostComment
        Public Property Info As Comment
        Public Sub New()

        End Sub
        Public Sub New(inf As Comment)
            Info = inf
        End Sub
        Public Sub New(i As Integer, tex As String, fel As Comment.DiscussFeeling, sc As Integer)
            Info = New Comment(i, tex, fel, sc)
        End Sub
        Private Function GetURL(site As String) As String
            Return site & "?ID=" & Info.ID & "&text=" & Info.Text & "&feeling=" & Info.Feeling & "&score=" & Info.Score
        End Function

        Public Sub Post()
            Dim c As New System.Net.WebClient
            c.OpenRead(GetURL("http://jx.ballance.cn/Toolbox/post.php"))
        End Sub
    End Class
End Namespace