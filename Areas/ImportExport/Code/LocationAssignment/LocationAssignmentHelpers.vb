' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module LocationAssignmentHelpers

        ''' <summary>
        ''' Tells if the given object is null/nothing
        ''' </summary>
        ''' <param name="obj">The object to check for</param>
        ''' <returns>A boolean telling the object was nothing or null</returns>
        Friend Function IsNothingOrDBNull(ByVal obj As Object) As Boolean
            Return obj Is Nothing OrElse IsDBNull(obj)
        End Function

        ''' <summary>
        ''' Auto completes the desired open transaction record
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The id of the record to auto complete</param>
        Friend Sub AutoCompleteOT(ByVal wsid As String, ByVal id As Integer)
            RunSP("UpdOTAutoComplete", wsid, {{"@ID", id, intVar}})
        End Sub

        ''' <summary>
        ''' Moves the desired open trasnaction record to reprocess 
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The id of the record to move</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="nameStamp">The name of the user that performed this action</param>
        ''' <param name="reasonMessage">The reason why this record was moved</param>
        ''' <param name="masterRecordID">The master record id of the transaction</param>
        Friend Sub ToReprocessQueue(ByVal wsid As String,
                                    ByVal id As Integer,
                                    ByVal dateStamp As Date,
                                    ByVal nameStamp As String,
                                    ByVal reasonMessage As String,
                                    Optional ByVal masterRecordID As Integer = -1)

            RunSP("InsOTTempLocAssignReprocess", wsid, {
                        {"@ID", id, intVar},
                        {"@DateStamp", dateStamp, dteVar},
                        {"@NameStamp", nameStamp, strVar},
                        {"@MasterRecordID", masterRecordID, If(masterRecordID = -1, nullVar, intVar)},
                        {"@ReasonMessage", reasonMessage, strVar}})
        End Sub

        ''' <summary>
        ''' Moves the desired open trasnaction record to reprocess 
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The id of the record to move</param>
        ''' <param name="dateStamp">The desired date stamp</param>
        ''' <param name="nameStamp">The name of the user that performed this action</param>
        ''' <param name="reasonMessage">The reason why this record was moved</param>
        Friend Sub ToReprocessQueuePicks(ByVal wsid As String,
                                    ByVal id As Integer,
                                    ByVal dateStamp As Date,
                                    ByVal nameStamp As String,
                                    ByVal reasonMessage As String)

            RunSP("InsOTTempLocAssignReprocess", wsid, {
                        {"@ID", id, intVar},
                        {"@DateStamp", dateStamp, dteVar},
                        {"@NameStamp", nameStamp, strVar},
                        {"@ReasonMessage", reasonMessage, strVar}})
        End Sub

        ''' <summary>
        ''' Performs the quantity allocated reset function
        ''' </summary>
        ''' <param name="wsid">The wokrstation that is currently logged in</param>
        ''' <param name="name">The name of the user performing this action</param>
        Friend Sub QtyAllocReset(ByVal wsid As String, ByVal name As String)
            RunSP("QtyAllocReset", wsid, {{"@LoginName", name, strVar}, {"@DynWhse", "Yes", strVar}})
        End Sub

        ''' <summary>
        ''' Deletes the desired record from open transactions
        ''' </summary>
        ''' <param name="wsid">The wokrstation that is currently being worked on</param>
        ''' <param name="otid">The id to delete</param>
        ''' <param name="pending">If the record is found in the open transactions pending table</param>
        Friend Sub DelOTID(ByVal wsid As String, ByVal otid As String, ByVal pending As Boolean)
            RunSP("DelOT", wsid, {{"@ID", otid, intVar}, {"@Pending", pending, boolVar}})
        End Sub

        ''' <summary>
        ''' Gets the location string for the given inv map record
        ''' </summary>
        ''' <param name="invMapRow">Dictionary that contains the data for the desired record</param>
        ''' <returns>A string that tells what the location is</returns>
        Friend Function LocationFromInvMap(ByVal invMapRow As Dictionary(Of String, Object)) As String
            Return invMapRow("Zone") & invMapRow("Carousel") & invMapRow("Row") & invMapRow("Shelf") & invMapRow("Bin")
        End Function

        ''' <summary>
        ''' Checks if an item number is valid
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="itemNum">The item number that is validated</param>
        ''' <returns>A boolean telling if the item number is valid</returns>
        Friend Function SelInventoryValidItemNumber(ByVal wsid As String, ByVal itemNum As String) As Boolean
            Dim ret As Boolean = GetResultSingleCol("SelInventoryValidItemNumber", wsid, {{"@ItemNumber", itemNum, strVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Gets the category for the desired item number
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="itemNum">The item number to get the category for</param>
        ''' <returns>A string that tells the item number's category</returns>
        Friend Function SelInventoryCategory(ByVal wsid As String, ByVal itemNum As String) As String
            Dim ret As String = GetResultSingleCol("SelInventoryCategory", wsid, {{"@ItemNumber", itemNum, strVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Selects data for the desired record from open transactions
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="id">The id of the record to get the data from</param>
        ''' <returns>A dictionary that contains the data for the desired record</returns>
        Friend Function SelOTMasterRecordNullLoc(ByVal wsid As String, ByVal id As Integer) As Dictionary(Of String, Object)
            Dim ret = GetResultMap("SelOTMasterRecordNullLoc", wsid, {{"@ID", id, intVar}})
            Return ret
        End Function

        ''' <summary>
        ''' Gets the transaction quantity for the desired record
        ''' </summary>
        ''' <param name="wsid">The workstationt hat is currently being worked on</param>
        ''' <param name="id">The id of the record from OT to get the quantity for</param>
        ''' <returns>An integer that tells the transaction quantity for the desired record</returns>
        Friend Function SelOTTransQtyMasterRecordNullLoc(ByVal wsid As String, ByVal id As Integer) As Integer
            Dim ret As Integer = SelOTMasterRecordNullLoc(wsid, id)("Transaction Quantity")
            Return ret
        End Function

        ''' <summary>
        ''' Selects the system preferences
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <returns>A dictionary that cotnaisn the preferences and their values</returns>
        Friend Function SelSystemPref(ByVal wsid As String) As Dictionary(Of String, Object)
            Dim ret As Dictionary(Of String, Object) = GetResultMap("SelSystemPref", wsid)
            Return ret
        End Function

        ''' <summary>
        ''' Updates the open trasnaction record and marks the location
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="otid">The open trasnactions record's id</param>
        ''' <param name="invMapID">The desired location id</param>
        Friend Sub UpdOTLocationFull(ByVal wsid As String, ByVal otid As Integer, ByVal invMapID As Integer)
            RunSP("UpdOTLocationFull", wsid, {{"@ID", otid, intVar}, {"@InvMapID", invMapID, intVar}})
        End Sub

        ''' <summary>
        ''' Updates the master record id
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <param name="masterRecord">Boolean telling if this is the master record</param>
        Friend Sub UpdOTMasterRecordID(ByVal wsid As String, ByVal masterRecord As Boolean)
            RunSP("updOTMastRecID", wsid, {{"@MasterRecord", masterRecord, boolVar}})
        End Sub

    End Module
End Namespace

