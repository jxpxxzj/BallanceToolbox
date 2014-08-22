Imports System.Xml
Imports System.Windows.Forms
Imports System.Web.Script.Serialization
Imports Toolbox.Base
Imports Toolbox.Data

Namespace Net.Reader
    Public Class CommentReader
        Inherits ReaderMain(Of Comment)
        Public Overrides Function ReadFromDB(url As String) As List(Of Comment)
            Dim lst As New List(Of Comment)
            Dim c As New System.Net.WebClient()
            Dim MyReader As New System.IO.StreamReader(c.OpenRead(url), System.Text.Encoding.UTF8) '定义新的文件流并读取网页文件数据
            Dim longTxt As String = MyReader.ReadToEnd
            Dim lc As List(Of String) = longTxt.Split("^").ToList

            For Each l In lc
                If l <> "" Then
                    Dim sr() As String = l.Split("|")
                    OnItemChanged(New SingleEventArgs(sr(3)))
                    lst.Add(New Comment(sr(0), sr(1), sr(3), sr(2), sr(4)))
                End If
            Next
            Return lst
        End Function
        Public Overrides Function ReadFromXml(url As String) As List(Of Comment)
            Dim lst As New List(Of Comment)

            Dim c As New System.Net.WebClient()
            Dim MyReader As New System.IO.StreamReader(c.OpenRead(url), System.Text.Encoding.Default) '定义新的文件流并读取网页文件数据
            Dim longTxt As String = MyReader.ReadToEnd
            Application.DoEvents()

            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(longTxt)

            Dim xn As XmlNode = xmlDoc.SelectSingleNode("DiscussList")
            Dim xnl As XmlNodeList = xn.ChildNodes
            Dim xnf As XmlNode

            For Each xnf In xnl
                Dim xe As XmlElement = CType(xnf, XmlElement)
                Dim tmp As New Comment
                tmp.ID = xe.GetAttribute("ID")

                Dim xnf1 As XmlNodeList = xnf.ChildNodes

                tmp.IP = xnf1(2).InnerText
                OnItemChanged(New SingleEventArgs(tmp.IP))
                tmp.Time = xnf1(0).InnerText
                tmp.Text = xnf1(1).InnerText
                tmp.Feeling = xnf1(3).InnerText

                lst.Add(tmp)
            Next
            Return lst
        End Function

        Public Overrides Function ReadFromjson(url As String) As List(Of Comment)
            Dim lst As New List(Of Comment)
            Dim c As New System.Net.WebClient()
            Dim MyReader As New System.IO.StreamReader(c.OpenRead(url), System.Text.Encoding.UTF8) '定义新的文件流并读取网页文件数据
            Dim longTxt As String = MyReader.ReadToEnd
            Dim lc As List(Of String) = longTxt.Split("^").ToList

            Dim jsr As New JavaScriptSerializer()
            For Each l In lc
                If l <> "" Then
                    Dim cm As Comment = jsr.Deserialize(Of Comment)(l)
                    lst.Add(cm)
                End If
            Next
            Return lst
        End Function
    End Class
End Namespace