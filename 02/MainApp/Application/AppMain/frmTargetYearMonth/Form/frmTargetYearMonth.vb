Imports LecturePay.Common.Infrastructure
Imports MainApp.Data.Entity

'''' <summary>
'''' 対象年月度設定画面
'''' </summary>
Public Class frmTargetYearMonth
    Inherits BaseForm

    ''' <summary>
    ''' 対象年月度ロックサービス
    ''' </summary>
    Private lockService As New YearMonthLockService


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        MyBase.New("月度設定")
        InitializeComponent()

        FormLogWrapper.WrapFormLifecycle(Me)
        FormLogWrapper.WrapAllEvents(Me)
    End Sub

#Region "Windowsイベント処理"

    ''' <summary>
    ''' Loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmTargetYearMonth_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitDisplay()
    End Sub

#End Region

#Region "画面イベント処理"

    ''' <summary>
    ''' [OK]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        ' 対象年月の取得
        Dim year As String = txtYear.Text
        Dim month As String = txtMonth.Text
        Dim yearMonthStr As String = String.Concat(year, month.PadLeft(2, "0"))
        Dim result As New ResultData

        Try
            ' 年月度ロック
            Dim newTargetYearMonth As Date = DateUtil.NoFormatYearMonthStrToDate(yearMonthStr)
            result = lockService.YearMonthLock(newTargetYearMonth, AppState.TargetYearMonth)

            ' ロック失敗
            If Not result.Success Then
                MsgBox(MessageIdConst.MSGID_E2009)
                Return
            End If

            ' 他ユーザロック中
            If result.SubStatus = 1 Then
                ' 使用中：メッセージ表示
                Dim resultRec As YearMonthLockEntity = result.ResDataClass
                Dim message As String = String.Concat(
                    "選択した対象年月は現在使用中です", vbCrLf,
                    "使用者  =[", resultRec.Lock_User, "]", vbCrLf,
                    "使用PC  =[", resultRec.Lock_Pc, "]", vbCrLf,
                    "使用開始=[", resultRec.Lock_Datetime, "]")
                MsgBox(message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                Return
            Else
                AppState.LockStatus = True
                AppState.TargetYearMonth = newTargetYearMonth
            End If

        Catch ex As Exception
            MsgBox(MessageIdConst.MSGID_E9001)

        Finally
            If result.Success Then
                Me.Close()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' [キャンセル]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region

#Region "内部共通処理"

    ''' <summary>
    ''' 初期画面表示処理
    ''' </summary>
    Private Sub InitDisplay()
        ' 対象年月度：初期表示
        If DateUtil.IsNullOrEmpty(AppState.TargetYearMonth) Then
            ' システム日付の年月
            txtYear.Text = AppCommon.SystemDate.ToString("yyyy")
            txtMonth.Text = AppCommon.SystemDate.ToString("MM")
        Else
            ' 現在の対象年月
            txtYear.Text = AppState.TargetYearMonth.ToString("yyyy")
            txtMonth.Text = AppState.TargetYearMonth.ToString("MM")
        End If
    End Sub

#End Region

End Class