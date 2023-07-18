' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace WorkManager
    Public Class OrganizeWork
        ''' <summary>
        ''' Gets the Open Count and Refresh time setting for the page on load
        ''' </summary>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>object containg the refresh timer and open count</returns>
        ''' <remarks></remarks>
        Public Shared Function getOrgainzeWorkData(User As String, WSID As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim data As New Object

            Try
                DataReader = RunSPArray("selWMSettingsRefresOTCount", WSID, {{"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    data = New With {.OpenCount = DataReader("Open Count"), .RefreshTime = DataReader("Refresh Time")}
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("Organize Work", "getOrgainzeWorkData", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return data
        End Function

        ''' <summary>
        ''' Gets the data for the Organize Work datatable
        ''' </summary>
        ''' <param name="draw">Paramater needed for the datatable</param>
        ''' <param name="startRow">The row number to start getting records from</param>
        ''' <param name="endRow">The row number to stop gettign records from</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="filter">The filter that is being applied o the datatable</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A tableobjetc containg all the desired data</returns>
        ''' <remarks></remarks>
        Public Shared Function getOrgainzeWorkTable(draw As Integer, startRow As Integer, endRow As Integer, sortCol As Integer,
                                                    sortOrder As String, filter As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"ID", "Range Name", "Start Location", "End Location", "Open Picks", "Open Put Aways",
                                                            "Open Counts", "Need Workers Picks", "Need Workers Put Aways", "Need Workers Counts",
                                                            "Multi Worker Range", "Active"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim Reader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0
            Dim FilterCount As Integer = 0
            Dim StartLoc As String = ""
            Dim EndLoc As String = ""
            Dim OpenPick As Integer = 0
            Dim OpenPut As Integer = 0
            Dim OpenCount As Integer = 0
            Dim WorkerPick As Integer = 0
            Dim WorkerPut As Integer = 0
            Dim WorkerCount As Integer = 0

            Try
                DataReader = RunSPArray("selOrganizeWorkDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar},
                                                                    {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar},
                                                                    {"@filter", filter, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        FilterCount = DataReader("FilterCount")
                        For Each col In columnSequence
                            Select Case col
                                Case "Open Picks"
                                    inners.Add(OpenPick)
                                Case "Open Put Aways"
                                    inners.Add(OpenPut)
                                Case "Open Counts"
                                    inners.Add(OpenCount)
                                Case "Need Workers Picks"
                                    inners.Add(WorkerPick)
                                Case "Need Workers Put Aways"
                                    inners.Add(WorkerPut)
                                Case "Need Workers Counts"
                                    inners.Add(WorkerCount)
                                Case "Start Location"
                                    StartLoc = DataReader(col)
                                    inners.Add(CheckDBNull(DataReader(col)))
                                Case "End Location"
                                    EndLoc = DataReader(col)
                                    inners.Add(CheckDBNull(DataReader(col)))
                                    Reader = RunSPArray("selOrganizeWorkCountDT", WSID, {{"@StartLoc", StartLoc, strVar}, {"@EndLoc", EndLoc, strVar}})
                                    If Reader.HasRows Then
                                        Reader.Read()
                                        OpenPick = Reader(columnSequence(4))
                                        OpenPut = Reader(columnSequence(5))
                                        OpenCount = Reader(columnSequence(6))
                                        WorkerPick = Reader(columnSequence(7))
                                        WorkerPut = Reader(columnSequence(8))
                                        WorkerCount = Reader(columnSequence(9))
                                    End If
                                Case Else
                                    inners.Add(CheckDBNull(DataReader(col)))
                            End Select
                        Next
                        data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("OrganizeWork", "getOrgainzeWorkTable", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
                If Not IsNothing(Reader) Then
                    Reader.Dispose()
                End If
            End Try

            Return New TableObject(draw, RecordCount, FilterCount, data)
        End Function

        ''' <summary>
        ''' Clears work assigned to the given username for all transaction types selected
        ''' </summary>
        ''' <param name="pick">Tells if pick transactions should be cleared</param>
        ''' <param name="put">Tells if putaway transactions should be cleared</param>
        ''' <param name="count">Tells if count trasnactions should be cleared</param>
        ''' <param name="username">The username to clear work for</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function ClearCurrentWork(pick As Integer, put As Integer, count As Integer, username As String, user As String, WSID As String) As Boolean
            Try
                RunActionSP("wmClearWorkBatch", WSID, {{"@Username", username, strVar}, {"@PickClear", pick, intVar}, {"@PutClear", put, intVar}, {"@CountClear", count, intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("OrganizeWork", "ClearCurrentWork", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Clears batch pick ids assigned to the given username for all transaction types selected
        ''' </summary>
        ''' <param name="pick">Tells if pick batches should be cleared</param>
        ''' <param name="put">Tells if put away batches should be cleared</param>
        ''' <param name="count">Tells if count batches should be cleared</param>
        ''' <param name="username">The username to clear batches for</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function ClearBatchIDs(pick As Integer, put As Integer, count As Integer, username As String, user As String, WSID As String) As Boolean
            Try
                RunActionSP("wmClearWorkers", WSID, {{"@Username", username, strVar}, {"@PickClear", pick, intVar}, {"@PutClear", put, intVar}, {"@CountClear", count, intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("OrganizeWork", "ClearBatchIDs", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Creates batches for the automatically generated batch id that is either just the batch id or includes the username. 
        ''' This is done for picks, put aways, and counts. If  acknowledged this will also clear bacth ids for the transactions types that are designated
        ''' </summary>
        ''' <param name="withUsername">If the username should be included in the batch id</param>
        ''' <param name="username">The username who is getting the batches</param>
        ''' <param name="pick">If pick batches should be cleared</param>
        ''' <param name="put">If put away batches should be cleared</param>
        ''' <param name="count">If count batches should be cleared</param>
        ''' <param name="clearBatches">If he batcehs should be cleared</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>boolean telling if the operation completed succesfully</returns>
        ''' <remarks></remarks>
        Public Shared Function CreateBatch(withUsername As Boolean, username As String, pick As Integer, put As Integer, count As Integer, clearBatches As Boolean, user As String, WSID As String) As Boolean
            Try
                RunActionSP("wmAssignWorkers", WSID, {{"@Username", username, strVar}, {"@PickClear", pick, intVar}, {"@PutClear", put, intVar}, {"@CountClear", count, intVar},
                                                      {"@BatchType", IIf(withUsername, "BU", "B"), strVar}})
                If clearBatches Then
                    Return ClearBatchIDs(pick, put, count, username, user, WSID)
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("OrganizeWork", "CreateBatchWithUsername", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets the data for the worker typeahead
        ''' </summary>
        ''' <param name="worker">The value to get the suggestions for</param>
        ''' <param name="user">The user that si currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of object containing the information for every worker dipslayed in the typeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function GetWorkerTA(worker As String, user As String, WSID As String, all As Integer) As List(Of Object)
            Dim reader As SqlDataReader = Nothing
            Dim workers As New List(Of Object)
            Try
                reader = RunSPArray("selWMWorkerTA", WSID, {{"@Name", worker, strVar}, {"@All", all, intVar}})
                While reader.HasRows
                    While reader.Read
                        If Not IsDBNull(reader(1)) Then
                            workers.Add(New With {.name = CheckDBNull(reader(0)), .username = reader(1)})
                        End If
                    End While
                    reader.NextResult()
                End While
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("OrganizeWork", "GetWorkerTA", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return workers
        End Function
    End Class
End Namespace

