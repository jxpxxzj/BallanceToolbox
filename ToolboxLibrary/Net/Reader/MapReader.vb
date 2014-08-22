Imports System.Xml
Imports System.Windows.Forms
Imports System.Web.Script.Serialization
Imports Toolbox.Base
Imports Toolbox.Data

Namespace Net.Reader
    Public Class MapReader
        Inherits ReaderMain(Of Map)
        Public Overrides Function ReadFromXml(url As String) As List(Of Map)
            Dim lst As New List(Of Map)

            Dim c As New System.Net.WebClient()
            Dim MyReader As New System.IO.StreamReader(c.OpenRead(url), System.Text.Encoding.Default) '定义新的文件流并读取网页文件数据
            Dim longTxt As String = MyReader.ReadToEnd
            Application.DoEvents()

            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(longTxt)

            Dim xn As XmlNode = xmlDoc.SelectSingleNode("ItemList")
            Dim xnl As XmlNodeList = xn.ChildNodes
            Dim xnf As XmlNode

            For Each xnf In xnl
                Dim xe As XmlElement = CType(xnf, XmlElement)
                Dim tmp As New Map
                tmp.Type = xe.GetAttribute("Type")
                tmp.ID = xe.GetAttribute("ID")

                Dim xnf1 As XmlNodeList = xnf.ChildNodes

                tmp.Title = xnf1(0).InnerText
                OnItemChanged(New SingleEventArgs(tmp.Title))
                Application.DoEvents()
                tmp.Creator = xnf1(1).InnerText
                tmp.Description = xnf1(2).InnerText
                tmp.Image = xnf1(3).InnerText
                tmp.Link = xnf1(4).InnerText

                lst.Add(tmp)
            Next
            Return lst
        End Function
        Public Overrides Function ReadFromDB(url As String) As List(Of Map)
            Dim lst As New List(Of Map)
            Dim c As New System.Net.WebClient()
            Dim MyReader As New System.IO.StreamReader(c.OpenRead(url), System.Text.Encoding.UTF8) '定义新的文件流并读取网页文件数据
            Dim longTxt As String = MyReader.ReadToEnd
            Dim lc As List(Of String) = longTxt.Split("^").ToList

            For Each l In lc
                If l <> "" Then
                    Dim sr() As String = l.Split("|")
                    OnItemChanged(New SingleEventArgs(sr(3)))
                    lst.Add(New Map(sr(0), sr(1), sr(2), sr(3), sr(4), sr(5), sr(6), sr(7), sr(8), sr(9)))
                End If
            Next
            Return lst
        End Function

        Public Overrides Function ReadFromjson(url As String) As List(Of Map)
            Dim lst As New List(Of Map)
            Dim c As New System.Net.WebClient()
            Dim MyReader As New System.IO.StreamReader(c.OpenRead(url), System.Text.Encoding.UTF8) '定义新的文件流并读取网页文件数据
            Dim longTxt As String = MyReader.ReadToEnd
            Dim lc As List(Of String) = longTxt.Split("^").ToList

            Dim jsr As New JavaScriptSerializer()
            For Each l In lc
                If l <> "" Then
                    Dim mp As Map = jsr.Deserialize(Of Map)(l)
                    lst.Add(mp)
                End If
            Next
            Return lst
        End Function
    End Class
End Namespace