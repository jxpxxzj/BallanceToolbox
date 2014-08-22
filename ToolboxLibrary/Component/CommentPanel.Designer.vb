Namespace Component.WindowsForms
    Partial Class CommentPanel
        Inherits System.Windows.Forms.UserControl

        'UserControl 重写 Dispose，以清理组件列表。
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer

        '注意:  以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器修改它。  
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CommentPanel))
            Me.Label_Text = New System.Windows.Forms.Label()
            Me.Label_Info = New System.Windows.Forms.Label()
            Me.PictureBox_Quote = New System.Windows.Forms.PictureBox()
            Me.PictureBox1 = New System.Windows.Forms.PictureBox()
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.查看全文ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.举报RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            CType(Me.PictureBox_Quote, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ContextMenuStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Label_Text
            '
            Me.Label_Text.AutoEllipsis = True
            Me.Label_Text.ContextMenuStrip = Me.ContextMenuStrip1
            Me.Label_Text.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.Label_Text.Location = New System.Drawing.Point(25, 3)
            Me.Label_Text.Name = "Label_Text"
            Me.Label_Text.Size = New System.Drawing.Size(439, 37)
            Me.Label_Text.TabIndex = 6
            Me.Label_Text.Text = "评论文本"
            Me.ToolTip1.SetToolTip(Me.Label_Text, "右键打开菜单")
            '
            'Label_Info
            '
            Me.Label_Info.AutoSize = True
            Me.Label_Info.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.Label_Info.Location = New System.Drawing.Point(25, 40)
            Me.Label_Info.Name = "Label_Info"
            Me.Label_Info.Size = New System.Drawing.Size(244, 17)
            Me.Label_Info.TabIndex = 7
            Me.Label_Info.Text = "jxpxxzj ・ 192.168.1.*  ・  2014-3-8 18:05"
            '
            'PictureBox_Quote
            '
            Me.PictureBox_Quote.Image = CType(resources.GetObject("PictureBox_Quote.Image"), System.Drawing.Image)
            Me.PictureBox_Quote.InitialImage = Nothing
            Me.PictureBox_Quote.Location = New System.Drawing.Point(3, 3)
            Me.PictureBox_Quote.Name = "PictureBox_Quote"
            Me.PictureBox_Quote.Size = New System.Drawing.Size(16, 16)
            Me.PictureBox_Quote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox_Quote.TabIndex = 8
            Me.PictureBox_Quote.TabStop = False
            '
            'PictureBox1
            '
            Me.PictureBox1.Image = My.Resources.Resources._like
            Me.PictureBox1.InitialImage = Nothing
            Me.PictureBox1.Location = New System.Drawing.Point(3, 24)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
            Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox1.TabIndex = 9
            Me.PictureBox1.TabStop = False
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.查看全文ToolStripMenuItem, Me.举报RToolStripMenuItem})
            Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
            Me.ContextMenuStrip1.Size = New System.Drawing.Size(139, 48)
            '
            '查看全文ToolStripMenuItem
            '
            Me.查看全文ToolStripMenuItem.Name = "查看全文ToolStripMenuItem"
            Me.查看全文ToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
            Me.查看全文ToolStripMenuItem.Text = "查看全文(&F)"
            '
            '举报RToolStripMenuItem
            '
            Me.举报RToolStripMenuItem.Name = "举报RToolStripMenuItem"
            Me.举报RToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
            Me.举报RToolStripMenuItem.Text = "举报(&R)"
            '
            'clsDiscussItem
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.PictureBox_Quote)
            Me.Controls.Add(Me.Label_Info)
            Me.Controls.Add(Me.Label_Text)
            Me.Name = "clsDiscussItem"
            Me.Size = New System.Drawing.Size(470, 60)
            CType(Me.PictureBox_Quote, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ContextMenuStrip1.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label_Text As System.Windows.Forms.Label
        Friend WithEvents Label_Info As System.Windows.Forms.Label
        Friend WithEvents PictureBox_Quote As System.Windows.Forms.PictureBox
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents 查看全文ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents 举报RToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    End Class
End Namespace
