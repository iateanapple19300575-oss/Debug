<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTargetYearMonth
	'Inherits System.Windows.Forms.Form
	Inherits BaseForm

	'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Windows フォーム デザイナーで必要です。
	Private components As System.ComponentModel.IContainer

	'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
	'Windows フォーム デザイナーを使用して変更できます。  
	'コード エディターを使って変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnOK = New System.Windows.Forms.Button()
		Me.txtYear = New System.Windows.Forms.TextBox()
		Me.lblHGT = New System.Windows.Forms.Label()
		Me._lblCaption_0 = New System.Windows.Forms.Label()
		Me.txtMonth = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'btnCancel
		'
		Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
		Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.btnCancel.Location = New System.Drawing.Point(360, 160)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.btnCancel.Size = New System.Drawing.Size(81, 37)
		Me.btnCancel.TabIndex = 8
		Me.btnCancel.Tag = "Close"
		Me.btnCancel.Text = "キャンセル"
		Me.btnCancel.UseVisualStyleBackColor = False
		'
		'btnOK
		'
		Me.btnOK.BackColor = System.Drawing.SystemColors.Control
		Me.btnOK.Cursor = System.Windows.Forms.Cursors.Default
		Me.btnOK.ForeColor = System.Drawing.SystemColors.ControlText
		Me.btnOK.Location = New System.Drawing.Point(268, 160)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.btnOK.Size = New System.Drawing.Size(81, 37)
		Me.btnOK.TabIndex = 7
		Me.btnOK.Tag = "Close"
		Me.btnOK.Text = "OK"
		Me.btnOK.UseVisualStyleBackColor = False
		'
		'txtYear
		'
		Me.txtYear.AcceptsReturn = True
		Me.txtYear.BackColor = System.Drawing.SystemColors.Window
		Me.txtYear.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtYear.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
		Me.txtYear.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtYear.Location = New System.Drawing.Point(28, 80)
		Me.txtYear.MaxLength = 4
		Me.txtYear.Name = "txtYear"
		Me.txtYear.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtYear.Size = New System.Drawing.Size(65, 28)
		Me.txtYear.TabIndex = 1
		Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'lblHGT
		'
		Me.lblHGT.BackColor = System.Drawing.SystemColors.Control
		Me.lblHGT.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblHGT.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
		Me.lblHGT.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblHGT.Location = New System.Drawing.Point(22, 20)
		Me.lblHGT.Name = "lblHGT"
		Me.lblHGT.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblHGT.Size = New System.Drawing.Size(316, 33)
		Me.lblHGT.TabIndex = 0
		Me.lblHGT.Text = "出講料計算対象年月度"
		Me.lblHGT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'_lblCaption_0
		'
		Me._lblCaption_0.BackColor = System.Drawing.SystemColors.Control
		Me._lblCaption_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._lblCaption_0.Font = New System.Drawing.Font("游ゴシック", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
		Me._lblCaption_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._lblCaption_0.Location = New System.Drawing.Point(99, 80)
		Me._lblCaption_0.Name = "_lblCaption_0"
		Me._lblCaption_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._lblCaption_0.Size = New System.Drawing.Size(35, 25)
		Me._lblCaption_0.TabIndex = 2
		Me._lblCaption_0.Text = "年"
		Me._lblCaption_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'txtMonth
		'
		Me.txtMonth.AcceptsReturn = True
		Me.txtMonth.BackColor = System.Drawing.SystemColors.Window
		Me.txtMonth.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtMonth.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
		Me.txtMonth.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtMonth.Location = New System.Drawing.Point(135, 80)
		Me.txtMonth.MaxLength = 4
		Me.txtMonth.Name = "txtMonth"
		Me.txtMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtMonth.Size = New System.Drawing.Size(35, 28)
		Me.txtMonth.TabIndex = 3
		Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label1
		'
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.Font = New System.Drawing.Font("游ゴシック", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Location = New System.Drawing.Point(176, 80)
		Me.Label1.Name = "Label1"
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.Size = New System.Drawing.Size(58, 28)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "月度"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'frmTargetYearMonth
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
		Me.ClientSize = New System.Drawing.Size(464, 217)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.txtMonth)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOK)
		Me.Controls.Add(Me.txtYear)
		Me.Controls.Add(Me.lblHGT)
		Me.Controls.Add(Me._lblCaption_0)
		Me.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmTargetYearMonth"
		Me.Text = "月度設定"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Public WithEvents btnCancel As Button
	Public WithEvents btnOK As Button
	Public WithEvents txtYear As TextBox
	Public WithEvents lblHGT As Label
	Public WithEvents _lblCaption_0 As Label
	Public WithEvents txtMonth As TextBox
	Public WithEvents Label1 As Label
End Class
