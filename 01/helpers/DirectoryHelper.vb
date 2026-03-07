Imports System.IO

''' <summary>
''' フォルダ操作をまとめた FW3.5 専用 DirectoryHelper。
''' </summary>
Public NotInheritable Class DirectoryHelper

    Private Sub New()
    End Sub

    '===========================================================
    ' 安全なフォルダ作成
    '===========================================================
    ''' <summary>
    ''' フォルダを安全に作成します（存在していても例外なし）。
    ''' </summary>
    Public Shared Function CreateDirectorySafe(path As String) As Boolean
        Try
            path = PathHelper.Normalize(path)
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            Return True
        Catch
            Return False
        End Try
    End Function

    '===========================================================
    ' 安全なフォルダ削除（中身があっても削除）
    '===========================================================
    ''' <summary>
    ''' フォルダを安全に削除します（中身があっても削除）。
    ''' </summary>
    Public Shared Function DeleteDirectorySafe(path As String) As Boolean
        Try
            path = PathHelper.Normalize(path)
            If Directory.Exists(path) Then
                Directory.Delete(path, True)
            End If
            Return True
        Catch
            Return False
        End Try
    End Function

    '===========================================================
    ' フォルダコピー（再帰）
    '===========================================================
    ''' <summary>
    ''' フォルダを再帰的にコピーします。
    ''' </summary>
    Public Shared Function CopyDirectory(src As String, dest As String) As Boolean
        Try
            src = PathHelper.Normalize(src)
            dest = PathHelper.Normalize(dest)

            If Not Directory.Exists(src) Then Return False

            If Not Directory.Exists(dest) Then
                Directory.CreateDirectory(dest)
            End If

            ' ファイルコピー
            For Each fileNm As String In Directory.GetFiles(src)
                Dim fileName As String = Path.GetFileName(fileNm)
                File.Copy(fileNm, PathHelper.Combine(dest, fileName), True)
            Next

            ' サブフォルダ再帰コピー
            For Each dir As String In Directory.GetDirectories(src)
                Dim dirName As String = Path.GetFileName(dir)
                CopyDirectory(dir, PathHelper.Combine(dest, dirName))
            Next

            Return True
        Catch
            Return False
        End Try
    End Function

    '===========================================================
    ' フォルダ内のファイル一覧取得
    '===========================================================
    Public Shared Function GetFiles(path As String) As String()
        Try
            path = PathHelper.Normalize(path)
            If Directory.Exists(path) Then
                Return Directory.GetFiles(path)
            End If
        Catch
        End Try
        Return New String() {}
    End Function

    '===========================================================
    ' フォルダ内のサブフォルダ一覧取得
    '===========================================================
    Public Shared Function GetDirectories(path As String) As String()
        Try
            path = PathHelper.Normalize(path)
            If Directory.Exists(path) Then
                Return Directory.GetDirectories(path)
            End If
        Catch
        End Try
        Return New String() {}
    End Function

    '===========================================================
    ' 空フォルダ判定
    '===========================================================
    ''' <summary>
    ''' フォルダが空かどうかを判定します。
    ''' </summary>
    Public Shared Function IsEmpty(path As String) As Boolean
        Try
            path = PathHelper.Normalize(path)
            If Not Directory.Exists(path) Then Return True

            If Directory.GetFiles(path).Length > 0 Then Return False
            If Directory.GetDirectories(path).Length > 0 Then Return False

            Return True
        Catch
            Return True
        End Try
    End Function

    '===========================================================
    ' フォルダサイズ取得（再帰）
    '===========================================================
    ''' <summary>
    ''' フォルダの総サイズ（バイト）を返します。
    ''' </summary>
    Public Shared Function GetDirectorySize(path As String) As Long
        Dim total As Long = 0

        Try
            path = PathHelper.Normalize(path)
            If Not Directory.Exists(path) Then Return 0

            ' ファイルサイズ合計
            For Each file As String In Directory.GetFiles(path)
                Try
                    total += New FileInfo(file).Length
                Catch
                End Try
            Next

            ' サブフォルダ再帰
            For Each dir As String In Directory.GetDirectories(path)
                total += GetDirectorySize(dir)
            Next

        Catch
        End Try

        Return total
    End Function

    '===========================================================
    ' フォルダ内のファイル数取得（再帰）
    '===========================================================
    ''' <summary>
    ''' フォルダ内のファイル数を返します（サブフォルダ含む）。
    ''' </summary>
    Public Shared Function CountFiles(path As String) As Integer
        Dim count As Integer = 0

        Try
            path = PathHelper.Normalize(path)
            If Not Directory.Exists(path) Then Return 0

            count += Directory.GetFiles(path).Length

            For Each dir As String In Directory.GetDirectories(path)
                count += CountFiles(dir)
            Next

        Catch
        End Try

        Return count
    End Function

End Class