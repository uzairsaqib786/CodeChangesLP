' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web
Namespace Induction
    Public Class ToteTransactionsManager

        ''' <summary>
        ''' Selects the data for the tote transaction manager datatable
        ''' </summary>
        ''' <param name="draw">tells if table is being drawn</param>
        ''' <param name="BatchID"> the batch id to filter out bacthes</param>
        ''' <param name="startRow">the start row</param>
        ''' <param name="endRow">the end row</param>
        ''' <param name="sortCol">the column index that is being sorted on</param>
        ''' <param name="sortOrder">the order that column is being sorted on</param>
        ''' <param name="filter">any filters that are applied to this table</param>
        ''' <param name="User">user that is logged in</param>
        ''' <param name="WSID">workstatio id</param>
        ''' <returns>Object containing all the ctable components</returns>
        ''' <remarks></remarks>
        Public Shared Function selectToteTransManTable(draw As Integer, BatchID As String, startRow As Integer, endRow As Integer, sortCol As Integer,
                                                       sortOrder As String, filter As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"Order Number", "Batch Pick ID", "Tote Number", "Tote ID", "Zone Label", "Transaction Type", "Host Transaction ID"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim Data As New List(Of List(Of String))
            Dim RecordCount As Integer = 0
            Dim FilterCount As Integer = 0

            Try
                If BatchID Is Nothing Then
                    BatchID = ""
                End If

                DataReader = RunSPArray("selOTToteTransManDT", WSID, {{"@BatchID", BatchID, strVar}, {"@sRow", startRow, intVar},
                                                                      {"@eRow", endRow, intVar}, {"@sortColumn", column, strVar},
                                                                      {"@sortOrder", sortOrder, strVar}, {"@filter", filter, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        FilterCount = DataReader("FilterCount")
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        Data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ToteTransactionsManager", "selectToteTransManTable", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, RecordCount, FilterCount, Data)
        End Function

        ''' <summary>
        ''' Selects datata for batch pick id typeahead
        ''' </summary>
        ''' <param name="BatchPickID">the vlaue entered wihtin the typeaehad already</param>
        ''' <param name="User">user currently logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>list of object containg all the batch pick ids</returns>
        ''' <remarks></remarks>
        Public Shared Function selectBatchPickTA(BatchPickID As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim BatchIDs As New List(Of Object)
            Dim Nulls As Boolean = False

            Try
                DataReader = RunSPArray("selOTBatchPickIDTA", WSID, {{"@BatchID", BatchPickID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        If DataReader("NullCount") > 0 Then
                            Nulls = True
                        End If
                        BatchIDs.Add(New With {.BatchID = DataReader("Batch Pick ID")})
                    End While
                End If

                If Nulls Then
                    BatchIDs.Add(New With {.BatchID = "No Batch ID"})
                End If
            Catch ex As Exception
                insertErrorMessages("ToteTransactionsManager", "selectBatchPickTA", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return BatchIDs
        End Function


    End Class
End Namespace
