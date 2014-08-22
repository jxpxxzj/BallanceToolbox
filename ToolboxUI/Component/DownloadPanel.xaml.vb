Imports Toolbox.Data
Imports Toolbox.Helper
Public Class DownloadPanel
    Implements Base.Interface.IValueable(Of DownloadHelper)

    Public Property Info As DownloadHelper Implements Base.Interface.IValueable(Of DownloadHelper).Info
        Get
            Return _dl
        End Get
        Set(value As DownloadHelper)
            _dl = value
        End Set
    End Property
    Private WithEvents _dl As DownloadHelper
    Private _frm As MainWindow
    Private Cancel As Boolean = False
    Private Finish As Boolean = False
    Public Sub New(Dl As DownloadHelper, frm As MainWindow)
        InitializeComponent()
        Info = Dl
        _frm = frm
        SetValue()
    End Sub
    Private Sub SetValue() Implements Base.Interface.IValueable(Of DownloadHelper).SetValue
        TitleText.Content = Info.Map.Title
        ReceiveText.Content = Info.ReceiveSize
        ImageRec.Fill = New ImageBrush(Info.Map.TargetImage)
    End Sub

    Private Sub _dl_DownloadComplete(sender As Object, e As EventArgs) Handles _dl.DownloadComplete
        If Cancel = False Then
            Finish = True
            ReceiveText.Content = "完成"
            ProgressBar.Value = 100
            _frm.PostMessage(Info.Map.Title & "的下载已完成.")

        End If
    End Sub

    Private Sub _dl_ProgressChanged(sender As Object, e As EventArgs) Handles _dl.ProgressChanged
        If Cancel = False Then
            ReceiveText.Content = Info.ReceiveSize & "KB"
            ProgressBar.Value = Info.Progress * 100
        End If
    End Sub

    Private Sub StopDownload(sender As Object, e As RoutedEventArgs)
        If Finish = False Then
            Cancel = True
            _dl.Stop()
            ReceiveText.Content = "取消"
            _frm.PostMessage(Info.Map.Title & "的下载已被取消.")
            ProgressBar.IsIndeterminate = True
        End If
    End Sub
End Class
