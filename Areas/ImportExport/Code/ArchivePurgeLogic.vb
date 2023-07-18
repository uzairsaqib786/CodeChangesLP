' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module ArchivePurgeLogic

        ''' <summary>
        ''' Gets all the purge tables
        ''' </summary>
        ''' <returns>A dictionary that contains the purage tables and their information</returns>
        Public Function GetPurgeTables() As List(Of Dictionary(Of String, Object))
            Dim ret = GetResultMapList("SelPurgeTables", "IE")
            Return ret
        End Function

        ''' <summary>
        ''' Updates the purage tables' data
        ''' </summary>
        ''' <param name="archivePurgeList">A list that contains the new values for every purge table</param>
        Public Sub UpdateArchivePurge(ByVal archivePurgeList As List(Of ArchivePurgeModel))
            For Each model As ArchivePurgeModel In archivePurgeList
                RunSP("UpdPurgeTables", "IE", {
                        {"@Archive", model.Archive, boolVar},
                        {"@PurgeDays", model.PurgeDays, intVar},
                        {"@ID", model.ID, intVar}
                    })
            Next
        End Sub

    End Module
End Namespace

