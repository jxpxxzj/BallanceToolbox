Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Toolbox.Base
Imports Toolbox.Base.Interface
Imports Toolbox.Data

Namespace Component.WindowsForms
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

        Public Sub New(ByVal Info As Map)
            InitializeComponent()
            MapInfo = Info
            SetValue()
        End Sub

        Private Sub SetValue() Implements IValueable(Of Map).SetValue
            Label_Title.Text = MapInfo.Title
            Label_Description.Text = MapInfo.Description
            Dim g As Graphics
            g = Panel1.CreateGraphics()
            DrawShadow(g)
            Label_Info.Text = MapInfo.ToString()
        End Sub
        Private Sub DrawShadow(g As Graphics)
            Dim bru As New LinearGradientBrush(New Rectangle(New Point(0, 0), New Size(600, 5)), Color.FromArgb(206, 206, 206), Color.FromArgb(244, 244, 244), LinearGradientMode.Vertical)
            g.FillRectangle(bru, New Rectangle(New Point(0, 0), New Size(600, 5)))
        End Sub
        Private Sub Label_Title_Click(sender As Object, e As EventArgs) Handles Label_Title.Click
            Label_Title.Focus()
            OnDetail(e)
        End Sub

        Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
            DrawShadow(e.Graphics)
        End Sub

        Private Sub LoadImg()
            Label1.Visible = True
            MapInfo.TargetImage = MapInfo.LoadImage
            'PictureBox_Image.Image = MapInfo.TargetImage
            Me.Refresh()
            Label1.Visible = False
            Me.PictureBox_Image.Refresh()
        End Sub

        Private Sub clsListItem_Load(sender As Object, e As EventArgs) Handles Me.Load
            Dim t As New Threading.Thread(AddressOf LoadImg)
            t.Start()
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            OnDownload(e)
        End Sub
    End Class
End Namespace