Imports System.ComponentModel
Imports Toolbox.Base.Interface
Imports Toolbox.Data

Public Class MapPanel
    Implements IValueable(Of Map)
    Public Property MapInfo As Map Implements IValueable(Of Map).Info

    Public Event Detail As EventHandler
    Public Event Download As EventHandler

    Protected Overridable Sub OnDetail(e As EventArgs)
        RaiseEvent Detail(Me, e)
    End Sub
    Protected Overridable Sub OnDownload(e As EventArgs)
        RaiseEvent Download(Me, e)
    End Sub

    Private Sub SetValue() Implements IValueable(Of Map).SetValue
        Label_Title.Content = MapInfo.Title
        Label_Description.Content = MapInfo.Description
        Label_Info.Content = MapInfo.ToString()
        LoadRing.IsActive = True
        MapInfo.TargetImage = MapInfo.LoadImage
        LoadRing.IsActive = False
        ItemImage.Fill = New ImageBrush(MapInfo.TargetImage)
    End Sub

    Public Sub New(ByVal Info As Map)
        InitializeComponent()
        MapInfo = Info
        SetValue()
    End Sub

    Private Sub Label_Title_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Label_Title.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            OnDetail(e)
        End If
    End Sub
    Dim WithEvents w As New BackgroundWorker

    Private Sub Button_Download_Click(sender As Object, e As RoutedEventArgs) Handles Button_Download.Click
        OnDownload(e)
    End Sub
End Class
