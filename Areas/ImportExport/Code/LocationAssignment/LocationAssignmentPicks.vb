' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module LocationAssignmentPicks
        Private laLock As New Object()
        Dim locInCode As Integer = 0

        ''' <summary>
        ''' Sync that prevents other threads from running while this is executing
        ''' </summary>
        ''' <param name="wsid">The workstation that is being wokred on</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="singleOtid">Determines if a single ot id is used</param>
        ''' <param name="isSplit">If this a split case</param>
        ''' <param name="invMapIdFromShort">The desired inventory map id</param>
        ''' <param name="otids">A list of integer that contains open transaction ids</param>
        ''' <param name="dateStamp">The desired date stmap</param>
        Public Sub AssignPicksSync(ByVal wsid As String, ByVal username As String,
                                Optional ByVal singleOtid As Integer = -1,
                                Optional ByVal isSplit As Boolean = False,
                                Optional ByVal invMapIdFromShort As Integer = -1,
                                Optional ByVal otids As List(Of Integer) = Nothing,
                                Optional dateStamp As DateTime? = Nothing)
            SyncLock laLock
                AssignPicks(wsid, username, singleOtid, isSplit, invMapIdFromShort, otids, dateStamp)
                RunSP("Alloc_Custom_Pick", wsid)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Assigns pick transactions
        ''' </summary>
        ''' <param name="wsid">The workstation that is being wokred on</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="singleOtid">Determines if a single ot id is used</param>
        ''' <param name="isSplit">If this a split case</param>
        ''' <param name="invMapIdFromShort">The desired inventory map id</param>
        ''' <param name="otids">A list of integer that contains open transaction ids</param>
        ''' <param name="dateStamp">The desired date stmap</param>
        Private Sub AssignPicks(ByVal wsid As String, ByVal username As String,
                                Optional ByVal singleOtid As Integer = -1,
                                Optional ByVal isSplit As Boolean = False,
                                Optional ByVal invMapIdFromShort As Integer = -1,
                                Optional ByVal otids As List(Of Integer) = Nothing,
                                Optional dateStamp As DateTime? = Nothing)

            Try


                locInCode = 0
                If Not dateStamp.HasValue Then dateStamp = Now()

                locInCode = 1
                Dim autoImportCheck = GetResultSingleCol("SelXferPrefAutoImportCheck", "IE")
                locInCode = 2
                Dim pref = GetResultMap("SelPreferencesLocationAssignment", wsid, {{"@WSID", wsid, strVar}})
                locInCode = 3

                pref.Add("Automatic Import Check", autoImportCheck)

                ' Break out kits
                Dim kitsToPickAllocate = GetResultMapList("SelOTKitsToPickAllocate", wsid)
                locInCode = 4
                For Each row In kitsToPickAllocate
                    'TODO : gvPending = GetSettingLM("PickPro", "Preferences", "Import To Table")
                    locInCode = 5
                    BreakOutKits(wsid, row("ID"), False, pref("Distinct Kit Orders"))
                Next

                locInCode = 6
                Dim otidJoined = If(otids Is Nothing, "", String.Join(",", otids))
                ' Get OTs that need location assigned
                locInCode = 7
                Dim otsToLocAssign = GetOTsToLocAssign(wsid, singleOtid, otidJoined, isSplit)

                locInCode = 8
                Dim allocResetYes As Boolean = True ' TODO: gvAllocResetYes value

                locInCode = 9
                If otsToLocAssign.Count > 0 AndAlso allocResetYes Then
                    'TODO: EventLogAlloc Count of result set {EventLogAlloc("LocAssPick", rst1.RecordCount, "Pick Location Assignment")}
                    locInCode = 0
                    RunSP("QtyAllocReset", wsid, {{"@LoginName", username, strVar}, {"@DynWhse", "LA", strVar}})
                End If

                locInCode = 10
                Dim laPickSort As String = GetResultMap("SelXferPref", "IE")("LocAssPickSort")

                'Dim laPickSort As String = "Item Quantity" 'TODO: = GetResultSingle("selSettingLM", || Table => "Pick Preferences", Val => "LocAss Pick Sort")
                locInCode = 11
                Dim fpaw As Boolean = False
                locInCode = 12
                ProcessOTList(wsid, username, dateStamp, otsToLocAssign, laPickSort, fpaw, allocResetYes, pref, invMapIdFromShort)

            Catch ex As Exception
                insertErrorMessages("LocationAssignmentPicks", "AssignPicks: loc = " + locInCode.ToString, ex.ToString(), "AutoLA", wsid)
            End Try

        End Sub

        ''' <summary>
        ''' Go thorugh each open transaction given and processes them
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently ebing worked on</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="dateStamp">The deisred date stamp</param>
        ''' <param name="otList">A dictionary that contains the desired open transactiosn and their data</param>
        ''' <param name="laPickSort">The desired sorting</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <param name="allocResetYes">Tells if ot allocate after reset</param>
        ''' <param name="pref">Dictionary that contains the preferences</param>
        ''' <param name="invMapIdFromShort">The desired inventory map id</param>
        Private Sub ProcessOTList(ByVal wsid As String, ByVal username As String, ByVal dateStamp As Date, ByVal otList As List(Of Dictionary(Of String, Object)), ByVal laPickSort As String, ByRef fpaw As Boolean, ByRef allocResetYes As Boolean, ByVal pref As Dictionary(Of String, Object), ByVal invMapIdFromShort As Integer)
            Dim locinCode As Integer = 0

            Try
                locinCode = 1
                If IsNothingOrDBNull(laPickSort) OrElse laPickSort = "" Then
                    locinCode = 2
                    laPickSort = "Item Quantity"
                    'TODO: SaveSettingLM "PickPro", "Pick Preferences", "LocAss Pick Sort", "Item Quantity"
                End If
                locinCode = 11
                For Each ot In otList
                    locinCode = 3
                    If ot("Sub Category") = "Reel Tracking" Then
                        locinCode = 4
                        AssignPickReels(wsid, ot("Master Record ID"), ot("Item Number"), ot("Warehouse"), username, dateStamp:=dateStamp)
                        fpaw = False
                        locinCode = 5
                        Continue For
                    End If

                    locinCode = 6
                    Dim fifo = GetFIFOCode(ot("FIFO"), ot("FIFO Date"))

                    locinCode = 7
                    Dim invMapLocs = GetInvMapLocations(wsid, ot("Warehouse"), fifo, ot("Item Number"), ot("Lot Number"), ot("Serial Number"), ot("Expiration Date"), laPickSort)
                    locinCode = 8
                    FifoNextLoc(wsid, ot, invMapLocs, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref)
                    locinCode = 9
                    fpaw = False
                Next
                locinCode = 10

            Catch ex As Exception
                insertErrorMessages("LocationAssignmentPicks", "ProcessOTList: loc = " + locInCode.ToString, ex.ToString(), "AutoLA", wsid)

            End Try
            'TODO: Alloc_Custom_Pick
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="ot">Dictionary that contains the desired open trasnaction records and their data</param>
        ''' <param name="invMapLocs">Dictionary that contaisn the desired inventory map records and their data</param>
        ''' <param name="laPickSort">The desired sort</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <param name="username">The user that is currently logged in</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="invMapIdFromShort">The desired inventory mpa id</param>
        ''' <param name="pref">Dictionary that cotnans the preferences and their values</param>
        ''' <param name="index">The position to start looking at locations</param>
        Private Sub FifoNextLoc(ByVal wsid As String, ByVal ot As Dictionary(Of String, Object), ByVal invMapLocs As List(Of Dictionary(Of String, Object)), ByVal laPickSort As String,
                                ByRef fpaw As Boolean, ByVal username As String, ByVal dateStamp As Date, ByVal invMapIdFromShort As Integer, ByVal pref As Dictionary(Of String, Object),
                                Optional index As Integer = 0)
            Dim locinCode As Integer = 0

            Try
                locinCode = 1

                ' Try to get records if there are none
                If index >= invMapLocs.Count Then

                    locinCode = 2
                    If pref("FIFO Pick Across Warehouse") _
                    AndAlso ot("Category") = "FIFO Pick Across Warehouse" _
                    AndAlso Not fpaw Then
                        locinCode = 3
                        invMapLocs = GetInvMapLocations(wsid, ot("Warehouse"), 1, ot("Item Number"), ot("Lot Number"), ot("Serial Number"), ot("Expiration Date"), laPickSort, fifoAcrossWH:=True)
                        locinCode = 4
                        fpaw = True
                        locinCode = 5
                        FifoNextLoc(wsid, ot, invMapLocs, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref)
                        locinCode = 6
                    Else
                        locinCode = 7
                        ToReprocessQueue(wsid, ot("ID"), dateStamp, username, "No Available Quantity in Stock to Allocate to this transaction.", ot("Master Record ID"))
                        locinCode = 8
                        fpaw = False
                    End If
                    Return
                End If

                locinCode = 9
                ' Definitely have a match
                For iter As Integer = index To invMapLocs.Count - 1
                    locinCode = 10
                    Dim invMap = invMapLocs(iter)
                    locinCode = 11
                    If invMap("Inv Map ID") = invMapIdFromShort Then Continue For

                    locinCode = 12
                    Dim splitCaseBool As Boolean = ot("Split Case")
                    locinCode = 13
                    Dim caseQty As Integer

                    locinCode = 14
                    If splitCaseBool Then
                        locinCode = 15
                        caseQty = 1
                    Else
                        locinCode = 16
                        caseQty = ot("Case Quantity")
                        locinCode = 17
                        If caseQty = 0 Then
                            locinCode = 18
                            splitCaseBool = True
                            locinCode = 19
                        End If
                    End If

                    locinCode = 20
                    ' Pick Fence
                    Dim pfModel As New PickFenceModel With {
                    .Checked = False,
                    .PFQty = ot("Pick Fence Qty"),
                    .ReplenZone = invMap("Replenishment Zone"),
                    .CasePickQty = ot("Transaction Quantity"),
                    .Index = iter
                }

                    locinCode = 21
                    Dim pfPath = PickFence(pfModel, invMapLocs)

                    locinCode = 22
                    HandlePickFenceResult(wsid, pfPath, pfModel, ot, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref, invMapLocs, caseQty, splitCaseBool)

                    Return
                Next

                ' Reached the end and didn't find a match
                ' Try one more time, will reprocess if none found again
                locinCode = 23
                FifoNextLoc(wsid, ot, invMapLocs, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref)

            Catch ex As Exception
                insertErrorMessages("LocationAssignmentPicks", "FifoNextLoc: loc = " + locInCode.ToString, ex.ToString(), "AutoLA", wsid)

            End Try

        End Sub

        ''' <summary>
        ''' Handles whena pick fence is seen
        ''' </summary>
        ''' <param name="wsid">The wrokstation that is currently being worked on</param>
        ''' <param name="pfPath">The current stage of the pick fence</param>
        ''' <param name="pfModel">Object that contains data for the pick fence</param>
        ''' <param name="ot">Dictionary that contains the desired open transactions records and their data</param>
        ''' <param name="laPickSort">The desired sort</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <param name="username">User that is currently logged in</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="invMapIdFromShort">The desired inventory map id</param>
        ''' <param name="pref">Dictionary that contains the preferences</param>
        ''' <param name="invMapLocs">A list of dictionary that contains locationsa dn their data</param>
        ''' <param name="caseQty">The desired quantity amount</param>
        ''' <param name="splitCaseBool">If this is a split case</param>
        Private Sub HandlePickFenceResult(wsid As String, pfPath As NextSub, pfModel As PickFenceModel, ot As Dictionary(Of String, Object),
                                          laPickSort As String, ByRef fpaw As Boolean, username As String, dateStamp As Date, invMapIdFromShort As Integer,
                                          pref As Dictionary(Of String, Object), invMapLocs As List(Of Dictionary(Of String, Object)),
                                          caseQty As Integer, splitCaseBool As Boolean)
            Dim locinCode As Integer = 0
            Try
                locinCode = 1
                If pfPath = NextSub.FIFONextLoc Then
                    locinCode = 2
                    FifoNextLoc(wsid, ot, invMapLocs, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref, pfModel.Index)
                    locinCode = 3
                End If

                locinCode = 4
                If pfPath = NextSub.AssignLoc Then
                    locinCode = 5
                    Dim assignSub = AssignLoc(splitCaseBool, caseQty, ot("Transaction Quantity"), pfModel)

                    locinCode = 6
                    Dim fromAssign As NextSub

                    locinCode = 7
                    If assignSub = NextSub.NotSplitCase Then
                        locinCode = 8
                        fromAssign = NotSplitCase(wsid, ot, invMapLocs(pfModel.Index), caseQty, fpaw)
                        locinCode = 9
                    ElseIf assignSub = NextSub.SplitCase Then
                        locinCode = 10
                        fromAssign = SplitCase(wsid, ot, invMapLocs(pfModel.Index), fpaw)
                    End If
                    locinCode = 11

                    ' Now where to go now that we assigned (maybe)
                    ' fromAssign could also be NextSub.None, in which case we do nothing
                    locinCode = 12
                    If fromAssign = NextSub.SplitAssign Then
                        locinCode = 13
                        AssignPicks(wsid, username, ot("Master Record ID"), True, invMapIdFromShort, dateStamp:=dateStamp)
                        locinCode = 14
                    ElseIf fromAssign = NextSub.FIFONextLoc Then
                        locinCode = 15
                        pfModel.Index += 1
                        locinCode = 16
                        FifoNextLoc(wsid, ot, invMapLocs, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref, pfModel.Index) ' Move to next invMap and call FIFONextLoc
                        locinCode = 17
                    ElseIf fromAssign = NextSub.PickFence Then
                        locinCode = 18
                        pfModel.Index += 1
                        locinCode = 19
                        pfPath = PickFence(pfModel, invMapLocs)
                        locinCode = 20
                        HandlePickFenceResult(wsid, pfPath, pfModel, ot, laPickSort, fpaw, username, dateStamp, invMapIdFromShort, pref, invMapLocs, caseQty, splitCaseBool)
                    End If
                    locinCode = 21

                End If

            Catch ex As Exception
                insertErrorMessages("LocationAssignmentPicks", "HandlePickFenceResult: loc = " + locinCode.ToString, ex.ToString(), "AutoLA", wsid)

            End Try
        End Sub

        ''' <summary>
        ''' Returns Enum deciding which path to take
        ''' </summary>
        ''' <param name="pfModel">Object that contains information for the pick fence</param>
        ''' <param name="invMapRecords">A list of dictionary that contains the desired lcoations adn their data</param>
        ''' <returns>The enum with the path to take</returns>
        Private Function PickFence(ByVal pfModel As PickFenceModel, ByVal invMapRecords As List(Of Dictionary(Of String, Object))) As NextSub
            If pfModel.PFQty > 0 AndAlso Not pfModel.Checked AndAlso pfModel.CasePickQty >= pfModel.PFQty Then
                If pfModel.Index >= invMapRecords.Count Then
                    Return NextSub.FIFONextLoc
                End If

                If Not pfModel.ReplenZone Then
                    pfModel.Index += 1
                    If pfModel.Index >= invMapRecords.Count And pfModel.Checked Then
                        Return NextSub.FIFONextLoc
                    ElseIf pfModel.Index >= invMapRecords.Count And Not pfModel.Checked Then
                        pfModel.Checked = True
                        pfModel.Index = 0
                        Return NextSub.AssignLoc
                    Else
                        pfModel.ReplenZone = invMapRecords(pfModel.Index)("Replenishment Zone")
                        Return PickFence(pfModel, invMapRecords)
                    End If
                Else
                    pfModel.Checked = True
                    Return NextSub.AssignLoc
                End If
            End If
            Return NextSub.AssignLoc
        End Function

        ''' <summary>
        ''' Assigns a location
        ''' </summary>
        ''' <param name="splitCase">If this is a split case</param>
        ''' <param name="caseQty">The quantity within the case</param>
        ''' <param name="transQty">The quantity to assign</param>
        ''' <param name="pfModel">The current path</param>
        ''' <returns>The next step for the assign</returns>
        Private Function AssignLoc(ByVal splitCase As Boolean, ByVal caseQty As Integer, ByVal transQty As Integer, ByVal pfModel As PickFenceModel) As NextSub
            Dim nextPath As NextSub
            If Not splitCase AndAlso transQty >= pfModel.PFQty AndAlso Not pfModel.Checked Then
                nextPath = NextSub.NotSplitCase
            Else
                If caseQty > transQty And pfModel.ReplenZone Then
                    nextPath = NextSub.NotSplitCase
                Else
                    nextPath = NextSub.SplitCase
                End If
            End If
            Return nextPath
        End Function

        ''' <summary>
        ''' Handles a split case
        ''' </summary>
        ''' <param name="wsid">The workstations that is currently being worked on</param>
        ''' <param name="ot">Dictionary that contains the open trasnaction and its information</param>
        ''' <param name="invMap">Dictionary that contaisn the inventory mpa id and its information</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <returns>The next step for the assign</returns>
        Private Function SplitCase(ByVal wsid As String, ByVal ot As Dictionary(Of String, Object), ByVal invMap As Dictionary(Of String, Object), ByRef fpaw As Boolean) As NextSub
            If invMap("AvailQty") >= ot("Transaction Quantity") Then
                UpdateOTLAPick(wsid, ot("ID"), invMap, fpaw)
                Return NextSub.None
            Else
                Dim splitId = ot("Master Record ID")
                Dim splitQty = CInt(ot("Transaction Quantity")) - CInt(invMap("AvailQty"))
                InsertOTLASplitPick(wsid, ot("ID"), splitId, splitQty)
                UpdateOTLAPick(wsid, ot("ID"), invMap, fpaw, True, invMap("AvailQty"))
                Return NextSub.SplitAssign
            End If
        End Function

        ''' <summary>
        ''' Handles the non split case
        ''' </summary>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <param name="ot">Dictionary that contains the open trasnaction record's data</param>
        ''' <param name="invMap">Dictionary that contains the inventory map record's data</param>
        ''' <param name="caseQty">The case quatity for the location</param>
        ''' <param name="fpaw">FIFO pick across warehouse</param>
        ''' <returns>The next step for the assign</returns>
        Private Function NotSplitCase(ByVal wsid As String, ByVal ot As Dictionary(Of String, Object), ByVal invMap As Dictionary(Of String, Object), ByVal caseQty As Integer, ByVal fpaw As Boolean) As NextSub
            Dim pickCaseQty As Integer
            Dim availCaseQty As Integer
            Dim pickQty As Integer
            If caseQty = 0 Then
                pickCaseQty = 0
                availCaseQty = 0
            Else
                pickCaseQty = CInt(ot("Transaction Quantity") / caseQty)
                availCaseQty = CInt(invMap("AvailQty") / caseQty)
            End If

            If pickCaseQty = 0 Then
                Return NextSub.FIFONextLoc
            Else
                pickCaseQty = pickCaseQty * caseQty
            End If

            If availCaseQty = 0 Then
                Return NextSub.PickFence
            Else
                availCaseQty = availCaseQty * caseQty
            End If

            If pickCaseQty <= availCaseQty Then
                pickQty = pickCaseQty
            Else
                pickQty = availCaseQty
            End If

            ' PickFullCases

            If ot("Transaction Quantity") = pickQty Then
                UpdateOTLAPick(wsid, ot("ID"), invMap, fpaw)
                Return NextSub.None
            Else
                Dim splitId = ot("Master Record ID")
                Dim splitQty = ot("Transaction Quantity") - pickQty
                InsertOTLASplitPick(wsid, ot("ID"), splitId, splitQty)
                UpdateOTLAPick(wsid, ot("ID"), invMap, fpaw, True, pickQty)
                Return NextSub.SplitAssign

            End If

        End Function

        ''' <summary>
        ''' Gets all the open transactions to assign
        ''' </summary>
        ''' <param name="wsid">The workstation that is currnetly being worked on</param>
        ''' <param name="singleOtid">If a single ot id is used</param>
        ''' <param name="otidJoinedList">String that contains comma serpated ids</param>
        ''' <param name="isSplit">If the record is going to be split</param>
        ''' <returns>A list of dictionary that contains the open transaction records and their data</returns>
        Private Function GetOTsToLocAssign(ByVal wsid As String, ByVal singleOtid As Integer,
                                           ByVal otidJoinedList As String, ByVal isSplit As Boolean) As List(Of Dictionary(Of String, Object))

            Dim otsToLocAssign As List(Of Dictionary(Of String, Object))
            
            ' Get the open transactions with no location (all or just the ones passed as arguments)
            If singleOtid = -1 Then
                otsToLocAssign = GetResultMapList("SelOTLocationAssignmentPicks", wsid, {{"@IDs", otidJoinedList, strVar}})
            Else 'single otid passed in
                otsToLocAssign = GetResultMapList("SelOTLocationAssignmentPicksSplit", wsid, {{"@OTID", singleOtid, intVar}, {"@MasterRecord", isSplit, boolVar}})
            End If

            Return otsToLocAssign
        End Function

        ''' <summary>
        ''' Gets the inventory mpa locations
        ''' </summary>
        ''' <param name="wsid">Workstation that is currently being worked on</param>
        ''' <param name="whse">The warehouse value</param>
        ''' <param name="fifo">The fifo value</param>
        ''' <param name="itemNum">The item number</param>
        ''' <param name="lotNum">The lot number</param>
        ''' <param name="serialNum">The serial number</param>
        ''' <param name="expDate">The expirationd ate</param>
        ''' <param name="pickSort">The diesired sort</param>
        ''' <param name="replen">If replenishment</param>
        ''' <param name="fifoAcrossWH">if fifo across warehouse</param>
        ''' <returns>A list of dictionary that contains the inventory map locations and their data</returns>
        Private Function GetInvMapLocations(ByVal wsid As String, ByVal whse As String,
                                            ByVal fifo As Integer, ByVal itemNum As String,
                                            ByVal lotNum As String, ByVal serialNum As String,
                                            ByVal expDate As Date, ByVal pickSort As String,
                                            Optional ByVal replen As Boolean = False, Optional ByVal fifoAcrossWH As Boolean = False)
            Dim ret As List(Of Dictionary(Of String, Object))
            ret = GetResultMapList("LocAssPickInvMap", wsid, {
                                   {"@Whse", whse, strVar},
                                   {"@FIFO", fifo, intVar},
                                   {"@Item", itemNum, strVar},
                                   {"@Lot", lotNum, strVar},
                                   {"@Ser", serialNum, strVar},
                                   {"@ExpDate", expDate, dteVar},
                                   {"@PickSort", pickSort, strVar},
                                   {"@Replen", replen, boolVar},
                                   {"@FIFOAcrossWarehouse", fifoAcrossWH, boolVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Gets the fifo code
        ''' </summary>
        ''' <param name="fifo">Tells i fififo is enabled</param>
        ''' <param name="fifoDate">The field for the fifo date</param>
        ''' <returns>An integer that tells the fifo code</returns>
        Private Function GetFIFOCode(ByVal fifo As Boolean, ByVal fifoDate As String) As Integer

            Dim code As Integer = 0
            If fifo Then code = If(fifoDate = "Put Away Date", 1, 2)
            Return code

        End Function

        ''' <summary>
        ''' Updates the desired open trasnaction record
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="otid">The open transaction record id ot update</param>
        ''' <param name="invMap">Dictionary that contaisn a location and its data</param>
        ''' <param name="fpaw">FIFO pick across warehouse </param>
        ''' <param name="useQuantity">Tells if to use the provided quantity</param>
        ''' <param name="quantity">The new quantity value</param>
        Private Sub UpdateOTLAPick(ByVal wsid As String, ByVal otid As Integer, ByVal invMap As Dictionary(Of String, Object), ByVal fpaw As Boolean,
                                   Optional ByVal useQuantity As Boolean = False, Optional ByVal quantity As Integer = -1)
            Dim location = LocationFromInvMap(invMap)
            Dim warehouse = If(fpaw, invMap("Warehouse"), "NONE")
            RunSP("updOTLAPick", wsid, {
                    {"@ID", otid, intVar},
                    {"@Loc", location, strVar},
                    {"@Zone", invMap("Zone"), strVar},
                    {"@Carousel", invMap("Carousel"), strVar},
                    {"@Row", invMap("Row"), strVar},
                    {"@Shelf", invMap("Shelf"), strVar},
                    {"@Bin", invMap("Bin"), strVar},
                    {"@IMID", invMap("Inv Map ID"), intVar},
                    {"@Lot", invMap("Lot Number"), strVar},
                    {"@Exp", invMap("Expiration Date"), dteVar},
                    {"@Ser", invMap("Serial Number"), strVar},
                    {"@Qty", If(quantity = -1, invMap("AvailQty"), quantity), If(useQuantity, intVar, nullVar)},
                    {"@Warehouse", warehouse, strVar}})
        End Sub

        ''' <summary>
        ''' Inserts a new split record
        ''' </summary>
        ''' <param name="wsid">The workstationt that is currenlty being worked on</param>
        ''' <param name="otid">The record of the open transaction record</param>
        ''' <param name="splitId">The id fo the record to split</param>
        ''' <param name="splitQty">The quantity to split</param>
        Private Sub InsertOTLASplitPick(ByVal wsid As String, ByVal otid As Integer, ByVal splitId As Integer, ByVal splitQty As Integer)
            RunSP("insOTLASplitPick", wsid, {
                  {"@ID", otid, intVar},
                  {"@SplitID", splitId, intVar},
                  {"@Qty", splitQty, intVar}})
        End Sub

        ''' <summary>
        ''' Inserts a new kit
        ''' </summary>
        ''' <param name="wsid">The workstationt aht is currently being worked on</param>
        ''' <param name="otid">The ot id record to get data from</param>
        ''' <param name="pending">If teh kit is pending</param>
        ''' <param name="distinctKitOrders">If the kit order is distinct</param>
        Private Sub BreakOutKits(ByVal wsid As String, ByVal otid As Integer, ByVal pending As Boolean, ByVal distinctKitOrders As Boolean)
            Dim otRow As Dictionary(Of String, Object)
            If pending Then
                otRow = GetResultMap("SelOTPendID", wsid, {{"@ID", otid, intVar}})
            Else
                otRow = GetResultMap("SelOTID", wsid, {{"@ID", otid, intVar}})
            End If
            Dim kitDescs = GetResultMapList("SelKitItemNumberDescriptions", wsid, {{"@ItemNumber", otRow("Item Number")}})
            If distinctKitOrders Then
                For iter As Integer = 1 To otRow("Transaction Quantity")
                    For Each kitDesc In kitDescs
                        RunSP("InsOTKitLines", wsid, {
                              {"@ID", otRow("ID"), intVar},
                              {"@ItemNumber", kitDesc("Item Number"), strVar},
                              {"@KitQty", kitDesc("Kit Quantity"), intVar},
                              {"@Desc", kitDesc("Description"), strVar},
                              {"@UoM", kitDesc("Unit of Measure"), strVar},
                              {"@Pending", pending, boolVar},
                              {"@DistinctKitOrders", True, boolVar},
                              {"@TransQty", iter, intVar}})
                    Next
                Next
            Else
                For Each kitDesc In kitDescs
                    RunSP("InsOTKitLines", wsid, {
                          {"@ID", otRow("ID"), intVar},
                          {"@ItemNumber", kitDesc("Item Number"), strVar},
                          {"@KitQty", kitDesc("Kit Quantity"), intVar},
                          {"@Desc", kitDesc("Description"), strVar},
                          {"@UoM", kitDesc("Unit of Measure"), strVar},
                          {"@Pending", pending, boolVar},
                          {"@DistinctKitOrders", False, boolVar}})
                Next
            End If

            RunSP("DelOT", wsid, {{"@ID", otRow("ID"), intVar}, {"@Pending", pending, boolVar}})
            RunSP("updOTMastRecID", wsid, {{"@MasterRecord", True, boolVar}})
        End Sub

        Private Class PickFenceModel
            Property Checked As Boolean
            Property PFQty As Integer
            Property CasePickQty As Integer
            Property ReplenZone As Boolean
            Property Index As Integer
        End Class

        Enum NextSub
            None = 0
            FIFONextLoc = 1
            AssignLoc = 2
            SplitCase = 3
            NotSplitCase = 4
            PickFence = 5
            SplitAssign = 6
        End Enum

    End Module
End Namespace
