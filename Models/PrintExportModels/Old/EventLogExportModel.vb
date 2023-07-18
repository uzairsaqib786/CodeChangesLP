''' <summary>
''' Model for exporting database entries from Event Log to an Excel speadsheet.
''' </summary>
''' <remarks></remarks>
Public Class EventLogExportModel
    Public Property DateStamp As List(Of String)
    Public Property EventLocation As List(Of String)
    Public Property EventID As List(Of Integer)
    Public Property EventType As List(Of String)
    Public Property EventCode As List(Of String)
    Public Property NameStamp As List(Of String)
    Public Property TransID As List(Of Integer)
    Public Property Message As List(Of String)
    Public Property Notes As List(Of List(Of String))
    Public Property ExportSize As Integer = 0

    ''' <summary>
    ''' Cleans all values from potentially dangerous values (similar to SQL injection, mostly Excel keywords and keyphrases.)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanInputs()
        For x As Integer = 0 To Me.ExportSize - 1
            DateStamp(x) = Clean(DateStamp(x))
            EventLocation(x) = Clean(EventLocation(x))
            EventType(x) = Clean(EventType(x))
            EventCode(x) = Clean(EventCode(x))
            NameStamp(x) = Clean(NameStamp(x))
            Message(x) = Clean(Message(x))
            For y As Integer = 0 To Me.Notes(x).Count - 1
                Notes(x)(y) = Clean(Notes(x)(y))
            Next
        Next
    End Sub

    ''' <summary>
    ''' Escapes special characters in str.  Trims whitespace off both ends of the string.
    ''' </summary>
    ''' <param name="str">Uncleaned string.</param>
    ''' <returns>Cleaned version of str.</returns>
    ''' <remarks>%, _, [, ], !, ', " ", vbCrLf, vbCr, vbLf are escaped</remarks>
    Private Function Clean(str As String) As String
        ' Cleans input from the search function field so that special characters are escaped.
        Dim returnString As String = ""
        For c As Integer = 0 To str.Length - 1
            Select Case str(c)
                ' if input is potentially dangerous add escape character (!)
                Case "%", "_", "[", "]", "!", "'"
                Case " ", vbCrLf, vbCr, vbLf
                    returnString = returnString.TrimEnd()
                Case Else
                    returnString += str(c)
            End Select
        Next

        Return returnString.Trim()
    End Function

End Class
