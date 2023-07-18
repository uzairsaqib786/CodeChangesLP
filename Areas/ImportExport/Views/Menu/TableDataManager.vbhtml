<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(Of String, Object)
<div class="modal fade" id="TableDataManagerModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="TableDataManagerModal" aria-hidden="true">
    <div class="modal-dialog" style="width:95%;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="modal-title" id="TableDataManagerLabel">
                            Manage Inventory Data
                        </h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row" id="InventoryMapTableSection">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <select id="InvMapDataSelect" class="form-control">
                                    <option value="current">Current Map</option>
                                    <option value="import">Import Map</option>
                                    <option value="export">Export Map</option>
                                    <option value="archive">Archive Map</option>
                                    <option value="audit">Audit Map</option>
                                </select>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-danger btn-block" id="InvMapDelete" data-toggle="tooltip" data-placement="top" data-original-title="Delete Displayed Records"><span class="glyphicon glyphicon-remove"></span></button>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-primary btn-block" id="GetInvMapLocs">Get Inventory Map Locations</button>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-primary btn-block" id="ImportInvMapLocs">Import Inventory Map Locations</button>
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-12">
                                <table id="InvMapDataManTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                    <thead>
                                        <tr>
                                            <th>Inv Map ID</th>
                                            <th>Transaction Type</th>
                                            <th>Location ID</th>
                                            <th>Warehouse</th>
                                            <th>Location Number</th>
                                            <th>Cell Size</th>
                                            <th>Golden Zone</th>
                                            <th>Zone</th>
                                            <th>Carousel</th>
                                            <th>Row</th>
                                            <th>Shelf</th>
                                            <th>Bin</th>
                                            <th>Item Number</th>
                                            <th>Description</th>
                                            <th>Item Quantity</th>
                                            <th>Unit of Measure</th>
                                            <th>Put Away Date</th>
                                            <th>Maximum Quantity</th>
                                            <th>Revision</th>
                                            <th>Serial Number</th>
                                            <th>Lot Number</th>
                                            <th>Expiration Date</th>
                                            <th>User Field1</th>
                                            <th>User FIeld2</th>
                                            <th>Date Sensitive</th>
                                            <th>Dedicated</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                        <select id="InvMapDataManCols" hidden="hidden">
                            <option value="Inv Map ID">Inv Map ID</option>
                            <option value="Transaction Type">Transaction Type</option>
                            <option value="Location ID">Location ID</option>
                            <option value="Warehouse">Warehouse</option>
                            <option value="Location Number">Location Number</option>
                            <option value="Cell Size">Cell Size</option>
                            <option value="Golden Zone">Golden Zone</option>
                            <option value="Zone">Zone</option>
                            <option value="Carousel">Carousel</option>
                            <option value="Row">Row</option>
                            <option value="Shelf">Shelf</option>
                            <option value="Bin">Bin</option>
                            <option value="Item Number">Item Number</option>
                            <option value="Description">Description</option>
                            <option value="Item Quantity">Item Quantity</option>
                            <option value="Unit of Measure">Unit of Measure</option>
                            <option value="Put Away Date">Put Away Date</option>
                            <option value="Maximum Quantity">Maximum Quantity</option>
                            <option value="Revision">Revision</option>
                            <option value="Serial Number">Serial Number</option>
                            <option value="Lot Number">Lot Number</option>
                            <option value="Expiration Date">Expiration Date</option>
                            <option value="User Field1">User Field1</option>
                            <option value="User Field2">User Field2</option>
                            <option value="Date Sensitive">Date Sensitive</option>
                            <option value="Dedicated">Dedicated</option>
                        </select>
                    </div>
                </div>
                <div class="row" id="InventoryTableSection">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <select id="InvDataSelect" class="form-control">
                                    <option value="current">Current Inventory</option>
                                    <option value="kit">Kit Inventory</option>
                                    <option value="scan codes">Current Scan Codes</option>
                                    <option value="import">Import Inventory</option>
                                    <option value="export">Export Inventory</option>
                                    <option value="archive">Archive Inventory</option>
                                </select>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-danger btn-block" id="InvDelete" data-toggle="tooltip" data-placement="top" data-original-title="Delete Displayed Records"><span class="glyphicon glyphicon-remove"></span></button>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-primary btn-block" id="GetInvRecs">Get Inventory Records</button>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-primary btn-block" id="ProcInvRecs">Process Inventory Records</button>
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-primary btn-block" id="ExpScanCodes">Export Scan Codes</button>
                            </div>
                        </div>
                        <div class="row top-spacer">
                            <div class="col-md-12">
                                <table id="InvDataManTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                    <thead>
                                        <tr>
                                            <th class="text-center">Transaction Type</th>
                                            <th class="text-center">Item Number</th>
                                            <th class="text-center">Supplier Item ID</th>
                                            <th class="text-center">Description</th>
                                            <th class="text-center">Category</th>
                                            <th class="text-center">Sub Category</th>
                                            <th class="text-center">Primary Zone</th>
                                            <th class="text-center">Supplier Number</th>
                                            <th class="text-center">Manufacturer</th>
                                            <th class="text-center">Model</th>
                                            <th class="text-center">Special Features</th>
                                            <th class="text-center">Reorder Point</th>
                                            <th class="text-center">Reorder Quantity</th>
                                            <th class="text-center">Unit of Measure</th>
                                            <th class="text-center">Secondary Zone</th>
                                            <th class="text-center">Carousel Cell Size</th>
                                            <th class="text-center">Carousel Velocity</th>
                                            <th class="text-center">FIFO</th>
                                            <th class="text-center">Active</th>
                                            <th class="text-center">Date Sensitive</th>
                                            <th class="text-center">Warehouse Sensitive</th>
                                            <th class="text-center">Carousel Min Qty</th>
                                            <th class="text-center">Carousel Max Qty</th>
                                            <th class="text-center">Pick Fence Qty</th> 
                                            <th class="text-center">Split Case</th>
                                            <th class="text-center">Case Quantity</th>
                                            <th class="text-center">Unit Cost</th>
                                            <th class="text-center">Bulk Cell Size</th>
                                            <th class="text-center">Bulk Velocity</th>
                                            <th class="text-center">Bulk Min Qty</th>
                                            <th class="text-center">Bulk Max Qty</th>
                                            <th class="text-center">CF Cell Size</th>
                                            <th class="text-center">CF Velocity</th>
                                            <th class="text-center">CF Min Qty</th>
                                            <th class="text-center">CF Max Qty</th>
                                            <th class="text-center">Replenishment Point</th>
                                            <th class="text-center">Replenishment Level</th>
                                            <th class="text-center">Avg Piece Weight</th>
                                            <th class="text-center">Sample Quantity</th>
                                            <th class="text-center">Use Scale</th>
                                            <th class="text-center">Min Use Scale Quantity</th>
                                            <th class="text-center">Kit Item Number</th>
                                            <th class="text-center">Kit Quantity</th>
                                            <th class="text-center">Scan Code</th>
                                            <th class="text-center">Scan Type</th>
                                            <th class="text-center">Scan Range</th>
                                            <th class="text-center">Start Position</th>
                                            <th class="text-center">Code Length</th>
                                            <th class="text-center">Minimum RTS Reel Quantity</th>
                                            <th class="text-center">Pick Sequence</th>
                                            <th class="text-center">Include In Auto RTS Update</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                        <select id="InvDataManCols" hidden="hidden">
                            <option value="Transaction Type">Transaction Type</option>
                            <option value="Item Number">Item Number</option>
                            <option value="Supplier Item ID">Supplier Item ID</option>
                            <option value="Description">Description</option>
                            <option value="Category">Category</option>
                            <option value="Sub Category">Sub Category</option>
                            <option value="Primary Zone">Primary Zone</option>
                            <option value="Supplier Number">Supplier Number</option>
                            <option value="Manufacturer">Manufacturer</option>
                            <option value="Model">Model</option>
                            <option value="Special Features">Special Features</option>
                            <option value="Reorder Point">Reorder Point</option>
                            <option value="Reorder Quantity">Reorder Quantity</option>
                            <option value="Unit of Measure">Unit of Measure</option>
                            <option value="Secondary Zone">Secondary Zone</option>
                            <option value="Carousel Cell Size">Carousel Cell Size</option>
                            <option value="Carousel Velocity">Carousel Velocity</option>
                            <option value="FIFO">FIFO</option>
                            <option value="Active">Active</option>
                            <option value="Date Sensitive">Date Sensitive</option>
                            <option value="Warehouse Sensitive">Warehouse Sensitive</option>
                            <option value="Carousel Min Qty">Carousel Min Qty</option>
                            <option value="Carousel Max Qty">Carousel Max Qty</option>
                            <option value="Pick Fence Qty">Pick Fence Qty</option>
                            <option value="Split Case">Split Case</option>
                            <option value="Case Quantity">Case Quantity</option>
                            <option value="Unit Cost">Unit Cost</option>
                            <option value="Bulk Cell Size">Bulk Cell Size</option>
                            <option value="Bulk Velocity">Bulk Velocity</option>
                            <option value="Bulk Min Qty">Bulk Min Qty</option>
                            <option value="Bulk Max Qty">Bulk Max Qty</option>
                            <option value="CF Cell Size">CF Cell Size</option>
                            <option value="CF Velocity">CF Velocity</option>
                            <option value="CF Min Qty">CF Min Qty</option>
                            <option value="CF Max Qty">CF Max Qty</option>
                            <option value="Replenishment Point">Replenishment Point</option>
                            <option value="Replenishment Level">Replenishment Level</option>
                            <option value="Avg Piece Weight">Avg Piece Weight</option>
                            <option value="Sample Quantity">Sample Quantity</option>
                            <option value="Use Scale">Use Scale</option>
                            <option value="Min Use Scale">Min Use Scale Quantity</option>
                            <option value="Kit Item Number">Kit Item Number</option>
                            <option value="Kit Quantity">Kit Quantity</option>
                            <option value="Scan Code">Scan Code</option>
                            <option value="Scan Type">Scan Type</option>
                            <option value="Scan Range">Scan Range</option>
                            <option value="Start Position">Start Position</option>
                            <option value="Code Length">Code Length</option>
                            <option value="Minimum RTS Reel Quantity">Minimum RTS Reel Quantity</option>
                            <option value="Pick Sequence">Pick Sequence</option>
                            <option value="Include In Auto RTS Update">Include In Auto RTS Update</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="TableDataManager_Dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/ImportExport/Scripts/TableDataManager.js"></script>
