' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class ProcessPutAwaysHub
    Inherits Hub

    Private Shared _findLocationLock As Object = New Object()

    ''' <summary>
    ''' Gets totes that belong to the specified batch
    ''' </summary>
    ''' <param name="BatchID">The batch id to get the totes from</param>
    ''' <returns>A list of object that contains the totes for the desired batch id</returns>
    ''' <remarks></remarks>
    Public Function GetBatchTotes(BatchID As String) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return Induction.ProcessPutAways.GetBatchTotes(BatchID, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    
    ''' <summary>
    ''' Gets the next batch id 
    ''' </summary>
    ''' <returns>string containg the next batch id</returns>
    ''' <remarks></remarks>
    Public Function nextBatchID() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Return GlobalFunctions.getNextBatchID(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function

    ''' <summary>
    ''' Refreshes the totes and their information
    ''' </summary>
    ''' <returns>None</returns>
    Public Function RefreshTotes()
        Dim WSID = Context.QueryString.Get("WSID")
        Clients.Group(WSID).RefreshTotes()
    End Function

    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
    ''' </summary>
    ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task
        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("WSID"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Deallocates or clears the specified tote or batch
    ''' </summary>
    ''' <param name="IsBatch">If a batch is getting cleared</param>
    ''' <param name="BatchID">The batch id to deallocate or delete</param>
    ''' <param name="ToteID">The tote id to deallocate or delete</param>
    ''' <param name="TransType">The transaction type of the tote id</param>
    ''' <param name="DeAllocate">If the batch/tote is getting deallocated</param>
    ''' <param name="PageFrom">The page that this command came from</param>
    ''' <returns>A boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function DeleteToteOrBatch(IsBatch As Boolean, BatchID As String, ToteID As String, TransType As String, DeAllocate As Boolean, PageFrom As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.DeleteBatchOrTote(IsBatch, ToteID, BatchID, TransType, DeAllocate, PageFrom, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Deallocates all batches for the current workstation
    ''' </summary>
    ''' <returns>A boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function DeleteAllBatches() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.DeleteAllBatches(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets the next tote id from system preferences
    ''' </summary>
    ''' <returns>An integer that tells what the next tote id value is</returns>
    ''' <remarks></remarks>
    Public Function GetNextTote() As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Return Induction.ProcessPutAways.GetNextTote(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    Public Function GetAndUpdateNextTote() As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Return Induction.ProcessPutAways.GetAndUpdateNextTote(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates the next tote id field in system preferences
    ''' </summary>
    ''' <param name="tote">The next tote id value</param>
    ''' <returns>A boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function UpdateNextTote(tote As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.UpdateNextTote(tote, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets the next available batch id to assign to a new batch
    ''' </summary>
    ''' <returns>A string that tells what the next batch id value is</returns>
    ''' <remarks></remarks>
    Public Function GetNextBatchID() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Return GlobalFunctions.getNextBatchID(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets all zones and their eligibility to be assigned to the current batch.
    ''' </summary>
    ''' <param name="BatchID">The batch id to get the zones for</param>
    ''' <returns>A list of object that contains the zones and if they are eligible</returns>
    ''' <remarks></remarks>
    Public Function GetAvailableZones(BatchID As String) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return Induction.ProcessPutAways.GetAvailableZones(BatchID, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Starts a batch in the tote setup queue by moving it into the sql table.  It is stored in html and javascript before this point and does not exist in sql.
    ''' </summary>
    ''' <param name="BatchID">The batch id to preocess</param>
    ''' <param name="ZoneLabel">The zone label for the batch id</param>
    ''' <param name="Totes">The tote ids that are assigned to the batch</param>
    ''' <returns>A boolean telling if the oepration completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ProcessBatch(BatchID As String, ZoneLabel As String, Totes As List(Of String)) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.ProcessBatch(BatchID, ZoneLabel, Totes, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Marks a tote as full by changing its number of cells to be equal to the tote's current capacity.
    ''' </summary>
    ''' <param name="ToteNum">The tote number to mark as full</param>
    ''' <param name="Cell">The value to set the cells to show it is full</param>
    ''' <param name="BatchID">The batch that contains the tote</param>
    ''' <returns>A string telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function MarkToteFull(ToteNum As Integer, Cell As Integer, BatchID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updToteSetupQueueMarkFull", Context.QueryString.Get("WSID"), {{"@ToteNum", ToteNum, intVar}, _
                                                                                                                                   {"@Cell", Cell, intVar}, _
                                                                                                                                   {"@BatchID", BatchID, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("ProcessPutAwaysHub", "MarkToteFull", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets any eligible transactions that fit the criteria for use with assignment to a batch/tote.
    ''' </summary>
    ''' <param name="lowerBound">The lowest row number</param>
    ''' <param name="upperBound">The highest row number</param>
    ''' <param name="input">The value that is going ot be checked</param>
    ''' <returns>A st paging object for javascript plugin</returns>
    ''' <remarks></remarks>
    Public Function GetTransactionsForTote(lowerBound As Integer, upperBound As Integer, input As List(Of String)) As Task(Of STPagingResult)
        Return Task(Of STPagingResult).Factory.StartNew(Function() As STPagingResult
                                                            Try
                                                                Return Induction.ProcessPutAways.GetTransactionsForTote(input(0), input(1), input(2), lowerBound, upperBound, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            Catch ex As Exception
                                                                Debug.Print(ex.ToString())
                                                                insertErrorMessages("ProcessPutAwaysHub", "GetTransactionsForTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            End Try
                                                            Return New STPagingResult(False, New List(Of List(Of String)), "An error occurred.  Check the error log for details.", 0, 0)
                                                        End Function)
    End Function

    ''' <summary>
    ''' Gets and updates the Next Serial Number in system preferences
    ''' </summary>
    ''' <param name="NumReels">The number to increment the serial number by</param>
    ''' <returns>A long number value that contains the new serial number value</returns>
    ''' <remarks></remarks>
    Public Function GetNextSerialNumber(NumReels As Integer) As Task(Of Long)
        Return Task(Of Long).Factory.StartNew(Function() As Long
                                                  Return Induction.ProcessPutAways.GetNextSerialNumber(NumReels, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                              End Function)
    End Function

    ''' <summary>
    ''' Updates the cell sizes and velocity codes of the specified item in the inventory master.
    ''' </summary>
    ''' <param name="item">The desired item number to updates</param>
    ''' <param name="ccell">The new cell size value</param>
    ''' <param name="bcell">The new bulk cell size value</param>
    ''' <param name="cfcell">The new carrton flwo cell size value</param>
    ''' <param name="cvel">The new velocity code value</param>
    ''' <param name="bvel">The new bulk velocity code value</param>
    ''' <param name="cfvel">The new carton flow velocity code value</param>
    ''' <returns>A boolean that tells if the oepration completed successfully</returns>
    ''' <remarks></remarks>
    Public Function UpdateInventoryMaster(item As String, ccell As String, bcell As String, cfcell As String, cvel As String, bvel As String, cfvel As String, pzone As String, szone As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.UpdateInventoryMaster(item, ccell, bcell, cfcell, cvel, bvel, cfvel, pzone, szone, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets item details (with open transaction details if OTID is greater than 0)
    ''' </summary>
    ''' <param name="OTID">The open transactions id to get info for</param>
    ''' <param name="item">The item number to get info for</param>
    ''' <returns>A list of string that contains the infomration on either the item number or open trasnaction id</returns>
    ''' <remarks></remarks>
    Public Function GetItemDetails(OTID As Long, item As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Return Induction.ProcessPutAways.GetItemDetails(OTID, item, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates a shelf as being full where the zone and row fields match.
    ''' </summary>
    ''' <param name="zone">The zone where the shelf is located</param>
    ''' <param name="row">The row of the shelf within the zone</param>
    ''' <returns>A boolean that tells if the oepration completed successfully</returns>
    ''' <remarks>Steve mentioned this may be currently implemented incorrectly in both versions of the application because it may mess up a carousel location.</remarks>
    Public Function UpdateShelfFull(zone As String, row As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("updIMShelfFull", Context.QueryString.Get("WSID"), {{"@Zone", zone, strVar}, {"@Row", row, strVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.ToString())
                                                         insertErrorMessages("ProcessPutAwaysHub", "UpdateShelfFull", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Creates a set of reels from a reel tracking inventory item.
    ''' </summary>
    ''' <param name="item">The item number ot create the reels from</param>
    ''' <param name="reels">The information for each of the reel transactions</param>
    ''' <returns>A list of integer that contains the isd of the new records that were created</returns>
    ''' <remarks></remarks>
    Public Function CreateReels(item As String, reels As List(Of List(Of String))) As Task(Of List(Of Integer))
        Return Task(Of List(Of Integer)).Factory.StartNew(Function() As List(Of Integer)
                                                              Return Induction.ProcessPutAways.CreateReels(item, reels, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                          End Function)
    End Function

    ''' <summary>
    ''' Determines if there is a cross dock opportunity for the current item by returning any eligible reprocess transactions.
    ''' </summary>
    ''' <param name="lowerbound">The lowest row number to get records from</param>
    ''' <param name="upperbound">The highest row number to get records from</param>
    ''' <param name="itemWhse">list of string that contaisn the item number, warehouse, and the where clause that was applied</param>
    ''' <returns>A st paging object for the javascript plugin</returns>
    ''' <remarks></remarks>
    Public Function GetCrossDock(lowerbound As Integer, upperbound As Integer, itemWhse As List(Of String)) As Task(Of STPagingResult)
        Return Task(Of STPagingResult).Factory.StartNew(Function() As STPagingResult
                                                            Dim user As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
                                                            Try
                                                                Return Induction.ProcessPutAways.GetCrossDock(lowerbound, upperbound, itemWhse(0), itemWhse(1), itemWhse(2), user, WSID)
                                                            Catch ex As Exception
                                                                Debug.Print(ex.ToString())
                                                                insertErrorMessages("ProcessPutAwaysHub", "GetCrossDock", ex.ToString(), user, WSID)
                                                                Return New STPagingResult(False, New List(Of List(Of String)), ex.Message, 0, 0)
                                                            End Try
                                                        End Function)
    End Function

    ''' <summary>
    ''' Gets the top 1 batch id from tote setup queue where the workstation matches this one's and zone is in the zone label field.
    ''' </summary>
    ''' <param name="zone">The desired zone to check for</param>
    ''' <returns>A string that contaisn the batch id for the zone</returns>
    ''' <remarks></remarks>
    Public Function GetBatchByZone(zone As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Return Induction.ProcessPutAways.GetBatchByZone(zone, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets the Open Transactions Temp (reprocess) data related to ID passed.
    ''' </summary>
    ''' <param name="rpid">The reprocess id to get infor for</param>
    ''' <returns>A list fo string that continas the data for the desired reprocess record</returns>
    ''' <remarks></remarks>
    Public Function GetRPDetail(rpid As String) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Return Induction.ProcessPutAways.GetRPDetail(rpid, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Marks a container as full by changing its velocity code to "FULL" + velocity
    ''' </summary>
    ''' <param name="location">The location to mark as full</param>
    ''' <returns>A boolean telling if the oepration was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function MarkContainerFull(location As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.MarkContainerFull(location, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Checks if the inv map id provided is part of a bad container (multiple assigned items to a single [vInventoryMap].[Reel Location Number])
    ''' </summary>
    ''' <param name="InvMapID">The inventory map id to check</param>
    ''' <returns>A task of object that tells if the desired inventory map id is in a bad container</returns>
    ''' <remarks></remarks>
    Public Function CheckBadContainer(InvMapID As Integer) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Return Induction.ProcessPutAways.CheckBadContainer(InvMapID, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                End Function)
    End Function

    ''' <summary>
    ''' Reserves the specified location for the item specified.
    ''' </summary>
    ''' <param name="InvMapID">The location to reserve</param>
    ''' <param name="item">The item to reserve the location for</param>
    ''' <param name="whse">The warehouse</param>
    ''' <returns>A boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function DedicateLocation(InvMapID As Integer, item As String, whse As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.DedicateLocation(InvMapID, Context.User.Identity.Name, Context.QueryString.Get("WSID"), item, whse)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Verifies that the provided serial numbers are not already allocated in open transactions or inventory map.
    ''' </summary>
    ''' <param name="SNs">A list of serial numbers to verify</param>
    ''' <returns>A list of object that tells if each serial number is vaild</returns>
    ''' <remarks></remarks>
    Public Function CheckValidSN(SNs As List(Of String)) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return Induction.ProcessPutAways.CheckValidSN(SNs, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Finds the prioritized location for the specified item and reserves the location for the workstation that requested the assignment.  The reservation is cleared only after the item is task completed or abandoned.
    ''' </summary>
    ''' <param name="qtyPut">The quantity to put away</param>
    ''' <param name="item">The item number to find the location for</param>
    ''' <param name="ccell">The cell size of the item</param>
    ''' <param name="cvel">The velocity code of the item</param>
    ''' <param name="bcell">The bulk cell size of the item</param>
    ''' <param name="bvel">The bulk velocity code of the item</param>
    ''' <param name="cfcell">The carton flow cell size of the item</param>
    ''' <param name="cfvel">The carton flow velocity code of the item</param>
    ''' <param name="whse">The warehouse of the item</param>
    ''' <param name="dateSens">The date sensitive value of the item</param>
    ''' <param name="FIFO">The FIFO value of the item</param>
    ''' <param name="isReel">The isReel value of the item</param>
    ''' <param name="lot">The lot number of the item</param>
    ''' <param name="ser">The serial number of the item</param>
    ''' <param name="replenfwd">If the item is repelenished forward</param>
    ''' <param name="prevZone">The previous zone value of the item</param>
    ''' <param name="dedicate">If the item is dedicated</param>
    ''' <param name="RTS">The RTS valu of the item</param>
    ''' <returns>An object that contains the location if one is found</returns>
    ''' <remarks></remarks>
    Public Function FindLocation(qtyPut As Integer, item As String, ccell As String, cvel As String, bcell As String, bvel As String, cfcell As String, cfvel As String,
                                 whse As String, dateSens As Boolean, FIFO As Boolean, isReel As Boolean, lot As String, ser As String, replenfwd As Boolean, prevZone As String,
                                 dedicate As Boolean, RTS As Boolean, expDate As String, PrimaryZone As String, SecondaryZone As String) As Object
        SyncLock _findLocationLock
            Return Induction.InductionLocAss.AssignPutAwayLocation(qtyPut, item, ccell, cvel, bcell, bvel, cfcell, cfvel, whse, dateSens, FIFO,
                                                                                           isReel, lot, ser, replenfwd, prevZone, dedicate, RTS, expDate, Context.QueryString.Get("WSID"),
                                                                                           Context.User.Identity.Name, PrimaryZone, SecondaryZone)
        End SyncLock
    End Function

    ''' <summary>
    ''' Checks if specified location is dedicated to the specified item
    ''' </summary>
    ''' <param name="item">The item number to check</param>
    ''' <param name="invmapid">The location the check</param>
    ''' <returns>A boolean that tells if the location is dedicated</returns>
    ''' <remarks></remarks>
    Public Function IsDedicated(item As String, invmapid As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Return Induction.ProcessPutAways.IsDedicated(item, invmapid, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Moves a transaction into a tote in open transactions
    ''' </summary>
    ''' <param name="OTID">The open transaction id to be assigned</param>
    ''' <param name="SplitQty">The split quantity</param>
    ''' <param name="Qty">The quantity</param>
    ''' <param name="ToteID">The tote id to be assigned</param>
    ''' <param name="BatchID">The bacth to be assigned</param>
    ''' <param name="Item">The item number of the transaction</param>
    ''' <param name="UF1">The user field 1 of the transaction</param>
    ''' <param name="UF2">The user fiedl 2 of the transaction</param>
    ''' <param name="Lot">The lot number of the transaction</param>
    ''' <param name="Ser">The serial nuumber of the transaction</param>
    ''' <param name="TotePos">The tote position</param>
    ''' <param name="Cell">The cell of the transaction</param>
    ''' <param name="Warehouse">The warehouse of the transaction</param>
    ''' <param name="ExpDate">The experiation date of the transaction</param>
    ''' <param name="Revision">The revision of the transaction</param>
    ''' <param name="Zone">The zone of the transaction</param>
    ''' <param name="Carousel">The carousel of the transaction</param>
    ''' <param name="Row">The row of the transaction</param>
    ''' <param name="Shelf">The shelf of the transaction</param>
    ''' <param name="Bin">The bin of the transaction</param>
    ''' <param name="InvMapID">The inventory map id of the transaction</param>
    ''' <param name="LocMaxQty">The max quantiry for the location</param>
    ''' <param name="Reel">If it's a reel transaction</param>
    ''' <param name="Dedicate">If it's dedicated</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TaskComplete(OTID As Integer, SplitQty As Integer, Qty As Integer, ToteID As String, BatchID As String, Item As String, _
                                            UF1 As String, UF2 As String, Lot As String, Ser As String, TotePos As Integer, Cell As String, Warehouse As String, ExpDate As String, Revision As String, _
                                            Zone As String, Carousel As String, Row As String, Shelf As String, Bin As String, InvMapID As Integer, LocMaxQty As Integer, Reel As Boolean, Dedicate As Boolean,
                                            OrderNumber As String) As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(
            Function() As Integer
                Return Induction.ProcessPutAways.TaskComplete(Context.QueryString.Get("WSID"), Context.User.Identity.Name, OTID, SplitQty, Qty, ToteID, _
                                                              BatchID, Item, UF1, UF2, Lot, Ser, TotePos, Cell, Warehouse, ExpDate, Revision, Zone, Carousel, _
                                                              Row, Shelf, Bin, InvMapID, LocMaxQty, Reel, Dedicate, OrderNumber)
            End Function)
    End Function

    ''' <summary>
    ''' Gets the specified invmapid's location name by identifying it in location zones.
    ''' </summary>
    ''' <param name="invmapid">The inventory map id to look up</param>
    ''' <returns>A string that tells the location name for the desired inventory map id</returns>
    ''' <remarks></remarks>
    Public Function GetLocationName(invmapid As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim reader As SqlDataReader = Nothing
                                                    Dim locname As String = ""
                                                    Try
                                                        reader = RunSPArray("selIMLocName", Context.QueryString.Get("WSID"), {{"@InvMapID", invmapid, intVar}})
                                                        If reader.HasRows Then
                                                            If reader.Read Then
                                                                locname = CheckDBNull(reader(0))
                                                            End If
                                                        End If
                                                    Catch ex As Exception
                                                        Debug.Print(ex.ToString())
                                                        insertErrorMessages("ProcessPutAwaysHub", "GetLocationName", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(reader) Then
                                                            reader.Dispose()
                                                        End If
                                                    End Try
                                                    Return locname
                                                End Function)
    End Function

    ''' <summary>
    ''' Checks if there is a need for qty of the specified item in forward locations (carousel needs item, but we would normally assign to bulk first).
    ''' </summary>
    ''' <param name="item">The item number to check for</param>
    ''' <returns>An integer that tells the quantity that is needed for the given item numebr</returns>
    ''' <remarks></remarks>
    Public Function CheckForwardLocations(item As String) As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Dim reader As SqlDataReader = Nothing
                                                     Dim user As String = Context.User.Identity.Name
                                                     Dim WSID As String = Context.QueryString.Get("WSID")
                                                     Dim FwdQty As Integer = 0
                                                     Try
                                                         reader = RunSPArray("selIMFwdQty", WSID, {{"@Item", item, strVar}})
                                                         If reader.HasRows Then
                                                             If reader.Read Then
                                                                 If Not IsDBNull(reader(0)) Then
                                                                     FwdQty = CInt(reader(0))
                                                                 End If
                                                             End If
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.Print(ex.ToString())
                                                         insertErrorMessages("ProcessPutAwaysHub", "CheckForwardLocations", ex.ToString(), user, WSID)
                                                     Finally
                                                         If Not IsNothing(reader) Then
                                                             reader.Dispose()
                                                         End If
                                                     End Try
                                                     Return FwdQty
                                                 End Function)
    End Function

    ''' <summary>
    ''' Completes a cross dock transaction.
    ''' </summary>
    ''' <param name="pick"># items being picked</param>
    ''' <param name="put"># items being put to this transaction</param>
    ''' <param name="reel">Whether the item is a reel or not.</param>
    ''' <param name="ser">serial number</param>
    ''' <param name="htid">host transaction id</param>
    ''' <param name="rpid">reprocess id (Open Transactions Temp ID)</param>
    ''' <param name="otid">Open Transactions ID</param>
    ''' <param name="item">item number</param>
    ''' <param name="uf1">user field 1</param>
    ''' <param name="toteID">The tote id</param>
    ''' <param name="order">the order number</param>
    ''' <param name="invmapid">the inventory mpa id</param>
    ''' <param name="whse">warehouse</param>
    ''' <param name="batch">batch id</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function CompletePick(pick As Integer, put As Integer, reel As Boolean, ser As String, htid As String, rpid As Integer, otid As Integer, item As String, uf1 As String,
                                 toteID As String, order As String, invmapid As Integer, whse As String, batch As String) As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Dim WSID As String = Context.QueryString.Get("WSID")
                                                     Dim user As String = Context.User.Identity.Name
                                                     Dim reader As SqlDataReader = Nothing
                                                     Dim OT_ID As Integer = 0
                                                     Try
                                                         reader = RunSPArray("updCrossDockTransactions", WSID, {{"@PickTransQty", pick, intVar}, {"@PutTransQty", put, intVar}, {"@Reel", CastAsSqlBool(reel), intVar},
                                                                                                        {"@SerNum", ser, strVar}, {"@HostTransID", htid, strVar}, {"@RPID", rpid, intVar}, {"@OTID", otid, intVar},
                                                                                                        {"@User", user, strVar}, {"@Item", item, strVar}, {"@UF1", uf1, strVar}, {"@ToteID", toteID, strVar},
                                                                                                        {"@Order", order, strVar}, {"@InvMapID", invmapid, intVar}, {"@Whse", whse, strVar}, {"@BatchID", batch, strVar}})

                                                         If reader.HasRows Then
                                                             reader.Read()
                                                             OT_ID = CInt(reader("OTID"))
                                                         End If

                                                     Catch ex As Exception
                                                         Debug.Print(ex.ToString())
                                                         insertErrorMessages("ProcessPutAwaysHub", "CompletePick", ex.ToString(), user, WSID)
                                                     Finally
                                                         If Not IsNothing(reader) Then
                                                             reader.Dispose()
                                                         End If
                                                     End Try
                                                     Return OT_ID
                                                 End Function)
    End Function

    ''' <summary>
    ''' Marks a batch as complete.  Deletes the batch from the tote setup queue.  Depending on preferences it may also erase the batch id.
    ''' </summary>
    ''' <param name="batchID">The batch id to mark as complete</param>
    ''' <returns>An object that tells if the operation was successful and contaisn the next batch id value</returns>
    ''' <remarks></remarks>
    Public Function CompleteBatch(batchID As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Return Induction.ProcessPutAways.CompleteBatch(batchID, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates the inventory map to clear out any reservations made in the map when the page is closed.  This function is tied to the unload event in ProcessPutAwayDetail.js
    ''' Sets Item Number = NULL, Item Qty = 0 WHERE WSID + "-IM" = Item Number
    ''' </summary>
    ''' <returns>A boolean that tells if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ClearLocationReservations() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Dim WSID As String = Context.QueryString.Get("WSID")
                Try
                    RunActionSP("delIMReservations", WSID, {{"@WSID", WSID, strVar}})
                Catch ex As Exception
                    Debug.Print(ex.ToString())
                    insertErrorMessages("ProcessPutAwaysHub", "ClearLocationReservations", ex.ToString(), Context.User.Identity.Name, WSID)
                    Return False
                End Try
                Return True
            End Function)
    End Function

    ''' <summary>
    ''' Checks if a batch exists in the tote setup queue for the current workstation.
    ''' </summary>
    ''' <param name="batchID">The batch id to check</param>
    ''' <returns>A boolean that tells if the batch exists</returns>
    ''' <remarks></remarks>
    Public Function BatchExists(batchID As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Return Induction.ProcessPutAways.BatchExists(batchID, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            End Function)
    End Function

    ''' <summary>
    ''' Validates each tote to see if it already exists 
    ''' </summary>
    ''' <param name="ToteIDs">The totes to check for</param>
    ''' <returns>The tote that was invalid</returns>
    Public Function validateTotes(ToteIDs As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function()
                                                    Dim InvalidTote As String = ""
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        For Each Tote In ToteIDs.Split(",")
                                                            DataReader = RunSPArray("selOTValidTote", Context.QueryString.Get("WSID"), {{"@ToteID", Tote, strVar}})

                                                            If DataReader.HasRows Then
                                                                DataReader.Read()
                                                                InvalidTote = DataReader("Tote ID")
                                                                Exit For
                                                            End If

                                                            DataReader.Close()
                                                        Next
                                                    Catch ex As Exception
                                                        insertErrorMessages("PickToteSetupHub", "validateTotes", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Error"
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return InvalidTote
                                                End Function)
    End Function


    ''' <summary>
    ''' Reserves this location if needed for the wokrstation. This is called specifically for choose location
    ''' </summary>
    ''' <returns>A boolean that tells if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ReserveLocation(InvMapID As Integer, PrevZone As String, Dedicated As Boolean) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Dim WSID As String = Context.QueryString.Get("WSID")
                Dim DataReader As SqlDataReader = Nothing
                Try

                    If Not Induction.InductionLocAss.ClearReservedNotComplete(PrevZone, Dedicated, Context.User.Identity.Name, WSID) Then
                        Return False
                    End If

                    DataReader = RunSPArray("ST_selInventoryMapByInvMapID", WSID, {{"@InvMapID", InvMapID, intVar}})

                    If DataReader.HasRows AndAlso DataReader.Read() Then
                        'If the selected inv mpa record does not have an item, set to workstation
                        If IsDBNull(DataReader("Item Number")) OrElse DataReader("Item Number") = "" Then
                            RunActionSP("updIMNewLocMachineName", WSID, {{"@Item", WSID & "-IM", strVar}, {"@rt", CastAsSqlBool(False), intVar}, {"@InvMapID", InvMapID, intVar}})
                        End If
                    Else
                        'No location record
                        Return False
                    End If


                Catch ex As Exception
                    Debug.Print(ex.ToString())
                    insertErrorMessages("ProcessPutAwaysHub", "ReserveLocation", ex.ToString(), Context.User.Identity.Name, WSID)
                    Return False
                Finally
                    If Not IsNothing(DataReader) Then
                        DataReader.Dispose()
                    End If
                End Try
                Return True
            End Function)
    End Function

End Class
