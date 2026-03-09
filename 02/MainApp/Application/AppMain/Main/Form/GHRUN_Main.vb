Imports System.ComponentModel
Imports AppParts
Imports LecturePay.Common.Infrastructure
Imports MasterImport
Imports Microsoft.Win32
Imports PunchDataImport
Imports Verification

''' <summary>
''' 出講料計算メイン画面
''' </summary>
Public Class GHRUN_Main
    Inherits BaseForm

#Region "WIN32 API宣言と定数"

#Region "*** WIN32 API宣言 ***"

    Private Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    Private Declare Auto Function ReleaseCapture Lib "user32.dll" () As Boolean

#End Region

#Region "*** Windowsイベント定数 ***"

    ' 境界線の幅 (ピクセル)
    Private Const BORDER_WIDTH As Integer = 10

    ' Windows APIの定義: リサイズ用
    ''' <summary>
    ''' 左側の境界線
    ''' </summary>
    Private Const HT_LEFT As Integer = 10

    ''' <summary>
    ''' 右側の境界線
    ''' </summary>
    Private Const HT_RIGHT As Integer = 11

    ''' <summary>
    ''' 上側の境界線
    ''' </summary>
    Private Const HT_TOP As Integer = 12

    ''' <summary>
    ''' 左上隅の境界線
    ''' </summary>
    Private Const HT_TOPLEFT As Integer = 13

    ''' <summary>
    ''' 右上隅の境界線
    ''' </summary>
    Private Const HT_TOPRIGHT As Integer = 14

    ''' <summary>
    ''' 
    ''' </summary>
    Private Const HT_BOTTOM As Integer = 15

    ''' <summary>
    ''' 左下隅の境界線
    ''' </summary>
    Private Const HT_BOTTOMLEFT As Integer = 16

    ''' <summary>
    ''' 右下隅の境界線
    ''' </summary>
    Private Const HT_BOTTOMRIGHT As Integer = 17

    ''' <summary>
    ''' 特定の画面座標に対応するウィンドウの部分を決定するために、ウィンドウに送信されます。
    ''' </summary>
    Private Const WM_NCHITTEST As Integer = &H84

    ''' <summary>
    ''' カーソルがウィンドウの非クライアント領域内にあるときに、マウスの左ボタンを押すと投稿されます。
    ''' </summary>
    Private Const WM_NCLBUTTONDOWN As UInteger = &HA1

    ''' <summary>
    ''' ウィンドウのタイトルバー（キャプション）領域
    ''' </summary>
    Private Const HT_CAPTION As UInteger = &H2

#End Region

#End Region

#Region "画面定数"

    ' TODO: サイドメニューの開閉用
    Private Const SIDE_PANEL_WIDTH As Integer = 240

    ''' <summary>
    ''' タイトルパネルの構成部品定数
    ''' </summary>
    ''' *** 配置位置情報 ***
    Private Const TITLE_MARGIN As Integer = 10
    Private Const TITLE_SEPARATE As Integer = 40
    Private Const TITLE_PANEL_MENU_CTRL_LEFT As Integer = TITLE_MARGIN
    Private Const TITLE_PANEL_APPL_NAME_LEFT As Integer = TITLE_MARGIN + TITLE_MENU_CTRL_WIDTH
    Private Const TITLE_PANEL_YEAR_MONTH_LEFT As Integer = SIDE_PANEL_WIDTH
    Private Const TITLE_PANEL_LOCK_STATUS_LEFT As Integer = TITLE_PANEL_YEAR_MONTH_LEFT + TITLE_YEAR_MONTH_WIDTH
    Private Const TITLE_PANEL_SYSTEM_DATE_RLEFT As Integer = TITLE_SYSTEM_DATE_WIDTH + TITLE_MARGIN + TITLE_PANEL_USER_NAME_RLEFT
    Private Const TITLE_PANEL_USER_NAME_RLEFT As Integer = TITLE_USER_NAME_WIDTH + TITLE_MARGIN + TITLE_PANEL_MIN_RLEFT
    Private Const TITLE_PANEL_MIN_RLEFT As Integer = TITLE_PANEL_MAX_RLEFT + TITLE_MIN_WIDTH
    Private Const TITLE_PANEL_MAX_RLEFT As Integer = TITLE_MAX_WIDTH + TITLE_PANEL_CLOSE_RLEFT
    Private Const TITLE_PANEL_CLOSE_RLEFT As Integer = TITLE_MARGIN + TITLE_CLOSE_WIDTH

    ''' <summary>
    ''' タイトルパネル構成部品定数（サイズ）
    ''' </summary>
    ''' *** 部品サイズ情報 ***
    Private Const TITLE_MENU_CTRL_WIDTH As Integer = 30
    Private Const TITLE_APPL_NAME_WIDTH As Integer = 200
    Private Const TITLE_YEAR_MONTH_WIDTH As Integer = 200
    Private Const TITLE_USER_NAME_WIDTH As Integer = 200
    Private Const TITLE_SYSTEM_DATE_WIDTH As Integer = 300
    Private Const TITLE_MIN_WIDTH As Integer = 30
    Private Const TITLE_MAX_WIDTH As Integer = 30
    Private Const TITLE_CLOSE_WIDTH As Integer = 80

    ''' <summary>
    ''' タイトルバー背景色
    ''' </summary>
    Private COLOR_TITLE_BAR As Color = Color.DarkSlateGray

    ''' <summary>
    ''' ボタン活性化時の色
    ''' </summary>
    Private COLOR_BUTTON_ENABLE As Color = Color.LightGoldenrodYellow

    ''' <summary>
    ''' ボタン非活性時の色
    ''' </summary>
    Private COLOR_BUTTON_DEFAULT As Color = Color.LightGray

#End Region

#Region "画面主要管理変数関連"

    ''' <summary>
    ''' コンポーネントイベント情報
    ''' </summary>
    Private Class EventInfo
        ' Name属性
        Property Name As String
        ' Text属性
        Property Text As String
        ' イベントハンドラ
        Property eventHandler As EventHandler
    End Class

    ''' <summary>
    ''' 画面表示管理変数：画面表示サイズ：高さ
    ''' </summary>
    Private Property CcreenHeight As Integer = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height

    ''' <summary>
    ''' 画面表示管理変数：画面表示サイズ：幅
    ''' </summary>
    ''' <returns></returns>
    Private Property ScreenWidth As Integer = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width

    Private isExpanding As Boolean = False
    Private isCollapsing As Boolean = False
    Private sidePanelWidth As Integer = 240
    Private minWidth As Integer = 0
    Private animationStep As Integer = 10


    ''' <summary>
    ''' UI スレッド登録および StateDispatcher.Attach が実行済みかどうかを示すフラグ。
    ''' </summary>
    Private Shared _initialized As Boolean = False

