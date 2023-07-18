' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module LocationAssignmentPutAways
        Private laLock As New Object()

        ''' <summary>
        ''' Assigns putaways and makes sure that no other threads are running
        ''' </summary>
        ''' <param name="wsid">The workstation that si being worked on</param>
        ''' <param name="username">The suer that is currently logged in</param>
        ''' <param name="orderNums">A list of order numbers to assign</param>
        ''' <param name="splitMasterRecordID">Tells if the master record id could be split</param>
        Public Sub AssignPutAwaysSync(ByVal wsid As String, ByVal username As String,
                                  Optional ByVal orderNums As List(Of String) = Nothing, Optional ByVal splitMasterRecordID As Integer = -1)
            SyncLock laLock
                AssignPutAways(wsid, username, orderNums, splitMasterRecordID)
                RunSP("Alloc_Custom_Put", wsid)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Assigns put away trasnactions
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="orderNums">A list of numbers to assign</param>
        ''' <param name="splitMasterRecordID">If the master record ids are able to be split</param>
        Private Sub AssignPutAways(ByVal wsid As String, ByVal username As String,
                                  Optional ByVal orderNums As List(Of String) = Nothing, Optional ByVal splitMasterRecordID As Integer = -1)
            Dim dateStamp = Now()

            Dim pref = GetResultMap("SelPreferencesLocationAssignment", wsid, {{"@WSID", wsid, strVar}})

            Dim orderNumsJoined = If(orderNums Is Nothing, "", String.Join(",", orderNums))
            QtyAllocReset(wsid, username)
            Dim otInvRowsToLocAssign = GetOTsToLocAssign(wsid, orderNumsJoined, splitMasterRecordID)
            'TODO: EventLogAlloc Count of result set {EventLogAlloc("LocAssPick", rst1.RecordCount, "Pick Location Assignment")}

            For Each otInvRow In otInvRowsToLocAssign

                Dim dynCheck As Boolean = False
                ' Send to reprocess if missing WH for WH sensitive transactions
                If otInvRow("Warehouse Sensitive") AndAlso IsNothingOrDBNull(otInvRow("Warehouse")) Then
                    ToReprocessQueue(wsid, otInvRow("ID"), dateStamp, username, "Warehouse sensitive and missing", otInvRow("Master Record ID"))
                    Continue For
                End If


                Dim locTypes = GetLocType(otInvRow("Primary Zone"), otInvRow("Secondary Zone"))
                Dim breakLoop = False
                For Each locType In locTypes
                    If locType IsNot Nothing Then
                        For vSort As Integer = 1 To 2
                            Dim assigned As Boolean = assignByLocType(wsid, username, locType, vSort, dynCheck, otInvRow, dateStamp)
                            'if assigned, do not need to check other loctypes of velocity codes.
                            If assigned Then
                                breakLoop = True
                                Exit For
                            End If
                        Next
                    End If
                    'if assigned, this transaction is finished
                    If breakLoop Then Exit For
                Next
                If breakLoop Then Continue For
                'if transaction was not assigned, reprocess the transaction
                ToReprocessQueue(wsid, otInvRow("ID"), dateStamp, username, "There are no open locations available for Put Away.", otInvRow("Master Record ID"))
            Next
            Debug.Print("Location Assignment Time:" & DateDiff(DateInterval.Second, dateStamp, Now()))
        End Sub

        ''' <summary>
        ''' Assigns locations by the location type (carousel, bulk, carton flow)
        ''' </summary>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="locType">The location type to assign</param>
        ''' <param name="vSort">If the results should be sorted</param>
        ''' <param name="dynCheck">If dynamic warehouses are included</param>
        ''' <param name="otInvRow">A dictionary that contains the data for transaction</param>
        ''' <param name="dateStamp">The date</param>
        ''' <returns>A boolean telling if it was assigned</returns>
        Private Function assignByLocType(ByVal wsid As String, ByVal username As String, ByVal locType As String,
                                         ByVal vSort As Integer, ByVal dynCheck As Boolean, ByVal otInvRow As Dictionary(Of String, Object), ByVal dateStamp As Date) As Boolean

            Dim velCode, cellSize As String

            If locType.ToLower = "carousel" Then
                velCode = otInvRow("CarouselVelocity Code")
                cellSize = otInvRow("CarouselCell Size")
            ElseIf locType.ToLower = "bulk" Then
                velCode = otInvRow("BulkVelocity Code")
                cellSize = otInvRow("BulkCell Size")
            Else
                velCode = otInvRow("CFVelocity Code")
                cellSize = otInvRow("CFCell Size")
            End If

            Dim invMapLocations = GetInvMapLocations(wsid, otInvRow, velCode, cellSize, vSort, locType, dynCheck)

            For Each invMapRow In invMapLocations
                Dim availQty = GetAvailQty(locType, otInvRow, invMapRow)
                Dim passedWHCheck As Boolean = WHCheck(wsid, invMapRow("Zone"), invMapRow("Warehouse"), otInvRow, dynCheck)
                If passedWHCheck Then
                    Dim assigned As Boolean = AssignLocation(wsid, username, availQty, otInvRow("Transaction Quantity"), locType, otInvRow, invMapRow)
                    If assigned Then
                        Return True
                    End If
                End If
            Next
            Return False

        End Function

        ''' <summary>
        ''' Return the location types in order of Primary, Secondary, and then whatever's left. 
        ''' </summary>
        ''' <param name="primaryZone">The primary zone to check</param>
        ''' <param name="secondaryZone">The secondary zone to check</param>
        ''' <returns>location types in the order to be used</returns>
        ''' <remarks></remarks>
        Private Function GetLocType(ByVal primaryZone As String, ByVal secondaryZone As String) As String()

            Dim ret As String = ""

            If IsNothingOrDBNull(primaryZone) Then
                primaryZone = "Carousel"
            Else
                If primaryZone = "Carousel" Then
                    If secondaryZone = "Carousel" Then ret = "Bulk"
                    If secondaryZone = "Bulk" Then ret = "Carton Flow"
                    If secondaryZone = "Carton Flow" Then ret = "Bulk"
                ElseIf primaryZone = "Bulk" Then
                    If secondaryZone = "Carousel" Then ret = "Carton Flow"
                    If secondaryZone = "Carton Flow" Then ret = "Carousel"
                    If secondaryZone = "Bulk" Then ret = "Carousel"
                ElseIf primaryZone = "Carton Flow" Then
                    If secondaryZone = "Bulk" Then ret = "Carousel"
                    If secondaryZone = "Carousel" Then ret = "Bulk"
                    If secondaryZone = "Carton Flow" Then ret = "Bulk"
                End If
            End If

            Return {primaryZone, If(secondaryZone = primaryZone, Nothing, secondaryZone), ret}
        End Function

        ''' <summary>
        ''' Gets the available quantity depending on the location type
        ''' </summary>
        ''' <param name="locType">The type of lcoation</param>
        ''' <param name="otInvRow">The open transaction record data</param>
        ''' <param name="invMapRow">The inventory map record data</param>
        ''' <returns>An integer that tells the avaialbel quantity</returns>
        Private Function GetAvailQty(ByVal locType As String, ByVal otInvRow As Dictionary(Of String, Object), ByVal invMapRow As Dictionary(Of String, Object)) As Integer
            Dim availQty = 999999
            If Not IsNothingOrDBNull(invMapRow("Available Qty")) Then
                availQty = invMapRow("Available Qty")
            End If

            'BulkMaxQty = rstOT![Bulk Max Qty]
            'CarMaxQty = rstOT![Maximum Quantity]
            'CFMaxQty = rstOT![CF Max Qty]

            If availQty = 999999 Then
                If locType = "Carousel" Then
                    If otInvRow("Maximum Quantity") <> 0 Then
                        availQty = otInvRow("Maximum Quantity")
                    End If
                ElseIf locType = "Bulk" Then
                    If otInvRow("Bulk Max Qty") <> 0 Then
                        availQty = otInvRow("Bulk Max Qty")
                    End If
                Else
                    If otInvRow("CF Max Qty") <> 0 Then
                        availQty = otInvRow("CF Max Qty")
                    End If
                End If
            End If

            Return availQty

        End Function

        ''' <summary>
        ''' Returns if location passed dynamic warehouse check
        ''' </summary>
        ''' <param name="wsid">The workstationt hat is currently being worked on</param>
        ''' <param name="zone"></param>
        ''' <param name="invMapWH">The inventory map wahrehouse</param>
        ''' <param name="otInvRow">The open transaction record data</param>
        ''' <param name="dynCheck">if dynamic should be checked</param>
        ''' <returns>0 if location is valid to assign, 1 if dynCheck needs to toggle, -1 for reprocess</returns>
        ''' <remarks></remarks>
        Private Function WHCheck(ByVal wsid As String, ByVal zone As String, ByVal invMapWH As String, ByVal otInvRow As Dictionary(Of String, Object), ByVal dynCheck As Boolean) As Boolean
            If Not GetLocZoneDWByZone(wsid, zone) AndAlso invMapWH <> otInvRow("Warehouse") Then
                Return False
            Else
                Return True
            End If
        End Function

        ''' <summary>
        ''' Assigns a location
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="username">The user that si currently logged in</param>
        ''' <param name="availQty">The available quantity</param>
        ''' <param name="transQty">The transaction quantity</param>
        ''' <param name="locType">The type of lcoation</param>
        ''' <param name="otInvRow">The open transaction record data</param>
        ''' <param name="invMapRow">The inventory map record data</param>
        ''' <returns>A boolean telling if the location was assigned</returns>
        Private Function AssignLocation(ByVal wsid As String, ByVal username As String, ByVal availQty As Integer, ByVal transQty As Integer, ByVal locType As String,
                                   ByVal otInvRow As Dictionary(Of String, Object), ByVal invMapRow As Dictionary(Of String, Object)) As Boolean

            Dim maxQty, minQty As Integer

            If locType = "Carousel" Then
                If otInvRow("Maximum Quantity") > 0 Then
                    maxQty = otInvRow("Maximum Quantity")
                    minQty = otInvRow("Min Quantity")
                End If
            ElseIf locType = "Bulk" Then
                If otInvRow("Bulk Max Qty") > 0 Then
                    maxQty = otInvRow("Bulk Max Qty")
                    minQty = otInvRow("Bulk Min Qty")
                End If
            Else
                If otInvRow("CF Max Qty") > 0 Then
                    maxQty = otInvRow("CF Max Qty")
                    minQty = otInvRow("CF Min Qty")
                End If
            End If

            If availQty >= transQty Then
                If invMapRow("Maximum Quantity") = 0 Then
                    UpdateInvMapQty(wsid, maxQty, minQty, invMapRow("Inv Map ID"))
                End If
                If IsNothingOrDBNull(invMapRow("Item Number")) OrElse invMapRow("Item Number") = "" Then
                    UpdateItemInfo(wsid, otInvRow, invMapRow("Inv Map ID"))
                End If

                UpdOTLocation(wsid, otInvRow("ID"), invMapRow("Inv Map ID"))
                Return True

            ElseIf availQty > 0 Then
                If invMapRow("Maximum Quantity") = 0 Then
                    minQty = invMapRow("Min Quantity")
                    UpdateInvMapQty(wsid, maxQty, minQty, invMapRow("Inv Map ID"))
                End If
                If IsNothingOrDBNull(invMapRow("Item Number")) OrElse invMapRow("Item Number") = "" Then
                    UpdateItemInfo(wsid, otInvRow, invMapRow("Inv Map ID"))
                End If

                UpdOTLocation(wsid, otInvRow("ID"), invMapRow("Inv Map ID"), availQty)

                Dim splitQty = transQty - availQty
                InsSplitOT(wsid, otInvRow("ID"), splitQty)
                AssignPutAways(wsid, username, splitMasterRecordID:=otInvRow("Master Record ID"))
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Inserts a split a open trasnaction record
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="otid">The open transaction record id to split</param>
        ''' <param name="transQty">The quantity for the new transaction record</param>
        Private Sub InsSplitOT(ByVal wsid As String, ByVal otid As Integer, ByVal transQty As Integer)
            RunSP("InsOTLASplitPutAway", wsid, {{"@OTID", otid, intVar}, {"@TransQty", transQty, intVar}})
        End Sub

        ''' <summary>
        ''' Updates the open transaction record
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="otid">The open transaction record to update</param>
        ''' <param name="invMapID">The inventory map id to get data from</param>
        ''' <param name="transQty">The new trasaction quantity</param>
        Private Sub UpdOTLocation(ByVal wsid As String, ByVal otid As Integer, ByVal invMapID As Integer, Optional ByVal transQty As Integer = -1)
            RunSP("UpdOTLocation", wsid, {{"@ID", otid, intVar}, {"@InvMapID", invMapID, intVar}, {"@TransQty", transQty, If(transQty = -1, nullVar, intVar)}})
        End Sub

        ''' <summary>
        ''' Gets the open transaction records to assign
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="orderNumsJoined">String with comma seperated order numbers</param>
        ''' <param name="splitMasterRecordID">If the master eecord id could be split</param>
        ''' <returns></returns>
        Private Function GetOTsToLocAssign(ByVal wsid As String, ByVal orderNumsJoined As String, ByVal splitMasterRecordID As Integer)
            Dim ret
            If splitMasterRecordID = -1 Then
                ret = GetResultMapList("SelOTLocationAssignmentPutAways", wsid, {{"@OrderNums", orderNumsJoined, strVar}})
            Else
                ret = GetResultMapList("SelOTLocationAssignmentPutAways", wsid, {{"@OrderNums", orderNumsJoined, strVar}, {"@MasterRecordID", splitMasterRecordID, intVar}})
            End If
            Return ret
        End Function

        ''' <summary>
        ''' Updates the desired inventory map records max and min quantities
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="maxQty">The new max quantity value</param>
        ''' <param name="minQty">The new minimum quantity value</param>
        ''' <param name="invMapId">The id of the inventory map record to update</param>
        Private Sub UpdateInvMapQty(ByVal wsid As String, ByVal maxQty As Integer, ByVal minQty As Integer, ByVal invMapId As Integer)
            RunSP("HPAUpdateMaxQty", wsid, {{"@MaxQty", maxQty, intVar}, {"@MinQty", minQty, intVar}, {"@InvMapID", invMapId, intVar}})
        End Sub

        ''' <summary>
        ''' Updates the desired inventory map record
        ''' </summary>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <param name="otInvRow">The new data values</param>
        ''' <param name="invMapId">The inventory map id to udpate</param>
        Private Sub UpdateItemInfo(ByVal wsid As String, ByVal otInvRow As Dictionary(Of String, Object), invMapId As Integer)
            RunSP("HPAUpdateItemInfo", wsid, {
                  {"@InvMapID", invMapId, intVar},
                  {"@Whse", otInvRow("Warehouse"), strVar},
                  {"@Item", otInvRow("Item Number"), strVar},
                  {"@Lot", otInvRow("Lot Number"), strVar},
                  {"@Ser", otInvRow("Serial Number"), strVar},
                  {"@Descr", otInvRow("Description"), strVar},
                  {"@UM", otInvRow("Unit of Measure"), strVar},
                  {"@Exp", otInvRow("Expiration Date"), dteVar}})
        End Sub

        ''' <summary>
        ''' Gets inventory map locations
        ''' </summary>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <param name="otInvRow">The row data</param>
        ''' <param name="velCode">The desired velocity code</param>
        ''' <param name="cellSize">The desired cell size</param>
        ''' <param name="vSortBy">Determines the type of sorting</param>
        ''' <param name="locType">The desired location type</param>
        ''' <param name="dynCheck">If dynamic warehousing should be used</param>
        ''' <returns>A dictionary that contains the inventory map locations and their data</returns>
        Private Function GetInvMapLocations(ByVal wsid As String, ByVal otInvRow As Dictionary(Of String, Object),
                                            ByVal velCode As String, ByVal cellSize As String, ByVal vSortBy As Integer,
                                            ByVal locType As String, ByVal dynCheck As Boolean) As List(Of Dictionary(Of String, Object))

            Dim whType = If(otInvRow("Warehouse") Is Nothing, nullVar, strVar)
            Dim spParams As String(,) = {{"@DateSens", otInvRow("Date Sensitive"), boolVar},
                        {"@Whse", otInvRow("Warehouse"), whType},
                        {"@Velcode", velCode, strVar},
                        {"@CellSz", cellSize, strVar},
                        {"@FIFO", If(otInvRow("Revision") = "Return", False, otInvRow("FIFO")), boolVar},
                        {"@Item", otInvRow("Item Number") & otInvRow("Lot Number") & otInvRow("Serial Number"), strVar},
                        {"@VSortBy", vSortBy, intVar},
                        {"@DynWhse", If(dynCheck, "No", "Yes"), strVar},
                        {"@LocType", locType, strVar},
                        {"@InvMapID", 0, intVar},
                        {"@WPZ", "0", strVar},
                        {"@Reel", 0, boolVar},
                        {"@Replenish", 0, boolVar},
                        {"@ExpDate", otInvRow("Expiration Date"), dteVar}}
            Dim time = Now()
            Dim ret = GetResultMapList("LocAssPutAway", wsid, spParams)
            Debug.Print("LocAssPutAway Time:" & DateDiff(DateInterval.Second, time, Now()))
            Return ret
        End Function

        ''' <summary>
        ''' Returns whether a location zone is enabled for dynamic warehousing
        ''' </summary>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <param name="zone">The zone to check</param>
        ''' <returns>A boolean telling if the location is enabled for dynamic warehousing</returns>
        ''' <remarks></remarks>
        Private Function GetLocZoneDWByZone(ByVal wsid As String, ByVal zone As String) As Boolean
            Dim ret = GetResultSingleCol("SelLocationZonesDynWH", wsid, {{"@Zone", zone, strVar}})
            If IsNothingOrDBNull(ret) Then ret = False
            Return ret
        End Function

    End Module
End Namespace