Imports System.Windows.Forms
Imports System.ComponentModel
Imports Toolbox.Data

Namespace Helper
    Public NotInheritable Class DownloadHelper
        Public Sub New(TargetMap As Map)
            Map = TargetMap
        End Sub
        Public Property Map As Map
        Public Property FileName As String
        Public ReadOnly Property Size As Double
            Get
                Return _size
            End Get
        End Property
        Private _size As Double

        Public Event ProgressChanged As EventHandler

        Public Event DownloadComplete As EventHandler

        Protected Sub OnProgressChanged()
            RaiseEvent ProgressChanged(Me, EventArgs.Empty)
        End Sub

        Protected Sub OnDownloadComplete()
            RaiseEvent DownloadComplete(Me, EventArgs.Empty)
        End Sub

        Public ReadOnly Property ReceiveSize As Double
            Get
                Return _receivesize
            End Get
        End Property
        Private _receivesize As Double

        Public ReadOnly Property Progress As Double
            Get
                Return _progress
            End Get
        End Property
        Private _progress As Double

        Private Client As New Net.WebClient
        Private WithEvents ProgreessTimer As New Timer
        Private WithEvents SizeWorker As New BackgroundWorker
        Private WithEvents DownloadWorker As New BackgroundWorker

        Public Sub Start()
            ProgreessTimer.Interval = 1
            ProgreessTimer.Start()
            DownloadWorker.RunWorkerAsync()
            SizeWorker.RunWorkerAsync()
        End Sub
        Public Sub [Stop]()
            Client.Start = False
        End Sub

        Private Sub ProgreessTimer_Tick(sender As Object, e As EventArgs) Handles ProgreessTimer.Tick
            If Size <> 0 Then
                _progress = (Client.ReceiveProgress / 1024) / Size
            End If
            _receivesize = Client.ReceiveProgress / 1024
            OnProgressChanged()
        End Sub

        Private Sub SizeWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles SizeWorker.DoWork
            Dim c As New System.Net.WebClient
            Dim MyReader As New System.IO.StreamReader(c.OpenRead("http://jx.ballance.cn/toolbox/download.php?ID=" & Map.ID), System.Text.Encoding.UTF8) '定义新的文件流并读取网页文件数据
            _size = MyReader.ReadToEnd
        End Sub

        Private Sub DownloadWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles DownloadWorker.DoWork
            Client.strUrl = Map.Link
            Client.DownLoadFile(Application.StartupPath & "/Download/" & Map.Title & ".nmo")
            _progress = 1
            _receivesize = Client.ReceiveContentLength
        End Sub

        Private Sub DownloadWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles DownloadWorker.RunWorkerCompleted
            ProgreessTimer.Stop()
            OnDownloadComplete()
        End Sub
    End Class
End Namespace
