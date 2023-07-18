' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class DeAllocateOrders

        ''' <summary>
        ''' Gets allocated orders filtered by order number
        ''' </summary>
        ''' <param name="orderNum">Order Number filter</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The owrkstation that is currently being worked on</param>
        ''' <returns>A list of string containing all viable order numbers that begin with the input</returns>
        ''' <remarks></remarks>
        Public Shared Function getAllocatedOrders(orderNum As String, user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, orders As New List(Of String)
            Try
                DataReader = RunSPArray("selOTOrderNumAlloc", WSID, {{"@Order", orderNum & "%", strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        orders.Add(DataReader(0))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("DeAllocateOrders", "getAllocatedOrders", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return orders
        End Function

        ''' <summary>
        ''' Gets allocated orders by Item Number
        ''' </summary>
        ''' <param name="itemNum">Item Number to filter on</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string containing all viable item numbers that begin with the input</returns>
        ''' <remarks></remarks>
        Public Shared Function getAllocatedItems(itemNum As String, user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, orders As New List(Of String)
            Try
                DataReader = RunSPArray("selOTItemNumAlloc", WSID, {{"@ItemNumber", itemNum & "%", strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        orders.Add(DataReader(0))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("DeAllocateOrders", "getAllocatedItems", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return orders
        End Function

        ''' <summary>
        ''' Gets all allocated orders by item number and order number
        ''' </summary>
        ''' <param name="orderNum">Order Number to filter on</param>
        ''' <param name="itemNum">Item Number to filter on</param>
        ''' <param name="transType">The transaction type to filter on</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string containing order numbers that fit the given filters</returns>
        ''' <remarks></remarks>
        Public Shared Function getAllAllocatedOrders(orderNum As String, itemNum As String, transType As String, user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, orders As New List(Of String)
            Try
                DataReader = RunSPArray("selOTAllocAll", WSID, {{"@Order", orderNum, strVar}, {"@ItemNumber", itemNum, strVar}, {"@TransType", transType, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        orders.Add(DataReader(0))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex)
                insertErrorMessages("DeAllocateOrders", "getAllocatedOrders", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return orders
        End Function

        ''' <summary>
        ''' Gets the table data for the Order Items datatable (JS Plugin)
        ''' </summary>
        ''' <param name="draw">DataTables parameter to keep requests paired with responses</param>
        ''' <param name="sRow">Starting row</param>
        ''' <param name="eRow">Ending row</param>
        ''' <param name="sortColumnin">Index of the column sorted on</param>
        ''' <param name="sortOrder">ASC or DESC for sort order on the sorted column</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="OrderNum">Order Number filter</param>
        ''' <param name="displayFilter">Tells if order numbers or transactions are being displayed</param>
        ''' <param name="filter">The current filter applied to the table</param>
        ''' <param name="transType">The diesred transaction type to be displayed</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A table object containing the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getTableData(draw As Integer, sRow As Integer, eRow As Integer, sortColumnin As Integer, _
                                            sortOrder As String, transType As String, user As String, displayFilter As String, OrderNum As String, filter As String, WSID As String) As TableObject
            Dim DataReader As SqlDataReader = Nothing, Data As New List(Of List(Of String)), inners As New List(Of String)
            Dim recTotal As Integer = -1, recFilter As Integer = -1, index As Integer = 0
            Dim colList As String() = {"Order Number", "Item Number", "Description", "Priority", "Transaction Quantity", "Unit of Measure", "Batch Pick ID", "Transaction Type"}
            Dim sortColumn As String = colList.GetValue(sortColumnin)

            Try
                If IsNothing(OrderNum) Then
                    OrderNum = ""
                End If
                DataReader = RunSPArray("selOTAllocDT", WSID, {{"@OrderFilter", displayFilter, strVar}, _
                                                               {"@Order", OrderNum, strVar}, {"@sRow", sRow, intVar}, _
                                                               {"@eRow", eRow, intVar}, {"@sortColumn", sortColumn, strVar}, _
                                                               {"@sortOrder", sortOrder, strVar}, _
                                                               {"@TransType", transType, strVar}, _
                                                               {"@filter", filter, strVar}})
                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            recTotal = DataReader(0)
                            recFilter = DataReader(0)
                        Else
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                inners.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                            Next
                            inners.Add("")
                            Data.Add(inners)
                            inners = New List(Of String)
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("DeAllocateOrders", "getTableData", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, recTotal, recFilter, Data)
        End Function
    End Class
End Namespace
