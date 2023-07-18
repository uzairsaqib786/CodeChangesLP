' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class ItemNumber

    ''' <summary>
    ''' Gets Item Number, Description, Unit of Measure and Cell Size for typeahead
    ''' </summary>
    ''' <param name="item">User input item number to find suggestions for.</param>
    ''' <param name="user">The user that is currently logged on</param>
    ''' <param name="beginItem">The item number who will limit results to those after it</param>
    ''' <param name="WSID">The workstation that is currently being worked on</param>
    ''' <param name="isEqual">Optional parameter to show that the item will only show if it is the exact item number value</param>
    ''' <returns>A list that contains the information for each of the items that fufill the filters</returns>
    ''' <remarks></remarks>
    Public Shared Function getItem(item As String, user As String, beginItem As String, WSID As String, Optional isEqual As Boolean = False) As List(Of ItemNumberDropObj)
        Dim dropInfo As New List(Of String)
        Dim dropObj As New List(Of ItemNumberDropObj)
        Dim totalInfo As ItemNumberDropObj
        Dim DataReader As SqlDataReader = Nothing
        Try
            If beginItem <> "---" Then
                DataReader = RunSPArray("selInvEndItemTA", WSID, {{"@endItem", item & "%", strVar}, _
                                                                  {"@beginItem", beginItem, strVar}})
            ElseIf isEqual Then
                DataReader = RunSPArray("selInvItemNumTA", WSID, {{"@Num", item, strVar}})
            Else
                DataReader = RunSPArray("selInvItemNumTA", WSID, {{"@Num", item & "%", strVar}})
            End If


            If DataReader.HasRows Then
                While DataReader.Read()
                    dropInfo.Clear()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        dropInfo.Add(checkdbnull(DataReader(x)))
                    Next
                    totalInfo = New ItemNumberDropObj(ItemNumber:=dropInfo(0), Description:=dropInfo(1), UnitOfMeasure:=dropInfo(2), CellSize:=dropInfo(3), Whse:=dropInfo(4), dte:=dropInfo(5), vel:=dropInfo(6), max:=dropInfo(7), min:=dropInfo(8))
                    dropObj.Add(totalInfo)
                End While
            End If

        Catch ex As Exception
            Debug.WriteLine(ex)
            insertErrorMessages("Inventory Map", "getItemNum", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return dropObj
    End Function

    ''' <summary>
    ''' Gets specified item's description, max/min qty, date sensitive, unit of measure, cell size and velocity code fields from inventory
    ''' </summary>
    ''' <param name="item">The item number whose infomration is being retrieved</param>
    ''' <param name="user">The suer that is currenlty logged on</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>An object that contains the infomration about the desired item number</returns>
    ''' <remarks></remarks>
    Public Shared Function getItemInfo(item As String, user As String, WSID As String) As Object
        Dim DataReader As SqlDataReader = Nothing
        Dim description As String = "", maxqty As String = "0", minqty As String = "0", datesensitive As String = "", uom As String = "", velocity As String = "", cell As String = ""
        Dim supplierItemID As String = "", expDate As String = "", warehousesensitive As String = ""

        Try
            DataReader = RunSPArray("selItemInfo", WSID, {{"@Item", item, strVar}})

            If DataReader.HasRows Then
                If DataReader.Read() Then
                    description = IIf(IsDBNull(DataReader(0)), "", DataReader(0))
                    maxqty = IIf(IsDBNull(DataReader(1)), "0", DataReader(1))
                    minqty = IIf(IsDBNull(DataReader(2)), "0", DataReader(2))
                    datesensitive = IIf(IsDBNull(DataReader(3)), "", DataReader(3))
                    uom = IIf(IsDBNull(DataReader(4)), "", DataReader(4))
                    cell = IIf(IsDBNull(DataReader(5)), "", DataReader(5))
                    velocity = IIf(IsDBNull(DataReader(6)), "", DataReader(6))
                    supplierItemID = IIf(IsDBNull(DataReader(7)), "", DataReader(7))
                    expDate = IIf(IsDBNull(DataReader(8)), "", DataReader(8))
                    warehousesensitive = IIf(IsDBNull(DataReader(9)), "", DataReader(9))
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("ItemNumber", "getItemInfo", ex.Message, user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return New With {.description = description, .maxqty = maxqty, .minqty = minqty, .datesensitive = datesensitive, .uom = uom, _
                         .cell = cell, .velocity = velocity, .supplierItemID = supplierItemID, .expDate = expDate, .warehousesensitive = warehousesensitive}
    End Function

End Class
