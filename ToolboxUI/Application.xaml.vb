Class Application

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。

    Private Sub Application_DispatcherUnhandledException(sender As Object, e As Windows.Threading.DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        'MsgBox("程序遇到了未经处理的异常,将尝试继续运行,以下是错误信息:" & vbCrLf & e.Exception.Message & vbCrLf & e.Exception.StackTrace, MsgBoxStyle.OkOnly, "错误提示")
        'e.Handled = True
    End Sub
End Class
