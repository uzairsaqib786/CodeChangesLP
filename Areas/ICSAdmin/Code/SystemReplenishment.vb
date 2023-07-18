' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Admin
    Public Class SystemReplenishment
        ''' <summary>
        ''' Gets the data for the system replenishment datatable
        ''' </summary>
        ''' <param name="draw">Tells if the table is currently being drawn</param>
        ''' <param name="sRow">The first row being displayed</param>
        ''' <param name="eRow">The last row being displayed</param>
        ''' <param name="searchString">The string value that is being searched on</param>
        ''' <param name="searchCol">The column where the string will searched on</param>
        ''' <param name="sortColumn">The column that is being sorted</param>
        ''' <param name="sortOrder">The order that the column is bieng sorted</param>
        ''' <param name="Status">Tells which transactions will be shown (all, open, or completed)</param>
        ''' <param name="filter">The filter that is applied to the table</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that being worked on</param>
        ''' <returns>A tableobject containing the data needed for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function selSystRepTable(draw As Integer, sRow As Integer, eRow As Integer, searchString As String, searchCol As String, sortColumn As String, sortOrder As String, _
                                               Status As String, filter As String, user As String, WSID As String) As TableObject
            Dim Data As New List(Of List(Of String)), rowData As New List(Of String), recordsTotal As Integer = -1, RecordsFiltered As Integer = -1
            Dim DataReader As SqlDataReader = Nothing, index As Integer = 0

            Select Case sortColumn
                Case "Item Num"
                    sortColumn = "Item Number"
                Case "Trans Type"
                    sortColumn = "Transaction Type"
                Case "Carsl"
                    sortColumn = "Carousel"
                Case "Trans Qty"
                    sortColumn = "Transaction Quantity"
                Case "UofM"
                    sortColumn = "Unit of Measure"
            End Select

            Select Case searchCol
                Case "Trans Type"
                    searchCol = "Transaction Type"
                Case "Carsl"
                    searchCol = "Carousel"
                Case "Trans Qty"
                    searchCol = "Transaction Quantity"
                Case "UofM"
                    searchCol = "Unit of Measure"
                Case "Comp Date"
                    searchCol = "Completed Date"
            End Select

            searchString = GlobalFunctions.cleanSearch(searchString)
            'order is data, then filtered, then total 
            Try
                'edit filter here for current
                filter = filter.Replace("[", "[ReplenishmentReport].[").Replace("Trans Type", "Transaction Type").Replace("Carsl", "Carousel").Replace("Trans Qty", "Transaction Quantity").Replace("UofM", "Unit of Measure").Replace("Comp Date", "Completed Date")
                DataReader = RunSPArray("selReplenReportDT", WSID, {{"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, {"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}, _
                                                             {"@sortColumn", sortColumn, strVar}, {"@sortOrder", sortOrder, strVar}, {"@Status", Status, strVar}, {"@filter", filter, strVar}})
                If Not DataReader.HasRows Then
                    RecordsFiltered = 0
                    recordsTotal = 0
                End If
                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                rowData.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                            Next
                            Data.Add(rowData)
                            rowData = New List(Of String)
                        ElseIf index = 1 Then
                            RecordsFiltered = DataReader(0)
                        ElseIf index = 2 Then
                            recordsTotal = DataReader(0)
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("System Replenishment", "selSystRepTable", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return New TableObject(draw, recordsTotal, RecordsFiltered, Data, selReplenCountInfo(WSID, user))
        End Function

        ''' <summary>
        ''' Retrieves the datatables data for replenishments create new orders table
        ''' </summary>
        ''' <param name="draw">Datatable parameter</param>
        ''' <param name="sRow">Start row</param>
        ''' <param name="eRow">End row</param>
        ''' <param name="searchString">Searched string</param>
        ''' <param name="searchColumn">Column to search on</param>
        ''' <param name="sortColumn">Column to sort on as an index</param>
        ''' <param name="sortOrder">ASC or DESC</param>
        ''' <param name="reorder">t/f to determine whether WHERE clause should include [Available Qty] less than [Replenishment Qty]</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">Requesting workstation</param>
        ''' <returns>A tableobject that contains all the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getNewSystemReplenishmentOrdersTable(draw As Integer, sRow As Integer, eRow As Integer, searchString As String, searchColumn As String, sortColumn As Integer, _
                                                                    sortOrder As String, reorder As Boolean, filter As String, user As String, WSID As String) As TableObject
            Dim row As New List(Of String), data As New List(Of List(Of String)), recordsFiltered As Integer = 0, recordsTotal As Integer = 0, selectedReplenish As Integer = 0
            Dim datareader As SqlDataReader = Nothing
            Dim columns As String() = {"Item Number", "Description", "Warehouse", "Stock Qty", "Replenishment Point", "Replenishment Level", "Available Qty", "Replenishment Qty", "Case Qty",
                                       "Transaction Qty", "Replenish", "Replenish Exists", "RP_ID", "Alloc Pick", "Alloc Put"}
            Try
                'changes filter here for new
                filter = filter.Replace("[", "[Replenishment Queue].[").Replace("Item Num.", "Item Number").Replace("Repln, Point", "Replenishment Point").Replace("Repln. Level", "Replenishment Level").Replace("Avail. Qty.", "Available Qty").Replace("Repln. Qty", "Replenishment Qty").Replace("Trans. Qty", "Transaction Qty")
                datareader = RunSPArray("selReplenishmentsTable", WSID, _
                                        {{"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, {"@searchString", GlobalFunctions.cleanSearch(searchString), strVar}, _
                                         {"@searchColumn", searchColumn, strVar}, {"@sortColumn", columns.GetValue(sortColumn), strVar}, {"@sortOrder", sortOrder, strVar}, _
                                         {"@reorder", CastAsSqlBool(reorder), intVar}, {"@filter", filter, strVar}})

                Dim x As Integer = 0
                While datareader.HasRows
                    While datareader.Read()
                        If x = 2 Then
                            For column As Integer = 0 To datareader.FieldCount - 1
                                row.Add(IIf(IsDBNull(datareader(column)), "", datareader(column)))
                            Next
                            data.Add(row)
                            row = New List(Of String)
                        ElseIf x = 1 Then
                            recordsFiltered = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                        ElseIf x = 0 Then
                            recordsTotal = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                        ElseIf x = 3 Then
                            selectedReplenish = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                        End If
                    End While
                    datareader.NextResult()
                    x += 1
                End While

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishment", "getNewSystemReplenishmentOrdersTable", ex.Message, user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return New TableObject(draw, recordsTotal, recordsFiltered, data, selectedReplenish)
        End Function

        ''' <summary>
        ''' Gets the typeahead for the search box on the system replenishment new page
        ''' </summary>
        ''' <param name="query">String to search for</param>
        ''' <param name="column">Column to search in</param>
        ''' <param name="wsid">Requesting workstation</param>
        ''' <param name="user">Requesting user</param>
        ''' <returns>A list of string containing the values for the given column that begin with the query input</returns>
        ''' <remarks></remarks>
        Public Shared Function getSysReplnNewTypeahead(query As String, column As String, wsid As String, user As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, typeahead As New List(Of String)

            Try
                datareader = RunSPArray("selSystemReplenishmentNewTypeahead", wsid, {{"@searchString", query, strVar}, {"@searchColumn", column, strVar}})

                If datareader.HasRows Then
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            typeahead.Add(datareader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishment", "getSysReplnNewTypeahead", ex.Message, user, wsid)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return typeahead
        End Function

        ''' <summary>
        ''' Gets the typeahead for the search box on the system replenishment current page
        ''' </summary>
        ''' <param name="searchString">The string value ot be searched</param>
        ''' <param name="searchCol">The column that the stirng value will be searched in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="user">The suer that is logged in</param>
        ''' <returns>A list of string containing the data from the desired column that begins with the string string value</returns>
        ''' <remarks></remarks>
        Public Shared Function selCurrReplenSearchTypeahead(searchString As String, searchCol As String, WSID As String, user As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, DropList As New List(Of String)

            Try
                Select Case searchCol
                    Case "Trans Type"
                        searchCol = "Transaction Type"
                    Case "Carsl"
                        searchCol = "Carousel"
                    Case "Trans Qty"
                        searchCol = "Transaction Quantity"
                    Case "UofM"
                        searchCol = "Unit of Measure"
                    Case "Comp Date"
                        searchCol = "Completed Date"
                End Select

                DataReader = RunSPArray("selReplenReportSearchTA", WSID, {{"@searchString", searchString, strVar}, {"@searchColumn", searchCol, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        If Not IsDBNull(DataReader(0)) Then
                            DropList.Add(DataReader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishment", "selCurrReplenSearchTypeahead", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return DropList
        End Function

        ''' <summary>
        ''' The first part for deleting by a range
        ''' </summary>
        ''' <param name="query">The begin location</param>
        ''' <param name="DelCol">The column where this location is from</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="user">The user that is logged in</param>
        ''' <returns>A list of string containing possible locations</returns>
        ''' <remarks></remarks>
        Public Shared Function selDeleteRangeBegin(query As String, DelCol As String, WSID As String, user As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim BeginOptions As New List(Of String)

            Try
                DataReader = RunSPArray("delReplenReportBeginTA", WSID, {{"@DelCol", DelCol, strVar}, {"@BeginLoc", query, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        If Not IsDBNull(DataReader(0)) Then
                            BeginOptions.Add(DataReader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishment", "selDeleteRangeBegin", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return BeginOptions
        End Function

        ''' <summary>
        ''' The second part for deleting by a range
        ''' </summary>
        ''' <param name="query">The end location</param>
        ''' <param name="DelCol">The column that is being looked at</param>
        ''' <param name="Begin">The beginning date that was selected. IN order to only show results that come after it</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="user">The user that is currently logged on</param>
        ''' <returns>List of string containing possible locations</returns>
        ''' <remarks></remarks>
        Public Shared Function selDeleteRangeEnd(query As String, DelCol As String, Begin As String, WSID As String, user As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, EndOptions As New List(Of String)

            Try
                DataReader = RunSPArray("delReplenReportEndTA", WSID, {{"@DelCol", DelCol, strVar}, {"@BeginLoc", Begin, strVar}, {"@EndLoc", query, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        If Not IsDBNull(DataReader(0)) Then
                            EndOptions.Add(DataReader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishment", "selDeleteRangeEnd", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return EndOptions
        End Function

        ''' <summary>
        ''' Gets the picka nd put count info
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="user">The user that is currently logged on</param>
        ''' <returns>An object containing the pick and put count info</returns>
        ''' <remarks></remarks>
        Public Shared Function selReplenCountInfo(WSID As String, user As String) As Object
            Dim DataReader As SqlDataReader = Nothing, index As Integer = 0, PickCount As Integer = -1, PutCount As Integer = -1

            Try
                DataReader = RunSPArray("selOTReplenCount", WSID, {{"nothing"}})

                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            If Not IsDBNull(DataReader(0)) Then
                                PickCount = DataReader(0)
                            End If
                        ElseIf index = 1 Then
                            If Not IsDBNull(DataReader(0)) Then
                                PutCount = DataReader(0)
                            End If
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("SystemReplenishment", "selReplenCountInfo", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New With {.putcount = PutCount, .pickcount = PickCount}
        End Function
    End Class
End Namespace

