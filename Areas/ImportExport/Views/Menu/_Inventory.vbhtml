<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-body">
        <div class="alert alert-info white-bg" role="alert">
            Enter the default settings for importing new inventory records. These will only be used if the data is missing from the import record.
        </div>

        <div class="col-md-6 bottom-spacer">
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Primary Storage</label>
                        <div class="col-md-6">
                            <select class="form-control" name="PrimaryZoneType">
                                @GlobalHTMLHelpers.SelectOption(Model("Primary Zone Type"), {"Bulk", "Carousel", "Carton Flow"})
                            </select>
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Secondary Storage</label>
                        <div class="col-md-6">
                            <select class="form-control" name="SecondaryZoneType">
                                @GlobalHTMLHelpers.SelectOption(Model("Secondary Zone Type"), {"Bulk", "Carousel", "Carton Flow"})
                            </select>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 bottom-spacer">
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    @Code
                        Dim cellSizes = Model("Cell Sizes")
                        cellSizes.insert(0, "N/A")
                        
                        Dim goldenZoneTypes = Model("Golden Zone Types")
                        goldenZoneTypes.insert(0, "N/A")
                    End Code
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Bulk Cell Size</label>
                        <div class="col-md-6">
                            <input type="text" name="BulkCellSize" readonly="readonly" id="bulkcellsize" class="form-control cell-modal modal-launch-style" value="@Model("Bulk Cell Size")">
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Bulk Velocity Code</label>
                        <div class="col-md-6">
                            <input type="text" name="BulkVelocity" readonly="readonly" id="bulkvelocity" class="form-control velocity-modal modal-launch-style" value="@Model("Bulk Velocity")">
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Carousel Cell Size</label>
                        <div class="col-md-6">
                            <input type="text" name="CarouselCellSize" readonly="readonly" id="carcellsize" class="form-control cell-modal modal-launch-style" value="@Model("Carousel Cell Size")">
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Carousel Velocity Code</label>
                        <div class="col-md-6">
                            <input type="text" name="CarouselVelocity" readonly="readonly" id="carvelocity" class="form-control velocity-modal modal-launch-style" value="@Model("Carousel Velocity")">
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Carton Flow Cell Size</label>
                        <div class="col-md-6">
                            <input type="text" name="CFCellSize" readonly="readonly" id="cfcellsize" class="form-control cell-modal modal-launch-style" value="@Model("CF Cell Size")">
                        </div>
                    </div>
                    <div class="row bottom-spacer">
                        <label class="col-md-6">Carton Flow Velocity Code</label>
                        <div class="col-md-6">
                            <input type="text" name="CFVelocity" readonly="readonly" id="cfvelocity" class="form-control velocity-modal modal-launch-style" value="@Model("CF Velocity")">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

