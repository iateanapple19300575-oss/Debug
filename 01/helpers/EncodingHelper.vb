Imports System.Text
Imports System.IO

''' <summary>
''' 文字コード判定とテキスト読み書きを行う FW3.5 専用ヘルパー。
''' </summary>
Public NotInheritable Class EncodingHelper

    Private Sub New()
    End Sub

    '===============================
    ' UTF-8 妥当性チェック
    '===============================
    Public Shared Function IsValidUtf8(bytes As Byte()) As Boolean
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return False

        Try
            Dim enc As New UTF8Encoding(False, True)
            enc.GetString(bytes)
            Return True
        Catch ex As DecoderFallbackException
            Return False
        End Try
    End Function

    '===============================
    ' UTF-8 BOM チェック
    '===============================
    Public Shared Function HasUtf8Bom(bytes As Byte()) As Boolean
        If bytes Is Nothing OrElse bytes.Length < 3 Then Return False
        Return (bytes(0) = &HEF AndAlso bytes(1) = &HBB AndAlso bytes(2) = &HBF)
    End Function

    '===============================
    ' 文字コード自動判定
    '===============================
    Public Shared Function DetectEncoding(bytes As Byte()) As Encoding
        If bytes Is Nothing OrElse bytes.Length = 0 Then
            Return Encoding.GetEncoding("Shift_JIS")
        End If

        If HasUtf8Bom(bytes) Then
            Return New UTF8Encoding(False)
        End If

        If IsValidUtf8(bytes) Then
            Return New UTF8Encoding(False)
        End If

        Return Encoding.GetEncoding("Shift_JIS")
    End Function

    '===============================
    ' BOM 除去
    '===============================
    Public Shared Function RemoveUtf8Bom(bytes As Byte()) As Byte()
        If Not HasUtf8Bom(bytes) Then Return bytes

        Dim newBytes(bytes.Length - 4) As Byte
        Array.Copy(bytes, 3, newBytes, 0, bytes.Length - 3)
        Return newBytes
    End Function

    '===============================
    ' 改行コード統一
    '===============================
    Public Shared Function NormalizeNewLine(text As String) As String
        If text Is Nothing Then Return ""
        text = text.Replace(vbCrLf, vbLf)
        text = text.Replace(vbCr, vbLf)
        Return text.Replace(vbLf, vbCrLf)
    End Function

    '===============================
    ' 自動文字コード読み込み
    '===============================
    Public Shared Function ReadAllTextAuto(path As String) As String
        Dim bytes As Byte() = File.ReadAllBytes(path)
        Dim enc As Encoding = DetectEncoding(bytes)

        If HasUtf8Bom(bytes) Then
            bytes = RemoveUtf8Bom(bytes)
        End If

        Dim text As String = enc.GetString(bytes)
        Return NormalizeNewLine(text)
    End Function

    '===============================
    ' 書き込み
    '===============================
    Public Shared Sub WriteUtf8(path As String, text As String)
        File.WriteAllText(path, text, New UTF8Encoding(False))
    End Sub

    Public Shared Sub WriteSjis(path As String, text As String)
        File.WriteAllText(path, text, Encoding.GetEncoding("Shift_JIS"))
    End Sub

End Class