#End Region

#Region "コンポーネント管理変数"

    ''' <summary>
    ''' タイトルバー内：メニュー制御
    ''' </summary>
    Private btnMenuCtrl As Button

    ''' <summary>
    ''' 画面タイトル
    ''' </summary>
    Private lblTitle As Label

    ''' <summary>
    ''' タイトルバー内：年月度
    ''' </summary>
    Private lblYM As Label

    ''' <summary>
    ''' タイトルバー内：ロック状態
    ''' </summary>
    Private lblLock As Label

    ''' <summary>
    ''' タイトルバー内：ユーザー名
    ''' </summary>
    Private lblUser As Label

    ''' <summary>
    ''' タイトルバー内：システム日付
    ''' </summary>
    Private lblSystemDate As Label

    ''' <summary>
    ''' タイトルバー内：最小化
    ''' </summary>
    Private btnMinimize As Button

    ''' <summary>
    ''' タイトルバー内：最大化
    ''' </summary>
    Private btnMaximize As Button

    ''' <summary>
    ''' タイトルバー内：閉じる
    ''' </summary>
    Private btnAppClose As Button

    ''' <summary>
    ''' タイトルバーパネル
    ''' </summary>
    Private pnlTitle As Panel

    ''' <summary>
    ''' サイドメニューパネル
    ''' </summary>
    Private pnlMenu As Panel

    ''' <summary>
    ''' サイドメニュー開閉トグル
    ''' </summary>
    Private btnToggle As Button

    ''' <summary>
    ''' サイドメニューボタン
    ''' </summary>
    Public menuButtons As New List(Of Button)

    ''' <summary>
    ''' サイドサブメニューボタン 
    ''' </summary>
    Private subMenuButtons As New List(Of Button)

    ''' <summary>
    ''' ユーザ
    ''' </summary>
    Private currentUser As String = Environment.UserName

    ''' <summary>
    ''' 対象年月度
    ''' </summary>
    Private currentYM As String

    ''' <summary>
    ''' 展開中メニューインデックス
    ''' </summary>
    Private currentExpandedIndex As Integer = -1

#End Region


#Region "画面クラス初期化処理"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        MyBase.New("出講料計算メイン", False)
        InitializeComponent()

        'FormLogWrapper.WrapFormLifecycle(Me)
        'FormLogWrapper.WrapAllEvents(Me)

        ' MDI画面初期設定
        Me.IsMdiContainer = True
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.Black
        Me.MinimumSize = New Size(960, 540)
        animationTimer.Interval = 10

        ' 画面サイズ変更関連イベントハンドラ追加
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
        AddHandler SystemEvents.UserPreferenceChanged, AddressOf OnUserPreferenceChanged

        InitializeWindow()
    End Sub

    ''' <summary>
    ''' メイン画面初期化処理
    ''' </summary>
    Private Sub InitializeWindow()
        ' タイトルパネルに部品配置
        AddTitleButton()

        ' メニューパネルに部品配置
        AddMainButton("年月度設定", AddressOf ShowMonthSettingSubmenu)
        AddMainButton("勤怠データ管理", AddressOf ShowAttendanceDataManagementSubMenu)
        AddMainButton("勤怠マスタ管理", AddressOf ShowAttendanceMasterManagementSubMenu)
        AddMainButton("出講管理情報出力", AddressOf ShowAttendanceDataOutputSubMenu)
        AddMainButton("設定", AddressOf ShowSettingSubMenu)
        LayoutMenu()
    End Sub

#End Region

