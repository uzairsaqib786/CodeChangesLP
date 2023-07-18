' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class ReprocessTransactions

    ''' <summary>
    ''' Gets both Item Number and Order Number reprocess typeahead data
    ''' </summary>
    ''' <param name="ItemNumber">Item Number to match against</param>
    ''' <param name="OrderNumber">Order Number to match against</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A list of object that contains the typeahead information</returns>
    ''' <remarks></remarks>
    Public Shared Function getReprocessTypeahead(ItemNumber As String, OrderNumber As String, user As String, WSID As String, history As Boolean) As List(Of Object)
        Dim dr As SqlDataReader = Nothing
        Dim typeahead As New List(Of Object)
        Dim sp As String = ""
        If history Then
            sp = "selReprocessHistTypeahead"
        Else
            sp = "selOTTempTA"
        End If
        Try
            dr = RunSPArray(sp, WSID, {{"@ItemNumber", ItemNumber & "%", strVar}, {"@OrderNumber", OrderNumber & "%", strVar}})

            If dr.HasRows Then
                While dr.Read()
                    typeahead.Add(New With {
                                  .ItemNumber = CheckDBNull(dr(0)),
                                  .OrderNumber = CheckDBNull(dr(1)),
                                  .DateField = IIf(IsDBNull(dr(2)), "", CDate(dr(2)).ToString()),
                                  .TransType = CheckDBNull(dr(3)),
                                  .Qty = CheckDBNull(dr(4))
                              })
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getReprocessTypeahead", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(dr) Then dr.Dispose()
        End Try
        Return typeahead
    End Function

    ''' <summary>
    ''' Gets the data for the Reprocess Transactions DataTable
    ''' </summary>
    ''' <param name="draw">DataTable parameter to keep requests paired with responses</param>
    ''' <param name="sRow">Start row</param>
    ''' <param name="eRow">End row</param>
    ''' <param name="sortColumnNum">Index of the column to sort on</param>
    ''' <param name="sortOrder">Order to sort the sorted column on</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A table object that contians the data needed for the datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function getReprocessTable(draw As Integer, sRow As String, eRow As String, sortColumnNum As Integer, sortOrder As String, orderNumber As String,
                                             itemNumber As String, hold As Boolean, searchColumn As String, searchString As String, user As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim columnSeq As List(Of String) = GlobalFunctions.getDefaultColumnSequence("Open Transactions Temp", user, WSID)
        Dim sortColumn As String = columnSeq(sortColumnNum)
        Try
            table = GetJQueryDataTableResult(draw, "selOTTempDTNew2", WSID, user, {{"@sRow", sRow, strVar},
                                                                    {"@eRow", eRow, strVar},
                                                                    {"@OrderNumber", orderNumber, strVar},
                                                                    {"@ItemNumber", itemNumber, strVar},
                                                                    {"@Hold", hold, boolVar},
                                                                    {"@searchColumn", searchColumn, strVar},
                                                                    {"@searchString", searchString, strVar},
                                                                    {"@sortColumn", sortColumn, strVar},
                                                                    {"@sortOrder", sortOrder, strVar}}, columnOrder:=columnSeq)

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getReprocessTable", ex.ToString(), user, WSID)
        End Try
        Return table
    End Function

    ''' <summary>
    ''' Gets the search results for the reprocess datatable
    ''' </summary>
    ''' <param name="draw">Tells if the table is currently being drawn</param>
    ''' <param name="sRow">The row number where the records start</param>
    ''' <param name="eRow">The row number where the records end</param>
    ''' <param name="sortColumn">The index of the column that is currently being sorted on</param>
    ''' <param name="sortOrder">The direction of the sort</param>
    ''' <param name="filter">The filter that is being applied</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <returns>A table object containg the filtered results</returns>
    Public Shared Function getReprocTransSearchResults(draw As Integer, sRow As String, eRow As String, sortColumn As Integer, sortOrder As String,
                                                       filter As String, user As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim column As String() = {"Item Number", "Date Stamp", "Transaction Type", "Transaction Quantity", "Reprocess", "Post as Complete", "Send to History", "ID"}
        Try
            table = GetJQueryDataTableResult(draw, "selOTTempSearchResults", WSID, user, {{"@sRow", sRow, strVar}, {"@eRow", eRow, strVar},
                                                      {"@sortColumn", column.GetValue(sortColumn), strVar}, {"@sortOrder", sortOrder, strVar}, {"@filter", filter, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getReprocTransSearchResults", ex.ToString(), user, WSID)
        End Try
        Return table
    End Function


    ''' <summary>
    ''' Gets the Reprocess Table's reprocess, post as complete and send to history fields with order number and item number
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <returns>An object that contains the data for each posting choice</returns>
    ''' <remarks></remarks>
    Public Shared Function getReprocessTransInclude(user As String, WSID As String) As Object
        Dim dr As SqlDataReader = Nothing
        Dim innerlist As New List(Of Object)
        Dim index As Integer = 0
        Dim returnObj As Object = New With {.Reprocess = New List(Of Object), .PostComplete = New List(Of Object), .SendHist = New List(Of Object)}
        Try
            dr = RunSPArray("selOTTempInclude", WSID, {{"nothing"}})

            While dr.HasRows
                While dr.Read()
                    innerlist.Add(New With {.OrderNumber = CheckDBNull(dr(0)),
                                            .ItemNumber = CheckDBNull(dr(1)),
                                            .ID = CheckDBNull(dr(2))})
                End While
                dr.NextResult()
                If index = 0 Then
                    returnObj.Reprocess = innerlist
                ElseIf index = 1 Then
                    returnObj.PostComplete = innerlist
                Else
                    returnObj.SendHist = innerlist
                End If
                innerlist = New List(Of Object)
                index += 1
            End While
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getReprocessTransInclude", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(dr) Then dr.Dispose()
        End Try
        Return returnObj
    End Function

    ''' <summary>
    ''' Sets the Reprocess, Post as Complete and Send to History fields in Open Transactions Temp by ID
    ''' </summary>
    ''' <param name="ID">ID of the entry to edit</param>
    ''' <param name="reprocess">If the transaction is marked for reprocess</param>
    ''' <param name="postcomplete">If the transaction is marked for post as complete</param>
    ''' <param name="sendhist">Is the transaction marked for send to history</param>
    ''' <param name="user">Requesting user</param>
    ''' <remarks></remarks>
    Public Shared Sub setReprocessInclude(ID As Integer, reprocess As Integer, postcomplete As Integer, sendhist As Integer, field As String, user As String, WSID As String)
        Try
            RunActionSP("updOTTempInclude", WSID, {{"@ID", ID, intVar}, {"@Field", field, strVar}, {"@Reprocess", IIf(reprocess, 1, 0), intVar},
                                                   {"@PostComplete", IIf(postcomplete, 1, 0), intVar}, {"@SendHist", IIf(sendhist, 1, 0), intVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "setReprocessInclude", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the filtered orders from open transactions temp
    ''' </summary>
    ''' <param name="ItemNumber">Item Number filter</param>
    ''' <param name="OrderNumber">Order Number filter</param>
    ''' <param name="Holds">Hold or all transactions</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A list that contains the order numbers that satisfy the filters</returns>
    ''' <remarks></remarks>
    Public Shared Function getFilteredOrders(ItemNumber As String, OrderNumber As String, Holds As Boolean, history As Boolean, user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim orders As New List(Of String)
        Try
            DataReader = RunSPArray("selReprocessOrderNumbers", WSID, {{"@ItemNumber", ItemNumber, strVar}, {"@OrderNumber", OrderNumber, strVar}, {"@Holds", IIf(Holds, 1, 0), intVar},
                                                                 {"@History", IIf(history, 1, 0), intVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    If Not IsDBNull(DataReader(0)) Then
                        orders.Add(DataReader(0))
                    End If
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getFilteredOrders", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return orders
    End Function

    ''' <summary>
    ''' Gets the orders marked for reprocess, post as complete and send to history
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A object that contains the records that are in each post option</returns>
    ''' <remarks></remarks>
    Public Shared Function getOrdersToPost(user As String, WSID As String) As ReprocCompleteHistOrdersObj
        Dim datareader As SqlDataReader = Nothing
        Dim orders As New ReprocCompleteHistOrdersObj
        Dim index As Integer = 0
        Try
            datareader = RunSPArray("selReprocessInclude", WSID, {{"nothing"}})

            While index < 6
                While datareader.Read()
                    If index = 0 Then
                        orders.Reprocess.Add(New With {.OrderNumber = IIf(IsDBNull(datareader(0)), "", datareader(0)),
                                                       .ItemNumber = IIf(IsDBNull(datareader(1)), "", datareader(1)),
                                                       .ID = IIf(IsDBNull(datareader(2)), "", datareader(2))})
                    ElseIf index = 1 Then
                        orders.Complete.Add(New With {.OrderNumber = IIf(IsDBNull(datareader(0)), "", datareader(0)),
                                                       .ItemNumber = IIf(IsDBNull(datareader(1)), "", datareader(1)),
                                                       .ID = IIf(IsDBNull(datareader(2)), "", datareader(2))})
                    ElseIf index = 2 Then
                        orders.History.Add(New With {.OrderNumber = IIf(IsDBNull(datareader(0)), "", datareader(0)),
                                                       .ItemNumber = IIf(IsDBNull(datareader(1)), "", datareader(1)),
                                                       .ID = IIf(IsDBNull(datareader(2)), "", datareader(2))})
                    ElseIf index = 3 Then
                        orders.ReprocessCount = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                    ElseIf index = 4 Then
                        orders.CompleteCount = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                    ElseIf index = 5 Then
                        orders.HistoryCount = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                    End If
                End While
                index += 1
                datareader.NextResult()
            End While
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getOrdersToPost", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(datareader) Then datareader.Dispose()
        End Try
        Return orders
    End Function

    ''' <summary>
    ''' Gets information for a specific transaction for editing
    ''' </summary>
    ''' <param name="ID">ID of the reprocess transaction to get.</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A list of string that contains the data for the selected trasnaction</returns>
    ''' <remarks></remarks>
    Public Shared Function getTransaction(ID As Integer, history As Boolean, user As String, WSID As String) As List(Of String)
        Dim datareader As SqlDataReader = Nothing
        Dim transaction As New List(Of String)
        Try
            datareader = RunSPArray("selReprocessTransactionByID", WSID, {{"@ID", ID, intVar}, {"@History", IIf(history, 1, 0), intVar}})

            If datareader.HasRows Then
                If datareader.Read() Then
                    For x As Integer = 0 To datareader.FieldCount - 1
                        ' reprocess transaction history data is stored with the reason and reason message in a single field, so we need to split it out if we're on that field
                        If history And x = 28 Then
                            Dim notes As String = IIf(IsDBNull(datareader(x)), "", datareader(x)).ToString().Trim()
                            Dim noteList As String() = notes.Split({":", "-"}, StringSplitOptions.None)
                            If noteList.Count = 1 Then
                                transaction.AddRange({noteList.GetValue(0).ToString().Trim(), ""})
                            ElseIf noteList.Count >= 2 Then
                                transaction.AddRange({noteList.GetValue(0).ToString().Trim(), noteList.GetValue(1).ToString().Trim()})
                            Else
                                transaction.AddRange({"", ""})
                            End If
                        ElseIf (history And x <> 29) Or Not history Then
                            transaction.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getTransaction", ex.ToString(), user, WSID)
        End Try
        Return transaction
    End Function

    ''' <summary>
    ''' Saves an edited transaction in the reprocess queue
    ''' </summary>
    ''' <param name="ID">ID of the transaction</param>
    ''' <param name="oldvalues">Old values to check for conflicts</param>
    ''' <param name="newvalues">New values to save</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Shared Function saveTransaction(ID As Integer, oldvalues As List(Of String), newvalues As List(Of String), user As String, WSID As String) As Boolean
        Dim comparisonvalues As List(Of String)
        Try
            comparisonvalues = getTransaction(ID, False, user, WSID)
            ' if our values were out of date when save occurred then refresh the data first and prompt to save again
            If Not comparisonvalues.SequenceEqual(oldvalues) Then
                Return True
            Else
                RunActionSP("updateReprocessTransaction", WSID, {{"@ID", ID, intVar}, {"@Qty", newvalues(0), intVar}, {"@UoM", IIf(newvalues(1) Is Nothing, "", newvalues(1)), strVar}, {"@SN", newvalues(2), strVar},
                                                           {"@Lot", newvalues(3), strVar}, {"@ExpDate", newvalues(4), dteVar}, {"@Revision", newvalues(5), strVar},
                                                           {"@Notes", newvalues(6), strVar}, {"@UF1", newvalues(7), strVar}, {"@UF2", newvalues(8), strVar}, {"@HostTID", newvalues(9), strVar},
                                                           {"@Required", newvalues(10), strVar}, {"@Batch", newvalues(11), strVar}, {"@LineNum", newvalues(12), intVar}, {"@LineSeq", newvalues(13), intVar},
                                                           {"@Priority", newvalues(14), intVar}, {"@Label", newvalues(15), intVar}, {"@Emergency", newvalues(16), intVar}, {"@Warehouse", IIf(newvalues(17) Is Nothing, "", newvalues(17)), strVar}})
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "saveTransaction", ex.ToString(), user, WSID)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Gets the data for the Reprocess Transactions DataTable
    ''' </summary>
    ''' <param name="draw">DataTable parameter to keep requests paired with responses</param>
    ''' <param name="OrderNumber">Order number to filter on</param>
    ''' <param name="sRow">Start row</param>
    ''' <param name="eRow">End row</param>
    ''' <param name="sortColumnNum">Index of the column to sort on</param>
    ''' <param name="sortOrder">Order to sort the sorted column on</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A table object that contains the infomration for the recordss in reprocess history</returns>
    ''' <remarks></remarks>
    Public Shared Function getReprocessHistTable(draw As Integer, sRow As String, eRow As String, sortColumnNum As Integer, sortOrder As String, orderNumber As String, itemNumber As String,
                                              searchColumn As String, searchString As String, user As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim columnSeq As List(Of String) = GlobalFunctions.getDefaultColumnSequence("Open Transactions Temp", user, WSID) 'since ot temp and history on same page use same col sequence
        Dim sortColumn As String = columnSeq(sortColumnNum)
        Try
            'remove unecessary columns
            columnSeq.Remove("Reason Message")
            columnSeq.Remove("Reason")
            columnSeq.Remove("Date Stamp")
            columnSeq.Remove("Name Stamp")
            columnSeq.Remove("Reprocess")
            columnSeq.Remove("Post as Complete")
            columnSeq.Remove("Send to History")

            table = GetJQueryDataTableResult(draw, "selReprocessHistTableNew", WSID, user, {{"@sRow", sRow, strVar},
                                                                                        {"@eRow", eRow, strVar},
                                                                                        {"@sortColumn", sortColumn, strVar},
                                                                                        {"@searchColumn", searchColumn, strVar},
                                                                                        {"@searchString", searchString, strVar},
                                                                                        {"@sortOrder", sortOrder, strVar},
                                                                                        {"@OrderNumber", orderNumber, strVar},
                                                                                        {"@ItemNumber", itemNumber, strVar}}, columnOrder:=columnSeq)

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ReprocessTransactions", "getReprocessTable", ex.ToString(), user, WSID)
        End Try
        Return table
    End Function

End Class


