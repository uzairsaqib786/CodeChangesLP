' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Model for exporting database entries from Event Log to an Excel speadsheet.
''' </summary>
''' <remarks></remarks>
Public Class EventLogExportModel

    'The list of datestamps
    Public Property DateStamp As List(Of String)

    'The list of event locations
    Public Property EventLocation As List(Of String)

    'The list of event ids
    Public Property EventID As List(Of Integer)

    'List of event types
    Public Property EventType As List(Of String)

    'List of event codes
    Public Property EventCode As List(Of String)

    'List of name stamps
    Public Property NameStamp As List(Of String)

    'List of transaction ids
    Public Property TransID As List(Of Integer)

    'List of messages
    Public Property Message As List(Of String)

    'List that contains the notes
    Public Property Notes As List(Of List(Of String))

    'How many records are being exported
    Public Property ExportSize As Integer = 0

    ''' <summary>
    ''' Constructor for the object
    ''' </summary>
    ''' <param name="DateStamp"></param>
    ''' <param name="EventLocation"></param>
    ''' <param name="EventId"></param>
    ''' <param name="EventType"></param>
    ''' <param name="EventCode"></param>
    ''' <param name="NameStamp"></param>
    ''' <param name="TransID"></param>
    ''' <param name="Message"></param>
    ''' <param name="Notes"></param>
    ''' <param name="ExportSize"></param>
    ''' <remarks></remarks>
    Public Sub New(DateStamp As List(Of String), EventLocation As List(Of String), EventId As List(Of Integer), EventType As List(Of String), _
                   EventCode As List(Of String), NameStamp As List(Of String), TransID As List(Of Integer), Message As List(Of String), Notes As List(Of List(Of String)), _
                   ExportSize As Integer)
        Me.DateStamp = DateStamp
        Me.EventLocation = EventLocation
        Me.EventID = EventId
        Me.EventType = EventType
        Me.EventCode = EventCode
        Me.NameStamp = NameStamp
        Me.TransID = TransID
        Me.Message = Message
        Me.Notes = Notes
        Me.ExportSize = ExportSize
        CleanInputs()
    End Sub

    ''' <summary>
    ''' Cleans all values from potentially dangerous values (similar to SQL injection, mostly Excel keywords and keyphrases.)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanInputs()
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
