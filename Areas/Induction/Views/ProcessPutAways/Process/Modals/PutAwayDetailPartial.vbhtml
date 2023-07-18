<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12" id="PutAwayDetail" style="display:none;">
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading" style="background-color:black;">
                        <h3 class="panel-title">
                            <label style="color:ghostwhite">Trans. Info</label>
                            <div class="pull-right">
                                <button class="btn btn-danger btn-sm" id="ClearTrans" style="margin-top:-6px;">Clear Trans. Info</button>
                            </div>
                        </h3>
                    </div>
                    <div class="panel-body" style="background-color:lightsteelblue">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Order Number</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="OrderNumber" />
                            </div>
                            <div class="col-md-3">
                                <label>Category</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="Category" />
                            </div>
                            <div class="col-md-3">
                                <label>Sub Category</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="SubCategory" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>@Model.aliases.UserFields(0)</label>
                                <input type="text" class="form-control input-sm" id="UF1" placeholder="@Model.aliases.UserFields(0)" />
                            </div>
                            <div class="col-md-6">
                                <label>@Model.aliases.UserFields(1)</label>
                                <input type="text" class="form-control input-sm" id="UF2" placeholder="@Model.aliases.UserFields(1)" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Lot Number</label>
                                <input type="text" class="form-control input-sm" id="LotNum" placeholder="Lot Number" />
                            </div>
                            <div class="col-md-4">
                                <label>Expiration Date</label>
                                <input type="text" class="form-control input-sm date-picker" id="ExpDate" />
                            </div>
                            <div class="col-md-4">
                                <label>Serial Number</label>
                                <input type="text" class="form-control input-sm" id="SerialNumber" placeholder="Serial Number" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Current Trans. Qty</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="TransQty" />
                            </div>
                            <div class="col-md-4">
                                <div class="form-group has-feedback">
                                    <label>Warehouse</label>
                                    <input type="text" class="form-control input-sm modal-launch-style" id="Warehouse" readonly="readonly" data-no-edits="true" />
                                    <i class="glyphicon glyphicon-resize-full form-control-feedback warehouse modal-launch-style"></i>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Return to Stock?</label>
                                <input style="margin-top:0px;" type="checkbox" class="form-control input-sm" id="RTS" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading" style="background-color:black">
                        <h3 class="panel-title">
                            <label style="color:ghostwhite">Item Info</label>
                            <div class="pull-right">
                                <button type="button" class="btn btn-primary btn-sm" id="ViewInvMaster" style="margin-top:-6px;">View Item Detail</button>&nbsp;
                                <button type="button" class="btn btn-primary btn-sm" id="UpdateInvMaster" style="margin-top:-6px;">Update Item Info</button>
                            </div>
                        </h3>
                    </div>
                    <div class="panel-body" style="background-color:lightsteelblue">
                        <div class="row">
                            <div class="col-md-4">
                                <label>@Model.aliases.ItemNumber:</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="ItemNumber" />
                            </div>
                            <div class="col-md-4">
                                <label>Supplier Item ID:</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="SupplierItemID" />
                            </div>
                            <div class="col-md-4">
                                <label>Description: </label>
                                <input type="text" class="form-control input-sm" disabled="disabled" id="Description" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>Whse. Sens.</label>
                                <input style="margin-top:0px;" type="checkbox" class="form-control input-sm" id="WhseSens" disabled="disabled" readonly="readonly" />
                            </div>
                            <div class="col-md-3">
                                <label>Date Sens.</label>
                                <input style="margin-top:0px;" type="checkbox" class="form-control input-sm" id="DateSens" disabled="disabled" readonly="readonly" />
                            </div>
                            <div class="col-md-3">
                                <label>FIFO</label>
                                <input style="margin-top:0px;" type="checkbox" class="form-control input-sm" id="FIFO" disabled="disabled" readonly="readonly" />
                                <input type="hidden" id="FIFODate" />
                            </div>
                            <div class="col-md-3">
                                <label>U of M:</label>
                                <input type="text" class="form-control input-sm" id="UoM" readonly="readonly" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label>Carousel Cell Size</label>
                                    <select class="form-control" id="CCell" style="max-width:90%"></select>
                                    @*<input type="text" class="form-control modal-launch-style cell-size input-sm" readonly="readonly" id="CCell" />*@
                                    <i class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label>Bulk Cell Size</label>
                                    <select class="form-control" id="BCell" style="max-width:90%"></select>
                                    @*<input type="text" class="form-control input-sm modal-launch-style cell-size input-sm" readonly="readonly" id="BCell" />*@
                                    <i style="height:30px;width:30px;" class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label>Flow Rack Cell Size</label>
                                    <select class="form-control" id="FCell" style="max-width:90%"></select>
                                    @*<input type="text" class="form-control input-sm modal-launch-style cell-size input-sm" readonly="readonly" id="FCell" />*@
                                    <i style="height:30px;width:30px;" class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label>Carousel Velocity</label>
                                    <select class="form-control" id="CVel" style="max-width:90%"></select>
                                    @*<input type="text" class="form-control input-sm modal-launch-style velocity-modal input-sm" readonly="readonly" id="CVel" />*@
                                    <i style="height:30px;width:30px;" class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label>Bulk Velocity</label>
                                    <select class="form-control" id="BVel" style="max-width:90%"></select>
                                    @*<input type="text" class="form-control input-sm modal-launch-style velocity-modal input-sm" readonly="readonly" id="BVel" />*@
                                    <i style="height:30px;width:30px;" class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label>Flow Rack Velocity</label>
                                    <select class="form-control" id="FVel" style="max-width:90%"></select>
                                    @*<input type="text" class="form-control input-sm modal-launch-style velocity-modal input-sm" readonly="readonly" id="FVel" />*@
                                    <i style="height:30px;width:30px;" class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Primary Zone:</label>
                                <select class="form-control" id="PrimZone">
                                    <option selected="selected" value=""> </option>
                                    <option value="carousel">Carousel</option>
                                    <option value="bulk">Bulk</option>
                                    <option value="carton flow">Carton Flow</option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Secondary Zone:</label>
                                <select class="form-control" id="SecZone">
                                    <option selected="selected" value=""> </option>
                                    <option value="carousel">Carousel</option>
                                    <option value="bulk">Bulk</option>
                                    <option value="carton flow">Carton Flow</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" style="background-color:black">
                        <h3 class="panel-title">
                            <label style="color:ghostwhite">Location Info</label>
                            <div class="pull-right">
                                <button class="btn btn-primary btn-sm" id="AssignLocation" style="margin-top:-6px;">Choose Location</button>&nbsp;
                                <button class="btn btn-primary btn-sm" id="FindLocation" style="margin-top:-6px;">Find Location</button>
                            </div>
                        </h3>
                    </div>
                    <div class="panel-body" style="background-color:ghostwhite">
                        <div class="row">
                            <div class="col-md-2">
                                <label>Zone</label>
                                <input type="text" class="form-control input-sm" id="Zone" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Carousel</label>
                                <input type="text" class="form-control input-sm" id="Carousel" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Row</label>
                                <input type="text" class="form-control input-sm" id="Row" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Shelf</label>
                                <input type="text" class="form-control input-sm" id="Shelf" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Bin</label>
                                <input type="text" class="form-control input-sm" id="Bin" readonly="readonly" />
                            </div>
                            <div class="col-md-2" id="ReplenCol" style="display:none">
                                <label>Relenishment Needed</label>
                                <input type="text" class="form-control input-sm" id="ReplenNeeded" readonly="readonly" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <label>Location Cell Size</label>
                                <input type="text" class="form-control input-sm" id="CellSize" readonly />
                            </div>
                            <div class="col-md-2">
                                <label>Location Velocity Code</label>
                                <input type="text" class="form-control input-sm" id="Velocity" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Location Quantity</label>
                                <input type="text" class="form-control input-sm" id="Qty" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Max Quantity</label>
                                <input type="text" class="form-control input-sm" id="MaxQty" readonly="readonly" />
                            </div>
                            <div class="col-md-2">
                                <label>Quantity Allocated</label>
                                <input type="text" class="form-control input-sm" id="QtyAlloc" readonly="readonly" />
                            </div>
                            @*Commented out as marking a shelf as full from induction makes no sense*@
                            @*<div class="col-md-2">
                                <label style="visibility:hidden;">Full Shelf</label>
                                <button class="btn btn-primary btn-block" id="FullShelf">Full Shelf</button>
                            </div>*@
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <label>Tote ID</label>
                <select style="padding-left: 45%" class="form-control input-sm" id="AssignToTote"></select>
            </div>
            <div class="col-md-3">
                <label>Tote Position</label>
                <select style="padding-left: 45%" class="form-control input-sm" id="AssignToPosition"></select>
            </div>
            <div class="col-md-3">
                <label>Available Cells in Tote</label>
                <input type="text" class="form-control input-sm text-center" readonly="readonly" id="OpenCells" />
            </div>
            <div class="col-md-3">
                <label>Quantity to Assign</label>
                <input type="text" class="form-control text-center" id="NumberToAssign" data-default="@Model.preferences.DefPutQuant" placeholder="Quantity" />
                @*<div class="input-group">
                    <span class="input-group-btn">
                        <button class="btn btn-default nmpd-target btn-sm" id="numpadQtyAssign" data-input-target="#NumberToAssign" type="button" data-numpad="nmpd21">
                            <i class="glyphicon glyphicon-th"></i>
                        </button>
                    </span>
                </div>*@
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-3 col-md-offset-6">
                <button type="button" class="btn btn-primary pull-right" id="ContainerLocations">View Container Locations</button>
            </div>
            <div class="col-md-3">
                <button type="button" class="btn btn-primary pull-right" id="trans_set">Complete Transaction</button>
            </div>
        </div>
    </div>
</div>
