Namespace Component.WindowsForms
    Partial Class MapPanel
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
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Label_Info = New System.Windows.Forms.Label()
            Me.PictureBox_Image = New System.Windows.Forms.PictureBox()
            Me.Label_Description = New System.Windows.Forms.Label()
            Me.Label_Title = New System.Windows.Forms.Label()
            Me.Button1 = New System.Windows.Forms.Button()
            CType(Me.PictureBox_Image, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(11, 26)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(65, 12)
            Me.Label1.TabIndex = 16
            Me.Label1.Text = "Loading..."
            Me.Label1.Visible = False
            '
            'Panel1
            '
            Me.Panel1.Location = New System.Drawing.Point(0, 100)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(600, 5)
            Me.Panel1.TabIndex = 15
            '
            'Label_Info
            '
            Me.Label_Info.AutoSize = True
            Me.Label_Info.BackColor = System.Drawing.Color.Transparent
            Me.Label_Info.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label_Info.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label_Info.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label_Info.Location = New System.Drawing.Point(89, 75)
            Me.Label_Info.Name = "Label_Info"
            Me.Label_Info.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label_Info.Size = New System.Drawing.Size(94, 17)
            Me.Label_Info.TabIndex = 14
            Me.Label_Info.Text = "x条评论 ・ 普通"
            '
            'PictureBox_Image
            '
            Me.PictureBox_Image.Location = New System.Drawing.Point(3, 3)
            Me.PictureBox_Image.Name = "PictureBox_Image"
            Me.PictureBox_Image.Size = New System.Drawing.Size(80, 60)
            Me.PictureBox_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox_Image.TabIndex = 13
            Me.PictureBox_Image.TabStop = False
            '
            'Label_Description
            '
            Me.Label_Description.AutoEllipsis = True
            Me.Label_Description.BackColor = System.Drawing.Color.Transparent
            Me.Label_Description.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label_Description.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label_Description.ForeColor = System.Drawing.SystemColors.ControlText
            Me.Label_Description.Location = New System.Drawing.Point(89, 36)
            Me.Label_Description.Name = "Label_Description"
            Me.Label_Description.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label_Description.Size = New System.Drawing.Size(508, 39)
            Me.Label_Description.TabIndex = 11
            Me.Label_Description.Text = "描述"
            '
            'Label_Title
            '
            Me.Label_Title.AutoSize = True
            Me.Label_Title.BackColor = System.Drawing.Color.Transparent
            Me.Label_Title.Cursor = System.Windows.Forms.Cursors.Default
            Me.Label_Title.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.Label_Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(223, Byte), Integer))
            Me.Label_Title.Location = New System.Drawing.Point(87, 3)
            Me.Label_Title.Name = "Label_Title"
            Me.Label_Title.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.Label_Title.Size = New System.Drawing.Size(50, 25)
            Me.Label_Title.TabIndex = 10
            Me.Label_Title.Text = "名称"
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(3, 67)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(83, 27)
            Me.Button1.TabIndex = 17
            Me.Button1.Text = "下载"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'MapPanel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.Label_Info)
            Me.Controls.Add(Me.PictureBox_Image)
            Me.Controls.Add(Me.Label_Description)
            Me.Controls.Add(Me.Label_Title)
            Me.Name = "MapPanel"
            Me.Size = New System.Drawing.Size(600, 105)
            CType(Me.PictureBox_Image, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Label_Info As System.Windows.Forms.Label
        Friend WithEvents PictureBox_Image As System.Windows.Forms.PictureBox
        Friend WithEvents Label_Description As System.Windows.Forms.Label
        Public WithEvents Button1 As System.Windows.Forms.Button
        Public WithEvents Label_Title As System.Windows.Forms.Label

    End Class
End Namespace