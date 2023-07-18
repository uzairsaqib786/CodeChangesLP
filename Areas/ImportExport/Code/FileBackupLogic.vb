' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module FileBackupLogic

        ''' <summary>
        ''' Updates the file backup settings
        ''' </summary>
        ''' <param name="fileBackup">Object that contains the new file backup information</param>
        Public Sub UpdateFileBackup(ByVal fileBackup As FileBackupModel)
            RunSP("UpdFileBackup", "IE", {
                {"@ImportFileBackupLoc", fileBackup.ImportFileBackupLoc, strVar},
                {"@ExportFileBackupLoc", fileBackup.ExportFileBackupLoc, strVar},
                {"@BackupByDay", fileBackup.BackupByDay, boolVar}
            })
        End Sub
    End Module
End Namespace

