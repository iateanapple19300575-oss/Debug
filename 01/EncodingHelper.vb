Imports System.Text

''' <summary>
''' 文字コード判定およびCSV読み込みを行うユーティリティクラス。
''' </summary>
Public NotInheritable Class EncodingHelper

    Private Sub New()
    End Sub

    ''' <summary>
    ''' バイト配列の文字コードを Encoding オブジェクトとして返します。
    ''' </summary>
    ''' <param name="bytes">判定対象のバイト配列</param>
    ''' <returns>Encoding.UTF8 または Shift_JIS Encoding</returns>
    Public Shared Function GetEncoding(bytes As Byte()) As Encoding
        Dim name As String = DetectEncoding(bytes)

        If name = "UTF-8" Then
            Return New UTF8Encoding(False)
        Else
            Return Encoding.GetEncoding("Shift_JIS")
        End If
    End Function

    ''' <summary>
    ''' UTF-8 と Shift_JIS のどちらである可能性が高いかを判定します。
    ''' </summary>
    ''' <param name="bytes">判定対象のバイト配列</param>
    ''' <returns>"UTF-8" または "Shift_JIS"</returns>
    Public Shared Function DetectEncoding(bytes As Byte()) As String
        ' データなし（サイズ０は、Shift_JISとする）
        If bytes Is Nothing OrElse bytes.Length = 0 Then
            Return "Shift_JIS"
        End If

        ' UTF-8 BOM チェック（BOM付きはUTF-8）
        If bytes.Length >= 3 AndAlso
           bytes(0) = &HEF AndAlso bytes(1) = &HBB AndAlso bytes(2) = &HBF Then
            Return "UTF-8"
        End If

        ' UTF-8 妥当性チェック
        If IsValidUtf8(bytes) Then
            Return "UTF-8"
        End If

        ' 以外は Shift_JIS とみなす
        Return "Shift_JIS"
    End Function

    ''' <summary>
    ''' バイト配列が「正しいUTF-8」としてデコード可能かを判定します。
    ''' </summary>
    ''' <param name="bytes">判定対象のバイト配列</param>
    ''' <returns>UTF-8として妥当ならTrue、不正ならFalse</returns>
    Public Shared Function IsValidUtf8(bytes As Byte()) As Boolean
        If bytes Is Nothing OrElse bytes.Length = 0 Then
            Return False
        End If

        Try
            ' 第二引数: エラー時に例外を投げる設定
            Dim enc As New UTF8Encoding(False, True)
            enc.GetString(bytes)
            Return True

        Catch ex As DecoderFallbackException
            Return False
        End Try
    End Function
End Class