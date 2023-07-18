' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web.Certificates

Namespace Induction
    Public Class ProcessPutAways
        ''' <summary>
        ''' Typeahead for the Batch ID fields on process put aways page
        ''' </summary>
        ''' <param name="BatchID">The batch id to get suggestions from</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function BatchIDTypeahead(BatchID As String, WSID As String, user As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim batches As New List(Of Object)
            Try
                DataReader = RunSPArray("selIMBatchIDTA", WSID, {{"@BatchID", BatchID, strVar}, {"@TransType", "Put Away", strVar}, {"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        batches.Add(New With {.BatchID = DataReader("Batch ID"), .ZoneLabel = DataReader("Zone Label")})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ProcessPutAways", "BatchIDTypeahead", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return batches
        End Function

        ''' <summary>
        ''' Gets the Totes contained in the specified batch
        ''' </summary>
        ''' <param name="draw">parameter needed for datatables</param>
        ''' <param name="BatchID">The desired bacth to display</param>
        ''' <param name="SortOrder">The direction of the sort</param>
        ''' <param name="SortColumn">The column index that the sort is being applied to</param>
        ''' <param name="WSID">The workstation that is curently being worked on</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <returns>A table object that contaisn the data for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTotesTable(draw As Integer, BatchID As String, SortOrder As String, SortColumn As Integer, WSID As String, user As String) As TableObject
            Dim reader As SqlDataReader = Nothing
            Dim totes As New TableObject(draw, 0, 0, New List(Of List(Of String)))
            If BatchID.Trim() = "" Then Return totes
            Try
                Dim cols As New List(Of String) From {"Tote Position", "Tote ID", "Cells", "Tote Qty", "Zone Label"}
                reader = RunSPArray("selIMTotesDT", WSID, {{"@BatchID", BatchID, strVar}, {"@sortOrder", SortOrder, strVar}, _
                                                           {"@sortColumn", cols(SortColumn), strVar}})

                Dim index As Integer = 0
                Dim innerlist As New List(Of String)
                While reader.HasRows
                    While reader.Read
                        If index = 0 Then
                            totes.recordsTotal = reader(0)
                            totes.recordsFiltered = reader(0)
                        Else
                            For x As Integer = 0 To reader.FieldCount - 1
                                innerlist.Add(reader(x))
                            Next
                            totes.data.Add(innerlist)
                            innerlist = New List(Of String)
                        End If
                    End While
                    index += 1
                    reader.NextResult()
                End While
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetTotesTable", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return totes
        End Function

        ''' <summary>
        ''' Deallocates or clears a batch or tote.
        ''' </summary>
        ''' <param name="batch">If a batch or tote is being deleted</param>
        ''' <param name="toteid">The tote id to deletd if tote</param>
        ''' <param name="batchid">The batch id to delete if batch</param>
        ''' <param name="transtype">The transaction type of the batch or tote</param>
        ''' <param name="deallocate">Tells whether to deallocate or delete</param>
        ''' <param name="PageFrom">The page that the record is being deleted from</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>A boolean telling if the operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function DeleteBatchOrTote(batch As Boolean, toteid As String, batchid As String, transtype As String, deallocate As Boolean, PageFrom As String, WSID As String, user As String) As Boolean
            Try
                RunActionSP("delIMBatchTote", WSID, {{"@IsBatchNotTote", IIf(batch, 1, 0), intVar}, {"@ToteID", toteid, strVar}, _
                                                     {"@BatchID", batchid, strVar}, {"@WSID", WSID, strVar}, {"@TransType", transtype, strVar}, _
                                                     {"@DeAllocate", IIf(deallocate, 1, 0), intVar}, {"@PageFrom", PageFrom, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessTransactions", "DeleteBatchOrTote", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Clears all batches created by induction manager (import filename ends in "-IM") where the WSID matches the passed value in the Tote Setup Queue (batch must exist in Tote Setup Queue and not be completed).
        ''' </summary>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="user">The user that currently logged in</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function DeleteAllBatches(WSID As String, user As String) As Boolean
            Try
                RunActionSP("delIMBatchAll", WSID, {{"@WSID", WSID, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessTransactions", "DeleteAllBatches", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets the Next Tote field from System Preferences
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The suer that currently logged in</param>
        ''' <returns>An integer that tells what the next tote is</returns>
        ''' <remarks></remarks>
        Public Shared Function GetNextTote(WSID As String, user As String) As Integer
            Dim dr As SqlDataReader = Nothing
            Dim tote As Integer = -1
            Try
                dr = RunSPArray("selNextToteID", WSID, {{"nothing"}})
                If dr.HasRows Then
                    If dr.Read Then
                        tote = dr(0)
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetNextTote", ex.Message, user, WSID)
            Finally
                If Not IsNothing(dr) Then
                    dr.Dispose()
                End If
            End Try
            Return tote
        End Function

        

        ''' <summary>
        ''' Gets the Next Tote field from System Preferences
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The suer that currently logged in</param>
        ''' <returns>An integer that tells what the next tote is</returns>
        ''' <remarks></remarks>
        Public Shared Function GetAndUpdateNextTote(WSID As String, user As String) As Integer
            Dim dr As SqlDataReader = Nothing
            Dim tote As Integer = -1
            Try
                dr = RunSPArray("selNextToteID", WSID, {{"nothing"}})
                If dr.HasRows Then
                    If dr.Read Then
                        tote = dr(0)
                    End If
                End If
                RunActionSP("updNextToteID", WSID, {{"@ToteID", tote + 1, intVar}})

            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetNextTote", ex.Message, user, WSID)
            Finally
                If Not IsNothing(dr) Then
                    dr.Dispose()
                End If
            End Try
            Return tote
        End Function

        ''' <summary>
        ''' Updates the Next Tote ID field in System Preferences
        ''' </summary>
        ''' <param name="tote">The next tote id value</param>
        ''' <param name="WSID">The workstation that us currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateNextTote(tote As Integer, WSID As String, user As String) As Boolean
            Try
                RunActionSP("updNextToteID", WSID, {{"@ToteID", tote, intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "UpdateNextTote", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Selects all totes for a batch for the setup screen and dictates whether the specified returned tote(s) are locked in the batch because of allocated in open transactions.
        ''' </summary>
        ''' <param name="BatchID">The desired batch to get the totes from</param>
        ''' <param name="user">The suer that currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of objects that contains the totes for the given batch</returns>
        ''' <remarks></remarks>
        Public Shared Function GetBatchTotes(BatchID As String, user As String, WSID As String) As List(Of Object)
            Dim batch As New List(Of Object)
            Dim reader As SqlDataReader = Nothing
            Try
                reader = RunSPArray("selIMBatchTotes", WSID, {{"@BatchID", BatchID, strVar}})
                If reader.HasRows Then
                    While reader.Read
                        If Not IsDBNull(reader("WSID")) Then
                            If reader("WSID").ToString().ToLower() = WSID.ToLower() Then
                                batch.Add(New With {.cells = reader("Cells"), .tote = reader("Tote ID"), .tlocked = reader("Locked"), .zonelabel = reader("Zone Label"), _
                                                    .position = reader("Tote Position"), .wsname = "self"})
                            Else
                                batch.Add(New With {.wsname = getPCName(reader("WSID"))})
                                Exit While
                            End If
                        End If
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAwaysHub", "GetBatchTotes", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return batch
        End Function

        ''' <summary>
        ''' Gets the zones available to the current workstation along with whether they can be used in a new batch or if they are being used in the batch already.
        ''' </summary>
        ''' <param name="BatchID">The currently displayed batch id</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that currently being worked on</param>
        ''' <returns>A list of object that contains the zones and their data</returns>
        ''' <remarks></remarks>
        Public Shared Function GetAvailableZones(BatchID As String, user As String, WSID As String) As List(Of Object)
            Dim dr As SqlDataReader = Nothing
            Dim zones As New List(Of Object)
            Dim z As New List(Of String)
            Dim thisBatch As New List(Of String)
            Try
                dr = RunSPArray("selIMBatchZones", WSID, {{"@BatchID", BatchID, strVar}, {"@WSID", WSID, strVar}})
                For index As Integer = 0 To 1
                    If dr.HasRows Then
                        While dr.Read
                            If index = 0 Then
                                If Not IsDBNull(dr(0)) And Not IsDBNull(dr(1)) Then
                                    Dim labels As String = dr("Zone Label")
                                    Dim lSplit As List(Of String) = labels.Replace("Zones:", "").Trim().Split(" ").ToList()
                                    Dim batch As String = dr("Batch ID")

                                    If batch = BatchID Then
                                        thisBatch = lSplit
                                    Else
                                        z.AddRange(lSplit)
                                    End If
                                End If
                            Else
                                If Not IsDBNull(dr("Zone")) Then
                                    zones.Add(New With {.zone = dr("Zone"), .location = IIf(IsDBNull(dr("Location Name")), "", dr("Location Name")), .loctype = dr("Location Type"), _
                                                        .staging = IIf(CBool(dr("Staging Zone")), "Yes", "No"), _
                                        .selected = thisBatch.Contains(dr("Zone")), _
                                        .available = Not z.Contains(dr("Zone"))})
                                End If
                            End If
                        End While
                    End If
                    dr.NextResult()
                Next index
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetAvailableZones", ex.Message, user, WSID)
            Finally
                If Not IsNothing(dr) Then
                    dr.Dispose()
                End If
            End Try
            Return zones
        End Function

        ''' <summary>
        ''' Processes a batch to move it from the "Tote Setup" table (table in Access, which is no longer included) to the Tote Setup Queue.
        ''' Until this function is called the batch being worked on in the Tote Setup area will not exist in SQL or the setup queue.
        ''' </summary>
        ''' <param name="BatchID">The desired batch to the process</param>
        ''' <param name="ZoneLabel">The zone label that is assigned to the batch</param>
        ''' <param name="Totes">The totes within the batch</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>A boolean telling if the oepration completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function ProcessBatch(BatchID As String, ZoneLabel As String, Totes As List(Of String), WSID As String, user As String) As Boolean
            Try
                RunActionSP("insIMBatchPutAway", WSID, {{"@BatchID", BatchID, strVar}, {"@ZoneLabel", ZoneLabel, strVar}, {"@WSID", WSID, strVar}, {"@Totes", Totes(0), strVar}, _
                                                        {"@Cells", Totes(1), strVar}, {"@Positions", Totes(2), strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "ProcessBatch", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets the transactions that can be assigned to a batch at the current time.  Includes "ineligible" transactions for the currently selected batch (zones do not match).
        ''' First identifies what the input type is and then acts appropriately to return relevant records.
        ''' </summary>
        ''' <param name="input">The value that is going to be checked</param>
        ''' <param name="inputType">The type of field that the input value is</param>
        ''' <param name="lowerBound">The lowest row number</param>
        ''' <param name="upperBound">The highest row number</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A st paging model for the javascript plugin</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTransactionsForTote(input As String, inputType As String, pluginWhereClause As String, lowerBound As Integer, upperBound As Integer, user As String, WSID As String) As STPagingResult
            Dim dr As SqlDataReader = Nothing
            Dim result As New STPagingResult(False, New List(Of List(Of String)), "", 0, 0, New With {.inputType = "", .subCategory = "", .itemNumber = "", .itemDescription = "", .whseSens = False})
            Dim innerList As New List(Of String)
            Try
                dr = RunSPArray("selIMTransForBatch", WSID, {{"@WSID", WSID, strVar}, {"@InputType", inputType, strVar}, {"@InputValue", input, strVar}, _
                                                             {"@StartRow", lowerBound, intVar}, {"@EndRow", upperBound, intVar}, {"@PluginWhere", pluginWhereClause, strVar}})
                If dr Is Nothing Then
                    insertErrorMessages("ProcessPutAways", "GetTransactionsForTote", $"Error in selIMTransForBatch SP, dataReader not set to an instance of an object. input: {input}, inputType: {inputType}, pluginWhereClause: {pluginWhereClause}", user, WSID)
                    result.extraData.inputType = inputType
                    result.success = 0
                    Return result
                End If

                If dr.HasRows Then
                    If dr.Read Then
                        result.extraData.inputType = dr("Input Type")
                        result.message = IIf(IsDBNull(dr("Failure Reason")), "", dr("Failure Reason"))
                        result.success = (dr("Success") = 1)
                    End If
                    ' if we successfully identified the input code then we may have more data
                    If result.success Then
                        dr.NextResult()
                        ' if we got the item number, description and sub category then look for the transactions in the next result set.
                        If dr.HasRows Then
                            If dr.Read Then
                                result.extraData.subCategory = IIf(IsDBNull(dr("Sub Category")), "", dr("Sub Category"))
                                result.extraData.itemNumber = dr("Item Number")
                                result.extraData.itemDescription = dr("Description")
                                result.extraData.whseSens = IIf(IsDBNull(dr("Warehouse Sensitive")), False, dr("Warehouse Sensitive"))
                            End If
                            dr.NextResult()
                            ' if we got transactions matching the requirements
                            If dr.HasRows Then
                                While dr.Read
                                    For x As Integer = 0 To dr.FieldCount - 1
                                        innerList.Add(IIf(IsDBNull(dr(x)), "", dr(x)))
                                    Next
                                    result.pages.Add(innerList)
                                    innerList = New List(Of String)
                                End While
                                dr.NextResult()
                                If dr.HasRows Then
                                    If dr.Read Then
                                        result.numRecords = dr(0)
                                    End If
                                End If
                                dr.NextResult()
                                If dr.HasRows Then
                                    If dr.Read Then
                                        result.filteredRecords = dr(0)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetTransactionsForTote", ex.Message, user, WSID)
            Finally
                If dr IsNot Nothing Then
                    dr.Dispose()
                End If
            End Try
            Return result
        End Function

        ''' <summary>
        ''' Gets the next serial number from system preferences and increments it by the number specified.
        ''' </summary>
        ''' <param name="NumReels">The number to increment the serial number by</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A long (number too big for integer) that contains the new serial number value</returns>
        ''' <remarks></remarks>
        Public Shared Function GetNextSerialNumber(NumReels As Integer, user As String, WSID As String) As Long
            Dim datareader As SqlDataReader = Nothing
            Dim sn As Long = 0
            Try
                datareader = RunSPArray("selUpdIMNextSerial", WSID, {{"@IncrementBy", NumReels, intVar}})
                If datareader.HasRows Then
                    If datareader.Read() Then
                        sn = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetNextSerialNumber", ex.Message, user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return sn
        End Function

        ''' <summary>
        ''' Updates the inventory master record for the specified item number with new cell sizes and velocity codes
        ''' </summary>
        ''' <param name="item">The item number ot update</param>
        ''' <param name="ccell">New cell size value</param>
        ''' <param name="bcell">New bulk cell size value</param>
        ''' <param name="cfcell">New carton flow cell size value</param>
        ''' <param name="cvel">New velocity code</param>
        ''' <param name="bvel">New bulk velocity code</param>
        ''' <param name="cfvel">New carton flow velocity code</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateInventoryMaster(item As String, ccell As String, bcell As String, cfcell As String, cvel As String, bvel As String, cfvel As String, pzone As String, szone As String, user As String, WSID As String) As Boolean
            Try
                RunActionSP("updIMItem", WSID, {{"@User", user, strVar}, {"@WSID", WSID, strVar}, {"@CCell", ccell, strVar}, {"@BCell", bcell, strVar}, {"@CFCell", cfcell, strVar},
                                                {"@CVel", cvel, strVar}, {"@BVel", bvel, strVar}, {"@CFVel", cfvel, strVar}, {"@ItemNumber", item, strVar}, {"@PZone", pzone, strVar},
                                                {"@SZone", szone, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "UpdateInventoryMaster", ex.Message, user, WSID)
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets the data related to the open transaction specified with a focus on the item contained in the OT record.  
        ''' If there is no OTID (OTID less than or equal to 0) then it retrieves as much data as possible from the inventory table instead.
        ''' </summary>
        ''' <param name="OTID">The open transactions id for the desired record</param>
        ''' <param name="itemNum">The desied item number</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <returns>A list of string that contains the infomration for the item number</returns>
        ''' <remarks></remarks>
        Public Shared Function GetItemDetails(OTID As Long, itemNum As String, WSID As String, user As String) As Object
            Dim datareader As SqlDataReader = Nothing
            Dim item As New List(Of String)
            Try
                datareader = RunSPArray("selIMItemDetails", WSID, {{"@OTID", OTID, intVar}, {"@ItemNumber", itemNum, strVar}})
                If datareader.HasRows Then
                    If datareader.Read Then
                        For x As Integer = 0 To datareader.FieldCount - 1
                            If IsDBNull(datareader(x)) Then
                                item.Add("")
                            Else
                                item.Add(datareader(x))
                            End If
                        Next
                    End If
                End If

                'Primary Zone
                item(37) = item(37).ToLower
                'Secondary Zone
                item(38) = item(38).ToLower
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetItemDetails", ex.Message, user, WSID)
                Return New With {.Succ = False}
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try

            Return New With {.Succ = True, .Detail = item}
        End Function

        ''' <summary>
        ''' Gets the top 1 batch ID from the Tote Setup Queue which matches the WSID (w/ no sorting specified) to prepopulate the batch id field.
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currenlty logged in</param>
        ''' <returns>A string that contaisn the batch id</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTop1Batch(WSID As String, user As String) As String
            Dim datareader As SqlDataReader = Nothing
            Dim batch As String = ""
            Try
                datareader = RunSPArray("selIMTopBatch", WSID, {{"@WSID", WSID, strVar}})
                If datareader.HasRows Then
                    If datareader.Read Then
                        If Not IsDBNull(datareader) Then
                            batch = datareader(0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetTop1Batch", ex.Message, user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return batch
        End Function

        ''' <summary>
        ''' Creates a series of reel put away transactions in the open transactions table for use in the put away batch screen.  The serial numbers provided here are essential for use with assigning reels to a batch/tote.
        ''' </summary>
        ''' <param name="item">The item number for the transactions</param>
        ''' <param name="reels">The data for the trasnactions</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that is currently logged on</param>
        ''' <returns>A list of intenger that contains the open trasnaction ids that were created</returns>
        ''' <remarks></remarks>
        Public Shared Function CreateReels(item As String, ByVal reels As List(Of List(Of String)), WSID As String, user As String) As List(Of Integer)
            Dim reader As SqlDataReader = Nothing
            Dim otids As New List(Of Integer)
            Try
                reader = RunSPArray("insIMReelFirst", WSID, {{"@SN", reels(0)(0), strVar}, {"@Qty", reels(0)(1), intVar}, {"@Order", reels(0)(2), strVar},
                                                         {"@Lot", reels(0)(3), strVar}, {"@UF1", reels(0)(4), If(reels(0)(4) = "", nullVar, strVar)},
                                                         {"@UF2", reels(0)(5), If(reels(0)(5) = "", nullVar, strVar)}, {"@Warehouse", reels(0)(6), If(reels(0)(6) = "", nullVar, strVar)},
                                                         {"@ExpDate", reels(0)(7), dteVar}, {"@Notes", reels(0)(8), If(reels(0)(8) = "", nullVar, strVar)}, {"@Item", item, strVar}, {"@User", user, strVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            otids.Add(CInt(reader(0)))
                        End If
                    End If
                End If
                reader.Dispose()
                reels.RemoveAt(0)
                For Each reel As List(Of String) In reels
                    reader = RunSPArray("insIMReels", WSID, {{"@OTID", otids(0), intVar}, {"@SN", reel(0), strVar}, {"@Qty", reel(1), intVar}, {"@Order", reel(2), strVar},
                                                     {"@Lot", reel(3), strVar}, {"@UF1", reel(4), If(reel(4) = "", nullVar, strVar)},
                                                     {"@UF2", reel(5), If(reel(5) = "", nullVar, strVar)}, {"@Warehouse", reel(6), If(reel(6) = "", nullVar, strVar)},
                                                     {"@ExpDate", reel(7), dteVar}, {"@Notes", reel(8), If(reel(8) = "", nullVar, strVar)}})
                    If reader.HasRows Then
                        If reader.Read Then
                            If Not IsDBNull(reader(0)) Then
                                otids.Add(CInt(reader(0)))
                            End If
                        End If
                    End If
                    reader.Dispose()
                Next
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "CreateReels", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return otids
        End Function

        ''' <summary>
        ''' Gets the location typeahead for the current batch.  Some locations are not "valid" because they are may be outside the specified batch's zones or because of reel tracking bad containers/dedication.
        ''' </summary>
        ''' <param name="location">The value to get suggestions for</param>
        ''' <param name="warehouse">The warehouse value to filter invalid results</param>
        ''' <param name="serial">The serial number in order to filter invalid results</param>
        ''' <param name="lot">The lot number in order to filter invalid results</param>
        ''' <param name="ccell">the cell size in order to filter invalid results</param>
        ''' <param name="cvel">the velocity code in order to filter invalid results</param>
        ''' <param name="bcell">the bulk cell size in order to filter invalid results</param>
        ''' <param name="bvel">the bulk velocity code in order to filter invalid results</param>
        ''' <param name="cfcell">the carton flow cell size in order to filter invalid results</param>
        ''' <param name="cfvel">the carton flow velocity code in order to filter invalid results</param>
        ''' <param name="item">the item number in order to filter invalid results</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that currently being worked on</param>
        ''' <returns>A list of object that contains the possible suggestions for the tpyeahead</returns>
        ''' <remarks></remarks>
        Public Shared Function GetBatchLocationTypeahead(location As String, warehouse As String, serial As String, lot As String, ccell As String, cvel As String, _
                                                         bcell As String, bvel As String, cfcell As String, cfvel As String, item As String, user As String, WSID As String) As List(Of Object)
            Dim datareader As SqlDataReader = Nothing
            Dim locations As New List(Of Object)
            Try
                datareader = RunSPArray("selIMLocationTA", WSID, {{"@Warehouse", warehouse, strVar}, {"@SerialNumber", serial, strVar}, {"@LotNumber", lot, strVar}, {"@CCell", ccell, strVar}, _
                                                                  {"@CVel", cvel, strVar}, {"@BCell", bcell, strVar}, {"@BVel", bvel, strVar}, {"@CFCell", cfcell, strVar}, _
                                                                  {"@CFVel", cfvel, strVar}, {"@ItemNumber", item, strVar}, {"@Location", location, strVar}})
                If datareader.HasRows Then
                    While datareader.Read
                        locations.Add(New With {.InvMapID = datareader("Inv Map ID"), .LocNum = datareader("Location Number"), _
                                                .Warehouse = CheckDBNull(datareader("Warehouse")), _
                                                .Location = CheckDBNull(datareader("Location")), _
                                                .ItemNumber = CheckDBNull(datareader("Item Number")), _
                                                .CellSize = CheckDBNull(datareader("Cell Size")), _
                                                .Velocity = CheckDBNull(datareader("Velocity Code")), _
                                                .Qty = CheckDBNull(datareader("In Stock")), _
                                                .Max = CheckDBNull(datareader("Max Qty")), _
                                                .QtyPut = CheckDBNull(datareader("Qty Alloc Put Away")), _
                                                .LocType = datareader("Location Type"), .Zone = CheckDBNull(datareader("Zone")), _
                                                .Carousel = CheckDBNull(datareader("Carousel")), .Row = CheckDBNull(datareader("Row")), _
                                                .Shelf = CheckDBNull(datareader("Shelf")), .Bin = CheckDBNull(datareader("Bin"))})
                    End While
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetBatchLocationTypeahead", ex.Message, user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return locations
        End Function

        ''' <summary>
        ''' Used to check and get transactions related to the cross dock process.  If there are returned records and the Success flag on the STPagingResult object is true then there is a cross dock opportunity for the item/warehouse.
        ''' </summary>
        ''' <param name="lowerbound">The lowest row number to get records from</param>
        ''' <param name="upperbound">The highest row number ot get records from</param>
        ''' <param name="item">The item number to find a cross dock for</param>
        ''' <param name="warehouse">The warehouse ot get a cross dock for</param>
        ''' <param name="pluginWhere">Where clause that was applied</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A st paging model for the javascript plugin</returns>
        ''' <remarks></remarks>
        Public Shared Function GetCrossDock(lowerbound As Integer, upperbound As Integer, item As String, warehouse As String, pluginWhere As String, user As String, WSID As String) As STPagingResult
            Dim datareader As SqlDataReader = Nothing
            Dim transactions As New List(Of List(Of String))
            Dim innerlist As New List(Of String)
            Dim needed As Long = 0, allocated As Long = 0
            Dim page As New STPagingResult(True, New List(Of List(Of String)), "", 0, 0, New With {.allocated = 0, .backorder = 0})
            Try
                datareader = RunSPArray("selIMCrossDock", WSID, {{"@PluginWhere", pluginWhere, strVar}, {"@ItemNumber", item, strVar}, {"@Whse", warehouse, strVar}, _
                                                                 {"@StartRow", lowerbound, intVar}, {"@EndRow", upperbound, intVar}})
                If datareader.HasRows Then
                    While datareader.Read
                        For x As Integer = 0 To datareader.FieldCount - 1
                            If Not IsDBNull(datareader(x)) Then
                                innerlist.Add(datareader(x))
                            Else
                                innerlist.Add("")
                            End If
                        Next
                        transactions.Add(innerlist)
                        innerlist = New List(Of String)
                    End While
                    page.pages = transactions
                    datareader.NextResult()
                    If datareader.HasRows Then
                        If datareader.Read Then
                            If Not IsDBNull(datareader(0)) Then
                                page.extraData.backorder = datareader(0)
                            End If
                            If Not IsDBNull(datareader(1)) Then
                                page.extraData.allocated = datareader(1)
                            End If
                            If Not IsDBNull(datareader(2)) Then
                                page.filteredRecords = datareader(2)
                            End If
                        End If
                    End If
                    datareader.NextResult()
                    If datareader.HasRows Then
                        If datareader.Read Then
                            If Not IsDBNull(datareader(0)) Then page.numRecords = datareader(0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "CrossDockExists", ex.Message, user, WSID)
                page.success = False
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return page
        End Function

        ''' <summary>
        ''' Gets the batches eligible for a particular zone.  Used when the current batch does not match the location that a transaction was assigned.
        ''' </summary>
        ''' <param name="zone">The zone to get batches from</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The user that si currently logged on</param>
        ''' <returns>A string that contains the batch id for the zone</returns>
        ''' <remarks></remarks>
        Public Shared Function GetBatchByZone(zone As String, WSID As String, user As String) As String
            Dim datareader As SqlDataReader = Nothing
            Dim batch As String = ""
            Try
                datareader = RunSPArray("selIMBatchByZone", WSID, {{"@WSID", WSID, strVar}, {"@Zone", zone, strVar}})
                If datareader.HasRows Then
                    If datareader.Read Then
                        If Not IsDBNull(datareader(0)) Then
                            batch = datareader(0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetBatchByZone", ex.Message, user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return batch
        End Function

        ''' <summary>
        ''' Gets Open Transactions Temp (Reprocess) details for a particular transaction for the cross docking area.
        ''' </summary>
        ''' <param name="id">The id of the transaction to get the info of</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of string that contains the info for the desired reprocess transaction</returns>
        ''' <remarks></remarks>
        Public Shared Function GetRPDetail(id As String, user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing
            Dim rp As New List(Of String)
            Try
                datareader = RunSPArray("selIMReprocessByID", WSID, {{"@ID", id, intVar}})
                If datareader.HasRows Then
                    If datareader.Read Then
                        For x As Integer = 0 To datareader.FieldCount - 1
                            If IsDBNull(datareader(x)) Then
                                rp.Add("")
                            Else
                                rp.Add(datareader(x))
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetRPDetail", ex.Message, user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return rp
        End Function

        ''' <summary>
        ''' Gets both tables for the dedicated container modal.
        ''' </summary>
        ''' <param name="draw">Required paramater for datatables</param>
        ''' <param name="top">Which table (top or bottom = true or false)</param>
        ''' <param name="locNum">The location number</param>
        ''' <param name="item">The item number</param>
        ''' <param name="invMapID">The inventory map id</param>
        ''' <param name="startRow">The first row number where records should be pulled from</param>
        ''' <param name="endRow">The last row number where records should be pulled from</param>
        ''' <param name="sortColumn">The column index that is being sorted on</param>
        ''' <param name="sortOrder">The direction of the sort</param>
        ''' <param name="user">The user that is currently loggged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A table object that contains the information needed for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function GetContainerTables(draw As Integer, top As Boolean, locNum As String, item As String, invMapID As Integer, startRow As Integer, endRow As Integer, sortColumn As Integer, sortOrder As String, user As String, WSID As String) As TableObject
            Dim table As New TableObject(draw, 0, 0, New List(Of List(Of String)))
            Dim reader As SqlDataReader = Nothing
            Dim cols As Object = New With {.top = New List(Of String) From {"Location", "[Carousel]", "[Item Number]", "[Serial Number]", "[Item Quantity]", "[Put Away Date]", "[Quantity Allocated Pick]", "[Quantity Allocated Put Away]"}, _
                                           .bottom = New List(Of String) From {"[Warehouse]", "[Location Number]", "LocType", "[Serial Number]", "[Item Quantity]", "[Put Away Date]", "[Quantity Allocated Pick]", "[Quantity Allocated Put Away]", "[Cell Size]", "[Golden Zone]"}}
            Dim innerlist As New List(Of String)
            Try
                If top Then
                    reader = RunSPArray("selIMDedicatedInvLocsTop", WSID, {{"@InvMapID", invMapID, intVar}, {"@StartRow", startRow, intVar}, _
                                                                           {"@EndRow", endRow, intVar}, {"@SortOrder", sortOrder, strVar}, _
                                                                           {"@SortColumn", cols.top(sortColumn), strVar}})
                Else
                    reader = RunSPArray("selIMDedicatedInvLocsBottom", WSID, {{"@LocNum", locNum, strVar}, {"@StartRow", startRow, intVar}, _
                                                                               {"@EndRow", endRow, intVar}, {"@SortOrder", sortOrder, strVar}, _
                                                                               {"@SortColumn", cols.bottom(sortColumn), strVar}, {"@Item", item, strVar}})
                End If

                Dim i As Integer = 0
                While reader.HasRows
                    While reader.Read
                        If i = 0 Then
                            table.recordsFiltered = reader(0)
                            table.recordsTotal = reader(0)
                        Else
                            For x As Integer = 0 To reader.FieldCount - 1
                                If IsDBNull(reader(x)) Then
                                    innerlist.Add("")
                                Else
                                    innerlist.Add(reader(x))
                                End If
                            Next
                            table.data.Add(innerlist)
                            innerlist = New List(Of String)
                        End If
                    End While
                    reader.NextResult()
                    i += 1
                End While
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "GetContainerTables", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return table
        End Function

        ''' <summary>
        ''' Marks a container as full by changing its velocity code to "FULL" + velocity
        ''' </summary>
        ''' <param name="location">The location to mark as full</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A boolean telling if the operation was executed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function MarkContainerFull(location As String, user As String, WSID As String) As Boolean
            Try
                RunActionSP("updIMFullGoldenZone", WSID, {{"@Location", location, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "MarkContainerFull", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Checks if a dedicated location is a bad container.  A location is a bad container if there are dedications or allocations that do not match the desired allocation for a put away with a reel transaction.
        ''' </summary>
        ''' <param name="InvMapID">The inventory map id to check</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currentlry being worked on</param>
        ''' <returns>A object that tells if a container is bad</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckBadContainer(InvMapID As Integer, user As String, WSID As String) As Object
            Dim container As Object = New With {.BadContainer = True, .ReelLocNum = ""}
            Dim reader As SqlDataReader = Nothing
            Try
                reader = RunSPArray("selIMBadContainer", WSID, {{"@WSID", WSID, strVar}, {"@User", user, strVar}, {"@InvMapID", InvMapID, intVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            container.BadContainer = (reader(0) = 0)
                        End If
                        If Not IsDBNull(reader(1)) Then
                            container.ReelLocNum = reader(1)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "CheckBadContainer", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return container
        End Function

        ''' <summary>
        ''' Marks a specified location and all locations with a matching Reel Location Number (vInventoryMap) as reserved for a particular item (reel tracking)
        ''' </summary>
        ''' <param name="InvMapID">The inventory map id to mark</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="item">The item number that is being reserved</param>
        ''' <param name="whse">The warehouse</param>
        ''' <returns>A boolean that tells if the operation was successful</returns>
        ''' <remarks></remarks>
        Public Shared Function DedicateLocation(InvMapID As Integer, user As String, WSID As String, item As String, whse As String) As Boolean
            Try
                RunActionSP("updIMDedicateLocation", WSID, {{"@InvMapID", InvMapID, intVar}, {"@Item", item, strVar}, {"@Whse", whse, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "DedicateLocation", ex.Message, user, WSID)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Checks if the provided serial numbers are not already allocated in open transactions or inventory map.
        ''' </summary>
        ''' <param name="SNs">A list of serial numbers</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of object that tells if each serial number is valid</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckValidSN(SNs As List(Of String), user As String, WSID As String) As List(Of Object)
            Dim reader As SqlDataReader = Nothing
            Dim reason As New List(Of Object)
            Try
                For Each sn In SNs
                    reader = RunSPArray("selIMValidSerNum", WSID, {{"@SN", sn, strVar}})
                    If reader.HasRows Then
                        If reader.Read Then
                            If Not IsDBNull(reader(0)) Then
                                reason.Add(New With {.valid = (reader(0) = "VALID"), .reason = reader(0)})
                            End If
                        End If
                    End If
                    reader.Dispose()
                    reader = Nothing
                Next
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "CheckValidSN", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return reason
        End Function

        ''' <summary>
        ''' Checks if a particular location is dedicated to the item specified.  In the case of a reel a location is required to be dedicated before a transaction can be completed.
        ''' </summary>
        ''' <param name="item">The item number</param>
        ''' <param name="invmapid">The location to check</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that si currently being worked on</param>
        ''' <returns>A boolean that tells if the location is dedicated to the item</returns>
        ''' <remarks></remarks>
        Public Shared Function IsDedicated(item As String, invmapid As Integer, user As String, WSID As String) As Boolean
            Dim reader As SqlDataReader = Nothing
            Dim dedicated As Boolean = False
            Try
                reader = RunSPArray("selIMIsDedicated", WSID, {{"@InvMapID", invmapid, intVar}, {"@Item", item, strVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            dedicated = (reader(0) = 0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "IsDedicated", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return dedicated
        End Function

        ''' <summary>
        ''' Indicates a task complete action.  This assigns a transaction to the specified tote in the specified batch.
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="user">The suer that si currently logged in</param>
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
        ''' <returns>An integer that contains the new open trasnaction id</returns>
        ''' <remarks></remarks>
        Public Shared Function TaskComplete(WSID As String, user As String, OTID As Integer, SplitQty As Integer, Qty As Integer, ToteID As String, BatchID As String, Item As String, _
                                            UF1 As String, UF2 As String, Lot As String, Ser As String, TotePos As Integer, Cell As String, Warehouse As String, ExpDate As String, Revision As String, _
                                            Zone As String, Carousel As String, Row As String, Shelf As String, Bin As String, InvMapID As Integer, LocMaxQty As Integer, Reel As Boolean, Dedicate As Boolean,
                                            OrderNumber As String) As Integer
            Dim NewOTID As Integer = 0
            Dim DataReader As SqlDataReader = Nothing
            Try

                DataReader = RunSPArray("ST_selInventoryMapByInvMapID", WSID, {{"@InvMapID", InvMapID, intVar}})

                If DataReader.HasRows AndAlso DataReader.Read() Then
                    'At this point item number on location should be set to the workstations reservation value or the item we are putting away
                    If IsDBNull(DataReader("Item Number")) OrElse (DataReader("Item Number") <> (WSID & "-IM") AndAlso DataReader("Item Number") <> Item) Then
                        'Invalid Location
                        Return -2
                    End If
                Else
                    'Error validating location
                    Return -1
                End If


                NewOTID = GetResultSingleCol("insUpdIMOTBatch", WSID, {{"@WSID", WSID, strVar}, {"@User", user, strVar}, {"@OTID", OTID, intVar}, {"@SplitQty", SplitQty, intVar}, {"@Qty", Qty, intVar},
                                                      {"@ToteID", ToteID, strVar}, {"@BatchID", BatchID, strVar}, {"@Item", Item, strVar}, {"@UF1", UF1, strVar}, {"@UF2", UF2, strVar},
                                                      {"@Lot", Lot, strVar}, {"@Ser", Ser, strVar}, {"@TotePos", TotePos, intVar}, {"@Cell", Cell, strVar}, {"@Whse", Warehouse, If(Warehouse = "", nullVar, strVar)},
                                                      {"@ExpDate", ExpDate, dteVar}, {"@Revision", Revision, strVar}, {"@Zone", Zone, strVar}, {"@Carousel", Carousel, strVar},
                                                      {"@Row", Row, strVar}, {"@Shelf", Shelf, strVar}, {"@Bin", Bin, strVar}, {"@InvMapID", InvMapID, intVar}, {"@LocMaxQty", LocMaxQty, intVar},
                                                      {"@Reel", CastAsSqlBool(Reel), intVar}, {"@Dedicate", CastAsSqlBool(Dedicate), intVar}, {"@OrderNumber", OrderNumber, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "TaskComplete", ex.Message, user, WSID)
                NewOTID = -1
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return NewOTID
        End Function

        ''' <summary>
        ''' Completes the current batch, updates the open transactions records where necessary and deletes the batch from the tote setup queue.
        ''' </summary>
        ''' <param name="BatchID">The bacth id ot complete</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The wrokstation that is currently being worked on</param>
        ''' <returns>An object that tells if the operation was successful and the next batch id</returns>
        ''' <remarks></remarks>
        Public Shared Function CompleteBatch(BatchID As String, user As String, WSID As String) As Object
            Dim reader As SqlDataReader = Nothing
            Dim batch As Object = New With {.Success = True, .NextBatch = ""}
            Try
                reader = RunSPArray("updIMBatchComplete", WSID, {{"@WSID", WSID, strVar}, {"@User", user, strVar}, {"@BatchID", BatchID, strVar}})
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            batch.NextBatch = reader(0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "CompleteBatch", ex.Message, user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return batch
        End Function

        ''' <summary>
        ''' Selects whether a batch exists in the tote setup queue for the workstation provided
        ''' </summary>
        ''' <param name="batchID">The batch id the check for</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A boolean telling if the batch exists</returns>
        ''' <remarks></remarks>
        Public Shared Function BatchExists(batchID As String, user As String, WSID As String) As Boolean
            Dim exists As Boolean = False
            Try
                exists = GetResultSingleCol("selToteSetupQueueBatchID", WSID, {{"@WSID", WSID, strVar}, {"@BatchID", batchID, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ProcessPutAways", "BatchExists", ex.Message, user, WSID)
            End Try
            Return exists
        End Function
    End Class
End Namespace