#Region "Windowsイベント処理"

    ''' <summary>
    ''' Loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler Me.MdiChildActivate, AddressOf OnMdiChildActivated
        If Not _initialized Then
            StateDispatcher.InitializeForUIThread()
            StateDispatcher.Attach()
            _initialized = True
        End If

        DisplayInitialize()
        TitleLayoutRefresh()
        AddMouseEventHandler()
    End Sub

    ''' <summary>
    ''' Closingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim result As New ResultData

        Try
            ' 自分の使用していた対象年月のロック解除
            Dim lockService As New YearMonthLockService()
            result = lockService.UnLockedTargetYearMonth(AppState.TargetYearMonth)
            If Not result.Success Then
                MsgBox(MessageIdConst.MSGID_E2011)
            End If
        Catch ex As Exception
            MsgBox(MessageIdConst.MSGID_E9001)
            result.Success = False
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' フォームクローズ時に購読解除を行う（メモリリーク防止）。
    ''' </summary>
    ''' <param name="e">イベントデータ</param>
    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        RemoveHandler StateDispatcher.UiStateChanged, AddressOf OnUiStateChanged
        MyBase.OnFormClosed(e)
    End Sub

    ''' <summary>
    ''' MDI子フォームがアクティブ化された際に呼び出される。
    ''' </summary>
    ''' <param name="sender">イベントの送信元</param>
    ''' <param name="e">イベントデータ</param>
    ''' <remarks>
    ''' 派生クラスで、アクティブな MDI子フォームに応じた UI更新や状態反映を行う場合にオーバーライドする。
    ''' </remarks>
    Protected Overridable Sub OnMdiChildActivated(sender As Object, e As EventArgs)
        Dim child = Me.ActiveMdiChild

        ' メニューボタン表示制御
        SideMenuEnable()

        If child Is Nothing Then
            Return
        End If

        ' 子フォーム起動時に何かする場合、ここに処理を実装予定
    End Sub

    ''' <summary>
    ''' 画面リサイズイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        WindowResize()
    End Sub

    ''' <summary>
    ''' Resizeイベントの処理
    ''' </summary>
    Private Sub WindowResize()
        'If Me.WindowState = FormWindowState.Normal Then
        '	CcreenHeight = Me.Height
        '	ScreenWidth = Me.Width
        'ElseIf Me.WindowState = FormWindowState.Maximized Then
        '	CcreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        '	ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
        'End If

        Dim scrSize As Size = WinApiHelper.ScreenSize(Me)
        CcreenHeight = scrSize.Height
        ScreenWidth = scrSize.Width

        TitleLayoutRefresh()
    End Sub

    ''' <summary>
    ''' DisplaySettingsChangedイベント（ユーザーが表示設定を変更すると発生します。）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnDisplaySettingsChanged(sender As Object, e As EventArgs)
        WindowResize()
    End Sub

    ''' <summary>
    ''' UserPreferenceChangedイベント（ユーザー設定が変更されると発生します。）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnUserPreferenceChanged(sender As Object, e As UserPreferenceChangedEventArgs)
        If e.Category = UserPreferenceCategory.Window Then
            WindowResize()
        End If
    End Sub

    ''' <summary>
    ''' 画面クローズイベント(FormClosed)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GHRUN_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
        RemoveHandler SystemEvents.UserPreferenceChanged, AddressOf OnUserPreferenceChanged
    End Sub

    ''' <summary>
    ''' [MouseDown]イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AllControls_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            ' どのコントロール上の座標かを、フォーム基準の座標に変換する
            Dim screenPoint As Point = CType(sender, Control).PointToScreen(e.Location)
            Dim formPoint As Point = Me.PointToClient(screenPoint)

            ' フォーム基準の座標でサイズ変更方向を判断する
            Dim resizeDir As UInteger = GetHitTestResult(formPoint)

            ' システムに操作開始を指示する
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, CType(resizeDir, IntPtr), IntPtr.Zero)
        End If
    End Sub

    ''' <summary>
    ''' [MouseMove]イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AllControls_MouseMove(sender As Object, e As MouseEventArgs)
        ' ドラッグ中：カーソル形状変更なし
        If e.Button = MouseButtons.Left Then
            Exit Sub
        End If

        ' フォーム基準の座標取得
        Dim screenPoint As Point = CType(sender, Control).PointToScreen(e.Location)
        Dim formPoint As Point = Me.PointToClient(screenPoint)

        ' どの方向にサイズ変更可能か判定
        Dim resizeDir As UInteger = GetHitTestResult(formPoint)

        ' カーソル形状を設定
        Select Case resizeDir
            Case HT_LEFT, HT_RIGHT
                Me.Cursor = Cursors.SizeWE
            Case HT_TOP, HT_BOTTOM
                Me.Cursor = Cursors.SizeNS
            Case HT_TOPLEFT, HT_BOTTOMRIGHT
                Me.Cursor = Cursors.SizeNWSE
            Case HT_TOPRIGHT, HT_BOTTOMLEFT
                Me.Cursor = Cursors.SizeNESW
            Case Else
                Me.Cursor = Cursors.Default
        End Select
    End Sub

    ''' <summary>
    ''' Windowsメッセージを処理(WndProcオーバーライド)
    ''' </summary>
    ''' <param name="m"></param>
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCCALCSIZE As Integer = &H83
        Const WM_NCHITTEST As Integer = &H84

        ' WM_NCCALCSIZE を無視することで、クライアント領域をフォーム全体に広げる
        If m.Msg = WM_NCCALCSIZE Then
            ' デフォルトの処理を続行させるために何もしない
        ElseIf m.Msg = WM_NCHITTEST Then
            ' デフォルトの処理を先に実行
            MyBase.WndProc(m)

            ' タイトルバーや他のシステム領域を示している場合は、そのまま返す
            If m.Result.ToInt32() <> HT_CAPTION AndAlso m.Result.ToInt32() <> 0 Then
                Return
            End If

            ' クライアント領域内でサイズ変更の境界線を判定する
            Dim screenPoint As Point = New Point(m.LParam.ToInt32() And &HFFFF, m.LParam.ToInt32() >> 16)
            Dim clientPoint As Point = Me.PointToClient(screenPoint)
            Dim result As UInteger = GetHitTestResult(clientPoint)
            If result <> 0 Then
                m.Result = CType(result, IntPtr)
                Return
            End If
        End If

        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' グローバル状態の変更イベント(コールバック)。
    ''' </summary>
    ''' <param name="key">変更された状態キー。</param>
    ''' <param name="value">新しい値。</param>
    Public Overrides Sub OnUiStateChanged(key As String, value As Object)
        StateUpdateHelper.UpdateState(Me, key, value)
    End Sub

