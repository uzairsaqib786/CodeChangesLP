' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System
Imports System.Web
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class OpenTransactions
    ''' <summary>
    ''' Will return the values in order to draw the table. each input is a different search parameter.
    ''' </summary>
    ''' <param name="draw">verifies that the current request is the one deisplayed. Never changed</param>
    ''' <param name="sDate">The start date.</param>
    ''' <param name="eDate">The end date.</param>
    ''' <param name="transType">The transaction type.</param>
    ''' <param name="transStatus">The transaction status.</param>
    ''' <param name="searchString">The search string given by user,</param>
    ''' <param name="searchColumn">The column to search the stirng for.</param>
    ''' <param name="sRow">The start row.</param>
    ''' <param name="eRow">The end row.</param>
    ''' <param name="orderNumber">The order number</param>
    ''' <param name="toteID">The tote id </param>
    ''' <param name="sortColumnNum">The column to sort on</param>
    ''' <param name="sortOrder">The order eigther ascending or descending</param>
    ''' <param name="user">The current user that is signed in</param>
    ''' <param name="filter">The filter for the table</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>the table object values ot draw the table</returns>
    ''' <remarks></remarks>
    Public Shared Function updateOpenTransTable(draw As Integer, sDate As DateTime, eDate As DateTime, transType As String, _
                                                transStatus As String, searchString As String, searchColumn As String, sRow As Integer, _
                                                eRow As Integer, orderNumber As String, toteID As String, sortColumnNum As Integer, _
                                                sortOrder As String, filter As String, user As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim columnSeq As List(Of String) = GlobalFunctions.getDefaultColumnSequence("Open Transactions", user, WSID)
        Dim sortColumn As String = columnSeq(sortColumnNum)
        ' clean input of potential sql injections
        orderNumber = GlobalFunctions.DangerAhead(orderNumber)
        toteID = GlobalFunctions.DangerAhead(toteID)
        searchString = GlobalFunctions.cleanSearch(searchString)

        ' if invalid date return an empty object
        If Not (sDate = nullDate Or eDate = nullDate Or sDate.CompareTo(minDate) < 0 Or eDate.CompareTo(minDate) < 0 Or sDate.CompareTo(maxDate) > 0 Or eDate.CompareTo(maxDate) > 0) Then
            Try
                table = GetJQueryDataTableResult(draw, "selOTDTNew", WSID, user, {{"@TransStatus", transStatus, strVar},
                                                                {"@TransType", transType, strVar},
                                                                {"@FromDate", sDate, dteVar},
                                                                {"@ToDate", eDate, dteVar},
                                                                {"@sRow", sRow, intVar},
                                                                {"@eRow", eRow, intVar},
                                                                {"@searchString", searchString, strVar},
                                                                {"@searchColumn", searchColumn, strVar},
                                                                {"@orderNumber", orderNumber, strVar},
                                                                {"@toteID", toteID, strVar},
                                                                {"@sortColumn", sortColumn, strVar},
                                                                {"@sortOrder", sortOrder, strVar},
                                                                {"@filter", filter, strVar}}, columnOrder:=columnSeq)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Open Transactions", "updateOpenTransTable", ex.Message, user, WSID)
            End Try
        End If
        Return table
    End Function

    ' returns the next few order numbers that fit the currently inputted order number as suggestions to be handled by Bloodhound plugin
    ''' <summary>
    ''' Grabs the typeahead information for the order number
    ''' </summary>
    ''' <param name="orderNumber">The current order number that is typed in</param>
    ''' <param name="user">The suer that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>a lits of all order numbers that start with the input order number</returns>
    ''' <remarks></remarks>
    Public Shared Function getNextOrderNumbers(orderNumber As String, user As String, WSID As String)
        Dim DataReader As SqlDataReader = Nothing
        Dim orders As New List(Of String)
        Try
            DataReader = RunSPArray("selOTOrderTA", WSID, {{"@OrderNumber", orderNumber + "%", strVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        orders.Add(DataReader(x))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Open Transactions", "getNextOrderNumbers", ex.Message, user, WSID)
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
            oldest = GetResultSingleCol("selOTItemOldestDate", WSID, {{"@ItemNum", itemNumber, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Open Transactions", "getOldestDate", ex.Message, user, WSID)
        End Try
        Return oldest
    End Function

End Class
