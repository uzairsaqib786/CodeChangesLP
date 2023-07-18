' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Public Class EventLog

    ''' <summary>
    ''' Selects all data displayed in the Event Log Manager screen that fit the criteria.
    ''' </summary>
    ''' <param name="draw">DataTables plugin parameter, must NOT be changed.</param>
    ''' <param name="sRow">DataTables plugin parameter which determines the starting row to select.  Controlled elsewhere.</param>
    ''' <param name="eRow">DataTables plugin parameter which determines the ending row to select.  Controlled elsewhere.</param>
    ''' <param name="messageFilter">[Message] column filter from [Event Log] table.</param>
    ''' <param name="eventLocation">[Event Location] column filter from [Event Log] table.</param>
    ''' <param name="nameStamp">[Name Stamp] column filter from [Event Log] table.</param>
    ''' <param name="sDate">Start date filter for [Date Stamp] column in [Event Log] table.</param>
    ''' <param name="eDate">End date filter for [Date Stamp] column in [Event Log] table.</param>
    ''' <param name="sortColumnNum">DataTables plugin parameter determining sorted column index as Integer.</param>
    ''' <param name="sortOrder">DataTables plugin parameter determining the sort order.  (ASC or DESC for ascending or descending order).</param>
    ''' <returns>Returns the table data in the form of a TableObject for the DataTables plugin to properly parse and display.</returns>
    ''' <remarks></remarks>
    Public Shared Function selEventLog(draw As Integer, sRow As Integer, eRow As Integer, messageFilter As String, eventLocation As String, eventCode As String, eventType As String, nameStamp As String, _
                                sDate As DateTime, eDate As DateTime, sortColumnNum As Integer, sortOrder As String, filter As String, user As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim order As String() = {"Date Stamp", "Message", "Event Code", "Name Stamp", "Event Type", "Event Location", "Notes", "Transaction ID", "Event ID"}
        Dim DataReader As SqlDataReader = Nothing, sortColumn As String = order.GetValue(sortColumnNum)
        If IsNothing(messageFilter) Then messageFilter = ""
        If IsNothing(eventLocation) Then eventLocation = ""
        If IsNothing(nameStamp) Then nameStamp = ""
        If IsNothing(sortOrder) Then sortOrder = "DESC"

        messageFilter = GlobalFunctions.cleanSearch(messageFilter)
        eventLocation = GlobalFunctions.cleanSearch(eventLocation)
        nameStamp = GlobalFunctions.cleanSearch(nameStamp)
        If sDate = nullDate Or eDate = nullDate Or sDate.CompareTo(minDate) < 0 Or eDate.CompareTo(minDate) < 0 Or sDate.CompareTo(maxDate) > 0 Or eDate.CompareTo(maxDate) > 0 Then
            Return table
        End If
        Try
            Dim columnorder = New List(Of String) From {"Date Stamp", "Message", "Event Code", "Name Stamp", "Event Type", "Event Location", "Notes", "Transaction ID", "Event ID"}
            filter = filter.Replace("[", "[Event Log].[").Replace("Date", "Date Stamp").Replace("Username", "Name Stamp").Replace("Trans. ID", "Transaction ID")
            table = GetJQueryDataTableResult(draw, "selEventLogDT", WSID, user, {{"@sRow", sRow, intVar}, _
                                                            {"@eRow", eRow, intVar}, _
                                                            {"@messageFilter", messageFilter, strVar}, _
                                                            {"@eventLocation", eventLocation, strVar}, _
                                                            {"@nameStamp", nameStamp, strVar}, _
                                                            {"@fromDate", sDate, dteVar}, _
                                                            {"@toDate", eDate, dteVar}, _
                                                            {"@eventType", eventType, strVar}, _
                                                            {"@eventCode", eventCode, strVar}, _
                                                            {"@sortColumn", sortColumn, strVar}, _
                                                            {"@sortOrder", sortOrder, strVar}, _
                                                            {"@filter", filter, strVar}}, columnOrder:=columnorder)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Event Log Manager", "selEventLog", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try

        Return table
    End Function

    ''' <summary>
    ''' Get the type ahead for the message column
    ''' </summary>
    ''' <param name="message">The currenlty typed in message</param>
    ''' <param name="sDate">The start date</param>
    ''' <param name="eDate">The end date</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <returns>A list of all the messages that start with the input</returns>
    ''' <remarks></remarks>
    Public Shared Function getMessageDrop(message As String, sDate As DateTime, eDate As DateTime, user As String, WSID As String) As List(Of String)
        Dim messInfo As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selEventLogMessageTA", WSID, {{"@Mess", message + "%", strVar}, {"@sDate", sDate, dteVar}, {"@eDate", eDate, dteVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        messInfo.Add(CheckDBNull(DataReader(x)))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Event Log Manager", "getMessageDrop", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return messInfo
    End Function

    ''' <summary>
    ''' Get the location column typeahead
    ''' </summary>
    ''' <param name="eventLoc">The location that is currently typed in</param>
    ''' <param name="sDate">The start date</param>
    ''' <param name="eDate">The end date</param>
    ''' <param name="user">The suer that is currently logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list containg all the locations that begin with the input</returns>
    ''' <remarks></remarks>
    Public Shared Function getEventLocDrop(eventLoc As String, sDate As Date, eDate As Date, user As String, WSID As String) As List(Of String)
        Dim eventLocInfo As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selEventLogLocationTA", WSID, {{"@location", eventLoc + "%", strVar}, {"@sDate", sDate, dteVar}, {"@eDate", eDate, dteVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        eventLocInfo.Add(CheckDBNull(DataReader(x)))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Event Log Manager", "getEventLocDrop", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return eventLocInfo
    End Function

    ''' <summary>
    ''' Gets the typeahead for the name stamp column
    ''' </summary>
    ''' <param name="name">The name that is currently typed in</param>
    ''' <param name="sDate">The start date</param>
    ''' <param name="eDate">The end date</param>
    ''' <param name="user">The suer that is currently logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list containing all the namestamps starting with the input</returns>
    ''' <remarks></remarks>
    Public Shared Function getNameStampDrop(name As String, sDate As Date, eDate As Date, user As String, WSID As String) As List(Of String)
        Dim nameInfo As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selEventLogNameStampTA", WSID, {{"@Name", name + "%", strVar}, {"@sDate", sDate, dteVar}, {"@eDate", eDate, dteVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        nameInfo.Add(CheckDBNull(DataReader(x)))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Event Log Manager", "getNameStampDrop", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return nameInfo
    End Function

    ''' <summary>
    ''' Gets the typeahead for the Event Type column
    ''' </summary>
    ''' <param name="name">The name that is currently typed in</param>
    ''' <param name="sDate">The start date</param>
    ''' <param name="eDate">The end date</param>
    ''' <param name="user">The suer that is currently logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list containing all the namestamps starting with the input</returns>
    ''' <remarks></remarks>
    Public Shared Function getEventTypeDrop(name As String, sDate As Date, eDate As Date, user As String, WSID As String) As List(Of String)
        Dim nameInfo As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selEventLogEventTypeTA", WSID, {{"@Evnt", name + "%", strVar}, {"@sDate", sDate, dteVar}, {"@eDate", eDate, dteVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        nameInfo.Add(CheckDBNull(DataReader(x)))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Event Log Manager", "getEventTypeDrop", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return nameInfo
    End Function

    ''' <summary>
    ''' Gets the typeahead for the Event Code column
    ''' </summary>
    ''' <param name="name">The name that is currently typed in</param>
    ''' <param name="sDate">The start date</param>
    ''' <param name="eDate">The end date</param>
    ''' <param name="user">The suer that is currently logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list containing all the namestamps starting with the input</returns>
    ''' <remarks></remarks>
    Public Shared Function getEventCodeDrop(name As String, sDate As Date, eDate As Date, user As String, WSID As String) As List(Of String)
        Dim nameInfo As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selEventLogEventCodeTA", WSID, {{"@Evnt", name + "%", strVar}, {"@sDate", sDate, dteVar}, {"@eDate", eDate, dteVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        nameInfo.Add(CheckDBNull(DataReader(x)))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Event Log Manager", "getEventCodeDrop", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return nameInfo
    End Function
End Class