' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports combit.ListLabel21

Public Class GlobalHub
    Inherits Hub
    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
    ''' </summary>
    ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task
        ' checks if the print service is the one connecting
        Dim name As String = Context.QueryString.Get("currentUser")
        Dim WSID As String = Context.QueryString.Get("WSID")
        Dim CurrentPage As String = Context.QueryString.Get("CurrentPage")
        If CurrentPage <> "" Then
            Try
                Config.WorkstationCurrentPages(WSID).TryAdd(CurrentPage.ToLower, "")
            Catch ex As KeyNotFoundException
                ' this error is fine
            End Try
        End If
        If name = PrintService.PrintServiceHubName Then
            PrintService.PrintServiceRunning = True
            Clients.All.alertPrintServiceStateChange(True)
        End If
        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, name)
        Groups.Add(Context.ConnectionId, WSID)
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Captures user disconnects.  Ignores all users except the print service.  When the service is caught all clients are updated to the state of the print service.
    ''' </summary>
    ''' <param name="stopCalled"></param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Overrides Function OnDisconnected(stopCalled As Boolean) As Task
        Dim name As String = Context.QueryString.Get("currentUser")
        Dim WSID As String = Context.QueryString.Get("WSID")
        Dim CurrentPage As String = Context.QueryString.Get("CurrentPage")
        If CurrentPage <> "" Then
            Try
                If Config.WorkstationCurrentPages(WSID).ContainsKey(CurrentPage.ToLower) Then
                    Config.WorkstationCurrentPages(WSID).TryRemove(CurrentPage.ToLower, CurrentPage)
                End If
            Catch ex As KeyNotFoundException
                'fine
            End Try
        End If
        If name = PrintService.PrintServiceHubName Then
            PrintService.PrintServiceRunning = False
            Clients.All.alertPrintServiceStateChange(False)
        End If
        Return MyBase.OnDisconnected(stopCalled)
    End Function

    ''' <summary>
    ''' Sub to capture when a page is being left
    ''' </summary>
    Public Sub LeavingPage()
        Dim CurrentPage As String = Context.QueryString.Get("CurrentPage")
        Dim WSID As String = Context.QueryString.Get("WSID")
        If CurrentPage <> "" Then
            Try
                Config.WorkstationCurrentPages(WSID).TryRemove(CurrentPage.ToLower, CurrentPage)
            Catch ex As KeyNotFoundException
                ' this exception is okay
            End Try

        End If
    End Sub

    ''' <summary>
    ''' Checks if the print service is running
    ''' </summary>
    Public Sub PrintServiceCheckIn()
        PrintService.PrintServiceRunning = True
        PrintService.PrintServiceCheckinTime = Now
    End Sub

    Public Function CheckSTEService() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return STEService.ServiceStatus()
                                                 End Function)
    End Function

    ''' <summary>
    ''' Keeps users current.  Every time the global javascript timer expires it sends a command to this method to update user's most recent active action.
    ''' If a user hasn't called this method in a certain timeframe they will be forceably logged out and their session abandoned if it hasn't already been disposed.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub clientHeartbeat()
        Dim name As String = Context.QueryString.Get("currentUser")
        Dim WSID As String = Context.QueryString.Get("WSID")
        Dim AppName As String = Config.getFullAppName(Context.QueryString.Get("AppName"))
        If name <> "" AndAlso WSID <> "" AndAlso AppName <> "" AndAlso Config.AppUsers(AppName).ContainsKey(WSID) Then
            Config.AppUsers(AppName)(WSID).heartbeat = Now()
        End If
    End Sub

    ''' <summary>
    ''' Changes working status of the current user based on work parameter.
    ''' </summary>
    ''' <param name="work">Specifies whether the user is starting or ending work as a string ("Start Work" or "End Work").</param>
    ''' <returns>Task to prevent timeout issues.</returns>
    ''' <remarks></remarks>
    Public Function workClick(work As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             If work = "Start Work" Then
                                                 RunActionSP("insUserStats", Context.QueryString.Get("WSID"), {{"@username", Context.User.Identity.Name, strVar}})
                                             ElseIf work = "End Work" Then
                                                 RunActionSP("updUserStats", Context.QueryString.Get("WSID"), {{"@username", Context.User.Identity.Name, strVar}})
                                             Else
                                                 insertErrorMessages("GlobalHub", "workClick", "Hit else", Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End If
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("GlobalHub", "workClick", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try

                                     End Sub)
    End Function

    ''' <summary>
    ''' Returns Units of Measure from the database in list of string format.
    ''' </summary>
    ''' <returns>A list of string that contains the unit of measures</returns>
    ''' <remarks></remarks>
    Public Function getUnitsOfMeasure() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                             Return UnitOfMeasure.getUoM(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Deletes UoM from Unit of Measure table
    ''' </summary>
    ''' <param name="UoM">Unit of Measure to delete</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteUoM(UoM As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return UnitOfMeasure.delUoM(UoM, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates a OldValue to NewValue.
    ''' </summary>
    ''' <param name="OldValue">Old Unit of Measure value</param>
    ''' <param name="NewValue">New Unit of Measure value</param>
    ''' <returns>A boolean telling if the operation completed successsfully</returns>
    ''' <remarks></remarks>
    Public Function updateUoM(OldValue As String, NewValue As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return UnitOfMeasure.updUoM(OldValue, NewValue, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Saves a new Unit of Measure
    ''' </summary>
    ''' <param name="value">New Unit of Measure to save.</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveUoM(value As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return UnitOfMeasure.saveUoM(value, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets categories/subcategories via Hub connection
    ''' </summary>
    ''' <returns>A object that contains the categories adn subcategories</returns>
    ''' <remarks></remarks>
    Public Function getCategories() As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function()
                                                    Return Category.getCategories(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes a category/subcategory via hub connection
    ''' </summary>
    ''' <param name="cat">Category to delete</param>
    ''' <param name="subcat">Subcategory to delete</param>
    ''' <returns>T/F = Success/Failure</returns>
    ''' <remarks></remarks>
    Public Function deleteCategory(cat As String, subcat As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return Category.delCategory(Context.User.Identity.Name, cat, subcat, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Saves a category/sub via hub connection
    ''' </summary>
    ''' <param name="cat">Category to be saved.</param>
    ''' <param name="subcat">Subcategory to be saved.</param>
    ''' <param name="oldcat">Old category value to update.</param>
    ''' <param name="oldsub">Old subcategory value to update.</param>
    ''' <returns>T/F = success/failure</returns>
    ''' <remarks></remarks>
    Public Function saveCategory(cat As String, subcat As String, oldcat As String, oldsub As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return Category.saveCategory(cat, subcat, Context.User.Identity.Name, oldcat, oldsub, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets velocity codes from db.
    ''' </summary>
    ''' <returns>List of string of velocity codes</returns>
    ''' <remarks></remarks>
    Public Function getVelocityCodes() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                             Return VelocityCode.getVelocityCodes(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Saves a velocity code
    ''' </summary>
    ''' <param name="vel">Velocity code to save</param>
    ''' <param name="oldvel">Velocity code to alter if it's an edit, otherwise this should be set to ""</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveVelocityCode(vel As String, oldvel As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return VelocityCode.saveVelocityCode(Context.User.Identity.Name, vel, oldvel, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Deletes a velocity code
    ''' </summary>
    ''' <param name="vel">Velocity Code to delete.</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteVelocityCode(vel As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return VelocityCode.deleteVelocityCode(Context.User.Identity.Name, vel, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets all warehouses
    ''' </summary>
    ''' <returns>Warehouses in list of string</returns>
    ''' <remarks></remarks>
    Public Function getWarehouses() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                             Return Warehouse.getWarehouses(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Deletes a warehouse
    ''' </summary>
    ''' <param name="wh">Warehouse to delete.</param>
    ''' <returns>A boolean telling if the operation completed successsfully</returns>
    ''' <remarks></remarks>
    Public Function deleteWarehouse(wh As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return Warehouse.deleteWarehouse(Context.User.Identity.Name, wh, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Saves a warehouse
    ''' </summary>
    ''' <param name="wh">Warehouse to save.</param>
    ''' <param name="oldwh">Warehouse to update or "" if new</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveWarehouse(wh As String, oldwh As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return Warehouse.saveWarehouse(Context.User.Identity.Name, wh, oldwh, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets the user field values for a manual transaction
    ''' </summary>
    ''' <param name="transaction">Transaction order number to get the user fields for.</param>
    ''' <returns>A list of string that contains the user field values</returns>
    ''' <remarks></remarks>
    Public Function getUserFields(transaction As String, view As String) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                             Select Case view.ToLower()
                                                                 Case "manual transactions"
                                                                     Return UserFields.getUserFieldsMT(Context.User.Identity.Name, transaction, Context.QueryString.Get("WSID"))
                                                                 Case Else
                                                                     Return New List(Of String)
                                                             End Select
                                                         End Function)

    End Function

    ''' <summary>
    ''' Saves User Fields
    ''' </summary>
    ''' <param name="ufs">User Fields in a list</param>
    ''' <param name="transaction">Transaction Order Number</param>
    ''' <param name="view">Sending view.</param>
    ''' <returns>A boolean thats tells if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveUserFields(ufs As List(Of String), transaction As String, view As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Select Case view
                                             Case "Manual Transactions"
                                                 UserFields.saveUserFieldsMT(Context.User.Identity.Name, ufs, transaction, Context.QueryString.Get("WSID"))
                                         End Select
                                     End Sub)
    End Function

    ''' <summary>
    ''' Gets Zones from Location
    ''' </summary>
    ''' <param name="location">Location to get zones for.</param>
    ''' <returns>A list fo string that contains the zones</returns>
    ''' <remarks></remarks>
    Public Function getZones(location As String) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                             Return Locations.getZones(location, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Gets Cell Sizes and cell types
    ''' </summary>
    ''' <returns>A cell size model that contains the cell sizes and types</returns>
    ''' <remarks></remarks>
    Public Function getCellSizes() As Task(Of CellSizeModel)
        Return Task(Of CellSizeModel).Factory.StartNew(Function()
                                                           Return CellSize.getCellSizes(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                       End Function)
    End Function

    ''' <summary>
    ''' Deletes cell size specified
    ''' </summary>
    ''' <param name="cell">Cell Size to be deleted.</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function deleteCell(cell As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         CellSize.deleteCell(cell, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Saves a cell size and type
    ''' </summary>
    ''' <param name="oldcell">Cell Size to update or "".</param>
    ''' <param name="newcell">Cell Size value to save.</param>
    ''' <param name="celltype">Cell Type/Description.</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function saveCell(oldcell As String, newcell As String, celltype As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         CellSize.saveCell(oldcell, newcell, celltype, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Gets Item Information, including description, max/min qty, date sensitive, unit of measure, cell size, velocity code, supplier item id, expiration date, warehouse sensitive
    ''' </summary>
    ''' <param name="item">Item Number to get information for.</param>
    ''' <returns>An object the contains the information fo rthe desired item number</returns>
    ''' <remarks></remarks>
    Public Function getItemInfo(item As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function()
                                                    Return ItemNumber.getItemInfo(item, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets Supplier Item ID information (like Item Number, Description)
    ''' </summary>
    ''' <param name="id">supplier item id</param>
    ''' <returns>An object that contains the information for the given supplier item id</returns>
    ''' <remarks></remarks>
    Public Function getSupplierItemIDInfo(id As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function()
                                                    Return SupplierItemID.getSupplierItemIDInfo(id, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                End Function)
    End Function

    ''' <summary>
    ''' selects Item Quantity modal information
    ''' </summary>
    ''' <param name="mapID">Inv Map ID to select data for.</param>
    ''' <returns>A list of string that contaisn the data for the given inv map id</returns>
    ''' <remarks></remarks>
    Public Function selItemQtyData(mapID As Integer) As List(Of String)
        Dim vals As New List(Of String), DataReader As SqlDataReader = Nothing
        Try
            DataReader = RunSPArray("selVInvMap", Context.QueryString.Get("WSID"), {{"@MapID", mapID, intVar}})
            If DataReader.HasRows Then
                While DataReader.Read()
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        'if the value is not null add it, otherwise add an empty string
                        If Not IsDBNull(DataReader(x)) Then
                            vals.Add(DataReader(x))
                        Else
                            vals.Add("")
                        End If
                    Next
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("InventoryMapHub", "itemQty", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try
        Return vals

    End Function

    ''' <summary>
    ''' Selects count of records between beginLoc and endLoc (location numbers in Inventory Map) for printing purposes in the modal.
    ''' </summary>
    ''' <param name="beginLoc">Beginning location for the range.</param>
    ''' <param name="endLoc">Ending location for the range.</param>
    ''' <param name="unique">Boolean to specify group like locations.  True will restrict results to unique entries based on location number.</param>
    ''' <returns>Count of all records currently selected for printing.</returns>
    ''' <remarks></remarks>
    Public Function selQtySelected(beginLoc As String, endLoc As String, unique As Boolean) As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function()
                                                     Dim result As Integer = -1, DataReader As SqlDataReader = Nothing

                                                     Try
                                                         DataReader = RunSPArray("selInvMapLocCount", Context.QueryString.Get("WSID"), {{"@BeginLoc", beginLoc, strVar}, _
                                                                                                              {"@EndLoc", endLoc, strVar}, _
                                                                                                              {"@uniq", CastAsSqlBool(unique), intVar}})

                                                         If DataReader.HasRows Then
                                                             If DataReader.Read() Then
                                                                 If Not IsDBNull(DataReader(0)) Then
                                                                     result = DataReader(0)
                                                                 End If
                                                             End If
                                                         End If
                                                     Catch ex As Exception
                                                         insertErrorMessages("Inventory Map", "selQtySelected", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(DataReader) Then
                                                             DataReader.Dispose()
                                                         End If
                                                     End Try

                                                     Return result
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets Scan Types for modal
    ''' </summary>
    ''' <returns>A list of string that contains the scan types</returns>
    ''' <remarks></remarks>
    Public Function getScanTypes() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                             Return ScanCodes.getScanTypes(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Deletes specified scan type
    ''' </summary>
    ''' <param name="scantype">Scan type to delete</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function deleteScanType(scantype As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         ScanCodes.deleteScanType(scantype, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Saves a scan type
    ''' </summary>
    ''' <param name="scantype">Scan type to save.</param>
    ''' <param name="oldscantype">Scan type to update or ""</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function saveScanType(scantype As String, oldscantype As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         ScanCodes.saveScanType(scantype, oldscantype, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Returns whether item input exists.
    ''' </summary>
    ''' <param name="item">Item Number to check against</param>
    ''' <returns>True if item exists, false otherwise</returns>
    ''' <remarks></remarks>
    Public Function itemExists(item As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Dim datareader As SqlDataReader = Nothing
                                                     Try
                                                         datareader = RunSPArray("selInventoryItemNum", Context.QueryString.Get("WSID"), {{"@ItemNumber", item, strVar}})

                                                         If datareader.HasRows Then
                                                             Return True
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.Message)
                                                         insertErrorMessages("GlobalHub", "itemExists", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(datareader) Then
                                                             datareader.Dispose()
                                                         End If
                                                     End Try
                                                     Return False
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates printing clients to the print service's success/failure resulting from their print request
    ''' </summary>
    ''' <remarks></remarks>
    Public Function updatePrintStatus(wsid As String, message As String) As task
        ' insertErrorMessages("GlobalHub", "updatePrintStatus", "Called", Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        Clients.Group(wsid).updatePrintStatus(message)
        Return Nothing
    End Function

    ''' <summary>
    ''' Tests print functionality
    ''' </summary>
    ''' <param name="printerName">The printer name ot test</param>
    ''' <param name="printerAddress">The address of the printer</param>
    ''' <param name="isLabel">If the printer is a label printer</param>
    ''' <returns></returns>
    Public Function testPrint(printerName As String, printerAddress As String, isLabel As Boolean) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             Dim location As HttpServerUtilityBase = Context.Request.GetHttpContext.Server
                                             Dim userDirectory As String = GlobalFunctions.getWSDirectory(location, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Dim reportDesign As String = System.IO.Path.Combine(userDirectory, IIf(isLabel, "test_label.lbl", "test_page.lst"))
                                             Dim user As String = Context.User.Identity.Name
                                             Dim WSID As String = Context.QueryString.Get("WSID")
                                             Dim LLType As LlProject = IIf(isLabel, LlProject.Label, LlProject.List)
                                             Clients.Group(PrintService.PrintServiceHubName).TestPrint( _
                                                 New TestPrintModel(reportDesign, user, userDirectory, WSID, LLType, printerName, printerAddress, isLabel))
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("GlobalHub", "TestPrint", ex.GetType.ToString() & ": " & ex.Message, _
                                                                 Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Help page function, but help pages don't have hubs so we put it here :D
    ''' </summary>
    ''' <param name="lbound"></param>
    ''' <param name="ubound"></param>
    ''' <param name="pluginWhere"></param>
    ''' <returns>Test data for the help page</returns>
    ''' <remarks></remarks>
    Function GetSTPageExample(lbound As Integer, ubound As Integer, pluginWhere As String) As Task(Of STPagingResult)
        Return Task(Of STPagingResult).Factory.StartNew(Function() As STPagingResult
                                                            Dim reader As SqlDataReader = Nothing
                                                            Dim result As New STPagingResult(True, New List(Of List(Of String)), "", 0, 0)
                                                            Try
                                                                reader = RunSPArray("selHelpExampleData", Context.QueryString.Get("WSID"), _
                                                                                    {{"@lbound", lbound, intVar}, {"@ubound", ubound, intVar}, {"@pluginWhere", pluginWhere, strVar}})
                                                                For x As Integer = 0 To 2
                                                                    If reader.HasRows Then
                                                                        Dim innerList As New List(Of String)
                                                                        While reader.Read
                                                                            If x = 0 Then
                                                                                result.numRecords = IIf(IsDBNull(reader(0)), 0, reader(0))
                                                                            ElseIf x = 1 Then
                                                                                For y As Integer = 0 To reader.FieldCount - 1
                                                                                    innerList.Add(CheckDBNull(reader(y)))
                                                                                Next
                                                                                result.pages.Add(innerList)
                                                                                innerList = New List(Of String)
                                                                            Else
                                                                                result.filteredRecords = IIf(IsDBNull(reader(0)), 0, reader(0))
                                                                            End If
                                                                        End While
                                                                        reader.NextResult()
                                                                    End If
                                                                Next
                                                            Catch ex As Exception
                                                                Debug.Print(ex.Message)
                                                                result.success = False
                                                                result.message = ex.Message
                                                                insertErrorMessages("GlobalHub", "GetSTPageExample", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            Finally
                                                                If Not IsNothing(reader) Then reader.Dispose()
                                                            End Try
                                                            Return result
                                                        End Function)
    End Function

    ''' <summary>
    ''' Gets all printers
    ''' </summary>
    ''' <returns></returns>
    Function getAllPrintersandWSPrinter()
        Dim WSID As String = Context.QueryString.Get("WSID")
        Dim User As String = Context.User.Identity.Name
        Return New With {.AllPrinters = GlobalFunctions.getAllPrinters(WSID, User), .WSPrinters = GlobalFunctions.getWorkStatPrinters(WSID, User)}
    End Function

    ''' <summary>
    ''' Updates a printer
    ''' </summary>
    ''' <param name="ReportPrinter">The report printer for the workstation</param>
    ''' <param name="LabelPrinter">The label printer for the workstation</param>
    Sub UpdateWSPrinters(ReportPrinter As String, LabelPrinter As String)
        Try
            Dim WSID As String = Context.QueryString.Get("WSID")
            Dim User As String = Context.User.Identity.Name
            RunSPArray("updWSPrefsPrinters", WSID, {{"@ReportPrinter", ReportPrinter, strVar}, {"@LabelPrinter", LabelPrinter, strVar}, {"@WSID", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("GlobalHub", "GetSTPageExample", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        End Try
    End Sub

    Function TestPrintFunc(type As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim PrintMess As String = ""
                                                    Dim reader As SqlDataReader = Nothing
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        Dim WSID As String = Context.QueryString.Get("WSID")
                                                        Dim User As String = Context.User.Identity.Name

                                                        Dim location As HttpServerUtilityBase = Context.Request.GetHttpContext.Server

                                                        Dim userDirectory As String = GlobalFunctions.getWSDirectory(location, Context.User.Identity.Name, Context.QueryString.Get("WSID"))

                                                        Dim Printer As String = ""

                                                        If type = "list" Then
                                                            Printer = GlobalFunctions.getWorkStatPrinters(WSID, User).ReportPrinter
                                                            testPrint(Printer, Printer, False)
                                                        Else
                                                            Printer = GlobalFunctions.getWorkStatPrinters(WSID, User).LabelPrinter
                                                            testPrint(Printer, Printer, True)
                                                        End If


                                                        Threading.Thread.Sleep(4000)

                                                        DataReader = RunSPArray("selPrintLogPrintMess", "CONFIG", {{"@WSID", WSID, strVar}})


                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            PrintMess = DataReader("Message")
                                                        End If

                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.Message)
                                                        insertErrorMessages("GlobalHub", "TestPrint", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(reader) Then reader.Dispose()

                                                        If Not IsNothing(DataReader) Then DataReader.Dispose()
                                                    End Try

                                                    Return PrintMess
                                                End Function)
    End Function

End Class
