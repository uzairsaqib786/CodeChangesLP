' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System
Imports System.Web
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class TransactionHistory

    ' Redraw the transaction history table on the client side by requerrying for all needed data
    ''' <summary>
    ''' Gives the table object to draw and redraw the transaction history table
    ''' </summary>
    ''' <param name="draw">Datatables parmater which does not change</param>
    ''' <param name="sDate">The start date</param>
    ''' <param name="eDate">The end date</param>
    ''' <param name="searchString">The searchstring given by the user</param>
    ''' <param name="searchColumn">The column for which the search string will be search on</param>
    ''' <param name="sRow">The start row</param>
    ''' <param name="eRow">The end row</param>
    ''' <param name="orderNumber">The order number that cna be filtered</param>
    ''' <param name="sortColumnNum">The column which is being sorted</param>
    ''' <param name="sortOrder">The sort order either ascending or descending</param>
    ''' <param name="user">The current user that is logged in</param>
    ''' <param name="filter">The filter being applied to the table</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A TableObject that contains the data needed for a datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function updateTransHistTable(draw As Integer, sDate As DateTime, eDate As DateTime, searchString As String, searchColumn As String,
                                                sRow As Integer, eRow As Integer, orderNumber As String, sortColumnNum As Integer, sortOrder As String, filter As String,
                                                user As String, WSID As String) As TableObject
        Dim columnSeq As List(Of String) = GlobalFunctions.getDefaultColumnSequence("Transaction History", user, WSID)
        Dim sortColumn As String = columnSeq(sortColumnNum)
        ' clean order number and search strings from potential injections
        orderNumber = GlobalFunctions.DangerAhead(orderNumber)
        searchString = GlobalFunctions.cleanSearch(searchString)
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        ' if date is invalid don't try to get the data
        If Not (sDate = nullDate Or eDate = nullDate Or sDate.CompareTo(minDate) < 0 Or eDate.CompareTo(minDate) < 0 Or sDate.CompareTo(maxDate) > 0 Or eDate.CompareTo(maxDate) > 0) Then
            Try
                table = GetJQueryDataTableResult(draw, "selTHDTNew", WSID, user, {{"@startDate", sDate, dteVar},
                                                                {"@endDate", eDate, dteVar},
                                                                    {"@sRow", sRow, intVar},
                                                                    {"@eRow", eRow, intVar},
                                                                    {"@searchString", searchString, strVar},
                                                                    {"@searchColumn", searchColumn, strVar},
                                                                    {"@orderNumber", orderNumber, strVar},
                                                                    {"@sortColumn", sortColumn, strVar},
                                                                    {"@sortOrder", sortOrder, strVar},
                                                                    {"@filter", filter, strVar}}, columnOrder:=columnSeq)

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Transaction History", "updateTransHistTable", ex.Message, user, WSID)
            End Try
        End If
        Return table
    End Function

    ' get the next order numbers that fit the currently input string for suggestions through Bloodhound plugin.
    ''' <summary>
    ''' Get the typeahead for the ordernumber column
    ''' </summary>
    ''' <param name="orderNumber">The vlaue currenlty typed in the ordernumber field</param>
    ''' <param name="user">The suer that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of order numbers that begin with the input</returns>
    ''' <remarks></remarks>
    Public Shared Function getNextOrderNumbersTrans(orderNumber As String, user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim orders As New List(Of String)
        Try
            ' Order number concatenation below is necessary for like clause to return the proper results
            DataReader = RunSPArray("selTHOrderNumTA", WSID, {{"@OrderNumber", orderNumber + "%", strVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        orders.Add(DataReader(x))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Transaction History", "getNextOrderNumbersTrans", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return orders
    End Function

    ''' <summary>
    ''' Gets oldest Entry by Item Number
    ''' </summary>
    ''' <param name="itemNumber">Item Number to get oldest date for</param>
    ''' <param name="user">The user that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>String containing the oldest date</returns>
    ''' <remarks></remarks>
    Public Shared Function getOldestDate(itemNumber As String, user As String, WSID As String) As String
        Dim oldest As String = ""
        Try
            oldest = GetResultSingleCol("selTHItemOldestDate", WSID, {{"@ItemNum", itemNumber, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Transaction Hisotry", "getOldestDate", ex.Message, user, WSID)
        End Try
        Return oldest
    End Function

End Class