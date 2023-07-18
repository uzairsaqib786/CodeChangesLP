' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class CellSize

    ''' <summary>
    ''' Gets all cell size and cell type (descriptions) from the db
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A object with .cellsize and .celltype</returns>
    ''' <remarks></remarks>
    Public Shared Function getCellSizes(user As String, WSID As String) As CellSizeModel
        Dim DataReader As SqlDataReader = Nothing, cells As New List(Of String), ctypes As New List(Of String)

        Try
            DataReader = RunSPArray("selCellSize", WSID, {{"nothing"}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    cells.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                    ctypes.Add(IIf(IsDBNull(DataReader(1)), "", DataReader(1)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CellSize", "getCellSizes", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return New CellSizeModel(cells, ctypes)
    End Function

    ''' <summary>
    ''' Deletes specified cell size and logs the event in the Event Log
    ''' </summary>
    ''' <param name="cell">Cell Size to delete.</param>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <remarks></remarks>
    Public Shared Sub deleteCell(cell As String, user As String, WSID As String)
        Try
            RunActionSP("delCellSize", WSID, {{"@Size", cell, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CellSize", "deleteCell", ex.Message, user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Saves a cell size and type
    ''' </summary>
    ''' <param name="oldcell">"" if not an edit otherwise the previous value of cell size</param>
    ''' <param name="newcell">Cell Size value</param>
    ''' <param name="celltype">Cell Type/Description</param>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <remarks></remarks>
    Public Shared Sub saveCell(oldcell As String, newcell As String, celltype As String, user As String, WSID As String)
        Try
            RunActionSP("saveCell", WSID, {{"@OldCell", oldcell, strVar}, {"@NewCell", newcell, strVar}, {"@CellType", celltype, strVar}, {"@User", user, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("CellSize", "saveCell", ex.Message, user, WSID)
        End Try
    End Sub

End Class
