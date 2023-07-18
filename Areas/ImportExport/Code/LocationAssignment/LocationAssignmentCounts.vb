' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module LocationAssignmentCounts
        Private laLock As New Object

        ''' <summary>
        ''' Prevents other threads from executing while this one does
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="otids">A list of open transaction ids</param>
        Public Sub AssignCountsSync(ByVal wsid As String, ByVal username As String, Optional ByVal otids As List(Of Integer) = Nothing)
            SyncLock laLock
                AssignCounts(wsid, username, otids)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Assigns the desired count transactions
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently logged in</param>
        ''' <param name="username">The suer that is currently logged in</param>
        ''' <param name="otids">A list of open transaction ids</param>
        Private Sub AssignCounts(ByVal wsid As String, ByVal username As String, Optional ByVal otids As List(Of Integer) = Nothing)

            Dim dateStamp = Now()

            Dim pref = GetResultMap("SelPreferencesLocationAssignment", wsid, {{"@WSID", wsid, strVar}})
            pref.Add("Auto Complete Backorders", GetResultMap("SelXferPref", "IE")("Auto Complete Backorders"))

            Dim paramsSelOTLocationAssignmentCounts As String = ""
            ' If OTIDs are passed, build a comma separted string
            ' Otherwise, will get all OT counts that need location assigned
            If otids IsNot Nothing Then
                paramsSelOTLocationAssignmentCounts = String.Join(",", otids)
            End If

            Dim retSelOTLocationAssignmentCounts = SelOTLocationAssignmentCounts(wsid, paramsSelOTLocationAssignmentCounts)

            'TODO: EventLogAlloc Count of result set {EventLogAlloc("LocAssCount", rst1.RecordCount, "Count Location Assignment")}

            For Each row In retSelOTLocationAssignmentCounts
                'Checks if itemnum exists in inventory and inventory map
                Dim invItemNumCheck As Boolean = SelInventoryValidItemNumber(wsid, row("Item Number"))

                'First check for Rejects that can not be assigned
                If Not invItemNumCheck Then
                    If pref("Auto Complete Backorders") Then
                        AutoCompleteOT(wsid, row("ID"))
                    Else
                        Dim reasonMessage As String = If(invItemNumCheck, "Invalid Item ID - NOT in Inventory.", "NO Locations for Item ID in Inventory Map.")
                        ToReprocessQueue(wsid, row("ID"), dateStamp, username, reasonMessage)

                    End If
                    Continue For
                End If

                ' Get InvMap matches
                Dim invMapLocations = SelInvMapLocAssignCounts(wsid, row("Item Number"), row("Warehouse"), row("Lot Number"), row("Serial Number"), row("Expiration Date"))

                If invMapLocations.Count < 1 Then
                    Dim reasonMessage = String.Format(
                            "No Location for Item Number: {0}, Warehouse: {1}, Lot Number: {2}, Serial Number: {3}, Expiration Date: {4}",
                            row("Item Number"), row("Warehouse"), row("Lot Number"), row("Serial Number"), row("Expiration Date"))

                    ToReprocessQueue(wsid, row("ID"), dateStamp, username, reasonMessage, row("Master Record ID"))

                    Continue For
                End If

                If IsNothingOrDBNull(row("Location")) Then
                    UpdOTLocationFull(wsid, row("ID"), invMapLocations(0)("Inv Map ID"))
                End If

                For Each invMapRow In invMapLocations.GetRange(1, invMapLocations.Count - 1)
                    InsOTSplitLocAssignCount(wsid, row("ID"), invMapRow("Inv Map ID"))
                Next
            Next ' Next OT

            UpdOTMasterRecordID(wsid, True)

        End Sub

        ''' <summary>
        ''' Inserts a split count transaction
        ''' </summary>
        ''' <param name="wsid">The wrokstation that is currently being worked on</param>
        ''' <param name="masterOTID">The master id</param>
        ''' <param name="invMapID">The desired inventory map id</param>
        Private Sub InsOTSplitLocAssignCount(ByVal wsid As String, ByVal masterOTID As Integer, ByVal invMapID As Integer)
            RunSP("InsOTSplitLocAssignCount", wsid, {
                          {"@MasterID", masterOTID, intVar},
                          {"@InvMapID", invMapID, intVar}})
        End Sub

        ''' <summary>
        ''' Selects the desired locations
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="itemNum">The desired item number</param>
        ''' <param name="warehouse">The desired warehouse</param>
        ''' <param name="lotNum">The desired lot number</param>
        ''' <param name="serialNum">The desired serial number</param>
        ''' <param name="expDate">The desired expiration date</param>
        ''' <returns>A list of dictionary that contains the locations and their data</returns>
        Private Function SelInvMapLocAssignCounts(ByVal wsid As String, ByVal itemNum As String, ByVal warehouse As String, ByVal lotNum As String, ByVal serialNum As String, ByVal expDate As Date) As List(Of Dictionary(Of String, Object))
            Dim ret = GetResultMapList("SelInvMapLocAssignCounts", wsid, {
                      {"@ItemNumber", itemNum, strVar},
                      {"@Warehouse", warehouse, strVar},
                      {"@LotNumber", lotNum, strVar},
                      {"@SerialNumber", serialNum, strVar},
                      {"@ExpDate", expDate, dteVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Selects count transactions for location assignment
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being wokred on</param>
        ''' <param name="idList">A list of open transaction ids</param>
        ''' <returns>A list of dictionary that contains the information on the desired transactions</returns>
        Private Function SelOTLocationAssignmentCounts(ByVal wsid As String, ByVal idList As String) As List(Of Dictionary(Of String, Object))
            Dim ret = GetResultMapList("SelOTLocationAssignmentCounts", wsid, {{"@IDs", idList, strVar}})
            Return ret
        End Function

    End Module
End Namespace
