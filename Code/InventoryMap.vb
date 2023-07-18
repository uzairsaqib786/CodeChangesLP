' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class InventoryMap
    ''' <summary>
    ''' Draws the tbale for inventory map
    ''' </summary>
    ''' <param name="draw">parmater whioch cannot change. for the datatables plugin</param>
    ''' <param name="sRow">The start row</param>
    ''' <param name="eRow">The end row</param>
    ''' <param name="searchString">The search string</param>
    ''' <param name="searchColumn">The column for which the search string is going to be searched on</param>
    ''' <param name="sortColumnIndex">The sort column</param>
    ''' <param name="sortOrder">The sort order fpr the sort colum. ascending or descending</param>
    ''' <param name="user">The current user</param>
    ''' <param name="OQA">Which ones ot view Opened, quarantined, or all.</param>
    ''' <returns>A table object for inventory map</returns>
    ''' <remarks></remarks>
    Public Shared Function selInventoryMap(draw As Integer, sRow As Integer, eRow As Integer, searchString As String, searchColumn As String, sortColumnIndex As Integer, sortOrder As String,
                                           filter As String, user As String, OQA As String, WSID As String) As TableObject
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))

        Dim columnSeq As List(Of String) = GlobalFunctions.getDefaultColumnSequence("Inventory Map", user, WSID)
        Dim sortColumn As String = columnSeq(sortColumnIndex)
        Select Case (sortColumn)
            Case "Velocity Code"
                sortColumn = "Golden Zone"
            Case "Alternate Light"
                sortColumn = "Location ID"
        End Select
        ' prevent potential injection
        searchString = GlobalFunctions.cleanSearch(searchString)

        ' renamed or aliased columns need to be searched properly
        Select Case searchColumn
            Case "Alternate Light"
                searchColumn = "Location ID"
            Case "Velocity Code"
                searchColumn = "Golden Zone"
        End Select
        Try
            filter = filter.Replace("[Location Number]", "(isNull([Zone], '') + isNull([Carousel], '') + isNull([Row], '') + isNull([Shelf], '') + isNull([Bin], ''))").Replace("[", "[Inventory Map].[").Replace("Alternate Light", "Location ID").Replace("Velocity Code", "Golden Zone")
            filter = filter.Replace("[Inventory Map].[Quantity Allocated Pick]", "[vAllocatedQuantity].[Quantity Allocated Pick]").Replace("[Inventory Map].[Quantity Allocated Put Away]", "[vAllocatedQuantity].[Quantity Allocated Put Away]")
            filter = filter.Replace("Laser X", "Quantity Allocated Pick").Replace("Laser Y", "Quantity Allocated Put Away")

            If columnSeq.IndexOf("Velocity Code") <> -1 Then columnSeq(columnSeq.IndexOf("Velocity Code")) = "Golden Zone"
            If columnSeq.IndexOf("Alternate Light") <> -1 Then columnSeq(columnSeq.IndexOf("Alternate Light")) = "Location ID"
            table = GetJQueryDataTableResult(draw, "selInvMapDT", WSID, user, {{"@OQA", OQA, strVar}, _
                                                        {"@searchString", searchString, strVar}, _
                                                        {"@searchColumn", searchColumn, strVar}, _
                                                        {"@sRow", sRow, intVar}, _
                                                        {"@eRow", eRow, intVar}, _
                                                        {"@sortColumn", sortColumn, strVar}, _
                                                        {"@sortOrder", sortOrder, strVar}, _
                                                        {"@filter", filter, strVar}}, columnOrder:=columnSeq)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("Inventory Map", "selInventoryMap", ex.Message, user, WSID)
        End Try

        Return table
    End Function

    ''' <summary>
    ''' Gets the location/zone typeaheads
    ''' </summary>
    ''' <param name="location">The location to look up by</param>
    ''' <param name="zone">The zone to look up by</param>
    ''' <param name="user">The suer that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of object containg location and zone combinations that begin with the given filters</returns>
    ''' <remarks></remarks>
    Public Shared Function getLocationZoneTypeahead(location As String, zone As String, user As String, WSID As String) As List(Of Object)
        Dim datareader As SqlDataReader = Nothing
        Dim typeahead As New List(Of Object)
        Try
            datareader = RunSPArray("selLocationZoneTypeahead", WSID, {{"@Location", location & "%", strVar}, {"@Zone", zone & "%", strVar}})

            If datareader.HasRows Then
                While datareader.Read()
                    typeahead.Add(New With {.Location = IIf(IsDBNull(datareader.Item("Location Name")), "", datareader.Item("Location Name")), _
                                           .Zone = IIf(IsDBNull(datareader.Item("Zone")), "", datareader.Item("Zone"))})
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("InventoryMap", "getLocationZoneTypeahead", ex.Message, user, WSID)
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
            End If
        End Try
        Return typeahead
    End Function

    ''' <summary>
    ''' Gets the type ahead information for inventory map. 
    ''' </summary>
    ''' <param name="columnName">The column which currenlty seletced</param>
    ''' <param name="value">The value that is being inputted inot the column</param>
    ''' <returns>A list of results from that column that start with the input value.</returns>
    ''' <remarks></remarks>
    Public Shared Function getTypeaheadInvMap(columnName As String, value As String, user As String, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim typeahead As New List(Of String)
        Try
            ' rename the columns that are aliases in the db
            If columnName = "Alternate Light" Then
                columnName = "Location ID"
            ElseIf columnName = "Velocity Code" Then
                columnName = "Golden Zone"
            ElseIf columnName = "Laser X" Then
                columnName = "Quantity Allocated Pick"
            ElseIf columnName = "Laser Y" Then
                columnName = "Quantity Allocated Put Away"
            End If

            DataReader = RunSPArray("selInvMapTA", WSID, {{"@columnName", columnName, strVar}, _
                                                       {"@value", value, strVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        typeahead.Add(CheckDBNull(DataReader(x)))
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Inventory Map", "getTypeaheadInvMap", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        Return typeahead
    End Function

End Class

