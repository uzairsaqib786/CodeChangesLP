' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace WorkManager
    Public Class SelectWork
        ''' <summary>
        ''' DataTable function for the Select Work page tables
        ''' </summary>
        ''' <param name="draw">Paramater needed for the datatable</param>
        ''' <param name="sRow">The row number to start from</param>
        ''' <param name="eRow">The row number to end from</param>
        ''' <param name="which">String "selected" or "unselected"</param>
        ''' <param name="selected">String of single quoted CSVs which indicates which batches, orders or totes have been selected by the user. Like "'batchID', 'batch2', 'batch3'"</param>
        ''' <param name="transType">String "pick", "put away", "count"</param>
        ''' <param name="batchOrderTote">String "batch", "order" or "tote"</param>
        ''' <param name="pluginWhere">Custom filter plugin's where string</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="user">The user that si currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A table object that contains the data needed in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSelectWorkTables(draw As Integer, sRow As Integer, eRow As Integer, ByVal which As String, selected As String, transType As String, ByVal batchOrderTote As String, _
                                                   ByVal pluginWhere As String, sortCol As Integer, sortOrder As String, user As String, WSID As String, selectedUser As String) As TableObject
            Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)), New List(Of Integer))
            Dim sort As String = "", pluginReplace As String = "", sp As String = ""
            which = which.ToLower()
            Select Case batchOrderTote.ToLower()
                Case "batch"
                    pluginReplace = "[Open Transactions].[Batch Pick ID]"
                    sp = "selWMBatchTable"
                Case "order"
                    pluginReplace = "[Order Number]"
                    sp = "selWMOrderTable"
                Case Else
                    pluginReplace = "[Tote ID]"
                    sp = "selWMToteTable"
            End Select
            Select Case sortCol
                Case 0
                    sort = "[Transaction Type]"
                Case 1
                    sort = pluginReplace
                Case 2
                    sort = "[Required Date]"
                Case 3
                    sort = "[Priority]"
                Case 4
                    sort = "COUNT(*)"
                Case 5
                    sort = "[WMAssignedWork].[Username]"
                Case Else
                    sort = ""
            End Select
            ' replace our swapped column
            ' [Column 2] is an alias for either Batch Pick ID, Order Number or Tote ID depending on which radio buttons the user has selected.
            pluginWhere = pluginWhere.Replace("[Column 2]", pluginReplace)

            Try
                table = GetJQueryDataTableResult(draw, sp, WSID, user, {{"@Which", CastAsSqlBool(which.ToLower() = "selected"), intVar}, {"@Selected", selected, strVar}, _
                                                                    {"@TransType", transType, strVar}, {"@PluginWhere", pluginWhere, strVar}, _
                                                                    {"@StartRow", sRow, intVar}, {"@EndRow", eRow, intVar}, {"@SortCol", sort, strVar}, {"@SortOrder", sortOrder, strVar}, _
                                                                    {"@User", selectedUser, strVar}, {"@WSID", WSID, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWork", "GetSelectWorkTables", ex.Message, user, WSID)
            End Try
            Return table
        End Function

        ''' <summary>
        ''' Gets the number of picks, put aways, counts, batches, totes, and order numbers that fit the count criteria
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="transType">Currently selected transaction type</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns></returns>
        ''' <remarks>Originally included in the datatables function for SelectWork, but it caused table draws to take too long.</remarks>
        Public Shared Function GetWorkCounts(user As String, transType As String, WSID As String) As List(Of Integer)
            Dim reader As SqlDataReader = Nothing
            Dim counts As New List(Of Integer)
            Try
                reader = RunSPArray("selWMCountData", WSID, {{"@TransType", transType, strVar}, {"@User", user, strVar}})
                While reader.HasRows
                    While reader.Read
                        If Not IsDBNull(reader(0)) Then
                            counts.Add(reader(0))
                        Else
                            counts.Add(0)
                        End If
                    End While
                    reader.NextResult()
                End While
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWork", "GetWorkCounts", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return counts
        End Function

        ''' <summary>
        ''' Assigns a batch ID to the selected transactions
        ''' </summary>
        ''' <param name="selected">A comma seperated string that contians all the desired transctions</param>
        ''' <param name="transType">The transaction type of the transactions</param>
        ''' <param name="isTote">If the transactions are tote ids</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function AssignBatchID(selected As String, transType As String, isTote As Integer, user As String, WSID As String) As Boolean
            Try
                RunActionSP("updWMOTBatch", WSID, {{"@User", user, strVar}, {"@Selected", selected, strVar}, {"@TransType", transType, strVar}, {"@Tote", isTote, intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("SelectWork", "AssignBatchID", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function
    End Class
End Namespace