#End Region

#Region "初期化処理：タイトルバー"

    ''' <summary>
    ''' タイトルバーの追加処理
    ''' </summary>
    Private Sub AddTitleButton()
        ' メニューパネル
        pnlMenu = New Panel With {.Dock = DockStyle.Left, .Width = SIDE_PANEL_WIDTH, .AutoScroll = True, .BackColor = Color.FromArgb(&HFFCAE6CA)}
        Me.Controls.Add(pnlMenu)

        pnlTitle = New Panel With {.Name = "HeaderTitlePanel", .Dock = DockStyle.Top, .Height = 40, .BackColor = Color.DarkSlateGray}

        ' タイトル：ハンバーガー
        btnToggle = New Button With {.Text = "Ξ", .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                .Size = New Size(30, 30), .ForeColor = Color.Black, .BackColor = COLOR_BUTTON_DEFAULT,
                .Location = New Point(10, 5), .Anchor = AnchorStyles.Top Or AnchorStyles.Left}
        AddHandler btnToggle.Click, AddressOf toggleButton_Click
        pnlTitle.Controls.Add(btnToggle)

        ' タイトル：画面タイトル
        lblTitle = New Label With {.Name = "lblAppliName", .Text = AppCommon.ApplicationName, .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                  .Location = New Point(10, 10), .Height = 30,
                  .ForeColor = Color.White, .BackColor = Color.DarkSlateGray, .AutoSize = True}
        pnlTitle.Controls.Add(lblTitle)

        ' タイトル：現在の年月度
        lblYM = New Label With {.Name = "lblYearMonth", .Text = AppCommon.GetTargetYearMonth(), .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                  .Location = New Point(10, 10),
                  .ForeColor = Color.White, .BackColor = Color.DarkSlateGray, .AutoSize = True}
        pnlTitle.Controls.Add(lblYM)

        ' タイトル：ロック状態
        lblLock = New Label With {.Name = "lblLockState", .Text = AppCommon.GetLockStatus(), .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                  .Location = New Point(10, 10),
                  .ForeColor = Color.White, .BackColor = Color.DarkSlateGray, .AutoSize = True}
        pnlTitle.Controls.Add(lblLock)

        ' タイトル：使用者
        lblUser = New Label With {.Name = "lblUserName", .Text = AppCommon.GetUser(), .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                  .Location = New Point(10, 10),
                  .ForeColor = Color.White, .BackColor = Color.DarkSlateGray, .AutoSize = True}
        pnlTitle.Controls.Add(lblUser)

#If DEBUG_DEV Then

		' タイトル：日付
		lblSystemDate = New Label With {.Text = "(デバッグ用)日付：", .Font = New Font("游ゴシック", 12, FontStyle.Regular),
							.Location = New Point(10, 10),
							.ForeColor = Color.White, .BackColor = Color.DarkSlateGray, .AutoSize = True}
		pnlTitle.Controls.Add(lblSystemDate)

#End If

        ' タイトル：最小化ボタン
        btnMinimize = New Button With {.Text = "＿", .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                .Size = New Size(30, 30), .ForeColor = Color.Black, .BackColor = COLOR_BUTTON_DEFAULT,
                .Location = New Point(10, 5), .Anchor = AnchorStyles.Top Or AnchorStyles.Left}
        AddHandler btnMinimize.Click, Sub(sender, e) Me.WindowState = FormWindowState.Minimized
        Me.Controls.Add(btnMinimize)

        ' タイトル：最大化ボタン
        btnMaximize = New Button With {.Text = "□", .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                .Size = New Size(30, 30), .ForeColor = Color.Black, .BackColor = COLOR_BUTTON_DEFAULT,
                .Location = New Point(10, 5), .Anchor = AnchorStyles.Top Or AnchorStyles.Left}
        AddHandler btnMaximize.Click, Sub(sender, e)
                                          If Me.WindowState = FormWindowState.Maximized Then
                                              Me.WindowState = FormWindowState.Normal
                                          Else
                                              Me.WindowState = FormWindowState.Maximized
                                          End If
                                      End Sub
        Me.Controls.Add(btnMaximize)

        ' タイトル：閉じるボタン
        btnAppClose = New Button With {.Text = "閉じる", .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                .Size = New Size(80, 30), .ForeColor = Color.Black, .BackColor = COLOR_BUTTON_DEFAULT,
                .Location = New Point(10, 5), .Anchor = AnchorStyles.Top Or AnchorStyles.Left, .Name = "btnAppClose"}
        AddHandler btnAppClose.Click, Sub(sender, e) Me.Close()
        Me.Controls.Add(btnAppClose)

        ' タイトルパネル
        'pnlTitle = New Panel With {.Dock = DockStyle.Top, .Height = 40, .BackColor = Color.DarkSlateGray}
        Me.Controls.Add(pnlTitle)

    End Sub

#End Region

#Region "サブメニュー"

#Region "*** 年月度設定メニュー ***"

    ''' <summary>
    ''' [月度設定]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MenuItemSelectMonth_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of frmTargetYearMonth)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)

        ' [対象年月度]画面表示
        Dim frm As Form = New frmTargetYearMonth
        AddHandler frm.FormClosed, AddressOf frmTargetYearMonth_FormClosed
        ShowMdiChildForm(frm)
    End Sub

    ''' <summary>
    ''' [対象年月度]画面クローズのタイミングでメニューボタンを表示更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmTargetYearMonth_FormClosed(sender As Object, e As FormClosedEventArgs)
        SideMenuEnable()
    End Sub


    Private Sub OnStateChanged(key As String, value As Object)
        StateUpdateHelper.UpdateState(Me, key, value)
    End Sub



#End Region

#Region "*** 勤怠データ管理メニュー ***"

    ''' <summary>
    ''' [勤怠データ取込]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AttendanceDataImport_Click(sender As Object, e As EventArgs)

        If ShowChildForm(Of FrmLectureImport)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmLectureImport)

    End Sub

    ''' <summary>
    ''' [手当・控除データ取込]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AllowanceDeductionDataImport_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmAllowanceDeductionImport)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmAllowanceDeductionImport)
    End Sub

    ''' <summary>
    ''' [交通費管理]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TransportationExpenseManagement_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmTransportExpenses)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmTransportExpenses)
    End Sub

    ''' <summary>
    ''' [勤怠データ確認]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckAttendanceData_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmCheckAttendanceData)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmCheckAttendanceData)
    End Sub

    ''' <summary>
    ''' [締め処理]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ClosingProcess_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FormMonthlyClosing)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FormMonthlyClosing)
    End Sub

    ''' <summary>
    ''' [勤怠データ出力処理]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ConfirmationProcess_Click(sender As Object, e As EventArgs)
        'If ShowChildForm(Of FormMonthlyClosing)() Then
        '    Return
        'End If

        'ContextMenuItem_Click_logger(sender, e)
        'CloseMDIChildScreen(New FormMonthlyClosing)
        MessageBox.Show("未実装")
    End Sub

#End Region

#Region "*** 勤怠マスタ管理メニュー ***"

    ''' <summary>
    ''' [時間割設定データ取込]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimeTableDataImport_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmImportTimetableData)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmImportTimetableData)
    End Sub

    ''' <summary>
    ''' [出講料設定データ取込]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LectureFeeDataImport_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmImportLectureFeeData)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmImportLectureFeeData)
    End Sub

#End Region

#Region "*** 勤怠データ出力メニュー ***"

    ''' <summary>
    ''' [出講情報出力]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LectureInformationOutput_Click(sender As Object, e As EventArgs)
        'If ShowChildForm(Of FrmXxxxxxxx)() Then
        '	Return
        'End If

        'ContextMenuItem_Click_logger(sender, e)
        'CloseMDIChildScreen(New FrmXxxxxxxx)
        MessageBox.Show("未実装")
    End Sub

#End Region

#Region "*** 設定メニュー ***"

    ''' <summary>
    ''' [環境設定]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Preferences_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmSystemEnvSettings)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmSystemEnvSettings)
    End Sub

    ''' <summary>
    ''' [報酬確認]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CheckRewards_Click(sender As Object, e As EventArgs)
        If Not ShowChildForm(Of FrmMonthlyList)() Then
            Dim frm As Form = New FrmMonthlyList(AppState.TargetYearMonth)
            Me.WindowState = FormWindowState.Maximized
            ContextMenuItem_Click_logger(sender, e)
            ShowMdiChildForm(frm)
        Else
            ContextMenuItem_Click_logger(sender, e)
        End If

        For Each frm As Form In Me.MdiChildren
            If Not frm.Name.Equals("FrmMonthlyList") AndAlso Not frm.Name.Equals("FrmDaylyList") Then
                frm.Close()
            End If
        Next
    End Sub

    ''' <summary>
    ''' [ロック状況]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LockHistory_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmLockHistory)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmLockHistory)
    End Sub

    ''' <summary>
    ''' [取込履歴]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ImportHistory_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmImportHistory)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmImportHistory)
    End Sub

    ''' <summary>
    ''' [操作履歴]メニューボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OperationHistory_Click(sender As Object, e As EventArgs)
        If ShowChildForm(Of FrmOperationHistory)() Then
            Return
        End If

        ContextMenuItem_Click_logger(sender, e)
        CloseMDIChildScreen(New FrmOperationHistory)
    End Sub



#End Region

#End Region

#Region "サイドメニュー関連処理"

    ''' <summary>
    ''' メニューボタン追加
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="clickHandler"></param>
    Private Sub AddMainButton(ByVal text As String, clickHandler As EventHandler)
        Dim btn As New Button With {
            .Text = text,
            .Width = pnlMenu.Width - 40, .Left = 20, .Height = 35,
            .BackColor = COLOR_BUTTON_DEFAULT, .Font = New Font("游ゴシック", 12, FontStyle.Regular),
            .Tag = "NoLog"
          }
        AddHandler btn.Click, clickHandler
        menuButtons.Add(btn)
        pnlMenu.Controls.Add(btn)
    End Sub

    ''' <summary>
    ''' メニュー表示レイアウト調整
    ''' </summary>
    Private Sub LayoutMenu()
        Dim y As Integer = 10
        For i As Integer = 0 To menuButtons.Count - 1
            menuButtons(i).Top = y
            y += menuButtons(i).Height + 15
            If i = currentExpandedIndex Then
                y -= 10
                For Each subBtn In subMenuButtons
                    subBtn.Top = y
                    pnlMenu.Controls.Add(subBtn)
                    y += subBtn.Height + 3
                Next
                y += 20
            End If
        Next
    End Sub

    ''' <summary>
    ''' サブメニューボタン表示
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="eventInfo"></param>
    Private Sub ShowSubMenu(ByVal index As Integer, ByVal eventInfo As List(Of EventInfo))
        If currentExpandedIndex >= 0 Then
            menuButtons(currentExpandedIndex).BackColor = COLOR_BUTTON_DEFAULT
        End If
        If index = currentExpandedIndex Then
            ClearSubMenu()
            LayoutMenu()
            Return
        End If

        ClearSubMenu()
        currentExpandedIndex = index
        menuButtons(currentExpandedIndex).BackColor = COLOR_BUTTON_ENABLE
        For Each info In eventInfo
            Dim btn As New Button With {
                .Name = info.Name, .Text = info.Text,
                .Width = pnlMenu.Width - 40, .Left = 35, .Height = 30,
                .BackColor = COLOR_BUTTON_ENABLE, .Font = New Font("游ゴシック", 12, FontStyle.Regular),
                .Tag = "NoLog"
              }
            AddHandler btn.Click, info.eventHandler
            subMenuButtons.Add(btn)
        Next
        LayoutMenu()
    End Sub

    ''' <summary>
    ''' [年月度設定]ボタン押下時処理（サブメニューボタン表示）
    ''' </summary>
    Private Sub ShowMonthSettingSubmenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
          New EventInfo With {.Name = "btnMenuItemSelectMonth", .Text = "年月度設定", .eventHandler = AddressOf MenuItemSelectMonth_Click}
        }
        ContextMenu_Click_logger(sender, e)
        ShowSubMenu(0, eventInfo)
    End Sub

    ''' <summary>
    ''' [勤怠データ管理]ボタン押下時処理（サブメニューボタン表示）
    ''' </summary>
    Private Sub ShowAttendanceDataManagementSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
          New EventInfo With {.Name = "btnAttendanceDataImport", .Text = "勤怠データ取込", .eventHandler = AddressOf AttendanceDataImport_Click},
          New EventInfo With {.Name = "btnAllowanceDeductionDataImport", .Text = "手当・控除データ取込", .eventHandler = AddressOf AllowanceDeductionDataImport_Click},
          New EventInfo With {.Name = "btnTravelExpensesMgmt", .Text = "交通費管理", .eventHandler = AddressOf TransportationExpenseManagement_Click},
          New EventInfo With {.Name = "btnCheckAttendanceData", .Text = "勤怠データ確認", .eventHandler = AddressOf CheckAttendanceData_Click},
          New EventInfo With {.Name = "btnClosingProcess", .Text = "締め処理", .eventHandler = AddressOf ClosingProcess_Click},
          New EventInfo With {.Name = "btnAttendanceDataOutput", .Text = "勤怠データ出力", .eventHandler = AddressOf ConfirmationProcess_Click}
        }
        ContextMenu_Click_logger(sender, e)
        ShowSubMenu(1, eventInfo)
    End Sub

    ''' <summary>
    ''' [勤怠マスタ管理]ボタン押下時処理（サブメニューボタン表示）
    ''' </summary>
    Private Sub ShowAttendanceMasterManagementSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
          New EventInfo With {.Name = "btnTimeTableDataImport", .Text = "時間割設定データ取込", .eventHandler = AddressOf TimeTableDataImport_Click},
          New EventInfo With {.Name = "btnLectureFeeDataImport", .Text = "出講料設定データ取込", .eventHandler = AddressOf LectureFeeDataImport_Click}
        }
        ContextMenu_Click_logger(sender, e)
        ShowSubMenu(2, eventInfo)
    End Sub

    ''' <summary>
    ''' [出講管理情報出力]ボタン押下時処理（サブメニューボタン表示）
    ''' </summary>
    Private Sub ShowAttendanceDataOutputSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
          New EventInfo With {.Name = "btnLectureInformationOutput", .Text = "出講管理情報出力", .eventHandler = AddressOf LectureInformationOutput_Click}
        }
        ContextMenu_Click_logger(sender, e)
        ShowSubMenu(3, eventInfo)
    End Sub

    ''' <summary>
    ''' [設定]ボタン押下時処理（サブメニューボタン表示）
    ''' </summary>
    Private Sub ShowSettingSubMenu(sender As Object, e As EventArgs)
        Dim eventInfo As New List(Of EventInfo) From {
          New EventInfo With {.Name = "btnLockHistory", .Text = "年月ロック状況確認", .eventHandler = AddressOf LockHistory_Click},
          New EventInfo With {.Name = "btnImportHistory", .Text = "取込履歴", .eventHandler = AddressOf ImportHistory_Click},
          New EventInfo With {.Name = "btnOperationHistory", .Text = "操作履歴", .eventHandler = AddressOf OperationHistory_Click}
        }

        'New EventInfo With {.Name = "btnPreferences", .Text = "環境設定", .eventHandler = AddressOf Preferences_Click},
        'New EventInfo With {.Name = "btnCheckRewards", .Text = "報酬確認", .eventHandler = AddressOf CheckRewards_Click},

        ContextMenu_Click_logger(sender, e)
        ShowSubMenu(4, eventInfo)
    End Sub

    ''' <summary>
    ''' サブメニューボタンクリア
    ''' </summary>
    Private Sub ClearSubMenu()
        For Each btn In subMenuButtons
            pnlMenu.Controls.Remove(btn)
        Next
        subMenuButtons.Clear()
        currentExpandedIndex = -1
    End Sub

