' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module InventoryLogic
        ''' <summary>
        ''' Updates the inventory settings 
        ''' </summary>
        ''' <param name="inventory">Model that contains the new values for the preferences</param>
        Public Sub UpdateInventory(ByVal inventory As InventoryModel)
            RunSP("UpdInventorySettings", "IE", {
                {"@PrimaryZoneType", inventory.PrimaryZoneType, strVar},
                {"@SecondaryZoneType", inventory.SecondaryZoneType, strVar},
                {"@BulkCellSize", inventory.BulkCellSize, strVar},
                {"@BulkVelocity", inventory.BulkVelocity, strVar},
                {"@CarouselCellSize", inventory.CarouselCellSize, strVar},
                {"@CarouselVelocity", inventory.CarouselVelocity, strVar},
                {"@CFCellSize", inventory.CFCellSize, strVar},
                {"@CFVelocity", inventory.CFVelocity, strVar}
            })
        End Sub
    End Module
End Namespace

