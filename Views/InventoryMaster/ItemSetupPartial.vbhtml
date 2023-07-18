<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-6">
        <div class="row">
            <div class="col-md-12">
                <div id="nan_alerts">

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Date Sensitive: <input type="checkbox" data-colname="[Inventory].[Date Sensitive]" id="dateSensitive" class="inv-edit inv-bool" /></label>
            </div>
            <div class="col-md-6">
                <label>Warehouse Sensitive: <input type="checkbox" data-colname="[Inventory].[Warehouse Sensitive]" id="warehouseSensitive" class="inv-edit inv-bool" /></label>
            </div>

        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Primary Zone: </label>
                <select class="form-control inv-edit inv-text" data-colname="[Inventory].[Primary Pick Zone]" id="pZone">
                    <option selected="selected" value=" "> </option>
                    <option value="carousel">Carousel</option>
                    <option value="bulk">Bulk</option>
                    <option value="carton flow">Carton Flow</option>
                </select>
            </div>
            <div class="col-md-6">
                <label>Secondary Zone: </label>
                <select class="form-control inv-edit inv-text" id="sZone" data-colname="[Inventory].[Secondary Pick Zone]" disabled="disabled">
                    <option selected="selected" value=" "> </option>
                    <option value="carousel">Carousel</option>
                    <option value="bulk">Bulk</option>
                    <option value="carton flow">Carton Flow</option>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>FIFO: <input type="checkbox" data-colname="[Inventory].[FIFO]" id="FIFO" class="inv-edit inv-bool" /></label>
            </div>
            <div class="col-md-6" id="FIFOshow">
                <label>FIFO Date</label><select id="FIFODate" data-colname="[Inventory].[FIFO Date]" class="form-control inv-edit inv-text"><option>Put Away Date</option><option>Expiration Date</option></select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Split Case:<input type="checkbox" data-colname="[Inventory].[Split Case]" checked="checked" id="splitCase" class="inv-edit inv-bool" /></label>
            </div>
            <div class="col-md-6">
                <label>Pick Fence Qty:</label><input type="text" data-colname="[Inventory].[Pick Fence Qty]" class="form-control inv-edit inv-num" id="pFenceQty" maxlength="9" placeholder="Pick Fence Qty" value="0" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Case Quantity:</label><input type="text" data-colname="[Inventory].[Case Quantity]" class="form-control inv-edit inv-num" id="caseQty" maxlength="9" placeholder="Case Qty" value="0" />
            </div>
            <div class="col-md-6">
                <label>Pick Sequence:</label><input type="text" data-colname="[Inventory].[Pick Sequence]" class="form-control inv-edit inv-num" id="pSequence" maxlength="9" placeholder="Pick Sequence" value="0" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <label>Active:<input type="checkbox" checked="checked" data-colname="[Inventory].[Active]" id="activeCheckbox" class="inv-edit inv-bool" /></label>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="row">
            <div class="col-md-12">
                <div id="qty_alerts">

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-md-offset-3">
                <label>Carousel:</label>
            </div>
            <div class="col-md-3">
                <label>Bulk:</label>
            </div>
            <div class="col-md-3">
                <label>Carton Flow:</label>
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-3">
                <label>Cell Size:</label>
            </div>
            <div class="col-md-3">
                <div class="form-group has-feedback" style="margin-bottom:0px;">
                    <input type="text" readonly="readonly" data-colname="[Inventory].[Cell Size]" class="form-control cell-modal modal-launch-style inv-edit inv-text"  id="carCell" disabled="disabled">
                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group has-feedback" style="margin-bottom:0px;">
                    <input type="text" readonly="readonly" data-colname="[Inventory].[Bulk Cell Size]" class="form-control cell-modal modal-launch-style inv-edit inv-text" id="bulkCell" disabled="disabled">
                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group has-feedback" style="margin-bottom:0px;">
                    <input type="text" readonly="readonly" data-colname="[Inventory].[CF Cell Size]" class="form-control cell-modal modal-launch-style inv-edit inv-text" id="cartonCell" disabled="disabled">
                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback cell-modal modal-launch-style"></i>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-3">
                <label>Velocity Code:</label>
            </div>
            <div class="col-md-3">
                <div class="form-group has-feedback" style="margin-bottom:0px;">
                    <input type="text" readonly="readonly" data-colname="[Inventory].[Golden Zone]" class="form-control velocity-modal modal-launch-style inv-edit inv-text"  id="carVel" disabled="disabled">
                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group has-feedback" style="margin-bottom:0px;">
                    <input type="text" readonly="readonly" data-colname="[Inventory].[Bulk Velocity]" class="form-control velocity-modal modal-launch-style inv-edit inv-text"  id="bulkVel" disabled="disabled">
                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group has-feedback" style="margin-bottom:0px;">
                    <input type="text" readonly="readonly" data-colname="[Inventory].[CF Velocity]" class="form-control velocity-modal modal-launch-style inv-edit inv-text" id="cartonVel" disabled="disabled">
                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback velocity-modal modal-launch-style"></i>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-3">
                <label>Minimum Cell Quantity:</label>
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control inv-edit inv-num" data-colname="[Inventory].[Min Quantity]" id="carMinQty" maxlength="9" placeholder="Carousel Minimum Cell Qty" value="0" />
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control inv-edit inv-num" id="bulkMinQty" data-colname="[Inventory].[Bulk Min Qty]" maxlength="9" placeholder="Bulk Minimum Cell Qty" value="0" />
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control inv-edit inv-num" data-colname="[Inventory].[CF Min Qty]" id="cartonMinQty" maxlength="9" placeholder="Carton Flow Minimum Cell Qty" value="0" />
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-3">
                <label>Maximum Cell Quantity:</label>
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control inv-edit inv-num" data-colname="[Inventory].[Maximum Quantity]" id="carMaxQty" maxlength="9" placeholder="Carousel Maximum Cell Qty" value="0" />
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control inv-edit inv-num" data-colname="[Inventory].[Bulk Max Qty]" id="bulkMaxQty" maxlength="9" placeholder="Bulk Maximum Cell Qty" value="0" />
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control inv-edit inv-num" data-colname="[Inventory].[CF Max Qty]" id="cartonMaxQty" maxlength="9" placeholder="Carton Flow Maximum Cell Qty" value="0" />
            </div>
        </div>

    </div>
</div>