#End Region

#Region "内部共通処理"

    ''' <summary>
    ''' 画面表示初期化
    ''' </summary>
    Public Sub DisplayInitialize()
        ' システム日付取得
        AppState.DebugSystemDate = SystemDateForDebugging()

        ' タイトルバー：情報表示
        lblYM.Text = AppCommon.GetTargetYearMonth()
        lblLock.Text = AppCommon.GetLockStatus()
        lblUser.Text = AppCommon.GetUser()

#If DEBUG_DEV Then

		lblSystemDate.Text = "(デバッグ用)日付：" & AppCommon.SystemDate().ToString("yyyy年MM月dd日")
#End If
        ' メニューボタン表示制御
        SideMenuEnable()

        IsImportComplete = False
    End Sub

    ''' <summary>
    ''' MDI画面の構成部品にマウスイベントのハンドラー追加する。
    ''' </summary>
    Private Sub AddMouseEventHandler()
        ' タイトルパネル
        AddHandler pnlMenu.MouseDown, AddressOf AllControls_MouseDown
        AddHandler pnlMenu.MouseMove, AddressOf AllControls_MouseMove
        ' メニューパネル
        AddHandler pnlTitle.MouseDown, AddressOf AllControls_MouseDown
        AddHandler pnlTitle.MouseMove, AddressOf AllControls_MouseMove

        AddHandler lblTitle.MouseDown, AddressOf AllControls_MouseDown
        AddHandler lblYM.MouseDown, AddressOf AllControls_MouseDown
        AddHandler lblLock.MouseDown, AddressOf AllControls_MouseDown
        AddHandler lblUser.MouseDown, AddressOf AllControls_MouseDown

