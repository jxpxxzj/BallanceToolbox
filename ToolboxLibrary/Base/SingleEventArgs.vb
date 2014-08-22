Namespace Base
    Public Class SingleEventArgs
        Inherits EventArgs
        Public Property Text As String
        Public Sub New(t As String)
            Text = t
        End Sub
    End Class
End Namespace