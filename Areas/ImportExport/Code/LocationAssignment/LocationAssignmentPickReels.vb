' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module LocationAssignmentPickReels

        ''' <summary>
        ''' Assigns pick rele transactions
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">Id of the transaction to assign</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="warehouseOT">The desired warehouse value</param>
        ''' <param name="username">The user that is curently logged in</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        Public Sub AssignPickReels(ByVal wsid As String, ByVal id As Integer, ByVal itemNum As String,
                                        ByVal warehouseOT As String, ByVal username As String,
                                        Optional ByRef fpaw As Boolean = False, Optional ByVal dateStamp As Date? = Nothing)
            If Not dateStamp.HasValue Then dateStamp = Now()
            Dim category As String = SelInventoryCategory(wsid, itemNum)
            Dim pref = SelSystemPref(wsid)
            Dim fifo As Boolean = pref("FIFO Pick Across Warehouse")
            Dim gvRTPickLogic As String = pref("Reel Tracking Pick Logic")

            Dim invMapRecords = PickLogic(wsid, id, itemNum, warehouseOT, dateStamp, username, gvRTPickLogic)
            Dim invMapRecordsFifo = FIFONextLoc(wsid, id, itemNum, warehouseOT, dateStamp, username, gvRTPickLogic, invMapRecords, category, fifo, fpaw)
            ProcessInvMapRecords(wsid, id, itemNum, warehouseOT, dateStamp, username, gvRTPickLogic, invMapRecordsFifo, fpaw)

        End Sub

        ''' <summary>
        ''' Determines the pick logic and gets locations corresponding to it
        ''' </summary>
        ''' <param name="wsid">The workstation taht is currently being worked on</param>
        ''' <param name="id">The desired id of the master ot record</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="warehouseOT">The desired warehouse</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="username">The suer that is currently logged in</param>
        ''' <param name="gvRTPickLogic">The pick logic that determines output</param>
        ''' <returns>A list of dictionary that contains the locations for the pick logic</returns>
        Private Function PickLogic(ByVal wsid As String, ByVal id As Integer, ByVal itemNum As String,
                                        ByVal warehouseOT As String, ByVal dateStamp As Date,
                                        ByVal username As String, ByVal gvRTPickLogic As String) As List(Of Dictionary(Of String, Object))

            Dim invMapRecords As List(Of Dictionary(Of String, Object))

            If gvRTPickLogic = "Closest Demand Quantity" Then
                invMapRecords = GetInvMapRecordsClosestDemandQty(wsid, id, warehouseOT, itemNum)
            ElseIf gvRTPickLogic = "Serial Number Sort - FIFO" Then
                invMapRecords = GetVInventoryMapRecords(wsid, warehouseOT, itemNum)
            ElseIf gvRTPickLogic = "Dynamic" Then
                invMapRecords = GetInvMapRecordsDynamic(wsid, warehouseOT, itemNum)
            Else
                insertErrorMessages("Location Assignment", "PickReels.PickLogic", "gvRTPickLogic unexpected: " & gvRTPickLogic, username, wsid)
                invMapRecords = Nothing
            End If

            Return invMapRecords
        End Function

        ''' <summary>
        ''' Gets the locations for FIFO
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The desired ot id</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="warehouseOT">The desired warehouse</param>
        ''' <param name="dateStamp">The desired datestamp</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="gvRTPickLogic">The pick logic</param>
        ''' <param name="invMapRecords">A list of dictionaory that contains locations</param>
        ''' <param name="category">The desired category</param>
        ''' <param name="fifo">The fifo value</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <returns>A list of dictionary that contains possible locations and their data</returns>
        Private Function FIFONextLoc(
                                    ByVal wsid As String, ByVal id As Integer, ByVal itemNum As String, ByVal warehouseOT As String,
                                    ByVal dateStamp As Date, ByVal username As String, ByVal gvRTPickLogic As String,
                                    ByVal invMapRecords As List(Of Dictionary(Of String, Object)), ByVal category As String,
                                    ByVal fifo As Boolean, ByRef fpaw As Boolean) As List(Of Dictionary(Of String, Object))
            If invMapRecords.Count = 0 Then
                ' Only for Serial Number Sort - FIFO and Dynamic
                If gvRTPickLogic = "Serial Number Sort - FIFO" OrElse gvRTPickLogic = "Dynamic" Then
                    If fifo AndAlso category = "FIFO Pick Across Warehouse" AndAlso Not fpaw Then
                        invMapRecords = GetRTReelContainerItemNum(wsid, itemNum)
                        fpaw = True
                        Return FIFONextLoc(wsid, id, itemNum, warehouseOT, username, gvRTPickLogic, dateStamp, invMapRecords, category, fifo, fpaw)
                    Else
                        Reprocess(wsid, id, warehouseOT, username, dateStamp)
                        Return Nothing
                    End If
                Else
                    ' Dynamic
                    Reprocess(wsid, id, warehouseOT, username, dateStamp)
                    Return Nothing
                End If
            End If

            ' Exact map records from container with avail qty for Dynamic
            If gvRTPickLogic = "Dynamic" Then
                Dim rln = invMapRecords(0)("Reel Location Number")
                invMapRecords = GetVInventoryMapPickReels(wsid, itemNum, rln, warehouseOT, fpaw)
            End If

            Return invMapRecords

        End Function

        ''' <summary>
        ''' Process each location
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The desired ot id</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="warehouseOT">The desired warehouse</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="gvRTPickLogic">The pick logic</param>
        ''' <param name="invMapRecords">A list of dictionary that contains locations and their data</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        Private Sub ProcessInvMapRecords(ByVal wsid As String, ByVal id As Integer, ByVal itemNum As String, ByVal warehouseOT As String, ByVal dateStamp As Date, ByVal username As String,
                                         ByVal gvRTPickLogic As String, ByVal invMapRecords As List(Of Dictionary(Of String, Object)), ByVal fpaw As Boolean)
            For Each invMap In invMapRecords
                Dim ot = SelOTMasterRecordNullLoc(wsid, id)
                Dim rln = invMap("Reel Location Number")
                Dim locQty = If(gvRTPickLogic = "Dynamic", GetVInventoryMapQtySum(wsid, rln), invMap("Item Quantity"))
                Dim transQty = ot("Transaction Quantity")
                Dim origQty = If(ot("Master Record"), transQty, ot("Line Sequence"))

                If locQty >= transQty Then
                    If fpaw Then UpdOTUserField5(wsid, ot("ID"), ot("Warehouse"))

                    UpdOTLocAssPickReels(wsid, gvRTPickLogic, ot, invMap, If(gvRTPickLogic = "Dynamic", transQty, locQty), transQty, locQty, origQty, transQty)

                    If gvRTPickLogic = "Dynamic" Then
                        Return
                    End If

                    UpdInvMapLocAssPickReelsPutAway(wsid, invMap("Inv Map ID"), ot("User Field1"))

                    If locQty > transQty Then
                        InsOTLocAssPickReelsPutAway(wsid, ot("ID"), locQty - transQty)
                    Else
                        InsOTLocAssPickReelsPutAway(wsid, ot("ID"), 1)
                    End If

                    UpdOTMasterRecordID(wsid, True)

                    Return
                Else
                    InsOTLocAssPickReelsSplitPick(wsid, ot("ID"), transQty - locQty)

                    If fpaw Then UpdOTUserField5(wsid, ot("ID"), ot("Warehouse"))

                    UpdOTLocAssPickReels(wsid, gvRTPickLogic, ot, invMap, locQty, transQty, locQty, origQty, locQty)

                    If gvRTPickLogic <> "Dynamic" Then

                        InsOTLocAssPickReelsPutAway(wsid, ot("ID"), 1)
                        UpdInvMapLocAssPickReelsPutAway(wsid, invMap("Inv Map ID"), ot("User Field1"))
                    End If

                    ' Split The Pick
                    fpaw = False
                End If

                If gvRTPickLogic <> "Serial Number Sort - FIFO" Then
                    ' Assign the split
                    AssignPickReels(wsid, id, itemNum, warehouseOT, username, fpaw, dateStamp)
                    Return
                End If

                ' Will only loop if gvRTPickLogic = Serial Number Sort - FIFO and splits

            Next
        End Sub

        ''' <summary>
        ''' Gets locatiosn that are the closets to the desired quantity
        ''' </summary>
        ''' <param name="wsid">The workstationt hat is currently being worked on</param>
        ''' <param name="id">The desired ot id</param>
        ''' <param name="warehouseOT">The desired warehouse</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <returns>A list of dictionary that contains locations and their data</returns>
        Private Function GetInvMapRecordsClosestDemandQty(ByVal wsid As String, ByVal id As Integer, ByVal warehouseOT As String, ByVal itemNum As String) As List(Of Dictionary(Of String, Object))
            Dim transQty = SelOTTransQtyMasterRecordNullLoc(wsid, id)
            ' Check for greater than first with 'Less Than' set to False
            Dim invMapRecords = GetVInventoryMapRecords(wsid, warehouseOT, itemNum, False, transQty)
            If invMapRecords.Count = 0 Then
                ' No greater than records found, check less than
                invMapRecords = GetVInventoryMapRecords(wsid, warehouseOT, itemNum, True, transQty)
            End If
            Return invMapRecords
        End Function

        ''' <summary>
        ''' Gets the inventory map dynamic warehouse location records
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="warehouseOT">The desired warehouse</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <returns>A list of dictionary that contains the locations and their data</returns>
        Private Function GetInvMapRecordsDynamic(ByVal wsid As String, ByVal warehouseOT As String, ByVal itemNum As String) As List(Of Dictionary(Of String, Object))
            Dim invMapRecords = GetRTReelContainerItemNumWH(wsid, itemNum, warehouseOT)
            ' Now pop over any records where the query returns a count of 0
            Dim popUntil As Integer = 0
            For Each invMapRow In invMapRecords
                Dim count = GetVInventoryMapCount(wsid, itemNum, invMapRow("Reel Location Number"))
                If count <> 0 Then
                    Exit For
                End If
                popUntil += 1
            Next
            invMapRecords = invMapRecords.GetRange(popUntil, invMapRecords.Count - popUntil)
            Return invMapRecords
        End Function

        ''' <summary>
        ''' Gets the quantity depending on the pick logic
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="invMapRow">Dictionary that contaisn the desired location's data</param>
        ''' <param name="rtPickLogic">The pick logic</param>
        ''' <returns>An integer that tells the lcoation quantity</returns>
        Private Function GetLocQty(ByVal wsid As String, ByVal invMapRow As Dictionary(Of String, Object), ByVal rtPickLogic As String) As Integer
            Dim ret As Integer
            If rtPickLogic = "Dynamic" Then
                ret = GetVInventoryMapQtySum(wsid, invMapRow("Reel Location Number"))
            Else
                ret = invMapRow("Item Quantity")
            End If

            Return ret
        End Function

        ''' <summary>
        ''' Inserts records into reprocess
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="otid">The desired ot id to indert</param>
        ''' <param name="warehouseOT">The warehouse value</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        Private Sub Reprocess(ByVal wsid As String, ByVal otid As Integer, ByVal warehouseOT As String, ByVal username As String, ByVal dateStamp As Date)
            Dim otRemaining = SelOTMasterRecordNullLoc(wsid, otid)
            If otRemaining.Keys.Count <> 0 Then
                Dim reasonMessage = If(IsNothingOrDBNull(warehouseOT), "No quantity available to allocate for Reel Tracking.", "No quantity available in Warehouse code to allocate for Reel Tracking.")
                InsOTTempPickReels(wsid, otRemaining("ID"), "Location Assignment ReProcess", reasonMessage, dateStamp, username)
                DelOTID(wsid, otRemaining("ID"), False)
            End If
        End Sub

        ''' <summary>
        ''' Gets the quantity sum for the desired location
        ''' </summary>
        ''' <param name="wsid">The workstation that si currently being worked on</param>
        ''' <param name="RLN">The reel location number</param>
        ''' <returns>An integer telling what he quantity sum is</returns>
        Private Function GetVInventoryMapQtySum(ByVal wsid As String, ByVal RLN As String)
            Dim ret = GetResultSingleCol("SelVInventoryMapQtySum", wsid, {{"@RLN", RLN, strVar}})
            Return ret
        End Function

        ''' <summary>
        ''' For Dynamic Pick Logic
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="RLN">The desired reel location number</param>
        ''' <param name="warehouse">The desired warehouse</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <returns>A list of dictionary that contains locations and their data</returns>
        Private Function GetVInventoryMapPickReels(ByVal wsid As String,
                                                   ByVal itemNum As String, ByVal RLN As String,
                                                   ByVal warehouse As String, ByVal fpaw As Boolean
                                                   ) As List(Of Dictionary(Of String, Object))
            Dim ret = GetResultMapList("SelVInventoryMapPickReels", wsid, {
                                       {"@ItemNum", itemNum, strVar},
                                       {"@RLN", RLN, strVar},
                                       {"@Warehouse", warehouse, strVar},
                                       {"@FPAW", fpaw, boolVar}})

            Return ret

        End Function

        ''' <summary>
        ''' Updates open transaction pick reels
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="rtPickLogic">The desired pick logic</param>
        ''' <param name="otRow">A dictionary that contaisn the data for the desired open transaction record</param>
        ''' <param name="invMapRow">A dictionary that contains the data for the desired inventory map record</param>
        ''' <param name="qty">The desired quantity</param>
        ''' <param name="transQty">The desired trasnaction quantity</param>
        ''' <param name="locQty">The desired location quantity</param>
        ''' <param name="origQty">The original quantity</param>
        ''' <param name="demandQty">The quantity that is being demanded</param>
        Private Sub UpdOTLocAssPickReels(ByVal wsid As String, ByVal rtPickLogic As String,
                                         ByVal otRow As Dictionary(Of String, Object),
                                         ByVal invMapRow As Dictionary(Of String, Object),
                                         ByVal qty As Integer, ByVal transQty As Integer, ByVal locQty As Integer,
                                         ByVal origQty As Integer, ByVal demandQty As Integer)
            Dim params As String(,) = {
                {"@ID", otRow("ID"), intVar},
                {"@MapID", invMapRow("Inv Map ID"), strVar},
                {"@SerNum", invMapRow("Serial Number"), strVar},
                {"@Qty", qty, intVar},
                {"@Loc", LocationFromInvMap(invMapRow), strVar},
                {"@Whse", invMapRow("Warehouse"), strVar},
                {"@Zone", invMapRow("Zone"), strVar},
                {"@Carousel", invMapRow("Carousel"), strVar},
                {"@Row", invMapRow("Row"), strVar},
                {"@Shelf", invMapRow("Shelf"), strVar},
                {"@Bin", invMapRow("Bin"), strVar},
                {"@OrigQty", origQty, intVar},
                {"@DemandQty", demandQty, intVar}}

            RunSP("UpdOTLocAssPickReels", wsid, params)
        End Sub

        ''' <summary>
        ''' Inserts a reel transaction put away
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The id of the desired record to create a put away for</param>
        ''' <param name="qty">The quantity that is being put away</param>
        Private Sub InsOTLocAssPickReelsPutAway(ByVal wsid As String, ByVal id As Integer, ByVal qty As Integer)
            RunSP("InsOTLocAssPickReelsPutAway", wsid, {{"@ID", id, intVar}, {"@Qty", qty, intVar}})
        End Sub

        ''' <summary>
        ''' Inserts a new split transaction in open transaction
        ''' </summary>
        ''' <param name="wsid">The wokrstation that is currently being worked on</param>
        ''' <param name="id">The id of open transaction record to split from</param>
        ''' <param name="qty">The quantity of the split</param>
        Private Sub InsOTLocAssPickReelsSplitPick(ByVal wsid As String, ByVal id As Integer, ByVal qty As Integer)
            RunSP("InsOTLocAssPickReelsSplitPick", wsid, {{"@ID", id, intVar}, {"@Qty", qty, intVar}})
        End Sub

        ''' <summary>
        ''' Inserts a reel transaction into the open transactions temp (reprocess) table
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="otid">The desired id to get the data for the transaction from</param>
        ''' <param name="reason">The reason for the record being reprocessed</param>
        ''' <param name="reasonMessage">The more detailed explanantion of the reason</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="username">The suer that is currently logged in</param>
        Private Sub InsOTTempPickReels(ByVal wsid As String, ByVal otid As Integer, ByVal reason As String, ByVal reasonMessage As String, ByVal dateStamp As Date, ByVal username As String)
            RunSP("InsOTTempPickReels", wsid, {
                  {"@OTID", otid, intVar},
                  {"@Reason", reason, strVar},
                  {"@ReasonMessage", reasonMessage, strVar},
                  {"@DateStamp", dateStamp, dteVar},
                  {"@NameStamp", username, strVar}})
        End Sub

        ''' <summary>
        ''' Selects the containder data for the desired item number
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <returns>A list of dictionary that contains the container info for the item number</returns>
        Private Function GetRTReelContainerItemNum(ByVal wsid As String, ByVal itemNum As String)
            Dim ret = GetResultMapList("SelRTReelContainerItemNum", wsid, {{"@ItemNum", itemNum, strVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Gets the number of records for the item number of location number combination
        ''' </summary>
        ''' <param name="wsid">The wokrstation that is currently being worked on</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="reelLocNum">The desireed reel location number</param>
        ''' <returns>An integer that tells the count value</returns>
        Private Function GetVInventoryMapCount(
                                              ByVal wsid As String,
                                              ByVal itemNum As String,
                                              ByVal reelLocNum As String) As Integer
            Dim ret = GetResultSingleCol("SelVInventoryMapCount", wsid, {
                                         {"@ItemNum", itemNum, strVar},
                                         {"@ReelLocNum", reelLocNum, strVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Gets the desired records from the vInventoryMap table
        ''' </summary>
        ''' <param name="wsid">The wokrstation that is currently being worked on</param>
        ''' <param name="warehouse">The desired warehouse value</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="lessThan">If the quantities shoudl be compared with a greater than or a less than</param>
        ''' <param name="transQtyCmp">The transaction quantty to compare for</param>
        ''' <returns>A list of dictionary that contains the desired locations</returns>
        Private Function GetVInventoryMapRecords(
                        ByVal wsid As String, ByVal warehouse As String,
                        ByVal itemNum As String, Optional ByVal lessThan As Boolean = False,
                        Optional ByVal transQtyCmp As Integer = -1) As List(Of Dictionary(Of String, Object))
            Dim params As String(,) = {{"@ItemNum", itemNum, strVar},
                                       {"@Warehouse", warehouse, strVar},
                                       {"@LessThan", lessThan, boolVar},
                                       {"@TransQty", transQtyCmp, intVar}}
            Dim ret = GetResultMapList("SelVInventoryMapItemNo", wsid, params)
            Return ret
        End Function

        ''' <summary>
        ''' Gets the containers for the item number and warehouse combination
        ''' </summary>
        ''' <param name="wsid">The workstation that is currenlty beinging worked on</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="warehouse">The desired warehouse</param>
        ''' <returns>A list of dictionary that contains the containers adn their data</returns>
        Private Function GetRTReelContainerItemNumWH(
                        ByVal wsid As String, ByVal itemNum As String,
                        ByVal warehouse As String) As List(Of Dictionary(Of String, Object))
            Dim ret = GetResultMapList("SelRTReelContainerItemNumWH", wsid, {
                                       {"@ItemNum", itemNum, strVar},
                                       {"@Warehouse", warehouse, strVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Updates the desired inventory map records's user field 1
        ''' </summary>
        ''' <param name="wsid">The wokrstation is currently being worked on</param>
        ''' <param name="invMapId">The desired record to update</param>
        ''' <param name="userField1">The new user field 1 value</param>
        Private Sub UpdInvMapLocAssPickReelsPutAway(ByVal wsid As String, ByVal invMapId As String, ByVal userField1 As String)
            RunSP("UpdInvMapLocAssPickReelsPutAway", wsid, {{"@InvMapID", invMapId, strVar}, {"@UserField1", userField1, strVar}})
        End Sub

        ''' <summary>
        ''' Updates the open trasnaction record's user field 5
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The id of the record to update</param>
        ''' <param name="userField5">The new value for user field 5</param>
        Private Sub UpdOTUserField5(ByVal wsid As String, ByVal id As Integer, ByVal userField5 As String)
            RunSP("UpdOTUserField5", wsid, {{"@ID", id, intVar}, {"@UserField5", userField5, strVar}})
        End Sub

    End Module
End Namespace

