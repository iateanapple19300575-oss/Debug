<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GHRUN_Main
	'Inherits System.Windows.Forms.Form
	Inherits BaseForm

	'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

	'Windows フォーム デザイナーで必要です。
	Private components As System.ComponentModel.IContainer

	'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
	'Windows フォーム デザイナーを使用して変更できます。  
	'コード エディターを使って変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.cmnSetting = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.MenuItemCheckRewards = New System.Windows.Forms.ToolStripMenuItem()
		Me.cmnAttendanceMasterManagement = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.MenuItemMasterDataImport = New System.Windows.Forms.ToolStripMenuItem()
		Me.cmnStartSettings = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.MenuItemSelectMonth = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuItemStampCheck = New System.Windows.Forms.ToolStripMenuItem()
		Me.cmnAttendanceDataManagement = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.MenuItemAttendanceDataImport = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuItemAllowanceDeductionImport = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuItemTrans_Exp_Mngmnt = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuItemConfirmationProcess = New System.Windows.Forms.ToolStripMenuItem()
		Me.cmnAttendanceDataOutput = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.animationTimer = New System.Windows.Forms.Timer(Me.components)
		Me.cmnSetting.SuspendLayout()
		Me.cmnAttendanceMasterManagement.SuspendLayout()
		Me.cmnStartSettings.SuspendLayout()
		Me.cmnAttendanceDataManagement.SuspendLayout()
		Me.SuspendLayout()
		'
		'cmnSetting
		'
		Me.cmnSetting.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemCheckRewards})
		Me.cmnSetting.Name = "cmnSetting"
		Me.cmnSetting.Size = New System.Drawing.Size(123, 26)
		'
		'MenuItemCheckRewards
		'
		Me.MenuItemCheckRewards.Name = "MenuItemCheckRewards"
		Me.MenuItemCheckRewards.Size = New System.Drawing.Size(122, 22)
		Me.MenuItemCheckRewards.Text = "報酬確認"
		'
		'cmnAttendanceMasterManagement
		'
		Me.cmnAttendanceMasterManagement.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemMasterDataImport})
		Me.cmnAttendanceMasterManagement.Name = "cmnAttendanceMasterManagement"
		Me.cmnAttendanceMasterManagement.Size = New System.Drawing.Size(171, 26)
		'
		'MenuItemMasterDataImport
		'
		Me.MenuItemMasterDataImport.Name = "MenuItemMasterDataImport"
		Me.MenuItemMasterDataImport.Size = New System.Drawing.Size(170, 22)
		Me.MenuItemMasterDataImport.Text = "マスタデータ取り込み"
		'
		'cmnStartSettings
		'
		Me.cmnStartSettings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemSelectMonth, Me.MenuItemStampCheck})
		Me.cmnStartSettings.Name = "cmnStartSettings"
		Me.cmnStartSettings.Size = New System.Drawing.Size(123, 48)
		'
		'MenuItemSelectMonth
		'
		Me.MenuItemSelectMonth.Name = "MenuItemSelectMonth"
		Me.MenuItemSelectMonth.Size = New System.Drawing.Size(122, 22)
		Me.MenuItemSelectMonth.Text = "月度設定"
		'
		'MenuItemStampCheck
		'
		Me.MenuItemStampCheck.Name = "MenuItemStampCheck"
		Me.MenuItemStampCheck.Size = New System.Drawing.Size(122, 22)
		Me.MenuItemStampCheck.Text = "打刻確認"
		'
		'cmnAttendanceDataManagement
		'
		Me.cmnAttendanceDataManagement.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemAttendanceDataImport, Me.MenuItemAllowanceDeductionImport, Me.MenuItemTrans_Exp_Mngmnt, Me.MenuItemConfirmationProcess})
		Me.cmnAttendanceDataManagement.Name = "cmnAttendanceDataManagement"
		Me.cmnAttendanceDataManagement.Size = New System.Drawing.Size(179, 92)
		'
		'MenuItemAttendanceDataImport
		'
		Me.MenuItemAttendanceDataImport.Name = "MenuItemAttendanceDataImport"
		Me.MenuItemAttendanceDataImport.Size = New System.Drawing.Size(178, 22)
		Me.MenuItemAttendanceDataImport.Text = "勤怠データ取込"
		'
		'MenuItemAllowanceDeductionImport
		'
		Me.MenuItemAllowanceDeductionImport.Name = "MenuItemAllowanceDeductionImport"
		Me.MenuItemAllowanceDeductionImport.Size = New System.Drawing.Size(178, 22)
		Me.MenuItemAllowanceDeductionImport.Text = "手当・控除データ取込"
		'
		'MenuItemTrans_Exp_Mngmnt
		'
		Me.MenuItemTrans_Exp_Mngmnt.Name = "MenuItemTrans_Exp_Mngmnt"
		Me.MenuItemTrans_Exp_Mngmnt.Size = New System.Drawing.Size(178, 22)
		Me.MenuItemTrans_Exp_Mngmnt.Text = "交通費管理"
		'
		'MenuItemConfirmationProcess
		'
		Me.MenuItemConfirmationProcess.Name = "MenuItemConfirmationProcess"
		Me.MenuItemConfirmationProcess.Size = New System.Drawing.Size(178, 22)
		Me.MenuItemConfirmationProcess.Text = "締め処理"
		'
		'cmnAttendanceDataOutput
		'
		Me.cmnAttendanceDataOutput.Name = "cmnAttendanceDataOutput"
		Me.cmnAttendanceDataOutput.Size = New System.Drawing.Size(61, 4)
		'
		'animationTimer
		'
		Me.animationTimer.Interval = 10
		'
		'GHRUN_Main
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1484, 861)
		Me.ControlBox = False
		Me.IsMdiContainer = True
		Me.Name = "GHRUN_Main"
		Me.Text = "出講料管理"
		Me.cmnSetting.ResumeLayout(False)
		Me.cmnAttendanceMasterManagement.ResumeLayout(False)
		Me.cmnStartSettings.ResumeLayout(False)
		Me.cmnAttendanceDataManagement.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents cmnSetting As ContextMenuStrip
	Friend WithEvents cmnAttendanceMasterManagement As ContextMenuStrip
	Friend WithEvents cmnAttendanceDataManagement As ContextMenuStrip
	Friend WithEvents MenuItemAttendanceDataImport As ToolStripMenuItem
	Friend WithEvents MenuItemAllowanceDeductionImport As ToolStripMenuItem
	Friend WithEvents MenuItemTrans_Exp_Mngmnt As ToolStripMenuItem
	Friend WithEvents MenuItemConfirmationProcess As ToolStripMenuItem
	Friend WithEvents cmnAttendanceDataOutput As ContextMenuStrip
	Friend WithEvents MenuItemMasterDataImport As ToolStripMenuItem
	Friend WithEvents MenuItemCheckRewards As ToolStripMenuItem
	Friend WithEvents cmnStartSettings As ContextMenuStrip
	Friend WithEvents MenuItemSelectMonth As ToolStripMenuItem
	Friend WithEvents MenuItemStampCheck As ToolStripMenuItem
	Friend WithEvents animationTimer As Timer
End Class
