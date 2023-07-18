' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class Reprocessed

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
    Public Shared Function getReprocessedTable(draw As Integer, sRow As String, eRow As String, sortColumnNum As Integer, sortOrder As String,
                                             searchColumn As String, searchString As String, user As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim columnSeq As List(Of String) = GlobalFunctions.getDefaultColumnSequence("ReProcessed", user, WSID)
        Dim sortColumn As String = columnSeq(sortColumnNum)
        Try
            table = GetJQueryDataTableResult(draw, "selReprocessedDT", WSID, user, {{"@sRow", sRow, strVar},
                                                                    {"@eRow", eRow, strVar},
                                                                    {"@searchColumn", searchColumn, strVar},
                                                                    {"@searchString", searchString, strVar},
                                                                    {"@sortColumn", sortColumn, strVar},
                                                                    {"@sortOrder", sortOrder, strVar}}, columnOrder:=columnSeq)

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Reprocessed", "getReprocessedTable", ex.ToString(), user, WSID)
        End Try
        Return table
    End Function

End Class
