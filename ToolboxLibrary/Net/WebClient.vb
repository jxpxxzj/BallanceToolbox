Imports System
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports Microsoft.VisualBasic

Namespace Net
    ''' <summary>
    ''' 实现向web发送和接收数据的类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WebClient
        Const BOUNDARY As String = "--HEDAODE--"
        Const SEND_BUFFER_SIZE As Integer = 8000
        Const RECEIVE_BUFFER_SIZE As Integer = 15000
        Dim _strUrl, _strTextField, _strFileField As String
        Dim method, contentType, host, path, _headerText, _respHtml, _referer, _cookie As String
        Public Encode As Encoding = Encoding.Default
        Dim r As RegularExpressions.Regex
        Dim m As RegularExpressions.Match
        Dim PostDataList()(), header() As Byte
        Dim _start As Boolean = True
        Dim _SendProgress, _ReceiveProgress, _SendContentLength, _ReceiveContentLength, startIndex As Integer
        ''' <summary>
        ''' 请求的url地址
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property strUrl() As String
            Get
                Return _strUrl
            End Get
            Set(ByVal value As String)
                If value <> "" Then
                    m = RegularExpressions.Regex.Match(value, "https*://([^/]+)(/(.+))?")
                    If m.Success Then
                        host = m.Groups(1).Value
                        path = "/" + m.Groups(3).Value
                        _strUrl = value
                    Else
                        MsgBox("URL不合法!")
                    End If
                End If
            End Set
        End Property
        ''' <summary>
        ''' 要发送的文本域
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property strTextField() As String
            Get
                Return _strTextField
            End Get
            Set(ByVal value As String)
                If value <> "" Then
                    m = RegularExpressions.Regex.Match(value, "(\w+=[^&]+)|((\w+=[^&]+&)+)")
                    If m.Success Then
                        _strTextField = value
                    Else
                        MsgBox("数据有误:" & value)
                    End If
                End If
            End Set
        End Property
        ''' <summary>
        ''' 要发送的文件域
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property strFileField() As String
            Get
                Return _strFileField
            End Get
            Set(ByVal value As String)
                If value <> "" Then
                    m = RegularExpressions.Regex.Match(value, "(\w+=[^&]+)|((\w+=[^&]+&)+)")
                    If m.Success Then
                        _strFileField = value
                    Else
                        MsgBox("数据有误:" & value)
                    End If
                End If
            End Set
        End Property
        ''' <summary>
        ''' 获取或设置Cookie
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>用来跟踪用户身份</remarks>
        Property Cookie() As String
            Get
                Return _cookie
            End Get
            Set(ByVal value As String)
                _cookie = value
            End Set
        End Property
        ''' <summary>
        ''' 获取或设置Referer
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>模拟上一次访问地址</remarks>
        Property Referer() As String
            Get
                Return _referer
            End Get
            Set(ByVal value As String)
                _referer = value
            End Set
        End Property
        ''' <summary>
        ''' 获取http响应头文本
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property headerText() As String
            Get
                Return _headerText
            End Get
        End Property
        ''' <summary>
        ''' 获取服务器返回的文本
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property RespHtml() As String
            Get
                Return _respHtml
            End Get
            Set(ByVal value As String)
                _respHtml = value
            End Set
        End Property
        ''' <summary>
        ''' 获取发送实体大小
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property SendContentLength() As Integer
            Get
                Return _SendContentLength
            End Get
        End Property
        ''' <summary>
        ''' 获取接收实体大小
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property ReceiveContentLength() As Integer
            Get
                Return _ReceiveContentLength
            End Get
        End Property
        ''' <summary>
        ''' 获取数据发送进度
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property SendProgress() As Integer
            Get
                Return _SendProgress
            End Get
        End Property
        ''' <summary>
        ''' 获取数据接收进度
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property ReceiveProgress() As Integer
            Get
                Return _ReceiveProgress
            End Get
        End Property
        ''' <summary>
        ''' 设置是否立即中断发送和接收数据
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Start() As Boolean
            Get
                Return _start
            End Get
            Set(ByVal value As Boolean)
                _start = value
            End Set
        End Property
        ''' <summary>
        ''' 建立socket连接
        ''' </summary>
        ''' <param name="server"></param>
        ''' <param name="port"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ConnectSocket(ByVal server As String, ByVal port As Integer) As Sockets.Socket
            Dim s As Sockets.Socket = Nothing
            Dim hostEntry As IPHostEntry = Nothing
            ' Get host related information.
            Try
                hostEntry = Dns.GetHostEntry(server)
                ' Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
                ' an exception that occurs when the host host IP Address is not compatible with the address family
                ' (typical in the IPv6 case).
                Dim address As IPAddress
                For Each address In hostEntry.AddressList
                    Dim endPoint As New IPEndPoint(address, port)
                    Dim tempSocket As New Sockets.Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                    tempSocket.Connect(endPoint)
                    If tempSocket.Connected Then
                        s = tempSocket
                        Exit For
                    End If
                Next address
                Return s
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' 发送并接收数据
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SocketSendReceive(Optional ByVal progress As Boolean = False) As Byte()
            _SendProgress = 0
            _ReceiveProgress = 0
            Dim s As Sockets.Socket = ConnectSocket(host, 80)
            If s Is Nothing Then
                Return Encode.GetBytes("无法解析主机名称")
            End If
            Dim i, count As Integer
            Dim receivedByte() As Byte = Nothing
            '创建请求数据
            CreateRequestData()
            Try
                '发送数据****************************************
                s.SendTimeout = 5000
                s.Send(header) '发送请求头
                '如果有数据要发送就继续发送
                If Me._SendContentLength > 0 Then
                    For i = 0 To PostDataList.Length - 1
                        If Not Start Then
                            s.Close()
                            Return Encode.GetBytes("中断")
                        End If
                        s.Send(PostDataList(i)) '将缓冲区的数据发送出去
                        _SendProgress += PostDataList(i).Length
                    Next
                    s.Shutdown(SocketShutdown.Send) '停止发送数据
                End If
                '接收数据*****************************************
                Do
                    If Not Start Then
                        s.Close()
                        Return Encode.GetBytes("中断")
                    End If
                    Dim receiveBuffer(RECEIVE_BUFFER_SIZE) As Byte
                    count = s.Receive(receiveBuffer)
                    ReDim Preserve receiveBuffer(count - 1)
                    receivedByte = UniteArr(receivedByte, receiveBuffer)
                    _ReceiveProgress += count
                Loop While count > 0
                s.Close()
                Me._strTextField = ""
                Me._strFileField = ""
                Return GetResponseByte(receivedByte)
            Catch ex As Exception
                Return Encode.GetBytes(ex.Message)
            End Try
        End Function
        ''' <summary>
        ''' 分析服务器返回的字节流，去掉响应头，返回实体部分
        ''' </summary>
        ''' <param name="receivedByte"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetResponseByte(ByRef receivedByte() As Byte) As Byte()
            Dim tempByte() As Byte
            If Not receivedByte Is Nothing Then
                If receivedByte.Length <= 5000 Then
                    GetHeaderInfo(receivedByte)
                Else
                    Dim headByte(5000) As Byte
                    Array.Copy(receivedByte, headByte, 5000)
                    GetHeaderInfo(headByte)
                End If
                If Me._ReceiveContentLength > receivedByte.Length - startIndex Then
                    ReDim tempByte(receivedByte.Length - startIndex - 1)
                    Me._ReceiveContentLength = tempByte.Length
                    Array.Copy(receivedByte, startIndex, tempByte, 0, tempByte.Length)
                Else
                    ReDim tempByte(Me._ReceiveContentLength - 1)
                    Array.Copy(receivedByte, startIndex, tempByte, 0, Me._ReceiveContentLength)
                End If
                Return tempByte
            Else
                Return Encode.GetBytes("没有数据返回")
            End If
        End Function
        ''' <summary>
        ''' 分析获取响应头，获取实体大小
        ''' </summary>
        ''' <param name="receiveBuffer"></param>
        ''' <remarks></remarks>
        Private Sub GetHeaderInfo(ByVal receiveBuffer() As Byte)
            Dim text As String = Encode.GetChars(receiveBuffer)
            m = RegularExpressions.Regex.Match(text, "(.+" + vbCrLf + ")+" + vbCrLf + "((?<len>[a-fA-F0-9]{1,20}) {0,20}" + vbCrLf + ")?")
            If m.Success Then
                If m.Groups("len").Value <> "" Then
                    '某些服务器在响应头里
                    Me._ReceiveContentLength = "&H" + m.Groups("len").Value
                Else
                    Me._ReceiveContentLength = 500000 '如果响应头中没有实体大小，就取前500K，但这种几率很小
                End If
                startIndex = Encode.GetBytes((m.Value)).Length '实体开始的字节位置
                _headerText = m.Value '响应头
            End If
            '获取实体大小
            m = RegularExpressions.Regex.Match(text, "Content-Length:(?<len>.+)")
            If m.Success Then
                Me._ReceiveContentLength = Trim(m.Groups("len").Value)
            End If
        End Sub
        ''' <summary>
        ''' 向服务器请求数据并返回响应文本
        ''' </summary>
        ''' <param name="SetCookie">如果要保存cookie则为true</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Proc(Optional ByVal SetCookie As Boolean = False) As String
            _respHtml = Encode.GetChars(SocketSendReceive)
            If SetCookie And _headerText <> "" Then
                '从响应头中获取cookie
                m = RegularExpressions.Regex.Match(_headerText, "Set-Cookie:([^;]+)")
                Do While m.Success
                    Cookie += m.Groups(1).Value + ";"
                    m = m.NextMatch
                Loop
                Cookie = Trim(Cookie)
            End If
            Return _respHtml
        End Function
        ''' <summary>
        ''' 上传文件
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function UploadFile() As String
            _respHtml = Encode.GetChars(SocketSendReceive)
            Return _respHtml
        End Function
        ''' <summary>
        ''' 下载文件
        ''' </summary>
        ''' <param name="SavePath">文件保存路径，含文件名</param>
        ''' <remarks></remarks>
        Sub DownLoadFile(ByVal SavePath As String)
            Dim receivedData() As Byte = SocketSendReceive()
            If Start Then
                Dim fs As New FileStream(SavePath, FileMode.Create)
                fs.Write(receivedData, 0, receivedData.Length)
                fs.Close()
                fs = Nothing
            End If
        End Sub
        ''' <summary>
        ''' 创建http请求数据包
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateRequestData()
            Dim PostData() As Byte
            If strTextField = "" And strFileField = "" Then
                '不发送任何数据
                Me.method = "GET"
                Me._SendContentLength = 0
            ElseIf strTextField <> "" And strFileField <> "" Then
                '发送文本和文件数据
                Dim strText As String = ""
                m = RegularExpressions.Regex.Match(_strTextField, "(\w+)=([^&]+)")
                Do While m.Success
                    strText += "--" + BOUNDARY + vbCrLf
                    strText += "Content-Disposition: form-data; name=""" + m.Groups(1).Value + """" + vbCrLf + vbCrLf + m.Groups(2).Value + vbCrLf
                    m = m.NextMatch
                Loop
                PostData = UniteArr(Encode.GetBytes(strText), GetFileByte(_strFileField))
                Me.method = "POST"
                Me.contentType = "multipart/form-data; boundary=" + BOUNDARY
                Me._SendContentLength = PostData.Length
                GetPostDataList(PostData)
            ElseIf strTextField <> "" Then
                '只发送文本数据
                PostData = Encode.GetBytes(strTextField)
                Me.method = "POST"
                Me.contentType = "application/x-www-form-urlencoded"
                Me._SendContentLength = PostData.Length
                GetPostDataList(PostData)
            ElseIf strFileField <> "" Then
                '只发送文件数据
                PostData = GetFileByte(strFileField)
                Me.method = "POST"
                Me.contentType = "multipart/form-data; boundary=" + BOUNDARY
                Me._SendContentLength = PostData.Length
                GetPostDataList(PostData)
            End If
            header = GetHeader()
        End Sub
        ''' <summary>
        ''' 获取http请求头数据
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetHeader() As Byte()
            Dim strHeader As String
            strHeader = method + " " + path + " HTTP/1.1" + vbCrLf
            strHeader += "Accept: */*" + vbCrLf
            strHeader += "Accept-Language: zh-cn" + vbCrLf
            strHeader += "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)" + vbCrLf
            strHeader += "Host: " + host + vbCrLf
            If Referer <> "" Then
                strHeader += "Referer: " + Referer + vbCrLf
            End If
            Referer = ""
            If Me._SendContentLength > 0 Then
                strHeader += "Content-Type: " + contentType + vbCrLf
                strHeader += "Content-Length: " & Me._SendContentLength & vbCrLf
                'strHeader += "Expect:100-continue" + vbCrLf
                strHeader += "Connection: Keep-Alive" + vbCrLf
            Else
                strHeader += "Connection: Close" + vbCrLf
            End If
            If Cookie <> "" Then strHeader += "Cookie: " + _cookie + vbCrLf
            strHeader += vbCrLf
            Return Encode.GetBytes(strHeader)
        End Function
        ''' <summary>
        ''' 获取发送文件数据
        ''' </summary>
        ''' <param name="strFileField">文件域名称及文件路径</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetFileByte(ByVal strFileField As String) As Byte()
            Dim postByte() As Byte = Nothing
            Dim filePath As String
            m = RegularExpressions.Regex.Match(strFileField, "(\w+)=([^&]+)")
            Do While m.Success
                filePath = m.Groups(2).Value
                If File.Exists(filePath) Then
                    strFileField = "--" + BOUNDARY + vbCrLf
                    strFileField += "Content-Disposition: form-data; name=""" + m.Groups(1).Value + """; filename=""" + filePath + """" + vbCrLf
                    strFileField += "Content-Type: image/jpeg" + vbCrLf + vbCrLf
                    postByte = UniteArr(postByte, Encode.GetBytes(strFileField))
                    Dim fs As New FileStream(filePath, FileMode.Open)
                    Dim br As New BinaryReader(fs)
                    postByte = UniteArr(postByte, br.ReadBytes(fs.Length))
                    br.Close()
                    fs.Close()
                    postByte = UniteArr(postByte, Encode.GetBytes(vbCrLf))
                Else
                    MsgBox("上传文件不存在！")
                End If
                m = m.NextMatch
            Loop
            postByte = UniteArr(postByte, Encode.GetBytes("--" + BOUNDARY + "--" + vbCrLf))
            Return postByte
        End Function
        ''' <summary>
        ''' 将大数据包拆分成小包
        ''' </summary>
        ''' <param name="postData">要拆分的数据包</param>
        ''' <remarks></remarks>
        Private Sub GetPostDataList(ByRef postData() As Byte)
            Dim blockCount, remnant, i, p As Integer
            blockCount = postData.Length \ SEND_BUFFER_SIZE '整块个数
            remnant = postData.Length Mod SEND_BUFFER_SIZE '零头
            Dim bufferList(blockCount + IIf(remnant > 0, 0, -1))() As Byte
            '复制整块
            For i = 0 To blockCount - 1
                ReDim bufferList(i)(SEND_BUFFER_SIZE - 1)
                System.Array.Copy(postData, p, bufferList(i), 0, SEND_BUFFER_SIZE)
                p += SEND_BUFFER_SIZE
            Next
            '复制零头
            If remnant > 0 Then
                ReDim bufferList(i)(remnant - 1)
                System.Array.Copy(postData, p, bufferList(i), 0, remnant)
            End If
            PostDataList = bufferList
        End Sub
        ''' <summary>
        ''' 合并两个字节数组
        ''' </summary>
        ''' <param name="byte1"></param>
        ''' <param name="byte2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function UniteArr(ByVal byte1() As Byte, ByVal byte2() As Byte) As Byte()
            Dim temp() As Byte
            If byte1 Is Nothing And byte2 Is Nothing Then
                MsgBox("两个数组不能同时为空！")
                Return Nothing
            ElseIf byte1 Is Nothing Then
                ReDim temp(byte2.Length - 1)
                Array.Copy(byte2, 0, temp, 0, byte2.Length)
            ElseIf byte2 Is Nothing Then
                ReDim temp(byte1.Length - 1)
                Array.Copy(byte1, 0, temp, 0, byte1.Length)
            Else
                ReDim temp(byte1.Length + byte2.Length - 1)
                Array.Copy(byte1, 0, temp, 0, byte1.Length)
                Array.Copy(byte2, 0, temp, byte1.Length, byte2.Length)
            End If
            Return temp
        End Function

    End Class
End Namespace