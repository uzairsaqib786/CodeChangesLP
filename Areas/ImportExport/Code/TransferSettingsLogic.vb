' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module TransferSettingsLogic
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="transferSettings"></param>
        Public Sub UpdateTransferSettings(ByVal transferSettings As TransferSettingsModel)
            RunSP("UpdTransferSettings", "IE", {
                {"@Complete", transferSettings.Complete, boolVar},
                {"@ExportType", transferSettings.ExportType, strVar},
                {"@ExportAdjustments", transferSettings.ExportAdjustments, boolVar},
                {"@ExportDateRange", transferSettings.ExportDateRange, boolVar},
                {"@ExportFromDate", transferSettings.ExportFromDate, dteVar},
                {"@ExportHotPicks", transferSettings.ExportHotPicks, boolVar},
                {"@ExportHotPutAways", transferSettings.ExportHotPutAways, boolVar},
                {"@ExportJobType", transferSettings.ExportJobType, strVar},
                {"@ExportToDate", transferSettings.ExportToDate, dteVar},
                {"@HoldPickUntilOrderComplete", transferSettings.HoldPickUntilOrderComplete, boolVar},
                {"@HoldPickUntilShipComplete", transferSettings.HoldPickUntilShipComplete, boolVar},
                {"@HoldPickUntilToteComplete", transferSettings.HoldPickUntilToteComplete, boolVar},
                {"@ImportJobType", transferSettings.ImportJobType, strVar},
                {"@RecombineSplit", transferSettings.RecombineSplit, boolVar},
                {"@Shipping", transferSettings.Shipping, boolVar},
                {"@ShippingComplete", transferSettings.ShippingComplete, boolVar},
                {"@ShippingTransactions", transferSettings.ShippingTransactions, boolVar},
                {"@VerifyLocations", transferSettings.VerifyLocations, boolVar},
                {"@WaitForSplit", transferSettings.WaitForSplit, boolVar}
            })
        End Sub
    End Module
End Namespace

