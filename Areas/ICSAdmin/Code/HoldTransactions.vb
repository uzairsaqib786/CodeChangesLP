' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class HoldTransactions
        ''' <summary>
        ''' Selects filtered data for the datatables plugin for the purpose of placing selected transactions on hold (into reprocess)
        ''' </summary>
        ''' <param name="draw">DataTables parameter, don't edit</param>
        ''' <param name="sRow">DataTables parameter, don't edit</param>
        ''' <param name="eRow">DataTables parameter, don't edit</param>
        ''' <param name="sortColumnin">Column to be sorted/ordered by</param>
        ''' <param name="sortOrder">SQL Order By asc or desc</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="ident">Identifies if order number or item number were given</param>
        ''' <param name="reels">Identifies if looking at non reels or reels</param>
        ''' <param name="orderitem">The order or item number to display the data for</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A tableobject containing the data needed for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function selHoldTransactionsData(draw As Integer, sRow As Integer, eRow As Integer, sortColumnin As Integer, _
                                                       sortOrder As String, user As String, ident As String, reels As String, orderitem As String, WSID As String) As TableObject
            Dim columns As String() = {"Order Number", "Item Number", "Warehouse", "Location", "Transaction Type", "Transaction Quantity", "Serial Number", "Lot Number", "Line Number", _
                                       "Host Transaction ID", "Tote ID", "ID"}
            Dim sortColumn As String = columns.GetValue(sortColumnin)

            Dim DataReader As SqlDataReader = Nothing, index As Integer = 0, innerList As New List(Of String), data As New List(Of List(Of String)), recordsTotal As Integer = -1
            Dim recordsFiltered As Integer = -1, reelInt As Integer = 0

            If IsNothing(orderitem) Then
                orderitem = ""
            End If

            Try
                Select Case reels
                    Case "reel"
                        reelInt = 1
                    Case "non"
                        reelInt = 0
                    Case "both"
                        reelInt = 2
                End Select
                Select Case ident
                    Case "Order Number"
                        DataReader = RunSPArray("selOTOrderNumHoldDT", WSID, {{"@OrderNum", orderitem, strVar}, {"@Reels", reelInt, intVar}, {"@sRow", sRow, intVar}, {"@eRow", eRow, strVar}, _
                                                                                       {"@sortColumn", sortColumn, strVar}, {"@sortOrder", sortOrder, strVar}})
                    Case "Item Number"
                        DataReader = RunSPArray("selOTItemNumHoldDT", WSID, {{"@ItemNum", orderitem, strVar}, {"@Reels", reelInt, intVar}, {"@sRow", sRow, intVar}, {"@eRow", eRow, strVar}, _
                                                                                      {"@sortColumn", sortColumn, strVar}, {"@sortOrder", sortOrder, strVar}})
                End Select
                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            recordsTotal = DataReader(0)
                        ElseIf index = 1 Then
                            recordsFiltered = DataReader(0)
                        Else
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                innerList.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                            Next
                            data.Add(innerList)
                            innerList = New List(Of String)
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("HoldTransactions", "selHoldTransactions", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return New TableObject(draw, recordsTotal, recordsFiltered, data)
        End Function

        ''' <summary>
        ''' Gets the typeahead for order/item numbers for hold transactions
        ''' </summary>
        ''' <param name="type">Item Number or Order Number string to differentiate between the filters</param>
        ''' <param name="orderitem">The order or item number to filter on</param>
        ''' <param name="reel">Reel, Non, or All Transactions</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>Typeahead suggestions</returns>
        ''' <remarks></remarks>
        Public Shared Function getTypeaheadHoldTransactions(type As String, orderitem As String, reel As String, user As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing, returnvals As New List(Of Object), reelInt As Integer = 0
            Try
                Select Case reel
                    Case "reel"
                        reelInt = 1
                    Case "non"
                        reelInt = 0
                    Case "both"
                        reelInt = 2
                End Select
                Select Case type
                    Case "Item Number"
                        DataReader = RunSPArray("selOTItemNumHold", WSID, {{"@ItemNum", orderitem & "%", strVar}, {"@Reels", reelInt, intVar}})
                    Case "Order Number"
                        DataReader = RunSPArray("selOTOrderNumHold", WSID, {{"@OrderNum", orderitem & "%", strVar}, {"@Reels", reelInt, intVar}})
                End Select
                If DataReader.HasRows Then
                    While DataReader.Read()
                        returnvals.Add(New With {.Number = IIf(IsDBNull(DataReader(0)), "", DataReader(0)), .Info = IIf(IsDBNull(DataReader(1)), "", DataReader(1))})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("HoldTransactions", "getTypeaheadHoldTransactions", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return returnvals
        End Function

        ''' <summary>
        ''' Gets the subcategories of any reel item numbers and returns whether any were found
        ''' </summary>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A boolean telling i frells should be displayed</returns>
        ''' <remarks></remarks>
        Public Shared Function getReelCount(user As String, WSID As String) As Boolean
            Dim DataReader As SqlDataReader = Nothing, dispReel As Boolean = True

            Try
                DataReader = RunSPArray("selReelCount", WSID, {{"nothing"}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        If DataReader(0) <= 0 Then
                            dispReel = False
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("HoldTransactions", "getReelCount", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return dispReel
        End Function
    End Class

End Namespace
