' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Induction
    Public Class InductionLocAss
        ''' <summary>
        ''' Indicates the state of the search for a valid location.
        ''' </summary>
        ''' <remarks></remarks>
        Private Enum LocationFound
            Found ' valid location, we can assign here.
            Invalid ' invalid location, we should continue looking for a valid location if there is one.
            MoreRecords ' there are more records and we have not found a valid location.
            ErrorThrown ' there was an error during location assignment, so we will exit at the first chance and alert the user to the issue.
        End Enum

        ''' <summary>
        ''' Finds a location that can be assigned to a specified transaction with the specified parameters.
        ''' Assigns a temporary item to the selected valid location if one is found and clears out any other reservations by this workstation, so that the item may be "dedicated" more effectively and so that bad containers are not created.
        ''' </summary>
        ''' <param name="qtyPut"># of item to put away</param>
        ''' <param name="item">item number to put away</param>
        ''' <param name="ccell">The cell size value</param>
        ''' <param name="cvel">The velocity code value</param>
        ''' <param name="bcell">The bulk cell size value</param>
        ''' <param name="bvel">The bulk velocity code value</param>
        ''' <param name="cfcell">The carton flow cell size value</param>
        ''' <param name="cfvel">The carton flow velocity code value</param>
        ''' <param name="whse">The warehouse value</param>
        ''' <param name="dateSens">The date sensitive value</param>
        ''' <param name="FIFO">The FIFO value</param>
        ''' <param name="isReel">The isReel value</param>
        ''' <param name="lot">The lot number value</param>
        ''' <param name="ser">The serial number value</param>
        ''' <param name="replenfwd">The replen forward value</param>
        ''' <param name="prevZone">The previous zone value</param>
        ''' <param name="dedicate">IF the transaction is dedicated</param>
        ''' <param name="RTS">The RTS value</param>
        ''' <param name="WSID">The workstation that is currenlty being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>An object that contains the location if one is found</returns>
        ''' <remarks></remarks>
        Public Shared Function AssignPutAwayLocation(ByVal qtyPut As Integer, ByVal item As String, ByVal ccell As String, ByVal cvel As String, ByVal bcell As String, ByVal bvel As String,
                                                     ByVal cfcell As String, ByVal cfvel As String, ByVal whse As String, ByVal dateSens As Boolean, ByVal FIFO As Boolean,
                                                     ByVal isReel As Boolean, ByVal lot As String, ByVal ser As String, ByVal replenfwd As Boolean, ByVal prevZone As String,
                                                     ByVal dedicate As Boolean, ByVal RTS As Boolean, ByVal expDate As String, ByVal WSID As String, ByVal user As String,
                                                     PrimaryZone As String, SecondaryZone As String) As Object
            If qtyPut <= 0 Then qtyPut = 1
            Dim location As Object = New With {.Success = False, .Message = "No available locations found.", .Zone = "", .Row = "", .Shelf = "", .Bin = "", .Whse = "", .Carousel = "", .InvMapID = 0, .LocMaxQty = 0,
                                               .LocQty = 0, .QtyAlloc = 0, .VelCode = "", .CellSz = "", .RequireDedicated = False}
            If Not ClearReservedNotComplete(prevZone, dedicate, user, WSID) Then
                location.Success = False
                location.Message = "Error while clearing incomplete location dedicate records."
                Return location
            End If
            If RTS Then FIFO = False
            If ser = "" Then ser = "0"
            If lot = "" Then lot = "0"
            Dim reader As SqlDataReader = Nothing
            Dim cfbulk As Object = CFBulkExist(user, WSID)
            Dim CFExists As Boolean = cfbulk.cf, BulkExists As Boolean = cfbulk.bulk

            Dim LocType As New List(Of String) From {If(PrimaryZone.ToLower() <> "", PrimaryZone.ToLower(), "carousel"), If(SecondaryZone.ToLower() <> "", SecondaryZone.ToLower(), "bulk")}
            'GetLocTypes(item, user, WSID)

            If Not LocType.Contains("carousel") Then
                LocType.Add("carousel")
            ElseIf Not LocType.Contains("bulk") Then
                LocType.Add("bulk")
            ElseIf Not LocType.Contains("carton flow") Then
                LocType.Add("carton flow")
            End If

            Dim currentVelocity As String, currentCell As String
            Dim cellvel As New Dictionary(Of String, Object) From {
                {"bulk", New With {.cell = bcell, .vel = bvel}},
                {"carousel", New With {.cell = ccell, .vel = cvel}},
                {"carton flow", New With {.cell = cfcell, .vel = cfvel}}}
            Dim dynwhse As Boolean = False

            Dim availQty As Integer = 0
            Dim result As LocationFound = LocationFound.MoreRecords
            For x As Integer = 0 To 2
                LocType(0) = LocType(x)
                ' don't check location types we do not have.
                If (LocType(0).ToLower() = "carton flow" And Not CFExists) Or (LocType(0).ToLower() = "bulk" And Not BulkExists) Then Continue For
                currentCell = cellvel.Item(LocType(0)).cell
                currentVelocity = cellvel.Item(LocType(0)).vel
                ' for matching cell + velocity on 1, anything else on 2
                For v As Integer = 1 To 2
                    Try
                        reader = RunSPArray("LocAssPutAway", WSID, {{"@DateSens", CastAsSqlBool(dateSens), intVar},
                                                                    {"@Whse", whse, strVar},
                                                                    {"@VelCode", currentVelocity, strVar},
                                                                    {"@CellSz", currentCell, strVar},
                                                                    {"@FIFO", CastAsSqlBool(FIFO), intVar},
                                                                    {"@Item", IIf(isReel, item & "0", item & lot & ser), strVar},
                                                                    {"@VSortBy", v, intVar},
                                                                    {"@DynWhse", IIf(Not dynwhse, "Yes", "No"), strVar},
                                                                    {"@InvMapID", "0", intVar},
                                                                    {"@WPZ", "0", strVar},
                                                                    {"@Reel", CastAsSqlBool(isReel), intVar},
                                                                    {"@Replenish", CastAsSqlBool(replenfwd), intVar},
                                                                    {"@LocType", LocType(0), strVar},
                                                                    {"@ExpDate", expDate, dteVar}})

                        If reader.HasRows Then
                            While reader.Read
                                If IsDBNull(reader("Available Qty")) Then
                                    availQty = 999999
                                Else
                                    availQty = CInt(reader("Available Qty"))
                                End If
                                '------------------------------------------------------------------------------------------------------------------
                                ' this could be easily replaced by a refactor in locassputaway to include [Dynamic warehouse] from [location zones]
                                Dim dynreader As SqlDataReader = Nothing
                                Try
                                    dynreader = RunSPArray("selDynWhse", WSID, {{"@Zone", reader("Zone"), strVar}})
                                    If dynreader.HasRows Then
                                        If dynreader.Read Then
                                            If Not IsDBNull(dynreader) Then
                                                dynwhse = (dynreader(0) = 1)
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception
                                    Debug.Print(ex.Message)
                                    insertErrorMessages("InductionLocAss", "AssignPutAwayLocations: dynreader", ex.Message, user, WSID)
                                    location.Success = False
                                    location.Message = ex.Message
                                    result = LocationFound.ErrorThrown
                                Finally
                                    If Not IsNothing(dynreader) Then
                                        dynreader.Dispose()
                                    End If
                                End Try
                                If result = LocationFound.ErrorThrown Then Return location
                                '--------------------------------------------------------------------------------------------------------------------
                                If Not dynwhse And Not IsDBNull(reader("Warehouse")) AndAlso reader("Warehouse").ToString().ToLower() <> whse.ToLower() Then
                                    dynwhse = True

                                    ' goto startloc, try again, didn't find good location
                                    location.Success = False
                                    location.Message = "No available locations found."
                                    result = LocationFound.MoreRecords
                                ElseIf availQty >= qtyPut Then
                                    ' we've found a valid location, lets return it
                                    Dim MapMax As Integer = GetMapMax(LocType(0), item, user, WSID)
                                    ' if the location is not reserved for this item then we want to prevent put aways from being allocated here of a different item.
                                    ' reserve the location if it is available and unreserved.
                                    If IsDBNull(reader("Item Number")) OrElse reader("Item Number") = "" Then
                                        Try

                                            RunActionSP("updIMNewLocMachineName", WSID, {{"@Item", WSID & "-IM", strVar}, {"@rt", CastAsSqlBool(isReel), intVar}, {"@InvMapID", reader("Inv Map ID"), strVar}})
                                        Catch ex As Exception
                                            Debug.Print(ex.Message)
                                            insertErrorMessages("InductionLocAss", "AssignPutAwayLocation: updIMNewLocMachineName", ex.Message, user, WSID)
                                            result = LocationFound.ErrorThrown
                                        End Try
                                        If result = LocationFound.ErrorThrown Then Return location
                                    ElseIf reader("Item Number").ToLower() <> (WSID & "-IM").ToLower() And reader("Item Number").ToLower() <> item.ToLower() Then ' the location is also not already reserved by our workstation, so it is invalid
                                        ' this location is invalid because it is either already allocated to an item that we are not assigning or it is reserved by another workstation
                                        result = LocationFound.MoreRecords
                                        location.Message = "Location found, but is has already been reserved or allocated.  Continuing to search for a valid location."
                                        Continue While
                                    End If
                                    ' above if:  else would indicate that our workstation has already reserved this location and it is valid.

                                    Dim rd As Boolean = False
                                    If IsDBNull(reader("Item Number")) OrElse reader("Item Number").ToLower() <> item.ToLower() Then
                                        If isReel Then
                                            rd = True
                                        End If
                                    End If
                                    With location
                                        .Success = True
                                        .Message = ""
                                        .Zone = CheckDBNull(reader("Zone"))
                                        .Row = CheckDBNull(reader("Row"))
                                        .Whse = CheckDBNull(reader("Warehouse"))
                                        .Carousel = CheckDBNull(reader("Carousel"))
                                        .Shelf = CheckDBNull(reader("Shelf"))
                                        .Bin = CheckDBNull(reader("Bin"))
                                        .LocMaxQty = MapMax
                                        .InvMapID = IIf(IsDBNull(reader("Inv Map ID")), 0, reader("Inv Map ID"))
                                        .LocQty = IIf(IsDBNull(reader("Item Quantity")), 0, reader("Item Quantity"))
                                        .QtyAlloc = IIf((MapMax - availQty) < 0, 0, (MapMax - availQty))
                                        .VelCode = CheckDBNull(reader("Velocity Code"))
                                        .CellSz = CheckDBNull(reader("Cell Size"))
                                        .RequireDedicated = rd
                                    End With
                                    result = LocationFound.Found
                                Else
                                    ' next record
                                    location.Success = False
                                    location.Message = "No available locations found."
                                    result = LocationFound.MoreRecords
                                End If
                                If result = LocationFound.Found Then Exit While
                            End While
                        End If
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                        insertErrorMessages("InductionLocAss", "AssignPutAwayLocation: For v", ex.ToString(), user, WSID)
                        location.Success = False
                        location.Message = ex.Message
                        result = LocationFound.ErrorThrown
                    Finally
                        If Not IsNothing(reader) Then
                            reader.Dispose()
                            reader = Nothing
                        End If
                    End Try
                    If result = LocationFound.Found Then Return location
                Next v
            Next x
            Return location
        End Function

        ''' <summary>
        ''' Checks to see if there are zones without the carousel flag (bulk) and with the carton flow flag to identify the types of locations that we will be looking for.
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>An object that tells if there are zones that aremarked for carton flow but not carousel</returns>
        ''' <remarks></remarks>
        Public Shared Function CFBulkExist(user As String, WSID As String) As Object
            Dim reader As SqlDataReader = Nothing
            Dim exist As Object = New With {.cf = False, .bulk = False}
            Try
                reader = RunSPArray("selIMCFBulkExist", WSID, {{"nothing"}})
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            exist.cf = (reader(0) = 1)
                        End If
                    End If
                End If
                reader.NextResult()
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            exist.bulk = (reader(0) = 1)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("InductionLocAss", "CFBulkExist", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return exist
        End Function

        ''' <summary>
        ''' Clears incomplete transactions that an operator did not task complete.  When a location is assigned a reservation is made.  This clears any reservations that were pending/discarded.
        ''' </summary>
        ''' <param name="zone">The zone where the transactiosna re located</param>
        ''' <param name="dedicate">If the transactions are dedicated</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function ClearReservedNotComplete(zone As String, dedicate As Boolean, user As String, WSID As String) As Boolean
            Try
                RunActionSP("updIMUnreserve", WSID, {{"@WSID", WSID, strVar}, {"@Zone", zone, strVar}, {"@Dedicate", CastAsSqlBool(dedicate), intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("InductionLocAss", "ClearReservedNotComplete", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets an item number's primary and secondary pick zones.  The third location type is set as the tertiary choice.
        ''' </summary>
        ''' <param name="item">The item number to get the info for</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A likst of string that contains the zones</returns>
        ''' <remarks></remarks>
        Public Shared Function GetLocTypes(item As String, user As String, WSID As String) As List(Of String)
            Dim reader As SqlDataReader = Nothing
            ' we haven't iterated in the main loop yet, so we have not set a primary location type to look through first
            Dim locs As New List(Of String)
            Try
                reader = RunSPArray("selIMItemZones", WSID, {{"@Item", item, strVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        ' if there is not a primary pick zone then we default to carousel
                        If Not IsDBNull(reader("Primary Pick Zone")) Then
                            locs.Add(reader("Primary Pick Zone").ToString().ToLower())
                        Else
                            locs.Add("carousel")
                        End If
                        ' if there is not a secondary pick zone then we default to bulk
                        If Not IsDBNull(reader("Secondary Pick Zone")) Then
                            locs.Add(reader("Secondary Pick Zone").ToString().ToLower())
                        Else
                            locs.Add("bulk")
                        End If
                    End If
                End If
                ' assign the final location type that we may consider.
                If Not locs.Contains("carousel") Then
                    locs.Add("carousel")
                ElseIf Not locs.Contains("bulk") Then
                    locs.Add("bulk")
                ElseIf Not locs.Contains("carton flow") Then
                    locs.Add("carton flow")
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("InductionLocAss", "GetLocTypes", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return locs
        End Function

        ''' <summary>
        ''' Gets the inventory map max item quantity for the specified location and/or item number depending on location type and what data is available in the map and inventory master
        ''' </summary>
        ''' <param name="loctype">The desired location type</param>
        ''' <param name="item">The desired item number</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>An integer that contains the max item quantity size</returns>
        ''' <remarks></remarks>
        Public Shared Function GetMapMax(loctype As String, item As String, user As String, WSID As String) As Integer
            Dim reader As SqlDataReader = Nothing
            Dim max As Integer = 0
            Try
                reader = RunSPArray("selIMItemMaxQty", WSID, {{"@Item", item, strVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        Select Case loctype.ToLower()
                            Case "carousel"
                                If Not IsDBNull(reader("Car Max Qty")) Then max = CInt(reader("Car Max Qty"))
                            Case "bulk"
                                If Not IsDBNull(reader("Bulk Max Qty")) Then max = CInt(reader("Bulk Max Qty"))
                            Case "carton flow"
                                If Not IsDBNull(reader("CF Max Qty")) Then max = CInt(reader("CF Max Qty"))
                            Case Else
                                Throw New Exception("Unknown location type (" & loctype & ") in location assignment.")
                        End Select
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("InductionLocAss", "GetMapMax", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return max
        End Function
    End Class
End Namespace