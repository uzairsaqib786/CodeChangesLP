' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace ImportExport
    Public Class ManageDataModalTables

        ''' <summary>
        ''' Gets the data for the desired inventory map table
        ''' </summary>
        ''' <param name="draw">Tells if the datatable is being drawn</param>
        ''' <param name="startRow">The first row that being displayed</param>
        ''' <param name="endRow">The last row that is being displayed</param>
        ''' <param name="sortCol">The column that is being sorted on</param>
        ''' <param name="sortOrder">The order that the column is being sorted by</param>
        ''' <param name="filter">The filter that is applied to the table</param>
        ''' <param name="table">The specific inventory map table whose data will be selected</param>
        ''' <param name="User">The currently logged in user</param>
        ''' <param name="WSID">The wurrent workstation being worked on</param>
        ''' <returns>Table Object containg the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getInvMapTables(draw As Integer, startRow As Integer, endRow As Integer, sortCol As Integer, _
                                             sortOrder As String, filter As String, table As String, User As String, WSID As String) As TableObject
            Dim colSequence As New List(Of String) From {"Inv Map ID", "Transaction Type", "Location ID", "Warehouse", "Location Number",
                                                         "Cell Size", "Golden Zone", "Zone", "Carousel", "Row", "Shelf", "Bin", "Item Number", "Description",
                                                         "Item Quantity", "Unit of Measure", "Put Away Date", "Maximum Quantity", "Revision", "Serial Number",
                                                         "Lot Number", "Expiration Date", "User Field1", "User Field2", "Date Sensitive", "Dedicated"}
            Dim column As String = colSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0
            Dim FilterCount As Integer = 0


            Try
                If table = "current" Then
                    DataReader = RunSPArray("selCurrInvMapDataManDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                         {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                         {"@filter", filter, strVar}})
                ElseIf table = "import" Then
                    DataReader = RunSPArray("selImpInvMapDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                         {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                         {"@filter", filter, strVar}})
                ElseIf table = "export" Then
                    DataReader = RunSPArray("selExpInvMapDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                         {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                         {"@filter", filter, strVar}})
                ElseIf table = "archive" Then
                    DataReader = RunSPArray("selArchInvMapDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                         {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                         {"@filter", filter, strVar}})
                ElseIf table = "audit" Then
                    DataReader = RunSPArray("selAuditInvMapDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                        {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                        {"@filter", filter, strVar}})
                End If
                
                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        FilterCount = DataReader("FilterCount")
                        For Each col In colSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        data.Add(inners)
                    End While
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ManageDataModalTables", "getInvMapTables", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, RecordCount, FilterCount, data)
        End Function

        ''' <summary>
        ''' Gets the data for the desired inventory table
        ''' </summary>
        ''' <param name="draw">Tells if the datatable is being drawn</param>
        ''' <param name="startRow">The first row that being displayed</param>
        ''' <param name="endRow">The last row that is being displayed</param>
        ''' <param name="sortCol">The column that is being sorted on</param>
        ''' <param name="sortOrder">The order that the column is being sorted by</param>
        ''' <param name="filter">The filter that is applied to the table</param>
        ''' <param name="table">The specific inventory table whose data will be selected</param>
        ''' <param name="User">The currently logged in user</param>
        ''' <param name="WSID">The wurrent workstation being worked on</param>
        ''' <returns>Table Object containg the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getInvTables(draw As Integer, startRow As Integer, endRow As Integer, sortCol As Integer, _
                                             sortOrder As String, filter As String, table As String, User As String, WSID As String) As TableObject
            Dim colSequence As New List(Of String) From {"Transaction Type", "Item Number", "Supplier Item ID", "Description", "Category",
                                                        "Sub Category", "Primary Zone", "Supplier Number", "Manufacturer", "Model",
                                                        "Special Features", "Reorder Point", "Reorder Quantity", "Unit of Measure",
                                                        "Secondary Zone", "Carousel Cell Size", "Carousel Velocity", "FIFO", "Active",
                                                        "Date Sensitive", "Warehouse Sensitive", "Carousel Min Qty", "Carousel Max Qty",
                                                        "Pick Fence Qty", "Split Case", "Case Quantity", "Unit Cost", "Bulk Cell Size", "Bulk Velocity",
                                                        "Bulk Min Qty", "Bulk Max Qty", "CF Cell Size", "CF Velocity", "CF Min Qty",
                                                        "CF Max Qty", "Replenishment Point", "Replenishment Level", "Avg Piece Weight",
                                                        "Sample Quantity", "Use Scale", "Min Use Scale Quantity", "Kit Item Number",
                                                        "Kit Quantity", "Scan Code", "Scan Type", "Scan Range", "Start Position",
                                                        "Code Length", "Minimum RTS Reel Quantity", "Pick Sequence", "Include In Auto RTS Update"}

            Dim column As String = colSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0
            Dim FilterCount As Integer = 0

            Try

                If table = "current" Then
                    DataReader = RunSPArray("selCurrInvDataManDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                         {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                         {"@filter", filter, strVar}})
                ElseIf table = "import" Then
                    DataReader = RunSPArray("selImpInvDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                         {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                         {"@filter", filter, strVar}})
                ElseIf table = "export" Then
                    DataReader = RunSPArray("selExpInvDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                        {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                        {"@filter", filter, strVar}})
                ElseIf table = "archive" Then
                    DataReader = RunSPArray("selArchInvDataManDT", "IE", {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                        {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                        {"@filter", filter, strVar}})
                ElseIf table = "kit" Then
                    DataReader = RunSPArray("selKitInvDataManDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                        {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                        {"@filter", filter, strVar}})
                ElseIf table = "scan codes" Then
                    DataReader = RunSPArray("selScanCodeInvDataManDT", WSID, {{"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                        {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}, _
                                                                        {"@filter", filter, strVar}})
                End If



                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        FilterCount = DataReader("FilterCount")
                        For Each col In colSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ManageDataModalTables", "getInvTables", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return New TableObject(draw, RecordCount, FilterCount, data)
        End Function

    End Class
End Namespace