#If DEBUG_DEV Then
		AddHandler lblSystemDate.MouseDown, AddressOf AllControls_MouseDown
#End If

        ' MDIクライアント
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is System.Windows.Forms.MdiClient Then
                AddHandler ctrl.MouseDown, AddressOf AllControls_MouseDown
                AddHandler ctrl.MouseMove, AddressOf AllControls_MouseMove
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' マウスポインタ境界線/角判定ヘルパー関数
    ''' </summary>
    ''' <param name="p"></param>
    ''' <returns></returns>
    Private Function GetHitTestResult(p As Point) As UInteger
        Dim left As Boolean = p.X < BORDER_WIDTH
        Dim right As Boolean = p.X > Me.Width - BORDER_WIDTH
        Dim top As Boolean = p.Y < BORDER_WIDTH
        Dim bottom As Boolean = p.Y > Me.Height - BORDER_WIDTH

        Select Case True
            Case left And top
                Return HT_TOPLEFT
            Case left And bottom
                Return HT_BOTTOMLEFT
            Case right And top
                Return HT_TOPRIGHT
            Case right And bottom
                Return HT_BOTTOMRIGHT
            Case left
                Return HT_LEFT
            Case right
                Return HT_RIGHT
            Case top
                Return HT_TOP
            Case bottom
                Return HT_BOTTOM
            Case Else
                ' 境界線でなければ、移動のために HT_CAPTION を返す
                Return HT_CAPTION
        End Select
    End Function

    ''' <summary>
    ''' タイトルバー内の部品レイアウトを画面サイズに合わせて調整する。
    ''' </summary>
    Private Sub TitleLayoutRefresh()
        If IsImportComplete Then
            Return
        End If

        btnToggle.Left = TITLE_PANEL_MENU_CTRL_LEFT

        ' 画面タイトル
        lblTitle.Left = TITLE_PANEL_APPL_NAME_LEFT

        ' 現在の年月度
        lblYM.Left = TITLE_PANEL_YEAR_MONTH_LEFT

        ' ロック状態
        lblLock.Left = TITLE_PANEL_LOCK_STATUS_LEFT

