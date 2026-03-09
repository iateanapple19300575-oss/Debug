
Imports System.Threading

Public Class StartUp
	<STAThread()>
	Shared Sub Main()
		Dim result As Boolean = False
		Dim hasHandle As Boolean = False
		Dim mutexName As String = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath)

		' Mutexオブジェクトを作成する
		Dim mutex As New System.Threading.Mutex(False, mutexName)

		Try
			Try
				' ミューテックスの所有権を要求する
				hasHandle = mutex.WaitOne(0, False)
				' .NET Framework 2.0以降の場合
			Catch ex As System.Threading.AbandonedMutexException
				' 別のアプリケーションがミューテックスを解放しないで終了した時
				hasHandle = True
			End Try

			If hasHandle = False Then
				MessageBox.Show(MessageIdConst.MSGID_E9002)
				Return
			End If

            ' ★ Settings.settings から読み込み
            Dim logDir As String = "D:\App\Log"
            Dim retention As Integer = 30

            Dim minLevel As LogLevel =
            LogLevelHelper.Parse(1, LogLevel.Info)

            Dim fileLogger As New SingleFileLogger(logDir, retention)
            fileLogger.MinimumLevel = minLevel

            Dim loggers As New List(Of Action(Of SqlLogEntry))
            loggers.Add(AddressOf FileLogger.Write)
            Dim multi As New MultiLogger(loggers.ToArray())
            SqlExecutor.LogWriter = AddressOf multi.Write


            PresetInitializer.RegisterPresets()
			AppBootstrapper.Initialize()
			AppBootstrapper.Run()

			'Application.EnableVisualStyles()
			'Application.SetCompatibleTextRenderingDefault(False)
			'Application.Run(New GHRUN_Main())

		Catch ex As Exception
            ' 起動時の致命的例外
            Debug.WriteLine("Error")
        Finally
			If hasHandle Then
				'ミューテックスを解放する
				mutex.ReleaseMutex()
			End If
			mutex.Close()
		End Try
	End Sub
End Class
