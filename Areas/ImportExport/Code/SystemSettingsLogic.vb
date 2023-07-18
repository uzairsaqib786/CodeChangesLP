' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Public Module SystemSettingsLogic
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="wsid"></param>
        ''' <returns></returns>
        Public Function GetLastImportExport(ByVal wsid As String) As Dictionary(Of String, Object)
            Dim ret = GetResultMap("SelEventLogLastImportExport", wsid)

            Dim lastImport As Date = ret("LastImport")
            Dim lastExport As Date = ret("LastExport")

            ret("LastImport") = lastImport.ToString("MM/dd/yyyy HH:mm")
            ret("LastExport") = lastExport.ToString("MM/dd/yyyy HH:mm")

            Return ret
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="systemSettings"></param>
        Public Sub UpdateSystemSettings(ByVal systemSettings As SystemSettingsModel)
            RunSP("UpdSystemSettings", "IE", {
                  {"@AutoExport", systemSettings.AutoExport, boolVar},
                  {"@AutoImport", systemSettings.AutoImport, boolVar},
                  {"@AutoImportInv", systemSettings.AutoImportInv, boolVar},
                  {"@CheckDupes", systemSettings.CheckDupes, boolVar},
                  {"@DupeDays", systemSettings.DupeDays, intVar},
                  {"@ExportFileType", systemSettings.ExportFileType, strVar},
                  {"@ExportFiles", systemSettings.ExportFiles, boolVar},
                  {"@ExportInterval", systemSettings.ExportInterval, intVar},
                  {"@ImportFileType", systemSettings.ImportFileType, strVar},
                  {"@ImportInterval", systemSettings.ImportInterval, intVar},
                  {"@ImportTransactionsTo", systemSettings.ImportTransactionsTo, strVar},
                  {"@NoCarriageReturn", systemSettings.NoCarriageReturn, boolVar},
                  {"@TrimItemNumber", systemSettings.TrimItemNumber, boolVar},
                  {"@TrimUserFields", systemSettings.TrimUserFields, boolVar}})
        End Sub

    End Module
End Namespace
