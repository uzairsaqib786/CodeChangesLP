' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Admin.Controllers
    <Authorize()>
    Public Class MoveItemsController
        Inherits ICSAdminController

        ' GET: MoveItems
        Function Index() As ActionResult
            Return View(MoveItemsColumnSequence)
        End Function

        ''' <summary>
        ''' Gets the move items datatable
        ''' </summary>
        ''' <param name="data">The data needed to get the records for the datatable</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        Function MoveItemsTable(data As TableObjectSent)
            If data.searchString = Nothing Then
                data.searchString = ""
            End If
            Return Json(getMoveItemsTable(data.draw, data.start + 1, data.length + data.start, _
                                                     data.searchString, "Item Number", Request.QueryString.Get("order[0][column]"), _
                                                          Request.QueryString.Get("order[0][dir]"), User.Identity.Name, data.nameStamp, data.Cellsize, data.Warehouse, data.InvMapID, data.OQA, data.filter, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the records for the datatable depending on the given filters
        ''' </summary>
        ''' <param name="draw">Parameter to make sure the datatables are drawn in sequence</param>
        ''' <param name="sRow">The row from which the records will start</param>
        ''' <param name="eRow">The row from which the records will end</param>
        ''' <param name="searchString">The string that will be searched for</param>
        ''' <param name="searchColumn">The column that contains the values that will be comapred ot the search string</param>
        ''' <param name="sortColumnIndex">The column number which is being sorted on</param>
        ''' <param name="sortOrder">The sort direction</param>
        ''' <param name="user">The user that is currently loggged in</param>
        ''' <param name="tableName">The table where to grab the records from</param>
        ''' <param name="Cellsize">The desired cell size</param>
        ''' <param name="Warehouse">The desired warehouse</param>
        ''' <param name="InvMapid">The desired inventory map id</param>
        ''' <param name="ViewMode">Which type of mmove transactions are being displayed</param>
        ''' <param name="filter">The filter that is being applied ot the table</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        Function getMoveItemsTable(draw As Integer, sRow As Integer, eRow As Integer, searchString As String, searchColumn As String, sortColumnIndex As Integer, sortOrder As String, user As String, tableName As String, Cellsize As String, Warehouse As String, InvMapid As Integer, ViewMode As String, filter As String, WSID As String) As TableObject
            Dim DataReader As SqlDataReader = Nothing
            Dim datalist As New List(Of List(Of String))
            Dim innerlist As New List(Of String)
            Dim recordsTotal As Integer
            Dim recordsFiltered As Integer
            Dim sortColumn As String = MoveItemsSortColumnByIndex(sortColumnIndex)
            Dim columnSeq As List(Of String) = MoveItemsColumnSequence()

            searchString = GlobalFunctions.cleanSearch(searchString)
            columnSeq(columnSeq.IndexOf("Velocity Code")) = "Golden Zone"
            filter = filter.Replace("[Quantity Allocated Pick]", "[vAllocatedQuantity].[Quantity Allocated Pick]").Replace("[Quantity Allocated Put Away]", "[vAllocatedQuantity].[Quantity Allocated Put Away]")
            filter = filter.Replace("[Location Number]", "(isNull([Zone], '') + isNull([Carousel], '') + isNull([Row], '') + isNull([Shelf], '') + isNull([Bin], ''))").Replace("[", "[Inventory Map].[").Replace("Alternate Light", "Location ID").Replace("Velocity Code", "Golden Zone").Replace("Laser X", "Quantity Allocated Pick").Replace("Laser Y", "Quantity Allocated Put Away")
            Try
                ' OQA is Open, Quarantine, All
                DataReader = RunSPArray("selInvMapMoveItemsDT", WSID, {{"@Table", tableName, strVar}, _
                                                            {"@Warehouse", Warehouse, strVar}, _
                                                            {"@CellSize", Cellsize, strVar}, _
                                                            {"@ViewMode", ViewMode, strVar}, _
                                                            {"@InvMapID", InvMapid, intVar}, _
                                                            {"@searchString", searchString, strVar}, _
                                                            {"@searchColumn", searchColumn, strVar}, _
                                                            {"@sRow", sRow, intVar}, _
                                                            {"@eRow", eRow, intVar}, _
                                                            {"@sortColumn", sortColumn, strVar}, _
                                                            {"@sortOrder", sortOrder, strVar}, _
                                                            {"@filter", filter, strVar}})
                Dim index = 0
                While DataReader.HasRows

                    While DataReader.Read()
                        ' recordsTotal/Filtered are for the datatable plugin
                        If index = 0 Then
                            recordsTotal = DataReader(0)
                        ElseIf index = 1 Then
                            recordsFiltered = DataReader(0)
                        Else
                            'If Move From Table, only shows locations that have Items and Qty  in them
                            'If tableName = "MoveFrom" AndAlso (IsDBNull(DataReader("Item Number")) OrElse DataReader("Item Number") = "") AndAlso (Not IsDBNull(DataReader("Item Number")) OrElse DataReader("Item Quantity") > 0) Then
                            'Continue While
                            'End If
                            'If Move to Table, only shows locations that have Items 
                            'If tableName = "MoveTo" AndAlso (searchString <> "" AndAlso Not IsDBNull(DataReader("Item Number")) AndAlso searchString.ToLower = DataReader("Item Number").ToString.ToLower) Then

                            'End If
                            ' reorder the indicies to match column sequence of the user
                            For Each column In columnSeq
                                If IsDBNull(DataReader.Item(column)) Then
                                    innerlist.Add("")
                                Else
                                    innerlist.Add(DataReader.Item(column))
                                End If
                            Next
                            datalist.Add(innerlist)
                            innerlist = New List(Of String)
                        End If
                        index += 1
                    End While
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Inventory Map", "selInventoryMap", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New TableObject(draw, recordsTotal, recordsFiltered, datalist)
        End Function

        ''' <summary>
        ''' Gives the column sequence for the move transactions
        ''' </summary>
        ''' <returns>A list of string that contains the move transactions</returns>
        Private Function MoveItemsColumnSequence() As List(Of String)
            Return New List(Of String) From {"Warehouse", "Location Number", "Velocity Code", "Item Number", "Description", "Item Quantity", "Quantity Allocated Pick", "Quantity Allocated Put Away", _
                                             "Zone", "Carousel", "Row", "Shelf", "Bin", "Cell Size", "Lot Number", "Serial Number", "Expiration Date", "Revision", "Unit of Measure", "Maximum Quantity", "Put Away Date", _
                                             "User Field1", "User Field2", "Master Location", "Date Sensitive", "Dedicated", "Master Inv Map ID", "Min Quantity", "Inv Map ID"}
        End Function

        ''' <summary>
        ''' Gives the index number of the given column
        ''' </summary>
        ''' <param name="index">The column value whose index is to be determined</param>
        ''' <returns>The index position for the given column</returns>
        Private Function MoveItemsSortColumnByIndex(index As String) As String
            Dim ColumnSequence = MoveItemsColumnSequence()
            Select Case ColumnSequence(index)
                Case "Velocity code"
                    Return "Golden Zone"
                Case Else
                    Return ColumnSequence(index)
            End Select
        End Function

        ''' <summary>
        ''' Creates a move transaction
        ''' </summary>
        ''' <param name="MoveFromID">The id which is being moved from</param>
        ''' <param name="MoveToID">The id which is the new location</param>
        ''' <param name="MoveFromItemNumber">The item number which is being moved from</param>
        ''' <param name="MoveToItemNumber">The recieveing item number</param>
        ''' <param name="MoveToZone">The zone where the items will be moved to</param>
        ''' <param name="MoveQty">The amount that is being moved</param>
        ''' <param name="ReqDate">The required date for the items</param>
        ''' <param name="Priority">The priority of the item that is being moved</param>
        ''' <param name="dedicateMoveTo">If the move to is a dedicated one</param>
        ''' <param name="unDedicateMoveFrom">If the move from is being undedicated</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        <HttpPost()>
        Public Function CreateMoveTransactions(ByVal MoveFromID As Integer, ByVal MoveToID As Integer, ByVal MoveFromItemNumber As String, ByVal MoveToItemNumber As String, _
                                                  ByVal MoveToZone As String, ByVal MoveQty As Integer, ByVal ReqDate As DateTime, ByVal Priority As Integer, _
                                                  ByVal dedicateMoveTo As Boolean, ByVal unDedicateMoveFrom As Boolean)
            Return Json(MoveItems.CreateMoveTransactions(MoveFromID, MoveToID, MoveFromItemNumber, MoveToItemNumber, MoveToZone, MoveQty, ReqDate, Priority, dedicateMoveTo, unDedicateMoveFrom, User.Identity.Name, Session("WSID")))
        End Function
    End Class
End Namespace