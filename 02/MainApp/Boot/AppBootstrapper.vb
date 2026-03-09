''' <summary>
''' アプリケーションの初期化と起動処理を管理するクラス。
''' </summary>
''' <remarks>
''' ・起動フェーズを段階的に実行し、例外が発生した場合は ErrorFacade に委譲する。
''' ・Initialize → Run の順で呼び出される。
''' </remarks>
Public NotInheritable Class AppBootstrapper

  ''' <summary>
  ''' アプリケーションの初期化処理を実行する。
  ''' </summary>
  Public Shared Sub Initialize()
    Try
      LoadConfig()
      SetupLogging()
      SetupDI()
      SetupGlobalState()

    Catch ex As Exception
            'ErrorFacade.Handle(ex, "BootstrapInitialize")
            Throw
    End Try
  End Sub

  ''' <summary>
  ''' アプリケーションの実行（メインフォームの起動など）。
  ''' </summary>
  Public Shared Sub Run()
    Try
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)
      Application.Run(New GHRUN_Main())

    Catch ex As Exception
            'ErrorFacade.Handle(ex, "BootstrapRun")
            Throw
    End Try
  End Sub

  '---------------------------------------------
  ' ▼ 起動フェーズ（例示）
  '---------------------------------------------

  Private Shared Sub LoadConfig()
    ' 設定ファイル読み込み
  End Sub

  Private Shared Sub SetupLogging()
    ' ログ初期化
  End Sub

  Private Shared Sub SetupDI()
    ' DI コンテナ初期化
  End Sub

  Private Shared Sub SetupGlobalState()
    ' GlobalState 初期値設定
  End Sub

End Class