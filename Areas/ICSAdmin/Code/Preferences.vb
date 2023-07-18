' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class Preferences
        ''' <summary>
        ''' Selects Order Sort table
        ''' </summary>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of string containing the possible sort orders</returns>
        ''' <remarks></remarks>
        Public Shared Function getOrderSort(user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, ordersort As New List(Of String)

            Try
                datareader = RunSPArray("selOrderSort", WSID, {{"nothing"}})

                If datareader.HasRows Then
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            ordersort.Add(datareader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getOrderSort", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return ordersort
        End Function
        ''' <summary>
        ''' Selects the values to be displayed in the pod id dropdwon
        ''' </summary>
        ''' <param name="user">The currently signed in user</param>
        ''' <param name="WSID">The workstation that is being worked ib</param>
        ''' <param name="SP">The stored procedure that is going to be exected</param>
        ''' <returns>A list of object containing the zone and the location name of the zone</returns>
        ''' <remarks></remarks>
        Public Shared Function selPodIDDropDown(user As String, WSID As String, SP As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing, podInfo As New List(Of Object), Zone As String = "", LocName As String = ""
            Try
                DataReader = RunSPArray(SP, WSID, {{"nothing"}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        Zone = IIf(Not IsDBNull(DataReader(0)), DataReader(0), "")
                        LocName = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                        podInfo.Add(New With {.zone = Zone, .locname = LocName})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "selPodIDDropDown", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return podInfo
        End Function

        ''' <summary>
        ''' Selects the dropdown information for the bulk zones dropdown
        ''' </summary>
        ''' <param name="user">The currently signed in user</param>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <returns>A list of object containing the zone and location name </returns>
        ''' <remarks></remarks>
        Public Shared Function selBulkZonesDropDown(wsid As String, user As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing, ZoneInfo As New List(Of Object), Zone As String = "", LocName As String = "", bulkList As List(Of String) = Preferences.selBulkZones(wsid, user)
            Dim isBulk As Boolean = True

            Try
                DataReader = RunSPArray("PrefSelBulkZonesDrop", wsid, {{"nothing"}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        Zone = IIf(Not IsDBNull(DataReader(0)), DataReader(0), "")
                        LocName = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                        If (bulkList.Contains(DataReader(0))) Then
                            isBulk = True
                        Else
                            isBulk = False
                        End If

                        ZoneInfo.Add(New With {.zone = Zone, .locname = LocName, .isbulk = isBulk})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("PreferencesHub", "selBulkZonesDropDown", ex.ToString(), user, wsid)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return ZoneInfo
        End Function

        ''' <summary>
        ''' Gets locations from the locations table
        ''' </summary>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string that contains all the locations</returns>
        ''' <remarks></remarks>
        Public Shared Function getLocations(user As String, WSID As String) As List(Of String)
            Dim locations As New List(Of String)
            Dim datareader As SqlDataReader = Nothing
            Try
                datareader = RunSPArray("selLocations", WSID, {{"nothing"}})

                If datareader.HasRows Then
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            locations.Add(datareader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getLocations", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return locations
        End Function

        ''' <summary>
        ''' Gets possible parent zones of other zones from location zones table
        ''' </summary>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string containing the parent zones</returns>
        ''' <remarks></remarks>
        Public Shared Function getParentZones(user As String, WSID As String) As List(Of String)
            Dim parents As New List(Of String)
            Dim datareader As SqlDataReader = Nothing

            Try
                datareader = RunSPArray("selParentZones", WSID, {{"nothing"}})

                If datareader.HasRows Then
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            parents.Add(datareader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getParentZones", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return parents
        End Function
        ''' <summary>
        ''' Selects the zones for the pod zones dropwdown 
        ''' </summary>
        ''' <param name="user">The current user signed in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list containing all the zones</returns>
        ''' <remarks></remarks>
        Public Shared Function selPodZonesDropDown(WSID As String, user As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing, Zones As New List(Of String), IsPodZone As Boolean = False
            Dim Quants As Integer = 0, PodZones As List(Of Object) = Preferences.selPodSetupPods(WSID, user), PodZoneInfo As New List(Of Object), CarPods As New List(Of String)

            Try
                DataReader = RunSPArray("PrefSelPODZonesDrop", WSID, {{"nothing"}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        If Not IsDBNull(DataReader(0)) Then
                            Zones.Add(DataReader(0))
                        End If
                    End While
                End If

                For Each c In PodZones
                    CarPods.Add(c.podid)
                Next

                If PodZones.Count > 0 Then
                    For Each c In Zones
                        If CarPods.IndexOf(c) >= 0 Then
                            Quants = PodZones(CarPods.IndexOf(c)).maxorders
                            IsPodZone = True
                        Else
                            IsPodZone = False
                            Quants = 0
                        End If
                        PodZoneInfo.Add(New With {.zone = c, .maxquants = Quants, .ispodzone = IsPodZone})
                    Next
                Else
                    For Each c In Zones
                        Quants = 0
                        IsPodZone = False
                        PodZoneInfo.Add(New With {.zone = c, .maxquants = Quants, .ispodzone = IsPodZone})
                    Next

                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "selPodZonesDropDown", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return PodZoneInfo
        End Function

        ''' <summary>
        ''' Gets the Scan Verify datatable data
        ''' </summary>
        ''' <param name="draw">DataTable parameter</param>
        ''' <param name="sRow">Start row</param>
        ''' <param name="eRow">End row</param>
        ''' <param name="column">Column as integer to sort on</param>
        ''' <param name="sortDir">Sort desc or asc</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A tableobject containing all the data needed for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getScanVerifyTable(draw As Integer, sRow As Integer, eRow As Integer, column As Integer, sortDir As String, user As String, WSID As String) As TableObject
            Dim recordsTotal As Integer = 0, row As New List(Of String), table As New List(Of List(Of String)), datareader As SqlDataReader = Nothing
            Dim columns As String() = {"Location", "Location Name", "Zone", "Carousel", "Row", "Shelf", "Bin", "Warehouse", "Cell Size", "Velocity Code", "Carousel Location", "Carton Flow Location", _
                                       "Item Number", "Description", "Serial Number", "Lot Number", "Expiration Date", "Unit of Measure", "Maximum Quantity", "Quantity Allocated Pick", _
                                       "Quantity Allocated Put Away", "Item Quantity", "Put Away Date", "Date Sensitive", "User Field1", "User Field2", "Dedicated", "Master Location", _
                                       "Inv Map ID"}

            If column < 0 Or column > UBound(columns) Then
                column = 0
            End If
            Try
                datareader = RunSPArray("selScanVerifyTable", WSID, {{"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, {"@column", columns.GetValue(column), strVar}, {"@sortDir", sortDir, strVar}})

                If datareader.HasRows Then
                    If datareader.Read() Then
                        If Not IsDBNull(datareader(0)) Then
                            recordsTotal = CInt(datareader(0))
                        End If
                    End If
                    datareader.NextResult()
                    If datareader.HasRows Then
                        While datareader.Read()
                            For x As Integer = 0 To datareader.FieldCount - 1
                                row.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                            Next
                            table.Add(row)
                            row = New List(Of String)
                        End While
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getScanVerifyTable", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return New TableObject(draw, recordsTotal, recordsTotal, table)
        End Function

        ''' <summary>
        ''' Gets the Field Names for Scan Verify Setup
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string containing the sv field names</returns>
        ''' <remarks></remarks>
        Public Shared Function getSVFieldNames(user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, SV As New List(Of String)

            Try
                datareader = RunSPArray("selSVFieldnames", WSID, {{"nothing"}})

                If datareader.HasRows Then
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            SV.Add(datareader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getSVFieldNames", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return SV
        End Function

        ''' <summary>
        ''' Selects all the bulk zones for the workstation id
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <returns>A list consisting of all the zones</returns>
        ''' <remarks></remarks>
        Public Shared Function selBulkZones(wsid As String, user As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing, Zones As New List(Of String)

            Try
                DataReader = RunSPArray("PrefSelBulkZones", wsid, {{"@WSID", wsid, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            Zones.Add(IIf(Not IsDBNull(DataReader(x)), DataReader(x), ""))
                        Next
                    End While
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("PreferencesHub", "selBulkZones", ex.ToString(), user, wsid)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return Zones

        End Function

        ''' <summary>
        ''' Selects the Pod Zone and maximum orders for that zone absed on the work station id
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of object containg the zone (pod id) and its respective max order</returns>
        ''' <remarks></remarks>
        Public Shared Function selPodSetupPods(WSID As String, user As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing, podData As New List(Of Object), PodID As String = "", MaxOrders As Integer = -1

            Try
                DataReader = RunSPArray("PrefSelPODZones", WSID, {{"@WSID", WSID, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        PodID = IIf(Not IsDBNull(DataReader(0)), DataReader(0), "")
                        MaxOrders = IIf(Not IsDBNull(DataReader(1)), DataReader(1), -1)
                        podData.Add(New With {.podid = PodID, .maxorders = MaxOrders})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("PreferencesHub", "selPodSetupPods", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return podData
        End Function

        ''' <summary>
        ''' Gets the Device Preferences table data
        ''' </summary>
        ''' <param name="draw">DataTables plugin parameter</param>
        ''' <param name="zone">Zone to filter on</param>
        ''' <param name="sortColumn">Column index to sort on</param>
        ''' <param name="sortOrder">ASC or DESC</param>
        ''' <param name="sRow">Start row</param>
        ''' <param name="eRow">End row</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A tableobject containing all the data needed for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getDevicePreferencesTable(draw As Integer, zone As String, sortColumn As Integer, sortOrder As String, sRow As Integer, eRow As Integer, user As String, WSID As String) As TableObject
            Dim datareader As SqlDataReader = Nothing, table As New List(Of List(Of String)), row As New List(Of String), recordsTotal As Integer = 0, recordsFiltered As Integer = 0
            Dim columns As String() = {"Zone", "Device Type", "Device Number", "Device Model", "Controller Type", "Controller Term Port", "Arrow Direction", "Light Direction", _
                                       "Laser Pointer", "Light Tree Number", "Begin Address", "Display Positions", "Display Characters"}
            If IsNothing(zone) Then
                zone = ""
            End If
            Try
                datareader = RunSPArray("selDevicePreferencesBuildTable", WSID, {{"@sRow", sRow, intVar}, {"@eRow", eRow, intVar}, {"@sortColumn", columns.GetValue(sortColumn), strVar},
                                                                 {"@sortOrder", sortOrder, strVar}, {"@Zone", zone, strVar}})
                Dim index As Integer = 0
                While datareader.HasRows
                    While datareader.Read()
                        If index = 0 Then
                            recordsTotal = datareader(0)
                        ElseIf index = 1 Then
                            recordsFiltered = datareader(0)
                        Else
                            For x As Integer = 0 To datareader.FieldCount - 1
                                row.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                            Next
                            table.Add(row)
                            row = New List(Of String)
                        End If
                    End While
                    index += 1
                    datareader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getDevicePreferencesTable", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return New TableObject(draw, recordsTotal, recordsFiltered, table)
        End Function

        ''' <summary>
        ''' Gets Location Zones.Zone with Carton Flow = 1
        ''' </summary>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of strirng containing all carton flow zones</returns>
        ''' <remarks></remarks>
        Public Shared Function getCartonFlowZones(user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, cfs As New List(Of String)

            Try
                datareader = RunSPArray("selCartonFlowZones", WSID, {{"nothing"}})

                If datareader.HasRows Then
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            cfs.Add(datareader(0))
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", WSID, "getCartonFlowZones", ex.ToString(), user)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return cfs
        End Function

        ''' <summary>
        ''' Device Preference Typeahead retrieval
        ''' </summary>
        ''' <param name="filter">Zone to filter on</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of string containing zones that being with the filter input</returns>
        ''' <remarks></remarks>
        Public Shared Function getDevPrefZoneTypeahead(filter As String, user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, zones As New List(Of String)

            Try
                datareader = RunSPArray("selZonesForDevPrefs", WSID, {{"@Zone", filter & "%", strVar}})

                While datareader.HasRows
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            zones.Add(datareader(0))
                        End If
                    End While
                    datareader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getDevPrefZoneTypeahead", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return zones
        End Function

        ''' <summary>
        ''' Gets the device preferences Unit of measure typeahead
        ''' </summary>
        ''' <param name="filter">Uom to filter on</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string containing unit of measures that being with fileter input</returns>
        ''' <remarks></remarks>
        Public Shared Function getDevPrefUoMTypeahead(filter As String, user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, UoM As New List(Of String)

            Try
                datareader = RunSPArray("selUoMDevicePreferences", WSID, {{"@UoM", filter & "%", strVar}})

                While datareader.HasRows
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            UoM.Add(datareader(0))
                        End If
                    End While
                    datareader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getDevPrefUoMTypeahead", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return UoM
        End Function

        ''' <summary>
        ''' Gets teh cell size typeahead for device preferences
        ''' </summary>
        ''' <param name="filter">cell size to filter on</param>
        ''' <param name="user">Requesting user</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string that contains cell sizes that begin with the input</returns>
        ''' <remarks></remarks>
        Public Shared Function getCellSizeTypeaheadDevPrefs(filter As String, user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, cells As New List(Of String)

            Try
                datareader = RunSPArray("selDevPrefsCellTypeahead", WSID, {{"@Cell", filter & "%", strVar}})

                While datareader.HasRows
                    While datareader.Read()
                        If Not IsDBNull(datareader(0)) Then
                            cells.Add(datareader(0))
                        End If
                    End While
                    datareader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getCellSizeTypeaheadDevPrefs", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return cells
        End Function

        ''' <summary>
        ''' Stops the Carousel Interface specified
        ''' </summary>
        ''' <param name="WSID">Workstation to stop carousel at</param>
        ''' <param name="Carousel">Carousel to stop</param>
        ''' <param name="user">Requesting user</param>
        ''' <remarks></remarks>
        Public Shared Sub StopCarouselInterface(WSID As String, Carousel As String, user As String)
            Try
                RunActionSP("updateCarouselState", WSID, {{"@WSID", WSID, strVar}, {"@Carousel", Carousel, strVar}})
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "StopCarouselInterface", ex.ToString(), user, WSID)
            End Try
        End Sub

        ''' <summary>
        ''' Gets the information for the workstation setup page
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="user">The user that is logged in</param>
        ''' <returns>An object that contains the preferences for workstation setup</returns>
        ''' <remarks></remarks>
        Public Shared Function getWorkstationSetup(WSID As String, user As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim index As Integer = 0
            'First section
            Dim PodID As String = ""
            Dim ScanPicks As Boolean = False
            Dim ScanPuts As Boolean = False
            Dim ScanCounts As Boolean = False
            Dim PrintRepLoc As String = ""
            Dim PrintLabLoc As String = ""
            Dim CartonFlowID As String = ""
            Dim QuickPick As String = ""
            Dim DefQuickPick As String = ""
            'Second Section
            Dim PickTotes As Boolean = False
            Dim PutTotes As Boolean = False
            Dim AutoPrintTote As Boolean = False
            Dim BatchPut As Boolean = False
            Dim AutoManifest As String = ""
            Dim CarManifest As String = ""
            Dim OffCarManifest As String = ""
            Dim BatchHotPut As String = ""
            'Third Section
            Dim LocAssOrderSel As Boolean = False
            Dim PrintReprocRep As Boolean = False
            Dim PrintPickLab As Boolean = False
            Dim PrintPickLabBatch As Boolean = False
            Dim ShowTransQtyBulk As String = ""
            Dim LocAssPickSort As String = ""
            Dim AutoCompBackOrder As String = ""
            Dim SapLocChange As String = ""

            Try
                DataReader = RunSPArray("PrefSelWorkstationSetup", WSID, {{"@WSID", WSID, strVar}})

                While DataReader.HasRows
                    While DataReader.Read()
                        'For x As Integer = 0 To DataReader.FieldCount - 1
                        If index = 0 Then
                            PodID = IIf(Not IsDBNull(DataReader(0)), DataReader(0), "")
                            ScanPicks = IIf(Not IsDBNull(DataReader(1)), DataReader(1), False)
                            ScanCounts = IIf(Not IsDBNull(DataReader(2)), DataReader(2), False)
                            ScanPuts = IIf(Not IsDBNull(DataReader(3)), DataReader(3), False)
                            PrintRepLoc = IIf(Not IsDBNull(DataReader(4)), DataReader(4), "")
                            PrintLabLoc = IIf(Not IsDBNull(DataReader(5)), DataReader(5), "")
                            CartonFlowID = IIf(Not IsDBNull(DataReader(6)), DataReader(6), "")
                        ElseIf index = 1 Then
                            If DataReader(0) = "Quick Pick" Then
                                QuickPick = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "Default Quick Pick" Then
                                DefQuickPick = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            End If
                        ElseIf index = 2 Then
                            PickTotes = IIf(Not IsDBNull(DataReader(0)), DataReader(0), False)
                            PutTotes = IIf(Not IsDBNull(DataReader(1)), DataReader(1), False)
                            AutoPrintTote = IIf(Not IsDBNull(DataReader(2)), DataReader(2), False)
                            BatchPut = IIf(Not IsDBNull(DataReader(3)), DataReader(3), False)
                        ElseIf index = 3 Then
                            If DataReader(0) = "Batch Hot Put Away" Then
                                BatchHotPut = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "Carousel Manifest" Then
                                CarManifest = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "Off Carousel Manifest" Then
                                OffCarManifest = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "Auto Tote Manifest" Then
                                AutoManifest = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            End If
                        ElseIf index = 4 Then
                            LocAssOrderSel = IIf(Not IsDBNull(DataReader(0)), DataReader(0), False)
                            PrintReprocRep = IIf(Not IsDBNull(DataReader(1)), DataReader(1), False)
                            PrintPickLab = IIf(Not IsDBNull(DataReader(2)), DataReader(2), False)
                            PrintPickLabBatch = IIf(Not IsDBNull(DataReader(3)), DataReader(3), False)
                        ElseIf index = 5 Then
                            If DataReader(0) = "LocAss Pick Sort" Then
                                LocAssPickSort = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "Show Trans Qty Bulk" Then
                                ShowTransQtyBulk = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "Auto Complete Backorders" Then
                                AutoCompBackOrder = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            ElseIf DataReader(0) = "SAP Location Change" Then
                                SapLocChange = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                            End If
                        End If
                        'Next
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("PreferencesHub", "selectWorkstationSetupInfo", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return New With {.wssett = New With {.podid = PodID, .scanpicks = ScanPicks, .scancounts = ScanCounts, .scanputs = ScanPuts, .cfid = CartonFlowID,
                                                 .quickpick = QuickPick, .defquickpick = DefQuickPick, .printreploc = PrintRepLoc, .printlabloc = PrintLabLoc},
                             .toteman = New With {.batchhotput = BatchHotPut, .picktote = PickTotes, .puttote = PutTotes, .carmanifest = CarManifest, .offcarmanifest = OffCarManifest,
                                                  .autoprintoffcar = AutoManifest, .autoprinttote = AutoPrintTote, .batchput = BatchPut},
                             .locassfun = New With {.picksort = LocAssPickSort, .qtybulk = ShowTransQtyBulk, .ordersel = LocAssOrderSel, .printreprocrep = PrintReprocRep,
                                                    .autobackorder = AutoCompBackOrder, .saploc = SapLocChange, .printpicklab = PrintPickLab, .printbatch = PrintPickLabBatch}}
        End Function

        ''' <summary>
        ''' Gets the light tree zones
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The suer that is currently loggged in</param>
        ''' <returns>A list of object containing the zone and its name</returns>
        ''' <remarks></remarks>
        Public Shared Function getLightTreeZones(WSID As String, User As String) As List(Of Object)
            Dim data As New List(Of Object)
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selLocZonesLightTree", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        data.Add(New With {.Zone = DataReader(0), .Name = DataReader(1)})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getLightTreeZones", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return data
        End Function

        ''' <summary>
        ''' Gets the light tree carousels
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>A list of string that contains the carousels for the light trees</returns>
        ''' <remarks></remarks>
        Public Shared Function getLightTreeCars(WSID As String, User As String) As List(Of String)
            Dim data As New List(Of String)
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selInvMapLightTreeCar", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            data.Add(DataReader(x))
                        Next
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getLightTreeCars", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return data
        End Function

        ''' <summary>
        ''' Gets the light tree bin locations
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>A list of string that contains the bin locations for the light trees</returns>
        ''' <remarks></remarks>
        Public Shared Function getLightTreeBinLoc(WSID As String, User As String) As List(Of String)
            Dim data As New List(Of String)
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selInvMapLightTreeBinLoc", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            data.Add(DataReader(x))
                        Next
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "getLightTreeBinLoc", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return data
        End Function
    End Class
End Namespace

