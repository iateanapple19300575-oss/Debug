Imports System.Data
Imports System.IO
Imports System.Text

''' <summary>
''' CSV の読み取り・解析・DataTable マッピングを行う FW3.5 専用ヘルパー。
''' </summary>
Public NotInheritable Class CsvHelper

    Private Sub New()
    End Sub

    '===========================================================
    ' 区切り文字自動判定
    '===========================================================
    ''' <summary>
    ''' CSV の先頭数行から区切り文字を推定します。
    ''' 対象: カンマ, タブ, セミコロン, パイプ
    ''' </summary>
    Public Shared Function DetectSeparator(lines As IList(Of String)) As Char
        If lines Is Nothing OrElse lines.Count = 0 Then
            Return ","c
        End If

        Dim candidates As Char() = New Char() {","c, vbTab, ";"c, "|"c}
        Dim bestSep As Char = ","c
        Dim bestScore As Integer = -1

        For Each sep As Char In candidates
            Dim score As Integer = 0
            Dim prevCount As Integer = -1
            Dim stable As Boolean = True

            Dim i As Integer
            For i = 0 To Math.Min(lines.Count, 10) - 1
                Dim line As String = lines(i)
                Dim cnt As Integer = CountChar(line, sep)

                If cnt = 0 Then
                    stable = False
                    Exit For
                End If

                If prevCount <> -1 AndAlso prevCount <> cnt Then
                    stable = False
                    Exit For
                End If

                prevCount = cnt
                score += cnt
            Next

            If stable AndAlso score > bestScore Then
                bestScore = score
                bestSep = sep
            End If
        Next

        Return bestSep
    End Function

    Private Shared Function CountChar(text As String, ch As Char) As Integer
        If String.IsNullOrEmpty(text) Then Return 0
        Dim c As Integer = 0
        Dim i As Integer
        For i = 0 To text.Length - 1
            If text(i) = ch Then c += 1
        Next
        Return c
    End Function

    '===========================================================
    ' 1 行パース（簡易CSV: ダブルクォート非対応版）
    '===========================================================
    ''' <summary>
    ''' 区切り文字で行を分割します（ダブルクォート非対応の高速版）。
    ''' </summary>
    Public Shared Function SplitLine(line As String, sep As Char) As String()
        If line Is Nothing Then
            Return New String() {}
        End If

        Dim list As New List(Of String)()
        Dim sb As New StringBuilder()
        Dim i As Integer

        For i = 0 To line.Length - 1
            Dim ch As Char = line(i)
            If ch = sep Then
                list.Add(sb.ToString())
                sb.Length = 0
            Else
                sb.Append(ch)
            End If
        Next

        list.Add(sb.ToString())
        Return list.ToArray()
    End Function

    '===========================================================
    ' CSV → DataTable（全文読み込み）
    '===========================================================
    ''' <summary>
    ''' CSVファイルを自動文字コード判定・区切り自動判定して DataTable に読み込みます。
    ''' 1行目をヘッダとして列名に使用します。
    ''' </summary>
    Public Shared Function LoadCsvToDataTable(path As String) As DataTable
        Dim dt As New DataTable()

        ' まず全テキストを読み込み（IOHelper / EncodingHelper を想定）
        Dim text As String = EncodingHelper.ReadAllTextAuto(path)

        Dim lines As String() = text.Split(New String() {vbCrLf}, StringSplitOptions.None)
        Dim lineList As New List(Of String)()
        Dim i As Integer
        For i = 0 To lines.Length - 1
            If lines(i).Length > 0 Then
                lineList.Add(lines(i))
            End If
        Next

        If lineList.Count = 0 Then
            Return dt
        End If

        ' 区切り文字自動判定
        Dim sep As Char = DetectSeparator(lineList)

        ' 1行目をヘッダとして列定義
        Dim header As String() = SplitLine(lineList(0), sep)
        Dim colCount As Integer = header.Length

        For i = 0 To colCount - 1
            Dim colName As String = header(i)
            If String.IsNullOrEmpty(colName) Then
                colName = "Column" & (i + 1).ToString()
            End If

            ' 重複列名対策
            Dim baseName As String = colName
            Dim suffix As Integer = 1
            While dt.Columns.Contains(colName)
                colName = baseName & "_" & suffix.ToString()
                suffix += 1
            End While

            dt.Columns.Add(colName, GetType(String))
        Next

        ' データ行
        For i = 1 To lineList.Count - 1
            Dim rowValues As String() = SplitLine(lineList(i), sep)
            Dim row As DataRow = dt.NewRow()

            Dim j As Integer
            For j = 0 To colCount - 1
                If j < rowValues.Length Then
                    row(j) = rowValues(j)
                Else
                    row(j) = DBNull.Value
                End If
            Next

            dt.Rows.Add(row)
        Next

        Return dt
    End Function

    '===========================================================
    ' 巨大CSV高速パーサー（コールバック方式）
    '===========================================================
    ''' <summary>
    ''' 巨大CSVを1行ずつ解析し、各行のフィールド配列をコールバックに渡します。
    ''' 区切り文字は先頭数行から自動判定されます。
    ''' </summary>
    Public Shared Sub ParseCsvStream(path As String, action As Action(Of String()))
        ' 先頭数行だけ読み込んで区切り判定
        Dim headLines As New List(Of String)()
        Using sr As New StreamReader(path, EncodingHelper.DetectEncoding(File.ReadAllBytes(path)))
            Dim i As Integer = 0
            While Not sr.EndOfStream AndAlso i < 10
                Dim line As String = sr.ReadLine()
                If line IsNot Nothing AndAlso line.Length > 0 Then
                    headLines.Add(line)
                    i += 1
                End If
            End While
        End Using

        If headLines.Count = 0 Then
            Exit Sub
        End If

        Dim sep As Char = DetectSeparator(headLines)

        ' 再度ストリームを開き直して全行処理
        Using sr As New StreamReader(path, EncodingHelper.DetectEncoding(File.ReadAllBytes(path)))
            While Not sr.EndOfStream
                Dim line As String = sr.ReadLine()
                If line Is Nothing Then
                    Continue While
                End If

                Dim fields As String() = SplitLine(line, sep)
                action(fields)
            End While
        End Using
    End Sub

End Class