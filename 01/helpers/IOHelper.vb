Imports System.Text
Imports System.IO
Imports System.Data

''' <summary>
''' ファイル・フォルダ・パス・CSV・文字コード操作の窓口となる統合ヘルパー。
''' 内部で EncodingHelper / PathHelper / DirectoryHelper / CsvHelper を利用します。
''' </summary>
Public NotInheritable Class IOHelper

    Private Sub New()
    End Sub

    '===========================================================
    ' 文字コード関連（EncodingHelper へのファサード）
    '===========================================================
    ''' <summary>
    ''' 自動文字コード判定でテキストファイルを読み込みます。
    ''' </summary>
    Public Shared Function ReadAllTextAuto(path As String) As String
        Return EncodingHelper.ReadAllTextAuto(path)
    End Function

    ''' <summary>
    ''' UTF-8（BOMなし）でテキストを書き込みます。
    ''' </summary>
    Public Shared Sub WriteUtf8(path As String, text As String)
        EncodingHelper.WriteUtf8(path, text)
    End Sub

    ''' <summary>
    ''' Shift_JIS でテキストを書き込みます。
    ''' </summary>
    Public Shared Sub WriteSjis(path As String, text As String)
        EncodingHelper.WriteSjis(path, text)
    End Sub

    '===========================================================
    ' パス関連（PathHelper へのファサード）
    '===========================================================
    Public Shared Function NormalizePath(path As String) As String
        Return PathHelper.Normalize(path)
    End Function

    Public Shared Function CombinePath(basePath As String, relative As String) As String
        Return PathHelper.Combine(basePath, relative)
    End Function

    Public Shared Function GetFileName(path As String) As String
        Return PathHelper.GetFileName(path)
    End Function

    Public Shared Function GetExtension(path As String) As String
        Return PathHelper.GetExtension(path)
    End Function

    Public Shared Function GetDirectory(path As String) As String
        Return PathHelper.GetDirectory(path)
    End Function

    Public Shared Function SanitizeFileName(name As String) As String
        Return PathHelper.SanitizeFileName(name)
    End Function

    Public Shared Function GetTempFolder() As String
        Return PathHelper.GetTempFolder()
    End Function

    Public Shared Function CreateDateFolder(basePath As String) As String
        Return PathHelper.CreateDateFolder(basePath)
    End Function

    '===========================================================
    ' ディレクトリ関連（DirectoryHelper へのファサード）
    '===========================================================
    Public Shared Function CreateDirectorySafe(path As String) As Boolean
        Return DirectoryHelper.CreateDirectorySafe(path)
    End Function

    Public Shared Function DeleteDirectorySafe(path As String) As Boolean
        Return DirectoryHelper.DeleteDirectorySafe(path)
    End Function

    Public Shared Function CopyDirectory(src As String, dest As String) As Boolean
        Return DirectoryHelper.CopyDirectory(src, dest)
    End Function

    Public Shared Function GetFiles(path As String) As String()
        Return DirectoryHelper.GetFiles(path)
    End Function

    Public Shared Function GetDirectories(path As String) As String()
        Return DirectoryHelper.GetDirectories(path)
    End Function

    Public Shared Function IsDirectoryEmpty(path As String) As Boolean
        Return DirectoryHelper.IsEmpty(path)
    End Function

    Public Shared Function GetDirectorySize(path As String) As Long
        Return DirectoryHelper.GetDirectorySize(path)
    End Function

    Public Shared Function CountFiles(path As String) As Integer
        Return DirectoryHelper.CountFiles(path)
    End Function

    '===========================================================
    ' ファイル存在・コピー・削除（簡易窓口）
    '===========================================================
    Public Shared Function FileExists(path As String) As Boolean
        Return File.Exists(PathHelper.Normalize(path))
    End Function

    Public Shared Sub CopyFile(src As String, dest As String, overwrite As Boolean)
        File.Copy(PathHelper.Normalize(src), PathHelper.Normalize(dest), overwrite)
    End Sub

    Public Shared Sub DeleteFile(path As String)
        Dim p As String = PathHelper.Normalize(path)
        If File.Exists(p) Then
            File.Delete(p)
        End If
    End Sub

    '===========================================================
    ' CSV 関連（CsvHelper へのファサード）
    '===========================================================
    ''' <summary>
    ''' CSV を DataTable に読み込みます（自動文字コード・区切り判定）。
    ''' </summary>
    Public Shared Function LoadCsvToDataTable(path As String) As DataTable
        Return CsvHelper.LoadCsvToDataTable(path)
    End Function

    ''' <summary>
    ''' 巨大CSVを1行ずつ解析し、各行のフィールド配列をコールバックに渡します。
    ''' </summary>
    Public Shared Sub ParseCsvStream(path As String, action As Action(Of String()))
        CsvHelper.ParseCsvStream(path, action)
    End Sub

End Class