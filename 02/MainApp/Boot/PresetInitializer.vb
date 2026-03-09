Public Class PresetInitializer

  Public Shared Sub RegisterPresets()

    ' 画面表示（Load）用プリセット
    LogPresetRegistry.Register("DefaultLoad",
      New LogAttribute With {
        .Name = "Form.Load",
        .Level = LogLevels.Info,
        .MeasureDuration = False,
        .Template = "{EventName}-{Message}",
        .Target = LogTargets.Database
      }
    )

    ' 画面終了（FormClosing）用プリセット
    LogPresetRegistry.Register("DefaultClosing",
      New LogAttribute With {
        .Name = "Form.Closing",
        .Level = LogLevels.Info,
        .MeasureDuration = False,
        .Template = "{EventName}-{Message}",
        .Target = LogTargets.Database
      }
    )

    ' 標準クリック用プリセット
    LogPresetRegistry.Register("DefaultClick",
      New LogAttribute With {
        .Level = LogLevels.Info,
        .MeasureDuration = True,
        .Template = "{EventName}-{Message}",
        .Target = LogTargets.Database,
        .AllowEvents = New String() {"Click"}
      }
    )

    ' 例外ログ用プリセット
    LogPresetRegistry.Register("ErrorLog",
      New LogAttribute With {
        .Level = LogLevels.Error,
        .MeasureDuration = False,
        .Template = "{EventName} [ERROR] {Message}",
        .Target = LogTargets.Database
      }
    )

  End Sub

End Class
