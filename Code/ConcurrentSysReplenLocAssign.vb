' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports Microsoft.AspNet.SignalR.Hubs
Imports System.Threading.Tasks
Imports System.Collections.Concurrent

Public Class ConcurrentSysReplenLocAssign
    Private Shared Clients As IHubConnectionContext(Of Object)

    Private Shared IDDictionary As New ConcurrentDictionary(Of Integer, Object)

    ''' <summary>
    ''' Processes Replenishments
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function processSystemReplenishments(C As IHubCallerConnectionContext(Of Object), WSID As String, user As String) As String
        Clients = C
        Clients.Group(WSID).updateReplenishmentStatus("All", "Processing Started")

        Dim datareader As SqlDataReader = Nothing

        Try
            datareader = RunSPArray("selReplenishCount", WSID, {{"nothing"}})

            ' If there are no valid replenishments return
            If datareader.HasRows Then
                If datareader.Read() Then
                    If Not IsDBNull(datareader(0)) Then
                        If datareader(0) = 0 Then
                            Return "no"
                        End If
                    End If
                End If
            End If
            ' Else return the result of the processed replenishments
            Return LocAssReplen(WSID, user)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "processSystemReplenishments", ex.ToString(), user, WSID)
            Return "Error in Sys Replen Loc Assignment" & ex.Message
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
                datareader = Nothing
            End If
            IDDictionary.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Processes Replenishments
    ''' </summary>
    ''' <param name="WSID">Workstation ID of the user</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>Status of the finished replenishments</returns>
    ''' <remarks></remarks>
    Private Shared Function LocAssReplen(WSID As String, user As String) As String
        EventLogAlloc("QtyAllocReset from Replenishment - LocAssReplen", "Event", WSID, user)
        QtyAllocReset(user, WSID)
        Clients.Group(WSID).updateReplenishmentStatus("All", "Inserting Pick Replenishments")
        ' Insert pick transactions for all replenish = 1, replenish exists = 0 records from Replenishment Queue
        Dim replenishmentNumber As Integer = LocReplenOT(user, WSID)
        ' If there are no transactions to place / no replenishments to reprocess return.
        If replenishmentNumber = 0 Then
            Return "no" ' no replenishments to process
        End If
        Clients.Group(WSID).updateReplenishmentStatus(replenishmentNumber, "Processing Pick Replenishments")
        LocReplenPick(user, WSID)

        Try
            RunActionSP("updateRPWarehouse", WSID, {{"nothing"}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "updateRPWarehouse", ex.ToString(), user, WSID)
        End Try
        Clients.Group(WSID).updateReplenishmentStatus(replenishmentNumber, "Inserting Put Away Replenishments")
        LocReplenOTPutAway(user, WSID)
        Clients.Group(WSID).updateReplenishmentStatus(replenishmentNumber, "Processing Put Away Replenishments")
        StartPutAways(user, WSID)
        Clients.Group(WSID).updateReplenishmentStatus(replenishmentNumber, "Cleaning Up Replenishments")
        Dim datareader As SqlDataReader = Nothing
        Try
            datareader = RunSPArray("updateReplenishmentsFinish", WSID, {{"nothing"}})

            If datareader.HasRows Then
                If datareader.Read() Then
                    If Not IsDBNull(datareader(0)) Then
                        Return datareader(0)
                    End If
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "LocAssReplen", ex.ToString(), user, WSID)
        End Try
        Return "Replenishments processed."
    End Function

    Private Shared Sub StartPutAways(user As String, WSID As String)
        Dim itemReader As SqlDataReader = Nothing
        Dim tasks As New List(Of Task), items As New List(Of String)
        Try
            itemReader = RunSPArray("selReplenishItems", WSID, {{"nothing"}})

            If itemReader.HasRows Then
                While itemReader.Read()
                    If Not IsDBNull(itemReader(0)) Then
                        items.Add(itemReader(0))
                    End If
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ConcurrentSysReplenLocAssign", "StartPutAways", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(itemReader) Then
                itemReader.Dispose()
            End If
        End Try
        For Each item In items
            tasks.Add(Task.Factory.StartNew(Sub()
                                                LocReplenPutODBC(user, WSID, item)
                                            End Sub))
        Next
        Task.WaitAll(tasks.ToArray())
    End Sub

    ''' <summary>
    ''' Inserts an Event Log allocation message
    ''' </summary>
    ''' <param name="message">Message to insert</param>
    ''' <param name="eventCode">Event code field</param>
    ''' <param name="WSID">Requesting workstation</param>
    ''' <param name="user">Requesting user</param>
    ''' <remarks></remarks>
    Private Shared Sub EventLogAlloc(message As String, eventCode As String, WSID As String, user As String)
        Try
            RunActionSP("insEventLogAlloc", WSID, {{"@Message", message, strVar}, {"@EventCode", eventCode, strVar}, {"@EventLocation", WSID, strVar}, _
                                                   {"@User", user, strVar}, {"@Notes", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "EventLogAlloc", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Resets various fields with invalid data, like Item Quantity less than 0, etc.
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting workstation</param>
    ''' <remarks></remarks>
    Private Shared Sub QtyAllocReset(user As String, WSID As String)
        Try
            RunActionSP("QtyAllocReset", WSID, {{"@LoginName", user, strVar}, {"@DynWhse", "Yes", strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "QtyAllocReset", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Inserts Replenish = 1, Replenish Exists = 0 replenishment pick transactions from replenishment queue
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">User's workstation</param>
    ''' <returns>Number of replenishment transactions of type pick.</returns>
    ''' <remarks></remarks>
    Private Shared Function LocReplenOT(user As String, WSID As String) As Integer
        Dim datareader As SqlDataReader = Nothing
        Try
            datareader = RunSPArray("insOpenTransactionsReplenishments", WSID, {{"@User", user, strVar}, {"@WSID", WSID, strVar}})

            If datareader.HasRows Then
                If datareader.Read() Then
                    If Not IsDBNull(datareader(0)) Then
                        Return datareader(0)
                    End If
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "LocReplenOT", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
            End If
        End Try
        Return 0
    End Function

    ''' <summary>
    ''' Gets the integer value of OTValue
    ''' </summary>
    ''' <param name="OTValue"></param>
    ''' <param name="caller"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getOTValueAsInt(OTValue As String, Optional caller As String = "Not specified") As Integer
        If OTValue = nullVar Then
            Return 0
        ElseIf OTValue.ToLower() = "false" Then
            Return 0
        ElseIf OTValue.ToLower() = "true" Then
            Return 1
        ElseIf IsNumeric(OTValue) Then
            Return CInt(OTValue)
        Else
            Debug.WriteLine("getOTValueAsInt: Not Numeric, value: " & OTValue)
            Throw New Exception("getOTValueAsInt: Not numeric, OTValue passed: " & OTValue & ", sending call: " & caller)
        End If
    End Function

    Private Shared Function GetSettingLM(program As String, section As String, setting As String, user As String, WSID As String) As String
        Dim datareader As SqlDataReader = Nothing
        Try
            datareader = RunSPArray("selSettingLM", WSID, {{"@WSID", WSID, strVar}, {"@Program", program, strVar}, {"@Section", section, strVar}, {"@Name", setting, strVar}})

            If datareader.HasRows Then
                If datareader.Read() Then
                    If Not IsDBNull(datareader(0)) Then
                        Return datareader(0)
                    End If
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "GetSettingLM", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
            End If
        End Try
        Return ""
    End Function

    Private Shared Sub SaveSettingLM(program As String, section As String, keynameLM As String, setting As String, user As String, wsid As String)
        Try
            RunActionSP("updateSettingLM", wsid, {{"@Program", program, strVar}, {"@Section", section, strVar}, {"@Name", keynameLM, strVar}, {"@Setting", setting, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "SaveSettingLM", ex.ToString(), user, wsid)
        End Try
    End Sub

    Private Shared Sub LocReplenOTPutAway(user As String, WSID As String)
        Try
            RunActionSP("insOTReplenishmentPuts", WSID, {{"@User", user, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "LocReplenOTPutAway", ex.ToString(), user, WSID)
        End Try
    End Sub

    Private Shared Function getOTPutReplenishments(user As String, wsid As String, ItemNumber As String)
        Dim reader As SqlDataReader = Nothing
        Dim OTData As New List(Of List(Of String))
        Dim row As New List(Of String)
        Try
            reader = RunSPArray("LocAssOpenTransactionsReplenish1", wsid, {{"@WSID", wsid, strVar}, {"@Item", ItemNumber, strVar}})
            If reader.HasRows Then
                While reader.Read()
                    For x As Integer = 0 To reader.FieldCount - 1
                        row.Add(IIf(IsDBNull(reader(x)), nullVar, reader(x)))
                    Next
                    OTData.Add(row)
                    row = New List(Of String)
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemRelenishmentLocationAssignment", "LocReplenPutODBC", ex.ToString(), user, wsid)
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
                reader = Nothing
            End If
        End Try
        Return OTData
    End Function

    Private Shared Sub LocReplenPutODBC(user As String, WSID As String, ItemNumber As String)
        Dim ItemID As String = Nothing, LotNum As String = Nothing, SerNum As String = Nothing, MapLoc As String = Nothing, Item As String = Nothing, ExpDate As String = "", LastOrder As String = "None"
        Dim AutoCmpShort As String = GetSettingLM("PickPro", "Preferences", "Auto Complete Backorders", user, WSID), OrderCount As Integer = 1, SplitCount As Integer = 0, DynCheck As Boolean = False
        Dim BulkMaxQty As Integer = 0, CarMaxQty As Integer = 0, CFMaxQty As Integer = 0, BulkMinQty As Integer = 0, CarMinQty As Integer = 0, CFMinQty As Integer = 0, warehouse As String = ""
        Dim WarehouseSensitive As Integer = 0, LocType As String = "", LocType2 As String = "", LocType1 As String = "", OrderNumber As String = "", LotMix As Integer = 0, VelCode As String = ""
        Dim CellSz As String = "", AvailQty As Integer = 0, MinQty As Integer = 0, TQty As Integer = 0, otqty As Integer = 0, SplitQty As Integer = 0, SplitID As Integer = 0
        ' Up to Master Record ID in Open Transactions, Up to CF Min Qty in Inventory, Up to Lot Mixing in System Preferences, Replenishment Queue has Replenish Exists
        Dim OTFields As List(Of String) = New List(Of String) From {"Order Number", "Item Number", "Line Number", "ID", "Required Date", "Label", "Table Type", "User Field1", "User Field2", "User Field3", "User Field4", _
                                    "User Field5", "User Field6", "User Field7", "User Field8", "User Field9", "User Field10", "Description", "Batch Pick ID", "Notes", "Line Sequence", _
                                    "Import By", "Import Date", "Transaction Type", "Priority", "Revision", "Lot Number", "Expiration Date", "Serial Number", "Transaction Quantity", _
                                    "Import Filename", "Completed Date", "Location", "Warehouse", "Zone", "Carousel", "Row", "Shelf", "Bin", "Unit of Measure", "Inv Map ID", "Master Record", _
                                    "Master Record ID", "Warehouse Code", _
                                    "Date Sensitive", "Warehouse Sensitive", "Pick Fence Qty", "Split Case", "Case Quantity", "Primary Zone", "Secondary Zone", "CarouselCell Size", _
                                    "CarouselVelocity Code", "BulkCell Size", "BulkVelocity Code", "CFCell Size", "CFVelocity Code", "FIFO", "Maximum Quantity", "Bulk Max Qty", "CF Max Qty", _
                                    "Min Quantity", "Bulk Min Qty", "CF Min Qty", _
                                    "Lot Mixing", _
                                    "Replenish Exists"}
        While True
            Dim reader As SqlDataReader = Nothing, row As New List(Of String), OTData As New List(Of List(Of String))
            DynCheck = False

            OTData = getOTPutReplenishments(user, WSID, ItemNumber)

            If (OTData.Count = 0) Then
                Return
            End If

            SplitCount = 0
            'Call QtyAllocReset(user, WSID)
            Try
                For I As Integer = 0 To OTData.Count - 1
                    Clients.Group(WSID).updateReplenishmentStatus(OTData.Count, IIf(SplitCount = 0, "Processing Put Away Replenishments", "Processing Split Transactions"))
                    Item = OTData(I)(OTFields.IndexOf("Item Number"))
                    SerNum = OTData(I)(OTFields.IndexOf("Serial Number"))
                    LotNum = OTData(I)(OTFields.IndexOf("Lot Number"))
                    ExpDate = OTData(I)(OTFields.IndexOf("Expiration Date"))
                    BulkMaxQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Bulk Max Qty")))
                    CarMaxQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Maximum Quantity")))
                    CFMaxQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("CF Max Qty")))
                    BulkMinQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Bulk Min Qty")))
                    CarMinQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Min Quantity")))
                    CFMinQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("CF Min Qty")))
                    warehouse = OTData(I)(OTFields.IndexOf("Warehouse"))
                    WarehouseSensitive = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Warehouse Sensitive")))
                    LocType = OTData(I)(OTFields.IndexOf("Primary Zone")) ' Primary Pick Zone
                    LocType1 = OTData(I)(OTFields.IndexOf("Secondary Zone")) ' Secondary Pick Zone
                    OrderNumber = OTData(I)(OTFields.IndexOf("Order Number"))
                    LotMix = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Lot Mixing")))

                    If LocType.ToLower() = "carousel" And LocType1.ToLower() = "bulk" Then
                        LocType2 = "Carton Flow"
                    ElseIf LocType.ToLower() = "carousel" And LocType1.ToLower() = "carton flow" Then
                        LocType2 = "Bulk"
                    ElseIf LocType.ToLower() = "bulk" And LocType1.ToLower() = "carousel" Then
                        LocType2 = "Carton Flow"
                    ElseIf LocType.ToLower() = "bulk" And LocType1.ToLower() = "carton flow" Then
                        LocType2 = "Carousel"
                    ElseIf LocType.ToLower() = "carton flow" And LocType1.ToLower() = "carousel" Then
                        LocType2 = "Bulk"
                    ElseIf LocType.ToLower() = "carton flow" And LocType1.ToLower() = "bulk" Then
                        LocType2 = "Carousel"
                    End If

                    If (LocType = nullVar) Then
                        LocType = "Carousel"
                    End If

                    If OrderNumber = LastOrder Then
                        OrderCount += 1
                    Else
                        OrderCount = 1
                    End If

                    LastOrder = OrderNumber
                    Dim returnVal = StartLoc(OTData, I, OTFields, LocType, VelCode, CellSz, warehouse, Item, LotNum, SerNum, DynCheck, AvailQty, _
                                  CarMaxQty, CarMinQty, MinQty, BulkMaxQty, BulkMinQty, CFMaxQty, CFMinQty, TQty, OrderCount, SplitQty, otqty, SplitID, LocType1, LocType2, AutoCmpShort, _
                                  SplitCount, user, WSID) ' StartLoc:
                    If (returnVal) Then
                        Continue While
                    End If
                    If I < OTData.Count Then ' NextOT:
                        DynCheck = False
                    End If
                Next I
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "NextOT: for loop", ex.ToString(), user, WSID)
            End Try
            If SplitCount <= 0 Then
                Exit While
            End If
        End While
    End Sub

    Private Shared Function StartLoc(OTData As List(Of List(Of String)), I As Integer, OTFields As List(Of String), ByRef LocType As String, ByRef VelCode As String, ByRef CellSz As String, _
                                ByRef warehouse As String, ByRef Item As String, ByRef LotNum As String, ByRef SerNum As String, ByRef DynCheck As Boolean, ByRef AvailQty As Integer, _
                                ByRef CarMaxQty As Integer, ByRef CarMinQty As Integer, ByRef MinQty As Integer, ByRef BulkMaxQty As Integer, ByRef BulkMinQty As Integer, _
                                ByRef CFMaxQty As Integer, ByRef CFMinQty As Integer, ByRef TQty As Integer, ByRef OrderCount As Integer, ByRef SplitQty As Integer, ByRef otqty As Integer, _
                                ByRef SplitID As Integer, ByRef LocType1 As String, ByRef LocType2 As String, ByRef AutoCmpShort As String, ByRef SplitCount As Integer, _
                                user As String, WSID As String) As Boolean
        Dim rstInvMap As SqlDataReader = Nothing
        Dim InvMapData As New List(Of List(Of String))
        Dim row As New List(Of String)
        Dim IMFields As New List(Of String) From {"Inv Map ID", "Location Type", "Item Number", "Serial Number", "Expiration Date", "Lot Number", "Warehouse", "Location String", _
                                                  "Zone", "Carousel", "Row", "Shelf", "Bin", "Unit of Measure", "Item Quantity", "Maximum Quantity", "Min Quantity", "Available Qty", _
                                                  "Date Sensitive", "Cell Size", "Velocity Code", "Put Away Date", "Dedicated", "Percent Empty"}
        Dim invMapID As Integer = 0
        Dim rst As SqlDataReader = Nothing, DynWhse As Integer = 0, MaxQty1 As Integer = 0
        Try
            For x As Integer = 1 To 3
                For v As Integer = 1 To 2
                    If LocType.ToLower() = "carousel" Then
                        VelCode = OTData(I)(OTFields.IndexOf("CarouselVelocity Code"))
                        CellSz = OTData(I)(OTFields.IndexOf("CarouselCell Size"))
                    ElseIf LocType.ToLower() = "bulk" Then
                        VelCode = OTData(I)(OTFields.IndexOf("BulkVelocity Code"))
                        CellSz = OTData(I)(OTFields.IndexOf("BulkCell Size"))
                    Else
                        VelCode = OTData(I)(OTFields.IndexOf("CFVelocity Code"))
                        CellSz = OTData(I)(OTFields.IndexOf("CFCell Size"))
                    End If
                    Try
                        InvMapData = New List(Of List(Of String))
                        rstInvMap = RunSPArray("LocAssPutAwayNoViewDynamic", WSID, {{"@DateSens", CastAsSqlBool(OTData(I)(OTFields.IndexOf("Date Sensitive"))), intVar}, _
                                                                       {"@Whse", warehouse, strVar}, {"@Velcode", VelCode, strVar}, {"@CellSz", CellSz, strVar}, _
                                                                       {"@FIFO", CastAsSqlBool(OTData(I)(OTFields.IndexOf("FIFO"))), intVar}, _
                                                                       {"@Item", Item & LotNum & SerNum, strVar}, {"@VSortBy", v, intVar}, {"@DynWhse", IIf(DynCheck, "No", "Yes"), strVar}, _
                                                                       {"@LocType", LocType, strVar}, {"@InvMapID", 0, intVar}, {"@WPZ", "0", strVar}, {"@Reel", 0, intVar}, _
                                                                       {"@Replenish", 1, intVar}})
                        If Not rstInvMap.HasRows Then
                            Continue For ' Skips to the next iteration of the for loop, GoTo NextVelocity equivalent
                        Else
                            While rstInvMap.Read()
                                For rstx As Integer = 0 To rstInvMap.FieldCount - 1
                                    row.Add(IIf(IsDBNull(rstInvMap(rstx)), nullVar, rstInvMap(rstx)))
                                Next
                                InvMapData.Add(row)
                                row = New List(Of String)
                            End While
                        End If
                    Catch ex As Exception
                        Debug.WriteLine(ex.ToString())
                        insertErrorMessages("SystemReplenishmentLocationAssignment", "StartLoc", ex.ToString(), user, WSID)
                    Finally
                        If Not IsNothing(rstInvMap) Then
                            rstInvMap.Dispose()
                            rstInvMap = Nothing
                        End If
                    End Try

                    For invIndex As Integer = 0 To InvMapData.Count - 1
                        invMapID = getOTValueAsInt(InvMapData(invIndex)(IMFields.IndexOf("Inv Map ID")), "line 425")
                        Dim lock As New Object
                        lock = IDDictionary.GetOrAdd(invMapID, lock)
                        SyncLock lock

                            If Not checkValidLocation(invMapID, Item, user, WSID) Then
                                Debug.WriteLine("Hit not checkValidLocation:" & invMapID & " " & Item)
                                Continue For ' next invIndex
                            End If

                            If InvMapData(invIndex)(IMFields.IndexOf("Available Qty")) = nullVar Then
                                AvailQty = 999999
                            Else
                                AvailQty = getOTValueAsInt(InvMapData(invIndex)(IMFields.IndexOf("Available Qty")))
                            End If
                            If AvailQty = 999999 Then
                                If LocType.ToLower() = "carousel" Then
                                    If CarMaxQty = 0 Then
                                        MinQty = CarMinQty
                                    Else
                                        AvailQty = CarMaxQty
                                        MinQty = CarMinQty
                                    End If
                                ElseIf LocType.ToLower() = "bulk" Then
                                    If BulkMaxQty = 0 Then
                                        MinQty = BulkMinQty
                                    Else
                                        AvailQty = BulkMaxQty
                                        MinQty = BulkMinQty
                                    End If
                                Else
                                    If CFMaxQty = 0 Then
                                        MinQty = CFMinQty
                                    Else
                                        AvailQty = CFMaxQty
                                        MinQty = CFMinQty
                                    End If
                                End If
                            End If

                            TQty = getOTValueAsInt(OTData(I)(OTFields.IndexOf("Transaction Quantity")))

                            Try
                                rst = RunSPArray("selDynWhse", WSID, {{"@Zone", InvMapData(invIndex)(IMFields.IndexOf("Zone")), strVar}})

                                If rst.HasRows Then
                                    If rst.Read() Then
                                        If Not IsDBNull(rst(0)) Then
                                            DynWhse = getOTValueAsInt(rst(0)) ' Dynamic Warehouse in this zone
                                        End If
                                    End If
                                End If
                            Catch ex As Exception
                                Debug.WriteLine(ex.ToString())
                                insertErrorMessages("SystemReplenishmentLocationAssignment", "StartLoc", ex.ToString(), user, WSID)
                            Finally
                                If Not IsNothing(rst) Then
                                    rst.Dispose()
                                    rst = Nothing
                                End If
                            End Try

                            If DynWhse = 0 Then
                                ' if invalid location to assign to
                                If InvMapData(invIndex)(IMFields.IndexOf("Warehouse")) <> OTData(I)(OTFields.IndexOf("Warehouse")) Then
                                    ' retry 1 time looking only for a warehouse match
                                    If Not DynCheck Then
                                        LocType = OTData(I)(OTFields.IndexOf("Primary Zone")) ' Primary Pick Zone
                                        If LocType = nullVar Then
                                            LocType = "Carousel"
                                        End If
                                        DynCheck = True
                                        Debug.WriteLine("Recursing")
                                        Return StartLoc(OTData, I, OTFields, LocType, VelCode, CellSz, warehouse, Item, LotNum, SerNum, DynCheck, AvailQty, _
                                            CarMaxQty, CarMinQty, MinQty, BulkMaxQty, BulkMinQty, CFMaxQty, CFMinQty, TQty, OrderCount, SplitQty, otqty, SplitID, LocType1, LocType2, AutoCmpShort, _
                                            SplitCount, user, WSID) ' GoTo StartLoc
                                    Else
                                        Call ReProcess(AutoCmpShort, getOTValueAsInt(OTData(I)(OTFields.IndexOf("ID"))), OrderCount, SplitID, user, WSID)
                                        Return False
                                    End If
                                End If
                            End If
                            If AvailQty >= TQty Then
                                updateOTReplen(InvMapData(invIndex)(IMFields.IndexOf("Maximum Quantity")), LocType, MaxQty1, CarMaxQty, CarMinQty, BulkMaxQty, BulkMinQty, CFMaxQty, CFMinQty, _
                                               InvMapData(invIndex)(IMFields.IndexOf("Item Number")), InvMapData(invIndex)(IMFields.IndexOf("Inv Map ID")), MinQty, Item, warehouse, LotNum, _
                                               SerNum, OTData(I)(OTFields.IndexOf("Description")), OTData(I)(OTFields.IndexOf("Unit of Measure")), _
                                               OTData(I)(OTFields.IndexOf("Expiration Date")), InvMapData(invIndex)(IMFields.IndexOf("Zone")), _
                                               InvMapData(invIndex)(IMFields.IndexOf("Carousel")), InvMapData(invIndex)(IMFields.IndexOf("Row")), _
                                               InvMapData(invIndex)(IMFields.IndexOf("Shelf")), InvMapData(invIndex)(IMFields.IndexOf("Bin")), _
                                               OrderCount, OTData(I)(OTFields.IndexOf("ID")), -1, user, WSID)
                                Return False
                            ElseIf AvailQty > 0 Then
                                ' Split Line if necessary
                                ' Not enough room at location for whole transaction qty
                                ' Split Line in Open Transactions, append duplicate record
                                ' with SplitID as the master record id and SplitQty as the Transaction Qty
                                SplitID = OTData(I)(OTFields.IndexOf("Master Record ID"))
                                otqty = OTData(I)(OTFields.IndexOf("Transaction Quantity"))

                                updateOTReplen(InvMapData(invIndex)(IMFields.IndexOf("Maximum Quantity")), LocType, MaxQty1, CarMaxQty, CarMinQty, BulkMaxQty, BulkMinQty, CFMaxQty, CFMinQty, _
                                               InvMapData(invIndex)(IMFields.IndexOf("Item Number")), InvMapData(invIndex)(IMFields.IndexOf("Inv Map ID")), MinQty, Item, warehouse, LotNum, _
                                               SerNum, OTData(I)(OTFields.IndexOf("Description")), OTData(I)(OTFields.IndexOf("Unit of Measure")), _
                                               OTData(I)(OTFields.IndexOf("Expiration Date")), InvMapData(invIndex)(IMFields.IndexOf("Zone")), _
                                               InvMapData(invIndex)(IMFields.IndexOf("Carousel")), InvMapData(invIndex)(IMFields.IndexOf("Row")), _
                                               InvMapData(invIndex)(IMFields.IndexOf("Shelf")), InvMapData(invIndex)(IMFields.IndexOf("Bin")), _
                                               OrderCount, OTData(I)(OTFields.IndexOf("ID")), AvailQty, user, WSID)
                                SplitQty = otqty - AvailQty
                                Try
                                    RunActionSP("insOTReplenSplitTrans", WSID, {{"@OTID", OTData(I)(OTFields.IndexOf("ID")), intVar}, {"@SplitID", SplitID, intVar}, {"@OrderCount", OrderCount, intVar}, _
                                                                                {"@SplitQty", SplitQty, intVar}})
                                Catch ex As Exception
                                    Debug.WriteLine(ex.ToString())
                                    insertErrorMessages("SystemReplenishmentLocationAssigment", "StartLoc", ex.ToString(), user, WSID)
                                End Try
                                SplitCount += 1
                                Return True
                            End If
                        End SyncLock
                        ' GoTo NextMap = next invIndex
                    Next invIndex
                    ' GoTo NextVelocity = next v
                Next v

                Call NextLocType(x, LocType, LocType1, LocType2)
            Next x
            Call ReProcess(AutoCmpShort, OTData(I)(OTFields.IndexOf("ID")), OrderCount, SplitID, user, WSID)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "StartLoc", ex.ToString(), user, WSID)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Checks if Inv Map location is valid for the current item to assign
    ''' </summary>
    ''' <param name="InvMapID">Map ID</param>
    ''' <param name="Item">Item Number to assign</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting workstation</param>
    ''' <returns>True if the location IS VALID for the current item, else false</returns>
    ''' <remarks></remarks>
    Private Shared Function checkValidLocation(InvMapID As Integer, Item As String, user As String, WSID As String) As Boolean
        Dim datareader As SqlDataReader = Nothing
        Try
            datareader = RunSPArray("selInvMapIDLocAssign", WSID, {{"@InvMapID", InvMapID, intVar}, {"@Item", Item, strVar}})

            If datareader.HasRows Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("ConcurrentSysReplenLocAssign", "checkValidLocation", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Reprocesses transactions that could not be placed
    ''' </summary>
    ''' <param name="AutoCmpShort">If "Yes" it will mark reprocessed transactions as complete.</param>
    ''' <param name="OTID">ID of the transaction to reprocess from open transactions.</param>
    ''' <param name="OrderCount">Number of order lines in this order.</param>
    ''' <param name="SplitID">ID of the transaction that this was split on.</param>
    ''' <param name="user">User processing replenishments</param>
    ''' <param name="WSID">Workstation ID of user</param>
    ''' <remarks></remarks>
    Private Shared Sub ReProcess(AutoCmpShort As String, OTID As Integer, OrderCount As Integer, SplitID As Integer, user As String, WSID As String)
        If AutoCmpShort = "Yes" Then
            Try
                RunActionSP("updateOTReplenAutoComplete", WSID, {{"@OTID", OTID, intVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "ReProcess", ex.ToString(), user, WSID)
            End Try
        Else ' No Locations available, send to reprocess table
            Try
                RunActionSP("insOTTempReplen", WSID, {{"@OTID", OTID, intVar}, {"@OrderCount", OrderCount, intVar}, {"@SplitID", SplitID, intVar}, {"@User", user, strVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "ReProcess", ex.ToString(), user, WSID)
            End Try
        End If
    End Sub

    Private Shared Sub NextLocType(x As Integer, ByRef LocType As String, ByRef LocType1 As String, ByRef LocType2 As String)
        If x = 1 Then
            LocType = LocType1
            If LocType = nullVar Then
                LocType = "Bulk"
            End If
        Else
            LocType = LocType2
            If LocType = nullVar Then
                LocType = "Carton Flow"
            End If
        End If
    End Sub

    Private Shared Sub updateOTReplen(ByRef MaximumQty As Integer, ByRef LocType As String, ByRef MaxQty1 As Integer, ByRef CarMaxQty As Integer, ByRef CarMinQty As Integer, _
                                      ByRef BulkMaxQty As Integer, ByRef BulkMinQty As Integer, ByRef CFMaxQty As Integer, ByRef CFMinQty As Integer, ByRef ItemNumber As String, _
                                      ByRef InvMapID As Integer, ByRef MinQty As Integer, ByRef Item As String, ByRef warehouse As String, ByRef LotNum As String, _
                                      ByRef SerNum As String, Descr As String, UM As String, ExpDate As String, Zone As String, Carousel As String, Row As String, _
                                      Shelf As String, Bin As String, ByRef OrderCount As Integer, OTID As Integer, ByRef AvailQty As Integer, user As String, WSID As String)
        If MaximumQty = 0 Then
            If LocType.ToLower() = "carousel" And CarMaxQty > 0 Then
                MaxQty1 = CarMaxQty
            ElseIf LocType.ToLower() = "bulk" And BulkMaxQty > 0 Then
                MaxQty1 = BulkMaxQty
            ElseIf LocType.ToLower() = "carton flow" And CFMaxQty > 0 Then
                MaxQty1 = CFMaxQty
            Else
                MaxQty1 = 0
            End If

            Try
                RunActionSP("HPAUpdateMaxQty", WSID, {{"@MaxQty", MaxQty1, intVar}, {"@MinQty", MinQty, intVar}, _
                                                      {"@InvMapID", InvMapID, intVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "StartLoc", ex.ToString(), user, WSID)
            End Try
        End If

        If ItemNumber = nullVar Or ItemNumber = "" Then
            Try
                RunActionSP("HPAUpdateItemInfo", WSID, {{"@InvMapID", InvMapID, intVar}, _
                                                        {"@Whse", warehouse, strVar}, {"@Item", Item, strVar}, {"@Lot", LotNum, strVar}, _
                                                        {"@Ser", SerNum, strVar}, {"@Descr", Descr, strVar}, _
                                                        {"@UM", UM, strVar}, _
                                                        {"@Exp", ExpDate, dteVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "StartLoc", ex.ToString(), user, WSID)
            End Try
        End If

        ' update open transactions
        Try
            RunActionSP("updateOTReplenAssign", WSID, {{"@Location", _
                                                        IIf(Zone = nullVar, "", Zone) & _
                                                        IIf(Carousel = nullVar, "", Carousel) & _
                                                        IIf(Row = nullVar, "", Row) & _
                                                        IIf(Shelf = nullVar, "", Shelf) & _
                                                        IIf(Bin = nullVar, "", Bin), _
                                                        strVar}, _
                                                       {"@Zone", Zone, strVar}, _
                                                       {"@Carousel", Carousel, strVar}, _
                                                       {"@Row", Row, strVar}, _
                                                       {"@Shelf", Shelf, strVar}, _
                                                       {"@Bin", Bin, strVar}, _
                                                       {"@InvMapID", InvMapID, intVar}, _
                                                       {"@LotNum", LotNum, strVar}, _
                                                       {"@SerNum", SerNum, strVar}, _
                                                       {"@ExpDate", ExpDate, dteVar}, _
                                                       {"@LineNum", OrderCount, intVar}, _
                                                       {"@OTID", OTID, intVar}, {"@TransQty", AvailQty, intVar}})

        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "StartLoc", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Gets All Open Transactions that are marked as replenishment, and have no completed date or location
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="wsid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getOTPickReplens(user As String, wsid As String) As List(Of List(Of String))
        Dim OTReader As SqlDataReader = Nothing
        Dim row As New List(Of String)
        Dim rst1OT As New List(Of List(Of String))
        Try
            OTReader = RunSPArray("selOpenTransReplenishments", wsid, {{"nothing"}})
            If OTReader.HasRows Then
                While OTReader.Read()
                    For x As Integer = 0 To OTReader.FieldCount - 1
                        row.Add(IIf(IsDBNull(OTReader(x)), nullVar, OTReader(x)))
                    Next
                    rst1OT.Add(row)
                    row = New List(Of String)
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "LocReplenPick, SP: selOpenTransReplenishments", ex.ToString(), user, wsid)
        Finally
            If Not IsNothing(OTReader) Then
                OTReader.Dispose()
                OTReader = Nothing
            End If
        End Try
        Return rst1OT
    End Function

    ''' <summary>
    ''' Assigns Locations for Replenishments, splitting records as needed
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting workstation</param>
    ''' <remarks></remarks>
    Private Shared Sub LocReplenPick(user As String, WSID As String)
        Dim rst1OT As New List(Of List(Of String)), row As New List(Of String), OTReader As SqlDataReader = Nothing
        Dim OTColumns As New List(Of String) From {"ID", "Import Date", "Import By", "Import Filename", "Transaction Type", "Order Number", "Line Number", "Line Sequence", "Priority", "Required Date", _
                                                   "Item Number", "Unit of Measure", "Lot Number", "Expiration Date", "Serial Number", "Description", "Revision", "Transaction Quantity", _
                                                   "Location", "Warehouse", "Zone", "Carousel", "Row", "Shelf", "Bin", "Inv Map ID", "Completed Date", "Completed By", "Completed Quantity", _
                                                   "Batch Pick ID", "Notes", "Export File Name", "Export Date", "Exported By", "Export Batch ID", "Table Type", "Status Code", "Master Record", _
                                                   "Master Record ID", "Label", "In Progress", "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7", _
                                                   "User Field8", "User Field9", "User Field10", "Tote ID", "Tote Number", "Cell", "Host Transaction ID", "Emergency", "Sub Category", "FIFO", _
                                                   "FIFO Date", "Pick Fence Qty", "Split Case", "Case Quantity"}
        Dim LastOrder As String = "None", NewOrder As String = "None", OrderCount As Integer = 0, FIFO As Integer = 0, LAPickSort As String = GetSettingLM("PickPro", "Pick Preferences", "LocAss Pick Sort", user, WSID), ReasonText As String = ""
        Dim rst2InvMapColumns As New List(Of String) From {"Inv Map ID", "Location ID", "Location", "Warehouse", "Location Number", "Reel Location Number", "Zone", "Carousel", "Row", "Shelf", _
                                                           "Bin", "Item Number", "Revision", "Serial Number", "Lot Number", "Expiration Date", "Description", "Item Quantity", "Unit of Measure", _
                                                           "Maximum Quantity", "Cell Size", "Golden Zone", "Put Away Date", "User Field1", "User Field2", "Master Location", "Date Sensitive", _
                                                           "Dedicated", "Master Inv Map ID", "Min Quantity", "Quantity Allocated Pick", "Quantity Allocated Put Away", "AvailQty", "Replenishment Zone"}
        Dim rst2InvMap As New List(Of List(Of String)), InvMapReader As SqlDataReader = Nothing, PFChecked As String = "No", PF As Integer = 0
        Dim CaseQty As Integer = 0, SplitCase As Integer = 0, Item As Integer = 0, rst3 As SqlDataReader = Nothing, SPZone As Integer = 0, BulkLocQty As Integer = 0, ShortCaseQty As Integer = 0
        Dim SplitID As Integer = 0, SplitQty As Integer = 0, CasePickQty As Integer = 0, PickCaseQty As Integer = 0, AvailCaseQty As Integer = 0, PickQty As Integer = 0, InvMapIndex As Integer = 0


        If LAPickSort = "" Then
            LAPickSort = "Item Quantity"
            SaveSettingLM("PickPro", "Pick Preferences", "LocAss Pick Sort", LAPickSort, user, WSID)
        End If


        Try
            While True
                ' Get Open Transactions Records and read them into a list list string

                rst1OT = getOTPickReplens(user, WSID)

                Clients.Group(WSID).updateReplenishmentStatus(rst1OT.Count, "Processing Pick Replenishments")

                If (rst1OT.Count = 0) Then
                    Exit While
                End If

                Dim OTRow As List(Of String) = rst1OT(0)

                If LastOrder = OTRow(OTColumns.IndexOf("Order Number")) Then
                    OrderCount += 1
                Else
                    OrderCount = 1
                End If

                If LastOrder = "None" Then
                    LastOrder = OTRow(OTColumns.IndexOf("Order Number")) & " 1"
                Else
                    LastOrder = OTRow(OTColumns.IndexOf("Order Number")).Split(" ")(0) & " " & OrderCount
                End If

                If OTRow(OTColumns.IndexOf("FIFO")).ToLower() = "false" Then
                    FIFO = 0
                ElseIf OTRow(OTColumns.IndexOf("FIFO Date") = "Put Away Date") Then
                    FIFO = 1
                Else
                    FIFO = 2 ' Expiration Date
                End If
                ' Get Inventory Map records
                Try
                    rst2InvMap = New List(Of List(Of String))
                    InvMapReader = RunSPArray("LocAssPickInvMap", WSID, {{"@Whse", OTRow(OTColumns.IndexOf("Warehouse")), strVar}, _
                                                                         {"@FIFO", CastAsSqlBool(OTRow(OTColumns.IndexOf("FIFO"))), intVar}, _
                                                                         {"@Item", OTRow(OTColumns.IndexOf("Item Number")), strVar}, _
                                                                         {"@Lot", OTRow(OTColumns.IndexOf("Lot Number")), strVar}, _
                                                                         {"@Ser", OTRow(OTColumns.IndexOf("Serial Number")), strVar}, _
                                                                         {"@ExpDate", OTRow(OTColumns.IndexOf("Expiration Date")), dteVar}, _
                                                                         {"@PickSort", LAPickSort, strVar}, _
                                                                         {"@Replen", 1, intVar}, _
                                                                         {"@FIFOAcrossWarehouse", nullVar, nullVar} _
                                                                         })
                    row = New List(Of String)
                    If InvMapReader.HasRows Then
                        While InvMapReader.Read()
                            For x As Integer = 0 To InvMapReader.FieldCount - 1
                                row.Add(IIf(IsDBNull(InvMapReader(x)), nullVar, InvMapReader(x)))
                            Next
                            rst2InvMap.Add(row)
                            row = New List(Of String)
                        End While
                    End If
                    InvMapIndex = 0
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString())
                    insertErrorMessages("SystemReplenishmentLocationAssignment", "LocReplenPick, SP: LocAssPickInvMap", ex.ToString(), user, WSID)
                Finally
                    If Not IsNothing(InvMapReader) Then
                        InvMapReader.Dispose()
                        InvMapReader = Nothing
                    End If
                End Try

                ReasonText = "No Available Quantity in Stock to Allocate to this transaction."
                FIFONextLocation(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                                 rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                                 OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                                 SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                                 NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                                 OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
            End While
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "LocReplenPick, For Each OTRow", ex.ToString(), user, WSID)
        End Try
    End Sub

    Private Shared Sub FIFONextLocation(ByRef rst2InvMap As List(Of List(Of String)), ByRef rst3 As SqlDataReader, ByRef user As String, ByRef WSID As String, ByRef OTRow As List(Of String), _
                                       ByRef rst2InvMapColumns As List(Of String), ByRef InvMapIndex As Integer, ByRef BulkLocQty As Integer, ByRef OTColumns As List(Of String), _
                                        ByRef PFChecked As String, ByRef PF As Integer, ByRef SplitCase As Integer, ByRef Item As Integer, ByRef SPZone As Integer, _
                                        ByRef CaseQty As Integer, ByRef CasePickQty As Integer, ByRef ShortCaseQty As Integer, ByRef NewOrder As String, ByRef LastOrder As String, _
                                        ByRef SplitID As Integer, ByRef SplitQty As Integer, ByRef OrderCount As Integer, ByRef PickCaseQty As Integer, _
                                        ByRef AvailCaseQty As Integer, ByRef PickQty As Integer, ByRef ReasonText As String)
        Try
            If rst2InvMap.Count = 0 Or InvMapIndex = rst2InvMap.Count Then
                Try
                    RunActionSP("ReProcessOT", WSID, {{"@OTID", OTRow(OTColumns.IndexOf("ID")), intVar}, _
                                                          {"@Name", user, strVar}, _
                                                          {"@Msg", "Location Assignment Error", strVar}, _
                                                          {"@Reason", ReasonText, strVar}, _
                                                          {"@AutoCmp", "No", strVar}, _
                                                      {"@DateStamp", Now(), dteVar}})
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString())
                    insertErrorMessages("SystemReplenishmentLocationAssignment", "FIFONextLocation, SP: ReProcessOT", ex.ToString(), user, WSID)
                End Try
                Return
            End If

            PFChecked = "No"
            PF = getOTValueAsInt(OTRow(OTColumns.IndexOf("Pick Fence Qty")), "PF: 1106")
            SplitCase = getOTValueAsInt(OTRow(OTColumns.IndexOf("Split Case")), "SplitCase: 1107")

            If SplitCase = 1 Then
                CaseQty = 1
            Else
                CaseQty = getOTValueAsInt(OTRow(OTColumns.IndexOf("Case Quantity")), "CaseQty: 1112")
                If CaseQty = 0 Then
                    SplitCase = 1
                End If
            End If

            Item = getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity")), "Item: 1118")

            Try
                rst3 = RunSPArray("selCarouselByZone", WSID, {{"@Zone", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")), strVar}})

                If rst3.HasRows Then
                    If rst3.Read() Then
                        SPZone = IIf(IsDBNull(rst3(0)), 0, getOTValueAsInt(rst3(0), "SPZone: 1125"))
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "FIFONextLocation, SP: selCarouselByZone", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(rst3) Then
                    rst3.Dispose()
                    rst3 = Nothing
                End If
            End Try

            BulkLocQty = getOTValueAsInt(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Item Quantity")), "BulkLocQty: 1138") _
                - getOTValueAsInt(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Quantity Allocated Pick")), "QtyAllocPick: 1139")
            CasePickQty = getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity")), "CasePickQty: 1140")
            ShortCaseQty = 0
            Call PickFence(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                                 rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                                 OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                                 SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                                 NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                                 OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("SystemReplenishmentLocationAssignment", "FIFONextLocation, outer try/catch block", ex.ToString(), user, WSID)
        End Try
    End Sub

    Private Shared Sub PickFence(ByRef rst2InvMap As List(Of List(Of String)), ByRef rst3 As SqlDataReader, ByRef user As String, ByRef WSID As String, ByRef OTRow As List(Of String), _
                                       ByRef rst2InvMapColumns As List(Of String), ByRef InvMapIndex As Integer, ByRef BulkLocQty As Integer, ByRef OTColumns As List(Of String), _
                                        ByRef PFChecked As String, ByRef PF As Integer, ByRef SplitCase As Integer, ByRef Item As Integer, ByRef SPZone As Integer, _
                                        ByRef CaseQty As Integer, ByRef CasePickQty As Integer, ByRef ShortCaseQty As Integer, ByRef NewOrder As String, ByRef LastOrder As String, _
                                        ByRef SplitID As Integer, ByRef SplitQty As Integer, ByRef OrderCount As Integer, ByRef PickCaseQty As Integer, _
                                        ByRef AvailCaseQty As Integer, ByRef PickQty As Integer, ByRef ReasonText As String)

        If PF > 0 And PFChecked = "No" And CasePickQty >= PF Then
            ' end of records
            If InvMapIndex = rst2InvMap.Count Then
                ReasonText = "There was not enough quantity in bulk to satisfy the transaction quantity, which was set by the Pick Fence/Split Case settings in the Item Master."
                Call FIFONextLocation(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
                Return
            ElseIf SPZone = 1 Then
                ' rst2.MoveNext
                InvMapIndex += 1
                If InvMapIndex = rst2InvMap.Count Then
                    ReasonText = "There was not enough quantity in bulk to satisfy the transaction quantity, which was set by the Pick Fence/Split Case settings in the Item Master."
                    Call FIFONextLocation(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
                    Return
                Else
                    Try
                        rst3 = RunSPArray("selCarouselByZone", WSID, {{"@Zone", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")), strVar}})

                        If rst3.HasRows Then
                            If rst3.Read() Then
                                SPZone = IIf(IsDBNull(rst3(0)), 0, getOTValueAsInt(rst3(0)))
                            End If
                        End If
                    Catch ex As Exception
                        Debug.WriteLine(ex.ToString())
                        insertErrorMessages("SystemReplenishmentLocationAssignment", "PickFence, SP: selCarouselByZone", ex.ToString(), user, WSID)
                    Finally
                        If Not IsNothing(rst3) Then
                            rst3.Dispose()
                            rst3 = Nothing
                        End If
                    End Try
                    Call PickFence(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
                    Return
                End If
            End If
        End If
        Call AssignLocation(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
    End Sub

    Private Shared Sub AssignLocation(ByRef rst2InvMap As List(Of List(Of String)), ByRef rst3 As SqlDataReader, ByRef user As String, ByRef WSID As String, ByRef OTRow As List(Of String), _
                                       ByRef rst2InvMapColumns As List(Of String), ByRef InvMapIndex As Integer, ByRef BulkLocQty As Integer, ByRef OTColumns As List(Of String), _
                                        ByRef PFChecked As String, ByRef PF As Integer, ByRef SplitCase As Integer, ByRef Item As Integer, ByRef SPZone As Integer, _
                                        ByRef CaseQty As Integer, ByRef CasePickQty As Integer, ByRef ShortCaseQty As Integer, ByRef NewOrder As String, ByRef LastOrder As String, _
                                        ByRef SplitID As Integer, ByRef SplitQty As Integer, ByRef OrderCount As Integer, ByRef PickCaseQty As Integer, _
                                        ByRef AvailCaseQty As Integer, ByRef PickQty As Integer, ByRef ReasonText As String)
        If SplitCase = 0 Or (CaseQty > getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity"))) And SPZone = 0) Then
            Call NoSplitCase(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
        Else
            Call DoSplitCase(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
        End If
    End Sub

    Private Shared Sub DoSplitCase(ByRef rst2InvMap As List(Of List(Of String)), ByRef rst3 As SqlDataReader, ByRef user As String, ByRef WSID As String, ByRef OTRow As List(Of String), _
                                       ByRef rst2InvMapColumns As List(Of String), ByRef InvMapIndex As Integer, ByRef BulkLocQty As Integer, ByRef OTColumns As List(Of String), _
                                        ByRef PFChecked As String, ByRef PF As Integer, ByRef SplitCase As Integer, ByRef Item As Integer, ByRef SPZone As Integer, _
                                        ByRef CaseQty As Integer, ByRef CasePickQty As Integer, ByRef ShortCaseQty As Integer, ByRef NewOrder As String, ByRef LastOrder As String, _
                                        ByRef SplitID As Integer, ByRef SplitQty As Integer, ByRef OrderCount As Integer, ByRef PickCaseQty As Integer, _
                                        ByRef AvailCaseQty As Integer, ByRef PickQty As Integer, ByRef ReasonText As String)

        If rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("AvailQty")) >= getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity"))) Then
            NewOrder = LastOrder
            Try
                RunActionSP("updOTLAPickReplen", WSID, {{"@ID", getOTValueAsInt(OTRow(OTColumns.IndexOf("ID"))), intVar}, _
                                                        {"@Loc", _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin"))), _
                                                         strVar}, _
                                                        {"@Zone", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")), strVar}, _
                                                        {"@Carousel", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")), strVar}, _
                                                        {"@Row", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")), strVar}, _
                                                        {"@Shelf", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")), strVar}, _
                                                        {"@Bin", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")), strVar}, _
                                                        {"@IMID", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Inv Map ID")), intVar}, _
                                                        {"@Lot", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Lot Number")), strVar}, _
                                                        {"@Exp", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Expiration Date")), dteVar}, _
                                                        {"@Ser", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Serial Number")), strVar}, _
                                                        {"@LineNum", OrderCount, intVar}, {"@OrdNum", NewOrder, strVar}, _
                                                        {"@UF1", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field1")), strVar}, _
                                                        {"@UF2", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field2")), strVar}, _
                                                        {"@ReqDate", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Put Away Date")), dteVar}, _
                                                        {"@Qty", nullVar, nullVar}})
                Return
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "DoSplitCase, SP: updOTLAPickReplen avail>=trans", ex.ToString(), user, WSID)
            End Try
        Else
            SplitID = getOTValueAsInt(OTRow(OTColumns.IndexOf("Master Record ID")))
            SplitQty = getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity"))) _
                - getOTValueAsInt(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("AvailQty")))
            NewOrder = Left(LastOrder, InStr(1, LastOrder, " ") - 1)

            Try
                RunActionSP("insOTLASplitPickReplen", WSID, {{"@ID", getOTValueAsInt(OTRow(OTColumns.IndexOf("ID"))), intVar}, _
                                                             {"@SplitID", SplitID, intVar}, _
                                                             {"@Qty", SplitQty, intVar}, _
                                                             {"@LineNum", OrderCount + 1, intVar}, _
                                                             {"@OrdNum", NewOrder, strVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "DoSplitCase, SP: insOTLASplitPickReplen", ex.ToString(), user, WSID)
            End Try

            Try
                RunActionSP("updOTLAPickReplen", WSID, {{"@ID", getOTValueAsInt(OTRow(OTColumns.IndexOf("ID"))), intVar}, _
                                                        {"@Loc", _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin"))), _
                                                         strVar}, _
                                                        {"@Zone", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")), strVar}, _
                                                        {"@Carousel", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")), strVar}, _
                                                        {"@Row", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")), strVar}, _
                                                        {"@Shelf", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")), strVar}, _
                                                        {"@Bin", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")), strVar}, _
                                                        {"@IMID", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Inv Map ID")), intVar}, _
                                                        {"@Lot", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Lot Number")), strVar}, _
                                                        {"@Exp", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Expiration Date")), dteVar}, _
                                                        {"@Ser", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Serial Number")), strVar}, _
                                                        {"@LineNum", OrderCount, intVar}, _
                                                        {"@OrdNum", LastOrder, strVar}, _
                                                        {"@UF1", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field1")), strVar}, _
                                                        {"@UF2", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field2")), strVar}, _
                                                        {"@ReqDate", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Put Away Date")), dteVar}, _
                                                        {"@Qty", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("AvailQty")), intVar}})
                LastOrder = NewOrder
                Return
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "DoSplitCase, SP: updOTLAPickReplen in Else", ex.ToString(), user, WSID)
            End Try
        End If
    End Sub

    Private Shared Sub NoSplitCase(ByRef rst2InvMap As List(Of List(Of String)), ByRef rst3 As SqlDataReader, ByRef user As String, ByRef WSID As String, ByRef OTRow As List(Of String), _
                                       ByRef rst2InvMapColumns As List(Of String), ByRef InvMapIndex As Integer, ByRef BulkLocQty As Integer, ByRef OTColumns As List(Of String), _
                                        ByRef PFChecked As String, ByRef PF As Integer, ByRef SplitCase As Integer, ByRef Item As Integer, ByRef SPZone As Integer, _
                                        ByRef CaseQty As Integer, ByRef CasePickQty As Integer, ByRef ShortCaseQty As Integer, ByRef NewOrder As String, ByRef LastOrder As String, _
                                        ByRef SplitID As Integer, ByRef SplitQty As Integer, ByRef OrderCount As Integer, ByRef PickCaseQty As Integer, _
                                        ByRef AvailCaseQty As Integer, ByRef PickQty As Integer, ByRef ReasonText As String)

        ReasonText = "There was not enough quantity in bulk to satisfy the transaction quantity, which was set by the Pick Fence/Split Case settings in the Item Master."
        If CaseQty = 0 Then
            PickCaseQty = 0
            AvailCaseQty = 0
        Else
            PickCaseQty = getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity"))) / CaseQty
            If InvMapIndex = rst2InvMap.Count - 1 Then
                Call FIFONextLocation(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
            Else
                AvailCaseQty = getOTValueAsInt(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("AvailQty"))) / CaseQty
            End If
        End If

        If PickCaseQty = 0 Then
            InvMapIndex += 1
            Call FIFONextLocation(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
        Else
            PickCaseQty = PickCaseQty * CaseQty
        End If
        If AvailCaseQty = 0 Then
            InvMapIndex += 1
            Call PickFence(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
        Else
            AvailCaseQty = AvailCaseQty * CaseQty
        End If
        If PickCaseQty <= AvailCaseQty Then
            PickQty = AvailCaseQty
        Else
            PickQty = AvailCaseQty
        End If
        Call PickFullCases(rst2InvMap:=rst2InvMap, rst3:=rst3, user:=user, WSID:=WSID, OTRow:=OTRow, _
                             rst2InvMapColumns:=rst2InvMapColumns, InvMapIndex:=InvMapIndex, BulkLocQty:=BulkLocQty, _
                             OTColumns:=OTColumns, PFChecked:=PFChecked, PF:=PF, SplitCase:=SplitCase, Item:=Item, _
                             SPZone:=SPZone, CaseQty:=CaseQty, CasePickQty:=CasePickQty, ShortCaseQty:=ShortCaseQty,
                             NewOrder:=NewOrder, LastOrder:=LastOrder, SplitID:=SplitID, SplitQty:=SplitQty, _
                             OrderCount:=OrderCount, ReasonText:=ReasonText, PickCaseQty:=PickCaseQty, AvailCaseQty:=AvailCaseQty, PickQty:=PickQty)
    End Sub
    Private Shared Sub PickFullCases(ByRef rst2InvMap As List(Of List(Of String)), ByRef rst3 As SqlDataReader, ByRef user As String, ByRef WSID As String, ByRef OTRow As List(Of String), _
                                       ByRef rst2InvMapColumns As List(Of String), ByRef InvMapIndex As Integer, ByRef BulkLocQty As Integer, ByRef OTColumns As List(Of String), _
                                        ByRef PFChecked As String, ByRef PF As Integer, ByRef SplitCase As Integer, ByRef Item As Integer, ByRef SPZone As Integer, _
                                        ByRef CaseQty As Integer, ByRef CasePickQty As Integer, ByRef ShortCaseQty As Integer, ByRef NewOrder As String, ByRef LastOrder As String, _
                                        ByRef SplitID As Integer, ByRef SplitQty As Integer, ByRef OrderCount As Integer, ByRef PickCaseQty As Integer, _
                                        ByRef AvailCaseQty As Integer, ByRef PickQty As Integer, ByRef ReasonText As String)
        If getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity"))) = PickQty Then
            NewOrder = LastOrder
            Try
                RunActionSP("updOTLAPickReplen", WSID, {{"@ID", getOTValueAsInt(OTRow(OTColumns.IndexOf("ID"))), intVar}, _
                                                        {"@Loc", _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin"))), _
                                                         strVar}, _
                                                        {"@Zone", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")), strVar}, _
                                                        {"@Carousel", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")), strVar}, _
                                                        {"@Row", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")), strVar}, _
                                                        {"@Shelf", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")), strVar}, _
                                                        {"@Bin", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")), strVar}, _
                                                        {"@IMID", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Inv Map ID")), intVar}, _
                                                        {"@Lot", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Lot Number")), strVar}, _
                                                        {"@Exp", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Expiration Date")), dteVar}, _
                                                        {"@Ser", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Serial Number")), strVar}, _
                                                        {"@LineNum", OrderCount, intVar}, _
                                                        {"@OrdNum", NewOrder, strVar}, _
                                                        {"@UF1", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field1")), strVar}, _
                                                        {"@UF2", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field2")), strVar}, _
                                                        {"@ReqDate", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Put Away Date")), dteVar}, _
                                                        {"@Qty", nullVar, nullVar}})
                Return
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "PickFullCases, SP: updOTLAPickReplen", ex.ToString(), user, WSID)
            End Try
        Else
            SplitID = getOTValueAsInt(OTRow(OTColumns.IndexOf("Master Record ID")))
            SplitQty = getOTValueAsInt(OTRow(OTColumns.IndexOf("Transaction Quantity"))) - PickQty
            NewOrder = LastOrder
            Try
                RunActionSP("insOTLASplitPickReplen", WSID, {{"@ID", getOTValueAsInt(OTRow(OTColumns.IndexOf("ID"))), intVar}, _
                                                             {"@SplitID", SplitID, intVar}, _
                                                             {"@Qty", SplitQty, intVar}, _
                                                             {"@LineNum", OrderCount, intVar}, _
                                                             {"@OrdNum", NewOrder, strVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "PickFullCases, SP: insOTLASplitPickReplen", ex.ToString(), user, WSID)
            End Try
            Try
                RunActionSP("updOTLAPickReplen", WSID, {{"@ID", getOTValueAsInt(OTRow(OTColumns.IndexOf("ID"))), intVar}, _
                                                        {"@Loc", _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf"))) & _
                                                            IIf(rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")) = nullVar, "", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin"))), _
                                                         strVar}, _
                                                        {"@Zone", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Zone")), strVar}, _
                                                        {"@Carousel", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Carousel")), strVar}, _
                                                        {"@Row", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Row")), strVar}, _
                                                        {"@Shelf", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Shelf")), strVar}, _
                                                        {"@Bin", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Bin")), strVar}, _
                                                        {"@IMID", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Inv Map ID")), intVar}, _
                                                        {"@Lot", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Lot Number")), strVar}, _
                                                        {"@Exp", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Expiration Date")), dteVar}, _
                                                        {"@Ser", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Serial Number")), strVar}, _
                                                        {"@LineNum", OrderCount, intVar}, _
                                                        {"@OrdNum", NewOrder, strVar}, _
                                                        {"@UF1", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field1")), strVar}, _
                                                        {"@UF2", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("User Field2")), strVar}, _
                                                        {"@ReqDate", rst2InvMap(InvMapIndex)(rst2InvMapColumns.IndexOf("Put Away Date")), dteVar}, _
                                                        {"@Qty", PickQty, intVar}})
                Return
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("SystemReplenishmentLocationAssignment", "PickFullCases, SP: updOTLAPickReplen", ex.ToString(), user, WSID)
            End Try
        End If
    End Sub


End Class
