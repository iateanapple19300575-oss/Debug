Imports System.IO
Imports System.Text

''' <summary>
''' パス操作をまとめた FW3.5 専用 PathHelper。
''' </summary>
Public NotInheritable Class PathHelper

    Private Sub New()
    End Sub

    '===========================================================
    ' パス正規化（\ に統一）
    '===========================================================
    ''' <summary>
    ''' パス区切り文字を \ に統一し、不要な空白を除去します。
    ''' </summary>
    Public Shared Function Normalize(path As String) As String
        If String.IsNullOrEmpty(path) Then Return ""

        path = path.Trim()
        path = path.Replace("/", "\")

        ' \\ を \ に統一
        While path.Contains("\\")
            path = path.Replace("\\", "\")
        End While

        Return path
    End Function

    '===========================================================
    ' パス結合（FW3.5 安全版）
    '===========================================================
    ''' <summary>
    ''' パスを安全に結合します（Normalize 付き）。
    ''' </summary>
    Public Shared Function Combine(basePath As String, relative As String) As String
        basePath = Normalize(basePath)
        relative = Normalize(relative)

        If basePath.EndsWith("\") Then
            Return basePath & relative
        Else
            Return basePath & "\" & relative
        End If
    End Function

    '===========================================================
    ' ファイル名取得
    '===========================================================
    Public Shared Function GetFileName(filePath As String) As String
        filePath = Normalize(filePath)
        Return Path.GetFileName(filePath)
    End Function

    '===========================================================
    ' 拡張子取得
    '===========================================================
    Public Shared Function GetExtension(filePath As String) As String
        filePath = Normalize(filePath)
        Return Path.GetExtension(filePath)
    End Function

    '===========================================================
    ' 親フォルダ取得
    '===========================================================
    Public Shared Function GetDirectory(filePath As String) As String
        filePath = Normalize(filePath)
        Return Path.GetDirectoryName(filePath)
    End Function

    '===========================================================
    ' 禁止文字除去（Windows ファイル名安全化）
    '===========================================================
    Private Shared ReadOnly InvalidChars As Char() =
        Path.GetInvalidFileNameChars()

    ''' <summary>
    ''' Windows の禁止文字を除去した安全なファイル名を返します。
    ''' </summary>
    Public Shared Function SanitizeFileName(name As String) As String
        If String.IsNullOrEmpty(name) Then Return ""

        Dim sb As New StringBuilder()

        For Each ch As Char In name
            If Array.IndexOf(InvalidChars, ch) = -1 Then
                sb.Append(ch)
            End If
        Next

        Return sb.ToString()
    End Function

    '===========================================================
    ' 一時フォルダ生成
    '===========================================================
    ''' <summary>
    ''' 一時フォルダを生成して返します（存在しなければ作成）。
    ''' </summary>
    Public Shared Function GetTempFolder() As String
        Dim temp As String = Normalize(Path.GetTempPath())
        If Not Directory.Exists(temp) Then
            Directory.CreateDirectory(temp)
        End If
        Return temp
    End Function

    '===========================================================
    ' 日付フォルダ生成（yyyyMMdd）
    '===========================================================
    ''' <summary>
    ''' yyyyMMdd の日付フォルダを basePath の下に作成して返します。
    ''' </summary>
    Public Shared Function CreateDateFolder(basePath As String) As String
        basePath = Normalize(basePath)
        Dim folder As String = Combine(basePath, DateTime.Now.ToString("yyyyMMdd"))

        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If

        Return folder
    End Function

    '===========================================================
    ' パスがファイルかフォルダか判定
    '===========================================================
    Public Shared Function IsFile(path As String) As Boolean
        Return File.Exists(Normalize(path))
    End Function

    Public Shared Function IsDirectory(path As String) As Boolean
        Return Directory.Exists(Normalize(path))
    End Function

    '===========================================================
    ' パスの存在チェック
    '===========================================================
    Public Shared Function Exists(path As String) As Boolean
        path = Normalize(path)
        Return File.Exists(path) OrElse Directory.Exists(path)
    End Function

End Class