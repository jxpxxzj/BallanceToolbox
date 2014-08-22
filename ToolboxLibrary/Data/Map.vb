Imports System.Net
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Namespace Data
    Public Class Map
        Public Enum ItemType
            地图 = 0
            模组 = 1
            补丁 = 2
        End Enum
        Public Property Type As ItemType
        Public Property ID As Integer
        Public Property Title As String
        Public Property Creator As String
        Public Property Description As String
        Public Property Image As String
        Public Property Link As String
        Public Property Score As Double = 5.0
        Public Property VoteCount As Integer
        Public Property DownloadCount As Integer
        Public Property TargetImage As BitmapImage
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="i">ID</param>
        ''' <param name="t">Type</param>
        ''' <param name="l">Title</param>
        ''' <param name="c">Creator</param>
        ''' <param name="d">Description</param>
        ''' <param name="m">ImageLink</param>
        ''' <param name="k">DownloadLink</param>
        ''' <param name="o">Score</param>
        ''' <param name="v">VoteCount</param>
        ''' <param name="w">DownloadCount</param>
        ''' <remarks></remarks>
        Public Sub New(i As Integer, t As String, l As String, c As String, d As String, m As String, k As String, o As Integer, v As Integer, w As Integer)
            ID = i
            Type = t
            Title = l
            Creator = c
            Description = d
            Image = m
            Link = k
            Score = o
            VoteCount = v
            DownloadCount = w
        End Sub
        Public Sub New()

        End Sub
        Public Sub New(i As Integer)
            ID = i
        End Sub

        Public Overrides Function ToString() As String
            If VoteCount <> 0 Then
                Return DownloadCount & " 次下载 ・ 评分: " & Math.Round(Score / VoteCount, 2) & " 分 ・ " & Type.ToString
            Else
                Return DownloadCount & " 次下载 ・ 尚无评分 ・ " & Type.ToString
            End If
        End Function
        '    INSERT INTO  `db_jxpxxzj`.`tb_map` (
        '`ID` ,
        '`type` ,
        '`title` ,
        '`creator` ,
        '`description` ,
        '`image` ,
        '`link` ,
        '`score` ,
        '`votecount` ,
        '`downloadcount`
        ')
        'VALUES (
        '        '811',  '0',  'TitTest',  'Unknown',  'DesTest',  'ImgTest',  'LinTest',  '0',  '0',  '0'
        ');
        Public Function ToSQL() As String
            Return ("('" & ID & "','" & Type & "','" & Title & "','Unknown','" & Description & "','" & Image & "','" & Link & "','0','0','0')")
        End Function
        Public Function LoadImage() As ImageSource
            On Error GoTo err
            Dim img As New BitmapImage(New Uri(Image))
            Return img
            Exit Function
err:
            Return Nothing
        End Function
    End Class
End Namespace