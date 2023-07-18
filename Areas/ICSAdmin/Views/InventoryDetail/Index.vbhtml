<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Inventory Detail"
    ViewData("PageName") = "&nbsp; | &nbsp; Inventory Detail"
End Code
<div class="container-fluid">
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h3 class="panel-title">Inventory Detail</h3>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <label>@Model.Alias.ItemNumber Lookup</label><input maxlength="50" placeholder="@Model.Alias.ItemNumber" type="text" id="ItemNumTA" class="form-control input-sm" />
                                </div>
                                <div class="col-lg-2">
                                    <label style="visibility:hidden">Clear @Model.Alias.ItemNumber Lookup</label><button type="button" class="btn btn-primary btn-block btn-sm" id="clearButtDetail">Clear</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label>@Model.Alias.ItemNumber</label> <input disabled="disabled"  type="text" id="ItemNumDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label>Description</label><input disabled="disabled"  type="text" id="DescriptionDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>@Model.Alias.UoM</label><input disabled="disabled"  type="text" id="UnitOfMeasureDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Category</label><input disabled="disabled" type="text" id="CategoryDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Sub Category</label><input disabled="disabled"  type="text" id="SubCategoryDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Supplier Item ID</label><input disabled="disabled" type="text" id="SupplierItemIDDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:60px;">
                                        <div class="col-lg-6">
                                            <label>FIFO</label><input disabled="disabled"  type="text" id="FIFODetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Pick Fence Qty</label><input disabled="disabled"  type="text" id="PickFenceQtyDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Date Sensitive</label><input disabled="disabled"  type="text" id="DateSensitiveDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Warehouse Sensitive</label><input disabled="disabled"  type="text" id="WarehouseSensitiveDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Split Case</label><input disabled="disabled"  type="text" id="SplitCaseDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Case Quantity</label><input disabled="disabled" type="text" id="CaseQuantityDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Primary Pick Zone</label><input disabled="disabled" type="text" id="PrimaryPickZoneDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Secondary Pick Zone</label><input disabled="disabled"  type="text" id="SecondaryPickZoneDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Reorder Point</label><input disabled="disabled"  type="text" id="ReorderPointDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Reorder Quantity</label><input disabled="disabled"  type="text" id="ReorderQuantityDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Replenishment Point</label><input disabled="disabled"  type="text" id="ReplenishmentPointDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Replenishment Level</label><input disabled="disabled"  type="text" id="ReplenishmentLevelDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:165px;">
                                        <div class="col-lg-6">
                                            <label>Active</label><input disabled="disabled" type="text" id="ActiveDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Pick Sequence</label><input disabled="disabled"  type="text" id="PickSequenceDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Unit Cost</label><input disabled="disabled" type="text" id="UnitCostDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Supplier Number</label><input disabled="disabled"  type="text" id="SupplierNumberDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Manufacturer</label><input disabled="disabled"  type="text" id="ManufacturerDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Model</label><input disabled="disabled"  type="text" id="ModelDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label>Special Features</label><input disabled="disabled" type="text" id="SpecialFeaturesDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <label>Carousel Cell Size</label><input disabled="disabled"  type="text" id="CarouselCellSizeDetail" class="form-control input-sm" />
                                            <label>Carousel Velocity</label><input disabled="disabled"  type="text" id="CarouselVelocityDetail" class="form-control input-sm" />
                                            <label>Carousel Min Qty</label><input disabled="disabled" type="text" id="CarouselMinQtyDetail" class="form-control input-sm" />
                                            <label>Carousel Max Qty</label><input disabled="disabled"  type="text" id="CarouselMaxQtyDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-4">
                                            <label>Bulk Cell Size</label><input disabled="disabled"  type="text" id="BulkCellSizeDetail" class="form-control input-sm" />
                                            <label>Bulk Velocity</label><input disabled="disabled"  type="text" id="BulkVelocityDetail" class="form-control input-sm" />
                                            <label>Bulk Min Qty</label><input disabled="disabled" type="text" id="BulkMinQtyDetail" class="form-control input-sm" />
                                            <label>Bulk Max Qty</label><input disabled="disabled"  type="text" id="BulkMaxQtyDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-4">
                                            <label>CF Cell Size</label><input disabled="disabled"  type="text" id="CFCellSizeDetail" class="form-control input-sm" />
                                            <label>CF Velocity</label><input disabled="disabled" type="text" id="CFVelocityDetail" class="form-control input-sm" />
                                            <label>CF Min Qty</label><input disabled="disabled" type="text" id="CFMinQtyDetail" class="form-control input-sm" />
                                            <label>CF Max Qty</label><input disabled="disabled"  type="text" id="CFMaxQtyDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:60px;">
                                        <div class="col-lg-6">
                                            <label>Include In Auto RTS Update</label><input disabled="disabled"  type="text" id="IncludeInAutoRTSUpdateDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Avg Piece Weight</label><input disabled="disabled" type="text" id="AvgPieceWeightDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Sample Quantity</label><input disabled="disabled"  type="text" id="SampleQuantityDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Use Scale</label><input disabled="disabled"  type="text" id="UseScaleDetail" class="form-control input-sm" />
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Min Use Scale Quantity</label><input disabled="disabled" type="text" id="MinUseScaleQuantityDetail" class="form-control input-sm" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Minimum RTS Reel Quantity</label><input disabled="disabled" type="text" id="MinimumRTSReelQuantityDetail" class="form-control input-sm" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    $(document).ready(function () {
        $('#ItemNumTA').focus();
    });
</script>
<script src="~/Areas/ICSAdmin/Scripts/InventoryDetail/InventoryDetail.js"></script>