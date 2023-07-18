' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
    Public Class InventoryMapHub
        Inherits Hub

        ''' <summary>
        ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
        ''' </summary>
        ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
        ''' <remarks></remarks>
        Public Overrides Function OnConnected() As Task
            'Adds a user to their own unique group by a value passed in during connection
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
            'Calls the Original Onconnected Function pass control back to the server
            Return MyBase.OnConnected()
        End Function

        ''' <summary>
        ''' Retrieves details about an item number from inventory master
        ''' </summary>
        ''' <param name="item">Item number to filter on</param>
        ''' <param name="zone">Zone to check if the zone is a carousel, bulk or carton flow location, which determines which cell size, velocity get selected from inventory</param>
        ''' <returns>Description, Unit of Measure, Max Qty, Min Qty, Date Sensitive, Warehouse Sensitive, Cell Size, Velocity Code for specified item number and zone</returns>
        ''' <remarks></remarks>
        Public Function getItemDetails(item As String, zone As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, details As New List(Of String)
            Try
                datareader = RunSPArray("selItemDetails", Context.QueryString.Get("WSID"), {{"@Item", item, strVar}, {"@Zone", zone, strVar}})

                If datareader.HasRows Then
                    If datareader.Read() Then
                        For x As Integer = 0 To datareader.FieldCount - 1
                            If IsDBNull(datareader(x)) Then
                                details.Add("")
                            Else
                                details.Add(datareader(x))
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("InventoryMapHub", "getItemDetails", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return details
        End Function

    ''' <summary>
    ''' Gets the date sensitive and warehouse sensitive fields from the inventory table for the specified item number
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns>An object that contains the sensitive data for the given item number</returns>
    ''' <remarks></remarks>
    Public Function getDateWarehouseSensitive(item As String) As Object
            Dim datareader As SqlDataReader = Nothing
            Try
                datareader = RunSPArray("selWarehouseDateSensitive", Context.QueryString.Get("WSID"), {{"@Item", item, strVar}})

                If datareader.HasRows Then
                If datareader.Read() Then
                    Return New With {.DateSensitive = IIf(IsDBNull(datareader(0)), False, datareader(0)),
                                         .WarehouseSensitive = IIf(IsDBNull(datareader(1)), False, datareader(1))}
                End If
            End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("InventoryMapHub", "getDateWarehouseSensitive", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return New With {.DateSensitive = False, _
                             .WarehouseSensitive = False}
        End Function

    ''' <summary>
    ''' Inserts a new Inventory Map entry
    ''' </summary>
    ''' <param name="entry">InvMapRowObj instance with properties to insert</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function addNewInvMap(entry As InvMapRowObj) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             RunActionSP("insInvMap", Context.QueryString.Get("WSID"), _
                                                         {{"@AltLight", entry.AltLight, intVar}, {"@Location", entry.Location, strVar}, _
                                                          {"@Warehouse", entry.Warehouse, If(entry.Warehouse = "", nullVar, strVar)}, _
                                                          {"@Zone", entry.Zone, strVar}, {"@Carousel", entry.Carousel, strVar}, {"@Row", entry.Row, strVar}, {"@Shelf", entry.Shelf, strVar}, _
                                                          {"@Bin", entry.Bin, strVar}, {"@Item", entry.ItemNumber, strVar}, {"@MaxQty", entry.MaxQty, intVar}, _
                                                          {"@MinQty", entry.MinQty, intVar}, {"@Cell", entry.CellSize, strVar}, {"@Velocity", entry.VelocityCode, strVar}, _
                                                          {"@UF1", entry.UF1, strVar}, {"@UF2", entry.UF2, strVar}, {"@DateSensitive", CastAsSqlBool(entry.DateSensitive), intVar}, _
                                                          {"@Dedicated", CastAsSqlBool(entry.Dedicated), intVar}, {"@LaserX", entry.LaserX, intVar}, {"@LaserY", entry.LaserY, intVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("InventoryMapHub", "addNewInvMap", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
        End Function

    ''' <summary>
    ''' Edits an Inventory Map record
    ''' </summary>
    ''' <param name="entry">InvMapRowObj instance with properties to alter the entry with, done by Inv Map ID</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function editInvMap(entry As InvMapRowObj) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             RunActionSP("updateInvMap", Context.QueryString.Get("WSID"),
                                                         {{"@AltLight", entry.AltLight, intVar}, {"@Location", entry.Location, strVar},
                                                          {"@Warehouse", entry.Warehouse, If(entry.Warehouse = "", nullVar, strVar)},
                                                          {"@Zone", entry.Zone, If(entry.Zone = "", nullVar, strVar)},
                                                          {"@Carousel", entry.Carousel, If(entry.Carousel = "", nullVar, strVar)},
                                                          {"@Row", entry.Row, If(entry.Row = "", nullVar, strVar)},
                                                          {"@Shelf", entry.Shelf, If(entry.Shelf = "", nullVar, strVar)},
                                                          {"@Bin", entry.Bin, If(entry.Bin = "", nullVar, strVar)},
                                                          {"@Item", entry.ItemNumber, strVar}, {"@MaxQty", entry.MaxQty, intVar},
                                                          {"@MinQty", entry.MinQty, intVar}, {"@Cell", entry.CellSize, strVar}, {"@Velocity", entry.VelocityCode, strVar},
                                                          {"@UF1", entry.UF1, strVar}, {"@UF2", entry.UF2, strVar}, {"@DateSensitive", CastAsSqlBool(entry.DateSensitive), intVar},
                                                          {"@Dedicated", CastAsSqlBool(entry.Dedicated), intVar}, {"@LaserX", entry.LaserX, intVar}, {"@LaserY", entry.LaserY, intVar},
                                                          {"@InvMapID", entry.InvMapID, intVar}, {"@MasterLocation", CastAsSqlBool(entry.MasterLocation), intVar},
                                                          {"@MasterInvMapID", entry.MasterInvMapID, intVar}, {"@User", Context.User.Identity.Name, strVar},
                                                          {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                             Dim columns As New List(Of String) From {"Alternate Light", "Location", "Warehouse", "Zone", "Carousel", "Row", "Shelf", "Bin", "Item Number", _
                                                                                      "Maximum Quantity", "Min Quantity", "Cell Size", "Velocity Code", "User Field1", "User Field2", _
                                                                                      "Date Sensitive", "Dedicated", "Laser X", "Laser Y", "Master Location", "Master Inv Map ID"}
                                             Dim update As New List(Of String) From {entry.AltLight, entry.Location, entry.Warehouse, entry.Zone, entry.Carousel, entry.Row, entry.Shelf, _
                                                                                     entry.Bin, entry.ItemNumber, entry.MaxQty, entry.MinQty, entry.CellSize, entry.VelocityCode, _
                                                                                     entry.UF1, entry.UF2, entry.DateSensitive, entry.Dedicated, entry.LaserX, entry.LaserY, entry.MasterLocation, _
                                                                                     entry.MasterInvMapID}

                                             Clients.Group(Context.QueryString.Get("ConnectionName")).tableUpdated(New With {.columns = columns, .update = update, .invMapId = entry.InvMapID})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("InventoryMapHub", "editInvMap", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
        End Function

        ''' <summary>
        ''' Quarantines a specified entry in Inventory Map based on Inv Map ID.
        ''' </summary>
        ''' <param name="MapID">Inv Map ID to quarantine.</param>
        ''' <returns>Task to prevent timeout issues.</returns>
        ''' <remarks></remarks>
        Public Function quarantineInventory(MapID As Integer) As Task
            ' quarantines an item by inv map id and adds a message to the event log

            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updInvMapOTQuarantine", Context.QueryString.Get("WSID"), {{"@MapID", MapID, strVar}, _
                                                        {"@XferBy", Context.User.Identity.Name, strVar},
                                                        {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("Inventory Map", "quarantineInventory", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try

                                         End Sub)
        End Function

        ''' <summary>
        ''' Unquarantines an entry in Inventory Map based on Inv Map ID.
        ''' </summary>
        ''' <param name="MapID">Inv Map ID to unquarantine.</param>
        ''' <returns>Task to prevent timeout issues.</returns>
        ''' <remarks></remarks>
        Public Function unQuarantineInventory(MapID As Integer) As Task
            ' unquarantines an item by inv map id and adds a message to the event log

            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updInvMapUnQuarantine", Context.QueryString.Get("WSID"), {{"@MapID", MapID, strVar}, _
                                                        {"@XferBy", Context.User.Identity.Name, strVar},
                                                        {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("Inventory Map", "unQuarantineInventory", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try

                                         End Sub)
        End Function

        ''' <summary>
        ''' Duplicates an entry in Inventory Map based on Inv Map ID.
        ''' </summary>
        ''' <param name="invMapID">Inv Map ID to duplicate.</param>
        ''' <returns>Task to prevent timeout issues.</returns>
        ''' <remarks></remarks>
        Public Function duplicateItem(invMapID As Integer) As Task
            ' duplicates item at supplied inv map id if the sending user has permission
            Return Task.Factory.StartNew(Sub()
                                             Dim DataReader As SqlDataReader = Nothing
                                             Try
                                                 DataReader = RunSPArray("selStaffAcessControlName", Context.QueryString.Get("WSID"), {{"@UserName", Context.User.Identity.Name, strVar}, {"@AccessCheck", "Inv Map Dupe Location", strVar}})
                                                 'if user has permissions to duplicate an item
                                                 If DataReader.HasRows() Then
                                                     RunActionSP("insInvMapDuplicate", Context.QueryString.Get("WSID"), {{"@mapID", invMapID, intVar}})
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("Inventory Map", "duplicateItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(DataReader) Then
                                                     DataReader.Dispose()
                                                 End If
                                             End Try

                                         End Sub)
        End Function

        ''' <summary>
        ''' Clears certain fields in Inventory Map by specified Inv Map ID.
        ''' </summary>
        ''' <param name="invMapID">Inv Map ID to filter by.</param>
        ''' <returns>Task to prevent timeout issues.</returns>
        ''' <remarks></remarks>
        Public Function clearInventoryMap(invMapID As Integer) As Task(Of Boolean)
            ' clears fields at the specified inv map id and logs the event in the event log
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Try
                                                             DataReader = RunSPArray("selEmployeesAccessLevel", Context.QueryString.Get("WSID"), {{"@UserName", Context.User.Identity.Name, strVar}})
                                                             If DataReader.HasRows() Then
                                                                 If DataReader.Read Then
                                                                     ' if the user is an administrator run the SP
                                                                     If DataReader(0) = "Administrator" Then
                                                                         DataReader.Dispose()
                                                                         DataReader = Nothing
                                                                         DataReader = RunSPArray("updInvMapClear", Context.QueryString.Get("WSID"), {{"@mapID", invMapID, intVar}, _
                                                                                                         {"@user", Context.User.Identity.Name, strVar}, _
                                                                                                         {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                                         If DataReader.HasRows Then
                                                                             If DataReader.Read() Then
                                                                                 If Not IsDBNull(DataReader(0)) Then
                                                                                     Return IIf(DataReader(0) = 1, True, False)
                                                                                 End If
                                                                             End If
                                                                         End If
                                                                     End If
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("Inventory Map", "clearInventoryMap", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return False
                                                     End Function)
        End Function

    ''' <summary>
    ''' Deletes an entry from Inventory Map table by Inv Map ID.
    ''' </summary>
    ''' <param name="invMapID">Inv Map ID to delete.</param>
    ''' <returns>Task to prevent timeout issues.</returns>
    ''' <remarks></remarks>
    Public Function deleteInvMapID(invMapID As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim DataReader As SqlDataReader = Nothing
                                                     Dim OTCountReader As SqlDataReader = Nothing

                                                     Try
                                                         DataReader = RunSPArray("selStaffAcessControlName", Context.QueryString.Get("WSID"), {{"@UserName", Context.User.Identity.Name, strVar}, {"@AccessCheck", "Inv Map Delete", strVar}})
                                                         'if the stored procedure returned a vlaue then, run the intended operation 
                                                         If DataReader.HasRows Then
                                                             'Check OT for pick/put aways
                                                             OTCountReader = RunSPArray("selOTInvMapCount", Context.QueryString.Get("WSID"), {{"@InvMapID", invMapID, intVar}})

                                                             Dim Index As Integer = 0
                                                             Dim PickQty As Integer = 0
                                                             Dim PutQty As Integer = 0

                                                             While OTCountReader.HasRows
                                                                 If OTCountReader.Read() Then
                                                                     If Index = 2 Then
                                                                         'Pick Qty
                                                                         PickQty = If(IsDBNull(OTCountReader(0)), 0, OTCountReader(0))
                                                                     ElseIf Index = 3 Then
                                                                         'Put Qty
                                                                         PutQty = If(IsDBNull(OTCountReader(0)), 0, OTCountReader(0))
                                                                     End If
                                                                 End If
                                                                 Index += 1
                                                                 OTCountReader.NextResult()
                                                             End While

                                                             'if qtys are greater than zero exit with a false
                                                             If PickQty > 0 OrElse PutQty > 0 Then
                                                                 Return False
                                                             End If

                                                             RunActionSP("delInvMapMapID", Context.QueryString.Get("WSID"), {{"@mapID", invMapID, intVar},
                                                                                    {"@user", Context.User.Identity.Name, strVar},
                                                                                    {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("Inventory Map", "deleteInvMapID", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(DataReader) Then
                                                             DataReader.Dispose()
                                                         End If

                                                         If Not IsNothing(OTCountReader) Then
                                                             OTCountReader.Dispose()
                                                         End If
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Checks any Open Transactions for date sensitivity before the field can be toggled.
    ''' </summary>
    ''' <param name="locationNumber">Location in Open Transactions to compare against.  This is the Location Number column in Inventory Map.</param>
    ''' <returns>True if there are Open Transactions that need to be addressed before date sensitive can be altered.</returns>
    ''' <remarks></remarks>
    Public Function dateSensitiveChange(locationNumber As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Try
                                                             DataReader = RunSPArray("selOTLocCompDate", Context.QueryString.Get("WSID"), {{"@LocNum", locationNumber, strVar}})
                                                             If DataReader.HasRows Then
                                                                 Return True
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex)
                                                             insertErrorMessages("Inventory Map", "dateSensitiveChange", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return False
                                                     End Function)
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
                                                             insertErrorMessages("Inventory Map", "selQtySelected", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try

                                                         Return result
                                                     End Function)
        End Function

        ''' <summary>
        ''' Updates Master Inv Map ID based on Master Location checkbox.
        ''' </summary>
        ''' <param name="mapId">Inv Map ID to update.</param>
        ''' <param name="ware">Warehouse checked against if not a Master Location.</param>
        ''' <param name="zone">Zone checked against if not a Master Location.</param>
        ''' <param name="car">Carousel checked against if not a Master Location.</param>
        ''' <param name="row">Row checked against if not a Master Location.</param>
        ''' <param name="shelf">Shelf checked against if not a Master Location.</param>
        ''' <param name="bin">Bin checked against if not a Master Location.</param>
        ''' <returns>Returns the new value of the Master Inv Map ID based on checkbox value.</returns>
        ''' <remarks>If the location is not a master location the next nearest Inv Map ID is selected to be the Master Inv Map ID at that location based on where Warehouse, Zone, Carousel, Row, Shelf and Bin match the next highest Inv Map ID that is a Master Location entry.</remarks>
        Public Function selectMasterMapID(mapId As Integer, ware As String, zone As String, car As String, row As String, shelf As String, bin As String) As Task(Of Integer)
            Return Task(Of Integer).Factory.StartNew(Function()
                                                         Dim DataReader As SqlDataReader = Nothing, num As Integer = -1
                                                         Try
                                                             DataReader = RunSPArray("selInvMapMastInvMapID", Context.QueryString.Get("WSID"), {{"@MapID", mapId, intVar}, {"@Ware", ware, strVar}, {"@Zone", zone, strVar}, {"@Car", car, strVar}, _
                                                                                                           {"@Row", row, strVar}, {"@Shelf", shelf, strVar}, {"@Bin", bin, strVar}})

                                                             If DataReader.HasRows Then
                                                                 If DataReader.Read() Then
                                                                     If Not IsDBNull(DataReader(0)) Then
                                                                         num = CInt(DataReader(0))
                                                                     End If
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex)
                                                             insertErrorMessages("Inventory Map", "selectMasterMapID", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return num
                                                     End Function)

        End Function


        ''' <summary>
        ''' Updates item quantity for specified Inv Map ID in Inventory Map.
        ''' </summary>
        ''' <param name="mapId">Inv Map ID entry to update.</param>
        ''' <param name="quantity">New quantity.</param>
        ''' <param name="description">Reason for adjustment at the Inv Map ID specified.</param>
        ''' <returns>Task to prevent timeout issues.</returns>
        ''' <remarks></remarks>
        Public Function updateItemQuan(mapId As Integer, quantity As Integer, description As String) As Task
            Return Task.Factory.StartNew(Sub()

                                             Try
                                                 RunActionSP("updInvMapItemQuant", Context.QueryString.Get("WSID"), _
                                                             {{"@Quant", quantity, intVar}, {"@Desc", description, strVar}, {"@MapID", mapId, intVar}, _
                                                              {"@XferBy", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex)
                                                 insertErrorMessages("Inventory Map", "updateItemQuan", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
    End Class