#If DEBUG_DEV Then
		' 日付
		lblSystemDate.Left = ScreenWidth - TITLE_PANEL_SYSTEM_DATE_RLEFT
#End If

        ' 使用者
        lblUser.Left = ScreenWidth - TITLE_PANEL_USER_NAME_RLEFT

        ' 最小化ボタン
        btnMinimize.Left = ScreenWidth - TITLE_PANEL_MIN_RLEFT

        ' 最大化ボタン
        btnMaximize.Left = ScreenWidth - TITLE_PANEL_MAX_RLEFT

        ' 閉じるボタン
        btnAppClose.Left = ScreenWidth - TITLE_PANEL_CLOSE_RLEFT

    End Sub

#End Region

#Region "サイドメニューの開閉処理"

    ''' <summary>
    ''' サイドメニューパネル開閉ボタンイベント
    ''' </summary>
    Private Sub toggleButton_Click()
        If pnlMenu.Width > minWidth Then
            isCollapsing = False
            isExpanding = True
        Else
            isCollapsing = True
            isExpanding = False
        End If
        animationTimer.Start()
    End Sub

    ''' <summary>
    ''' サイドメニューの開閉アニメーション処理を行います。
    ''' タイマー処理により
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AnimationTimer_Tick(sender As Object, e As EventArgs) Handles animationTimer.Tick
        If isCollapsing Then
            pnlMenu.Width += animationStep
            If pnlMenu.Width >= sidePanelWidth Then
                pnlMenu.Width = sidePanelWidth
                animationTimer.Stop()
                isExpanding = False
                btnToggle.Text = "Ξ"
            End If
        ElseIf isExpanding Then
            pnlMenu.Width -= animationStep
            If pnlMenu.Width <= minWidth Then
                pnlMenu.Width = minWidth
                animationTimer.Stop()
                isCollapsing = False
                btnToggle.Text = "Ξ"
            End If
        End If
    End Sub

