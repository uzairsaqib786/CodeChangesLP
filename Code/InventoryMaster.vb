' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Public Class InventoryMaster

    ''' <summary>
    ''' Gets the Item Number typeahead for Inventory Master
    ''' </summary>
    ''' <param name="stockCode">User input as Item Number</param>
    ''' <param name="user">Requesting user</param>
    ''' <returns>A list of itemnumbers and some of their fields that begin wiht the given filter</returns>
    ''' <remarks></remarks>
    Public Shared Function stockCodeTypeahead(stockCode As String, user As String, WSID As String) As List(Of InventoryMasterTypeaheadModel)
        Dim stockCodes As New List(Of String), DataReader As SqlDataReader = Nothing, returnObj As New List(Of InventoryMasterTypeaheadModel)

        Try
            DataReader = RunSPArray("selInventoryItemNumTA", WSID, {{"@ItemNum", stockCode & "%", strVar}})

            If DataReader.HasRows Then
                While DataReader.Read
                    returnObj.Add(New InventoryMasterTypeaheadModel(
                                  CheckDBNull(DataReader.Item("Item Number")), CheckDBNull(DataReader.Item("Description")),
                                  CheckDBNull(DataReader.Item("Category")), CheckDBNull(DataReader.Item("Category"))))

                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Inventory Master", "stockCodeTypeahead", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try

        Return returnObj
    End Function

    ''' <summary>
    ''' Selects the locations table in inventory master
    ''' </summary>
    ''' <param name="draw">DataTables parameter to track current request</param>
    ''' <param name="itemNum">Item Number</param>
    ''' <param name="sRow">Start row</param>
    ''' <param name="eRow">End row</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="sortColumnNum">Index of the sorted column</param>
    ''' <param name="sortOrder">ASC or DESC order for sorted column</param>
    ''' <returns>A tableobject containing the data needed for the datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function selInventoryMasterLocations(draw As Integer, itemNum As String, sRow As Integer, eRow As Integer, user As String, sortColumnNum As Integer, _
                                                       sortOrder As String, WSID As String) As TableObject
        Dim sort As String = ""
        Dim sortColumns As String() = {"Location", "Warehouse", "Zone", "Carousel", "Row", "Shelf", "Bin", "Lot Number", "Expiration Date", "Serial Number", "Cell Size", _
                                       "User Field1", "User Field2", "Quantity Allocated Pick", "Quantity Allocated Put Away", _
                                       "Unit of Measure", "Item Quantity", "Put Away Date", "Golden Zone"}

        sort = "[" & sortColumns(sortColumnNum) & "] " & sortOrder

        If IsNothing(itemNum) Then itemNum = ""
        Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Try
            table = GetJQueryDataTableResult(draw, "selVInvMapDT", WSID, user, {{"@ItemNum", itemNum, strVar}, {"@Start", sRow, intVar}, {"@End", eRow, intVar}, {"@Sort", sort, strVar}}, 0, 0, 1)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Inventory Master", "selInventoryMasterLocations", ex.ToString(), user, WSID)
        End Try

        Return table
    End Function

    ''' <summary>
    ''' Selects the Inventory Master information for editting
    ''' </summary>
    ''' <param name="itemNum">Item Number</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of object containing the data for the desired item number</returns>
    ''' <remarks></remarks>
    Public Shared Function selInventoryMasterData(itemNum As String, user As String, WSID As String) As List(Of Object)
        ' all number fields have a default of 0
        Dim DataReader As SqlDataReader = Nothing, TopPart As InvMastTopObj, SetupTab As InvMastItemSetupObj, KitTab As InvMastKitItemObj
        Dim ScanTab As InvMastScanCodeObj, OtherTab As InvMastOtherObj, ReelTab As InvMastReelTrackObj, WeighTab As InvMastWeighScaleObj, values As New List(Of String)
        Dim OpenCount As Integer = 0, HistCount As Integer = 0, ProcCount As Integer = 0, totalQuan As Integer = 0, totalPicks As Integer = 0, totalPuts As Integer = 0
        Dim innerKit As New List(Of String), innerScan As New List(Of String), kitvals As New List(Of List(Of String)), kititem As New List(Of String), descrip As New List(Of String)
        Dim kitqaun As New List(Of Integer), kitspecfeats As New List(Of String), scanvals As New List(Of List(Of String)), scancode As New List(Of String), scantype As New List(Of String), scanrange As New List(Of String)
        Dim scanpos As New List(Of Integer), scanlen As New List(Of Integer), Everything As New List(Of Object), index As Integer = 0, quarantine As Boolean = False
        Dim WipCount As Integer = 0

        Try
            DataReader = RunSPArray("selInventoryData", WSID, {{"@ItemNum", itemNum, strVar}})
            If Not DataReader.HasRows Then
                Return New List(Of Object) From {New With {.Empty = True}}
            End If
            While index < 9
                If DataReader.HasRows Then
                    While DataReader.Read()
                        ' setting some of the tabs 
                        If index = 0 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                values.Add(CheckDBNull(DataReader(x)))
                            Next
                        ElseIf index = 1 AndAlso Not IsDBNull(DataReader(0)) Then
                            OpenCount = DataReader(0)
                        ElseIf index = 2 AndAlso Not IsDBNull(DataReader(0)) Then
                            HistCount = DataReader(0)
                        ElseIf index = 3 AndAlso Not IsDBNull(DataReader(0)) Then
                            ProcCount = DataReader(0)
                        ElseIf index = 4 Then
                            If Not IsDBNull(DataReader(0)) Then totalQuan = DataReader(0)
                            If Not IsDBNull(DataReader(1)) Then totalPicks = DataReader(1)
                            If Not IsDBNull(DataReader(2)) Then totalPuts = DataReader(2)
                        ElseIf index = 5 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                innerKit.Add(CheckDBNull(DataReader(x)))
                            Next
                            kitvals.Add(innerKit)
                            innerKit = New List(Of String)
                        ElseIf index = 6 Then
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                innerScan.Add(CheckDBNull(DataReader(x)))
                            Next
                            scanvals.Add(innerScan)
                            innerScan = New List(Of String)
                        ElseIf index = 7 AndAlso Not IsDBNull(DataReader(0)) Then
                            quarantine = (DataReader(0) = 1)
                        ElseIf index = 8 AndAlso Not IsDBNull(DataReader(0)) Then
                            WipCount = DataReader(0)
                        End If
                    End While
                End If
                index += 1
                DataReader.NextResult()
            End While
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Inventory Master", "selInventoryMasterData", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
        TopPart = New InvMastTopObj(SC:=values(0), SID:=values(1), Desc:=values(2), Cat:=values(3), SubCat:=values(4), UnitMeas:=values(5), TotQty:=totalQuan, TotPicks:=totalPicks, TotPuts:=totalPuts, TotWip:=WipCount, _
                                    RepPoint:=CInt(values(6)), RepLevel:=CInt(values(7)), ReorPoint:=CInt(values(8)), ReorQuant:=CInt(values(9)), KanbanPoint:=CInt(values(44)), KanbanLevel:=CInt(values(45)))
        SetupTab = New InvMastItemSetupObj(DateSense:=CBool(values(10)), WareSense:=CBool(values(11)), FIFO:=CBool(values(12)), FIFODate:=values(42), PrimeZone:=values(13).ToLower(), SecZone:=values(14).ToLower(), PickFence:=CInt(values(15)), SplitCase:=CBool(values(16)),
                                           CaseQuant:=CInt(values(17)), PickSeq:=CInt(values(18)), Active:=CBool(values(19)), CarCell:=values(20), CarVel:=values(21), CarMin:=CInt(values(22)), CarMax:=CInt(values(23)),
                                           BulkCell:=values(24), BulkVel:=values(25), BulkMin:=CInt(values(26)), BulkMax:=CInt(values(27)), CfCell:=values(28), CfVel:=values(29), CfMin:=CInt(values(30)), CfMax:=CInt(values(31)),
                                           OpenQuan:=OpenCount, HistQuan:=HistCount, ProcQuan:=ProcCount)
        OtherTab = New InvMastOtherObj(UnitCost:=CDec(values(32)), SupplyID:=values(33), ManufactID:=values(34), SpecFeats:=values(35))
        ReelTab = New InvMastReelTrackObj(MinReelQaun:=CInt(values(36)), IncAutoUpd:=CBool(values(37)))
        WeighTab = New InvMastWeighScaleObj(UseScale:=CBool(values(38)), AvgPieceWeight:=CDec(values(39)), SampleQuan:=CInt(values(40)), MinUseScale:=CInt(values(41)))
        For x As Integer = 0 To kitvals.Count - 1
            kititem.Add(kitvals(x)(0))
            descrip.Add(kitvals(x)(1))
            kitqaun.Add(CInt(kitvals(x)(2)))
            kitspecfeats.Add(kitvals(x)(3))
        Next
        KitTab = New InvMastKitItemObj(ItemNum:=kititem, Desc:=descrip, KitQuan:=kitqaun, KitSpecFeats:=kitspecfeats)
        For x As Integer = 0 To scanvals.Count - 1
            scancode.Add(scanvals(x)(0))
            scantype.Add(scanvals(x)(1))
            scanrange.Add(scanvals(x)(2))
            scanpos.Add(CInt(scanvals(x)(3)))
            scanlen.Add(CInt(scanvals(x)(4)))
        Next
        ScanTab = New InvMastScanCodeObj(ScanCode:=scancode, ScanType:=scantype, ScanRange:=scanrange, StartPos:=scanpos, CodeLen:=scanlen)
        Everything.AddRange({TopPart, SetupTab, KitTab, ScanTab, OtherTab, ReelTab, WeighTab, New With {.Quarantined = quarantine}, New With {.model = values(43)}})
        Return Everything
    End Function

    ''' <summary>
    ''' Selects the next item number
    ''' </summary>
    ''' <param name="CurrItemNum">The item number that is currently selected</param>
    ''' <param name="filter">The filter that is currently applied ot the page</param>
    ''' <param name="direct">Tells the diirection (next or previous)</param>
    ''' <param name="User">The user that is logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A string containing the next item number</returns>
    ''' <remarks></remarks>
    Public Shared Function selectNextItemNum(CurrItemNum As String, filter As String, direct As Integer, User As String, WSID As String) As String
        Dim ItemNum As String = ""
        Try
            ItemNum = GetResultSingleCol("selInventoryItemNumFilt", WSID, {{"@ItemNumber", CurrItemNum, strVar}, {"@filter", filter, strVar}, _
                                                                       {"@FirstFilt", direct, intVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Inventory Master", "selectNextItemNum", ex.ToString(), User, WSID)
        End Try
        Return ItemNum
    End Function

    ''' <summary>
    ''' Gets the number of item numbers that are able to be selected
    ''' </summary>
    ''' <param name="ItemNumber">The current item number</param>
    ''' <param name="filter">The filter that is applied to the page</param>
    ''' <param name="User">The suer that is currently logged in</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>An object containing the position, total, and item number</returns>
    ''' <remarks></remarks>
    Public Shared Function selectItemNumFilterCount(ItemNumber As String, filter As String, User As String, WSID As String) As Object
        Dim DataReader As SqlDataReader = Nothing
        Dim CountData As New Object

        Try
            If ItemNumber Is Nothing Then ItemNumber = ""

            DataReader = RunSPArray("selInventoryItemNumFilterCount", WSID, {{"@ItemNumber", ItemNumber, strVar}, {"@filter", filter, strVar}})

            If DataReader.HasRows AndAlso DataReader.Read() Then
                CountData = New With {.Pos = CheckDBNull(DataReader(0)), .Total = CheckDBNull(DataReader(1)), .ItemNumber = CheckDBNull(DataReader(2))}
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Inventory Master", "selectItemNumFilterCount", ex.ToString(), User, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try

        Return CountData
    End Function
End Class