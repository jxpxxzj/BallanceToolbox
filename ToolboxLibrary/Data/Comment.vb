Namespace Data
    Public Class Comment
        Public Enum DiscussFeeling
            [Like] = 0
            Dislike = 1
        End Enum

        Public Property ID As Integer
        Public Property Time As String
        Public Property IP As String
        Public Property Text As String
        Public Property Feeling As DiscussFeeling
        Public Property Score As Integer

        Public Function ToSQL() As String
            Return ("('" & ID & "','" & Time & "','" & Text & "','" & IP & "','" & Feeling & "')")
        End Function
        Public Overrides Function ToString() As String
            Dim temp() As String = IP.Split(".")
            Dim tar As String = temp(0) & "." & temp(1) & "." & temp(2) & ".*"
            Return tar & " ・ " & Time
        End Function
        Public Sub New(i As Integer, tex As String, fel As Comment.DiscussFeeling, sc As Integer)
            ID = i
            Text = tex
            Feeling = fel
            Score = sc
        End Sub

        Public Sub New(i As Integer, tm As String, p As String, tex As String, fel As Comment.DiscussFeeling)
            ID = i
            Time = tm
            IP = p
            Text = tex
            Feeling = fel
        End Sub
        Public Sub New()

        End Sub
    End Class
End Namespace