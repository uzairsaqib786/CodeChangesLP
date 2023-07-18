' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module ManageDataLogic
        ''' <summary>
        ''' Updates the desired data within the xfer preferences table
        ''' </summary>
        ''' <param name="manageData">A model that contaisn the new values for the preferences</param>
        Public Sub UpdateManageData(ByVal manageData As ManageDataModel)
            RunSP("UpdManageData", "IE", {
                {"@EmpExportFile", manageData.EmpExportFile, strVar},
                {"@EmpImportFile", manageData.EmpImportFile, strVar},
                {"@EventExportFile", manageData.EventExportFile, strVar},
                {"@ExportEmpType", manageData.ExportEmpType, strVar},
                {"@ExportInvMapType", manageData.ExportInvMapType, strVar},
                {"@ExportInvType", manageData.ExportInvType, strVar},
                {"@ImportEmpType", manageData.ImportEmpType, strVar},
                {"@ImportInvMapType", manageData.ImportInvMapType, strVar},
                {"@ImportInvType", manageData.ImportInvType, strVar},
                {"@InvExportFile", manageData.InvExportFile, strVar},
                {"@InvImportFile", manageData.InvImportFile, strVar},
                {"@InvMapExportFile", manageData.InvMapExportFile, strVar},
                {"@InvMapImportFile", manageData.InvMapImportFile, strVar}
            })
        End Sub
    End Module
End Namespace