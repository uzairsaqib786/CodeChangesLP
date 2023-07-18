' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class ManualTransactions
    ''' <summary>
    ''' Gets typeahead for manual transactions temporary order numbers
    ''' </summary>
    ''' <param name="transaction">Order Number of temporary transaction</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of object containing order numbers and thier info that begin with the given value</returns>
    ''' <remarks></remarks>
    Public Shared Function manualTransactionsTypeahead(transaction As String, user As String, WSID As String) As List(Of Object)
        Dim DataReader As SqlDataReader = Nothing, typeahead As New List(Of Object)

        Try
            DataReader = RunSPArray("selXferOTTA", WSID, {{"@OrderNumber", transaction & "%", strVar}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    typeahead.Add(New With {.OrderNumber = CheckDBNull(DataReader(0)), _
                                            .TransactionType = CheckDBNull(DataReader(1)), _
                                            .ItemNumber = CheckDBNull(DataReader(2)), _
                                            .TransQty = CheckDBNull(DataReader(3)), _
                                            .ID = CheckDBNull(DataReader(4))})
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("ManualTransactions", "manualTransactionsTypeahead", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return typeahead
    End Function

    ''' <summary>
    ''' Returns field values of a Manual Transaction specified by ID
    ''' </summary>
    ''' <param name="ID">ID of the Manual Transaction to get from the database.</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of dictonaries where each dictionary represents a row</returns>
    ''' <remarks></remarks>
    Public Shared Function getTransactionInfo(ID As Integer, user As String, WSID As String) As List(Of Dictionary(Of String, Object))
        Dim transInfo As New List(Of Dictionary(Of String, Object))
        Try
            transInfo = GetResultListMultiDataSetSingle("selXferOTLL", WSID, {{"@ID", ID, intVar}, {"@User", user, strVar}})
        Catch ex As Exception
            insertErrorMessages("ManualTransactionsHub", "getTransactionInfo", ex.Message, user, WSID)
        End Try
        Return transInfo
    End Function


    Public Shared Function getGenOrderTable(OrderNum As String, TransType As String, draw As Integer, sRow As Integer, eRow As Integer, column As Integer, sortDir As String, user As String, WSID As String) As TableObject
        Dim recordsTotal As Integer = 0, row As New List(Of String), table As New List(Of List(Of String)), datareader As SqlDataReader = Nothing
        Dim columns As New List(Of String) From {"ID", "Item Number", "Transaction Quantity", "Line Number", "Line Sequence", "Priority", "Required Date", "Lot Number",
                                   "Expiration Date", "Serial Number", "Warehouse", "Batch Pick ID", "Notes", "Tote Number", "Host Transaction ID", "Emergency",
                                   "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", "User Field8",
                                   "User Field9", "User Field10"}
        Try
            If column < 0 Or column > columns.Count Then
                column = 0
            End If

            datareader = RunSPArray("selXferOTOrderDT", WSID, {{"@OrderNum", OrderNum, strVar}, {"@TransType", TransType, strVar},
                                                               {"@sRow", sRow, intVar}, {"@eRow", eRow, intVar},
                                                               {"@column", columns(column), strVar}, {"@sortDir", sortDir, strVar}})

            If datareader.HasRows Then
                If datareader.Read() Then
                    If Not IsDBNull(datareader(0)) Then
                        recordsTotal = CInt(datareader(0))
                    End If
                End If
                datareader.NextResult()
                If datareader.HasRows Then
                    While datareader.Read()
                        For x As Integer = 0 To columns.Count - 1
                            row.Add(If(IsDBNull(datareader(columns(x))), "", datareader(columns(x))))
                        Next

                        table.Add(row)
                        row = New List(Of String)
                    End While
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Manual Transactions", "getGenOrderTable", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try

        Return New TableObject(draw, recordsTotal, recordsTotal, table)
    End Function

    Public Shared Function selManualOrderTypeahead(OrderNum As String, TransType As String, user As String, WSID As String) As List(Of Object)
        Dim datareader As SqlDataReader = Nothing, Orders As New List(Of Object)

        Try
            datareader = RunSPArray("selXferOTGenOrderTA", WSID, {{"@OrderNumber", OrderNum & "%", strVar}, {"@TransType", TransType, strVar}})

            If datareader.HasRows Then
                While datareader.Read()
                    Orders.Add(New With {.OrderNumber = datareader("Order Number")})
                End While
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("ManualTransactions", "selManualOrderTypeahead", ex.Message, user, WSID)
        Finally
            If Not IsNothing(datareader) Then datareader.Dispose()
        End Try

        Return Orders
    End Function

End Class