#End Region


#Region "MDI子画面表示制御関連"

    ''' <summary>
    ''' MIDチャイルドフォームを左上に初期表示する。
    ''' </summary>
    ''' <param name="mdiChild">MDI子フォーム</param>
    ''' <param name="startPosition">表示位置</param>
    Public Sub ShowMdiChildForm(ByRef mdiChild As Form, Optional ByVal startPosition As FormStartPosition = FormStartPosition.Manual)
        Dim mdiChildren() As Form = Me.MdiChildren
        Mdihelper.ShowMdiChildFormStart(Me, mdiChild, mdiChildren)
    End Sub

    ''' <summary>
    ''' 画面表示制御（画面表示中はアクティブにする）
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Function ShowChildForm(Of T As Form)() As Boolean
        For Each child As Form In Me.MdiChildren
            If TypeOf child Is T Then
                child.Activate()
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' サイドメニューボタン活性／非活性制御
    ''' </summary>
    Private Sub SideMenuEnable()
        If DateUtil.IsNullOrEmpty(AppState.TargetYearMonth) Then
            menuButtons(1).Enabled = False
            menuButtons(2).Enabled = False
            menuButtons(3).Enabled = False
            menuButtons(4).Enabled = False
        Else
            menuButtons(0).Enabled = True
            menuButtons(1).Enabled = True
            menuButtons(2).Enabled = True
            menuButtons(3).Enabled = True
            menuButtons(4).Enabled = True
        End If
    End Sub






    ''' <summary>
    ''' MDI子画面をすべて閉じる。
    ''' </summary>
    Public Sub CloseAllChildForms()
        For Each child As Form In Me.MdiChildren
            Try
                child.Close()
            Catch ex As Exception
                MessageBox.Show(
                  $"子フォーム {child.Name} を閉じる際にエラー: {ex.Message}",
                  "エラー",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error
                )
            End Try
        Next
    End Sub

#End Region



    ''' <summary>
    ''' 現在表示中の画面を閉じます。
    ''' </summary>
    ''' <param name="newScreen"></param>
    Private Sub CloseMDIChildScreen(ByVal newScreen As Form)
        ShowMdiChildForm(newScreen)
        For Each frm As Form In Me.MdiChildren
            If Not frm.Name.Equals(newScreen.Name) Then
                frm.Close()
            End If
        Next
    End Sub


#Region "デバッグ用処理"

    ''' <summary>
    ''' システム日付を取得する
    ''' </summary>
    ''' <returns></returns>
    Private Function SystemDateForDebugging() As Date
        Dim result As Date = Now()

#If DEBUG Then
        If My.Application.CommandLineArgs.Count > 0 Then
            For Each arg As String In My.Application.CommandLineArgs
                Dim param() As String = arg.ToUpper().Split("=")
                If param(0) = "SYSTEM_DATE" Then
                    If Not Date.TryParse(param(1), result) Then
                        result = Now()
                    End If
                    Exit For
                End If
            Next
        End If
#End If

        Return result
    End Function

#End Region

End Class