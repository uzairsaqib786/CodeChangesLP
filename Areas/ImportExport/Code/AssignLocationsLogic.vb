' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022


Namespace ImportExport
    Module AssignLocationsLogic

        ''' <summary>
        ''' Gets the number of unallocated trasnactions for each transaction type
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently logged on</param>
        ''' <returns>A dictonary that contains each transaction type and the number of unallocated records for each one</returns>
        Public Function GetUnallocatedTransactionsCount(ByVal wsid As String) As Dictionary(Of String, Integer)

            Dim res = GetResultMapList("SelOTUnallocatedTransactionsCount", wsid)

            Dim picks = 0, putAways = 0, counts = 0

            For Each row In res
                Dim tType = row("Transaction Type")
                Dim count = row("Count")
                If tType = "Pick" Then picks = count
                If tType = "Put Away" Then putAways = count
                If tType = "Count" Then counts = count
            Next

            Dim ret = New Dictionary(Of String, Integer)
            ret.Add("Picks", picks)
            ret.Add("Put Aways", putAways)
            ret.Add("Counts", counts)

            Return ret

        End Function

        ''' <summary>
        ''' Updates the assign locations preferences to their desired values 
        ''' </summary>
        ''' <param name="assignLocations">An oobject that contains the new values for each preference</param>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        Public Sub UpdateAssignLocations(ByVal assignLocations As AssignLocationsModel, ByVal wsid As String)
            RunSP("UpdxferPrefWSPrefAssignLocations", "IE", {
                {"@LocAssignPickSort", assignLocations.LocAssignPickSort, strVar},
                {"@DynamicWHLoc", assignLocations.DynamicWHLoc, boolVar},
                {"@PrintReprocessReport", assignLocations.PrintReprocessReport, boolVar},
                {"@AutoCompleteBackorders", assignLocations.AutoCompleteBackorders, boolVar}
            })

            RunSP("UpdSysPrefAssignLocations", wsid, {
                {"@AutoLocPicks", assignLocations.AutoLocPicks, boolVar},
                {"@AutoLocPutAways", assignLocations.AutoLocPutAways, boolVar},
                {"@AutoLocCounts", assignLocations.AutoLocCounts, boolVar}
            })
        End Sub

    End Module
End Namespace
