' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web
Namespace Induction
    Public Class ToteTransView
        ''' <summary>
        ''' Selects the datatable information for the tote transactions view page
        ''' </summary>
        ''' <param name="draw">tells if the table is being drawn</param>
        ''' <param name="ToteNum">The tote number of the selected tote</param>
        ''' <param name="BatchID">The batch that the tote is contained in</param>
        ''' <param name="startRow">the start row</param>
        ''' <param name="endRow">the end row</param>
        ''' <param name="sortCol">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The order that the sort column is being sorted on</param>
        ''' <param name="User">user currently logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>Object containging all the data table info</returns>
        ''' <remarks></remarks>
        Public Shared Function selectToteTransViewTable(draw As Integer, ToteNum As Integer, BatchID As String, startRow As Integer, endRow As Integer, _
                                                        sortCol As Integer, sortOrder As String, User As String, WSID As String) As TableObject
            Dim columnSequence As New List(Of String) From {"ID", "Cell", "Item Number", "Transaction Quantity", "Item Location", "Host Transaction ID"}
            Dim column As String = columnSequence(sortCol)
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim Data As New List(Of List(Of String))
            Try
                DataReader = RunSPArray("selOTToteTransViewDT", WSID, {{"@ToteNum", ToteNum, intVar}, {"@BatchID", BatchID, strVar}, _
                                                                       {"@sRow", startRow, intVar}, {"@eRow", endRow, intVar}, _
                                                                       {"@sortColumn", column, strVar}, {"@sortOrder", sortOrder, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        Data.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ToteTransView", "selectToteTransViewTable", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, Data.Count, Data.Count, Data)
        End Function
    End Class
End Namespace
